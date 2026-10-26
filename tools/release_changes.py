"""Write docs/dialogue-polish-changes-v<major>.md for every release in tools/releases.json.

Each release is marked by how many lines .brevity/cache.jsonl had when it shipped; the cache is
append-only, so a release's text is the last cache entry per line up to its mark. A release's list
holds the lines whose shown text differs from the release before it (v1: from the game's original).
Existing files are kept unless --force, so a hand-annotated list (v3's reasons) is never replaced.

Releasing: append {"version": "<x.0.0>", "cacheLines": <current line count>, "summary": "..."}
to tools/releases.json, then run `python -I tools/release_changes.py`.
"""
import hashlib, json, os, sys

root = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
releases = json.load(open(os.path.join(root, "tools", "releases.json"), encoding="utf-8"))
lines = [json.loads(l) for l in open(os.path.join(root, "dialogue.jsonl"), encoding="utf-8")]
cache = [json.loads(l) for l in open(os.path.join(root, ".brevity", "cache.jsonl"), encoding="utf-8")]
force = "--force" in sys.argv


def digest(text):
    return hashlib.sha256(f"v3\n{text}".encode()).hexdigest().upper()[:32]


def shown(mark):
    last = {}
    for e in cache[:mark]:
        last[e["Hash"]] = e
    return {x["key"]: last[h]["Text"] for x in lines
            if (h := digest(x["text"])) in last and last[h]["Status"] == "ok"}


prev = {}
for rel in releases:
    cur = shown(rel["cacheLines"])
    major = rel["version"].split(".")[0]
    path = os.path.join(root, "docs", f"dialogue-polish-changes-v{major}.md")
    if os.path.exists(path) and not force:
        print(f"{rel['version']}: kept {os.path.relpath(path, root)}")
        prev = cur
        continue
    changed = [x for x in lines if cur.get(x["key"], x["text"]) != prev.get(x["key"], x["text"])]
    md = [f"# Dialogue changes in {rel['version']}", "", rel["summary"], "",
          f"**{len(changed):,} lines changed.** Each entry shows the text before this release "
          f"({'the original game text' if not prev else 'as the previous release showed it'}) and after, "
          "grouped by conversation.", ""]
    conv = None
    for x in sorted(changed, key=lambda x: (x["dialog"] or os.path.dirname(x["blueprint"]))):
        c = x["dialog"] or os.path.dirname(x["blueprint"])
        if c != conv:
            conv = c
            md += ["", f"## {c}", ""]
        who = "Player" if x["kind"] == "answer" else x["speaker"] or "(conversation owner)"
        md += [f"- **{who}**", f"  - Before: {prev.get(x['key'], x['text'])}", f"  - After: {cur.get(x['key'], x['text'])}"]
    open(path, "w", encoding="utf-8").write("\n".join(md) + "\n")
    print(f"{rel['version']}: {len(changed):,} lines -> {os.path.relpath(path, root)}")
    prev = cur
