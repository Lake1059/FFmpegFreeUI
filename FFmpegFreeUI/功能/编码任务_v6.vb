Imports System.Globalization
Imports System.IO
Imports System.Text

Public Enum 编码任务状态_v6
    未处理 = 0
    正在处理 = 1
    已暂停 = 2
    已完成 = 10
    已停止 = 20
    错误 = 99
End Enum

Public Enum 编码步骤状态_v6
    未处理 = 0
    正在处理 = 1
    已完成 = 10
    已停止 = 20
    错误 = 99
End Enum

Public Enum 编码任务日志类别_v6
    系统 = 0
    输出 = 1
    进度 = 2
    错误 = 3
End Enum

Public Enum 编码任务日志显示模式_v6
    全部输出 = 0
    最新输出不含进度 = 1
    仅错误信息 = 2
    当前阶段输出 = 3
End Enum

Public Class 编码任务日志条目_v6
    Public Property 序号 As Long
    Public Property 时间 As DateTime
    Public Property 阶段名 As String = ""
    Public Property 文本 As String = ""
    Public Property 类别 As 编码任务日志类别_v6 = 编码任务日志类别_v6.输出
    Public Property 是否错误 As Boolean = False
End Class

Public Class 编码任务日志版本信息_v6
    Public Property 日志版本号 As Long = 0
    Public Property 日志结构版本号 As Long = 0
End Class

Public Class 编码任务日志快照_v6
    Public Property 条目 As New List(Of 编码任务日志条目_v6)
    Public Property 日志版本号 As Long = 0
    Public Property 日志结构版本号 As Long = 0
End Class

