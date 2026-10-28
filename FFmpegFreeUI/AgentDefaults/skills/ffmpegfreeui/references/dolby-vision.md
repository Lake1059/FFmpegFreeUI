# 杜比视界编码

适用范围：用 FFmpeg 重编码已有逐帧 Dolby Vision RPU 的视频。当前 FFmpeg 直出 DV 使用 `libx265`、`libsvtav1`、`libaom-av1`；先运行 `ffmpeg -h encoder=编码器名` 确认本机构建有 `-dolbyvision` 选项。只设置 HDR 色彩标签不会生成 RPU；普通 HDR10 输入没有 DV 动态元数据时，不能靠 `-dolbyvision 1` 创造正确的场景元数据。

## 先读取输入 Profile 和信号

```text
ffprobe -v error -select_streams V -show_streams -of json INPUT.mkv
ffprobe -v error -show_stream_groups -of json INPUT.mkv
ffprobe -v error -select_streams v:0 -read_intervals "%+#1" -show_frames -show_entries frame=side_data_list -of json INPUT.mkv
```

读取 `dv_profile`、`dv_bl_signal_compatibility_id`、`rpu_present_flag`、`el_present_flag`、分辨率、像素格式、帧率、时长和 `color_primaries/color_transfer/color_space/color_range`。stream group 和视频流数用来判断 BL/EL 是分离流还是同一 HEVC 多层流；stream group 为空不等于无 DV。

从 stream/frame 的 `Mastering display metadata` 读取色域坐标和最大/最小亮度，从 `Content light level metadata` 读取 MaxCLL/MaxFALL。它们是 HDR10 静态元数据，不是 RPU。若目标为 P8.1/x265 而 mastering display 缺失，不能满足 x265 要求；先取得可信的母版数据，不能用通用值冒填。x265 `master-display` 的 x/y 单位为 1/50000，亮度单位为 0.0001 cd/m²；将 ffprobe 有理数转为小数后，x/y 乘 50000、亮度乘 10000。`max-cll=MaxCLL,MaxFALL` 的单位是 cd/m²。若源文件没有有效数据，不从片名、网站或另一部片猜值；Profile 8.1 下 x265 会自动发射 CLL SEI，缺失时值可能为 0/0，不能把它解释成实测峰值。

## 按 Profile 选择路径

| 输入 Profile | 基础层兼容类型 | 编码/转换路径与边界 |
| --- | --- | --- |
| 5 | 非 HDR10 兼容 | x265 文档列出 Profile 5 输出支持；保留同一 RPU/profile 与基础画面信号时可尝试同 Profile 重编码。要转 8.1，`dovi_tool -m 3` 转的是 RPU；它不转换像素，只有基础视频本身符合目标 HDR10 信号时才可注入为 P8.1。 |
| 7（MEL/FEL） | HDR10 BL + EL/RPU | 不能由 x265 输出双层 P7。提取 BL 与 EL/RPU，把 RPU 转为 P8.1 后注入 BL；FEL 的增强残差不进入单层 P8.1，画面会有相应信息损失。步骤见下节。 |
| 8.1 | HDR10 兼容，常见兼容 ID 1 | 可用 x265 同 Profile 重编码。必须给 VBV 和源片 `master-display`；RPU 与源帧一一对应。 |
| 8.2 | SDR 兼容，常见兼容 ID 2 | x265 文档列出支持。保持 SDR 基础层与 SDR 色彩标记；不要套用 P8.1 的 PQ、HDR10 mastering display 或 MaxCLL。 |
| 8.4 | HLG 兼容，常见兼容 ID 4 | x265 文档列出支持。保持 HLG 基础层及 HLG 色彩标记；不要把 HLG 标成 PQ/HDR10。 |
| 10（AV1） | 由兼容 ID 指示基础层类型 | 用 `libsvtav1` 或 `libaom-av1` 输出 AV1 DV 元数据；不适用 x265。输入 RPU 必须和目标 AV1 基础层兼容，不能只把 HEVC 的 Profile 数字改成 10。 |
| P8.0、P4/P6/P9 或未知 Profile | 不推断 | P8.0 不在 x265 文档列出的 5/8.1/8.2/8.4 输出列表内；其他 Profile 也不套用下方模板或只改 Profile 字段，先依据对应格式文档确认 RPU 与基础层的适配方式。 |

