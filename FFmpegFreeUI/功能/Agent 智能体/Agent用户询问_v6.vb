Imports System.Text
Imports System.Text.Json
Imports LakeUI

' 只管理询问窗口；问题保存在会话中，等待期间没有运行任务、计时器或异步等待。
Friend NotInheritable Class AgentUserInputCoordinator
    Implements IDisposable

    Private ReadOnly _owner As Control
    Private ReadOnly _selectedConversation As Func(Of AgentConversationData)
    Private ReadOnly _answered As Action(Of AgentConversationData, AgentPendingQuestionData, String, Boolean)
    Private _dialog As Form
    Private _displayed As AgentPendingQuestionData
    Private _hiddenQuestion As AgentPendingQuestionData
    Private _disposed As Boolean

    Public Sub New(owner As Control, selectedConversation As Func(Of AgentConversationData),
                   answered As Action(Of AgentConversationData, AgentPendingQuestionData, String, Boolean))
        _owner = owner
        _selectedConversation = selectedConversation
        _answered = answered
        AddHandler owner.VisibleChanged, AddressOf OwnerContextChanged
        AddHandler owner.HandleCreated, AddressOf OwnerContextChanged
        AddHandler owner.Disposed, AddressOf OwnerDisposed
    End Sub

    Public Shared Function CreateQuestion(callInfo As AgentToolCallInfo, turnId As String,
                                           ByRef errorResult As String) As AgentPendingQuestionData
        errorResult = ""
        Try
            Dim args = Agent通用工具_v6.ParseJsonArguments(callInfo.Arguments)
            Dim question As New AgentPendingQuestionData With {
                .ToolCallId = callInfo.Id, .TurnId = turnId,
                .Question = Agent通用工具_v6.GetJsonString(args, "question").Trim()
            }
            If question.Question = "" Then Throw New FormatException("question 不能为空")
            Dim options As JsonElement
            If args.ValueKind <> JsonValueKind.Object OrElse Not args.TryGetProperty("options", options) OrElse
               options.ValueKind <> JsonValueKind.Array OrElse options.GetArrayLength() < 2 Then
                Throw New FormatException("options 至少包含两个选项")
            End If
            For Each value In options.EnumerateArray()
                Dim item As New AgentQuestionOptionData With {
                    .Id = Agent通用工具_v6.GetJsonString(value, "id").Trim(),
                    .Label = Agent通用工具_v6.GetJsonString(value, "label").Trim(),
                    .Description = Agent通用工具_v6.GetJsonString(value, "description").Trim()
                }
                If item.Id = "" OrElse item.Label = "" OrElse item.Label = "稍后选择" OrElse item.Label = "取消选择" OrElse
                   item.Label.Contains(vbCr) OrElse item.Label.Contains(vbLf) OrElse
                   question.Options.Any(Function(existing) existing.Id = item.Id OrElse existing.Label = item.Label) Then
                    Throw New FormatException("选项 id、label 须非空且唯一；label 须为单行文本，不能使用保留按钮名‘稍后选择’或‘取消选择’")
                End If
                question.Options.Add(item)
            Next
            Return question
        Catch ex As Exception When TypeOf ex Is JsonException OrElse TypeOf ex Is FormatException
            errorResult = JsonSerializer.Serialize(New With {.status = "invalid_arguments", .reason = ex.Message})
            Return Nothing
        End Try
    End Function

    Public Sub ShowCurrentQuestion()
        _hiddenQuestion = Nothing
        NotifyContextChanged()
    End Sub

    Public Sub NotifyContextChanged()
        If _disposed Then Return
        Dim conversation = _selectedConversation()
        Dim question = conversation?.PendingQuestion
        If question Is Nothing Then _hiddenQuestion = Nothing
        If _dialog IsNot Nothing AndAlso (Not ReferenceEquals(_displayed, question) OrElse Not _owner.Visible) Then CloseDialog()
        If _dialog IsNot Nothing OrElse question Is Nothing OrElse ReferenceEquals(question, _hiddenQuestion) OrElse
           Not _owner.Visible OrElse Not _owner.IsHandleCreated OrElse _owner.IsDisposed Then Return

        Dim prompt As New StringBuilder()
        prompt.AppendLine("会话：" & conversation.Title).AppendLine().AppendLine(question.Question).AppendLine()
        Dim buttons As New List(Of ExMsgBoxButton)
        For Each choice In question.Options
            prompt.AppendLine(choice.Label & If(choice.Description = "", "", "：" & choice.Description))
            buttons.Add(New ExMsgBoxButton(choice.Label, False,
                Function(e)
                    QueueAnswer(conversation, question, JsonSerializer.Serialize(New With {
                        .status = "selected", .conversation_id = conversation.Id, .option_id = choice.Id, .label = choice.Label
                    }), True)
                    Return True
                End Function))
        Next
        prompt.AppendLine().Append("推理已暂停，问题已保存且不会超时。稍后选择或关闭窗口可保留问题；取消选择后可直接补充文字。")
        buttons.Add(New ExMsgBoxButton("稍后选择", False,
            Function(e)
                _hiddenQuestion = question
                Return True
            End Function))
        buttons.Add(New ExMsgBoxButton("取消选择", True,
            Function(e)
                QueueAnswer(conversation, question, "{""status"":""unanswered"",""reason"":""dismissed""}", False)
                Return True
            End Function))
        Try
            _displayed = question
            _dialog = CreateExMsgBox(prompt.ToString(), buttons, "Agent 需要你的选择", _owner.FindForm(), MsgBoxStyle.Question)
            AddHandler _dialog.FormClosed, AddressOf DialogClosed
            _dialog.Show(_owner.FindForm())
        Catch
            _hiddenQuestion = question
            CloseDialog()
        End Try
    End Sub

    Private Sub QueueAnswer(conversation As AgentConversationData, question As AgentPendingQuestionData,
                            result As String, shouldResume As Boolean)
        If _disposed OrElse _owner.IsDisposed OrElse Not _owner.IsHandleCreated Then Return
        _hiddenQuestion = question
        ' 等按钮回调退出、窗口关闭后再恢复推理，避免旧窗口事件干扰新问题。
        _owner.BeginInvoke(Sub()
                               If Not _disposed AndAlso Not _owner.IsDisposed Then _answered(conversation, question, result, shouldResume)
                           End Sub)
    End Sub

    Private Sub DialogClosed(sender As Object, e As FormClosedEventArgs)
        If Not ReferenceEquals(sender, _dialog) Then Return
        _hiddenQuestion = _displayed
        _dialog = Nothing
        _displayed = Nothing
    End Sub

    Private Sub CloseDialog()
        Dim dialog = _dialog
        _dialog = Nothing
        _displayed = Nothing
        If dialog Is Nothing Then Return
        RemoveHandler dialog.FormClosed, AddressOf DialogClosed
        dialog.Close()
        dialog.Dispose()
    End Sub

    Private Sub OwnerContextChanged(sender As Object, e As EventArgs)
        NotifyContextChanged()
    End Sub

    Private Sub OwnerDisposed(sender As Object, e As EventArgs)
        Dispose()
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        If _disposed Then Return
        _disposed = True
        CloseDialog()
        RemoveHandler _owner.VisibleChanged, AddressOf OwnerContextChanged
        RemoveHandler _owner.HandleCreated, AddressOf OwnerContextChanged
        RemoveHandler _owner.Disposed, AddressOf OwnerDisposed
    End Sub
