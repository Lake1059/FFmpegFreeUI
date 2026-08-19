Imports System.Diagnostics
Imports System.Threading
Imports System.Threading.Tasks
Imports LakeUI

Public NotInheritable Class 用户使用统计_v6
    Private Const 赞助地址 As String = "https://ifdian.net/a/1059Studio"
    Private Shared ReadOnly 时长里程碑Ticks As Long = TimeSpan.FromHours(240).Ticks
    Private Shared ReadOnly 后台调度器 As TaskScheduler =
        New ConcurrentExclusiveSchedulerPair(TaskScheduler.Default, 1).ExclusiveScheduler
    Private Shared ReadOnly 提示队列锁 As New Object()
    Private Shared 提示队列尾 As Task = Task.CompletedTask
    Private Shared ReadOnly ButtonTexts As String() = {"当然", "现在不"}

    Private Sub New()
    End Sub

    Public Shared Sub 记录编码任务执行结果(成功完成 As Boolean, 执行时长 As TimeSpan)
        Dim 记录任务 = 后台执行(
            Function()
                Dim 设置 = 设置_v6.实例对象
                设置.用户统计_任务执行总时长Ticks = 安全累加(设置.用户统计_任务执行总时长Ticks, Math.Max(0, 执行时长.Ticks))

                Dim 提示项目 As New List(Of String)
                If 成功完成 Then
                    设置.用户统计_成功编码任务数 = 安全累加(设置.用户统计_成功编码任务数, 1)
                    If Not 设置.用户统计_首次成功提示已显示 Then
                        设置.用户统计_首次成功提示已显示 = True
                        提示项目.Add("首次成功编码任务")
                    End If
                End If

                设置_v6.后台保存设置()
                Return 提示项目
            End Function)

        完成记录并提示(记录任务)
    End Sub

    Public Shared Sub 启动时后台检查(所属窗体 As Form)
        启动时后台检查核心(所属窗体)
    End Sub

    Public Shared Async Function 退出时后台检查Async(所属窗体 As Form) As Task
        Dim 提示项目 = Await 后台执行(AddressOf 检查待提示里程碑)
        Await 排队显示赞助提示Async(所属窗体, 提示项目)
    End Function

    Private Shared Async Sub 启动时后台检查核心(所属窗体 As Form)
        Try
            Dim 提示项目 = Await 后台执行(AddressOf 检查待提示里程碑)
            Await 排队显示赞助提示Async(所属窗体, 提示项目)
        Catch
        End Try
    End Sub

    Private Shared Async Sub 完成记录并提示(记录任务 As Task(Of List(Of String)))
        Try
            Dim 提示项目 = Await 记录任务
            Await 排队显示赞助提示Async(Nothing, 提示项目)
        Catch
        End Try
    End Sub

    Private Shared Function 检查待提示里程碑() As List(Of String)
        Dim 设置 = 设置_v6.实例对象
        Dim 提示项目 As New List(Of String)

        If 设置.用户统计_成功编码任务数 > 0 AndAlso Not 设置.用户统计_首次成功提示已显示 Then
            设置.用户统计_首次成功提示已显示 = True
            提示项目.Add("首次成功编码任务")
        End If

        Dim 编码任务百次档位 = Math.Max(0, 设置.用户统计_成功编码任务数) \ 100
        If 编码任务百次档位 > 设置.用户统计_已提示编码任务百次档位 Then
            设置.用户统计_已提示编码任务百次档位 = 编码任务百次档位
            提示项目.Add($"{编码任务百次档位 * 100} 次成功编码任务")
        End If

        Dim 任务时长240小时档位 = Math.Max(0, 设置.用户统计_任务执行总时长Ticks) \ 时长里程碑Ticks
        If 任务时长240小时档位 > 设置.用户统计_已提示任务时长240小时档位 Then
            设置.用户统计_已提示任务时长240小时档位 = 任务时长240小时档位
            提示项目.Add($"累计 {任务时长240小时档位 * 240} 小时的任务执行")
        End If

        If 提示项目.Count > 0 Then 设置_v6.后台保存设置()
        Return 提示项目
    End Function

    Private Shared Function 后台执行(Of T)(操作 As Func(Of T)) As Task(Of T)
        Return Task.Factory.StartNew(操作, CancellationToken.None, TaskCreationOptions.DenyChildAttach, 后台调度器)
    End Function

    Private Shared Function 排队显示赞助提示Async(所属窗体 As Form, 提示项目 As IReadOnlyCollection(Of String)) As Task
        SyncLock 提示队列锁
            Dim 前一提示 = 提示队列尾
            Dim 当前提示 = 等待并显示赞助提示Async(前一提示, 所属窗体, 提示项目)
            提示队列尾 = 当前提示
            Return 当前提示
        End SyncLock
    End Function

    Private Shared Async Function 等待并显示赞助提示Async(前一提示 As Task, 所属窗体 As Form, 提示项目 As IReadOnlyCollection(Of String)) As Task
        Await Task.Yield()
        Try
            Await 前一提示
        Catch
        End Try
        Await 显示赞助提示Async(所属窗体, 提示项目)
    End Function

    Private Shared Function 显示赞助提示Async(所属窗体 As Form, 提示项目 As IReadOnlyCollection(Of String)) As Task
        If 提示项目 Is Nothing OrElse 提示项目.Count = 0 Then Return Task.CompletedTask
        Dim 完成源 As New TaskCompletionSource(Of Boolean)(TaskCreationOptions.RunContinuationsAsynchronously)
        Try
            If 所属窗体 Is Nothing Then
                界面线程执行(Sub(状态 As Object) 执行赞助提示(FormMain_v6, 提示项目, 完成源))
            ElseIf 所属窗体.IsDisposed OrElse Not 所属窗体.IsHandleCreated Then
                完成源.TrySetResult(True)
            ElseIf 所属窗体.InvokeRequired Then
                所属窗体.BeginInvoke(Sub() 执行赞助提示(所属窗体, 提示项目, 完成源))
            Else
                执行赞助提示(所属窗体, 提示项目, 完成源)
            End If
        Catch
            完成源.TrySetResult(True)
        End Try

        Return 完成源.Task
    End Function

    Private Shared Sub 执行赞助提示(所属窗体 As Form, 提示项目 As IReadOnlyCollection(Of String), 完成源 As TaskCompletionSource(Of Boolean))
        Try
            If 所属窗体 Is Nothing OrElse 所属窗体.IsDisposed OrElse Not 所属窗体.IsHandleCreated Then Return
            Dim 提示内容 = $"您已经完成了 {String.Join("、", 提示项目)}，想要为 3FUI 提供支持吗？"
            Dim 结果 = ExOverlayMsgBox(所属窗体, vbCrLf & 提示内容, ButtonTexts, "已完成里程碑", 1)
            If 结果 = 0 Then Process.Start(New ProcessStartInfo With {.FileName = 赞助地址, .UseShellExecute = True})
        Catch
        Finally
            完成源.TrySetResult(True)
        End Try
    End Sub

    Private Shared Function 安全累加(当前值 As Long, 增量 As Long) As Long
        If 当前值 < 0 Then 当前值 = 0
        If 增量 <= 0 Then Return 当前值
        If 当前值 > Long.MaxValue - 增量 Then Return Long.MaxValue
        Return 当前值 + 增量
    End Function
End Class
