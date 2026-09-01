Public Class Form_v6_参数面板_画面帧

    Public 所属参数面板对象 As Form_v6_参数面板
    Private 正在更新缩放算法选项 As Boolean = False

    Private Shared ReadOnly CPU缩放算法列表 As String() = {
        "", "lanczos", "bilinear", "fast_bilinear", "bicubic", "neighbor",
        "area", "bicublin", "gauss", "sinc", "spline"
    }
    Private Shared ReadOnly CUDA缩放算法列表 As String() = {
        "", "lanczos", "bilinear", "nearest", "bicubic"
    }

    Private Sub Form_v6_参数面板_画面帧_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        更新缩放滤镜交互()
    End Sub

    Private Sub MCB_指定缩放滤镜_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_指定缩放滤镜.SelectedIndexChanged
        If 正在更新缩放算法选项 Then Exit Sub
        更新缩放滤镜交互()
        通知参数面板刷新()
    End Sub

    Private Sub MCB_强调帧率模式_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_强调帧率模式.SelectedIndexChanged
        通知参数面板刷新()
    End Sub

    Public Sub 更新缩放滤镜交互()
        If 正在更新缩放算法选项 OrElse MCB_指定缩放滤镜 Is Nothing OrElse MCB_指定缩放算法 Is Nothing Then Exit Sub

        正在更新缩放算法选项 = True
        Try
            Dim 当前算法 = If(MCB_指定缩放算法.Text, "").Trim()
            Dim 使用CUDA = String.Equals(获取缩放滤镜标识符(), "scale_cuda", StringComparison.OrdinalIgnoreCase)
            Dim 算法列表 = If(使用CUDA, CUDA缩放算法列表, CPU缩放算法列表)

            MCB_指定缩放算法.Items.Clear()
            MCB_指定缩放算法.Items.AddRange(算法列表)
            MCB_指定缩放算法.Enabled = 算法列表.Length > 1

            If 算法列表.Any(Function(x) String.Equals(x, 当前算法, StringComparison.OrdinalIgnoreCase)) Then
                MCB_指定缩放算法.Text = 当前算法
            Else
                MCB_指定缩放算法.SelectedIndex = 0
                MCB_指定缩放算法.Text = ""
            End If
        Finally
            正在更新缩放算法选项 = False
        End Try
    End Sub

    Public Function 获取缩放滤镜标识符() As String
        Select Case MCB_指定缩放滤镜.SelectedIndex
            Case 2
                Return "scale_cuda"
            Case 1
                Return "scale"
            Case Else
                Return ""
        End Select
    End Function

    Private Sub 通知参数面板刷新()
        If 所属参数面板对象 Is Nothing OrElse 所属参数面板对象.抑制自动刷新 Then Exit Sub
        所属参数面板对象.请求刷新参数状态()
    End Sub

    Private Sub Form_v6_参数面板_画面帧_Shown(sender As Object, e As EventArgs) Handles Me.Shown

    End Sub

    Private Sub Form_v6_参数面板_画面帧_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged

    End Sub

    Private Sub MB_画面裁剪交互_Click(sender As Object, e As EventArgs) Handles MB_画面裁剪交互.Click
        Form_v6_参数面板.弹出画面区域选择窗口(MTB_画面裁剪参数, "画面裁剪")
    End Sub

    Public 私有窗口_抽帧参数 As New Form_v6_参数面板_抽帧参数
    Private Sub MB_抽帧设置_Click(sender As Object, e As EventArgs) Handles MB_抽帧设置.Click
        显示窗体(私有窗口_抽帧参数, FormMain_v6)
    End Sub

    Public 私有窗口_简易插帧参数 As New Form_v6_参数面板_插帧_简易
    Private Sub MB_简易插帧_Click(sender As Object, e As EventArgs) Handles MB_简易插帧.Click
        显示窗体(私有窗口_简易插帧参数, FormMain_v6)
    End Sub
    Public 私有窗口_NV_FRUC_插帧参数 As New Form_v6_参数面板_插帧_NV_FRUC
    Private Sub MB_NV_FRUC_插帧_Click(sender As Object, e As EventArgs) Handles MB_NV_FRUC_插帧.Click
        显示窗体(私有窗口_NV_FRUC_插帧参数, FormMain_v6)
    End Sub
    Public 私有窗口_烧录字幕 As New Form_v6_参数面板_烧录字幕
    Private Sub MB_烧录字幕_Click(sender As Object, e As EventArgs) Handles MB_烧录字幕.Click
        显示窗体(私有窗口_烧录字幕, FormMain_v6)
    End Sub
    Public 私有窗口_降噪 As New Form_v6_参数面板_降噪
    Private Sub MB_传统降噪_Click(sender As Object, e As EventArgs) Handles MB_传统降噪.Click
        显示窗体(私有窗口_降噪, FormMain_v6)
    End Sub
    Public 私有窗口_锐化 As New Form_v6_参数面板_锐化
    Private Sub MB_传统锐化_Click(sender As Object, e As EventArgs) Handles MB_传统锐化.Click
        显示窗体(私有窗口_锐化, FormMain_v6)
    End Sub
    Public 私有窗口_胶片颗粒 As New Form_v6_参数面板_胶片颗粒
    Private Sub MB_胶片颗粒_Click(sender As Object, e As EventArgs) Handles MB_胶片颗粒.Click
        显示窗体(私有窗口_胶片颗粒, FormMain_v6)
    End Sub
    Public 私有窗口_动态模糊 As New Form_v6_参数面板_动态模糊
    Private Sub MB_动态模糊_Click(sender As Object, e As EventArgs) Handles MB_动态模糊.Click
        显示窗体(私有窗口_动态模糊, FormMain_v6)
    End Sub
    Public 私有窗口_着色器超分 As New Form_v6_参数面板_超分
    Private Sub MB_着色器超分_Click(sender As Object, e As EventArgs) Handles MB_着色器超分.Click
        显示窗体(私有窗口_着色器超分, FormMain_v6)
    End Sub
    Public 私有窗口_扫描方式 As New Form_v6_参数面板_扫描方式
    Private Sub MB_扫描方式_Click(sender As Object, e As EventArgs) Handles MB_扫描方式.Click
        显示窗体(私有窗口_扫描方式, FormMain_v6)
    End Sub
    Public 私有窗口_画面翻转 As New Form_v6_参数面板_画面翻转
    Private Sub MB_画面翻转_Click(sender As Object, e As EventArgs) Handles MB_画面翻转.Click
        显示窗体(私有窗口_画面翻转, FormMain_v6)
    End Sub
    Public 私有窗口_平滑断层 As New Form_v6_参数面板_平滑断层
    Private Sub MB_平滑断层_Click(sender As Object, e As EventArgs) Handles MB_平滑断层.Click
        显示窗体(私有窗口_平滑断层, FormMain_v6)
    End Sub


End Class
