Public Class Form_v6_设置_界面显示
    Private _正在加载新增外观设置 As Boolean

    Friend Sub 加载新增外观设置()
        _正在加载新增外观设置 = True
        Try
            MCB_界面主题.SelectedIndex = Math.Clamp(设置_v6.实例对象.界面主题, 0, 2)
            If LakeUI.DwmWindowStyle.IsCornerModeSupported Then
                MCB_窗口圆角.Enabled = True
                MCB_窗口圆角.SelectedIndex = Math.Clamp(设置_v6.实例对象.窗口圆角, 0, 1)
                HtmlColorLabel3.Text = "<span style=""font-size:13; color:Silver"">窗口圆角</span>   Windows 11 默认圆角；可在此切回直角，修改后即时生效"
            Else
                设置_v6.实例对象.窗口圆角 = 0
                MCB_窗口圆角.SelectedIndex = 0
                MCB_窗口圆角.Enabled = False
                HtmlColorLabel3.Text = "<span style=""font-size:13; color:Silver"">窗口圆角</span>   当前系统不支持，需要 Windows 11 Build 22000 或更高版本"
            End If
        Finally
            _正在加载新增外观设置 = False
        End Try
    End Sub

    Private Sub Form_v6_设置_界面显示_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MCB_界面主题.Size = MCB_全局字体.Size
        MCB_窗口圆角.Size = MCB_全局字体.Size
        Dim topPadding = CInt(Math.Round(20.0R * DeviceDpi / 96.0R))
        HtmlColorLabel2.Padding = New Padding(0, topPadding, 0, 0)
        HtmlColorLabel3.Padding = New Padding(0, topPadding, 0, 0)
        ModernPanel1.PerformLayout()
    End Sub

    Private Sub MCB_全局字体_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_全局字体.SelectedIndexChanged
        设置_v6.实例对象.字体 = MCB_全局字体.Text
        字体控制.更新所有控件字体属性()
    End Sub

    Private Sub MCB_界面主题_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_界面主题.SelectedIndexChanged
        If _正在加载新增外观设置 OrElse MCB_界面主题.SelectedIndex < 0 Then Return
        设置_v6.实例对象.界面主题 = MCB_界面主题.SelectedIndex
        界面主题_v6.刷新主题(True)
    End Sub

    Private Sub MCB_窗口圆角_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_窗口圆角.SelectedIndexChanged
        If _正在加载新增外观设置 OrElse MCB_窗口圆角.SelectedIndex < 0 Then Return
        设置_v6.实例对象.窗口圆角 = If(LakeUI.DwmWindowStyle.IsCornerModeSupported, MCB_窗口圆角.SelectedIndex, 0)
        界面主题_v6.应用窗口圆角设置()
    End Sub
End Class
