<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_v6_参数面板_自定义参数
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
        Dim ModernTab1 As LakeUI.ModernTabControl.ModernTab = New LakeUI.ModernTabControl.ModernTab()
        Dim ModernTab2 As LakeUI.ModernTabControl.ModernTab = New LakeUI.ModernTabControl.ModernTab()
        Dim ModernTab3 As LakeUI.ModernTabControl.ModernTab = New LakeUI.ModernTabControl.ModernTab()
        Dim ModernTab4 As LakeUI.ModernTabControl.ModernTab = New LakeUI.ModernTabControl.ModernTab()
        Dim ModernTab5 As LakeUI.ModernTabControl.ModernTab = New LakeUI.ModernTabControl.ModernTab()
        Dim ModernTab6 As LakeUI.ModernTabControl.ModernTab = New LakeUI.ModernTabControl.ModernTab()
        ModernTabControl1 = New LakeUI.ModernTabControl()
        ModernPanel1 = New LakeUI.ModernPanel()
        ModernPanel1.SuspendLayout()
        SuspendLayout()
        ' 
        ' ModernTabControl1
        ' 
        ModernTabControl1.ContentBackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ModernTabControl1.Dock = DockStyle.Fill
        ModernTabControl1.IndicatorHeight = 2
        ModernTabControl1.IndicatorPadding = 10
        ModernTab1.Text = "自定义参数说明"
        ModernTab2.IsSeparator = True
        ModernTab3.Text = "流自定义参数"
        ModernTab4.Text = "在位置插入参数"
        ModernTab5.IsSeparator = True
        ModernTab6.Text = "完全自己写"
        ModernTabControl1.Items.Add(ModernTab1)
        ModernTabControl1.Items.Add(ModernTab2)
        ModernTabControl1.Items.Add(ModernTab3)
        ModernTabControl1.Items.Add(ModernTab4)
        ModernTabControl1.Items.Add(ModernTab5)
        ModernTabControl1.Items.Add(ModernTab6)
        ModernTabControl1.Location = New Point(0, 0)
        ModernTabControl1.Name = "ModernTabControl1"
        ModernTabControl1.Size = New Size(835, 609)
        ModernTabControl1.TabAlignment = LakeUI.ModernTabControl.TabAlignmentEnum.Center
        ModernTabControl1.TabIndex = 1
        ModernTabControl1.TabItemHoverBackColor = Color.FromArgb(CByte(40), CByte(200), CByte(200), CByte(200))
        ModernTabControl1.TabItemSelectedBackColor = Color.FromArgb(CByte(80), CByte(200), CByte(200), CByte(200))
        ModernTabControl1.TabItemTextPadding = 20
        ModernTabControl1.TabStripBackColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        ModernTabControl1.TabStripHeight = 55
        ModernTabControl1.TabStripPadding = New Padding(10)
        ' 
        ' ModernPanel1
        ' 
        ModernPanel1.BackColor1 = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ModernPanel1.BorderSize = 0
        ModernPanel1.Controls.Add(ModernTabControl1)
        ModernPanel1.Dock = DockStyle.Fill
        ModernPanel1.Location = New Point(0, 0)
        ModernPanel1.Name = "ModernPanel1"
        ModernPanel1.ScrollBarMode = LakeUI.ModernPanel.ScrollMode.None
        ModernPanel1.Size = New Size(835, 609)
        ModernPanel1.TabIndex = 2
        ' 
        ' Form_v6_参数面板_自定义参数
        ' 
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ClientSize = New Size(835, 609)
        Controls.Add(ModernPanel1)
        Font = New Font("Microsoft YaHei UI", 10F)
        ForeColor = Color.Silver
        Name = "Form_v6_参数面板_自定义参数"
        Text = "Form_v6_参数面板_自定义参数"
        ModernPanel1.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents ModernTabControl1 As LakeUI.ModernTabControl
    Friend WithEvents ModernPanel1 As LakeUI.ModernPanel
End Class
