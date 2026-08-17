Imports System.Text.Json
Imports System.Text.Json.Nodes

Public Class AgentConversationUpgradeResult
    Public Property Conversation As AgentConversationData
    Public Property LegacyCheckpoint As AgentContextCheckpointData
    Public Property WasUpgraded As Boolean
End Class

Public NotInheritable Class AgentConversationJsonUpgrader
    Private Sub New()
    End Sub

    Public Shared Function Upgrade(rawJson As String) As AgentConversationUpgradeResult
        If String.IsNullOrWhiteSpace(rawJson) Then Return Nothing
        Dim root = TryCast(JsonNode.Parse(rawJson), JsonObject)
        If root Is Nothing Then Return Nothing

        Dim originalVersion = GetInteger(root, "Version", 0)
        Dim requiresTurnUpgrade = originalVersion < AgentConversationSchema.LatestVersion OrElse Not TypeOf root("Turns") Is JsonArray
        Dim changed As Boolean = originalVersion < AgentConversationSchema.LatestVersion
        EnsureObjectArray(root, "Messages", changed)
        EnsureObjectArray(root, "Turns", changed)
        EnsureObject(root, "Usage", changed)
        EnsureIdentifier(root, "Id", changed)

        Dim messagesNode = TryCast(root("Messages"), JsonArray)
        If messagesNode IsNot Nothing Then
            For Each node In messagesNode
                Dim messageObject = TryCast(node, JsonObject)
                If messageObject IsNot Nothing Then EnsureIdentifier(messageObject, "Id", changed)
            Next
        End If

        root("Version") = JsonValue.Create(AgentConversationSchema.LatestVersion)
        Dim conversation = JsonSerializer.Deserialize(Of AgentConversationData)(root.ToJsonString(), JsonSO)
        If conversation Is Nothing Then Return Nothing
        conversation.Version = AgentConversationSchema.LatestVersion
        NormalizeMessageKinds(conversation, changed)
        If requiresTurnUpgrade Then UpgradeTurns(conversation, changed)

        Return New AgentConversationUpgradeResult With {
            .Conversation = conversation,
            .LegacyCheckpoint = BuildLegacyCheckpoint(root, conversation),
            .WasUpgraded = changed
        }
    End Function

    Private Shared Sub EnsureObjectArray(root As JsonObject, propertyName As String, ByRef changed As Boolean)
        If TypeOf root(propertyName) Is JsonArray Then Return
        root(propertyName) = New JsonArray()
        changed = True
    End Sub

    Private Shared Sub EnsureObject(root As JsonObject, propertyName As String, ByRef changed As Boolean)
        If TypeOf root(propertyName) Is JsonObject Then Return
        root(propertyName) = New JsonObject()
        changed = True
    End Sub

    Private Shared Sub EnsureIdentifier(root As JsonObject, propertyName As String, ByRef changed As Boolean)
        If Not String.IsNullOrWhiteSpace(GetString(root, propertyName)) Then Return
        root(propertyName) = JsonValue.Create(Guid.NewGuid().ToString("N"))
        changed = True
    End Sub

    Private Shared Sub NormalizeMessageKinds(conversation As AgentConversationData, ByRef changed As Boolean)
        If conversation.Messages Is Nothing Then conversation.Messages = New List(Of AgentMessageData)
        For Each message In conversation.Messages.Where(Function(x) x IsNot Nothing)
            Dim normalizedRole = If(message.Role, "").Trim().ToLowerInvariant()
            If Not String.Equals(message.Role, normalizedRole, StringComparison.Ordinal) Then
                message.Role = normalizedRole
                changed = True
            End If
            If String.IsNullOrWhiteSpace(message.Id) Then
                message.Id = Guid.NewGuid().ToString("N")
                changed = True
            End If
            If String.Equals(message.Role, "assistant", StringComparison.OrdinalIgnoreCase) AndAlso
               message.ToolCalls IsNot Nothing AndAlso message.ToolCalls.Count > 0 AndAlso
               Not String.Equals(message.Name, AgentConversationSchema.ActivityMessageName, StringComparison.OrdinalIgnoreCase) Then
                message.Name = AgentConversationSchema.ActivityMessageName
                changed = True
            End If
        Next
    End Sub

    Private Shared Sub UpgradeTurns(conversation As AgentConversationData, ByRef changed As Boolean)
        Dim userMessageIds = conversation.Messages.
            Where(Function(x) x IsNot Nothing AndAlso
                String.Equals(x.Role, "user", StringComparison.OrdinalIgnoreCase) AndAlso
                Not String.Equals(x.Name, AgentConversationSchema.SteeringMessageName, StringComparison.OrdinalIgnoreCase)).
            Select(Function(x) x.Id).
            ToHashSet(StringComparer.OrdinalIgnoreCase)
        Dim existingTurnsValid = conversation.Turns IsNot Nothing AndAlso
            conversation.Turns.Count > 0 AndAlso
            conversation.Turns.All(Function(x) x IsNot Nothing AndAlso userMessageIds.Contains(x.UserMessageId))
        If existingTurnsValid Then
            For Each turn In conversation.Turns
                If String.IsNullOrWhiteSpace(turn.Id) Then
                    turn.Id = Guid.NewGuid().ToString("N")
                    changed = True
                End If
                If turn.Activities Is Nothing Then
                    turn.Activities = New List(Of AgentTurnActivityData)
                    changed = True
                End If
                For Each activity In turn.Activities.Where(Function(x) x IsNot Nothing)
                    If String.IsNullOrWhiteSpace(activity.Id) Then
                        activity.Id = Guid.NewGuid().ToString("N")
                        changed = True
                    End If
                Next
            Next
            Return
        End If
        conversation.Turns = New List(Of AgentTurnData)
        Dim currentTurn As AgentTurnData = Nothing
        Dim toolActivities As New Dictionary(Of String, AgentTurnActivityData)(StringComparer.OrdinalIgnoreCase)

        For Each message In conversation.Messages.Where(Function(x) x IsNot Nothing)
            Select Case If(message.Role, "").ToLowerInvariant()
                Case "user"
                    If String.Equals(message.Name, AgentConversationSchema.SteeringMessageName, StringComparison.OrdinalIgnoreCase) AndAlso currentTurn IsNot Nothing Then
                        currentTurn.Activities.Add(New AgentTurnActivityData With {
                            .Kind = "guidance",
                            .Title = "调整方向",
                            .Content = message.Content,
                            .State = "completed",
                            .CreatedAt = message.CreatedAt
                        })
                        Continue For
                    End If
                    currentTurn = New AgentTurnData With {
                        .UserMessageId = message.Id,
                        .StartedAt = message.CreatedAt,
                        .CompletedAt = message.CreatedAt,
                        .State = "completed",
                        .StatusText = "历史记录已升级"
                    }
                    conversation.Turns.Add(currentTurn)
                    toolActivities.Clear()
                Case "assistant"
                    If currentTurn Is Nothing Then Continue For
                    If message.ToolCalls IsNot Nothing AndAlso message.ToolCalls.Count > 0 Then
                        If Not String.IsNullOrWhiteSpace(message.Content) Then AddAssistantActivity(currentTurn, message)
                        For Each callInfo In message.ToolCalls.Where(Function(x) x IsNot Nothing)
                            Dim activity As New AgentTurnActivityData With {
                                .Kind = "tool",
                                .Title = If(callInfo.Name, ""),
                                .ToolName = If(callInfo.Name, ""),
                                .Arguments = If(callInfo.Arguments, ""),
                                .State = "completed",
                                .CreatedAt = message.CreatedAt
                            }
                            currentTurn.Activities.Add(activity)
                            If Not String.IsNullOrWhiteSpace(callInfo.Id) Then toolActivities(callInfo.Id) = activity
                        Next
                    ElseIf String.Equals(message.Name, AgentConversationSchema.ActivityMessageName, StringComparison.OrdinalIgnoreCase) AndAlso
                           Not String.IsNullOrWhiteSpace(message.Content) Then
                        AddAssistantActivity(currentTurn, message)
                    Else
                        currentTurn.CompletedAt = message.CreatedAt
                    End If
                Case "tool"
                    If currentTurn Is Nothing Then Continue For
                    Dim activity As AgentTurnActivityData = Nothing
                    If Not String.IsNullOrWhiteSpace(message.ToolCallId) Then toolActivities.TryGetValue(message.ToolCallId, activity)
                    If activity Is Nothing Then
                        activity = New AgentTurnActivityData With {
                            .Kind = "tool",
                            .Title = If(message.Name, "工具调用"),
                            .ToolName = If(message.Name, ""),
                            .State = "completed",
                            .CreatedAt = message.CreatedAt
                        }
                        currentTurn.Activities.Add(activity)
                    End If
                    activity.ResultText = If(message.Content, "")
                    activity.State = "completed"
                    currentTurn.CompletedAt = message.CreatedAt
            End Select
        Next

        For Each turn In conversation.Turns
            If turn.CompletedAt < turn.StartedAt Then turn.CompletedAt = turn.StartedAt
        Next
        changed = True
    End Sub

    Private Shared Sub AddAssistantActivity(turn As AgentTurnData, message As AgentMessageData)
        turn.Activities.Add(New AgentTurnActivityData With {
            .Kind = "assistant",
            .Content = message.Content,
            .State = "completed",
            .CreatedAt = message.CreatedAt
        })
    End Sub

    Private Shared Function BuildLegacyCheckpoint(root As JsonObject,
                                                  conversation As AgentConversationData) As AgentContextCheckpointData
        Dim summary = GetString(root, "ContextSummary").Trim()
        Dim coveredCount = GetInteger(root, "ContextSummaryMessageCount", 0)
        If summary = "" OrElse coveredCount <= 0 Then Return Nothing

        Dim requestMessages = conversation.Messages.
            Where(Function(x) x IsNot Nothing AndAlso Not String.Equals(If(x.Role, ""), "card", StringComparison.OrdinalIgnoreCase)).
            ToList()
        Dim boundaryIndex = Math.Min(coveredCount, requestMessages.Count) - 1
        While boundaryIndex >= 0 AndAlso boundaryIndex + 1 < requestMessages.Count AndAlso
              String.Equals(If(requestMessages(boundaryIndex + 1).Role, ""), "tool", StringComparison.OrdinalIgnoreCase)
            boundaryIndex -= 1
        End While
        If boundaryIndex < 0 Then Return Nothing

        Dim updatedAt = GetDateTime(root, "ContextSummaryUpdatedAt", DateTime.Now)
        Return New AgentContextCheckpointData With {
            .ConversationId = conversation.Id,
            .Summary = summary,
            .CoveredThroughMessageId = requestMessages(boundaryIndex).Id,
            .CoveredMessageCountSnapshot = boundaryIndex + 1,
            .ModelId = GetString(root, "ContextSummaryModelId"),
            .CreatedAt = updatedAt,
            .UpdatedAt = updatedAt
        }
    End Function

    Private Shared Function GetString(root As JsonObject, propertyName As String) As String
        Try
            Return If(root(propertyName)?.GetValue(Of String)(), "")
        Catch
            Return ""
        End Try
    End Function

    Private Shared Function GetInteger(root As JsonObject, propertyName As String, fallback As Integer) As Integer
        Try
            Return root(propertyName).GetValue(Of Integer)()
        Catch
            Return fallback
        End Try
    End Function

    Private Shared Function GetDateTime(root As JsonObject, propertyName As String, fallback As DateTime) As DateTime
        Try
            Return root(propertyName).GetValue(Of DateTime)()
        Catch
            Return fallback
        End Try
    End Function
End Class
