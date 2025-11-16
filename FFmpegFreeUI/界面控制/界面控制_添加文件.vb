Imports Microsoft.WindowsAPICodePack.Dialogs
Public Class 界面控制_添加文件

    ' 提取公共验证逻辑
    Private Shared Function 验证参数面板(参数面板 As 界面_常规流程参数_V2, 父窗体 As Form) As Boolean
        If 参数面板.UiTextBox输出容器.Text = "" And Not 参数面板.UiSwitch不使用输出文件参数.Active And 参数面板.UiTextBox完全自己写参数.Text = "" Then
            软件内对话框.显示对话框(父窗体, "输出容器未填写", $"没有选择或填写输出容器！{vbCrLf & vbCrLf}如果设定了自动加载预设请先点开参数页面检查", New Dictionary(Of String, Action) From {{"了解", Nothing}}, 软件内对话框.主题类型.错误)
            Return False
        End If

        If 参数面板.UiComboBox输出目录.SelectedIndex > 0 AndAlso Not FileIO.FileSystem.DirectoryExists(参数面板.UiComboBox输出目录.Text.Trim) Then
            软件内对话框.显示对话框(父窗体, "输出位置错误", "自定义输出目录不存在！", New Dictionary(Of String, Action) From {{"了解", Nothing}}, 软件内对话框.主题类型.错误)
            Return False
        End If

        If Not 参数面板.是否已初始化 Then
            软件内对话框.显示对话框(父窗体, "启动后请切到参数面板一次", "参数面板未初始化，这是选项卡控件的底层机制问题。", New Dictionary(Of String, Action) From {{"了解", Nothing}}, 软件内对话框.主题类型.错误)
            Return False
        End If

        Return True
    End Function

    ' 提取添加单个任务的逻辑
    Private Shared Sub 添加单个任务(文件路径 As String, 预设数据 As 预设数据类型, 输出目录 As String, 是否转译模式 As Boolean)
        Dim 最终路径 = If(是否转译模式, 转译模式处理路径(文件路径), 文件路径)
        Dim m As New 编码任务.单片任务 With {.输入文件 = 最终路径, .预设数据 = 预设数据}

        If 输出目录 <> Form1.常规流程参数页面.UiComboBox输出目录.Items(0).Trim Then
            m.自定义输出位置 = 输出目录
        End If

        Dim i2 As New ListViewItem(IO.Path.GetFileName(文件路径))
        If 用户设置.实例对象.混淆任务名称 = 1 Then i2.Text = 混淆字符_喵(i2.Text)
        i2.SubItems.AddRange("未处理", "", "", "", "", "", "")
        Form1.ListView1.Items.Add(i2)
        m.列表视图项 = i2
        编码任务.队列.Add(m)
    End Sub

    Public Shared Sub 加入编码队列()
        If Not 验证参数面板(Form1.常规流程参数页面, Form1) Then Exit Sub

        Dim a As New 预设数据类型
        预设管理.储存预设(a, Form1.常规流程参数页面)
        Dim 输出目录 = Form1.常规流程参数页面.UiComboBox输出目录.Text.Trim

        For Each item As ListViewItem In Form1.ListView2.Items
            添加单个任务(item.Text, a, 输出目录, False)
        Next

        Form1.ListView2.Items.Clear()
        Form1.UiTabControlMenu1.SelectedTab = Form1.TabPage编码队列
        编码任务.检查并开始新任务的定时器.Enabled = True
    End Sub

    Public Shared Function 加入编码队列(拖入的文件 As String(), 参数面板 As 界面_常规流程参数_V2) As Boolean
        If Not 验证参数面板(参数面板, 参数面板.ParentForm) Then Return False
        Dim a As New 预设数据类型
        预设管理.储存预设(a, 参数面板)
        Dim 输出目录 = 参数面板.UiComboBox输出目录.Text.Trim
        For Each item In 拖入的文件
            添加单个任务(item, a, 输出目录, 用户设置.实例对象.转译模式)
        Next
        编码任务.检查并开始新任务的定时器.Enabled = True
        Return True
    End Function

    Public Shared Sub ListView2_DragEnter(sender As Object, e As DragEventArgs)
        e.Effect = If(e.Data.GetDataPresent(DataFormats.FileDrop), DragDropEffects.Copy, DragDropEffects.None)
    End Sub

    Public Shared Sub ListView2_DragDrop(sender As Object, e As DragEventArgs)
        If Not e.Data.GetDataPresent(DataFormats.FileDrop) Then Exit Sub

        Dim files As String() = e.Data.GetData(DataFormats.FileDrop)
        For Each b As String In files
            Dim 路径 = If(用户设置.实例对象.转译模式, 转译模式处理路径(b), b)
            Form1.ListView2.Items.Add(路径)
        Next
    End Sub

    Public Shared Sub 添加文件()
        Dim openFileDialog As New OpenFileDialog With {.Multiselect = True, .Filter = "所有文件|*.*"}
        If openFileDialog.ShowDialog() <> DialogResult.OK Then Exit Sub

        For Each filePath As String In openFileDialog.FileNames
            Dim 路径 = If(用户设置.实例对象.转译模式, 转译模式处理路径(filePath), filePath)
            Form1.ListView2.Items.Add(路径)
        Next
    End Sub

    Public Shared Sub 打开文件夹添加全部文件()
        Dim dialog As New CommonOpenFileDialog With {.IsFolderPicker = True}
        If dialog.ShowDialog() <> CommonFileDialogResult.Ok Then Exit Sub

        For Each file As String In 获取文件夹中的所有文件(dialog.FileName)
            Dim 路径 = If(用户设置.实例对象.转译模式, 转译模式处理路径(file), file)
            Form1.ListView2.Items.Add(路径)
        Next
    End Sub

    Shared Function 获取文件夹中的所有文件(folderPath As String) As List(Of String)
        Dim fileList As New List(Of String)
        Try
            For Each file As IO.FileInfo In New IO.DirectoryInfo(folderPath).GetFiles("*", IO.SearchOption.AllDirectories)
                If (file.Attributes And (IO.FileAttributes.Hidden Or IO.FileAttributes.System)) = 0 Then
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

End Class
