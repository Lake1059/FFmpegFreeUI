Partial Public Class 预设管理_v6

    Private Shared ReadOnly separator As String() = {","}
    Public Const 输入占位符 As String = "<输入文件>"
    Public Const 输出占位符 As String = "<输出文件>"
    Public Const 媒体总时长占位符 As String = "<媒体总时长>"

    Private Class 附加输出片段
        Public Property 额外输入 As New List(Of String)
        Public Property 输出前 As New List(Of String)
        Public Property 包含显式流映射 As Boolean = False
        Public Property 附加封面图数量 As Integer = 0
        Public Property 自动混流字幕数量 As Integer = 0

        Public Sub 重设附加封面图视频序号(起始序号 As Integer)
            If 附加封面图数量 <= 0 Then Exit Sub
            Dim index = 0
            For i = 0 To 输出前.Count - 1
                Dim line = 输出前(i)
                If line.StartsWith("-c:v:", StringComparison.Ordinal) Then
                    输出前(i) = $"-c:v:{起始序号 + index} copy"
                ElseIf line.StartsWith("-disposition:v:", StringComparison.Ordinal) Then
                    输出前(i) = $"-disposition:v:{起始序号 + index} attached_pic"
                    index += 1
                    If index >= 附加封面图数量 Then Exit For
                End If
            Next
        End Sub

        Public Sub 重设自动混流字幕序号(起始序号 As Integer)
            If 自动混流字幕数量 <= 0 Then Exit Sub
            Dim index = 0
            For i = 0 To 输出前.Count - 1
                Dim line = 输出前(i)
                If line.StartsWith("-c:s:", StringComparison.Ordinal) Then
                    Dim firstSpace = line.IndexOf(" "c)
                    If firstSpace > 0 Then
                        输出前(i) = $"-c:s:{起始序号 + index}{line.Substring(firstSpace)}"
                        index += 1
                        If index >= 自动混流字幕数量 Then Exit For
                    End If
                End If
            Next
        End Sub
    End Class

    Private Class 剪辑参数片段
        Public Property 输入前 As String = ""
        Public Property 输出前 As String = ""
    End Class

    Public Shared Function 将预设数据转换为命令行(a As 预设数据_v6, 输入文件 As String, 输出文件 As String) As String
        Return 生成命令行(a, 输入文件, 输出文件, 预设数据_v6.命令行阶段.普通单次).命令行
    End Function

    Public Shared Function 生成命令行展示文本(a As 预设数据_v6, 输入文件 As String, 输出文件 As String) As String
        Dim 阶段列表 = 生成阶段化命令行(a, 输入文件, 输出文件)
        If 阶段列表.Count = 0 Then Return ""

        Dim lines As New List(Of String)
        For Each item In 阶段列表
            If lines.Count > 0 Then lines.Add("")
            lines.Add($"{获取命令行进程名(item.阶段)} {item.命令行}")
        Next
        Return String.Join(vbCrLf, lines)
    End Function

    Public Shared Function 获取命令行进程名(阶段 As 预设数据_v6.命令行阶段) As String
        If 阶段 = 预设数据_v6.命令行阶段.FFprobe获取时长 Then Return "ffprobe"
        Return 格式化命令行进程名(获取当前FFmpeg进程文件名())
    End Function

    Private Shared Function 获取当前FFmpeg进程文件名() As String
        Dim processName = If(设置_v6.实例对象?.替代进程文件名, "").Trim()
        Return If(processName = "", "ffmpeg", processName)
    End Function

    Private Shared Function 格式化命令行进程名(value As String) As String
        Dim processName = If(value, "").Trim()
        If processName = "" Then processName = "ffmpeg"
        If processName.Any(Function(c) Char.IsWhiteSpace(c)) AndAlso Not (processName.StartsWith("""c", StringComparison.Ordinal) AndAlso processName.EndsWith("""c", StringComparison.Ordinal)) Then
            Return Q(processName)
        End If
        Return processName
    End Function

    Public Shared Function 生成阶段化命令行(a As 预设数据_v6,
                                  输入文件 As String,
                                  输出文件 As String,
                                  Optional 媒体总时长 As String = "",
                                  Optional 帧服务器脚本后缀 As String = "") As List(Of 预设数据_v6.命令行生成结果)
        初始化空集合(a)
        Dim 结果 As New List(Of 预设数据_v6.命令行生成结果)
        If a Is Nothing Then Return 结果
        If Not String.IsNullOrWhiteSpace(a.自定义参数_完全自己写) Then
            结果.Add(生成命令行(a, 输入文件, 输出文件, 预设数据_v6.命令行阶段.普通单次, 媒体总时长, 帧服务器脚本后缀))
            Return 结果
        End If

        If a.剪辑区间_方法 = 预设数据_v6.剪辑方法.掐头去尾 AndAlso String.IsNullOrWhiteSpace(媒体总时长) Then
            结果.Add(生成命令行(a, 输入文件, 输出文件, 预设数据_v6.命令行阶段.FFprobe获取时长, 帧服务器脚本后缀:=帧服务器脚本后缀))
        End If

        If 可以生成二次编码(a) Then
            结果.Add(生成命令行(a, 输入文件, 输出文件, 预设数据_v6.命令行阶段.二次编码第一遍, 媒体总时长, 帧服务器脚本后缀))
            结果.Add(生成命令行(a, 输入文件, 输出文件, 预设数据_v6.命令行阶段.二次编码第二遍, 媒体总时长, 帧服务器脚本后缀))
        Else
            结果.Add(生成命令行(a, 输入文件, 输出文件, 预设数据_v6.命令行阶段.普通单次, 媒体总时长, 帧服务器脚本后缀))
        End If

        Return 结果
    End Function

    Public Shared Function 生成命令行(a As 预设数据_v6,
                                  输入文件 As String,
                                  输出文件 As String,
                                  Optional 阶段 As 预设数据_v6.命令行阶段 = 预设数据_v6.命令行阶段.普通单次,
                                  Optional 媒体总时长 As String = "",
                                  Optional 帧服务器脚本后缀 As String = "") As 预设数据_v6.命令行生成结果
        Dim 结果 As New 预设数据_v6.命令行生成结果 With {.阶段 = 阶段}
        If a Is Nothing Then Return 结果
        初始化空集合(a)

        If a.自定义参数_完全自己写.Trim() <> "" Then
            结果.命令行 = 规范命令行换行(应用自定义参数通配字符串(a.自定义参数_完全自己写, 输入文件, 输出文件))
            Return 结果
        End If

        If 阶段 = 预设数据_v6.命令行阶段.FFprobe获取时长 Then
            结果.命令行 = $"-v error -show_entries format=duration -of default=noprint_wrappers=1:nokey=1 {Q(应用转译模式路径(输入文件))}"
            结果.说明 = "获取媒体总时长"
            Return 结果
        End If

        Dim 前置 As New List(Of String)
        Dim 输入前 As New List(Of String)
        Dim 主输入后 As New List(Of String)
        Dim 额外输入 As New List(Of String)
        Dim 输出前 As New List(Of String)
        Dim 输出后 As New List(Of String)

        AddRaw(前置, 应用自定义参数通配字符串(a.自定义参数_开头参数, 输入文件, 输出文件))
        AddRaw(主输入后, 应用自定义参数通配字符串(a.自定义参数_之前参数, 输入文件, 输出文件))
        AddRaw(输出后, 应用自定义参数通配字符串(a.自定义参数_最后参数, 输入文件, 输出文件))
        Dim 剪辑参数 = 生成剪辑参数(a, 阶段, 媒体总时长, 结果)
        AddRaw(输入前, 剪辑参数.输入前)
        AddRaw(输入前, 生成解码参数(a))
        Dim 主输入参数 = 生成主输入参数(a, 输入文件, 帧服务器脚本后缀)

        If (阶段 = 预设数据_v6.命令行阶段.二次编码第一遍 OrElse 阶段 = 预设数据_v6.命令行阶段.二次编码第二遍) AndAlso Not 二次编码基础码率有效(a) Then
            结果.说明 = "二次编码需要填写基础码率，已按普通单次生成。"
            阶段 = 预设数据_v6.命令行阶段.普通单次
            结果.阶段 = 阶段
        End If
        If (阶段 = 预设数据_v6.命令行阶段.二次编码第一遍 OrElse 阶段 = 预设数据_v6.命令行阶段.二次编码第二遍) AndAlso 二次编码明显不兼容(a) Then
            结果.说明 = "二次编码需要实际重编码视频流，当前视频编码设置不适用，已按普通单次生成。"
            阶段 = 预设数据_v6.命令行阶段.普通单次
            结果.阶段 = 阶段
        End If

        Dim 滤镜图 = 生成滤镜图(a, 仅视频:=(阶段 = 预设数据_v6.命令行阶段.二次编码第一遍), 输入文件:=输入文件, 输出文件:=输出文件)
        结果.滤镜图 = 滤镜图.滤镜图
        结果.映射参数 = 滤镜图.映射参数
        结果.输出滤镜参数 = 滤镜图.输出滤镜参数
        AddRaw(输出前, 剪辑参数.输出前)
        If 滤镜图.滤镜图 <> "" Then
            输出前.Add($"-filter_complex {Q(滤镜图.滤镜图)}")
        End If
        AddRaw(输出前, 滤镜图.映射参数)
        AddRaw(输出前, 滤镜图.输出滤镜参数)

        Dim 编码参数 = 生成编码参数(a, 阶段, 滤镜图.编码视频选择器, 滤镜图.编码音频选择器, 滤镜图.请求视频输出, 滤镜图.请求音频输出, 滤镜图.视频输出来自滤镜, 滤镜图.音频输出来自滤镜, 输入文件, 输出文件)
        AddRaw(输出前, 编码参数)
        If 阶段 = 预设数据_v6.命令行阶段.二次编码第一遍 Then 输出前.Add("-an")

        If 阶段 <> 预设数据_v6.命令行阶段.二次编码第一遍 Then
            Dim 附加片段 = 生成元数据章节附件片段(a, 滤镜图.视频输出数量, 输入文件, 输出文件, 滤镜图.字幕输出数量)
            If 附加片段.包含显式流映射 AndAlso String.IsNullOrWhiteSpace(滤镜图.映射参数) Then
                Dim 视频编码器 = 视频编码器数据库_v6.获取编码器数据(a.视频参数_编码器_具体编码)
                Dim 音频编码器 = 音频编码器数据库_v6.获取编码器数据(a.音频参数_编码器_代号)
                Dim 补默认主视频 = Not (视频编码器 IsNot Nothing AndAlso 视频编码器.是否禁用) AndAlso 附加片段默认映射主视频(a, 输出文件)
                If 补默认主视频 Then 输出前.Add("-map 0:v:0?")
                If Not (音频编码器 IsNot Nothing AndAlso 音频编码器.是否禁用) AndAlso 附加片段默认映射主音频(a, 输出文件) Then 输出前.Add("-map 0:a:0?")
                If 附加片段.附加封面图数量 > 0 Then 附加片段.重设附加封面图视频序号(If(补默认主视频, 1, 0))
                If 附加片段.自动混流字幕数量 > 0 Then 附加片段.重设自动混流字幕序号(0)
            End If
            额外输入.AddRange(附加片段.额外输入)
            输出前.AddRange(附加片段.输出前)
        End If
        AddRaw(输出前, 应用自定义参数通配字符串(a.自定义参数_之后参数, 输入文件, 输出文件))

        Dim 丢弃输出 = 阶段 = 预设数据_v6.命令行阶段.二次编码第一遍 OrElse a.输出_输出文件参数使用方法 = 预设数据_v6.输出文件参数使用方法.声明丢弃输出
        Dim 输出目标 = If(丢弃输出, "NUL", 输出文件)
        Dim 命令 As New List(Of String)
        命令.AddRange(前置.Where(Function(x) x <> ""))
        命令.Add("-hide_banner")
        命令.Add("-y")
        命令.AddRange(输入前.Where(Function(x) x <> ""))
        命令.AddRange(主输入参数.Where(Function(x) x <> ""))
        命令.AddRange(主输入后.Where(Function(x) x <> ""))
        命令.AddRange(额外输入.Where(Function(x) x <> ""))
        命令.AddRange(输出前.Where(Function(x) x <> ""))
        If 丢弃输出 Then
            命令.Add("-f null")
            命令.Add("NUL")
        Else
            If a.输出_输出文件参数使用方法 <> 预设数据_v6.输出文件参数使用方法.不附加 Then
                命令.Add(Q(应用转译模式路径(输出目标)))
            End If
        End If
        命令.AddRange(输出后.Where(Function(x) x <> ""))
        结果.命令行 = String.Join(" ", 命令.Where(Function(x) Not String.IsNullOrWhiteSpace(x)))
        Return 结果
    End Function

    Public Shared Function 生成排序页预览(a As 预设数据_v6) As String
        If a Is Nothing Then Return ""
        初始化空集合(a)
        Dim 图 = 生成滤镜图(a)
        Dim 片段 As New List(Of String)
        If 图.滤镜图 <> "" Then 片段.Add($"-filter_complex {Q(图.滤镜图)}")
        If 图.映射参数 <> "" Then 片段.Add(图.映射参数)
        If 图.输出滤镜参数 <> "" Then 片段.Add(图.输出滤镜参数)
        Return String.Join(" ", 片段)
    End Function

    Public Shared Function 获取滤镜片段(a As 预设数据_v6, item As 预设数据_v6.滤镜排序单片结构, Optional 输入文件 As String = 输入占位符, Optional 输出文件 As String = 输出占位符) As String
        If item Is Nothing Then Return ""
        If item.是自定义滤镜 OrElse item.滤镜标识符 = 预设数据_v6.滤镜排序单片结构.标识符枚举.自定义视频滤镜 OrElse item.滤镜标识符 = 预设数据_v6.滤镜排序单片结构.标识符枚举.自定义音频滤镜 Then
            Return 应用自定义参数通配字符串(item.自定义滤镜内容, 输入文件, 输出文件).Trim()
        End If

        Select Case item.滤镜标识符
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.裁剪
                Return 构造裁剪滤镜(a)
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.缩放
                Return 构造缩放滤镜(a)
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.抽帧
                Return 构造抽帧滤镜(a)
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.插帧
                Return 构造插帧滤镜(a)
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.动态模糊
                Return 构造动态模糊滤镜(a)
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.超分
                Return 构造超分滤镜(a)
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.降噪
                Return 构造降噪滤镜(a)
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.锐化
                Return 构造锐化滤镜(a)
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.胶片颗粒
                Return 构造胶片颗粒滤镜(a)
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.平滑断层
                Return 构造平滑断层滤镜(a)
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.扫描方式
                Return 构造扫描方式滤镜(a)
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.画面翻转
                Return 构造翻转滤镜(a)
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.烧录字幕
                Return 构造烧字幕滤镜(a, 输入文件)
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.像素格式预先转换
                Return 构造像素格式预先转换滤镜(a)
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.色彩转换
                Return 构造色彩转换滤镜(a)
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.调色
                Return 构造调色滤镜(a)
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.音频响度标准化
                Return 构造响度滤镜(a)
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.音频格式转换
                Return If(a.音频参数_声道数 <> "", $"aformat=channel_layouts={a.音频参数_声道数}", "")
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.音频重采样
                Return ""
        End Select
        Return ""
    End Function

End Class
