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
        Dim ModernTabPage18 As LakeUI.ModernTabListControl.ModernTabPage = New LakeUI.ModernTabListControl.ModernTabPage()
        Dim ModernTabPage19 As LakeUI.ModernTabListControl.ModernTabPage = New LakeUI.ModernTabListControl.ModernTabPage()
        Dim ModernTabPage20 As LakeUI.ModernTabListControl.ModernTabPage = New LakeUI.ModernTabListControl.ModernTabPage()
        Dim ModernTabPage21 As LakeUI.ModernTabListControl.ModernTabPage = New LakeUI.ModernTabListControl.ModernTabPage()
        Dim ModernTabPage22 As LakeUI.ModernTabListControl.ModernTabPage = New LakeUI.ModernTabListControl.ModernTabPage()
        Dim ModernTabPage23 As LakeUI.ModernTabListControl.ModernTabPage = New LakeUI.ModernTabListControl.ModernTabPage()
        Dim ModernTabPage24 As LakeUI.ModernTabListControl.ModernTabPage = New LakeUI.ModernTabListControl.ModernTabPage()
        Dim ModernTabPage25 As LakeUI.ModernTabListControl.ModernTabPage = New LakeUI.ModernTabListControl.ModernTabPage()
        Dim ModernTabPage26 As LakeUI.ModernTabListControl.ModernTabPage = New LakeUI.ModernTabListControl.ModernTabPage()
        Dim ModernTabPage27 As LakeUI.ModernTabListControl.ModernTabPage = New LakeUI.ModernTabListControl.ModernTabPage()
        Dim ModernTabPage28 As LakeUI.ModernTabListControl.ModernTabPage = New LakeUI.ModernTabListControl.ModernTabPage()
        Dim ModernTabPage29 As LakeUI.ModernTabListControl.ModernTabPage = New LakeUI.ModernTabListControl.ModernTabPage()
        Dim ModernTabPage30 As LakeUI.ModernTabListControl.ModernTabPage = New LakeUI.ModernTabListControl.ModernTabPage()
        Dim ModernTabPage31 As LakeUI.ModernTabListControl.ModernTabPage = New LakeUI.ModernTabListControl.ModernTabPage()
        Dim ModernTabPage32 As LakeUI.ModernTabListControl.ModernTabPage = New LakeUI.ModernTabListControl.ModernTabPage()
        Dim ModernTabPage33 As LakeUI.ModernTabListControl.ModernTabPage = New LakeUI.ModernTabListControl.ModernTabPage()
        Dim ModernTabPage34 As LakeUI.ModernTabListControl.ModernTabPage = New LakeUI.ModernTabListControl.ModernTabPage()
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
        ModernTabPage18.IsDescription = True
        ModernTabPage18.Text = "FFmpegFreeUI"
        ModernTabPage19.Text = "起始页面"
        ModernTabPage20.Text = "编码队列"
        ModernTabPage21.IsSeparator = True
        ModernTabPage22.Text = "准备文件"
        ModernTabPage23.Text = "参数面板"
        ModernTabPage24.Text = "Agent 智能体"
        ModernTabPage25.Text = "浏览社区"
        ModernTabPage26.IsSeparator = True
        ModernTabPage27.Text = "ffprobe 媒体信息"
        ModernTabPage28.Text = "ffplay 调试播放器"
        ModernTabPage29.Text = "性能监控"
        ModernTabPage30.Text = "集成的工具"
        ModernTabPage31.IsSeparator = True
        ModernTabPage32.Text = "软件设置"
        ModernTabPage33.Text = "支持者"
        ModernTabPage34.IsSeparator = True
        ModernTabListControl1.Items.Add(ModernTabPage18)
        ModernTabListControl1.Items.Add(ModernTabPage19)
        ModernTabListControl1.Items.Add(ModernTabPage20)
        ModernTabListControl1.Items.Add(ModernTabPage21)
        ModernTabListControl1.Items.Add(ModernTabPage22)
        ModernTabListControl1.Items.Add(ModernTabPage23)
        ModernTabListControl1.Items.Add(ModernTabPage24)
        ModernTabListControl1.Items.Add(ModernTabPage25)
        ModernTabListControl1.Items.Add(ModernTabPage26)
        ModernTabListControl1.Items.Add(ModernTabPage27)
        ModernTabListControl1.Items.Add(ModernTabPage28)
        ModernTabListControl1.Items.Add(ModernTabPage29)
        ModernTabListControl1.Items.Add(ModernTabPage30)
        ModernTabListControl1.Items.Add(ModernTabPage31)
        ModernTabListControl1.Items.Add(ModernTabPage32)
        ModernTabListControl1.Items.Add(ModernTabPage33)
        ModernTabListControl1.Items.Add(ModernTabPage34)
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
