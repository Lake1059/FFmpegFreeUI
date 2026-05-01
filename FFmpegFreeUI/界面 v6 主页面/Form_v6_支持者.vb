Public Class Form_v6_支持者

    Public Shared Property 付费支持者列表 As New List(Of String) From {
        "易相逢|#FBE4FF", "夜枫|#CCA4A3",
        "爱发电用户_Ck8g", "David King",
        "Daydreamer|#037DEC", "爱发电用户_217cb",
        "BAILING (学生)|#905BD9", "zhengjun638504@163.com",
        "尧泉", "FlyBalloon|#ADD8E6", "落叶清风|#6495ED"
    }

    Public Shared Property 赠送支持者列表 As New List(Of String) From {
        "格里芬指挥官|#39C5BB",
        "陆耀YSNX462 (FFBOX最严厉的父亲)|#66FF66",
        "Celery (酒吧点蛋炒饭的)|#21AEFF",
        "哈哈6662333 (坏点子大师/""网""管)|#FF9633",
        "哈基曼波|#FF96DE",
        "ZOGMOS (终末诗) (首席教程制作大师) (开发者特别授予)|#72565F|终末诗",
        "Uyanide (I use arch btw) (首席二次元)|#89B4FA",
        "Simlalsy (压片的)|#E3E0F9"
    }

    Sub 读取付费支持者()
        For Each t In 付费支持者列表
            Dim name = t.Split("|"c)(0)
            Dim color = If(t.Split("|"c).Length > 1, t.Split("|"c)(1), "")
            创建一个支持者标签(name, color)
        Next
    End Sub

    Sub 读取赠送支持者()
        For Each t In 赠送支持者列表
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
        Dim b As New LakeUI.ModernButton With {
            .Text = 文本,
            .AutoSize = False,
            .Height = 32 * Form1.DPI,
            .BorderRadius = 16,
            .BackColor = Color.Transparent,
            .BackColor1 = 背景颜色,
            .BorderColor = 背景颜色,
            .BorderSize = 1,
            .ForeColor = 文字颜色,
            .Font = New Font(Me.Font.Name, 10),
            .Margin = New Padding(0, 0, 15, 15),
            .Cursor = Cursors.Default
        }
        Select Case 特殊标记
            Case "终末诗"
                b.BorderColor = Color.White
                b.BorderSize = 2
        End Select
        根据文本设置按钮宽度(b, 30 * Form1.DPI)

        Me.FlowLayoutPanel1.Controls.Add(b)

    End Sub

    Sub 清空支持者列表()
        Me.FlowLayoutPanel1.Controls.Clear()
    End Sub

    Private Sub Form_v6_支持者_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub

    Private Sub ModernButton1_Click(sender As Object, e As EventArgs) Handles ModernButton1.Click
        清空支持者列表()
        读取付费支持者()
        读取赠送支持者()
    End Sub

    Private Sub ModernButton2_Click(sender As Object, e As EventArgs) Handles ModernButton2.Click
        清空支持者列表()
        读取付费支持者()
    End Sub

    Private Sub ModernButton3_Click(sender As Object, e As EventArgs) Handles ModernButton3.Click
        清空支持者列表()
        读取赠送支持者()
    End Sub

    Private Sub ModernButton4_Click(sender As Object, e As EventArgs) Handles ModernButton4.Click
        清空支持者列表()
    End Sub
End Class