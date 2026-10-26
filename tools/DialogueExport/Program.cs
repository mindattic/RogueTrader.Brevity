using RogueTrader.Shared;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.RegularExpressions;

var opts = ParseArgs(args);
var game = opts.GetValueOrDefault("game", @"D:\Games\Steam\steamapps\common\Warhammer 40,000 Rogue Trader");
var blueprints = opts.GetValueOrDefault("blueprints", Path.Combine(game, "Modding", "WhRtModificationTemplate.tar"));
var outPath = opts.GetValueOrDefault("out", "dialogue.jsonl");
var locDir = Path.Combine(game, "WH40KRT_Data", "StreamingAssets", "Localization");

var strings = LoadStrings(Path.Combine(locDir, "enGB.json"));
var voiced = LoadStrings(Path.Combine(locDir, "Sound.json"));
Console.WriteLine($"enGB strings: {strings.Count:N0}, voiced keys: {voiced.Count:N0}");

var nodes = new Dictionary<string, Node>();
var unitNames = new Dictionary<string, string>();
var typeRx = new Regex(@"""\$type"": ""[0-9a-f]+, (\w+)""");
var scanned = 0;

foreach (var (path, json) in BlueprintArchive.Read(blueprints))
{
    if (++scanned % 20000 == 0) Console.WriteLine($"  scanned {scanned:N0} blueprints");
    var m = typeRx.Match(json, 0, Math.Min(json.Length, 400));
    if (!m.Success || !Node.Kinds.Contains(m.Groups[1].Value) && m.Groups[1].Value != "BlueprintUnit") continue;

    using var doc = JsonDocument.Parse(json);
    var root = doc.RootElement;
    var id = root.GetProperty("AssetId").GetString()!;
    var data = root.GetProperty("Data");

    if (m.Groups[1].Value == "BlueprintUnit")
    {
        if (data.TryGetProperty("LocalizedName", out var ln) && ln.ValueKind == JsonValueKind.Object
            && ln.TryGetProperty("stringkey", out var sk) && strings.TryGetValue(sk.GetString() ?? "", out var name))
            unitNames[id] = name;
        continue;
    }

    var deleted = root.TryGetProperty("Meta", out var meta) && meta.TryGetProperty("ShadowDeleted", out var sd) && sd.GetBoolean();
    nodes[id] = Node.From(m.Groups[1].Value, id, path, data, deleted);
}
Console.WriteLine($"blueprints scanned: {scanned:N0}, dialogue nodes: {nodes.Count:N0}, named units: {unitNames.Count:N0}");

var visits = new Dictionary<string, Visit>();
foreach (var dialog in nodes.Values.Where(n => n.Kind == "BlueprintDialog").OrderBy(n => n.Path, StringComparer.Ordinal))
{
    var queue = new Queue<(string Id, string? PrevKey, string? Check)>();
    var seen = new HashSet<string>();
    foreach (var next in dialog.Next) queue.Enqueue((next, null, null));
    while (queue.Count > 0)
    {
        var (id, prevKey, check) = queue.Dequeue();
        if (!seen.Add(id) || !nodes.TryGetValue(id, out var node)) continue;

        var isText = node.TextKey is not null;
        if (isText) visits.TryAdd(id, new Visit(dialog.Path, prevKey, check));

        var carryKey = isText ? node.TextKey : prevKey;
        if (node.Kind == "BlueprintCheck")
        {
            if (node.Success is not null) queue.Enqueue((node.Success, carryKey, $"{node.CheckType} {node.Dc} success"));
            if (node.Fail is not null) queue.Enqueue((node.Fail, carryKey, $"{node.CheckType} {node.Dc} fail"));
            continue;
        }
        foreach (var next in node.Next) queue.Enqueue((next, carryKey, null));
    }
}

var jsonOpts = new JsonSerializerOptions { Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping };
var written = new HashSet<string>();
long words = 0, voicedWords = 0;
int orphans = 0;
using (var w = new StreamWriter(outPath, false, new UTF8Encoding(false)))
{
    foreach (var n in nodes.Values.Where(n => n.TextKey is not null && !n.Deleted).OrderBy(n => n.Path, StringComparer.Ordinal))
    {
        if (!strings.TryGetValue(n.TextKey!, out var text) || string.IsNullOrWhiteSpace(text) || !written.Add(n.TextKey!)) continue;
        visits.TryGetValue(n.Id, out var v);
        if (v is null) orphans++;

        var speaker = n.Kind == "BlueprintAnswer" ? "Player"
            : n.IsNarrator ? "Narrator"
            : n.SpeakerRef is not null ? unitNames.GetValueOrDefault(n.SpeakerRef) : null;
        var count = text.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries).Length;
        var isVoiced = voiced.ContainsKey(n.TextKey!);
        words += count;
        if (isVoiced) voicedWords += count;

        w.WriteLine(JsonSerializer.Serialize(new
        {
            key = n.TextKey,
            kind = n.Kind == "BlueprintAnswer" ? "answer" : "cue",
            dialog = v?.Dialog,
            blueprint = n.Path,
            speaker,
            prevKey = v?.PrevKey,
            check = v?.Check ?? n.ShowCheck,
            soulMarkShift = n.SoulShift,
            soulMarkRequirement = n.SoulRequirement,
            voiced = isVoiced,
            words = count,
            text,
        }, jsonOpts));
    }
}
Console.WriteLine($"wrote {written.Count:N0} lines ({words:N0} words, {voicedWords:N0} voiced) to {Path.GetFullPath(outPath)}; {orphans:N0} not reachable from any dialog");

