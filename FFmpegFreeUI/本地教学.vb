
Imports Sunny.UI

Public Class 本地教学
    Public Shared Sub 初始化()
        绑定下拉框(Form1.UiComboBox字体名称, "使用任何已安装的字体", $"手持两把锟斤拷，口中直呼烫烫烫")
        绑定文本框(Form1.UiTextBox处理器核心, "指定处理器相关性", $"此处指定新的任务要使用哪些处理器逻辑核心来运行，例如 0,1,2,3,4,5{vbCrLf & vbCrLf}可以使用旁边的文本框来快速生成，输入 0~9 然后按下 Enter 键即可直接生成从 CPU 0 到 CPU 9 的相关性文本")
        绑定下拉框(Form1.UiComboBox自动开始最大任务数量, "自动开始任务数量", $"当程序要自动开始任务时，最多允许同时运行的任务数量{vbCrLf & vbCrLf}手动开始任务永远不受数量限制")
        绑定下拉框(Form1.UiComboBox有任务时系统状态, "不要让系统睡觉！", $"系统一旦真的睡死那么任务必定停转，同时还可能导致并发症：软件崩溃、进度刷新卡死无法恢复；所以为了您的优质体验，不要让系统睡觉，如果没插显示器，更推荐设置为阻止显示器关闭")
        绑定下拉框(Form1.UiComboBox提示音, "全体欣赏音乐", $"要自定义音乐，在 3FUI 目录下放置这些文件：{vbCrLf & vbCrLf}完成任务：Sound_Finish.wav{vbCrLf}任务出错：Sound_Error.wav")
        绑定文本框(Form1.UiTextBoxFFmpeg自定义工作目录, "指定 ffmpeg 的工作目录", $"不想把硕大的文件复制到 3FUI 目录？还是要使用指定的 ffmpeg？只需指定工作目录即可")
        绑定转译辅助的文本框(Form1.UiTextBox替代进程的文件名, "在 Linux 和 macOS 上运行", $"我不会任何一种 C，要不然早就做了，要在其他系统上运行只能通过 Wine 转译")
        绑定转译辅助的文本框(Form1.UiTextBox覆盖参数传递, "在 Linux 和 macOS 上运行", $"我不会任何一种 C，要不然早就做了，要在其他系统上运行只能通过 Wine 转译")

        绑定下拉框(Form1.常规流程参数页面.UiComboBox解码器, "解码器", $"除非有特定需求，否则是不用选的，新手经常在这里死磕")
        绑定下拉框(Form1.常规流程参数页面.UiComboBox解码数据格式, "解码数据格式", $"除非有特定需求，否则是不用选的，新手经常在这里死磕")

        绑定下拉框(Form1.常规流程参数页面.UiComboBox编码类别, "编码类别", $"新手经常就只选个类别，旁边的具体编码怎么着都看不见，然后死磕半天还是 CPU 编码，这种奇特的现象通常是由看短视频引起的，此外在众多的样本中还发现了学错了相关知识也会导致这种现象{vbCrLf & vbCrLf}社会学 +3")
        绑定下拉框(Form1.常规流程参数页面.UiComboBox具体编码, "具体编码", $"3FUI 收录了众多编码几乎所有可能的编码器，最新的编码要求使用更新的 ffmpeg，个别编码甚至还未实现，而在新版 ffmpeg 上使用硬件加速还需要对应更新的显卡驱动；区分这些编码器也非常容易：{vbCrLf & vbCrLf}lib = CPU{vbCrLf}nvenc = NVIDIA{vbCrLf}qsv = Intel{vbCrLf}amf = AMD")
        绑定下拉框(Form1.常规流程参数页面.UiComboBox编码预设, "编码预设", $"每个编码器内置的一套速度档位。控制编码器如何平衡编码速度与压缩效率，这通常与画质无关或影响很小。你是需要用更长的时间换更小的文件，还是用更大的文件换更短的时间。通常情况下对编码速度的影响很大，而对压缩效率的影响较小，尤其是硬件加速的编码器，相反对于软件编码的影响则很大")
        绑定下拉框(Form1.常规流程参数页面.UiComboBox配置文件, "配置文件", $"控制编码器的配置档次，也就是需要支持哪些特性，这通常会影响编码复杂度，从而影响到在各种设备上播放的兼容性。例如典型的是否支持 10bit 色深。保留原文件的特性一般直接不写即可")
        绑定下拉框(Form1.常规流程参数页面.UiComboBox场景优化, "场景优化", $"控制编码器如何针对特定的视频内容或播放用途进行专门优化。视频内容方面比如电影、动画、胶片颗粒、静态图片；用途方面比如更快的解码速度、更低的延迟等。通常情况下不需要考虑")
        绑定文本框(Form1.常规流程参数页面.UiTextBoxgpu, "多张同品牌的显卡", $"如果你安装了多张N卡 或者 多张A卡，使用此参数来决定使用哪一张卡，照着任务管理器填索引，对于 Intel 此参数可能是无效的")
        绑定文本框(Form1.常规流程参数页面.UiTextBoxthreads, "CPU 编码线程数", $"使用 CPU 编码的时候可控制编码器使用的性能资源，尤其是内存占用，如果你的内存条太小，可以拉低这个数值；当然如果你想全功率运转且内存条够大，那么不写即可，编码器往往能够吃满性能")

        绑定下拉框(Form1.常规流程参数页面.UiComboBox分辨率, "分辨率", $"直接设定宽高，使用英文小写字母x来连接宽和高")
        绑定文本框(Form1.常规流程参数页面.UiTextBox分辨率自动计算宽度, "让 ffmpeg 自动计算宽和高", $"如果写了宽度那么就会清除高度")
        绑定文本框(Form1.常规流程参数页面.UiTextBox分辨率自动计算高度, "让 ffmpeg 自动计算宽和高", $"如果写了高度那么就会清除宽度")
        绑定文本框(Form1.常规流程参数页面.UiTextBox画面裁剪滤镜参数, "画面裁剪", $"使用旁边的交互窗口来框选，而不是自己硬写")

        绑定下拉框(Form1.常规流程参数页面.UiComboBox帧速率, "帧速率", $"直接设定每秒钟有多少帧，ffmpeg 会自动丢弃帧或者重复帧")
        绑定文本框(Form1.常规流程参数页面.UiTextBox抽帧最大变化比例, "快速抽帧", $"指定超过多少百分比的变化应该保留帧，对 2D 动漫尤其适用，如果你不在乎那些细微的过渡细节的话")

        绑定下拉框(Form1.常规流程参数页面.UiComboBox全局质量控制参数, "硬件加速用 cq，软件编码用 crf", $"除非你要用的编码器不支持或者有特殊需求，否则不应选择 qp。质量的默认值是 23，肉眼无损是 16，注意这是一个均衡标准；23 不一定是糊的，但 16 一定是清晰的，还很可能清晰过度了！；每个编码器的质量度量都不一样，还会受到片源清晰度和实际内容的影响；所以要根据每种不同的情况慢慢尝试，不要指望一步到位！通常情况下硬件加速要设定更大一些的质量值。已经被压过的视频几乎不可能再压得更小，不要用这点参数去挑战压制组的实力！除非你开挂！{vbCrLf}鱼和熊掌不可兼得，编码时间、画面质量、文件体积、解码性能，你总要至少放弃一样，不要想着全都要！")


    End Sub

    Shared Sub 绑定文本框(控件 As UITextBox, 标题 As String, 描述 As String)
        AddHandler 控件.TextBox.MouseWheel, Sub(sender, e)
                                              软件内对话框.显示对话框(标题, 描述, New Dictionary(Of String, Action) From {{"了解", Nothing}}, 软件内对话框.主题类型.放行)
                                          End Sub
        AddHandler 控件.TextBox.KeyDown, Sub(sender, e)
                                           If e.KeyCode <> Keys.F1 Then Exit Sub
                                           软件内对话框.显示对话框(标题, 描述, New Dictionary(Of String, Action) From {{"了解", Nothing}}, 软件内对话框.主题类型.放行)
                                       End Sub
    End Sub
    Shared Sub 绑定下拉框(控件 As UIComboBox, 标题 As String, 描述 As String)
        AddHandler 控件.MouseWheel, Sub(sender, e)
                                      软件内对话框.显示对话框(标题, 描述, New Dictionary(Of String, Action) From {{"了解", Nothing}}, 软件内对话框.主题类型.放行)
                                  End Sub
        AddHandler 控件.MouseDoubleClick, Sub(sender, e)
                                            软件内对话框.显示对话框(标题, 描述, New Dictionary(Of String, Action) From {{"了解", Nothing}}, 软件内对话框.主题类型.放行)
                                        End Sub
    End Sub
    Shared Sub 绑定转译辅助的文本框(控件 As UITextBox, 标题 As String, 描述 As String)
        AddHandler 控件.TextBox.MouseWheel, Sub(sender, e)
                                              软件内对话框.显示对话框(标题, 描述, New Dictionary(Of String, Action) From {{"了解", Nothing}, {"查看转译运行的文档", Sub() Process.Start(New ProcessStartInfo With {.FileName = "https://github.com/Lake1059/FFmpegFreeUI/blob/main/doc/linux.md", .UseShellExecute = True})}}, 软件内对话框.主题类型.放行)
                                          End Sub
        AddHandler 控件.TextBox.KeyDown, Sub(sender, e)
                                           If e.KeyCode <> Keys.F1 Then Exit Sub
                                           软件内对话框.显示对话框(标题, 描述, New Dictionary(Of String, Action) From {{"了解", Nothing}, {"查看转译运行的文档", Sub() Process.Start(New ProcessStartInfo With {.FileName = "https://github.com/Lake1059/FFmpegFreeUI/blob/main/doc/linux.md", .UseShellExecute = True})}}, 软件内对话框.主题类型.放行)
                                       End Sub
    End Sub
End Class
