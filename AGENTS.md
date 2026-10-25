# MindAttic project agent entrypoint

Read the shared protocol at ..\mindattic-agent-standard\AGENTS.md and this project's own
documentation. Common prompt commands are implemented by the shared runner; do not add a second
copy under a provider-specific command folder.

## Project context

RogueTrader.Brevity shortens Warhammer 40,000: Rogue Trader's dialogue: export the dialogue lines,
simplify them with an LLM, and load the shorter text back through a runtime overlay mod.

- Game install: `D:\Games\Steam\steamapps\common\Warhammer 40,000 Rogue Trader`
- English strings: `WH40KRT_Data\StreamingAssets\Localization\enGB.json` (`strings` → GUID →
  `{Offset, Text}`); `Sound.json` maps voiced string keys to voice events.
- Markup tokens such as `{g|...}...{/g}`, `{n}`, `{mf|...}`, `{name}` must survive simplification.
