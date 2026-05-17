<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_v6_参数面板_画面裁剪窗口
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
        PixelPictureBox1 = New LakeUI.PixelPictureBox()
        JustEmptyControl5 = New LakeUI.JustEmptyControl()
        Panel2 = New Panel()
        ModernCheckBox4 = New LakeUI.ModernCheckBox()
        ModernComboBox1 = New LakeUI.ModernComboBox()
        JustEmptyControl4 = New LakeUI.JustEmptyControl()
        ModernTextBox2 = New LakeUI.ModernTextBox()
        JustEmptyControl3 = New LakeUI.JustEmptyControl()
        ModernTextBox1 = New LakeUI.ModernTextBox()
        JustEmptyControl2 = New LakeUI.JustEmptyControl()
        ModernButton2 = New LakeUI.ModernButton()
        JustEmptyControl1 = New LakeUI.JustEmptyControl()
        ModernButton1 = New LakeUI.ModernButton()
        HtmlColorLabel1 = New LakeUI.HtmlColorLabel()
        ModernPanel1.SuspendLayout()
        Panel2.SuspendLayout()
        SuspendLayout()
        ' 
        ' ModernPanel1
        ' 
        ModernPanel1.BackColor1 = Color.Transparent
        ModernPanel1.BorderSize = 0
        ModernPanel1.Controls.Add(PixelPictureBox1)
        ModernPanel1.Controls.Add(JustEmptyControl5)
        ModernPanel1.Controls.Add(Panel2)
        ModernPanel1.Controls.Add(HtmlColorLabel1)
        ModernPanel1.Dock = DockStyle.Fill
        ModernPanel1.Location = New Point(0, 0)
        ModernPanel1.Name = "ModernPanel1"
        ModernPanel1.Padding = New Padding(20)
        ModernPanel1.Size = New Size(772, 588)
        ModernPanel1.TabIndex = 18
        ' 
        ' PixelPictureBox1
        ' 
        PixelPictureBox1.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        PixelPictureBox1.BorderSize = 0
        PixelPictureBox1.Dock = DockStyle.Fill
        PixelPictureBox1.Location = New Point(20, 118)
        PixelPictureBox1.Name = "PixelPictureBox1"
        PixelPictureBox1.Size = New Size(732, 450)
        PixelPictureBox1.TabIndex = 19
        ' 
        ' JustEmptyControl5
        ' 
        JustEmptyControl5.Dock = DockStyle.Top
        JustEmptyControl5.Location = New Point(20, 103)
        JustEmptyControl5.Name = "JustEmptyControl5"
        JustEmptyControl5.Size = New Size(732, 15)
        JustEmptyControl5.TabIndex = 18
        ' 
        ' Panel2
        ' 
        Panel2.Controls.Add(ModernCheckBox4)
        Panel2.Controls.Add(ModernComboBox1)
        Panel2.Controls.Add(JustEmptyControl4)
        Panel2.Controls.Add(ModernTextBox2)
        Panel2.Controls.Add(JustEmptyControl3)
        Panel2.Controls.Add(ModernTextBox1)
        Panel2.Controls.Add(JustEmptyControl2)
        Panel2.Controls.Add(ModernButton2)
        Panel2.Controls.Add(JustEmptyControl1)
        Panel2.Controls.Add(ModernButton1)
        Panel2.Dock = DockStyle.Top
        Panel2.Location = New Point(20, 61)
        Panel2.Name = "Panel2"
        Panel2.Padding = New Padding(0, 10, 0, 0)
        Panel2.Size = New Size(732, 42)
        Panel2.TabIndex = 16
        ' 
        ' ModernCheckBox4
        ' 
        ModernCheckBox4.AutoSize = True
        ModernCheckBox4.BoxBorderRadius = 0
        ModernCheckBox4.BoxBorderSize = 2
        ModernCheckBox4.BoxSize = 20
        ModernCheckBox4.BoxTextSpacing = 10
        ModernCheckBox4.Dock = DockStyle.Left
        ModernCheckBox4.Location = New Point(602, 10)
        ModernCheckBox4.Name = "ModernCheckBox4"
        ModernCheckBox4.Padding = New Padding(10, 0, 0, 0)
        ModernCheckBox4.Size = New Size(70, 32)
        ModernCheckBox4.TabIndex = 15
        ModernCheckBox4.Text = "居中"
        ' 
        ' ModernComboBox1
        ' 
        ModernComboBox1.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernComboBox1.BorderColorFocus = Color.Silver
        ModernComboBox1.BorderRadius = 10
        ModernComboBox1.BorderSize = 0
        ModernComboBox1.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        ModernComboBox1.Dock = DockStyle.Left
        ModernComboBox1.DropDownBorderSize = 2
        ModernComboBox1.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        ModernComboBox1.DropDownPadding = New Padding(10)
        ModernComboBox1.DropDownScrollBarHoverColor = Color.FromArgb(CByte(200), CByte(200), CByte(200))
        ModernComboBox1.Items.Add("自由")
        ModernComboBox1.Items.Add("21:9")
        ModernComboBox1.Items.Add("16:9")
        ModernComboBox1.Items.Add("3:2")
        ModernComboBox1.Items.Add("2:1")
        ModernComboBox1.Items.Add("4:3")
        ModernComboBox1.Items.Add("1:1")
        ModernComboBox1.Location = New Point(502, 10)
        ModernComboBox1.Margin = New Padding(2, 2, 2, 2)
        ModernComboBox1.Name = "ModernComboBox1"
        ModernComboBox1.Padding = New Padding(10, 0, 10, 0)
        ModernComboBox1.Size = New Size(100, 32)
        ModernComboBox1.TabIndex = 12
        ModernComboBox1.ToolTipBorderSize = 2
        ModernComboBox1.ToolTipGap = 10
        ModernComboBox1.ToolTipMaxWidth = 350
        ModernComboBox1.ToolTipPadding = New Padding(15)
        ModernComboBox1.WaterText = "比例"
        ' 
        ' JustEmptyControl4
        ' 
        JustEmptyControl4.Dock = DockStyle.Left
        JustEmptyControl4.Location = New Point(491, 10)
        JustEmptyControl4.Name = "JustEmptyControl4"
        JustEmptyControl4.Size = New Size(11, 32)
        JustEmptyControl4.TabIndex = 13
        ' 
        ' ModernTextBox2
        ' 
        ModernTextBox2.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernTextBox2.BorderColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernTextBox2.BorderColorFocus = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        ModernTextBox2.BorderRadius = 10
        ModernTextBox2.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        ModernTextBox2.Dock = DockStyle.Left
        ModernTextBox2.Location = New Point(341, 10)
        ModernTextBox2.Margin = New Padding(2, 2, 2, 2)
        ModernTextBox2.Name = "ModernTextBox2"
        ModernTextBox2.Padding = New Padding(10, 0, 10, 0)
        ModernTextBox2.ScrollBarHoverColor = Color.FromArgb(CByte(200), CByte(200), CByte(200))
        ModernTextBox2.Size = New Size(150, 32)
        ModernTextBox2.TabIndex = 11
        ModernTextBox2.WaterText = "指定时间戳"
        ' 
        ' JustEmptyControl3
        ' 
        JustEmptyControl3.Dock = DockStyle.Left
        JustEmptyControl3.Location = New Point(330, 10)
        JustEmptyControl3.Name = "JustEmptyControl3"
        JustEmptyControl3.Size = New Size(11, 32)
        JustEmptyControl3.TabIndex = 10
        ' 
        ' ModernTextBox1
        ' 
        ModernTextBox1.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernTextBox1.BorderColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernTextBox1.BorderColorFocus = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        ModernTextBox1.BorderRadius = 10
        ModernTextBox1.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        ModernTextBox1.Dock = DockStyle.Left
        ModernTextBox1.Location = New Point(162, 10)
        ModernTextBox1.Margin = New Padding(2, 2, 2, 2)
        ModernTextBox1.Name = "ModernTextBox1"
        ModernTextBox1.Padding = New Padding(10, 0, 10, 0)
        ModernTextBox1.ScrollBarHoverColor = Color.FromArgb(CByte(200), CByte(200), CByte(200))
        ModernTextBox1.Size = New Size(168, 32)
        ModernTextBox1.TabIndex = 9
        ModernTextBox1.WaterText = "宽:高:左上X:左上Y"
        ' 
        ' JustEmptyControl2
        ' 
        JustEmptyControl2.Dock = DockStyle.Left
        JustEmptyControl2.Location = New Point(151, 10)
        JustEmptyControl2.Name = "JustEmptyControl2"
        JustEmptyControl2.Size = New Size(11, 32)
        JustEmptyControl2.TabIndex = 8
        ' 
        ' ModernButton2
        ' 
        ModernButton2.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernButton2.BorderRadius = 10
        ModernButton2.BorderSize = 0
        ModernButton2.Dock = DockStyle.Left
        ModernButton2.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        ModernButton2.Location = New Point(81, 10)
        ModernButton2.Margin = New Padding(2)
        ModernButton2.Name = "ModernButton2"
        ModernButton2.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        ModernButton2.Size = New Size(70, 32)
        ModernButton2.TabIndex = 7
        ModernButton2.Text = "完成"
        ' 
        ' JustEmptyControl1
        ' 
        JustEmptyControl1.Dock = DockStyle.Left
        JustEmptyControl1.Location = New Point(70, 10)
        JustEmptyControl1.Name = "JustEmptyControl1"
        JustEmptyControl1.Size = New Size(11, 32)
        JustEmptyControl1.TabIndex = 6
        ' 
        ' ModernButton1
        ' 
        ModernButton1.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernButton1.BorderRadius = 10
        ModernButton1.BorderSize = 0
        ModernButton1.Dock = DockStyle.Left
        ModernButton1.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        ModernButton1.Location = New Point(0, 10)
        ModernButton1.Margin = New Padding(2)
        ModernButton1.Name = "ModernButton1"
        ModernButton1.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        ModernButton1.Size = New Size(70, 32)
        ModernButton1.TabIndex = 1
        ModernButton1.Text = "打开"
        ' 
        ' HtmlColorLabel1
        ' 
        HtmlColorLabel1.AutoSize = True
        HtmlColorLabel1.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel1.Dock = DockStyle.Top
        HtmlColorLabel1.Location = New Point(20, 20)
        HtmlColorLabel1.Margin = New Padding(2)
        HtmlColorLabel1.Name = "HtmlColorLabel1"
        HtmlColorLabel1.Size = New Size(732, 41)
        HtmlColorLabel1.TabIndex = 15
        HtmlColorLabel1.Text = "鼠标左键移动视野，鼠标滚轮控制缩放，鼠标右键绘制新框选<br>默认取第 10 秒处截图，可以先自行设定再打开视频"
        ' 
        ' Form_v6_参数面板_画面裁剪窗口
        ' 
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ClientSize = New Size(772, 588)
        Controls.Add(ModernPanel1)
        Font = New Font("Microsoft YaHei UI", 10F)
        ForeColor = Color.Silver
        MaximizeBox = False
        MinimizeBox = False
        Name = "Form_v6_参数面板_画面裁剪窗口"
        ShowIcon = False
        ShowInTaskbar = False
        StartPosition = FormStartPosition.Manual
        Text = "画面裁剪"
        ModernPanel1.ResumeLayout(False)
        ModernPanel1.PerformLayout()
        Panel2.ResumeLayout(False)
        Panel2.PerformLayout()
        ResumeLayout(False)
    End Sub
    Friend WithEvents ModernPanel1 As LakeUI.ModernPanel
    Friend WithEvents PixelPictureBox1 As LakeUI.PixelPictureBox
    Friend WithEvents JustEmptyControl5 As LakeUI.JustEmptyControl
    Friend WithEvents Panel2 As Panel
    Friend WithEvents ModernCheckBox4 As LakeUI.ModernCheckBox
    Friend WithEvents ModernComboBox1 As LakeUI.ModernComboBox
    Friend WithEvents JustEmptyControl4 As LakeUI.JustEmptyControl
    Friend WithEvents ModernTextBox2 As LakeUI.ModernTextBox
    Friend WithEvents JustEmptyControl3 As LakeUI.JustEmptyControl
    Friend WithEvents ModernTextBox1 As LakeUI.ModernTextBox
    Friend WithEvents JustEmptyControl2 As LakeUI.JustEmptyControl
    Friend WithEvents ModernButton2 As LakeUI.ModernButton
    Friend WithEvents JustEmptyControl1 As LakeUI.JustEmptyControl
    Friend WithEvents ModernButton1 As LakeUI.ModernButton
    Friend WithEvents HtmlColorLabel1 As LakeUI.HtmlColorLabel
End Class
