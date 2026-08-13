Imports System.Diagnostics
Imports System.Globalization
Imports System.IO
Imports System.Reflection
Imports System.Runtime.ExceptionServices
Imports System.Text.Json
Imports System.Threading

''' <summary>界面扩展相对于原生锚点的布局方式。</summary>
Public Enum 插件界面锚点位置_v2
    在目标之前
    在目标之后
    装饰目标控件
End Enum

''' <summary>
''' 3FUI 核心与可选插件宿主之间传递的中立上下文。
''' 此类型不引用 Plugin SDK，因此缺少 SDK 时核心处理链仍可正常运行。
''' </summary>
Public NotInheritable Class 插件管线上下文_v2
    Public Property StageId As String = ""
    Public Property PresetJson As String = ""
    Public Property InputPath As String = ""
    Public Property OutputPath As String = ""
    Public Property CommandLine As String = ""
    Public Property ProcessFileName As String = ""
    Public Property TaskId As String = ""
    Public Property SurfaceId As String = ""
    Public Property PhaseName As String = ""
    Public Property IsPreview As Boolean
    Public Property ExitCode As Integer?
    Public Property TaskStatus As String = "unknown"
    Public ReadOnly Property Properties As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)
    Friend Property 进度回调 As Action(Of String, Double?)
    Friend Property 结果回调 As Action(Of String, String, String, String, String)

    Public Sub ReportProgress(message As String, fraction As Double?)
        进度回调?.Invoke(If(message, ""), fraction)
    End Sub

    Public Sub ReportResult(pluginId As String, key As String, value As String, displayName As String, unit As String)
        结果回调?.Invoke(pluginId, key, value, displayName, unit)
    End Sub
End Class

''' <summary>核心内部使用的稳定 UI 锚点 ID。</summary>
Friend Module 插件界面锚点_v2
    Friend Const 视频质量控制方式 As String = "parameters.video.quality.mode"
    Friend Const 视频质量参数名 As String = "parameters.video.quality.parameter-name"
    Friend Const 视频质量值 As String = "parameters.video.quality.value"
    Friend Const 全局质量控制之后 As String = "parameters.video.quality.global.after"
    Friend Const 进阶质量控制之前 As String = "parameters.video.quality.advanced.before"
    Friend Const 视频质量页底部 As String = "parameters.video.quality.page.bottom"
End Module

