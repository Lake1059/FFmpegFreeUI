[《如何在 GitHub 下载软件》<br>小白提醒：你的需求是下载这个软件去使用而不是要工程文件，点此去 Releases 下载发行版才是下载软件；上面的 Code 是下载源代码；我都写成这样了如果你在 Releases 里又下成了源代码，那说明不满足使用本软件的最低要求，连 Windows 可执行文件的后缀是什么、压缩包的概念都不清楚，都是手机害的。](https://github.com/Lake1059/FFmpegFreeUI/releases)

## FFmpegFreeUI

![GitHub Downloads](https://img.shields.io/github/downloads/Lake1059/FFmpegFreeUI/total?label=全部下载量)

FFmpegFreeUI（简称 3FUI）是在 Windows 上的 [FFmpeg](https://ffmpeg.org/) 的专业交互外壳，使用 .NET 10 框架，使用纯 WinForm 开发，配合 SunnyUI 和自绘制打造专业高效的暗黑风格界面。

设计定位：3FUI 的目标是做一款轻度专业参数调整的转码软件，并非行业深度专业软件；让普通人能够通过图形化界面接触到较为全面的通用参数来轻松压制和转换格式。

> 广告词：难以忍受格式工厂的烦人捆绑？行业专业软件限制太多？小作坊产品过于业余？来用 3FUI 就对了，专注转码，不干别的，真免费，真自由，真专业，真开源！

## 关于用户离开电脑时间过长导致软件容易被关的提示

使用时间一长就被关的主要原因是系统息屏和杀毒软件拦截<br>请检查这些事项：

- 0.6 已经改进了许多，但仍要继续关注
- 任务进行时关闭杀毒软件，或添加此程序到白名单
- 尽可能不要在任务期间插拔显示器或是更改显示器设置
- 如果不接显示器可能会有一定影响，还在继续收集样本

## 特点

- **底层逻辑基于预设**<br>保存到 json 文件中，由用户自行管理，以及与他人分享<br>注意：不保证跨版本的兼容，因为参数项目和数据类型可能更改

- **专业参数，而非业余表述**<br>低中高是什么玩意，真实参数才是真理<br>且大多数地方直接标出参数名称，更易于上手

- **智能交互**<br>至少可以在一定程度上阻止炸膛的发生

- **准确显示 ffmpeg 输出的信息**<br>更易于尝试新方案

- **实时计算剩余时间**<br>

  ```visual basic
  剩余时间 = Max(总时长 - 已处理时长, 0) / 实时速度比值
  ```

  你的时间非常值钱

- **预估最终输出大小**<br>

  ```visual basic
  最终大小 = 已生成大小 / 进度百分比
  ```

  当然这肯定是不准的，但有个大概总比没有强

- **可暂停！是的！真正可暂停**<br>虽然 ffmpeg 自身并不支持暂停，但是 ntdll 可不会让着谁<br>这让你可以先暂停去奖励一下然后再继续烧机，而不是全程被硬控

- **完成后自动开始下一个**<br>你先睡觉，让它自己转

- **干净无垃圾，不保存任何信息**<br>没有用户设置，没有软件缓存，更不会碰注册表<br>不会在任何地方扔垃圾，也不会收集任何信息

## 截图

<img src="IMG\mi.png"  />

![](IMG\vp.png)

## 准备步骤

1. 首先需要明确，这只能用于 Windows，我没有能力开发其他系统的版本。
2. 前往 [FFmpeg 官网](https://ffmpeg.org/) 下载最新的发行版，gyan.dev 和 BtbN 两者的发行皆可。
   - 若选择 gyan.dev 的发行版，应该下载 ffmpeg-release-full.7z
   - 若选择 BtbN 的发行版，应该下载 ffmpeg-master-latest-win64-gpl.zip
3. 找到压缩包中的 ffmpeg.exe 可执行文件。
4. 将 ffmpeg.exe 和我的 FFmpegFreeUI 放在同一个文件夹中。<br>或者将 ffmpeg.exe 加入环境变量中也可。
5. 然后就可以正常使用了。

## 反馈

- 如果我的设计对你有帮助，请考虑资金支持一下：[前往爱发电](https://afdian.com/a/1059Studio)
- 3FUI 没有针对酒吧的炒饭进行预防，非正常操作极易引发报错
- 要反馈任何问题，请到Q群：1050613952

### 新手必看：发生错误时如何寻求帮助

选中一个错误的任务 然后将调试信息完整地截图 然后发给技术人员！<br>最好带上命令行，如果你不想让别人看到你的文件名，可以手动抹掉或在预设管理中复制！<br>如果有条件，请提供输入文件的详细参数，很多播放器都可以查看！

## 许可

- 3FUI 使用 MIT 开源许可，可以自由地使用和分发此软件
- 仅发布于 GitHub，在其他平台看到的源代码都不是本人！

## 视频编码器

| 编码器类别 | 提供的编码                                                   | 备注                                                         |
| ---------- | ------------------------------------------------------------ | ------------------------------------------------------------ |
| 复制流     | copy                                                         |                                                              |
| H.266/VVC  | libx266<br>libvvenc                                          | 266 还不成熟，需要大量性能，也没几个播放器放得出来           |
| AV1        | libaom-av1<br>av1_nvenc<br>av1_qsv<br>av1_amf<br>libsvtav1<br>rav1e | 如果没有可用的新显卡进行硬件加速，强烈建议考虑软件编码 **libsvtav1** |
| H.265/HEVC | libx265<br>hevc_nvenc<br>hevc_qsv<br>hevc_amf                |                                                              |
| H.264/AVC  | libx264<br>h264_nvenc<br>h264_qsv<br>h264_amf                |                                                              |
| ProRes     | prores_ks                                                    |                                                              |
| VP9        | libvpx-vp9                                                   |                                                              |
| 禁用       | -vn                                                          |                                                              |

### 新手常问：能不能显卡加速？有没有 N /A/ I 卡？

答：lib = CPU，nvenc = NVIDIA，amf = AMD，qsv = INTEL，再问紫砂

## 音频编码器

复制流、AAC、LAME MP3、FLAC、ALAC、WAV 16bit、WAV 24bit、WAV 32bit Float、Dolby Digital AC3、DTS Coherent Acoustics、Opus、Vorbis、True Audio、禁用

## 图片编码器

PNG、JPEG\JPG、WEBP、TIFF、AVIF、GIF、BMP、JPEG-LS、DPX、OpenEXR

## 比特率控制方式

| 方式            | 说明                                                         |
| --------------- | ------------------------------------------------------------ |
| 动态码率 VBR    | 存储首选，硬件加速首选<br/>配合 -cq 或 -qp 使用，VBR HQ 同理 |
| 动态码率 VBR HQ | 硬件加速专用<br>仅限 NVIDIA、INTEL、AMD 的 AV1、HEVC、H264 硬件加速编码器使用<br>其他编码器选这个等同于 ABR |
| 恒定质量 CRF    | 存储首选，软件编码首选<br>-rc 并没有 crf 这个值，而是使用 -crf <?>，你需要在质量控制里填写它 |
| 恒定量化 CQP    | 不推荐，主用于研究和特定场景<br/>仅向 NVIDIA 和 AMD 的 AV1、HEVC、H264 硬件加速编码传递对应的参数<br/>其他编码器选这个等同于 ABR |
| 平均码率 ABR    | 相当于没有 -rc 参数，经典的转码                              |
| 二次编码 TPE    | 也不建议，二次编码不见得总会比单次编码好<br>只对 NVIDIA 的 HEVC 和 H264 硬件加速编码器传递参数<br>其他编码器选这个等同于 ABR |
| 恒定速率 CBR    | 应该没什么人用这玩意，我知道还有个 CBR HQ，但恒定速率对于普通人来说真没什么意义 |

### 前瞻分析帧数

建议搭配 VBR HQ 使用，通常考虑为原视频的帧率<br>AMD 的硬件加速编码器不要写这个参数，写了也不会传递，已经预置高质量参数了

### 质量设定 -crf / -cq / -qp

质量的默认值是 23，肉眼无损是 16，但仍需根据具体编码器调整<br>不要设置为 0！除非你知道自己在做什么以及需要什么

### 新手常问：要怎么选？什么对应什么？设置多少比特率才是最高画质？

答：硬件加速选 VBR 和 HQ 搭配 -cq 使用，软件编码选 CRF；要想同时拥有画质和降低大小就不要想着设置比特率，去设置质量值，让编码器自己算；没有通用的方案也没有最好的方案，编码时间、画面质量、文件体积、解码性能，你总要至少放弃一样，不要想着全都要，都是按照自己的需求慢慢试出来的，只能给参考，没有作业抄。

## 视频滤镜

- 画面缩放维持比例 scale
- 画面裁剪 crop
- 智能抽帧 select='gt(scene,?)',setpts=N/FRAME_RATE/TB
- 色彩管理 zscale
- 降噪 hqdn3d、nlmeans、atadenoise、bm3d、AviSynth（avs）
- 锐化 unsharp
- 转逐行 yadif
- 转隔行 tinterlace

## 音频滤镜

- 响度标准化 loudnorm

## 如何使用画面裁剪可视化交互

![](IMG\sc.png)

画面裁剪滤镜 crop 的四个参数值是：<kbd>裁剪宽度:裁剪高度:左上角X:左上角Y</kbd>

在分辨率板块中打开此窗口，此界面独立，仅向主窗口上的那个文本框传递最终值，与已添加的文件无关。首先需要打开一个视频文件用于获取一帧预览图用来裁剪，在此之前可以设定时间戳来或者指定位置的帧，如果不设置默认获取第10秒的第一帧。此功能需要 ffmpeg 才能获取到视频帧。

获取到视频帧后，图片将以原分辨率显示，在图片上使用鼠标左右键来调整红框的尺寸，红框就是要裁剪的目标区域。左键控制左上角坐标，右键控制右下角坐标，如果按错了或者在越界的位置按下并拖动鼠标，则会自动互换功能。可以手动更改参数文本框的值然后按 Enter 键来微调红框，也就是可以反向操作。如果需要让红框保持固定比例或居中，也提供了相应的功能。在窗口右上角还有红框定位的放大镜视图，更方便精确定位。

我都写成这样了，软件里又写一遍，再不会用就去紫砂。

## 如何通过 avs 滤镜脚本文件使用 AviSynth+ 滤镜

AviSynth 选项在降噪设置里提供，最后一个选项就是。3FUI 只能使用 avs 文件来加载，在开始前必须明确，3FUI 是设计用于批量处理的转码工具，因此这个加载逻辑也必然是这样设计的。当使用此滤镜时，avs 文件将取代输入文件的位置，3FUI 默认您不会在 avs 里处理音频，为了避免新手直接用发现视频声音没了，因此当选择此降噪时自动覆盖填写自定义参数和流控制参数，请注意检查。

首先下载这些 dll 文件：

- AviSynth.dll（在发行版中有个名为 filesonly 的那里面找）<br>https://github.com/AviSynth/AviSynthPlus/releases
- LSMASHSource.dll<br>https://github.com/HomeOfAviSynthPlusEvolution/L-SMASH-Works/releases<br>此文件用于读取视频文件
- knlmeanscl.dll<br>https://github.com/Khanattila/KNLMeansCL/releases<br>此文件用于降噪
- fmtconv.dll<br>https://gitlab.com/EleonoreMizo/fmtconv<br>用于色彩空间转换，如果你需要的话
- ffms2.dll<br>https://github.com/FFMS/ffms2/releases<br>如果你需要在 avs 里处理音频的话，可以用这个，当然是如果你需要的话

将这些文件与 ffmpeg 放在一起！

在 3FUI 的目录下创建 AviSynth.avs 文件，此文件作为模板提供给 3FUI，在其中填写以下内容：

```vb
LoadPlugin("C:\FFmpeg\LSMASHSource.dll")
LoadPlugin("C:\FFmpeg\knlmeanscl.dll")
LWLibavVideoSource("<FilePath>")
KNLMeansCL(d=3,a=2,s=4,h=2,channels="UV",wmode=3)
```

- LoadPlugin 方法用来加载这些 dll，不需要在这里加载 AviSynth.dll<br>dll 的路径请写绝对路径！
- LWLibavVideoSource 加载视频文件，直接把第三行复制过去，3FUI 会自动替换这个文件路径
- KNLMeansCL 就是降噪方法，其参数如下：

原文：https://github.com/Khanattila/KNLMeansCL/wiki/Filter-description

```c#
KNLMeansCL(clip, int d, int a, int s, float h, string channels, int wmode, float wref, clip rclip, string device_type, int device_id, bool lsb_inout, bool info)
```

| 参数名      | 说明                                                         |
| ----------- | ------------------------------------------------------------ |
| clip        | 不写                                                         |
| d           | 前后参考帧数量，d=0 使用 1 帧，d=1 使用 3 帧，n=2*d+1，以此类推，值越大降噪效果越好 |
| a           | 搜索像素半径，a=1 使用 9 像素，a=2 使用 25 像素，n=(2*a+1)^2，以此类推，值越大降噪效果越好 |
| s           | 相似半径，默认 = 4，对性能的影响很小，这取决于噪声的性质     |
| h           | 过滤强度，默认 = 1.2，值越大，去除的杂色越多                 |
| channels    | 设置要去噪的颜色通道。可能的值为 YUV、Y、UV                  |
| wmode       | 0 := Welsch weighting function has a faster decay, but still assigns positive weights to dissimilar blocks. Original Non-local means denoising weighting function.<br/>1 := Modified Bisquare weighting function to be less robust.<br/>2 := Bisquare weighting function use a soft threshold to compare neighbourhoods (the weight is 0 as soon as a given threshold is exceeded).<br/>3 := Modified Bisquare weighting function to be even more robust. |
| wref        | 默认 =  1，相对于找到的最相似像素的权重，影响滤镜输出的原始像素量 |
| rclip       | 不写                                                         |
| device_type | accelerator、cpu、gpu、auto                                  |
| device_id   | 显卡索引                                                     |
| lsb_inout   | 没必要                                                       |
| info        | 输出额外信息                                                 |

3FUI 目前不会自动删除为每个文件生成的 avs 文件，这是为了方便检查有没有正常运行。
