Public Class Form_v6_参数面板_质量

    Public 所属参数面板对象 As Form_v6_参数面板
    Private 正在同步质量控制方式 As Boolean = False
    Private 预制条目菜单已初始化 As Boolean = False

    Private Sub Form_v6_参数面板_质量_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        初始化质量参数名下拉框()
        初始化预制条目菜单()
    End Sub

    Private Sub 初始化质量参数名下拉框()
        Dim 当前文本 = MCB_质量参数名称.Text
        MCB_质量参数名称.Items.Clear()

        For Each 参数名 In 视频编码器数据库_v6.获取质量参数名列表()
            MCB_质量参数名称.Items.Add(参数名)
        Next

        If 当前文本 <> "" Then MCB_质量参数名称.Text = 当前文本
    End Sub

    Private Sub 初始化预制条目菜单()
        If 预制条目菜单已初始化 Then Exit Sub
        预制条目菜单已初始化 = True

        添加说明项(预制条目菜单, "前向参考帧")
        添加预制条目("-rc-lookahead 前向参考帧数 适用于 Nvidia/libx264", "-rc-lookahead ")
        添加预制条目("-look_ahead_depth 前向参考帧数 适用于 Intel", "-look_ahead_depth ")
        添加预制条目("-la_depth 前向参考帧数 适用于 libsvtav1", "-la_depth ")
        添加分割线(预制条目菜单)

        添加说明项(预制条目菜单, "GOP 和帧类型")
        添加预制条目("-g 关键帧 (i) 间隔", "-g ")
        添加预制条目("-bf 双向预测帧 (b) 数量", "-bf ")
        添加预制条目("-qp_i 关键帧质量", "-qp_i ")
        添加预制条目("-qp_p 前向参考帧质量", "-qp_p ")
        添加预制条目("-qp_b 双向参考帧质量", "-qp_b ")
        添加分割线(预制条目菜单)

        添加说明项(预制条目菜单, "量化控制")
        添加预制条目("-qmin 量化最小值（最高画质）", "-qmin ")
        添加预制条目("-qpmin 量化最小值（最高画质）", "-qpmin ")
        添加预制条目("-qmax 量化最大值（最低画质）", "-qmax ")
        添加预制条目("-qpmax 量化最大值（最低画质）", "-qpmax ")
        添加预制条目("-qcomp 量化系数非线性压缩因子 0.0~1.0", "-qcomp ")
        添加预制条目("-qvbr_quality_level AMD QVBR 质量级别", "-qvbr_quality_level ")
        添加分割线(预制条目菜单)

        添加说明项(预制条目菜单, "编码器专项")
        添加预制条目("-extbrc 1 启用激进比特率分配 适用于 Intel", "-extbrc 1")
        添加预制条目("-spatial-aq 1 启用 NVENC 空间 AQ", "-spatial-aq 1")
        添加预制条目("-temporal-aq 1 启用 NVENC 时间 AQ", "-temporal-aq 1")
        添加预制条目("-level 编码级别 较少使用", "-level ")
    End Sub

    Private Sub 添加说明项(menu As LakeUI.ModernContextMenu, text As String)
        menu.Items.Add(New LakeUI.ModernContextMenu.ModernMenuItem(text) With {.IsDescription = True})
    End Sub

    Private Sub 添加分割线(menu As LakeUI.ModernContextMenu)
        menu.Items.Add(New LakeUI.ModernContextMenu.ModernMenuItem() With {.IsSeparator = True})
    End Sub

    Private Sub 添加预制条目(menuText As String, insertText As String)
        Dim item As New LakeUI.ModernContextMenu.ModernMenuItem(menuText)
        AddHandler item.Click, Sub()
                                   插入进阶质量参数(insertText)
                               End Sub
        预制条目菜单.Items.Add(item)
    End Sub

    Private Sub 插入进阶质量参数(text As String)
        If String.IsNullOrEmpty(text) Then Exit Sub

        Dim insertText = text
        If MTB_进阶质量控制参数.Text <> "" Then
            Dim caret = MTB_进阶质量控制参数.SelectionStart
            Dim needsSpace = caret > 0 AndAlso Not Char.IsWhiteSpace(MTB_进阶质量控制参数.Text(caret - 1))
            If needsSpace Then insertText = " " & insertText
        End If

        MTB_进阶质量控制参数.SelectedText = insertText
        MTB_进阶质量控制参数.Focus()
        通知参数面板刷新()
    End Sub

    Private Sub MCB_全局质量控制方式_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_全局质量控制方式.SelectedIndexChanged
        If 正在同步质量控制方式 Then Exit Sub

        正在同步质量控制方式 = True
        Try
            Select Case MCB_全局质量控制方式.SelectedIndex
                Case 0
                    MCB_质量参数名称.Text = ""
                    MTB_质量值.Text = ""
                Case 1
                    MCB_质量参数名称.Text = "-crf"
                Case 2
                    MCB_质量参数名称.Text = "-cq"
                Case 3
                    MCB_质量参数名称.Text = If(当前视频编码器是AMF(), "", "-qp")
                Case 4, 5
                    MCB_质量参数名称.Text = ""
                    MTB_质量值.Text = ""
            End Select
        Finally
            正在同步质量控制方式 = False
        End Try

        通知参数面板刷新()
    End Sub

    Public Sub 同步当前编码器质量参数名()
        If MCB_全局质量控制方式.SelectedIndex <> 3 Then Exit Sub
        If 当前视频编码器是AMF() AndAlso String.Equals(MCB_质量参数名称.Text.Trim().TrimStart("-"c), "qp", StringComparison.OrdinalIgnoreCase) Then
            MCB_质量参数名称.Text = ""
            通知参数面板刷新()
        End If
    End Sub

    Private Function 当前视频编码器是AMF() As Boolean
        Dim 编码器 = If(所属参数面板对象?.私有界面_视频编码器?.MCB_具体编码器.Text, "").Trim()
        Return 编码器.EndsWith("_amf", StringComparison.OrdinalIgnoreCase)
    End Function

    Private Sub MB_插入预制条目_Click(sender As Object, e As EventArgs) Handles MB_插入预制条目.Click
        初始化预制条目菜单()
        Dim labelPoint = HCL_全局质量控制.PointToScreen(Point.Empty)
        Dim formPoint = FormMain_v6.PointToScreen(Point.Empty)
        预制条目菜单.Show(labelPoint.X, formPoint.Y)
    End Sub

    Private Sub 通知参数面板刷新()
        If 所属参数面板对象 Is Nothing OrElse 所属参数面板对象.抑制自动刷新 Then Exit Sub
        所属参数面板对象.请求刷新参数状态()
    End Sub

End Class
