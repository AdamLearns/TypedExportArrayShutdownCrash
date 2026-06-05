# TypedExportArrayShutdownCrash

## Overview

This project is the repro for [this Godot issue](https://github.com/godotengine/godot/issues/119984).

This repro was largely made with AI (although I'm personally typing this README and the issue that I filed); there were just too many variables in my own investigation for me to narrow down what was causing the issue.

Some other notes:

- Despite that `addons/YarnSpinner-Godot` exists in this repo, it isn't actually the real addon; it's a recreation of any potentially significant causes.
- I only tested this on macOS.
- The crash rate is 63% (big caveat: see instructions about timing).
  - 49 of 100 runs crashed due to `handle_crash: Program crashed with signal 11`
  - 14 of 100 runs crashed with `libc++abi: terminating due to uncaught exception of type std::__1::system_error: mutex lock failed: Invalid argument`
- I mentioned having a controller connected in the original issue, but for this repro, I didn't have it connected.

## Instructions

Since this issue is almost certainly timing-related, the most important variable to change is `double quitDelay`. I have it tuned to a value that causes repros more frequently for _my_ machine. I suggest the following:

- Start with `double quitDelay = GD.RandRange(1.0, 3.0)`
- Run 100 times:
  - `touch log.txt`
  - `dotnet build && for i in {1..100}; do /Applications/Godot_mono.app/Contents/MacOS/Godot --path ./ >> ./log.txt 2>&1 ; done`
- Analyze the log for crashes and see which `quitDelay` values tended to be used
- Narrow the `GD.RandRange()` call based on those times, e.g. `double quitDelay = GD.RandRange(1.7, 1.8);`
