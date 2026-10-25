"""Pair capture_dialogue.py's mod-on and mod-off screenshots of the same line into comparison images.

Usage: python -I tools/pair_screenshots.py [capture folder]   (default R:\\MindAttic\\Capture\\RogueTraderBrevity)

Reads examples.md, takes the first mod-off and first mod-on screenshot of each line, and writes
pairs/NN_<dialog>_<cue>.png: the dialogue panel with the original text above the shortened one.
pairs/pairs.md lists them, most words saved first.
"""
import re, sys
from pathlib import Path
from PIL import Image, ImageDraw, ImageFont

ROOT = Path(sys.argv[1] if len(sys.argv) > 1 else r"R:\MindAttic\Capture\RogueTraderBrevity")
# The dialogue panel's share of the window height (title bar included): the scene above it is cropped away.
PANEL_TOP, PANEL_BOTTOM = 0.635, 0.985
LABEL_HEIGHT = 44

SECTION = re.compile(r"^## (?P<dialog>\S+) / (?P<cue>\S+) \(mod (?P<state>on|off)\)$")
IMAGE = re.compile(r"^!\[[^\]]*\]\(<(?P<path>[^>]+)>\)$")
WORDS = re.compile(r"^  - (?P<which>Original|Brevity) \((?P<n>\d+) words\): (?P<text>.*)$")


def read_examples():
    """(dialog, cue) -> {"on": png, "off": png, "orig": (words, text), "short": (words, text), "who": name}."""
    lines, current = {}, None
    for line in (ROOT / "examples.md").read_text(encoding="utf-8").splitlines():
        if m := SECTION.match(line):
            current = lines.setdefault((m["dialog"], m["cue"]), {})
            state = m["state"]
        elif current is None:
            continue
        elif m := IMAGE.match(line):
            current.setdefault(state + "_all", []).append(m["path"])
        elif line.startswith("- **") and "who" not in current:
            current["who"] = line[4:line.index("**", 4)]
        elif (m := WORDS.match(line)) and m["which"] == "Original":
            current.setdefault("orig", (int(m["n"]), m["text"]))
        elif m := WORDS.match(line):
            current.setdefault("short", (int(m["n"]), m["text"]))
    # A shot taken while the panel switches lines shows an empty page; use the first one with text.
    for v in lines.values():
        for state in ("on", "off"):
            drawn = [p for p in v.get(state + "_all", []) if has_text(p)]
            if drawn:
                v[state] = drawn[0]
    return lines


def has_text(path):
    """True when the parchment's first text lines hold dark (text) pixels."""
    img = Image.open(path).convert("L")
    box = img.crop((int(img.width * 0.13), int(img.height * 0.72), int(img.width * 0.51), int(img.height * 0.80)))
    dark = sum(count for value, count in enumerate(box.histogram()) if value < 90)
    return dark / (box.width * box.height) >= 0.01


def panel(path):
    img = Image.open(path).convert("RGB")
    return img.crop((0, int(img.height * PANEL_TOP), img.width, int(img.height * PANEL_BOTTOM)))


def labelled(img, text, font):
    out = Image.new("RGB", (img.width, img.height + LABEL_HEIGHT), (20, 20, 20))
    ImageDraw.Draw(out).text((16, 8), text, fill=(235, 215, 160), font=font)
    out.paste(img, (0, LABEL_HEIGHT))
    return out


def main():
    try:
        font = ImageFont.truetype("arialbd.ttf", 26)
    except OSError:
        font = ImageFont.load_default()
    pairs = [(k, v) for k, v in read_examples().items() if "on" in v and "off" in v]
    # Most words saved first, so the strongest examples lead.
    pairs.sort(key=lambda kv: -(kv[1].get("orig", (0,))[0] - kv[1].get("short", (0,))[0]))
    out = ROOT / "pairs"
    out.mkdir(exist_ok=True)
    md = ["# Rogue Trader Brevity: before and after\n"]
    for i, ((dialog, cue), v) in enumerate(pairs, 1):
        orig, short = v.get("orig", (0, "")), v.get("short", (0, ""))
        top = labelled(panel(v["off"]), f"Original ({orig[0]} words)", font)
        bottom = labelled(panel(v["on"]), f"Rogue Trader Brevity ({short[0]} words)", font)
        both = Image.new("RGB", (top.width, top.height + bottom.height))
        both.paste(top, (0, 0))
        both.paste(bottom, (0, top.height))
        name = f"{i:02d}_{dialog}_{cue}.png"
        both.save(out / name)
        md.append(f"## {i}. {v.get('who', '?')}: {orig[0]} -> {short[0]} words ({cue})\n\n"
                  f"![{cue}](<{name}>)\n\n- Original: {orig[1]}\n- Brevity: {short[1]}\n")
    (out / "pairs.md").write_text("\n".join(md), encoding="utf-8")
    print(f"{len(pairs)} pairs -> {out}")


if __name__ == "__main__":
    main()
