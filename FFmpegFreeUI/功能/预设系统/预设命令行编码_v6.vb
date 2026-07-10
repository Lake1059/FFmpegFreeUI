Imports System.Globalization
Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions


Partial Public Class 预设管理_v6

    Private Shared Function 生成编码参数(a As 预设数据_v6, 阶段 As 预设数据_v6.命令行阶段, 视频选择器 As List(Of String), 音频选择器 As List(Of String), 请求视频输出 As Boolean, 请求音频输出 As Boolean, 视频来自滤镜 As Boolean, 音频来自滤镜 As Boolean, 输入文件 As String, 输出文件 As String) As String
        Dim parts As New List(Of String)
        Dim 编码器 = 视频编码器数据库_v6.获取编码器数据(a.视频参数_编码器_具体编码)
        If 编码器 IsNot Nothing AndAlso 编码器.命令行编码器名 <> "" Then
            If 编码器.是否禁用 Then
                parts.Add("-vn")
            Else
                添加按流视频编码参数(parts, 编码器, If(视频选择器, New List(Of String)), 视频来自滤镜)
            End If
        End If
        If 请求视频输出 Then 添加按流视频附加参数(parts, a, 阶段, If(视频选择器, New List(Of String)), 视频来自滤镜)

        Dim 音频 = 音频编码器数据库_v6.获取编码器数据(a.音频参数_编码器_代号)
        If 音频 IsNot Nothing AndAlso 阶段 <> 预设数据_v6.命令行阶段.二次编码第一遍 Then
            添加按流音频编码参数(parts, 音频, If(音频选择器, New List(Of String)), 音频来自滤镜)
        End If
        If 请求音频输出 AndAlso 阶段 <> 预设数据_v6.命令行阶段.二次编码第一遍 Then
            添加按流音频附加参数(parts, a, 音频, If(音频选择器, New List(Of String)), 音频来自滤镜)
        End If

        If 阶段 = 预设数据_v6.命令行阶段.二次编码第一遍 OrElse 阶段 = 预设数据_v6.命令行阶段.二次编码第二遍 Then
            parts.Add(If(阶段 = 预设数据_v6.命令行阶段.二次编码第一遍, "-pass 1", "-pass 2"))
            parts.Add("-passlogfile " & Q(生成二次编码日志路径(输入文件, 输出文件)))
        End If

        Return String.Join(" ", parts.Where(Function(x) x <> ""))
    End Function

    Private Shared Sub 添加按流视频编码参数(parts As List(Of String), 编码器 As 视频编码器数据库_v6.视频编码器数据, 选择器 As List(Of String), 来自滤镜 As Boolean)
        Dim targets = 规范输出流选择器列表(选择器)
        If targets.Count = 0 Then
            parts.Add($"-c:v {编码器.命令行编码器名}")
            Exit Sub
        End If
        If 编码器.是否复制流 AndAlso 来自滤镜 Then Exit Sub
        For Each target In targets
            parts.Add($"-c:{target} {编码器.命令行编码器名}")
        Next
    End Sub

    Private Shared Sub 添加按流视频附加参数(parts As List(Of String), a As 预设数据_v6, 阶段 As 预设数据_v6.命令行阶段, 选择器 As List(Of String), 来自滤镜 As Boolean)
        Dim targets = 规范输出流选择器列表(选择器, True)
        Dim 编码器 = 视频编码器数据库_v6.获取编码器数据(a.视频参数_编码器_具体编码)
        If 编码器 IsNot Nothing AndAlso 编码器.是否禁用 Then Exit Sub
        Dim 选择复制流编码器 = 编码器 IsNot Nothing AndAlso 编码器.是否复制流
        For Each target In targets
            If 编码器 IsNot Nothing AndAlso Not 选择复制流编码器 Then
                添加编码器参数(parts, 编码器.编码预设.参数名, a.视频参数_编码器_编码预设, target)
                添加编码器参数(parts, 编码器.配置文件.参数名, a.视频参数_编码器_配置文件, target)
                添加编码器参数(parts, 编码器.场景优化.参数名, a.视频参数_编码器_场景优化, target)
                添加编码器参数(parts, 编码器.图片质量.参数名, a.视频参数_编码器_图片编码器质量值, target)
            End If
            If Not 选择复制流编码器 Then 添加编码器参数(parts, "-r", a.视频参数_帧速率, target)
            If Not 选择复制流编码器 Then 添加编码器参数(parts, "-pix_fmt", a.视频参数_色彩管理_像素格式, target)
            parts.AddRange(生成色彩元数据参数(a, target))
            If Not 选择复制流编码器 Then
                添加编码器参数(parts, "-gpu", a.视频参数_编码器_gpu, target)
                添加编码器参数(parts, "-threads", a.视频参数_编码器_threads, target)
                parts.AddRange(生成质量参数(a, 阶段, target))
            End If
        Next
    End Sub

    Private Shared Sub 添加按流音频编码参数(parts As List(Of String), 音频 As 音频编码器数据库_v6.音频编码器数据, 选择器 As List(Of String), 来自滤镜 As Boolean)
        If 音频.是否禁用 Then
            parts.Add("-an")
            Exit Sub
        End If
        If 音频.是否复制流 AndAlso 来自滤镜 Then Exit Sub
        Dim targets = 规范输出流选择器列表(选择器, True)
        For Each target In targets
            If 音频.命令行编码器名 <> "" Then
                parts.Add($"{应用输出流选择器("-c:a", target)} {音频.命令行编码器名}")
            End If
            If 音频.是否复制流 Then Continue For
            For Each 单项参数 In 音频.默认附加参数列表
                添加音频编码器默认参数(parts, 单项参数, target)
            Next
        Next
    End Sub

    Private Shared Sub 添加按流音频附加参数(parts As List(Of String), a As 预设数据_v6, 音频 As 音频编码器数据库_v6.音频编码器数据, 选择器 As List(Of String), 来自滤镜 As Boolean)
        If 音频 IsNot Nothing AndAlso 音频.是否禁用 Then Exit Sub
        Dim 选择复制流编码器 = 音频 IsNot Nothing AndAlso 音频.是否复制流
        Dim targets = 规范输出流选择器列表(选择器, True)
        For Each target In targets
            If 选择复制流编码器 Then Continue For
            添加编码器参数(parts, "-b:a", a.音频参数_比特率, target)
            添加编码器参数(parts, a.音频参数_质量参数名, a.音频参数_质量值, target)
            添加编码器参数(parts, a.音频参数_质量参数名2, a.音频参数_质量值2, target)
            添加编码器参数(parts, "-sample_fmt", a.音频参数_位深度, target)
            添加编码器参数(parts, "-ar", a.音频参数_采样率, target)
        Next
    End Sub

    Private Shared Function 生成质量参数(a As 预设数据_v6, 阶段 As 预设数据_v6.命令行阶段, Optional 输出流选择器 As String = "") As List(Of String)
        Dim parts As New List(Of String)
        Dim 控制方式 = 标准化视频全局质量控制方式(a.视频参数_比特率_控制方式)
        Select Case 控制方式
            Case 预设数据_v6.视频全局质量控制方式.未选择
                添加视频质量参数(parts, a, 输出流选择器)
            Case 预设数据_v6.视频全局质量控制方式.CRF
                添加编码器参数(parts, 获取视频质量参数名(a.视频参数_质量控制_参数名, "crf"), a.视频参数_质量控制_值, 输出流选择器)
            Case 预设数据_v6.视频全局质量控制方式.CQP
                If 是AMF编码器(a.视频参数_编码器_具体编码) Then
                    添加AMFCQP码率控制参数(parts, a, 输出流选择器)
                Else
                    添加NVENC码率控制参数(parts, a, 控制方式, 输出流选择器)
                    添加编码器参数(parts, 获取视频质量参数名(a.视频参数_质量控制_参数名, "qp"), a.视频参数_质量控制_值, 输出流选择器)
                End If
            Case 预设数据_v6.视频全局质量控制方式.VBR, 预设数据_v6.视频全局质量控制方式.CBR
                添加NVENC码率控制参数(parts, a, 控制方式, 输出流选择器)
                添加视频质量参数(parts, a, 输出流选择器)
                添加编码器参数(parts, "-b:v", a.视频参数_比特率_基础, 输出流选择器)
                添加编码器参数(parts, "-minrate", a.视频参数_比特率_最低值, 输出流选择器)
                添加编码器参数(parts, "-maxrate", a.视频参数_比特率_最高值, 输出流选择器)
                添加编码器参数(parts, "-bufsize", a.视频参数_比特率_缓冲区, 输出流选择器)
            Case 预设数据_v6.视频全局质量控制方式.TPE
                If 阶段 = 预设数据_v6.命令行阶段.二次编码第一遍 OrElse 阶段 = 预设数据_v6.命令行阶段.二次编码第二遍 OrElse 二次编码基础码率有效(a) Then
                    添加编码器参数(parts, "-b:v", a.视频参数_比特率_基础, 输出流选择器)
                    添加编码器参数(parts, "-minrate", a.视频参数_比特率_最低值, 输出流选择器)
                    添加编码器参数(parts, "-maxrate", a.视频参数_比特率_最高值, 输出流选择器)
                    添加编码器参数(parts, "-bufsize", a.视频参数_比特率_缓冲区, 输出流选择器)
                Else
                    添加视频质量参数(parts, a, 输出流选择器)
                End If
        End Select
        AddRaw(parts, a.视频参数_质量控制_进阶参数集)
        Return parts
    End Function

    Private Shared Sub 添加NVENC码率控制参数(parts As List(Of String), a As 预设数据_v6, 控制方式 As 预设数据_v6.视频全局质量控制方式, 输出流选择器 As String)
        If Not 是NVENC编码器(a.视频参数_编码器_具体编码) Then Exit Sub
        If 已显式设置码率控制(a.视频参数_质量控制_进阶参数集) Then Exit Sub

        Select Case 控制方式
            Case 预设数据_v6.视频全局质量控制方式.CQP
                添加编码器参数(parts, "-rc", "constqp", 输出流选择器)
            Case 预设数据_v6.视频全局质量控制方式.VBR
                添加编码器参数(parts, "-rc", "vbr", 输出流选择器)
            Case 预设数据_v6.视频全局质量控制方式.CBR
                添加编码器参数(parts, "-rc", "cbr", 输出流选择器)
        End Select
    End Sub

    Private Shared Sub 添加AMFCQP码率控制参数(parts As List(Of String), a As 预设数据_v6, 输出流选择器 As String)
        If Not 已显式设置码率控制(a.视频参数_质量控制_进阶参数集) Then
            添加编码器参数(parts, "-rc", "cqp", 输出流选择器)
        End If
    End Sub

    Private Shared Function 是NVENC编码器(编码器 As String) As Boolean
        Dim value = If(编码器, "").Trim()
        Return value.EndsWith("_nvenc", StringComparison.OrdinalIgnoreCase)
    End Function

    Private Shared Function 是AMF编码器(编码器 As String) As Boolean
        Dim value = If(编码器, "").Trim()
        Return value.EndsWith("_amf", StringComparison.OrdinalIgnoreCase)
    End Function

    Private Shared Function 已显式设置码率控制(value As String) As Boolean
        Return Regex.IsMatch(If(value, ""), "(^|\s)-rc(?::\S+)?(?=\s|$)", RegexOptions.IgnoreCase)
    End Function

    Private Shared Sub 添加视频质量参数(parts As List(Of String), a As 预设数据_v6, 输出流选择器 As String)
        If String.IsNullOrWhiteSpace(a.视频参数_质量控制_参数名) OrElse String.IsNullOrWhiteSpace(a.视频参数_质量控制_值) Then Exit Sub
        添加编码器参数(parts, 获取视频质量参数名(a.视频参数_质量控制_参数名, ""), a.视频参数_质量控制_值, 输出流选择器)
    End Sub

    Private Shared Function 获取视频质量参数名(参数名 As String, 默认名 As String) As String
        Dim value = If(参数名, "").Trim()
        If value = "" Then value = 默认名
        Return 规范FFmpeg参数名(value)
    End Function

    Private Shared Function 标准化视频全局质量控制方式(value As 预设数据_v6.视频全局质量控制方式) As 预设数据_v6.视频全局质量控制方式
        Dim raw = Convert.ToInt32(value, CultureInfo.InvariantCulture)
        If raw = 3 Then Return 预设数据_v6.视频全局质量控制方式.VBR
        If [Enum].IsDefined(value) Then Return value
        Return 预设数据_v6.视频全局质量控制方式.未选择
    End Function

    Private Shared Function 生成色彩元数据参数(a As 预设数据_v6, 输出流选择器 As String) As List(Of String)
        Dim parts As New List(Of String)
        If Not 色彩元数据已设置(a) Then Return parts
        添加编码器参数(parts, "-colorspace", 标准化色彩值(a.视频参数_色彩管理_矩阵系数, False), 输出流选择器)
        添加编码器参数(parts, "-color_primaries", 标准化色彩值(a.视频参数_色彩管理_色域, False), 输出流选择器)
        添加编码器参数(parts, "-color_trc", 标准化色彩值(a.视频参数_色彩管理_传输特性, False), 输出流选择器)
        添加编码器参数(parts, "-color_range", 标准化色彩范围(a.视频参数_色彩管理_范围, False), 输出流选择器)
        Return parts
    End Function

    Private Shared Function 色彩元数据已设置(a As 预设数据_v6) As Boolean
        If a Is Nothing Then Return False
        Dim mode = If(a.视频参数_色彩管理_处理方式, "").Trim()
        If Not String.Equals(mode, "写入元数据并转换", StringComparison.Ordinal) AndAlso
           Not String.Equals(mode, "仅写入元数据", StringComparison.Ordinal) Then Return False
        Return 标准化色彩值(a.视频参数_色彩管理_矩阵系数, False) <> "" OrElse
               标准化色彩值(a.视频参数_色彩管理_色域, False) <> "" OrElse
               标准化色彩值(a.视频参数_色彩管理_传输特性, False) <> "" OrElse
               标准化色彩范围(a.视频参数_色彩管理_范围, False) <> ""
    End Function

    Private Shared Function 色彩转换滤镜已设置(a As 预设数据_v6) As Boolean
        Return 构造色彩转换滤镜(a) <> ""
    End Function

    Private Shared Function 标准化色彩值(value As String, 允许Auto As Boolean) As String
        Dim raw = If(value, "").Trim()
        If raw = "" Then Return ""
        If String.Equals(raw, "auto", StringComparison.OrdinalIgnoreCase) Then Return If(允许Auto, "auto", "")
        Return raw
    End Function

    Private Shared Function 标准化色彩范围(value As String, 允许Auto As Boolean) As String
        Dim raw = 标准化色彩值(value, 允许Auto)
        If raw = "" Then Return ""
        If raw.StartsWith("tv", StringComparison.OrdinalIgnoreCase) OrElse raw.Contains("有限", StringComparison.Ordinal) OrElse raw.Contains("limited", StringComparison.OrdinalIgnoreCase) Then Return "tv"
        If raw.StartsWith("pc", StringComparison.OrdinalIgnoreCase) OrElse raw.Contains("全范围", StringComparison.Ordinal) OrElse raw.Contains("full", StringComparison.OrdinalIgnoreCase) Then Return "pc"
        Return raw
    End Function

    Private Shared Function 可以生成二次编码(a As 预设数据_v6) As Boolean
        Return a IsNot Nothing AndAlso
               a.视频参数_比特率_控制方式 = 预设数据_v6.视频全局质量控制方式.TPE AndAlso
               二次编码基础码率有效(a) AndAlso
               Not 二次编码明显不兼容(a)
    End Function

    Private Shared Function 二次编码明显不兼容(a As 预设数据_v6) As Boolean
        If a Is Nothing Then Return False
        If a.视频参数_编码器_类型 = 预设数据_v6.视频编码器类型.图片 Then Return True
        Dim 编码器 = 视频编码器数据库_v6.获取编码器数据(If(a.视频参数_编码器_具体编码, ""))
        Return 编码器 IsNot Nothing AndAlso (编码器.是否复制流 OrElse 编码器.是否禁用)
    End Function

    Private Shared Function 二次编码基础码率有效(a As 预设数据_v6) As Boolean
        Return a IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(a.视频参数_比特率_基础)
    End Function

    Private Shared Function 全局质量参数已设置(a As 预设数据_v6) As Boolean
        Return a IsNot Nothing AndAlso
               (Not String.IsNullOrWhiteSpace(a.视频参数_质量控制_参数名) OrElse
                Not String.IsNullOrWhiteSpace(a.视频参数_质量控制_值))
    End Function

    Private Shared Function 视频输出附加参数已设置(a As 预设数据_v6) As Boolean
        If a Is Nothing Then Return False
        Return Not String.IsNullOrWhiteSpace(a.视频参数_编码器_编码预设) OrElse
               Not String.IsNullOrWhiteSpace(a.视频参数_编码器_配置文件) OrElse
               Not String.IsNullOrWhiteSpace(a.视频参数_编码器_场景优化) OrElse
               Not String.IsNullOrWhiteSpace(a.视频参数_编码器_图片编码器质量值) OrElse
               Not String.IsNullOrWhiteSpace(a.视频参数_帧速率) OrElse
               Not String.IsNullOrWhiteSpace(a.视频参数_色彩管理_像素格式) OrElse
               色彩元数据已设置(a) OrElse
               Not String.IsNullOrWhiteSpace(a.视频参数_编码器_gpu) OrElse
               Not String.IsNullOrWhiteSpace(a.视频参数_编码器_threads) OrElse
               a.视频参数_比特率_控制方式 <> 预设数据_v6.视频全局质量控制方式.未选择 OrElse
               Not String.IsNullOrWhiteSpace(a.视频参数_比特率_基础) OrElse
               Not String.IsNullOrWhiteSpace(a.视频参数_比特率_最低值) OrElse
               Not String.IsNullOrWhiteSpace(a.视频参数_比特率_最高值) OrElse
               Not String.IsNullOrWhiteSpace(a.视频参数_比特率_缓冲区) OrElse
               全局质量参数已设置(a) OrElse
               Not String.IsNullOrWhiteSpace(a.视频参数_质量控制_进阶参数集)
    End Function

    Private Shared Function 音频输出附加参数已设置(a As 预设数据_v6) As Boolean
        If a Is Nothing Then Return False
        Return Not String.IsNullOrWhiteSpace(a.音频参数_比特率) OrElse
               Not String.IsNullOrWhiteSpace(a.音频参数_质量参数名) OrElse
               Not String.IsNullOrWhiteSpace(a.音频参数_质量值) OrElse
               Not String.IsNullOrWhiteSpace(a.音频参数_质量参数名2) OrElse
               Not String.IsNullOrWhiteSpace(a.音频参数_质量值2) OrElse
               Not String.IsNullOrWhiteSpace(a.音频参数_位深度) OrElse
               Not String.IsNullOrWhiteSpace(a.音频参数_采样率)
    End Function

    Private Shared Function 生成二次编码日志路径(输入文件 As String, 输出文件 As String) As String
        Dim source = 应用转译模式路径(If(输入文件, "").Trim())
        Dim target = 应用转译模式路径(If(输出文件, "").Trim())
        Dim token = 短路径哈希(source & "|" & target)
        If source = "" OrElse (source.StartsWith("<"c) AndAlso source.EndsWith(">"c)) Then Return $"3fui-v6-passlog-{token}"

        Dim dir = 获取路径目录保持分隔符(source)
        If String.IsNullOrWhiteSpace(dir) Then dir = Environment.CurrentDirectory
        Dim baseName = 获取路径文件名不含扩展名保持分隔符(source)
        If String.IsNullOrWhiteSpace(baseName) Then baseName = "input"
        Return 合并路径保持分隔符(dir, $"3fui-v6-passlog-{清理二次编码日志文件名(baseName)}-{token}", source)
    End Function

    Private Shared Function 短路径哈希(value As String) As String
        Dim bytes = System.Security.Cryptography.SHA256.HashData(Encoding.UTF8.GetBytes(If(value, "")))
        Return Convert.ToHexString(bytes, 0, 6).ToLowerInvariant()
    End Function

    Private Shared Function 清理二次编码日志文件名(value As String) As String
        Dim result = If(value, "").Trim()
        For Each c In Path.GetInvalidFileNameChars()
            result = result.Replace(c, "_"c)
        Next
        If result = "" Then result = "input"
        Return result
    End Function

End Class
