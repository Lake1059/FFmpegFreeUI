---
name: nvidia-video
description: 视频转码时检查输入格式和 GPU，RTX 40 系及以上默认优先 NVENC；含五份完整 NVENC 预设、全 CUDA 依赖和自动 CPU 解码回退规则。明确要求全 CUDA 时，可用 CUDA 实现的旋转等操作必须使用 CUDA 滤镜；用户明确要求软编码时尊重其选择。
---

# NVIDIA 视频编码

## 触发与原则

用于视频压缩、转码和 NVENC 排错，不用于独立歌曲或 AVIF 图片。先用 `get_system_hardware` 获取真实显卡，识别 RTX 40 系（Ada）、RTX 50 系（Blackwell）或有明确能力依据的更新型号。检测到 40 系及以上时先推荐 NVIDIA 编码器；不要把核显、RTX 30 系的 AV1 解码能力当成 AV1 编码能力。

用户明确要求软编码、目标格式必须软编码，或设备能力不足时尊重目标，读取 `ffmpegfreeui/references/recommendations.md` 的软编码建议，不反复要求改回 NVENC。用户的目标、声道保留、播放兼容性和质量要求优先于默认预设。

## 配套 FFmpeg 必须说明

推荐用户从 https://github.com/Dominic485649/ffmpeg-full 的 Releases 下载新预设与 **setup 版本的 FFmpeg**：https://github.com/Dominic485649/ffmpeg-full/releases/latest 。本技能的全 CUDA 路径以该配套构建为支持前提，尤其是其 `scale_cuda` 格式转换及扩展滤镜；普通构建不保证直接运行这些预设。不要自动安装或覆盖 FFmpeg，安装须用户授权。

即使用了该构建，也必须检查 GPU、驱动、输入格式和实际编码器/滤镜。预设备注要求驱动 610 及以上；以当前发行说明和本机实际支持为准，不能只看驱动版本数字就宣称 UHQ 可用。全 CUDA 指视频的解码、滤镜、编码路径，不包括必然使用 CPU 的容器处理、音频编码和程序自身。

## 按需读取的完整预设

以下 reference 已包含完整 v6 预设 JSON，不依赖用户安装同名预设，也不能去开发者机器的 D 盘找文件。

| 目标 | reference | 关键视频参数 |
| --- | --- | --- |
| 常规高质量 AV1，40/50 系优先 | references/NVENC-AV1.md | av1_nvenc，p2，uhq，VBR，cq=36 |
| AV1 更快 | references/NVENC-AV1-Fast.md | av1_nvenc，p2，hq，VBR，cq=36 |
| HEVC，更广泛播放兼容性 | references/NVENC-HEVC.md | hevc_nvenc，p2，uhq，VBR，cq=30 |
| HEVC 更快 | references/NVENC-HEVC-Fast.md | hevc_nvenc，p2，hq，VBR，cq=30 |
| HEVC 较低码率 | references/NVENC-HEVC-LowRate.md | hevc_nvenc，p4，uhq，VBR，cq=30 |

普通视频默认从 NVENC-AV1 开始，用户重视兼容性时选 HEVC；速度要求明确时选 Fast。

这些视频预设默认 `.mkv`、10-bit 视频转换、视频伴随音轨使用 `opus.libopus` 96k、stereo，音频 SoXR 48 kHz、precision=28、cheby=1。用户需要保留多声道时不要强制立体声；有损到有损转码不能恢复细节，所谓听觉无损是目标而非数学无损。

## 必做的兼容性检查

先读 `references/compatibility.md`。至少取得输入的 codec_name、profile、pix_fmt、位深/色度、分辨率、帧率、色彩标记和实际 GPU 型号。可以在系统访问权限下用配置的 ffprobe/ffmpeg，通过 `run_windows_executable` 的 arguments 数组读取 `-show_streams -show_format -of json`，以及 `-encoders`、`-filters`、`-h encoder=av1_nvenc` 或 `hevc_nvenc`。硬件名称不代替实际运行测试；权限不足时报告并请求适当权限，不假装检查过。

分别判断 **NVDEC 输入解码** 与 **NVENC 输出编码**。用户未坚持全 CUDA 时，遇到不兼容自动修正为 CPU 解码 + NVENC，不必等批量任务全部失败；明确要求全 CUDA 时按后文约束先说明限制，不静默降级：

