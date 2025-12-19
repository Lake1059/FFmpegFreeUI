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

        根据文本设置标签高度(Me.Label126)
        根据文本设置标签高度(Me.Label21)
        根据文本设置标签高度(Me.Label1)
        根据文本设置标签高度(Me.Label35)
    End Sub
End Class
