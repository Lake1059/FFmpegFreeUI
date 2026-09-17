Imports System.Text
Imports System.IO
Imports LakeUI

Partial Public Class Form_v6_Agent
    Private Const ContextSummaryLimitTokens As Integer = 12000
    Private Const ContextAutoCompactRatio As Double = 0.9
    Private Const MaxContextCompactionPasses As Integer = 32
    Private Const ContextRequestEnvelopeReserveTokens As Integer = 512
    Private Const ContextMinimumCompletionReserveTokens As Integer = 2048

    Private Class ContextCompressionPlan
        Public Property AutoCompactTokenLimit As Integer
        Public Property CompactModelId As String = ""
        Public Property CurrentRequestTokens As Integer
        Public Property RecentMessageBudgetTokens As Integer
        Public Property RecentMessageTokens As Integer
        Public Property ExistingBoundaryIndex As Integer = -1
        Public Property TargetBoundaryIndex As Integer = -1
        Public Property CompressionSourceBudgetTokens As Integer
        Public Property SummaryBudgetTokens As Integer

        Public ReadOnly Property RequiresCompaction As Boolean
            Get
                Return TargetBoundaryIndex > ExistingBoundaryIndex
            End Get
        End Property
    End Class

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
            .AutoCompactTokenLimit = Math.Max(1, CInt(Math.Floor(contextWindowTokens * ContextAutoCompactRatio))),
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

    Private Sub MB_页面用量_Click(sender As Object, e As EventArgs) Handles MB_页面用量.Click
        Dim currentUsage = If(_current?.Usage, New AgentUsageInfo)
        Dim currentContextTokens = Math.Max(0, currentUsage.LastRequestInputTokens)
        Dim contextWindowTokens = GetUsageContextWindowTokens(currentUsage)
        Dim lastRequestModelId = If(String.IsNullOrWhiteSpace(currentUsage.LastRequestModelId), "未知", currentUsage.LastRequestModelId)
        Dim detail =
            $"当前上下文用量：{currentContextTokens} / {contextWindowTokens} tokens{vbCrLf}" &
            $"当前上下文使用率：{FormatContextPercent(currentUsage)}{vbCrLf}" &
            $"最近请求模型：{lastRequestModelId}{vbCrLf}" &
            $"最近请求输入：{currentUsage.LastRequestInputTokens} tokens{vbCrLf}" &
            $"最近请求输出：{Math.Max(0, currentUsage.LastRequestTotalTokens - currentUsage.LastRequestInputTokens)} tokens{vbCrLf}" &
            $"最近请求总量：{Math.Max(0, currentUsage.LastRequestTotalTokens)} tokens{vbCrLf}" &
            $"最近请求缓存：{Math.Max(0, currentUsage.LastRequestCachedTokens)} tokens{vbCrLf}" &
            $"最近请求缓存命中率：{FormatLastRequestCacheHitRate(currentUsage)}{vbCrLf}" &
            $"当前对话累计总量：{Math.Max(0, currentUsage.TotalTokens)} tokens{vbCrLf}" &
            $"当前对话累计输入：{Math.Max(0, GetUsageInputTokens(currentUsage))} tokens{vbCrLf}" &
            $"当前对话累计输出：{Math.Max(0, GetUsageOutputTokens(currentUsage))} tokens{vbCrLf}" &
            $"当前对话累计缓存：{Math.Max(0, currentUsage.CachedTokens)} tokens{vbCrLf}" &
            $"当前对话累计缓存命中率：{FormatCacheHitRate(currentUsage)}{vbCrLf}" &
            $"当前对话累计推理：{Math.Max(0, currentUsage.ReasoningTokens)} tokens"
        ExMsgBox(FormMain_v6, detail, MsgBoxStyle.Information, "当前对话用量")
    End Sub

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
        Dim modelId = If(String.IsNullOrWhiteSpace(usage?.LastRequestModelId), If(_current?.ModelId, ""), usage.LastRequestModelId)
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
End Class
