Imports System.Globalization
Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions

Public Class 编码队列_v6

    Public Class 预设同步结果
        Public Property 已更新 As Integer
        Public Property 已跳过非预设任务 As Integer
        Public Property 已跳过不可修改任务 As Integer
    End Class

    Public Class 未处理任务缓存文件_v6
        Public Property 版本 As Integer = 1
        Public Property 保存时间 As DateTime = DateTime.Now
        Public Property 任务 As New List(Of 未处理任务缓存项_v6)
    End Class

    Public Class 未处理任务缓存项_v6
        Public Property 任务名称 As String = ""
        Public Property 输入文件 As String = ""
        Public Property 输出文件 As String = ""
        Public Property 输出文件由自动命名生成 As Boolean = False
        Public Property 预设数据 As 预设数据_v6
        Public Property 命令行 As String = ""
        Public Property 允许自动启动 As Boolean = False
    End Class

    Public Shared ReadOnly Property 队列 As New List(Of 编码任务_v6)
    Private Shared ReadOnly 队列锁 As New Object
    Private Shared 调度中 As Boolean = False
    Private Shared 自动调度已暂停 As Boolean = False
    Private Shared 完成提示待播放 As Boolean = False
    Private Shared 完成提示待调度结束检查 As Boolean = False
    Private Shared 全部任务已完成是否有错误 As Boolean = False
    Private Const 未处理任务缓存文件名 As String = "QueuePendingTasks_v6.json"
    Private Shared ReadOnly FFmpeg状态进度输出正则 As New Regex("^\s*(?:frame|size)\s*=\s*\S+.*\b(?:time|bitrate|speed)\s*=", RegexOptions.Compiled Or RegexOptions.IgnoreCase)
    Private Shared ReadOnly FFmpegProgress键值输出正则 As New Regex("^\s*(?:frame|fps|stream_\d+_\d+_q|bitrate|total_size|out_time(?:_ms|_us)?|dup_frames|drop_frames|speed|progress)\s*=", RegexOptions.Compiled Or RegexOptions.IgnoreCase)
    Private Shared ReadOnly 自动命名时间戳结尾正则 As New Regex("_[0-9]{4}\.[0-9]{2}\.[0-9]{2}-[0-9]{2}\.[0-9]{2}\.[0-9]{2}$", RegexOptions.Compiled Or RegexOptions.CultureInvariant)

    Public Shared Event 队列已变化()
    Public Shared Event 任务已更新(任务 As 编码任务_v6)

    Public Shared Property 错误输出匹配字符串列表 As New List(Of String) From {"Error", "Invalid", "cannot", "failed", "not supported", "require", "must be", "Could not", "is experimental", "if you want to use it", "Nothing was written", "Unable to choose"}
    Public Shared ReadOnly Property 华强买瓜宇宙任务名称列表 As New List(Of String) From {"有一个人前来买瓜", "哥们，这瓜多少钱一斤啊", "两块钱一斤", "这瓜皮子是金子做的还是瓜粒子是金子做的", "你瞧瞧现在这哪有瓜啊", "这都是大棚的瓜", "你嫌贵我还嫌贵呢", "你这瓜保熟嘛", "我开水果摊的能卖你生瓜蛋子", "我问你这瓜保熟嘛", "你是故意找茬是不是", "你要不要吧", "这瓜要熟我肯定要啊", "那它要是不熟怎么办啊", "要是不熟，我自己吃了它，满意了吧", "十五斤三十块", "你这哪够十五斤呐，你这秤有问题啊", "你TM故意找茬是不是，你到底要不要", "吸铁石", "这瓜要生的你自己吞进去啊", "你TM劈我瓜是吧", "萨日朗~"
    }

    Public Shared Function 是否错误输出(line As String) As Boolean
        If String.IsNullOrEmpty(line) Then Return False
        Return 错误输出匹配字符串列表.Any(Function(keyword) line.Contains(keyword, StringComparison.OrdinalIgnoreCase))
    End Function

    Public Shared Function 是否进度输出(line As String) As Boolean
        If String.IsNullOrWhiteSpace(line) Then Return False
        Return FFmpeg状态进度输出正则.IsMatch(line) OrElse FFmpegProgress键值输出正则.IsMatch(line)
    End Function

    Public Shared Function 应用任务名称混淆(原始任务名称 As String) As String
        Select Case 设置_v6.实例对象.混淆任务名称
            Case 1
                Return 混淆字符_喵(If(原始任务名称, ""))
            Case 2
                If 华强买瓜宇宙任务名称列表.Count = 0 Then Return 原始任务名称
                Return 华强买瓜宇宙任务名称列表(System.Random.Shared.Next(华强买瓜宇宙任务名称列表.Count))
            Case Else
                Return 原始任务名称
        End Select
    End Function

    Public Shared Function 获取队列快照() As List(Of 编码任务_v6)
        SyncLock 队列锁
            Return 队列.ToList()
        End SyncLock
    End Function

    Public Shared Function 根据ID获取任务(id As String) As 编码任务_v6
        If String.IsNullOrWhiteSpace(id) Then Return Nothing
        SyncLock 队列锁
            Return 队列.FirstOrDefault(Function(x) x.ID = id)
        End SyncLock
    End Function

    Public Shared Function 获取进行中任务数量() As Integer
        SyncLock 队列锁
            Return 队列.Where(Function(x) 是否进行中任务(x)).Count()
        End SyncLock
    End Function

    Public Shared Function 获取未处理任务数量() As Integer
        SyncLock 队列锁
            Return 队列.Where(Function(x) x.状态 = 编码任务状态_v6.未处理).Count()
        End SyncLock
    End Function

    Public Shared ReadOnly Property 未处理任务缓存文件路径 As String
        Get
            Return Path.Combine(Application.StartupPath, 未处理任务缓存文件名)
        End Get
    End Property

    Public Shared Function 存在未处理任务缓存() As Boolean
        Return File.Exists(未处理任务缓存文件路径)
    End Function

    Public Shared Function 读取未处理任务缓存任务数量() As Integer
        Try
            Dim cache = 读取未处理任务缓存文件()
            Return If(cache?.任务?.Count, 0)
        Catch
            Return 0
        End Try
    End Function

    Public Shared Function 保存未处理任务缓存() As Integer
        Dim cache As New 未处理任务缓存文件_v6 With {.保存时间 = DateTime.Now}
        Dim changed As New List(Of 编码任务_v6)

        SyncLock 队列锁
            For Each task In 队列
                If task.状态 <> 编码任务状态_v6.未处理 Then Continue For
                If task.预设数据 Is Nothing AndAlso String.IsNullOrWhiteSpace(task.命令行) Then Continue For

                If task.允许自动启动 Then
                    task.允许自动启动 = False
                    changed.Add(task)
                End If

                cache.任务.Add(New 未处理任务缓存项_v6 With {
                    .任务名称 = task.任务名称,
                    .输入文件 = task.输入文件,
                    .输出文件 = task.输出文件,
                    .输出文件由自动命名生成 = task.输出文件由自动命名生成,
                    .预设数据 = 克隆预设(task.预设数据),
                    .命令行 = task.命令行,
                    .允许自动启动 = False
                })
            Next
        End SyncLock

        广播任务更新(changed)

        If cache.任务.Count = 0 Then
            删除未处理任务缓存()
            Return 0
        End If

        Dim json = Text.Json.JsonSerializer.Serialize(cache, JsonSO)
        File.WriteAllText(未处理任务缓存文件路径, json, New UTF8Encoding(False))
        Return cache.任务.Count
    End Function

    Public Shared Function 加载未处理任务缓存() As Integer
        Dim cache = 读取未处理任务缓存文件()
        If cache Is Nothing OrElse cache.任务 Is Nothing OrElse cache.任务.Count = 0 Then
            删除未处理任务缓存()
            Return 0
        End If

        Dim restored As New List(Of 编码任务_v6)
        For Each item In cache.任务
            If item Is Nothing Then Continue For
            Dim hasPreset = item.预设数据 IsNot Nothing
            Dim hasCommand = Not String.IsNullOrWhiteSpace(item.命令行)
            If Not hasPreset AndAlso Not hasCommand Then Continue For

            Dim task As New 编码任务_v6 With {
                .任务名称 = If(item.任务名称, ""),
                .输入文件 = If(item.输入文件, ""),
                .输出文件 = If(item.输出文件, ""),
                .输出文件由自动命名生成 = item.输出文件由自动命名生成,
                .预设数据 = If(hasPreset, 克隆预设(item.预设数据), Nothing),
                .命令行 = If(item.命令行, ""),
                .状态 = 编码任务状态_v6.未处理,
                .允许自动启动 = False
            }
            restored.Add(task)
        Next

        If restored.Count > 0 Then
            SyncLock 队列锁
                队列.AddRange(restored)
            End SyncLock
            RaiseEvent 队列已变化()
        End If

        删除未处理任务缓存()
        Return restored.Count
    End Function

    Public Shared Sub 删除未处理任务缓存()
        Try
            If File.Exists(未处理任务缓存文件路径) Then File.Delete(未处理任务缓存文件路径)
        Catch
        End Try
    End Sub

    Private Shared Function 读取未处理任务缓存文件() As 未处理任务缓存文件_v6
        If Not File.Exists(未处理任务缓存文件路径) Then Return Nothing
        Dim json = File.ReadAllText(未处理任务缓存文件路径, Encoding.UTF8)
        Return Text.Json.JsonSerializer.Deserialize(Of 未处理任务缓存文件_v6)(json, JsonSO)
    End Function

    Public Shared Sub 停止所有进行中任务()
        Dim stopping As New List(Of 编码任务_v6)
        Dim changed As New List(Of 编码任务_v6)

        SyncLock 队列锁
            For Each task In 队列
                If task.状态 = 编码任务状态_v6.未处理 AndAlso task.允许自动启动 Then
                    task.允许自动启动 = False
                    changed.Add(task)
                ElseIf task.状态 = 编码任务状态_v6.正在处理 OrElse task.状态 = 编码任务状态_v6.已暂停 Then
                    stopping.Add(task)
                End If
            Next
        End SyncLock

        广播任务更新(changed)
        For Each task In stopping
            task.停止()
        Next
    End Sub

    Public Shared Function 添加预设任务(输入文件 As String, 预设数据 As 预设数据_v6, Optional 任务名称 As String = "", Optional 输出文件 As String = "") As 编码任务_v6
        Dim task As New 编码任务_v6 With {
            .输入文件 = 输入文件,
            .输出文件 = 输出文件,
            .任务名称 = 应用任务名称混淆(If(String.IsNullOrWhiteSpace(任务名称), Path.GetFileName(输入文件), 任务名称)),
            .预设数据 = 预设数据
        }
        SyncLock 队列锁
            队列.Add(task)
        End SyncLock
        RaiseEvent 队列已变化()
        请求调度()
        Return task
    End Function

    Public Shared Function 添加命令行任务(命令行 As String, 任务名称 As String, 输出文件 As String, Optional 输入文件 As String = "") As 编码任务_v6
        Dim task As New 编码任务_v6 With {
            .输入文件 = 输入文件,
            .输出文件 = 输出文件,
            .任务名称 = 应用任务名称混淆(If(String.IsNullOrWhiteSpace(任务名称), If(String.IsNullOrWhiteSpace(输入文件), "命令行任务", Path.GetFileName(输入文件)), 任务名称)),
            .命令行 = 命令行
        }
        SyncLock 队列锁
            队列.Add(task)
        End SyncLock
        RaiseEvent 队列已变化()
        请求调度()
        Return task
    End Function

    Public Shared Function 添加来自参数面板的文件(files As IEnumerable(Of String), 参数面板 As Form_v6_参数面板) As Integer
        If files Is Nothing OrElse 参数面板 Is Nothing Then Return 0
        Dim preset = 预设管理_v6.从面板创建预设(参数面板)
        Dim count As Integer = 0
        For Each file In files
            If String.IsNullOrWhiteSpace(file) Then Continue For
            If Directory.Exists(file) Then
                For Each child In Directory.GetFiles(file, "*", SearchOption.AllDirectories)
                    添加预设任务(If(设置_v6.实例对象.转译模式, 转译模式处理路径(child), child), 克隆预设(preset), Path.GetFileName(child))
                    count += 1
                Next
            Else
                添加预设任务(If(设置_v6.实例对象.转译模式, 转译模式处理路径(file), file), 克隆预设(preset), Path.GetFileName(file))
                count += 1
            End If
        Next
        Return count
    End Function

    Public Shared Function 同步未处理预设任务(预设数据 As 预设数据_v6) As 预设同步结果
        Dim result As New 预设同步结果
        If 预设数据 Is Nothing Then Return result

        Dim changed As New List(Of 编码任务_v6)
        SyncLock 队列锁
            For Each task In 队列
                If task.预设数据 Is Nothing Then
                    result.已跳过非预设任务 += 1
                    Continue For
                End If

                If task.状态 <> 编码任务状态_v6.未处理 Then
                    result.已跳过不可修改任务 += 1
                    Continue For
                End If

                task.预设数据 = 克隆预设(预设数据)
                If task.输出文件由自动命名生成 Then
                    task.输出文件 = ""
                    task.输出文件由自动命名生成 = False
                End If
                task.步骤.Clear()
                task.当前步骤索引 = -1
                task.媒体总时长 = ""
                task.AviSynthCachePath = ""
                task.VapourSynthCachePath = ""
                task.进度 = New 编码进度_v6
                changed.Add(task)
                result.已更新 += 1
            Next
        End SyncLock

        If changed.Count > 0 Then
            广播任务更新(changed)
            RaiseEvent 队列已变化()
        End If
        Return result
    End Function

    Public Shared Function 同步指定未处理预设任务(ids As IEnumerable(Of String), 预设数据 As 预设数据_v6) As 预设同步结果
        Dim result As New 预设同步结果
        If 预设数据 Is Nothing Then Return result

        Dim idSet As New HashSet(Of String)(If(ids, Array.Empty(Of String)()))
        If idSet.Count = 0 Then Return result

        Dim changed As New List(Of 编码任务_v6)
        SyncLock 队列锁
            For Each task In 队列
                If Not idSet.Contains(task.ID) Then Continue For

                If task.预设数据 Is Nothing Then
                    result.已跳过非预设任务 += 1
                    Continue For
                End If

                If task.状态 <> 编码任务状态_v6.未处理 Then
                    result.已跳过不可修改任务 += 1
                    Continue For
                End If

                task.预设数据 = 克隆预设(预设数据)
                If task.输出文件由自动命名生成 Then
                    task.输出文件 = ""
                    task.输出文件由自动命名生成 = False
                End If
                task.步骤.Clear()
                task.当前步骤索引 = -1
                task.媒体总时长 = ""
                task.AviSynthCachePath = ""
                task.VapourSynthCachePath = ""
                task.进度 = New 编码进度_v6
                changed.Add(task)
                result.已更新 += 1
            Next
        End SyncLock

        If changed.Count > 0 Then
            广播任务更新(changed)
            RaiseEvent 队列已变化()
        End If
        Return result
    End Function

    Public Shared Function 克隆预设(source As 预设数据_v6) As 预设数据_v6
        If source Is Nothing Then Return Nothing
        Dim json = Text.Json.JsonSerializer.Serialize(source, JsonSO)
        Dim result = Text.Json.JsonSerializer.Deserialize(Of 预设数据_v6)(json, JsonSO)
        预设管理_v6.初始化空集合(result)
        If result IsNot Nothing Then result.运行时使用输出位置 = source.运行时使用输出位置
        Return result
    End Function

    Public Shared Sub 开始任务(ids As IEnumerable(Of String))
        Dim idSet As New HashSet(Of String)(If(ids, Array.Empty(Of String)()))
        If idSet.Count = 0 Then Exit Sub

        Dim starting As New List(Of KeyValuePair(Of 编码任务_v6, Long))
        SyncLock 队列锁
            For Each task In 队列
                If idSet.Contains(task.ID) AndAlso task.状态 = 编码任务状态_v6.未处理 Then
                    Dim 执行标识 = task.开始执行()
                    If 执行标识 = 0 Then Continue For
                    task.状态 = 编码任务状态_v6.正在处理
                    为任务保留输出文件(task)
                    标记任务已实际启动()
                    starting.Add(New KeyValuePair(Of 编码任务_v6, Long)(task, 执行标识))
                End If
            Next
            If starting.Count > 0 Then 自动调度已暂停 = False
        End SyncLock

        广播任务更新(starting.Select(Function(x) x.Key))
        For Each item In starting
            异步执行任务(item.Key, item.Value)
        Next
        If starting.Count > 0 Then 请求调度()
    End Sub

    Public Shared Sub 暂停任务(ids As IEnumerable(Of String))
        For Each task In 获取指定任务(ids)
            task.暂停()
        Next
    End Sub

    Public Shared Sub 恢复任务(ids As IEnumerable(Of String))
        For Each task In 获取指定任务(ids)
            task.恢复()
        Next
    End Sub

    Public Shared Sub 停止任务(ids As IEnumerable(Of String))
        Dim stopping = 获取指定任务(ids).Where(Function(task) task.可停止).ToList()
        If stopping.Count = 0 Then Exit Sub

        SyncLock 队列锁
            自动调度已暂停 = True
        End SyncLock
        For Each task In stopping
            task.停止()
        Next
    End Sub

    Public Shared Sub 取消自动开始任务(ids As IEnumerable(Of String))
        Dim changed As New List(Of 编码任务_v6)
        For Each task In 获取指定任务(ids)
            If task.状态 = 编码任务状态_v6.未处理 AndAlso task.允许自动启动 Then
                task.允许自动启动 = False
                changed.Add(task)
            End If
        Next
        广播任务更新(changed)
    End Sub

    Public Shared Sub 重置任务(ids As IEnumerable(Of String))
        Dim changed As New List(Of 编码任务_v6)
        For Each task In 获取指定任务(ids)
            If task.可重置 Then
                task.重置()
                changed.Add(task)
            End If
        Next
        广播任务更新(changed)
    End Sub

    Public Shared Sub 移除任务(ids As IEnumerable(Of String))
        Dim idSet As New HashSet(Of String)(If(ids, Array.Empty(Of String)()))
        If idSet.Count = 0 Then Exit Sub
        Dim removed As New List(Of 编码任务_v6)
        SyncLock 队列锁
            For i = 队列.Count - 1 To 0 Step -1
                If idSet.Contains(队列(i).ID) AndAlso 队列(i).可移除 Then
                    removed.Add(队列(i))
                    队列.RemoveAt(i)
                End If
            Next
        End SyncLock
        For Each task In removed
            task.释放资源()
        Next
        If removed.Count > 0 Then RaiseEvent 队列已变化()
        请求调度()
    End Sub

    Public Shared Sub 全选错误任务()
        RaiseEvent 队列已变化()
    End Sub

    Public Shared Sub 刷新显示()
        RaiseEvent 队列已变化()
    End Sub

    Public Shared Sub 应用自动开始任务设置(自动开始 As Boolean)
        Dim changed As New List(Of 编码任务_v6)
        SyncLock 队列锁
            For Each task In 队列
                If task.状态 = 编码任务状态_v6.未处理 AndAlso task.允许自动启动 <> 自动开始 Then
                    task.允许自动启动 = 自动开始
                    changed.Add(task)
                End If
            Next
        End SyncLock

        广播任务更新(changed)
        If 自动开始 Then
            SyncLock 队列锁
                自动调度已暂停 = False
            End SyncLock
            请求调度()
        End If
    End Sub

    Public Shared Function 重新排序(idsInOrder As IEnumerable(Of String)) As Boolean
        Dim ids = If(idsInOrder, Array.Empty(Of String)()).Where(Function(x) Not String.IsNullOrWhiteSpace(x)).ToList()
        If ids.Count = 0 Then Return False
        SyncLock 队列锁
            If ids.Count <> 队列.Count Then Return False
            For i = 0 To 队列.Count - 1
                If Not 队列(i).可排序 AndAlso ids(i) <> 队列(i).ID Then Return False
            Next
            Dim map = 队列.ToDictionary(Function(x) x.ID)
            If ids.Any(Function(id) Not map.ContainsKey(id)) Then Return False
            队列.Clear()
            队列.AddRange(ids.Select(Function(id) map(id)))
        End SyncLock
        RaiseEvent 队列已变化()
        请求调度()
        Return True
    End Function

    Public Shared Sub 上移任务(ids As IEnumerable(Of String))
        移动任务(ids, -1)
    End Sub

    Public Shared Sub 下移任务(ids As IEnumerable(Of String))
        移动任务(ids, 1)
    End Sub

    Private Shared Sub 移动任务(ids As IEnumerable(Of String), direction As Integer)
        Dim idSet As New HashSet(Of String)(If(ids, Array.Empty(Of String)()))
        If idSet.Count = 0 Then Exit Sub
        SyncLock 队列锁
            If 队列.Any(Function(x) idSet.Contains(x.ID) AndAlso Not x.可排序) Then Exit Sub
            If direction < 0 Then
                For i = 1 To 队列.Count - 1
                    If idSet.Contains(队列(i).ID) AndAlso Not idSet.Contains(队列(i - 1).ID) AndAlso 队列(i - 1).可排序 Then
                        Dim temp = 队列(i - 1)
                        队列(i - 1) = 队列(i)
                        队列(i) = temp
                    End If
                Next
            Else
                For i = 队列.Count - 2 To 0 Step -1
                    If idSet.Contains(队列(i).ID) AndAlso Not idSet.Contains(队列(i + 1).ID) AndAlso 队列(i + 1).可排序 Then
                        Dim temp = 队列(i + 1)
                        队列(i + 1) = 队列(i)
                        队列(i) = temp
                    End If
                Next
            End If
        End SyncLock
        RaiseEvent 队列已变化()
    End Sub

    Public Shared Sub 请求调度(Optional 允许完成提示检查 As Boolean = False)
        SyncLock 队列锁
            If 允许完成提示检查 Then 完成提示待调度结束检查 = True
            If 自动调度已暂停 Then Exit Sub
            If 调度中 Then Exit Sub
            调度中 = True
        End SyncLock
        Task.Run(Sub()
                     Try
                         调度循环()
                     Finally
                         Dim shouldRunAgain As Boolean
                         Dim shouldCheckCompletion As Boolean
                         SyncLock 队列锁
                             调度中 = False
                             Dim running = 队列.Where(Function(x) 是否进行中任务(x) OrElse x.正在执行).Count()
                             shouldRunAgain = Not 自动调度已暂停 AndAlso running < 获取并发上限() AndAlso 队列.Any(Function(x) x.状态 = 编码任务状态_v6.未处理 AndAlso x.允许自动启动 AndAlso Not x.正在执行)
                             shouldCheckCompletion = 完成提示待调度结束检查
                             If Not shouldRunAgain Then 完成提示待调度结束检查 = False
                         End SyncLock
                         If shouldRunAgain Then
                             请求调度(shouldCheckCompletion)
                         ElseIf shouldCheckCompletion Then
                             检查全部完成提示()
                         End If
                     End Try
                 End Sub)
    End Sub

    Private Shared Sub 调度循环()
        Do
            Dim nextTask As 编码任务_v6 = Nothing
            Dim 执行标识 As Long = 0
            SyncLock 队列锁
                If 自动调度已暂停 Then Exit Do
                Dim running = 队列.Where(Function(x) 是否进行中任务(x) OrElse x.正在执行).Count()
                If running >= 获取并发上限() Then Exit Do
                nextTask = 队列.FirstOrDefault(Function(x) x.状态 = 编码任务状态_v6.未处理 AndAlso x.允许自动启动 AndAlso Not x.正在执行)
                If nextTask Is Nothing Then Exit Do
                执行标识 = nextTask.开始执行()
                If 执行标识 = 0 Then Continue Do
                nextTask.状态 = 编码任务状态_v6.正在处理
                为任务保留输出文件(nextTask)
                标记任务已实际启动()
            End SyncLock
            RaiseEvent 任务已更新(nextTask)
            异步执行任务(nextTask, 执行标识)
        Loop
    End Sub

    Private Shared Sub 异步执行任务(task As 编码任务_v6, 执行标识 As Long)
        Threading.Tasks.Task.Run(Async Function()
                     Try
                         Await task.开始Async(执行标识)
                     Finally
                         task.结束执行()
                         任务执行结束后处理(task)
                     End Try
                 End Function)
    End Sub

    Private Shared Function 获取并发上限() As Integer
        Dim n = 设置_v6.实例对象.自动同时运行任务数量选项 + 1
        Return Math.Min(Math.Max(n, 1), 10)
    End Function

    Private Shared Function 获取指定任务(ids As IEnumerable(Of String)) As List(Of 编码任务_v6)
        Dim idSet As New HashSet(Of String)(If(ids, Array.Empty(Of String)()))
        If idSet.Count = 0 Then Return New List(Of 编码任务_v6)
        SyncLock 队列锁
            Return 队列.Where(Function(x) idSet.Contains(x.ID)).ToList()
        End SyncLock
    End Function

    Private Shared Function 是否进行中任务(task As 编码任务_v6) As Boolean
        If task Is Nothing Then Return False
        Return task.状态 = 编码任务状态_v6.正在处理 OrElse
               task.状态 = 编码任务状态_v6.已暂停
    End Function

    Private Shared Sub 广播任务更新(tasks As IEnumerable(Of 编码任务_v6))
        If tasks Is Nothing Then Exit Sub
        For Each task In tasks
            RaiseEvent 任务已更新(task)
        Next
    End Sub

    Friend Shared Sub 通知任务更新(task As 编码任务_v6)
        RaiseEvent 任务已更新(task)
    End Sub

    Friend Shared Sub 标记任务出错()
        全部任务已完成是否有错误 = True
    End Sub

    Private Shared Sub 标记任务已实际启动()
        If Not 完成提示待播放 Then 全部任务已完成是否有错误 = False
        完成提示待播放 = True
    End Sub

    Private Shared Sub 任务执行结束后处理(task As 编码任务_v6)
        请求调度(True)
    End Sub

    Private Shared Sub 检查全部完成提示()
        Dim shouldPlay As Boolean = False
        Dim hasError As Boolean = False
        SyncLock 队列锁
            If Not 完成提示待播放 Then Exit Sub
            If 队列.Count = 0 Then
                完成提示待播放 = False
                全部任务已完成是否有错误 = False
                Exit Sub
            End If
            If 队列.Any(Function(x) x.状态 = 编码任务状态_v6.未处理 OrElse x.状态 = 编码任务状态_v6.正在处理 OrElse x.状态 = 编码任务状态_v6.已暂停) Then Exit Sub
            shouldPlay = True
            hasError = 全部任务已完成是否有错误 OrElse 队列.Any(Function(x) x.状态 = 编码任务状态_v6.错误)
            完成提示待播放 = False
            全部任务已完成是否有错误 = False
        End SyncLock
        If shouldPlay AndAlso 设置_v6.实例对象.提示音选项 = 0 Then
            Try
                If hasError Then
                    Sound_Error.Position = 0
                    My.Computer.Audio.Play(Sound_Error, AudioPlayMode.Background)
                Else
                    Sound_Finish.Position = 0
                    My.Computer.Audio.Play(Sound_Finish, AudioPlayMode.Background)
                End If
            Catch
            End Try
        End If
    End Sub

    Public Shared Function 计算输出位置_v6(输入文件 As String,
                                        预设数据 As 预设数据_v6,
                                        Optional 创建输出目录 As Boolean = False,
                                        Optional 已保留输出路径 As ISet(Of String) = Nothing) As String
        If 预设数据 Is Nothing Then Return ""
        If 预设数据.输出_输出文件参数使用方法 <> 预设数据_v6.输出文件参数使用方法.正常使用 Then Return ""

        Dim 容器 = 规范化输出容器(预设数据.输出容器)
        If 容器 = "" Then Return ""

        Dim 输入目录 = 获取输入目录(输入文件)
        Dim 输出目录 = 计算输出目录(输入目录, 预设数据, 创建输出目录)
        Dim 文件名 = 生成输出文件名(输入文件, 预设数据)
        Dim 输出路径 = Path.Combine(输出目录, 文件名 & 容器)
        输出路径 = 应用存在检测命名选项(输出路径, 预设数据.输出_自动命名选项, 已保留输出路径)

        If 设置_v6.实例对象.转译模式 Then Return 转译模式处理路径(输出路径)
        Return 输出路径
    End Function

    Public Shared Function 获取任务实际命令行文本(task As 编码任务_v6) As String
        If task Is Nothing Then Return ""
        Dim lines As New List(Of String)
        If task.步骤.Count > 0 Then
            lines.AddRange(task.步骤.Select(Function(x) $"{预设管理_v6.获取命令行进程名(x.阶段)} {x.命令行}"))
        ElseIf task.预设数据 IsNot Nothing Then
            Dim output = If(task.输出文件 <> "", task.输出文件, 计算输出位置_v6(task.输入文件, task.预设数据))
            lines.AddRange(预设管理_v6.生成阶段化命令行(task.预设数据, task.输入文件, output, 帧服务器脚本后缀:=task.ID).
                Select(Function(x) $"{预设管理_v6.获取命令行进程名(x.阶段)} {x.命令行}"))
        ElseIf task.命令行 <> "" Then
            lines.Add($"{预设管理_v6.获取命令行进程名(预设数据_v6.命令行阶段.普通单次)} {task.命令行}")
        End If
        Return String.Join(vbCrLf, lines)
    End Function

    Public Shared Function 获取任务执行命令行文本(task As 编码任务_v6) As String
        If task Is Nothing Then Return ""
        Dim lines As New List(Of String)
        If task.步骤.Count > 0 Then
            lines.AddRange(task.步骤.Select(Function(x) 获取步骤执行命令行(x)))
        ElseIf task.预设数据 IsNot Nothing Then
            Dim output = If(task.输出文件 <> "", task.输出文件, 计算输出位置_v6(task.输入文件, task.预设数据))
            lines.AddRange(预设管理_v6.生成阶段化命令行(task.预设数据, task.输入文件, output, 帧服务器脚本后缀:=task.ID).
                Select(Function(x) 格式化实际执行命令行(x.阶段, x.命令行)))
        ElseIf task.命令行 <> "" Then
            lines.Add(格式化实际执行命令行(预设数据_v6.命令行阶段.普通单次, task.命令行))
        End If
        Return String.Join(vbCrLf, lines)
    End Function

    Private Shared Function 格式化实际执行命令行(stage As 预设数据_v6.命令行阶段, arguments As String) As String
        Dim processName = If(stage = 预设数据_v6.命令行阶段.FFprobe获取时长, "ffprobe", If(设置_v6.实例对象.替代进程文件名 <> "", 设置_v6.实例对象.替代进程文件名, "ffmpeg"))
        Dim actualArgs = If(stage = 预设数据_v6.命令行阶段.FFprobe获取时长 OrElse 设置_v6.实例对象.覆盖参数传递 = "", arguments, 设置_v6.实例对象.覆盖参数传递.Replace("<args>", arguments))
        Return 格式化进程文件名(processName) & If(String.IsNullOrWhiteSpace(actualArgs), "", " " & actualArgs)
    End Function

    Private Shared Function 获取步骤执行命令行(stepItem As 编码步骤_v6) As String
        If stepItem Is Nothing Then Return ""
        If Not String.IsNullOrWhiteSpace(stepItem.实际执行文件名) Then
            Return 格式化进程文件名(stepItem.实际执行文件名) & If(String.IsNullOrWhiteSpace(stepItem.实际执行参数), "", " " & stepItem.实际执行参数)
        End If
        Return 格式化实际执行命令行(stepItem.阶段, stepItem.命令行)
    End Function

    Private Shared Function 格式化进程文件名(value As String) As String
        Dim processName = If(value, "").Trim()
        If processName = "" Then processName = "ffmpeg"
        If processName.Any(Function(c) Char.IsWhiteSpace(c)) AndAlso Not (processName.StartsWith("""c", StringComparison.Ordinal) AndAlso processName.EndsWith("""c", StringComparison.Ordinal)) Then
            Return """" & processName & """"
        End If
        Return processName
    End Function

    Private Shared Function 规范化输出容器(value As String) As String
        Dim 容器 = If(value, "").Trim()
        If 容器 = "" Then Return ""
        If Not 容器.StartsWith("."c) Then 容器 = "." & 容器
        Return 容器
    End Function

    Private Shared Function 获取输入目录(输入文件 As String) As String
        Dim 输入目录 = Path.GetDirectoryName(输入文件)
        Return If(String.IsNullOrWhiteSpace(输入目录), Environment.CurrentDirectory, 输入目录)
    End Function

    Private Shared Function 计算输出目录(输入目录 As String, 预设数据 As 预设数据_v6, 创建输出目录 As Boolean) As String
        Dim 输出目录 = 输入目录
        Dim 使用自定义输出目录 = 选择自定义输出目录(预设数据, 输出目录)
        If Not 使用自定义输出目录 Then Return 输出目录

        Dim 保留子目录 = 计算保留子文件夹结构相对目录(输入目录, 预设数据.输出位置_保留子文件夹结构起始点)
        If 保留子目录 <> "" Then 输出目录 = Path.Combine(输出目录, 保留子目录)
        If 创建输出目录 Then Directory.CreateDirectory(输出目录)
        Return 输出目录
    End Function

    Private Shared Function 选择自定义输出目录(预设数据 As 预设数据_v6, ByRef 输出目录 As String) As Boolean
        If 预设数据 Is Nothing Then Return False
        If Not (预设管理_v6.可使用预设输出位置(预设数据) OrElse 可使用运行时输出位置(预设数据)) Then Return False

        输出目录 = 预设数据.输出位置
        Return True
    End Function

    Private Shared Function 可使用运行时输出位置(预设数据 As 预设数据_v6) As Boolean
        Return 预设数据 IsNot Nothing AndAlso
               预设数据.运行时使用输出位置 AndAlso
               Directory.Exists(If(预设数据.输出位置, "").Trim())
    End Function

    Private Shared Function 生成输出文件名(输入文件 As String, 预设数据 As 预设数据_v6) As String
        Dim 原名 = Path.GetFileNameWithoutExtension(输入文件)
        If String.IsNullOrWhiteSpace(原名) Then 原名 = "output"

        Dim 文件名 = If(预设数据.输出命名_开头文本, "")
        文件名 &= If(预设数据.输出命名_替代文本 Is Nothing OrElse 预设数据.输出命名_替代文本 = "", 原名, 预设数据.输出命名_替代文本)
        文件名 &= If(预设数据.输出命名_结尾文本, "")
        文件名 = 应用非冲突检测命名选项(文件名, 预设数据.输出_自动命名选项, 预设数据)
        文件名 = 清理文件名(文件名)
        Return If(文件名 = "", "output", 文件名)
    End Function

    Private Shared Function 应用非冲突检测命名选项(文件名 As String,
                                           optionValue As 预设数据_v6.自动命名选项,
                                           预设数据 As 预设数据_v6) As String
        If optionValue = 预设数据_v6.自动命名选项.附加_递增时间戳 Then Return 应用自动命名时间戳(文件名)
        If 使用补零结尾序号(optionValue) Then Return 文件名
        Return 文件名 & 生成自动命名后缀(预设数据)
    End Function

    Private Shared Function 应用存在检测命名选项(输出路径 As String,
                                            optionValue As 预设数据_v6.自动命名选项,
                                            Optional 已保留输出路径 As ISet(Of String) = Nothing) As String
        Select Case optionValue
            Case 预设数据_v6.自动命名选项.附加_递增数字
                Return 获取递增输出路径(输出路径, 已保留输出路径)
            Case 预设数据_v6.自动命名选项.附加_2位结尾序号
                Return 获取补零结尾序号输出路径(输出路径, 2, 已保留输出路径)
            Case 预设数据_v6.自动命名选项.附加_3位结尾序号
                Return 获取补零结尾序号输出路径(输出路径, 3, 已保留输出路径)
            Case Else
                Return 输出路径
        End Select
    End Function

    Private Shared Function 计算保留子文件夹结构相对目录(输入目录 As String, 起始点 As String) As String
        Dim root = 规范化文件夹路径(If(起始点, ""))
        If root = "" OrElse String.IsNullOrWhiteSpace(输入目录) Then Return ""

        Dim rel = 计算本机相对目录(输入目录, root)
        If rel <> "" Then Return rel

        If 设置_v6.实例对象.转译模式 Then Return 计算转译相对目录(输入目录, root)
        Return ""
    End Function

    Private Shared Function 计算本机相对目录(输入目录 As String, 起始点 As String) As String
        Try
            Dim inputFull = 规范化比较目录(输入目录)
            Dim rootFull = 规范化比较目录(起始点)
            If inputFull = "" OrElse rootFull = "" Then Return ""
            If Not 路径位于目录内(inputFull, rootFull) Then Return ""
            Return 清理相对子目录(Path.GetRelativePath(rootFull, inputFull))
        Catch
            Return ""
        End Try
    End Function

    Private Shared Function 计算转译相对目录(输入目录 As String, 起始点 As String) As String
        Try
            Dim inputVirtual = 规范化转译比较目录(输入目录)
            Dim rootVirtual = 规范化转译比较目录(转译模式处理路径(起始点))
            If inputVirtual = "" OrElse rootVirtual = "" Then Return ""
            If String.Equals(inputVirtual, rootVirtual, StringComparison.OrdinalIgnoreCase) Then Return ""
            Dim rootPrefix = If(rootVirtual.EndsWith("/"c), rootVirtual, rootVirtual & "/")
            If Not inputVirtual.StartsWith(rootPrefix, StringComparison.OrdinalIgnoreCase) Then Return ""
            Return 清理相对子目录(inputVirtual.Substring(rootPrefix.Length).Replace("/"c, Path.DirectorySeparatorChar))
        Catch
            Return ""
        End Try
    End Function

    Private Shared Function 规范化比较目录(pathText As String) As String
        Dim value = If(pathText, "").Trim()
        If value = "" Then Return ""
        Return 规范化文件夹路径(Path.GetFullPath(value))
    End Function

    Private Shared Function 规范化转译比较目录(pathText As String) As String
        Dim value = If(pathText, "").Trim().Replace("\"c, "/"c)
        If value = "" Then Return ""
        If Not value.StartsWith("/"c) Then value = "/" & value
        While value.Contains("//")
            value = value.Replace("//", "/")
        End While
        Return value.TrimEnd("/"c)
    End Function

    Private Shared Function 路径位于目录内(pathText As String, rootText As String) As Boolean
        If String.Equals(pathText, rootText, StringComparison.OrdinalIgnoreCase) Then Return True
        Dim rootPrefix = If(rootText.EndsWith("\"c) OrElse rootText.EndsWith("/"c), rootText, rootText & Path.DirectorySeparatorChar)
        Return pathText.StartsWith(rootPrefix, StringComparison.OrdinalIgnoreCase)
    End Function

    Private Shared Function 清理相对子目录(relativePath As String) As String
        Dim rel = If(relativePath, "").Trim()
        If rel = "" OrElse rel = "." Then Return ""
        rel = rel.Replace("/"c, Path.DirectorySeparatorChar).Replace("\"c, Path.DirectorySeparatorChar)
        If Path.IsPathRooted(rel) Then Return ""
        If rel = ".." OrElse rel.StartsWith(".." & Path.DirectorySeparatorChar, StringComparison.Ordinal) Then Return ""
        Return rel.Trim(Path.DirectorySeparatorChar)
    End Function

    Private Shared Function 生成自动命名后缀(preset As 预设数据_v6) As String
        Select Case preset.输出_自动命名选项
            Case 预设数据_v6.自动命名选项.不使用自动命名
                Return ""
            Case 预设数据_v6.自动命名选项.附加_递增时间戳
                Return $"_{Now:yyyy.MM.dd-HH.mm.ss}"
            Case 预设数据_v6.自动命名选项.附加_递增数字
                Return "~1"
            Case 预设数据_v6.自动命名选项.附加_3FUI
                Return "_3fui"
            Case 预设数据_v6.自动命名选项.常规压片_附加编码器和质量参数
                Dim parts As New List(Of String)
                If preset.视频参数_编码器_具体编码 <> "" Then parts.Add(preset.视频参数_编码器_具体编码)
                If preset.视频参数_编码器_编码预设 <> "" Then parts.Add(preset.视频参数_编码器_编码预设)
                If preset.视频参数_质量控制_参数名 <> "" AndAlso preset.视频参数_质量控制_值 <> "" Then parts.Add(preset.视频参数_质量控制_参数名.TrimStart("-"c) & preset.视频参数_质量控制_值)
                If preset.视频参数_比特率_基础 <> "" Then parts.Add(preset.视频参数_比特率_基础)
                If preset.视频参数_比特率_最低值 <> "" Then parts.Add("L" & preset.视频参数_比特率_最低值)
                If preset.视频参数_比特率_最高值 <> "" Then parts.Add("H" & preset.视频参数_比特率_最高值)
                If preset.视频参数_比特率_缓冲区 <> "" Then parts.Add("BF" & preset.视频参数_比特率_缓冲区)
                Return If(parts.Count = 0, "", "." & String.Join(".", parts))
            Case 预设数据_v6.自动命名选项.附加_随机8位数字
                Return "_" & 随机字符串生成(8, True, False, False)
            Case 预设数据_v6.自动命名选项.附加_随机8位字母
                Return "_" & 随机字符串生成(8, False, True, True)
            Case 预设数据_v6.自动命名选项.附加_随机8位数字和字母组合
                Return "_" & 随机字符串生成(8, True, True, True)
            Case 预设数据_v6.自动命名选项.附加_随机16位数字
                Return "_" & 随机字符串生成(16, True, False, False)
            Case 预设数据_v6.自动命名选项.附加_随机16位字母
                Return "_" & 随机字符串生成(16, False, True, True)
            Case 预设数据_v6.自动命名选项.附加_随机16位数字和字母组合
                Return "_" & 随机字符串生成(16, True, True, True)
            Case Else
                Return ""
        End Select
    End Function

    Private Shared Function 使用补零结尾序号(optionValue As 预设数据_v6.自动命名选项) As Boolean
        Return optionValue = 预设数据_v6.自动命名选项.附加_2位结尾序号 OrElse
               optionValue = 预设数据_v6.自动命名选项.附加_3位结尾序号
    End Function

    Private Shared Function 应用自动命名时间戳(文件名 As String) As String
        Dim value = If(文件名, "")
        Dim timeStamp = $"_{Now:yyyy.MM.dd-HH.mm.ss}"
        If 自动命名时间戳结尾正则.IsMatch(value) Then Return 自动命名时间戳结尾正则.Replace(value, timeStamp)
        Return value & timeStamp
    End Function

    Private Shared Function 清理文件名(value As String) As String
        Dim result = If(value, "")
        For Each c In Path.GetInvalidFileNameChars()
            result = result.Replace(c, "_"c)
        Next
        Return result
    End Function

    Private Shared Function 获取递增输出路径(输出路径 As String, 已保留输出路径 As ISet(Of String)) As String
        If String.IsNullOrWhiteSpace(输出路径) Then Return 输出路径
        If 输出路径可用(输出路径, 已保留输出路径) Then Return 输出路径
        Dim dir = IO.Path.GetDirectoryName(输出路径)
        Dim name = IO.Path.GetFileNameWithoutExtension(输出路径)
        Dim ext = IO.Path.GetExtension(输出路径)
        Dim baseName = Regex.Replace(name, "~\d+$", "")
        For i = 1 To 99999
            Dim candidate = IO.Path.Combine(dir, $"{baseName}~{i}{ext}")
            If 输出路径可用(candidate, 已保留输出路径) Then Return candidate
        Next
        Return IO.Path.Combine(dir, $"{baseName}~{Now:yyyyMMddHHmmss}{ext}")
    End Function

    Private Shared Function 获取补零结尾序号输出路径(输出路径 As String,
                                                位数 As Integer,
                                                已保留输出路径 As ISet(Of String)) As String
        If String.IsNullOrWhiteSpace(输出路径) Then Return 输出路径
        Dim dir = If(IO.Path.GetDirectoryName(输出路径), "")
        Dim name = IO.Path.GetFileNameWithoutExtension(输出路径)
        Dim ext = IO.Path.GetExtension(输出路径)
        Dim width = Math.Max(1, 位数)
        Dim baseName = name
        Dim startNumber As Long = 1
        Dim numberMatch = Regex.Match(name, "\d+$", RegexOptions.CultureInvariant)

        If numberMatch.Success Then
            baseName = name.Substring(0, name.Length - numberMatch.Value.Length)
            width = Math.Max(width, numberMatch.Value.Length)

            Dim currentNumber As Long
            If Long.TryParse(numberMatch.Value, NumberStyles.None, CultureInfo.InvariantCulture, currentNumber) AndAlso currentNumber < Long.MaxValue Then
                startNumber = currentNumber + 1
            End If
        End If

        For offset As Long = 0 To 99999
            Dim serial = startNumber + offset
            Dim candidateName = $"{baseName}{serial.ToString(New String("0"c, width), CultureInfo.InvariantCulture)}"
            Dim candidate = IO.Path.Combine(dir, $"{candidateName}{ext}")
            If 输出路径可用(candidate, 已保留输出路径) Then Return candidate
        Next
        Return IO.Path.Combine(dir, $"{baseName}{Now:yyyyMMddHHmmss}{ext}")
    End Function

    Private Shared Function 输出路径可用(输出路径 As String, 已保留输出路径 As ISet(Of String)) As Boolean
        Return Not File.Exists(输出路径) AndAlso (已保留输出路径 Is Nothing OrElse Not 已保留输出路径.Contains(输出路径))
    End Function

        ' Must be called while 队列锁 is held so active and stopping tasks reserve different names.
    Private Shared Sub 为任务保留输出文件(task As 编码任务_v6)
        If task Is Nothing OrElse task.预设数据 Is Nothing Then Exit Sub
        If Not String.IsNullOrWhiteSpace(task.输出文件) AndAlso Not task.输出文件由自动命名生成 Then Exit Sub

        Dim 已保留输出路径 As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
        For Each other In 队列
            If other Is task OrElse (Not 是否进行中任务(other) AndAlso Not other.正在执行) OrElse String.IsNullOrWhiteSpace(other.输出文件) Then Continue For
            已保留输出路径.Add(other.输出文件)
        Next

        task.输出文件 = 计算输出位置_v6(task.输入文件, task.预设数据, True, 已保留输出路径)
        task.输出文件由自动命名生成 = Not String.IsNullOrWhiteSpace(task.输出文件)
    End Sub

End Class

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
    Public Property ID As String = Guid.NewGuid().ToString("N")
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
    Private 正在执行标记 As Boolean = False
    Private 执行版本 As Long = 0
    Private ReadOnly 日志锁 As New Object
    Private ReadOnly 完整日志缓存 As New List(Of 编码任务日志条目_v6)
    Private 日志序号 As Long = 0
    Private 进度日志数量 As Integer = 0
    Private 最新原始日志文本 As String = ""
    Private 最新原始日志是否错误 As Boolean = False
    Private 最新非进度日志文本 As String = ""
    Private 最新非进度日志是否错误 As Boolean = False
    Private 上次进度日志提交时间 As DateTime = DateTime.MinValue

    Public ReadOnly Property 可移除 As Boolean
        Get
            Return 状态 = 编码任务状态_v6.未处理 OrElse 状态 = 编码任务状态_v6.已完成 OrElse 状态 = 编码任务状态_v6.已停止 OrElse 状态 = 编码任务状态_v6.错误
        End Get
    End Property

    Public ReadOnly Property 可重置 As Boolean
        Get
            Return 状态 = 编码任务状态_v6.已完成 OrElse 状态 = 编码任务状态_v6.已停止 OrElse 状态 = 编码任务状态_v6.错误
        End Get
    End Property

    Public ReadOnly Property 可停止 As Boolean
        Get
            Return 状态 = 编码任务状态_v6.正在处理 OrElse 状态 = 编码任务状态_v6.已暂停 OrElse 状态 = 编码任务状态_v6.错误
        End Get
    End Property

    Public ReadOnly Property 可排序 As Boolean
        Get
            Return 状态 <> 编码任务状态_v6.正在处理 AndAlso 状态 <> 编码任务状态_v6.已暂停
        End Get
    End Property

    Public ReadOnly Property 当前步骤 As 编码步骤_v6
        Get
            If 当前步骤索引 < 0 OrElse 当前步骤索引 >= 步骤.Count Then Return Nothing
            Return 步骤(当前步骤索引)
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

    Friend Function 开始执行() As Long
        SyncLock 状态锁
            If 正在执行标记 Then Return 0
            正在执行标记 = True
            执行版本 += 1
            Return 执行版本
        End SyncLock
    End Function

    Friend Sub 结束执行()
        SyncLock 状态锁
            正在执行标记 = False
        End SyncLock
    End Sub

    Private Sub 使当前执行失效()
        SyncLock 状态锁
            If 正在执行标记 Then 执行版本 += 1
        End SyncLock
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
        SyncLock 日志锁
            If 类别 = 编码任务日志类别_v6.进度 AndAlso Not isError Then
                Dim nowTime = DateTime.Now
                If (nowTime - 上次进度日志提交时间).TotalMilliseconds < 1000 Then Exit Sub
                上次进度日志提交时间 = nowTime
            End If

            日志序号 += 1
            Dim entry As New 编码任务日志条目_v6 With {
                .序号 = 日志序号,
                .时间 = DateTime.Now,
                .阶段名 = stageName,
                .文本 = 文本,
                .类别 = If(isError, 编码任务日志类别_v6.错误, 类别),
                .是否错误 = isError
            }
            完整日志缓存.Add(entry)
            If entry.类别 = 编码任务日志类别_v6.进度 Then 进度日志数量 += 1
            日志版本号 += 1
            实时输出 = 文本

            最新原始日志文本 = 文本
            最新原始日志是否错误 = isError
            If entry.类别 <> 编码任务日志类别_v6.进度 Then
                非进度输出列表.Add(文本)
                最新非进度日志文本 = 文本
                最新非进度日志是否错误 = isError
            End If
            If isError Then 错误列表.Add(文本)
            更新最新底部日志()
            裁剪日志缓存()
        End SyncLock
        If 通知更新 Then 编码队列_v6.通知任务更新(Me)
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
            上次进度日志提交时间 = DateTime.MinValue
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
        Dim retained As New List(Of 编码任务日志条目_v6)(完整日志缓存.Count - removeCount)
        For Each entry In 完整日志缓存
            If removed < removeCount AndAlso entry.类别 = 编码任务日志类别_v6.进度 Then
                removed += 1
            Else
                retained.Add(entry)
            End If
        Next
        If removed = 0 Then Exit Sub

        完整日志缓存.Clear()
        完整日志缓存.AddRange(retained)
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
            准备输入输出与步骤()
            If Not 是当前执行(执行标识) Then Exit Try
            设定系统状态_v6()
            编码队列_v6.通知任务更新(Me)

            当前步骤索引 = 0
            While 当前步骤索引 < 步骤.Count
                Dim stepItem = 步骤(当前步骤索引)
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

            If 是当前执行(执行标识) AndAlso 状态 = 编码任务状态_v6.正在处理 Then
                状态 = 编码任务状态_v6.已完成
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
            释放进程()
            清理帧服务器缓存()
            恢复系统状态_v6()
            编码队列_v6.通知任务更新(Me)
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
            步骤.Clear()
            步骤.Add(New 编码步骤_v6 With {
                .阶段 = 预设数据_v6.命令行阶段.普通单次,
                .命令行 = 命令行,
                .显示名称 = "命令行"
            })
        End If
        If 步骤.Count = 0 Then Throw New InvalidOperationException("没有可执行的编码步骤")
    End Sub

    Private Sub 重建编码步骤()
        If 预设数据 Is Nothing Then Exit Sub
        Dim generated = 预设管理_v6.生成阶段化命令行(预设数据, 输入文件, 输出文件, 媒体总时长, ID)
        步骤.Clear()
        For Each item In generated
            If item.阶段 = 预设数据_v6.命令行阶段.FFprobe获取时长 AndAlso Not String.IsNullOrWhiteSpace(媒体总时长) Then Continue For
            步骤.Add(New 编码步骤_v6 With {
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
    End Sub

    Private Function 运行步骤Async(stepItem As 编码步骤_v6) As Task(Of Integer)
        Dim tcs As New TaskCompletionSource(Of Integer)
        Dim process As New Process()
        当前进程 = process
        process.StartInfo.FileName = If(stepItem.阶段 = 预设数据_v6.命令行阶段.FFprobe获取时长, "ffprobe", If(设置_v6.实例对象.替代进程文件名 <> "", 设置_v6.实例对象.替代进程文件名, "ffmpeg"))
        process.StartInfo.WorkingDirectory = If(设置_v6.实例对象.工作目录 <> "", 设置_v6.实例对象.工作目录, "")
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
        process.EnableRaisingEvents = True
        AddHandler process.OutputDataReceived, Sub(sender, e) 处理输出(stepItem, e.Data)
        AddHandler process.ErrorDataReceived, Sub(sender, e) 处理输出(stepItem, e.Data)
        AddHandler process.Exited, Sub()
                                       Dim code As Integer = -1
                                       Try
                                           code = process.ExitCode
                                       Catch
                                       End Try
                                       tcs.TrySetResult(code)
                                   End Sub
        追加日志($"[3FUI] 执行：{process.StartInfo.FileName} {process.StartInfo.Arguments}", 编码任务日志类别_v6.系统, stepItem, False, False)
        process.Start()
        编码队列_v6.通知任务更新(Me)
        process.BeginOutputReadLine()
        process.BeginErrorReadLine()
        If 设置_v6.实例对象.指定处理器核心 <> "" Then
            process.ProcessorAffinity = GetAffinityMask(设置_v6.实例对象.指定处理器核心.Split(","c).Select(Function(s) s.Trim()).Where(Function(s) Integer.TryParse(s, Nothing)).Select(Function(s) Integer.Parse(s)).ToArray())
        End If
        Return tcs.Task
    End Function

    Private Sub 处理输出(stepItem As 编码步骤_v6, line As String)
        If line Is Nothing Then Exit Sub
        stepItem.输出缓存.Add(line)
        If stepItem.输出缓存.Count > 2000 Then stepItem.输出缓存.RemoveRange(0, stepItem.输出缓存.Count - 1000)
        If line.Contains("Duration:", StringComparison.OrdinalIgnoreCase) Then 进度.解析FFmpeg输出(line, 计算当前总时长())
        Dim isProgressLine = 编码队列_v6.是否进度输出(line)
        If isProgressLine Then
            进度.解析FFmpeg输出(line, 计算当前总时长())
        End If
        Dim isError = 编码队列_v6.是否错误输出(line)
        Dim category = If(isError, 编码任务日志类别_v6.错误, If(isProgressLine, 编码任务日志类别_v6.进度, 编码任务日志类别_v6.输出))
        追加日志(line, category, stepItem, isError)
    End Sub

    Private Function 计算当前总时长() As TimeSpan
        If 预设数据 IsNot Nothing Then
            If 预设数据.剪辑区间_入点 <> "" AndAlso 预设数据.剪辑区间_出点 <> "" Then
                Return 编码进度_v6.转换时间(预设数据.剪辑区间_出点) - 编码进度_v6.转换时间(预设数据.剪辑区间_入点)
            End If
            If 预设数据.剪辑区间_入点 = "" AndAlso 预设数据.剪辑区间_出点 <> "" Then Return 编码进度_v6.转换时间(预设数据.剪辑区间_出点)
        End If
        If Not String.IsNullOrWhiteSpace(媒体总时长) Then Return TimeSpan.FromSeconds(Val(媒体总时长))
        Return 进度.总时长
    End Function

    Public Sub 暂停()
        Try
            If 状态 = 编码任务状态_v6.正在处理 AndAlso 当前进程 IsNot Nothing AndAlso Not 当前进程.HasExited Then
                If NtSuspendProcess(当前进程.Handle) = 0 Then
                    状态 = 编码任务状态_v6.已暂停
                    任务耗时计时器.Stop()
                    追加日志("[3FUI] 任务已暂停", 编码任务日志类别_v6.系统, 当前步骤, False, False)
                    编码队列_v6.通知任务更新(Me)
                End If
            End If
        Catch ex As Exception
            追加日志("[3FUI] 暂停失败：" & ex.Message, 编码任务日志类别_v6.错误, 当前步骤, True)
        End Try
    End Sub

    Public Sub 恢复()
        Try
            If 状态 = 编码任务状态_v6.已暂停 AndAlso 当前进程 IsNot Nothing AndAlso Not 当前进程.HasExited Then
                If NtResumeProcess(当前进程.Handle) = 0 Then
                    状态 = 编码任务状态_v6.正在处理
                    任务耗时计时器.Start()
                    设定系统状态_v6()
                    追加日志("[3FUI] 任务已恢复", 编码任务日志类别_v6.系统, 当前步骤, False, False)
                    编码队列_v6.通知任务更新(Me)
                End If
            End If
        Catch ex As Exception
            追加日志("[3FUI] 恢复失败：" & ex.Message, 编码任务日志类别_v6.错误, 当前步骤, True)
        End Try
    End Sub

    Public Sub 停止()
        Try
            Dim process As Process = Nothing
            SyncLock 状态锁
                If Not 可停止 Then Exit Sub
                手动停止 = True
                状态 = 编码任务状态_v6.已停止
                process = 当前进程
            End SyncLock
            If process IsNot Nothing AndAlso Not process.HasExited Then process.Kill()
            任务耗时计时器.Stop()
            追加日志("[3FUI] 正在停止任务", 编码任务日志类别_v6.系统, 当前步骤, False, False)
            编码队列_v6.通知任务更新(Me)
        Catch ex As Exception
            追加日志("[3FUI] 停止失败：" & ex.Message, 编码任务日志类别_v6.错误, 当前步骤, True)
        End Try
    End Sub

    Public Sub 重置()
        使当前执行失效()
        释放资源()
        If 输出文件由自动命名生成 Then 输出文件 = ""
        输出文件由自动命名生成 = False
        状态 = 编码任务状态_v6.未处理
        当前步骤索引 = -1
        手动停止 = False
        实时输出 = ""
        清空日志(False)
        步骤.Clear()
        进度 = New 编码进度_v6
        输入文件大小 = 0
        媒体总时长 = ""
        任务耗时计时器.Reset()
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

Public Class 编码进度_v6
    Private Shared ReadOnly DurationPattern As New Regex("Duration:\s*(\d+:\d{2}:\d{2}\.\d{2})", RegexOptions.Compiled)
    Private Shared ReadOnly SizePattern As New Regex("size=\s*(?<value>\d+)\s*(?<unit>[KMG]iB|kB)", RegexOptions.Compiled Or RegexOptions.IgnoreCase)
    Private Shared ReadOnly TimePattern As New Regex("time=\s*(?<value>\d+:\d{2}:\d{2}\.\d{2})", RegexOptions.Compiled)
    Private Shared ReadOnly QPattern As New Regex("q=\s*(?<value>[\d\.\-]+)", RegexOptions.Compiled)
    Private Shared ReadOnly BitratePattern As New Regex("bitrate=\s*(?<value>[\d\.]+)\s*kbits/s", RegexOptions.Compiled)
    Private Shared ReadOnly SpeedPattern As New Regex("speed=\s*(?<value>[\d\.eE\+\-]+)\s*x", RegexOptions.Compiled)

    Public Property 当前阶段 As String = ""
    Public Property 总时长 As TimeSpan = TimeSpan.Zero
    Public Property 当前时间 As TimeSpan = TimeSpan.Zero
    Public Property 百分比 As Double = 0
    Public Property 进度文本 As String = ""
    Public Property 效率文本 As String = ""
    Public Property 输出大小文本 As String = ""
    Public Property 输出大小KB As Long = 0
    Public Property 质量文本 As String = ""
    Public Property 比特率文本 As String = ""
    Public Property 时间文本 As String = ""

    Public Sub 解析FFmpeg输出(line As String, preferredTotal As TimeSpan)
        If String.IsNullOrWhiteSpace(line) Then Exit Sub
        If 总时长 = TimeSpan.Zero AndAlso preferredTotal > TimeSpan.Zero Then 总时长 = preferredTotal
        Dim dm = DurationPattern.Match(line)
        If 总时长 = TimeSpan.Zero AndAlso dm.Success Then 总时长 = 转换时间(dm.Groups(1).Value)
        Dim tm = TimePattern.Match(line)
        If tm.Success Then 当前时间 = 转换时间(tm.Groups("value").Value)

        If 总时长.TotalSeconds > 0 AndAlso 当前时间.TotalSeconds > 0 Then
            百分比 = Math.Min(Math.Max(当前时间.TotalSeconds / 总时长.TotalSeconds, 0), 1)
            进度文本 = $"{百分比 * 100:F1}%"
        Else
            进度文本 = ""
        End If

        Dim sm = SizePattern.Match(line)
        If sm.Success Then
            输出大小KB = 转换到KB(Long.Parse(sm.Groups("value").Value), sm.Groups("unit").Value)
        End If
        更新输出大小文本()
        Dim qm = QPattern.Match(line)
        If qm.Success Then 质量文本 = 格式化质量文本(qm.Groups("value").Value)
        Dim bm = BitratePattern.Match(line)
        If bm.Success Then 比特率文本 = 格式化比特率文本(bm.Groups("value").Value)
        Dim sp = SpeedPattern.Match(line)
        If sp.Success Then
            Dim speed As Double
            If Double.TryParse(sp.Groups("value").Value, NumberStyles.Any, CultureInfo.InvariantCulture, speed) Then
                效率文本 = 格式化效率文本(speed)
                If 总时长.TotalSeconds > 0 AndAlso 当前时间.TotalSeconds > 0 AndAlso speed > 0 Then
                    Dim remain = Math.Max(0, (总时长.TotalSeconds - 当前时间.TotalSeconds) / speed)
                    时间文本 = 格式化秒(remain)
                End If
            End If
        End If
    End Sub

    Private Shared Function 格式化质量文本(value As String) As String
        Dim q As Double
        If Not Double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, q) Then Return If(value, "")
        If q = 0 OrElse q = -1 Then Return ""
        Return q.ToString("F0", CultureInfo.InvariantCulture)
    End Function

    Private Shared Function 格式化效率文本(speed As Double) As String
        If speed * 100 >= 10000 Then Return speed.ToString("F0", CultureInfo.InvariantCulture) & "x"
        Return $"{speed * 100:F0}%"
    End Function

    Private Shared Function 格式化比特率文本(value As String) As String
        Dim bitrate As Double
        If Not Double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, bitrate) Then Return If(value, "") & " kbps"
        If bitrate >= 10000 Then Return (bitrate / 1000).ToString("F2", CultureInfo.InvariantCulture) & " Mbps"
        Return bitrate.ToString("F0", CultureInfo.InvariantCulture) & " kbps"
    End Function

    Private Sub 更新输出大小文本()
        If 输出大小KB <= 0 Then
            输出大小文本 = ""
            Exit Sub
        End If

        输出大小文本 = 格式化大小KB(输出大小KB)
        If 百分比 <= 0 OrElse 百分比 >= 1 Then Exit Sub

        Dim estimatedSizeKB = CLng(输出大小KB / 百分比)
        输出大小文本 &= " - " & 格式化预估大小KB(estimatedSizeKB)
    End Sub

    Public Shared Function 转换时间(value As String) As TimeSpan
        Dim t As TimeSpan
        If TimeSpan.TryParse(value, CultureInfo.InvariantCulture, t) Then Return t
        Dim seconds As Double
        If Double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, seconds) Then Return TimeSpan.FromSeconds(seconds)
        Return TimeSpan.Zero
    End Function

    Private Shared Function 转换到KB(value As Long, unit As String) As Long
        Select Case unit.ToLowerInvariant()
            Case "kb", "kib" : Return value
            Case "mib" : Return value * 1024
            Case "gib" : Return value * 1024 * 1024
            Case Else : Return value
        End Select
    End Function

    Public Shared Function 格式化大小KB(value As Long) As String
        If value >= 1024L * 1024L Then Return $"{value / 1024.0 / 1024.0:F2} GB"
        If value >= 1024L Then Return $"{value / 1024.0:F0} MB"
        Return $"{value} KB"
    End Function

    Private Shared Function 格式化预估大小KB(value As Long) As String
        If value >= 1024L * 1024L Then Return $"{value / 1024.0 / 1024.0:F1} GB"
        If value >= 1024L Then Return $"{value / 1024.0:F0} MB"
        Return $"{value} KB"
    End Function

    Public Shared Function 格式化秒(value As Double) As String
        Dim h = CInt(Math.Floor(value / 3600.0))
        Dim m = CInt(Math.Floor((value - h * 3600.0) / 60.0))
        Dim s = CInt(Math.Floor(value - h * 3600.0 - m * 60.0))
        Dim parts As New List(Of String)
        If h > 0 Then parts.Add($"{h}h")
        If m > 0 OrElse h > 0 Then parts.Add($"{m}m")
        parts.Add($"{s}s")
        Return String.Join("", parts)
    End Function
End Class
