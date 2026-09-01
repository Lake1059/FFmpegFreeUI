Public Class Form_v6_设置_界面显示
    Private WithEvents MCB_界面主题 As LakeUI.ModernComboBox
    Private WithEvents MCB_窗口圆角 As LakeUI.ModernComboBox
    Private _主题说明 As LakeUI.HtmlColorLabel
    Private _主题行 As LakeUI.ModernPanel
    Private _窗口圆角说明 As LakeUI.HtmlColorLabel
    Private _圆角行 As LakeUI.ModernPanel
    Private _正在加载新增外观设置 As Boolean

    Public Sub New()
        InitializeComponent()
        初始化新增外观控件()
    End Sub

    Private Sub 初始化新增外观控件()
        _主题说明 = 创建设置标题("界面主题", "跟随 Windows 的应用模式，系统切换浅色/深色时界面即时同步")
        MCB_界面主题 = 创建一致尺寸下拉框()
        MCB_界面主题.Name = "MCB_界面主题"
        MCB_界面主题.Items.Add("跟随 Windows")
        MCB_界面主题.Items.Add("始终深色")
        _主题行 = 创建设置行(MCB_界面主题)
        _主题行.Name = "Panel_界面主题"

        _窗口圆角说明 = 创建设置标题("窗口圆角", "Windows 11 默认圆角；可在此切回直角，修改后即时生效")
        MCB_窗口圆角 = 创建一致尺寸下拉框()
        MCB_窗口圆角.Name = "MCB_窗口圆角"
        MCB_窗口圆角.Items.Add("直角")
        MCB_窗口圆角.Items.Add("圆角")
        _圆角行 = 创建设置行(MCB_窗口圆角)
        _圆角行.Name = "Panel_窗口圆角"

        添加到底部(_主题说明)
        添加到底部(_主题行)
        添加到底部(_窗口圆角说明)
        添加到底部(_圆角行)
        ' DockStyle.Top 按逆 Z 序布局：索引越大越靠上。
        ModernPanel1.Controls.SetChildIndex(HtmlColorLabel1, 5)
        ModernPanel1.Controls.SetChildIndex(Panel2, 4)
        ModernPanel1.Controls.SetChildIndex(_主题说明, 3)
        ModernPanel1.Controls.SetChildIndex(_主题行, 2)
        ModernPanel1.Controls.SetChildIndex(_窗口圆角说明, 1)
        ModernPanel1.Controls.SetChildIndex(_圆角行, 0)
    End Sub

    Private Sub 添加到底部(control As Control)
        ModernPanel1.Controls.Add(control)
        ModernPanel1.Controls.SetChildIndex(control, ModernPanel1.Controls.Count - 1)
    End Sub

    Private Function 创建一致尺寸下拉框() As LakeUI.ModernComboBox
        Return New LakeUI.ModernComboBox With {
            .BackColor1 = MCB_全局字体.BackColor1,
            .BorderRadius = MCB_全局字体.BorderRadius,
            .BorderSize = MCB_全局字体.BorderSize,
            .Dock = DockStyle.Left,
            .DropDownBackdropBlurPasses = MCB_全局字体.DropDownBackdropBlurPasses,
            .DropDownBackdropBlurRadius = MCB_全局字体.DropDownBackdropBlurRadius,
            .DropDownBackdropMode = MCB_全局字体.DropDownBackdropMode,
            .DropDownHoverColor = MCB_全局字体.DropDownHoverColor,
            .DropDownItemHeight = MCB_全局字体.DropDownItemHeight,
            .DropDownPadding = MCB_全局字体.DropDownPadding,
            .DropDownSelectedColor = MCB_全局字体.DropDownSelectedColor,
            .DropDownSelectedForeColor = MCB_全局字体.DropDownSelectedForeColor,
            .HoverBackColor1 = MCB_全局字体.HoverBackColor1,
            .Margin = MCB_全局字体.Margin,
            .MaxDropDownItems = MCB_全局字体.MaxDropDownItems,
            .Padding = MCB_全局字体.Padding,
            .SelectionColor = MCB_全局字体.SelectionColor,
            .Size = MCB_全局字体.Size,
            .ToolTipGap = MCB_全局字体.ToolTipGap,
            .ToolTipMaxWidth = MCB_全局字体.ToolTipMaxWidth,
            .ToolTipPadding = MCB_全局字体.ToolTipPadding,
            .WaterTextForeColor = MCB_全局字体.WaterTextForeColor
        }
    End Function

    Private Shared Function 创建设置标题(title As String, description As String) As LakeUI.HtmlColorLabel
        Return New LakeUI.HtmlColorLabel With {
            .AutoSize = True,
            .AutoSizeMode = AutoSizeMode.GrowAndShrink,
            .Dock = DockStyle.Top,
            .ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255)),
            .Padding = New Padding(0, 20, 0, 0),
            .Text = $"<span style=""font-size:13; color:Silver"">{title}</span>   {description}"
        }
    End Function

    Private Shared Function 创建设置行(combo As LakeUI.ModernComboBox) As LakeUI.ModernPanel
        Dim panel As New LakeUI.ModernPanel With {
            .BackColor = Color.Transparent,
            .BackColor1 = Color.Transparent,
            .BorderSize = 0,
            .Dock = DockStyle.Top,
            .Padding = New Padding(0, 10, 0, 0),
            .Size = New Size(712, 42)
        }
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
                _窗口圆角说明.Text = "<span style=""font-size:13; color:Silver"">窗口圆角</span>   Windows 11 默认圆角；可在此切回直角，修改后即时生效"
            Else
                设置_v6.实例对象.窗口圆角 = 0
                MCB_窗口圆角.SelectedIndex = 0
                MCB_窗口圆角.Enabled = False
                _窗口圆角说明.Text = "<span style=""font-size:13; color:Silver"">窗口圆角</span>   当前系统不支持，需要 Windows 11 Build 22000 或更高版本"
            End If
        Finally
            _正在加载新增外观设置 = False
        End Try
    End Sub

    Private Sub Form_v6_设置_界面显示_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MCB_界面主题.Size = MCB_全局字体.Size
        MCB_窗口圆角.Size = MCB_全局字体.Size
        _主题行.Height = Panel2.Height
        _圆角行.Height = Panel2.Height
        _主题行.Padding = Panel2.Padding
        _圆角行.Padding = Panel2.Padding
        Dim topPadding = CInt(Math.Round(20.0R * DeviceDpi / 96.0R))
        _主题说明.Padding = New Padding(0, topPadding, 0, 0)
        _窗口圆角说明.Padding = New Padding(0, topPadding, 0, 0)
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
