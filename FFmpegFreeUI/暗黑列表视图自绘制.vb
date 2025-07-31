
Public Class 暗黑列表视图自绘制

    Public Shared Sub 绑定列表视图事件(哪个列表视图控件 As ListView)
        哪个列表视图控件.DoubleBuffer
        AddHandler 哪个列表视图控件.DrawSubItem, Sub(sender, e) 绘制子项(sender, e)
        AddHandler 哪个列表视图控件.SelectedIndexChanged, Sub(sender, e) sender.Invalidate(sender.ClientRectangle)
        AddHandler 哪个列表视图控件.ItemMouseHover, Sub(sender, e) sender.Invalidate(sender.ClientRectangle)
    End Sub

    Public Shared Property 项被选中时的背景颜色 As Color = ColorTranslator.FromWin32(RGB(48, 48, 48))
    Public Shared Property 项被选中时的高亮遮罩颜色 As Color = Color.FromArgb(60, 200, 200, 200)

    ''' <summary>
    ''' 绑定此事件时不要绑定 DrawListViewItemEvent 事件，本事件直接绘制主项
    ''' </summary>
    ''' <param name="哪个列表视图控件"></param>
    ''' <param name="e"></param>
    Public Shared Sub 绘制子项(哪个列表视图控件 As ListView, e As DrawListViewSubItemEventArgs)
        Try
            Form1.重新创建句柄()
            If Not e.Bounds.IntersectsWith(哪个列表视图控件.ClientRectangle) OrElse e.Bounds.Width = 0 Then Exit Sub
            Dim 项背景色 As Color = If(哪个列表视图控件.SelectedIndices.Contains(e.ItemIndex), 项被选中时的背景颜色, 哪个列表视图控件.BackColor)
            Dim 文本高度修正 As Integer = (e.Bounds.Height - TextRenderer.MeasureText(e.SubItem.Text, e.SubItem.Font).Height) \ 2
            Dim 文本绘制区 As New Rectangle(e.Bounds.X + 5 * Form1.DPI, e.Bounds.Y + 文本高度修正, e.Bounds.Width, e.Bounds.Height)
            Dim 文字显示所需尺寸 As Size = TextRenderer.MeasureText(e.SubItem.Text, e.SubItem.Font)
            Dim 实际要绘制的文本 As String = e.SubItem.Text
            If 文字显示所需尺寸.Width > (e.Bounds.Width - 3 * Form1.DPI) Then
                Dim 点号所占用的宽度 As Integer = TextRenderer.MeasureText("...", e.SubItem.Font).Width
                Dim 实际文本可用宽度 As Integer = e.Bounds.Width - 点号所占用的宽度
                While TextRenderer.MeasureText(实际要绘制的文本, e.SubItem.Font).Width > 实际文本可用宽度 AndAlso 实际要绘制的文本.Length > 0
                    实际要绘制的文本 = 实际要绘制的文本.Substring(0, 实际要绘制的文本.Length - 1)
                End While
                实际要绘制的文本 &= "..."
            End If
            Using brush As New SolidBrush(项背景色)
                e.Graphics.FillRectangle(brush, e.Bounds)
            End Using
            TextRenderer.DrawText(e.Graphics, 实际要绘制的文本.Replace("&", "&&"), e.SubItem.Font, 文本绘制区, e.Item.ForeColor, 项背景色, TextFormatFlags.Default)
        Catch ex As Exception
        End Try
    End Sub

End Class
