Imports System.Globalization
Imports System.IO
Imports System.Text
Imports System.Text.Json
Imports System.Text.RegularExpressions
Imports LakeUI

Partial Public Class 预设管理_v6

    Private Shared Function 生成剪辑参数(a As 预设数据_v6, 阶段 As 预设数据_v6.命令行阶段, 媒体总时长 As String, 结果 As 预设数据_v6.命令行生成结果) As 剪辑参数片段
        Dim clip As New 剪辑参数片段
        Dim 入点 = If(a.剪辑区间_入点, "").Trim()
        Dim 出点 = If(a.剪辑区间_出点, "").Trim()
        If 入点 = "" AndAlso 出点 = "" Then Return clip

        Select Case a.剪辑区间_方法
            Case 预设数据_v6.剪辑方法.粗剪
                clip.输入前 = 生成常规剪辑参数(入点, 出点)
            Case 预设数据_v6.剪辑方法.精剪从头解码
                clip.输出前 = 生成常规剪辑参数(入点, 出点)
            Case 预设数据_v6.剪辑方法.精剪空降解码
                生成空降精剪参数(a, 入点, 出点, clip)
            Case 预设数据_v6.剪辑方法.掐头去尾
                结果.需要媒体总时长 = True
                clip.输入前 = 生成掐头去尾参数(入点, 出点, 媒体总时长, 结果)
            Case 预设数据_v6.剪辑方法.剔除中间
                结果.需要媒体总时长 = True
        End Select

        Return clip
    End Function

    Private Shared Function 生成常规剪辑参数(入点 As String, 出点 As String) As String
        Dim parts As New List(Of String)
        If 入点 <> "" Then parts.Add("-ss " & 入点)
        If 出点 <> "" Then parts.Add("-to " & 出点)
        Return String.Join(" ", parts)
    End Function

    Private Shared Sub 生成空降精剪参数(a As 预设数据_v6, 入点 As String, 出点 As String, clip As 剪辑参数片段)
        Dim startSeconds As Double
        Dim decodeBackSeconds As Double
        If 入点 <> "" AndAlso TryParseTimeSeconds(入点, startSeconds) AndAlso
           TryParseTimeSeconds(If(a.剪辑区间_向前解码多久秒, "").Trim(), decodeBackSeconds) AndAlso
           decodeBackSeconds > 0 Then
            Dim inputSeek = Math.Max(0, startSeconds - decodeBackSeconds)
            Dim outputStart = startSeconds - inputSeek
            Dim outputEnd As Double

            clip.输入前 = "-ss " & FormatSeconds(inputSeek)
            Dim outputParts As New List(Of String) From {"-ss " & FormatSeconds(outputStart)}
            If 出点 <> "" AndAlso TryParseTimeSeconds(出点, outputEnd) Then
                outputParts.Add("-to " & FormatSeconds(Math.Max(0, outputEnd - inputSeek)))
            ElseIf 出点 <> "" Then
                outputParts.Add("-to " & 出点)
            End If
            clip.输出前 = String.Join(" ", outputParts)
            Exit Sub
        End If

        clip.输入前 = 生成常规剪辑参数(入点, 出点)
    End Sub

    Private Shared Function 生成掐头去尾参数(入点 As String, 出点 As String, 媒体总时长 As String, 结果 As 预设数据_v6.命令行生成结果) As String
        Dim parts As New List(Of String)
        If 入点 <> "" Then parts.Add("-ss " & 入点)

        Dim startSeconds As Double
        Dim endSeconds As Double
        If 出点 <> "" AndAlso TryParseTimeSeconds(出点, endSeconds) Then
            If 入点 <> "" AndAlso TryParseTimeSeconds(入点, startSeconds) Then
                parts.Add("-t " & FormatSeconds(Math.Max(0, endSeconds - startSeconds)))
            Else
                parts.Add("-t " & FormatSeconds(Math.Max(0, endSeconds)))
            End If
        ElseIf 出点 <> "" Then
            parts.Add("-t " & 出点)
        End If
        Return String.Join(" ", parts)
    End Function

    Private Shared Function TryParseTimeSeconds(value As String, ByRef seconds As Double) As Boolean
        seconds = 0
        Dim raw = If(value, "").Trim()
        If raw = "" OrElse raw.StartsWith("<"c) Then Return False

        Dim t As TimeSpan
        If TimeSpan.TryParse(raw, CultureInfo.InvariantCulture, t) Then
            seconds = t.TotalSeconds
            Return True
        End If

        Return Double.TryParse(raw, NumberStyles.Float, CultureInfo.InvariantCulture, seconds)
    End Function

    Private Shared Function FormatSeconds(seconds As Double) As String
        Return seconds.ToString("0.######", CultureInfo.InvariantCulture)
    End Function

    Private Shared Sub AddRaw(parts As List(Of String), value As String)
        If Not String.IsNullOrWhiteSpace(value) Then parts.Add(规范命令行换行(value))
    End Sub

    Private Shared Function 应用自定义参数通配字符串(value As String, 输入文件 As String, 输出文件 As String) As String
        If value Is Nothing Then Return ""

        Dim inputPath = 应用转译模式路径(If(输入文件, ""))
        Dim outputPath = 应用转译模式路径(If(输出文件, ""))
        Dim inputIsPlaceholder = 是输入文件占位符(inputPath)
        Dim inputWithoutExtension = If(inputIsPlaceholder, "<InputFileWithOutExtension>", 获取路径不含扩展名保持分隔符(inputPath))
        Dim inputPathOnly = If(inputIsPlaceholder, "<InputFilePath>", 获取路径目录保持分隔符(inputPath))
        Dim inputFileName = If(inputIsPlaceholder, "<InputFileName>", 获取路径文件名保持分隔符(inputPath))
        Dim inputFileNameWithoutExtension = If(inputIsPlaceholder, "<InputFileNameWithOutExtension>", 获取路径文件名不含扩展名保持分隔符(inputPath))
        Dim escapedInputWithoutExtension = If(inputIsPlaceholder, "<\InputFileWithOutExtension>", 转换通配路径为滤镜路径(inputWithoutExtension))
        Dim escapedInputPath = If(inputIsPlaceholder, "<\InputFilePath>", 转换通配路径为滤镜路径(inputPathOnly))

        Return value.
            Replace("<\InputFileWithOutExtension>", escapedInputWithoutExtension).
            Replace("<\InputFilePath>", escapedInputPath).
            Replace("<InputFileWithOutExtension>", inputWithoutExtension).
            Replace("<InputFileNameWithOutExtension>", inputFileNameWithoutExtension).
            Replace("<InputFilePath>", inputPathOnly).
            Replace("<InputFileName>", inputFileName).
            Replace("<InputFile>", inputPath).
            Replace("<OutputFile>", outputPath).
            Replace(输入占位符, inputPath).
            Replace(输出占位符, outputPath)
    End Function

    Private Shared Function 转换通配路径为滤镜路径(value As String) As String
        If String.IsNullOrWhiteSpace(value) Then Return ""
        Return 将路径转换为FFmpeg滤镜接受的格式(value)
    End Function

    Private Shared Function 应用转译模式路径(value As String) As String
        Dim raw = If(value, "")
        If raw = "" OrElse Not 设置_v6.实例对象.转译模式 Then Return raw
        If raw.StartsWith("<"c) AndAlso raw.EndsWith(">"c) Then Return raw
        If String.Equals(raw, "NUL", StringComparison.OrdinalIgnoreCase) Then Return raw
        If raw.StartsWith("/"c) AndAlso Not raw.StartsWith("//", StringComparison.Ordinal) Then Return raw
        Return 转译模式处理路径(raw)
    End Function

    Private Shared Function 获取路径目录保持分隔符(value As String) As String
        Dim raw = If(value, "")
        If raw = "" Then Return ""
        Dim index = 最后路径分隔符索引(raw)
        If index < 0 Then Return ""
        If index = 0 Then Return raw.Substring(0, 1)
        Return raw.Substring(0, index)
    End Function

    Private Shared Function 获取路径文件名保持分隔符(value As String) As String
        Dim raw = If(value, "")
        If raw = "" Then Return ""
        Dim index = 最后路径分隔符索引(raw)
        If index < 0 OrElse index >= raw.Length - 1 Then Return If(index >= 0, "", raw)
        Return raw.Substring(index + 1)
    End Function

    Private Shared Function 获取路径文件名不含扩展名保持分隔符(value As String) As String
        Dim fileName = 获取路径文件名保持分隔符(value)
        If fileName = "" Then Return ""
        Dim dot = fileName.LastIndexOf("."c)
        If dot <= 0 Then Return fileName
        Return fileName.Substring(0, dot)
    End Function

    Private Shared Function 获取路径不含扩展名保持分隔符(value As String) As String
        Dim raw = If(value, "")
        If raw = "" Then Return ""
        Dim dir = 获取路径目录保持分隔符(raw)
        Dim name = 获取路径文件名不含扩展名保持分隔符(raw)
        If name = "" Then Return ""
        If dir = "" Then Return name
        Return 合并路径保持分隔符(dir, name, raw)
    End Function

    Private Shared Function 合并路径保持分隔符(dir As String, fileName As String, styleSource As String) As String
        Dim folder = If(dir, "")
        Dim name = If(fileName, "")
        If folder = "" Then Return name
        If name = "" Then Return folder
        If folder.EndsWith("/"c) OrElse folder.EndsWith("\"c) Then Return folder & name
        Dim separator = If(If(styleSource, "").Contains("/"c) AndAlso Not If(styleSource, "").Contains("\"c), "/"c, "\"c)
        Return folder & separator & name
    End Function

    Private Shared Function 最后路径分隔符索引(value As String) As Integer
        If String.IsNullOrEmpty(value) Then Return -1
        Return Math.Max(value.LastIndexOf("/"c), value.LastIndexOf("\"c))
    End Function

    Private Shared Function 规范命令行换行(value As String) As String
        If value Is Nothing Then Return ""
        Return value.Trim().Replace(vbCrLf, " ").Replace(vbLf, " ").Replace(vbCr, " ")
    End Function

    Private Shared Function Q(value As String) As String
        If value Is Nothing Then value = ""
        If value.StartsWith("<"c) AndAlso value.EndsWith(">"c) Then Return value
        Return $"""{value.Replace("""", "\""")}"""
    End Function

    Private Shared Function QMetadata(value As String) As String
        If value Is Nothing Then value = ""
        If value = "" Then Return """"""
        If value.Any(Function(c) Char.IsWhiteSpace(c) OrElse c = """"c OrElse c = "\"c) Then Return Q(value)
        Return value
    End Function


End Class
