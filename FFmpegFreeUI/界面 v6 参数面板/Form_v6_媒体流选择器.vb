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

    Private Class 媒体信息解析器
        Public Class 媒体信息
            Public Property 视频流 As List(Of 视频流信息)
            Public Property 音频流 As List(Of 音频流信息)
            Public Property 字幕流 As List(Of 字幕流信息)
        End Class

        Public Class 视频流信息
            Public Property 编码器 As String = ""
            Public Property 编码器库 As String = ""
            Public Property 比特率 As String = ""
            Public Property 像素格式 As String = ""
            Public Property 分辨率 As String = ""
            Public Property 帧率 As String = ""
            Public Property 流语言 As String = ""
            Public Property 元数据标题 As String = ""
        End Class

        Public Class 音频流信息
            Public Property 编码器 As String = ""
            Public Property 编码器库 As String = ""
            Public Property 比特率 As String = ""
            Public Property 采样率 As String = ""
            Public Property 声道布局 As String = ""
            Public Property 流语言 As String = ""
            Public Property 元数据标题 As String = ""
        End Class

        Public Class 字幕流信息
            Public Property 编码器 As String = ""
            Public Property 编码器库 As String = ""
            Public Property 流语言 As String = ""
            Public Property 元数据标题 As String = ""
        End Class

        Public Shared Function 解析(ffprobeOutput As List(Of String)) As 媒体信息
            Dim 结果 As New 媒体信息 With {
                .视频流 = New List(Of 视频流信息),
                .音频流 = New List(Of 音频流信息),
                .字幕流 = New List(Of 字幕流信息)
            }

            Dim 当前流类型 As String = ""
            Dim 当前视频流 As 视频流信息 = Nothing
            Dim 当前音频流 As 音频流信息 = Nothing
            Dim 当前字幕流 As 字幕流信息 = Nothing

            For Each 行 In ffprobeOutput
                Dim 清理行 = 行.Trim()

                Dim 流匹配 = System.Text.RegularExpressions.Regex.Match(清理行, "Stream #\d+:\d+(?:\[0x[0-9a-fA-F]+\])?(?:\((\w+)\))?: (Video|Audio|Subtitle): (.+)")
                If 流匹配.Success Then
                    Dim 语言 = 流匹配.Groups(1).Value
                    Dim 流类型 = 流匹配.Groups(2).Value
                    Dim 流详情 = 流匹配.Groups(3).Value
                    当前流类型 = 流类型

                    Select Case 流类型
                        Case "Video"
                            当前视频流 = 解析视频流(流详情, 语言)
                            结果.视频流.Add(当前视频流)
                        Case "Audio"
                            当前音频流 = 解析音频流(流详情, 语言)
                            结果.音频流.Add(当前音频流)
                        Case "Subtitle"
                            当前字幕流 = 解析字幕流(流详情, 语言)
                            结果.字幕流.Add(当前字幕流)
                    End Select
                    Continue For
                End If

                Dim 标题匹配 = System.Text.RegularExpressions.Regex.Match(清理行, "^\s*title\s*:\s*(.+)$", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
                If 标题匹配.Success Then
                    Dim 标题值 = 标题匹配.Groups(1).Value.Trim()
                    Select Case 当前流类型
                        Case "Video"
                            If 当前视频流 IsNot Nothing Then 当前视频流.元数据标题 = 标题值
                        Case "Audio"
                            If 当前音频流 IsNot Nothing Then 当前音频流.元数据标题 = 标题值
                        Case "Subtitle"
                            If 当前字幕流 IsNot Nothing Then 当前字幕流.元数据标题 = 标题值
                    End Select
                End If

                Dim 编码器库匹配 = System.Text.RegularExpressions.Regex.Match(清理行, "^\s*encoder\s*:\s*(.+)$", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
                If 编码器库匹配.Success Then
                    Dim 编码器库值 = 编码器库匹配.Groups(1).Value.Trim()
                    Select Case 当前流类型
                        Case "Video"
                            If 当前视频流 IsNot Nothing Then 当前视频流.编码器库 = 编码器库值
                        Case "Audio"
                            If 当前音频流 IsNot Nothing Then 当前音频流.编码器库 = 编码器库值
                        Case "Subtitle"
                            If 当前字幕流 IsNot Nothing Then 当前字幕流.编码器库 = 编码器库值
                    End Select
                End If
            Next

            Return 结果
        End Function

        Private Shared Function 解析视频流(流详情 As String, 语言 As String) As 视频流信息
            Dim 信息 As New 视频流信息 With {.流语言 = 语言}
            Dim 部分 = 分割流详情(流详情)

            If 部分.Count > 0 Then 信息.编码器 = 提取带括号内容(部分(0))

            For Each 部分项 In 部分
                Dim 清理项 = 部分项.Trim()

                If System.Text.RegularExpressions.Regex.IsMatch(清理项, "^(yuv|rgb|bgr|gray|p0|nv)\w*", System.Text.RegularExpressions.RegexOptions.IgnoreCase) Then
                    信息.像素格式 = 提取带括号内容(清理项)
                End If

                Dim 分辨率匹配 = System.Text.RegularExpressions.Regex.Match(清理项, "(\d{2,5}x\d{2,5})")
                If 分辨率匹配.Success AndAlso String.IsNullOrEmpty(信息.分辨率) Then
                    信息.分辨率 = 分辨率匹配.Groups(1).Value
                End If

                Dim 帧率匹配 = System.Text.RegularExpressions.Regex.Match(清理项, "([\d.]+)\s*fps")
                If 帧率匹配.Success Then
                    Dim 帧率值 As Double
                    If Double.TryParse(帧率匹配.Groups(1).Value, 帧率值) Then
                        信息.帧率 = CInt(Math.Round(帧率值)).ToString() & "fps"
                    End If
                End If

                Dim 比特率匹配 = System.Text.RegularExpressions.Regex.Match(清理项, "(\d+)\s*kb/s(?:\s*\([^)]*\))?", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
                If 比特率匹配.Success Then 信息.比特率 = 比特率匹配.Groups(1).Value & "kbps"
            Next

            Return 信息
        End Function

        Private Shared Function 解析音频流(流详情 As String, 语言 As String) As 音频流信息
            Dim 信息 As New 音频流信息 With {.流语言 = 语言}
            Dim 部分 = 分割流详情(流详情)

            If 部分.Count > 0 Then 信息.编码器 = 提取带括号内容(部分(0))

            For Each 部分项 In 部分
                Dim 清理项 = 部分项.Trim()

                Dim 采样率匹配 = System.Text.RegularExpressions.Regex.Match(清理项, "(\d+)\s*Hz")
                If 采样率匹配.Success Then 信息.采样率 = 采样率匹配.Groups(1).Value & "Hz"

                If System.Text.RegularExpressions.Regex.IsMatch(清理项, "^(mono|stereo|[\d.]+|[a-z]+\(side\))$", System.Text.RegularExpressions.RegexOptions.IgnoreCase) Then
                    信息.声道布局 = 清理项
                End If

                Dim 比特率匹配 = System.Text.RegularExpressions.Regex.Match(清理项, "(\d+)\s*kb/s(?:\s*\([^)]*\))?", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
                If 比特率匹配.Success Then 信息.比特率 = 比特率匹配.Groups(1).Value & "kbps"
            Next

            Return 信息
        End Function

        Private Shared Function 解析字幕流(流详情 As String, 语言 As String) As 字幕流信息
            Dim 信息 As New 字幕流信息 With {.流语言 = 语言}
            Dim 部分 = 分割流详情(流详情)
            If 部分.Count > 0 Then 信息.编码器 = 提取带括号内容(部分(0))
            Return 信息
        End Function

        Private Shared Function 提取带括号内容(输入 As String) As String
            Dim 清理输入 = 输入.Trim()
            Dim 匹配 = System.Text.RegularExpressions.Regex.Match(清理输入, "^(\w+(?:\s*\([^)]*\))?)")
            If 匹配.Success Then Return 匹配.Groups(1).Value.Trim()
            Return 清理输入.Split({","c, " "c})(0)
        End Function

        Private Shared Function 分割流详情(详情 As String) As List(Of String)
            Dim 结果 As New List(Of String)
            Dim 当前部分 As New Text.StringBuilder()
            Dim 括号深度 As Integer = 0

            For Each 字符 In 详情
                If 字符 = "("c Then
                    括号深度 += 1
                    当前部分.Append(字符)
                ElseIf 字符 = ")"c Then
                    括号深度 -= 1
                    当前部分.Append(字符)
                ElseIf 字符 = ","c AndAlso 括号深度 = 0 Then
                    If 当前部分.Length > 0 Then
                        结果.Add(当前部分.ToString().Trim())
                        当前部分.Clear()
                    End If
                Else
                    当前部分.Append(字符)
                End If
            Next

            If 当前部分.Length > 0 Then 结果.Add(当前部分.ToString().Trim())
            Return 结果
        End Function
    End Class
End Class
