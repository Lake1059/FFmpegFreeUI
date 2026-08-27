Imports System.Drawing.Imaging
Imports System.IO
Imports System.Net
Imports System.Net.Http
Imports System.Text
Imports System.Text.Json
Imports System.Text.RegularExpressions

Public Class AgentLocalTools
    Public Const PermissionSafe As Integer = 0
    Public Const PermissionEnvironment As Integer = 1
    Public Const PermissionSystem As Integer = 2

    Public MustInherit Class ConsoleRunSession
        Implements IDisposable

        Private Const DefaultTimeoutSeconds As Integer = 60
        Private Const MaxCapturedOutputCharacters As Integer = 12000
        Private Shared ReadOnly ConsoleProtocolJsonOptions As New JsonSerializerOptions With {
            .WriteIndented = False,
            .PropertyNamingPolicy = Nothing,
            .DictionaryKeyPolicy = Nothing,
            .Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        }
        Private Shared ReadOnly Utf8NoBomEncoding As New UTF8Encoding(False)

        Private ReadOnly _gate As New Threading.SemaphoreSlim(1, 1)
        Private ReadOnly _outputLock As New Object()
        Private _process As Process = Nothing
        Private _processExitedTcs As TaskCompletionSource(Of Boolean) = Nothing
        Private _stdoutBuilder As StringBuilder = Nothing
        Private _stderrBuilder As StringBuilder = Nothing
        Private _stdoutEndPrefix As String = ""
        Private _stderrEndMarker As String = ""
        Private _stdoutEndTcs As TaskCompletionSource(Of Boolean) = Nothing
        Private _stderrEndTcs As TaskCompletionSource(Of Boolean) = Nothing
        Private _reportedExitCode As Integer = -1
        Private _reportedWorkingDirectory As String = ""
        Private _lastWorkingDirectory As String = ""
        Private _processFileName As String = ""
        Private _disposed As Boolean = False

        Protected MustOverride ReadOnly Property DisplayName As String
        Protected MustOverride ReadOnly Property InputPropertyNames As String()
        Protected MustOverride ReadOnly Property StdoutMarkerPrefixBase As String
        Protected MustOverride ReadOnly Property StderrMarkerPrefixBase As String

        Protected ReadOnly Property CurrentProcessFileName As String
            Get
                Return _processFileName
            End Get
        End Property

        Public Async Function ExecuteAsync(args As JsonElement,
                                           cancellationToken As Threading.CancellationToken) As Task(Of String)
            Dim inputText = ReadInputText(args).Trim()
            If inputText = "" Then Return MissingInputMessage()

            Dim requestedWorkingDirectory = GetJsonStringValue(args, "working_directory").Trim()
            If requestedWorkingDirectory <> "" AndAlso Not Directory.Exists(requestedWorkingDirectory) Then Return "Directory does not exist: " & requestedWorkingDirectory

            Dim timeoutSeconds = GetJsonIntegerValue(args, "timeout_seconds", DefaultTimeoutSeconds)
            timeoutSeconds = Math.Min(Math.Max(timeoutSeconds, 1), 300)

            Await _gate.WaitAsync(cancellationToken)
            Try
                If _disposed Then Return DisplayName & " session closed."

                Dim initialDirectory = If(requestedWorkingDirectory <> "", requestedWorkingDirectory, Application.StartupPath)
                EnsureProcessStarted(initialDirectory)

                Dim token = Guid.NewGuid().ToString("N")
                Dim stdoutEndPrefix = StdoutMarkerPrefixBase & token & ":"
                Dim stderrEndMarker = StderrMarkerPrefixBase & token
                Dim stdoutCapture As New StringBuilder()
                Dim stderrCapture As New StringBuilder()
                Dim stdoutEndTcs As New TaskCompletionSource(Of Boolean)(TaskCreationOptions.RunContinuationsAsynchronously)
                Dim stderrEndTcs As New TaskCompletionSource(Of Boolean)(TaskCreationOptions.RunContinuationsAsynchronously)

                SyncLock _outputLock
                    _stdoutBuilder = stdoutCapture
                    _stderrBuilder = stderrCapture
                    _stdoutEndPrefix = stdoutEndPrefix
                    _stderrEndMarker = stderrEndMarker
                    _stdoutEndTcs = stdoutEndTcs
                    _stderrEndTcs = stderrEndTcs
                    _reportedExitCode = -1
                    _reportedWorkingDirectory = ""
                End SyncLock

                Dim sw = System.Diagnostics.Stopwatch.StartNew()
                Dim exitCode As Integer = -1
                Dim timedOut As Boolean = False
                Dim processEnded As Boolean = False
                Dim waitAfterKill As Boolean = False
                Dim throwCancellation As Boolean = False
                Dim operationFailure As Exception = Nothing

                Using linkedCts = Threading.CancellationTokenSource.CreateLinkedTokenSource(cancellationToken)
                    linkedCts.CancelAfter(TimeSpan.FromSeconds(timeoutSeconds))
                    Try
                        _process.StandardInput.WriteLine(BuildInputPayload(inputText, requestedWorkingDirectory, stdoutEndPrefix, stderrEndMarker))
                        _process.StandardInput.Flush()

                        Dim markerTask = Task.WhenAll(stdoutEndTcs.Task, stderrEndTcs.Task)
                        Dim completedTask = Await Task.WhenAny(markerTask, _processExitedTcs.Task).WaitAsync(linkedCts.Token)
                        If completedTask Is _processExitedTcs.Task AndAlso Not markerTask.IsCompleted Then
                            processEnded = True
                            If _process IsNot Nothing AndAlso _process.HasExited Then exitCode = _process.ExitCode
                        Else
                            Await markerTask
                        End If
                    Catch ex As OperationCanceledException
                        KillProcess()
                        waitAfterKill = True
                        throwCancellation = cancellationToken.IsCancellationRequested
                        timedOut = Not throwCancellation
                        processEnded = True
                    Catch ex As Exception
                        operationFailure = ex
                        processEnded = True
                    End Try
                End Using

                If waitAfterKill Then
                    Await WaitForCurrentProcessExitAsync()
                    If _process IsNot Nothing AndAlso _process.HasExited Then exitCode = _process.ExitCode
                    If throwCancellation Then Throw New OperationCanceledException(cancellationToken)
                End If

                sw.Stop()

                Dim stdout As String
                Dim stderr As String
                Dim workingDirectory As String = ""
                SyncLock _outputLock
                    stdout = stdoutCapture.ToString()
                    stderr = stderrCapture.ToString()
                    If exitCode < 0 Then exitCode = _reportedExitCode
                    workingDirectory = _reportedWorkingDirectory
                    ClearCaptureLocked()
                End SyncLock

                If workingDirectory = "" Then workingDirectory = If(requestedWorkingDirectory <> "", requestedWorkingDirectory, _lastWorkingDirectory)
                If workingDirectory <> "" Then _lastWorkingDirectory = workingDirectory
                If processEnded Then CleanupProcess()

                If operationFailure IsNot Nothing Then Throw New InvalidOperationException(DisplayName & " execution failed: " & operationFailure.Message, operationFailure)

                stdout = LimitSessionText(stdout, MaxCapturedOutputCharacters)
                stderr = LimitSessionText(stderr, MaxCapturedOutputCharacters)

                Dim result = BuildResultPayload(inputText, workingDirectory, timeoutSeconds, timedOut, exitCode, CLng(sw.Elapsed.TotalMilliseconds), stdout, stderr)
                Return JsonSerializer.Serialize(result, JsonSO)
            Finally
                _gate.Release()
            End Try
        End Function

        Protected MustOverride Function CreateProcessCandidates(workingDirectory As String) As IEnumerable(Of Process)

        Protected MustOverride Function BuildInputPayload(inputText As String,
                                                          workingDirectory As String,
                                                          stdoutEndPrefix As String,
                                                          stderrEndMarker As String) As String

        Protected Overridable Sub OnProcessStarted(process As Process)
        End Sub

        Protected Overridable Function BuildResultPayload(inputText As String,
                                                          workingDirectory As String,
                                                          timeoutSeconds As Integer,
                                                          timedOut As Boolean,
                                                          exitCode As Integer,
                                                          elapsedMilliseconds As Long,
                                                          stdout As String,
                                                          stderr As String) As Dictionary(Of String, Object)
            Return New Dictionary(Of String, Object) From {
                {InputPropertyNames(0), inputText},
                {"working_directory", workingDirectory},
                {"timeout_seconds", timeoutSeconds},
                {"timed_out", timedOut},
                {"exit_code", exitCode},
                {"elapsed_ms", elapsedMilliseconds},
                {"stdout", stdout},
                {"stderr", stderr}
            }
        End Function

        Protected Overridable Function MissingInputMessage() As String
            Return "Missing " & InputPropertyNames(0) & "."
        End Function

        Private Function ReadInputText(args As JsonElement) As String
            For Each name In InputPropertyNames
                Dim value = GetJsonStringValue(args, name).Trim()
                If value <> "" Then Return value
            Next
            Return ""
        End Function

        Private Sub EnsureProcessStarted(initialWorkingDirectory As String)
            If _process IsNot Nothing AndAlso Not _process.HasExited Then Return

            CleanupProcess()

            Dim workingDirectory = If(initialWorkingDirectory, "").Trim()
            If workingDirectory = "" OrElse Not Directory.Exists(workingDirectory) Then workingDirectory = Application.StartupPath

            Dim lastError As Exception = Nothing
            For Each process In CreateProcessCandidates(workingDirectory)
                Try
                    AddHandler process.OutputDataReceived, AddressOf OnOutputDataReceived
                    AddHandler process.ErrorDataReceived, AddressOf OnErrorDataReceived
                    AddHandler process.Exited, AddressOf OnProcessExited
                    _processExitedTcs = New TaskCompletionSource(Of Boolean)(TaskCreationOptions.RunContinuationsAsynchronously)

                    If Not process.Start() Then Throw New InvalidOperationException("Start returned false.")

                    _process = process
                    _processFileName = process.StartInfo.FileName
                    _lastWorkingDirectory = workingDirectory
                    process.BeginOutputReadLine()
                    process.BeginErrorReadLine()
                    OnProcessStarted(process)
                    Return
                Catch ex As Exception
                    lastError = ex
                    Try
                        RemoveHandler process.OutputDataReceived, AddressOf OnOutputDataReceived
                        RemoveHandler process.ErrorDataReceived, AddressOf OnErrorDataReceived
                        RemoveHandler process.Exited, AddressOf OnProcessExited
                    Catch
                    End Try
                    Try
                        If Not process.HasExited Then process.Kill(True)
                    Catch
                    End Try
                    Try
                        process.Dispose()
                    Catch
                    End Try
                End Try
            Next

            _processExitedTcs = Nothing
            Throw New InvalidOperationException(DisplayName & " start failed: " & If(lastError?.Message, "No candidate executable could be started."))
        End Sub

        Protected Shared Function CreateRedirectedProcess(fileName As String, workingDirectory As String) As Process
            Return New Process With {
                .StartInfo = New ProcessStartInfo With {
                    .FileName = fileName,
                    .WorkingDirectory = workingDirectory,
                    .UseShellExecute = False,
                    .RedirectStandardInput = True,
                    .RedirectStandardOutput = True,
                    .RedirectStandardError = True,
                    .CreateNoWindow = True,
                    .StandardInputEncoding = Utf8NoBomEncoding,
                    .StandardOutputEncoding = Utf8NoBomEncoding,
                    .StandardErrorEncoding = Utf8NoBomEncoding
                },
                .EnableRaisingEvents = True
            }
        End Function

        Protected Shared Function SerializeConsoleProtocolPayload(payload As Object) As String
            Return JsonSerializer.Serialize(payload, ConsoleProtocolJsonOptions)
        End Function

        Protected Shared Function ToBase64Utf8(text As String) As String
            Return Convert.ToBase64String(Encoding.UTF8.GetBytes(If(text, "")))
        End Function

        Protected Shared Function FromBase64Utf8(text As String) As String
            If String.IsNullOrWhiteSpace(text) Then Return ""
            Try
                Return Encoding.UTF8.GetString(Convert.FromBase64String(text))
            Catch
                Return ""
            End Try
        End Function

        Private Shared Function GetJsonStringValue(root As JsonElement, name As String, Optional defaultValue As String = "") As String
            If root.ValueKind <> JsonValueKind.Object Then Return defaultValue
            Dim value As JsonElement
            If Not root.TryGetProperty(name, value) Then Return defaultValue
            If value.ValueKind = JsonValueKind.String Then Return If(value.GetString(), "")
            If value.ValueKind = JsonValueKind.Number OrElse value.ValueKind = JsonValueKind.True OrElse value.ValueKind = JsonValueKind.False Then Return value.ToString()
            Return defaultValue
        End Function

        Private Shared Function GetJsonIntegerValue(root As JsonElement, name As String, defaultValue As Integer) As Integer
            If root.ValueKind <> JsonValueKind.Object Then Return defaultValue
            Dim value As JsonElement
            If Not root.TryGetProperty(name, value) Then Return defaultValue
            If value.ValueKind = JsonValueKind.Number Then
                Dim result As Integer
                If value.TryGetInt32(result) Then Return result
            End If
            Dim text = GetJsonStringValue(root, name)
            Dim parsed As Integer
            If Integer.TryParse(text, parsed) Then Return parsed
            Return defaultValue
        End Function

        Private Shared Function LimitSessionText(text As String, maxLength As Integer) As String
            text = If(text, "")
            If maxLength <= 0 OrElse text.Length <= maxLength Then Return text
            Return text.Substring(0, maxLength) & "...[truncated]"
        End Function

        Private Sub OnOutputDataReceived(sender As Object, e As DataReceivedEventArgs)
            If e.Data Is Nothing Then Return

            SyncLock _outputLock
                If _stdoutBuilder Is Nothing Then Return

                Dim line = e.Data
                Dim index = If(_stdoutEndPrefix = "", -1, line.IndexOf(_stdoutEndPrefix, StringComparison.Ordinal))
                If index >= 0 Then
                    If index > 0 Then _stdoutBuilder.AppendLine(line.Substring(0, index))

                    Dim markerPayload = line.Substring(index + _stdoutEndPrefix.Length).Trim()
                    Dim separator = markerPayload.IndexOf(":"c)
                    Dim exitText = If(separator >= 0, markerPayload.Substring(0, separator), markerPayload)
                    Dim parsedExitCode As Integer
                    If Integer.TryParse(exitText, parsedExitCode) Then _reportedExitCode = parsedExitCode
                    If separator >= 0 Then _reportedWorkingDirectory = FromBase64Utf8(markerPayload.Substring(separator + 1))

                    _stdoutEndTcs?.TrySetResult(True)
                    Return
                End If

                _stdoutBuilder.AppendLine(line)
            End SyncLock
        End Sub

        Private Sub OnErrorDataReceived(sender As Object, e As DataReceivedEventArgs)
            If e.Data Is Nothing Then Return

            SyncLock _outputLock
                If _stderrBuilder Is Nothing Then Return

                Dim line = e.Data
                Dim index = If(_stderrEndMarker = "", -1, line.IndexOf(_stderrEndMarker, StringComparison.Ordinal))
                If index >= 0 Then
                    If index > 0 Then _stderrBuilder.AppendLine(line.Substring(0, index))
                    _stderrEndTcs?.TrySetResult(True)
                    Return
                End If

                _stderrBuilder.AppendLine(line)
            End SyncLock
        End Sub

        Private Sub OnProcessExited(sender As Object, e As EventArgs)
            _processExitedTcs?.TrySetResult(True)
        End Sub

        Private Sub ClearCaptureLocked()
            _stdoutBuilder = Nothing
            _stderrBuilder = Nothing
            _stdoutEndPrefix = ""
            _stderrEndMarker = ""
            _stdoutEndTcs = Nothing
            _stderrEndTcs = Nothing
            _reportedExitCode = -1
            _reportedWorkingDirectory = ""
        End Sub

        Private Sub KillProcess()
            If _process Is Nothing Then Return
            Try
                If Not _process.HasExited Then
                    Try
                        _process.Kill(True)
                    Catch
                        _process.Kill()
                    End Try
                End If
            Catch
            End Try
        End Sub

        Private Async Function WaitForCurrentProcessExitAsync() As Task
            If _process Is Nothing Then Return
            Try
                Await _process.WaitForExitAsync(Threading.CancellationToken.None)
            Catch
            End Try
        End Function

        Private Sub CleanupProcess()
            If _process Is Nothing Then Return

            Try
                If _process.HasExited Then _process.WaitForExit()
            Catch
            End Try
            Try
                RemoveHandler _process.OutputDataReceived, AddressOf OnOutputDataReceived
                RemoveHandler _process.ErrorDataReceived, AddressOf OnErrorDataReceived
                RemoveHandler _process.Exited, AddressOf OnProcessExited
            Catch
            End Try
            Try
                _process.Dispose()
            Catch
            End Try
            _process = Nothing
            _processExitedTcs = Nothing
            _processFileName = ""
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            If _disposed Then Return
            _disposed = True

            _gate.Wait()
            Try
                KillProcess()
                CleanupProcess()
                SyncLock _outputLock
                    ClearCaptureLocked()
                End SyncLock
            Finally
                _gate.Release()
                _gate.Dispose()
            End Try
        End Sub
    End Class

    Public NotInheritable Class PowerShellRunSession
        Inherits ConsoleRunSession

        Protected Overrides ReadOnly Property DisplayName As String
            Get
                Return "PowerShell"
            End Get
        End Property

        Protected Overrides ReadOnly Property InputPropertyNames As String()
            Get
                Return New String() {"command"}
            End Get
        End Property

        Protected Overrides ReadOnly Property StdoutMarkerPrefixBase As String
            Get
                Return "__3FUI_AGENT_PS_STDOUT_END__"
            End Get
        End Property

        Protected Overrides ReadOnly Property StderrMarkerPrefixBase As String
            Get
                Return "__3FUI_AGENT_PS_STDERR_END__"
            End Get
        End Property

        Protected Overrides Iterator Function CreateProcessCandidates(workingDirectory As String) As IEnumerable(Of Process)
            Dim process = CreateRedirectedProcess("powershell.exe", workingDirectory)
            process.StartInfo.ArgumentList.Add("-NoLogo")
            process.StartInfo.ArgumentList.Add("-NoProfile")
            process.StartInfo.ArgumentList.Add("-NonInteractive")
            process.StartInfo.ArgumentList.Add("-ExecutionPolicy")
            process.StartInfo.ArgumentList.Add("Bypass")
            process.StartInfo.ArgumentList.Add("-Command")
            process.StartInfo.ArgumentList.Add("-")
            process.StartInfo.EnvironmentVariables("POWERSHELL_TELEMETRY_OPTOUT") = "1"
            Yield process
        End Function

        Protected Overrides Sub OnProcessStarted(process As Process)
            process.StandardInput.WriteLine(
                "$__3FUI_Utf8 = [System.Text.UTF8Encoding]::new($false); " &
                "[Console]::InputEncoding = $__3FUI_Utf8; " &
                "[Console]::OutputEncoding = $__3FUI_Utf8; " &
                "$global:OutputEncoding = $__3FUI_Utf8; " &
                "$global:PSDefaultParameterValues['*:Encoding'] = 'utf8'; " &
                "$ProgressPreference = 'SilentlyContinue'")
            process.StandardInput.Flush()
        End Sub

        Protected Overrides Function BuildInputPayload(inputText As String,
                                                       workingDirectory As String,
                                                       stdoutEndPrefix As String,
                                                       stderrEndMarker As String) As String
            Return BuildPersistentPowerShellScript(inputText, workingDirectory, stdoutEndPrefix, stderrEndMarker)
        End Function

        Private Shared Function BuildPersistentPowerShellScript(command As String,
                                                                workingDirectory As String,
                                                                stdoutEndPrefix As String,
                                                                stderrEndMarker As String) As String
            Dim commandBase64 = ToBase64Utf8(command)
            Dim workingDirectoryBase64 = If(String.IsNullOrWhiteSpace(workingDirectory), "", ToBase64Utf8(workingDirectory))
            Dim sb As New StringBuilder()
            sb.AppendLine("$__3FUI_PreviousLastExitCode = $global:LASTEXITCODE")
            sb.AppendLine("$global:LASTEXITCODE = $null")
            sb.AppendLine("$__3FUI_CommandSucceeded = $true")
            sb.AppendLine("try {")
            If workingDirectoryBase64 <> "" Then
                sb.AppendLine("    Set-Location -LiteralPath ([System.Text.Encoding]::UTF8.GetString([System.Convert]::FromBase64String('" & workingDirectoryBase64 & "')))")
            End If
            sb.AppendLine("    $__3FUI_CommandText = [System.Text.Encoding]::UTF8.GetString([System.Convert]::FromBase64String('" & commandBase64 & "'))")
            sb.AppendLine("    $__3FUI_ScriptBlock = [System.Management.Automation.ScriptBlock]::Create($__3FUI_CommandText)")
            sb.AppendLine("    . $__3FUI_ScriptBlock")
            sb.AppendLine("    $__3FUI_CommandSucceeded = $?")
            sb.AppendLine("} catch {")
            sb.AppendLine("    $__3FUI_CommandSucceeded = $false")
            sb.AppendLine("    Write-Error $_")
            sb.AppendLine("} finally {")
            sb.AppendLine("    $__3FUI_HasNativeExitCode = $null -ne $global:LASTEXITCODE")
            sb.AppendLine("    if ($__3FUI_HasNativeExitCode) { $__3FUI_ExitCode = [int]$global:LASTEXITCODE } elseif (-not $__3FUI_CommandSucceeded) { $__3FUI_ExitCode = 1 } else { $__3FUI_ExitCode = 0 }")
            sb.AppendLine("    if (-not $__3FUI_HasNativeExitCode) { $global:LASTEXITCODE = $__3FUI_PreviousLastExitCode }")
            sb.AppendLine("    $__3FUI_CwdText = ''")
            sb.AppendLine("    try { $__3FUI_CwdText = (Get-Location).ProviderPath } catch { }")
            sb.AppendLine("    $__3FUI_CwdBase64 = [System.Convert]::ToBase64String([System.Text.Encoding]::UTF8.GetBytes($__3FUI_CwdText))")
            sb.AppendLine("    [Console]::Out.WriteLine('" & stdoutEndPrefix & "' + $__3FUI_ExitCode + ':' + $__3FUI_CwdBase64)")
            sb.AppendLine("    [Console]::Error.WriteLine('" & stderrEndMarker & "')")
            sb.AppendLine("    Remove-Variable -Name __3FUI_PreviousLastExitCode,__3FUI_CommandSucceeded,__3FUI_CommandText,__3FUI_ScriptBlock,__3FUI_HasNativeExitCode,__3FUI_ExitCode,__3FUI_CwdText,__3FUI_CwdBase64 -ErrorAction SilentlyContinue")
            sb.AppendLine("}")
            Return sb.ToString()
        End Function
    End Class

    <CodeAnalysis.SuppressMessage("Performance", "CA1861:不要将常量数组作为参数", Justification:="<挂起>")>
    Public Shared Function BuildToolDefinitions(permissionLevel As Integer, networkMode As Integer) As List(Of Dictionary(Of String, Object))
        Dim tools As New List(Of Dictionary(Of String, Object)) From {
            FunctionTool("list_agent_skills", "列出 3FUI Agent 内置 skill 资料库及可按需读取的 reference。遇到 3FUI 环境机制、工具行为、参数面板、命令生成、队列、预设、联网权限或编码推荐问题时先用它查看索引。", New Dictionary(Of String, Object)),
            FunctionTool("read_agent_skill_reference", "读取 3FUI Agent 内置 skill 资料库中的指定 reference。只读取与当前任务相关的章节，避免一次加载全部资料。", New Dictionary(Of String, Object) From {
            {"skill", New Dictionary(Of String, Object) From {{"type", "string"}, {"description", "skill 名称，默认 ffmpegfreeui"}}},
            {"reference", New Dictionary(Of String, Object) From {{"type", "string"}, {"description", "reference 名称，例如 SKILL.md 或 references/parameter-panel.md"}}}
        }, {"reference"}),
            FunctionTool("get_parameter_panel_state", "读取当前 3FUI 参数面板的结构化预设 JSON、人类可读总览和命令行预览。", New Dictionary(Of String, Object)),
            FunctionTool("get_parameter_field_info", "按字段名或关键词查询参数面板字段的类型、当前值、候选值和格式规则。填写不熟悉的参数前优先调用，避免猜字段值。", New Dictionary(Of String, Object) From {
            {"fields", New Dictionary(Of String, Object) From {{"type", "array"}, {"items", New Dictionary(Of String, Object) From {{"type", "string"}}}}},
            {"query", New Dictionary(Of String, Object) From {{"type", "string"}, {"description", "可选关键词，用于模糊查找字段名"}}},
            {"include_current_values", New Dictionary(Of String, Object) From {{"type", "boolean"}}}
        }),
            FunctionTool("apply_parameter_panel_patch", "修改当前 3FUI 参数面板，并返回结构化结果、总览和命令行预览。优先传 changes 对象，键为 预设数据_v6 的属性名；也可传 preset_json 应用完整预设。修改 滤镜排序系统 时必须传完整排序列表，删除内置滤镜会同步清空对应参数页。", New Dictionary(Of String, Object) From {
            {"changes", New Dictionary(Of String, Object) From {{"type", "object"}, {"additionalProperties", True}}},
            {"preset_json", New Dictionary(Of String, Object) From {{"type", "string"}}},
            {"note", New Dictionary(Of String, Object) From {{"type", "string"}}}
        })
        }

        If permissionLevel >= PermissionEnvironment Then
            tools.Add(FunctionTool("get_queue_summary", "读取 3FUI 编码队列任务信息及活动任务的性能占用。性能采样独立于任务日志窗口；可用 id/ids 或 1-based index/indexes 查询指定任务，detail=true 返回完整任务信息，target=all 返回全部。include_commands=true 会同时返回参数预览命令行和实际执行命令行；日志请另用 get_queue_task_logs。", New Dictionary(Of String, Object) From {
                {"id", New Dictionary(Of String, Object) From {{"type", "string"}, {"description", "单个任务 ID"}}},
                {"ids", New Dictionary(Of String, Object) From {{"type", "array"}, {"items", New Dictionary(Of String, Object) From {{"type", "string"}}}, {"description", "任务 ID 列表"}}},
                {"index", New Dictionary(Of String, Object) From {{"type", "integer"}, {"description", "队列中的 1-based 序号"}}},
                {"indexes", New Dictionary(Of String, Object) From {{"type", "array"}, {"items", New Dictionary(Of String, Object) From {{"type", "integer"}}}, {"description", "队列中的 1-based 序号列表"}}},
                {"target", New Dictionary(Of String, Object) From {{"type", "string"}, {"description", "all/全部 表示读取全部任务；不传目标时默认读取全部摘要"}}},
                {"detail", New Dictionary(Of String, Object) From {{"type", "boolean"}, {"description", "是否返回完整任务信息；查询指定任务时默认 true"}}},
                {"include_performance", New Dictionary(Of String, Object) From {{"type", "boolean"}, {"description", "是否返回活动进程的 CPU、内存和 GPU 占用，默认 true；无需打开任务日志窗口"}}},
                {"include_commands", New Dictionary(Of String, Object) From {{"type", "boolean"}, {"description", "是否附带可执行命令行，默认 false"}}},
                {"include_preset_json", New Dictionary(Of String, Object) From {{"type", "boolean"}, {"description", "是否附带任务预设 JSON，可能很大，默认 false"}}},
                {"offset", New Dictionary(Of String, Object) From {{"type", "integer"}, {"description", "target=all 时跳过的任务数，默认 0"}}},
                {"limit", New Dictionary(Of String, Object) From {{"type", "integer"}, {"description", "target=all 时最多返回的任务数；0 表示不限制"}}}
            }))
            tools.Add(FunctionTool("get_queue_task_logs", "读取指定编码队列任务日志。先用 get_queue_summary 获取任务 ID 或序号；默认一次返回四档：all、latest_non_progress、errors、current_stage。", New Dictionary(Of String, Object) From {
                {"id", New Dictionary(Of String, Object) From {{"type", "string"}, {"description", "单个任务 ID"}}},
                {"ids", New Dictionary(Of String, Object) From {{"type", "array"}, {"items", New Dictionary(Of String, Object) From {{"type", "string"}}}, {"description", "任务 ID 列表"}}},
                {"index", New Dictionary(Of String, Object) From {{"type", "integer"}, {"description", "队列中的 1-based 序号"}}},
                {"indexes", New Dictionary(Of String, Object) From {{"type", "array"}, {"items", New Dictionary(Of String, Object) From {{"type", "integer"}}}, {"description", "队列中的 1-based 序号列表"}}},
                {"target", New Dictionary(Of String, Object) From {{"type", "string"}, {"description", "all/全部 表示读取全部任务日志，可能很大"}}},
                {"mode", New Dictionary(Of String, Object) From {{"type", "string"}, {"description", "单个日志档位：all、latest_non_progress、errors、current_stage；不传则返回四档"}}},
                {"modes", New Dictionary(Of String, Object) From {{"type", "array"}, {"items", New Dictionary(Of String, Object) From {{"type", "string"}}}, {"description", "多个日志档位；可传 all_modes 返回四档"}}},
                {"log_limit", New Dictionary(Of String, Object) From {{"type", "integer"}, {"description", "每个任务每档返回的最新日志条数，1-200，默认 20"}}}
            }))
            tools.Add(FunctionTool("control_queue_tasks", "控制 3FUI 编码队列任务。action 支持 start/pause/resume/stop/remove/reset；用 id/ids 或 1-based index/indexes 指定任务，或 target=all 控制全部任务。停止或移除全部只应在用户明确要求时使用。", New Dictionary(Of String, Object) From {
                {"action", New Dictionary(Of String, Object) From {{"type", "string"}, {"description", "start、pause、resume、stop、remove、reset"}}},
                {"id", New Dictionary(Of String, Object) From {{"type", "string"}, {"description", "单个任务 ID"}}},
                {"ids", New Dictionary(Of String, Object) From {{"type", "array"}, {"items", New Dictionary(Of String, Object) From {{"type", "string"}}}, {"description", "任务 ID 列表"}}},
                {"index", New Dictionary(Of String, Object) From {{"type", "integer"}, {"description", "队列中的 1-based 序号"}}},
                {"indexes", New Dictionary(Of String, Object) From {{"type", "array"}, {"items", New Dictionary(Of String, Object) From {{"type", "integer"}}}, {"description", "队列中的 1-based 序号列表"}}},
                {"target", New Dictionary(Of String, Object) From {{"type", "string"}, {"description", "all/全部 表示控制全部任务"}}},
                {"detail", New Dictionary(Of String, Object) From {{"type", "boolean"}, {"description", "返回控制前后详情，默认 false"}}}
            }, {"action"}))
            tools.Add(FunctionTool("sync_parameter_panel_to_queue", "将当前参数面板预设同步到编码队列中尚未开始的预设任务。只有用户明确要求同步队列时才能调用。", New Dictionary(Of String, Object)))
            tools.Add(FunctionTool("get_ui_tabs", "读取 3FUI 主页面、参数面板、集成工具或嵌套页的选项卡列表和当前选中项。", New Dictionary(Of String, Object) From {
                {"scope", New Dictionary(Of String, Object) From {{"type", "string"}, {"description", "main、parameter、integrated、settings、custom_parameters、attachments"}}}
            }))
            tools.Add(FunctionTool("switch_ui_tab", "切换 3FUI 竖向选项卡或相关嵌套选项卡。", New Dictionary(Of String, Object) From {
                {"scope", New Dictionary(Of String, Object) From {{"type", "string"}}},
                {"tab", New Dictionary(Of String, Object) From {{"type", "string"}, {"description", "选项卡索引或文本"}}}
            }, {"scope", "tab"}))
            tools.Add(FunctionTool("get_prepare_files", "读取准备文件页面的文件列表。", New Dictionary(Of String, Object)))
            tools.Add(FunctionTool("set_prepare_files", "追加、替换或清空准备文件页面的文件列表。", New Dictionary(Of String, Object) From {
                {"paths", New Dictionary(Of String, Object) From {{"type", "array"}, {"items", New Dictionary(Of String, Object) From {{"type", "string"}}}}},
                {"mode", New Dictionary(Of String, Object) From {{"type", "string"}, {"description", "append、replace 或 clear，默认 append"}}}
            }))
            tools.Add(FunctionTool("submit_prepare_files_to_queue", "将准备文件页面中的文件按当前参数面板设置加入编码队列。", New Dictionary(Of String, Object)))
            tools.Add(FunctionTool("get_integrated_tool_state", "读取集成工具页面中合并、混流或抽流的当前状态。", New Dictionary(Of String, Object) From {
                {"tool", New Dictionary(Of String, Object) From {{"type", "string"}, {"description", "merge/合并、mux/混流、extract/抽流"}}}
            }, {"tool"}))
            tools.Add(FunctionTool("configure_integrated_tool", "配置集成工具页面。合并使用 files/output/mode；混流 files 可为对象数组；抽流使用 file/output_location/selected_streams。", New Dictionary(Of String, Object) From {
                {"tool", New Dictionary(Of String, Object) From {{"type", "string"}}},
                {"payload", New Dictionary(Of String, Object) From {{"type", "object"}, {"additionalProperties", True}}}
            }, {"tool", "payload"}))
            tools.Add(FunctionTool("run_integrated_tool", "运行集成工具页面的当前配置。合并/混流添加到编码队列，抽流直接执行。", New Dictionary(Of String, Object) From {
                {"tool", New Dictionary(Of String, Object) From {{"type", "string"}}},
                {"payload", New Dictionary(Of String, Object) From {{"type", "object"}, {"additionalProperties", True}}}
            }, {"tool"}))
            tools.Add(FunctionTool("get_system_hardware", "读取系统硬件概要：处理器名称、内存、显卡名称。", New Dictionary(Of String, Object)))
            tools.Add(FunctionTool("get_parameter_panel_controls", "读取参数面板上的用户直接操作控件和说明控件，包括控件名、类型、文本、当前值、候选项等。", New Dictionary(Of String, Object) From {
                {"query", New Dictionary(Of String, Object) From {{"type", "string"}, {"description", "可选控件名或文本关键词，例如 MCB_视频编码器分类、MCB_输出位置、HCL_质量"}}}
            }))
            tools.Add(FunctionTool("list_parameter_presets", "列出参数面板预设，来源可为用户自定义、从社区下载、开发者内置。", New Dictionary(Of String, Object) From {
                {"source", New Dictionary(Of String, Object) From {{"type", "string"}}}
            }))
            tools.Add(FunctionTool("read_parameter_preset", "读取指定参数预设的 JSON、备注、总览和命令行预览。", New Dictionary(Of String, Object) From {
                {"source", New Dictionary(Of String, Object) From {{"type", "string"}}},
                {"name", New Dictionary(Of String, Object) From {{"type", "string"}}}
            }, {"source", "name"}))
            tools.Add(FunctionTool("apply_parameter_preset", "将指定参数预设加载到参数面板。", New Dictionary(Of String, Object) From {
                {"source", New Dictionary(Of String, Object) From {{"type", "string"}}},
                {"name", New Dictionary(Of String, Object) From {{"type", "string"}}}
            }, {"source", "name"}))
            tools.Add(FunctionTool("save_parameter_preset", "保存参数预设到用户自定义或从社区下载来源。删除权限禁用；开发者内置只允许读取。save_output_location=true 时保存输出位置及保留子文件夹结构起始点；false 时清空这些输出位置字段；不传则沿用界面勾选状态。", New Dictionary(Of String, Object) From {
                {"source", New Dictionary(Of String, Object) From {{"type", "string"}}},
                {"name", New Dictionary(Of String, Object) From {{"type", "string"}}},
                {"preset_json", New Dictionary(Of String, Object) From {{"type", "string"}}},
                {"note", New Dictionary(Of String, Object) From {{"type", "string"}}},
                {"save_output_location", New Dictionary(Of String, Object) From {{"type", "boolean"}, {"description", "是否写入 输出位置 和 输出位置_保留子文件夹结构起始点；不传则使用预设管理页的额外保存输出位置勾选状态。"}}}
            }, {"source", "name"}))
        End If

        Select Case AgentNetworkMode.Normalize(networkMode)
            Case AgentNetworkMode.Endpoint
                tools.Add(FunctionTool("web_search", "联网搜索。使用端点原生 web_search_preview。", New Dictionary(Of String, Object) From {
                    {"query", New Dictionary(Of String, Object) From {{"type", "string"}, {"description", "要搜索的问题"}}}
                }, {"query"}))
            Case AgentNetworkMode.Local
                tools.Add(FunctionTool("web_search", "联网搜索。由你选择搜索引擎，并使用 engine_url 指定完整的搜索 URL，或使用 {query} 作为查询词占位符，例如 https://www.google.com/search?q={query}。", New Dictionary(Of String, Object) From {
                    {"query", New Dictionary(Of String, Object) From {{"type", "string"}, {"description", "要搜索的问题"}}},
                    {"engine_url", New Dictionary(Of String, Object) From {{"type", "string"}, {"description", "自行选择的 HTTP/HTTPS 搜索引擎 URL。必须是已包含查询词的完整 URL，或使用 {query} 占位符。"}}}
                }, {"query", "engine_url"}))
                tools.Add(FunctionTool("fetch_url", "读取指定 URL 的文本或 JSON。请求由 3FUI 在本机发起；可按需填写 User-Agent、Referer、Cookie、自定义请求头、请求方法和请求体。", New Dictionary(Of String, Object) From {
                    {"url", New Dictionary(Of String, Object) From {{"type", "string"}}},
                    {"method", New Dictionary(Of String, Object) From {{"type", "string"}, {"description", "GET、POST、PUT、PATCH、DELETE 等 HTTP 方法，默认 GET"}}},
                    {"headers", New Dictionary(Of String, Object) From {{"type", "object"}, {"additionalProperties", New Dictionary(Of String, Object) From {{"type", "string"}}}}},
                    {"user_agent", New Dictionary(Of String, Object) From {{"type", "string"}}},
                    {"referer", New Dictionary(Of String, Object) From {{"type", "string"}}},
                    {"cookies", New Dictionary(Of String, Object) From {{"type", "string"}}},
                    {"body", New Dictionary(Of String, Object) From {{"type", "string"}}},
                    {"response_format", New Dictionary(Of String, Object) From {{"type", "string"}, {"description", "auto、text、json、raw；默认 auto"}}},
                    {"max_chars", New Dictionary(Of String, Object) From {{"type", "integer"}, {"description", "响应文本最大字符数，1-200000，默认 12000"}}}
                }, {"url"}))
                tools.Add(FunctionTool("http_request", "发起通用 HTTP 请求并返回状态码、响应头、内容类型和响应体摘要。用于 API、JSON、XML 或需要自定义身份标识的站点。", New Dictionary(Of String, Object) From {
                    {"url", New Dictionary(Of String, Object) From {{"type", "string"}}},
                    {"method", New Dictionary(Of String, Object) From {{"type", "string"}}},
                    {"headers", New Dictionary(Of String, Object) From {{"type", "object"}, {"additionalProperties", New Dictionary(Of String, Object) From {{"type", "string"}}}}},
                    {"user_agent", New Dictionary(Of String, Object) From {{"type", "string"}}},
                    {"referer", New Dictionary(Of String, Object) From {{"type", "string"}}},
                    {"cookies", New Dictionary(Of String, Object) From {{"type", "string"}}},
                    {"body", New Dictionary(Of String, Object) From {{"type", "string"}}},
                    {"response_format", New Dictionary(Of String, Object) From {{"type", "string"}}},
                    {"max_chars", New Dictionary(Of String, Object) From {{"type", "integer"}}}
                }, {"url"}))
        End Select

        If permissionLevel >= PermissionSystem Then
            tools.Add(FunctionTool("read_local_text_file", "读取本地文本文件。仅系统访问权限可用，支持按行读取，避免把大文件一次性放入上下文。", New Dictionary(Of String, Object) From {
                {"path", New Dictionary(Of String, Object) From {{"type", "string"}}},
                {"start_line", New Dictionary(Of String, Object) From {{"type", "integer"}, {"description", "1-based 起始行，默认 1"}}},
                {"line_count", New Dictionary(Of String, Object) From {{"type", "integer"}, {"description", "最多读取行数，默认读取到文件末尾，最多 10000 行"}}},
                {"max_chars", New Dictionary(Of String, Object) From {{"type", "integer"}, {"description", "最大返回字符数，1-200000，默认 20000"}}}
            }, {"path"}))
            tools.Add(FunctionTool("write_local_text_file", "写入本地文本文件，使用临时文件替换保证写入过程不会留下半截文件；必要时可创建父目录。仅系统访问权限可用。", New Dictionary(Of String, Object) From {
                {"path", New Dictionary(Of String, Object) From {{"type", "string"}}},
                {"content", New Dictionary(Of String, Object) From {{"type", "string"}}},
                {"encoding", New Dictionary(Of String, Object) From {{"type", "string"}, {"description", "utf-8、utf-8-bom、utf-16，默认 utf-8"}}},
                {"create_directories", New Dictionary(Of String, Object) From {{"type", "boolean"}}}
            }, {"path", "content"}))
            tools.Add(FunctionTool("apply_local_text_patch", "对本地文本文件执行精确 old_text 到 new_text 替换，默认要求恰好匹配一次；用于安全编辑代码而不需要传输 Base64。", New Dictionary(Of String, Object) From {
                {"path", New Dictionary(Of String, Object) From {{"type", "string"}}},
                {"old_text", New Dictionary(Of String, Object) From {{"type", "string"}}},
                {"new_text", New Dictionary(Of String, Object) From {{"type", "string"}}},
                {"replace_all", New Dictionary(Of String, Object) From {{"type", "boolean"}}},
                {"expected_replacements", New Dictionary(Of String, Object) From {{"type", "integer"}}}
            }, {"path", "old_text", "new_text"}))
            tools.Add(FunctionTool("list_directory", "列举本地目录，支持递归和条数上限。仅系统访问权限可用。", New Dictionary(Of String, Object) From {
                {"path", New Dictionary(Of String, Object) From {{"type", "string"}}},
                {"recursive", New Dictionary(Of String, Object) From {{"type", "boolean"}}},
                {"max_items", New Dictionary(Of String, Object) From {{"type", "integer"}}}
            }, {"path"}))
            tools.Add(FunctionTool("create_directory", "创建本地目录（包含不存在的父目录）。仅系统访问权限可用。", New Dictionary(Of String, Object) From {
                {"path", New Dictionary(Of String, Object) From {{"type", "string"}}}
            }, {"path"}))
            tools.Add(FunctionTool("copy_local_file", "复制本地文件。仅系统访问权限可用；目标已存在时默认拒绝覆盖。", New Dictionary(Of String, Object) From {
                {"source", New Dictionary(Of String, Object) From {{"type", "string"}}},
                {"destination", New Dictionary(Of String, Object) From {{"type", "string"}}},
                {"overwrite", New Dictionary(Of String, Object) From {{"type", "boolean"}}}
            }, {"source", "destination"}))
            tools.Add(FunctionTool("move_local_path", "移动本地文件或目录。仅系统访问权限可用；目标已存在时默认拒绝覆盖。", New Dictionary(Of String, Object) From {
                {"source", New Dictionary(Of String, Object) From {{"type", "string"}}},
                {"destination", New Dictionary(Of String, Object) From {{"type", "string"}}},
                {"overwrite", New Dictionary(Of String, Object) From {{"type", "boolean"}}}
            }, {"source", "destination"}))
            tools.Add(FunctionTool("delete_local_path", "删除本地文件或目录。为了避免误删，必须明确传 confirm=true；文件会优先移入回收站。仅系统访问权限可用。", New Dictionary(Of String, Object) From {
                {"path", New Dictionary(Of String, Object) From {{"type", "string"}}},
                {"confirm", New Dictionary(Of String, Object) From {{"type", "boolean"}}}
            }, {"path", "confirm"}))
            tools.Add(FunctionTool("get_image_info", "读取本地图片的宽高、格式和大小，不返回图片 Base64。仅系统访问权限可用。", New Dictionary(Of String, Object) From {
                {"path", New Dictionary(Of String, Object) From {{"type", "string"}}}
            }, {"path"}))
            AddConsoleToolDefinitions(tools)
        End If

        Return tools
    End Function

    Private Shared Sub AddConsoleToolDefinitions(tools As List(Of Dictionary(Of String, Object)))
        tools.Add(FunctionTool("run_powershell", "运行 PowerShell 命令。仅系统访问权限可用。同一次用户消息触发的 Agent 运行会复用同一个 PowerShell 进程，变量、当前位置和模块导入可在本轮多次调用之间保留；会话启动时强制标准输入、标准输出、标准错误、$OutputEncoding 和带 Encoding 参数的文本 cmdlet 使用 UTF-8；本轮响应结束、超时或任务终止时会关闭进程。若本轮首次使用，先验证 $PSVersionTable.PSVersion 和 $PSVersionTable.PSEdition，再选择兼容语法；脚本读写文本仍需显式使用 -Encoding UTF8 或 .NET UTF8Encoding。", New Dictionary(Of String, Object) From {
            {"command", New Dictionary(Of String, Object) From {{"type", "string"}, {"description", "要执行的 PowerShell 命令"}}},
            {"working_directory", New Dictionary(Of String, Object) From {{"type", "string"}, {"description", "可选工作目录。首次调用默认使用程序目录；后续调用默认沿用当前 PowerShell 位置。"}}},
            {"timeout_seconds", New Dictionary(Of String, Object) From {{"type", "integer"}, {"description", "可选超时时间，1-300 秒，默认 60 秒"}}}
        }, {"command"}))
        tools.Add(FunctionTool("run_windows_executable", "直接运行 Windows 可执行文件并传递参数。仅系统访问权限可用；参数使用数组逐项传递，不经过 shell 拼接。返回 exit_code、stdout、stderr 和超时状态。", New Dictionary(Of String, Object) From {
            {"executable", New Dictionary(Of String, Object) From {{"type", "string"}, {"description", "可执行文件路径或 PATH 中的程序名，例如 ffmpeg.exe"}}},
            {"arguments", New Dictionary(Of String, Object) From {{"type", "array"}, {"items", New Dictionary(Of String, Object) From {{"type", "string"}}}, {"description", "按顺序传递给程序的参数，每一项单独填写，不要自行加引号"}}},
            {"working_directory", New Dictionary(Of String, Object) From {{"type", "string"}}},
            {"stdin", New Dictionary(Of String, Object) From {{"type", "string"}, {"description", "可选标准输入文本；传入后程序收到文本并关闭输入流"}}},
            {"timeout_seconds", New Dictionary(Of String, Object) From {{"type", "integer"}, {"description", "1-300 秒，默认 60 秒"}}},
            {"max_output_chars", New Dictionary(Of String, Object) From {{"type", "integer"}, {"description", "stdout/stderr 各自最大字符数，默认 12000"}}}
        }, {"executable"}))
    End Sub

    Public Shared Async Function ExecuteAsync(callInfo As AgentToolCallInfo,
                                              permissionLevel As Integer,
                                              networkMode As Integer,
                                              endpointClient As AgentEndpointClient,
                                              modelId As String,
                                              reasoningEffort As String,
                                              Optional powerShellSession As PowerShellRunSession = Nothing,
                                              Optional cancellationToken As Threading.CancellationToken = Nothing) As Task(Of String)
        Try
            Dim argumentParseError As String = ""
            Dim args = ParseToolArguments(callInfo, argumentParseError)
            If argumentParseError <> "" Then Return argumentParseError

            Select Case callInfo?.Name
                Case "list_agent_skills"
                    Return Agent技能资料库_v6.列出技能()
                Case "read_agent_skill_reference"
                    Return Agent技能资料库_v6.读取资料(Agent通用工具_v6.GetJsonString(args, "skill", "ffmpegfreeui"), Agent通用工具_v6.GetJsonString(args, "reference"))
                Case "get_parameter_panel_state"
                    Return GetParameterPanelState()
                Case "get_parameter_field_info"
                    Return GetParameterFieldInfo(args)
                Case "apply_parameter_panel_patch"
                    Return ApplyParameterPanelPatch(args)
                Case "get_queue_summary"
                    If permissionLevel < PermissionEnvironment Then Return "权限不足：需要环境控制"
                    Return GetQueueSummary(args)
                Case "get_queue_task_logs"
                    If permissionLevel < PermissionEnvironment Then Return "权限不足：需要环境控制"
                    Return GetQueueTaskLogs(args)
                Case "control_queue_tasks"
                    If permissionLevel < PermissionEnvironment Then Return "权限不足：需要环境控制"
                    Return ControlQueueTasks(args)
                Case "sync_parameter_panel_to_queue"
                    If permissionLevel < PermissionEnvironment Then Return "权限不足：需要环境控制"
                    Return SyncParameterPanelToQueue()
                Case "get_ui_tabs"
                    If permissionLevel < PermissionEnvironment Then Return "权限不足：需要环境控制"
                    Return Agent工具封装_v6.获取选项卡(Agent通用工具_v6.GetJsonString(args, "scope"))
                Case "switch_ui_tab"
                    If permissionLevel < PermissionEnvironment Then Return "权限不足：需要环境控制"
                    Return Agent工具封装_v6.切换选项卡(Agent通用工具_v6.GetJsonString(args, "scope"), Agent通用工具_v6.GetJsonString(args, "tab"))
                Case "get_prepare_files"
                    If permissionLevel < PermissionEnvironment Then Return "权限不足：需要环境控制"
                    Return Agent工具封装_v6.获取准备文件()
                Case "set_prepare_files"
                    If permissionLevel < PermissionEnvironment Then Return "权限不足：需要环境控制"
                    Return Agent工具封装_v6.设置准备文件(Agent通用工具_v6.GetJsonStringArray(args, "paths"), Agent通用工具_v6.GetJsonString(args, "mode"))
                Case "submit_prepare_files_to_queue"
                    If permissionLevel < PermissionEnvironment Then Return "权限不足：需要环境控制"
                    Return Agent工具封装_v6.提交准备文件到队列()
                Case "get_integrated_tool_state"
                    If permissionLevel < PermissionEnvironment Then Return "权限不足：需要环境控制"
                    Return Agent工具封装_v6.获取集成工具状态(Agent通用工具_v6.GetJsonString(args, "tool"))
                Case "configure_integrated_tool"
                    If permissionLevel < PermissionEnvironment Then Return "权限不足：需要环境控制"
                    Return Await Agent工具封装_v6.配置集成工具Async(Agent通用工具_v6.GetJsonString(args, "tool"), Agent通用工具_v6.GetJsonObject(args, "payload"))
                Case "run_integrated_tool"
                    If permissionLevel < PermissionEnvironment Then Return "权限不足：需要环境控制"
                    Return Await Agent工具封装_v6.运行集成工具Async(Agent通用工具_v6.GetJsonString(args, "tool"), Agent通用工具_v6.GetJsonObject(args, "payload"))
                Case "get_system_hardware"
                    If permissionLevel < PermissionEnvironment Then Return "权限不足：需要环境控制"
                    Return Agent工具封装_v6.获取系统硬件()
                Case "get_parameter_panel_controls"
                    If permissionLevel < PermissionEnvironment Then Return "权限不足：需要环境控制"
                    Return Agent工具封装_v6.获取参数面板控件信息(Agent通用工具_v6.GetJsonString(args, "query"))
                Case "list_parameter_presets"
                    If permissionLevel < PermissionEnvironment Then Return "权限不足：需要环境控制"
                    Return Agent工具封装_v6.列出参数预设(Agent通用工具_v6.GetJsonString(args, "source"))
                Case "read_parameter_preset"
                    If permissionLevel < PermissionEnvironment Then Return "权限不足：需要环境控制"
                    Return Agent工具封装_v6.读取参数预设(Agent通用工具_v6.GetJsonString(args, "source"), Agent通用工具_v6.GetJsonString(args, "name"))
                Case "apply_parameter_preset"
                    If permissionLevel < PermissionEnvironment Then Return "权限不足：需要环境控制"
                    Return Agent工具封装_v6.应用参数预设(Agent通用工具_v6.GetJsonString(args, "source"), Agent通用工具_v6.GetJsonString(args, "name"))
                Case "save_parameter_preset"
                    If permissionLevel < PermissionEnvironment Then Return "权限不足：需要环境控制"
                    Dim saveOutputLocation As Boolean? = Nothing
                    If HasJsonProperty(args, "save_output_location") Then saveOutputLocation = Agent通用工具_v6.GetJsonBoolean(args, "save_output_location", False)
                    Return Agent工具封装_v6.保存参数预设(Agent通用工具_v6.GetJsonString(args, "source"), Agent通用工具_v6.GetJsonString(args, "name"), Agent通用工具_v6.GetJsonString(args, "preset_json"), Agent通用工具_v6.GetJsonString(args, "note"), saveOutputLocation)
                Case "web_search"
                    If Not AgentNetworkMode.IsEnabled(networkMode) Then Return "联网已禁用"
                    Return Await WebSearchAsync(Agent通用工具_v6.GetJsonString(args, "query"), Agent通用工具_v6.GetJsonString(args, "engine_url"), networkMode, endpointClient, modelId, reasoningEffort, cancellationToken)
                Case "fetch_url"
                    If Not AgentNetworkMode.IsEnabled(networkMode) Then Return "联网已禁用"
                    If AgentNetworkMode.Normalize(networkMode) <> AgentNetworkMode.Local Then Return "当前联网模式不允许本地网页请求"
                    Return Await FetchUrlAsync(Agent通用工具_v6.GetJsonString(args, "url"), cancellationToken,
                                               Agent通用工具_v6.GetJsonString(args, "method", "GET"),
                                               ReadHeaderMap(args),
                                               Agent通用工具_v6.GetJsonString(args, "user_agent"),
                                               Agent通用工具_v6.GetJsonString(args, "referer"),
                                               Agent通用工具_v6.GetJsonString(args, "cookies"),
                                               Agent通用工具_v6.GetJsonString(args, "body"),
                                               Agent通用工具_v6.GetJsonInteger(args, "max_chars", 12000),
                                               Agent通用工具_v6.GetJsonString(args, "response_format", "auto"), False)
                Case "http_request"
                    If Not AgentNetworkMode.IsEnabled(networkMode) Then Return "联网已禁用"
                    If AgentNetworkMode.Normalize(networkMode) <> AgentNetworkMode.Local Then Return "当前联网模式不允许本地 HTTP 请求"
                    Return Await FetchUrlAsync(Agent通用工具_v6.GetJsonString(args, "url"), cancellationToken,
                                               Agent通用工具_v6.GetJsonString(args, "method", "GET"),
                                               ReadHeaderMap(args),
                                               Agent通用工具_v6.GetJsonString(args, "user_agent"),
                                               Agent通用工具_v6.GetJsonString(args, "referer"),
                                               Agent通用工具_v6.GetJsonString(args, "cookies"),
                                               Agent通用工具_v6.GetJsonString(args, "body"),
                                               Agent通用工具_v6.GetJsonInteger(args, "max_chars", 20000),
                                               Agent通用工具_v6.GetJsonString(args, "response_format", "auto"), True)
                Case "read_local_text_file"
                    If permissionLevel < PermissionSystem Then Return "权限不足：需要系统访问"
                    Return ReadLocalTextFile(Agent通用工具_v6.GetJsonString(args, "path"),
                                             Agent通用工具_v6.GetJsonInteger(args, "start_line", 1),
                                             Agent通用工具_v6.GetJsonInteger(args, "line_count", 0),
                                             Agent通用工具_v6.GetJsonInteger(args, "max_chars", 20000))
                Case "write_local_text_file"
                    If permissionLevel < PermissionSystem Then Return "权限不足：需要系统访问"
                    Return WriteLocalTextFile(Agent通用工具_v6.GetJsonString(args, "path"),
                                              Agent通用工具_v6.GetJsonString(args, "content"),
                                              Agent通用工具_v6.GetJsonString(args, "encoding", "utf-8"),
                                              Agent通用工具_v6.GetJsonBoolean(args, "create_directories", False))
                Case "apply_local_text_patch"
                    If permissionLevel < PermissionSystem Then Return "权限不足：需要系统访问"
                    Return ApplyLocalTextPatch(Agent通用工具_v6.GetJsonString(args, "path"),
                                               Agent通用工具_v6.GetJsonString(args, "old_text"),
                                               Agent通用工具_v6.GetJsonString(args, "new_text"),
                                               Agent通用工具_v6.GetJsonBoolean(args, "replace_all", False),
                                               Agent通用工具_v6.GetJsonInteger(args, "expected_replacements", 0))
                Case "list_directory"
                    If permissionLevel < PermissionSystem Then Return "权限不足：需要系统访问"
                    Return ListDirectory(Agent通用工具_v6.GetJsonString(args, "path"),
                                         Agent通用工具_v6.GetJsonBoolean(args, "recursive", False),
                                         Agent通用工具_v6.GetJsonInteger(args, "max_items", 200))
                Case "create_directory"
                    If permissionLevel < PermissionSystem Then Return "权限不足：需要系统访问"
                    Return CreateDirectory(Agent通用工具_v6.GetJsonString(args, "path"))
                Case "copy_local_file"
                    If permissionLevel < PermissionSystem Then Return "权限不足：需要系统访问"
                    Return CopyLocalFile(Agent通用工具_v6.GetJsonString(args, "source"), Agent通用工具_v6.GetJsonString(args, "destination"), Agent通用工具_v6.GetJsonBoolean(args, "overwrite", False))
                Case "move_local_path"
                    If permissionLevel < PermissionSystem Then Return "权限不足：需要系统访问"
                    Return MoveLocalPath(Agent通用工具_v6.GetJsonString(args, "source"), Agent通用工具_v6.GetJsonString(args, "destination"), Agent通用工具_v6.GetJsonBoolean(args, "overwrite", False))
                Case "delete_local_path"
                    If permissionLevel < PermissionSystem Then Return "权限不足：需要系统访问"
                    If Not Agent通用工具_v6.GetJsonBoolean(args, "confirm", False) Then Return "拒绝删除：必须传 confirm=true"
                    Return DeleteLocalPath(Agent通用工具_v6.GetJsonString(args, "path"))
                Case "get_image_info"
                    If permissionLevel < PermissionSystem Then Return "权限不足：需要系统访问"
                    Return GetImageInfo(Agent通用工具_v6.GetJsonString(args, "path"))
                Case "run_powershell"
                    Return Await RunConsoleToolAsync(permissionLevel, powerShellSession, args, cancellationToken, "PowerShell")
                Case "run_windows_executable"
                    If permissionLevel < PermissionSystem Then Return "权限不足：需要系统访问"
                    Return Await RunWindowsExecutableAsync(args, cancellationToken)
                Case Else
                    Return $"未知工具：{callInfo?.Name}"
            End Select
        Catch ex As OperationCanceledException When cancellationToken.IsCancellationRequested
            Throw
        Catch ex As Exception
            Return $"工具执行失败：{ex.Message}"
        End Try
    End Function

    Private Shared Function ParseToolArguments(callInfo As AgentToolCallInfo, ByRef errorMessage As String) As JsonElement
        errorMessage = ""
        Dim raw = If(callInfo?.Arguments, "")
        Dim rawArgumentProperty = GetRawConsoleArgumentPropertyName(callInfo?.Name)
        Try
            Dim parsed = Agent通用工具_v6.ParseJsonArguments(raw)
            If parsed.ValueKind = JsonValueKind.Object Then Return parsed

            If rawArgumentProperty <> "" AndAlso parsed.ValueKind = JsonValueKind.String Then
                Return NormalizeToolArguments(callInfo, rawArgumentProperty, If(parsed.GetString(), ""))
            End If

            callInfo.Arguments = "{}"
            errorMessage = "工具参数必须是 JSON 对象。原始参数：" & Agent通用工具_v6.LimitText(raw, 4000)
            Return BuildEmptyJsonElement()
        Catch ex As Exception
            If rawArgumentProperty <> "" Then Return NormalizeToolArguments(callInfo, rawArgumentProperty, raw)

            callInfo.Arguments = "{}"
            errorMessage = "工具参数 JSON 解析失败：" & ex.Message & vbCrLf &
                           "原始参数：" & Agent通用工具_v6.LimitText(raw, 4000)
            Return BuildEmptyJsonElement()
        End Try
    End Function

    Private Shared Function GetRawConsoleArgumentPropertyName(toolName As String) As String
        Select Case If(toolName, "")
            Case "run_powershell"
                Return "command"
            Case Else
                Return ""
        End Select
    End Function

    Private Shared Function NormalizeToolArguments(callInfo As AgentToolCallInfo, propertyName As String, value As String) As JsonElement
        Dim normalizedJson = BuildSingleStringJson(propertyName, value)
        callInfo.Arguments = normalizedJson
        Using doc = JsonDocument.Parse(normalizedJson)
            Return doc.RootElement.Clone()
        End Using
    End Function

    Private Shared Function BuildSingleStringJson(propertyName As String, value As String) As String
        Dim payload As New Dictionary(Of String, String) From {
            {propertyName, If(value, "")}
        }
        Return JsonSerializer.Serialize(payload)
    End Function

    Private Shared Function BuildEmptyJsonElement() As JsonElement
        Using doc = JsonDocument.Parse("{}")
            Return doc.RootElement.Clone()
        End Using
    End Function

    Private Shared Async Function RunConsoleToolAsync(permissionLevel As Integer,
                                                      session As ConsoleRunSession,
                                                      args As JsonElement,
                                                      cancellationToken As Threading.CancellationToken,
                                                      displayName As String) As Task(Of String)
        If permissionLevel < PermissionSystem Then Return "权限不足：需要系统访问"
        If session Is Nothing Then Return displayName & " 会话不可用。"
        Return Await session.ExecuteAsync(args, cancellationToken)
    End Function

    Private Shared Async Function RunWindowsExecutableAsync(args As JsonElement,
                                                            cancellationToken As Threading.CancellationToken) As Task(Of String)
        Dim executable = Agent通用工具_v6.GetJsonString(args, "executable").Trim()
        If executable = "" Then Return "缺少 executable"

        Dim workingDirectory = Agent通用工具_v6.GetJsonString(args, "working_directory").Trim()
        If workingDirectory = "" Then workingDirectory = Application.StartupPath
        If Not Directory.Exists(workingDirectory) Then Return "工作目录不存在：" & workingDirectory

        Dim timeoutSeconds = Math.Min(Math.Max(Agent通用工具_v6.GetJsonInteger(args, "timeout_seconds", 60), 1), 300)
        Dim maxOutputCharacters = Math.Min(Math.Max(Agent通用工具_v6.GetJsonInteger(args, "max_output_chars", 12000), 1), 200000)
        Dim arguments = Agent通用工具_v6.GetJsonStringArray(args, "arguments", False)
        Dim process As New Process With {
            .StartInfo = New ProcessStartInfo With {
                .FileName = executable,
                .WorkingDirectory = workingDirectory,
                .UseShellExecute = False,
                .CreateNoWindow = True,
                .RedirectStandardInput = True,
                .RedirectStandardOutput = True,
                .RedirectStandardError = True,
                .StandardInputEncoding = New UTF8Encoding(False),
                .StandardOutputEncoding = New UTF8Encoding(False),
                .StandardErrorEncoding = New UTF8Encoding(False)
            }
        }
        For Each argument In arguments
            process.StartInfo.ArgumentList.Add(argument)
        Next

        Dim stopwatch = Diagnostics.Stopwatch.StartNew()
        Dim timedOut = False
        Dim cancelled = False
        Dim exitCode = -1
        Dim stdout = ""
        Dim stderr = ""
        Try
            If Not process.Start() Then Return "无法启动可执行文件：" & executable
            Dim stdoutTask = process.StandardOutput.ReadToEndAsync()
            Dim stderrTask = process.StandardError.ReadToEndAsync()
            Dim stdinText = Agent通用工具_v6.GetJsonString(args, "stdin")
            If HasJsonProperty(args, "stdin") AndAlso stdinText <> "" Then
                Await process.StandardInput.WriteAsync(stdinText)
            End If
            process.StandardInput.Close()

            Using linkedCts = Threading.CancellationTokenSource.CreateLinkedTokenSource(cancellationToken)
                linkedCts.CancelAfter(TimeSpan.FromSeconds(timeoutSeconds))
                Dim waitForExitAfterKill = False
                Try
                    Await Task.WhenAll(New Task() {stdoutTask, stderrTask, process.WaitForExitAsync(linkedCts.Token)})
                Catch ex As OperationCanceledException
                    cancelled = cancellationToken.IsCancellationRequested
                    timedOut = Not cancelled
                    waitForExitAfterKill = True
                    Try
                        If Not process.HasExited Then
                            Try
                                process.Kill(True)
                            Catch
                                process.Kill()
                            End Try
                        End If
                    Catch
                    End Try
                End Try
                If waitForExitAfterKill Then
                    Try
                        Await process.WaitForExitAsync(Threading.CancellationToken.None)
                    Catch
                    End Try
                End If
            End Using

            Try
                Await Task.WhenAll(New Task() {stdoutTask, stderrTask})
            Catch
            End Try
            If process.HasExited Then exitCode = process.ExitCode
            stdout = Agent通用工具_v6.LimitText(stdoutTask.Result, maxOutputCharacters, "...[truncated]")
            stderr = Agent通用工具_v6.LimitText(stderrTask.Result, maxOutputCharacters, "...[truncated]")
        Catch ex As OperationCanceledException When cancellationToken.IsCancellationRequested
            cancelled = True
            Throw
        Catch ex As Exception
            stderr = ex.Message
        Finally
            stopwatch.Stop()
            Try
                If Not process.HasExited Then process.Kill(True)
            Catch
            End Try
            process.Dispose()
        End Try

        If cancelled Then Throw New OperationCanceledException(cancellationToken)
        Return JsonSerializer.Serialize(New Dictionary(Of String, Object) From {
            {"success", Not timedOut AndAlso exitCode = 0},
            {"executable", executable},
            {"working_directory", workingDirectory},
            {"arguments", arguments},
            {"exit_code", exitCode},
            {"timed_out", timedOut},
            {"elapsed_ms", CLng(stopwatch.Elapsed.TotalMilliseconds)},
            {"stdout", stdout},
            {"stderr", stderr}
        }, JsonSO)
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

    Private Shared Function GetParameterPanelState() As String
        Dim preset = 预设管理_v6.从面板创建预设(Form_v6_参数面板)
        Dim overview = BuildParameterOverview(preset)
        Dim payload As New Dictionary(Of String, Object) From {
            {"overview", overview},
            {"command_preview", BuildParameterCommandPreview(preset)},
            {"preset_json", JsonSerializer.Serialize(preset, JsonSO)}
        }
        Return JsonSerializer.Serialize(payload, JsonSO)
    End Function

    Private Shared Function ApplyParameterPanelPatch(args As JsonElement) As String
        Dim presetJson As JsonElement
        Dim preset As 预设数据_v6
        Dim previousPreset = 预设管理_v6.从面板创建预设(Form_v6_参数面板)
        Dim explicitFilterOrderChange As Boolean = False
        Dim requestedChanges As New List(Of Dictionary(Of String, Object))
        If args.ValueKind = JsonValueKind.Object AndAlso args.TryGetProperty("preset_json", presetJson) AndAlso presetJson.ValueKind = JsonValueKind.String AndAlso presetJson.GetString() <> "" Then
            preset = JsonSerializer.Deserialize(Of 预设数据_v6)(presetJson.GetString(), JsonSO)
            explicitFilterOrderChange = JsonObjectHasProperty(presetJson.GetString(), NameOf(预设数据_v6.滤镜排序系统))
            requestedChanges.Add(New Dictionary(Of String, Object) From {
                {"field", "preset_json"},
                {"mode", "replace_all"}
            })
        Else
            preset = ClonePresetData(previousPreset)
            Dim changes As JsonElement
            If args.ValueKind = JsonValueKind.Object AndAlso args.TryGetProperty("changes", changes) AndAlso changes.ValueKind = JsonValueKind.Object Then
                explicitFilterOrderChange = HasJsonProperty(changes, NameOf(预设数据_v6.滤镜排序系统))
                requestedChanges = ApplyTopLevelChanges(preset, changes)
            Else
                Return "没有提供 changes 或 preset_json"
            End If
        End If

        Dim noteElement As JsonElement
        If args.ValueKind = JsonValueKind.Object AndAlso args.TryGetProperty("note", noteElement) Then
            Dim oldNote = preset.预设备注
            preset.预设备注 = If(noteElement.ValueKind = JsonValueKind.Null OrElse noteElement.ValueKind = JsonValueKind.Undefined, "", Agent通用工具_v6.GetJsonString(args, "note"))
            requestedChanges.Add(New Dictionary(Of String, Object) From {
                {"field", NameOf(预设数据_v6.预设备注)},
                {"before", oldNote},
                {"requested", preset.预设备注}
            })
        End If

        If explicitFilterOrderChange Then 预设管理_v6.应用Agent滤镜排序请求(preset, previousPreset)
        预设管理_v6.显示预设(preset, Form_v6_参数面板)
        Dim actualPreset = 预设管理_v6.从面板创建预设(Form_v6_参数面板)
        Dim payload As New Dictionary(Of String, Object) From {
            {"message", "已应用参数面板修改"},
            {"requested_changes", requestedChanges},
            {"effective_changed_fields", BuildChangedFieldList(previousPreset, actualPreset)},
            {"filter_order_requested", explicitFilterOrderChange},
            {"overview", BuildParameterOverview(actualPreset)},
            {"command_preview", BuildParameterCommandPreview(actualPreset)},
            {"preset_json", JsonSerializer.Serialize(actualPreset, JsonSO)}
        }
        Return JsonSerializer.Serialize(payload, JsonSO)
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
        Dim includeCurrentValues = Agent通用工具_v6.GetJsonBoolean(args, "include_current_values", True)
        Dim requestedFields = Agent通用工具_v6.GetJsonStringArray(args, "fields")
        Dim query = Agent通用工具_v6.GetJsonString(args, "query").Trim()
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
                Select(Function(prop) New With {.Prop = prop, .Score = GetPropertyMatchScore(query, prop.Name)}).
                Where(Function(x) x.Score > 0).
                OrderByDescending(Function(x) x.Score).
                ThenBy(Function(x) x.Prop.Name, StringComparer.CurrentCultureIgnoreCase).
                Select(Function(x) x.Prop).
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
                Return BuildStringCandidates(Agent工具封装_v6.获取输出容器候选())
            Case NameOf(预设数据_v6.输出_自动命名选项)
                Return BuildAutoNameOptionCandidates()
            Case NameOf(预设数据_v6.视频参数_编码器_分类名称)
                Return BuildVideoCategoryCandidates(preset)
            Case NameOf(预设数据_v6.视频参数_编码器_具体编码)
                Return BuildVideoEncoderCandidates(preset)
            Case NameOf(预设数据_v6.视频参数_编码器_编码预设)
                Return BuildVideoParameterListCandidates(preset, 视频编码器数据库_v6.编码器参数角色.编码预设)
            Case NameOf(预设数据_v6.视频参数_编码器_配置文件)
                Return BuildVideoParameterListCandidates(preset, 视频编码器数据库_v6.编码器参数角色.配置文件)
            Case NameOf(预设数据_v6.视频参数_编码器_场景优化)
                Return BuildVideoParameterListCandidates(preset, 视频编码器数据库_v6.编码器参数角色.场景优化)
            Case NameOf(预设数据_v6.视频参数_色彩管理_像素格式)
                Return BuildVideoParameterListCandidates(preset, 视频编码器数据库_v6.编码器参数角色.像素格式)
            Case NameOf(预设数据_v6.视频参数_色彩管理_像素格式预先转换)
                Return BuildStringCandidates(Agent工具封装_v6.获取组合框候选("MCB_像素格式预先转换"))
            Case NameOf(预设数据_v6.视频参数_质量控制_参数名)
                Return BuildStringCandidates(视频编码器数据库_v6.获取质量参数名列表())
            Case NameOf(预设数据_v6.音频参数_编码器_代号)
                Return BuildAudioEncoderCandidates()
            Case NameOf(预设数据_v6.音频参数_质量参数名),
                 NameOf(预设数据_v6.音频参数_质量参数名2)
                Return BuildStringCandidates(音频编码器数据库_v6.获取质量参数名列表())
        End Select
        Return New List(Of Object)
    End Function

    Private Shared Function BuildStringCandidates(values As IEnumerable(Of String)) As List(Of Object)
        Dim result As New List(Of Object)
        If values Is Nothing Then Return result

        Dim seen As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
        For Each rawValue In values
            Dim value = If(rawValue, "")
            If seen.Add(value) Then result.Add(value)
        Next
        Return result
    End Function

    Private Shared Function BuildVideoCategoryCandidates(preset As 预设数据_v6) As List(Of Object)
        Return 视频编码器数据库_v6.获取分类列表(preset.视频参数_编码器_类型).
            Select(Function(x) CType(New Dictionary(Of String, Object) From {
                {"value", x.名称},
                {"description", x.描述}
            }, Object)).
            ToList()
    End Function

    Private Shared Function BuildVideoEncoderCandidates(preset As 预设数据_v6) As List(Of Object)
        Return 视频编码器数据库_v6.获取编码器列表(preset.视频参数_编码器_分类名称).
            Select(Function(x) CType(New Dictionary(Of String, Object) From {
                {"value", x.名称},
                {"command", x.命令行编码器名},
                {"type", x.类型.ToString()}
            }, Object)).
            ToList()
    End Function

    Private Shared Function BuildAudioEncoderCandidates() As List(Of Object)
        Return 音频编码器数据库_v6.全部编码器.
            Select(Function(x) CType(New Dictionary(Of String, Object) From {
                {"value", x.私有ID},
                {"label", x.显示名称},
                {"command", x.命令行编码器名}
            }, Object)).
            ToList()
    End Function

    Private Shared Function BuildAutoNameOptionCandidates() As List(Of Object)
        Return New List(Of Object) From {
            New Dictionary(Of String, Object) From {{"name", NameOf(预设数据_v6.自动命名选项.不使用自动命名)}, {"value", CInt(预设数据_v6.自动命名选项.不使用自动命名)}, {"description", "不附加自动后缀，容易覆盖目标文件。"}},
            New Dictionary(Of String, Object) From {{"name", NameOf(预设数据_v6.自动命名选项.附加_递增时间戳)}, {"value", CInt(预设数据_v6.自动命名选项.附加_递增时间戳)}, {"description", "默认选项；追加 _yyyy.MM.dd-HH.mm.ss，若文件名结尾已有同格式时间戳则替换它。"}},
            New Dictionary(Of String, Object) From {{"name", NameOf(预设数据_v6.自动命名选项.附加_递增数字)}, {"value", CInt(预设数据_v6.自动命名选项.附加_递增数字)}, {"description", "目标存在时使用 ~1、~2 递增。"}},
            New Dictionary(Of String, Object) From {{"name", NameOf(预设数据_v6.自动命名选项.附加_3FUI)}, {"value", CInt(预设数据_v6.自动命名选项.附加_3FUI)}, {"description", "附加 _3fui。"}},
            New Dictionary(Of String, Object) From {{"name", NameOf(预设数据_v6.自动命名选项.常规压片_附加编码器和质量参数)}, {"value", CInt(预设数据_v6.自动命名选项.常规压片_附加编码器和质量参数)}, {"description", "根据编码器、预设、质量、码率等参数生成后缀。"}},
            New Dictionary(Of String, Object) From {{"name", NameOf(预设数据_v6.自动命名选项.附加_随机8位数字)}, {"value", CInt(预设数据_v6.自动命名选项.附加_随机8位数字)}, {"description", "附加 _ 加 8 位随机数字。"}},
            New Dictionary(Of String, Object) From {{"name", NameOf(预设数据_v6.自动命名选项.附加_随机8位字母)}, {"value", CInt(预设数据_v6.自动命名选项.附加_随机8位字母)}, {"description", "附加 _ 加 8 位随机字母。"}},
            New Dictionary(Of String, Object) From {{"name", NameOf(预设数据_v6.自动命名选项.附加_随机8位数字和字母组合)}, {"value", CInt(预设数据_v6.自动命名选项.附加_随机8位数字和字母组合)}, {"description", "附加 _ 加 8 位随机数字字母。"}},
            New Dictionary(Of String, Object) From {{"name", NameOf(预设数据_v6.自动命名选项.附加_随机16位数字)}, {"value", CInt(预设数据_v6.自动命名选项.附加_随机16位数字)}, {"description", "附加 _ 加 16 位随机数字。"}},
            New Dictionary(Of String, Object) From {{"name", NameOf(预设数据_v6.自动命名选项.附加_随机16位字母)}, {"value", CInt(预设数据_v6.自动命名选项.附加_随机16位字母)}, {"description", "附加 _ 加 16 位随机字母。"}},
            New Dictionary(Of String, Object) From {{"name", NameOf(预设数据_v6.自动命名选项.附加_随机16位数字和字母组合)}, {"value", CInt(预设数据_v6.自动命名选项.附加_随机16位数字和字母组合)}, {"description", "附加 _ 加 16 位随机数字字母。"}},
            New Dictionary(Of String, Object) From {{"name", NameOf(预设数据_v6.自动命名选项.附加_2位结尾序号)}, {"value", CInt(预设数据_v6.自动命名选项.附加_2位结尾序号)}, {"description", "直接在文件名末尾附加 01、02；只在同输出后缀目标存在时递增，不自带空格或下划线。"}},
            New Dictionary(Of String, Object) From {{"name", NameOf(预设数据_v6.自动命名选项.附加_3位结尾序号)}, {"value", CInt(预设数据_v6.自动命名选项.附加_3位结尾序号)}, {"description", "直接在文件名末尾附加 001、002；只在同输出后缀目标存在时递增，不自带空格或下划线。"}}
        }
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
        Dim result As New List(Of Object) From {""}
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
                Return New List(Of String) From {"后缀必须包含点号，例如 .mp4；空字符串表示不指定或由输出路径决定。", "候选值来自当前输出文件设置页的后缀菜单，会随菜单项增删变化；菜单外的自定义后缀也可填写。"}
            Case NameOf(预设数据_v6.输出位置)
                Return New List(Of String) From {"必须是已存在文件夹才会作为自定义输出目录生效；空字符串表示默认输出到输入文件原目录。", "设置为默认原目录时必须同时清空 输出位置_保留子文件夹结构起始点。"}
            Case NameOf(预设数据_v6.输出位置_保留子文件夹结构起始点)
                Return New List(Of String) From {"必须是已存在文件夹；路径不存在时界面会清空。", "只有 输出位置 是有效自定义输出目录时才生效；默认原目录与此逻辑不兼容。", "它保存到预设时属于额外保存输出位置的一部分；调用 save_parameter_preset 保存时需要传 save_output_location=true。"}
            Case NameOf(预设数据_v6.输出_自动命名选项)
                Return New List(Of String) From {"可传枚举名或数值。默认 附加_递增时间戳 会替换文件名结尾已有的 _yyyy.MM.dd-HH.mm.ss，否则追加新时间戳。", "附加_2位结尾序号 和 附加_3位结尾序号 始终从 01/001 开始递增，判断已占用序号时只检查相同输出后缀，且直接附加在扩展名前，不包含空格、下划线或其他分隔符。"}
            Case NameOf(预设数据_v6.输出命名_结尾文本)
                Return New List(Of String) From {"若希望结尾序号前有空格、下划线或其他分隔符，需要把分隔符写在此字段；2 位/3 位结尾序号功能只负责直接拼数字。"}
            Case NameOf(预设数据_v6.视频参数_质量控制_参数名)
                Return New List(Of String) From {"面板显示带横杠的 FFmpeg 参数名；保存预设时会自动去掉开头横杠，changes 中可传 crf 或 -crf。"}
            Case NameOf(预设数据_v6.视频参数_色彩管理_像素格式)
                Return New List(Of String) From {"此字段对应可编辑下拉框，候选取决于当前视频编码器，只代表编码器数据库中的常见或建议值。", "可填写 FFmpeg/当前编码器支持的其他像素格式；生成命令时会写为 -pix_fmt <值>，changes 中只传像素格式值，不要带 -pix_fmt。"}
            Case NameOf(预设数据_v6.视频参数_色彩管理_像素格式预先转换)
                Return New List(Of String) From {"此字段对应可编辑下拉框，候选值只是常用建议，会随控件 Items 增删变化。", "可填写任意 FFmpeg 支持的像素格式；生成命令时会写为 format=<值>，changes 中只传像素格式值，不要带 format=。"}
            Case NameOf(预设数据_v6.音频参数_编码器_代号)
                Return New List(Of String) From {"changes 中优先传 value 的私有 ID，不要传显示名称；界面会自动显示对应名称。"}
            Case NameOf(预设数据_v6.音频参数_质量参数名),
                 NameOf(预设数据_v6.音频参数_质量参数名2)
                Return New List(Of String) From {"音频质量参数名保存时保留横杠，例如 -q:a、-b:a。", "第二组音频质量参数会在命令中紧跟第一组之后写入，可用于同时设置两个编码器参数，例如 -vbr 与 -aac_nmr_speed。"}
        End Select
        Return New List(Of String)
    End Function

    Private Shared Function GetKnownParameterNotes(fieldName As String, preset As 预设数据_v6) As List(Of String)
        Dim notes As New List(Of String)
        Select Case fieldName
            Case NameOf(预设数据_v6.输出位置)
                notes.Add($"当前 {NameOf(预设数据_v6.输出位置_保留子文件夹结构起始点)} 为：{preset.输出位置_保留子文件夹结构起始点}")
            Case NameOf(预设数据_v6.输出位置_保留子文件夹结构起始点)
                notes.Add($"当前 {NameOf(预设数据_v6.输出位置)} 为：{preset.输出位置}")
                notes.Add("Agent 修改此字段后应重读 get_parameter_panel_state 验证；如果输出位置为默认原目录或路径不存在，界面会清空它。")
            Case NameOf(预设数据_v6.输出_自动命名选项)
                notes.Add($"当前开头/替代/结尾文本为：{preset.输出命名_开头文本} / {preset.输出命名_替代文本} / {preset.输出命名_结尾文本}")
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

    Private Shared Function ApplyTopLevelChanges(preset As 预设数据_v6, changes As JsonElement) As List(Of Dictionary(Of String, Object))
        Dim type = GetType(预设数据_v6)
        Dim props = type.GetProperties().Where(Function(x) x.CanRead AndAlso x.CanWrite).ToList()
        Dim result As New List(Of Dictionary(Of String, Object))
        For Each item In changes.EnumerateObject()
            Dim prop = ResolvePresetProperty(item.Name, props)
            If prop Is Nothing Then
                Dim suggestions = GetPropertySuggestions(item.Name, props)
                Dim suffix = If(suggestions.Count = 0, "", "。相近字段：" & String.Join("、", suggestions))
                Throw New InvalidOperationException($"未知或不可写属性：{item.Name}{suffix}")
            End If
            Dim before = prop.GetValue(preset)
            Dim value = DeserializeJsonElement(item.Value, prop.PropertyType)
            prop.SetValue(preset, value)
            result.Add(New Dictionary(Of String, Object) From {
                {"field", prop.Name},
                {"before", before},
                {"requested", value}
            })
        Next
        Return result
    End Function

    Private Shared Function DeserializeJsonElement(element As JsonElement, targetType As Type) As Object
        If element.ValueKind = JsonValueKind.Null OrElse element.ValueKind = JsonValueKind.Undefined Then
            Return GetDefaultJsonPatchValue(targetType)
        End If

        If targetType Is GetType(String) Then
            If element.ValueKind = JsonValueKind.String Then Return element.GetString()
            Return element.GetRawText()
        End If
        If targetType Is GetType(Boolean) Then
            If element.ValueKind = JsonValueKind.True OrElse element.ValueKind = JsonValueKind.False Then Return element.GetBoolean()
            If element.ValueKind = JsonValueKind.String Then
                Dim boolValue As Boolean
                If Boolean.TryParse(element.GetString(), boolValue) Then Return boolValue
            End If
            If element.ValueKind = JsonValueKind.Number Then
                Dim intValue As Integer
                If element.TryGetInt32(intValue) Then Return intValue <> 0
            End If
            Throw New InvalidOperationException("布尔字段必须传 true/false。")
        End If
        If targetType Is GetType(Integer) Then
            If element.ValueKind = JsonValueKind.Number Then Return element.GetInt32()
            If element.ValueKind = JsonValueKind.String Then
                Dim intValue As Integer
                If Integer.TryParse(element.GetString(), intValue) Then Return intValue
            End If
            Throw New InvalidOperationException("整数字段必须传数字或可解析的数字字符串。")
        End If
        If targetType Is GetType(Double) Then
            If element.ValueKind = JsonValueKind.Number Then Return element.GetDouble()
            If element.ValueKind = JsonValueKind.String Then
                Dim doubleValue As Double
                If Double.TryParse(element.GetString(), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture, doubleValue) Then Return doubleValue
            End If
            Throw New InvalidOperationException("数字字段必须传数字或可解析的数字字符串。")
        End If
        If targetType.IsEnum Then
            If element.ValueKind = JsonValueKind.String Then
                Dim text = element.GetString()
                Dim intValue As Integer
                If Integer.TryParse(text, intValue) Then Return [Enum].ToObject(targetType, intValue)
                Try
                    Return [Enum].Parse(targetType, text, True)
                Catch ex As Exception
                    Throw New InvalidOperationException($"枚举字段 {targetType.Name} 不包含值：{text}。可用值：{String.Join("、", [Enum].GetNames(targetType))}")
                End Try
            End If
            Return [Enum].ToObject(targetType, element.GetInt32())
        End If
        Return JsonSerializer.Deserialize(element.GetRawText(), targetType, JsonSO)
    End Function

    Private Shared Function GetDefaultJsonPatchValue(targetType As Type) As Object
        If targetType Is GetType(String) Then Return ""
        If Not targetType.IsValueType Then Return Nothing
        Return Activator.CreateInstance(targetType)
    End Function

    Private Shared Function ResolvePresetProperty(name As String, props As List(Of Reflection.PropertyInfo)) As Reflection.PropertyInfo
        If String.IsNullOrWhiteSpace(name) OrElse props Is Nothing Then Return Nothing
        Return props.FirstOrDefault(Function(x) String.Equals(x.Name, name, StringComparison.OrdinalIgnoreCase))
    End Function

    Private Shared Function GetPropertySuggestions(name As String, props As List(Of Reflection.PropertyInfo)) As List(Of String)
        If String.IsNullOrWhiteSpace(name) OrElse props Is Nothing Then Return New List(Of String)
        Return props.
            Select(Function(prop) New With {.Name = prop.Name, .Score = GetPropertyMatchScore(name, prop.Name)}).
            Where(Function(x) x.Score > 0).
            OrderByDescending(Function(x) x.Score).
            ThenBy(Function(x) x.Name, StringComparer.CurrentCultureIgnoreCase).
            Take(8).
            Select(Function(x) x.Name).
            ToList()
    End Function

    Private Shared Function GetPropertyMatchScore(query As String, propertyName As String) As Integer
        Dim q = If(query, "").Trim()
        Dim p = If(propertyName, "").Trim()
        If q = "" OrElse p = "" Then Return 0
        If String.Equals(q, p, StringComparison.OrdinalIgnoreCase) Then Return 10000
        If p.Contains(q, StringComparison.OrdinalIgnoreCase) Then Return 8000 + q.Length
        If q.Contains(p, StringComparison.OrdinalIgnoreCase) Then Return 7000 + p.Length

        Dim qParts = q.Split({"_"c, " "c, ControlChars.Tab}, StringSplitOptions.RemoveEmptyEntries)
        Dim score = 0
        For Each part In qParts
            If part.Length > 0 AndAlso p.Contains(part, StringComparison.OrdinalIgnoreCase) Then score += 200 + part.Length
        Next

        Dim common = CountCommonCharacters(q, p)
        If common >= Math.Min(3, Math.Min(q.Length, p.Length)) Then score += common
        Return score
    End Function

    Private Shared Function CountCommonCharacters(a As String, b As String) As Integer
        Dim setB = New HashSet(Of Char)(If(b, "").ToLowerInvariant())
        Dim count = 0
        For Each ch In If(a, "").ToLowerInvariant()
            If setB.Contains(ch) Then count += 1
        Next
        Return count
    End Function

    Private Shared Function JsonObjectHasProperty(json As String, propertyName As String) As Boolean
        If String.IsNullOrWhiteSpace(json) OrElse String.IsNullOrWhiteSpace(propertyName) Then Return False
        Try
            Using doc = JsonDocument.Parse(json)
                If doc.RootElement.ValueKind <> JsonValueKind.Object Then Return False
                Dim value As JsonElement
                Return doc.RootElement.TryGetProperty(propertyName, value)
            End Using
        Catch
            Return False
        End Try
    End Function

    Private Shared Function ClonePresetData(source As 预设数据_v6) As 预设数据_v6
        If source Is Nothing Then Return New 预设数据_v6
        Return JsonSerializer.Deserialize(Of 预设数据_v6)(JsonSerializer.Serialize(source, JsonSO), JsonSO)
    End Function

    Private Shared Function BuildParameterOverview(preset As 预设数据_v6) As String
        Using box As New LakeUI.ModernTextBox
            预设管理_v6.显示参数总览(box, preset)
            Return box.Text
        End Using
    End Function

    Private Shared Function BuildParameterCommandPreview(preset As 预设数据_v6) As String
        Return 预设管理_v6.生成命令行展示文本(preset, 预设管理_v6.输入占位符, 预设管理_v6.输出占位符)
    End Function

    Private Shared Function BuildChangedFieldList(beforePreset As 预设数据_v6, afterPreset As 预设数据_v6) As List(Of String)
        Dim result As New List(Of String)
        If beforePreset Is Nothing OrElse afterPreset Is Nothing Then Return result
        For Each prop In GetType(预设数据_v6).GetProperties().Where(Function(x) x.CanRead)
            Dim beforeValue = prop.GetValue(beforePreset)
            Dim afterValue = prop.GetValue(afterPreset)
            If Not JsonValuesEqual(beforeValue, afterValue) Then result.Add(prop.Name)
        Next
        Return result
    End Function

    Private Shared Function JsonValuesEqual(left As Object, right As Object) As Boolean
        Return String.Equals(JsonSerializer.Serialize(left, JsonSO), JsonSerializer.Serialize(right, JsonSO), StringComparison.Ordinal)
    End Function

    Private Class QueueTargetResolution
        Public Property RequestedAll As Boolean = False
        Public Property UsedDefaultAll As Boolean = False
        Public Property HasSpecificSelectors As Boolean = False
        Public Property Tasks As New List(Of 编码任务_v6)
        Public Property MissingIds As New List(Of String)
        Public Property MissingIndexes As New List(Of Integer)
        Public Property Errors As New List(Of String)
        Public Property IndexById As New Dictionary(Of String, Integer)(StringComparer.OrdinalIgnoreCase)
    End Class

    Private Shared Function GetQueueSummary(args As JsonElement) As String
        Dim snapshot = 编码队列_v6.获取队列快照()
        Dim resolution = ResolveQueueTarget(args, snapshot, True)
        Dim includeCommands = Agent通用工具_v6.GetJsonBoolean(args, "include_commands", False)
        Dim includePresetJson = Agent通用工具_v6.GetJsonBoolean(args, "include_preset_json", False)
        Dim includePerformance = Agent通用工具_v6.GetJsonBoolean(args, "include_performance", True)
        Dim detail = Agent通用工具_v6.GetJsonBoolean(args, "detail", resolution.HasSpecificSelectors OrElse includeCommands OrElse includePresetJson)
        Dim offset = Math.Max(Agent通用工具_v6.GetJsonInteger(args, "offset", 0), 0)
        Dim limit = Math.Max(Agent通用工具_v6.GetJsonInteger(args, "limit", 0), 0)
        Dim selectedCountBeforePaging = resolution.Tasks.Count
        If offset > 0 Then resolution.Tasks = resolution.Tasks.Skip(offset).ToList()
        If limit > 0 Then resolution.Tasks = resolution.Tasks.Take(Math.Min(limit, 5000)).ToList()

        Dim items = resolution.Tasks.
            Select(Function(t) BuildQueueTaskPayload(t, QueueIndexOf(snapshot, t.ID, resolution.IndexById), detail, includeCommands, includePresetJson, includePerformance)).
            ToList()

        If Not HasQueueQueryArguments(args) AndAlso resolution.Errors.Count = 0 Then
            Return JsonSerializer.Serialize(items, JsonSO)
        End If

        Dim payload As New Dictionary(Of String, Object) From {
            {"queue_count", snapshot.Count},
            {"returned_count", items.Count},
            {"offset", offset},
            {"limit", limit},
            {"has_more", offset + items.Count < selectedCountBeforePaging},
            {"target_all", resolution.RequestedAll},
            {"used_default_all", resolution.UsedDefaultAll},
            {"tasks", items},
            {"missing_ids", resolution.MissingIds},
            {"missing_indexes", resolution.MissingIndexes},
            {"errors", resolution.Errors}
        }
        Return JsonSerializer.Serialize(payload, JsonSO)
    End Function

    Private Shared Function GetQueueTaskLogs(args As JsonElement) As String
        Dim snapshot = 编码队列_v6.获取队列快照()
        Dim resolution = ResolveQueueTarget(args, snapshot, False)
        Dim logLimit = NormalizeLogLimit(Agent通用工具_v6.GetJsonInteger(args, "log_limit", 20))
        Dim modes = ResolveQueueLogModes(args)

        Dim items = resolution.Tasks.
            Select(Function(t) BuildQueueTaskLogsPayload(t, QueueIndexOf(snapshot, t.ID, resolution.IndexById), modes, logLimit)).
            ToList()

        Dim payload As New Dictionary(Of String, Object) From {
            {"queue_count", snapshot.Count},
            {"returned_count", items.Count},
            {"target_all", resolution.RequestedAll},
            {"modes", modes},
            {"log_limit", logLimit},
            {"tasks", items},
            {"missing_ids", resolution.MissingIds},
            {"missing_indexes", resolution.MissingIndexes},
            {"errors", resolution.Errors}
        }
        Return JsonSerializer.Serialize(payload, JsonSO)
    End Function

    Private Shared Function ControlQueueTasks(args As JsonElement) As String
        Dim action = NormalizeQueueAction(Agent通用工具_v6.GetJsonString(args, "action"))
        If action = "" Then
            Return JsonSerializer.Serialize(New Dictionary(Of String, Object) From {
                {"success", False},
                {"error", "未知或缺少 action。可用：start、pause、resume、stop、remove、reset"}
            }, JsonSO)
        End If

        Dim snapshotBefore = 编码队列_v6.获取队列快照()
        Dim resolution = ResolveQueueTarget(args, snapshotBefore, False)
        Dim detail = Agent通用工具_v6.GetJsonBoolean(args, "detail", False)
        If resolution.Tasks.Count = 0 AndAlso (resolution.MissingIds.Count > 0 OrElse resolution.MissingIndexes.Count > 0) Then
            resolution.Errors.Add("没有匹配到可控制的任务")
        End If
        Dim ids = resolution.Tasks.Select(Function(t) t.ID).ToList()
        Dim beforeItems = resolution.Tasks.
            Select(Function(t) BuildQueueTaskPayload(t, QueueIndexOf(snapshotBefore, t.ID, resolution.IndexById), detail, False, False, False)).
            ToList()
        Dim eligibleCount = resolution.Tasks.Where(Function(t) IsQueueActionAvailable(t, action)).Count()

        If resolution.Errors.Count = 0 AndAlso ids.Count > 0 Then
            ApplyQueueAction(action, ids, resolution.RequestedAll)
        End If

        Dim snapshotAfter = 编码队列_v6.获取队列快照()
        Dim afterIndex As New Dictionary(Of String, Integer)(StringComparer.OrdinalIgnoreCase)
        For i = 0 To snapshotAfter.Count - 1
            afterIndex(snapshotAfter(i).ID) = i + 1
        Next
        Dim afterItems As New List(Of Dictionary(Of String, Object))
        Dim afterById = snapshotAfter.ToDictionary(Function(t) t.ID, StringComparer.OrdinalIgnoreCase)
        For Each id In ids
            Dim task As 编码任务_v6 = Nothing
            If Not afterById.TryGetValue(id, task) Then
                afterItems.Add(New Dictionary(Of String, Object) From {
                    {"id", id},
                    {"removed", True}
                })
            Else
                afterItems.Add(BuildQueueTaskPayload(task, QueueIndexOf(snapshotAfter, id, afterIndex), detail, False, False, False))
            End If
        Next

        Dim payload As New Dictionary(Of String, Object) From {
            {"success", resolution.Errors.Count = 0},
            {"action", action},
            {"action_text", QueueActionDisplayName(action)},
            {"target_all", resolution.RequestedAll},
            {"requested_count", ids.Count},
            {"eligible_count", eligibleCount},
            {"queue_count_before", snapshotBefore.Count},
            {"queue_count_after", snapshotAfter.Count},
            {"matched_ids", ids},
            {"missing_ids", resolution.MissingIds},
            {"missing_indexes", resolution.MissingIndexes},
            {"errors", resolution.Errors},
            {"before", beforeItems},
            {"after", afterItems},
            {"message", BuildQueueActionMessage(action, ids.Count, eligibleCount, resolution.Errors)}
        }
        Return JsonSerializer.Serialize(payload, JsonSO)
    End Function

    Private Shared Function ResolveQueueTarget(args As JsonElement, snapshot As List(Of 编码任务_v6), defaultAll As Boolean) As QueueTargetResolution
        Dim result As New QueueTargetResolution
        If snapshot IsNot Nothing Then
            For i = 0 To snapshot.Count - 1
                If snapshot(i) IsNot Nothing Then result.IndexById(snapshot(i).ID) = i + 1
            Next
        End If
        Dim byId = snapshot.ToDictionary(Function(t) t.ID, StringComparer.OrdinalIgnoreCase)
        Dim target = Agent通用工具_v6.GetJsonString(args, "target").Trim().ToLowerInvariant()
        Dim requestedIds As New List(Of String)
        Dim requestedIndexes As New List(Of Integer)
        Dim seen As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)

        Dim singleId = Agent通用工具_v6.GetJsonString(args, "id").Trim()
        If singleId <> "" Then requestedIds.Add(singleId)
        requestedIds.AddRange(Agent通用工具_v6.GetJsonStringArray(args, "ids"))

        If HasJsonProperty(args, "index") Then requestedIndexes.Add(Agent通用工具_v6.GetJsonInteger(args, "index", 0))
        requestedIndexes.AddRange(GetJsonIntegerArray(args, "indexes"))

        result.HasSpecificSelectors = requestedIds.Count > 0 OrElse requestedIndexes.Count > 0

        Select Case target
            Case "", "specified", "指定"
            Case "all", "*", "全部", "所有"
                result.RequestedAll = True
            Case Else
                result.Errors.Add("未知 target：" & target)
        End Select

        Dim addTask =
            Sub(task As 编码任务_v6)
                If task Is Nothing OrElse seen.Contains(task.ID) Then Exit Sub
                seen.Add(task.ID)
                result.Tasks.Add(task)
            End Sub

        If result.RequestedAll Then
            For Each task In snapshot
                addTask(task)
            Next
            Return result
        End If

        For Each id In requestedIds
            Dim task As 编码任务_v6 = Nothing
            If Not byId.TryGetValue(id, task) Then
                If Not result.MissingIds.Contains(id, StringComparer.OrdinalIgnoreCase) Then result.MissingIds.Add(id)
            Else
                addTask(task)
            End If
        Next

        For Each index In requestedIndexes
            If index <= 0 OrElse index > snapshot.Count Then
                If Not result.MissingIndexes.Contains(index) Then result.MissingIndexes.Add(index)
            Else
                addTask(snapshot(index - 1))
            End If
        Next

        If result.Tasks.Count = 0 AndAlso requestedIds.Count = 0 AndAlso requestedIndexes.Count = 0 Then
            If defaultAll Then
                result.RequestedAll = True
                result.UsedDefaultAll = True
                For Each task In snapshot
                    addTask(task)
                Next
            Else
                result.Errors.Add("缺少目标：请传 id/ids/index/indexes 或 target=all")
            End If
        End If

        Return result
    End Function

    Private Shared Function BuildQueueTaskPayload(task As 编码任务_v6,
                                                  index As Integer,
                                                  detail As Boolean,
                                                  includeCommands As Boolean,
                                                  includePresetJson As Boolean,
                                                  includePerformance As Boolean) As Dictionary(Of String, Object)
        Dim item = BuildQueueTaskSummary(task, index)
        If task Is Nothing Then Return item
        If includePerformance Then item("performance") = 任务性能统计_v6.获取快照(task)
        If Not detail AndAlso Not includeCommands AndAlso Not includePresetJson Then Return item

        item("status_code") = CInt(task.状态)
        item("task_type") = If(task.预设数据 Is Nothing, "command_line", "preset")
        item("command_line_task") = task.预设数据 Is Nothing
        item("allow_auto_start") = task.允许自动启动
        item("can_start") = task.状态 = 编码任务状态_v6.未处理
        item("can_pause") = task.状态 = 编码任务状态_v6.正在处理
        item("can_resume") = task.状态 = 编码任务状态_v6.已暂停
        item("can_stop") = task.可停止 OrElse (task.状态 = 编码任务状态_v6.未处理 AndAlso task.允许自动启动)
        item("can_remove") = task.可移除
        item("can_reset") = task.可重置
        item("can_sort") = task.可排序
        item("elapsed_seconds") = Math.Round(task.任务耗时计时器.Elapsed.TotalSeconds, 3)
        item("elapsed_text") = 编码进度_v6.格式化秒(task.任务耗时计时器.Elapsed.TotalSeconds)
        item("input_size_bytes") = task.输入文件大小
        item("media_duration_seconds") = task.媒体总时长
        item("current_step_index") = task.当前步骤索引 + 1
        item("step_count") = task.步骤.Count
        item("current_step") = If(task.当前步骤 Is Nothing, Nothing, BuildQueueStepPayload(task.当前步骤, task.当前步骤索引 + 1, False))
        item("steps") = task.步骤.Select(Function(s, i) BuildQueueStepPayload(s, i + 1, includeCommands)).ToList()
        item("progress_detail") = BuildQueueProgressPayload(task.进度)
        item("process") = New Dictionary(Of String, Object) From {
            {"id", task.当前进程ID},
            {"name", task.当前进程名称}
        }
        item("latest_log") = New Dictionary(Of String, Object) From {
            {"text", task.最新底部日志文本},
            {"is_error", task.最新底部日志是否错误},
            {"raw_text", task.实时输出}
        }
        item("error_count") = If(task.错误列表 Is Nothing, 0, task.错误列表.Count)
        item("non_progress_output_count") = If(task.非进度输出列表 Is Nothing, 0, task.非进度输出列表.Count)
        item("log_version") = task.日志版本号

        If includeCommands Then
            item("commands") = BuildQueueCommandPayload(task)
            item("command_preview_text") = 编码队列_v6.获取任务实际命令行文本(task)
            item("actual_command_text") = 编码队列_v6.获取任务执行命令行文本(task)
        End If
        If includePresetJson Then item("preset_json") = If(task.预设数据 Is Nothing, "", JsonSerializer.Serialize(task.预设数据, JsonSO))
        Return item
    End Function

    Private Shared Function BuildQueueTaskSummary(task As 编码任务_v6, index As Integer) As Dictionary(Of String, Object)
        If task Is Nothing Then Return New Dictionary(Of String, Object)
        Return New Dictionary(Of String, Object) From {
            {"index", index},
            {"id", task.ID},
            {"name", task.任务名称},
            {"input", task.输入文件},
            {"output", task.输出文件},
            {"status", task.状态.ToString()},
            {"progress", If(task.进度 Is Nothing, "", task.进度.进度文本)}
        }
    End Function

    Private Shared Function BuildQueueProgressPayload(progress As 编码进度_v6) As Dictionary(Of String, Object)
        If progress Is Nothing Then Return New Dictionary(Of String, Object)
        Return New Dictionary(Of String, Object) From {
            {"stage", progress.当前阶段},
            {"total_seconds", Math.Round(progress.总时长.TotalSeconds, 3)},
            {"current_seconds", Math.Round(progress.当前时间.TotalSeconds, 3)},
            {"percent", Math.Round(progress.百分比, 4)},
            {"text", progress.进度文本},
            {"speed", progress.效率文本},
            {"output_size_text", progress.输出大小文本},
            {"output_size_kb", progress.输出大小KB},
            {"quality", progress.质量文本},
            {"bitrate", progress.比特率文本},
            {"eta_text", progress.时间文本}
        }
    End Function

    Private Shared Function BuildQueueStepPayload(stepItem As 编码步骤_v6, index As Integer, includeCommand As Boolean) As Dictionary(Of String, Object)
        If stepItem Is Nothing Then Return New Dictionary(Of String, Object)
        Dim item As New Dictionary(Of String, Object) From {
            {"index", index},
            {"name", stepItem.显示名称},
            {"stage", stepItem.阶段.ToString()},
            {"status", stepItem.状态.ToString()},
            {"status_code", CInt(stepItem.状态)},
            {"description", stepItem.说明},
            {"requires_media_duration", stepItem.需要媒体总时长},
            {"output_cache_count", If(stepItem.输出缓存 Is Nothing, 0, stepItem.输出缓存.Count)}
        }
        If stepItem.输出缓存 IsNot Nothing AndAlso stepItem.输出缓存.Count > 0 Then item("latest_output") = stepItem.输出缓存(stepItem.输出缓存.Count - 1)
        If includeCommand Then
            item("process") = 预设管理_v6.获取命令行进程名(stepItem.阶段)
            item("arguments") = stepItem.命令行
            item("command_line") = 预设管理_v6.获取命令行进程名(stepItem.阶段) & " " & stepItem.命令行
            If Not String.IsNullOrWhiteSpace(stepItem.实际执行文件名) Then
                item("actual_process") = stepItem.实际执行文件名
                item("actual_arguments") = stepItem.实际执行参数
                item("actual_command_line") = 格式化Agent进程文件名(stepItem.实际执行文件名) & If(String.IsNullOrWhiteSpace(stepItem.实际执行参数), "", " " & stepItem.实际执行参数)
            End If
        End If
        Return item
    End Function

    Private Shared Function BuildQueueTaskLogsPayload(task As 编码任务_v6,
                                                      index As Integer,
                                                      modes As List(Of String),
                                                      logLimit As Integer) As Dictionary(Of String, Object)
        Dim item = BuildQueueTaskSummary(task, index)
        If task Is Nothing Then Return item

        item("current_step") = If(task.当前步骤 Is Nothing, "", task.当前步骤.显示名称)
        item("log_version") = task.日志版本号
        item("log_structure_version") = task.日志结构版本号

        Dim logsByMode As New Dictionary(Of String, Object)(StringComparer.OrdinalIgnoreCase)
        For Each mode In modes
            logsByMode(mode) = BuildQueueLogPayload(task, logLimit, mode)
        Next
        item("logs") = logsByMode
        Return item
    End Function

    Private Shared Function BuildQueueLogPayload(task As 编码任务_v6, logLimit As Integer, logMode As String) As List(Of Dictionary(Of String, Object))
        Dim logs = task.获取日志快照(ParseQueueLogMode(logMode))
        If logLimit > 0 AndAlso logs.Count > logLimit Then logs = logs.Skip(logs.Count - logLimit).ToList()
        Return logs.Select(Function(entry) New Dictionary(Of String, Object) From {
            {"sequence", entry.序号},
            {"time", entry.时间.ToString("yyyy-MM-dd HH:mm:ss")},
            {"stage", entry.阶段名},
            {"category", entry.类别.ToString()},
            {"is_error", entry.是否错误},
            {"text", entry.文本}
        }).ToList()
    End Function

    Private Shared Function BuildQueueCommandPayload(task As 编码任务_v6) As List(Of Dictionary(Of String, Object))
        Dim result As New List(Of Dictionary(Of String, Object))
        Try
            If task.步骤 IsNot Nothing AndAlso task.步骤.Count > 0 Then
                For i = 0 To task.步骤.Count - 1
                    Dim stepItem = task.步骤(i)
                    result.Add(BuildQueueCommandItem(i + 1, stepItem.阶段, stepItem.显示名称, stepItem.命令行, stepItem.实际执行文件名, stepItem.实际执行参数))
                Next
            ElseIf task.预设数据 IsNot Nothing Then
                Dim output = If(task.输出文件 <> "", task.输出文件, 编码队列_v6.计算输出位置_v6(task.输入文件, task.预设数据))
                Dim generated = 预设管理_v6.生成阶段化命令行(task.预设数据, task.输入文件, output, 帧服务器脚本后缀:=task.ID)
                For i = 0 To generated.Count - 1
                    Dim command = generated(i)
                    result.Add(BuildQueueCommandItem(i + 1, command.阶段, command.阶段.ToString(), command.命令行))
                Next
            ElseIf task.命令行 <> "" Then
                result.Add(BuildQueueCommandItem(1, 预设数据_v6.命令行阶段.普通单次, "命令行", task.命令行))
            End If
        Catch ex As Exception
            result.Add(New Dictionary(Of String, Object) From {{"error", ex.Message}})
        End Try
        Return result
    End Function

    Private Shared Function BuildQueueCommandItem(index As Integer,
                                                  stage As 预设数据_v6.命令行阶段,
                                                  displayName As String,
                                                  arguments As String,
                                                  Optional actualProcess As String = "",
                                                  Optional actualArguments As String = "") As Dictionary(Of String, Object)
        Dim processName = 预设管理_v6.获取命令行进程名(stage)
        Dim item As New Dictionary(Of String, Object) From {
            {"index", index},
            {"stage", stage.ToString()},
            {"name", displayName},
            {"process", processName},
            {"arguments", arguments},
            {"command_line", processName & " " & arguments}
        }
        If Not String.IsNullOrWhiteSpace(actualProcess) Then
            item("actual_process") = actualProcess
            item("actual_arguments") = actualArguments
            item("actual_command_line") = 格式化Agent进程文件名(actualProcess) & If(String.IsNullOrWhiteSpace(actualArguments), "", " " & actualArguments)
        End If
        Return item
    End Function

    Private Shared Function 格式化Agent进程文件名(value As String) As String
        Dim processName = If(value, "").Trim()
        If processName = "" Then processName = "ffmpeg"
        If processName.Any(Function(c) Char.IsWhiteSpace(c)) AndAlso Not (processName.StartsWith("""c", StringComparison.Ordinal) AndAlso processName.EndsWith("""c", StringComparison.Ordinal)) Then
            Return """" & processName & """"
        End If
        Return processName
    End Function

    Private Shared Sub ApplyQueueAction(action As String, ids As List(Of String), requestedAll As Boolean)
        Select Case action
            Case "start"
                编码队列_v6.开始任务(ids)
            Case "pause"
                编码队列_v6.取消自动开始任务(ids)
                编码队列_v6.暂停任务(ids)
            Case "resume"
                编码队列_v6.恢复任务(ids)
            Case "stop"
                If requestedAll Then
                    编码队列_v6.停止所有进行中任务()
                Else
                    编码队列_v6.取消自动开始任务(ids)
                    编码队列_v6.停止任务(ids)
                End If
            Case "remove"
                编码队列_v6.移除任务(ids)
            Case "reset"
                编码队列_v6.重置任务(ids)
        End Select
    End Sub

    Private Shared Function IsQueueActionAvailable(task As 编码任务_v6, action As String) As Boolean
        If task Is Nothing Then Return False
        Select Case action
            Case "start"
                Return task.状态 = 编码任务状态_v6.未处理
            Case "pause"
                Return task.状态 = 编码任务状态_v6.正在处理 OrElse (task.状态 = 编码任务状态_v6.未处理 AndAlso task.允许自动启动)
            Case "resume"
                Return task.状态 = 编码任务状态_v6.已暂停
            Case "stop"
                Return task.可停止 OrElse (task.状态 = 编码任务状态_v6.未处理 AndAlso task.允许自动启动)
            Case "remove"
                Return task.可移除
            Case "reset"
                Return task.可重置
            Case Else
                Return False
        End Select
    End Function

    Private Shared Function NormalizeQueueAction(value As String) As String
        Select Case If(value, "").Trim().ToLowerInvariant()
            Case "start", "run", "begin", "开始", "启动"
                Return "start"
            Case "pause", "暂停"
                Return "pause"
            Case "resume", "continue", "恢复", "继续"
                Return "resume"
            Case "stop", "cancel", "terminate", "停止", "终止", "取消"
                Return "stop"
            Case "remove", "delete", "移除", "删除"
                Return "remove"
            Case "reset", "restartable", "重置"
                Return "reset"
            Case Else
                Return ""
        End Select
    End Function

    Private Shared Function QueueActionDisplayName(action As String) As String
        Select Case action
            Case "start" : Return "开始"
            Case "pause" : Return "暂停"
            Case "resume" : Return "恢复"
            Case "stop" : Return "停止"
            Case "remove" : Return "移除"
            Case "reset" : Return "重置"
            Case Else : Return action
        End Select
    End Function

    Private Shared Function BuildQueueActionMessage(action As String, requestedCount As Integer, eligibleCount As Integer, errors As List(Of String)) As String
        If errors IsNot Nothing AndAlso errors.Count > 0 Then Return "队列控制未执行：" & String.Join("；", errors)
        Return $"已请求{QueueActionDisplayName(action)} {requestedCount} 个任务，其中当前可执行 {eligibleCount} 个。"
    End Function

    Private Shared Function ParseQueueLogMode(value As String) As 编码任务日志显示模式_v6
        Select Case NormalizeQueueLogModeName(value)
            Case "latest_non_progress", "non_progress", "latest", "latest_output", "最新非进度"
                Return 编码任务日志显示模式_v6.最新输出不含进度
            Case "errors", "error", "错误"
                Return 编码任务日志显示模式_v6.仅错误信息
            Case "current_stage", "stage", "当前阶段"
                Return 编码任务日志显示模式_v6.当前阶段输出
            Case Else
                Return 编码任务日志显示模式_v6.全部输出
        End Select
    End Function

    Private Shared Function ResolveQueueLogModes(args As JsonElement) As List(Of String)
        Dim requested As New List(Of String)
        Dim singleMode = Agent通用工具_v6.GetJsonString(args, "mode").Trim()
        If singleMode <> "" Then requested.Add(singleMode)
        requested.AddRange(Agent通用工具_v6.GetJsonStringArray(args, "modes", False))

        If requested.Count = 0 OrElse requested.Any(Function(x) IsAllQueueLogModesToken(x)) Then
            Return DefaultQueueLogModes()
        End If

        Dim result As New List(Of String)
        For Each item In requested
            Dim normalized = NormalizeQueueLogModeName(item)
            If normalized = "" Then Continue For
            If Not result.Contains(normalized, StringComparer.OrdinalIgnoreCase) Then result.Add(normalized)
        Next
        If result.Count = 0 Then Return DefaultQueueLogModes()
        Return result
    End Function

    Private Shared Function DefaultQueueLogModes() As List(Of String)
        Return New List(Of String) From {"all", "latest_non_progress", "errors", "current_stage"}
    End Function

    Private Shared Function IsAllQueueLogModesToken(value As String) As Boolean
        Select Case If(value, "").Trim().ToLowerInvariant()
            Case "all_modes", "all_modes_default", "four", "四档", "全部档位"
                Return True
            Case Else
                Return False
        End Select
    End Function

    Private Shared Function NormalizeQueueLogModeName(value As String) As String
        Select Case If(value, "").Trim().ToLowerInvariant()
            Case "all", "full", "全部", "全部输出"
                Return "all"
            Case "latest_non_progress", "non_progress", "latest", "latest_output", "最新非进度", "最新输出不含进度"
                Return "latest_non_progress"
            Case "errors", "error", "err", "错误", "仅错误信息"
                Return "errors"
            Case "current_stage", "stage", "current", "当前阶段", "当前阶段输出"
                Return "current_stage"
            Case Else
                Return ""
        End Select
    End Function

    Private Shared Function NormalizeLogLimit(value As Integer) As Integer
        If value <= 0 Then Return 20
        Return Math.Min(value, 200)
    End Function

    Private Shared Function QueueIndexOf(snapshot As List(Of 编码任务_v6), id As String, Optional indexById As Dictionary(Of String, Integer) = Nothing) As Integer
        If snapshot Is Nothing OrElse String.IsNullOrWhiteSpace(id) Then Return 0
        If indexById IsNot Nothing Then
            Dim index As Integer
            If indexById.TryGetValue(id, index) Then Return index
            Return 0
        End If
        For i = 0 To snapshot.Count - 1
            If String.Equals(snapshot(i).ID, id, StringComparison.OrdinalIgnoreCase) Then Return i + 1
        Next
        Return 0
    End Function

    Private Shared Function HasQueueQueryArguments(args As JsonElement) As Boolean
        Return HasJsonProperty(args, "id", "ids", "index", "indexes", "target", "detail", "include_commands", "include_preset_json", "offset", "limit")
    End Function

    Private Shared Function HasJsonProperty(root As JsonElement, ParamArray names As String()) As Boolean
        If root.ValueKind <> JsonValueKind.Object Then Return False
        Dim value As JsonElement
        For Each name In names
            If root.TryGetProperty(name, value) Then Return True
        Next
        Return False
    End Function

    Private Shared Function GetJsonIntegerArray(root As JsonElement, name As String) As List(Of Integer)
        Dim result As New List(Of Integer)
        Dim value As JsonElement
        If root.ValueKind <> JsonValueKind.Object OrElse Not root.TryGetProperty(name, value) OrElse value.ValueKind <> JsonValueKind.Array Then Return result

        For Each item In value.EnumerateArray()
            Dim n As Integer
            If item.ValueKind = JsonValueKind.Number AndAlso item.TryGetInt32(n) Then
                If Not result.Contains(n) Then result.Add(n)
            ElseIf item.ValueKind = JsonValueKind.String AndAlso Integer.TryParse(item.GetString(), n) Then
                If Not result.Contains(n) Then result.Add(n)
            End If
        Next
        Return result
    End Function

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
        Using http As New HttpClient With {.Timeout = TimeSpan.FromSeconds(45)}
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

                Using response = Await http.SendAsync(request, cancellationToken)
                    Dim responseText = Await response.Content.ReadAsStringAsync(cancellationToken)
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
        Dim options = If(recursive, SearchOption.AllDirectories, SearchOption.TopDirectoryOnly)
        Dim items = dir.EnumerateFileSystemInfos("*", options).
            OrderByDescending(Function(x) TypeOf x Is DirectoryInfo).
            ThenBy(Function(x) x.Name, StringComparer.CurrentCultureIgnoreCase).
            Take(safeLimit).
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
        Dim fullDestination = System.IO.Path.GetFullPath(destination)
        If (File.Exists(fullDestination) OrElse Directory.Exists(fullDestination)) AndAlso Not overwrite Then Return "目标路径已存在，请传 overwrite=true"
        If Directory.Exists(source) Then
            If overwrite AndAlso Directory.Exists(fullDestination) Then Directory.Delete(fullDestination, True)
            Directory.Move(source, fullDestination)
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
