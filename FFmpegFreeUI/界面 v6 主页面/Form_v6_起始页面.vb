Imports LakeUI

Public Class Form_v6_起始页面

    Private Sub Form_v6_起始页面_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Form_v6_起始页面_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
        Dim a = (Me.ModernPanel1.Width - Me.ModernPanel1.Padding.Left * 2 - Me.JustEmptyControl2.Width * 2) / 3
        Me.ModernPanel4.Width = a
        Me.MP_新闻列表.Width = a
    End Sub

    Private Sub MCB_清理内存_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_清理内存.SelectedIndexChanged
        Select Case MCB_清理内存.SelectedIndex
            Case 0
                GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, True, True)
                GC.WaitForPendingFinalizers()
            Case 1
                Module1.EmptyWorkingSet(Process.GetCurrentProcess.Handle)
            Case 2
                D3D_PaintBridge.CleanupD2DResources(D3DCacheCleanupLevel.TrimToBudget, Me)
            Case 3
                D3D_PaintBridge.CleanupD2DResources(D3DCacheCleanupLevel.ReleaseAllCaches, Me)
            Case 4
                D3D_PaintBridge.CleanupD2DResources(D3DCacheCleanupLevel.ReleaseEverything, Me)
        End Select
        MCB_清理内存.SelectedIndex = -1
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
        ExOverlayMsgBox(FormMain_v6, $"{vbCrLf}能救多少是多少，不听的我们也没办法。{vbCrLf}{vbCrLf}不要拿着 AI 来质疑整个行业几十年的经验！不要信 AI 给你的建议！不要信 AI 给你的建议！不要信 AI 给你的建议！现在的 AI 叫做生成式 AI，底层逻辑是猜，没有自我意识，全信 AI 的人注定被自然选择淘汰。内置 Agent 有一定可信度是因为系统提示词已经包含部分答案和设计细节。{vbCrLf}{vbCrLf}不懂就去学，没见过就是去见，搞清楚自己需要的究竟是什么，每个人都有自己的需求，盲目复制他人参数不可取。{vbCrLf}{vbCrLf}3FUI 玩的是真理，是一个自由度极高的可扩展平台，不是小白的一键全自动，借用终末诗的一句话：看看自己是不是来错地方了。{vbCrLf}{vbCrLf}祝愿遇见 3FUI 的你能成为压片大师！！", MsgBoxStyle.OkOnly, "致所有新手")
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
        Process.Start(New ProcessStartInfo With {.FileName = "https://zhuanlan.zhihu.com/p/2053530841180009271", .UseShellExecute = True})
    End Sub

    Private Sub MB_FFmpegFull_Click(sender As Object, e As EventArgs) Handles MB_FFmpegFull.Click
        Process.Start(New ProcessStartInfo With {.FileName = "https://qm.qq.com/q/fiauAsddG8", .UseShellExecute = True})
    End Sub

    Private Sub MB_LakeUI_Click(sender As Object, e As EventArgs) Handles MB_LakeUI.Click
        Process.Start(New ProcessStartInfo With {.FileName = "https://github.com/Lake1059/LakeUI", .UseShellExecute = True})
    End Sub

    <CodeAnalysis.SuppressMessage("Performance", "CA1861:不要将常量数组作为参数", Justification:="<挂起>")>
    Private Sub MB_关于3F项目_Click(sender As Object, e As EventArgs) Handles MB_关于3F项目.Click
        Select Case ExOverlayMsgBox(FormMain_v6, "
FFF Project（简称 3F 项目）是以 FFmpeg 的 Shared 库制作的软件系列，这些软件需要直接处理高密度数据而必须采用直接调用的方式，命令行无法提供这些软件的需求。始祖项目是 FFmpegFreeUI（3FUI）但由于该产品直接使用 FFmpeg 的命令行而不包含在本项目内。FFF Project 最大的特色是允许用户自己更换核心，就像 3FUI 一样，这样无需对交互软件进行更改即可享受到最新 FFmpeg 的改进。", {"前往仓库查看", "现在不"}, "了解全新的 3F 项目")
            Case 0
                Process.Start(New ProcessStartInfo With {.FileName = "https://github.com/Lake1059/FFF_Project", .UseShellExecute = True})
        End Select
    End Sub

    <CodeAnalysis.SuppressMessage("Performance", "CA1861:不要将常量数组作为参数", Justification:="<挂起>")>
    Private Sub MB_3FR单源录制器_Click(sender As Object, e As EventArgs) Handles MB_3FR单源录制器.Click
        Select Case ExOverlayMsgBox(FormMain_v6, "
3FR 是一款简单明确的单源录屏软件，它不对标市面上任何产品，而是一条全新的赛道：3FR 将常规录屏软件的简单快速和近似 OBS 的专业控制进行了融合，可谓是又简单又专业。

最低系统要求 Windows 10 1803，推荐 Windows 10 2004+
只使用 WGC 进行窗口捕获 + 显示器捕获，反作弊安全
收录 AV1/HEVC/AVC 的软件编码器和红绿蓝三家硬件编码器
自由使用 CRF/CQ/QP/自定义参数 来进行质量控制
支持 CFR/VFR 自由切换
支持 420/422/444 采样，支持 8bit/10bit
支持 HDR PQ，可调节 SDR 亮度和 HDR 最大亮度
音频收录 AAC/NMR/FDK，无损支持 WAV 24bit/32bit 和 FLAC
MKV 直出，无混流合成步骤，被中断仍可正常播放", {"立即下载", "前往仓库查看", "现在不"}, "获取 FFF.Recorder / 3FR")
            Case 0
                Process.Start(New ProcessStartInfo With {.FileName = "https://github.com/Lake1059/FFF_Project/releases", .UseShellExecute = True})
            Case 1
                Process.Start(New ProcessStartInfo With {.FileName = "https://github.com/Lake1059/FFF_Project", .UseShellExecute = True})
        End Select
    End Sub

    Private Sub MB_3FP极简本地播放器_Click(sender As Object, e As EventArgs) Handles MB_3FP极简本地播放器.Click
        Select Case ExOverlayMsgBox(FormMain_v6, "
3FP 是一款独特路线的本地视频播放器，也支持单独的音频和图片。自主 D3D11 渲染引擎，由 avcodec 等 dll 解码后直接送到自己的渲染逻辑中，没有任何第三方插手，全 GPU 渲染链路 + 真实高亮 HDR 支持，占用极低，功能极简，可完全取代普通人的日常本地播放需求。

私有协议：3FP 作为 3FUI 的全新可视化剪辑区间交互软件，当 3FP 切换至剪辑区间模式时键盘方向键变为帧级精确操作以及提供帧级时间戳显示，可一键送入 3FUI 的参数面板。

过老的 Win10 版本不兼容，强烈推荐 Windows 10 22H2+
界面仿 PotPlayer 设计，更易于上手，但功能有较大差别
理论上支持 ffmpeg 的全部编码，工程上需要逐步适配
采用 WASAPI 音频输出，支持独占模式以提供最佳音质
支持 SRT / ASS / SSA / SUP 字幕
支持哔哩哔哩规范的弹幕
三挡 HDR 模式，适配不同的屏幕和场景", {"立即下载", "前往仓库查看", "现在不"}, "获取 FFF.Player / 3FP")
            Case 0
                Process.Start(New ProcessStartInfo With {.FileName = "https://github.com/Lake1059/FFF_Project/releases", .UseShellExecute = True})
            Case 1
                Process.Start(New ProcessStartInfo With {.FileName = "https://github.com/Lake1059/FFF_Project", .UseShellExecute = True})
        End Select
    End Sub
End Class
