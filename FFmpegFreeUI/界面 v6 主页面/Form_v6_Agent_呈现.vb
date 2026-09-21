Imports System.Text

Partial Public Class Form_v6_Agent
    Private _statusItem As LakeUI.AgentRoom.ChatItem = Nothing
    Private _latestTurnOverviewItem As LakeUI.AgentRoom.ChatItem = Nothing

    Private Sub RefreshLatestTurnOverview()
        If _current Is Nothing Then Return
        Dim turn = GetConversationRuntime(_current).ActiveTurn
        If turn Is Nothing Then turn = _current.Turns?.LastOrDefault()
        If turn Is Nothing Then Return
        Dim summary = FormatTurnHeader(turn)
        Dim header = AgentRoom1.FindItem(turn.Id)
        If header IsNot Nothing Then header.Title = summary
        If _latestTurnOverviewItem Is Nothing OrElse Not AgentRoom1.Items.Contains(_latestTurnOverviewItem) Then
            _latestTurnOverviewItem = AgentRoom1.AddCard(summary)
        Else
            _latestTurnOverviewItem.Text = summary
            If AgentRoom1.Items.IndexOf(_latestTurnOverviewItem) <> AgentRoom1.Items.Count - 1 Then
                AgentRoom1.Items.Remove(_latestTurnOverviewItem)
                AgentRoom1.Items.Add(_latestTurnOverviewItem)
            End If
        End If
    End Sub

    Private Sub FollowLatestWithOverview()
        RefreshLatestTurnOverview()
        AgentRoom1.FollowLatestIfPinned()
    End Sub

    Private Sub RenderCurrentConversation(Optional scrollToBottom As Boolean = True)
        Dim run = GetConversationRuntime(_current)
        AgentRoom1.Clear()
        _statusItem = Nothing
        _latestTurnOverviewItem = Nothing
        run.ActiveTurnItem = Nothing
        run.ActiveResponseItem = Nothing
        run.ActiveThinkingItem = Nothing
        If _current Is Nothing Then
            UpdateSendButtonState()
            Return
        End If
        Dim turnsByUserMessage = If(_current.Turns, New List(Of AgentTurnData)).
            Where(Function(x) x IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(x.UserMessageId)).
            GroupBy(Function(x) x.UserMessageId, StringComparer.OrdinalIgnoreCase).
            ToDictionary(Function(x) x.Key, Function(x) x.Last(), StringComparer.OrdinalIgnoreCase)

        For Each message In _current.Messages
            Select Case message.Role
                Case "user"
                    If String.Equals(message.Name, AgentConversationSchema.SteeringMessageName, StringComparison.OrdinalIgnoreCase) Then Continue For
                    AgentRoom1.AddUserMessage(message.Content)
                    Dim turn As AgentTurnData = Nothing
                    If turnsByUserMessage.TryGetValue(message.Id, turn) Then RenderTurnRecord(turn)
                Case "assistant"
                    If String.Equals(message.Name, AgentConversationSchema.ActivityMessageName, StringComparison.OrdinalIgnoreCase) Then Continue For
                    If Not String.IsNullOrWhiteSpace(message.Content) Then AgentRoom1.AddAssistantMessage(message.Content)
                Case "tool"
                    ' 工具过程由 Turns 中的展示记录承载。
                Case "card"
                    If Not IsToolSummaryMessage(message) Then AddStoredCard(message)
            End Select
        Next
        RenderActiveRunOverlay()
        RefreshLatestTurnOverview()
        UpdateSendButtonState()
        If scrollToBottom Then AgentRoom1.ScrollToBottom()
    End Sub

    Private Sub RenderTurnRecord(turn As AgentTurnData)
        Dim run = GetConversationRuntime(_current)
        If turn Is Nothing Then Return
        Dim isActive = ReferenceEquals(turn, run.ActiveTurn)
        Dim expanded = isActive OrElse Not String.Equals(turn.State, "completed", StringComparison.OrdinalIgnoreCase)
        Dim header = AgentRoom1.AddTurnHeader(turn.Id, FormatTurnHeader(turn), expanded)
        If isActive Then run.ActiveTurnItem = header

        Dim activities = If(turn.Activities, New List(Of AgentTurnActivityData))
        Dim activityIndex As Integer = 0
        While activityIndex < activities.Count
            Dim activity = activities(activityIndex)
            activityIndex += 1
            If activity Is Nothing Then Continue While
            Select Case If(activity.Kind, "").ToLowerInvariant()
                Case "assistant"
                    If Not String.IsNullOrWhiteSpace(activity.Content) Then
                        AgentRoom1.AddAssistantActivity(turn.Id, activity.Content, activity.Id)
                    End If
                Case "guidance"
                    If Not String.IsNullOrWhiteSpace(activity.Content) Then
                        AgentRoom1.AddUserMessage(activity.Content)
                    End If
                Case "tool"
                    Dim toolActivities As New List(Of AgentTurnActivityData) From {activity}
                    While activityIndex < activities.Count AndAlso
                          activities(activityIndex) IsNot Nothing AndAlso
                          String.Equals(activities(activityIndex).Kind, "tool", StringComparison.OrdinalIgnoreCase)
                        toolActivities.Add(activities(activityIndex))
                        activityIndex += 1
                    End While
                    Dim item = AgentRoom1.AddToolCall(
                        turn.Id,
                        FormatToolGroupTitle(toolActivities),
                        FormatToolGroupDetails(toolActivities),
                        activity.Id,
                        expanded:=False,
                        running:=toolActivities.Any(Function(x) String.Equals(x.State, "running", StringComparison.OrdinalIgnoreCase)),
                        isError:=toolActivities.Any(Function(x) IsToolActivityError(x)))
            End Select
        End While
    End Sub

    Private Function FormatTurnHeader(turn As AgentTurnData) As String
        If turn Is Nothing Then Return "工作记录"
        Dim elapsed = If(turn.CompletedAt > turn.StartedAt,
                         turn.CompletedAt - turn.StartedAt,
                         If(String.Equals(turn.State, "running", StringComparison.OrdinalIgnoreCase), DateTime.Now - turn.StartedAt, TimeSpan.Zero))
        Dim toolCount = If(turn.Activities, New List(Of AgentTurnActivityData)).
            Where(Function(x) x IsNot Nothing AndAlso String.Equals(x.Kind, "tool", StringComparison.OrdinalIgnoreCase)).
            Count()
        Dim suffix = $" · {toolCount} 次工具调用"
        Select Case If(turn.State, "").ToLowerInvariant()
            Case "running"
                Dim status = CompactSingleLine(If(String.IsNullOrWhiteSpace(turn.StatusText), "正在工作", turn.StatusText), 34)
                Return $"{status} · {FormatElapsedMilliseconds(elapsed.TotalMilliseconds)}{suffix}"
            Case "canceled"
                Return $"已停止 · {FormatElapsedMilliseconds(elapsed.TotalMilliseconds)}{suffix}"
            Case "error"
                Return $"运行失败 · {FormatElapsedMilliseconds(elapsed.TotalMilliseconds)}{suffix}"
            Case Else
                Return $"已工作 {FormatElapsedMilliseconds(elapsed.TotalMilliseconds)}{suffix}"
        End Select
    End Function

    Private Function CompactSingleLine(value As String, maxLength As Integer) As String
        Dim text = If(value, "").Replace(vbCr, " ").Replace(vbLf, " ").Trim()
        While text.Contains("  ", StringComparison.Ordinal)
            text = text.Replace("  ", " ")
        End While
        maxLength = Math.Max(4, maxLength)
        If text.Length > maxLength Then text = String.Concat(text.AsSpan(0, maxLength - 3), "...")
        Return text
    End Function

    Private Function FormatToolActivityDetails(activity As AgentTurnActivityData) As String
        If activity Is Nothing Then Return ""
        Dim elapsed = FormatElapsedMilliseconds(GetToolElapsedMilliseconds(activity))
        Return $"{GetToolDisplayName(activity.ToolName)} ({activity.ToolName}) · 耗时 {elapsed} · {If(activity.ResultText, "").Length} 字 · {GetToolExecutionStatus(activity)}" &
            vbCrLf & "参数：" & If(String.IsNullOrWhiteSpace(activity.Arguments), "（无）", activity.Arguments)
    End Function

    ' 返回正文只提取完成标志；参数在记录详情中完整显示。
    Private Function GetToolExecutionStatus(activity As AgentTurnActivityData) As String
        Select Case If(activity?.State, "").ToLowerInvariant()
            Case "running" : Return "正在执行"
            Case "pending" : Return "等待执行"
            Case "canceled" : Return "已取消"
        End Select
        Dim failed = String.Equals(activity?.State, "error", StringComparison.OrdinalIgnoreCase)
        Dim result = If(activity?.ResultText, "").Trim()
        Dim exitCode As Integer
        Dim hasExitCode As Boolean = False
        Dim timedOut As Boolean = False
        Dim success As Boolean = False
        Try
            Using doc = System.Text.Json.JsonDocument.Parse(result)
                Dim root = doc.RootElement
                Dim value As System.Text.Json.JsonElement
                If root.ValueKind = System.Text.Json.JsonValueKind.Object Then
                    If root.TryGetProperty("exit_code", value) AndAlso value.ValueKind = System.Text.Json.JsonValueKind.Number Then hasExitCode = value.TryGetInt32(exitCode)
                    If root.TryGetProperty("timed_out", value) Then timedOut = value.ValueKind = System.Text.Json.JsonValueKind.True
                    If root.TryGetProperty("success", value) Then
                        success = value.ValueKind = System.Text.Json.JsonValueKind.True
                        failed = failed OrElse value.ValueKind = System.Text.Json.JsonValueKind.False
                    End If
                    If root.TryGetProperty("error", value) Then
                        failed = failed OrElse (value.ValueKind <> System.Text.Json.JsonValueKind.Null AndAlso value.ValueKind <> System.Text.Json.JsonValueKind.False AndAlso value.ToString().Trim() <> "")
                    End If
                End If
            End Using
        Catch ex As System.Text.Json.JsonException
            ' 没有结构化成功标志的纯文本返回不标注为成功。
            Dim errorPrefixes = {"工具执行失败", "工具参数 JSON 解析失败", "权限不足", "请求失败", "读取失败", "端点联网失败", "本地联网搜索失败", "缺少 ", "Missing ", "Directory does not exist:", "工作目录不存在", "无法启动可执行文件", "拒绝删除", "补丁未应用", "文件不存在", "目录不存在", "父目录不存在", "源文件不存在", "源路径不存在", "路径不存在", "目标文件已存在", "目标路径已存在", "联网已禁用", "当前联网模式不允许", "URL 无效", "搜索引擎地址无效"}
            failed = failed OrElse errorPrefixes.Any(Function(prefix) result.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
        End Try
        Dim suffix = If(hasExitCode, $"（退出码 {exitCode}）", "")
        If timedOut Then Return "超时" & suffix
        If failed OrElse (hasExitCode AndAlso exitCode <> 0) Then Return "失败" & suffix
        If success OrElse hasExitCode Then Return "成功" & suffix
        Return "已返回"
    End Function

    Private Function IsToolActivityError(activity As AgentTurnActivityData) As Boolean
        Dim status = GetToolExecutionStatus(activity)
        Return status.StartsWith("失败", StringComparison.Ordinal) OrElse status.StartsWith("超时", StringComparison.Ordinal)
    End Function

    Private Function FormatToolGroupTitle(activities As IReadOnlyList(Of AgentTurnActivityData)) As String
        If activities Is Nothing OrElse activities.Count = 0 Then Return "工具调用"
        Dim runningActivity = activities.LastOrDefault(Function(x) String.Equals(x?.State, "running", StringComparison.OrdinalIgnoreCase))
        Dim currentName = If(runningActivity Is Nothing, "无", GetToolDisplayName(runningActivity.ToolName))
        Dim currentElapsed = If(runningActivity Is Nothing, "—", FormatElapsedMilliseconds(GetToolElapsedMilliseconds(runningActivity)))
        Dim errorCount = activities.Where(Function(x) IsToolActivityError(x)).Count()
        Dim canceledCount = activities.Where(Function(x) String.Equals(x?.State, "canceled", StringComparison.OrdinalIgnoreCase)).Count()
        Dim elapsed = activities.Sum(Function(x) GetToolElapsedMilliseconds(x))
        Dim currentPrefix = If(runningActivity Is Nothing, "", $"当前调用：{currentName} · 正在执行：{currentElapsed} · ")
        Return currentPrefix & $"总耗时 {FormatElapsedMilliseconds(elapsed)} · 共 {activities.Count} 次 · 错误 {errorCount} 次" & If(canceledCount > 0, $" · 取消 {canceledCount} 次", "")
    End Function

    Private Function GetToolElapsedMilliseconds(activity As AgentTurnActivityData) As Double
        If activity Is Nothing Then Return 0
        If String.Equals(activity.State, "running", StringComparison.OrdinalIgnoreCase) Then
            Return Math.Max(0, (DateTime.Now - activity.CreatedAt).TotalMilliseconds)
        End If
        Return Math.Max(0, activity.ElapsedMilliseconds)
    End Function

    Private Function FormatToolGroupDetails(activities As IReadOnlyList(Of AgentTurnActivityData)) As String
        If activities Is Nothing OrElse activities.Count = 0 Then Return ""
        Return String.Join(vbCrLf, activities.Where(Function(x) x IsNot Nothing).Select(Function(activity, index) $"#{index + 1}  {FormatToolActivityDetails(activity)}"))
    End Function

    Private Function GetConsecutiveToolActivities(conversation As AgentConversationData, activity As AgentTurnActivityData) As List(Of AgentTurnActivityData)
        Dim run = GetConversationRuntime(conversation)
        Dim result As New List(Of AgentTurnActivityData)
        If activity Is Nothing OrElse run.ActiveTurn?.Activities Is Nothing Then Return result
        Dim index = run.ActiveTurn.Activities.IndexOf(activity)
        If index < 0 Then Return result

        Dim first = index
        While first > 0 AndAlso run.ActiveTurn.Activities(first - 1) IsNot Nothing AndAlso
              String.Equals(run.ActiveTurn.Activities(first - 1).Kind, "tool", StringComparison.OrdinalIgnoreCase)
            first -= 1
        End While
        For i = first To run.ActiveTurn.Activities.Count - 1
            Dim candidate = run.ActiveTurn.Activities(i)
            If candidate Is Nothing OrElse Not String.Equals(candidate.Kind, "tool", StringComparison.OrdinalIgnoreCase) Then Exit For
            result.Add(candidate)
        Next
        Return result
    End Function

    Private Sub UpdateToolGroupItem(item As LakeUI.AgentRoom.ChatItem,
                                    activities As IReadOnlyList(Of AgentTurnActivityData))
        If item Is Nothing OrElse activities Is Nothing OrElse activities.Count = 0 Then Return
        item.Title = FormatToolGroupTitle(activities)
        item.Text = FormatToolGroupDetails(activities)
        item.IsRunning = activities.Any(Function(x) String.Equals(x?.State, "running", StringComparison.OrdinalIgnoreCase))
        item.IsError = activities.Any(Function(x) IsToolActivityError(x))
    End Sub

    Private Function IsToolSummaryMessage(message As AgentMessageData) As Boolean
        Return message IsNot Nothing AndAlso
            String.Equals(message.Role, "card", StringComparison.OrdinalIgnoreCase) AndAlso
            String.Equals(If(message.Name, ""), "tool_summary", StringComparison.OrdinalIgnoreCase)
    End Function

    Private Sub AddStoredCard(message As AgentMessageData)
        If message Is Nothing OrElse String.IsNullOrWhiteSpace(message.Content) Then Return
        AgentRoom1.AddCard(message.Content)
    End Sub

    Private Function IsRunResponsePlaceholder(text As String) As Boolean
        text = If(text, "").Trim()
        Return text = "" OrElse
            text = "正在思考..." OrElse
            text = "正在重新连接..." OrElse
            text.StartsWith("正在调用工具：", StringComparison.Ordinal)
    End Function

    Private Function FormatElapsedMilliseconds(elapsedMilliseconds As Double) As String
        If Double.IsNaN(elapsedMilliseconds) OrElse Double.IsInfinity(elapsedMilliseconds) OrElse elapsedMilliseconds < 0 Then elapsedMilliseconds = 0
        Dim totalMilliseconds = CLng(Math.Round(elapsedMilliseconds))
        If totalMilliseconds < 1000 Then Return $"{totalMilliseconds} 毫秒"

        Dim span = TimeSpan.FromMilliseconds(totalMilliseconds)
        If span.TotalMinutes < 1 Then Return $"{span.TotalSeconds:0.#} 秒"
        If span.TotalHours < 1 Then Return $"{CInt(Math.Floor(span.TotalMinutes))} 分钟 {span.Seconds} 秒"
        Return $"{CInt(Math.Floor(span.TotalHours))} 小时 {span.Minutes} 分钟"
    End Function

    Private Sub UpdateActiveRunOverviewCard(conversation As AgentConversationData)
        Dim run = GetConversationRuntime(conversation)
        If Not ReferenceEquals(conversation, run.ActiveRunConversation) Then Return
        If Not IsConversationSelected(conversation) Then Return
        If run.ActiveTurn Is Nothing Then Return
        If run.ActiveTurnItem Is Nothing OrElse Not AgentRoom1.Items.Contains(run.ActiveTurnItem) Then
            run.ActiveTurnItem = AgentRoom1.FindItem(run.ActiveTurn.Id)
        End If
        If run.ActiveTurnItem Is Nothing Then Return
        run.ActiveTurnItem.Title = FormatTurnHeader(run.ActiveTurn)
        run.ActiveTurnItem.IsRunning = String.Equals(run.ActiveTurn.State, "running", StringComparison.OrdinalIgnoreCase)
        run.ActiveTurnItem.IsError = String.Equals(run.ActiveTurn.State, "error", StringComparison.OrdinalIgnoreCase)
        If IsConversationSelected(conversation) Then FollowLatestWithOverview()
    End Sub

    Private Function GetToolDisplayName(toolName As String) As String
        Select Case If(toolName, "").Trim()
            Case "get_parameter_panel_state"
                Return "读取参数面板"
            Case "get_parameter_field_info"
                Return "查询参数字段"
            Case "apply_parameter_panel_patch"
                Return "修改参数面板"
            Case "get_queue_summary"
                Return "读取队列信息"
            Case "get_queue_task_logs"
                Return "读取任务日志"
            Case "control_queue_tasks"
                Return "控制队列任务"
            Case "sync_parameter_panel_to_queue"
                Return "同步参数到队列"
            Case "patch_queue_task_presets"
                Return "修改任务预设选项"
            Case "get_ui_tabs"
                Return "读取选项卡"
            Case "switch_ui_tab"
                Return "切换页面"
            Case "get_prepare_files"
                Return "读取准备文件"
            Case "set_prepare_files"
                Return "设置准备文件"
            Case "submit_prepare_files_to_queue"
                Return "准备文件入队"
            Case "get_integrated_tool_state"
                Return "读取集成工具"
            Case "configure_integrated_tool"
                Return "配置集成工具"
            Case "run_integrated_tool"
                Return "运行集成工具"
            Case "get_system_hardware"
                Return "读取硬件信息"
            Case "get_parameter_panel_controls"
                Return "读取参数控件"
            Case "list_parameter_presets"
                Return "列出参数预设"
            Case "read_parameter_preset"
                Return "读取参数预设"
            Case "apply_parameter_preset"
                Return "应用参数预设"
            Case "save_parameter_preset"
                Return "保存参数预设"
            Case "web_search"
                Return "联网搜索"
            Case "fetch_url"
                Return "读取网页"
            Case "http_request"
                Return "HTTP 请求"
            Case "read_local_text_file"
                Return "读取本地文件"
            Case "write_local_text_file"
                Return "写入本地文件"
            Case "apply_local_text_patch"
                Return "编辑本地文件"
            Case "list_directory"
                Return "列举目录"
            Case "create_directory"
                Return "创建目录"
            Case "copy_local_file"
                Return "复制本地文件"
            Case "move_local_path"
                Return "移动本地路径"
            Case "delete_local_path"
                Return "删除本地路径"
            Case "get_image_info"
                Return "读取图片信息"
            Case "run_powershell"
                Return "PowerShell 终端"
            Case "run_windows_executable"
                Return "运行 Windows 程序"
            Case Else
                Return If(String.IsNullOrWhiteSpace(toolName), "工具", toolName)
        End Select
    End Function

    Private Sub UpdateUsageButton()
        If _closing OrElse IsDisposed Then Return
        Dim currentUsage = If(_current?.Usage, New AgentUsageInfo)
        Dim currentContextTokens = Math.Max(0, currentUsage.LastRequestInputTokens)
        Dim contextWindowTokens = GetUsageContextWindowTokens(currentUsage)
        MB_页面用量.Text = $"{FormatContextPercent(currentUsage)} | {currentContextTokens} / {contextWindowTokens}"
    End Sub

    Private Sub UpdateSendButtonState()
        If _closing OrElse IsDisposed Then Return
        Dim run = GetConversationRuntime(_current)
        If run.Busy AndAlso IsConversationSelected(run.ActiveRunConversation) Then
            If run.RequestCts IsNot Nothing AndAlso run.RequestCts.IsCancellationRequested Then
                MB_发送.Text = "停止中"
            ElseIf Not String.IsNullOrWhiteSpace(ModernTextBox1.Text) OrElse _pendingFiles.Count > 0 Then
                MB_发送.Text = "调整方向"
            Else
                MB_发送.Text = "停止"
            End If
        Else
            MB_发送.Text = "发送"
        End If
        MB_发送.Enabled = Not run.Busy OrElse Not If(run.RequestCts?.IsCancellationRequested, False)
        MCB_模型选择.Enabled = Not _refreshingModels
        MCB_推理级别.Enabled = MCB_模型选择.Enabled
        MCB_联网设置.Enabled = True
        MCB_权限控制.Enabled = True
    End Sub

    Private Sub ShowStatus(text As String, Optional keepRecord As Boolean = False)
        Dim content = $"状态 {DateTime.Now:HH:mm:ss}{vbCrLf}{If(text, "").Trim()}"
        If keepRecord OrElse _statusItem Is Nothing OrElse Not AgentRoom1.Items.Contains(_statusItem) Then
            _statusItem = AddCardBeforeActiveResponse(content)
        Else
            _statusItem.Text = content
        End If
        FollowLatestWithOverview()
    End Sub

    Private Sub ShowRunStatus(conversation As AgentConversationData, text As String, Optional keepRecord As Boolean = False)
        Dim run = GetConversationRuntime(conversation)
        Dim content = If(text, "").Trim()
        If ReferenceEquals(conversation, run.ActiveRunConversation) Then
            run.ActiveRunStatusText = content
            If run.ActiveTurn IsNot Nothing Then run.ActiveTurn.StatusText = content
            UpdateActiveRunOverviewCard(conversation)
            If keepRecord AndAlso IsConversationSelected(conversation) Then
                AgentRoom1.AddCard(content)
                If IsConversationSelected(conversation) Then FollowLatestWithOverview()
            End If
            Return
        End If

        If Not IsConversationSelected(conversation) Then Return
        AgentRoom1.AddCard(content)
        If IsConversationSelected(conversation) Then FollowLatestWithOverview()
    End Sub

    Private Sub ShowActiveThinking(conversation As AgentConversationData)
        Dim run = GetConversationRuntime(conversation)
        If Not ReferenceEquals(conversation, run.ActiveRunConversation) OrElse
           Not IsConversationSelected(conversation) OrElse
           run.ActiveTurn Is Nothing Then Return
        If run.ActiveThinkingItem Is Nothing OrElse Not AgentRoom1.Items.Contains(run.ActiveThinkingItem) Then
            run.ActiveThinkingItem = AgentRoom1.AddAssistantActivity(run.ActiveTurn.Id, "正在思考...")
        Else
            run.ActiveThinkingItem.Text = "正在思考..."
        End If
        If IsConversationSelected(conversation) Then FollowLatestWithOverview()
    End Sub

    Private Sub BeginThinkingTextStream(conversation As AgentConversationData)
        Dim run = GetConversationRuntime(conversation)
        If run.ActiveThinkingParser Is Nothing Then run.ActiveThinkingParser = New LakeUI.AgentThinkingTextParser()
        run.ActiveThinkingParser.Reset()
        run.ActiveThinkingTextBuilder.Clear()
        run.ActiveThinkingActivity = Nothing
        run.ActiveStreamReceivedDelta = False
    End Sub

    Private Sub AppendThinkingText(conversation As AgentConversationData, text As String)
        Dim run = GetConversationRuntime(conversation)
        If String.IsNullOrEmpty(text) OrElse run.ActiveTurn Is Nothing Then Return
        If run.ActiveThinkingActivity Is Nothing Then
            run.ActiveThinkingActivity = New AgentTurnActivityData With {
                .Kind = "assistant",
                .State = "running"
            }
            run.ActiveTurn.Activities.Add(run.ActiveThinkingActivity)
        End If
        run.ActiveThinkingTextBuilder.Append(text)
        run.ActiveThinkingActivity.Content = run.ActiveThinkingTextBuilder.ToString()

        If Not IsConversationSelected(conversation) Then Return
        If run.ActiveThinkingItem Is Nothing OrElse Not AgentRoom1.Items.Contains(run.ActiveThinkingItem) Then
            run.ActiveThinkingItem = AgentRoom1.AddAssistantActivity(run.ActiveTurn.Id, run.ActiveThinkingActivity.Content, run.ActiveThinkingActivity.Id)
        Else
            run.ActiveThinkingItem.Text = run.ActiveThinkingActivity.Content
        End If
        If IsConversationSelected(conversation) Then FollowLatestWithOverview()
    End Sub

    Private Sub CompleteThinkingText(conversation As AgentConversationData)
        Dim run = GetConversationRuntime(conversation)
        If run.ActiveThinkingActivity Is Nothing Then Return
        run.ActiveThinkingActivity.State = "completed"
        If String.IsNullOrWhiteSpace(run.ActiveThinkingActivity.Content) Then
            HideActiveThinking(conversation)
        Else
            run.ActiveThinkingItem = Nothing
        End If
    End Sub

    Private Sub ApplyThinkingChunk(conversation As AgentConversationData,
                                   chunk As LakeUI.AgentThinkingTextChunk,
                                   Optional appendVisibleToResponse As Boolean = True)
        If chunk Is Nothing Then Return
        AppendThinkingText(conversation, chunk.ThinkingText)
        If Not String.IsNullOrEmpty(chunk.VisibleText) Then
            CompleteThinkingText(conversation)
            If appendVisibleToResponse Then AppendRunResponseText(conversation, chunk.VisibleText, False)
        End If
    End Sub

    Private Function ParseCompleteAgentText(conversation As AgentConversationData, text As String) As String
        Dim run = GetConversationRuntime(conversation)
        BeginThinkingTextStream(conversation)
        Dim chunk = run.ActiveThinkingParser.Append(If(text, ""))
        ApplyThinkingChunk(conversation, chunk, appendVisibleToResponse:=False)
        Dim tail = run.ActiveThinkingParser.Complete()
        ApplyThinkingChunk(conversation, tail, appendVisibleToResponse:=False)
        Dim visible As New StringBuilder()
        visible.Append(chunk.VisibleText)
        visible.Append(tail.VisibleText)
        Return visible.ToString()
    End Function

    Private Shared Function StripAgentThinkingText(text As String) As String
        Dim parser As New LakeUI.AgentThinkingTextParser()
        Dim first = parser.Append(If(text, ""))
        Dim tail = parser.Complete()
        Return first.VisibleText & tail.VisibleText
    End Function

    Private Sub HideActiveThinking(conversation As AgentConversationData)
        Dim run = GetConversationRuntime(conversation)
        If run.ActiveThinkingActivity IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(run.ActiveThinkingActivity.Content) Then
            run.ActiveThinkingActivity.State = "completed"
            run.ActiveThinkingItem = Nothing
            Return
        End If
        If run.ActiveThinkingItem IsNot Nothing AndAlso AgentRoom1.Items.Contains(run.ActiveThinkingItem) Then
            AgentRoom1.Items.Remove(run.ActiveThinkingItem)
        End If
        run.ActiveThinkingItem = Nothing
        run.ActiveThinkingActivity = Nothing
        run.ActiveThinkingTextBuilder.Clear()
    End Sub

    Private Function AddCardBeforeActiveResponse(content As String) As LakeUI.AgentRoom.ChatItem
        Dim run = GetConversationRuntime(_current)
        Dim item As New LakeUI.AgentRoom.ChatItem(LakeUI.AgentRoom.ChatItemKind.Card, content)
        Dim anchor = run.ActiveResponseItem
        Dim insertIndex = If(anchor Is Nothing, -1, AgentRoom1.Items.IndexOf(anchor))
        If insertIndex >= 0 Then
            AgentRoom1.Items.Insert(insertIndex, item)
        Else
            AgentRoom1.Items.Add(item)
        End If
        Return item
    End Function

    Private Function IsConversationSelected(conversation As AgentConversationData) As Boolean
        Return Not _closing AndAlso Not IsDisposed AndAlso conversation IsNot Nothing AndAlso _current IsNot Nothing AndAlso String.Equals(conversation.Id, _current.Id, StringComparison.OrdinalIgnoreCase)
    End Function

    Private Sub SetRunResponseText(conversation As AgentConversationData, text As String)
        Dim run = GetConversationRuntime(conversation)
        If ReferenceEquals(conversation, run.ActiveRunConversation) Then ReplaceActiveResponseText(conversation, If(text, ""))
        If Not IsConversationSelected(conversation) Then Return

        If IsRunResponsePlaceholder(run.ActiveResponseText) Then
            If run.ActiveResponseItem IsNot Nothing AndAlso AgentRoom1.Items.Contains(run.ActiveResponseItem) Then
                AgentRoom1.Items.Remove(run.ActiveResponseItem)
            End If
            run.ActiveResponseItem = Nothing
            If IsConversationSelected(conversation) Then FollowLatestWithOverview()
            Return
        End If

        If run.ActiveResponseItem Is Nothing OrElse Not AgentRoom1.Items.Contains(run.ActiveResponseItem) Then
            run.ActiveResponseItem = AgentRoom1.AddAssistantMessage(run.ActiveResponseText)
        Else
            run.ActiveResponseItem.Text = run.ActiveResponseText
        End If
        If IsConversationSelected(conversation) Then FollowLatestWithOverview()
    End Sub

    Private Sub AppendRunResponseText(conversation As AgentConversationData,
                                      delta As String,
                                      prependParagraphBreak As Boolean)
        Dim run = GetConversationRuntime(conversation)
        If String.IsNullOrEmpty(delta) OrElse Not ReferenceEquals(conversation, run.ActiveRunConversation) Then Return
        HideActiveThinking(conversation)

        If Not run.ActiveResponseTextDirty AndAlso IsRunResponsePlaceholder(run.ActiveResponseText) Then
            run.ActiveResponseTextBuilder.Clear()
            run.ActiveResponseText = ""
            run.ActiveResponseTextDirty = False
        End If
        Dim appendedText = delta
        If prependParagraphBreak AndAlso run.ActiveResponseTextBuilder.Length > 0 Then
            appendedText = vbCrLf & vbCrLf & appendedText
        End If
        run.ActiveResponseTextBuilder.Append(appendedText)
        run.ActiveResponseTextDirty = True
        If Not IsConversationSelected(conversation) Then Return

        If run.ActiveResponseItem Is Nothing OrElse Not AgentRoom1.Items.Contains(run.ActiveResponseItem) Then
            run.ActiveResponseItem = AgentRoom1.AddAssistantMessage(GetActiveResponseTextSnapshot(conversation))
        Else
            AgentRoom1.AppendToItem(run.ActiveResponseItem, appendedText)
        End If
        If IsConversationSelected(conversation) Then FollowLatestWithOverview()
    End Sub

    Private Sub AppendRunResponseDelta(conversation As AgentConversationData, delta As String)
        Dim run = GetConversationRuntime(conversation)
        If String.IsNullOrEmpty(delta) OrElse Not ReferenceEquals(conversation, run.ActiveRunConversation) Then Return
        run.ActiveStreamReceivedDelta = True
        If run.ActiveThinkingParser Is Nothing Then BeginThinkingTextStream(conversation)
        ApplyThinkingChunk(conversation, run.ActiveThinkingParser.Append(delta))
    End Sub

    Private Sub CompleteRunResponseText(conversation As AgentConversationData, text As String)
        Dim run = GetConversationRuntime(conversation)
        HideActiveThinking(conversation)
        Dim finalText = If(text, "").Trim()
        Dim currentText = GetActiveResponseTextSnapshot(conversation).Trim()
        If ReferenceEquals(conversation, run.ActiveRunConversation) AndAlso
           run.ActiveResponseItem IsNot Nothing AndAlso
           AgentRoom1.Items.Contains(run.ActiveResponseItem) AndAlso
           String.Equals(currentText, finalText, StringComparison.Ordinal) Then
            ReplaceActiveResponseText(conversation, finalText)
            AgentRoom1.CompleteStreamingMessage(run.ActiveResponseItem)
            Return
        End If
        SetRunResponseText(conversation, finalText)
    End Sub

    Private Sub PromoteActiveResponseToActivity(conversation As AgentConversationData, content As String)
        Dim run = GetConversationRuntime(conversation)
        If run.ActiveTurn Is Nothing OrElse Not ReferenceEquals(conversation, run.ActiveRunConversation) Then Return
        HideActiveThinking(conversation)
        Dim activityText = If(content, "").Trim()
        If activityText = "" Then activityText = GetActiveResponseTextSnapshot(conversation).Trim()

        If activityText <> "" AndAlso Not IsRunResponsePlaceholder(activityText) Then
            Dim activity As New AgentTurnActivityData With {
                .Kind = "assistant",
                .Content = activityText,
                .State = "completed"
            }
            run.ActiveTurn.Activities.Add(activity)

            If IsConversationSelected(conversation) Then
                If run.ActiveResponseItem IsNot Nothing AndAlso AgentRoom1.Items.Contains(run.ActiveResponseItem) Then
                    AgentRoom1.CompleteStreamingMessage(run.ActiveResponseItem)
                    run.ActiveResponseItem.Kind = LakeUI.AgentRoom.ChatItemKind.AssistantActivity
                    run.ActiveResponseItem.Key = activity.Id
                    run.ActiveResponseItem.ParentTurnId = run.ActiveTurn.Id
                Else
                    AgentRoom1.AddAssistantActivity(run.ActiveTurn.Id, activityText, activity.Id)
                End If
            End If
        ElseIf run.ActiveResponseItem IsNot Nothing AndAlso AgentRoom1.Items.Contains(run.ActiveResponseItem) Then
            AgentRoom1.Items.Remove(run.ActiveResponseItem)
        End If

        run.ActiveResponseItem = Nothing
        ReplaceActiveResponseText(conversation, "正在思考...")
        If IsConversationSelected(conversation) Then FollowLatestWithOverview()
    End Sub

    Private Function BeginToolActivity(conversation As AgentConversationData,
                                       callInfo As AgentToolCallInfo) As AgentTurnActivityData
        HideActiveThinking(conversation)
        Dim activity As New AgentTurnActivityData With {
            .Kind = "tool", .ToolName = If(callInfo?.Name, ""),
            .ToolCallId = If(callInfo?.Id, ""), .Arguments = If(callInfo?.Arguments, ""),
            .State = "running"
        }
        GetConversationRuntime(conversation).ActiveTurn?.Activities.Add(activity)
        RefreshToolActivity(conversation, activity)
        Return activity
    End Function

    Private Sub CompleteToolActivity(conversation As AgentConversationData,
                                     activity As AgentTurnActivityData,
                                     resultText As String,
                                     elapsedMilliseconds As Double,
                                     isError As Boolean)
        If activity Is Nothing Then Return
        activity.ElapsedMilliseconds = elapsedMilliseconds
        activity.ResultText = If(resultText, "")
        activity.State = If(isError, "error", "completed")
        If IsToolActivityError(activity) Then activity.State = "error"
        RefreshToolActivity(conversation, activity)
    End Sub

    Private Sub RefreshToolActivity(conversation As AgentConversationData, activity As AgentTurnActivityData)
        If Not IsConversationSelected(conversation) Then Return
        Dim group = GetConsecutiveToolActivities(conversation, activity)
        If group.Count = 0 Then Return
        Dim item = AgentRoom1.FindItem(group(0).Id)
        If item Is Nothing Then
            item = AgentRoom1.AddToolCall(GetConversationRuntime(conversation).ActiveTurn.Id,
                                         FormatToolGroupTitle(group), FormatToolGroupDetails(group), group(0).Id,
                                         expanded:=False)
        End If
        UpdateToolGroupItem(item, group)
        UpdateActiveRunOverviewCard(conversation)
    End Sub

    Private Sub ReplaceActiveResponseText(conversation As AgentConversationData, text As String)
        Dim run = GetConversationRuntime(conversation)
        run.ActiveResponseText = If(text, "")
        run.ActiveResponseTextBuilder.Clear()
        run.ActiveResponseTextBuilder.Append(run.ActiveResponseText)
        run.ActiveResponseTextDirty = False
    End Sub

    Private Function GetActiveResponseTextSnapshot(conversation As AgentConversationData) As String
        Dim run = GetConversationRuntime(conversation)
        If run.ActiveResponseTextDirty Then
            run.ActiveResponseText = run.ActiveResponseTextBuilder.ToString()
            run.ActiveResponseTextDirty = False
        End If
        Return If(run.ActiveResponseText, "")
    End Function

    Private Sub RenderActiveRunOverlay()
        Dim run = GetConversationRuntime(_current)
        run.ActiveResponseItem = Nothing
        run.ActiveThinkingItem = If(run.ActiveThinkingActivity Is Nothing, Nothing, AgentRoom1.FindItem(run.ActiveThinkingActivity.Id))
        If run.ActiveRunConversation Is Nothing OrElse Not IsConversationSelected(run.ActiveRunConversation) Then Return

        If run.ActiveTurn IsNot Nothing Then
            run.ActiveTurnItem = AgentRoom1.FindItem(run.ActiveTurn.Id)
            If run.ActiveTurnItem IsNot Nothing Then run.ActiveTurnItem.IsExpanded = True
        End If
        Dim responseText = GetActiveResponseTextSnapshot(_current)
        If Not IsRunResponsePlaceholder(responseText) Then
            run.ActiveResponseItem = AgentRoom1.AddAssistantMessage(responseText)
        ElseIf run.ActiveThinkingItem Is Nothing AndAlso run.ActiveRunStatusText.StartsWith("正在思考", StringComparison.Ordinal) Then
            ShowActiveThinking(run.ActiveRunConversation)
        End If
    End Sub

    Private Sub ClearActiveRunState(conversation As AgentConversationData)
        Dim run = GetConversationRuntime(conversation)
        HideActiveThinking(conversation)
        run.ActiveThinkingParser?.Reset()
        run.ActiveThinkingParser = Nothing
        run.ActiveThinkingActivity = Nothing
        run.ActiveThinkingTextBuilder.Clear()
        run.ActiveStreamReceivedDelta = False
        run.ActiveResponseItem = Nothing
        run.ActiveRunConversation = Nothing
        run.ActiveTurn = Nothing
        run.ActiveTurnItem = Nothing
        ReplaceActiveResponseText(conversation, "")
        run.ActiveRunStatusText = ""
    End Sub
End Class
