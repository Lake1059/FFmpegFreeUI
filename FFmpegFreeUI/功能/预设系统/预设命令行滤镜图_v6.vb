Imports System.Globalization
Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions


Partial Public Class 预设管理_v6

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
            If Not inSingleQuote AndAlso Not inDoubleQuote AndAlso chars.Contains(c) Then Return True
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

End Class
