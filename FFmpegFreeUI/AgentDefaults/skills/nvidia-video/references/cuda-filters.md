# CUDA 滤镜：功能、参数与使用方法

## 适用范围与硬性规则

用户明确使用全 CUDA 流程时，当前构建能用 CUDA 实现的画面处理必须采用 CUDA 滤镜，不能换成 CPU 滤镜后仍声称全 CUDA。这里的全 CUDA 指视频解码、像素处理、编码保留兼容的 GPU 帧，不代表解封装、时间戳运算、音频和应用逻辑也由 GPU 执行。

本参考覆盖配套 FFmpeg 构建中探测到的全部 12 项名称含 CUDA 的滤镜，并说明下载/映射边界与可选 NPP。参数依据 2026-10-28 查询的 `git-2026-10-24-d8990137ee` 帮助；不是所有 FFmpeg 版本都提供相同扩展。帮助可见不等于所有像素格式、GPU 或输入都可运行，必须按用户实际构建复核并以短样本验证。

先查询：

```text
ffmpeg -hide_banner -filters
ffmpeg -hide_banner -h filter=transpose_cuda
ffmpeg -hide_banner -h filter=scale_cuda
```

其他滤镜把名称替换即可。不要依赖退出码判断是否存在：某些构建遇到 Unknown filter 也返回 0，必须检查实际帮助内容。

以下多为 `-vf` 或 `-filter_complex` 片段，不是可直接提交的完整应用状态；实际面板命令必须通过工具生成。以当前 GPU、驱动、输入 codec/profile/位深/色度、硬件帧底层格式与滤镜帮助为准。

## 总览

| 滤镜 | 功能 | 输入/输出与常见边界 |
| --- | --- | --- |
| `transpose_cuda` | 90°/180°旋转、转置和本构建提供的镜像模式 | 单路 CUDA 帧；90°交换宽高，扩展模式依赖构建。 |
| `scale_cuda` | 缩放、兼容像素格式转换及本构建提供的范围/矩阵转换 | 单路 CUDA 帧；格式和矩阵支持不等于完整 HDR 色彩管理。 |
| `bilateral_cuda` | 保边双边降噪 | 单路 CUDA 帧；强度/窗口影响细节和性能，不等于时域降噪。 |
| `yadif_cuda` | CUDA 反交错 | 确认源是否交错、场序和输出帧率；不能替代逆电视电影。 |
| `bwdif_cuda` | CUDA BWDIF 反交错 | 同样确认场序和单帧/双场输出，不应对逐行源盲目使用。 |
| `chromakey_cuda` | 色键抠像 | CUDA 帧；alpha 是否保留取决于格式和后续滤镜/容器。 |
| `overlay_cuda` | GPU 双路叠加、水印、画中画 | 两路兼容 CUDA 帧，同一设备，底层格式和 alpha 必须兼容。 |
| `pad_cuda` | GPU 扩大画布、加边、按宽高比填充 | 不等于裁剪；画布必须能容纳输入。 |
| `colorspace_cuda` | 本构建只暴露输出颜色范围转换 | 帮助仅有 `range`，不能据此承诺色域、PQ/HLG 或 tone mapping。 |
| `thumbnail_cuda` | 每批帧选择代表性缩略帧 | GPU 选帧不等于最终图片编码全 CUDA。 |
| `hwupload_cuda` | CPU 帧上传到 CUDA | 从这里开始的 GPU 后段不是输入至输出全 CUDA。 |
| `libvmaf_cuda` | 双路视频 CUDA VMAF 评测 | 不因滤镜存在就支持 V1/HDR；模型和特征提取器必须兼容。 |

## 1. transpose_cuda：旋转和镜像

`dir` 在所测构建中提供：

