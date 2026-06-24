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

    Public NotInheritable Class PowerShellRunSession
        Implements IDisposable

        Private Const StdoutEndPrefixBase As String = "__3FUI_AGENT_PS_STDOUT_END__"
        Private Const StderrEndPrefixBase As String = "__3FUI_AGENT_PS_STDERR_END__"

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
        Private _disposed As Boolean = False

        Public Async Function ExecuteAsync(args As JsonElement,
                                           cancellationToken As Threading.CancellationToken) As Task(Of String)
            Dim command = Agent通用工具_v6.GetJsonString(args, "command").Trim()
            If command = "" Then Return "缺少 command。"

            Dim requestedWorkingDirectory = Agent通用工具_v6.GetJsonString(args, "working_directory").Trim()
            If requestedWorkingDirectory <> "" AndAlso Not Directory.Exists(requestedWorkingDirectory) Then Return "目录不存在：" & requestedWorkingDirectory

            Dim timeoutSeconds = Agent通用工具_v6.GetJsonInteger(args, "timeout_seconds", 60)
            timeoutSeconds = Math.Min(Math.Max(timeoutSeconds, 1), 300)

            Await _gate.WaitAsync(cancellationToken)
            Try
                If _disposed Then Return "PowerShell 会话已关闭。"

                Dim initialDirectory = If(requestedWorkingDirectory <> "", requestedWorkingDirectory, Application.StartupPath)
                EnsureProcessStarted(initialDirectory)

                Dim token = Guid.NewGuid().ToString("N")
                Dim stdoutEndPrefix = StdoutEndPrefixBase & token & ":"
                Dim stderrEndMarker = StderrEndPrefixBase & token
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

                Using linkedCts = Threading.CancellationTokenSource.CreateLinkedTokenSource(cancellationToken)
                    linkedCts.CancelAfter(TimeSpan.FromSeconds(timeoutSeconds))
                    Try
                        _process.StandardInput.WriteLine(BuildPersistentPowerShellScript(command, requestedWorkingDirectory, stdoutEndPrefix, stderrEndMarker))
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

                stdout = Agent通用工具_v6.LimitText(stdout, 12000)
                stderr = Agent通用工具_v6.LimitText(stderr, 12000)

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
            Finally
                _gate.Release()
            End Try
        End Function

        Private Sub EnsureProcessStarted(initialWorkingDirectory As String)
            If _process IsNot Nothing AndAlso Not _process.HasExited Then Return

            CleanupProcess()

            Dim workingDirectory = If(initialWorkingDirectory, "").Trim()
            If workingDirectory = "" OrElse Not Directory.Exists(workingDirectory) Then workingDirectory = Application.StartupPath

            Dim process As New Process()
            process.StartInfo = New ProcessStartInfo With {
                .FileName = "powershell.exe",
                .WorkingDirectory = workingDirectory,
                .UseShellExecute = False,
                .RedirectStandardInput = True,
                .RedirectStandardOutput = True,
                .RedirectStandardError = True,
                .CreateNoWindow = True,
                .StandardOutputEncoding = Encoding.UTF8,
                .StandardErrorEncoding = Encoding.UTF8
            }
            process.StartInfo.ArgumentList.Add("-NoLogo")
            process.StartInfo.ArgumentList.Add("-NoProfile")
            process.StartInfo.ArgumentList.Add("-NonInteractive")
            process.StartInfo.ArgumentList.Add("-ExecutionPolicy")
            process.StartInfo.ArgumentList.Add("Bypass")
            process.StartInfo.ArgumentList.Add("-Command")
            process.StartInfo.ArgumentList.Add("-")
            process.StartInfo.EnvironmentVariables("POWERSHELL_TELEMETRY_OPTOUT") = "1"
            process.EnableRaisingEvents = True
            _processExitedTcs = New TaskCompletionSource(Of Boolean)(TaskCreationOptions.RunContinuationsAsynchronously)
            AddHandler process.OutputDataReceived, AddressOf OnOutputDataReceived
            AddHandler process.ErrorDataReceived, AddressOf OnErrorDataReceived
            AddHandler process.Exited, AddressOf OnProcessExited

            If Not process.Start() Then
                process.Dispose()
                Throw New InvalidOperationException("PowerShell 启动失败。")
            End If

            _process = process
            _lastWorkingDirectory = workingDirectory
            process.BeginOutputReadLine()
            process.BeginErrorReadLine()
            process.StandardInput.WriteLine("[Console]::OutputEncoding=[System.Text.Encoding]::UTF8; $OutputEncoding=[System.Text.Encoding]::UTF8; $ProgressPreference='SilentlyContinue'")
            process.StandardInput.Flush()
        End Sub

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

        Private Shared Function ToBase64Utf8(text As String) As String
            Return Convert.ToBase64String(Encoding.UTF8.GetBytes(If(text, "")))
        End Function

        Private Shared Function FromBase64Utf8(text As String) As String
            If String.IsNullOrWhiteSpace(text) Then Return ""
            Try
                Return Encoding.UTF8.GetString(Convert.FromBase64String(text))
            Catch
                Return ""
            End Try
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
            tools.Add(FunctionTool("get_parameter_panel_controls", "读取参数面板上的下拉框可选值，以及 Label/HtmlColorLabel 文本。", New Dictionary(Of String, Object) From {
                {"query", New Dictionary(Of String, Object) From {{"type", "string"}, {"description", "可选控件名或文本关键词"}}}
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
            tools.Add(FunctionTool("save_parameter_preset", "保存参数预设到用户自定义或从社区下载来源。删除权限禁用；开发者内置只允许读取。", New Dictionary(Of String, Object) From {
                {"source", New Dictionary(Of String, Object) From {{"type", "string"}}},
                {"name", New Dictionary(Of String, Object) From {{"type", "string"}}},
                {"preset_json", New Dictionary(Of String, Object) From {{"type", "string"}}},
                {"note", New Dictionary(Of String, Object) From {{"type", "string"}}}
            }, {"source", "name"}))
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
            tools.Add(FunctionTool("run_powershell", "运行 PowerShell 命令。仅系统访问权限可用。同一次用户消息触发的 Agent 运行会复用同一个 PowerShell 进程，变量、当前位置和模块导入可在本轮多次调用之间保留；本轮响应结束、超时或任务终止时会关闭进程。", New Dictionary(Of String, Object) From {
                {"command", New Dictionary(Of String, Object) From {{"type", "string"}, {"description", "要执行的 PowerShell 命令"}}},
                {"working_directory", New Dictionary(Of String, Object) From {{"type", "string"}, {"description", "可选工作目录。首次调用默认使用程序目录；后续调用默认沿用当前 PowerShell 位置。"}}},
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
                                              Optional powerShellSession As PowerShellRunSession = Nothing,
                                              Optional cancellationToken As Threading.CancellationToken = Nothing) As Task(Of String)
        Dim args = Agent通用工具_v6.ParseJsonArguments(callInfo?.Arguments)
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
                    Return Agent工具封装_v6.保存参数预设(Agent通用工具_v6.GetJsonString(args, "source"), Agent通用工具_v6.GetJsonString(args, "name"), Agent通用工具_v6.GetJsonString(args, "preset_json"), Agent通用工具_v6.GetJsonString(args, "note"))
                Case "web_search"
                    If Not AgentNetworkMode.IsEnabled(networkMode) Then Return "联网已禁用"
                    Return Await WebSearchAsync(Agent通用工具_v6.GetJsonString(args, "query"), networkMode, endpointClient, modelId, reasoningEffort, cancellationToken)
                Case "fetch_url"
                    If Not AgentNetworkMode.IsEnabled(networkMode) Then Return "联网已禁用"
                    If AgentNetworkMode.Normalize(networkMode) <> AgentNetworkMode.Local Then Return "当前联网模式不允许本地网页请求"
                    Return Await FetchUrlAsync(Agent通用工具_v6.GetJsonString(args, "url"), cancellationToken)
                Case "read_local_text_file"
                    If permissionLevel < PermissionSystem Then Return "权限不足：需要系统访问"
                    Return ReadLocalTextFile(Agent通用工具_v6.GetJsonString(args, "path"))
                Case "list_directory"
                    If permissionLevel < PermissionSystem Then Return "权限不足：需要系统访问"
                    Return ListDirectory(Agent通用工具_v6.GetJsonString(args, "path"))
                Case "get_image_info"
                    If permissionLevel < PermissionSystem Then Return "权限不足：需要系统访问"
                    Return GetImageInfo(Agent通用工具_v6.GetJsonString(args, "path"))
                Case "run_powershell"
                    If permissionLevel < PermissionSystem Then Return "权限不足：需要系统访问"
                    If powerShellSession Is Nothing Then Return "PowerShell 会话不可用。"
                    Return Await powerShellSession.ExecuteAsync(args, cancellationToken)
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
                errors.Add(host & "：" & Agent通用工具_v6.LimitText(result, 300))
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
        Return Agent通用工具_v6.LimitText(Agent通用工具_v6.DecodeTextBytes(File.ReadAllBytes(path)), 20000, "")
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

    Private Shared Function ImageFormatName(format As ImageFormat) As String
        If format.Guid = ImageFormat.Png.Guid Then Return "png"
        If format.Guid = ImageFormat.Jpeg.Guid Then Return "jpeg"
        If format.Guid = ImageFormat.Gif.Guid Then Return "gif"
        If format.Guid = ImageFormat.Bmp.Guid Then Return "bmp"
        Return format.ToString()
    End Function
End Class
