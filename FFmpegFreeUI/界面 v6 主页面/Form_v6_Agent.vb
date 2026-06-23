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
    Private _activeResponseItem As LakeUI.AgentRoom.ChatItem = Nothing
    Private _activeRunConversation As AgentConversationData = Nothing
    Private _activeToolRoundLogs As List(Of ToolRoundLog) = Nothing
    Private _activeToolSummaryItem As LakeUI.AgentRoom.ChatItem = Nothing
    Private _activeRunStatusItem As LakeUI.AgentRoom.ChatItem = Nothing
    Private _activeResponseText As String = ""
    Private _activeRunStatusText As String = ""
    Private _restartRunConversation As AgentConversationData = Nothing
    Private _modelEndpointSignature As String = ""
    Private _refreshingModels As Boolean = False
    Private _pendingModelRefresh As Boolean = False

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

    Private Class ToolSummaryGroup
        Public Property ToolName As String = ""
        Public Property Count As Integer
        Public Property CompletedCount As Integer
        Public Property RunningCount As Integer
        Public Property ElapsedMilliseconds As Double
        Public Property AddedCharacters As Integer
    End Class

    Private Class StreamingTextBuffer
        Private ReadOnly _room As LakeUI.AgentRoom
        Private ReadOnly _item As LakeUI.AgentRoom.ChatItem
        Private ReadOnly _onTextChanged As Action(Of String)
        Private ReadOnly _fullText As New StringBuilder
        Private ReadOnly _pendingText As New StringBuilder
        Private _lastFlushUtc As DateTime = DateTime.MinValue
        Private _started As Boolean = False

        Public Sub New(room As LakeUI.AgentRoom, item As LakeUI.AgentRoom.ChatItem, Optional onTextChanged As Action(Of String) = Nothing)
            _room = room
            _item = item
            _onTextChanged = onTextChanged
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
            _onTextChanged?.Invoke(_fullText.ToString())

            Dim shouldFlush = _pendingText.Length >= StreamFlushCharacters OrElse
                delta.Contains(vbLf) OrElse
                (DateTime.UtcNow - _lastFlushUtc).TotalMilliseconds >= StreamFlushIntervalMs
            If shouldFlush Then Flush(False)
        End Sub

        Public Sub Flush(forceScroll As Boolean)
            If _pendingText.Length = 0 Then Return
            If _item Is Nothing Then
                _pendingText.Clear()
                _lastFlushUtc = DateTime.UtcNow
                Return
            End If
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

        Await RefreshModelsIfEndpointChangedAsync(False)
    End Sub

    Private Function CreateClient() As AgentEndpointClient
        Return 网络功能.创建Agent端点客户端()
    End Function

    Private Function GetSelectedNetworkMode() As Integer
        Return AgentNetworkMode.Normalize(Math.Max(0, MCB_联网设置.SelectedIndex))
    End Function

    Public Async Sub 请求刷新模型列表()
        Await RefreshModelsIfEndpointChangedAsync(True)
    End Sub

    Public Async Sub 检查并刷新模型列表()
        Await RefreshModelsIfEndpointChangedAsync(False)
    End Sub

    Private Function BuildModelEndpointSignature() As String
        Return AgentCapabilityCache.BuildEndpointSignature(CreateClient())
    End Function

    Private Async Function RefreshModelsIfEndpointChangedAsync(force As Boolean) As Task
        Dim client As AgentEndpointClient
        Dim signature As String
        Dim dailyReasoningRefreshDue As Boolean
        Try
            client = CreateClient()
            signature = AgentCapabilityCache.BuildEndpointSignature(client)
            dailyReasoningRefreshDue = AgentCapabilityCache.IsDailyReasoningRefreshDue(client)
        Catch ex As Exception
            ShowStatus("Agent 端点配置无效：" & ex.Message, True)
            Return
        End Try

        If Not force AndAlso
            Not dailyReasoningRefreshDue AndAlso
            String.Equals(signature, _modelEndpointSignature, StringComparison.Ordinal) Then Return

        Await RefreshModelsAsync(signature)
    End Function

    Private Async Function RefreshModelsAsync(Optional expectedSignature As String = Nothing) As Task
        If _refreshingModels Then
            _pendingModelRefresh = True
            Return
        End If

        Try
            If expectedSignature Is Nothing Then expectedSignature = BuildModelEndpointSignature()
        Catch ex As Exception
            ShowStatus("Agent 端点配置无效：" & ex.Message, True)
            Return
        End Try

        _refreshingModels = True
        Dim oldLoading = _loading
        _loading = True
        Try
            Dim client = CreateClient()
            Dim actualSignature = BuildModelEndpointSignature()
            expectedSignature = actualSignature
            If String.IsNullOrWhiteSpace(client.Endpoint) Then
                _modelEndpointSignature = actualSignature
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
            If Not String.Equals(expectedSignature, BuildModelEndpointSignature(), StringComparison.Ordinal) Then
                _pendingModelRefresh = True
                Return
            End If

            _models = If(modelResult.Value, New List(Of AgentModelInfo))
            AgentCapabilityCache.ImportReasoningEfforts(client, _models)
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
            _modelEndpointSignature = expectedSignature
            ShowStatus($"模型列表已刷新：{_models.Count} 个模型")
        Catch ex As Exception
            ShowStatus("系统故障：获取模型列表失败：" & ex.Message, True)
            ExFloatingTip(MCB_模型选择, ex.Message, 2600)
        Finally
            _loading = oldLoading
            _refreshingModels = False
            MCB_模型选择.WaterText = "模型选择"
        End Try

        If _pendingModelRefresh Then
            _pendingModelRefresh = False
            Await RefreshModelsIfEndpointChangedAsync(False)
        End If
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
        Dim oldLoading = _loading
        _loading = True
        Try
            ModernListBox1.Items.Clear()
            ModernListBox1.Items.AddRange(_orderedConversations.Select(Function(x) FormatConversationTitle(x)))

            Dim index = _orderedConversations.FindIndex(Function(x) _current IsNot Nothing AndAlso x.Id = _current.Id)
            If index >= 0 Then ModernListBox1.SelectedIndex = index
        Finally
            _loading = oldLoading
        End Try
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
        If _current Is Nothing Then
            UpdateSendButtonState()
            Return
        End If
        Dim i = 0
        While i < _current.Messages.Count
            Dim message = _current.Messages(i)
            Select Case message.Role
                Case "user"
                    AgentRoom1.AddUserMessage(message.Content)
                Case "assistant"
                    If i + 1 < _current.Messages.Count AndAlso IsToolSummaryMessage(_current.Messages(i + 1)) Then
                        AddStoredCard(_current.Messages(i + 1))
                        i += 1
                    End If
                    If Not String.IsNullOrWhiteSpace(message.Content) Then AgentRoom1.AddAssistantMessage(message.Content)
                Case "tool"
                    ' 工具详情只用于上下文，不再作为聊天正文渲染。
                Case "card"
                    AddStoredCard(message)
            End Select
            i += 1
        End While
        RenderActiveRunOverlay()
        UpdateSendButtonState()
        AgentRoom1.ScrollToBottom()
    End Sub

    Private Function IsToolSummaryMessage(message As AgentMessageData) As Boolean
        Return message IsNot Nothing AndAlso _
            String.Equals(message.Role, "card", StringComparison.OrdinalIgnoreCase) AndAlso _
            String.Equals(If(message.Name, ""), "tool_summary", StringComparison.OrdinalIgnoreCase)
    End Function

    Private Sub AddStoredCard(message As AgentMessageData)
        If message Is Nothing OrElse String.IsNullOrWhiteSpace(message.Content) Then Return
        AgentRoom1.AddCard(message.Content)
    End Sub

    Private Function FormatToolSummaryCard(roundLogs As IEnumerable(Of ToolRoundLog)) As String
        Dim calls As New List(Of ToolRunLog)
        If roundLogs IsNot Nothing Then
            calls = roundLogs.
                Where(Function(x) x IsNot Nothing AndAlso x.Calls IsNot Nothing AndAlso x.Calls.Count > 0).
                SelectMany(Function(x) x.Calls).
                Where(Function(x) x IsNot Nothing).
                ToList()
        End If
        If calls.Count = 0 Then Return ""

        Dim groups = BuildToolSummaryGroups(calls)
        Dim totalCalls = calls.Count
        Dim completedCount = calls.Where(Function(x) x.ElapsedMilliseconds >= 0).Count()
        Dim runningCount = totalCalls - completedCount
        Dim totalElapsed = groups.Sum(Function(x) x.ElapsedMilliseconds)
        Dim totalCharacters = groups.Sum(Function(x) x.AddedCharacters)
        Dim sb As New StringBuilder
        sb.AppendLine(FormatToolTotalSummary(totalCalls, completedCount, runningCount, totalElapsed, totalCharacters))

        For Each group In groups
            sb.AppendLine(FormatToolGroupSummary(group))
        Next

        Return sb.ToString().Trim()
    End Function

    Private Function BuildToolSummaryGroups(calls As IEnumerable(Of ToolRunLog)) As List(Of ToolSummaryGroup)
        Dim result As New List(Of ToolSummaryGroup)
        For Each group In calls.GroupBy(Function(x) If(x.ToolName, "").Trim(), StringComparer.OrdinalIgnoreCase)
            Dim summary As New ToolSummaryGroup With {
                .ToolName = group.First().ToolName,
                .Count = group.Count(),
                .CompletedCount = group.Where(Function(x) x.ElapsedMilliseconds >= 0).Count(),
                .RunningCount = group.Where(Function(x) x.ElapsedMilliseconds < 0).Count(),
                .ElapsedMilliseconds = group.Where(Function(x) x.ElapsedMilliseconds >= 0).Sum(Function(x) x.ElapsedMilliseconds),
                .AddedCharacters = group.Sum(Function(x) If(x.ResultText, "").Length)
            }
            result.Add(summary)
        Next
        Return result
    End Function

    Private Function FormatToolTotalSummary(totalCalls As Integer,
                                            completedCount As Integer,
                                            runningCount As Integer,
                                            elapsedMilliseconds As Double,
                                            addedCharacters As Integer) As String
        If completedCount = 0 Then Return $"工具调用：{totalCalls} 次 正在执行"

        Dim text = $"工具调用：{totalCalls} 次 {FormatElapsedMilliseconds(elapsedMilliseconds)} +{addedCharacters} 字符"
        If runningCount > 0 Then text &= $"，{runningCount} 次进行中"
        Return text
    End Function

    Private Function FormatToolGroupSummary(group As ToolSummaryGroup) As String
        If group Is Nothing Then Return ""

        Dim countText = If(group.Count > 1, $"{group.Count} 次 ", "")
        If group.CompletedCount = 0 Then Return $"{GetToolDisplayName(group.ToolName)} {countText}正在执行".Trim()

        Dim text = $"{GetToolDisplayName(group.ToolName)} {countText}{FormatElapsedMilliseconds(group.ElapsedMilliseconds)} +{group.AddedCharacters} 字符".Trim()
        If group.RunningCount > 0 Then text &= $"，{group.RunningCount} 次进行中"
        Return text
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

    Private Sub UpdateToolSummaryCard(roundLogs As List(Of ToolRoundLog),
                                      ByRef cardItem As LakeUI.AgentRoom.ChatItem,
                                      Optional beforeItem As LakeUI.AgentRoom.ChatItem = Nothing)
        Dim content = FormatToolSummaryCard(roundLogs)
        If String.IsNullOrWhiteSpace(content) Then Return

        If cardItem Is Nothing Then
            cardItem = New LakeUI.AgentRoom.ChatItem(LakeUI.AgentRoom.ChatItemKind.Card, content)
            Dim insertIndex = If(beforeItem Is Nothing, -1, AgentRoom1.Items.IndexOf(beforeItem))
            If insertIndex >= 0 Then
                AgentRoom1.Items.Insert(insertIndex, cardItem)
            Else
                AgentRoom1.Items.Add(cardItem)
            End If
        Else
            cardItem.Text = content
        End If
        AgentRoom1.ScrollToBottom()
    End Sub

    Private Sub AddToolSummaryMessage(conversation As AgentConversationData, roundLogs As List(Of ToolRoundLog))
        Dim content = FormatToolSummaryCard(roundLogs)
        If String.IsNullOrWhiteSpace(content) OrElse conversation Is Nothing Then Return
        Dim message As New AgentMessageData With {
            .Role = "card",
            .Name = "tool_summary",
            .Content = content
        }

        Dim insertIndex = conversation.Messages.Count
        If insertIndex > 0 AndAlso String.Equals(conversation.Messages(insertIndex - 1).Role, "assistant", StringComparison.OrdinalIgnoreCase) Then
            insertIndex -= 1
        End If
        conversation.Messages.Insert(insertIndex, message)
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
                Return "读取队列状态"
            Case "sync_parameter_panel_to_queue"
                Return "同步参数到队列"
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

    Private Sub UpdateUsageButton()
        Dim currentUsage = If(_current?.Usage, New AgentUsageInfo)
        MB_页面用量.Text = $"当前 {currentUsage.TotalTokens} / 页面 {_pageUsage.TotalTokens} tokens"
    End Sub

    Private Sub UpdateSendButtonState()
        If _busy AndAlso IsConversationSelected(_activeRunConversation) Then
            MB_发送.Text = If(_requestCts IsNot Nothing AndAlso _requestCts.IsCancellationRequested, "停止中", "停止")
        Else
            MB_发送.Text = "发送"
        End If
        MB_发送.Enabled = True
    End Sub

    Private Sub ShowStatus(text As String, Optional keepRecord As Boolean = False)
        Dim content = $"状态 {DateTime.Now:HH:mm:ss}{vbCrLf}{If(text, "").Trim()}"
        If keepRecord OrElse _statusItem Is Nothing OrElse Not AgentRoom1.Items.Contains(_statusItem) Then
            _statusItem = AddCardBeforeActiveResponse(content)
        Else
            _statusItem.Text = content
        End If
        AgentRoom1.ScrollToBottom()
    End Sub

    Private Sub ShowRunStatus(conversation As AgentConversationData, text As String, Optional keepRecord As Boolean = False)
        Dim content = $"状态 {DateTime.Now:HH:mm:ss}{vbCrLf}{If(text, "").Trim()}"
        If ReferenceEquals(conversation, _activeRunConversation) Then _activeRunStatusText = content
        If Not IsConversationSelected(conversation) Then Return

        If keepRecord OrElse _activeRunStatusItem Is Nothing OrElse Not AgentRoom1.Items.Contains(_activeRunStatusItem) Then
            _activeRunStatusItem = AddCardBeforeActiveResponse(content)
        Else
            _activeRunStatusItem.Text = content
        End If
        AgentRoom1.ScrollToBottom()
    End Sub

    Private Function AddCardBeforeActiveResponse(content As String) As LakeUI.AgentRoom.ChatItem
        Dim item As New LakeUI.AgentRoom.ChatItem(LakeUI.AgentRoom.ChatItemKind.Card, content)
        Dim insertIndex = If(_activeResponseItem Is Nothing, -1, AgentRoom1.Items.IndexOf(_activeResponseItem))
        If insertIndex >= 0 Then
            AgentRoom1.Items.Insert(insertIndex, item)
        Else
            AgentRoom1.Items.Add(item)
        End If
        Return item
    End Function

    Private Function IsConversationSelected(conversation As AgentConversationData) As Boolean
        Return conversation IsNot Nothing AndAlso _current IsNot Nothing AndAlso String.Equals(conversation.Id, _current.Id, StringComparison.OrdinalIgnoreCase)
    End Function

    Private Sub SetRunResponseText(conversation As AgentConversationData, text As String)
        If ReferenceEquals(conversation, _activeRunConversation) Then _activeResponseText = If(text, "")
        If Not IsConversationSelected(conversation) Then Return

        If _activeResponseItem Is Nothing OrElse Not AgentRoom1.Items.Contains(_activeResponseItem) Then
            _activeResponseItem = AgentRoom1.AddAssistantMessage(_activeResponseText)
        Else
            _activeResponseItem.Text = _activeResponseText
        End If
        AgentRoom1.ScrollToBottom()
    End Sub

    Private Sub RefreshActiveToolSummaryCard(conversation As AgentConversationData)
        If Not ReferenceEquals(conversation, _activeRunConversation) Then Return
        If _activeToolRoundLogs Is Nothing Then Return
        If Not IsConversationSelected(conversation) Then Return
        UpdateToolSummaryCard(_activeToolRoundLogs, _activeToolSummaryItem, _activeResponseItem)
    End Sub

    Private Sub RenderActiveRunOverlay()
        _activeRunStatusItem = Nothing
        _activeToolSummaryItem = Nothing
        _activeResponseItem = Nothing
        If _activeRunConversation Is Nothing OrElse Not IsConversationSelected(_activeRunConversation) Then Return

        If Not String.IsNullOrWhiteSpace(_activeRunStatusText) Then
            _activeRunStatusItem = AgentRoom1.AddCard(_activeRunStatusText)
        End If

        _activeResponseItem = AgentRoom1.AddAssistantMessage(If(String.IsNullOrWhiteSpace(_activeResponseText), "正在思考...", _activeResponseText))

        If _activeToolRoundLogs IsNot Nothing AndAlso _activeToolRoundLogs.Count > 0 Then
            Dim content = FormatToolSummaryCard(_activeToolRoundLogs)
            If Not String.IsNullOrWhiteSpace(content) Then
                _activeToolSummaryItem = New LakeUI.AgentRoom.ChatItem(LakeUI.AgentRoom.ChatItemKind.Card, content)
                Dim insertIndex = AgentRoom1.Items.IndexOf(_activeResponseItem)
                If insertIndex >= 0 Then
                    AgentRoom1.Items.Insert(insertIndex, _activeToolSummaryItem)
                Else
                    AgentRoom1.Items.Add(_activeToolSummaryItem)
                End If
            End If
        End If
    End Sub

    Private Sub ClearActiveRunState()
        _activeResponseItem = Nothing
        _activeRunConversation = Nothing
        _activeToolRoundLogs = Nothing
        _activeToolSummaryItem = Nothing
        _activeRunStatusItem = Nothing
        _activeResponseText = ""
        _activeRunStatusText = ""
    End Sub

    Private Sub SaveCurrent()
        SaveConversation(_current)
    End Sub

    Private Sub SaveConversation(conversation As AgentConversationData)
        If conversation IsNot Nothing Then conversation.UpdatedAt = DateTime.Now
        _store.Save()
        RefreshConversationList()
        UpdateUsageButton()
    End Sub

    Private Sub MB_新对话_Click(sender As Object, e As EventArgs) Handles MB_新对话.Click
        _current = CreateConversationFromCurrentSettings()
        SaveCurrent()
        RenderCurrentConversation()
        ShowStatus("已新建对话")
    End Sub

    Private Function CreateConversationFromCurrentSettings() As AgentConversationData
        If _store IsNot Nothing Then
            For Each storedConversation In _store.Conversations
                storedConversation.SortOrder += 1
            Next
        End If
        Dim newConversation As New AgentConversationData With {
            .SortOrder = 1,
            .ModelId = If(MCB_模型选择.SelectedItem, 设置_v6.实例对象.AgentModelId),
            .ReasoningEffort = If(MCB_推理级别.SelectedItem, 设置_v6.实例对象.Agent推理级别),
            .NetworkMode = GetSelectedNetworkMode(),
            .PermissionLevel = Math.Max(0, MCB_权限控制.SelectedIndex)
        }
        _store.Conversations.Add(newConversation)
        Return newConversation
    End Function

    Private Sub MB_删除对话_Click(sender As Object, e As EventArgs) Handles MB_删除对话.Click
        DeleteCurrentConversation()
    End Sub

    Private Sub DeleteCurrentConversation()
        If _current Is Nothing Then Return
        If _busy AndAlso IsConversationSelected(_activeRunConversation) Then
            ExFloatingTip(ModernListBox1, "当前对话正在响应，请先停止任务后再删除", 2200)
            Return
        End If

        Dim confirm = ExMsgBox(FormMain_v6, "确认删除当前对话？", MsgBoxStyle.YesNo Or MsgBoxStyle.Question, "删除对话")
        If confirm <> MsgBoxResult.Yes Then Return

        Dim deletedTitle = If(_current.Title, "新对话")
        _store.Conversations.RemoveAll(Function(x) x.Id = _current.Id)
        NormalizeConversationOrder()
        _current = Nothing
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
            If IsConversationSelected(_activeRunConversation) Then
                RequestStopAgentTask(False)
            Else
                ExFloatingTip(MB_发送, "当前已有其他对话正在响应，请先切回该对话或停止任务", 2200)
            End If
            Return
        End If
        Await StartUserMessageAsync()
    End Sub

    Private Async Sub ModernTextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles ModernTextBox1.KeyDown
        If e.KeyCode = Keys.Enter AndAlso Not e.Shift Then
            e.SuppressKeyPress = True
            e.Handled = True
            If _busy Then
                If IsConversationSelected(_activeRunConversation) Then
                    Await OfferGuidanceMessageAsync()
                Else
                    ExFloatingTip(ModernTextBox1, "当前已有其他对话正在响应，请先切回该对话或停止任务", 2200)
                End If
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

        If _current Is Nothing Then _current = CreateConversationFromCurrentSettings()
        ApplyConversationSnapshot()
        AppendUserMessage(text)
        ModernTextBox1.Text = ""
        SaveCurrent()

        Await StartAgentRunAsync(_current)
    End Function

    Private Async Function OfferGuidanceMessageAsync() As Task
        Dim text = ModernTextBox1.Text.Trim()
        If text = "" Then Return
        If _busy AndAlso Not IsConversationSelected(_activeRunConversation) Then
            ExFloatingTip(ModernTextBox1, "当前已有其他对话正在响应，请先切回该对话或停止任务", 2200)
            Return
        End If

        Dim confirm = ExMsgBox(FormMain_v6,
            "当前任务仍在进行。是否将这条消息作为新的引导，并停止当前响应后重新生成？",
            MsgBoxStyle.YesNo Or MsgBoxStyle.Question,
            "引导对话")
        If confirm <> MsgBoxResult.Yes Then Return

        If _current Is Nothing Then _current = CreateConversationFromCurrentSettings()
        ApplyConversationSnapshot()
        AppendUserMessage(text)
        ModernTextBox1.Text = ""
        _restartAfterCancel = True
        _restartRunConversation = _current
        ShowRunStatus(_restartRunConversation, "已添加引导消息，正在停止当前响应")
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
            ShowRunStatus(_activeRunConversation, If(restartAfterCancel, "正在按新的引导停止当前响应", "正在停止当前任务"))
        End If
    End Sub

    Private Async Function StartAgentRunAsync(Optional conversation As AgentConversationData = Nothing) As Task
        If _busy Then Return
        Dim runConversation = If(conversation, _current)
        If runConversation Is Nothing Then Return
        If String.IsNullOrWhiteSpace(runConversation.ModelId) Then
            If IsConversationSelected(runConversation) Then
                ShowStatus("请先选择模型。", True)
                ExFloatingTip(MCB_模型选择, "请先选择模型", 1600)
            End If
            Return
        End If

        _busy = True
        MB_发送.Enabled = True
        Dim localCts As New Threading.CancellationTokenSource()
        _requestCts = localCts
        _activeRunConversation = runConversation
        _activeResponseText = "正在思考..."
        _activeRunStatusText = ""
        _activeRunStatusItem = Nothing
        _activeResponseItem = Nothing
        If IsConversationSelected(runConversation) Then _activeResponseItem = AgentRoom1.AddAssistantMessage("正在思考...")
        UpdateSendButtonState()
        Dim toolRoundLogs As New List(Of ToolRoundLog)
        _activeToolRoundLogs = toolRoundLogs
        _activeToolSummaryItem = Nothing
        Dim shouldRestart As Boolean = False
        Try
            ShowRunStatus(runConversation, "正在准备上下文")
            SaveConversation(runConversation)

            Await RunAgentLoopAsync(runConversation, toolRoundLogs, localCts.Token)
        Catch ex As OperationCanceledException
            Dim canceledText = BuildCanceledResponseText(_activeResponseText, _restartAfterCancel)
            SetRunResponseText(runConversation, canceledText)
            If Not _restartAfterCancel Then runConversation.Messages.Add(New AgentMessageData With {.Role = "assistant", .Content = canceledText})
            ShowRunStatus(runConversation, If(_restartAfterCancel, "已停止当前响应，准备按新的引导继续", "已停止当前任务"))
        Catch ex As Exception
            ShowRunStatus(runConversation, "系统故障：请求失败：" & ex.Message, True)
            SetRunResponseText(runConversation, "请求失败：" & ex.Message)
            runConversation.Messages.Add(New AgentMessageData With {.Role = "assistant", .Content = _activeResponseText})
            ExFloatingTip(MB_发送, ex.Message, 2600)
        Finally
            shouldRestart = _restartAfterCancel AndAlso localCts.IsCancellationRequested
            _requestCts = Nothing
            localCts.Dispose()
            _busy = False
            MB_发送.Enabled = True
            If Not shouldRestart Then AddToolSummaryMessage(runConversation, toolRoundLogs)
            SaveConversation(runConversation)
            Dim shouldRenderConversation = IsConversationSelected(runConversation)
            ClearActiveRunState()
            _restartAfterCancel = False
            If shouldRenderConversation Then RenderCurrentConversation()
            If Not shouldRestart Then UpdateSendButtonState()
        End Try

        If shouldRestart Then
            Dim restartConversation = If(_restartRunConversation, runConversation)
            _restartRunConversation = Nothing
            Await StartAgentRunAsync(restartConversation)
        Else
            _restartRunConversation = Nothing
        End If
    End Function

    Private Function BuildCanceledResponseText(currentText As String, restarting As Boolean) As String
        Dim text = If(currentText, "").Trim()
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

    Private Async Function RunAgentLoopAsync(conversation As AgentConversationData,
                                             toolRoundLogs As List(Of ToolRoundLog),
                                             cancellationToken As Threading.CancellationToken) As Task
        Dim client = CreateClient()
        Dim modelId = If(conversation.ModelId, "")
        Dim reasoning = If(conversation.ReasoningEffort, "")
        Dim networkMode = AgentNetworkMode.Normalize(conversation.NetworkMode)
        Dim permissionLevel = Math.Max(0, conversation.PermissionLevel)
        Dim tools = AgentLocalTools.BuildToolDefinitions(permissionLevel, networkMode)
        Dim messages = BuildRequestMessages(conversation)
        Dim round As Integer = 0

        Do
            cancellationToken.ThrowIfCancellationRequested()
            round += 1
            Dim streamedAny As Boolean = False
            Dim result As AgentChatResult = Nothing
            Dim streamBuffer As New StreamingTextBuffer(AgentRoom1, Nothing, Sub(value) SetRunResponseText(conversation, value))

            ShowRunStatus(conversation, $"正在思考：第 {round} 轮")
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
                ShowRunStatus(conversation, "重新连接：流式响应不可用，切换非流式。" & result.ErrorMessage)
                If Not streamedAny Then SetRunResponseText(conversation, "正在重新连接...")
                result = Await client.TryCreateChatCompletionAsync(modelId, messages, tools, reasoning, cancellationToken)
                If Not result.Success Then
                    Dim errorText = "系统故障：请求失败：" & result.ErrorMessage
                    SetRunResponseText(conversation, errorText)
                    conversation.Messages.Add(New AgentMessageData With {.Role = "assistant", .Content = errorText})
                    ShowRunStatus(conversation, errorText, True)
                    ExFloatingTip(MB_发送, result.ErrorMessage, 2600)
                    Exit Function
                End If
            End If
            AddUsage(conversation, result.Usage)

            If result.ToolCalls Is Nothing OrElse result.ToolCalls.Count = 0 Then
                Dim content = If(result.Content, "").Trim()
                If content = "" Then content = "模型没有返回内容。"
                SetRunResponseText(conversation, content)
                conversation.Messages.Add(New AgentMessageData With {.Role = "assistant", .Content = content})
                ShowRunStatus(conversation, "响应完成")
                Exit Function
            End If

            SetRunResponseText(conversation, "正在调用工具：" & String.Join("、", result.ToolCalls.Select(Function(x) x.Name)))
            ShowRunStatus(conversation, _activeResponseText)
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
                RefreshActiveToolSummaryCard(conversation)

                Dim started = DateTime.Now
                ShowRunStatus(conversation, "正在调用工具：" & callInfo.Name)
                Dim toolResult = Await AgentLocalTools.ExecuteAsync(callInfo, permissionLevel, networkMode, client, modelId, reasoning, cancellationToken)
                cancellationToken.ThrowIfCancellationRequested()
                Dim elapsed = DateTime.Now - started
                toolResult = LimitText(toolResult, 16000)
                callLog.ElapsedMilliseconds = elapsed.TotalMilliseconds
                callLog.ResultText = toolResult
                RefreshActiveToolSummaryCard(conversation)

                Dim toolMessage As New AgentMessageData With {
                    .Role = "tool",
                    .Name = callInfo.Name,
                    .ToolCallId = callInfo.Id,
                    .Content = toolResult
                }
                messages.Add(toolMessage)
                ShowRunStatus(conversation, $"工具完成：{callInfo.Name}，耗时 {FormatElapsedMilliseconds(elapsed.TotalMilliseconds)}")
            Next
        Loop
    End Function

    Private Function BuildRequestMessages(conversation As AgentConversationData) As List(Of AgentMessageData)
        Dim conversationMessages = conversation.Messages.Where(Function(x) x.Role <> "card").ToList()
        Dim estimatedCharacters = conversationMessages.Sum(Function(x) EstimateMessageCharacters(x))
        If conversationMessages.Count > MaxRequestMessages OrElse estimatedCharacters > MaxRequestCharacters Then
            Dim omitted = Math.Max(0, conversationMessages.Count - KeepRecentMessages)
            ShowRunStatus(conversation, $"正在压缩上下文：{conversationMessages.Count} 条消息，保留最近 {Math.Min(KeepRecentMessages, conversationMessages.Count)} 条")
            conversationMessages = conversationMessages.Skip(Math.Max(0, conversationMessages.Count - KeepRecentMessages)).ToList()
            conversationMessages.Insert(0, New AgentMessageData With {
                .Role = "system",
                .Content = $"上下文已压缩：较早的 {omitted} 条历史消息未放入本次请求。不要声称完整读取了被省略的历史，只基于当前可见上下文和工具结果回答。"
            })
        End If

        Dim systemText = Agent提示词_v6.构建系统提示词(
            AgentNetworkMode.Normalize(conversation.NetworkMode),
            GetPermissionLevelDisplayName(conversation.PermissionLevel))

        Dim result As New List(Of AgentMessageData) From {
            New AgentMessageData With {.Role = "system", .Content = systemText}
        }
        result.AddRange(conversationMessages)
        Return result
    End Function

    Private Function GetPermissionLevelDisplayName(permissionLevel As Integer) As String
        Select Case Math.Max(0, permissionLevel)
            Case 0
                Return "安全区域"
            Case 1
                Return "环境访问"
            Case Else
                Return "系统访问"
        End Select
    End Function

    Private Function EstimateMessageCharacters(message As AgentMessageData) As Integer
        If message Is Nothing Then Return 0
        Dim total = If(message.Content, "").Length + If(message.Name, "").Length + If(message.ToolCallId, "").Length
        If message.ToolCalls IsNot Nothing Then
            total += message.ToolCalls.Sum(Function(x) If(x.Name, "").Length + If(x.Arguments, "").Length)
        End If
        Return total
    End Function

    Private Sub AddUsage(conversation As AgentConversationData, usage As AgentUsageInfo)
        If usage Is Nothing Then Return
        _pageUsage.Add(usage)
        If conversation.Usage Is Nothing Then conversation.Usage = New AgentUsageInfo
        conversation.Usage.Add(usage)
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
