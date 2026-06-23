Imports System.Drawing.Imaging
Imports System.IO
Imports System.Net
Imports System.Net.Http
Imports System.Text
Imports System.Text.Json
Imports System.Text.RegularExpressions

Public Class AgentUsageInfo
    Public Property PromptTokens As Integer
    Public Property CompletionTokens As Integer
    Public Property TotalTokens As Integer
    Public Property CachedTokens As Integer
    Public Property ReasoningTokens As Integer
    Public Property InputTokens As Integer
    Public Property OutputTokens As Integer

    Public Sub Add(other As AgentUsageInfo)
        If other Is Nothing Then Exit Sub
        PromptTokens += other.PromptTokens
        CompletionTokens += other.CompletionTokens
        TotalTokens += other.TotalTokens
        CachedTokens += other.CachedTokens
        ReasoningTokens += other.ReasoningTokens
        InputTokens += other.InputTokens
        OutputTokens += other.OutputTokens
    End Sub
End Class

Public Class AgentModelInfo
    Public Property Id As String = ""
    Public Property SupportedEndpointTypes As New List(Of String)
    Public Property ReasoningEfforts As New List(Of String)
    Public Property RawJson As String = ""
End Class

Public Class AgentToolCallInfo
    Public Property Id As String = ""
    Public Property Name As String = ""
    Public Property Arguments As String = ""
End Class

Public Class AgentMessageData
    Public Property Id As String = Guid.NewGuid().ToString("N")
    Public Property Role As String = ""
    Public Property Content As String = ""
    Public Property Name As String = ""
    Public Property ToolCallId As String = ""
    Public Property ToolCalls As New List(Of AgentToolCallInfo)
    Public Property CreatedAt As DateTime = DateTime.Now
End Class

Public Class AgentConversationData
    Public Property Id As String = Guid.NewGuid().ToString("N")
    Public Property Title As String = "新对话"
    Public Property CreatedAt As DateTime = DateTime.Now
    Public Property UpdatedAt As DateTime = DateTime.Now
    Public Property SortOrder As Integer = 0
    Public Property ModelId As String = ""
    Public Property ReasoningEffort As String = ""
    Public Property NetworkMode As Integer = 0
    Public Property PermissionLevel As Integer = 0
    Public Property Messages As New List(Of AgentMessageData)
    Public Property Usage As New AgentUsageInfo
End Class

Public NotInheritable Class AgentNetworkMode
    Private Sub New()
    End Sub

    Public Const Local As Integer = 0
    Public Const Endpoint As Integer = 1
    Public Const Disabled As Integer = 2

    Public Shared Function Normalize(value As Integer) As Integer
        Select Case value
            Case Endpoint, Disabled, Local
                Return value
            Case Else
                Return Local
        End Select
    End Function

    Public Shared Function IsEnabled(value As Integer) As Boolean
        Return Normalize(value) <> Disabled
    End Function

    Public Shared Function DisplayName(value As Integer) As String
        Select Case Normalize(value)
            Case Local
                Return "本地联网"
            Case Endpoint
                Return "端点联网"
            Case Else
                Return "禁用联网"
        End Select
    End Function
End Class

Public Class AgentConversationIndexFile
    Public Property Version As Integer = 2
    Public Property Items As New List(Of AgentConversationIndexItem)
End Class

Public Class AgentConversationIndexItem
    Public Property Id As String = ""
    Public Property Title As String = ""
    Public Property CreatedAt As DateTime = DateTime.Now
    Public Property UpdatedAt As DateTime = DateTime.Now
    Public Property SortOrder As Integer = 0
    Public Property FileName As String = ""
End Class

Public Class AgentChatResult
    Public Property Success As Boolean = True
    Public Property ErrorMessage As String = ""
    Public Property StatusCode As Integer = 0
    Public Property Content As String = ""
    Public Property ToolCalls As New List(Of AgentToolCallInfo)
    Public Property Usage As AgentUsageInfo
    Public Property RawJson As String = ""

    Public Shared Function Fail(message As String, Optional statusCode As Integer = 0, Optional rawJson As String = "") As AgentChatResult
        Return New AgentChatResult With {
            .Success = False,
            .ErrorMessage = If(message, ""),
            .StatusCode = statusCode,
            .RawJson = If(rawJson, "")
        }
    End Function
End Class

Public Class AgentClientResult(Of T)
    Public Property Success As Boolean
    Public Property Value As T
    Public Property ErrorMessage As String = ""
    Public Property StatusCode As Integer = 0

    Public Shared Function Ok(value As T) As AgentClientResult(Of T)
        Return New AgentClientResult(Of T) With {.Success = True, .Value = value}
    End Function

    Public Shared Function Fail(message As String, Optional statusCode As Integer = 0) As AgentClientResult(Of T)
        Return New AgentClientResult(Of T) With {
            .Success = False,
            .ErrorMessage = If(message, ""),
            .StatusCode = statusCode
        }
    End Function
End Class

