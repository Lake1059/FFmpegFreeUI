
Public Class 软件内对话框

    Public Shared Sub 显示对话框(标题 As String, 内容 As String, 按钮列表和程序 As Dictionary(Of String, Action), 对话框主题 As 主题类型, Optional 宽度比例 As Single = 0.5, Optional 高度比例 As Single = 0.5)
        Dim 主面板 As New Panel With {
            .Dock = DockStyle.Fill,
            .BackColor = Color.FromArgb(0, 0, 0),
            .BackgroundImageLayout = ImageLayout.None
        }
        Dim 背景图 As Image = 截取画面_对话框背景专用()
        AddHandler 主面板.Paint, Sub(sender As Object, e As PaintEventArgs) If 背景图 IsNot Nothing Then e.Graphics.DrawImage(背景图, 主面板.ClientRectangle)

        Dim 对话框区域 As New Panel With {.Dock = DockStyle.Fill, .Padding = New Padding(20 * Form1.DPI)}
        主面板.Controls.Add(对话框区域)
        AddHandler 主面板.SizeChanged, Sub() 主面板.Padding = New Padding(主面板.Width * 宽度比例 * 0.5, 主面板.Height * 高度比例 * 0.5, 主面板.Width * 宽度比例 * 0.5, 主面板.Height * 高度比例 * 0.5)
        Select Case 对话框主题
            Case 主题类型.常规 : 对话框区域.BackColor = Color.FromArgb(36, 36, 36)
            Case 主题类型.错误 : 对话框区域.BackColor = Color.FromArgb(64, 24, 24)
            Case 主题类型.放行 : 对话框区域.BackColor = Color.FromArgb(24, 64, 24)
        End Select

        Dim 标题文字 As New Label With {
            .Text = 标题,
            .Dock = DockStyle.Top,
            .AutoSize = True,
            .Padding = New Padding(0, 0, 0, 20),
            .Font = New Font(用户设置.实例对象.字体, 16),
            .ForeColor = Color.Silver
        }
        对话框区域.Controls.Add(标题文字)

        Dim 按钮容器 As New Panel With {.Dock = DockStyle.Bottom, .Height = 40 * Form1.DPI}

        Dim 按钮文本列表 As New List(Of String)(按钮列表和程序.Keys)
        For i As Integer = 0 To 按钮文本列表.Count - 1
            Dim 按钮 As New Sunny.UI.UIButton With {
                .Text = 按钮文本列表(i),
                .Font = New Font(用户设置.实例对象.字体, 11),
                .Width = TextRenderer.MeasureText(按钮文本列表(i), New Font(用户设置.实例对象.字体, 11)).Width + 30 * Form1.DPI,
                .Dock = DockStyle.Right,
                .Radius = 0,
                .FillColor = Color.FromArgb(48, 48, 48),
                .FillHoverColor = Color.FromArgb(56, 56, 56),
                .FillPressColor = Color.FromArgb(64, 64, 64),
                .FillSelectedColor = Color.FromArgb(64, 64, 64),
                .ForeColor = Color.Silver,
                .RectColor = Color.FromArgb(80, 80, 80),
                .RectHoverColor = Color.FromArgb(80, 80, 80),
                .RectPressColor = Color.FromArgb(80, 80, 80),
                .RectSelectedColor = Color.FromArgb(80, 80, 80),
                .RectSize = 1
            }
            AddHandler 按钮.Click, Sub(s1, e1) 按钮列表和程序(s1.Text)?()
            AddHandler 按钮.Click, Sub()
                                     主面板.Dispose()
                                     GC.Collect()
                                 End Sub
            按钮容器.Controls.Add(按钮)
            按钮.BringToFront()
            If i < 按钮文本列表.Count - 1 Then
                Dim 间隔标签 As New Label With {.Width = 10 * Form1.DPI, .Dock = DockStyle.Right}
                按钮容器.Controls.Add(间隔标签)
                间隔标签.BringToFront()
            End If
        Next
        对话框区域.Controls.Add(按钮容器)

        Dim 描述文字 As New Label With {
            .Text = 内容,
            .Dock = DockStyle.Fill,
            .AutoSize = False,
            .AutoEllipsis = True,
            .Padding = New Padding(2, 0, 0, 20),
            .Font = New Font(用户设置.实例对象.字体, 12),
            .ForeColor = Color.Silver
        }
        对话框区域.Controls.Add(描述文字)
        描述文字.BringToFront()
        Form1.Controls.Add(主面板)
        主面板.BringToFront()
    End Sub

    Enum 主题类型
        常规 = 1
        错误 = 2
        放行 = 5
    End Enum




End Class
