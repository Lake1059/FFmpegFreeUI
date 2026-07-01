<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_v6_编码队列_查看参数
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
        Panel2 = New Panel()
        MTB_命令行模板 = New LakeUI.ModernTextBox()
        Panel70 = New Panel()
        MB_复制命令行模板 = New LakeUI.ModernButton()
        Panel1 = New Panel()
        MTB_参数总览 = New LakeUI.ModernTextBox()
        Panel3 = New Panel()
        MB_复制参数总览 = New LakeUI.ModernButton()
        ModernPanel1.SuspendLayout()
        Panel2.SuspendLayout()
        Panel70.SuspendLayout()
        Panel1.SuspendLayout()
        Panel3.SuspendLayout()
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
        ModernPanel1.Size = New Size(742, 537)
        ModernPanel1.TabIndex = 3
        ' 
        ' Panel2
        ' 
        Panel2.Controls.Add(MTB_命令行模板)
        Panel2.Controls.Add(Panel70)
        Panel2.Dock = DockStyle.Fill
        Panel2.Location = New Point(370, 0)
        Panel2.Name = "Panel2"
        Panel2.Padding = New Padding(5, 10, 10, 10)
        Panel2.Size = New Size(372, 537)
        Panel2.TabIndex = 1
        ' 
        ' MTB_命令行模板
        ' 
        MTB_命令行模板.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_命令行模板.BorderColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_命令行模板.BorderColorFocus = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_命令行模板.BorderRadius = 10
        MTB_命令行模板.BorderSize = 0
        MTB_命令行模板.Dock = DockStyle.Fill
        MTB_命令行模板.Font = New Font("Microsoft YaHei UI", 11F)
        MTB_命令行模板.LineHeight = 20
        MTB_命令行模板.Location = New Point(5, 10)
        MTB_命令行模板.Margin = New Padding(2)
        MTB_命令行模板.MultiLine = True
        MTB_命令行模板.Name = "MTB_命令行模板"
        MTB_命令行模板.Padding = New Padding(12, 10, 12, 10)
        MTB_命令行模板.ReadOnly = True
        MTB_命令行模板.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_命令行模板.Size = New Size(357, 475)
        MTB_命令行模板.TabIndex = 3
        ' 
        ' Panel70
        ' 
        Panel70.Controls.Add(MB_复制命令行模板)
        Panel70.Dock = DockStyle.Bottom
        Panel70.Location = New Point(5, 485)
        Panel70.Name = "Panel70"
        Panel70.Padding = New Padding(0, 10, 0, 0)
        Panel70.Size = New Size(357, 42)
        Panel70.TabIndex = 39
        ' 
        ' MB_复制命令行模板
        ' 
        MB_复制命令行模板.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_复制命令行模板.BorderRadius = 10
        MB_复制命令行模板.BorderSize = 0
        MB_复制命令行模板.Dock = DockStyle.Fill
        MB_复制命令行模板.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_复制命令行模板.Location = New Point(0, 10)
        MB_复制命令行模板.Margin = New Padding(2)
        MB_复制命令行模板.Name = "MB_复制命令行模板"
        MB_复制命令行模板.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_复制命令行模板.Size = New Size(357, 32)
        MB_复制命令行模板.TabIndex = 3
        MB_复制命令行模板.Text = "复制实际命令行"
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(MTB_参数总览)
        Panel1.Controls.Add(Panel3)
        Panel1.Dock = DockStyle.Left
        Panel1.Location = New Point(0, 0)
        Panel1.Name = "Panel1"
        Panel1.Padding = New Padding(10, 10, 5, 10)
        Panel1.Size = New Size(370, 537)
        Panel1.TabIndex = 0
        ' 
        ' MTB_参数总览
        ' 
        MTB_参数总览.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_参数总览.BorderColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_参数总览.BorderColorFocus = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_参数总览.BorderRadius = 10
        MTB_参数总览.BorderSize = 0
        MTB_参数总览.Dock = DockStyle.Fill
        MTB_参数总览.Font = New Font("Microsoft YaHei UI", 11F)
        MTB_参数总览.LineHeight = 20
        MTB_参数总览.LineNumberBackColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_参数总览.LineNumberForeColor = Color.Silver
        MTB_参数总览.Location = New Point(10, 10)
        MTB_参数总览.Margin = New Padding(2)
        MTB_参数总览.MultiLine = True
        MTB_参数总览.Name = "MTB_参数总览"
        MTB_参数总览.Padding = New Padding(12, 10, 12, 10)
        MTB_参数总览.ReadOnly = True
        MTB_参数总览.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_参数总览.ShowLineNumbers = True
        MTB_参数总览.Size = New Size(355, 475)
        MTB_参数总览.TabIndex = 1
        ' 
        ' Panel3
        ' 
        Panel3.Controls.Add(MB_复制参数总览)
        Panel3.Dock = DockStyle.Bottom
        Panel3.Location = New Point(10, 485)
        Panel3.Name = "Panel3"
        Panel3.Padding = New Padding(0, 10, 0, 0)
        Panel3.Size = New Size(355, 42)
        Panel3.TabIndex = 40
        ' 
        ' MB_复制参数总览
        ' 
        MB_复制参数总览.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_复制参数总览.BorderRadius = 10
        MB_复制参数总览.BorderSize = 0
        MB_复制参数总览.Dock = DockStyle.Fill
        MB_复制参数总览.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_复制参数总览.Location = New Point(0, 10)
        MB_复制参数总览.Margin = New Padding(2)
        MB_复制参数总览.Name = "MB_复制参数总览"
        MB_复制参数总览.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_复制参数总览.Size = New Size(355, 32)
        MB_复制参数总览.TabIndex = 2
        MB_复制参数总览.Text = "复制参数总览"
        ' 
        ' Form_v6_编码队列_查看参数
        ' 
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ClientSize = New Size(742, 537)
        Controls.Add(ModernPanel1)
        Font = New Font("Microsoft YaHei UI", 10F)
        ForeColor = Color.Silver
        Name = "Form_v6_编码队列_查看参数"
        Text = "查看任务参数"
        ModernPanel1.ResumeLayout(False)
        Panel2.ResumeLayout(False)
        Panel70.ResumeLayout(False)
        Panel1.ResumeLayout(False)
        Panel3.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents ModernPanel1 As LakeUI.ModernPanel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents MTB_命令行模板 As LakeUI.ModernTextBox
    Friend WithEvents Panel70 As Panel
    Friend WithEvents MB_复制命令行模板 As LakeUI.ModernButton
    Friend WithEvents Panel1 As Panel
    Friend WithEvents MTB_参数总览 As LakeUI.ModernTextBox
    Friend WithEvents Panel3 As Panel
    Friend WithEvents MB_复制参数总览 As LakeUI.ModernButton
End Class
