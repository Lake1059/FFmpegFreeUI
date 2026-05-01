<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_v6_设置_LakeUI性能选项
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
        Panel6 = New Panel()
        HtmlColorLabel5 = New LakeUI.HtmlColorLabel()
        ModernComboBox6 = New LakeUI.ModernComboBox()
        HtmlColorLabel3 = New LakeUI.HtmlColorLabel()
        Panel5 = New Panel()
        HtmlColorLabel10 = New LakeUI.HtmlColorLabel()
        ModernComboBox5 = New LakeUI.ModernComboBox()
        Panel4 = New Panel()
        HtmlColorLabel9 = New LakeUI.HtmlColorLabel()
        ModernComboBox4 = New LakeUI.ModernComboBox()
        Panel3 = New Panel()
        HtmlColorLabel8 = New LakeUI.HtmlColorLabel()
        ModernComboBox3 = New LakeUI.ModernComboBox()
        Panel1 = New Panel()
        HtmlColorLabel7 = New LakeUI.HtmlColorLabel()
        ModernComboBox2 = New LakeUI.ModernComboBox()
        HtmlColorLabel2 = New LakeUI.HtmlColorLabel()
        Panel2 = New Panel()
        ModernComboBox1 = New LakeUI.ModernComboBox()
        HtmlColorLabel4 = New LakeUI.HtmlColorLabel()
        HtmlColorLabel1 = New LakeUI.HtmlColorLabel()
        ModernPanel1.SuspendLayout()
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
        ModernPanel1.Controls.Add(Panel6)
        ModernPanel1.Controls.Add(HtmlColorLabel3)
        ModernPanel1.Controls.Add(Panel5)
        ModernPanel1.Controls.Add(Panel4)
        ModernPanel1.Controls.Add(Panel3)
        ModernPanel1.Controls.Add(Panel1)
        ModernPanel1.Controls.Add(HtmlColorLabel2)
        ModernPanel1.Controls.Add(Panel2)
        ModernPanel1.Controls.Add(HtmlColorLabel4)
        ModernPanel1.Controls.Add(HtmlColorLabel1)
        ModernPanel1.Dock = DockStyle.Fill
        ModernPanel1.Location = New Point(0, 0)
        ModernPanel1.Name = "ModernPanel1"
        ModernPanel1.Padding = New Padding(20)
        ModernPanel1.ScrollBarMode = LakeUI.ModernPanel.ScrollMode.Vertical
        ModernPanel1.Size = New Size(696, 641)
        ModernPanel1.TabIndex = 0
        ' 
        ' Panel6
        ' 
        Panel6.Controls.Add(HtmlColorLabel5)
        Panel6.Controls.Add(ModernComboBox6)
        Panel6.Dock = DockStyle.Top
        Panel6.Location = New Point(20, 381)
        Panel6.Name = "Panel6"
        Panel6.Padding = New Padding(0, 10, 0, 0)
        Panel6.Size = New Size(656, 42)
        Panel6.TabIndex = 23
        ' 
        ' HtmlColorLabel5
        ' 
        HtmlColorLabel5.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel5.Dock = DockStyle.Fill
        HtmlColorLabel5.Location = New Point(200, 10)
        HtmlColorLabel5.Margin = New Padding(2)
        HtmlColorLabel5.Name = "HtmlColorLabel5"
        HtmlColorLabel5.Padding = New Padding(10, 0, 0, 0)
        HtmlColorLabel5.Size = New Size(456, 32)
        HtmlColorLabel5.TabIndex = 19
        HtmlColorLabel5.Text = "<span style=""font-size:10"">0 = 无限帧率</span>   <span style=""font-size:10pt; color:Gray"">CPU0 将对你宣战</span>"
        HtmlColorLabel5.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        ' 
        ' ModernComboBox6
        ' 
        ModernComboBox6.BackColor1 = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        ModernComboBox6.BorderColorFocus = Color.Silver
        ModernComboBox6.BorderRadius = 10
        ModernComboBox6.BorderSize = 0
        ModernComboBox6.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        ModernComboBox6.Dock = DockStyle.Left
        ModernComboBox6.DropDownBorderSize = 2
        ModernComboBox6.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        ModernComboBox6.DropDownPadding = New Padding(10)
        ModernComboBox6.DropDownScrollBarHoverColor = Color.FromArgb(CByte(200), CByte(200), CByte(200))
        ModernComboBox6.Items.Add("30")
        ModernComboBox6.Items.Add("60")
        ModernComboBox6.Items.Add("90")
        ModernComboBox6.Items.Add("120")
        ModernComboBox6.Items.Add("0")
        ModernComboBox6.Location = New Point(0, 10)
        ModernComboBox6.Margin = New Padding(2, 2, 2, 2)
        ModernComboBox6.Name = "ModernComboBox6"
        ModernComboBox6.Padding = New Padding(10, 0, 10, 0)
        ModernComboBox6.Size = New Size(200, 32)
        ModernComboBox6.TabIndex = 0
        ModernComboBox6.ToolTipBorderSize = 2
        ModernComboBox6.ToolTipGap = 10
        ModernComboBox6.ToolTipMaxWidth = 350
        ModernComboBox6.ToolTipPadding = New Padding(15)
        ' 
        ' HtmlColorLabel3
        ' 
        HtmlColorLabel3.AutoSize = True
        HtmlColorLabel3.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel3.Dock = DockStyle.Top
        HtmlColorLabel3.Location = New Point(20, 331)
        HtmlColorLabel3.Margin = New Padding(2)
        HtmlColorLabel3.Name = "HtmlColorLabel3"
        HtmlColorLabel3.Padding = New Padding(0, 20, 0, 5)
        HtmlColorLabel3.Size = New Size(656, 50)
        HtmlColorLabel3.TabIndex = 22
        HtmlColorLabel3.Text = "<span style=""font-size:13"">动画帧率</span>   <span style=""font-size:10pt; color:Gray"">在当前应用程序中此设置仅对特定控件生效</span>"
        ' 
        ' Panel5
        ' 
        Panel5.Controls.Add(HtmlColorLabel10)
        Panel5.Controls.Add(ModernComboBox5)
        Panel5.Dock = DockStyle.Top
        Panel5.Location = New Point(20, 289)
        Panel5.Name = "Panel5"
        Panel5.Padding = New Padding(0, 10, 0, 0)
        Panel5.Size = New Size(656, 42)
        Panel5.TabIndex = 21
        ' 
        ' HtmlColorLabel10
        ' 
        HtmlColorLabel10.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel10.Dock = DockStyle.Fill
        HtmlColorLabel10.Location = New Point(200, 10)
        HtmlColorLabel10.Margin = New Padding(2)
        HtmlColorLabel10.Name = "HtmlColorLabel10"
        HtmlColorLabel10.Padding = New Padding(10, 0, 0, 0)
        HtmlColorLabel10.Size = New Size(456, 32)
        HtmlColorLabel10.TabIndex = 18
        HtmlColorLabel10.Text = "<span style=""font-size:10"">合成质量</span>   <span style=""font-size:10pt; color:Gray"">SSAA 下采样质量</span>"
        HtmlColorLabel10.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        ' 
        ' ModernComboBox5
        ' 
        ModernComboBox5.BackColor1 = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        ModernComboBox5.BorderColorFocus = Color.Silver
        ModernComboBox5.BorderRadius = 10
        ModernComboBox5.BorderSize = 0
        ModernComboBox5.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        ModernComboBox5.Dock = DockStyle.Left
        ModernComboBox5.DropDownBorderSize = 2
        ModernComboBox5.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        ModernComboBox5.DropDownPadding = New Padding(10)
        ModernComboBox5.DropDownScrollBarHoverColor = Color.FromArgb(CByte(200), CByte(200), CByte(200))
        ModernComboBox5.Items.Add("假设线性")
        ModernComboBox5.Items.Add("伽马校正")
        ModernComboBox5.Items.Add("高质量")
        ModernComboBox5.Items.Add("低质量")
        ModernComboBox5.Location = New Point(0, 10)
        ModernComboBox5.Margin = New Padding(2, 2, 2, 2)
        ModernComboBox5.Name = "ModernComboBox5"
        ModernComboBox5.Padding = New Padding(10, 0, 10, 0)
        ModernComboBox5.Size = New Size(200, 32)
        ModernComboBox5.TabIndex = 0
        ModernComboBox5.ToolTipBorderSize = 2
        ModernComboBox5.ToolTipGap = 10
        ModernComboBox5.ToolTipMaxWidth = 350
        ModernComboBox5.ToolTipPadding = New Padding(15)
        ' 
        ' Panel4
        ' 
        Panel4.Controls.Add(HtmlColorLabel9)
        Panel4.Controls.Add(ModernComboBox4)
        Panel4.Dock = DockStyle.Top
        Panel4.Location = New Point(20, 247)
        Panel4.Name = "Panel4"
        Panel4.Padding = New Padding(0, 10, 0, 0)
        Panel4.Size = New Size(656, 42)
        Panel4.TabIndex = 19
        ' 
        ' HtmlColorLabel9
        ' 
        HtmlColorLabel9.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel9.Dock = DockStyle.Fill
        HtmlColorLabel9.Location = New Point(200, 10)
        HtmlColorLabel9.Margin = New Padding(2)
        HtmlColorLabel9.Name = "HtmlColorLabel9"
        HtmlColorLabel9.Padding = New Padding(10, 0, 0, 0)
        HtmlColorLabel9.Size = New Size(456, 32)
        HtmlColorLabel9.TabIndex = 18
        HtmlColorLabel9.Text = "<span style=""font-size:10"">插值模式</span>   <span style=""font-size:10pt; color:Gray"">图形的缩放效果</span>"
        HtmlColorLabel9.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        ' 
        ' ModernComboBox4
        ' 
        ModernComboBox4.BackColor1 = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        ModernComboBox4.BorderColorFocus = Color.Silver
        ModernComboBox4.BorderRadius = 10
        ModernComboBox4.BorderSize = 0
        ModernComboBox4.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        ModernComboBox4.Dock = DockStyle.Left
        ModernComboBox4.DropDownBorderSize = 2
        ModernComboBox4.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        ModernComboBox4.DropDownPadding = New Padding(10)
        ModernComboBox4.DropDownScrollBarHoverColor = Color.FromArgb(CByte(200), CByte(200), CByte(200))
        ModernComboBox4.Items.Add("高质量双三次插值")
        ModernComboBox4.Items.Add("高质量双线性插值")
        ModernComboBox4.Items.Add("最近邻")
        ModernComboBox4.Items.Add("双三次")
        ModernComboBox4.Items.Add("双线性")
        ModernComboBox4.Items.Add("高")
        ModernComboBox4.Items.Add("低")
        ModernComboBox4.Location = New Point(0, 10)
        ModernComboBox4.Margin = New Padding(2, 2, 2, 2)
        ModernComboBox4.Name = "ModernComboBox4"
        ModernComboBox4.Padding = New Padding(10, 0, 10, 0)
        ModernComboBox4.Size = New Size(200, 32)
        ModernComboBox4.TabIndex = 0
        ModernComboBox4.ToolTipBorderSize = 2
        ModernComboBox4.ToolTipGap = 10
        ModernComboBox4.ToolTipMaxWidth = 350
        ModernComboBox4.ToolTipPadding = New Padding(15)
        ' 
        ' Panel3
        ' 
        Panel3.Controls.Add(HtmlColorLabel8)
        Panel3.Controls.Add(ModernComboBox3)
        Panel3.Dock = DockStyle.Top
        Panel3.Location = New Point(20, 205)
        Panel3.Name = "Panel3"
        Panel3.Padding = New Padding(0, 10, 0, 0)
        Panel3.Size = New Size(656, 42)
        Panel3.TabIndex = 17
        ' 
        ' HtmlColorLabel8
        ' 
        HtmlColorLabel8.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel8.Dock = DockStyle.Fill
        HtmlColorLabel8.Location = New Point(200, 10)
        HtmlColorLabel8.Margin = New Padding(2)
        HtmlColorLabel8.Name = "HtmlColorLabel8"
        HtmlColorLabel8.Padding = New Padding(10, 0, 0, 0)
        HtmlColorLabel8.Size = New Size(456, 32)
        HtmlColorLabel8.TabIndex = 18
        HtmlColorLabel8.Text = "<span style=""font-size:10"">像素偏移模式</span>   <span style=""font-size:10pt; color:Gray"">原生绘制质量</span>"
        HtmlColorLabel8.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
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
        ModernComboBox3.Items.Add("高质量")
        ModernComboBox3.Items.Add("半偏移")
        ModernComboBox3.Items.Add("低质量")
        ModernComboBox3.Location = New Point(0, 10)
        ModernComboBox3.Margin = New Padding(2, 2, 2, 2)
        ModernComboBox3.Name = "ModernComboBox3"
        ModernComboBox3.Padding = New Padding(10, 0, 10, 0)
        ModernComboBox3.Size = New Size(200, 32)
        ModernComboBox3.TabIndex = 0
        ModernComboBox3.ToolTipBorderSize = 2
        ModernComboBox3.ToolTipGap = 10
        ModernComboBox3.ToolTipMaxWidth = 350
        ModernComboBox3.ToolTipPadding = New Padding(15)
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(HtmlColorLabel7)
        Panel1.Controls.Add(ModernComboBox2)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(20, 163)
        Panel1.Name = "Panel1"
        Panel1.Padding = New Padding(0, 10, 0, 0)
        Panel1.Size = New Size(656, 42)
        Panel1.TabIndex = 15
        ' 
        ' HtmlColorLabel7
        ' 
        HtmlColorLabel7.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel7.Dock = DockStyle.Fill
        HtmlColorLabel7.Location = New Point(200, 10)
        HtmlColorLabel7.Margin = New Padding(2)
        HtmlColorLabel7.Name = "HtmlColorLabel7"
        HtmlColorLabel7.Padding = New Padding(10, 0, 0, 0)
        HtmlColorLabel7.Size = New Size(456, 32)
        HtmlColorLabel7.TabIndex = 17
        HtmlColorLabel7.Text = "<span style=""font-size:10"">平滑模式</span>   <span style=""font-size:10pt; color:Gray"">原生抗锯齿效果</span>"
        HtmlColorLabel7.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
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
        ModernComboBox2.Items.Add("抗锯齿")
        ModernComboBox2.Items.Add("最高质量")
        ModernComboBox2.Items.Add("最低质量")
        ModernComboBox2.Location = New Point(0, 10)
        ModernComboBox2.Margin = New Padding(2, 2, 2, 2)
        ModernComboBox2.Name = "ModernComboBox2"
        ModernComboBox2.Padding = New Padding(10, 0, 10, 0)
        ModernComboBox2.Size = New Size(200, 32)
        ModernComboBox2.TabIndex = 0
        ModernComboBox2.ToolTipBorderSize = 2
        ModernComboBox2.ToolTipGap = 10
        ModernComboBox2.ToolTipMaxWidth = 350
        ModernComboBox2.ToolTipPadding = New Padding(15)
        ' 
        ' HtmlColorLabel2
        ' 
        HtmlColorLabel2.AutoSize = True
        HtmlColorLabel2.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel2.Dock = DockStyle.Top
        HtmlColorLabel2.Location = New Point(20, 113)
        HtmlColorLabel2.Margin = New Padding(2)
        HtmlColorLabel2.Name = "HtmlColorLabel2"
        HtmlColorLabel2.Padding = New Padding(0, 20, 0, 5)
        HtmlColorLabel2.Size = New Size(656, 50)
        HtmlColorLabel2.TabIndex = 14
        HtmlColorLabel2.Text = "<span style=""font-size:13"">图形绘制质量</span>   <span style=""font-size:10pt; color:Gray"">LakeUI 默认采用最高质量绘制，如有调低需求可在此设置</span>"
        ' 
        ' Panel2
        ' 
        Panel2.Controls.Add(ModernComboBox1)
        Panel2.Dock = DockStyle.Top
        Panel2.Location = New Point(20, 71)
        Panel2.Name = "Panel2"
        Panel2.Padding = New Padding(0, 10, 0, 0)
        Panel2.Size = New Size(656, 42)
        Panel2.TabIndex = 13
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
        ModernComboBox1.Items.Add("保持控件独立设定")
        ModernComboBox1.Items.Add("x2")
        ModernComboBox1.Items.Add("x3")
        ModernComboBox1.Items.Add("x4")
        ModernComboBox1.Location = New Point(0, 10)
        ModernComboBox1.Margin = New Padding(2, 2, 2, 2)
        ModernComboBox1.Name = "ModernComboBox1"
        ModernComboBox1.Padding = New Padding(10, 0, 10, 0)
        ModernComboBox1.Size = New Size(200, 32)
        ModernComboBox1.TabIndex = 0
        ModernComboBox1.ToolTipBorderSize = 2
        ModernComboBox1.ToolTipGap = 10
        ModernComboBox1.ToolTipMaxWidth = 350
        ModernComboBox1.ToolTipPadding = New Padding(15)
        ' 
        ' HtmlColorLabel4
        ' 
        HtmlColorLabel4.AutoSize = True
        HtmlColorLabel4.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel4.Dock = DockStyle.Top
        HtmlColorLabel4.ForeColor = Color.IndianRed
        HtmlColorLabel4.Location = New Point(20, 50)
        HtmlColorLabel4.Margin = New Padding(2)
        HtmlColorLabel4.Name = "HtmlColorLabel4"
        HtmlColorLabel4.Size = New Size(656, 21)
        HtmlColorLabel4.TabIndex = 12
        HtmlColorLabel4.Text = "警告：严重性能消耗！高 DPI 模式下更是指数级增加！"
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
        HtmlColorLabel1.Size = New Size(656, 30)
        HtmlColorLabel1.TabIndex = 8
        HtmlColorLabel1.Text = "<span style=""font-size:13"">SSAA 超采样抗锯齿</span>   <span style=""font-size:10pt; color:Gray"">覆盖所有预先设定，在下一次重绘时生效，下同</span>"
        ' 
        ' Form_v6_设置_LakeUI性能选项
        ' 
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ClientSize = New Size(696, 641)
        Controls.Add(ModernPanel1)
        Font = New Font("Microsoft YaHei UI", 10F)
        ForeColor = Color.Silver
        Name = "Form_v6_设置_LakeUI性能选项"
        Text = "Form_v6_设置_LakeUI性能选项"
        ModernPanel1.ResumeLayout(False)
        ModernPanel1.PerformLayout()
        Panel6.ResumeLayout(False)
        Panel5.ResumeLayout(False)
        Panel4.ResumeLayout(False)
        Panel3.ResumeLayout(False)
        Panel1.ResumeLayout(False)
        Panel2.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents ModernPanel1 As LakeUI.ModernPanel
    Friend WithEvents HtmlColorLabel1 As LakeUI.HtmlColorLabel
    Friend WithEvents HtmlColorLabel4 As LakeUI.HtmlColorLabel
    Friend WithEvents HtmlColorLabel2 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents ModernComboBox1 As LakeUI.ModernComboBox
    Friend WithEvents Panel5 As Panel
    Friend WithEvents ModernComboBox5 As LakeUI.ModernComboBox
    Friend WithEvents Panel4 As Panel
    Friend WithEvents ModernComboBox4 As LakeUI.ModernComboBox
    Friend WithEvents Panel3 As Panel
    Friend WithEvents ModernComboBox3 As LakeUI.ModernComboBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ModernComboBox2 As LakeUI.ModernComboBox
    Friend WithEvents HtmlColorLabel10 As LakeUI.HtmlColorLabel
    Friend WithEvents HtmlColorLabel9 As LakeUI.HtmlColorLabel
    Friend WithEvents HtmlColorLabel8 As LakeUI.HtmlColorLabel
    Friend WithEvents HtmlColorLabel7 As LakeUI.HtmlColorLabel
    Friend WithEvents HtmlColorLabel3 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel6 As Panel
    Friend WithEvents HtmlColorLabel5 As LakeUI.HtmlColorLabel
    Friend WithEvents ModernComboBox6 As LakeUI.ModernComboBox
End Class
