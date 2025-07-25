﻿Imports Microsoft.WindowsAPICodePack.Dialogs
Imports Sunny.UI

Public Class 界面控制
    Public Shared Sub 初始化()
        绑定拖动控件移动窗体(Form1.Panel73)
        绑定拖动控件移动窗体(Form1.Label主标题)
        绑定拖动控件移动窗体(Form1.Label副标题)
        绑定拖动控件移动窗体(Form1.PictureBox1)
        绑定拖动控件移动窗体(Form1.Label11)

        Dim 字体列表 As New List(Of String)
        For Each 字体 As FontFamily In FontFamily.Families
            字体列表.Add(字体.Name)
        Next
        字体列表.Sort()

        Form1.UiComboBox字体名称.Font = New Font(SystemFonts.DefaultFont.FontFamily.Name, 9.75)
        Form1.UiComboBox字体名称.Items.AddRange(字体列表.ToArray)
        AddHandler Form1.UiButton4.Click, Sub()
                                              If Form1.UiComboBox字体名称.Text = "" Then Exit Sub
                                              SetControlFont(Form1.UiComboBox字体名称.Text, Form1, {Form1.UiComboBox字体名称})
                                              Form1.ListView1.ContextMenuStrip.Font = New Font(Form1.UiComboBox字体名称.Text, Form1.ListView1.ContextMenuStrip.Font.Size)
                                              用户设置.实例对象.字体 = Form1.UiComboBox字体名称.Text
                                          End Sub
        AddHandler Form1.UiComboBox字体名称.TextChanged, Sub()
                                                         If Form1.UiComboBox字体名称.Text = "" Then Exit Sub
                                                         If Not Form1.UiComboBox字体名称.Items.Contains(Form1.UiComboBox字体名称.Text) Then Exit Sub
                                                         Form1.Label字体预览文本.Font = New Font(Form1.UiComboBox字体名称.Text, Form1.Label字体预览文本.Font.Size)
                                                     End Sub

        AddHandler Form1.UiTextBox处理器核心.TextChanged, Sub() 用户设置.实例对象.指定处理器核心 = Form1.UiTextBox处理器核心.Text
        AddHandler Form1.UiComboBox自动开始最大任务数量.SelectedIndexChanged, Sub()
                                                                        用户设置.实例对象.自动同时运行任务数量选项 = Form1.UiComboBox自动开始最大任务数量.SelectedIndex
                                                                        Select Case Form1.UiComboBox自动开始最大任务数量.SelectedIndex
                                                                            Case 0 : 同时运行任务上限 = 1
                                                                            Case 1 : 同时运行任务上限 = 2
                                                                            Case 2 : 同时运行任务上限 = 3
                                                                            Case 3 : 同时运行任务上限 = 5
                                                                            Case 4 : 同时运行任务上限 = 10
                                                                            Case Else : 同时运行任务上限 = 1
                                                                        End Select
                                                                    End Sub

        AddHandler Form1.UiComboBox有任务时系统状态.SelectedIndexChanged, Sub()
                                                                      If Form1.UiComboBox有任务时系统状态.Text = "" Then Exit Sub
                                                                      If Form1.UiComboBox有任务时系统状态.SelectedIndex < 0 Then Exit Sub
                                                                      用户设置.实例对象.有任务时系统保持状态选项 = Form1.UiComboBox有任务时系统状态.SelectedIndex
                                                                  End Sub
        AddHandler Form1.UiComboBox提示音.SelectedIndexChanged, Sub() 用户设置.实例对象.提示音选项 = Form1.UiComboBox提示音.SelectedIndex

        AddHandler Form1.UiTextBoxFFmpeg自定义工作目录.TextChanged, Sub() 用户设置.实例对象.工作目录 = Form1.UiTextBoxFFmpeg自定义工作目录.Text
        AddHandler Form1.UiButton13.Click, Sub()
                                               Dim dialog As New CommonOpenFileDialog With {.IsFolderPicker = True}
                                               If dialog.ShowDialog() = CommonFileDialogResult.Ok Then Form1.UiTextBoxFFmpeg自定义工作目录.Text = dialog.FileName
                                           End Sub
        AddHandler Form1.UiTextBox替代进程的文件名.TextChanged, Sub() 用户设置.实例对象.替代进程文件名 = Form1.UiTextBox替代进程的文件名.Text
        AddHandler Form1.UiTextBox覆盖参数传递.TextChanged, Sub() 用户设置.实例对象.覆盖参数传递 = Form1.UiTextBox覆盖参数传递.Text
        AddHandler Form1.UiCheckBox转译模式.Click, Sub() 用户设置.实例对象.转译模式 = Form1.UiCheckBox转译模式.Checked = True



        Form1.编码队列右键菜单 = New 暗黑上下文菜单 With {.ShowImageMargin = False, .Font = Form1.Font}
        Form1.ListView1.ContextMenuStrip = Form1.编码队列右键菜单
        Form1.编码队列右键菜单.Items.AddRange(New ToolStripItem() {
                                                             New ToolStripSeparator() With {.Tag = "null"},
                                                             New ToolStripMenuItem("任务状态控制") With {.ForeColor = Color.CornflowerBlue, .Enabled = False},
                                                             New ToolStripMenuItem("全新开始任务", Nothing, AddressOf 界面控制_编码队列.开始任务) With {.ForeColor = Color.YellowGreen},
                                                             New ToolStripMenuItem("暂停（挂起进程）", Nothing, AddressOf 界面控制_编码队列.暂停任务) With {.ForeColor = Color.Goldenrod},
                                                             New ToolStripMenuItem("继续（恢复进程）", Nothing, AddressOf 界面控制_编码队列.恢复任务) With {.ForeColor = Color.YellowGreen},
                                                             New ToolStripMenuItem("停止（关闭进程）", Nothing, AddressOf 界面控制_编码队列.停止任务) With {.ForeColor = Color.IndianRed},
                                                             New ToolStripMenuItem("移除", Nothing, AddressOf 界面控制_编码队列.移除任务) With {.ForeColor = Color.IndianRed},
                                                             New ToolStripMenuItem("重置状态", Nothing, AddressOf 界面控制_编码队列.重置任务),
                                                             New ToolStripSeparator(),
                                                             New ToolStripMenuItem("任务管理") With {.ForeColor = Color.CornflowerBlue, .Enabled = False},
                                                             New ToolStripMenuItem("全选", Nothing, AddressOf 界面控制_编码队列.全选任务),
                                                             New ToolStripSeparator() With {.Tag = "null"}
                                                         })
        AddHandler Form1.Labelffmpeg实时信息.Click, Sub()
                                                    Dim a1 As String = InputBox("向 FFmpeg 进程发送消息")
                                                    If a1 <> "" Then 编码任务.队列(Form1.ListView1.SelectedItems(0).Index).发送消息(a1)
                                                End Sub

        设置富文本框行高(Form1.RichTextBox1, 350)
        Form1.RichTextBox1.AllowDrop = True
        Form1.Panel6.Controls.Add(Form1.常规流程参数页面)
        Form1.常规流程参数页面.Dock = DockStyle.Fill
        Form1.TabPageEX混流.Controls.Add(Form1.混流页面)
        Form1.混流页面.Dock = DockStyle.Fill
        Form1.TabPageEX合并.Controls.Add(Form1.合并页面)
        Form1.合并页面.Dock = DockStyle.Fill

        Form1.UiComboBox21.SelectedIndex = 0
        Form1.Panel41.AutoSize = True

        AddHandler Form1.UiTabControlMenu1.SelectedIndexChanged, AddressOf 界面校准

        AddHandler Form1.LinkLabel7.LinkClicked, Sub() Process.Start(New ProcessStartInfo With {.FileName = "https://github.com/Lake1059/FFmpegFreeUI", .UseShellExecute = True})
        AddHandler Form1.LinkLabel2.LinkClicked, Sub() Process.Start(New ProcessStartInfo With {.FileName = "https://ffmpeg.org/documentation.html", .UseShellExecute = True})
        AddHandler Form1.LinkLabel3.LinkClicked, Sub() Process.Start(New ProcessStartInfo With {.FileName = "https://www.gyan.dev/ffmpeg/builds/", .UseShellExecute = True})
        AddHandler Form1.LinkLabel4.LinkClicked, Sub() Process.Start(New ProcessStartInfo With {.FileName = "https://github.com/BtbN/FFmpeg-Builds/releases", .UseShellExecute = True})
        AddHandler Form1.LinkLabel5.LinkClicked, Sub() Process.Start(New ProcessStartInfo With {.FileName = "https://afdian.com/a/1059Studio", .UseShellExecute = True})
        AddHandler Form1.LinkLabel6.LinkClicked, Sub() Process.Start(New ProcessStartInfo With {.FileName = "https://space.bilibili.com/319785096", .UseShellExecute = True})

        暗黑列表视图自绘制.绑定列表视图事件(Form1.ListView1)
        暗黑列表视图自绘制.绑定列表视图事件(Form1.ListView2)
        暗黑列表视图自绘制.绑定列表视图事件(Form1.ListView3)
        暗黑列表视图自绘制.绑定列表视图事件(Form1.ListView4)

        AddHandler Form1.UiButton3.Click, AddressOf 界面控制_编码队列.开始任务
        AddHandler Form1.UiButton1.Click, AddressOf 界面控制_编码队列.暂停任务
        AddHandler Form1.UiButton2.Click, AddressOf 界面控制_编码队列.恢复任务
        AddHandler Form1.UiButton5.Click, AddressOf 界面控制_编码队列.停止任务
        AddHandler Form1.UiButton6.Click, AddressOf 界面控制_编码队列.移除任务
        AddHandler Form1.UiButton16.Click, AddressOf 界面控制_编码队列.重置任务
        AddHandler Form1.UiButton9.Click, AddressOf 界面控制_编码队列.定位输出
        AddHandler Form1.UiButton8.Click, AddressOf 界面控制_编码队列.重新配置
        AddHandler Form1.UiButton重新添加.Click, AddressOf 界面控制_编码队列.重新添加
        AddHandler Form1.UiButton7.Click, AddressOf 界面控制_编码队列.导出配置
        AddHandler Form1.UiButton15.Click, AddressOf 界面控制_编码队列.复制命令行

        AddHandler Form1.UiButton14.Click, AddressOf 界面控制_添加文件.加入编码队列
        AddHandler Form1.ListView2.DragEnter, AddressOf 界面控制_添加文件.ListView2_DragEnter
        AddHandler Form1.ListView2.DragDrop, AddressOf 界面控制_添加文件.ListView2_DragDrop
        AddHandler Form1.UiButton18.Click, AddressOf 界面控制_添加文件.添加文件
        AddHandler Form1.UiButton10.Click, AddressOf 界面控制_添加文件.打开文件夹添加全部文件
        AddHandler Form1.UiButton11.Click, AddressOf 界面控制_添加文件.批量移除选中项
        AddHandler Form1.UiButton12.Click, AddressOf 界面控制_添加文件.移除全部项
        AddHandler Form1.UiComboBox21.SelectedIndexChanged, AddressOf 界面控制_添加文件.选择输出目录

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
                Form1.UiComboBox字体名称.ItemHeight = 30 * Form1.DPI
                Form1.UiComboBox自动开始最大任务数量.ItemHeight = 30 * Form1.DPI
                Form1.UiComboBox有任务时系统状态.ItemHeight = 30 * Form1.DPI
                Form1.UiCheckBox转译模式.CheckBoxSize = 20 * Form1.DPI

            Case 选项卡.IsEqual(Form1.TabPage编码队列)
                Form1.Label1.Width = Form1.Panel1.Width - Form1.Panel1.Padding.Left - Form1.Label2.Width - Form1.Label3.Width - Form1.Label4.Width - Form1.Label5.Width - Form1.Label6.Width - Form1.Label7.Width - 200 * Form1.DPI
                Form1.ListView1.Columns(0).Width = Form1.Label1.Width - 5 * Form1.DPI
                Form1.ListView1.Columns(1).Width = Form1.Label2.Width
                Form1.ListView1.Columns(2).Width = Form1.Label3.Width
                Form1.ListView1.Columns(3).Width = Form1.Label4.Width
                Form1.ListView1.Columns(4).Width = Form1.Label5.Width
                Form1.ListView1.Columns(5).Width = Form1.Label6.Width
                Form1.ListView1.Columns(6).Width = Form1.Label7.Width
                Form1.ListView1.Columns(7).Width = Form1.Label8.Width - SystemInformation.VerticalScrollBarWidth * Form1.DPI * 2

                Dim s1 As Integer = 0
                For Each c As UIButton In Form1.Panel2.Controls
                    s1 += c.Width
                Next
                Form1.Panel2.Padding = New Padding((Form1.Panel2.Width - s1) * 0.5, Form1.Panel2.Padding.Top, (Form1.Panel2.Width - s1) * 0.5, 0)

            Case 选项卡.IsEqual(Form1.TabPage添加文件)
                Form1.ListView2.Columns(0).Width = Form1.ListView2.Width - SystemInformation.VerticalScrollBarWidth * Form1.DPI * 2
                Form1.UiComboBox输出容器.ItemHeight = 30 * Form1.DPI
                Form1.UiComboBox21.ItemHeight = 30 * Form1.DPI

            Case 选项卡.IsEqual(Form1.TabPage转码压制)
                Form1.常规流程参数页面.界面校准()

            Case 选项卡.IsEqual(Form1.TabPage媒体信息)
                Form1.RichTextBox1.Size = New Size(Form1.RichTextBox1.Parent.Width, Form1.RichTextBox1.Parent.Height - Form1.RichTextBox1.Parent.Padding.Top * 2)

            Case 选项卡.IsEqual(Form1.TabPageEX混流)
                Form1.混流页面.界面校准()

            Case 选项卡.IsEqual(Form1.TabPageEX合并)
                Form1.合并页面.界面校准()

            Case 选项卡.IsEqual(Form1.TabPage性能监控)
                Form1.Panel18.Width = Form1.Panel18.Parent.Width * 0.3
                Form1.ListView3.Columns(0).Width = Form1.ListView3.Width * 0.7 - SystemInformation.VerticalScrollBarWidth * Form1.DPI
                Form1.ListView3.Columns(1).Width = Form1.ListView3.Width * 0.3
                Form1.ListView4.Columns(0).Width = Form1.ListView4.Width * 0.8 - SystemInformation.VerticalScrollBarWidth * Form1.DPI
                Form1.ListView4.Columns(1).Width = Form1.ListView4.Width * 0.2
                Form1.性能统计刷新计时器.Enabled = True

        End Select
    End Sub



End Class