| 值 | 效果 | 片段 |
| --- | --- | --- |
| `clock` / 1 | 顺时针 90° | `transpose_cuda=dir=clock` |
| `cclock` / 2 | 逆时针 90° | `transpose_cuda=dir=cclock` |
| `cclock_flip` / 0 | 逆时针 90°并垂直翻转 | `transpose_cuda=dir=cclock_flip` |
| `clock_flip` / 3 | 顺时针 90°并垂直翻转 | `transpose_cuda=dir=clock_flip` |
| `reversal` / 4 | 180° | `transpose_cuda=dir=reversal` |
| `hflip` / 5 | 水平镜像 | `transpose_cuda=dir=hflip` |
| `vflip` / 6 | 垂直翻转 | `transpose_cuda=dir=vflip` |

模式 4–6 是本机构建实测帮助列出的能力，不能假定旧构建也有；没有 `reversal` 但支持 `clock` 时可验证两次 `clock` 的等效 180° 组合。`passthrough=none` 默认执行；`landscape` / `portrait` 可按输入几何条件跳过，只在用户确实要求条件处理时用，不能导致必须旋转的任务被跳过。

例如：

```text
transpose_cuda=dir=clock,scale_cuda=format=p010le
```

这保持 CUDA 帧并准备兼容 10-bit NVENC 输出。任意角度旋转不能拿 90° 或镜像凑成近似目标；本机未提供 `rotate_cuda` 时说明限制，不编造名字。不使用 `hwdownload,transpose,hwupload_cuda` 代替可用 CUDA 转置。核对自动旋转元数据，防止 FFmpeg 自动旋转与显式处理重复。

## 2. scale_cuda：缩放和格式

主要选项：

- `w` / `h`：宽高表达式，默认 `iw` / `ih`；例如 1920×1080。
- `interp_algo`：`nearest`、`bilinear`、`bicubic`、`lanczos`；依速度/细节目标选择。
- `format`：目标底层像素格式，例如兼容时用 `p010le`；这不是把硬件帧改成 CPU `format` 滤镜。
- `passthrough`：默认 true；参数匹配时可直接复用帧，`passthrough=0` 可要求重新处理/分配，不能随意当质量开关。
- `param`：算法相关参数，仅按当前算法帮助/文档使用。
- `force_original_aspect_ratio=decrease|increase|disable`、`force_divisible_by`、`reset_sar`：控制等比尺寸、对齐和 SAR；不把强拉伸冒充等比。
- 本构建另有 `use_filters=auto|0|1`、`out_range`、`out_color_matrix`。它们并非所有版本都有；矩阵实际列出 bt601/bt709/bt2020nc 等，先查帮助，不猜选项。

例如限制到 1920×1080 内、保持宽高比并对齐偶数：

```text
scale_cuda=w=1920:h=1080:interp_algo=lanczos:force_original_aspect_ratio=decrease:force_divisible_by=2:format=p010le
```

恰好输出 1920×1080 而非拉伸，可以在实际缩放尺寸确定后接 `pad_cuda` 居中填充。输入 4:2:2/4:4:4 变成 4:2:0、12-bit 变 10-bit 等是内容损失，需要说明且不能违背保留目标。矩阵/范围变换不等于传递函数转换；BT.2020/PQ→BT.709/SDR 需要真正 tone mapping，不能只改标记。

## 3. bilateral_cuda：双边降噪

- `sigmaS`：空间尺度，所测范围 0.1–512。
- `sigmaR`：颜色/范围尺度，所测范围 0.1–512。
- `window_size`：邻域窗口，所测范围 1–255；较大窗口可能明显增大 GPU 开销。

起点示例，不作为所有素材的默认强度：

```text
bilateral_cuda=sigmaS=3:sigmaR=0.1:window_size=9
```

必须用实际底层像素格式短测并比较细节、肤色、纹理和带状失真。该滤镜不是通用时域降噪或 AI 修复，也不保证支持所有 10/12-bit、色度或浮点格式；不支持时先说明全 CUDA 约束，不静默插 CPU 降噪。

## 4. yadif_cuda / bwdif_cuda：反交错

共同选项：

