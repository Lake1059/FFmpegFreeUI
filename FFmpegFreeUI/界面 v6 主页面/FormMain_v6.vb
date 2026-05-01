Public Class FormMain_v6
    Private Sub FormMain_v6_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.ThisIsYourWindow1.Attach(Me)
        Me.ModernTabListControl1.Focus()
        Me.ModernTabListControl1.Items(1).BoundControl = Form_v6_起始页面
        Form_v6_起始页面.ModernPanel1.BackgroundSource = Me
        Me.ModernTabListControl1.Items(2).BoundControl = Form_v6_编码队列
        Me.ModernTabListControl1.Items(5).BoundControl = Form_v6_参数面板

        Me.ModernTabListControl1.Items(14).BoundControl = Form_v6_性能监控

        Me.ModernTabListControl1.Items(16).BoundControl = Form_v6_设置
        Me.ModernTabListControl1.Items(17).BoundControl = Form_v6_支持者
        Me.ModernTabListControl1.SelectedIndex = 1
        Me.ModernTextBox1.Parent = Me.ModernTabListControl1
    End Sub

    Private Sub FormMain_v6_Shown(sender As Object, e As EventArgs) Handles Me.Shown





    End Sub


    Private Sub ModernTabListControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ModernTabListControl1.SelectedIndexChanged
        Select Case ModernTabListControl1.SelectedIndex
            Case 1 : 启用透明背景模式()
            Case Else : 禁用透明背景模式()
        End Select
        Select Case ModernTabListControl1.SelectedIndex
            Case 14 : Form_v6_性能监控.开始()
            Case Else : Form_v6_性能监控.停止()
        End Select
    End Sub

    Sub 启用透明背景模式()
        ModernTabListControl1.TabStripBackColor = Color.Transparent
        ModernTabListControl1.ContentBackColor = Color.Transparent
        ModernTextBox1.BackColor1 = Color.FromArgb(80, 220, 220, 220)
    End Sub

    Sub 禁用透明背景模式()
        ModernTabListControl1.TabStripBackColor = Color.FromArgb(48, 48, 48)
        ModernTabListControl1.ContentBackColor = Color.FromArgb(48, 48, 48)
        ModernTextBox1.BackColor1 = Color.FromArgb(40, 220, 220, 220)
    End Sub

End Class