<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_v6_设置
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
        ModernTabListControl1 = New LakeUI.ModernTabListControl()
        SuspendLayout()
        ' 
        ' ModernTabListControl1
        ' 
        ModernTabListControl1.ContentBackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ModernTabListControl1.Dock = DockStyle.Fill
        ModernTabPage1.IsDescription = True
        ModernTabPage1.Text = "界面主框架 - 湖界"
        ModernTabPage2.Text = "LakeUI 性能选项"
        ModernTabPage3.Text = "LakeUI 视觉体验"
        ModernTabPage4.Text = "许可证"
        ModernTabPage5.IsSeparator = True
        ModernTabPage6.IsDescription = True
        ModernTabPage6.Text = "3FUI 配置选项"
        ModernTabPage7.Text = "界面显示"
        ModernTabPage8.Text = "性能调度"
        ModernTabPage9.Text = "功能设定"
        ModernTabPage10.Text = "转译辅助"
        ModernTabPage11.Text = "更新选项"
        ModernTabPage12.Text = "隐私设置"
        ModernTabPage13.Text = "远程调用"
        ModernTabPage14.IsSeparator = True
        ModernTabPage15.IsDescription = True
        ModernTabPage15.Text = "支持者内容包"
        ModernTabPage16.Text = "个性化"
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
        ModernTabListControl1.Location = New Point(0, 0)
        ModernTabListControl1.Name = "ModernTabListControl1"
        ModernTabListControl1.ScrollBarWidth = 8
        ModernTabListControl1.Size = New Size(904, 622)
        ModernTabListControl1.TabIndex = 2
        ModernTabListControl1.TabItemHeight = 32
        ModernTabListControl1.TabItemSpacing = 0
        ModernTabListControl1.TabStripBackColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        ModernTabListControl1.TabStripWidth = 200
        ' 
        ' Form_v6_设置
        ' 
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ClientSize = New Size(904, 622)
        Controls.Add(ModernTabListControl1)
        Font = New Font("Microsoft YaHei UI", 10F)
        ForeColor = Color.Silver
        Name = "Form_v6_设置"
        Text = "Form_v6_设置"
        ResumeLayout(False)
    End Sub

    Friend WithEvents ModernTabListControl1 As LakeUI.ModernTabListControl
End Class
