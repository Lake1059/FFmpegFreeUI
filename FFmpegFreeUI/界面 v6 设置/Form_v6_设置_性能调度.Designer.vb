<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_v6_设置_性能调度
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
        Panel3 = New Panel()
        MCB_编码队列刷新速度 = New LakeUI.ModernComboBox()
        HtmlColorLabel3 = New LakeUI.HtmlColorLabel()
        Panel1 = New Panel()
        MCB_自动开始数量 = New LakeUI.ModernComboBox()
        HtmlColorLabel2 = New LakeUI.HtmlColorLabel()
        Panel2 = New Panel()
        HtmlColorLabel6 = New LakeUI.HtmlColorLabel()
        ModernTextBox2 = New LakeUI.ModernTextBox()
        JustEmptyControl1 = New LakeUI.JustEmptyControl()
        MTB_处理器线程 = New LakeUI.ModernTextBox()
        HtmlColorLabel1 = New LakeUI.HtmlColorLabel()
        ModernPanel1.SuspendLayout()
        Panel3.SuspendLayout()
        Panel1.SuspendLayout()
        Panel2.SuspendLayout()
        SuspendLayout()
        ' 
        ' ModernPanel1
        ' 
        ModernPanel1.BackColor1 = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ModernPanel1.BorderSize = 0
        ModernPanel1.Controls.Add(Panel3)
        ModernPanel1.Controls.Add(HtmlColorLabel3)
        ModernPanel1.Controls.Add(Panel1)
        ModernPanel1.Controls.Add(HtmlColorLabel2)
        ModernPanel1.Controls.Add(Panel2)
        ModernPanel1.Controls.Add(HtmlColorLabel1)
        ModernPanel1.Dock = DockStyle.Fill
        ModernPanel1.Location = New Point(0, 0)
        ModernPanel1.Name = "ModernPanel1"
        ModernPanel1.Padding = New Padding(20)
        ModernPanel1.ScrollBarMode = LakeUI.ModernPanel.ScrollMode.Vertical
        ModernPanel1.Size = New Size(739, 636)
        ModernPanel1.TabIndex = 1
        ' 
        ' Panel3
        ' 
        Panel3.Controls.Add(MCB_编码队列刷新速度)
        Panel3.Dock = DockStyle.Top
        Panel3.Location = New Point(20, 219)
        Panel3.Name = "Panel3"
        Panel3.Padding = New Padding(0, 10, 0, 0)
        Panel3.Size = New Size(699, 42)
        Panel3.TabIndex = 15
        ' 
        ' MCB_编码队列刷新速度
        ' 
        MCB_编码队列刷新速度.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_编码队列刷新速度.BorderColorFocus = Color.Silver
        MCB_编码队列刷新速度.BorderRadius = 10
        MCB_编码队列刷新速度.BorderSize = 0
        MCB_编码队列刷新速度.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        MCB_编码队列刷新速度.Dock = DockStyle.Left
        MCB_编码队列刷新速度.DropDownBorderSize = 2
        MCB_编码队列刷新速度.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_编码队列刷新速度.DropDownPadding = New Padding(10)
        MCB_编码队列刷新速度.DropDownScrollBarHoverColor = Color.FromArgb(CByte(200), CByte(200), CByte(200))
        MCB_编码队列刷新速度.Items.Add("极快速度（200ms）")
        MCB_编码队列刷新速度.Items.Add("较快速度（500ms）")
        MCB_编码队列刷新速度.Items.Add("默认速度（1s）")
        MCB_编码队列刷新速度.Items.Add("较慢速度（2s）")
        MCB_编码队列刷新速度.Items.Add("极慢速度（3s）")
        MCB_编码队列刷新速度.Location = New Point(0, 10)
        MCB_编码队列刷新速度.Margin = New Padding(2, 2, 2, 2)
        MCB_编码队列刷新速度.MaxDropDownItems = 15
        MCB_编码队列刷新速度.Name = "MCB_编码队列刷新速度"
        MCB_编码队列刷新速度.Padding = New Padding(10, 0, 10, 0)
        MCB_编码队列刷新速度.Size = New Size(200, 32)
        MCB_编码队列刷新速度.TabIndex = 0
        MCB_编码队列刷新速度.ToolTipBorderSize = 2
        MCB_编码队列刷新速度.ToolTipGap = 10
        MCB_编码队列刷新速度.ToolTipMaxWidth = 350
        MCB_编码队列刷新速度.ToolTipPadding = New Padding(15)
        ' 
        ' HtmlColorLabel3
        ' 
        HtmlColorLabel3.AutoSize = True
        HtmlColorLabel3.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel3.Dock = DockStyle.Top
        HtmlColorLabel3.Location = New Point(20, 174)
        HtmlColorLabel3.Margin = New Padding(2)
        HtmlColorLabel3.Name = "HtmlColorLabel3"
        HtmlColorLabel3.Padding = New Padding(0, 20, 0, 0)
        HtmlColorLabel3.Size = New Size(699, 45)
        HtmlColorLabel3.TabIndex = 14
        HtmlColorLabel3.Text = "<span style=""font-size:13"">编码队列刷新速度</span>   <span style=""font-size:10pt; color:DarkGray"">低性能设备可以降低来节约不必要的开支</span>"
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(MCB_自动开始数量)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(20, 132)
        Panel1.Name = "Panel1"
        Panel1.Padding = New Padding(0, 10, 0, 0)
        Panel1.Size = New Size(699, 42)
        Panel1.TabIndex = 13
        ' 
        ' MCB_自动开始数量
        ' 
        MCB_自动开始数量.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_自动开始数量.BorderColorFocus = Color.Silver
        MCB_自动开始数量.BorderRadius = 10
        MCB_自动开始数量.BorderSize = 0
        MCB_自动开始数量.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        MCB_自动开始数量.Dock = DockStyle.Left
        MCB_自动开始数量.DropDownBorderSize = 2
        MCB_自动开始数量.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_自动开始数量.DropDownPadding = New Padding(10)
        MCB_自动开始数量.DropDownScrollBarHoverColor = Color.FromArgb(CByte(200), CByte(200), CByte(200))
        MCB_自动开始数量.Items.Add("同时进行 1 个任务")
        MCB_自动开始数量.Items.Add("同时进行 2 个任务")
        MCB_自动开始数量.Items.Add("同时进行 3 个任务")
        MCB_自动开始数量.Items.Add("同时进行 4 个任务")
        MCB_自动开始数量.Items.Add("同时进行 5 个任务")
        MCB_自动开始数量.Items.Add("同时进行 6 个任务")
        MCB_自动开始数量.Items.Add("同时进行 7 个任务")
        MCB_自动开始数量.Items.Add("同时进行 8 个任务")
        MCB_自动开始数量.Items.Add("同时进行 9 个任务")
        MCB_自动开始数量.Items.Add("同时进行 10 个任务")
        MCB_自动开始数量.Location = New Point(0, 10)
        MCB_自动开始数量.Margin = New Padding(2, 2, 2, 2)
        MCB_自动开始数量.MaxDropDownItems = 15
        MCB_自动开始数量.Name = "MCB_自动开始数量"
        MCB_自动开始数量.Padding = New Padding(10, 0, 10, 0)
        MCB_自动开始数量.Size = New Size(200, 32)
        MCB_自动开始数量.TabIndex = 0
        MCB_自动开始数量.ToolTipBorderSize = 2
        MCB_自动开始数量.ToolTipGap = 10
        MCB_自动开始数量.ToolTipMaxWidth = 350
        MCB_自动开始数量.ToolTipPadding = New Padding(15)
        ' 
        ' HtmlColorLabel2
        ' 
        HtmlColorLabel2.AutoSize = True
        HtmlColorLabel2.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel2.Dock = DockStyle.Top
        HtmlColorLabel2.Location = New Point(20, 87)
        HtmlColorLabel2.Margin = New Padding(2)
        HtmlColorLabel2.Name = "HtmlColorLabel2"
        HtmlColorLabel2.Padding = New Padding(0, 20, 0, 0)
        HtmlColorLabel2.Size = New Size(699, 45)
        HtmlColorLabel2.TabIndex = 12
        HtmlColorLabel2.Text = "<span style=""font-size:13"">自动开始数量</span>   <span style=""font-size:10pt; color:DarkGray"">视频建议 1，音频或图片可以尝试按照 CPU 核心数量</span>"
        ' 
        ' Panel2
        ' 
        Panel2.Controls.Add(HtmlColorLabel6)
        Panel2.Controls.Add(ModernTextBox2)
        Panel2.Controls.Add(JustEmptyControl1)
        Panel2.Controls.Add(MTB_处理器线程)
        Panel2.Dock = DockStyle.Top
        Panel2.Location = New Point(20, 45)
        Panel2.Name = "Panel2"
        Panel2.Padding = New Padding(0, 10, 0, 0)
        Panel2.Size = New Size(699, 42)
        Panel2.TabIndex = 11
        ' 
        ' HtmlColorLabel6
        ' 
        HtmlColorLabel6.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel6.Dock = DockStyle.Fill
        HtmlColorLabel6.ForeColor = Color.Gray
        HtmlColorLabel6.Location = New Point(310, 10)
        HtmlColorLabel6.Margin = New Padding(2)
        HtmlColorLabel6.Name = "HtmlColorLabel6"
        HtmlColorLabel6.Padding = New Padding(10, 0, 0, 0)
        HtmlColorLabel6.Size = New Size(389, 32)
        HtmlColorLabel6.TabIndex = 9
        HtmlColorLabel6.Text = "短文本框：输入 ?~? 然后 Enter 来快速填写"
        HtmlColorLabel6.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        ' 
        ' ModernTextBox2
        ' 
        ModernTextBox2.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernTextBox2.BorderColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernTextBox2.BorderColorFocus = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        ModernTextBox2.BorderRadius = 10
        ModernTextBox2.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        ModernTextBox2.Dock = DockStyle.Left
        ModernTextBox2.Location = New Point(210, 10)
        ModernTextBox2.Margin = New Padding(2, 2, 2, 2)
        ModernTextBox2.Name = "ModernTextBox2"
        ModernTextBox2.Padding = New Padding(10, 0, 10, 0)
        ModernTextBox2.ScrollBarHoverColor = Color.FromArgb(CByte(200), CByte(200), CByte(200))
        ModernTextBox2.Size = New Size(100, 32)
        ModernTextBox2.TabIndex = 8
        ' 
        ' JustEmptyControl1
        ' 
        JustEmptyControl1.Dock = DockStyle.Left
        JustEmptyControl1.Location = New Point(200, 10)
        JustEmptyControl1.Name = "JustEmptyControl1"
        JustEmptyControl1.Size = New Size(10, 32)
        JustEmptyControl1.TabIndex = 7
        ' 
        ' MTB_处理器线程
        ' 
        MTB_处理器线程.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_处理器线程.BorderColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_处理器线程.BorderColorFocus = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MTB_处理器线程.BorderRadius = 10
        MTB_处理器线程.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        MTB_处理器线程.Dock = DockStyle.Left
        MTB_处理器线程.Location = New Point(0, 10)
        MTB_处理器线程.Margin = New Padding(2, 2, 2, 2)
        MTB_处理器线程.Name = "MTB_处理器线程"
        MTB_处理器线程.Padding = New Padding(10, 0, 10, 0)
        MTB_处理器线程.ScrollBarHoverColor = Color.FromArgb(CByte(200), CByte(200), CByte(200))
        MTB_处理器线程.Size = New Size(200, 32)
        MTB_处理器线程.TabIndex = 6
        ' 
        ' HtmlColorLabel1
        ' 
        HtmlColorLabel1.AutoSize = True
        HtmlColorLabel1.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel1.Dock = DockStyle.Top
        HtmlColorLabel1.Location = New Point(20, 20)
        HtmlColorLabel1.Margin = New Padding(2)
        HtmlColorLabel1.Name = "HtmlColorLabel1"
        HtmlColorLabel1.Size = New Size(699, 25)
        HtmlColorLabel1.TabIndex = 10
        HtmlColorLabel1.Text = "<span style=""font-size:13"">处理器核心 && 线程</span>   <span style=""font-size:10pt; color:DarkGray"">任务处理器里的处理器相关性，仅对新任务生效</span>"
        ' 
        ' Form_v6_设置_性能调度
        ' 
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ClientSize = New Size(739, 636)
        Controls.Add(ModernPanel1)
        Font = New Font("Microsoft YaHei UI", 10F)
        ForeColor = Color.Silver
        Name = "Form_v6_设置_性能调度"
        Text = "Form_v6_设置_性能调度"
        ModernPanel1.ResumeLayout(False)
        ModernPanel1.PerformLayout()
        Panel3.ResumeLayout(False)
        Panel1.ResumeLayout(False)
        Panel2.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents ModernPanel1 As LakeUI.ModernPanel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents MCB_自动开始数量 As LakeUI.ModernComboBox
    Friend WithEvents HtmlColorLabel2 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents HtmlColorLabel1 As LakeUI.HtmlColorLabel
    Friend WithEvents MTB_处理器线程 As LakeUI.ModernTextBox
    Friend WithEvents ModernTextBox2 As LakeUI.ModernTextBox
    Friend WithEvents JustEmptyControl1 As LakeUI.JustEmptyControl
    Friend WithEvents HtmlColorLabel6 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents MCB_编码队列刷新速度 As LakeUI.ModernComboBox
    Friend WithEvents HtmlColorLabel3 As LakeUI.HtmlColorLabel
End Class
