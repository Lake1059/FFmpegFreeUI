Imports LakeUI

Public Class Form_v6_支持者

    Public Shared ReadOnly 付费支持者列表 As New List(Of String) From {
        "易相逢|#FBE4FF", "夜枫|#CCA4A3",
        "爱发电用户_Ck8g", "David King",
        "Daydreamer|#037DEC", "爱发电用户_217cb",
        "BAILING (学生)|#905BD9", "zhengjun638504@163.com",
        "尧泉", "FlyBalloon|#ADD8E6",
        "落叶清风|#6495ED|https://github.com/luoye-cpu/ffmpegPictureUI",
        "Xing|#135da7",
        "xrh0905||https://www.xrh0905.top",
        "L. Snow|#6028e0", "爱发电用户_b274c", "不安的乌鸦 (暗夜精灵德鲁伊)|#FF8C00", "小豆豆变毛豆",
        "Liu Zheng|#FFFFFF|https://www3.ntu.edu.sg/home/z.liu"
    }
    Public Shared ReadOnly 赠送支持者列表 As New List(Of String) From {
        "格里芬指挥官|#39C5BB",
        "陆耀YSNX462 (FFBOX最严厉的父亲)|#66FF66",
        "Celery (酒吧点蛋炒饭的)|#21AEFF",
        "哈哈6662333 (坏点子大师/""网""管)|#FF9633",
        "哈基曼波|#FF96DE",
        "ZOGMOS (终末诗) (首席教程制作大师) (开发者特别授予)|#72565F|https://zhuanlan.zhihu.com/p/1943079795341623993",
        "Uyanide (I use arch btw) (首席二次元)|#89B4FA",
        "Simlalsy (压片的)|#E3E0F9",
        "Dominic (AWJ神力)|#FF9D9F|https://github.com/Dominic485649/AWJimage"
    }

    Sub 读取付费支持者()
        For Each t In 付费支持者列表
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

    Sub 创建一个支持者标签(文本 As String, HTML颜色值文本 As String, Optional 站点 As String = "")
        Dim 背景颜色 As Color
        If HTML颜色值文本.StartsWith("#"c) Then
            背景颜色 = ColorTranslator.FromHtml(HTML颜色值文本)
            背景颜色 = Color.FromArgb(200, 背景颜色.R, 背景颜色.G, 背景颜色.B)
        Else
            背景颜色 = Color.FromArgb(160, 255, 255, 255)
        End If
        Dim 背景色亮度 As Double = 背景颜色.R * 0.299 + 背景颜色.G * 0.587 + 背景颜色.B * 0.114
        Dim 文字颜色 As Color = If(背景色亮度 >= 128, Color.Black, Color.Silver)
        Dim a As New MemberWall.MemberItem With {.Text = 文本, .BackColor = 背景颜色, .ForeColor = 文字颜色}
        If 文本.Contains("ZOGMOS") Then
            a.BorderColor = Color.FromArgb(200, 255, 255, 255)
            a.BorderSize = 2
        End If
        If 站点 <> "" Then
            If 站点.StartsWith("http") Then
                a.ClickAction = Sub() Process.Start(New ProcessStartInfo With {.FileName = 站点, .UseShellExecute = True})
            Else
                a.ClickAction = Sub() ExOverlayMsgBox(FormMain_v6, 站点)
            End If
        End If
        Me.MemberWall1.Items.Add(a)
    End Sub

    Sub 清空支持者列表()
        Me.MemberWall1.Items.Clear()
    End Sub

    Private Sub Form_v6_支持者_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub

    Private Sub ModernButton1_Click(sender As Object, e As EventArgs) Handles ModernButton1.Click
        清空支持者列表()
        读取付费支持者()
        读取赠送支持者()
        Me.MemberWall1.Redraw()
    End Sub

    Private Sub ModernButton2_Click(sender As Object, e As EventArgs) Handles ModernButton2.Click
        清空支持者列表()
        读取付费支持者()
        Me.MemberWall1.Redraw()
    End Sub

    Private Sub ModernButton3_Click(sender As Object, e As EventArgs) Handles ModernButton3.Click
        清空支持者列表()
        读取赠送支持者()
        Me.MemberWall1.Redraw()
    End Sub

    Private Sub ModernButton4_Click(sender As Object, e As EventArgs) Handles ModernButton4.Click
        清空支持者列表()
        Me.MemberWall1.Redraw()
    End Sub

    Private Sub ModernButton5_Click(sender As Object, e As EventArgs) Handles ModernButton5.Click
        Me.MemberWall1.Search(ModernTextBox1.Text)
    End Sub

    Private Sub ModernTextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles ModernTextBox1.KeyDown
        If e.KeyData = Keys.Enter Then Me.MemberWall1.Search(ModernTextBox1.Text)
    End Sub
End Class