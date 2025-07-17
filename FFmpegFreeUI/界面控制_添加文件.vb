
Imports Microsoft.WindowsAPICodePack.Dialogs
Public Class 界面控制_添加文件

    Public Shared Sub 加入编码队列()
        If Form1.UiComboBox输出容器.Text = "" Then
            MsgBox("没有选择或填写输出容器！", MsgBoxStyle.Critical)
            Exit Sub
        End If
        Select Case Form1.UiComboBox21.Text
            Case "输出到原目录"
            Case Else
                If Not FileIO.FileSystem.DirectoryExists(Form1.UiComboBox21.Text) Then
                    MsgBox("自定义输出目录不存在！", MsgBoxStyle.Critical)
                    Exit Sub
                End If
        End Select

        Dim a As New 预设数据类型
        预设管理.储存预设(a)

        For Each item As ListViewItem In Form1.ListView2.Items
            Dim m As New 编码任务.单片任务 With {.输入文件 = item.Text, .预设数据 = a}

            Select Case Form1.UiComboBox21.Text
                Case "输出到原目录"
                Case Else
                    m.自定义输出位置 = Form1.UiComboBox21.Text
            End Select
            Dim i2 As New ListViewItem(IO.Path.GetFileName(item.Text))
            i2.SubItems.AddRange("未处理", "", "", "", "", "", "")
            Form1.ListView1.Items.Add(i2)
            m.列表视图项 = i2
            编码任务.队列.Add(m)
        Next
        Form1.ListView2.Items.Clear()
        Form1.UiTabControlMenu1.SelectedTab = Form1.TabPage编码队列
        Task.Run(AddressOf 编码任务.检查是否有可以开始的任务)
    End Sub
    Public Shared Sub 加入编码队列(拖入的文件 As String())
        If Form1.UiComboBox输出容器.Text = "" Then
            MsgBox("没有选择或填写输出容器！", MsgBoxStyle.Critical)
            Exit Sub
        End If
        Select Case Form1.UiComboBox21.Text
            Case "输出到原目录"
            Case Else
                If Not FileIO.FileSystem.DirectoryExists(Form1.UiComboBox21.Text) Then
                    MsgBox("自定义输出目录不存在！", MsgBoxStyle.Critical)
                    Exit Sub
                End If
        End Select
        Dim a As New 预设数据类型
        预设管理.储存预设(a)
        For Each item In 拖入的文件
            Dim m As New 编码任务.单片任务 With {.输入文件 = item, .预设数据 = a}
            If 用户设置.实例对象.转译模式 Then m.输入文件 = 转译模式处理路径(item)

            Select Case Form1.UiComboBox21.Text
                Case "输出到原目录"
                Case Else
                    m.自定义输出位置 = Form1.UiComboBox21.Text
            End Select
            Dim i2 As New ListViewItem(IO.Path.GetFileName(item))
            i2.SubItems.AddRange("未处理", "", "", "", "", "", "")
            Form1.ListView1.Items.Add(i2)
            m.列表视图项 = i2
            编码任务.队列.Add(m)
        Next
        Task.Run(AddressOf 编码任务.检查是否有可以开始的任务)
    End Sub

    Public Shared Sub ListView2_DragEnter(sender As Object, e As DragEventArgs)
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Public Shared Sub ListView2_DragDrop(sender As Object, e As DragEventArgs)
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            Dim a As String() = e.Data.GetData(DataFormats.FileDrop)
            For Each b As String In a
                If 用户设置.实例对象.转译模式 Then b = 转译模式处理路径(b)
                Form1.ListView2.Items.Add(b)
            Next
        End If
    End Sub

    Public Shared Sub 添加文件()
        Dim openFileDialog As New OpenFileDialog With {.Multiselect = True, .Filter = "所有文件|*.*"}
        If openFileDialog.ShowDialog() = DialogResult.OK Then
            For Each filePath As String In openFileDialog.FileNames
                If 用户设置.实例对象.转译模式 Then filePath = 转译模式处理路径(filePath)
                Form1.ListView2.Items.Add(filePath)
            Next
        End If
    End Sub

    Public Shared Sub 打开文件夹添加全部文件()
        Dim dialog As New CommonOpenFileDialog With {.IsFolderPicker = True}
        If dialog.ShowDialog() = CommonFileDialogResult.Ok Then
            Dim folderPath As String = dialog.FileName
            Dim files As List(Of String) = 获取文件夹中的所有文件(folderPath)
            For Each file As String In files
                If 用户设置.实例对象.转译模式 Then file = 转译模式处理路径(file)
                Form1.ListView2.Items.Add(file)
            Next
        End If
    End Sub

    Shared Function 获取文件夹中的所有文件(folderPath As String) As List(Of String)
        Dim fileList As New List(Of String)
        Try
            Dim directoryInfo As New IO.DirectoryInfo(folderPath)
            For Each file As IO.FileInfo In directoryInfo.GetFiles("*", IO.SearchOption.AllDirectories)
                If (file.Attributes And IO.FileAttributes.Hidden) = 0 AndAlso (file.Attributes And IO.FileAttributes.System) = 0 Then
                    fileList.Add(file.FullName)
                End If
            Next
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
        Return fileList
    End Function

    Public Shared Sub 批量移除选中项()
        For Each a As ListViewItem In Form1.ListView2.SelectedItems
            Form1.ListView2.Items.Remove(a)
        Next
    End Sub

    Public Shared Sub 移除全部项()
        Form1.ListView2.Items.Clear()
    End Sub

    Public Shared Sub 选择输出目录()
        If Form1.UiComboBox21.SelectedIndex = 1 Then
            Dim dialog As New CommonOpenFileDialog With {.IsFolderPicker = True}
            If dialog.ShowDialog() = CommonFileDialogResult.Ok Then
                If 用户设置.实例对象.转译模式 Then
                    Form1.UiComboBox21.Text = 转译模式处理路径(dialog.FileName)
                Else
                    Form1.UiComboBox21.Text = dialog.FileName
                End If
            Else
                Form1.UiComboBox21.SelectedIndex = 0
            End If
        End If
    End Sub

End Class
