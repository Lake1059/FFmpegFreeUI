---
name: avif-aom
description: 仅在用户明确选择 AVIF 时，使用完整 AOM-AVIF 软件编码预设，并按可用 CPU 数和文件数自动配置单线程任务的批次并行。
---

# AVIF 图片压缩

## 触发边界

只有用户明确说输出 AVIF 或已经指定 `.avif` 目标时才触发。泛泛的“压图”、JPEG/WebP/JXL 请求不自动改成 AVIF，也不因为检测到 NVIDIA 显卡就用 NVENC 替代本技能的 AOM 软件编码。

先读取 `references/AOM-AVIF.md`，其中包含完整 FFmpegFreeUI v6 JSON，不需要用户安装同名预设。

## 固定参考配置

- 容器 `.avif`，软件编码器 `libaom-av1`，AV1 类别。
- 编码预设 `5`（生成 `-cpu-used 5`），CRF 23。
- 进阶参数 `-usage allintra -still-picture 1 -row-mt 0 -aom-params tune=iq`。
- 像素格式候选滤镜 `format=pix_fmts=yuv420p10le|yuv422p10le|yuv444p10le`。
- 解码线程 `1`，编码线程 `1`，开头参数 `-filter_threads 1`。不要再给每个并发任务开启全部 CPU 线程。
- 输出自动命名沿用预设，避免覆盖原图。用户要求保留透明度、ICC、HDR 或动画时先确认该预设的限制，不能把普通单图方案冒充完整保留。

“视觉无损”是参考预设的目标，不是对所有图片的保证，也不是数学无损。先核对本机构建包含对应 libaom 选项；`tune=iq` 不可用时推荐配套 https://github.com/Dominic485649/ffmpeg-full 的 setup 版本，不直接吞掉参数并声称等价。

## 自动并行的实际操作

1. 确认待处理图片路径、数量和用户已经授权编码。调用 `get_system_hardware`，使用 `processor_count`（进程实际可用逻辑处理器数），不要把 CPU 型号字符串中的数字当成核心数。`physical_core_count=0` 表示无法读取，不等于零核。
2. 读取队列；有无关的活动或可自动启动任务时，不修改它们，也不把它们混入 AVIF 批次。可等待空闲或让用户决定；工具会拒绝在这种情况下放大并行度。
3. 应用完整参考预设，并保留用户要求的输出目录等字段。确认本批次每项确实是 `.avif`、`libaom-av1`、解码/编码/滤镜各 1 线程。
4. **入队前**调用 `configure_avif_parallelism`，`file_count` 是本次图片实际数量。默认上限为 `min(processor_count, file_count)`；高分辨率图片内存压力大时可传更小的 `max_parallel`。只在工具成功后才能说并行度已配置。
5. 工具不启动任务、不修改全局并发设置，只给单线程 AOM AVIF 队列提供临时上限，可超过普通界面的 10 任务范围。入队和启动仍通过准备文件及队列工具；按本次返回的 ID 操作，不调用启动全部或操作无关项。
6. 自动开始开启时，正常入队由调度器补满；未自动开始时，可逐个用 `control_queue_tasks(action=start,id=...)` 启动指定任务，首批不超过工具返回的 `parallel_limit`。普通单次 start 不自动补满，不得虚报已并行运行。
7. 批次自然结束/停止后临时上限清理，普通视频并发设置保持不变。检查队列实际状态和输出，不把“已入队”说成“编码完成”。

只要求推荐时给出配置，不直接开始编码。涉及目录、媒体信息或依赖检查时遵守权限；无法读取文件或设置批次时明确说明缺少的权限或条件。
