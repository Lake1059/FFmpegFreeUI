Public Class Form_v6_参数面板_视频编码器

    Public 所属参数面板对象 As Form_v6_参数面板
    Private MCB_像素格式 As LakeUI.ModernComboBox

    Private Sub Form_v6_参数面板_视频编码器_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If 所属参数面板对象 IsNot Nothing AndAlso 所属参数面板对象.私有界面_色彩管理 IsNot Nothing Then
            MCB_像素格式 = 所属参数面板对象.私有界面_色彩管理.MCB_像素格式
        End If
    End Sub

    Private Sub MCB_视频编码器类型_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_视频编码器类型.SelectedIndexChanged
        清空分类()
        清空编码器()
        清空编码器参数()

        Dim 类型 = CType(MCB_视频编码器类型.SelectedIndex, 预设数据_v6.视频编码器类型)
        If 类型 = 预设数据_v6.视频编码器类型.未选择 Then Exit Sub

        Dim 分类列表 = 视频编码器数据库_v6.获取分类列表(类型)
        For Each 分类 In 分类列表
            MCB_视频编码器分类.Items.Add(分类.名称)
            If 分类.描述 <> "" Then 添加提示(MCB_视频编码器分类, 分类.名称, 分类.描述)
        Next
        MCB_视频编码器分类.MaxDropDownItems = If(类型 = 预设数据_v6.视频编码器类型.图片, 10, 15)
        If MCB_视频编码器分类.Items.Count > 0 Then MCB_视频编码器分类.SelectedIndex = 0
    End Sub

    Private Sub MCB_视频编码器分类_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_视频编码器分类.SelectedIndexChanged
        清空编码器()
        清空编码器参数()

        Dim 编码器列表 = 视频编码器数据库_v6.获取编码器列表(MCB_视频编码器分类.Text)
        For Each 编码器 In 编码器列表
            MCB_具体编码器.Items.Add(编码器.名称)
            Dim 提示 = 合成编码器提示文本(编码器)
            If 提示 <> "" Then 添加提示(MCB_具体编码器, 编码器.名称, 提示)
        Next
        If MCB_具体编码器.Items.Count > 0 Then MCB_具体编码器.SelectedIndex = 0
    End Sub

    Private Sub MCB_具体编码器_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_具体编码器.SelectedIndexChanged
        清空编码器参数()

        Dim 编码器 = 视频编码器数据库_v6.获取编码器数据(MCB_具体编码器.Text)
        If 编码器 Is Nothing Then Exit Sub

        填充参数列表(MCB_编码预设, 编码器.编码预设)
        填充参数列表(MCB_配置文件, 编码器.配置文件)
        填充参数列表(MCB_场景优化, 编码器.场景优化)
        If MCB_像素格式 IsNot Nothing Then 填充参数列表(MCB_像素格式, 编码器.像素格式)

        更新图片质量输入(编码器)
    End Sub

    Private Sub 清空分类()
        MCB_视频编码器分类.Items.Clear()
        MCB_视频编码器分类.ItemToolTips.Clear()
        MCB_视频编码器分类.Text = ""
    End Sub

    Private Sub 清空编码器()
        MCB_具体编码器.Items.Clear()
        MCB_具体编码器.ItemToolTips.Clear()
        MCB_具体编码器.Text = ""
    End Sub

    Private Sub 清空编码器参数()
        清空组合框(MCB_编码预设)
        清空组合框(MCB_配置文件)
        清空组合框(MCB_场景优化)
        If MCB_像素格式 IsNot Nothing Then 清空组合框(MCB_像素格式)

        MCB_编码预设.WaterText = "-preset"
        MCB_配置文件.WaterText = "-profile:v"
        MCB_场景优化.WaterText = "-tune"
        If MCB_像素格式 IsNot Nothing Then MCB_像素格式.WaterText = "-pix_fmt"

        Panel7.Visible = False
        MTB_图片编码器质量值.Text = ""
        MTB_图片编码器质量值.WaterText = ""
        HtmlColorLabel7.Text = "图片编码器质量值 / 其他定制参数"
    End Sub

    Private Sub 清空组合框(MCB As LakeUI.ModernComboBox)
        MCB.Items.Clear()
        MCB.ItemToolTips.Clear()
        MCB.Text = ""
    End Sub

    Private Sub 填充参数列表(MCB As LakeUI.ModernComboBox, 参数数据 As 视频编码器数据库_v6.编码器参数列表数据)
        If 参数数据 Is Nothing Then Exit Sub

        MCB.WaterText = If(参数数据.参数名 <> "", 参数数据.参数名, MCB.WaterText)
        If 参数数据.值列表.Count > 0 Then
            MCB.Items.Add("")
            MCB.Items.AddRange(参数数据.值列表.ToArray())
        End If

        For Each kv In 参数数据.值说明
            添加提示(MCB, kv.Key, kv.Value)
        Next

        If 参数数据.默认值 <> "" Then MCB.Text = 参数数据.默认值
    End Sub

    Private Sub 更新图片质量输入(编码器 As 视频编码器数据库_v6.视频编码器数据)
        If 编码器.类型 <> 预设数据_v6.视频编码器类型.图片 Then Exit Sub

        If 编码器.图片质量.参数名 <> "" Then
            Panel7.Visible = True
            MTB_图片编码器质量值.WaterText = If(编码器.图片质量.默认值 <> "", 编码器.图片质量.默认值, 编码器.图片质量.参数名)
            HtmlColorLabel7.Text = $"{编码器.图片质量.参数名}：{编码器.图片质量.值范围说明}"
            If 编码器.必要参数列表.Count > 0 Then HtmlColorLabel7.Text &= "；必要参数见下拉提示"
        End If

        If 编码器.必要参数列表.Count > 0 AndAlso 编码器.图片质量.参数名 = "" Then
            Panel7.Visible = True
            HtmlColorLabel7.Text = "必要/建议参数见编码器下拉提示"
        End If
    End Sub

    Private Function 合成编码器提示文本(编码器 As 视频编码器数据库_v6.视频编码器数据) As String
        Dim 片段 As New List(Of String)
        If 编码器.描述 <> "" Then 片段.Add(编码器.描述)
        If 编码器.可用性说明 <> "" Then 片段.Add(编码器.可用性说明)

        Dim 参数名 = 编码器.特殊参数名列表
        If 参数名.Count > 0 Then 片段.Add("参数名：" & String.Join(" ", 参数名))
        If 编码器.图片质量.值范围说明 <> "" Then 片段.Add("质量：" & 编码器.图片质量.值范围说明)
        If 编码器.必要参数列表.Count > 0 Then 片段.Add("必要/建议：" & String.Join("；", 编码器.必要参数列表.Select(Function(x) $"{x.参数名} {x.值范围说明}")))

        Return String.Join(vbCrLf, 片段)
    End Function

    Private Sub 添加提示(MCB As LakeUI.ModernComboBox, 项文本 As String, 提示文本 As String)
        MCB.ItemToolTips.Add(New LakeUI.ModernComboBox.ToolTipEntry With {
            .ItemText = 项文本,
            .ToolTipText = 提示文本
        })
    End Sub

    Private Sub MCB_编码预设_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_编码预设.SelectedIndexChanged

    End Sub

    Private Sub MCB_配置文件_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_配置文件.SelectedIndexChanged

    End Sub

    Private Sub MCB_场景优化_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_场景优化.SelectedIndexChanged

    End Sub

End Class
