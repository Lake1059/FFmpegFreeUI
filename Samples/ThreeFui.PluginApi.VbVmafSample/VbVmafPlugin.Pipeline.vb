Imports System.Diagnostics
Imports System.Globalization
Imports System.IO
Imports System.Linq
Imports System.Text.Json
Imports System.Text.Json.Nodes
Imports System.Threading
Imports System.Threading.Tasks
Imports FFmpegFreeUI.PluginSdk

Partial Public NotInheritable Class VbVmafPlugin
    ' preset.before-apply：原生控件接收预设前迁移插件状态。
    Private Function 应用预设前(
        context As PluginPipelineContext,
        cancellationToken As CancellationToken) As ValueTask

        cancellationToken.ThrowIfCancellationRequested()
        Dim pair = 读取预设与状态(context.PresetJson)
        If pair.状态.Version < 2 Then
            pair.状态.Version = 2
            写入状态(pair.预设, pair.状态)
            写回预设(context, pair.预设)
            写日志(PluginLogLevel.Information, $"已在 {context.SurfaceId} 迁移示例状态到版本 2")
        End If
        Return ValueTask.CompletedTask
    End Function

    ' preset.after-apply：原生映射已结束，适合观察；此时改 JSON 不会让界面重新映射。
    Private Function 应用预设后(
        context As PluginPipelineContext,
        cancellationToken As CancellationToken) As ValueTask

        cancellationToken.ThrowIfCancellationRequested()
        写日志(PluginLogLevel.Trace, $"预设已应用：stage={context.StageId}, surface={context.SurfaceId}")
        Return ValueTask.CompletedTask
    End Function

    ' preset.before-capture：随后原生控件会覆盖自己拥有的字段，这里只保存辅助状态。
    Private Function 捕获预设前(
        context As PluginPipelineContext,
        cancellationToken As CancellationToken) As ValueTask

        cancellationToken.ThrowIfCancellationRequested()
        Dim pair = 读取预设与状态(context.PresetJson)
        pair.状态.LastSurfaceId = context.SurfaceId
        写入状态(pair.预设, pair.状态)
        写回预设(context, pair.预设)
        Return ValueTask.CompletedTask
    End Function

    ' preset.after-capture：原生字段捕获完成，是最终覆盖质量参数的可靠位置。
    Private Function 捕获预设后(
        context As PluginPipelineContext,
        cancellationToken As CancellationToken) As ValueTask

        cancellationToken.ThrowIfCancellationRequested()
        Dim pair = 读取预设与状态(context.PresetJson)
        If pair.状态.Enabled Then
            应用质量参数(pair.预设, pair.状态)
            写回预设(context, pair.预设)
        End If
        Return ValueTask.CompletedTask
    End Function

    ' queue.before-add：快速修改任务快照和 Properties("taskName")，不可在同步阶段执行耗时 I/O。
    Private Function 加入队列前(
        context As PluginPipelineContext,
        cancellationToken As CancellationToken) As ValueTask

        cancellationToken.ThrowIfCancellationRequested()
        Dim state = 尝试读取状态(context.PresetJson)
        If state IsNot Nothing AndAlso state.PrefixTaskName Then
            Dim taskName As String = Nothing
            If context.Properties.TryGetValue("taskName", taskName) Then
                Dim current = If(taskName, "")
                If Not current.StartsWith(插件标记, StringComparison.Ordinal) Then
                    context.Properties("taskName") = $"{插件标记} {current}".TrimEnd()
                End If
            End If
        End If
        If state IsNot Nothing Then context.OutputPath = 添加输出后缀(context.OutputPath, state.OutputSuffix)
        Return ValueTask.CompletedTask
    End Function

    ' VB.NET 的 Async Function 不能直接返回 ValueTask，所以所有真正异步的方法使用轻量适配器。
    Private Function 准备任务前Async(
        context As PluginPipelineContext,
        cancellationToken As CancellationToken) As ValueTask

        Return New ValueTask(准备任务前核心Async(context, cancellationToken))
    End Function

    ' task.before-prepare：可取消的媒体分析或网络查询放在这里；改预设后宿主据此构建步骤。
    Private Async Function 准备任务前核心Async(
        context As PluginPipelineContext,
        cancellationToken As CancellationToken) As Task

        Dim state = 尝试读取状态(context.PresetJson)
        If Not 是否启用任何功能(state) Then Return

        Dim session = 获取或创建任务会话(context)
        session.输入路径 = context.InputPath
        session.输出路径 = context.OutputPath
        context.OutputPath = 添加输出后缀(context.OutputPath, state.OutputSuffix)
        session.输出路径 = context.OutputPath
        If Not state.Enabled Then Return

        context.ReportProgress("VB 示例正在执行可取消的任务分析……", 0.1)
        Await Task.Delay(80, cancellationToken).ConfigureAwait(False)
        Dim pair = 读取预设与状态(context.PresetJson)
        应用质量参数(pair.预设, pair.状态)
        写回预设(context, pair.预设)
        context.OutputPath = 添加输出后缀(context.OutputPath, pair.状态.OutputSuffix)
        session.输出路径 = context.OutputPath
        context.ReportProgress($"示例质量值已确定为 CRF {pair.状态.Crf}", 0.25)
    End Function

    ' command.before-build：结构化修改 JSON；预览和每个真实步骤都会调用，必须幂等。
    Private Function 构建命令前(
        context As PluginPipelineContext,
        cancellationToken As CancellationToken) As ValueTask

        cancellationToken.ThrowIfCancellationRequested()
        Dim state = 尝试读取状态(context.PresetJson)
        If state Is Nothing OrElse Not state.Enabled OrElse String.IsNullOrWhiteSpace(state.AdvancedArguments) Then
            Return ValueTask.CompletedTask
        End If

        Dim pair = 读取预设与状态(context.PresetJson)
        Dim current = ""
        Dim advancedNode = pair.预设("视频参数_质量控制_进阶参数集")
        If advancedNode IsNot Nothing Then current = advancedNode.GetValue(Of String)()
        pair.预设("视频参数_质量控制_进阶参数集") =
            追加一次(current, state.AdvancedArguments)
        写回预设(context, pair.预设)
        写日志(PluginLogLevel.Trace, $"结构化命令调整：phase={context.PhaseName}, preview={context.IsPreview}")
        Return ValueTask.CompletedTask
    End Function

    ' command.after-build：只能修改最终参数字符串；这里用可重复调用的方式前置全局参数。
    Private Function 构建命令后(
        context As PluginPipelineContext,
        cancellationToken As CancellationToken) As ValueTask

        cancellationToken.ThrowIfCancellationRequested()
        Dim state = 尝试读取状态(context.PresetJson)
        If state IsNot Nothing AndAlso state.AddNoStats Then
            context.CommandLine = 前置选项一次(context.CommandLine, "-nostats")
        End If
        Return ValueTask.CompletedTask
    End Function

    Private Function 准备任务后Async(
        context As PluginPipelineContext,
        cancellationToken As CancellationToken) As ValueTask

        Return New ValueTask(准备任务后核心Async(context, cancellationToken))
    End Function

    ' task.after-prepare：全部步骤首次生成后验证；修改任务数据会使宿主重建步骤。
    Private Async Function 准备任务后核心Async(
        context As PluginPipelineContext,
        cancellationToken As CancellationToken) As Task

        Dim state = 尝试读取状态(context.PresetJson)
        If state Is Nothing OrElse Not state.Enabled Then Return
        Await Task.Yield()
        cancellationToken.ThrowIfCancellationRequested()
        Dim stepCount As String = Nothing
        context.Properties.TryGetValue("stepCount", stepCount)
        context.ReportProgress(
            $"任务准备完成：{If(stepCount, "未知")} 个步骤，输出 {context.OutputPath}",
            0.3)
    End Function

    Private Function 启动进程前Async(
        context As PluginPipelineContext,
        cancellationToken As CancellationToken) As ValueTask

        cancellationToken.ThrowIfCancellationRequested()
        Dim state = 尝试读取状态(context.PresetJson)
        If Not 是否启用任何功能(state) Then Return ValueTask.CompletedTask

        If Not String.IsNullOrWhiteSpace(state.ProcessOverride) Then
            context.ProcessFileName = state.ProcessOverride
        End If
        If state.AddNoStdin Then
            context.CommandLine = 前置选项一次(context.CommandLine, "-nostdin")
        End If
        Dim stepNumber As String = Nothing
        Dim stepCount As String = Nothing
        Dim commandStage As String = Nothing
        context.Properties.TryGetValue("stepNumber", stepNumber)
        context.Properties.TryGetValue("stepCount", stepCount)
        context.Properties.TryGetValue("commandStage", commandStage)
        context.ReportProgress(
            $"准备启动 {context.ProcessFileName}：步骤 {If(stepNumber, "?")}/{If(stepCount, "?")}，" &
            $"阶段 {If(commandStage, context.PhaseName)}")
        Return ValueTask.CompletedTask
    End Function

    ' process.after-exit：观察或按明确协议校正单步骤退出码；它不代表整个任务结束。
    Private Function 进程退出后Async(
        context As PluginPipelineContext,
        cancellationToken As CancellationToken) As ValueTask

        cancellationToken.ThrowIfCancellationRequested()
        Dim state = 尝试读取状态(context.PresetJson)
        If Not 是否启用任何功能(state) Then Return ValueTask.CompletedTask

        If context.ExitCode.HasValue Then
            获取或创建任务会话(context).退出码.Enqueue(context.ExitCode.Value)
            If state.AcceptExitCodeOne AndAlso context.ExitCode.Value = 1 Then
                context.ExitCode = 0
            End If
        End If
        Return ValueTask.CompletedTask
    End Function

    Private Function 任务成功后Async(
        context As PluginPipelineContext,
        cancellationToken As CancellationToken) As ValueTask

        Return New ValueTask(任务成功后核心Async(context, cancellationToken))
    End Function

    ' task.after-complete：适合可取消的成功后处理；示例实际调用 ffmpeg/libvmaf。
    Private Async Function 任务成功后核心Async(
        context As PluginPipelineContext,
        cancellationToken As CancellationToken) As Task

        Dim state = 尝试读取状态(context.PresetJson)
        If state Is Nothing OrElse Not state.ComputeVmaf Then Return
        If Not File.Exists(context.InputPath) Then
            Throw New FileNotFoundException("VMAF 参考文件不存在", context.InputPath)
        End If
        If Not File.Exists(context.OutputPath) Then
            Throw New FileNotFoundException("VMAF 待测文件不存在", context.OutputPath)
        End If

        context.ReportProgress("正在计算 VMAF……", 0.6)
        Dim score = Await 运行VmafAsync(
            context.InputPath,
            context.OutputPath,
            cancellationToken).ConfigureAwait(False)
        Dim scoreText = score.ToString("0.000", CultureInfo.InvariantCulture)
        context.ReportResult("vmaf.mean", scoreText, "VMAF")
        context.ReportResult("vmaf.reference", Path.GetFileName(context.InputPath), "参考文件")
        context.ReportProgress($"VMAF：{scoreText}", 1)
    End Function

    ' task.after-failed：只读诊断并上报；该阶段使用不可取消令牌，应快速返回。
    Private Function 任务失败后Async(
        context As PluginPipelineContext,
        cancellationToken As CancellationToken) As ValueTask

        If Not 是否启用任何功能(尝试读取状态(context.PresetJson)) Then
            Return ValueTask.CompletedTask
        End If

        context.ReportResult("task.failure", context.TaskStatus.ToString(), "失败状态")
        写日志(PluginLogLevel.Error, $"任务 {context.TaskId} 进入 {描述任务状态(context.TaskStatus)} 状态")
        Return ValueTask.CompletedTask
    End Function

    ' task.after-finish：成功、失败、取消都会运行；只做有界且幂等的缓存清理。
    Private Function 任务结束后Async(
        context As PluginPipelineContext,
        cancellationToken As CancellationToken) As ValueTask

        Dim session As 任务会话 = Nothing
        If 尝试移除任务会话(context.TaskId, session) AndAlso session IsNot Nothing Then
            Dim elapsed As String = Nothing
            context.Properties.TryGetValue("elapsedMilliseconds", elapsed)
            context.ReportResult(
                "sample.cleanup",
                $"状态={context.TaskStatus}; 进程数={session.退出码.Count}; 耗时={If(elapsed, "未知")}ms",
                "示例清理")
        End If
        Return ValueTask.CompletedTask
    End Function

    Private Shared Function 尝试读取状态(presetJson As String) As 插件状态
        If String.IsNullOrWhiteSpace(presetJson) Then Return Nothing ' 纯命令行任务允许没有预设 JSON。
        Try
            Return 读取预设与状态(presetJson).状态
        Catch ex As JsonException
            Return Nothing
        End Try
    End Function

    Private Shared Sub 应用质量参数(preset As JsonObject, state As 插件状态)
        preset("视频参数_比特率_控制方式") = 1
        preset("视频参数_质量控制_参数名") = "crf"
        preset("视频参数_质量控制_值") = Math.Clamp(state.Crf, 0, 63).ToString()
    End Sub

    Private Shared Function 添加输出后缀(outputPath As String, suffix As String) As String
        Dim cleanSuffix = If(suffix, "").Trim()
        If String.IsNullOrWhiteSpace(outputPath) OrElse String.IsNullOrWhiteSpace(cleanSuffix) Then Return outputPath
        Dim directory = If(Path.GetDirectoryName(outputPath), "")
        Dim extension = Path.GetExtension(outputPath)
        Dim name = Path.GetFileNameWithoutExtension(outputPath)
        If name.EndsWith(cleanSuffix, StringComparison.OrdinalIgnoreCase) Then Return outputPath
        Return Path.Combine(directory, name & cleanSuffix & extension)
    End Function

    Private Shared Function 追加一次(source As String, token As String) As String
        Dim cleanSource = If(source, "").Trim()
        Dim cleanToken = If(token, "").Trim()
        If cleanSource.Contains(cleanToken, StringComparison.Ordinal) Then Return cleanSource
        Return String.Join(" ", New String() {cleanSource, cleanToken}.Where(Function(value) value.Length > 0))
    End Function

    Private Shared Function 前置选项一次(commandLine As String, optionValue As String) As String
        Dim source = If(commandLine, "")
        If source.Contains(optionValue, StringComparison.Ordinal) Then Return source
        Return $"{optionValue} {source}".TrimEnd()
    End Function

    Private Shared Function 描述任务状态(status As PluginTaskStatus) As String
        Select Case status
            Case PluginTaskStatus.Unknown : Return "未知"
            Case PluginTaskStatus.Pending : Return "等待中"
            Case PluginTaskStatus.Running : Return "运行中"
            Case PluginTaskStatus.Paused : Return "已暂停"
            Case PluginTaskStatus.Succeeded : Return "成功"
            Case PluginTaskStatus.Failed : Return "失败"
            Case PluginTaskStatus.Canceled : Return "已取消"
            Case Else : Return status.ToString()
        End Select
    End Function

    Private Shared Async Function 运行VmafAsync(
        referencePath As String,
        distortedPath As String,
        cancellationToken As CancellationToken) As Task(Of Double)

        Dim resultPath = Path.Combine(Path.GetTempPath(), $"3fui-vmaf-{Guid.NewGuid():N}.json")
        Try
            Dim startInfo As New ProcessStartInfo With {
                .FileName = "ffmpeg",
                .UseShellExecute = False,
                .CreateNoWindow = True,
                .RedirectStandardOutput = True,
                .RedirectStandardError = True
            }
            startInfo.ArgumentList.Add("-hide_banner")
            startInfo.ArgumentList.Add("-nostdin")
            startInfo.ArgumentList.Add("-i")
            startInfo.ArgumentList.Add(distortedPath)
            startInfo.ArgumentList.Add("-i")
            startInfo.ArgumentList.Add(referencePath)
            startInfo.ArgumentList.Add("-lavfi")
            startInfo.ArgumentList.Add(
                $"[0:v]settb=AVTB,setpts=PTS-STARTPTS[dist];" &
                $"[1:v]settb=AVTB,setpts=PTS-STARTPTS[ref];" &
                $"[dist][ref]libvmaf=eof_action=endall:log_fmt=json:" &
                $"log_path='{转义滤镜路径(resultPath)}'")
            startInfo.ArgumentList.Add("-f")
            startInfo.ArgumentList.Add("null")
            startInfo.ArgumentList.Add("-")

            Using process As New Process With {.StartInfo = startInfo}
                process.Start()
                Dim stdoutTask = process.StandardOutput.ReadToEndAsync(cancellationToken)
                Dim stderrTask = process.StandardError.ReadToEndAsync(cancellationToken)
                Try
                    Await process.WaitForExitAsync(cancellationToken).ConfigureAwait(False)
                    Dim stdout = Await stdoutTask.ConfigureAwait(False)
                    Dim stderr = Await stderrTask.ConfigureAwait(False)
                    If process.ExitCode <> 0 Then
                        Dim detail = If(String.IsNullOrWhiteSpace(stderr), stdout, stderr).Trim()
                        Throw New InvalidOperationException($"ffmpeg/libvmaf 退出码 {process.ExitCode}：{detail}")
                    End If
                Catch ex As OperationCanceledException
                    If Not process.HasExited Then process.Kill(entireProcessTree:=True)
                    Throw
                End Try
            End Using

            Return 读取Vmaf均值(resultPath)
        Finally
            Try
                If File.Exists(resultPath) Then File.Delete(resultPath)
            Catch
                ' 临时文件清理失败不能掩盖主要的 VMAF 结果或错误。
            End Try
        End Try
    End Function

    Private Shared Function 转义滤镜路径(path As String) As String
        Return path.Replace("\", "/").Replace(":", "\:").Replace("'", "\'")
    End Function

    Private Shared Function 读取Vmaf均值(path As String) As Double
        Using document = JsonDocument.Parse(File.ReadAllText(path))
            Dim mean = document.RootElement.
                GetProperty("pooled_metrics").
                GetProperty("vmaf").
                GetProperty("mean").
                GetDouble()
            If Double.IsNaN(mean) OrElse Double.IsInfinity(mean) Then
                Throw New InvalidDataException("VMAF 结果不是有效数字")
            End If
            Return mean
        End Using
    End Function
End Class
