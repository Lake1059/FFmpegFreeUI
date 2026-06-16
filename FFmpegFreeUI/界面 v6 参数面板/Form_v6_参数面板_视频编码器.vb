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

        Dim 命令行 = 获取命令行提示(编码器)
        If 命令行 <> "" Then 片段.Add(命令行)
        If 编码器.可用性说明 <> "" Then 片段.Add("可用性：" & 编码器.可用性说明)
        If 编码器.视觉体积均衡点 <> "" Then 片段.Add("视觉体积均衡点：" & 编码器.视觉体积均衡点)
        If 编码器.无损模式说明 <> "" Then 片段.Add("无损模式：" & 编码器.无损模式说明)

        添加非空片段(片段, 格式化图片质量(编码器.图片质量))
        添加非空片段(片段, 格式化特殊参数列表("进阶质量参数", 编码器.特殊参数列表.Where(AddressOf 是主提示质量参数).ToList()))
        添加非空片段(片段, 格式化特殊参数列表("其他特殊参数", 编码器.特殊参数列表.Where(Function(x) Not 是质量相关参数(x)).ToList()))
        添加非空片段(片段, 格式化特殊参数列表("必要/建议参数", 编码器.必要参数列表))
        添加非空片段(片段, 格式化特殊参数列表("默认附加参数", 编码器.默认附加参数列表))

        If 编码器.类型 = 预设数据_v6.视频编码器类型.视频 AndAlso Not 编码器.是否复制流 AndAlso Not 编码器.是否禁用 Then
            片段.Add("二次编码：" & If(编码器.支持二次编码, "支持。", "未标记为支持。"))
        End If

        Return String.Join(vbCrLf & vbCrLf, 片段.Where(Function(x) x <> ""))
    End Function

    Private Sub 添加非空片段(片段 As List(Of String), 文本 As String)
        If 文本 <> "" Then 片段.Add(文本)
    End Sub

    Private Function 获取命令行提示(编码器 As 视频编码器数据库_v6.视频编码器数据) As String
        If 编码器.是否禁用 Then Return "命令：-vn"

        Dim 参数 As New List(Of String)
        If 编码器.命令行编码器名 <> "" Then 参数.Add("-c:v " & 编码器.命令行编码器名)
        For Each 单项参数 In 编码器.默认附加参数列表
            Dim 文本 = 格式化命令行参数(单项参数)
            If 文本 <> "" Then 参数.Add(文本)
        Next

        If 参数.Count = 0 Then Return ""
        Return "命令：" & String.Join(" ", 参数)
    End Function

    Private Function 格式化命令行参数(参数 As 视频编码器数据库_v6.编码器特殊参数数据) As String
        If 参数 Is Nothing OrElse 参数.参数名 = "" Then Return ""
        If 参数.默认值 = "" Then Return 参数.参数名
        Return 参数.参数名 & " " & 参数.默认值
    End Function

    Private Function 格式化参数列表数据(标题 As String, 参数数据 As 视频编码器数据库_v6.编码器参数列表数据) As String
        If 参数数据 Is Nothing Then Return ""
        If 参数数据.参数名 = "" AndAlso 参数数据.值范围说明 = "" AndAlso 参数数据.值列表.Count = 0 Then Return ""

        Dim 行 As New List(Of String) From {标题 & "："}
        Dim 参数名 = If(参数数据.参数名 <> "", 参数数据.参数名, 标题)
        Dim 说明 As New List(Of String)
        If 参数数据.值范围说明 <> "" Then 说明.Add(参数数据.值范围说明)
        If 参数数据.默认值 <> "" Then 说明.Add("默认 " & 参数数据.默认值)
        行.Add(参数名 & If(说明.Count > 0, "：" & String.Join("；", 说明), ""))

        If 参数数据.值列表.Count > 0 Then 行.Add("可选：" & String.Join("、", 参数数据.值列表))
        Return String.Join(vbCrLf, 行)
    End Function

    Private Function 格式化图片质量(图片质量 As 视频编码器数据库_v6.图片质量参数数据) As String
        If 图片质量 Is Nothing OrElse 图片质量.参数名 = "" Then Return ""

        Dim 说明 As New List(Of String)
        If 图片质量.值范围说明 <> "" Then 说明.Add(图片质量.值范围说明)
        If 图片质量.默认值 <> "" Then 说明.Add("默认 " & 图片质量.默认值)
        Return "图片质量：" & 图片质量.参数名 & If(说明.Count > 0, "：" & String.Join("；", 说明), "")
    End Function

    Private Function 格式化特殊参数列表(标题 As String, 参数列表 As List(Of 视频编码器数据库_v6.编码器特殊参数数据)) As String
        If 参数列表 Is Nothing OrElse 参数列表.Count = 0 Then Return ""

        Dim 行 As New List(Of String) From {标题 & "："}
        For Each 参数 In 参数列表
            If 参数 Is Nothing OrElse 参数.参数名 = "" Then Continue For

            Dim 说明 As New List(Of String)
            If 参数.值范围说明 <> "" Then 说明.Add(参数.值范围说明)
            If 参数.默认值 <> "" Then 说明.Add("默认 " & 参数.默认值)
            If 参数.是否必要 Then 说明.Add("必要")
            If 参数.说明 <> "" Then 说明.Add(参数.说明)
            If 参数.值列表.Count > 0 Then 说明.Add("可选 " & String.Join("、", 参数.值列表))

            行.Add(参数.参数名 & If(说明.Count > 0, "：" & String.Join("；", 说明), ""))
        Next

        If 行.Count = 1 Then Return ""
        Return String.Join(vbCrLf, 行)
    End Function

    Private Function 是质量相关参数(参数 As 视频编码器数据库_v6.编码器特殊参数数据) As Boolean
        If 参数 Is Nothing Then Return False
        Dim 参数名 = If(参数.参数名, "").Trim()
        If 参数名 = "" Then Return False

        Select Case 参数名
            Case "-crf", "-crf_max", "-cq", "-global_quality", "-qp", "-qvbr_quality_level",
                 "-lossless",
                 "-rc", "-rc_mode", "-quality", "-2pass", "-multipass",
                 "-init_qpI", "-init_qpP", "-init_qpB",
                 "-qp_i", "-qp_p", "-qp_b",
                 "-qmin", "-qmax", "-qpmin", "-qpmax",
                 "-min_qp", "-max_qp", "-min_qp_i", "-max_qp_i", "-min_qp_p", "-max_qp_p", "-min_qp_b", "-max_qp_b",
                 "-aq-mode", "-aq_mode", "-aq-strength", "-spatial-aq", "-spatial_aq", "-temporal-aq", "-temporal_aq",
                 "-vbaq", "-pa_paq_mode", "-pa_taq_mode", "-pa_caq_strength", "-pa_lookahead_buffer_depth",
                 "-look_ahead", "-look_ahead_depth", "-rc-lookahead", "-lookahead_level", "-la_depth",
                 "-lag-in-frames", "-auto-alt-ref", "-arnr-strength", "-arnr-maxframes", "-arnr-max-frames", "-arnr-type",
                 "-cplxblur", "-chromaoffset", "-tf_level",
                 "-x264-params", "-x265-params", "-aom-params", "-svtav1-params", "-rav1e-params", "-vvenc-params", "-kvazaar-params", "-xeve-params", "-xavs2-params", "-oapv-params"
                Return True
        End Select

        Return 参数名.IndexOf("qp", StringComparison.OrdinalIgnoreCase) >= 0 OrElse
               参数名.IndexOf("quality", StringComparison.OrdinalIgnoreCase) >= 0
    End Function

    Private Function 是主提示质量参数(参数 As 视频编码器数据库_v6.编码器特殊参数数据) As Boolean
        If 参数 Is Nothing Then Return False

        Select Case If(参数.参数名, "").Trim()
            Case "-crf", "-cq", "-qp", "-global_quality", "-qvbr_quality_level", "-lossless",
                 "-rc", "-rc_mode",
                 "-rc-lookahead", "-look_ahead", "-look_ahead_depth", "-la_depth",
                 "-multipass",
                 "-aq-mode", "-aq_mode", "-aq-strength",
                 "-spatial-aq", "-spatial_aq", "-temporal-aq", "-temporal_aq", "-vbaq"
                Return True
        End Select

        Return False
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
