<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_v6_参数面板_音频参数
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
        Dim TrackLabel1 As LakeUI.ExcellentTrackBar.TrackLabel = New LakeUI.ExcellentTrackBar.TrackLabel()
        Dim TrackLabel2 As LakeUI.ExcellentTrackBar.TrackLabel = New LakeUI.ExcellentTrackBar.TrackLabel()
        Dim TrackLabel3 As LakeUI.ExcellentTrackBar.TrackLabel = New LakeUI.ExcellentTrackBar.TrackLabel()
        Dim TrackLabel4 As LakeUI.ExcellentTrackBar.TrackLabel = New LakeUI.ExcellentTrackBar.TrackLabel()
        Dim TrackLabel5 As LakeUI.ExcellentTrackBar.TrackLabel = New LakeUI.ExcellentTrackBar.TrackLabel()
        Dim TrackLabel6 As LakeUI.ExcellentTrackBar.TrackLabel = New LakeUI.ExcellentTrackBar.TrackLabel()
        Dim TrackLabel7 As LakeUI.ExcellentTrackBar.TrackLabel = New LakeUI.ExcellentTrackBar.TrackLabel()
        Dim TrackLabel8 As LakeUI.ExcellentTrackBar.TrackLabel = New LakeUI.ExcellentTrackBar.TrackLabel()
        Dim TrackLabel9 As LakeUI.ExcellentTrackBar.TrackLabel = New LakeUI.ExcellentTrackBar.TrackLabel()
        Dim TrackLabel10 As LakeUI.ExcellentTrackBar.TrackLabel = New LakeUI.ExcellentTrackBar.TrackLabel()
        Dim TrackLabel11 As LakeUI.ExcellentTrackBar.TrackLabel = New LakeUI.ExcellentTrackBar.TrackLabel()
        Dim TrackLabel12 As LakeUI.ExcellentTrackBar.TrackLabel = New LakeUI.ExcellentTrackBar.TrackLabel()
        Dim TrackLabel13 As LakeUI.ExcellentTrackBar.TrackLabel = New LakeUI.ExcellentTrackBar.TrackLabel()
        ModernPanel1 = New LakeUI.ModernPanel()
        Panel7 = New Panel()
        Panel14 = New Panel()
        ETB_峰值电平 = New LakeUI.ExcellentTrackBar()
        MCK_启用峰值电平 = New LakeUI.ModernCheckBox()
        JustEmptyControl3 = New LakeUI.JustEmptyControl()
        Panel11 = New Panel()
        ETB_动态范围 = New LakeUI.ExcellentTrackBar()
        MCK_启用动态范围 = New LakeUI.ModernCheckBox()
        JustEmptyControl2 = New LakeUI.JustEmptyControl()
        Panel10 = New Panel()
        ETB_目标响度 = New LakeUI.ExcellentTrackBar()
        MCK_启用目标响度 = New LakeUI.ModernCheckBox()
        HCL_响度标准化 = New LakeUI.HtmlColorLabel()
        Panel6 = New Panel()
        HCL_声道布局 = New LakeUI.HtmlColorLabel()
        MCB_声道布局 = New LakeUI.ModernComboBox()
        Panel5 = New Panel()
        HCL_位深度 = New LakeUI.HtmlColorLabel()
        MCB_位深度 = New LakeUI.ModernComboBox()
        Panel4 = New Panel()
        HCL_采样率 = New LakeUI.HtmlColorLabel()
        MCB_采样率 = New LakeUI.ModernComboBox()
        HCL_规格 = New LakeUI.HtmlColorLabel()
        Panel3 = New Panel()
        MTB_质量值2 = New LakeUI.ModernTextBox()
        JustEmptyControl5 = New LakeUI.JustEmptyControl()
        MCB_质量参数名2 = New LakeUI.ModernComboBox()
        JustEmptyControl4 = New LakeUI.JustEmptyControl()
        MTB_质量值 = New LakeUI.ModernTextBox()
        JustEmptyControl1 = New LakeUI.JustEmptyControl()
        MCB_质量参数名 = New LakeUI.ModernComboBox()
        Panel1 = New Panel()
        MCB_比特率 = New LakeUI.ModernComboBox()
        HCL_质量 = New LakeUI.HtmlColorLabel()
        Panel2 = New Panel()
        MCB_音频编码器 = New LakeUI.ModernComboBox()
        HCL_音频编码器 = New LakeUI.HtmlColorLabel()
        ModernPanel1.SuspendLayout()
        Panel7.SuspendLayout()
        Panel14.SuspendLayout()
        Panel11.SuspendLayout()
        Panel10.SuspendLayout()
        Panel6.SuspendLayout()
        Panel5.SuspendLayout()
        Panel4.SuspendLayout()
        Panel3.SuspendLayout()
        Panel1.SuspendLayout()
        Panel2.SuspendLayout()
        SuspendLayout()
        ' 
        ' ModernPanel1
        ' 
        ModernPanel1.BackColor1 = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ModernPanel1.BorderSize = 0
        ModernPanel1.Controls.Add(Panel7)
        ModernPanel1.Controls.Add(HCL_响度标准化)
        ModernPanel1.Controls.Add(Panel6)
        ModernPanel1.Controls.Add(Panel5)
        ModernPanel1.Controls.Add(Panel4)
        ModernPanel1.Controls.Add(HCL_规格)
        ModernPanel1.Controls.Add(Panel3)
        ModernPanel1.Controls.Add(Panel1)
        ModernPanel1.Controls.Add(HCL_质量)
        ModernPanel1.Controls.Add(Panel2)
        ModernPanel1.Controls.Add(HCL_音频编码器)
        ModernPanel1.Dock = DockStyle.Fill
        ModernPanel1.Location = New Point(0, 0)
        ModernPanel1.Name = "ModernPanel1"
        ModernPanel1.Padding = New Padding(20)
        ModernPanel1.ScrollBarMode = LakeUI.ModernPanel.ScrollMode.Vertical
        ModernPanel1.Size = New Size(774, 669)
        ModernPanel1.TabIndex = 0
        ' 
        ' Panel7
        ' 
        Panel7.Controls.Add(Panel14)
        Panel7.Controls.Add(JustEmptyControl3)
        Panel7.Controls.Add(Panel11)
        Panel7.Controls.Add(JustEmptyControl2)
        Panel7.Controls.Add(Panel10)
        Panel7.Dock = DockStyle.Fill
        Panel7.Location = New Point(20, 442)
        Panel7.Name = "Panel7"
        Panel7.Size = New Size(734, 207)
        Panel7.TabIndex = 17
        ' 
        ' Panel14
        ' 
        Panel14.Controls.Add(ETB_峰值电平)
        Panel14.Controls.Add(MCK_启用峰值电平)
        Panel14.Dock = DockStyle.Left
        Panel14.Location = New Point(460, 0)
        Panel14.Name = "Panel14"
        Panel14.Size = New Size(200, 207)
        Panel14.TabIndex = 7
        ' 
        ' ETB_峰值电平
        ' 
        ETB_峰值电平.BackColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        ETB_峰值电平.Dock = DockStyle.Fill
        ETB_峰值电平.LabelColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ETB_峰值电平.LabelLineColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ETB_峰值电平.LabelLineLength = 20
        TrackLabel1.Side = LakeUI.ExcellentTrackBar.LabelSideEnum.TopOrLeft
        TrackLabel1.Text = "理论最佳"
        TrackLabel2.Position = -1R
        TrackLabel2.Text = "最常用 -1"
        TrackLabel3.Position = -5R
        TrackLabel3.Side = LakeUI.ExcellentTrackBar.LabelSideEnum.TopOrLeft
        TrackLabel3.Text = "失真严重"
        ETB_峰值电平.Labels.Add(TrackLabel1)
        ETB_峰值电平.Labels.Add(TrackLabel2)
        ETB_峰值电平.Labels.Add(TrackLabel3)
        ETB_峰值电平.LargeChange = 0.1R
        ETB_峰值电平.Location = New Point(0, 0)
        ETB_峰值电平.Margin = New Padding(2, 2, 2, 2)
        ETB_峰值电平.Maximum = 0R
        ETB_峰值电平.Minimum = -5R
        ETB_峰值电平.Name = "ETB_峰值电平"
        ETB_峰值电平.Orientation = LakeUI.ExcellentTrackBar.TrackOrientationEnum.Vertical
        ETB_峰值电平.Padding = New Padding(10)
        ETB_峰值电平.Size = New Size(200, 165)
        ETB_峰值电平.SmallChange = 0.1R
        ETB_峰值电平.TabIndex = 1
        ETB_峰值电平.ThumbBorderWidth = 0
        ETB_峰值电平.ThumbColor = Color.CornflowerBlue
        ETB_峰值电平.ThumbHeight = 30
        ETB_峰值电平.ThumbTextDecimalPlaces = 1
        ETB_峰值电平.ThumbTextMode = LakeUI.ExcellentTrackBar.ThumbTextModeEnum.Value
        ETB_峰值电平.ThumbWidth = 42
        ETB_峰值电平.TrackColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ETB_峰值电平.TrackFillColor = Color.Transparent
        ETB_峰值电平.Value = -1R
        ' 
        ' MCK_启用峰值电平
        ' 
        MCK_启用峰值电平.AutoSize = True
        MCK_启用峰值电平.BackColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCK_启用峰值电平.BoxBorderRadius = 5
        MCK_启用峰值电平.BoxBorderSize = 0
        MCK_启用峰值电平.BoxCheckedBackColor = Color.CornflowerBlue
        MCK_启用峰值电平.BoxInnerPadding = 6
        MCK_启用峰值电平.BoxSize = 22
        MCK_启用峰值电平.BoxTextSpacing = 10
        MCK_启用峰值电平.BoxUncheckedBackColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCK_启用峰值电平.ClickAnywhere = True
        MCK_启用峰值电平.Dock = DockStyle.Bottom
        MCK_启用峰值电平.Location = New Point(0, 165)
        MCK_启用峰值电平.Name = "MCK_启用峰值电平"
        MCK_启用峰值电平.Padding = New Padding(10)
        MCK_启用峰值电平.Size = New Size(200, 42)
        MCK_启用峰值电平.TabIndex = 16
        MCK_启用峰值电平.Text = "峰值电平 dBTP"
        ' 
        ' JustEmptyControl3
        ' 
        JustEmptyControl3.Dock = DockStyle.Left
        JustEmptyControl3.Location = New Point(455, 0)
        JustEmptyControl3.Name = "JustEmptyControl3"
        JustEmptyControl3.Size = New Size(5, 207)
        JustEmptyControl3.TabIndex = 6
        ' 
        ' Panel11
        ' 
        Panel11.Controls.Add(ETB_动态范围)
        Panel11.Controls.Add(MCK_启用动态范围)
        Panel11.Dock = DockStyle.Left
        Panel11.Location = New Point(255, 0)
        Panel11.Name = "Panel11"
        Panel11.Size = New Size(200, 207)
        Panel11.TabIndex = 5
        ' 
        ' ETB_动态范围
        ' 
        ETB_动态范围.BackColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        ETB_动态范围.Dock = DockStyle.Fill
        ETB_动态范围.LabelColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ETB_动态范围.LabelLineColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ETB_动态范围.LabelLineLength = 20
        TrackLabel4.Position = 40R
        TrackLabel4.Side = LakeUI.ExcellentTrackBar.LabelSideEnum.TopOrLeft
        TrackLabel4.Text = "3FUI 允许最大"
        TrackLabel5.Position = 1R
        TrackLabel5.Side = LakeUI.ExcellentTrackBar.LabelSideEnum.TopOrLeft
        TrackLabel5.Text = "3FUI 允许最小"
        TrackLabel6.Position = 30R
        TrackLabel6.Side = LakeUI.ExcellentTrackBar.LabelSideEnum.TopOrLeft
        TrackLabel6.Text = "20~30 星际穿越"
        TrackLabel7.Position = 10R
        TrackLabel7.Side = LakeUI.ExcellentTrackBar.LabelSideEnum.TopOrLeft
        TrackLabel7.Text = "10~15 常规内容推荐"
        ETB_动态范围.Labels.Add(TrackLabel4)
        ETB_动态范围.Labels.Add(TrackLabel5)
        ETB_动态范围.Labels.Add(TrackLabel6)
        ETB_动态范围.Labels.Add(TrackLabel7)
        ETB_动态范围.LargeChange = 1R
        ETB_动态范围.Location = New Point(0, 0)
        ETB_动态范围.Margin = New Padding(2, 2, 2, 2)
        ETB_动态范围.Maximum = 40R
        ETB_动态范围.Minimum = 1R
        ETB_动态范围.Name = "ETB_动态范围"
        ETB_动态范围.Orientation = LakeUI.ExcellentTrackBar.TrackOrientationEnum.Vertical
        ETB_动态范围.Padding = New Padding(10)
        ETB_动态范围.Size = New Size(200, 165)
        ETB_动态范围.TabIndex = 1
        ETB_动态范围.ThumbBorderWidth = 0
        ETB_动态范围.ThumbColor = Color.OliveDrab
        ETB_动态范围.ThumbHeight = 30
        ETB_动态范围.ThumbTextDecimalPlaces = 0
        ETB_动态范围.ThumbTextMode = LakeUI.ExcellentTrackBar.ThumbTextModeEnum.Value
        ETB_动态范围.ThumbWidth = 42
        ETB_动态范围.TrackColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ETB_动态范围.TrackFillColor = Color.Transparent
        ETB_动态范围.Value = 1R
        ' 
        ' MCK_启用动态范围
        ' 
        MCK_启用动态范围.AutoSize = True
        MCK_启用动态范围.BackColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCK_启用动态范围.BoxBorderRadius = 5
        MCK_启用动态范围.BoxBorderSize = 0
        MCK_启用动态范围.BoxCheckedBackColor = Color.OliveDrab
        MCK_启用动态范围.BoxInnerPadding = 6
        MCK_启用动态范围.BoxSize = 22
        MCK_启用动态范围.BoxTextSpacing = 10
        MCK_启用动态范围.BoxUncheckedBackColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCK_启用动态范围.ClickAnywhere = True
        MCK_启用动态范围.Dock = DockStyle.Bottom
        MCK_启用动态范围.Location = New Point(0, 165)
        MCK_启用动态范围.Name = "MCK_启用动态范围"
        MCK_启用动态范围.Padding = New Padding(10)
        MCK_启用动态范围.Size = New Size(200, 42)
        MCK_启用动态范围.TabIndex = 15
        MCK_启用动态范围.Text = "动态范围 LU"
        ' 
        ' JustEmptyControl2
        ' 
        JustEmptyControl2.Dock = DockStyle.Left
        JustEmptyControl2.Location = New Point(250, 0)
        JustEmptyControl2.Name = "JustEmptyControl2"
        JustEmptyControl2.Size = New Size(5, 207)
        JustEmptyControl2.TabIndex = 4
        ' 
        ' Panel10
        ' 
        Panel10.Controls.Add(ETB_目标响度)
        Panel10.Controls.Add(MCK_启用目标响度)
        Panel10.Dock = DockStyle.Left
        Panel10.Location = New Point(0, 0)
        Panel10.Name = "Panel10"
        Panel10.Size = New Size(250, 207)
        Panel10.TabIndex = 2
        ' 
        ' ETB_目标响度
        ' 
        ETB_目标响度.BackColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        ETB_目标响度.Dock = DockStyle.Fill
        ETB_目标响度.LabelColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ETB_目标响度.LabelLineColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ETB_目标响度.LabelLineLength = 20
        TrackLabel8.Position = -8R
        TrackLabel8.Side = LakeUI.ExcellentTrackBar.LabelSideEnum.TopOrLeft
        TrackLabel8.Text = "3FUI 最响允许"
        TrackLabel9.Position = -12R
        TrackLabel9.Text = "最响建议 -12"
        TrackLabel10.Position = -36R
        TrackLabel10.Side = LakeUI.ExcellentTrackBar.LabelSideEnum.TopOrLeft
        TrackLabel10.Text = "3FUI 最静允许"
        TrackLabel11.Position = -24R
        TrackLabel11.Text = "我国标准 -24"
        TrackLabel12.Position = -23R
        TrackLabel12.Side = LakeUI.ExcellentTrackBar.LabelSideEnum.TopOrLeft
        TrackLabel12.Text = "-23 国际标准"
        TrackLabel13.Position = -16R
        TrackLabel13.Side = LakeUI.ExcellentTrackBar.LabelSideEnum.TopOrLeft
        TrackLabel13.Text = "-16 综合推荐"
        ETB_目标响度.Labels.Add(TrackLabel8)
        ETB_目标响度.Labels.Add(TrackLabel9)
        ETB_目标响度.Labels.Add(TrackLabel10)
        ETB_目标响度.Labels.Add(TrackLabel11)
        ETB_目标响度.Labels.Add(TrackLabel12)
        ETB_目标响度.Labels.Add(TrackLabel13)
        ETB_目标响度.LargeChange = 1R
        ETB_目标响度.Location = New Point(0, 0)
        ETB_目标响度.Margin = New Padding(2, 2, 2, 2)
        ETB_目标响度.Maximum = -8R
        ETB_目标响度.Minimum = -36R
        ETB_目标响度.Name = "ETB_目标响度"
        ETB_目标响度.Orientation = LakeUI.ExcellentTrackBar.TrackOrientationEnum.Vertical
        ETB_目标响度.Padding = New Padding(0, 10, 0, 10)
        ETB_目标响度.Size = New Size(250, 165)
        ETB_目标响度.TabIndex = 1
        ETB_目标响度.ThumbBorderWidth = 0
        ETB_目标响度.ThumbColor = Color.IndianRed
        ETB_目标响度.ThumbHeight = 30
        ETB_目标响度.ThumbTextDecimalPlaces = 0
        ETB_目标响度.ThumbTextMode = LakeUI.ExcellentTrackBar.ThumbTextModeEnum.Value
        ETB_目标响度.ThumbWidth = 42
        ETB_目标响度.TrackColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ETB_目标响度.TrackFillColor = Color.Transparent
        ETB_目标响度.Value = -24R
        ' 
        ' MCK_启用目标响度
        ' 
        MCK_启用目标响度.AutoSize = True
        MCK_启用目标响度.BackColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCK_启用目标响度.BoxBorderRadius = 5
        MCK_启用目标响度.BoxBorderSize = 0
        MCK_启用目标响度.BoxCheckedBackColor = Color.IndianRed
        MCK_启用目标响度.BoxInnerPadding = 6
        MCK_启用目标响度.BoxSize = 22
        MCK_启用目标响度.BoxTextSpacing = 10
        MCK_启用目标响度.BoxUncheckedBackColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCK_启用目标响度.ClickAnywhere = True
        MCK_启用目标响度.Dock = DockStyle.Bottom
        MCK_启用目标响度.Location = New Point(0, 165)
        MCK_启用目标响度.Name = "MCK_启用目标响度"
        MCK_启用目标响度.Padding = New Padding(10)
        MCK_启用目标响度.Size = New Size(250, 42)
        MCK_启用目标响度.TabIndex = 14
        MCK_启用目标响度.Text = "目标响度 LUFS"
        ' 
        ' HCL_响度标准化
        ' 
        HCL_响度标准化.AutoSize = True
        HCL_响度标准化.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_响度标准化.Dock = DockStyle.Top
        HCL_响度标准化.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_响度标准化.Location = New Point(20, 387)
        HCL_响度标准化.Margin = New Padding(2)
        HCL_响度标准化.Name = "HCL_响度标准化"
        HCL_响度标准化.Padding = New Padding(0, 20, 0, 10)
        HCL_响度标准化.Size = New Size(734, 55)
        HCL_响度标准化.TabIndex = 16
        HCL_响度标准化.Text = "<span style=""font-size:13; color:Silver"">响度标准化</span>   歌曲细节少可适当拉响，电影细节多需要放静并提升动态；勾选上才有效"
        ' 
        ' Panel6
        ' 
        Panel6.Controls.Add(HCL_声道布局)
        Panel6.Controls.Add(MCB_声道布局)
        Panel6.Dock = DockStyle.Top
        Panel6.Location = New Point(20, 345)
        Panel6.Name = "Panel6"
        Panel6.Padding = New Padding(0, 10, 0, 0)
        Panel6.Size = New Size(734, 42)
        Panel6.TabIndex = 15
        ' 
        ' HCL_声道布局
        ' 
        HCL_声道布局.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_声道布局.Dock = DockStyle.Fill
        HCL_声道布局.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_声道布局.Location = New Point(160, 10)
        HCL_声道布局.Margin = New Padding(2)
        HCL_声道布局.Name = "HCL_声道布局"
        HCL_声道布局.Padding = New Padding(10, 0, 0, 0)
        HCL_声道布局.Size = New Size(574, 32)
        HCL_声道布局.TabIndex = 1
        HCL_声道布局.Text = "<span style=""color:Silver"">声道布局</span>   如果使用 CBR 则需要注意增加比特率"
        HCL_声道布局.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        ' 
        ' MCB_声道布局
        ' 
        MCB_声道布局.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_声道布局.BorderRadius = 10
        MCB_声道布局.BorderSize = 0
        MCB_声道布局.Dock = DockStyle.Left
        MCB_声道布局.DropDownBackdropBlurPasses = 2
        MCB_声道布局.DropDownBackdropBlurRadius = 30
        MCB_声道布局.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_声道布局.DropDownHoverColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        MCB_声道布局.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_声道布局.DropDownPadding = New Padding(10)
        MCB_声道布局.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_声道布局.DropDownSelectedForeColor = Color.White
        MCB_声道布局.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_声道布局.Items.Add("")
        MCB_声道布局.Items.Add("mono")
        MCB_声道布局.Items.Add("stereo")
        MCB_声道布局.Items.Add("2.1")
        MCB_声道布局.Items.Add("4.0")
        MCB_声道布局.Items.Add("5.0")
        MCB_声道布局.Items.Add("5.1")
        MCB_声道布局.Items.Add("6.1")
        MCB_声道布局.Items.Add("7.1")
        MCB_声道布局.Items.Add("hexagonal")
        MCB_声道布局.Items.Add("octagonal")
        MCB_声道布局.Location = New Point(0, 10)
        MCB_声道布局.Margin = New Padding(2, 2, 2, 2)
        MCB_声道布局.Name = "MCB_声道布局"
        MCB_声道布局.Padding = New Padding(10, 0, 10, 0)
        MCB_声道布局.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_声道布局.Size = New Size(160, 32)
        MCB_声道布局.TabIndex = 0
        MCB_声道布局.ToolTipMaxWidth = 350
        MCB_声道布局.ToolTipPadding = New Padding(15)
        MCB_声道布局.WaterText = "-channel_layout"
        MCB_声道布局.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' Panel5
        ' 
        Panel5.Controls.Add(HCL_位深度)
        Panel5.Controls.Add(MCB_位深度)
        Panel5.Dock = DockStyle.Top
        Panel5.Location = New Point(20, 303)
        Panel5.Name = "Panel5"
        Panel5.Padding = New Padding(0, 10, 0, 0)
        Panel5.Size = New Size(734, 42)
        Panel5.TabIndex = 14
        ' 
        ' HCL_位深度
        ' 
        HCL_位深度.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_位深度.Dock = DockStyle.Fill
        HCL_位深度.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_位深度.Location = New Point(160, 10)
        HCL_位深度.Margin = New Padding(2)
        HCL_位深度.Name = "HCL_位深度"
        HCL_位深度.Padding = New Padding(10, 0, 0, 0)
        HCL_位深度.Size = New Size(574, 32)
        HCL_位深度.TabIndex = 1
        HCL_位深度.Text = "<span style=""color:Silver"">位深度</span>   注意查询编码器所支持的位深；WAV 是通过编码器名称选择的"
        HCL_位深度.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        ' 
        ' MCB_位深度
        ' 
        MCB_位深度.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_位深度.BorderRadius = 10
        MCB_位深度.BorderSize = 0
        MCB_位深度.Dock = DockStyle.Left
        MCB_位深度.DropDownBackdropBlurPasses = 2
        MCB_位深度.DropDownBackdropBlurRadius = 30
        MCB_位深度.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_位深度.DropDownHoverColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        MCB_位深度.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_位深度.DropDownPadding = New Padding(10)
        MCB_位深度.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_位深度.DropDownSelectedForeColor = Color.White
        MCB_位深度.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_位深度.Items.Add("")
        MCB_位深度.Items.Add("s8")
        MCB_位深度.Items.Add("s16")
        MCB_位深度.Items.Add("s24")
        MCB_位深度.Items.Add("s32")
        MCB_位深度.Items.Add("s64")
        MCB_位深度.Location = New Point(0, 10)
        MCB_位深度.Margin = New Padding(2, 2, 2, 2)
        MCB_位深度.Name = "MCB_位深度"
        MCB_位深度.Padding = New Padding(10, 0, 10, 0)
        MCB_位深度.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_位深度.Size = New Size(160, 32)
        MCB_位深度.TabIndex = 0
        MCB_位深度.ToolTipMaxWidth = 350
        MCB_位深度.ToolTipPadding = New Padding(15)
        MCB_位深度.WaterText = "-sample_fmt"
        MCB_位深度.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' Panel4
        ' 
        Panel4.Controls.Add(HCL_采样率)
        Panel4.Controls.Add(MCB_采样率)
        Panel4.Dock = DockStyle.Top
        Panel4.Location = New Point(20, 261)
        Panel4.Name = "Panel4"
        Panel4.Padding = New Padding(0, 10, 0, 0)
        Panel4.Size = New Size(734, 42)
        Panel4.TabIndex = 13
        ' 
        ' HCL_采样率
        ' 
        HCL_采样率.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_采样率.Dock = DockStyle.Fill
        HCL_采样率.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_采样率.Location = New Point(160, 10)
        HCL_采样率.Margin = New Padding(2)
        HCL_采样率.Name = "HCL_采样率"
        HCL_采样率.Padding = New Padding(10, 0, 0, 0)
        HCL_采样率.Size = New Size(574, 32)
        HCL_采样率.TabIndex = 1
        HCL_采样率.Text = "<span style=""color:Silver"">采样率</span>   采样并非越高越好，需要适配目标设备"
        HCL_采样率.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        ' 
        ' MCB_采样率
        ' 
        MCB_采样率.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_采样率.BorderRadius = 10
        MCB_采样率.BorderSize = 0
        MCB_采样率.Dock = DockStyle.Left
        MCB_采样率.DropDownBackdropBlurPasses = 2
        MCB_采样率.DropDownBackdropBlurRadius = 30
        MCB_采样率.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_采样率.DropDownHoverColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        MCB_采样率.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_采样率.DropDownPadding = New Padding(10)
        MCB_采样率.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_采样率.DropDownSelectedForeColor = Color.White
        MCB_采样率.Editable = True
        MCB_采样率.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_采样率.Items.Add("")
        MCB_采样率.Items.Add("192000")
        MCB_采样率.Items.Add("96000")
        MCB_采样率.Items.Add("48000")
        MCB_采样率.Items.Add("44100")
        MCB_采样率.Items.Add("32000")
        MCB_采样率.Items.Add("24000")
        MCB_采样率.Items.Add("22050")
        MCB_采样率.Items.Add("16000")
        MCB_采样率.Items.Add("11025")
        MCB_采样率.Items.Add("8000")
        MCB_采样率.Location = New Point(0, 10)
        MCB_采样率.Margin = New Padding(2, 2, 2, 2)
        MCB_采样率.Name = "MCB_采样率"
        MCB_采样率.Padding = New Padding(10, 0, 10, 0)
        MCB_采样率.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_采样率.Size = New Size(160, 32)
        MCB_采样率.TabIndex = 0
        MCB_采样率.ToolTipMaxWidth = 350
        MCB_采样率.ToolTipPadding = New Padding(15)
        MCB_采样率.WaterText = "-ar"
        MCB_采样率.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' HCL_规格
        ' 
        HCL_规格.AutoSize = True
        HCL_规格.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_规格.Dock = DockStyle.Top
        HCL_规格.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_规格.Location = New Point(20, 216)
        HCL_规格.Margin = New Padding(2)
        HCL_规格.Name = "HCL_规格"
        HCL_规格.Padding = New Padding(0, 20, 0, 0)
        HCL_规格.Size = New Size(734, 45)
        HCL_规格.TabIndex = 12
        HCL_规格.Text = "<span style=""font-size:13; color:Silver"">规格</span>"
        ' 
        ' Panel3
        ' 
        Panel3.Controls.Add(MTB_质量值2)
        Panel3.Controls.Add(JustEmptyControl5)
        Panel3.Controls.Add(MCB_质量参数名2)
        Panel3.Controls.Add(JustEmptyControl4)
        Panel3.Controls.Add(MTB_质量值)
        Panel3.Controls.Add(JustEmptyControl1)
        Panel3.Controls.Add(MCB_质量参数名)
        Panel3.Dock = DockStyle.Top
        Panel3.Location = New Point(20, 174)
        Panel3.Name = "Panel3"
        Panel3.Padding = New Padding(0, 10, 0, 0)
        Panel3.Size = New Size(734, 42)
        Panel3.TabIndex = 11
        ' 
        ' MTB_质量值2
        ' 
        MTB_质量值2.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_质量值2.BorderColor = Color.Transparent
        MTB_质量值2.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MTB_质量值2.BorderRadius = 10
        MTB_质量值2.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        MTB_质量值2.Dock = DockStyle.Left
        MTB_质量值2.Location = New Point(580, 10)
        MTB_质量值2.Margin = New Padding(2)
        MTB_质量值2.Name = "MTB_质量值2"
        MTB_质量值2.Padding = New Padding(10, 0, 10, 0)
        MTB_质量值2.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_质量值2.Size = New Size(80, 32)
        MTB_质量值2.TabIndex = 9
        MTB_质量值2.WaterText = "质量值"
        MTB_质量值2.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' JustEmptyControl5
        ' 
        JustEmptyControl5.Dock = DockStyle.Left
        JustEmptyControl5.Location = New Point(570, 10)
        JustEmptyControl5.Name = "JustEmptyControl5"
        JustEmptyControl5.Size = New Size(10, 32)
        JustEmptyControl5.TabIndex = 8
        ' 
        ' MCB_质量参数名2
        ' 
        MCB_质量参数名2.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_质量参数名2.BorderRadius = 10
        MCB_质量参数名2.BorderSize = 0
        MCB_质量参数名2.Dock = DockStyle.Left
        MCB_质量参数名2.DropDownBackdropBlurPasses = 2
        MCB_质量参数名2.DropDownBackdropBlurRadius = 30
        MCB_质量参数名2.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_质量参数名2.DropDownHoverColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        MCB_质量参数名2.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_质量参数名2.DropDownPadding = New Padding(10)
        MCB_质量参数名2.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_质量参数名2.DropDownSelectedForeColor = Color.White
        MCB_质量参数名2.Editable = True
        MCB_质量参数名2.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_质量参数名2.Location = New Point(370, 10)
        MCB_质量参数名2.Margin = New Padding(2, 2, 2, 2)
        MCB_质量参数名2.Name = "MCB_质量参数名2"
        MCB_质量参数名2.Padding = New Padding(10, 0, 10, 0)
        MCB_质量参数名2.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_质量参数名2.Size = New Size(200, 32)
        MCB_质量参数名2.TabIndex = 7
        MCB_质量参数名2.ToolTipMaxWidth = 350
        MCB_质量参数名2.ToolTipPadding = New Padding(15)
        MCB_质量参数名2.WaterText = "选择第二质量参数名"
        MCB_质量参数名2.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' JustEmptyControl4
        ' 
        JustEmptyControl4.Dock = DockStyle.Left
        JustEmptyControl4.Location = New Point(290, 10)
        JustEmptyControl4.Name = "JustEmptyControl4"
        JustEmptyControl4.Size = New Size(80, 32)
        JustEmptyControl4.TabIndex = 6
        ' 
        ' MTB_质量值
        ' 
        MTB_质量值.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_质量值.BorderColor = Color.Transparent
        MTB_质量值.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MTB_质量值.BorderRadius = 10
        MTB_质量值.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        MTB_质量值.Dock = DockStyle.Left
        MTB_质量值.Location = New Point(210, 10)
        MTB_质量值.Margin = New Padding(2)
        MTB_质量值.Name = "MTB_质量值"
        MTB_质量值.Padding = New Padding(10, 0, 10, 0)
        MTB_质量值.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_质量值.Size = New Size(80, 32)
        MTB_质量值.TabIndex = 5
        MTB_质量值.WaterText = "质量值"
        MTB_质量值.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' JustEmptyControl1
        ' 
        JustEmptyControl1.Dock = DockStyle.Left
        JustEmptyControl1.Location = New Point(200, 10)
        JustEmptyControl1.Name = "JustEmptyControl1"
        JustEmptyControl1.Size = New Size(10, 32)
        JustEmptyControl1.TabIndex = 3
        ' 
        ' MCB_质量参数名
        ' 
        MCB_质量参数名.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_质量参数名.BorderRadius = 10
        MCB_质量参数名.BorderSize = 0
        MCB_质量参数名.Dock = DockStyle.Left
        MCB_质量参数名.DropDownBackdropBlurPasses = 2
        MCB_质量参数名.DropDownBackdropBlurRadius = 30
        MCB_质量参数名.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_质量参数名.DropDownHoverColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        MCB_质量参数名.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_质量参数名.DropDownPadding = New Padding(10)
        MCB_质量参数名.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_质量参数名.DropDownSelectedForeColor = Color.White
        MCB_质量参数名.Editable = True
        MCB_质量参数名.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_质量参数名.Location = New Point(0, 10)
        MCB_质量参数名.Margin = New Padding(2, 2, 2, 2)
        MCB_质量参数名.Name = "MCB_质量参数名"
        MCB_质量参数名.Padding = New Padding(10, 0, 10, 0)
        MCB_质量参数名.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_质量参数名.Size = New Size(200, 32)
        MCB_质量参数名.TabIndex = 0
        MCB_质量参数名.ToolTipMaxWidth = 350
        MCB_质量参数名.ToolTipPadding = New Padding(15)
        MCB_质量参数名.WaterText = "选择质量参数名"
        MCB_质量参数名.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(MCB_比特率)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(20, 132)
        Panel1.Name = "Panel1"
        Panel1.Padding = New Padding(0, 10, 0, 0)
        Panel1.Size = New Size(734, 42)
        Panel1.TabIndex = 10
        ' 
        ' MCB_比特率
        ' 
        MCB_比特率.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_比特率.BorderRadius = 10
        MCB_比特率.BorderSize = 0
        MCB_比特率.Dock = DockStyle.Left
        MCB_比特率.DropDownBackdropBlurPasses = 2
        MCB_比特率.DropDownBackdropBlurRadius = 30
        MCB_比特率.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_比特率.DropDownHoverColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        MCB_比特率.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_比特率.DropDownPadding = New Padding(10)
        MCB_比特率.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_比特率.DropDownSelectedForeColor = Color.White
        MCB_比特率.Editable = True
        MCB_比特率.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_比特率.Items.Add("")
        MCB_比特率.Items.Add("96k")
        MCB_比特率.Items.Add("128k")
        MCB_比特率.Items.Add("192k")
        MCB_比特率.Items.Add("256k")
        MCB_比特率.Items.Add("320k")
        MCB_比特率.Items.Add("384k")
        MCB_比特率.Items.Add("448k")
        MCB_比特率.Items.Add("512k")
        MCB_比特率.Items.Add("640k")
        MCB_比特率.Items.Add("1411k")
        MCB_比特率.Location = New Point(0, 10)
        MCB_比特率.Margin = New Padding(2, 2, 2, 2)
        MCB_比特率.Name = "MCB_比特率"
        MCB_比特率.Padding = New Padding(10, 0, 10, 0)
        MCB_比特率.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_比特率.Size = New Size(200, 32)
        MCB_比特率.TabIndex = 0
        MCB_比特率.ToolTipMaxWidth = 350
        MCB_比特率.ToolTipPadding = New Padding(15)
        MCB_比特率.WaterText = "选择或填写比特率"
        MCB_比特率.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' HCL_质量
        ' 
        HCL_质量.AutoSize = True
        HCL_质量.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_质量.Dock = DockStyle.Top
        HCL_质量.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_质量.Location = New Point(20, 87)
        HCL_质量.Margin = New Padding(2)
        HCL_质量.Name = "HCL_质量"
        HCL_质量.Padding = New Padding(0, 20, 0, 0)
        HCL_质量.Size = New Size(734, 45)
        HCL_质量.TabIndex = 9
        HCL_质量.Text = "<span style=""font-size:13; color:Silver"">质量</span>   CBR 直接写比特率，VBR 通过对应参数名和值来实现"
        ' 
        ' Panel2
        ' 
        Panel2.Controls.Add(MCB_音频编码器)
        Panel2.Dock = DockStyle.Top
        Panel2.Location = New Point(20, 45)
        Panel2.Name = "Panel2"
        Panel2.Padding = New Padding(0, 10, 0, 0)
        Panel2.Size = New Size(734, 42)
        Panel2.TabIndex = 8
        ' 
        ' MCB_音频编码器
        ' 
        MCB_音频编码器.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_音频编码器.BorderRadius = 10
        MCB_音频编码器.BorderSize = 0
        MCB_音频编码器.Dock = DockStyle.Left
        MCB_音频编码器.DropDownBackdropBlurPasses = 2
        MCB_音频编码器.DropDownBackdropBlurRadius = 30
        MCB_音频编码器.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_音频编码器.DropDownHoverColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        MCB_音频编码器.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_音频编码器.DropDownPadding = New Padding(10)
        MCB_音频编码器.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_音频编码器.DropDownSelectedForeColor = Color.White
        MCB_音频编码器.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_音频编码器.Location = New Point(0, 10)
        MCB_音频编码器.Margin = New Padding(2, 2, 2, 2)
        MCB_音频编码器.MaxDropDownItems = 15
        MCB_音频编码器.Name = "MCB_音频编码器"
        MCB_音频编码器.Padding = New Padding(10, 0, 10, 0)
        MCB_音频编码器.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_音频编码器.Size = New Size(250, 32)
        MCB_音频编码器.TabIndex = 0
        MCB_音频编码器.ToolTipGap = -1
        MCB_音频编码器.ToolTipMaxWidth = 500
        MCB_音频编码器.ToolTipPadding = New Padding(15)
        MCB_音频编码器.WaterText = "选择音频编码器"
        MCB_音频编码器.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' HCL_音频编码器
        ' 
        HCL_音频编码器.AutoSize = True
        HCL_音频编码器.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_音频编码器.Dock = DockStyle.Top
        HCL_音频编码器.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_音频编码器.Location = New Point(20, 20)
        HCL_音频编码器.Margin = New Padding(2)
        HCL_音频编码器.Name = "HCL_音频编码器"
        HCL_音频编码器.Size = New Size(734, 25)
        HCL_音频编码器.TabIndex = 7
        HCL_音频编码器.Text = "<span style=""font-size:13; color:Silver"">音频编码器</span>"
        ' 
        ' Form_v6_参数面板_音频参数
        ' 
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ClientSize = New Size(774, 669)
        Controls.Add(ModernPanel1)
        Font = New Font("Microsoft YaHei UI", 10F)
        ForeColor = Color.Silver
        Name = "Form_v6_参数面板_音频参数"
        Text = "Form_v6_参数面板_音频参数"
        ModernPanel1.ResumeLayout(False)
        ModernPanel1.PerformLayout()
        Panel7.ResumeLayout(False)
        Panel14.ResumeLayout(False)
        Panel14.PerformLayout()
        Panel11.ResumeLayout(False)
        Panel11.PerformLayout()
        Panel10.ResumeLayout(False)
        Panel10.PerformLayout()
        Panel6.ResumeLayout(False)
        Panel5.ResumeLayout(False)
        Panel4.ResumeLayout(False)
        Panel3.ResumeLayout(False)
        Panel1.ResumeLayout(False)
        Panel2.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents ModernPanel1 As LakeUI.ModernPanel
    Friend WithEvents HCL_音频编码器 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents MCB_音频编码器 As LakeUI.ModernComboBox
    Friend WithEvents HCL_质量 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents MCB_比特率 As LakeUI.ModernComboBox
    Friend WithEvents Panel3 As Panel
    Friend WithEvents MCB_质量参数名 As LakeUI.ModernComboBox
    Friend WithEvents JustEmptyControl1 As LakeUI.JustEmptyControl
    Friend WithEvents HCL_规格 As LakeUI.HtmlColorLabel
    Friend WithEvents MTB_质量值 As LakeUI.ModernTextBox
    Friend WithEvents Panel4 As Panel
    Friend WithEvents HCL_采样率 As LakeUI.HtmlColorLabel
    Friend WithEvents MCB_采样率 As LakeUI.ModernComboBox
    Friend WithEvents HCL_响度标准化 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel6 As Panel
    Friend WithEvents HCL_声道布局 As LakeUI.HtmlColorLabel
    Friend WithEvents MCB_声道布局 As LakeUI.ModernComboBox
    Friend WithEvents Panel5 As Panel
    Friend WithEvents HCL_位深度 As LakeUI.HtmlColorLabel
    Friend WithEvents MCB_位深度 As LakeUI.ModernComboBox
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Panel10 As Panel
    Friend WithEvents ETB_目标响度 As LakeUI.ExcellentTrackBar
    Friend WithEvents JustEmptyControl3 As LakeUI.JustEmptyControl
    Friend WithEvents Panel11 As Panel
    Friend WithEvents ETB_动态范围 As LakeUI.ExcellentTrackBar
    Friend WithEvents JustEmptyControl2 As LakeUI.JustEmptyControl
    Friend WithEvents Panel14 As Panel
    Friend WithEvents ETB_峰值电平 As LakeUI.ExcellentTrackBar
    Friend WithEvents MCK_启用峰值电平 As LakeUI.ModernCheckBox
    Friend WithEvents MCK_启用动态范围 As LakeUI.ModernCheckBox
    Friend WithEvents MCK_启用目标响度 As LakeUI.ModernCheckBox
    Friend WithEvents MTB_质量值2 As LakeUI.ModernTextBox
    Friend WithEvents JustEmptyControl5 As LakeUI.JustEmptyControl
    Friend WithEvents MCB_质量参数名2 As LakeUI.ModernComboBox
    Friend WithEvents JustEmptyControl4 As LakeUI.JustEmptyControl
End Class