Public Class AgentEndpointClient
    Private Shared ReadOnly Http As New HttpClient With {.Timeout = TimeSpan.FromSeconds(90)}

    Public Property Endpoint As String
    Public Property ApiKey As String
    Public Property ExtraHeaders As IReadOnlyDictionary(Of String, String)

    Public Sub New(endpoint As String, apiKey As String, extraHeadersText As String)
        Me.Endpoint = NormalizeEndpoint(endpoint)
        Me.ApiKey = If(apiKey, "").Trim()
        Me.ExtraHeaders = ParseAdditionalHeaders(extraHeadersText)
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

    Public Async Function GetModelsAsync(Optional cancellationToken As Threading.CancellationToken = Nothing) As Task(Of List(Of AgentModelInfo))
        Dim result = Await TryGetModelsAsync(cancellationToken)
        If Not result.Success Then Throw New InvalidOperationException(result.ErrorMessage)
        Return result.Value
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

        Try
            Using response = Await SendJsonAsync(HttpMethod.Post, "chat/completions", payload, cancellationToken)
                Dim raw = Await response.Content.ReadAsStringAsync(cancellationToken)
                If Not response.IsSuccessStatusCode Then
                    Return AgentChatResult.Fail(ExtractErrorMessage(raw, response.StatusCode), CInt(response.StatusCode), raw)
                End If
                Dim result = ParseChatResult(raw)
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

    Public Async Function CreateChatCompletionAsync(modelId As String,
                                                    messages As IEnumerable(Of AgentMessageData),
                                                    tools As List(Of Dictionary(Of String, Object)),
                                                    reasoningEffort As String,
                                                    Optional cancellationToken As Threading.CancellationToken = Nothing) As Task(Of AgentChatResult)
        Dim result = Await TryCreateChatCompletionAsync(modelId, messages, tools, reasoningEffort, cancellationToken)
        If Not result.Success Then Throw New InvalidOperationException(result.ErrorMessage)
        Return result
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
                                    Dim root = doc.RootElement
                                    Dim usage As JsonElement
                                    If root.TryGetProperty("usage", usage) AndAlso usage.ValueKind = JsonValueKind.Object Then
                                        result.Usage = ParseUsage(root)
                                    End If

                                    Dim choices As JsonElement
                                    If Not root.TryGetProperty("choices", choices) OrElse choices.ValueKind <> JsonValueKind.Array OrElse choices.GetArrayLength() = 0 Then Continue While
                                    Dim delta As JsonElement
                                    If Not choices(0).TryGetProperty("delta", delta) OrElse delta.ValueKind <> JsonValueKind.Object Then Continue While

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
                                End Using
                            End While
                        End Using
                    End Using

                    result.Content = content.ToString()
                    result.RawJson = raw.ToString()
                    For Each item In toolCallMap.OrderBy(Function(x) x.Key).Select(Function(x) x.Value)
                        If item.Id = "" Then item.Id = Guid.NewGuid().ToString("N")
                        If item.Name <> "" Then result.ToolCalls.Add(item)
                    Next
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

    Public Async Function CreateChatCompletionStreamingAsync(modelId As String,
                                                             messages As IEnumerable(Of AgentMessageData),
                                                             tools As List(Of Dictionary(Of String, Object)),
                                                             reasoningEffort As String,
                                                             onContentDelta As Action(Of String),
                                                             Optional cancellationToken As Threading.CancellationToken = Nothing) As Task(Of AgentChatResult)
        Dim result = Await TryCreateChatCompletionStreamingAsync(modelId, messages, tools, reasoningEffort, onContentDelta, cancellationToken)
        If Not result.Success Then Throw New InvalidOperationException(result.ErrorMessage)
        Return result
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

    Public Async Function CreateResponsesWebSearchAsync(modelId As String,
                                                        query As String,
                                                        reasoningEffort As String,
                                                        Optional cancellationToken As Threading.CancellationToken = Nothing) As Task(Of AgentChatResult)
        Dim result = Await TryCreateResponsesWebSearchAsync(modelId, query, reasoningEffort, cancellationToken)
        If Not result.Success Then Throw New InvalidOperationException(result.ErrorMessage)
        Return result
    End Function

    Private Async Function SendJsonAsync(method As HttpMethod,
                                         relativePath As String,
                                         payload As Object,
                                         cancellationToken As Threading.CancellationToken) As Task(Of HttpResponseMessage)
        Return Await Http.SendAsync(CreateJsonRequest(method, relativePath, payload), cancellationToken)
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
                    item("content") = If(msg.Content, "")
                    If msg.ToolCalls IsNot Nothing AndAlso msg.ToolCalls.Count > 0 Then
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
                AddStringArrayProperty(item, "supported_reasoning_efforts", model.ReasoningEfforts)
                AddStringArrayProperty(item, "reasoning_efforts", model.ReasoningEfforts)
                AddStringArrayProperty(item, "reasoning", model.ReasoningEfforts)
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

        Dim details As JsonElement
        If usage.TryGetProperty("prompt_tokens_details", details) AndAlso details.ValueKind = JsonValueKind.Object Then
            result.CachedTokens = GetInt(details, "cached_tokens")
        End If
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