static Dictionary<string, string> ParseArgs(string[] args)
{
    var d = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
    for (var i = 0; i + 1 < args.Length; i += 2)
    {
        if (!args[i].StartsWith("--")) throw new ArgumentException($"Unexpected argument '{args[i]}'. Use --game, --blueprints, --out.");
        d[args[i][2..]] = args[i + 1];
    }
    return d;
}

static Dictionary<string, string> LoadStrings(string path)
{
    using var fs = File.OpenRead(path);
    using var doc = JsonDocument.Parse(fs);
    var d = new Dictionary<string, string>();
    foreach (var p in doc.RootElement.GetProperty("strings").EnumerateObject())
        d[p.Name] = p.Value.GetProperty("Text").GetString() ?? "";
    return d;
}

sealed record Visit(string Dialog, string? PrevKey, string? Check);

sealed class Node
{
    public static readonly HashSet<string> Kinds =
        ["BlueprintDialog", "BlueprintCue", "BlueprintAnswer", "BlueprintAnswersList", "BlueprintCueSequence", "BlueprintCheck", "BlueprintBookPage"];

    public required string Kind, Id, Path;
    public bool Deleted, IsNarrator;
    public string? TextKey, SpeakerRef, ShowCheck, SoulShift, SoulRequirement, Success, Fail, CheckType, Dc;
    public List<string> Next = [];

    public static Node From(string kind, string id, string path, JsonElement d, bool deleted)
    {
        var n = new Node { Kind = kind, Id = id, Path = path[..^4], Deleted = deleted };
        switch (kind)
        {
            case "BlueprintDialog": n.AddRefs(d, "FirstCue", "Cues"); break;
            case "BlueprintBookPage": n.AddRefs(d, "Cues"); n.AddRefs(d, "Answers"); goto case "BlueprintCue";
            case "BlueprintAnswersList": n.AddRefs(d, "Answers"); break;
            case "BlueprintCueSequence": n.AddRefs(d, "Cues"); n.AddRef(d, "m_Exit"); break;
            case "BlueprintCheck":
                n.Success = Ref(d, "m_Success");
                n.Fail = Ref(d, "m_Fail");
                n.CheckType = Str(d, "Type");
                n.Dc = Difficulty(d);
                break;
            case "BlueprintCue":
                if (kind == "BlueprintCue") { n.AddRefs(d, "Continue", "Cues"); n.AddRefs(d, "Answers"); }
                n.IsNarrator = d.TryGetProperty("IsNarratorText", out var nt) && nt.GetBoolean();
                n.SpeakerRef = d.TryGetProperty("Speaker", out var sp) && sp.ValueKind == JsonValueKind.Object ? Ref(sp, "m_Blueprint") : null;
                n.ReadCommon(d);
                break;
            case "BlueprintAnswer":
                n.AddRefs(d, "NextCue", "Cues");
                if (d.TryGetProperty("ShowCheck", out var sc) && Str(sc, "Type") is { } t && t != "Unknown")
                    n.ShowCheck = $"{t} {Difficulty(sc)}";
                n.ReadCommon(d);
                break;
        }
        return n;
    }

    void ReadCommon(JsonElement d)
    {
        if (d.TryGetProperty("Text", out var t) && t.ValueKind == JsonValueKind.Object && Str(t, "m_Key") is { Length: > 0 } key) TextKey = key;
        SoulShift = SoulMark(d, "SoulMarkShift");
        SoulRequirement = SoulMark(d, "SoulMarkRequirement");
    }

    static string Difficulty(JsonElement d) =>
        d.TryGetProperty("DC", out var dc) && dc.GetInt32() is var v and > 0 ? $"DC{v}" : Str(d, "Difficulty") ?? "DC0";

    static string? SoulMark(JsonElement d, string prop) =>
        d.TryGetProperty(prop, out var s) && s.ValueKind == JsonValueKind.Object && Str(s, "Direction") is { } dir && dir != "None"
            ? $"{dir} {(s.TryGetProperty("Value", out var v) ? v.GetInt32() : 0)}" : null;

    void AddRefs(JsonElement d, string prop, string? inner = null)
    {
        if (!d.TryGetProperty(prop, out var e)) return;
        if (inner is not null && (e.ValueKind != JsonValueKind.Object || !e.TryGetProperty(inner, out e))) return;
        if (e.ValueKind != JsonValueKind.Array) return;
        foreach (var x in e.EnumerateArray())
            if (Strip(x.ValueKind == JsonValueKind.String ? x.GetString() : null) is { } r) Next.Add(r);
    }

    void AddRef(JsonElement d, string prop) { if (Ref(d, prop) is { } r) Next.Add(r); }

    static string? Ref(JsonElement d, string prop) =>
        d.TryGetProperty(prop, out var e) && e.ValueKind == JsonValueKind.String ? Strip(e.GetString()) : null;

    static string? Str(JsonElement d, string prop) =>
        d.TryGetProperty(prop, out var e) && e.ValueKind == JsonValueKind.String ? e.GetString() : null;

    static string? Strip(string? r) => r is { Length: > 4 } && r.StartsWith("!bp_") ? r[4..] : null;
}
