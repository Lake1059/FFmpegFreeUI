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

    Private Class 批量预设任务输入_v6
        Public Property 输入文件 As String = ""
        Public Property 任务名称 As String = ""
        Public Property 输出文件 As String = ""
    End Class

    Private Shared ReadOnly 队列项目 As New List(Of 编码任务_v6)
    Private Shared ReadOnly 任务索引 As New Dictionary(Of String, 编码任务_v6)(StringComparer.Ordinal)

    Public Shared ReadOnly Property 队列 As IReadOnlyList(Of 编码任务_v6)
        Get
            Return 获取队列快照().AsReadOnly()
        End Get
    End Property
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
    Friend Shared Event 任务需立即刷新()
    Friend Shared Event 插件事件已触发(事件名称 As String, 任务 As 编码任务_v6, 日志 As 编码任务日志条目_v6)

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
            Return 队列项目.ToList()
        End SyncLock
    End Function

    Public Shared Function 根据ID获取任务(id As String) As 编码任务_v6
        If String.IsNullOrWhiteSpace(id) Then Return Nothing
        SyncLock 队列锁
            Dim task As 编码任务_v6 = Nothing
            任务索引.TryGetValue(id, task)
            Return task
        End SyncLock
    End Function

    Public Shared Function 获取进行中任务数量() As Integer
        SyncLock 队列锁
            Return 队列项目.Where(Function(x) 是否进行中任务(x)).Count()
        End SyncLock
    End Function

    Public Shared Function 获取未处理任务数量() As Integer
        SyncLock 队列锁
            Return 队列项目.Where(Function(x) x.状态 = 编码任务状态_v6.未处理).Count()
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
            For Each task In 队列项目
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
        原子文件写入_v6.写入文本(未处理任务缓存文件路径, json)
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
                队列项目.AddRange(restored)
                For Each task In restored
                    任务索引.Add(task.ID, task)
                Next
            End SyncLock
            For Each task In restored
                触发插件事件("task.added", task)
            Next
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
        Dim cache = Text.Json.JsonSerializer.Deserialize(Of 未处理任务缓存文件_v6)(json, JsonSO)
        If cache Is Nothing OrElse cache.版本 <> 1 OrElse cache.任务 Is Nothing Then
            Throw New InvalidDataException("未处理任务缓存格式或版本不受支持")
        End If
        Return cache
    End Function

    Public Shared Sub 停止所有进行中任务()
        Dim stopping As New List(Of 编码任务_v6)
        Dim changed As New List(Of 编码任务_v6)

        SyncLock 队列锁
            自动调度已暂停 = True
            For Each task In 队列项目
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
        Return 添加预设任务批量(New List(Of 批量预设任务输入_v6) From {
            New 批量预设任务输入_v6 With {.输入文件 = 输入文件, .任务名称 = 任务名称, .输出文件 = 输出文件}
        }, 预设数据).FirstOrDefault()
    End Function

    Public Shared Function 批量添加预设任务(输入文件列表 As IEnumerable(Of String), 预设数据 As 预设数据_v6) As List(Of 编码任务_v6)
        Dim specs As New List(Of 批量预设任务输入_v6)
        For Each inputPath In If(输入文件列表, Array.Empty(Of String)())
            If String.IsNullOrWhiteSpace(inputPath) Then Continue For
            specs.Add(New 批量预设任务输入_v6 With {.输入文件 = inputPath, .任务名称 = Path.GetFileName(inputPath)})
        Next
        Return 添加预设任务批量(specs, 预设数据)
    End Function

    Private Shared Function 添加预设任务批量(specs As IEnumerable(Of 批量预设任务输入_v6), 预设数据 As 预设数据_v6) As List(Of 编码任务_v6)
        Dim added As New List(Of 编码任务_v6)
        If 预设数据 Is Nothing Then Return added
        Dim clonePreset = 预设管理_v6.创建预设克隆工厂(预设数据)
        For Each spec In If(specs, Array.Empty(Of 批量预设任务输入_v6)())
            If spec Is Nothing OrElse String.IsNullOrWhiteSpace(spec.输入文件) Then Continue For
            added.Add(New 编码任务_v6 With {
                .输入文件 = spec.输入文件,
                .输出文件 = If(spec.输出文件, ""),
                .任务名称 = 应用任务名称混淆(If(String.IsNullOrWhiteSpace(spec.任务名称), Path.GetFileName(spec.输入文件), spec.任务名称)),
                .预设数据 = clonePreset()
            })
        Next
        If added.Count = 0 Then Return added
        SyncLock 队列锁
            队列项目.AddRange(added)
            For Each task In added
                任务索引.Add(task.ID, task)
            Next
        End SyncLock
        For Each task In added
            触发插件事件("task.added", task)
        Next
        RaiseEvent 队列已变化()
        请求调度()
        Return added
    End Function

    Public Shared Function 添加命令行任务(命令行 As String, 任务名称 As String, 输出文件 As String, Optional 输入文件 As String = "") As 编码任务_v6
        Dim task As New 编码任务_v6 With {
            .输入文件 = 输入文件,
            .输出文件 = 输出文件,
            .任务名称 = 应用任务名称混淆(If(String.IsNullOrWhiteSpace(任务名称), If(String.IsNullOrWhiteSpace(输入文件), "命令行任务", Path.GetFileName(输入文件)), 任务名称)),
            .命令行 = 命令行
        }
        SyncLock 队列锁
            队列项目.Add(task)
            任务索引.Add(task.ID, task)
        End SyncLock
        触发插件事件("task.added", task)
        RaiseEvent 队列已变化()
        请求调度()
        Return task
    End Function

    Public Shared Function 添加来自参数面板的文件(files As IEnumerable(Of String), 参数面板 As Form_v6_参数面板) As Integer
        If files Is Nothing OrElse 参数面板 Is Nothing Then Return 0
        Dim preset = 预设管理_v6.从面板创建预设(参数面板)
        Dim expanded As New List(Of String)
        For Each file In files
            If String.IsNullOrWhiteSpace(file) Then Continue For
            If Directory.Exists(file) Then
                For Each child In Directory.GetFiles(file, "*", SearchOption.AllDirectories)
                    expanded.Add(If(设置_v6.实例对象.转译模式, 转译模式处理路径(child), child))
                Next
            Else
                expanded.Add(If(设置_v6.实例对象.转译模式, 转译模式处理路径(file), file))
            End If
        Next
        If expanded.Count = 0 Then Return 0
        If Not 预设管理_v6.验证可添加任务(preset) Then Return 0
        Return 批量添加预设任务(expanded, preset).Count
    End Function

    Public Shared Function 同步未处理预设任务(预设数据 As 预设数据_v6) As 预设同步结果
        Return 同步预设任务核心(Nothing, 预设数据)
    End Function

    Public Shared Function 同步指定未处理预设任务(ids As IEnumerable(Of String), 预设数据 As 预设数据_v6) As 预设同步结果
        Return 同步预设任务核心(New HashSet(Of String)(If(ids, Array.Empty(Of String)())), 预设数据)
    End Function

    Private Shared Function 同步预设任务核心(idSet As HashSet(Of String), 预设数据 As 预设数据_v6) As 预设同步结果
        If 预设数据 Is Nothing Then Return New 预设同步结果
        Dim clonePreset = 预设管理_v6.创建预设克隆工厂(预设数据)
        Return 修改预设任务核心(idSet, Function(task) clonePreset())
    End Function

    Public Shared Function 修改指定未处理预设任务(ids As IEnumerable(Of String), transform As Func(Of 编码任务_v6, 预设数据_v6)) As 预设同步结果
        Return 修改预设任务核心(New HashSet(Of String)(If(ids, Array.Empty(Of String)()), StringComparer.OrdinalIgnoreCase), transform)
    End Function

    Private Shared Function 修改预设任务核心(idSet As HashSet(Of String), transform As Func(Of 编码任务_v6, 预设数据_v6)) As 预设同步结果
        Dim result As New 预设同步结果
        If idSet IsNot Nothing AndAlso idSet.Count = 0 Then Return result

        Dim changed As New List(Of 编码任务_v6)
        SyncLock 队列锁
            Dim prepared As New Dictionary(Of 编码任务_v6, 预设数据_v6)
            For Each task In 队列项目
                If idSet IsNot Nothing AndAlso Not idSet.Contains(task.ID) Then Continue For

                If task.预设数据 Is Nothing Then
                    result.已跳过非预设任务 += 1
                    Continue For
                End If

                If task.状态 <> 编码任务状态_v6.未处理 OrElse task.正在执行 Then
                    result.已跳过不可修改任务 += 1
                    Continue For
                End If

                prepared.Add(task, transform(task))
            Next
            ' 先验证所有副本，避免后续目标校验失败造成部分写入。
            For Each entry In prepared
                Dim task = entry.Key
                task.预设数据 = entry.Value
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
        Return 预设管理_v6.克隆预设数据(source)
    End Function

    Public Shared Sub 开始任务(ids As IEnumerable(Of String))
        Dim idSet As New HashSet(Of String)(If(ids, Array.Empty(Of String)()))
        If idSet.Count = 0 Then Exit Sub

        Dim starting As New List(Of KeyValuePair(Of 编码任务_v6, Long))
        SyncLock 队列锁
            For Each task In 队列项目
                If idSet.Contains(task.ID) AndAlso task.状态 = 编码任务状态_v6.未处理 Then
                    Dim 执行标识 = task.开始执行()
                    If 执行标识 = 0 Then Continue For
                    标记任务已实际启动()
                    starting.Add(New KeyValuePair(Of 编码任务_v6, Long)(task, 执行标识))
                End If
            Next
            If starting.Count > 0 Then 自动调度已暂停 = False
        End SyncLock

        广播任务更新(starting.Select(Function(x) x.Key))
        For Each item In starting
            触发插件事件("task.started", item.Key)
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

        Dim 可能停止执行中任务 = stopping.Any(Function(task) task.正在执行)
        If 可能停止执行中任务 Then
            SyncLock 队列锁
                自动调度已暂停 = True
            End SyncLock
        End If

        Dim 已停止执行中任务 As Boolean = False
        For Each task In stopping
            If task.停止并报告是否停止执行() Then 已停止执行中任务 = True
        Next

        If 可能停止执行中任务 AndAlso Not 已停止执行中任务 Then
            SyncLock 队列锁
                自动调度已暂停 = False
            End SyncLock
            请求调度()
        End If
    End Sub

    Public Shared Sub 取消自动开始任务(ids As IEnumerable(Of String))
        Dim changed As New List(Of 编码任务_v6)
        SyncLock 队列锁
            For Each task In 获取指定任务(ids)
                If task.状态 = 编码任务状态_v6.未处理 AndAlso task.允许自动启动 Then
                    task.允许自动启动 = False
                    changed.Add(task)
                End If
            Next
        End SyncLock
        广播任务更新(changed)
    End Sub

    Public Shared Sub 重置任务(ids As IEnumerable(Of String))
        Dim changed As New List(Of 编码任务_v6)
        SyncLock 队列锁
            For Each task In 获取指定任务(ids)
                If task.可重置 Then
                    task.重置()
                    changed.Add(task)
                End If
            Next
        End SyncLock
        广播任务更新(changed)
    End Sub

    Public Shared Sub 移除任务(ids As IEnumerable(Of String))
        Dim idSet As New HashSet(Of String)(If(ids, Array.Empty(Of String)()))
        If idSet.Count = 0 Then Exit Sub
        Dim removed As New List(Of 编码任务_v6)
        SyncLock 队列锁
            队列项目.RemoveAll(Function(task)
                              If Not idSet.Contains(task.ID) OrElse Not task.可移除 Then Return False
                              removed.Add(task)
                              任务索引.Remove(task.ID)
                              Return True
                          End Function)
        End SyncLock
        For Each task In removed
            触发插件事件("task.removed", task)
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
            For Each task In 队列项目
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
        If ids.Count = 0 OrElse ids.Distinct(StringComparer.Ordinal).Count() <> ids.Count Then Return False
        SyncLock 队列锁
            If ids.Count <> 队列项目.Count Then Return False
            For i = 0 To 队列项目.Count - 1
                If Not 队列项目(i).可排序 AndAlso ids(i) <> 队列项目(i).ID Then Return False
            Next
            If ids.Any(Function(id) Not 任务索引.ContainsKey(id)) Then Return False
            队列项目.Clear()
            队列项目.AddRange(ids.Select(Function(id) 任务索引(id)))
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
            If 队列项目.Any(Function(x) idSet.Contains(x.ID) AndAlso Not x.可排序) Then Exit Sub
            If direction < 0 Then
                For i = 1 To 队列项目.Count - 1
                    If idSet.Contains(队列项目(i).ID) AndAlso Not idSet.Contains(队列项目(i - 1).ID) AndAlso 队列项目(i - 1).可排序 Then
                        Dim temp = 队列项目(i - 1)
                        队列项目(i - 1) = 队列项目(i)
                        队列项目(i) = temp
                    End If
                Next
            Else
                For i = 队列项目.Count - 2 To 0 Step -1
                    If idSet.Contains(队列项目(i).ID) AndAlso Not idSet.Contains(队列项目(i + 1).ID) AndAlso 队列项目(i + 1).可排序 Then
                        Dim temp = 队列项目(i + 1)
                        队列项目(i + 1) = 队列项目(i)
                        队列项目(i) = temp
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
                             Dim running = 队列项目.Where(Function(x) 是否进行中任务(x) OrElse x.正在执行).Count()
                             shouldRunAgain = Not 自动调度已暂停 AndAlso running < 获取并发上限() AndAlso 队列项目.Any(Function(x) x.状态 = 编码任务状态_v6.未处理 AndAlso x.允许自动启动 AndAlso Not x.正在执行)
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
                Dim running = 队列项目.Where(Function(x) 是否进行中任务(x) OrElse x.正在执行).Count()
                If running >= 获取并发上限() Then Exit Do
                nextTask = 队列项目.FirstOrDefault(Function(x) x.状态 = 编码任务状态_v6.未处理 AndAlso x.允许自动启动 AndAlso Not x.正在执行)
                If nextTask Is Nothing Then Exit Do
                执行标识 = nextTask.开始执行()
                If 执行标识 = 0 Then Continue Do
                标记任务已实际启动()
            End SyncLock
            通知任务更新(nextTask)
            触发插件事件("task.started", nextTask)
            异步执行任务(nextTask, 执行标识)
        Loop
    End Sub

    Private Shared Sub 异步执行任务(task As 编码任务_v6, 执行标识 As Long)
        Threading.Tasks.Task.Run(Async Function()
                                     Try
                                         Await task.开始Async(执行标识)
                                     Finally
                                         task.结束执行()
                                         通知任务更新(task)
                                         请求调度(True)
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
            Return 队列项目.Where(Function(x) idSet.Contains(x.ID)).ToList()
        End SyncLock
    End Function

    Private Shared Function 是否进行中任务(task As 编码任务_v6) As Boolean
        If task Is Nothing Then Return False
        Return task.状态 = 编码任务状态_v6.正在处理 OrElse
               task.状态 = 编码任务状态_v6.已暂停
    End Function

    Private Shared Sub 广播任务更新(tasks As IEnumerable(Of 编码任务_v6))
        If tasks Is Nothing Then Exit Sub
        Dim hasChanges As Boolean = False
        For Each task In tasks
            RaiseEvent 任务已更新(task)
            hasChanges = True
        Next
        If hasChanges Then RaiseEvent 任务需立即刷新()
    End Sub

    Friend Shared Sub 通知任务更新(task As 编码任务_v6, Optional 立即刷新 As Boolean = True)
        RaiseEvent 任务已更新(task)
        If 立即刷新 Then RaiseEvent 任务需立即刷新()
    End Sub

    Friend Shared Sub 触发插件事件(事件名称 As String, task As 编码任务_v6, Optional 日志 As 编码任务日志条目_v6 = Nothing)
        If String.IsNullOrWhiteSpace(事件名称) OrElse task Is Nothing Then Exit Sub
        RaiseEvent 插件事件已触发(事件名称, task, 日志)
    End Sub

    Friend Shared Sub 标记任务出错()
        SyncLock 队列锁
            全部任务已完成是否有错误 = True
        End SyncLock
    End Sub

    Private Shared Sub 标记任务已实际启动()
        If Not 完成提示待播放 Then 全部任务已完成是否有错误 = False
        完成提示待播放 = True
    End Sub

    Private Shared Sub 检查全部完成提示()
        Dim shouldPlay As Boolean = False
        Dim hasError As Boolean = False
        SyncLock 队列锁
            If Not 完成提示待播放 Then Exit Sub
            If 队列项目.Count = 0 Then
                完成提示待播放 = False
                全部任务已完成是否有错误 = False
                Exit Sub
            End If
            If 队列项目.Any(Function(x) x.状态 = 编码任务状态_v6.未处理 OrElse x.状态 = 编码任务状态_v6.正在处理 OrElse x.状态 = 编码任务状态_v6.已暂停) Then Exit Sub
            shouldPlay = True
            hasError = 全部任务已完成是否有错误 OrElse 队列项目.Any(Function(x) x.状态 = 编码任务状态_v6.错误)
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
        Dim processName = If(stage = 预设数据_v6.命令行阶段.FFprobe获取时长, 设置_v6.获取FFprobe进程文件名(), 设置_v6.获取FFmpeg进程文件名())
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
            Case 预设数据_v6.自动命名选项.附加_递增时间戳
                Return 获取递增输出路径(输出路径, 已保留输出路径)
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

    Friend Shared Sub 为任务保留输出文件(task As 编码任务_v6)
        SyncLock 队列锁
            保留输出文件核心(task)
        End SyncLock
    End Sub

    Private Shared Sub 保留输出文件核心(task As 编码任务_v6)
        If task Is Nothing OrElse task.预设数据 Is Nothing Then Exit Sub
        If Not String.IsNullOrWhiteSpace(task.输出文件) AndAlso Not task.输出文件由自动命名生成 Then Exit Sub

        Dim 已保留输出路径 As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
        For Each other In 队列项目
            If other Is task OrElse (Not 是否进行中任务(other) AndAlso Not other.正在执行) OrElse String.IsNullOrWhiteSpace(other.输出文件) Then Continue For
            已保留输出路径.Add(other.输出文件)
        Next

        task.输出文件 = 计算输出位置_v6(task.输入文件, task.预设数据, True, 已保留输出路径)
        task.输出文件由自动命名生成 = Not String.IsNullOrWhiteSpace(task.输出文件)
    End Sub

End Class
