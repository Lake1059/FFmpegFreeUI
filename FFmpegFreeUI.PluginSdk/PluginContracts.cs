using System.Collections.ObjectModel;
using System.Windows.Forms;

namespace FFmpegFreeUI.PluginSdk;

/// <summary>3FUI v2 插件入口。每个插件程序集可以包含一个实现。</summary>
public interface IThreeFuiPlugin
{
    string Id { get; }
    string DisplayName { get; }
    void Initialize(IThreeFuiHost host);
}

/// <summary>3FUI 向插件开放的宿主能力。</summary>
public interface IThreeFuiHost
{
    Version ApiVersion { get; }
    string HostVersion { get; }
    IPluginUiRegistry Ui { get; }
    IPluginPipelineRegistry Pipeline { get; }
    void Log(PluginLogLevel level, string message, Exception? exception = null);
}

public enum PluginLogLevel
{
    Trace,
    Information,
    Warning,
    Error
}

/// <summary>在宿主定义的稳定 UI 锚点上注册控件或装饰逻辑。</summary>
public interface IPluginUiRegistry
{
    IReadOnlyCollection<string> AvailableAnchors { get; }
    IDisposable Register(PluginUiExtension extension);
}

/// <summary>
/// 描述一项 UI 扩展。若要向锚点插槽插入界面，应为每个上下文返回新的 Control；
/// 若扩展只装饰 Context.AnchorControl，则返回 null。
/// </summary>
public sealed class PluginUiExtension
{
    public PluginUiExtension(string id, string anchorId, Func<IPluginUiContext, Control?> createControl)
    {
        Id = id;
        AnchorId = anchorId;
        CreateControl = createControl;
    }

    public string Id { get; set; }
    public string AnchorId { get; set; }
    public int Order { get; set; }
    public Func<IPluginUiContext, Control?> CreateControl { get; set; }
}

/// <summary>某个插件扩展在一个原生 UI 界面实例中的独立上下文。</summary>
public interface IPluginUiContext
{
    string PluginId { get; }
    string ExtensionId { get; }
    string AnchorId { get; }
    string SurfaceId { get; }

    /// <summary>锚点所标识的原生控件。</summary>
    Control AnchorControl { get; }

    /// <summary>宿主创建的插入容器；仅支持装饰的锚点为 null。</summary>
    Control? ContainerControl { get; }

    /// <summary>
    /// 返回同一 UI 界面中另一个已注册锚点的原生控件；该锚点不可用时返回 null。
    /// 扩展可借此协调相邻控件，无需反射或依赖宿主的私有控件层级。
    /// </summary>
    Control? GetAnchorControl(string anchorId);

    /// <summary>随当前 v6 预设持久化、由插件自行解释的 JSON。</summary>
    string StateJson { get; set; }

    /// <summary>将另一个预设的插件状态恢复到当前界面后触发。</summary>
    event EventHandler? StateRestored;

    void RequestParameterRefresh();
}

/// <summary>在 3FUI 参数处理管线的稳定阶段注册有序处理器。</summary>
public interface IPluginPipelineRegistry
{
    IReadOnlyCollection<string> AvailableStages { get; }
    IDisposable Register(PluginPipelineHandler handler);
}

public delegate ValueTask PluginPipelineCallback(
    PluginPipelineContext context,
    CancellationToken cancellationToken);

public sealed class PluginPipelineHandler
{
    public PluginPipelineHandler(string id, string stageId, PluginPipelineCallback callback)
    {
        Id = id;
        StageId = stageId;
        Callback = callback;
    }

    public string Id { get; set; }
    public string StageId { get; set; }
    public int Order { get; set; }
    public PluginPipelineCallback Callback { get; set; }
}

/// <summary>
/// 在管线阶段间传递的可变数据。PresetJson 是完整的 v6 预设；插件替换它时，
/// 应保留不属于本插件的字段。
/// </summary>
public sealed class PluginPipelineContext
{
    private readonly Action<PluginPipelineProgress>? _progressSink;
    private readonly Action<PluginTaskResult>? _resultSink;

    public PluginPipelineContext(Action<PluginPipelineProgress>? progressSink = null)
        : this(progressSink, null)
    {
    }

    /// <summary>
    /// 供宿主创建带进度和结构化结果接收器的上下文。保留单参数构造函数以兼容既有插件。
    /// </summary>
    public PluginPipelineContext(
        Action<PluginPipelineProgress>? progressSink,
        Action<PluginTaskResult>? resultSink)
    {
        _progressSink = progressSink;
        _resultSink = resultSink;
    }