- AV1 4:2:2 + RTX 40 系：取消 CUDA 硬解，使用 CPU 解码，再转换到 NVENC 支持的输出格式。
- H.264 10-bit + RTX 40 系，或 HEVC 4:2:2 + RTX 40 系：同样回退 CPU 解码。
- RTX 50 系新增部分 H.264 10-bit/4:2:2 和 HEVC 4:2:2 能力，不代表所有 codec、位深、色度均支持；按官方矩阵和短样本核对。AV1 4:2:2 不因“50 系更新”就假定能硬解。
- NVENC AV1 输出是 4:2:0 的 8/10-bit 路径；输入 4:2:2/4:4:4 必须明确转换的色度损失。用户要求保留原色度时选择确实支持的编码器，不暗中降采样。
- 输入 H.264 10-bit 不代表输出也必须 H.264。用户明确要求 H.264 10-bit，而当前 NVENC 不支持时推荐合适的软编码方案，不把 8-bit 输出当成满足要求。

## 明确要求全 CUDA 时的滤镜约束

当用户明确选择或要求**全 CUDA 流程**，且请求的旋转、缩放、镜像等处理可由当前 FFmpeg 的 CUDA 滤镜完成时，**必须使用 CUDA 滤镜**，不能因为参数面板的普通选项更方便就改用 CPU 滤镜。先按需读取 `references/cuda-filters.md` 的完整功能、参数、示例和限制（覆盖本机构建的全部 12 项 CUDA 滤镜及下载/映射/NPP 边界），再按 `-filters` 与 `-h filter=<实际滤镜名>` 核对本机构建的参数。

- 90° 旋转优先使用已确认支持的 `transpose_cuda=dir=clock` / `transpose_cuda=dir=cclock`；180°、镜像等仅使用本机构建帮助中真实列出的模式或验证过的 CUDA 组合。不猜造 `rotate_cuda`、`hflip_cuda` 等名称。
- 缩放/兼容像素格式转换使用可满足目标的 `scale_cuda`；叠加、填边等同样先确认实际 CUDA 实现与输入约束。任意角度旋转不能用 90° 转置冒充，也不能仅凭名称推断有对应 GPU 实现。
- 读取现有面板和滤镜排序后，把该操作放进正确的自定义视频滤镜链；清除由面板普通旋转/镜像/缩放字段产生的同一 CPU 操作，避免处理两次，保留无关字幕、音频和用户其他设置。
- 验证应用实际生成的命令。视频解码输出、CUDA 滤镜输入/输出和 NVENC 应保持兼容的 GPU 帧；不允许用 `hwdownload,transpose,hwupload_cuda` 等 CPU 往返链替代可用 CUDA 滤镜，还把它称为全 CUDA。
- 输入不支持 NVDEC，或当前构建没有能满足目标的 CUDA 滤镜时，明确说明全 CUDA 要求暂时无法满足；不要静默删掉操作、改角度或切 CPU。用户同意放宽后才采用 CPU 解码/CPU 滤镜与 NVENC 的混合方案，并如实说明。未明确要求全 CUDA 的任务仍适用原自动 CPU 解码回退规则。

## 应用和验证顺序

1. 查当前面板、队列及字段，读取对应 reference 的完整 JSON。不要假设预设名称存在于用户预设列表。
2. 用户要求整套配置时，可把 JSON 作为 `apply_parameter_panel_patch.preset_json` 应用，再按实际输入和用户要求做最小 changes；只要求局部修改时不要覆盖整份面板。未知字段先 `get_parameter_field_info`。
3. 全 CUDA 兼容时沿用解码器/输出格式 `cuda` 与 `scale_cuda=format=p010le`。不兼容时按 compatibility.md 清理硬解参数并建立正确的 CPU→GPU 上传路径。
4. 验证应用生成的命令。批量前在授权范围内用短样本验证实际编码器、UHQ、像素格式和滤镜链；报错时读取原始错误再定位，不原样循环重试。
5. 仅提交和启动用户授权的文件；修改面板不会自动同步已入队快照。反馈真实的全 CUDA 或 CPU 解码 + NVENC 路径，不把混合方案称作全 CUDA。
