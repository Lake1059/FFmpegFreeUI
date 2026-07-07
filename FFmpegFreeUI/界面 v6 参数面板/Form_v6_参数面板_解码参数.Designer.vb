<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_v6_参数面板_解码参数
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_v6_参数面板_解码参数))
        ModernPanel1 = New LakeUI.ModernPanel()
        Panel4 = New Panel()
        MTB_硬件解码设备参数值 = New LakeUI.ModernTextBox()
        Panel3 = New Panel()
        MCB_硬件解码设备参数名 = New LakeUI.ModernComboBox()
        HCL_硬件解码设备标题 = New LakeUI.HtmlColorLabel()
        Panel2 = New Panel()
        MCB_硬件解码输出格式 = New LakeUI.ModernComboBox()
        HCL_解码数据格式提示 = New LakeUI.HtmlColorLabel()
        HCL_解码数据格式标题 = New LakeUI.HtmlColorLabel()
        Panel5 = New Panel()
        MTB_CPU解码线程数 = New LakeUI.ModernTextBox()
        HCL_CPU解码线程数说明 = New LakeUI.HtmlColorLabel()
        Panel1 = New Panel()
        MCB_硬件加速解码方式 = New LakeUI.ModernComboBox()
        HCL_硬件加速解码说明 = New LakeUI.HtmlColorLabel()
        HCL_解码器标题 = New LakeUI.HtmlColorLabel()
        ModernPanel1.SuspendLayout()
        Panel4.SuspendLayout()
        Panel3.SuspendLayout()
        Panel2.SuspendLayout()
        Panel5.SuspendLayout()
        Panel1.SuspendLayout()
        SuspendLayout()
        '
        ' ModernPanel1
        '
        ModernPanel1.BackColor1 = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ModernPanel1.BorderSize = 0
        ModernPanel1.Controls.Add(Panel4)
        ModernPanel1.Controls.Add(Panel3)
        ModernPanel1.Controls.Add(HCL_硬件解码设备标题)
        ModernPanel1.Controls.Add(Panel2)
        ModernPanel1.Controls.Add(HCL_解码数据格式提示)
        ModernPanel1.Controls.Add(HCL_解码数据格式标题)
        ModernPanel1.Controls.Add(Panel5)
        ModernPanel1.Controls.Add(HCL_CPU解码线程数说明)
        ModernPanel1.Controls.Add(Panel1)
        ModernPanel1.Controls.Add(HCL_硬件加速解码说明)
        ModernPanel1.Controls.Add(HCL_解码器标题)
        ModernPanel1.Dock = DockStyle.Fill
        ModernPanel1.Location = New Point(0, 0)
        ModernPanel1.Name = "ModernPanel1"
        ModernPanel1.Padding = New Padding(20)
        ModernPanel1.ScrollBarMode = LakeUI.ModernPanel.ScrollMode.Vertical
        ModernPanel1.Size = New Size(753, 623)
        ModernPanel1.TabIndex = 0
        '
        ' Panel4
        '
        Panel4.Controls.Add(MTB_硬件解码设备参数值)
        Panel4.Dock = DockStyle.Top
        Panel4.Location = New Point(20, 420)
        Panel4.Name = "Panel4"
        Panel4.Padding = New Padding(0, 10, 0, 0)
        Panel4.Size = New Size(713, 42)
        Panel4.TabIndex = 24
        '
        ' MTB_硬件解码设备参数值
        '
        MTB_硬件解码设备参数值.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_硬件解码设备参数值.BorderColor = Color.Transparent
        MTB_硬件解码设备参数值.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MTB_硬件解码设备参数值.BorderRadius = 10
        MTB_硬件解码设备参数值.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        MTB_硬件解码设备参数值.Dock = DockStyle.Left
        MTB_硬件解码设备参数值.Location = New Point(0, 10)
        MTB_硬件解码设备参数值.Margin = New Padding(2)
        MTB_硬件解码设备参数值.Name = "MTB_硬件解码设备参数值"
        MTB_硬件解码设备参数值.Padding = New Padding(10, 0, 10, 0)
        MTB_硬件解码设备参数值.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_硬件解码设备参数值.Size = New Size(220, 32)
        MTB_硬件解码设备参数值.TabIndex = 4
        MTB_硬件解码设备参数值.WaterText = "?"
        MTB_硬件解码设备参数值.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        '
        ' Panel3
        '
        Panel3.Controls.Add(MCB_硬件解码设备参数名)
        Panel3.Dock = DockStyle.Top
        Panel3.Location = New Point(20, 378)
        Panel3.Name = "Panel3"
        Panel3.Padding = New Padding(0, 10, 0, 0)
        Panel3.Size = New Size(713, 42)
        Panel3.TabIndex = 23
        '
        ' MCB_硬件解码设备参数名
        '
        MCB_硬件解码设备参数名.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_硬件解码设备参数名.BorderRadius = 10
        MCB_硬件解码设备参数名.BorderSize = 0
        MCB_硬件解码设备参数名.Dock = DockStyle.Left
        MCB_硬件解码设备参数名.DropDownBackdropBlurPasses = 2
        MCB_硬件解码设备参数名.DropDownBackdropBlurRadius = 30
        MCB_硬件解码设备参数名.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_硬件解码设备参数名.DropDownHoverColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        MCB_硬件解码设备参数名.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_硬件解码设备参数名.DropDownPadding = New Padding(10)
        MCB_硬件解码设备参数名.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_硬件解码设备参数名.DropDownSelectedForeColor = Color.White
        MCB_硬件解码设备参数名.Editable = True
        MCB_硬件解码设备参数名.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_硬件解码设备参数名.Items.Add("")
        MCB_硬件解码设备参数名.Items.Add("-hwaccel_device")
        MCB_硬件解码设备参数名.Items.Add("-init_hw_device")
        MCB_硬件解码设备参数名.Items.Add("-qsv_device")
        MCB_硬件解码设备参数名.Location = New Point(0, 10)
        MCB_硬件解码设备参数名.Margin = New Padding(2, 2, 2, 2)
        MCB_硬件解码设备参数名.Name = "MCB_硬件解码设备参数名"
        MCB_硬件解码设备参数名.Padding = New Padding(10, 0, 10, 0)
        MCB_硬件解码设备参数名.Size = New Size(220, 32)
        MCB_硬件解码设备参数名.TabIndex = 0
        MCB_硬件解码设备参数名.ToolTipGap = -1
        MCB_硬件解码设备参数名.ToolTipMaxWidth = 350
        MCB_硬件解码设备参数名.ToolTipPadding = New Padding(15)
        MCB_硬件解码设备参数名.WaterText = "选择参数"
        MCB_硬件解码设备参数名.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        '
        ' HCL_硬件解码设备标题
        '
        HCL_硬件解码设备标题.AutoSize = True
        HCL_硬件解码设备标题.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_硬件解码设备标题.Dock = DockStyle.Top
        HCL_硬件解码设备标题.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_硬件解码设备标题.Location = New Point(20, 328)
        HCL_硬件解码设备标题.Margin = New Padding(2)
        HCL_硬件解码设备标题.Name = "HCL_硬件解码设备标题"
        HCL_硬件解码设备标题.Padding = New Padding(0, 20, 0, 5)
        HCL_硬件解码设备标题.Size = New Size(713, 50)
        HCL_硬件解码设备标题.TabIndex = 22
        HCL_硬件解码设备标题.Text = "<span style=""font-size:13; color:Silver"">硬件加速解码设备</span>   如果安装了多张同品牌显卡可以指定卡，不一定有效"
        '
        ' Panel2
        '
        Panel2.Controls.Add(MCB_硬件解码输出格式)
        Panel2.Dock = DockStyle.Top
        Panel2.Location = New Point(20, 286)
        Panel2.Name = "Panel2"
        Panel2.Padding = New Padding(0, 10, 0, 0)
        Panel2.Size = New Size(713, 42)
        Panel2.TabIndex = 21
        '
        ' MCB_硬件解码输出格式
        '
        MCB_硬件解码输出格式.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_硬件解码输出格式.BorderRadius = 10
        MCB_硬件解码输出格式.BorderSize = 0
        MCB_硬件解码输出格式.Dock = DockStyle.Left
        MCB_硬件解码输出格式.DropDownBackdropBlurPasses = 2
        MCB_硬件解码输出格式.DropDownBackdropBlurRadius = 30
        MCB_硬件解码输出格式.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_硬件解码输出格式.DropDownHoverColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        MCB_硬件解码输出格式.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_硬件解码输出格式.DropDownPadding = New Padding(10)
        MCB_硬件解码输出格式.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_硬件解码输出格式.DropDownSelectedForeColor = Color.White
        MCB_硬件解码输出格式.Editable = True
        MCB_硬件解码输出格式.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_硬件解码输出格式.Items.Add("")
        MCB_硬件解码输出格式.Items.Add("nv12")
        MCB_硬件解码输出格式.Items.Add("yuv420p")
        MCB_硬件解码输出格式.Items.Add("p010")
        MCB_硬件解码输出格式.Items.Add("d3d11")
        MCB_硬件解码输出格式.Location = New Point(0, 10)
        MCB_硬件解码输出格式.Margin = New Padding(2, 2, 2, 2)
        MCB_硬件解码输出格式.Name = "MCB_硬件解码输出格式"
        MCB_硬件解码输出格式.Padding = New Padding(10, 0, 10, 0)
        MCB_硬件解码输出格式.Size = New Size(220, 32)
        MCB_硬件解码输出格式.TabIndex = 0
        MCB_硬件解码输出格式.ToolTipGap = -1
        MCB_硬件解码输出格式.ToolTipMaxWidth = 350
        MCB_硬件解码输出格式.ToolTipPadding = New Padding(15)
        MCB_硬件解码输出格式.WaterText = "-hwaccel_output_format"
        MCB_硬件解码输出格式.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        '
        ' HCL_解码数据格式提示
        '
        HCL_解码数据格式提示.AutoSize = True
        HCL_解码数据格式提示.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_解码数据格式提示.Dock = DockStyle.Top
        HCL_解码数据格式提示.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_解码数据格式提示.Location = New Point(20, 260)
        HCL_解码数据格式提示.Margin = New Padding(2)
        HCL_解码数据格式提示.Name = "HCL_解码数据格式提示"
        HCL_解码数据格式提示.Padding = New Padding(0, 0, 0, 5)
        HCL_解码数据格式提示.Size = New Size(713, 26)
        HCL_解码数据格式提示.TabIndex = 20
        HCL_解码数据格式提示.Text = "比如 <span style=""color:CornflowerBlue"">I卡解码</span> + <span style=""color:YellowGreen"">N卡编码</span> 这样的情况，出问题再考虑！"
        '
        ' HCL_解码数据格式标题
        '
        HCL_解码数据格式标题.AutoSize = True
        HCL_解码数据格式标题.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_解码数据格式标题.Dock = DockStyle.Top
        HCL_解码数据格式标题.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_解码数据格式标题.Location = New Point(20, 210)
        HCL_解码数据格式标题.Margin = New Padding(2)
        HCL_解码数据格式标题.Name = "HCL_解码数据格式标题"
        HCL_解码数据格式标题.Padding = New Padding(0, 20, 0, 5)
        HCL_解码数据格式标题.Size = New Size(713, 50)
        HCL_解码数据格式标题.TabIndex = 19
        HCL_解码数据格式标题.Text = "<span style=""font-size:13; color:Silver"">解码数据格式</span>   编解码都是 CPU 不需要考虑，通常没必要指定"
        '
        ' Panel5
        '
        Panel5.Controls.Add(MTB_CPU解码线程数)
        Panel5.Dock = DockStyle.Top
        Panel5.Location = New Point(20, 168)
        Panel5.Name = "Panel5"
        Panel5.Padding = New Padding(0, 10, 0, 0)
        Panel5.Size = New Size(713, 42)
        Panel5.TabIndex = 18
        '
        ' MTB_CPU解码线程数
        '
        MTB_CPU解码线程数.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_CPU解码线程数.BorderColor = Color.Transparent
        MTB_CPU解码线程数.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MTB_CPU解码线程数.BorderRadius = 10
        MTB_CPU解码线程数.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        MTB_CPU解码线程数.Dock = DockStyle.Left
        MTB_CPU解码线程数.Location = New Point(0, 10)
        MTB_CPU解码线程数.Margin = New Padding(2)
        MTB_CPU解码线程数.Name = "MTB_CPU解码线程数"
        MTB_CPU解码线程数.Padding = New Padding(10, 0, 10, 0)
        MTB_CPU解码线程数.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_CPU解码线程数.Size = New Size(100, 32)
        MTB_CPU解码线程数.TabIndex = 4
        MTB_CPU解码线程数.WaterText = "-threads"
        MTB_CPU解码线程数.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        '
        ' HCL_CPU解码线程数说明
        '
        HCL_CPU解码线程数说明.AutoSize = True
        HCL_CPU解码线程数说明.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_CPU解码线程数说明.Dock = DockStyle.Top
        HCL_CPU解码线程数说明.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_CPU解码线程数说明.Location = New Point(20, 118)
        HCL_CPU解码线程数说明.Margin = New Padding(2)
        HCL_CPU解码线程数说明.Name = "HCL_CPU解码线程数说明"
        HCL_CPU解码线程数说明.Padding = New Padding(0, 20, 0, 5)
        HCL_CPU解码线程数说明.Size = New Size(713, 50)
        HCL_CPU解码线程数说明.TabIndex = 17
        HCL_CPU解码线程数说明.Text = "<span style=""font-size:13; color:Silver"">CPU 解码线程数</span>   不一定有效，通常没必要指定"
        '
        ' Panel1
        '
        Panel1.Controls.Add(MCB_硬件加速解码方式)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(20, 76)
        Panel1.Name = "Panel1"
        Panel1.Padding = New Padding(0, 10, 0, 0)
        Panel1.Size = New Size(713, 42)
        Panel1.TabIndex = 16
        '
        ' MCB_硬件加速解码方式
        '
        MCB_硬件加速解码方式.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_硬件加速解码方式.BorderRadius = 10
        MCB_硬件加速解码方式.BorderSize = 0
        MCB_硬件加速解码方式.Dock = DockStyle.Left
        MCB_硬件加速解码方式.DropDownBackdropBlurPasses = 2
        MCB_硬件加速解码方式.DropDownBackdropBlurRadius = 30
        MCB_硬件加速解码方式.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_硬件加速解码方式.DropDownHoverColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        MCB_硬件加速解码方式.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_硬件加速解码方式.DropDownPadding = New Padding(10)
        MCB_硬件加速解码方式.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_硬件加速解码方式.DropDownSelectedForeColor = Color.White
        MCB_硬件加速解码方式.Editable = True
        MCB_硬件加速解码方式.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_硬件加速解码方式.Items.Add("")
        MCB_硬件加速解码方式.Items.Add("d3d11va")
        MCB_硬件加速解码方式.Items.Add("d3d12va")
        MCB_硬件加速解码方式.Items.Add("cuda")
        MCB_硬件加速解码方式.Items.Add("qsv")
        MCB_硬件加速解码方式.Items.Add("amf")
        MCB_硬件加速解码方式.Items.Add("vulkan")
        MCB_硬件加速解码方式.Items.Add("dxva2")
        MCB_硬件加速解码方式.Items.Add("vaapi")
        MCB_硬件加速解码方式.Items.Add("opencl")
        MCB_硬件加速解码方式.Location = New Point(0, 10)
        MCB_硬件加速解码方式.Margin = New Padding(2, 2, 2, 2)
        MCB_硬件加速解码方式.Name = "MCB_硬件加速解码方式"
        MCB_硬件加速解码方式.Padding = New Padding(10, 0, 10, 0)
        MCB_硬件加速解码方式.Size = New Size(150, 32)
        MCB_硬件加速解码方式.TabIndex = 0
        MCB_硬件加速解码方式.ToolTipGap = -1
        MCB_硬件加速解码方式.ToolTipMaxWidth = 350
        MCB_硬件加速解码方式.ToolTipPadding = New Padding(15)
        MCB_硬件加速解码方式.WaterText = "-hwaccel"
        MCB_硬件加速解码方式.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        '
        ' HCL_硬件加速解码说明
        '
        HCL_硬件加速解码说明.AutoSize = True
        HCL_硬件加速解码说明.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_硬件加速解码说明.Dock = DockStyle.Top
        HCL_硬件加速解码说明.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_硬件加速解码说明.Location = New Point(20, 50)
        HCL_硬件加速解码说明.Margin = New Padding(2)
        HCL_硬件加速解码说明.Name = "HCL_硬件加速解码说明"
        HCL_硬件加速解码说明.Padding = New Padding(0, 0, 0, 5)
        HCL_硬件加速解码说明.Size = New Size(713, 26)
        HCL_硬件加速解码说明.TabIndex = 15
        HCL_硬件加速解码说明.Text = resources.GetString("HCL_硬件加速解码说明.Text")
        '
        ' HCL_解码器标题
        '
        HCL_解码器标题.AutoSize = True
        HCL_解码器标题.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_解码器标题.Dock = DockStyle.Top
        HCL_解码器标题.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_解码器标题.Location = New Point(20, 20)
        HCL_解码器标题.Margin = New Padding(2)
        HCL_解码器标题.Name = "HCL_解码器标题"
        HCL_解码器标题.Padding = New Padding(0, 0, 0, 5)
        HCL_解码器标题.Size = New Size(713, 30)
        HCL_解码器标题.TabIndex = 14
        HCL_解码器标题.Text = "<span style=""font-size:13; color:Silver"">解码器</span>   本页如果不知道选什么就不要选   <span style=""color:Orange"">总有人他非要选然后跑不起来还死磕</span>"
        '
        ' Form_v6_参数面板_解码参数
        '
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ClientSize = New Size(753, 623)
        Controls.Add(ModernPanel1)
        Font = New Font("Microsoft YaHei UI", 10F)
        ForeColor = Color.Silver
        Name = "Form_v6_参数面板_解码参数"
        Text = "Form_v6_参数面板_解码参数"
        ModernPanel1.ResumeLayout(False)
        ModernPanel1.PerformLayout()
        Panel4.ResumeLayout(False)
        Panel3.ResumeLayout(False)
        Panel2.ResumeLayout(False)
        Panel5.ResumeLayout(False)
        Panel1.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents ModernPanel1 As LakeUI.ModernPanel
    Friend WithEvents HCL_硬件加速解码说明 As LakeUI.HtmlColorLabel
    Friend WithEvents HCL_解码器标题 As LakeUI.HtmlColorLabel
    Friend WithEvents HCL_CPU解码线程数说明 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents MCB_硬件加速解码方式 As LakeUI.ModernComboBox
    Friend WithEvents Panel5 As Panel
    Friend WithEvents MTB_CPU解码线程数 As LakeUI.ModernTextBox
    Friend WithEvents HCL_解码数据格式标题 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents MCB_硬件解码输出格式 As LakeUI.ModernComboBox
    Friend WithEvents HCL_解码数据格式提示 As LakeUI.HtmlColorLabel
    Friend WithEvents HCL_硬件解码设备标题 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents MTB_硬件解码设备参数值 As LakeUI.ModernTextBox
    Friend WithEvents Panel3 As Panel
    Friend WithEvents MCB_硬件解码设备参数名 As LakeUI.ModernComboBox
End Class