    public string StageId { get; set; } = string.Empty;
    public string PresetJson { get; set; } = string.Empty;
    public string InputPath { get; set; } = string.Empty;
    public string OutputPath { get; set; } = string.Empty;
    public string CommandLine { get; set; } = string.Empty;
    public string ProcessFileName { get; set; } = string.Empty;
    public string TaskId { get; set; } = string.Empty;
    public string SurfaceId { get; set; } = string.Empty;
    public string PhaseName { get; set; } = string.Empty;
    public bool IsPreview { get; set; }
    public int? ExitCode { get; set; }
    /// <summary>宿主提供的当前任务状态。插件赋值不会改变 3FUI 的真实任务状态。</summary>
    public PluginTaskStatus TaskStatus { get; set; }
    public IDictionary<string, string?> Properties { get; } =
        new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase);

    public void ReportProgress(string message, double? fraction = null) =>
        _progressSink?.Invoke(new PluginPipelineProgress(message, fraction));

    /// <summary>
    /// 为当前编码任务发布一个可覆盖的结构化结果。同一插件使用相同 key 再次上报时更新原结果。
    /// 结果会写入任务日志；没有实际任务接收器的预览或预设阶段会忽略本次上报。
    /// </summary>
    public void ReportResult(
        string key,
        string value,
        string? displayName = null,
        string? unit = null)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            throw new ArgumentException("结果 key 不能为空", nameof(key));
        }

        _resultSink?.Invoke(new PluginTaskResult(
            key.Trim(),
            value ?? string.Empty,
            string.IsNullOrWhiteSpace(displayName) ? null : displayName.Trim(),
            string.IsNullOrWhiteSpace(unit) ? null : unit.Trim()));
    }
}

public sealed record PluginPipelineProgress(string Message, double? Fraction = null);

/// <summary>插件向当前编码任务发布的结构化结果。</summary>
public sealed record PluginTaskResult(
    string Key,
    string Value,
    string? DisplayName = null,
    string? Unit = null);

/// <summary>当前任务在插件处理链中的生命周期状态。</summary>
public enum PluginTaskStatus
{
    Unknown,
    Pending,
    Running,
    Paused,
    Succeeded,
    Failed,
    Canceled
}

/// <summary>稳定的 API 与发现机制常量。</summary>
public static class ThreeFuiPluginApi
{
    public static Version Version { get; } = new(2, 1, 0);
}

/// <summary>宿主当前提供的 UI 锚点 ID。</summary>
public static class ThreeFuiUiAnchors
{
    public const string ParametersVideoQualityMode = "parameters.video.quality.mode";
    public const string ParametersVideoQualityParameterName = "parameters.video.quality.parameter-name";
    public const string ParametersVideoQualityValue = "parameters.video.quality.value";
    public const string ParametersVideoQualityAfterGlobal = "parameters.video.quality.global.after";
    public const string ParametersVideoQualityBeforeAdvanced = "parameters.video.quality.advanced.before";
    public const string ParametersVideoQualityPageBottom = "parameters.video.quality.page.bottom";

    public static IReadOnlyCollection<string> All { get; } = new ReadOnlyCollection<string>(
        new[]
        {
            ParametersVideoQualityMode,
            ParametersVideoQualityParameterName,
            ParametersVideoQualityValue,
            ParametersVideoQualityAfterGlobal,
            ParametersVideoQualityBeforeAdvanced,
            ParametersVideoQualityPageBottom
        });
}

/// <summary>
/// 管线阶段 ID。预设、队列和命令构建阶段同步执行；任务与进程阶段异步执行，
/// 并接收取消令牌。
/// </summary>
public static class ThreeFuiPipelineStages
{
    public const string PresetBeforeCapture = "preset.before-capture";
    public const string PresetAfterCapture = "preset.after-capture";
    public const string PresetBeforeApply = "preset.before-apply";
    public const string PresetAfterApply = "preset.after-apply";
    public const string QueueBeforeAdd = "queue.before-add";
    public const string TaskBeforePrepare = "task.before-prepare";
    public const string TaskAfterPrepare = "task.after-prepare";
    public const string CommandBeforeBuild = "command.before-build";
    public const string CommandAfterBuild = "command.after-build";
    public const string ProcessBeforeStart = "process.before-start";
    public const string ProcessAfterExit = "process.after-exit";
    /// <summary>全部原生步骤成功后、任务标记完成前执行一次；适合可取消的成功后处理。</summary>
    public const string TaskAfterComplete = "task.after-complete";
    /// <summary>任务确定失败后执行一次；用户取消不触发。</summary>
    public const string TaskAfterFailed = "task.after-failed";
    /// <summary>任务成功、失败或取消后均执行一次；适合有界清理。</summary>
    public const string TaskAfterFinish = "task.after-finish";

    public static IReadOnlyCollection<string> All { get; } = new ReadOnlyCollection<string>(
        new[]
        {
            PresetBeforeCapture,
            PresetAfterCapture,
            PresetBeforeApply,
            PresetAfterApply,
            QueueBeforeAdd,
            TaskBeforePrepare,
            TaskAfterPrepare,
            CommandBeforeBuild,
            CommandAfterBuild,
            ProcessBeforeStart,
            ProcessAfterExit,
            TaskAfterComplete,
            TaskAfterFailed,
            TaskAfterFinish
        });
}
