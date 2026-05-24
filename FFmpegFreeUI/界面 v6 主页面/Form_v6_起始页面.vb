Imports LakeUI

Public Class Form_v6_起始页面

    Private Sub Form_v6_起始页面_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Form_v6_起始页面_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
        Dim a = (Me.ModernPanel1.Width - Me.ModernPanel1.Padding.Left * 2 - Me.JustEmptyControl2.Width * 2) / 3
        Me.ModernPanel4.Width = a
        Me.ModernPanel6.Width = a
    End Sub

    Private Sub MCB_清理内存_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_清理内存.SelectedIndexChanged
        Select Case MCB_清理内存.SelectedIndex
            Case 0
                GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, True, True)
                GC.WaitForPendingFinalizers()
                MCB_清理内存.SelectedIndex = -1
            Case 1
                Module1.EmptyWorkingSet(Process.GetCurrentProcess.Handle)
                MCB_清理内存.SelectedIndex = -1
        End Select
    End Sub

    Private Sub MB_软件本体更新_Click(sender As Object, e As EventArgs) Handles MB_软件本体更新.Click
        If ExOverlayMsgBox(FormMain_v6, $"强制检查本体更新？{If(网络功能.检查软件本体更新最后一次错误 <> "", vbCrLf & vbCrLf & "最后的错误信息：" & 网络功能.检查软件本体更新最后一次错误, "")}", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            网络功能.检查软件本体更新()
        End If
    End Sub

    Private Sub MB_更新器更新_Click(sender As Object, e As EventArgs) Handles MB_更新器更新.Click
        If ExOverlayMsgBox(FormMain_v6, $"强制检查更新器更新？{If(网络功能.检查更新器更新最后一次错误 <> "", vbCrLf & vbCrLf & "最后的错误信息：" & 网络功能.检查更新器更新最后一次错误, "")}", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            网络功能.检查更新器更新(True)
        End If
    End Sub

    Private Sub MB_AI提示_Click(sender As Object, e As EventArgs) Handles MB_AI提示.Click
        ExOverlayMsgBox(FormMain_v6, $"{vbCrLf}能救多少是多少，不听的我们也没办法。{vbCrLf}{vbCrLf}不要拿着 AI 来质疑整个行业几十年的经验！不要信 AI 给你的建议！不要信 AI 给你的建议！不要信 AI 给你的建议！现在的 AI 叫做生成式 AI，底层逻辑是猜，没有自我意识，全信 AI 的人注定被自然选择淘汰。{vbCrLf}{vbCrLf}不懂就去学，没见过就是去见，搞清楚自己需要的究竟是什么，每个人都有自己的需求，盲目复制他人参数不可取。{vbCrLf}{vbCrLf}3FUI 玩的是真理，是一个自由度极高的可扩展平台，不是小白的一键全自动，借用终末诗的一句话：看看自己是不是来错地方了。{vbCrLf}{vbCrLf}祝愿遇见 3FUI 的你能成为压片大师！！", MsgBoxStyle.OkOnly, "致所有新手")
    End Sub

    Private Sub MB_GitHub_Click(sender As Object, e As EventArgs) Handles MB_GitHub.Click
        Process.Start(New ProcessStartInfo With {.FileName = "https://github.com/Lake1059/FFmpegFreeUI", .UseShellExecute = True})
    End Sub

    Private Sub MB_官网_Click(sender As Object, e As EventArgs) Handles MB_官网.Click
        Process.Start(New ProcessStartInfo With {.FileName = "https://ffmpegfreeui.top", .UseShellExecute = True})
    End Sub

    Private Sub MB_哔哩哔哩_Click(sender As Object, e As EventArgs) Handles MB_哔哩哔哩.Click
        Process.Start(New ProcessStartInfo With {.FileName = "https://space.bilibili.com/319785096", .UseShellExecute = True})
    End Sub

    Private Sub MB_爱发电_Click(sender As Object, e As EventArgs) Handles MB_爱发电.Click
        Process.Start(New ProcessStartInfo With {.FileName = "https://afdian.com/a/1059Studio", .UseShellExecute = True})
    End Sub

    Private Sub MB_终末诗_Click(sender As Object, e As EventArgs) Handles MB_终末诗.Click
        Process.Start(New ProcessStartInfo With {.FileName = "https://zhuanlan.zhihu.com/p/1943079795341623993", .UseShellExecute = True})
    End Sub

    Private Sub MB_FFmpegFull_Click(sender As Object, e As EventArgs) Handles MB_FFmpegFull.Click
        Process.Start(New ProcessStartInfo With {.FileName = "https://github.com/netony/FFmpeg-Builds", .UseShellExecute = True})
    End Sub

    Private Sub MB_LakeUI_Click(sender As Object, e As EventArgs) Handles MB_LakeUI.Click
        Process.Start(New ProcessStartInfo With {.FileName = "https://github.com/Lake1059/LakeUI", .UseShellExecute = True})
    End Sub
End Class