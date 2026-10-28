# NVIDIA 解码、编码与 CPU 回退

官方依据：https://developer.nvidia.com/video-encode-decode-support-matrix 。矩阵区分 NVDEC 和 NVENC；同系列不同产品、移动版、驱动和 FFmpeg 构建还须实际检查。未来型号不能只凭编号推断支持。

## 已知边界

| 输入/目标 | RTX 40 系（Ada） | RTX 50 系（Blackwell） |
| --- | --- | --- |
| NVDEC：常见 AV1 4:2:0 8/10-bit | 支持，仍查尺寸等限制 | 支持，仍查尺寸等限制 |
| NVDEC：AV1 4:2:2 | 不按可硬解处理，CPU 回退 | 官方矩阵未逐项给出 AV1 色度能力，必须另证；不能盲设 CUDA |
| NVDEC：H.264 10-bit 4:2:0/4:2:2 | 不支持，CPU 回退 | 新增相应支持，但须检查 profile、构建和短样本 |
| NVDEC：HEVC 4:2:2 8/10/12-bit | 不支持，CPU 回退 | 新增相应支持，仍查实际设备和构建 |
| NVENC：AV1 | 4:2:0 8/10-bit | 4:2:0 8/10-bit；不能推断 AV1 4:2:2 编码 |
| NVENC：HEVC 10-bit | 支持 | 支持 |
| NVENC：HEVC/H.264 4:2:2 | 不支持 | 新增相应能力，不等于支持所有 codec/位深组合 |

H.264、HEVC、AV1 的硬件支持必须分开。12-bit 输入可由 CPU 解码并转成 10-bit，但须说明有位深损失；HDR 与位深不是同一概念，SDR 也可能是 10-bit。保留 PQ/HLG、BT.2020 等真实颜色标记，不凭空添加母版亮度，不为硬件兼容暗中把 HDR 转 SDR。

## 全 CUDA 兼容路径

参考预设使用：

- `解码参数_解码器=cuda`
- `解码参数_解码数据格式=cuda`
- 视频自定义滤镜 `scale_cuda=format=p010le`
- `av1_nvenc` 或 `hevc_nvenc`

必须使用本技能入口中推荐的 ffmpeg-full setup 配套构建来保证这些扩展预设的支持。检测 `-filters` 中的滤镜和 `-h encoder=...` 中的选项，只能证明构建包含功能；还要核对 GPU、驱动，并用短样本确认实际路径。

## 全 CUDA 滤镜选择

完整功能、参数、使用示例与限制按需读取 `cuda-filters.md`，涵盖当前构建全部 12 项 CUDA 滤镜及下载/映射/NPP 边界。

用户明确要求全 CUDA，且本机构建确实支持所需操作时，必须采用 CUDA 实现，不能退回普通 CPU 滤镜：

| 操作 | 选择与核对 |
| --- | --- |
| 顺时针 90° / 逆时针 90° | 核对 `-h filter=transpose_cuda` 后使用 `dir=clock` / `dir=cclock`。 |
| 180° / 水平或垂直镜像 | 仅在帮助列出对应模式时使用 `dir=reversal` / `dir=hflip` / `dir=vflip`；不同构建不保证相同扩展，也可采用已验证等效的 CUDA 组合。 |
| 缩放与兼容格式转换 | 核对 `scale_cuda` 支持的输入/输出像素格式与参数；保持 GPU 帧，不能默默插入 CPU scale/format。 |
| 叠加 / 填边 / 其他处理 | 先查询实际 `overlay_cuda` / `pad_cuda` 等实现、格式与设备约束；不存在时说明限制，不凭名称猜造实现。 |

例如兼容输入要求顺时针 90°、10-bit NVENC 输出时，可在确认选项后采用 `transpose_cuda=dir=clock,scale_cuda=format=p010le`。本例不包含 CPU 帧上传；若输入需要 CPU 解码则已经不是全 CUDA，必须另作说明并获得用户放宽要求。

先读取参数面板并清除该操作对应的普通旋转/镜像/缩放设置，只替换相同操作，保留其他滤镜顺序。核对实际命令没有重复的 CPU `transpose`、`rotate`、`hflip`、`vflip`、`scale` 或不必要的 `hwdownload`/`hwupload_cuda` 往返。留意输入自动旋转元数据，避免自动旋转和显式滤镜叠加。

任意角度不等于 90° 转置；未发现 `rotate_cuda` 时不能编造该滤镜，更不能以近似角度冒充完成。缺少满足目标的 CUDA 能力时明确报告，用户同意后才走混合方案。

## 不支持 NVDEC 时自动修复

本节仅适用于未坚持全 CUDA，或已同意放宽要求的任务。明确要求全 CUDA 时，先说明硬解不兼容，不能直接静默采用以下回退。

以 AV1 4:2:2 + RTX 40 系为例，**保留 NVENC 输出，不再坚持硬解**：

1. 清空 `解码参数_解码器`、`解码参数_解码数据格式`、不适用的硬件指定字段；检查自定义输入参数没有残留 `-hwaccel cuda`、强制 CUDA 输出或旧 `*_cuvid` 解码器。
2. 让 FFmpeg 使用 CPU 解码器。若滤镜仍需 CUDA，将原 `scale_cuda=format=p010le` 视频片段改为 `format=p010le,hwupload_cuda,scale_cuda=format=p010le`。CPU 先转换到兼容的 10-bit 4:2:0，再上传 GPU；不能把 CPU 帧直接交给 scale_cuda。
3. 只替换相关视频滤镜，保留音频 SoXR、声道、用户其他滤镜的顺序。`滤镜排序系统` 补丁必须带完整排序列表，因此先读当前列表，修改目标片段后整体提交；不要无意删掉字幕或其他处理。
4. 如果全部滤镜都走 CPU，可使用 `format=yuv420p10le` 后让 NVENC 完成必要上传，不再遗留只接收 CUDA 帧的 scale_cuda。两种路径择一，不能重复上传。
5. 检查命令没有不兼容的硬解参数；短样本验证；向用户说明“CPU 解码 + NVIDIA 编码”，以及为 AV1 输出发生的 4:2:2→4:2:0 转换。用户要求保持 4:2:2 时停止这项降采样，改选支持该目标的路径。

GPU 设备不是默认设备时，上传和 NVENC 必须使用同一 GPU。NVENC 不可用、目标输出本身不受支持、UHQ 或滤镜缺失时，报告具体限制并提供适当方案；不得通过“有 NVIDIA 卡”就宣称一定全 CUDA。
