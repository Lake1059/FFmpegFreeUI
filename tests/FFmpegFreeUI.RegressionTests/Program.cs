using System.Diagnostics;
using System.Reflection;
using System.Text.Json;
using FFmpegFreeUI;

internal static partial class Program
{
    private static int checks;
    private static readonly BindingFlags PrivateInstance = BindingFlags.Instance | BindingFlags.NonPublic;

    [STAThread]
    private static int Main(string[] args) => MainAsync(args).GetAwaiter().GetResult();

    private static async Task<int> MainAsync(string[] args)
    {
        if (args.FirstOrDefault() == "--agent-args-fixture")
        {
            Console.WriteLine(JsonSerializer.Serialize(args.Skip(1)));
            return 0;
        }
        if (args.Contains("--output-fixture"))
        {
            for (var i = 0; i < 200; i++)
            {
                Console.WriteLine($"stdout-{i}");
                Console.Error.WriteLine($"stderr-{i}");
            }
            Console.Error.WriteLine("stderr-tail");
            Console.WriteLine("123.456");
            return 0;
        }
        if (args.Contains("--wait-fixture"))
        {
            Console.WriteLine("ready");
            await Task.Delay(TimeSpan.FromSeconds(30));
            return 0;
        }
        if (args.Contains("--ui-preview")) { ShowThemePreview(); return 0; }
        if (args.Length > 0 && !(args.Length == 2 && args[0] == "--ffmpeg") && !args.SequenceEqual(new[] { "--agent-only" })) return 2;

        var directory = Path.Combine(Path.GetTempPath(), "FFmpegFreeUI-regression-" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(directory);
        try
        {
            设置_v6.实例对象 = new 设置_v6
            {
                自动开始任务选项 = 1,
                提示音选项 = 1,
                任务失败自动删除输出文件 = 2,
                用户统计_首次成功提示已显示 = true,
                替代进程文件名 = Environment.ProcessPath!,
                工作目录 = directory
            };
            TestAgentConversations(directory);
            TestAgentAudit(directory);
            TestAgentPresentation(directory);
            TestQueuePresetTools(directory);
            Console.WriteLine("PASS: concurrent Agent runs, stop isolation, per-conversation drafts and colors");
            if (args.Contains("--agent-only")) { Console.WriteLine($"PASS: {checks} checks"); return 0; }
            TestPresetCopies(directory);
            TestThemeAndVmaf();
            Console.WriteLine("PASS: light/dark theme, first-load colors, VMAF AUTO and model selection");
            Console.WriteLine("PASS: preset isolation and atomic persistence");
            TestQueueOperations(directory);
            Console.WriteLine("PASS: queue ordering, task lifecycle guards, and naming");
            TestProgress();
            Console.WriteLine("PASS: progress parsing and numeric boundaries");
            TestPendingCache();
            Console.WriteLine("PASS: pending queue persistence");
            TestQueueView();
            Console.WriteLine("PASS: queue multi-selection");
            SynchronizationContext.SetSynchronizationContext(null);
            await TestProcessLifecycle();
            Console.WriteLine("PASS: process output drainage and cancellation");
            await TestScheduler(directory);
            Console.WriteLine("PASS: scheduler error recovery and stop isolation");
            if (args.Length == 2)
            {
                await TestRealEncoding(directory, args[1]);
                Console.WriteLine("PASS: real FFmpeg encoding and FFprobe preset workflow");
            }
            BenchmarkPresetCopies();
            Console.WriteLine($"PASS: {checks} checks");
            return 0;
        }
        catch (Exception error)
        {
            Console.Error.WriteLine(error);
            return 1;
        }
        finally
        {
            编码队列_v6.停止所有进行中任务();
            await WaitUntil(() => 编码队列_v6.获取队列快照().All(task => !task.正在执行), "Test processes did not stop");
            编码队列_v6.移除任务(编码队列_v6.获取队列快照().Select(task => task.ID));
            Directory.Delete(directory, true);
        }
    }

    private static void Check(bool condition, string message)
    {
        if (!condition) throw new InvalidOperationException(message);
        checks++;
    }

    private static object? Invoke(object instance, string name, params object[] arguments) =>
        instance.GetType().GetMethod(name, PrivateInstance)!.Invoke(instance, arguments);

    private static 预设数据_v6 CreatePreset() => new()
    {
        输出容器 = "mkv",
        输出位置 = "original-output",
        计算机名称 = Environment.MachineName,
        运行时使用输出位置 = true,
        额外保存输出位置 = true,
        自定义参数_视频滤镜 = "hflip",
        滤镜排序系统 = [null!],
        元数据_要写入的信息 = [new() { 字段 = "title", 值 = "original" }],
        流控制_将视频参数应用于指定流 = ["0"]
    };

    private static void TestPresetCopies(string directory)
    {
        var original = CreatePreset();
        var clone = 编码队列_v6.克隆预设(original);
        Check(clone.运行时使用输出位置, "Runtime output location must survive cloning");
        Check(clone.滤镜排序系统.All(item => item is not null), "Legacy null filter entries must be normalized");
        clone.元数据_要写入的信息[0].值 = "changed";
        clone.流控制_将视频参数应用于指定流[0] = "1";
        clone.视频参数_烧录字幕_字幕格式优先级.Add(预设数据_v6.烧字幕格式.ASS);
        Check(original.元数据_要写入的信息[0].值 == "original", "Nested metadata must be independent");
        Check(original.流控制_将视频参数应用于指定流[0] == "0", "Stream arrays must be independent");
        Check(original.视频参数_烧录字幕_字幕格式优先级.Count == 0, "Subtitle lists must be independent");

        var path = Path.Combine(directory, "preset.json");
        预设管理_v6.写入预设文件(path, original, false);
        var saved = 预设管理_v6.读取预设文件(path);
        Check(saved.输出位置 == "" && saved.计算机名称 == "", "Portable preset must omit local output location");
        Check(original.输出位置 == "original-output" && original.运行时使用输出位置 && original.额外保存输出位置,
            "Saving must not alter runtime fields on the source");
        Check(original.自定义参数_视频滤镜 == "hflip" && original.滤镜排序系统[0] is null,
            "Saving must not migrate the caller's preset in place");
        var previous = File.ReadAllText(path);
        File.SetAttributes(path, FileAttributes.ReadOnly);
        try
        {
            try
            {
                预设管理_v6.写入预设文件(path, new 预设数据_v6 { 预设备注 = "replacement" });
                throw new InvalidOperationException("Writing a read-only preset unexpectedly succeeded");
            }
            catch (UnauthorizedAccessException) { }
            catch (IOException) { }
            Check(File.ReadAllText(path) == previous, "Failed replacement must preserve the previous preset");
            Check(!Directory.EnumerateFiles(directory, "*.tmp").Any(), "Failed replacement must remove its temporary file");
        }
        finally
        {
            File.SetAttributes(path, FileAttributes.Normal);
        }
    }

    private static void TestQueueOperations(string directory)
    {
        var tasks = 编码队列_v6.批量添加预设任务(["a.mp4", "b.mp4", "c.mp4"], CreatePreset());
        var ids = tasks.Select(task => task.ID).ToArray();
        Check(!编码队列_v6.重新排序([ids[0], ids[0], ids[2]]), "Duplicate IDs must be rejected");
        Check(编码队列_v6.获取队列快照().Select(task => task.ID).SequenceEqual(ids), "Rejected reorder must leave queue intact");
        Check(编码队列_v6.重新排序(ids.Reverse()), "Valid reorder must succeed");
        Check(ReferenceEquals(编码队列_v6.根据ID获取任务(ids[1]), tasks[1]), "ID lookup must survive reorder");
        tasks[0].预设数据.元数据_要写入的信息[0].值 = "changed";
        Check(tasks[1].预设数据.元数据_要写入的信息[0].值 == "original", "Batch tasks must own independent presets");
        var snapshot = 编码队列_v6.获取队列快照();
        snapshot.Clear();
        Check(编码队列_v6.队列.Count == 3, "Clearing a snapshot must not alter the queue");

        var task = tasks[0];
        Invoke(task, "开始执行");
        task.停止();
        Check(task.状态 == 编码任务状态_v6.已停止 && task.正在执行, "Stopped task must remain active during cleanup");
        Check(!task.可重置 && !task.可移除 && !task.可排序, "Cleanup must block reset, remove, and reorder");
        task.重置();
        编码队列_v6.移除任务([task.ID]);
        Check(task.状态 == 编码任务状态_v6.已停止 && 编码队列_v6.根据ID获取任务(task.ID) is not null,
            "Reset and removal must not race cleanup");
        Invoke(task, "结束执行");
        task.重置();
        Check(task.状态 == 编码任务状态_v6.未处理 && !task.手动停止, "Reset must work after cleanup completes");

        var preset = new 预设数据_v6 { 输出容器 = "mkv", 输出_自动命名选项 = 预设数据_v6.自动命名选项.附加_递增时间戳 };
        var input = Path.Combine(directory, "input.mp4");
        var reserved = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        for (var i = 0; i < 5; i++)
        {
            var output = 编码队列_v6.计算输出位置_v6(input, preset, false, reserved);
            Check(reserved.Add(output), "Concurrent timestamp names must be unique");
        }
        var synchronized = 编码队列_v6.同步指定未处理预设任务([ids[1]], preset);
        Check(synchronized.已更新 == 1, "Targeted preset sync must only affect the selected pending task");
        编码队列_v6.移除任务(ids);
        Check(编码队列_v6.队列.Count == 0 && 编码队列_v6.根据ID获取任务(ids[0]) is null, "Removal must update both queue and ID index");
    }

    private static void TestProgress()
    {
        Check(编码进度_v6.转换时间("3600") == TimeSpan.FromHours(1), "Numeric duration must mean seconds");
        Check(编码进度_v6.转换时间("25:01:02.123456") == TimeSpan.FromHours(25) + TimeSpan.FromMinutes(1) + TimeSpan.FromSeconds(2.123456),
            "FFmpeg clocks must support more than 24 hours and microseconds");
        Check(编码进度_v6.转换时间("NaN") == TimeSpan.Zero, "Non-finite duration must be rejected");
        Check(编码进度_v6.转换时间(TimeSpan.MaxValue.TotalSeconds.ToString(System.Globalization.CultureInfo.InvariantCulture)) == TimeSpan.Zero,
            "Rounded TimeSpan maximum must not overflow");
        var progress = new 编码进度_v6();
        progress.解析FFmpeg输出("frame= 1 size= 9223372036854775807 GiB time=00:00:01.00 speed=0.00x", TimeSpan.FromHours(1));
        Check(progress.输出大小KB == long.MaxValue && progress.时间文本 == "", "Extreme sizes and zero speeds must not overflow");
        progress.解析FFmpeg输出("size=9999999999999999999999999kB time=00:00:01.00 speed=1x", TimeSpan.FromHours(1));
        progress.解析FFmpeg输出("out_time_us=1800000000", TimeSpan.FromHours(1));
        progress.解析FFmpeg输出("total_size=1048576", TimeSpan.FromHours(1));
        progress.解析FFmpeg输出("speed=2.0x", TimeSpan.FromHours(1));
        Check(progress.百分比 == 0.5 && progress.输出大小KB == 1024 && progress.时间文本 == "15m0s",
            "Machine-readable progress must update time, size, and ETA");
    }

    private static void TestQueueView()
    {
        using var view = new Form_v6_编码队列();
        _ = view.Handle;
        Invoke(view, "Form_v6_编码队列_Load", view, EventArgs.Empty);
        var timer = (System.Windows.Forms.Timer)view.GetType().GetProperty("队列刷新计时器", PrivateInstance)!.GetValue(view)!;
        timer.Stop();
        var rows = (System.Collections.IDictionary)view.GetType().GetField("任务行", PrivateInstance)!.GetValue(view)!;
        string RowText(string id, int column)
        {
            var row = rows[id]!;
            var subItems = (System.Collections.IList)row.GetType().GetProperty("SubItems")!.GetValue(row)!;
            var cell = subItems[column]!;
            return (string)cell.GetType().GetProperty("Text")!.GetValue(cell)!;
        }
        var ids = new List<string>();
        try
        {
            var tasks = 编码队列_v6.批量添加预设任务(["one.mp4", "two.mp4", "three.mp4"], new 预设数据_v6());
            ids.AddRange(tasks.Select(task => task.ID));
            Application.DoEvents();
            Check(rows.Count == 3, "Adding tasks must update rows without the progress timer");
            Invoke(view, "全选任务");
            var selected = (List<string>)Invoke(view, "获取选中任务ID")!;
            Check(selected.Count == 3, "Select all must select every queue item");
            编码队列_v6.重新排序(tasks.Select(task => task.ID).Reverse());
            Application.DoEvents();
            selected = (List<string>)Invoke(view, "获取选中任务ID")!;
            Check(selected.Count == 3 && selected[0] == tasks[2].ID, "Immediate reorder must preserve multiple selection");

            Task.Run(() =>
            {
                tasks[0].进度.进度文本 = "50%";
                tasks[0].追加日志("background output");
            }).GetAwaiter().GetResult();
            Application.DoEvents();
            Check(RowText(tasks[0].ID, 2) == "", "Background progress must still wait for its refresh timer");
            Invoke(view, "队列刷新计时器_Tick", view, EventArgs.Empty);
            Check(RowText(tasks[0].ID, 2) == "50%", "Timer must apply pending progress updates");

            var task = 编码队列_v6.添加命令行任务("--wait-fixture", "UI lifecycle", "");
            ids.Add(task.ID);
            Application.DoEvents();
            编码队列_v6.开始任务([task.ID]);
            Application.DoEvents();
            Check(RowText(task.ID, 1) == "正在处理", "Start must update the row without the progress timer");
            Check(SpinWait.SpinUntil(() => task.当前进程ID > 0, TimeSpan.FromSeconds(5)), "UI fixture must start");
            编码队列_v6.暂停任务([task.ID]);
            Application.DoEvents();
            Check(RowText(task.ID, 1) == "已暂停", "Pause must update the row immediately");
            编码队列_v6.恢复任务([task.ID]);
            Application.DoEvents();
            Check(RowText(task.ID, 1) == "正在处理", "Resume must update the row immediately");
            编码队列_v6.停止任务([task.ID]);
            Application.DoEvents();
            Check(RowText(task.ID, 1) == "已停止", "Stop must update the row immediately");
            Check(SpinWait.SpinUntil(() => !task.正在执行, TimeSpan.FromSeconds(5)), "UI fixture must stop");
            编码队列_v6.重置任务([task.ID]);
            Application.DoEvents();
            Check(RowText(task.ID, 1) == "未处理", "Reset must update the row immediately");

            Task.Run(() => 编码队列_v6.移除任务(ids)).GetAwaiter().GetResult();
            Application.DoEvents();
            Check(rows.Count == 0, "Background removal must update rows without the progress timer");
        }
        finally
        {
            编码队列_v6.停止任务(ids);
            Invoke(view, "Form_v6_编码队列_FormClosed", view, new FormClosedEventArgs(CloseReason.None));
            编码队列_v6.移除任务(ids);
            Application.DoEvents();
        }
    }

    private static void TestPendingCache()
    {
        var path = 编码队列_v6.未处理任务缓存文件路径;
        Check(!File.Exists(path), "Regression cache path must start empty");
        try
        {
            var task = 编码队列_v6.添加命令行任务("--output-fixture", "cached", "");
            Check(编码队列_v6.保存未处理任务缓存() == 1, "Pending task must be saved");
            编码队列_v6.移除任务([task.ID]);
            Check(编码队列_v6.加载未处理任务缓存() == 1, "Pending task must be restored");
            var restored = 编码队列_v6.获取队列快照().Single();
            Check(restored.任务名称 == "cached" && !restored.允许自动启动 && restored.ID != task.ID,
                "Restored task must retain data, use a fresh ID, and wait for manual start");
            Check(ReferenceEquals(编码队列_v6.根据ID获取任务(restored.ID), restored), "Restored task must be indexed");
            编码队列_v6.移除任务([restored.ID]);
            File.WriteAllText(path, "{\"版本\":99,\"任务\":[]}");
            try
            {
                编码队列_v6.加载未处理任务缓存();
                throw new InvalidOperationException("Unknown cache version was accepted");
            }
            catch (InvalidDataException) { }
            Check(File.Exists(path), "Unsupported cache must be preserved");
        }
        finally
        {
            File.Delete(path);
        }
    }

    private static async Task TestProcessLifecycle()
    {
        for (var attempt = 0; attempt < 20; attempt++)
        {
            var task = new 编码任务_v6();
            var step = new 编码步骤_v6 { 命令行 = "--output-fixture", 显示名称 = "fixture" };
            Invoke(task, "开始执行");
            try
            {
                var result = await ((Task<int>)Invoke(task, "运行步骤Async", step)!).WaitAsync(TimeSpan.FromSeconds(10));
                Check(result == 0 && step.输出缓存.Contains("123.456") && step.输出缓存.Contains("stderr-tail"),
                    "Short process must drain the tail of both streams");
                Check(step.输出缓存.Count == 402, "Concurrent stdout/stderr must not lose entries");
                Check(task.当前进程ID == 0, "Step completion must release its process");
            }
            finally
            {
                Invoke(task, "结束执行");
            }
        }

        var stopped = new 编码任务_v6();
        Invoke(stopped, "开始执行");
        stopped.停止();
        var stoppedResult = await (Task<int>)Invoke(stopped, "运行步骤Async", new 编码步骤_v6 { 命令行 = "--output-fixture" })!;
        Check(stoppedResult == -1 && stopped.当前进程ID == 0, "Stop before launch must suppress the process");
        Invoke(stopped, "结束执行");

        var running = new 编码任务_v6();
        Invoke(running, "开始执行");
        var execution = (Task<int>)Invoke(running, "运行步骤Async", new 编码步骤_v6 { 命令行 = "--wait-fixture" })!;
        Check(running.当前进程ID > 0, "Fixture must be running before stop");
        running.暂停();
        Check(running.状态 == 编码任务状态_v6.已暂停, "Pause must suspend the live process");
        running.恢复();
        Check(running.状态 == 编码任务状态_v6.正在处理, "Resume must restore the live process");
        running.停止();
        await execution.WaitAsync(TimeSpan.FromSeconds(10));
        Invoke(running, "结束执行");
        Check(running.当前进程ID == 0 && running.可重置, "Stopping must release process and permit reset after cleanup");

        var probe = new 编码任务_v6();
        var probeStep = new 编码步骤_v6 { 阶段 = 预设数据_v6.命令行阶段.FFprobe获取时长 };
        Invoke(probe, "处理输出", probeStep, "999", false);
        Invoke(probe, "处理输出", probeStep, "123.456", true);
        Check(probeStep.输出缓存.SequenceEqual(["123.456"]), "FFprobe duration input must exclude stderr");
    }

    private static async Task WaitUntil(Func<bool> predicate, string message)
    {
        var timer = Stopwatch.StartNew();
        while (!predicate())
        {
            if (timer.Elapsed > TimeSpan.FromSeconds(10)) throw new TimeoutException(message);
            await Task.Delay(10);
        }
    }

    private static async Task TestScheduler(string directory)
    {
        var invalid = 编码队列_v6.添加预设任务("invalid\0input.mp4",
            new 预设数据_v6 { 输出容器 = "mkv" }, "invalid path");
        编码队列_v6.开始任务([invalid.ID]);
        await WaitUntil(() => invalid.状态 == 编码任务状态_v6.错误 && !invalid.正在执行, "Invalid output path stranded a task");
        Check(invalid.可重置, "Output preparation failure must release the execution slot");
        编码队列_v6.移除任务([invalid.ID]);

        var first = 编码队列_v6.添加命令行任务("--wait-fixture", "first", "");
        var second = 编码队列_v6.添加命令行任务("--output-fixture", "second", "");
        var pending = 编码队列_v6.添加命令行任务("--output-fixture", "pending", "");
        编码队列_v6.开始任务([first.ID]);
        await WaitUntil(() => first.当前进程ID > 0, "First task did not start");
        编码队列_v6.开始任务([second.ID]);
        编码队列_v6.停止任务([first.ID]);
        pending.允许自动启动 = true;
        await WaitUntil(() => !first.正在执行 && second.状态 == 编码任务状态_v6.已完成 && !second.正在执行,
            "Concurrent tasks did not finish");
        编码队列_v6.请求调度();
        await Task.Delay(100);
        Check(pending.状态 == 编码任务状态_v6.未处理, "Another task finishing must not undo a manual scheduling pause");
        编码队列_v6.应用自动开始任务设置(true);
        await WaitUntil(() => pending.状态 == 编码任务状态_v6.已完成 && !pending.正在执行, "Explicit resume did not run pending task");
        Check(pending.当前进程ID == 0, "Completed scheduled task must release its process");
        编码队列_v6.移除任务([first.ID, second.ID, pending.ID]);
        编码队列_v6.应用自动开始任务设置(false);
    }

    private static void BenchmarkPresetCopies()
    {
        var preset = CreatePreset();
        var jsonOptions = (JsonSerializerOptions)typeof(预设数据_v6).Assembly.GetType("FFmpegFreeUI.Module1")!.GetField("JsonSO")!.GetValue(null)!;
        var factory = (Func<预设数据_v6>)typeof(预设管理_v6).GetMethod("创建预设克隆工厂", BindingFlags.Static | BindingFlags.NonPublic)!.Invoke(null, [preset])!;
        预设数据_v6 LegacyCopy()
        {
            var json = JsonSerializer.Serialize(preset, jsonOptions);
            var copy = JsonSerializer.Deserialize<预设数据_v6>(json, jsonOptions)!;
            预设管理_v6.初始化空集合(copy);
            return copy;
        }
        for (var i = 0; i < 30; i++) { LegacyCopy(); factory(); }
        Measure("Legacy preset copy x1000", LegacyCopy);
        Measure("Batch preset copy x1000", factory);
    }

    private static async Task TestRealEncoding(string directory, string ffmpegPath)
    {
        设置_v6.实例对象.替代进程文件名 = Path.GetFullPath(ffmpegPath);
        var input = Path.Combine(directory, "generated.mp4");
        var generate = 编码队列_v6.添加命令行任务(
            $"-hide_banner -y -f lavfi -i testsrc2=size=64x64:rate=12 -t 1 -c:v mpeg4 \"{input}\"", "generate", input);
        编码队列_v6.开始任务([generate.ID]);
        await WaitUntil(() => !generate.正在执行, "Source generation did not finish");
        Check(generate.状态 == 编码任务状态_v6.已完成 && File.Exists(input), generate.获取日志文本());
        var output = Path.Combine(directory, "trimmed.mp4");
        var encode = 编码队列_v6.添加预设任务(input, new 预设数据_v6
        {
            输出容器 = "mp4",
            视频参数_编码器_类型 = 预设数据_v6.视频编码器类型.视频,
            视频参数_编码器_具体编码 = "mpeg4",
            剪辑区间_方法 = 预设数据_v6.剪辑方法.掐头去尾,
            剪辑区间_入点 = "0.1",
            剪辑区间_出点 = "0.8"
        }, "preset encode", output);
        编码队列_v6.开始任务([encode.ID]);
        await WaitUntil(() => !encode.正在执行, "Preset encoding did not finish");
        Check(encode.状态 == 编码任务状态_v6.已完成 && File.Exists(output), encode.获取日志文本());
        Check(encode.媒体总时长 == "1", "FFprobe duration must reach the rebuilt encoding step");
        Check(encode.进度.百分比 == 1 && encode.当前进程ID == 0, "Real encoding must finish at 100% and release the process");

        var probe = new 编码任务_v6();
        var step = new 编码步骤_v6
        {
            阶段 = 预设数据_v6.命令行阶段.FFprobe获取时长,
            命令行 = $"-v error -show_entries format=duration -of default=noprint_wrappers=1:nokey=1 \"{output}\""
        };
        var exitCode = await (Task<int>)Invoke(probe, "运行步骤Async", step)!;
        Check(exitCode == 0 && step.输出缓存.Count == 1, "Real FFprobe must retain its final duration line");
        var duration = double.Parse(step.输出缓存.Single(), System.Globalization.CultureInfo.InvariantCulture);
        Check(duration > 0 && duration < 1, "Preset output must be shorter than the original media");
        await TestRealVmaf(directory, ffmpegPath, input);
        编码队列_v6.移除任务([generate.ID, encode.ID]);
    }

    private static void Measure(string label, Func<预设数据_v6> copy)
    {
        var before = GC.GetAllocatedBytesForCurrentThread();
        var timer = Stopwatch.StartNew();
        for (var i = 0; i < 1000; i++) copy();
        timer.Stop();
        Console.WriteLine($"{label}: {timer.Elapsed.TotalMilliseconds:F1} ms, {(GC.GetAllocatedBytesForCurrentThread() - before) / 1024.0 / 1024.0:F2} MiB allocated");
    }
}
