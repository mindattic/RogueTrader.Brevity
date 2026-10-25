# Nexus Mods page: Rogue Trader Brevity

Live at https://www.nexusmods.com/warhammer40kroguetrader/mods/651 (1.0.0 published 2026-10-25).

Copy each field into the Nexus Mods upload form. The description is BBCode, which is what Nexus's editor
accepts in source mode.

## Mod name

Rogue Trader Brevity

## Version

1.0.0

## Category

User Interface (alternative: Miscellaneous)

## Summary (max 350 characters)

Cuts the reading in Rogue Trader's dialogue by ~66%, including all three story DLCs. NPC speeches, narration and
answer options are rewritten shorter, 1.28 million words (~85 hours) down to 425 thousand (~28 hours), every story
beat, choice and skill check stays the same. Toggle it in-game with Ctrl+F10. Safe to add or remove mid-playthrough.

## Tags

Dialogue, Quality of Life, Text, Immersion, User Interface, Overhaul

## Requirements

- Warhammer 40,000: Rogue Trader (Steam), English language
- No other mods required. Unity Mod Manager is built into the game.
- DLC: not required. DLC dialogue is shortened too when you own it.

## AI disclosure

The shortened text was written with an AI model (Claude) and checked by script for length and game markup. Tick
Nexus's "contains AI-generated content" option when uploading.

## Images

Upload these from `docs/nexus/` in the Images tab, in this order. Make the first one the primary image. Each
shows the same in-game line twice: the original text above, the Brevity text below.

| File | Caption |
|---|---|
| `example-1-governor-objects.png` | Governor Medineh objects: 85 words down to 11. The answer options are shorter too. |
| `example-2-prison-revolt.png` | The Rykadi Philia revolt: 72 words down to 15. |

Once they're uploaded, right-click each one on the mod page, copy its image address, and paste it over
`IMAGE_1_URL` and `IMAGE_2_URL` in the description below.

To capture more pairs, run `python -I tools/capture_dialogue.py` while playing a conversation once with the
mod on and once with it off (Ctrl+F10), then `python -I tools/pair_screenshots.py`.

## Full description (BBCode)

Laid out in the five sections of Nexus's description template. Paste it with "Import description" in the
editor's top right corner, which converts BBCode.

```
[size=5][b]Description[/b][/size]
Rogue Trader has some of the longest dialogue in any CRPG. Characters often take a full paragraph to say something that fits in a sentence. Brevity rewrites the game's dialogue to be shorter while keeping what happens, what each character wants and every choice you make.

[list]
[*][b]39,650[/b] lines rewritten, covering the base game and all three story DLCs:
[list]
[*]Base game: 26,867 lines
[*][b]Void Shadows[/b]: 4,918 lines
[*][b]Lex Imperialis[/b]: 3,550 lines
[*][b]The Infinite Museion[/b]: 4,315 lines
[/list]
[*]NPC lines and narration: [b]71% fewer words[/b]
[*]Your answer options: [b]42% fewer words[/b], kept closer to the original so you always know what you're choosing
[*]All dialogue: [b]1.28 million words down to 425 thousand (66.8% less)[/b], about 85 hours of reading down to roughly 28
[/list]

The same line in game, original above and Brevity below:

[b]Governor Medineh objects: 85 words down to 11[/b]
[img]IMAGE_1_URL[/img]

[b]The Rykadi Philia revolt: 72 words down to 15[/b]
[img]IMAGE_2_URL[/img]

[size=5][b]Installation instructions[/b][/size]
[b]Vortex:[/b] click "Mod Manager Download" and enable the mod.

[b]Manual:[/b]
[list=1]
[*]Download the archive.
[*]Extract the [b]RogueTraderBrevity[/b] folder into:
[code]%USERPROFILE%\AppData\LocalLow\Owlcat Games\Warhammer 40000 Rogue Trader\UnityModManager\[/code]
[*]Start the game and press [b]Ctrl+F10[/b] to check that "Rogue Trader Brevity" shows as active.
[/list]

[b]Uninstalling:[/b] delete the [b]RogueTraderBrevity[/b] folder, or switch the mod off with Ctrl+F10. The mod never writes to your saves or the game files. It swaps the text in memory while the game runs, so it's safe to add or remove at any point in a playthrough.

[size=5][b]Main features[/b][/size]
[list]
[*][b]Shorter dialogue everywhere:[/b] NPC and companion lines, narration and stage directions, your answer options, and colony and exploration events.
[*][b]Nothing about the game changes but the text:[/b] every choice, skill check, alignment shift and consequence is untouched, and answer tags such as [Lie] or skill checks stay exactly as they were.
[*][b]Live toggle:[/b] press Ctrl+F10 to switch between short and full text, even mid-conversation. Use it to compare, or to read a scene you care about in full.
[*][b]Save-safe:[/b] add or remove it at any point in a playthrough.
[*][b]Game markup kept:[/b] your name, gendered words and glossary links work as normal.
[/list]

[b]What it doesn't change:[/b]
[list]
[*]Voice acting. Voiced lines still play the full original audio, so the text may finish before the voice does. Most of the game's dialogue is unvoiced.
[*]Codex, item and ability descriptions, tutorials and menus.
[*]Languages other than English.
[/list]

[b]Known issues:[/b]
[list]
[*]Some long passages lose detail. The prayers, Ulfar's poems and some lore monologues read noticeably thinner. Switch the mod off for those scenes if you want the full version.
[*]New or changed lines from future game patches keep their original text until the mod is updated. Nothing breaks.
[*]If you see raw markup such as {g| or {mf| in a line, or a line that no longer makes sense, post it in the Posts tab with the character's name and the text.
[/list]

[size=5][b]Requirements[/b][/size]
[list]
[*]Warhammer 40,000: Rogue Trader, with the game language set to English.
[*]Nothing else. Unity Mod Manager is built into the game, and no DLC is required. If you own Void Shadows, Lex Imperialis or The Infinite Museion, their dialogue is shortened too, with nothing extra to install.
[*]Compatible with mods that don't change dialogue text. A mod that also replaces English dialogue will conflict on the lines both change.
[/list]

[size=5][b]Shout outs[/b][/size]
[list]
[*][b]Owlcat Games[/b] for Rogue Trader and for building Unity Mod Manager support into it.
[*]The [b]Unity Mod Manager[/b] project, which loads the mod and provides the live toggle.
[*]The shortened text was written with an AI model (Claude) from each conversation's original text and context, and checked by script so every name, glossary link and gendered word survived.
[/list]

Made by MindAttic. Warhammer 40,000 is a trademark of Games Workshop. This is an unofficial fan mod.
```
