<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_v6_参数面板_抽帧参数
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
        Panel7 = New Panel()
        Panel5 = New Panel()
        ModernTextBox3 = New LakeUI.ModernTextBox()
        HtmlColorLabel11 = New LakeUI.HtmlColorLabel()
        HtmlColorLabel10 = New LakeUI.HtmlColorLabel()
        Panel4 = New Panel()
        ModernComboBox2 = New LakeUI.ModernComboBox()
        HtmlColorLabel9 = New LakeUI.HtmlColorLabel()
        HtmlColorLabel8 = New LakeUI.HtmlColorLabel()
        Panel3 = New Panel()
        ModernComboBox1 = New LakeUI.ModernComboBox()
        HtmlColorLabel7 = New LakeUI.HtmlColorLabel()
        HtmlColorLabel5 = New LakeUI.HtmlColorLabel()
        HtmlColorLabel6 = New LakeUI.HtmlColorLabel()
        Panel6 = New Panel()
        HtmlColorLabel13 = New LakeUI.HtmlColorLabel()
        HtmlColorLabel12 = New LakeUI.HtmlColorLabel()
        Panel1 = New Panel()
        ModernTextBox2 = New LakeUI.ModernTextBox()
        HtmlColorLabel3 = New LakeUI.HtmlColorLabel()
        HtmlColorLabel4 = New LakeUI.HtmlColorLabel()
        Panel2 = New Panel()
        ModernTextBox1 = New LakeUI.ModernTextBox()
        HtmlColorLabel2 = New LakeUI.HtmlColorLabel()
        HtmlColorLabel1 = New LakeUI.HtmlColorLabel()
        ModernCheckBox1 = New LakeUI.ModernCheckBox()
        ModernPanel1.SuspendLayout()
        Panel7.SuspendLayout()
        Panel5.SuspendLayout()
        Panel4.SuspendLayout()
        Panel3.SuspendLayout()
        Panel6.SuspendLayout()
        Panel1.SuspendLayout()
        Panel2.SuspendLayout()
        SuspendLayout()
        ' 
        ' ModernPanel1
        ' 
        ModernPanel1.BackColor1 = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ModernPanel1.BorderSize = 0
        ModernPanel1.Controls.Add(Panel7)
        ModernPanel1.Controls.Add(Panel6)
        ModernPanel1.Controls.Add(ModernCheckBox1)
        ModernPanel1.Dock = DockStyle.Fill
        ModernPanel1.Location = New Point(0, 0)
        ModernPanel1.Name = "ModernPanel1"
        ModernPanel1.Padding = New Padding(20)
        ModernPanel1.ScrollBarMode = LakeUI.ModernPanel.ScrollMode.Vertical
        ModernPanel1.Size = New Size(984, 561)
        ModernPanel1.TabIndex = 1
        ' 
        ' Panel7
        ' 
        Panel7.Controls.Add(Panel5)
        Panel7.Controls.Add(HtmlColorLabel11)
        Panel7.Controls.Add(HtmlColorLabel10)
        Panel7.Controls.Add(Panel4)
        Panel7.Controls.Add(HtmlColorLabel9)
        Panel7.Controls.Add(HtmlColorLabel8)
        Panel7.Controls.Add(Panel3)
        Panel7.Controls.Add(HtmlColorLabel7)
        Panel7.Controls.Add(HtmlColorLabel5)
        Panel7.Controls.Add(HtmlColorLabel6)
        Panel7.Dock = DockStyle.Fill
        Panel7.Location = New Point(492, 44)
        Panel7.Name = "Panel7"
        Panel7.Size = New Size(472, 497)
        Panel7.TabIndex = 23
        ' 
        ' Panel5
        ' 
        Panel5.Controls.Add(ModernTextBox3)
        Panel5.Dock = DockStyle.Top
        Panel5.Location = New Point(0, 360)
        Panel5.Name = "Panel5"
        Panel5.Padding = New Padding(0, 10, 0, 0)
        Panel5.Size = New Size(472, 42)
        Panel5.TabIndex = 22
        ' 
        ' ModernTextBox3
        ' 
        ModernTextBox3.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernTextBox3.BorderColor = Color.Transparent
        ModernTextBox3.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        ModernTextBox3.BorderRadius = 10
        ModernTextBox3.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        ModernTextBox3.Dock = DockStyle.Left
        ModernTextBox3.Location = New Point(0, 10)
        ModernTextBox3.Margin = New Padding(2)
        ModernTextBox3.Name = "ModernTextBox3"
        ModernTextBox3.Padding = New Padding(10, 0, 10, 0)
        ModernTextBox3.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernTextBox3.Size = New Size(100, 32)
        ModernTextBox3.TabIndex = 6
        ModernTextBox3.WaterText = "frac"
        ModernTextBox3.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' HtmlColorLabel11
        ' 
        HtmlColorLabel11.AutoSize = True
        HtmlColorLabel11.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel11.Dock = DockStyle.Top
        HtmlColorLabel11.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel11.Location = New Point(0, 339)
        HtmlColorLabel11.Margin = New Padding(2)
        HtmlColorLabel11.Name = "HtmlColorLabel11"
        HtmlColorLabel11.Size = New Size(472, 21)
        HtmlColorLabel11.TabIndex = 21
        HtmlColorLabel11.Text = "例如 0.1 表示只有 10% 以下的变化才会丢帧"
        ' 
        ' HtmlColorLabel10
        ' 
        HtmlColorLabel10.AutoSize = True
        HtmlColorLabel10.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel10.Dock = DockStyle.Top
        HtmlColorLabel10.Font = New Font("Microsoft YaHei UI", 11F)
        HtmlColorLabel10.Location = New Point(0, 303)
        HtmlColorLabel10.Margin = New Padding(2)
        HtmlColorLabel10.Name = "HtmlColorLabel10"
        HtmlColorLabel10.Padding = New Padding(0, 15, 0, 0)
        HtmlColorLabel10.Size = New Size(472, 36)
        HtmlColorLabel10.TabIndex = 20
        HtmlColorLabel10.Text = "允许超过低阈值的最大占比（1=整张图）"
        ' 
        ' Panel4
        ' 
        Panel4.Controls.Add(ModernComboBox2)
        Panel4.Dock = DockStyle.Top
        Panel4.Location = New Point(0, 261)
        Panel4.Name = "Panel4"
        Panel4.Padding = New Padding(0, 10, 0, 0)
        Panel4.Size = New Size(472, 42)
        Panel4.TabIndex = 19
        ' 
        ' ModernComboBox2
        ' 
        ModernComboBox2.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernComboBox2.BorderRadius = 10
        ModernComboBox2.BorderSize = 0
        ModernComboBox2.Dock = DockStyle.Left
        ModernComboBox2.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        ModernComboBox2.DropDownHoverAnimationDuration = 0
        ModernComboBox2.DropDownHoverColor = Color.FromArgb(CByte(80), CByte(0), CByte(0), CByte(0))
        ModernComboBox2.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        ModernComboBox2.DropDownPadding = New Padding(10)
        ModernComboBox2.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernComboBox2.DropDownSelectedForeColor = Color.White
        ModernComboBox2.Editable = True
        ModernComboBox2.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        ModernComboBox2.Items.Add("64*4")
        ModernComboBox2.Items.Add("64*6")
        ModernComboBox2.Items.Add("64*8")
        ModernComboBox2.Items.Add("64*10")
        ModernComboBox2.Location = New Point(0, 10)
        ModernComboBox2.Margin = New Padding(2, 2, 2, 2)
        ModernComboBox2.Name = "ModernComboBox2"
        ModernComboBox2.Padding = New Padding(10, 0, 10, 0)
        ModernComboBox2.Size = New Size(150, 32)
        ModernComboBox2.TabIndex = 1
        ModernComboBox2.ToolTipGap = -1
        ModernComboBox2.ToolTipMaxWidth = 350
        ModernComboBox2.ToolTipPadding = New Padding(15)
        ModernComboBox2.WaterText = "lo"
        ModernComboBox2.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' HtmlColorLabel9
        ' 
        HtmlColorLabel9.AutoSize = True
        HtmlColorLabel9.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel9.Dock = DockStyle.Top
        HtmlColorLabel9.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel9.Location = New Point(0, 220)
        HtmlColorLabel9.Margin = New Padding(2)
        HtmlColorLabel9.Name = "HtmlColorLabel9"
        HtmlColorLabel9.Size = New Size(472, 41)
        HtmlColorLabel9.TabIndex = 18
        HtmlColorLabel9.Text = "（格式同上）在满足高阈值的前提下<br>变化必须超过低阈值且不能超过最大占比才会丢帧"
        ' 
        ' HtmlColorLabel8
        ' 
        HtmlColorLabel8.AutoSize = True
        HtmlColorLabel8.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel8.Dock = DockStyle.Top
        HtmlColorLabel8.Font = New Font("Microsoft YaHei UI", 11F)
        HtmlColorLabel8.Location = New Point(0, 184)
        HtmlColorLabel8.Margin = New Padding(2)
        HtmlColorLabel8.Name = "HtmlColorLabel8"
        HtmlColorLabel8.Padding = New Padding(0, 15, 0, 0)
        HtmlColorLabel8.Size = New Size(472, 36)
        HtmlColorLabel8.TabIndex = 17
        HtmlColorLabel8.Text = "低阈值，所有 <span style=""color:YellowGreen"">8*8=64</span> 的像素块差异最小值"
        ' 
        ' Panel3
        ' 
        Panel3.Controls.Add(ModernComboBox1)
        Panel3.Dock = DockStyle.Top
        Panel3.Location = New Point(0, 142)
        Panel3.Name = "Panel3"
        Panel3.Padding = New Padding(0, 10, 0, 0)
        Panel3.Size = New Size(472, 42)
        Panel3.TabIndex = 16
        ' 
        ' ModernComboBox1
        ' 
        ModernComboBox1.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernComboBox1.BorderRadius = 10
        ModernComboBox1.BorderSize = 0
        ModernComboBox1.Dock = DockStyle.Left
        ModernComboBox1.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        ModernComboBox1.DropDownHoverAnimationDuration = 0
        ModernComboBox1.DropDownHoverColor = Color.FromArgb(CByte(80), CByte(0), CByte(0), CByte(0))
        ModernComboBox1.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        ModernComboBox1.DropDownPadding = New Padding(10)
        ModernComboBox1.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernComboBox1.DropDownSelectedForeColor = Color.White
        ModernComboBox1.Editable = True
        ModernComboBox1.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        ModernComboBox1.Items.Add("64*8")
        ModernComboBox1.Items.Add("64*10")
        ModernComboBox1.Items.Add("64*12")
        ModernComboBox1.Location = New Point(0, 10)
        ModernComboBox1.Margin = New Padding(2, 2, 2, 2)
        ModernComboBox1.Name = "ModernComboBox1"
        ModernComboBox1.Padding = New Padding(10, 0, 10, 0)
        ModernComboBox1.Size = New Size(150, 32)
        ModernComboBox1.TabIndex = 1
        ModernComboBox1.ToolTipGap = -1
        ModernComboBox1.ToolTipMaxWidth = 350
        ModernComboBox1.ToolTipPadding = New Padding(15)
        ModernComboBox1.WaterText = "hi"
        ModernComboBox1.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' HtmlColorLabel7
        ' 
        HtmlColorLabel7.AutoSize = True
        HtmlColorLabel7.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel7.Dock = DockStyle.Top
        HtmlColorLabel7.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel7.Location = New Point(0, 81)
        HtmlColorLabel7.Margin = New Padding(2)
        HtmlColorLabel7.Name = "HtmlColorLabel7"
        HtmlColorLabel7.Size = New Size(472, 61)
        HtmlColorLabel7.TabIndex = 15
        HtmlColorLabel7.Text = "格式：64 个像素 × 每像素平均差值 ?<br>例如：<span style=""color:YellowGreen"">64*10</span> 或 <span style=""color:YellowGreen"">640</span>，写乘法和结果都可以<br>表示：如果有任一 8*8 块中的每个像素平均变化了 10 灰度级则不丢帧"
        ' 
        ' HtmlColorLabel5
        ' 
        HtmlColorLabel5.AutoSize = True
        HtmlColorLabel5.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel5.Dock = DockStyle.Top
        HtmlColorLabel5.Font = New Font("Microsoft YaHei UI", 11F)
        HtmlColorLabel5.Location = New Point(0, 45)
        HtmlColorLabel5.Margin = New Padding(2)
        HtmlColorLabel5.Name = "HtmlColorLabel5"
        HtmlColorLabel5.Padding = New Padding(0, 15, 0, 0)
        HtmlColorLabel5.Size = New Size(472, 36)
        HtmlColorLabel5.TabIndex = 14
        HtmlColorLabel5.Text = "高阈值，所有 <span style=""color:YellowGreen"">8*8=64</span> 的像素块差异最大值"
        ' 
        ' HtmlColorLabel6
        ' 
        HtmlColorLabel6.AutoSize = True
        HtmlColorLabel6.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel6.Dock = DockStyle.Top
        HtmlColorLabel6.Location = New Point(0, 0)
        HtmlColorLabel6.Margin = New Padding(2)
        HtmlColorLabel6.Name = "HtmlColorLabel6"
        HtmlColorLabel6.Padding = New Padding(0, 20, 0, 0)
        HtmlColorLabel6.Size = New Size(472, 45)
        HtmlColorLabel6.TabIndex = 13
        HtmlColorLabel6.Text = "<span style=""font-size:13"">帧丢弃判定</span>"
        ' 
        ' Panel6
        ' 
        Panel6.Controls.Add(HtmlColorLabel13)
        Panel6.Controls.Add(HtmlColorLabel12)
        Panel6.Controls.Add(Panel1)
        Panel6.Controls.Add(HtmlColorLabel3)
        Panel6.Controls.Add(HtmlColorLabel4)
        Panel6.Controls.Add(Panel2)
        Panel6.Controls.Add(HtmlColorLabel2)
        Panel6.Controls.Add(HtmlColorLabel1)
        Panel6.Dock = DockStyle.Left
        Panel6.Location = New Point(20, 44)
        Panel6.Name = "Panel6"
        Panel6.Size = New Size(472, 497)
        Panel6.TabIndex = 21
        ' 
        ' HtmlColorLabel13
        ' 
        HtmlColorLabel13.AutoSize = True
        HtmlColorLabel13.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel13.Dock = DockStyle.Fill
        HtmlColorLabel13.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel13.Location = New Point(0, 316)
        HtmlColorLabel13.Margin = New Padding(2)
        HtmlColorLabel13.Name = "HtmlColorLabel13"
        HtmlColorLabel13.Padding = New Padding(0, 0, 20, 0)
        HtmlColorLabel13.Size = New Size(472, 181)
        HtmlColorLabel13.TabIndex = 14
        HtmlColorLabel13.Text = "当你决定要对视频抽帧时，即代表你认为视频的细节不重要，且没有收藏意义。如果不能同时满足这两点，则不应考虑使用。抽帧是在能够正确传达信息的前提下以细节大量损失为代价换取体积大幅降低来极大增加信息传播效率的手段，属于压片战争的邪修流。如果你的存储空间紧张到需要对收藏内容进行抽帧了，此时你应该去扩充空间，而不是损失自己的收藏。"
        ' 
        ' HtmlColorLabel12
        ' 
        HtmlColorLabel12.AutoSize = True
        HtmlColorLabel12.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel12.Dock = DockStyle.Top
        HtmlColorLabel12.Location = New Point(0, 266)
        HtmlColorLabel12.Margin = New Padding(2)
        HtmlColorLabel12.Name = "HtmlColorLabel12"
        HtmlColorLabel12.Padding = New Padding(0, 20, 0, 5)
        HtmlColorLabel12.Size = New Size(472, 50)
        HtmlColorLabel12.TabIndex = 13
        HtmlColorLabel12.Text = "<span style=""font-size:13"">如何决定是否需要抽帧</span>   <span style=""color:IndianRed"">三思而后行</span>"
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(ModernTextBox2)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(0, 224)
        Panel1.Name = "Panel1"
        Panel1.Padding = New Padding(0, 10, 0, 0)
        Panel1.Size = New Size(472, 42)
        Panel1.TabIndex = 12
        ' 
        ' ModernTextBox2
        ' 
        ModernTextBox2.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernTextBox2.BorderColor = Color.Transparent
        ModernTextBox2.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        ModernTextBox2.BorderRadius = 10
        ModernTextBox2.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        ModernTextBox2.Dock = DockStyle.Left
        ModernTextBox2.Location = New Point(0, 10)
        ModernTextBox2.Margin = New Padding(2)
        ModernTextBox2.Name = "ModernTextBox2"
        ModernTextBox2.Padding = New Padding(10, 0, 10, 0)
        ModernTextBox2.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernTextBox2.Size = New Size(100, 32)
        ModernTextBox2.TabIndex = 6
        ModernTextBox2.WaterText = "keep"
        ModernTextBox2.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' HtmlColorLabel3
        ' 
        HtmlColorLabel3.AutoSize = True
        HtmlColorLabel3.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel3.Dock = DockStyle.Top
        HtmlColorLabel3.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel3.Location = New Point(0, 203)
        HtmlColorLabel3.Margin = New Padding(2)
        HtmlColorLabel3.Name = "HtmlColorLabel3"
        HtmlColorLabel3.Size = New Size(472, 21)
        HtmlColorLabel3.TabIndex = 11
        HtmlColorLabel3.Text = "连续相似帧达到多少才开始丢"
        ' 
        ' HtmlColorLabel4
        ' 
        HtmlColorLabel4.AutoSize = True
        HtmlColorLabel4.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel4.Dock = DockStyle.Top
        HtmlColorLabel4.Location = New Point(0, 153)
        HtmlColorLabel4.Margin = New Padding(2)
        HtmlColorLabel4.Name = "HtmlColorLabel4"
        HtmlColorLabel4.Padding = New Padding(0, 20, 0, 5)
        HtmlColorLabel4.Size = New Size(472, 50)
        HtmlColorLabel4.TabIndex = 10
        HtmlColorLabel4.Text = "<span style=""font-size:13"">连续相似要求</span>   <span style=""font-size:10pt; color:YellowGreen"">默认：0</span>"
        ' 
        ' Panel2
        ' 
        Panel2.Controls.Add(ModernTextBox1)
        Panel2.Dock = DockStyle.Top
        Panel2.Location = New Point(0, 111)
        Panel2.Name = "Panel2"
        Panel2.Padding = New Padding(0, 10, 0, 0)
        Panel2.Size = New Size(472, 42)
        Panel2.TabIndex = 9
        ' 
        ' ModernTextBox1
        ' 
        ModernTextBox1.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernTextBox1.BorderColor = Color.Transparent
        ModernTextBox1.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        ModernTextBox1.BorderRadius = 10
        ModernTextBox1.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        ModernTextBox1.Dock = DockStyle.Left
        ModernTextBox1.Location = New Point(0, 10)
        ModernTextBox1.Margin = New Padding(2)
        ModernTextBox1.Name = "ModernTextBox1"
        ModernTextBox1.Padding = New Padding(10, 0, 10, 0)
        ModernTextBox1.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernTextBox1.Size = New Size(100, 32)
        ModernTextBox1.TabIndex = 6
        ModernTextBox1.WaterText = "max"
        ModernTextBox1.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' HtmlColorLabel2
        ' 
        HtmlColorLabel2.AutoSize = True
        HtmlColorLabel2.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel2.Dock = DockStyle.Top
        HtmlColorLabel2.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel2.Location = New Point(0, 50)
        HtmlColorLabel2.Margin = New Padding(2)
        HtmlColorLabel2.Name = "HtmlColorLabel2"
        HtmlColorLabel2.Size = New Size(472, 61)
        HtmlColorLabel2.TabIndex = 8
        HtmlColorLabel2.Text = "正数：最多允许连续丢弃的帧数<br>负数：两次丢帧之间的最小间隔帧数<br>0：不限制，无论之前连续丢了多少帧都可以继续丢"
        ' 
        ' HtmlColorLabel1
        ' 
        HtmlColorLabel1.AutoSize = True
        HtmlColorLabel1.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel1.Dock = DockStyle.Top
        HtmlColorLabel1.Location = New Point(0, 0)
        HtmlColorLabel1.Margin = New Padding(2)
        HtmlColorLabel1.Name = "HtmlColorLabel1"
        HtmlColorLabel1.Padding = New Padding(0, 20, 0, 5)
        HtmlColorLabel1.Size = New Size(472, 50)
        HtmlColorLabel1.TabIndex = 7
        HtmlColorLabel1.Text = "<span style=""font-size:13"">连续丢帧数量</span>   <span style=""font-size:10pt; color:YellowGreen"">默认：0</span>"
        ' 
        ' ModernCheckBox1
        ' 
        ModernCheckBox1.AutoSize = True
        ModernCheckBox1.BoxBorderRadius = 5
        ModernCheckBox1.BoxBorderSize = 0
        ModernCheckBox1.BoxCheckedBackColor = Color.OliveDrab
        ModernCheckBox1.BoxInnerPadding = 6
        ModernCheckBox1.BoxSize = 24
        ModernCheckBox1.BoxTextSpacing = 10
        ModernCheckBox1.BoxUncheckedBackColor = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        ModernCheckBox1.CheckMarkWidth = 3F
        ModernCheckBox1.Dock = DockStyle.Top
        ModernCheckBox1.Location = New Point(20, 20)
        ModernCheckBox1.Name = "ModernCheckBox1"
        ModernCheckBox1.Size = New Size(944, 24)
        ModernCheckBox1.TabIndex = 22
        ModernCheckBox1.Text = "抽帧总开关 / 勾选才会使用"
        ' 
        ' Form_v6_参数面板_抽帧参数
        ' 
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ClientSize = New Size(984, 561)
        Controls.Add(ModernPanel1)
        Font = New Font("Microsoft YaHei UI", 10F)
        ForeColor = Color.Silver
        MaximizeBox = False
        MinimizeBox = False
        MinimumSize = New Size(1000, 600)
        Name = "Form_v6_参数面板_抽帧参数"
        ShowIcon = False
        ShowInTaskbar = False
        StartPosition = FormStartPosition.Manual
        Text = "视频抽帧"
        ModernPanel1.ResumeLayout(False)
        ModernPanel1.PerformLayout()
        Panel7.ResumeLayout(False)
        Panel7.PerformLayout()
        Panel5.ResumeLayout(False)
        Panel4.ResumeLayout(False)
        Panel3.ResumeLayout(False)
        Panel6.ResumeLayout(False)
        Panel6.PerformLayout()
        Panel1.ResumeLayout(False)
        Panel2.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents ModernPanel1 As LakeUI.ModernPanel
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ModernTextBox2 As LakeUI.ModernTextBox
    Friend WithEvents HtmlColorLabel3 As LakeUI.HtmlColorLabel
    Friend WithEvents HtmlColorLabel4 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents ModernTextBox1 As LakeUI.ModernTextBox
    Friend WithEvents HtmlColorLabel2 As LakeUI.HtmlColorLabel
    Friend WithEvents HtmlColorLabel1 As LakeUI.HtmlColorLabel
    Friend WithEvents ModernCheckBox1 As LakeUI.ModernCheckBox
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents ModernComboBox1 As LakeUI.ModernComboBox
    Friend WithEvents HtmlColorLabel7 As LakeUI.HtmlColorLabel
    Friend WithEvents HtmlColorLabel5 As LakeUI.HtmlColorLabel
    Friend WithEvents HtmlColorLabel6 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents ModernTextBox3 As LakeUI.ModernTextBox
    Friend WithEvents HtmlColorLabel11 As LakeUI.HtmlColorLabel
    Friend WithEvents HtmlColorLabel10 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents ModernComboBox2 As LakeUI.ModernComboBox
    Friend WithEvents HtmlColorLabel9 As LakeUI.HtmlColorLabel
    Friend WithEvents HtmlColorLabel8 As LakeUI.HtmlColorLabel
    Friend WithEvents HtmlColorLabel12 As LakeUI.HtmlColorLabel
    Friend WithEvents HtmlColorLabel13 As LakeUI.HtmlColorLabel
End Class