- `mode=send_frame`：每输入帧输出一帧；`send_field`：每场输出一帧，常导致 25i→50p / 29.97i→59.94p。
- 所测构建另列 `send_frame_nospatial` / `send_field_nospatial`；跳过空间检查不应当作无条件提速默认值。
- `parity=auto|tff|bff`：自动/顶场优先/底场优先。元数据错误时需要依据真实样本确认，不能猜场序。
- `deint=all|interlaced`：全部处理或仅处理标记为交错的帧。

例如：

```text
bwdif_cuda=mode=send_field:parity=auto:deint=interlaced
```

或在需求适合时改为 `yadif_cuda`。两者择一，不串联重复反交错；不要对逐行视频默认开启。25i 的场输出不等于人工插帧，电影胶转视频的 cadence 问题也不能盲目靠反交错解决。

## 5. chromakey_cuda：色键抠像

选项：`color` 默认 black；`similarity` 为 0.01–1；`blend` 为 0–1；`yuv=true` 时 color 按 YUV 而不是 RGB 解释。

绿色背景示例：

```text
chromakey_cuda=color=0x00FF00:similarity=0.1:blend=0.05
```

先验证底层格式、alpha 输出及后续 `overlay_cuda` 的支持。旋钮越大不等于边缘越好，需检查溢色、发丝和半透明区域；不保证 NVENC、最终容器或播放器能保留透明通道。明确要求全流程 CUDA 时，多路输入必须都满足 GPU 路径约束。

## 6. overlay_cuda：水印和画中画

两路输入顺序是主画面、叠加画面；必须使用同一 GPU、兼容的 CUDA 硬件帧与底层格式。

- `x` / `y`：叠加位置表达式；`eval=init|frame` 决定初始化或逐帧求值。
- `shortest`：以较短输入结束；`repeatlast`：是否延用最后叠加帧；`eof_action=repeat|endall|pass`：第二路结束后的行为。
- 帧同步另有 `ts_sync_mode=default|nearest`；先统一分辨率、时间基准与实际时间戳含义。

位置示例：

```text
[main][mark]overlay_cuda=x=10:y=10:eval=init:shortest=1[out]
```

这是滤镜图，不是合法的单路 `-vf` 参数。静态图片水印如果靠 CPU 解码后上传，虽叠加在 GPU 上，整体也不能称为所有输入全 CUDA。应用现有自定义滤镜不支持新增双输入图时先说明能力边界，不把 `-filter_complex` 文本硬塞进只接受单输入的字段；使用已授权的合适工具/阶段。

## 7. pad_cuda：画布和填边

- `width`/`w`、`height`/`h`：输出画布，默认 `iw` / `ih`。
- `x` / `y`：输入放置位置；`color`：边框颜色，默认 black。
- `eval=init|frame`：求值时机；`aspect`：按目标宽高比填充。

将能容纳的图像居中放进 1920×1080：

```text
pad_cuda=w=1920:h=1080:x=(ow-iw)/2:y=(oh-ih)/2:color=black
```

需先等比缩小过大的输入，尺寸和偏移符合底层色度采样对齐。填边不等于裁剪，不允许给比输入还小的画布并假装完成 crop。

## 8. colorspace_cuda：颜色范围

所测帮助仅暴露 `range=tv|mpeg|pc|jpeg`，即有限/全范围。例：

```text
colorspace_cuda=range=tv
```

先核对输入真实范围和用户目标，避免黑位压缩/泛白；不能从这个滤镜名推断它支持色域、原色、EOTF/PQ/HLG 或 HDR→SDR。需要矩阵转换时核对本构建 `scale_cuda` 的实际扩展；完整 HDR 色调映射仍需可证明支持的处理链，不得用范围变换冒充。

## 9. thumbnail_cuda：代表帧

`n` 为每批帧数量，最小 2、所测默认 100：

```text
thumbnail_cuda=n=100
```

它从一批连续帧中选代表帧，不是“每秒固定截图”或 scene 检测。若目标是 PNG/JPEG 等 CPU 图片编码，需要显式下载到兼容软件格式并说明 GPU 仅负责解码/选帧；例如在已确认底层为 nv12 时，后续可用 `hwdownload,format=nv12`，不能把该出口当成端到端全 CUDA。图片输出还要按任务约束设置帧数/时间戳策略，避免重复帧。

