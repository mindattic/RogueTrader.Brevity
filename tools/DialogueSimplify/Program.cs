using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.RegularExpressions;
using Anthropic;
using Anthropic.Exceptions;
using Anthropic.Models.Messages;
using Anthropic.Models.Messages.Batches;

const string PromptVersion = "v1";
const string SystemPrompt = """
    You shorten dialogue from the video game Warhammer 40,000: Rogue Trader. The player finds it too
    verbose and wants the same conversations in fewer words.

    You get one conversation as JSON lines. Rewrite each line's "text" and return every id you were
    given, exactly once.

    Keep:
    - The meaning, every name, place, quest fact and number.
    - Each speaker's voice and the setting's tone, in plainer and shorter sentences.
    - Anything that tells the player what a choice leads to: stances, threats, promises, the
      bracketed action or label at the start of an answer (e.g. "[Lie]", "[Touch Weisz's hand]").
    - Every markup token exactly as written, the same number of times: {g|...}...{/g}, {n}...{/n},
      {mf|...|...}, {name}, {uip|...}, {d|...}, <i>...</i> and any other {...} or <...> token. You may
      move a token with the words it wraps, but never drop, add, translate or alter one.

    Length:
    - NPC lines ("cue"): aim for 30-50% of the original length. Cut repetition, flourishes and
      restated context first; narration in {n}...{/n} can usually shrink the most.
    - Player answers ("answer"): shorten less, about 60-80%, so the player can still tell options apart.
    - Lines that are already short: return them unchanged rather than lose meaning.

    "prev" is the line shown just before, for context only; do not rewrite it.
    """;

var schema = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>("""
    {"type":"object","additionalProperties":false,"required":["lines"],
     "properties":{"lines":{"type":"array","items":{"type":"object","additionalProperties":false,
       "required":["id","text"],"properties":{"id":{"type":"string"},"text":{"type":"string"}}}}}}
    """)!;

var opts = ParseArgs(args);
var input = opts.GetValueOrDefault("in", "dialogue.jsonl");
var work = opts.GetValueOrDefault("work", ".brevity");
var model = opts.GetValueOrDefault("model", "claude-opus-5-5");
var effort = opts.GetValueOrDefault("effort", "medium");
var speaker = opts.GetValueOrDefault("speaker");
var dialogFilter = opts.GetValueOrDefault("dialog");
var limit = opts.TryGetValue("limit", out var l) ? int.Parse(l) : int.MaxValue;
var skipVoiced = opts.ContainsKey("skip-voiced");
var sync = opts.ContainsKey("sync");
var dryRun = opts.ContainsKey("dry-run");
Directory.CreateDirectory(work);

var jsonOpts = new JsonSerializerOptions { Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping, PropertyNameCaseInsensitive = true };
var lines = File.ReadLines(input).Select(x => JsonSerializer.Deserialize<Line>(x, jsonOpts)!).ToList();
var byKey = lines.ToDictionary(x => x.Key);
var cachePath = Path.Combine(work, "cache.jsonl");
var cache = File.Exists(cachePath)
    ? File.ReadLines(cachePath).Select(x => JsonSerializer.Deserialize<CacheEntry>(x, jsonOpts)!).GroupBy(c => c.Hash).ToDictionary(g => g.Key, g => g.Last())
    : new Dictionary<string, CacheEntry>();

var conversations = lines
    .Where(x => !skipVoiced || !x.Voiced)
    .GroupBy(x => x.Dialog ?? Path.GetDirectoryName(x.Blueprint)!.Replace('\\', '/'))
    .Where(g => dialogFilter is null || g.Key.Contains(dialogFilter, StringComparison.OrdinalIgnoreCase))
    .Where(g => speaker is null || g.Any(x => string.Equals(x.Speaker, speaker, StringComparison.OrdinalIgnoreCase)))
    .OrderBy(g => g.Key, StringComparer.Ordinal)
    .Take(limit)
    .ToList();

var chunks = new List<Chunk>();
foreach (var conv in conversations)
{
    var todo = conv.Where(x => !cache.ContainsKey(Hash(x.Text))).ToList();
    for (var i = 0; i < todo.Count;)
    {
        var part = new List<Line>();
        var words = 0;
        while (i < todo.Count && part.Count < 120 && (part.Count == 0 || words + todo[i].Words <= 4000)) { words += todo[i].Words; part.Add(todo[i++]); }
        chunks.Add(new Chunk($"c{chunks.Count}", conv.Key, part));
    }
}

var pendingWords = chunks.Sum(c => c.Lines.Sum(x => x.Words));
Console.WriteLine($"{conversations.Count:N0} conversations, {conversations.Sum(g => g.Count()):N0} lines; {chunks.Sum(c => c.Lines.Count):N0} lines ({pendingWords:N0} words) not cached yet, in {chunks.Count:N0} requests");
if (dryRun) { WriteOutput(); return; }