''' <summary>核心内部使用的稳定处理阶段 ID。</summary>
Friend Module 插件处理阶段_v2
    Friend Const 捕获预设之前 As String = "preset.before-capture"
    Friend Const 捕获预设之后 As String = "preset.after-capture"
    Friend Const 应用预设之前 As String = "preset.before-apply"
    Friend Const 应用预设之后 As String = "preset.after-apply"
    Friend Const 加入队列之前 As String = "queue.before-add"
    Friend Const 准备任务之前 As String = "task.before-prepare"
    Friend Const 准备任务之后 As String = "task.after-prepare"
    Friend Const 构建命令之前 As String = "command.before-build"
    Friend Const 构建命令之后 As String = "command.after-build"
    Friend Const 启动进程之前 As String = "process.before-start"
    Friend Const 进程退出之后 As String = "process.after-exit"
    Friend Const 任务成功之后 As String = "task.after-complete"
    Friend Const 任务失败之后 As String = "task.after-failed"
    Friend Const 任务结束之后 As String = "task.after-finish"
End Module

''' <summary>
''' 可选 Plugin API 的核心桥接层。核心只依赖本模块；只有同时检测到 SDK 与插件宿主时，
''' 才会通过反射进入引用 SDK 的实现程序集。
''' </summary>
Friend Module 插件扩展桥接_v2
    Private Const SDK文件名 As String = "FFmpegFreeUI.PluginSdk.dll"
    Private Const 宿主文件名 As String = "FFmpegFreeUI.PluginHost.dll"
    Private Const 宿主类型名 As String = "FFmpegFreeUI.插件扩展宿主_v2"
    Private ReadOnly SDK最低版本 As New Version(2, 1, 0)
    Private ReadOnly 宿主最低版本 As New Version(2, 1, 0)

    Private ReadOnly 初始化锁 As New Object
    Private ReadOnly 方法缓存 As New Dictionary(Of String, MethodInfo)(StringComparer.Ordinal)
    Private 初始化完成 As Boolean
    Private 宿主类型 As Type

    Friend ReadOnly Property 可用 As Boolean
        Get
            确保初始化()
            Return 宿主类型 IsNot Nothing
        End Get
    End Property

    Friend Function 尝试加载v2插件(程序集 As Assembly) As Boolean
        If 程序集 Is Nothing OrElse Not 可用 Then Return False
        Return CBool(调用宿主(NameOf(尝试加载v2插件), 程序集))
    End Function

    Friend Sub 注册界面锚点(anchorId As String,
                         anchorControl As Control,
                         surface As Control,
                         position As 插件界面锚点位置_v2)
        If Not 可用 Then Exit Sub
        调用宿主(NameOf(注册界面锚点), anchorId, anchorControl, surface, position)
    End Sub

    Friend Sub 还原参数面板插件状态(surface As Control, values As IDictionary(Of String, String))
        If Not 可用 Then Exit Sub
        调用宿主(NameOf(还原参数面板插件状态), surface, values)
    End Sub

    Friend Function 捕获参数面板插件状态(surface As Control) As Dictionary(Of String, String)
        If Not 可用 Then Return New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)
        Return DirectCast(调用宿主(NameOf(捕获参数面板插件状态), surface), Dictionary(Of String, String))
    End Function

    Friend Sub 执行同步阶段(stageId As String, context As 插件管线上下文_v2)
        If context Is Nothing Then Throw New ArgumentNullException(NameOf(context))
        context.StageId = stageId
        If Not 可用 Then Exit Sub
        调用宿主(NameOf(执行同步阶段), stageId, context)
    End Sub

    Friend Function 执行异步阶段Async(stageId As String,
                                 context As 插件管线上下文_v2,
                                 cancellationToken As CancellationToken) As Task
        If context Is Nothing Then Throw New ArgumentNullException(NameOf(context))
        context.StageId = stageId
        If Not 可用 Then Return Task.CompletedTask
        Return DirectCast(调用宿主(NameOf(执行异步阶段Async), stageId, context, cancellationToken), Task)
    End Function

    Friend Function 创建预设管线上下文(stageId As String,
                                  preset As 预设数据_v6,
                                  Optional surface As Control = Nothing,
                                  Optional isPreview As Boolean = False) As 插件管线上下文_v2
        Return New 插件管线上下文_v2 With {
            .StageId = stageId,
            .PresetJson = 序列化预设(preset),
            .SurfaceId = 获取界面标识(surface),
            .IsPreview = isPreview
        }
    End Function

    Friend Function 创建任务管线上下文(stageId As String, task As 编码任务_v6) As 插件管线上下文_v2
        If task Is Nothing Then Throw New ArgumentNullException(NameOf(task))
        Dim result = New 插件管线上下文_v2 With {
            .StageId = stageId,
            .PresetJson = 序列化预设(task.预设数据),
            .InputPath = If(task.输入文件, ""),
            .OutputPath = If(task.输出文件, ""),
            .CommandLine = If(task.命令行, ""),
            .TaskId = If(task.ID, ""),
            .IsPreview = False,
            .TaskStatus = 获取任务状态(task),
            .进度回调 = Sub(message, fraction) 报告任务插件进度(task, stageId, message, fraction),
            .结果回调 = Sub(pluginId, key, value, displayName, unit)
                            task.记录插件结果(pluginId, key, value, displayName, unit)
                        End Sub
        }
        result.Properties("stepCount") = task.步骤.Count.ToString(CultureInfo.InvariantCulture)
        If task.当前步骤索引 >= 0 AndAlso task.当前步骤索引 < task.步骤.Count Then
            result.Properties("stepIndex") = task.当前步骤索引.ToString(CultureInfo.InvariantCulture)
            result.Properties("stepNumber") = (task.当前步骤索引 + 1).ToString(CultureInfo.InvariantCulture)
            result.Properties("isFinalStep") = If(task.当前步骤索引 = task.步骤.Count - 1, "true", "false")
        End If
        Return result
    End Function

    Friend Sub 应用任务管线上下文(task As 编码任务_v6, context As 插件管线上下文_v2)
        If task Is Nothing OrElse context Is Nothing Then Exit Sub
        If task.预设数据 IsNot Nothing OrElse Not String.IsNullOrWhiteSpace(context.PresetJson) Then
            task.预设数据 = 反序列化预设(context.PresetJson, task.预设数据)
        End If
        task.输入文件 = If(context.InputPath, "")
        task.输出文件 = If(context.OutputPath, "")
        task.命令行 = If(context.CommandLine, "")
    End Sub

    Friend Function 序列化预设(preset As 预设数据_v6) As String
        If preset Is Nothing Then Return ""
        预设管理_v6.初始化空集合(preset)
        Return JsonSerializer.Serialize(preset, JsonSO)
    End Function

    Friend Function 反序列化预设(json As String, fallback As 预设数据_v6) As 预设数据_v6
        If String.IsNullOrWhiteSpace(json) Then Return fallback
        Dim result = JsonSerializer.Deserialize(Of 预设数据_v6)(json, JsonSO)
        If result Is Nothing Then Throw New InvalidOperationException("插件处理后返回了空预设")
        预设管理_v6.初始化空集合(result)
        Return result
    End Function

    Private Function 获取界面标识(surface As Control) As String
        If surface Is Nothing OrElse Not 可用 Then Return ""
        Return CStr(调用宿主(NameOf(获取界面标识), surface))
    End Function

    Private Sub 报告任务插件进度(task As 编码任务_v6, stageId As String, message As String, fraction As Double?)
        If task Is Nothing Then Exit Sub
        If fraction.HasValue Then
            task.进度.百分比 = Math.Min(Math.Max(fraction.Value, 0), 1)
            task.进度.进度文本 = $"{task.进度.百分比:P0}"
        End If
        If String.Equals(stageId, 插件处理阶段_v2.任务成功之后, StringComparison.Ordinal) Then
            task.进度.当前阶段 = "插件后处理"
        ElseIf stageId.StartsWith("task.", StringComparison.Ordinal) Then
            task.进度.当前阶段 = "插件任务处理"
        Else
            task.进度.当前阶段 = "插件处理"
        End If
        If Not String.IsNullOrWhiteSpace(message) Then
            task.追加日志("[插件] " & message, 编码任务日志类别_v6.系统)
        Else
            编码队列_v6.通知任务更新(task)
        End If
    End Sub

    Private Function 获取任务状态(task As 编码任务_v6) As String
        If task Is Nothing Then Return "unknown"
        Select Case task.状态
            Case 编码任务状态_v6.未处理 : Return "pending"
            Case 编码任务状态_v6.正在处理 : Return "running"
            Case 编码任务状态_v6.已暂停 : Return "paused"
            Case 编码任务状态_v6.已完成 : Return "succeeded"
            Case 编码任务状态_v6.错误 : Return "failed"
            Case 编码任务状态_v6.已停止 : Return "canceled"
            Case Else : Return "unknown"
        End Select
    End Function

    Private Sub 确保初始化()
        If 初始化完成 Then Exit Sub
        SyncLock 初始化锁
            If 初始化完成 Then Exit Sub
            初始化完成 = True

            Dim sdkPath = Path.Combine(AppContext.BaseDirectory, SDK文件名)
            Dim hostPath = Path.Combine(AppContext.BaseDirectory, 宿主文件名)
            If Not File.Exists(sdkPath) Then
                Debug.WriteLine($"[3FUI Plugin] 未检测到 {SDK文件名}，Plugin API v2 已禁用。")
                Exit Sub
            End If
            If Not File.Exists(hostPath) Then
                Debug.WriteLine($"[3FUI Plugin] 未检测到 {宿主文件名}，Plugin API v2 已禁用。")
                Exit Sub
            End If

            Try
                Dim sdkAssembly = Assembly.LoadFrom(sdkPath)
                Dim apiType = sdkAssembly.GetType("FFmpegFreeUI.PluginSdk.ThreeFuiPluginApi", throwOnError:=True, ignoreCase:=False)
                Dim versionProperty = apiType.GetProperty("Version", BindingFlags.Public Or BindingFlags.Static)
                Dim sdkVersion = TryCast(versionProperty?.GetValue(Nothing), Version)
                If sdkVersion Is Nothing OrElse sdkVersion.Major <> SDK最低版本.Major OrElse sdkVersion < SDK最低版本 Then
                    Debug.WriteLine($"[3FUI Plugin] {SDK文件名} 版本不兼容，需要 2.1.x 或更高的 2.x 版本。")
                    Exit Sub
                End If
                Dim hostAssembly = Assembly.LoadFrom(hostPath)
                Dim hostVersion = hostAssembly.GetName().Version
                If hostVersion Is Nothing OrElse hostVersion.Major <> 宿主最低版本.Major OrElse hostVersion < 宿主最低版本 Then
                    Debug.WriteLine($"[3FUI Plugin] {宿主文件名} 版本不兼容，需要 2.1.x 或更高的 2.x 版本。")
                    Exit Sub
                End If
                宿主类型 = hostAssembly.GetType(宿主类型名, throwOnError:=True, ignoreCase:=False)
            Catch ex As Exception
                宿主类型 = Nothing
                Debug.WriteLine($"[3FUI Plugin] Plugin API v2 初始化失败，已安全禁用：{ex}")
            End Try
        End SyncLock
    End Sub

    Private Function 调用宿主(methodName As String, ParamArray arguments As Object()) As Object
        确保初始化()
        If 宿主类型 Is Nothing Then Return Nothing

        Dim method As MethodInfo = Nothing
        SyncLock 初始化锁
            If Not 方法缓存.TryGetValue(methodName, method) Then
                method = 宿主类型.GetMethod(
                    methodName,
                    BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.Static)
                If method Is Nothing Then Throw New MissingMethodException(宿主类型.FullName, methodName)
                方法缓存(methodName) = method
            End If
        End SyncLock

        Try
            Return method.Invoke(Nothing, arguments)
        Catch ex As TargetInvocationException When ex.InnerException IsNot Nothing
            ExceptionDispatchInfo.Capture(ex.InnerException).Throw()
            Throw
        End Try
    End Function
End Module
