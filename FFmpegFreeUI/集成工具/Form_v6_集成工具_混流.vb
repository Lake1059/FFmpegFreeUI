Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions
Imports LakeUI

Public Class Form_v6_集成工具_混流

    Private Const 文件列 As Integer = 0
    Private Const 视频列 As Integer = 1
    Private Const 音频列 As Integer = 2
    Private Const 字幕列 As Integer = 3
    Private Const 章节列 As Integer = 4
    Private Const 元数据列 As Integer = 5
    Private Const 使用此 As String = "使用此"

    Private 正在同步控件 As Boolean = False

    Private Sub Form_v6_集成工具_混流_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        初始化列表()
        绑定文件拖入(Me)
        绑定文件拖入(ModernPanel1)
        绑定文件拖入(Panel1)
        绑定文件拖入(UltraDetailListView1)
        绑定文件拖入(Panel2)
        绑定文件拖入(Panel3)
        绑定文件拖入(Panel4)
        调整列表交互区域()
        调整列宽()
    End Sub

    Private Sub 初始化列表()
        UltraDetailListView1.MultiSelect = True
        UltraDetailListView1.AllowDragReorder = True
        For Each column In UltraDetailListView1.Columns
            column.WordWrapHeightFixed = True
        Next
        MCB_使用此文件的章节.ClickAnywhere = True
        MCB_使用此文件的元数据.ClickAnywhere = True
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
            New UltraDetailListView.ListSubItem(""),
            New UltraDetailListView.ListSubItem(""),
            New UltraDetailListView.ListSubItem(""),
            New UltraDetailListView.ListSubItem(""),
            New UltraDetailListView.ListSubItem("")
        ) With {.Tag = file}
    End Function

    Private Function 获取项路径(item As UltraDetailListView.ListItem) As String
        If item Is Nothing Then Return ""
        If TypeOf item.Tag Is String Then Return CStr(item.Tag)
        If item.SubItems.Count > 文件列 Then Return item.SubItems(文件列).Text
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
        同步属性面板()
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
        同步属性面板()
        调整列宽()
    End Sub

    Private Sub MB_移除全部_Click(sender As Object, e As EventArgs) Handles MB_移除全部.Click
        If UltraDetailListView1.Items.Count = 0 Then Exit Sub
        UltraDetailListView1.Items.Clear()
        同步属性面板()
        调整列宽()
    End Sub

    Private Sub MB_选择位置_Click(sender As Object, e As EventArgs) Handles MB_选择位置.Click
        Using d As New SaveFileDialog With {.Filter = "所有文件|*.*"}
            If d.ShowDialog(Me) = DialogResult.OK Then MTB_输出目标文件.Text = d.FileName
        End Using
    End Sub

    Private Sub UltraDetailListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles UltraDetailListView1.SelectedIndexChanged
        同步属性面板()
    End Sub

    Private Sub UltraDetailListView1_ItemDoubleClick(sender As Object, e As UltraDetailListView.ListItemEventArgs) Handles UltraDetailListView1.ItemDoubleClick
        Dim item = If(e?.Item, UltraDetailListView1.SelectedItem)
        If item Is Nothing Then Exit Sub
        Dim index = UltraDetailListView1.Items.IndexOf(item)
        If index < 0 Then Exit Sub

        显示窗体(New Form_v6_媒体流选择器(
            要读取的媒体文件:=获取项路径(item),
            视频流文本目标对象:=item.SubItems(视频列),
            音频流文本目标对象:=item.SubItems(音频列),
            字幕流文本目标对象:=item.SubItems(字幕列),
            文件索引:=index.ToString(),
            视频流已选:=item.SubItems(视频列).Text,
            音频流已选:=item.SubItems(音频列).Text,
            字幕流已选:=item.SubItems(字幕列).Text), FormMain_v6)
    End Sub

    Private Sub 同步属性面板()
        正在同步控件 = True
        Try
            Dim selected = UltraDetailListView1.SelectedItems
            Dim singleItem = If(selected.Count = 1, selected(0), Nothing)
            Dim enabled = singleItem IsNot Nothing

            MTB_视频流索引.Enabled = enabled
            MTB_音频流索引.Enabled = enabled
            MTB_字幕流索引.Enabled = enabled
            MCB_使用此文件的章节.Enabled = enabled
            MCB_使用此文件的元数据.Enabled = enabled

            MTB_视频流索引.Text = If(enabled, 获取子项文本(singleItem, 视频列), "")
            MTB_音频流索引.Text = If(enabled, 获取子项文本(singleItem, 音频列), "")
            MTB_字幕流索引.Text = If(enabled, 获取子项文本(singleItem, 字幕列), "")
            MCB_使用此文件的章节.Checked = enabled AndAlso 获取子项文本(singleItem, 章节列) = 使用此
            MCB_使用此文件的元数据.Checked = enabled AndAlso 获取子项文本(singleItem, 元数据列) = 使用此
        Finally
            正在同步控件 = False
        End Try
    End Sub

    Private Shared Function 获取子项文本(item As UltraDetailListView.ListItem, index As Integer) As String
        If item Is Nothing OrElse item.SubItems.Count <= index Then Return ""
        Return item.SubItems(index).Text
    End Function

    Private Sub MTB_视频流索引_TextChanged(sender As Object, e As EventArgs) Handles MTB_视频流索引.TextChanged
        设置选中项子项(视频列, MTB_视频流索引.Text)
    End Sub

    Private Sub MTB_音频流索引_TextChanged(sender As Object, e As EventArgs) Handles MTB_音频流索引.TextChanged
        设置选中项子项(音频列, MTB_音频流索引.Text)
    End Sub

    Private Sub MTB_字幕流索引_TextChanged(sender As Object, e As EventArgs) Handles MTB_字幕流索引.TextChanged
        设置选中项子项(字幕列, MTB_字幕流索引.Text)
    End Sub

    Private Sub 设置选中项子项(columnIndex As Integer, value As String)
        If 正在同步控件 Then Exit Sub
        If UltraDetailListView1.SelectedItems.Count <> 1 Then Exit Sub
        Dim item = UltraDetailListView1.SelectedItems(0)
        If item.SubItems.Count <= columnIndex Then Exit Sub
        item.SubItems(columnIndex).Text = If(value, "").Trim()
        调整列宽()
    End Sub

    Private Sub MCB_使用此文件的章节_CheckedChanged(sender As Object, e As EventArgs) Handles MCB_使用此文件的章节.CheckedChanged
        设置独占标记(章节列, MCB_使用此文件的章节.Checked)
    End Sub

    Private Sub MCB_使用此文件的元数据_CheckedChanged(sender As Object, e As EventArgs) Handles MCB_使用此文件的元数据.CheckedChanged
        设置独占标记(元数据列, MCB_使用此文件的元数据.Checked)
    End Sub

    Private Sub 设置独占标记(columnIndex As Integer, checked As Boolean)
        If 正在同步控件 Then Exit Sub
        If UltraDetailListView1.SelectedItems.Count <> 1 Then Exit Sub
        Dim selected = UltraDetailListView1.SelectedItems(0)

        UltraDetailListView1.BeginUpdate()
        Try
            If checked Then
                For Each item In UltraDetailListView1.Items
                    If item.SubItems.Count > columnIndex Then item.SubItems(columnIndex).Text = ""
                Next
            End If
            If selected.SubItems.Count > columnIndex Then selected.SubItems(columnIndex).Text = If(checked, 使用此, "")
        Finally
            UltraDetailListView1.EndUpdate()
        End Try
        调整列宽()
    End Sub

    Private Sub MB_启动合并_Click(sender As Object, e As EventArgs) Handles MB_启动合并.Click
        If Not 验证任务参数() Then Exit Sub

        Dim arg As New StringBuilder("-hide_banner -nostdin ")
        For Each item In UltraDetailListView1.Items
            arg.Append("-i ").Append(引用参数(获取项路径(item))).Append(" ")
        Next

        Dim mapCount As Integer = 0
        For i = 0 To UltraDetailListView1.Items.Count - 1
            Dim item = UltraDetailListView1.Items(i)
            mapCount += 追加流映射(arg, i, "v", 获取子项文本(item, 视频列))
            mapCount += 追加流映射(arg, i, "a", 获取子项文本(item, 音频列))
            mapCount += 追加流映射(arg, i, "s", 获取子项文本(item, 字幕列))

            If 获取子项文本(item, 章节列) = 使用此 Then arg.Append("-map_chapters ").Append(i).Append(" ")
            If 获取子项文本(item, 元数据列) = 使用此 Then arg.Append("-map_metadata ").Append(i).Append(" ")
        Next

        If mapCount = 0 Then
            ExFloatingTip(UltraDetailListView1, "请至少指定一个要混流的媒体流", 2200)
            Exit Sub
        End If

        Dim output = MTB_输出目标文件.Text.Trim()
        arg.Append("-c copy ").Append(引用参数(output)).Append(" -y")

        插件管理.使用命令行添加任务到编码队列(arg.ToString(), $"混流任务 {Now:HHmmss}", output, 获取项路径(UltraDetailListView1.Items(0)))
        FormMain_v6.ModernTabListControl1.SelectedIndex = 2
        ExFloatingTip(MB_启动合并, "已添加到编码队列", 1200)
    End Sub

    Private Function 验证任务参数() As Boolean
        If UltraDetailListView1.Items.Count = 0 Then
            ExFloatingTip(UltraDetailListView1, "请先添加要混流的文件", 1800)
            Return False
        End If

        Dim missing = 查找缺失文件()
        If missing <> "" Then
            ExFloatingTip(UltraDetailListView1, $"文件不存在：{missing}", 2600)
            Return False
        End If

        For Each item In UltraDetailListView1.Items
            If Not 验证流索引文本(获取子项文本(item, 视频列), "视频") Then Return False
            If Not 验证流索引文本(获取子项文本(item, 音频列), "音频") Then Return False
            If Not 验证流索引文本(获取子项文本(item, 字幕列), "字幕") Then Return False
        Next

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

    Private Function 验证流索引文本(value As String, label As String) As Boolean
        For Each token In 切分流索引(value)
            If 解析流索引(token).局部流索引 = "" Then
                ExFloatingTip(UltraDetailListView1, $"{label}流索引格式无效：{token}", 2400)
                Return False
            End If
        Next
        Return True
    End Function

    Private Function 追加流映射(arg As StringBuilder, fallbackInputIndex As Integer, kind As String, value As String) As Integer
        Dim count As Integer = 0
        For Each token In 切分流索引(value)
            Dim parsed = 解析流索引(token)
            If parsed.局部流索引 = "" Then Continue For
            Dim inputIndex = If(parsed.输入索引 >= 0, parsed.输入索引, fallbackInputIndex)
            arg.Append("-map ").Append(inputIndex).Append(":").Append(kind).Append(":").Append(parsed.局部流索引).Append("? ")
            count += 1
        Next
        Return count
    End Function

    Private Function 切分流索引(value As String) As IEnumerable(Of String)
        Return If(value, "").Split(","c, StringSplitOptions.RemoveEmptyEntries).Select(Function(x) x.Trim()).Where(Function(x) x <> "")
    End Function

    Private Function 解析流索引(token As String) As (输入索引 As Integer, 局部流索引 As String)
        Dim s = If(token, "").Trim().ToLowerInvariant()
        If s = "" Then Return (-1, "")

        Dim m = Regex.Match(s, "^(?<input>\d+):[vas]:(?<stream>\d+)$", RegexOptions.CultureInvariant)
        If m.Success Then Return (Integer.Parse(m.Groups("input").Value), m.Groups("stream").Value)

        m = Regex.Match(s, "^[vas]:(?<stream>\d+)$", RegexOptions.CultureInvariant)
        If m.Success Then Return (-1, m.Groups("stream").Value)

        If Regex.IsMatch(s, "^\d+$", RegexOptions.CultureInvariant) Then Return (-1, s)

        Return (-1, "")
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
        同步属性面板()
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
        If UltraDetailListView1.Columns.Count < 6 OrElse UltraDetailListView1.ClientSize.Width <= 0 Then Exit Sub

        Dim scale = 获取Dpi比例()
        Dim available = UltraDetailListView1.ClientSize.Width - UltraDetailListView1.Padding.Left - UltraDetailListView1.Padding.Right - CInt(30 * scale)
        If available <= 300 Then Exit Sub

        Dim smallMin = CInt(70 * scale)
        Dim smallMax = CInt(180 * scale)
        Dim widths(5) As Integer
        widths(视频列) = 限制宽度(测量列宽(视频列), smallMin, smallMax)
        widths(音频列) = 限制宽度(测量列宽(音频列), smallMin, smallMax)
        widths(字幕列) = 限制宽度(测量列宽(字幕列), smallMin, smallMax)
        widths(章节列) = 限制宽度(测量列宽(章节列), smallMin, CInt(110 * scale))
        widths(元数据列) = 限制宽度(测量列宽(元数据列), smallMin, CInt(120 * scale))

        Dim other = widths.Skip(1).Sum()
        widths(文件列) = Math.Max(CInt(180 * scale), available - other)

        For i = 0 To widths.Length - 1
            UltraDetailListView1.Columns(i).Width = widths(i)
        Next
    End Sub

    Private Function 测量列宽(columnIndex As Integer) As Integer
        Dim maxWidth As Integer = TextRenderer.MeasureText(UltraDetailListView1.Columns(columnIndex).Text, UltraDetailListView1.Font).Width + CInt(36 * 获取Dpi比例())
        For Each item In UltraDetailListView1.Items
            If item.SubItems.Count <= columnIndex Then Continue For
            Dim width = TextRenderer.MeasureText(item.SubItems(columnIndex).Text, UltraDetailListView1.Font).Width + CInt(36 * 获取Dpi比例())
            If width > maxWidth Then maxWidth = width
        Next
        Return maxWidth
    End Function

    Private Shared Function 限制宽度(value As Integer, min As Integer, max As Integer) As Integer
        Return Math.Max(min, Math.Min(max, value))
    End Function

    Private Function 获取Dpi比例() As Single
        Return CSng(DeviceDpi) / 96.0F
    End Function

End Class
