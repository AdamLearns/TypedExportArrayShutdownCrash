# TypedExportArrayShutdownCrash

## Overview

This project is the repro for [this Godot issue](https://github.com/godotengine/godot/issues/119984).

This repro was largely made with AI (although I'm personally typing this README and the issue that I filed); there were just too many variables in my own investigation for me to narrow down what was causing the issue.

Some other notes:

- Despite that `addons/YarnSpinner-Godot` exists in this repo, it isn't actually the real addon; it's a recreation of any potentially significant causes.
- I only tested this on macOS.
- The crash rate is 38%.
  - 30 of 100 runs crashed due to `handle_crash: Program crashed with signal 11`
  - 8 of 100 runs crashed with `libc++abi: terminating due to uncaught exception of type std::__1::system_error: mutex lock failed: Invalid argument`
- I mentioned having a controller connected in the original issue, but for this repro, I didn't have it connected.

## Instructions

Since this is almost certainly timing-related, I think the most important variable to change is `double quitDelay`.

I ran several hundred times and noticed that the crashes most frequently occurred for me in this range. In fact, from the logs, here are the actual times that resulted in a SIGSEGV:

```
quitDelay: 1.4868990778760565
quitDelay: 1.4869652751285
quitDelay: 1.4870048327491303
quitDelay: 1.4878840186428908
quitDelay: 1.5039623057874791
quitDelay: 1.5069221775074755
quitDelay: 1.5072125376637113
quitDelay: 1.5103204772214358
quitDelay: 1.5105583970131797
quitDelay: 1.510658273075608
quitDelay: 1.5113069414063487
quitDelay: 1.5140373409576224
quitDelay: 1.5142333570864677
quitDelay: 1.5147974521230676
quitDelay: 1.5170795480547523
quitDelay: 1.5229245004311966
quitDelay: 1.523970040859538
quitDelay: 1.5282653225279472
quitDelay: 1.5295813209816724
quitDelay: 1.5305074143912136
quitDelay: 1.5366538794772853
quitDelay: 1.537196675736902
quitDelay: 1.5379715814787498
quitDelay: 1.5380244502366505
quitDelay: 1.5395911016828172
quitDelay: 1.540082195746863
quitDelay: 1.5441533869076907
quitDelay: 1.544238611730464
quitDelay: 1.5448659980924997
quitDelay: 1.5476992543685815
```

If `quitDelay` is even relevant, then it'll likely need to be tweaked for your machine.

I ran via `zsh` with:

- `touch log.txt`
- `dotnet build && for i in {1..100}; do /Applications/Godot_mono.app/Contents/MacOS/Godot --path ./ >> ./log.txt 2>&1 ; done`
