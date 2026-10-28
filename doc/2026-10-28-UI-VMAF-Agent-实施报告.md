# UI、HDR VMAF 与 Agent 技能实施报告

日期：2026-10-28。基于 `main` / `becda5b` 开发。用户已审查前版并授权补充修改后以中文提交 PR；不启用自动合并，不发布 GitHub Release。PR 标题与正文见 `2026-10-28-PR-说明草稿.md`，实际提交/PR 状态以仓库和会话最终链接为准。

## 交付结论

完成公共主题修复、HDR 官方目录与运行库检测分离、AUTO 分流、可编辑 Agent 与三项技能。按后续意见补上 HDR 说明、将下拉缩为十二行、调整许可证浅紫配色，并扩充全部本机构建 CUDA 滤镜的参考与强制使用规则；本次 VPY 方案不再分发。

**八项 HDR 目录已恢复显示，但目录存在不等于当前引擎能执行。实测 FFmpeg 仍不能加载新 HDR 模型，真实 HDR 评分未跑通。** 本次不修改/替换 FFmpeg，不把 SDR 回退结果当成 HDR 成功。

### 产物

- `release/FFmpegFreeUI.exe`：50,378,171 字节，约 48.0 MiB，Windows x64，依赖 **.NET 10 Desktop Runtime**。
- 项目版本仍为 `6.2.40`，未擅自增加正式发行版本号。
- `release/Agent`：25 个默认文件、四个技能目录、七份参考预设，新增完整 CUDA 滤镜参考。
- 另含项目许可、LakeUI GPL-3.0 全文、第三方说明及 `SHA256SUMS.json`。

程序 SHA-256：

```text
44E402BBAA0D3EB7BD26E58D0DA82C6A0D1DF03AB9025571C20EFFEC48F7F0D1
```

本版本替代前几次阶段包。交付清单只列本次 29 个 exe/默认资料/许可文件；用户后续修改 Agent 时，其文件摘要相应变化，不代表程序损坏。

## 逐项实现

| 项目 | 实现与边界 |
| --- | --- |
| 标题栏 | 浅色普通按钮悬停保持深色图标、背景变深；深色保留变浅反馈，关闭按钮保持红底白图标。 |
| 统一选中 | 文本、下拉、列表、详情行、动态编辑框、导航及 LakeUI 输入弹窗共用选中语义：浅色 `#D6DEE6`、深色 `#565656`。不被毛玻璃低透明度削弱，弹窗不重复转换背景。 |
| 许可证配色 | 仅调整许可证页面浅色主题：购买按钮正常/悬停/按下分别为 `#E2DAFA` / `#D6CBF5` / `#CBBCF1`，深色文字；编号使用 RGB 128,99,180。深色主题原配色可恢复。 |
| 模型提示/行数 | 描述顺序补为 V1/V0、4K、距离、HFR、HDR、CUDA；模型下拉最多十二行，保留滚动，不改其余页面布局。 |
| HDR 胶囊 | 固定列为版本 / 4K / 距离 / HFR / HDR / CUDA，CUDA 右移；官方八项普通/HFR HDR 目录始终显示。 |
| 目录/检测 | `IsRuntimeDetected` 区分名称探测与目录补充。未检测项提示所需引擎，AUTO 不选用，手动评测前明确说明；本地 JSON 仍可手选。名称探测本身不代表完整评分链已验证。 |
| AUTO | 默认 AUTO，按 PQ/HLG 识别 HDR，不只看位深/BT.2020。优先运行库已检测的同档 HDR，缺失回退 SDR 并警告；沿用长边 ≥3840、≥48 fps 优先 HFR、4K 1.5H/其他 3H、版本/NEG 规则。 |
| 刷新 | 新运行库扫描结果与目录去重；更换 FFmpeg 后不保留已消失内置模型/CUDA 项的旧检测状态，保留手选本地模型。 |
| Agent 文件 | 静态提示词位于 exe 旁 `Agent/agents.md`，技能在 `Agent/skills/`。只补缺失，已有修改/空文件不覆盖；新请求和技能读取使用当前文件。权限/联网事实动态追加，工具端校验不变。 |
| NVIDIA | 五份 NVENC 预设，RTX 40+ 默认优先 NVENC，尊重软编码目标，分别判断 NVDEC 与 NVENC，提供不兼容输入的 CPU 解码＋NVENC 回退。 |
| AVIF | 仅明确指定 AVIF 时触发，单任务单线程，批次按 CPU 和图片数实际并行；不改全局并发，不放大视频任务，结束清理临时上限。 |
| 音频 | 独立音频 NMR AAC：q=1.3、SoXR 48 kHz/precision 28、双声道、封面与元数据；核对实际 coder。视频音轨默认 Opus。 |
| 删除 VPY | 移除本次 VPY 方案/参考/技能引用，省略七份保留 JSON 的关闭/空值字段；不拆除应用既有帧服务器功能，不删除用户自有脚本或原预设输入。 |

