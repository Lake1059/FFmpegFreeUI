Imports System.IO
Imports Sunny.UI

Public Class Form1
    Public Shared Property DPI As Single = Form1.CreateGraphics.DpiX / 96

    Public 是否初始化 As Boolean = False
    Private 上一次窗口状态 As FormWindowState

    Public 常规流程参数页面 As New 界面_常规流程参数
    Public 混流页面 As New 界面_混流
    Public 合并页面 As New 界面_合并
    Public 编码队列右键菜单 As 暗黑上下文菜单

    Public 性能统计对象 As New 性能统计
    Public 性能统计刷新计时器 As New Timer With {.Interval = 2000, .Enabled = False}

    Public 选中项刷新信息计时器 As New Timer With {.Interval = 1000, .Enabled = False}
    Public 任务进度更新计时器 As New Timer With {.Interval = 1000, .Enabled = False}

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim 版本号 = String.Join(".", Application.ProductVersion.Split("."c).Take(3)).Split("+"c)(0)
        Me.Text = $"FFmpegFreeUI {版本号}"
        Label主标题.Text = $"FFmpegFreeUI 正式版 v{版本号}"

        ' 加载完成音效
        If FileIO.FileSystem.FileExists(Path.Combine(Application.StartupPath, "Sound_Finish.wav")) Then
            Try
                Using fileStream As New FileStream(Path.Combine(Application.StartupPath, "Sound_Finish.wav"), FileMode.Open, FileAccess.Read)
                    Dim soundStream As New MemoryStream()
                    fileStream.CopyTo(soundStream)
                    soundStream.Position = 0
                    Sound_Finish = soundStream
                End Using
            Catch ex As Exception
                ' 如果加载失败，保持使用默认的资源文件
                Sound_Finish = My.Resources.Resource1.完成
            End Try
        End If

        ' 加载错误音效
        If FileIO.FileSystem.FileExists(Path.Combine(Application.StartupPath, "Sound_Error.wav")) Then
            Try
                Using fileStream As New FileStream(Path.Combine(Application.StartupPath, "Sound_Error.wav"), FileMode.Open, FileAccess.Read)
                    Dim soundStream As New MemoryStream()
                    fileStream.CopyTo(soundStream)
                    soundStream.Position = 0
                    Sound_Error = soundStream
                End Using
            Catch ex As Exception
                ' 如果加载失败，保持使用默认的资源文件
                Sound_Error = My.Resources.Resource1.错误
            End Try
        End If


        视频编码器数据库.初始化()
        界面控制.初始化()
        用户设置.启动时加载设置()

        UiComboBox字体名称.Text = 用户设置.实例对象.字体
        If UiComboBox字体名称.Items.Contains("微软雅黑") Then UiComboBox字体名称.Font = New Font("微软雅黑", UiComboBox字体名称.Font.Size)
        SetControlFont(用户设置.实例对象.字体, Me, {UiComboBox字体名称}, True)
        Me.ListView1.ContextMenuStrip.Font = New Font(Me.UiComboBox字体名称.Text, Me.ListView1.ContextMenuStrip.Font.Size)
        界面控制.界面校准()
        If DPI <> 1 Then DPI变动时校准界面()

        If FileIO.FileSystem.FileExists(Path.Combine(Application.StartupPath, "icon.png")) Then
            Me.PictureBox1.Image = LoadImageFromFile(Path.Combine(Application.StartupPath, "icon.png")).GetThumbnailImage(Me.PictureBox1.Width, Me.PictureBox1.Height, Nothing, Nothing)
            Using bitmap As New Bitmap(Me.PictureBox1.Image)
                Me.Icon = Icon.FromHandle(bitmap.GetHicon())
            End Using
        Else
            Me.PictureBox1.Width = Me.PictureBox1.Height
            Me.PictureBox1.Image = My.Resources.Resource1.AppIcon.GetThumbnailImage(Me.PictureBox1.Width, Me.PictureBox1.Height, Nothing, Nothing)
        End If
        本地教学.初始化()
        上一次窗口状态 = Me.WindowState
    End Sub

    Private Sub Form1_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        界面线程执行(AddressOf 插件管理.启动时加载插件)
        If UI同步上下文 Is Nothing Then MsgBox("警告：UI 同步上下文是空的，继续使用软件将导致崩溃，请联系开发者排查问题", MsgBoxStyle.Critical)
        界面线程执行(AddressOf 检查更新.检查)
        任务进度更新计时器.Enabled = True

        If 用户设置.实例对象.TipsTriggeringTeachingContentAtStartup Then
            T99 = New Timer With {.Enabled = False, .Interval = 3000}
            AddHandler T99.Tick, AddressOf 显示教学信息
            T99.Start()
        End If
    End Sub
    Public T99 As Timer
    Sub 显示教学信息()
        Dim 选项字典 As New Dictionary(Of String, Action)
        选项字典("了解") = Nothing
        选项字典("永久关闭这个提示") = Sub() 用户设置.实例对象.TipsTriggeringTeachingContentAtStartup = False
        软件内对话框.显示对话框("基本提示信息可用", $"现在很多功能都有了内置的新手教学，触发方式如下：{vbCrLf & vbCrLf}对于文本框，按下 F1 或者动一下鼠标滚轮来触发{vbCrLf}对于下拉框，双击或者动一下鼠标滚轮来触发{vbCrLf & vbCrLf}如不想在启动时看到这个提示，请手动编辑设置文件，将 TipsTriggeringTeachingContentAtStartup 设置为 false 即可", 选项字典, 软件内对话框.主题类型.常规)
        T99.Enabled = False
        T99.Dispose()
        T99 = Nothing
    End Sub


    Private Sub Form1_DpiChanged(sender As Object, e As DpiChangedEventArgs) Handles Me.DpiChanged
        DPI = e.DeviceDpiNew / 96
        DPI变动时校准界面()
    End Sub

    Public Sub 重新创建句柄()
        Try
            If Not Me.IsHandleCreated Then Me.CreateHandle()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Form1_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        If 是否初始化 = False Then Exit Sub
        If Me.WindowState = FormWindowState.Minimized Then Exit Sub
        If Me.WindowState <> 上一次窗口状态 Then
            界面控制.界面校准()
            上一次窗口状态 = Me.WindowState
        Else
            界面控制.界面校准()
        End If
    End Sub

    Private Sub Form1_Move(sender As Object, e As EventArgs) Handles Me.Move
        If 是否初始化 = False Then Exit Sub
        界面控制.界面校准()
    End Sub

    Sub DPI变动时校准界面()
        Me.MinimumSize = New Size(0, 0)
        Me.Size = New Size(1300 * DPI, 700 * DPI)
        Me.UiTabControlMenu1.ItemSize = New Size(150 * DPI, 40 * DPI)
        Me.ImageList1.ImageSize = New Size(1, 30 * DPI)
        常规流程参数页面.UiTabControl1.ItemSize = New Size(120 * Form1.DPI, 50 * Form1.DPI)
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If 编码任务.队列.Any(Function(task) task.状态 = 编码任务.编码状态.正在处理) Then
            Dim result = MsgBox("有任务正在处理，是否强制关闭所有进程？", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo)
            If result = MsgBoxResult.No Then
                e.Cancel = True
                Exit Sub
            End If
        End If
        For Each task In 编码任务.队列
            task.清除占用()
        Next
        恢复系统状态()
        用户设置.退出时保存设置()
        e.Cancel = False
    End Sub

    Private Sub ListView1_DragEnter(sender As Object, e As DragEventArgs) Handles ListView1.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        End If
    End Sub
    Private Sub ListView1_DragDrop(sender As Object, e As DragEventArgs) Handles ListView1.DragDrop
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            界面控制_添加文件.加入编码队列(e.Data.GetData(DataFormats.FileDrop))
        End If
    End Sub
    Private Sub ListView1_KeyDown(sender As Object, e As KeyEventArgs) Handles ListView1.KeyDown
        Select Case e.KeyCode
            Case Keys.A : If e.Control Then 界面控制_编码队列.全选任务()
            Case Keys.Delete : 界面控制_编码队列.移除任务()
        End Select
    End Sub
    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        If Me.ListView1.SelectedItems.Count = 1 Then
            选中项刷新信息计时器.Enabled = True
            Panel41.Visible = True
            编码任务.选中项刷新信息()
        Else
            选中项刷新信息计时器.Enabled = False
            Panel41.Visible = False
        End If
    End Sub

    Private Sub UiButton打开文件显示参数_Click(sender As Object, e As EventArgs) Handles UiButton打开文件显示参数.Click
        Dim openFileDialog As New OpenFileDialog With {.Multiselect = False, .Filter = "所有文件|*.*"}
        If openFileDialog.ShowDialog = DialogResult.OK Then
            显示媒体信息流程(openFileDialog.FileName)
        End If
    End Sub
    Sub 显示媒体信息流程(文件路径 As String)
        Me.RichTextBox1.Text = ""
        Dim FFprobeProcess As New Process
        FFprobeProcess = New Process()
        FFprobeProcess.StartInfo.FileName = "ffprobe"
        FFprobeProcess.StartInfo.WorkingDirectory = If(用户设置.实例对象.工作目录 <> "", 用户设置.实例对象.工作目录, "")
        FFprobeProcess.StartInfo.Arguments = $"-hide_banner ""{文件路径}"""
        FFprobeProcess.StartInfo.RedirectStandardOutput = True
        FFprobeProcess.StartInfo.RedirectStandardError = True
        FFprobeProcess.StartInfo.StandardOutputEncoding = System.Text.Encoding.UTF8
        FFprobeProcess.StartInfo.StandardErrorEncoding = System.Text.Encoding.UTF8
        FFprobeProcess.StartInfo.CreateNoWindow = True
        FFprobeProcess.EnableRaisingEvents = True
        AddHandler FFprobeProcess.OutputDataReceived, AddressOf 显示媒体信息输出事件
        AddHandler FFprobeProcess.ErrorDataReceived, AddressOf 显示媒体信息输出事件
        FFprobeProcess.Start()
        FFprobeProcess.BeginOutputReadLine()
        FFprobeProcess.BeginErrorReadLine()
    End Sub
    Sub 显示媒体信息输出事件(sender As Object, e As DataReceivedEventArgs)
        If e.Data Is Nothing Then Exit Sub
        Try
            界面线程执行(Sub() Me.RichTextBox1.AppendText(e.Data & vbCrLf))
        Catch ex As Exception
        End Try
    End Sub
    Private Sub UiButton打开文件显示参数_DragDrop(sender As Object, e As DragEventArgs) Handles UiButton打开文件显示参数.DragDrop
        Dim files() As String = e.Data.GetData(DataFormats.FileDrop)
        If files.Length > 0 Then
            显示媒体信息流程(files(0))
        End If
    End Sub
    Private Sub UiButton打开文件显示参数_DragEnter(sender As Object, e As DragEventArgs) Handles UiButton打开文件显示参数.DragEnter
        If e.Data.GetData(DataFormats.FileDrop) IsNot Nothing Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub
    Private Sub UiTextBox快捷输入CPU核心_KeyPress(sender As Object, e As KeyPressEventArgs) Handles UiTextBox快捷输入CPU核心.KeyPress
        Select Case e.KeyChar
            Case "0"c To "9"c, "~"c, ChrW(Keys.Back)
            Case ChrW(Keys.Enter)
                Dim input = UiTextBox快捷输入CPU核心.Text.Trim
                Dim result As New List(Of Integer)
                Try
                    If input.Contains("~"c) Then
                        Dim parts = input.Split("~"c)
                        If parts.Length = 2 Then
                            Dim startNum, endNum As Integer
                            If Integer.TryParse(parts(0), startNum) AndAlso Integer.TryParse(parts(1), endNum) Then
                                If startNum <= endNum Then
                                    For i = startNum To endNum
                                        result.Add(i)
                                    Next
                                    UiTextBox处理器核心.Text = String.Join(",", result)
                                End If
                            End If
                        End If
                    End If
                Catch ex As Exception
                    MsgBox(ex.Message, MsgBoxStyle.Critical)
                End Try
                e.Handled = True
            Case Else
                Exit Sub
        End Select
    End Sub

    Private Sub UiButton切换处理器占用面板_Click(sender As Object, e As EventArgs) Handles UiButton切换处理器占用面板.Click
        If Panel18.Visible Then
            Panel18.Visible = False
            Panel19.Visible = False
        Else
            Panel18.Visible = True
            Panel19.Visible = True
        End If
        界面控制.界面校准()
    End Sub

    Private Sub UiComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles UiComboBox3.SelectedIndexChanged
        For Each C As Control In Panel24.Controls
            Panel24.Controls.Remove(C)
        Next
        If Me.UiComboBox3.Text = "" Then Exit Sub
        If Me.UiComboBox3.SelectedIndex < 0 Then Exit Sub
        Panel24.Controls.Add(插件管理.由插件加载的自定义界面(Me.UiComboBox3.Text))
        插件管理.由插件加载的自定义界面(Me.UiComboBox3.Text).Dock = DockStyle.Fill
        SetControlFont(用户设置.实例对象.字体, Panel24)
    End Sub
End Class
