<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_v6_参数面板_画面帧
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
        MB_烧录字幕 = New LakeUI.ModernButton()
        HCL_画面内容标题 = New LakeUI.HtmlColorLabel()
        Panel8 = New Panel()
        MB_平滑断层 = New LakeUI.ModernButton()
        JustEmptyControl10 = New LakeUI.JustEmptyControl()
        MB_画面翻转 = New LakeUI.ModernButton()
        JustEmptyControl11 = New LakeUI.JustEmptyControl()
        MB_扫描方式 = New LakeUI.ModernButton()
        Panel7 = New Panel()
        MB_胶片颗粒 = New LakeUI.ModernButton()
        JustEmptyControl8 = New LakeUI.JustEmptyControl()
        MB_传统锐化 = New LakeUI.ModernButton()
        JustEmptyControl9 = New LakeUI.JustEmptyControl()
        MB_传统降噪 = New LakeUI.ModernButton()
        Panel5 = New Panel()
        MB_着色器超分 = New LakeUI.ModernButton()
        JustEmptyControl5 = New LakeUI.JustEmptyControl()
        MB_动态模糊 = New LakeUI.ModernButton()
        JustEmptyControl4 = New LakeUI.JustEmptyControl()
        MB_简易插帧 = New LakeUI.ModernButton()
        HCL_画面增强标题 = New LakeUI.HtmlColorLabel()
        Panel3 = New Panel()
        MB_抽帧设置 = New LakeUI.ModernButton()
        JustEmptyControl3 = New LakeUI.JustEmptyControl()
        MCB_直接指定帧率 = New LakeUI.ModernComboBox()
        HCL_帧率设置标题 = New LakeUI.HtmlColorLabel()
        Panel4 = New Panel()
        LB_裁剪滤镜排序说明 = New Label()
        MTB_画面裁剪参数 = New LakeUI.ModernTextBox()
        JustEmptyControl1 = New LakeUI.JustEmptyControl()
        MB_画面裁剪交互 = New LakeUI.ModernButton()
        Panel1 = New Panel()
        LB_单独缩放说明 = New Label()
        MCB_指定缩放算法 = New LakeUI.ModernComboBox()
        JustEmptyControl6 = New LakeUI.JustEmptyControl()
        MCB_高度缩放 = New LakeUI.ModernComboBox()
        JustEmptyControl2 = New LakeUI.JustEmptyControl()
        MCB_宽度缩放 = New LakeUI.ModernComboBox()
        Panel2 = New Panel()
        LB_直接指定分辨率说明 = New Label()
        MCB_直接指定分辨率 = New LakeUI.ModernComboBox()
        HCL_分辨率设置标题 = New LakeUI.HtmlColorLabel()
        ModernPanel1.SuspendLayout()
        Panel6.SuspendLayout()
        Panel8.SuspendLayout()
        Panel7.SuspendLayout()
        Panel5.SuspendLayout()
        Panel3.SuspendLayout()
        Panel4.SuspendLayout()
        Panel1.SuspendLayout()
        Panel2.SuspendLayout()
        SuspendLayout()
        ' 
        ' ModernPanel1
        ' 
        ModernPanel1.BackColor1 = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ModernPanel1.BorderSize = 0
        ModernPanel1.Controls.Add(Panel6)
        ModernPanel1.Controls.Add(HCL_画面内容标题)
        ModernPanel1.Controls.Add(Panel8)
        ModernPanel1.Controls.Add(Panel7)
        ModernPanel1.Controls.Add(Panel5)
        ModernPanel1.Controls.Add(HCL_画面增强标题)
        ModernPanel1.Controls.Add(Panel3)
        ModernPanel1.Controls.Add(HCL_帧率设置标题)
        ModernPanel1.Controls.Add(Panel4)
        ModernPanel1.Controls.Add(Panel1)
        ModernPanel1.Controls.Add(Panel2)
        ModernPanel1.Controls.Add(HCL_分辨率设置标题)
        ModernPanel1.Dock = DockStyle.Fill
        ModernPanel1.Location = New Point(0, 0)
        ModernPanel1.Name = "ModernPanel1"
        ModernPanel1.Padding = New Padding(20)
        ModernPanel1.ScrollBarMode = LakeUI.ModernPanel.ScrollMode.Vertical
        ModernPanel1.Size = New Size(769, 618)
        ModernPanel1.TabIndex = 0
        ' 
        ' Panel6
        ' 
        Panel6.Controls.Add(MB_烧录字幕)
        Panel6.Dock = DockStyle.Top
        Panel6.Location = New Point(20, 494)
        Panel6.Name = "Panel6"
        Panel6.Padding = New Padding(0, 10, 0, 0)
        Panel6.Size = New Size(729, 42)
        Panel6.TabIndex = 14
        ' 
        ' MB_烧录字幕
        ' 
        MB_烧录字幕.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_烧录字幕.BorderRadius = 10
        MB_烧录字幕.BorderSize = 0
        MB_烧录字幕.Dock = DockStyle.Left
        MB_烧录字幕.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_烧录字幕.Location = New Point(0, 10)
        MB_烧录字幕.Margin = New Padding(2)
        MB_烧录字幕.Name = "MB_烧录字幕"
        MB_烧录字幕.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_烧录字幕.Size = New Size(150, 32)
        MB_烧录字幕.TabIndex = 11
        MB_烧录字幕.Text = "烧录字幕"
        ' 
        ' HCL_画面内容标题
        ' 
        HCL_画面内容标题.AutoSize = True
        HCL_画面内容标题.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_画面内容标题.Dock = DockStyle.Top
        HCL_画面内容标题.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_画面内容标题.Location = New Point(20, 444)
        HCL_画面内容标题.Margin = New Padding(2)
        HCL_画面内容标题.Name = "HCL_画面内容标题"
        HCL_画面内容标题.Padding = New Padding(0, 20, 0, 5)
        HCL_画面内容标题.Size = New Size(729, 50)
        HCL_画面内容标题.TabIndex = 13
        HCL_画面内容标题.Text = "<span style=""font-size:13; color:Silver"">内容</span>   专业需求请用剪辑和特效软件"
        ' 
        ' Panel8
        ' 
        Panel8.Controls.Add(MB_平滑断层)
        Panel8.Controls.Add(JustEmptyControl10)
        Panel8.Controls.Add(MB_画面翻转)
        Panel8.Controls.Add(JustEmptyControl11)
        Panel8.Controls.Add(MB_扫描方式)
        Panel8.Dock = DockStyle.Top
        Panel8.Location = New Point(20, 402)
        Panel8.Name = "Panel8"
        Panel8.Padding = New Padding(0, 10, 0, 0)
        Panel8.Size = New Size(729, 42)
        Panel8.TabIndex = 16
        ' 
        ' MB_平滑断层
        ' 
        MB_平滑断层.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_平滑断层.BorderRadius = 10
        MB_平滑断层.BorderSize = 0
        MB_平滑断层.Dock = DockStyle.Left
        MB_平滑断层.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_平滑断层.Location = New Point(320, 10)
        MB_平滑断层.Margin = New Padding(2)
        MB_平滑断层.Name = "MB_平滑断层"
        MB_平滑断层.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_平滑断层.Size = New Size(150, 32)
        MB_平滑断层.TabIndex = 13
        MB_平滑断层.Text = "平滑断层"
        ' 
        ' JustEmptyControl10
        ' 
        JustEmptyControl10.Dock = DockStyle.Left
        JustEmptyControl10.Location = New Point(310, 10)
        JustEmptyControl10.Name = "JustEmptyControl10"
        JustEmptyControl10.Size = New Size(10, 32)
        JustEmptyControl10.TabIndex = 12
        ' 
        ' MB_画面翻转
        ' 
        MB_画面翻转.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_画面翻转.BorderRadius = 10
        MB_画面翻转.BorderSize = 0
        MB_画面翻转.Dock = DockStyle.Left
        MB_画面翻转.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_画面翻转.Location = New Point(160, 10)
        MB_画面翻转.Margin = New Padding(2)
        MB_画面翻转.Name = "MB_画面翻转"
        MB_画面翻转.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_画面翻转.Size = New Size(150, 32)
        MB_画面翻转.TabIndex = 11
        MB_画面翻转.Text = "画面翻转"
        ' 
        ' JustEmptyControl11
        ' 
        JustEmptyControl11.Dock = DockStyle.Left
        JustEmptyControl11.Location = New Point(150, 10)
        JustEmptyControl11.Name = "JustEmptyControl11"
        JustEmptyControl11.Size = New Size(10, 32)
        JustEmptyControl11.TabIndex = 9
        ' 
        ' MB_扫描方式
        ' 
        MB_扫描方式.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_扫描方式.BorderRadius = 10
        MB_扫描方式.BorderSize = 0
        MB_扫描方式.Dock = DockStyle.Left
        MB_扫描方式.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_扫描方式.Location = New Point(0, 10)
        MB_扫描方式.Margin = New Padding(2)
        MB_扫描方式.Name = "MB_扫描方式"
        MB_扫描方式.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_扫描方式.Size = New Size(150, 32)
        MB_扫描方式.TabIndex = 10
        MB_扫描方式.Text = "扫描方式"
        ' 
        ' Panel7
        ' 
        Panel7.Controls.Add(MB_胶片颗粒)
        Panel7.Controls.Add(JustEmptyControl8)
        Panel7.Controls.Add(MB_传统锐化)
        Panel7.Controls.Add(JustEmptyControl9)
        Panel7.Controls.Add(MB_传统降噪)
        Panel7.Dock = DockStyle.Top
        Panel7.Location = New Point(20, 360)
        Panel7.Name = "Panel7"
        Panel7.Padding = New Padding(0, 10, 0, 0)
        Panel7.Size = New Size(729, 42)
        Panel7.TabIndex = 15
        ' 
        ' MB_胶片颗粒
        ' 
        MB_胶片颗粒.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_胶片颗粒.BorderRadius = 10
        MB_胶片颗粒.BorderSize = 0
        MB_胶片颗粒.Dock = DockStyle.Left
        MB_胶片颗粒.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_胶片颗粒.Location = New Point(320, 10)
        MB_胶片颗粒.Margin = New Padding(2)
        MB_胶片颗粒.Name = "MB_胶片颗粒"
        MB_胶片颗粒.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_胶片颗粒.Size = New Size(150, 32)
        MB_胶片颗粒.TabIndex = 13
        MB_胶片颗粒.Text = "胶片颗粒"
        ' 
        ' JustEmptyControl8
        ' 
        JustEmptyControl8.Dock = DockStyle.Left
        JustEmptyControl8.Location = New Point(310, 10)
        JustEmptyControl8.Name = "JustEmptyControl8"
        JustEmptyControl8.Size = New Size(10, 32)
        JustEmptyControl8.TabIndex = 12
        ' 
        ' MB_传统锐化
        ' 
        MB_传统锐化.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_传统锐化.BorderRadius = 10
        MB_传统锐化.BorderSize = 0
        MB_传统锐化.Dock = DockStyle.Left
        MB_传统锐化.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_传统锐化.Location = New Point(160, 10)
        MB_传统锐化.Margin = New Padding(2)
        MB_传统锐化.Name = "MB_传统锐化"
        MB_传统锐化.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_传统锐化.Size = New Size(150, 32)
        MB_传统锐化.TabIndex = 11
        MB_传统锐化.Text = "传统锐化"
        ' 
        ' JustEmptyControl9
        ' 
        JustEmptyControl9.Dock = DockStyle.Left
        JustEmptyControl9.Location = New Point(150, 10)
        JustEmptyControl9.Name = "JustEmptyControl9"
        JustEmptyControl9.Size = New Size(10, 32)
        JustEmptyControl9.TabIndex = 9
        ' 
        ' MB_传统降噪
        ' 
        MB_传统降噪.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_传统降噪.BorderRadius = 10
        MB_传统降噪.BorderSize = 0
        MB_传统降噪.Dock = DockStyle.Left
        MB_传统降噪.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_传统降噪.Location = New Point(0, 10)
        MB_传统降噪.Margin = New Padding(2)
        MB_传统降噪.Name = "MB_传统降噪"
        MB_传统降噪.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_传统降噪.Size = New Size(150, 32)
        MB_传统降噪.TabIndex = 10
        MB_传统降噪.Text = "传统降噪"
        ' 
        ' Panel5
        ' 
        Panel5.Controls.Add(MB_着色器超分)
        Panel5.Controls.Add(JustEmptyControl5)
        Panel5.Controls.Add(MB_动态模糊)
        Panel5.Controls.Add(JustEmptyControl4)
        Panel5.Controls.Add(MB_简易插帧)
        Panel5.Dock = DockStyle.Top
        Panel5.Location = New Point(20, 318)
        Panel5.Name = "Panel5"
        Panel5.Padding = New Padding(0, 10, 0, 0)
        Panel5.Size = New Size(729, 42)
        Panel5.TabIndex = 12
        ' 
        ' MB_着色器超分
        ' 
        MB_着色器超分.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_着色器超分.BorderRadius = 10
        MB_着色器超分.BorderSize = 0
        MB_着色器超分.Dock = DockStyle.Left
        MB_着色器超分.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_着色器超分.Location = New Point(320, 10)
        MB_着色器超分.Margin = New Padding(2)
        MB_着色器超分.Name = "MB_着色器超分"
        MB_着色器超分.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_着色器超分.Size = New Size(150, 32)
        MB_着色器超分.TabIndex = 13
        MB_着色器超分.Text = "着色器超分"
        ' 
        ' JustEmptyControl5
        ' 
        JustEmptyControl5.Dock = DockStyle.Left
        JustEmptyControl5.Location = New Point(310, 10)
        JustEmptyControl5.Name = "JustEmptyControl5"
        JustEmptyControl5.Size = New Size(10, 32)
        JustEmptyControl5.TabIndex = 12
        ' 
        ' MB_动态模糊
        ' 
        MB_动态模糊.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_动态模糊.BorderRadius = 10
        MB_动态模糊.BorderSize = 0
        MB_动态模糊.Dock = DockStyle.Left
        MB_动态模糊.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_动态模糊.Location = New Point(160, 10)
        MB_动态模糊.Margin = New Padding(2)
        MB_动态模糊.Name = "MB_动态模糊"
        MB_动态模糊.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_动态模糊.Size = New Size(150, 32)
        MB_动态模糊.TabIndex = 11
        MB_动态模糊.Text = "动态模糊"
        ' 
        ' JustEmptyControl4
        ' 
        JustEmptyControl4.Dock = DockStyle.Left
        JustEmptyControl4.Location = New Point(150, 10)
        JustEmptyControl4.Name = "JustEmptyControl4"
        JustEmptyControl4.Size = New Size(10, 32)
        JustEmptyControl4.TabIndex = 9
        ' 
        ' MB_简易插帧
        ' 
        MB_简易插帧.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_简易插帧.BorderRadius = 10
        MB_简易插帧.BorderSize = 0
        MB_简易插帧.Dock = DockStyle.Left
        MB_简易插帧.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_简易插帧.Location = New Point(0, 10)
        MB_简易插帧.Margin = New Padding(2)
        MB_简易插帧.Name = "MB_简易插帧"
        MB_简易插帧.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_简易插帧.Size = New Size(150, 32)
        MB_简易插帧.TabIndex = 10
        MB_简易插帧.Text = "简易插帧"
        ' 
        ' HCL_画面增强标题
        ' 
        HCL_画面增强标题.AutoSize = True
        HCL_画面增强标题.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_画面增强标题.Dock = DockStyle.Top
        HCL_画面增强标题.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_画面增强标题.Location = New Point(20, 268)
        HCL_画面增强标题.Margin = New Padding(2)
        HCL_画面增强标题.Name = "HCL_画面增强标题"
        HCL_画面增强标题.Padding = New Padding(0, 20, 0, 5)
        HCL_画面增强标题.Size = New Size(729, 50)
        HCL_画面增强标题.TabIndex = 11
        HCL_画面增强标题.Text = "<span style=""font-size:13; color:Silver"">增强</span>   专业需求请考虑行业软件或 AI 软件"
        ' 
        ' Panel3
        ' 
        Panel3.Controls.Add(MB_抽帧设置)
        Panel3.Controls.Add(JustEmptyControl3)
        Panel3.Controls.Add(MCB_直接指定帧率)
        Panel3.Dock = DockStyle.Top
        Panel3.Location = New Point(20, 226)
        Panel3.Name = "Panel3"
        Panel3.Padding = New Padding(0, 10, 0, 0)
        Panel3.Size = New Size(729, 42)
        Panel3.TabIndex = 10
        ' 
        ' MB_抽帧设置
        ' 
        MB_抽帧设置.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_抽帧设置.BorderRadius = 10
        MB_抽帧设置.BorderSize = 0
        MB_抽帧设置.Dock = DockStyle.Left
        MB_抽帧设置.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_抽帧设置.Location = New Point(160, 10)
        MB_抽帧设置.Margin = New Padding(2)
        MB_抽帧设置.Name = "MB_抽帧设置"
        MB_抽帧设置.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_抽帧设置.Size = New Size(150, 32)
        MB_抽帧设置.TabIndex = 10
        MB_抽帧设置.Text = "抽帧设置"
        ' 
        ' JustEmptyControl3
        ' 
        JustEmptyControl3.Dock = DockStyle.Left
        JustEmptyControl3.Location = New Point(150, 10)
        JustEmptyControl3.Name = "JustEmptyControl3"
        JustEmptyControl3.Size = New Size(10, 32)
        JustEmptyControl3.TabIndex = 9
        ' 
        ' MCB_直接指定帧率
        ' 
        MCB_直接指定帧率.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_直接指定帧率.BorderRadius = 10
        MCB_直接指定帧率.BorderSize = 0
        MCB_直接指定帧率.Dock = DockStyle.Left
        MCB_直接指定帧率.DropDownBackdropBlurPasses = 2
        MCB_直接指定帧率.DropDownBackdropBlurRadius = 30
        MCB_直接指定帧率.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_直接指定帧率.DropDownHoverColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        MCB_直接指定帧率.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_直接指定帧率.DropDownPadding = New Padding(10)
        MCB_直接指定帧率.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_直接指定帧率.DropDownSelectedForeColor = Color.White
        MCB_直接指定帧率.Editable = True
        MCB_直接指定帧率.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_直接指定帧率.Items.Add("")
        MCB_直接指定帧率.Items.Add("15")
        MCB_直接指定帧率.Items.Add("23.97")
        MCB_直接指定帧率.Items.Add("24")
        MCB_直接指定帧率.Items.Add("25")
        MCB_直接指定帧率.Items.Add("30")
        MCB_直接指定帧率.Items.Add("50")
        MCB_直接指定帧率.Items.Add("59.94")
        MCB_直接指定帧率.Items.Add("60")
        MCB_直接指定帧率.Items.Add("90")
        MCB_直接指定帧率.Items.Add("120")
        MCB_直接指定帧率.Location = New Point(0, 10)
        MCB_直接指定帧率.Margin = New Padding(2, 2, 2, 2)
        MCB_直接指定帧率.Name = "MCB_直接指定帧率"
        MCB_直接指定帧率.Padding = New Padding(10, 0, 10, 0)
        MCB_直接指定帧率.Size = New Size(150, 32)
        MCB_直接指定帧率.TabIndex = 0
        MCB_直接指定帧率.ToolTipGap = -1
        MCB_直接指定帧率.ToolTipMaxWidth = 350
        MCB_直接指定帧率.ToolTipPadding = New Padding(15)
        MCB_直接指定帧率.WaterText = "直接指定"
        MCB_直接指定帧率.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' HCL_帧率设置标题
        ' 
        HCL_帧率设置标题.AutoSize = True
        HCL_帧率设置标题.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_帧率设置标题.Dock = DockStyle.Top
        HCL_帧率设置标题.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_帧率设置标题.Location = New Point(20, 176)
        HCL_帧率设置标题.Margin = New Padding(2)
        HCL_帧率设置标题.Name = "HCL_帧率设置标题"
        HCL_帧率设置标题.Padding = New Padding(0, 20, 0, 5)
        HCL_帧率设置标题.Size = New Size(729, 50)
        HCL_帧率设置标题.TabIndex = 9
        HCL_帧率设置标题.Text = "<span style=""font-size:13; color:Silver"">帧率</span>   直接指定是静态帧率，抽帧可变为动态帧率"
        ' 
        ' Panel4
        ' 
        Panel4.Controls.Add(LB_裁剪滤镜排序说明)
        Panel4.Controls.Add(MTB_画面裁剪参数)
        Panel4.Controls.Add(JustEmptyControl1)
        Panel4.Controls.Add(MB_画面裁剪交互)
        Panel4.Dock = DockStyle.Top
        Panel4.Location = New Point(20, 134)
        Panel4.Name = "Panel4"
        Panel4.Padding = New Padding(0, 10, 0, 0)
        Panel4.Size = New Size(729, 42)
        Panel4.TabIndex = 8
        ' 
        ' LB_裁剪滤镜排序说明
        ' 
        LB_裁剪滤镜排序说明.Dock = DockStyle.Fill
        LB_裁剪滤镜排序说明.ForeColor = Color.MediumPurple
        LB_裁剪滤镜排序说明.Location = New Point(310, 10)
        LB_裁剪滤镜排序说明.Name = "LB_裁剪滤镜排序说明"
        LB_裁剪滤镜排序说明.Padding = New Padding(10, 0, 0, 0)
        LB_裁剪滤镜排序说明.Size = New Size(419, 32)
        LB_裁剪滤镜排序说明.TabIndex = 7
        LB_裁剪滤镜排序说明.Text = "默认将裁剪的滤镜排在缩放滤镜之前"
        LB_裁剪滤镜排序说明.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' MTB_画面裁剪参数
        ' 
        MTB_画面裁剪参数.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_画面裁剪参数.BorderColor = Color.Transparent
        MTB_画面裁剪参数.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MTB_画面裁剪参数.BorderRadius = 10
        MTB_画面裁剪参数.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        MTB_画面裁剪参数.Dock = DockStyle.Left
        MTB_画面裁剪参数.Location = New Point(160, 10)
        MTB_画面裁剪参数.Margin = New Padding(2)
        MTB_画面裁剪参数.Name = "MTB_画面裁剪参数"
        MTB_画面裁剪参数.Padding = New Padding(10, 0, 10, 0)
        MTB_画面裁剪参数.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_画面裁剪参数.Size = New Size(150, 32)
        MTB_画面裁剪参数.TabIndex = 5
        MTB_画面裁剪参数.WaterText = "crop"
        MTB_画面裁剪参数.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' JustEmptyControl1
        ' 
        JustEmptyControl1.Dock = DockStyle.Left
        JustEmptyControl1.Location = New Point(150, 10)
        JustEmptyControl1.Name = "JustEmptyControl1"
        JustEmptyControl1.Size = New Size(10, 32)
        JustEmptyControl1.TabIndex = 4
        ' 
        ' MB_画面裁剪交互
        ' 
        MB_画面裁剪交互.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_画面裁剪交互.BorderRadius = 10
        MB_画面裁剪交互.BorderSize = 0
        MB_画面裁剪交互.Dock = DockStyle.Left
        MB_画面裁剪交互.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_画面裁剪交互.Location = New Point(0, 10)
        MB_画面裁剪交互.Margin = New Padding(2)
        MB_画面裁剪交互.Name = "MB_画面裁剪交互"
        MB_画面裁剪交互.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_画面裁剪交互.Size = New Size(150, 32)
        MB_画面裁剪交互.TabIndex = 1
        MB_画面裁剪交互.Text = "画面裁剪交互"
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(LB_单独缩放说明)
        Panel1.Controls.Add(MCB_指定缩放算法)
        Panel1.Controls.Add(JustEmptyControl6)
        Panel1.Controls.Add(MCB_高度缩放)
        Panel1.Controls.Add(JustEmptyControl2)
        Panel1.Controls.Add(MCB_宽度缩放)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(20, 92)
        Panel1.Name = "Panel1"
        Panel1.Padding = New Padding(0, 10, 0, 0)
        Panel1.Size = New Size(729, 42)
        Panel1.TabIndex = 6
        ' 
        ' LB_单独缩放说明
        ' 
        LB_单独缩放说明.Dock = DockStyle.Fill
        LB_单独缩放说明.ForeColor = Color.OliveDrab
        LB_单独缩放说明.Location = New Point(470, 10)
        LB_单独缩放说明.Name = "LB_单独缩放说明"
        LB_单独缩放说明.Padding = New Padding(10, 0, 0, 0)
        LB_单独缩放说明.Size = New Size(259, 32)
        LB_单独缩放说明.TabIndex = 7
        LB_单独缩放说明.Text = "留空其一可以自动维持比例"
        LB_单独缩放说明.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' MCB_指定缩放算法
        ' 
        MCB_指定缩放算法.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_指定缩放算法.BorderRadius = 10
        MCB_指定缩放算法.BorderSize = 0
        MCB_指定缩放算法.Dock = DockStyle.Left
        MCB_指定缩放算法.DropDownBackdropBlurPasses = 2
        MCB_指定缩放算法.DropDownBackdropBlurRadius = 30
        MCB_指定缩放算法.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_指定缩放算法.DropDownHoverColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        MCB_指定缩放算法.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_指定缩放算法.DropDownPadding = New Padding(10)
        MCB_指定缩放算法.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_指定缩放算法.DropDownSelectedForeColor = Color.White
        MCB_指定缩放算法.Editable = True
        MCB_指定缩放算法.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_指定缩放算法.Items.Add("")
        MCB_指定缩放算法.Items.Add("lanczos")
        MCB_指定缩放算法.Items.Add("bilinear")
        MCB_指定缩放算法.Items.Add("fast_bilinear")
        MCB_指定缩放算法.Items.Add("bicubic")
        MCB_指定缩放算法.Items.Add("neighbor")
        MCB_指定缩放算法.Items.Add("area")
        MCB_指定缩放算法.Items.Add("bicublin")
        MCB_指定缩放算法.Items.Add("gauss")
        MCB_指定缩放算法.Items.Add("sinc")
        MCB_指定缩放算法.Items.Add("spline")
        MCB_指定缩放算法.Location = New Point(320, 10)
        MCB_指定缩放算法.Margin = New Padding(2, 2, 2, 2)
        MCB_指定缩放算法.Name = "MCB_指定缩放算法"
        MCB_指定缩放算法.Padding = New Padding(10, 0, 10, 0)
        MCB_指定缩放算法.Size = New Size(150, 32)
        MCB_指定缩放算法.TabIndex = 11
        MCB_指定缩放算法.ToolTipGap = -1
        MCB_指定缩放算法.ToolTipMaxWidth = 350
        MCB_指定缩放算法.ToolTipPadding = New Padding(15)
        MCB_指定缩放算法.WaterText = "指定缩放算法"
        MCB_指定缩放算法.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' JustEmptyControl6
        ' 
        JustEmptyControl6.Dock = DockStyle.Left
        JustEmptyControl6.Location = New Point(310, 10)
        JustEmptyControl6.Name = "JustEmptyControl6"
        JustEmptyControl6.Size = New Size(10, 32)
        JustEmptyControl6.TabIndex = 10
        ' 
        ' MCB_高度缩放
        ' 
        MCB_高度缩放.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_高度缩放.BorderRadius = 10
        MCB_高度缩放.BorderSize = 0
        MCB_高度缩放.Dock = DockStyle.Left
        MCB_高度缩放.DropDownBackdropBlurPasses = 2
        MCB_高度缩放.DropDownBackdropBlurRadius = 30
        MCB_高度缩放.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_高度缩放.DropDownHoverColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        MCB_高度缩放.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_高度缩放.DropDownPadding = New Padding(10)
        MCB_高度缩放.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_高度缩放.DropDownSelectedForeColor = Color.White
        MCB_高度缩放.Editable = True
        MCB_高度缩放.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_高度缩放.Items.Add("")
        MCB_高度缩放.Items.Add("ih")
        MCB_高度缩放.Items.Add("ih/2")
        MCB_高度缩放.Items.Add("ih*2")
        MCB_高度缩放.Location = New Point(160, 10)
        MCB_高度缩放.Margin = New Padding(2, 2, 2, 2)
        MCB_高度缩放.Name = "MCB_高度缩放"
        MCB_高度缩放.Padding = New Padding(10, 0, 10, 0)
        MCB_高度缩放.Size = New Size(150, 32)
        MCB_高度缩放.TabIndex = 9
        MCB_高度缩放.ToolTipGap = -1
        MCB_高度缩放.ToolTipMaxWidth = 350
        MCB_高度缩放.ToolTipPadding = New Padding(15)
        MCB_高度缩放.WaterText = "高度缩放"
        MCB_高度缩放.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' JustEmptyControl2
        ' 
        JustEmptyControl2.Dock = DockStyle.Left
        JustEmptyControl2.Location = New Point(150, 10)
        JustEmptyControl2.Name = "JustEmptyControl2"
        JustEmptyControl2.Size = New Size(10, 32)
        JustEmptyControl2.TabIndex = 8
        ' 
        ' MCB_宽度缩放
        ' 
        MCB_宽度缩放.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_宽度缩放.BorderRadius = 10
        MCB_宽度缩放.BorderSize = 0
        MCB_宽度缩放.Dock = DockStyle.Left
        MCB_宽度缩放.DropDownBackdropBlurPasses = 2
        MCB_宽度缩放.DropDownBackdropBlurRadius = 30
        MCB_宽度缩放.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_宽度缩放.DropDownHoverColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        MCB_宽度缩放.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_宽度缩放.DropDownPadding = New Padding(10)
        MCB_宽度缩放.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_宽度缩放.DropDownSelectedForeColor = Color.White
        MCB_宽度缩放.Editable = True
        MCB_宽度缩放.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_宽度缩放.Items.Add("")
        MCB_宽度缩放.Items.Add("iw")
        MCB_宽度缩放.Items.Add("iw/2")
        MCB_宽度缩放.Items.Add("iw*2")
        MCB_宽度缩放.Location = New Point(0, 10)
        MCB_宽度缩放.Margin = New Padding(2, 2, 2, 2)
        MCB_宽度缩放.Name = "MCB_宽度缩放"
        MCB_宽度缩放.Padding = New Padding(10, 0, 10, 0)
        MCB_宽度缩放.Size = New Size(150, 32)
        MCB_宽度缩放.TabIndex = 0
        MCB_宽度缩放.ToolTipGap = -1
        MCB_宽度缩放.ToolTipMaxWidth = 350
        MCB_宽度缩放.ToolTipPadding = New Padding(15)
        MCB_宽度缩放.WaterText = "宽度缩放"
        MCB_宽度缩放.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' Panel2
        ' 
        Panel2.Controls.Add(LB_直接指定分辨率说明)
        Panel2.Controls.Add(MCB_直接指定分辨率)
        Panel2.Dock = DockStyle.Top
        Panel2.Location = New Point(20, 50)
        Panel2.Name = "Panel2"
        Panel2.Padding = New Padding(0, 10, 0, 0)
        Panel2.Size = New Size(729, 42)
        Panel2.TabIndex = 5
        ' 
        ' LB_直接指定分辨率说明
        ' 
        LB_直接指定分辨率说明.Dock = DockStyle.Fill
        LB_直接指定分辨率说明.ForeColor = Color.DarkGoldenrod
        LB_直接指定分辨率说明.Location = New Point(150, 10)
        LB_直接指定分辨率说明.Name = "LB_直接指定分辨率说明"
        LB_直接指定分辨率说明.Padding = New Padding(10, 0, 0, 0)
        LB_直接指定分辨率说明.Size = New Size(579, 32)
        LB_直接指定分辨率说明.TabIndex = 6
        LB_直接指定分辨率说明.Text = "传统的直接指定分辨率，批量任务通常不这样做"
        LB_直接指定分辨率说明.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' MCB_直接指定分辨率
        ' 
        MCB_直接指定分辨率.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_直接指定分辨率.BorderRadius = 10
        MCB_直接指定分辨率.BorderSize = 0
        MCB_直接指定分辨率.Dock = DockStyle.Left
        MCB_直接指定分辨率.DropDownBackdropBlurPasses = 2
        MCB_直接指定分辨率.DropDownBackdropBlurRadius = 30
        MCB_直接指定分辨率.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_直接指定分辨率.DropDownHoverColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        MCB_直接指定分辨率.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_直接指定分辨率.DropDownPadding = New Padding(10)
        MCB_直接指定分辨率.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_直接指定分辨率.DropDownSelectedForeColor = Color.White
        MCB_直接指定分辨率.Editable = True
        MCB_直接指定分辨率.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_直接指定分辨率.Items.Add("1024x576")
        MCB_直接指定分辨率.Items.Add("1280x720")
        MCB_直接指定分辨率.Items.Add("1600x900")
        MCB_直接指定分辨率.Items.Add("1920x1080")
        MCB_直接指定分辨率.Items.Add("2560x1440")
        MCB_直接指定分辨率.Items.Add("3840x2160")
        MCB_直接指定分辨率.Location = New Point(0, 10)
        MCB_直接指定分辨率.Margin = New Padding(2, 2, 2, 2)
        MCB_直接指定分辨率.Name = "MCB_直接指定分辨率"
        MCB_直接指定分辨率.Padding = New Padding(10, 0, 10, 0)
        MCB_直接指定分辨率.Size = New Size(150, 32)
        MCB_直接指定分辨率.TabIndex = 0
        MCB_直接指定分辨率.ToolTipGap = -1
        MCB_直接指定分辨率.ToolTipMaxWidth = 350
        MCB_直接指定分辨率.ToolTipPadding = New Padding(15)
        MCB_直接指定分辨率.WaterText = "直接指定"
        MCB_直接指定分辨率.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' HCL_分辨率设置标题
        ' 
        HCL_分辨率设置标题.AutoSize = True
        HCL_分辨率设置标题.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_分辨率设置标题.Dock = DockStyle.Top
        HCL_分辨率设置标题.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_分辨率设置标题.Location = New Point(20, 20)
        HCL_分辨率设置标题.Margin = New Padding(2)
        HCL_分辨率设置标题.Name = "HCL_分辨率设置标题"
        HCL_分辨率设置标题.Padding = New Padding(0, 0, 0, 5)
        HCL_分辨率设置标题.Size = New Size(729, 30)
        HCL_分辨率设置标题.TabIndex = 4
        HCL_分辨率设置标题.Text = "<span style=""font-size:13; color:Silver"">分辨率</span>   推荐使用在滤镜中处理的单独缩放"
        ' 
        ' Form_v6_参数面板_画面帧
        ' 
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ClientSize = New Size(769, 618)
        Controls.Add(ModernPanel1)
        Font = New Font("Microsoft YaHei UI", 10F)
        ForeColor = Color.Silver
        Name = "Form_v6_参数面板_画面帧"
        Text = "Form_v6_参数面板_画面帧"
        ModernPanel1.ResumeLayout(False)
        ModernPanel1.PerformLayout()
        Panel6.ResumeLayout(False)
        Panel8.ResumeLayout(False)
        Panel7.ResumeLayout(False)
        Panel5.ResumeLayout(False)
        Panel3.ResumeLayout(False)
        Panel4.ResumeLayout(False)
        Panel1.ResumeLayout(False)
        Panel2.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents ModernPanel1 As LakeUI.ModernPanel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents MCB_直接指定分辨率 As LakeUI.ModernComboBox
    Friend WithEvents HCL_分辨率设置标题 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents MCB_宽度缩放 As LakeUI.ModernComboBox
    Friend WithEvents LB_单独缩放说明 As Label
    Friend WithEvents LB_直接指定分辨率说明 As Label
    Friend WithEvents Panel4 As Panel
    Friend WithEvents MB_画面裁剪交互 As LakeUI.ModernButton
    Friend WithEvents LB_裁剪滤镜排序说明 As Label
    Friend WithEvents MTB_画面裁剪参数 As LakeUI.ModernTextBox
    Friend WithEvents JustEmptyControl1 As LakeUI.JustEmptyControl
    Friend WithEvents MCB_高度缩放 As LakeUI.ModernComboBox
    Friend WithEvents JustEmptyControl2 As LakeUI.JustEmptyControl
    Friend WithEvents HCL_帧率设置标题 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents MCB_直接指定帧率 As LakeUI.ModernComboBox
    Friend WithEvents MB_抽帧设置 As LakeUI.ModernButton
    Friend WithEvents JustEmptyControl3 As LakeUI.JustEmptyControl
    Friend WithEvents HCL_画面增强标题 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents MB_着色器超分 As LakeUI.ModernButton
    Friend WithEvents JustEmptyControl5 As LakeUI.JustEmptyControl
    Friend WithEvents MB_动态模糊 As LakeUI.ModernButton
    Friend WithEvents JustEmptyControl4 As LakeUI.JustEmptyControl
    Friend WithEvents MB_简易插帧 As LakeUI.ModernButton
    Friend WithEvents HCL_画面内容标题 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel6 As Panel
    Friend WithEvents MB_烧录字幕 As LakeUI.ModernButton
    Friend WithEvents Panel7 As Panel
    Friend WithEvents MB_胶片颗粒 As LakeUI.ModernButton
    Friend WithEvents JustEmptyControl8 As LakeUI.JustEmptyControl
    Friend WithEvents MB_传统锐化 As LakeUI.ModernButton
    Friend WithEvents JustEmptyControl9 As LakeUI.JustEmptyControl
    Friend WithEvents MB_传统降噪 As LakeUI.ModernButton
    Friend WithEvents Panel8 As Panel
    Friend WithEvents MB_平滑断层 As LakeUI.ModernButton
    Friend WithEvents JustEmptyControl10 As LakeUI.JustEmptyControl
    Friend WithEvents MB_画面翻转 As LakeUI.ModernButton
    Friend WithEvents JustEmptyControl11 As LakeUI.JustEmptyControl
    Friend WithEvents MB_扫描方式 As LakeUI.ModernButton
    Friend WithEvents MCB_指定缩放算法 As LakeUI.ModernComboBox
    Friend WithEvents JustEmptyControl6 As LakeUI.JustEmptyControl
End Class
