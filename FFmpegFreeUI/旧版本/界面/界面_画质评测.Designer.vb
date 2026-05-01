<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class 界面_画质评测
    Inherits System.Windows.Forms.UserControl

    'UserControl 重写释放以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Panel1 = New Panel()
        Panel23 = New Panel()
        UiButton停止 = New Sunny.UI.UIButton()
        Label1 = New Label()
        UiButton从选择处开始 = New Sunny.UI.UIButton()
        Label82 = New Label()
        UiButton全新开始评测 = New Sunny.UI.UIButton()
        Label145 = New Label()
        UiButton重置页面 = New Sunny.UI.UIButton()
        Label31 = New Label()
        Panel2 = New Panel()
        Panel7 = New Panel()
        Panel6 = New Panel()
        UiButton1 = New Sunny.UI.UIButton()
        UiCheckBoxPSNR = New Sunny.UI.UICheckBox()
        UiCheckBox1 = New Sunny.UI.UICheckBox()
        UiCheckBox2 = New Sunny.UI.UICheckBox()
        UiCheckBox3 = New Sunny.UI.UICheckBox()
        Label76 = New Label()
        Panel80 = New Panel()
        UiComboBox输出目录 = New Sunny.UI.UIComboBox()
        Label2 = New Label()
        Label8 = New Label()
        Panel8 = New Panel()
        UiComboBox1 = New Sunny.UI.UIComboBox()
        Label9 = New Label()
        Panel9 = New Panel()
        UiComboBox2 = New Sunny.UI.UIComboBox()
        Label12 = New Label()
        Label3 = New Label()
        Panel3 = New Panel()
        UiComboBox4 = New Sunny.UI.UIComboBox()
        Label4 = New Label()
        Panel10 = New Panel()
        UiComboBox3 = New Sunny.UI.UIComboBox()
        Panel11 = New Panel()
        UiComboBox5 = New Sunny.UI.UIComboBox()
        Label5 = New Label()
        Panel1.SuspendLayout()
        Panel23.SuspendLayout()
        Panel2.SuspendLayout()
        Panel7.SuspendLayout()
        Panel6.SuspendLayout()
        Panel80.SuspendLayout()
        Panel8.SuspendLayout()
        Panel9.SuspendLayout()
        Panel3.SuspendLayout()
        Panel10.SuspendLayout()
        Panel11.SuspendLayout()
        SuspendLayout()
        ' 
        ' Panel1
        ' 
        Panel1.AutoSize = True
        Panel1.BackColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        Panel1.Controls.Add(Panel23)
        Panel1.Controls.Add(Label31)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(0, 0)
        Panel1.Name = "Panel1"
        Panel1.Padding = New Padding(0, 10, 0, 20)
        Panel1.Size = New Size(1000, 90)
        Panel1.TabIndex = 83
        ' 
        ' Panel23
        ' 
        Panel23.Controls.Add(UiButton停止)
        Panel23.Controls.Add(Label1)
        Panel23.Controls.Add(UiButton从选择处开始)
        Panel23.Controls.Add(Label82)
        Panel23.Controls.Add(UiButton全新开始评测)
        Panel23.Controls.Add(Label145)
        Panel23.Controls.Add(UiButton重置页面)
        Panel23.Dock = DockStyle.Top
        Panel23.Location = New Point(0, 40)
        Panel23.Name = "Panel23"
        Panel23.Padding = New Padding(20, 0, 20, 0)
        Panel23.Size = New Size(1000, 30)
        Panel23.TabIndex = 24
        ' 
        ' UiButton停止
        ' 
        UiButton停止.Dock = DockStyle.Left
        UiButton停止.FillColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton停止.FillColor2 = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton停止.FillDisableColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiButton停止.FillHoverColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton停止.FillPressColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiButton停止.FillSelectedColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiButton停止.Font = New Font("微软雅黑", 9.75F)
        UiButton停止.ForeColor = Color.Silver
        UiButton停止.ForeDisableColor = Color.Silver
        UiButton停止.ForeHoverColor = Color.Silver
        UiButton停止.ForePressColor = Color.Silver
        UiButton停止.ForeSelectedColor = Color.Silver
        UiButton停止.Location = New Point(390, 0)
        UiButton停止.MinimumSize = New Size(1, 1)
        UiButton停止.Name = "UiButton停止"
        UiButton停止.RadiusSides = Sunny.UI.UICornerRadiusSides.None
        UiButton停止.RectColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton停止.RectDisableColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton停止.RectHoverColor = Color.DarkGray
        UiButton停止.RectPressColor = Color.FromArgb(CByte(64), CByte(148), CByte(64))
        UiButton停止.RectSelectedColor = Color.DarkGray
        UiButton停止.Size = New Size(80, 30)
        UiButton停止.TabIndex = 90
        UiButton停止.Text = "停止"
        UiButton停止.TipsFont = New Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ' 
        ' Label1
        ' 
        Label1.Dock = DockStyle.Left
        Label1.Location = New Point(380, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(10, 30)
        Label1.TabIndex = 89
        Label1.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' UiButton从选择处开始
        ' 
        UiButton从选择处开始.Dock = DockStyle.Left
        UiButton从选择处开始.FillColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton从选择处开始.FillColor2 = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton从选择处开始.FillDisableColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiButton从选择处开始.FillHoverColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton从选择处开始.FillPressColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiButton从选择处开始.FillSelectedColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiButton从选择处开始.Font = New Font("微软雅黑", 9.75F)
        UiButton从选择处开始.ForeColor = Color.Silver
        UiButton从选择处开始.ForeDisableColor = Color.Silver
        UiButton从选择处开始.ForeHoverColor = Color.Silver
        UiButton从选择处开始.ForePressColor = Color.Silver
        UiButton从选择处开始.ForeSelectedColor = Color.Silver
        UiButton从选择处开始.Location = New Point(260, 0)
        UiButton从选择处开始.MinimumSize = New Size(1, 1)
        UiButton从选择处开始.Name = "UiButton从选择处开始"
        UiButton从选择处开始.RadiusSides = Sunny.UI.UICornerRadiusSides.None
        UiButton从选择处开始.RectColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton从选择处开始.RectDisableColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton从选择处开始.RectHoverColor = Color.DarkGray
        UiButton从选择处开始.RectPressColor = Color.FromArgb(CByte(64), CByte(148), CByte(64))
        UiButton从选择处开始.RectSelectedColor = Color.DarkGray
        UiButton从选择处开始.Size = New Size(120, 30)
        UiButton从选择处开始.TabIndex = 88
        UiButton从选择处开始.Text = "从选择处开始"
        UiButton从选择处开始.TipsFont = New Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ' 
        ' Label82
        ' 
        Label82.Dock = DockStyle.Left
        Label82.Location = New Point(250, 0)
        Label82.Name = "Label82"
        Label82.Size = New Size(10, 30)
        Label82.TabIndex = 87
        Label82.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' UiButton全新开始评测
        ' 
        UiButton全新开始评测.Dock = DockStyle.Left
        UiButton全新开始评测.FillColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton全新开始评测.FillColor2 = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton全新开始评测.FillDisableColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiButton全新开始评测.FillHoverColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton全新开始评测.FillPressColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiButton全新开始评测.FillSelectedColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiButton全新开始评测.Font = New Font("微软雅黑", 9.75F)
        UiButton全新开始评测.ForeColor = Color.Silver
        UiButton全新开始评测.ForeDisableColor = Color.Silver
        UiButton全新开始评测.ForeHoverColor = Color.Silver
        UiButton全新开始评测.ForePressColor = Color.Silver
        UiButton全新开始评测.ForeSelectedColor = Color.Silver
        UiButton全新开始评测.Location = New Point(130, 0)
        UiButton全新开始评测.MinimumSize = New Size(1, 1)
        UiButton全新开始评测.Name = "UiButton全新开始评测"
        UiButton全新开始评测.RadiusSides = Sunny.UI.UICornerRadiusSides.None
        UiButton全新开始评测.RectColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton全新开始评测.RectDisableColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton全新开始评测.RectHoverColor = Color.DarkGray
        UiButton全新开始评测.RectPressColor = Color.FromArgb(CByte(64), CByte(148), CByte(64))
        UiButton全新开始评测.RectSelectedColor = Color.DarkGray
        UiButton全新开始评测.Size = New Size(120, 30)
        UiButton全新开始评测.TabIndex = 86
        UiButton全新开始评测.Text = "全新开始评测"
        UiButton全新开始评测.TipsFont = New Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ' 
        ' Label145
        ' 
        Label145.Dock = DockStyle.Left
        Label145.Location = New Point(120, 0)
        Label145.Name = "Label145"
        Label145.Size = New Size(10, 30)
        Label145.TabIndex = 85
        Label145.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' UiButton重置页面
        ' 
        UiButton重置页面.Dock = DockStyle.Left
        UiButton重置页面.FillColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton重置页面.FillColor2 = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton重置页面.FillDisableColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiButton重置页面.FillHoverColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton重置页面.FillPressColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiButton重置页面.FillSelectedColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiButton重置页面.Font = New Font("微软雅黑", 9.75F)
        UiButton重置页面.ForeColor = Color.Silver
        UiButton重置页面.ForeDisableColor = Color.Silver
        UiButton重置页面.ForeHoverColor = Color.Silver
        UiButton重置页面.ForePressColor = Color.Silver
        UiButton重置页面.ForeSelectedColor = Color.Silver
        UiButton重置页面.Location = New Point(20, 0)
        UiButton重置页面.MinimumSize = New Size(1, 1)
        UiButton重置页面.Name = "UiButton重置页面"
        UiButton重置页面.RadiusSides = Sunny.UI.UICornerRadiusSides.None
        UiButton重置页面.RectColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton重置页面.RectDisableColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton重置页面.RectHoverColor = Color.DarkGray
        UiButton重置页面.RectPressColor = Color.FromArgb(CByte(64), CByte(148), CByte(64))
        UiButton重置页面.RectSelectedColor = Color.DarkGray
        UiButton重置页面.Size = New Size(100, 30)
        UiButton重置页面.TabIndex = 84
        UiButton重置页面.Text = "重置页面"
        UiButton重置页面.TipsFont = New Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ' 
        ' Label31
        ' 
        Label31.AutoSize = True
        Label31.Dock = DockStyle.Top
        Label31.Font = New Font("微软雅黑", 10F)
        Label31.Location = New Point(0, 10)
        Label31.Name = "Label31"
        Label31.Padding = New Padding(16, 0, 16, 10)
        Label31.Size = New Size(545, 30)
        Label31.TabIndex = 0
        Label31.Text = "视频质量评测并不能代表绝对表现，其会受到各种因素的影响，请以人眼视觉为准"
        ' 
        ' Panel2
        ' 
        Panel2.Controls.Add(Panel11)
        Panel2.Controls.Add(Label5)
        Panel2.Controls.Add(Panel10)
        Panel2.Controls.Add(Label4)
        Panel2.Controls.Add(Panel3)
        Panel2.Controls.Add(Label3)
        Panel2.Controls.Add(Label12)
        Panel2.Controls.Add(Panel9)
        Panel2.Controls.Add(Label9)
        Panel2.Controls.Add(Panel8)
        Panel2.Controls.Add(Label8)
        Panel2.Controls.Add(Label2)
        Panel2.Dock = DockStyle.Right
        Panel2.Location = New Point(762, 90)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(238, 560)
        Panel2.TabIndex = 128
        ' 
        ' Panel7
        ' 
        Panel7.Controls.Add(Panel6)
        Panel7.Controls.Add(Label76)
        Panel7.Controls.Add(Panel80)
        Panel7.Dock = DockStyle.Fill
        Panel7.Location = New Point(0, 90)
        Panel7.Name = "Panel7"
        Panel7.Padding = New Padding(0, 0, 0, 20)
        Panel7.Size = New Size(762, 560)
        Panel7.TabIndex = 140
        ' 
        ' Panel6
        ' 
        Panel6.Controls.Add(UiButton1)
        Panel6.Controls.Add(UiCheckBoxPSNR)
        Panel6.Controls.Add(UiCheckBox1)
        Panel6.Controls.Add(UiCheckBox2)
        Panel6.Controls.Add(UiCheckBox3)
        Panel6.Dock = DockStyle.Top
        Panel6.Location = New Point(0, 85)
        Panel6.Name = "Panel6"
        Panel6.Padding = New Padding(20, 0, 0, 0)
        Panel6.Size = New Size(762, 30)
        Panel6.TabIndex = 150
        ' 
        ' UiButton1
        ' 
        UiButton1.Dock = DockStyle.Left
        UiButton1.FillColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton1.FillColor2 = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton1.FillDisableColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiButton1.FillHoverColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton1.FillPressColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiButton1.FillSelectedColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiButton1.Font = New Font("微软雅黑", 9.75F)
        UiButton1.ForeColor = Color.Silver
        UiButton1.ForeDisableColor = Color.Silver
        UiButton1.ForeHoverColor = Color.Silver
        UiButton1.ForePressColor = Color.Silver
        UiButton1.ForeSelectedColor = Color.Silver
        UiButton1.Location = New Point(20, 0)
        UiButton1.MinimumSize = New Size(1, 1)
        UiButton1.Name = "UiButton1"
        UiButton1.RadiusSides = Sunny.UI.UICornerRadiusSides.None
        UiButton1.RectColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton1.RectDisableColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton1.RectHoverColor = Color.DarkGray
        UiButton1.RectPressColor = Color.FromArgb(CByte(64), CByte(148), CByte(64))
        UiButton1.RectSelectedColor = Color.DarkGray
        UiButton1.Size = New Size(233, 30)
        UiButton1.TabIndex = 103
        UiButton1.Text = "添加编码后的文件"
        UiButton1.TipsFont = New Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ' 
        ' UiCheckBoxPSNR
        ' 
        UiCheckBoxPSNR.CheckBoxColor = Color.Silver
        UiCheckBoxPSNR.CheckBoxSize = 20
        UiCheckBoxPSNR.Dock = DockStyle.Right
        UiCheckBoxPSNR.Font = New Font("微软雅黑", 10F)
        UiCheckBoxPSNR.ForeColor = Color.DarkGray
        UiCheckBoxPSNR.Location = New Point(362, 0)
        UiCheckBoxPSNR.MinimumSize = New Size(1, 1)
        UiCheckBoxPSNR.Name = "UiCheckBoxPSNR"
        UiCheckBoxPSNR.Size = New Size(100, 30)
        UiCheckBoxPSNR.TabIndex = 99
        UiCheckBoxPSNR.Text = "PSNR"
        ' 
        ' UiCheckBox1
        ' 
        UiCheckBox1.CheckBoxColor = Color.Silver
        UiCheckBox1.CheckBoxSize = 20
        UiCheckBox1.Dock = DockStyle.Right
        UiCheckBox1.Font = New Font("微软雅黑", 10F)
        UiCheckBox1.ForeColor = Color.DarkGray
        UiCheckBox1.Location = New Point(462, 0)
        UiCheckBox1.MinimumSize = New Size(1, 1)
        UiCheckBox1.Name = "UiCheckBox1"
        UiCheckBox1.Size = New Size(100, 30)
        UiCheckBox1.TabIndex = 100
        UiCheckBox1.Text = "XPSNR"
        ' 
        ' UiCheckBox2
        ' 
        UiCheckBox2.CheckBoxColor = Color.Silver
        UiCheckBox2.CheckBoxSize = 20
        UiCheckBox2.Dock = DockStyle.Right
        UiCheckBox2.Font = New Font("微软雅黑", 10F)
        UiCheckBox2.ForeColor = Color.DarkGray
        UiCheckBox2.Location = New Point(562, 0)
        UiCheckBox2.MinimumSize = New Size(1, 1)
        UiCheckBox2.Name = "UiCheckBox2"
        UiCheckBox2.Size = New Size(100, 30)
        UiCheckBox2.TabIndex = 101
        UiCheckBox2.Text = "SSIM"
        ' 
        ' UiCheckBox3
        ' 
        UiCheckBox3.CheckBoxColor = Color.Silver
        UiCheckBox3.CheckBoxSize = 20
        UiCheckBox3.Dock = DockStyle.Right
        UiCheckBox3.Font = New Font("微软雅黑", 10F)
        UiCheckBox3.ForeColor = Color.DarkGray
        UiCheckBox3.Location = New Point(662, 0)
        UiCheckBox3.MinimumSize = New Size(1, 1)
        UiCheckBox3.Name = "UiCheckBox3"
        UiCheckBox3.Size = New Size(100, 30)
        UiCheckBox3.TabIndex = 102
        UiCheckBox3.Text = "VMAF"
        ' 
        ' Label76
        ' 
        Label76.AutoSize = True
        Label76.Dock = DockStyle.Top
        Label76.Font = New Font("微软雅黑", 10F)
        Label76.ForeColor = Color.CornflowerBlue
        Label76.Location = New Point(0, 50)
        Label76.Name = "Label76"
        Label76.Padding = New Padding(16, 5, 0, 10)
        Label76.Size = New Size(95, 35)
        Label76.TabIndex = 149
        Label76.Text = "读取的信息"
        ' 
        ' Panel80
        ' 
        Panel80.Controls.Add(UiComboBox输出目录)
        Panel80.Dock = DockStyle.Top
        Panel80.Location = New Point(0, 0)
        Panel80.Name = "Panel80"
        Panel80.Padding = New Padding(20, 20, 0, 0)
        Panel80.Size = New Size(762, 50)
        Panel80.TabIndex = 148
        ' 
        ' UiComboBox输出目录
        ' 
        UiComboBox输出目录.AllowDrop = True
        UiComboBox输出目录.DataSource = Nothing
        UiComboBox输出目录.Dock = DockStyle.Fill
        UiComboBox输出目录.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList
        UiComboBox输出目录.FillColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiComboBox输出目录.FillColor2 = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiComboBox输出目录.FillDisableColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiComboBox输出目录.Font = New Font("微软雅黑", 9.75F)
        UiComboBox输出目录.ForeColor = Color.Silver
        UiComboBox输出目录.ForeDisableColor = Color.Silver
        UiComboBox输出目录.ItemFillColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiComboBox输出目录.ItemForeColor = Color.Silver
        UiComboBox输出目录.ItemHeight = 30
        UiComboBox输出目录.ItemHoverColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiComboBox输出目录.ItemRectColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiComboBox输出目录.Items.AddRange(New Object() {"  输出到原目录", "  浏览 ..."})
        UiComboBox输出目录.ItemSelectBackColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiComboBox输出目录.ItemSelectForeColor = Color.Silver
        UiComboBox输出目录.Location = New Point(20, 20)
        UiComboBox输出目录.Margin = New Padding(4, 5, 4, 5)
        UiComboBox输出目录.MaxDropDownItems = 17
        UiComboBox输出目录.MinimumSize = New Size(63, 0)
        UiComboBox输出目录.Name = "UiComboBox输出目录"
        UiComboBox输出目录.Padding = New Padding(0, 0, 30, 2)
        UiComboBox输出目录.RadiusSides = Sunny.UI.UICornerRadiusSides.None
        UiComboBox输出目录.RectColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiComboBox输出目录.RectDisableColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiComboBox输出目录.ScrollBarBackColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiComboBox输出目录.ScrollBarColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiComboBox输出目录.ScrollBarHandleWidth = 20
        UiComboBox输出目录.ScrollBarStyleInherited = False
        UiComboBox输出目录.Size = New Size(742, 30)
        UiComboBox输出目录.Style = Sunny.UI.UIStyle.Custom
        UiComboBox输出目录.SymbolSize = 24
        UiComboBox输出目录.TabIndex = 93
        UiComboBox输出目录.TextAlignment = ContentAlignment.MiddleLeft
        UiComboBox输出目录.Watermark = "选择原视频文件"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Dock = DockStyle.Top
        Label2.Font = New Font("微软雅黑", 12F)
        Label2.Location = New Point(0, 0)
        Label2.Name = "Label2"
        Label2.Padding = New Padding(16, 16, 0, 0)
        Label2.Size = New Size(90, 37)
        Label2.TabIndex = 148
        Label2.Text = "通用配置"
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Dock = DockStyle.Top
        Label8.Font = New Font("微软雅黑", 10F)
        Label8.ForeColor = Color.DarkGray
        Label8.Location = New Point(0, 37)
        Label8.Name = "Label8"
        Label8.Padding = New Padding(16, 10, 0, 5)
        Label8.Size = New Size(81, 35)
        Label8.TabIndex = 149
        Label8.Text = "评测时长"
        ' 
        ' Panel8
        ' 
        Panel8.AutoSize = True
        Panel8.Controls.Add(UiComboBox1)
        Panel8.Dock = DockStyle.Top
        Panel8.Location = New Point(0, 72)
        Panel8.Name = "Panel8"
        Panel8.Padding = New Padding(20, 0, 20, 0)
        Panel8.Size = New Size(238, 30)
        Panel8.TabIndex = 151
        ' 
        ' UiComboBox1
        ' 
        UiComboBox1.AllowDrop = True
        UiComboBox1.DataSource = Nothing
        UiComboBox1.Dock = DockStyle.Top
        UiComboBox1.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList
        UiComboBox1.FillColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiComboBox1.FillColor2 = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiComboBox1.FillDisableColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiComboBox1.Font = New Font("微软雅黑", 9.75F)
        UiComboBox1.ForeColor = Color.Silver
        UiComboBox1.ForeDisableColor = Color.Silver
        UiComboBox1.ItemFillColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiComboBox1.ItemForeColor = Color.Silver
        UiComboBox1.ItemHeight = 30
        UiComboBox1.ItemHoverColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiComboBox1.ItemRectColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiComboBox1.Items.AddRange(New Object() {"  输出到原目录", "  浏览 ..."})
        UiComboBox1.ItemSelectBackColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiComboBox1.ItemSelectForeColor = Color.Silver
        UiComboBox1.Location = New Point(20, 0)
        UiComboBox1.Margin = New Padding(4, 5, 4, 5)
        UiComboBox1.MaxDropDownItems = 17
        UiComboBox1.MinimumSize = New Size(63, 0)
        UiComboBox1.Name = "UiComboBox1"
        UiComboBox1.Padding = New Padding(0, 0, 30, 2)
        UiComboBox1.RadiusSides = Sunny.UI.UICornerRadiusSides.None
        UiComboBox1.RectColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiComboBox1.RectDisableColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiComboBox1.ScrollBarBackColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiComboBox1.ScrollBarColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiComboBox1.ScrollBarHandleWidth = 20
        UiComboBox1.ScrollBarStyleInherited = False
        UiComboBox1.Size = New Size(198, 30)
        UiComboBox1.Style = Sunny.UI.UIStyle.Custom
        UiComboBox1.SymbolSize = 24
        UiComboBox1.TabIndex = 151
        UiComboBox1.TextAlignment = ContentAlignment.MiddleLeft
        UiComboBox1.Watermark = "评测长度"
        ' 
        ' Label9
        ' 
        Label9.AutoSize = True
        Label9.Dock = DockStyle.Top
        Label9.Font = New Font("微软雅黑", 10F)
        Label9.ForeColor = Color.DarkGray
        Label9.Location = New Point(0, 102)
        Label9.Name = "Label9"
        Label9.Padding = New Padding(16, 10, 0, 5)
        Label9.Size = New Size(123, 35)
        Label9.TabIndex = 152
        Label9.Text = "从指定位置开始"
        ' 
        ' Panel9
        ' 
        Panel9.AutoSize = True
        Panel9.Controls.Add(UiComboBox2)
        Panel9.Dock = DockStyle.Top
        Panel9.Location = New Point(0, 137)
        Panel9.Name = "Panel9"
        Panel9.Padding = New Padding(20, 0, 20, 0)
        Panel9.Size = New Size(238, 30)
        Panel9.TabIndex = 153
        ' 
        ' UiComboBox2
        ' 
        UiComboBox2.AllowDrop = True
        UiComboBox2.DataSource = Nothing
        UiComboBox2.Dock = DockStyle.Top
        UiComboBox2.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList
        UiComboBox2.FillColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiComboBox2.FillColor2 = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiComboBox2.FillDisableColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiComboBox2.Font = New Font("微软雅黑", 9.75F)
        UiComboBox2.ForeColor = Color.Silver
        UiComboBox2.ForeDisableColor = Color.Silver
        UiComboBox2.ItemFillColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiComboBox2.ItemForeColor = Color.Silver
        UiComboBox2.ItemHeight = 30
        UiComboBox2.ItemHoverColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiComboBox2.ItemRectColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiComboBox2.Items.AddRange(New Object() {"  输出到原目录", "  浏览 ..."})
        UiComboBox2.ItemSelectBackColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiComboBox2.ItemSelectForeColor = Color.Silver
        UiComboBox2.Location = New Point(20, 0)
        UiComboBox2.Margin = New Padding(4, 5, 4, 5)
        UiComboBox2.MaxDropDownItems = 17
        UiComboBox2.MinimumSize = New Size(63, 0)
        UiComboBox2.Name = "UiComboBox2"
        UiComboBox2.Padding = New Padding(0, 0, 30, 2)
        UiComboBox2.RadiusSides = Sunny.UI.UICornerRadiusSides.None
        UiComboBox2.RectColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiComboBox2.RectDisableColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiComboBox2.ScrollBarBackColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiComboBox2.ScrollBarColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiComboBox2.ScrollBarHandleWidth = 20
        UiComboBox2.ScrollBarStyleInherited = False
        UiComboBox2.Size = New Size(198, 30)
        UiComboBox2.Style = Sunny.UI.UIStyle.Custom
        UiComboBox2.SymbolSize = 24
        UiComboBox2.TabIndex = 96
        UiComboBox2.TextAlignment = ContentAlignment.MiddleLeft
        UiComboBox2.Watermark = "从时长开始"
        ' 
        ' Label12
        ' 
        Label12.AutoSize = True
        Label12.Dock = DockStyle.Top
        Label12.Font = New Font("微软雅黑", 12F)
        Label12.Location = New Point(0, 167)
        Label12.Name = "Label12"
        Label12.Padding = New Padding(16, 16, 0, 0)
        Label12.Size = New Size(110, 37)
        Label12.TabIndex = 154
        Label12.Text = "VMAF 配置"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Dock = DockStyle.Top
        Label3.Font = New Font("微软雅黑", 10F)
        Label3.ForeColor = Color.DarkGray
        Label3.Location = New Point(0, 204)
        Label3.Name = "Label3"
        Label3.Padding = New Padding(16, 10, 0, 5)
        Label3.Size = New Size(81, 35)
        Label3.TabIndex = 155
        Label3.Text = "模型选择"
        ' 
        ' Panel3
        ' 
        Panel3.AutoSize = True
        Panel3.Controls.Add(UiComboBox4)
        Panel3.Dock = DockStyle.Top
        Panel3.Location = New Point(0, 239)
        Panel3.Name = "Panel3"
        Panel3.Padding = New Padding(20, 0, 20, 0)
        Panel3.Size = New Size(238, 30)
        Panel3.TabIndex = 156
        ' 
        ' UiComboBox4
        ' 
        UiComboBox4.AllowDrop = True
        UiComboBox4.DataSource = Nothing
        UiComboBox4.Dock = DockStyle.Top
        UiComboBox4.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList
        UiComboBox4.FillColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiComboBox4.FillColor2 = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiComboBox4.FillDisableColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiComboBox4.Font = New Font("微软雅黑", 9.75F)
        UiComboBox4.ForeColor = Color.Silver
        UiComboBox4.ForeDisableColor = Color.Silver
        UiComboBox4.ItemFillColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiComboBox4.ItemForeColor = Color.Silver
        UiComboBox4.ItemHeight = 30
        UiComboBox4.ItemHoverColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiComboBox4.ItemRectColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiComboBox4.Items.AddRange(New Object() {"  输出到原目录", "  浏览 ..."})
        UiComboBox4.ItemSelectBackColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiComboBox4.ItemSelectForeColor = Color.Silver
        UiComboBox4.Location = New Point(20, 0)
        UiComboBox4.Margin = New Padding(4, 5, 4, 5)
        UiComboBox4.MaxDropDownItems = 17
        UiComboBox4.MinimumSize = New Size(63, 0)
        UiComboBox4.Name = "UiComboBox4"
        UiComboBox4.Padding = New Padding(0, 0, 30, 2)
        UiComboBox4.RadiusSides = Sunny.UI.UICornerRadiusSides.None
        UiComboBox4.RectColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiComboBox4.RectDisableColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiComboBox4.ScrollBarBackColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiComboBox4.ScrollBarColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiComboBox4.ScrollBarHandleWidth = 20
        UiComboBox4.ScrollBarStyleInherited = False
        UiComboBox4.Size = New Size(198, 30)
        UiComboBox4.Style = Sunny.UI.UIStyle.Custom
        UiComboBox4.SymbolSize = 24
        UiComboBox4.TabIndex = 94
        UiComboBox4.TextAlignment = ContentAlignment.MiddleLeft
        UiComboBox4.Watermark = ""
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Dock = DockStyle.Top
        Label4.Font = New Font("微软雅黑", 10F)
        Label4.ForeColor = Color.DarkGray
        Label4.Location = New Point(0, 269)
        Label4.Name = "Label4"
        Label4.Padding = New Padding(16, 10, 0, 5)
        Label4.Size = New Size(81, 35)
        Label4.TabIndex = 157
        Label4.Text = "统计方式"
        ' 
        ' Panel10
        ' 
        Panel10.AutoSize = True
        Panel10.Controls.Add(UiComboBox3)
        Panel10.Dock = DockStyle.Top
        Panel10.Location = New Point(0, 304)
        Panel10.Name = "Panel10"
        Panel10.Padding = New Padding(20, 0, 20, 0)
        Panel10.Size = New Size(238, 30)
        Panel10.TabIndex = 158
        ' 
        ' UiComboBox3
        ' 
        UiComboBox3.AllowDrop = True
        UiComboBox3.DataSource = Nothing
        UiComboBox3.Dock = DockStyle.Top
        UiComboBox3.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList
        UiComboBox3.FillColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiComboBox3.FillColor2 = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiComboBox3.FillDisableColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiComboBox3.Font = New Font("微软雅黑", 9.75F)
        UiComboBox3.ForeColor = Color.Silver
        UiComboBox3.ForeDisableColor = Color.Silver
        UiComboBox3.ItemFillColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiComboBox3.ItemForeColor = Color.Silver
        UiComboBox3.ItemHeight = 30
        UiComboBox3.ItemHoverColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiComboBox3.ItemRectColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiComboBox3.Items.AddRange(New Object() {"  输出到原目录", "  浏览 ..."})
        UiComboBox3.ItemSelectBackColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiComboBox3.ItemSelectForeColor = Color.Silver
        UiComboBox3.Location = New Point(20, 0)
        UiComboBox3.Margin = New Padding(4, 5, 4, 5)
        UiComboBox3.MaxDropDownItems = 17
        UiComboBox3.MinimumSize = New Size(63, 0)
        UiComboBox3.Name = "UiComboBox3"
        UiComboBox3.Padding = New Padding(0, 0, 30, 2)
        UiComboBox3.RadiusSides = Sunny.UI.UICornerRadiusSides.None
        UiComboBox3.RectColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiComboBox3.RectDisableColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiComboBox3.ScrollBarBackColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiComboBox3.ScrollBarColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiComboBox3.ScrollBarHandleWidth = 20
        UiComboBox3.ScrollBarStyleInherited = False
        UiComboBox3.Size = New Size(198, 30)
        UiComboBox3.Style = Sunny.UI.UIStyle.Custom
        UiComboBox3.SymbolSize = 24
        UiComboBox3.TabIndex = 94
        UiComboBox3.TextAlignment = ContentAlignment.MiddleLeft
        UiComboBox3.Watermark = ""
        ' 
        ' Panel11
        ' 
        Panel11.AutoSize = True
        Panel11.Controls.Add(UiComboBox5)
        Panel11.Dock = DockStyle.Top
        Panel11.Location = New Point(0, 369)
        Panel11.Name = "Panel11"
        Panel11.Padding = New Padding(20, 0, 20, 0)
        Panel11.Size = New Size(238, 30)
        Panel11.TabIndex = 160
        ' 
        ' UiComboBox5
        ' 
        UiComboBox5.AllowDrop = True
        UiComboBox5.DataSource = Nothing
        UiComboBox5.Dock = DockStyle.Top
        UiComboBox5.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList
        UiComboBox5.FillColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiComboBox5.FillColor2 = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiComboBox5.FillDisableColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiComboBox5.Font = New Font("微软雅黑", 9.75F)
        UiComboBox5.ForeColor = Color.Silver
        UiComboBox5.ForeDisableColor = Color.Silver
        UiComboBox5.ItemFillColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiComboBox5.ItemForeColor = Color.Silver
        UiComboBox5.ItemHeight = 30
        UiComboBox5.ItemHoverColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiComboBox5.ItemRectColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiComboBox5.Items.AddRange(New Object() {"  输出到原目录", "  浏览 ..."})
        UiComboBox5.ItemSelectBackColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiComboBox5.ItemSelectForeColor = Color.Silver
        UiComboBox5.Location = New Point(20, 0)
        UiComboBox5.Margin = New Padding(4, 5, 4, 5)
        UiComboBox5.MaxDropDownItems = 17
        UiComboBox5.MinimumSize = New Size(63, 0)
        UiComboBox5.Name = "UiComboBox5"
        UiComboBox5.Padding = New Padding(0, 0, 30, 2)
        UiComboBox5.RadiusSides = Sunny.UI.UICornerRadiusSides.None
        UiComboBox5.RectColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiComboBox5.RectDisableColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiComboBox5.ScrollBarBackColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiComboBox5.ScrollBarColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiComboBox5.ScrollBarHandleWidth = 20
        UiComboBox5.ScrollBarStyleInherited = False
        UiComboBox5.Size = New Size(198, 30)
        UiComboBox5.Style = Sunny.UI.UIStyle.Custom
        UiComboBox5.SymbolSize = 24
        UiComboBox5.TabIndex = 94
        UiComboBox5.TextAlignment = ContentAlignment.MiddleLeft
        UiComboBox5.Watermark = ""
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Dock = DockStyle.Top
        Label5.Font = New Font("微软雅黑", 10F)
        Label5.ForeColor = Color.DarkGray
        Label5.Location = New Point(0, 334)
        Label5.Name = "Label5"
        Label5.Padding = New Padding(16, 10, 0, 5)
        Label5.Size = New Size(53, 35)
        Label5.TabIndex = 159
        Label5.Text = "抽样"
        ' 
        ' 界面_画质评测
        ' 
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        Controls.Add(Panel7)
        Controls.Add(Panel2)
        Controls.Add(Panel1)
        Font = New Font("微软雅黑", 10F)
        ForeColor = Color.Silver
        Name = "界面_画质评测"
        Size = New Size(1000, 650)
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        Panel23.ResumeLayout(False)
        Panel2.ResumeLayout(False)
        Panel2.PerformLayout()
        Panel7.ResumeLayout(False)
        Panel7.PerformLayout()
        Panel6.ResumeLayout(False)
        Panel80.ResumeLayout(False)
        Panel8.ResumeLayout(False)
        Panel9.ResumeLayout(False)
        Panel3.ResumeLayout(False)
        Panel10.ResumeLayout(False)
        Panel11.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label31 As Label
    Friend WithEvents Panel23 As Panel
    Friend WithEvents UiButton从选择处开始 As Sunny.UI.UIButton
    Friend WithEvents Label82 As Label
    Friend WithEvents UiButton全新开始评测 As Sunny.UI.UIButton
    Friend WithEvents Label145 As Label
    Friend WithEvents UiButton重置页面 As Sunny.UI.UIButton
    Friend WithEvents UiButton停止 As Sunny.UI.UIButton
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Label76 As Label
    Friend WithEvents Panel80 As Panel
    Friend WithEvents UiComboBox输出目录 As Sunny.UI.UIComboBox
    Friend WithEvents UiCheckBoxPSNR As Sunny.UI.UICheckBox
    Friend WithEvents UiButton1 As Sunny.UI.UIButton
    Friend WithEvents UiCheckBox1 As Sunny.UI.UICheckBox
    Friend WithEvents UiCheckBox2 As Sunny.UI.UICheckBox
    Friend WithEvents UiCheckBox3 As Sunny.UI.UICheckBox
    Friend WithEvents Panel11 As Panel
    Friend WithEvents UiComboBox5 As Sunny.UI.UIComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Panel10 As Panel
    Friend WithEvents UiComboBox3 As Sunny.UI.UIComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents UiComboBox4 As Sunny.UI.UIComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Panel9 As Panel
    Friend WithEvents UiComboBox2 As Sunny.UI.UIComboBox
    Friend WithEvents Label9 As Label
    Friend WithEvents Panel8 As Panel
    Friend WithEvents UiComboBox1 As Sunny.UI.UIComboBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Label2 As Label

End Class
