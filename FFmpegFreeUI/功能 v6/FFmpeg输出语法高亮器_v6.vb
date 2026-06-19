Imports System.Text.RegularExpressions
Imports LakeUI

Public Class FFmpeg输出语法高亮器_v6
    Implements ModernTextBox.ISyntaxHighlighter

    Public Shared ReadOnly 默认实例 As New FFmpeg输出语法高亮器_v6()

    Private Shared ReadOnly 颜色_错误 As Color = Color.FromArgb(232, 116, 116)
    Private Shared ReadOnly 颜色_警告 As Color = Color.FromArgb(215, 174, 101)
    Private Shared ReadOnly 颜色_系统 As Color = Color.FromArgb(132, 170, 232)
    Private Shared ReadOnly 颜色_输入 As Color = Color.FromArgb(142, 205, 156)
    Private Shared ReadOnly 颜色_输出 As Color = Color.FromArgb(224, 184, 108)
    Private Shared ReadOnly 颜色_流编号 As Color = Color.FromArgb(196, 159, 220)
    Private Shared ReadOnly 颜色_流类型 As Color = Color.FromArgb(238, 167, 124)
    Private Shared ReadOnly 颜色_键名 As Color = Color.FromArgb(124, 192, 190)
    Private Shared ReadOnly 颜色_数值 As Color = Color.FromArgb(140, 201, 236)
    Private Shared ReadOnly 颜色_格式 As Color = Color.FromArgb(177, 198, 135)
    Private Shared ReadOnly 颜色_路径 As Color = Color.FromArgb(198, 199, 205)
    Private Shared ReadOnly 颜色_选项 As Color = Color.FromArgb(176, 158, 225)

    Private Shared ReadOnly 进度键值正则 As New Regex("\b(frame|fps|q|size|time|bitrate|speed|dup|drop|total_size|out_time(?:_ms|_us)?|progress)\s*=\s*\S+", RegexOptions.Compiled Or RegexOptions.IgnoreCase)
    Private Shared ReadOnly 时间正则 As New Regex("\b\d{1,2}:\d{2}:\d{2}(?:\.\d+)?\b", RegexOptions.Compiled)
    Private Shared ReadOnly 输入输出标题正则 As New Regex("^\s*(Input|Output)\s+#\d+", RegexOptions.Compiled Or RegexOptions.IgnoreCase)
    Private Shared ReadOnly 流编号正则 As New Regex("\bStream\s+#\d+:\d+(?:\[[^\]]+\])?(?:\([^)]+\))?", RegexOptions.Compiled Or RegexOptions.IgnoreCase)
    Private Shared ReadOnly 流类型正则 As New Regex("\b(Video|Audio|Subtitle|Data|Attachment):", RegexOptions.Compiled Or RegexOptions.IgnoreCase)
    Private Shared ReadOnly 章节正则 As New Regex("\b(Chapter|Program)\s+#\d+(?::\d+)?", RegexOptions.Compiled Or RegexOptions.IgnoreCase)
    Private Shared ReadOnly 键名正则 As New Regex("^\s{2,}([A-Za-z0-9_. -]+?)\s*:", RegexOptions.Compiled)
    Private Shared ReadOnly 引号内容正则 As New Regex("'[^']*'|""[^""]*""", RegexOptions.Compiled)
    Private Shared ReadOnly 命令选项正则 As New Regex("(?<!\S)-{1,2}[A-Za-z0-9][A-Za-z0-9_.:-]*", RegexOptions.Compiled)
    Private Shared ReadOnly 分辨率正则 As New Regex("\b\d{2,5}x\d{2,5}\b(?:\s*\[[^\]]+\])?", RegexOptions.Compiled Or RegexOptions.IgnoreCase)
    Private Shared ReadOnly 数值单位正则 As New Regex("\b\d+(?:\.\d+)?\s*(?:k|m|g|t)?(?:b/s|bit/s|bits/s|hz|fps|tbr|tbn|tb|x|s|ms)\b|\b\d+(?:\.\d+)?\s*(?:k|m|g|t)?i?b\b", RegexOptions.Compiled Or RegexOptions.IgnoreCase)
    Private Shared ReadOnly 格式正则 As New Regex("\b(?:h264|hevc|av1|vp9|vp8|mpeg[124]?|aac|ac3|eac3|opus|vorbis|flac|mp3|pcm_[a-z0-9_]+|png|mjpeg|jpeg|webp|gif|ass|ssa|subrip|mov_text|yuvj?\d{3,4}p\d*|nv12|p010le|p016le|rgb24|bgr24|rgba|bgra|pal8|fltp|s16p?|s32p?|s64p?|u8p?|dblp?|mono|stereo|5\.1|7\.1|bt\d{3,4}|smpte\d{3}m?)\b(?:\([^)]*\))?", RegexOptions.Compiled Or RegexOptions.IgnoreCase)

    Public Function HighlightLine(lineIndex As Integer, lineText As String, previousLineState As Integer) As ModernTextBox.SyntaxHighlightResult Implements ModernTextBox.ISyntaxHighlighter.HighlightLine
        Dim tokens As New List(Of ModernTextBox.SyntaxToken)
        Dim text = If(lineText, "")
        If text.Length = 0 Then Return New ModernTextBox.SyntaxHighlightResult(tokens, 0)

        If 编码队列_v6.是否错误输出(text) Then
            tokens.Add(New ModernTextBox.SyntaxToken(0, text.Length, 颜色_错误))
            Return New ModernTextBox.SyntaxHighlightResult(tokens, 0)
        End If

        If text.Contains("warning", StringComparison.OrdinalIgnoreCase) OrElse text.Contains("deprecated", StringComparison.OrdinalIgnoreCase) Then
            tokens.Add(New ModernTextBox.SyntaxToken(0, text.Length, 颜色_警告))
            Return New ModernTextBox.SyntaxHighlightResult(tokens, 0)
        End If

        添加包含文本(tokens, text, "[3FUI]", 颜色_系统)

        For Each m As Match In 输入输出标题正则.Matches(text)
            添加片段(tokens, m.Index, m.Length, If(m.Value.TrimStart().StartsWith("Input", StringComparison.OrdinalIgnoreCase), 颜色_输入, 颜色_输出))
        Next
        添加包含文本(tokens, text, "Metadata:", 颜色_键名)
        添加包含文本(tokens, text, "Side data:", 颜色_键名)
        添加包含文本(tokens, text, "Stream mapping:", 颜色_键名)
        添加包含文本(tokens, text, "Press [q] to stop", 颜色_系统)
        添加包含文本(tokens, text, "configuration:", 颜色_键名)
        添加包含文本(tokens, text, "built with", 颜色_键名)

        添加正则匹配(tokens, text, 流编号正则, 颜色_流编号)
        添加正则匹配(tokens, text, 流类型正则, 颜色_流类型)
        添加正则匹配(tokens, text, 章节正则, 颜色_流编号)
        添加正则匹配(tokens, text, 键名正则, 颜色_键名, 1)
        添加正则匹配(tokens, text, 引号内容正则, 颜色_路径)
        添加正则匹配(tokens, text, 命令选项正则, 颜色_选项)

        If text.Contains("Duration:", StringComparison.OrdinalIgnoreCase) Then
            添加包含文本(tokens, text, "Duration:", 颜色_键名)
            添加包含文本(tokens, text, "start:", 颜色_键名)
            添加包含文本(tokens, text, "bitrate:", 颜色_键名)
            添加包含文本(tokens, text, "N/A", 颜色_警告)
            For Each m As Match In 时间正则.Matches(text)
                添加片段(tokens, m.Index, m.Length, 颜色_数值)
            Next
        End If

        For Each m As Match In 进度键值正则.Matches(text)
            添加片段(tokens, m.Index, m.Length, 颜色_数值)
        Next
        添加正则匹配(tokens, text, 分辨率正则, 颜色_数值)
        添加正则匹配(tokens, text, 数值单位正则, 颜色_数值)
        添加正则匹配(tokens, text, 格式正则, 颜色_格式)

        tokens.Sort(Function(a, b) a.StartCol.CompareTo(b.StartCol))
        Return New ModernTextBox.SyntaxHighlightResult(tokens, 0)
    End Function

    Private Shared Sub 添加包含文本(tokens As List(Of ModernTextBox.SyntaxToken), text As String, value As String, color As Color)
        Dim index = text.IndexOf(value, StringComparison.OrdinalIgnoreCase)
        Do While index >= 0
            添加片段(tokens, index, value.Length, color)
            index = text.IndexOf(value, index + value.Length, StringComparison.OrdinalIgnoreCase)
        Loop
    End Sub

    Private Shared Sub 添加正则匹配(tokens As List(Of ModernTextBox.SyntaxToken), text As String, regex As Regex, color As Color, Optional groupIndex As Integer = 0)
        For Each m As Match In regex.Matches(text)
            Dim g = If(groupIndex > 0 AndAlso groupIndex < m.Groups.Count, m.Groups(groupIndex), m.Groups(0))
            If g.Success Then 添加片段(tokens, g.Index, g.Length, color)
        Next
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
