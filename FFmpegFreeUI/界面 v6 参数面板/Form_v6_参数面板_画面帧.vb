Public Class Form_v6_参数面板_画面帧

    Public 所属参数面板对象 As Form_v6_参数面板

    Private Sub Form_v6_参数面板_画面帧_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Form_v6_参数面板_画面帧_Shown(sender As Object, e As EventArgs) Handles Me.Shown

    End Sub

    Private Sub Form_v6_参数面板_画面帧_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged

    End Sub

    Private Sub MB_画面裁剪交互_Click(sender As Object, e As EventArgs) Handles MB_画面裁剪交互.Click
        Form_v6_参数面板.弹出画面区域选择窗口(ModernTextBox1, "画面裁剪")
    End Sub

    Public 私有窗口_抽帧参数 As New Form_v6_参数面板_抽帧参数
    Private Sub MB_抽帧设置_Click(sender As Object, e As EventArgs) Handles MB_抽帧设置.Click
        显示窗体(私有窗口_抽帧参数, FormMain_v6)
    End Sub

    Public 私有窗口_插帧参数 As New Form_v6_参数面板_插帧参数
    Private Sub MB_简易插帧_Click(sender As Object, e As EventArgs) Handles MB_简易插帧.Click
        显示窗体(私有窗口_插帧参数, FormMain_v6)
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