## CUDA 滤镜技能增强

`nvidia-video/references/cuda-filters.md` 覆盖所测构建中全部十二项 CUDA 滤镜，逐项提供功能、选项、用法及限制：

- `transpose_cuda`：90°、扩展 180°/镜像模式及构建差异。
- `scale_cuda`：尺寸、插值、兼容格式、范围/矩阵扩展及非 tone mapping 边界。
- `bilateral_cuda`：双边降噪强度、窗口和格式/性能限制。
- `yadif_cuda` / `bwdif_cuda`：反交错、场序、每帧/每场输出。
- `chromakey_cuda` / `overlay_cuda`：色键、alpha、多输入、同设备及帧同步。
- `pad_cuda`：画布、位置、边框色、比例和尺寸约束。
- `colorspace_cuda`：当前仅暴露输出范围，不冒充完整 HDR 色彩管理。
- `thumbnail_cuda`：批次代表帧，不冒充最终 PNG/JPEG 编码全 CUDA。
- `hwupload_cuda`：上传、设备和混合流程边界。
- `libvmaf_cuda`：双路评分、日志/模型参数及 V1/HDR 能力限制。

另说明 `hwdownload`、`hwmap` 和未被当前构建列出的可选 NPP；不编造 `rotate_cuda` 等不存在的名称，不把其他 GPU API 叫作 CUDA。

用户明确要求全 CUDA 时，**可由当前 CUDA 滤镜实现的画面处理必须使用 CUDA**；清除面板同一操作产生的 CPU 重复滤镜，保留其他操作顺序，核对应用实际生成命令。NVDEC 不兼容或滤镜缺失时先说明限制，用户同意后才放宽为混合流程，不静默降级或更改角度。

本轮逐项保存了实际帮助，不能据此声称全部滤镜都已实机验收。SKILL 与兼容性入口指向完整参考。

## Agent 编辑与安全规则

四个目录为 `ffmpegfreeui`、`nvidia-video`、`avif-aom`、`audio-nmr-aac`。五份 NVENC 参考是 AV1/AV1-Fast/HEVC/HEVC-Fast/HEVC-LowRate，另有 AOM-AVIF 和 NMR-AAC。新 CUDA 参考独立保存，按需读取。

安装根是 exe 目录而非当前工作目录。缺失文件采用原子写入，用户修改及空文件不覆盖，支持自定义技能发现，旧 list/read 工具与别名继续兼容。读取拒绝穿越、绝对路径、尾点/尾空格、设备名、链接及过大文本。

此次本地交付只更新经旧默认摘要确认未修改的资料，并新增 CUDA 参考；已有其他安装不会被升级强制覆盖。用户修改过的文件不能为追求新默认而擅自清空。

## 验证结果

### 21 项定向检查全部通过

最终记录：`tests/20261028-144305-839/results.json`。覆盖主题往返、弹窗、许可证浅紫/对比度、HDR 提示顺序、十二行配置、CUDA 技能、HDR 目录/AUTO/刷新、本地模型、Agent 安装/用户覆盖/路径/链接/权限、无 VPY、七份命令及并行隔离。

真实三张 AVIF 队列再次完成，观察到三个不同 FFmpeg 进程重叠，临时上限归零，全局设置不变；实际命令/进程采样在同目录 `real-avif-queue/queue-smoke.json`。

复现入口：`Tests/FFmpegFreeUI.RegressionTests/README.md`。这是定向覆盖，不是全量回归；静态技能检查不等于自然语言模型端点验收。

### 实际页面与十二行下拉

独立进程运行主项目真实窗体，不操作用户实例，在 **150% / 144 DPI** 检查：

- 浅色十二行：`ui-catalog/20261028-141802-024/`。
- 深色十二行：`ui-catalog/20261028-141804-566/`。
- 浅紫许可证：`ui-license/20261028-141806-980/`。

模型目录有八项 HDR，当前均明确为未检测；单独捕获下拉顶层窗口并核对实际十二行高度、HDR/CUDA 对齐、选中颜色。许可证截图与实际颜色记录保留，未点击购买/外链按钮。深色恢复和按钮对比度另由控件测试验证。

早期仅截主窗体或过窄夹具的抓取不冒充最终证据。未改用户系统缩放，未执行 100% 系统缩放实际复测；未调用用户模型端点，不宣称覆盖用户全部媒体场景。

### CUDA 旋转真实短样本

`cuda-rotation/20261028-142018-653/results.json`：

