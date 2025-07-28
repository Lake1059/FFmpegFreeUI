[《如何在 GitHub 下载软件》小白提醒：你的需求是下载这个软件去使用而不是要工程文件，点此去 Releases 下载发行版才是下载软件；上面的 Code 是下载源代码；我都写成这样了如果你在 Releases 里又下成了源代码，那真的是人机，都是手机和短视频害的。](https://github.com/Lake1059/FFmpegFreeUI/releases) 本文已包含新手内容，就看有没有心思学了。

<img src="FFmpegFreeUI\Resources\AppIcon.png" style="zoom: 25%;" />

## FFmpegFreeUI

请认准官网域名：https://ffmpegfreeui.top 和 https://3fui.top

![](https://img.shields.io/github/downloads/Lake1059/FFmpegFreeUI/total?label=所有文件总下载量)

FFmpegFreeUI（简称 3FUI）是在 Windows 上的 [FFmpeg](https://ffmpeg.org) 的专业交互外壳，使用 .NET 10 框架，使用纯 WinForm 开发，配合 SunnyUI 和自绘制打造专业高效的暗黑风格界面。为了打破市面上转码软件的臃肿捆绑、广告水印、限制离谱、乱收费、业余糊弄的局面，3FUI 永久保持干净和自由，同时还收录了最新的编码和参数改动，是追求纯净和专业的不二之选。

3FUI 的目标是做一款轻度专业参数调整的转码软件，让普通人能够通过图形化界面接触到较为全面的通用参数来轻松压制和转换格式。3FUI 是纯填参数的，没有内置傻瓜方案，不是让小白一键无脑全自动的，如果没有相关技术参数的概念，请先在本文的新手内容中学习相关技术知识。

3FUI 的底层逻辑就是基于预设的，可以很方便地与他人分享方案，导出的预设自行管理；多数地方直接标出参数名以及准确显示 ffmpeg 输出的信息，更易于上手和尝试新方案；实时计算剩余时间、预估最终大小、可暂停任务、最多自动同时开始 10 个任务；不会向输出文件里写入软件信息；缓存自动收拾，不碰注册表，不在任何地方扔垃圾（崩溃转储除外，这不是我能控制的），也不会收集任何信息。

支持开发插件来扩展功能，可接入编码队列。VB 和 C# 都可以写，还额外支持 WPF 界面。

如果我的设计对你有帮助，请帮我宣传或者考虑资金支持。

## Linux & macOS

[Wine](https://www.winehq.org) 是一个在多种 POSIX-compliant 操作系统上运行 Windows 应用的兼容层，如果能够在 macOS 和 Linux 上安装并正确使用，理论上可以直接在这些操作系统上使用 3FUI。关于这部分的内容请进群跟群友讨论，我买不起苹果电脑也用不来 Linux。

关于在 Linux 中使用 Wine 转译运行 3FUI 的方法可参阅 [linux.md](https://github.com/Lake1059/FFmpegFreeUI/blob/main/linux-doc/linux.md)

## 下载说明

由于加入了插件支持以及其实现方法，需要保留一些 dll 来确保插件功能可用并稳定运行。仍旧可以把很大的 EXE 文件拿出来单独运行，不过这也就代表放弃插件，在这种情况下加载插件必定卡死崩溃。

[ReadyToRun](https://learn.microsoft.com/zh-cn/dotnet/core/deploying/ready-to-run) 就是原来的单文件版编译，不过现在保留了一些 dll 还是叫专业术语比较好；[SelfContained](https://learn.microsoft.com/zh-cn/dotnet/core/deploying/#publish-self-contained) 是备选项，如果 ReadyToRun 无法在你的电脑上运行，那么尝试 SelfContained。

我还额外提供了一个 Debug 版本，这是调试版本，也许有人会需要。

PluginExample 是我做的示例插件；在程序目录下创建 Plugin 文件夹，然后把插件放进去重启 3FUI 就可以加载了，插件也要注意架构；插件的后缀是 **.3fui.dll**，看不见后缀的自己去面壁。

## 猜你想问

> **为什么不提供需要安装运行库的最小体积版本？**  
> 让用户去安装一个还未发布正式版的运行库不是一个好的选择，压根没人会记得去更新。等到什么时候微软把这东西集成到 Windows 里了我就发布最小体积版本。

> **文件为什么这么大？隔壁只有一百多KB**  
>其实本体也只有几百KB，但我要求自带运行时的发布，你想要的话可以改项目设置自己编译。

> **全都无法运行怎么办？**  
> 依次检查这些项目：
>
> 1. 检查 Windows 更新中是否有 **.NET Framework 3.5 和 4.8 的累积更新**，如果有则立即安装
> 2. 从正式版 1.0 开始，编译为目标最低系统版本是 Win10 1809，更低版本看天意
> 3. 检查杀毒软件拦截记录
> 5. 把仓库扒下来自己编译
> 6. 没救了，重装系统吧

> **如何自行编译这个项目？**  
>
> 1. 下载并安装 **Visual Studio 2022**（目前 .NET 10 是预览版，所以需要预览版的 VS）
> 2. 工作负载只需要 **.NET 桌面开发**，可选组件全都可以扔掉（看自己需求）
> 3. 手动去下载并安装 **.NET 10 SDK**
> 4. 然后直接打开 **.sln** 文件，剩下的依赖会自动补齐（需联网下载）
> 5. 开发过程中是用 AnyCPU 生成来调试的，所以你什么都不用调直接运行**全部重新生成**就行了

> **长时间使用的崩溃问题解决了吗？**  
> 已彻底解决，可放心拿去跑重要任务；甚至能抗住资源管理器爆炸了。当然不是保证所有人的电脑都这样，反正我自己的电脑都没问题。如果长时间运行依旧崩溃，到群里发 Windows 事件查看器记录的错误，不方便加群就写 issues，错误信息提供完整，各种代码信息全都要。

## 截图

<img src="IMG\mi.png"  />
<img src="IMG\fe.png"  />
<img src="IMG\vp.png"  />
<img src="IMG\vp2.png"  />
<img src="IMG\pe.png"  />

## 准备步骤

1. 首先下载 3FUI；不管我重复多少次永远都会有人下成源代码，md
   - 还有一大堆人连处理器架构的概念都没有，tmd

2. 前往 [FFmpeg 官网](https://ffmpeg.org/) 下载最新的发行版，gyan.dev 和 BtbN 两者的发行皆可
   - 若选择 gyan.dev 的发行版，下载 ffmpeg-git-full.7z
   - 若选择 BtbN 的发行版，下载 ffmpeg-master-latest-win64-gpl.zip
   - 不要选带 lgpl 名称的，不然你又要来问怎么连 libx264 都跑不起来
3. 将压缩包中的 ffmpeg.exe 和我的 FFmpegFreeUI 放在同一个文件夹中<br>或者将其加入环境变量中也可以，推荐是加环境变量
4. 然后就可以开始使用了

## 软件机制 & 使用提示

- 编码队列里每个任务都有自己的预设数据快照，而命令行是开始任务的时候当场根据这个快照生成的，而不是再读取参数面板，也就是说任务一旦添加，其参数就已经等于锁死了，不能再更改，因此要注意如果要更改参数必须重新添加任务
- 编码队列的列表视图也可以直接拖文件的，会直接用当前参数面板生成快照并尝试立即开始
- 性能监控里显卡的占用只要有显示就说明那个核心在工作，0% 是占用太小四舍五入没了，不是没在干活，没干活的核心会自动移除显示
- 很多跨线程的界面刷新是基于 SynchronizationContext 实现的，但是这东西可能有自己的想法，为了避免这玩意自行销毁，我在旁边放了个监控，一旦软件报错UI同步上下文丢失请立即停止操作并重启软件；其实这就像薛定谔的猫一样，只要有东西观察就不会坍缩，反正我是没有遇到突然丢失的情况
- 开始任务有延迟是正常的，这部分代码是交给后台线程的，状态已经改变了只是没有刷新到界面上，当然你点了多次开始也不会出事，写了判断的

## 反馈

- 3FUI 没有针对酒吧的炒饭进行预防，非正常操作极易引发报错
- 故意卡 bug 造成的任何损失均与我无关
- 要反馈任何问题，请到Q群：1050613952
- **不要在 B站 汇报问题！** 评论很容易被刷掉；私信也基本是让加群

### 新手必看：如何寻求帮助

选中一个错误的任务 然后将调试信息完整地截图 然后发给技术人员！<br>最好带上命令行，如果你不想让别人看到你的文件名，可以手动抹掉或在预设管理中复制！<br>如果有条件，请提供输入文件的详细参数，很多播放器都可以查看！

> 请勿让 **我** 或 **群友** 或 **专业人士** 或 **外行人士** 进行包括但不限于这些行为：算卦、猜谜、托梦、占卜、人脑推理、强行传教、交流物理学、灵能飞升、虚空扰动、尝试进入量子隧道等等，如有以上行为或类似行为的，造成的全部后果由用户全责承担。

## 许可和引用

- 3FUI 使用 MIT 开源许可（仅限我的代码），可以自由地使用和分发此软件
- SunnyUI 是 GPL 且具有商用授权属性，因此可能受到相关限制
- 仅在 GitHub 开源，在其他平台看到的源代码都不是本人！

| 引用程序集                                                   | 许可证         | 作用                       |
| ------------------------------------------------------------ | -------------- | -------------------------- |
| [SunnyUI](https://gitee.com/yhuse/SunnyUI)                   | GPL-3.0-only   | 界面主框架                 |
| [WindowsAPICodePack](https://github.com/contre/Windows-API-Code-Pack-1.1) | 微软软件许可证 | 提供更舒适的文件夹选择对话框 |
| [LibreHardwareMonitorLib](https://github.com/LibreHardwareMonitor/LibreHardwareMonitor) | MPL-2.0        | 性能监控                   |

## 新手入门

如果是纯新手，对视频的技术参数没有任何了解，建议先学习以下内容

- 极客湾 | 视频基础参数科普 | [BV1nt411Q7S6](https://www.bilibili.com/video/BV1nt411Q7S6)
- 极客湾 | 电影和游戏的帧数效果差别 | [BV19x411L7fH](https://www.bilibili.com/video/BV19x411L7fH)
- 影视飓风 | 视频的封装与编码 | [BV1ws41157f8](https://www.bilibili.com/video/BV1ws41157f8)
- 影视飓风 | 色深和色度采样 | [BV1ds411T7F4](https://www.bilibili.com/video/BV1ds411T7F4)
- 影视飓风 | 帧率的旧事 | [BV1hp4y1f7B5](https://www.bilibili.com/video/BV1hp4y1f7B5)
- 大河李斯特视频工作室 | 视频格式与基础参数科普 | [BV1Ug4y1L7aK](https://www.bilibili.com/video/BV1Ug4y1L7aK)
- 大河李斯特视频工作室 | 视频压缩、色度半采样、视频中的高低频 | [BV1FM41137MM](https://www.bilibili.com/video/BV1FM41137MM)
- 大河李斯特视频工作室 | FFmpeg 是什么 | [BV1SjdoY9Erj](https://www.bilibili.com/video/BV1SjdoY9Erj)
- 终末诗 | 适用于小白的视频压缩教学 | [知乎](https://zhuanlan.zhihu.com/p/1913258114746122747)  此文章包含大量测试结果总结和设置教学<br>
  新手把这篇文章看完能学会很多东西，继续往下看之前先把这个打开看！！

另外我仓库 IMG 文件夹里也有部分编码器的测试结果，数据来自群友。

## 视频编码器

| 编码器类别 | 提供的编码                                                   | 备注                                                         |
| ---------- | ------------------------------------------------------------ | ------------------------------------------------------------ |
| 复制流     | copy                                                         | 我不想看到有人问能不能不重编码。                             |
| H.266/VVC  | libx266<br>libvvenc                                          | 截止 2025 年，H.266 还不成熟，需要大量性能，也没几个播放器放得出来，也没有硬件加速可用。可能未实现 x266。 |
| AV1        | libaom-av1<br>libsvtav1<br>av1_nvenc<br>av1_qsv<br>av1_amf<br>librav1e<br>av1_vaapi | 如果没有新显卡进行硬件加速，强烈建议考虑软件编码 **libsvtav1**，我默认给的 preset 是 4，这是最平衡速度和体积的选择，如果只是为了尝鲜，可以先给个 6。 |
| H.265/HEVC | libx265<br>hevc_nvenc<br>hevc_qsv<br>hevc_amf<br/>hevc_d3d12va<br/>hevc_vaapi<br/>hevc_vulkan | 目前 265 是非常合适的。d3d12va 要求 Win11 24H2，但其效率并没有比三家硬件加速高出多少。 |
| H.264/AVC  | libx264<br>h264_nvenc<br>h264_qsv<br>h264_amf<br/>h264_vaapi<br/>h264_vulkan | 只要小参数调的好，libx264 仍旧是非常能打的，但从技术上讲 264 快要无法满足专业需求了。 |
| ProRes     | prores_ks<br>prores_aw                                       | aw 是 macOS 上用的，Windows 和 Linux 要用 ks                 |
| FFV1       | ffv1 -level 3<br>ffv1 -level 1                               | FFV1 是无损编码，这种编码常用于博物馆级别的存档，不适合个人存储，与其他编码的用途不同。对于个人而言可以作为中转来使用。 |
| VP9        | libvpx-vp9<br/>vp9_vaapi                                     | VP9 是谷歌的格式，在油管上大量使用，其压缩度高于 264。       |
| RMVB       | rv20<br>rv10                                                 | RMVB 是早些年的高压缩格式，但后来缺乏维护已被淘汰。          |
| MPEG       | mpeg4<br>libxvid<br>libxeve                                  | mpeg4 和 libxvid 都是早些年的主流编码，就像现在的 264 一样。<br>libxeve 是 MPEG-5，高通、华为、三星参与制定，介绍是比 265 更先进，但直到 2025 年也没有看到大规模使用，至于效率，跟 266 坐一桌。 |
| WMV        | wmv2<br>wmv1                                                 | Windows 的专用视频编码，已经过时了。                         |
| 禁用       | -vn                                                          | 啪，没了~                                                    |

> **新手常问：为什么说 FFV1 才是无损编码？**
> 答：这里的有损和无损是技术上的，不是视觉上的。生活中看的视频都是有损压缩，它们都在欺骗视觉，人眼的能力是非常局限的，就像你眼睛贴到屏幕上才能细微察觉所有的颜色都是用红绿蓝混合出来的。技术上的无损编码是确保了每个像素的信息正确，而生活中的视频每次重编码都会让像素产生变化，所以都是有损压缩，你也不希望一分钟的视频有1GB吧。

| 区分标记 | 适用的硬件             |
| -------- | ---------------------- |
| lib      | CPU                    |
| nvenc    | NVIDIA                 |
| amf      | AMD                    |
| qsv      | Intel                  |
| vaapi    | Linux API              |
| vulkan   | 受支持的显卡           |
| d3d12va  | 受支持的显卡和操作系统 |

### NVIDIA NVENC 规格

有关 N卡 硬件加速的详细规格可以在官方页面上查询<br>
https://developer.nvidia.com/video-encode-and-decode-gpu-support-matrix-new

所以是的，3090 = 3060

### 预设 -preset

控制编码器如何平衡编码速度与压缩效率，这通常与画质无关或影响很小。你是需要用更长的时间换更小的文件，还是用更大的文件换更短的时间。通常情况下对编码速度的影响很大，而对压缩效率的影响较小，尤其是硬件加速的编码器，相反对于软件编码的影响则很大。

### 配置文件 -profile

控制编码器的配置档次，也就是需要支持哪些特性，这通常会影响编码复杂度，从而影响到在各种设备上播放的兼容性。例如典型的是否支持 10bit 色深。保留原文件的特性一般直接不写即可。

### 场景优化 -tune

控制编码器如何针对特定的视频内容或播放用途进行专门优化。视频内容方面比如电影、动画、胶片颗粒、静态图片；用途方面比如更快的解码速度、更低的延迟等。通常情况下不需要考虑。

### 像素格式 -pix_fmt

设定像素如何存储，这会影响颜色空间、位深、通道排列等。例如生活中绝大多数视频都是 yuv420p，为 8bit 色深 4:2:0 采样；对于 HDR 视频常用的是 yuv420p10le。这还直接影响每个像素的数据量，444 的视频体积必然比 420 大出很多；422 和 444 通常是专业工作者接触的。

## 音频编码器

复制流、AAC、LAME MP3、FLAC、ALAC、WAV 16bit、WAV 24bit、WAV 32bit Float、Dolby Digital AC3、DTS Coherent Acoustics、Opus、Vorbis、True Audio、禁用

## 图片编码器

PNG、JPEG\JPG、WEBP、TIFF、AVIF、GIF、BMP、JPEG-LS、DPX、OpenEXR

## 比特率控制方式

| 方式            | 说明                                                         |
| --------------- | ------------------------------------------------------------ |
| 动态码率 VBR    | 存储首选，硬件加速首选<br/>配合 -cq 或 -qp 使用，VBR HQ 同理 |
| 动态码率 VBR HQ | 硬件加速专用，但其实并不会比 VBR 提升太多<br>仅限 NVIDIA、INTEL、AMD 的 AV1、HEVC、H264 硬件加速编码器使用<br>其他编码器选这个等同于 ABR |
| 恒定质量 CRF    | 存储首选，软件编码首选<br>-rc 并没有 crf 这个值，而是使用 -crf <?>，你需要在质量控制里填写它 |
| 恒定量化 CQP    | 不推荐，主用于研究和特定场景<br/>仅向 NVIDIA 和 AMD 的 AV1、HEVC、H264 硬件加速编码传递对应的参数<br/>其他编码器选这个等同于 ABR<br/>*其实并没有 CQP 这玩意* |
| *平均码率 ABR*  | 已在 1.1 版本中移除，其实不选控制方式就是这个                |
| *二次编码 TPE*  | 已在 1.1 版本中移除，本身就跑不起来，就这点钱很难办的        |
| 恒定速率 CBR    | 应该没什么人用这玩意，我知道还有个 CBR HQ<br/>但恒定速率对于普通人来说真没什么意义 |

### 前瞻分析帧数

建议搭配 VBR HQ 使用，通常考虑为原视频的帧率<br>AMD 的硬件加速编码器不要写这个参数，写了也不会传递，已经预置高质量参数了

### 质量设定 -crf / -cq / -qp

质量的默认值是 23，肉眼无损是 16，但仍需根据具体编码器调整<br>实在不知道写什么就直接 23 扔上去看结果再调整，数字越大越模糊<br>不要设置为 0！除非你知道自己在做什么以及需要什么

> **新手常问：要怎么选？什么对应什么？怎样才是最高画质？怎么降低文件大小？**
> 硬件加速选 VBR 或 HQ 跟 -cq 使用，软件编码选 CRF 跟 -crf 使用。
>
> 所谓压制是为了让文件变得更小，不要想着设置比特率，去设置质量值，让编码器自己算。但是如果画面有过多噪点则不适用这种方法，噪点是干扰，会导致比特率飙升。
>
> 已经被压过的视频几乎不可能再压得更小，例如流媒体平台上扒下来的和压制组发布的动漫资源，继续压就是让编码器没事找事，哪怕 264 压成 av1 也很可能不会有可观的结果，除非你有压制组的参数。
>
> 没有通用的方案也没有最好的方案，编码时间、画面质量、文件体积、解码性能，你总要至少放弃一样，不要想着全都要，都是按照自己的需求慢慢试出来的，只能给参考，没有作业抄。

## 视频滤镜

- 画面缩放维持比例 scale
- 画面裁剪 crop
- 智能抽帧 mpdecimate=frac=? 和 -vsync vfr
- 插帧 minterpolate，这个补帧效果非常一般，但非常稳定，速度极快，使用 CPU 处理，完全没有果冻，对于要求不高的临时观影可以用一用，仅适用于动静小的视频，不适用于3D游戏录制、动静大的电影等
  - 最佳质量选项：运动补偿插值+加权obmc

- 动态模糊 tblend，只有与前一帧的平均值出来的画面是正常的，其他的需要专门调整
- 色彩管理 zscale
- 降噪 hqdn3d、nlmeans、atadenoise、bm3d、AviSynth（avs）
- 锐化 unsharp
- 转逐行 yadif
- 转隔行 tinterlace

关于使用 AviSynth+ 降噪滤镜可参阅 [AviSynth+.md](https://github.com/Lake1059/FFmpegFreeUI/blob/main/linux-doc/AviSynth+.md)

## 音频滤镜

- 响度标准化 loudnorm

## 画面裁剪可视化交互

<img src="IMG\sc.png"  />

画面裁剪滤镜 crop 的四个参数值是：**裁剪宽度:裁剪高度:左上角X:左上角Y**

在分辨率板块中打开此窗口，此界面独立，仅向主窗口上的那个文本框传递最终值，与已添加的文件无关。首先需要打开一个视频文件用于获取一帧预览图用来裁剪，在此之前可以设定时间戳来指定某个位置的帧，如果不设置默认获取第10秒的第一帧。此功能需要 ffmpeg 才能获取到视频帧。

获取到视频帧后，图片将以原分辨率显示，在图片上使用鼠标左右键来调整红框的尺寸，红框就是要裁剪的目标区域。左键控制左上角坐标，右键控制右下角坐标，如果按错了或者在越界的位置按下并拖动鼠标，则会自动互换功能。可以手动更改参数文本框的值然后按 Enter 键来微调红框，也就是可以反向操作。如果需要让红框保持固定比例或居中，也提供了相应的功能。在窗口右上角还有红框定位的放大镜视图，更方便精确定位。

我都写成这样了，软件里又写一遍，再不会用就去紫砂。

## 剪辑区间

暂时还没做可视化交互，先用播放器对着写吧，或者直接找剪辑软件。

## 插件开发

通过插件，你可以给 3FUI 添加各种功能来满足自己的需求，只需要像我那样把可视化摆上来然后生成对应的参数即可，还可以选择接入我的编码队列，而不用自己做进度显示。

<img src="IMG\vs_pe.png" />

考虑到 ReadyToRun 生成的 exe 无法被添加引用，插件使用 反射 + 特性 + 动态调用 来实现，你在开发插件的时候不需要引用 3FUI，只需要按照我制定的接口标准写代码即可。目前总共只有 3 个接口功能，非常简单，通常情况下你只需要用其中 2 个，所以不要担心要硬啃代码。

首先你需要有与我开发 3FUI 相同的集成开发环境：

1. 下载并安装 [Visual Studio 2022](https://visualstudio.microsoft.com/zh-hans/vs/)（注意在安装程序里选择预览版）
2. 工作负载勾选：.NET 桌面开发
   - 可选组件看自己需求，可以能不要就都不要的
   - 但是我仍旧推荐这些组件：IntelliCode、.NET 可移植库目标包、.NET 分析工具
3. 完成 VS2022 的安装
4. 手动下载并安装 [.NET 10 SDK](https://dotnet.microsoft.com/zh-cn/download/dotnet/10.0)
   - 截止目前 .NET 10 还是预览版，没有包含在 VS 的安装程序中
   - 等到正式版的时候就可以在可选组件里勾选了，到时候也不需要预览版的 VS 了

然后就可以开始开发了，从新建工程开始：

1. 使用 VB 或 C# 创建一个 **Windows 窗体应用** 项目

   - 如果你更擅长 WPF，也可以选择那个
   - 但我不会 WPF，后面的代码如果不是这么写的那你得自己想办法
   - 目标框架必须和 3FUI 一样，选 .NET 10
   - 我知道你会疑惑为什么开发插件要选窗体应用而不是类库，其实这是 VS 的限制，要做界面你必须选这个，否则就没有可视化什么事了

2. 创建项目后把默认窗体 Form1 关掉，我们不需要它

   - 但是也不要删掉，否则你就没法生成了
   - 或者你可以用它来做测试，不然你没法调试自己的设计
   - 事实上我们并不需要“启动”这个项目

3. 新建一个 Entry 类

4. 在这个 Entry 类中写一个 Entry 方法，需要是共享\静态的

   - VB 语言如下：

     ```vb
     Public Shared Sub Entry()
     	'初始化的代码都写这里，在 3FUI 启动后执行
     End Sub
     ```

   - C# 语言如下：

     ```c#
     public static void Entry()
     {
     	// 初始化的代码都写这里，在 3FUI 启动后执行
     }
     ```

现在，你就完成了插件初始化的方法，确认一下：是名为 Entry 的 Class 里有名为 Entry 的方法。接下来就是三个接口来实现关键的功能，直接把下面的代码复制过去自己改就行了。

### 添加自定义 WinForm 界面

要向 3FUI 的插件扩展选项卡添加自己的界面，必须创建你自己的用户控件，将你的功能全部集成到这个控件里，然后将下列代码复制到你的 Entry 类中：

VB 语言：

```vb
Public Shared Property HostCall_AddCustomWinformPanel As Action(Of String, Control)
Public Shared Sub SetHost_AddCustomWinformPanel(action As Object)
	HostCall_AddCustomWinformPanel = CType(action, Action(Of String, Control))
End Sub
Public Shared Sub AddCustomWinformPanel()
	HostCall_AddCustomWinformPanel.Invoke("在下拉框中显示的名称", New 自定义控件)
End Sub
```

C# 语言：

```c#
public static Action<string, Control> HostCall_AddCustomWinformPanel { get; set; }
public static void SetHost_AddCustomWinformPanel(object action)
{
	HostCall_AddCustomWinformPanel = (Action<string, Control>)action;
}
public static void AddCustomWinformPanel()
{
	HostCall_AddCustomWinformPanel?.Invoke("在下拉框中显示的名称", new 自定义控件());
}
```

- 不可以更改 HostCall 定义和 SetHost 方法，否则 3FUI 无法正确调用
- 添加自定义界面的方法必须在 Entry 方法中调用，因为我不会在其他地方刷新那个下拉框，如果你在其他地方添加界面，那个下拉框里是没有的
- 以上内容后文不再赘述

通常情况下直接在这个过程中把界面 New 出来即可，如果你有更高级的需求，当然也可以在其他地方 New，只是千万记得添加到 3FUI 中必须在 Entry 方法里执行。

### 添加自定义 WPF 界面

如果你更擅长 WPF，则使用另一个接口。

VB 语言：

```vb
Public Shared Property HostCall_AddCustomWpfPanel As Action(Of String, UIElement)
Public Shared Sub SetHost_AddCustomWpfPanel(action As Object)
	HostCall_AddCustomWpfPanel = CType(action, Action(Of String, UIElement))
End Sub
Public Shared Sub AddCustomWpfPanel()
	HostCall_AddCustomWpfPanel.Invoke("在下拉框中显示的名称", New 自定义控件)
End Sub
```

C# 语言：

```c#
public static Action<string, UIElement> HostCall_AddCustomWpfPanel { get; set; }
public static void SetHost_AddCustomWpfPanel(object action)
{
	HostCall_AddCustomWpfPanel = (Action<string, UIElement>)action;
}
public static void AddCustomWpfPanel()
{
	HostCall_AddCustomWpfPanel?.Invoke("在下拉框中显示的名称", new 自定义控件());
}
```

### 将编码任务添加到队列中

同样简单的方式即可将你的编码任务添加到 3FUI 的编码队列中。

VB 语言：

```vb
Public Shared Property HostCall_AddMissionToQueue As Action(Of String, String, String)
Public Shared Sub SetHost_AddMissionToQueue(action As Object)
	HostCall_AddMissionToQueue = CType(action, Action(Of String, String, String))
End Sub
Public Shared Sub AddMissionToQueue()
	HostCall_AddMissionToQueue.Invoke(
            "给 ffmpeg 的参数，不要以 ffmpeg 开始",
            "在编码队列里显示的文件名，也可以用来显示其他信息",
            "输出文件的路径在哪，用于编码队列中的定位输出功能")
End Sub
```

C# 语言：

```c#
public static Action<string, string, string> HostCall_AddMissionToQueue { get; set; }
public static void SetHost_AddMissionToQueue(object action)
{
	HostCall_AddMissionToQueue = (Action<string, string, string>)action;
}
public static void AddMissionToQueue()
{
	HostCall_AddMissionToQueue?.Invoke(
    	"给 ffmpeg 的参数，不要以 ffmpeg 开始",
    	"在编码队列里显示的文件名，也可以用来显示其他信息",
    	"输出文件的路径在哪，用于编码队列中的定位输出功能"
	);
}
```

### 发布你的插件

当你完成开发和测试后，点击生成即可在输出目录得到你的插件。前面我们选择了窗体应用，所以是生成的 exe 文件，但从 .NET 5 开始这个 exe 是纯二进制文件，同名的 dll 文件才是代码。此时将这个同名的 dll 单独复制出来，将其后缀从 .dll 改为 .3fui.dll，这样这个文件就是你要发布的插件了，而那个 exe 是完全不需要的。

如果你引用了其他三方组件，需要将那些文件一并发布，当然那些文件就不要改后缀了，3FUI 就是用这个后缀来识别哪些是要加载的插件的。

由于你并没有引用 3FUI，相关许可证条款对你不生效，因此你可以选择闭源甚至出售（虽然这并不能保护你的代码，.NET 程序非常容易反编译）。

## 隐藏功能

### 全屏无边框

在 3FUI 的启动参数中加入 fullscreen 即可变为全屏无边框

### 保留创建、修改、访问时间

随便在一个自定义参数里写这些字符串：

- \<KeepCreationTime>
- \<KeepWriteTime>
- \<KeepAccessTime>

### 给 ffmpeg 进程发消息

虽然基本上用不上，但是点击底部的实时输出文字可以给 ffmpeg 发消息
