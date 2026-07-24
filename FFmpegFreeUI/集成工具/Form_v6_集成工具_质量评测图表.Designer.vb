<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form_v6_集成工具_质量评测图表
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        ModernPanel1 = New LakeUI.ModernPanel()
        Panel2 = New Panel()
        Ultra2DChart1 = New LakeUI.Ultra2DChart()
        Panel1 = New Panel()
        HtmlColorLabel1 = New LakeUI.HtmlColorLabel()
        MCB_指标选择 = New LakeUI.ModernComboBox()
        ModernPanel1.SuspendLayout()
        Panel2.SuspendLayout()
        Panel1.SuspendLayout()
        SuspendLayout()
        ' 
        ' ModernPanel1
        ' 
        ModernPanel1.BackColor1 = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ModernPanel1.BorderSize = 0
        ModernPanel1.Controls.Add(Panel2)
        ModernPanel1.Controls.Add(Panel1)
        ModernPanel1.Dock = DockStyle.Fill
        ModernPanel1.Location = New Point(0, 0)
        ModernPanel1.Name = "ModernPanel1"
        ModernPanel1.Padding = New Padding(20)
        ModernPanel1.Size = New Size(960, 560)
        ModernPanel1.TabIndex = 0
        ' 
        ' Panel2
        ' 
        Panel2.BackColor = Color.Transparent
        Panel2.Controls.Add(Ultra2DChart1)
        Panel2.Dock = DockStyle.Fill
        Panel2.Location = New Point(20, 62)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(920, 478)
        Panel2.TabIndex = 1
        ' 
        ' Ultra2DChart1
        ' 
        Ultra2DChart1.BackColor = Color.Transparent
        Ultra2DChart1.BackgroundSource = ModernPanel1
        Ultra2DChart1.Dock = DockStyle.Fill
        Ultra2DChart1.Font = New Font("Microsoft YaHei UI", 10F)
        Ultra2DChart1.GridLineColor = Color.FromArgb(CByte(42), CByte(42), CByte(42))
        Ultra2DChart1.Location = New Point(0, 0)
        Ultra2DChart1.Name = "Ultra2DChart1"
        Ultra2DChart1.Padding = New Padding(18, 16, 18, 12)
        Ultra2DChart1.Palette.AddRange(New Color() {Color.FromArgb(CByte(68), CByte(114), CByte(196)), Color.FromArgb(CByte(237), CByte(125), CByte(49)), Color.FromArgb(CByte(165), CByte(165), CByte(165)), Color.FromArgb(CByte(255), CByte(192), CByte(0)), Color.FromArgb(CByte(91), CByte(155), CByte(213)), Color.FromArgb(CByte(112), CByte(173), CByte(71)), Color.FromArgb(CByte(38), CByte(68), CByte(120)), Color.FromArgb(CByte(158), CByte(72), CByte(14)), Color.FromArgb(CByte(68), CByte(114), CByte(196)), Color.FromArgb(CByte(237), CByte(125), CByte(49)), Color.FromArgb(CByte(165), CByte(165), CByte(165)), Color.FromArgb(CByte(255), CByte(192), CByte(0)), Color.FromArgb(CByte(91), CByte(155), CByte(213)), Color.FromArgb(CByte(112), CByte(173), CByte(71)), Color.FromArgb(CByte(38), CByte(68), CByte(120)), Color.FromArgb(CByte(158), CByte(72), CByte(14)), Color.FromArgb(CByte(68), CByte(114), CByte(196)), Color.FromArgb(CByte(237), CByte(125), CByte(49)), Color.FromArgb(CByte(165), CByte(165), CByte(165)), Color.FromArgb(CByte(255), CByte(192), CByte(0)), Color.FromArgb(CByte(91), CByte(155), CByte(213)), Color.FromArgb(CByte(112), CByte(173), CByte(71)), Color.FromArgb(CByte(38), CByte(68), CByte(120)), Color.FromArgb(CByte(158), CByte(72), CByte(14))})
        Ultra2DChart1.PlotBackColor = Color.FromArgb(CByte(80), CByte(0), CByte(0), CByte(0))
        Ultra2DChart1.PlotBorderColor = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        Ultra2DChart1.ShowLineHoverCrosshair = True
        Ultra2DChart1.Size = New Size(920, 478)
        Ultra2DChart1.TabIndex = 0
        Ultra2DChart1.ValueLabelFormat = "0.###"
        Ultra2DChart1.YAxisLabelFormat = "0.###"
        ' 
        ' Panel1
        ' 
        Panel1.BackColor = Color.Transparent
        Panel1.Controls.Add(HtmlColorLabel1)
        Panel1.Controls.Add(MCB_指标选择)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(20, 20)
        Panel1.Name = "Panel1"
        Panel1.Padding = New Padding(0, 0, 0, 10)
        Panel1.Size = New Size(920, 42)
        Panel1.TabIndex = 0
        ' 
        ' HtmlColorLabel1
        ' 
        HtmlColorLabel1.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel1.Dock = DockStyle.Fill
        HtmlColorLabel1.ForeColor = Color.FromArgb(CByte(180), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel1.Location = New Point(150, 0)
        HtmlColorLabel1.Margin = New Padding(2)
        HtmlColorLabel1.Name = "HtmlColorLabel1"
        HtmlColorLabel1.Padding = New Padding(16, 0, 0, 0)
        HtmlColorLabel1.Size = New Size(770, 32)
        HtmlColorLabel1.TabIndex = 1
        HtmlColorLabel1.Text = "暂无帧级数据"
        HtmlColorLabel1.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        ' 
        ' MCB_指标选择
        ' 
        MCB_指标选择.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_指标选择.BorderRadius = 10
        MCB_指标选择.BorderSize = 0
        MCB_指标选择.Dock = DockStyle.Left
        MCB_指标选择.DropDownBackdropBlurPasses = 2
        MCB_指标选择.DropDownBackdropBlurRadius = 30
        MCB_指标选择.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_指标选择.DropDownHoverColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        MCB_指标选择.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_指标选择.DropDownPadding = New Padding(10)
        MCB_指标选择.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_指标选择.DropDownSelectedForeColor = Color.White
        MCB_指标选择.Items.Add("PSNR")
        MCB_指标选择.Items.Add("SSIM")
        MCB_指标选择.Items.Add("VMAF")
        MCB_指标选择.Items.Add("XPSNR")
        MCB_指标选择.Location = New Point(0, 0)
        MCB_指标选择.Margin = New Padding(2, 2, 2, 2)
        MCB_指标选择.Name = "MCB_指标选择"
        MCB_指标选择.Padding = New Padding(10, 0, 10, 0)
        MCB_指标选择.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_指标选择.Size = New Size(150, 32)
        MCB_指标选择.TabIndex = 0
        MCB_指标选择.Text = "PSNR"
        MCB_指标选择.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' Form_v6_集成工具_质量评测图表
        ' 
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ClientSize = New Size(960, 560)
        Controls.Add(ModernPanel1)
        Font = New Font("Microsoft YaHei UI", 10F)
        ForeColor = Color.Silver
        MinimumSize = New Size(720, 420)
        Name = "Form_v6_集成工具_质量评测图表"
        StartPosition = FormStartPosition.Manual
        Text = "质量评测图表"
        ModernPanel1.ResumeLayout(False)
        Panel2.ResumeLayout(False)
        Panel1.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents ModernPanel1 As LakeUI.ModernPanel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents MCB_指标选择 As LakeUI.ModernComboBox
    Friend WithEvents HtmlColorLabel1 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Ultra2DChart1 As LakeUI.Ultra2DChart
End Class
