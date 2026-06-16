<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_v6_参数面板_超分
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
        ModernPanel1 = New LakeUI.ModernPanel()
        ModernListBox1 = New LakeUI.ModernListBox()
        Panel5 = New Panel()
        HtmlColorLabel10 = New LakeUI.HtmlColorLabel()
        MB_克隆 = New LakeUI.ModernButton()
        JustEmptyControl5 = New LakeUI.JustEmptyControl()
        MB_移除 = New LakeUI.ModernButton()
        JustEmptyControl4 = New LakeUI.JustEmptyControl()
        MB_保存 = New LakeUI.ModernButton()
        JustEmptyControl2 = New LakeUI.JustEmptyControl()
        MB_读取 = New LakeUI.ModernButton()
        HtmlColorLabel9 = New LakeUI.HtmlColorLabel()
        Panel3 = New Panel()
        MCB_着色器文件路径 = New LakeUI.ModernComboBox()
        HtmlColorLabel8 = New LakeUI.HtmlColorLabel()
        Panel1 = New Panel()
        MTB_抗振铃强度 = New LakeUI.ModernTextBox()
        JustEmptyControl1 = New LakeUI.JustEmptyControl()
        MCB_下采样算法 = New LakeUI.ModernComboBox()
        JustEmptyControl3 = New LakeUI.JustEmptyControl()
        MCB_上采样算法 = New LakeUI.ModernComboBox()
        Panel4 = New Panel()
        HtmlColorLabel7 = New LakeUI.HtmlColorLabel()
        HtmlColorLabel6 = New LakeUI.HtmlColorLabel()
        HtmlColorLabel4 = New LakeUI.HtmlColorLabel()
        HtmlColorLabel3 = New LakeUI.HtmlColorLabel()
        Panel2 = New Panel()
        HtmlColorLabel2 = New LakeUI.HtmlColorLabel()
        MTB_高度 = New LakeUI.ModernTextBox()
        HtmlColorLabel5 = New LakeUI.HtmlColorLabel()
        MTB_宽度 = New LakeUI.ModernTextBox()
        HtmlColorLabel1 = New LakeUI.HtmlColorLabel()
        MCB_超分总开关 = New LakeUI.ModernCheckBox()
        ModernPanel1.SuspendLayout()
        Panel5.SuspendLayout()
        Panel3.SuspendLayout()
        Panel1.SuspendLayout()
        Panel4.SuspendLayout()
        Panel2.SuspendLayout()
        SuspendLayout()
        ' 
        ' ModernPanel1
        ' 
        ModernPanel1.BackColor1 = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ModernPanel1.BorderSize = 0
        ModernPanel1.Controls.Add(ModernListBox1)
        ModernPanel1.Controls.Add(Panel5)
        ModernPanel1.Controls.Add(HtmlColorLabel9)
        ModernPanel1.Controls.Add(Panel3)
        ModernPanel1.Controls.Add(HtmlColorLabel8)
        ModernPanel1.Controls.Add(Panel1)
        ModernPanel1.Controls.Add(Panel4)
        ModernPanel1.Controls.Add(HtmlColorLabel3)
        ModernPanel1.Controls.Add(Panel2)
        ModernPanel1.Controls.Add(HtmlColorLabel1)
        ModernPanel1.Controls.Add(MCB_超分总开关)
        ModernPanel1.Dock = DockStyle.Fill
        ModernPanel1.Location = New Point(0, 0)
        ModernPanel1.Name = "ModernPanel1"
        ModernPanel1.Padding = New Padding(20)
        ModernPanel1.Size = New Size(684, 561)
        ModernPanel1.TabIndex = 0
        ' 
        ' ModernListBox1
        ' 
        ModernListBox1.AllowDragReorder = True
        ModernListBox1.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernListBox1.BorderRadius = 10
        ModernListBox1.BorderSize = 0
        ModernListBox1.Dock = DockStyle.Fill
        ModernListBox1.DragSelectZoneWidth = 200
        ModernListBox1.ItemHoverColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        ModernListBox1.ItemPaddingLeft = 8
        ModernListBox1.Items.Add("默认策略")
        ModernListBox1.ItemSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernListBox1.Location = New Point(20, 432)
        ModernListBox1.Name = "ModernListBox1"
        ModernListBox1.Size = New Size(644, 109)
        ModernListBox1.TabIndex = 35
        ' 
        ' Panel5
        ' 
        Panel5.Controls.Add(HtmlColorLabel10)
        Panel5.Controls.Add(MB_克隆)
        Panel5.Controls.Add(JustEmptyControl5)
        Panel5.Controls.Add(MB_移除)
        Panel5.Controls.Add(JustEmptyControl4)
        Panel5.Controls.Add(MB_保存)
        Panel5.Controls.Add(JustEmptyControl2)
        Panel5.Controls.Add(MB_读取)
        Panel5.Dock = DockStyle.Top
        Panel5.Location = New Point(20, 380)
        Panel5.Name = "Panel5"
        Panel5.Padding = New Padding(0, 10, 0, 10)
        Panel5.Size = New Size(644, 52)
        Panel5.TabIndex = 34
        ' 
        ' HtmlColorLabel10
        ' 
        HtmlColorLabel10.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel10.Dock = DockStyle.Fill
        HtmlColorLabel10.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel10.Location = New Point(350, 10)
        HtmlColorLabel10.Margin = New Padding(2)
        HtmlColorLabel10.Name = "HtmlColorLabel10"
        HtmlColorLabel10.Padding = New Padding(10, 0, 0, 0)
        HtmlColorLabel10.Size = New Size(294, 32)
        HtmlColorLabel10.TabIndex = 21
        HtmlColorLabel10.Text = "可直接拖拽排序"
        HtmlColorLabel10.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        ' 
        ' MB_克隆
        ' 
        MB_克隆.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_克隆.BorderRadius = 10
        MB_克隆.BorderSize = 0
        MB_克隆.Dock = DockStyle.Left
        MB_克隆.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_克隆.Location = New Point(270, 10)
        MB_克隆.Margin = New Padding(2)
        MB_克隆.Name = "MB_克隆"
        MB_克隆.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_克隆.Size = New Size(80, 32)
        MB_克隆.TabIndex = 20
        MB_克隆.Text = "克隆"
        ' 
        ' JustEmptyControl5
        ' 
        JustEmptyControl5.Dock = DockStyle.Left
        JustEmptyControl5.Location = New Point(260, 10)
        JustEmptyControl5.Name = "JustEmptyControl5"
        JustEmptyControl5.Size = New Size(10, 32)
        JustEmptyControl5.TabIndex = 19
        ' 
        ' MB_移除
        ' 
        MB_移除.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_移除.BorderRadius = 10
        MB_移除.BorderSize = 0
        MB_移除.Dock = DockStyle.Left
        MB_移除.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_移除.Location = New Point(180, 10)
        MB_移除.Margin = New Padding(2)
        MB_移除.Name = "MB_移除"
        MB_移除.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_移除.Size = New Size(80, 32)
        MB_移除.TabIndex = 18
        MB_移除.Text = "移除"
        ' 
        ' JustEmptyControl4
        ' 
        JustEmptyControl4.Dock = DockStyle.Left
        JustEmptyControl4.Location = New Point(170, 10)
        JustEmptyControl4.Name = "JustEmptyControl4"
        JustEmptyControl4.Size = New Size(10, 32)
        JustEmptyControl4.TabIndex = 17
        ' 
        ' MB_保存
        ' 
        MB_保存.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_保存.BorderRadius = 10
        MB_保存.BorderSize = 0
        MB_保存.Dock = DockStyle.Left
        MB_保存.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_保存.Location = New Point(90, 10)
        MB_保存.Margin = New Padding(2)
        MB_保存.Name = "MB_保存"
        MB_保存.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_保存.Size = New Size(80, 32)
        MB_保存.TabIndex = 16
        MB_保存.Text = "保存"
        ' 
        ' JustEmptyControl2
        ' 
        JustEmptyControl2.Dock = DockStyle.Left
        JustEmptyControl2.Location = New Point(80, 10)
        JustEmptyControl2.Name = "JustEmptyControl2"
        JustEmptyControl2.Size = New Size(10, 32)
        JustEmptyControl2.TabIndex = 15
        ' 
        ' MB_读取
        ' 
        MB_读取.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_读取.BorderRadius = 10
        MB_读取.BorderSize = 0
        MB_读取.Dock = DockStyle.Left
        MB_读取.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_读取.Location = New Point(0, 10)
        MB_读取.Margin = New Padding(2)
        MB_读取.Name = "MB_读取"
        MB_读取.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_读取.Size = New Size(80, 32)
        MB_读取.TabIndex = 2
        MB_读取.Text = "读取"
        ' 
        ' HtmlColorLabel9
        ' 
        HtmlColorLabel9.AutoSize = True
        HtmlColorLabel9.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel9.Dock = DockStyle.Top
        HtmlColorLabel9.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel9.Location = New Point(20, 335)
        HtmlColorLabel9.Margin = New Padding(2)
        HtmlColorLabel9.Name = "HtmlColorLabel9"
        HtmlColorLabel9.Padding = New Padding(0, 20, 0, 0)
        HtmlColorLabel9.Size = New Size(644, 45)
        HtmlColorLabel9.TabIndex = 33
        HtmlColorLabel9.Text = "<span style=""font-size:13; color:Silver"">滤镜叠加策略</span>   要叠加多个着色器，必须使用这里的叠加策略"
        HtmlColorLabel9.ToolTipText = "策略组中有数据时单独设定不生效"
        ' 
        ' Panel3
        ' 
        Panel3.Controls.Add(MCB_着色器文件路径)
        Panel3.Dock = DockStyle.Top
        Panel3.Location = New Point(20, 293)
        Panel3.Name = "Panel3"
        Panel3.Padding = New Padding(0, 10, 0, 0)
        Panel3.Size = New Size(644, 42)
        Panel3.TabIndex = 32
        ' 
        ' MCB_着色器文件路径
        ' 
        MCB_着色器文件路径.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_着色器文件路径.BorderRadius = 10
        MCB_着色器文件路径.BorderSize = 0
        MCB_着色器文件路径.Dock = DockStyle.Left
        MCB_着色器文件路径.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_着色器文件路径.DropDownHoverAnimationDuration = 0
        MCB_着色器文件路径.DropDownHoverColor = Color.FromArgb(CByte(80), CByte(0), CByte(0), CByte(0))
        MCB_着色器文件路径.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_着色器文件路径.DropDownPadding = New Padding(10)
        MCB_着色器文件路径.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_着色器文件路径.DropDownSelectedForeColor = Color.White
        MCB_着色器文件路径.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_着色器文件路径.Items.Add("浏览 ...")
        MCB_着色器文件路径.Location = New Point(0, 10)
        MCB_着色器文件路径.Margin = New Padding(2, 2, 2, 2)
        MCB_着色器文件路径.Name = "MCB_着色器文件路径"
        MCB_着色器文件路径.Padding = New Padding(10, 0, 10, 0)
        MCB_着色器文件路径.Size = New Size(470, 32)
        MCB_着色器文件路径.TabIndex = 15
        MCB_着色器文件路径.ToolTipGap = -1
        MCB_着色器文件路径.ToolTipMaxWidth = 350
        MCB_着色器文件路径.ToolTipPadding = New Padding(15)
        MCB_着色器文件路径.WaterText = "custom_shader_path="
        MCB_着色器文件路径.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' HtmlColorLabel8
        ' 
        HtmlColorLabel8.AutoSize = True
        HtmlColorLabel8.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel8.Dock = DockStyle.Top
        HtmlColorLabel8.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel8.Location = New Point(20, 248)
        HtmlColorLabel8.Margin = New Padding(2)
        HtmlColorLabel8.Name = "HtmlColorLabel8"
        HtmlColorLabel8.Padding = New Padding(0, 20, 0, 0)
        HtmlColorLabel8.Size = New Size(644, 45)
        HtmlColorLabel8.TabIndex = 31
        HtmlColorLabel8.Text = "<span style=""font-size:13; color:Silver"">着色器</span>   非常推荐使用着色器，支持 .glsl 和 .hook 格式，例如 Anime4K、FSRCNNX"
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(MTB_抗振铃强度)
        Panel1.Controls.Add(JustEmptyControl1)
        Panel1.Controls.Add(MCB_下采样算法)
        Panel1.Controls.Add(JustEmptyControl3)
        Panel1.Controls.Add(MCB_上采样算法)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(20, 206)
        Panel1.Name = "Panel1"
        Panel1.Padding = New Padding(0, 10, 0, 0)
        Panel1.Size = New Size(644, 42)
        Panel1.TabIndex = 30
        ' 
        ' MTB_抗振铃强度
        ' 
        MTB_抗振铃强度.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_抗振铃强度.BorderColor = Color.Transparent
        MTB_抗振铃强度.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MTB_抗振铃强度.BorderRadius = 10
        MTB_抗振铃强度.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        MTB_抗振铃强度.Dock = DockStyle.Left
        MTB_抗振铃强度.Location = New Point(320, 10)
        MTB_抗振铃强度.Margin = New Padding(2)
        MTB_抗振铃强度.Name = "MTB_抗振铃强度"
        MTB_抗振铃强度.Padding = New Padding(10, 0, 10, 0)
        MTB_抗振铃强度.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_抗振铃强度.Size = New Size(150, 32)
        MTB_抗振铃强度.TabIndex = 18
        MTB_抗振铃强度.WaterText = "antiringing="
        MTB_抗振铃强度.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' JustEmptyControl1
        ' 
        JustEmptyControl1.Dock = DockStyle.Left
        JustEmptyControl1.Location = New Point(310, 10)
        JustEmptyControl1.Name = "JustEmptyControl1"
        JustEmptyControl1.Size = New Size(10, 32)
        JustEmptyControl1.TabIndex = 17
        ' 
        ' MCB_下采样算法
        ' 
        MCB_下采样算法.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_下采样算法.BorderRadius = 10
        MCB_下采样算法.BorderSize = 0
        MCB_下采样算法.Dock = DockStyle.Left
        MCB_下采样算法.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_下采样算法.DropDownHoverAnimationDuration = 0
        MCB_下采样算法.DropDownHoverColor = Color.FromArgb(CByte(80), CByte(0), CByte(0), CByte(0))
        MCB_下采样算法.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_下采样算法.DropDownPadding = New Padding(10)
        MCB_下采样算法.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_下采样算法.DropDownSelectedForeColor = Color.White
        MCB_下采样算法.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_下采样算法.Items.Add("")
        MCB_下采样算法.Items.Add("none")
        MCB_下采样算法.Items.Add("oversample")
        MCB_下采样算法.Items.Add("bilinear")
        MCB_下采样算法.Items.Add("nearest")
        MCB_下采样算法.Items.Add("bicubic")
        MCB_下采样算法.Items.Add("lanczos")
        MCB_下采样算法.Items.Add("ewa_lanczos")
        MCB_下采样算法.Items.Add("ewa_lanczossharp")
        MCB_下采样算法.Items.Add("ewa_lanczos4sharpest")
        MCB_下采样算法.Items.Add("gaussian")
        MCB_下采样算法.Items.Add("spline16")
        MCB_下采样算法.Items.Add("spline36")
        MCB_下采样算法.Items.Add("spline64")
        MCB_下采样算法.Items.Add("mitchell")
        MCB_下采样算法.Items.Add("sinc")
        MCB_下采样算法.Items.Add("ginseng")
        MCB_下采样算法.Items.Add("ewa_jinc")
        MCB_下采样算法.Items.Add("ewa_ginseng")
        MCB_下采样算法.Items.Add("ewa_hann")
        MCB_下采样算法.Items.Add("hermite")
        MCB_下采样算法.Items.Add("catmull_rom")
        MCB_下采样算法.Items.Add("robidoux")
        MCB_下采样算法.Items.Add("robidouxsharp")
        MCB_下采样算法.Items.Add("ewa_robidoux")
        MCB_下采样算法.Items.Add("ewa_robidouxsharp")
        MCB_下采样算法.Items.Add("triangle")
        MCB_下采样算法.Items.Add("ewa_hanning")
        MCB_下采样算法.Location = New Point(160, 10)
        MCB_下采样算法.Margin = New Padding(2, 2, 2, 2)
        MCB_下采样算法.Name = "MCB_下采样算法"
        MCB_下采样算法.Padding = New Padding(10, 0, 10, 0)
        MCB_下采样算法.Size = New Size(150, 32)
        MCB_下采样算法.TabIndex = 16
        MCB_下采样算法.ToolTipGap = -1
        MCB_下采样算法.ToolTipMaxWidth = 350
        MCB_下采样算法.ToolTipPadding = New Padding(15)
        MCB_下采样算法.WaterText = "downscaler="
        MCB_下采样算法.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' JustEmptyControl3
        ' 
        JustEmptyControl3.Dock = DockStyle.Left
        JustEmptyControl3.Location = New Point(150, 10)
        JustEmptyControl3.Name = "JustEmptyControl3"
        JustEmptyControl3.Size = New Size(10, 32)
        JustEmptyControl3.TabIndex = 14
        ' 
        ' MCB_上采样算法
        ' 
        MCB_上采样算法.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_上采样算法.BorderRadius = 10
        MCB_上采样算法.BorderSize = 0
        MCB_上采样算法.Dock = DockStyle.Left
        MCB_上采样算法.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_上采样算法.DropDownHoverAnimationDuration = 0
        MCB_上采样算法.DropDownHoverColor = Color.FromArgb(CByte(80), CByte(0), CByte(0), CByte(0))
        MCB_上采样算法.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_上采样算法.DropDownPadding = New Padding(10)
        MCB_上采样算法.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_上采样算法.DropDownSelectedForeColor = Color.White
        MCB_上采样算法.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_上采样算法.Items.Add("")
        MCB_上采样算法.Items.Add("none")
        MCB_上采样算法.Items.Add("oversample")
        MCB_上采样算法.Items.Add("bilinear")
        MCB_上采样算法.Items.Add("nearest")
        MCB_上采样算法.Items.Add("bicubic")
        MCB_上采样算法.Items.Add("lanczos")
        MCB_上采样算法.Items.Add("ewa_lanczos")
        MCB_上采样算法.Items.Add("ewa_lanczossharp")
        MCB_上采样算法.Items.Add("ewa_lanczos4sharpest")
        MCB_上采样算法.Items.Add("gaussian")
        MCB_上采样算法.Items.Add("spline16")
        MCB_上采样算法.Items.Add("spline36")
        MCB_上采样算法.Items.Add("spline64")
        MCB_上采样算法.Items.Add("mitchell")
        MCB_上采样算法.Items.Add("sinc")
        MCB_上采样算法.Items.Add("ginseng")
        MCB_上采样算法.Items.Add("ewa_jinc")
        MCB_上采样算法.Items.Add("ewa_ginseng")
        MCB_上采样算法.Items.Add("ewa_hann")
        MCB_上采样算法.Items.Add("hermite")
        MCB_上采样算法.Items.Add("catmull_rom")
        MCB_上采样算法.Items.Add("robidoux")
        MCB_上采样算法.Items.Add("robidouxsharp")
        MCB_上采样算法.Items.Add("ewa_robidoux")
        MCB_上采样算法.Items.Add("ewa_robidouxsharp")
        MCB_上采样算法.Items.Add("triangle")
        MCB_上采样算法.Items.Add("ewa_hanning")
        MCB_上采样算法.Location = New Point(0, 10)
        MCB_上采样算法.Margin = New Padding(2, 2, 2, 2)
        MCB_上采样算法.Name = "MCB_上采样算法"
        MCB_上采样算法.Padding = New Padding(10, 0, 10, 0)
        MCB_上采样算法.Size = New Size(150, 32)
        MCB_上采样算法.TabIndex = 15
        MCB_上采样算法.ToolTipGap = -1
        MCB_上采样算法.ToolTipMaxWidth = 350
        MCB_上采样算法.ToolTipPadding = New Padding(15)
        MCB_上采样算法.WaterText = "upscaler="
        MCB_上采样算法.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' Panel4
        ' 
        Panel4.Controls.Add(HtmlColorLabel7)
        Panel4.Controls.Add(HtmlColorLabel6)
        Panel4.Controls.Add(HtmlColorLabel4)
        Panel4.Dock = DockStyle.Top
        Panel4.Location = New Point(20, 176)
        Panel4.Name = "Panel4"
        Panel4.Size = New Size(644, 30)
        Panel4.TabIndex = 29
        ' 
        ' HtmlColorLabel7
        ' 
        HtmlColorLabel7.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel7.Dock = DockStyle.Fill
        HtmlColorLabel7.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel7.Location = New Point(320, 0)
        HtmlColorLabel7.Margin = New Padding(2)
        HtmlColorLabel7.Name = "HtmlColorLabel7"
        HtmlColorLabel7.Size = New Size(324, 30)
        HtmlColorLabel7.TabIndex = 2
        HtmlColorLabel7.Text = "抗振铃强度 0~1 建议 0.3~0.7"
        HtmlColorLabel7.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.BottomLeft
        ' 
        ' HtmlColorLabel6
        ' 
        HtmlColorLabel6.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel6.Dock = DockStyle.Left
        HtmlColorLabel6.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel6.Location = New Point(160, 0)
        HtmlColorLabel6.Margin = New Padding(2)
        HtmlColorLabel6.Name = "HtmlColorLabel6"
        HtmlColorLabel6.Size = New Size(160, 30)
        HtmlColorLabel6.TabIndex = 1
        HtmlColorLabel6.Text = "下采样算法 (缩小用)"
        HtmlColorLabel6.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.BottomLeft
        ' 
        ' HtmlColorLabel4
        ' 
        HtmlColorLabel4.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel4.Dock = DockStyle.Left
        HtmlColorLabel4.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel4.Location = New Point(0, 0)
        HtmlColorLabel4.Margin = New Padding(2)
        HtmlColorLabel4.Name = "HtmlColorLabel4"
        HtmlColorLabel4.Size = New Size(160, 30)
        HtmlColorLabel4.TabIndex = 0
        HtmlColorLabel4.Text = "上采样算法 (放大用)"
        HtmlColorLabel4.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.BottomLeft
        ' 
        ' HtmlColorLabel3
        ' 
        HtmlColorLabel3.AutoSize = True
        HtmlColorLabel3.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel3.Dock = DockStyle.Top
        HtmlColorLabel3.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel3.Location = New Point(20, 131)
        HtmlColorLabel3.Margin = New Padding(2)
        HtmlColorLabel3.Name = "HtmlColorLabel3"
        HtmlColorLabel3.Padding = New Padding(0, 20, 0, 0)
        HtmlColorLabel3.Size = New Size(644, 45)
        HtmlColorLabel3.TabIndex = 27
        HtmlColorLabel3.Text = "<span style=""font-size:13; color:Silver"">算法设置</span>   仅提供重要设置，如需深度控制请去自定义参数实现整套滤镜参数"
        ' 
        ' Panel2
        ' 
        Panel2.Controls.Add(HtmlColorLabel2)
        Panel2.Controls.Add(MTB_高度)
        Panel2.Controls.Add(HtmlColorLabel5)
        Panel2.Controls.Add(MTB_宽度)
        Panel2.Dock = DockStyle.Top
        Panel2.Location = New Point(20, 89)
        Panel2.Name = "Panel2"
        Panel2.Padding = New Padding(0, 10, 0, 0)
        Panel2.Size = New Size(644, 42)
        Panel2.TabIndex = 26
        ' 
        ' HtmlColorLabel2
        ' 
        HtmlColorLabel2.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel2.Dock = DockStyle.Left
        HtmlColorLabel2.Location = New Point(260, 10)
        HtmlColorLabel2.Margin = New Padding(2)
        HtmlColorLabel2.Name = "HtmlColorLabel2"
        HtmlColorLabel2.Padding = New Padding(10, 0, 0, 0)
        HtmlColorLabel2.Size = New Size(100, 32)
        HtmlColorLabel2.TabIndex = 9
        HtmlColorLabel2.Text = "高度"
        HtmlColorLabel2.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        ' 
        ' MTB_高度
        ' 
        MTB_高度.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_高度.BorderColor = Color.Transparent
        MTB_高度.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MTB_高度.BorderRadius = 10
        MTB_高度.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        MTB_高度.Dock = DockStyle.Left
        MTB_高度.Location = New Point(180, 10)
        MTB_高度.Margin = New Padding(2)
        MTB_高度.Name = "MTB_高度"
        MTB_高度.Padding = New Padding(10, 0, 10, 0)
        MTB_高度.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_高度.Size = New Size(80, 32)
        MTB_高度.TabIndex = 8
        MTB_高度.WaterText = "h="
        MTB_高度.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' HtmlColorLabel5
        ' 
        HtmlColorLabel5.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel5.Dock = DockStyle.Left
        HtmlColorLabel5.Location = New Point(80, 10)
        HtmlColorLabel5.Margin = New Padding(2)
        HtmlColorLabel5.Name = "HtmlColorLabel5"
        HtmlColorLabel5.Padding = New Padding(10, 0, 0, 0)
        HtmlColorLabel5.Size = New Size(100, 32)
        HtmlColorLabel5.TabIndex = 7
        HtmlColorLabel5.Text = "宽度"
        HtmlColorLabel5.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        ' 
        ' MTB_宽度
        ' 
        MTB_宽度.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_宽度.BorderColor = Color.Transparent
        MTB_宽度.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MTB_宽度.BorderRadius = 10
        MTB_宽度.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        MTB_宽度.Dock = DockStyle.Left
        MTB_宽度.Location = New Point(0, 10)
        MTB_宽度.Margin = New Padding(2)
        MTB_宽度.Name = "MTB_宽度"
        MTB_宽度.Padding = New Padding(10, 0, 10, 0)
        MTB_宽度.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_宽度.Size = New Size(80, 32)
        MTB_宽度.TabIndex = 6
        MTB_宽度.WaterText = "w="
        MTB_宽度.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' HtmlColorLabel1
        ' 
        HtmlColorLabel1.AutoSize = True
        HtmlColorLabel1.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel1.Dock = DockStyle.Top
        HtmlColorLabel1.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel1.Location = New Point(20, 44)
        HtmlColorLabel1.Margin = New Padding(2)
        HtmlColorLabel1.Name = "HtmlColorLabel1"
        HtmlColorLabel1.Padding = New Padding(0, 20, 0, 0)
        HtmlColorLabel1.Size = New Size(644, 45)
        HtmlColorLabel1.TabIndex = 25
        HtmlColorLabel1.Text = "<span style=""font-size:13; color:Silver"">目标分辨率</span>   此设置仅影响此滤镜的输出，后续处理仍可配合使用"
        ' 
        ' MCB_超分总开关
        ' 
        MCB_超分总开关.AutoSize = True
        MCB_超分总开关.BoxBorderRadius = 5
        MCB_超分总开关.BoxBorderSize = 0
        MCB_超分总开关.BoxCheckedBackColor = Color.OliveDrab
        MCB_超分总开关.BoxInnerPadding = 6
        MCB_超分总开关.BoxSize = 24
        MCB_超分总开关.BoxTextSpacing = 10
        MCB_超分总开关.BoxUncheckedBackColor = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_超分总开关.CheckMarkWidth = 3F
        MCB_超分总开关.Dock = DockStyle.Top
        MCB_超分总开关.Location = New Point(20, 20)
        MCB_超分总开关.Name = "MCB_超分总开关"
        MCB_超分总开关.Size = New Size(644, 24)
        MCB_超分总开关.TabIndex = 24
        MCB_超分总开关.Text = "超分总开关 / 勾选才会使用 / 建议考虑 AI 软件，但合适的着色器配合性价比更高"
        ' 
        ' Form_v6_参数面板_超分
        ' 
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ClientSize = New Size(684, 561)
        Controls.Add(ModernPanel1)
        DoubleBuffered = True
        Font = New Font("Microsoft YaHei UI", 10F)
        ForeColor = Color.Silver
        MaximizeBox = False
        MinimizeBox = False
        MinimumSize = New Size(700, 600)
        Name = "Form_v6_参数面板_超分"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.Manual
        Text = "着色器超分"
        ModernPanel1.ResumeLayout(False)
        ModernPanel1.PerformLayout()
        Panel5.ResumeLayout(False)
        Panel3.ResumeLayout(False)
        Panel1.ResumeLayout(False)
        Panel4.ResumeLayout(False)
        Panel2.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents ModernPanel1 As LakeUI.ModernPanel
    Friend WithEvents MCB_超分总开关 As LakeUI.ModernCheckBox
    Friend WithEvents HtmlColorLabel1 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents MTB_宽度 As LakeUI.ModernTextBox
    Friend WithEvents HtmlColorLabel5 As LakeUI.HtmlColorLabel
    Friend WithEvents HtmlColorLabel2 As LakeUI.HtmlColorLabel
    Friend WithEvents MTB_高度 As LakeUI.ModernTextBox
    Friend WithEvents HtmlColorLabel3 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents JustEmptyControl1 As LakeUI.JustEmptyControl
    Friend WithEvents MCB_下采样算法 As LakeUI.ModernComboBox
    Friend WithEvents JustEmptyControl3 As LakeUI.JustEmptyControl
    Friend WithEvents MCB_上采样算法 As LakeUI.ModernComboBox
    Friend WithEvents Panel4 As Panel
    Friend WithEvents HtmlColorLabel7 As LakeUI.HtmlColorLabel
    Friend WithEvents HtmlColorLabel6 As LakeUI.HtmlColorLabel
    Friend WithEvents HtmlColorLabel4 As LakeUI.HtmlColorLabel
    Friend WithEvents MTB_抗振铃强度 As LakeUI.ModernTextBox
    Friend WithEvents HtmlColorLabel8 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents MCB_着色器文件路径 As LakeUI.ModernComboBox
    Friend WithEvents HtmlColorLabel9 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents MB_读取 As LakeUI.ModernButton
    Friend WithEvents MB_保存 As LakeUI.ModernButton
    Friend WithEvents JustEmptyControl2 As LakeUI.JustEmptyControl
    Friend WithEvents MB_移除 As LakeUI.ModernButton
    Friend WithEvents JustEmptyControl4 As LakeUI.JustEmptyControl
    Friend WithEvents ModernListBox1 As LakeUI.ModernListBox
    Friend WithEvents MB_克隆 As LakeUI.ModernButton
    Friend WithEvents JustEmptyControl5 As LakeUI.JustEmptyControl
    Friend WithEvents HtmlColorLabel10 As LakeUI.HtmlColorLabel
End Class
