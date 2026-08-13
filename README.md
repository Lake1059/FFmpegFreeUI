官网 https://ffmpegfreeui.top 和 https://3fui.top 短域名将于 2028 年废弃<br>主群 [1050613952](https://qm.qq.com/q/fiauAsddG8) 分群① [1070953324](https://qm.qq.com/q/nKoapm6KyW) 频道 [3fui10590000](https://pd.qq.com/s/9emex878m?b=5) KOOK [稻草的工坊](https://kook.vip/1nLQNk)

![](https://img.shields.io/github/stars/Lake1059/FFmpegFreeUI?label=星标) ![GitHub License](https://img.shields.io/github/license/Lake1059/FFmpegFreeUI?label=许可证) ![](https://img.shields.io/github/downloads/Lake1059/FFmpegFreeUI/total?label=Github%20总下载量)

<img src="FFmpegFreeUI\Resources\AppIcon.png" width="100" />

## FFmpegFreeUI v6 - 1st Anniversary！

FFmpegFreeUI（简称 3FUI）是在 Windows 上的 [FFmpeg](https://ffmpeg.org) 的专业交互外壳。此，即为真理！这不是给纯小白的一键全自动软件，即便 6.0 已经大幅改善了普通人的体验，但 3FUI 仍旧面向懂基本参数的进阶编码人员，小白上手有门槛但上限无穷大，这不是一个普通的编码软件，而是一整套可扩展平台。

知乎终末诗的教程：https://zhuanlan.zhihu.com/p/1943079795341623993<br>
v6 开发者官方宣传视频：https://www.bilibili.com/video/BV1rT7E6wEK4<br>
来自 小in 的宣传视频：https://www.bilibili.com/video/BV1vZud6VEsn

- 发布形式：所有数据存于当前目录的单文件
- 系统要求：Windows 10 1609+ 仅限 x64 / arm64
- 运行环境：.NET 10（不自带，需安装到系统，可由更新器自动下载安装）
- 基底框架：WinForms
- 交互呈现：[LakeUI](https://github.com/Lake1059/LakeUI) 自主维护的基于 DirectX 的渲染引擎
- 硬件要求：渲染器需要支持 D3D11 的显卡
- 收费情况：所有生产力功能免费 + 个性化功能收费

3FUI 是专为国内环境设计的，语言只有简体中文，不计划任何多语言功能，如有其他语言需求可自行开仓库维护所有字符串，并在 MIT 许可范围内行使所有权利。

### LakeUI 控件集

3FUI 是 LakeUI 的招牌宣传作品，如果你也想在 WinForms 上制作这样的风格界面，欢迎试用 LakeUI，该产品像 .NET 原生控件一样简单易用，同时价格也非常亲民。也欢迎看看华丽的 [LakeUI 官网](https://lakeui.top/)。

### 3FUI Agent 智能体

3FUI Agent 智能体现已可用！专属于 3FUI 的副驾驶！可接入任何兼容 OpenAI SDK 的端点来使用任何模型，其能力几乎相当于半个 Codex 或 CC，不仅可以控制整个参数面板和协助管理任务，还具备完整的联网能力、访问文件系统、读取文本文件和图片、使用 Windows PowerShell 终端。对话支持中途切换模型，数据完全存于本地，并与所有模型和端点共享。对话交互也同时具备主流 AI 软件的流式展现，支持基础 MarkDown 语法，同时占用极低，完全纯本地渲染，绝无半点 Web 套壳！

## 新图标征集活动

为了提高 3FUI 的辨识度，特此举办图标征集活动，即日起至二周年时刻，期限一年，最终结果由群内成员初审 + 开发者本人最终决定，**最终被采纳者** 和 **未采纳但优秀作品者** 可直接获得 SP 支持者包和金钱奖励（不确定有多少，取决于我富裕程度），参与既表示同意此条款：创作者继续拥有作品的版权但无权撤回给 3FUI 的使用授权，图标还将被用于 3FUI 商业宣传用途以及其他所有人的非商业用途。

图标设计有以下两条赛道，虽然原则上只会采纳一个但如遇质量足够高会同时采纳：

- 专业软件方向：像是 Adobe、各大 MC 启动器、WinUI 等风格
- 二次元方向：像是各大二游的助手软件

设计要求如下：

- 必须带透明通道，必须是正方形
- 最终交付分辨率至少 64 像素边长的 PNG 或 SVG 矢量图
- 在 Windows 100% 缩放的桌面图标尺寸下必须细节清晰

投稿方式：

- 直接加主群发到群文件的【新图标征集】文件夹
- 在B站发动态 @湖边的稻草

## 旧版交由社区维护

从 3FUI 6.0 开始，旧系统已不再兼容，且性能过低的电脑体验会很差，传统 GDI+ 路线的渲染版本现已交给所有开发者，可以自行开仓库继续维护 5.3 版本，该时期的源码可以直接去该标签处下载。但请注意，早期版本使用的 SunnyUI 也是付费授权的，如果不能购买该授权，必须撤掉所属 UI 组件或者撤掉 SP 功能。

如果你维护了旧版本可以直接提 PR 写在这里。

## 设计定位和特点

3FUI 与 [HandBrake](https://github.com/HandBrake/HandBrake)、[ShanaEncoder](https://shana.pe.kr/shanaencoder_portable) 同坐一桌，属于常规专业级压制转换软件，尽管被 **终末诗** 评价为比菠萝刹那更专业，但在我自己看来是同一桌。与菠萝刹那不同是，3FUI 只使用 ffmpeg 来执行任务，没有内置任何编解码器，需要用户手动放置 ffmpeg 或将其添加到环境变量中，这使得 3FUI 的性能始终保持在最新水平，同时也无需在参数上频繁更新。当 ffmpeg 更新的时候，你可以直接换上去使用，而不用等待任何事情。

- 全自由转码，自由组合，任意自写参数
- 专业调校的交互设计，主次分明，简洁高效
- 完全无广告，所有生产力功能全部免费
- 超高缩放支持，带手动微调校准
- 底层逻辑基于预设，方便分享方案
- 多数地方直接标出参数名，更易于上手和尝试新方案
- 实时计算剩余时间、预估最终大小、可暂停任务
- 专为批量处理而设计，无限制任务添加数量
- 不会擅自向输出文件里写入软件信息
- 不碰注册表、不乱扔垃圾、不会收集任何信息
- 完整色彩管理选项
- 为烧字幕提供大量选项
- 为调用 AviSynth 和 VapourSynth 提供便捷
- 附带简易混流和合并
- 附带性能监控，戈门把任务管理器搬过来了？
- 附带 ffprobe 和 ffplay 调用
- 附带质量评测
- 支持外部调用和远程调用
- 支持插件

## 截图

- 玻璃背景功能是 SP 特权，图中展示是内置图片
- 浮动内容的毛玻璃向所有用户开放
- 图中展示用字体是鸿蒙字体 + 矢量几何文字渲染模式

<img src="IMG\1.png"  />

<img src="IMG\2.png"  />

<img src="IMG\3.png"  />

<img src="IMG\4.png"  />

<img src="IMG\5.png"  />


## 反馈渠道

- 3FUI 没有针对酒吧的炒饭进行预防，非正常操作极易引发报错
- 故意卡 bug 造成的任何损失均与我无关
- 要反馈任何问题，请优先到Q群，已经在此文件的开头写了
- **不要在 B站 汇报问题！** 评论很容易被刷掉；私信也基本是让加群

### 不要算卦！

<img src="IMG\rep.png" />

**把错误信息发给技术人员！不要发给你的生活AI！说的就是豆包这样的！<br>ffmpeg 的输出并不是所有情况都有具体原因<br>尤其是说功能未支持、参数不正确的，这些情况没有命令行还是算卦<br>连 GPT 和 Claude 都不一定猜得准，更别提国内那些AI的训练数据了！**

最好带上命令行，如果你不想让别人看到你的文件名，可以手动抹掉！<br>如果有条件，请提供输入文件的详细参数，很多播放器都可以查看！

> 请勿让 **我** 或 **群友** 或 **专业人士** 或 **外行人士** 进行包括但不限于这些行为：算卦、猜谜、托梦、占卜、人脑推理、强行传教、交流物理学、灵能飞升、虚空扰动、尝试进入量子隧道等等，如有以上行为或类似行为的，造成的全部后果由用户全责承担。

重要的事情再说三遍：不要算卦！尽快提供完整信息！  
重要的事情再说三遍：不要算卦！尽快提供完整信息！  
重要的事情再说三遍：不要算卦！尽快提供完整信息！

你说这种人是不是生活不能自理啊，他不是来求助的，是来求心理安慰的！

## 许可和引用

- 3FUI 使用 MIT 开源许可，可以自由地使用和分发此软件
- 仅在 GitHub 开源，在其他平台看到的源代码都不是本人！

| 引用程序集                                                   | 许可证         | 作用                       |
| ------------------------------------------------------------ | -------------- | -------------------------- |
| [LakeUI](https://github.com/Lake1059/LakeUI)                 | MIT            | v6 界面主框架              |
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

### 概念科普：封装格式和编码格式

这是大众的广泛误区。

既然你在用 3FUI，那就必须清楚这个最基本的概念，mp4 是封装格式，不是编码格式，没有 mp4 这种编码，x264 才是编码格式，mp4 只是外面的壳子，其内部可以塞 x264\x265\av1 等等主流编码。其余以此类推，而 mkv 所支持的编码最为广泛。

### NVIDIA NVENC 规格

https://developer.nvidia.com/video-encode-and-decode-gpu-support-matrix-new

所以是的，3090 等于 3060，如果只是单任务，那 5090 也等于 5050。编解码核心只有代数和个数的差距，没有所谓的规模差距，老黄不至于抠成这样，所以如果你要买一张N卡只做编解码，那么直接买当代最低型号即可，例如50系买 RTX 5050 即可，而 ffmpeg 也没法在一个任务中正确调用多个编解码核心。另外老黄对游戏卡的同时调用数量做了限制，一般是8个，也就是差不多能同时启动 8 个调用N卡进行编码的 ffmpeg，但专业卡是没有这个限制的，这方面倒是专业卡高人一等。

要不还得是老黄上心呢，你看看另外两家的表格多寒颤。

### INTEL QSV 规格

https://en.wikipedia.org/wiki/Intel_Quick_Sync_Video

### AMD AMF 规格

- https://github.com/GPUOpen-LibrariesAndSDKs/AMF/wiki/GPU%20and%20APU%20HW%20Features%20and%20Support
- https://en.wikipedia.org/wiki/Video_Core_Next

## 启动参数

3FUI 具有和 FFmpeg 一样的参数调用方式，你可以随便找个终端来使用或者在外部程序中启动时传递，也可以用快捷方式做个测试；这些功能在原理上是走的插件功能。(需要5.0及以上版本)

| 参数                | 作用                        | 在情况下使用 |
| ------------------- | --------------------------- | ------------ |
| -i [string]         | 输入媒体文件                | 首次或已启动软件 |
| -3fui_file [string] | 输入预设文件                | 首次或已启动软件 |
| -ffmpeg [string]... | 把后面的参数全部喂给 ffmpeg | 首次或已启动软件 |
| -test               | 测试用，会弹出“哔哔”        | 首次或已启动软件 |
| fullscreen          | 全屏无边框模式              | 未启动软件   |

- -i 和 -3fui_file 必须一起用，表示使用指定预设文件对指定媒体文件进行任务，预设文件可以直接指定在 Preset 文件夹也就是方案管理中的预设名称。
- -ffmpeg 是纯命令模式，后面的所有内容全都会给 ffmpeg。可传入参数列表，也可传入以 ffmpeg、ffmpeg.exe 或其完整路径开头的完整 FFmpeg 命令行；程序会自动忽略其中的可执行文件部分。
- 另外还有用于传递剪辑区间参数的 -3fuiVideoHelperInPointTime 和 -3fuiVideoHelperOutPointTime

## 远程调用

在设置中打开远程调用即可监听指定的端口，收到消息就会开始任务，消息数据内容与启动参数是一样的，这就意味着 3FUI 可以部署在一个巨大的局域网中，只要 3FUI 能够访问文件，那么你可以从这个局域网的任意电脑通过其他程序发任务给编码机上的 3FUI，只要有权限访问，远程访问也是理所当然的。

注意发起程序需要用 UDP 协议发送，默认端口 10591。

(需要5.0及以上版本)

## 插件开发

Plugin API v2 面向所有插件开发者，主要能力包括：

- 在宿主公开的稳定 UI 锚点插入输入框、下拉框、按钮或自定义控件；
- 装饰公开的原生控件，并把插件状态随 v6 预设保存；
- 在预设、队列、任务准备、命令构建、外部进程和任务终态阶段注册有序处理器；
- 在异步任务阶段接收停止令牌，并向原生任务日志报告进度和结构化结果。

API v2 是可选组件。只有程序根目录同时存在 `FFmpegFreeUI.PluginHost.dll` 和
`FFmpegFreeUI.PluginSdk.dll` 时才会启用；缺少 SDK 时，3FUI 会在加载程序集前静默跳过依赖它的插件，
主程序与不依赖 SDK 的旧版 `Entry` 插件仍可正常运行。

从创建类库、实现入口、保存 UI 状态，到完整处理链、全部锚点和每个阶段实际能修改的字段，请阅读
[Plugin API v2 中文开发指南](doc/Plugin-API-v2.zh-CN.md)。仓库同时提供
[C# 全接口综合示例](Samples/ThreeFui.PluginApi.Sample)和
[VB.NET 全接口综合示例](Samples/ThreeFui.PluginApi.VbVmafSample)。两个示例均覆盖全部公开 UI 锚点和
处理阶段，分别以 SHA-256 与 VMAF 展示成功后处理。

旧版 `Entry` / `SetHost_*` 回调继续兼容，适合已有插件或添加独立页面；需要嵌入原生参数页、参与参数
处理链或获得任务取消令牌的新插件应使用 Plugin API v2。

## 你已获得成就

- 看完了这个 md 文件，击败了全球 99% 的用户
- 或者你直接滑到底，击败了全球 50% 的用户
