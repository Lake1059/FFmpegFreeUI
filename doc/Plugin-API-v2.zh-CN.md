# 3FUI Plugin API v2.1 插件开发指南

本文面向希望扩展 3FUI 原生参数界面和任务处理链的插件开发者，对应当前 Plugin API `2.1.0`。插件只需要面向`FFmpegFreeUI.PluginSdk.dll` 的公共类型编程，不应引用`FFmpegFreeUI.exe` 的内部类型，也不应通过反射查找 3FUI 的私有控件或方法。

Plugin API v2.1 提供两类能力：

- UI 扩展：装饰宿主公开的原生控件，或在稳定锚点插入下拉框、输入框、按钮等 WinForms 控件。
- 处理链扩展：在预设、入队、任务准备、命令生成、进程执行和任务终态阶段注册有序处理器。

API v2 仍处于实验阶段。公开的锚点 ID、阶段 ID 和合同类型会作为兼容性契约维护；如果以后必须进行破坏性修改，应提升 API 主版本号。

## 1. 运行时组成与安装目录

Plugin API v2 由三部分组成：

| 组件 | 作用 | 放置位置 |
|---|---|---|
| `FFmpegFreeUI.exe` | 3FUI 核心；只保留不直接引用 SDK 的桥接逻辑 | 程序根目录 |
| `FFmpegFreeUI.PluginHost.dll` | v2 宿主实现；把 SDK 合同连接到 3FUI 的界面和任务系统 | 程序根目录 |
| `FFmpegFreeUI.PluginSdk.dll` | 插件编译时引用的公共合同 | 程序根目录 |

标准目录结构如下：

```text
FFmpegFreeUI.exe
FFmpegFreeUI.PluginHost.dll
FFmpegFreeUI.PluginSdk.dll
Plugin\
├─ MyCompany.MyPlugin.3fui.dll
├─ MyCompany.MyPlugin.Dependency.dll
└─ ...
```

注意：

- 3FUI 只从 `Plugin` 目录发现文件名匹配 `*.3fui.dll` 的入口程序集。
- `PluginHost` 和 `PluginSdk` 必须放在程序根目录，不要放进 `Plugin`。
- 插件自己的托管依赖可以放在 `Plugin`；不要复制另一份 SDK、PluginHost、LakeUI 或宿主自带依赖。
- 根目录缺少 `FFmpegFreeUI.PluginSdk.dll` 时，Plugin API v2 会安全禁用。依赖 SDK 的插件会在程序集加载之前被静默跳过，3FUI 本体仍可运行。
- 根目录缺少 `FFmpegFreeUI.PluginHost.dll`、版本不兼容或初始化失败时，v2 同样安全禁用。
- 当前桥接层要求 SDK 和 PluginHost 都是 `2.1.0` 或更高的 `2.x` 版本；不同主版本不兼容。
- 原有 `Entry` / `SetHost_*` 插件仍走旧加载逻辑，但不能使用本指南中的 UI 锚点和处理阶段。

### 为什么同时需要 PluginSdk 和 PluginHost

`PluginSdk` 只定义接口、上下文和稳定 ID，不引用 3FUI 内部实现；`PluginHost` 负责把这些接口连接到3FUI。这样核心程序没有 SDK 时仍能启动，也避免插件直接依赖庞大的宿主程序集。

相关源码：

- SDK 合同：[`FFmpegFreeUI.PluginSdk/PluginContracts.cs`](../FFmpegFreeUI.PluginSdk/PluginContracts.cs)
- 可选桥接：[`FFmpegFreeUI/功能/插件扩展桥接_v2.vb`](../FFmpegFreeUI/功能/插件扩展桥接_v2.vb)
- 宿主实现：[`FFmpegFreeUI/功能/插件扩展宿主_v2.vb`](../FFmpegFreeUI/功能/插件扩展宿主_v2.vb)
- PluginHost 项目：[`FFmpegFreeUI.PluginHost/FFmpegFreeUI.PluginHost.vbproj`](../FFmpegFreeUI.PluginHost/FFmpegFreeUI.PluginHost.vbproj)

## 2. 开发环境与示例

当前项目使用 Windows、.NET 10 和 WinForms。可使用 Visual Studio、Rider、Visual Studio Code 或`dotnet` 命令行。

仓库提供两套可直接编译的全接口示例：

