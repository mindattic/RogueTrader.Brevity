using System.Collections.Generic;
using System.IO;
using Kingmaker.Localization;
using Kingmaker.Localization.Enums;
using Kingmaker.Localization.Shared;
using Newtonsoft.Json;
using UnityModManagerNet;

namespace RogueTraderBrevity
{
    public static class Main
    {
        static Dictionary<string, string> shortText = new Dictionary<string, string>();
        static readonly Dictionary<string, string> originalText = new Dictionary<string, string>();
        static bool enabled = true;
        static UnityModManager.ModEntry.ModLogger log;

        static bool Load(UnityModManager.ModEntry mod)
        {
            log = mod.Logger;
            shortText = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(Path.Combine(mod.Path, "simplified.json")));
            // The game loads its pack asynchronously and may finish before or after this mod loads;
            // LocaleChanged fires once the new pack is the current one, on startup and on language change.
            LocalizationManager.Instance.LocaleChanged += _ => Apply(LocalizationManager.Instance.CurrentPack);
            mod.OnToggle = (_, on) =>
            {
                enabled = on;
                Apply(LocalizationManager.Instance.CurrentPack);
                return true;
            };
            Apply(LocalizationManager.Instance.CurrentPack);
            log.Log($"{shortText.Count} shortened lines ready");
            return true;
        }

        // The game reads every LocalizedString from the pack on each access, so rewriting the pack
        // switches dialogue live, including mid-conversation.
        internal static void Apply(LocalizationPack pack)
        {
            if (pack == null || pack.Locale != Locale.enGB) return;
            var applied = 0;
            foreach (var entry in shortText)
            {
                if (!originalText.TryGetValue(entry.Key, out var original))
                {
                    if (!pack.TryGetText(entry.Key, out original, false)) continue;
                    originalText[entry.Key] = original;
                }
                pack.PutString(entry.Key, enabled ? entry.Value : original);
                applied++;
            }
            log?.Log($"{(enabled ? "short" : "full")} dialogue on {applied} lines");
        }
    }
}
