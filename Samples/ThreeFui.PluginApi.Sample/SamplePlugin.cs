using System.Collections.Concurrent;
using System.Drawing;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Nodes;
using FFmpegFreeUI.PluginSdk;

namespace ThreeFui.PluginApi.Sample;

/// <summary>
/// C# 综合示例：以“自动质量策略、命令审计、输出校验”为应用场景，展示 Plugin API v2.1
/// 的全部 UI 锚点和处理阶段。所有会改变任务的选项默认关闭，便于直接安装调试。
/// </summary>
public sealed partial class SamplePlugin : IThreeFuiPlugin
{
    internal const string PluginId = "sample.csharp-complete-api";
    internal const string PluginTag = "[C# API 示例]";
    private static readonly Version RequiredApiVersion = ThreeFuiPluginApi.Version;

    private readonly List<IDisposable> _registrations = new();
    private readonly ConcurrentDictionary<string, TaskSession> _taskSessions =
        new(StringComparer.OrdinalIgnoreCase);
    private IThreeFuiHost? _host;

    public string Id => PluginId;
    public string DisplayName => "C# Plugin API 综合示例";

    public void Initialize(IThreeFuiHost host)
    {
        ArgumentNullException.ThrowIfNull(host);
        if (host.ApiVersion < RequiredApiVersion)
        {
            throw new NotSupportedException(
                $"本示例需要 Plugin API {RequiredApiVersion} 或更高版本，当前为 {host.ApiVersion}");
        }

        _host = host;
        host.Log(
            PluginLogLevel.Information,
            $"正在初始化 {DisplayName}；API={host.ApiVersion}，3FUI={host.HostVersion}");

        RegisterUiExtensions(host);
        RegisterPipelineHandlers(host);

        // AvailableAnchors / AvailableStages 可用于兼容较旧宿主；不要反射查找宿主私有控件或方法。
        var missingAnchors = ThreeFuiUiAnchors.All.Except(
            host.Ui.AvailableAnchors,
            StringComparer.OrdinalIgnoreCase);
        var missingStages = ThreeFuiPipelineStages.All.Except(
            host.Pipeline.AvailableStages,
            StringComparer.OrdinalIgnoreCase);
        foreach (var anchor in missingAnchors)
        {
            host.Log(PluginLogLevel.Warning, $"宿主未提供 UI 锚点：{anchor}");
        }
        foreach (var stage in missingStages)
        {
            host.Log(PluginLogLevel.Warning, $"宿主未提供处理阶段：{stage}");
        }

        host.Log(PluginLogLevel.Trace, $"已保存 {_registrations.Count} 个可释放的注册句柄");
    }

    private void RegisterUiExtensions(IThreeFuiHost host)
    {
        RegisterUiIfAvailable(
            host,
            "decorate-quality-mode",
            ThreeFuiUiAnchors.ParametersVideoQualityMode,
            CreateQualityModeDecoration,
            10);
        RegisterUiIfAvailable(
            host,
            "decorate-parameter-name",
            ThreeFuiUiAnchors.ParametersVideoQualityParameterName,
            CreateParameterNameDecoration,
            20);
        RegisterUiIfAvailable(
            host,
            "decorate-quality-value",
            ThreeFuiUiAnchors.ParametersVideoQualityValue,
            CreateQualityValueDecoration,
            30);
        RegisterUiIfAvailable(
            host,
            "quality-policy-row",
            ThreeFuiUiAnchors.ParametersVideoQualityAfterGlobal,
            CreateQualityPolicyRow,
            100);
        RegisterUiIfAvailable(
            host,
            "command-options-row",
            ThreeFuiUiAnchors.ParametersVideoQualityBeforeAdvanced,
            CreateCommandOptionsRow,
            200);
        RegisterUiIfAvailable(
            host,
            "post-process-row",
            ThreeFuiUiAnchors.ParametersVideoQualityPageBottom,
            CreatePostProcessRow,
            300);
    }

    private void RegisterUiIfAvailable(
        IThreeFuiHost host,
        string id,
        string anchorId,
        Func<IPluginUiContext, Control?> factory,
        int order)
    {
        if (!host.Ui.AvailableAnchors.Contains(anchorId, StringComparer.OrdinalIgnoreCase))
        {
            return;
        }

        _registrations.Add(host.Ui.Register(new PluginUiExtension(id, anchorId, factory)
        {
            Order = order
        }));
    }

