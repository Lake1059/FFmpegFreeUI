Imports LakeUI

' 独立于任务日志窗口的按需采样器，供队列和 Agent 共用。
Public NotInheritable Class 任务性能统计_v6
    Private Sub New()
    End Sub

    Private Shared ReadOnly 同步锁 As New Object()
    Private Shared ReadOnly 计数器表 As New Dictionary(Of Integer, MainAppUsageCounter)()

    Public Shared Function 获取快照(task As 编码任务_v6) As Dictionary(Of String, Object)
        Dim processId = If(task Is Nothing, 0, task.当前进程ID)
        Dim result As New Dictionary(Of String, Object) From {
            {"process_id", processId},
            {"sampling", "on_demand_independent_of_log_window"}
        }
        If processId <= 0 Then
            result("available") = False
            result("reason") = "当前任务没有活动进程"
            Return result
        End If

        SyncLock 同步锁
            清理已失效计数器(processId)
            Dim counter As MainAppUsageCounter = Nothing
            If Not 计数器表.TryGetValue(processId, counter) Then
                Try
                    counter = MainAppUsageCounter.Start(processId, True)
                    计数器表(processId) = counter
                Catch ex As Exception
                    result("available") = False
                    result("reason") = "任务性能计数器不可用：" & ex.Message
                    Return result
                End Try
            End If

            Try
                Dim snapshot = counter.GetSnapshot(True)
                result("available") = True
                result("cpu_percent") = Math.Round(snapshot.CpuUsagePercent, 1)
                result("active_private_working_set_bytes") = snapshot.ActivePrivateWorkingSetBytes
                result("commit_size_bytes") = snapshot.CommitSizeBytes
                result("gpu_percent") = Math.Round(snapshot.GpuUsagePercent, 1)
                result("gpu_dedicated_memory_bytes") = snapshot.GpuDedicatedMemoryBytes
                result("gpu_shared_memory_bytes") = snapshot.GpuSharedMemoryBytes
                Return result
            Catch ex As Exception
                释放计数器(processId)
                result("available") = False
                result("reason") = "无法获取任务性能快照：" & ex.Message
                Return result
            End Try
        End SyncLock
    End Function

    Private Shared Sub 清理已失效计数器(currentProcessId As Integer)
        For Each processId In 计数器表.Keys.Where(Function(x) x <> currentProcessId).ToList()
            释放计数器(processId)
        Next
    End Sub

    Private Shared Sub 释放计数器(processId As Integer)
        Dim counter As MainAppUsageCounter = Nothing
        If Not 计数器表.TryGetValue(processId, counter) Then Return
        计数器表.Remove(processId)
        Try
            counter.Dispose()
        Catch
        End Try
    End Sub
End Class
