# UI 与 HDR VMAF 定向验证

这是无额外测试框架依赖的 Windows/.NET 10 控制台入口，直接引用主项目；失败返回非零退出码。仅覆盖当前 PR 保留的 UI/VMAF 改动，不代表整个应用全量回归。

## 十一项定向检查

在仓库根目录运行：

```bash
dotnet run --project Tests/FFmpegFreeUI.RegressionTests/FFmpegFreeUI.RegressionTests.csproj --configuration Release -- E:\Code\CS\FFmpegFreeUI
```

覆盖公共选中色和主题往返、普通标题按钮与关闭按钮、LakeUI 输入弹窗、许可证浅紫/对比度、HDR 描述顺序与支持说明、十二行配置、模型分类、官方八项 HDR 目录、未检测项隔离、AUTO 矩阵与回退、模型刷新状态及二进制前缀。

本轮新增模型界面检查：四项旧 V0/NEG 提示、底部非 NEG 警示行及四项顺序、分组三列合并宽度、标题不可选、滚轮跨标题、本地项插入/刷新后的索引与分组、AUTO 排除非 NEG、未检测模型/缺 CUDA 滤镜拦截，以及浅/深色各十种胶囊的独立配色与至少 4.5:1 文字对比度。观看距离按 1.5H、3H、5H 使用由深到浅的紫色。

输入参数为当前仓库绝对路径。结果和最小二进制扫描样例位于 `artifacts/ui-vmaf-20261029/tests/<时间>/`；不读取或修改用户安装目录的配置。此前的技能、文件安装、编码预设、权限和批次并行测试已从该入口移除。

## 胶囊微调定向入口

只检查模型界面这一组，不重跑无关模块：

```bash
dotnet run --project Tests/FFmpegFreeUI.RegressionTests/FFmpegFreeUI.RegressionTests.csproj --configuration Release -- --capsule-only E:\Code\CS\FFmpegFreeUI
```

实测窄窗口，最后加 `dark` 检查深色：

```bash
dotnet run --project Tests/FFmpegFreeUI.RegressionTests/FFmpegFreeUI.RegressionTests.csproj --configuration Release -- --preview-capsules E:\Code\CS\FFmpegFreeUI D:\DevTools\Cli\FFmpeg\ffmpeg.exe
```

结果位于 `capsule-tests/`、`capsule-preview/`。预览按真实 Dock 布局缩小窗口，检查 370/373/405 逻辑像素宽的模型框，并从实际截图确认 CUDA 胶囊仍然出现；同时检查 SubSample/标题对齐、Pooling 保持原宽、32 行高、22 胶囊高度及三档紫色。非 NEG 胶囊仍保持原 160 逻辑像素宽，本轮通过缩小 SubSample 给模型框让出空间，不采用缩窄非 NEG 的方案。

## 实际质量评测页面

```bash
dotnet run --project Tests/FFmpegFreeUI.RegressionTests/FFmpegFreeUI.RegressionTests.csproj --configuration Release -- --preview-vmaf E:\Code\CS\FFmpegFreeUI D:\DevTools\Cli\FFmpeg\ffmpeg.exe
```

独立进程运行主项目真实质量评测窗体，使用临时设置和指定 FFmpeg，不操作用户实例。最后附加 `dark` 检查深色。结果位于 `artifacts/ui-vmaf-20261029/ui-catalog/<时间>/`，完成后自动关闭。

- `dropdown.png`、`dropdown-legacy-nonneg.png`：分别捕获下拉顶层窗口的 HDR 区和底部分组，显示彩色胶囊、旧 V0 四项、警示行与非 NEG 四项。
- `models.json`、`display.json`：目录、检测状态、CUDA 滤镜可用性、分组标记、DPI、尺寸和十二行高度。未展开、六列宽度不足或行数不符均不算通过。
- `tooltips.json` 与代表项 PNG：通过 MouseMove 事件逐项生成提示，检查固定像素宽度、可见文字区域、滚轮关闭后重建、左右翻转及底部定位；同时验证按键跳过警示行和鼠标点击警示行不会改变选中模型。记录主窗口可见边界与显示器工作区，覆盖靠右翻左、靠左翻右、偏好左侧、右侧恰好能放下四种情况，逐项确认提示同时位于窗口与屏幕内。`window-right-edge.png`、`right-fits.png` 仅组合本次隔离窗口及其弹层，展示窗口未铺满显示器时的实际效果。

HDR 简介在原分类说明后补充“支持 HDR”，HFR/H 术语段落仍与 SDR 相同；旧 V0/NEG 提示统一为“旧模型，不推荐”。所有提示固定为 320 逻辑像素（150% 下 480 像素），不随文案长短缩宽，高度按换行计算。优先约束在主窗口客户区与显示器工作区的交集中，右侧空间不足时翻到左侧；主窗口小到无法容纳提示时退回显示器工作区边界。此适配局限于 VMAF 控件，复用 LakeUI 5.116 原提示窗；升级库或覆盖本地 DLL 时需重新运行预览。

## 许可证页面

```bash
dotnet run --project Tests/FFmpegFreeUI.RegressionTests/FFmpegFreeUI.RegressionTests.csproj --configuration Release -- --preview-license E:\Code\CS\FFmpegFreeUI
```

许可证预览仍保存到历史工作根 `artifacts/ui-vmaf-20261028/ui-license/<时间>/`，包含浅色页面截图与实际颜色；不点击购买或其他外链。本轮未修改许可证/公共主题源码，可复用对应原始记录，不将其称为本轮重新打开页面。

逻辑检查、页面显示、不同系统显示缩放和真实评分是不同验证。目录显示及检测到模型名称都不等于 FFmpeg 已支持相应模型；评分能力必须用正确引擎和真实媒体单独确认。
