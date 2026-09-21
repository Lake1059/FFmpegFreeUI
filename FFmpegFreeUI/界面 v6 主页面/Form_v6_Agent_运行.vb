Imports System.Text
Imports System.IO
Imports LakeUI

Partial Public Class Form_v6_Agent
    Private Const StreamFlushIntervalMs As Integer = 80
    Private Const StreamFlushCharacters As Integer = 384
    Private Const MaxConsecutiveUpstreamFailures As Integer = 5
    Private Const RetryDelayMilliseconds As Integer = 750

    Private Class StreamingTextBuffer
        Private ReadOnly _onTextAppended As Action(Of String)
        Private ReadOnly _pendingText As New StringBuilder
        Private ReadOnly _syncRoot As New Object
        Private ReadOnly _uiContext As Threading.SynchronizationContext
        Private _lastFlushUtc As DateTime = DateTime.MinValue

        Public Sub New(Optional onTextAppended As Action(Of String) = Nothing)
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
            If shouldFlush Then Flush()
        End Sub

        Public Sub Flush()
            Dim appendedText As String
            SyncLock _syncRoot
                If _pendingText.Length = 0 Then Return
                appendedText = _pendingText.ToString()
                _pendingText.Clear()
                _lastFlushUtc = DateTime.UtcNow
            End SyncLock

            Dim apply =
                Sub()
                    _onTextAppended?.Invoke(appendedText)
                End Sub
            If _uiContext IsNot Nothing AndAlso Not Object.ReferenceEquals(Threading.SynchronizationContext.Current, _uiContext) Then
                _uiContext.Send(Sub(state) apply(), Nothing)
            Else
                apply()
            End If
        End Sub
    End Class

    ' 异步续体和流式回调显式使用所属对话；界面事件使用当前选中的对话。
    ' 切换视图不会改变任何运行任务的归属。
    Private ReadOnly _conversationRuntimes As New Dictionary(Of String, ConversationRuntime)(StringComparer.OrdinalIgnoreCase)
    Private ReadOnly _emptyRuntime As New ConversationRuntime

    Private Class ConversationRuntime
        Public Busy As Boolean = False
        Public RequestCts As Threading.CancellationTokenSource = Nothing
        Public ReadOnly PendingSteeringMessages As New List(Of AgentMessageData)
        Public ActiveResponseItem As LakeUI.AgentRoom.ChatItem = Nothing
        Public ActiveThinkingItem As LakeUI.AgentRoom.ChatItem = Nothing
        Public ActiveThinkingActivity As AgentTurnActivityData = Nothing
        Public ReadOnly ActiveThinkingTextBuilder As New StringBuilder
        Public ActiveThinkingParser As LakeUI.AgentThinkingTextParser = Nothing
        Public ActiveStreamReceivedDelta As Boolean
        Public ActiveRunConversation As AgentConversationData = Nothing
        Public ActiveTurn As AgentTurnData = Nothing
        Public ActiveTurnItem As LakeUI.AgentRoom.ChatItem = Nothing
        Public ActiveResponseText As String = ""
        Public ReadOnly ActiveResponseTextBuilder As New StringBuilder
        Public ActiveResponseTextDirty As Boolean = False
        Public ActiveRunStatusText As String = ""
    End Class

    Private Function GetConversationRuntime(conversation As AgentConversationData) As ConversationRuntime
        If conversation Is Nothing Then Return _emptyRuntime
        Dim runtime As ConversationRuntime = Nothing
        If Not _conversationRuntimes.TryGetValue(conversation.Id, runtime) Then
            runtime = New ConversationRuntime()
            _conversationRuntimes.Add(conversation.Id, runtime)
        End If
        Return runtime
    End Function

    Private Async Function StartAgentRunAsync(Optional conversation As AgentConversationData = Nothing) As Task
        conversation = If(conversation, _current)
        Dim run = GetConversationRuntime(conversation)
        If run.Busy Then Return
        Dim runConversation = conversation
        If runConversation Is Nothing Then Return
        If String.IsNullOrWhiteSpace(runConversation.ModelId) Then
            If IsConversationSelected(runConversation) Then
                ShowStatus("请先选择模型。", True)
                ExFloatingTip(MCB_模型选择, "请先选择模型", 1600)
            End If
            Return
        End If

        run.Busy = True
        MB_发送.Enabled = True
        Dim localCts As New Threading.CancellationTokenSource()
        run.RequestCts = localCts
        run.ActiveRunConversation = runConversation
        run.PendingSteeringMessages.Clear()
        run.ActiveTurn = EnsureLatestTurn(runConversation)
        If run.ActiveTurn IsNot Nothing Then
            run.ActiveTurn.StartedAt = DateTime.Now
            run.ActiveTurn.CompletedAt = DateTime.MinValue
            run.ActiveTurn.State = "running"
            run.ActiveTurn.StatusText = "正在准备上下文"
        End If
        ReplaceActiveResponseText(conversation, "正在思考...")
        run.ActiveRunStatusText = ""
        run.ActiveResponseItem = Nothing
        run.ActiveTurnItem = Nothing
        UpdateSendButtonState()
        If IsConversationSelected(runConversation) Then
            If run.ActiveTurn IsNot Nothing Then
                run.ActiveTurnItem = AgentRoom1.FindItem(run.ActiveTurn.Id)
                If run.ActiveTurnItem Is Nothing Then
                    run.ActiveTurnItem = AgentRoom1.AddTurnHeader(run.ActiveTurn.Id, FormatTurnHeader(run.ActiveTurn), expanded:=True)
                Else
                    run.ActiveTurnItem.IsExpanded = True
                End If
            End If
            UpdateActiveRunOverviewCard(runConversation)
        End If
        StartActiveRunElapsedTimer()
        Try
            ShowRunStatus(runConversation, "正在准备上下文")
            SaveConversation(runConversation)

            Await RunAgentLoopAsync(runConversation, localCts.Token)
        Catch ex As OperationCanceledException When localCts.IsCancellationRequested
            CompleteInterruptedToolBatch(runConversation, "工具调用已取消，未返回结果。", "canceled")
            Dim canceledText = BuildCanceledResponseText(GetActiveResponseTextSnapshot(conversation))
            SetRunResponseText(runConversation, canceledText)
            AppendRunConversationMessage(runConversation, New AgentMessageData With {.Role = "assistant", .Content = canceledText})
            ShowRunStatus(runConversation, "已停止当前任务")
            FinishActiveTurn(conversation, "canceled", "已停止")
        Catch ex As Exception
            CompleteInterruptedToolBatch(runConversation, "工具调用因运行异常中止：" & ex.Message, "error")
            ShowRunStatus(runConversation, "系统故障：请求失败：" & ex.Message, True)
            SetRunResponseText(runConversation, "请求失败：" & ex.Message)
            AppendRunConversationMessage(runConversation, New AgentMessageData With {.Role = "assistant", .Content = GetActiveResponseTextSnapshot(conversation)})
            FinishActiveTurn(conversation, "error", ex.Message)
            If IsConversationSelected(conversation) Then ExFloatingTip(MB_发送, ex.Message, 2600)
        Finally
            run.RequestCts = Nothing
            localCts.Dispose()
            run.Busy = False
            ClearActiveRunState(conversation)
            run.PendingSteeringMessages.Clear()
            UpdateSendButtonState()
            SaveConversation(runConversation)
        End Try
    End Function

    Private Sub RequestStopAgentTask()
        Dim run = GetConversationRuntime(_current)
        If Not run.Busy Then Return
        If run.RequestCts IsNot Nothing AndAlso Not run.RequestCts.IsCancellationRequested Then
            run.RequestCts.Cancel()
            MB_发送.Text = "停止中"
            ShowRunStatus(run.ActiveRunConversation, "正在停止当前任务")
        End If
    End Sub

    Private Sub FinishActiveTurn(conversation As AgentConversationData, state As String, Optional statusText As String = "")
        Dim run = GetConversationRuntime(conversation)
        If run.ActiveTurn Is Nothing Then Return
        HideActiveThinking(conversation)
        For Each activity In If(run.ActiveTurn.Activities, New List(Of AgentTurnActivityData))
            If activity Is Nothing OrElse Not String.Equals(activity.State, "running", StringComparison.OrdinalIgnoreCase) Then Continue For
            activity.ElapsedMilliseconds = Math.Max(0, (DateTime.Now - activity.CreatedAt).TotalMilliseconds)
            activity.State = If(String.Equals(state, "canceled", StringComparison.OrdinalIgnoreCase), "canceled", "error")
            If String.IsNullOrWhiteSpace(activity.ResultText) Then activity.ResultText = If(statusText, "运行已结束")
            If activity.Kind = "tool" Then RefreshToolActivity(conversation, activity)
        Next
        run.ActiveTurn.State = If(state, "completed")
        run.ActiveTurn.CompletedAt = DateTime.Now
        If Not String.IsNullOrWhiteSpace(statusText) Then run.ActiveTurn.StatusText = statusText.Trim()
        If run.ActiveTurnItem IsNot Nothing AndAlso AgentRoom1.Items.Contains(run.ActiveTurnItem) Then
            run.ActiveTurnItem.Title = FormatTurnHeader(run.ActiveTurn)
            run.ActiveTurnItem.IsRunning = False
            run.ActiveTurnItem.IsError = String.Equals(run.ActiveTurn.State, "error", StringComparison.OrdinalIgnoreCase)
            run.ActiveTurnItem.IsExpanded = False
        End If
        If IsConversationSelected(conversation) Then FollowLatestWithOverview()
    End Sub

    Private Async Function RunAgentLoopAsync(conversation As AgentConversationData,
                                             cancellationToken As Threading.CancellationToken) As Task
        Dim run = GetConversationRuntime(conversation)
        Dim client = CreateClient()
        Dim modelId = If(conversation.ModelId, "")
        Dim reasoning = If(conversation.ReasoningEffort, "")
        Dim networkMode = AgentNetworkMode.Normalize(conversation.NetworkMode)
        Dim permissionLevel = Math.Max(0, conversation.PermissionLevel)
        Dim tools = AgentLocalTools.BuildToolDefinitions(permissionLevel, networkMode)
        Dim round As Integer = 0

        Using powerShellSession As New AgentLocalTools.PowerShellRunSession()
            Do
                cancellationToken.ThrowIfCancellationRequested()
                round += 1
                ' 每轮重新检查上下文预算；长工具循环同样需要自动压缩。
                Dim messages = Await BuildRequestMessagesAsync(client, conversation, modelId, tools, cancellationToken)
                ConsumePendingSteeringMessages(messages, conversation)
                Dim result = Await RequestChatCompletionWithRetryAsync(
                    client, conversation, modelId, messages, tools, reasoning, round, cancellationToken)
                If Not result.Success Then
                    Dim errorText = "请求失败：" & result.ErrorMessage
                    SetRunResponseText(conversation, errorText)
                    AppendRunConversationMessage(conversation, New AgentMessageData With {.Role = "assistant", .Content = errorText})
                    ShowRunStatus(conversation, errorText, True)
                    FinishActiveTurn(conversation, "error", result.ErrorMessage)
                    If IsConversationSelected(conversation) Then ExFloatingTip(MB_发送, result.ErrorMessage, 2600)
                    Exit Function
                End If
                AddUsage(conversation, result.Usage, modelId)

                If result.ToolCalls Is Nothing OrElse result.ToolCalls.Count = 0 Then
                    Dim content = StripAgentThinkingText(result.Content).Trim()
                    If content = "" Then
                        Dim streamedContent = GetActiveResponseTextSnapshot(conversation).Trim()
                        If Not IsRunResponsePlaceholder(streamedContent) Then content = streamedContent
                    End If
                    If content = "" Then content = "模型没有返回内容。"
                    If run.PendingSteeringMessages.Count > 0 Then
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
                    FinishActiveTurn(conversation, "completed", "响应完成")
                    Exit Function
                End If

                PromoteActiveResponseToActivity(conversation, StripAgentThinkingText(result.Content))
                ShowRunStatus(conversation, "正在调用工具：" & String.Join("、", result.ToolCalls.Select(Function(x) x.Name)))
                Dim assistantToolMessage As New AgentMessageData With {
                .Role = "assistant",
                .Name = AgentConversationSchema.ActivityMessageName,
                .Content = StripAgentThinkingText(result.Content),
                .ToolCalls = result.ToolCalls
            }
                messages.Add(assistantToolMessage)
                AppendRunConversationMessage(conversation, assistantToolMessage)
                SaveConversation(conversation)

                For Each callInfo In result.ToolCalls
                    cancellationToken.ThrowIfCancellationRequested()
                    Dim activity = BeginToolActivity(conversation, callInfo)

                    Dim toolTimer = System.Diagnostics.Stopwatch.StartNew()
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
                    toolTimer.Stop()
                    Dim elapsed = toolTimer.Elapsed
                    ' 完整返回保存在会话活动中，仅模型上下文沿用长度限制。
                    CompleteToolActivity(conversation, activity, toolResult, elapsed.TotalMilliseconds, False)
                    toolResult = Agent通用工具_v6.LimitText(toolResult, 16000)

                    Dim toolMessage As New AgentMessageData With {
                    .Role = "tool",
                    .Name = callInfo.Name,
                    .ToolCallId = callInfo.Id,
                    .Content = toolResult
                    }
                    messages.Add(toolMessage)
                    AppendRunConversationMessage(conversation, toolMessage)
                    SaveConversation(conversation)
                    ShowRunStatus(conversation, $"工具{GetToolExecutionStatus(activity)}：{callInfo.Name}，耗时 {FormatElapsedMilliseconds(elapsed.TotalMilliseconds)}")
                    cancellationToken.ThrowIfCancellationRequested()
                Next
                ConsumePendingSteeringMessages(messages, conversation)
            Loop
        End Using
    End Function

    Private Async Function RequestChatCompletionWithRetryAsync(client As AgentEndpointClient,
                                                                conversation As AgentConversationData,
                                                                modelId As String,
                                                                messages As List(Of AgentMessageData),
                                                                tools As List(Of Dictionary(Of String, Object)),
                                                                reasoning As String,
                                                                round As Integer,
                                                                cancellationToken As Threading.CancellationToken) As Task(Of AgentChatResult)
        Dim run = GetConversationRuntime(conversation)
        Dim lastResult As AgentChatResult = Nothing

        For attempt = 1 To MaxConsecutiveUpstreamFailures
            cancellationToken.ThrowIfCancellationRequested()
            If attempt > 1 Then
                Dim retryDelay = Math.Min(4000, RetryDelayMilliseconds * CInt(Math.Pow(2, Math.Min(attempt - 2, 3))))
                SetRunResponseText(conversation, "正在重新连接...")
                ShowRunStatus(conversation, $"上游请求连续失败 {attempt - 1}/{MaxConsecutiveUpstreamFailures}，正在重试：{If(lastResult?.ErrorMessage, "未知错误")}")
                Await Task.Delay(retryDelay, cancellationToken)
            End If

            If run.ActiveResponseItem Is Nothing Then ReplaceActiveResponseText(conversation, "正在思考...")
            ShowRunStatus(conversation, $"正在思考：第 {round} 轮")
            ShowActiveThinking(conversation)

            BeginThinkingTextStream(conversation)
            Dim streamBuffer As New StreamingTextBuffer(
                Sub(value) AppendRunResponseDelta(conversation, value))
            Dim result As AgentChatResult
            Try
                result = Await client.TryCreateChatCompletionStreamingAsync(
                    modelId, messages, tools, reasoning,
                    Sub(delta) streamBuffer.Append(delta), cancellationToken)
            Finally
                streamBuffer.Flush()
            End Try
            cancellationToken.ThrowIfCancellationRequested()
            If result.Success Then
                If run.ActiveStreamReceivedDelta Then
                    ApplyThinkingChunk(conversation, run.ActiveThinkingParser.Complete())
                Else
                    ParseCompleteAgentText(conversation, result.Content)
                End If
                CompleteThinkingText(conversation)
                Return result
            End If

            HideActiveThinking(conversation)
            run.ActiveThinkingParser?.Reset()
            run.ActiveThinkingActivity = Nothing
            run.ActiveThinkingTextBuilder.Clear()
            SetRunResponseText(conversation, "正在重新连接...")
            ShowRunStatus(conversation, $"流式响应不可用，切换非流式（连续失败 {attempt}/{MaxConsecutiveUpstreamFailures}）")
            result = Await client.TryCreateChatCompletionAsync(modelId, messages, tools, reasoning, cancellationToken)
            cancellationToken.ThrowIfCancellationRequested()
            If result.Success Then
                ParseCompleteAgentText(conversation, result.Content)
                CompleteThinkingText(conversation)
                Return result
            End If
            lastResult = result
        Next

        Return If(lastResult, AgentChatResult.Fail("上游请求连续失败。"))
    End Function

    Private Function ConsumePendingSteeringMessages(messages As List(Of AgentMessageData),
                                                     conversation As AgentConversationData) As Boolean
        Dim run = GetConversationRuntime(conversation)
        If messages Is Nothing OrElse run.PendingSteeringMessages.Count = 0 Then Return False
        For Each steeringMessage In run.PendingSteeringMessages
            If steeringMessage IsNot Nothing AndAlso Not messages.Any(Function(x) x.Id = steeringMessage.Id) Then messages.Add(steeringMessage)
        Next
        Dim count = run.PendingSteeringMessages.Count
        run.PendingSteeringMessages.Clear()
        ReplaceActiveResponseText(conversation, "正在根据调整继续...")
        ShowRunStatus(conversation, $"正在应用 {count} 条调整方向")
        Return True
    End Function

    Private Sub AppendRunConversationMessage(conversation As AgentConversationData, message As AgentMessageData)
        Dim run = GetConversationRuntime(conversation)
        If conversation?.Messages Is Nothing OrElse message Is Nothing Then Return
        Dim insertIndex = conversation.Messages.Count
        For Each pending In run.PendingSteeringMessages
            If pending Is Nothing Then Continue For
            Dim candidate = conversation.Messages.IndexOf(pending)
            If candidate >= 0 Then insertIndex = Math.Min(insertIndex, candidate)
        Next
        conversation.Messages.Insert(insertIndex, message)
    End Sub

    Private Sub CompleteInterruptedToolBatch(conversation As AgentConversationData,
                                             resultText As String,
                                             state As String)
        Dim run = GetConversationRuntime(conversation)
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
        For Each callInfo In assistantMessage.ToolCalls.Where(Function(x) x IsNot Nothing)
            If Not String.IsNullOrWhiteSpace(callInfo.Id) AndAlso completedIds.Contains(callInfo.Id) Then Continue For

            Dim activity = run.ActiveTurn?.Activities.LastOrDefault(Function(x) x IsNot Nothing AndAlso
                x.Kind = "tool" AndAlso String.Equals(x.ToolCallId, callInfo.Id, StringComparison.OrdinalIgnoreCase))
            If activity Is Nothing AndAlso run.ActiveTurn IsNot Nothing Then
                activity = New AgentTurnActivityData With {
                    .Kind = "tool",
                    .ToolName = If(callInfo.Name, ""),
                    .ToolCallId = If(callInfo.Id, ""),
                    .Arguments = If(callInfo.Arguments, ""),
                    .CreatedAt = DateTime.Now
                }
                run.ActiveTurn.Activities.Add(activity)
            End If
            If activity IsNot Nothing Then
                activity.ResultText = If(resultText, "")
                activity.State = If(state, "error")
                If activity.ElapsedMilliseconds < 0 Then activity.ElapsedMilliseconds = Math.Max(0, (DateTime.Now - activity.CreatedAt).TotalMilliseconds)
                RefreshToolActivity(conversation, activity)
            End If

            AppendRunConversationMessage(conversation, New AgentMessageData With {
                .Role = "tool",
                .Name = If(callInfo.Name, ""),
                .ToolCallId = If(callInfo.Id, ""),
                .Content = If(resultText, "")
            })
        Next
    End Sub
End Class
