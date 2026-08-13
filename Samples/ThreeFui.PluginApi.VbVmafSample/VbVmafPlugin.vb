Imports System.Collections.Concurrent
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text.Json
Imports System.Text.Json.Nodes
Imports System.Threading
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports FFmpegFreeUI.PluginSdk

''' <summary>
''' VB.NET 综合示例：以“自动质量策略、命令审计、VMAF 后处理”为应用场景，展示
''' Plugin API v2.1 的全部 UI 锚点和处理阶段。所有会改变任务的选项默认关闭。
''' </summary>
Partial Public NotInheritable Class VbVmafPlugin
    Implements IThreeFuiPlugin

    Friend Const 插件ID As String = "sample.vb-complete-api"
    Friend Const 插件标记 As String = "[VB API 示例]"
    Private Shared ReadOnly 所需API版本 As Version = ThreeFuiPluginApi.Version

    Private ReadOnly 注册项 As New List(Of IDisposable)
    Private ReadOnly 任务会话表 As New ConcurrentDictionary(Of String, 任务会话)(StringComparer.OrdinalIgnoreCase)
    Private 宿主 As IThreeFuiHost

    Public ReadOnly Property Id As String Implements IThreeFuiPlugin.Id
        Get
            Return 插件ID
        End Get
    End Property

    Public ReadOnly Property DisplayName As String Implements IThreeFuiPlugin.DisplayName
        Get
            Return "VB.NET Plugin API 综合示例"
        End Get
    End Property

    Public Sub Initialize(host As IThreeFuiHost) Implements IThreeFuiPlugin.Initialize
        If host Is Nothing Then Throw New ArgumentNullException(NameOf(host))
        If host.ApiVersion < 所需API版本 Then
            Throw New NotSupportedException(
                $"本示例需要 Plugin API {所需API版本} 或更高版本，当前为 {host.ApiVersion}")
        End If

        宿主 = host
        host.Log(
            PluginLogLevel.Information,
            $"正在初始化 {DisplayName}；API={host.ApiVersion}，3FUI={host.HostVersion}")

        注册界面扩展(host)
        注册处理阶段(host)

        ' AvailableAnchors / AvailableStages 用于兼容较旧宿主，不应改用反射查找宿主私有成员。
        For Each anchor In ThreeFuiUiAnchors.All.Except(
            host.Ui.AvailableAnchors,
            StringComparer.OrdinalIgnoreCase)

            host.Log(PluginLogLevel.Warning, $"宿主未提供 UI 锚点：{anchor}")
        Next
        For Each stage In ThreeFuiPipelineStages.All.Except(
            host.Pipeline.AvailableStages,
            StringComparer.OrdinalIgnoreCase)

            host.Log(PluginLogLevel.Warning, $"宿主未提供处理阶段：{stage}")
        Next

        host.Log(PluginLogLevel.Trace, $"已保存 {注册项.Count} 个可释放的注册句柄")
    End Sub

    Private Sub 注册界面扩展(host As IThreeFuiHost)
        按需注册界面(host,
            "decorate-quality-mode",
            ThreeFuiUiAnchors.ParametersVideoQualityMode,
            AddressOf 创建质量模式装饰,
            10)
        按需注册界面(host,
            "decorate-parameter-name",
            ThreeFuiUiAnchors.ParametersVideoQualityParameterName,
            AddressOf 创建参数名装饰,
            20)
        按需注册界面(host,
            "decorate-quality-value",
            ThreeFuiUiAnchors.ParametersVideoQualityValue,
            AddressOf 创建质量值装饰,
            30)
        按需注册界面(host,
            "quality-policy-row",
            ThreeFuiUiAnchors.ParametersVideoQualityAfterGlobal,
            AddressOf 创建质量策略行,
            100)
        按需注册界面(host,
            "command-options-row",
            ThreeFuiUiAnchors.ParametersVideoQualityBeforeAdvanced,
            AddressOf 创建命令选项行,
            200)
        按需注册界面(host,
            "post-process-row",
            ThreeFuiUiAnchors.ParametersVideoQualityPageBottom,
            AddressOf 创建后处理行,
            300)
    End Sub

    Private Sub 按需注册界面(
        host As IThreeFuiHost,
        id As String,
        anchorId As String,
        factory As Func(Of IPluginUiContext, Control),
        order As Integer)

        If Not host.Ui.AvailableAnchors.Contains(anchorId, StringComparer.OrdinalIgnoreCase) Then Return
        注册项.Add(host.Ui.Register(New PluginUiExtension(id, anchorId, factory) With {
            .Order = order
        }))
    End Sub

    Private Sub 注册处理阶段(host As IThreeFuiHost)
        按需注册阶段(host, "migrate-state", ThreeFuiPipelineStages.PresetBeforeApply, AddressOf 应用预设前, -200)
        按需注册阶段(host, "observe-applied-preset", ThreeFuiPipelineStages.PresetAfterApply, AddressOf 应用预设后, 200)
        按需注册阶段(host, "mark-capture", ThreeFuiPipelineStages.PresetBeforeCapture, AddressOf 捕获预设前, -100)
        按需注册阶段(host, "normalize-captured-preset", ThreeFuiPipelineStages.PresetAfterCapture, AddressOf 捕获预设后, 100)
        按需注册阶段(host, "name-queued-task", ThreeFuiPipelineStages.QueueBeforeAdd, AddressOf 加入队列前, 100)
        按需注册阶段(host, "analyze-task", ThreeFuiPipelineStages.TaskBeforePrepare, AddressOf 准备任务前Async, 100)
        按需注册阶段(host, "adjust-structured-command", ThreeFuiPipelineStages.CommandBeforeBuild, AddressOf 构建命令前, 100)
        按需注册阶段(host, "adjust-final-command", ThreeFuiPipelineStages.CommandAfterBuild, AddressOf 构建命令后, 100)
        按需注册阶段(host, "validate-prepared-task", ThreeFuiPipelineStages.TaskAfterPrepare, AddressOf 准备任务后Async, 100)
        按需注册阶段(host, "configure-process", ThreeFuiPipelineStages.ProcessBeforeStart, AddressOf 启动进程前Async, 100)
        按需注册阶段(host, "observe-process", ThreeFuiPipelineStages.ProcessAfterExit, AddressOf 进程退出后Async, 100)
        按需注册阶段(host, "calculate-vmaf", ThreeFuiPipelineStages.TaskAfterComplete, AddressOf 任务成功后Async, 100)
        按需注册阶段(host, "report-failure", ThreeFuiPipelineStages.TaskAfterFailed, AddressOf 任务失败后Async, 100)
        按需注册阶段(host, "release-task-cache", ThreeFuiPipelineStages.TaskAfterFinish, AddressOf 任务结束后Async, 100)
    End Sub

    Private Sub 按需注册阶段(
        host As IThreeFuiHost,
        id As String,
        stageId As String,
        callback As PluginPipelineCallback,
        order As Integer)

        If Not host.Pipeline.AvailableStages.Contains(stageId, StringComparer.OrdinalIgnoreCase) Then Return
        注册项.Add(host.Pipeline.Register(New PluginPipelineHandler(id, stageId, callback) With {
            .Order = order
        }))
    End Sub

    Friend Sub 写日志(level As PluginLogLevel, message As String, Optional exception As Exception = Nothing)
        If 宿主 IsNot Nothing Then 宿主.Log(level, message, exception)
    End Sub

    ' 调用示例：写日志(PluginLogLevel.Error, "处理失败", exception)

    Friend Function 获取或创建任务会话(context As PluginPipelineContext) As 任务会话
        Dim taskId = If(
            String.IsNullOrWhiteSpace(context.TaskId),
            $"preview:{context.SurfaceId}",
            context.TaskId)
        Return 任务会话表.GetOrAdd(taskId, Function(ignored) New 任务会话)
    End Function

    Friend Function 尝试移除任务会话(taskId As String, ByRef session As 任务会话) As Boolean
        If String.IsNullOrWhiteSpace(taskId) Then
            session = Nothing
            Return False
        End If
        Return 任务会话表.TryRemove(taskId, session)
    End Function

    Friend NotInheritable Class 任务会话
        Public ReadOnly Property 开始时间 As DateTimeOffset = DateTimeOffset.Now
        Public Property 输入路径 As String = ""
        Public Property 输出路径 As String = ""
        Public ReadOnly Property 退出码 As New ConcurrentQueue(Of Integer)
    End Class

    Friend NotInheritable Class 插件状态
        Public Property Version As Integer = 2
        Public Property Enabled As Boolean
        Public Property Crf As Integer = 32
        Public Property PrefixTaskName As Boolean
        Public Property OutputSuffix As String = ""
        Public Property AdvancedArguments As String = ""
        Public Property AddNoStats As Boolean
        Public Property AddNoStdin As Boolean
        Public Property ProcessOverride As String = ""
        Public Property AcceptExitCodeOne As Boolean
        Public Property ComputeVmaf As Boolean
        Public Property LastSurfaceId As String = ""
    End Class

    Friend Shared Function 读取状态(json As String) As 插件状态
        If String.IsNullOrWhiteSpace(json) Then Return New 插件状态
        Try
            Return If(JsonSerializer.Deserialize(Of 插件状态)(json), New 插件状态)
        Catch ex As JsonException
            Return New 插件状态
        End Try
    End Function

    Friend Shared Function 读取预设与状态(presetJson As String) As (预设 As JsonObject, 状态 As 插件状态)
        Dim root = JsonNode.Parse(presetJson)
        Dim preset = TryCast(root, JsonObject)
        If preset Is Nothing Then Throw New InvalidOperationException("宿主没有提供有效的预设 JSON 对象")

        Dim stateJson As String = Nothing
        Dim extensionData = TryCast(preset("插件扩展数据"), JsonObject)
        If extensionData IsNot Nothing Then
            Dim stateValue = TryCast(extensionData(插件ID), JsonValue)
            If stateValue IsNot Nothing Then
                Dim parsed As String = Nothing
                If stateValue.TryGetValue(parsed) Then stateJson = parsed
            End If
        End If
        Return (preset, 读取状态(stateJson))
    End Function

    Friend Shared Sub 写入状态(preset As JsonObject, state As 插件状态)
        Dim extensionData = TryCast(preset("插件扩展数据"), JsonObject)
        If extensionData Is Nothing Then
            extensionData = New JsonObject
            preset("插件扩展数据") = extensionData
        End If
        extensionData(插件ID) = JsonValue.Create(JsonSerializer.Serialize(state))
    End Sub

    Friend Shared Sub 写回预设(context As PluginPipelineContext, preset As JsonObject)
        context.PresetJson = preset.ToJsonString(New JsonSerializerOptions With {
            .WriteIndented = True
        })
    End Sub

    Friend Shared Function 是否启用任何功能(state As 插件状态) As Boolean
        Return state IsNot Nothing AndAlso
            (state.Enabled OrElse
             state.PrefixTaskName OrElse
             Not String.IsNullOrWhiteSpace(state.OutputSuffix) OrElse
             Not String.IsNullOrWhiteSpace(state.AdvancedArguments) OrElse
             state.AddNoStats OrElse
             state.AddNoStdin OrElse
             Not String.IsNullOrWhiteSpace(state.ProcessOverride) OrElse
             state.AcceptExitCodeOne OrElse
             state.ComputeVmaf)
    End Function
End Class
