Imports System.Globalization
Imports System.IO
Imports LakeUI
Imports Microsoft.WindowsAPICodePack.Dialogs

Public Class Form_v6_准备文件

    Private Const 文件名列 As Integer = 0
    Private Const 路径列 As Integer = 1
    Private Const 后缀列 As Integer = 2
    Private Const 大小列 As Integer = 3

    Private 正在重置排序选择 As Boolean = False

    Private Sub Form_v6_准备文件_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        初始化列表()
        初始化排序下拉框()

        绑定文件拖入(Me)
        绑定文件拖入(ModernPanel1)
        绑定文件拖入(Panel1)
        绑定文件拖入(UltraDetailListView1)
        绑定文件拖入(MB_加入编码队列)
        绑定文件拖入(MB_添加文件)
        绑定文件拖入(MB_添加文件夹及子目录)
        绑定文件拖入(MB_移除选中)
        绑定文件拖入(MB_移除全部)

        调整列表交互区域()
        调整列宽()
    End Sub

    Private Sub 初始化列表()
        UltraDetailListView1.MultiSelect = True
        UltraDetailListView1.AllowDragReorder = True
        For Each column In UltraDetailListView1.Columns
            column.WordWrapHeightFixed = True
        Next
    End Sub

    Private Sub 初始化排序下拉框()
        MCB_排序.Items.Clear()
        MCB_排序.Items.Add("列表视图的升序")
        MCB_排序.Items.Add("列表视图的降序")
        MCB_排序.Items.Add("按照文件大小升序")
        MCB_排序.Items.Add("按照文件大小降序")
        MCB_排序.SelectedIndex = -1
        MCB_排序.Text = ""
    End Sub

    Private Sub 绑定文件拖入(target As Control)
        If target Is Nothing Then Exit Sub
        target.AllowDrop = True
        RemoveHandler target.DragEnter, AddressOf 文件拖入事件
        RemoveHandler target.DragDrop, AddressOf 文件放下事件
        AddHandler target.DragEnter, AddressOf 文件拖入事件
        AddHandler target.DragDrop, AddressOf 文件放下事件
    End Sub

    Private Sub 文件拖入事件(sender As Object, e As DragEventArgs)
        e.Effect = If(获取拖入文件(e).Count > 0, DragDropEffects.Copy, DragDropEffects.None)
    End Sub

    Private Sub 文件放下事件(sender As Object, e As DragEventArgs)
        添加文件(获取拖入文件(e))
    End Sub

    Private Function 获取拖入文件(e As DragEventArgs) As List(Of String)
        If e Is Nothing OrElse e.Data Is Nothing OrElse Not e.Data.GetDataPresent(DataFormats.FileDrop) Then Return New List(Of String)
        Dim paths = TryCast(e.Data.GetData(DataFormats.FileDrop), String())
        If paths Is Nothing Then Return New List(Of String)
        Return 展开文件路径(paths)
    End Function

    Private Function 展开文件路径(paths As IEnumerable(Of String)) As List(Of String)
        Dim result As New List(Of String)
        If paths Is Nothing Then Return result

        For Each raw In paths
            If String.IsNullOrWhiteSpace(raw) Then Continue For
            Try
                If Directory.Exists(raw) Then
                    result.AddRange(获取文件夹中的所有文件(raw))
                ElseIf File.Exists(raw) Then
                    result.Add(raw)
                End If
            Catch ex As Exception
                ExFloatingTip(UltraDetailListView1, $"读取路径失败：{ex.Message}", 2200)
            End Try
        Next

        Return result
    End Function

    Private Function 获取文件夹中的所有文件(folderPath As String) As List(Of String)
        Dim result As New List(Of String)
        If String.IsNullOrWhiteSpace(folderPath) OrElse Not Directory.Exists(folderPath) Then Return result

        Dim pending As New Stack(Of String)
        pending.Push(folderPath)

        Do While pending.Count > 0
            Dim current = pending.Pop()
            Dim dirInfo As DirectoryInfo = Nothing

            Try
                dirInfo = New DirectoryInfo(current)
                If (dirInfo.Attributes And (FileAttributes.Hidden Or FileAttributes.System)) <> 0 Then Continue Do
            Catch ex As Exception
                ExFloatingTip(UltraDetailListView1, $"读取路径失败：{ex.Message}", 2200)
                Continue Do
            End Try

            Try
                For Each fileInfo In dirInfo.GetFiles("*").OrderBy(Function(x) x.FullName, StringComparer.CurrentCultureIgnoreCase)
                    If (fileInfo.Attributes And (FileAttributes.Hidden Or FileAttributes.System)) = 0 Then result.Add(fileInfo.FullName)
                Next
            Catch ex As Exception
                ExFloatingTip(UltraDetailListView1, $"读取文件失败：{ex.Message}", 2200)
            End Try

            Try
                For Each child In dirInfo.GetDirectories("*").OrderByDescending(Function(x) x.FullName, StringComparer.CurrentCultureIgnoreCase)
                    pending.Push(child.FullName)
                Next
            Catch ex As Exception
                ExFloatingTip(UltraDetailListView1, $"读取子目录失败：{ex.Message}", 2200)
            End Try
        Loop

        Return result
    End Function

    Private Sub 添加文件(files As IEnumerable(Of String))
        If files Is Nothing Then Exit Sub

        Dim fileList = files.
            Where(Function(x) Not String.IsNullOrWhiteSpace(x) AndAlso File.Exists(x)).
            Select(Function(x) Path.GetFullPath(x)).
            ToList()

        If fileList.Count = 0 Then Exit Sub

        UltraDetailListView1.BeginUpdate()
        Try
            For Each file In fileList
                Try
                    UltraDetailListView1.Items.Add(创建文件项(file))
                Catch ex As Exception
                    ExFloatingTip(UltraDetailListView1, $"添加文件失败：{ex.Message}", 2200)
                End Try
            Next
        Finally
            UltraDetailListView1.EndUpdate()
        End Try

        If UltraDetailListView1.Items.Count > 0 Then UltraDetailListView1.SelectedIndex = UltraDetailListView1.Items.Count - 1
        调整列宽()
    End Sub

    Public Function Agent获取文件列表() As List(Of String)
        Return UltraDetailListView1.Items.Select(Function(item) 获取项路径(item)).Where(Function(x) x <> "").ToList()
    End Function

    Public Function Agent设置文件列表(files As IEnumerable(Of String), mode As String) As String
        Select Case If(mode, "").Trim().ToLowerInvariant()
            Case "clear", "清空"
                UltraDetailListView1.Items.Clear()
            Case "replace", "替换"
                UltraDetailListView1.Items.Clear()
                添加文件(files)
            Case Else
                添加文件(files)
        End Select
        调整列宽()
        Return $"准备文件列表：{UltraDetailListView1.Items.Count} 个文件"
    End Function

    Public Function Agent加入编码队列() As String
        Dim beforeCount = UltraDetailListView1.Items.Count
        MB_加入编码队列_Click(MB_加入编码队列, EventArgs.Empty)
        Return $"已请求将准备文件加入编码队列，原列表 {beforeCount} 个文件，当前剩余 {UltraDetailListView1.Items.Count} 个文件"
    End Function

    Private Function 创建文件项(file As String) As UltraDetailListView.ListItem
        Dim info As New FileInfo(file)
        Return New UltraDetailListView.ListItem(
            New UltraDetailListView.ListSubItem(info.Name),
            New UltraDetailListView.ListSubItem(If(info.DirectoryName, "")),
            New UltraDetailListView.ListSubItem(Path.GetExtension(info.FullName).TrimStart("."c).ToUpperInvariant()),
            New UltraDetailListView.ListSubItem(格式化文件大小(info.Length))
        ) With {.Tag = info.FullName}
    End Function

    Private Function 获取项路径(item As UltraDetailListView.ListItem) As String
        If item Is Nothing Then Return ""
        If TypeOf item.Tag Is String Then Return CStr(item.Tag)
        If item.SubItems.Count > 路径列 Then
            Dim dir = item.SubItems(路径列).Text
            Dim name = 获取子项文本(item, 文件名列)
            If dir <> "" AndAlso name <> "" Then Return Path.Combine(dir, name)
        End If
        Return 获取子项文本(item, 文件名列)
    End Function

    Private Shared Function 获取子项文本(item As UltraDetailListView.ListItem, index As Integer) As String
        If item Is Nothing OrElse item.SubItems.Count <= index Then Return ""
        Return If(item.SubItems(index).Text, "")
    End Function

    Private Function 获取项大小(item As UltraDetailListView.ListItem) As Long
        Try
            Dim pathValue = 获取项路径(item)
            If pathValue = "" OrElse Not File.Exists(pathValue) Then Return -1
            Return New FileInfo(pathValue).Length
        Catch
            Return -1
        End Try
    End Function

    Private Sub 应用排序(sortIndex As Integer)
        If UltraDetailListView1.Items.Count < 2 Then Exit Sub

        Dim items As New List(Of UltraDetailListView.ListItem)
        For Each item In UltraDetailListView1.Items
            items.Add(item)
        Next

        Select Case sortIndex
            Case 0
                items = items.
                    OrderBy(Function(x) 获取子项文本(x, 文件名列), StringComparer.CurrentCultureIgnoreCase).
                    ThenBy(Function(x) 获取项路径(x), StringComparer.CurrentCultureIgnoreCase).
                    ToList()
            Case 1
                items = items.
                    OrderByDescending(Function(x) 获取子项文本(x, 文件名列), StringComparer.CurrentCultureIgnoreCase).
                    ThenBy(Function(x) 获取项路径(x), StringComparer.CurrentCultureIgnoreCase).
                    ToList()
            Case 2
                items = items.
                    OrderBy(Function(x)
                                Dim size = 获取项大小(x)
                                Return If(size >= 0, size, Long.MaxValue)
                            End Function).
                    ThenBy(Function(x) 获取项路径(x), StringComparer.CurrentCultureIgnoreCase).
                    ToList()
            Case 3
                items = items.
                    OrderByDescending(Function(x)
                                          Dim size = 获取项大小(x)
                                          Return If(size >= 0, size, Long.MinValue)
                                      End Function).
                    ThenBy(Function(x) 获取项路径(x), StringComparer.CurrentCultureIgnoreCase).
                    ToList()
            Case Else
                Exit Sub
        End Select

        UltraDetailListView1.BeginUpdate()
        Try
            UltraDetailListView1.Items.Clear()
            For Each item In items
                UltraDetailListView1.Items.Add(item)
            Next
        Finally
            UltraDetailListView1.EndUpdate()
        End Try

        调整列宽()
    End Sub

    Private Shared Function 格式化文件大小(bytes As Long) As String
        If bytes < 0 Then Return "未知"
        If bytes = 0 Then Return "0K"

        Const K As Double = 1024.0
        Const M As Double = K * 1024.0
        Const G As Double = M * 1024.0

        If bytes >= CLng(G) Then Return 格式化文件大小数值(bytes / G) & "G"
        If bytes >= CLng(M) Then Return 格式化文件大小数值(bytes / M) & "M"
        Return 格式化文件大小数值(Math.Max(1.0, bytes / K)) & "K"
    End Function

    Private Shared Function 格式化文件大小数值(value As Double) As String
        If value >= 100 Then Return value.ToString("0", CultureInfo.InvariantCulture)
        If value >= 10 Then Return value.ToString("0.#", CultureInfo.InvariantCulture)
        Return value.ToString("0.##", CultureInfo.InvariantCulture)
    End Function

    Private Sub MB_添加文件_Click(sender As Object, e As EventArgs) Handles MB_添加文件.Click
        Using d As New OpenFileDialog With {.Multiselect = True, .Filter = "所有文件|*.*"}
            If d.ShowDialog(Me) = DialogResult.OK Then 添加文件(d.FileNames)
        End Using
    End Sub

    Private Sub MB_添加文件夹及子目录_Click(sender As Object, e As EventArgs) Handles MB_添加文件夹及子目录.Click
        Using d As New CommonOpenFileDialog With {.IsFolderPicker = True}
            If d.ShowDialog() = CommonFileDialogResult.Ok Then 添加文件(获取文件夹中的所有文件(d.FileName))
        End Using
    End Sub

    Private Sub MB_移除选中_Click(sender As Object, e As EventArgs) Handles MB_移除选中.Click
        移除选中项()
    End Sub

    Private Sub 移除选中项()
        Dim selected = UltraDetailListView1.SelectedItems
        If selected.Count = 0 Then Exit Sub

        UltraDetailListView1.BeginUpdate()
        Try
            For Each item In selected.ToArray()
                UltraDetailListView1.Items.Remove(item)
            Next
        Finally
            UltraDetailListView1.EndUpdate()
        End Try

        调整列宽()
    End Sub

    Private Sub MB_移除全部_Click(sender As Object, e As EventArgs) Handles MB_移除全部.Click
        If UltraDetailListView1.Items.Count = 0 Then Exit Sub
        UltraDetailListView1.Items.Clear()
        调整列宽()
    End Sub

    Private Sub MB_加入编码队列_Click(sender As Object, e As EventArgs) Handles MB_加入编码队列.Click
        If UltraDetailListView1.Items.Count = 0 Then
            ExFloatingTip(UltraDetailListView1, "请先添加文件", 1800)
            Exit Sub
        End If

        Dim missing = 查找缺失文件()
        If missing <> "" Then
            ExFloatingTip(UltraDetailListView1, $"文件不存在：{missing}", 2600)
            Exit Sub
        End If

        Dim files = UltraDetailListView1.Items.Select(Function(item) 获取项路径(item)).ToList()
        Dim count = 编码队列_v6.添加来自参数面板的文件(files, Form_v6_参数面板)
        If count <= 0 Then Exit Sub

        UltraDetailListView1.Items.Clear()
        调整列宽()
        FormMain_v6.ModernTabListControl1.SelectedIndex = 2
        ExFloatingTip(Form_v6_编码队列.UltraDetailListView1, $"已添加 {count} 个任务", 1200)
    End Sub

    Private Function 查找缺失文件() As String
        For Each item In UltraDetailListView1.Items
            Dim pathValue = 获取项路径(item)
            If Not File.Exists(pathValue) Then Return pathValue
        Next
        Return ""
    End Function

    Private Sub MCB_排序_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_排序.SelectedIndexChanged
        If 正在重置排序选择 OrElse MCB_排序.SelectedIndex < 0 Then Exit Sub

        应用排序(MCB_排序.SelectedIndex)

        正在重置排序选择 = True
        Try
            MCB_排序.SelectedIndex = -1
            MCB_排序.Text = ""
        Finally
            正在重置排序选择 = False
        End Try
    End Sub

    Private Sub UltraDetailListView1_KeyDown(sender As Object, e As KeyEventArgs) Handles UltraDetailListView1.KeyDown
        If e.KeyCode = Keys.Delete Then
            移除选中项()
            e.Handled = True
        End If
    End Sub

    Private Sub UltraDetailListView1_ItemOrderChanged(sender As Object, e As EventArgs) Handles UltraDetailListView1.ItemOrderChanged
        调整列宽()
    End Sub

    Private Sub UltraDetailListView1_SizeChanged(sender As Object, e As EventArgs) Handles UltraDetailListView1.SizeChanged
        调整列表交互区域()
        调整列宽()
    End Sub

    Private Sub 调整列表交互区域()
        UltraDetailListView1.DragSelectZoneWidth = Math.Max(0, UltraDetailListView1.Size.Width \ 2)
    End Sub

    Private Sub 调整列宽()
        If UltraDetailListView1.Columns.Count < 4 OrElse UltraDetailListView1.ClientSize.Width <= 0 Then Exit Sub

        Dim scale = 获取Dpi比例()
        Dim available = UltraDetailListView1.ClientSize.Width - UltraDetailListView1.Padding.Left - UltraDetailListView1.Padding.Right - CInt(30 * scale)
        If available <= 180 Then Exit Sub

        Dim suffixWidth = 限制范围(测量列宽(后缀列), CInt(70 * scale), CInt(130 * scale))
        Dim sizeWidth = 限制范围(测量列宽(大小列), CInt(70 * scale), CInt(120 * scale))
        Dim remaining = available - suffixWidth - sizeWidth
        If remaining <= CInt(160 * scale) Then
            UltraDetailListView1.Columns(文件名列).Width = CInt(90 * scale)
            UltraDetailListView1.Columns(路径列).Width = Math.Max(CInt(90 * scale), remaining - CInt(90 * scale))
            UltraDetailListView1.Columns(后缀列).Width = suffixWidth
            UltraDetailListView1.Columns(大小列).Width = sizeWidth
            Exit Sub
        End If

        Dim nameMeasured = 测量列宽(文件名列)
        Dim nameMin = CInt(160 * scale)
        Dim pathMin = CInt(220 * scale)
        Dim nameMax = Math.Max(nameMin, CInt(remaining * 0.48))
        Dim nameWidth = Math.Min(Math.Max(nameMeasured, nameMin), nameMax)

        If remaining - nameWidth < pathMin Then nameWidth = Math.Max(nameMin, remaining - pathMin)
        Dim pathWidth = Math.Max(pathMin, remaining - nameWidth)

        UltraDetailListView1.Columns(文件名列).Width = nameWidth
        UltraDetailListView1.Columns(路径列).Width = pathWidth
        UltraDetailListView1.Columns(后缀列).Width = suffixWidth
        UltraDetailListView1.Columns(大小列).Width = sizeWidth
    End Sub

    Private Function 测量列宽(columnIndex As Integer) As Integer
        Dim maxWidth As Integer = TextRenderer.MeasureText(UltraDetailListView1.Columns(columnIndex).Text, UltraDetailListView1.Font).Width + CInt(36 * 获取Dpi比例())
        For Each item In UltraDetailListView1.Items
            If item.SubItems.Count <= columnIndex Then Continue For
            Dim width = TextRenderer.MeasureText(获取子项文本(item, columnIndex), UltraDetailListView1.Font).Width + CInt(36 * 获取Dpi比例())
            If width > maxWidth Then maxWidth = width
        Next
        Return Math.Min(maxWidth, CInt(UltraDetailListView1.ClientSize.Width * 0.7))
    End Function

    Private Function 获取Dpi比例() As Single
        Return CSng(DeviceDpi) / 96.0F
    End Function

    Private Shared Function 限制范围(value As Integer, minValue As Integer, maxValue As Integer) As Integer
        Return Math.Min(Math.Max(value, minValue), maxValue)
    End Function

End Class
