Imports Microsoft.WindowsAPICodePack.Dialogs

Public Class Form_v6_设置_功能设定
    Private Sub MTB_工作目录_TextChanged(sender As Object, e As EventArgs) Handles MTB_工作目录.TextChanged
        设置_v6.实例对象.工作目录 = MTB_工作目录.Text
    End Sub

    Private Sub MB_浏览文件夹_Click(sender As Object, e As EventArgs) Handles MB_浏览文件夹.Click
        Dim dialog As New CommonOpenFileDialog With {.IsFolderPicker = True}
        If dialog.ShowDialog() = CommonFileDialogResult.Ok Then
            MTB_工作目录.Text = dialog.FileName
        Else
            MTB_工作目录.Text = ""
        End If
    End Sub

    Private Sub MCB_有任务时系统状态_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_有任务时系统状态.SelectedIndexChanged
        设置_v6.实例对象.有任务时系统保持状态选项 = MCB_有任务时系统状态.SelectedIndex
    End Sub

    Private Sub MCB_是否启用提示音_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_是否启用提示音.SelectedIndexChanged
        设置_v6.实例对象.提示音选项 = MCB_是否启用提示音.SelectedIndex
    End Sub

    Private Sub MCB_是否自动开始任务_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_是否自动开始任务.SelectedIndexChanged
        设置_v6.实例对象.自动开始任务选项 = MCB_是否自动开始任务.SelectedIndex
    End Sub

    Private Sub MCB_是否自动重置参数面板到第一个页面_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_是否自动重置参数面板到第一个页面.SelectedIndexChanged
        设置_v6.实例对象.自动重置参数面板的页面选择 = MCB_是否自动重置参数面板到第一个页面.SelectedIndex
    End Sub

    Private Sub MCB_任务名称混淆_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_任务名称混淆.SelectedIndexChanged
        设置_v6.实例对象.混淆任务名称 = MCB_任务名称混淆.SelectedIndex
    End Sub

    Private Sub MCB_独立参数面板自动切预设管理_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_独立参数面板自动切预设管理.SelectedIndexChanged
        设置_v6.实例对象.打开独立参数面板时自动切到预设管理页面 = MCB_独立参数面板自动切预设管理.SelectedIndex
    End Sub

    Private Sub MCB_任务失败删除文件_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_任务失败删除文件.SelectedIndexChanged
        设置_v6.实例对象.任务失败自动删除输出文件 = MCB_任务失败删除文件.SelectedIndex
    End Sub

    Private Sub MCB_编码队列显示最新日志行_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_编码队列显示最新日志行.SelectedIndexChanged
        设置_v6.实例对象.编码队列显示最新日志行 = MCB_编码队列显示最新日志行.SelectedIndex
        编码队列_v6.刷新显示()
    End Sub

    Private Sub MCB_任务日志保留行数_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_任务日志保留行数.SelectedIndexChanged
        设置_v6.实例对象.任务日志保留行数选项 = MCB_任务日志保留行数.SelectedIndex
        编码队列_v6.刷新显示()
    End Sub


End Class
