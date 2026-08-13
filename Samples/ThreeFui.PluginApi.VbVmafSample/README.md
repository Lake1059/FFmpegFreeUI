# VB.NET Plugin API v2.1 综合示例

本示例用三个可以独立启用的场景覆盖当前全部公共接口：

1. 自动质量策略：在参数面板中保存 CRF，在预设捕获和任务准备阶段写入结构化预设。
2. 命令与进程审计：修改任务名、输出后缀、进阶参数、最终命令和实际启动程序，并观察退出码。
3. VMAF 后处理：全部原生步骤成功后异步运行环境中的 `ffmpeg`/`libvmaf`，再用 `ReportResult`
   发布结果；任务结束时释放按 `TaskId` 保存的并发缓存。

所有会改变任务或执行耗时工作的选项默认关闭。代码按职责拆成：

- `VbVmafPlugin.vb`：入口、版本/能力检测、所有注册、状态模型和并发任务缓存。
- `VbVmafPlugin.Ui.vb`：3 个装饰型锚点和 3 个插入型锚点。
- `VbVmafPlugin.Pipeline.vb`：14 个处理阶段，按真实调用顺序排列，以及可实际运行的 VMAF 代码。

## 接口覆盖

- `IThreeFuiPlugin`：`Id`、`DisplayName`、`Initialize`。
- `IThreeFuiHost`：`ApiVersion`、`HostVersion`、`Ui`、`Pipeline`、4 种 `Log` 级别。
- `IPluginUiRegistry`：`AvailableAnchors`、`Register`、注册句柄保存。
- `IPluginUiContext`：全部身份字段、两个控件字段、`GetAnchorControl`、`StateJson`、
  `StateRestored`、`RequestParameterRefresh`。
- `IPluginPipelineRegistry`：`AvailableStages`、`Register` 和 `Order`。
- `PluginPipelineContext`：全部字段、阶段属性、取消令牌、`ReportProgress`、`ReportResult`。
- `ThreeFuiUiAnchors.All` 中的 6 个锚点。
- `ThreeFuiPipelineStages.All` 中的 14 个阶段。

示例中的命令字符串修改只用于解释接口。生产插件应使用可靠的参数解析/转义方式，并谨慎提供“接受非零
退出码”或“替换进程”这类高风险选项。VMAF 示例假设参考和编码文件已有可比较的尺寸、帧率、时长、
色彩和时间轴，生产插件需要明确自己的缩放、裁剪、色彩转换、帧同步和模型策略。

## 构建和安装

在仓库根目录执行：

```powershell
dotnet build .\Samples\ThreeFui.PluginApi.VbVmafSample\ThreeFui.PluginApi.VbVmafSample.vbproj -c Release
```

只把生成的 `ThreeFui.PluginApi.VbVmafSample.3fui.dll` 放到 3FUI 的 `Plugin` 目录。不要复制构建目录中的
`FFmpegFreeUI.PluginSdk.dll`；SDK 和 PluginHost 应由 3FUI 发行包统一放在程序根目录。
