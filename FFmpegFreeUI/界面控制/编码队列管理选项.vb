Public Class 编码队列管理选项

    Public Shared ReadOnly a As New 暗黑上下文菜单 With {.ShowImageMargin = False, .Font = Form1.Font}

    Public Shared Sub 初始化()
        AddHandler Form1.UiButton任务管理菜单.MouseDown, Sub(s, e)
                                                       If e.Button = MouseButtons.Left Then
                                                           a.Show(Form1.UiButton任务管理菜单, New Point(15 * Form1.DPI, 15 * Form1.DPI + Form1.UiButton任务管理菜单.Height))
                                                       End If
                                                   End Sub
        a.Items.AddRange(New ToolStripItem() {
                 New ToolStripSeparator() With {.Tag = "null"},
                 New ToolStripMenuItem("任务数据管理") With {.ForeColor = Color.CornflowerBlue, .Enabled = False},
                 New ToolStripMenuItem("(可多选) 复制任务的命令行（每行一个）", Nothing, AddressOf 界面控制_编码队列.复制多个任务的命令行),
                 New ToolStripMenuItem("(可多选) 将参数面板数据  -->  覆盖到任务", Nothing, AddressOf 界面控制_编码队列.将参数面板数据覆盖到任务),
                 New ToolStripMenuItem("(仅单选) 将任务参数  -->  覆盖到参数面板", Nothing, AddressOf 界面控制_编码队列.将任务参数覆盖到参数面板),
                 New ToolStripMenuItem("(可多选) 将任务  -->  放到添加文件选项卡", Nothing, AddressOf 界面控制_编码队列.将任务放到添加文件选项卡),
                 New ToolStripMenuItem("(仅单选) 导出任务参数数据  -->  到预设文件", Nothing, AddressOf 界面控制_编码队列.导出任务参数数据到预设文件),
                 New ToolStripSeparator(),
                 New ToolStripMenuItem("任务数量管理") With {.ForeColor = Color.CornflowerBlue, .Enabled = False},
                 New ToolStripMenuItem("全选", Nothing, AddressOf 界面控制_编码队列.全选任务),
                 New ToolStripMenuItem("反选", Nothing, AddressOf 界面控制_编码队列.反选任务),
                 New ToolStripMenuItem("选中所有出错的任务并定位首个", Nothing, AddressOf 界面控制_编码队列.选中所有出错的任务),
                 New ToolStripMenuItem("定位首个选中", Nothing, Sub() If Form1.ListView1.SelectedItems.Count > 0 Then Form1.ListView1.EnsureVisible(Form1.ListView1.SelectedItems(0).Index)),
                 New ToolStripSeparator(),
                 New ToolStripMenuItem("教学内容") With {.ForeColor = Color.CornflowerBlue, .Enabled = False},
                 New ToolStripMenuItem("查看快捷键", Nothing, Sub() 软件内对话框.显示对话框(Form1, "快捷键", My.Resources.Resource1.编码队列快捷键, New Dictionary(Of String, Action) From {{"了解", Nothing}}, 软件内对话框.主题类型.放行,, 0.3)),
                 New ToolStripSeparator() With {.Tag = "null"}})
    End Sub

    Public Shared Sub 重设字体()
        a.Font = New Font(用户设置.实例对象.字体, a.Font.Size)
    End Sub

End Class