Imports System.Globalization
Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions


Partial Public Class 预设管理_v6

    Private Shared Sub 添加编码器参数(parts As List(Of String), 参数名 As String, 值 As String)
        添加编码器参数(parts, 参数名, 值, "")
    End Sub

    Private Shared Sub 添加编码器参数(parts As List(Of String), 参数名 As String, 值 As String, 输出流选择器 As String)
        If String.IsNullOrWhiteSpace(参数名) OrElse String.IsNullOrWhiteSpace(值) Then Exit Sub
        parts.Add($"{应用输出流选择器(规范FFmpeg参数名(参数名), 输出流选择器)} {值}")
    End Sub

    Private Shared Function 规范FFmpeg参数名(参数名 As String) As String
        Dim value = If(参数名, "").Trim()
        If value = "" Then Return ""
        If value.StartsWith("-"c, StringComparison.Ordinal) Then Return value
        Return "-" & value
    End Function

    Private Shared Sub 添加音频编码器默认参数(parts As List(Of String), 单项参数 As 音频编码器数据库_v6.音频编码器参数数据, 输出流选择器 As String)
        If 单项参数 Is Nothing OrElse String.IsNullOrWhiteSpace(单项参数.参数名) Then Exit Sub
        Dim 参数名 = 应用输出流选择器(单项参数.参数名, 输出流选择器)
        If String.IsNullOrWhiteSpace(单项参数.默认值) Then
            parts.Add(参数名)
        Else
            parts.Add($"{参数名} {单项参数.默认值}")
        End If
    End Sub

    Private Shared Function 规范输出流选择器列表(选择器 As List(Of String), Optional 空列表表示全局 As Boolean = False) As List(Of String)
        If 选择器 Is Nothing OrElse 选择器.Count = 0 Then
            If 空列表表示全局 Then Return New List(Of String) From {""}
            Return New List(Of String)
        End If
        Return 选择器.Where(Function(x) Not String.IsNullOrWhiteSpace(x)).Distinct(StringComparer.OrdinalIgnoreCase).ToList()
    End Function

    Private Shared Function 应用输出流选择器(参数名 As String, 输出流选择器 As String) As String
        If String.IsNullOrWhiteSpace(参数名) Then Return ""
        Dim p = 规范FFmpeg参数名(参数名)
        If String.IsNullOrWhiteSpace(输出流选择器) Then Return p
        If Not p.StartsWith("-"c, StringComparison.Ordinal) Then Return p
        Dim firstSpace = p.IndexOf(" "c)
        Dim head = If(firstSpace >= 0, p.Substring(0, firstSpace), p)
        Dim tail = If(firstSpace >= 0, p.Substring(firstSpace), "")
        Dim colon = head.IndexOf(":"c)
        If colon >= 0 Then
            Return String.Concat(head.AsSpan(0, colon), ":", 输出流选择器, tail)
        End If
        Return head & ":" & 输出流选择器 & tail
    End Function

    Private Shared Function 获取字幕编码参数(index As Integer) As String
        Select Case index
            Case 1
                Return "copy"
            Case 2
                Return "mov_text"
            Case 3
                Return "srt"
            Case 4
                Return "ass"
            Case 5
                Return "ssa"
            Case Else
                Return ""
        End Select
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

    Private Shared Function 获取附件Mimetype(file As String, 类型 As 预设数据_v6.附件单片结构.附件类型) As String
        Dim ext = Path.GetExtension(If(file, "")).TrimStart("."c).ToLowerInvariant()
        Select Case ext
            Case "jpg", "jpeg"
                Return "image/jpeg"
            Case "png"
                Return "image/png"
            Case "webp"
                Return "image/webp"
            Case "bmp"
                Return "image/bmp"
            Case "gif"
                Return "image/gif"
            Case "ttf"
                Return "application/x-truetype-font"
            Case "otf"
                Return "application/vnd.ms-opentype"
            Case "ttc"
                Return "application/x-font-ttf"
            Case "woff"
                Return "font/woff"
            Case "woff2"
                Return "font/woff2"
            Case "txt", "md", "nfo"
                Return "text/plain"
            Case "json"
                Return "application/json"
            Case "xml"
                Return "application/xml"
            Case Else
                Select Case 类型
                    Case 预设数据_v6.附件单片结构.附件类型.字体文件
                        Return "application/octet-stream"
                    Case 预设数据_v6.附件单片结构.附件类型.文本文档
                        Return "text/plain"
                    Case Else
                        Return "application/octet-stream"
                End Select
        End Select
    End Function

    Private Shared Function 规范流列表(values As IEnumerable(Of String), 类型 As String) As List(Of String)
        Dim result As New List(Of String)
        If values Is Nothing Then Return result
        For Each raw In values
            For Each item In If(raw, "").Split(separator, StringSplitOptions.RemoveEmptyEntries)
                Dim s = item.Trim()
                If s = "" Then Continue For
                If s.Contains(":"c) Then
                    result.Add(s)
                Else
                    result.Add($"0:{类型}:{s}")
                End If
            Next
        Next
        Return result.Distinct().ToList()
    End Function

    Private Shared Function 标准化插帧选项值(value As String, ParamArray options() As (Text As String, Value As String)) As String
        Dim text = If(value, "").Trim()
        If text = "" Then Return ""
        For Each item In options
            If String.Equals(text, item.Text, StringComparison.Ordinal) OrElse
               String.Equals(text, item.Value, StringComparison.OrdinalIgnoreCase) Then
                Return item.Value
            End If
        Next
        Return text
    End Function

    Private Shared Function 插帧选项显示文本(value As String, ParamArray options() As (Text As String, Value As String)) As String
        Dim normalized = 标准化插帧选项值(value, options)
        If normalized = "" Then Return ""
        For Each item In options
            If String.Equals(normalized, item.Value, StringComparison.OrdinalIgnoreCase) Then Return item.Text
        Next
        Return normalized
    End Function

    Private Shared Function 插帧模式参数值(value As String) As String
        Return 标准化插帧选项值(value,
            ("两帧加权平均", "blend"),
            ("运动补偿插值", "mci"))
    End Function

    Private Shared Function 插帧模式显示文本(value As String) As String
        Return 插帧选项显示文本(value,
            ("两帧加权平均", "blend"),
            ("运动补偿插值", "mci"))
    End Function

    Private Shared Function 运动估计模式参数值(value As String) As String
        Return 标准化插帧选项值(value,
            ("双向运动估计", "bidir"),
            ("双侧运动估计", "bilat"))
    End Function

    Private Shared Function 运动估计模式显示文本(value As String) As String
        Return 插帧选项显示文本(value,
            ("双向运动估计", "bidir"),
            ("双侧运动估计", "bilat"))
    End Function

    Private Shared Function 运动估计算法参数值(value As String) As String
        Return 标准化插帧选项值(value,
            ("穷举搜索", "esa"),
            ("三步搜索", "tss"),
            ("二维对数搜索", "tdls"),
            ("新三步搜索", "ntss"),
            ("四步搜索", "fss"),
            ("菱形搜索", "ds"),
            ("基于 Hexagon", "hexbs"),
            ("增强的预测区域", "epzs"),
            ("不均匀多六边形", "umh"))
    End Function

    Private Shared Function 运动估计算法显示文本(value As String) As String
        Return 插帧选项显示文本(value,
            ("穷举搜索", "esa"),
            ("三步搜索", "tss"),
            ("二维对数搜索", "tdls"),
            ("新三步搜索", "ntss"),
            ("四步搜索", "fss"),
            ("菱形搜索", "ds"),
            ("基于 Hexagon", "hexbs"),
            ("增强的预测区域", "epzs"),
            ("不均匀多六边形", "umh"))
    End Function

    Private Shared Function 运动补偿模式参数值(value As String) As String
        Return 标准化插帧选项值(value,
            ("重叠块运动补偿", "obmc"),
            ("加权 obmc", "aobmc"))
    End Function

    Private Shared Function 运动补偿模式显示文本(value As String) As String
        Return 插帧选项显示文本(value,
            ("重叠块运动补偿", "obmc"),
            ("加权 obmc", "aobmc"))
    End Function

End Class
