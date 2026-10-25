---
codex: 1
project: RogueTraderBrevity
code: RTB
layer: stories
status: living
updated: 2026-10-24
---
<!-- GENERATED from MindAttic.Tickets/data/Tickets.db by `tickets db render`. Do not edit: this file is rewritten from the database; change stories and tickets with the tickets CLI. -->

# RogueTraderBrevity — User Stories
> ✅ done (shipped & tested) · 🟡 partial · ⬜ planned. Every ✅ cites the test.

## Priority backlog
_Nothing recorded yet._

## Tickets
> Permanent log of every change and fix — this repo's ticket system. Rows are append-only: never
> delete or reword one; only move its Status (open → closed / wont-do) and fill in Ref. Status is
> a word, not a glyph, so tickets don't count toward the story index. Keep this section last.

| ID | Date | Type | Status | Summary | Ref |
|---|---|---|---|---|---|
| RTB-T1 | 2026-10-24 | docs | closed | Start the permanent ticket log in the ticket database | — |
| RTB-T2 | 2026-10-24 | fix | closed | Repo has no commits, so the launcher's pull fails with 'no such ref was fetched'; add the first commit (AGENTS.md pointer, .gitignore, commit hooks) and push main | — |
| RTB-T3 | 2026-10-24 | feature | closed | Phase 1 dialogue export: a mod built from Owlcat's template that walks the dialogue blueprints (BlueprintCue, BlueprintAnswer, BlueprintBookPage) and writes dialogue.jsonl with each line's string key, speaker, dialogue tree, previous line, skill check/conviction markers and voiced flag, so the LLM simplify pass gets context | — |
| RTB-T4 | 2026-10-24 | feature | open | Phase 2 simplify: C# console app that sends each conversation from dialogue.jsonl to the Claude API (Batch API), keeps meaning, names, numbers, choice hints and markup tokens, trims to ~30-50%, caches by a hash of the original text, and supports a trial run on one companion's conversations | — |
| RTB-T5 | 2026-10-24 | feature | wont-do | Length packs: DialogueSimplify --pack 50\|30\|20 with per-pack length rules, cache entries and simplified-<pack>.json, so the overlay mod can switch between Full, 50, 30 and 20 in game | — |
| RTB-T6 | 2026-10-24 | chore | open | Rewrite all dialogue to the v3 rules with Claude Code subagents (no API key): companions first, then chapters 1-5, then DLC1-3 and the rest; import each wave into .brevity/cache.jsonl | — |
| RTB-T7 | 2026-10-24 | feature | open | Phase 4 overlay mod: a mod that loads .brevity/simplified.json and replaces those localization strings at runtime via Harmony, with an in-game toggle between full and short dialogue; game files stay untouched | — |
