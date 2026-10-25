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
| RTB-T3 | 2026-10-24 | feature | open | Phase 1 dialogue export: a mod built from Owlcat's template that walks the dialogue blueprints (BlueprintCue, BlueprintAnswer, BlueprintBookPage) and writes dialogue.jsonl with each line's string key, speaker, dialogue tree, previous line, skill check/conviction markers and voiced flag, so the LLM simplify pass gets context | — |