End Class

Partial Friend Class Agent页面控制器_v6
    Private _userInputCoordinator As AgentUserInputCoordinator

    Private Sub ShowPendingQuestion(Optional reopen As Boolean = False)
        If _closing OrElse _view.IsDisposed Then Return
        If _userInputCoordinator Is Nothing Then
            _userInputCoordinator = New AgentUserInputCoordinator(_view,
                Function() If(_closing OrElse GetConversationRuntime(_current).Busy, Nothing, _current),
                AddressOf AnswerPendingQuestion)
        End If
        If reopen Then
            _userInputCoordinator.ShowCurrentQuestion()
        Else
            _userInputCoordinator.NotifyContextChanged()
        End If
    End Sub

    Private Async Sub AnswerPendingQuestion(conversation As AgentConversationData, question As AgentPendingQuestionData,
                                             result As String, shouldResume As Boolean)
        If _closing OrElse _view.IsDisposed OrElse Not ReferenceEquals(conversation.PendingQuestion, question) OrElse
           GetConversationRuntime(conversation).Busy OrElse _store Is Nothing OrElse Not _store.Conversations.Contains(conversation) Then Return
        ResolvePendingQuestion(conversation, result)
        If shouldResume Then
            If IsConversationSelected(conversation) Then ApplyConversationSnapshot()
            Await StartAgentRunAsync(conversation)
        ElseIf IsConversationSelected(conversation) Then
            ShowStatus("已取消选择，可在输入框补充说明后发送。")
            _view.ModernTextBox1.Focus()
        End If
    End Sub

    Private Sub ResolvePendingQuestion(conversation As AgentConversationData, result As String)
        Dim question = conversation?.PendingQuestion
        If question Is Nothing Then Return
        Dim index = conversation.Messages.FindLastIndex(Function(message) message.ToolCalls IsNot Nothing AndAlso
            message.ToolCalls.Any(Function(callInfo) callInfo.Id = question.ToolCallId))
        If index >= 0 Then
            index += 1
            Dim resultIndex = index
            While index < conversation.Messages.Count AndAlso conversation.Messages(index).Role = "tool"
                index += 1
            End While
            ' 答案紧跟询问所在调用批次，且不会因重复 UI 回调追加第二份结果。
            Dim existingResult = conversation.Messages.Skip(resultIndex).Take(index - resultIndex).
                FirstOrDefault(Function(message) message.ToolCallId = question.ToolCallId)
            If existingResult IsNot Nothing Then
                existingResult.Content = result
            Else
                conversation.Messages.Insert(index, New AgentMessageData With {
                    .Role = "tool", .Name = "ask_user", .ToolCallId = question.ToolCallId, .Content = result
                })
            End If
        End If
        Dim turn = conversation.Turns.FirstOrDefault(Function(item) item.Id = question.TurnId)
        Dim activity = turn?.Activities.FirstOrDefault(Function(item) item.ToolCallId = question.ToolCallId)
        If activity IsNot Nothing Then
            activity.State = "completed"
            activity.ResultText = result
        End If
        If turn IsNot Nothing Then
            turn.State = If(GetConversationRuntime(conversation).Busy, "running", "completed")
            turn.StatusText = "用户已回应询问"
        End If
        conversation.PendingQuestion = Nothing
        SaveConversation(conversation)
        _userInputCoordinator?.NotifyContextChanged()
        If IsConversationSelected(conversation) AndAlso Not GetConversationRuntime(conversation).Busy Then RenderCurrentConversation()
    End Sub
End Class
