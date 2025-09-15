Public Class 编码队列右键菜单

    Public Shared ReadOnly a As New 暗黑上下文菜单 With {.ShowImageMargin = False, .Font = Form1.Font}

    Public Shared Sub 初始化()
        Form1.ListView1.ContextMenuStrip = a
        a.Items.AddRange(New ToolStripItem() {
                         New ToolStripSeparator() With {.Tag = "null"},
                         New ToolStripMenuItem("任务状态控制") With {.ForeColor = Color.CornflowerBlue, .Enabled = False},
                         New ToolStripMenuItem("全新开始任务", Nothing, AddressOf 界面控制_编码队列.开始任务) With {.ForeColor = Color.YellowGreen},
                         New ToolStripMenuItem("暂停（挂起进程）", Nothing, AddressOf 界面控制_编码队列.暂停任务) With {.ForeColor = Color.Goldenrod},
                         New ToolStripMenuItem("继续（恢复进程）", Nothing, AddressOf 界面控制_编码队列.恢复任务) With {.ForeColor = Color.YellowGreen},
                         New ToolStripMenuItem("停止（关闭进程）", Nothing, AddressOf 界面控制_编码队列.停止任务) With {.ForeColor = Color.IndianRed},
                         New ToolStripMenuItem("移除", Nothing, AddressOf 界面控制_编码队列.移除任务) With {.ForeColor = Color.IndianRed},
                         New ToolStripMenuItem("重置状态", Nothing, AddressOf 界面控制_编码队列.重置任务),
                         New ToolStripSeparator() With {.Tag = "null"}})
    End Sub

    Public Shared Sub 重设字体()
        a.Font = New Font(用户设置.实例对象.字体, a.Font.Size)
    End Sub

End Class
