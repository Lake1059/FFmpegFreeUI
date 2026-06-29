Imports System.Globalization
Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions

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
        AddRaw(输出前, 应用自定义参数通配字符串(a.自定义参数_视频参数, 输入文件, 输出文件))
        AddRaw(输出前, 应用自定义参数通配字符串(a.自定义参数_音频参数, 输入文件, 输出文件))
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

    Private Shared Function 生成滤镜图(a As 预设数据_v6, Optional 仅视频 As Boolean = False, Optional 输入文件 As String = 输入占位符, Optional 输出文件 As String = 输出占位符) As 滤镜图结果
        Dim 排序 = If(a.滤镜排序系统, Array.Empty(Of 预设数据_v6.滤镜排序单片结构)()).ToList()
        Dim 视频链 = 排序.Where(Function(x) x.滤镜目标流类型 = 预设数据_v6.滤镜排序单片结构.流类型.视频).
            Select(Function(x) 清理线性滤镜片段(获取滤镜片段(a, x, 输入文件, 输出文件))).
            Where(Function(x) x <> "").
            ToList()
        Dim 音频链 = If(仅视频, New List(Of String), 排序.Where(Function(x) x.滤镜目标流类型 = 预设数据_v6.滤镜排序单片结构.流类型.音频).
            Select(Function(x) 清理线性滤镜片段(获取滤镜片段(a, x, 输入文件, 输出文件))).
            Where(Function(x) x <> "").
            ToList())

        Dim 视频流 = 规范流列表(a.流控制_将视频参数应用于指定流, "v")
        Dim 音频流 = If(仅视频, New List(Of String), 规范流列表(a.流控制_将音频参数应用于指定流, "a"))
        Dim 视频Trim = 构造Trim滤镜(a, False)
        If 视频Trim <> "" Then 视频链.Insert(0, 视频Trim)
        Dim 音频Trim = If(仅视频 OrElse (音频流.Count = 0 AndAlso 音频链.Count = 0 AndAlso String.IsNullOrWhiteSpace(a.音频参数_编码器_代号)), "", 构造Trim滤镜(a, True))
        If 音频Trim <> "" Then 音频链.Insert(0, 音频Trim)
        Dim 视频附加参数已设置 = 视频输出附加参数已设置(a)
        Dim 音频附加参数已设置 = 音频输出附加参数已设置(a)
        If 视频流.Count = 0 AndAlso (视频链.Count > 0 OrElse a.视频参数_编码器_具体编码 <> "" OrElse 视频附加参数已设置) Then 视频流.Add("0:v:0")
        If Not 仅视频 AndAlso 音频流.Count = 0 AndAlso (音频链.Count > 0 OrElse a.音频参数_编码器_代号 <> "" OrElse 音频附加参数已设置) Then 音频流.Add("0:a:0")

        Dim 图段 As New List(Of String)
        Dim 映射 As New List(Of String)
        Dim 输出滤镜 As New List(Of String)
        Dim 编码视频选择器 As New List(Of String)
        Dim 编码音频选择器 As New List(Of String)
        Dim 视频输出数量 As Integer = 0
        Dim 音频输出数量 As Integer = 0
        Dim 保留其他视频 = a.流控制_启用保留其他视频流
        Dim 保留其他音频 = Not 仅视频 AndAlso a.流控制_启用保留其他音频流
        Dim 保留其他字幕 = Not 仅视频 AndAlso a.流控制_启用保留其他字幕流
        Dim 视频编码器 = 视频编码器数据库_v6.获取编码器数据(a.视频参数_编码器_具体编码)
        Dim 音频编码器 = 音频编码器数据库_v6.获取编码器数据(a.音频参数_编码器_代号)
        Dim 字幕流 = If(仅视频, New List(Of String), 规范流列表(a.流控制_将字幕参数应用于指定流, "s"))
        Dim 禁用视频 = 视频编码器 IsNot Nothing AndAlso 视频编码器.是否禁用
        Dim 禁用音频 = 音频编码器 IsNot Nothing AndAlso 音频编码器.是否禁用
        Dim 请求视频输出 = Not 禁用视频 AndAlso (视频链.Count > 0 OrElse 视频流.Count > 0 OrElse 保留其他视频 OrElse 视频附加参数已设置 OrElse 编码器请求输出(视频编码器))
        Dim 请求音频输出 = Not 仅视频 AndAlso Not 禁用音频 AndAlso (音频链.Count > 0 OrElse 音频流.Count > 0 OrElse 保留其他音频 OrElse 音频附加参数已设置 OrElse 编码器请求输出(音频编码器))
        Dim 请求字幕输出 = Not 仅视频 AndAlso (字幕流.Count > 0 OrElse 保留其他字幕)
        Dim 视频链使用输出滤镜 = 视频链.Count > 0 AndAlso Not 保留其他视频 AndAlso 可作为输出滤镜链(视频链)
        Dim 音频链使用输出滤镜 = 音频链.Count > 0 AndAlso Not 保留其他音频 AndAlso 可作为输出滤镜链(音频链)
        Dim 视频链含完整滤镜图 = 视频链.Any(Function(x) Not 可作为输出滤镜片段(x))
        Dim 音频链含完整滤镜图 = 音频链.Any(Function(x) Not 可作为输出滤镜片段(x))

        If 请求视频输出 AndAlso Not 保留其他视频 AndAlso 视频链.Count > 0 AndAlso 视频链含完整滤镜图 Then
            Dim 完整图输出 As New List(Of String)
            If 添加完整滤镜图链(图段, 视频链, "vout0", "v0", 完整图输出) Then
                添加完整滤镜图输出映射(映射, 编码视频选择器, 完整图输出, "v")
            Else
                For i = 0 To 视频流.Count - 1
                    Dim label = $"vout{i}"
                    添加线性滤镜链图段(图段, 视频流(i), 视频链, label, $"v{i}")
                    映射.Add($"-map [{label}]?")
                    编码视频选择器.Add($"v:{i}")
                Next
            End If
        ElseIf 请求视频输出 AndAlso Not 保留其他视频 Then
            For i = 0 To 视频流.Count - 1
                If 视频链.Count > 0 Then
                    If 视频链使用输出滤镜 Then
                        映射.Add($"-map {可选输入流映射(视频流(i))}")
                    Else
                        Dim label = $"vout{i}"
                        添加线性滤镜链图段(图段, 视频流(i), 视频链, label, $"v{i}")
                        映射.Add($"-map [{label}]?")
                    End If
                Else
                    映射.Add($"-map {可选输入流映射(视频流(i))}")
                End If
                编码视频选择器.Add($"v:{i}")
            Next
            If 视频链使用输出滤镜 Then 输出滤镜.Add($"-filter:v {Q(String.Join(",", 视频链))}")
        ElseIf 请求视频输出 AndAlso 视频链.Count = 0 Then
            For Each stream In 视频流
                编码视频选择器.Add(获取保留映射输出流选择器(stream, "v"))
            Next
        End If

        If 请求音频输出 AndAlso Not 保留其他音频 AndAlso 音频链.Count > 0 AndAlso 音频链含完整滤镜图 Then
            Dim 完整图输出 As New List(Of String)
            If 添加完整滤镜图链(图段, 音频链, "aout0", "a0", 完整图输出) Then
                添加完整滤镜图输出映射(映射, 编码音频选择器, 完整图输出, "a")
            Else
                For i = 0 To 音频流.Count - 1
                    Dim label = $"aout{i}"
                    添加线性滤镜链图段(图段, 音频流(i), 音频链, label, $"a{i}")
                    映射.Add($"-map [{label}]?")
                    编码音频选择器.Add($"a:{i}")
                Next
            End If
        ElseIf 请求音频输出 AndAlso Not 保留其他音频 Then
            For i = 0 To 音频流.Count - 1
                If 音频链.Count > 0 Then
                    If 音频链使用输出滤镜 Then
                        映射.Add($"-map {可选输入流映射(音频流(i))}")
                    Else
                        Dim label = $"aout{i}"
                        添加线性滤镜链图段(图段, 音频流(i), 音频链, label, $"a{i}")
                        映射.Add($"-map [{label}]?")
                    End If
                Else
                    映射.Add($"-map {可选输入流映射(音频流(i))}")
                End If
                编码音频选择器.Add($"a:{i}")
            Next
            If 音频链使用输出滤镜 Then 输出滤镜.Add($"-filter:a {Q(String.Join(",", 音频链))}")
        ElseIf 请求音频输出 AndAlso 音频链.Count = 0 Then
            For Each stream In 音频流
                编码音频选择器.Add(获取保留映射输出流选择器(stream, "a"))
            Next
        End If

        If 请求字幕输出 AndAlso Not 保留其他字幕 Then
            For Each s In 字幕流
                映射.Add($"-map {可选输入流映射(s)}")
            Next
        End If

        If 请求视频输出 AndAlso 保留其他视频 Then
            If 视频链.Count > 0 Then
                Dim 完整图输出 As New List(Of String)
                If 视频链含完整滤镜图 AndAlso 添加完整滤镜图链(图段, 视频链, "vkeep0", "vkeep", 完整图输出) Then
                    添加完整滤镜图输出映射(映射, 编码视频选择器, 完整图输出, "v")
                Else
                    For i = 0 To 视频流.Count - 1
                        Dim label = $"vkeep{i}"
                        添加线性滤镜链图段(图段, 视频流(i), 视频链, label, $"vkeep{i}")
                        映射.Add($"-map [{label}]?")
                        编码视频选择器.Add($"v:{i}")
                    Next
                End If
            End If
            映射.Add("-map 0:v?")
            If 视频链.Count > 0 Then
                For Each stream In 视频流
                    映射.Add("-map " & 可选输入流映射("-" & stream))
                Next
            End If
            If 视频链.Count = 0 OrElse (视频编码器 IsNot Nothing AndAlso Not 视频编码器.是否复制流 AndAlso Not 视频编码器.是否禁用 AndAlso 视频编码器.命令行编码器名 <> "") Then
                映射.Add("-c:v copy")
            End If
        End If
        If 请求视频输出 Then
            视频输出数量 = If(保留其他视频, -1, If(编码视频选择器.Count > 0, 编码视频选择器.Count, 视频流.Count))
        End If
        If 请求音频输出 AndAlso 保留其他音频 Then
            If 音频链.Count > 0 Then
                Dim 完整图输出 As New List(Of String)
                If 音频链含完整滤镜图 AndAlso 添加完整滤镜图链(图段, 音频链, "akeep0", "akeep", 完整图输出) Then
                    添加完整滤镜图输出映射(映射, 编码音频选择器, 完整图输出, "a")
                Else
                    For i = 0 To 音频流.Count - 1
                        Dim label = $"akeep{i}"
                        添加线性滤镜链图段(图段, 音频流(i), 音频链, label, $"akeep{i}")
                        映射.Add($"-map [{label}]?")
                        编码音频选择器.Add($"a:{i}")
                    Next
                End If
            End If
            映射.Add("-map 0:a?")
            If 音频链.Count > 0 Then
                For Each stream In 音频流
                    映射.Add("-map " & 可选输入流映射("-" & stream))
                Next
            End If
            If 音频链.Count = 0 OrElse (音频编码器 IsNot Nothing AndAlso Not 音频编码器.是否复制流 AndAlso Not 音频编码器.是否禁用 AndAlso 音频编码器.命令行编码器名 <> "") Then
                映射.Add("-c:a copy")
            End If
        End If
        If 请求音频输出 Then
            音频输出数量 = If(保留其他音频, -1, If(编码音频选择器.Count > 0, 编码音频选择器.Count, 音频流.Count))
        End If
        If 请求字幕输出 AndAlso 保留其他字幕 Then
            映射.Add("-map 0:s?")
            映射.Add("-c:s copy")
        End If

        Dim 字幕参数 = 获取容器兼容字幕编码(获取字幕编码参数(a.流控制_如何操作指定的字幕), a, 输出文件, False)
        If 请求字幕输出 AndAlso 字幕参数 <> "" AndAlso 字幕流.Count > 0 Then
            If 保留其他字幕 Then
                For Each selector In 字幕流.Select(Function(x) 获取保留映射输出流选择器(x, "s")).Distinct(StringComparer.OrdinalIgnoreCase)
                    映射.Add($"-c:{selector} {字幕参数}")
                Next
            Else
                映射.Add($"-c:s {字幕参数}")
            End If
        End If

        Return New 滤镜图结果 With {
            .滤镜图 = String.Join(";", 图段),
            .映射参数 = String.Join(" ", 映射.Distinct(StringComparer.Ordinal)),
            .输出滤镜参数 = String.Join(" ", 输出滤镜.Distinct(StringComparer.Ordinal)),
            .编码视频选择器 = 编码视频选择器.Where(Function(x) x <> "").Distinct(StringComparer.OrdinalIgnoreCase).ToList(),
            .编码音频选择器 = 编码音频选择器.Where(Function(x) x <> "").Distinct(StringComparer.OrdinalIgnoreCase).ToList(),
            .视频输出数量 = 视频输出数量,
            .音频输出数量 = 音频输出数量,
            .字幕输出数量 = If(仅视频, 0, If(保留其他字幕, -1, 字幕流.Count)),
            .请求视频输出 = 请求视频输出,
            .请求音频输出 = 请求音频输出,
            .请求字幕输出 = 请求字幕输出,
            .视频输出来自滤镜 = 视频链.Count > 0,
            .音频输出来自滤镜 = 音频链.Count > 0
        }
    End Function

    Private Shared Function 编码器请求输出(编码器 As Object) As Boolean
        If 编码器 Is Nothing Then Return False
        Dim p = 编码器.GetType().GetProperty("命令行编码器名")
        If p Is Nothing Then Return False
        Return Not String.IsNullOrWhiteSpace(Convert.ToString(p.GetValue(编码器), CultureInfo.InvariantCulture))
    End Function

    Private Shared Function 可作为输出滤镜链(滤镜链 As List(Of String)) As Boolean
        If 滤镜链 Is Nothing OrElse 滤镜链.Count = 0 Then Return False
        Return 滤镜链.All(Function(x) 可作为输出滤镜片段(x))
    End Function

    Private Shared Function 可作为输出滤镜片段(片段 As String) As Boolean
        Dim value = If(片段, "").Trim()
        If value = "" Then Return False
        Return Not 包含未引用滤镜结构字符(value, "[];")
    End Function

    Private Shared Function 添加完整滤镜图链(图段 As List(Of String),
                                      滤镜链 As List(Of String),
                                      输出标签 As String,
                                      中间标签前缀 As String,
                                      输出标签集合 As List(Of String)) As Boolean
        If 滤镜链 Is Nothing OrElse 滤镜链.Count = 0 Then Return False
        If 滤镜链.Count = 1 AndAlso Not 可作为输出滤镜片段(滤镜链(0)) Then
            图段.Add(滤镜链(0))
            输出标签集合.AddRange(获取完整滤镜图输出标签(滤镜链(0)))
            Return True
        End If

        If 可作为输出滤镜片段(滤镜链(0)) OrElse Not 滤镜链.Skip(1).All(Function(x) 可作为输出滤镜片段(x)) Then Return False

        Dim labels = 获取完整滤镜图输出标签(滤镜链(0))
        If labels.Count = 0 Then
            Dim 后续线性滤镜 = 滤镜链.Skip(1).Where(Function(x) 可作为输出滤镜片段(x)).ToList()
            图段.Add(滤镜链(0) & If(后续线性滤镜.Count > 0, "," & String.Join(",", 后续线性滤镜), ""))
            Return True
        End If
        If labels.Count <> 1 Then
            添加完整滤镜图原样片段(图段, 滤镜链, 输出标签集合)
            Return True
        End If

        图段.Add(滤镜链(0))
        Dim 当前标签 = labels(0)
        For i = 1 To 滤镜链.Count - 1
            Dim 下一个标签 = If(i = 滤镜链.Count - 1, 输出标签, $"{中间标签前缀}step{i}")
            图段.Add($"[{当前标签}]{滤镜链(i)}[{下一个标签}]")
            当前标签 = 下一个标签
        Next
        输出标签集合.Add(输出标签)
        Return True
    End Function

    Private Shared Sub 添加完整滤镜图原样片段(图段 As List(Of String), 滤镜链 As List(Of String), 输出标签集合 As List(Of String))
        For Each 片段 In 滤镜链.Where(Function(x) Not 可作为输出滤镜片段(x))
            图段.Add(片段)
            输出标签集合.AddRange(获取完整滤镜图输出标签(片段))
        Next
    End Sub

    Private Shared Sub 添加完整滤镜图输出映射(映射 As List(Of String), 编码选择器 As List(Of String), 输出标签集合 As List(Of String), 流类型 As String)
        If 输出标签集合 Is Nothing OrElse 输出标签集合.Count = 0 Then
            编码选择器.Add($"{流类型}:0")
            Exit Sub
        End If

        Dim index = 0
        For Each label In 输出标签集合.Where(Function(x) Not String.IsNullOrWhiteSpace(x)).Distinct(StringComparer.Ordinal)
            映射.Add($"-map [{label}]?")
            编码选择器.Add($"{流类型}:{index}")
            index += 1
        Next
    End Sub

    Private Shared Function 获取完整滤镜图输出标签(滤镜图 As String) As List(Of String)
        Dim counts As New Dictionary(Of String, Integer)(StringComparer.Ordinal)
        For Each rawLabel In 获取未引用滤镜标签(滤镜图)
            Dim label = rawLabel.Trim()
            If label = "" OrElse 是输入流标签(label) Then Continue For
            If counts.ContainsKey(label) Then
                counts(label) += 1
            Else
                counts(label) = 1
            End If
        Next
        Return counts.Where(Function(x) x.Value = 1).Select(Function(x) x.Key).ToList()
    End Function

    Private Shared Function 包含未引用滤镜结构字符(value As String, chars As String) As Boolean
        Dim inSingleQuote = False
        Dim inDoubleQuote = False
        Dim escaped = False
        For Each c In If(value, "")
            If escaped Then
                escaped = False
                Continue For
            End If
            If c = "\"c Then
                escaped = True
                Continue For
            End If
            If c = "'"c AndAlso Not inDoubleQuote Then
                inSingleQuote = Not inSingleQuote
                Continue For
            End If
            If c = """"c AndAlso Not inSingleQuote Then
                inDoubleQuote = Not inDoubleQuote
                Continue For
            End If
            If Not inSingleQuote AndAlso Not inDoubleQuote AndAlso chars.IndexOf(c) >= 0 Then Return True
        Next
        Return False
    End Function

    Private Shared Function 获取未引用滤镜标签(滤镜图 As String) As List(Of String)
        Dim result As New List(Of String)
        Dim value = If(滤镜图, "")
        Dim inSingleQuote = False
        Dim inDoubleQuote = False
        Dim escaped = False
        Dim labelStart = -1

        For i = 0 To value.Length - 1
            Dim c = value(i)
            If escaped Then
                escaped = False
                Continue For
            End If
            If c = "\"c Then
                escaped = True
                Continue For
            End If
            If labelStart >= 0 Then
                If c = "]"c Then
                    result.Add(value.Substring(labelStart + 1, i - labelStart - 1))
                    labelStart = -1
                End If
                Continue For
            End If
            If c = "'"c AndAlso Not inDoubleQuote Then
                inSingleQuote = Not inSingleQuote
                Continue For
            End If
            If c = """"c AndAlso Not inSingleQuote Then
                inDoubleQuote = Not inDoubleQuote
                Continue For
            End If
            If inSingleQuote OrElse inDoubleQuote Then Continue For
            If c = "["c Then labelStart = i
        Next

        Return result
    End Function

    Private Shared Function 是输入流标签(label As String) As Boolean
        Dim value = If(label, "").Trim()
        If value = "" Then Return False
        If value.Any(Function(c) c = ":"c) Then Return True
        Return value.All(Function(c) Char.IsDigit(c))
    End Function

    Private Shared Function 清理线性滤镜片段(片段 As String) As String
        Dim result = If(片段, "").Trim()
        While result.StartsWith(","c)
            result = result.Substring(1).TrimStart()
        End While
        While result.EndsWith(","c)
            result = result.Substring(0, result.Length - 1).TrimEnd()
        End While
        Return result
    End Function

    Private Shared Function 构造Trim滤镜(a As 预设数据_v6, 音频 As Boolean) As String
        If a.剪辑区间_方法 <> 预设数据_v6.剪辑方法.Trim滤镜 Then Return ""
        Dim 入点 = If(a.剪辑区间_入点, "").Trim()
        Dim 出点 = If(a.剪辑区间_出点, "").Trim()
        Dim startValue = 转换Trim时间值(入点)
        Dim endValue = 转换Trim时间值(出点)
        Dim opts As New List(Of String)
        If startValue <> "" Then opts.Add("start=" & startValue)
        If endValue <> "" Then opts.Add("end=" & endValue)
        If opts.Count = 0 Then Return ""
        Return If(音频, "atrim=", "trim=") & String.Join(":", opts) & If(音频, ",asetpts=PTS-STARTPTS", ",setpts=PTS-STARTPTS")
    End Function

    Private Shared Function 转换Trim时间值(value As String) As String
        Dim raw = If(value, "").Trim()
        If raw = "" Then Return ""
        Dim seconds As Double
        If TryParseTimeSeconds(raw, seconds) Then Return FormatSeconds(seconds)
        If raw.Contains(":"c) Then Return ""
        Return raw
    End Function

    Private Shared Function 可选输入流映射(stream As String) As String
        Dim s = If(stream, "").Trim()
        If s = "" Then Return s
        If s.StartsWith("["c) AndAlso s.EndsWith("]"c, StringComparison.Ordinal) Then Return s & "?"

        Dim 排除映射 = s.StartsWith("-"c, StringComparison.Ordinal)
        If 排除映射 Then s = s.Substring(1).TrimStart()
        s = s.TrimEnd("?"c)
        If s.StartsWith("["c) AndAlso s.EndsWith("]"c, StringComparison.Ordinal) Then Return If(排除映射, "-", "") & s & "?"
        If s = "" Then Return ""
        Return If(排除映射, "-", "") & s & "?"
    End Function

    Private Shared Sub 添加线性滤镜链图段(图段 As List(Of String), 输入标签 As String, 滤镜链 As List(Of String), 输出标签 As String, 中间标签前缀 As String)
        Dim 当前标签 = 输入标签
        For i = 0 To 滤镜链.Count - 1
            Dim 下一个标签 = If(i = 滤镜链.Count - 1, 输出标签, $"{中间标签前缀}step{i}")
            图段.Add($"[{当前标签}]{滤镜链(i)}[{下一个标签}]")
            当前标签 = 下一个标签
        Next
    End Sub

    Private Shared Function 获取保留映射输出流选择器(stream As String, 类型 As String) As String
        Dim s = If(stream, "").Trim().TrimEnd("?"c)
        Dim parts = s.Split(":"c, StringSplitOptions.RemoveEmptyEntries)
        If parts.Length >= 2 AndAlso String.Equals(parts(parts.Length - 2), 类型, StringComparison.OrdinalIgnoreCase) Then
            Dim index As Integer
            If Integer.TryParse(parts(parts.Length - 1), index) Then Return $"{类型}:{index}"
        End If
        If parts.Length >= 2 AndAlso String.Equals(parts(parts.Length - 1), 类型, StringComparison.OrdinalIgnoreCase) Then Return 类型
        Return 类型
    End Function

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
                添加NVENC码率控制参数(parts, a, 控制方式, 输出流选择器)
                添加编码器参数(parts, 获取视频质量参数名(a.视频参数_质量控制_参数名, "qp"), a.视频参数_质量控制_值, 输出流选择器)
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

    Private Shared Function 是NVENC编码器(编码器 As String) As Boolean
        Dim value = If(编码器, "").Trim()
        Return value.EndsWith("_nvenc", StringComparison.OrdinalIgnoreCase)
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
        If [Enum].IsDefined(GetType(预设数据_v6.视频全局质量控制方式), value) Then Return value
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

    Private Shared Function 构造缩放滤镜(a As 预设数据_v6) As String
        If a.视频参数_分辨率 <> "" Then Return $"scale={a.视频参数_分辨率.Replace("x", ":")}"

        Dim 宽度 = If(a.视频参数_分辨率自动计算_宽度, "")
        Dim 高度 = If(a.视频参数_分辨率自动计算_高度, "")
        If 宽度 = "" AndAlso 高度 = "" Then Return ""
        If 宽度 = "" Then 宽度 = "-2"
        If 高度 = "" Then 高度 = "-2"
        Return $"scale={宽度}:{高度}"
    End Function

    Private Shared Function 构造裁剪滤镜(a As 预设数据_v6) As String
        Dim value = If(a.视频参数_分辨率_裁剪滤镜参数, "").Trim()
        Return If(value <> "", "crop=" & value, "")
    End Function

    Private Shared Function 构造抽帧滤镜(a As 预设数据_v6) As String
        Dim opts As New List(Of String)
        If a.视频参数_抽帧_max <> "" Then opts.Add("max=" & a.视频参数_抽帧_max)
        If a.视频参数_抽帧_keep <> "" Then opts.Add("keep=" & a.视频参数_抽帧_keep)
        If a.视频参数_抽帧_hi <> "" Then opts.Add("hi=" & a.视频参数_抽帧_hi)
        If a.视频参数_抽帧_lo <> "" Then opts.Add("lo=" & a.视频参数_抽帧_lo)
        If a.视频参数_抽帧_frac <> "" Then opts.Add("frac=" & a.视频参数_抽帧_frac)
        Return If(opts.Count > 0, "mpdecimate=" & String.Join(":", opts), "")
    End Function

    Private Shared Function 构造插帧滤镜(a As 预设数据_v6) As String
        If a.视频参数_插帧_目标帧率 = "" Then Return ""
        Dim opts As New List(Of String) From {$"fps={a.视频参数_插帧_目标帧率}"}
        Dim 插帧模式 = 插帧模式参数值(a.视频参数_插帧_插帧模式)
        Dim 运动估计模式 = 运动估计模式参数值(a.视频参数_插帧_运动估计模式)
        Dim 运动估计算法 = 运动估计算法参数值(a.视频参数_插帧_运动估计算法)
        Dim 运动补偿模式 = 运动补偿模式参数值(a.视频参数_插帧_运动补偿模式)
        If 插帧模式 <> "" Then opts.Add("mi_mode=" & 插帧模式)
        If 运动估计模式 <> "" Then opts.Add("me_mode=" & 运动估计模式)
        If 运动估计算法 <> "" Then opts.Add("me=" & 运动估计算法)
        If 运动补偿模式 <> "" Then opts.Add("mc_mode=" & 运动补偿模式)
        If a.视频参数_插帧_可变块大小的运动补偿 Then opts.Add("vsbmc=1")
        If a.视频参数_插帧_块大小 <> "" Then opts.Add("mb_size=" & a.视频参数_插帧_块大小)
        If a.视频参数_插帧_搜索范围 <> "" Then opts.Add("search_param=" & a.视频参数_插帧_搜索范围)
        If a.视频参数_插帧_场景变化检测强度 <> "" Then opts.Add("scd_threshold=" & a.视频参数_插帧_场景变化检测强度)
        Return "minterpolate=" & String.Join(":", opts)
    End Function

    Private Shared Function 构造动态模糊滤镜(a As 预设数据_v6) As String
        If a.视频参数_动态模糊_连续混合帧数 = "" Then Return ""
        Dim opts As New List(Of String) From {$"frames={a.视频参数_动态模糊_连续混合帧数}"}
        If a.视频参数_动态模糊_每帧权重 <> "" Then opts.Add("weights=" & a.视频参数_动态模糊_每帧权重)
        If a.视频参数_动态模糊_输出缩放系数 <> "" Then opts.Add("scale=" & a.视频参数_动态模糊_输出缩放系数)
        If a.视频参数_动态模糊_处理颜色平面 <> "" Then opts.Add("planes=" & a.视频参数_动态模糊_处理颜色平面)
        Return "tmix=" & String.Join(":", opts)
    End Function

    Private Shared Function 构造超分滤镜(a As 预设数据_v6) As String
        Dim list As New List(Of 预设数据_v6.超分数据单片结构)
        If a.视频参数_超分_滤镜叠加策略组 IsNot Nothing AndAlso a.视频参数_超分_滤镜叠加策略组.Length > 0 Then
            list.AddRange(a.视频参数_超分_滤镜叠加策略组)
        ElseIf a.视频参数_超分_直接面板 IsNot Nothing Then
            list.Add(a.视频参数_超分_直接面板)
        End If
        Dim filters As New List(Of String)
        For Each s In list
            If s Is Nothing Then Continue For
            Dim 目标宽度 = If(s.目标宽度, "")
            Dim 目标高度 = If(s.目标高度, "")
            Dim libplaceboOpts As New List(Of String)
            If 目标宽度 <> "" Then libplaceboOpts.Add("w=" & 目标宽度)
            If 目标高度 <> "" Then libplaceboOpts.Add("h=" & 目标高度)
            If s.上采样算法 <> "" Then libplaceboOpts.Add("upscaler=" & s.上采样算法)
            If s.下采样算法 <> "" Then libplaceboOpts.Add("downscaler=" & s.下采样算法)
            If s.抗振铃强度 <> "" Then libplaceboOpts.Add("antiringing=" & s.抗振铃强度)
            If s.着色器文件路径 <> "" Then libplaceboOpts.Add($"custom_shader_path='{转义字幕滤镜值(应用转译模式路径(s.着色器文件路径))}'")
            If libplaceboOpts.Count > 0 Then
                filters.Add("libplacebo=" & String.Join(":", libplaceboOpts))
            End If
        Next
        Return String.Join(",", filters)
    End Function

    Private Shared Function 构造降噪滤镜(a As 预设数据_v6) As String
        Select Case a.视频参数_降噪_方式
            Case 预设数据_v6.降噪方式.hqdn3d
                Return $"hqdn3d={JoinNonEmpty(":", a.视频参数_降噪_参数1, a.视频参数_降噪_参数2, a.视频参数_降噪_参数3, a.视频参数_降噪_参数4)}".TrimEnd("="c)
            Case 预设数据_v6.降噪方式.nlmeans
                Return $"nlmeans={JoinNamed(":", ("s", a.视频参数_降噪_参数1), ("p", a.视频参数_降噪_参数2), ("pc", a.视频参数_降噪_参数3), ("r", a.视频参数_降噪_参数4))}".TrimEnd("="c)
            Case 预设数据_v6.降噪方式.atadenoise
                Return $"atadenoise={JoinNamed(":", ("0a", a.视频参数_降噪_参数1), ("0b", a.视频参数_降噪_参数2), ("1a", a.视频参数_降噪_参数3), ("1b", a.视频参数_降噪_参数4))}".TrimEnd("="c)
            Case 预设数据_v6.降噪方式.bm3d
                Return $"bm3d={JoinNamed(":", ("sigma", a.视频参数_降噪_参数1), ("block", a.视频参数_降噪_参数2), ("bstep", a.视频参数_降噪_参数3), ("group", a.视频参数_降噪_参数4))}".TrimEnd("="c)
            Case 预设数据_v6.降噪方式.bilateral_cuda
                Return $"bilateral_cuda={JoinNamed(":", ("sigmaS", a.视频参数_降噪_参数1), ("sigmaR", a.视频参数_降噪_参数2), ("window_size", a.视频参数_降噪_参数3))}".TrimEnd("="c)
        End Select
        Return ""
    End Function

    Private Shared Function 构造锐化滤镜(a As 预设数据_v6) As String
        Select Case a.视频参数_锐化_方式
            Case 预设数据_v6.锐化方式.cas
                Return $"cas={JoinNamed(":", ("strength", a.视频参数_锐化_参数1), ("planes", a.视频参数_锐化_参数2))}".TrimEnd("="c)
            Case 预设数据_v6.锐化方式.unsharp
                Return $"unsharp={JoinNonEmpty(":", a.视频参数_锐化_参数1, a.视频参数_锐化_参数2, a.视频参数_锐化_参数3)}".TrimEnd("="c)
        End Select
        Return ""
    End Function

    Private Shared Function 构造胶片颗粒滤镜(a As 预设数据_v6) As String
        Select Case a.视频参数_胶片颗粒_方式
            Case 预设数据_v6.胶片颗粒方式.noise_全平面动态均匀颗粒
                Return 构造命名滤镜("noise",
                    ("alls", a.视频参数_胶片颗粒_参数1),
                    ("allf", If(a.视频参数_胶片颗粒_参数1 <> "", "t+u", "")),
                    ("all_seed", a.视频参数_胶片颗粒_参数2))
            Case 预设数据_v6.胶片颗粒方式.noise_亮度为主动态颗粒
                Return 构造命名滤镜("noise",
                    ("c0s", a.视频参数_胶片颗粒_参数1),
                    ("c0f", If(a.视频参数_胶片颗粒_参数1 <> "", "t+u", "")),
                    ("c1s", a.视频参数_胶片颗粒_参数2),
                    ("c1f", If(a.视频参数_胶片颗粒_参数2 <> "", "t+u", "")),
                    ("c2s", a.视频参数_胶片颗粒_参数2),
                    ("c2f", If(a.视频参数_胶片颗粒_参数2 <> "", "t+u", "")),
                    ("all_seed", a.视频参数_胶片颗粒_参数3))
            Case 预设数据_v6.胶片颗粒方式.noise_柔和平均颗粒
                Return 构造命名滤镜("noise",
                    ("alls", a.视频参数_胶片颗粒_参数1),
                    ("allf", If(a.视频参数_胶片颗粒_参数1 <> "", "t+a+u", "")),
                    ("all_seed", a.视频参数_胶片颗粒_参数2))
            Case 预设数据_v6.胶片颗粒方式.libplacebo_应用片源胶片颗粒元数据
                Return "libplacebo=apply_filmgrain=true"
        End Select
        Return ""
    End Function

    Private Shared Function 获取容器兼容字幕编码(字幕编码 As String, a As 预设数据_v6, 输出文件 As String, 自动混流 As Boolean) As String
        Dim codec = If(字幕编码, "").Trim()
        If codec = "" Then Return ""

        If 输出容器支持MovText字幕(a, 输出文件) Then
            If codec = "copy" AndAlso Not 自动混流 Then Return codec
            Return "mov_text"
        End If

        If 输出容器支持WebVtt字幕(a, 输出文件) Then
            If codec = "copy" AndAlso Not 自动混流 Then Return codec
            Return "webvtt"
        End If

        If 输出容器支持附件(a, 输出文件) AndAlso String.Equals(codec, "mov_text", StringComparison.OrdinalIgnoreCase) Then Return "copy"
        Return codec
    End Function

    Private Shared Function 构造平滑断层滤镜(a As 预设数据_v6) As String
        Select Case a.视频参数_平滑断层_方式
            Case 预设数据_v6.平滑断层方式.deband_标准去色带, 预设数据_v6.平滑断层方式.deband_强力去色带
                Return 构造命名滤镜("deband",
                    ("1thr", a.视频参数_平滑断层_参数1),
                    ("2thr", a.视频参数_平滑断层_参数1),
                    ("3thr", a.视频参数_平滑断层_参数1),
                    ("range", a.视频参数_平滑断层_参数2),
                    ("direction", a.视频参数_平滑断层_参数3),
                    ("blur", "1"),
                    ("coupling", a.视频参数_平滑断层_参数4))
            Case 预设数据_v6.平滑断层方式.gradfun_快速渐变平滑
                Return 构造命名滤镜("gradfun",
                    ("strength", a.视频参数_平滑断层_参数1),
                    ("radius", a.视频参数_平滑断层_参数2))
            Case 预设数据_v6.平滑断层方式.libplacebo_GPU去色带加颗粒
                Return 构造命名滤镜("libplacebo",
                    ("deband", "true"),
                    ("deband_iterations", a.视频参数_平滑断层_参数1),
                    ("deband_threshold", a.视频参数_平滑断层_参数2),
                    ("deband_radius", a.视频参数_平滑断层_参数3),
                    ("deband_grain", a.视频参数_平滑断层_参数4))
        End Select
        Return ""
    End Function

    Private Shared Function 构造扫描方式滤镜(a As 预设数据_v6) As String
        Select Case a.视频参数_处理扫描方式
            Case 预设数据_v6.扫描方式.yadif_单帧输入_自动场序_空间检查, 预设数据_v6.扫描方式.PAL_标准反交错
                Return "yadif=mode=send_frame:parity=auto:deint=all"
            Case 预设数据_v6.扫描方式.yadif_单帧输入_顶场优先_空间检查
                Return "yadif=mode=send_frame:parity=tff:deint=all"
            Case 预设数据_v6.扫描方式.yadif_单帧输入_底场优先_空间检查
                Return "yadif=mode=send_frame:parity=bff:deint=all"
            Case 预设数据_v6.扫描方式.tinterlace_顶场优先
                Return "tinterlace=mode=interleave_top"
            Case 预设数据_v6.扫描方式.tinterlace_底场优先
                Return "tinterlace=mode=interleave_bottom"
            Case 预设数据_v6.扫描方式.NTSC_标准IVTC_胶片32Pulldown转逐行
                Return "fieldmatch,yadif=deint=interlaced,decimate"
            Case 预设数据_v6.扫描方式.NTSC_纯隔行_非胶片转逐行, 预设数据_v6.扫描方式.PAL_标准反交错_双倍帧率
                Return "yadif=mode=send_field:parity=auto:deint=all"
            Case 预设数据_v6.扫描方式.NTSC_自动检测Pulldown至25fps
                Return "pullup=jl=1:jr=1,fps=25"
            Case 预设数据_v6.扫描方式.PAL_高质量反交错
                Return "bwdif=mode=send_frame:parity=auto:deint=all"
            Case 预设数据_v6.扫描方式.PAL_高质量反交错_双倍帧率
                Return "bwdif=mode=send_field:parity=auto:deint=all"
            Case 预设数据_v6.扫描方式.yadif_cuda_自动场序
                Return "yadif_cuda=mode=send_frame:parity=auto:deint=all"
            Case 预设数据_v6.扫描方式.bwdif_cuda_自动场序
                Return "bwdif_cuda=mode=send_frame:parity=auto:deint=all"
        End Select
        Return ""
    End Function

    <CodeAnalysis.SuppressMessage("Performance", "CA1861:不要将常量数组作为参数", Justification:="<挂起>")>
    Private Shared Function 构造翻转滤镜(a As 预设数据_v6) As String
        Dim filters As New List(Of String)
        Select Case a.视频参数_画面翻转_角度翻转
            Case 预设数据_v6.画面翻转角度.顺时针旋转90度 : filters.Add("transpose=1")
            Case 预设数据_v6.画面翻转角度.顺时针旋转180度 : filters.AddRange({"transpose=1", "transpose=1"})
            Case 预设数据_v6.画面翻转角度.顺时针旋转270度 : filters.AddRange({"transpose=1", "transpose=1", "transpose=1"})
            Case 预设数据_v6.画面翻转角度.逆时针旋转90度 : filters.Add("transpose=2")
            Case 预设数据_v6.画面翻转角度.逆时针旋转180度 : filters.AddRange({"transpose=2", "transpose=2"})
            Case 预设数据_v6.画面翻转角度.逆时针旋转270度 : filters.AddRange({"transpose=2", "transpose=2", "transpose=2"})
        End Select
        Select Case a.视频参数_画面翻转_镜像翻转
            Case 预设数据_v6.画面翻转镜像.水平镜像 : filters.Add("hflip")
            Case 预设数据_v6.画面翻转镜像.垂直镜像 : filters.Add("vflip")
        End Select
        Return String.Join(",", filters)
    End Function

    Private Shared Function 构造烧字幕滤镜(a As 预设数据_v6, 输入文件 As String) As String
        If a.视频参数_烧录字幕_自己写滤镜取代所有设置 <> "" Then Return 应用自定义参数通配字符串(a.视频参数_烧录字幕_自己写滤镜取代所有设置, 输入文件, 输出占位符)
        If a.视频参数_烧录字幕_滤镜选择 = 预设数据_v6.烧字幕滤镜.未选择 Then Return ""

        Dim 滤镜参数列表 As New List(Of String)
        Dim 样式参数列表 As New List(Of String)
        Dim 字幕文件 As String = ""
        Dim 需要内嵌流 As Boolean = False
        Dim 有字幕来源 As Boolean = False

        If a.视频参数_烧录字幕_字幕来源是外部文件 = 预设数据_v6.烧字幕来源.外部字幕文件 Then
            字幕文件 = 解析外部字幕文件(a, 输入文件)
            If 字幕文件 <> "" Then
                有字幕来源 = True
                滤镜参数列表.Add($"filename='{转义字幕滤镜值(应用转译模式路径(字幕文件))}'")
            End If
        End If
        If a.视频参数_烧录字幕_字幕来源是外部文件 = 预设数据_v6.烧字幕来源.内嵌的流 AndAlso a.视频参数_烧录字幕_指定内嵌的流 <> "" Then
            需要内嵌流 = True
            有字幕来源 = True
            滤镜参数列表.Add($"filename='{转义字幕滤镜值(应用转译模式路径(输入文件))}'")
            滤镜参数列表.Add($"stream_index={a.视频参数_烧录字幕_指定内嵌的流}")
        End If
        If Not 有字幕来源 Then Return ""

        If a.视频参数_烧录字幕_字体文件夹 <> "" Then 滤镜参数列表.Add($"fontsdir='{转义字幕滤镜值(应用转译模式路径(a.视频参数_烧录字幕_字体文件夹))}'")
        If a.视频参数_烧录字幕_基本样式_名称 <> "" Then 样式参数列表.Add($"FontName={a.视频参数_烧录字幕_基本样式_名称}")
        If a.视频参数_烧录字幕_基本样式_大小 <> 0 Then 样式参数列表.Add($"FontSize={a.视频参数_烧录字幕_基本样式_大小.ToString(CultureInfo.InvariantCulture)}")
        If a.视频参数_烧录字幕_基本样式_粗体 Then 样式参数列表.Add("Bold=-1")
        If a.视频参数_烧录字幕_基本样式_斜体 Then 样式参数列表.Add("Italic=-1")
        If a.视频参数_烧录字幕_基本样式_下划线 Then 样式参数列表.Add("Underline=-1")
        If a.视频参数_烧录字幕_基本样式_删除线 Then 样式参数列表.Add("StrikeOut=-1")
        Select Case a.视频参数_烧录字幕_边框样式
            Case 预设数据_v6.烧字幕边框样式.边框_阴影 : 样式参数列表.Add("BorderStyle=1")
            Case 预设数据_v6.烧字幕边框样式.背景框 : 样式参数列表.Add("BorderStyle=3")
        End Select
        If a.视频参数_烧录字幕_描边宽度 <> "" Then 样式参数列表.Add($"Outline={a.视频参数_烧录字幕_描边宽度}")
        If a.视频参数_烧录字幕_阴影距离 <> "" Then 样式参数列表.Add($"Shadow={a.视频参数_烧录字幕_阴影距离}")
        添加字幕颜色样式(样式参数列表, "PrimaryColour", a.视频参数_烧录字幕_主要颜色)
        添加字幕颜色样式(样式参数列表, "SecondaryColour", a.视频参数_烧录字幕_次要颜色)
        添加字幕颜色样式(样式参数列表, "OutlineColour", a.视频参数_烧录字幕_描边颜色)
        添加字幕颜色样式(样式参数列表, "BackColour", a.视频参数_烧录字幕_背景颜色)
        If a.视频参数_烧录字幕_对齐方位 <> 预设数据_v6.烧字幕对齐方位.未选择 Then 样式参数列表.Add($"Alignment={CInt(a.视频参数_烧录字幕_对齐方位)}")
        If a.视频参数_烧录字幕_垂直边距 <> "" Then 样式参数列表.Add($"MarginV={a.视频参数_烧录字幕_垂直边距}")
        If a.视频参数_烧录字幕_左边距 <> "" Then 样式参数列表.Add($"MarginL={a.视频参数_烧录字幕_左边距}")
        If a.视频参数_烧录字幕_右边距 <> "" Then 样式参数列表.Add($"MarginR={a.视频参数_烧录字幕_右边距}")
        If a.视频参数_烧录字幕_字距 <> "" Then 样式参数列表.Add($"Spacing={a.视频参数_烧录字幕_字距}")
        If a.视频参数_烧录字幕_行距 <> "" Then 样式参数列表.Add($"LineSpacing={a.视频参数_烧录字幕_行距}")
        If a.视频参数_烧录字幕_补充样式 <> "" Then 样式参数列表.Add(a.视频参数_烧录字幕_补充样式)
        If 样式参数列表.Count > 0 Then 滤镜参数列表.Add($"force_style='{String.Join(",", 样式参数列表).Replace("'", "\'")}'")

        Dim name = 获取烧字幕滤镜名(a, 字幕文件, 需要内嵌流, 样式参数列表.Count > 0)
        Return If(滤镜参数列表.Count > 0, $"{name}={String.Join(":", 滤镜参数列表)}", name)
    End Function

    Private Shared Function 获取烧字幕滤镜名(a As 预设数据_v6, 字幕文件 As String, 需要内嵌流 As Boolean, 需要强制样式 As Boolean) As String
        If a.视频参数_烧录字幕_滤镜选择 <> 预设数据_v6.烧字幕滤镜.ass Then Return "subtitles"
        If 需要内嵌流 OrElse 需要强制样式 Then Return "subtitles"

        Dim ext = Path.GetExtension(If(字幕文件, "")).ToLowerInvariant()
        If ext = ".ass" OrElse ext = ".ssa" Then Return "ass"
        If 是输入文件占位符(字幕文件) OrElse ext = "" Then Return "ass"
        Return "subtitles"
    End Function

    Private Shared Function 解析外部字幕文件(a As 预设数据_v6, 输入文件 As String) As String
        If 输入文件 = 输入占位符 OrElse 输入文件 = "<InputFile>" OrElse String.IsNullOrWhiteSpace(输入文件) Then
            Return "<字幕文件 | 预览模式专用字符>"
        End If

        Dim 字幕位置 = If(String.IsNullOrWhiteSpace(a.视频参数_烧录字幕_外部字幕文件夹位置), 获取路径目录保持分隔符(输入文件), a.视频参数_烧录字幕_外部字幕文件夹位置)
        Dim 字幕文件名 = If(String.IsNullOrWhiteSpace(a.视频参数_烧录字幕_外部字幕文件名), 获取路径文件名不含扩展名保持分隔符(输入文件), a.视频参数_烧录字幕_外部字幕文件名)
        If String.IsNullOrWhiteSpace(字幕文件名) Then Return ""

        Dim 格式列表 = If(a.视频参数_烧录字幕_字幕格式优先级, New List(Of 预设数据_v6.烧字幕格式)).
            Where(Function(x) x <> 预设数据_v6.烧字幕格式.未选择).
            Distinct().
            ToList()
        If 格式列表.Count = 0 Then 格式列表.AddRange({预设数据_v6.烧字幕格式.SRT, 预设数据_v6.烧字幕格式.ASS, 预设数据_v6.烧字幕格式.SSA})

        Dim 候选 As New List(Of String)
        For Each 格式 In 格式列表
            Dim ext = "." & 格式.ToString().ToLowerInvariant()
            候选.Add(If(String.IsNullOrWhiteSpace(字幕位置), 字幕文件名 & ext, 合并路径保持分隔符(字幕位置, 字幕文件名 & ext, 字幕位置)))
        Next
        Dim 已存在 = 候选.FirstOrDefault(Function(x) File.Exists(x))
        Return If(已存在, "")
    End Function

    Private Shared Function 烧字幕滤镜已设置(a As 预设数据_v6, Optional 输入文件 As String = 输入占位符) As Boolean
        Return Not String.IsNullOrWhiteSpace(构造烧字幕滤镜(a, 输入文件))
    End Function

    Private Shared Function 内置烧字幕缺少必要来源设置(a As 预设数据_v6) As Boolean
        If a Is Nothing Then Return False
        If Not String.IsNullOrWhiteSpace(a.视频参数_烧录字幕_自己写滤镜取代所有设置) Then Return False
        If a.视频参数_烧录字幕_滤镜选择 = 预设数据_v6.烧字幕滤镜.未选择 Then Return False
        Select Case a.视频参数_烧录字幕_字幕来源是外部文件
            Case 预设数据_v6.烧字幕来源.外部字幕文件
                Return False
            Case 预设数据_v6.烧字幕来源.内嵌的流
                Return String.IsNullOrWhiteSpace(a.视频参数_烧录字幕_指定内嵌的流)
            Case Else
                Return True
        End Select
    End Function

    Private Shared Sub 添加字幕颜色样式(列表 As List(Of String), 名称 As String, 颜色 As 预设数据_v6.烧字幕专用颜色类型)
        If Not 字幕颜色已设置(颜色) Then Exit Sub
        列表.Add($"{名称}=&H{限制颜色通道(颜色.A):X2}{限制颜色通道(颜色.B):X2}{限制颜色通道(颜色.G):X2}{限制颜色通道(颜色.R):X2}")
    End Sub

    Private Shared Function 转义字幕滤镜值(value As String) As String
        If value Is Nothing Then Return ""
        If value.StartsWith("<"c) AndAlso value.EndsWith(">"c) Then Return value
        Return 将路径转换为FFmpeg滤镜接受的格式(value).Replace("'", "\'")
    End Function

    Private Shared Function 构造色彩转换滤镜(a As 预设数据_v6) As String
        If a Is Nothing Then Return ""
        If String.Equals(If(a.视频参数_色彩管理_处理方式, "").Trim(), "仅写入元数据", StringComparison.Ordinal) Then Return ""
        Dim filterName = 获取色彩转换滤镜名(a)
        Dim opts As New List(Of String)
        Dim 矩阵系数 = 标准化色彩滤镜矩阵值(a.视频参数_色彩管理_矩阵系数, filterName)
        Dim 色域 = 标准化色彩值(a.视频参数_色彩管理_色域, False)
        Dim 传输特性 = 标准化色彩值(a.视频参数_色彩管理_传输特性, False)
        Dim 色彩范围 = 标准化色彩范围(a.视频参数_色彩管理_范围, False)
        Dim 色调映射算法 = 标准化色彩值(a.视频参数_色彩管理_色调映射算法, False)

        Select Case filterName.ToLowerInvariant()
            Case "colorspace"
                If 矩阵系数 <> "" Then opts.Add("space=" & 矩阵系数)
                If 色域 <> "" Then opts.Add("primaries=" & 色域)
                If 传输特性 <> "" Then opts.Add("trc=" & 传输特性)
                If 色彩范围 <> "" Then opts.Add("range=" & 色彩范围)
            Case "zscale"
                If 矩阵系数 <> "" Then opts.Add("matrix=" & 矩阵系数)
                If 色域 <> "" Then opts.Add("primaries=" & 色域)
                If 传输特性 <> "" Then opts.Add("transfer=" & 传输特性)
                If 色彩范围 <> "" Then opts.Add("range=" & 色彩范围)
            Case "libplacebo"
                If 矩阵系数 <> "" Then opts.Add("colorspace=" & 矩阵系数)
                If 色域 <> "" Then opts.Add("color_primaries=" & 色域)
                If 传输特性 <> "" Then opts.Add("color_trc=" & 传输特性)
                If 色彩范围 <> "" Then opts.Add("range=" & 色彩范围)
                If 色调映射算法 <> "" Then opts.Add("tonemapping=" & 色调映射算法)
            Case Else
                If 矩阵系数 <> "" Then opts.Add("matrix=" & 矩阵系数)
                If 色域 <> "" Then opts.Add("primaries=" & 色域)
                If 传输特性 <> "" Then opts.Add("transfer=" & 传输特性)
                If 色彩范围 <> "" Then opts.Add("range=" & 色彩范围)
        End Select
        If opts.Count = 0 Then Return ""
        Return filterName & "=" & String.Join(":", opts)
    End Function

    Private Shared Function 获取色彩转换滤镜名(a As 预设数据_v6) As String
        Dim filterName = If(a?.视频参数_色彩管理_滤镜选择, "").Trim()
        If filterName <> "" Then Return filterName
        If 标准化色彩值(a?.视频参数_色彩管理_色域, False) <> "" OrElse
           标准化色彩值(a?.视频参数_色彩管理_传输特性, False) <> "" OrElse
           标准化色彩范围(a?.视频参数_色彩管理_范围, False) <> "" OrElse
           标准化色彩值(a?.视频参数_色彩管理_色调映射算法, False) <> "" Then Return "libplacebo"
        If 标准化色彩值(a?.视频参数_色彩管理_矩阵系数, False) <> "" Then Return "colorspace"
        Return "libplacebo"
    End Function

    Private Shared Function 标准化色彩滤镜矩阵值(value As String, filterName As String) As String
        Dim raw = 标准化色彩值(value, False)
        If raw = "" Then Return ""
        If String.Equals(raw, "rgb", StringComparison.OrdinalIgnoreCase) Then Return "gbr"
        Return raw
    End Function

    Private Shared Function 构造像素格式预先转换滤镜(a As 预设数据_v6) As String
        Dim value = If(a.视频参数_色彩管理_像素格式预先转换, "").Trim()
        Return If(value <> "", "format=" & value, "")
    End Function

    Private Shared Function 构造调色滤镜(a As 预设数据_v6) As String
        Dim opts As New List(Of String)
        If a.视频参数_色彩管理_启用调整亮度 AndAlso a.视频参数_色彩管理_亮度 <> "" Then opts.Add("brightness=" & a.视频参数_色彩管理_亮度)
        If a.视频参数_色彩管理_启用调整对比度 AndAlso a.视频参数_色彩管理_对比度 <> "" Then opts.Add("contrast=" & a.视频参数_色彩管理_对比度)
        If a.视频参数_色彩管理_启用调整饱和度 AndAlso a.视频参数_色彩管理_饱和度 <> "" Then opts.Add("saturation=" & a.视频参数_色彩管理_饱和度)
        If a.视频参数_色彩管理_启用调整伽马 AndAlso a.视频参数_色彩管理_伽马 <> "" Then opts.Add("gamma=" & a.视频参数_色彩管理_伽马)
        Return If(opts.Count > 0, "eq=" & String.Join(":", opts), "")
    End Function

    Private Shared Function 构造响度滤镜(a As 预设数据_v6) As String
        Dim opts As New List(Of String)
        If a.音频参数_响度标准化_启用调整目标响度 AndAlso a.音频参数_响度标准化_目标响度 <> "" Then opts.Add("I=" & a.音频参数_响度标准化_目标响度)
        If a.音频参数_响度标准化_启用调整动态范围 AndAlso a.音频参数_响度标准化_动态范围 <> "" Then opts.Add("LRA=" & a.音频参数_响度标准化_动态范围)
        If a.音频参数_响度标准化_启用调整峰值电平 AndAlso a.音频参数_响度标准化_峰值电平 <> "" Then opts.Add("TP=" & a.音频参数_响度标准化_峰值电平)
        Return If(opts.Count > 0, "loudnorm=" & String.Join(":", opts), "")
    End Function

    Private Shared Function JoinNonEmpty(delimiter As String, ParamArray values() As String) As String
        Return String.Join(delimiter, values.Where(Function(x) Not String.IsNullOrWhiteSpace(x)))
    End Function

    Private Shared Function JoinNamed(delimiter As String, ParamArray values() As (Name As String, Value As String)) As String
        Return String.Join(delimiter, values.Where(Function(x) Not String.IsNullOrWhiteSpace(x.Value)).Select(Function(x) $"{x.Name}={x.Value}"))
    End Function

    Private Shared Function 构造命名滤镜(名称 As String, ParamArray values() As (Name As String, Value As String)) As String
        Dim opts = JoinNamed(":", values)
        Return If(opts = "", 名称, 名称 & "=" & opts)
    End Function

    Private Class 滤镜图结果
        Public Property 滤镜图 As String = ""
        Public Property 映射参数 As String = ""
        Public Property 输出滤镜参数 As String = ""
        Public Property 编码视频选择器 As New List(Of String)
        Public Property 编码音频选择器 As New List(Of String)
        Public Property 视频输出数量 As Integer = 0
        Public Property 音频输出数量 As Integer = 0
        Public Property 字幕输出数量 As Integer = 0
        Public Property 请求视频输出 As Boolean = False
        Public Property 请求音频输出 As Boolean = False
        Public Property 请求字幕输出 As Boolean = False
        Public Property 视频输出来自滤镜 As Boolean = False
        Public Property 音频输出来自滤镜 As Boolean = False
    End Class

End Class
