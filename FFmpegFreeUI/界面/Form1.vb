Imports System.IO
Imports Sunny.UI

Public Class Form1
    Public Shared Property DPI As Single = Form1.CreateGraphics.DpiX / 96

    Public 是否初始化 As Boolean = False
    Private 上一次窗口状态 As FormWindowState

    Public 起始页面 As New 界面_起始页 With {.Dock = DockStyle.Fill}
    Public 准备文件页面 As New 界面_准备文件 With {.Dock = DockStyle.Fill}
    Public 常规流程参数页面 As New 界面_常规流程参数_V2 With {.Dock = DockStyle.Fill}
    Public 混流页面 As New 界面_混流 With {.Dock = DockStyle.Fill}
    Public 合并页面 As New 界面_合并 With {.Dock = DockStyle.Fill}
    Public 设置页面 As New 界面_设置 With {.Dock = DockStyle.Fill}
    Public 性能监控页面 As New 界面_性能监控 With {.Dock = DockStyle.Fill}

    Public 性能统计对象 As New 性能统计
    Public 性能统计刷新计时器 As New Timer With {.Interval = 2000, .Enabled = False}

    Public 选中项刷新信息计时器 As New Timer With {.Interval = 1000, .Enabled = False}
    Public 任务进度更新计时器 As New Timer With {.Interval = 1000, .Enabled = False}

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        启用Win32API深色模式(Me.Handle)

        If Not FileIO.FileSystem.DirectoryExists(Path.Combine(Application.StartupPath, "Preset")) Then
            FileIO.FileSystem.CreateDirectory(Path.Combine(Application.StartupPath, "Preset"))
        End If
        'If Not FileIO.FileSystem.DirectoryExists(Path.Combine(Application.StartupPath, "Plugin")) Then
        '    FileIO.FileSystem.CreateDirectory(Path.Combine(Application.StartupPath, "Plugin"))
        'End If

        Dim 版本号 = String.Join(".", Application.ProductVersion.Split("."c).Take(3)).Split("+"c)(0)
        Me.Text = $"FFmpegFreeUI {版本号}"
        起始页面.Label主标题.Text = $"FFmpegFreeUI 内部开发版本 {版本号}"

        视频编码器数据库.初始化()
        界面控制.初始化()
        用户设置.启动时加载设置()
        插件管理.启动时读取个性化功能解锁器()
        编码队列右键菜单.重设字体()
        编码队列管理选项.重设字体()
        界面控制.界面校准()
        编码任务.初始化()
        If DPI <> 1 Then DPI变动时校准界面()

        上一次窗口状态 = Me.WindowState
    End Sub

    Private Sub Form1_DpiChanged(sender As Object, e As DpiChangedEventArgs) Handles Me.DpiChanged
        DPI = e.DeviceDpiNew / 96
        DPI变动时校准界面()
    End Sub

    Private Sub Form1_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Me.Panel顶部视觉修正区域_一级选项卡.Width = Me.UiTabControlMenu1.ItemSize.Width + 1
        'bro薛定谔的猫知道吧，你不搞个监控这玩意就是随机态
        If UI同步上下文 Is Nothing Then MsgBox("警告：UI 同步上下文是空的，继续使用软件将导致崩溃，请联系开发者排查问题", MsgBoxStyle.Critical)

        任务进度更新计时器.Enabled = True
        界面线程执行(AddressOf 插件管理.启动时加载插件)
        界面线程执行(AddressOf 检查更新.检查)
        回收自身内存占用()

        新闻列表.获取新闻()
        用户统计.回报活跃()
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

    Sub DPI变动时校准界面()
        Me.MinimumSize = New Size(0, 0)
        Me.Size = New Size(1200 * DPI, 700 * DPI)
        Me.MinimumSize = New Size(1200 * DPI, 700 * DPI)
        Me.UiTabControlMenu1.ItemSize = New Size(150 * DPI, 38 * DPI)
        Me.ImageList1.ImageSize = New Size(1, 30 * DPI)
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
        Dim files() As String = e.Data.GetData(DataFormats.FileDrop)
        Select Case e.KeyState
            Case 4, 8, 32
                If files.Length > 0 Then
                    Dim a As New Form独立参数面板 With {.文件列表 = files}
                    a.Label1.Text = $"为 {files.Length} 个文件使用单独的参数方案{vbCrLf}{files(0)}"
                    If 用户设置.实例对象.打开独立参数面板时自动切到预设管理页面 = 1 Then
                        a.参数面板.UiTabControlMenu1.SelectedTab = a.参数面板.TabPage方案管理
                    End If
                    显示窗体(a, Me)
                End If
            Case Else
                If files.Length > 0 Then
                    界面控制_添加文件.加入编码队列(files, 常规流程参数页面)
                End If
        End Select
    End Sub
    Private Sub ListView1_KeyDown(sender As Object, e As KeyEventArgs) Handles ListView1.KeyDown
        Select Case e.KeyCode
            Case Keys.Enter : 界面控制_编码队列.开始任务()
            Case Keys.Space : 界面控制_编码队列.暂停任务()
            Case Keys.A
                If e.Control Then 界面控制_编码队列.全选任务()
                If e.Alt Then 界面控制_编码队列.反选任务()
            Case Keys.Delete : 界面控制_编码队列.移除任务()
            Case Keys.Escape : 界面控制_编码队列.重置任务()
            Case Keys.End : 界面控制_编码队列.停止任务()
            Case Keys.F3 : 界面控制_编码队列.上移任务()
            Case Keys.F4 : 界面控制_编码队列.下移任务()
        End Select
    End Sub
    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        If Me.ListView1.SelectedItems.Count = 1 Then
            Panel41.Visible = True
            If 是否打开了输出面板 Then Panel输出面板.Visible = True
            界面控制.校准输出面板的宽度()
            编码任务.选中项刷新信息()
            选中项刷新信息计时器.Enabled = True
        Else
            选中项刷新信息计时器.Enabled = False
            Panel41.Visible = False
            Panel输出面板.Visible = False
        End If
    End Sub

    Public 是否打开了输出面板 As Boolean = False

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

    Public Shared Sub 加载自定义任务完成音效(file As String)
        If FileIO.FileSystem.FileExists(file) Then
            Try
                Using fileStream As New FileStream(file, FileMode.Open, FileAccess.Read)
                    Dim soundStream As New MemoryStream()
                    fileStream.CopyTo(soundStream)
                    soundStream.Position = 0
                    Sound_Finish = soundStream
                End Using
            Catch ex As Exception
                Sound_Finish = My.Resources.Resource1.完成
                MsgBox(ex.Message, MsgBoxStyle.Critical)
            End Try
        End If
    End Sub
    Public Shared Sub 加载自定义任务失败音效(file As String)
        If FileIO.FileSystem.FileExists(file) Then
            Try
                Using fileStream As New FileStream(file, FileMode.Open, FileAccess.Read)
                    Dim soundStream As New MemoryStream()
                    fileStream.CopyTo(soundStream)
                    soundStream.Position = 0
                    Sound_Error = soundStream
                End Using
            Catch ex As Exception
                Sound_Error = My.Resources.Resource1.错误
                MsgBox(ex.Message, MsgBoxStyle.Critical)
            End Try
        End If
    End Sub
    Public Sub 加载自定义图标(img As String)
        Try
            起始页面.PictureBox1.Width = 起始页面.PictureBox1.Height
            If FileIO.FileSystem.FileExists(img) AndAlso img IsNot Nothing AndAlso img <> "" Then
                起始页面.PictureBox1.Image = LoadImageFromFile(img).GetThumbnailImage(起始页面.PictureBox1.Width, 起始页面.PictureBox1.Height, Nothing, Nothing)
                Using bitmap As New Bitmap(起始页面.PictureBox1.Image)
                    Me.Icon = Icon.FromHandle(bitmap.GetHicon())
                End Using
            Else
                起始页面.PictureBox1.Image = My.Resources.Resource1.AppIcon.GetThumbnailImage(起始页面.PictureBox1.Width, 起始页面.PictureBox1.Height, Nothing, Nothing)
            End If
        Catch ex As Exception
        End Try
    End Sub

End Class