Public Class AgentCapabilityCache
    Private Shared ReadOnly SyncRoot As New Object
    Private Shared ReadOnly DefaultReasoningEfforts As String() = {"low", "medium", "high"}
    Private Shared ReadOnly CacheDirectory As String = Path.Combine(Application.StartupPath, "Agent")
    Private Shared ReadOnly ReasoningEffortCachePath As String = Path.Combine(CacheDirectory, "ReasoningEfforts.cache.json")
    Private Shared _cacheFile As AgentReasoningEffortCacheFile = Nothing
    Private Shared _cacheLoaded As Boolean = False

    Private Class AgentReasoningEffortCacheFile
        Public Property Version As Integer = 1
        Public Property Entries As New List(Of AgentReasoningEffortCacheEntry)
        Public Property EndpointRefreshes As New List(Of AgentReasoningEffortEndpointRefresh)
    End Class

    Private Class AgentReasoningEffortCacheEntry
        Public Property EndpointKey As String = ""
        Public Property ModelId As String = ""
        Public Property ReasoningEfforts As New List(Of String)
        Public Property UpdatedAt As DateTime = DateTime.Now
    End Class

    Private Class AgentReasoningEffortEndpointRefresh
        Public Property EndpointKey As String = ""
        Public Property RefreshedDate As String = ""
        Public Property RefreshedAt As DateTime = DateTime.MinValue
    End Class

    Public Shared Function GetReasoningEffortsAsync(client As AgentEndpointClient,
                                                    model As AgentModelInfo,
                                                    Optional cancellationToken As Threading.CancellationToken = Nothing) As Task(Of List(Of String))
        cancellationToken.ThrowIfCancellationRequested()
        If model Is Nothing OrElse String.IsNullOrWhiteSpace(model.Id) Then Return Task.FromResult(New List(Of String))
        If model.ReasoningEfforts IsNot Nothing AndAlso model.ReasoningEfforts.Count > 0 Then
            Dim efforts = NormalizeReasoningEfforts(model.ReasoningEfforts)
            model.ReasoningEfforts = efforts
            SaveReasoningEfforts(client, model.Id, efforts)
            Return Task.FromResult(efforts)
        End If

        Dim cached = GetCachedReasoningEfforts(client, model.Id)
        If cached.Count > 0 Then
            model.ReasoningEfforts = cached
            Return Task.FromResult(cached)
        End If

        Dim result = DefaultReasoningEfforts.ToList()
        Return Task.FromResult(result)
    End Function

    Public Shared Function BuildEndpointSignature(client As AgentEndpointClient) As String
        If client Is Nothing Then Return ""
        Return String.Join(vbLf, {
            If(client.Endpoint, "").Trim(),
            If(client.ApiKey, "").Trim(),
            If(client.ExtraHeaders Is Nothing, "", String.Join(vbLf, client.ExtraHeaders.OrderBy(Function(x) x.Key).Select(Function(x) x.Key & ":" & x.Value)))
        })
    End Function

    Public Shared Function IsDailyReasoningRefreshDue(client As AgentEndpointClient) As Boolean
        Dim endpointKey = BuildEndpointKey(client)
        If endpointKey = "" Then Return False

        SyncLock SyncRoot
            Dim cacheFile = LoadCacheFile()
            Dim today = GetTodayKey()
            Dim refresh = cacheFile.EndpointRefreshes.FirstOrDefault(Function(x) String.Equals(x.EndpointKey, endpointKey, StringComparison.OrdinalIgnoreCase))
            Return refresh Is Nothing OrElse Not String.Equals(If(refresh.RefreshedDate, ""), today, StringComparison.Ordinal)
        End SyncLock
    End Function

    Public Shared Sub ImportReasoningEfforts(client As AgentEndpointClient, models As IEnumerable(Of AgentModelInfo))
        Dim endpointKey = BuildEndpointKey(client)
        If endpointKey = "" Then Return

        SyncLock SyncRoot
            Dim cacheFile = LoadCacheFile()
            If models IsNot Nothing Then
                For Each model In models
                    If model Is Nothing OrElse String.IsNullOrWhiteSpace(model.Id) Then Continue For
                    Dim efforts = NormalizeReasoningEfforts(model.ReasoningEfforts)
                    If efforts.Count = 0 Then Continue For
                    model.ReasoningEfforts = efforts
                    UpsertReasoningEffortEntry(cacheFile, endpointKey, model.Id, efforts)
                Next
            End If
            MarkEndpointRefreshed(cacheFile, endpointKey)
            SaveCacheFile()
        End SyncLock
    End Sub

    Private Shared Function GetCachedReasoningEfforts(client As AgentEndpointClient, modelId As String) As List(Of String)
        Dim endpointKey = BuildEndpointKey(client)
        If endpointKey = "" OrElse String.IsNullOrWhiteSpace(modelId) Then Return New List(Of String)

        SyncLock SyncRoot
            Dim cacheFile = LoadCacheFile()
            Dim entry = cacheFile.Entries.FirstOrDefault(Function(x) String.Equals(x.EndpointKey, endpointKey, StringComparison.OrdinalIgnoreCase) AndAlso
                                                                    String.Equals(x.ModelId, modelId, StringComparison.OrdinalIgnoreCase))
            If entry Is Nothing Then Return New List(Of String)
            Return NormalizeReasoningEfforts(entry.ReasoningEfforts)
        End SyncLock
    End Function

    Private Shared Sub SaveReasoningEfforts(client As AgentEndpointClient, modelId As String, efforts As IEnumerable(Of String))
        Dim endpointKey = BuildEndpointKey(client)
        Dim normalized = NormalizeReasoningEfforts(efforts)
        If endpointKey = "" OrElse String.IsNullOrWhiteSpace(modelId) OrElse normalized.Count = 0 Then Return

        SyncLock SyncRoot
            Dim cacheFile = LoadCacheFile()
            UpsertReasoningEffortEntry(cacheFile, endpointKey, modelId, normalized)
            SaveCacheFile()
        End SyncLock
    End Sub

    Private Shared Sub UpsertReasoningEffortEntry(cacheFile As AgentReasoningEffortCacheFile,
                                                  endpointKey As String,
                                                  modelId As String,
                                                  efforts As List(Of String))
        If cacheFile.Entries Is Nothing Then cacheFile.Entries = New List(Of AgentReasoningEffortCacheEntry)
        Dim entry = cacheFile.Entries.FirstOrDefault(Function(x) String.Equals(x.EndpointKey, endpointKey, StringComparison.OrdinalIgnoreCase) AndAlso
                                                                String.Equals(x.ModelId, modelId, StringComparison.OrdinalIgnoreCase))
        If entry Is Nothing Then
            entry = New AgentReasoningEffortCacheEntry With {
                .EndpointKey = endpointKey,
                .ModelId = modelId
            }
            cacheFile.Entries.Add(entry)
        End If

        entry.ReasoningEfforts = efforts
        entry.UpdatedAt = DateTime.Now
    End Sub

    Private Shared Sub MarkEndpointRefreshed(cacheFile As AgentReasoningEffortCacheFile, endpointKey As String)
        If cacheFile.EndpointRefreshes Is Nothing Then cacheFile.EndpointRefreshes = New List(Of AgentReasoningEffortEndpointRefresh)
        Dim refresh = cacheFile.EndpointRefreshes.FirstOrDefault(Function(x) String.Equals(x.EndpointKey, endpointKey, StringComparison.OrdinalIgnoreCase))
        If refresh Is Nothing Then
            refresh = New AgentReasoningEffortEndpointRefresh With {.EndpointKey = endpointKey}
            cacheFile.EndpointRefreshes.Add(refresh)
        End If

        refresh.RefreshedDate = GetTodayKey()
        refresh.RefreshedAt = DateTime.Now
    End Sub

    Private Shared Function LoadCacheFile() As AgentReasoningEffortCacheFile
        If _cacheLoaded AndAlso _cacheFile IsNot Nothing Then Return _cacheFile

        Try
            If IO.File.Exists(ReasoningEffortCachePath) Then
                _cacheFile = JsonSerializer.Deserialize(Of AgentReasoningEffortCacheFile)(IO.File.ReadAllText(ReasoningEffortCachePath, Encoding.UTF8), JsonSO)
            End If
        Catch
            _cacheFile = Nothing
        End Try

        If _cacheFile Is Nothing Then _cacheFile = New AgentReasoningEffortCacheFile
        If _cacheFile.Entries Is Nothing Then _cacheFile.Entries = New List(Of AgentReasoningEffortCacheEntry)
        If _cacheFile.EndpointRefreshes Is Nothing Then _cacheFile.EndpointRefreshes = New List(Of AgentReasoningEffortEndpointRefresh)
        _cacheLoaded = True
        Return _cacheFile
    End Function

    Private Shared Sub SaveCacheFile()
        Try
            Directory.CreateDirectory(CacheDirectory)
            IO.File.WriteAllText(ReasoningEffortCachePath, JsonSerializer.Serialize(LoadCacheFile(), JsonSO), Encoding.UTF8)
        Catch
        End Try
    End Sub

    Private Shared Function BuildEndpointKey(client As AgentEndpointClient) As String
        If client Is Nothing OrElse String.IsNullOrWhiteSpace(client.Endpoint) Then Return ""
        Dim signature = BuildEndpointSignature(client)
        If String.IsNullOrWhiteSpace(signature) Then Return ""
        Return Convert.ToHexString(System.Security.Cryptography.SHA256.HashData(Encoding.UTF8.GetBytes(signature)))
    End Function

    Private Shared Function NormalizeReasoningEfforts(efforts As IEnumerable(Of String)) As List(Of String)
        If efforts Is Nothing Then Return New List(Of String)
        Return efforts.
            Where(Function(x) Not String.IsNullOrWhiteSpace(x)).
            Select(Function(x) x.Trim()).
            Distinct(StringComparer.OrdinalIgnoreCase).
            ToList()
    End Function

    Private Shared Function GetTodayKey() As String
        Return DateTime.Today.ToString("yyyy-MM-dd", Globalization.CultureInfo.InvariantCulture)
    End Function
End Class

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

