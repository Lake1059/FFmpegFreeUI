Imports System.Globalization
Imports System.IO
Imports System.Text
Imports System.Text.Json
Imports System.Text.RegularExpressions
Imports LakeUI

Partial Public Class 预设管理_v6

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


End Class
