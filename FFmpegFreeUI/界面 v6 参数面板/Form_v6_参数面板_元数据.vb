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
        For Each item In UltraDetailListView1.Items
            If item.SubItems.Count < 2 Then Continue For
            Dim field = item.SubItems(0).Text.Trim()
            Dim value = item.SubItems(1).Text.Trim()
            If field = "" Then Continue For
            result.Add(New 预设数据_v6.元数据单片结构 With {.字段 = field, .值 = value})
        Next
        Return result
    End Function

    Public Sub 设置数据(items As IEnumerable(Of 预设数据_v6.元数据单片结构))
        UltraDetailListView1.Items.Clear()
        If items Is Nothing Then Exit Sub
        For Each item In items
            If item Is Nothing OrElse item.字段.Trim() = "" Then Continue For
            添加列表项(item.字段.Trim(), If(item.值, ""))
        Next
    End Sub

    Private Sub 添加列表项(field As String, value As String)
        Dim lv As New UltraDetailListView.ListItem()
        lv.SubItems.Add(New UltraDetailListView.ListSubItem With {.Text = field})
        lv.SubItems.Add(New UltraDetailListView.ListSubItem With {.Text = value})
        UltraDetailListView1.Items.Add(lv)
    End Sub

    Private Sub 添加字段(displayText As String)
        Dim key = If(displayText, "").Trim()
        If key = "" Then Exit Sub
        Dim field = If(字段映射.ContainsKey(key), 字段映射(key), key)
        添加列表项(field, "")
        通知参数面板刷新()
    End Sub

    Private Sub 删除所选()
        For Each item In UltraDetailListView1.SelectedItems.ToArray()
            UltraDetailListView1.Items.Remove(item)
        Next
        通知参数面板刷新()
    End Sub

    Private Sub 清空全部()
        If UltraDetailListView1.Items.Count = 0 Then Exit Sub
        UltraDetailListView1.Items.Clear()
        通知参数面板刷新()
    End Sub

    Private Sub 导出列表()
        Using d As New SaveFileDialog With {.Filter = "JSON 元数据列表|*.json", .FileName = "metadata.json"}
            If d.ShowDialog(Me) <> DialogResult.OK OrElse d.FileName = "" Then Exit Sub
            File.WriteAllText(d.FileName, JsonSerializer.Serialize(获取数据(), JsonSO), Encoding.UTF8)
            ExFloatingTip(ModernButton2, "已导出元数据列表", 1200)
        End Using
    End Sub

    Private Sub 导入列表()
        Using d As New OpenFileDialog With {.Filter = "JSON 元数据列表|*.json", .Multiselect = False}
            If d.ShowDialog(Me) <> DialogResult.OK OrElse d.FileName = "" Then Exit Sub
            Dim data = JsonSerializer.Deserialize(Of List(Of 预设数据_v6.元数据单片结构))(File.ReadAllText(d.FileName), JsonSO)
            设置数据(data)
            通知参数面板刷新()
            ExFloatingTip(ModernButton3, "已导入元数据列表", 1200)
        End Using
    End Sub

    Private Sub ModernComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ModernComboBox1.SelectedIndexChanged
        If ModernComboBox1.SelectedIndex < 0 Then Exit Sub
        添加字段(ModernComboBox1.Text)
        ModernComboBox1.SelectedIndex = -1
        ModernComboBox1.Text = ""
    End Sub

    Private Sub ModernButton4_Click(sender As Object, e As EventArgs) Handles ModernButton4.Click
        删除所选()
    End Sub

    Private Sub ModernButton1_Click(sender As Object, e As EventArgs) Handles ModernButton1.Click
        清空全部()
    End Sub

    Private Sub ModernButton2_Click(sender As Object, e As EventArgs) Handles ModernButton2.Click
        导出列表()
    End Sub

    Private Sub ModernButton3_Click(sender As Object, e As EventArgs) Handles ModernButton3.Click
        导入列表()
    End Sub

    Private Sub UltraDetailListView1_AfterLabelEdit(sender As Object, e As UltraDetailListView.LabelEditEventArgs) Handles UltraDetailListView1.AfterLabelEdit
        If e.ColumnIndex <> 1 Then
            e.CancelEdit = True
            Exit Sub
        End If
        通知参数面板刷新()
    End Sub

    Private Sub UltraDetailListView1_ItemOrderChanged(sender As Object, e As EventArgs) Handles UltraDetailListView1.ItemOrderChanged
        通知参数面板刷新()
    End Sub

    Private Sub 通知参数面板刷新()
        If 所属参数面板对象 Is Nothing OrElse 所属参数面板对象.抑制自动刷新 Then Exit Sub
        所属参数面板对象.请求刷新参数状态()
    End Sub

End Class
