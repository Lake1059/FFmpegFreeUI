Imports System.IO
Imports System.Text
Imports System.Text.Json
Imports System.Text.RegularExpressions

Partial Public Class AgentLocalTools
    Private Const SearchResponseBudget As Integer = 12000
    Private Const SearchFileSizeLimit As Long = 8L * 1024 * 1024

    Private Shared Sub AddSearchToolDefinitions(tools As List(Of Dictionary(Of String, Object)))
        For Each toolName In {"search_local_files", "search_local_text"}
            Dim properties As New Dictionary(Of String, Object) From {
                {"path", New Dictionary(Of String, Object) From {{"type", "string"}, {"description", "搜索根目录或单个文件"}}},
                {"pattern", New Dictionary(Of String, Object) From {{"type", "string"}, {"description", "文件名通配符（如 *.srt）或相对路径 glob（如 **/*.vb），默认 *"}}},
                {"recursive", New Dictionary(Of String, Object) From {{"type", "boolean"}, {"description", "默认 true"}}},
                {"include_hidden", New Dictionary(Of String, Object) From {{"type", "boolean"}, {"description", "是否包含隐藏文件/目录，默认 false；不跟随链接"}}},
                {"exclude_directories", New Dictionary(Of String, Object) From {{"type", "array"}, {"items", New Dictionary(Of String, Object) From {{"type", "string"}}}, {"description", "排除的目录名；默认 .git、.vs、bin、obj、node_modules，传 [] 可清空默认排除"}}},
                {"offset", New Dictionary(Of String, Object) From {{"type", "integer"}, {"description", "跳过的匹配数，默认 0；与 next_offset 配合翻页"}}},
                {"limit", New Dictionary(Of String, Object) From {{"type", "integer"}, {"description", "最多返回匹配数，1-200，默认 50；约 12000 字符预算可能提前分页"}}},
                {"timeout_seconds", New Dictionary(Of String, Object) From {{"type", "integer"}, {"description", "1-30 秒，默认 10；超时返回部分结果和 stop_reason"}}}
            }
            If toolName = "search_local_text" Then
                properties.Add("query", New Dictionary(Of String, Object) From {{"type", "string"}, {"description", "必填，逐行匹配的文本或正则表达式；每行返回一次"}})
                properties.Add("regex", New Dictionary(Of String, Object) From {{"type", "boolean"}, {"description", "默认 false；正则每行匹配超时 100 ms"}})
                properties.Add("case_sensitive", New Dictionary(Of String, Object) From {{"type", "boolean"}, {"description", "默认 false"}})
                properties.Add("context_lines", New Dictionary(Of String, Object) From {{"type", "integer"}, {"description", "前后上下文行数，0-5，默认 0；单行片段最多 400 字符"}})
            End If
            tools.Add(FunctionTool(toolName,
                If(toolName = "search_local_files", "按文件名或相对路径 glob 搜索本地文件。", "搜索本地 UTF-8/带 BOM 的 Unicode 文本，返回路径、原始行号、列号及上下文；跳过二进制、无法解码和超过 8 MiB 的文件。") &
                "仅系统访问权限可用。返回 matches、has_more、next_offset、stop_reason 和扫描统计；翻页须沿用相同条件，目录或文件变动可能影响结果。无需安装外部搜索程序。",
                properties, If(toolName = "search_local_text", {"path", "query"}, {"path"})))
        Next
    End Sub

    Private Shared Iterator Function EnumerateSearchFiles(root As String, recursive As Boolean, includeHidden As Boolean,
                                                          excluded As HashSet(Of String), token As Threading.CancellationToken) As IEnumerable(Of FileInfo)
        Dim pending As New Stack(Of String)
        pending.Push(root)
        Dim options As New EnumerationOptions With {
            .IgnoreInaccessible = True, .RecurseSubdirectories = False,
            .AttributesToSkip = FileAttributes.ReparsePoint Or If(includeHidden, CType(0, FileAttributes), FileAttributes.Hidden)
        }
        While pending.Count > 0
            token.ThrowIfCancellationRequested()
            Dim folder = pending.Pop()
            If Not Directory.Exists(folder) Then Continue While
            For Each entry In New DirectoryInfo(folder).EnumerateFileSystemInfos("*", options)
                token.ThrowIfCancellationRequested()
                If TypeOf entry Is DirectoryInfo Then
                    If recursive AndAlso Not excluded.Contains(entry.Name) Then pending.Push(entry.FullName)
                ElseIf TypeOf entry Is FileInfo Then
                    Yield DirectCast(entry, FileInfo)
                End If
            Next
        End While
    End Function

    Private Shared Function BuildSearchGlob(pattern As String) As Func(Of String, String, Boolean)
        pattern = If(String.IsNullOrWhiteSpace(pattern), "*", pattern.Replace("\", "/"))
        If Not pattern.Contains("/"c) Then
            Return Function(name, relative) IO.Enumeration.FileSystemName.MatchesSimpleExpression(pattern, name, ignoreCase:=True)
        End If
        Dim expression = "^" & Regex.Escape(pattern).Replace("\*\*/", "(?:.*/)?").Replace("\*\*", ".*").Replace("\*", "[^/]*").Replace("\?", "[^/]") & "$"
        Dim matcher As New Regex(expression, RegexOptions.IgnoreCase Or RegexOptions.CultureInvariant, TimeSpan.FromMilliseconds(100))
        Return Function(name, relative) matcher.IsMatch(relative.Replace("\", "/"))
    End Function

    Private Shared Function SearchLocalFiles(args As JsonElement, textSearch As Boolean, cancellationToken As Threading.CancellationToken) As String
        cancellationToken.ThrowIfCancellationRequested()
        Dim path = Agent通用工具_v6.GetJsonString(args, "path").Trim()
        If path = "" Then Return "缺少 path"
        path = IO.Path.GetFullPath(path)
        Dim singleFile = File.Exists(path)
        If Not singleFile AndAlso Not Directory.Exists(path) Then Return "路径不存在或不是可搜索目录"
        If (File.GetAttributes(path) And FileAttributes.ReparsePoint) <> 0 Then Return "工具执行失败：搜索根路径不能是链接"
        Dim query = Agent通用工具_v6.GetJsonString(args, "query")
        If textSearch AndAlso query = "" Then Return "缺少 query"
        Dim caseSensitive = Agent通用工具_v6.GetJsonBoolean(args, "case_sensitive", False)
        Dim regexMatcher As Regex = Nothing
        If textSearch AndAlso Agent通用工具_v6.GetJsonBoolean(args, "regex", False) Then
            Try
                regexMatcher = New Regex(query, RegexOptions.CultureInvariant Or If(caseSensitive, RegexOptions.None, RegexOptions.IgnoreCase), TimeSpan.FromMilliseconds(100))
            Catch ex As ArgumentException
                Return JsonSerializer.Serialize(New With {.success = False, .error = "正则表达式无效：" & ex.Message}, ToolJsonOptions)
            End Try
        End If
        Dim glob = BuildSearchGlob(Agent通用工具_v6.GetJsonString(args, "pattern", "*"))
        Dim excluded As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
        Dim exclusionValue As JsonElement
        If args.TryGetProperty("exclude_directories", exclusionValue) AndAlso exclusionValue.ValueKind = JsonValueKind.Array Then
            excluded.UnionWith(Agent通用工具_v6.GetJsonStringArray(args, "exclude_directories"))
        Else
            excluded.UnionWith({".git", ".vs", "bin", "obj", "node_modules"})
        End If
        Dim offset = Math.Max(0, Agent通用工具_v6.GetJsonInteger(args, "offset", 0))
        Dim limit = Math.Clamp(Agent通用工具_v6.GetJsonInteger(args, "limit", 50), 1, 200)
        Dim context = Math.Clamp(Agent通用工具_v6.GetJsonInteger(args, "context_lines", 0), 0, 5)
        Dim timeoutSeconds = Math.Clamp(Agent通用工具_v6.GetJsonInteger(args, "timeout_seconds", 10), 1, 30)
        Dim matches As New List(Of Dictionary(Of String, Object))
        Dim skipped, scanned, unreadable, oversized As Integer
        Dim bytesScanned As Long
        Dim responseSize As Integer = 1024 + path.Length * 2
        Dim stopReason = "completed"
        Dim hasMore As Boolean
        Using deadline = Threading.CancellationTokenSource.CreateLinkedTokenSource(cancellationToken)
            deadline.CancelAfter(TimeSpan.FromSeconds(timeoutSeconds))
            Dim addMatch As Func(Of Dictionary(Of String, Object), Boolean) =
                Function(match)
                    If skipped < offset Then
                        skipped += 1
                        Return True
                    End If
                    Dim size = JsonSerializer.Serialize(match, ToolJsonOptions).Length + 1
                    If matches.Count >= limit OrElse (matches.Count > 0 AndAlso responseSize + size > SearchResponseBudget) Then
                        hasMore = True
                        stopReason = "page_limit"
                        Return False
                    End If
                    matches.Add(match)
                    responseSize += size
                    Return True
                End Function
            Try
                Dim files = If(singleFile, {New FileInfo(path)}.AsEnumerable(),
                    EnumerateSearchFiles(path, Agent通用工具_v6.GetJsonBoolean(args, "recursive", True),
                        Agent通用工具_v6.GetJsonBoolean(args, "include_hidden", False), excluded, deadline.Token))
                For Each file In files
                    deadline.Token.ThrowIfCancellationRequested()
                    scanned += 1
                    If scanned > If(textSearch, 10000, 100000) Then
                        stopReason = "scan_limit"
                        Exit For
                    End If
                    If Not glob(file.Name, If(singleFile, file.Name, IO.Path.GetRelativePath(path, file.FullName))) Then Continue For
                    If Not textSearch Then
                        If Not addMatch(New Dictionary(Of String, Object) From {{"path", file.FullName}, {"name", file.Name}}) Then Exit For
                        Continue For
                    End If
                    Dim text As String
                    Try
                        If file.Length > SearchFileSizeLimit Then
                            oversized += 1
                            Continue For
                        End If
                        If bytesScanned + file.Length > 64L * 1024 * 1024 Then
                            stopReason = "byte_limit"
                            Exit For
                        End If
                        Dim bytes As Byte()
                        Using stream As New FileStream(file.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite Or FileShare.Delete,
                                                       4096, FileOptions.Asynchronous Or FileOptions.SequentialScan),
                              capture As New MemoryStream()
                            Dim buffer(16383) As Byte
                            Do
                                Dim read = stream.ReadAsync(buffer.AsMemory(), deadline.Token).AsTask().GetAwaiter().GetResult()
                                If read = 0 Then Exit Do
                                If capture.Length + read > SearchFileSizeLimit Then Exit Do
                                capture.Write(buffer, 0, read)
                            Loop
                            If stream.Position > SearchFileSizeLimit Then
                                oversized += 1
                                Continue For
                            End If
                            bytes = capture.ToArray()
                        End Using
                        bytesScanned += bytes.LongLength
                        text = DecodeSearchText(bytes)
                        If text.Contains(ChrW(0)) Then
                            unreadable += 1
                            Continue For
                        End If
                    Catch ex As Exception When TypeOf ex Is IOException OrElse TypeOf ex Is UnauthorizedAccessException OrElse TypeOf ex Is DecoderFallbackException
                        unreadable += 1
                        Continue For
                    End Try
                    Using reader As New StringReader(text)
                        Dim before As New Queue(Of String)
                        Dim after As New Queue(Of String)
                        For i = 0 To context
                            Dim line = reader.ReadLine()
                            If line Is Nothing Then Exit For
                            after.Enqueue(line)
                        Next
                        Dim number As Integer
                        While after.Count > 0
                            deadline.Token.ThrowIfCancellationRequested()
                            number += 1
                            Dim line = after.Dequeue()
                            Dim column As Integer
                            If regexMatcher Is Nothing Then
                                column = line.IndexOf(query, If(caseSensitive, StringComparison.Ordinal, StringComparison.OrdinalIgnoreCase))
                            Else
                                Dim found = regexMatcher.Match(line)
                                column = If(found.Success, found.Index, -1)
                            End If
                            If column >= 0 Then
                                Dim snippetStart = Math.Max(0, column - 120)
                                Dim match As New Dictionary(Of String, Object) From {
                                    {"path", file.FullName}, {"line", number}, {"column", column + 1},
                                    {"text", SearchSnippet(line, snippetStart)}, {"text_start_column", snippetStart + 1}
                                }
                                If context > 0 Then
                                    match("before") = before.ToArray()
                                    match("after") = after.Select(Function(value) SearchSnippet(value, 0)).ToArray()
                                End If
                                If Not addMatch(match) Then Exit While
                            End If
                            before.Enqueue(SearchSnippet(line, 0))
                            While before.Count > context
                                before.Dequeue()
                            End While
                            Dim nextLine = reader.ReadLine()
                            If nextLine IsNot Nothing Then after.Enqueue(nextLine)
                        End While
                    End Using
                    If hasMore Then Exit For
                Next
            Catch ex As OperationCanceledException When Not cancellationToken.IsCancellationRequested
                stopReason = "timeout"
            Catch ex As RegexMatchTimeoutException
                stopReason = "regex_timeout"
            End Try
        End Using
        cancellationToken.ThrowIfCancellationRequested()
        Return JsonSerializer.Serialize(New With {
            .success = True, .path = path, .matches = matches, .offset = offset, .has_more = hasMore,
            .next_offset = If(hasMore, CType(offset + matches.Count, Integer?), Nothing),
            .stop_reason = stopReason, .complete = stopReason = "completed", .scanned_files = scanned,
            .scanned_bytes = bytesScanned, .skipped_unreadable_or_binary = unreadable, .skipped_oversized = oversized
        }, ToolJsonOptions)
    End Function

    Private Shared Function SearchSnippet(line As String, start As Integer) As String
        Dim take = Math.Min(400, line.Length - start)
        Return If(start > 0, "…", "") & line.Substring(start, take) & If(start + take < line.Length, "…", "")
    End Function

    Private Shared Function DecodeSearchText(bytes As Byte()) As String
        Dim encoding As Encoding = New UTF8Encoding(False, True)
        Dim skip As Integer
        If bytes.Length >= 4 AndAlso bytes(0) = &HFF AndAlso bytes(1) = &HFE AndAlso bytes(2) = 0 AndAlso bytes(3) = 0 Then
            encoding = New UTF32Encoding(False, True, True)
            skip = 4
        ElseIf bytes.Length >= 4 AndAlso bytes(0) = 0 AndAlso bytes(1) = 0 AndAlso bytes(2) = &HFE AndAlso bytes(3) = &HFF Then
            encoding = New UTF32Encoding(True, True, True)
            skip = 4
        ElseIf bytes.Length >= 3 AndAlso bytes(0) = &HEF AndAlso bytes(1) = &HBB AndAlso bytes(2) = &HBF Then
            skip = 3
        ElseIf bytes.Length >= 2 AndAlso bytes(0) = &HFF AndAlso bytes(1) = &HFE Then
            encoding = New UnicodeEncoding(False, True, True)
            skip = 2
        ElseIf bytes.Length >= 2 AndAlso bytes(0) = &HFE AndAlso bytes(1) = &HFF Then
            encoding = New UnicodeEncoding(True, True, True)
            skip = 2
        End If
        Return encoding.GetString(bytes, skip, bytes.Length - skip)
    End Function
End Class
