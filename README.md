官网：https://ffmpegfreeui.top 和 https://3fui.top<br>主群 [1050613952](https://qm.qq.com/q/fiauAsddG8) 分群① [1070953324](https://qm.qq.com/q/nKoapm6KyW) 频道 [3fui10590000](https://pd.qq.com/s/9emex878m?b=5) KOOK [稻草的工坊](https://kook.vip/1nLQNk)<br>![](https://img.shields.io/github/stars/Lake1059/FFmpegFreeUI?label=星标)![GitHub License](https://img.shields.io/github/license/Lake1059/FFmpegFreeUI?label=许可证)![GitHub repo size](https://img.shields.io/github/repo-size/Lake1059/FFmpegFreeUI?label=仓库大小)![](https://img.shields.io/github/downloads/Lake1059/FFmpegFreeUI/total?label=Github%20总下载量)![](https://img.shields.io/badge/dynamic/json?url=https%3A%2F%2F3fui.top%2Fapi%2Fgithub-downloads&label=镜像站总下载量&query=$.totalDownloads)

## FFmpegFreeUI

**本 README 文件已包含全部教学，请耐心看完**<br>**在反馈问题之前先看看是不是最新版，并不是所有改动都写出来了**

<img src="FFmpegFreeUI\Resources\AppIcon.png" width="100" />

