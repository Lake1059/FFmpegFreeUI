<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form_v6_编码队列_任务日志
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
        ModernTextBox1 = New LakeUI.ModernTextBox()
        Panel1 = New Panel()
        ModernCheckBox1 = New LakeUI.ModernCheckBox()
        MB_复制当前视图 = New LakeUI.ModernButton()
        MCB_显示模式 = New LakeUI.ModernComboBox()
        Panel2 = New Panel()
        MB_任务性能计数器 = New LakeUI.ModernButton()
        ModernPanel1.SuspendLayout()
        Panel1.SuspendLayout()
        Panel2.SuspendLayout()
        SuspendLayout()
        ' 
        ' ModernPanel1
        ' 
        ModernPanel1.BackColor1 = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ModernPanel1.BorderSize = 0
        ModernPanel1.Controls.Add(ModernTextBox1)
        ModernPanel1.Controls.Add(Panel2)
        ModernPanel1.Controls.Add(Panel1)
        ModernPanel1.Dock = DockStyle.Fill
        ModernPanel1.Location = New Point(0, 0)
        ModernPanel1.Name = "ModernPanel1"
        ModernPanel1.Padding = New Padding(10)
        ModernPanel1.Size = New Size(634, 461)
        ModernPanel1.TabIndex = 0
        ' 
        ' ModernTextBox1
        ' 
        ModernTextBox1.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernTextBox1.BorderColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernTextBox1.BorderColorFocus = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernTextBox1.BorderRadius = 10
        ModernTextBox1.BorderSize = 0
        ModernTextBox1.Dock = DockStyle.Fill
        ModernTextBox1.Font = New Font("Microsoft YaHei UI", 11F)
        ModernTextBox1.LineHeight = 20
        ModernTextBox1.Location = New Point(10, 52)
        ModernTextBox1.Margin = New Padding(2)
        ModernTextBox1.MultiLine = True
        ModernTextBox1.Name = "ModernTextBox1"
        ModernTextBox1.Padding = New Padding(10)
        ModernTextBox1.ReadOnly = True
        ModernTextBox1.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernTextBox1.Size = New Size(614, 357)
        ModernTextBox1.TabIndex = 3
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(ModernCheckBox1)
        Panel1.Controls.Add(MB_复制当前视图)
        Panel1.Controls.Add(MCB_显示模式)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(10, 10)
        Panel1.Name = "Panel1"
        Panel1.Padding = New Padding(0, 0, 0, 10)
        Panel1.Size = New Size(614, 42)
        Panel1.TabIndex = 0
        ' 
        ' ModernCheckBox1
        ' 
        ModernCheckBox1.AutoSize = True
        ModernCheckBox1.BoxBorderRadius = 5
        ModernCheckBox1.BoxBorderSize = 0
        ModernCheckBox1.BoxCheckedBackColor = Color.OliveDrab
        ModernCheckBox1.BoxInnerPadding = 6
        ModernCheckBox1.BoxSize = 22
        ModernCheckBox1.BoxTextSpacing = 10
        ModernCheckBox1.Checked = True
        ModernCheckBox1.Dock = DockStyle.Fill
        ModernCheckBox1.Location = New Point(130, 0)
        ModernCheckBox1.Name = "ModernCheckBox1"
        ModernCheckBox1.Padding = New Padding(10, 0, 10, 0)
        ModernCheckBox1.Size = New Size(294, 32)
        ModernCheckBox1.TabIndex = 13
        ModernCheckBox1.Text = "自动滚动到底部"
        ' 
        ' MB_复制当前视图
        ' 
        MB_复制当前视图.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_复制当前视图.BorderRadius = 10
        MB_复制当前视图.BorderSize = 0
        MB_复制当前视图.Dock = DockStyle.Left
        MB_复制当前视图.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_复制当前视图.Location = New Point(0, 0)
        MB_复制当前视图.Margin = New Padding(2)
        MB_复制当前视图.Name = "MB_复制当前视图"
        MB_复制当前视图.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_复制当前视图.Size = New Size(130, 32)
        MB_复制当前视图.TabIndex = 3
        MB_复制当前视图.Text = "复制当前视图"
        ' 
        ' MCB_显示模式
        ' 
        MCB_显示模式.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_显示模式.BorderRadius = 10
        MCB_显示模式.BorderSize = 0
        MCB_显示模式.Dock = DockStyle.Right
        MCB_显示模式.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_显示模式.DropDownHoverAnimationDuration = 0
        MCB_显示模式.DropDownHoverColor = Color.FromArgb(CByte(80), CByte(0), CByte(0), CByte(0))
        MCB_显示模式.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_显示模式.DropDownPadding = New Padding(10)
        MCB_显示模式.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_显示模式.DropDownSelectedForeColor = Color.White
        MCB_显示模式.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_显示模式.Items.Add("全部输出")
        MCB_显示模式.Items.Add("最新输出（不含进度）")
        MCB_显示模式.Items.Add("仅错误信息")
        MCB_显示模式.Items.Add("当前阶段输出")
        MCB_显示模式.Location = New Point(424, 0)
        MCB_显示模式.Margin = New Padding(2, 2, 2, 2)
        MCB_显示模式.Name = "MCB_显示模式"
        MCB_显示模式.Padding = New Padding(10, 0, 10, 0)
        MCB_显示模式.Size = New Size(190, 32)
        MCB_显示模式.TabIndex = 1
        MCB_显示模式.ToolTipGap = -1
        MCB_显示模式.ToolTipMaxWidth = 350
        MCB_显示模式.ToolTipPadding = New Padding(15)
        MCB_显示模式.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' Panel2
        ' 
        Panel2.Controls.Add(MB_任务性能计数器)
        Panel2.Dock = DockStyle.Bottom
        Panel2.Location = New Point(10, 409)
        Panel2.Name = "Panel2"
        Panel2.Padding = New Padding(0, 10, 0, 0)
        Panel2.Size = New Size(614, 42)
        Panel2.TabIndex = 4
        ' 
        ' MB_任务性能计数器
        ' 
        MB_任务性能计数器.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_任务性能计数器.BorderRadius = 10
        MB_任务性能计数器.BorderSize = 0
        MB_任务性能计数器.Dock = DockStyle.Fill
        MB_任务性能计数器.Location = New Point(0, 10)
        MB_任务性能计数器.Margin = New Padding(2)
        MB_任务性能计数器.Name = "MB_任务性能计数器"
        MB_任务性能计数器.Size = New Size(614, 32)
        MB_任务性能计数器.TabIndex = 4
        MB_任务性能计数器.Text = ""
        ' 
        ' Form_v6_编码队列_任务日志
        ' 
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ClientSize = New Size(634, 461)
        Controls.Add(ModernPanel1)
        Font = New Font("Microsoft YaHei UI", 10F)
        ForeColor = Color.Silver
        MinimumSize = New Size(650, 500)
        Name = "Form_v6_编码队列_任务日志"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "编码队列任务日志"
        ModernPanel1.ResumeLayout(False)
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        Panel2.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents ModernPanel1 As LakeUI.ModernPanel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents MCB_显示模式 As LakeUI.ModernComboBox
    Friend WithEvents MB_复制当前视图 As LakeUI.ModernButton
    Friend WithEvents ModernTextBox1 As LakeUI.ModernTextBox
    Friend WithEvents ModernCheckBox1 As LakeUI.ModernCheckBox
    Friend WithEvents Panel2 As Panel
    Friend WithEvents MB_任务性能计数器 As LakeUI.ModernButton
End Class
