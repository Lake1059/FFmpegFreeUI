<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_v6_参数面板_动态模糊
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
        Panel4 = New Panel()
        MTB_处理哪些颜色平面 = New LakeUI.ModernTextBox()
        HCL_处理哪些颜色平面 = New LakeUI.HtmlColorLabel()
        Panel3 = New Panel()
        MTB_输出缩放系数 = New LakeUI.ModernTextBox()
        HCL_输出缩放系数 = New LakeUI.HtmlColorLabel()
        Panel1 = New Panel()
        MTB_每帧的权重 = New LakeUI.ModernTextBox()
        HCL_每帧的权重 = New LakeUI.HtmlColorLabel()
        Panel2 = New Panel()
        MTB_连续混合帧数 = New LakeUI.ModernTextBox()
        HCL_连续混合帧数 = New LakeUI.HtmlColorLabel()
        MCK_动态模糊总开关 = New LakeUI.ModernCheckBox()
        ModernPanel1.SuspendLayout()
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
        ModernPanel1.Controls.Add(Panel4)
        ModernPanel1.Controls.Add(HCL_处理哪些颜色平面)
        ModernPanel1.Controls.Add(Panel3)
        ModernPanel1.Controls.Add(HCL_输出缩放系数)
        ModernPanel1.Controls.Add(Panel1)
        ModernPanel1.Controls.Add(HCL_每帧的权重)
        ModernPanel1.Controls.Add(Panel2)
        ModernPanel1.Controls.Add(HCL_连续混合帧数)
        ModernPanel1.Controls.Add(MCK_动态模糊总开关)
        ModernPanel1.Dock = DockStyle.Fill
        ModernPanel1.Location = New Point(0, 0)
        ModernPanel1.Name = "ModernPanel1"
        ModernPanel1.Padding = New Padding(20)
        ModernPanel1.Size = New Size(584, 441)
        ModernPanel1.TabIndex = 0
        '
        ' Panel4
        '
        Panel4.Controls.Add(MTB_处理哪些颜色平面)
        Panel4.Dock = DockStyle.Top
        Panel4.Location = New Point(20, 370)
        Panel4.Name = "Panel4"
        Panel4.Padding = New Padding(0, 10, 0, 0)
        Panel4.Size = New Size(544, 42)
        Panel4.TabIndex = 34
        '
        ' MTB_处理哪些颜色平面
        '
        MTB_处理哪些颜色平面.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_处理哪些颜色平面.BorderColor = Color.Transparent
        MTB_处理哪些颜色平面.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MTB_处理哪些颜色平面.BorderRadius = 10
        MTB_处理哪些颜色平面.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        MTB_处理哪些颜色平面.Dock = DockStyle.Left
        MTB_处理哪些颜色平面.Location = New Point(0, 10)
        MTB_处理哪些颜色平面.Margin = New Padding(2)
        MTB_处理哪些颜色平面.Name = "MTB_处理哪些颜色平面"
        MTB_处理哪些颜色平面.Padding = New Padding(10, 0, 10, 0)
        MTB_处理哪些颜色平面.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_处理哪些颜色平面.Size = New Size(100, 32)
        MTB_处理哪些颜色平面.TabIndex = 6
        MTB_处理哪些颜色平面.WaterText = "planes="
        MTB_处理哪些颜色平面.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        '
        ' HCL_处理哪些颜色平面
        '
        HCL_处理哪些颜色平面.AutoSize = True
        HCL_处理哪些颜色平面.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_处理哪些颜色平面.Dock = DockStyle.Top
        HCL_处理哪些颜色平面.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_处理哪些颜色平面.Location = New Point(20, 325)
        HCL_处理哪些颜色平面.Margin = New Padding(2)
        HCL_处理哪些颜色平面.Name = "HCL_处理哪些颜色平面"
        HCL_处理哪些颜色平面.Padding = New Padding(0, 20, 0, 0)
        HCL_处理哪些颜色平面.Size = New Size(544, 45)
        HCL_处理哪些颜色平面.TabIndex = 33
        HCL_处理哪些颜色平面.Text = "<span style=""font-size:13; color:Silver"">处理哪些颜色平面</span>   0~15，默认 15 处理所有平面"
        HCL_处理哪些颜色平面.ToolTipText = "1 = 只处理第 0 平面，常见是 Y / R；" & vbLf & "2 = 只处理第 1 平面，常见是 U / G" & vbLf & "；4 = 只处理第 2 平面，常见是 V / B" & vbLf & "；8 = 只处理第 3 平面，常见是 A" & vbLf & "；15 = 1+2+4+8，处理全部平面"
        '
        ' Panel3
        '
        Panel3.Controls.Add(MTB_输出缩放系数)
        Panel3.Dock = DockStyle.Top
        Panel3.Location = New Point(20, 283)
        Panel3.Name = "Panel3"
        Panel3.Padding = New Padding(0, 10, 0, 0)
        Panel3.Size = New Size(544, 42)
        Panel3.TabIndex = 32
        '
        ' MTB_输出缩放系数
        '
        MTB_输出缩放系数.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_输出缩放系数.BorderColor = Color.Transparent
        MTB_输出缩放系数.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MTB_输出缩放系数.BorderRadius = 10
        MTB_输出缩放系数.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        MTB_输出缩放系数.Dock = DockStyle.Left
        MTB_输出缩放系数.Location = New Point(0, 10)
        MTB_输出缩放系数.Margin = New Padding(2)
        MTB_输出缩放系数.Name = "MTB_输出缩放系数"
        MTB_输出缩放系数.Padding = New Padding(10, 0, 10, 0)
        MTB_输出缩放系数.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_输出缩放系数.Size = New Size(100, 32)
        MTB_输出缩放系数.TabIndex = 6
        MTB_输出缩放系数.WaterText = "scale="
        MTB_输出缩放系数.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        '
        ' HCL_输出缩放系数
        '
        HCL_输出缩放系数.AutoSize = True
        HCL_输出缩放系数.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_输出缩放系数.Dock = DockStyle.Top
        HCL_输出缩放系数.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_输出缩放系数.Location = New Point(20, 238)
        HCL_输出缩放系数.Margin = New Padding(2)
        HCL_输出缩放系数.Name = "HCL_输出缩放系数"
        HCL_输出缩放系数.Padding = New Padding(0, 20, 0, 0)
        HCL_输出缩放系数.Size = New Size(544, 45)
        HCL_输出缩放系数.TabIndex = 31
        HCL_输出缩放系数.Text = "<span style=""font-size:13; color:Silver"">输出缩放系数</span>   0~32767，默认 0 自动按权重归一化"
        '
        ' Panel1
        '
        Panel1.Controls.Add(MTB_每帧的权重)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(20, 196)
        Panel1.Name = "Panel1"
        Panel1.Padding = New Padding(0, 10, 0, 0)
        Panel1.Size = New Size(544, 42)
        Panel1.TabIndex = 30
        '
        ' MTB_每帧的权重
        '
        MTB_每帧的权重.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_每帧的权重.BorderColor = Color.Transparent
        MTB_每帧的权重.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MTB_每帧的权重.BorderRadius = 10
        MTB_每帧的权重.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        MTB_每帧的权重.Dock = DockStyle.Left
        MTB_每帧的权重.Location = New Point(0, 10)
        MTB_每帧的权重.Margin = New Padding(2)
        MTB_每帧的权重.Name = "MTB_每帧的权重"
        MTB_每帧的权重.Padding = New Padding(10, 0, 10, 0)
        MTB_每帧的权重.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_每帧的权重.Size = New Size(300, 32)
        MTB_每帧的权重.TabIndex = 6
        MTB_每帧的权重.WaterText = "weights="
        MTB_每帧的权重.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        '
        ' HCL_每帧的权重
        '
        HCL_每帧的权重.AutoSize = True
        HCL_每帧的权重.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_每帧的权重.Dock = DockStyle.Top
        HCL_每帧的权重.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_每帧的权重.Location = New Point(20, 131)
        HCL_每帧的权重.Margin = New Padding(2)
        HCL_每帧的权重.Name = "HCL_每帧的权重"
        HCL_每帧的权重.Padding = New Padding(0, 20, 0, 0)
        HCL_每帧的权重.Size = New Size(544, 65)
        HCL_每帧的权重.TabIndex = 29
        HCL_每帧的权重.Text = "<span style=""font-size:13; color:Silver"">每帧的权重</span>   用空格分割每帧权重值<br>weights 数量可以少于 frames，不够的部分会重复最后一个权重"
        '
        ' Panel2
        '
        Panel2.Controls.Add(MTB_连续混合帧数)
        Panel2.Dock = DockStyle.Top
        Panel2.Location = New Point(20, 89)
        Panel2.Name = "Panel2"
        Panel2.Padding = New Padding(0, 10, 0, 0)
        Panel2.Size = New Size(544, 42)
        Panel2.TabIndex = 28
        '
        ' MTB_连续混合帧数
        '
        MTB_连续混合帧数.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_连续混合帧数.BorderColor = Color.Transparent
        MTB_连续混合帧数.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MTB_连续混合帧数.BorderRadius = 10
        MTB_连续混合帧数.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        MTB_连续混合帧数.Dock = DockStyle.Left
        MTB_连续混合帧数.Location = New Point(0, 10)
        MTB_连续混合帧数.Margin = New Padding(2)
        MTB_连续混合帧数.Name = "MTB_连续混合帧数"
        MTB_连续混合帧数.Padding = New Padding(10, 0, 10, 0)
        MTB_连续混合帧数.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_连续混合帧数.Size = New Size(100, 32)
        MTB_连续混合帧数.TabIndex = 6
        MTB_连续混合帧数.WaterText = "frames="
        MTB_连续混合帧数.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        '
        ' HCL_连续混合帧数
        '
        HCL_连续混合帧数.AutoSize = True
        HCL_连续混合帧数.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_连续混合帧数.Dock = DockStyle.Top
        HCL_连续混合帧数.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_连续混合帧数.Location = New Point(20, 44)
        HCL_连续混合帧数.Margin = New Padding(2)
        HCL_连续混合帧数.Name = "HCL_连续混合帧数"
        HCL_连续混合帧数.Padding = New Padding(0, 20, 0, 0)
        HCL_连续混合帧数.Size = New Size(544, 45)
        HCL_连续混合帧数.TabIndex = 27
        HCL_连续混合帧数.Text = "<span style=""font-size:13; color:Silver"">连续混合帧数</span>   整数，1~1024，默认 3"
        '
        ' MCK_动态模糊总开关
        '
        MCK_动态模糊总开关.AutoSize = True
        MCK_动态模糊总开关.BoxBorderRadius = 5
        MCK_动态模糊总开关.BoxBorderSize = 0
        MCK_动态模糊总开关.BoxCheckedBackColor = Color.OliveDrab
        MCK_动态模糊总开关.BoxInnerPadding = 6
        MCK_动态模糊总开关.BoxSize = 24
        MCK_动态模糊总开关.BoxTextSpacing = 10
        MCK_动态模糊总开关.BoxUncheckedBackColor = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCK_动态模糊总开关.CheckMarkWidth = 3F
        MCK_动态模糊总开关.Dock = DockStyle.Top
        MCK_动态模糊总开关.Location = New Point(20, 20)
        MCK_动态模糊总开关.Name = "MCK_动态模糊总开关"
        MCK_动态模糊总开关.Size = New Size(544, 24)
        MCK_动态模糊总开关.TabIndex = 26
        MCK_动态模糊总开关.Text = "动态模糊总开关 / 勾选才会启用"
        '
        ' Form_v6_参数面板_动态模糊
        '
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ClientSize = New Size(584, 441)
        Controls.Add(ModernPanel1)
        Font = New Font("Microsoft YaHei UI", 10F)
        ForeColor = Color.Silver
        MaximizeBox = False
        MinimizeBox = False
        MinimumSize = New Size(600, 480)
        Name = "Form_v6_参数面板_动态模糊"
        ShowInTaskbar = False
        Text = "动态模糊"
        ModernPanel1.ResumeLayout(False)
        ModernPanel1.PerformLayout()
        Panel4.ResumeLayout(False)
        Panel3.ResumeLayout(False)
        Panel1.ResumeLayout(False)
        Panel2.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents ModernPanel1 As LakeUI.ModernPanel
    Friend WithEvents MCK_动态模糊总开关 As LakeUI.ModernCheckBox
    Friend WithEvents Panel2 As Panel
    Friend WithEvents MTB_连续混合帧数 As LakeUI.ModernTextBox
    Friend WithEvents HCL_连续混合帧数 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents MTB_处理哪些颜色平面 As LakeUI.ModernTextBox
    Friend WithEvents HCL_处理哪些颜色平面 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents MTB_输出缩放系数 As LakeUI.ModernTextBox
    Friend WithEvents HCL_输出缩放系数 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents MTB_每帧的权重 As LakeUI.ModernTextBox
    Friend WithEvents HCL_每帧的权重 As LakeUI.HtmlColorLabel
End Class
