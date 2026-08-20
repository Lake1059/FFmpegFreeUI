Imports System.IO
Imports System.Net
Imports System.Net.Http
Imports System.Text
Imports System.Text.Json
Imports System.Text.RegularExpressions

Public Class AgentEndpointClient
    Private Shared ReadOnly Http As New HttpClient With {.Timeout = TimeSpan.FromSeconds(90)}
    Private Const MaxErrorMessageLength As Integer = 600
    Private Const MaxRawErrorLength As Integer = 32768
    Private Const MaxStreamingRawCaptureLength As Integer = 65536

    Public Property Endpoint As String
    Public Property ApiKey As String
    Public Property ExtraHeaders As IReadOnlyDictionary(Of String, String)
    Public Property ExtraBody As IReadOnlyDictionary(Of String, Object)
    Private _apiPrefix As String = Nothing
    Private _apiPrefixResolved As Boolean = False

    Public Sub New(endpoint As String, apiKey As String, extraHeadersText As String, Optional extraBodyText As String = "")
        Me.Endpoint = NormalizeEndpoint(endpoint)
        Me.ApiKey = If(apiKey, "").Trim()
        Me.ExtraHeaders = ParseAdditionalHeaders(extraHeadersText)
        Me.ExtraBody = ParseExtraBody(extraBodyText)
    End Sub

    Public Shared Function NormalizeEndpoint(endpoint As String) As String
        Dim value = If(endpoint, "").Trim()
        If value.EndsWith("/"c) Then value = value.TrimEnd("/"c)
        Return value
    End Function

    Public Shared Function ParseAdditionalHeaders(text As String) As IReadOnlyDictionary(Of String, String)
        Dim result As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)
        If String.IsNullOrWhiteSpace(text) Then Return result

        Dim lineNumber = 0
        For Each rawLine In text.Replace(vbCrLf, vbLf).Replace(vbCr, vbLf).Split(ControlChars.Lf)
            lineNumber += 1
            Dim line = rawLine.Trim()
            If line = "" Then Continue For
            Dim index = line.IndexOf(":"c)
            If index <= 0 Then Throw New FormatException($"附加请求头第 {lineNumber} 行缺少冒号")
            Dim name = line.Substring(0, index).Trim()
            Dim value = line.Substring(index + 1).Trim()
            If name = "" Then Throw New FormatException($"附加请求头第 {lineNumber} 行缺少名称")
            If name.Contains(ControlChars.Cr) OrElse name.Contains(ControlChars.Lf) Then Throw New FormatException($"附加请求头第 {lineNumber} 行名称无效")
            result(name) = value
        Next

        Return result
    End Function

    Public Shared Function ParseExtraBody(text As String) As IReadOnlyDictionary(Of String, Object)
        Dim result As New Dictionary(Of String, Object)(StringComparer.OrdinalIgnoreCase)
        If String.IsNullOrWhiteSpace(text) Then Return result

        Try
            Using doc = JsonDocument.Parse(text)
                If doc.RootElement.ValueKind <> JsonValueKind.Object Then
                    Throw New FormatException("附加请求 Body 必须是 JSON 对象")
                End If

                For Each prop In doc.RootElement.EnumerateObject()
                    If String.IsNullOrWhiteSpace(prop.Name) Then Throw New FormatException("附加请求 Body 中存在空字段名")
                    result(prop.Name) = prop.Value.Clone()
                Next
            End Using
        Catch ex As JsonException
            Throw New FormatException("附加请求 Body 不是有效 JSON：" & ex.Message, ex)
        End Try

        Return result
    End Function

    Public Async Function TryGetModelsAsync(Optional cancellationToken As Threading.CancellationToken = Nothing) As Task(Of AgentClientResult(Of List(Of AgentModelInfo)))
        Dim lastFailure As AgentClientResult(Of List(Of AgentModelInfo)) = Nothing
        Try
            For Each prefix In GetApiPrefixes()
                Using response = Await SendJsonAsync(HttpMethod.Get, BuildApiPath(prefix, "models"), Nothing, cancellationToken)
                    Dim raw = Await response.Content.ReadAsStringAsync(cancellationToken)
                    If Not response.IsSuccessStatusCode OrElse IsHtmlResponse(raw, response) Then
                        lastFailure = AgentClientResult(Of List(Of AgentModelInfo)).Fail(ExtractErrorMessage(raw, response), CInt(response.StatusCode))
                        Continue For
                    End If

                    Try
                        Dim models = ParseModels(raw)
                        _apiPrefix = prefix
                        _apiPrefixResolved = True
                        Return AgentClientResult(Of List(Of AgentModelInfo)).Ok(models)
                    Catch ex As Exception
                        lastFailure = AgentClientResult(Of List(Of AgentModelInfo)).Fail(FormatExceptionMessage(ex), CInt(response.StatusCode))
                    End Try
                End Using
            Next

            If lastFailure IsNot Nothing Then Return lastFailure
            Return AgentClientResult(Of List(Of AgentModelInfo)).Fail("端点没有返回模型列表。")
        Catch ex As OperationCanceledException
            Throw
        Catch ex As Exception
            Return AgentClientResult(Of List(Of AgentModelInfo)).Fail(FormatExceptionMessage(ex))
        End Try
    End Function

    Public Async Function TryCreateChatCompletionAsync(modelId As String,
                                                       messages As IEnumerable(Of AgentMessageData),
                                                       tools As List(Of Dictionary(Of String, Object)),
                                                       reasoningEffort As String,
                                                       Optional cancellationToken As Threading.CancellationToken = Nothing) As Task(Of AgentChatResult)
        Await EnsureApiPrefixAsync(cancellationToken).ConfigureAwait(False)
        Dim payload As New Dictionary(Of String, Object) From {
            {"model", modelId},
            {"messages", BuildChatMessages(messages)}
        }

        If Not String.IsNullOrWhiteSpace(reasoningEffort) Then payload("reasoning_effort") = reasoningEffort.Trim()
        If tools IsNot Nothing AndAlso tools.Count > 0 Then
            payload("tools") = tools
            payload("tool_choice") = "auto"
        End If
        MergeExtraBodyIntoPayload(payload)

        Dim raw As String = ""
        Try
            Using response = Await SendJsonAsync(HttpMethod.Post, GetChatCompletionsPath(), payload, cancellationToken)
                raw = Await response.Content.ReadAsStringAsync(cancellationToken)
                If Not response.IsSuccessStatusCode Then
                    Return AgentChatResult.Fail(ExtractErrorMessage(raw, response), CInt(response.StatusCode), LimitRawError(raw))
                End If
                If IsHtmlResponse(raw, response) Then
                    Return AgentChatResult.Fail(ExtractErrorMessage(raw, response), CInt(response.StatusCode), LimitRawError(raw))
                End If
                Dim result = ParseChatCompletionRaw(raw)
                If ShouldRetryEmptySseAsStreaming(raw, result) Then
                    Dim streamedResult = Await TryCreateChatCompletionStreamingAsync(modelId, messages, tools, reasoningEffort, Nothing, cancellationToken)
                    If HasChatResultPayload(streamedResult) Then Return streamedResult
                End If
                result.Success = True
                result.StatusCode = CInt(response.StatusCode)
                Return result
            End Using
        Catch ex As OperationCanceledException
            Throw
        Catch ex As Exception
            Return AgentChatResult.Fail(FormatExceptionMessage(ex), rawJson:=LimitRawError(raw))
        End Try
    End Function

    Public Async Function TryCreateChatCompletionStreamingAsync(modelId As String,
                                                                messages As IEnumerable(Of AgentMessageData),
                                                                tools As List(Of Dictionary(Of String, Object)),
                                                                reasoningEffort As String,
                                                                onContentDelta As Action(Of String),
                                                                Optional cancellationToken As Threading.CancellationToken = Nothing) As Task(Of AgentChatResult)
        Await EnsureApiPrefixAsync(cancellationToken)
        Dim payload As New Dictionary(Of String, Object) From {
            {"model", modelId},
            {"messages", BuildChatMessages(messages)},
            {"stream", True},
            {"stream_options", New Dictionary(Of String, Object) From {{"include_usage", True}}}
        }

        If Not String.IsNullOrWhiteSpace(reasoningEffort) Then payload("reasoning_effort") = reasoningEffort.Trim()
        If tools IsNot Nothing AndAlso tools.Count > 0 Then
            payload("tools") = tools
            payload("tool_choice") = "auto"
        End If
        MergeExtraBodyIntoPayload(payload)

        Dim result As New AgentChatResult
        Dim content As New StringBuilder
        Dim raw As New StringBuilder
        Dim toolCallMap As New Dictionary(Of Integer, AgentToolCallInfo)

        Try
            Using request = CreateJsonRequest(HttpMethod.Post, GetChatCompletionsPath(), payload)
                Using response = Await Http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(False)
                    If Not response.IsSuccessStatusCode Then
                        Dim errorRaw = Await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(False)
                        Return AgentChatResult.Fail(ExtractErrorMessage(errorRaw, response), CInt(response.StatusCode), LimitRawError(errorRaw))
                    End If
                    Using stream = Await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(False)
                        Using reader As New StreamReader(stream, Encoding.UTF8)
                            Dim firstNonDataLines As New StringBuilder
                            Dim sawDataEvent As Boolean = False
                            While True
                                cancellationToken.ThrowIfCancellationRequested()
                                Dim line = Await reader.ReadLineAsync(cancellationToken).ConfigureAwait(False)
                                If line Is Nothing Then Exit While
                                line = line.Trim()
                                If line = "" Then Continue While
                                If Not line.StartsWith("data:", StringComparison.OrdinalIgnoreCase) Then
                                    If Not sawDataEvent AndAlso firstNonDataLines.Length < 4096 Then firstNonDataLines.AppendLine(line)
                                    Continue While
                                End If

                                Dim data = line.Substring(5).Trim()
                                sawDataEvent = True
                                If data = "[DONE]" Then Exit While
                                If raw.Length < MaxStreamingRawCaptureLength Then
                                    Dim remaining = MaxStreamingRawCaptureLength - raw.Length
                                    If data.Length + Environment.NewLine.Length <= remaining Then
                                        raw.AppendLine(data)
                                    ElseIf remaining > 0 Then
                                        raw.Append(data.AsSpan(0, Math.Min(data.Length, remaining)))
                                    End If
                                End If

                                Using doc = JsonDocument.Parse(data)
                                    AccumulateStreamingChatChunk(doc.RootElement, result, content, toolCallMap, onContentDelta)
                                End Using
                            End While

                            If Not sawDataEvent Then
                                Dim responseText = firstNonDataLines.ToString()
                                If IsCloudflareChallengeText(responseText) Then
                                    Return AgentChatResult.Fail(BuildCloudflareChallengeMessage(BuildHttpStatusText(response.StatusCode), GetCloudflareRay(response, responseText)), CInt(response.StatusCode), LimitRawError(responseText))
                                End If
                                If ContainsHtmlMarkup(responseText) OrElse String.IsNullOrWhiteSpace(responseText) Then
                                    Return AgentChatResult.Fail(BuildHtmlResponseMessage(BuildHttpStatusText(response.StatusCode)), CInt(response.StatusCode), LimitRawError(responseText))
                                End If
                                Return AgentChatResult.Fail("端点没有返回有效的流式 JSON。", CInt(response.StatusCode), LimitRawError(responseText))
                            End If
                        End Using
                    End Using

                    result.Content = content.ToString()
                    result.RawJson = raw.ToString()
                    AddAccumulatedToolCalls(result, toolCallMap)
                    result.Success = True
                    result.StatusCode = CInt(response.StatusCode)
                    Return result
                End Using
            End Using
        Catch ex As OperationCanceledException
            Throw
        Catch ex As Exception
            Dim failed = AgentChatResult.Fail(FormatExceptionMessage(ex))
            failed.Content = content.ToString()
            failed.RawJson = LimitRawError(raw.ToString())
            Return failed
        End Try
    End Function

    Public Async Function TryCreateResponsesWebSearchAsync(modelId As String,
                                                           query As String,
                                                           reasoningEffort As String,
                                                           Optional cancellationToken As Threading.CancellationToken = Nothing) As Task(Of AgentChatResult)
        Await EnsureApiPrefixAsync(cancellationToken)
        Dim payload As New Dictionary(Of String, Object) From {
            {"model", modelId},
            {"input", If(query, "")},
            {"tools", New Object() {
                New Dictionary(Of String, Object) From {{"type", "web_search_preview"}}
            }}
        }
        If Not String.IsNullOrWhiteSpace(reasoningEffort) Then
            payload("reasoning") = New Dictionary(Of String, Object) From {{"effort", reasoningEffort.Trim()}}
        End If
        Try
            Using response = Await SendJsonAsync(HttpMethod.Post, BuildApiPath(_apiPrefix, "responses"), payload, cancellationToken)
                Dim raw = Await response.Content.ReadAsStringAsync(cancellationToken)
                If Not response.IsSuccessStatusCode Then
                    Return AgentChatResult.Fail(ExtractErrorMessage(raw, response), CInt(response.StatusCode), LimitRawError(raw))
                End If
                If IsHtmlResponse(raw, response) Then
                    Return AgentChatResult.Fail(ExtractErrorMessage(raw, response), CInt(response.StatusCode), LimitRawError(raw))
                End If
                Dim result = ParseResponsesResult(raw)
                result.Success = True
                result.StatusCode = CInt(response.StatusCode)
                Return result
            End Using
        Catch ex As OperationCanceledException
            Throw
        Catch ex As Exception
            Return AgentChatResult.Fail(FormatExceptionMessage(ex))
        End Try
    End Function

    Private Async Function SendJsonAsync(method As HttpMethod,
                                         relativePath As String,
                                         payload As Object,
                                         cancellationToken As Threading.CancellationToken) As Task(Of HttpResponseMessage)
        Return Await Http.SendAsync(CreateJsonRequest(method, relativePath, payload), cancellationToken)
    End Function

    Private Async Function EnsureApiPrefixAsync(cancellationToken As Threading.CancellationToken) As Task
        If _apiPrefixResolved Then Return

        Dim endpointVersion = GetEndpointVersion(Endpoint)
        If endpointVersion <> "" Then
            _apiPrefix = ""
            _apiPrefixResolved = True
            Return
        End If

        Await TryGetModelsAsync(cancellationToken)
        If Not _apiPrefixResolved Then
            _apiPrefix = "v1"
            _apiPrefixResolved = True
        End If
    End Function

    Private Function GetChatCompletionsPath() As String
        Return BuildApiPath(_apiPrefix, "chat/completions")
    End Function

    Private Function GetApiPrefixes() As IEnumerable(Of String)
        If _apiPrefixResolved Then Return {If(_apiPrefix, "")}

        Dim endpointVersion = GetEndpointVersion(Endpoint)
        If endpointVersion <> "" Then Return {""}
        Return {"v1", "v2", "v3", "v4", "v5", "v6", "v7", "v8", "v9", ""}
    End Function

    Private Shared Function BuildApiPath(prefix As String, relativePath As String) As String
        Dim cleanRelative = If(relativePath, "").TrimStart("/"c)
        Dim cleanPrefix = If(prefix, "").Trim("/"c)
        If cleanPrefix = "" Then Return cleanRelative
        Return cleanPrefix & "/" & cleanRelative
    End Function

    Private Shared Function GetEndpointVersion(endpoint As String) As String
        Dim value = NormalizeEndpoint(endpoint)
        If value = "" Then Return ""
        Try
            Dim uri = New Uri(value)
            Dim path = uri.AbsolutePath.TrimEnd("/"c)
            Dim match = Regex.Match(path, "(?:^|/)(v\d+)$", RegexOptions.IgnoreCase)
            Return If(match.Success, match.Groups(1).Value.ToLowerInvariant(), "")
        Catch
            Dim match = Regex.Match(value, "(?:^|/)(v\d+)$", RegexOptions.IgnoreCase)
            Return If(match.Success, match.Groups(1).Value.ToLowerInvariant(), "")
        End Try
    End Function

    Private Sub MergeExtraBodyIntoPayload(payload As Dictionary(Of String, Object))
        If payload Is Nothing OrElse ExtraBody Is Nothing OrElse ExtraBody.Count = 0 Then Return

        For Each item In ExtraBody
            If IsProtectedExtraBodyKey(item.Key) Then Continue For
            payload(item.Key) = item.Value
        Next
    End Sub

    Private Shared Function IsProtectedExtraBodyKey(key As String) As Boolean
        Select Case If(key, "").Trim().ToLowerInvariant()
            Case "model", "messages", "tools", "tool_choice", "stream", "stream_options"
                Return True
            Case Else
                Return False
        End Select
    End Function

    Private Function CreateJsonRequest(method As HttpMethod,
                                       relativePath As String,
                                       payload As Object) As HttpRequestMessage
        If String.IsNullOrWhiteSpace(Endpoint) Then Throw New InvalidOperationException("尚未设置 Agent 自定义地址")
        Dim uri = New Uri($"{Endpoint}/{relativePath.TrimStart("/"c)}")
        Dim request As New HttpRequestMessage(method, uri)

        If payload IsNot Nothing Then
            Dim json = JsonSerializer.Serialize(payload, JsonSO)
            request.Content = New StringContent(json, Encoding.UTF8, "application/json")
        End If

        If Not String.IsNullOrWhiteSpace(ApiKey) Then
            request.Headers.Authorization = New Headers.AuthenticationHeaderValue("Bearer", ApiKey)
        End If
        For Each item In ExtraHeaders
            Dim addedToRequest As Boolean = False
            Try
                request.Headers.Remove(item.Key)
                addedToRequest = request.Headers.TryAddWithoutValidation(item.Key, item.Value)
            Catch ex As InvalidOperationException
                addedToRequest = False
            End Try
            If Not addedToRequest AndAlso request.Content IsNot Nothing Then
                request.Content.Headers.Remove(item.Key)
                request.Content.Headers.TryAddWithoutValidation(item.Key, item.Value)
            End If
        Next

        Return request
    End Function

    Private Shared Function BuildChatMessages(messages As IEnumerable(Of AgentMessageData)) As List(Of Dictionary(Of String, Object))
        Dim result As New List(Of Dictionary(Of String, Object))
        If messages Is Nothing Then Return result

        For Each msg In messages
            If msg Is Nothing OrElse String.IsNullOrWhiteSpace(msg.Role) Then Continue For
            Dim item As New Dictionary(Of String, Object)(StringComparer.OrdinalIgnoreCase) From {
                {"role", msg.Role}
            }
            Select Case msg.Role
                Case "tool"
                    item("tool_call_id") = msg.ToolCallId
                    item("content") = If(msg.Content, "")
                Case "assistant"
                    Dim hasToolCalls = msg.ToolCalls IsNot Nothing AndAlso msg.ToolCalls.Count > 0
                    item("content") = If(hasToolCalls AndAlso String.IsNullOrWhiteSpace(msg.Content), Nothing, If(msg.Content, ""))
                    If hasToolCalls Then
                        item("tool_calls") = msg.ToolCalls.Select(Function(t) New Dictionary(Of String, Object) From {
                            {"id", t.Id},
                            {"type", "function"},
                            {"function", New Dictionary(Of String, Object) From {
                                {"name", t.Name},
                                {"arguments", If(t.Arguments, "{}")}
                            }}
                        }).ToList()
                    End If
                Case Else
                    item("content") = If(msg.Content, "")
                    If Not String.IsNullOrWhiteSpace(msg.Name) AndAlso
                       Not String.Equals(msg.Name, AgentConversationSchema.SteeringMessageName, StringComparison.OrdinalIgnoreCase) AndAlso
                       Not String.Equals(msg.Name, AgentConversationSchema.ActivityMessageName, StringComparison.OrdinalIgnoreCase) Then
                        item("name") = msg.Name
                    End If
            End Select
            result.Add(item)
        Next

        Return result
    End Function

    Private Shared Function ParseModels(raw As String) As List(Of AgentModelInfo)
        Dim result As New List(Of AgentModelInfo)
        Using doc = JsonDocument.Parse(raw)
            Dim root = doc.RootElement
            Dim data As JsonElement
            If Not root.TryGetProperty("data", data) OrElse data.ValueKind <> JsonValueKind.Array Then Return result
            For Each item In data.EnumerateArray()
                Dim model As New AgentModelInfo With {.RawJson = item.GetRawText()}
                Dim id As JsonElement
                If item.TryGetProperty("id", id) AndAlso id.ValueKind = JsonValueKind.String Then model.Id = id.GetString()
                AddStringArrayProperty(item, "supported_endpoint_types", model.SupportedEndpointTypes)
                AddStringArrayProperty(item, "endpoint_types", model.SupportedEndpointTypes)
                AddReasoningEfforts(item, model.ReasoningEfforts)
                model.SupportedEndpointTypes = model.SupportedEndpointTypes.Distinct(StringComparer.OrdinalIgnoreCase).ToList()
                model.ReasoningEfforts = model.ReasoningEfforts.Distinct(StringComparer.OrdinalIgnoreCase).ToList()
                If model.Id <> "" Then result.Add(model)
            Next
        End Using
        Return result
    End Function

    Private Shared Sub AddStringArrayProperty(item As JsonElement, propertyName As String, target As List(Of String))
        Dim element As JsonElement
        If Not item.TryGetProperty(propertyName, element) Then Exit Sub
        If element.ValueKind = JsonValueKind.Array Then
            For Each value In element.EnumerateArray()
                If value.ValueKind = JsonValueKind.String Then target.Add(value.GetString())
            Next
        ElseIf element.ValueKind = JsonValueKind.String Then
            target.Add(element.GetString())
        End If
    End Sub

    Private Shared Sub AddReasoningEfforts(item As JsonElement, target As List(Of String))
        AddReasoningEffortsFromProperty(item, "supported_reasoning_efforts", target)
        AddReasoningEffortsFromProperty(item, "reasoning_efforts", target)
        AddReasoningEffortsFromProperty(item, "reasoning_effort_values", target)
        AddReasoningEffortsFromProperty(item, "supportedReasoningEfforts", target)
        AddReasoningEffortsFromProperty(item, "reasoningEfforts", target)
        AddReasoningEffortsFromProperty(item, "reasoningEffort", target)
        AddReasoningEffortsFromProperty(item, "reasoning_effort", target)
        AddReasoningContainerFromProperty(item, "reasoning", target)
        AddReasoningContainerFromProperty(item, "capabilities", target)
        AddReasoningContainerFromProperty(item, "metadata", target)
        AddReasoningParameterDefinitionsFromProperty(item, "supported_parameters", target)
        AddReasoningParameterDefinitionsFromProperty(item, "supportedParameters", target)
        AddReasoningParameterDefinitionsFromProperty(item, "parameters", target)
    End Sub

    Private Shared Sub AddReasoningEffortsFromProperty(item As JsonElement, propertyName As String, target As List(Of String))
        Dim element As JsonElement
        If item.ValueKind <> JsonValueKind.Object OrElse Not item.TryGetProperty(propertyName, element) Then Return
        AddReasoningEffortValues(element, target)
    End Sub

    Private Shared Sub AddReasoningContainerFromProperty(item As JsonElement, propertyName As String, target As List(Of String))
        Dim element As JsonElement
        If item.ValueKind <> JsonValueKind.Object OrElse Not item.TryGetProperty(propertyName, element) Then Return
        If element.ValueKind = JsonValueKind.String OrElse
           (element.ValueKind = JsonValueKind.Array AndAlso element.EnumerateArray().All(Function(x) x.ValueKind = JsonValueKind.String)) Then
            AddReasoningEffortValues(element, target)
        Else
            AddReasoningEffortsFromContainer(element, target)
        End If
    End Sub

    Private Shared Sub AddReasoningParameterDefinitionsFromProperty(item As JsonElement, propertyName As String, target As List(Of String))
        Dim element As JsonElement
        If item.ValueKind <> JsonValueKind.Object OrElse Not item.TryGetProperty(propertyName, element) Then Return
        AddReasoningEffortsFromParameterDefinitions(element, target)
    End Sub

    Private Shared Sub AddReasoningEffortsFromContainer(element As JsonElement, target As List(Of String))
        Select Case element.ValueKind
            Case JsonValueKind.Object
                If IsReasoningEffortParameterDefinition(element) Then AddReasoningEffortValues(element, target)

                For Each prop In element.EnumerateObject()
                    Dim name = NormalizeJsonName(prop.Name)
                    If IsReasoningEffortValueProperty(name) Then
                        AddReasoningEffortValues(prop.Value, target)
                    ElseIf IsReasoningContainerProperty(name) Then
                        AddReasoningEffortsFromContainer(prop.Value, target)
                    ElseIf IsParameterCollectionProperty(name) Then
                        AddReasoningEffortsFromParameterDefinitions(prop.Value, target)
                    End If
                Next
            Case JsonValueKind.Array
                For Each value In element.EnumerateArray()
                    If value.ValueKind = JsonValueKind.Object Then
                        If IsReasoningEffortParameterDefinition(value) Then
                            AddReasoningEffortValues(value, target)
                        Else
                            AddReasoningEffortsFromContainer(value, target)
                        End If
                    End If
                Next
        End Select
    End Sub

    Private Shared Sub AddReasoningEffortsFromParameterDefinitions(element As JsonElement, target As List(Of String))
        Select Case element.ValueKind
            Case JsonValueKind.Object
                For Each prop In element.EnumerateObject()
                    Dim name = NormalizeJsonName(prop.Name)
                    If IsReasoningEffortValueProperty(name) OrElse IsReasoningEffortParameterName(prop.Name) Then
                        AddReasoningEffortValues(prop.Value, target)
                    ElseIf prop.Value.ValueKind = JsonValueKind.Object OrElse prop.Value.ValueKind = JsonValueKind.Array Then
                        AddReasoningEffortsFromParameterDefinitions(prop.Value, target)
                    End If
                Next
            Case JsonValueKind.Array
                For Each value In element.EnumerateArray()
                    If value.ValueKind = JsonValueKind.String Then
                        If IsReasoningEffortParameterName(value.GetString()) Then Continue For
                    ElseIf value.ValueKind = JsonValueKind.Object Then
                        If IsReasoningEffortParameterDefinition(value) Then
                            AddReasoningEffortValues(value, target)
                        Else
                            AddReasoningEffortsFromParameterDefinitions(value, target)
                        End If
                    End If
                Next
        End Select
    End Sub

    Private Shared Sub AddReasoningEffortValues(element As JsonElement, target As List(Of String))
        Select Case element.ValueKind
            Case JsonValueKind.String
                AddReasoningEffortToken(element.GetString(), target)
            Case JsonValueKind.Array
                For Each value In element.EnumerateArray()
                    AddReasoningEffortValues(value, target)
                Next
            Case JsonValueKind.Object
                For Each prop In element.EnumerateObject()
                    Dim name = NormalizeJsonName(prop.Name)
                    If IsReasoningEffortValueProperty(name) OrElse IsReasoningContainerProperty(name) Then
                        AddReasoningEffortValues(prop.Value, target)
                    ElseIf IsReasoningEffortToken(prop.Name) AndAlso IsReasoningEffortFlagValue(prop.Value) Then
                        AddReasoningEffortToken(prop.Name, target)
                    End If
                Next
        End Select
    End Sub

    Private Shared Function IsReasoningEffortFlagValue(value As JsonElement) As Boolean
        Return value.ValueKind = JsonValueKind.True OrElse value.ValueKind = JsonValueKind.Object OrElse value.ValueKind = JsonValueKind.Array
    End Function

    Private Shared Function IsReasoningEffortParameterDefinition(element As JsonElement) As Boolean
        If element.ValueKind <> JsonValueKind.Object Then Return False
        Dim value As JsonElement
        For Each name In {"name", "id", "key", "param", "parameter"}
            If element.TryGetProperty(name, value) AndAlso value.ValueKind = JsonValueKind.String AndAlso IsReasoningEffortParameterName(value.GetString()) Then Return True
        Next
        Return False
    End Function

    Private Shared Function IsReasoningEffortParameterName(value As String) As Boolean
        Dim normalized = NormalizeJsonName(value)
        Return normalized = "reasoningeffort" OrElse normalized = "reasoningefforts" OrElse normalized = "reasoning.effort"
    End Function

    Private Shared Function IsReasoningContainerProperty(name As String) As Boolean
        Return name = "reasoning" OrElse
               name = "reasoningconfig" OrElse
               name = "reasoningconfiguration" OrElse
               name = "reasoningcapabilities" OrElse
               name = "capabilities" OrElse
               name = "capability" OrElse
               name = "schema" OrElse
               name = "items" OrElse
               name = "properties"
    End Function

    Private Shared Function IsParameterCollectionProperty(name As String) As Boolean
        Return name = "parameters" OrElse
               name = "supportedparameters" OrElse
               name = "supportedparams" OrElse
               name = "params"
    End Function

    Private Shared Function IsReasoningEffortValueProperty(name As String) As Boolean
        Return name = "supportedreasoningefforts" OrElse
               name = "reasoningefforts" OrElse
               name = "reasoningeffort" OrElse
               name = "reasoningeffortvalues" OrElse
               name = "supportedefforts" OrElse
               name = "efforts" OrElse
               name = "effort" OrElse
               name = "supportedlevels" OrElse
               name = "levels" OrElse
               name = "level" OrElse
               name = "enum" OrElse
               name = "choices" OrElse
               name = "options" OrElse
               name = "values" OrElse
               name = "allowedvalues" OrElse
               name = "allowed" OrElse
               name = "oneof" OrElse
               name = "anyof" OrElse
               name = "const" OrElse
               name = "default" OrElse
               name = "min" OrElse
               name = "minimum" OrElse
               name = "max" OrElse
               name = "maximum"
    End Function

    Private Shared Sub AddReasoningEffortToken(rawValue As String, target As List(Of String))
        Dim value = If(rawValue, "").Trim().Trim(""""c, "'"c).ToLowerInvariant()
        If value = "" Then Return

        Dim pieces = Regex.Split(value, "[,\s;/|]+").Where(Function(x) x <> "").ToList()
        If pieces.Count > 1 Then
            For Each piece In pieces
                AddReasoningEffortToken(piece, target)
            Next
            Return
        End If

        If IsReasoningEffortToken(value) Then target.Add(value)
    End Sub

    Private Shared Function IsReasoningEffortToken(value As String) As Boolean
        Dim token = If(value, "").Trim().ToLowerInvariant()
        If token = "" OrElse token.Length > 32 Then Return False
        If Not Regex.IsMatch(token, "^[a-z][a-z0-9_-]*$") Then Return False

        Select Case token
            Case "object", "string", "number", "integer", "boolean", "array", "null",
                 "true", "false", "enabled", "disabled", "supported", "unsupported",
                 "reasoning", "effort", "efforts", "level", "levels", "default",
                 "type", "description", "name", "id", "key", "param", "parameter",
                 "required", "optional", "minimum", "maximum", "min", "max"
                Return False
            Case Else
                Return True
        End Select
    End Function

    Private Shared Function NormalizeJsonName(value As String) As String
        Return If(value, "").Trim().Replace("_", "").Replace("-", "").ToLowerInvariant()
    End Function

    Private Shared Function ParseChatCompletionRaw(raw As String) As AgentChatResult
        If IsSseRaw(raw) Then Return ParseStreamingChatResult(raw)
        Return ParseChatResult(raw)
    End Function

    Private Shared Function IsSseRaw(raw As String) As Boolean
        If String.IsNullOrWhiteSpace(raw) Then Return False
        For Each line In raw.Replace(vbCrLf, vbLf).Replace(vbCr, vbLf).Split(ControlChars.Lf)
            line = line.Trim()
            If line = "" Then Continue For
            Return line.StartsWith("data:", StringComparison.OrdinalIgnoreCase)
        Next
        Return False
    End Function

    Private Shared Function ShouldRetryEmptySseAsStreaming(raw As String, result As AgentChatResult) As Boolean
        Return IsSseRaw(raw) AndAlso Not HasChatResultPayload(result)
    End Function

    Private Shared Function HasChatResultPayload(result As AgentChatResult) As Boolean
        If result Is Nothing OrElse Not result.Success Then Return False
        If Not String.IsNullOrWhiteSpace(result.Content) Then Return True
        Return result.ToolCalls IsNot Nothing AndAlso result.ToolCalls.Count > 0
    End Function

    Private Shared Function ParseStreamingChatResult(raw As String) As AgentChatResult
        Dim result As New AgentChatResult With {.RawJson = raw}
        Dim content As New StringBuilder
        Dim toolCallMap As New Dictionary(Of Integer, AgentToolCallInfo)

        For Each line In If(raw, "").Replace(vbCrLf, vbLf).Replace(vbCr, vbLf).Split(ControlChars.Lf)
            line = line.Trim()
            If line = "" OrElse Not line.StartsWith("data:", StringComparison.OrdinalIgnoreCase) Then Continue For

            Dim data = line.Substring(5).Trim()
            If data = "" OrElse data = "[DONE]" Then Continue For

            Using doc = JsonDocument.Parse(data)
                AccumulateStreamingChatChunk(doc.RootElement, result, content, toolCallMap, Nothing)
            End Using
        Next

        result.Content = content.ToString()
        AddAccumulatedToolCalls(result, toolCallMap)
        Return result
    End Function

    Private Shared Sub AccumulateStreamingChatChunk(root As JsonElement,
                                                    result As AgentChatResult,
                                                    content As StringBuilder,
                                                    toolCallMap As Dictionary(Of Integer, AgentToolCallInfo),
                                                    onContentDelta As Action(Of String))
        Dim usage As JsonElement
        If root.TryGetProperty("usage", usage) AndAlso usage.ValueKind = JsonValueKind.Object Then
            result.Usage = ParseUsage(root)
        End If

        Dim choices As JsonElement
        If Not root.TryGetProperty("choices", choices) OrElse choices.ValueKind <> JsonValueKind.Array OrElse choices.GetArrayLength() = 0 Then Return

        Dim delta As JsonElement
        If choices(0).TryGetProperty("delta", delta) AndAlso delta.ValueKind = JsonValueKind.Object Then
            AccumulateChatMessageDelta(delta, content, toolCallMap, onContentDelta)
            Return
        End If

        Dim message As JsonElement
        If choices(0).TryGetProperty("message", message) AndAlso message.ValueKind = JsonValueKind.Object Then
            AccumulateChatMessageDelta(message, content, toolCallMap, onContentDelta)
        End If
    End Sub

    Private Shared Sub AccumulateChatMessageDelta(delta As JsonElement,
                                                  content As StringBuilder,
                                                  toolCallMap As Dictionary(Of Integer, AgentToolCallInfo),
                                                  onContentDelta As Action(Of String))
        Dim textPart As JsonElement
        If delta.TryGetProperty("content", textPart) AndAlso textPart.ValueKind = JsonValueKind.String Then
            Dim part = textPart.GetString()
            If part <> "" Then
                content.Append(part)
                onContentDelta?.Invoke(part)
            End If
        End If

        Dim toolCalls As JsonElement
        If delta.TryGetProperty("tool_calls", toolCalls) AndAlso toolCalls.ValueKind = JsonValueKind.Array Then
            For Each callDelta In toolCalls.EnumerateArray()
                Dim indexValue = GetInt(callDelta, "index")
                Dim callInfo As AgentToolCallInfo = Nothing
                If Not toolCallMap.TryGetValue(indexValue, callInfo) Then
                    callInfo = New AgentToolCallInfo
                    toolCallMap(indexValue) = callInfo
                End If

                Dim value As JsonElement
                If callDelta.TryGetProperty("id", value) AndAlso value.ValueKind = JsonValueKind.String AndAlso value.GetString() <> "" Then callInfo.Id = value.GetString()
                Dim fn As JsonElement
                If callDelta.TryGetProperty("function", fn) AndAlso fn.ValueKind = JsonValueKind.Object Then
                    If fn.TryGetProperty("name", value) AndAlso value.ValueKind = JsonValueKind.String Then callInfo.Name &= value.GetString()
                    If fn.TryGetProperty("arguments", value) AndAlso value.ValueKind = JsonValueKind.String Then callInfo.Arguments &= value.GetString()
                End If
            Next
        End If
    End Sub

    Private Shared Sub AddAccumulatedToolCalls(result As AgentChatResult,
                                               toolCallMap As Dictionary(Of Integer, AgentToolCallInfo))
        For Each item In toolCallMap.OrderBy(Function(x) x.Key).Select(Function(x) x.Value)
            If item.Id = "" Then item.Id = Guid.NewGuid().ToString("N")
            If item.Name <> "" Then result.ToolCalls.Add(item)
        Next
    End Sub

    Private Shared Function ParseChatResult(raw As String) As AgentChatResult
        Dim result As New AgentChatResult With {.RawJson = raw}
        Using doc = JsonDocument.Parse(raw)
            Dim root = doc.RootElement
            result.Usage = ParseUsage(root)
            Dim choices As JsonElement
            If Not root.TryGetProperty("choices", choices) OrElse choices.ValueKind <> JsonValueKind.Array OrElse choices.GetArrayLength() = 0 Then Return result
            Dim message As JsonElement
            If Not choices(0).TryGetProperty("message", message) Then Return result
            Dim content As JsonElement
            If message.TryGetProperty("content", content) AndAlso content.ValueKind = JsonValueKind.String Then result.Content = content.GetString()
            Dim toolCalls As JsonElement
            If message.TryGetProperty("tool_calls", toolCalls) AndAlso toolCalls.ValueKind = JsonValueKind.Array Then
                For Each callItem In toolCalls.EnumerateArray()
                    Dim callInfo As New AgentToolCallInfo
                    Dim v As JsonElement
                    If callItem.TryGetProperty("id", v) AndAlso v.ValueKind = JsonValueKind.String Then callInfo.Id = v.GetString()
                    Dim fn As JsonElement
                    If callItem.TryGetProperty("function", fn) Then
                        If fn.TryGetProperty("name", v) AndAlso v.ValueKind = JsonValueKind.String Then callInfo.Name = v.GetString()
                        If fn.TryGetProperty("arguments", v) AndAlso v.ValueKind = JsonValueKind.String Then callInfo.Arguments = v.GetString()
                    End If
                    If callInfo.Id = "" Then callInfo.Id = Guid.NewGuid().ToString("N")
                    If callInfo.Name <> "" Then result.ToolCalls.Add(callInfo)
                Next
            End If
        End Using
        Return result
    End Function

    Private Shared Function ParseResponsesResult(raw As String) As AgentChatResult
        Dim result As New AgentChatResult With {.RawJson = raw}
        Using doc = JsonDocument.Parse(raw)
            Dim root = doc.RootElement
            result.Usage = ParseUsage(root)
            Dim outputText As JsonElement
            If root.TryGetProperty("output_text", outputText) AndAlso outputText.ValueKind = JsonValueKind.String AndAlso outputText.GetString() <> "" Then
                result.Content = outputText.GetString()
                Return result
            End If

            Dim output As JsonElement
            If root.TryGetProperty("output", output) AndAlso output.ValueKind = JsonValueKind.Array Then
                Dim sb As New StringBuilder
                For Each item In output.EnumerateArray()
                    Dim content As JsonElement
                    If Not item.TryGetProperty("content", content) OrElse content.ValueKind <> JsonValueKind.Array Then Continue For
                    For Each part In content.EnumerateArray()
                        Dim text As JsonElement
                        If part.TryGetProperty("text", text) AndAlso text.ValueKind = JsonValueKind.String Then
                            If sb.Length > 0 Then sb.AppendLine()
                            sb.Append(text.GetString())
                        End If
                    Next
                Next
                result.Content = sb.ToString()
            End If
        End Using
        Return result
    End Function

    Private Shared Function ParseUsage(root As JsonElement) As AgentUsageInfo
        Dim result As New AgentUsageInfo
        Dim usage As JsonElement
        If Not root.TryGetProperty("usage", usage) OrElse usage.ValueKind <> JsonValueKind.Object Then Return result
        result.PromptTokens = GetInt(usage, "prompt_tokens")
        result.CompletionTokens = GetInt(usage, "completion_tokens")
        result.TotalTokens = GetInt(usage, "total_tokens")
        result.InputTokens = GetInt(usage, "input_tokens")
        result.OutputTokens = GetInt(usage, "output_tokens")
        result.EffectiveInputTokens = If(result.InputTokens > 0, result.InputTokens, result.PromptTokens)
        result.EffectiveOutputTokens = If(result.OutputTokens > 0, result.OutputTokens, result.CompletionTokens)
        If result.TotalTokens <= 0 Then result.TotalTokens = result.EffectiveInputTokens + result.EffectiveOutputTokens

        Dim details As JsonElement
        If usage.TryGetProperty("prompt_tokens_details", details) AndAlso details.ValueKind = JsonValueKind.Object Then
            result.CachedTokens = GetInt(details, "cached_tokens")
        End If
        If usage.TryGetProperty("input_tokens_details", details) AndAlso details.ValueKind = JsonValueKind.Object Then
            result.CachedTokens = Math.Max(result.CachedTokens, GetInt(details, "cached_tokens"))
        End If
        result.CachedTokens = Math.Max(result.CachedTokens, GetInt(usage, "cached_tokens"))
        result.CachedTokens = Math.Max(result.CachedTokens, GetInt(usage, "cache_read_input_tokens"))
        If usage.TryGetProperty("completion_tokens_details", details) AndAlso details.ValueKind = JsonValueKind.Object Then
            result.ReasoningTokens = GetInt(details, "reasoning_tokens")
        End If
        If usage.TryGetProperty("output_tokens_details", details) AndAlso details.ValueKind = JsonValueKind.Object Then
            result.ReasoningTokens = Math.Max(result.ReasoningTokens, GetInt(details, "reasoning_tokens"))
        End If
        Return result
    End Function

    Private Shared Function GetInt(element As JsonElement, name As String) As Integer
        Dim value As JsonElement
        If element.TryGetProperty(name, value) AndAlso value.ValueKind = JsonValueKind.Number Then
            Dim result As Integer
            If value.TryGetInt32(result) Then Return result
        End If
        Return 0
    End Function

    Private Shared Function ExtractErrorMessage(raw As String, response As HttpResponseMessage) As String
        Dim statusCode = response.StatusCode
        Dim statusText = BuildHttpStatusText(statusCode)
        If IsCloudflareChallengeResponse(raw, response) Then
            Return BuildCloudflareChallengeMessage(statusText, GetCloudflareRay(response, raw))
        End If
        If IsHtmlResponse(raw, response) Then Return BuildHtmlResponseMessage(statusText)
        If String.IsNullOrWhiteSpace(raw) Then Return statusText

        Try
            Using doc = JsonDocument.Parse(raw)
                Dim root = doc.RootElement
                Dim err As JsonElement
                If root.TryGetProperty("error", err) Then
                    If err.ValueKind = JsonValueKind.String Then Return FormatErrorDetail(err.GetString(), statusText)
                    Dim msg As JsonElement
                    If err.ValueKind = JsonValueKind.Object AndAlso err.TryGetProperty("message", msg) AndAlso msg.ValueKind = JsonValueKind.String Then
                        Return FormatErrorDetail(msg.GetString(), statusText)
                    End If
                End If
                Dim message As JsonElement
                If root.TryGetProperty("message", message) AndAlso message.ValueKind = JsonValueKind.String Then
                    Return FormatErrorDetail(message.GetString(), statusText)
                End If
            End Using
        Catch
        End Try

        Dim detail = LimitErrorMessage(raw)
        If detail = "" Then Return statusText
        Return LimitErrorMessage(statusText & "：" & detail)
    End Function

    Private Shared Function FormatExceptionMessage(ex As Exception) As String
        Dim message = If(ex?.Message, "").Trim()
        If message = "" Then Return "Agent 请求失败。"

        Dim statusText = TryExtractHttpStatusText(message)
        If IsCloudflareChallengeText(message) Then
            Return BuildCloudflareChallengeMessage(statusText, GetCloudflareRay(Nothing, message))
        End If
        If ContainsHtmlMarkup(message) Then Return BuildHtmlResponseMessage(statusText)
        Return LimitErrorMessage(message)
    End Function

    Private Shared Function FormatErrorDetail(detail As String, statusText As String) As String
        If IsCloudflareChallengeText(detail) Then
            Return BuildCloudflareChallengeMessage(statusText, GetCloudflareRay(Nothing, detail))
        End If
        If ContainsHtmlMarkup(detail) Then Return BuildHtmlResponseMessage(statusText)

        Dim result = LimitErrorMessage(detail)
        Return If(result = "", statusText, result)
    End Function

    Private Shared Function IsCloudflareChallengeResponse(raw As String, response As HttpResponseMessage) As Boolean
        If Not IsJsonDocument(raw) AndAlso IsCloudflareChallengeText(raw) Then Return True
        If Not HasCloudflareHeader(response) OrElse Not IsHtmlResponse(raw, response) Then Return False

        Select Case response.StatusCode
            Case HttpStatusCode.Forbidden, HttpStatusCode.TooManyRequests, HttpStatusCode.ServiceUnavailable
                Return True
            Case Else
                Return False
        End Select
    End Function

    Private Shared Function IsCloudflareChallengeText(value As String) As Boolean
        If String.IsNullOrWhiteSpace(value) Then Return False
        Return ContainsIgnoreCase(value, "challenges.cloudflare.com") OrElse
               ContainsIgnoreCase(value, "cf-chl-") OrElse
               ContainsIgnoreCase(value, "cf-mitigated") OrElse
               (ContainsIgnoreCase(value, "cloudflare") AndAlso
                (ContainsIgnoreCase(value, "just a moment") OrElse
                 ContainsIgnoreCase(value, "attention required") OrElse
                 ContainsIgnoreCase(value, "verify you are human") OrElse
                 ContainsIgnoreCase(value, "security verification")))
    End Function

    Private Shared Function IsHtmlResponse(raw As String, response As HttpResponseMessage) As Boolean
        If IsJsonDocument(raw) Then Return False
        Dim mediaType = response?.Content?.Headers?.ContentType?.MediaType
        Return ContainsIgnoreCase(mediaType, "html") OrElse ContainsHtmlMarkup(raw)
    End Function

    Private Shared Function IsJsonDocument(value As String) As Boolean
        If String.IsNullOrWhiteSpace(value) Then Return False
        Try
            Using doc = JsonDocument.Parse(value)
                Return doc.RootElement.ValueKind = JsonValueKind.Object OrElse
                       doc.RootElement.ValueKind = JsonValueKind.Array
            End Using
        Catch
            Return False
        End Try
    End Function

    Private Shared Function ContainsHtmlMarkup(value As String) As Boolean
        If String.IsNullOrWhiteSpace(value) Then Return False
        Return Regex.IsMatch(value, "<!doctype\s+html|<html(?:\s|>)|<head(?:\s|>)|<body(?:\s|>)", RegexOptions.IgnoreCase)
    End Function

    Private Shared Function HasCloudflareHeader(response As HttpResponseMessage) As Boolean
        Return HeaderContains(response, "server", "cloudflare") OrElse
               HeaderContains(response, "cf-ray", "") OrElse
               HeaderContains(response, "cf-mitigated", "")
    End Function

    Private Shared Function HeaderContains(response As HttpResponseMessage, name As String, expectedValue As String) As Boolean
        If response Is Nothing Then Return False
        Dim values As IEnumerable(Of String) = Nothing
        If Not response.Headers.TryGetValues(name, values) Then Return False
        If expectedValue = "" Then Return values.Any()
        Return values.Any(Function(value) ContainsIgnoreCase(value, expectedValue))
    End Function

    Private Shared Function GetCloudflareRay(response As HttpResponseMessage, source As String) As String
        If response IsNot Nothing Then
            Dim values As IEnumerable(Of String) = Nothing
            If response.Headers.TryGetValues("cf-ray", values) Then
                Dim headerValue = values.FirstOrDefault()
                If Not String.IsNullOrWhiteSpace(headerValue) Then Return LimitErrorMessage(headerValue.Trim(), 80)
            End If
        End If

        Dim match = Regex.Match(If(source, ""), "cf-ray[^0-9a-f]*(?<ray>[0-9a-f]{12,32}(?:-[a-z0-9]+)?)", RegexOptions.IgnoreCase)
        Return If(match.Success, match.Groups("ray").Value, "")
    End Function

    Private Shared Function BuildCloudflareChallengeMessage(statusText As String, rayId As String) As String
        Dim result = statusText & "。目标站点返回了 Cloudflare 安全检查页面，Agent 无法完成浏览器验证。请改用可直接调用的 API 地址，或在 Cloudflare/上游服务中放行该 API 路径和当前 IP。"
        If Not String.IsNullOrWhiteSpace(rayId) Then result &= " CF-Ray：" & rayId
        Return LimitErrorMessage(result)
    End Function

    Private Shared Function BuildHtmlResponseMessage(statusText As String) As String
        Return LimitErrorMessage(statusText & "。目标站点返回了 HTML 页面而不是 API JSON。请检查端点地址、反向代理和上游服务状态。")
    End Function

    Private Shared Function BuildHttpStatusText(statusCode As HttpStatusCode) As String
        Dim prefix = If(CInt(statusCode) >= 200 AndAlso CInt(statusCode) < 300, "端点响应异常", "请求失败")
        Dim reason = statusCode.ToString()
        If reason = CInt(statusCode).ToString() Then Return $"{prefix}：HTTP {CInt(statusCode)}"
        Return $"{prefix}：HTTP {CInt(statusCode)} {reason}"
    End Function

    Private Shared Function TryExtractHttpStatusText(message As String) As String
        Dim match = Regex.Match(If(message, ""), "(?:unexpected\s+status|http)\s+(?<code>\d{3})", RegexOptions.IgnoreCase)
        If Not match.Success Then Return "Agent 请求失败"

        Dim code As Integer
        If Not Integer.TryParse(match.Groups("code").Value, code) Then Return "Agent 请求失败"
        Return BuildHttpStatusText(CType(code, HttpStatusCode))
    End Function

    Private Shared Function LimitErrorMessage(value As String, Optional maxLength As Integer = MaxErrorMessageLength) As String
        If maxLength <= 0 Then Return ""

        Dim source = If(value, "")
        Dim scanLength = Math.Max(maxLength + 1, maxLength * 4)
        Dim sourceWasTruncated = source.Length > scanLength
        If sourceWasTruncated Then source = source.Substring(0, scanLength)

        Dim result = Regex.Replace(source, "\s+", " ").Trim()
        If Not sourceWasTruncated AndAlso result.Length <= maxLength Then Return result

        Const suffix As String = "...[错误信息已截断]"
        If maxLength <= suffix.Length Then Return suffix.Substring(0, maxLength)
        Return result.Substring(0, Math.Min(result.Length, maxLength - suffix.Length)) & suffix
    End Function

    Private Shared Function LimitRawError(value As String) As String
        value = If(value, "")
        If value.Length <= MaxRawErrorLength Then Return value
        Return value.Substring(0, MaxRawErrorLength) & "...[错误响应已截断]"
    End Function

    Private Shared Function ContainsIgnoreCase(value As String, expected As String) As Boolean
        Return Not String.IsNullOrEmpty(value) AndAlso value.IndexOf(expected, StringComparison.OrdinalIgnoreCase) >= 0
    End Function
End Class
