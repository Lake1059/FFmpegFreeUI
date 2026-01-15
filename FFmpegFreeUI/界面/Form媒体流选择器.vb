Imports System.Text.RegularExpressions
Imports Sunny.UI

Public Class Form媒体流选择器
    Public Sub New(Optional 要读取的媒体文件 As String = "",
                    Optional 视频流文本目标对象 As Object = Nothing,
                    Optional 音频流文本目标对象 As Object = Nothing,
                    Optional 字幕流文本目标对象 As Object = Nothing,
                    Optional 文件索引 As String = "",
                    Optional 视频流已选 As String = "",
                    Optional 音频流已选 As String = "",
                    Optional 字幕流已选 As String = ""
                    )
        InitializeComponent()
        Me.设置_要读取的媒体文件 = 要读取的媒体文件
        Me.设置_视频流文本目标对象 = 视频流文本目标对象
        Me.设置_音频流文本目标对象 = 音频流文本目标对象
        Me.设置_字幕流文本目标对象 = 字幕流文本目标对象
        Me.设置_文件索引 = 文件索引

        Dim 末尾数字正则 As New Regex("\d+$", RegexOptions.Compiled)

        Dim vlist As New List(Of String)(视频流已选.ToLower.Trim.Replace($"{文件索引}:v:", "").Split(","))
        Dim alist As New List(Of String)(音频流已选.ToLower.Trim.Replace($"{文件索引}:a:", "").Split(","))
        Dim slist As New List(Of String)(字幕流已选.ToLower.Trim.Replace($"{文件索引}:s:", "").Split(","))

        If vlist.Contains("0:v") Then
            Me.设置_已经选择全部视频流 = True
        Else
            For Each i In vlist
                If String.IsNullOrWhiteSpace(i) Then Continue For
                Dim m = 末尾数字正则.Match(i)
                If Not m.Success Then Continue For
                Dim n As Integer
                If Integer.TryParse(m.Value, n) Then Me.设置_视频流已选.Add(n)
            Next
        End If
        If alist.Contains("0:a") Then
            Me.设置_已经选择全部音频流 = True
        Else
            For Each i In alist
                If String.IsNullOrWhiteSpace(i) Then Continue For
                Dim m = 末尾数字正则.Match(i)
                If Not m.Success Then Continue For
                Dim n As Integer
                If Integer.TryParse(m.Value, n) Then Me.设置_音频流已选.Add(n)
            Next
        End If
        If slist.Contains("0:s") Then
            Me.设置_已经选择全部字幕流 = True
        Else
            For Each i In slist
                If String.IsNullOrWhiteSpace(i) Then Continue For
                Dim m = 末尾数字正则.Match(i)
                If Not m.Success Then Continue For
                Dim n As Integer
                If Integer.TryParse(m.Value, n) Then Me.设置_字幕流已选.Add(n)
            Next
        End If
    End Sub
    Enum 使用场景
        常规流程参数页面 = 1
        简易混流页面 = 2
    End Enum

    Private 设置_要读取的媒体文件 As String = ""
    Private 设置_视频流文本目标对象 As Object
    Private 设置_音频流文本目标对象 As Object
    Private 设置_字幕流文本目标对象 As Object
    Private 设置_文件索引 As String = ""
    Private 设置_视频流已选 As New List(Of Integer)
    Private 设置_音频流已选 As New List(Of Integer)
    Private 设置_字幕流已选 As New List(Of Integer)

    Private 设置_已经选择全部视频流 As Boolean = False
    Private 设置_已经选择全部音频流 As Boolean = False
    Private 设置_已经选择全部字幕流 As Boolean = False

    Private 视频流复选框对象列表 As New List(Of UICheckBox)
    Private 音频流复选框对象列表 As New List(Of UICheckBox)
    Private 字幕流复选框对象列表 As New List(Of UICheckBox)

    Sub 界面重置()
        Me.Panel2.Controls.Clear()
        视频流复选框对象列表.Clear()
        音频流复选框对象列表.Clear()
        字幕流复选框对象列表.Clear()
    End Sub

    Private Sub Form媒体流选择器_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetControlFont(用户设置.实例对象.字体, Me,, True)
    End Sub
    Private Sub Form媒体流选择器_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        If FileIO.FileSystem.FileExists(设置_要读取的媒体文件) Then 开始读取(设置_要读取的媒体文件)
    End Sub
    Private Sub Form媒体流选择器_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing

    End Sub

    Sub 开始读取(媒体文件 As String)
        Dim 输出内容 As New List(Of String)
        Dim FFprobeProcess As New Process
        FFprobeProcess.StartInfo.FileName = "ffprobe"
        FFprobeProcess.StartInfo.WorkingDirectory = If(用户设置.实例对象.工作目录 <> "", 用户设置.实例对象.工作目录, "")
        FFprobeProcess.StartInfo.Arguments = $"-hide_banner ""{媒体文件}"""
        FFprobeProcess.StartInfo.RedirectStandardOutput = True
        FFprobeProcess.StartInfo.RedirectStandardError = True
        FFprobeProcess.StartInfo.StandardOutputEncoding = System.Text.Encoding.UTF8
        FFprobeProcess.StartInfo.StandardErrorEncoding = System.Text.Encoding.UTF8
        FFprobeProcess.StartInfo.CreateNoWindow = True
        FFprobeProcess.EnableRaisingEvents = True
        AddHandler FFprobeProcess.OutputDataReceived, Sub(sender, e)
                                                          If e.Data Is Nothing Then Exit Sub
                                                          输出内容.Add(e.Data)
                                                      End Sub
        AddHandler FFprobeProcess.ErrorDataReceived, Sub(sender, e)
                                                         If e.Data Is Nothing Then Exit Sub
                                                         输出内容.Add(e.Data)
                                                     End Sub
        FFprobeProcess.Start()
        FFprobeProcess.BeginOutputReadLine()
        FFprobeProcess.BeginErrorReadLine()

        FFprobeProcess.WaitForExit()
        FFprobeProcess.Dispose()
        界面重置()

        If 输出内容.Count = 0 Then
            Me.Panel2.Controls.Add(New Label With {.Text = "ffprobe 未输出任何信息", .AutoSize = True, .Dock = DockStyle.Top, .ForeColor = Color.IndianRed})
            Exit Sub
        End If
        Dim 媒体信息 As 媒体信息解析器.媒体信息 = 媒体信息解析器.解析(输出内容)

        For i = 0 To 媒体信息.视频流.Count - 1
            Dim 复选框 As New UICheckBox With {
                    .Text = $"视频 [{i}]",
                    .BackColor = Panel2.BackColor,
                    .ForeColor = Panel2.ForeColor,
                    .Font = New Font(用户设置.实例对象.字体, 10),
                    .CheckBoxSize = 20 * Form1.DPI,
                    .Dock = DockStyle.Top,
                    .Height = 30 * Form1.DPI,
                    .AutoSize = False,
                    .Tag = i
                }
            If 媒体信息.视频流(i).流语言 <> "" Then 复选框.Text &= $" [{媒体信息.视频流(i).流语言}]"
            复选框.Text &= $" {媒体信息.视频流(i).编码器}"
            If 媒体信息.视频流(i).分辨率 <> "" Then 复选框.Text &= $" | {媒体信息.视频流(i).分辨率}"
            If 媒体信息.视频流(i).帧率 <> "" Then 复选框.Text &= $" | {媒体信息.视频流(i).帧率}"
            If 媒体信息.视频流(i).比特率 <> "" Then 复选框.Text &= $" | {媒体信息.视频流(i).比特率}"
            If 设置_视频流已选.Contains(i) Then 复选框.Checked = True
            If 设置_已经选择全部视频流 Then 复选框.Checked = True
            Me.Panel2.Controls.Add(复选框)
            复选框.BringToFront()
            视频流复选框对象列表.Add(复选框)

            If 媒体信息.视频流(i).像素格式 <> "" Then
                Dim L1 As New Label With {.Text = $"{媒体信息.视频流(i).像素格式}", .AutoSize = True, .Padding = New Padding(26 * Form1.DPI, 0, 0, 5 * Form1.DPI), .ForeColor = Color.CornflowerBlue, .Font = New Font(用户设置.实例对象.字体, 10), .Dock = DockStyle.Top}
                Me.Panel2.Controls.Add(L1)
                L1.BringToFront()
            End If
            If 媒体信息.视频流(i).编码器库 <> "" Then
                Dim L1 As New Label With {.Text = $"{媒体信息.视频流(i).编码器库}", .AutoSize = True, .Padding = New Padding(26 * Form1.DPI, 0, 0, 5 * Form1.DPI), .ForeColor = Color.DimGray, .Font = New Font(用户设置.实例对象.字体, 10), .Dock = DockStyle.Top}
                Me.Panel2.Controls.Add(L1)
                L1.BringToFront()
            End If
            If 媒体信息.视频流(i).元数据标题 <> "" Then
                Dim L1 As New Label With {.Text = $"{媒体信息.视频流(i).元数据标题}", .AutoSize = True, .Padding = New Padding(26 * Form1.DPI, 0, 0, 5 * Form1.DPI), .ForeColor = Color.DimGray, .Font = New Font(用户设置.实例对象.字体, 10), .Dock = DockStyle.Top}
                Me.Panel2.Controls.Add(L1)
                L1.BringToFront()
            End If
        Next

        For i = 0 To 媒体信息.音频流.Count - 1
            Dim 复选框 As New UICheckBox With {
                    .Text = $"音频 [{i}]",
                    .BackColor = Panel2.BackColor,
                    .ForeColor = Panel2.ForeColor,
                    .Font = New Font(用户设置.实例对象.字体, 10),
                    .CheckBoxSize = 20 * Form1.DPI,
                    .Dock = DockStyle.Top,
                    .Height = 30 * Form1.DPI,
                    .AutoSize = False,
                    .CheckBoxColor = Color.OliveDrab,
                    .Tag = i
                }
            If 媒体信息.音频流(i).流语言 <> "" Then 复选框.Text &= $" [{媒体信息.音频流(i).流语言}]"
            复选框.Text &= $" {媒体信息.音频流(i).编码器}"
            If 媒体信息.音频流(i).声道布局 <> "" Then 复选框.Text &= $" | {媒体信息.音频流(i).声道布局}"
            If 媒体信息.音频流(i).采样率 <> "" Then 复选框.Text &= $" | {媒体信息.音频流(i).采样率}"
            If 媒体信息.音频流(i).比特率 <> "" Then 复选框.Text &= $" | {媒体信息.音频流(i).比特率}"
            If 设置_音频流已选.Contains(i) Then 复选框.Checked = True
            If 设置_已经选择全部音频流 Then 复选框.Checked = True
            Me.Panel2.Controls.Add(复选框)
            复选框.BringToFront()
            音频流复选框对象列表.Add(复选框)
            If 媒体信息.音频流(i).编码器库 <> "" Then
                Dim L1 As New Label With {.Text = $"{媒体信息.音频流(i).编码器库}", .AutoSize = True, .Padding = New Padding(26 * Form1.DPI, 0, 0, 5 * Form1.DPI), .ForeColor = Color.DimGray, .Font = New Font(用户设置.实例对象.字体, 10), .Dock = DockStyle.Top}
                Me.Panel2.Controls.Add(L1)
                L1.BringToFront()
            End If
            If 媒体信息.音频流(i).元数据标题 <> "" Then
                Dim L1 As New Label With {.Text = $"{媒体信息.音频流(i).元数据标题}", .AutoSize = True, .Padding = New Padding(26 * Form1.DPI, 0, 0, 5 * Form1.DPI), .ForeColor = Color.DimGray, .Font = New Font(用户设置.实例对象.字体, 10), .Dock = DockStyle.Top}
                Me.Panel2.Controls.Add(L1)
                L1.BringToFront()
            End If
        Next

        For i = 0 To 媒体信息.字幕流.Count - 1
            Dim 复选框 As New UICheckBox With {
                    .Text = $"字幕 [{i}]",
                    .BackColor = Panel2.BackColor,
                    .ForeColor = Panel2.ForeColor,
                    .Font = New Font(用户设置.实例对象.字体, 10),
                    .CheckBoxSize = 20 * Form1.DPI,
                    .Dock = DockStyle.Top,
                    .Height = 30 * Form1.DPI,
                    .AutoSize = False,
                    .CheckBoxColor = Color.MediumPurple,
                    .Tag = i
                }
            If 媒体信息.字幕流(i).流语言 <> "" Then 复选框.Text &= $" [{媒体信息.字幕流(i).流语言}]"
            复选框.Text &= $" {媒体信息.字幕流(i).编码器}"
            If 设置_字幕流已选.Contains(i) Then 复选框.Checked = True
            If 设置_已经选择全部字幕流 Then 复选框.Checked = True
            Me.Panel2.Controls.Add(复选框)
            复选框.BringToFront()
            字幕流复选框对象列表.Add(复选框)
            If 媒体信息.字幕流(i).编码器库 <> "" Then
                Dim L1 As New Label With {.Text = $"{媒体信息.字幕流(i).编码器库}", .AutoSize = True, .Padding = New Padding(26 * Form1.DPI, 0, 0, 5 * Form1.DPI), .ForeColor = Color.DimGray, .Font = New Font(用户设置.实例对象.字体, 10), .Dock = DockStyle.Top}
                Me.Panel2.Controls.Add(L1)
                L1.BringToFront()
            End If
            If 媒体信息.字幕流(i).元数据标题 <> "" Then
                Dim L1 As New Label With {.Text = $"{媒体信息.字幕流(i).元数据标题}", .AutoSize = True, .Padding = New Padding(26 * Form1.DPI, 0, 0, 5 * Form1.DPI), .ForeColor = Color.DimGray, .Font = New Font(用户设置.实例对象.字体, 10), .Dock = DockStyle.Top}
                Me.Panel2.Controls.Add(L1)
                L1.BringToFront()
            End If
        Next
    End Sub

    Private Sub Panel2_DragEnter(sender As Object, e As DragEventArgs) Handles Panel2.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub
    Private Sub Panel2_DragDrop(sender As Object, e As DragEventArgs) Handles Panel2.DragDrop
        Dim files As String() = e.Data.GetData(DataFormats.FileDrop)
        If files.Length > 0 Then
            开始读取(files(0))
        End If
    End Sub

    Private Sub UiButton打开_Click(sender As Object, e As EventArgs) Handles UiButton打开.Click
        Dim a As New OpenFileDialog With {.Multiselect = False}
        If a.ShowDialog = DialogResult.OK Then
            开始读取(a.FileName)
        End If
    End Sub

    Private Sub UiButton重置_Click(sender As Object, e As EventArgs) Handles UiButton重置.Click
        For Each cb As UICheckBox In 视频流复选框对象列表
            cb.Checked = False
        Next
        For Each cb As UICheckBox In 音频流复选框对象列表
            cb.Checked = False
        Next
        For Each cb As UICheckBox In 字幕流复选框对象列表
            cb.Checked = False
        Next
    End Sub

    Private Sub UiButton确认_Click(sender As Object, e As EventArgs) Handles UiButton确认.Click
        Dim vlist As New List(Of String)
        For Each cb As UICheckBox In 视频流复选框对象列表
            If Not cb.Checked Then Continue For
            vlist.Add(If(设置_文件索引 <> "", $"{设置_文件索引}:v:", "") & cb.Tag)
        Next
        If 设置_视频流文本目标对象 IsNot Nothing Then 设置_视频流文本目标对象.Text = String.Join(",", vlist)
        Dim alist As New List(Of String)
        For Each cb As UICheckBox In 音频流复选框对象列表
            If Not cb.Checked Then Continue For
            alist.Add(If(设置_文件索引 <> "", $"{设置_文件索引}:a:", "") & cb.Tag)
        Next
        If 设置_音频流文本目标对象 IsNot Nothing Then 设置_音频流文本目标对象.Text = String.Join(",", alist)
        Dim slist As New List(Of String)
        For Each cb As UICheckBox In 字幕流复选框对象列表
            If Not cb.Checked Then Continue For
            slist.Add(If(设置_文件索引 <> "", $"{设置_文件索引}:s:", "") & cb.Tag)
        Next
        If 设置_字幕流文本目标对象 IsNot Nothing Then 设置_字幕流文本目标对象.Text = String.Join(",", slist)
        Me.Close()
    End Sub

End Class