Public Class AgentLocalTools
    Public Const PermissionSafe As Integer = 0
    Public Const PermissionEnvironment As Integer = 1
    Public Const PermissionSystem As Integer = 2

    <CodeAnalysis.SuppressMessage("Performance", "CA1861:不要将常量数组作为参数", Justification:="<挂起>")>
    Public Shared Function BuildToolDefinitions(permissionLevel As Integer, networkMode As Integer) As List(Of Dictionary(Of String, Object))
        Dim tools As New List(Of Dictionary(Of String, Object)) From {
            FunctionTool("get_parameter_panel_state", "读取当前 3FUI 参数面板的结构化预设 JSON 和人类可读总览。", New Dictionary(Of String, Object)),
            FunctionTool("get_parameter_field_info", "按字段名或关键词查询参数面板字段的类型、当前值、候选值和格式规则。填写不熟悉的参数前优先调用，避免猜字段值。", New Dictionary(Of String, Object) From {
            {"fields", New Dictionary(Of String, Object) From {{"type", "array"}, {"items", New Dictionary(Of String, Object) From {{"type", "string"}}}}},
            {"query", New Dictionary(Of String, Object) From {{"type", "string"}, {"description", "可选关键词，用于模糊查找字段名"}}},
            {"include_current_values", New Dictionary(Of String, Object) From {{"type", "boolean"}}}
        }),
            FunctionTool("apply_parameter_panel_patch", "只修改当前 3FUI 参数面板，不会同步编码队列。优先传 changes 对象，键为 预设数据_v6 的属性名；也可传 preset_json 应用完整预设。", New Dictionary(Of String, Object) From {
            {"changes", New Dictionary(Of String, Object) From {{"type", "object"}, {"additionalProperties", True}}},
            {"preset_json", New Dictionary(Of String, Object) From {{"type", "string"}}},
            {"note", New Dictionary(Of String, Object) From {{"type", "string"}}}
        })
        }

        If permissionLevel >= PermissionEnvironment Then
            tools.Add(FunctionTool("get_queue_summary", "读取 3FUI 编码队列概要。", New Dictionary(Of String, Object)))
            tools.Add(FunctionTool("sync_parameter_panel_to_queue", "将当前参数面板预设同步到编码队列中尚未开始的预设任务。只有用户明确要求同步队列时才能调用。", New Dictionary(Of String, Object)))
        End If

        Select Case AgentNetworkMode.Normalize(networkMode)
            Case AgentNetworkMode.Endpoint
                tools.Add(FunctionTool("web_search", "联网搜索。只使用端点原生 web_search_preview，不回退到本地网页请求。", New Dictionary(Of String, Object) From {
                    {"query", New Dictionary(Of String, Object) From {{"type", "string"}, {"description", "要搜索的问题"}}}
                }, {"query"}))
            Case AgentNetworkMode.Local
                tools.Add(FunctionTool("web_search", "联网搜索。使用 3FUI 在本机发起常规网页搜索请求。", New Dictionary(Of String, Object) From {
                    {"query", New Dictionary(Of String, Object) From {{"type", "string"}, {"description", "要搜索的问题"}}}
                }, {"query"}))
                tools.Add(FunctionTool("fetch_url", "读取指定 URL 的网页文本。使用 3FUI 在本机发起 HTTP 请求。", New Dictionary(Of String, Object) From {
                    {"url", New Dictionary(Of String, Object) From {{"type", "string"}}}
                }, {"url"}))
        End Select

        If permissionLevel >= PermissionSystem Then
            tools.Add(FunctionTool("read_local_text_file", "读取本地文本文件。仅系统访问权限可用，文件大小限制 512 KiB。", New Dictionary(Of String, Object) From {
                {"path", New Dictionary(Of String, Object) From {{"type", "string"}}}
            }, {"path"}))
            tools.Add(FunctionTool("list_directory", "列举本地目录。仅系统访问权限可用。", New Dictionary(Of String, Object) From {
                {"path", New Dictionary(Of String, Object) From {{"type", "string"}}}
            }, {"path"}))
            tools.Add(FunctionTool("get_image_info", "读取本地图片的宽高、格式和小图 base64。仅系统访问权限可用。", New Dictionary(Of String, Object) From {
                {"path", New Dictionary(Of String, Object) From {{"type", "string"}}}
            }, {"path"}))
            tools.Add(FunctionTool("run_powershell", "运行一次性 PowerShell 命令。仅系统访问权限可用。执行完成、超时或任务终止时会关闭 PowerShell 进程。", New Dictionary(Of String, Object) From {
                {"command", New Dictionary(Of String, Object) From {{"type", "string"}, {"description", "要执行的 PowerShell 命令"}}},
                {"working_directory", New Dictionary(Of String, Object) From {{"type", "string"}, {"description", "可选工作目录，默认使用程序目录"}}},
                {"timeout_seconds", New Dictionary(Of String, Object) From {{"type", "integer"}, {"description", "可选超时时间，1-300 秒，默认 60 秒"}}}
            }, {"command"}))
        End If

        Return tools
    End Function

    Public Shared Async Function ExecuteAsync(callInfo As AgentToolCallInfo,
                                              permissionLevel As Integer,
                                              networkMode As Integer,
                                              endpointClient As AgentEndpointClient,
                                              modelId As String,
                                              reasoningEffort As String,
                                              Optional cancellationToken As Threading.CancellationToken = Nothing) As Task(Of String)
        Dim args = ParseArguments(callInfo?.Arguments)
        Try
            Select Case callInfo?.Name
                Case "get_parameter_panel_state"
                    Return GetParameterPanelState()
                Case "get_parameter_field_info"
                    Return GetParameterFieldInfo(args)
                Case "apply_parameter_panel_patch"
                    Return ApplyParameterPanelPatch(args)
                Case "get_queue_summary"
                    If permissionLevel < PermissionEnvironment Then Return "权限不足：需要环境控制"
                    Return GetQueueSummary()
                Case "sync_parameter_panel_to_queue"
                    If permissionLevel < PermissionEnvironment Then Return "权限不足：需要环境控制"
                    Return SyncParameterPanelToQueue()
                Case "web_search"
                    If Not AgentNetworkMode.IsEnabled(networkMode) Then Return "联网已禁用"
                    Return Await WebSearchAsync(GetStringArg(args, "query"), networkMode, endpointClient, modelId, reasoningEffort, cancellationToken)
                Case "fetch_url"
                    If Not AgentNetworkMode.IsEnabled(networkMode) Then Return "联网已禁用"
                    If AgentNetworkMode.Normalize(networkMode) <> AgentNetworkMode.Local Then Return "当前联网模式不允许本地网页请求"
                    Return Await FetchUrlAsync(GetStringArg(args, "url"), cancellationToken)
                Case "read_local_text_file"
                    If permissionLevel < PermissionSystem Then Return "权限不足：需要系统访问"
                    Return ReadLocalTextFile(GetStringArg(args, "path"))
                Case "list_directory"
                    If permissionLevel < PermissionSystem Then Return "权限不足：需要系统访问"
                    Return ListDirectory(GetStringArg(args, "path"))
                Case "get_image_info"
                    If permissionLevel < PermissionSystem Then Return "权限不足：需要系统访问"
                    Return GetImageInfo(GetStringArg(args, "path"))
                Case "run_powershell"
                    If permissionLevel < PermissionSystem Then Return "权限不足：需要系统访问"
                    Return Await RunPowerShellAsync(args, cancellationToken)
                Case Else
                    Return $"未知工具：{callInfo?.Name}"
            End Select
        Catch ex As OperationCanceledException When cancellationToken.IsCancellationRequested
            Throw
        Catch ex As Exception
            Return $"工具执行失败：{ex.Message}"
        End Try
    End Function

    Private Shared Function FunctionTool(name As String,
                                         description As String,
                                         properties As Dictionary(Of String, Object),
                                         Optional required As IEnumerable(Of String) = Nothing) As Dictionary(Of String, Object)
        Return New Dictionary(Of String, Object) From {
            {"type", "function"},
            {"function", New Dictionary(Of String, Object) From {
                {"name", name},
                {"description", description},
                {"parameters", New Dictionary(Of String, Object) From {
                    {"type", "object"},
                    {"properties", properties},
                    {"required", If(required?.ToArray(), Array.Empty(Of String)())}
                }}
            }}
        }
    End Function

    Private Shared Function ParseArguments(arguments As String) As JsonElement
        If String.IsNullOrWhiteSpace(arguments) Then arguments = "{}"
        Return JsonDocument.Parse(arguments).RootElement.Clone()
    End Function

    Private Shared Function GetStringArg(args As JsonElement, name As String) As String
        Dim value As JsonElement
        If args.ValueKind = JsonValueKind.Object AndAlso args.TryGetProperty(name, value) AndAlso value.ValueKind = JsonValueKind.String Then Return value.GetString()
        Return ""
    End Function

    Private Shared Function GetIntArg(args As JsonElement, name As String, defaultValue As Integer) As Integer
        Dim value As JsonElement
        If args.ValueKind = JsonValueKind.Object AndAlso args.TryGetProperty(name, value) AndAlso value.ValueKind = JsonValueKind.Number Then
            Dim result As Integer
            If value.TryGetInt32(result) Then Return result
        End If
        Return defaultValue
    End Function

    Private Shared Function GetParameterPanelState() As String
        Dim preset = 预设管理_v6.从面板创建预设(Form_v6_参数面板)
        Dim overview = BuildParameterOverview(preset)
        Dim payload As New Dictionary(Of String, Object) From {
            {"overview", overview},
            {"preset_json", JsonSerializer.Serialize(preset, JsonSO)}
        }
        Return JsonSerializer.Serialize(payload, JsonSO)
    End Function

    Private Shared Function ApplyParameterPanelPatch(args As JsonElement) As String
        Dim presetJson As JsonElement
        Dim preset As 预设数据_v6
        If args.ValueKind = JsonValueKind.Object AndAlso args.TryGetProperty("preset_json", presetJson) AndAlso presetJson.ValueKind = JsonValueKind.String AndAlso presetJson.GetString() <> "" Then
            preset = JsonSerializer.Deserialize(Of 预设数据_v6)(presetJson.GetString(), JsonSO)
        Else
            preset = 预设管理_v6.从面板创建预设(Form_v6_参数面板)
            Dim changes As JsonElement
            If args.ValueKind = JsonValueKind.Object AndAlso args.TryGetProperty("changes", changes) AndAlso changes.ValueKind = JsonValueKind.Object Then
                ApplyTopLevelChanges(preset, changes)
            Else
                Return "没有提供 changes 或 preset_json"
            End If
        End If

        预设管理_v6.显示预设(preset, Form_v6_参数面板)
        Return "已应用参数面板修改" & vbCrLf & BuildParameterOverview(preset)
    End Function

    Private Shared Function SyncParameterPanelToQueue() As String
        Dim preset = 预设管理_v6.从面板创建预设(Form_v6_参数面板)
        Dim queueSync = 编码队列_v6.同步未处理预设任务(preset)
        Return BuildQueueSyncSummary(queueSync)
    End Function

    Private Shared Function BuildQueueSyncSummary(result As 编码队列_v6.预设同步结果) As String
        If result Is Nothing Then Return "编码队列同步：未更新任务"
        Dim parts As New List(Of String) From {$"已更新 {result.已更新} 个未处理预设任务"}
        If result.已跳过非预设任务 > 0 Then parts.Add($"跳过 {result.已跳过非预设任务} 个命令行任务")
        If result.已跳过不可修改任务 > 0 Then parts.Add($"跳过 {result.已跳过不可修改任务} 个已开始或已结束任务")
        Return "编码队列同步：" & String.Join("，", parts)
    End Function

    Private Shared Function GetParameterFieldInfo(args As JsonElement) As String
        Dim preset = 预设管理_v6.从面板创建预设(Form_v6_参数面板)
        Dim includeCurrentValues = GetBooleanArg(args, "include_current_values", True)
        Dim requestedFields = GetStringArrayArg(args, "fields")
        Dim query = GetStringArg(args, "query").Trim()
        Dim props = GetType(预设数据_v6).GetProperties().
            Where(Function(x) x.CanRead AndAlso x.CanWrite).
            OrderBy(Function(x) x.Name, StringComparer.CurrentCultureIgnoreCase).
            ToList()

        Dim candidateProps As New List(Of Reflection.PropertyInfo)
        Dim matchedProps As New List(Of Reflection.PropertyInfo)
        Dim missingFields As New List(Of String)
        If requestedFields.Count > 0 Then
            For Each field In requestedFields
                Dim prop = props.FirstOrDefault(Function(x) String.Equals(x.Name, field, StringComparison.OrdinalIgnoreCase))
                If prop Is Nothing Then
                    missingFields.Add(field)
                ElseIf Not matchedProps.Contains(prop) Then
                    matchedProps.Add(prop)
                End If
            Next
        ElseIf query <> "" Then
            candidateProps = props.
                Where(Function(x) x.Name.Contains(query, StringComparison.OrdinalIgnoreCase)).
                ToList()
            matchedProps = candidateProps.Take(30).ToList()
        Else
            candidateProps = props
            matchedProps = candidateProps.Take(30).ToList()
        End If

        Dim items = matchedProps.Select(Function(prop) BuildParameterFieldInfoItem(prop, preset, includeCurrentValues)).ToList()
        Dim payload As New Dictionary(Of String, Object) From {
            {"fields", items},
            {"missing_fields", missingFields},
            {"truncated", requestedFields.Count = 0 AndAlso candidateProps.Count > matchedProps.Count},
            {"hint", "字段值必须传给 apply_parameter_panel_patch 的 changes；本工具只查询候选和规则，不修改参数面板。"}
        }
        Return JsonSerializer.Serialize(payload, JsonSO)
    End Function

    Private Shared Function GetBooleanArg(args As JsonElement, name As String, defaultValue As Boolean) As Boolean
        Dim value As JsonElement
        If args.ValueKind = JsonValueKind.Object AndAlso args.TryGetProperty(name, value) Then
            Select Case value.ValueKind
                Case JsonValueKind.True
                    Return True
                Case JsonValueKind.False
                    Return False
            End Select
        End If
        Return defaultValue
    End Function

    Private Shared Function GetStringArrayArg(args As JsonElement, name As String) As List(Of String)
        Dim result As New List(Of String)
        Dim value As JsonElement
        If args.ValueKind <> JsonValueKind.Object OrElse Not args.TryGetProperty(name, value) Then Return result
        If value.ValueKind <> JsonValueKind.Array Then Return result

        For Each item In value.EnumerateArray()
            If item.ValueKind <> JsonValueKind.String Then Continue For
            Dim text = If(item.GetString(), "").Trim()
            If text <> "" AndAlso Not result.Contains(text, StringComparer.OrdinalIgnoreCase) Then result.Add(text)
        Next
        Return result
    End Function

    Private Shared Function BuildParameterFieldInfoItem(prop As Reflection.PropertyInfo, preset As 预设数据_v6, includeCurrentValue As Boolean) As Dictionary(Of String, Object)
        Dim info As New Dictionary(Of String, Object) From {
            {"name", prop.Name},
            {"type", GetFriendlyTypeName(prop.PropertyType)}
        }

        If includeCurrentValue Then
            info("current_value") = prop.GetValue(preset)
        End If

        Dim enumValues = GetEnumValues(prop.PropertyType)
        If enumValues.Count > 0 Then info("enum_values") = enumValues

        If prop.PropertyType Is GetType(Boolean) Then
            info("candidates") = New String() {"true", "false"}
        End If

        Dim candidates = GetKnownParameterCandidates(prop.Name, preset)
        If candidates.Count > 0 Then info("candidates") = candidates

        Dim rules = GetKnownParameterRules(prop.Name)
        If rules.Count > 0 Then info("rules") = rules

        Dim notes = GetKnownParameterNotes(prop.Name, preset)
        If notes.Count > 0 Then info("notes") = notes

        Return info
    End Function

    Private Shared Function GetFriendlyTypeName(type As Type) As String
        If type Is GetType(String) Then Return "string"
        If type Is GetType(Boolean) Then Return "boolean"
        If type Is GetType(Integer) Then Return "integer"
        If type Is GetType(Double) Then Return "number"
        If type.IsEnum Then Return "enum:" & type.Name
        If type.IsArray Then Return "array:" & GetFriendlyTypeName(type.GetElementType())
        If type.IsGenericType AndAlso type.GetGenericTypeDefinition() Is GetType(List(Of )) Then Return "array:" & GetFriendlyTypeName(type.GetGenericArguments()(0))
        Return type.Name
    End Function

    Private Shared Function GetEnumValues(type As Type) As List(Of Dictionary(Of String, Object))
        Dim result As New List(Of Dictionary(Of String, Object))
        If Not type.IsEnum Then Return result

        For Each value In [Enum].GetValues(type)
            result.Add(New Dictionary(Of String, Object) From {
                {"name", [Enum].GetName(type, value)},
                {"value", CInt(value)}
            })
        Next
        Return result
    End Function

    Private Shared Function GetKnownParameterCandidates(fieldName As String, preset As 预设数据_v6) As List(Of Object)
        Select Case fieldName
            Case NameOf(预设数据_v6.输出容器)
                Return {".mp4", ".mkv", ".mov", ".webm", ".flv", ".avi", ".mp3", ".m4a", ".opus", ".flac", ".wav", ".png", ".jpg", ".webp", ".gif"}.Cast(Of Object).ToList()
            Case NameOf(预设数据_v6.视频参数_编码器_分类名称)
                Return 视频编码器数据库_v6.获取分类列表(preset.视频参数_编码器_类型).
                    Select(Function(x) CType(New Dictionary(Of String, Object) From {
                        {"value", x.名称},
                        {"description", x.描述}
                    }, Object)).
                    ToList()
            Case NameOf(预设数据_v6.视频参数_编码器_具体编码)
                Return 视频编码器数据库_v6.获取编码器列表(preset.视频参数_编码器_分类名称).
                    Select(Function(x) CType(New Dictionary(Of String, Object) From {
                        {"value", x.名称},
                        {"command", x.命令行编码器名},
                        {"type", x.类型.ToString()}
                    }, Object)).
                    ToList()
            Case NameOf(预设数据_v6.视频参数_编码器_编码预设)
                Return BuildVideoParameterListCandidates(preset, 视频编码器数据库_v6.编码器参数角色.编码预设)
            Case NameOf(预设数据_v6.视频参数_编码器_配置文件)
                Return BuildVideoParameterListCandidates(preset, 视频编码器数据库_v6.编码器参数角色.配置文件)
            Case NameOf(预设数据_v6.视频参数_编码器_场景优化)
                Return BuildVideoParameterListCandidates(preset, 视频编码器数据库_v6.编码器参数角色.场景优化)
            Case NameOf(预设数据_v6.视频参数_色彩管理_像素格式)
                Return BuildVideoParameterListCandidates(preset, 视频编码器数据库_v6.编码器参数角色.像素格式)
            Case NameOf(预设数据_v6.视频参数_色彩管理_像素格式预先转换)
                Return {"", "yuv420p", "yuv420p10le", "yuv422p", "yuv422p10le", "yuv444p", "yuv444p10le", "p010le"}.Cast(Of Object).ToList()
            Case NameOf(预设数据_v6.视频参数_质量控制_参数名)
                Return 视频编码器数据库_v6.获取质量参数名列表().Cast(Of Object).ToList()
            Case NameOf(预设数据_v6.音频参数_编码器_代号)
                Return 音频编码器数据库_v6.全部编码器.
                    Select(Function(x) CType(New Dictionary(Of String, Object) From {
                        {"value", x.私有ID},
                        {"label", x.显示名称},
                        {"command", x.命令行编码器名}
                    }, Object)).
                    ToList()
            Case NameOf(预设数据_v6.音频参数_质量参数名)
                Return 音频编码器数据库_v6.获取质量参数名列表().Cast(Of Object).ToList()
        End Select
        Return New List(Of Object)
    End Function

    Private Shared Function BuildVideoParameterListCandidates(preset As 预设数据_v6, role As 视频编码器数据库_v6.编码器参数角色) As List(Of Object)
        Dim encoder = 视频编码器数据库_v6.获取编码器数据(preset.视频参数_编码器_具体编码)
        Dim data As 视频编码器数据库_v6.编码器参数列表数据 = Nothing
        If encoder Is Nothing Then Return New List(Of Object)

        Select Case role
            Case 视频编码器数据库_v6.编码器参数角色.编码预设
                data = encoder.编码预设
            Case 视频编码器数据库_v6.编码器参数角色.配置文件
                data = encoder.配置文件
            Case 视频编码器数据库_v6.编码器参数角色.场景优化
                data = encoder.场景优化
            Case 视频编码器数据库_v6.编码器参数角色.像素格式
                data = encoder.像素格式
        End Select

        If data Is Nothing Then Return New List(Of Object)
        Dim result As New List(Of Object)
        result.Add("")
        If data.默认值 <> "" Then
            result.Add(New Dictionary(Of String, Object) From {
                {"value", data.默认值},
                {"is_default", True}
            })
        End If
        For Each value In data.值列表
            Dim candidate As New Dictionary(Of String, Object) From {
                {"value", value}
            }
            Dim description As String = Nothing
            If data.值说明 IsNot Nothing AndAlso data.值说明.TryGetValue(value, description) AndAlso description <> "" Then candidate("description") = description
            If Not result.OfType(Of Dictionary(Of String, Object)).Any(Function(x) String.Equals(CStr(x("value")), value, StringComparison.OrdinalIgnoreCase)) Then result.Add(candidate)
        Next
        Return result
    End Function

    Private Shared Function GetKnownParameterRules(fieldName As String) As List(Of String)
        Select Case fieldName
            Case NameOf(预设数据_v6.输出容器)
                Return New List(Of String) From {"后缀必须包含点号，例如 .mp4；空字符串表示不指定或由输出路径决定。"}
            Case NameOf(预设数据_v6.视频参数_质量控制_参数名)
                Return New List(Of String) From {"面板显示带横杠的 FFmpeg 参数名；保存预设时会自动去掉开头横杠，changes 中可传 crf 或 -crf。"}
            Case NameOf(预设数据_v6.音频参数_编码器_代号)
                Return New List(Of String) From {"changes 中优先传 value 的私有 ID，不要传显示名称；界面会自动显示对应名称。"}
            Case NameOf(预设数据_v6.音频参数_质量参数名)
                Return New List(Of String) From {"音频质量参数名保存时保留横杠，例如 -q:a、-b:a。"}
        End Select
        Return New List(Of String)
    End Function

    Private Shared Function GetKnownParameterNotes(fieldName As String, preset As 预设数据_v6) As List(Of String)
        Dim notes As New List(Of String)
        Select Case fieldName
            Case NameOf(预设数据_v6.视频参数_编码器_分类名称)
                notes.Add($"候选取决于 {NameOf(预设数据_v6.视频参数_编码器_类型)}，当前为 {preset.视频参数_编码器_类型}。")
            Case NameOf(预设数据_v6.视频参数_编码器_具体编码)
                notes.Add($"候选取决于 {NameOf(预设数据_v6.视频参数_编码器_分类名称)}，当前为 {preset.视频参数_编码器_分类名称}。")
            Case NameOf(预设数据_v6.视频参数_编码器_编码预设),
                 NameOf(预设数据_v6.视频参数_编码器_配置文件),
                 NameOf(预设数据_v6.视频参数_编码器_场景优化),
                 NameOf(预设数据_v6.视频参数_色彩管理_像素格式)
                notes.Add($"候选取决于 {NameOf(预设数据_v6.视频参数_编码器_具体编码)}，当前为 {preset.视频参数_编码器_具体编码}。")
        End Select
        Return notes
    End Function

    Private Shared Sub ApplyTopLevelChanges(preset As 预设数据_v6, changes As JsonElement)
        Dim type = GetType(预设数据_v6)
        For Each item In changes.EnumerateObject()
            Dim prop = type.GetProperty(item.Name)
            If prop Is Nothing OrElse Not prop.CanWrite Then Throw New InvalidOperationException($"未知或不可写属性：{item.Name}")
            Dim value = DeserializeJsonElement(item.Value, prop.PropertyType)
            prop.SetValue(preset, value)
        Next
    End Sub

    Private Shared Function DeserializeJsonElement(element As JsonElement, targetType As Type) As Object
        If targetType Is GetType(String) Then
            If element.ValueKind = JsonValueKind.String Then Return element.GetString()
            Return element.GetRawText()
        End If
        If targetType Is GetType(Boolean) Then Return element.GetBoolean()
        If targetType Is GetType(Integer) Then Return element.GetInt32()
        If targetType.IsEnum Then
            If element.ValueKind = JsonValueKind.String Then Return [Enum].Parse(targetType, element.GetString(), True)
            Return [Enum].ToObject(targetType, element.GetInt32())
        End If
        Return JsonSerializer.Deserialize(element.GetRawText(), targetType, JsonSO)
    End Function

    Private Shared Function BuildParameterOverview(preset As 预设数据_v6) As String
        Using box As New LakeUI.ModernTextBox
            预设管理_v6.显示参数总览(box, preset)
            Return box.Text
        End Using
    End Function

    Private Shared Function GetQueueSummary() As String
        Dim snapshot = 编码队列_v6.获取队列快照()
        Dim items = snapshot.Select(Function(t) New Dictionary(Of String, Object) From {
            {"id", t.ID},
            {"name", t.任务名称},
            {"input", t.输入文件},
            {"output", t.输出文件},
            {"status", t.状态.ToString()},
            {"progress", If(t.进度 Is Nothing, "", t.进度.进度文本)}
        }).ToList()
        Return JsonSerializer.Serialize(items, JsonSO)
    End Function

    Private Shared Async Function WebSearchAsync(query As String,
                                                 networkMode As Integer,
                                                 endpointClient As AgentEndpointClient,
                                                 modelId As String,
                                                 reasoningEffort As String,
                                                 cancellationToken As Threading.CancellationToken) As Task(Of String)
        If String.IsNullOrWhiteSpace(query) Then Return "缺少 query"

        Select Case AgentNetworkMode.Normalize(networkMode)
            Case AgentNetworkMode.Endpoint
                If endpointClient Is Nothing OrElse String.IsNullOrWhiteSpace(modelId) Then Return "端点联网不可用：缺少端点或模型"

                Dim response = Await endpointClient.TryCreateResponsesWebSearchAsync(modelId, query, reasoningEffort, cancellationToken)
                If response.Success Then
                    If Not String.IsNullOrWhiteSpace(response.Content) Then Return response.Content
                    Return "端点联网没有返回内容"
                End If
                Return "端点联网失败：" & response.ErrorMessage

            Case AgentNetworkMode.Local
                Return Await LocalWebSearchAsync(query, cancellationToken)

            Case Else
                Return "联网已禁用。"
        End Select
    End Function

    Private Shared Async Function LocalWebSearchAsync(query As String,
                                                      cancellationToken As Threading.CancellationToken) As Task(Of String)
        Dim encodedQuery = Uri.EscapeDataString(query)
        Dim urls = {
            "https://cn.bing.com/search?q=" & encodedQuery & "&setlang=zh-CN&mkt=zh-CN",
            "https://www.bing.com/search?q=" & encodedQuery & "&setlang=zh-CN&mkt=zh-CN"
        }
        Dim errors As New List(Of String)

        For Each url In urls
            cancellationToken.ThrowIfCancellationRequested()
            Dim host = New Uri(url).Host
            Try
                Dim result = Await FetchUrlAsync(url, cancellationToken)
                If Not IsFetchFailure(result) Then Return $"搜索来源：{host}{vbCrLf}{result}"
                errors.Add(host & "：" & LimitToolText(result, 300))
            Catch ex As OperationCanceledException When cancellationToken.IsCancellationRequested
                Throw
            Catch ex As Exception
                errors.Add(host & "：" & ex.Message)
            End Try
        Next

        Return "本地联网搜索失败：" & String.Join("；", errors)
    End Function

    Private Shared Function IsFetchFailure(text As String) As Boolean
        text = If(text, "").Trim()
        Return text = "" OrElse
            text.StartsWith("请求失败", StringComparison.Ordinal) OrElse
            text.StartsWith("读取失败", StringComparison.Ordinal) OrElse
            text.StartsWith("URL 无效", StringComparison.Ordinal) OrElse
            text.StartsWith("缺少 url", StringComparison.Ordinal)
    End Function

    Private Shared Async Function FetchUrlAsync(url As String, cancellationToken As Threading.CancellationToken) As Task(Of String)
        If String.IsNullOrWhiteSpace(url) Then Return "缺少 url"
        Dim uri As Uri = Nothing
        If Not Uri.TryCreate(url, UriKind.Absolute, uri) Then Return "URL 无效"
        Using http As New HttpClient With {.Timeout = TimeSpan.FromSeconds(45)}
            Using request As New HttpRequestMessage(HttpMethod.Get, uri)
                request.Headers.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/125.0 Safari/537.36")
                request.Headers.Accept.ParseAdd("text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8")
                request.Headers.AcceptLanguage.ParseAdd("zh-CN,zh;q=0.9,en;q=0.8")

                Using response = Await http.SendAsync(request, cancellationToken)
                    If Not response.IsSuccessStatusCode Then Return $"请求失败：HTTP {CInt(response.StatusCode)} {response.ReasonPhrase}"
                    Dim html = Await response.Content.ReadAsStringAsync(cancellationToken)
                    html = Regex.Replace(html, "<script[\s\S]*?</script>", "", RegexOptions.IgnoreCase)
                    html = Regex.Replace(html, "<style[\s\S]*?</style>", "", RegexOptions.IgnoreCase)
                    html = Regex.Replace(html, "<[^>]+>", " ")
                    html = WebUtility.HtmlDecode(html)
                    html = Regex.Replace(html, "\s+", " ").Trim()
                    If html.Length > 12000 Then html = html.Substring(0, 12000)
                    Return html
                End Using
            End Using
        End Using
    End Function

    Private Shared Function ReadLocalTextFile(path As String) As String
        If String.IsNullOrWhiteSpace(path) Then Return "缺少 path"
        If Not File.Exists(path) Then Return "文件不存在"
        Dim info As New FileInfo(path)
        If info.Length > 512 * 1024 Then Return "文件超过 512 KiB 限制"
        Dim bytes = File.ReadAllBytes(path)
        Dim text As String
        If bytes.Length >= 2 AndAlso bytes(0) = &HFF AndAlso bytes(1) = &HFE Then
            text = Encoding.Unicode.GetString(bytes)
        ElseIf bytes.Length >= 2 AndAlso bytes(0) = &HFE AndAlso bytes(1) = &HFF Then
            text = Encoding.BigEndianUnicode.GetString(bytes)
        Else
            text = Encoding.UTF8.GetString(bytes)
        End If
        If text.Length > 20000 Then text = text.Substring(0, 20000)
        Return text
    End Function

    Private Shared Function ListDirectory(path As String) As String
        If String.IsNullOrWhiteSpace(path) Then Return "缺少 path"
        If Not Directory.Exists(path) Then Return "目录不存在"
        Dim dir As New DirectoryInfo(path)
        Dim items = dir.EnumerateFileSystemInfos().
            OrderByDescending(Function(x) TypeOf x Is DirectoryInfo).
            ThenBy(Function(x) x.Name, StringComparer.CurrentCultureIgnoreCase).
            Take(200).
            Select(Function(x) New Dictionary(Of String, Object) From {
                {"name", x.Name},
                {"path", x.FullName},
                {"type", If(TypeOf x Is DirectoryInfo, "directory", "file")},
                {"size", If(TypeOf x Is FileInfo, DirectCast(x, FileInfo).Length, 0)}
            }).ToList()
        Return JsonSerializer.Serialize(items, JsonSO)
    End Function

    Private Shared Function GetImageInfo(path As String) As String
        If String.IsNullOrWhiteSpace(path) Then Return "缺少 path"
        If Not File.Exists(path) Then Return "文件不存在"
        Using img = Image.FromFile(path)
            Dim info As New FileInfo(path)
            Dim payload As New Dictionary(Of String, Object) From {
                {"path", path},
                {"width", img.Width},
                {"height", img.Height},
                {"format", ImageFormatName(img.RawFormat)},
                {"size", info.Length}
            }
            If info.Length <= 1024 * 1024 Then
                payload("base64") = Convert.ToBase64String(File.ReadAllBytes(path))
            End If
            Return JsonSerializer.Serialize(payload, JsonSO)
        End Using
    End Function

    Private Shared Async Function RunPowerShellAsync(args As JsonElement,
                                                     cancellationToken As Threading.CancellationToken) As Task(Of String)
        Dim command = GetStringArg(args, "command").Trim()
        If command = "" Then Return "缺少 command。"

        Dim workingDirectory = GetStringArg(args, "working_directory").Trim()
        If workingDirectory = "" Then workingDirectory = Application.StartupPath
        If Not Directory.Exists(workingDirectory) Then Return "目录不存在：" & workingDirectory

        Dim timeoutSeconds = GetIntArg(args, "timeout_seconds", 60)
        timeoutSeconds = Math.Min(Math.Max(timeoutSeconds, 1), 300)

        Dim script = "[Console]::OutputEncoding=[System.Text.Encoding]::UTF8; $OutputEncoding=[System.Text.Encoding]::UTF8; " & command
        Dim encodedCommand = Convert.ToBase64String(Encoding.Unicode.GetBytes(script))
        Dim sw As System.Diagnostics.Stopwatch = System.Diagnostics.Stopwatch.StartNew()
        Dim stdout As String = ""
        Dim stderr As String = ""
        Dim exitCode As Integer = -1
        Dim timedOut As Boolean = False

        Using linkedCts = Threading.CancellationTokenSource.CreateLinkedTokenSource(cancellationToken)
            linkedCts.CancelAfter(TimeSpan.FromSeconds(timeoutSeconds))

            Using process As New Process()
                Dim started As Boolean = False
                Try
                    process.StartInfo = New ProcessStartInfo With {
                        .FileName = "powershell.exe",
                        .WorkingDirectory = workingDirectory,
                        .UseShellExecute = False,
                        .RedirectStandardOutput = True,
                        .RedirectStandardError = True,
                        .CreateNoWindow = True,
                        .StandardOutputEncoding = Encoding.UTF8,
                        .StandardErrorEncoding = Encoding.UTF8
                    }
                    process.StartInfo.ArgumentList.Add("-NoLogo")
                    process.StartInfo.ArgumentList.Add("-NoProfile")
                    process.StartInfo.ArgumentList.Add("-ExecutionPolicy")
                    process.StartInfo.ArgumentList.Add("Bypass")
                    process.StartInfo.ArgumentList.Add("-EncodedCommand")
                    process.StartInfo.ArgumentList.Add(encodedCommand)
                    process.StartInfo.EnvironmentVariables("POWERSHELL_TELEMETRY_OPTOUT") = "1"

                    started = process.Start()
                    If Not started Then Return "PowerShell 启动失败。"

                    Dim stdoutTask = process.StandardOutput.ReadToEndAsync(cancellationToken)
                    Dim stderrTask = process.StandardError.ReadToEndAsync(cancellationToken)
                    Dim waitAfterKill As Boolean = False
                    Dim throwCancellation As Boolean = False

                    Try
                        Await process.WaitForExitAsync(linkedCts.Token)
                    Catch ex As OperationCanceledException
                        If Not process.HasExited Then
                            Try
                                process.Kill(True)
                            Catch
                            End Try
                        End If
                        waitAfterKill = True
                        throwCancellation = cancellationToken.IsCancellationRequested
                        timedOut = Not throwCancellation
                    End Try

                    If waitAfterKill Then
                        Try
                            Await process.WaitForExitAsync(Threading.CancellationToken.None)
                        Catch
                        End Try
                        If throwCancellation Then Throw New OperationCanceledException(cancellationToken)
                    End If

                    stdout = Await stdoutTask
                    stderr = Await stderrTask
                    If process.HasExited Then exitCode = process.ExitCode
                Finally
                    If started AndAlso Not process.HasExited Then
                        Try
                            process.Kill(True)
                        Catch
                        End Try
                    End If
                End Try
            End Using
        End Using

        sw.Stop()
        stdout = LimitToolText(stdout, 12000)
        stderr = LimitToolText(stderr, 12000)

        Dim payload As New Dictionary(Of String, Object) From {
            {"command", command},
            {"working_directory", workingDirectory},
            {"timeout_seconds", timeoutSeconds},
            {"timed_out", timedOut},
            {"exit_code", exitCode},
            {"elapsed_ms", CLng(sw.Elapsed.TotalMilliseconds)},
            {"stdout", stdout},
            {"stderr", stderr}
        }
        Return JsonSerializer.Serialize(payload, JsonSO)
    End Function

    Private Shared Function LimitToolText(text As String, maxLength As Integer) As String
        text = If(text, "")
        If text.Length <= maxLength Then Return text
        Return String.Concat(text.AsSpan(0, maxLength), "...[已截断]")
    End Function

    Private Shared Function ImageFormatName(format As ImageFormat) As String
        If format.Guid = ImageFormat.Png.Guid Then Return "png"
        If format.Guid = ImageFormat.Jpeg.Guid Then Return "jpeg"
        If format.Guid = ImageFormat.Gif.Guid Then Return "gif"
        If format.Guid = ImageFormat.Bmp.Guid Then Return "bmp"
        Return format.ToString()
    End Function
End Class
