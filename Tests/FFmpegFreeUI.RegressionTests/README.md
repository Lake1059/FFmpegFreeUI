# UI、VMAF 与 Agent 定向回归

这是无额外测试框架依赖的 Windows/.NET 10 控制台检查入口，直接调用主项目，失败返回非零退出码。只覆盖本次改动，不作为全量回归证明。

在仓库根目录运行：

```bash
dotnet run --project Tests/FFmpegFreeUI.RegressionTests/FFmpegFreeUI.RegressionTests.csproj --configuration Release -- E:\Code\CS\FFmpegFreeUI
```

也可把最后一个参数换成当前仓库的绝对路径。再增加 FFmpeg 可执行文件的绝对路径，即会额外运行三张图片的真实 AVIF 队列，并记录同时运行的 FFmpeg 进程、每个任务实际执行命令、输出及临时上限清理结果：

```bash
dotnet run --project Tests/FFmpegFreeUI.RegressionTests/FFmpegFreeUI.RegressionTests.csproj --configuration Release -- E:\Code\CS\FFmpegFreeUI D:\DevTools\Cli\FFmpeg\ffmpeg.exe
```

测试结果、预设生成命令及隔离输入保存在仓库 `artifacts/ui-vmaf-agent-20261028/tests/<时间>/`；测试不会读写用户已安装程序的 Settings、Agent 或预设目录。

覆盖公共选中色、标题栏颜色、LakeUI 自带输入弹窗选中色及不二次转换背景、HDR 模型解析、AUTO 分流与回退、颜色标记检查、扫描前缀、Agent 安装/用户覆盖/动态技能/路径边界、官方八项 HDR 目录在旧引擎下仍可见、未检测项不进入 AUTO 及手动评测前提示、技能不分发 VPY、七份完整预设及 AVIF 临时并行与权限。目录链接测试在系统不允许创建符号链接时记为 skipped，而不是通过。

## 实际质量评测页面预览

以下入口在独立进程中显示主项目的真实质量评测窗体，使用临时设置和指定 FFmpeg，不操作用户正在运行的主程序。默认展开并选中 HDR 4K/HFR 项，模型目录与检测状态记录在 `artifacts/ui-vmaf-agent-20261028/ui-catalog/<时间>/models.json`。下拉层是独立顶层窗口，因此入口单独捕获它为 `dropdown.png`，并在 `display.json` 记录真实展开状态、窗口尺寸和 DPI；检查结束自动关闭。只显示选择框、未展开或六列宽度不足时不能视为验证通过。

```bash
dotnet run --project Tests/FFmpegFreeUI.RegressionTests/FFmpegFreeUI.RegressionTests.csproj --configuration Release -- --preview-vmaf E:\Code\CS\FFmpegFreeUI D:\DevTools\Cli\FFmpeg\ffmpeg.exe
```

最后附加 `dark` 可预览深色主题。该入口显示模型目录不等于真实 HDR 评分成功。

## 许可证页面与 CUDA 资料

使用 `--preview-license <仓库绝对路径>` 可独立显示并捕获浅色许可证页，结果位于 `ui-license/<时间>/`。模型预览同时核对 `MaxDropDownItems=12` 及实际十二行高度。新增定向检查覆盖 HDR 描述顺序、浅紫按钮/文字对比度与主题恢复，以及完整 CUDA 技能入口和十二项滤镜覆盖；包含真实 AVIF 队列时共 21 项。

`AgentDefaults/skills/nvidia-video/references/cuda-filters.md` 提供本次所测构建全部十二项 CUDA 滤镜的功能、参数、示例和限制。静态技能检查不是自然语言模型验收，也不意味着全部滤镜已实机运行；CUDA 旋转短样本及其 GPU 命令/输出对比另留原始证据。

逻辑回归不是 GUI 截图或真实 HDR 评分验证。实际窗口交互、100%/150% DPI、GPU 编解码、FFmpeg HDR 颜色传递及模型端点行为仍需分别记录证据。当前系统 FFmpeg 是否支持新模型，必须用真实运行确认，不能由这些逻辑检查代替。
