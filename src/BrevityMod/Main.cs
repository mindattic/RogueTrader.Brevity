using System.Collections.Generic;
using System.IO;
using Kingmaker.Localization;
using Newtonsoft.Json;
using RogueTrader.Shared;
using UnityModManagerNet;

namespace RogueTraderBrevity
{
    public static class Main
    {
        static LocalizationOverlay overlay;
        static bool enabled = true;
        static UnityModManager.ModEntry.ModLogger log;

        static bool Load(UnityModManager.ModEntry mod)
        {
            log = mod.Logger;
            overlay = new LocalizationOverlay(JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(Path.Combine(mod.Path, "simplified.json"))));
            // The game loads its pack asynchronously and may finish before or after this mod loads;
            // LocaleChanged fires once the new pack is the current one, on startup and on language change.
            LocalizationManager.Instance.LocaleChanged += _ => Apply();
            mod.OnToggle = (_, on) =>
            {
                enabled = on;
                Apply();
                return true;
            };
            Apply();
            log.Log($"{overlay.Count} shortened lines ready");
            return true;
        }

        // Rewriting the pack switches dialogue live, including mid-conversation.
        static void Apply()
        {
            var applied = overlay.Apply(LocalizationManager.Instance.CurrentPack, enabled);
            if (applied >= 0)
                log?.Log($"{(enabled ? "short" : "full")} dialogue on {applied} lines");
        }
    }
}
