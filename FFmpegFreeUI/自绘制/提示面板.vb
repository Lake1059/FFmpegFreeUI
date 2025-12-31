Imports Sunny.UI

Public Class 提示面板

    Public Shared Sub 绑定(作用控件 As Control, 数据 As List(Of Tp_Data))
        Dim Menu As New 暗黑上下文菜单 With {.ShowImageMargin = False}
        Menu.Items.Add(New ToolStripSeparator() With {.Tag = "null"})
        For Each item In 数据
            If Menu.Items.Count > 1 Then Menu.Items.Add(New ToolStripSeparator())
            If item.Title <> "" Then
                Dim 标题项 As New ToolStripMenuItem(item.Title) With {.Enabled = False, .Tag = "label", .ForeColor = item.TitleColor}
                Menu.Items.Add(标题项)
            End If
            Dim a1 As New List(Of String)(item.Text.Split("<br>"))
            For Each t In a1
                Dim 内容项 As New ToolStripMenuItem(t) With {.Tag = "label"}
                Select Case item.TextType
                    Case 提示项类型.警告 : 内容项.ForeColor = Color.Goldenrod
                    Case 提示项类型.严重 : 内容项.ForeColor = Color.IndianRed
                End Select
                Menu.Items.Add(内容项)
            Next
        Next
        Menu.Items.Add(New ToolStripSeparator() With {.Tag = "null"})

        AddHandler 作用控件.MouseClick, Sub(s1, e1)
                                        Select Case e1.Button
                                            Case MouseButtons.Middle, MouseButtons.XButton1, MouseButtons.XButton2
                                                Menu.Font = Form1.Font
                                                Menu.Show(Control.MousePosition)
                                        End Select
                                    End Sub
        AddHandler 作用控件.KeyDown, Sub(s1, e1)
                                     Select Case e1.KeyCode
                                         Case Keys.F1
                                             Menu.Font = Form1.Font
                                             Menu.Show(Control.MousePosition)
                                     End Select
                                 End Sub

        Dim c1 As UIComboBox = TryCast(作用控件, UIComboBox)
        If c1 IsNot Nothing Then
            AddHandler c1.TextBox.MouseDown, Sub(s2, e1)
                                                 Select Case e1.Button
                                                     Case MouseButtons.Middle, MouseButtons.XButton1, MouseButtons.XButton2
                                                         Menu.Font = Form1.Font
                                                         Menu.Show(Control.MousePosition)
                                                 End Select
                                             End Sub
            AddHandler c1.TextBox.KeyDown, Sub(s1, e1)
                                               Select Case e1.KeyCode
                                                   Case Keys.F1
                                                       Menu.Font = Form1.Font
                                                       Menu.Show(Control.MousePosition)
                                               End Select
                                           End Sub
        End If

        Dim c2 As UITextBox = TryCast(作用控件, UITextBox)
        If c2 IsNot Nothing Then
            AddHandler c2.TextBox.MouseDown, Sub(s2, e1)
                                                 Select Case e1.Button
                                                     Case MouseButtons.Middle, MouseButtons.XButton1, MouseButtons.XButton2
                                                         Menu.Font = Form1.Font
                                                         Menu.Show(Control.MousePosition)
                                                 End Select
                                             End Sub
            AddHandler c2.TextBox.KeyDown, Sub(s1, e1)
                                               Select Case e1.KeyCode
                                                   Case Keys.F1
                                                       Menu.Font = Form1.Font
                                                       Menu.Show(Control.MousePosition)
                                               End Select
                                           End Sub
        End If

    End Sub

    Public Class Tp_Data
        Public Property Title As String
        Public Property TitleColor As Color = Color.YellowGreen
        Public Property Text As String
        Public Property TextType As 提示项类型
    End Class

    Enum 提示项类型
        警告 = 1
        严重 = 2
    End Enum

End Class
