Imports System.Drawing.Imaging
Imports System.IO
Imports System.Net
Imports System.Net.Http
Imports System.Text
Imports System.Text.Json
Imports System.Text.RegularExpressions

Partial Public Class AgentLocalTools
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

    Private Shared Sub AddQueueDiagnostics(payload As Dictionary(Of String, Object), resolution As QueueTargetResolution)
        If resolution.MissingIds.Count > 0 Then payload("missing_ids") = resolution.MissingIds
        If resolution.MissingIndexes.Count > 0 Then payload("missing_indexes") = resolution.MissingIndexes
        If resolution.Errors.Count > 0 Then payload("errors") = resolution.Errors
    End Sub

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
            Return JsonSerializer.Serialize(items, ToolJsonOptions)
        End If

        Dim payload As New Dictionary(Of String, Object) From {
            {"queue_count", snapshot.Count},
            {"returned_count", items.Count},
            {"offset", offset},
            {"limit", limit},
            {"has_more", offset + items.Count < selectedCountBeforePaging},
            {"target_all", resolution.RequestedAll},
            {"used_default_all", resolution.UsedDefaultAll},
            {"tasks", items}
        }
        AddQueueDiagnostics(payload, resolution)
        Return JsonSerializer.Serialize(payload, ToolJsonOptions)
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
            {"tasks", items}
        }
        AddQueueDiagnostics(payload, resolution)
        Return JsonSerializer.Serialize(payload, ToolJsonOptions)
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
        If resolution.MissingIds.Count > 0 OrElse resolution.MissingIndexes.Count > 0 Then
            resolution.Errors.Add("部分目标不存在，未执行队列修改；请确认全部目标后重试")
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
            {"before", beforeItems},
            {"after", afterItems},
            {"message", BuildQueueActionMessage(action, ids.Count, eligibleCount, resolution.Errors)}
        }
        AddQueueDiagnostics(payload, resolution)
        Return JsonSerializer.Serialize(payload, ToolJsonOptions)
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

        If result.RequestedAll AndAlso result.HasSpecificSelectors Then
            result.Errors.Add("target=all 不能与指定 id 或 index 同时使用")
            Return result
        End If
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
        If includePresetJson Then item("preset_json") = If(task.预设数据 Is Nothing, "", JsonSerializer.Serialize(task.预设数据, ToolJsonOptions))
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
End Class
