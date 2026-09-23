Imports System.Text
Imports System.IO
Imports LakeUI

Partial Public Class Form_v6_Agent
    Private _store As AgentConversationStore
    Private _current As AgentConversationData
    Private _orderedConversations As New List(Of AgentConversationData)
    Private _loading As Boolean = False
    Private _activeRunElapsedTimer As System.Windows.Forms.Timer = Nothing
    Private _refreshingConversationList As Boolean
    Private _closing As Boolean

    Private Sub Form_v6_Agent_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _loading = True
        Try
            ModernListBox1.AllowDragReorder = False
            ModernListBox1.TabStop = True
            InitializeSubmittedFileList()
            ApplyAgentButtonPanelLayout()
            If MCB_联网设置.SelectedIndex < 0 Then MCB_联网设置.SelectedIndex = Math.Min(Math.Max(AgentNetworkMode.Normalize(设置_v6.实例对象.Agent联网设置), 0), MCB_联网设置.Items.Count - 1)
            If MCB_权限控制.SelectedIndex < 0 Then MCB_权限控制.SelectedIndex = Math.Min(Math.Max(设置_v6.实例对象.Agent权限级别, 0), MCB_权限控制.Items.Count - 1)

            _orderedConversations = AgentConversationStore.ReadConversationIndex().
                Select(Function(item) New AgentConversationData With {
                    .Id = item.Id, .Title = item.Title, .CreatedAt = item.CreatedAt,
                    .UpdatedAt = item.UpdatedAt, .SortOrder = item.SortOrder
                }).ToList()
            RefreshConversationList()
            UpdateSendButtonState()
            UpdateUsageButton()
        Finally
            _loading = False
        End Try
    End Sub

    Private Sub RefreshConversationList()
        If _closing OrElse IsDisposed Then Return
        If _store IsNot Nothing Then
            NormalizeConversationOrder()
            _orderedConversations = _store.Conversations
        End If
        _orderedConversations = _orderedConversations.
            OrderBy(Function(x) If(x.SortOrder <= 0, Integer.MaxValue, x.SortOrder)).
            ThenByDescending(Function(x) x.UpdatedAt).
            ToList()
        Dim oldLoading = _loading
        _loading = True
        _refreshingConversationList = True
        Try
            ModernListBox1.Items.Clear()
            ModernListBox1.Items.AddRange(_orderedConversations.Select(Function(x) FormatConversationTitle(x)))

            Dim index = _orderedConversations.FindIndex(Function(x) _current IsNot Nothing AndAlso x.Id = _current.Id)
            ModernListBox1.SelectedIndex = index
        Finally
            _loading = oldLoading
            _refreshingConversationList = False
        End Try
    End Sub

    Private Sub EnsureStoreLoaded()
        If _store IsNot Nothing Then Return
        _store = AgentConversationStore.Load()
        ModernListBox1.AllowDragReorder = True
        RefreshConversationList()
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

    Private Sub ModernListBox1_ItemForeColorNeeded(sender As Object, e As ModernListBox.ItemForeColorEventArgs) Handles ModernListBox1.ItemForeColorNeeded
        If e.Index < 0 OrElse e.Index >= _orderedConversations.Count Then Return
        Dim run As ConversationRuntime = Nothing
        If _conversationRuntimes.TryGetValue(_orderedConversations(e.Index).Id, run) AndAlso run.Busy Then
            e.ForeColor = MB_新对话.ForeColor
        End If
    End Sub

    Private Sub Form_v6_Agent_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If e.Cancel Then Return
        CaptureCurrentDraft()
        Try
            _store?.Save()
        Catch ex As Exception
            ShowStatus("保存草稿失败：" & ex.Message)
            e.Cancel = True
        End Try
    End Sub

    Private Sub Form_v6_Agent_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _closing = True
        ' 嵌入主窗口时可能直接 Dispose，不一定触发 FormClosing。
        Try
            _store?.Save()
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine("保存 Agent 草稿失败：" & ex.Message)
        End Try
        _draftSaveTimer?.Dispose()
        _activeRunElapsedTimer?.Dispose()
        For Each run In _conversationRuntimes.Values
            run.RequestCts?.Cancel()
        Next
    End Sub

    Private Sub StartActiveRunElapsedTimer()
        If _activeRunElapsedTimer IsNot Nothing Then
            _activeRunElapsedTimer.Start()
            Return
        End If
        _activeRunElapsedTimer = New System.Windows.Forms.Timer With {.Interval = 1000}
        AddHandler _activeRunElapsedTimer.Tick, AddressOf ActiveRunElapsedTimer_Tick
        _activeRunElapsedTimer.Start()
    End Sub

    Private Sub ActiveRunElapsedTimer_Tick(sender As Object, e As EventArgs)
        Dim turn = GetConversationRuntime(_current).ActiveTurn
        If turn IsNot Nothing Then
            For Each activity In turn.Activities.Where(Function(x) x IsNot Nothing AndAlso x.Kind = "tool" AndAlso x.State = "running")
                Dim group = GetConsecutiveToolActivities(_current, activity)
                Dim item = AgentRoom1.FindItem(group(0).Id)
                If item IsNot Nothing Then item.Title = FormatToolGroupTitle(group)
            Next
        End If
        UpdateActiveRunOverviewCard(_current)
        If Not _conversationRuntimes.Values.Any(Function(x) x.Busy) Then _activeRunElapsedTimer.Stop()
    End Sub

    Private Sub SaveCurrent()
        SaveConversation(_current)
    End Sub

    Private Sub SaveConversation(conversation As AgentConversationData)
        If conversation IsNot Nothing Then conversation.UpdatedAt = DateTime.Now
        Try
            _store.Save()
        Catch ex As Exception
            If Not _closing AndAlso Not IsDisposed Then ShowStatus("保存对话失败：" & ex.Message, True)
        End Try
        RefreshConversationList()
        UpdateUsageButton()
    End Sub

    Private Sub MB_新对话_Click(sender As Object, e As EventArgs) Handles MB_新对话.Click
        EnsureStoreLoaded()
        CaptureCurrentDraft()
        _current = CreateConversationFromCurrentSettings()
        RestoreCurrentDraft()
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
        Dim run = GetConversationRuntime(_current)
        If _current Is Nothing Then Return
        If run.Busy AndAlso IsConversationSelected(run.ActiveRunConversation) Then
            ExFloatingTip(ModernListBox1, "当前对话正在响应，请先停止任务后再删除", 2200)
            Return
        End If

        Dim confirm = ExMsgBox(FormMain_v6, "确认删除当前对话？", MsgBoxStyle.YesNo Or MsgBoxStyle.Question, "删除对话")
        If confirm <> MsgBoxResult.Yes Then Return

        Dim deletedTitle = If(_current.Title, "新对话")
        _conversationRuntimes.Remove(_current.Id)
        _store.Conversations.RemoveAll(Function(x) x.Id = _current.Id)
        NormalizeConversationOrder()
        _current = _store.EnsureConversation()
        RestoreCurrentDraft()
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
        If GetConversationRuntime(_current).Busy Then
            If String.IsNullOrWhiteSpace(ModernTextBox1.Text) AndAlso _pendingFiles.Count = 0 Then
                RequestStopAgentTask()
            Else
                OfferGuidanceMessage()
            End If
        Else
            Await StartUserMessageAsync()
        End If
    End Sub

    Private Async Sub ModernTextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles ModernTextBox1.KeyDown
        Dim run = GetConversationRuntime(_current)
        If e.KeyCode = Keys.Delete AndAlso e.Control Then
            e.SuppressKeyPress = True
            e.Handled = True
            RemoveLatestUserTurnAndCopyToClipboard()
        ElseIf e.KeyCode = Keys.Enter AndAlso e.Alt Then
            e.SuppressKeyPress = True
            e.Handled = True
            If run.Busy Then
                ExFloatingTip(ModernTextBox1, "当前任务正在响应，请先停止后再撤回", 2200)
            Else
                Await ReplaceLatestUserTurnAsync()
            End If
        ElseIf e.KeyCode = Keys.Enter AndAlso Not e.Shift Then
            e.SuppressKeyPress = True
            e.Handled = True
            If run.Busy Then
                OfferGuidanceMessage()
            Else
                Await StartUserMessageAsync()
            End If
        End If
    End Sub

    Private Async Function StartUserMessageAsync() As Task
        Dim run = GetConversationRuntime(_current)
        If run.Busy Then Return
        Dim text = ModernTextBox1.Text.Trim()
        If text = "" AndAlso _pendingFiles.Count > 0 Then text = "已提交以下文件或文件夹。"
        If text = "" Then
            Await RetryLatestUserTurnAsync()
            Return
        End If
        If Not EnsureModelSelected() Then Return

        CommitUserMessage(text)
        Await StartAgentRunAsync(_current)
    End Function

    Private Async Function RetryLatestUserTurnAsync() As Task
        Dim run = GetConversationRuntime(_current)
        If run.Busy OrElse _current Is Nothing Then Return
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
        Dim run = GetConversationRuntime(_current)
        If run.Busy Then Return
        Dim text = ModernTextBox1.Text.Trim()
        If text = "" Then
            ExFloatingTip(ModernTextBox1, "请输入要重新发送的内容", 1800)
            Return
        End If
        If Not EnsureModelSelected() Then Return

        If _current IsNot Nothing Then
            Dim userIndex = GetLatestUserMessageIndex()
            If userIndex >= 0 Then
                Dim latestUserMessage = _current.Messages(userIndex)
                If IsSteeringMessage(latestUserMessage) Then
                    Dim confirmSteering = ExMsgBox(FormMain_v6,
                        "是否移除这条用户消息之后的 AI 推理，并以当前文本从该位置重新开始？",
                        MsgBoxStyle.YesNo Or MsgBoxStyle.Question,
                        "撤回上一条消息")
                    If confirmSteering <> MsgBoxResult.Yes Then Return

                    Dim replacementContent = text
                    Dim fileContext = BuildSubmittedFilesContext()
                    If Not String.IsNullOrWhiteSpace(fileContext) Then replacementContent &= vbCrLf & vbCrLf & fileContext.Trim()
                    RemoveConversationMessagesFrom(userIndex + 1)
                    latestUserMessage.Content = replacementContent
                    For Each turn In If(_current.Turns, New List(Of AgentTurnData))
                        Dim guidance = turn?.Activities?.FirstOrDefault(
                            Function(x) x IsNot Nothing AndAlso
                                String.Equals(x.Kind, "guidance", StringComparison.OrdinalIgnoreCase) AndAlso
                                String.Equals(x.Id, latestUserMessage.Id, StringComparison.OrdinalIgnoreCase))
                        If guidance Is Nothing Then Continue For
                        guidance.Content = replacementContent
                        Exit For
                    Next
                    ApplyConversationSnapshot()
                    ModernTextBox1.Text = ""
                    ClearSubmittedFiles()
                    SaveCurrent()
                    RenderCurrentConversation()
                    Await StartAgentRunAsync(_current)
                    Return
                End If

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
            If String.Equals(_current.Messages(i)?.Role, "user", StringComparison.OrdinalIgnoreCase) Then Return i
        Next
        Return -1
    End Function

    Private Sub RemoveLatestUserTurnAndCopyToClipboard()
        Dim run = GetConversationRuntime(_current)
        If run.Busy Then
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
            If Not String.Equals(message?.Role, "user", StringComparison.OrdinalIgnoreCase) Then Continue For
            If IsSteeringMessage(message) Then
                TrimTurnActivitiesFromSteering(message, includeSteering:=True)
            Else
                affectedUserIds.Add(message.Id)
            End If
        Next
        Dim startsAtUser = startIndex < _current.Messages.Count AndAlso
            String.Equals(_current.Messages(startIndex)?.Role, "user", StringComparison.OrdinalIgnoreCase)
        If Not startsAtUser Then
            For i = Math.Min(startIndex - 1, _current.Messages.Count - 1) To 0 Step -1
                Dim message = _current.Messages(i)
                If Not String.Equals(message?.Role, "user", StringComparison.OrdinalIgnoreCase) Then Continue For
                If IsSteeringMessage(message) Then
                    TrimTurnActivitiesFromSteering(message, includeSteering:=False)
                Else
                    affectedUserIds.Add(message.Id)
                End If
                Exit For
            Next
        End If
        If startIndex < _current.Messages.Count Then _current.Messages.RemoveRange(startIndex, _current.Messages.Count - startIndex)
        If _current.Turns IsNot Nothing AndAlso affectedUserIds.Count > 0 Then
            _current.Turns.RemoveAll(Function(x) x IsNot Nothing AndAlso affectedUserIds.Contains(x.UserMessageId))
        End If

        If ResolveContextCheckpointBoundaryIndex(_current, GetRequestConversationMessages(_current)) < 0 Then
            _current.ContextCheckpoint = Nothing
        End If
    End Sub

    Private Sub OfferGuidanceMessage()
        Dim run = GetConversationRuntime(_current)
        Dim text = ModernTextBox1.Text.Trim()
        If text = "" AndAlso _pendingFiles.Count > 0 Then text = "已提交以下文件或文件夹。"
        If text = "" Then Return
        If Not run.Busy OrElse run.RequestCts Is Nothing OrElse run.RequestCts.IsCancellationRequested Then Return

        Dim content = text
        Dim fileContext = BuildSubmittedFilesContext()
        If Not String.IsNullOrWhiteSpace(fileContext) Then content &= vbCrLf & vbCrLf & fileContext.Trim()
        Dim steeringMessage As New AgentMessageData With {
            .Role = "user",
            .Name = AgentConversationSchema.SteeringMessageName,
            .Content = content
        }
        run.ActiveRunConversation.Messages.Add(steeringMessage)
        run.PendingSteeringMessages.Add(steeringMessage)

        If run.ActiveTurn IsNot Nothing Then
            Dim activity As New AgentTurnActivityData With {
                .Id = steeringMessage.Id,
                .Kind = "guidance",
                .Content = content,
                .State = "completed"
            }
            run.ActiveTurn.Activities.Add(activity)
            If IsConversationSelected(run.ActiveRunConversation) Then AddSteeringActivityToRoom(activity)
        End If

        ModernTextBox1.Text = ""
        ClearSubmittedFiles()
        SaveConversation(run.ActiveRunConversation)
        ShowRunStatus(run.ActiveRunConversation, "已收到调整方向，将在当前步骤结束后继续")
    End Sub

    Private Sub AddSteeringActivityToRoom(activity As AgentTurnActivityData)
        Dim run = GetConversationRuntime(_current)
        If activity Is Nothing OrElse run.ActiveTurn Is Nothing Then Return
        Dim item = AgentRoom1.AddUserMessage(activity.Content)
        Dim anchor = If(run.ActiveResponseItem, run.ActiveThinkingItem)
        Dim anchorIndex = If(anchor Is Nothing, -1, AgentRoom1.Items.IndexOf(anchor))
        If anchorIndex >= 0 AndAlso AgentRoom1.Items.IndexOf(item) > anchorIndex Then
            AgentRoom1.MoveItem(item, anchorIndex)
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
        EnsureStoreLoaded()
        If _current Is Nothing Then _current = CreateConversationFromCurrentSettings()
        ApplyConversationSnapshot()
        AppendUserMessage(text, BuildSubmittedFilesContext())
        ModernTextBox1.Text = ""
        ClearSubmittedFiles()
        SaveCurrent()
        If rerenderConversation Then RenderCurrentConversation()
    End Sub

    Private Function IsSteeringMessage(message As AgentMessageData) As Boolean
        Return message IsNot Nothing AndAlso
            String.Equals(message.Role, "user", StringComparison.OrdinalIgnoreCase) AndAlso
            String.Equals(message.Name, AgentConversationSchema.SteeringMessageName, StringComparison.OrdinalIgnoreCase)
    End Function

    Private Sub TrimTurnActivitiesFromSteering(message As AgentMessageData, includeSteering As Boolean)
        If message Is Nothing OrElse _current?.Turns Is Nothing Then Return
        For Each turn In _current.Turns.AsEnumerable().Reverse()
            If turn?.Activities Is Nothing Then Continue For
            Dim activityIndex = turn.Activities.FindIndex(
                Function(x) x IsNot Nothing AndAlso
                    String.Equals(x.Kind, "guidance", StringComparison.OrdinalIgnoreCase) AndAlso
                    String.Equals(x.Id, message.Id, StringComparison.OrdinalIgnoreCase))
            If activityIndex < 0 Then
                activityIndex = turn.Activities.FindIndex(
                    Function(x) x IsNot Nothing AndAlso
                        String.Equals(x.Kind, "guidance", StringComparison.OrdinalIgnoreCase) AndAlso
                        String.Equals(NormalizeSteeringContent(x.Content), NormalizeSteeringContent(message.Content), StringComparison.Ordinal))
            End If
            If activityIndex < 0 Then Continue For

            Dim removeIndex = activityIndex + If(includeSteering, 0, 1)
            If removeIndex < turn.Activities.Count Then
                turn.Activities.RemoveRange(removeIndex, turn.Activities.Count - removeIndex)
            End If
            turn.State = "completed"
            turn.StatusText = "后续记录已撤回"
            turn.CompletedAt = DateTime.Now
            Return
        Next
    End Sub

    Private Shared Function NormalizeSteeringContent(content As String) As String
        Dim text = If(content, "").Trim()
        If text.StartsWith("调整方向：", StringComparison.Ordinal) Then
            text = text.Substring("调整方向：".Length).TrimStart(ChrW(13), ChrW(10), " "c)
        End If
        Return text
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

    Private Sub ModernListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ModernListBox1.SelectedIndexChanged
        If _refreshingConversationList Then Return
        Dim index = ModernListBox1.SelectedIndex
        If index < 0 OrElse index >= _orderedConversations.Count Then Return
        Dim selectedId = _orderedConversations(index).Id
        EnsureStoreLoaded()
        Dim selectedConversation = _orderedConversations.FirstOrDefault(Function(x) x.Id = selectedId)
        If selectedConversation Is Nothing Then Return
        If ReferenceEquals(_current, selectedConversation) Then Return
        CaptureCurrentDraft()
        _current = selectedConversation
        RestoreCurrentDraft()
        RefreshConversationList()
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
        _store.Save()
        RefreshConversationList()
        ShowStatus("已保存对话排序")
    End Sub

    Private Sub MB_操作提示_Click(sender As Object, e As EventArgs) Handles MB_操作提示.Click
        ExOverlayMsgBox(FormMain_v6, $"{vbCrLf}【文本框快捷键】{vbCrLf}【常规】按 Enter 发送，按 Shift + Enter 换行{vbCrLf}【重新推理】发送空信息可触发重试推理{vbCrLf}【撤回重发】按 Alt + Enter 用当前内容替换最新一次你的内容并重新开始推理{vbCrLf}【单纯撤回】按 Ctrl + Delete 删除最新一轮对话并将用户消息复制到剪贴板{vbCrLf}{vbCrLf}【关于上下文限制和推理级别】{vbCrLf}【*】几乎没有端点会返回模型的上下文限制和推理级别列表{vbCrLf}【*】因此 3FUI 使用兜底策略在本地建立静态数据{vbCrLf}【*】如果你希望对某个模型增加上下文或是增加推理级别可以提 issue{vbCrLf}【*】但我不会把上下文给的太高，这会浪费token，除非该模型特别便宜",, "Agent 操作提示")
    End Sub

    Private Sub AgentButtonLayoutChanged(sender As Object, e As EventArgs) Handles Panel4.SizeChanged, Panel1.SizeChanged, JustEmptyControl5.SizeChanged, JustEmptyControl6.SizeChanged
        ApplyAgentButtonPanelLayout()
    End Sub

    Private Sub ApplyAgentButtonPanelLayout()
        EqualizeTwoButtons(Panel4, MB_新对话, JustEmptyControl5)
        EqualizeTwoButtons(Panel1, MB_重载连接, JustEmptyControl6)
    End Sub

    Private Sub EqualizeTwoButtons(panel As LakeUI.ModernPanel, leftButton As Control, gap As Control)
        If panel Is Nothing OrElse leftButton Is Nothing Then Return
        Dim gapWidth = If(gap Is Nothing OrElse Not gap.Visible, 0, gap.Width)
        Dim available = Math.Max(0, panel.DisplayRectangle.Width - gapWidth)
        Dim leftWidth = available \ 2
        If leftButton.Width <> leftWidth Then leftButton.Width = leftWidth
    End Sub

    Private Sub AgentRoom1_LinkClicked(sender As Object, e As AgentRoom.LinkClickedEventArgs) Handles AgentRoom1.LinkClicked
        If ExFloatingBox("确定打开此链接？", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Process.Start(New ProcessStartInfo With {.FileName = e.Url, .UseShellExecute = True})
        End If
    End Sub
End Class
