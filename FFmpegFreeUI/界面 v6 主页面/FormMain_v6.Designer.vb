<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormMain_v6
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
        components = New ComponentModel.Container()
        Dim ModernTabPage1 As LakeUI.ModernTabListControl.ModernTabPage = New LakeUI.ModernTabListControl.ModernTabPage()
        Dim ModernTabPage2 As LakeUI.ModernTabListControl.ModernTabPage = New LakeUI.ModernTabListControl.ModernTabPage()
        Dim ModernTabPage3 As LakeUI.ModernTabListControl.ModernTabPage = New LakeUI.ModernTabListControl.ModernTabPage()
        Dim ModernTabPage4 As LakeUI.ModernTabListControl.ModernTabPage = New LakeUI.ModernTabListControl.ModernTabPage()
        Dim ModernTabPage5 As LakeUI.ModernTabListControl.ModernTabPage = New LakeUI.ModernTabListControl.ModernTabPage()
        Dim ModernTabPage6 As LakeUI.ModernTabListControl.ModernTabPage = New LakeUI.ModernTabListControl.ModernTabPage()
        Dim ModernTabPage7 As LakeUI.ModernTabListControl.ModernTabPage = New LakeUI.ModernTabListControl.ModernTabPage()
        Dim ModernTabPage8 As LakeUI.ModernTabListControl.ModernTabPage = New LakeUI.ModernTabListControl.ModernTabPage()
        Dim ModernTabPage9 As LakeUI.ModernTabListControl.ModernTabPage = New LakeUI.ModernTabListControl.ModernTabPage()
        Dim ModernTabPage10 As LakeUI.ModernTabListControl.ModernTabPage = New LakeUI.ModernTabListControl.ModernTabPage()
        Dim ModernTabPage11 As LakeUI.ModernTabListControl.ModernTabPage = New LakeUI.ModernTabListControl.ModernTabPage()
        Dim ModernTabPage12 As LakeUI.ModernTabListControl.ModernTabPage = New LakeUI.ModernTabListControl.ModernTabPage()
        Dim ModernTabPage13 As LakeUI.ModernTabListControl.ModernTabPage = New LakeUI.ModernTabListControl.ModernTabPage()
        Dim ModernTabPage14 As LakeUI.ModernTabListControl.ModernTabPage = New LakeUI.ModernTabListControl.ModernTabPage()
        Dim ModernTabPage15 As LakeUI.ModernTabListControl.ModernTabPage = New LakeUI.ModernTabListControl.ModernTabPage()
        Dim ModernTabPage16 As LakeUI.ModernTabListControl.ModernTabPage = New LakeUI.ModernTabListControl.ModernTabPage()
        Dim ModernTabPage17 As LakeUI.ModernTabListControl.ModernTabPage = New LakeUI.ModernTabListControl.ModernTabPage()
        ModernTabListControl1 = New LakeUI.ModernTabListControl()
        ModernTextBox1 = New LakeUI.ModernTextBox()
        ThisIsYourWindow1 = New LakeUI.ThisIsYourWindow(components)
        PrecisionTimer1 = New LakeUI.PrecisionTimer()
        ModernTabListControl1.SuspendLayout()
        SuspendLayout()
        ' 
        ' ModernTabListControl1
        ' 
        ModernTabListControl1.AnimationDuration = 0
        ModernTabListControl1.BackColor = Color.Transparent
        ModernTabListControl1.Controls.Add(ModernTextBox1)
        ModernTabListControl1.Dock = DockStyle.Fill
        ModernTabPage1.IsDescription = True
        ModernTabPage1.Text = "FFmpegFreeUI"
        ModernTabPage2.Text = "起始页面"
        ModernTabPage3.Text = "编码队列"
        ModernTabPage4.IsSeparator = True
        ModernTabPage5.Text = "准备文件"
        ModernTabPage6.Text = "参数面板"
        ModernTabPage7.Text = "Agent 智能体"
        ModernTabPage8.Text = "浏览社区"
        ModernTabPage9.IsSeparator = True
        ModernTabPage10.Text = "ffprobe 媒体信息"
        ModernTabPage11.Text = "ffplay 调试播放器"
        ModernTabPage12.Text = "性能监控"
        ModernTabPage13.Text = "集成工具"
        ModernTabPage14.IsSeparator = True
        ModernTabPage15.Text = "软件设置"
        ModernTabPage16.Text = "支持者"
        ModernTabPage17.IsSeparator = True
        ModernTabListControl1.Items.Add(ModernTabPage1)
        ModernTabListControl1.Items.Add(ModernTabPage2)
        ModernTabListControl1.Items.Add(ModernTabPage3)
        ModernTabListControl1.Items.Add(ModernTabPage4)
        ModernTabListControl1.Items.Add(ModernTabPage5)
        ModernTabListControl1.Items.Add(ModernTabPage6)
        ModernTabListControl1.Items.Add(ModernTabPage7)
        ModernTabListControl1.Items.Add(ModernTabPage8)
        ModernTabListControl1.Items.Add(ModernTabPage9)
        ModernTabListControl1.Items.Add(ModernTabPage10)
        ModernTabListControl1.Items.Add(ModernTabPage11)
        ModernTabListControl1.Items.Add(ModernTabPage12)
        ModernTabListControl1.Items.Add(ModernTabPage13)
        ModernTabListControl1.Items.Add(ModernTabPage14)
        ModernTabListControl1.Items.Add(ModernTabPage15)
        ModernTabListControl1.Items.Add(ModernTabPage16)
        ModernTabListControl1.Items.Add(ModernTabPage17)
        ModernTabListControl1.Location = New Point(0, 0)
        ModernTabListControl1.Name = "ModernTabListControl1"
        ModernTabListControl1.ScrollBarThumbColor = Color.FromArgb(CByte(40), CByte(200), CByte(200), CByte(200))
        ModernTabListControl1.ScrollBarThumbHoverColor = Color.FromArgb(CByte(80), CByte(200), CByte(200), CByte(200))
        ModernTabListControl1.ScrollBarTrackColor = Color.FromArgb(CByte(20), CByte(200), CByte(200), CByte(200))
        ModernTabListControl1.ScrollBarWidth = 8
        ModernTabListControl1.SearchBoxControl = ModernTextBox1
        ModernTabListControl1.SeparatorColor = Color.FromArgb(CByte(80), CByte(200), CByte(200), CByte(200))
        ModernTabListControl1.Size = New Size(1184, 661)
        ModernTabListControl1.TabIndex = 0
        ModernTabListControl1.TabItemHeight = 32
        ModernTabListControl1.TabItemHoverBackColor = Color.FromArgb(CByte(40), CByte(200), CByte(200), CByte(200))
        ModernTabListControl1.TabItemSelectedBackColor = Color.FromArgb(CByte(80), CByte(200), CByte(200), CByte(200))
        ModernTabListControl1.TabItemSpacing = 0
        ModernTabListControl1.TabStripWidth = 200
        ' 
        ' ModernTextBox1
        ' 
        ModernTextBox1.BackColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        ModernTextBox1.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernTextBox1.BorderColor = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        ModernTextBox1.BorderColorFocus = Color.FromArgb(CByte(120), CByte(220), CByte(220), CByte(220))
        ModernTextBox1.BorderRadius = 5
        ModernTextBox1.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        ModernTextBox1.Location = New Point(10, 10)
        ModernTextBox1.Margin = New Padding(2)
        ModernTextBox1.Name = "ModernTextBox1"
        ModernTextBox1.Padding = New Padding(10, 0, 10, 0)
        ModernTextBox1.Size = New Size(180, 30)
        ModernTextBox1.TabIndex = 13
        ModernTextBox1.TabStop = False
        ModernTextBox1.WaterText = "搜索选项卡标题"
        ModernTextBox1.WaterTextForeColor = Color.DarkGray
        ' 
        ' ThisIsYourWindow1
        ' 
        ThisIsYourWindow1.BackdropBlurPasses = 1
        ThisIsYourWindow1.BackdropBlurRadius = 1
        ThisIsYourWindow1.BackdropFirstWindowOnly = True
        ThisIsYourWindow1.BackdropNoiseScale = 0.5F
        ThisIsYourWindow1.BackdropTintColor = Color.FromArgb(CByte(160), CByte(0), CByte(0), CByte(0))
        ThisIsYourWindow1.BackdropTintInactiveColor = Color.FromArgb(CByte(160), CByte(0), CByte(0), CByte(0))
        ThisIsYourWindow1.BorderColor = Color.Gray
        ThisIsYourWindow1.BorderInactiveColor = Color.Gray
        ThisIsYourWindow1.ButtonCornerRadius = 6
        ThisIsYourWindow1.ButtonGlyphLineWidth = 2F
        ThisIsYourWindow1.ButtonGlyphSize = 12
        ThisIsYourWindow1.ButtonPadding = New Padding(0, 6, 6, 6)
        ThisIsYourWindow1.CaptionBackColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        ThisIsYourWindow1.CaptionButtonGlyphColor = Color.FromArgb(CByte(200), CByte(200), CByte(200))
        ThisIsYourWindow1.CaptionButtonHoverBackColor = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        ThisIsYourWindow1.CaptionButtonPressedBackColor = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        ThisIsYourWindow1.CaptionHeight = 50
        ThisIsYourWindow1.CaptionInactiveBackColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        ThisIsYourWindow1.CloseButtonGlyphColor = Color.FromArgb(CByte(200), CByte(200), CByte(200))
        ThisIsYourWindow1.IconPaddingLeft = 10
        ThisIsYourWindow1.IconSize = 30
        ThisIsYourWindow1.LayerShadowColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ThisIsYourWindow1.LayerShadowResizeFullArea = True
        ThisIsYourWindow1.ShadowMode = LakeUI.ThisIsYourWindow.ShadowModeEnum.Layer
        ThisIsYourWindow1.TitleAlign = LakeUI.ThisIsYourWindow.TitleAlignEnum.Center
        ThisIsYourWindow1.TitleForeColor = Color.Silver
        ThisIsYourWindow1.TitleInactiveForeColor = Color.DarkGray
        ' 
        ' PrecisionTimer1
        ' 
        PrecisionTimer1.DispatchMode = LakeUI.PrecisionTimer.DispatchModeEnum.NonBlocking
        PrecisionTimer1.Interval = 1000
        PrecisionTimer1.SynchronizingObject = Me
        ' 
        ' FormMain_v6
        ' 
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ClientSize = New Size(1184, 661)
        Controls.Add(ModernTabListControl1)
        DoubleBuffered = True
        Font = New Font("Microsoft YaHei UI", 10F)
        MinimumSize = New Size(1200, 700)
        Name = "FormMain_v6"
        StartPosition = FormStartPosition.CenterScreen
        Text = "FFmpegFreeUI"
        ModernTabListControl1.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents ModernTabListControl1 As LakeUI.ModernTabListControl
    Friend WithEvents ThisIsYourWindow1 As LakeUI.ThisIsYourWindow
    Friend WithEvents ModernTextBox1 As LakeUI.ModernTextBox
    Friend WithEvents PrecisionTimer1 As LakeUI.PrecisionTimer
End Class
