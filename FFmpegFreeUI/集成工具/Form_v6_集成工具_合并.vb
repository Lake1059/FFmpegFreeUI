Imports System.IO
Imports System.Text
Imports LakeUI

Public Class Form_v6_集成工具_合并

    Private Const 路径列 As Integer = 1

    Private Sub Form_v6_集成工具_合并_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        初始化列表()
        绑定文件拖入(Me)
        绑定文件拖入(ModernPanel1)
        绑定文件拖入(Panel1)
        绑定文件拖入(UltraDetailListView1)
        绑定文件拖入(Panel2)
        调整列表交互区域()
        调整列宽()
    End Sub

    Private Sub 初始化列表()
        UltraDetailListView1.MultiSelect = True
        UltraDetailListView1.AllowDragReorder = True
        If UltraDetailListView1.Columns.Count > 0 Then UltraDetailListView1.Columns(0).WordWrapHeightFixed = True
        If UltraDetailListView1.Columns.Count > 1 Then UltraDetailListView1.Columns(1).WordWrapHeightFixed = True
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
                    result.AddRange(Directory.GetFiles(raw, "*", SearchOption.TopDirectoryOnly).OrderBy(Function(x) x, StringComparer.CurrentCultureIgnoreCase))
                ElseIf File.Exists(raw) Then
                    result.Add(raw)
                End If
            Catch ex As Exception
                ExFloatingTip(UltraDetailListView1, $"读取路径失败：{ex.Message}", 2200)
            End Try
        Next

        Return result
    End Function

    Private Sub 添加文件(files As IEnumerable(Of String))
        If files Is Nothing Then Exit Sub
        Dim fileList = files.Where(Function(x) Not String.IsNullOrWhiteSpace(x) AndAlso File.Exists(x)).ToList()
        If fileList.Count = 0 Then Exit Sub

        UltraDetailListView1.BeginUpdate()
        Try
            For Each file In fileList
                UltraDetailListView1.Items.Add(创建文件项(file))
            Next
        Finally
            UltraDetailListView1.EndUpdate()
        End Try

        If UltraDetailListView1.Items.Count > 0 Then UltraDetailListView1.SelectedIndex = UltraDetailListView1.Items.Count - 1
        调整列宽()
    End Sub

    Private Function 创建文件项(file As String) As UltraDetailListView.ListItem
        Return New UltraDetailListView.ListItem(
            New UltraDetailListView.ListSubItem(Path.GetFileName(file)),
            New UltraDetailListView.ListSubItem(file)
        ) With {.Tag = file}
    End Function

    Private Function 获取项路径(item As UltraDetailListView.ListItem) As String
        If item Is Nothing Then Return ""
        If TypeOf item.Tag Is String Then Return CStr(item.Tag)
        If item.SubItems.Count > 路径列 Then Return item.SubItems(路径列).Text
        If item.SubItems.Count > 0 Then Return item.SubItems(0).Text
        Return ""
    End Function

    Private Sub MB_添加文件_Click(sender As Object, e As EventArgs) Handles MB_添加文件.Click
        Using d As New OpenFileDialog With {.Multiselect = True, .Filter = "所有文件|*.*"}
            If d.ShowDialog(Me) = DialogResult.OK Then 添加文件(d.FileNames)
        End Using
    End Sub

    Private Sub MB_上移_Click(sender As Object, e As EventArgs) Handles MB_上移.Click
        移动选中项(-1)
    End Sub

    Private Sub MB_下移_Click(sender As Object, e As EventArgs) Handles MB_下移.Click
        移动选中项(1)
    End Sub

    Private Sub 移动选中项(direction As Integer)
        Dim selected = UltraDetailListView1.SelectedItems
        If selected.Count = 0 OrElse direction = 0 Then Exit Sub

        Dim selectedSet As New HashSet(Of UltraDetailListView.ListItem)(selected)
        UltraDetailListView1.BeginUpdate()
        Try
            If direction < 0 Then
                For i = 1 To UltraDetailListView1.Items.Count - 1
                    If selectedSet.Contains(UltraDetailListView1.Items(i)) AndAlso Not selectedSet.Contains(UltraDetailListView1.Items(i - 1)) Then
                        Dim moving = UltraDetailListView1.Items(i)
                        UltraDetailListView1.Items.RemoveAt(i)
                        UltraDetailListView1.Items.Insert(i - 1, moving)
                    End If
                Next
            Else
                For i = UltraDetailListView1.Items.Count - 2 To 0 Step -1
                    If selectedSet.Contains(UltraDetailListView1.Items(i)) AndAlso Not selectedSet.Contains(UltraDetailListView1.Items(i + 1)) Then
                        Dim moving = UltraDetailListView1.Items(i)
                        UltraDetailListView1.Items.RemoveAt(i)
                        UltraDetailListView1.Items.Insert(i + 1, moving)
                    End If
                Next
            End If
        Finally
            UltraDetailListView1.EndUpdate()
        End Try
    End Sub

    Private Sub MB_移除选中_Click(sender As Object, e As EventArgs) Handles MB_移除选中.Click
        移除选中项()
    End Sub

    Private Sub 移除选中项()
        Dim selected = UltraDetailListView1.SelectedItems
        If selected.Count = 0 Then Exit Sub
        UltraDetailListView1.BeginUpdate()
        Try
            For Each item In selected
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

    Private Sub MB_选择位置_Click(sender As Object, e As EventArgs) Handles MB_选择位置.Click
        Using d As New SaveFileDialog With {.Filter = "所有文件|*.*"}
            If d.ShowDialog(Me) = DialogResult.OK Then MTB_输出目标文件.Text = d.FileName
        End Using
    End Sub

    Private Sub MB_启动合并_Click(sender As Object, e As EventArgs) Handles MB_启动合并.Click
        If Not 验证任务参数() Then Exit Sub

        Dim inputFiles = UltraDetailListView1.Items.Select(Function(item) 获取项路径(item)).ToList()
        Dim lines = inputFiles.Select(Function(file) $"file '{转义Concat清单路径(file)}'").ToList()
        Dim 清单文件路径 = Path.Combine(Application.StartupPath, $"ffmpeg_concat_demuxer_v6_{Guid.NewGuid():N}.txt")
        File.WriteAllText(清单文件路径, String.Join(vbCrLf, lines), New UTF8Encoding(False))

        Dim output = MTB_输出目标文件.Text.Trim()
        Dim arg = $"-hide_banner -nostdin -f concat -safe 0 -i {引用参数(清单文件路径)} -c copy {引用参数(output)} -y"

        插件管理.使用命令行添加任务到编码队列(arg, $"合并任务 {Now:HHmmss}", output, inputFiles.FirstOrDefault())
        FormMain_v6.ModernTabListControl1.SelectedIndex = 2
        ExFloatingTip(MB_启动合并, "已添加到编码队列", 1200)
    End Sub

    Private Function 验证任务参数() As Boolean
        If UltraDetailListView1.Items.Count = 0 Then
            ExFloatingTip(UltraDetailListView1, "请先添加要合并的文件", 1800)
            Return False
        End If
        If UltraDetailListView1.Items.Count < 2 Then
            ExFloatingTip(UltraDetailListView1, "合并至少需要两个文件", 1800)
            Return False
        End If

        Dim missing = 查找缺失文件()
        If missing <> "" Then
            ExFloatingTip(UltraDetailListView1, $"文件不存在：{missing}", 2600)
            Return False
        End If

        Dim output = MTB_输出目标文件.Text.Trim()
        If output = "" Then
            ExFloatingTip(MTB_输出目标文件, "请设置输出目标文件", 1800)
            Return False
        End If

        Dim outputDir = Path.GetDirectoryName(output)
        If outputDir <> "" AndAlso Not Directory.Exists(outputDir) Then
            ExFloatingTip(MTB_输出目标文件, "输出目录不存在", 1800)
            Return False
        End If

        Return True
    End Function

    Private Function 查找缺失文件() As String
        For Each item In UltraDetailListView1.Items
            Dim pathValue = 获取项路径(item)
            If Not File.Exists(pathValue) Then Return pathValue
        Next
        Return ""
    End Function

    Private Shared Function 转义Concat清单路径(file As String) As String
        Dim value = file.Replace("\", "\\")
        If value.StartsWith("\\\\", StringComparison.Ordinal) Then value = String.Concat("\\", value.AsSpan(4))
        Return value.Replace("'", "'\''")
    End Function

    Private Shared Function 引用参数(value As String) As String
        Return $"""{If(value, "").Replace("""", "\""")}"""
    End Function

    Private Sub UltraDetailListView1_KeyDown(sender As Object, e As KeyEventArgs) Handles UltraDetailListView1.KeyDown
        Select Case e.KeyCode
            Case Keys.F3
                移动选中项(-1)
                e.Handled = True
            Case Keys.F4
                移动选中项(1)
                e.Handled = True
            Case Keys.Delete
                移除选中项()
                e.Handled = True
        End Select
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
        If UltraDetailListView1.Columns.Count < 2 OrElse UltraDetailListView1.ClientSize.Width <= 0 Then Exit Sub

        Dim available = UltraDetailListView1.ClientSize.Width - UltraDetailListView1.Padding.Left - UltraDetailListView1.Padding.Right - CInt(30 * 获取Dpi比例())
        If available <= 120 Then Exit Sub

        Dim nameWidth = Math.Max(CInt(180 * 获取Dpi比例()), 测量列宽(0))
        Dim pathWidth = Math.Max(CInt(260 * 获取Dpi比例()), 测量列宽(1))
        Dim total = nameWidth + pathWidth

        If total > available Then
            Dim fixedName = Math.Max(CInt(160 * 获取Dpi比例()), Math.Min(nameWidth, available \ 2))
            UltraDetailListView1.Columns(0).Width = fixedName
            UltraDetailListView1.Columns(1).Width = Math.Max(CInt(180 * 获取Dpi比例()), available - fixedName)
        Else
            UltraDetailListView1.Columns(0).Width = nameWidth
            UltraDetailListView1.Columns(1).Width = Math.Max(pathWidth, available - nameWidth)
        End If
    End Sub

    Private Function 测量列宽(columnIndex As Integer) As Integer
        Dim maxWidth As Integer = TextRenderer.MeasureText(UltraDetailListView1.Columns(columnIndex).Text, UltraDetailListView1.Font).Width + CInt(40 * 获取Dpi比例())
        For Each item In UltraDetailListView1.Items
            If item.SubItems.Count <= columnIndex Then Continue For
            Dim width = TextRenderer.MeasureText(item.SubItems(columnIndex).Text, UltraDetailListView1.Font).Width + CInt(40 * 获取Dpi比例())
            If width > maxWidth Then maxWidth = width
        Next
        Return Math.Min(maxWidth, CInt(UltraDetailListView1.ClientSize.Width * 0.65))
    End Function

    Private Function 获取Dpi比例() As Single
        Return CSng(DeviceDpi) / 96.0F
    End Function

End Class
