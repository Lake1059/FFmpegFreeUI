using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Nodes;
using FFmpegFreeUI.PluginSdk;

namespace ThreeFui.PluginApi.Sample;

public sealed partial class SamplePlugin
{
    // preset.before-apply：可在原生控件接收预设前迁移插件状态或原生字段。
    private ValueTask PresetBeforeApply(PluginPipelineContext context, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var (preset, state) = ReadPresetAndState(context.PresetJson);
        if (state.Version < 2)
        {
            state.Version = 2;
            WriteState(preset, state);
            WritePreset(context, preset);
            Log(PluginLogLevel.Information, $"已在 {context.SurfaceId} 迁移示例状态到版本 2");
        }
        return ValueTask.CompletedTask;
    }

    // preset.after-apply：原生映射已经结束，适合观察和刷新插件显示，不适合再改 JSON 期待原生界面重映射。
    private ValueTask PresetAfterApply(PluginPipelineContext context, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        Log(PluginLogLevel.Trace, $"预设已应用：stage={context.StageId}, surface={context.SurfaceId}");
        return ValueTask.CompletedTask;
    }

    // preset.before-capture：随后原生控件会覆盖它们拥有的字段，因此这里只保存辅助状态。
    private ValueTask PresetBeforeCapture(PluginPipelineContext context, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var (preset, state) = ReadPresetAndState(context.PresetJson);
        state.LastSurfaceId = context.SurfaceId;
        WriteState(preset, state);
        WritePreset(context, preset);
        return ValueTask.CompletedTask;
    }

    // preset.after-capture：原生字段已经完整捕获，是覆盖最终预设质量参数的可靠位置。
    private ValueTask PresetAfterCapture(PluginPipelineContext context, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var (preset, state) = ReadPresetAndState(context.PresetJson);
        if (state.Enabled)
        {
            ApplyQuality(preset, state);
            WritePreset(context, preset);
        }
        return ValueTask.CompletedTask;
    }

    // queue.before-add：快速修改新任务快照和 Properties["taskName"]，不可在同步阶段执行耗时 I/O。
    private ValueTask QueueBeforeAdd(PluginPipelineContext context, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var state = TryReadState(context.PresetJson);
        if (state?.PrefixTaskName == true && context.Properties.TryGetValue("taskName", out var taskName))
        {
            var current = taskName ?? string.Empty;
            if (!current.StartsWith(PluginTag, StringComparison.Ordinal))
            {
                context.Properties["taskName"] = $"{PluginTag} {current}".TrimEnd();
            }
        }
        if (state is not null)
        {
            context.OutputPath = AddOutputSuffix(context.OutputPath, state.OutputSuffix);
        }
        return ValueTask.CompletedTask;
    }

    // task.before-prepare：可取消的媒体分析或网络查询应放在这里；修改预设后宿主会据此构建步骤。
    private async ValueTask TaskBeforePrepareAsync(
        PluginPipelineContext context,
        CancellationToken cancellationToken)
    {
        var state = TryReadState(context.PresetJson);
        if (!IsActive(state))
        {
            return;
        }

        var session = GetOrCreateSession(context);
        session.InputPath = context.InputPath;
        session.OutputPath = context.OutputPath;
        context.OutputPath = AddOutputSuffix(context.OutputPath, state!.OutputSuffix);
        session.OutputPath = context.OutputPath;
        if (!state.Enabled)
        {
            return;
        }

        context.ReportProgress("C# 示例正在执行可取消的任务分析……", 0.1);
        await Task.Delay(80, cancellationToken).ConfigureAwait(false);
        var (preset, enabledState) = ReadPresetAndState(context.PresetJson);
        ApplyQuality(preset, enabledState);
        WritePreset(context, preset);
        context.OutputPath = AddOutputSuffix(context.OutputPath, enabledState.OutputSuffix);
        session.OutputPath = context.OutputPath;
        context.ReportProgress($"示例质量值已确定为 CRF {enabledState.Crf}", 0.25);
    }

