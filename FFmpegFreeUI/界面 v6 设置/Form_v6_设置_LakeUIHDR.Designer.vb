<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_v6_设置_LakeUIHDR
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
        Dim PanelHDR启用 As Panel
        HtmlColorLabel启用 = New LakeUI.HtmlColorLabel()
        MCB_HDR启用 = New LakeUI.ModernComboBox()
        ModernPanel1 = New LakeUI.ModernPanel()
        PanelHDR图片 = New Panel()
        HtmlColorLabel图片 = New LakeUI.HtmlColorLabel()
        MCB_HDR图片 = New LakeUI.ModernComboBox()
        PanelHDR矢量颜色 = New Panel()
        HtmlColorLabel矢量颜色 = New LakeUI.HtmlColorLabel()
        MCB_HDR矢量颜色 = New LakeUI.ModernComboBox()
        PanelHDR显示档位 = New Panel()
        HtmlColorLabel显示档位 = New LakeUI.HtmlColorLabel()
        MCB_HDR显示档位 = New LakeUI.ModernComboBox()
        HtmlColorLabel1 = New LakeUI.HtmlColorLabel()
        PanelHDR启用 = New Panel()
        PanelHDR启用.SuspendLayout()
        ModernPanel1.SuspendLayout()
        PanelHDR图片.SuspendLayout()
        PanelHDR矢量颜色.SuspendLayout()
        PanelHDR显示档位.SuspendLayout()
        SuspendLayout()
        ' 
        ' PanelHDR启用
        ' 
        PanelHDR启用.Controls.Add(HtmlColorLabel启用)
        PanelHDR启用.Controls.Add(MCB_HDR启用)
        PanelHDR启用.Dock = DockStyle.Top
        PanelHDR启用.Location = New Point(20, 65)
        PanelHDR启用.Name = "PanelHDR启用"
        PanelHDR启用.Padding = New Padding(0, 10, 0, 0)
        PanelHDR启用.Size = New Size(702, 42)
        PanelHDR启用.TabIndex = 2
        ' 
        ' HtmlColorLabel启用
        ' 
        HtmlColorLabel启用.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel启用.Dock = DockStyle.Fill
        HtmlColorLabel启用.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel启用.Location = New Point(220, 10)
        HtmlColorLabel启用.Margin = New Padding(2)
        HtmlColorLabel启用.Name = "HtmlColorLabel启用"
        HtmlColorLabel启用.Padding = New Padding(10, 0, 0, 0)
        HtmlColorLabel启用.Size = New Size(482, 32)
        HtmlColorLabel启用.TabIndex = 1
        HtmlColorLabel启用.Text = "<span style=""color:Silver"">HDR 总开关</span>"
        HtmlColorLabel启用.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        ' 
        ' MCB_HDR启用
        ' 
        MCB_HDR启用.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_HDR启用.BorderRadius = 10
        MCB_HDR启用.BorderSize = 0
        MCB_HDR启用.Dock = DockStyle.Left
        MCB_HDR启用.DropDownBackdropBlurPasses = 2
        MCB_HDR启用.DropDownBackdropBlurRadius = 30
        MCB_HDR启用.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_HDR启用.DropDownHoverColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        MCB_HDR启用.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_HDR启用.DropDownPadding = New Padding(10)
        MCB_HDR启用.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_HDR启用.DropDownSelectedForeColor = Color.White
        MCB_HDR启用.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_HDR启用.Items.Add("关闭 HDR 映射")
        MCB_HDR启用.Items.Add("启用 HDR 映射")
        MCB_HDR启用.Location = New Point(0, 10)
        MCB_HDR启用.Margin = New Padding(2, 2, 2, 2)
        MCB_HDR启用.Name = "MCB_HDR启用"
        MCB_HDR启用.Padding = New Padding(10, 0, 10, 0)
        MCB_HDR启用.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_HDR启用.Size = New Size(220, 32)
        MCB_HDR启用.TabIndex = 0
        MCB_HDR启用.ToolTipGap = -1
        MCB_HDR启用.ToolTipMaxWidth = 350
        MCB_HDR启用.ToolTipPadding = New Padding(15)
        MCB_HDR启用.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' ModernPanel1
        ' 
        ModernPanel1.BackColor1 = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ModernPanel1.BorderSize = 0
        ModernPanel1.Controls.Add(PanelHDR图片)
        ModernPanel1.Controls.Add(PanelHDR矢量颜色)
        ModernPanel1.Controls.Add(PanelHDR显示档位)
        ModernPanel1.Controls.Add(PanelHDR启用)
        ModernPanel1.Controls.Add(HtmlColorLabel1)
        ModernPanel1.Dock = DockStyle.Fill
        ModernPanel1.Location = New Point(0, 0)
        ModernPanel1.Name = "ModernPanel1"
        ModernPanel1.Padding = New Padding(20)
        ModernPanel1.ScrollBarMode = LakeUI.ModernPanel.ScrollMode.Vertical
        ModernPanel1.Size = New Size(742, 632)
        ModernPanel1.TabIndex = 0
        ' 
        ' PanelHDR图片
        ' 
        PanelHDR图片.Controls.Add(HtmlColorLabel图片)
        PanelHDR图片.Controls.Add(MCB_HDR图片)
        PanelHDR图片.Dock = DockStyle.Top
        PanelHDR图片.Location = New Point(20, 191)
        PanelHDR图片.Name = "PanelHDR图片"
        PanelHDR图片.Padding = New Padding(0, 10, 0, 0)
        PanelHDR图片.Size = New Size(702, 42)
        PanelHDR图片.TabIndex = 5
        ' 
        ' HtmlColorLabel图片
        ' 
        HtmlColorLabel图片.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel图片.Dock = DockStyle.Fill
        HtmlColorLabel图片.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel图片.Location = New Point(220, 10)
        HtmlColorLabel图片.Margin = New Padding(2)
        HtmlColorLabel图片.Name = "HtmlColorLabel图片"
        HtmlColorLabel图片.Padding = New Padding(10, 0, 0, 0)
        HtmlColorLabel图片.Size = New Size(482, 32)
        HtmlColorLabel图片.TabIndex = 1
        HtmlColorLabel图片.Text = "<span style=""color:Silver"">是否对图片映射</span>"
        HtmlColorLabel图片.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        ' 
        ' MCB_HDR图片
        ' 
        MCB_HDR图片.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_HDR图片.BorderRadius = 10
        MCB_HDR图片.BorderSize = 0
        MCB_HDR图片.Dock = DockStyle.Left
        MCB_HDR图片.DropDownBackdropBlurPasses = 2
        MCB_HDR图片.DropDownBackdropBlurRadius = 30
        MCB_HDR图片.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_HDR图片.DropDownHoverColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        MCB_HDR图片.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_HDR图片.DropDownPadding = New Padding(10)
        MCB_HDR图片.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_HDR图片.DropDownSelectedForeColor = Color.White
        MCB_HDR图片.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_HDR图片.Items.Add("映射 HDR")
        MCB_HDR图片.Items.Add("保持 SDR")
        MCB_HDR图片.Location = New Point(0, 10)
        MCB_HDR图片.Margin = New Padding(2, 2, 2, 2)
        MCB_HDR图片.Name = "MCB_HDR图片"
        MCB_HDR图片.Padding = New Padding(10, 0, 10, 0)
        MCB_HDR图片.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_HDR图片.Size = New Size(220, 32)
        MCB_HDR图片.TabIndex = 0
        MCB_HDR图片.ToolTipGap = -1
        MCB_HDR图片.ToolTipMaxWidth = 350
        MCB_HDR图片.ToolTipPadding = New Padding(15)
        MCB_HDR图片.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' PanelHDR矢量颜色
        ' 
        PanelHDR矢量颜色.Controls.Add(HtmlColorLabel矢量颜色)
        PanelHDR矢量颜色.Controls.Add(MCB_HDR矢量颜色)
        PanelHDR矢量颜色.Dock = DockStyle.Top
        PanelHDR矢量颜色.Location = New Point(20, 149)
        PanelHDR矢量颜色.Name = "PanelHDR矢量颜色"
        PanelHDR矢量颜色.Padding = New Padding(0, 10, 0, 0)
        PanelHDR矢量颜色.Size = New Size(702, 42)
        PanelHDR矢量颜色.TabIndex = 4
        ' 
        ' HtmlColorLabel矢量颜色
        ' 
        HtmlColorLabel矢量颜色.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel矢量颜色.Dock = DockStyle.Fill
        HtmlColorLabel矢量颜色.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel矢量颜色.Location = New Point(220, 10)
        HtmlColorLabel矢量颜色.Margin = New Padding(2)
        HtmlColorLabel矢量颜色.Name = "HtmlColorLabel矢量颜色"
        HtmlColorLabel矢量颜色.Padding = New Padding(10, 0, 0, 0)
        HtmlColorLabel矢量颜色.Size = New Size(482, 32)
        HtmlColorLabel矢量颜色.TabIndex = 1
        HtmlColorLabel矢量颜色.Text = "<span style=""color:Silver"">是否对矢量对象映射</span>"
        HtmlColorLabel矢量颜色.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        ' 
        ' MCB_HDR矢量颜色
        ' 
        MCB_HDR矢量颜色.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_HDR矢量颜色.BorderRadius = 10
        MCB_HDR矢量颜色.BorderSize = 0
        MCB_HDR矢量颜色.Dock = DockStyle.Left
        MCB_HDR矢量颜色.DropDownBackdropBlurPasses = 2
        MCB_HDR矢量颜色.DropDownBackdropBlurRadius = 30
        MCB_HDR矢量颜色.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_HDR矢量颜色.DropDownHoverColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        MCB_HDR矢量颜色.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_HDR矢量颜色.DropDownPadding = New Padding(10)
        MCB_HDR矢量颜色.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_HDR矢量颜色.DropDownSelectedForeColor = Color.White
        MCB_HDR矢量颜色.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_HDR矢量颜色.Items.Add("映射 HDR")
        MCB_HDR矢量颜色.Items.Add("保持 SDR")
        MCB_HDR矢量颜色.Location = New Point(0, 10)
        MCB_HDR矢量颜色.Margin = New Padding(2, 2, 2, 2)
        MCB_HDR矢量颜色.Name = "MCB_HDR矢量颜色"
        MCB_HDR矢量颜色.Padding = New Padding(10, 0, 10, 0)
        MCB_HDR矢量颜色.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_HDR矢量颜色.Size = New Size(220, 32)
        MCB_HDR矢量颜色.TabIndex = 0
        MCB_HDR矢量颜色.ToolTipGap = -1
        MCB_HDR矢量颜色.ToolTipMaxWidth = 350
        MCB_HDR矢量颜色.ToolTipPadding = New Padding(15)
        MCB_HDR矢量颜色.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' PanelHDR显示档位
        ' 
        PanelHDR显示档位.Controls.Add(HtmlColorLabel显示档位)
        PanelHDR显示档位.Controls.Add(MCB_HDR显示档位)
        PanelHDR显示档位.Dock = DockStyle.Top
        PanelHDR显示档位.Location = New Point(20, 107)
        PanelHDR显示档位.Name = "PanelHDR显示档位"
        PanelHDR显示档位.Padding = New Padding(0, 10, 0, 0)
        PanelHDR显示档位.Size = New Size(702, 42)
        PanelHDR显示档位.TabIndex = 3
        ' 
        ' HtmlColorLabel显示档位
        ' 
        HtmlColorLabel显示档位.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel显示档位.Dock = DockStyle.Fill
        HtmlColorLabel显示档位.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel显示档位.Location = New Point(220, 10)
        HtmlColorLabel显示档位.Margin = New Padding(2)
        HtmlColorLabel显示档位.Name = "HtmlColorLabel显示档位"
        HtmlColorLabel显示档位.Padding = New Padding(10, 0, 0, 0)
        HtmlColorLabel显示档位.Size = New Size(482, 32)
        HtmlColorLabel显示档位.TabIndex = 1
        HtmlColorLabel显示档位.Text = "<span style=""color:Silver"">HDR 映射档位</span>"
        HtmlColorLabel显示档位.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        ' 
        ' MCB_HDR显示档位
        ' 
        MCB_HDR显示档位.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_HDR显示档位.BorderRadius = 10
        MCB_HDR显示档位.BorderSize = 0
        MCB_HDR显示档位.Dock = DockStyle.Left
        MCB_HDR显示档位.DropDownBackdropBlurPasses = 2
        MCB_HDR显示档位.DropDownBackdropBlurRadius = 30
        MCB_HDR显示档位.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_HDR显示档位.DropDownHoverColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        MCB_HDR显示档位.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_HDR显示档位.DropDownPadding = New Padding(10)
        MCB_HDR显示档位.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_HDR显示档位.DropDownSelectedForeColor = Color.White
        MCB_HDR显示档位.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_HDR显示档位.Items.Add("HDR200")
        MCB_HDR显示档位.Items.Add("HDR300")
        MCB_HDR显示档位.Items.Add("HDR400")
        MCB_HDR显示档位.Items.Add("HDR500")
        MCB_HDR显示档位.Items.Add("HDR600")
        MCB_HDR显示档位.Items.Add("HDR700")
        MCB_HDR显示档位.Items.Add("HDR800")
        MCB_HDR显示档位.Items.Add("HDR900")
        MCB_HDR显示档位.Items.Add("HDR1000")
        MCB_HDR显示档位.Location = New Point(0, 10)
        MCB_HDR显示档位.Margin = New Padding(2, 2, 2, 2)
        MCB_HDR显示档位.Name = "MCB_HDR显示档位"
        MCB_HDR显示档位.Padding = New Padding(10, 0, 10, 0)
        MCB_HDR显示档位.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_HDR显示档位.Size = New Size(220, 32)
        MCB_HDR显示档位.TabIndex = 0
        MCB_HDR显示档位.ToolTipGap = -1
        MCB_HDR显示档位.ToolTipMaxWidth = 350
        MCB_HDR显示档位.ToolTipPadding = New Padding(15)
        MCB_HDR显示档位.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' HtmlColorLabel1
        ' 
        HtmlColorLabel1.AutoSize = True
        HtmlColorLabel1.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel1.Dock = DockStyle.Top
        HtmlColorLabel1.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel1.Location = New Point(20, 20)
        HtmlColorLabel1.Margin = New Padding(2)
        HtmlColorLabel1.Name = "HtmlColorLabel1"
        HtmlColorLabel1.Size = New Size(702, 45)
        HtmlColorLabel1.TabIndex = 0
        HtmlColorLabel1.Text = "<span style=""font-size:13; color:Silver"">LakeUI HDR</span>   此 HDR 方案不同于视频和游戏，因此实际效果有所出入<br>不建议在没有玻璃背景的情况下使用 HDR，因为仅兼容 LakeUI 的控件"
        ' 
        ' Form_v6_设置_LakeUIHDR
        ' 
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ClientSize = New Size(742, 632)
        Controls.Add(ModernPanel1)
        Font = New Font("Microsoft YaHei UI", 10F)
        ForeColor = Color.Silver
        Name = "Form_v6_设置_LakeUIHDR"
        Text = "Form_v6_设置_LakeUIHDR"
        PanelHDR启用.ResumeLayout(False)
        ModernPanel1.ResumeLayout(False)
        ModernPanel1.PerformLayout()
        PanelHDR图片.ResumeLayout(False)
        PanelHDR矢量颜色.ResumeLayout(False)
        PanelHDR显示档位.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents ModernPanel1 As LakeUI.ModernPanel
    Friend WithEvents HtmlColorLabel1 As LakeUI.HtmlColorLabel
    Friend WithEvents PanelHDR启用 As Panel
    Friend WithEvents HtmlColorLabel启用 As LakeUI.HtmlColorLabel
    Friend WithEvents MCB_HDR启用 As LakeUI.ModernComboBox
    Friend WithEvents PanelHDR显示档位 As Panel
    Friend WithEvents HtmlColorLabel显示档位 As LakeUI.HtmlColorLabel
    Friend WithEvents MCB_HDR显示档位 As LakeUI.ModernComboBox
    Friend WithEvents PanelHDR矢量颜色 As Panel
    Friend WithEvents HtmlColorLabel矢量颜色 As LakeUI.HtmlColorLabel
    Friend WithEvents MCB_HDR矢量颜色 As LakeUI.ModernComboBox
    Friend WithEvents PanelHDR图片 As Panel
    Friend WithEvents HtmlColorLabel图片 As LakeUI.HtmlColorLabel
    Friend WithEvents MCB_HDR图片 As LakeUI.ModernComboBox
End Class
