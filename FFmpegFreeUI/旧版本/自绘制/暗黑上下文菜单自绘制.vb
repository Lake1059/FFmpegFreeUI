
Public Class 暗黑上下文菜单自绘制
    Inherits ToolStripRenderer

    Protected Overrides Sub Initialize(toolStrip As ToolStrip)
        MyBase.Initialize(toolStrip)
        toolStrip.BackColor = ColorTranslator.FromWin32(RGB(36, 36, 36))
        toolStrip.ForeColor = Color.Silver

    End Sub

    Protected Overrides Sub InitializeItem(item As ToolStripItem)
        MyBase.InitializeItem(item)
        Select Case item.Tag
            Case "label"
                item.Padding = New Padding(0)
            Case Else
                item.Padding = New Padding(5 * Form1.DPI, 3 * Form1.DPI, 5 * Form1.DPI, 3 * Form1.DPI)
        End Select
        item.Margin = New Padding(0)
        'If item.GetType() = GetType(ToolStripSeparator) Then
        'End If
    End Sub

    Protected Overrides Sub OnRenderToolStripBackground(e As ToolStripRenderEventArgs)
        Dim g = e.Graphics
        Using b As New SolidBrush(ColorTranslator.FromWin32(RGB(36, 36, 36)))
            g.FillRectangle(b, e.AffectedBounds)
        End Using
        Using p As New Pen(ColorTranslator.FromWin32(RGB(80, 80, 80)), 2)
            g.DrawRectangle(p, e.AffectedBounds)
        End Using
        Using p As New Pen(ColorTranslator.FromWin32(RGB(80, 80, 80)), 2)
            g.DrawRectangle(p, e.AffectedBounds.Left + 1, e.AffectedBounds.Top + 1, e.AffectedBounds.Width - 2, e.AffectedBounds.Height - 2)
        End Using
    End Sub

    Protected Overrides Sub OnRenderImageMargin(e As ToolStripRenderEventArgs)
        Dim g = e.Graphics
        Dim rect As New Rectangle(0, 0, e.ToolStrip.Width - 1, e.ToolStrip.Height - 1)
        Using p As New Pen(ColorTranslator.FromWin32(RGB(80, 80, 80)), 2)
            g.DrawRectangle(p, rect)
        End Using
    End Sub

    Protected Overrides Sub OnRenderSeparator(e As ToolStripSeparatorRenderEventArgs)
        Select Case e.Item.Tag
            Case "nothing", "null"
                e.Item.AutoSize = False
                e.Item.Margin = New Padding(0)
                e.Item.Padding = New Padding(0)
                e.Item.Height = 7 * Form1.DPI
                Exit Sub
            Case Else
                e.Item.AutoSize = False
                e.Item.Height = 2
                e.Item.Margin = New Padding(0, 5 * Form1.DPI, 0, 5 * Form1.DPI)
                e.Item.Padding = New Padding(0)
        End Select
        Dim g = e.Graphics
        Using b As New SolidBrush(ColorTranslator.FromWin32(RGB(80, 80, 80)))
            g.FillRectangle(b, 0, 0, e.Item.Width, e.Item.Height)
        End Using
    End Sub

    Protected Overrides Sub OnRenderArrow(e As ToolStripArrowRenderEventArgs)
        e.ArrowColor = Color.Silver
        e.ArrowRectangle = New Rectangle(New Point(e.ArrowRectangle.Left, e.ArrowRectangle.Top - 1), e.ArrowRectangle.Size)
        MyBase.OnRenderArrow(e)
    End Sub

    Protected Overrides Sub OnRenderMenuItemBackground(e As ToolStripItemRenderEventArgs)
        Dim g = e.Graphics
        If e.Item.Enabled Then
            Select Case e.Item.Tag
                Case "label"
                Case Else
                    If e.Item.Selected Then
                        Using b As New SolidBrush(ColorTranslator.FromWin32(RGB(64, 64, 64)))
                            g.FillRectangle(b, e.Item.ContentRectangle.Left + 5 * Form1.DPI - 1, e.Item.ContentRectangle.Top, e.Item.ContentRectangle.Width - 18 * Form1.DPI, e.Item.ContentRectangle.Height)
                        End Using
                    End If
            End Select

        End If
    End Sub

    'Private ReadOnly ShortcutKeys As New List(Of String) From {
    '    "Ctrl+", "Alt+", "Shift+", "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "F10", "F11", "F12"
    '}

    Protected Overrides Sub OnRenderItemText(e As ToolStripItemTextRenderEventArgs)
        Dim g = e.Graphics
        'Dim isShortcutKeyText As Boolean = ShortcutKeys.Any(Function(key) e.Text.Contains(key))
        If e.Item.Enabled Then
            Using b As New SolidBrush(e.TextColor)
                'If isShortcutKeyText Then
                '    TextRenderer.DrawText(e.Graphics, e.Text.Replace("&", "&&"), e.TextFont, New Rectangle(e.TextRectangle.Left, e.Item.ContentRectangle.Top, e.TextRectangle.Width, e.Item.ContentRectangle.Height), e.TextColor, Nothing, TextFormatFlags.Right Or TextFormatFlags.VerticalCenter)
                'Else
                TextRenderer.DrawText(e.Graphics, e.Text.Replace("&", "&&"), e.TextFont, New Rectangle(e.TextRectangle.Left, e.Item.ContentRectangle.Top, e.TextRectangle.Width, e.Item.ContentRectangle.Height), e.TextColor, Nothing, TextFormatFlags.VerticalCenter)
                'End If
            End Using
        Else
            e.Item.Margin = New Padding(0)
            e.Item.Padding = New Padding(0)
            Select Case e.Item.Tag
                Case "label"
                    Using b As New SolidBrush(e.TextColor)
                        TextRenderer.DrawText(e.Graphics, e.Text.Replace("&", "&&"), New Font(e.TextFont.Name, e.TextFont.Size, FontStyle.Regular), New Rectangle(e.TextRectangle.Left, e.Item.ContentRectangle.Top, e.TextRectangle.Width, e.Item.ContentRectangle.Height), e.TextColor, Nothing, TextFormatFlags.VerticalCenter)
                    End Using
                Case Else
                    Using b As New SolidBrush(e.TextColor)
                        TextRenderer.DrawText(e.Graphics, e.Text.Replace("&", "&&"), New Font(e.TextFont.Name, e.TextFont.Size - 2, FontStyle.Regular), New Rectangle(e.TextRectangle.Left, e.Item.ContentRectangle.Top, e.TextRectangle.Width, e.Item.ContentRectangle.Height), e.TextColor, Nothing, TextFormatFlags.VerticalCenter)
                    End Using
            End Select
        End If
    End Sub


End Class