FFmpegFreeUI（简称 3FUI）是在 Windows 上的 [FFmpeg](https://ffmpeg.org) 的专业交互外壳，目前使用 .NET 10 运行时以纯 WinForm 框架开发，使用 [SunnyUI](https://github.com/yhuse/SunnyUI) 和自绘制打造的专业高效的暗黑风格界面。目标做一款轻度专业参数调整的转码软件，让普通人能够通过图形化界面接触到较为全面的通用参数来轻松压制和转换格式。不仅如此，3FUI 具备极高的自由和扩展性，因此也适合任何深度专业人士，即便只是用上一个低消耗的进度条。

3FUI 目前收录了 40 种视频编码器（包含红绿蓝三家全部硬件加速）、21 种音频编码器、13 种图片编码器，如果这还不够还可以自定义参数，提供各种形式的自写参数，所以不要问什么能不能实现什么有没有。如果我的设计对你有帮助，请帮我宣传或者考虑资金支持，目前这个项目纯靠着生活费维持的，所以不要抱怨更新慢。

哔哩哔哩宣传视频：https://www.bilibili.com/video/BV1eeH9zLED5  
知乎终末诗的教程：https://zhuanlan.zhihu.com/p/1943079795341623993

<img src="IMG\img1.png"  />

## 开发目的

是啊，市面上那么多现成的难道不好吗？请记住 3FUI 是被气出来的项目，如果不是某盒子的运营行为，3FUI 根本不会存在，想要一个参数真正透明纯净自由批量的壳很难吗？What can i say，还真 TMD 难。这是一群压片党的需求，不是大众随便转一下的需求，也不是还搞各种限制的需求，我知道菠萝和刹那很火，但我强迫症既要又要，而且也别搞什么私人秘制配方。有人会说怎么不搞批量脚本，那你跟群友的两万五千个任务说去吧，外壳不仅仅是简单拼一下参数，各种外围支持也是非常重要的。

## 设计定位

3FUI 与 [HandBrake](https://github.com/HandBrake/HandBrake)、[ShanaEncoder](https://shana.pe.kr/shanaencoder_portable) 同坐一桌，属于常规专业级压制转换软件，尽管被 **终末诗** 评价为比菠萝刹那更专业，但在我自己看来是同一桌。与菠萝刹那不同是，3FUI 只使用 ffmpeg 来执行任务，没有内置任何编解码器，需要用户手动放置 ffmpeg 或将其添加到环境变量中，这使得 3FUI 的性能始终保持在最新水平，同时也无需在参数上频繁更新。当 ffmpeg 更新的时候，你可以直接换上去使用，而不用等待任何事情。

3FUI 也是市面上唯一一个完全不内置现成预设的外壳，它只是帮你传参数和管理任务，你该学的概念是一个都不能落下，既然在用 3FUI，那必然要明确自己需要的是什么，以及基本的参数概念，你总不能连封装格式和编码格式都分不清吧。如果你觉得陌生，那是因为 3FUI 这里强调实事求是，是什么就是什么，如果你此前一直被各种工具的业余叙述蒙在鼓里，那么你可以在这里看到真理。

会用格式工厂吗？那你已经会用 3FUI 了。所有转码软件的最终逻辑和基本参数都一样，没有谁能自己造一个概念出来，它们内部都是 ffmpeg 在干活，这可是古希腊掌管音视频编解码的神。

## 特点

- 底层逻辑基于预设，可以很方便地与他人分享方案
- 多数地方直接标出参数名、准确显示 ffmpeg 输出的信息，更易于上手和尝试新方案
- 实时计算剩余时间、预估最终大小、可暂停任务
- 最多自动同时开始 10 个任务，手动开始无限制
- 不会向输出文件里写入软件信息
- 缓存自动收拾、不碰注册表
- 不在任何地方扔垃圾（崩溃转储除外，这不是我能控制的）
- 不会收集任何信息
- 附带简易混流和合并功能
- 附带轻量性能监控，可以看处理器和显卡的每个核心占用

支持开发插件来扩展功能，可接入编码队列。VB 和 C# 都可以写，还额外支持 WPF 界面。

## 支持者计划

为了维持 3FUI 的 Free，并且能够维持更新和后续项目，从 3.0 版本开始推出一次性购买的 **[Supporter Pack](https://afdian.com/item/a98d04e8b98011f0a49952540025c377)**，仅提供个性化功能，如果手头充裕欢迎支持。作为付费支持者，您可以向我提供一个支持者信息并在下一次更新中硬编码到 3FUI 中。

## Linux & macOS

[Wine](https://www.winehq.org) 是一个在多种 POSIX-compliant 操作系统上运行 Windows 应用的兼容层，如果能够在 macOS 和 Linux 上安装并正确使用，理论上可以直接在这些操作系统上使用 3FUI。关于这部分的内容请进群跟群友讨论，我买不起苹果电脑也用不来 Linux。

群友倾情贡献：关于在 Linux 中使用 Wine 转译运行 3FUI 的方法：[linux.md](doc/linux.md)

## 下载说明

| 生成名称      | 运行库 | 启动性能 | 运行性能 | 文件数量 | 单文件执行    | 大小 |
| ------------- | ------ | -------- | -------- | -------- | ------------- | ---- |
| ReadyToRun    | 集成   | 相对慢   | 理论最佳 | 几个     | 可以 (仅本体) | 中等 |
| SelfContained | 集成   | 正常     | 正常     | 一堆     | 不能          | 最大 |
| SingleFile    | 不包含 | 正常     | 正常     | 一个     | 当然          | 最小 |

- ReadyToRun 如果把 exe 单独拿出来执行则无法加载插件，也有可能无法解锁 SP
- SelfContained 不建议在机械硬盘上使用，因为包含大量文件
- SingleFile 需要另外安装 [.NET 10 桌面运行时](https://dotnet.microsoft.com/zh-cn/download/dotnet/thank-you/runtime-desktop-10.0.0-windows-x64-installer)
- ReadyToRun 启用了压缩，因此在启动时需要 SelfContained 启动时内存占用的两到三倍，但不要担心，这只会持续一瞬间，启动完成后会强制调用内核级清理

> [!IMPORTANT]  
>
> 埋伏一手：来问 SingleFile 运行库的来一个刀一个，我说的<br>老夫早已料到有人选择性失明

PluginExample 是我做的示例插件；在程序目录下创建 Plugin 文件夹，然后把插件放进去重启 3FUI 就可以加载了，插件也要注意架构；插件的后缀是 **.3fui.dll**，看不见后缀的自己去面壁。

## 猜你想问

> [!IMPORTANT]  
>
> **全都无法运行怎么办？**  
> 依次检查这些项目：
>
> 1. 检查 Windows 更新中是否有 **.NET Framework 3.5、4.8 和 4.8.1 的累积更新** 或者长得像这个的，如果有则立即安装，不要怀疑这个名，这就是微软懒得改名，你系统差了相关补丁，装上立马就好
>    1. 什么你系统更新已经被炸掉了？这是报应！谁叫你信那些“优化博主”的~
> 2. 从正式版 1.0 开始，编译为目标最低系统版本是 Win10 1809，更低版本看天意；实际上 Win7 还能跑，只要补丁打齐依旧能用，但也仅仅能用，bug 是满天飞的
> 3. 检查杀毒软件拦截记录
> 4. 把仓库扒下来自己编译
> 5. 没救了，重装系统吧，已知的老 LTSC 无解

> **如何自行编译这个项目？**  
>
> 1. 下载并安装 **Visual Studio 2026**
>
> 2. 工作负载只需要 **.NET 桌面开发**，可选组件全都可以扔掉（看自己需求）
>3. 直接打开 **.sln** 文件，剩下的依赖会自动补齐（需联网下载）
> 4. 直接运行**全部重新生成**就行了

> **用低版本会死？**  
> 会死。我需要最新的特性和bug修复，保持最新的代码规范。

## 截图
<img src="IMG\img2.png"  />
<img src="IMG\img3.png"  />

注意：字体是可以自由选择的，不要来抱怨字难看<br>图中演示用的字体是 MiSans Demibold 加上 MacType 的效果

## 准备步骤

1. 首先下载 3FUI
   - 不管我重复多少次永远都会有人下成源代码，md
   - 还有一大堆人连处理器架构的概念都没有，tmd
   - 老夫不用掐指都能算到有人没看上面的下载说明跑来问有什么区别
   
2. 前往 [FFmpeg 官网](https://ffmpeg.org/) 下载最新的发行版，gyan.dev 和 BtbN 两者的发行皆可
   - 若选择 gyan.dev 的发行版，下载 **ffmpeg-git-full**
   - 若选择 BtbN 的发行版，下载 **ffmpeg-master-latest-win64-gpl**
   - 不要选带 lgpl 名称的，不然你又要来问怎么连 libx264 都跑不起来
   - 也不建议选带 shared 名称的，老夫掐指一算就料到你没放完整文件
   - 如果你想自己编译 ffmpeg，可以试试这个[全自动编译脚本](https://github.com/m-ab-s/media-autobuild_suite)
3. 将压缩包中的 ffmpeg.exe 和我的 FFmpegFreeUI 放在同一个文件夹中<br>或者将其加入环境变量中也可以，推荐是加环境变量
4. 然后就可以开始使用了
5. ffprobe.exe 也建议放上，查看信息需要的

## 软件机制 & 使用提示

- 编码队列里每个任务都有自己的预设数据快照，而命令行是开始任务的时候当场根据这个快照生成的，而不是再读取参数面板，也就是说任务一旦添加，其参数就已经等于锁死了，不能再更改，因此要注意如果要更改参数必须重新添加任务。不过新版本已经可以在任务管理菜单里覆盖这个快照了。
- 编码队列的列表视图也可以直接拖文件的，会直接用当前参数面板生成快照并尝试立即开始。
  - 从 3.0 版本开始，在把文件拖进编码队列来添加时，按住 Ctrl 或 Shift 或 Alt 来打开单独的参数面板（在松手的时候判断，所以如果忘记按了可以在松手之前继续按；不要多按，没做适配），这样就可以在不变动主界面参数面板的情况下对任务使用其他的方案

- 性能监控里显卡的占用只要有显示就说明那个核心在工作，0% 是占用太小四舍五入没了，不是没在干活，没干活的核心会自动移除显示。
- 开始任务有延迟是正常的，这部分代码是交给后台线程的，状态已经改变了只是没有刷新到界面上，当然你点了多次开始也不会出事，写了判断的。
- ~~不要同时一次性开始太多任务，如果加上任务完成得太快，有概率导致任务已经结束了而我代码还没绑定输出重定向，这会引发连锁反应炸掉 3FUI。~~
  - 划掉，从 3.3 版本开始，随便开，炸了算我输（已经在连续两万五千个文件自动开10个任务的暴力测试中成功存活）
- 启动时有个 sys 的驱动文件生成？应该是性能监控的库要用的，反正我代码里没这玩意，正常情况下应该生成出来后就自动删除了，没自动删除我也不清楚怎么回事。什么你说担心安全问题？都是开源组件你觉得能干什么坏事。
- 如果电脑没有插显示器，请设置阻止显示器关闭来防止进度刷新卡住，系统睡了会把 GDI+ 绘制消息泵睡死导致重新亮屏后无法更新进度了，只能重启软件，但是 Windows 又不允许重连进程，你说气不气人。
  - 如果插了显示器发现息屏后也卡了，那也设置阻止显示器关闭，睡觉或离开直接关显示器电源
  - 笔记本请不要盒盖！保持打开状态！


## 反馈

- 3FUI 没有针对酒吧的炒饭进行预防，非正常操作极易引发报错
- 故意卡 bug 造成的任何损失均与我无关
- 要反馈任何问题，请到Q群：1050613952
- **不要在 B站 汇报问题！** 评论很容易被刷掉；私信也基本是让加群

### 不要算卦！

<img src="IMG\img_rep.png" />

选中一个错误的任务，点击 编码队列底部 的 **捕获N个错误** 文字来打开输出日志<br>**然后发给技术人员！**<br>最好带上命令行，如果你不想让别人看到你的文件名，可以手动抹掉！<br>如果有条件，请提供输入文件的详细参数，很多播放器都可以查看！

> 请勿让 **我** 或 **群友** 或 **专业人士** 或 **外行人士** 进行包括但不限于这些行为：算卦、猜谜、托梦、占卜、人脑推理、强行传教、交流物理学、灵能飞升、虚空扰动、尝试进入量子隧道等等，如有以上行为或类似行为的，造成的全部后果由用户全责承担。

重要的事情再说三遍：不要算卦！尽快提供完整信息！  
重要的事情再说三遍：不要算卦！尽快提供完整信息！  
重要的事情再说三遍：不要算卦！尽快提供完整信息！

你说这种人是不是生活不能自理啊，他不是来求助的，是来求心理安慰的！

## 许可和引用

- 3FUI 使用 MIT 开源许可，可以自由地使用和分发此软件
- SunnyUI 商用授权证书编号：PVIP2023080201
- 仅在 GitHub 开源，在其他平台看到的源代码都不是本人！

| 引用程序集                                                   | 许可证         | 作用                       |
| ------------------------------------------------------------ | -------------- | -------------------------- |
| [SunnyUI](https://gitee.com/yhuse/SunnyUI)                   | GPL-3.0-only   | 界面主框架                 |
| [WindowsAPICodePack](https://github.com/contre/Windows-API-Code-Pack-1.1) | 微软软件许可证 | 提供更舒适的文件夹选择对话框 |
| [LibreHardwareMonitorLib](https://github.com/LibreHardwareMonitor/LibreHardwareMonitor) | MPL-2.0        | 性能监控                   |

是的，三方库就这么点，连 Json.NET 都没用，你就说够不够轻量吧

## 新手入门

如果是纯新手，对视频的技术参数没有任何了解，建议先学习以下内容

- 极客湾 | 视频基础参数科普 | [BV1nt411Q7S6](https://www.bilibili.com/video/BV1nt411Q7S6)
- 极客湾 | 电影和游戏的帧数效果差别 | [BV19x411L7fH](https://www.bilibili.com/video/BV19x411L7fH)
- 影视飓风 | 视频的封装与编码 | [BV1ws41157f8](https://www.bilibili.com/video/BV1ws41157f8)
- 影视飓风 | 色深和色度采样 | [BV1ds411T7F4](https://www.bilibili.com/video/BV1ds411T7F4)
- 影视飓风 | 帧率的旧事 | [BV1hp4y1f7B5](https://www.bilibili.com/video/BV1hp4y1f7B5)
- 终末诗 | 适用于小白的视频压缩教学 | [知乎](https://zhuanlan.zhihu.com/p/1913258114746122747)  此文章包含大量测试结果总结和设置教学<br>
  新手把这篇文章看完能学会很多东西，继续往下看之前先把这个打开看！！
- 终末诗 | 3FUI 入门教程 | [知乎](https://zhuanlan.zhihu.com/p/1943079795341623993)

另外我仓库 IMG 文件夹里也有部分编码器的测试结果，数据来自群友。

## 视频编码器

推荐的质量值为个人观点，仅供参考，请自行慢慢调

| 归属类别     | 编码器具体名称 | 描述                                                         | 推荐的质量值                              |
| ------------ | -------------- | ------------------------------------------------------------ | ----------------------------------------- |
| 复制流       | copy           | 不重编码直接复制一遍                                         |                                           |
| H.266/VVC    | libvvenc       | 跑分的神，faster 即可吊打全场，解码和编码性能消耗巨大，优化空间巨大，目前不适合日常使用和生产环境 | 不支持 crf<br>按照 qp 给定即可            |
| H.266/VVC    | libx266        | 本应是 266 的官方实现，可到现在仍未发布                      |                                           |
| AV1          | libaom-av1     | 官方实现，很慢，偏商用，适合多开任务跑单线程，不适合日常使用 |                                           |
| AV1          | libsvtav1      | Intel 主导，多线程优化，民用友好，轻松吃满 CPU，日常推荐     | crf=32：综合推荐                          |
| AV1          | av1_nvenc      | RTX 40 系开始支持，但建议用 RTX 50 系及以上显卡（第九代 NVENC），可跟 libsvtav1 五五开，部分场景已超越，日常强烈推荐 | cq=36：肉眼无损万能通用值                 |
| AV1          | av1_qsv        | ARC 独显、Ultra100核显开始支持                               | 缺少实测<br/>-global_quality=34           |
| AV1          | av1_amf        | 7000系独显、780M核显开始支持，A卡虽然一直很弱，但AV1大概还行？ | 缺少实测<br/>-qp_i=34 和 -qp_p=34         |
| AV1          | librav1e       | 很慢，综合表现一般                                           |                                           |
| AV1          | av1_vaapi      | Linux                                                        |                                           |
| H.265/HEVC   | libx265        | 目前的最佳选择，压制组目前的首选                             | crf=24/25：综合推荐                       |
| H.265/HEVC   | hevc_nvenc     | 比 libx265 有明显差距，但足够日常基本需求                    | cq=28：综合推荐<br/>cq=26：压视觉无损内容 |
| H.265/HEVC   | hevc_qsv       | 大概好于N卡？反正I卡这个还是不错的，可以用于日常需求         | -global_quality=24                        |
| H.265/HEVC   | hevc_amf       | 不推荐将 AMD 的 265 用于日常需求，除非你只是转个码不在乎别的 | -qp_i=28 和 -qp_p=28                      |
| H.265/HEVC   | hevc_d3d12va   | ？                                                           |                                           |
| H.265/HEVC   | hevc_vaapi     | Linux                                                        |                                           |
| H.265/HEVC   | hevc_vulkan    | ？                                                           |                                           |
| H.264/AVC    | libx264        | 说实在的有点过时，除非为了兼容老设备否则也没必要用于日常了   | crf=23：综合推荐                          |
| H.264/AVC    | h264_nvenc     | 不推荐，除非你只是转个码不在乎别的                           |                                           |
| H.264/AVC    | h264_qsv       | 不推荐，除非你只是转个码不在乎别的                           |                                           |
| H.264/AVC    | h264_amf       | 极其不推荐，图一乐就行，纯转码都不推荐                       |                                           |
| H.264/AVC    | h264_vaapi     | Linux                                                        |                                           |
| H.264/AVC    | h264_vulkan    | ？                                                           |                                           |
| 来自 Apple   | prores_ks      | 通用，但是不建议用于日常，prores 本身就是几乎没有压缩度的编码，仅建议用于中转流程 |                                           |
| 来自 Apple   | prores_aw      | 需要 macOS                                                   |                                           |
| 来自 Google  | libvpx-vp9     | VP9 强于 264，弱于 265                                       |                                           |
| 来自 Google  | vp9_vaapi      | Linux                                                        |                                           |
| 来自 Google  | libvpx         | 这个就是 VP8，稍逊于 264                                     |                                           |
| 来自 Google  | vp8_vaapi      | Linux                                                        |                                           |
| FFv1         | ffv1 -level 3  | 博物馆级别无损编码，个人使用仅建议用于中转流程               | 不要设置质量参数                          |
| FFv1         | ffv1 -level 1  | 博物馆级别无损编码，第一版                                   | 不要设置质量参数                          |
| FFv1         | ffv1_vulkan    | ？                                                           | 不要设置质量参数                          |
| 其他现代编码 | libxeve        | 这是 MPEG5，目前并不好用，速度极慢跟 266 坐一桌              |                                           |
| 其他现代编码 | libxavs2       | 国产编码 AVS 第二版，用于电视节目，但是挺难用，速度也比较慢  |                                           |
| 老旧格式     | mpeg4          | 已过时                                                       |                                           |
| 老旧格式     | libxvid        | 已过时，但目前仍有工业场景在用                               |                                           |
| 老旧格式     | rv20           | 已过时，RMVB 2.0                                             |                                           |
| 老旧格式     | rv10           | 已过时，RMVB 1.0                                             |                                           |
| 老旧格式     | wmv2           | 已被淘汰                                                     |                                           |
| 老旧格式     | wmv1           | 已被淘汰                                                     |                                           |
| 禁用         | -vn            |                                                              |                                           |

> **新手常问：为什么说 FFV1 才是无损编码？**
> 这里的有损和无损是技术上的，不是视觉上的。生活中看的视频都是有损压缩，它们都在欺骗视觉，人眼的能力是非常局限的，就像你眼睛贴到屏幕上才能细微察觉所有的颜色都是用红绿蓝混合出来的。技术上的无损编码是确保了每个像素的信息正确，而生活中的视频每次重编码都会让像素产生变化，所以都是有损压缩，你也不希望一分钟的视频有1GB吧。

> **为什么 AV1 已足够成熟但压制组都不用？**
> AV1 有明显的涂抹效果，人话就是会损坏纹理细节，虽然普通人很难看出，对于个人压片没什么影响，手机上看就更不影响了，但盯帧时仍旧可肉眼察觉，这种问题对于专业压制组不可接受。另外 266 也有类似的涂抹。

| 区分标记 | 适用的硬件             | 人话     |
| -------- | ---------------------- | -------- |
| lib      | CPU                    |          |
| nvenc    | NVIDIA                 | 你的老黄 |
| amf      | AMD                    | 你的苏妈 |
| qsv      | Intel                  | 你的老陈 |
| vaapi    | Linux API              |          |
| vulkan   | 受支持的显卡           |          |
| d3d12va  | 受支持的显卡和操作系统 |          |

### NVIDIA NVENC 规格

有关 N卡 硬件加速的详细规格可以在官方页面上查询<br>
https://developer.nvidia.com/video-encode-and-decode-gpu-support-matrix-new

所以是的，3090 等于 3060，如果只是单任务，那 5090 也等于 5050

### 预设 -preset

控制编码器如何平衡编码速度与压缩效率，这通常与画质无关或影响很小。你是需要用更长的时间换更小的文件，还是用更大的文件换更短的时间。通常情况下对编码速度的影响很大，而对压缩效率的影响较小，尤其是硬件加速的编码器，相反对于软件编码的影响则很大。

### 配置文件 -profile

控制编码器的配置档次，也就是需要支持哪些特性，这通常会影响编码复杂度，从而影响到在各种设备上播放的兼容性。例如典型的是否支持 10bit 色深。保留原文件的特性一般直接不写即可。

### 场景优化 -tune

控制编码器如何针对特定的视频内容或播放用途进行专门优化。视频内容方面比如电影、动画、胶片颗粒、静态图片；用途方面比如更快的解码速度、更低的延迟等。通常情况下不需要考虑。

### 像素格式 -pix_fmt

设定像素如何存储，这会影响颜色空间、位深、通道排列等。例如生活中绝大多数视频都是 yuv420p，为 8bit 色深 4:2:0 采样；对于 HDR 视频常用的是 yuv420p10le。这还直接影响每个像素的数据量，444 的视频体积必然比 420 大出很多；422 和 444 通常是专业工作者接触的。

## 音频编码器

复制流、禁用、AAC、LAME MP3、Opus、FLAC、ALAC、WAV 16bit、WAV 24bit、WAV 32bit、WAV 64bit、AC3、EAC3、DTS Coherent Acoustics、TrueHD、True Audio、Vorbis、RealAudio、WavPack、LAME MP2、AMR-NB、AMR-WB

## 图片编码器

PNG、APNG、JPEG\JPG、WEBP、GIF、BMP、JPEG 2000、JPEG-LS、HDR、TIFF、DPX、OpenEXR

## 比特率控制方式

其实这东西不选也行的，直接去设置质量参数和值，这东西就是对特定编码器加上对应 rc

- **恒定质量 CRF**：软件编码首选，-rc 并没有 crf 这个值，而是使用 -crf <?>，你需要在质量控制里填写它
- **动态码率 VBR**：硬件加速首选，会自动选到 cq，但 I卡 和 A卡 通常不用 cq，VBR HQ 同理

注意我写的是首选，某些编码器不走寻常路，比如 vvenc 根本不支持 crf。

- **动态码率 VBR HQ**：硬件加速专用，一般没必要选，并不会有太多肉眼可见的提升，反而时间爆涨
- **恒定量化 CQP**：较少使用，主用于研究和特定场景，并没有 CQP 这个玩意，但是个别编码器的 rc 是真的有这个值，反正日常别用就对了
- **恒定速率 CBR**：到底是什么人这么浪费还用这玩意

做不了一点

- **平均码率 ABR**：哪有 ABR，不就是传统转码，直接写比特率不就完了
- **二次编码 TPE**：没钱，给我转一个W就做，不然我先饿死了
- **三次编码？** 富哥V我一个小目标看看实力

## 质量设定 -crf / -cq / -qp / -global_quality

| 硬件类型        | 建议首选       |
| --------------- | -------------- |
| CPU（软件编码） | crf            |
| N卡             | cq             |
| I卡             | global_quality |
| A卡             | qp_i 和 -qp_p  |

### 注意根据具体编码器和视频内容灵活调整质量值
质量的默认值是 23，肉眼无损是 16，注意这是一个均衡标准（实时输出的质量值，就是 ffmpeg 进度信息里的 q= 那个），实际使用起来不同的编码器要设定的值差得非常远；23 不一定是糊的，但 16 一定是清晰的，还很可能清晰过度了！每种编码器的质量度量都不一样，还会受到片源自身清晰度和内容的影响。所以要根据每种不同的情况慢慢尝试，不要指望一步到位！通常情况下硬件加速要设定更大一些的质量值。

### 已经被压过的视频几乎不可能再压得更小
例如流媒体平台上扒下来的和压制组发布的动漫资源（仅限于看起来已经压得很好的比较小体积的），在维持视觉质量的前提下即便是 264 转 AV1 也基本不可能有好的结果（266 除外，那已经是纯挂逼了），编码器会利用人眼的各种局限性，诸如视觉暂留等。

请牢记：不要用这点参数去挑战压制组的实力！除非你开挂，否则你不可能干得过他们！

### 鱼和熊掌不可兼得
编码时间、画面质量、文件体积、解码性能，你总要至少放弃一样，不要想着全都要

## 更多功能和所用滤镜

- 画面缩放维持比例 scale
- 画面裁剪 crop
- 抽帧 mpdecimate=max=? 和 -vsync vfr
- 插帧 minterpolate，这个效果非常一般，但非常稳定，速度极快，使用 CPU 处理，完全没有果冻，对于要求不高的临时观影可以用一用，仅适用于动静小的视频，不适用于3D游戏录制、动静大的电影等
  - 最佳质量选项：运动补偿插值+加权obmc
- 动态模糊 tblend，只有**与前一帧的平均值**出来的画面是正常的，其他的需要专门调整
- 超分 libplacebo
  - 支持使用自定义着色器，例如 Anime4K、FSRCNNX 等，文件请自行下载

- 色彩管理 zscale 和 libplacebo
- 基本调色 eq
- 降噪 hqdn3d、nlmeans、atadenoise、bm3d、AviSynth+（avs）
  - 关于使用 AviSynth+ 降噪滤镜可参阅 [AviSynth+.md](doc/AviSynth+.md)

- 锐化 unsharp
- 转逐行 yadif
- 转隔行 tinterlace
- 角度翻转 transpose
- 镜像翻转 hflip、vflip
- 响度标准化 loudnorm

## 剪辑区间

使用此配套工具来进行剪辑区间可视化：https://github.com/Lake1059/3fuiVideoHelper

## 烧录字幕不会做！

真的还不如手写参数来得灵活稳定，而且目前这样的设计是可以实现批量烧录的

- SRT/ASS/SSA

  ```bash
  // 视频滤镜
  
  subtitles='字幕文件路径':force_style='FontName=字体名称,FontSize=字号,PrimaryColour=HTML颜色值,MarginV=距离底部的像素距离'
  
  // 字幕文件路径配合我的通配字符串去写
  // 特效字幕都内置了字体样式和其他属性，当然也可以使用 force_style 进行覆盖
  ```

  - stream_index 可直接指定已混流在文件中的字幕流索引
  - charenc 可指定字符编码
  - fontsdir 可指定字体文件目录

- SUP 位图字幕<br>最好还是用专门的 sup 工具，ffmpeg 这里必须先把 sup 转成逐帧图像，然后再用 overlay 叠加到视频上，无法一次性处理好。

## 插件开发

通过插件，你可以给 3FUI 添加各种功能来满足自己的需求，只需要像我那样把可视化摆上来然后生成对应的参数即可，还可以选择接入我的编码队列，而不用自己做进度显示。

考虑到 ReadyToRun 生成的 exe 无法被添加引用，插件使用 反射 + 特性 + 动态调用 来实现，你在开发插件的时候不需要引用 3FUI，只需要按照我制定的接口标准写代码即可。目前总共只有 4 个接口功能，非常简单，通常情况下你只需要用其中 2 个，所以不要担心要硬啃代码。

首先你需要有与我开发 3FUI 相同的集成开发环境：

1. 下载并安装 [Visual Studio Community 2026](https://visualstudio.microsoft.com/zh-hans/vs/)（注意在安装程序里选择预览版）
2. 工作负载勾选：.NET 桌面开发
   - 可选组件看自己需求，可以能不要就都不要的
   - 但是我仍旧推荐这些组件：IntelliCode、.NET 可移植库目标包、.NET 分析工具
   - 按理说会强制安装 .NET 10 SDK，记得检查
3. 完成 VS2026 的安装

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

现在，你就完成了插件初始化的方法，确认一下：是名为 Entry 的 Class 里有名为 Entry 的方法。接下来就是接口来实现关键的功能，直接把下面的代码复制过去自己改就行了。

### 添加自定义 WinForm 界面

要向 3FUI 的插件扩展选项卡添加自己的界面，必须创建你自己的用户控件，将你的功能全部集成到这个控件里，然后将下列代码复制到你的 Entry 类中：

VB 语言：

```vb
Public Shared Property HostCall_AddCustomWinformPanel As Action(Of String, Control)
Public Shared Sub SetHost_AddCustomWinformPanel(action As Object)
	HostCall_AddCustomWinformPanel = CType(action, Action(Of String, Control))
End Sub

'调用
HostCall_AddCustomWinformPanel.Invoke("在下拉框中显示的名称", New 自定义控件)
```

C# 语言：

```c#
public static Action<string, Control> HostCall_AddCustomWinformPanel { get; set; }
public static void SetHost_AddCustomWinformPanel(object action)
{
	HostCall_AddCustomWinformPanel = (Action<string, Control>)action;
}

//调用
HostCall_AddCustomWinformPanel?.Invoke("在下拉框中显示的名称", new 自定义控件());
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
    
'调用
HostCall_AddCustomWpfPanel.Invoke("在下拉框中显示的名称", New 自定义控件)
```

C# 语言：

```c#
public static Action<string, UIElement> HostCall_AddCustomWpfPanel { get; set; }
public static void SetHost_AddCustomWpfPanel(object action)
{
	HostCall_AddCustomWpfPanel = (Action<string, UIElement>)action;
}

//调用
HostCall_AddCustomWpfPanel?.Invoke("在下拉框中显示的名称", new 自定义控件());
```

### 将编码任务添加到队列中

同样简单的方式即可将你的编码任务添加到 3FUI 的编码队列中。从 2.0 版本开始有两种添加的方式，一个是用命令行添加，这样添加的任务不包含预设数据，无法使用重配置相关功能；另一个是用 3FUI 的预设文件来添加，这样添加的任务可以使用完整功能，但是注意每添加一个任务都会读取一遍预设文件，这个方法不是专门设计给批量添加需求的。

VB 语言：

```vb
'使用 FFmpeg 命令行添加
Public Shared Property HostCall_AddMissionToQueueWithArgs As Action(Of String, String, String, String)
Public Shared Sub SetHost_AddMissionToQueueWithArgs(action As Object)
	HostCall_AddMissionToQueueWithArgs = CType(action, Action(Of String, String, String, String))
End Sub

'调用
HostCall_AddMissionToQueueWithArgs.Invoke("给 ffmpeg 的参数，不要以 ffmpeg 开始", "在编码队列里显示的文件名，也可以用来显示其他信息", "输出文件的路径在哪，用于编码队列中的定位输出功能", "输入文件在哪，可以不写")
```

```vb
'使用 3FUI 预设文件添加
Public Shared Property HostCall_AddMissionToQueueWith3fuiFile As Action(Of String, String, String, String)
Public Shared Sub SetHost_AddMissionToQueueWith3fuiFile(action As Object)
	HostCall_AddMissionToQueueWith3fuiFile = CType(action, Action(Of String, String, String, String))
End Sub

'调用
HostCall_AddMissionToQueueWith3fuiFile.Invoke("3FUI 预设文件的路径", "在编码队列里显示的文件名，也可以用来显示其他信息", "输出文件的路径在哪，用于编码队列中的定位输出功能", "输入文件在哪，可以不写")
```

C# 语言：

```c#
//使用 FFmpeg 命令行添加
public static Action<string, string, string, string> HostCall_AddMissionToQueueWithArgs { get; set; }
public static void SetHost_AddMissionToQueueWithArgs(object action)
{
	HostCall_AddMissionToQueueWithArgs = (Action<string, string, string, string>)action;
}

//调用
HostCall_AddMissionToQueueWithArgs?.Invoke("给 ffmpeg 的参数，不要以 ffmpeg 开始", "在编码队列里显示的文件名，也可以用来显示其他信息", "输出文件的路径在哪，用于编码队列中的定位输出功能", "输入文件在哪，可以不写");
```

```c#
//使用 3FUI 预设文件添加
public static Action<string, string, string, string> HostCall_AddMissionToQueueWith3fuiFile { get; set; }
public static void SetHost_AddMissionToQueueWith3fuiFile(object action)
{
	HostCall_AddMissionToQueueWith3fuiFile = (Action<string, string, string, string>)action;
}

//调用
HostCall_AddMissionToQueueWith3fuiFile?.Invoke("3FUI 预设文件的路径", "在编码队列里显示的文件名，也可以用来显示其他信息", "输出文件的路径在哪，用于编码队列中的定位输出功能", "输入文件在哪，可以不写");
```

### 发布你的插件

当你完成开发和测试后，点击生成即可在输出目录得到你的插件。前面我们选择了窗体应用，所以是生成的 exe 文件，但从 .NET 5 开始这个 exe 是纯二进制文件，同名的 dll 文件才是代码。此时将这个同名的 dll 单独复制出来，将其后缀从 .dll 改为 .3fui.dll，这样这个文件就是你要发布的插件了，而那个 exe 是完全不需要的。

如果你引用了其他三方组件，需要将那些文件一并发布，当然那些文件就不要改后缀了，3FUI 就是用这个后缀来识别哪些是要加载的插件的。

由于你并没有引用 3FUI，相关许可证条款对你不生效，因此你可以选择闭源甚至出售（虽然这并不能保护你的代码，.NET 程序非常容易反编译）。

## 隐藏功能

### 全屏无边框

在 3FUI 的启动参数中加入 fullscreen 即可变为全屏无边框，在该模式下，右键主页顶部区域可以切换全屏，你应该知道那部分区域是可以用来拖动窗口的，全屏下拖一下就变回原来的尺寸了

## 你已获得成就

- 看完了这个 md 文件，击败了全球 99% 的用户
- 或者你直接滑到底，击败了全球 50% 的用户