表中 Profile 5/8.x 的 x265 能力来自 x265 CLI 文档，Profile 10 编码器选项来自 FFmpeg；本机只有 P7 样片，所以其他 Profile 的参数兼容性属于公开文档依据，未在本机逐 Profile 实测。Dolby Vision Profile 数字和兼容 ID 不同概念；判断 8.1/8.2/8.4 时同时看 `dv_profile=8` 和兼容 ID，不要只看数字 8。

`dovi_tool` 当前公开文档列出的 RPU 转换模式：`-m 1` 转 MEL 兼容，`-m 2` 转 P8.1 并移除 P7 FEL 的 luma/chroma mapping，`-m 3` 将 P5 RPU 转 P8.1，`-m 4` 转 P8.4，`-m 5` 转 P8.1 并保留 mapping。它们只处理 RPU，不会把 SDR 变 HDR、把 HLG 变 PQ 或修复不兼容的基础视频。版本间选项可能变化；按已安装版本的 `dovi_tool --help` 与对应 README 确认模式，不能盲目照抄另一版工具参数。

## 编码参数及含义

下列完整模板以 P8.1 为目标。P5、P8.2、P8.4 应沿用各自基础层色彩与相应 RPU；不要把 P8.1 的 HDR10 静态参数强加给它们。滤镜/剪辑若改变帧数、顺序、画面区域或色彩变换，原 RPU 可能不再适配；除非同步编辑/重建 RPU，否则保持逐帧与画面对应。`-map 0:v:0 -an` 仅映射视频，音频/字幕按目标另行映射。

```text
# P8.1 HEVC；占位符从本片静态 HDR 元数据及目标码率约束填写
ffmpeg -i INPUT_P8_1.mkv -map 0:v:0 -an -c:v libx265 -preset slow -crf 18 -pix_fmt yuv420p10le -dolbyvision 1 -x265-params "vbv-maxrate=MAXRATE_KBPS:vbv-bufsize=BUFSIZE_KBITS:master-display=G(GX,GY)B(BX,BY)R(RX,RY)WP(WPX,WPY)L(MAX_LUMA,MIN_LUMA)" OUTPUT.mkv

# AV1 Profile 10
ffmpeg -i INPUT_P8_1.mkv -map 0:v:0 -an -c:v libsvtav1 -preset 6 -crf 34 -pix_fmt yuv420p10le -dolbyvision 1 OUTPUT.mkv
ffmpeg -i INPUT_P8_1.mkv -map 0:v:0 -an -c:v libaom-av1 -cpu-used 5 -crf 30 -b:v 0 -pix_fmt yuv420p10le -dolbyvision 1 OUTPUT.mkv
```

逐项规则：

- `-x265-params`：冒号分隔的 x265 原生参数。不要把 CLI 专用 `dolby-vision-rpu` 当作 FFmpeg libx265 参数；不能用 `dolby-vision-profile` 单独将错误 RPU 变成正确 Profile。输出 Profile 由受支持的 x265 Profile、输入 RPU 和基础层信号共同决定。
- `-dolbyvision 1`：FFmpeg 编码器的 RPU 编码开关，显式设置为 1（或者 true），不依赖默认 `auto`。它编码输入帧已有的 DV 元数据，不从 HDR10 画面创作新的逐场景 RPU。
- `vbv-maxrate` / `vbv-bufsize`：x265 VBV 峰值码率（kbit/s）和缓冲区大小（kbit）。启用 x265 DV/HRD 时两者必须同时给出。maxrate 来自目标分发峰值码率；bufsize 通常按 maxrate × 缓冲秒数换算。不能使用固定样片值作为所有输出的通用限制。
- `master-display`：写入 SMPTE ST 2086 mastering-display 元数据。x265 P8.1 要求该参数；G/B/R、白点和最大/最小亮度必须来自当前片源。坐标按 1/50000、亮度按 0.0001 cd/m² 表示，模板内占位符需先换算为整数。
- `max-cll`：模板中方括号表示可选参数；有可靠源值时用输入的 `Content light level metadata` 填写，没有时删除整个 `[:max-cll=...]` 部分，不要编造。P8.1 x265 仍会发射 CLL SEI，未指定时的 0/0 表示未提供有效测量。

Profile 8.1 x265 输出应探测为 `dv_profile=8`、兼容 ID 1；Profile 8.2/8.4 分别预期兼容 ID 2/4。AV1 应探测为 Profile 10。所有情形都应有 `rpu_present_flag=1`、`el_present_flag=0`。兼容 ID 必须按目标播放器和实际码流再核对；设备需要支持对应 codec/Profile 才能播放 DV。

