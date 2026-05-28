Imports System.IO
Imports LakeUI

Public Class Form_v6_设置_个性化
    Private Sub MB_前往购买_Click(sender As Object, e As EventArgs) Handles MB_前往购买.Click
        Process.Start(New ProcessStartInfo With {.FileName = "https://ifdian.net/item/15f0758814a911f1979752540025c377", .UseShellExecute = True})
    End Sub

    Private Sub MB_窗口标题文字_Click(sender As Object, e As EventArgs) Handles MB_窗口标题文字.Click
        设置_v6.实例对象.SP_窗口标题文字 = ExInputBox(FormMain_v6, "自定义窗口标题文本，清空即表示还原")
    End Sub

    Private Sub MB_起始页面顶栏标题_Click(sender As Object, e As EventArgs) Handles MB_起始页面顶栏标题.Click
        设置_v6.实例对象.SP_起始页面顶栏标题 = ExInputBox(FormMain_v6, "自定义起始页面顶栏标题文本，清空即表示还原，该位置是 LakeUI 的 HtmlColorLabel，因此可以使用受支持的 HTML 标记来定制字体和颜色")
    End Sub

    Private Sub MB_起始页面顶栏副标题_Click(sender As Object, e As EventArgs) Handles MB_起始页面顶栏副标题.Click
        设置_v6.实例对象.SP_起始页面顶栏副标题 = ExInputBox(FormMain_v6, "自定义起始页面顶栏副标题文本，清空即表示还原，该位置是 LakeUI 的 HtmlColorLabel，因此可以使用受支持的 HTML 标记来定制字体和颜色")
    End Sub

    Private Sub MB_图标_Click(sender As Object, e As EventArgs) Handles MB_图标.Click
        Select Case ExMsgBox($"图标除了显示在窗口左上角，还会显示在起始页面顶栏左侧大图，因此推荐使用较为清晰的 png 透明背景图片，注意顶栏位置的图片为了确保质量使用 2x SSAA 渲染，所以分辨率不宜过高。{vbCrLf & vbCrLf}设置之后如果要取消，请直接删除当前目录下的 {Path.GetFileName(设置_v6.自定义图标路径)} 并重启软件即可。", New List(Of ExMsgBoxButton) From {New ExMsgBoxButton("选择图标", True), New ExMsgBoxButton("取消")}, , MsgBoxStyle.Information, FormMain_v6)
            Case 0
                Dim a As New OpenFileDialog With {.Multiselect = False, .Filter = "支持的图片|*.png;*.jpg;*.gif"}
                If a.ShowDialog(FormMain_v6) = DialogResult.OK Then
                    FileIO.FileSystem.CopyFile(a.FileName, 设置_v6.自定义图标路径, True)
                    设置_v6.加载SP自定义图标()
                End If
        End Select
    End Sub

    Private Sub MB_起始页顶栏背景图_Click(sender As Object, e As EventArgs) Handles MB_起始页顶栏背景图.Click
        Select Case ExMsgBox($"起始页面顶栏是一个长条区域，因此非常适合宽图的展示。{vbCrLf & vbCrLf}设置之后如果要取消，请直接删除当前目录下的 {Path.GetFileName(设置_v6.自定义起始页顶栏背景图路径)} 并重启软件即可。", New List(Of ExMsgBoxButton) From {New ExMsgBoxButton("选择图片", True), New ExMsgBoxButton("取消")}, , MsgBoxStyle.Information, FormMain_v6)
            Case 0
                Dim a As New OpenFileDialog With {.Multiselect = False, .Filter = "支持的图片|*.png;*.jpg"}
                If a.ShowDialog(FormMain_v6) = DialogResult.OK Then
                    FileIO.FileSystem.CopyFile(a.FileName, 设置_v6.自定义起始页顶栏背景图路径, True)
                    设置_v6.加载SP自定义起始页顶栏背景图()
                End If
        End Select
    End Sub

    Private Sub MB_窗口边框颜色_Click(sender As Object, e As EventArgs) Handles MB_窗口边框颜色.Click
        Dim a As New ModernColorDialog With {.SelectedColor = FormMain_v6.ThisIsYourWindow1.BorderColor, .Icon = FormMain_v6.Icon}
        FormMain_v6.ThisIsYourWindow1.Attach(a)
        If a.ShowDialog(FormMain_v6) = DialogResult.OK Then
            设置_v6.实例对象.SP_窗口边框颜色_A = a.SelectedColor.A
            设置_v6.实例对象.SP_窗口边框颜色_R = a.SelectedColor.R
            设置_v6.实例对象.SP_窗口边框颜色_G = a.SelectedColor.G
            设置_v6.实例对象.SP_窗口边框颜色_B = a.SelectedColor.B
            FormMain_v6.ThisIsYourWindow1.BorderColor = a.SelectedColor
            FormMain_v6.ThisIsYourWindow1.BorderInactiveColor = a.SelectedColor
        End If
    End Sub

    Private Sub MB_分层阴影颜色_Click(sender As Object, e As EventArgs) Handles MB_分层阴影颜色.Click
        Dim a As New ModernColorDialog With {.SelectedColor = FormMain_v6.ThisIsYourWindow1.LayerShadowColor, .Icon = FormMain_v6.Icon}
        FormMain_v6.ThisIsYourWindow1.Attach(a)
        If a.ShowDialog(FormMain_v6) = DialogResult.OK Then
            设置_v6.实例对象.SP_分层阴影颜色_A = a.SelectedColor.A
            设置_v6.实例对象.SP_分层阴影颜色_R = a.SelectedColor.R
            设置_v6.实例对象.SP_分层阴影颜色_G = a.SelectedColor.G
            设置_v6.实例对象.SP_分层阴影颜色_B = a.SelectedColor.B
            FormMain_v6.ThisIsYourWindow1.LayerShadowColor = a.SelectedColor
        End If
    End Sub

    Private Sub MCB_边框宽度_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_边框宽度.SelectedIndexChanged
        If Not SP_UnLock Then Exit Sub
        设置_v6.实例对象.SP_边框宽度 = MCB_边框宽度.SelectedIndex
        FormMain_v6.ThisIsYourWindow1.BorderSize = MCB_边框宽度.SelectedIndex
    End Sub

    Private Sub MCB_毛玻璃模式_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_毛玻璃模式.SelectedIndexChanged
        If Not SP_UnLock Then Exit Sub
        设置_v6.实例对象.SP_毛玻璃模式 = MCB_毛玻璃模式.SelectedIndex
        Select Case 设置_v6.实例对象.SP_毛玻璃模式
            Case 0
                FormMain_v6.ThisIsYourWindow1.BackdropMode = ThisIsYourWindow.BackdropModeEnum.None
                FormMain_v6.ThisIsYourWindow1.BackdropImage = Nothing
                FormMain_v6.ThisIsYourWindow1.BackdropNoiseOpacity = 0
                MCB_背景来源.SelectedIndex = -1
                MCB_噪点颗粒.SelectedIndex = -1
                MCB_背景来源.Enabled = False
                MCB_噪点颗粒.Enabled = False
            Case 1
                FormMain_v6.ThisIsYourWindow1.BackdropBlurPasses = 0
                MCB_背景来源.Enabled = True
                MCB_噪点颗粒.Enabled = True
            Case 2
                FormMain_v6.ThisIsYourWindow1.BackdropBlurPasses = 1
                MCB_背景来源.Enabled = True
                MCB_噪点颗粒.Enabled = True
            Case 3
                FormMain_v6.ThisIsYourWindow1.BackdropBlurPasses = 3
                MCB_背景来源.Enabled = True
                MCB_噪点颗粒.Enabled = True
        End Select
    End Sub

    Private Sub MCB_背景来源_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_背景来源.SelectedIndexChanged
        If Not SP_UnLock Then Exit Sub
        设置_v6.实例对象.SP_毛玻璃背景来源 = MCB_背景来源.SelectedIndex
        Select Case 设置_v6.实例对象.SP_毛玻璃背景来源
            Case 0
                FormMain_v6.ThisIsYourWindow1.BackdropMode = ThisIsYourWindow.BackdropModeEnum.Image
                设置_v6.加载SP自定义背景图()
            Case 1
                FormMain_v6.ThisIsYourWindow1.BackdropMode = ThisIsYourWindow.BackdropModeEnum.Auto
                FormMain_v6.ThisIsYourWindow1.BackdropImage = Nothing
        End Select
    End Sub

    Private Sub MCB_噪点颗粒_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_噪点颗粒.SelectedIndexChanged
        If Not SP_UnLock Then Exit Sub
        设置_v6.实例对象.SP_毛玻璃噪点颗粒 = MCB_噪点颗粒.SelectedIndex
        Select Case MCB_噪点颗粒.SelectedIndex
            Case 0
                FormMain_v6.ThisIsYourWindow1.BackdropNoiseOpacity = 0
            Case 1
                FormMain_v6.ThisIsYourWindow1.BackdropNoiseOpacity = 18
        End Select
    End Sub

    Private Sub MB_选择背景图_Click(sender As Object, e As EventArgs) Handles MB_选择背景图.Click
        Select Case ExMsgBox($"要使用背景图，必须启用毛玻璃模式。{vbCrLf & vbCrLf}设置之后如果要取消，请直接删除当前目录下的 {Path.GetFileName(设置_v6.自定义背景图路径)} 并重启软件即可。", New List(Of ExMsgBoxButton) From {New ExMsgBoxButton("选择图片", True), New ExMsgBoxButton("取消")}, , MsgBoxStyle.Information, FormMain_v6)
            Case 0
                Dim a As New OpenFileDialog With {.Multiselect = False, .Filter = "支持的图片|*.png;*.jpg"}
                If a.ShowDialog(FormMain_v6) = DialogResult.OK Then
                    FileIO.FileSystem.CopyFile(a.FileName, 设置_v6.自定义背景图路径, True)
                    设置_v6.加载SP自定义背景图()
                End If
        End Select
    End Sub

    Private Sub Form_v6_设置_个性化_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub


End Class