if (opts.GetValueOrDefault("export") is { Length: > 0 } exportDir)
{
    Directory.CreateDirectory(exportDir);
    File.WriteAllText(Path.Combine(exportDir, "INSTRUCTIONS.md"), SystemPrompt + """

        Output: for each `<id>.json` chunk, write `<id>.out.json` next to it containing only
        {"lines":[{"id":"L1","text":"..."}, ...]} with every id from the chunk exactly once.
        """);
    foreach (var c in chunks) File.WriteAllText(Path.Combine(exportDir, c.Id + ".json"), UserMessage(c));
    File.WriteAllText(Path.Combine(exportDir, "manifest.json"), JsonSerializer.Serialize(chunks.Select(c => new ChunkRef(c.Id, c.Conversation, c.Lines.Select(x => x.Key).ToList()))));
    Console.WriteLine($"exported {chunks.Count:N0} chunks to {Path.GetFullPath(exportDir)}");
    return;
}

var client = new Lazy<AnthropicClient>(() => new());
var statePath = Path.Combine(work, "pending-batch.json");
var stats = new Dictionary<string, int>();
var rejects = new StreamWriter(Path.Combine(work, "rejects.jsonl"), append: true, new UTF8Encoding(false));
var cacheWriter = new StreamWriter(cachePath, append: true, new UTF8Encoding(false));

if (opts.GetValueOrDefault("import") is { Length: > 0 } importDir)
{
    var refs = JsonSerializer.Deserialize<List<ChunkRef>>(File.ReadAllText(Path.Combine(importDir, "manifest.json")), jsonOpts)!;
    var imported = 0;
    foreach (var r in refs)
    {
        var outFile = Path.Combine(importDir, r.Id + ".out.json");
        if (!File.Exists(outFile)) continue;
        ApplyJson(new Chunk(r.Id, r.Conversation, r.Keys.Select(k => byKey[k]).ToList()), File.ReadAllText(outFile));
        imported++;
    }
    Console.WriteLine($"imported {imported:N0} of {refs.Count:N0} chunks");
}
else if (File.Exists(statePath))
{
    var state = JsonSerializer.Deserialize<BatchState>(File.ReadAllText(statePath), jsonOpts)!;
    Console.WriteLine($"resuming batch {state.BatchId}");
    await CollectBatch(state.BatchId, state.Chunks.ToDictionary(c => c.Id, c => new Chunk(c.Id, c.Conversation, c.Keys.Select(k => byKey[k]).ToList())));
}
else if (chunks.Count == 0) Console.WriteLine("nothing to send");
else if (sync)
{
    foreach (var c in chunks)
    {
        try
        {
            var msg = await client.Value.Messages.Create(new MessageCreateParams
            {
                Model = model, MaxTokens = 16000, System = SystemPrompt, OutputConfig = Output(),
                Messages = [new UserMessageParam { Content = UserMessage(c) }],
            });
            Apply(c, msg);
        }
        catch (AnthropicRateLimitException) { Console.WriteLine($"{c.Id}: rate limited after retries; rerun to continue"); break; }
        catch (AnthropicApiException e) { Console.WriteLine($"{c.Id}: API error {e.Message}"); Count("api error", c.Lines.Count); }
        Console.WriteLine($"  {c.Id} {c.Conversation}: done");
    }
}
else
{
    var batch = await client.Value.Messages.Batches.Create(new BatchCreateParams
    {
        Requests = chunks.Select(c => new Request
        {
            CustomID = c.Id,
            Params = new Params
            {
                Model = model, MaxTokens = 16000, System = SystemPrompt, OutputConfig = Output(),
                Messages = [new UserMessageParam { Content = UserMessage(c) }],
            },
        }).ToList(),
    });
    File.WriteAllText(statePath, JsonSerializer.Serialize(new BatchState(batch.ID, chunks.Select(c => new ChunkRef(c.Id, c.Conversation, c.Lines.Select(x => x.Key).ToList())).ToList())));
    Console.WriteLine($"created batch {batch.ID}; safe to stop and rerun the same command to resume");
    await CollectBatch(batch.ID, chunks.ToDictionary(c => c.Id));
}

cacheWriter.Dispose();
rejects.Dispose();
foreach (var (k, v) in stats.OrderBy(kv => kv.Key)) Console.WriteLine($"  {k}: {v:N0} lines");
WriteOutput();

async Task CollectBatch(string id, Dictionary<string, Chunk> byId)
{
    while (true)
    {
        var b = await client.Value.Messages.Batches.Retrieve(id);
        Console.WriteLine($"  {DateTime.Now:HH:mm:ss} {b.ProcessingStatus}: {b.RequestCounts.Succeeded} succeeded, {b.RequestCounts.Processing} processing, {b.RequestCounts.Errored} errored");
        if (b.ProcessingStatus == ProcessingStatus.Ended) break;
        await Task.Delay(TimeSpan.FromSeconds(60));
    }
    await foreach (var r in client.Value.Messages.Batches.ResultsStreaming(id))
    {
        var c = byId[r.CustomID];
        if (r.Result.TryPickSucceeded(out var ok)) Apply(c, ok.Message);
        else Count($"batch {r.Result.Value.GetType().Name.Replace("MessageBatch", "").Replace("Result", "").ToLowerInvariant()}", c.Lines.Count);
    }
    File.Delete(statePath);
}

