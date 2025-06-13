Imports System.ComponentModel
Public Class Form1
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Shared Property DPI As Single = Form1.CreateGraphics.DpiX / 96

    Public 是否初始化 As Boolean = False
    Private 上一次窗口状态 As FormWindowState


    Public 常规流程参数页面 As New 界面_常规流程参数


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        界面控制.初始化()
        视频编码器数据库.初始化()
        上一次窗口状态 = Me.WindowState
    End Sub

    Private Async Sub Form1_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        界面控制.界面校准()
        重新创建句柄()
        If DPI <> 1 Then DPI变动时校准界面()

        Label64.Text = "正在检查更新版本 ..."
        Label65.Text = ""
        Label73.Text = ""
        Label75.Text = ""
        Label122.Text = ""
        '检测是否有网络
        If My.Computer.Network.IsAvailable Then
            Dim a As New GitHubAPI.Release
            Dim s1 As String = Await a.获取仓库发布版信息Async("Lake1059/FFmpegFreeUI", "")
            If s1 <> "" Then
                Label64.Text = "获取更新信息失败"
                Label65.Text = ""
                Label73.Text = ""
                Label75.Text = ""
                Label122.Text = ""
            Else
                Label64.Text = "发布标题：" & a.发布标题
                Label65.Text = "最新标签：" & a.版本标签
                Label73.Text = "发布用户：" & a.发布者用户名
                Label75.Text = "文件数量：" & a.可供下载的文件.Count
                Label122.Text = "发布时间：" & a.发布时间
            End If
        Else
            Label64.Text = "无网络连接！联网后重启应用程序以重试"
            Label65.Text = ""
            Label73.Text = ""
            Label75.Text = ""
            Label122.Text = ""
        End If
    End Sub

    Private Sub Form1_DpiChanged(sender As Object, e As DpiChangedEventArgs) Handles Me.DpiChanged
        DPI = e.DeviceDpiNew / 96
        DPI变动时校准界面()
    End Sub

    Public Sub 重新创建句柄()
        If Not Me.IsHandleCreated Then Me.CreateHandle()
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
        Me.ClientSize = New Size(1300 * DPI, 800 * DPI)
        Me.MinimumSize = Me.Size
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
        SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS)
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        If Me.ListView1.SelectedItems.Count = 1 Then
            Timer1.Enabled = True
            Panel41.Visible = True
            刷新调试信息()
        Else
            Timer1.Enabled = False
            Panel41.Visible = False
        End If
    End Sub

    Sub 刷新调试信息()
        Try
            Label74.Text = 编码任务.队列(Me.ListView1.SelectedItems(0).Index).实时输出
            Label76.Text = String.Join(vbCrLf, 编码任务.队列(Me.ListView1.SelectedItems(0).Index).错误列表)
            If Label76.Text = "" Then
                Panel47.Visible = False
                Label120.Visible = False
            Else
                Panel47.Visible = True
                Label120.Visible = True
                Dim s1 = 根据标签宽度计算显示高度(Label76)
                Label76.Height = s1
                If s1 > TabPage编码队列.Width * 0.3 Then
                    Panel47.Height = TabPage编码队列.Width * 0.3
                Else
                    Panel47.Height = s1
                End If
            End If
        Catch ex As Exception
            编码任务.队列(Me.ListView1.SelectedItems(0).Index).错误列表.Add($"刷新界面失败 {Now}")
        End Try
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        刷新调试信息()
    End Sub



    Private Sub UiButton打开文件显示参数_Click(sender As Object, e As EventArgs) Handles UiButton打开文件显示参数.Click
        Dim openFileDialog As New OpenFileDialog With {.Multiselect = False, .Filter = "所有文件|*.*"}
        If openFileDialog.ShowDialog() = DialogResult.OK Then
            显示媒体信息流程(openFileDialog.FileName)
        End If
    End Sub
    Sub 显示媒体信息流程(文件路径 As String)
        Me.RichTextBox1.Text = ""
        Dim FFprobeProcess As New Process
        FFprobeProcess = New Process()
        FFprobeProcess.StartInfo.FileName = "ffprobe.exe"
        FFprobeProcess.StartInfo.WorkingDirectory = Application.StartupPath
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
            Me.重新创建句柄()
            Me.Invoke(Sub() Me.RichTextBox1.AppendText(e.Data & vbCrLf))
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
                Dim input As String = UiTextBox快捷输入CPU核心.Text.Trim()
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
                                    Me.UiTextBox处理器核心.Text = String.Join(",", result)
                                End If
                            End If
                        End If
                    End If
                Catch ex As Exception
                    MsgBox("输入处理异常: " & ex.Message, MsgBoxStyle.Critical)
                End Try
                e.Handled = True
            Case Else
                Exit Sub
        End Select
    End Sub
End Class
