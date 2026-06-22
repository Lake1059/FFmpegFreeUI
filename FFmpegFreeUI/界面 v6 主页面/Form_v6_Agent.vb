Imports System.Text
Imports System.Text.Json
Imports LakeUI

Public Class Form_v6_Agent
    Private _store As AgentConversationStore
    Private _current As AgentConversationData
    Private _orderedConversations As New List(Of AgentConversationData)
    Private _models As New List(Of AgentModelInfo)
    Private _pageUsage As New AgentUsageInfo
    Private _loading As Boolean = False
    Private _busy As Boolean = False
    Private _statusItem As LakeUI.AgentRoom.ChatItem = Nothing
    Private _requestCts As Threading.CancellationTokenSource = Nothing
    Private _restartAfterCancel As Boolean = False

    Private Const MaxRequestMessages As Integer = 60
    Private Const MaxRequestCharacters As Integer = 48000
    Private Const KeepRecentMessages As Integer = 40
    Private Const StreamFlushIntervalMs As Integer = 35
    Private Const StreamFlushCharacters As Integer = 96

    Private Class ToolRunLog
        Public Property Round As Integer
        Public Property ToolName As String = ""
        Public Property ElapsedMilliseconds As Double = -1
        Public Property ResultText As String = ""
    End Class

    Private Class ToolRoundLog
        Public Property Round As Integer
        Public Property Calls As New List(Of ToolRunLog)
    End Class

    Private Class StreamingTextBuffer
        Private ReadOnly _room As LakeUI.AgentRoom
        Private ReadOnly _item As LakeUI.AgentRoom.ChatItem
        Private ReadOnly _fullText As New StringBuilder
        Private ReadOnly _pendingText As New StringBuilder
        Private _lastFlushUtc As DateTime = DateTime.MinValue
        Private _started As Boolean = False

        Public Sub New(room As LakeUI.AgentRoom, item As LakeUI.AgentRoom.ChatItem)
            _room = room
            _item = item
        End Sub

        Public ReadOnly Property HasContent As Boolean
            Get
                Return _fullText.Length > 0
            End Get
        End Property

        Public ReadOnly Property Text As String
            Get
                Return _fullText.ToString()
            End Get
        End Property

        Public Sub Append(delta As String)
            If String.IsNullOrEmpty(delta) Then Return
            _fullText.Append(delta)
            _pendingText.Append(delta)

            Dim shouldFlush = _pendingText.Length >= StreamFlushCharacters OrElse
                delta.Contains(vbLf) OrElse
                (DateTime.UtcNow - _lastFlushUtc).TotalMilliseconds >= StreamFlushIntervalMs
            If shouldFlush Then Flush(False)
        End Sub

        Public Sub Flush(forceScroll As Boolean)
            If _pendingText.Length = 0 Then Return
            If Not _started Then
                If _item.Text = "正在思考..." OrElse
                    _item.Text = "正在重新连接..." OrElse
                    _item.Text.StartsWith("正在调用工具：") Then
                    _item.Text = ""
                End If
                _started = True
            End If

            _item.Text &= _pendingText.ToString()
            _pendingText.Clear()
            _lastFlushUtc = DateTime.UtcNow
            If forceScroll Then _room.ScrollToBottom()
        End Sub
    End Class

    Private Async Sub Form_v6_Agent_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _loading = True
        Try
            ModernListBox1.AllowDragReorder = True
            ModernListBox1.TabStop = True
            If MCB_联网设置.SelectedIndex < 0 Then MCB_联网设置.SelectedIndex = Math.Min(Math.Max(AgentNetworkMode.Normalize(设置_v6.实例对象.Agent联网设置), 0), MCB_联网设置.Items.Count - 1)
            If MCB_权限控制.SelectedIndex < 0 Then MCB_权限控制.SelectedIndex = Math.Min(Math.Max(设置_v6.实例对象.Agent权限级别, 0), MCB_权限控制.Items.Count - 1)

            _store = AgentConversationStore.Load()
            _current = _store.EnsureConversation()
            RefreshConversationList()
            RenderCurrentConversation()
            UpdateUsageButton()
        Finally
            _loading = False
        End Try

        Await RefreshModelsAsync()
    End Sub

    Private Function CreateClient() As AgentEndpointClient
        Return 网络功能.创建Agent端点客户端()
    End Function

    Private Function GetSelectedNetworkMode() As Integer
        Return AgentNetworkMode.Normalize(Math.Max(0, MCB_联网设置.SelectedIndex))
    End Function

    Public Async Sub 请求刷新模型列表()
        Await RefreshModelsAsync()
    End Sub

    Private Async Function RefreshModelsAsync() As Task
        Dim oldLoading = _loading
        _loading = True
        Try
            Dim client = CreateClient()
            If String.IsNullOrWhiteSpace(client.Endpoint) Then
                ShowStatus("请先在 Agent 设置中选择或填写端点。", True)
                ExFloatingTip(MCB_模型选择, "请先选择或填写 Agent 端点", 1800)
                Return
            End If

            ShowStatus("正在连接端点并获取模型列表")
            MCB_模型选择.Items.Clear()
            MCB_模型选择.Text = ""
            MCB_模型选择.WaterText = "正在获取模型"
            Dim modelResult = Await client.TryGetModelsAsync()
            If Not modelResult.Success Then
                ShowStatus("获取模型列表失败：" & modelResult.ErrorMessage, True)
                ExFloatingTip(MCB_模型选择, modelResult.ErrorMessage, 2600)
                Return
            End If
            _models = If(modelResult.Value, New List(Of AgentModelInfo))
            MCB_模型选择.Items.AddRange(_models.Select(Function(x) x.Id))

            If _models.Count = 0 Then
                ShowStatus("端点没有返回可用模型。", True)
                ExFloatingTip(MCB_模型选择, "端点没有返回可用模型", 1800)
                Return
            End If

            Dim selected = If(设置_v6.实例对象.AgentModelId, "")
            Dim index = _models.FindIndex(Function(x) String.Equals(x.Id, selected, StringComparison.OrdinalIgnoreCase))
            If index < 0 Then index = 0
            MCB_模型选择.SelectedIndex = index
            设置_v6.实例对象.AgentModelId = If(MCB_模型选择.SelectedItem, "")
            Await RefreshReasoningEffortsAsync()
            ShowStatus($"模型列表已刷新：{_models.Count} 个模型")
        Catch ex As Exception
            ShowStatus("系统故障：获取模型列表失败：" & ex.Message, True)
            ExFloatingTip(MCB_模型选择, ex.Message, 2600)
        Finally
            _loading = oldLoading
            MCB_模型选择.WaterText = "模型选择"
        End Try
    End Function

    Private Async Function RefreshReasoningEffortsAsync() As Task
        If MCB_模型选择.SelectedIndex < 0 OrElse MCB_模型选择.SelectedIndex >= _models.Count Then Return
        Dim oldLoading = _loading
        _loading = True
        Try
            Dim model = _models(MCB_模型选择.SelectedIndex)
            MCB_推理级别.Items.Clear()
            MCB_推理级别.Text = ""
            MCB_推理级别.WaterText = "正在读取"
            ShowStatus("正在读取推理级别：" & model.Id)
            Dim efforts = Await AgentCapabilityCache.GetReasoningEffortsAsync(CreateClient(), model)
            MCB_推理级别.Items.AddRange(efforts)

            Dim selected = If(设置_v6.实例对象.Agent推理级别, "")
            Dim index = efforts.FindIndex(Function(x) String.Equals(x, selected, StringComparison.OrdinalIgnoreCase))
            If index < 0 AndAlso efforts.Count > 0 Then index = Math.Min(1, efforts.Count - 1)
            If index >= 0 Then MCB_推理级别.SelectedIndex = index
            设置_v6.实例对象.Agent推理级别 = If(MCB_推理级别.SelectedItem, "")
            If model.ReasoningEfforts IsNot Nothing AndAlso model.ReasoningEfforts.Count > 0 Then
                ShowStatus("推理级别已就绪：" & String.Join("、", efforts))
            Else
                ShowStatus("推理级别使用默认值：" & String.Join("、", efforts))
            End If
        Catch ex As Exception
            ShowStatus("系统故障：读取推理级别失败：" & ex.Message, True)
            ExFloatingTip(MCB_推理级别, ex.Message, 2600)
        Finally
            _loading = oldLoading
            MCB_推理级别.WaterText = "推理级别"
        End Try
    End Function

    Private Sub RefreshConversationList()
        NormalizeConversationOrder()
        _orderedConversations = _store.Conversations.
            OrderBy(Function(x) If(x.SortOrder <= 0, Integer.MaxValue, x.SortOrder)).
            ThenByDescending(Function(x) x.UpdatedAt).
            ToList()
        ModernListBox1.Items.Clear()
        ModernListBox1.Items.AddRange(_orderedConversations.Select(Function(x) FormatConversationTitle(x)))

        Dim index = _orderedConversations.FindIndex(Function(x) x.Id = _current?.Id)
        If index >= 0 Then ModernListBox1.SelectedIndex = index
    End Sub

    Private Sub NormalizeConversationOrder()
        If _store Is Nothing OrElse _store.Conversations Is Nothing OrElse _store.Conversations.Count = 0 Then Return

        Dim hasManualOrder = _store.Conversations.Any(Function(x) x.SortOrder > 0)
        Dim ordered As List(Of AgentConversationData)
        If hasManualOrder Then
            ordered = _store.Conversations.
                OrderBy(Function(x) If(x.SortOrder > 0, 0, 1)).
                ThenBy(Function(x) If(x.SortOrder > 0, x.SortOrder, Integer.MaxValue)).
                ThenByDescending(Function(x) x.UpdatedAt).
                ToList()
        Else
            ordered = _store.Conversations.OrderByDescending(Function(x) x.UpdatedAt).ToList()
        End If

        For i = 0 To ordered.Count - 1
            ordered(i).SortOrder = i + 1
        Next
        _store.Conversations = ordered
    End Sub

    Private Function FormatConversationTitle(conversation As AgentConversationData) As String
        Dim title = If(conversation.Title, "").Trim()
        If title = "" Then title = "新对话"
        If title.Length > 28 Then title = title.Substring(0, 28) & "..."
        Return $"{title}  {conversation.UpdatedAt:MM-dd HH:mm}"
    End Function

    Private Sub RenderCurrentConversation()
        AgentRoom1.Clear()
        _statusItem = Nothing
        If _current Is Nothing Then Return
        For Each message In _current.Messages
            Select Case message.Role
                Case "user"
                    AgentRoom1.AddUserMessage(message.Content)
                Case "assistant"
                    If Not String.IsNullOrWhiteSpace(message.Content) Then AgentRoom1.AddAssistantMessage(message.Content)
                Case "tool"
                    AgentRoom1.AddCard(FormatToolCard(message))
                Case "card"
                    If Not String.IsNullOrWhiteSpace(message.Content) Then AgentRoom1.AddCard(message.Content)
            End Select
        Next
        AgentRoom1.ScrollToBottom()
    End Sub

    Private Function FormatToolCard(message As AgentMessageData) As String
        Return FormatToolCard(If(message.Name, "工具"), 0, If(message.Content, ""), False)
    End Function

    Private Function FormatToolCard(toolName As String, elapsedMilliseconds As Double, resultText As String, hasElapsed As Boolean) As String
        Dim title = $"{GetToolDisplayName(toolName)} 已完成"
        If hasElapsed Then title &= $"  {elapsedMilliseconds:F0} ms"
        Return $"{title}{vbCrLf}{BuildToolResultSummary(toolName, resultText)}"
    End Function

    Private Function FormatToolSummaryCard(roundLogs As IEnumerable(Of ToolRoundLog)) As String
        Dim rounds As New List(Of ToolRoundLog)
        If roundLogs IsNot Nothing Then
            rounds = roundLogs.
                Where(Function(x) x IsNot Nothing AndAlso x.Calls IsNot Nothing AndAlso x.Calls.Count > 0).
                ToList()
        End If
        If rounds.Count = 0 Then Return ""

        Dim totalCalls = rounds.Sum(Function(x) x.Calls.Count)
        Dim sb As New StringBuilder
        sb.AppendLine($"工具调用记录：{totalCalls} 次")

        For Each roundLog In rounds
            sb.AppendLine()
            sb.AppendLine($"第 {roundLog.Round} 轮思考")
            For Each callLog In roundLog.Calls
                sb.AppendLine(FormatToolRunSummary(callLog))
            Next
        Next

        Return sb.ToString().Trim()
    End Function

    Private Function FormatToolRunSummary(callLog As ToolRunLog) As String
        If callLog Is Nothing Then Return ""
        Dim title = $"{GetToolDisplayName(callLog.ToolName)}"
        If callLog.ElapsedMilliseconds >= 0 Then
            title &= $"  {callLog.ElapsedMilliseconds:F0} ms"
        Else
            title &= "  正在执行"
        End If

        Dim resultText = If(callLog.ResultText, "").Trim()
        If resultText = "" OrElse callLog.ElapsedMilliseconds < 0 Then Return $"  {title}"

        Dim summary = BuildToolResultSummary(callLog.ToolName, resultText)
        summary = "    " & summary.Replace(vbCrLf, vbLf).Replace(vbCr, vbLf).Replace(vbLf, vbCrLf & "    ")
        Return $"  {title}{vbCrLf}{summary}"
    End Function

    Private Sub UpdateToolSummaryCard(roundLogs As List(Of ToolRoundLog), ByRef cardItem As LakeUI.AgentRoom.ChatItem)
        Dim content = FormatToolSummaryCard(roundLogs)
        If String.IsNullOrWhiteSpace(content) Then Return

        If cardItem Is Nothing Then
            cardItem = AgentRoom1.AddCard(content)
        Else
            cardItem.Text = content
        End If
        AgentRoom1.ScrollToBottom()
    End Sub

    Private Sub AddToolSummaryMessage(roundLogs As List(Of ToolRoundLog))
        Dim content = FormatToolSummaryCard(roundLogs)
        If String.IsNullOrWhiteSpace(content) OrElse _current Is Nothing Then Return
        _current.Messages.Add(New AgentMessageData With {
            .Role = "card",
            .Name = "tool_summary",
            .Content = content
        })
    End Sub

    Private Function GetToolDisplayName(toolName As String) As String
        Select Case If(toolName, "").Trim()
            Case "get_parameter_panel_state"
                Return "读取参数面板"
            Case "apply_parameter_panel_patch"
                Return "修改参数面板"
            Case "get_queue_summary"
                Return "读取队列状态"
            Case "web_search"
                Return "联网搜索"
            Case "fetch_url"
                Return "读取网页"
            Case "read_local_text_file"
                Return "读取本地文件"
            Case "list_directory"
                Return "列举目录"
            Case "get_image_info"
                Return "读取图片信息"
            Case "run_powershell"
                Return "PowerShell 终端"
            Case Else
                Return If(String.IsNullOrWhiteSpace(toolName), "工具", toolName)
        End Select
    End Function

    Private Function BuildToolResultSummary(toolName As String, resultText As String) As String
        resultText = If(resultText, "").Trim()
        If resultText = "" Then Return "没有返回内容"

        If IsToolErrorResult(resultText) Then Return LimitSingleLine(resultText, 220)

        Select Case If(toolName, "").Trim()
            Case "get_parameter_panel_state"
                Return $"结果已收起，参数面板状态已送入上下文（{resultText.Length} 字符）。"
            Case "apply_parameter_panel_patch"
                Return LimitSingleLine(resultText, 180)
            Case "run_powershell"
                Return BuildPowerShellSummary(resultText)
            Case Else
                Return $"结果已收起，完整内容已送入上下文（{resultText.Length} 字符）。"
        End Select
    End Function

    Private Function BuildPowerShellSummary(resultText As String) As String
        Try
            Using doc = JsonDocument.Parse(resultText)
                Dim root = doc.RootElement
                Dim exitCode = GetJsonInt(root, "exit_code", -1)
                Dim timedOut = GetJsonBool(root, "timed_out", False)
                Dim elapsedMs = GetJsonInt(root, "elapsed_ms", 0)
                Dim workingDirectory = GetJsonString(root, "working_directory")
                Dim stdout = GetJsonString(root, "stdout")
                Dim stderr = GetJsonString(root, "stderr")

                Dim sb As New StringBuilder
                sb.AppendLine($"{If(timedOut, "已超时并终止", "已退出")}，退出码 {exitCode}，耗时 {elapsedMs} ms")
                If Not String.IsNullOrWhiteSpace(workingDirectory) Then sb.AppendLine("目录：" & workingDirectory)
                If Not String.IsNullOrWhiteSpace(stdout) Then
                    sb.AppendLine("stdout:")
                    sb.AppendLine(LimitLines(stdout, 8, 1200))
                End If
                If Not String.IsNullOrWhiteSpace(stderr) Then
                    sb.AppendLine("stderr:")
                    sb.AppendLine(LimitLines(stderr, 6, 900))
                End If
                If String.IsNullOrWhiteSpace(stdout) AndAlso String.IsNullOrWhiteSpace(stderr) Then sb.AppendLine("没有输出。")
                Return sb.ToString().Trim()
            End Using
        Catch
            Return LimitSingleLine(resultText, 260)
        End Try
    End Function

    Private Function GetJsonString(root As JsonElement, name As String) As String
        Dim value As JsonElement
        If root.ValueKind = JsonValueKind.Object AndAlso root.TryGetProperty(name, value) AndAlso value.ValueKind = JsonValueKind.String Then Return value.GetString()
        Return ""
    End Function

    Private Function GetJsonInt(root As JsonElement, name As String, defaultValue As Integer) As Integer
        Dim value As JsonElement
        If root.ValueKind = JsonValueKind.Object AndAlso root.TryGetProperty(name, value) AndAlso value.ValueKind = JsonValueKind.Number Then
            Dim result As Integer
            If value.TryGetInt32(result) Then Return result
        End If
        Return defaultValue
    End Function

    Private Function GetJsonBool(root As JsonElement, name As String, defaultValue As Boolean) As Boolean
        Dim value As JsonElement
        If root.ValueKind = JsonValueKind.Object AndAlso root.TryGetProperty(name, value) AndAlso value.ValueKind = JsonValueKind.True Then Return True
        If root.ValueKind = JsonValueKind.Object AndAlso root.TryGetProperty(name, value) AndAlso value.ValueKind = JsonValueKind.False Then Return False
        Return defaultValue
    End Function

    Private Function IsToolErrorResult(text As String) As Boolean
        Return text.StartsWith("权限不足", StringComparison.Ordinal) OrElse
            text.StartsWith("联网已禁用", StringComparison.Ordinal) OrElse
            text.StartsWith("读取失败", StringComparison.Ordinal) OrElse
            text.StartsWith("请求失败", StringComparison.Ordinal) OrElse
            text.StartsWith("工具失败", StringComparison.Ordinal) OrElse
            text.StartsWith("路径不存在", StringComparison.Ordinal) OrElse
            text.StartsWith("文件不存在", StringComparison.Ordinal)
    End Function

    Private Function LimitSingleLine(text As String, maxLength As Integer) As String
        text = If(text, "").Replace(vbCr, " ").Replace(vbLf, " ").Trim()
        Do While text.Contains("  ")
            text = text.Replace("  ", " ")
        Loop
        If text.Length <= maxLength Then Return text
        Return text.Substring(0, maxLength) & "..."
    End Function

    Private Function LimitLines(text As String, maxLines As Integer, maxCharacters As Integer) As String
        text = If(text, "").Replace(vbCrLf, vbLf).Replace(vbCr, vbLf)
        If text.Length > maxCharacters Then text = text.Substring(0, maxCharacters) & "...[已截断]"

        Dim lines = text.Split(ControlChars.Lf)
        Dim visible = lines.Take(maxLines).ToList()
        Dim result = String.Join(vbCrLf, visible)
        If lines.Length > maxLines Then result &= vbCrLf & "...[还有更多输出]"
        Return result.Trim()
    End Function

    Private Sub UpdateUsageButton()
        Dim currentUsage = If(_current?.Usage, New AgentUsageInfo)
        MB_页面用量.Text = $"当前 {currentUsage.TotalTokens} / 页面 {_pageUsage.TotalTokens} tokens"
    End Sub

    Private Sub ShowStatus(text As String, Optional keepRecord As Boolean = False)
        Dim content = $"状态 {DateTime.Now:HH:mm:ss}{vbCrLf}{If(text, "").Trim()}"
        If keepRecord OrElse _statusItem Is Nothing OrElse Not AgentRoom1.Items.Contains(_statusItem) Then
            _statusItem = AgentRoom1.AddCard(content)
        Else
            _statusItem.Text = content
        End If
        AgentRoom1.ScrollToBottom()
    End Sub

    Private Sub SaveCurrent()
        If _current IsNot Nothing Then _current.UpdatedAt = DateTime.Now
        _store.Save()
        RefreshConversationList()
        UpdateUsageButton()
    End Sub

    Private Sub MB_新对话_Click(sender As Object, e As EventArgs) Handles MB_新对话.Click
        If _store IsNot Nothing Then
            For Each conversation In _store.Conversations
                conversation.SortOrder += 1
            Next
        End If
        _current = New AgentConversationData With {
            .SortOrder = 1,
            .ModelId = If(MCB_模型选择.SelectedItem, 设置_v6.实例对象.AgentModelId),
            .ReasoningEffort = If(MCB_推理级别.SelectedItem, 设置_v6.实例对象.Agent推理级别),
            .NetworkMode = GetSelectedNetworkMode(),
            .PermissionLevel = Math.Max(0, MCB_权限控制.SelectedIndex)
        }
        _store.Conversations.Add(_current)
        SaveCurrent()
        RenderCurrentConversation()
        ShowStatus("已新建对话")
    End Sub

    Private Sub MB_删除对话_Click(sender As Object, e As EventArgs) Handles MB_删除对话.Click
        DeleteCurrentConversation()
    End Sub

    Private Sub DeleteCurrentConversation()
        If _current Is Nothing Then Return
        Dim confirm = ExMsgBox(FormMain_v6, "确认删除当前对话？", MsgBoxStyle.YesNo Or MsgBoxStyle.Question, "删除对话")
        If confirm <> MsgBoxResult.Yes Then Return

        Dim deletedTitle = If(_current.Title, "新对话")
        _store.Conversations.RemoveAll(Function(x) x.Id = _current.Id)
        If _store.Conversations.Count = 0 Then _store.Conversations.Add(New AgentConversationData)
        NormalizeConversationOrder()
        _current = _store.Conversations.OrderBy(Function(x) x.SortOrder).First()
        SaveCurrent()
        RenderCurrentConversation()
        ExFloatingTip(ModernListBox1, "已删除对话：" & deletedTitle, 1600)
    End Sub

    Private Sub RenameCurrentConversation()
        If _current Is Nothing Then Return
        Dim oldTitle = If(_current.Title, "新对话").Trim()
        Dim newTitle = ExInputBox(FormMain_v6, "请输入新的对话名称", "重命名对话", oldTitle).Trim()
        If newTitle = "" OrElse String.Equals(newTitle, oldTitle, StringComparison.Ordinal) Then Return
        _current.Title = newTitle
        SaveCurrent()
        ExFloatingTip(ModernListBox1, "已重命名对话", 1400)
    End Sub

    Private Async Sub MB_发送_Click(sender As Object, e As EventArgs) Handles MB_发送.Click
        If _busy Then
            RequestStopAgentTask(False)
            Return
        End If
        Await StartUserMessageAsync()
    End Sub

    Private Async Sub ModernTextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles ModernTextBox1.KeyDown
        If e.KeyCode = Keys.Enter AndAlso Not e.Shift Then
            e.SuppressKeyPress = True
            e.Handled = True
            If _busy Then
                Await OfferGuidanceMessageAsync()
            Else
                Await StartUserMessageAsync()
            End If
        End If
    End Sub

    Private Async Function StartUserMessageAsync() As Task
        If _busy Then Return
        Dim text = ModernTextBox1.Text.Trim()
        If text = "" Then Return
        If Not EnsureModelSelected() Then Return

        If _current Is Nothing Then _current = _store.EnsureConversation()
        ApplyConversationSnapshot()
        AppendUserMessage(text)
        ModernTextBox1.Text = ""
        SaveCurrent()

        Await StartAgentRunAsync()
    End Function

    Private Async Function OfferGuidanceMessageAsync() As Task
        Dim text = ModernTextBox1.Text.Trim()
        If text = "" Then Return

        Dim confirm = ExMsgBox(FormMain_v6,
            "当前任务仍在进行。是否将这条消息作为新的引导，并停止当前响应后重新生成？",
            MsgBoxStyle.YesNo Or MsgBoxStyle.Question,
            "引导对话")
        If confirm <> MsgBoxResult.Yes Then Return

        If _current Is Nothing Then _current = _store.EnsureConversation()
        ApplyConversationSnapshot()
        AppendUserMessage(text)
        ModernTextBox1.Text = ""
        _restartAfterCancel = True
        ShowStatus("已添加引导消息，正在停止当前响应")
        SaveCurrent()
        RequestStopAgentTask(True)
        Await Task.CompletedTask
    End Function

    Private Function EnsureModelSelected() As Boolean
        If MCB_模型选择.SelectedIndex >= 0 Then Return True
        ShowStatus("请先选择模型。", True)
        ExFloatingTip(MCB_模型选择, "请先选择模型", 1600)
        Return False
    End Function

    Private Sub AppendUserMessage(text As String)
        Dim userMessage As New AgentMessageData With {.Role = "user", .Content = text}
        _current.Messages.Add(userMessage)
        If _current.Title = "新对话" OrElse String.IsNullOrWhiteSpace(_current.Title) Then _current.Title = BuildTitle(text)
        AgentRoom1.AddUserMessage(text)
    End Sub

    Private Sub RequestStopAgentTask(restartAfterCancel As Boolean)
        If Not _busy Then Return
        If restartAfterCancel Then _restartAfterCancel = True
        If _requestCts IsNot Nothing AndAlso Not _requestCts.IsCancellationRequested Then
            _requestCts.Cancel()
            MB_发送.Text = "停止中"
            ShowStatus(If(restartAfterCancel, "正在按新的引导停止当前响应", "正在停止当前任务"))
        End If
    End Sub

    Private Async Function StartAgentRunAsync() As Task
        If _busy Then Return
        If Not EnsureModelSelected() Then Return

        _busy = True
        MB_发送.Enabled = True
        MB_发送.Text = "停止"
        Dim localCts As New Threading.CancellationTokenSource()
        _requestCts = localCts
        Dim pendingItem = AgentRoom1.AddAssistantMessage("正在思考...")
        Dim toolRoundLogs As New List(Of ToolRoundLog)
        Dim shouldRestart As Boolean = False
        Try
            ShowStatus("正在准备上下文")
            SaveCurrent()

            Await RunAgentLoopAsync(pendingItem, toolRoundLogs, localCts.Token)
        Catch ex As OperationCanceledException
            Dim canceledText = BuildCanceledResponseText(pendingItem, _restartAfterCancel)
            pendingItem.Text = canceledText
            If Not _restartAfterCancel Then _current.Messages.Add(New AgentMessageData With {.Role = "assistant", .Content = canceledText})
            ShowStatus(If(_restartAfterCancel, "已停止当前响应，准备按新的引导继续", "已停止当前任务"))
        Catch ex As Exception
            ShowStatus("系统故障：请求失败：" & ex.Message, True)
            pendingItem.Text = "请求失败：" & ex.Message
            _current.Messages.Add(New AgentMessageData With {.Role = "assistant", .Content = pendingItem.Text})
            ExFloatingTip(MB_发送, ex.Message, 2600)
        Finally
            shouldRestart = _restartAfterCancel AndAlso localCts.IsCancellationRequested
            _requestCts = Nothing
            localCts.Dispose()
            _busy = False
            MB_发送.Enabled = True
            MB_发送.Text = "发送"
            If Not shouldRestart Then AddToolSummaryMessage(toolRoundLogs)
            SaveCurrent()
            _restartAfterCancel = False
        End Try

        If shouldRestart Then Await StartAgentRunAsync()
    End Function

    Private Function BuildCanceledResponseText(pendingItem As LakeUI.AgentRoom.ChatItem, restarting As Boolean) As String
        Dim text = If(pendingItem?.Text, "").Trim()
        If text = "" OrElse
            text = "正在思考..." OrElse
            text = "正在重新连接..." OrElse
            text.StartsWith("正在调用工具：", StringComparison.Ordinal) Then
            Return If(restarting, "已按新的引导停止当前响应。", "已停止。")
        End If

        Return text & vbCrLf & vbCrLf & If(restarting, "（已被新的引导中断）", "（已停止）")
    End Function

    Private Sub ApplyConversationSnapshot()
        _current.ModelId = If(MCB_模型选择.SelectedItem, "")
        _current.ReasoningEffort = If(MCB_推理级别.SelectedItem, "")
        _current.NetworkMode = GetSelectedNetworkMode()
        _current.PermissionLevel = Math.Max(0, MCB_权限控制.SelectedIndex)
    End Sub

    Private Function BuildTitle(text As String) As String
        text = text.Replace(vbCr, " ").Replace(vbLf, " ").Trim()
        If text.Length > 18 Then text = text.Substring(0, 18) & "..."
        Return If(text = "", "新对话", text)
    End Function

    Private Async Function RunAgentLoopAsync(pendingItem As LakeUI.AgentRoom.ChatItem,
                                             toolRoundLogs As List(Of ToolRoundLog),
                                             cancellationToken As Threading.CancellationToken) As Task
        Dim client = CreateClient()
        Dim modelId = If(MCB_模型选择.SelectedItem, "")
        Dim reasoning = If(MCB_推理级别.SelectedItem, "")
        Dim networkMode = GetSelectedNetworkMode()
        Dim permissionLevel = Math.Max(0, MCB_权限控制.SelectedIndex)
        Dim tools = AgentLocalTools.BuildToolDefinitions(permissionLevel, networkMode)
        Dim messages = BuildRequestMessages()
        Dim toolSummaryCard As LakeUI.AgentRoom.ChatItem = Nothing
        Dim round As Integer = 0

        Do
            cancellationToken.ThrowIfCancellationRequested()
            round += 1
            Dim streamedAny As Boolean = False
            Dim result As AgentChatResult = Nothing
            Dim streamBuffer As New StreamingTextBuffer(AgentRoom1, pendingItem)

            ShowStatus($"正在思考：第 {round} 轮")
            result = Await client.TryCreateChatCompletionStreamingAsync(
                modelId,
                messages,
                tools,
                reasoning,
                Sub(delta)
                    streamedAny = True
                    streamBuffer.Append(delta)
                End Sub,
                cancellationToken)
            streamBuffer.Flush(True)
            cancellationToken.ThrowIfCancellationRequested()

            If Not result.Success Then
                ShowStatus("重新连接：流式响应不可用，切换非流式。" & result.ErrorMessage)
                If Not streamedAny Then pendingItem.Text = "正在重新连接..."
                result = Await client.TryCreateChatCompletionAsync(modelId, messages, tools, reasoning, cancellationToken)
                If Not result.Success Then
                    Dim errorText = "系统故障：请求失败：" & result.ErrorMessage
                    pendingItem.Text = errorText
                    _current.Messages.Add(New AgentMessageData With {.Role = "assistant", .Content = errorText})
                    ShowStatus(errorText, True)
                    ExFloatingTip(MB_发送, result.ErrorMessage, 2600)
                    Exit Function
                End If
            End If
            AddUsage(result.Usage)

            If result.ToolCalls Is Nothing OrElse result.ToolCalls.Count = 0 Then
                Dim content = If(result.Content, "").Trim()
                If content = "" Then content = "模型没有返回内容。"
                pendingItem.Text = content
                _current.Messages.Add(New AgentMessageData With {.Role = "assistant", .Content = content})
                ShowStatus("响应完成")
                Exit Function
            End If

            pendingItem.Text = "正在调用工具：" & String.Join("、", result.ToolCalls.Select(Function(x) x.Name))
            ShowStatus(pendingItem.Text)
            Dim assistantToolMessage As New AgentMessageData With {
                .Role = "assistant",
                .Content = If(result.Content, ""),
                .ToolCalls = result.ToolCalls
            }
            messages.Add(assistantToolMessage)

            Dim currentRoundLog As New ToolRoundLog With {.Round = round}
            toolRoundLogs.Add(currentRoundLog)
            For Each callInfo In result.ToolCalls
                cancellationToken.ThrowIfCancellationRequested()
                Dim callLog As New ToolRunLog With {
                    .Round = round,
                    .ToolName = callInfo.Name
                }
                currentRoundLog.Calls.Add(callLog)
                UpdateToolSummaryCard(toolRoundLogs, toolSummaryCard)

                Dim started = DateTime.Now
                ShowStatus("正在调用工具：" & callInfo.Name)
                Dim toolResult = Await AgentLocalTools.ExecuteAsync(callInfo, permissionLevel, networkMode, client, modelId, reasoning, cancellationToken)
                cancellationToken.ThrowIfCancellationRequested()
                Dim elapsed = DateTime.Now - started
                toolResult = LimitText(toolResult, 16000)
                callLog.ElapsedMilliseconds = elapsed.TotalMilliseconds
                callLog.ResultText = toolResult
                UpdateToolSummaryCard(toolRoundLogs, toolSummaryCard)

                Dim toolMessage As New AgentMessageData With {
                    .Role = "tool",
                    .Name = callInfo.Name,
                    .ToolCallId = callInfo.Id,
                    .Content = toolResult
                }
                messages.Add(toolMessage)
                ShowStatus($"工具完成：{callInfo.Name}，耗时 {elapsed.TotalMilliseconds:F0} ms")
            Next
        Loop
    End Function

    Private Function BuildRequestMessages() As List(Of AgentMessageData)
        Dim conversationMessages = _current.Messages.Where(Function(x) x.Role <> "card").ToList()
        Dim estimatedCharacters = conversationMessages.Sum(Function(x) EstimateMessageCharacters(x))
        If conversationMessages.Count > MaxRequestMessages OrElse estimatedCharacters > MaxRequestCharacters Then
            Dim omitted = Math.Max(0, conversationMessages.Count - KeepRecentMessages)
            ShowStatus($"正在压缩上下文：{conversationMessages.Count} 条消息，保留最近 {Math.Min(KeepRecentMessages, conversationMessages.Count)} 条")
            conversationMessages = conversationMessages.Skip(Math.Max(0, conversationMessages.Count - KeepRecentMessages)).ToList()
            conversationMessages.Insert(0, New AgentMessageData With {
                .Role = "system",
                .Content = $"上下文已压缩：较早的 {omitted} 条历史消息未放入本次请求。不要声称完整读取了被省略的历史，只基于当前可见上下文和工具结果回答。"
            })
        End If

        Dim systemText = Agent提示词_v6.构建系统提示词(
            GetSelectedNetworkMode(),
            If(MCB_权限控制.SelectedItem, "安全区域"))

        Dim result As New List(Of AgentMessageData) From {
            New AgentMessageData With {.Role = "system", .Content = systemText}
        }
        result.AddRange(conversationMessages)
        Return result
    End Function

    Private Function EstimateMessageCharacters(message As AgentMessageData) As Integer
        If message Is Nothing Then Return 0
        Dim total = If(message.Content, "").Length + If(message.Name, "").Length + If(message.ToolCallId, "").Length
        If message.ToolCalls IsNot Nothing Then
            total += message.ToolCalls.Sum(Function(x) If(x.Name, "").Length + If(x.Arguments, "").Length)
        End If
        Return total
    End Function

    Private Sub AddUsage(usage As AgentUsageInfo)
        If usage Is Nothing Then Return
        _pageUsage.Add(usage)
        If _current.Usage Is Nothing Then _current.Usage = New AgentUsageInfo
        _current.Usage.Add(usage)
        UpdateUsageButton()
    End Sub

    Private Function LimitText(text As String, maxLength As Integer) As String
        text = If(text, "")
        If text.Length <= maxLength Then Return text
        Return text.Substring(0, maxLength) & "..."
    End Function

    Private Sub ModernListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ModernListBox1.SelectedIndexChanged
        If _loading Then Return
        Dim index = ModernListBox1.SelectedIndex
        If index < 0 OrElse index >= _orderedConversations.Count Then Return
        _current = _orderedConversations(index)
        RenderCurrentConversation()
        UpdateUsageButton()
    End Sub

    Private Sub ModernListBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles ModernListBox1.KeyDown
        Select Case e.KeyCode
            Case Keys.Delete
                DeleteCurrentConversation()
                e.Handled = True
                e.SuppressKeyPress = True
            Case Keys.F2
                RenameCurrentConversation()
                e.Handled = True
                e.SuppressKeyPress = True
        End Select
    End Sub

    Private Sub ModernListBox1_ItemOrderChanged(sender As Object, e As EventArgs) Handles ModernListBox1.ItemOrderChanged
        If _loading OrElse _store Is Nothing OrElse _orderedConversations.Count = 0 Then Return

        Dim remaining As New List(Of AgentConversationData)(_orderedConversations)
        Dim reordered As New List(Of AgentConversationData)
        For Each itemText In ModernListBox1.Items
            Dim displayText = If(itemText, "")
            Dim index = remaining.FindIndex(Function(x) String.Equals(FormatConversationTitle(x), displayText, StringComparison.Ordinal))
            If index < 0 Then Continue For
            reordered.Add(remaining(index))
            remaining.RemoveAt(index)
        Next
        If reordered.Count <> _orderedConversations.Count Then Return

        For i = 0 To reordered.Count - 1
            reordered(i).SortOrder = i + 1
        Next
        _store.Conversations = reordered
        _orderedConversations = reordered
        _current = If(ModernListBox1.SelectedIndex >= 0 AndAlso ModernListBox1.SelectedIndex < _orderedConversations.Count, _orderedConversations(ModernListBox1.SelectedIndex), _current)
        _store.Save()
        RefreshConversationList()
        ShowStatus("已保存对话排序")
    End Sub

    Private Async Sub MCB_模型选择_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_模型选择.SelectedIndexChanged
        If _loading OrElse MCB_模型选择.SelectedIndex < 0 Then Return
        设置_v6.实例对象.AgentModelId = If(MCB_模型选择.SelectedItem, "")
        If _current IsNot Nothing Then _current.ModelId = 设置_v6.实例对象.AgentModelId
        Await RefreshReasoningEffortsAsync()
    End Sub

    Private Sub MCB_推理级别_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_推理级别.SelectedIndexChanged
        If _loading Then Return
        设置_v6.实例对象.Agent推理级别 = If(MCB_推理级别.SelectedItem, "")
        If _current IsNot Nothing Then _current.ReasoningEffort = 设置_v6.实例对象.Agent推理级别
    End Sub

    Private Sub MCB_联网设置_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_联网设置.SelectedIndexChanged
        If _loading Then Return
        设置_v6.实例对象.Agent联网设置 = GetSelectedNetworkMode()
        If _current IsNot Nothing Then _current.NetworkMode = 设置_v6.实例对象.Agent联网设置
    End Sub

    Private Sub MCB_权限控制_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_权限控制.SelectedIndexChanged
        If _loading Then Return
        设置_v6.实例对象.Agent权限级别 = Math.Max(0, MCB_权限控制.SelectedIndex)
        If _current IsNot Nothing Then _current.PermissionLevel = 设置_v6.实例对象.Agent权限级别
    End Sub

    Private Sub MB_页面用量_Click(sender As Object, e As EventArgs) Handles MB_页面用量.Click
        Dim currentUsage = If(_current?.Usage, New AgentUsageInfo)
        ExMsgBox(FormMain_v6, $"当前会话：{FormatUsage(currentUsage)}{vbCrLf}本页累计：{FormatUsage(_pageUsage)}", MsgBoxStyle.Information, "页面用量")
    End Sub

    Private Function FormatUsage(usage As AgentUsageInfo) As String
        Return $"总 {usage.TotalTokens}，输入 {usage.PromptTokens + usage.InputTokens}，输出 {usage.CompletionTokens + usage.OutputTokens}，缓存 {usage.CachedTokens}，推理 {usage.ReasoningTokens}"
    End Function
End Class