## 10. hwupload_cuda：上传和设备

`device` 为 GPU 索引，默认 0；所测构建另有 `output_format=cuda|cuarray`，并不是所有消费者都支持 cuarray，不能随意切换。

CPU 帧混合路径示例：

```text
format=p010le,hwupload_cuda=device=0,scale_cuda=format=p010le
```

`format` 在上传前是 CPU 转换；此路径仅是 CPU 解码＋GPU 后段，必须如实称为混合。已经是 CUDA 帧时不要重复上传。多 GPU 时解码、上传、滤镜与编码使用同一个兼容设备上下文，不凭编号相同就忽略上下文兼容性。

## 11. libvmaf_cuda：评测

两路输入分别为失真/主路、参考路。主要参数：`model`、`log_path`、`log_fmt=csv|json|xml|sub`、`pool`、`n_threads`、`n_subsample`、`feature`；另有帧同步的 shortest/eof_action/repeatlast/ts_sync_mode。

在模型与格式确实兼容、两路已经对齐的前提下，滤镜片段例如：

```text
[dist][ref]libvmaf_cuda=model=version=vmaf_v0.6.1neg:log_fmt=json:log_path=vmaf.json[out]
```

不把 libvmaf_cuda 的存在当成 V1/HDR 可用。应用目前仅为已验证兼容的 V0 NEG 提供 CUDA 项；新 HDR 模型还需要正确颜色信息与引擎支持，不能简单换模型名。分辨率、帧顺序/PTS、采样、颜色和预处理必须一致，不以评分动作混入生产转码链。

## 12. 下载、映射与可选 NPP

- `hwdownload` 将硬件帧变成软件帧；输出软件格式必须与真实硬件底层格式兼容。进入 CPU 像素滤镜后再上传就是混合，不得隐藏。
- `hwmap` 用于硬件/软件或设备间映射。`mode=read|write|overwrite|direct`、`derive_device`、`reverse` 必须按设备支持配置；出现映射滤镜不代表一定零拷贝，direct 不支持时不能继续声称零拷贝。
- `scale_npp`、`scale2ref_npp`、`sharpen_npp` 属于可能随 NVIDIA NPP 构建提供的相关实现，本次所测 `-filters` 没有列出，不能直接推荐为已安装。若用户构建实际提供，再读取对应帮助核对缩放、双路参考尺寸或锐化功能、格式和参数；不凭 NVENC 存在推断 NPP 已编译。
- Vulkan/OpenCL/libplacebo 等是其他设备/渲染路径，不能仅因为也使用 GPU 就称为 CUDA。无实际能力证明时，不能编造 `crop_cuda`、`rotate_cuda`、`hflip_cuda` 或其他相似名字。

## 应用内操作与验收清单

1. 先读取用户目标、面板状态、GPU 与输入；明确全 CUDA 是硬要求还是一般性能偏好。
2. 查询实际滤镜/编码器帮助，逐个核对位深、色度、alpha、多输入、尺寸对齐、设备及时间戳要求。
3. 单输入 CUDA 操作放入正确的自定义视频滤镜位置；清除面板中会产生同一 CPU 操作的字段。保持完整排序列表，避免删除无关滤镜和音频设置。
4. 检查生成命令：兼容输入保留 `-hwaccel cuda -hwaccel_output_format cuda`、CUDA 像素滤镜和兼容 NVENC。不能单凭编码器叫 nvenc 就称全 CUDA。
5. 用授权短样本确认执行成功，核对输出尺寸/帧率/格式、旋转方向与画面、颜色和音轨；预设能生成命令不等于所有滤镜已实机验收。
6. 缺少能力时明确说明，用户坚持全 CUDA 就停止不符合要求的降级；用户允许后再用混合/软处理，并如实标注哪些步骤使用 CPU。

参考入口：FFmpeg 官方滤镜文档 https://ffmpeg.org/ffmpeg-filters.html 。配套构建的扩展以该构建实际帮助和短测为准，不能用上游文档代替本机能力。
