Imports System.Drawing.Imaging
Imports System.IO
Imports System.Net
Imports System.Net.Http
Imports System.Text
Imports System.Text.Json
Imports System.Text.RegularExpressions

Partial Public Class AgentLocalTools
    Private Shared Async Function WebSearchAsync(query As String,
                                                 engineUrl As String,
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
                Return Await LocalWebSearchAsync(query, engineUrl, cancellationToken)

            Case Else
                Return "联网已禁用。"
        End Select
    End Function

    Private Shared Async Function LocalWebSearchAsync(query As String,
                                                      engineUrl As String,
                                                      cancellationToken As Threading.CancellationToken) As Task(Of String)
        Dim url = BuildSearchUrl(query, engineUrl)
        If url = "" Then Return "缺少 engine_url。请提供包含查询词的完整搜索 URL，或使用 {query} 占位符。"

        Dim uri As Uri = Nothing
        If Not Uri.TryCreate(url, UriKind.Absolute, uri) OrElse
           (Not String.Equals(uri.Scheme, Uri.UriSchemeHttp, StringComparison.OrdinalIgnoreCase) AndAlso
            Not String.Equals(uri.Scheme, Uri.UriSchemeHttps, StringComparison.OrdinalIgnoreCase)) Then
            Return "搜索引擎地址无效：必须是 HTTP 或 HTTPS 的完整 URL"
        End If

        cancellationToken.ThrowIfCancellationRequested()
        Dim result = Await FetchUrlAsync(uri.AbsoluteUri, cancellationToken)
        If Not IsFetchFailure(result) Then Return $"搜索来源：{uri.Host}{vbCrLf}{result}"
        Return $"本地联网搜索失败：{uri.Host}：{Agent通用工具_v6.LimitText(result, 300)}"
    End Function

    Private Shared Function BuildSearchUrl(query As String, engineUrl As String) As String
        Dim url = If(engineUrl, "").Trim()
        If url = "" Then Return ""
        Return url.Replace("{query}", Uri.EscapeDataString(query), StringComparison.Ordinal)
    End Function

    Private Shared Function IsFetchFailure(text As String) As Boolean
        text = If(text, "").Trim()
        Return text = "" OrElse
            text.StartsWith("请求失败", StringComparison.Ordinal) OrElse
            text.StartsWith("读取失败", StringComparison.Ordinal) OrElse
            text.StartsWith("URL 无效", StringComparison.Ordinal) OrElse
            text.StartsWith("缺少 url", StringComparison.Ordinal)
    End Function

    Private Shared Function ReadHeaderMap(args As JsonElement) As Dictionary(Of String, String)
        Dim result As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)
        Dim headers = Agent通用工具_v6.GetJsonObject(args, "headers")
        If headers.ValueKind <> JsonValueKind.Object Then Return result
        For Each prop In headers.EnumerateObject()
            If prop.Value.ValueKind = JsonValueKind.String Then
                Dim value = If(prop.Value.GetString(), "").Trim()
                If prop.Name.Trim() <> "" AndAlso value <> "" Then result(prop.Name.Trim()) = value
            End If
        Next
        Return result
    End Function

    Private Shared Async Function FetchUrlAsync(url As String,
                                                cancellationToken As Threading.CancellationToken,
                                                Optional methodName As String = "GET",
                                                Optional headers As Dictionary(Of String, String) = Nothing,
                                                Optional userAgent As String = "",
                                                Optional referer As String = "",
                                                Optional cookies As String = "",
                                                Optional body As String = "",
                                                Optional maxChars As Integer = 12000,
                                                Optional responseFormat As String = "auto",
                                                Optional includeMetadata As Boolean = False) As Task(Of String)
        If String.IsNullOrWhiteSpace(url) Then Return "缺少 url"
        Dim uri As Uri = Nothing
        If Not Uri.TryCreate(url, UriKind.Absolute, uri) OrElse
           (Not String.Equals(uri.Scheme, Uri.UriSchemeHttp, StringComparison.OrdinalIgnoreCase) AndAlso
            Not String.Equals(uri.Scheme, Uri.UriSchemeHttps, StringComparison.OrdinalIgnoreCase)) Then Return "URL 无效"
        Dim normalizedMethod = If(methodName, "GET").Trim().ToUpperInvariant()
        If normalizedMethod = "" Then normalizedMethod = "GET"
        Dim safeMaxChars = Math.Min(Math.Max(If(maxChars <= 0, 12000, maxChars), 1), 200000)
        Using http As New HttpClient With {.Timeout = TimeSpan.FromSeconds(45)},
              deadline = Threading.CancellationTokenSource.CreateLinkedTokenSource(cancellationToken)
            deadline.CancelAfter(http.Timeout)
            Dim requestMethod As New HttpMethod(normalizedMethod)
            Using request As New HttpRequestMessage(requestMethod, uri)
                Dim defaultAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/148.0.0.0 Safari/537.36 Edg/148.0.0.0"
                request.Headers.UserAgent.ParseAdd(If(String.IsNullOrWhiteSpace(userAgent), defaultAgent, userAgent.Trim()))
                request.Headers.Accept.ParseAdd("text/html,application/xhtml+xml,application/json,application/xml,text/plain;q=0.9,*/*;q=0.8")
                request.Headers.AcceptLanguage.ParseAdd("zh-CN,zh;q=0.9,en;q=0.8")
                If Not String.IsNullOrWhiteSpace(referer) Then
                    Dim refererUri As Uri = Nothing
                    If Uri.TryCreate(referer.Trim(), UriKind.Absolute, refererUri) Then request.Headers.Referrer = refererUri
                End If
                If Not String.IsNullOrWhiteSpace(cookies) Then request.Headers.TryAddWithoutValidation("Cookie", cookies.Trim())
                If headers IsNot Nothing Then
                    For Each pair In headers
                        If String.Equals(pair.Key, "Content-Type", StringComparison.OrdinalIgnoreCase) Then Continue For
                        request.Headers.TryAddWithoutValidation(pair.Key, pair.Value)
                    Next
                End If
                If Not String.Equals(normalizedMethod, "GET", StringComparison.OrdinalIgnoreCase) AndAlso
                   Not String.Equals(normalizedMethod, "HEAD", StringComparison.OrdinalIgnoreCase) AndAlso
                   body IsNot Nothing AndAlso body <> "" Then
                    Dim contentType = "application/json"
                    If headers IsNot Nothing AndAlso headers.ContainsKey("Content-Type") Then contentType = headers("Content-Type")
                    request.Content = New StringContent(body, Encoding.UTF8, contentType)
                End If

                Using response = Await http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, deadline.Token)
                    Await response.Content.LoadIntoBufferAsync(8L * 1024 * 1024, deadline.Token)
                    Dim responseText = Await response.Content.ReadAsStringAsync(deadline.Token)
                    Dim contentType = If(response.Content.Headers.ContentType?.MediaType, "")
                    Dim format = If(responseFormat, "auto").Trim().ToLowerInvariant()
                    If format = "auto" Then
                        Dim looksLikeHtml = contentType.Contains("html", StringComparison.OrdinalIgnoreCase) OrElse
                                            responseText.Contains("<html", StringComparison.OrdinalIgnoreCase) OrElse
                                            responseText.Contains("<body", StringComparison.OrdinalIgnoreCase)
                        format = If(looksLikeHtml, "text", "raw")
                    End If
                    If format = "text" AndAlso contentType.Contains("html", StringComparison.OrdinalIgnoreCase) Then
                        responseText = Regex.Replace(responseText, "<script[\s\S]*?</script>", "", RegexOptions.IgnoreCase)
                        responseText = Regex.Replace(responseText, "<style[\s\S]*?</style>", "", RegexOptions.IgnoreCase)
                        responseText = Regex.Replace(responseText, "<[^>]+>", " ")
                        responseText = WebUtility.HtmlDecode(responseText)
                        responseText = Regex.Replace(responseText, "\s+", " ").Trim()
                    ElseIf format = "json" Then
                        Try
                            Using doc = JsonDocument.Parse(responseText)
                                responseText = JsonSerializer.Serialize(doc.RootElement, JsonSO)
                            End Using
                        Catch
                        End Try
                    End If
                    If responseText.Length > safeMaxChars Then responseText = responseText.Substring(0, safeMaxChars) & "...[truncated]"
                    If includeMetadata Then
                        Dim responseHeaders As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)
                        For Each pair In response.Headers.Concat(response.Content.Headers)
                            responseHeaders(pair.Key) = String.Join(", ", pair.Value)
                        Next
                        Return JsonSerializer.Serialize(New Dictionary(Of String, Object) From {
                            {"success", response.IsSuccessStatusCode},
                            {"status_code", CInt(response.StatusCode)},
                            {"reason", If(response.ReasonPhrase, "")},
                            {"url", uri.AbsoluteUri},
                            {"content_type", contentType},
                            {"headers", responseHeaders},
                            {"body", responseText}
                        }, JsonSO)
                    End If
                    If Not response.IsSuccessStatusCode Then Return $"请求失败：HTTP {CInt(response.StatusCode)} {response.ReasonPhrase}{vbCrLf}{responseText}"
                    Return responseText
                End Using
            End Using
        End Using
    End Function

    Private Shared Function ReadLocalTextFile(path As String,
                                              startLine As Integer,
                                              lineCount As Integer,
                                              maxChars As Integer) As String
        If String.IsNullOrWhiteSpace(path) Then Return "缺少 path"
        If Not File.Exists(path) Then Return "文件不存在"
        Dim info As New FileInfo(path)
        If info.Length > 8 * 1024 * 1024 Then Return "文件超过 8 MiB 限制"
        Dim text = Agent通用工具_v6.DecodeTextBytes(File.ReadAllBytes(path))
        Dim first = Math.Max(startLine, 1)
        If first > 1 OrElse lineCount > 0 Then
            Dim lines = text.Replace(vbCrLf, vbLf).Replace(vbCr, vbLf).Split(ControlChars.Lf)
            Dim skip = Math.Min(first - 1, lines.Length)
            Dim take = If(lineCount <= 0, Math.Min(10000, lines.Length - skip), Math.Min(Math.Max(lineCount, 0), 10000))
            text = String.Join(vbCrLf, lines.Skip(skip).Take(take))
        End If
        Return Agent通用工具_v6.LimitText(text, Math.Min(Math.Max(maxChars, 1), 200000), "...[truncated]")
    End Function

    Private Shared Function WriteLocalTextFile(path As String, content As String, encodingName As String, createDirectories As Boolean) As String
        If String.IsNullOrWhiteSpace(path) Then Return "缺少 path"
        Dim fullPath = System.IO.Path.GetFullPath(path)
        Dim parent = System.IO.Path.GetDirectoryName(fullPath)
        If createDirectories AndAlso Not String.IsNullOrWhiteSpace(parent) Then Directory.CreateDirectory(parent)
        If String.IsNullOrWhiteSpace(parent) OrElse Not Directory.Exists(parent) Then Return "父目录不存在"
        Dim encoding As Encoding
        Select Case If(encodingName, "utf-8").Trim().ToLowerInvariant()
            Case "utf-16", "unicode" : encoding = Encoding.Unicode
            Case "utf-8-bom", "utf8-bom" : encoding = New UTF8Encoding(True)
            Case Else : encoding = New UTF8Encoding(False)
        End Select
        Dim tempPath = fullPath & ".3fui-tmp-" & Guid.NewGuid().ToString("N")
        Try
            File.WriteAllText(tempPath, If(content, ""), encoding)
            File.Move(tempPath, fullPath, True)
            Return JsonSerializer.Serialize(New Dictionary(Of String, Object) From {{"success", True}, {"path", fullPath}, {"bytes", New FileInfo(fullPath).Length}}, JsonSO)
        Finally
            If File.Exists(tempPath) Then File.Delete(tempPath)
        End Try
    End Function

    Private Shared Function ApplyLocalTextPatch(path As String, oldText As String, newText As String, replaceAll As Boolean, expectedReplacements As Integer) As String
        If String.IsNullOrWhiteSpace(path) Then Return "缺少 path"
        If Not File.Exists(path) Then Return "文件不存在"
        If String.IsNullOrEmpty(oldText) Then Return "old_text 不能为空"
        Dim original = Agent通用工具_v6.DecodeTextBytes(File.ReadAllBytes(path))
        Dim count = 0
        Dim searchStart = 0
        While searchStart <= original.Length - oldText.Length
            Dim found = original.IndexOf(oldText, searchStart, StringComparison.Ordinal)
            If found < 0 Then Exit While
            count += 1
            searchStart = found + oldText.Length
        End While
        If expectedReplacements > 0 AndAlso count <> expectedReplacements Then Return $"补丁未应用：匹配 {count} 次，期望 {expectedReplacements} 次"
        If count = 0 Then Return "补丁未应用：找不到完全匹配的 old_text"
        If Not replaceAll AndAlso count > 1 Then Return "补丁未应用：old_text 匹配多次，请提供更大上下文或传 replace_all=true"
        Dim updated = original.Replace(oldText, If(newText, ""), StringComparison.Ordinal)
        Dim result = WriteLocalTextFile(path, updated, "utf-8", False)
        Return JsonSerializer.Serialize(New Dictionary(Of String, Object) From {{"success", True}, {"path", System.IO.Path.GetFullPath(path)}, {"replacements", If(replaceAll, count, 1)}, {"result", result}}, JsonSO)
    End Function

    Private Shared Function ListDirectory(path As String, recursive As Boolean, maxItems As Integer) As String
        If String.IsNullOrWhiteSpace(path) Then Return "缺少 path"
        If Not Directory.Exists(path) Then Return "目录不存在"
        Dim dir As New DirectoryInfo(path)
        Dim safeLimit = Math.Min(Math.Max(maxItems, 1), 5000)
        Dim options As New EnumerationOptions With {.RecurseSubdirectories = recursive, .IgnoreInaccessible = True, .AttributesToSkip = FileAttributes.ReparsePoint}
        Dim items = dir.EnumerateFileSystemInfos("*", options).
            Take(safeLimit).
            OrderByDescending(Function(x) TypeOf x Is DirectoryInfo).
            ThenBy(Function(x) x.Name, StringComparer.CurrentCultureIgnoreCase).
            Select(Function(x) New Dictionary(Of String, Object) From {
                {"name", x.Name},
                {"path", x.FullName},
                {"type", If(TypeOf x Is DirectoryInfo, "directory", "file")},
                {"size", If(TypeOf x Is FileInfo, DirectCast(x, FileInfo).Length, 0)}
            }).ToList()
        Return JsonSerializer.Serialize(items, JsonSO)
    End Function

    Private Shared Function CreateDirectory(path As String) As String
        If String.IsNullOrWhiteSpace(path) Then Return "缺少 path"
        Dim fullPath = System.IO.Path.GetFullPath(path)
        Directory.CreateDirectory(fullPath)
        Return JsonSerializer.Serialize(New Dictionary(Of String, Object) From {{"success", True}, {"path", fullPath}}, JsonSO)
    End Function

    Private Shared Function CopyLocalFile(source As String, destination As String, overwrite As Boolean) As String
        If String.IsNullOrWhiteSpace(source) OrElse String.IsNullOrWhiteSpace(destination) Then Return "缺少 source 或 destination"
        If Not File.Exists(source) Then Return "源文件不存在"
        Dim fullDestination = System.IO.Path.GetFullPath(destination)
        Dim parent = System.IO.Path.GetDirectoryName(fullDestination)
        If Not String.IsNullOrWhiteSpace(parent) Then Directory.CreateDirectory(parent)
        If File.Exists(fullDestination) AndAlso Not overwrite Then Return "目标文件已存在，请传 overwrite=true"
        File.Copy(source, fullDestination, overwrite)
        Return JsonSerializer.Serialize(New Dictionary(Of String, Object) From {{"success", True}, {"source", System.IO.Path.GetFullPath(source)}, {"destination", fullDestination}}, JsonSO)
    End Function

    Private Shared Function MoveLocalPath(source As String, destination As String, overwrite As Boolean) As String
        If String.IsNullOrWhiteSpace(source) OrElse String.IsNullOrWhiteSpace(destination) Then Return "缺少 source 或 destination"
        If Not File.Exists(source) AndAlso Not Directory.Exists(source) Then Return "源路径不存在"
        source = System.IO.Path.TrimEndingDirectorySeparator(System.IO.Path.GetFullPath(source))
        Dim fullDestination = System.IO.Path.TrimEndingDirectorySeparator(System.IO.Path.GetFullPath(destination))
        If String.Equals(source, fullDestination, StringComparison.OrdinalIgnoreCase) OrElse
            (Directory.Exists(source) AndAlso
             (fullDestination.StartsWith(source & System.IO.Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase) OrElse
              source.StartsWith(fullDestination & System.IO.Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase))) Then
            Return "工具执行失败：源路径和目标路径相同或互为父子目录，拒绝移动。"
        End If
        If (File.Exists(fullDestination) OrElse Directory.Exists(fullDestination)) AndAlso Not overwrite Then Return "目标路径已存在，请传 overwrite=true"
        If Directory.Exists(source) Then
            Dim backup = If(overwrite AndAlso Directory.Exists(fullDestination), fullDestination & ".agent-backup-" & Guid.NewGuid().ToString("N"), "")
            If backup <> "" Then Directory.Move(fullDestination, backup)
            Try
                Directory.Move(source, fullDestination)
            Catch
                If backup <> "" Then Directory.Move(backup, fullDestination)
                Throw
            End Try
            If backup <> "" Then
                Try
                    Directory.Delete(backup, True)
                Catch
                    Return JsonSerializer.Serialize(New With {.success = True, .source = source, .destination = fullDestination, .backup_path = backup}, JsonSO)
                End Try
            End If
        Else
            Dim parent = System.IO.Path.GetDirectoryName(fullDestination)
            If Not String.IsNullOrWhiteSpace(parent) Then Directory.CreateDirectory(parent)
            File.Move(source, fullDestination, overwrite)
        End If
        Return JsonSerializer.Serialize(New Dictionary(Of String, Object) From {{"success", True}, {"source", System.IO.Path.GetFullPath(source)}, {"destination", fullDestination}}, JsonSO)
    End Function

    Private Shared Function DeleteLocalPath(path As String) As String
        If String.IsNullOrWhiteSpace(path) Then Return "缺少 path"
        If File.Exists(path) Then
            FileIO.FileSystem.DeleteFile(System.IO.Path.GetFullPath(path), FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin)
        ElseIf Directory.Exists(path) Then
            FileIO.FileSystem.DeleteDirectory(System.IO.Path.GetFullPath(path), FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin)
        Else
            Return "路径不存在"
        End If
        Return JsonSerializer.Serialize(New Dictionary(Of String, Object) From {{"success", True}, {"path", System.IO.Path.GetFullPath(path)}, {"recycled", True}}, JsonSO)
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
            Return JsonSerializer.Serialize(payload, JsonSO)
        End Using
    End Function

    Private Shared Function ImageFormatName(format As ImageFormat) As String
        If format.Guid = ImageFormat.Png.Guid Then Return "png"
        If format.Guid = ImageFormat.Jpeg.Guid Then Return "jpeg"
        If format.Guid = ImageFormat.Gif.Guid Then Return "gif"
        If format.Guid = ImageFormat.Bmp.Guid Then Return "bmp"
        Return format.ToString()
    End Function
End Class
