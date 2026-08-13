Imports System.IO
Imports System.Reflection
Imports System.Text.Json
Imports System.Text.Json.Serialization

Public Class 插件管理

    Public Shared ReadOnly Property 由插件加载的自定义界面 As New Dictionary(Of String, Control)(StringComparer.CurrentCultureIgnoreCase)
    Private Shared ReadOnly 编码队列事件订阅列表 As New List(Of Action(Of String, String))
    Private Shared ReadOnly 编码队列事件锁 As New Object
    Private Shared ReadOnly 编码队列事件序列化选项 As New JsonSerializerOptions With {
        .PropertyNamingPolicy = Nothing,
        .DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        .WriteIndented = False
    }
    Private Shared 编码队列事件已连接 As Boolean = False

    Public Shared Sub 启动时加载插件()
        If Not 编码队列事件已连接 Then
            AddHandler 编码队列_v6.插件事件已触发, AddressOf 编码队列事件处理
            编码队列事件已连接 = True
        End If
        Dim 插件文件夹路径 As String = Path.Combine(Application.StartupPath, "Plugin")
        If Not Directory.Exists(插件文件夹路径) Then Exit Sub

        For Each 插件文件 In Directory.GetFiles(插件文件夹路径, "*.3fui.dll")
            Try
                加载单个插件(插件文件)
            Catch ex As Exception
                MsgBox($"{插件文件} 加载失败：{ex.Message}", MsgBoxStyle.Exclamation)
            End Try
        Next
    End Sub

    Private Shared Sub 加载单个插件(插件文件 As String)
        Dim 程序集 = Assembly.LoadFrom(插件文件)
        Dim Entry类 As Type = 程序集.GetType(程序集.GetName.Name & ".Entry")

        If Entry类 Is Nothing Then
            MsgBox($"{插件文件} 不包含 Entry 类，无法加载此插件", MsgBoxStyle.Critical)
            Exit Sub
        End If

        Dim Entry类的实例 As Object = Nothing
        If Entry类.GetConstructor(Type.EmptyTypes) IsNot Nothing Then Entry类的实例 = Activator.CreateInstance(Entry类)

        注入宿主回调(Entry类, Entry类的实例, "SetHost_AddCustomWinformPanel", New Action(Of String, Control)(AddressOf 添加自定义Winform面板))
        注入宿主回调(Entry类, Entry类的实例, "SetHost_AddCustomWpfPanel", New Action(Of String, System.Windows.UIElement)(AddressOf 添加自定义Wpf面板))
        注入宿主回调(Entry类, Entry类的实例, "SetHost_AddMissionToQueueWithArgs", New Action(Of String, String, String, String)(AddressOf 使用命令行添加任务到编码队列))
        注入宿主回调(Entry类, Entry类的实例, "SetHost_AddMissionToQueueWith3fuiFile", New Action(Of String, String, String, String)(AddressOf 使用预设文件添加任务到编码队列))
        注入宿主回调(Entry类, Entry类的实例, "SetHost_MediaStreamVisualSelector", New Action(Of String, Object, Object, Object, String, String, String, String)(AddressOf 打开媒体流可视化选择器))
        注入宿主回调(Entry类, Entry类的实例, "SetHost_SubscribeQueueEvents", New Action(Of String, Object)(AddressOf 注册编码队列事件))

        Dim Entry方法 As MethodInfo = Entry类.GetMethod("Entry", BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.Static)
        If Entry方法 Is Nothing Then
            MsgBox($"{插件文件} 找不到在 Entry 类中的 Entry 方法（需要是共享\静态的，VB 使用 Shared，C# 使用 static），此插件未执行初始化", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        Entry方法.Invoke(Nothing, Nothing)
    End Sub

    Private Shared Sub 注册编码队列事件(事件名称 As String, callback As Object)
        Dim action = TryCast(callback, Action(Of String, String))
        If action Is Nothing Then Exit Sub
        Dim filter = If(事件名称, "").Trim()
        SyncLock 编码队列事件锁
            编码队列事件订阅列表.Add(Sub(name As String, data As String)
                               If filter = "" OrElse filter = "*" OrElse String.Equals(filter, name, StringComparison.OrdinalIgnoreCase) Then
                                   Try
                                       action(name, data)
                                   Catch
                                       '插件回调异常不得影响编码队列
                                   End Try
                               End If
                           End Sub)
        End SyncLock
    End Sub

    <CodeAnalysis.SuppressMessage("Style", "IDE0037:使用推断的成员名称", Justification:="<挂起>")>
    Private Shared Sub 编码队列事件处理(事件名称 As String, task As 编码任务_v6, log As 编码任务日志条目_v6)
        Dim data As String
        Try
            data = JsonSerializer.Serialize(New With {
            .eventName = 事件名称,
            .timestamp = DateTime.Now,
            .task = New With {
                .id = task.ID,
                .name = task.任务名称,
                .inputPath = task.输入文件,
                .outputPath = task.输出文件,
                .status = task.状态.ToString(),
                .statusCode = CInt(task.状态),
                .progress = task.进度?.百分比,
                .progressText = task.进度?.进度文本,
                .currentStage = task.当前步骤?.显示名称,
                .realtimeOutput = task.实时输出,
                .nonProgressOutput = task.非进度输出列表?.ToArray()
            },
            .log = If(log Is Nothing, Nothing, New With {
                .sequence = log.序号,
                .time = log.时间,
                .text = log.文本,
                .category = log.类别.ToString(),
                .categoryCode = CInt(log.类别),
                .isError = log.是否错误,
                .stage = log.阶段名
            })
            }, 编码队列事件序列化选项)
        Catch
            Exit Sub
        End Try

        Dim callbacks As List(Of Action(Of String, String))
        SyncLock 编码队列事件锁
            callbacks = 编码队列事件订阅列表.ToList()
        End SyncLock
        For Each callback In callbacks
            Try
                callback(事件名称, data)
            Catch
            End Try
        Next
    End Sub

    Private Shared Sub 注入宿主回调(entryType As Type, entryInstance As Object, methodName As String, callback As Object)
        Dim method = entryType.GetMethod(methodName, BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.Static Or BindingFlags.Instance)
        If method Is Nothing Then Exit Sub
        Dim target = If(method.IsStatic, Nothing, entryInstance)
        If target Is Nothing AndAlso Not method.IsStatic Then Exit Sub
        method.Invoke(target, New Object() {callback})
    End Sub

    Public Shared Sub 添加自定义Winform面板(Name As String, C As Control)
        If String.IsNullOrWhiteSpace(Name) OrElse C Is Nothing Then Exit Sub
        UI线程执行(Sub()
                   Dim 标题 = Name.Trim()
                   由插件加载的自定义界面(标题) = C
                   FormMain_v6.添加插件选项卡(标题, C)
               End Sub)
    End Sub

    Public Shared Sub 添加自定义Wpf面板(Name As String, UIE As System.Windows.UIElement)
        If String.IsNullOrWhiteSpace(Name) OrElse UIE Is Nothing Then Exit Sub
        UI线程执行(Sub()
                   Dim 标题 = Name.Trim()
                   Dim host As New Integration.ElementHost With {.Child = UIE, .Dock = DockStyle.Fill}
                   由插件加载的自定义界面(标题) = host
                   FormMain_v6.添加插件选项卡(标题, host)
               End Sub)
    End Sub

    Public Shared Sub 使用命令行添加任务到编码队列(FFmpegArg As String, FileName As String, OutputPath As String, Optional InputPath As String = "")
        Dim taskName = If(String.IsNullOrWhiteSpace(FileName), $"插件命令行任务 {Now:HHmmss}", FileName)
        编码队列_v6.添加命令行任务(If(FFmpegArg, ""), taskName, If(OutputPath, ""), If(InputPath, ""))
    End Sub

    Public Shared Sub 使用预设文件添加任务到编码队列(File_3FUI_JsonPath As String, FileName As String, OutputPath As String, Optional InputPath As String = "")
        Dim preset = 启动参数响应_v6.读取预设数据(File_3FUI_JsonPath)
        If preset Is Nothing Then
            MsgBox("指定的 v6 预设文件不存在或无法读取", MsgBoxStyle.Critical)
            Exit Sub
        End If

        Dim input = If(InputPath, "")
        If input = "" AndAlso File.Exists(FileName) Then input = FileName
        Dim taskName = If(String.IsNullOrWhiteSpace(FileName), If(input <> "", Path.GetFileName(input), $"插件预设任务 {Now:HHmmss}"), FileName)
        编码队列_v6.添加预设任务(input, preset, taskName, If(OutputPath, ""))
    End Sub

    Public Shared Sub 打开媒体流可视化选择器(FilePath As String,
                                  VideoStreamTargetObject As Object,
                                  AudioStreamTargetObject As Object,
                                  SubtitleStreamTargetObject As Object,
                                  InputFileIndex As String,
                                  VideoStreamSelected As String,
                                  AudioStreamSelected As String,
                                  SubtitleStreamSelected As String)
        UI线程执行(Sub()
                   显示窗体(New Form_v6_媒体流选择器(要读取的媒体文件:=FilePath,
                    视频流文本目标对象:=VideoStreamTargetObject,
                    音频流文本目标对象:=AudioStreamTargetObject,
                    字幕流文本目标对象:=SubtitleStreamTargetObject,
                    文件索引:=InputFileIndex,
                    视频流已选:=VideoStreamSelected,
                    音频流已选:=AudioStreamSelected,
                    字幕流已选:=SubtitleStreamSelected), FormMain_v6)
               End Sub)
    End Sub

    Private Shared Sub UI线程执行(action As Action)
        If action Is Nothing Then Exit Sub
        If FormMain_v6 IsNot Nothing AndAlso FormMain_v6.IsHandleCreated AndAlso Not FormMain_v6.IsDisposed Then
            If FormMain_v6.InvokeRequired Then
                FormMain_v6.BeginInvoke(action)
            Else
                action()
            End If
        Else
            action()
        End If
    End Sub

End Class