Public Class 编码任务_v6
    Public ReadOnly Property ID As String = Guid.NewGuid().ToString("N")
    Public Property 任务名称 As String = ""
    Public Property 输入文件 As String = ""
    Public Property 输出文件 As String = ""
    Public Property 输出文件由自动命名生成 As Boolean = False
    Public Property 预设数据 As 预设数据_v6
    Public Property 命令行 As String = ""
    Public Property 状态 As 编码任务状态_v6 = 编码任务状态_v6.未处理
    Public Property 步骤 As New List(Of 编码步骤_v6)
    Public Property 当前步骤索引 As Integer = -1
    Public Property 允许自动启动 As Boolean = 设置_v6.实例对象.自动开始任务选项 = 0
    Public Property 手动停止 As Boolean = False
    Public Property 实时输出 As String = ""
    Public Property 错误列表 As New List(Of String)
    Public Property 非进度输出列表 As New List(Of String)
    Public Property 进度 As New 编码进度_v6
    Public Property 任务耗时计时器 As New Stopwatch
    Public Property 输入文件大小 As Long = 0
    Public Property 媒体总时长 As String = ""
    Public Property AviSynthCachePath As String = ""
    Public Property VapourSynthCachePath As String = ""
    Public Property 最新底部日志文本 As String = ""
    Public Property 最新底部日志是否错误 As Boolean = False
    Public Property 日志版本号 As Long = 0
    Public Property 日志结构版本号 As Long = 0

    Private 当前进程 As Process
    Private ReadOnly 状态锁 As New Object
    Private ReadOnly 输出锁 As New Object
    Private 正在执行标记 As Boolean = False
    Private 已实际开始标记 As Boolean = False
    Private 启动准备异常 As Exception
    Private 当前执行已请求停止 As Boolean = False
    Private 执行版本 As Long = 0
    Private ReadOnly 日志锁 As New Object
    Private ReadOnly 完整日志缓存 As New List(Of 编码任务日志条目_v6)
    Private 日志序号 As Long = 0
    Private 进度日志数量 As Integer = 0
    Private 最新原始日志文本 As String = ""
    Private 最新原始日志是否错误 As Boolean = False
    Private 最新非进度日志文本 As String = ""
    Private 最新非进度日志是否错误 As Boolean = False
    Private 上次进度日志提交时间 As Long = -1000

    Public ReadOnly Property 可移除 As Boolean
        Get
            Return Not 正在执行 AndAlso (状态 = 编码任务状态_v6.未处理 OrElse 状态 = 编码任务状态_v6.已完成 OrElse 状态 = 编码任务状态_v6.已停止 OrElse 状态 = 编码任务状态_v6.错误)
        End Get
    End Property

    Public ReadOnly Property 可重置 As Boolean
        Get
            Return Not 正在执行 AndAlso (状态 = 编码任务状态_v6.已完成 OrElse 状态 = 编码任务状态_v6.已停止 OrElse 状态 = 编码任务状态_v6.错误)
        End Get
    End Property

    Public ReadOnly Property 可停止 As Boolean
        Get
            Return 状态 = 编码任务状态_v6.正在处理 OrElse 状态 = 编码任务状态_v6.已暂停 OrElse 状态 = 编码任务状态_v6.错误
        End Get
    End Property

    Public ReadOnly Property 可排序 As Boolean
        Get
            Return Not 正在执行 AndAlso 状态 <> 编码任务状态_v6.正在处理 AndAlso 状态 <> 编码任务状态_v6.已暂停
        End Get
    End Property

    Public ReadOnly Property 当前步骤 As 编码步骤_v6
        Get
            Dim items = 步骤
            Dim index = 当前步骤索引
            If index < 0 OrElse index >= items.Count Then Return Nothing
            Return items(index)
        End Get
    End Property

    Public ReadOnly Property 当前进程ID As Integer
        Get
            Try
                Dim process = 当前进程
                If process Is Nothing OrElse process.HasExited Then Return 0
                Return process.Id
            Catch
                Return 0
            End Try
        End Get
    End Property

    Public ReadOnly Property 正在执行 As Boolean
        Get
            SyncLock 状态锁
                Return 正在执行标记
            End SyncLock
        End Get
    End Property

    Public ReadOnly Property 已实际开始 As Boolean
        Get
            SyncLock 状态锁
                Return 已实际开始标记
            End SyncLock
        End Get
    End Property

    Friend ReadOnly Property 可自动开始 As Boolean
        Get
            SyncLock 状态锁
                Return 允许自动启动 AndAlso Not 已实际开始标记 AndAlso Not 正在执行标记 AndAlso
                       (状态 = 编码任务状态_v6.未处理 OrElse 状态 = 编码任务状态_v6.已停止)
            End SyncLock
        End Get
    End Property

    Friend Function 开始执行() As Long
        SyncLock 状态锁
            If 正在执行标记 OrElse 状态 <> 编码任务状态_v6.未处理 Then Return 0
            正在执行标记 = True
            已实际开始标记 = True
            启动准备异常 = Nothing
            当前执行已请求停止 = False
            手动停止 = False
            状态 = 编码任务状态_v6.正在处理
            执行版本 += 1
            Return 执行版本
        End SyncLock
    End Function

    Friend Function 结束执行() As Boolean
        SyncLock 状态锁
            Dim result = 当前执行已请求停止
            正在执行标记 = False
            ' 停止清理结束后，才允许下一次自动调度按队列顺序重启。
            If result Then 已实际开始标记 = False
            当前执行已请求停止 = False
            Return result
        End SyncLock
    End Function

    Friend Sub 准备启动输出文件(Optional 保留已分配输出 As Boolean = False)
        If 保留已分配输出 AndAlso Not String.IsNullOrWhiteSpace(输出文件) Then Exit Sub
        Try
            编码队列_v6.为任务保留输出文件(Me)
        Catch ex As Exception
            ' 交给执行流程统一记录失败、清理资源并推进队列。
            启动准备异常 = ex
        End Try
    End Sub

    Private Function 是当前执行(执行标识 As Long) As Boolean
        SyncLock 状态锁
            Return 正在执行标记 AndAlso 执行版本 = 执行标识
        End SyncLock
    End Function

    Public ReadOnly Property 当前进程名称 As String
        Get
            Try
                Dim process = 当前进程
                If process Is Nothing OrElse process.HasExited Then Return ""
                Dim fileName = If(process.StartInfo?.FileName, "")
                If String.IsNullOrWhiteSpace(fileName) Then Return "ffmpeg"
                Return Path.GetFileNameWithoutExtension(fileName)
            Catch
                Return ""
            End Try
        End Get
    End Property

    Public Function 获取日志快照(Optional 显示模式 As 编码任务日志显示模式_v6 = 编码任务日志显示模式_v6.全部输出) As List(Of 编码任务日志条目_v6)
        SyncLock 日志锁
            Return 筛选日志条目(显示模式)
        End SyncLock
    End Function

    Public Function 获取日志版本信息() As 编码任务日志版本信息_v6
        SyncLock 日志锁
            Return New 编码任务日志版本信息_v6 With {
                .日志版本号 = 日志版本号,
                .日志结构版本号 = 日志结构版本号
            }
        End SyncLock
    End Function

    Public Function 获取日志快照数据(Optional 显示模式 As 编码任务日志显示模式_v6 = 编码任务日志显示模式_v6.全部输出,
                              Optional 阶段名 As String = Nothing) As 编码任务日志快照_v6
        SyncLock 日志锁
            Return New 编码任务日志快照_v6 With {
                .条目 = 筛选日志条目(显示模式, 阶段名),
                .日志版本号 = 日志版本号,
                .日志结构版本号 = 日志结构版本号
            }
        End SyncLock
    End Function

    Private Function 筛选日志条目(显示模式 As 编码任务日志显示模式_v6, Optional 指定阶段名 As String = Nothing) As List(Of 编码任务日志条目_v6)
        Select Case 显示模式
            Case 编码任务日志显示模式_v6.最新输出不含进度
                Return 完整日志缓存.Where(Function(x) x.类别 <> 编码任务日志类别_v6.进度).ToList()
            Case 编码任务日志显示模式_v6.仅错误信息
                Return 完整日志缓存.Where(Function(x) x.是否错误 OrElse x.类别 = 编码任务日志类别_v6.错误).ToList()
            Case 编码任务日志显示模式_v6.当前阶段输出
                Dim stageName = If(指定阶段名, If(当前步骤?.显示名称, ""))
                If String.IsNullOrWhiteSpace(stageName) Then Return New List(Of 编码任务日志条目_v6)
                Return 完整日志缓存.Where(Function(x) String.Equals(x.阶段名, stageName, StringComparison.Ordinal)).ToList()
            Case Else
                Return 完整日志缓存.ToList()
        End Select
    End Function

    Public Function 获取日志文本(Optional 显示模式 As 编码任务日志显示模式_v6 = 编码任务日志显示模式_v6.全部输出) As String
        Return String.Join(vbCrLf, 获取日志快照(显示模式).Select(Function(x) x.文本))
    End Function

    Public Sub 追加日志(文本 As String, Optional 类别 As 编码任务日志类别_v6 = 编码任务日志类别_v6.输出, Optional 步骤项 As 编码步骤_v6 = Nothing, Optional 强制错误 As Boolean = False, Optional 通知更新 As Boolean = True)
        If 文本 Is Nothing Then Exit Sub
        Dim stageName = If(步骤项?.显示名称, If(当前步骤?.显示名称, ""))
        Dim isError = 强制错误 OrElse 类别 = 编码任务日志类别_v6.错误 OrElse 编码队列_v6.是否错误输出(文本)
        Dim addedEntry As 编码任务日志条目_v6 = Nothing
        SyncLock 日志锁
            If 类别 = 编码任务日志类别_v6.进度 AndAlso Not isError Then
                Dim nowTime = Environment.TickCount64
                If nowTime - 上次进度日志提交时间 < 1000 Then Exit Sub
                上次进度日志提交时间 = nowTime
            End If

            日志序号 += 1
            addedEntry = New 编码任务日志条目_v6 With {
                .序号 = 日志序号,
                .时间 = DateTime.Now,
                .阶段名 = stageName,
                .文本 = 文本,
                .类别 = If(isError, 编码任务日志类别_v6.错误, 类别),
                .是否错误 = isError
            }
            完整日志缓存.Add(addedEntry)
            If addedEntry.类别 = 编码任务日志类别_v6.进度 Then 进度日志数量 += 1
            日志版本号 += 1
            实时输出 = 文本

            最新原始日志文本 = 文本
            最新原始日志是否错误 = isError
            If addedEntry.类别 <> 编码任务日志类别_v6.进度 Then
                非进度输出列表.Add(文本)
                最新非进度日志文本 = 文本
                最新非进度日志是否错误 = isError
            End If
            If isError Then 错误列表.Add(文本)
            更新最新底部日志()
            裁剪日志缓存()
        End SyncLock
        If addedEntry IsNot Nothing Then
            If addedEntry.类别 = 编码任务日志类别_v6.进度 Then
                编码队列_v6.触发插件事件("task.progress", Me, addedEntry)
            Else
                编码队列_v6.触发插件事件("task.log", Me, addedEntry)
            End If
        End If
        If 通知更新 Then 编码队列_v6.通知任务更新(Me, 立即刷新:=False)
    End Sub

    Public Sub 清空日志(Optional 通知更新 As Boolean = True)
        SyncLock 日志锁
            完整日志缓存.Clear()
            进度日志数量 = 0
            错误列表.Clear()
            非进度输出列表.Clear()
            实时输出 = ""
            最新底部日志文本 = ""
            最新底部日志是否错误 = False
            最新原始日志文本 = ""
            最新原始日志是否错误 = False
            最新非进度日志文本 = ""
            最新非进度日志是否错误 = False
            上次进度日志提交时间 = -1000
            日志版本号 += 1
            日志结构版本号 += 1
        End SyncLock
        If 通知更新 Then 编码队列_v6.通知任务更新(Me)
    End Sub

    Private Sub 更新最新底部日志()
        最新底部日志文本 = 最新原始日志文本
        最新底部日志是否错误 = 最新原始日志是否错误
    End Sub

    Private Sub 裁剪日志缓存()
        Dim limit = 获取日志保留行数()
        If limit <= 0 Then Exit Sub
        If 完整日志缓存.Count <= limit OrElse 进度日志数量 <= 0 Then Exit Sub

        Dim removeCount = Math.Min(完整日志缓存.Count - limit, 进度日志数量)
        Dim removed As Integer = 0
        完整日志缓存.RemoveAll(Function(entry)
                             If removed >= removeCount OrElse entry.类别 <> 编码任务日志类别_v6.进度 Then Return False
                             removed += 1
                             Return True
                         End Function)
        If removed = 0 Then Exit Sub
        进度日志数量 -= removed
        日志结构版本号 += 1
    End Sub

    Private Shared Function 获取日志保留行数() As Integer
        Select Case 设置_v6.实例对象.任务日志保留行数选项
            Case 0 : Return 100
            Case 1 : Return 500
            Case 2 : Return 1000
            Case 3 : Return 0
            Case Else : Return 500
        End Select
    End Function

    Public Async Function 开始Async(执行标识 As Long) As Task
        SyncLock 状态锁
            If Not 正在执行标记 OrElse 执行版本 <> 执行标识 Then Exit Function
            If 手动停止 OrElse 状态 = 编码任务状态_v6.已停止 Then Exit Function
            状态 = 编码任务状态_v6.正在处理
        End SyncLock
        Try
            清空日志(False)
            进度 = New 编码进度_v6
            任务耗时计时器.Restart()
            追加日志($"[3FUI] 任务开始：{If(任务名称 <> "", 任务名称, Path.GetFileName(输入文件))}", 编码任务日志类别_v6.系统, Nothing, False, False)
            If 启动准备异常 IsNot Nothing Then Throw 启动准备异常
            准备输入输出与步骤()
            If Not 是当前执行(执行标识) Then Exit Try
            设定系统状态_v6()
            编码队列_v6.通知任务更新(Me)

            当前步骤索引 = 0
            While 当前步骤索引 < 步骤.Count
                If 手动停止 Then Exit While
                Dim stepItem = 步骤(当前步骤索引)
                进度 = New 编码进度_v6 With {.总时长 = 计算当前总时长()}
                stepItem.状态 = 编码步骤状态_v6.正在处理
                进度.当前阶段 = stepItem.显示名称
                追加日志($"[3FUI] 开始阶段：{stepItem.显示名称}", 编码任务日志类别_v6.系统, stepItem, False, False)
                编码队列_v6.通知任务更新(Me)
                Dim exitCode = Await 运行步骤Async(stepItem)
                If Not 是当前执行(执行标识) Then Exit While
                If 手动停止 Then
                    stepItem.状态 = 编码步骤状态_v6.已停止
                    状态 = 编码任务状态_v6.已停止
                    追加日志("[3FUI] 任务已手动停止", 编码任务日志类别_v6.系统, stepItem, False, False)
                    手动停止后清理输出()
                    Exit While
                End If
                If exitCode <> 0 Then
                    stepItem.状态 = 编码步骤状态_v6.错误
                    状态 = 编码任务状态_v6.错误
                    追加日志($"[3FUI] 阶段 {stepItem.显示名称} 退出码 {exitCode}", 编码任务日志类别_v6.错误, stepItem, True, False)
                    编码队列_v6.标记任务出错()
                    失败后清理输出()
                    Exit While
                End If
                stepItem.状态 = 编码步骤状态_v6.已完成
                追加日志($"[3FUI] 完成阶段：{stepItem.显示名称}", 编码任务日志类别_v6.系统, stepItem, False, False)
                If stepItem.阶段 = 预设数据_v6.命令行阶段.FFprobe获取时长 Then
                    媒体总时长 = 提取FFprobe时长(stepItem.输出缓存)
                    If String.IsNullOrWhiteSpace(媒体总时长) Then
                        状态 = 编码任务状态_v6.错误
                        追加日志("[3FUI] ffprobe 未能获取媒体总时长", 编码任务日志类别_v6.错误, stepItem, True, False)
                        编码队列_v6.标记任务出错()
                        Exit While
                    End If
                    追加日志($"[3FUI] 已获取媒体总时长：{媒体总时长}s", 编码任务日志类别_v6.系统, stepItem, False, False)
                    重建编码步骤()
                    当前步骤索引 = 0
                    Continue While
                End If
                当前步骤索引 += 1
            End While

            SyncLock 状态锁
                If 是当前执行(执行标识) AndAlso Not 手动停止 AndAlso (状态 = 编码任务状态_v6.正在处理 OrElse 状态 = 编码任务状态_v6.已暂停) Then
                    状态 = 编码任务状态_v6.已完成
                End If
            End SyncLock
            If 状态 = 编码任务状态_v6.已完成 Then
                进度.百分比 = 1
                进度.进度文本 = "100%"
                完成后处理输出时间()
                追加日志("[3FUI] 任务完成", 编码任务日志类别_v6.系统, Nothing, False, False)
            End If
        Catch ex As Exception
            If Not 是当前执行(执行标识) Then Exit Try
            状态 = If(手动停止, 编码任务状态_v6.已停止, 编码任务状态_v6.错误)
            Dim logCategory = If(状态 = 编码任务状态_v6.已停止, 编码任务日志类别_v6.系统, 编码任务日志类别_v6.错误)
            追加日志("[3FUI] " & ex.Message, logCategory, 当前步骤, 状态 = 编码任务状态_v6.错误, False)
            If 状态 = 编码任务状态_v6.错误 Then 编码队列_v6.标记任务出错()
        Finally
            任务耗时计时器.Stop()
            用户使用统计_v6.记录编码任务执行结果(状态 = 编码任务状态_v6.已完成, 任务耗时计时器.Elapsed)
            释放进程()
            清理帧服务器缓存()
            恢复系统状态_v6()
            编码队列_v6.通知任务更新(Me)
            If 状态 = 编码任务状态_v6.已完成 Then
                编码队列_v6.触发插件事件("task.completed", Me)
            ElseIf 状态 = 编码任务状态_v6.错误 Then
                编码队列_v6.触发插件事件("task.failed", Me)
            End If
        End Try
    End Function

    Private Sub 准备输入输出与步骤()
        If File.Exists(输入文件) Then
            Try
                输入文件大小 = New FileInfo(输入文件).Length
            Catch
            End Try
        End If

        If 预设数据 IsNot Nothing Then
            预设管理_v6.初始化空集合(预设数据)
            生成帧服务器脚本()
            If String.IsNullOrWhiteSpace(输出文件) Then 输出文件 = 编码队列_v6.计算输出位置_v6(输入文件, 预设数据, True)
            重建编码步骤()
        Else
            步骤 = New List(Of 编码步骤_v6) From {New 编码步骤_v6 With {
                .阶段 = 预设数据_v6.命令行阶段.普通单次,
                .命令行 = 命令行,
                .显示名称 = "命令行"
            }}
        End If
        If 步骤.Count = 0 Then Throw New InvalidOperationException("没有可执行的编码步骤")
    End Sub

    Private Sub 重建编码步骤()
        If 预设数据 Is Nothing Then Exit Sub
        Dim generated = 预设管理_v6.生成阶段化命令行(预设数据, 输入文件, 输出文件, 媒体总时长, ID)
        Dim items As New List(Of 编码步骤_v6)
        For Each item In generated
            If item.阶段 = 预设数据_v6.命令行阶段.FFprobe获取时长 AndAlso Not String.IsNullOrWhiteSpace(媒体总时长) Then Continue For
            items.Add(New 编码步骤_v6 With {
                .阶段 = item.阶段,
                .命令行 = item.命令行,
                .滤镜图 = item.滤镜图,
                .映射参数 = item.映射参数,
                .输出滤镜参数 = item.输出滤镜参数,
                .需要媒体总时长 = item.需要媒体总时长,
                .说明 = item.说明,
                .显示名称 = 获取阶段显示名称(item.阶段)
            })
        Next
        步骤 = items
    End Sub

    Private Async Function 运行步骤Async(stepItem As 编码步骤_v6) As Task(Of Integer)
        Dim process As New Process()
        process.StartInfo.FileName = If(stepItem.阶段 = 预设数据_v6.命令行阶段.FFprobe获取时长, 设置_v6.获取FFprobe进程文件名(), 设置_v6.获取FFmpeg进程文件名())
        process.StartInfo.WorkingDirectory = 设置_v6.获取有效工作目录()
        process.StartInfo.Arguments = If(stepItem.阶段 = 预设数据_v6.命令行阶段.FFprobe获取时长 OrElse 设置_v6.实例对象.覆盖参数传递 = "", stepItem.命令行, 设置_v6.实例对象.覆盖参数传递.Replace("<args>", stepItem.命令行))
        stepItem.实际执行文件名 = process.StartInfo.FileName
        stepItem.实际执行参数 = process.StartInfo.Arguments
        process.StartInfo.UseShellExecute = False
        process.StartInfo.RedirectStandardOutput = True
        process.StartInfo.RedirectStandardError = True
        process.StartInfo.StandardOutputEncoding = Encoding.UTF8
        process.StartInfo.StandardErrorEncoding = Encoding.UTF8
        process.StartInfo.RedirectStandardInput = True
        process.StartInfo.StandardInputEncoding = Encoding.UTF8
        process.StartInfo.CreateNoWindow = True
        Dim outputHandler As DataReceivedEventHandler = Sub(sender, e) 处理输出(stepItem, e.Data)
        Dim errorHandler As DataReceivedEventHandler = Sub(sender, e) 处理输出(stepItem, e.Data, False)
        AddHandler process.OutputDataReceived, outputHandler
        AddHandler process.ErrorDataReceived, errorHandler
        Dim processId As Integer = 0
        Try
            追加日志($"[3FUI] 执行：{process.StartInfo.FileName} {process.StartInfo.Arguments}", 编码任务日志类别_v6.系统, stepItem, False, False)
            ' Serialize launch and stop so a stop request cannot miss a not-yet-started process.
            SyncLock 状态锁
                If 手动停止 Then Return -1
                process.Start()
                当前进程 = process
                processId = process.Id
            End SyncLock
            process.BeginOutputReadLine()
            process.BeginErrorReadLine()
            编码队列_v6.通知任务更新(Me)
            If 设置_v6.实例对象.指定处理器核心 <> "" Then
                Try
                    process.ProcessorAffinity = GetAffinityMask(设置_v6.实例对象.指定处理器核心.Split(","c).Select(Function(s) s.Trim()).Where(Function(s) Integer.TryParse(s, Nothing)).Select(Function(s) Integer.Parse(s)).ToArray())
                Catch ex As InvalidOperationException
                    ' A short ffprobe step may already have exited.
                Catch ex As ComponentModel.Win32Exception
                    追加日志("[3FUI] 设置处理器核心失败：" & ex.Message, 编码任务日志类别_v6.系统, stepItem)
                End Try
            End If
            ' WaitForExitAsync also drains both redirected streams before completing.
            Await process.WaitForExitAsync().ConfigureAwait(False)
            Return process.ExitCode
        Finally
            SyncLock 状态锁
                If 当前进程 Is process Then 当前进程 = Nothing
            End SyncLock
            RemoveHandler process.OutputDataReceived, outputHandler
            RemoveHandler process.ErrorDataReceived, errorHandler
            Try
                If processId > 0 AndAlso Not process.HasExited Then process.Kill(True)
            Finally
                任务性能统计_v6.释放进程计数器(processId)
                process.Dispose()
            End Try
        End Try
    End Function

    Private Sub 处理输出(stepItem As 编码步骤_v6, line As String, Optional 是标准输出 As Boolean = True)
        If line Is Nothing Then Exit Sub
        SyncLock 输出锁
            处理输出核心(stepItem, line, 是标准输出)
        End SyncLock
    End Sub

    Private Sub 处理输出核心(stepItem As 编码步骤_v6, line As String, 是标准输出 As Boolean)
        If 是标准输出 OrElse stepItem.阶段 <> 预设数据_v6.命令行阶段.FFprobe获取时长 Then
            stepItem.输出缓存.Add(line)
            If stepItem.输出缓存.Count > 2000 Then stepItem.输出缓存.RemoveRange(0, stepItem.输出缓存.Count - 1000)
        End If
        If line.Contains("Duration:", StringComparison.OrdinalIgnoreCase) Then
            Dim detectedDuration = 编码进度_v6.提取媒体总时长(line)
            If String.IsNullOrWhiteSpace(媒体总时长) AndAlso detectedDuration > TimeSpan.Zero Then
                媒体总时长 = detectedDuration.TotalSeconds.ToString("0.###", CultureInfo.InvariantCulture)
            End If
            进度.解析FFmpeg输出(line, 计算当前总时长())
        End If
        Dim isProgressLine = 编码队列_v6.是否进度输出(line)
        If isProgressLine Then
            进度.解析FFmpeg输出(line, 计算当前总时长())
        End If
        Dim isError = 编码队列_v6.是否错误输出(line)
        Dim category = If(isError, 编码任务日志类别_v6.错误, If(isProgressLine, 编码任务日志类别_v6.进度, 编码任务日志类别_v6.输出))
        追加日志(line, category, stepItem, isError)
    End Sub

    Private Function 计算当前总时长() As TimeSpan
        Dim originalDuration = If(Not String.IsNullOrWhiteSpace(媒体总时长),
                                  编码进度_v6.转换时间(媒体总时长),
                                  进度.总时长)
        If 预设数据 Is Nothing OrElse 预设数据.剪辑区间_方法 = 预设数据_v6.剪辑方法.未知 Then Return originalDuration

        Dim inPointText = If(预设数据.剪辑区间_入点, "").Trim()
        Dim outPointText = If(预设数据.剪辑区间_出点, "").Trim()
        If inPointText = "" AndAlso outPointText = "" Then Return originalDuration

        Dim inPoint = 编码进度_v6.转换时间(inPointText)
        Dim outPoint = 编码进度_v6.转换时间(outPointText)
        If 预设数据.剪辑区间_方法 = 预设数据_v6.剪辑方法.掐头去尾 Then
            If originalDuration <= TimeSpan.Zero Then Return originalDuration
            If outPointText <> "" Then
                Return TimeSpan.FromSeconds(Math.Max(0, Math.Min(originalDuration.TotalSeconds, outPoint.TotalSeconds) - Math.Min(originalDuration.TotalSeconds, inPoint.TotalSeconds)))
            ElseIf inPointText <> "" Then
                Return TimeSpan.FromSeconds(Math.Max(0, originalDuration.TotalSeconds - Math.Min(originalDuration.TotalSeconds, inPoint.TotalSeconds)))
            End If
            Return originalDuration
        End If

        If 预设数据.剪辑区间_方法 = 预设数据_v6.剪辑方法.剔除中间 Then
            If originalDuration <= TimeSpan.Zero Then Return originalDuration
            If inPointText <> "" AndAlso outPointText <> "" Then
                Return TimeSpan.FromSeconds(Math.Max(0, originalDuration.TotalSeconds - Math.Max(0, outPoint.TotalSeconds - inPoint.TotalSeconds)))
            ElseIf inPointText <> "" Then
                Return TimeSpan.FromSeconds(Math.Min(originalDuration.TotalSeconds, Math.Max(0, inPoint.TotalSeconds)))
            ElseIf outPointText <> "" Then
                Return TimeSpan.FromSeconds(Math.Max(0, originalDuration.TotalSeconds - Math.Max(0, outPoint.TotalSeconds)))
            End If
        End If

        If outPointText <> "" Then
            Return TimeSpan.FromSeconds(Math.Max(0, outPoint.TotalSeconds - inPoint.TotalSeconds))
        End If
        If inPointText <> "" AndAlso originalDuration > TimeSpan.Zero Then
            Return TimeSpan.FromSeconds(Math.Max(0, originalDuration.TotalSeconds - inPoint.TotalSeconds))
        End If
        Return originalDuration
    End Function

    Public Sub 暂停()
        Try
            Dim changed As Boolean = False
            SyncLock 状态锁
                If 状态 = 编码任务状态_v6.正在处理 AndAlso 当前进程 IsNot Nothing AndAlso Not 当前进程.HasExited Then
                    If NtSuspendProcess(当前进程.Handle) = 0 Then
                        状态 = 编码任务状态_v6.已暂停
                        任务耗时计时器.Stop()
                        changed = True
                    End If
                End If
            End SyncLock
            If changed Then
                追加日志("[3FUI] 任务已暂停", 编码任务日志类别_v6.系统, 当前步骤, False, False)
                编码队列_v6.通知任务更新(Me)
                编码队列_v6.触发插件事件("task.paused", Me)
            End If
        Catch ex As Exception
            追加日志("[3FUI] 暂停失败：" & ex.Message, 编码任务日志类别_v6.错误, 当前步骤, True)
        End Try
    End Sub

    Public Sub 恢复()
        Try
            Dim changed As Boolean = False
            SyncLock 状态锁
                If 状态 = 编码任务状态_v6.已暂停 AndAlso 当前进程 IsNot Nothing AndAlso Not 当前进程.HasExited Then
                    If NtResumeProcess(当前进程.Handle) = 0 Then
                        状态 = 编码任务状态_v6.正在处理
                        任务耗时计时器.Start()
                        changed = True
                    End If
                End If
            End SyncLock
            If changed Then
                设定系统状态_v6()
                追加日志("[3FUI] 任务已恢复", 编码任务日志类别_v6.系统, 当前步骤, False, False)
                编码队列_v6.通知任务更新(Me)
                编码队列_v6.触发插件事件("task.resumed", Me)
            End If
        Catch ex As Exception
            追加日志("[3FUI] 恢复失败：" & ex.Message, 编码任务日志类别_v6.错误, 当前步骤, True)
        End Try
    End Sub

    Public Sub 停止()
        停止并报告是否停止执行()
    End Sub

    Friend Function 停止并报告是否停止执行() As Boolean
        Dim stoppedExecution As Boolean = False
        Try
            Dim process As Process = Nothing
            SyncLock 状态锁
                If Not 可停止 Then Return False
                stoppedExecution = 正在执行标记
                If stoppedExecution Then 当前执行已请求停止 = True
                If Not stoppedExecution Then 已实际开始标记 = False
                手动停止 = True
                状态 = 编码任务状态_v6.已停止
                process = 当前进程
            End SyncLock
            If process IsNot Nothing AndAlso Not process.HasExited Then process.Kill(True)
            任务耗时计时器.Stop()
            追加日志("[3FUI] 正在停止任务", 编码任务日志类别_v6.系统, 当前步骤, False, False)
            编码队列_v6.通知任务更新(Me)
            编码队列_v6.触发插件事件("task.stopped", Me)
            Return stoppedExecution
        Catch ex As Exception
            追加日志("[3FUI] 停止失败：" & ex.Message, 编码任务日志类别_v6.错误, 当前步骤, True)
            Return stoppedExecution
        End Try
    End Function

    Public Sub 重置(Optional 保留已分配输出 As Boolean = False)
        SyncLock 状态锁
            If Not 可重置 Then Exit Sub
            释放资源()
            If Not 保留已分配输出 Then
                If 输出文件由自动命名生成 Then 输出文件 = ""
                输出文件由自动命名生成 = False
            End If
            当前步骤索引 = -1
            手动停止 = False
            已实际开始标记 = False
            启动准备异常 = Nothing
            清空日志(False)
            步骤.Clear()
            进度 = New 编码进度_v6
            输入文件大小 = 0
            媒体总时长 = ""
            任务耗时计时器.Reset()
            状态 = 编码任务状态_v6.未处理
        End SyncLock
    End Sub

    Public Sub 释放资源()
        释放进程()
        清理帧服务器缓存()
    End Sub

    Private Sub 释放进程()
        Try
            If 当前进程 IsNot Nothing Then
                If Not 当前进程.HasExited Then 当前进程.Kill()
                当前进程.Dispose()
            End If
        Catch
        Finally
            当前进程 = Nothing
        End Try
    End Sub

    Private Sub 生成帧服务器脚本()
        If 预设数据 Is Nothing Then Exit Sub
        If 预设数据.视频参数_视频帧服务器_使用AviSynth Then
            Dim scriptPath = 解析帧服务器模板路径(预设数据.视频参数_视频帧服务器_avs脚本文件)
            If Not File.Exists(scriptPath) Then Throw New FileNotFoundException("AviSynth 脚本模板文件不存在", scriptPath)
            Dim content = File.ReadAllText(scriptPath).Replace("<FilePath>", 输入文件)
            AviSynthCachePath = 预设管理_v6.派生帧服务器脚本路径(输入文件, ".avs", ID)
            File.WriteAllText(AviSynthCachePath, content, New UTF8Encoding(False))
        End If
        If 预设数据.视频参数_视频帧服务器_使用VapourSynth Then
            Dim scriptPath = 解析帧服务器模板路径(预设数据.视频参数_视频帧服务器_vpy脚本文件)
            If Not File.Exists(scriptPath) Then Throw New FileNotFoundException("VapourSynth 脚本模板文件不存在", scriptPath)
            Dim content = File.ReadAllText(scriptPath).Replace("<FilePath>", 输入文件)
            VapourSynthCachePath = 预设管理_v6.派生帧服务器脚本路径(输入文件, Path.GetExtension(scriptPath), ID)
            File.WriteAllText(VapourSynthCachePath, content, New UTF8Encoding(False))
        End If
    End Sub

    Private Shared Function 解析帧服务器模板路径(pathText As String) As String
        Dim raw = If(pathText, "").Trim()
        If raw = "" Then Return raw
        Try
            If Path.IsPathRooted(raw) Then Return raw
            Return Path.GetFullPath(Path.Combine(Application.StartupPath, raw))
        Catch
            Return raw
        End Try
    End Function

    Private Sub 清理帧服务器缓存()
        Try
            If File.Exists(AviSynthCachePath) Then File.Delete(AviSynthCachePath)
            If File.Exists(VapourSynthCachePath) Then File.Delete(VapourSynthCachePath)
        Catch
        End Try
    End Sub

    Private Sub 完成后处理输出时间()
        If 预设数据 Is Nothing OrElse String.IsNullOrWhiteSpace(输出文件) Then Exit Sub
        Try
            If File.Exists(输出文件) AndAlso File.Exists(输入文件) Then
                If 预设数据.输出命名_保留创建时间 Then File.SetCreationTime(输出文件, File.GetCreationTime(输入文件))
                If 预设数据.输出命名_保留修改时间 Then File.SetLastWriteTime(输出文件, File.GetLastWriteTime(输入文件))
                If 预设数据.输出命名_保留访问时间 Then File.SetLastAccessTime(输出文件, File.GetLastAccessTime(输入文件))
            End If
        Catch ex As Exception
            追加日志("[3FUI] 保留文件时间失败：" & ex.Message, 编码任务日志类别_v6.输出, Nothing, False, False)
        End Try
    End Sub

    Private Sub 失败后清理输出()
        清理已报废MP4输出("失败")
    End Sub

    Private Sub 手动停止后清理输出()
        清理已报废MP4输出("手动停止")
    End Sub

    Private Sub 清理已报废MP4输出(触发原因 As String)
        If String.IsNullOrWhiteSpace(输出文件) OrElse Not File.Exists(输出文件) Then Exit Sub
        If Not Path.GetExtension(输出文件).Equals(".mp4", StringComparison.OrdinalIgnoreCase) Then Exit Sub
        If 输出文件是否等于输入文件() Then Exit Sub
        Try
            Select Case 设置_v6.实例对象.任务失败自动删除输出文件
                Case 0
                    FileIO.FileSystem.DeleteFile(输出文件, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin)
                    追加日志($"[3FUI] {触发原因}后已将报废 MP4 输出文件删除到回收站", 编码任务日志类别_v6.系统, 当前步骤, False, False)
                Case 1
                    FileIO.FileSystem.DeleteFile(输出文件, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)
                    追加日志($"[3FUI] {触发原因}后已永久删除报废 MP4 输出文件", 编码任务日志类别_v6.系统, 当前步骤, False, False)
            End Select
        Catch ex As Exception
            追加日志($"[3FUI] {触发原因}后删除报废 MP4 输出文件失败：" & ex.Message, 编码任务日志类别_v6.错误, 当前步骤, True, False)
        End Try
    End Sub

    Private Function 输出文件是否等于输入文件() As Boolean
        If String.IsNullOrWhiteSpace(输出文件) OrElse String.IsNullOrWhiteSpace(输入文件) Then Return False
        Try
            Return Path.GetFullPath(输出文件).Equals(Path.GetFullPath(输入文件), StringComparison.OrdinalIgnoreCase)
        Catch
            Return 输出文件.Equals(输入文件, StringComparison.OrdinalIgnoreCase)
        End Try
    End Function

    Private Shared Function 提取FFprobe时长(lines As IEnumerable(Of String)) As String
        For Each line In lines
            Dim v As Double
            If Double.TryParse(line.Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, v) AndAlso v > 0 Then Return v.ToString("0.###", CultureInfo.InvariantCulture)
        Next
        Return ""
    End Function

    Private Shared Function 获取阶段显示名称(stage As 预设数据_v6.命令行阶段) As String
        Select Case stage
            Case 预设数据_v6.命令行阶段.FFprobe获取时长 : Return "ffprobe 获取时长"
            Case 预设数据_v6.命令行阶段.二次编码第一遍 : Return "二次编码 1/2"
            Case 预设数据_v6.命令行阶段.二次编码第二遍 : Return "二次编码 2/2"
            Case Else : Return "编码"
        End Select
    End Function

    Private Shared Sub 设定系统状态_v6()
        Select Case 设置_v6.实例对象.有任务时系统保持状态选项
            Case 0
                SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS Or EXECUTION_STATE.ES_SYSTEM_REQUIRED)
            Case 1
                SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS Or EXECUTION_STATE.ES_SYSTEM_REQUIRED Or EXECUTION_STATE.ES_DISPLAY_REQUIRED)
            Case 2
                SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS)
        End Select
    End Sub

    Private Shared Sub 恢复系统状态_v6()
        SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS)
    End Sub
End Class

Public Class 编码步骤_v6
    Public Property 阶段 As 预设数据_v6.命令行阶段 = 预设数据_v6.命令行阶段.普通单次
    Public Property 显示名称 As String = ""
    Public Property 命令行 As String = ""
    Public Property 实际执行文件名 As String = ""
    Public Property 实际执行参数 As String = ""
    Public Property 滤镜图 As String = ""
    Public Property 映射参数 As String = ""
    Public Property 输出滤镜参数 As String = ""
    Public Property 需要媒体总时长 As Boolean = False
    Public Property 说明 As String = ""
    Public Property 状态 As 编码步骤状态_v6 = 编码步骤状态_v6.未处理
    Public Property 输出缓存 As New List(Of String)
End Class
