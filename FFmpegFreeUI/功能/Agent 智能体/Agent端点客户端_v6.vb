Imports System.IO
Imports System.Net
Imports System.Net.Http
Imports System.Text
Imports System.Text.Json
Imports System.Text.RegularExpressions

Public Class AgentEndpointClient
    Private Shared ReadOnly Http As New HttpClient With {.Timeout = TimeSpan.FromSeconds(90)}

    Public Property Endpoint As String
    Public Property ApiKey As String
    Public Property ExtraHeaders As IReadOnlyDictionary(Of String, String)
    Public Property ExtraBody As IReadOnlyDictionary(Of String, Object)

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
        Try
            Using response = Await SendJsonAsync(HttpMethod.Get, "models", Nothing, cancellationToken)
                Dim raw = Await response.Content.ReadAsStringAsync(cancellationToken)
                If Not response.IsSuccessStatusCode Then
                    Return AgentClientResult(Of List(Of AgentModelInfo)).Fail(ExtractErrorMessage(raw, response.StatusCode), CInt(response.StatusCode))
                End If
                Return AgentClientResult(Of List(Of AgentModelInfo)).Ok(ParseModels(raw))
            End Using
        Catch ex As OperationCanceledException
            Throw
        Catch ex As Exception
            Return AgentClientResult(Of List(Of AgentModelInfo)).Fail(ex.Message)
        End Try
    End Function

    Public Async Function TryCreateChatCompletionAsync(modelId As String,
                                                       messages As IEnumerable(Of AgentMessageData),
                                                       tools As List(Of Dictionary(Of String, Object)),
                                                       reasoningEffort As String,
                                                       Optional cancellationToken As Threading.CancellationToken = Nothing) As Task(Of AgentChatResult)
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
            Using response = Await SendJsonAsync(HttpMethod.Post, "chat/completions", payload, cancellationToken)
                raw = Await response.Content.ReadAsStringAsync(cancellationToken)
                If Not response.IsSuccessStatusCode Then
                    Return AgentChatResult.Fail(ExtractErrorMessage(raw, response.StatusCode), CInt(response.StatusCode), raw)
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
            Return AgentChatResult.Fail(ex.Message, rawJson:=raw)
        End Try
    End Function

    Public Async Function TryCreateChatCompletionStreamingAsync(modelId As String,
                                                                messages As IEnumerable(Of AgentMessageData),
                                                                tools As List(Of Dictionary(Of String, Object)),
                                                                reasoningEffort As String,
                                                                onContentDelta As Action(Of String),
                                                                Optional cancellationToken As Threading.CancellationToken = Nothing) As Task(Of AgentChatResult)
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
            Using request = CreateJsonRequest(HttpMethod.Post, "chat/completions", payload)
                Using response = Await Http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                    If Not response.IsSuccessStatusCode Then
                        Dim errorRaw = Await response.Content.ReadAsStringAsync(cancellationToken)
                        Return AgentChatResult.Fail(ExtractErrorMessage(errorRaw, response.StatusCode), CInt(response.StatusCode), errorRaw)
                    End If

                    Using stream = Await response.Content.ReadAsStreamAsync(cancellationToken)
                        Using reader As New StreamReader(stream, Encoding.UTF8)
                            While True
                                cancellationToken.ThrowIfCancellationRequested()
                                Dim line = Await reader.ReadLineAsync(cancellationToken)
                                If line Is Nothing Then Exit While
                                line = line.Trim()
                                If line = "" OrElse Not line.StartsWith("data:", StringComparison.OrdinalIgnoreCase) Then Continue While

                                Dim data = line.Substring(5).Trim()
                                If data = "[DONE]" Then Exit While
                                raw.AppendLine(data)

                                Using doc = JsonDocument.Parse(data)
                                    AccumulateStreamingChatChunk(doc.RootElement, result, content, toolCallMap, onContentDelta)
                                End Using
                            End While
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
            Dim failed = AgentChatResult.Fail(ex.Message)
            failed.Content = content.ToString()
            failed.RawJson = raw.ToString()
            Return failed
        End Try
    End Function

    Public Async Function TryCreateResponsesWebSearchAsync(modelId As String,
                                                           query As String,
                                                           reasoningEffort As String,
                                                           Optional cancellationToken As Threading.CancellationToken = Nothing) As Task(Of AgentChatResult)
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
            Using response = Await SendJsonAsync(HttpMethod.Post, "responses", payload, cancellationToken)
                Dim raw = Await response.Content.ReadAsStringAsync(cancellationToken)
                If Not response.IsSuccessStatusCode Then
                    Return AgentChatResult.Fail(ExtractErrorMessage(raw, response.StatusCode), CInt(response.StatusCode), raw)
                End If
                Dim result = ParseResponsesResult(raw)
                result.Success = True
                result.StatusCode = CInt(response.StatusCode)
                Return result
            End Using
        Catch ex As OperationCanceledException
            Throw
        Catch ex As Exception
            Return AgentChatResult.Fail(ex.Message)
        End Try
    End Function

    Private Async Function SendJsonAsync(method As HttpMethod,
                                         relativePath As String,
                                         payload As Object,
                                         cancellationToken As Threading.CancellationToken) As Task(Of HttpResponseMessage)
        Return Await Http.SendAsync(CreateJsonRequest(method, relativePath, payload), cancellationToken)
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

        If Not String.IsNullOrWhiteSpace(ApiKey) Then
            request.Headers.Authorization = New Headers.AuthenticationHeaderValue("Bearer", ApiKey)
        End If
        For Each item In ExtraHeaders
            request.Headers.Remove(item.Key)
            If Not request.Headers.TryAddWithoutValidation(item.Key, item.Value) Then
                request.Content?.Headers.TryAddWithoutValidation(item.Key, item.Value)
            End If
        Next

        If payload IsNot Nothing Then
            Dim json = JsonSerializer.Serialize(payload, JsonSO)
            request.Content = New StringContent(json, Encoding.UTF8, "application/json")
        End If

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
                    If Not String.IsNullOrWhiteSpace(msg.Name) Then item("name") = msg.Name
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

    Private Shared Function ExtractErrorMessage(raw As String, statusCode As HttpStatusCode) As String
        If String.IsNullOrWhiteSpace(raw) Then Return $"请求失败：HTTP {CInt(statusCode)}"
        Try
            Using doc = JsonDocument.Parse(raw)
                Dim root = doc.RootElement
                Dim err As JsonElement
                If root.TryGetProperty("error", err) Then
                    If err.ValueKind = JsonValueKind.String Then Return err.GetString()
                    Dim msg As JsonElement
                    If err.ValueKind = JsonValueKind.Object AndAlso err.TryGetProperty("message", msg) AndAlso msg.ValueKind = JsonValueKind.String Then Return msg.GetString()
                End If
                Dim message As JsonElement
                If root.TryGetProperty("message", message) AndAlso message.ValueKind = JsonValueKind.String Then Return message.GetString()
            End Using
        Catch
        End Try
        Return $"请求失败：HTTP {CInt(statusCode)} {raw}"
    End Function
End Class
