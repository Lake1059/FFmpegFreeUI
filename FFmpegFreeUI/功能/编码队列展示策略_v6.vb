Imports System.IO
Imports LakeUI

Public Interface I编码队列展示策略_v6
    Sub 应用(task As 编码任务_v6, item As UltraDetailListView.ListItem)
End Interface

Public Class 旧版兼容编码队列展示策略_v6
    Implements I编码队列展示策略_v6

    Private Shared ReadOnly 预估大小警告色 As Color = Color.FromArgb(224, 198, 180, 72)

    Public Sub 应用(task As 编码任务_v6, item As UltraDetailListView.ListItem) Implements I编码队列展示策略_v6.应用
        If task Is Nothing OrElse item Is Nothing Then Exit Sub
        确保子项数量(item, 8)

        item.SubItems(0).Text = If(String.IsNullOrWhiteSpace(task.任务名称), Path.GetFileName(task.输入文件), task.任务名称)
        item.SubItems(1).Text = 状态文本(task.状态)
        item.SubItems(2).Text = task.进度.进度文本
        item.SubItems(3).Text = task.进度.效率文本
        item.SubItems(4).Text = task.进度.输出大小文本
        item.SubItems(5).Text = task.进度.质量文本
        item.SubItems(6).Text = task.进度.比特率文本
        item.SubItems(7).Text = 默认时间文本(task)

        Dim rowColor = 状态颜色(task.状态)
        设置整行颜色(item, rowColor)

        Select Case task.状态
            Case 编码任务状态_v6.正在处理, 编码任务状态_v6.已暂停
                设置进行中大小颜色(task, item)

            Case 编码任务状态_v6.已完成
                item.SubItems(2).Text = "100%"
                设置完成输出大小(task, item)
                item.SubItems(5).Text = ""
                item.SubItems(7).Text = "耗时 " & 格式化旧版耗时(task.任务耗时计时器.Elapsed)

            Case 编码任务状态_v6.已停止, 编码任务状态_v6.错误
                item.SubItems(5).Text = ""
        End Select

        应用底部日志行(task, item)
    End Sub

    Private Shared Sub 确保子项数量(item As UltraDetailListView.ListItem, count As Integer)
        While item.SubItems.Count < count
            item.SubItems.Add(New UltraDetailListView.ListSubItem())
        End While
    End Sub

    Private Shared Sub 设置整行颜色(item As UltraDetailListView.ListItem, color As Color)
        For Each subItem In item.SubItems
            subItem.ForeColor = color
        Next
    End Sub

    Private Shared Function 状态文本(status As 编码任务状态_v6) As String
        Select Case status
            Case 编码任务状态_v6.未处理 : Return "未处理"
            Case 编码任务状态_v6.正在处理 : Return "正在处理"
            Case 编码任务状态_v6.已暂停 : Return "已暂停"
            Case 编码任务状态_v6.已完成 : Return "已完成"
            Case 编码任务状态_v6.已停止 : Return "已停止"
            Case 编码任务状态_v6.错误 : Return "错误"
            Case Else : Return status.ToString()
        End Select
    End Function

    Private Shared Function 状态颜色(status As 编码任务状态_v6) As Color
        Select Case status
            Case 编码任务状态_v6.未处理 : Return Color.Silver
            Case 编码任务状态_v6.正在处理 : Return Color.YellowGreen
            Case 编码任务状态_v6.已暂停 : Return Color.Goldenrod
            Case 编码任务状态_v6.已完成 : Return Color.FromArgb(210, 132, 184, 72)
            Case 编码任务状态_v6.已停止, 编码任务状态_v6.错误 : Return 界面配色_v6.错误文本色
            Case Else : Return Color.Silver
        End Select
    End Function

    Private Shared Function 默认时间文本(task As 编码任务_v6) As String
        If task.状态 = 编码任务状态_v6.未处理 Then Return ""
        Dim elapsedText = 编码进度_v6.格式化秒(task.任务耗时计时器.Elapsed.TotalSeconds)
        If task.状态 = 编码任务状态_v6.正在处理 AndAlso task.进度.时间文本 <> "" Then Return $"{task.进度.时间文本} - {elapsedText}"
        Return elapsedText
    End Function

    Private Shared Sub 设置完成输出大小(task As 编码任务_v6, item As UltraDetailListView.ListItem)
        If File.Exists(task.输出文件) Then
            Dim outputSize = New FileInfo(task.输出文件).Length
            Dim inputSize As Long = 0
            If File.Exists(task.输入文件) Then inputSize = New FileInfo(task.输入文件).Length
            Dim sizeText = 编码进度_v6.格式化大小KB(CLng(outputSize / 1024))
            If inputSize > 0 Then
                Dim ratio = outputSize / inputSize
                item.SubItems(4).Text = $"{sizeText} ({ratio * 100:F0}%)"
            Else
                item.SubItems(4).Text = sizeText
            End If
            Dim comparisonInputSize = If(inputSize > 0, inputSize, task.输入文件大小)
            If 输出大小超过输入(comparisonInputSize, outputSize) Then item.SubItems(4).ForeColor = 界面配色_v6.错误文本色
            Exit Sub
        End If

        If task.进度.输出大小KB > 0 Then
            item.SubItems(4).Text = 编码进度_v6.格式化大小KB(task.进度.输出大小KB)
            If 输出大小超过输入(task.输入文件大小, CDbl(task.进度.输出大小KB) * 1024.0R) Then item.SubItems(4).ForeColor = 界面配色_v6.错误文本色
        Else
            item.SubItems(4).Text = ""
        End If
    End Sub

    Private Shared Sub 设置进行中大小颜色(task As 编码任务_v6, item As UltraDetailListView.ListItem)
        If 输出大小超过输入(task.输入文件大小, CDbl(task.进度.输出大小KB) * 1024.0R) Then
            item.SubItems(4).ForeColor = 界面配色_v6.错误文本色
        ElseIf 预估输出大小过大(task) Then
            item.SubItems(4).ForeColor = 预估大小警告色
        End If
    End Sub

    Private Shared Function 输出大小超过输入(inputSize As Long, outputSizeBytes As Double) As Boolean
        Return inputSize > 0 AndAlso outputSizeBytes > CDbl(inputSize)
    End Function

    Private Shared Function 预估输出大小过大(task As 编码任务_v6) As Boolean
        If task.输入文件大小 <= 0 OrElse task.进度.输出大小KB <= 0 Then Return False
        Dim percent = task.进度.百分比
        If percent <= 0 OrElse percent >= 1 OrElse Double.IsNaN(percent) OrElse Double.IsInfinity(percent) Then Return False

        ' Compare in bytes without rounding the displayed estimate first.
        Return 输出大小超过输入(task.输入文件大小, CDbl(task.进度.输出大小KB) * 1024.0R / percent)
    End Function

    Private Shared Sub 应用底部日志行(task As 编码任务_v6, item As UltraDetailListView.ListItem)
        item.BottomLines.Clear()
        If 设置_v6.实例对象.编码队列显示最新日志行 <> 0 Then Exit Sub
        If Not 应显示底部日志(task.状态) Then Exit Sub
        If String.IsNullOrWhiteSpace(task.最新底部日志文本) Then Exit Sub

        Dim text = task.最新底部日志文本.Replace(vbCr, " ").Replace(vbLf, " ").Trim()
        If text = "" Then Exit Sub
        Dim color As Color = If(task.最新底部日志是否错误,
                                界面配色_v6.错误文本色,
                                If(界面主题_v6.当前为浅色模式, Color.Black, Color.FromArgb(150, 220, 220, 220)))
        item.BottomLines.Add(New UltraDetailListView.TextLine(text, Nothing, color))
    End Sub

    Private Shared Function 应显示底部日志(status As 编码任务状态_v6) As Boolean
        Return status = 编码任务状态_v6.正在处理 OrElse
               status = 编码任务状态_v6.已暂停 OrElse
               status = 编码任务状态_v6.错误
    End Function

    Private Shared Function 格式化旧版耗时(elapsed As TimeSpan) As String
        Dim parts As New List(Of String)
        If elapsed.Hours > 0 Then parts.Add($"{elapsed.Hours} 时")
        If elapsed.Minutes > 0 OrElse parts.Count > 0 Then parts.Add($"{elapsed.Minutes} 分")
        parts.Add($"{elapsed.Seconds} 秒")
        Return String.Join(" ", parts)
    End Function
End Class
