Imports System.Globalization
Imports System.IO

Partial Public Class 预设管理_v6

    Private Shared Function 构造缩放滤镜(a As 预设数据_v6) As String
        Dim 缩放算法 = If(a.视频参数_分辨率自动计算_缩放算法, "").Trim()
        Dim 缩放参数 = If(缩放算法 = "", "", ":flags=" & 缩放算法)
        If a.视频参数_分辨率 <> "" Then Return $"scale={a.视频参数_分辨率.Replace("x", ":")}{缩放参数}"

        Dim 宽度 = If(a.视频参数_分辨率自动计算_宽度, "")
        Dim 高度 = If(a.视频参数_分辨率自动计算_高度, "")
        If 宽度 = "" AndAlso 高度 = "" Then Return ""
        If 宽度 = "" Then 宽度 = "-2"
        If 高度 = "" Then 高度 = "-2"
        Return $"scale={宽度}:{高度}{缩放参数}"
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
