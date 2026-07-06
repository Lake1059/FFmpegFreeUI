Imports System.IO
Imports System.Text
Imports System.Text.Json

Public Class AgentConversationStore
    Private Shared ReadOnly StoreDirectory As String = Path.Combine(Application.StartupPath, "Agent")
    Private Shared ReadOnly ConversationDirectory As String = Path.Combine(StoreDirectory, "Conversations")
    Private Shared ReadOnly IndexPath As String = Path.Combine(StoreDirectory, "Conversations.index.json")

    Public Property Conversations As New List(Of AgentConversationData)

    Public Shared Function Load() As AgentConversationStore
        Dim store As New AgentConversationStore
        Try
            Directory.CreateDirectory(StoreDirectory)
            Directory.CreateDirectory(ConversationDirectory)

            If IO.File.Exists(IndexPath) Then
                store.Conversations = LoadFromIndex()
                If store.Conversations.Count = 0 Then store.Conversations = LoadFromConversationDirectory()
            Else
                store.Conversations = LoadFromConversationDirectory()
            End If

            store.NormalizeConversations()
        Catch
        End Try
        Return store
    End Function

    Public Sub Save()
        Directory.CreateDirectory(StoreDirectory)
        Directory.CreateDirectory(ConversationDirectory)

        NormalizeConversations()

        Dim indexFile As New AgentConversationIndexFile
        Dim activeFiles As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)

        For Each conversation In Conversations
            Dim fileName = GetConversationFileName(conversation)
            Dim filePath = Path.Combine(ConversationDirectory, fileName)
            IO.File.WriteAllText(filePath, JsonSerializer.Serialize(conversation, JsonSO), Encoding.UTF8)
            activeFiles.Add(fileName)

            indexFile.Items.Add(New AgentConversationIndexItem With {
                .Id = conversation.Id,
                .Title = conversation.Title,
                .CreatedAt = conversation.CreatedAt,
                .UpdatedAt = conversation.UpdatedAt,
                .SortOrder = conversation.SortOrder,
                .FileName = fileName
            })
        Next

        IO.File.WriteAllText(IndexPath, JsonSerializer.Serialize(indexFile, JsonSO), Encoding.UTF8)
        RemoveOrphanConversationFiles(activeFiles)
    End Sub

    Public Function EnsureConversation() As AgentConversationData
        If Conversations.Count = 0 Then
            Dim c As New AgentConversationData
            Conversations.Add(c)
            Save()
        End If
        Return Conversations(0)
    End Function

    Private Shared Function LoadFromIndex() As List(Of AgentConversationData)
        Dim result As New List(Of AgentConversationData)
        Dim indexFile = JsonSerializer.Deserialize(Of AgentConversationIndexFile)(IO.File.ReadAllText(IndexPath, Encoding.UTF8), JsonSO)
        Dim items = If(indexFile?.Items, New List(Of AgentConversationIndexItem))

        For Each item In items.
            OrderBy(Function(x) If(x.SortOrder <= 0, Integer.MaxValue, x.SortOrder)).
            ThenByDescending(Function(x) x.UpdatedAt)

            Dim fileName = If(item.FileName, "").Trim()
            If fileName = "" Then fileName = SafeFileName(item.Id) & ".json"

            Dim conversation = TryLoadConversation(Path.Combine(ConversationDirectory, fileName))
            If conversation Is Nothing Then Continue For

            If String.IsNullOrWhiteSpace(conversation.Id) Then conversation.Id = item.Id
            If conversation.SortOrder <= 0 Then conversation.SortOrder = item.SortOrder
            If String.IsNullOrWhiteSpace(conversation.Title) Then conversation.Title = item.Title
            result.Add(conversation)
        Next

        Return result
    End Function

    Private Shared Function LoadFromConversationDirectory() As List(Of AgentConversationData)
        Dim result As New List(Of AgentConversationData)
        If Not Directory.Exists(ConversationDirectory) Then Return result

        For Each filePath In Directory.EnumerateFiles(ConversationDirectory, "*.json")
            Dim conversation = TryLoadConversation(filePath)
            If conversation IsNot Nothing Then result.Add(conversation)
        Next

        Return result
    End Function

    Private Shared Function TryLoadConversation(filePath As String) As AgentConversationData
        Try
            If Not IO.File.Exists(filePath) Then Return Nothing
            Return JsonSerializer.Deserialize(Of AgentConversationData)(IO.File.ReadAllText(filePath, Encoding.UTF8), JsonSO)
        Catch
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
            conversation.SortOrder = i + 1
            If conversation.Messages Is Nothing Then conversation.Messages = New List(Of AgentMessageData)
            If conversation.Usage Is Nothing Then conversation.Usage = New AgentUsageInfo
        Next
    End Sub

    Private Shared Function GetConversationFileName(conversation As AgentConversationData) As String
        Return SafeFileName(conversation.Id) & ".json"
    End Function

    Private Shared Function SafeFileName(value As String) As String
        Dim name = If(value, "").Trim()
        If name = "" Then name = Guid.NewGuid().ToString("N")

        For Each invalidChar In Path.GetInvalidFileNameChars()
            name = name.Replace(invalidChar, "_"c)
        Next

        Return name
    End Function

    Private Shared Sub RemoveOrphanConversationFiles(activeFiles As HashSet(Of String))
        If Not Directory.Exists(ConversationDirectory) Then Return

        For Each filePath In Directory.EnumerateFiles(ConversationDirectory, "*.json")
            Dim fileName = Path.GetFileName(filePath)
            If activeFiles.Contains(fileName) Then Continue For

            Try
                IO.File.Delete(filePath)
            Catch
            End Try
        Next
    End Sub
End Class
