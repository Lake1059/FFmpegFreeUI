Imports System.IO
Imports System.Text
Imports System.Text.Json

Public Class AgentConversationStore
    Private ReadOnly _storeDirectory As String
    Private ReadOnly _conversationDirectory As String
    Private ReadOnly _contextDirectory As String
    Private ReadOnly _indexPath As String
    Private _requiresUpgradeSave As Boolean
    Private _allowOrphanCleanup As Boolean = True

    Public Property Conversations As New List(Of AgentConversationData)

    Private Sub New(storeDirectory As String)
        _storeDirectory = If(String.IsNullOrWhiteSpace(storeDirectory),
                             Path.Combine(Application.StartupPath, "Agent"),
                             Path.GetFullPath(storeDirectory))
        _conversationDirectory = Path.Combine(_storeDirectory, "Conversations")
        _contextDirectory = Path.Combine(_storeDirectory, "Contexts")
        _indexPath = Path.Combine(_storeDirectory, "Conversations.index.json")
    End Sub

    Public Shared Function Load(Optional storeDirectory As String = Nothing) As AgentConversationStore
        Dim store As New AgentConversationStore(storeDirectory)
        Try
            Directory.CreateDirectory(store._storeDirectory)
            Directory.CreateDirectory(store._conversationDirectory)
            Directory.CreateDirectory(store._contextDirectory)

            If IO.File.Exists(store._indexPath) Then
                Try
                    store.Conversations = store.LoadFromIndex()
                Catch
                    store._allowOrphanCleanup = False
                    store.Conversations = New List(Of AgentConversationData)
                End Try
            End If

            store.MergeDiscoveredConversations(store.LoadFromConversationDirectory())

            store.NormalizeConversations()
            store.RecoverInterruptedRuns()
            store.LoadContextCheckpoints()
            If store._requiresUpgradeSave Then store.Save()
        Catch
            store._allowOrphanCleanup = False
        End Try
        Return store
    End Function

    Public Sub Save()
        Directory.CreateDirectory(_storeDirectory)
        Directory.CreateDirectory(_conversationDirectory)
        Directory.CreateDirectory(_contextDirectory)

        NormalizeConversations()

        Dim indexFile As New AgentConversationIndexFile
        Dim activeConversationFiles As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
        Dim activeContextFiles As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)

        For Each conversation In Conversations
            Dim fileName = GetConversationFileName(conversation)
            Dim filePath = Path.Combine(_conversationDirectory, fileName)
            Agent通用工具_v6.WriteJsonAtomically(filePath, conversation)
            activeConversationFiles.Add(fileName)

            Dim checkpoint = NormalizeCheckpoint(conversation)
            If checkpoint IsNot Nothing Then
                Dim contextFileName = GetContextFileName(conversation.Id)
                Agent通用工具_v6.WriteJsonAtomically(Path.Combine(_contextDirectory, contextFileName), checkpoint)
                activeContextFiles.Add(contextFileName)
            End If

            indexFile.Items.Add(New AgentConversationIndexItem With {
                .Id = conversation.Id,
                .Title = conversation.Title,
                .CreatedAt = conversation.CreatedAt,
                .UpdatedAt = conversation.UpdatedAt,
                .SortOrder = conversation.SortOrder,
                .FileName = fileName
            })
        Next

        Agent通用工具_v6.WriteJsonAtomically(_indexPath, indexFile)
        If _allowOrphanCleanup Then
            RemoveOrphanFiles(_conversationDirectory, "*.json", activeConversationFiles)
            RemoveOrphanFiles(_contextDirectory, "*.context.json", activeContextFiles)
        End If
        _requiresUpgradeSave = False
    End Sub

    Public Function EnsureConversation() As AgentConversationData
        If Conversations.Count = 0 Then
            Dim c As New AgentConversationData
            Conversations.Add(c)
            Save()
        End If
        Return Conversations(0)
    End Function

    Private Function LoadFromIndex() As List(Of AgentConversationData)
        Dim result As New List(Of AgentConversationData)
        Dim indexFile = JsonSerializer.Deserialize(Of AgentConversationIndexFile)(IO.File.ReadAllText(_indexPath, Encoding.UTF8), JsonSO)
        Dim items = If(indexFile?.Items, New List(Of AgentConversationIndexItem))

        For Each item In items.Where(Function(x) x IsNot Nothing).
            OrderBy(Function(x) If(x.SortOrder <= 0, Integer.MaxValue, x.SortOrder)).
            ThenByDescending(Function(x) x.UpdatedAt)

            Dim fileName = If(item.FileName, "").Trim()
            If fileName = "" Then fileName = SafeFileName(item.Id) & ".json"

            If Path.GetFileName(fileName) <> fileName OrElse Path.IsPathRooted(fileName) Then
                _allowOrphanCleanup = False
                Continue For
            End If
            Dim conversation = TryLoadConversation(Path.Combine(_conversationDirectory, fileName))
            If conversation Is Nothing Then Continue For

            If String.IsNullOrWhiteSpace(conversation.Id) Then conversation.Id = item.Id
            If conversation.SortOrder <= 0 Then conversation.SortOrder = item.SortOrder
            If String.IsNullOrWhiteSpace(conversation.Title) Then conversation.Title = item.Title
            result.Add(conversation)
        Next

        Return result
    End Function

    Private Function LoadFromConversationDirectory() As List(Of AgentConversationData)
        Dim result As New List(Of AgentConversationData)
        If Not Directory.Exists(_conversationDirectory) Then Return result

        For Each filePath In Directory.EnumerateFiles(_conversationDirectory, "*.json")
            Dim conversation = TryLoadConversation(filePath)
            If conversation IsNot Nothing Then result.Add(conversation)
        Next

        Return result
    End Function

    Private Sub MergeDiscoveredConversations(discovered As IEnumerable(Of AgentConversationData))
        If Conversations Is Nothing Then Conversations = New List(Of AgentConversationData)
        If discovered Is Nothing Then Return

        Dim knownIds As New HashSet(Of String)(
            Conversations.
                Where(Function(x) x IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(x.Id)).
                Select(Function(x) x.Id),
            StringComparer.OrdinalIgnoreCase)
        For Each conversation In discovered
            If conversation Is Nothing Then Continue For
            If Not String.IsNullOrWhiteSpace(conversation.Id) AndAlso knownIds.Contains(conversation.Id) Then Continue For
            Conversations.Add(conversation)
            If Not String.IsNullOrWhiteSpace(conversation.Id) Then knownIds.Add(conversation.Id)
        Next
    End Sub

    Private Sub LoadContextCheckpoints()
        For Each conversation In Conversations
            Dim filePath = Path.Combine(_contextDirectory, GetContextFileName(conversation.Id))
            Dim storedCheckpoint = TryLoadCheckpoint(filePath)
            If storedCheckpoint IsNot Nothing Then conversation.ContextCheckpoint = storedCheckpoint
            NormalizeCheckpoint(conversation)
        Next
    End Sub

    Private Function TryLoadConversation(filePath As String) As AgentConversationData
        Try
            If Not IO.File.Exists(filePath) Then
                _allowOrphanCleanup = False
                Return Nothing
            End If
            Dim result = AgentConversationJsonUpgrader.Upgrade(IO.File.ReadAllText(filePath, Encoding.UTF8))
            If result Is Nothing Then
                _allowOrphanCleanup = False
                Return Nothing
            End If
            _requiresUpgradeSave = _requiresUpgradeSave OrElse result.WasUpgraded OrElse result.LegacyCheckpoint IsNot Nothing
            result.Conversation.ContextCheckpoint = result.LegacyCheckpoint
            Return result.Conversation
        Catch
            _allowOrphanCleanup = False
            Return Nothing
        End Try
    End Function

    Private Function TryLoadCheckpoint(filePath As String) As AgentContextCheckpointData
        Try
            If Not IO.File.Exists(filePath) Then Return Nothing
            Return JsonSerializer.Deserialize(Of AgentContextCheckpointData)(IO.File.ReadAllText(filePath, Encoding.UTF8), JsonSO)
        Catch
            _allowOrphanCleanup = False
            Return Nothing
        End Try
    End Function

    Private Sub NormalizeConversations()
        If Conversations Is Nothing Then Conversations = New List(Of AgentConversationData)

        Conversations = Conversations.
            Where(Function(x) x IsNot Nothing).
            OrderBy(Function(x) If(x.SortOrder <= 0, Integer.MaxValue, x.SortOrder)).
            ThenByDescending(Function(x) x.UpdatedAt).
            ToList()

        Dim seenIds As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
        For i = 0 To Conversations.Count - 1
            Dim conversation = Conversations(i)
            If String.IsNullOrWhiteSpace(conversation.Id) OrElse seenIds.Contains(conversation.Id) Then
                conversation.Id = Guid.NewGuid().ToString("N")
            End If
            seenIds.Add(conversation.Id)
            conversation.Version = AgentConversationSchema.LatestVersion
            conversation.SortOrder = i + 1
            conversation.DraftText = If(conversation.DraftText, "")
            conversation.DraftPaths = If(conversation.DraftPaths, New List(Of String)).Where(Function(x) Not String.IsNullOrWhiteSpace(x)).Distinct(StringComparer.OrdinalIgnoreCase).ToList()
            conversation.Messages = If(conversation.Messages, New List(Of AgentMessageData)).Where(Function(x) x IsNot Nothing).ToList()
            Dim seenMessageIds As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
            For Each message In conversation.Messages
                message.ToolCalls = If(message.ToolCalls, New List(Of AgentToolCallInfo)).Where(Function(x) x IsNot Nothing).ToList()
                If String.IsNullOrWhiteSpace(message.Id) OrElse seenMessageIds.Contains(message.Id) Then
                    message.Id = Guid.NewGuid().ToString("N")
                End If
                seenMessageIds.Add(message.Id)
            Next
            conversation.Turns = If(conversation.Turns, New List(Of AgentTurnData)).Where(Function(x) x IsNot Nothing).ToList()
            For Each turn In conversation.Turns
                If String.IsNullOrWhiteSpace(turn.Id) Then turn.Id = Guid.NewGuid().ToString("N")
                turn.Activities = If(turn.Activities, New List(Of AgentTurnActivityData)).Where(Function(x) x IsNot Nothing).ToList()
            Next
            If conversation.Usage Is Nothing Then conversation.Usage = New AgentUsageInfo
        Next
    End Sub

    ' 仅在加载时恢复；保存其他对话时绝不能改动仍在执行的任务状态。
    Private Sub RecoverInterruptedRuns()
        For Each conversation In Conversations
            For Each turn In conversation.Turns.Where(Function(x) x.State = "running")
                turn.State = "canceled"
                turn.CompletedAt = DateTime.Now
                turn.StatusText = "上次运行因应用退出中断"
                For Each activity In turn.Activities.Where(Function(x) x.State = "running")
                    activity.State = "canceled"
                    If activity.Kind = "tool" AndAlso activity.ResultText = "" Then activity.ResultText = turn.StatusText
                Next
                _requiresUpgradeSave = True
            Next
            ' 修复崩溃后已记录调用但没有结果的协议对，防止续聊请求被端点拒绝。
            For i = conversation.Messages.Count - 1 To 0 Step -1
                Dim message = conversation.Messages(i)
                If message.Role <> "assistant" OrElse message.ToolCalls.Count = 0 Then Continue For
                Dim insertIndex = i + 1
                Dim completed As New HashSet(Of String)(StringComparer.Ordinal)
                While insertIndex < conversation.Messages.Count AndAlso conversation.Messages(insertIndex).Role = "tool"
                    completed.Add(conversation.Messages(insertIndex).ToolCallId)
                    insertIndex += 1
                End While
                For Each tool In message.ToolCalls
                    If completed.Contains(tool.Id) Then Continue For
                    conversation.Messages.Insert(insertIndex, New AgentMessageData With {
                        .Role = "tool", .Name = tool.Name, .ToolCallId = tool.Id,
                        .Content = "上次运行中断，工具结果未知；请先核实实际状态，不要假设操作未执行。"
                    })
                    insertIndex += 1
                    _requiresUpgradeSave = True
                Next
            Next
        Next
    End Sub

    Private Shared Function NormalizeCheckpoint(conversation As AgentConversationData) As AgentContextCheckpointData
        Dim checkpoint = conversation?.ContextCheckpoint
        If checkpoint Is Nothing Then Return Nothing

        checkpoint.ConversationId = If(conversation.Id, "")
        checkpoint.Summary = If(checkpoint.Summary, "").Trim()
        checkpoint.CoveredThroughMessageId = If(checkpoint.CoveredThroughMessageId, "").Trim()
        Dim requestMessages = If(conversation.Messages, New List(Of AgentMessageData)).
            Where(Function(x) x IsNot Nothing AndAlso Not String.Equals(If(x.Role, ""), "card", StringComparison.OrdinalIgnoreCase)).
            ToList()
        Dim boundaryIndex = AgentContextProjection.ResolveBoundaryIndex(requestMessages, checkpoint)
        If checkpoint.Summary = "" OrElse checkpoint.CoveredThroughMessageId = "" OrElse boundaryIndex < 0 Then
            conversation.ContextCheckpoint = Nothing
            Return Nothing
        End If

        checkpoint.Version = 1
        checkpoint.CoveredMessageCountSnapshot = boundaryIndex + 1
        If checkpoint.CreatedAt = DateTime.MinValue Then checkpoint.CreatedAt = checkpoint.UpdatedAt
        If checkpoint.CreatedAt = DateTime.MinValue Then checkpoint.CreatedAt = DateTime.Now
        If checkpoint.UpdatedAt = DateTime.MinValue Then checkpoint.UpdatedAt = checkpoint.CreatedAt
        Return checkpoint
    End Function

    Private Shared Function GetConversationFileName(conversation As AgentConversationData) As String
        Return SafeFileName(conversation.Id) & ".json"
    End Function

    Private Shared Function GetContextFileName(conversationId As String) As String
        Return SafeFileName(conversationId) & ".context.json"
    End Function

    Private Shared Function SafeFileName(value As String) As String
        Dim name = If(value, "").Trim()
        If name = "" Then name = Guid.NewGuid().ToString("N")

        If name.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0 OrElse name.EndsWith(".", StringComparison.Ordinal) OrElse name <> value Then
            Return "conversation-" & Convert.ToHexString(System.Security.Cryptography.SHA256.HashData(Encoding.UTF8.GetBytes(If(value, ""))))
        End If

        Return name
    End Function

    Private Shared Sub RemoveOrphanFiles(directoryPath As String,
                                         searchPattern As String,
                                         activeFiles As HashSet(Of String))
        If Not Directory.Exists(directoryPath) Then Return

        For Each filePath In Directory.EnumerateFiles(directoryPath, searchPattern)
            Dim fileName = Path.GetFileName(filePath)
            If activeFiles.Contains(fileName) Then Continue For

            Try
                IO.File.Delete(filePath)
            Catch
            End Try
        Next
    End Sub
End Class
