Public Class Form_v6_设置_LakeUI视觉体验
    Private WithEvents MCB_界面主题 As LakeUI.ModernComboBox
    Private WithEvents MCB_窗口圆角 As LakeUI.ModernComboBox

    Private _窗口圆角说明 As LakeUI.HtmlColorLabel
    Private _正在加载新增外观设置 As Boolean

    Public Sub New()
        InitializeComponent()
        初始化新增外观控件()
    End Sub

    Private Sub 初始化新增外观控件()
        MCB_界面主题 = 创建外观下拉框()
        MCB_界面主题.Name = "MCB_界面主题"
        MCB_界面主题.Items.Add("跟随 Windows")
        MCB_界面主题.Items.Add("始终深色")
        Dim 主题说明 = 创建说明标签("浅色模式：跟随 Windows 的应用模式设置；系统切换浅色/深色时界面即时同步")
        Dim 主题行 = 创建设置行(MCB_界面主题, 主题说明)
        主题行.Name = "Panel_界面主题"

        MCB_窗口圆角 = 创建外观下拉框()
        MCB_窗口圆角.Name = "MCB_窗口圆角"
        MCB_窗口圆角.Items.Add("直角")
        MCB_窗口圆角.Items.Add("圆角")
        _窗口圆角说明 = 创建说明标签("Windows 11 默认圆角；旧版 Windows 不支持此 DWM 能力")
        Dim 圆角行 = 创建设置行(MCB_窗口圆角, _窗口圆角说明)
        圆角行.Name = "Panel_窗口圆角"

        ModernPanel1.Controls.Add(主题行)
        ModernPanel1.Controls.SetChildIndex(主题行, 0)
        ModernPanel1.Controls.Add(圆角行)
        ModernPanel1.Controls.SetChildIndex(圆角行, 0)
    End Sub

    Private Shared Function 创建外观下拉框() As LakeUI.ModernComboBox
        Dim combo As New LakeUI.ModernComboBox With {
            .BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220)),
            .BorderRadius = 10,
            .BorderSize = 0,
            .Dock = DockStyle.Left,
            .DropDownBackdropBlurPasses = 2,
            .DropDownBackdropBlurRadius = 30,
            .DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto,
            .DropDownHoverColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220)),
            .DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay,
            .DropDownPadding = New Padding(10),
            .DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220)),
            .DropDownSelectedForeColor = Color.White,
            .HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220)),
            .Margin = New Padding(2),
            .Padding = New Padding(10, 0, 10, 0),
            .SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220)),
            .Size = New Size(200, 32),
            .ToolTipGap = -1,
            .ToolTipMaxWidth = 350,
            .ToolTipPadding = New Padding(15),
            .WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        }
        Return combo
    End Function

    Private Shared Function 创建说明标签(text As String) As LakeUI.HtmlColorLabel
        Return New LakeUI.HtmlColorLabel With {
            .AutoSizeMode = AutoSizeMode.GrowAndShrink,
            .Dock = DockStyle.Fill,
            .ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255)),
            .Padding = New Padding(10, 0, 0, 0),
            .Text = text,
            .TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        }
    End Function

    Private Shared Function 创建设置行(combo As LakeUI.ModernComboBox, description As LakeUI.HtmlColorLabel) As LakeUI.ModernPanel
        Dim panel As New LakeUI.ModernPanel With {
            .BackColor = Color.Transparent,
            .BackColor1 = Color.Transparent,
            .BorderSize = 0,
            .Dock = DockStyle.Top,
            .Padding = New Padding(0, 10, 0, 0),
            .Size = New Size(702, 42)
        }
        panel.Controls.Add(description)
        panel.Controls.Add(combo)
        Return panel
    End Function

    Friend Sub 加载新增外观设置()
        _正在加载新增外观设置 = True
        Try
            MCB_界面主题.SelectedIndex = Math.Clamp(设置_v6.实例对象.界面主题, 0, 1)

            If LakeUI.DwmWindowStyle.IsCornerModeSupported Then
                MCB_窗口圆角.Enabled = True
                MCB_窗口圆角.SelectedIndex = Math.Clamp(设置_v6.实例对象.窗口圆角, 0, 1)
                _窗口圆角说明.Text = "Windows 11 默认圆角；可在此切回直角，修改后即时生效"
            Else
                设置_v6.实例对象.窗口圆角 = 0
                MCB_窗口圆角.SelectedIndex = 0
                MCB_窗口圆角.Enabled = False
                _窗口圆角说明.Text = "当前系统不支持窗口圆角，需要 Windows 11 Build 22000 或更高版本"
            End If
        Finally
            _正在加载新增外观设置 = False
        End Try
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

    Private Sub MCB_窗口样式_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_窗口样式.SelectedIndexChanged
        设置_v6.实例对象.窗口样式 = MCB_窗口样式.SelectedIndex
    End Sub

    Private Sub MCB_性能计数器_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_性能计数器.SelectedIndexChanged
        设置_v6.实例对象.启用性能计数器 = MCB_性能计数器.SelectedIndex
    End Sub
End Class