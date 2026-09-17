Imports System.Drawing.Imaging
Imports System.IO
Imports System.Net
Imports System.Net.Http
Imports System.Text
Imports System.Text.Json
Imports System.Text.RegularExpressions

Partial Public Class AgentLocalTools
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
                        Await _process.StandardInput.WriteLineAsync(BuildInputPayload(inputText, requestedWorkingDirectory, stdoutEndPrefix, stderrEndMarker).AsMemory(), linkedCts.Token)
                        Await _process.StandardInput.FlushAsync(linkedCts.Token)

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

                If operationFailure IsNot Nothing Then KillProcess()
                If processEnded Then Await WaitForCurrentProcessExitAsync()
                If waitAfterKill Then
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
            Dim result As New Dictionary(Of String, Object) From {
                {InputPropertyNames(0), inputText},
                {"working_directory", workingDirectory},
                {"timeout_seconds", timeoutSeconds},
                {"timed_out", timedOut},
                {"exit_code", exitCode},
                {"elapsed_ms", elapsedMilliseconds}
            }
            If stdout <> "" Then result("stdout") = stdout
            If stderr <> "" Then result("stderr") = stderr
            Return result
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

        Private Shared Sub AppendCapturedLine(builder As StringBuilder, line As String)
            Dim remaining = MaxCapturedOutputCharacters + 1 - builder.Length
            If remaining > 0 Then builder.Append((line & Environment.NewLine).AsSpan(0, Math.Min(remaining, line.Length + Environment.NewLine.Length)))
        End Sub

        Private Sub OnOutputDataReceived(sender As Object, e As DataReceivedEventArgs)
            If e.Data Is Nothing Then Return

            SyncLock _outputLock
                If _stdoutBuilder Is Nothing Then Return

                Dim line = e.Data
                Dim index = If(_stdoutEndPrefix = "", -1, line.IndexOf(_stdoutEndPrefix, StringComparison.Ordinal))
                If index >= 0 Then
                    If index > 0 Then AppendCapturedLine(_stdoutBuilder, line.Substring(0, index))

                    Dim markerPayload = line.Substring(index + _stdoutEndPrefix.Length).Trim()
                    Dim separator = markerPayload.IndexOf(":"c)
                    Dim exitText = If(separator >= 0, markerPayload.Substring(0, separator), markerPayload)
                    Dim parsedExitCode As Integer
                    If Integer.TryParse(exitText, parsedExitCode) Then _reportedExitCode = parsedExitCode
                    If separator >= 0 Then _reportedWorkingDirectory = FromBase64Utf8(markerPayload.Substring(separator + 1))

                    _stdoutEndTcs?.TrySetResult(True)
                    Return
                End If

                AppendCapturedLine(_stdoutBuilder, line)
            End SyncLock
        End Sub

        Private Sub OnErrorDataReceived(sender As Object, e As DataReceivedEventArgs)
            If e.Data Is Nothing Then Return

            SyncLock _outputLock
                If _stderrBuilder Is Nothing Then Return

                Dim line = e.Data
                Dim index = If(_stderrEndMarker = "", -1, line.IndexOf(_stderrEndMarker, StringComparison.Ordinal))
                If index >= 0 Then
                    If index > 0 Then AppendCapturedLine(_stderrBuilder, line.Substring(0, index))
                    _stderrEndTcs?.TrySetResult(True)
                    Return
                End If

                AppendCapturedLine(_stderrBuilder, line)
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
                Await _process.WaitForExitAsync(Threading.CancellationToken.None).WaitAsync(TimeSpan.FromSeconds(5))
            Catch
            End Try
        End Function

        Private Sub CleanupProcess()
            If _process Is Nothing Then Return

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
            ' Prefer the cross-platform PowerShell (7+) executable when it is
            ' installed and available on PATH.  Keep Windows PowerShell 5 as
            ' a fallback so existing installations continue to work.
            For Each executable In New String() {"pwsh.exe", "powershell.exe"}
                Dim process = CreateRedirectedProcess(executable, workingDirectory)
                process.StartInfo.ArgumentList.Add("-NoLogo")
                process.StartInfo.ArgumentList.Add("-NoProfile")
                process.StartInfo.ArgumentList.Add("-NonInteractive")
                process.StartInfo.ArgumentList.Add("-ExecutionPolicy")
                process.StartInfo.ArgumentList.Add("Bypass")
                process.StartInfo.ArgumentList.Add("-Command")
                process.StartInfo.ArgumentList.Add("-")
                process.StartInfo.EnvironmentVariables("POWERSHELL_TELEMETRY_OPTOUT") = "1"
                Yield process
            Next
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
        Dim exitCode = -1
        Dim stdout = ""
        Dim stderr = ""
        Try
            cancellationToken.ThrowIfCancellationRequested()
            If Not process.Start() Then Return "无法启动可执行文件：" & executable
            Using timeout = Threading.CancellationTokenSource.CreateLinkedTokenSource(cancellationToken)
                timeout.CancelAfter(TimeSpan.FromSeconds(timeoutSeconds))
                Dim stdoutTask = ReadBoundedOutputAsync(process.StandardOutput, maxOutputCharacters, timeout.Token)
                Dim stderrTask = ReadBoundedOutputAsync(process.StandardError, maxOutputCharacters, timeout.Token)
                Dim inputTask = WriteProcessInputAsync(process, Agent通用工具_v6.GetJsonString(args, "stdin"), timeout.Token)
                Dim work = Task.WhenAll(stdoutTask, stderrTask, inputTask, process.WaitForExitAsync(timeout.Token))
                Try
                    ' 对整个任务设置超时，不能先等待读到 EOF 再处理进程取消。
                    Await work.WaitAsync(timeout.Token)
                Catch ex As OperationCanceledException When timeout.IsCancellationRequested
                    timedOut = Not cancellationToken.IsCancellationRequested
                    Try
                        If Not process.HasExited Then process.Kill(True)
                    Catch
                    End Try
                End Try
                If timeout.IsCancellationRequested Then
                    Try
                        Await work.WaitAsync(TimeSpan.FromSeconds(5))
                    Catch
                    End Try
                    cancellationToken.ThrowIfCancellationRequested()
                End If
                If process.HasExited Then exitCode = process.ExitCode
                If stdoutTask.IsCompletedSuccessfully Then stdout = stdoutTask.Result
                If stderrTask.IsCompletedSuccessfully Then stderr = stderrTask.Result
            End Using
        Catch ex As OperationCanceledException When cancellationToken.IsCancellationRequested
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

        Dim result As New Dictionary(Of String, Object) From {
            {"success", Not timedOut AndAlso exitCode = 0},
            {"executable", executable},
            {"working_directory", workingDirectory},
            {"arguments", arguments},
            {"exit_code", exitCode},
            {"timed_out", timedOut},
            {"elapsed_ms", CLng(stopwatch.Elapsed.TotalMilliseconds)}
        }
        If stdout <> "" Then result("stdout") = stdout
        If stderr <> "" Then result("stderr") = stderr
        Return JsonSerializer.Serialize(result, JsonSO)
    End Function

    Private Shared Async Function WriteProcessInputAsync(process As Process, text As String,
                                                          cancellationToken As Threading.CancellationToken) As Task
        If text <> "" Then Await process.StandardInput.WriteAsync(text.AsMemory(), cancellationToken)
        Await process.StandardInput.FlushAsync(cancellationToken)
        process.StandardInput.Close()
    End Function

    Private Shared Async Function ReadBoundedOutputAsync(reader As StreamReader, limit As Integer,
                                                         cancellationToken As Threading.CancellationToken) As Task(Of String)
        Dim captured As New StringBuilder
        Dim buffer(4095) As Char
        Dim truncated As Boolean
        Try
            Do
                Dim count = Await reader.ReadAsync(buffer.AsMemory(), cancellationToken).ConfigureAwait(False)
                If count = 0 Then Exit Do
                Dim take = Math.Min(count, Math.Max(0, limit - captured.Length))
                captured.Append(buffer, 0, take)
                truncated = truncated OrElse take < count
            Loop
        Catch ex As OperationCanceledException When cancellationToken.IsCancellationRequested
            ' 仍返回超时前已读取的输出；进程生命周期由调用者负责。
        End Try
        Return captured.ToString() & If(truncated, "...[truncated]", "")
    End Function
End Class
