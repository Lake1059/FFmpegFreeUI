Imports System.IO
Imports System.Text
Imports System.Text.Json
Imports LakeUI

Public Class Form_v6_参数面板_元数据

    Public 所属参数面板对象 As Form_v6_参数面板

    Private Shared ReadOnly 字段映射 As New Dictionary(Of String, String) From {
        {"标题", "title"},
        {"参与创作的艺术家", "artist"},
        {"专辑", "album"},
        {"流派", "genre"},
        {"曲目编号", "track"},
        {"碟片编号", "disc"},
        {"日期", "date"},
        {"版权", "copyright"},
        {"备注", "comment"},
        {"描述", "description"},
        {"编码器", "encoder"},
        {"软件", "software"}
    }

    Public Function 获取数据() As List(Of 预设数据_v6.元数据单片结构)
        Dim result As New List(Of 预设数据_v6.元数据单片结构)
        For Each item In UDLV_元数据列表.Items
            If item.SubItems.Count < 2 Then Continue For
            Dim field = item.SubItems(0).Text.Trim()
            Dim value = item.SubItems(1).Text.Trim()
            If field = "" Then Continue For
            result.Add(New 预设数据_v6.元数据单片结构 With {.字段 = field, .值 = value})
        Next
        Return result
    End Function

    Public Sub 设置数据(items As IEnumerable(Of 预设数据_v6.元数据单片结构))
        UDLV_元数据列表.Items.Clear()
        If items Is Nothing Then
            校准列表列宽()
            Exit Sub
        End If
        For Each item In items
            If item Is Nothing OrElse item.字段.Trim() = "" Then Continue For
            添加列表项(item.字段.Trim(), If(item.值, ""), False)
        Next
        校准列表列宽()
    End Sub

    Private Sub 添加列表项(field As String, value As String, Optional 校准列宽 As Boolean = True)
        Dim lv As New UltraDetailListView.ListItem()
        lv.SubItems.Add(New UltraDetailListView.ListSubItem With {.Text = field})
        lv.SubItems.Add(New UltraDetailListView.ListSubItem With {.Text = value})
        UDLV_元数据列表.Items.Add(lv)
        If 校准列宽 Then 校准列表列宽()
    End Sub

    Private Sub 添加字段(displayText As String)
        Dim key = If(displayText, "").Trim()
        If key = "" Then Exit Sub
        Dim field = If(字段映射.ContainsKey(key), 字段映射(key), key)
        添加列表项(field, "")
        通知参数面板刷新()
    End Sub

    Private Sub 删除所选()
        For Each item In UDLV_元数据列表.SelectedItems.ToArray()
            UDLV_元数据列表.Items.Remove(item)
        Next
        校准列表列宽()
        通知参数面板刷新()
    End Sub

    Private Sub 清空全部()
        If UDLV_元数据列表.Items.Count = 0 Then Exit Sub
        UDLV_元数据列表.Items.Clear()
        校准列表列宽()
        通知参数面板刷新()
    End Sub

    Private Sub 导出列表()
        Using d As New SaveFileDialog With {.Filter = "JSON 元数据列表|*.json", .FileName = "metadata.json"}
            If d.ShowDialog(Me) <> DialogResult.OK OrElse d.FileName = "" Then Exit Sub
            File.WriteAllText(d.FileName, JsonSerializer.Serialize(获取数据(), JsonSO), Encoding.UTF8)
            ExFloatingTip(MB_导出元数据, "已导出元数据列表", 1200)
        End Using
    End Sub

    Private Sub 导入列表()
        Using d As New OpenFileDialog With {.Filter = "JSON 元数据列表|*.json", .Multiselect = False}
            If d.ShowDialog(Me) <> DialogResult.OK OrElse d.FileName = "" Then Exit Sub
            Dim data = JsonSerializer.Deserialize(Of List(Of 预设数据_v6.元数据单片结构))(File.ReadAllText(d.FileName), JsonSO)
            设置数据(data)
            通知参数面板刷新()
            ExFloatingTip(MB_导入元数据, "已导入元数据列表", 1200)
        End Using
    End Sub

    Private Sub ModernComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_添加元数据预制项.SelectedIndexChanged
        If MCB_添加元数据预制项.SelectedIndex < 0 Then Exit Sub
        添加字段(MCB_添加元数据预制项.Text)
        MCB_添加元数据预制项.SelectedIndex = -1
        MCB_添加元数据预制项.Text = ""
    End Sub

    Private Sub ModernButton4_Click(sender As Object, e As EventArgs) Handles MB_删除所选元数据.Click
        删除所选()
    End Sub

    Private Sub ModernButton1_Click(sender As Object, e As EventArgs) Handles MB_清空全部元数据.Click
        清空全部()
    End Sub

    Private Sub ModernButton2_Click(sender As Object, e As EventArgs) Handles MB_导出元数据.Click
        导出列表()
    End Sub

    Private Sub ModernButton3_Click(sender As Object, e As EventArgs) Handles MB_导入元数据.Click
        导入列表()
    End Sub

    Private Sub UltraDetailListView1_AfterLabelEdit(sender As Object, e As UltraDetailListView.LabelEditEventArgs) Handles UDLV_元数据列表.AfterLabelEdit
        If e.ColumnIndex <> 1 Then
            e.CancelEdit = True
            Exit Sub
        End If
        通知参数面板刷新()
    End Sub

    Private Sub UltraDetailListView1_ItemOrderChanged(sender As Object, e As EventArgs) Handles UDLV_元数据列表.ItemOrderChanged
        通知参数面板刷新()
    End Sub

    Private Sub Form_v6_参数面板_元数据_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
        校准列表列宽()
    End Sub

    Private Sub UltraDetailListView1_SizeChanged(sender As Object, e As EventArgs) Handles UDLV_元数据列表.SizeChanged
        校准列表列宽()
    End Sub

    Private Sub 校准列表列宽()
        If UDLV_元数据列表 Is Nothing OrElse UDLV_元数据列表.Columns.Count < 2 Then Exit Sub

        Dim 可用宽度 = UDLV_元数据列表.ClientSize.Width
        If 可用宽度 <= 0 Then 可用宽度 = UDLV_元数据列表.Width
        If 可用宽度 <= 0 Then Exit Sub

        Dim dpi = DeviceDpi / 96.0
        可用宽度 = Math.Max(0, 可用宽度 - UDLV_元数据列表.Padding.Left - UDLV_元数据列表.Padding.Right - CInt(24 * dpi))

        Dim 字段列宽 = 限制列宽(测量列宽(0, CInt(42 * dpi)), CInt(110 * dpi), Math.Min(CInt(260 * dpi), CInt(可用宽度 * 0.38)))
        Dim 值列宽 = Math.Max(CInt(240 * dpi), 可用宽度 - 字段列宽)

        UDLV_元数据列表.Columns(0).Width = 字段列宽
        UDLV_元数据列表.Columns(1).Width = 值列宽
    End Sub

    Private Function 测量列宽(columnIndex As Integer, horizontalPadding As Integer) As Integer
        Dim maxWidth = TextRenderer.MeasureText(If(UDLV_元数据列表.Columns(columnIndex).Text, ""), UDLV_元数据列表.Font).Width + horizontalPadding
        For Each item In UDLV_元数据列表.Items
            If item.SubItems.Count <= columnIndex Then Continue For
            Dim width = TextRenderer.MeasureText(If(item.SubItems(columnIndex).Text, ""), UDLV_元数据列表.Font).Width + horizontalPadding
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
