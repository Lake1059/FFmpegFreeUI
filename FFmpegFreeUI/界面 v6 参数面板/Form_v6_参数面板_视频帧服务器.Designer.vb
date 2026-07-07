<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_v6_参数面板_视频帧服务器
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_v6_参数面板_视频帧服务器))
        ModernPanel1 = New LakeUI.ModernPanel()
        Panel1 = New Panel()
        MCB_VapourSynth脚本文件 = New LakeUI.ModernComboBox()
        JustEmptyControl2 = New LakeUI.JustEmptyControl()
        BS_使用VapourSynth = New LakeUI.BooleanSwitch()
        HCL_VapourSynth说明 = New LakeUI.HtmlColorLabel()
        HCL_VapourSynth标题 = New LakeUI.HtmlColorLabel()
        Panel7 = New Panel()
        MCB_AviSynth脚本文件 = New LakeUI.ModernComboBox()
        JustEmptyControl1 = New LakeUI.JustEmptyControl()
        BS_使用AviSynth = New LakeUI.BooleanSwitch()
        HCL_AviSynth说明 = New LakeUI.HtmlColorLabel()
        HCL_AviSynth标题 = New LakeUI.HtmlColorLabel()
        HCL_视频帧服务器说明 = New LakeUI.HtmlColorLabel()
        HCL_视频帧服务器标题 = New LakeUI.HtmlColorLabel()
        ModernPanel1.SuspendLayout()
        Panel1.SuspendLayout()
        Panel7.SuspendLayout()
        SuspendLayout()
        '
        ' ModernPanel1
        '
        ModernPanel1.BackColor1 = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ModernPanel1.BorderSize = 0
        ModernPanel1.Controls.Add(Panel1)
        ModernPanel1.Controls.Add(HCL_VapourSynth说明)
        ModernPanel1.Controls.Add(HCL_VapourSynth标题)
        ModernPanel1.Controls.Add(Panel7)
        ModernPanel1.Controls.Add(HCL_AviSynth说明)
        ModernPanel1.Controls.Add(HCL_AviSynth标题)
        ModernPanel1.Controls.Add(HCL_视频帧服务器说明)
        ModernPanel1.Controls.Add(HCL_视频帧服务器标题)
        ModernPanel1.Dock = DockStyle.Fill
        ModernPanel1.Location = New Point(0, 0)
        ModernPanel1.Name = "ModernPanel1"
        ModernPanel1.Padding = New Padding(20)
        ModernPanel1.ScrollBarMode = LakeUI.ModernPanel.ScrollMode.Vertical
        ModernPanel1.Size = New Size(764, 630)
        ModernPanel1.TabIndex = 0
        '
        ' Panel1
        '
        Panel1.Controls.Add(MCB_VapourSynth脚本文件)
        Panel1.Controls.Add(JustEmptyControl2)
        Panel1.Controls.Add(BS_使用VapourSynth)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(20, 390)
        Panel1.Name = "Panel1"
        Panel1.Padding = New Padding(0, 10, 0, 0)
        Panel1.Size = New Size(724, 42)
        Panel1.TabIndex = 14
        '
        ' MCB_VapourSynth脚本文件
        '
        MCB_VapourSynth脚本文件.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_VapourSynth脚本文件.BorderRadius = 10
        MCB_VapourSynth脚本文件.BorderSize = 0
        MCB_VapourSynth脚本文件.Dock = DockStyle.Fill
        MCB_VapourSynth脚本文件.DropDownBackdropBlurPasses = 2
        MCB_VapourSynth脚本文件.DropDownBackdropBlurRadius = 30
        MCB_VapourSynth脚本文件.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_VapourSynth脚本文件.DropDownHoverColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        MCB_VapourSynth脚本文件.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_VapourSynth脚本文件.DropDownPadding = New Padding(10)
        MCB_VapourSynth脚本文件.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_VapourSynth脚本文件.DropDownSelectedForeColor = Color.White
        MCB_VapourSynth脚本文件.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_VapourSynth脚本文件.Items.Add("浏览 ...")
        MCB_VapourSynth脚本文件.Location = New Point(65, 10)
        MCB_VapourSynth脚本文件.Margin = New Padding(2, 2, 2, 2)
        MCB_VapourSynth脚本文件.Name = "MCB_VapourSynth脚本文件"
        MCB_VapourSynth脚本文件.Padding = New Padding(10, 0, 10, 0)
        MCB_VapourSynth脚本文件.Size = New Size(659, 32)
        MCB_VapourSynth脚本文件.TabIndex = 1
        MCB_VapourSynth脚本文件.ToolTipGap = -1
        MCB_VapourSynth脚本文件.ToolTipMaxWidth = 350
        MCB_VapourSynth脚本文件.ToolTipPadding = New Padding(15)
        MCB_VapourSynth脚本文件.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        '
        ' JustEmptyControl2
        '
        JustEmptyControl2.Dock = DockStyle.Left
        JustEmptyControl2.Location = New Point(55, 10)
        JustEmptyControl2.Name = "JustEmptyControl2"
        JustEmptyControl2.Size = New Size(10, 32)
        JustEmptyControl2.TabIndex = 3
        '
        ' BS_使用VapourSynth
        '
        BS_使用VapourSynth.BorderColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        BS_使用VapourSynth.BorderSize = 2
        BS_使用VapourSynth.Dock = DockStyle.Left
        BS_使用VapourSynth.KnobColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        BS_使用VapourSynth.KnobPadding = 4
        BS_使用VapourSynth.Location = New Point(0, 10)
        BS_使用VapourSynth.Margin = New Padding(2, 2, 2, 2)
        BS_使用VapourSynth.Name = "BS_使用VapourSynth"
        BS_使用VapourSynth.Size = New Size(55, 32)
        BS_使用VapourSynth.TabIndex = 0
        BS_使用VapourSynth.TrackColorOff = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        BS_使用VapourSynth.TrackColorOn = Color.MediumSlateBlue
        '
        ' HCL_VapourSynth说明
        '
        HCL_VapourSynth说明.AutoSize = True
        HCL_VapourSynth说明.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_VapourSynth说明.Dock = DockStyle.Top
        HCL_VapourSynth说明.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_VapourSynth说明.Location = New Point(20, 344)
        HCL_VapourSynth说明.Margin = New Padding(2)
        HCL_VapourSynth说明.Name = "HCL_VapourSynth说明"
        HCL_VapourSynth说明.Padding = New Padding(0, 0, 0, 5)
        HCL_VapourSynth说明.Size = New Size(724, 46)
        HCL_VapourSynth说明.TabIndex = 13
        HCL_VapourSynth说明.Text = "<span style=""font-size:10pt; color:Gray"">在 .vpy/.py 脚本文件中使用 &lt;FilePath&gt; 来表示输入文件路径</span><br><span style=""font-size:10pt; color:Gray"">创建 VapourSynth 文件夹并放置 .vpy/.py 文件就可以在下拉框快速选择</span>"
        '
        ' HCL_VapourSynth标题
        '
        HCL_VapourSynth标题.AutoSize = True
        HCL_VapourSynth标题.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_VapourSynth标题.Dock = DockStyle.Top
        HCL_VapourSynth标题.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_VapourSynth标题.Location = New Point(20, 294)
        HCL_VapourSynth标题.Margin = New Padding(2)
        HCL_VapourSynth标题.Name = "HCL_VapourSynth标题"
        HCL_VapourSynth标题.Padding = New Padding(0, 20, 0, 5)
        HCL_VapourSynth标题.Size = New Size(724, 50)
        HCL_VapourSynth标题.TabIndex = 12
        HCL_VapourSynth标题.Text = "<span style=""font-size:13; color:Silver"">VapourSynth</span>"
        '
        ' Panel7
        '
        Panel7.Controls.Add(MCB_AviSynth脚本文件)
        Panel7.Controls.Add(JustEmptyControl1)
        Panel7.Controls.Add(BS_使用AviSynth)
        Panel7.Dock = DockStyle.Top
        Panel7.Location = New Point(20, 252)
        Panel7.Name = "Panel7"
        Panel7.Padding = New Padding(0, 10, 0, 0)
        Panel7.Size = New Size(724, 42)
        Panel7.TabIndex = 11
        '
        ' MCB_AviSynth脚本文件
        '
        MCB_AviSynth脚本文件.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_AviSynth脚本文件.BorderRadius = 10
        MCB_AviSynth脚本文件.BorderSize = 0
        MCB_AviSynth脚本文件.Dock = DockStyle.Fill
        MCB_AviSynth脚本文件.DropDownBackdropBlurPasses = 2
        MCB_AviSynth脚本文件.DropDownBackdropBlurRadius = 30
        MCB_AviSynth脚本文件.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_AviSynth脚本文件.DropDownHoverColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        MCB_AviSynth脚本文件.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_AviSynth脚本文件.DropDownPadding = New Padding(10)
        MCB_AviSynth脚本文件.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_AviSynth脚本文件.DropDownSelectedForeColor = Color.White
        MCB_AviSynth脚本文件.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_AviSynth脚本文件.Items.Add("浏览 ...")
        MCB_AviSynth脚本文件.Location = New Point(65, 10)
        MCB_AviSynth脚本文件.Margin = New Padding(2, 2, 2, 2)
        MCB_AviSynth脚本文件.Name = "MCB_AviSynth脚本文件"
        MCB_AviSynth脚本文件.Padding = New Padding(10, 0, 10, 0)
        MCB_AviSynth脚本文件.Size = New Size(659, 32)
        MCB_AviSynth脚本文件.TabIndex = 1
        MCB_AviSynth脚本文件.ToolTipGap = -1
        MCB_AviSynth脚本文件.ToolTipMaxWidth = 350
        MCB_AviSynth脚本文件.ToolTipPadding = New Padding(15)
        MCB_AviSynth脚本文件.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        '
        ' JustEmptyControl1
        '
        JustEmptyControl1.Dock = DockStyle.Left
        JustEmptyControl1.Location = New Point(55, 10)
        JustEmptyControl1.Name = "JustEmptyControl1"
        JustEmptyControl1.Size = New Size(10, 32)
        JustEmptyControl1.TabIndex = 3
        '
        ' BS_使用AviSynth
        '
        BS_使用AviSynth.BorderColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        BS_使用AviSynth.BorderSize = 2
        BS_使用AviSynth.Dock = DockStyle.Left
        BS_使用AviSynth.KnobColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        BS_使用AviSynth.KnobPadding = 4
        BS_使用AviSynth.Location = New Point(0, 10)
        BS_使用AviSynth.Margin = New Padding(2, 2, 2, 2)
        BS_使用AviSynth.Name = "BS_使用AviSynth"
        BS_使用AviSynth.Size = New Size(55, 32)
        BS_使用AviSynth.TabIndex = 0
        BS_使用AviSynth.TrackColorOff = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        BS_使用AviSynth.TrackColorOn = Color.MediumSlateBlue
        '
        ' HCL_AviSynth说明
        '
        HCL_AviSynth说明.AutoSize = True
        HCL_AviSynth说明.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_AviSynth说明.Dock = DockStyle.Top
        HCL_AviSynth说明.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_AviSynth说明.Location = New Point(20, 206)
        HCL_AviSynth说明.Margin = New Padding(2)
        HCL_AviSynth说明.Name = "HCL_AviSynth说明"
        HCL_AviSynth说明.Padding = New Padding(0, 0, 0, 5)
        HCL_AviSynth说明.Size = New Size(724, 46)
        HCL_AviSynth说明.TabIndex = 10
        HCL_AviSynth说明.Text = "<span style=""font-size:10pt; color:Gray"">在 .avs 脚本文件中使用 &lt;FilePath&gt; 来表示输入文件路径</span><br><span style=""font-size:10pt; color:Gray"">创建 AviSynth 文件夹并放置 .avs 文件就可以在下拉框快速选择</span>"
        '
        ' HCL_AviSynth标题
        '
        HCL_AviSynth标题.AutoSize = True
        HCL_AviSynth标题.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_AviSynth标题.Dock = DockStyle.Top
        HCL_AviSynth标题.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_AviSynth标题.Location = New Point(20, 156)
        HCL_AviSynth标题.Margin = New Padding(2)
        HCL_AviSynth标题.Name = "HCL_AviSynth标题"
        HCL_AviSynth标题.Padding = New Padding(0, 20, 0, 5)
        HCL_AviSynth标题.Size = New Size(724, 50)
        HCL_AviSynth标题.TabIndex = 9
        HCL_AviSynth标题.Text = "<span style=""font-size:13; color:Silver"">AviSynth</span>"
        '
        ' HCL_视频帧服务器说明
        '
        HCL_视频帧服务器说明.AutoSize = True
        HCL_视频帧服务器说明.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_视频帧服务器说明.Dock = DockStyle.Top
        HCL_视频帧服务器说明.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_视频帧服务器说明.Location = New Point(20, 50)
        HCL_视频帧服务器说明.Margin = New Padding(2)
        HCL_视频帧服务器说明.Name = "HCL_视频帧服务器说明"
        HCL_视频帧服务器说明.Padding = New Padding(0, 0, 0, 5)
        HCL_视频帧服务器说明.Size = New Size(724, 106)
        HCL_视频帧服务器说明.TabIndex = 8
        HCL_视频帧服务器说明.Text = resources.GetString("HCL_视频帧服务器说明.Text")
        '
        ' HCL_视频帧服务器标题
        '
        HCL_视频帧服务器标题.AutoSize = True
        HCL_视频帧服务器标题.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_视频帧服务器标题.Dock = DockStyle.Top
        HCL_视频帧服务器标题.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_视频帧服务器标题.Location = New Point(20, 20)
        HCL_视频帧服务器标题.Margin = New Padding(2)
        HCL_视频帧服务器标题.Name = "HCL_视频帧服务器标题"
        HCL_视频帧服务器标题.Padding = New Padding(0, 0, 0, 5)
        HCL_视频帧服务器标题.Size = New Size(724, 30)
        HCL_视频帧服务器标题.TabIndex = 7
        HCL_视频帧服务器标题.Text = "<span style=""font-size:13; color:Silver"">视频帧服务器</span>   这是高阶内容，如果你是新手，不要考虑这些"
        '
        ' Form_v6_参数面板_视频帧服务器
        '
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ClientSize = New Size(764, 630)
        Controls.Add(ModernPanel1)
        Font = New Font("Microsoft YaHei UI", 10F)
        ForeColor = Color.Silver
        Name = "Form_v6_参数面板_视频帧服务器"
        Text = "Form_v6_参数面板_视频帧服务器"
        ModernPanel1.ResumeLayout(False)
        ModernPanel1.PerformLayout()
        Panel1.ResumeLayout(False)
        Panel7.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents ModernPanel1 As LakeUI.ModernPanel
    Friend WithEvents HCL_视频帧服务器标题 As LakeUI.HtmlColorLabel
    Friend WithEvents HCL_视频帧服务器说明 As LakeUI.HtmlColorLabel
    Friend WithEvents HCL_AviSynth标题 As LakeUI.HtmlColorLabel
    Friend WithEvents HCL_AviSynth说明 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel7 As Panel
    Friend WithEvents BS_使用AviSynth As LakeUI.BooleanSwitch
    Friend WithEvents MCB_AviSynth脚本文件 As LakeUI.ModernComboBox
    Friend WithEvents JustEmptyControl1 As LakeUI.JustEmptyControl
    Friend WithEvents Panel1 As Panel
    Friend WithEvents MCB_VapourSynth脚本文件 As LakeUI.ModernComboBox
    Friend WithEvents JustEmptyControl2 As LakeUI.JustEmptyControl
    Friend WithEvents BS_使用VapourSynth As LakeUI.BooleanSwitch
    Friend WithEvents HCL_VapourSynth说明 As LakeUI.HtmlColorLabel
    Friend WithEvents HCL_VapourSynth标题 As LakeUI.HtmlColorLabel
End Class