- [C# 综合示例](../Samples/ThreeFui.PluginApi.Sample)：自动质量策略、命令/进程处理和 SHA-256 后处理。
- [VB.NET 综合示例](../Samples/ThreeFui.PluginApi.VbVmafSample)：自动质量策略、命令/进程处理和 VMAF 后处理。

两套示例都覆盖当前全部 6 个 UI 锚点和 14 个处理阶段。遇到文档与行为不一致时，以当前 SDK 公共合同和可编译示例为准。

## 3. 从零创建插件项目

### 3.1 创建 C# 类库

```powershell
dotnet new classlib -lang C# -n MyCompany.MyPlugin -f net10.0
cd MyCompany.MyPlugin
```

将 `.csproj` 改为：

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net10.0-windows10.0.17763.0</TargetFramework>
    <UseWindowsForms>true</UseWindowsForms>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
    <AssemblyName>MyCompany.MyPlugin.3fui</AssemblyName>
    <RootNamespace>MyCompany.MyPlugin</RootNamespace>
  </PropertyGroup>

  <ItemGroup>
    <ProjectReference Include="..\FFmpegFreeUI\FFmpegFreeUI.PluginSdk\FFmpegFreeUI.PluginSdk.csproj">
      <Private>false</Private>
    </ProjectReference>
  </ItemGroup>
</Project>
```

### 3.2 创建 VB.NET 类库

```powershell
dotnet new classlib -lang VB -n MyCompany.MyPlugin -f net10.0
cd MyCompany.MyPlugin
```

将 `.vbproj` 改为：

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net10.0-windows10.0.17763.0</TargetFramework>
    <UseWindowsForms>true</UseWindowsForms>
    <OptionExplicit>On</OptionExplicit>
    <OptionInfer>On</OptionInfer>
    <OptionStrict>On</OptionStrict>
    <AssemblyName>MyCompany.MyPlugin.3fui</AssemblyName>
    <RootNamespace>MyCompany.MyPlugin</RootNamespace>
  </PropertyGroup>

  <ItemGroup>
    <ProjectReference Include="..\FFmpegFreeUI\FFmpegFreeUI.PluginSdk\FFmpegFreeUI.PluginSdk.csproj">
      <Private>false</Private>
    </ProjectReference>
  </ItemGroup>
</Project>
```

关键设置：

- 必须使用与当前 SDK 兼容的 Windows 目标框架并启用 WinForms。
- `AssemblyName` 必须以 `.3fui` 结尾，输出文件才会匹配 `*.3fui.dll`。
- `<Private>false</Private>` 防止构建时把 SDK 当作插件私有依赖复制到发布目录。
- 如果使用 SDK 二进制而不是源码项目，可改用带 `HintPath` 的 `<Reference>`，仍应设置`<Private>false</Private>`。
- SDK 当前不是必须从 NuGet 获取；直接引用项目或发行包提供的合同 DLL 即可。
- LakeUI 不是 Plugin API 的硬依赖。普通 WinForms 控件最不容易受宿主 UI 库版本影响。如果插件自行引用第三方 UI 库，开发者需处理版本兼容、分发和许可证义务。

## 4. 实现插件入口

3FUI 会在每个 `*.3fui.dll` 中查找实现 `IThreeFuiPlugin` 的可实例化类型。入口必须：

1. 实现 `IThreeFuiPlugin`；
2. 不是抽象类或接口；
3. 提供公共无参构造函数；为便于其他工具发现和调试，也建议入口类型本身公开；
4. 返回非空且全局唯一的插件 `Id`。

一个程序集可以包含多个入口，宿主按类型全名排序后初始化；为了发布和排错简单，通常一个程序集只提供一个入口。

### 4.1 C# 入口

```csharp
using FFmpegFreeUI.PluginSdk;

namespace MyCompany.MyPlugin;

public sealed class MyPlugin : IThreeFuiPlugin
{
    private readonly List<IDisposable> _registrations = new();

    // 推荐使用组织前缀或反向域名；发布后不要随意修改。
    public string Id => "com.example.my-plugin";
    public string DisplayName => "我的 3FUI 插件";

    public void Initialize(IThreeFuiHost host)
    {
        if (host.ApiVersion < new Version(2, 1, 0))
        {
            throw new NotSupportedException("需要 Plugin API 2.1 或更高版本");
        }

        host.Log(
            PluginLogLevel.Information,
            $"插件已初始化；API={host.ApiVersion}，3FUI={host.HostVersion}");
    }
}
```

### 4.2 VB.NET 入口

```vb
Imports FFmpegFreeUI.PluginSdk

Public NotInheritable Class MyPlugin
    Implements IThreeFuiPlugin

    Private ReadOnly 注册项 As New List(Of IDisposable)

    Public ReadOnly Property Id As String Implements IThreeFuiPlugin.Id
        Get
            Return "com.example.my-plugin"
        End Get
    End Property

    Public ReadOnly Property DisplayName As String Implements IThreeFuiPlugin.DisplayName
        Get
            Return "我的 3FUI 插件"
        End Get
    End Property

    Public Sub Initialize(host As IThreeFuiHost) Implements IThreeFuiPlugin.Initialize
        If host.ApiVersion < New Version(2, 1, 0) Then
            Throw New NotSupportedException("需要 Plugin API 2.1 或更高版本")
        End If

        host.Log(
            PluginLogLevel.Information,
            $"插件已初始化；API={host.ApiVersion}，3FUI={host.HostVersion}")
    End Sub
End Class
```

`Initialize` 在启动加载插件时调用。这里只做版本检查和轻量注册，不要同步扫描大量文件、访问网络或等待外部进程。初始化抛出异常会导致当前插件加载失败。

注册方法返回 `IDisposable`。宿主会跟踪这些句柄，并在插件作用域释放时按相反顺序注销；插件也可保存句柄以便主动提前注销。当前版本没有插件热重载，修改 DLL 后应完全退出并重新启动 3FUI。

## 5. 宿主接口 `IThreeFuiHost`

| 成员 | 含义 |
|---|---|
| `ApiVersion` | 宿主实际支持的 Plugin API 版本；注册新能力前应检查。 |
| `HostVersion` | 3FUI 主程序集版本字符串，可用于诊断，不建议依赖字符串比较实现功能开关。 |
| `Ui` | UI 扩展注册表。 |
| `Pipeline` | 处理链注册表。 |
| `Log(level, message, exception)` | 写插件诊断信息；当前实现输出到调试器，不等同于任务日志。 |

`PluginLogLevel` 包含：

- `Trace`：高频诊断；
- `Information`：初始化或正常状态；
- `Warning`：功能降级或可恢复问题；
- `Error`：插件错误，可附带异常。

需要让用户在当前编码任务日志中看到信息时，使用管线上下文的 `ReportProgress` 或 `ReportResult`，不要只调用 `host.Log`。

## 6. UI 扩展

### 6.1 注册 UI 扩展

```csharp
if (host.Ui.AvailableAnchors.Contains(
        ThreeFuiUiAnchors.ParametersVideoQualityAfterGlobal))
{
    _registrations.Add(host.Ui.Register(new PluginUiExtension(
        id: "quality-options",
        anchorId: ThreeFuiUiAnchors.ParametersVideoQualityAfterGlobal,
        createControl: CreateQualityOptions)
    {
        Order = 100
    }));
}
```

`PluginUiExtension` 成员：

| 成员 | 规则 |
|---|---|
| `Id` | 当前插件内全局唯一；同一插件不能在不同锚点复用同一扩展 ID。 |
| `AnchorId` | 宿主公开的稳定锚点 ID。优先使用 `ThreeFuiUiAnchors` 常量。 |
| `Order` | 同一锚点内从小到大排列。相同值再按插件 ID、扩展 ID 排序。 |
| `CreateControl` | 每个参数面板实例都会调用；插入型返回新控件，装饰型必须返回 `null`/`Nothing`。 |

`AvailableAnchors` 表示宿主支持的锚点合同，不要求参数面板此刻已经打开。插件可以在启动时注册，宿主会在相应界面实例创建后应用扩展。

### 6.2 6 个 UI 锚点

当前全部锚点都位于“参数面板 → 视频参数｜质量”。UI 锚点只决定界面位置，不会自动修改预设或处理链；需要持久化和参数生效时，应配合 `StateJson` 与管线阶段。

| SDK 常量 / ID | 类型与位置 | 可做的事情 | 限制 |
|---|---|---|---|
| `ParametersVideoQualityMode` / `parameters.video.quality.mode` | 装饰型；全局质量控制方式下拉框 | 读取文本、追加选项、绑定事件、改变启用状态 | 工厂必须返回 `null`/`Nothing`；新增下拉项不会自动扩展宿主预设枚举。 |
| `ParametersVideoQualityParameterName` / `parameters.video.quality.parameter-name` | 装饰型；质量参数名控件 | 读取或填写 `-crf`、`-cq` 等参数名 | 原生捕获仍按宿主逻辑执行。 |
| `ParametersVideoQualityValue` / `parameters.video.quality.value` | 装饰型；质量值输入框 | 读取、填写、清空或锁定质量值 | 耗时计算不能阻塞 UI，应放到异步任务阶段。 |
| `ParametersVideoQualityAfterGlobal` / `parameters.video.quality.global.after` | 插入型；全局质量控制行之后 | 插入与质量策略配套的下拉框、输入框、按钮和说明 | 工厂返回一个新控件。 |
| `ParametersVideoQualityBeforeAdvanced` / `parameters.video.quality.advanced.before` | 插入型；比特率区域之后、进阶质量控制之前 | 插入高级参数生成器、校验按钮或摘要 | 只是视觉位置，参数仍需写入预设或命令上下文。 |
| `ParametersVideoQualityPageBottom` / `parameters.video.quality.page.bottom` | 插入型；进阶参数编辑区域之前的公开插槽 | 插入后处理开关或工具栏 | 名称是兼容性合同，不应假设它永远是滚动页面绝对末尾。 |

### 6.3 `IPluginUiContext` 全部成员

| 成员 | 含义 |
|---|---|
| `PluginId` | 当前插件入口 ID。 |
| `ExtensionId` | 当前 UI 扩展 ID。 |
| `AnchorId` | 当前锚点 ID。 |
| `SurfaceId` | 当前参数面板实例 ID；多个面板实例之间不同。 |
| `AnchorControl` | 锚点标识的原生控件。只使用稳定的 WinForms 公共成员。 |
| `ContainerControl` | 插入型锚点由宿主创建的容器；装饰型为 `null`/`Nothing`。 |
| `GetAnchorControl(anchorId)` | 获取同一参数面板实例中的另一个公开锚点控件；不可用时返回空。 |
| `StateJson` | 当前参数面板实例中、按插件 ID 隔离的持久化 JSON。默认是 `{}`。 |
| `StateRestored` | 宿主从另一个预设恢复该插件状态后触发。 |
| `RequestParameterRefresh()` | 请求刷新参数总览和命令预览。 |

同一插件在一个参数面板中注册的多个 UI 扩展共享一份 `StateJson`；不同插件按插件 ID 隔离。赋给`StateJson` 的内容必须是有效 JSON，宿主会解析并规范化，损坏的 JSON 会抛出异常。

### 6.4 插入型控件与状态持久化示例

```csharp
private sealed class PluginState
{
    public int Version { get; set; } = 1;
    public bool Enabled { get; set; }
    public string Value { get; set; } = "32";
}

private static Control CreateQualityOptions(IPluginUiContext context)
{
    var row = new FlowLayoutPanel
    {
        AutoSize = true,
        Dock = DockStyle.Top,
        WrapContents = false
    };
    var enabled = new CheckBox { AutoSize = true, Text = "启用插件质量值" };
    var value = new TextBox { Width = 100 };
    var refresh = new Button { AutoSize = true, Text = "刷新预览" };
    row.Controls.AddRange(new Control[] { enabled, value, refresh });

    var restoring = false;

    PluginState ReadState()
    {
        try
        {
            return JsonSerializer.Deserialize<PluginState>(context.StateJson) ?? new();
        }
        catch (JsonException)
        {
            return new PluginState();
        }
    }

    void Restore()
    {
        restoring = true;
        try
        {
            var state = ReadState();
            enabled.Checked = state.Enabled;
            value.Text = state.Value;
        }
        finally
        {
            restoring = false;
        }
    }

    void Save()
    {
        if (restoring) return;
        context.StateJson = JsonSerializer.Serialize(new PluginState
        {
            Enabled = enabled.Checked,
            Value = value.Text.Trim()
        });
        context.RequestParameterRefresh();
    }

    enabled.CheckedChanged += (_, _) => Save();
    value.TextChanged += (_, _) => Save();
    refresh.Click += (_, _) => context.RequestParameterRefresh();

    EventHandler restored = (_, _) => Restore();
    context.StateRestored += restored;
    row.Disposed += (_, _) => context.StateRestored -= restored;
    Restore();
    return row;
}
```

每次调用控件工厂必须创建新的控件，不能复用已经有 `Parent` 的全局实例。订阅 `StateRestored` 后，应在
控件释放时取消订阅；恢复期间使用保护标志，避免 `TextChanged` 等事件形成写回和刷新循环。

### 6.5 装饰型锚点示例

```csharp
private static Control? DecorateQualityValue(IPluginUiContext context)
{
    context.AnchorControl.AccessibleDescription = "插件可以辅助填写该质量值";

    var modeControl = context.GetAnchorControl(
        ThreeFuiUiAnchors.ParametersVideoQualityMode);
    // 可以读取或协调另一个公开锚点，但不要依赖宿主私有类型。

    return null; // 装饰型锚点必须返回 null。
}
```

如果装饰型工厂返回了控件，宿主会释放该控件并报错。

## 7. 处理链注册与执行规则

### 7.1 注册处理器

```csharp
if (host.Pipeline.AvailableStages.Contains(
        ThreeFuiPipelineStages.TaskBeforePrepare))
{
    _registrations.Add(host.Pipeline.Register(new PluginPipelineHandler(
        id: "prepare-quality",
        stageId: ThreeFuiPipelineStages.TaskBeforePrepare,
        callback: PrepareQualityAsync)
    {
        Order = 100
    }));
}
```

`PluginPipelineHandler` 成员：

| 成员 | 规则 |
|---|---|
| `Id` | 当前插件内全局唯一；不能在不同阶段复用同一处理器 ID。 |
| `StageId` | 必须是宿主支持的阶段；使用 `ThreeFuiPipelineStages` 常量。 |
| `Order` | 同一阶段内从小到大执行。 |
| `Callback` | 签名为 `ValueTask Callback(PluginPipelineContext, CancellationToken)`。 |

### 7.2 同阶段多个插件如何执行

对同一个上下文，同一阶段的处理器不是并行执行，而是按下面的稳定顺序逐个等待：

1. `Order` 从小到大；
2. `Order` 相同时按插件 ID；
3. 仍相同时按处理器 ID。

每个处理器成功返回后，宿主把它对上下文的修改复制回共享上下文，所以下一个处理器能看到前一个处理器的修改，也可能再次覆盖这些修改。不同阶段的 `Order` 互不比较。

当前实现采用失败即停：

- 一个处理器抛出异常后，当前阶段剩余处理器不再执行；
- 异常信息会包含插件 ID、处理器 ID 和阶段 ID；
- 普通任务阶段的异常通常会使当前任务失败；
- `task.after-failed` 和 `task.after-finish` 的外层会捕获并写任务日志，不改变已经确定的终态，但当前阶段中排在失败处理器之后的其他插件仍不会运行；
- 一个终态处理器长时间不返回，也会阻塞后续插件和任务最终清理。

因此，公共插件尤其应让 `task.after-finish` 保持快速、有界、幂等并自行处理可恢复错误。不要把插件间依赖建立在恰好相同的默认 `Order` 上。

### 7.3 同步阶段与异步阶段

以下阶段是同步阶段：

- `preset.before-apply`
- `preset.after-apply`
- `preset.before-capture`
- `preset.after-capture`
- `queue.before-add`
- `command.before-build`
- `command.after-build`

同步阶段的回调必须立即返回已经完成的 `ValueTask`。如果回调开始异步等待，宿主会报错。不要用`.Result`、`.Wait()` 或 `GetAwaiter().GetResult()` 在同步阶段阻塞 I/O。

其余 `task.*` 和 `process.*` 阶段是异步阶段，可以等待文件、网络和外部进程，并应传递取消令牌。

### 7.4 C# 与 VB.NET 回调写法

C# 可以直接使用 `async ValueTask`：

```csharp
private static async ValueTask PrepareQualityAsync(
    PluginPipelineContext context,
    CancellationToken cancellationToken)
{
    context.ReportProgress("正在分析媒体……", 0.1);
    await Task.Delay(200, cancellationToken).ConfigureAwait(false);
    context.ReportProgress("分析完成", 1);
}
```

VB.NET 的 `Async Function` 不能直接声明为 `ValueTask`，使用适配器：

```vb
Private Shared Function PrepareQualityAsync(
    context As PluginPipelineContext,
    cancellationToken As CancellationToken) As ValueTask

    Return New ValueTask(PrepareQualityCoreAsync(context, cancellationToken))
End Function

Private Shared Async Function PrepareQualityCoreAsync(
    context As PluginPipelineContext,
    cancellationToken As CancellationToken) As Task

    context.ReportProgress("正在分析媒体……", 0.1)
    Await Task.Delay(200, cancellationToken).ConfigureAwait(False)
    context.ReportProgress("分析完成", 1)
End Function
```

## 8. 完整处理链

### 8.1 总体时序

```text
加载预设到参数面板：
  preset.before-apply
    → 3FUI 把 PresetJson 映射到原生控件
    → 恢复各插件 StateJson，并触发 StateRestored
    → preset.after-apply

从参数面板捕获预设：
  预取当前插件 StateJson
    → preset.before-capture
    → 3FUI 从全部原生控件捕获预设字段
    → 再次捕获插件 StateJson
    → preset.after-capture

加入编码队列：
  queue.before-add
    → 任务进入队列

任务实际启动：
  task.before-prepare
    → 计算输入、输出和一个或多个步骤
    → 每个步骤生成命令：
        command.before-build
          → 3FUI 生成该步骤参数
          → command.after-build
    → task.after-prepare
    → 如果任务数据变化，重新计算输出并重建全部步骤
    → 每个步骤依次执行：
        process.before-start
          → 启动外部进程并等待退出
          → process.after-exit

全部原生步骤成功：
  task.after-complete
    → 全部成功后任务标记已完成
    → 任一后处理失败则任务转为错误

进入终态：
  错误：task.after-failed
  成功、错误或用户取消：task.after-finish
```

`command.*` 和 `process.*` 会按步骤重复；参数预览、参数总览、任务重建和二次编码也可能重复生成命令。
相应处理器必须幂等，不能在预览中执行计费、上传、删除文件等一次性副作用。

### 8.2 14 个阶段及其准确作用

| 阶段 | 位置与同步性 | 修改后会被宿主消费的主要内容 | 典型用途和注意事项 |
|---|---|---|---|
| `PresetBeforeApply` / `preset.before-apply` | 同步；预设写入任何原生控件之前 | `PresetJson` | 迁移旧状态、补默认值、把自定义表示转换成原生字段。此时修改会影响随后原生界面映射。 |
| `PresetAfterApply` / `preset.after-apply` | 同步；原生控件和插件状态已经恢复 | 主要作为通知点 | 此时再改 `PresetJson` 不会自动重新映射到界面。插件控件应通过 `StateRestored` 恢复。 |
| `PresetBeforeCapture` / `preset.before-capture` | 同步；宿主尚未从原生控件捕获字段 | 尚未被后续原生捕获覆盖的辅助内容 | 编码器、质量等原生字段随后可能被控件值覆盖；需要最终覆盖时使用 `preset.after-capture`。插件状态还会再次从 UI 捕获。 |
| `PresetAfterCapture` / `preset.after-capture` | 同步；完整原生字段和插件状态均已捕获 | `PresetJson` | 入队前规范化、校验和最终覆盖预设字段的可靠位置。调用频率可能很高，禁止耗时工作。 |
| `QueueBeforeAdd` / `queue.before-add` | 同步；任务对象已创建、尚未进入队列 | `PresetJson`、`InputPath`、`OutputPath`、`CommandLine`、`Properties["taskName"]` | 快速修改新任务快照或显示名。预设任务和纯命令行任务都会触发，后者 `PresetJson` 可能为空。 |
| `TaskBeforePrepare` / `task.before-prepare` | 异步；输出名和步骤生成之前 | `PresetJson`、`InputPath`、`OutputPath`、`CommandLine` | 媒体探测、网络查询、自动选择参数的首选阶段。修改后宿主据此重算输出并构建步骤；必须响应取消。 |
| `CommandBeforeBuild` / `command.before-build` | 同步；每个步骤开始生成参数之前 | 本次构建使用的 `PresetJson`、`InputPath`、`OutputPath` | 对当前 `PhaseName` 做结构化参数调整。`CommandLine` 此时尚未生成；修改通常只作用于本次命令构建。 |
| `CommandAfterBuild` / `command.after-build` | 同步；当前步骤完整参数字符串已生成 | `CommandLine` | 最后追加、删除或重排参数。字符串处理容易破坏引号、路径和映射，优先在更早阶段改结构化预设。其他字段不会触发本次重建。 |
| `TaskAfterPrepare` / `task.after-prepare` | 异步；全部步骤首次生成、任何进程启动之前 | `PresetJson`、`InputPath`、`OutputPath`、`CommandLine` | 最终验证或必要的任务修正。上述任务数据变化时，宿主会应用修改并重建全部步骤，所以 `command.*` 会再次运行。 |
| `ProcessBeforeStart` / `process.before-start` | 异步；进程 `StartInfo` 已创建但尚未 `Start()` | `ProcessFileName`、`CommandLine` | 替换可执行文件、包装命令或做最后一刻调整。不要修改 `PresetJson` 并期待重建。 |
| `ProcessAfterExit` / `process.after-exit` | 异步；每个进程退出、宿主判断步骤成败之前 | `ExitCode` | 读取真实退出码、清理单步骤文件，或按明确的外部工具协议校正退出码。它不是整个任务完成事件。 |
| `TaskAfterComplete` / `task.after-complete` | 异步且一次性；全部原生步骤成功、任务标记完成之前 | `ReportProgress`、`ReportResult`；路径和预设主要供读取 | VMAF、校验和、输出验证等可取消成功后处理的首选阶段。抛错会让任务转为错误，之后调用 `task.after-failed` 和 `task.after-finish`。插件自行启动的进程不会再次经过 `process.*`。 |
| `TaskAfterFailed` / `task.after-failed` | 异步且一次性；任务已确定错误后 | 主要供读取，可 `ReportResult` | 上报失败诊断、保留或清理插件文件。用户取消不触发。使用不可取消令牌，应快速返回；异常只写终态日志，但会中断本阶段后续处理器。 |
| `TaskAfterFinish` / `task.after-finish` | 异步且一次性；成功、错误或取消的专用阶段之后 | `TaskStatus`、路径供读取，可 `ReportResult` | 无论终态如何都执行的最终清理点，适合释放以 `TaskId` 为键的缓存。使用不可取消令牌，不要执行无限或长时间等待。 |

阶段相邻不代表所有调用构成一条只执行一次的直线。`preset.*` 围绕参数面板，`queue.*` 围绕入队，`task.*` 围绕一次实际执行，而 `command.*` / `process.*` 围绕每个步骤。

## 9. `PluginPipelineContext` 全部字段

宿主为每个处理器创建 SDK 上下文，插件通常不需要自行调用其构造函数。该类保留一个只接收进度回调的构造函数，以及一个同时接收进度和结构化结果回调的构造函数，主要供宿主实现、独立适配器或插件单元测试创建上下文；正常管线回调应直接使用宿主传入的实例。

| 字段 | 含义与写入规则 |
|---|---|
| `StageId` | 当前阶段 ID。宿主在调用前设置，插件只读使用。 |
| `PresetJson` | 完整 v6 预设 JSON。替换时必须保留未知字段及其他插件在 `插件扩展数据` 中的键。纯命令行任务可能为空。 |
| `InputPath` | 当前输入路径。是否会影响任务取决于阶段表。 |
| `OutputPath` | 当前输出路径。修改后应考虑自动命名、扩展名和容器一致性。 |
| `CommandLine` | FFmpeg/ffprobe 参数字符串，不包含 `ProcessFileName`。 |
| `ProcessFileName` | 实际启动的程序名，主要在 `process.before-start` 中有效。 |
| `TaskId` | 实际任务标识；预览和纯 UI 上下文可能为空。用于区分并发任务。 |
| `SurfaceId` | 参数面板实例标识，主要在预设/UI 阶段有意义。 |
| `PhaseName` | 当前命令或进程阶段名称，例如普通单次或二次编码步骤。 |
| `IsPreview` | 当前命令生成是否为预览。预览处理器必须避免外部副作用。 |
| `ExitCode` | 可空退出码；`process.after-exit` 中有实际值，并可覆盖宿主随后判断使用的值。 |
| `TaskStatus` | `Unknown`、`Pending`、`Running`、`Paused`、`Succeeded`、`Failed`、`Canceled`。只读使用；赋值不会改变 3FUI 的真实状态。 |
| `Properties` | 不区分大小写的阶段附加字典；处理器应保留不属于自己的键。 |

当前公开的 `Properties` 键：

| 键 | 可用位置 | 含义 |
|---|---|---|
| `taskName` | `queue.before-add` | 任务显示名称；修改会被宿主读取。 |
| `stepCount` | 任务/进程上下文 | 总步骤数；早期准备阶段可能是 `0`。 |
| `stepIndex` | 有当前步骤时 | 从 `0` 开始的步骤索引。 |
| `stepNumber` | 有当前步骤时 | 从 `1` 开始的步骤序号。 |
| `isFinalStep` | 有当前步骤时 | 小写字符串 `true` / `false`。 |
| `commandStage` | `process.before-start`、`process.after-exit` | 宿主命令步骤枚举名。 |
| `elapsedMilliseconds` | `task.after-complete`、`task.after-failed`、`task.after-finish` | 当前执行经过的毫秒数。 |

插件可以在 `Properties` 中加入自己的临时键，后面的同阶段处理器能看到，但宿主不会持久化未知键，也不保证在另一个阶段重新提供它们。跨阶段数据应放在插件拥有的预设状态，或放在以 `TaskId` 为键的插件内部线程安全缓存中，并在 `task.after-finish` 清理。

## 10. 修改 `PresetJson` 的安全方式

`PresetJson` 是整个 v6 预设，不属于单个插件。插件不得把它替换为只包含自己字段的新对象，也不得删除未知字段。

插件 UI 状态在完整预设中的结构类似：

```json
{
  "插件扩展数据": {
    "com.example.my-plugin": "{\"Version\":1,\"Enabled\":true,\"Value\":\"32\"}"
  }
}
```

外层的 `插件扩展数据` 是“插件 ID → JSON 字符串”字典。插件只解释自己的值。

C# 中可使用 `JsonNode` 保留未知字段：

```csharp
var preset = JsonNode.Parse(context.PresetJson)?.AsObject()
    ?? throw new InvalidOperationException("预设 JSON 无效");

// 示例：写入宿主当前公开预设中的原生质量字段。
preset["视频参数_比特率_控制方式"] = 1;
preset["视频参数_质量控制_参数名"] = "crf";
preset["视频参数_质量控制_值"] = "32";

context.PresetJson = preset.ToJsonString(
    new JsonSerializerOptions { WriteIndented = true });
```

字段名属于当前 v6 预设格式，仍可能随宿主预设版本演进。生产插件应验证字段类型、为缺失字段提供默认值，并保留自己的状态版本号以便迁移。

## 11. 进度与结构化结果

### 11.1 `ReportProgress`

```csharp
context.ReportProgress("正在计算质量指标……", 0.25);
context.ReportProgress("质量指标计算完成", 1);
```

- `fraction` 可省略，范围是 `0` 到 `1`；宿主会限制越界值。
- 在实际任务上下文中，它会更新任务进度并向任务日志写入消息。
- UI、预设和预览上下文可能没有实际任务接收器，此时调用不会产生任务显示。

### 11.2 `ReportResult`

```csharp
context.ReportResult("quality.mean", "96.417", "质量均值");
context.ReportResult("output.bytes", "1234567", "输出大小", "bytes");
```

```vb
context.ReportResult("quality.mean", "96.417", "质量均值")
context.ReportResult("bitrate.average", "1842", "平均码率", "kbps")
```

参数：

| 参数 | 含义 |
|---|---|
| `key` | 插件内部稳定键，不能为空。 |
| `value` | 字符串值。 |
| `displayName` | 可选的人类可读标题。 |
| `unit` | 可选单位。 |

宿主按“插件 ID + `key`”隔离结果。因此两个插件都上报 `quality.mean` 不会冲突；同一插件对相同 `key`再次上报会更新原结果。结果会写入当前任务日志和结果摘要，属于当前一次任务执行，任务重新运行时清空。

当前上下文不提供读取其他插件结果集合的 API。插件之间如需协作，应定义明确的外部协议，不能依赖任务结果摘要作为实时通信总线。

## 12. 成功后处理示例

需要在 FFmpeg 全部原生步骤成功后计算质量分数、校验和或生成报告时，使用`task.after-complete`，不要用 `process.after-exit` 猜测“最后一个进程”。

```csharp
private static async ValueTask VerifyOutputAsync(
    PluginPipelineContext context,
    CancellationToken cancellationToken)
{
    if (!File.Exists(context.OutputPath))
    {
        throw new FileNotFoundException("输出文件不存在", context.OutputPath);
    }

    context.ReportProgress("正在校验输出……", 0.5);
    await using var stream = new FileStream(
        context.OutputPath,
        FileMode.Open,
        FileAccess.Read,
        FileShare.Read,
        128 * 1024,
        FileOptions.Asynchronous | FileOptions.SequentialScan);

    var hash = await SHA256.HashDataAsync(stream, cancellationToken)
        .ConfigureAwait(false);
    context.ReportResult(
        "output.sha256",
        Convert.ToHexString(hash).ToLowerInvariant(),
        "SHA-256");
    context.ReportProgress("输出校验完成", 1);
}
```

后处理必须：

- 把取消令牌传给异步 I/O 和外部进程等待；
- 用户停止任务时结束插件启动的整棵子进程树；
- 使用唯一临时文件名，最好包含插件 ID、`TaskId` 或随机 GUID；
- 在 `Finally` 中清理临时文件；
- 明确失败策略：处理器抛错会让原本编码成功的任务进入错误状态，但已经生成的输出文件不会自动删除。

VB.NET 中调用外部工具并解析 VMAF 的完整实现见[VB.NET 综合示例](../Samples/ThreeFui.PluginApi.VbVmafSample/VbVmafPlugin.Pipeline.vb)。

## 13. 取消、并发与线程安全

### 13.1 取消令牌

- `task.before-prepare`、`task.after-prepare`、`process.before-start`、`process.after-exit` 和
  `task.after-complete` 接收当前任务取消令牌。
- 用户取消后应尽快停止插件的文件、网络和子进程工作。
- `task.after-failed` 和 `task.after-finish` 为保证清理而接收不可取消令牌；它们必须只做有界工作。
- 用户取消不会触发 `task.after-failed`，但会触发 `task.after-finish`，此时 `TaskStatus=Canceled`。

### 13.2 同一任务与不同任务

- 同一任务、同一阶段的插件处理器串行执行。
- 多个编码任务可以并行，所以同一个插件回调可能同时处理不同 `TaskId`。
- 不要用未加锁的全局字段保存“当前文件”“当前进程”或“当前分数”。
- 推荐使用 `ConcurrentDictionary<string, TaskState>`，以 `TaskId` 为键，并在 `task.after-finish` 删除。
- 预览可能没有 `TaskId`，预览处理器不应创建必须依赖任务终态清理的长期状态。

### 13.3 UI 线程

UI 控件工厂和 UI 事件运行在宿主 UI 线程。任务/进程处理器不保证在 UI 线程；如果必须更新插件控件，应使用该控件的 `InvokeRequired` / `BeginInvoke` 切回创建线程。不要在 UI 事件中同步等待异步任务。

## 14. 多插件冲突与规避

SDK 能提供确定的顺序和结果命名空间，但不能自动解决所有外部冲突。

| 冲突来源 | 当前行为 | 建议 |
|---|---|---|
| 多插件修改同一上下文字段 | 后执行者看到并可覆盖前执行者的值 | 只修改自己负责的内容，保留未知 JSON 和字典键；必要时公开约定 `Order`。 |
| 相同结果 `key` | 不同插件按插件 ID 隔离 | 使用稳定、语义清晰的插件内部 key。 |
| 处理器抛错 | 当前阶段剩余处理器跳过 | 捕获可恢复异常，终态清理保持简单；不要依赖后续插件一定运行。 |
| 处理器永久等待 | 后续处理器和任务清理被阻塞 | 对网络、子进程和锁设置超时；终态阶段禁止无限等待。 |
| 相同临时文件或输出文件 | SDK 不做文件级隔离 | 使用插件 ID + `TaskId` + GUID 命名；必要时使用文件锁或原子替换。 |
| 插件全局可变状态 | 不同任务可能并发读写 | 使用线程安全集合和每任务状态。 |

插件与 3FUI 在同一进程、同一用户权限下运行，不是安全沙箱。只安装可信插件；插件崩溃、死锁或修改共享文件都有可能影响宿主。

## 15. 选择阶段的快速规则

- 只保存 UI 设置：使用 `StateJson`，通常不需要处理器。
- 加载旧预设前迁移字段：`preset.before-apply`。
- 原生控件完成捕获后最终修正预设：`preset.after-capture`。
- 每个文件入队时快速改任务名或任务快照：`queue.before-add`。
- 需要媒体探测、网络查询或外部工具后再决定编码参数：`task.before-prepare`。
- 需要结构化调整当前步骤参数：`command.before-build`。
- 只能修改最终参数字符串：`command.after-build`。
- 全部步骤生成后必须做最终验证：`task.after-prepare`。
- 必须替换实际启动程序：`process.before-start`。
- 必须观察或校正真实进程退出码：`process.after-exit`。
- 全部编码步骤成功后做耗时评测或校验：`task.after-complete`。
- 只处理错误任务：`task.after-failed`。
- 无论成功、错误还是取消都释放任务缓存：`task.after-finish`。

## 16. 构建、安装和调试

### 16.1 构建

```powershell
dotnet restore .\MyCompany.MyPlugin.csproj
dotnet build .\MyCompany.MyPlugin.csproj -c Debug --no-restore
```

VB.NET 项目把扩展名改为 `.vbproj`。

### 16.2 安装

1. 完全退出 3FUI。
2. 确认程序根目录存在匹配版本的 `FFmpegFreeUI.PluginHost.dll` 和
   `FFmpegFreeUI.PluginSdk.dll`。
3. 只把 `MyCompany.MyPlugin.3fui.dll` 和插件独有依赖复制到 `Plugin`。
4. 重新启动 3FUI。

不要把示例或插件输出目录中的 SDK 再复制一份到 `Plugin`。

### 16.3 调试

推荐在 IDE 中将启动程序设置为 `FFmpegFreeUI.exe`，在下面位置设置断点：

- 插件 `Initialize`；
- UI 控件工厂；
- `StateRestored`；
- 各管线回调；
- 外部进程启动和取消处理。

`host.Log` 当前通过 `Debug.WriteLine` 输出，可在调试器“输出”窗口查看。任务阶段的
`ReportProgress` / `ReportResult` 可在编码队列任务日志中查看。

发布包应说明：

- 所需 3FUI 和 Plugin API 版本；
- 安装及卸载方法；
- 插件自己的托管和原生依赖；
- 是否启动网络请求或外部程序；
- 临时文件和隐私策略；
- 插件以及第三方库各自适用的许可证。

## 17. 常见问题

### 插件完全没有加载

依次检查：

1. 文件名是否以 `.3fui.dll` 结尾；
2. DLL 是否位于程序根目录下的 `Plugin`；
3. 根目录是否同时存在兼容的 PluginHost 和 SDK；
4. 入口是否公开、非抽象、实现 `IThreeFuiPlugin` 并有公共无参构造函数；
5. 插件 ID 是否为空或与其他插件重复；
6. 插件自己的依赖是否齐全；
7. `Initialize` 是否抛出异常。

### 没有 PluginSdk 时为什么不报错

这是预期行为。3FUI 在加载插件程序集之前读取其程序集引用表；发现插件依赖 SDK 而 v2 宿主不可用时，
直接跳过该插件，避免触发类型加载错误。

### UI 注册成功但页面没有出现控件

- 确认注册的是当前宿主提供的锚点；
- 打开“参数面板 → 视频参数｜质量”让相应界面实例创建；
- 插入型工厂必须返回一个新控件；
- 装饰型工厂必须返回空，它不会自动显示新的一行；
- 检查工厂是否抛出异常；
- 不要复用已经属于另一个父容器的控件实例。

### 为什么 `preset.before-capture` 写入的质量值消失了

该阶段之后宿主才从原生控件捕获质量等字段，因此会被控件当前值覆盖。改为直接协调公开的原生控件，或在`preset.after-capture` 对完整预设做最终修正。

### 为什么 `preset.after-apply` 修改 JSON 后界面没变化

原生映射已经结束，不会自动执行第二遍。需要改变加载到原生控件的字段时使用`preset.before-apply`；需要恢复插件控件时监听 `StateRestored`。

### 为什么同步阶段提示处理器不能异步等待

预设、入队和命令构建阶段要求返回已经完成的 `ValueTask`。把耗时或异步工作移到`task.before-prepare`、`task.after-prepare` 或其他异步任务阶段。

### 为什么 `command.*` 执行很多次

参数预览、总览、任务准备、任务重建、ffprobe 和二次编码都会生成命令。检查 `IsPreview` 和`PhaseName`，保持修改幂等，不要在这些阶段执行一次性外部副作用。

### 编码完成后计算 VMAF 或校验和用哪个阶段

使用 `task.after-complete`。它在全部原生步骤成功后调用一次，并在后处理成功之前不把任务标记为完成。使用 `ReportProgress` 展示进度，使用 `ReportResult` 发布最终分数。

### 多个 `task.after-finish` 插件会并发吗

对同一任务不会，它们按 `Order`、插件 ID、处理器 ID 串行执行。但是一个插件异常会中止该阶段剩余插件，一个插件长时间等待也会阻塞后续插件。不同编码任务仍可能并行执行同一处理器。

### 修改 `TaskStatus` 能改变任务结果吗

不能。该属性只用于读取宿主状态，宿主不会把插件赋值复制回真实任务。需要让成功后处理失败时，将异常抛出；需要校正单个外部进程结果时，只能在有明确协议的情况下修改 `process.after-exit` 的 `ExitCode`。

### 能否读取其他插件通过 `ReportResult` 发布的结果

当前不能。结果按插件 ID 隔离并供任务日志/摘要展示，管线上下文没有暴露结果集合。

### 插件可以用 VB.NET 编写吗

可以。任何能够生成兼容 .NET 程序集并实现 SDK 接口的语言原则上都可用。VB.NET 的主要差异是异步`ValueTask` 回调需要使用本指南中的 `Task` 适配器。

## 18. 公共类型速查

| 类型 | 用途 |
|---|---|
| `IThreeFuiPlugin` | 插件入口：`Id`、`DisplayName`、`Initialize`。 |
| `IThreeFuiHost` | 版本、日志、UI 注册表和处理链注册表。 |
| `PluginLogLevel` | `Trace`、`Information`、`Warning`、`Error`。 |
| `IPluginUiRegistry` | `AvailableAnchors`、`Register`。 |
| `PluginUiExtension` | UI 扩展 ID、锚点、顺序和控件工厂。 |
| `IPluginUiContext` | UI 身份、原生控件、插入容器、状态持久化和刷新。 |
| `IPluginPipelineRegistry` | `AvailableStages`、`Register`。 |
| `PluginPipelineHandler` | 处理器 ID、阶段、顺序和回调。 |
| `PluginPipelineCallback` | 返回 `ValueTask` 的处理器委托。 |
| `PluginPipelineContext` | 预设、路径、命令、进程、任务、属性、进度和结果。 |
| `PluginPipelineProgress` | 宿主内部传递进度消息和比例的记录类型。 |
| `PluginTaskResult` | 宿主内部传递结构化结果的记录类型。 |
| `PluginTaskStatus` | `Unknown`、`Pending`、`Running`、`Paused`、`Succeeded`、`Failed`、`Canceled`。 |
| `ThreeFuiPluginApi` | 当前 SDK 声明版本 `Version`。 |
| `ThreeFuiUiAnchors` | 6 个稳定 UI 锚点和 `All` 集合。 |
| `ThreeFuiPipelineStages` | 14 个稳定处理阶段和 `All` 集合。 |

建议先编译并运行仓库中的 C# 或 VB.NET 综合示例，再从最接近自己用途的 UI 扩展和处理阶段逐步删减，这样最容易保持正确的生命周期、取消和并发行为。