- CUDA 解码 → `transpose_cuda=dir=clock` → `scale_cuda=format=p010le` → `av1_nvenc` 成功。
- 输入 320×180，输出 180×320、AV1、`yuv420p10le`。
- 与顺时针 CPU 参考比较 SSIM **0.987045**；样例生成与参考比较单独执行，不属于被测全 CUDA 转码链。
- 实际命令、原始日志、输入输出保留。该样例验证旋转路径，不代表所有 CUDA 滤镜、任意格式或全部 GPU 已验收。

参数来源：`cuda-reference/20261028-140858-875/`，含 FFmpeg 摘要、完整滤镜清单及逐项帮助。

### 历史媒体结果复用

`media/20261028-014526-968/`：RTX 4060 Laptop / 驱动 621.01 / FFmpeg `git-2026-10-24-d8990137ee`。H.264 4:2:2 10-bit、AV1 4:2:2 强制硬解不支持，CPU＋NVENC 回退成功；兼容输入全 CUDA 视频链成功；SDR 同画面 VMAF 为 100；NMR AAC 的 48 kHz、双声道、封面和歌词标签核对成功。HDR 模型加载失败单独记录，不计成功。

这些旧结果按未变的处理链复用，不能说成最终 exe 重跑。七份保留预设的有效生成命令未改变；本轮 AVIF 与 CUDA 旋转另有新记录。

## HDR 真实评分的依赖限制

此前实测 FFmpeg 返回：

```text
could not load libvmaf model with version: hdr_vmaf_v1.1.8_3d0h
```

此前核对的 FFmpeg 集成尚未向 VmafPicture 传递完整颜色属性；官方 HDR 模型还需要完整颜色信息和对应 zimg 转换。仅复制 JSON 或更换模型名不足以保证正确评分。报告只记录测得构建，不保证后续所有版本状态相同。

应用目录保持可见、AUTO 排除未检测项、本地 JSON 入口保留；名称探测也不等于全链路成功。真实 HDR 评分仍需具备相应模型及正确颜色传递的 FFmpeg/libvmaf 验收，本次不改另一个仓库、不暗中 tone map、不虚构颜色标记。

## 构建、清单与数据保护

Windows PowerShell 7、.NET SDK 10.0.403；保留相邻 LakeUI DLL 覆盖，缺失时固定回退 NuGet `LakeUI 5.116`，其他固定依赖未升级。锁定的外部源码仅用于查阅 API/许可，不冒充编译输入。

```bash
dotnet publish FFmpegFreeUI/FFmpegFreeUI.vbproj -c Release -r win-x64 --self-contained false -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true -o E:\Code\CS\FFmpegFreeUI\release -bl:E:\Code\CS\FFmpegFreeUI\artifacts\ui-vmaf-agent-20261028\logs\release-publish-final-pr.binlog --nologo -v:minimal
```

主要证据：`delivery-pr.json`、`source-manifest.json`、`release/SHA256SUMS.json`、`logs/release-publish-final-pr.binlog`。`pr-verification.json` 复核十二行实际高度、许可证颜色和 CUDA 旋转。最终仅清理嵌入资料末尾多余空行后重新执行 21 项并发布，`final-pr-verification.json` 对应最新 exe、338 个源码文件、29 个交付文件及最终测试；七份预设命令仍逐项一致，旧清单/日志保留。上一版清单另存 `*-before-pr.json`，旧发布/验证记录原样保留；最初八份输入预设摘要是历史导出证据，不代表最终仍分发八份。

除显式 release 路径外，本文证据相对路径均位于 `artifacts/ui-vmaf-agent-20261028/`。测试 README 的最终说明不改变被测逻辑，编译后若有纯说明更新，清单在交付核对时另记录，不冒充重新测试。

用户 Settings、Preset_v6、Updater、会话与配置不读取内容、不覆盖、不纳入 PR/交付清单。移除 VPY 前仅按已知默认摘要处理本次副本，用户外部文件和历史证据保留。临时预览自动关闭，保留复现必需的日志、样例、最终截图与依赖缓存，不擅自清理其他项目或用户数据。

## 主要依据

- [Netflix VMAF v1/HDR 与颜色转换](https://github.com/Netflix/vmaf/blob/master/resource/doc/models_v1.md)
- [HDR 普通模型](https://github.com/Netflix/vmaf/tree/master/model/hdr_vmaf_v1.1.8) / [HDR HFR 模型](https://github.com/Netflix/vmaf/tree/master/model/hdr_vmaf_v1.1.8_hfr)
- [FFmpeg libvmaf 集成](https://github.com/FFmpeg/FFmpeg/blob/master/libavfilter/vf_libvmaf.c)
- [FFmpeg 滤镜文档](https://ffmpeg.org/ffmpeg-filters.html)；具体扩展以实际构建帮助及短测为准。
- [NVIDIA NVENC/NVDEC 支持矩阵](https://developer.nvidia.com/video-encode-decode-support-matrix)
- [配套 ffmpeg-full](https://github.com/Dominic485649/ffmpeg-full)
