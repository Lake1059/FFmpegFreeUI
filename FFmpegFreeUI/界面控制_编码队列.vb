Imports System.IO
Imports System.Text.Json

Public Class 界面控制_编码队列

    Public Shared Sub 开始任务()
        For Each item As ListViewItem In Form1.ListView1.SelectedItems
            Dim i = item.Index
            Select Case 编码任务.队列(i).状态
                Case 编码任务.编码状态.未处理, 编码任务.编码状态.已停止, 编码任务.编码状态.错误
                    编码任务.队列(i).开始()
            End Select
        Next
    End Sub

    Public Shared Sub 暂停任务()
        For Each item As ListViewItem In Form1.ListView1.SelectedItems
            Dim i = item.Index
            Select Case 编码任务.队列(i).状态
                Case 编码任务.编码状态.正在处理
                    编码任务.队列(i).暂停()
            End Select
        Next
    End Sub

    Public Shared Sub 恢复任务()
        For Each item As ListViewItem In Form1.ListView1.SelectedItems
            Dim i = item.Index
            Select Case 编码任务.队列(i).状态
                Case 编码任务.编码状态.已暂停
                    编码任务.队列(i).恢复()
            End Select
        Next
    End Sub

    Public Shared Sub 停止任务()
        For Each item As ListViewItem In Form1.ListView1.SelectedItems
            Dim i = item.Index
            Select Case 编码任务.队列(i).状态
                Case 编码任务.编码状态.正在处理, 编码任务.编码状态.已暂停, 编码任务.编码状态.错误
                    编码任务.队列(i).停止()
            End Select
        Next
    End Sub

    Public Shared Sub 移除任务()
        Dim indices As New List(Of Integer)
        For Each item As ListViewItem In Form1.ListView1.SelectedItems
            Dim i = item.Index
            Select Case 编码任务.队列(i).状态
                Case 编码任务.编码状态.已完成, 编码任务.编码状态.未处理, 编码任务.编码状态.错误, 编码任务.编码状态.已停止
                    indices.Add(i)
            End Select
        Next
        indices.Sort()
        indices.Reverse()
        For Each i In indices
            编码任务.队列(i).清除占用()
            编码任务.队列.RemoveAt(i)
        Next
    End Sub

    Public Shared Sub 重置任务()
        For Each item As ListViewItem In Form1.ListView1.SelectedItems
            Dim i = item.Index
            Select Case 编码任务.队列(i).状态
                Case 编码任务.编码状态.已完成, 编码任务.编码状态.错误, 编码任务.编码状态.已停止
                    编码任务.队列(i).状态 = 编码任务.编码状态.未处理
                    编码任务.队列(i).状态刷新统一逻辑()
            End Select
        Next
    End Sub

    Public Shared Sub 全选任务()
        For Each item As ListViewItem In Form1.ListView1.Items
            item.Selected = True
        Next
    End Sub

    Public Shared Sub 定位输出()
        If Form1.ListView1.SelectedItems.Count <> 1 Then Exit Sub
        Dim 输出文件 = 编码任务.队列(Form1.ListView1.SelectedItems(0).Index).输出文件
        If String.IsNullOrEmpty(输出文件) OrElse Not IO.File.Exists(输出文件) Then Exit Sub
        Process.Start("explorer", "/select,""" & 输出文件 & """")
    End Sub

    Public Shared Sub 重新配置()
        If Form1.ListView1.SelectedItems.Count <> 1 Then Exit Sub
        If 编码任务.队列(Form1.ListView1.SelectedItems(0).Index).预设数据 Is Nothing Then
            MsgBox("此任务不包含 3FUI 的预设数据，一般是由其他程序添加的，这样不能使用这个功能。", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If MsgBox("确定将此任务的配置数据用于覆盖几个选项卡中的设置？此操作不可逆！", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            预设管理.显示预设(编码任务.队列(Form1.ListView1.SelectedItems(0).Index).预设数据)
            Dim 输入文件 = 编码任务.队列(Form1.ListView1.SelectedItems(0).Index).输入文件
            Dim 已存在 As Boolean = False
            For Each item As ListViewItem In Form1.ListView2.Items
                If item.Text = 输入文件 Then
                    已存在 = True
                    Exit For
                End If
            Next
            If Not 已存在 Then Form1.ListView2.Items.Add(输入文件)
        End If
    End Sub

    Public Shared Sub 重新添加()
        If Form1.ListView1.SelectedItems.Count <> 1 Then Exit Sub
        For i = 0 To Form1.ListView1.SelectedItems.Count - 1
            If 编码任务.队列(Form1.ListView1.SelectedItems(i).Index).预设数据 Is Nothing Then
                MsgBox("此任务不包含 3FUI 的预设数据，一般是由其他程序添加的，这样不能使用这个功能。", MsgBoxStyle.Exclamation)
                Continue For
            End If
            For Each item As ListViewItem In Form1.ListView2.Items
                If item.Text = 编码任务.队列(Form1.ListView1.SelectedItems(i).Index).输入文件 Then
                    GoTo jx1
                End If
            Next
            Form1.ListView2.Items.Add(编码任务.队列(Form1.ListView1.SelectedItems(i).Index).输入文件)
jx1:
        Next
    End Sub

    Public Shared Sub 导出配置()
        If Form1.ListView1.SelectedItems.Count <> 1 Then Exit Sub
        If 编码任务.队列(Form1.ListView1.SelectedItems(0).Index).预设数据 Is Nothing Then
            MsgBox("此任务不包含 3FUI 的预设数据，一般是由其他程序添加的，这样不能使用这个功能。", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        Dim d As New SaveFileDialog With {.Filter = "Json|*.json"}
        d.ShowDialog(Form1)
        If d.FileName = "" Then Exit Sub
        File.WriteAllText(d.FileName, JsonSerializer.Serialize(编码任务.队列(Form1.ListView1.SelectedItems(0).Index).预设数据, JsonSO))
    End Sub

    Public Shared Sub 复制命令行()
        If Form1.ListView1.SelectedItems.Count <> 1 Then Exit Sub
        If 编码任务.队列(Form1.ListView1.SelectedItems(0).Index).命令行 = "" Then
            Clipboard.SetText("ffmpeg " & 预设管理.将预设数据转换为命令行(编码任务.队列(Form1.ListView1.SelectedItems(0).Index).预设数据, 编码任务.队列(Form1.ListView1.SelectedItems(0).Index).输入文件, 编码任务.队列(Form1.ListView1.SelectedItems(0).Index).输出文件))
        Else
            Clipboard.SetText("ffmpeg " & 编码任务.队列(Form1.ListView1.SelectedItems(0).Index).命令行)
        End If
    End Sub

End Class
