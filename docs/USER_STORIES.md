---
codex: 1
project: RogueTrader.Brevity
code: RTB
layer: stories
status: living
updated: 2026-10-24
---
<!-- GENERATED from MindAttic.Tickets/data/Tickets.db by `tickets db render`. Do not edit: this file is rewritten from the database; change stories and tickets with the tickets CLI. -->

# RogueTrader.Brevity — User Stories
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
| RTB-T4 | 2026-10-24 | feature | closed | Phase 2 simplify: C# console app that sends each conversation from dialogue.jsonl to the Claude API (Batch API), keeps meaning, names, numbers, choice hints and markup tokens, trims to ~30-50%, caches by a hash of the original text, and supports a trial run on one companion's conversations | tools/DialogueSimplify: Batch API + --sync + --export/--import; the full run used --export/--import with plan subagents (RTB-T6) |
| RTB-T5 | 2026-10-24 | feature | wont-do | Length packs: DialogueSimplify --pack 50\|30\|20 with per-pack length rules, cache entries and simplified-<pack>.json, so the overlay mod can switch between Full, 50, 30 and 20 in game | — |
| RTB-T6 | 2026-10-24 | chore | closed | Rewrite all dialogue to the v3 rules with Claude Code subagents (no API key): companions first, then chapters 1-5, then DLC1-3 and the rest; import each wave into .brevity/cache.jsonl | all 1,393 chunks imported: 39,650 lines, 1,263,733 -> 408,983 words (32%); 0 uncached; mod rebuilt |
| RTB-T7 | 2026-10-24 | feature | closed | Phase 4 overlay mod: a mod that loads .brevity/simplified.json and replaces those localization strings at runtime via Harmony, with an in-game toggle between full and short dialogue; game files stay untouched | Owner's in-game check: mod on/off screenshot pairs, then 1.0.0 release |
| RTB-T8 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 0/237 (2 chunks: c0, c1) | RTB-T6 |
| RTB-T9 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 1/237 (3 chunks: c2, c3, c4) | RTB-T6 |
| RTB-T10 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 2/237 (4 chunks: c5, c6, c7...) | RTB-T6 |
| RTB-T11 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 3/237 (3 chunks: c9, c10, c11) | RTB-T6 |
| RTB-T12 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 4/237 (2 chunks: c12, c13) | RTB-T6 |
| RTB-T13 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 5/237 (3 chunks: c14, c15, c16) | RTB-T6 |
| RTB-T14 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 6/237 (2 chunks: c17, c18) | RTB-T6 |
| RTB-T15 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 7/237 (1 chunks: c19) | RTB-T6 |
| RTB-T16 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 8/237 (3 chunks: c20, c21, c22) | RTB-T6 |
| RTB-T17 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 9/237 (5 chunks: c23, c24, c25...) | RTB-T6 |
| RTB-T18 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 10/237 (5 chunks: c28, c29, c30...) | RTB-T6 |
| RTB-T19 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 11/237 (10 chunks: c33, c34, c35...) | RTB-T6 |
| RTB-T20 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 12/237 (4 chunks: c43, c44, c45...) | RTB-T6 |
| RTB-T21 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 13/237 (4 chunks: c47, c48, c49...) | RTB-T6 |
| RTB-T22 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 14/237 (7 chunks: c51, c52, c53...) | RTB-T6 |
| RTB-T23 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 15/237 (5 chunks: c58, c59, c60...) | RTB-T6 |
| RTB-T24 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 16/237 (3 chunks: c63, c64, c65) | RTB-T6 |
| RTB-T25 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 17/237 (5 chunks: c66, c67, c68...) | RTB-T6 |
| RTB-T26 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 18/237 (9 chunks: c71, c72, c73...) | RTB-T6 |
| RTB-T27 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 19/237 (5 chunks: c80, c81, c82...) | RTB-T6 |
| RTB-T28 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 20/237 (7 chunks: c85, c86, c87...) | RTB-T6 |
| RTB-T29 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 21/237 (6 chunks: c92, c93, c94...) | RTB-T6 |
| RTB-T30 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 22/237 (8 chunks: c98, c99, c100...) | RTB-T6 |
| RTB-T31 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 23/237 (4 chunks: c106, c107, c108...) | RTB-T6 |
| RTB-T32 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 24/237 (4 chunks: c110, c111, c112...) | RTB-T6 |
| RTB-T33 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 25/237 (14 chunks: c114, c115, c116...) | RTB-T6 |
| RTB-T34 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 26/237 (14 chunks: c128, c129, c130...) | RTB-T6 |
| RTB-T35 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 27/237 (6 chunks: c142, c143, c144...) | RTB-T6 |
| RTB-T36 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 28/237 (2 chunks: c148, c149) | RTB-T6 |
| RTB-T37 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 29/237 (2 chunks: c150, c151) | RTB-T6 |
| RTB-T38 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 30/237 (4 chunks: c152, c153, c154...) | RTB-T6 |
| RTB-T39 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 31/237 (9 chunks: c156, c157, c158...) | RTB-T6 |
| RTB-T40 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 32/237 (5 chunks: c165, c166, c167...) | RTB-T6 |
| RTB-T41 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 33/237 (9 chunks: c170, c171, c172...) | RTB-T6 |
| RTB-T42 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 34/237 (2 chunks: c179, c180) | RTB-T6 |
| RTB-T43 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 35/237 (1 chunks: c181) | RTB-T6 |
| RTB-T44 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 36/237 (9 chunks: c182, c183, c184...) | RTB-T6 |
| RTB-T45 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 37/237 (5 chunks: c191, c192, c193...) | RTB-T6 |
| RTB-T46 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 38/237 (4 chunks: c196, c197, c198...) | RTB-T6 |
| RTB-T47 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 39/237 (2 chunks: c200, c201) | RTB-T6 |
| RTB-T48 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 40/237 (4 chunks: c202, c203, c204...) | RTB-T6 |
| RTB-T49 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 41/237 (4 chunks: c206, c207, c208...) | RTB-T6 |
| RTB-T50 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 42/237 (3 chunks: c210, c211, c212) | RTB-T6 |
| RTB-T51 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 43/237 (6 chunks: c213, c214, c215...) | RTB-T6 |
| RTB-T52 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 44/237 (21 chunks: c219, c220, c221...) | RTB-T6 |
| RTB-T53 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 45/237 (7 chunks: c240, c241, c242...) | RTB-T6 |
| RTB-T54 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 46/237 (3 chunks: c247, c248, c249) | RTB-T6 |
| RTB-T55 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 47/237 (4 chunks: c250, c251, c252...) | RTB-T6 |
| RTB-T56 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 48/237 (3 chunks: c254, c255, c256) | RTB-T6 |
| RTB-T57 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 49/237 (3 chunks: c257, c258, c259) | RTB-T6 |
| RTB-T58 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 50/237 (6 chunks: c260, c261, c262...) | RTB-T6 |
| RTB-T59 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 51/237 (6 chunks: c266, c267, c268...) | RTB-T6 |
| RTB-T60 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 52/237 (3 chunks: c272, c273, c274) | RTB-T6 |
| RTB-T61 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 53/237 (2 chunks: c275, c276) | RTB-T6 |
| RTB-T62 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 54/237 (4 chunks: c277, c278, c279...) | RTB-T6 |
| RTB-T63 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 55/237 (6 chunks: c281, c282, c283...) | RTB-T6 |
| RTB-T64 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 56/237 (4 chunks: c287, c288, c289...) | RTB-T6 |
| RTB-T65 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 57/237 (4 chunks: c291, c292, c293...) | RTB-T6 |
| RTB-T66 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 58/237 (12 chunks: c295, c296, c297...) | RTB-T6 |
| RTB-T67 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 59/237 (4 chunks: c307, c308, c309...) | RTB-T6 |
| RTB-T68 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 60/237 (6 chunks: c311, c312, c313...) | RTB-T6 |
| RTB-T69 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 61/237 (6 chunks: c317, c318, c319...) | RTB-T6 |
| RTB-T70 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 62/237 (10 chunks: c323, c324, c325...) | RTB-T6 |
| RTB-T71 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 63/237 (6 chunks: c333, c334, c335...) | RTB-T6 |
| RTB-T72 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 64/237 (7 chunks: c339, c340, c341...) | RTB-T6 |
| RTB-T73 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 65/237 (6 chunks: c346, c347, c348...) | RTB-T6 |
| RTB-T74 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 66/237 (6 chunks: c352, c353, c354...) | RTB-T6 |
| RTB-T75 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 67/237 (3 chunks: c358, c359, c360) | RTB-T6 |
| RTB-T76 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 68/237 (5 chunks: c361, c362, c363...) | RTB-T6 |
| RTB-T77 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 69/237 (3 chunks: c366, c367, c368) | RTB-T6 |
| RTB-T78 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 70/237 (6 chunks: c369, c370, c371...) | RTB-T6 |
| RTB-T79 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 71/237 (5 chunks: c375, c376, c377...) | RTB-T6 |
| RTB-T80 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 72/237 (15 chunks: c380, c381, c382...) | RTB-T6 |
| RTB-T81 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 73/237 (6 chunks: c395, c396, c397...) | RTB-T6 |
| RTB-T82 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 74/237 (21 chunks: c401, c402, c403...) | RTB-T6 |
| RTB-T83 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 75/237 (4 chunks: c422, c423, c424...) | RTB-T6 |
| RTB-T84 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 76/237 (5 chunks: c426, c427, c428...) | RTB-T6 |
| RTB-T85 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 77/237 (5 chunks: c431, c432, c433...) | RTB-T6 |
| RTB-T86 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 78/237 (6 chunks: c436, c437, c438...) | RTB-T6 |
| RTB-T87 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 79/237 (5 chunks: c442, c443, c444...) | RTB-T6 |
| RTB-T88 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 80/237 (5 chunks: c447, c448, c449...) | RTB-T6 |
| RTB-T89 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 81/237 (6 chunks: c452, c453, c454...) | RTB-T6 |
| RTB-T90 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 82/237 (29 chunks: c458, c459, c460...) | RTB-T6 |
| RTB-T91 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 83/237 (29 chunks: c487, c488, c489...) | RTB-T6 |
| RTB-T92 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 84/237 (15 chunks: c516, c517, c518...) | RTB-T6 |
| RTB-T93 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 85/237 (12 chunks: c531, c532, c533...) | RTB-T6 |
| RTB-T94 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 86/237 (19 chunks: c543, c544, c545...) | RTB-T6 |
| RTB-T95 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 87/237 (16 chunks: c562, c563, c564...) | RTB-T6 |
| RTB-T96 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 88/237 (14 chunks: c578, c579, c580...) | RTB-T6 |
| RTB-T97 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 89/237 (36 chunks: c592, c593, c594...) | RTB-T6 |
| RTB-T98 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 90/237 (33 chunks: c628, c629, c630...) | RTB-T6 |
| RTB-T99 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 91/237 (8 chunks: c661, c662, c663...) | RTB-T6 |
| RTB-T100 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 92/237 (7 chunks: c669, c670, c671...) | RTB-T6 |
| RTB-T101 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 93/237 (4 chunks: c676, c677, c678...) | RTB-T6 |
| RTB-T102 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 94/237 (5 chunks: c680, c681, c682...) | RTB-T6 |
| RTB-T103 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 95/237 (3 chunks: c685, c686, c687) | RTB-T6 |
| RTB-T104 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 96/237 (4 chunks: c688, c689, c690...) | RTB-T6 |
| RTB-T105 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 97/237 (3 chunks: c692, c693, c694) | RTB-T6 |
| RTB-T106 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 98/237 (3 chunks: c695, c696, c697) | RTB-T6 |
| RTB-T107 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 99/237 (3 chunks: c698, c699, c700) | RTB-T6 |
| RTB-T108 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 100/237 (3 chunks: c701, c702, c703) | RTB-T6 |
| RTB-T109 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 101/237 (6 chunks: c704, c705, c706...) | check.py OK on .brevity/all c704-c709 (group 101 done in the RTB-T6 run) |
| RTB-T110 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 102/237 (5 chunks: c710, c711, c712...) | check.py OK on .brevity/all group 102 (done in the RTB-T6 run) |
| RTB-T111 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 103/237 (3 chunks: c715, c716, c717) | check.py OK on .brevity/all group 103 (done in the RTB-T6 run) |
| RTB-T112 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 104/237 (5 chunks: c718, c719, c720...) | check.py OK on .brevity/all group 104 (done in the RTB-T6 run) |
| RTB-T113 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 105/237 (7 chunks: c723, c724, c725...) | check.py OK on .brevity/all group 105 (done in the RTB-T6 run) |
| RTB-T114 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 106/237 (6 chunks: c730, c731, c732...) | check.py OK on .brevity/all group 106 |
| RTB-T115 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 107/237 (5 chunks: c736, c737, c738...) | check.py OK on .brevity/all group 107 |
| RTB-T116 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 108/237 (5 chunks: c741, c742, c743...) | check.py OK on .brevity/all group 108 |
| RTB-T117 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 109/237 (4 chunks: c746, c747, c748...) | check.py OK on .brevity/all group 109 |
| RTB-T118 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 110/237 (2 chunks: c750, c751) | check.py OK on .brevity/all group 110 |
| RTB-T119 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 111/237 (7 chunks: c752, c753, c754...) | check.py OK on .brevity/all group 111 |
| RTB-T120 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 112/237 (6 chunks: c759, c760, c761...) | check.py OK on .brevity/all group 112 |
| RTB-T121 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 113/237 (2 chunks: c765, c766) | check.py OK on .brevity/all group 113 |
| RTB-T122 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 114/237 (1 chunks: c767) | check.py OK on .brevity/all group 114 |
| RTB-T123 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 115/237 (3 chunks: c768, c769, c770) | check.py OK on .brevity/all group 115 |
| RTB-T124 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 116/237 (6 chunks: c771, c772, c773...) | check.py OK on .brevity/all group 116 |
| RTB-T125 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 117/237 (2 chunks: c777, c778) | check.py OK on .brevity/all group 117 |
| RTB-T126 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 118/237 (4 chunks: c779, c780, c781...) | check.py OK on .brevity/all group 118 |
| RTB-T127 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 119/237 (3 chunks: c783, c784, c785) | check.py OK on .brevity/all group 119 |
| RTB-T128 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 120/237 (6 chunks: c786, c787, c788...) | check.py OK on .brevity/all group 120 |
| RTB-T129 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 121/237 (4 chunks: c792, c793, c794...) | check.py OK on .brevity/all group 121 |
| RTB-T130 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 122/237 (3 chunks: c796, c797, c798) | check.py OK on .brevity/all group 122 |
| RTB-T131 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 123/237 (2 chunks: c799, c800) | check.py OK on .brevity/all group 123 |
| RTB-T132 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 124/237 (4 chunks: c801, c802, c803...) | check.py OK on .brevity/all group 124 |
| RTB-T133 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 125/237 (8 chunks: c805, c806, c807...) | check.py OK on .brevity/all group 125 |
| RTB-T134 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 126/237 (3 chunks: c813, c814, c815) | check.py OK on .brevity/all group 126 |
| RTB-T135 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 127/237 (2 chunks: c816, c817) | check.py OK on .brevity/all group 127 |
| RTB-T136 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 128/237 (4 chunks: c818, c819, c820...) | check.py OK on .brevity/all group 128 |
| RTB-T137 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 129/237 (4 chunks: c822, c823, c824...) | check.py OK on .brevity/all group 129 |
| RTB-T138 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 130/237 (3 chunks: c826, c827, c828) | check.py OK on .brevity/all group 130 |
| RTB-T139 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 131/237 (6 chunks: c829, c830, c831...) | check.py OK on .brevity/all group 131 |
| RTB-T140 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 132/237 (10 chunks: c835, c836, c837...) | check.py OK on .brevity/all group 132 |
| RTB-T141 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 133/237 (4 chunks: c845, c846, c847...) | check.py OK on .brevity/all group 133 |
| RTB-T142 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 134/237 (4 chunks: c849, c850, c851...) | check.py OK on .brevity/all group 134 |
| RTB-T143 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 135/237 (3 chunks: c853, c854, c855) | check.py OK on .brevity/all group 135 |
| RTB-T144 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 136/237 (4 chunks: c856, c857, c858...) | check.py OK on .brevity/all group 136 |
| RTB-T145 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 137/237 (5 chunks: c860, c861, c862...) | check.py OK on .brevity/all group 137 |
| RTB-T146 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 138/237 (4 chunks: c865, c866, c867...) | check.py OK on .brevity/all group 138 |
| RTB-T147 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 139/237 (4 chunks: c869, c870, c871...) | check.py OK on .brevity/all group 139 |
| RTB-T148 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 140/237 (5 chunks: c873, c874, c875...) | check.py OK on .brevity/all group 140 |
| RTB-T149 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 141/237 (5 chunks: c878, c879, c880...) | check.py OK on .brevity/all group 141 |
| RTB-T150 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 142/237 (6 chunks: c883, c884, c885...) | check.py OK on .brevity/all group 142 |
| RTB-T151 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 143/237 (6 chunks: c889, c890, c891...) | check.py OK on .brevity/all group 143 |
| RTB-T152 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 144/237 (4 chunks: c895, c896, c897...) | check.py OK on .brevity/all group 144 |
| RTB-T153 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 145/237 (2 chunks: c899, c900) | check.py OK on .brevity/all group 145 |
| RTB-T154 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 146/237 (5 chunks: c901, c902, c903...) | check.py OK on .brevity/all group 146 |
| RTB-T155 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 147/237 (1 chunks: c906) | check.py OK on .brevity/all group 147 |
| RTB-T156 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 148/237 (3 chunks: c907, c908, c909) | check.py OK on .brevity/all group 148 |
| RTB-T157 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 149/237 (3 chunks: c910, c911, c912) | check.py OK on .brevity/all group 149 |
| RTB-T158 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 150/237 (3 chunks: c913, c914, c915) | check.py OK on .brevity/all group 150 |
| RTB-T159 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 151/237 (5 chunks: c916, c917, c918...) | check.py OK on .brevity/all group 151 |
| RTB-T160 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 152/237 (3 chunks: c921, c922, c923) | check.py OK on .brevity/all group 152 |
| RTB-T161 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 153/237 (4 chunks: c924, c925, c926...) | check.py OK on .brevity/all group 153 |
| RTB-T162 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 154/237 (6 chunks: c928, c929, c930...) | check.py OK on .brevity/all group 154 |
| RTB-T163 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 155/237 (4 chunks: c934, c935, c936...) | check.py OK on .brevity/all group 155 |
| RTB-T164 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 156/237 (6 chunks: c938, c939, c940...) | check.py OK on .brevity/all group 156 |
| RTB-T165 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 157/237 (8 chunks: c944, c945, c946...) | check.py OK on .brevity/all group 157 |
| RTB-T166 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 158/237 (6 chunks: c952, c953, c954...) | check.py OK on .brevity/all group 158 |
| RTB-T167 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 159/237 (3 chunks: c958, c959, c960) | check.py OK on .brevity/all group 159 |
| RTB-T168 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 160/237 (3 chunks: c961, c962, c963) | check.py OK on .brevity/all group 160 |
| RTB-T169 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 161/237 (4 chunks: c964, c965, c966...) | check.py OK on .brevity/all group 161 |
| RTB-T170 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 162/237 (5 chunks: c968, c969, c970...) | check.py OK on .brevity/all group 162 |
| RTB-T171 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 163/237 (4 chunks: c973, c974, c975...) | check.py OK on .brevity/all group 163 |
| RTB-T172 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 164/237 (2 chunks: c977, c978) | check.py OK on .brevity/all group 164 |
| RTB-T173 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 165/237 (2 chunks: c979, c980) | check.py OK on .brevity/all group 165 |
| RTB-T174 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 166/237 (2 chunks: c981, c982) | check.py OK on .brevity/all group 166 |
| RTB-T175 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 167/237 (3 chunks: c983, c984, c985) | check.py OK on .brevity/all group 167 |
| RTB-T176 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 168/237 (3 chunks: c986, c987, c988) | check.py OK on .brevity/all group 168 |
| RTB-T177 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 169/237 (2 chunks: c989, c990) | check.py OK on .brevity/all group 169 |
| RTB-T178 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 170/237 (3 chunks: c991, c992, c993) | check.py OK on .brevity/all group 170 |
| RTB-T179 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 171/237 (1 chunks: c994) | check.py OK on .brevity/all group 171 |
| RTB-T180 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 172/237 (2 chunks: c995, c996) | check.py OK on .brevity/all group 172 |
| RTB-T181 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 173/237 (3 chunks: c997, c998, c999) | check.py OK on .brevity/all group 173 |
| RTB-T182 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 174/237 (5 chunks: c1000, c1001, c1002...) | check.py OK on .brevity/all group 174 |
| RTB-T183 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 175/237 (3 chunks: c1005, c1006, c1007) | check.py OK on .brevity/all group 175 |
| RTB-T184 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 176/237 (5 chunks: c1008, c1009, c1010...) | check.py OK on .brevity/all group 176 |
| RTB-T185 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 177/237 (5 chunks: c1013, c1014, c1015...) | check.py OK on .brevity/all group 177 |
| RTB-T186 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 178/237 (5 chunks: c1018, c1019, c1020...) | check.py OK on .brevity/all group 178 |
| RTB-T187 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 179/237 (11 chunks: c1023, c1024, c1025...) | check.py OK on .brevity/all group 179 |
| RTB-T188 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 180/237 (6 chunks: c1034, c1035, c1036...) | check.py OK on .brevity/all group 180 |
| RTB-T189 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 181/237 (2 chunks: c1040, c1041) | check.py OK on .brevity/all group 181 |
| RTB-T190 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 182/237 (2 chunks: c1042, c1043) | check.py OK on .brevity/all group 182 |
| RTB-T191 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 183/237 (2 chunks: c1044, c1045) | check.py OK on .brevity/all group 183 |
| RTB-T192 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 184/237 (5 chunks: c1046, c1047, c1048...) | check.py OK on .brevity/all group 184 |
| RTB-T193 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 185/237 (5 chunks: c1051, c1052, c1053...) | check.py OK on .brevity/all group 185 |
| RTB-T194 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 186/237 (7 chunks: c1056, c1057, c1058...) | check.py OK on .brevity/all group 186 |
| RTB-T195 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 187/237 (3 chunks: c1063, c1064, c1065) | check.py OK on .brevity/all group 187 |
| RTB-T196 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 188/237 (3 chunks: c1066, c1067, c1068) | check.py OK on .brevity/all group 188 |
| RTB-T197 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 189/237 (5 chunks: c1069, c1070, c1071...) | check.py OK on .brevity/all group 189 |
| RTB-T198 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 190/237 (7 chunks: c1074, c1075, c1076...) | check.py OK on .brevity/all group 190 |
| RTB-T199 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 191/237 (3 chunks: c1081, c1082, c1083) | check.py OK on .brevity/all group 191 |
| RTB-T200 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 192/237 (4 chunks: c1084, c1085, c1086...) | check.py OK on .brevity/all group 192 |
| RTB-T201 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 193/237 (1 chunks: c1088) | check.py OK on .brevity/all group 193 |
| RTB-T202 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 194/237 (1 chunks: c1089) | check.py OK on .brevity/all group 194 |
| RTB-T203 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 195/237 (4 chunks: c1090, c1091, c1092...) | check.py OK on .brevity/all group 195 |
| RTB-T204 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 196/237 (6 chunks: c1094, c1095, c1096...) | check.py OK on .brevity/all group 196 |
| RTB-T205 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 197/237 (3 chunks: c1100, c1101, c1102) | check.py OK on .brevity/all group 197 |
| RTB-T206 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 198/237 (4 chunks: c1103, c1104, c1105...) | check.py OK on .brevity/all group 198 |
| RTB-T207 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 199/237 (5 chunks: c1107, c1108, c1109...) | check.py OK on .brevity/all group 199 |
| RTB-T208 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 200/237 (5 chunks: c1112, c1113, c1114...) | check.py OK on .brevity/all group 200 |
| RTB-T209 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 201/237 (6 chunks: c1117, c1118, c1119...) | check.py OK on .brevity/all group 201 |
| RTB-T210 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 202/237 (2 chunks: c1123, c1124) | check.py OK on .brevity/all group 202 |
| RTB-T211 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 203/237 (3 chunks: c1125, c1126, c1127) | check.py OK on .brevity/all group 203 |
| RTB-T212 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 204/237 (6 chunks: c1128, c1129, c1130...) | check.py OK on .brevity/all group 204 |
| RTB-T213 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 205/237 (6 chunks: c1134, c1135, c1136...) | check.py OK on .brevity/all group 205 |
| RTB-T214 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 206/237 (6 chunks: c1140, c1141, c1142...) | check.py OK on .brevity/all group 206 |
| RTB-T215 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 207/237 (6 chunks: c1146, c1147, c1148...) | check.py OK on .brevity/all group 207 |
| RTB-T216 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 208/237 (3 chunks: c1152, c1153, c1154) | check.py OK on .brevity/all group 208 |
| RTB-T217 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 209/237 (1 chunks: c1155) | check.py OK on .brevity/all group 209 |
| RTB-T218 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 210/237 (2 chunks: c1156, c1157) | check.py OK on .brevity/all group 210 |
| RTB-T219 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 211/237 (13 chunks: c1158, c1159, c1160...) | check.py OK on .brevity/all group 211 |
| RTB-T220 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 212/237 (7 chunks: c1171, c1172, c1173...) | check.py OK on .brevity/all group 212 |
| RTB-T221 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 213/237 (7 chunks: c1178, c1179, c1180...) | check.py OK on .brevity/all group 213 |
| RTB-T222 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 214/237 (6 chunks: c1185, c1186, c1187...) | check.py OK on .brevity/all group 214 |
| RTB-T223 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 215/237 (3 chunks: c1191, c1192, c1193) | check.py OK on .brevity/all group 215 |
| RTB-T224 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 216/237 (10 chunks: c1194, c1195, c1196...) | check.py OK on .brevity/all group 216 |
| RTB-T225 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 217/237 (5 chunks: c1204, c1205, c1206...) | check.py OK on .brevity/all group 217 |
| RTB-T226 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 218/237 (6 chunks: c1209, c1210, c1211...) | check.py OK on .brevity/all group 218 |
| RTB-T227 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 219/237 (7 chunks: c1215, c1216, c1217...) | check.py OK on .brevity/all group 219 |
| RTB-T228 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 220/237 (5 chunks: c1222, c1223, c1224...) | check.py OK on .brevity/all group 220 |
| RTB-T229 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 221/237 (5 chunks: c1227, c1228, c1229...) | check.py OK on .brevity/all group 221 |
| RTB-T230 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 222/237 (7 chunks: c1232, c1233, c1234...) | check.py OK on .brevity/all group 222 |
| RTB-T231 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 223/237 (1 chunks: c1239) | check.py OK on .brevity/all group 223 |
| RTB-T232 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 224/237 (1 chunks: c1240) | check.py OK on .brevity/all group 224 |
| RTB-T233 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 225/237 (1 chunks: c1241) | check.py OK on .brevity/all group 225 |
| RTB-T234 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 226/237 (1 chunks: c1242) | check.py OK on .brevity/all group 226 |
| RTB-T235 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 227/237 (1 chunks: c1243) | check.py OK on .brevity/all group 227 |
| RTB-T236 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 228/237 (1 chunks: c1244) | check.py OK on .brevity/all group 228 |
| RTB-T237 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 229/237 (5 chunks: c1245, c1246, c1247...) | check.py OK on .brevity/all group 229 |
| RTB-T238 | 2026-10-25 | feature | closed | Package the overlay mod for Nexus Mods / Vortex: zip the build output (Info.json, RogueTraderBrevity.dll, simplified.json) as a single top-level RogueTraderBrevity/ folder (matching the UnityModManager layout ChemBoy1's Rogue Trader Vortex extension and UMM both expect), version the filename from Info.json's Version, add an MSBuild/PowerShell packaging step producing dist/RogueTraderBrevity-<version>.zip from the already-installed ModDir, and write the Nexus page requirements (Unity Mod Manager hard dependency, note Steam achievements are disabled while modded, mention MicroPatches for the in-game mod manager) | https://www.nexusmods.com/warhammer40kroguetrader/mods/651 |
| RTB-T239 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 230/237 (5 chunks: c1250, c1251, c1252...) | check.py OK on .brevity/all group 230 |
| RTB-T240 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 231/237 (8 chunks: c1255, c1256, c1257...) | check.py OK on .brevity/all group 231 |
| RTB-T241 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 232/237 (13 chunks: c1263, c1264, c1265...) | check.py OK on .brevity/all group 232 |
| RTB-T242 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 233/237 (35 chunks: c1276, c1277, c1278...) | check.py OK on .brevity/all group 233 |
| RTB-T243 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 234/237 (38 chunks: c1311, c1312, c1313...) | check.py OK on .brevity/all group 234 |
| RTB-T244 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 235/237 (10 chunks: c1349, c1350, c1351...) | check.py OK on .brevity/all group 235 |
| RTB-T245 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 236/237 (17 chunks: c1359, c1360, c1361...) | check.py OK on .brevity/all group 236 |
| RTB-T246 | 2026-10-25 | chore | closed | RTB v3 dialogue rewrite: .brevity/all group 237/237 (17 chunks: c1376, c1377, c1378...) | check.py OK on .brevity/all group 237 |
| RTB-T247 | 2026-10-25 | fix | closed | Restore lines the 30% cap cut too far: Calcazar's and Argenta's prayers and Ulfar's poems in full, Calligos's suspicion of the xenos, two Afterlight plot points; released as 1.0.1 | .brevity/review.txt; dist/RogueTraderBrevity-1.0.1.zip |
| RTB-T248 | 2026-10-25 | fix | closed | Replace the NSFW dev-name placeholder on the Nexus before/after images' Rogue Trader portrait plate with 'Rogue Trader', matching one clean border across all 15 screenshots | assets/*_WarGovernor_RM_dialog_*.png |
| RTB-T249 | 2026-10-25 | docs | closed | Re-render docs/USER_STORIES.md from the ticket database to pick up already-closed tickets (T4, T7, T238, T247) and the repo rename to RogueTrader.Brevity | MindAttic.Tickets/data/Tickets.db |
| RTB-T250 | 2026-10-26 | fix | open | Fix stilted over-shortened dialogue: restore natural grammar and word choice (e.g. Medineh's broken English, incinerate->burn), list every change | — |
| RTB-T251 | 2026-10-26 | docs | closed | Credit Nexus user BoethiahQueen in the attributions for reporting the stilted-dialogue problem | — |
| RTB-T252 | 2026-10-26 | chore | closed | Build on RogueTrader.Shared: game paths, references, packaging, LocalizationOverlay and BlueprintArchive from the shared source | 5651d66 |
