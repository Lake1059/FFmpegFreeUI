Imports Sunny.UI

Public Class 界面_支持者
    Private Sub 界面_支持者_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Sub 读取付费支持者()
        Dim a As New List(Of String)(My.Resources.Resource1.支持者名单.Split(vbCrLf))
        For Each t In a
            Dim name = t.Split("|"c)(0)
            Dim color = If(t.Split("|"c).Length > 1, t.Split("|"c)(1), "")
            创建一个支持者标签(name, color)
        Next
    End Sub

    Sub 读取赠送支持者()
        Dim a As New List(Of String)(My.Resources.Resource1.支持者名单_赠送.Split(vbCrLf))
        For Each t In a
            Dim data = t.Split("|"c)
            Dim name = data(0)
            Dim color = If(data.Length > 1, data(1), "")
            If data.Length <= 2 Then
                创建一个支持者标签(name, color)
            ElseIf data.Length = 3 Then
                创建一个支持者标签(name, color, data(2))
            End If
        Next
    End Sub

    Sub 创建一个支持者标签(文本 As String, HTML颜色值文本 As String, Optional 特殊标记 As String = "")
        Dim 背景颜色 As Color
        If HTML颜色值文本.StartsWith("#"c) Then
            背景颜色 = ColorTranslator.FromHtml(HTML颜色值文本)
        Else
            背景颜色 = ColorTranslator.FromWin32(RGB(56, 56, 56))
        End If
        Dim 背景色亮度 As Double = 背景颜色.R * 0.299 + 背景颜色.G * 0.587 + 背景颜色.B * 0.114
        Dim 文字颜色 As Color = If(背景色亮度 >= 128, Color.Black, Color.Silver)
        Dim b As New UIButton With {
            .Text = 文本,
            .AutoSize = False,
            .Height = 30 * Form1.DPI,
            .Radius = 30 * Form1.DPI,
            .FillColor = 背景颜色,
            .FillHoverColor = 背景颜色,
            .FillPressColor = 背景颜色,
            .RectColor = 背景颜色,
            .RectHoverColor = 背景颜色,
            .RectPressColor = 背景颜色,
            .ForeColor = 文字颜色,
            .ForeHoverColor = 文字颜色,
            .ForePressColor = 文字颜色,
            .Font = New Font(用户设置.实例对象.字体, 10),
            .Margin = New Padding(0, 0, 15, 15),
            .Cursor = Cursors.Default
        }
        Select Case 特殊标记
            Case "终末诗"
                b.RectHoverColor = Color.Yellow
        End Select

        Me.FlowLayoutPanel1.Controls.Add(b)
        根据文本设置按钮宽度(b, False, 24 * Form1.DPI)
    End Sub

    Sub 清空支持者列表()
        Me.FlowLayoutPanel1.Controls.Clear()
    End Sub


    Private Sub UiButton1_Click(sender As Object, e As EventArgs) Handles UiButton1.Click
        UiButton1.FillColor = Color.DarkSlateBlue
        UiButton2.FillColor = Me.BackColor
        UiButton3.FillColor = Me.BackColor
        清空支持者列表()
        读取付费支持者()
        读取赠送支持者()
    End Sub

    Private Sub UiButton2_Click(sender As Object, e As EventArgs) Handles UiButton2.Click
        UiButton1.FillColor = Me.BackColor
        UiButton2.FillColor = Color.DarkSlateBlue
        UiButton3.FillColor = Me.BackColor
        清空支持者列表()
        读取付费支持者()
    End Sub

    Private Sub UiButton3_Click(sender As Object, e As EventArgs) Handles UiButton3.Click
        UiButton1.FillColor = Me.BackColor
        UiButton2.FillColor = Me.BackColor
        UiButton3.FillColor = Color.DarkSlateBlue
        清空支持者列表()
        读取赠送支持者()
    End Sub

    Private Sub 界面_支持者_DpiChangedAfterParent(sender As Object, e As EventArgs) Handles Me.DpiChangedAfterParent
        调整界面()
    End Sub

    Sub 调整界面()
        UiButton1.Radius = 30 * Form1.DPI
        UiButton2.Radius = 30 * Form1.DPI
        UiButton3.Radius = 30 * Form1.DPI
        For Each b As UIButton In FlowLayoutPanel1.Controls.OfType(Of UIButton)()
            根据文本设置按钮宽度(b, False, 20 * Form1.DPI)
        Next
    End Sub

    Sub 调整顶部按钮宽度()
        根据文本设置按钮宽度(UiButton1)
        根据文本设置按钮宽度(UiButton2)
        根据文本设置按钮宽度(UiButton3)
    End Sub

End Class
