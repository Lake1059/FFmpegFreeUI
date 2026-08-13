Imports System.Diagnostics
Imports System.Drawing
Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports System.Text.Json
Imports System.Threading
Imports System.Windows.Forms
Imports FFmpegFreeUI.PluginSdk

''' <summary>
''' 3FUI 插件 API v2 的内部宿主。公开给插件的对象均由插件 ID 限定作用域，插件不能冒充其他插件。
''' </summary>
Friend Module 插件扩展宿主_v2

    Private ReadOnly 支持API版本 As New Version(2, 1, 0)
    Private ReadOnly 同步锁 As New Object
    Private ReadOnly 界面扩展列表 As New List(Of 已注册界面扩展)
    Private ReadOnly 处理器列表 As New List(Of 已注册处理器)
    Private ReadOnly 界面锚点列表 As New List(Of 已注册界面锚点)
    Private ReadOnly 参数面板状态表 As New ConditionalWeakTable(Of Form_v6_参数面板, 参数面板插件状态)
    Private ReadOnly 插件实例表 As New Dictionary(Of String, IThreeFuiPlugin)(StringComparer.OrdinalIgnoreCase)
    Private ReadOnly 插件宿主表 As New Dictionary(Of String, IThreeFuiHost)(StringComparer.OrdinalIgnoreCase)

    ''' <summary>尝试从程序集发现并初始化 v2 插件；没有 v2 入口时返回 False。</summary>
    Friend Function 尝试加载v2插件(程序集 As Assembly) As Boolean
        If 程序集 Is Nothing Then Throw New ArgumentNullException(NameOf(程序集))
        Dim pluginTypes = 获取可加载类型(程序集).
            Where(Function(type) GetType(IThreeFuiPlugin).IsAssignableFrom(type) AndAlso
                                 Not type.IsAbstract AndAlso
                                 Not type.IsInterface AndAlso
                                 type.GetConstructor(Type.EmptyTypes) IsNot Nothing).
            OrderBy(Function(type) type.FullName, StringComparer.Ordinal).
            ToList()
        If pluginTypes.Count = 0 Then Return False

        For Each pluginType In pluginTypes
            Dim plugin = DirectCast(Activator.CreateInstance(pluginType), IThreeFuiPlugin)
            Dim pluginId = If(plugin.Id, "").Trim()
            If pluginId = "" Then Throw New InvalidOperationException($"{pluginType.FullName} 的插件 ID 为空")
            If 插件实例表.ContainsKey(pluginId) Then Throw New InvalidOperationException($"插件 ID {pluginId} 已被占用")

            Dim host = 创建插件作用域(pluginId, plugin.DisplayName)
            Try
                plugin.Initialize(host)
            Catch
                TryCast(host, IDisposable)?.Dispose()
                Throw
            End Try
            插件实例表.Add(pluginId, plugin)
            插件宿主表.Add(pluginId, host)
        Next
        Return True
    End Function

    Private Function 获取可加载类型(程序集 As Assembly) As IEnumerable(Of Type)
        Try
            Return 程序集.GetTypes()
        Catch ex As ReflectionTypeLoadException
            Dim loaderMessages = ex.LoaderExceptions.
                Where(Function(item) item IsNot Nothing).
                Select(Function(item) item.Message)
            Throw New InvalidOperationException(
                $"读取插件类型失败：{String.Join("；", loaderMessages)}",
                ex)
        End Try
    End Function

    Friend Function 创建插件作用域(pluginId As String, displayName As String) As IThreeFuiHost
        Dim id = If(pluginId, "").Trim()
        If id = "" Then Throw New ArgumentException("插件 ID 不能为空", NameOf(pluginId))
        Return New 插件作用域宿主(id, If(displayName, "").Trim())
    End Function

    Friend Sub 注册界面锚点(anchorId As String,
                         anchorControl As Control,
                         surface As Form_v6_参数面板,
                         position As 插件界面锚点位置_v2)
        If String.IsNullOrWhiteSpace(anchorId) OrElse anchorControl Is Nothing OrElse surface Is Nothing Then Exit Sub
        Dim anchor As 已注册界面锚点
        Dim extensions As List(Of 已注册界面扩展)

        SyncLock 同步锁
            anchor = 界面锚点列表.FirstOrDefault(
                Function(x) x.Surface Is surface AndAlso String.Equals(x.AnchorId, anchorId, StringComparison.OrdinalIgnoreCase))
            If anchor IsNot Nothing Then Exit Sub

            anchor = New 已注册界面锚点 With {
                .AnchorId = anchorId.Trim(),
                .AnchorControl = anchorControl,
                .Surface = surface,
                .Position = position
            }
            界面锚点列表.Add(anchor)
            extensions = 界面扩展列表.
                Where(Function(x) String.Equals(x.Extension.AnchorId, anchor.AnchorId, StringComparison.OrdinalIgnoreCase)).
                OrderBy(Function(x) x.Extension.Order).
                ThenBy(Function(x) x.PluginId, StringComparer.OrdinalIgnoreCase).
                ThenBy(Function(x) x.Extension.Id, StringComparer.OrdinalIgnoreCase).
                ToList()
        End SyncLock

        AddHandler anchorControl.Disposed, Sub() 移除界面锚点(anchor)
        For Each extension In extensions
            应用界面扩展(anchor, extension)
        Next
    End Sub

    Friend Sub 还原参数面板插件状态(surface As Form_v6_参数面板, values As IDictionary(Of String, String))
        If surface Is Nothing Then Exit Sub
        Dim state = 获取参数面板状态(surface)
        Dim contexts As List(Of 插件界面上下文)
        SyncLock 同步锁
            state.Values.Clear()
            If values IsNot Nothing Then
                For Each pair In values
                    If Not String.IsNullOrWhiteSpace(pair.Key) Then
                        state.Values(pair.Key.Trim()) = 规范化状态Json(pair.Value)
                    End If
                Next
            End If
            contexts = state.Contexts.ToList()
        End SyncLock

        For Each context In contexts
            context.通知状态已还原()
        Next
    End Sub

    Friend Function 捕获参数面板插件状态(surface As Form_v6_参数面板) As Dictionary(Of String, String)
        Dim result As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)
        If surface Is Nothing Then Return result
        Dim state = 获取参数面板状态(surface)
        SyncLock 同步锁
            For Each pair In state.Values
                result(pair.Key) = 规范化状态Json(pair.Value)
            Next
        End SyncLock
        Return result
    End Function

    Friend Function 获取界面标识(surface As Form_v6_参数面板) As String
        If surface Is Nothing Then Return ""
        Return 获取参数面板状态(surface).SurfaceId
    End Function

    Friend Sub 执行同步阶段(stageId As String, context As 插件管线上下文_v2)
        If context Is Nothing Then Throw New ArgumentNullException(NameOf(context))
        context.StageId = stageId
        For Each registration In 获取阶段处理器(stageId)
            Dim sdkContext = 转换到SDK上下文(context, registration.PluginId)
            Try
                Dim pending = registration.Handler.Callback.Invoke(sdkContext, CancellationToken.None)
                If Not pending.IsCompleted Then
                    Throw New InvalidOperationException($"阶段 {stageId} 是同步阶段，处理器不能执行异步等待")
                End If
                pending.GetAwaiter().GetResult()
            Catch ex As Exception
                Throw 包装处理器异常(registration, stageId, ex)
            End Try
            应用SDK上下文(sdkContext, context)
        Next
    End Sub

    Friend Async Function 执行异步阶段Async(stageId As String,
                                       context As 插件管线上下文_v2,
                                       cancellationToken As CancellationToken) As Task
        If context Is Nothing Then Throw New ArgumentNullException(NameOf(context))
        context.StageId = stageId
        For Each registration In 获取阶段处理器(stageId)
            cancellationToken.ThrowIfCancellationRequested()
            Dim sdkContext = 转换到SDK上下文(context, registration.PluginId)
            Try
                Await registration.Handler.Callback.Invoke(sdkContext, cancellationToken).AsTask().ConfigureAwait(False)
            Catch ex As OperationCanceledException When cancellationToken.IsCancellationRequested
                Throw
            Catch ex As Exception
                Throw 包装处理器异常(registration, stageId, ex)
            End Try
            应用SDK上下文(sdkContext, context)
        Next
    End Function

    Private Function 转换到SDK上下文(source As 插件管线上下文_v2, pluginId As String) As PluginPipelineContext
        Dim result As New PluginPipelineContext(
            Sub(progress) source.ReportProgress(progress.Message, progress.Fraction),
            Sub(taskResult) source.ReportResult(
                pluginId,
                taskResult.Key,
                taskResult.Value,
                taskResult.DisplayName,
                taskResult.Unit)) With {
            .StageId = source.StageId,
            .PresetJson = source.PresetJson,
            .InputPath = source.InputPath,
            .OutputPath = source.OutputPath,
            .CommandLine = source.CommandLine,
            .ProcessFileName = source.ProcessFileName,
            .TaskId = source.TaskId,
            .SurfaceId = source.SurfaceId,
            .PhaseName = source.PhaseName,
            .IsPreview = source.IsPreview,
            .ExitCode = source.ExitCode,
            .TaskStatus = 转换任务状态(source.TaskStatus)
        }
        For Each pair In source.Properties
            result.Properties(pair.Key) = pair.Value
        Next
        Return result
    End Function

    Private Sub 应用SDK上下文(source As PluginPipelineContext, target As 插件管线上下文_v2)
        target.PresetJson = source.PresetJson
        target.InputPath = source.InputPath
        target.OutputPath = source.OutputPath
        target.CommandLine = source.CommandLine
        target.ProcessFileName = source.ProcessFileName
        target.TaskId = source.TaskId
        target.SurfaceId = source.SurfaceId
        target.PhaseName = source.PhaseName
        target.IsPreview = source.IsPreview
        target.ExitCode = source.ExitCode
        target.Properties.Clear()
        For Each pair In source.Properties
            target.Properties(pair.Key) = pair.Value
        Next
    End Sub

    Private Function 转换任务状态(value As String) As PluginTaskStatus
        Select Case If(value, "").Trim().ToLowerInvariant()
            Case "pending" : Return PluginTaskStatus.Pending
            Case "running" : Return PluginTaskStatus.Running
            Case "paused" : Return PluginTaskStatus.Paused
            Case "succeeded" : Return PluginTaskStatus.Succeeded
            Case "failed" : Return PluginTaskStatus.Failed
            Case "canceled" : Return PluginTaskStatus.Canceled
            Case Else : Return PluginTaskStatus.Unknown
        End Select
    End Function

    Private Function 注册界面扩展(pluginId As String, extension As PluginUiExtension) As IDisposable
        If extension Is Nothing Then Throw New ArgumentNullException(NameOf(extension))
        If String.IsNullOrWhiteSpace(extension.Id) Then Throw New ArgumentException("界面扩展 ID 不能为空")
        If String.IsNullOrWhiteSpace(extension.AnchorId) Then Throw New ArgumentException("界面锚点 ID 不能为空")
        If extension.CreateControl Is Nothing Then Throw New ArgumentException("界面控件工厂不能为空")

        Dim registration As New 已注册界面扩展 With {.PluginId = pluginId, .Extension = extension}
        Dim anchors As List(Of 已注册界面锚点)
        SyncLock 同步锁
            If 界面扩展列表.Any(Function(x) String.Equals(x.PluginId, pluginId, StringComparison.OrdinalIgnoreCase) AndAlso
                                             String.Equals(x.Extension.Id, extension.Id, StringComparison.OrdinalIgnoreCase)) Then
                Throw New InvalidOperationException($"插件 {pluginId} 已注册界面扩展 {extension.Id}")
            End If
            界面扩展列表.Add(registration)
            anchors = 界面锚点列表.
                Where(Function(x) String.Equals(x.AnchorId, extension.AnchorId, StringComparison.OrdinalIgnoreCase)).
                ToList()
        End SyncLock

        Try
            For Each anchor In anchors
                在控件线程执行(anchor.AnchorControl, Sub() 应用界面扩展(anchor, registration))
            Next
        Catch
            注销界面扩展(registration)
            Throw
        End Try
        Return New 注销句柄(Sub() 注销界面扩展(registration))
    End Function

    Private Function 注册处理器(pluginId As String, handler As PluginPipelineHandler) As IDisposable
        If handler Is Nothing Then Throw New ArgumentNullException(NameOf(handler))
        If String.IsNullOrWhiteSpace(handler.Id) Then Throw New ArgumentException("处理器 ID 不能为空")
        If String.IsNullOrWhiteSpace(handler.StageId) Then Throw New ArgumentException("处理阶段 ID 不能为空")
        If handler.Callback Is Nothing Then Throw New ArgumentException("处理器回调不能为空")
        If Not ThreeFuiPipelineStages.All.Contains(handler.StageId, StringComparer.OrdinalIgnoreCase) Then
            Throw New ArgumentException($"3FUI 不支持处理阶段 {handler.StageId}")
        End If

        Dim registration As New 已注册处理器 With {.PluginId = pluginId, .Handler = handler}
        SyncLock 同步锁
            If 处理器列表.Any(Function(x) String.Equals(x.PluginId, pluginId, StringComparison.OrdinalIgnoreCase) AndAlso
                                           String.Equals(x.Handler.Id, handler.Id, StringComparison.OrdinalIgnoreCase)) Then
                Throw New InvalidOperationException($"插件 {pluginId} 已注册处理器 {handler.Id}")
            End If
            处理器列表.Add(registration)
        End SyncLock
        Return New 注销句柄(Sub()
                                SyncLock 同步锁
                                    处理器列表.Remove(registration)
                                End SyncLock
                            End Sub)
    End Function

    Private Function 获取阶段处理器(stageId As String) As List(Of 已注册处理器)
        SyncLock 同步锁
            Return 处理器列表.
                Where(Function(x) String.Equals(x.Handler.StageId, stageId, StringComparison.OrdinalIgnoreCase)).
                OrderBy(Function(x) x.Handler.Order).
                ThenBy(Function(x) x.PluginId, StringComparer.OrdinalIgnoreCase).
                ThenBy(Function(x) x.Handler.Id, StringComparer.OrdinalIgnoreCase).
                ToList()
        End SyncLock
    End Function

    Private Sub 应用界面扩展(anchor As 已注册界面锚点, registration As 已注册界面扩展)
        If anchor.AnchorControl.IsDisposed Then Exit Sub
        Dim key = registration.PluginId & ":" & registration.Extension.Id
        SyncLock 同步锁
            If anchor.Applied.ContainsKey(key) Then Exit Sub
        End SyncLock

        Dim container As Control = Nothing
        If anchor.Position <> 插件界面锚点位置_v2.装饰目标控件 Then
            container = 获取或创建插入槽(anchor)
        End If
        Dim context As New 插件界面上下文(registration.PluginId,
                                     registration.Extension.Id,
                                     anchor,
                                     container)
        Dim control = registration.Extension.CreateControl.Invoke(context)
        If anchor.Position = 插件界面锚点位置_v2.装饰目标控件 AndAlso control IsNot Nothing Then
            control.Dispose()
            Throw New InvalidOperationException($"装饰型锚点 {anchor.AnchorId} 的控件工厂必须返回 null/Nothing")
        End If

        Dim applied As New 已应用界面扩展 With {
            .Registration = registration,
            .Context = context,
            .Control = control
        }
        SyncLock 同步锁
            anchor.Applied(key) = applied
            获取参数面板状态(anchor.Surface).Contexts.Add(context)
        End SyncLock

        If control IsNot Nothing Then
            If control.Parent IsNot Nothing AndAlso control.Parent IsNot container Then control.Parent.Controls.Remove(control)
            control.Margin = New Padding(0)
            control.Dock = DockStyle.Top
            container.Controls.Add(control)
            AddHandler control.VisibleChanged, Sub() 重排插入槽(anchor)
            重排插入槽(anchor)
        End If
    End Sub

    Private Function 获取或创建插入槽(anchor As 已注册界面锚点) As TableLayoutPanel
        If anchor.Container IsNot Nothing AndAlso Not anchor.Container.IsDisposed Then Return anchor.Container
        Dim parent = anchor.AnchorControl.Parent
        If parent Is Nothing Then Throw New InvalidOperationException($"界面锚点 {anchor.AnchorId} 尚未加入父容器")
        If anchor.AnchorControl.Dock <> DockStyle.Top AndAlso anchor.AnchorControl.Dock <> DockStyle.Bottom Then
            Throw New InvalidOperationException($"插入型界面锚点 {anchor.AnchorId} 必须引用 DockStyle.Top 或 DockStyle.Bottom 控件")
        End If

        Dim slot As New TableLayoutPanel With {
            .Name = "PluginSlot_" & anchor.AnchorId.Replace("."c, "_"c),
            .AutoSize = True,
            .AutoSizeMode = AutoSizeMode.GrowAndShrink,
            .BackColor = Color.Transparent,
            .ColumnCount = 1,
            .Dock = anchor.AnchorControl.Dock,
            .Margin = New Padding(0),
            .Padding = New Padding(0),
            .RowCount = 0
        }
        slot.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100))
        parent.Controls.Add(slot)
        Dim targetIndex = parent.Controls.GetChildIndex(anchor.AnchorControl)
        Dim desiredIndex = If(anchor.Position = 插件界面锚点位置_v2.在目标之前,
                              targetIndex + 1,
                              targetIndex)
        parent.Controls.SetChildIndex(slot, Math.Min(Math.Max(desiredIndex, 0), parent.Controls.Count - 1))
        anchor.Container = slot
        Return slot
    End Function

    Private Sub 重排插入槽(anchor As 已注册界面锚点)
        Dim slot = anchor.Container
        If slot Is Nothing OrElse slot.IsDisposed Then Exit Sub
        Dim ordered As List(Of Control)
        SyncLock 同步锁
            ordered = anchor.Applied.Values.
                Where(Function(x) x.Control IsNot Nothing).
                OrderBy(Function(x) x.Registration.Extension.Order).
                ThenBy(Function(x) x.Registration.PluginId, StringComparer.OrdinalIgnoreCase).
                ThenBy(Function(x) x.Registration.Extension.Id, StringComparer.OrdinalIgnoreCase).
                Select(Function(x) x.Control).
                ToList()
        End SyncLock
        slot.SuspendLayout()
        Try
            slot.RowCount = ordered.Count
            slot.RowStyles.Clear()
            For index = 0 To ordered.Count - 1
                Dim control = ordered(index)
                control.Dock = DockStyle.Top
                slot.SetCellPosition(control, New TableLayoutPanelCellPosition(0, index))
                slot.RowStyles.Add(New RowStyle(SizeType.AutoSize))
            Next
        Finally
            slot.ResumeLayout(True)
        End Try
    End Sub

    Private Sub 注销界面扩展(registration As 已注册界面扩展)
        Dim removals As New List(Of Tuple(Of 已注册界面锚点, 已应用界面扩展))
        Dim key = registration.PluginId & ":" & registration.Extension.Id
        SyncLock 同步锁
            界面扩展列表.Remove(registration)
            For Each anchor In 界面锚点列表
                Dim applied As 已应用界面扩展 = Nothing
                If anchor.Applied.TryGetValue(key, applied) Then
                    anchor.Applied.Remove(key)
                    获取参数面板状态(anchor.Surface).Contexts.Remove(applied.Context)
                    removals.Add(Tuple.Create(anchor, applied))
                End If
            Next
        End SyncLock

        For Each removal In removals
            在控件线程执行(removal.Item1.AnchorControl,
                    Sub()
                        removal.Item2.Control?.Dispose()
                        重排插入槽(removal.Item1)
                    End Sub)
        Next
    End Sub

    Private Sub 移除界面锚点(anchor As 已注册界面锚点)
        SyncLock 同步锁
            界面锚点列表.Remove(anchor)
            Dim state = 获取参数面板状态(anchor.Surface)
            For Each item In anchor.Applied.Values
                state.Contexts.Remove(item.Context)
            Next
            anchor.Applied.Clear()
        End SyncLock
        anchor.Container?.Dispose()
    End Sub

    Private Function 获取参数面板状态(surface As Form_v6_参数面板) As 参数面板插件状态
        Return 参数面板状态表.GetValue(surface, Function(ignored) New 参数面板插件状态)
    End Function

    Private Function 读取界面状态(surface As Form_v6_参数面板, pluginId As String) As String
        Dim state = 获取参数面板状态(surface)
        SyncLock 同步锁
            Dim value As String = Nothing
            If state.Values.TryGetValue(pluginId, value) Then Return 规范化状态Json(value)
        End SyncLock
        Return "{}"
    End Function

    Private Function 获取同界面锚点控件(surface As Form_v6_参数面板, anchorId As String) As Control
        If surface Is Nothing OrElse String.IsNullOrWhiteSpace(anchorId) Then Return Nothing
        SyncLock 同步锁
            Dim anchor = 界面锚点列表.FirstOrDefault(
                Function(x) x.Surface Is surface AndAlso
                            String.Equals(x.AnchorId, anchorId.Trim(), StringComparison.OrdinalIgnoreCase))
            If anchor Is Nothing OrElse anchor.AnchorControl.IsDisposed Then Return Nothing
            Return anchor.AnchorControl
        End SyncLock
    End Function

    Private Sub 写入界面状态(surface As Form_v6_参数面板, pluginId As String, value As String)
        Dim state = 获取参数面板状态(surface)
        SyncLock 同步锁
            state.Values(pluginId) = 规范化状态Json(value)
        End SyncLock
    End Sub

    Private Function 规范化状态Json(value As String) As String
        If String.IsNullOrWhiteSpace(value) Then Return "{}"
        Using document = JsonDocument.Parse(value)
            Return document.RootElement.GetRawText()
        End Using
    End Function

    Private Function 包装处理器异常(registration As 已注册处理器, stageId As String, ex As Exception) As Exception
        Dim actual = If(TypeOf ex Is Reflection.TargetInvocationException AndAlso ex.InnerException IsNot Nothing, ex.InnerException, ex)
        Return New InvalidOperationException(
            $"插件 {registration.PluginId} 的处理器 {registration.Handler.Id} 在阶段 {stageId} 失败：{actual.Message}",
            actual)
    End Function

    Private Sub 在控件线程执行(control As Control, action As Action)
        If control Is Nothing OrElse control.IsDisposed OrElse action Is Nothing Then Exit Sub
        If control.IsHandleCreated AndAlso control.InvokeRequired Then
            control.Invoke(action)
        Else
            action()
        End If
    End Sub

    Private NotInheritable Class 插件作用域宿主
        Implements IThreeFuiHost, IDisposable

        Private ReadOnly _pluginId As String
        Private ReadOnly _displayName As String
        Private ReadOnly _ui As IPluginUiRegistry
        Private ReadOnly _pipeline As IPluginPipelineRegistry
        Private ReadOnly _registrations As New List(Of IDisposable)
        Private ReadOnly _registrationLock As New Object
        Private _disposed As Boolean

        Public Sub New(pluginId As String, displayName As String)
            _pluginId = pluginId
            _displayName = displayName
            _ui = New 插件界面注册表(pluginId, AddressOf 跟踪注册)
            _pipeline = New 插件处理注册表(pluginId, AddressOf 跟踪注册)
        End Sub

        Public ReadOnly Property ApiVersion As Version Implements IThreeFuiHost.ApiVersion
            Get
                ' 返回实际宿主能力，而不是动态读取 SDK 声明，避免版本错配时误报支持。
                Return 支持API版本
            End Get
        End Property

        Public ReadOnly Property HostVersion As String Implements IThreeFuiHost.HostVersion
            Get
                Return GetType(插件管理).Assembly.GetName().Version?.ToString()
            End Get
        End Property

        Public ReadOnly Property Ui As IPluginUiRegistry Implements IThreeFuiHost.Ui
            Get
                Return _ui
            End Get
        End Property

        Public ReadOnly Property Pipeline As IPluginPipelineRegistry Implements IThreeFuiHost.Pipeline
            Get
                Return _pipeline
            End Get
        End Property

        Public Sub Log(level As PluginLogLevel, message As String, Optional exception As Exception = Nothing) Implements IThreeFuiHost.Log
            Dim prefix = If(_displayName = "", _pluginId, _displayName)
            Debug.WriteLine($"[3FUI Plugin/{level}] {prefix}: {message}{If(exception Is Nothing, "", " " & exception.ToString())}")
        End Sub

        Private Sub 跟踪注册(registration As IDisposable)
            SyncLock _registrationLock
                If _disposed Then
                    registration.Dispose()
                    Throw New ObjectDisposedException(NameOf(插件作用域宿主))
                End If
                _registrations.Add(registration)
            End SyncLock
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            Dim registrations As List(Of IDisposable)
            SyncLock _registrationLock
                If _disposed Then Exit Sub
                _disposed = True
                registrations = _registrations.ToList()
                _registrations.Clear()
            End SyncLock
            For index = registrations.Count - 1 To 0 Step -1
                registrations(index).Dispose()
            Next
        End Sub
    End Class

    Private NotInheritable Class 插件界面注册表
        Implements IPluginUiRegistry

        Private ReadOnly _pluginId As String
        Private ReadOnly _track As Action(Of IDisposable)

        Public Sub New(pluginId As String, track As Action(Of IDisposable))
            _pluginId = pluginId
            _track = track
        End Sub

        Public ReadOnly Property AvailableAnchors As IReadOnlyCollection(Of String) Implements IPluginUiRegistry.AvailableAnchors
            Get
                Return ThreeFuiUiAnchors.All
            End Get
        End Property

        Public Function Register(extension As PluginUiExtension) As IDisposable Implements IPluginUiRegistry.Register
            Dim registration = 注册界面扩展(_pluginId, extension)
            _track.Invoke(registration)
            Return registration
        End Function
    End Class

    Private NotInheritable Class 插件处理注册表
        Implements IPluginPipelineRegistry

        Private ReadOnly _pluginId As String
        Private ReadOnly _track As Action(Of IDisposable)

        Public Sub New(pluginId As String, track As Action(Of IDisposable))
            _pluginId = pluginId
            _track = track
        End Sub

        Public ReadOnly Property AvailableStages As IReadOnlyCollection(Of String) Implements IPluginPipelineRegistry.AvailableStages
            Get
                Return ThreeFuiPipelineStages.All
            End Get
        End Property

        Public Function Register(handler As PluginPipelineHandler) As IDisposable Implements IPluginPipelineRegistry.Register
            Dim registration = 注册处理器(_pluginId, handler)
            _track.Invoke(registration)
            Return registration
        End Function
    End Class

    Private NotInheritable Class 插件界面上下文
        Implements IPluginUiContext

        Private ReadOnly _anchor As 已注册界面锚点
        Private ReadOnly _container As Control

        Public Sub New(pluginId As String, extensionId As String, anchor As 已注册界面锚点, container As Control)
            Me.PluginId = pluginId
            Me.ExtensionId = extensionId
            Me.AnchorId = anchor.AnchorId
            Me.SurfaceId = 获取参数面板状态(anchor.Surface).SurfaceId
            _anchor = anchor
            _container = container
        End Sub

        Public ReadOnly Property PluginId As String Implements IPluginUiContext.PluginId
        Public ReadOnly Property ExtensionId As String Implements IPluginUiContext.ExtensionId
        Public ReadOnly Property AnchorId As String Implements IPluginUiContext.AnchorId
        Public ReadOnly Property SurfaceId As String Implements IPluginUiContext.SurfaceId

        Public ReadOnly Property AnchorControl As Control Implements IPluginUiContext.AnchorControl
            Get
                Return _anchor.AnchorControl
            End Get
        End Property

        Public ReadOnly Property ContainerControl As Control Implements IPluginUiContext.ContainerControl
            Get
                Return _container
            End Get
        End Property

        Public Function GetAnchorControl(anchorId As String) As Control Implements IPluginUiContext.GetAnchorControl
            Return 获取同界面锚点控件(_anchor.Surface, anchorId)
        End Function

        Public Property StateJson As String Implements IPluginUiContext.StateJson
            Get
                Return 读取界面状态(_anchor.Surface, PluginId)
            End Get
            Set(value As String)
                写入界面状态(_anchor.Surface, PluginId, value)
            End Set
        End Property

        Public Event StateRestored As EventHandler Implements IPluginUiContext.StateRestored

        Friend Sub 通知状态已还原()
            Dim notify As Action = Sub() RaiseEvent StateRestored(Me, EventArgs.Empty)
            在控件线程执行(_anchor.AnchorControl, notify)
        End Sub

        Public Sub RequestParameterRefresh() Implements IPluginUiContext.RequestParameterRefresh
            在控件线程执行(_anchor.AnchorControl, Sub() _anchor.Surface.请求刷新参数状态())
        End Sub
    End Class

    Private NotInheritable Class 注销句柄
        Implements IDisposable

        Private _disposeAction As Action

        Public Sub New(disposeAction As Action)
            _disposeAction = disposeAction
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            Dim action = Interlocked.Exchange(_disposeAction, Nothing)
            action?.Invoke()
        End Sub
    End Class

    Private NotInheritable Class 参数面板插件状态
        Public ReadOnly SurfaceId As String = Guid.NewGuid().ToString("N")
        Public ReadOnly Values As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)
        Public ReadOnly Contexts As New List(Of 插件界面上下文)
    End Class

    Private NotInheritable Class 已注册界面扩展
        Public Property PluginId As String
        Public Property Extension As PluginUiExtension
    End Class

    Private NotInheritable Class 已注册处理器
        Public Property PluginId As String
        Public Property Handler As PluginPipelineHandler
    End Class

    Private NotInheritable Class 已注册界面锚点
        Public Property AnchorId As String
        Public Property AnchorControl As Control
        Public Property Surface As Form_v6_参数面板
        Public Property Position As 插件界面锚点位置_v2
        Public Property Container As TableLayoutPanel
        Public ReadOnly Applied As New Dictionary(Of String, 已应用界面扩展)(StringComparer.OrdinalIgnoreCase)
    End Class

    Private NotInheritable Class 已应用界面扩展
        Public Property Registration As 已注册界面扩展
        Public Property Context As 插件界面上下文
        Public Property Control As Control
    End Class

End Module
