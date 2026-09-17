# Queue and Preset Regression Checks

The checks also cover light/dark theme restoration, secondary-window Load colors,
queue text, chart lines, model badges, AUTO selection boundaries, saved manual
choices, and exporting the actual VMAF model and warning. With `--ffmpeg`, the
suite additionally runs the application's generated AUTO VMAF command when the
supplied FFmpeg has discoverable models.

Build LakeUI first: the model selector uses the two custom-content rendering hooks
in `ModernComboBox`, and Agent running rows use `ModernListBox.ItemForeColorNeeded`.
An alternate DLL can be supplied to builds/tests with
`-p:LakeUIAssembly=<absolute-path-to-LakeUI.dll>`.

Agent checks use a local gated SSE fixture (no external API or credentials) to
exercise overlapping runs, cancellation isolation, interleaved responses and
tool results, guidance, running-row colors, and draft text/file/folder save/load.
Audit checks additionally cover actual executable and PowerShell timeouts,
precise argv, process isolation, truncated/error SSE, interrupted-session recovery,
safe directory moves, endpoint-specific capability caches, and parameter/queue validation.
Run only these checks with:

```powershell
dotnet run --project tests/FFmpegFreeUI.RegressionTests -- --agent-only
```

## VMAF AUTO contract

- New/empty selections default to AUTO; saved explicit selections remain manual.
- Resolution follows the reference video: longest side >= 3840 is 4K, including
  cropped widescreen, portrait and 8K inputs.
- Prefer same-resolution V1, with HFR first at >= 48 fps. Prefer 1.5H for 4K and
  3H otherwise; newest version breaks ties.
- Without a matching V1, use same-resolution V0 NEG. Missing both fails only VMAF
  with an actionable message; AUTO never silently crosses resolution families.
- Unknown dimensions fail AUTO; unknown frame rate uses normal models with a note.
  Above 60 fps, continue with a visible/exported calibration warning.
- AUTO uses CPU. Manual CUDA entries are limited to compatible V0 NEG models.
- Resolve once per run, storing the actual model and note with each result.

An isolated native preview (synthetic model list, no user settings) is available:

```powershell
dotnet run --project tests/FFmpegFreeUI.RegressionTests -- --ui-preview
```

## Running checks

Run on Windows with .NET 10 and the application's existing LakeUI reference available:

```powershell
dotnet run --project tests/FFmpegFreeUI.RegressionTests/FFmpegFreeUI.RegressionTests.csproj
```

Include real media generation, preset trimming, and FFprobe verification:

```powershell
dotnet run --project tests/FFmpegFreeUI.RegressionTests/FFmpegFreeUI.RegressionTests.csproj -- --ffmpeg C:/Tools/ffmpeg/bin/ffmpeg.exe
```

The executable serves as its own short-lived stdout/stderr fixture. It covers preset
copy isolation, failed file replacement, queue ordering and lookup, cache restoration,
task cancellation and reset, multi-selection, numeric progress boundaries, and scheduler
recovery. No test framework package is required. Test data lives in temporary folders
or the test build output; the application's working settings and presets are not used.

The final measurements compare 1,000 legacy JSON copies with the batch clone factory
after warmup. Timings are diagnostic, not pass/fail thresholds.

## Ownership

- `编码队列_v6.vb`: queue membership, immutable task IDs, lookup, scheduling and output reservations.
- `编码任务_v6.vb`: execution lifecycle, processes, stage data and logs.
- `编码进度_v6.vb`: FFmpeg output parsing and display formatting.
- `预设存储_v6.vb`: preset copying, migration and persistence.

Queue membership changes must go through queue methods. `队列` is a read-only snapshot;
`获取队列快照` returns a detached list. A terminal status does not imply cleanup has
finished: reset, removal and reorder also require `正在执行 = False`. Process output
streams finish before the next stage starts, and process startup shares the task state
lock with cancellation. Queue events may arrive on background threads. Queue membership
and task state changes request a coalesced update on the next UI turn; only progress
and log output wait for the configured refresh timer.