    private void RegisterPipelineHandlers(IThreeFuiHost host)
    {
        RegisterStage(host, "migrate-state", ThreeFuiPipelineStages.PresetBeforeApply, PresetBeforeApply, -200);
        RegisterStage(host, "observe-applied-preset", ThreeFuiPipelineStages.PresetAfterApply, PresetAfterApply, 200);
        RegisterStage(host, "mark-capture", ThreeFuiPipelineStages.PresetBeforeCapture, PresetBeforeCapture, -100);
        RegisterStage(host, "normalize-captured-preset", ThreeFuiPipelineStages.PresetAfterCapture, PresetAfterCapture, 100);
        RegisterStage(host, "name-queued-task", ThreeFuiPipelineStages.QueueBeforeAdd, QueueBeforeAdd, 100);
        RegisterStage(host, "analyze-task", ThreeFuiPipelineStages.TaskBeforePrepare, TaskBeforePrepareAsync, 100);
        RegisterStage(host, "adjust-structured-command", ThreeFuiPipelineStages.CommandBeforeBuild, CommandBeforeBuild, 100);
        RegisterStage(host, "adjust-final-command", ThreeFuiPipelineStages.CommandAfterBuild, CommandAfterBuild, 100);
        RegisterStage(host, "validate-prepared-task", ThreeFuiPipelineStages.TaskAfterPrepare, TaskAfterPrepareAsync, 100);
        RegisterStage(host, "configure-process", ThreeFuiPipelineStages.ProcessBeforeStart, ProcessBeforeStartAsync, 100);
        RegisterStage(host, "observe-process", ThreeFuiPipelineStages.ProcessAfterExit, ProcessAfterExitAsync, 100);
        RegisterStage(host, "hash-output", ThreeFuiPipelineStages.TaskAfterComplete, TaskAfterCompleteAsync, 100);
        RegisterStage(host, "report-failure", ThreeFuiPipelineStages.TaskAfterFailed, TaskAfterFailedAsync, 100);
        RegisterStage(host, "release-task-cache", ThreeFuiPipelineStages.TaskAfterFinish, TaskAfterFinishAsync, 100);
    }

    private void RegisterStage(
        IThreeFuiHost host,
        string id,
        string stageId,
        PluginPipelineCallback callback,
        int order)
    {
        if (!host.Pipeline.AvailableStages.Contains(stageId, StringComparer.OrdinalIgnoreCase))
        {
            return;
        }

        _registrations.Add(host.Pipeline.Register(new PluginPipelineHandler(id, stageId, callback)
        {
            Order = order
        }));
    }

    internal void Log(PluginLogLevel level, string message, Exception? exception = null) =>
        _host?.Log(level, message, exception);

    // 调用示例：Log(PluginLogLevel.Error, "处理失败", exception);

    internal TaskSession GetOrCreateSession(PluginPipelineContext context)
    {
        var taskId = string.IsNullOrWhiteSpace(context.TaskId)
            ? $"preview:{context.SurfaceId}"
            : context.TaskId;
        return _taskSessions.GetOrAdd(taskId, _ => new TaskSession());
    }

    internal bool TryRemoveSession(string taskId, out TaskSession? session)
    {
        if (string.IsNullOrWhiteSpace(taskId))
        {
            session = null;
            return false;
        }

        return _taskSessions.TryRemove(taskId, out session);
    }

    internal sealed class TaskSession
    {
        public DateTimeOffset StartedAt { get; init; } = DateTimeOffset.Now;
        public string InputPath { get; set; } = string.Empty;
        public string OutputPath { get; set; } = string.Empty;
        public ConcurrentQueue<int> ExitCodes { get; } = new();
    }

    internal sealed class SampleState
    {
        public int Version { get; set; } = 2;
        public bool Enabled { get; set; }
        public int Crf { get; set; } = 32;
        public bool PrefixTaskName { get; set; }
        public string OutputSuffix { get; set; } = string.Empty;
        public string AdvancedArguments { get; set; } = string.Empty;
        public bool AddNoStats { get; set; }
        public bool AddNoStdin { get; set; }
        public string ProcessOverride { get; set; } = string.Empty;
        public bool AcceptExitCodeOne { get; set; }
        public bool ComputeSha256 { get; set; }
        public string LastSurfaceId { get; set; } = string.Empty;
    }

    internal static SampleState DeserializeState(string? json)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            return new SampleState();
        }

        try
        {
            return JsonSerializer.Deserialize<SampleState>(json) ?? new SampleState();
        }
        catch (JsonException)
        {
            return new SampleState();
        }
    }

    internal static (JsonObject Preset, SampleState State) ReadPresetAndState(string presetJson)
    {
        var preset = JsonNode.Parse(presetJson)?.AsObject()
            ?? throw new InvalidOperationException("宿主没有提供有效的预设 JSON 对象");
        var extensionData = preset["插件扩展数据"] as JsonObject;
        string? stateJson = null;
        if (extensionData?[PluginId] is JsonValue stateValue &&
            stateValue.TryGetValue<string>(out var value))
        {
            stateJson = value;
        }

        return (preset, DeserializeState(stateJson));
    }

    internal static void WriteState(JsonObject preset, SampleState state)
    {
        var extensionData = preset["插件扩展数据"] as JsonObject;
        if (extensionData is null)
        {
            extensionData = new JsonObject();
            preset["插件扩展数据"] = extensionData;
        }

        extensionData[PluginId] = JsonSerializer.Serialize(state);
    }

    internal static void WritePreset(PluginPipelineContext context, JsonObject preset) =>
        context.PresetJson = preset.ToJsonString(new JsonSerializerOptions { WriteIndented = true });

    internal static bool IsActive(SampleState? state) => state is not null &&
        (state.Enabled ||
         state.PrefixTaskName ||
         !string.IsNullOrWhiteSpace(state.OutputSuffix) ||
         !string.IsNullOrWhiteSpace(state.AdvancedArguments) ||
         state.AddNoStats ||
         state.AddNoStdin ||
         !string.IsNullOrWhiteSpace(state.ProcessOverride) ||
         state.AcceptExitCodeOne ||
         state.ComputeSha256);
}
