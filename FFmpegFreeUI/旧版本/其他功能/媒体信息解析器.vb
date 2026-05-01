Imports System.Text.RegularExpressions

Public Class 媒体信息解析器
    Public Class 媒体信息
        Public Property 视频流 As List(Of 视频流信息)
        Public Property 音频流 As List(Of 音频流信息)
        Public Property 字幕流 As List(Of 字幕流信息)
    End Class
    Public Class 视频流信息
        Public Property 编码器 As String = ""
        Public Property 编码器库 As String = ""
        Public Property 比特率 As String = ""
        Public Property 像素格式 As String = ""
        Public Property 分辨率 As String = ""
        Public Property 帧率 As String = ""
        Public Property 流语言 As String = ""
        Public Property 元数据标题 As String = ""
    End Class
    Public Class 音频流信息
        Public Property 编码器 As String = ""
        Public Property 编码器库 As String = ""
        Public Property 比特率 As String = ""
        Public Property 采样率 As String = ""
        Public Property 声道布局 As String = ""
        Public Property 流语言 As String = ""
        Public Property 元数据标题 As String = ""
    End Class
    Public Class 字幕流信息
        Public Property 编码器 As String = ""
        Public Property 编码器库 As String = ""
        Public Property 流语言 As String = ""
        Public Property 元数据标题 As String = ""
    End Class

    ''' <summary>
    ''' 解析 ffprobe 输出内容
    ''' </summary>
    ''' <param name="ffprobeOutput">ffprobe 的完整输出文本</param>
    ''' <returns>解析后的媒体信息</returns>
    Public Shared Function 解析(ffprobeOutput As List(Of String)) As 媒体信息
        Dim 结果 As New 媒体信息 With {
            .视频流 = New List(Of 视频流信息),
            .音频流 = New List(Of 音频流信息),
            .字幕流 = New List(Of 字幕流信息)
        }

        Dim 当前流类型 As String = ""
        Dim 当前视频流 As 视频流信息 = Nothing
        Dim 当前音频流 As 音频流信息 = Nothing
        Dim 当前字幕流 As 字幕流信息 = Nothing

        For Each 行 In ffprobeOutput
            Dim 清理行 = 行.Trim()

            ' 检测流类型行
            ' MKV格式: Stream #0:0(eng): Video: hevc (Main 10)...
            ' MP4格式: Stream #0:0[0x1](und): Video: hevc (Main 10) (hev1 / 0x31766568)...
            Dim 流匹配 = Regex.Match(清理行, "Stream #\d+:\d+(?:\[0x[0-9a-fA-F]+\])?(?:\((\w+)\))?: (Video|Audio|Subtitle): (.+)")
            If 流匹配.Success Then
                Dim 语言 = 流匹配.Groups(1).Value
                Dim 流类型 = 流匹配.Groups(2).Value
                Dim 流详情 = 流匹配.Groups(3).Value
                当前流类型 = 流类型

                Select Case 流类型
                    Case "Video"
                        当前视频流 = 解析视频流(流详情, 语言)
                        结果.视频流.Add(当前视频流)
                    Case "Audio"
                        当前音频流 = 解析音频流(流详情, 语言)
                        结果.音频流.Add(当前音频流)
                    Case "Subtitle"
                        当前字幕流 = 解析字幕流(流详情, 语言)
                        结果.字幕流.Add(当前字幕流)
                End Select
                Continue For
            End If

            ' 解析元数据标题
            Dim 标题匹配 = Regex.Match(清理行, "^\s*title\s*:\s*(.+)$", RegexOptions.IgnoreCase)
            If 标题匹配.Success Then
                Dim 标题值 = 标题匹配.Groups(1).Value.Trim()
                Select Case 当前流类型
                    Case "Video"
                        If 当前视频流 IsNot Nothing Then 当前视频流.元数据标题 = 标题值
                    Case "Audio"
                        If 当前音频流 IsNot Nothing Then 当前音频流.元数据标题 = 标题值
                    Case "Subtitle"
                        If 当前字幕流 IsNot Nothing Then 当前字幕流.元数据标题 = 标题值
                End Select
            End If

            ' 解析元数据编码器
            Dim 编码器库匹配 = Regex.Match(清理行, "^\s*encoder\s*:\s*(.+)$", RegexOptions.IgnoreCase)
            If 编码器库匹配.Success Then
                Dim 编码器库值 = 编码器库匹配.Groups(1).Value.Trim()
                Select Case 当前流类型
                    Case "Video"
                        If 当前视频流 IsNot Nothing Then 当前视频流.编码器库 = 编码器库值
                    Case "Audio"
                        If 当前音频流 IsNot Nothing Then 当前音频流.编码器库 = 编码器库值
                    Case "Subtitle"
                        If 当前字幕流 IsNot Nothing Then 当前字幕流.编码器库 = 编码器库值
                End Select
            End If

        Next

        Return 结果
    End Function

    Private Shared Function 解析视频流(流详情 As String, 语言 As String) As 视频流信息
        Dim 信息 As New 视频流信息 With {.流语言 = 语言}
        Dim 部分 = 分割流详情(流详情)

        If 部分.Count > 0 Then
            ' 编码器包含括号内容，例如: hevc (Main 10)
            信息.编码器 = 提取带括号内容(部分(0))
        End If

        For Each 部分项 In 部分
            Dim 清理项 = 部分项.Trim()

            ' 像素格式包含括号内容，例如: yuv420p10le(tv, bt2020nc/bt2020/smpte2084)
            If Regex.IsMatch(清理项, "^(yuv|rgb|bgr|gray|p0|nv)\w*", RegexOptions.IgnoreCase) Then
                信息.像素格式 = 提取带括号内容(清理项)
            End If

            ' 分辨率，例如: 3840x2160 或 1920x1080 [SAR 1:1 DAR 16:9]
            Dim 分辨率匹配 = Regex.Match(清理项, "(\d{2,5}x\d{2,5})")
            If 分辨率匹配.Success AndAlso String.IsNullOrEmpty(信息.分辨率) Then
                信息.分辨率 = 分辨率匹配.Groups(1).Value
            End If

            ' 帧率，例如: 23.98 fps 或 24 fps
            Dim 帧率匹配 = Regex.Match(清理项, "([\d.]+)\s*fps")
            If 帧率匹配.Success Then
                Dim 帧率值 As Double
                If Double.TryParse(帧率匹配.Groups(1).Value, 帧率值) Then
                    信息.帧率 = CInt(Math.Round(帧率值)).ToString() & "fps"
                End If
            End If

            ' 比特率，例如: 15000 kb/s
            Dim 比特率匹配 = Regex.Match(清理项, "(\d+)\s*kb/s(?:\s*\([^)]*\))?", RegexOptions.IgnoreCase)
            If 比特率匹配.Success Then
                信息.比特率 = 比特率匹配.Groups(1).Value & "kbps"
            End If
        Next

        Return 信息
    End Function

    Private Shared Function 解析音频流(流详情 As String, 语言 As String) As 音频流信息
        Dim 信息 As New 音频流信息 With {.流语言 = 语言}
        Dim 部分 = 分割流详情(流详情)

        If 部分.Count > 0 Then
            信息.编码器 = 提取带括号内容(部分(0))
        End If

        For Each 部分项 In 部分
            Dim 清理项 = 部分项.Trim()

            ' 采样率，例如: 48000 Hz
            Dim 采样率匹配 = Regex.Match(清理项, "(\d+)\s*Hz")
            If 采样率匹配.Success Then
                信息.采样率 = 采样率匹配.Groups(1).Value & "Hz"
            End If

            ' 声道布局，例如: stereo, 5.1, 7.1
            If Regex.IsMatch(清理项, "^(mono|stereo|[\d.]+|[a-z]+\(side\))$", RegexOptions.IgnoreCase) Then
                信息.声道布局 = 清理项
            End If

            ' 比特率（允许例如: 192 kb/s (default)）
            Dim 比特率匹配 = Regex.Match(清理项, "(\d+)\s*kb/s(?:\s*\([^)]*\))?", RegexOptions.IgnoreCase)
            If 比特率匹配.Success Then
                信息.比特率 = 比特率匹配.Groups(1).Value & "kbps"
            End If
        Next

        Return 信息
    End Function

    Private Shared Function 解析字幕流(流详情 As String, 语言 As String) As 字幕流信息
        Dim 信息 As New 字幕流信息 With {.流语言 = 语言}
        Dim 部分 = 分割流详情(流详情)

        If 部分.Count > 0 Then
            信息.编码器 = 提取带括号内容(部分(0))
        End If

        Return 信息
    End Function

    ''' <summary>
    ''' 提取包含紧跟括号的完整内容
    ''' </summary>
    Private Shared Function 提取带括号内容(输入 As String) As String
        Dim 清理输入 = 输入.Trim()
        Dim 匹配 = Regex.Match(清理输入, "^(\w+(?:\s*\([^)]*\))?)")
        If 匹配.Success Then
            Return 匹配.Groups(1).Value.Trim()
        End If
        Return 清理输入.Split({","c, " "c})(0)
    End Function

    ''' <summary>
    ''' 智能分割流详情，保留括号完整性
    ''' </summary>
    Private Shared Function 分割流详情(详情 As String) As List(Of String)
        Dim 结果 As New List(Of String)
        Dim 当前部分 As New Text.StringBuilder()
        Dim 括号深度 As Integer = 0

        For Each 字符 In 详情
            If 字符 = "("c Then
                括号深度 += 1
                当前部分.Append(字符)
            ElseIf 字符 = ")"c Then
                括号深度 -= 1
                当前部分.Append(字符)
            ElseIf 字符 = ","c AndAlso 括号深度 = 0 Then
                If 当前部分.Length > 0 Then
                    结果.Add(当前部分.ToString().Trim())
                    当前部分.Clear()
                End If
            Else
                当前部分.Append(字符)
            End If
        Next

        If 当前部分.Length > 0 Then
            结果.Add(当前部分.ToString().Trim())
        End If

        Return 结果
    End Function

End Class