    // command.before-build：以结构化 JSON 改参数；会用于预览和每个真实步骤，必须幂等。
    private ValueTask CommandBeforeBuild(PluginPipelineContext context, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var state = TryReadState(context.PresetJson);
        if (state?.Enabled != true || string.IsNullOrWhiteSpace(state.AdvancedArguments))
        {
            return ValueTask.CompletedTask;
        }

        var (preset, _) = ReadPresetAndState(context.PresetJson);
        var current = preset["视频参数_质量控制_进阶参数集"]?.GetValue<string>() ?? string.Empty;
        preset["视频参数_质量控制_进阶参数集"] = AppendTokenOnce(current, state.AdvancedArguments);
        WritePreset(context, preset);
        Log(
            PluginLogLevel.Trace,
            $"结构化命令调整：phase={context.PhaseName}, preview={context.IsPreview}");
        return ValueTask.CompletedTask;
    }

    // command.after-build：只能修改最终参数字符串；这里用可重复调用的方式前置全局参数。
    private ValueTask CommandAfterBuild(PluginPipelineContext context, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var state = TryReadState(context.PresetJson);
        if (state?.AddNoStats == true)
        {
            context.CommandLine = PrependOptionOnce(context.CommandLine, "-nostats");
        }
        return ValueTask.CompletedTask;
    }

    // task.after-prepare：全部步骤首次生成后验证；修改任务数据会让宿主重建步骤。
    private async ValueTask TaskAfterPrepareAsync(
        PluginPipelineContext context,
        CancellationToken cancellationToken)
    {
        var state = TryReadState(context.PresetJson);
        if (state?.Enabled != true)
        {
            return;
        }

        await Task.Yield();
        cancellationToken.ThrowIfCancellationRequested();
        context.Properties.TryGetValue("stepCount", out var stepCount);
        context.ReportProgress(
            $"任务准备完成：{stepCount ?? "未知"} 个步骤，输出 {context.OutputPath}",
            0.3);
    }

    // process.before-start：最后一刻替换可执行文件或参数，并读取宿主提供的步骤属性。
    private ValueTask ProcessBeforeStartAsync(
        PluginPipelineContext context,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var state = TryReadState(context.PresetJson);
        if (!IsActive(state))
        {
            return ValueTask.CompletedTask;
        }

        if (!string.IsNullOrWhiteSpace(state!.ProcessOverride))
        {
            context.ProcessFileName = state.ProcessOverride;
        }
        if (state.AddNoStdin)
        {
            context.CommandLine = PrependOptionOnce(context.CommandLine, "-nostdin");
        }
        context.Properties.TryGetValue("stepNumber", out var stepNumber);
        context.Properties.TryGetValue("stepCount", out var stepCount);
        context.Properties.TryGetValue("commandStage", out var commandStage);
        context.ReportProgress(
            $"准备启动 {context.ProcessFileName}：步骤 {stepNumber ?? "?"}/{stepCount ?? "?"}，阶段 {commandStage ?? context.PhaseName}");
        return ValueTask.CompletedTask;
    }

    // process.after-exit：观察或按明确协议校正单步骤退出码；它不表示整个任务已经结束。
    private ValueTask ProcessAfterExitAsync(
        PluginPipelineContext context,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var state = TryReadState(context.PresetJson);
        if (!IsActive(state))
        {
            return ValueTask.CompletedTask;
        }

        if (context.ExitCode is int exitCode)
        {
            GetOrCreateSession(context).ExitCodes.Enqueue(exitCode);
            if (state!.AcceptExitCodeOne && exitCode == 1)
            {
                context.ExitCode = 0;
            }
        }
        return ValueTask.CompletedTask;
    }

    // task.after-complete：适合可取消的成功后处理；示例计算 SHA-256 并发布两个结构化结果。
    private async ValueTask TaskAfterCompleteAsync(
        PluginPipelineContext context,
        CancellationToken cancellationToken)
    {
        var state = TryReadState(context.PresetJson);
        if (state?.ComputeSha256 != true)
        {
            return;
        }
        if (!File.Exists(context.OutputPath))
        {
            throw new FileNotFoundException("无法校验不存在的输出文件", context.OutputPath);
        }

        context.ReportProgress("正在计算输出文件 SHA-256……", 0.6);
        await using var stream = new FileStream(
            context.OutputPath,
            FileMode.Open,
            FileAccess.Read,
            FileShare.Read,
            1024 * 128,
            FileOptions.Asynchronous | FileOptions.SequentialScan);
        var hash = await SHA256.HashDataAsync(stream, cancellationToken).ConfigureAwait(false);
        var hashText = Convert.ToHexString(hash).ToLowerInvariant();
        context.ReportResult("output.sha256", hashText, "SHA-256");
        context.ReportResult("output.bytes", stream.Length.ToString(), "输出大小", "bytes");
        context.ReportProgress("输出校验完成", 1);
    }

