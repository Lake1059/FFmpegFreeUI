Public Class Form_v6_设置_个性化
    Private Sub MB_前往购买_Click(sender As Object, e As EventArgs) Handles MB_前往购买.Click

    End Sub

    Private Sub MB_窗口标题文字_Click(sender As Object, e As EventArgs) Handles MB_窗口标题文字.Click

    End Sub

    Private Sub MB_起始页面顶栏标题_Click(sender As Object, e As EventArgs) Handles MB_起始页面顶栏标题.Click

    End Sub

    Private Sub MB_起始页面顶栏副标题_Click(sender As Object, e As EventArgs) Handles MB_起始页面顶栏副标题.Click

    End Sub

    Private Sub MB_图标_Click(sender As Object, e As EventArgs) Handles MB_图标.Click

    End Sub

    Private Sub MB_起始页顶栏背景图_Click(sender As Object, e As EventArgs) Handles MB_起始页顶栏背景图.Click

    End Sub

    Private Sub MB_窗口边框颜色_Click(sender As Object, e As EventArgs) Handles MB_窗口边框颜色.Click

    End Sub

    Private Sub MB_分层阴影颜色_Click(sender As Object, e As EventArgs) Handles MB_分层阴影颜色.Click

    End Sub

    Private Sub MCB_边框宽度_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_边框宽度.SelectedIndexChanged
        If Not SP_UnLock Then Exit Sub
        设置_v6.实例对象.SP_边框宽度 = MCB_边框宽度.SelectedIndex
        FormMain_v6.ThisIsYourWindow1.BorderSize = 设置_v6.实例对象.SP_边框宽度
    End Sub

    Private Sub MCB_毛玻璃模式_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_毛玻璃模式.SelectedIndexChanged
        If Not SP_UnLock Then Exit Sub
        设置_v6.实例对象.SP_毛玻璃模式 = MCB_毛玻璃模式.SelectedIndex
        Select Case 设置_v6.实例对象.SP_毛玻璃模式
            Case 0
                FormMain_v6.ThisIsYourWindow1.BackdropMode = LakeUI.ThisIsYourWindow.BackdropModeEnum.None
                FormMain_v6.ThisIsYourWindow1.BackdropImage = Nothing
                FormMain_v6.ThisIsYourWindow1.BackdropNoiseOpacity = 0
                MCB_背景来源.SelectedIndex = -1
                MCB_噪点颗粒.SelectedIndex = -1
                MCB_背景来源.Enabled = False
                MCB_噪点颗粒.Enabled = False
            Case 1
                FormMain_v6.ThisIsYourWindow1.BackdropBlurPasses = 1
                MCB_背景来源.Enabled = True
                MCB_噪点颗粒.Enabled = True
                If MCB_背景来源.SelectedIndex = -1 Then MCB_背景来源.SelectedIndex = 0
                If MCB_噪点颗粒.SelectedIndex = -1 Then MCB_噪点颗粒.SelectedIndex = 0
            Case 2
                FormMain_v6.ThisIsYourWindow1.BackdropBlurPasses = 3
                MCB_背景来源.Enabled = True
                MCB_噪点颗粒.Enabled = True
                If MCB_背景来源.SelectedIndex = -1 Then MCB_背景来源.SelectedIndex = 0
                If MCB_噪点颗粒.SelectedIndex = -1 Then MCB_噪点颗粒.SelectedIndex = 0
        End Select
    End Sub

    Private 自定义背景图路径 As String = IO.Path.Combine(Application.StartupPath, "SP_BackImage.png")

    Private Sub MCB_背景来源_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_背景来源.SelectedIndexChanged
        If Not SP_UnLock Then Exit Sub
        设置_v6.实例对象.SP_毛玻璃背景来源 = MCB_背景来源.SelectedIndex
        Select Case 设置_v6.实例对象.SP_毛玻璃背景来源
            Case 0
                FormMain_v6.ThisIsYourWindow1.BackdropMode = LakeUI.ThisIsYourWindow.BackdropModeEnum.Image
                If FileIO.FileSystem.FileExists(自定义背景图路径) Then
                    FormMain_v6.ThisIsYourWindow1.BackdropImage = LoadImageFromFile(自定义背景图路径)
                Else
                    FormMain_v6.ThisIsYourWindow1.BackdropImage = My.Resources.Resource1.SP_默认背景图
                End If
            Case 1
                FormMain_v6.ThisIsYourWindow1.BackdropMode = LakeUI.ThisIsYourWindow.BackdropModeEnum.Auto
                FormMain_v6.ThisIsYourWindow1.BackdropImage = Nothing
        End Select
    End Sub

    Private Sub MCB_噪点颗粒_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_噪点颗粒.SelectedIndexChanged
        If Not SP_UnLock Then Exit Sub
        Select Case MCB_噪点颗粒.SelectedIndex
            Case 0
                FormMain_v6.ThisIsYourWindow1.BackdropNoiseOpacity = 0
            Case 1
                FormMain_v6.ThisIsYourWindow1.BackdropNoiseOpacity = 18
        End Select
    End Sub

    Private Sub MB_选择背景图_Click(sender As Object, e As EventArgs) Handles MB_选择背景图.Click

    End Sub

    Private Sub MB_恢复默认背景图_Click(sender As Object, e As EventArgs) Handles MB_恢复默认背景图.Click

    End Sub

    Private Sub Form_v6_设置_个性化_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub


End Class