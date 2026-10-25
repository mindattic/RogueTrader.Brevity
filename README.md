# RogueTraderBrevity

Shortens Warhammer 40,000: Rogue Trader's dialogue: export every conversation line, simplify it
with an LLM, and load the shorter text back through a runtime overlay mod.

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
sent to `claude-opus-5-5` with structured JSON output. Options:

- `--dry-run` counts what would be sent and sends nothing.
- `--speaker NAME`, `--dialog TEXT`, `--limit N` pick a subset; `--skip-voiced` leaves voiced lines alone.
- `--sync` sends requests one at a time (good for trials); without it the job goes through the Message
  Batches API at half price. Stopping and rerunning the same command resumes a batch.
- `--model`, `--effort low|medium|high|max` (default `medium`).

Results are cached in `.brevity/cache.jsonl` by a hash of the original text (plus the prompt
version), so reruns and game patches only send lines that changed. A line is rejected (and kept in
`.brevity/rejects.jsonl` to retry later) if it comes back missing, empty, or with a different set of
markup tokens. `.brevity/simplified.json` maps each string key to its shortened text for the overlay mod.
