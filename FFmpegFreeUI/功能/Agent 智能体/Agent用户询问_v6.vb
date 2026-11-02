Imports System.Text
Imports System.Text.Json
Imports LakeUI

' 所有状态在 UI 线程访问；对话框只属于发起它的会话，网络请求不占用等待期。
Friend NotInheritable Class AgentUserInputCoordinator
    Implements IDisposable

    Private NotInheritable Class PendingQuestion
        Public ConversationId As String
        Public ConversationTitle As String
        Public Question As String
        Public ReadOnly Options As New List(Of (Id As String, Label As String, Description As String))
        Public ReadOnly Completion As New TaskCompletionSource(Of String)(TaskCreationOptions.RunContinuationsAsynchronously)
    End Class

    Private ReadOnly _owner As Control
    Private ReadOnly _selectedConversation As Func(Of String)
    Private ReadOnly _pendingChanged As Action
    Private ReadOnly _pending As New List(Of PendingQuestion)
    Private _dialog As Form
    Private _displayed As PendingQuestion
    Private _suspending As Boolean
    Private _disposed As Boolean

    Public Sub New(owner As Control, selectedConversation As Func(Of String), pendingChanged As Action)
        _owner = owner
        _selectedConversation = selectedConversation
        _pendingChanged = pendingChanged
        AddHandler owner.VisibleChanged, AddressOf OwnerContextChanged
        AddHandler owner.HandleCreated, AddressOf OwnerContextChanged
        AddHandler owner.Disposed, AddressOf OwnerDisposed
    End Sub

    Public Function HasPending(conversationId As String) As Boolean
        Return _pending.Any(Function(question) question.ConversationId = conversationId AndAlso Not question.Completion.Task.IsCompleted)
    End Function

    Public Async Function AskAsync(conversationId As String, conversationTitle As String, args As JsonElement,
                                    cancellationToken As Threading.CancellationToken) As Task(Of String)
        cancellationToken.ThrowIfCancellationRequested()
        If _disposed Then Return Reply("unanswered", "owner_closed")
        Dim question As New PendingQuestion With {
            .ConversationId = conversationId, .ConversationTitle = conversationTitle,
            .Question = Agent通用工具_v6.GetJsonString(args, "question").Trim()
        }
        If question.Question.Length = 0 OrElse question.Question.Length > 600 Then Return Reply("invalid_arguments", "question 必须为 1-600 字符")
        Dim options As JsonElement
        If Not args.TryGetProperty("options", options) OrElse options.ValueKind <> JsonValueKind.Array OrElse options.GetArrayLength() < 2 OrElse options.GetArrayLength() > 3 Then
            Return Reply("invalid_arguments", "options 必须包含 2-3 个选项；详细说明放在正文，按钮仅用短标签")
        End If
        For Each optionValue In options.EnumerateArray()
            Dim id = Agent通用工具_v6.GetJsonString(optionValue, "id").Trim()
            Dim label = Agent通用工具_v6.GetJsonString(optionValue, "label").Trim()
            Dim description = Agent通用工具_v6.GetJsonString(optionValue, "description").Trim()
            If id.Length = 0 OrElse id.Length > 40 OrElse label.Length = 0 OrElse label.Length > 12 OrElse label = "暂不选择" OrElse description.Length > 200 OrElse
               label.Contains(vbCr) OrElse label.Contains(vbLf) OrElse question.Options.Any(Function(value) value.Id = id OrElse value.Label = label) Then
                Return Reply("invalid_arguments", "选项 id 须唯一且为 1-40 字符，label 须唯一且为 1-12 字符单行短文本，description 最多 200 字符")
            End If
            question.Options.Add((id, label, description))
        Next
        Using deadline = Threading.CancellationTokenSource.CreateLinkedTokenSource(cancellationToken)
            deadline.CancelAfter(TimeSpan.FromSeconds(Math.Clamp(Agent通用工具_v6.GetJsonInteger(args, "timeout_seconds", 600), 30, 1800)))
            _pending.Add(question)
            Try
                _pendingChanged?.Invoke()
                NotifyContextChanged()
                Return Await question.Completion.Task.WaitAsync(deadline.Token)
            Catch ex As OperationCanceledException When Not cancellationToken.IsCancellationRequested
                Return Reply("unanswered", "timeout")
            Finally
                _pending.Remove(question)
                If ReferenceEquals(_displayed, question) Then SuspendDialog()
                If Not _disposed Then
                    _pendingChanged?.Invoke()
                    NotifyContextChanged()
                End If
            End Try
        End Using
    End Function

    Public Sub NotifyContextChanged()
        If _disposed Then Return
        Dim selected = If(_selectedConversation(), "")
        If _displayed IsNot Nothing AndAlso (_displayed.ConversationId <> selected OrElse Not _owner.Visible) Then SuspendDialog()
        If _dialog IsNot Nothing OrElse selected = "" OrElse Not _owner.Visible OrElse Not _owner.IsHandleCreated OrElse _owner.IsDisposed Then Return
        Dim question = _pending.FirstOrDefault(Function(value) value.ConversationId = selected AndAlso Not value.Completion.Task.IsCompleted)
        If question Is Nothing Then Return
        Dim prompt As New StringBuilder()
        prompt.AppendLine("会话：" & Agent通用工具_v6.LimitText(question.ConversationTitle, 60)).AppendLine().AppendLine(question.Question).AppendLine()
        Dim buttons As New List(Of ExMsgBoxButton)
        For Each optionValue In question.Options
            Dim choice = optionValue
            prompt.AppendLine(choice.Label & If(choice.Description = "", "", "：" & choice.Description))
            buttons.Add(New ExMsgBoxButton(choice.Label, False,
                Function(e)
                    question.Completion.TrySetResult(JsonSerializer.Serialize(New With {
                        .status = "selected", .conversation_id = question.ConversationId, .option_id = choice.Id, .label = choice.Label
                    }))
                    Return True
                End Function))
        Next
        prompt.AppendLine().Append("关闭或暂不选择不会视为同意；可切换会话，返回后继续选择。")
        buttons.Add(New ExMsgBoxButton("暂不选择", True,
            Function(e)
                question.Completion.TrySetResult(Reply("unanswered", "dismissed"))
                Return True
            End Function))
        Try
            _displayed = question
            _dialog = CreateExMsgBox(prompt.ToString(), buttons, "Agent 需要你的选择", _owner.FindForm(), MsgBoxStyle.Question)
            AddHandler _dialog.FormClosed, AddressOf DialogClosed
            _dialog.Show(_owner.FindForm())
        Catch ex As Exception
            SuspendDialog()
            question.Completion.TrySetResult(Reply("unanswered", "dialog_unavailable"))
        End Try
    End Sub

    Private Sub DialogClosed(sender As Object, e As FormClosedEventArgs)
        If _suspending OrElse Not ReferenceEquals(sender, _dialog) Then Return
        Dim question = _displayed
        _dialog = Nothing
        _displayed = Nothing
        question?.Completion.TrySetResult(Reply("unanswered", "dismissed"))
    End Sub

    Private Sub SuspendDialog()
        Dim dialog = _dialog
        _dialog = Nothing
        _displayed = Nothing
        If dialog Is Nothing Then Return
        _suspending = True
        Try
            RemoveHandler dialog.FormClosed, AddressOf DialogClosed
            dialog.Close()
            dialog.Dispose()
        Finally
            _suspending = False
        End Try
    End Sub

    Private Shared Function Reply(status As String, reason As String) As String
        Return JsonSerializer.Serialize(New With {.status = status, .reason = reason})
    End Function

    Private Sub OwnerContextChanged(sender As Object, e As EventArgs)
        NotifyContextChanged()
    End Sub

    Private Sub OwnerDisposed(sender As Object, e As EventArgs)
        Dispose()
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        If _disposed Then Return
        _disposed = True
        SuspendDialog()
        For Each question In _pending
            question.Completion.TrySetResult(Reply("unanswered", "owner_closed"))
        Next
        _pending.Clear()
        RemoveHandler _owner.VisibleChanged, AddressOf OwnerContextChanged
        RemoveHandler _owner.HandleCreated, AddressOf OwnerContextChanged
        RemoveHandler _owner.Disposed, AddressOf OwnerDisposed
    End Sub
End Class

Partial Friend Class Agent页面控制器_v6
    Private _userInputCoordinator As AgentUserInputCoordinator

    Private Function AskUserAsync(conversation As AgentConversationData, args As JsonElement,
                                  cancellationToken As Threading.CancellationToken) As Task(Of String)
        If _userInputCoordinator Is Nothing Then
            _userInputCoordinator = New AgentUserInputCoordinator(_view,
                Function() If(_closing OrElse _current Is Nothing, "", _current.Id), AddressOf RefreshConversationList)
        End If
        ShowRunStatus(conversation, "等待用户选择")
        SaveConversation(conversation)
        Return _userInputCoordinator.AskAsync(conversation.Id, conversation.Title, args, cancellationToken)
    End Function
End Class
