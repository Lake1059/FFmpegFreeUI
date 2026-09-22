Public Class Form_v6_参数面板_音频参数
    Private 正在初始化音频参数 As Boolean

    Private Sub Form_v6_参数面板_音频参数_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        正在初始化音频参数 = True
        Try
            初始化音频编码器下拉框()
            绑定音频编码器工具提示()
            初始化质量参数名下拉框()
        Finally
            正在初始化音频参数 = False
        End Try
        If 当前编码器需要清空其他参数() Then 清空其他音频参数()
    End Sub

    Private Sub 初始化音频编码器下拉框()
        Dim 当前文本 = MCB_音频编码器.Text
        MCB_音频编码器.Items.Clear()
        MCB_音频编码器.Items.Add("")

        For Each 编码器 In 音频编码器数据库_v6.全部编码器
            MCB_音频编码器.Items.Add(编码器.显示名称)
        Next

        设置组合框文本并尝试选中(MCB_音频编码器, 当前文本)
    End Sub

    Private Sub MCB_音频编码器_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_音频编码器.SelectedIndexChanged
        If 正在初始化音频参数 Then Exit Sub
        If 当前编码器需要清空其他参数() Then 清空其他音频参数()
    End Sub

    Public Function 当前编码器需要清空其他参数() As Boolean
        If String.IsNullOrWhiteSpace(MCB_音频编码器.Text) Then Return True

        Dim 编码器 = 音频编码器数据库_v6.获取编码器数据_按显示名称(MCB_音频编码器.Text)
        Return 编码器 IsNot Nothing AndAlso (编码器.是否复制流 OrElse 编码器.是否禁用)
    End Function

    Public Sub 清空其他音频参数()
        MCB_比特率.Text = ""
        MCB_质量参数名.Text = ""
        MTB_质量值.Text = ""
        MCB_质量参数名2.Text = ""
        MTB_质量值2.Text = ""
        MCB_声道布局.Text = ""
        MCB_位深度.Text = ""
        MCB_采样率.Text = ""

        MCK_启用目标响度.Checked = False
        MCK_启用动态范围.Checked = False
        MCK_启用峰值电平.Checked = False
        ETB_目标响度.Value = -24.0R
        ETB_动态范围.Value = 1.0R
        ETB_峰值电平.Value = -1.0R
    End Sub

    Private Sub 绑定音频编码器工具提示()
        MCB_音频编码器.ItemToolTips.Clear()

        For Each item In MCB_音频编码器.Items
            Dim 显示名称 = If(item, "").ToString()
            Dim 编码器 = 音频编码器数据库_v6.获取编码器数据_按显示名称(显示名称)
            If 编码器 Is Nothing OrElse 编码器.下拉提示文本 = "" Then Continue For
            MCB_音频编码器.ItemToolTips.Add(New LakeUI.ModernComboBox.ToolTipEntry With {
                .ItemText = 显示名称,
                .ToolTipText = 编码器.下拉提示文本
            })
        Next
    End Sub

    Private Sub 初始化质量参数名下拉框()
        初始化质量参数名下拉框(MCB_质量参数名)
        初始化质量参数名下拉框(MCB_质量参数名2)
    End Sub

    Private Sub 初始化质量参数名下拉框(combo As LakeUI.ModernComboBox)
        Dim 当前文本 = combo.Text
        combo.Items.Clear()
        For Each 参数名 In 音频编码器数据库_v6.获取质量参数名列表()
            combo.Items.Add(参数名)
        Next
        设置组合框文本并尝试选中(combo, 当前文本)
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
