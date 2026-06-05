Public Class Form_v6_参数面板_音频参数
    Private Sub Form_v6_参数面板_音频参数_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        初始化音频编码器下拉框()
    End Sub

    Private Sub 初始化音频编码器下拉框()
        ModernComboBox1.Items.Clear()
        ModernComboBox1.ItemToolTips.Clear()

        For Each 编码器 In 音频编码器数据库_v6.全部编码器
            ModernComboBox1.Items.Add(编码器.显示名称)
            If 编码器.下拉提示文本 <> "" Then
                ModernComboBox1.ItemToolTips.Add(New LakeUI.ModernComboBox.ToolTipEntry With {
                    .ItemText = 编码器.显示名称,
                    .ToolTipText = 编码器.下拉提示文本
                })
            End If
        Next
    End Sub
End Class
