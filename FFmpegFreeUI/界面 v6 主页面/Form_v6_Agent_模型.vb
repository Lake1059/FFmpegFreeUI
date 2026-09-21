Imports LakeUI

Partial Public Class Form_v6_Agent
    Private _models As New List(Of AgentModelInfo)
    Private _modelEndpointSignature As String = ""
    Private _refreshingModels As Boolean = False
    Private _pendingModelRefresh As Boolean = False
    Private _agentPageActivated As Boolean = False

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

    Private Sub UseDefaultReasoningEffortsAfterRefreshFailure(client As AgentEndpointClient,
                                                                              selectedModel As String)
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
                RefreshReasoningEfforts()
            Else
                Dim efforts = AgentCapabilityCache.GetDefaultReasoningEfforts(selectedModel)
                MCB_推理级别.Items.Clear()
                MCB_推理级别.Items.AddRange(efforts)

                Dim selected = GetPreferredReasoningEffort()
                Dim index = efforts.FindIndex(Function(x) String.Equals(x, selected, StringComparison.OrdinalIgnoreCase))
                If index < 0 AndAlso efforts.Count > 0 Then index = Math.Min(1, efforts.Count - 1)
                If index >= 0 Then MCB_推理级别.SelectedIndex = index
            End If

            _modelEndpointSignature = currentSignature
            ShowStatus("推理级别使用默认值：" & String.Join("、", AgentCapabilityCache.GetDefaultReasoningEfforts(selectedModel)))
        Finally
            _loading = oldLoading
        End Try
    End Sub

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
                _loading = oldLoading
                modelResult = Await client.TryGetModelsAsync()
                _loading = True
                If _closing OrElse IsDisposed Then Return
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

                UseDefaultReasoningEffortsAfterRefreshFailure(client, selectedModel)
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

            Dim selected = If(String.IsNullOrWhiteSpace(_current?.ModelId), 设置_v6.实例对象.AgentModelId, _current.ModelId)
            Dim index = _models.FindIndex(Function(x) String.Equals(x.Id, selected, StringComparison.OrdinalIgnoreCase))
            If index < 0 Then index = 0
            MCB_模型选择.SelectedIndex = index
            设置_v6.实例对象.AgentModelId = If(MCB_模型选择.SelectedItem, "")
            RefreshReasoningEfforts()
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
            ShowStatus("获取模型列表失败：" & ex.Message, True)
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
            UpdateSendButtonState()
        End Try

        If useDefaultAfterCatch Then
            UseDefaultReasoningEffortsAfterRefreshFailure(client, selectedModel)
        End If

        If _pendingModelRefresh Then
            _pendingModelRefresh = False
            Await RefreshModelsIfEndpointChangedAsync(True)
        End If
    End Function

    Private Sub RefreshReasoningEfforts()
        If MCB_模型选择.SelectedIndex < 0 OrElse MCB_模型选择.SelectedIndex >= _models.Count Then Return
        Dim oldLoading = _loading
        _loading = True
        Try
            Dim model = _models(MCB_模型选择.SelectedIndex)
            MCB_推理级别.Items.Clear()
            MCB_推理级别.Text = ""
            MCB_推理级别.WaterText = "正在读取"
            ShowStatus("正在读取推理级别：" & model.Id)
            Dim efforts = AgentCapabilityCache.GetReasoningEfforts(model, CreateClient())
            MCB_推理级别.Items.AddRange(efforts)

            Dim selected = GetPreferredReasoningEffort()
            Dim index = efforts.FindIndex(Function(x) String.Equals(x, selected, StringComparison.OrdinalIgnoreCase))
            If index < 0 AndAlso efforts.Count > 0 Then index = 0
            If index >= 0 Then MCB_推理级别.SelectedIndex = index
            If model.ReasoningEfforts IsNot Nothing AndAlso model.ReasoningEfforts.Count > 0 Then
                ShowStatus("推理级别已就绪：" & String.Join("、", efforts))
            Else
                ShowStatus("推理级别使用默认值：" & String.Join("、", efforts))
            End If
        Catch ex As Exception
            ShowStatus("读取推理级别失败：" & ex.Message, True)
            ExFloatingTip(MCB_推理级别, ex.Message, 2600)
        Finally
            _loading = oldLoading
            MCB_推理级别.WaterText = "推理级别"
        End Try
    End Sub

    Private Sub MCB_模型选择_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_模型选择.SelectedIndexChanged
        If _loading OrElse MCB_模型选择.SelectedIndex < 0 Then Return
        设置_v6.实例对象.AgentModelId = If(MCB_模型选择.SelectedItem, "")
        If _current IsNot Nothing AndAlso Not GetConversationRuntime(_current).Busy Then _current.ModelId = 设置_v6.实例对象.AgentModelId
        RefreshReasoningEfforts()
    End Sub

    Private Function GetPreferredReasoningEffort() As String
        ' 与其他隐藏设置一样沿用上次选择；模型临时不支持时不覆盖偏好。
        If Not String.IsNullOrWhiteSpace(设置_v6.实例对象.Agent推理级别) Then Return 设置_v6.实例对象.Agent推理级别
        Return If(_current?.ReasoningEffort, "")
    End Function

    Private Sub MCB_推理级别_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_推理级别.SelectedIndexChanged
        If _loading OrElse MCB_推理级别.SelectedIndex < 0 Then Return
        设置_v6.实例对象.Agent推理级别 = If(MCB_推理级别.SelectedItem, "")
        If _current IsNot Nothing AndAlso Not GetConversationRuntime(_current).Busy Then _current.ReasoningEffort = 设置_v6.实例对象.Agent推理级别
        ScheduleDraftSave()
    End Sub

    Private Sub MCB_联网设置_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_联网设置.SelectedIndexChanged
        If _loading Then Return
        设置_v6.实例对象.Agent联网设置 = GetSelectedNetworkMode()
        If _current IsNot Nothing AndAlso Not GetConversationRuntime(_current).Busy Then _current.NetworkMode = 设置_v6.实例对象.Agent联网设置
    End Sub

    Private Sub MCB_权限控制_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_权限控制.SelectedIndexChanged
        If _loading Then Return
        设置_v6.实例对象.Agent权限级别 = Math.Max(0, MCB_权限控制.SelectedIndex)
        If _current IsNot Nothing AndAlso Not GetConversationRuntime(_current).Busy Then _current.PermissionLevel = 设置_v6.实例对象.Agent权限级别
    End Sub

    Private Async Sub MB_重载连接_Click(sender As Object, e As EventArgs) Handles MB_重载连接.Click
        Await RefreshModelsIfEndpointChangedAsync(True)
    End Sub
End Class
