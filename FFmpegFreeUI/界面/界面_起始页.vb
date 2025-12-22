Public Class 界面_起始页
    Private Sub 界面_起始页_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        绑定拖动控件移动窗体(Me.Panel73)
        绑定拖动控件移动窗体(Me.Label主标题)
        绑定拖动控件移动窗体(Me.Label副标题)
        绑定拖动控件移动窗体(Me.PictureBox1)
        绑定拖动控件移动窗体(Me.Label11)
        绑定拖动控件移动窗体(Me.Label36)

        AddHandler Me.LinkLabel清理内存.LinkClicked, AddressOf 回收自身内存占用
        AddHandler Me.LinkLabel7.LinkClicked, Sub() Process.Start(New ProcessStartInfo With {.FileName = "https://github.com/Lake1059/FFmpegFreeUI", .UseShellExecute = True})
        AddHandler Me.LinkLabel2.LinkClicked, Sub() Process.Start(New ProcessStartInfo With {.FileName = "https://ffmpeg.org/documentation.html", .UseShellExecute = True})
        AddHandler Me.LinkLabel3.LinkClicked, Sub() Process.Start(New ProcessStartInfo With {.FileName = "https://www.gyan.dev/ffmpeg/builds/", .UseShellExecute = True})
        AddHandler Me.LinkLabel4.LinkClicked, Sub() Process.Start(New ProcessStartInfo With {.FileName = "https://github.com/BtbN/FFmpeg-Builds/releases", .UseShellExecute = True})
        AddHandler Me.LinkLabel5.LinkClicked, Sub() Process.Start(New ProcessStartInfo With {.FileName = "https://afdian.com/a/1059Studio", .UseShellExecute = True})
        AddHandler Me.LinkLabel6.LinkClicked, Sub() Process.Start(New ProcessStartInfo With {.FileName = "https://space.bilibili.com/319785096", .UseShellExecute = True})
        AddHandler Me.LinkLabel1.LinkClicked, Sub() Process.Start(New ProcessStartInfo With {.FileName = "https://ffmpegfreeui.top", .UseShellExecute = True})
        AddHandler Me.LinkLabel8.LinkClicked, Sub() Process.Start(New ProcessStartInfo With {.FileName = "https://3fui.top", .UseShellExecute = True})
        AddHandler Me.LinkLabel9.LinkClicked, Sub()
                                                  软件内对话框.显示对话框(Form1, "不要相信 AI 的建议 !!!", "生成式 AI 没有自我意识，运作原理也不是大众认为的那样，其回答依赖其训练数据和网络搜索甚至后台控制，你搜不到的东西 AI 基本上也搜不到，而且 AI 还会一本正经地胡说八道，各模型之间经常唱反调，甚至每次对话的答案都不一样。搞清楚自己的需求到底是什么，编解码本就是非常专业的事情，属于重度生产力，只是在媒体和市场需求的渲染下让大众觉得很简单。在提问的时候不要拿着 AI 的建议来质疑整个行业几十年的经验！", New Dictionary(Of String, Action) From {{"了解", Nothing}, {"懒得看字", Nothing}, {"不了解", Nothing}, {"了解但不了解", Nothing}, {"杀意渐起", Nothing}}, 软件内对话框.主题类型.错误, 0.3, 0.3)
                                              End Sub

        根据文本设置标签高度(Me.Label126)
        根据文本设置标签高度(Me.Label21)
        根据文本设置标签高度(Me.Label1)
        根据文本设置标签高度(Me.Label35)
    End Sub
End Class
