Imports System.IO
Imports System.Text
Imports System.Text.Json
Imports System.Threading

Partial Public Class 编码队列_v6
    Private Class 队列持久化文件_v6
        Public Property 版本 As Integer = 1
        Public Property 保存时间 As DateTime
        Public Property 任务 As New List(Of 编码任务快照_v6)
    End Class

    Private Class 编码任务快照_v6
        Public Property ID As String
        Public Property 任务名称 As String
        Public Property 输入文件 As String
        Public Property 输出文件 As String
        Public Property 输出文件由自动命名生成 As Boolean
        Public Property 预设数据 As 预设数据_v6
        Public Property 命令行 As String
        Public Property 状态 As 编码任务状态_v6
        Public Property 步骤 As List(Of 编码步骤快照_v6)
        Public Property 当前步骤索引 As Integer
        Public Property 允许自动启动 As Boolean
        Public Property 手动停止 As Boolean
        Public Property 进度 As 编码进度_v6
        Public Property 输入文件大小 As Long
        Public Property 媒体总时长 As String
        Public Property AviSynthCachePath As String
        Public Property VapourSynthCachePath As String
        Public Property 恢复进程ID As Integer
        Public Property 进程启动时间Utc As DateTime
        Public Property 已保存耗时 As TimeSpan
        Public Property 恢复输出大小KB As Long
    End Class

    Private Class 编码步骤快照_v6
        Public Property 阶段 As 预设数据_v6.命令行阶段
        Public Property 显示名称 As String
        Public Property 命令行 As String
        Public Property 实际执行文件名 As String
        Public Property 实际执行参数 As String
        Public Property 滤镜图 As String
        Public Property 映射参数 As String
        Public Property 输出滤镜参数 As String
        Public Property 需要媒体总时长 As Boolean
        Public Property 说明 As String
        Public Property 状态 As 编码步骤状态_v6
    End Class

    Private Shared ReadOnly 持久化路径 As String = Path.Combine(Application.StartupPath, "QueueTasks.json")
    Private Shared ReadOnly 持久化写入锁 As New Object
    Private Shared 持久化定时器 As System.Threading.Timer
    Private Shared 持久化待处理 As Integer
    Private Shared 正在恢复队列 As Boolean

    Public Shared Sub 请求保存队列()
        If 正在恢复队列 Then Return
        Interlocked.Exchange(持久化待处理, 1)
        SyncLock 持久化写入锁
            If 持久化定时器 Is Nothing Then
                持久化定时器 = New System.Threading.Timer(AddressOf 持久化计时到期, Nothing, 500, System.Threading.Timeout.Infinite)
            Else
                持久化定时器.Change(500, System.Threading.Timeout.Infinite)
            End If
        End SyncLock
    End Sub

    Public Shared Sub 立即保存队列()
        If 正在恢复队列 Then Return
        Interlocked.Exchange(持久化待处理, 1)
        保存队列快照()
    End Sub

    Private Shared Sub 持久化计时到期(state As Object)
        保存队列快照()
    End Sub

    Private Shared Sub 保存队列快照()
        SyncLock 持久化写入锁
            If Interlocked.Exchange(持久化待处理, 0) = 0 Then Return
            Dim snapshot As New 队列持久化文件_v6 With {.保存时间 = DateTime.UtcNow}
            SyncLock 队列锁
                For Each task In 队列项目
                    Dim pid = task.当前进程ID
                    snapshot.任务.Add(New 编码任务快照_v6 With {
                        .ID = task.ID, .任务名称 = task.任务名称, .输入文件 = task.输入文件, .输出文件 = task.输出文件,
                        .输出文件由自动命名生成 = task.输出文件由自动命名生成,
                        .预设数据 = If(task.预设数据 Is Nothing, Nothing, 克隆预设(task.预设数据)),
                        .命令行 = task.命令行, .状态 = task.状态,
                        .步骤 = task.步骤.Select(Function(stepItem) New 编码步骤快照_v6 With {
                            .阶段 = stepItem.阶段, .显示名称 = stepItem.显示名称, .命令行 = stepItem.命令行,
                            .实际执行文件名 = stepItem.实际执行文件名, .实际执行参数 = stepItem.实际执行参数,
                            .滤镜图 = stepItem.滤镜图, .映射参数 = stepItem.映射参数,
                            .输出滤镜参数 = stepItem.输出滤镜参数, .需要媒体总时长 = stepItem.需要媒体总时长,
                            .说明 = stepItem.说明, .状态 = stepItem.状态
                        }).ToList(),
                        .当前步骤索引 = task.当前步骤索引, .允许自动启动 = task.允许自动启动,
                        .手动停止 = task.手动停止, .进度 = JsonSerializer.Deserialize(Of 编码进度_v6)(JsonSerializer.Serialize(task.进度, JsonSO), JsonSO),
                        .输入文件大小 = task.输入文件大小, .媒体总时长 = task.媒体总时长,
                        .AviSynthCachePath = task.AviSynthCachePath, .VapourSynthCachePath = task.VapourSynthCachePath,
                        .恢复进程ID = pid, .进程启动时间Utc = task.进程启动时间Utc, .已保存耗时 = task.总耗时,
                        .恢复输出大小KB = If(task.进度 Is Nothing, task.恢复输出大小KB, task.进度.输出大小KB)
                    })
                Next
            End SyncLock
            If snapshot.任务.Count = 0 Then
                If File.Exists(持久化路径) Then File.Delete(持久化路径)
                Return
            End If
            原子文件写入_v6.写入文本(持久化路径, JsonSerializer.Serialize(snapshot, JsonSO))
        End SyncLock
    End Sub

    Public Shared Function 恢复持久化队列() As Integer
        If Not File.Exists(持久化路径) Then Return 迁移旧未处理任务缓存()
        Dim snapshot = JsonSerializer.Deserialize(Of 队列持久化文件_v6)(File.ReadAllText(持久化路径, Encoding.UTF8), JsonSO)
        If snapshot Is Nothing OrElse snapshot.版本 <> 1 OrElse snapshot.任务 Is Nothing Then
            Throw New InvalidDataException("队列持久化文件格式或版本不受支持")
        End If
        Dim restored As New List(Of 编码任务_v6)
        For Each item In snapshot.任务
            Dim task As New 编码任务_v6 With {
                .ID = item.ID, .任务名称 = If(item.任务名称, ""), .输入文件 = If(item.输入文件, ""), .输出文件 = If(item.输出文件, ""),
                .输出文件由自动命名生成 = item.输出文件由自动命名生成,
                .预设数据 = If(item.预设数据 Is Nothing, Nothing, 克隆预设(item.预设数据)),
                .命令行 = If(item.命令行, ""), .状态 = item.状态,
                .步骤 = item.步骤.Select(Function(stepItem) New 编码步骤_v6 With {
                    .阶段 = stepItem.阶段, .显示名称 = If(stepItem.显示名称, ""), .命令行 = If(stepItem.命令行, ""),
                    .实际执行文件名 = If(stepItem.实际执行文件名, ""), .实际执行参数 = If(stepItem.实际执行参数, ""),
                    .滤镜图 = If(stepItem.滤镜图, ""), .映射参数 = If(stepItem.映射参数, ""),
                    .输出滤镜参数 = If(stepItem.输出滤镜参数, ""), .需要媒体总时长 = stepItem.需要媒体总时长,
                    .说明 = If(stepItem.说明, ""), .状态 = stepItem.状态
                }).ToList(),
                .当前步骤索引 = item.当前步骤索引, .允许自动启动 = item.允许自动启动,
                .手动停止 = item.手动停止, .进度 = If(item.进度, New 编码进度_v6),
                .输入文件大小 = item.输入文件大小, .媒体总时长 = If(item.媒体总时长, ""),
                .AviSynthCachePath = If(item.AviSynthCachePath, ""), .VapourSynthCachePath = If(item.VapourSynthCachePath, ""),
                .恢复进程ID = item.恢复进程ID, .进程启动时间Utc = item.进程启动时间Utc, .已保存耗时 = item.已保存耗时,
                .恢复输出大小KB = item.恢复输出大小KB
            }
            If task.状态 = 编码任务状态_v6.正在处理 OrElse task.状态 = 编码任务状态_v6.已暂停 Then
                task.恢复进程仍在运行 = task.恢复进程ID > 0
                task.恢复进程仍在运行 = task.检查恢复进程()
                If Not task.恢复进程仍在运行 Then task.状态 = 编码任务状态_v6.已停止
            Else
                task.恢复进程ID = 0
            End If
            restored.Add(task)
        Next
        If restored.Count = 0 Then Return 0
        正在恢复队列 = True
        Try
            SyncLock 队列锁
                队列项目.AddRange(restored)
                For Each task In restored
                    任务索引.Add(task.ID, task)
                Next
            End SyncLock
            RaiseEvent 队列已变化()
        Finally
            正在恢复队列 = False
        End Try
        Dim count = restored.Count
        If restored.Any(Function(task) task.恢复进程仍在运行) Then
            Dim watcher As New System.Threading.Timer(Sub(state)
                                                          Dim changed As Boolean = False
                                                          For Each task In restored.Where(Function(candidate) candidate.恢复进程仍在运行)
                                                              If Not task.检查恢复进程() Then
                                                                  changed = True
                                                                  RaiseEvent 任务已更新(task)
                                                              End If
                                                          Next
                                                          If changed Then
                                                              立即保存队列()
                                                              请求自动调度()
                                                          End If
                                                          If Not restored.Any(Function(task) task.恢复进程仍在运行) Then DirectCast(state, System.Threading.Timer).Dispose()
                                                      End Sub, Nothing, 1000, 2000)
        End If
        删除未处理任务缓存()
        请求保存队列()
        Return count
    End Function

    Private Shared Function 迁移旧未处理任务缓存() As Integer
        Dim count = 加载未处理任务缓存()
        If count > 0 Then 立即保存队列()
        Return count
    End Function
End Class