# UI 与 HDR VMAF 定向验证

这是无额外测试框架依赖的 Windows/.NET 10 控制台入口，直接引用主项目；失败返回非零退出码。仅覆盖当前 PR 保留的 UI/VMAF 改动，不代表整个应用全量回归。

## 十项定向检查

在仓库根目录运行：

```bash
dotnet run --project Tests/FFmpegFreeUI.RegressionTests/FFmpegFreeUI.RegressionTests.csproj --configuration Release -- E:\Code\CS\FFmpegFreeUI
```

覆盖公共选中色和主题往返、普通标题按钮与关闭按钮、LakeUI 输入弹窗、许可证浅紫/对比度、HDR 描述顺序、十二行配置、模型分类、官方八项 HDR 目录、未检测项隔离、AUTO 矩阵与回退、模型刷新状态及二进制前缀。

输入参数为当前仓库绝对路径。结果和最小二进制扫描样例位于 `artifacts/ui-vmaf-20261028/tests/<时间>/`；不读取或修改用户安装目录的配置。此前的技能、文件安装、编码预设、权限和批次并行测试已从该入口移除。

## 实际质量评测页面

```bash
dotnet run --project Tests/FFmpegFreeUI.RegressionTests/FFmpegFreeUI.RegressionTests.csproj --configuration Release -- --preview-vmaf E:\Code\CS\FFmpegFreeUI D:\DevTools\Cli\FFmpeg\ffmpeg.exe
```

独立进程运行主项目真实质量评测窗体，使用临时设置和指定 FFmpeg，不操作用户实例。最后附加 `dark` 检查深色。入口选中并展开 HDR 项，单独捕获下拉顶层窗口为 `dropdown.png`，在 `models.json` / `display.json` 记录目录、检测状态、DPI、尺寸和十二行高度，完成后自动关闭。未展开、六列宽度不足或行数不符均不算通过。

## 许可证页面

```bash
dotnet run --project Tests/FFmpegFreeUI.RegressionTests/FFmpegFreeUI.RegressionTests.csproj --configuration Release -- --preview-license E:\Code\CS\FFmpegFreeUI
```

结果位于同一工作根的 `ui-license/<时间>/`，包含浅色页面截图与实际颜色；不点击购买或其他外链。

逻辑检查、页面显示、不同系统显示缩放和真实 HDR 评分是不同验证。目录显示及检测到模型名称都不等于 FFmpeg 已支持新 HDR 模型；评分能力必须用正确引擎和真实媒体单独确认。
