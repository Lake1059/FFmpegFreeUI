Imports System.Text
Imports System.IO
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
    Private ReadOnly _pendingSteeringMessages As New List(Of AgentMessageData)
    Private _activeResponseItem As LakeUI.AgentRoom.ChatItem = Nothing
    Private _activeRunConversation As AgentConversationData = Nothing
    Private _activeTurn As AgentTurnData = Nothing
    Private _activeTurnItem As LakeUI.AgentRoom.ChatItem = Nothing
    Private _activeToolRoundLogs As List(Of ToolRoundLog) = Nothing
    Private _activeResponseText As String = ""
    Private ReadOnly _activeResponseTextBuilder As New StringBuilder
    Private _activeResponseTextDirty As Boolean = False
    Private _activeRunStatusText As String = ""
    Private _activeRunElapsedTimer As System.Windows.Forms.Timer = Nothing
    Private _modelEndpointSignature As String = ""
    Private _refreshingModels As Boolean = False
    Private _pendingModelRefresh As Boolean = False
    Private _agentPageActivated As Boolean = False
    Private ReadOnly _pendingFiles As New List(Of String)
    Private _refreshingSubmittedFiles As Boolean = False

    Private Const ContextSummaryLimitTokens As Integer = 12000
    Private Const ContextAutoCompactRatio As Double = 0.9
    Private Const MaxContextCompactionPasses As Integer = 32
    Private Const ContextRequestEnvelopeReserveTokens As Integer = 512
    Private Const ContextMinimumCompletionReserveTokens As Integer = 2048
    Private Const StreamFlushIntervalMs As Integer = 80
    Private Const StreamFlushCharacters As Integer = 384

    Private Class ToolRunLog
        Public Property Round As Integer
        Public Property ToolName As String = ""
        Public Property CallId As String = ""
        Public Property ElapsedMilliseconds As Double = -1
        Public Property ResultText As String = ""
        Public Property Activity As AgentTurnActivityData
        Public Property Item As LakeUI.AgentRoom.ChatItem
    End Class

    Private Class ToolRoundLog
        Public Property Round As Integer
        Public Property Calls As New List(Of ToolRunLog)
    End Class

    Private Class ContextCompressionPlan
        Public Property ContextWindowTokens As Integer
        Public Property AutoCompactTokenLimit As Integer
        Public Property CompactModelId As String = ""
        Public Property CurrentRequestTokens As Integer
        Public Property RecentMessageBudgetTokens As Integer
        Public Property RecentMessageTokens As Integer
        Public Property ExistingBoundaryIndex As Integer = -1
        Public Property TargetBoundaryIndex As Integer = -1
        Public Property CompressionSourceBudgetTokens As Integer
        Public Property CompressionSourceTokens As Integer
        Public Property ToolDefinitionTokens As Integer
        Public Property CompletionReserveTokens As Integer
        Public Property SummaryBudgetTokens As Integer

        Public ReadOnly Property RequiresCompaction As Boolean
            Get
                Return TargetBoundaryIndex > ExistingBoundaryIndex
            End Get
        End Property
    End Class

    Private Class StreamingTextBuffer
        Private ReadOnly _room As LakeUI.AgentRoom
        Private ReadOnly _item As LakeUI.AgentRoom.ChatItem
        Private ReadOnly _onTextAppended As Action(Of String)
        Private ReadOnly _pendingText As New StringBuilder
        Private ReadOnly _syncRoot As New Object
        Private ReadOnly _uiContext As Threading.SynchronizationContext
        Private _lastFlushUtc As DateTime = DateTime.MinValue

        Public Sub New(room As LakeUI.AgentRoom, item As LakeUI.AgentRoom.ChatItem, Optional onTextAppended As Action(Of String) = Nothing)
            _room = room
            _item = item
            _onTextAppended = onTextAppended
            _uiContext = Threading.SynchronizationContext.Current
        End Sub

        Public Sub Append(delta As String)
            If String.IsNullOrEmpty(delta) Then Return
            Dim shouldFlush As Boolean
            SyncLock _syncRoot
                _pendingText.Append(delta)
                shouldFlush = _pendingText.Length >= StreamFlushCharacters OrElse
                    (DateTime.UtcNow - _lastFlushUtc).TotalMilliseconds >= StreamFlushIntervalMs
            End SyncLock
            If shouldFlush Then Flush(False)
        End Sub

        Public Sub Flush(forceScroll As Boolean)
            Dim appendedText As String
            SyncLock _syncRoot
                If _pendingText.Length = 0 Then Return
                appendedText = _pendingText.ToString()
                _pendingText.Clear()
                _lastFlushUtc = DateTime.UtcNow
            End SyncLock

            Dim apply =
                Sub()
                    If _item IsNot Nothing Then _room.AppendToItem(_item, appendedText)
                    _onTextAppended?.Invoke(appendedText)
                    If forceScroll Then _room.FollowLatestIfPinned()
                End Sub
            If _uiContext IsNot Nothing AndAlso Not Object.ReferenceEquals(Threading.SynchronizationContext.Current, _uiContext) Then
                _uiContext.Send(Sub(state) apply(), Nothing)
            Else
                apply()
            End If
        End Sub
    End Class

    Private Sub Form_v6_Agent_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _loading = True
        Try
            ModernListBox1.AllowDragReorder = True
            ModernListBox1.TabStop = True
            InitializeSubmittedFileList()
            ApplyAgentButtonPanelLayout()
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
    End Sub

    Private Sub InitializeSubmittedFileList()
        ModernListBox2.AllowDragReorder = True
        ModernListBox2.TabStop = True
        ModernListBox2.AllowDrop = True
        RefreshSubmittedFileList()
    End Sub

    Private Function CreateClient() As AgentEndpointClient
        Return 网络功能.创建Agent端点客户端()
    End Function

    Private Function GetSelectedNetworkMode() As Integer
        Return AgentNetworkMode.Normalize(Math.Max(0, MCB_联网设置.SelectedIndex))
    End Function

    Public Async Sub 请求刷新模型列表()
        If Not _agentPageActivated Then Return
        Await RefreshModelsIfEndpointChangedAsync(True)
    End Sub

    Public Async Sub 检查并刷新模型列表()
        _agentPageActivated = True
        Await RefreshModelsIfEndpointChangedAsync(False)
    End Sub

    Private Function ShouldRetryReasoningRefresh(errorMessage As String, Optional modelId As String = "") As Boolean
        Dim detail = If(String.IsNullOrWhiteSpace(errorMessage), "未知错误", errorMessage)
        Dim defaultEfforts = String.Join("、", AgentCapabilityCache.GetDefaultReasoningEfforts(modelId))
        Dim message = "拉取可用推理级别失败：" & detail & vbCrLf & vbCrLf &
            "选择 是 重新拉取；选择 否 使用默认级别：" & defaultEfforts & "。"
        Dim confirm = ExMsgBox(FormMain_v6, message, MsgBoxStyle.YesNo Or MsgBoxStyle.Question, "推理级别刷新失败")
        Return confirm = MsgBoxResult.Yes
    End Function

    Private Async Function UseDefaultReasoningEffortsAfterRefreshFailureAsync(client As AgentEndpointClient,
                                                                              selectedModel As String) As Task
        Dim oldLoading = _loading
        _loading = True
        Try
            Dim fallbackModels As List(Of AgentModelInfo)
            Dim currentSignature = AgentCapabilityCache.BuildEndpointSignature(client)
            Dim canReuseCurrentModels =
                _models IsNot Nothing AndAlso
                _models.Count > 0 AndAlso
                String.Equals(_modelEndpointSignature, currentSignature, StringComparison.Ordinal)

            If canReuseCurrentModels Then
                fallbackModels = _models
            Else
                fallbackModels = New List(Of AgentModelInfo)
                If Not String.IsNullOrWhiteSpace(selectedModel) Then
                    fallbackModels.Add(New AgentModelInfo With {.Id = selectedModel.Trim()})
                End If
            End If

            AgentCapabilityCache.UseDefaultReasoningEfforts(client, fallbackModels, selectedModel)

            If fallbackModels.Count > 0 Then
                _models = fallbackModels
                MCB_模型选择.Items.Clear()
                MCB_模型选择.Items.AddRange(_models.Select(Function(x) x.Id))

                Dim index = _models.FindIndex(Function(x) String.Equals(x.Id, selectedModel, StringComparison.OrdinalIgnoreCase))
                If index < 0 Then index = 0
                MCB_模型选择.SelectedIndex = index
                设置_v6.实例对象.AgentModelId = If(MCB_模型选择.SelectedItem, "")
                Await RefreshReasoningEffortsAsync()
            Else
                Dim efforts = AgentCapabilityCache.GetDefaultReasoningEfforts(selectedModel)
                MCB_推理级别.Items.Clear()
                MCB_推理级别.Items.AddRange(efforts)

                Dim selected = If(设置_v6.实例对象.Agent推理级别, "")
                Dim index = efforts.FindIndex(Function(x) String.Equals(x, selected, StringComparison.OrdinalIgnoreCase))
                If index < 0 AndAlso efforts.Count > 0 Then index = Math.Min(1, efforts.Count - 1)
                If index >= 0 Then MCB_推理级别.SelectedIndex = index
                设置_v6.实例对象.Agent推理级别 = If(MCB_推理级别.SelectedItem, "")
            End If

            _modelEndpointSignature = currentSignature
            ShowStatus("推理级别使用默认值：" & String.Join("、", AgentCapabilityCache.GetDefaultReasoningEfforts(selectedModel)))
        Finally
            _loading = oldLoading
        End Try
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

        Await RefreshModelsAsync(signature, force OrElse dailyReasoningRefreshDue)
    End Function

    Private Async Function RefreshModelsAsync(Optional expectedSignature As String = Nothing,
                                              Optional allowReasoningFallback As Boolean = False) As Task
        If _refreshingModels Then
            _pendingModelRefresh = True
            Return
        End If

        Try
            If expectedSignature Is Nothing Then expectedSignature = AgentCapabilityCache.BuildEndpointSignature(CreateClient())
        Catch ex As Exception
            ShowStatus("Agent 端点配置无效：" & ex.Message, True)
            Return
        End Try

        _refreshingModels = True
        Dim oldLoading = _loading
        Dim selectedModel = If(MCB_模型选择.SelectedItem, 设置_v6.实例对象.AgentModelId)
        Dim client As AgentEndpointClient = Nothing
        Dim useDefaultAfterCatch As Boolean = False
        _loading = True
        Try
            client = CreateClient()
            Dim actualSignature = AgentCapabilityCache.BuildEndpointSignature(client)
            expectedSignature = actualSignature
            If String.IsNullOrWhiteSpace(client.Endpoint) Then
                _modelEndpointSignature = actualSignature
                ShowStatus("请先在 Agent 设置中选择或填写端点。", True)
                ExFloatingTip(MCB_模型选择, "请先选择或填写 Agent 端点", 1800)
                Return
            End If

            Dim customModelResult = Agent自定义模型配置_v6.加载(client)
            If customModelResult.ErrorMessage <> "" Then
                ExFloatingTip(MCB_模型选择, $"{Agent自定义模型配置_v6.配置文件名} 无效：{customModelResult.ErrorMessage}", 3600)
            End If

            ShowStatus("正在连接端点并获取模型列表")
            MCB_模型选择.Items.Clear()
            MCB_模型选择.Text = ""
            MCB_模型选择.WaterText = "正在获取模型"
            Dim modelResult As AgentClientResult(Of List(Of AgentModelInfo))
            Dim endpointModelError As String = ""
            Do
                modelResult = Await client.TryGetModelsAsync()
                If modelResult.Success Then Exit Do

                If customModelResult.Models.Count > 0 Then
                    endpointModelError = modelResult.ErrorMessage
                    Exit Do
                End If

                ShowStatus("获取模型列表失败：" & modelResult.ErrorMessage, True)
                ExFloatingTip(MCB_模型选择, modelResult.ErrorMessage, 2600)
                If Not allowReasoningFallback Then Return
                If ShouldRetryReasoningRefresh(modelResult.ErrorMessage, selectedModel) Then
                    ShowStatus("正在重新连接端点并获取模型列表")
                    Continue Do
                End If

                Await UseDefaultReasoningEffortsAfterRefreshFailureAsync(client, selectedModel)
                Return
            Loop
            If Not String.Equals(expectedSignature, AgentCapabilityCache.BuildEndpointSignature(CreateClient()), StringComparison.Ordinal) Then
                _pendingModelRefresh = True
                Return
            End If

            Dim endpointModels = If(modelResult.Success, modelResult.Value, New List(Of AgentModelInfo))
            _models = Agent自定义模型配置_v6.合并模型(endpointModels, customModelResult.Models)
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
            If endpointModelError <> "" Then
                ShowStatus($"端点模型列表不可用，已加载 {_models.Count} 个自定义模型", True)
                ExFloatingTip(MCB_模型选择, endpointModelError, 2600)
            ElseIf customModelResult.ErrorMessage <> "" Then
                ShowStatus($"模型列表已刷新：{_models.Count} 个模型；{Agent自定义模型配置_v6.配置文件名} 无效", True)
            ElseIf customModelResult.Models.Count > 0 Then
                ShowStatus($"模型列表已刷新：{_models.Count} 个模型，已应用自定义模型配置")
            Else
                ShowStatus($"模型列表已刷新：{_models.Count} 个模型")
            End If
        Catch ex As Exception
            ShowStatus("系统故障：获取模型列表失败：" & ex.Message, True)
            ExFloatingTip(MCB_模型选择, ex.Message, 2600)
            If allowReasoningFallback Then
                If ShouldRetryReasoningRefresh(ex.Message, selectedModel) Then
                    _pendingModelRefresh = True
                Else
                    useDefaultAfterCatch = True
                End If
            End If
        Finally
            _loading = oldLoading
            _refreshingModels = False
            MCB_模型选择.WaterText = "模型选择"
        End Try

        If useDefaultAfterCatch Then
            Await UseDefaultReasoningEffortsAfterRefreshFailureAsync(client, selectedModel)
        End If

        If _pendingModelRefresh Then
            _pendingModelRefresh = False
            Await RefreshModelsIfEndpointChangedAsync(True)
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
        If title.Length > 28 Then title = String.Concat(title.AsSpan(0, 28), "...")
        Return $"{title}  {conversation.UpdatedAt:MM-dd HH:mm}"
    End Function

    Private Sub RenderCurrentConversation(Optional scrollToBottom As Boolean = True)
        AgentRoom1.Clear()
        _statusItem = Nothing
        _activeTurnItem = Nothing
        _activeResponseItem = Nothing
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
        UpdateSendButtonState()
        If scrollToBottom Then AgentRoom1.ScrollToBottom()
    End Sub

    Private Sub RenderTurnRecord(turn As AgentTurnData)
        If turn Is Nothing Then Return
        Dim isActive = ReferenceEquals(turn, _activeTurn)
        Dim expanded = isActive OrElse Not String.Equals(turn.State, "completed", StringComparison.OrdinalIgnoreCase)
        Dim header = AgentRoom1.AddTurnHeader(turn.Id, FormatTurnHeader(turn), expanded)
        If isActive Then _activeTurnItem = header

        For Each activity In If(turn.Activities, New List(Of AgentTurnActivityData))
            If activity Is Nothing Then Continue For
            Select Case If(activity.Kind, "").ToLowerInvariant()
                Case "assistant"
                    If Not String.IsNullOrWhiteSpace(activity.Content) Then
                        AgentRoom1.AddAssistantActivity(turn.Id, activity.Content, activity.Id)
                    End If
                Case "guidance"
                    If Not String.IsNullOrWhiteSpace(activity.Content) Then
                        AgentRoom1.AddAssistantActivity(turn.Id, "调整方向：" & activity.Content, activity.Id)
                    End If
                Case "tool"
                    Dim item = AgentRoom1.AddToolCall(
                        turn.Id,
                        FormatToolActivityTitle(activity),
                        FormatToolActivityDetails(activity),
                        activity.Id,
                        expanded:=False,
                        running:=String.Equals(activity.State, "running", StringComparison.OrdinalIgnoreCase),
                        isError:=String.Equals(activity.State, "error", StringComparison.OrdinalIgnoreCase))
                    Dim log = FindToolRunLog(activity.Id)
                    If log IsNot Nothing Then log.Item = item
            End Select
        Next
    End Sub

    Private Function FormatTurnHeader(turn As AgentTurnData) As String
        If turn Is Nothing Then Return "工作记录"
        Dim elapsed = If(turn.CompletedAt > turn.StartedAt,
                         turn.CompletedAt - turn.StartedAt,
                         If(ReferenceEquals(turn, _activeTurn), DateTime.Now - turn.StartedAt, TimeSpan.Zero))
        Dim toolCount = If(turn.Activities, New List(Of AgentTurnActivityData)).
            Where(Function(x) x IsNot Nothing AndAlso String.Equals(x.Kind, "tool", StringComparison.OrdinalIgnoreCase)).
            Count()
        Dim suffix = If(toolCount > 0, $" · {toolCount} 次工具调用", "")
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

    Private Function FormatToolActivityTitle(activity As AgentTurnActivityData) As String
        Dim name = CompactSingleLine(GetToolDisplayName(activity?.ToolName), 42)
        Select Case If(activity?.State, "").ToLowerInvariant()
            Case "running"
                Return name & " · 正在执行"
            Case "error"
                Return name & " · 失败"
            Case "canceled"
                Return name & " · 已取消"
            Case Else
                Return name & " · " & FormatElapsedMilliseconds(If(activity?.ElapsedMilliseconds, 0))
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
        Dim sb As New StringBuilder
        If Not String.IsNullOrWhiteSpace(activity.Arguments) Then
            sb.AppendLine("调用参数")
            sb.AppendLine(activity.Arguments.Trim())
        End If
        If Not String.IsNullOrWhiteSpace(activity.ResultText) Then
            If sb.Length > 0 Then sb.AppendLine()
            sb.AppendLine("返回结果")
            sb.Append(activity.ResultText.Trim())
        ElseIf String.Equals(activity.State, "running", StringComparison.OrdinalIgnoreCase) Then
            If sb.Length > 0 Then sb.AppendLine()
            sb.Append("正在等待工具返回结果...")
        End If
        Return sb.ToString()
    End Function

    Private Function FindToolRunLog(activityId As String) As ToolRunLog
        If String.IsNullOrWhiteSpace(activityId) OrElse _activeToolRoundLogs Is Nothing Then Return Nothing
        Return _activeToolRoundLogs.SelectMany(Function(x) If(x?.Calls, New List(Of ToolRunLog))).
            FirstOrDefault(Function(x) x?.Activity IsNot Nothing AndAlso String.Equals(x.Activity.Id, activityId, StringComparison.Ordinal))
    End Function

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
        If Not ReferenceEquals(conversation, _activeRunConversation) Then Return
        If Not IsConversationSelected(conversation) Then Return
        If _activeTurn Is Nothing Then Return
        If _activeTurnItem Is Nothing OrElse Not AgentRoom1.Items.Contains(_activeTurnItem) Then
            _activeTurnItem = AgentRoom1.FindItem(_activeTurn.Id)
        End If
        If _activeTurnItem Is Nothing Then Return
        _activeTurnItem.Title = FormatTurnHeader(_activeTurn)
        _activeTurnItem.IsRunning = String.Equals(_activeTurn.State, "running", StringComparison.OrdinalIgnoreCase)
        _activeTurnItem.IsError = String.Equals(_activeTurn.State, "error", StringComparison.OrdinalIgnoreCase)
        AgentRoom1.FollowLatestIfPinned()
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
        Dim currentUsage = If(_current?.Usage, New AgentUsageInfo)
        MB_页面用量.Text = $"{FormatContextPercent(currentUsage)} | {currentUsage.TotalTokens} / {_pageUsage.TotalTokens}"
    End Sub

    Private Sub UpdateSendButtonState()
        If _busy AndAlso IsConversationSelected(_activeRunConversation) Then
            If _requestCts IsNot Nothing AndAlso _requestCts.IsCancellationRequested Then
                MB_发送.Text = "停止中"
            ElseIf Not String.IsNullOrWhiteSpace(ModernTextBox1.Text) Then
                MB_发送.Text = "调整方向"
            Else
                MB_发送.Text = "停止"
            End If
        Else
            MB_发送.Text = "发送"
        End If
        MB_发送.Enabled = True
    End Sub

    Private Sub ModernTextBox1_TextChanged(sender As Object, e As EventArgs) Handles ModernTextBox1.TextChanged
        If _busy Then UpdateSendButtonState()
    End Sub

    Private Sub ShowStatus(text As String, Optional keepRecord As Boolean = False)
        Dim content = $"状态 {DateTime.Now:HH:mm:ss}{vbCrLf}{If(text, "").Trim()}"
        If keepRecord OrElse _statusItem Is Nothing OrElse Not AgentRoom1.Items.Contains(_statusItem) Then
            _statusItem = AddCardBeforeActiveResponse(content)
        Else
            _statusItem.Text = content
        End If
        AgentRoom1.FollowLatestIfPinned()
    End Sub

    Private Sub ShowRunStatus(conversation As AgentConversationData, text As String, Optional keepRecord As Boolean = False)
        Dim content = If(text, "").Trim()
        If ReferenceEquals(conversation, _activeRunConversation) Then
            _activeRunStatusText = content
            If _activeTurn IsNot Nothing Then _activeTurn.StatusText = content
            UpdateActiveRunOverviewCard(conversation)
            Return
        End If

        If Not IsConversationSelected(conversation) Then Return
        AgentRoom1.AddCard(content)
        AgentRoom1.FollowLatestIfPinned()
    End Sub

    Private Function AddCardBeforeActiveResponse(content As String) As LakeUI.AgentRoom.ChatItem
        Dim item As New LakeUI.AgentRoom.ChatItem(LakeUI.AgentRoom.ChatItemKind.Card, content)
        Dim anchor = _activeResponseItem
        Dim insertIndex = If(anchor Is Nothing, -1, AgentRoom1.Items.IndexOf(anchor))
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
        If ReferenceEquals(conversation, _activeRunConversation) Then ReplaceActiveResponseText(If(text, ""))
        If Not IsConversationSelected(conversation) Then Return

        If IsRunResponsePlaceholder(_activeResponseText) Then
            If _activeResponseItem IsNot Nothing AndAlso AgentRoom1.Items.Contains(_activeResponseItem) Then
                AgentRoom1.Items.Remove(_activeResponseItem)
            End If
            _activeResponseItem = Nothing
            AgentRoom1.FollowLatestIfPinned()
            Return
        End If

        If _activeResponseItem Is Nothing OrElse Not AgentRoom1.Items.Contains(_activeResponseItem) Then
            _activeResponseItem = AgentRoom1.AddAssistantMessage(_activeResponseText)
        Else
            _activeResponseItem.Text = _activeResponseText
        End If
        AgentRoom1.FollowLatestIfPinned()
    End Sub

    Private Sub AppendRunResponseText(conversation As AgentConversationData,
                                      delta As String,
                                      prependParagraphBreak As Boolean)
        If String.IsNullOrEmpty(delta) OrElse Not ReferenceEquals(conversation, _activeRunConversation) Then Return

        If Not _activeResponseTextDirty AndAlso IsRunResponsePlaceholder(_activeResponseText) Then
            _activeResponseTextBuilder.Clear()
            _activeResponseText = ""
            _activeResponseTextDirty = False
        End If
        Dim appendedText = delta
        If prependParagraphBreak AndAlso _activeResponseTextBuilder.Length > 0 Then
            appendedText = vbCrLf & vbCrLf & appendedText
        End If
        _activeResponseTextBuilder.Append(appendedText)
        _activeResponseTextDirty = True
        If Not IsConversationSelected(conversation) Then Return

        If _activeResponseItem Is Nothing OrElse Not AgentRoom1.Items.Contains(_activeResponseItem) Then
            _activeResponseItem = AgentRoom1.AddAssistantMessage(GetActiveResponseTextSnapshot())
        Else
            AgentRoom1.AppendToItem(_activeResponseItem, appendedText)
        End If
        AgentRoom1.FollowLatestIfPinned()
    End Sub

    Private Sub CompleteRunResponseText(conversation As AgentConversationData, text As String)
        Dim finalText = If(text, "").Trim()
        Dim currentText = GetActiveResponseTextSnapshot().Trim()
        If ReferenceEquals(conversation, _activeRunConversation) AndAlso
           _activeResponseItem IsNot Nothing AndAlso
           AgentRoom1.Items.Contains(_activeResponseItem) AndAlso
           String.Equals(currentText, finalText, StringComparison.Ordinal) Then
            ReplaceActiveResponseText(finalText)
            AgentRoom1.CompleteStreamingMessage(_activeResponseItem)
            Return
        End If
        SetRunResponseText(conversation, finalText)
    End Sub

    Private Sub PromoteActiveResponseToActivity(conversation As AgentConversationData, content As String)
        If _activeTurn Is Nothing OrElse Not ReferenceEquals(conversation, _activeRunConversation) Then Return
        Dim activityText = If(content, "").Trim()
        If activityText = "" Then activityText = GetActiveResponseTextSnapshot().Trim()

        If activityText <> "" AndAlso Not IsRunResponsePlaceholder(activityText) Then
            Dim activity As New AgentTurnActivityData With {
                .Kind = "assistant",
                .Content = activityText,
                .State = "completed"
            }
            _activeTurn.Activities.Add(activity)

            If IsConversationSelected(conversation) Then
                If _activeResponseItem IsNot Nothing AndAlso AgentRoom1.Items.Contains(_activeResponseItem) Then
                    AgentRoom1.CompleteStreamingMessage(_activeResponseItem)
                    _activeResponseItem.Kind = LakeUI.AgentRoom.ChatItemKind.AssistantActivity
                    _activeResponseItem.Key = activity.Id
                    _activeResponseItem.ParentTurnId = _activeTurn.Id
                Else
                    AgentRoom1.AddAssistantActivity(_activeTurn.Id, activityText, activity.Id)
                End If
            End If
        ElseIf _activeResponseItem IsNot Nothing AndAlso AgentRoom1.Items.Contains(_activeResponseItem) Then
            AgentRoom1.Items.Remove(_activeResponseItem)
        End If

        _activeResponseItem = Nothing
        ReplaceActiveResponseText("正在思考...")
        AgentRoom1.FollowLatestIfPinned()
    End Sub

    Private Function BeginToolActivity(conversation As AgentConversationData,
                                       callInfo As AgentToolCallInfo,
                                       round As Integer) As ToolRunLog
        Dim activity As New AgentTurnActivityData With {
            .Kind = "tool",
            .ToolName = If(callInfo?.Name, ""),
            .Arguments = If(callInfo?.Arguments, ""),
            .State = "running"
        }
        _activeTurn?.Activities.Add(activity)

        Dim log As New ToolRunLog With {
            .Round = round,
            .ToolName = activity.ToolName,
            .CallId = If(callInfo?.Id, ""),
            .Activity = activity
        }
        If IsConversationSelected(conversation) AndAlso _activeTurn IsNot Nothing Then
            log.Item = AgentRoom1.AddToolCall(
                _activeTurn.Id,
                FormatToolActivityTitle(activity),
                FormatToolActivityDetails(activity),
                activity.Id,
                expanded:=False,
                running:=True)
        End If
        UpdateActiveRunOverviewCard(conversation)
        AgentRoom1.FollowLatestIfPinned()
        Return log
    End Function

    Private Sub CompleteToolActivity(conversation As AgentConversationData,
                                     log As ToolRunLog,
                                     resultText As String,
                                     elapsedMilliseconds As Double,
                                     isError As Boolean)
        If log Is Nothing OrElse log.Activity Is Nothing Then Return
        log.ElapsedMilliseconds = elapsedMilliseconds
        log.ResultText = If(resultText, "")
        log.Activity.ElapsedMilliseconds = elapsedMilliseconds
        log.Activity.ResultText = log.ResultText
        log.Activity.State = If(isError, "error", "completed")

        If IsConversationSelected(conversation) Then
            If log.Item Is Nothing OrElse Not AgentRoom1.Items.Contains(log.Item) Then
                log.Item = AgentRoom1.FindItem(log.Activity.Id)
            End If
            If log.Item IsNot Nothing Then
                log.Item.Title = FormatToolActivityTitle(log.Activity)
                log.Item.Text = FormatToolActivityDetails(log.Activity)
                log.Item.IsRunning = False
                log.Item.IsError = isError
            End If
        End If
        UpdateActiveRunOverviewCard(conversation)
        AgentRoom1.FollowLatestIfPinned()
    End Sub

    Private Sub ReplaceActiveResponseText(text As String)
        _activeResponseText = If(text, "")
        _activeResponseTextBuilder.Clear()
        _activeResponseTextBuilder.Append(_activeResponseText)
        _activeResponseTextDirty = False
    End Sub

    Private Function GetActiveResponseTextSnapshot() As String
        If _activeResponseTextDirty Then
            _activeResponseText = _activeResponseTextBuilder.ToString()
            _activeResponseTextDirty = False
        End If
        Return If(_activeResponseText, "")
    End Function

    Private Sub RenderActiveRunOverlay()
        _activeResponseItem = Nothing
        If _activeRunConversation Is Nothing OrElse Not IsConversationSelected(_activeRunConversation) Then Return

        If _activeTurn IsNot Nothing Then
            _activeTurnItem = AgentRoom1.FindItem(_activeTurn.Id)
            If _activeTurnItem IsNot Nothing Then _activeTurnItem.IsExpanded = True
        End If
        Dim responseText = GetActiveResponseTextSnapshot()
        If Not IsRunResponsePlaceholder(responseText) Then
            _activeResponseItem = AgentRoom1.AddAssistantMessage(responseText)
        End If
    End Sub

    Private Sub ClearActiveRunState()
        StopActiveRunElapsedTimer()
        _activeResponseItem = Nothing
        _activeRunConversation = Nothing
        _activeTurn = Nothing
        _activeTurnItem = Nothing
        _activeToolRoundLogs = Nothing
        ReplaceActiveResponseText("")
        _activeRunStatusText = ""
    End Sub

    Private Sub StartActiveRunElapsedTimer()
        StopActiveRunElapsedTimer()
        _activeRunElapsedTimer = New System.Windows.Forms.Timer With {.Interval = 1000}
        AddHandler _activeRunElapsedTimer.Tick, AddressOf ActiveRunElapsedTimer_Tick
        _activeRunElapsedTimer.Start()
    End Sub

    Private Sub StopActiveRunElapsedTimer()
        If _activeRunElapsedTimer Is Nothing Then Return
        RemoveHandler _activeRunElapsedTimer.Tick, AddressOf ActiveRunElapsedTimer_Tick
        _activeRunElapsedTimer.Stop()
        _activeRunElapsedTimer.Dispose()
        _activeRunElapsedTimer = Nothing
    End Sub

    Private Sub ActiveRunElapsedTimer_Tick(sender As Object, e As EventArgs)
        UpdateActiveRunOverviewCard(_activeRunConversation)
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
                If String.IsNullOrWhiteSpace(ModernTextBox1.Text) Then
                    RequestStopAgentTask()
                Else
                    Await OfferGuidanceMessageAsync()
                End If
            Else
                ExFloatingTip(MB_发送, "当前已有其他对话正在响应，请先切回该对话或停止任务", 2200)
            End If
            Return
        End If
        Await StartUserMessageAsync()
    End Sub

    Private Async Sub ModernTextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles ModernTextBox1.KeyDown
        If e.KeyCode = Keys.Delete AndAlso e.Control Then
            e.SuppressKeyPress = True
            e.Handled = True
            RemoveLatestUserTurnAndCopyToClipboard()
        ElseIf e.KeyCode = Keys.Enter AndAlso e.Alt Then
            e.SuppressKeyPress = True
            e.Handled = True
            If _busy Then
                ExFloatingTip(ModernTextBox1, "当前任务正在响应，请先停止后再撤回", 2200)
            Else
                Await ReplaceLatestUserTurnAsync()
            End If
        ElseIf e.KeyCode = Keys.Enter AndAlso Not e.Shift Then
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
        If text = "" Then
            Await RetryLatestUserTurnAsync()
            Return
        End If
        If Not EnsureModelSelected() Then Return

        CommitUserMessage(text)
        Await StartAgentRunAsync(_current)
    End Function

    Private Async Function RetryLatestUserTurnAsync() As Task
        If _busy OrElse _current Is Nothing Then Return
        Dim userIndex = GetLatestUserMessageIndex()
        If userIndex < 0 Then
            ExFloatingTip(ModernTextBox1, "没有可重试的用户消息", 1800)
            Return
        End If

        Dim confirm = ExMsgBox(FormMain_v6,
            "是否移除最新一轮 AI 推理内容，并重新发送此前最后一次用户消息？",
            MsgBoxStyle.YesNo Or MsgBoxStyle.Question,
            "重试上一轮")
        If confirm <> MsgBoxResult.Yes Then Return

        RewindConversationFrom(userIndex + 1)
        Await StartAgentRunAsync(_current)
    End Function

    Private Async Function ReplaceLatestUserTurnAsync() As Task
        If _busy Then Return
        Dim text = ModernTextBox1.Text.Trim()
        If text = "" Then
            ExFloatingTip(ModernTextBox1, "请输入要重新发送的内容", 1800)
            Return
        End If
        If Not EnsureModelSelected() Then Return

        If _current IsNot Nothing Then
            Dim userIndex = GetLatestUserMessageIndex()
            If userIndex >= 0 Then
                Dim confirm = ExMsgBox(FormMain_v6,
                    "是否移除最新一轮 AI 推理和用户内容，并以当前文本重新开始？",
                    MsgBoxStyle.YesNo Or MsgBoxStyle.Question,
                    "撤回上一轮")
                If confirm <> MsgBoxResult.Yes Then Return
                RemoveConversationMessagesFrom(userIndex)
            End If
        End If

        CommitUserMessage(text, True)
        Await StartAgentRunAsync(_current)
    End Function

    Private Function GetLatestUserMessageIndex() As Integer
        If _current?.Messages Is Nothing Then Return -1
        For i = _current.Messages.Count - 1 To 0 Step -1
            If String.Equals(_current.Messages(i)?.Role, "user", StringComparison.OrdinalIgnoreCase) AndAlso
               Not String.Equals(_current.Messages(i)?.Name, AgentConversationSchema.SteeringMessageName, StringComparison.OrdinalIgnoreCase) Then Return i
        Next
        Return -1
    End Function

    Private Sub RemoveLatestUserTurnAndCopyToClipboard()
        If _busy Then
            ExFloatingTip(ModernTextBox1, "当前任务正在响应，请先停止后再删除", 2200)
            Return
        End If

        Dim userIndex = GetLatestUserMessageIndex()
        If userIndex < 0 Then
            ExFloatingTip(ModernTextBox1, "没有可删除的用户消息", 1800)
            Return
        End If

        Dim userContent = If(_current.Messages(userIndex)?.Content, "")
        Dim copied = True
        Try
            Clipboard.SetText(userContent)
        Catch ex As Exception
            copied = False
            ExFloatingTip(ModernTextBox1, "用户内容删除成功，但复制到剪贴板失败：" & ex.Message, 2600)
        End Try

        RewindConversationFrom(userIndex)
        If copied Then ExFloatingTip(ModernTextBox1, "已删除上一轮推理和用户内容，用户内容已复制到剪贴板", 2200)
    End Sub

    Private Sub RewindConversationFrom(index As Integer)
        RemoveConversationMessagesFrom(index)
        SaveCurrent()
        RenderCurrentConversation()
    End Sub

    Private Sub RemoveConversationMessagesFrom(index As Integer)
        If _current?.Messages Is Nothing Then Return
        Dim startIndex = Math.Max(0, Math.Min(index, _current.Messages.Count))
        Dim affectedUserIds As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
        For Each message In _current.Messages.Skip(startIndex)
            If String.Equals(message?.Role, "user", StringComparison.OrdinalIgnoreCase) Then affectedUserIds.Add(message.Id)
        Next
        For i = Math.Min(startIndex - 1, _current.Messages.Count - 1) To 0 Step -1
            Dim message = _current.Messages(i)
            If Not String.Equals(message?.Role, "user", StringComparison.OrdinalIgnoreCase) Then Continue For
            affectedUserIds.Add(message.Id)
            Exit For
        Next
        If startIndex < _current.Messages.Count Then _current.Messages.RemoveRange(startIndex, _current.Messages.Count - startIndex)
        If _current.Turns IsNot Nothing AndAlso affectedUserIds.Count > 0 Then
            _current.Turns.RemoveAll(Function(x) x IsNot Nothing AndAlso affectedUserIds.Contains(x.UserMessageId))
        End If

        If ResolveContextCheckpointBoundaryIndex(_current, GetRequestConversationMessages(_current)) < 0 Then
            _current.ContextCheckpoint = Nothing
        End If
    End Sub

    Private Async Function OfferGuidanceMessageAsync() As Task
        Dim text = ModernTextBox1.Text.Trim()
        If text = "" Then Return
        If _busy AndAlso Not IsConversationSelected(_activeRunConversation) Then
            ExFloatingTip(ModernTextBox1, "当前已有其他对话正在响应，请先切回该对话或停止任务", 2200)
            Return
        End If

        Dim content = text
        Dim fileContext = BuildSubmittedFilesContext()
        If Not String.IsNullOrWhiteSpace(fileContext) Then content &= vbCrLf & vbCrLf & fileContext.Trim()
        Dim steeringMessage As New AgentMessageData With {
            .Role = "user",
            .Name = AgentConversationSchema.SteeringMessageName,
            .Content = "调整方向：" & vbCrLf & content
        }
        _activeRunConversation.Messages.Add(steeringMessage)
        _pendingSteeringMessages.Add(steeringMessage)

        If _activeTurn IsNot Nothing Then
            Dim activity As New AgentTurnActivityData With {
                .Kind = "guidance",
                .Title = "调整方向",
                .Content = content,
                .State = "completed"
            }
            _activeTurn.Activities.Add(activity)
            If IsConversationSelected(_activeRunConversation) Then AddSteeringActivityToRoom(activity)
        End If

        ModernTextBox1.Text = ""
        ClearSubmittedFiles()
        SaveConversation(_activeRunConversation)
        ShowRunStatus(_activeRunConversation, "已收到调整方向，将在当前步骤结束后继续")
        Await Task.CompletedTask
    End Function

    Private Sub AddSteeringActivityToRoom(activity As AgentTurnActivityData)
        If activity Is Nothing OrElse _activeTurn Is Nothing Then Return
        Dim item = AgentRoom1.AddAssistantActivity(_activeTurn.Id, "调整方向：" & activity.Content, activity.Id)
        Dim responseIndex = If(_activeResponseItem Is Nothing, -1, AgentRoom1.Items.IndexOf(_activeResponseItem))
        If responseIndex >= 0 AndAlso AgentRoom1.Items.IndexOf(item) > responseIndex Then
            AgentRoom1.Items.Remove(item)
            AgentRoom1.Items.Insert(responseIndex, item)
        End If
        AgentRoom1.FollowLatestIfPinned()
    End Sub

    Private Function EnsureModelSelected() As Boolean
        If MCB_模型选择.SelectedIndex >= 0 Then Return True
        ShowStatus("请先选择模型。", True)
        ExFloatingTip(MCB_模型选择, "请先选择模型", 1600)
        Return False
    End Function

    Private Sub AppendUserMessage(text As String, Optional fileContext As String = "")
        Dim content = text
        If Not String.IsNullOrWhiteSpace(fileContext) Then
            content &= vbCrLf & vbCrLf & fileContext.Trim()
        End If
        Dim userMessage As New AgentMessageData With {.Role = "user", .Content = content}
        _current.Messages.Add(userMessage)
        If _current.Turns Is Nothing Then _current.Turns = New List(Of AgentTurnData)
        _current.Turns.Add(New AgentTurnData With {
            .UserMessageId = userMessage.Id,
            .State = "pending",
            .StatusText = "等待开始"
        })
        If _current.Title = "新对话" OrElse String.IsNullOrWhiteSpace(_current.Title) Then _current.Title = BuildTitle(text)
        AgentRoom1.AddUserMessage(content)
    End Sub

    Private Sub CommitUserMessage(text As String, Optional rerenderConversation As Boolean = False)
        If _current Is Nothing Then _current = CreateConversationFromCurrentSettings()
        ApplyConversationSnapshot()
        AppendUserMessage(text, BuildSubmittedFilesContext())
        ModernTextBox1.Text = ""
        ClearSubmittedFiles()
        SaveCurrent()
        If rerenderConversation Then RenderCurrentConversation()
    End Sub

    Private Sub RequestStopAgentTask()
        If Not _busy Then Return
        If _requestCts IsNot Nothing AndAlso Not _requestCts.IsCancellationRequested Then
            _requestCts.Cancel()
            MB_发送.Text = "停止中"
            ShowRunStatus(_activeRunConversation, "正在停止当前任务")
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
        _pendingSteeringMessages.Clear()
        _activeTurn = EnsureLatestTurn(runConversation)
        If _activeTurn IsNot Nothing Then
            _activeTurn.StartedAt = DateTime.Now
            _activeTurn.CompletedAt = DateTime.MinValue
            _activeTurn.State = "running"
            _activeTurn.StatusText = "正在准备上下文"
        End If
        ReplaceActiveResponseText("正在思考...")
        _activeRunStatusText = ""
        _activeResponseItem = Nothing
        _activeTurnItem = Nothing
        UpdateSendButtonState()
        Dim toolRoundLogs As New List(Of ToolRoundLog)
        _activeToolRoundLogs = toolRoundLogs
        If IsConversationSelected(runConversation) Then
            If _activeTurn IsNot Nothing Then
                _activeTurnItem = AgentRoom1.FindItem(_activeTurn.Id)
                If _activeTurnItem Is Nothing Then
                    _activeTurnItem = AgentRoom1.AddTurnHeader(_activeTurn.Id, FormatTurnHeader(_activeTurn), expanded:=True)
                Else
                    _activeTurnItem.IsExpanded = True
                End If
            End If
            UpdateActiveRunOverviewCard(runConversation)
        End If
        StartActiveRunElapsedTimer()
        Try
            ShowRunStatus(runConversation, "正在准备上下文")
            SaveConversation(runConversation)

            Await RunAgentLoopAsync(runConversation, toolRoundLogs, localCts.Token)
        Catch ex As OperationCanceledException
            CompleteInterruptedToolBatch(runConversation, toolRoundLogs, "工具调用已取消，未返回结果。", "canceled")
            Dim canceledText = BuildCanceledResponseText(GetActiveResponseTextSnapshot())
            SetRunResponseText(runConversation, canceledText)
            AppendRunConversationMessage(runConversation, New AgentMessageData With {.Role = "assistant", .Content = canceledText})
            ShowRunStatus(runConversation, "已停止当前任务")
            FinishActiveTurn("canceled", "已停止")
        Catch ex As Exception
            CompleteInterruptedToolBatch(runConversation, toolRoundLogs, "工具调用因运行异常中止：" & ex.Message, "error")
            ShowRunStatus(runConversation, "系统故障：请求失败：" & ex.Message, True)
            SetRunResponseText(runConversation, "请求失败：" & ex.Message)
            AppendRunConversationMessage(runConversation, New AgentMessageData With {.Role = "assistant", .Content = GetActiveResponseTextSnapshot()})
            FinishActiveTurn("error", ex.Message)
            ExFloatingTip(MB_发送, ex.Message, 2600)
        Finally
            _requestCts = Nothing
            localCts.Dispose()
            _busy = False
            MB_发送.Enabled = True
            SaveConversation(runConversation)
            ClearActiveRunState()
            _pendingSteeringMessages.Clear()
            UpdateSendButtonState()
        End Try
    End Function

    Private Function BuildCanceledResponseText(currentText As String) As String
        Dim text = If(currentText, "").Trim()
        If text = "" OrElse
            text = "正在思考..." OrElse
            text = "正在重新连接..." OrElse
            text.StartsWith("正在调用工具：", StringComparison.Ordinal) Then
            Return "已停止。"
        End If

        Return text & vbCrLf & vbCrLf & "（已停止）"
    End Function

    Private Sub ApplyConversationSnapshot()
        _current.ModelId = If(MCB_模型选择.SelectedItem, "")
        _current.ReasoningEffort = If(MCB_推理级别.SelectedItem, "")
        _current.NetworkMode = GetSelectedNetworkMode()
        _current.PermissionLevel = Math.Max(0, MCB_权限控制.SelectedIndex)
    End Sub

    Private Function BuildTitle(text As String) As String
        text = text.Replace(vbCr, " ").Replace(vbLf, " ").Trim()
        If text.Length > 18 Then text = String.Concat(text.AsSpan(0, 18), "...")
        Return If(text = "", "新对话", text)
    End Function

    Private Function EnsureLatestTurn(conversation As AgentConversationData) As AgentTurnData
        If conversation Is Nothing Then Return Nothing
        If conversation.Turns Is Nothing Then conversation.Turns = New List(Of AgentTurnData)
        Dim userMessage = conversation.Messages.LastOrDefault(
            Function(x) x IsNot Nothing AndAlso
                String.Equals(x.Role, "user", StringComparison.OrdinalIgnoreCase) AndAlso
                Not String.Equals(x.Name, AgentConversationSchema.SteeringMessageName, StringComparison.OrdinalIgnoreCase))
        If userMessage Is Nothing Then Return Nothing
        Dim turn = conversation.Turns.LastOrDefault(
            Function(x) x IsNot Nothing AndAlso String.Equals(x.UserMessageId, userMessage.Id, StringComparison.OrdinalIgnoreCase))
        If turn Is Nothing Then
            turn = New AgentTurnData With {.UserMessageId = userMessage.Id}
            conversation.Turns.Add(turn)
        End If
        If turn.Activities Is Nothing Then turn.Activities = New List(Of AgentTurnActivityData)
        Return turn
    End Function

    Private Sub FinishActiveTurn(state As String, Optional statusText As String = "")
        If _activeTurn Is Nothing Then Return
        For Each activity In If(_activeTurn.Activities, New List(Of AgentTurnActivityData))
            If activity Is Nothing OrElse Not String.Equals(activity.State, "running", StringComparison.OrdinalIgnoreCase) Then Continue For
            activity.State = If(String.Equals(state, "canceled", StringComparison.OrdinalIgnoreCase), "canceled", "error")
            If String.IsNullOrWhiteSpace(activity.ResultText) Then activity.ResultText = If(statusText, "运行已结束")
        Next
        _activeTurn.State = If(state, "completed")
        _activeTurn.CompletedAt = DateTime.Now
        If Not String.IsNullOrWhiteSpace(statusText) Then _activeTurn.StatusText = statusText.Trim()
        If _activeTurnItem IsNot Nothing AndAlso AgentRoom1.Items.Contains(_activeTurnItem) Then
            _activeTurnItem.Title = FormatTurnHeader(_activeTurn)
            _activeTurnItem.IsRunning = False
            _activeTurnItem.IsError = String.Equals(_activeTurn.State, "error", StringComparison.OrdinalIgnoreCase)
            _activeTurnItem.IsExpanded = False
        End If
        AgentRoom1.FollowLatestIfPinned()
    End Sub

    Private Async Function RunAgentLoopAsync(conversation As AgentConversationData,
                                             toolRoundLogs As List(Of ToolRoundLog),
                                             cancellationToken As Threading.CancellationToken) As Task
        Dim client = CreateClient()
        Dim modelId = If(conversation.ModelId, "")
        Dim reasoning = If(conversation.ReasoningEffort, "")
        Dim networkMode = AgentNetworkMode.Normalize(conversation.NetworkMode)
        Dim permissionLevel = Math.Max(0, conversation.PermissionLevel)
        Dim tools = AgentLocalTools.BuildToolDefinitions(permissionLevel, networkMode)
        Dim messages = Await BuildRequestMessagesAsync(client, conversation, modelId, tools, cancellationToken)
        Dim round As Integer = 0

        Using powerShellSession As New AgentLocalTools.PowerShellRunSession()
            Do
                cancellationToken.ThrowIfCancellationRequested()
                round += 1
                Dim result As AgentChatResult = Nothing
                If _activeResponseItem Is Nothing Then ReplaceActiveResponseText("正在思考...")
                Dim streamBuffer As New StreamingTextBuffer(
                    AgentRoom1,
                    Nothing,
                    Sub(value) AppendRunResponseText(conversation, value, False))

                ShowRunStatus(conversation, $"正在思考：第 {round} 轮")
                result = Await client.TryCreateChatCompletionStreamingAsync(
                modelId,
                messages,
                tools,
                reasoning,
                Sub(delta)
                    streamBuffer.Append(delta)
                End Sub,
                cancellationToken)
                streamBuffer.Flush(True)
                cancellationToken.ThrowIfCancellationRequested()

                If Not result.Success Then
                    ShowRunStatus(conversation, "重新连接：流式响应不可用，切换非流式。" & result.ErrorMessage)
                    SetRunResponseText(conversation, "正在重新连接...")
                    result = Await client.TryCreateChatCompletionAsync(modelId, messages, tools, reasoning, cancellationToken)
                    If Not result.Success Then
                        Dim errorText = "系统故障：请求失败：" & result.ErrorMessage
                        SetRunResponseText(conversation, errorText)
                        AppendRunConversationMessage(conversation, New AgentMessageData With {.Role = "assistant", .Content = errorText})
                        ShowRunStatus(conversation, errorText, True)
                        FinishActiveTurn("error", result.ErrorMessage)
                        ExFloatingTip(MB_发送, result.ErrorMessage, 2600)
                        Exit Function
                    End If
                End If
                AddUsage(conversation, result.Usage, modelId)

                If result.ToolCalls Is Nothing OrElse result.ToolCalls.Count = 0 Then
                    Dim content = If(result.Content, "").Trim()
                    If content = "" Then
                        Dim streamedContent = GetActiveResponseTextSnapshot().Trim()
                        If Not IsRunResponsePlaceholder(streamedContent) Then content = streamedContent
                    End If
                    If content = "" Then content = "模型没有返回内容。"
                    If _pendingSteeringMessages.Count > 0 Then
                        PromoteActiveResponseToActivity(conversation, content)
                        Dim intermediateMessage As New AgentMessageData With {
                            .Role = "assistant",
                            .Name = AgentConversationSchema.ActivityMessageName,
                            .Content = content
                        }
                        messages.Add(intermediateMessage)
                        AppendRunConversationMessage(conversation, intermediateMessage)
                        ConsumePendingSteeringMessages(messages, conversation)
                        Continue Do
                    End If
                    CompleteRunResponseText(conversation, content)
                    AppendRunConversationMessage(conversation, New AgentMessageData With {.Role = "assistant", .Content = content})
                    ShowRunStatus(conversation, "响应完成")
                    FinishActiveTurn("completed", "响应完成")
                    Exit Function
                End If

                PromoteActiveResponseToActivity(conversation, result.Content)
                ShowRunStatus(conversation, "正在调用工具：" & String.Join("、", result.ToolCalls.Select(Function(x) x.Name)))
                Dim assistantToolMessage As New AgentMessageData With {
                .Role = "assistant",
                .Name = AgentConversationSchema.ActivityMessageName,
                .Content = If(result.Content, ""),
                .ToolCalls = result.ToolCalls
            }
                messages.Add(assistantToolMessage)
                AppendRunConversationMessage(conversation, assistantToolMessage)

                Dim currentRoundLog As New ToolRoundLog With {.Round = round}
                toolRoundLogs.Add(currentRoundLog)
                For Each callInfo In result.ToolCalls
                    cancellationToken.ThrowIfCancellationRequested()
                    Dim callLog = BeginToolActivity(conversation, callInfo, round)
                    currentRoundLog.Calls.Add(callLog)

                    Dim started = DateTime.Now
                    ShowRunStatus(conversation, "正在调用工具：" & callInfo.Name)
                    Dim toolResult = Await AgentLocalTools.ExecuteAsync(
                        callInfo,
                        permissionLevel,
                        networkMode,
                        client,
                        modelId,
                        reasoning,
                        powerShellSession,
                        cancellationToken)
                    cancellationToken.ThrowIfCancellationRequested()
                    Dim elapsed = DateTime.Now - started
                    toolResult = Agent通用工具_v6.LimitText(toolResult, 16000)
                    CompleteToolActivity(conversation, callLog, toolResult, elapsed.TotalMilliseconds, False)

                    Dim toolMessage As New AgentMessageData With {
                    .Role = "tool",
                    .Name = callInfo.Name,
                    .ToolCallId = callInfo.Id,
                    .Content = toolResult
                    }
                    messages.Add(toolMessage)
                    AppendRunConversationMessage(conversation, toolMessage)
                    ShowRunStatus(conversation, $"工具完成：{callInfo.Name}，耗时 {FormatElapsedMilliseconds(elapsed.TotalMilliseconds)}")
                Next
                ConsumePendingSteeringMessages(messages, conversation)
            Loop
        End Using
    End Function

    Private Function ConsumePendingSteeringMessages(messages As List(Of AgentMessageData),
                                                     conversation As AgentConversationData) As Boolean
        If messages Is Nothing OrElse _pendingSteeringMessages.Count = 0 Then Return False
        For Each steeringMessage In _pendingSteeringMessages
            If steeringMessage IsNot Nothing Then messages.Add(steeringMessage)
        Next
        Dim count = _pendingSteeringMessages.Count
        _pendingSteeringMessages.Clear()
        ReplaceActiveResponseText("正在根据调整继续...")
        ShowRunStatus(conversation, $"正在应用 {count} 条调整方向")
        Return True
    End Function

    Private Sub AppendRunConversationMessage(conversation As AgentConversationData, message As AgentMessageData)
        If conversation?.Messages Is Nothing OrElse message Is Nothing Then Return
        Dim insertIndex = conversation.Messages.Count
        For Each pending In _pendingSteeringMessages
            If pending Is Nothing Then Continue For
            Dim candidate = conversation.Messages.IndexOf(pending)
            If candidate >= 0 Then insertIndex = Math.Min(insertIndex, candidate)
        Next
        conversation.Messages.Insert(insertIndex, message)
    End Sub

    Private Sub CompleteInterruptedToolBatch(conversation As AgentConversationData,
                                             toolRoundLogs As List(Of ToolRoundLog),
                                             resultText As String,
                                             state As String)
        If conversation?.Messages Is Nothing Then Return
        Dim assistantIndex = conversation.Messages.FindLastIndex(
            Function(x) x IsNot Nothing AndAlso
                String.Equals(x.Role, "assistant", StringComparison.OrdinalIgnoreCase) AndAlso
                x.ToolCalls IsNot Nothing AndAlso x.ToolCalls.Count > 0)
        If assistantIndex < 0 Then Return

        Dim assistantMessage = conversation.Messages(assistantIndex)
        Dim completedIds = conversation.Messages.Skip(assistantIndex + 1).
            Where(Function(x) x IsNot Nothing AndAlso String.Equals(x.Role, "tool", StringComparison.OrdinalIgnoreCase)).
            Select(Function(x) If(x.ToolCallId, "")).
            Where(Function(x) x <> "").
            ToHashSet(StringComparer.OrdinalIgnoreCase)
        Dim logs = If(toolRoundLogs, New List(Of ToolRoundLog)).
            SelectMany(Function(x) If(x?.Calls, New List(Of ToolRunLog))).
            Where(Function(x) x IsNot Nothing).
            ToList()

        For Each callInfo In assistantMessage.ToolCalls.Where(Function(x) x IsNot Nothing)
            If Not String.IsNullOrWhiteSpace(callInfo.Id) AndAlso completedIds.Contains(callInfo.Id) Then Continue For

            Dim log = logs.LastOrDefault(Function(x) String.Equals(x.CallId, callInfo.Id, StringComparison.OrdinalIgnoreCase))
            Dim activity = log?.Activity
            If activity Is Nothing AndAlso _activeTurn IsNot Nothing Then
                activity = New AgentTurnActivityData With {
                    .Kind = "tool",
                    .ToolName = If(callInfo.Name, ""),
                    .Arguments = If(callInfo.Arguments, ""),
                    .CreatedAt = DateTime.Now
                }
                _activeTurn.Activities.Add(activity)
            End If
            If activity IsNot Nothing Then
                activity.ResultText = If(resultText, "")
                activity.State = If(state, "error")
                If activity.ElapsedMilliseconds < 0 Then activity.ElapsedMilliseconds = 0
                If IsConversationSelected(conversation) Then
                    Dim item = If(log?.Item, AgentRoom1.FindItem(activity.Id))
                    If item Is Nothing Then
                        item = AgentRoom1.AddToolCall(_activeTurn.Id,
                                                     FormatToolActivityTitle(activity),
                                                     FormatToolActivityDetails(activity),
                                                     activity.Id,
                                                     expanded:=False,
                                                     running:=False,
                                                     isError:=String.Equals(state, "error", StringComparison.OrdinalIgnoreCase))
                    Else
                        item.Title = FormatToolActivityTitle(activity)
                        item.Text = FormatToolActivityDetails(activity)
                        item.IsRunning = False
                        item.IsError = String.Equals(state, "error", StringComparison.OrdinalIgnoreCase)
                    End If
                End If
            End If

            AppendRunConversationMessage(conversation, New AgentMessageData With {
                .Role = "tool",
                .Name = If(callInfo.Name, ""),
                .ToolCallId = If(callInfo.Id, ""),
                .Content = If(resultText, "")
            })
        Next
    End Sub

    Private Async Function BuildRequestMessagesAsync(client As AgentEndpointClient,
                                                     conversation As AgentConversationData,
                                                     modelId As String,
                                                     tools As List(Of Dictionary(Of String, Object)),
                                                     cancellationToken As Threading.CancellationToken) As Task(Of List(Of AgentMessageData))
        Dim conversationMessages = GetRequestConversationMessages(conversation)
        Dim plan = BuildContextCompressionPlan(conversation, conversationMessages, tools)
        Dim pass = 0
        While plan.RequiresCompaction AndAlso pass < MaxContextCompactionPasses
            cancellationToken.ThrowIfCancellationRequested()
            pass += 1
            If Not Await EnsureContextSummaryAsync(client, conversation, conversationMessages, modelId, plan, cancellationToken) Then Exit While
            plan = BuildContextCompressionPlan(conversation, conversationMessages, tools)
        End While

        plan = BuildContextCompressionPlan(conversation, conversationMessages, tools)
        If plan.CurrentRequestTokens > plan.AutoCompactTokenLimit Then
            Throw New InvalidOperationException(
                $"上下文约 {plan.CurrentRequestTokens} tokens，超过当前安全请求上限 {plan.AutoCompactTokenLimit} tokens，且自动压缩未能继续。请减少历史记录或更换上下文更大的模型。")
        End If

        conversationMessages = BuildCompressedConversationMessages(conversation, conversationMessages)

        Return BuildRequestMessagesWithSystem(conversation, conversationMessages)
    End Function

    Private Function GetRequestConversationMessages(conversation As AgentConversationData) As List(Of AgentMessageData)
        If conversation?.Messages Is Nothing Then Return New List(Of AgentMessageData)
        Return conversation.Messages.
            Where(Function(x) x IsNot Nothing AndAlso Not String.Equals(If(x.Role, ""), "card", StringComparison.OrdinalIgnoreCase)).
            ToList()
    End Function

    Private Function BuildContextCompressionPlan(conversation As AgentConversationData,
                                                 messages As List(Of AgentMessageData),
                                                 tools As List(Of Dictionary(Of String, Object))) As ContextCompressionPlan
        Dim contextWindowTokens = ResolveContextWindowTokens(If(conversation?.ModelId, ""))
        Dim completionReserveTokens = Math.Min(8192,
            Math.Max(ContextMinimumCompletionReserveTokens, CInt(Math.Ceiling(contextWindowTokens * 0.06))))
        Dim summaryBudgetTokens = Math.Min(ContextSummaryLimitTokens,
            Math.Max(1024, CInt(Math.Floor(contextWindowTokens * 0.15))))
        Dim toolDefinitionTokens = EstimateToolDefinitionTokens(tools)
        Dim plan As New ContextCompressionPlan With {
            .ContextWindowTokens = contextWindowTokens,
            .AutoCompactTokenLimit = Math.Max(1, CInt(Math.Floor(contextWindowTokens * ContextAutoCompactRatio))),
            .ToolDefinitionTokens = toolDefinitionTokens,
            .CompletionReserveTokens = completionReserveTokens,
            .SummaryBudgetTokens = summaryBudgetTokens
        }

        Dim systemTokens = EstimateRequestTokenCount(Agent提示词_v6.构建系统提示词(
            AgentNetworkMode.Normalize(conversation.NetworkMode),
            GetPermissionLevelDisplayName(conversation.PermissionLevel)))
        Dim fixedRequestTokens = systemTokens + toolDefinitionTokens + completionReserveTokens + ContextRequestEnvelopeReserveTokens
        If messages Is Nothing OrElse messages.Count = 0 Then
            plan.CurrentRequestTokens = fixedRequestTokens
            Return plan
        End If

        plan.ExistingBoundaryIndex = ResolveContextCheckpointBoundaryIndex(conversation, messages)
        Dim activeMessages = BuildCompressedConversationMessages(conversation, messages)
        plan.CurrentRequestTokens = fixedRequestTokens + activeMessages.Sum(Function(x) EstimateMessageTokens(x))
        If plan.CurrentRequestTokens <= plan.AutoCompactTokenLimit Then Return plan

        plan.CompactModelId = Agent上下文能力表_v6.选择上下文压缩模型(_models, conversation.ModelId)
        Dim compactContextWindow = ResolveContextWindowTokens(plan.CompactModelId)
        Dim compactSummaryBudget = Math.Min(ContextSummaryLimitTokens,
            Math.Max(1024, CInt(Math.Floor(compactContextWindow * 0.15))))
        plan.SummaryBudgetTokens = Math.Min(summaryBudgetTokens, compactSummaryBudget)
        plan.RecentMessageBudgetTokens = Math.Max(0, plan.AutoCompactTokenLimit - fixedRequestTokens - plan.SummaryBudgetTokens)
        plan.CompressionSourceBudgetTokens = Math.Max(1000,
            CInt(Math.Floor(compactContextWindow * ContextAutoCompactRatio)) - compactSummaryBudget - ContextRequestEnvelopeReserveTokens)
        Dim recentTokens = 0
        Dim firstRetainedIndex = messages.Count

        For i = messages.Count - 1 To plan.ExistingBoundaryIndex + 1 Step -1
            Dim messageTokens = EstimateMessageTokens(messages(i))
            If firstRetainedIndex < messages.Count AndAlso recentTokens + messageTokens > plan.RecentMessageBudgetTokens Then Exit For
            recentTokens += messageTokens
            firstRetainedIndex = i
        Next

        Dim desiredBoundary = AdjustBoundaryBeforeToolMessage(messages, firstRetainedIndex - 1, plan.ExistingBoundaryIndex)
        If desiredBoundary <= plan.ExistingBoundaryIndex Then Return plan

        Dim checkpointSummaryTokens = EstimateRequestTokenCount(If(conversation?.ContextCheckpoint?.Summary, ""))
        Dim sourceTokens = checkpointSummaryTokens + 900
        Dim limitedBoundary = plan.ExistingBoundaryIndex
        For i = plan.ExistingBoundaryIndex + 1 To desiredBoundary
            Dim messageTokens = EstimateRequestTokenCount(FormatMessageForCompression(messages(i), i + 1))
            If sourceTokens + messageTokens > plan.CompressionSourceBudgetTokens Then Exit For
            sourceTokens += messageTokens
            limitedBoundary = i
        Next

        limitedBoundary = AdjustBoundaryBeforeToolMessage(messages, limitedBoundary, plan.ExistingBoundaryIndex)
        If limitedBoundary <= plan.ExistingBoundaryIndex Then Return plan

        plan.TargetBoundaryIndex = limitedBoundary
        plan.CompressionSourceTokens = Math.Max(0, sourceTokens)
        plan.RecentMessageTokens = messages.Skip(limitedBoundary + 1).Sum(Function(x) EstimateMessageTokens(x))
        Return plan
    End Function

    Private Async Function EnsureContextSummaryAsync(client As AgentEndpointClient,
                                                     conversation As AgentConversationData,
                                                     conversationMessages As List(Of AgentMessageData),
                                                     modelId As String,
                                                     plan As ContextCompressionPlan,
                                                     cancellationToken As Threading.CancellationToken) As Task(Of Boolean)
        If client Is Nothing OrElse conversation Is Nothing OrElse conversationMessages Is Nothing OrElse plan Is Nothing Then Return False
        Dim existingBoundary = ResolveContextCheckpointBoundaryIndex(conversation, conversationMessages)
        Dim targetBoundary = Math.Min(plan.TargetBoundaryIndex, conversationMessages.Count - 1)
        If targetBoundary <= existingBoundary Then Return False

        Dim previousSummary = If(conversation.ContextCheckpoint?.Summary, "").Trim()
        Dim sourceStart = existingBoundary + 1
        Dim sourceMessages = conversationMessages.Skip(sourceStart).Take(targetBoundary - existingBoundary).ToList()
        If sourceMessages.Count = 0 Then Return False

        Dim compactModelId = If(String.IsNullOrWhiteSpace(plan.CompactModelId),
                                Agent上下文能力表_v6.选择上下文压缩模型(_models, modelId),
                                plan.CompactModelId)
        If String.IsNullOrWhiteSpace(compactModelId) Then Return False

        ShowRunStatus(conversation, $"正在压缩上下文：使用 {compactModelId}，活动上下文约 {plan.CurrentRequestTokens} / {plan.AutoCompactTokenLimit} tokens，本批推进到第 {targetBoundary + 1} 条")
        Dim compactMessages = BuildContextCompressionMessages(previousSummary,
                                                               sourceMessages,
                                                               targetBoundary + 1,
                                                               plan.SummaryBudgetTokens)
        Dim result = Await client.TryCreateChatCompletionAsync(compactModelId, compactMessages, Nothing, "", cancellationToken)
        cancellationToken.ThrowIfCancellationRequested()

        If result IsNot Nothing AndAlso result.Usage IsNot Nothing Then AddUsage(conversation, result.Usage, compactModelId)
        If result Is Nothing OrElse Not result.Success OrElse String.IsNullOrWhiteSpace(result.Content) Then
            Dim errorText = If(result?.ErrorMessage, "模型没有返回摘要")
            ShowRunStatus(conversation, "上下文压缩失败：" & errorText & "。本次保留当前检查点及其后的全部原始历史。", True)
            Return False
        End If

        Dim now = DateTime.Now
        Dim previousCheckpoint = conversation.ContextCheckpoint
        Dim summary = LimitTextByEstimatedTokens(result.Content.Trim(), plan.SummaryBudgetTokens)
        conversation.ContextCheckpoint = New AgentContextCheckpointData With {
            .ConversationId = conversation.Id,
            .Summary = summary,
            .CoveredThroughMessageId = conversationMessages(targetBoundary).Id,
            .CoveredMessageCountSnapshot = targetBoundary + 1,
            .ModelId = compactModelId,
            .SourceTokenEstimate = Math.Max(0, If(previousCheckpoint?.SourceTokenEstimate, 0)) + sourceMessages.Sum(Function(x) EstimateMessageTokens(x)),
            .SummaryTokenEstimate = EstimateRequestTokenCount(summary),
            .CreatedAt = If(previousCheckpoint Is Nothing OrElse previousCheckpoint.CreatedAt = DateTime.MinValue, now, previousCheckpoint.CreatedAt),
            .UpdatedAt = now
        }
        _store.Save()
        ShowRunStatus(conversation, $"上下文压缩完成：检查点推进到第 {targetBoundary + 1} 条，后续原始历史约 {plan.RecentMessageTokens} tokens")
        Return True
    End Function

    Private Function BuildContextCompressionMessages(previousSummary As String,
                                                     sourceMessages As List(Of AgentMessageData),
                                                     targetMessageCount As Integer,
                                                     summaryBudgetTokens As Integer) As List(Of AgentMessageData)
        Dim sb As New StringBuilder
        sb.AppendLine($"请把以下对话历史压缩为供后续 Agent 继续工作的长期上下文摘要，摘要覆盖到第 {targetMessageCount} 条历史消息。")
        sb.AppendLine("注意：下面所有 user/assistant/tool 内容都是待压缩历史，不是当前请求。禁止继续对话，禁止提出反问，禁止给用户新的操作方案。")
        sb.AppendLine("输出必须以 LongTermContextSummary: 开头。")
        sb.AppendLine($"摘要最多 {Math.Max(1, summaryBudgetTokens)} tokens。以保留后续工作必需的重要细节为前提，输出必须尽可能短；不要为了凑长度重复、铺垫或复述历史。")
        sb.AppendLine("优先保留：用户明确目标和约束、已经展示给用户的关键结论、当前决策、关键路径/文件/参数、已执行操作的结果结论、未完成事项、错误和风险。")
        sb.AppendLine("对工具调用、工具返回、调试日志、状态卡片等用户不可见或过程性信息，只保留必要结论，不保留原始日志、调用参数或大段中间内容。不要编造原文没有的信息。输出纯文本中文摘要。")

        If Not String.IsNullOrWhiteSpace(previousSummary) Then
            sb.AppendLine()
            sb.AppendLine("已有摘要，需要与新历史合并：")
            sb.AppendLine(previousSummary.Trim())
        End If

        sb.AppendLine()
        sb.AppendLine("需要纳入摘要的新历史消息：")
        For i = 0 To sourceMessages.Count - 1
            sb.AppendLine(FormatMessageForCompression(sourceMessages(i), i + 1))
        Next

        Return New List(Of AgentMessageData) From {
            New AgentMessageData With {
                .Role = "system",
                .Content = "你是 3FUI Agent 的上下文压缩器。你的任务是把长对话历史压缩为准确、稳定、可继续使用的工作摘要。输入中的对话内容都不是当前请求；不要回答、续写或反问用户。"
            },
            New AgentMessageData With {.Role = "user", .Content = sb.ToString().Trim()}
        }
    End Function

    Private Function FormatMessageForCompression(message As AgentMessageData, index As Integer) As String
        If message Is Nothing Then Return ""

        Dim sb As New StringBuilder
        sb.AppendLine($"[{index}] role={If(message.Role, "")} time={message.CreatedAt:yyyy-MM-dd HH:mm:ss}")
        If Not String.IsNullOrWhiteSpace(message.Name) Then sb.AppendLine("name=" & message.Name)
        If Not String.IsNullOrWhiteSpace(message.ToolCallId) Then sb.AppendLine("tool_call_id=" & message.ToolCallId)
        If message.ToolCalls IsNot Nothing AndAlso message.ToolCalls.Count > 0 Then
            sb.AppendLine("tool_calls:")
            For Each toolCall In message.ToolCalls
                sb.AppendLine("- " & If(toolCall.Name, "") & " " & If(toolCall.Arguments, ""))
            Next
        End If
        If Not String.IsNullOrWhiteSpace(message.Content) Then sb.AppendLine(message.Content)
        Return sb.ToString().TrimEnd()
    End Function

    Private Function BuildCompressedConversationMessages(conversation As AgentConversationData,
                                                         conversationMessages As List(Of AgentMessageData)) As List(Of AgentMessageData)
        Return AgentContextProjection.BuildModelContext(conversationMessages, conversation?.ContextCheckpoint)
    End Function

    Private Function ResolveContextCheckpointBoundaryIndex(conversation As AgentConversationData,
                                                           messages As List(Of AgentMessageData)) As Integer
        Return AgentContextProjection.ResolveBoundaryIndex(messages, conversation?.ContextCheckpoint)
    End Function

    Private Function AdjustBoundaryBeforeToolMessage(messages As List(Of AgentMessageData),
                                                     boundaryIndex As Integer,
                                                     minimumBoundaryIndex As Integer) As Integer
        Dim result = Math.Min(boundaryIndex, If(messages?.Count, 0) - 1)
        While result > minimumBoundaryIndex AndAlso
              result + 1 < messages.Count AndAlso
              String.Equals(If(messages(result + 1).Role, ""), "tool", StringComparison.OrdinalIgnoreCase)
            result -= 1
        End While
        Return result
    End Function

    Private Function BuildRequestMessagesWithSystem(conversation As AgentConversationData,
                                                    conversationMessages As List(Of AgentMessageData)) As List(Of AgentMessageData)
        Dim systemText = Agent提示词_v6.构建系统提示词(
            AgentNetworkMode.Normalize(conversation.NetworkMode),
            GetPermissionLevelDisplayName(conversation.PermissionLevel))

        Dim result As New List(Of AgentMessageData) From {
            New AgentMessageData With {.Role = "system", .Content = systemText}
        }
        result.AddRange(If(conversationMessages, New List(Of AgentMessageData)))
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

    Private Function ResolveContextWindowTokens(modelId As String) As Integer
        Dim configuredModel = If(_models, New List(Of AgentModelInfo)).
            FirstOrDefault(Function(x) x IsNot Nothing AndAlso String.Equals(x.Id, modelId, StringComparison.OrdinalIgnoreCase))
        If configuredModel IsNot Nothing AndAlso configuredModel.ContextWindowTokens > 0 Then
            Return configuredModel.ContextWindowTokens
        End If
        Return Agent上下文能力表_v6.获取上下文总量(modelId)
    End Function

    Private Function EstimateMessageTokens(message As AgentMessageData) As Integer
        If message Is Nothing Then Return 0
        Dim total = 4 + EstimateRequestTokenCount(If(message.Role, ""))
        total += EstimateRequestTokenCount(If(message.Content, ""))
        total += EstimateRequestTokenCount(If(message.Name, ""))
        total += EstimateRequestTokenCount(If(message.ToolCallId, ""))
        If message.ToolCalls IsNot Nothing Then
            For Each toolCall In message.ToolCalls
                total += 8 + EstimateRequestTokenCount(If(toolCall.Name, "")) + EstimateRequestTokenCount(If(toolCall.Arguments, ""))
            Next
        End If
        Return total
    End Function

    Private Function EstimateToolDefinitionTokens(tools As List(Of Dictionary(Of String, Object))) As Integer
        If tools Is Nothing OrElse tools.Count = 0 Then Return 0
        Try
            Return EstimateRequestTokenCount(System.Text.Json.JsonSerializer.Serialize(tools))
        Catch
            Return tools.Count * 256
        End Try
    End Function

    Private Function EstimateRequestTokenCount(text As String) As Integer
        text = If(text, "")
        If text = "" Then Return 0

        Dim asciiRun As Integer = 0
        Dim tokens As Integer = 0
        For Each ch In text
            If AscW(ch) >= 0 AndAlso AscW(ch) < 128 Then
                asciiRun += 1
            Else
                If asciiRun > 0 Then
                    tokens += CInt(Math.Ceiling(asciiRun / 4.0))
                    asciiRun = 0
                End If
                tokens += 1
            End If
        Next
        If asciiRun > 0 Then tokens += CInt(Math.Ceiling(asciiRun / 4.0))

        Return Math.Max(1, tokens)
    End Function

    Private Function LimitTextByEstimatedTokens(text As String, maxTokens As Integer) As String
        text = If(text, "")
        If maxTokens <= 0 OrElse EstimateRequestTokenCount(text) <= maxTokens Then Return text

        Const suffix As String = "...[已截断]"
        Dim contentBudget = maxTokens - EstimateRequestTokenCount(suffix)
        If contentBudget <= 0 Then Return suffix

        Dim tokens As Integer = 0
        Dim asciiRun As Integer = 0
        Dim safeLength As Integer = 0
        For Each ch In text
            Dim nextTokens As Integer
            If AscW(ch) >= 0 AndAlso AscW(ch) < 128 Then
                Dim previousAsciiTokens = CInt(Math.Ceiling(asciiRun / 4.0))
                asciiRun += 1
                Dim nextAsciiTokens = CInt(Math.Ceiling(asciiRun / 4.0))
                nextTokens = tokens + (nextAsciiTokens - previousAsciiTokens)
            Else
                asciiRun = 0
                nextTokens = tokens + 1
            End If

            If nextTokens > contentBudget Then Exit For
            tokens = nextTokens
            safeLength += 1
        Next

        If safeLength <= 0 Then Return suffix
        Return text.Substring(0, safeLength).TrimEnd() & suffix
    End Function

    Private Sub AddUsage(conversation As AgentConversationData, usage As AgentUsageInfo, modelId As String)
        If usage Is Nothing Then Return
        EnrichUsageForRequest(usage, modelId)
        _pageUsage.Add(usage)
        If conversation.Usage Is Nothing Then conversation.Usage = New AgentUsageInfo
        conversation.Usage.Add(usage)
        UpdateUsageButton()
    End Sub

    Private Sub EnrichUsageForRequest(usage As AgentUsageInfo, modelId As String)
        If usage Is Nothing Then Return
        If usage.EffectiveInputTokens <= 0 Then usage.EffectiveInputTokens = GetUsageInputTokens(usage)
        If usage.EffectiveOutputTokens <= 0 Then usage.EffectiveOutputTokens = GetUsageOutputTokens(usage)
        If usage.TotalTokens <= 0 Then usage.TotalTokens = usage.EffectiveInputTokens + usage.EffectiveOutputTokens
        usage.LastRequestInputTokens = usage.EffectiveInputTokens
        usage.LastRequestTotalTokens = usage.TotalTokens
        usage.LastRequestCachedTokens = usage.CachedTokens
        usage.LastRequestContextWindowTokens = ResolveContextWindowTokens(modelId)
        usage.LastRequestModelId = If(modelId, "")
    End Sub

    Private Sub AddSubmittedFiles(paths As IEnumerable(Of String))
        If paths Is Nothing Then Return

        For Each pathValue In NormalizeSubmittedPaths(paths)
            If _pendingFiles.Any(Function(x) String.Equals(x, pathValue, StringComparison.OrdinalIgnoreCase)) Then Continue For
            _pendingFiles.Add(pathValue)
        Next
        RefreshSubmittedFileList()
    End Sub

    Private Function NormalizeSubmittedPaths(paths As IEnumerable(Of String)) As List(Of String)
        Dim result As New List(Of String)
        If paths Is Nothing Then Return result

        For Each raw In paths
            If String.IsNullOrWhiteSpace(raw) Then Continue For
            Try
                If Directory.Exists(raw) Then
                    result.Add(Path.TrimEndingDirectorySeparator(Path.GetFullPath(raw)))
                ElseIf File.Exists(raw) Then
                    result.Add(Path.GetFullPath(raw))
                End If
            Catch ex As Exception
                ExFloatingTip(ModernListBox2, "读取路径失败：" & ex.Message, 2200)
            End Try
        Next

        Return result
    End Function

    Private Sub RefreshSubmittedFileList()
        If ModernListBox2 Is Nothing Then Return
        Dim selectedPath = If(ModernListBox2.SelectedIndex >= 0 AndAlso ModernListBox2.SelectedIndex < _pendingFiles.Count, _pendingFiles(ModernListBox2.SelectedIndex), "")
        _refreshingSubmittedFiles = True
        Try
            ModernListBox2.Items.Clear()
            ModernListBox2.ItemToolTips.Clear()
            For Each pathValue In _pendingFiles
                Dim display = BuildSubmittedFileDisplayName(pathValue)
                ModernListBox2.Items.Add(display)
                ModernListBox2.ItemToolTips.Add(New LakeUI.ModernListBox.ToolTipEntry(display, pathValue))
            Next
            If selectedPath <> "" Then
                Dim index = _pendingFiles.FindIndex(Function(x) String.Equals(x, selectedPath, StringComparison.OrdinalIgnoreCase))
                If index >= 0 Then ModernListBox2.SelectedIndex = index
            End If
        Finally
            _refreshingSubmittedFiles = False
        End Try
    End Sub

    Private Function BuildSubmittedFileDisplayName(pathValue As String) As String
        Dim name = GetSubmittedPathName(pathValue)
        Dim prefix = If(Directory.Exists(pathValue), "[文件夹] ", "")
        Dim sameNameCount = _pendingFiles.Where(Function(x) String.Equals(GetSubmittedPathName(x), name, StringComparison.CurrentCultureIgnoreCase)).Count()
        If sameNameCount <= 1 Then Return prefix & name
        Dim parent = Path.GetFileName(Path.GetDirectoryName(pathValue))
        If parent = "" Then parent = Path.GetDirectoryName(pathValue)
        Return $"{prefix}{name}  ({parent})"
    End Function

    Private Function GetSubmittedPathName(pathValue As String) As String
        Dim name = Path.GetFileName(Path.TrimEndingDirectorySeparator(pathValue))
        Return If(name = "", pathValue, name)
    End Function

    Private Sub RemoveSelectedSubmittedFile()
        If ModernListBox2.SelectedIndex < 0 OrElse ModernListBox2.SelectedIndex >= _pendingFiles.Count Then Return
        _pendingFiles.RemoveAt(ModernListBox2.SelectedIndex)
        RefreshSubmittedFileList()
    End Sub

    Private Sub ClearSubmittedFiles()
        If _pendingFiles.Count = 0 Then Return
        _pendingFiles.Clear()
        RefreshSubmittedFileList()
    End Sub

    Private Function BuildSubmittedFilesContext() As String
        If _pendingFiles.Count = 0 Then Return ""
        Return String.Join(vbCrLf, _pendingFiles.Select(Function(pathValue) BuildSubmittedFileContextItem(pathValue)))
    End Function

    Private Function BuildSubmittedFileContextItem(pathValue As String) As String
        Try
            If Directory.Exists(pathValue) Then
                Return Path.GetFullPath(pathValue)
            End If
            Return Path.GetFullPath(pathValue)
        Catch ex As Exception
            Return pathValue
        End Try
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

    Private Sub ModernListBox2_DragEnter(sender As Object, e As DragEventArgs) Handles ModernListBox2.DragEnter
        e.Effect = If(e.Data IsNot Nothing AndAlso e.Data.GetDataPresent(DataFormats.FileDrop), DragDropEffects.Copy, DragDropEffects.None)
    End Sub

    Private Sub ModernListBox2_DragDrop(sender As Object, e As DragEventArgs) Handles ModernListBox2.DragDrop
        If e.Data Is Nothing OrElse Not e.Data.GetDataPresent(DataFormats.FileDrop) Then Return
        Dim files = TryCast(e.Data.GetData(DataFormats.FileDrop), String())
        AddSubmittedFiles(files)
    End Sub

    Private Sub ModernListBox2_MouseUp(sender As Object, e As MouseEventArgs) Handles ModernListBox2.MouseUp
        If e.Button <> MouseButtons.Right Then Return
        Using d As New OpenFileDialog With {.Multiselect = True, .Filter = "所有文件|*.*"}
            If d.ShowDialog(Me) = DialogResult.OK Then AddSubmittedFiles(d.FileNames)
        End Using
    End Sub

    Private Sub ModernListBox2_ItemDoubleClick(sender As Object, e As LakeUI.ModernListBox.ItemEventArgs) Handles ModernListBox2.ItemDoubleClick
        RemoveSelectedSubmittedFile()
    End Sub

    Private Sub ModernListBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles ModernListBox2.KeyDown
        If e.KeyCode <> Keys.Delete Then Return
        RemoveSelectedSubmittedFile()
        e.Handled = True
        e.SuppressKeyPress = True
    End Sub

    Private Sub ModernListBox2_ItemOrderChanged(sender As Object, e As EventArgs) Handles ModernListBox2.ItemOrderChanged
        If _refreshingSubmittedFiles Then Return
        If ModernListBox2.Items.Count <> _pendingFiles.Count Then Return
        Dim remaining As New List(Of String)(_pendingFiles)
        Dim reordered As New List(Of String)
        For Each rawItem In ModernListBox2.Items
            Dim text = If(rawItem, "").ToString()
            Dim index = remaining.FindIndex(Function(x) String.Equals(BuildSubmittedFileDisplayName(x), text, StringComparison.Ordinal))
            If index < 0 Then Continue For
            reordered.Add(remaining(index))
            remaining.RemoveAt(index)
        Next
        If reordered.Count <> _pendingFiles.Count Then Return
        _pendingFiles.Clear()
        _pendingFiles.AddRange(reordered)
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

    Private Async Sub MB_重载连接_Click(sender As Object, e As EventArgs) Handles MB_重载连接.Click
        Await RefreshModelsIfEndpointChangedAsync(True)
    End Sub

    Private Sub MB_操作提示_Click(sender As Object, e As EventArgs) Handles MB_操作提示.Click
        ExOverlayMsgBox(FormMain_v6, $"{vbCrLf}【文本框快捷键】{vbCrLf}【常规】按 Enter 发送，按 Shift + Enter 换行{vbCrLf}【重新推理】发送空信息可触发重试推理{vbCrLf}【撤回重发】按 Alt + Enter 用当前内容替换最新一次你的内容并重新开始推理{vbCrLf}【单纯撤回】按 Ctrl + Delete 删除最新一轮对话并将用户消息复制到剪贴板{vbCrLf}{vbCrLf}【关于上下文限制和推理级别】{vbCrLf}【*】几乎没有端点会返回模型的上下文限制和推理级别列表{vbCrLf}【*】因此 3FUI 使用兜底策略在本地建立静态数据{vbCrLf}【*】如果你希望对某个模型增加上下文或是增加推理级别可以提 issue{vbCrLf}【*】但我不会把上下文给的太高，这会浪费token，除非该模型特别便宜",, "Agent 操作提示")
    End Sub

    Private Sub AgentButtonLayoutChanged(sender As Object, e As EventArgs) Handles Panel4.SizeChanged, Panel1.SizeChanged, JustEmptyControl5.SizeChanged, JustEmptyControl6.SizeChanged
        ApplyAgentButtonPanelLayout()
    End Sub

    Private Sub ApplyAgentButtonPanelLayout()
        EqualizeTwoButtons(Panel4, MB_新对话, MB_删除对话, JustEmptyControl5)
        EqualizeTwoButtons(Panel1, MB_重载连接, MB_操作提示, JustEmptyControl6)
    End Sub

    Private Sub EqualizeTwoButtons(panel As Panel, leftButton As Control, rightButton As Control, gap As Control)
        If panel Is Nothing OrElse leftButton Is Nothing OrElse rightButton Is Nothing Then Return
        Dim gapWidth = If(gap Is Nothing OrElse Not gap.Visible, 0, gap.Width)
        Dim available = Math.Max(0, panel.DisplayRectangle.Width - gapWidth)
        Dim leftWidth = available \ 2
        If leftButton.Width <> leftWidth Then leftButton.Width = leftWidth
    End Sub

    Private Sub MB_页面用量_Click(sender As Object, e As EventArgs) Handles MB_页面用量.Click
        Dim currentUsage = If(_current?.Usage, New AgentUsageInfo)
        Dim contextWindowTokens = GetUsageContextWindowTokens(currentUsage)
        Dim detail =
            $"上下文窗口：{contextWindowTokens} tokens{vbCrLf}" &
            $"当前会话：{FormatUsage(currentUsage)}{vbCrLf}" &
            $"本页累计：{FormatUsage(_pageUsage)}{vbCrLf}" &
            $"最近请求：模型 {If(String.IsNullOrWhiteSpace(currentUsage.LastRequestModelId), "未知", currentUsage.LastRequestModelId)}，输入 {currentUsage.LastRequestInputTokens}，总量上下文 {FormatContextPercent(currentUsage)}，缓存命中率 {FormatLastRequestCacheHitRate(currentUsage)}"
        ExMsgBox(FormMain_v6, detail, MsgBoxStyle.Information, "页面用量")
    End Sub

    Private Function FormatUsage(usage As AgentUsageInfo) As String
        Return $"总 {usage.TotalTokens}，输入 {GetUsageInputTokens(usage)}，输出 {GetUsageOutputTokens(usage)}，缓存 {usage.CachedTokens}，缓存命中率 {FormatCacheHitRate(usage)}，推理 {usage.ReasoningTokens}"
    End Function

    Private Function GetUsageInputTokens(usage As AgentUsageInfo) As Integer
        If usage Is Nothing Then Return 0
        If usage.EffectiveInputTokens > 0 Then Return usage.EffectiveInputTokens
        If usage.InputTokens > 0 Then Return usage.InputTokens
        Return usage.PromptTokens
    End Function

    Private Function GetUsageOutputTokens(usage As AgentUsageInfo) As Integer
        If usage Is Nothing Then Return 0
        If usage.EffectiveOutputTokens > 0 Then Return usage.EffectiveOutputTokens
        If usage.OutputTokens > 0 Then Return usage.OutputTokens
        Return usage.CompletionTokens
    End Function

    Private Function FormatContextPercent(usage As AgentUsageInfo) As String
        If usage Is Nothing OrElse usage.LastRequestInputTokens <= 0 Then Return "0%"
        Dim windowTokens = GetUsageContextWindowTokens(usage)
        Return FormatPercent(usage.LastRequestInputTokens / CDbl(windowTokens))
    End Function

    Private Function GetUsageContextWindowTokens(usage As AgentUsageInfo) As Integer
        If usage IsNot Nothing AndAlso usage.LastRequestContextWindowTokens > 0 Then Return usage.LastRequestContextWindowTokens
        Dim modelId = If(usage?.LastRequestModelId, If(_current?.ModelId, ""))
        Return ResolveContextWindowTokens(modelId)
    End Function

    Private Function FormatCacheHitRate(usage As AgentUsageInfo) As String
        Dim inputTokens = GetUsageInputTokens(usage)
        If inputTokens <= 0 Then Return "0%"
        Return FormatPercent(Math.Min(1.0, Math.Max(0.0, usage.CachedTokens / CDbl(inputTokens))))
    End Function

    Private Function FormatLastRequestCacheHitRate(usage As AgentUsageInfo) As String
        If usage Is Nothing OrElse usage.LastRequestInputTokens <= 0 Then Return "0%"
        Return FormatPercent(Math.Min(1.0, Math.Max(0.0, usage.LastRequestCachedTokens / CDbl(usage.LastRequestInputTokens))))
    End Function

    Private Function FormatPercent(value As Double) As String
        If Double.IsNaN(value) OrElse Double.IsInfinity(value) Then Return "0%"
        Return value.ToString("P1", Globalization.CultureInfo.InvariantCulture)
    End Function

    Private Sub AgentRoom1_LinkClicked(sender As Object, e As AgentRoom.LinkClickedEventArgs) Handles AgentRoom1.LinkClicked
        If ExFloatingBox("确定打开此链接？", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Process.Start(New ProcessStartInfo With {.FileName = e.Url, .UseShellExecute = True})
        End If
    End Sub
End Class
