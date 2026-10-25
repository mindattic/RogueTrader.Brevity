# Nexus Mods page: Rogue Trader Brevity

Copy each field into the Nexus Mods upload form. The description is BBCode, which is what Nexus's editor
accepts in source mode.

## Mod name

Rogue Trader Brevity - Shorter Dialogue

## Version

0.1.0

## Category

User Interface (alternative: Miscellaneous)

## Summary (max 350 characters)

Cuts the reading in Rogue Trader's dialogue by about two-thirds. Every conversation keeps its story, choices and
skill checks, but NPC speeches, narration and answer options are rewritten shorter: 1.28 million words down to
425,000. Toggle it live in-game with Ctrl+F10 to compare. Save-safe.

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

## Description (BBCode)

```
[size=5][b]Rogue Trader Brevity[/b][/size]
[i]All of the story, about a third of the reading.[/i]

Rogue Trader has some of the longest dialogue in any CRPG. Characters often take a full paragraph to say something that fits in a sentence. This mod rewrites the game's dialogue to be shorter while keeping what happens, what each character wants and every choice you make.

[size=4][b]By the numbers[/b][/size]
[list]
[*][b]39,650[/b] lines rewritten, covering the base game and DLC
[*]NPC lines and narration: [b]71% fewer words[/b]
[*]Your answer options: [b]42% fewer words[/b] (kept closer to the original so you always know what you're choosing)
[*]All dialogue: [b]1,279,720 words down to 424,970 (67% less)[/b], about 85 hours of reading down to roughly 28
[/list]

[size=4][b]Examples[/b][/size]
The same line in game, original above and Brevity below.

[b]Governor Medineh objects: 85 words down to 11[/b]
[img]IMAGE_1_URL[/img]

[b]The Rykadi Philia revolt: 72 words down to 15[/b]
[img]IMAGE_2_URL[/img]

[size=4][b]What it changes[/b][/size]
[list]
[*]NPC and companion dialogue, narration and stage directions in conversations
[*]Your answer options, with their tags ([Lie], [Kill the old man], skill checks and so on) left exactly as they were
[*]Colony and exploration event text
[/list]

[size=4][b]What it doesn't change[/b][/size]
[list]
[*][b]Voice acting.[/b] Voiced lines still play the full original audio, so the text may finish before the voice does. Most of the game's dialogue is unvoiced.
[*][b]Choices, outcomes, skill checks and alignment.[/b] Only the displayed text changes. Every option, requirement and consequence is untouched.
[*]Codex, item and ability descriptions, tutorials and menus
[*]Languages other than English
[/list]

[size=4][b]Toggle it live[/b][/size]
Press [b]Ctrl+F10[/b] to open Unity Mod Manager and switch the mod on or off. The text changes from the next line onward, even in the middle of a conversation, so you can compare the two versions or read a scene you care about in full.

[size=4][b]Installation[/b][/size]
[b]Vortex:[/b] click "Mod Manager Download" and enable the mod.

[b]Manual:[/b]
[list=1]
[*]Download the archive.
[*]Extract the [b]RogueTraderBrevity[/b] folder into:
[code]%USERPROFILE%\AppData\LocalLow\Owlcat Games\Warhammer 40000 Rogue Trader\UnityModManager\[/code]
[*]Start the game. Press Ctrl+F10 to check that "Rogue Trader Brevity" shows as active.
[/list]
The game language must be set to English.

[size=4][b]Uninstalling[/b][/size]
Delete the [b]RogueTraderBrevity[/b] folder, or switch the mod off in Ctrl+F10. The mod never writes to your saves or the game files. It swaps the text in memory while the game runs, so it's safe to add or remove at any point in a playthrough.

[size=4][b]Compatibility[/b][/size]
[list]
[*][b]Saves:[/b] fully safe to add or remove mid-playthrough.
[*][b]Other mods:[/b] compatible with mods that don't change dialogue text, including ToyBox and MicroPatches. Mods that replace English dialogue text will conflict on the lines both mods change, and whichever applies last wins.
[*][b]Game updates:[/b] new or rewritten lines from patches keep their original text until the mod is updated. Nothing breaks.
[/list]

[size=4][b]Known issues[/b][/size]
[list]
[*]Some long passages lose detail. The prayers, Ulfar's poems and some lore monologues read noticeably thinner. Switch the mod off with Ctrl+F10 for those scenes if you want the full version.
[*]Lines with dynamic text (your name, gendered words, glossary links) keep that markup. If you see raw text such as {g| or {mf| in a line, please report it with a screenshot.
[/list]

[size=4][b]How it was made[/b][/size]
Every dialogue line was exported from the game with its context: speaker, the line before it and the answer that led to it. An AI model (Claude) then rewrote each conversation as a whole, aiming for NPC lines at 20-40% of their original length and answer options at about half. Each rewrite was checked automatically so that every glossary link, name tag and gendered word survived and no line ran over its length limit. A runtime mod loads the result.

[size=4][b]Feedback[/b][/size]
If a line reads wrong, no longer makes sense, or cut something important, post it in the Posts tab with the character's name and the text. Specific lines can be rewritten and fixed in later versions.

[size=4][b]Credits[/b][/size]
Made by MindAttic. Warhammer 40,000: Rogue Trader by Owlcat Games. Warhammer 40,000 is a trademark of Games Workshop. This is an unofficial fan mod.
```
