Public Class Form_v6_参数面板_音频参数
    Private Sub Form_v6_参数面板_音频参数_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        初始化音频编码器下拉框()
        初始化质量参数名下拉框()
    End Sub

    Private Sub 初始化音频编码器下拉框()
        Dim 当前文本 = MCB_音频编码器.Text
        MCB_音频编码器.Items.Clear()
        MCB_音频编码器.ItemToolTips.Clear()

        For Each 编码器 In 音频编码器数据库_v6.全部编码器
            MCB_音频编码器.Items.Add(编码器.显示名称)
            If 编码器.下拉提示文本 <> "" Then
                MCB_音频编码器.ItemToolTips.Add(New LakeUI.ModernComboBox.ToolTipEntry With {
                    .ItemText = 编码器.显示名称,
                    .ToolTipText = 编码器.下拉提示文本
                })
            End If
        Next

        设置组合框文本并尝试选中(MCB_音频编码器, 当前文本)
    End Sub

    Private Sub 初始化质量参数名下拉框()
        Dim 当前文本 = MCB_质量参数名.Text
        MCB_质量参数名.Items.Clear()
        For Each 参数名 In 音频编码器数据库_v6.获取质量参数名列表()
            MCB_质量参数名.Items.Add(参数名)
        Next

        设置组合框文本并尝试选中(MCB_质量参数名, 当前文本)
    End Sub

    Private Sub 设置组合框文本并尝试选中(combo As LakeUI.ModernComboBox, text As String)
        If combo Is Nothing Then Exit Sub
        text = If(text, "")
        For i = 0 To combo.Items.Count - 1
            Dim itemText = If(combo.Items(i), "").ToString()
            If String.Equals(itemText, text, StringComparison.Ordinal) Then
                combo.SelectedIndex = i
                Exit Sub
            End If
        Next
        combo.Text = text
    End Sub
End Class
