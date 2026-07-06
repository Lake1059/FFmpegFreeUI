<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_v6_起始页面
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_v6_起始页面))
        ModernPanel2 = New LakeUI.ModernPanel()
        Panel1 = New Panel()
        MCB_清理内存 = New LakeUI.ModernComboBox()
        HtmlColorLabel2 = New LakeUI.HtmlColorLabel()
        HtmlColorLabel1 = New LakeUI.HtmlColorLabel()
        ModernPanel3 = New LakeUI.ModernPanel()
        ModernPanel1 = New LakeUI.ModernPanel()
        ModernPanel5 = New LakeUI.ModernPanel()
        ModernButton16 = New LakeUI.ModernButton()
        ModernButton15 = New LakeUI.ModernButton()
        HtmlColorLabel5 = New LakeUI.HtmlColorLabel()
        MB_更新器更新 = New LakeUI.ModernButton()
        MB_软件本体更新 = New LakeUI.ModernButton()
        HtmlColorLabel4 = New LakeUI.HtmlColorLabel()
        JustEmptyControl3 = New LakeUI.JustEmptyControl()
        MP_新闻列表 = New LakeUI.ModernPanel()
        HtmlColorLabel6 = New LakeUI.HtmlColorLabel()
        JustEmptyControl2 = New LakeUI.JustEmptyControl()
        ModernPanel4 = New LakeUI.ModernPanel()
        MB_FFmpegFull = New LakeUI.ModernButton()
        MB_LakeUI = New LakeUI.ModernButton()
        MB_终末诗 = New LakeUI.ModernButton()
        MB_爱发电 = New LakeUI.ModernButton()
        MB_哔哩哔哩 = New LakeUI.ModernButton()
        MB_官网 = New LakeUI.ModernButton()
        MB_GitHub = New LakeUI.ModernButton()
        MB_AI提示 = New LakeUI.ModernButton()
        HtmlColorLabel3 = New LakeUI.HtmlColorLabel()
        JustEmptyControl1 = New LakeUI.JustEmptyControl()
        ModernPanel2.SuspendLayout()
        Panel1.SuspendLayout()
        ModernPanel1.SuspendLayout()
        ModernPanel5.SuspendLayout()
        MP_新闻列表.SuspendLayout()
        ModernPanel4.SuspendLayout()
        SuspendLayout()
        ' 
        ' ModernPanel2
        ' 
        ModernPanel2.BackColor1 = Color.Transparent
        ModernPanel2.BorderRadius = 10
        ModernPanel2.BorderSize = 0
        ModernPanel2.Controls.Add(Panel1)
        ModernPanel2.Controls.Add(HtmlColorLabel1)
        ModernPanel2.Controls.Add(ModernPanel3)
        ModernPanel2.Dock = DockStyle.Top
        ModernPanel2.Location = New Point(15, 15)
        ModernPanel2.Name = "ModernPanel2"
        ModernPanel2.OverlayColor = Color.FromArgb(CByte(120), CByte(0), CByte(0), CByte(0))
        ModernPanel2.Padding = New Padding(15)
        ModernPanel2.Size = New Size(934, 100)
        ModernPanel2.TabIndex = 0
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(MCB_清理内存)
        Panel1.Controls.Add(HtmlColorLabel2)
        Panel1.Dock = DockStyle.Right
        Panel1.Location = New Point(714, 20)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(200, 60)
        Panel1.TabIndex = 14
        ' 
        ' MCB_清理内存
        ' 
        MCB_清理内存.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_清理内存.BorderRadius = 10
        MCB_清理内存.BorderSize = 0
        MCB_清理内存.Dock = DockStyle.Bottom
        MCB_清理内存.DropDownBackdropBlurPasses = 2
        MCB_清理内存.DropDownBackdropBlurRadius = 30
        MCB_清理内存.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_清理内存.DropDownHoverAnimationDuration = 0
        MCB_清理内存.DropDownHoverColor = Color.FromArgb(CByte(80), CByte(0), CByte(0), CByte(0))
        MCB_清理内存.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_清理内存.DropDownPadding = New Padding(10)
        MCB_清理内存.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_清理内存.DropDownSelectedForeColor = Color.White
        MCB_清理内存.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_清理内存.Items.Add("清理 3FUI 内存 (GC)")
        MCB_清理内存.Items.Add("清理 3FUI 内存 (内核)")
        MCB_清理内存.Items.Add("D2D 预算清理")
        MCB_清理内存.Items.Add("D2D 激进清理")
        MCB_清理内存.Items.Add("D2D 全量清理")
        MCB_清理内存.Location = New Point(0, 28)
        MCB_清理内存.Margin = New Padding(2, 2, 2, 2)
        MCB_清理内存.MaxDropDownItems = 15
        MCB_清理内存.Name = "MCB_清理内存"
        MCB_清理内存.Padding = New Padding(10, 0, 10, 0)
        MCB_清理内存.Size = New Size(200, 32)
        MCB_清理内存.TabIndex = 16
        MCB_清理内存.ToolTipGap = -1
        MCB_清理内存.ToolTipMaxWidth = 350
        MCB_清理内存.ToolTipPadding = New Padding(15)
        MCB_清理内存.WaterText = "清理占用"
        MCB_清理内存.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' HtmlColorLabel2
        ' 
        HtmlColorLabel2.AutoSize = True
        HtmlColorLabel2.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel2.Dock = DockStyle.Fill
        HtmlColorLabel2.Location = New Point(0, 0)
        HtmlColorLabel2.Margin = New Padding(2)
        HtmlColorLabel2.Name = "HtmlColorLabel2"
        HtmlColorLabel2.Size = New Size(200, 60)
        HtmlColorLabel2.TabIndex = 14
        HtmlColorLabel2.Text = "@湖边的稻草 (1059 Studio)"
        HtmlColorLabel2.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.TopRight
        ' 
        ' HtmlColorLabel1
        ' 
        HtmlColorLabel1.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel1.Dock = DockStyle.Fill
        HtmlColorLabel1.LineSpacing = 5
        HtmlColorLabel1.Location = New Point(80, 20)
        HtmlColorLabel1.Margin = New Padding(2)
        HtmlColorLabel1.Name = "HtmlColorLabel1"
        HtmlColorLabel1.Padding = New Padding(15, 0, 0, 0)
        HtmlColorLabel1.Size = New Size(834, 60)
        HtmlColorLabel1.TabIndex = 1
        HtmlColorLabel1.Text = "主标题<br>副标题"
        HtmlColorLabel1.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        ' 
        ' ModernPanel3
        ' 
        ModernPanel3.BackColor1 = Color.Transparent
        ModernPanel3.BorderColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        ModernPanel3.BorderRadius = 5
        ModernPanel3.BorderSize = 0
        ModernPanel3.Dock = DockStyle.Left
        ModernPanel3.Image = CType(resources.GetObject("ModernPanel3.Image"), Image)
        ModernPanel3.Location = New Point(20, 20)
        ModernPanel3.Name = "ModernPanel3"
        ModernPanel3.Size = New Size(60, 60)
        ModernPanel3.TabIndex = 0
        ' 
        ' ModernPanel1
        ' 
        ModernPanel1.BackColor = Color.Transparent
        ModernPanel1.BackColor1 = Color.Transparent
        ModernPanel1.BorderRoundCorners = New LakeUI.RoundCorners(False, False, False, False)
        ModernPanel1.BorderSize = 0
        ModernPanel1.Controls.Add(ModernPanel5)
        ModernPanel1.Controls.Add(JustEmptyControl3)
        ModernPanel1.Controls.Add(MP_新闻列表)
        ModernPanel1.Controls.Add(JustEmptyControl2)
        ModernPanel1.Controls.Add(ModernPanel4)
        ModernPanel1.Controls.Add(JustEmptyControl1)
        ModernPanel1.Controls.Add(ModernPanel2)
        ModernPanel1.Dock = DockStyle.Fill
        ModernPanel1.Location = New Point(0, 0)
        ModernPanel1.Name = "ModernPanel1"
        ModernPanel1.Padding = New Padding(15)
        ModernPanel1.Size = New Size(964, 645)
        ModernPanel1.TabIndex = 1
        ' 
        ' ModernPanel5
        ' 
        ModernPanel5.BackColor1 = Color.Transparent
        ModernPanel5.BorderColor = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        ModernPanel5.BorderRadius = 10
        ModernPanel5.BorderSize = 0
        ModernPanel5.Controls.Add(ModernButton16)
        ModernPanel5.Controls.Add(ModernButton15)
        ModernPanel5.Controls.Add(HtmlColorLabel5)
        ModernPanel5.Controls.Add(MB_更新器更新)
        ModernPanel5.Controls.Add(MB_软件本体更新)
        ModernPanel5.Controls.Add(HtmlColorLabel4)
        ModernPanel5.Dock = DockStyle.Fill
        ModernPanel5.Location = New Point(330, 130)
        ModernPanel5.Name = "ModernPanel5"
        ModernPanel5.OverlayColor = Color.FromArgb(CByte(120), CByte(0), CByte(0), CByte(0))
        ModernPanel5.Padding = New Padding(15)
        ModernPanel5.Size = New Size(304, 500)
        ModernPanel5.TabIndex = 4
        ' 
        ' ModernButton16
        ' 
        ModernButton16.AnimationDuration = 0
        ModernButton16.BackColor1 = Color.Transparent
        ModernButton16.BorderRadius = 10
        ModernButton16.BorderSize = 0
        ModernButton16.Dock = DockStyle.Top
        ModernButton16.HoverBackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernButton16.Location = New Point(20, 254)
        ModernButton16.Margin = New Padding(2)
        ModernButton16.Name = "ModernButton16"
        ModernButton16.PressedBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        ModernButton16.Size = New Size(264, 50)
        ModernButton16.SubText = "尚未准备好"
        ModernButton16.SubTextForeColor = Color.Peru
        ModernButton16.TabIndex = 24
        ModernButton16.Text = "自建社区应用组件"
        ModernButton16.TextAlign = LakeUI.ModernButton.TextAlignEnum.Left
        ' 
        ' ModernButton15
        ' 
        ModernButton15.AnimationDuration = 0
        ModernButton15.BackColor1 = Color.Transparent
        ModernButton15.BorderRadius = 10
        ModernButton15.BorderSize = 0
        ModernButton15.Dock = DockStyle.Top
        ModernButton15.HoverBackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernButton15.Location = New Point(20, 204)
        ModernButton15.Margin = New Padding(2)
        ModernButton15.Name = "ModernButton15"
        ModernButton15.PressedBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        ModernButton15.Size = New Size(264, 50)
        ModernButton15.SubText = "尚未准备好"
        ModernButton15.SubTextForeColor = Color.Peru
        ModernButton15.TabIndex = 23
        ModernButton15.Text = "可视化剪辑区间交互"
        ModernButton15.TextAlign = LakeUI.ModernButton.TextAlignEnum.Left
        ' 
        ' HtmlColorLabel5
        ' 
        HtmlColorLabel5.AutoSize = True
        HtmlColorLabel5.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel5.Dock = DockStyle.Top
        HtmlColorLabel5.Font = New Font("Microsoft YaHei UI", 12F)
        HtmlColorLabel5.Location = New Point(20, 152)
        HtmlColorLabel5.Margin = New Padding(2)
        HtmlColorLabel5.Name = "HtmlColorLabel5"
        HtmlColorLabel5.Padding = New Padding(0, 20, 0, 10)
        HtmlColorLabel5.Size = New Size(264, 52)
        HtmlColorLabel5.TabIndex = 3
        HtmlColorLabel5.Text = "分离的组件"
        HtmlColorLabel5.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.Center
        ' 
        ' MB_更新器更新
        ' 
        MB_更新器更新.AnimationDuration = 0
        MB_更新器更新.BackColor1 = Color.Transparent
        MB_更新器更新.BorderRadius = 10
        MB_更新器更新.BorderSize = 0
        MB_更新器更新.Dock = DockStyle.Top
        MB_更新器更新.HoverBackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_更新器更新.Location = New Point(20, 102)
        MB_更新器更新.Margin = New Padding(2)
        MB_更新器更新.Name = "MB_更新器更新"
        MB_更新器更新.PressedBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_更新器更新.Size = New Size(264, 50)
        MB_更新器更新.SubText = "体积/版本/下载速度/百分比"
        MB_更新器更新.SubTextForeColor = Color.CornflowerBlue
        MB_更新器更新.TabIndex = 21
        MB_更新器更新.Text = "控制台更新程序 0.0.1"
        MB_更新器更新.TextAlign = LakeUI.ModernButton.TextAlignEnum.Left
        ' 
        ' MB_软件本体更新
        ' 
        MB_软件本体更新.AnimationDuration = 0
        MB_软件本体更新.BackColor1 = Color.Transparent
        MB_软件本体更新.BorderRadius = 10
        MB_软件本体更新.BorderSize = 0
        MB_软件本体更新.Dock = DockStyle.Top
        MB_软件本体更新.HoverBackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_软件本体更新.Location = New Point(20, 52)
        MB_软件本体更新.Margin = New Padding(2)
        MB_软件本体更新.Name = "MB_软件本体更新"
        MB_软件本体更新.PressedBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_软件本体更新.Size = New Size(264, 50)
        MB_软件本体更新.SubText = "体积/版本/下载速度/百分比"
        MB_软件本体更新.SubTextForeColor = Color.YellowGreen
        MB_软件本体更新.TabIndex = 20
        MB_软件本体更新.Text = "[更新来源] 云端版本 6.0.0"
        MB_软件本体更新.TextAlign = LakeUI.ModernButton.TextAlignEnum.Left
        ' 
        ' HtmlColorLabel4
        ' 
        HtmlColorLabel4.AutoSize = True
        HtmlColorLabel4.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel4.Dock = DockStyle.Top
        HtmlColorLabel4.Font = New Font("Microsoft YaHei UI", 12F)
        HtmlColorLabel4.Location = New Point(20, 20)
        HtmlColorLabel4.Margin = New Padding(2)
        HtmlColorLabel4.Name = "HtmlColorLabel4"
        HtmlColorLabel4.Padding = New Padding(0, 0, 0, 10)
        HtmlColorLabel4.Size = New Size(264, 32)
        HtmlColorLabel4.TabIndex = 1
        HtmlColorLabel4.Text = "软件更新"
        HtmlColorLabel4.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.Center
        ' 
        ' JustEmptyControl3
        ' 
        JustEmptyControl3.BackColor = Color.Transparent
        JustEmptyControl3.Dock = DockStyle.Right
        JustEmptyControl3.Location = New Point(634, 130)
        JustEmptyControl3.Name = "JustEmptyControl3"
        JustEmptyControl3.Size = New Size(15, 500)
        JustEmptyControl3.TabIndex = 6
        ' 
        ' MP_新闻列表
        ' 
        MP_新闻列表.BackColor1 = Color.Transparent
        MP_新闻列表.BorderColor = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MP_新闻列表.BorderRadius = 10
        MP_新闻列表.BorderSize = 0
        MP_新闻列表.Controls.Add(HtmlColorLabel6)
        MP_新闻列表.Dock = DockStyle.Right
        MP_新闻列表.Location = New Point(649, 130)
        MP_新闻列表.Name = "MP_新闻列表"
        MP_新闻列表.OverlayColor = Color.FromArgb(CByte(120), CByte(0), CByte(0), CByte(0))
        MP_新闻列表.Padding = New Padding(15)
        MP_新闻列表.Size = New Size(300, 500)
        MP_新闻列表.TabIndex = 5
        ' 
        ' HtmlColorLabel6
        ' 
        HtmlColorLabel6.AutoSize = True
        HtmlColorLabel6.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel6.Dock = DockStyle.Top
        HtmlColorLabel6.Font = New Font("Microsoft YaHei UI", 12F)
        HtmlColorLabel6.Location = New Point(20, 20)
        HtmlColorLabel6.Margin = New Padding(2)
        HtmlColorLabel6.Name = "HtmlColorLabel6"
        HtmlColorLabel6.Padding = New Padding(0, 0, 0, 10)
        HtmlColorLabel6.Size = New Size(260, 32)
        HtmlColorLabel6.TabIndex = 2
        HtmlColorLabel6.Text = "新闻列表"
        HtmlColorLabel6.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.Center
        ' 
        ' JustEmptyControl2
        ' 
        JustEmptyControl2.BackColor = Color.Transparent
        JustEmptyControl2.Dock = DockStyle.Left
        JustEmptyControl2.Location = New Point(315, 130)
        JustEmptyControl2.Name = "JustEmptyControl2"
        JustEmptyControl2.Size = New Size(15, 500)
        JustEmptyControl2.TabIndex = 3
        ' 
        ' ModernPanel4
        ' 
        ModernPanel4.BackColor1 = Color.Transparent
        ModernPanel4.BorderRadius = 10
        ModernPanel4.BorderSize = 0
        ModernPanel4.Controls.Add(MB_FFmpegFull)
        ModernPanel4.Controls.Add(MB_LakeUI)
        ModernPanel4.Controls.Add(MB_终末诗)
        ModernPanel4.Controls.Add(MB_爱发电)
        ModernPanel4.Controls.Add(MB_哔哩哔哩)
        ModernPanel4.Controls.Add(MB_官网)
        ModernPanel4.Controls.Add(MB_GitHub)
        ModernPanel4.Controls.Add(MB_AI提示)
        ModernPanel4.Controls.Add(HtmlColorLabel3)
        ModernPanel4.Dock = DockStyle.Left
        ModernPanel4.Location = New Point(15, 130)
        ModernPanel4.Name = "ModernPanel4"
        ModernPanel4.OverlayColor = Color.FromArgb(CByte(120), CByte(0), CByte(0), CByte(0))
        ModernPanel4.Padding = New Padding(15)
        ModernPanel4.Size = New Size(300, 500)
        ModernPanel4.TabIndex = 2
        ' 
        ' MB_FFmpegFull
        ' 
        MB_FFmpegFull.AnimationDuration = 0
        MB_FFmpegFull.BackColor1 = Color.Transparent
        MB_FFmpegFull.BorderRadius = 10
        MB_FFmpegFull.BorderSize = 0
        MB_FFmpegFull.Dock = DockStyle.Top
        MB_FFmpegFull.ForeColor = Color.CornflowerBlue
        MB_FFmpegFull.HoverBackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_FFmpegFull.Location = New Point(20, 312)
        MB_FFmpegFull.Margin = New Padding(2)
        MB_FFmpegFull.Name = "MB_FFmpegFull"
        MB_FFmpegFull.PressedBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_FFmpegFull.Size = New Size(260, 50)
        MB_FFmpegFull.SubText = "包含有版权或许可限制的编码器"
        MB_FFmpegFull.SubTextForeColor = Color.DarkGray
        MB_FFmpegFull.TabIndex = 15
        MB_FFmpegFull.Text = "群内获取自编译完整版 FFmpeg"
        MB_FFmpegFull.TextAlign = LakeUI.ModernButton.TextAlignEnum.Left
        ' 
        ' MB_LakeUI
        ' 
        MB_LakeUI.AnimationDuration = 0
        MB_LakeUI.BackColor1 = Color.Transparent
        MB_LakeUI.BorderRadius = 10
        MB_LakeUI.BorderSize = 0
        MB_LakeUI.Dock = DockStyle.Bottom
        MB_LakeUI.HoverBackColor1 = Color.FromArgb(CByte(30), CByte(220), CByte(220), CByte(220))
        MB_LakeUI.Location = New Point(20, 430)
        MB_LakeUI.Margin = New Padding(2)
        MB_LakeUI.Name = "MB_LakeUI"
        MB_LakeUI.PressedBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_LakeUI.Size = New Size(260, 50)
        MB_LakeUI.SubText = "将 DirectX GPU 加速 带入 WinForms"
        MB_LakeUI.SubTextForeColor = Color.DarkGray
        MB_LakeUI.TabIndex = 14
        MB_LakeUI.Text = "界面主框架：LakeUI v3"
        MB_LakeUI.TextAlign = LakeUI.ModernButton.TextAlignEnum.Left
        ' 
        ' MB_终末诗
        ' 
        MB_终末诗.AnimationDuration = 0
        MB_终末诗.BackColor1 = Color.Transparent
        MB_终末诗.BorderRadius = 10
        MB_终末诗.BorderSize = 0
        MB_终末诗.Dock = DockStyle.Top
        MB_终末诗.ForeColor = Color.YellowGreen
        MB_终末诗.HoverBackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_终末诗.Location = New Point(20, 282)
        MB_终末诗.Margin = New Padding(2)
        MB_终末诗.Name = "MB_终末诗"
        MB_终末诗.PressedBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_终末诗.Size = New Size(260, 30)
        MB_终末诗.SubTextForeColor = Color.DarkGray
        MB_终末诗.TabIndex = 12
        MB_终末诗.Text = "知乎 终末诗的教程"
        MB_终末诗.TextAlign = LakeUI.ModernButton.TextAlignEnum.Left
        ' 
        ' MB_爱发电
        ' 
        MB_爱发电.AnimationDuration = 0
        MB_爱发电.BackColor1 = Color.Transparent
        MB_爱发电.BorderRadius = 10
        MB_爱发电.BorderSize = 0
        MB_爱发电.Dock = DockStyle.Top
        MB_爱发电.ForeColor = Color.MediumPurple
        MB_爱发电.HoverBackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_爱发电.Location = New Point(20, 232)
        MB_爱发电.Margin = New Padding(2)
        MB_爱发电.Name = "MB_爱发电"
        MB_爱发电.PressedBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_爱发电.Size = New Size(260, 50)
        MB_爱发电.SubText = "购买 SP 支持者包请前往电铺页面"
        MB_爱发电.SubTextForeColor = Color.DarkGray
        MB_爱发电.TabIndex = 7
        MB_爱发电.Text = "爱发电 ifdian.net"
        MB_爱发电.TextAlign = LakeUI.ModernButton.TextAlignEnum.Left
        ' 
        ' MB_哔哩哔哩
        ' 
        MB_哔哩哔哩.AnimationDuration = 0
        MB_哔哩哔哩.BackColor1 = Color.Transparent
        MB_哔哩哔哩.BorderRadius = 10
        MB_哔哩哔哩.BorderSize = 0
        MB_哔哩哔哩.Dock = DockStyle.Top
        MB_哔哩哔哩.ForeColor = Color.FromArgb(CByte(237), CByte(114), CByte(153))
        MB_哔哩哔哩.HoverBackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_哔哩哔哩.Location = New Point(20, 182)
        MB_哔哩哔哩.Margin = New Padding(2)
        MB_哔哩哔哩.Name = "MB_哔哩哔哩"
        MB_哔哩哔哩.PressedBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_哔哩哔哩.Size = New Size(260, 50)
        MB_哔哩哔哩.SubText = "唯一动态发布地，无其他任何流媒体账号"
        MB_哔哩哔哩.SubTextForeColor = Color.DarkGray
        MB_哔哩哔哩.TabIndex = 6
        MB_哔哩哔哩.Text = "3FUI 开发者主页 (哔哩哔哩)"
        MB_哔哩哔哩.TextAlign = LakeUI.ModernButton.TextAlignEnum.Left
        ' 
        ' MB_官网
        ' 
        MB_官网.AnimationDuration = 0
        MB_官网.BackColor1 = Color.Transparent
        MB_官网.BorderRadius = 10
        MB_官网.BorderSize = 0
        MB_官网.Dock = DockStyle.Top
        MB_官网.ForeColor = Color.CornflowerBlue
        MB_官网.HoverBackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_官网.Location = New Point(20, 132)
        MB_官网.Margin = New Padding(2)
        MB_官网.Name = "MB_官网"
        MB_官网.PressedBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_官网.Size = New Size(260, 50)
        MB_官网.SubText = "此为长域名，短域名将于 2028 年废弃"
        MB_官网.SubTextForeColor = Color.DarkGray
        MB_官网.TabIndex = 9
        MB_官网.Text = "官网 ffmpegfreeui.top"
        MB_官网.TextAlign = LakeUI.ModernButton.TextAlignEnum.Left
        ' 
        ' MB_GitHub
        ' 
        MB_GitHub.AnimationDuration = 0
        MB_GitHub.BackColor1 = Color.Transparent
        MB_GitHub.BorderRadius = 10
        MB_GitHub.BorderSize = 0
        MB_GitHub.Dock = DockStyle.Top
        MB_GitHub.ForeColor = Color.YellowGreen
        MB_GitHub.HoverBackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_GitHub.Location = New Point(20, 102)
        MB_GitHub.Margin = New Padding(2)
        MB_GitHub.Name = "MB_GitHub"
        MB_GitHub.PressedBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_GitHub.Size = New Size(260, 30)
        MB_GitHub.SubTextForeColor = Color.DarkGray
        MB_GitHub.TabIndex = 5
        MB_GitHub.Text = "3FUI GitHub 仓库"
        MB_GitHub.TextAlign = LakeUI.ModernButton.TextAlignEnum.Left
        ' 
        ' MB_AI提示
        ' 
        MB_AI提示.AnimationDuration = 0
        MB_AI提示.BackColor1 = Color.Transparent
        MB_AI提示.BorderRadius = 10
        MB_AI提示.BorderSize = 0
        MB_AI提示.Dock = DockStyle.Top
        MB_AI提示.ForeColor = Color.Goldenrod
        MB_AI提示.HoverBackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_AI提示.Location = New Point(20, 52)
        MB_AI提示.Margin = New Padding(2)
        MB_AI提示.Name = "MB_AI提示"
        MB_AI提示.PressedBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_AI提示.Size = New Size(260, 50)
        MB_AI提示.SubText = "群内提问默认某包的回答全错，不容异议"
        MB_AI提示.SubTextForeColor = Color.DarkGray
        MB_AI提示.TabIndex = 4
        MB_AI提示.Text = "不要相信 AI 的建议！"
        MB_AI提示.TextAlign = LakeUI.ModernButton.TextAlignEnum.Left
        ' 
        ' HtmlColorLabel3
        ' 
        HtmlColorLabel3.AutoSize = True
        HtmlColorLabel3.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel3.Dock = DockStyle.Top
        HtmlColorLabel3.Font = New Font("Microsoft YaHei UI", 12F)
        HtmlColorLabel3.Location = New Point(20, 20)
        HtmlColorLabel3.Margin = New Padding(2)
        HtmlColorLabel3.Name = "HtmlColorLabel3"
        HtmlColorLabel3.Padding = New Padding(0, 0, 0, 10)
        HtmlColorLabel3.Size = New Size(260, 32)
        HtmlColorLabel3.TabIndex = 0
        HtmlColorLabel3.Text = "链接和文档"
        HtmlColorLabel3.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.Center
        ' 
        ' JustEmptyControl1
        ' 
        JustEmptyControl1.BackColor = Color.Transparent
        JustEmptyControl1.Dock = DockStyle.Top
        JustEmptyControl1.Location = New Point(15, 115)
        JustEmptyControl1.Name = "JustEmptyControl1"
        JustEmptyControl1.Size = New Size(934, 15)
        JustEmptyControl1.TabIndex = 1
        ' 
        ' Form_v6_起始页面
        ' 
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        BackgroundImageLayout = ImageLayout.None
        ClientSize = New Size(964, 645)
        Controls.Add(ModernPanel1)
        Font = New Font("Microsoft YaHei UI", 10F)
        ForeColor = Color.Silver
        Name = "Form_v6_起始页面"
        Text = "Form_v6_起始页面"
        ModernPanel2.ResumeLayout(False)
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        ModernPanel1.ResumeLayout(False)
        ModernPanel5.ResumeLayout(False)
        ModernPanel5.PerformLayout()
        MP_新闻列表.ResumeLayout(False)
        MP_新闻列表.PerformLayout()
        ModernPanel4.ResumeLayout(False)
        ModernPanel4.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents ModernPanel2 As LakeUI.ModernPanel
    Friend WithEvents ModernPanel3 As LakeUI.ModernPanel
    Friend WithEvents HtmlColorLabel1 As LakeUI.HtmlColorLabel
    Friend WithEvents ModernPanel1 As LakeUI.ModernPanel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents HtmlColorLabel2 As LakeUI.HtmlColorLabel
    Friend WithEvents MCB_清理内存 As LakeUI.ModernComboBox
    Friend WithEvents JustEmptyControl1 As LakeUI.JustEmptyControl
    Friend WithEvents ModernPanel4 As LakeUI.ModernPanel
    Friend WithEvents ModernPanel5 As LakeUI.ModernPanel
    Friend WithEvents JustEmptyControl3 As LakeUI.JustEmptyControl
    Friend WithEvents MP_新闻列表 As LakeUI.ModernPanel
    Friend WithEvents JustEmptyControl2 As LakeUI.JustEmptyControl
    Friend WithEvents HtmlColorLabel3 As LakeUI.HtmlColorLabel
    Friend WithEvents MB_AI提示 As LakeUI.ModernButton
    Friend WithEvents MB_爱发电 As LakeUI.ModernButton
    Friend WithEvents MB_哔哩哔哩 As LakeUI.ModernButton
    Friend WithEvents MB_GitHub As LakeUI.ModernButton
    Friend WithEvents ModernButton8 As LakeUI.ModernButton
    Friend WithEvents MB_官网 As LakeUI.ModernButton
    Friend WithEvents MB_终末诗 As LakeUI.ModernButton
    Friend WithEvents HtmlColorLabel4 As LakeUI.HtmlColorLabel
    Friend WithEvents HtmlColorLabel5 As LakeUI.HtmlColorLabel
    Friend WithEvents HtmlColorLabel6 As LakeUI.HtmlColorLabel
    Friend WithEvents MB_LakeUI As LakeUI.ModernButton
    Friend WithEvents MB_软件本体更新 As LakeUI.ModernButton
    Friend WithEvents ModernButton16 As LakeUI.ModernButton
    Friend WithEvents ModernButton15 As LakeUI.ModernButton
    Friend WithEvents MB_更新器更新 As LakeUI.ModernButton
    Friend WithEvents MB_FFmpegFull As LakeUI.ModernButton
End Class