## Profile 7 分层转单轨 8.1

P7 的 BL 是可独立解码的 HDR10 基础画面，EL 携带 RPU 和可能存在的 FEL 残差。先判断文件结构：同一条 HEVC 多层流用 `dovi_split`；BL/EL 分属独立流时按 ffprobe 的绝对 stream index 分别提取。`mode=bl` 仅保留 BL，`mode=el_rpu` 输出 EL 与 RPU。`-c:v copy -f hevc` 是无损拆成裸 HEVC，不做编码。

```text
# 同一条 HEVC 多层流
ffmpeg -i INPUT.mkv -map 0:VIDEO_STREAM_INDEX -c:v copy -bsf:v dovi_split=mode=bl -f hevc bl.hevc
ffmpeg -i INPUT.mkv -map 0:VIDEO_STREAM_INDEX -c:v copy -bsf:v dovi_split=mode=el_rpu -f hevc el.hevc
# BL/EL 分属独立流时改成各自绝对 stream index
ffmpeg -i INPUT.mkv -map 0:BL_STREAM_INDEX -c:v copy -f hevc bl.hevc
ffmpeg -i INPUT.mkv -map 0:EL_STREAM_INDEX -c:v copy -f hevc el.hevc

dovi_tool -m 2 extract-rpu -i el.hevc -o rpu_p81.bin
dovi_tool info -s -i rpu_p81.bin
dovi_tool inject-rpu -i bl.hevc --rpu-in rpu_p81.bin -o bl_rpu.hevc
mkvmerge -o INPUT_P8_1.mkv --default-duration 0:SOURCE_FRAME_RATEfps bl_rpu.hevc
```

`-m 2` 将 P7 RPU 转成 P8.1 兼容形式并移除 FEL mapping；FEL 还原所需的信息不再完整。`inject-rpu` 只接受裸 HEVC，且 RPU 帧数/顺序必须与 BL 一致。裸 HEVC 没有容器时间戳，所以 `mkvmerge --default-duration` 中 `0` 是视频轨编号，帧率用 ffprobe 读到的精确恒定帧率（含分数）。VFR、BL/EL 帧数不等或时间线不一致时，不可直接套用此重建方式；不要用 `setts` 伪造时间戳绕过。本机未安装 `dovi_tool` / `mkvmerge`，因此没有重新执行整条外部流程；流程依据用户提供的已实测文档和公开工具文档整理。

## 输出核验

对输出运行 `ffprobe -v error -select_streams v:0 -show_streams -of json OUTPUT.mkv`，核对 `dv_profile`、兼容 ID、RPU/EL flags、色彩标记与预期基础层类型。HEVC 可用 `dovi_tool extract-rpu` 抽查 Profile/帧数；AV1 可运行 `ffmpeg -loglevel verbose -i OUTPUT.mkv -map 0:v:0 -c copy -bsf:v trace_headers -f null NUL`，核对每帧有 `metadata_type = 4` 和 ITU-T T.35 country code 181。容器配置记录只证明码流声明了 DV，不能单独证明每帧 RPU 正常；还要核对帧数、帧率、时长、RPU 顺序和 BL 兼容类型。当前环境的 48 帧功能实测确认三种编码器能输出 DV 元数据；P7 FEL 直接转码仍保留 residual flag，因此 P7 必须先做 RPU 转换。

## 公开依据

- [x265 CLI documentation](https://x265.readthedocs.io/en/master/cli.html)：x265 支持的 Dolby Vision 输出 Profile，以及 `master-display`、`max-cll`、VBV 和 CLI-only RPU 参数说明。
- [FFmpeg libx265](https://github.com/FFmpeg/FFmpeg/blob/master/libavcodec/libx265.c)、[libsvtav1](https://github.com/FFmpeg/FFmpeg/blob/master/libavcodec/libsvtav1.c)、[libaom-av1](https://github.com/FFmpeg/FFmpeg/blob/master/libavcodec/libaomenc.c)：三个 FFmpeg 编码器的 `-dolbyvision` 选项定义；本机仍应以 `ffmpeg -h encoder=...` 为准。
- [dovi_tool README](https://github.com/quietvoid/dovi_tool/blob/main/README.md)：RPU 分析与 Profile 转换模式。工具版本可能改变命令选项，使用时按安装版本文档核对。
