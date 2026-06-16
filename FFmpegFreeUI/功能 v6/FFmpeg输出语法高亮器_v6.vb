Imports System.Text.RegularExpressions
Imports LakeUI

Public Class FFmpeg输出语法高亮器_v6
    Implements ModernTextBox.ISyntaxHighlighter

    Public Shared ReadOnly 默认实例 As New FFmpeg输出语法高亮器_v6()

    Private Shared ReadOnly 进度键值正则 As New Regex("\b(frame|fps|q|size|time|bitrate|speed)\s*=\s*\S+", RegexOptions.Compiled Or RegexOptions.IgnoreCase)
    Private Shared ReadOnly 时间正则 As New Regex("\b\d{1,2}:\d{2}:\d{2}(?:\.\d+)?\b", RegexOptions.Compiled)

    Public Function HighlightLine(lineIndex As Integer, lineText As String, previousLineState As Integer) As ModernTextBox.SyntaxHighlightResult Implements ModernTextBox.ISyntaxHighlighter.HighlightLine
        Dim tokens As New List(Of ModernTextBox.SyntaxToken)
        Dim text = If(lineText, "")
        If text.Length = 0 Then Return New ModernTextBox.SyntaxHighlightResult(tokens, 0)

        If 编码队列_v6.是否错误输出(text) Then
            tokens.Add(New ModernTextBox.SyntaxToken(0, text.Length, Color.IndianRed))
            Return New ModernTextBox.SyntaxHighlightResult(tokens, 0)
        End If

        If text.Contains("warning", StringComparison.OrdinalIgnoreCase) OrElse text.Contains("deprecated", StringComparison.OrdinalIgnoreCase) Then
            tokens.Add(New ModernTextBox.SyntaxToken(0, text.Length, Color.Goldenrod))
            Return New ModernTextBox.SyntaxHighlightResult(tokens, 0)
        End If

        添加包含文本(tokens, text, "[3FUI]", Color.CornflowerBlue)

        If text.Contains("Input #", StringComparison.OrdinalIgnoreCase) Then 添加整行(tokens, text, Color.LightGreen)
        If text.Contains("Output #", StringComparison.OrdinalIgnoreCase) Then 添加整行(tokens, text, Color.Goldenrod)
        If text.Contains("Stream #", StringComparison.OrdinalIgnoreCase) Then 添加整行(tokens, text, Color.Plum)
        If text.Contains("Duration:", StringComparison.OrdinalIgnoreCase) Then
            添加包含文本(tokens, text, "Duration:", Color.CornflowerBlue)
            For Each m As Match In 时间正则.Matches(text)
                添加片段(tokens, m.Index, m.Length, Color.CornflowerBlue)
            Next
        End If

        For Each m As Match In 进度键值正则.Matches(text)
            添加片段(tokens, m.Index, m.Length, Color.LightSkyBlue)
        Next

        tokens.Sort(Function(a, b) a.StartCol.CompareTo(b.StartCol))
        Return New ModernTextBox.SyntaxHighlightResult(tokens, 0)
    End Function

    Private Shared Sub 添加整行(tokens As List(Of ModernTextBox.SyntaxToken), text As String, color As Color)
        添加片段(tokens, 0, text.Length, color)
    End Sub

    Private Shared Sub 添加包含文本(tokens As List(Of ModernTextBox.SyntaxToken), text As String, value As String, color As Color)
        Dim index = text.IndexOf(value, StringComparison.OrdinalIgnoreCase)
        If index >= 0 Then 添加片段(tokens, index, value.Length, color)
    End Sub

    Private Shared Sub 添加片段(tokens As List(Of ModernTextBox.SyntaxToken), startCol As Integer, length As Integer, color As Color)
        If startCol < 0 OrElse length <= 0 Then Exit Sub
        Dim endCol = startCol + length
        For Each token In tokens
            Dim tokenEnd = token.StartCol + token.Length
            If token.StartCol < endCol AndAlso startCol < tokenEnd Then Exit Sub
        Next
        tokens.Add(New ModernTextBox.SyntaxToken(startCol, length, color))
    End Sub
End Class
