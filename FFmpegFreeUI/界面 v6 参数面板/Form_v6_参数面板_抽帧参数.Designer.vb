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
        MTB_最大变化占比 = New LakeUI.ModernTextBox()
        HCL_最大变化占比说明 = New LakeUI.HtmlColorLabel()
        HCL_最大变化占比标签 = New LakeUI.HtmlColorLabel()
        Panel4 = New Panel()
        MCB_低阈值 = New LakeUI.ModernComboBox()
        HCL_低阈值说明 = New LakeUI.HtmlColorLabel()
        HCL_低阈值标签 = New LakeUI.HtmlColorLabel()
        Panel3 = New Panel()
        MCB_高阈值 = New LakeUI.ModernComboBox()
        HCL_高阈值说明 = New LakeUI.HtmlColorLabel()
        HCL_高阈值标签 = New LakeUI.HtmlColorLabel()
        HCL_帧丢弃判定标题 = New LakeUI.HtmlColorLabel()
        Panel6 = New Panel()
        HCL_抽帧风险说明 = New LakeUI.HtmlColorLabel()
        HCL_抽帧风险标题 = New LakeUI.HtmlColorLabel()
        Panel1 = New Panel()
        MTB_连续相似要求 = New LakeUI.ModernTextBox()
        HCL_连续相似要求说明 = New LakeUI.HtmlColorLabel()
        HCL_连续相似要求标题 = New LakeUI.HtmlColorLabel()
        Panel2 = New Panel()
        MTB_连续丢帧数量 = New LakeUI.ModernTextBox()
        HCL_连续丢帧数量说明 = New LakeUI.HtmlColorLabel()
        HCL_连续丢帧数量标题 = New LakeUI.HtmlColorLabel()
        MCK_抽帧总开关 = New LakeUI.ModernCheckBox()
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
        ModernPanel1.Controls.Add(MCK_抽帧总开关)
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
        Panel7.Controls.Add(HCL_最大变化占比说明)
        Panel7.Controls.Add(HCL_最大变化占比标签)
        Panel7.Controls.Add(Panel4)
        Panel7.Controls.Add(HCL_低阈值说明)
        Panel7.Controls.Add(HCL_低阈值标签)
        Panel7.Controls.Add(Panel3)
        Panel7.Controls.Add(HCL_高阈值说明)
        Panel7.Controls.Add(HCL_高阈值标签)
        Panel7.Controls.Add(HCL_帧丢弃判定标题)
        Panel7.Dock = DockStyle.Fill
        Panel7.Location = New Point(492, 44)
        Panel7.Name = "Panel7"
        Panel7.Size = New Size(472, 497)
        Panel7.TabIndex = 23
        '
        ' Panel5
        '
        Panel5.Controls.Add(MTB_最大变化占比)
        Panel5.Dock = DockStyle.Top
        Panel5.Location = New Point(0, 360)
        Panel5.Name = "Panel5"
        Panel5.Padding = New Padding(0, 10, 0, 0)
        Panel5.Size = New Size(472, 42)
        Panel5.TabIndex = 22
        '
        ' MTB_最大变化占比
        '
        MTB_最大变化占比.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_最大变化占比.BorderColor = Color.Transparent
        MTB_最大变化占比.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MTB_最大变化占比.BorderRadius = 10
        MTB_最大变化占比.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        MTB_最大变化占比.Dock = DockStyle.Left
        MTB_最大变化占比.Location = New Point(0, 10)
        MTB_最大变化占比.Margin = New Padding(2)
        MTB_最大变化占比.Name = "MTB_最大变化占比"
        MTB_最大变化占比.Padding = New Padding(10, 0, 10, 0)
        MTB_最大变化占比.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_最大变化占比.Size = New Size(100, 32)
        MTB_最大变化占比.TabIndex = 6
        MTB_最大变化占比.WaterText = "frac"
        MTB_最大变化占比.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        '
        ' HCL_最大变化占比说明
        '
        HCL_最大变化占比说明.AutoSize = True
        HCL_最大变化占比说明.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_最大变化占比说明.Dock = DockStyle.Top
        HCL_最大变化占比说明.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_最大变化占比说明.Location = New Point(0, 339)
        HCL_最大变化占比说明.Margin = New Padding(2)
        HCL_最大变化占比说明.Name = "HCL_最大变化占比说明"
        HCL_最大变化占比说明.Size = New Size(472, 21)
        HCL_最大变化占比说明.TabIndex = 21
        HCL_最大变化占比说明.Text = "例如 0.1 表示只有 10% 以下的变化才会丢帧"
        '
        ' HCL_最大变化占比标签
        '
        HCL_最大变化占比标签.AutoSize = True
        HCL_最大变化占比标签.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_最大变化占比标签.Dock = DockStyle.Top
        HCL_最大变化占比标签.Font = New Font("Microsoft YaHei UI", 11F)
        HCL_最大变化占比标签.Location = New Point(0, 303)
        HCL_最大变化占比标签.Margin = New Padding(2)
        HCL_最大变化占比标签.Name = "HCL_最大变化占比标签"
        HCL_最大变化占比标签.Padding = New Padding(0, 15, 0, 0)
        HCL_最大变化占比标签.Size = New Size(472, 36)
        HCL_最大变化占比标签.TabIndex = 20
        HCL_最大变化占比标签.Text = "允许超过低阈值的最大占比（1=整张图）"
        '
        ' Panel4
        '
        Panel4.Controls.Add(MCB_低阈值)
        Panel4.Dock = DockStyle.Top
        Panel4.Location = New Point(0, 261)
        Panel4.Name = "Panel4"
        Panel4.Padding = New Padding(0, 10, 0, 0)
        Panel4.Size = New Size(472, 42)
        Panel4.TabIndex = 19
        '
        ' MCB_低阈值
        '
        MCB_低阈值.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_低阈值.BorderRadius = 10
        MCB_低阈值.BorderSize = 0
        MCB_低阈值.Dock = DockStyle.Left
        MCB_低阈值.DropDownBackdropBlurPasses = 2
        MCB_低阈值.DropDownBackdropBlurRadius = 30
        MCB_低阈值.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_低阈值.DropDownHoverAnimationDuration = 0
        MCB_低阈值.DropDownHoverColor = Color.FromArgb(CByte(80), CByte(0), CByte(0), CByte(0))
        MCB_低阈值.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_低阈值.DropDownPadding = New Padding(10)
        MCB_低阈值.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_低阈值.DropDownSelectedForeColor = Color.White
        MCB_低阈值.Editable = True
        MCB_低阈值.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_低阈值.Items.Add("64*4")
        MCB_低阈值.Items.Add("64*6")
        MCB_低阈值.Items.Add("64*8")
        MCB_低阈值.Items.Add("64*10")
        MCB_低阈值.Location = New Point(0, 10)
        MCB_低阈值.Margin = New Padding(2, 2, 2, 2)
        MCB_低阈值.Name = "MCB_低阈值"
        MCB_低阈值.Padding = New Padding(10, 0, 10, 0)
        MCB_低阈值.Size = New Size(150, 32)
        MCB_低阈值.TabIndex = 1
        MCB_低阈值.ToolTipGap = -1
        MCB_低阈值.ToolTipMaxWidth = 350
        MCB_低阈值.ToolTipPadding = New Padding(15)
        MCB_低阈值.WaterText = "lo"
        MCB_低阈值.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        '
        ' HCL_低阈值说明
        '
        HCL_低阈值说明.AutoSize = True
        HCL_低阈值说明.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_低阈值说明.Dock = DockStyle.Top
        HCL_低阈值说明.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_低阈值说明.Location = New Point(0, 220)
        HCL_低阈值说明.Margin = New Padding(2)
        HCL_低阈值说明.Name = "HCL_低阈值说明"
        HCL_低阈值说明.Size = New Size(472, 41)
        HCL_低阈值说明.TabIndex = 18
        HCL_低阈值说明.Text = "（格式同上）在满足高阈值的前提下<br>变化必须超过低阈值且不能超过最大占比才会丢帧"
        '
        ' HCL_低阈值标签
        '
        HCL_低阈值标签.AutoSize = True
        HCL_低阈值标签.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_低阈值标签.Dock = DockStyle.Top
        HCL_低阈值标签.Font = New Font("Microsoft YaHei UI", 11F)
        HCL_低阈值标签.Location = New Point(0, 184)
        HCL_低阈值标签.Margin = New Padding(2)
        HCL_低阈值标签.Name = "HCL_低阈值标签"
        HCL_低阈值标签.Padding = New Padding(0, 15, 0, 0)
        HCL_低阈值标签.Size = New Size(472, 36)
        HCL_低阈值标签.TabIndex = 17
        HCL_低阈值标签.Text = "低阈值，所有 <span style=""color:YellowGreen"">8*8=64</span> 的像素块差异最小值"
        '
        ' Panel3
        '
        Panel3.Controls.Add(MCB_高阈值)
        Panel3.Dock = DockStyle.Top
        Panel3.Location = New Point(0, 142)
        Panel3.Name = "Panel3"
        Panel3.Padding = New Padding(0, 10, 0, 0)
        Panel3.Size = New Size(472, 42)
        Panel3.TabIndex = 16
        '
        ' MCB_高阈值
        '
        MCB_高阈值.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_高阈值.BorderRadius = 10
        MCB_高阈值.BorderSize = 0
        MCB_高阈值.Dock = DockStyle.Left
        MCB_高阈值.DropDownBackdropBlurPasses = 2
        MCB_高阈值.DropDownBackdropBlurRadius = 30
        MCB_高阈值.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_高阈值.DropDownHoverAnimationDuration = 0
        MCB_高阈值.DropDownHoverColor = Color.FromArgb(CByte(80), CByte(0), CByte(0), CByte(0))
        MCB_高阈值.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_高阈值.DropDownPadding = New Padding(10)
        MCB_高阈值.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_高阈值.DropDownSelectedForeColor = Color.White
        MCB_高阈值.Editable = True
        MCB_高阈值.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_高阈值.Items.Add("64*8")
        MCB_高阈值.Items.Add("64*10")
        MCB_高阈值.Items.Add("64*12")
        MCB_高阈值.Location = New Point(0, 10)
        MCB_高阈值.Margin = New Padding(2, 2, 2, 2)
        MCB_高阈值.Name = "MCB_高阈值"
        MCB_高阈值.Padding = New Padding(10, 0, 10, 0)
        MCB_高阈值.Size = New Size(150, 32)
        MCB_高阈值.TabIndex = 1
        MCB_高阈值.ToolTipGap = -1
        MCB_高阈值.ToolTipMaxWidth = 350
        MCB_高阈值.ToolTipPadding = New Padding(15)
        MCB_高阈值.WaterText = "hi"
        MCB_高阈值.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        '
        ' HCL_高阈值说明
        '
        HCL_高阈值说明.AutoSize = True
        HCL_高阈值说明.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_高阈值说明.Dock = DockStyle.Top
        HCL_高阈值说明.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_高阈值说明.Location = New Point(0, 81)
        HCL_高阈值说明.Margin = New Padding(2)
        HCL_高阈值说明.Name = "HCL_高阈值说明"
        HCL_高阈值说明.Size = New Size(472, 61)
        HCL_高阈值说明.TabIndex = 15
        HCL_高阈值说明.Text = "格式：64 个像素 × 每像素平均差值 ?<br>例如：<span style=""color:YellowGreen"">64*10</span> 或 <span style=""color:YellowGreen"">640</span>，写乘法和结果都可以<br>表示：如果有任一 8*8 块中的每个像素平均变化了 10 灰度级则不丢帧"
        '
        ' HCL_高阈值标签
        '
        HCL_高阈值标签.AutoSize = True
        HCL_高阈值标签.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_高阈值标签.Dock = DockStyle.Top
        HCL_高阈值标签.Font = New Font("Microsoft YaHei UI", 11F)
        HCL_高阈值标签.Location = New Point(0, 45)
        HCL_高阈值标签.Margin = New Padding(2)
        HCL_高阈值标签.Name = "HCL_高阈值标签"
        HCL_高阈值标签.Padding = New Padding(0, 15, 0, 0)
        HCL_高阈值标签.Size = New Size(472, 36)
        HCL_高阈值标签.TabIndex = 14
        HCL_高阈值标签.Text = "高阈值，所有 <span style=""color:YellowGreen"">8*8=64</span> 的像素块差异最大值"
        '
        ' HCL_帧丢弃判定标题
        '
        HCL_帧丢弃判定标题.AutoSize = True
        HCL_帧丢弃判定标题.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_帧丢弃判定标题.Dock = DockStyle.Top
        HCL_帧丢弃判定标题.Location = New Point(0, 0)
        HCL_帧丢弃判定标题.Margin = New Padding(2)
        HCL_帧丢弃判定标题.Name = "HCL_帧丢弃判定标题"
        HCL_帧丢弃判定标题.Padding = New Padding(0, 20, 0, 0)
        HCL_帧丢弃判定标题.Size = New Size(472, 45)
        HCL_帧丢弃判定标题.TabIndex = 13
        HCL_帧丢弃判定标题.Text = "<span style=""font-size:13"">帧丢弃判定</span>"
        '
        ' Panel6
        '
        Panel6.Controls.Add(HCL_抽帧风险说明)
        Panel6.Controls.Add(HCL_抽帧风险标题)
        Panel6.Controls.Add(Panel1)
        Panel6.Controls.Add(HCL_连续相似要求说明)
        Panel6.Controls.Add(HCL_连续相似要求标题)
        Panel6.Controls.Add(Panel2)
        Panel6.Controls.Add(HCL_连续丢帧数量说明)
        Panel6.Controls.Add(HCL_连续丢帧数量标题)
        Panel6.Dock = DockStyle.Left
        Panel6.Location = New Point(20, 44)
        Panel6.Name = "Panel6"
        Panel6.Size = New Size(472, 497)
        Panel6.TabIndex = 21
        '
        ' HCL_抽帧风险说明
        '
        HCL_抽帧风险说明.AutoSize = True
        HCL_抽帧风险说明.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_抽帧风险说明.Dock = DockStyle.Fill
        HCL_抽帧风险说明.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_抽帧风险说明.Location = New Point(0, 316)
        HCL_抽帧风险说明.Margin = New Padding(2)
        HCL_抽帧风险说明.Name = "HCL_抽帧风险说明"
        HCL_抽帧风险说明.Padding = New Padding(0, 0, 20, 0)
        HCL_抽帧风险说明.Size = New Size(472, 181)
        HCL_抽帧风险说明.TabIndex = 14
        HCL_抽帧风险说明.Text = "当你决定要对视频抽帧时，即代表你认为视频的细节不重要，且没有收藏意义。如果不能同时满足这两点，则不应考虑使用。抽帧是在能够正确传达信息的前提下以细节大量损失为代价换取体积大幅降低来极大增加信息传播效率的手段，属于压片战争的邪修流。如果你的存储空间紧张到需要对收藏内容进行抽帧了，此时你应该去扩充空间，而不是损失自己的收藏。"
        '
        ' HCL_抽帧风险标题
        '
        HCL_抽帧风险标题.AutoSize = True
        HCL_抽帧风险标题.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_抽帧风险标题.Dock = DockStyle.Top
        HCL_抽帧风险标题.Location = New Point(0, 266)
        HCL_抽帧风险标题.Margin = New Padding(2)
        HCL_抽帧风险标题.Name = "HCL_抽帧风险标题"
        HCL_抽帧风险标题.Padding = New Padding(0, 20, 0, 5)
        HCL_抽帧风险标题.Size = New Size(472, 50)
        HCL_抽帧风险标题.TabIndex = 13
        HCL_抽帧风险标题.Text = "<span style=""font-size:13"">如何决定是否需要抽帧</span>   <span style=""color:IndianRed"">三思而后行</span>"
        '
        ' Panel1
        '
        Panel1.Controls.Add(MTB_连续相似要求)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(0, 224)
        Panel1.Name = "Panel1"
        Panel1.Padding = New Padding(0, 10, 0, 0)
        Panel1.Size = New Size(472, 42)
        Panel1.TabIndex = 12
        '
        ' MTB_连续相似要求
        '
        MTB_连续相似要求.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_连续相似要求.BorderColor = Color.Transparent
        MTB_连续相似要求.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MTB_连续相似要求.BorderRadius = 10
        MTB_连续相似要求.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        MTB_连续相似要求.Dock = DockStyle.Left
        MTB_连续相似要求.Location = New Point(0, 10)
        MTB_连续相似要求.Margin = New Padding(2)
        MTB_连续相似要求.Name = "MTB_连续相似要求"
        MTB_连续相似要求.Padding = New Padding(10, 0, 10, 0)
        MTB_连续相似要求.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_连续相似要求.Size = New Size(100, 32)
        MTB_连续相似要求.TabIndex = 6
        MTB_连续相似要求.WaterText = "keep"
        MTB_连续相似要求.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        '
        ' HCL_连续相似要求说明
        '
        HCL_连续相似要求说明.AutoSize = True
        HCL_连续相似要求说明.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_连续相似要求说明.Dock = DockStyle.Top
        HCL_连续相似要求说明.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_连续相似要求说明.Location = New Point(0, 203)
        HCL_连续相似要求说明.Margin = New Padding(2)
        HCL_连续相似要求说明.Name = "HCL_连续相似要求说明"
        HCL_连续相似要求说明.Size = New Size(472, 21)
        HCL_连续相似要求说明.TabIndex = 11
        HCL_连续相似要求说明.Text = "连续相似帧达到多少才开始丢"
        '
        ' HCL_连续相似要求标题
        '
        HCL_连续相似要求标题.AutoSize = True
        HCL_连续相似要求标题.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_连续相似要求标题.Dock = DockStyle.Top
        HCL_连续相似要求标题.Location = New Point(0, 153)
        HCL_连续相似要求标题.Margin = New Padding(2)
        HCL_连续相似要求标题.Name = "HCL_连续相似要求标题"
        HCL_连续相似要求标题.Padding = New Padding(0, 20, 0, 5)
        HCL_连续相似要求标题.Size = New Size(472, 50)
        HCL_连续相似要求标题.TabIndex = 10
        HCL_连续相似要求标题.Text = "<span style=""font-size:13"">连续相似要求</span>   <span style=""font-size:10pt; color:YellowGreen"">默认：0</span>"
        '
        ' Panel2
        '
        Panel2.Controls.Add(MTB_连续丢帧数量)
        Panel2.Dock = DockStyle.Top
        Panel2.Location = New Point(0, 111)
        Panel2.Name = "Panel2"
        Panel2.Padding = New Padding(0, 10, 0, 0)
        Panel2.Size = New Size(472, 42)
        Panel2.TabIndex = 9
        '
        ' MTB_连续丢帧数量
        '
        MTB_连续丢帧数量.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_连续丢帧数量.BorderColor = Color.Transparent
        MTB_连续丢帧数量.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MTB_连续丢帧数量.BorderRadius = 10
        MTB_连续丢帧数量.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        MTB_连续丢帧数量.Dock = DockStyle.Left
        MTB_连续丢帧数量.Location = New Point(0, 10)
        MTB_连续丢帧数量.Margin = New Padding(2)
        MTB_连续丢帧数量.Name = "MTB_连续丢帧数量"
        MTB_连续丢帧数量.Padding = New Padding(10, 0, 10, 0)
        MTB_连续丢帧数量.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_连续丢帧数量.Size = New Size(100, 32)
        MTB_连续丢帧数量.TabIndex = 6
        MTB_连续丢帧数量.WaterText = "max"
        MTB_连续丢帧数量.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        '
        ' HCL_连续丢帧数量说明
        '
        HCL_连续丢帧数量说明.AutoSize = True
        HCL_连续丢帧数量说明.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_连续丢帧数量说明.Dock = DockStyle.Top
        HCL_连续丢帧数量说明.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_连续丢帧数量说明.Location = New Point(0, 50)
        HCL_连续丢帧数量说明.Margin = New Padding(2)
        HCL_连续丢帧数量说明.Name = "HCL_连续丢帧数量说明"
        HCL_连续丢帧数量说明.Size = New Size(472, 61)
        HCL_连续丢帧数量说明.TabIndex = 8
        HCL_连续丢帧数量说明.Text = "正数：最多允许连续丢弃的帧数<br>负数：两次丢帧之间的最小间隔帧数<br>0：不限制，无论之前连续丢了多少帧都可以继续丢"
        '
        ' HCL_连续丢帧数量标题
        '
        HCL_连续丢帧数量标题.AutoSize = True
        HCL_连续丢帧数量标题.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_连续丢帧数量标题.Dock = DockStyle.Top
        HCL_连续丢帧数量标题.Location = New Point(0, 0)
        HCL_连续丢帧数量标题.Margin = New Padding(2)
        HCL_连续丢帧数量标题.Name = "HCL_连续丢帧数量标题"
        HCL_连续丢帧数量标题.Padding = New Padding(0, 20, 0, 5)
        HCL_连续丢帧数量标题.Size = New Size(472, 50)
        HCL_连续丢帧数量标题.TabIndex = 7
        HCL_连续丢帧数量标题.Text = "<span style=""font-size:13"">连续丢帧数量</span>   <span style=""font-size:10pt; color:YellowGreen"">默认：0</span>"
        '
        ' MCK_抽帧总开关
        '
        MCK_抽帧总开关.AutoSize = True
        MCK_抽帧总开关.BoxBorderRadius = 5
        MCK_抽帧总开关.BoxBorderSize = 0
        MCK_抽帧总开关.BoxCheckedBackColor = Color.OliveDrab
        MCK_抽帧总开关.BoxInnerPadding = 6
        MCK_抽帧总开关.BoxSize = 24
        MCK_抽帧总开关.BoxTextSpacing = 10
        MCK_抽帧总开关.BoxUncheckedBackColor = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCK_抽帧总开关.CheckMarkWidth = 3F
        MCK_抽帧总开关.Dock = DockStyle.Top
        MCK_抽帧总开关.Location = New Point(20, 20)
        MCK_抽帧总开关.Name = "MCK_抽帧总开关"
        MCK_抽帧总开关.Size = New Size(944, 24)
        MCK_抽帧总开关.TabIndex = 22
        MCK_抽帧总开关.Text = "抽帧总开关 / 勾选才会使用"
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
    Friend WithEvents MTB_连续相似要求 As LakeUI.ModernTextBox
    Friend WithEvents HCL_连续相似要求说明 As LakeUI.HtmlColorLabel
    Friend WithEvents HCL_连续相似要求标题 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents MTB_连续丢帧数量 As LakeUI.ModernTextBox
    Friend WithEvents HCL_连续丢帧数量说明 As LakeUI.HtmlColorLabel
    Friend WithEvents HCL_连续丢帧数量标题 As LakeUI.HtmlColorLabel
    Friend WithEvents MCK_抽帧总开关 As LakeUI.ModernCheckBox
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents MCB_高阈值 As LakeUI.ModernComboBox
    Friend WithEvents HCL_高阈值说明 As LakeUI.HtmlColorLabel
    Friend WithEvents HCL_高阈值标签 As LakeUI.HtmlColorLabel
    Friend WithEvents HCL_帧丢弃判定标题 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents MTB_最大变化占比 As LakeUI.ModernTextBox
    Friend WithEvents HCL_最大变化占比说明 As LakeUI.HtmlColorLabel
    Friend WithEvents HCL_最大变化占比标签 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents MCB_低阈值 As LakeUI.ModernComboBox
    Friend WithEvents HCL_低阈值说明 As LakeUI.HtmlColorLabel
    Friend WithEvents HCL_低阈值标签 As LakeUI.HtmlColorLabel
    Friend WithEvents HCL_抽帧风险标题 As LakeUI.HtmlColorLabel
    Friend WithEvents HCL_抽帧风险说明 As LakeUI.HtmlColorLabel
End Class
