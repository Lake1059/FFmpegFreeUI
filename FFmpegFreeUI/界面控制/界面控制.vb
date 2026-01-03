
Imports Sunny.UI

Public Class 界面控制
    Public Shared Sub 初始化()

        绑定拖动控件移动窗体(Form1.Panel2)

        Form1.设置页面.初始化设置操作响应()

        编码队列右键菜单.初始化()
        编码队列管理选项.初始化()

        设置富文本框行高(Form1.RichTextBox2, 300)

        Form1.TabPage起始页面.Controls.Add(Form1.起始页面)
        Form1.起始页面.调整界面()
        Form1.TabPage准备文件.Controls.Add(Form1.准备文件页面)
        Form1.Panel6.Controls.Add(Form1.常规流程参数页面)
        绑定提示板.绑定参数面板的提示板(Form1.常规流程参数页面)
        Form1.TabPage媒体信息.Controls.Add(Form1.媒体信息页面)
        Form1.TabPage播放器.Controls.Add(Form1.播放器页面)
        Form1.TabPage混流.Controls.Add(Form1.混流页面)
        Form1.TabPage合并.Controls.Add(Form1.合并页面)
        Form1.TabPage设置.Controls.Add(Form1.设置页面)
        Form1.TabPage性能监控.Controls.Add(Form1.性能监控页面)
        Form1.TabPage支持者名单.Controls.Add(Form1.支持者页面)

        Form1.Panel41.AutoSize = True

        AddHandler Form1.UiTabControlMenu1.SelectedIndexChanged, AddressOf 界面校准

        暗黑列表视图自绘制.绑定列表视图事件(Form1.ListView1)

        AddHandler Form1.UiButton开始任务.Click, AddressOf 界面控制_编码队列.开始任务
        AddHandler Form1.UiButton暂停任务.Click, AddressOf 界面控制_编码队列.暂停任务
        AddHandler Form1.UiButton恢复任务.Click, AddressOf 界面控制_编码队列.恢复任务
        AddHandler Form1.UiButton停止任务.Click, AddressOf 界面控制_编码队列.停止任务
        AddHandler Form1.UiButton移除任务.Click, AddressOf 界面控制_编码队列.移除任务
        AddHandler Form1.UiButton重置任务.Click, AddressOf 界面控制_编码队列.重置任务
        AddHandler Form1.UiButton定位输出.Click, AddressOf 界面控制_编码队列.定位输出
        AddHandler Form1.LinkLabel向ffmpeg发送消息.LinkClicked, Sub()
                                                               Dim a1 As String = InputBox("向 FFmpeg 进程发送消息")
                                                               If a1 <> "" Then 编码任务.队列(Form1.ListView1.SelectedItems(0).Index).发送消息(a1)
                                                           End Sub
        Form1.UiComboBox输出显示类型.SelectedIndex = 0
        AddHandler Form1.LinkLabel切换显示输出面板.LinkClicked, Sub()
                                                            If Form1.Panel输出面板.Visible Then
                                                                Form1.Panel输出面板.Visible = False
                                                                Form1.是否打开了输出面板 = False
                                                            Else
                                                                Form1.Panel输出面板.Visible = True
                                                                Form1.是否打开了输出面板 = True
                                                                校准输出面板的宽度()
                                                            End If
                                                        End Sub
        AddHandler Form1.UiButton复制输出.Click, Sub() Clipboard.SetText(Form1.RichTextBox2.Text)
        AddHandler Form1.UiComboBox输出显示类型.SelectedIndexChanged, AddressOf 编码任务.切换输出类型时单独刷新

        AddHandler Form1.选中项刷新信息计时器.Tick, AddressOf 编码任务.选中项刷新信息
        AddHandler Form1.任务进度更新计时器.Tick, AddressOf 编码任务.用定时器刷新到界面上
        AddHandler Form1.性能统计刷新计时器.Tick, AddressOf 性能统计.刷新到界面上

        '==============================================

        Form1.是否初始化 = True
    End Sub

    Public Shared Sub 界面校准()
        Dim 选项卡 As TabPage = Form1.UiTabControlMenu1.SelectedTab
        Form1.性能统计刷新计时器.Enabled = False
        Select Case True
            Case 选项卡.IsEqual(Form1.TabPage起始页面)
                Form1.Panel顶部视觉修正区域_二级选项卡.BackColor = Form1.起始页面.Panel73.BackColor
                Form1.Panel顶部视觉修正区域_二级选项卡.Dock = DockStyle.Fill

            Case 选项卡.IsEqual(Form1.TabPage编码队列)
                Form1.Panel顶部视觉修正区域_二级选项卡.BackColor = Form1.Panel2.BackColor
                Form1.Panel顶部视觉修正区域_二级选项卡.Dock = DockStyle.Fill

                校准编码队列的列宽()
                校准输出面板的宽度()
                Dim s1 As Integer = 0
                For Each c As UIButton In Form1.Panel2.Controls
                    根据文本设置按钮宽度(c)
                    s1 += c.Width
                Next
                根据文本设置按钮宽度(Form1.UiButton任务管理菜单)
                Form1.Panel2.Padding = New Padding((Form1.Panel2.Width - s1) * 0.5, Form1.Panel2.Padding.Top, (Form1.Panel2.Width - s1) * 0.5, 0)
                校准UiComboBox视觉(Form1.UiComboBox输出显示类型)
                Form1.UiCheckBox强制滚动到最后.CheckBoxSize = 20 * Form1.DPI

            Case 选项卡.IsEqual(Form1.TabPage准备文件)
                Form1.Panel顶部视觉修正区域_二级选项卡.BackColor = Form1.准备文件页面.Panel3.BackColor
                Form1.Panel顶部视觉修正区域_二级选项卡.Dock = DockStyle.Fill

                Form1.准备文件页面.界面校准()

            Case 选项卡.IsEqual(Form1.TabPage参数面板)
                Form1.Panel顶部视觉修正区域_二级选项卡.BackColor = Form1.常规流程参数页面.UiTabControlMenu1.TabBackColor
                Form1.Panel顶部视觉修正区域_二级选项卡.Dock = DockStyle.Left
                Form1.Panel顶部视觉修正区域_二级选项卡.Width = Form1.常规流程参数页面.UiTabControlMenu1.ItemSize.Width + 1

                If 用户设置.实例对象.自动重置参数面板的页面选择 = 1 Then Form1.常规流程参数页面.UiTabControlMenu1.SelectedTab = Form1.常规流程参数页面.TabPage参数总览

            Case 选项卡.IsEqual(Form1.TabPage播放器)
                Form1.Panel顶部视觉修正区域_二级选项卡.BackColor = Form1.播放器页面.Panel1.BackColor
                Form1.Panel顶部视觉修正区域_二级选项卡.Dock = DockStyle.Fill

            Case 选项卡.IsEqual(Form1.TabPage媒体信息)
                Form1.Panel顶部视觉修正区域_二级选项卡.BackColor = Form1.媒体信息页面.Panel9.BackColor
                Form1.Panel顶部视觉修正区域_二级选项卡.Dock = DockStyle.Fill

                Form1.媒体信息页面.调整界面()

            Case 选项卡.IsEqual(Form1.TabPage混流)
                Form1.Panel顶部视觉修正区域_二级选项卡.BackColor = Form1.混流页面.BackColor
                Form1.Panel顶部视觉修正区域_二级选项卡.Dock = DockStyle.Fill
                Form1.混流页面.界面校准()

            Case 选项卡.IsEqual(Form1.TabPage合并)
                Form1.Panel顶部视觉修正区域_二级选项卡.BackColor = Form1.合并页面.BackColor
                Form1.Panel顶部视觉修正区域_二级选项卡.Dock = DockStyle.Fill
                Form1.合并页面.界面校准()

            Case 选项卡.IsEqual(Form1.TabPage性能监控)
                Form1.Panel顶部视觉修正区域_二级选项卡.BackColor = Form1.性能监控页面.Panel1.BackColor
                Form1.Panel顶部视觉修正区域_二级选项卡.Dock = DockStyle.Fill

                Form1.性能统计刷新计时器.Enabled = True

            Case 选项卡.IsEqual(Form1.TabPage插件扩展)
                Form1.Panel顶部视觉修正区域_二级选项卡.BackColor = Form1.Panel22.BackColor
                Form1.Panel顶部视觉修正区域_二级选项卡.Dock = DockStyle.Fill

            Case 选项卡.IsEqual(Form1.TabPage设置)
                Form1.Panel顶部视觉修正区域_二级选项卡.BackColor = Form1.设置页面.UiTabControlMenu1.TabBackColor
                Form1.Panel顶部视觉修正区域_二级选项卡.Dock = DockStyle.Left
                Form1.Panel顶部视觉修正区域_二级选项卡.Width = Form1.设置页面.UiTabControlMenu1.ItemSize.Width + 1

                校准UiComboBox视觉(Form1.设置页面.UiComboBox字体名称)
                校准UiComboBox视觉(Form1.设置页面.UiComboBox自动开始最大任务数量)
                校准UiComboBox视觉(Form1.设置页面.UiComboBox有任务时系统状态)
                校准UiComboBox视觉(Form1.设置页面.UiComboBox提示音)
                校准UiComboBox视觉(Form1.设置页面.UiComboBox自动开始任务)
                校准UiComboBox视觉(Form1.设置页面.UiComboBox自动重置参数面板的页面选择)
                校准UiComboBox视觉(Form1.设置页面.UiComboBox混淆任务名称)
                Form1.设置页面.UiCheckBox转译模式.CheckBoxSize = 20 * Form1.DPI

            Case 选项卡.IsEqual(Form1.TabPage支持者名单)
                Form1.Panel顶部视觉修正区域_二级选项卡.BackColor = Form1.支持者页面.BackColor
                Form1.Panel顶部视觉修正区域_二级选项卡.Dock = DockStyle.Fill

                Form1.支持者页面.调整界面()
                Form1.支持者页面.调整顶部按钮宽度()
        End Select
    End Sub

    Public Shared Sub 校准编码队列的列宽()
        Select Case 用户设置.实例对象.界面修正_编码队列的列宽调整逻辑
            Case 0
                Form1.Label状态.Width = 80 * Form1.DPI
                Form1.Label进度.Width = 70 * Form1.DPI
                Form1.Label效率.Width = 80 * Form1.DPI
                Form1.Label输出大小.Width = 150 * Form1.DPI
                Form1.Label质量.Width = 55 * Form1.DPI
                Form1.Label比特率.Width = 115 * Form1.DPI
                Form1.Label预计剩余.Width = 200 * Form1.DPI
            Case 1
                Dim 总宽度 As Integer = Form1.Panel1.Width
                Form1.Label状态.Width = 总宽度 * 0.076
                Form1.Label进度.Width = 总宽度 * 0.071
                Form1.Label效率.Width = 总宽度 * 0.076
                Form1.Label输出大小.Width = 总宽度 * 0.143
                Form1.Label质量.Width = 总宽度 * 0.053
                Form1.Label比特率.Width = 总宽度 * 0.113
                Form1.Label预计剩余.Width = 总宽度 * 0.186
        End Select
        Form1.ListView1.Columns(0).Width = Form1.Panel15.Width - Form1.ListView1.Parent.Padding.Left - 5 * Form1.DPI
        Form1.ListView1.Columns(1).Width = Form1.Label状态.Width
        Form1.ListView1.Columns(2).Width = Form1.Label进度.Width
        Form1.ListView1.Columns(3).Width = Form1.Label效率.Width
        Form1.ListView1.Columns(4).Width = Form1.Label输出大小.Width
        Form1.ListView1.Columns(5).Width = Form1.Label质量.Width
        Form1.ListView1.Columns(6).Width = Form1.Label比特率.Width
        Form1.ListView1.Columns(7).Width = Form1.Label预计剩余.Width - SystemInformation.VerticalScrollBarWidth * Form1.DPI * 2
    End Sub

    Public Shared Sub 校准输出面板的宽度()
        If Form1.Panel输出面板.Visible Then
            Form1.Panel输出面板.Width = Form1.Panel输出面板.Parent.Width * 0.5
        End If
    End Sub

End Class
