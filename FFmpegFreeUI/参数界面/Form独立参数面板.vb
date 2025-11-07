Public Class Form独立参数面板

    Public 参数面板 As 界面_常规流程参数_V2
    Public 文件列表 As String()

    Private Sub Form独立参数面板_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        启用Win32API深色模式(Me.Handle)
        参数面板 = New 界面_常规流程参数_V2
        Me.Panel3.Controls.Add(参数面板)
        参数面板.Dock = DockStyle.Fill
        参数面板.BringToFront()
    End Sub

    Private Sub Form独立参数面板_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        SetControlFont(用户设置.实例对象.字体, Me,, True)
    End Sub

    Private Sub UiButton确认并添加任务_Click(sender As Object, e As EventArgs) Handles UiButton确认并添加任务.Click
        If 界面控制_添加文件.加入编码队列(文件列表, 参数面板) Then
            Me.Close()
        End If
    End Sub

End Class