    // task.after-failed：只读诊断并上报结果；该阶段使用不可取消令牌，应快速返回。
    private ValueTask TaskAfterFailedAsync(
        PluginPipelineContext context,
        CancellationToken cancellationToken)
    {
        if (!IsActive(TryReadState(context.PresetJson)))
        {
            return ValueTask.CompletedTask;
        }

        context.ReportResult("task.failure", context.TaskStatus.ToString(), "失败状态");
        Log(
            PluginLogLevel.Error,
            $"任务 {context.TaskId} 进入 {DescribeTaskStatus(context.TaskStatus)} 状态");
        return ValueTask.CompletedTask;
    }

    // task.after-finish：成功、失败、取消都会运行；只做有界且幂等的缓存清理。
    private ValueTask TaskAfterFinishAsync(
        PluginPipelineContext context,
        CancellationToken cancellationToken)
    {
        if (TryRemoveSession(context.TaskId, out var session) && session is not null)
        {
            context.Properties.TryGetValue("elapsedMilliseconds", out var elapsed);
            context.ReportResult(
                "sample.cleanup",
                $"状态={context.TaskStatus}; 进程数={session.ExitCodes.Count}; 耗时={elapsed ?? "未知"}ms",
                "示例清理");
        }
        return ValueTask.CompletedTask;
    }

    private static SampleState? TryReadState(string presetJson)
    {
        if (string.IsNullOrWhiteSpace(presetJson))
        {
            return null; // 纯命令行任务允许没有预设 JSON。
        }
        try
        {
            return ReadPresetAndState(presetJson).State;
        }
        catch (JsonException)
        {
            return null;
        }
    }

    private static void ApplyQuality(JsonObject preset, SampleState state)
    {
        preset["视频参数_比特率_控制方式"] = 1;
        preset["视频参数_质量控制_参数名"] = "crf";
        preset["视频参数_质量控制_值"] = Math.Clamp(state.Crf, 0, 63).ToString();
    }

    private static string AddOutputSuffix(string path, string suffix)
    {
        var cleanSuffix = suffix.Trim();
        if (string.IsNullOrWhiteSpace(path) || string.IsNullOrWhiteSpace(cleanSuffix))
        {
            return path;
        }
        var directory = Path.GetDirectoryName(path) ?? string.Empty;
        var extension = Path.GetExtension(path);
        var name = Path.GetFileNameWithoutExtension(path);
        if (name.EndsWith(cleanSuffix, StringComparison.OrdinalIgnoreCase))
        {
            return path;
        }
        return Path.Combine(directory, name + cleanSuffix + extension);
    }

    private static string AppendTokenOnce(string source, string token)
    {
        var cleanSource = source.Trim();
        var cleanToken = token.Trim();
        if (cleanSource.Contains(cleanToken, StringComparison.Ordinal))
        {
            return cleanSource;
        }
        return string.Join(" ", new[] { cleanSource, cleanToken }.Where(x => x.Length > 0));
    }

    private static string PrependOptionOnce(string commandLine, string option)
    {
        if (commandLine.Contains(option, StringComparison.Ordinal))
        {
            return commandLine;
        }
        return $"{option} {commandLine}".TrimEnd();
    }

    private static string DescribeTaskStatus(PluginTaskStatus status) => status switch
    {
        PluginTaskStatus.Unknown => "未知",
        PluginTaskStatus.Pending => "等待中",
        PluginTaskStatus.Running => "运行中",
        PluginTaskStatus.Paused => "已暂停",
        PluginTaskStatus.Succeeded => "成功",
        PluginTaskStatus.Failed => "失败",
        PluginTaskStatus.Canceled => "已取消",
        _ => status.ToString()
    };
}
