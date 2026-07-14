Imports System.Globalization
Imports System.IO
Imports System.Text
Imports System.Text.Json
Imports System.Text.RegularExpressions
Imports System.Threading
Imports LakeUI

Public Class Form_v6_集成工具_质量评测

    Private Const 浏览本地模型项 As String = "浏览本地模型文件 ..."
    Private Const 文件列 As Integer = 0
    Private Const PSNR列 As Integer = 1
    Private Const SSIM列 As Integer = 2
    Private Const VMAF列 As Integer = 3
    Private Const XPSNR列 As Integer = 4

    Private ReadOnly 进程锁 As New Object()
    Private 当前进程 As Process = Nothing
    Private 取消令牌源 As CancellationTokenSource = Nothing
    Private 正在评测 As Boolean = False
    Private 最后动态图表刷新Tick As Long = 0
    Private ReadOnly 评测记录锁 As New Object()
    Private 当前评测记录 As StringBuilder = Nothing
    Private 最后评测记录 As String = ""
    Private 正在选择本地模型 As Boolean = False

    Private Enum 指标类型
        PSNR
        SSIM
        VMAF
        XPSNR
    End Enum

    Private Enum 覆盖已有成绩决策
        取消任务
        覆盖
        不覆盖
    End Enum

    Private Shared ReadOnly 全部指标 As 指标类型() = {指标类型.PSNR, 指标类型.SSIM, 指标类型.VMAF, 指标类型.XPSNR}
    ' 当两路像素格式不一致时，统一到常见且受全部四个滤镜支持的 10-bit 4:2:0 格式。
    ' 选择 4:2:0 而非 4:4:4 是为了让原始色度采样率不成为评分差异；同时保留 10-bit 精度。
    Private Const 统一评测像素格式 As String = "yuv420p10le"

    Private Class 评测文件数据
        Public Property 文件路径 As String = ""
        Public Property 状态 As String = "未评测"
        Public Property 指标结果 As New Dictionary(Of 指标类型, 指标结果数据)
        Public Property 最近错误 As String = ""
    End Class

    Private Class 指标结果数据
        Public Property 成功 As Boolean = False
        Public Property 汇总值 As Double = Double.NaN
        Public Property 每帧数据 As New List(Of Double)
        Public Property 已处理帧数 As Integer = 0
        Public Property 错误信息 As String = ""
        Public ReadOnly Property 数据锁 As New Object()
    End Class

    Private Class 进程运行结果
        Public Property ExitCode As Integer = -1
        Public Property Output As String = ""
    End Class

    Private Class 视频流信息
        Public Property 宽度 As Integer = 0
        Public Property 高度 As Integer = 0
        Public Property 像素格式 As String = ""
        Public Property 帧率 As String = ""
        Public Property 色彩范围 As String = ""
        Public Property 色彩矩阵 As String = ""
        Public Property 色彩原色 As String = ""
        Public Property 色彩传递 As String = ""
        Public Property 色度位置 As String = ""
    End Class

    Private Class 图表条目
        Public Property 文件名 As String = ""
        Public Property 每帧数据 As New List(Of Double)
        Public Property 成功 As Boolean = False
        Public Property 汇总值 As Double = Double.NaN
    End Class

    Private Sub Form_v6_集成工具_质量评测_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        初始化控件()
        绑定文件拖入(Me, False)
        绑定文件拖入(ModernPanel1, False)
        绑定文件拖入(Panel1, True)
        绑定文件拖入(MTB_原视频文件路径, True)
        绑定文件拖入(UltraDetailListView1, False)
        绑定文件拖入(Panel6, False)
        调整列表交互区域()
        调整列宽()
        调整底部按钮布局()
    End Sub

    Private Sub 初始化控件()
        UltraDetailListView1.MultiSelect = True
        UltraDetailListView1.AllowDragReorder = True
        For Each column In UltraDetailListView1.Columns
            column.WordWrapHeightFixed = True
        Next
        If UltraDetailListView1.Columns.Count > VMAF列 Then UltraDetailListView1.Columns(VMAF列).Text = "VMAF"

        For Each metric In 全部指标
            Dim checkBox = 获取指标复选框(metric)
            If checkBox Is Nothing Then Continue For
            checkBox.Checked = True
            checkBox.ClickAnywhere = True
        Next

        填充下拉框(MCB_模型选择,
              "",
              浏览本地模型项,
              "vmaf_v0.6.1",
              "vmaf_v0.6.1neg",
              "vmaf_4k_v0.6.1")
        填充下拉框(MCB_Pooling, "", "mean", "harmonic_mean", "min")
        填充下拉框(MCB_SubSample, "", "1", "2", "3", "5", "10", "15")
        绑定路径下拉框拖拽(MCB_模型选择, 路径下拉框拖拽模式.文件路径,
                         Sub(combo, path) 添加本地Vmaf模型(path))
    End Sub

    Private Shared Sub 填充下拉框(combo As ModernComboBox, ParamArray values() As String)
        If combo Is Nothing OrElse values Is Nothing Then Exit Sub
        combo.Items.Clear()
        For Each value In values
            combo.Items.Add(value)
        Next
        If combo.Items.Count > 0 Then combo.SelectedIndex = 0
    End Sub

    Private Sub MCB_模型选择_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_模型选择.SelectedIndexChanged
        If 正在选择本地模型 Then Exit Sub

        Select Case MCB_模型选择.Text.Trim()
            Case ""
                清空Vmaf附加选项()
            Case 浏览本地模型项
                选择本地Vmaf模型()
        End Select
    End Sub

    Private Sub 选择本地Vmaf模型()
        正在选择本地模型 = True
        Try
            Using dialog As New OpenFileDialog With {.Multiselect = False, .Filter = "VMAF 模型 JSON 文件|*.json|所有文件|*.*"}
                If dialog.ShowDialog(Me) = DialogResult.OK Then
                    添加本地Vmaf模型(dialog.FileName)
                Else
                    MCB_模型选择.SelectedIndex = 0
                    MCB_模型选择.Text = ""
                    清空Vmaf附加选项()
                End If
            End Using
        Finally
            正在选择本地模型 = False
        End Try
    End Sub

    Private Sub 添加本地Vmaf模型(path As String)
        Dim modelPath = If(path, "").Trim()
        If Not 是否有效本地Vmaf模型(modelPath) Then
            MCB_模型选择.SelectedIndex = 0
            MCB_模型选择.Text = ""
            ExFloatingTip(MCB_模型选择, "请选择 VMAF 模型 JSON 文件", 1800)
            Exit Sub
        End If

        For i = 0 To MCB_模型选择.Items.Count - 1
            If String.Equals(CStr(MCB_模型选择.Items(i)), modelPath, StringComparison.OrdinalIgnoreCase) Then
                MCB_模型选择.SelectedIndex = i
                Exit Sub
            End If
        Next

        MCB_模型选择.Items.Add(modelPath)
        MCB_模型选择.SelectedIndex = MCB_模型选择.Items.Count - 1
    End Sub

    Private Shared Function 是否有效本地Vmaf模型(path As String) As Boolean
        Return Not String.IsNullOrWhiteSpace(path) AndAlso
               String.Equals(System.IO.Path.GetExtension(path), ".json", StringComparison.OrdinalIgnoreCase) AndAlso
               File.Exists(path)
    End Function

    Private Sub 清空Vmaf附加选项()
        MCB_Pooling.SelectedIndex = If(MCB_Pooling.Items.Count > 0, 0, -1)
        MCB_SubSample.SelectedIndex = If(MCB_SubSample.Items.Count > 0, 0, -1)
    End Sub

    Private Shared Sub 设置下拉框选中值(combo As ModernComboBox, value As String)
        If combo Is Nothing Then Exit Sub
        For i = 0 To combo.Items.Count - 1
            If String.Equals(combo.Items(i), value, StringComparison.OrdinalIgnoreCase) Then
                combo.SelectedIndex = i
                Exit Sub
            End If
        Next
    End Sub

    Private Sub 绑定文件拖入(target As Control, 写入原视频 As Boolean)
        If target Is Nothing Then Exit Sub
        target.AllowDrop = True
        RemoveHandler target.DragEnter, AddressOf 文件拖入事件
        RemoveHandler target.DragDrop, AddressOf 文件放下事件
        target.Tag = 写入原视频
        AddHandler target.DragEnter, AddressOf 文件拖入事件
        AddHandler target.DragDrop, AddressOf 文件放下事件
    End Sub

    Private Sub 文件拖入事件(sender As Object, e As DragEventArgs)
        e.Effect = If(获取拖入文件(e).Count > 0, DragDropEffects.Copy, DragDropEffects.None)
    End Sub

    Private Sub 文件放下事件(sender As Object, e As DragEventArgs)
        Dim files = 获取拖入文件(e)
        If files.Count = 0 Then Exit Sub
        Dim target = TryCast(sender, Control)
        If target IsNot Nothing AndAlso TypeOf target.Tag Is Boolean AndAlso CBool(target.Tag) Then
            MTB_原视频文件路径.Text = files(0)
        Else
            添加文件(files)
        End If
    End Sub

    Private Function 获取拖入文件(e As DragEventArgs) As List(Of String)
        If e Is Nothing OrElse e.Data Is Nothing OrElse Not e.Data.GetDataPresent(DataFormats.FileDrop) Then Return New List(Of String)
        Dim paths = TryCast(e.Data.GetData(DataFormats.FileDrop), String())
        If paths Is Nothing Then Return New List(Of String)
        Return 展开文件路径(paths)
    End Function

    Private Function 展开文件路径(paths As IEnumerable(Of String)) As List(Of String)
        Dim result As New List(Of String)
        If paths Is Nothing Then Return result

        For Each raw In paths
            If String.IsNullOrWhiteSpace(raw) Then Continue For
            Try
                If Directory.Exists(raw) Then
                    result.AddRange(Directory.GetFiles(raw, "*", SearchOption.TopDirectoryOnly).OrderBy(Function(x) x, StringComparer.CurrentCultureIgnoreCase))
                ElseIf File.Exists(raw) Then
                    result.Add(raw)
                End If
            Catch ex As Exception
                ExFloatingTip(UltraDetailListView1, $"读取路径失败：{ex.Message}", 2200)
            End Try
        Next

        Return result
    End Function

    Private Sub 添加文件(files As IEnumerable(Of String))
        If files Is Nothing Then Exit Sub
        Dim fileList = files.Where(Function(x) Not String.IsNullOrWhiteSpace(x) AndAlso File.Exists(x)).ToList()
        If fileList.Count = 0 Then Exit Sub

        UltraDetailListView1.BeginUpdate()
        Try
            For Each file In fileList
                If UltraDetailListView1.Items.Any(Function(item) String.Equals(获取项数据(item).文件路径, file, StringComparison.OrdinalIgnoreCase)) Then Continue For
                UltraDetailListView1.Items.Add(创建文件项(file))
            Next
        Finally
            UltraDetailListView1.EndUpdate()
        End Try

        If UltraDetailListView1.Items.Count > 0 Then UltraDetailListView1.SelectedIndex = UltraDetailListView1.Items.Count - 1
        调整列宽()
        刷新图表()
    End Sub

    Private Function 创建文件项(file As String) As UltraDetailListView.ListItem
        Dim data As New 评测文件数据 With {.文件路径 = file}
        Return New UltraDetailListView.ListItem(
            New UltraDetailListView.ListSubItem(Path.GetFileName(file)),
            New UltraDetailListView.ListSubItem("未评测"),
            New UltraDetailListView.ListSubItem("未评测"),
            New UltraDetailListView.ListSubItem("未评测"),
            New UltraDetailListView.ListSubItem("未评测")
        ) With {.Tag = data}
    End Function

    Private Function 获取项数据(item As UltraDetailListView.ListItem) As 评测文件数据
        If item Is Nothing Then Return New 评测文件数据()
        Dim data = TryCast(item.Tag, 评测文件数据)
        If data IsNot Nothing Then Return data
        data = New 评测文件数据 With {.文件路径 = If(item.SubItems.Count > 文件列, item.SubItems(文件列).Text, "")}
        item.Tag = data
        Return data
    End Function

    Private Sub MB_选择原视频_Click(sender As Object, e As EventArgs) Handles MB_选择原视频.Click
        Using d As New OpenFileDialog With {.Multiselect = False, .Filter = "所有文件|*.*"}
            If d.ShowDialog(Me) = DialogResult.OK Then MTB_原视频文件路径.Text = d.FileName
        End Using
    End Sub

    Private Async Sub MB_开始评测_Click(sender As Object, e As EventArgs) Handles MB_开始评测.Click
        If 正在评测 Then
            取消当前评测()
            Exit Sub
        End If
        If Not 验证评测参数() Then
            Exit Sub
        End If

        Dim metrics = 获取选中指标()
        Dim itemsToRun = 获取待评测项()
        Dim overwriteDecision = 获取覆盖已有成绩决策(itemsToRun, metrics)
        If overwriteDecision = 覆盖已有成绩决策.取消任务 Then Exit Sub

        开始新的评测记录(metrics, itemsToRun)
        正在评测 = True
        取消令牌源 = New CancellationTokenSource()
        设置运行状态(True)
        Dim runStatus = "完成"
        Try
            Await 开始评测Async(取消令牌源.Token, metrics, itemsToRun, overwriteDecision)
        Catch ex As OperationCanceledException
            runStatus = "已取消"
            追加评测记录("评测已取消")
            标记未完成项为已取消(metrics)
        Catch ex As Exception
            runStatus = "失败"
            追加评测记录($"评测异常：{ex.Message}")
        Finally
            结束当前评测记录(runStatus, metrics, itemsToRun)
            清理当前进程()
            取消令牌源?.Dispose()
            取消令牌源 = Nothing
            正在评测 = False
            设置运行状态(False)
            调整列宽()
            刷新图表()
        End Try
    End Sub

    Private Function 验证评测参数() As Boolean
        If String.IsNullOrWhiteSpace(MTB_原视频文件路径.Text) Then
            ExFloatingTip(MTB_原视频文件路径, "请先选择原视频", 1800)
            Return False
        End If
        If Not File.Exists(MTB_原视频文件路径.Text.Trim()) Then
            ExFloatingTip(MTB_原视频文件路径, "原视频文件不存在", 1800)
            Return False
        End If
        If UltraDetailListView1.Items.Count = 0 Then
            ExFloatingTip(UltraDetailListView1, "请先添加对比文件", 1800)
            Return False
        End If
        If 获取选中指标().Count = 0 Then
            ExFloatingTip(Panel3, "请至少选择一个评测项目", 1800)
            Return False
        End If
        If 获取选中指标().Contains(指标类型.VMAF) Then
            Dim selectedModel = 获取下拉框文本(MCB_模型选择, "")
            If selectedModel.EndsWith(".json", StringComparison.OrdinalIgnoreCase) AndAlso Not 是否有效本地Vmaf模型(selectedModel) Then
                ExFloatingTip(MCB_模型选择, "本地 VMAF 模型文件不存在或不是 JSON 文件", 2200)
                Return False
            End If
        End If
        For Each item In UltraDetailListView1.Items
            Dim pathValue = 获取项数据(item).文件路径
            If Not File.Exists(pathValue) Then
                ExFloatingTip(UltraDetailListView1, $"文件不存在：{pathValue}", 2600)
                Return False
            End If
        Next
        If Not 验证时间文本(MTB_评测时长.Text, "评测长度", MTB_评测时长) Then Return False
        If Not 验证时间文本(MTB_从头开始.Text, "开始时间", MTB_从头开始) Then Return False
        Return True
    End Function

    Private Function 验证时间文本(value As String, label As String, target As Control) As Boolean
        Dim text = If(value, "").Trim()
        If text = "" Then Return True
        Dim seconds As Double
        If 尝试解析时间秒数(text, seconds) Then Return True
        ExFloatingTip(target, $"{label}格式无效，可填写秒数、分:秒 或 时:分:秒", 2200)
        Return False
    End Function

    Private Function 获取选中指标() As List(Of 指标类型)
        Return 全部指标.
            Where(Function(metric)
                      Dim checkBox = 获取指标复选框(metric)
                      Return checkBox IsNot Nothing AndAlso checkBox.Checked
                  End Function).
            ToList()
    End Function

    Private Function 获取评测起始索引() As Integer
        If UltraDetailListView1.SelectedItems.Count = 0 Then Return 0

        For i = 0 To UltraDetailListView1.Items.Count - 1
            For Each selectedItem In UltraDetailListView1.SelectedItems
                If Object.ReferenceEquals(selectedItem, UltraDetailListView1.Items(i)) Then Return i
            Next
        Next
        Return 0
    End Function

    Private Function 获取待评测项() As List(Of UltraDetailListView.ListItem)
        Dim result As New List(Of UltraDetailListView.ListItem)
        If UltraDetailListView1.Items.Count = 0 Then Return result

        For i = 获取评测起始索引() To UltraDetailListView1.Items.Count - 1
            result.Add(UltraDetailListView1.Items(i))
        Next
        Return result
    End Function

    Private Shared Function 指标已有成绩(data As 评测文件数据, metric As 指标类型) As Boolean
        If data Is Nothing Then Return False
        Dim result As 指标结果数据 = Nothing
        Return data.指标结果.TryGetValue(metric, result) AndAlso result IsNot Nothing AndAlso result.成功
    End Function

    <CodeAnalysis.SuppressMessage("Performance", "CA1861:不要将常量数组作为参数", Justification:="<挂起>")>
    Private Function 获取覆盖已有成绩决策(items As List(Of UltraDetailListView.ListItem), metrics As List(Of 指标类型)) As 覆盖已有成绩决策
        Dim overwriteCount As Integer = 0
        获取即将覆盖成绩签名(items, metrics, overwriteCount)
        If overwriteCount = 0 Then Return 覆盖已有成绩决策.覆盖

        Dim result = ExFloatingBox(
            MB_开始评测,
            $"检测到 {overwriteCount} 个已勾选项目已经有成绩。请选择是否覆盖已有对应成绩；点其他地方将取消本次任务。",
            {"确认覆盖", "不覆盖"},
            MsgBoxStyle.Question,
            1)
        Select Case result
            Case 0
                Return 覆盖已有成绩决策.覆盖
            Case 1
                Return 覆盖已有成绩决策.不覆盖
            Case Else
                Return 覆盖已有成绩决策.取消任务
        End Select
    End Function

    Private Function 获取即将覆盖成绩签名(items As IEnumerable(Of UltraDetailListView.ListItem), metrics As IEnumerable(Of 指标类型), ByRef overwriteCount As Integer) As String
        overwriteCount = 0
        Dim result As New List(Of String)
        If items Is Nothing OrElse metrics Is Nothing Then Return ""

        Dim metricList = metrics.ToList()
        For Each item In items
            Dim data = 获取项数据(item)
            For Each metric In metricList
                If Not 指标已有成绩(data, metric) Then Continue For
                result.Add($"{data.文件路径}{vbTab}{metric}")
                overwriteCount += 1
            Next
        Next

        Return String.Join(vbLf, result)
    End Function

    Private Sub 开始新的评测记录(metrics As IEnumerable(Of 指标类型), items As IEnumerable(Of UltraDetailListView.ListItem))
        Dim sb As New StringBuilder()
        sb.AppendLine("3FUI 质量评测记录")
        sb.AppendLine($"开始时间：{DateTime.Now:yyyy-MM-dd HH:mm:ss}")
        sb.AppendLine($"原视频：{MTB_原视频文件路径.Text.Trim()}")
        sb.AppendLine($"评测项目：{String.Join(", ", metrics.Select(Function(metric) 获取指标名称(metric)))}")
        sb.AppendLine($"开始时间参数：{If(String.IsNullOrWhiteSpace(MTB_从头开始.Text), "未设置", MTB_从头开始.Text.Trim())}")
        sb.AppendLine($"评测长度参数：{If(String.IsNullOrWhiteSpace(MTB_评测时长.Text), "完整全片", MTB_评测时长.Text.Trim())}")
        sb.AppendLine("待评测文件：")
        For Each item In items
            sb.AppendLine($"  {获取项数据(item).文件路径}")
        Next
        sb.AppendLine()

        SyncLock 评测记录锁
            当前评测记录 = sb
            最后评测记录 = ""
        End SyncLock
    End Sub

    Private Sub 追加评测记录(text As String)
        SyncLock 评测记录锁
            If 当前评测记录 Is Nothing Then Exit Sub
            当前评测记录.AppendLine(text)
        End SyncLock
    End Sub

    Private Sub 追加FFmpeg输出记录(line As String)
        If line Is Nothing Then Exit Sub
        追加评测记录(line)
    End Sub

    Private Sub 追加最终成绩记录(metrics As IEnumerable(Of 指标类型), items As IEnumerable(Of UltraDetailListView.ListItem))
        追加评测记录("")
        追加评测记录("最终成绩")
        For Each item In items
            Dim data = 获取项数据(item)
            追加评测记录($"文件：{data.文件路径}")
            追加评测记录($"状态：{data.状态}")
            For Each metric In metrics
                Dim result As 指标结果数据 = Nothing
                If data.指标结果.TryGetValue(metric, result) AndAlso result IsNot Nothing AndAlso result.成功 Then
                    追加评测记录($"  {获取指标名称(metric)}：{格式化分数(result.汇总值, metric)}")
                ElseIf data.指标结果.TryGetValue(metric, result) AndAlso result IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(result.错误信息) Then
                    追加评测记录($"  {获取指标名称(metric)}：失败，{result.错误信息}")
                Else
                    追加评测记录($"  {获取指标名称(metric)}：未评测")
                End If
            Next
        Next
    End Sub

    Private Sub 结束当前评测记录(status As String, metrics As IEnumerable(Of 指标类型), items As IEnumerable(Of UltraDetailListView.ListItem))
        If 当前评测记录 Is Nothing Then Exit Sub
        追加评测记录("")
        追加评测记录($"结束时间：{DateTime.Now:yyyy-MM-dd HH:mm:ss}")
        追加评测记录($"结束状态：{status}")
        追加最终成绩记录(metrics, items)
        SyncLock 评测记录锁
            最后评测记录 = 当前评测记录.ToString()
            当前评测记录 = Nothing
        End SyncLock
    End Sub

    Private Async Function 开始评测Async(token As CancellationToken, metrics As List(Of 指标类型), itemsToRun As List(Of UltraDetailListView.ListItem), overwriteDecision As 覆盖已有成绩决策) As Task
        Dim reference = MTB_原视频文件路径.Text.Trim()
        Dim startTime = MTB_从头开始.Text.Trim()
        Dim duration = MTB_评测时长.Text.Trim()
        Dim referenceInfo = Await 获取视频流信息Async(reference, token)
        Dim overwriteExisting = overwriteDecision = 覆盖已有成绩决策.覆盖

        For Each item In itemsToRun
            Dim data = 获取项数据(item)
            data.状态 = "等待"
            data.最近错误 = ""
            For Each metric In metrics
                If overwriteExisting OrElse Not 指标已有成绩(data, metric) Then 设置指标文本(item, metric, "等待")
            Next
        Next
        刷新全部评分颜色()

        For Each item In itemsToRun.ToArray()
            token.ThrowIfCancellationRequested()
            Dim data = 获取项数据(item)
            data.状态 = "处理中"
            data.最近错误 = ""
            刷新列表项显示()
            Dim distortedInfo = Await 获取视频流信息Async(data.文件路径, token)

            For Each metric In metrics
                token.ThrowIfCancellationRequested()
                If Not overwriteExisting AndAlso 指标已有成绩(data, metric) Then
                    恢复指标文本(item, metric)
                    追加评测记录("")
                    追加评测记录($"文件：{data.文件路径}")
                    追加评测记录($"项目：{获取指标名称(metric)}")
                    追加评测记录("跳过：已有成绩，用户选择不覆盖")
                    Continue For
                End If

                设置指标文本(item, metric, "处理中")
                刷新列表项显示()
                Dim result As New 指标结果数据()
                data.指标结果(metric) = result
                刷新图表()
                Await 运行单项指标Async(reference, data.文件路径, metric, startTime, duration, referenceInfo, distortedInfo, token, item, result)
                If result.成功 Then
                    设置指标文本(item, metric, 格式化分数(result.汇总值, metric))
                Else
                    data.最近错误 = result.错误信息
                    设置指标文本(item, metric, "失败")
                End If
                刷新评分颜色(metric)
                调整列宽()
                刷新图表()
            Next

            For Each metric In 全部指标
                If metrics.Contains(metric) Then Continue For
                Dim retained As 指标结果数据 = Nothing
                If data.指标结果.TryGetValue(metric, retained) AndAlso retained.成功 Then
                    设置指标文本(item, metric, 格式化分数(retained.汇总值, metric))
                ElseIf Not data.指标结果.ContainsKey(metric) Then
                    设置指标文本(item, metric, "未评测")
                End If
            Next
            data.状态 = If(data.指标结果.Values.Any(Function(x) x.成功), "完成", "失败")
            刷新列表项显示()
        Next
        刷新全部评分颜色()
    End Function

    Private Async Function 运行单项指标Async(reference As String, distorted As String, metric As 指标类型, startTime As String, duration As String, referenceInfo As 视频流信息, distortedInfo As 视频流信息, token As CancellationToken, item As UltraDetailListView.ListItem, result As 指标结果数据) As Task(Of 指标结果数据)
        Dim tempPath = Path.Combine(Path.GetTempPath(), $"3fui_quality_{Guid.NewGuid():N}_{metric.ToString().ToLowerInvariant()}.log")
        Dim vmafPool = If(metric = 指标类型.VMAF, 获取下拉框文本(MCB_Pooling, "mean"), "")
        Dim arguments = 构建指标命令(reference, distorted, metric, startTime, duration, tempPath, referenceInfo, distortedInfo)
        追加评测记录("")
        追加评测记录($"文件：{distorted}")
        追加评测记录($"项目：{获取指标名称(metric)}")
        追加评测记录($"命令行：{获取FFmpeg文件名()} {arguments}")
        追加评测记录("FFmpeg 输出：")
        Dim pollCts As CancellationTokenSource = Nothing
        Dim pollTask As Task = Task.CompletedTask
        Dim canceledException As OperationCanceledException = Nothing
        Try
            If metric <> 指标类型.VMAF Then
                pollCts = CancellationTokenSource.CreateLinkedTokenSource(token)
                pollTask = 轮询Stats文件Async(tempPath, metric, item, result, pollCts.Token)
            End If

            Dim processResult = Await 运行FFmpegAsync(arguments, token,
                Sub(line)
                    追加FFmpeg输出记录(line)
                    处理FFmpeg实时输出(line, metric, item, result)
                End Sub)
            If token.IsCancellationRequested Then
                result.错误信息 = "已取消"
            ElseIf processResult.ExitCode <> 0 Then
                result.错误信息 = 提取错误信息(processResult.Output)
            Else
                Dim parsed = 解析指标结果(metric, tempPath, processResult.Output, vmafPool)
                If Not parsed.成功 AndAlso String.IsNullOrWhiteSpace(parsed.错误信息) Then parsed.错误信息 = "未能解析评测结果"
                覆盖指标结果(result, parsed)
            End If
            If result.成功 Then
                追加评测记录($"本项结果：{格式化分数(result.汇总值, metric)}")
            ElseIf Not String.IsNullOrWhiteSpace(result.错误信息) Then
                追加评测记录($"本项结果：失败，{result.错误信息}")
            Else
                追加评测记录("本项结果：失败")
            End If
        Catch ex As OperationCanceledException
            result.错误信息 = "已取消"
            追加评测记录("本项结果：已取消")
            canceledException = ex
        Catch ex As Exception
            result.错误信息 = ex.Message
            追加评测记录($"本项结果：失败，{ex.Message}")
        Finally
            If pollCts IsNot Nothing Then pollCts.Cancel()
        End Try

        Try
            Await pollTask
        Catch
        End Try

        If pollCts IsNot Nothing Then pollCts.Dispose()
        If canceledException IsNot Nothing Then Throw canceledException

        Try
            If File.Exists(tempPath) Then File.Delete(tempPath)
        Catch
        End Try

        Return result
    End Function

    Private Sub 覆盖指标结果(target As 指标结果数据, source As 指标结果数据)
        If target Is Nothing OrElse source Is Nothing Then Exit Sub
        SyncLock target.数据锁
            target.成功 = source.成功
            target.汇总值 = source.汇总值
            target.每帧数据 = New List(Of Double)(source.每帧数据)
            target.已处理帧数 = Math.Max(source.已处理帧数, target.每帧数据.Count)
            target.错误信息 = source.错误信息
        End SyncLock
    End Sub

    Private Async Function 轮询Stats文件Async(statsPath As String, metric As 指标类型, item As UltraDetailListView.ListItem, result As 指标结果数据, token As CancellationToken) As Task
        Do While Not token.IsCancellationRequested
            Try
                If File.Exists(statsPath) Then
                    Dim parsed = 解析运行中Stats(metric, statsPath)
                    If parsed.每帧数据.Count > 0 Then
                        SyncLock result.数据锁
                            result.每帧数据 = New List(Of Double)(parsed.每帧数据)
                            result.已处理帧数 = Math.Max(result.已处理帧数, result.每帧数据.Count)
                        End SyncLock
                        更新运行中显示(item, metric, result)
                    End If
                End If
            Catch
            End Try
            Await Task.Delay(500, token)
        Loop
    End Function

    Private Function 解析运行中Stats(metric As 指标类型, path As String) As 指标结果数据
        Select Case metric
            Case 指标类型.PSNR
                Return 解析键值Stats(指标类型.PSNR, path, "", "psnr_avg", "average:")
            Case 指标类型.SSIM
                Return 解析键值Stats(指标类型.SSIM, path, "", "All", "All:")
            Case 指标类型.XPSNR
                Return 解析XpsnrStats(path, "")
            Case Else
                Return New 指标结果数据()
        End Select
    End Function

    Private Sub 处理FFmpeg实时输出(line As String, metric As 指标类型, item As UltraDetailListView.ListItem, result As 指标结果数据)
        If String.IsNullOrWhiteSpace(line) Then Exit Sub
        Dim frame = 提取进度帧数(line)
        If frame <= 0 Then Exit Sub
        SyncLock result.数据锁
            If frame > result.已处理帧数 Then result.已处理帧数 = frame
        End SyncLock
        更新运行中显示(item, metric, result)
    End Sub

    Private Sub 更新运行中显示(item As UltraDetailListView.ListItem, metric As 指标类型, result As 指标结果数据)
        Dim frameCount As Integer
        SyncLock result.数据锁
            frameCount = Math.Max(result.已处理帧数, result.每帧数据.Count)
        End SyncLock

        界面线程执行(Sub()
                   If item IsNot Nothing AndAlso UltraDetailListView1.Items.Contains(item) Then
                       设置指标文本(item, metric, $"帧 {frameCount}")
                       刷新列表项显示()
                   End If
                   请求动态图表刷新()
               End Sub)
    End Sub

    Private Sub 请求动态图表刷新()
        Dim nowTicks = Environment.TickCount64
        If nowTicks - 最后动态图表刷新Tick < 700 Then Exit Sub
        最后动态图表刷新Tick = nowTicks
        刷新图表()
    End Sub

    Private Shared Function 提取进度帧数(line As String) As Integer
        Dim m = Regex.Match(line, "^\s*frame\s*=\s*(?<value>\d+)", RegexOptions.IgnoreCase Or RegexOptions.CultureInvariant)
        If Not m.Success Then Return 0
        Dim value As Integer
        If Integer.TryParse(m.Groups("value").Value, NumberStyles.Integer, CultureInfo.InvariantCulture, value) Then Return value
        Return 0
    End Function

    Private Function 构建指标命令(reference As String, distorted As String, metric As 指标类型, startTime As String, duration As String, tempPath As String, referenceInfo As 视频流信息, distortedInfo As 视频流信息) As String
        Dim distortedFrameRate = 获取FFMetrics帧率参数(distortedInfo)
        Dim referenceFrameRate = 获取FFMetrics帧率参数(referenceInfo)
        Dim durationFrameRate = If(String.IsNullOrWhiteSpace(distortedFrameRate), referenceFrameRate, distortedFrameRate)
        Dim arg As New StringBuilder("-hide_banner -nostdin -probesize 50M ")
        追加FFMetrics输入参数(arg, distorted, distortedFrameRate)
        追加FFMetrics输入参数(arg, reference, referenceFrameRate)

        Dim frameLimit = 获取FFMetrics帧数限制(duration, durationFrameRate)
        If frameLimit > 0 Then arg.Append("-frames:v ").Append(frameLimit.ToString(CultureInfo.InvariantCulture)).Append(" "c)

        Dim filter As String
        Dim finalPixelFormat = 获取指标像素格式(referenceInfo, distortedInfo)
        Dim model = If(metric = 指标类型.VMAF, 获取下拉框文本(MCB_模型选择, "vmaf_v0.6.1"), "")
        Dim targetSize = 获取评测目标尺寸(metric, referenceInfo, model)
        Dim scaleDistorted = 构建视频预处理滤镜(distortedInfo, referenceInfo, targetSize.Width, targetSize.Height, finalPixelFormat)
        Dim scaleReference = 构建参考预处理滤镜(metric, referenceInfo, targetSize, finalPixelFormat)
        Dim trim = 构建FFMetrics剪辑滤镜(startTime)
        Dim dist = $"[0:v]{trim}settb=AVTB,setpts=PTS-STARTPTS{scaleDistorted}[dist];"
        Dim ref = $"[1:v]{trim}settb=AVTB,setpts=PTS-STARTPTS{scaleReference}[ref];"
        Select Case metric
            Case 指标类型.PSNR
                filter = $"{dist}{ref}[dist][ref]psnr=eof_action=endall:stats_file={引用过滤器参数(tempPath)}"
            Case 指标类型.SSIM
                filter = $"{dist}{ref}[dist][ref]ssim=eof_action=endall:stats_file={引用过滤器参数(tempPath)}"
            Case 指标类型.XPSNR
                filter = $"{dist}{ref}[ref][dist]xpsnr=eof_action=endall:stats_file={引用过滤器参数(tempPath)}"
            Case 指标类型.VMAF
                Dim pool = 获取下拉框文本(MCB_Pooling, "mean")
                Dim subsample = 获取下拉框文本(MCB_SubSample, "0")
                Dim threads = Math.Max(1, Environment.ProcessorCount - 1)
                Dim subsampleOption = 构建Vmaf子采样参数(subsample)
                Dim modelOption = 构建Vmaf模型参数(model)
                filter = $"{dist}{ref}[dist][ref]libvmaf=eof_action=endall:log_fmt=json:log_path={引用过滤器参数(tempPath)}:n_threads={threads.ToString(CultureInfo.InvariantCulture)}{subsampleOption}:pool={转义过滤器值(pool)}:model={转义过滤器值(modelOption)}"
            Case Else
                Throw New InvalidOperationException("未知评测项目")
        End Select

        arg.Append("-lavfi ").Append(引用参数(filter)).Append(" -f null -")
        Return arg.ToString()
    End Function

    Private Shared Sub 追加FFMetrics输入参数(arg As StringBuilder, file As String, inputFrameRate As String)
        If Not String.IsNullOrWhiteSpace(inputFrameRate) Then arg.Append("-r ").Append(引用参数(inputFrameRate.Trim())).Append(" "c)
        arg.Append("-i ").Append(引用参数(file)).Append(" "c)
    End Sub

    Private Shared Function 构建FFMetrics剪辑滤镜(startTime As String) As String
        Dim seekTime = 格式化FFmpeg时间参数(startTime)
        If String.IsNullOrWhiteSpace(seekTime) OrElse seekTime = "0" Then Return ""
        Return $"trim=start={seekTime},"
    End Function

    Private Shared Function 构建Vmaf子采样参数(subsample As String) As String
        Dim value As Integer
        If Not Integer.TryParse(If(subsample, "").Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, value) OrElse value <= 0 Then Return ""
        Return $":n_subsample={value.ToString(CultureInfo.InvariantCulture)}"
    End Function

    Private Shared Function 构建Vmaf模型参数(model As String) As String
        Dim value = If(model, "").Trim()
        If value.EndsWith(".json", StringComparison.OrdinalIgnoreCase) Then Return "path=" & value
        Return "version=" & value
    End Function

    Private Async Function 获取视频流信息Async(file As String, token As CancellationToken) As Task(Of 视频流信息)
        Dim result As New 视频流信息()
        If String.IsNullOrWhiteSpace(file) Then Return result
        Try
            Dim args = $"-v error -select_streams v:0 -show_entries stream=width,height,pix_fmt,avg_frame_rate,r_frame_rate,color_range,color_space,color_primaries,color_transfer,chroma_location -of json {引用参数(file)}"
            Dim probeResult = Await 运行FFprobeAsync(args, token)
            If probeResult.ExitCode <> 0 OrElse String.IsNullOrWhiteSpace(probeResult.Output) Then Return result

            Using doc = JsonDocument.Parse(probeResult.Output)
                Dim streams As JsonElement
                If Not doc.RootElement.TryGetProperty("streams", streams) OrElse streams.ValueKind <> JsonValueKind.Array OrElse streams.GetArrayLength() = 0 Then Return result

                Dim stream = streams.EnumerateArray().First()
                Dim width As JsonElement
                If stream.TryGetProperty("width", width) AndAlso width.ValueKind = JsonValueKind.Number Then width.TryGetInt32(result.宽度)
                Dim height As JsonElement
                If stream.TryGetProperty("height", height) AndAlso height.ValueKind = JsonValueKind.Number Then height.TryGetInt32(result.高度)
                Dim pixFmt As JsonElement
                If stream.TryGetProperty("pix_fmt", pixFmt) AndAlso pixFmt.ValueKind = JsonValueKind.String Then result.像素格式 = 清理像素格式(pixFmt.GetString())
                Dim avgFrameRate As JsonElement
                If stream.TryGetProperty("avg_frame_rate", avgFrameRate) AndAlso avgFrameRate.ValueKind = JsonValueKind.String Then result.帧率 = 清理帧率(avgFrameRate.GetString())
                If String.IsNullOrWhiteSpace(result.帧率) Then
                    Dim rFrameRate As JsonElement
                    If stream.TryGetProperty("r_frame_rate", rFrameRate) AndAlso rFrameRate.ValueKind = JsonValueKind.String Then result.帧率 = 清理帧率(rFrameRate.GetString())
                End If
                Dim colorRange As JsonElement
                If stream.TryGetProperty("color_range", colorRange) AndAlso colorRange.ValueKind = JsonValueKind.String Then result.色彩范围 = 清理色彩值(colorRange.GetString())
                Dim colorSpace As JsonElement
                If stream.TryGetProperty("color_space", colorSpace) AndAlso colorSpace.ValueKind = JsonValueKind.String Then result.色彩矩阵 = 清理色彩值(colorSpace.GetString())
                Dim colorPrimaries As JsonElement
                If stream.TryGetProperty("color_primaries", colorPrimaries) AndAlso colorPrimaries.ValueKind = JsonValueKind.String Then result.色彩原色 = 清理色彩值(colorPrimaries.GetString())
                Dim colorTransfer As JsonElement
                If stream.TryGetProperty("color_transfer", colorTransfer) AndAlso colorTransfer.ValueKind = JsonValueKind.String Then result.色彩传递 = 清理色彩值(colorTransfer.GetString())
                Dim chromaLocation As JsonElement
                If stream.TryGetProperty("chroma_location", chromaLocation) AndAlso chromaLocation.ValueKind = JsonValueKind.String Then result.色度位置 = 清理色彩值(chromaLocation.GetString())
            End Using
        Catch ex As OperationCanceledException
            Throw
        Catch
        End Try
        Return result
    End Function

    Private Async Function 运行FFprobeAsync(arguments As String, token As CancellationToken) As Task(Of 进程运行结果)
        Using process As New Process()
            process.StartInfo.FileName = 获取FFprobe文件名()
            process.StartInfo.WorkingDirectory = If(设置_v6.实例对象.工作目录 <> "", 设置_v6.实例对象.工作目录, "")
            process.StartInfo.Arguments = arguments
            process.StartInfo.UseShellExecute = False
            process.StartInfo.RedirectStandardOutput = True
            process.StartInfo.RedirectStandardError = True
            process.StartInfo.StandardOutputEncoding = Encoding.UTF8
            process.StartInfo.StandardErrorEncoding = Encoding.UTF8
            process.StartInfo.CreateNoWindow = True

            Using registration = token.Register(Sub()
                                                    Try
                                                        If Not process.HasExited Then process.Kill()
                                                    Catch
                                                    End Try
                                                End Sub)
                process.Start()
                Dim stdoutTask = process.StandardOutput.ReadToEndAsync(token)
                Dim stderrTask = process.StandardError.ReadToEndAsync(token)
                Await process.WaitForExitAsync(token)
                Dim stdout = Await stdoutTask
                Dim stderr = Await stderrTask
                Return New 进程运行结果 With {.ExitCode = process.ExitCode, .Output = stdout & vbCrLf & stderr}
            End Using
        End Using
    End Function

    Private Shared Function 获取FFprobe文件名() As String
        Dim custom = If(设置_v6.实例对象.替代进程文件名, "").Trim()
        If custom <> "" Then
            Dim fileName = Path.GetFileName(custom)
            If fileName.Contains("ffmpeg", StringComparison.OrdinalIgnoreCase) Then
                Dim probeName = Regex.Replace(fileName, "ffmpeg", "ffprobe", RegexOptions.IgnoreCase)
                Dim directory = Path.GetDirectoryName(custom)
                If Not String.IsNullOrWhiteSpace(directory) Then
                    Dim candidate = Path.Combine(directory, probeName)
                    If File.Exists(candidate) Then Return candidate
                    Return "ffprobe"
                End If
                If 查找可执行文件(probeName) <> "" Then Return probeName
            End If
        End If
        Return "ffprobe"
    End Function

    Private Shared Function 查找可执行文件(fileName As String) As String
        Dim name = If(fileName, "").Trim()
        If name = "" Then Return ""

        Dim searchDirs As New List(Of String)
        If Not String.IsNullOrWhiteSpace(Application.StartupPath) Then searchDirs.Add(Application.StartupPath)

        Dim pathValue = Environment.GetEnvironmentVariable("PATH")
        If Not String.IsNullOrWhiteSpace(pathValue) Then
            searchDirs.AddRange(pathValue.Split(Path.PathSeparator, StringSplitOptions.RemoveEmptyEntries))
        End If

        For Each rawDir In searchDirs
            Dim dir = If(rawDir, "").Trim().Trim(""""c)
            If dir = "" Then Continue For
            Try
                Dim candidate = Path.Combine(dir, name)
                If File.Exists(candidate) Then Return candidate
            Catch
            End Try
        Next

        Return ""
    End Function

    Private Shared Function 获取指标像素格式(referenceInfo As 视频流信息, distortedInfo As 视频流信息) As String
        Dim referenceFormat = 清理像素格式(If(referenceInfo?.像素格式, ""))
        Dim distortedFormat = 清理像素格式(If(distortedInfo?.像素格式, ""))

        ' 无需转换时不插入 format 滤镜，保留 FFMetrics 在同格式素材上的原始计分结果。
        If referenceFormat <> "" AndAlso String.Equals(referenceFormat, distortedFormat, StringComparison.OrdinalIgnoreCase) Then Return ""
        Return 统一评测像素格式
    End Function

    Private Shared Function 获取FFMetrics帧率参数(info As 视频流信息) As String
        Dim rate = 解析帧率值(If(info?.帧率, ""))
        If rate <= 0 Then Return ""
        Return rate.ToString("0.##", CultureInfo.InvariantCulture)
    End Function

    Private Shared Function 获取FFMetrics帧数限制(duration As String, frameRateText As String) As Integer
        Dim seconds = 解析秒数(duration)
        Dim frameRate = 解析帧率值(frameRateText)
        If seconds <= 0 OrElse frameRate <= 0 Then Return 0
        Return Math.Max(1, CInt(Math.Ceiling(seconds * frameRate)))
    End Function

    Private Shared Function 解析秒数(value As String) As Double
        Dim text = If(value, "").Trim()
        If text = "" Then Return 0
        Dim seconds As Double
        If 尝试解析时间秒数(text, seconds) Then Return Math.Max(0, seconds)
        Return 0
    End Function

    Private Shared Function 格式化FFmpeg时间参数(value As String) As String
        Dim seconds As Double
        If Not 尝试解析时间秒数(value, seconds) Then Return ""
        Return seconds.ToString("0.###", CultureInfo.InvariantCulture)
    End Function

    Private Shared Function 尝试解析时间秒数(text As String, ByRef seconds As Double) As Boolean
        seconds = 0
        text = If(text, "").Trim()
        If text = "" Then Return False

        Dim number As Double
        If Double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, number) Then
            If number < 0 Then Return False
            seconds = number
            Return True
        End If

        Dim parts = text.Split(":"c)
        If parts.Length <> 2 AndAlso parts.Length <> 3 Then Return False

        Dim values As New List(Of Double)
        For Each part In parts
            Dim value As Double
            If Not Double.TryParse(part.Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, value) OrElse value < 0 Then Return False
            values.Add(value)
        Next

        If parts.Length = 2 Then
            seconds = values(0) * 60 + values(1)
        Else
            seconds = values(0) * 3600 + values(1) * 60 + values(2)
        End If
        Return True
    End Function

    Private Shared Function 解析帧率值(value As String) As Double
        Dim text = If(value, "").Trim()
        If text = "" OrElse text = "0/0" OrElse text.Equals("N/A", StringComparison.OrdinalIgnoreCase) Then Return 0
        Dim m = Regex.Match(text, "^(?<num>\d+(?:\.\d+)?)/(?<den>\d+(?:\.\d+)?)$", RegexOptions.CultureInvariant)
        If m.Success Then
            Dim num As Double
            Dim den As Double
            If Double.TryParse(m.Groups("num").Value, NumberStyles.Float, CultureInfo.InvariantCulture, num) AndAlso
               Double.TryParse(m.Groups("den").Value, NumberStyles.Float, CultureInfo.InvariantCulture, den) AndAlso den <> 0 Then
                Return num / den
            End If
        End If
        Dim result As Double
        If Double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, result) Then Return result
        Return 0
    End Function

    Private Shared Function 获取评测目标尺寸(metric As 指标类型, referenceInfo As 视频流信息, model As String) As Size
        If metric = 指标类型.VMAF Then
            Dim modelSize = 获取Vmaf模型尺寸(model)
            If modelSize.Width > 0 AndAlso modelSize.Height > 0 AndAlso Vmaf需要缩放到模型尺寸(referenceInfo, modelSize) Then Return modelSize
        End If
        Return New Size(If(referenceInfo Is Nothing, 0, referenceInfo.宽度), If(referenceInfo Is Nothing, 0, referenceInfo.高度))
    End Function

    Private Shared Function 获取Vmaf模型尺寸(model As String) As Size
        Dim text = model.ToLowerInvariant()
        If text.Contains("4k") Then Return New Size(3840, 2160)
        Return New Size(1920, 1080)
    End Function

    Private Shared Function Vmaf需要缩放到模型尺寸(referenceInfo As 视频流信息, modelSize As Size) As Boolean
        If referenceInfo Is Nothing OrElse referenceInfo.宽度 <= 0 OrElse referenceInfo.高度 <= 0 Then Return True
        If modelSize.Width <= 0 OrElse modelSize.Height <= 0 Then Return False
        Dim widthDiff = Math.Abs(referenceInfo.宽度 - modelSize.Width) / CDbl(modelSize.Width)
        Dim heightDiff = Math.Abs(referenceInfo.高度 - modelSize.Height) / CDbl(modelSize.Height)
        Return widthDiff > 0.1 OrElse heightDiff > 0.1
    End Function

    Private Shared Function 清理像素格式(value As String) As String
        Dim text = If(value, "").Trim()
        If text = "" OrElse text.Equals("unknown", StringComparison.OrdinalIgnoreCase) Then Return ""
        If Not Regex.IsMatch(text, "^[A-Za-z0-9_]+$", RegexOptions.CultureInvariant) Then Return ""
        Return text
    End Function

    Private Shared Function 清理帧率(value As String) As String
        Dim text = If(value, "").Trim()
        If text = "" OrElse text = "0/0" OrElse text = "N/A" Then Return ""
        If Regex.IsMatch(text, "^\d+/\d+$", RegexOptions.CultureInvariant) Then Return text
        If Regex.IsMatch(text, "^\d+(?:\.\d+)?$", RegexOptions.CultureInvariant) Then Return text
        Return ""
    End Function

    Private Shared Function 清理色彩值(value As String) As String
        Dim text = If(value, "").Trim().ToLowerInvariant()
        If text = "" OrElse text = "unknown" OrElse text = "unspecified" OrElse text = "reserved" Then Return ""
        If Not Regex.IsMatch(text, "^[a-z0-9_\-]+$", RegexOptions.CultureInvariant) Then Return ""
        Return text
    End Function

    Private Shared Function 构建参考预处理滤镜(metric As 指标类型, referenceInfo As 视频流信息, targetSize As Size, pixelFormat As String) As String
        If metric = 指标类型.VMAF Then Return 构建视频预处理滤镜(referenceInfo, referenceInfo, targetSize.Width, targetSize.Height, pixelFormat)
        Return 构建像素格式滤镜(pixelFormat)
    End Function

    Private Shared Function 构建视频预处理滤镜(sourceInfo As 视频流信息, referenceInfo As 视频流信息, targetWidth As Integer, targetHeight As Integer, pixelFormat As String) As String
        Dim hasScale = targetWidth > 0 AndAlso targetHeight > 0 AndAlso
                       (sourceInfo Is Nothing OrElse sourceInfo.宽度 <= 0 OrElse sourceInfo.高度 <= 0 OrElse
                        sourceInfo.宽度 <> targetWidth OrElse sourceInfo.高度 <> targetHeight)

        Dim filter As New StringBuilder()
        If hasScale Then filter.Append($",scale={targetWidth}:{targetHeight}:flags=bicubic")
        filter.Append(构建像素格式滤镜(pixelFormat))
        Return filter.ToString()
    End Function

    Private Shared Function 构建像素格式滤镜(pixelFormat As String) As String
        Dim text = 清理像素格式(pixelFormat)
        If text = "" Then Return ""
        Return $",format={text}"
    End Function

    Private Function 获取下拉框文本(combo As ModernComboBox, fallback As String) As String
        If combo Is Nothing Then Return fallback
        If Not String.IsNullOrWhiteSpace(combo.Text) Then Return combo.Text.Trim()
        If combo.SelectedItem IsNot Nothing Then Return combo.SelectedItem
        Return fallback
    End Function

    Private Shared Function 获取FFmpeg文件名() As String
        Return If(设置_v6.实例对象.替代进程文件名 <> "", 设置_v6.实例对象.替代进程文件名, "ffmpeg")
    End Function

    Private Async Function 运行FFmpegAsync(arguments As String, token As CancellationToken, onLine As Action(Of String)) As Task(Of 进程运行结果)
        Dim tcs As New TaskCompletionSource(Of Integer)(TaskCreationOptions.RunContinuationsAsynchronously)
        Dim stdoutClosed As New TaskCompletionSource(Of Boolean)(TaskCreationOptions.RunContinuationsAsynchronously)
        Dim stderrClosed As New TaskCompletionSource(Of Boolean)(TaskCreationOptions.RunContinuationsAsynchronously)
        Dim output As New StringBuilder()
        Dim process As New Process()
        process.StartInfo.FileName = 获取FFmpeg文件名()
        process.StartInfo.WorkingDirectory = If(设置_v6.实例对象.工作目录 <> "", 设置_v6.实例对象.工作目录, "")
        process.StartInfo.Arguments = arguments
        process.StartInfo.UseShellExecute = False
        process.StartInfo.RedirectStandardOutput = True
        process.StartInfo.RedirectStandardError = True
        process.StartInfo.StandardOutputEncoding = Encoding.UTF8
        process.StartInfo.StandardErrorEncoding = Encoding.UTF8
        process.StartInfo.CreateNoWindow = True
        process.EnableRaisingEvents = True
        AddHandler process.OutputDataReceived, Sub(sender, e)
                                                   If e.Data Is Nothing Then
                                                       stdoutClosed.TrySetResult(True)
                                                       Return
                                                   End If
                                                   SyncLock output
                                                       output.AppendLine(e.Data)
                                                   End SyncLock
                                                   onLine?.Invoke(e.Data)
                                               End Sub
        AddHandler process.ErrorDataReceived, Sub(sender, e)
                                                  If e.Data Is Nothing Then
                                                      stderrClosed.TrySetResult(True)
                                                      Return
                                                  End If
                                                  SyncLock output
                                                      output.AppendLine(e.Data)
                                                  End SyncLock
                                                  onLine?.Invoke(e.Data)
                                              End Sub
        AddHandler process.Exited, Sub()
                                       Dim code = -1
                                       Try
                                           code = process.ExitCode
                                       Catch
                                       End Try
                                       tcs.TrySetResult(code)
                                   End Sub

        SyncLock 进程锁
            当前进程 = process
        End SyncLock

        Using registration = token.Register(Sub()
                                                Try
                                                    If Not process.HasExited Then process.Kill()
                                                Catch
                                                End Try
                                                tcs.TrySetCanceled(token)
                                            End Sub)
            Try
                process.Start()
                process.BeginOutputReadLine()
                process.BeginErrorReadLine()
                Dim exitCode = Await tcs.Task
                Await Task.WhenAll(stdoutClosed.Task, stderrClosed.Task)
                Return New 进程运行结果 With {.ExitCode = exitCode, .Output = output.ToString()}
            Finally
                SyncLock 进程锁
                    If Object.ReferenceEquals(当前进程, process) Then 当前进程 = Nothing
                End SyncLock
                Try
                    process.Dispose()
                Catch
                End Try
            End Try
        End Using
    End Function

    Private Function 解析指标结果(metric As 指标类型, tempPath As String, output As String, vmafPool As String) As 指标结果数据
        Select Case metric
            Case 指标类型.PSNR
                Return 解析键值Stats(指标类型.PSNR, tempPath, output, "psnr_avg", "average:")
            Case 指标类型.SSIM
                Return 解析键值Stats(指标类型.SSIM, tempPath, output, "All", "All:")
            Case 指标类型.XPSNR
                Return 解析XpsnrStats(tempPath, output)
            Case 指标类型.VMAF
                Return 解析VMAFJson(tempPath, vmafPool)
            Case Else
                Return New 指标结果数据 With {.错误信息 = "未知评测项目"}
        End Select
    End Function

    Private Function 解析键值Stats(metric As 指标类型, path As String, output As String, frameKey As String, summaryKey As String) As 指标结果数据
        Dim result As New 指标结果数据()
        If File.Exists(path) Then
            For Each line In File.ReadLines(path)
                Dim value = 提取键值(line, frameKey)
                添加规范化帧值(result.每帧数据, metric, value)
            Next
        End If

        result.汇总值 = 获取汇总值或帧平均值(提取摘要值(output, summaryKey), result.每帧数据, True, False)
        result.成功 = Not Double.IsNaN(result.汇总值)
        Return result
    End Function

    Private Function 解析XpsnrStats(path As String, output As String) As 指标结果数据
        Dim result As New 指标结果数据()
        If File.Exists(path) Then
            For Each line In File.ReadLines(path)
                Dim value = 提取Xpsnr帧值(line)
                添加规范化帧值(result.每帧数据, 指标类型.XPSNR, value)
            Next
        End If

        result.汇总值 = 获取汇总值或帧平均值(提取Xpsnr摘要值(output), result.每帧数据, True, True)
        result.成功 = Not Double.IsNaN(result.汇总值)
        Return result
    End Function

    Private Function 解析VMAFJson(path As String, pool As String) As 指标结果数据
        Dim result As New 指标结果数据()
        If Not File.Exists(path) Then
            result.错误信息 = "VMAF 日志文件不存在"
            Return result
        End If

        Using doc = JsonDocument.Parse(File.ReadAllText(path, Encoding.UTF8))
            Dim root = doc.RootElement
            Dim frames As JsonElement
            If root.TryGetProperty("frames", frames) Then
                For Each frame In frames.EnumerateArray()
                    Dim metrics As JsonElement
                    If frame.TryGetProperty("metrics", metrics) Then
                        Dim value = 获取Json数字(metrics, "vmaf")
                        添加规范化帧值(result.每帧数据, 指标类型.VMAF, value)
                    End If
                Next
            End If

            Dim pooled As JsonElement
            If root.TryGetProperty("pooled_metrics", pooled) Then
                Dim vmaf As JsonElement
                If pooled.TryGetProperty("vmaf", vmaf) Then
                    Dim poolKeys = If(String.IsNullOrWhiteSpace(pool),
                                      {"mean", "harmonic_mean", "min"},
                                      {pool.Trim(), "mean", "harmonic_mean", "min"})
                    For Each key In poolKeys.Distinct(StringComparer.OrdinalIgnoreCase)
                        Dim value = 获取Json数字(vmaf, key)
                        If Not Double.IsNaN(value) Then
                            result.汇总值 = value
                            Exit For
                        End If
                    Next
                End If
            End If
        End Using

        result.汇总值 = 获取汇总值或帧平均值(result.汇总值, result.每帧数据, False, False)
        result.成功 = Not Double.IsNaN(result.汇总值)
        Return result
    End Function

    Private Shared Function 获取汇总值或帧平均值(summary As Double, frameValues As List(Of Double), fallbackWhenInfinite As Boolean, allowPositiveInfinityFallback As Boolean) As Double
        If frameValues Is Nothing OrElse frameValues.Count = 0 Then Return summary
        If Not Double.IsNaN(summary) AndAlso (Not fallbackWhenInfinite OrElse Not Double.IsInfinity(summary)) Then Return summary

        Dim finite = frameValues.Where(Function(v) Not Double.IsNaN(v) AndAlso Not Double.IsInfinity(v)).ToList()
        If finite.Count > 0 Then Return finite.Average()
        If allowPositiveInfinityFallback AndAlso frameValues.Any(Function(v) Double.IsPositiveInfinity(v)) Then Return Double.PositiveInfinity
        Return summary
    End Function

    Private Shared Function 获取Json数字(element As JsonElement, propertyName As String) As Double
        Dim value As JsonElement
        If element.TryGetProperty(propertyName, value) AndAlso value.ValueKind = JsonValueKind.Number Then
            Dim number As Double
            If value.TryGetDouble(number) Then Return number
        End If
        Return Double.NaN
    End Function

    Private Shared Function 提取键值(line As String, key As String) As Double
        If String.IsNullOrWhiteSpace(line) Then Return Double.NaN
        Dim pattern = Regex.Escape(key) & "[:=](?<value>[+\-]?(?:Infinity|inf|nan|\d+(?:\.\d+)?))"
        Dim match = Regex.Match(line, pattern, RegexOptions.IgnoreCase Or RegexOptions.CultureInvariant)
        If Not match.Success Then Return Double.NaN
        Return 解析浮点(match.Groups("value").Value)
    End Function

    Private Shared Function 提取摘要值(text As String, key As String) As Double
        If String.IsNullOrWhiteSpace(text) Then Return Double.NaN
        Dim pattern = Regex.Escape(key) & "\s*(?<value>[+\-]?(?:Infinity|inf|nan|\d+(?:\.\d+)?))"
        Dim matches = Regex.Matches(text, pattern, RegexOptions.IgnoreCase Or RegexOptions.CultureInvariant)
        If matches.Count = 0 Then Return Double.NaN
        Return 解析浮点(matches(matches.Count - 1).Groups("value").Value)
    End Function

    Private Shared Function 提取Xpsnr帧值(line As String) As Double
        If String.IsNullOrWhiteSpace(line) Then Return Double.NaN
        Return 计算Xpsnr加权值(提取Xpsnr通道值(line))
    End Function

    Private Shared Function 提取Xpsnr摘要值(output As String) As Double
        If String.IsNullOrWhiteSpace(output) Then Return Double.NaN
        Dim lines = output.Split({vbCrLf, vbLf}, StringSplitOptions.RemoveEmptyEntries)
        For i = lines.Length - 1 To 0 Step -1
            Dim value = 计算Xpsnr加权值(提取Xpsnr通道值(lines(i)))
            If Not Double.IsNaN(value) Then Return value
        Next
        Return Double.NaN
    End Function

    Private Shared Function 提取Xpsnr通道值(line As String) As Dictionary(Of String, Double)
        Dim result As New Dictionary(Of String, Double)(StringComparer.OrdinalIgnoreCase)
        If String.IsNullOrWhiteSpace(line) Then Return result

        For Each match As Match In Regex.Matches(line, "XPSNR\s+(?<channel>[a-z0-9]+):\s*(?<value>[+\-]?(?:Infinity|inf|nan|\d+(?:\.\d+)?))", RegexOptions.IgnoreCase Or RegexOptions.CultureInvariant)
            Dim channel = match.Groups("channel").Value
            Dim value = 解析浮点(match.Groups("value").Value)
            If Not Double.IsNaN(value) Then result(channel) = value
        Next

        ' FFmpeg 的最终汇总行只有首个通道带有 "XPSNR" 前缀，例如：
        ' "XPSNR y: 43.5788  u: 47.0558  v: 46.2689"。必须在该行中继续解析无前缀通道。
        If result.Count < 3 AndAlso Regex.IsMatch(line, "\bXPSNR\b", RegexOptions.IgnoreCase Or RegexOptions.CultureInvariant) Then
            For Each match As Match In Regex.Matches(line, "(?<channel>[yuvrgb])\s*:\s*(?<value>[+\-]?(?:Infinity|inf|nan|\d+(?:\.\d+)?))", RegexOptions.IgnoreCase Or RegexOptions.CultureInvariant)
                Dim channel = match.Groups("channel").Value
                Dim value = 解析浮点(match.Groups("value").Value)
                If Not Double.IsNaN(value) Then result(channel) = value
            Next
        End If
        Return result
    End Function

    Private Shared Function 计算Xpsnr加权值(values As Dictionary(Of String, Double)) As Double
        If values Is Nothing OrElse values.Count = 0 Then Return Double.NaN
        Dim y As Double
        If values.TryGetValue("y", y) Then
            Dim u As Double
            Dim v As Double
            If values.TryGetValue("u", u) AndAlso values.TryGetValue("v", v) Then Return (4 * y + u + v) / 6
            Return y
        End If

        Dim finite = values.Values.Where(Function(v) Not Double.IsNaN(v) AndAlso Not Double.IsInfinity(v)).ToList()
        If finite.Count > 0 Then Return finite.Average()
        If values.Values.Any(Function(v) Double.IsPositiveInfinity(v)) Then Return Double.PositiveInfinity
        Return Double.NaN
    End Function

    Private Shared Function 解析浮点(value As String) As Double
        If String.IsNullOrWhiteSpace(value) Then Return Double.NaN
        If value.Equals("inf", StringComparison.OrdinalIgnoreCase) OrElse value.Equals("infinity", StringComparison.OrdinalIgnoreCase) Then Return Double.PositiveInfinity
        If value.Equals("nan", StringComparison.OrdinalIgnoreCase) Then Return Double.NaN
        Dim result As Double
        If Double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, result) Then Return result
        Return Double.NaN
    End Function

    Private Shared Sub 添加规范化帧值(values As List(Of Double), metric As 指标类型, value As Double)
        If values Is Nothing OrElse Double.IsNaN(value) Then Exit Sub
        values.Add(规范化FFMetrics帧值(metric, value))
    End Sub

    Private Shared Function 规范化FFMetrics帧值(metric As 指标类型, value As Double) As Double
        Dim minimum = 0.0
        Dim maximum = If(metric = 指标类型.SSIM, 1.0, 100.0)
        If Double.IsPositiveInfinity(value) Then Return maximum
        If Double.IsNegativeInfinity(value) Then Return minimum
        Return Math.Max(minimum, Math.Min(maximum, value))
    End Function

    Private Shared Function 提取错误信息(output As String) As String
        If String.IsNullOrWhiteSpace(output) Then Return "FFmpeg 执行失败"
        Dim lines = output.Split({vbCrLf, vbLf}, StringSplitOptions.RemoveEmptyEntries).
            Select(Function(x) x.Trim()).
            Where(Function(x) x <> "").
            ToList()
        If lines.Count = 0 Then Return "FFmpeg 执行失败"
        Return lines(Math.Max(0, lines.Count - 1))
    End Function

    Private Shared Function 格式化分数(value As Double, metric As 指标类型) As String
        If Double.IsPositiveInfinity(value) Then Return "∞"
        If Double.IsNaN(value) Then Return "失败"
        Select Case metric
            Case 指标类型.SSIM
                Return value.ToString("0.000000", CultureInfo.InvariantCulture)
            Case Else
                Return value.ToString("0.000", CultureInfo.InvariantCulture)
        End Select
    End Function

    Private Sub 设置运行状态(running As Boolean)
        MB_开始评测.Text = If(running, "取消评测", "开始评测")
        MB_开始评测.ForeColor = If(running, Color.IndianRed, Color.YellowGreen)
        MB_选择原视频.Enabled = Not running
        MB_移除选中文件.Enabled = Not running
        MB_移除全部文件.Enabled = Not running
        MB_清除选中记录.Enabled = Not running
        MB_清除全部记录.Enabled = Not running
        MB_导出记录.Enabled = Not running
        UltraDetailListView1.AllowDragReorder = Not running
    End Sub

    Private Sub 取消当前评测()
        Try
            取消令牌源?.Cancel()
            SyncLock 进程锁
                If 当前进程 IsNot Nothing AndAlso Not 当前进程.HasExited Then 当前进程.Kill()
            End SyncLock
        Catch
        End Try
    End Sub

    Private Sub 清理当前进程()
        SyncLock 进程锁
            Try
                If 当前进程 IsNot Nothing AndAlso Not 当前进程.HasExited Then 当前进程.Kill()
                当前进程?.Dispose()
            Catch
            Finally
                当前进程 = Nothing
            End Try
        End SyncLock
    End Sub

    Private Sub 标记未完成项为已取消(metrics As IEnumerable(Of 指标类型))
        Dim metricList = If(metrics, Enumerable.Empty(Of 指标类型)()).ToList()
        For Each item In UltraDetailListView1.Items
            Dim data = 获取项数据(item)
            If data.状态 = "处理中" Then
                data.状态 = "已取消"
                data.最近错误 = "已取消"
                For Each metric In metricList
                    Dim result As 指标结果数据 = Nothing
                    If Not data.指标结果.TryGetValue(metric, result) OrElse result Is Nothing OrElse (Not result.成功 AndAlso String.IsNullOrWhiteSpace(result.错误信息)) OrElse String.Equals(result.错误信息, "已取消", StringComparison.Ordinal) Then
                        设置指标文本(item, metric, "已取消")
                    End If
                Next
            ElseIf data.状态 = "等待" Then
                data.状态 = If(data.指标结果.Values.Any(Function(x) x.成功), "完成", "未评测")
                For Each metric In metricList
                    恢复指标文本(item, metric)
                Next
            End If
        Next
    End Sub

    Private Sub 设置指标文本(item As UltraDetailListView.ListItem, metric As 指标类型, text As String)
        If item Is Nothing Then Exit Sub
        Dim index = 获取指标列(metric)
        If item.SubItems.Count > index Then
            item.SubItems(index).Text = text
            item.SubItems(index).ForeColor = Color.Empty
        End If
    End Sub

    Private Sub 恢复指标文本(item As UltraDetailListView.ListItem, metric As 指标类型)
        Dim data = 获取项数据(item)
        Dim result As 指标结果数据 = Nothing
        If data.指标结果.TryGetValue(metric, result) AndAlso result IsNot Nothing AndAlso result.成功 Then
            设置指标文本(item, metric, 格式化分数(result.汇总值, metric))
        Else
            设置指标文本(item, metric, "未评测")
        End If
    End Sub

    Private Sub 刷新全部评分颜色()
        For Each metric In 全部指标
            刷新评分颜色(metric, False)
        Next
        刷新列表项显示()
    End Sub

    Private Sub 刷新评分颜色(metric As 指标类型, Optional refreshList As Boolean = True)
        Dim index = 获取指标列(metric)
        Dim scoredItems As New List(Of (Item As UltraDetailListView.ListItem, Value As Double))

        For Each item In UltraDetailListView1.Items
            If item.SubItems.Count <= index Then Continue For
            item.SubItems(index).ForeColor = Color.Empty

            Dim data = 获取项数据(item)
            Dim result As 指标结果数据 = Nothing
            If Not data.指标结果.TryGetValue(metric, result) OrElse Not result.成功 Then Continue For

            Dim value As Double
            SyncLock result.数据锁
                value = result.汇总值
            End SyncLock
            If Double.IsNaN(value) Then Continue For
            scoredItems.Add((item, value))
        Next

        If scoredItems.Count = 1 Then
            scoredItems(0).Item.SubItems(index).ForeColor = Color.YellowGreen
        ElseIf scoredItems.Count > 1 Then
            Dim minValue = scoredItems.Min(Function(x) x.Value)
            Dim maxValue = scoredItems.Max(Function(x) x.Value)

            For Each entry In scoredItems
                Dim color As Color
                If entry.Value.Equals(maxValue) Then
                    color = Color.YellowGreen
                ElseIf entry.Value.Equals(minValue) Then
                    color = Color.IndianRed
                Else
                    color = 获取评分渐变颜色(entry.Value, minValue, maxValue)
                End If
                entry.Item.SubItems(index).ForeColor = color
            Next
        End If

        If refreshList Then 刷新列表项显示()
    End Sub

    Private Shared Function 获取评分渐变颜色(value As Double, minValue As Double, maxValue As Double) As Color
        If Double.IsInfinity(value) Then Return Color.YellowGreen
        If Double.IsInfinity(minValue) OrElse Double.IsInfinity(maxValue) OrElse maxValue <= minValue Then Return Color.Silver
        Dim ratio = Math.Max(0.0, Math.Min(1.0, (value - minValue) / (maxValue - minValue)))
        Dim r = CInt(205 + (154 - 205) * ratio)
        Dim g = CInt(92 + (205 - 92) * ratio)
        Dim b = CInt(92 + (50 - 92) * ratio)
        Return Color.FromArgb(r, g, b)
    End Function

    Private Shared Function 获取指标列(metric As 指标类型) As Integer
        Select Case metric
            Case 指标类型.PSNR
                Return PSNR列
            Case 指标类型.SSIM
                Return SSIM列
            Case 指标类型.VMAF
                Return VMAF列
            Case 指标类型.XPSNR
                Return XPSNR列
            Case Else
                Return PSNR列
        End Select
    End Function

    Private Function 获取指标复选框(metric As 指标类型) As ModernCheckBox
        Select Case metric
            Case 指标类型.PSNR
                Return MCB_PSNR
            Case 指标类型.SSIM
                Return MCB_SSIM
            Case 指标类型.VMAF
                Return MCB_VMAF
            Case 指标类型.XPSNR
                Return MCB_XPSNR
            Case Else
                Return Nothing
        End Select
    End Function

    Private Shared Function 获取指标名称(metric As 指标类型) As String
        Return metric.ToString()
    End Function

    Private Sub 刷新列表项显示()
        Try
            UltraDetailListView1.RefreshItems()
        Catch
            UltraDetailListView1.Invalidate()
        End Try
    End Sub

    Private Sub 刷新列表布局和图表()
        刷新全部评分颜色()
        调整列宽()
        刷新图表()
    End Sub

    Private Sub MB_导出记录_Click(sender As Object, e As EventArgs) Handles MB_导出记录.Click
        If 正在评测 Then
            ExFloatingTip(MB_导出记录, "评测结束后才能导出记录", 1800)
            Exit Sub
        End If
        If String.IsNullOrWhiteSpace(最后评测记录) Then
            ExFloatingTip(MB_导出记录, "没有可导出的最后一次评测记录", 1800)
            Exit Sub
        End If

        Using d As New SaveFileDialog With {
            .Filter = "文本文档|*.txt|所有文件|*.*",
            .FileName = $"quality_report_{DateTime.Now:yyyyMMdd_HHmmss}.txt"
        }
            If d.ShowDialog(Me) <> DialogResult.OK Then Exit Sub
            File.WriteAllText(d.FileName, 最后评测记录, New UTF8Encoding(False))
            ExFloatingTip(MB_导出记录, "已导出记录", 1200)
        End Using
    End Sub

    Private Sub MB_清除选中记录_Click(sender As Object, e As EventArgs) Handles MB_清除选中记录.Click
        For Each item In UltraDetailListView1.SelectedItems
            清除记录(item)
        Next
        刷新列表布局和图表()
    End Sub

    Private Sub MB_清除全部记录_Click(sender As Object, e As EventArgs) Handles MB_清除全部记录.Click
        For Each item In UltraDetailListView1.Items
            清除记录(item)
        Next
        刷新列表布局和图表()
    End Sub

    Private Sub 清除记录(item As UltraDetailListView.ListItem)
        Dim data = 获取项数据(item)
        data.状态 = "未评测"
        data.最近错误 = ""
        data.指标结果.Clear()
        For Each metric In 全部指标
            设置指标文本(item, metric, "未评测")
        Next
    End Sub

    Private Sub MB_移除选中文件_Click(sender As Object, e As EventArgs) Handles MB_移除选中文件.Click
        移除选中项()
    End Sub

    Private Sub MB_移除全部文件_Click(sender As Object, e As EventArgs) Handles MB_移除全部文件.Click
        If UltraDetailListView1.Items.Count = 0 Then Exit Sub
        UltraDetailListView1.Items.Clear()
        刷新列表布局和图表()
    End Sub

    Private Sub 移除选中项()
        Dim selected = UltraDetailListView1.SelectedItems
        If selected.Count = 0 Then Exit Sub
        UltraDetailListView1.BeginUpdate()
        Try
            For Each item In selected.ToArray()
                UltraDetailListView1.Items.Remove(item)
            Next
        Finally
            UltraDetailListView1.EndUpdate()
        End Try
        刷新列表布局和图表()
    End Sub

    Private Sub UltraDetailListView1_KeyDown(sender As Object, e As KeyEventArgs) Handles UltraDetailListView1.KeyDown
        If e.KeyCode = Keys.Delete AndAlso Not 正在评测 Then
            移除选中项()
            e.Handled = True
        End If
    End Sub

    Private Sub UltraDetailListView1_ItemDoubleClick(sender As Object, e As UltraDetailListView.ListItemEventArgs) Handles UltraDetailListView1.ItemDoubleClick
        复制鼠标位置分数(e)
    End Sub

    Private Sub 复制鼠标位置分数(e As UltraDetailListView.ListItemEventArgs)
        Dim item = If(e?.Item, UltraDetailListView1.SelectedItem)
        If item Is Nothing Then Exit Sub

        Dim metric As 指标类型
        If Not 尝试获取指标列类型(e.ColumnIndex, metric) Then Exit Sub

        Dim result As 指标结果数据 = Nothing
        If Not 获取项数据(item).指标结果.TryGetValue(metric, result) OrElse result Is Nothing OrElse Not result.成功 Then
            ExFloatingTip(UltraDetailListView1, "此位置没有可复制的分数", 1200)
            Exit Sub
        End If

        Dim scoreText = 格式化分数(result.汇总值, metric)
        If String.IsNullOrWhiteSpace(scoreText) Then
            ExFloatingTip(UltraDetailListView1, "此位置没有可复制的分数", 1200)
            Exit Sub
        End If

        Try
            Clipboard.SetText(scoreText)
            ExFloatingTip(UltraDetailListView1, $"已复制{获取指标名称(metric)}分数：{scoreText}", 1200)
        Catch ex As Exception
            ExFloatingTip(UltraDetailListView1, $"复制失败：{ex.Message}", 1800)
        End Try
    End Sub

    Private Shared Function 尝试获取指标列类型(columnIndex As Integer, ByRef metric As 指标类型) As Boolean
        Select Case columnIndex
            Case PSNR列
                metric = 指标类型.PSNR
            Case SSIM列
                metric = 指标类型.SSIM
            Case VMAF列
                metric = 指标类型.VMAF
            Case XPSNR列
                metric = 指标类型.XPSNR
            Case Else
                Return False
        End Select
        Return True
    End Function

    Private Sub UltraDetailListView1_ItemOrderChanged(sender As Object, e As EventArgs) Handles UltraDetailListView1.ItemOrderChanged
        调整列宽()
        刷新图表()
    End Sub

    Private Sub UltraDetailListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles UltraDetailListView1.SelectedIndexChanged
        刷新图表()
    End Sub

    Private Sub UltraDetailListView1_SizeChanged(sender As Object, e As EventArgs) Handles UltraDetailListView1.SizeChanged
        调整列表交互区域()
        调整列宽()
    End Sub

    Private Sub Panel6_SizeChanged(sender As Object, e As EventArgs) Handles Panel6.SizeChanged
        调整底部按钮布局()
    End Sub

    Private Sub 调整底部按钮布局()
        If Panel6 Is Nothing Then Exit Sub

        Dim buttons As Control() = {
            MB_开始评测,
            MB_清除选中记录,
            MB_清除全部记录,
            MB_移除选中文件,
            MB_移除全部文件,
            MB_导出记录,
            MB_图表窗口
        }
        Dim gaps As Control() = {
            JustEmptyControl6,
            JustEmptyControl3,
            JustEmptyControl4,
            JustEmptyControl7,
            JustEmptyControl8,
            JustEmptyControl9
        }
        If buttons.Any(Function(button) button Is Nothing) Then Exit Sub

        Panel6.SuspendLayout()
        Try
            Dim layoutOrder As Control() = {
                MB_开始评测,
                JustEmptyControl6,
                MB_清除选中记录,
                JustEmptyControl3,
                MB_清除全部记录,
                JustEmptyControl4,
                MB_移除选中文件,
                JustEmptyControl7,
                MB_移除全部文件,
                JustEmptyControl8,
                MB_导出记录,
                JustEmptyControl9,
                MB_图表窗口
            }
            For i = 0 To layoutOrder.Length - 1
                If layoutOrder(i) IsNot Nothing Then Panel6.Controls.SetChildIndex(layoutOrder(i), layoutOrder.Length - 1 - i)
            Next

            For Each button In buttons
                button.Dock = DockStyle.Left
            Next
            For Each gap In gaps
                If gap Is Nothing Then Continue For
                gap.Dock = DockStyle.Left
                gap.Width = CInt(10 * 获取Dpi比例())
            Next

            Dim gapWidth = gaps.Where(Function(gap) gap IsNot Nothing AndAlso gap.Visible).Sum(Function(gap) gap.Width)
            Dim available = Math.Max(0, Panel6.ClientSize.Width - Panel6.Padding.Left - Panel6.Padding.Right - gapWidth)
            Dim baseWidth = If(buttons.Length = 0, 0, available \ buttons.Length)
            Dim remainder = If(buttons.Length = 0, 0, available Mod buttons.Length)

            For i = 0 To buttons.Length - 1
                buttons(i).Width = baseWidth + If(i < remainder, 1, 0)
            Next
        Finally
            Panel6.ResumeLayout()
        End Try
    End Sub

    Private Sub 调整列表交互区域()
        UltraDetailListView1.DragSelectZoneWidth = Math.Max(0, UltraDetailListView1.Size.Width \ 2)
    End Sub

    Private Sub 调整列宽()
        If UltraDetailListView1.Columns.Count < 5 OrElse UltraDetailListView1.ClientSize.Width <= 0 Then Exit Sub
        Dim scale = 获取Dpi比例()
        Dim available = UltraDetailListView1.ClientSize.Width - UltraDetailListView1.Padding.Left - UltraDetailListView1.Padding.Right - CInt(30 * scale)
        If available <= 280 Then Exit Sub

        Dim scoreMin = CInt(80 * scale)
        Dim scoreMax = CInt(150 * scale)
        Dim scoreWidths = 全部指标.ToDictionary(
            Function(metric) metric,
            Function(metric) 限制宽度(测量列宽(获取指标列(metric)), scoreMin, scoreMax))
        Dim fileWidth = Math.Max(CInt(180 * scale), available - scoreWidths.Values.Sum())

        UltraDetailListView1.Columns(文件列).Width = fileWidth
        For Each metric In 全部指标
            UltraDetailListView1.Columns(获取指标列(metric)).Width = scoreWidths(metric)
        Next
    End Sub

    Private Function 测量列宽(columnIndex As Integer) As Integer
        Dim maxWidth = TextRenderer.MeasureText(UltraDetailListView1.Columns(columnIndex).Text, UltraDetailListView1.Font).Width + CInt(36 * 获取Dpi比例())
        For Each item In UltraDetailListView1.Items
            If item.SubItems.Count <= columnIndex Then Continue For
            Dim width = TextRenderer.MeasureText(item.SubItems(columnIndex).Text, UltraDetailListView1.Font).Width + CInt(36 * 获取Dpi比例())
            If width > maxWidth Then maxWidth = width
        Next
        Return maxWidth
    End Function

    Private Shared Function 限制宽度(value As Integer, min As Integer, max As Integer) As Integer
        Return Math.Max(min, Math.Min(max, value))
    End Function

    Private Function 获取Dpi比例() As Single
        Return CSng(DeviceDpi) / 96.0F
    End Function

    Private Sub MB_图表窗口_Click(sender As Object, e As EventArgs) Handles MB_图表窗口.Click
        显示图表窗口()
    End Sub

    Private Sub 显示图表窗口()
        Form_v6_集成工具_质量评测图表.显示(Me, AddressOf 获取图表序列, AddressOf 获取图表分数文本)
    End Sub

    Private Sub 刷新图表()
        Form_v6_集成工具_质量评测图表.刷新当前图表()
    End Sub

    Private Function 获取图表序列(metricName As String) As Dictionary(Of String, List(Of Double))
        Dim result As New Dictionary(Of String, List(Of Double))(StringComparer.CurrentCultureIgnoreCase)
        For Each entry In 获取图表条目(解析图表指标(metricName))
            If entry.每帧数据.Count = 0 Then Continue For
            result(entry.文件名) = entry.每帧数据
        Next
        Return result
    End Function

    Private Function 获取图表分数文本(metricName As String) As Dictionary(Of String, String)
        Dim metric = 解析图表指标(metricName)
        Dim result As New Dictionary(Of String, String)(StringComparer.CurrentCultureIgnoreCase)
        For Each entry In 获取图表条目(metric)
            If Not entry.成功 Then Continue For
            result(entry.文件名) = 格式化分数(entry.汇总值, metric)
        Next
        Return result
    End Function

    Private Function 获取图表条目(metric As 指标类型) As List(Of 图表条目)
        Dim result As New List(Of 图表条目)
        Dim nameCounts As New Dictionary(Of String, Integer)(StringComparer.CurrentCultureIgnoreCase)
        For Each item In UltraDetailListView1.Items
            Dim data = 获取项数据(item)
            Dim metricResult As 指标结果数据 = Nothing
            If Not data.指标结果.TryGetValue(metric, metricResult) Then Continue For
            Dim entry As New 图表条目 With {.文件名 = 获取唯一图表文件名(data.文件路径, nameCounts)}
            SyncLock metricResult.数据锁
                entry.每帧数据 = New List(Of Double)(metricResult.每帧数据)
                entry.成功 = metricResult.成功
                entry.汇总值 = metricResult.汇总值
            End SyncLock
            result.Add(entry)
        Next
        Return result
    End Function

    Private Shared Function 获取唯一图表文件名(filePath As String, nameCounts As Dictionary(Of String, Integer)) As String
        Dim name = Path.GetFileName(filePath)
        If String.IsNullOrWhiteSpace(name) Then name = If(filePath, "")
        If String.IsNullOrWhiteSpace(name) Then name = "未命名文件"

        Dim count As Integer
        nameCounts.TryGetValue(name, count)
        count += 1
        nameCounts(name) = count
        If count = 1 Then Return name
        Return $"{name} #{count}"
    End Function

    Private Shared Function 解析图表指标(metricName As String) As 指标类型
        Dim name = If(metricName, "").Trim()
        For Each metric In 全部指标
            If String.Equals(name, 获取指标名称(metric), StringComparison.OrdinalIgnoreCase) Then Return metric
        Next
        Return 指标类型.PSNR
    End Function

    Private Shared Function 引用参数(value As String) As String
        Return $"""{If(value, "").Replace("""", "\""")}"""
    End Function

    Private Shared Function 引用过滤器参数(value As String) As String
        Return "'" & 转义过滤器值(value) & "'"
    End Function

    Private Shared Function 转义过滤器值(value As String) As String
        Return If(value, "").Replace("\", "\\").Replace(":", "\:").Replace("'", "\'")
    End Function

    Private Sub Form_v6_集成工具_质量评测_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        取消当前评测()
        Form_v6_集成工具_质量评测图表.关闭当前图表()
    End Sub

End Class
