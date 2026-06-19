Imports LakeUI

Public Class Form_v6_媒体流选择器
    Private ReadOnly 视频目标 As Object
    Private ReadOnly 音频目标 As Object
    Private ReadOnly 字幕目标 As Object
    Private ReadOnly 文件索引 As String
    Private ReadOnly 视频已选 As New HashSet(Of Integer)
    Private ReadOnly 音频已选 As New HashSet(Of Integer)
    Private ReadOnly 字幕已选 As New HashSet(Of Integer)
    Private 视频全选 As Boolean
    Private 音频全选 As Boolean
    Private 字幕全选 As Boolean

    Private ReadOnly 视频复选框 As New List(Of ModernCheckBox)
    Private ReadOnly 音频复选框 As New List(Of ModernCheckBox)
    Private ReadOnly 字幕复选框 As New List(Of ModernCheckBox)
    Private ReadOnly 初始媒体文件 As String

    Public Sub New(Optional 要读取的媒体文件 As String = "",
                   Optional 视频流文本目标对象 As Object = Nothing,
                   Optional 音频流文本目标对象 As Object = Nothing,
                   Optional 字幕流文本目标对象 As Object = Nothing,
                   Optional 文件索引 As String = "0",
                   Optional 视频流已选 As String = "",
                   Optional 音频流已选 As String = "",
                   Optional 字幕流已选 As String = "")
        InitializeComponent()
        视频目标 = 视频流文本目标对象
        音频目标 = 音频流文本目标对象
        字幕目标 = 字幕流文本目标对象
        Me.文件索引 = If(文件索引 = "", "0", 文件索引)
        初始媒体文件 = 要读取的媒体文件
        解析已选(视频流已选, "v", 视频已选, 视频全选)
        解析已选(音频流已选, "a", 音频已选, 音频全选)
        解析已选(字幕流已选, "s", 字幕已选, 字幕全选)
        绑定文件拖入(Me)
        绑定文件拖入(ModernPanel1)
        绑定文件拖入(列表面板)
        绑定文件拖入(顶部面板)
        绑定文件拖入(底部面板)
        绑定文件拖入(MB_全选视频)
        绑定文件拖入(MB_全选音频)
        绑定文件拖入(MB_全选字幕)
        绑定文件拖入(MB_打开文件)
        绑定文件拖入(MB_重置选择)
        绑定文件拖入(MB_确认选择)
        AddHandler Shown, Sub()
                              If 初始媒体文件 <> "" Then 开始读取(初始媒体文件)
                          End Sub
    End Sub

    Private Sub 绑定文件拖入(target As Control)
        target.AllowDrop = True
        RemoveHandler target.DragEnter, AddressOf 文件拖入事件
        RemoveHandler target.DragDrop, AddressOf 文件放下事件
        AddHandler target.DragEnter, AddressOf 文件拖入事件
        AddHandler target.DragDrop, AddressOf 文件放下事件
    End Sub

    Private Sub 文件拖入事件(sender As Object, e As DragEventArgs)
        e.Effect = If(e.Data IsNot Nothing AndAlso e.Data.GetDataPresent(DataFormats.FileDrop), DragDropEffects.Copy, DragDropEffects.None)
    End Sub

    Private Sub 文件放下事件(sender As Object, e As DragEventArgs)
        If e.Data Is Nothing Then Exit Sub
        Dim files = TryCast(e.Data.GetData(DataFormats.FileDrop), String())
        If files Is Nothing OrElse files.Length = 0 Then Exit Sub
        开始读取(files(0))
    End Sub

    Private Sub 打开_Click(sender As Object, e As EventArgs)
        Using d As New OpenFileDialog With {.Multiselect = False}
            If d.ShowDialog(Me) = DialogResult.OK Then 开始读取(d.FileName)
        End Using
    End Sub

    Private Sub MB_全选视频_Click(sender As Object, e As EventArgs) Handles MB_全选视频.Click
        设置全部(视频复选框, True)
    End Sub

    Private Sub MB_全选音频_Click(sender As Object, e As EventArgs) Handles MB_全选音频.Click
        设置全部(音频复选框, True)
    End Sub

    Private Sub MB_全选字幕_Click(sender As Object, e As EventArgs) Handles MB_全选字幕.Click
        设置全部(字幕复选框, True)
    End Sub

    Private Sub MB_打开文件_Click(sender As Object, e As EventArgs) Handles MB_打开文件.Click
        打开_Click(sender, e)
    End Sub

    Private Sub MB_重置选择_Click(sender As Object, e As EventArgs) Handles MB_重置选择.Click
        设置全部(视频复选框.Concat(音频复选框).Concat(字幕复选框), False)
    End Sub

    Private Sub MB_确认选择_Click(sender As Object, e As EventArgs) Handles MB_确认选择.Click
        确认_Click(sender, e)
    End Sub

    Private Sub 开始读取(媒体文件 As String)
        Dim 输出内容 As New List(Of String)
        Using ffprobe As New Process
            ffprobe.StartInfo.FileName = "ffprobe"
            ffprobe.StartInfo.WorkingDirectory = If(设置_v6.实例对象.工作目录 <> "", 设置_v6.实例对象.工作目录, "")
            ffprobe.StartInfo.Arguments = $"-hide_banner ""{媒体文件}"""
            ffprobe.StartInfo.RedirectStandardOutput = True
            ffprobe.StartInfo.RedirectStandardError = True
            ffprobe.StartInfo.StandardOutputEncoding = System.Text.Encoding.UTF8
            ffprobe.StartInfo.StandardErrorEncoding = System.Text.Encoding.UTF8
            ffprobe.StartInfo.CreateNoWindow = True
            AddHandler ffprobe.OutputDataReceived, Sub(sender, e)
                                                       If e.Data IsNot Nothing Then 输出内容.Add(e.Data)
                                                   End Sub
            AddHandler ffprobe.ErrorDataReceived, Sub(sender, e)
                                                      If e.Data IsNot Nothing Then 输出内容.Add(e.Data)
                                                  End Sub
            ffprobe.Start()
            ffprobe.BeginOutputReadLine()
            ffprobe.BeginErrorReadLine()
            ffprobe.WaitForExit()
        End Using

        列表面板.Controls.Clear()
        视频复选框.Clear()
        音频复选框.Clear()
        字幕复选框.Clear()
        Dim 列表控件 As New List(Of Control)

        If 输出内容.Count = 0 Then
            列表控件.Add(创建说明("ffprobe 未输出任何信息", Color.IndianRed))
            呈现列表控件(列表控件)
            Exit Sub
        End If

        Dim info = 媒体信息解析器.解析(输出内容)
        添加分组(列表控件, "视频流")
        For i = 0 To info.视频流.Count - 1
            Dim s = info.视频流(i)
            Dim text = $"{文件索引}:v:{i}  {If(s.流语言 <> "", "[" & s.流语言 & "] ", "")}{s.编码器}"
            If s.分辨率 <> "" Then text &= " | " & s.分辨率
            If s.帧率 <> "" Then text &= " | " & s.帧率
            If s.比特率 <> "" Then text &= " | " & s.比特率
            添加流复选框(列表控件, 视频复选框, text, i, 视频全选 OrElse 视频已选.Contains(i), Color.CornflowerBlue)
        Next

        添加分组(列表控件, "音频流")
        For i = 0 To info.音频流.Count - 1
            Dim s = info.音频流(i)
            Dim text = $"{文件索引}:a:{i}  {If(s.流语言 <> "", "[" & s.流语言 & "] ", "")}{s.编码器}"
            If s.声道布局 <> "" Then text &= " | " & s.声道布局
            If s.采样率 <> "" Then text &= " | " & s.采样率
            If s.比特率 <> "" Then text &= " | " & s.比特率
            添加流复选框(列表控件, 音频复选框, text, i, 音频全选 OrElse 音频已选.Contains(i), Color.MediumPurple)
        Next

        添加分组(列表控件, "字幕流")
        For i = 0 To info.字幕流.Count - 1
            Dim s = info.字幕流(i)
            Dim text = $"{文件索引}:s:{i}  {If(s.流语言 <> "", "[" & s.流语言 & "] ", "")}{s.编码器}"
            添加流复选框(列表控件, 字幕复选框, text, i, 字幕全选 OrElse 字幕已选.Contains(i), Color.OliveDrab)
        Next

        呈现列表控件(列表控件)
    End Sub

    Private Sub 添加分组(目标控件 As List(Of Control), text As String)
        目标控件.Add(创建说明(text, Color.Silver, 13.0F, True))
    End Sub

    Private Function 创建说明(text As String, color As Color, Optional size As Single = 10.0F, Optional 作为标题 As Boolean = False) As HtmlColorLabel
        Dim htmlColor = ColorTranslator.ToHtml(color)
        Return New HtmlColorLabel With {
            .AutoSize = True,
            .AutoSizeMode = AutoSizeMode.GrowAndShrink,
            .AllowDrop = True,
            .Dock = DockStyle.Top,
            .ForeColor = Color.FromArgb(120, 255, 255, 255),
            .Margin = New Padding(2),
            .Padding = If(作为标题, New Padding(0, 10, 0, 10), New Padding(0, 0, 0, 10)),
            .Text = $"<span style=""font-size:{CInt(size)}; color:{htmlColor}"">{text}</span>"
        }
    End Function

    Private Sub 添加流复选框(目标控件 As List(Of Control), list As List(Of ModernCheckBox), text As String, index As Integer, checked As Boolean, Optional 选中颜色 As Color = Nothing)
        If 选中颜色.IsEmpty Then 选中颜色 = ForeColor
        Dim cb As New ModernCheckBox With {
            .AllowDrop = True,
            .AutoSize = True,
            .Text = text,
            .Tag = index,
            .Checked = checked,
            .ClickAnywhere = True,
            .BoxBorderRadius = 5,
            .BoxBorderSize = 0,
            .BoxCheckedBackColor = 选中颜色,
            .BoxInnerPadding = 6,
            .BoxSize = 22,
            .BoxTextSpacing = 10,
            .Dock = DockStyle.Top,
            .Padding = New Padding(0, 0, 0, 10)
        }
        绑定文件拖入(cb)
        list.Add(cb)
        目标控件.Add(cb)
    End Sub

    Private Sub 呈现列表控件(控件列表 As List(Of Control))
        列表面板.SuspendLayout()
        Try
            列表面板.Controls.Clear()
            For i = 控件列表.Count - 1 To 0 Step -1
                绑定文件拖入(控件列表(i))
                列表面板.Controls.Add(控件列表(i))
            Next
        Finally
            列表面板.ResumeLayout(False)
            列表面板.PerformLayout()
        End Try
    End Sub

    Private Sub 设置全部(items As IEnumerable(Of ModernCheckBox), checked As Boolean)
        For Each cb In items
            cb.Checked = checked
        Next
    End Sub

    Private Sub 确认_Click(sender As Object, e As EventArgs)
        写回(视频目标, 视频复选框, "v")
        写回(音频目标, 音频复选框, "a")
        写回(字幕目标, 字幕复选框, "s")
        Close()
    End Sub

    Private Sub 写回(target As Object, items As List(Of ModernCheckBox), kind As String)
        If target Is Nothing Then Exit Sub
        target.Text = String.Join(",", items.Where(Function(x) x.Checked).Select(Function(x) $"{文件索引}:{kind}:{CInt(x.Tag)}"))
    End Sub

    Private Sub 解析已选(value As String, kind As String, target As HashSet(Of Integer), ByRef allSelected As Boolean)
        If String.IsNullOrWhiteSpace(value) Then Exit Sub
        For Each raw In value.Split(","c, StringSplitOptions.RemoveEmptyEntries)
            Dim s = raw.Trim().ToLowerInvariant()
            If s = $"{文件索引}:{kind}" OrElse s = $"{文件索引}:{kind}?" Then
                allSelected = True
                Continue For
            End If
            Dim tail = s.Split(":"c).LastOrDefault()
            Dim n As Integer
            If Integer.TryParse(tail, n) Then target.Add(n)
        Next
    End Sub

    Private Sub Form_v6_媒体流选择器_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = FormMain_v6.Icon
        SetControlFont(设置_v6.实例对象.字体, Me, , True)
        If FormMain_v6.ThisIsYourWindow1.AttachedForms.Count > 0 Then
            FormMain_v6.ThisIsYourWindow1.Attach(Me)
            ModernPanel1.BackColor = Color.Transparent
            ModernPanel1.BackColor1 = Color.Transparent
        End If
    End Sub
End Class
