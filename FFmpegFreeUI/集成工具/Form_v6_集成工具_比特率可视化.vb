Imports System.Globalization
Imports System.IO
Imports System.Threading
Imports System.Text
Imports LakeUI
Imports 核心 = FFmpegFreeUI.比特率可视化数据核心

Public Class Form_v6_集成工具_比特率可视化
    Private Class 文件数据
        Public Property 路径 As String
        Public Property 信息 As 核心.文件信息
        Public Property 已选 As New HashSet(Of Integer)
        Public Property 包 As List(Of 核心.包信息)
        Public Property 结果 As 核心.分析结果
        Public Property 状态 As String = "等待读取流"
        Public Property 错误 As String = ""
        Public Property 颜色序号 As Integer
        Public Property 当前帧 As Long?
    End Class
    Private 工作取消 As CancellationTokenSource
    Private 正在工作 As Boolean
    Private 已初始化 As Boolean
    Private 下个颜色序号 As Integer
    Private Const 列表设计高度 As Integer = 160
    Private ReadOnly 浅色 As Color() = {
        Color.FromArgb(0, 94, 184), Color.FromArgb(194, 32, 48), Color.FromArgb(176, 80, 0), Color.FromArgb(132, 102, 0),
        Color.FromArgb(0, 122, 0), Color.FromArgb(0, 118, 130), Color.FromArgb(63, 77, 199), Color.FromArgb(137, 42, 176)}
    Private ReadOnly 深色 As Color() = {
        Color.FromArgb(230, 230, 230), Color.FromArgb(230, 86, 86), Color.FromArgb(232, 144, 63), Color.FromArgb(222, 196, 68),
        Color.FromArgb(99, 197, 95), Color.FromArgb(70, 202, 196), Color.FromArgb(88, 139, 232), Color.FromArgb(170, 103, 224)}

    Private Sub 页面加载(sender As Object, e As EventArgs) Handles MyBase.Load
        UltraDetailListView1.MultiSelect = True
        For Each column In UltraDetailListView1.Columns
            column.WordWrapHeightFixed = True
        Next
        MCB_选择查看类型.SelectedIndex = 0
        MCB_选择图表精度.SelectedIndex = 1
        Ultra2DChart1.LegendPosition = Ultra2DChart.LegendPositionEnum.None
        Ultra2DChart1.EnableXAxisZoom = True
        Ultra2DChart1.EnableXAxisPan = True
        Ultra2DChart1.ShowLineHoverCrosshair = True
        Ultra2DChart1.DataPointMinimumSpacing = 0
        Ultra2DChart1.LineHoverTooltipFormatter = AddressOf 格式化悬停
        For Each target As Control In {Me, ModernPanel1, Panel1, ModernPanel2, UltraDetailListView1, Ultra2DChart1, HtmlColorLabel1, HtmlColorLabel2}
            target.AllowDrop = True
            AddHandler target.DragEnter, AddressOf 拖入
            AddHandler target.DragDrop, AddressOf 放下
        Next
        已初始化 = True
        AddHandler 界面主题_v6.主题已更改, AddressOf 主题变化
        刷新呈现(True)
    End Sub

    Private Sub 主题变化()
        刷新呈现(False)
    End Sub

    Private Function 取颜色(data As 文件数据) As Color
        Dim palette = If(界面主题_v6.当前为浅色模式, 浅色, 深色)
        If data.颜色序号 < palette.Length Then Return palette(data.颜色序号)
        ' 颜色跟随文件，移动或删除其他文件不改变它。
        Dim hue = (data.颜色序号 * 137.508) Mod 360
        Dim brightness = If(界面主题_v6.当前为浅色模式, 0.65, 0.95)
        Dim c = brightness * 0.65
        Dim x = c * (1 - Math.Abs((hue / 60) Mod 2 - 1))
        Dim m = brightness - c
        Dim rgb As Double()
        Select Case CInt(Math.Floor(hue / 60))
            Case 0 : rgb = {c, x, 0}
            Case 1 : rgb = {x, c, 0}
            Case 2 : rgb = {0, c, x}
            Case 3 : rgb = {0, x, c}
            Case 4 : rgb = {x, 0, c}
            Case Else : rgb = {c, 0, x}
        End Select
        Return Color.FromArgb(CInt((rgb(0) + m) * 255), CInt((rgb(1) + m) * 255), CInt((rgb(2) + m) * 255))
    End Function

    Private ReadOnly Property 精度 As String
        Get
            Return If(MCB_选择图表精度.SelectedIndex >= 0, MCB_选择图表精度.SelectedItem.ToString(), "每秒")
        End Get
    End Property
    Private ReadOnly Property 查看类型 As String
        Get
            Return If(MCB_选择查看类型.SelectedIndex >= 0, MCB_选择查看类型.SelectedItem.ToString(), "视频和音频")
        End Get
    End Property

    Private Sub 设置工作状态(busy As Boolean, Optional adding As Boolean = False)
        正在工作 = busy
        For Each control As Control In {MB_添加文件, MB_上移, MB_下移, MB_移除选中, MB_移除全部, MB_清除统计, MB_清除全部, MCB_选择图表精度, MCB_选择查看类型}
            control.Enabled = Not busy
        Next
        UltraDetailListView1.AllowDragReorder = Not busy
        MB_开始分析.Enabled = True
        MB_开始分析.Text = If(busy, If(adding, "停止读取", "停止分析"), "开始分析")
    End Sub

    Private Async Sub 添加点击(sender As Object, e As EventArgs) Handles MB_添加文件.Click
        If 正在工作 Then Return
        Using dialog As New OpenFileDialog With {.Multiselect = True, .Filter = "所有文件|*.*"}
            If dialog.ShowDialog(Me) = DialogResult.OK Then Await 添加文件Async(dialog.FileNames)
        End Using
    End Sub
    Private Sub 拖入(sender As Object, e As DragEventArgs)
        e.Effect = If(Not 正在工作 AndAlso e.Data IsNot Nothing AndAlso e.Data.GetDataPresent(DataFormats.FileDrop), DragDropEffects.Copy, DragDropEffects.None)
    End Sub
    Private Async Sub 放下(sender As Object, e As DragEventArgs)
        If 正在工作 OrElse e.Data Is Nothing Then Return
        Dim paths = TryCast(e.Data.GetData(DataFormats.FileDrop), String())
        If paths IsNot Nothing Then Await 添加文件Async(paths)
    End Sub

    Private Async Function 添加文件Async(paths As IEnumerable(Of String)) As Task
        If 正在工作 Then Return
        Dim files As New List(Of String)
        For Each rawPath In paths
            Try
                If Directory.Exists(rawPath) Then
                    files.AddRange(Directory.GetFiles(rawPath).OrderBy(Function(p) p, StringComparer.CurrentCultureIgnoreCase))
                ElseIf File.Exists(rawPath) Then
                    files.Add(Path.GetFullPath(rawPath))
                End If
            Catch ex As Exception
                ExFloatingTip(UltraDetailListView1, ex.Message, 2200)
            End Try
        Next
        If files.Count = 0 Then Return
        工作取消 = New CancellationTokenSource()
        设置工作状态(True, True)
        Try
            For Each filePath In files.Distinct(StringComparer.OrdinalIgnoreCase)
                工作取消.Token.ThrowIfCancellationRequested()
                If UltraDetailListView1.Items.Any(Function(i) String.Equals(DirectCast(i.Tag, 文件数据).路径, filePath, StringComparison.OrdinalIgnoreCase)) Then Continue For
                Dim data As New 文件数据 With {.路径 = filePath, .颜色序号 = 下个颜色序号}
                下个颜色序号 += 1
                Dim item As New UltraDetailListView.ListItem With {.Tag = data}
                For Each value In {Path.GetFileName(filePath), "读取流…", "—", "—", "—", "—", "—"}
                    item.SubItems.Add(New UltraDetailListView.ListSubItem(value))
                Next
                UltraDetailListView1.Items.Add(item)
                UltraDetailListView1.SelectedIndex = UltraDetailListView1.Items.Count - 1
                刷新呈现(False)
                Await 读取流Async(data, 工作取消.Token)
                刷新呈现(False)
            Next
        Catch ex As OperationCanceledException
        Finally
            工作取消.Dispose()
            工作取消 = Nothing
            If Not IsDisposed Then
                设置工作状态(False)
                刷新呈现(False)
            End If
        End Try
    End Function

    Private Async Function 读取流Async(data As 文件数据, token As CancellationToken) As Task
        Try
            Using timeout = CancellationTokenSource.CreateLinkedTokenSource(token)
                timeout.CancelAfter(TimeSpan.FromSeconds(30))
                data.信息 = Await 核心.读取文件信息Async(data.路径, timeout.Token)
            End Using
            data.已选.Clear()
            For Each kind In {"video", "audio"}
                Dim stream = data.信息.流.FirstOrDefault(Function(s) s.类型 = kind)
                If stream IsNot Nothing Then data.已选.Add(stream.索引)
            Next
            data.状态 = If(data.信息.流.Count > 0, "未分析", "没有可用音视频流")
            data.错误 = ""
        Catch ex As OperationCanceledException When token.IsCancellationRequested
            Throw
        Catch ex As Exception
            data.状态 = "读取流失败"
            data.错误 = ex.Message
        End Try
    End Function

    Private Async Sub 分析点击(sender As Object, e As EventArgs) Handles MB_开始分析.Click
        If 正在工作 Then
            工作取消?.Cancel()
            MB_开始分析.Enabled = False
            Return
        End If
        If UltraDetailListView1.Items.Count = 0 Then
            ExFloatingTip(MB_开始分析, "请先添加媒体文件", 1800)
            Return
        End If
        If 查看类型 = "仅音频" AndAlso 精度 <> "每秒" Then
            ExFloatingTip(MCB_选择图表精度, "音频仅支持每秒模式", 2000)
            Return
        End If
        工作取消 = New CancellationTokenSource()
        设置工作状态(True)
        Dim completed = 0
        Try
            For Each item In UltraDetailListView1.Items.ToArray()
                Dim data = DirectCast(item.Tag, 文件数据)
                工作取消.Token.ThrowIfCancellationRequested()
                If data.信息 Is Nothing Then Await 读取流Async(data, 工作取消.Token)
                If data.信息 Is Nothing Then
                    刷新分析列表项(item, data)
                    Continue For
                End If
                Dim streams = 核心.获取有效流(data.信息, data.已选, 精度, 查看类型)
                If streams.Count = 0 Then
                    data.结果 = Nothing
                    data.状态 = "当前模式无可用流"
                    刷新呈现(False)
                    Continue For
                End If
                data.当前帧 = 0
                data.错误 = ""
                刷新分析列表项(item, data)
                Dim token = 工作取消.Token
                Dim progress As New Progress(Of Long)(
                    Sub(value)
                        If IsDisposed OrElse token.IsCancellationRequested OrElse 工作取消 Is Nothing OrElse 工作取消.Token <> token OrElse Not data.当前帧.HasValue Then Return
                        data.当前帧 = value
                        刷新分析列表项(item, data)
                    End Sub)
                Try
                    Dim countStreams = If(streams.Any(Function(s) s.类型 = "video"), streams.Where(Function(s) s.类型 = "video"), streams)
                    Dim countIndices As New HashSet(Of Integer)(countStreams.Select(Function(s) s.索引))
                    If data.包 Is Nothing Then
                        Dim indices As New HashSet(Of Integer)(data.信息.流.Select(Function(s) s.索引))
                        data.包 = Await Task.Run(Function() 核心.读取包Async(data.路径, indices, token, progress, countIndices), token)
                    End If
                    data.当前帧 = data.包.LongCount(Function(p) countIndices.Contains(p.流索引))
                    刷新分析列表项(item, data)
                    Dim precision = 精度
                    Dim kind = 查看类型
                    data.结果 = Await Task.Run(Function() 核心.构造序列(data.信息, data.包, data.已选, precision, kind, token), token)
                    data.状态 = If(data.结果 Is Nothing, "无可用时间戳或数据包", "已完成")
                    completed += 1
                Catch ex As OperationCanceledException
                    data.状态 = "已停止"
                    Throw
                Catch ex As Exception
                    data.结果 = Nothing
                    data.状态 = "分析失败"
                    data.错误 = ex.Message
                Finally
                    data.当前帧 = Nothing
                    刷新呈现(False)
                End Try
            Next
            ExFloatingTip(MB_开始分析, $"分析完成：{completed} 个文件", 1800)
        Catch ex As OperationCanceledException
        Finally
            工作取消.Dispose()
            工作取消 = Nothing
            If Not IsDisposed Then
                设置工作状态(False)
                Ultra2DChart1.ResetXAxisView()
                Ultra2DChart1.YAxisRangeMode = Ultra2DChart.AxisRangeModeEnum.Auto
                Ultra2DChart1.YAxisIncludeZero = True
                调整布局()
                Ultra2DChart1.RefreshChart()
            End If
        End Try
    End Sub

    Private Sub 刷新分析列表项(item As UltraDetailListView.ListItem, data As 文件数据)
        If IsDisposed Then Return
        UltraDetailListView1.BeginUpdate()
        Try
            更新列表项(item, data)
        Finally
            UltraDetailListView1.EndUpdate()
        End Try
    End Sub

    Private Sub 选择流(sender As Object, e As UltraDetailListView.ListItemEventArgs) Handles UltraDetailListView1.ItemDoubleClick
        If 正在工作 Then Return
        Dim data = TryCast(e.Item?.Tag, 文件数据)
        If data?.信息 Is Nothing Then
            ExFloatingTip(UltraDetailListView1, "尚未读取到可用流，请重试分析", 1800)
            Return
        End If
        Using video As New ModernTextBox(), audio As New ModernTextBox()
            video.Text = String.Join(",", data.信息.流.Where(Function(s) s.类型 = "video" AndAlso data.已选.Contains(s.索引)).Select(Function(s) s.选择符))
            audio.Text = String.Join(",", data.信息.流.Where(Function(s) s.类型 = "audio" AndAlso data.已选.Contains(s.索引)).Select(Function(s) s.选择符))
            Using selector As New Form_v6_媒体流选择器(data.路径, video, audio, Nothing, "0", video.Text, audio.Text)
                selector.MB_打开文件.Enabled = False
                selector.MB_全选字幕.Enabled = False
                禁用拖入(selector)
                界面主题_v6.应用当前主题到窗体(selector)
                selector.StartPosition = FormStartPosition.CenterParent
                selector.ShowDialog(FindForm())
            End Using
            Dim choices As New HashSet(Of String)((video.Text & "," & audio.Text).Split(","c, StringSplitOptions.RemoveEmptyEntries), StringComparer.OrdinalIgnoreCase)
            data.已选 = New HashSet(Of Integer)(data.信息.流.Where(Function(s) choices.Contains(s.选择符)).Select(Function(s) s.索引))
        End Using
        重算缓存()
    End Sub
    Private Shared Sub 禁用拖入(control As Control)
        control.AllowDrop = False
        For Each child As Control In control.Controls
            禁用拖入(child)
        Next
    End Sub
    Private Sub 模式改变(sender As Object, e As EventArgs) Handles MCB_选择图表精度.SelectedIndexChanged, MCB_选择查看类型.SelectedIndexChanged
        If 已初始化 AndAlso Not 正在工作 Then 重算缓存()
    End Sub
    Private Sub 重算缓存()
        For Each item In UltraDetailListView1.Items
            Dim data = DirectCast(item.Tag, 文件数据)
            If data.包 Is Nothing OrElse data.信息 Is Nothing Then Continue For
            Try
                data.结果 = 核心.构造序列(data.信息, data.包, data.已选, 精度, 查看类型)
                data.状态 = If(data.结果 Is Nothing, "当前模式无可用数据", "已完成")
            Catch ex As Exception
                data.结果 = Nothing
                data.状态 = "数据异常"
                data.错误 = ex.Message
            End Try
        Next
        刷新呈现(True)
    End Sub

    Private Sub 刷新呈现(resetView As Boolean)
        If Not 已初始化 OrElse IsDisposed Then Return
        UltraDetailListView1.BeginUpdate()
        Ultra2DChart1.BeginUpdate()
        Try
            Ultra2DChart1.ClearData()
            Ultra2DChart1.LegendPosition = Ultra2DChart.LegendPositionEnum.None
            Ultra2DChart1.XAxisTitle = If(精度 = "每帧", "帧序号", "时间戳")
            Ultra2DChart1.YAxisTitle = If(精度 = "每秒", "码率（kbit/s）", "区间总码量（kbit）")
            Dim entries = UltraDetailListView1.Items.Select(Function(i) DirectCast(i.Tag, 文件数据)).ToArray()
            Dim frameMode = 精度 = "每帧"
            Dim keys As Long()
            If frameMode Then
                Dim frameCount = entries.Where(Function(d) d.结果 IsNot Nothing).Select(Function(d) d.结果.点.Count).DefaultIfEmpty(0).Max()
                keys = Enumerable.Range(0, frameCount).Select(Function(i) CLng(i)).ToArray()
            Else
                keys = entries.Where(Function(d) d.结果 IsNot Nothing).SelectMany(Function(d) d.结果.点).
                    Select(Function(p) 时间键(p.时间秒)).Distinct().OrderBy(Function(t) t).ToArray()
            End If
            Dim categoryIndices = keys.Select(Function(key, index) (Key:=key, Index:=index)).ToDictionary(Function(p) p.Key, Function(p) p.Index)
            Dim count = keys.Length
            Ultra2DChart1.SetCategories(keys.Select(Function(key) If(frameMode, (key + 1).ToString(CultureInfo.InvariantCulture), 格式化时间戳(key / 1000000.0))).ToArray())
            For Each item In UltraDetailListView1.Items
                Dim data = DirectCast(item.Tag, 文件数据)
                更新列表项(item, data)
                If data.结果 Is Nothing Then Continue For
                Dim values = Enumerable.Repeat(Double.NaN, count).ToArray()
                For i = 0 To data.结果.点.Count - 1
                    Dim index = If(frameMode, i, categoryIndices(时间键(data.结果.点(i).时间秒)))
                    values(index) = data.结果.点(i).千比特
                Next
                Dim series = Ultra2DChart1.AddSeries(Path.GetFileName(data.路径), Ultra2DChart.ChartSeriesTypeEnum.Line, values)
                series.XAxisRepresentationMode = Ultra2DChart.XAxisRepresentationModeEnum.Interval
                series.Tag = data
                For i = 0 To data.结果.点.Count - 1
                    Dim index = If(frameMode, i, categoryIndices(时间键(data.结果.点(i).时间秒)))
                    series.Points(index).Tag = i
                Next
                series.Color = 取颜色(data)
                series.MarkerShape = Ultra2DChart.MarkerShapeEnum.None
                series.ShowValueLabels = Ultra2DChart.SeriesValueLabelModeEnum.Hide
                series.LineThickness = If(界面主题_v6.当前为浅色模式, 2.0F, 1.0F)
                series.ConnectMissingValues = Not frameMode
            Next
            If resetView Then
                Ultra2DChart1.ResetXAxisView()
                Ultra2DChart1.YAxisRangeMode = Ultra2DChart.AxisRangeModeEnum.Auto
                Ultra2DChart1.YAxisIncludeZero = True
            End If
        Finally
            Ultra2DChart1.EndUpdate()
            UltraDetailListView1.EndUpdate()
        End Try
        调整布局()
        Ultra2DChart1.RefreshChart()
    End Sub

    Private Sub 更新列表项(item As UltraDetailListView.ListItem, data As 文件数据)
        item.SubItems(0).ForeColor = 取颜色(data)
        item.SubItems(1).Text = If(data.信息 Is Nothing, data.状态, String.Join(", ", data.信息.流.Where(Function(s) data.已选.Contains(s.索引)).Select(Function(s) s.选择符.Replace("0:", ""))))
        If item.SubItems(1).Text = "" Then item.SubItems(1).Text = "未选择"
        Dim result = data.结果
        item.SubItems(2).Text = If(result IsNot Nothing, 格式化时长(result.时长秒), If(data.信息 IsNot Nothing, 格式化时长(data.信息.时长秒), "—"))
        item.SubItems(3).Text = If(data.当前帧.HasValue, 格式化帧数(data.当前帧.GetValueOrDefault()), If(result IsNot Nothing, 格式化帧数(result.帧数), "—"))
        item.SubItems(4).Text = If(data.当前帧.HasValue, "", If(result Is Nothing, data.状态, 格式化码率(result.平均Kbps)))
        item.SubItems(5).Text = If(result IsNot Nothing, 格式化码率(result.最低Kbps), "—")
        item.SubItems(6).Text = If(result IsNot Nothing, 格式化码率(result.最高Kbps), "—")
        item.BottomLines.Clear()
        If data.错误 <> "" Then item.BottomLines.Add(New UltraDetailListView.TextLine With {.Text = data.错误, .ForeColor = Color.IndianRed})
        item.InvalidateCache()
    End Sub
    Private Function 格式化悬停(series As Ultra2DChart.ChartSeries, index As Integer, value As Double) As String
        Dim data = TryCast(series.Tag, 文件数据)
        If data?.结果 Is Nothing OrElse index < 0 OrElse index >= series.Points.Count OrElse series.Points(index).Tag Is Nothing Then Return Nothing
        index = CInt(series.Points(index).Tag)
        Dim point = data.结果.点(index)
        Dim lines As New List(Of String) From {$"文件：{data.路径}"}
        Select Case 精度
            Case "每帧"
                lines.Add($"第 {index + 1} 帧")
            Case "每关键帧"
                lines.Add($"第 {index + 1} 个 GOP（{point.帧数} 帧）")
        End Select
        If data.结果.包含音频 Then lines.Add($"音频包 {point.音频包数} 个")
        lines.Add($"时间戳：{格式化时间戳(point.时间秒)} ～ {格式化时间戳(point.结束秒)}")
        lines.Add(If(精度 = "每秒", $"真实码率：{value:0.###} kbit/s", $"区间总码量：{value:0.###} kbit"))
        Return String.Join(vbLf, lines)
    End Function
    Private Shared Function 时间键(seconds As Double) As Long
        Return CLng(Math.Round(seconds * 1000000))
    End Function
    Private Shared Function 格式化码率(value As Double) As String
        Return If(Double.IsFinite(value), value.ToString("0.###", CultureInfo.InvariantCulture) & " kbps", "—")
    End Function
    Private Shared Function 格式化帧数(value As Long) As String
        Dim digits = value.ToString(CultureInfo.InvariantCulture)
        Dim firstDigit = If(digits.StartsWith("-", StringComparison.Ordinal), 1, 0)
        Dim formatted As New StringBuilder(digits.Length + digits.Length \ 4)
        For index = 0 To digits.Length - 1
            If index >= firstDigit AndAlso index > firstDigit AndAlso (digits.Length - index) Mod 4 = 0 Then formatted.Append(","c)
            formatted.Append(digits(index))
        Next
        Return formatted.ToString()
    End Function
    Private Shared Function 格式化时长(value As Double) As String
        If Not Double.IsFinite(value) OrElse value < 0 OrElse value >= TimeSpan.MaxValue.TotalSeconds Then Return "未知"
        Return 格式化时间戳(value)
    End Function
    Private Shared Function 格式化时间戳(value As Double) As String
        If Not Double.IsFinite(value) OrElse Math.Abs(value) >= TimeSpan.MaxValue.TotalSeconds Then Return "未知"
        Dim t = TimeSpan.FromSeconds(Math.Abs(value))
        Return $"{If(value < 0, "-", "")}{CLng(Math.Floor(t.TotalHours)):00}:{t.Minutes:00}:{t.Seconds:00}.{t.Milliseconds:000}"
    End Function

    Private Sub 排序改变(sender As Object, e As EventArgs) Handles UltraDetailListView1.ItemOrderChanged
        刷新呈现(False)
    End Sub
    Private Sub 移动点击(sender As Object, e As EventArgs) Handles MB_上移.Click, MB_下移.Click
        If 正在工作 Then Return
        Dim selected As New HashSet(Of UltraDetailListView.ListItem)(UltraDetailListView1.SelectedItems)
        Dim direction = If(sender Is MB_上移, -1, 1)
        Dim indices = If(direction < 0, Enumerable.Range(1, Math.Max(0, UltraDetailListView1.Items.Count - 1)), Enumerable.Range(0, Math.Max(0, UltraDetailListView1.Items.Count - 1)).Reverse())
        UltraDetailListView1.BeginUpdate()
        Try
            For Each i In indices
                Dim item = UltraDetailListView1.Items(i)
                If Not selected.Contains(item) OrElse selected.Contains(UltraDetailListView1.Items(i + direction)) Then Continue For
                UltraDetailListView1.Items.RemoveAt(i)
                UltraDetailListView1.Items.Insert(i + direction, item)
                item.Selected = True
            Next
        Finally
            UltraDetailListView1.EndUpdate()
        End Try
        刷新呈现(False)
    End Sub
    Private Sub 移除点击(sender As Object, e As EventArgs) Handles MB_移除选中.Click, MB_移除全部.Click
        If 正在工作 Then Return
        If sender Is MB_移除全部 Then
            UltraDetailListView1.Items.Clear()
            下个颜色序号 = 0
        Else
            For Each item In UltraDetailListView1.SelectedItems.ToArray()
                UltraDetailListView1.Items.Remove(item)
            Next
        End If
        刷新呈现(True)
    End Sub
    Private Sub 清除点击(sender As Object, e As EventArgs) Handles MB_清除统计.Click, MB_清除全部.Click
        If 正在工作 Then Return
        Dim items = If(sender Is MB_清除全部, UltraDetailListView1.Items.ToArray(), UltraDetailListView1.SelectedItems.ToArray())
        For Each item In items
            Dim data = DirectCast(item.Tag, 文件数据)
            data.包 = Nothing
            data.结果 = Nothing
            data.错误 = ""
            data.当前帧 = Nothing
            data.状态 = "未分析"
        Next
        刷新呈现(True)
    End Sub
    Private Sub 列表按键(sender As Object, e As KeyEventArgs) Handles UltraDetailListView1.KeyDown
        If e.KeyCode = Keys.Delete AndAlso Not 正在工作 Then
            移除点击(MB_移除选中, EventArgs.Empty)
            e.Handled = True
            e.SuppressKeyPress = True
        End If
    End Sub
    Private Sub 重置图表(sender As Object, e As MouseEventArgs) Handles Ultra2DChart1.MouseDoubleClick
        刷新呈现(True)
    End Sub
    Private Sub 布局变化(sender As Object, e As EventArgs) Handles UltraDetailListView1.SizeChanged, UltraDetailListView1.DpiChangedAfterParent, UltraDetailListView1.FontChanged
        If 已初始化 Then 调整布局()
    End Sub
    Private Sub 调整布局()
        Dim scale = UltraDetailListView1.DeviceDpi / 96.0
        Dim height = CInt(列表设计高度 * scale)
        If UltraDetailListView1.Height <> height Then UltraDetailListView1.Height = height
        Dim available = UltraDetailListView1.ClientSize.Width - UltraDetailListView1.Padding.Horizontal - CInt(30 * scale)
        If available <= 280 OrElse UltraDetailListView1.Columns.Count < 7 Then Return
        Dim total = 0
        For column = 1 To 6
            Dim width = TextRenderer.MeasureText(UltraDetailListView1.Columns(column).Text, UltraDetailListView1.Font, Size.Empty, TextFormatFlags.NoPadding Or TextFormatFlags.SingleLine).Width + CInt(20 * scale)
            For Each item In UltraDetailListView1.Items
                width = Math.Max(width, TextRenderer.MeasureText(item.SubItems(column).Text, UltraDetailListView1.Font, Size.Empty, TextFormatFlags.NoPadding Or TextFormatFlags.SingleLine).Width + CInt(20 * scale))
            Next
            width = Math.Clamp(width, CInt(48 * scale), CInt(150 * scale))
            UltraDetailListView1.Columns(column).Width = width
            total += width
        Next
        UltraDetailListView1.Columns(0).Width = Math.Max(CInt(180 * scale), available - total)
    End Sub
    Private Sub 关闭中(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        工作取消?.Cancel()
    End Sub
    Private Sub 已关闭(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        RemoveHandler 界面主题_v6.主题已更改, AddressOf 主题变化
    End Sub
End Class