OutputConfig Output() => new()
{
    Effort = effort switch { "low" => Effort.Low, "high" => Effort.High, "max" => Effort.Max, _ => Effort.Medium },
    Format = new JsonOutputFormat { Schema = schema },
};

string UserMessage(Chunk c)
{
    var ids = c.Lines.Select((x, i) => (x.Key, Id: $"L{i + 1}")).ToDictionary(p => p.Key, p => p.Id);
    return JsonSerializer.Serialize(new
    {
        conversation = c.Conversation,
        lines = c.Lines.Select(x => new
        {
            id = ids[x.Key],
            kind = x.Kind,
            speaker = x.Speaker ?? "(conversation owner)",
            check = x.Check,
            conviction = x.SoulMarkShift ?? x.SoulMarkRequirement,
            prev = x.PrevKey is null ? null : ids.TryGetValue(x.PrevKey, out var pid) ? pid : byKey.TryGetValue(x.PrevKey, out var p) ? p.Text : null,
            text = x.Text,
        }),
    }, new JsonSerializerOptions { Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping, DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull });
}

void Apply(Chunk c, Message msg)
{
    if (msg.StopReason != StopReason.EndTurn)
    {
        Count($"stop: {msg.StopReason}", c.Lines.Count);
        return;
    }
    ApplyJson(c, string.Concat(msg.Content.Select(b => b.Value).OfType<TextBlock>().Select(t => t.Text)));
}

void ApplyJson(Chunk c, string json)
{
    Dictionary<string, string> outById;
    try { outById = JsonSerializer.Deserialize<Reply>(json, jsonOpts)!.Lines.GroupBy(x => x.Id).ToDictionary(g => g.Key, g => g.First().Text); }
    catch (JsonException) { Count("unparseable reply", c.Lines.Count); return; }

    for (var i = 0; i < c.Lines.Count; i++)
    {
        var line = c.Lines[i];
        var reason = !outById.TryGetValue($"L{i + 1}", out var text) ? "missing"
            : string.IsNullOrWhiteSpace(text) ? "empty"
            : !Tokens(text).SequenceEqual(Tokens(line.Text)) ? "markup changed"
            : null;
        if (reason is not null)
        {
            Count($"rejected: {reason}", 1);
            rejects.WriteLine(JsonSerializer.Serialize(new { line.Key, reason, original = line.Text, simplified = text }, jsonOpts));
            continue;
        }
        var status = WordCount(text!) < line.Words ? "ok" : "unchanged";
        Count(status, 1);
        var entry = new CacheEntry(Hash(line.Text), status == "ok" ? text! : line.Text, status);
        cache[entry.Hash] = entry;
        cacheWriter.WriteLine(JsonSerializer.Serialize(entry, jsonOpts));
    }
    cacheWriter.Flush();
    rejects.Flush();
}

void WriteOutput()
{
    var outMap = new SortedDictionary<string, string>(StringComparer.Ordinal);
    long before = 0, after = 0;
    foreach (var x in lines)
        if (cache.TryGetValue(Hash(x.Text), out var e) && e.Status == "ok")
        {
            outMap[x.Key] = e.Text;
            before += x.Words;
            after += WordCount(e.Text);
        }
    var outPath = Path.Combine(work, "simplified.json");
    File.WriteAllText(outPath, JsonSerializer.Serialize(outMap, new JsonSerializerOptions { WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping }));
    Console.WriteLine($"{outMap.Count:N0} simplified lines in {Path.GetFullPath(outPath)}: {before:N0} -> {after:N0} words ({(before == 0 ? 0 : 100.0 * after / before):F0}%)");
}

void Count(string k, int n) => stats[k] = stats.GetValueOrDefault(k) + n;

static string Hash(string text) => Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(PromptVersion + "\n" + text)))[..32];

static int WordCount(string s) => s.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries).Length;

static List<string> Tokens(string s) => Regex.Matches(s, @"\{[^{}]*\}|</?\w+[^<>]*>").Select(m => m.Value).Order(StringComparer.Ordinal).ToList();

static Dictionary<string, string> ParseArgs(string[] args)
{
    var d = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
    for (var i = 0; i < args.Length; i++)
    {
        if (!args[i].StartsWith("--")) throw new ArgumentException($"Unexpected argument '{args[i]}'");
        var name = args[i][2..];
        d[name] = i + 1 < args.Length && !args[i + 1].StartsWith("--") ? args[++i] : "";
    }
    return d;
}

sealed record Line(string Key, string Kind, string? Dialog, string Blueprint, string? Speaker, string? PrevKey, string? Check,
    string? SoulMarkShift, string? SoulMarkRequirement, bool Voiced, int Words, string Text);
sealed record Chunk(string Id, string Conversation, List<Line> Lines);
sealed record ChunkRef(string Id, string Conversation, List<string> Keys);
sealed record BatchState(string BatchId, List<ChunkRef> Chunks);
sealed record CacheEntry(string Hash, string Text, string Status);
sealed record Reply(List<ReplyLine> Lines);
sealed record ReplyLine(string Id, string Text);
