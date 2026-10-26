# RogueTrader.Brevity

Shortens Warhammer 40,000: Rogue Trader's dialogue: export every conversation line, simplify it
with an LLM, and load the shorter text back through a runtime overlay mod.

Released on Nexus Mods as **Rogue Trader Brevity**: https://www.nexusmods.com/warhammer40kroguetrader/mods/651

Building needs [RogueTrader.Shared](https://github.com/mindattic/RogueTrader.Shared) checked out next to this
repo (shared game paths, references, packaging and helpers).

## Phase 1: export the dialogue

```
dotnet run -c Release --project tools/DialogueExport -- --out dialogue.jsonl
```

It reads the blueprints straight from Owlcat's modding template
(`<game>/Modding/WhRtModificationTemplate.tar`, or an extracted `Blueprints` folder via
`--blueprints`) and the English strings from `enGB.json`; no game mod is needed. `--game` overrides
the install folder (default: the Steam path on SnowCrash).

Each line of `dialogue.jsonl` is one cue (NPC line) or answer (player reply):

| Field | Meaning |
|---|---|
| `key` | String key in `enGB.json`, the key the overlay mod replaces |
| `kind` | `cue` or `answer` |
| `dialog` | The conversation it belongs to (null if no dialog reaches it) |
| `speaker` | Unit name, `Player`, `Narrator`, or null for the conversation's owner |
| `prevKey` | The line shown just before it |
| `check` | Skill check that leads to it or that the answer shows, e.g. `SkillPersuasion Hard success` |
| `soulMarkShift` / `soulMarkRequirement` | Conviction change or gate, e.g. `Hope 2` |
| `voiced` | Has voice-over (from `Sound.json`) |
| `words`, `text` | Word count and the original English text |

## Phase 2: simplify with Claude

```
dotnet run --project tools/DialogueSimplify -- --in dialogue.jsonl --speaker Idira --limit 3 --sync
```

Needs `ANTHROPIC_API_KEY` set. Each request is one conversation (split at 120 lines / 4,000 words),
sent to `claude-opus-5-5` with structured JSON output. Target length: NPC lines 20-40% of the
original (close to 20% for most; more only for exposition or emotional beats), non-verbal
description cut to a few words, player answers about 50%. Options:

- `--dry-run` counts what would be sent and sends nothing.
- `--speaker NAME`, `--dialog TEXT`, `--limit N` pick a subset; `--skip-voiced` leaves voiced lines alone.
- `--sync` sends requests one at a time (good for trials); without it the job goes through the Message
  Batches API at half price. Stopping and rerunning the same command resumes a batch.
- `--model`, `--effort low|medium|high|max` (default `medium`).
- `--export DIR` writes each pending request as `DIR/<id>.json` plus `INSTRUCTIONS.md` and a
  manifest, and needs no API key: Claude Code subagents rewrite each chunk into `<id>.out.json`.
  `--import DIR` then runs those replies through the same checks and cache.

Results are cached in `.brevity/cache.jsonl` by a hash of the original text (plus the prompt
version), so reruns and game patches only send lines that changed. A line is rejected (and kept in
`.brevity/rejects.jsonl` to retry later) if it comes back missing, empty, or with a different set of
markup tokens. `.brevity/simplified.json` maps each string key to its shortened text for the overlay mod.

## Phase 4: the overlay mod

```
dotnet build src/BrevityMod
```

Builds a Unity Mod Manager mod and installs it, with the current `.brevity/simplified.json`, into
`%USERPROFILE%\AppData\LocalLow\Owlcat Games\Warhammer 40000 Rogue Trader\UnityModManager\RogueTraderBrevity`
(the game starts its own Unity Mod Manager, so nothing else is needed). Rebuild after each import to
pick up newly shortened lines. Override `GameDir`, `UmmDir` or `ShortText` with `-p:Name=value`.

## Releases

Every release has its own list of changed lines, `docs/dialogue-polish-changes-v<major>.md`. To
release, append the new version and the current line count of `.brevity/cache.jsonl` to
`tools/releases.json`, then run `python -I tools/release_changes.py`. It rebuilds each release's
text from the append-only cache and writes the list for any release that doesn't have one yet.

## How the mod works

When the English text loads, the mod writes the shortened lines into the game's localization pack in
memory; `enGB.json` on disk is never touched. Disabling the mod in Unity Mod Manager (Ctrl+F10)
puts the original text back immediately, even mid-conversation; lines without a shortened version
always show the original.
