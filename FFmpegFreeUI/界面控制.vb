Imports Microsoft.WindowsAPICodePack.Dialogs
Imports Sunny.UI

Public Class 界面控制
    Public Shared Sub 初始化()
        绑定拖动控件移动窗体(Form1.Panel73)
        绑定拖动控件移动窗体(Form1.Label主标题)
        绑定拖动控件移动窗体(Form1.Label副标题)
        绑定拖动控件移动窗体(Form1.PictureBox1)
        绑定拖动控件移动窗体(Form1.Label11)
        绑定拖动控件移动窗体(Form1.Label36)
        绑定拖动控件移动窗体(Form1.Panel2)

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
                                                                            Case 3 : 同时运行任务上限 = 4
                                                                            Case 4 : 同时运行任务上限 = 5
                                                                            Case 5 : 同时运行任务上限 = 6
                                                                            Case 6 : 同时运行任务上限 = 7
                                                                            Case 7 : 同时运行任务上限 = 8
                                                                            Case 8 : 同时运行任务上限 = 9
                                                                            Case 9 : 同时运行任务上限 = 10
                                                                            Case Else : 同时运行任务上限 = 1
                                                                        End Select
                                                                    End Sub

        AddHandler Form1.UiComboBox有任务时系统状态.SelectedIndexChanged, Sub()
                                                                      If Form1.UiComboBox有任务时系统状态.Text = "" Then Exit Sub
                                                                      If Form1.UiComboBox有任务时系统状态.SelectedIndex < 0 Then Exit Sub
                                                                      用户设置.实例对象.有任务时系统保持状态选项 = Form1.UiComboBox有任务时系统状态.SelectedIndex
                                                                  End Sub
        AddHandler Form1.UiComboBox提示音.SelectedIndexChanged, Sub() 用户设置.实例对象.提示音选项 = Form1.UiComboBox提示音.SelectedIndex
        AddHandler Form1.UiComboBox自动开始任务.SelectedIndexChanged, Sub() 用户设置.实例对象.自动开始任务选项 = Form1.UiComboBox自动开始任务.SelectedIndex

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
        AddHandler Form1.LinkLabel向ffmpeg发送消息.LinkClicked, Sub()
                                                               Dim a1 As String = InputBox("向 FFmpeg 进程发送消息")
                                                               If a1 <> "" Then 编码任务.队列(Form1.ListView1.SelectedItems(0).Index).发送消息(a1)
                                                           End Sub

        设置富文本框行高(Form1.RichTextBox1, 350)
        设置富文本框行高(Form1.RichTextBox2, 300)
        Form1.RichTextBox1.AllowDrop = True
        Form1.Panel6.Controls.Add(Form1.常规流程参数页面)
        Form1.常规流程参数页面.Dock = DockStyle.Fill
        Form1.TabPageEX混流.Controls.Add(Form1.混流页面)
        Form1.混流页面.Dock = DockStyle.Fill
        Form1.TabPageEX合并.Controls.Add(Form1.合并页面)
        Form1.合并页面.Dock = DockStyle.Fill

        Form1.UiComboBox输出目录.SelectedIndex = 0
        Form1.Panel41.AutoSize = True

        AddHandler Form1.UiTabControlMenu1.SelectedIndexChanged, AddressOf 界面校准

        AddHandler Form1.LinkLabel7.LinkClicked, Sub() Process.Start(New ProcessStartInfo With {.FileName = "https://github.com/Lake1059/FFmpegFreeUI", .UseShellExecute = True})
        AddHandler Form1.LinkLabel2.LinkClicked, Sub() Process.Start(New ProcessStartInfo With {.FileName = "https://ffmpeg.org/documentation.html", .UseShellExecute = True})
        AddHandler Form1.LinkLabel3.LinkClicked, Sub() Process.Start(New ProcessStartInfo With {.FileName = "https://www.gyan.dev/ffmpeg/builds/", .UseShellExecute = True})
        AddHandler Form1.LinkLabel4.LinkClicked, Sub() Process.Start(New ProcessStartInfo With {.FileName = "https://github.com/BtbN/FFmpeg-Builds/releases", .UseShellExecute = True})
        AddHandler Form1.LinkLabel5.LinkClicked, Sub() Process.Start(New ProcessStartInfo With {.FileName = "https://afdian.com/a/1059Studio", .UseShellExecute = True})
        AddHandler Form1.LinkLabel6.LinkClicked, Sub() Process.Start(New ProcessStartInfo With {.FileName = "https://space.bilibili.com/319785096", .UseShellExecute = True})
        AddHandler Form1.LinkLabel1.LinkClicked, Sub() Process.Start(New ProcessStartInfo With {.FileName = "https://ffmpegfreeui.top", .UseShellExecute = True})
        AddHandler Form1.LinkLabel8.LinkClicked, Sub() Process.Start(New ProcessStartInfo With {.FileName = "https://3fui.top", .UseShellExecute = True})

        暗黑列表视图自绘制.绑定列表视图事件(Form1.ListView1)
        暗黑列表视图自绘制.绑定列表视图事件(Form1.ListView2)
        性能统计.绑定性能统计处理器列表视图事件()
        性能统计.绑定性能统计显卡列表视图事件()

        AddHandler Form1.UiButton开始任务.Click, AddressOf 界面控制_编码队列.开始任务
        AddHandler Form1.UiButton暂停任务.Click, AddressOf 界面控制_编码队列.暂停任务
        AddHandler Form1.UiButton恢复任务.Click, AddressOf 界面控制_编码队列.恢复任务
        AddHandler Form1.UiButton停止任务.Click, AddressOf 界面控制_编码队列.停止任务
        AddHandler Form1.UiButton移除任务.Click, AddressOf 界面控制_编码队列.移除任务
        AddHandler Form1.UiButton重置任务.Click, AddressOf 界面控制_编码队列.重置任务
        AddHandler Form1.UiButton定位输出.Click, AddressOf 界面控制_编码队列.定位输出
        AddHandler Form1.UiButton重新配置.Click, AddressOf 界面控制_编码队列.重新配置
        AddHandler Form1.UiButton重新添加.Click, AddressOf 界面控制_编码队列.重新添加
        AddHandler Form1.UiButton导出配置.Click, AddressOf 界面控制_编码队列.导出配置
        AddHandler Form1.UiButton复制命令行.Click, AddressOf 界面控制_编码队列.复制命令行

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


        AddHandler Form1.UiButton14.Click, AddressOf 界面控制_添加文件.加入编码队列
        AddHandler Form1.ListView2.DragEnter, AddressOf 界面控制_添加文件.ListView2_DragEnter
        AddHandler Form1.ListView2.DragDrop, AddressOf 界面控制_添加文件.ListView2_DragDrop
        AddHandler Form1.UiButton18.Click, AddressOf 界面控制_添加文件.添加文件
        AddHandler Form1.UiButton10.Click, AddressOf 界面控制_添加文件.打开文件夹添加全部文件
        AddHandler Form1.UiButton11.Click, AddressOf 界面控制_添加文件.批量移除选中项
        AddHandler Form1.UiButton12.Click, AddressOf 界面控制_添加文件.移除全部项

        AddHandler Form1.UiButton选择容器.MouseDown, AddressOf 选择输出容器鼠标按下事件
        AddHandler Form1.UiComboBox输出目录.SelectedIndexChanged, AddressOf 界面控制_添加文件.选择输出目录

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


            Case 选项卡.IsEqual(Form1.TabPage编码队列)
                Form1.Label1.Width = Form1.Panel1.Width - Form1.Panel1.Padding.Left - Form1.Label2.Width - Form1.Label3.Width - Form1.Label4.Width - Form1.Label5.Width - Form1.Label6.Width - Form1.Label7.Width - 200 * Form1.DPI
                Form1.ListView1.Columns(0).Width = Form1.Label1.Width - Form1.ListView1.Parent.Padding.Left - 5 * Form1.DPI
                Form1.ListView1.Columns(1).Width = Form1.Label2.Width
                Form1.ListView1.Columns(2).Width = Form1.Label3.Width
                Form1.ListView1.Columns(3).Width = Form1.Label4.Width
                Form1.ListView1.Columns(4).Width = Form1.Label5.Width
                Form1.ListView1.Columns(5).Width = Form1.Label6.Width
                Form1.ListView1.Columns(6).Width = Form1.Label7.Width
                Form1.ListView1.Columns(7).Width = Form1.Label8.Width - SystemInformation.VerticalScrollBarWidth * Form1.DPI * 2
                校准输出面板的宽度()
                Dim s1 As Integer = 0
                For Each c As UIButton In Form1.Panel2.Controls
                    s1 += c.Width
                Next
                Form1.Panel2.Padding = New Padding((Form1.Panel2.Width - s1) * 0.5, Form1.Panel2.Padding.Top, (Form1.Panel2.Width - s1) * 0.5, 0)
                校准UiComboBox高DPI(Form1.UiComboBox输出显示类型)
                Form1.UiCheckBox强制滚动到最后.CheckBoxSize = 20 * Form1.DPI

            Case 选项卡.IsEqual(Form1.TabPage添加文件)
                Form1.ListView2.Columns(0).Width = Form1.ListView2.Width - SystemInformation.VerticalScrollBarWidth * Form1.DPI * 2
                校准UiComboBox高DPI(Form1.UiComboBox输出目录)
                Form1.Label那个神奇的视觉标签.Width = 1

            Case 选项卡.IsEqual(Form1.TabPage参数面板)


            Case 选项卡.IsEqual(Form1.TabPage媒体信息)
                Form1.RichTextBox1.Size = New Size(Form1.RichTextBox1.Parent.Width - Form1.RichTextBox1.Parent.Padding.Left, Form1.RichTextBox1.Parent.Height - Form1.RichTextBox1.Parent.Padding.Top * 2)

            Case 选项卡.IsEqual(Form1.TabPageEX混流)
                Form1.混流页面.界面校准()

            Case 选项卡.IsEqual(Form1.TabPageEX合并)
                Form1.合并页面.界面校准()

            Case 选项卡.IsEqual(Form1.TabPage性能监控)
                Form1.Panel18.Width = Form1.Panel18.Parent.Width * 0.3
                Form1.ListView3.Columns(0).Width = Form1.ListView3.Parent.Width - Form1.ListView3.Parent.Padding.Left - SystemInformation.VerticalScrollBarWidth * Form1.DPI * 2
                Form1.ListView4.Columns(0).Width = Form1.ListView4.Parent.Width - Form1.ListView4.Parent.Padding.Left - SystemInformation.VerticalScrollBarWidth * Form1.DPI * 2
                Form1.性能统计刷新计时器.Enabled = True

            Case 选项卡.IsEqual(Form1.TabPage软件设置)
                校准UiComboBox高DPI(Form1.UiComboBox字体名称)
                校准UiComboBox高DPI(Form1.UiComboBox自动开始最大任务数量)
                校准UiComboBox高DPI(Form1.UiComboBox有任务时系统状态)
                校准UiComboBox高DPI(Form1.UiComboBox提示音)
                校准UiComboBox高DPI(Form1.UiComboBox自动开始任务)
                Form1.UiCheckBox转译模式.CheckBoxSize = 20 * Form1.DPI

        End Select
    End Sub


    Public Shared Sub 选择输出容器鼠标按下事件(sender As Object, e As MouseEventArgs)

        Dim b As New 暗黑上下文菜单 With {.ShowImageMargin = False, .Font = Form1.Font}
        b.Items.AddRange(New ToolStripItem() {
             New ToolStripSeparator() With {.Tag = "null"},
             New ToolStripMenuItem(".mp4", Nothing, Sub(s1, e1) Form1.UiTextBox输出容器.Text = s1.Text) With {.ForeColor = Color.Silver},
             New ToolStripMenuItem(".mkv", Nothing, Sub(s1, e1) Form1.UiTextBox输出容器.Text = s1.Text) With {.ForeColor = Color.Silver},
             New ToolStripMenuItem(".flv", Nothing, Sub(s1, e1) Form1.UiTextBox输出容器.Text = s1.Text) With {.ForeColor = Color.Silver},
             New ToolStripMenuItem(".mov", Nothing, Sub(s1, e1) Form1.UiTextBox输出容器.Text = s1.Text) With {.ForeColor = Color.Silver},
             New ToolStripMenuItem(".webm", Nothing, Sub(s1, e1) Form1.UiTextBox输出容器.Text = s1.Text) With {.ForeColor = Color.Silver},
             New ToolStripMenuItem(".wmv", Nothing, Sub(s1, e1) Form1.UiTextBox输出容器.Text = s1.Text) With {.ForeColor = Color.Silver},
             New ToolStripMenuItem(".avi", Nothing, Sub(s1, e1) Form1.UiTextBox输出容器.Text = s1.Text) With {.ForeColor = Color.Silver},
             New ToolStripMenuItem(".rmvb", Nothing, Sub(s1, e1) Form1.UiTextBox输出容器.Text = s1.Text) With {.ForeColor = Color.Silver},
             New ToolStripMenuItem(".ts", Nothing, Sub(s1, e1) Form1.UiTextBox输出容器.Text = s1.Text) With {.ForeColor = Color.Silver},
             New ToolStripMenuItem(".3gp", Nothing, Sub(s1, e1) Form1.UiTextBox输出容器.Text = s1.Text) With {.ForeColor = Color.Silver},
             New ToolStripSeparator() With {.Tag = "null"}})

        Dim c As New 暗黑上下文菜单 With {.ShowImageMargin = False, .Font = Form1.Font}
        c.Items.AddRange(New ToolStripItem() {
             New ToolStripSeparator() With {.Tag = "null"},
             New ToolStripMenuItem(".mp3", Nothing, Sub(s1, e1) Form1.UiTextBox输出容器.Text = s1.Text) With {.ForeColor = Color.Silver},
             New ToolStripMenuItem(".aac", Nothing, Sub(s1, e1) Form1.UiTextBox输出容器.Text = s1.Text) With {.ForeColor = Color.Silver},
             New ToolStripMenuItem(".wav", Nothing, Sub(s1, e1) Form1.UiTextBox输出容器.Text = s1.Text) With {.ForeColor = Color.Silver},
             New ToolStripMenuItem(".flac", Nothing, Sub(s1, e1) Form1.UiTextBox输出容器.Text = s1.Text) With {.ForeColor = Color.Silver},
             New ToolStripMenuItem(".alac", Nothing, Sub(s1, e1) Form1.UiTextBox输出容器.Text = s1.Text) With {.ForeColor = Color.Silver},
             New ToolStripMenuItem(".aiff", Nothing, Sub(s1, e1) Form1.UiTextBox输出容器.Text = s1.Text) With {.ForeColor = Color.Silver},
             New ToolStripMenuItem(".ac3", Nothing, Sub(s1, e1) Form1.UiTextBox输出容器.Text = s1.Text) With {.ForeColor = Color.Silver},
             New ToolStripMenuItem(".ogg", Nothing, Sub(s1, e1) Form1.UiTextBox输出容器.Text = s1.Text) With {.ForeColor = Color.Silver},
             New ToolStripMenuItem(".opus", Nothing, Sub(s1, e1) Form1.UiTextBox输出容器.Text = s1.Text) With {.ForeColor = Color.Silver},
             New ToolStripMenuItem(".wma", Nothing, Sub(s1, e1) Form1.UiTextBox输出容器.Text = s1.Text) With {.ForeColor = Color.Silver},
             New ToolStripMenuItem(".amr", Nothing, Sub(s1, e1) Form1.UiTextBox输出容器.Text = s1.Text) With {.ForeColor = Color.Silver},
             New ToolStripSeparator() With {.Tag = "null"}})

        Dim d As New 暗黑上下文菜单 With {.ShowImageMargin = False, .Font = Form1.Font}
        d.Items.AddRange(New ToolStripItem() {
             New ToolStripSeparator() With {.Tag = "null"},
             New ToolStripMenuItem(".png", Nothing, Sub(s1, e1) Form1.UiTextBox输出容器.Text = s1.Text) With {.ForeColor = Color.Silver},
             New ToolStripMenuItem(".jpg", Nothing, Sub(s1, e1) Form1.UiTextBox输出容器.Text = s1.Text) With {.ForeColor = Color.Silver},
             New ToolStripMenuItem(".jpeg", Nothing, Sub(s1, e1) Form1.UiTextBox输出容器.Text = s1.Text) With {.ForeColor = Color.Silver},
             New ToolStripMenuItem(".webp", Nothing, Sub(s1, e1) Form1.UiTextBox输出容器.Text = s1.Text) With {.ForeColor = Color.Silver},
             New ToolStripMenuItem(".avif", Nothing, Sub(s1, e1) Form1.UiTextBox输出容器.Text = s1.Text) With {.ForeColor = Color.Silver},
             New ToolStripMenuItem(".bmp", Nothing, Sub(s1, e1) Form1.UiTextBox输出容器.Text = s1.Text) With {.ForeColor = Color.Silver},
             New ToolStripMenuItem(".gif", Nothing, Sub(s1, e1) Form1.UiTextBox输出容器.Text = s1.Text) With {.ForeColor = Color.Silver},
             New ToolStripMenuItem(".ico", Nothing, Sub(s1, e1) Form1.UiTextBox输出容器.Text = s1.Text) With {.ForeColor = Color.Silver},
             New ToolStripSeparator() With {.Tag = "null"}})

        Dim a As New 暗黑上下文菜单 With {.ShowImageMargin = False, .Font = Form1.Font}
        a.Items.AddRange(New ToolStripItem() {
             New ToolStripSeparator() With {.Tag = "null"},
             New ToolStripMenuItem("常用容器") With {.ForeColor = Color.CornflowerBlue, .Enabled = False},
             New ToolStripMenuItem(".mp4", Nothing, Sub(s1, e1) Form1.UiTextBox输出容器.Text = s1.Text) With {.ForeColor = Color.Silver},
             New ToolStripMenuItem(".mkv", Nothing, Sub(s1, e1) Form1.UiTextBox输出容器.Text = s1.Text) With {.ForeColor = Color.Silver},
             New ToolStripMenuItem("全部分类") With {.ForeColor = Color.CornflowerBlue, .Enabled = False},
             New ToolStripMenuItem("视频") With {.ForeColor = Color.Silver, .DropDown = b},
             New ToolStripMenuItem("音频") With {.ForeColor = Color.Silver, .DropDown = c},
             New ToolStripMenuItem("图片") With {.ForeColor = Color.Silver, .DropDown = d},
             New ToolStripSeparator() With {.Tag = "null"}})

        a.Show(sender, New Point(0, sender.Height + sender.Parent.Padding.Bottom * 2))

    End Sub

    Public Shared Sub 校准输出面板的宽度()
        If Form1.Panel输出面板.Visible Then
            Form1.Panel输出面板.Width = Form1.Panel输出面板.Parent.Width * 0.5
        End If
    End Sub



End Class
