Imports LakeUI

Public Class Form_v6_媒体流选择器
    Private 视频目标 As Object
    Private 音频目标 As Object
    Private 字幕目标 As Object
    Private 读取取消源 As Threading.CancellationTokenSource
    Private 读取进程 As Process
    Private 正在关闭 As Boolean
    Private 资源已释放 As Boolean
    Private 窗口样式 As ThisIsYourWindow
    Private ReadOnly 拖入控件 As New List(Of Control)
    Private ReadOnly 自有字体 As New Dictionary(Of (Single, FontStyle), Font)
    Private ReadOnly 文件索引 As String
    Private ReadOnly 视频已选 As New HashSet(Of Integer)
    Private ReadOnly 音频已选 As New HashSet(Of Integer)
    Private ReadOnly 字幕已选 As New HashSet(Of Integer)
    Private 视频全选 As Boolean
    Private 音频全选 As Boolean
    Private 字幕全选 As Boolean

    Private ReadOnly 视频列表项 As New List(Of UltraDetailListView.ListItem)
    Private ReadOnly 音频列表项 As New List(Of UltraDetailListView.ListItem)
    Private ReadOnly 字幕列表项 As New List(Of UltraDetailListView.ListItem)
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
        绑定文件拖入(UltraDetailListView1)
        绑定文件拖入(顶部面板)
        绑定文件拖入(底部面板)
        绑定文件拖入(MB_全选视频)
        绑定文件拖入(MB_全选音频)
        绑定文件拖入(MB_全选字幕)
        绑定文件拖入(MB_打开文件)
        绑定文件拖入(MB_重置选择)
        绑定文件拖入(MB_确认选择)
    End Sub

    Protected Overrides Sub OnShown(e As EventArgs)
        MyBase.OnShown(e)
        调整列宽()
        If 初始媒体文件 <> "" Then 开始读取(初始媒体文件)
    End Sub

    Private Sub 绑定文件拖入(target As Control)
        If 拖入控件.Contains(target) Then Return
        target.AllowDrop = True
        AddHandler target.DragEnter, AddressOf 文件拖入事件
        AddHandler target.DragDrop, AddressOf 文件放下事件
        拖入控件.Add(target)
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

    Private Sub MB_打开文件_Click(sender As Object, e As EventArgs) Handles MB_打开文件.Click
        Using d As New OpenFileDialog With {.Multiselect = False}
            If d.ShowDialog(Me) = DialogResult.OK Then 开始读取(d.FileName)
        End Using
    End Sub

    Private Sub MB_全选视频_Click(sender As Object, e As EventArgs) Handles MB_全选视频.Click
        设置全部(视频列表项, True)
    End Sub

    Private Sub MB_全选音频_Click(sender As Object, e As EventArgs) Handles MB_全选音频.Click
        设置全部(音频列表项, True)
    End Sub

    Private Sub MB_全选字幕_Click(sender As Object, e As EventArgs) Handles MB_全选字幕.Click
        设置全部(字幕列表项, True)
    End Sub

    Private Sub MB_重置选择_Click(sender As Object, e As EventArgs) Handles MB_重置选择.Click
        设置全部(视频列表项.Concat(音频列表项).Concat(字幕列表项), False)
    End Sub

    Private Async Sub 开始读取(媒体文件 As String)
        If 正在关闭 OrElse IsDisposed OrElse Disposing Then Return
        取消读取()
        Dim cancellation As New Threading.CancellationTokenSource(TimeSpan.FromSeconds(30))
        读取取消源 = cancellation
        MB_确认选择.Enabled = False
        显示读取提示("正在读取媒体流…")
        Await 读取媒体Async(媒体文件, cancellation)
    End Sub

    Private Async Function 读取媒体Async(媒体文件 As String, cancellation As Threading.CancellationTokenSource) As Task
        Using cancellationLifetime = cancellation, ffprobe As New Process
            Try
                ffprobe.StartInfo = New ProcessStartInfo With {
                    .FileName = 设置_v6.获取FFprobe进程文件名(),
                    .WorkingDirectory = 设置_v6.获取有效工作目录(),
                    .UseShellExecute = False, .CreateNoWindow = True,
                    .RedirectStandardOutput = True, .RedirectStandardError = True,
                    .StandardOutputEncoding = System.Text.Encoding.UTF8,
                    .StandardErrorEncoding = System.Text.Encoding.UTF8
                }
                ffprobe.StartInfo.ArgumentList.Add("-hide_banner")
                ffprobe.StartInfo.ArgumentList.Add(媒体文件)
                读取进程 = ffprobe
                cancellation.Token.ThrowIfCancellationRequested()
                ffprobe.Start()
                Dim stdout = 读取探测输出Async(ffprobe.StandardOutput, cancellation.Token)
                Dim stderr = 读取探测输出Async(ffprobe.StandardError, cancellation.Token)
                Await Task.WhenAll(stdout, stderr, ffprobe.WaitForExitAsync(cancellation.Token)).WaitAsync(cancellation.Token)
                If Not 可应用读取结果(cancellation) Then Return
                If ffprobe.ExitCode <> 0 Then
                    显示读取提示("读取媒体流失败：" & Agent通用工具_v6.LimitText(stderr.Result.Trim(), 300))
                    Return
                End If
                Dim lines = (stderr.Result & vbLf & stdout.Result).Split({vbCrLf, vbLf, vbCr}, StringSplitOptions.RemoveEmptyEntries).ToList()
                显示流信息(lines)
            Catch ex As OperationCanceledException When cancellation.IsCancellationRequested
                If 可应用读取结果(cancellation, True) Then 显示读取提示("读取媒体流超时")
            Catch ex As Exception
                If 可应用读取结果(cancellation) Then 显示读取提示("读取媒体流失败：" & ex.Message)
            Finally
                终止进程(ffprobe)
                If ReferenceEquals(读取进程, ffprobe) Then 读取进程 = Nothing
                If ReferenceEquals(读取取消源, cancellation) Then
                    读取取消源 = Nothing
                    If Not 正在关闭 AndAlso Not IsDisposed AndAlso Not Disposing Then MB_确认选择.Enabled = True
                End If
            End Try
        End Using
    End Function

    Private Function 可应用读取结果(cancellation As Threading.CancellationTokenSource, Optional allowTimeout As Boolean = False) As Boolean
        Return Not 正在关闭 AndAlso Not IsDisposed AndAlso Not Disposing AndAlso
            ReferenceEquals(读取取消源, cancellation) AndAlso (allowTimeout OrElse Not cancellation.IsCancellationRequested)
    End Function

    Private Shared Async Function 读取探测输出Async(reader As IO.StreamReader, cancellationToken As Threading.CancellationToken) As Task(Of String)
        Dim output As New Text.StringBuilder()
        Dim buffer(4095) As Char
        Do
            Dim count = Await reader.ReadAsync(buffer.AsMemory(), cancellationToken).ConfigureAwait(False)
            If count = 0 Then Return output.ToString()
            Dim take = Math.Min(count, Math.Max(0, 1024 * 1024 - output.Length))
            output.Append(buffer, 0, take)
        Loop
    End Function

    Private Shared Sub 终止进程(process As Process)
        If process Is Nothing Then Return
        Try
            If Not process.HasExited Then process.Kill(True)
        Catch ex As InvalidOperationException
        Catch ex As ComponentModel.Win32Exception
        End Try
    End Sub

    Private Sub 取消读取()
        Dim cancellation = 读取取消源
        读取取消源 = Nothing
        cancellation?.Cancel()
        终止进程(读取进程)
        读取进程 = Nothing
    End Sub

    Private Sub 显示读取提示(message As String)
        显示流信息(New List(Of String))
        UltraDetailListView1.Items(0).SubItems(0).Text = message
    End Sub

    Private Sub 显示流信息(输出内容 As List(Of String))
        视频列表项.Clear()
        音频列表项.Clear()
        字幕列表项.Clear()
        UltraDetailListView1.BeginUpdate()
        Try
            UltraDetailListView1.Items.Clear()
            UltraDetailListView1.Groups.Clear()

            If 输出内容.Count = 0 Then
                UltraDetailListView1.Items.Add(New UltraDetailListView.ListItem(
                    New UltraDetailListView.ListSubItem("ffprobe 未输出任何信息") With {.ForeColor = Color.IndianRed}))
                Exit Sub
            End If

            Dim info = 媒体信息解析器.解析(输出内容)
            添加分组("视频流", Color.CornflowerBlue)
            For i = 0 To info.视频流.Count - 1
                Dim s = info.视频流(i)
                Dim text = $"{文件索引}:v:{i}  {If(s.流语言 <> "", "[" & s.流语言 & "] ", "")}{s.编码器}"
                If s.分辨率 <> "" Then text &= " | " & s.分辨率
                If s.帧率 <> "" Then text &= " | " & s.帧率
                If s.比特率 <> "" Then text &= " | " & s.比特率
                添加流列表项(视频列表项, "视频流", text, s.元数据标题, i, 视频全选 OrElse 视频已选.Contains(i))
            Next

            添加分组("音频流", Color.MediumPurple)
            For i = 0 To info.音频流.Count - 1
                Dim s = info.音频流(i)
                Dim text = $"{文件索引}:a:{i}  {If(s.流语言 <> "", "[" & s.流语言 & "] ", "")}{s.编码器}"
                If s.声道布局 <> "" Then text &= " | " & s.声道布局
                If s.采样率 <> "" Then text &= " | " & s.采样率
                If s.比特率 <> "" Then text &= " | " & s.比特率
                添加流列表项(音频列表项, "音频流", text, s.元数据标题, i, 音频全选 OrElse 音频已选.Contains(i))
            Next

            添加分组("字幕流", Color.OliveDrab)
            For i = 0 To info.字幕流.Count - 1
                Dim s = info.字幕流(i)
                Dim text = $"{文件索引}:s:{i}  {If(s.流语言 <> "", "[" & s.流语言 & "] ", "")}{s.编码器}"
                添加流列表项(字幕列表项, "字幕流", text, s.元数据标题, i, 字幕全选 OrElse 字幕已选.Contains(i))
            Next
            If UltraDetailListView1.Items.Count = 0 Then
                UltraDetailListView1.Items.Add(New UltraDetailListView.ListItem(New UltraDetailListView.ListSubItem("未找到可选择的媒体流")))
            End If

        Finally
            UltraDetailListView1.EndUpdate()
            调整列宽()
        End Try
    End Sub

    Private Sub 添加分组(text As String, color As Color)
        UltraDetailListView1.Groups.Add(New UltraDetailListView.ListGroup(text) With {
            .ForeColor = color,
            .CheckBoxCheckedBackColor = color,
            .CheckBoxHoverCheckedBackColor = color,
            .CheckBoxPressedCheckedBackColor = color
        })
    End Sub

    Private Sub 添加流列表项(list As List(Of UltraDetailListView.ListItem), groupName As String, text As String, subText As String, index As Integer, isChecked As Boolean)
        Dim item As New UltraDetailListView.ListItem(New UltraDetailListView.ListSubItem(text)) With {
            .GroupName = groupName,
            .Tag = index,
            .Checked = isChecked
        }
        If subText <> "" Then item.BottomLines.Add(New UltraDetailListView.TextLine(subText, Nothing, Color.FromArgb(120, 255, 255, 255)))
        list.Add(item)
        UltraDetailListView1.Items.Add(item)
    End Sub

    Private Sub 设置全部(items As IEnumerable(Of UltraDetailListView.ListItem), isChecked As Boolean)
        UltraDetailListView1.BeginUpdate()
        Try
            For Each item In items
                item.Checked = isChecked
            Next
        Finally
            UltraDetailListView1.EndUpdate()
        End Try
    End Sub

    Private Sub UltraDetailListView1_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles UltraDetailListView1.PreviewKeyDown
        ' 将勾选快捷键作为列表输入，避免 Enter 触发按钮、Tab 切换焦点。
        If e.Modifiers <> Keys.None Then Exit Sub
        Select Case e.KeyCode
            Case Keys.Enter, Keys.Oemplus, Keys.Add, Keys.Tab, Keys.Delete, Keys.OemMinus, Keys.Subtract
                e.IsInputKey = True
        End Select
    End Sub

    Private Sub UltraDetailListView1_ItemDoubleClick(sender As Object, e As UltraDetailListView.ListItemEventArgs) Handles UltraDetailListView1.ItemDoubleClick
        If e?.Item Is Nothing OrElse e.Item.Tag Is Nothing OrElse 正在关闭 Then Return
        e.Item.Checked = Not e.Item.Checked
    End Sub

    Private Sub UltraDetailListView1_KeyDown(sender As Object, e As KeyEventArgs) Handles UltraDetailListView1.KeyDown
        If e.Modifiers <> Keys.None Then Exit Sub
        Select Case e.KeyCode
            Case Keys.Enter, Keys.Oemplus, Keys.Add, Keys.Tab, Keys.Delete, Keys.OemMinus, Keys.Subtract
                UltraDetailListView1.BeginUpdate()
                Try
                    For Each item In 视频列表项.Concat(音频列表项).Concat(字幕列表项).Where(Function(x) x.Selected)
                        Select Case e.KeyCode
                            Case Keys.Tab
                                item.Checked = Not item.Checked
                            Case Keys.Enter, Keys.Oemplus, Keys.Add
                                item.Checked = True
                            Case Else
                                item.Checked = False
                        End Select
                    Next
                Finally
                    UltraDetailListView1.EndUpdate()
                End Try
                e.Handled = True
                e.SuppressKeyPress = True
        End Select
    End Sub

    Private Sub UltraDetailListView1_布局变化(sender As Object, e As EventArgs) Handles UltraDetailListView1.SizeChanged, UltraDetailListView1.PaddingChanged, UltraDetailListView1.DpiChangedAfterParent
        调整列宽()
    End Sub

    Private Sub 调整列宽()
        If UltraDetailListView1.Columns.Count = 0 OrElse UltraDetailListView1.ClientSize.Width <= 0 Then Exit Sub
        Dim dpi = CSng(UltraDetailListView1.DeviceDpi) / 96.0F
        Dim scrollBarWidth = CInt(UltraDetailListView1.ScrollBarWidth * dpi)
        ' 列表项右缘与滚动条之间保留 10 个逻辑像素，避免选中背景和内容紧贴滚动条。
        Dim scrollBarGap = CInt(10 * dpi)
        Dim available = UltraDetailListView1.ClientSize.Width - UltraDetailListView1.Padding.Horizontal - scrollBarWidth - D3D_ScrollBarRenderer.Margin - scrollBarGap
        UltraDetailListView1.Columns(0).Width = Math.Max(1, available)
    End Sub

    Private Sub MB_确认选择_Click(sender As Object, e As EventArgs) Handles MB_确认选择.Click
        写回(视频目标, 视频列表项, "v")
        写回(音频目标, 音频列表项, "a")
        写回(字幕目标, 字幕列表项, "s")
        Close()
    End Sub

    Private Sub 写回(target As Object, items As List(Of UltraDetailListView.ListItem), kind As String)
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
        设置选择器字体(Me, 设置_v6.实例对象.字体)
        If FormMain_v6.ThisIsYourWindow1.AttachedForms.Count > 0 Then
            窗口样式 = FormMain_v6.ThisIsYourWindow1
            窗口样式.Attach(Me)
            ModernPanel1.BackColor = Color.Transparent
            ModernPanel1.BackColor1 = Color.Transparent
        End If
    End Sub

    Private Sub 设置选择器字体(control As Control, fontName As String)
        Dim key = (control.Font.Size, control.Font.Style)
        Dim ownedFont As Font = Nothing
        If Not 自有字体.TryGetValue(key, ownedFont) Then
            ownedFont = New Font(fontName, key.Size, key.Style)
            自有字体.Add(key, ownedFont)
        End If
        control.Font = ownedFont
        control.GetType().GetProperty("AnimationFPS")?.SetValue(control, 设置_v6.实例对象.图形动画帧率)
        control.GetType().GetProperty("DropDownAnimationFPS")?.SetValue(control, 设置_v6.实例对象.图形动画帧率)
        For Each child As Control In control.Controls
            设置选择器字体(child, fontName)
        Next
    End Sub

    Protected Overrides Sub OnFormClosed(e As FormClosedEventArgs)
        正在关闭 = True
        取消读取()
        MyBase.OnFormClosed(e)
    End Sub

    Private Sub 释放选择器资源()
        If 资源已释放 Then Return
        资源已释放 = True
        正在关闭 = True
        取消读取()
        窗口样式?.Detach(Me)
        窗口样式 = Nothing
        For Each target In 拖入控件
            RemoveHandler target.DragEnter, AddressOf 文件拖入事件
            RemoveHandler target.DragDrop, AddressOf 文件放下事件
        Next
        拖入控件.Clear()
        If UltraDetailListView1 IsNot Nothing Then
            UltraDetailListView1.Items.Clear()
            UltraDetailListView1.Groups.Clear()
        End If
        视频列表项.Clear()
        音频列表项.Clear()
        字幕列表项.Clear()
        视频已选.Clear()
        音频已选.Clear()
        字幕已选.Clear()
        视频目标 = Nothing
        音频目标 = Nothing
        字幕目标 = Nothing
    End Sub

    Private Sub 释放自有字体()
        For Each ownedFont As Font In 自有字体.Values
            ownedFont.Dispose()
        Next
        自有字体.Clear()
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
