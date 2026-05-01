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
        ModernTextBox2 = New LakeUI.ModernTextBox()
        Panel3 = New Panel()
        ModernComboBox3 = New LakeUI.ModernComboBox()
        HtmlColorLabel5 = New LakeUI.HtmlColorLabel()
        Panel2 = New Panel()
        ModernComboBox2 = New LakeUI.ModernComboBox()
        HtmlColorLabel4 = New LakeUI.HtmlColorLabel()
        HtmlColorLabel3 = New LakeUI.HtmlColorLabel()
        Panel5 = New Panel()
        ModernTextBox1 = New LakeUI.ModernTextBox()
        HtmlColorLabel2 = New LakeUI.HtmlColorLabel()
        Panel1 = New Panel()
        ModernComboBox1 = New LakeUI.ModernComboBox()
        HtmlColorLabel6 = New LakeUI.HtmlColorLabel()
        HtmlColorLabel1 = New LakeUI.HtmlColorLabel()
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
        ModernPanel1.Controls.Add(HtmlColorLabel5)
        ModernPanel1.Controls.Add(Panel2)
        ModernPanel1.Controls.Add(HtmlColorLabel4)
        ModernPanel1.Controls.Add(HtmlColorLabel3)
        ModernPanel1.Controls.Add(Panel5)
        ModernPanel1.Controls.Add(HtmlColorLabel2)
        ModernPanel1.Controls.Add(Panel1)
        ModernPanel1.Controls.Add(HtmlColorLabel6)
        ModernPanel1.Controls.Add(HtmlColorLabel1)
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
        Panel4.Controls.Add(ModernTextBox2)
        Panel4.Dock = DockStyle.Top
        Panel4.Location = New Point(20, 420)
        Panel4.Name = "Panel4"
        Panel4.Padding = New Padding(0, 10, 0, 0)
        Panel4.Size = New Size(713, 42)
        Panel4.TabIndex = 24
        ' 
        ' ModernTextBox2
        ' 
        ModernTextBox2.BackColor1 = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        ModernTextBox2.BorderColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        ModernTextBox2.BorderRadius = 10
        ModernTextBox2.BorderSize = 2
        ModernTextBox2.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        ModernTextBox2.Dock = DockStyle.Left
        ModernTextBox2.Location = New Point(0, 10)
        ModernTextBox2.Margin = New Padding(2, 2, 2, 2)
        ModernTextBox2.Name = "ModernTextBox2"
        ModernTextBox2.Padding = New Padding(10, 0, 10, 0)
        ModernTextBox2.ScrollBarHoverColor = Color.FromArgb(CByte(200), CByte(200), CByte(200))
        ModernTextBox2.Size = New Size(220, 32)
        ModernTextBox2.TabIndex = 4
        ModernTextBox2.WaterText = "?"
        ' 
        ' Panel3
        ' 
        Panel3.Controls.Add(ModernComboBox3)
        Panel3.Dock = DockStyle.Top
        Panel3.Location = New Point(20, 378)
        Panel3.Name = "Panel3"
        Panel3.Padding = New Padding(0, 10, 0, 0)
        Panel3.Size = New Size(713, 42)
        Panel3.TabIndex = 23
        ' 
        ' ModernComboBox3
        ' 
        ModernComboBox3.BackColor1 = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        ModernComboBox3.BorderColorFocus = Color.Silver
        ModernComboBox3.BorderRadius = 10
        ModernComboBox3.BorderSize = 0
        ModernComboBox3.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        ModernComboBox3.Dock = DockStyle.Left
        ModernComboBox3.DropDownBorderSize = 2
        ModernComboBox3.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        ModernComboBox3.DropDownPadding = New Padding(10)
        ModernComboBox3.DropDownScrollBarHoverColor = Color.FromArgb(CByte(200), CByte(200), CByte(200))
        ModernComboBox3.Editable = True
        ModernComboBox3.Items.Add("")
        ModernComboBox3.Items.Add("-hwaccel_device")
        ModernComboBox3.Items.Add("-init_hw_device")
        ModernComboBox3.Items.Add("-qsv_device")
        ModernComboBox3.Location = New Point(0, 10)
        ModernComboBox3.Margin = New Padding(2, 2, 2, 2)
        ModernComboBox3.Name = "ModernComboBox3"
        ModernComboBox3.Padding = New Padding(10, 0, 10, 0)
        ModernComboBox3.Size = New Size(220, 32)
        ModernComboBox3.TabIndex = 0
        ModernComboBox3.ToolTipBorderSize = 2
        ModernComboBox3.ToolTipGap = 10
        ModernComboBox3.ToolTipMaxWidth = 350
        ModernComboBox3.ToolTipPadding = New Padding(15)
        ModernComboBox3.WaterText = "选择参数"
        ' 
        ' HtmlColorLabel5
        ' 
        HtmlColorLabel5.AutoSize = True
        HtmlColorLabel5.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel5.Dock = DockStyle.Top
        HtmlColorLabel5.Location = New Point(20, 328)
        HtmlColorLabel5.Margin = New Padding(2)
        HtmlColorLabel5.Name = "HtmlColorLabel5"
        HtmlColorLabel5.Padding = New Padding(0, 20, 0, 5)
        HtmlColorLabel5.Size = New Size(713, 50)
        HtmlColorLabel5.TabIndex = 22
        HtmlColorLabel5.Text = "<span style=""font-size:13"">硬件加速解码设备</span>   <span style=""font-size:10pt; color:Gray"">如果安装了多张同品牌显卡可以指定卡，不一定有效</span>"
        ' 
        ' Panel2
        ' 
        Panel2.Controls.Add(ModernComboBox2)
        Panel2.Dock = DockStyle.Top
        Panel2.Location = New Point(20, 286)
        Panel2.Name = "Panel2"
        Panel2.Padding = New Padding(0, 10, 0, 0)
        Panel2.Size = New Size(713, 42)
        Panel2.TabIndex = 21
        ' 
        ' ModernComboBox2
        ' 
        ModernComboBox2.BackColor1 = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        ModernComboBox2.BorderColorFocus = Color.Silver
        ModernComboBox2.BorderRadius = 10
        ModernComboBox2.BorderSize = 0
        ModernComboBox2.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        ModernComboBox2.Dock = DockStyle.Left
        ModernComboBox2.DropDownBorderSize = 2
        ModernComboBox2.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        ModernComboBox2.DropDownPadding = New Padding(10)
        ModernComboBox2.DropDownScrollBarHoverColor = Color.FromArgb(CByte(200), CByte(200), CByte(200))
        ModernComboBox2.Editable = True
        ModernComboBox2.Items.Add("")
        ModernComboBox2.Items.Add("nv12")
        ModernComboBox2.Items.Add("yuv420p")
        ModernComboBox2.Items.Add("p010")
        ModernComboBox2.Items.Add("d3d11")
        ModernComboBox2.Location = New Point(0, 10)
        ModernComboBox2.Margin = New Padding(2, 2, 2, 2)
        ModernComboBox2.Name = "ModernComboBox2"
        ModernComboBox2.Padding = New Padding(10, 0, 10, 0)
        ModernComboBox2.Size = New Size(220, 32)
        ModernComboBox2.TabIndex = 0
        ModernComboBox2.ToolTipBorderSize = 2
        ModernComboBox2.ToolTipGap = 10
        ModernComboBox2.ToolTipMaxWidth = 350
        ModernComboBox2.ToolTipPadding = New Padding(15)
        ModernComboBox2.WaterText = "-hwaccel_output_format"
        ' 
        ' HtmlColorLabel4
        ' 
        HtmlColorLabel4.AutoSize = True
        HtmlColorLabel4.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel4.Dock = DockStyle.Top
        HtmlColorLabel4.Location = New Point(20, 260)
        HtmlColorLabel4.Margin = New Padding(2)
        HtmlColorLabel4.Name = "HtmlColorLabel4"
        HtmlColorLabel4.Padding = New Padding(0, 0, 0, 5)
        HtmlColorLabel4.Size = New Size(713, 26)
        HtmlColorLabel4.TabIndex = 20
        HtmlColorLabel4.Text = "比如 <span style=""font-size:10pt; color:CornflowerBlue"">I卡解码</span> + <span style=""font-size:10pt; color:YellowGreen"">N卡编码</span> 这样的情况，出问题再考虑！"
        ' 
        ' HtmlColorLabel3
        ' 
        HtmlColorLabel3.AutoSize = True
        HtmlColorLabel3.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel3.Dock = DockStyle.Top
        HtmlColorLabel3.Location = New Point(20, 210)
        HtmlColorLabel3.Margin = New Padding(2)
        HtmlColorLabel3.Name = "HtmlColorLabel3"
        HtmlColorLabel3.Padding = New Padding(0, 20, 0, 5)
        HtmlColorLabel3.Size = New Size(713, 50)
        HtmlColorLabel3.TabIndex = 19
        HtmlColorLabel3.Text = "<span style=""font-size:13"">解码数据格式</span>   <span style=""font-size:10pt; color:Gray"">编解码都是 CPU 不需要考虑，通常没必要指定</span>"
        ' 
        ' Panel5
        ' 
        Panel5.Controls.Add(ModernTextBox1)
        Panel5.Dock = DockStyle.Top
        Panel5.Location = New Point(20, 168)
        Panel5.Name = "Panel5"
        Panel5.Padding = New Padding(0, 10, 0, 0)
        Panel5.Size = New Size(713, 42)
        Panel5.TabIndex = 18
        ' 
        ' ModernTextBox1
        ' 
        ModernTextBox1.BackColor1 = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        ModernTextBox1.BorderColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        ModernTextBox1.BorderRadius = 10
        ModernTextBox1.BorderSize = 2
        ModernTextBox1.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        ModernTextBox1.Dock = DockStyle.Left
        ModernTextBox1.Location = New Point(0, 10)
        ModernTextBox1.Margin = New Padding(2, 2, 2, 2)
        ModernTextBox1.Name = "ModernTextBox1"
        ModernTextBox1.Padding = New Padding(10, 0, 10, 0)
        ModernTextBox1.ScrollBarHoverColor = Color.FromArgb(CByte(200), CByte(200), CByte(200))
        ModernTextBox1.Size = New Size(100, 32)
        ModernTextBox1.TabIndex = 4
        ModernTextBox1.WaterText = "-threads"
        ' 
        ' HtmlColorLabel2
        ' 
        HtmlColorLabel2.AutoSize = True
        HtmlColorLabel2.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel2.Dock = DockStyle.Top
        HtmlColorLabel2.Location = New Point(20, 118)
        HtmlColorLabel2.Margin = New Padding(2)
        HtmlColorLabel2.Name = "HtmlColorLabel2"
        HtmlColorLabel2.Padding = New Padding(0, 20, 0, 5)
        HtmlColorLabel2.Size = New Size(713, 50)
        HtmlColorLabel2.TabIndex = 17
        HtmlColorLabel2.Text = "<span style=""font-size:13"">CPU 解码线程数</span>   <span style=""font-size:10pt; color:Gray"">不一定有效，通常没必要指定</span>"
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(ModernComboBox1)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(20, 76)
        Panel1.Name = "Panel1"
        Panel1.Padding = New Padding(0, 10, 0, 0)
        Panel1.Size = New Size(713, 42)
        Panel1.TabIndex = 16
        ' 
        ' ModernComboBox1
        ' 
        ModernComboBox1.BackColor1 = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        ModernComboBox1.BorderColorFocus = Color.Silver
        ModernComboBox1.BorderRadius = 10
        ModernComboBox1.BorderSize = 0
        ModernComboBox1.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        ModernComboBox1.Dock = DockStyle.Left
        ModernComboBox1.DropDownBorderSize = 2
        ModernComboBox1.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        ModernComboBox1.DropDownPadding = New Padding(10)
        ModernComboBox1.DropDownScrollBarHoverColor = Color.FromArgb(CByte(200), CByte(200), CByte(200))
        ModernComboBox1.Items.Add("")
        ModernComboBox1.Items.Add("d3d11va")
        ModernComboBox1.Items.Add("d3d12va")
        ModernComboBox1.Items.Add("cuda")
        ModernComboBox1.Items.Add("qsv")
        ModernComboBox1.Items.Add("amf")
        ModernComboBox1.Items.Add("vulkan")
        ModernComboBox1.Items.Add("dxva2")
        ModernComboBox1.Items.Add("vaapi")
        ModernComboBox1.Items.Add("opencl")
        ModernComboBox1.Location = New Point(0, 10)
        ModernComboBox1.Margin = New Padding(2, 2, 2, 2)
        ModernComboBox1.Name = "ModernComboBox1"
        ModernComboBox1.Padding = New Padding(10, 0, 10, 0)
        ModernComboBox1.Size = New Size(150, 32)
        ModernComboBox1.TabIndex = 0
        ModernComboBox1.ToolTipBorderSize = 2
        ModernComboBox1.ToolTipGap = 10
        ModernComboBox1.ToolTipMaxWidth = 350
        ModernComboBox1.ToolTipPadding = New Padding(15)
        ModernComboBox1.WaterText = "-hwaccel"
        ' 
        ' HtmlColorLabel6
        ' 
        HtmlColorLabel6.AutoSize = True
        HtmlColorLabel6.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel6.Dock = DockStyle.Top
        HtmlColorLabel6.Location = New Point(20, 50)
        HtmlColorLabel6.Margin = New Padding(2)
        HtmlColorLabel6.Name = "HtmlColorLabel6"
        HtmlColorLabel6.Padding = New Padding(0, 0, 0, 5)
        HtmlColorLabel6.Size = New Size(713, 26)
        HtmlColorLabel6.TabIndex = 15
        HtmlColorLabel6.Text = resources.GetString("HtmlColorLabel6.Text")
        ' 
        ' HtmlColorLabel1
        ' 
        HtmlColorLabel1.AutoSize = True
        HtmlColorLabel1.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel1.Dock = DockStyle.Top
        HtmlColorLabel1.Location = New Point(20, 20)
        HtmlColorLabel1.Margin = New Padding(2)
        HtmlColorLabel1.Name = "HtmlColorLabel1"
        HtmlColorLabel1.Padding = New Padding(0, 0, 0, 5)
        HtmlColorLabel1.Size = New Size(713, 30)
        HtmlColorLabel1.TabIndex = 14
        HtmlColorLabel1.Text = "<span style=""font-size:13"">解码器</span>   <span style=""font-size:10pt; color:Gray"">本页如果不知道选什么就不要选</span>   <span style=""font-size:10pt; color:Orange"">总有人他非要选然后跑不起来还死磕</span>"
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
    Friend WithEvents HtmlColorLabel6 As LakeUI.HtmlColorLabel
    Friend WithEvents HtmlColorLabel1 As LakeUI.HtmlColorLabel
    Friend WithEvents HtmlColorLabel2 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ModernComboBox1 As LakeUI.ModernComboBox
    Friend WithEvents Panel5 As Panel
    Friend WithEvents ModernTextBox1 As LakeUI.ModernTextBox
    Friend WithEvents HtmlColorLabel3 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents ModernComboBox2 As LakeUI.ModernComboBox
    Friend WithEvents HtmlColorLabel4 As LakeUI.HtmlColorLabel
    Friend WithEvents HtmlColorLabel5 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents ModernTextBox2 As LakeUI.ModernTextBox
    Friend WithEvents Panel3 As Panel
    Friend WithEvents ModernComboBox3 As LakeUI.ModernComboBox
End Class
