Imports System.IO
Imports System.Text
Imports System.Text.Json
Imports LakeUI

Public Class Form_v6_参数面板_附件

    Public 所属参数面板对象 As Form_v6_参数面板

    Public Function 获取数据() As List(Of 预设数据_v6.附件单片结构)
        Dim result As New List(Of 预设数据_v6.附件单片结构)
        For Each item In UDLV_附件列表.Items
            If item.SubItems.Count < 2 Then Continue For
            Dim 类型 = 解析附件类型(item.SubItems(0).Text)
            Dim file = item.SubItems(1).Text.Trim()
            If 类型 = 预设数据_v6.附件单片结构.附件类型.未选择 Then Continue For
            result.Add(New 预设数据_v6.附件单片结构 With {.类型 = 类型, .文件路径 = file})
        Next
        Return result
    End Function

    Public Sub 设置数据(items As IEnumerable(Of 预设数据_v6.附件单片结构))
        UDLV_附件列表.Items.Clear()
        If items Is Nothing Then
            校准列表列宽()
            Exit Sub
        End If
        For Each item In items
            If item Is Nothing OrElse item.类型 = 预设数据_v6.附件单片结构.附件类型.未选择 Then Continue For
            添加列表项(item.类型, If(item.文件路径, ""), False)
        Next
        校准列表列宽()
    End Sub

    Private Sub 添加列表项(类型 As 预设数据_v6.附件单片结构.附件类型, 文件路径 As String, Optional 校准列宽 As Boolean = True)
        Dim lv As New UltraDetailListView.ListItem()
        lv.SubItems.Add(New UltraDetailListView.ListSubItem With {.Text = 附件类型显示名(类型)})
        lv.SubItems.Add(New UltraDetailListView.ListSubItem With {.Text = 文件路径})
        UDLV_附件列表.Items.Add(lv)
        If 校准列宽 Then 校准列表列宽()
    End Sub

    Private Sub 添加附件(displayText As String)
        Dim 类型 = 解析附件类型(displayText)
        If 类型 = 预设数据_v6.附件单片结构.附件类型.未选择 Then Exit Sub
        Dim file = 选择附件文件(类型)
        添加列表项(类型, file)
        通知参数面板刷新()
    End Sub

    Private Function 选择附件文件(类型 As 预设数据_v6.附件单片结构.附件类型) As String
        Dim filter = "所有文件|*.*"
        Select Case 类型
            Case 预设数据_v6.附件单片结构.附件类型.图片,
                 预设数据_v6.附件单片结构.附件类型.MP4封面图,
                 预设数据_v6.附件单片结构.附件类型.MKV封面图
                filter = "图片|*.png;*.jpg;*.jpeg;*.webp;*.bmp|所有文件|*.*"
            Case 预设数据_v6.附件单片结构.附件类型.字体文件
                filter = "字体|*.ttf;*.otf;*.ttc;*.woff;*.woff2|所有文件|*.*"
            Case 预设数据_v6.附件单片结构.附件类型.文本文档
                filter = "文本文档|*.txt;*.md;*.json;*.xml;*.nfo|所有文件|*.*"
        End Select
        Using d As New OpenFileDialog With {.Filter = filter, .Multiselect = False}
            If d.ShowDialog(Me) = DialogResult.OK Then Return d.FileName
        End Using
        Return ""
    End Function

    Private Function 解析附件类型(text As String) As 预设数据_v6.附件单片结构.附件类型
        Select Case If(text, "").Trim()
            Case "添加图片", "图片"
                Return 预设数据_v6.附件单片结构.附件类型.图片
            Case "添加 MP4 封面图", "MP4封面图", "MP4 封面图"
                Return 预设数据_v6.附件单片结构.附件类型.MP4封面图
            Case "添加 MKV 封面图", "MKV封面图", "MKV 封面图"
                Return 预设数据_v6.附件单片结构.附件类型.MKV封面图
            Case "添加字体文件", "字体文件"
                Return 预设数据_v6.附件单片结构.附件类型.字体文件
            Case "添加文本文档", "文本文档"
                Return 预设数据_v6.附件单片结构.附件类型.文本文档
            Case Else
                Dim parsed As 预设数据_v6.附件单片结构.附件类型
                If [Enum].TryParse(text, parsed) Then Return parsed
                Return 预设数据_v6.附件单片结构.附件类型.未选择
        End Select
    End Function

    Private Function 附件类型显示名(类型 As 预设数据_v6.附件单片结构.附件类型) As String
        Select Case 类型
            Case 预设数据_v6.附件单片结构.附件类型.MP4封面图
                Return "MP4 封面图"
            Case 预设数据_v6.附件单片结构.附件类型.MKV封面图
                Return "MKV 封面图"
            Case Else
                Return 类型.ToString()
        End Select
    End Function

    Private Sub 删除所选()
        For Each item In UDLV_附件列表.SelectedItems.ToArray()
            UDLV_附件列表.Items.Remove(item)
        Next
        校准列表列宽()
        通知参数面板刷新()
    End Sub

    Private Sub 清空全部()
        If UDLV_附件列表.Items.Count = 0 Then Exit Sub
        UDLV_附件列表.Items.Clear()
        校准列表列宽()
        通知参数面板刷新()
    End Sub

    Private Sub 导出列表()
        Using d As New SaveFileDialog With {.Filter = "JSON 附件列表|*.json", .FileName = "attachments.json"}
            If d.ShowDialog(Me) <> DialogResult.OK OrElse d.FileName = "" Then Exit Sub
            File.WriteAllText(d.FileName, JsonSerializer.Serialize(获取数据(), JsonSO), Encoding.UTF8)
            ExFloatingTip(MB_导出附件, "已导出附件列表", 1200)
        End Using
    End Sub

    Private Sub 导入列表()
        Using d As New OpenFileDialog With {.Filter = "JSON 附件列表|*.json", .Multiselect = False}
            If d.ShowDialog(Me) <> DialogResult.OK OrElse d.FileName = "" Then Exit Sub
            Dim data = JsonSerializer.Deserialize(Of List(Of 预设数据_v6.附件单片结构))(File.ReadAllText(d.FileName), JsonSO)
            设置数据(data)
            通知参数面板刷新()
            ExFloatingTip(MB_导入附件, "已导入附件列表", 1200)
        End Using
    End Sub

    Private Sub ModernComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_添加附件.SelectedIndexChanged
        If MCB_添加附件.SelectedIndex < 0 Then Exit Sub
        添加附件(MCB_添加附件.Text)
        MCB_添加附件.SelectedIndex = -1
        MCB_添加附件.Text = ""
    End Sub

    Private Sub ModernButton4_Click(sender As Object, e As EventArgs) Handles MB_删除所选附件.Click
        删除所选()
    End Sub

    Private Sub ModernButton1_Click(sender As Object, e As EventArgs) Handles MB_清空全部附件.Click
        清空全部()
    End Sub

    Private Sub ModernButton2_Click(sender As Object, e As EventArgs) Handles MB_导出附件.Click
        导出列表()
    End Sub

    Private Sub ModernButton3_Click(sender As Object, e As EventArgs) Handles MB_导入附件.Click
        导入列表()
    End Sub

    Private Sub UltraDetailListView1_AfterLabelEdit(sender As Object, e As UltraDetailListView.LabelEditEventArgs) Handles UDLV_附件列表.AfterLabelEdit
        If e.ColumnIndex <> 1 Then
            e.CancelEdit = True
            Exit Sub
        End If
        通知参数面板刷新()
    End Sub

    Private Sub UltraDetailListView1_ItemOrderChanged(sender As Object, e As EventArgs) Handles UDLV_附件列表.ItemOrderChanged
        通知参数面板刷新()
    End Sub

    Private Sub Form_v6_参数面板_附件_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
        校准列表列宽()
    End Sub

    Private Sub UltraDetailListView1_SizeChanged(sender As Object, e As EventArgs) Handles UDLV_附件列表.SizeChanged
        校准列表列宽()
    End Sub

    Private Sub 校准列表列宽()
        If UDLV_附件列表 Is Nothing OrElse UDLV_附件列表.Columns.Count < 2 Then Exit Sub

        Dim 可用宽度 = UDLV_附件列表.ClientSize.Width
        If 可用宽度 <= 0 Then 可用宽度 = UDLV_附件列表.Width
        If 可用宽度 <= 0 Then Exit Sub

        Dim dpi = DeviceDpi / 96.0
        可用宽度 = Math.Max(0, 可用宽度 - UDLV_附件列表.Padding.Left - UDLV_附件列表.Padding.Right - CInt(24 * dpi))

        Dim 类型列宽 = 限制列宽(测量列宽(0, CInt(42 * dpi)), CInt(110 * dpi), Math.Min(CInt(210 * dpi), CInt(可用宽度 * 0.32)))
        Dim 文件路径列宽 = Math.Max(CInt(260 * dpi), 可用宽度 - 类型列宽)

        UDLV_附件列表.Columns(0).Width = 类型列宽
        UDLV_附件列表.Columns(1).Width = 文件路径列宽
    End Sub

    Private Function 测量列宽(columnIndex As Integer, horizontalPadding As Integer) As Integer
        Dim maxWidth = TextRenderer.MeasureText(If(UDLV_附件列表.Columns(columnIndex).Text, ""), UDLV_附件列表.Font).Width + horizontalPadding
        For Each item In UDLV_附件列表.Items
            If item.SubItems.Count <= columnIndex Then Continue For
            Dim width = TextRenderer.MeasureText(If(item.SubItems(columnIndex).Text, ""), UDLV_附件列表.Font).Width + horizontalPadding
            If width > maxWidth Then maxWidth = width
        Next
        Return maxWidth
    End Function

    Private Function 限制列宽(value As Integer, minWidth As Integer, maxWidth As Integer) As Integer
        If maxWidth < minWidth Then maxWidth = minWidth
        Return Math.Min(maxWidth, Math.Max(minWidth, value))
    End Function

    Private Sub 通知参数面板刷新()
        If 所属参数面板对象 Is Nothing OrElse 所属参数面板对象.抑制自动刷新 Then Exit Sub
        所属参数面板对象.请求刷新参数状态()
    End Sub

End Class
