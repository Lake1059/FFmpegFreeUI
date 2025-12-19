Public Class 界面_性能监控

    Public 是否初始化 As Boolean = False

    Private Sub 界面_性能监控_Load(sender As Object, e As EventArgs) Handles Me.Load
        校准界面()
        FlowLayoutPanel1.Controls.Clear()
        UiRoundProcess物理内存.Value = 0
        UiRoundProcess虚拟内存.Value = 0
        UiRoundProcess解码核心.Value = 0
        UiRoundProcess编码核心.Value = 0
        UiRoundProcessPCIE带宽.Value = 0
        UiRoundProcess显存.Value = 0
        UiRoundProcess3D.Value = 0
        UiRoundProcessCopy.Value = 0
        UiRoundProcessCore.Value = 0
        UiRoundProcess核心功耗.Value = 0
        UiRoundProcess整卡功耗.Value = 0
        是否初始化 = True
    End Sub

    Public Sub 校准界面()
        Panel2.Width = (40 * 8 + 20 + 2 * 7 - 2) * Form1.DPI
        Panel18.Width = (Panel2.Width - Panel17.Padding.Left - Label21.Width) * 0.5

        FlowLayoutPanel1.Width = FlowLayoutPanel1.Parent.Width + SystemInformation.VerticalScrollBarWidth + 2 * Form1.DPI

        UiRoundProcess物理内存.Inner = 30 * Form1.DPI
        UiRoundProcess物理内存.Outer = 40 * Form1.DPI
        UiRoundProcess虚拟内存.Inner = 30 * Form1.DPI
        UiRoundProcess虚拟内存.Outer = 40 * Form1.DPI

        UiRoundProcess解码核心.Inner = 30 * Form1.DPI
        UiRoundProcess解码核心.Outer = 40 * Form1.DPI
        UiRoundProcess编码核心.Inner = 30 * Form1.DPI
        UiRoundProcess编码核心.Outer = 40 * Form1.DPI
        UiRoundProcessPCIE带宽.Inner = 30 * Form1.DPI
        UiRoundProcessPCIE带宽.Outer = 40 * Form1.DPI
        UiRoundProcess显存.Inner = 30 * Form1.DPI
        UiRoundProcess显存.Outer = 40 * Form1.DPI

        UiRoundProcess3D.Inner = 25 * Form1.DPI
        UiRoundProcess3D.Outer = 30 * Form1.DPI
        UiRoundProcessCopy.Inner = 25 * Form1.DPI
        UiRoundProcessCopy.Outer = 30 * Form1.DPI
        UiRoundProcessCore.Inner = 25 * Form1.DPI
        UiRoundProcessCore.Outer = 30 * Form1.DPI
        UiRoundProcess核心功耗.Inner = 25 * Form1.DPI
        UiRoundProcess核心功耗.Outer = 30 * Form1.DPI
        UiRoundProcess整卡功耗.Inner = 25 * Form1.DPI
        UiRoundProcess整卡功耗.Outer = 30 * Form1.DPI

    End Sub

    Private Sub 界面_性能监控_DpiChangedAfterParent(sender As Object, e As EventArgs) Handles Me.DpiChangedAfterParent
        If 是否初始化 Then 校准界面()
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Dim a As New 暗黑上下文菜单 With {.ShowImageMargin = False, .Font = Form1.Font}
        a.Items.Add(New ToolStripSeparator() With {.Tag = "null"})
        a.Items.Add(New ToolStripMenuItem("选择显卡") With {.ForeColor = Color.CornflowerBlue, .Enabled = False})
        For Each item In 性能统计.显卡信息表.Keys
            Dim b As New ToolStripMenuItem(item) With {.Tag = item}
            AddHandler b.Click, Sub() LinkLabel1.Text = item
            a.Items.Add(b)
        Next
        a.Items.Add(New ToolStripSeparator() With {.Tag = "null"})
        a.Show(LinkLabel1, New Point(20 * Form1.DPI, LinkLabel1.Height + 20 * Form1.DPI))
    End Sub
End Class
