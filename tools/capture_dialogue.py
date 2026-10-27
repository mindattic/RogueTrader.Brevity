"""Screenshot Rogue Trader every time a dialogue line appears, for before/after examples.

Usage: python -I tools/capture_dialogue.py   (start it before or after the game; Ctrl+C stops it)

Follows the game's GameLogFull.txt. Each time a cue finishes drawing, it takes a screenshot of the game window
with MindAttic.Capture (capture.exe shot) and adds an entry to examples.md next to the screenshots: the
picture, whether the mod was on, and the cue's and answers' original and shortened text from
dialogue.jsonl and the installed simplified.json.
"""
import datetime, json, os, re, subprocess, sys, time
from pathlib import Path

REPO = Path(__file__).resolve().parent.parent
GAME_DATA = Path(os.environ["USERPROFILE"]) / "AppData/LocalLow/Owlcat Games/Warhammer 40000 Rogue Trader"
LOG = GAME_DATA / "GameLogFull.txt"
SHORT = GAME_DATA / "UnityModManager/RogueTraderBrevity/simplified.json"
CAPTURE = Path(os.environ["LOCALAPPDATA"]) / "MindAttic/Capture/bin/capture.exe"
OUT = Path(r"R:\MindAttic\Capture\RogueTraderBrevity")
# Give the dialogue panel a moment to finish its fade-in before the shot.
SETTLE_SECONDS = 0.8

ENTRY = re.compile(r"^\[[^\]]+ - (?P<channel>[^\]]+)\]\[\w+\]: (?P<msg>.*)$")


def load_lines():
    """(dialog name, blueprint name) -> export row, for every line in dialogue.jsonl."""
    rows = {}
    with open(REPO / "dialogue.jsonl", encoding="utf-8") as f:
        for line in f:
            r = json.loads(line)
            dialog, bp = r.get("dialog") or "", r.get("blueprint") or ""
            rows.setdefault((dialog.rsplit("/", 1)[-1], bp.rsplit("/", 1)[-1]), r)
    return rows


def read_shared(path, offset):
    """(first 64 bytes, bytes from offset) of the file, opened only for this call and with every share
    flag set: a handle held open blocks the game from rotating its log at startup, which stops it logging."""
    import ctypes, msvcrt
    from ctypes import wintypes
    create = ctypes.windll.kernel32.CreateFileW
    create.restype = wintypes.HANDLE
    create.argtypes = [wintypes.LPCWSTR, wintypes.DWORD, wintypes.DWORD, wintypes.LPVOID,
                       wintypes.DWORD, wintypes.DWORD, wintypes.HANDLE]
    # GENERIC_READ; FILE_SHARE_READ | WRITE | DELETE; OPEN_EXISTING
    handle = create(str(path), 0x80000000, 0x7, None, 3, 0, None)
    if handle in (None, wintypes.HANDLE(-1).value):
        raise OSError(f"cannot open {path}")
    with open(msvcrt.open_osfhandle(handle, os.O_RDONLY | os.O_BINARY), "rb") as f:
        head = f.read(64)
        f.seek(offset)
        return head, f.read()


def follow(path):
    """Yield new lines of the log, starting at its current end; starts over when the game begins a new log."""
    head, offset, pending = None, None, b""
    while True:
        time.sleep(0.2)
        try:
            new_head, _ = read_shared(path, 1 << 62)
            # A log under 64 bytes is still growing its head; only a different start means a new log.
            same_log = head is not None and new_head[:len(head)] == head[:len(new_head)]
            if same_log:
                head = max(head, new_head, key=len)
            if not same_log or offset is None:
                # First look, or a new game session replaced the log: skip the old one, read a new one whole.
                first = offset is None
                head, pending = new_head, b""
                offset = len(read_shared(path, 0)[1]) if first else 0
                if first:
                    continue
            _, data = read_shared(path, offset)
        except OSError:
            continue
        if len(data) == 0:
            continue
        offset += len(data)
        *lines, pending = (pending + data).split(b"\n")
        for line in lines:
            yield line.decode("utf-8", "replace").rstrip("\r")


def screenshot(tag):
    """Shoot the game window; returns the PNG path capture.exe printed, or None."""
    done = subprocess.run([str(CAPTURE), "shot", "--process", "WH40KRT", "--app", "RogueTraderBrevity", "--tag", tag, "--format", "png"],
                          capture_output=True, text=True)
    lines = [l for l in done.stdout.splitlines() if l.strip().lower().endswith(".png")]
    if done.returncode != 0 or not lines:
        print(f"screenshot failed: {done.stdout.strip()} {done.stderr.strip()}", flush=True)
        return None
    return lines[-1].strip()


def describe(rows, short, dialog, name):
    r = rows.get((dialog, name))
    if r is None:
        return f"- **{name}**: not in the export\n"
    original, shortened = r["text"], short.get(r["key"])
    who = "Player" if r["kind"] == "answer" else r.get("speaker") or "?"
    if shortened is None:
        return f"- **{who}** ({name}, not shortened): {original}\n"
    return (f"- **{who}** ({name})\n"
            f"  - Original ({len(original.split())} words): {original}\n"
            f"  - Brevity ({len(shortened.split())} words): {shortened}\n")


def main():
    if not CAPTURE.exists():
        sys.exit(f"capture.exe not found at {CAPTURE}; run MindAttic.Capture's run.ps1")
    rows = load_lines()
    short = json.load(open(SHORT, encoding="utf-8"))
    OUT.mkdir(parents=True, exist_ok=True)
    md = OUT / "examples.md"
    print(f"watching {LOG}; screenshots and {md.name} go to {OUT}", flush=True)

    dialog, cue, answers, mod_on = "?", None, [], True
    for line in follow(LOG):
        m = ENTRY.match(line)
        if not m:
            continue
        msg = m["msg"]
        if msg.startswith("Requested dialog start "):
            dialog = msg.split()[-1]
        elif msg.startswith("[RogueTraderBrevity]") and "dialogue on" in msg:
            mod_on = "short dialogue" in msg
        elif msg.startswith("Dialog ") and msg.endswith(": played") and "Cue" in msg:
            cue, answers = msg[len("Dialog "):-len(": played")], []
        elif msg.startswith("Dialog ") and msg.endswith(": answer shown"):
            answers.append(msg[len("Dialog "):-len(": answer shown")])
        elif msg.endswith("CuePartUpdate Finished") and cue:
            time.sleep(SETTLE_SECONDS)
            state = "on" if mod_on else "off"
            png = screenshot(f"{dialog}_{cue}_mod-{state}")
            entry = [f"## {dialog} / {cue} (mod {state})\n\n"]
            if png:
                entry.append(f"![{cue}](<{png}>)\n\n")
            entry.append(describe(rows, short, dialog, cue))
            entry.extend(describe(rows, short, dialog, a) for a in answers)
            with open(md, "a", encoding="utf-8") as f:
                f.write("".join(entry) + "\n")
            print(f"{datetime.datetime.now():%H:%M:%S} {dialog} {cue} mod {state} -> {png}", flush=True)
            cue = None


if __name__ == "__main__":
    try:
        main()
    except KeyboardInterrupt:
        pass
