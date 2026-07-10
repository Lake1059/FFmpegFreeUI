Imports System.Globalization
Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions


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

        Dim durationSeconds As Double
        If Not TryParseTimeSeconds(媒体总时长, durationSeconds) Then
            parts.Add("-t " & 媒体总时长占位符)
            Return String.Join(" ", parts)
        End If

        Dim trimHead As Double
        Dim trimTail As Double
        If 入点 <> "" AndAlso Not TryParseTimeSeconds(入点, trimHead) Then trimHead = 0
        If 出点 <> "" AndAlso Not TryParseTimeSeconds(出点, trimTail) Then trimTail = 0
        Dim keepDuration = Math.Max(0, durationSeconds - trimHead - trimTail)
        parts.Add("-t " & FormatSeconds(keepDuration))
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

    Private Shared Function 生成解码参数(a As 预设数据_v6) As String
        Dim parts As New List(Of String)
        If a.解码参数_解码器 <> "" Then parts.Add($"-hwaccel {a.解码参数_解码器}")
        If a.解码参数_CPU解码线程数 <> "" Then parts.Add($"-threads {a.解码参数_CPU解码线程数}")
        If a.解码参数_解码数据格式 <> "" Then parts.Add($"-hwaccel_output_format {a.解码参数_解码数据格式}")
        If a.解码参数_指定硬件的参数名 <> "" AndAlso a.解码参数_指定硬件的参数 <> "" Then parts.Add($"{规范FFmpeg参数名(a.解码参数_指定硬件的参数名)} {a.解码参数_指定硬件的参数}")
        Return String.Join(" ", parts)
    End Function

    Private Shared Function 生成主输入参数(a As 预设数据_v6, 输入文件 As String, 帧服务器脚本后缀 As String) As List(Of String)
        Dim parts As New List(Of String)
        If a.视频参数_视频帧服务器_使用AviSynth AndAlso a.视频参数_视频帧服务器_avs脚本文件.Trim() <> "" Then
            parts.Add("-i")
            parts.Add(Q(应用转译模式路径(派生脚本路径(输入文件, ".avs", 帧服务器脚本后缀))))
        ElseIf a.视频参数_视频帧服务器_使用VapourSynth AndAlso a.视频参数_视频帧服务器_vpy脚本文件.Trim() <> "" Then
            parts.Add("-f vapoursynth")
            parts.Add("-i")
            parts.Add(Q(应用转译模式路径(派生脚本路径(输入文件, Path.GetExtension(a.视频参数_视频帧服务器_vpy脚本文件), 帧服务器脚本后缀))))
        Else
            parts.Add("-i")
            parts.Add(Q(应用转译模式路径(输入文件)))
        End If
        Return parts
    End Function

    Public Shared Function 派生帧服务器脚本路径(输入文件 As String, ext As String, Optional 后缀 As String = "") As String
        Return 派生脚本路径(输入文件, ext, 后缀)
    End Function

    Private Shared Function 派生脚本路径(输入文件 As String, ext As String, 后缀 As String) As String
        Dim safeSuffix = 清理二次编码日志文件名(If(后缀, "").Trim())
        Dim suffixPart = If(safeSuffix = "", "", "." & safeSuffix)
        Dim finalExt = If(String.IsNullOrWhiteSpace(ext), ".vpy", ext)
        If 输入文件 = 输入占位符 OrElse 输入文件 = "<InputFile>" Then
            Return "<InputFileWithOutExtension>" & suffixPart & finalExt
        End If
        Dim stem = 获取路径不含扩展名保持分隔符(输入文件)
        If String.IsNullOrWhiteSpace(stem) Then Return ""
        Return stem & suffixPart & finalExt
    End Function

    Private Shared Function 生成元数据章节附件片段(a As 预设数据_v6, 当前视频输出数量 As Integer, 输入文件 As String, 输出文件 As String, 当前字幕输出数量 As Integer) As 附加输出片段
        Dim result As New 附加输出片段
        Select Case a.流控制_元数据选项
            Case 1
                result.输出前.Add("-map_metadata 0")
            Case 2
                result.输出前.Add("-map_metadata -1")
            Case 3
                result.输出前.Add("-map_metadata 0 -movflags +use_metadata_tags")
        End Select

        For Each item In If(a.元数据_要写入的信息, Array.Empty(Of 预设数据_v6.元数据单片结构)())
            If item Is Nothing Then Continue For
            Dim 字段 = item.字段.Trim()
            If 字段 = "" Then Continue For
            result.输出前.Add($"-metadata {QMetadata(字段 & "=" & If(item.值, ""))}")
        Next

        Select Case a.流控制_章节选项
            Case 1
                result.输出前.Add("-map_chapters 0")
            Case 2
                result.输出前.Add("-map_chapters -1")
        End Select

        If a.章节_来源 <> 预设数据_v6.章节来源.未选择 AndAlso a.章节_文件路径.Trim() <> "" Then
            Dim inputIndex = 1 + result.额外输入.Where(Function(x) x = "-i").Count()
            Select Case a.章节_来源
                Case 预设数据_v6.章节来源.文本文档
                    result.额外输入.Add("-f ffmetadata")
                    result.额外输入.Add("-i")
                    result.额外输入.Add(Q(应用转译模式路径(a.章节_文件路径.Trim())))
                    result.输出前.Add($"-map_metadata {inputIndex}")
                    result.输出前.Add($"-map_chapters {inputIndex}")
                Case 预设数据_v6.章节来源.媒体文件
                    result.额外输入.Add("-i")
                    result.额外输入.Add(Q(应用转译模式路径(a.章节_文件路径.Trim())))
                    result.输出前.Add($"-map_chapters {inputIndex}")
            End Select
        End If

        Dim 允许常规附件 = 输出容器支持附件(a, 输出文件)
        Dim 允许附加封面图 = 输出容器支持附加封面图(a, 输出文件)
        Select Case a.流控制_附件选项
            Case 1
                If 允许常规附件 Then
                    result.输出前.Add("-map 0:t? -c:t copy")
                    result.包含显式流映射 = True
                End If
        End Select

        Dim 额外输入索引 = 1 + result.额外输入.Where(Function(x) x = "-i").Count()
        添加自动混流字幕片段(a, 输入文件, 输出文件, result, 额外输入索引, 当前字幕输出数量)

        Dim 封面序号 As Integer = 0
        Dim 附件序号 As Integer = 0
        For Each item In If(a.附件_要写入的附件, Array.Empty(Of 预设数据_v6.附件单片结构)())
            If item Is Nothing OrElse item.文件路径.Trim() = "" OrElse item.类型 = 预设数据_v6.附件单片结构.附件类型.未选择 Then Continue For
            Dim 附件路径 = 应用转译模式路径(item.文件路径.Trim())
            Select Case item.类型
                Case 预设数据_v6.附件单片结构.附件类型.MP4封面图
                    If Not 允许附加封面图 Then Continue For
                    If 当前视频输出数量 < 0 Then Continue For
                    result.额外输入.Add("-i")
                    result.额外输入.Add(Q(附件路径))
                    Dim 输出视频序号 = 当前视频输出数量 + 封面序号
                    Dim 封面视频流 = $"{额外输入索引}:v:0"
                    result.输出前.Add($"-map {可选输入流映射(封面视频流)}")
                    result.包含显式流映射 = True
                    result.输出前.Add($"-c:v:{输出视频序号} copy")
                    result.输出前.Add($"-disposition:v:{输出视频序号} attached_pic")
                    result.附加封面图数量 += 1
                    额外输入索引 += 1
                    封面序号 += 1
                Case 预设数据_v6.附件单片结构.附件类型.MKV封面图,
                     预设数据_v6.附件单片结构.附件类型.图片,
                     预设数据_v6.附件单片结构.附件类型.字体文件,
                     预设数据_v6.附件单片结构.附件类型.文本文档
                    If Not 允许常规附件 Then Continue For
                    result.输出前.Add($"-attach {Q(附件路径)}")
                    result.输出前.Add($"-metadata:s:t:{附件序号} mimetype={获取附件Mimetype(附件路径, item.类型)}")
                    If item.类型 = 预设数据_v6.附件单片结构.附件类型.MKV封面图 Then
                        result.输出前.Add($"-metadata:s:t:{附件序号} filename=cover{Path.GetExtension(附件路径)}")
                    Else
                        result.输出前.Add($"-metadata:s:t:{附件序号} {QMetadata("filename=" & 获取路径文件名保持分隔符(附件路径))}")
                    End If
                    附件序号 += 1
            End Select
        Next

        Return result
    End Function

    Private Shared Function 输出容器支持附件(a As 预设数据_v6, 输出文件 As String) As Boolean
        Select Case 获取输出容器扩展名(a, 输出文件)
            Case "mkv", "mka", "mks", "mk3d"
                Return True
            Case Else
                Return False
        End Select
    End Function

    Private Shared Function 输出容器支持附加封面图(a As 预设数据_v6, 输出文件 As String) As Boolean
        Select Case 获取输出容器扩展名(a, 输出文件)
            Case "mp4", "m4v", "m4a", "mov", "3gp", "3g2", "mkv", "mka", "mks", "mk3d"
                Return True
            Case Else
                Return False
        End Select
    End Function

    Private Shared Function 输出容器支持MovText字幕(a As 预设数据_v6, 输出文件 As String) As Boolean
        Select Case 获取输出容器扩展名(a, 输出文件)
            Case "mp4", "m4v", "m4a", "mov", "3gp", "3g2"
                Return True
            Case Else
                Return False
        End Select
    End Function

    Private Shared Function 输出容器支持WebVtt字幕(a As 预设数据_v6, 输出文件 As String) As Boolean
        Select Case 获取输出容器扩展名(a, 输出文件)
            Case "webm"
                Return True
            Case Else
                Return False
        End Select
    End Function

    Private Shared Function 附加片段默认映射主视频(a As 预设数据_v6, 输出文件 As String) As Boolean
        Select Case 获取输出容器扩展名(a, 输出文件)
            Case "mp3", "m4a", "aac", "flac", "wav", "ogg", "opus", "wma", "mka", "srt", "ass", "ssa", "vtt", "sup", "jpg", "jpeg", "png", "webp", "bmp", "gif", "raw", "yuv", "h264", "h265", "hevc", "av1", "ivf"
                Return False
            Case Else
                Return True
        End Select
    End Function

    Private Shared Function 附加片段默认映射主音频(a As 预设数据_v6, 输出文件 As String) As Boolean
        Select Case 获取输出容器扩展名(a, 输出文件)
            Case "jpg", "jpeg", "png", "webp", "bmp", "gif", "srt", "ass", "ssa", "vtt", "sup", "raw", "yuv", "h264", "h265", "hevc", "av1", "ivf"
                Return False
            Case Else
                Return True
        End Select
    End Function

    Private Shared Function 获取输出容器扩展名(a As 预设数据_v6, 输出文件 As String) As String
        Dim ext = Path.GetExtension(If(输出文件, "")).TrimStart("."c).ToLowerInvariant()
        If ext = "" OrElse 输出文件 = 输出占位符 OrElse 输出文件 = "<OutputFile>" Then ext = If(a?.输出容器, "").Trim().TrimStart("."c).ToLowerInvariant()
        Return ext
    End Function

    Private Shared Sub 添加自动混流字幕片段(a As 预设数据_v6, 输入文件 As String, 输出文件 As String, result As 附加输出片段, ByRef 额外输入索引 As Integer, 当前字幕输出数量 As Integer)
        If Not a.流控制_自动混流SRT AndAlso Not a.流控制_自动混流ASS AndAlso Not a.流控制_自动混流SSA Then Exit Sub

        Dim 自动字幕序号 As Integer = 0
        For Each 字幕文件 In 获取自动混流字幕文件(a, 输入文件)
            result.额外输入.Add("-i")
            result.额外输入.Add(Q(应用转译模式路径(字幕文件)))
            result.输出前.Add($"-map {可选输入流映射($"{额外输入索引}:0")}")
            result.包含显式流映射 = True

            Dim 字幕编码 = 获取自动混流字幕编码(a, 输出文件)
            If 当前字幕输出数量 >= 0 AndAlso 字幕编码 <> "" Then
                result.输出前.Add($"-c:s:{当前字幕输出数量 + 自动字幕序号} {字幕编码}")
                result.自动混流字幕数量 += 1
            ElseIf 当前字幕输出数量 < 0 AndAlso 字幕编码 <> "" Then
                result.输出前.Add($"-c:s {字幕编码}")
            End If

            额外输入索引 += 1
            自动字幕序号 += 1
        Next
    End Sub

    Private Shared Function 获取自动混流字幕编码(a As 预设数据_v6, 输出文件 As String) As String
        If 输出容器支持MovText字幕(a, 输出文件) Then Return "mov_text"
        If 输出容器支持WebVtt字幕(a, 输出文件) Then Return "webvtt"
        Return 获取容器兼容字幕编码(If(a.流控制_自动混流的字幕转为MOVTEXT, "mov_text", "copy"), a, 输出文件, True)
    End Function

    Private Shared Function 获取自动混流字幕文件(a As 预设数据_v6, 输入文件 As String) As List(Of String)
        Dim result As New List(Of String)
        If a.流控制_自动混流SRT Then 添加自动混流字幕候选(result, 输入文件, ".srt")
        If a.流控制_自动混流ASS Then 添加自动混流字幕候选(result, 输入文件, ".ass")
        If a.流控制_自动混流SSA Then 添加自动混流字幕候选(result, 输入文件, ".ssa")
        Return result.Distinct(StringComparer.OrdinalIgnoreCase).ToList()
    End Function

    Private Shared Sub 添加自动混流字幕候选(result As List(Of String), 输入文件 As String, ext As String)
        Dim path = 派生同名字幕路径(输入文件, ext)
        If path = "" Then Exit Sub
        If 是输入文件占位符(输入文件) OrElse File.Exists(path) Then result.Add(path)
    End Sub

    Private Shared Function 派生同名字幕路径(输入文件 As String, ext As String) As String
        If 是输入文件占位符(输入文件) Then Return "<InputFileWithOutExtension>" & ext
        Dim stem = 获取路径不含扩展名保持分隔符(输入文件)
        If String.IsNullOrWhiteSpace(stem) Then Return ""
        Return stem & ext
    End Function

    Private Shared Function 是输入文件占位符(输入文件 As String) As Boolean
        Dim value = If(输入文件, "").Trim()
        Return value = 输入占位符 OrElse value = "<InputFile>"
    End Function

End Class
