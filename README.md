官网 https://ffmpegfreeui.top 和 https://3fui.top 短域名将于 2028 年废弃<br>主群 [1050613952](https://qm.qq.com/q/fiauAsddG8) 分群① [1070953324](https://qm.qq.com/q/nKoapm6KyW) 频道 [3fui10590000](https://pd.qq.com/s/9emex878m?b=5) KOOK [稻草的工坊](https://kook.vip/1nLQNk)

![](https://img.shields.io/github/stars/Lake1059/FFmpegFreeUI?label=星标) ![GitHub License](https://img.shields.io/github/license/Lake1059/FFmpegFreeUI?label=许可证) ![](https://img.shields.io/github/downloads/Lake1059/FFmpegFreeUI/total?label=Github%20总下载量)

<img src="FFmpegFreeUI\Resources\AppIcon.png" width="100" />

## FFmpegFreeUI v6 - 1st Anniversary！

FFmpegFreeUI（简称 3FUI）是在 Windows 上的 [FFmpeg](https://ffmpeg.org) 的专业交互外壳。此，即为真理！这不是给纯小白的一键全自动软件，即便 6.0 已经大幅改善了普通人的体验，但 3FUI 仍旧面向懂基本参数的进阶编码人员，小白上手有门槛但上限无穷大，这不是一个普通的编码软件，而是一整套可扩展平台。

知乎终末诗的教程：https://zhuanlan.zhihu.com/p/1943079795341623993

- 发布形式：所有数据存于当前目录的单文件
- 系统要求：Windows 10 1609+ 仅限 x64 / arm64
- 运行环境：.NET 10（不自带，需安装到系统，可由更新器自动下载安装）
- 基底框架：WinForms
- 交互呈现：[LakeUI](https://github.com/Lake1059/LakeUI) 自主维护的基于 DirectX 的渲染引擎
- 硬件要求：渲染器需要支持 D3D11 的显卡
- 收费情况：所有生产力功能免费 + 个性化功能收费

3FUI 是专为国内环境设计的，语言只有简体中文，不计划任何多语言功能，如有其他语言需求可自行开仓库维护所有字符串，并在 MIT 许可范围内行使所有权利。

3FUI 是 LakeUI 的招牌宣传作品，如果你也想在 WinForms 上制作这样的风格界面，欢迎试用 LakeUI，该产品像 .NET 原生控件一样简单易用，同时价格也非常亲民。也欢迎看看华丽的 [LakeUI 官网](https://lakeui.top/)

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

------

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

选中一个错误的任务，点击 编码队列底部 的 **捕获N个错误** 文字来打开输出日志

**然后发给技术人员！不要发给你的生活AI！说的就是豆包这样的！<br>ffmpeg 的输出并不是所有情况都有具体原因<br>尤其是说功能未支持、参数不正确的，这些情况没有命令行还是算卦<br>连 GPT 和 Claude 都不一定猜得准，更别提国内那些AI的训练数据了！**

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
| -i [string]         | 输入媒体文件                | 已启动软件   |
| -3fui_file [string] | 输入预设文件                | 已启动软件   |
| -ffmpeg [string]... | 把后面的参数全部喂给 ffmpeg | 已启动软件   |
| -test               | 测试用，会弹出“哔哔”        | 已启动软件   |
| fullscreen          | 全屏无边框模式              | 未启动软件   |

- -i 和 -3fui_file 必须一起用，表示使用指定预设文件对指定媒体文件进行任务，预设文件可以直接指定在 Preset 文件夹也就是方案管理中的预设名称。
- -ffmpeg 是纯命令模式，后面的所有内容全都会给 ffmpeg。
- 另外还有用于传递剪辑区间参数的 -3fuiVideoHelperInPointTime 和 -3fuiVideoHelperOutPointTime

## 远程调用

在设置中打开远程调用即可监听指定的端口，收到消息就会开始任务，消息数据内容与启动参数是一样的，这就意味着 3FUI 可以部署在一个巨大的局域网中，只要 3FUI 能够访问文件，那么你可以从这个局域网的任意电脑通过其他程序发任务给编码机上的 3FUI，只要有权限访问，远程访问也是理所当然的。

注意发起程序需要用 UDP 协议发送，默认端口 10591。

(需要5.0及以上版本)

## 插件开发

通过插件，你可以给 3FUI 添加各种功能来满足自己的需求，只需要像我那样把可视化摆上来然后生成对应的参数即可，还可以选择接入我的编码队列，而不用自己做进度显示。

考虑到 ReadyToRun 生成的 exe 无法被添加引用，插件使用 反射 + 特性 + 动态调用 来实现，你在开发插件的时候不需要引用 3FUI，只需要按照我制定的接口标准写代码即可。目前总共只有 4 个接口功能，非常简单，通常情况下你只需要用其中 2 个，所以不要担心要硬啃代码。

首先你需要有与我开发 3FUI 相同的集成开发环境：

1. 下载并安装 [Visual Studio Community 2026](https://visualstudio.microsoft.com/zh-hans/vs/)
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
- 添加自定义界面的方法必须在 Entry 方法中调用，因为我不会在其他地方刷新那个下拉框
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

### 媒体流可视化选择器

从 5.1 版本开始，新增了一个可视化的媒体流选择器，得益于优秀的设计逻辑，这个功能不仅完美接入 3FUI 的现有功能，还可以通过插件调用来为你自己的功能服务。*不过从长远角度来讲，不太建议插件去使用这个功能，因为它的参数太多了，而这种调用方式也无法自定义参数的顺序，会显得很乱。*

VB 语言：

```vb
Public Shared Property HostCall_MediaStreamVisualSelector As Action(Of String, Object, Object, Object, String, String, String, String)
Public Shared Sub SetHost_MediaStreamVisualSelector(action As Object)
	HostCall_AddMissionToQueueWithArgs = CType(action, Action(Of String, Object, Object, Object, String, String, String, String))
End Sub

'调用，注意所有参数都是可选，如果不需要指定，直接给nothing即可
HostCall_MediaStreamVisualSelector.Invoke(
        "FilePath", '字符串，指定要让 ffprobe 读取的文件，如果文件存在那么窗口打开后将自动启动
        VideoStreamTargetObject, 'Object对象，但必须有Text属性，指定要将视频流的选择结果输出到什么对象上
        AudioStreamTargetObject, '同上，指定要将音频流的选择结果输出到什么对象上
        SubtitleStreamTargetObject, '同上，指定要将字幕流的选择结果输出到什么对象上
        "InputFileIndex", '字符串，选择器只能对一个文件的流进行选择，如果你希望输出附带文件索引，可以直接填写这是哪个索引的文件，会像这样输出：0:v:0,0:v:1 如果不设置则仅输出逗号分隔的数字：0,1,2
        "VideoStreamSelected", '字符串，如果用户已经选择了指定的视频流，可以设置此属性来让对应流直接勾选，格式必须是逗号分隔的直接索引：0,1,2 不能带其他字符
        "AudioStreamSelected", '同上，是指定的音频流
        "SubtitleStreamSelected", '同上，是指定的字幕流
        )
```

C# 的版本懒得写了，太长了，让AI直接翻译吧。

### 发布你的插件

当你完成开发和测试后，点击生成即可在输出目录得到你的插件。前面我们选择了窗体应用，所以是生成的 exe 文件，但从 .NET 5 开始这个 exe 是纯二进制文件，同名的 dll 文件才是代码。此时将这个同名的 dll 单独复制出来，将其后缀从 .dll 改为 .3fui.dll，这样这个文件就是你要发布的插件了，而那个 exe 是完全不需要的。

如果你引用了其他三方组件，需要将那些文件一并发布，当然那些文件就不要改后缀了，3FUI 就是用这个后缀来识别哪些是要加载的插件的。

由于你并没有引用 3FUI，相关许可证条款对你不生效，因此你可以选择闭源甚至出售（虽然这并不能保护你的代码，.NET 程序非常容易反编译）。

## 你已获得成就

- 看完了这个 md 文件，击败了全球 99% 的用户
- 或者你直接滑到底，击败了全球 50% 的用户
