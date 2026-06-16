Public Class 视频编码器数据库_v6

    Private Shared ReadOnly 分类列表 As New List(Of 视频编码器分类数据)
    Private Shared ReadOnly 分类索引 As New Dictionary(Of String, 视频编码器分类数据)(StringComparer.OrdinalIgnoreCase)
    Private Shared ReadOnly 编码器索引 As New Dictionary(Of String, 视频编码器数据)(StringComparer.OrdinalIgnoreCase)

    Shared Sub New()
        初始化分类()
        初始化编码器()
    End Sub

    Public Shared ReadOnly Property 全部分类 As IReadOnlyList(Of 视频编码器分类数据)
        Get
            Return 分类列表.AsReadOnly()
        End Get
    End Property

    Public Shared ReadOnly Property 全部编码器 As IReadOnlyDictionary(Of String, 视频编码器数据)
        Get
            Return 编码器索引
        End Get
    End Property

    Public Shared Function 获取分类列表(类型 As 预设数据_v6.视频编码器类型) As List(Of 视频编码器分类数据)
        Return 分类列表.Where(Function(x) x.类型 = 类型).ToList()
    End Function

    Public Shared Function 获取分类数据(分类名称 As String) As 视频编码器分类数据
        Dim value As 视频编码器分类数据 = Nothing
        If 分类索引.TryGetValue(分类名称, value) Then Return value
        Return Nothing
    End Function

    Public Shared Function 获取编码器列表(分类名称 As String) As List(Of 视频编码器数据)
        Dim 分类 = 获取分类数据(分类名称)
        If 分类 Is Nothing Then Return New List(Of 视频编码器数据)

        If 分类名称 = "自定义" Then
            Return 设置_v6.实例对象.自定义视频编码器列表.
                Select(Function(x) 创建自定义编码器数据(x)).
                ToList()
        End If

        Dim 结果 As New List(Of 视频编码器数据)
        For Each 编码器名称 In 分类.编码器名称列表
            Dim 数据 = 获取编码器数据(编码器名称)
            If 数据 IsNot Nothing Then 结果.Add(数据)
        Next
        Return 结果
    End Function

    Public Shared Function 获取编码器数据(显示名称 As String) As 视频编码器数据
        Dim value As 视频编码器数据 = Nothing
        If 编码器索引.TryGetValue(显示名称, value) Then Return value
        If Not String.IsNullOrWhiteSpace(显示名称) Then Return 创建自定义编码器数据(显示名称)
        Return Nothing
    End Function

    Public Shared Function 获取参数名(显示名称 As String, 参数角色 As 编码器参数角色) As String
        Dim 数据 = 获取编码器数据(显示名称)
        If 数据 Is Nothing Then Return ""

        Select Case 参数角色
            Case 编码器参数角色.编码预设
                Return 数据.编码预设.参数名
            Case 编码器参数角色.配置文件
                Return 数据.配置文件.参数名
            Case 编码器参数角色.场景优化
                Return 数据.场景优化.参数名
            Case 编码器参数角色.像素格式
                Return 数据.像素格式.参数名
            Case 编码器参数角色.图片质量
                Return 数据.图片质量.参数名
            Case Else
                Return ""
        End Select
    End Function

    Public Shared Function 获取质量参数名列表() As List(Of String)
        Dim 结果 As New List(Of String) From {""}
        Dim 已存在 As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase) From {""}

        For Each 参数名 In 基础质量参数名()
            加入质量参数名(结果, 已存在, 参数名)
        Next

        For Each 编码器 In 编码器索引.Values
            For Each 参数项 In 编码器.特殊参数列表.Concat(编码器.必要参数列表).Concat(编码器.默认附加参数列表)
                If 是质量参数下拉框参数(参数项) Then 加入质量参数名(结果, 已存在, 参数项.参数名)
            Next
        Next

        Return 结果
    End Function

    Private Shared Function 基础质量参数名() As String()
        Return {
            "-crf",
            "-cq",
            "-global_quality",
            "-qp",
            "-qvbr_quality_level",
            "-lossless",
            "-rc",
            "-rc_mode",
            "-quality",
            "-init_qpI",
            "-init_qpP",
            "-init_qpB",
            "-qp_i",
            "-qp_p",
            "-qp_b",
            "-qmin",
            "-qmax",
            "-qpmin",
            "-qpmax",
            "-crf_max",
            "-min_qp_i",
            "-max_qp_i",
            "-min_qp_p",
            "-max_qp_p",
            "-min_qp_b",
            "-max_qp_b",
            "-aq-mode",
            "-aq-strength",
            "-spatial-aq",
            "-temporal-aq",
            "-rc-lookahead",
            "-lookahead_level",
            "-look_ahead",
            "-look_ahead_depth",
            "-la_depth",
            "-vbaq",
            "-aom-params",
            "-svtav1-params",
            "-rav1e-params",
            "-x264-params",
            "-x265-params",
            "-vvenc-params",
            "-kvazaar-params",
            "-xeve-params",
            "-xavs2-params",
            "-oapv-params"
        }
    End Function

    Private Shared Sub 加入质量参数名(结果 As List(Of String), 已存在 As HashSet(Of String), 参数名 As String)
        Dim 清理后参数名 = If(参数名, "").Trim()
        If 清理后参数名 = "" OrElse 已存在.Contains(清理后参数名) Then Exit Sub
        结果.Add(清理后参数名)
        已存在.Add(清理后参数名)
    End Sub

    Private Shared Function 是质量参数下拉框参数(参数 As 编码器特殊参数数据) As Boolean
        If 参数 Is Nothing Then Return False
        Dim 参数名 = If(参数.参数名, "").Trim()
        If 参数名 = "" Then Return False

        Select Case 参数名
            Case "-crf", "-cq", "-global_quality", "-qp", "-qvbr_quality_level",
                 "-lossless",
                 "-rc", "-rc_mode", "-quality",
                 "-init_qpI", "-init_qpP", "-init_qpB",
                 "-qp_i", "-qp_p", "-qp_b",
                 "-qmin", "-qmax", "-qpmin", "-qpmax",
                 "-min_qp_i", "-max_qp_i", "-min_qp_p", "-max_qp_p", "-min_qp_b", "-max_qp_b"
                Return True
        End Select

        Return 参数名.IndexOf("qp", StringComparison.OrdinalIgnoreCase) >= 0 OrElse
               参数名.IndexOf("quality", StringComparison.OrdinalIgnoreCase) >= 0
    End Function

    Private Shared Sub 初始化分类()
        加入分类(预设数据_v6.视频编码器类型.视频, "复制流", "复制流可以解决多数因为元数据或轻度数据异常问题导致的播放故障，常见于进度条无法拖动、无法获取时长等经典问题。在 ffmpeg 的定义中，图片也是视频，如果你正在编码音乐文件，也要选择这里的复制流来保留专辑图等图像信息。", "copy")
        加入分类(预设数据_v6.视频编码器类型.视频, "AV2", "等待 FFmpeg 添加支持。", "libaom-av2")
        加入分类(预设数据_v6.视频编码器类型.视频, "H.266/VVC", "超前，默秒全，代价是极高的性能要求，名副其实的国家电网战略合作伙伴；截止 2026 年，libx266 仍未发布，只有 libvvenc 一个选择。", "libvvenc")
        加入分类(预设数据_v6.视频编码器类型.视频, "AV1", "AOMedia Video 1 作为完全免费开源的高压缩编码，在当前非常适合个人存储，其压缩度比 HEVC 更高，但相应的性能消耗也更大。此编码格式强烈建议优先考虑最新显卡编码。", "libaom-av1", "libsvtav1", "av1_nvenc", "av1_qsv", "av1_amf", "av1_d3d12va", "librav1e", "av1_vulkan")
        加入分类(预设数据_v6.视频编码器类型.视频, "H.265/HEVC", "HEVC 作为当前最主流最全能的编码，正在被全球压制组、视频平台等渠道广泛使用。截止 2026 年，红绿蓝三家的 HEVC 压缩度距离最新版本的 x265 还有非常明显的距离。", "libx265", "hevc_nvenc", "hevc_qsv", "hevc_amf", "hevc_d3d12va", "hevc_vulkan", "libkvazaar", "libsvt_hevc")
        加入分类(预设数据_v6.视频编码器类型.视频, "H.264/AVC", "除非目标设备不支持新的编码，否则不应该考虑 AVC。如果要使用此编码格式进行压制工作，请只选择 libx264，红绿蓝三家的压缩度都拉完了，除非你完全不在乎压缩度。", "libx264", "libx264rgb", "libopenh264", "h264_nvenc", "h264_qsv", "h264_amf", "h264_d3d12va", "h264_vulkan")
        加入分类(预设数据_v6.视频编码器类型.视频, "ProRes", "ProRes 是苹果的无损格式，旨在提高剪辑效率，最终存储不应使用，而且 ProRes 本就是几乎没有压缩度可言的编码。", "prores_ks", "prores_aw")
        加入分类(预设数据_v6.视频编码器类型.视频, "VP9 # VP8", "VP 系列是谷歌的高压缩格式，个人编码这些远不如 265 来得实在，也因为只有谷歌玩的好，经常出现 YouTube 的 VP9 暴打 AV1 的场面。", "libvpx-vp9", "libsvt_vp9", "vp9_qsv", "libvpx")
        加入分类(预设数据_v6.视频编码器类型.视频, "FFv1", "FFv1 是 FFmpeg 推出的理论无损格式，用于博物馆级别的存档，它保证了每一个像素的信息都是正确的，也就是完全没有压缩度可言，个人存储不应使用。", "ffv1 -level 3", "ffv1 -level 1", "ffv1_vulkan")
        加入分类(预设数据_v6.视频编码器类型.视频, "其他现代编码", "这些编码的压缩度并不差，但目前没有发展起来，效率非常不尽人意。", "libxeve", "libxavs", "libxavs2", "libuavs3e", "liboapv")
        加入分类(预设数据_v6.视频编码器类型.视频, "老旧编码", "在找本世纪 10 年代甚至上个世纪流行的编码？它们都在这。", "mpeg4", "libxvid", "rv20", "rv10", "wmv2", "wmv1")
        加入分类(预设数据_v6.视频编码器类型.视频, "禁用", "扔掉视频流，就选这个。", "-vn")
        加入分类(预设数据_v6.视频编码器类型.视频, "自定义", "在 3FUI 设置文件中添加自定义编码器，然后重启软件即可在这里看到，当然在保存之前你应该先退出 3FUI，否则你的改动就全没了，因为退出保存设置的时候不会再读一遍。")

        加入分类(预设数据_v6.视频编码器类型.图片, "PNG", "无损图片格式；质量值控制压缩强度。", "png")
        加入分类(预设数据_v6.视频编码器类型.图片, "APNG", "PNG 动图；循环和末帧延迟属于 muxer 参数。", "apng")
        加入分类(预设数据_v6.视频编码器类型.图片, "JPEG\JPG", "常规有损图片格式；质量值越低越清晰。", "mjpeg")
        加入分类(预设数据_v6.视频编码器类型.图片, "WEBP 静图", "WebP 静态图片；支持有损、无损和用途预设。", "libwebp")
        加入分类(预设数据_v6.视频编码器类型.图片, "WEBP 动图", "WebP 动图；循环次数属于 muxer 参数。", "libwebp_anim")
        加入分类(预设数据_v6.视频编码器类型.图片, "GIF", "GIF 动图；建议使用调色板滤镜提高观感。", "gif")
        加入分类(预设数据_v6.视频编码器类型.图片, "BMP", "无压缩位图格式，体积大但兼容性好。", "bmp")
        加入分类(预设数据_v6.视频编码器类型.图片, "OpenJPEG", "JPEG 2000 编码器，适合特殊归档和影院规格。", "libopenjpeg")
        加入分类(预设数据_v6.视频编码器类型.图片, "JPEG-LS", "JPEG-LS 无损或近无损编码。", "jpegls")
        加入分类(预设数据_v6.视频编码器类型.图片, "SVT JPEG XS", "当前自编译 FFmpeg 未识别 libsvtjpegxs，保留条目用于未来构建。", "libsvtjpegxs")
        加入分类(预设数据_v6.视频编码器类型.图片, "HDR (Radiance RGBE)", "Radiance RGBE HDR 图片格式。", "hdr")
        加入分类(预设数据_v6.视频编码器类型.图片, "TIFF", "TIFF 图片格式；压缩算法可另行指定。", "tiff")
        加入分类(预设数据_v6.视频编码器类型.图片, "DPX", "电影工业常用图像序列格式。", "dpx")
        加入分类(预设数据_v6.视频编码器类型.图片, "OpenEXR", "高动态范围图像格式，常用于 VFX 和线性工作流。", "exr")
    End Sub

    Private Shared Sub 初始化编码器()
        加入编码器(基础("copy", "复制视频流，不重新编码。", "复制流", 预设数据_v6.视频编码器类型.视频, 是否复制流:=True))
        加入编码器(基础("-vn", "禁用视频流输出。", "禁用", 预设数据_v6.视频编码器类型.视频, 是否禁用:=True))
        加入编码器(基础("libaom-av2", "", "AV2", 预设数据_v6.视频编码器类型.视频))

        加入编码器(基础("libvvenc", "VVC/H.266 软件编码器。", "H.266/VVC", 预设数据_v6.视频编码器类型.视频,
            编码预设:=参数("-preset", "slower 最慢 ~ faster 最快", "faster", "slower", "slow", "medium", "fast", "faster"),
            像素格式:=像素("yuv420p10le"),
            特殊参数:=特殊列表(特殊("-qp", "-1 自动；0 ~ 63", "恒定量化", False), 特殊("-qpa", "0/1", "感知优化，默认开启", False, "0", "1"), 特殊("-vvenc-params", "key=value:key=value", "vvenc 原生参数", False)),
            支持二次编码:=True))

        加入编码器(基础("libaom-av1", "AOM AV1 软件编码器，压缩能力强但速度较慢。", "AV1", 预设数据_v6.视频编码器类型.视频,
            编码预设:=参数("-cpu-used", "0 最慢 ~ 8 最快", "5", "0", "1", "2", "3", "4", "5", "6", "7", "8"),
            配置文件:=参数("-profile:v", "0 main / 1 high / 2 professional", "", "0", "1", "2"),
            场景优化:=参数("-tune", "psnr 或 ssim", "", "psnr", "ssim"),
            像素格式:=像素("yuv420p yuv422p yuv444p gbrp yuv420p10le yuv422p10le yuv444p10le yuv420p12le yuv422p12le yuv444p12le gbrp10le gbrp12le gray gray10le gray12le"),
            特殊参数:=特殊列表(特殊("-crf", "-1 自动；0 ~ 63", "恒定质量，数值越低越清晰", False), 特殊("-usage", "good/realtime/allintra", "编码用途", False, "good", "realtime", "allintra"), 特殊("-lag-in-frames", "-1 自动；0 起", "alt-ref 前瞻帧数", False), 特殊("-aq-mode", "-1 自动；0 ~ 4", "自适应量化模式", False), 特殊("-arnr-strength", "-1 自动；0 ~ 6", "alt-ref 降噪强度", False), 特殊("-aom-params", "key=value:key=value", "libaom 原生参数", False)),
            无损模式说明:="支持：使用 -crf 0 -b:v 0；同时避免有损像素格式转换。",
            支持二次编码:=True))

        加入编码器(基础("libsvtav1", "SVT-AV1 软件编码器，速度和压缩效率均衡。", "AV1", 预设数据_v6.视频编码器类型.视频,
            编码预设:=参数("-preset", "-2 自动；0 最慢 ~ 13 最快", "6", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13"),
            配置文件:=参数("-profile:v", "main/high/professional", "", "main", "high", "professional"),
            像素格式:=像素("yuv420p yuv420p10le"),
            特殊参数:=特殊列表(特殊("-crf", "0 ~ 63", "恒定质量，数值越低越清晰", False), 特殊("-qp", "0 ~ 63", "初始量化值/CQP", False), 特殊("-svtav1-params", "key=value:key=value", "SVT-AV1 原生参数", False)),
            无损模式说明:="不支持明确无损模式。"))

        加入编码器(基础("av1_nvenc", "NVIDIA NVENC AV1 硬件编码器。", "AV1", 预设数据_v6.视频编码器类型.视频,
            编码预设:=参数("-preset", "p7 最慢最好 ~ p1 最快", "p7", "p7", "p6", "p5", "p4", "p3", "p2", "p1"),
            场景优化:=参数("-tune", "hq/uhq/ll/ull/lossless", "", "hq", "uhq", "ll", "ull", "lossless"),
            像素格式:=像素("yuv420p nv12 p010le yuv444p p016le nv16 p210le p216le yuv444p10msble yuv444p16le bgr0 bgra rgb0 rgba x2rgb10le x2bgr10le gbrp gbrp10msble gbrp16le cuda d3d11"),
            特殊参数:=合并特殊列表(特殊列表(特殊("-gpu", "-2 list；-1 any；0 起编号", "NVIDIA GPU 索引", False)), NVENC质量参数("0 自动；1 ~ 63", "-1 自动；0 ~ 255")),
            视觉体积均衡点:="基于 RTX50 系列，常规模式 cq = 36，UHQ 模式 cq = 38，场景极简例如动漫极限平衡点 cq = 42",
            无损模式说明:="支持：使用 -tune lossless；也可尝试 -rc constqp -qp 0，实际可用性取决于 NVENC 硬件和驱动。"))

        加入编码器(基础("av1_qsv", "Intel Quick Sync AV1 硬件编码器。", "AV1", 预设数据_v6.视频编码器类型.视频,
            编码预设:=参数("-preset", "veryslow 最慢 ~ veryfast 最快", "veryslow", "veryslow", "slower", "slow", "medium", "fast", "faster", "veryfast"),
            配置文件:=参数("-profile:v", "main", "", "main"),
            像素格式:=像素("nv12 p010le qsv"),
            特殊参数:=合并特殊列表(QSV质量参数(), 特殊列表(特殊("-qsv_params", "key=value:key=value", "QSV 原生参数", False)))))

        加入编码器(基础("av1_amf", "AMD AMF AV1 硬件编码器。", "AV1", 预设数据_v6.视频编码器类型.视频,
            编码预设:=参数("-quality", "high_quality 最好 ~ speed 最快", "high_quality", "high_quality", "quality", "balanced", "speed"),
            配置文件:=参数("-profile:v", "main", "", "main"),
            场景优化:=参数("-usage", "transcoding/low latency/high quality", "", "transcoding", "ultralowlatency", "lowlatency", "high_quality", "lowlatency_high_quality"),
            像素格式:=像素("nv12 yuv420p d3d11 dxva2_vld p010le amf bgr0 rgb0 bgra argb rgba x2bgr10le rgbaf16le"),
            特殊参数:=AMF质量参数("-1 自动；0 ~ 255"),
            无损模式说明:="不支持明确无损模式；最低量化可写 -rc cqp -qp_i 0 -qp_p 0 -qp_b 0。"))

        加入编码器(基础("av1_d3d12va", "D3D12VA AV1 硬件编码器占位项。", "AV1", 预设数据_v6.视频编码器类型.视频,
            配置文件:=参数("-profile:v", "main/high/professional", "", "main", "high", "professional"),
            像素格式:=像素("d3d12"),
            可用性说明:="当前 FFmpeg 未识别此编码器"))

        加入编码器(基础("librav1e", "rav1e AV1 软件编码器。", "AV1", 预设数据_v6.视频编码器类型.视频,
            编码预设:=参数("-speed", "0 最慢 ~ 10 最快", "", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10"),
            像素格式:=像素("yuv420p yuvj420p yuv420p10le yuv420p12le yuv422p yuvj422p yuv422p10le yuv422p12le yuv444p yuvj444p yuv444p10le yuv444p12le"),
            特殊参数:=特殊列表(特殊("-qp", "-1 自动；0 ~ 255", "恒定量化值", False), 特殊("-rav1e-params", "key=value:key=value", "rav1e 原生参数", False)),
            无损模式说明:="支持：使用 -qp 0；同时避免有损像素格式转换。"))

        加入编码器(基础("av1_vulkan", "Vulkan AV1 硬件编码器。", "AV1", 预设数据_v6.视频编码器类型.视频,
            编码预设:=参数("-quality", "5 最慢 ~ 0 最快", "", "5", "4", "3", "2", "1", "0"),
            配置文件:=参数("-profile:v", "main/high/professional", "", "main", "high", "professional"),
            场景优化:=参数("-tune", "default/hq/ll/ull/lossless", "", "default", "hq", "ll", "ull", "lossless"),
            像素格式:=像素("vulkan"),
            特殊参数:=合并特殊列表(Vulkan质量参数("-1 自动；0 ~ 255"), 特殊列表(特殊("-usage", "transcode/stream/record/conference", "用途标志", False, "transcode", "stream", "record", "conference"))),
            无损模式说明:="支持：使用 -tune lossless；建议配合 -rc_mode cqp -qp 0，实际可用性取决于 Vulkan 驱动。"))

        加入编码器(基础("libx265", "x265 HEVC 软件编码器。", "H.265/HEVC", 预设数据_v6.视频编码器类型.视频,
            编码预设:=参数("-preset", "veryslow 最慢 ~ ultrafast 最快", "slow", "veryslow", "slower", "slow", "medium", "fast", "faster", "veryfast", "superfast", "ultrafast"),
            配置文件:=参数("-profile:v", "main/main10/main12/mainstillpicture", "", "main", "main10", "main12", "mainstillpicture"),
            场景优化:=参数("-tune", "psnr/ssim/grain/fastdecode/zerolatency", "", "psnr", "ssim", "grain", "fastdecode", "zerolatency", "animation", "stillimage"),
            像素格式:=像素("yuv420p yuvj420p yuv422p yuvj422p yuv444p yuvj444p gbrp yuv420p10le yuv422p10le yuv444p10le gbrp10le yuv420p12le yuv422p12le yuv444p12le gbrp12le gray gray10le gray12le yuva420p yuva420p10le"),
            特殊参数:=特殊列表(特殊("-crf", "-1 自动；常用 0 ~ 51，数值越低越清晰", "恒定质量", False), 特殊("-qp", "-1 自动；0 起", "恒定 QP", False), 特殊("-x265-params", "key=value:key=value", "x265 原生参数", False)),
            无损模式说明:="支持：使用 -x265-params lossless=1。",
            支持二次编码:=True))

        加入编码器(基础("hevc_nvenc", "NVIDIA NVENC HEVC 硬件编码器。", "H.265/HEVC", 预设数据_v6.视频编码器类型.视频,
            编码预设:=参数("-preset", "p7 最慢最好 ~ p1 最快；lossless/losslesshp 为无损模式", "p7", "p7", "p6", "p5", "p4", "p3", "p2", "p1", "lossless", "losslesshp"),
            配置文件:=参数("-profile:v", "main/main10/rext", "", "main", "main10", "rext"),
            场景优化:=参数("-tune", "hq/uhq/ll/ull/lossless", "", "hq", "uhq", "ll", "ull", "lossless"),
            像素格式:=像素("yuv420p nv12 p010le yuv444p p016le nv16 p210le p216le yuv444p10msble yuv444p16le bgr0 bgra rgb0 rgba x2rgb10le x2bgr10le gbrp gbrp10msble gbrp16le cuda d3d11"),
            特殊参数:=合并特殊列表(特殊列表(特殊("-gpu", "-2 list；-1 any；0 起编号", "NVIDIA GPU 索引", False), 特殊("-tier", "main/high", "HEVC tier", False, "main", "high")), NVENC质量参数("0 自动；1 ~ 51", "-1 自动；0 ~ 51")),
            视觉体积均衡点:="基于 RTX50 系列，常规模式 cq = 26，UHQ 模式 cq = 28",
            无损模式说明:="支持：使用 -tune lossless，或 -preset lossless/losslesshp；也可尝试 -rc constqp -qp 0。"))

        加入编码器(基础("hevc_qsv", "Intel Quick Sync HEVC 硬件编码器。", "H.265/HEVC", 预设数据_v6.视频编码器类型.视频,
            编码预设:=参数("-preset", "veryslow 最慢 ~ veryfast 最快", "veryslow", "veryslow", "slower", "slow", "medium", "fast", "faster", "veryfast"),
            配置文件:=参数("-profile:v", "main/main10/mainsp/rext/scc", "", "main", "main10", "mainsp", "rext", "scc"),
            像素格式:=像素("nv12 p010le p012le yuyv422 y210le qsv bgra x2rgb10le vuyx xv30le"),
            特殊参数:=合并特殊列表(QSV质量参数(), 特殊列表(特殊("-tier", "main/high", "HEVC tier", False, "main", "high")))))

        加入编码器(基础("hevc_amf", "AMD AMF HEVC 硬件编码器。", "H.265/HEVC", 预设数据_v6.视频编码器类型.视频,
            编码预设:=参数("-quality", "quality 最好 ~ speed 最快", "quality", "quality", "balanced", "speed"),
            配置文件:=参数("-profile:v", "main/main10", "", "main", "main10"),
            场景优化:=参数("-usage", "transcoding/low latency/high quality", "", "transcoding", "ultralowlatency", "lowlatency", "webcam", "high_quality", "lowlatency_high_quality"),
            像素格式:=像素("nv12 yuv420p d3d11 dxva2_vld p010le amf bgr0 rgb0 bgra argb rgba x2bgr10le rgbaf16le"),
            特殊参数:=合并特殊列表(AMF质量参数("-1 自动；0 ~ 51", False), 特殊列表(特殊("-profile_tier", "main/high", "HEVC tier", False, "main", "high"))),
            无损模式说明:="不支持明确无损模式；最低量化可写 -rc cqp -qp_i 0 -qp_p 0。"))

        加入编码器(基础("hevc_d3d12va", "D3D12VA HEVC 硬件编码器。", "H.265/HEVC", 预设数据_v6.视频编码器类型.视频,
            配置文件:=参数("-profile:v", "main/main10", "", "main", "main10"),
            像素格式:=像素("d3d12"),
            特殊参数:=D3D12VA质量参数("0 ~ 52")))

        加入编码器(基础("hevc_vulkan", "Vulkan HEVC 硬件编码器。", "H.265/HEVC", 预设数据_v6.视频编码器类型.视频,
            编码预设:=参数("-quality", "5 最慢 ~ 0 最快", "", "5", "4", "3", "2", "1", "0"),
            配置文件:=参数("-profile:v", "main/main10/rext", "", "main", "main10", "rext"),
            场景优化:=参数("-tune", "default/hq/ll/ull/lossless", "", "default", "hq", "ll", "ull", "lossless"),
            像素格式:=像素("vulkan"),
            特殊参数:=Vulkan质量参数("-1 自动；0 ~ 255"),
            无损模式说明:="支持：使用 -tune lossless；建议配合 -rc_mode cqp -qp 0，实际可用性取决于 Vulkan 驱动。"))

        加入编码器(基础("libkvazaar", "Kvazaar HEVC 软件编码器。", "H.265/HEVC", 预设数据_v6.视频编码器类型.视频,
            像素格式:=像素("yuv420p"),
            特殊参数:=特殊列表(特殊("-kvazaar-params", "key=value,key=value", "Kvazaar 原生参数", False)),
            无损模式说明:="支持：使用 -kvazaar-params lossless=1。"))

        加入编码器(基础("libsvt_hevc", "SVT-HEVC 软件编码器。", "H.265/HEVC", 预设数据_v6.视频编码器类型.视频,
            编码预设:=参数("-preset", "0 最慢 ~ 12 最快", "7", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12"),
            配置文件:=参数("-profile:v", "1 ~ 4", "", "1", "2", "3", "4"),
            场景优化:=参数("-tune", "sq/oq/vmaf", "", "sq", "oq", "vmaf"),
            像素格式:=像素("yuv420p yuv420p10le yuv422p yuv422p10le yuv444p yuv444p10le"),
            特殊参数:=特殊列表(特殊("-qp", "0 ~ 51", "QP 值", False), 特殊("-rc", "cqp/vbr", "码率控制模式", False, "cqp", "vbr"), 特殊("-la_depth", "-1 自动；0 ~ 256", "前瞻距离", False), 特殊("-thread_count", "0 自动", "SVT-HEVC 线程数", False))))

        加入编码器(基础("libx264", "x264 H.264 软件编码器。", "H.264/AVC", 预设数据_v6.视频编码器类型.视频,
            编码预设:=参数("-preset", "veryslow 最慢 ~ ultrafast 最快", "slower", "veryslow", "slower", "slow", "medium", "fast", "faster", "veryfast", "superfast", "ultrafast"),
            配置文件:=参数("-profile:v", "baseline/main/high/high10/high422/high444", "", "baseline", "main", "high", "high10", "high422", "high444"),
            场景优化:=参数("-tune", "film/animation/grain/stillimage/psnr/ssim/fastdecode/zerolatency", "", "film", "animation", "grain", "stillimage", "psnr", "ssim", "fastdecode", "zerolatency"),
            像素格式:=像素("yuv420p yuvj420p yuv422p yuvj422p yuv444p yuvj444p nv12 nv16 nv21 yuv420p10le yuv422p10le yuv444p10le nv20le gray gray10le"),
            特殊参数:=特殊列表(特殊("-crf", "-1 自动；0 ~ 51 常用，数值越低越清晰", "恒定质量", False), 特殊("-crf_max", "-1 自动；0 ~ 51 常用", "CRF+VBV 时限制最低质量", False), 特殊("-qp", "-1 自动；0 起", "恒定 QP", False), 特殊("-aq-mode", "-1 自动；0 ~ 3", "自适应量化模式", False), 特殊("-aq-strength", "-1 自动；0 起", "AQ 强度", False), 特殊("-x264-params", "key=value:key=value", "x264 原生参数", False)),
            无损模式说明:="支持：使用 -qp 0；也可使用 -crf 0。",
            支持二次编码:=True))

        加入编码器(基础("libx264rgb", "x264 RGB 输入编码器。", "H.264/AVC", 预设数据_v6.视频编码器类型.视频,
            编码预设:=参数("-preset", "veryslow 最慢 ~ ultrafast 最快", "slower", "veryslow", "slower", "slow", "medium", "fast", "faster", "veryfast", "superfast", "ultrafast"),
            配置文件:=参数("-profile:v", "high/high444", "", "high", "high444"),
            场景优化:=参数("-tune", "film/animation/grain/stillimage/psnr/ssim/fastdecode/zerolatency", "", "film", "animation", "grain", "stillimage", "psnr", "ssim", "fastdecode", "zerolatency"),
            像素格式:=像素("bgr0 bgr24 rgb24"),
            特殊参数:=特殊列表(特殊("-crf", "-1 自动；0 ~ 51 常用，数值越低越清晰", "恒定质量", False), 特殊("-crf_max", "-1 自动；0 ~ 51 常用", "CRF+VBV 时限制最低质量", False), 特殊("-qp", "-1 自动；0 起", "恒定 QP", False), 特殊("-aq-mode", "-1 自动；0 ~ 3", "自适应量化模式", False), 特殊("-aq-strength", "-1 自动；0 起", "AQ 强度", False), 特殊("-x264-params", "key=value:key=value", "x264 原生参数", False)),
            无损模式说明:="支持：使用 -qp 0；也可使用 -crf 0。",
            支持二次编码:=True))

        加入编码器(基础("libopenh264", "OpenH264 编码器，适合基础兼容需求。", "H.264/AVC", 预设数据_v6.视频编码器类型.视频,
            配置文件:=参数("-profile:v", "constrained_baseline/main/high", "", "constrained_baseline", "main", "high"),
            像素格式:=像素("yuv420p yuvj420p"),
            特殊参数:=特殊列表(特殊("-rc_mode", "quality/bitrate/buffer/timestamp", "码率控制模式", False, "quality", "bitrate", "buffer", "timestamp"))))

        加入编码器(基础("h264_nvenc", "NVIDIA NVENC H.264 硬件编码器。", "H.264/AVC", 预设数据_v6.视频编码器类型.视频,
            编码预设:=参数("-preset", "p7 最慢最好 ~ p1 最快；lossless/losslesshp 为无损模式", "p7", "p7", "p6", "p5", "p4", "p3", "p2", "p1", "lossless", "losslesshp"),
            配置文件:=参数("-profile:v", "baseline/main/high/high10/high422/high444p", "", "baseline", "main", "high", "high10", "high422", "high444p"),
            场景优化:=参数("-tune", "hq/ll/ull/lossless", "", "hq", "ll", "ull", "lossless"),
            像素格式:=像素("yuv420p nv12 p010le yuv444p p016le nv16 p210le p216le yuv444p10msble yuv444p16le bgr0 bgra rgb0 rgba x2rgb10le x2bgr10le gbrp gbrp10msble gbrp16le cuda d3d11"),
            特殊参数:=合并特殊列表(特殊列表(特殊("-gpu", "-2 list；-1 any；0 起编号", "NVIDIA GPU 索引", False)), NVENC质量参数("0 自动；1 ~ 51", "-1 自动；0 ~ 51")),
            无损模式说明:="支持：使用 -tune lossless，或 -preset lossless/losslesshp；也可尝试 -rc constqp -qp 0。"))

        加入编码器(基础("h264_qsv", "Intel Quick Sync H.264 硬件编码器。", "H.264/AVC", 预设数据_v6.视频编码器类型.视频,
            编码预设:=参数("-preset", "veryslow 最慢 ~ veryfast 最快", "veryslow", "veryslow", "slower", "slow", "medium", "fast", "faster", "veryfast"),
            配置文件:=参数("-profile:v", "baseline/main/high", "", "baseline", "main", "high"),
            像素格式:=像素("nv12 qsv"),
            特殊参数:=QSV质量参数(True)))

        加入编码器(基础("h264_amf", "AMD AMF H.264 硬件编码器。", "H.264/AVC", 预设数据_v6.视频编码器类型.视频,
            编码预设:=参数("-quality", "quality 最好 ~ speed 最快", "quality", "quality", "balanced", "speed"),
            配置文件:=参数("-profile:v", "main/high/constrained_baseline/constrained_high", "", "main", "high", "constrained_baseline", "constrained_high"),
            场景优化:=参数("-usage", "transcoding/low latency/high quality", "", "transcoding", "ultralowlatency", "lowlatency", "webcam", "high_quality", "lowlatency_high_quality"),
            像素格式:=像素("nv12 yuv420p d3d11 dxva2_vld p010le amf bgr0 rgb0 bgra argb rgba x2bgr10le rgbaf16le"),
            特殊参数:=AMF质量参数("-1 自动；0 ~ 51"),
            无损模式说明:="不支持明确无损模式；最低量化可写 -rc cqp -qp_i 0 -qp_p 0 -qp_b 0。"))

        加入编码器(基础("h264_d3d12va", "D3D12VA H.264 硬件编码器。", "H.264/AVC", 预设数据_v6.视频编码器类型.视频,
            配置文件:=参数("-profile:v", "main/high/high10", "", "main", "high", "high10"),
            像素格式:=像素("d3d12"),
            特殊参数:=合并特殊列表(D3D12VA质量参数("0 ~ 52"), 特殊列表(特殊("-coder", "cabac/cavlc", "熵编码器", False, "cabac", "cavlc")))))

        加入编码器(基础("h264_vulkan", "Vulkan H.264 硬件编码器。", "H.264/AVC", 预设数据_v6.视频编码器类型.视频,
            编码预设:=参数("-quality", "5 最慢 ~ 0 最快", "", "5", "4", "3", "2", "1", "0"),
            配置文件:=参数("-profile:v", "constrained_baseline/main/high/high444p", "", "constrained_baseline", "main", "high", "high444p"),
            场景优化:=参数("-tune", "default/hq/ll/ull/lossless", "", "default", "hq", "ll", "ull", "lossless"),
            像素格式:=像素("vulkan"),
            特殊参数:=Vulkan质量参数("-1 自动；0 ~ 255"),
            无损模式说明:="支持：使用 -tune lossless；建议配合 -rc_mode cqp -qp 0，实际可用性取决于 Vulkan 驱动。"))

        加入编码器(基础("prores_ks", "Apple ProRes 编码器，剪辑中间格式。", "ProRes", 预设数据_v6.视频编码器类型.视频,
            配置文件:=参数("-profile:v", "proxy/lt/standard/hq/4444/4444xq", "", "auto", "proxy", "lt", "standard", "hq", "4444", "4444xq"),
            像素格式:=像素("yuv422p10le yuv444p10le yuva444p10le"),
            特殊参数:=特殊列表(特殊("-quant_mat", "auto/default/proxy/lt/standard/hq", "ProRes 量化矩阵", False, "auto", "default", "proxy", "lt", "standard", "hq"))))

        加入编码器(基础("prores_aw", "Apple ProRes 编码器，功能较简。", "ProRes", 预设数据_v6.视频编码器类型.视频,
            像素格式:=像素("yuv422p10le yuv444p10le yuva444p10le")))

        加入编码器(基础("libvpx-vp9", "libvpx VP9 软件编码器；速度参数不是 -preset。", "VP9 # VP8", 预设数据_v6.视频编码器类型.视频,
            编码预设:=参数("-cpu-used", "-8 最慢 ~ 8 最快", "", "0", "1", "2", "3", "4", "5", "6", "7", "8"),
            场景优化:=参数("-tune", "psnr 或 ssim", "", "psnr", "ssim"),
            像素格式:=像素("yuv420p yuva420p yuv422p yuva422p yuv440p yuv444p yuva444p yuv420p10le yuva420p10le yuv422p10le yuva422p10le yuv440p10le yuv444p10le yuva444p10le yuv420p12le yuv422p12le yuv440p12le yuv444p12le yuva444p12le gbrp gbrap gbrp10le gbrap10le gbrp12le gbrap12le"),
            特殊参数:=特殊列表(特殊("-crf", "-1 自动；0 ~ 63", "恒定质量，数值越低越清晰；通常配合 -b:v 0", False), 特殊("-lossless", "-1 自动；0/1", "VP9 无损模式", False, "0", "1"), 特殊("-deadline", "best/good/realtime", "编码耗时策略", False, "best", "good", "realtime"), 特殊("-lag-in-frames", "-1 自动；0 起", "alt-ref 前瞻帧数", False), 特殊("-auto-alt-ref", "-1 自动；0 ~ 6", "alt-ref 帧，二次编码常用", False), 特殊("-aq-mode", "-1 自动；0 ~ 4", "自适应量化模式", False), 特殊("-arnr-strength", "-1 自动；0 起", "alt-ref 降噪强度", False), 特殊("-tune-content", "default/screen", "内容类型优化", False, "default", "screen")),
            无损模式说明:="支持：使用 -lossless 1。"))

        加入编码器(基础("libsvt_vp9", "SVT-VP9 软件编码器。", "VP9 # VP8", 预设数据_v6.视频编码器类型.视频,
            编码预设:=参数("-preset", "0 最慢 ~ 9 最快", "", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9"),
            场景优化:=参数("-tune", "vq/ssim/vmaf", "", "vq", "ssim", "vmaf"),
            像素格式:=像素("yuv420p")))

        加入编码器(基础("vp9_qsv", "Intel Quick Sync VP9 硬件编码器。", "VP9 # VP8", 预设数据_v6.视频编码器类型.视频,
            编码预设:=参数("-preset", "veryslow 最慢 ~ veryfast 最快", "veryslow", "veryslow", "slower", "slow", "medium", "fast", "faster", "veryfast"),
            配置文件:=参数("-profile:v", "profile0/profile1/profile2/profile3", "", "profile0", "profile1", "profile2", "profile3"),
            像素格式:=像素("nv12 p010le vuyx qsv xv30le"),
            特殊参数:=QSV质量参数()))

        加入编码器(基础("libvpx", "libvpx VP8 软件编码器；速度参数不是 -preset。", "VP9 # VP8", 预设数据_v6.视频编码器类型.视频,
            编码预设:=参数("-cpu-used", "-16 最慢 ~ 16 最快", "", "0", "1", "2", "3", "4", "5", "6", "7", "8"),
            场景优化:=参数("-tune", "psnr 或 ssim", "", "psnr", "ssim"),
            像素格式:=像素("yuv420p yuva420p"),
            特殊参数:=特殊列表(特殊("-crf", "-1 自动；0 ~ 63", "恒定质量，数值越低越清晰；通常配合 -b:v 0", False), 特殊("-deadline", "best/good/realtime", "编码耗时策略", False, "best", "good", "realtime"), 特殊("-lag-in-frames", "-1 自动；0 起", "alt-ref 前瞻帧数", False), 特殊("-auto-alt-ref", "-1 自动；0 ~ 2", "alt-ref 帧，二次编码常用", False), 特殊("-arnr-strength", "-1 自动；0 起", "alt-ref 降噪强度", False)),
            无损模式说明:="不支持明确无损模式。"))

        加入编码器(基础("ffv1 -level 3", "FFv1 level 3，无损存档推荐级别。", "FFv1", 预设数据_v6.视频编码器类型.视频,
            命令行编码器名:="ffv1",
            默认附加参数:=特殊列表(特殊("-level", "3", "FFv1 level", True, "3")),
            像素格式:=像素("yuv420p yuva420p yuva422p yuv444p yuva444p yuv440p yuv422p yuv411p yuv410p bgr0 bgra yuv420p16le yuv422p16le yuv444p16le yuv444p9le yuv422p9le yuv420p9le yuv420p10le yuv422p10le yuv444p10le yuv420p12le yuv422p12le yuv444p12le yuva444p16le yuva422p16le yuva420p16le gray16le gray gbrp9le gbrp10le gbrp12le gbrp14le gbrp16le rgb48le rgba64le gray10le gray12le gray14le"),
            特殊参数:=特殊列表(特殊("-coder", "rice/range_def/range_tab", "熵编码器", False, "rice", "range_def", "range_tab"), 特殊("-qtable", "default/8bit/greater8bit", "无损量化表选择", False))))

        加入编码器(基础("ffv1 -level 1", "FFv1 level 1，旧版兼容选项。", "FFv1", 预设数据_v6.视频编码器类型.视频,
            命令行编码器名:="ffv1",
            默认附加参数:=特殊列表(特殊("-level", "1", "FFv1 level", True, "1")),
            像素格式:=像素("yuv420p yuva420p yuv444p bgr0 bgra yuv420p16le yuv444p16le gray16le gray"),
            特殊参数:=特殊列表(特殊("-coder", "rice/range_def/range_tab", "熵编码器", False, "rice", "range_def", "range_tab"), 特殊("-qtable", "default/8bit/greater8bit", "无损量化表选择", False))))

        加入编码器(基础("ffv1_vulkan", "FFv1 Vulkan 硬件路径编码器。", "FFv1", 预设数据_v6.视频编码器类型.视频,
            像素格式:=像素("vulkan"),
            特殊参数:=特殊列表(特殊("-level", "1/3/4", "FFv1 level", False, "1", "3", "4"), 特殊("-coder", "rice/range_def/range_tab", "熵编码器", False, "rice", "range_def", "range_tab"), 特殊("-qtable", "default/8bit/greater8bit", "无损量化表选择", False), 特殊("-force_pcm", "0/1", "全部切片使用 PCM", False, "0", "1"))))

        加入编码器(基础("libxeve", "MPEG-5 EVC 软件编码器。", "其他现代编码", 预设数据_v6.视频编码器类型.视频,
            编码预设:=参数("-preset", "placebo 最慢 ~ fast 最快", "", "placebo", "slow", "medium", "fast"),
            配置文件:=参数("-profile:v", "baseline/main", "", "baseline", "main"),
            场景优化:=参数("-tune", "none/zerolatency/psnr", "", "none", "zerolatency", "psnr"),
            像素格式:=像素("yuv420p yuv420p10le"),
            特殊参数:=特殊列表(特殊("-rc_mode", "CQP/ABR/CRF", "码率控制模式", False, "CQP", "ABR", "CRF"), 特殊("-qp", "0 ~ 51", "CQP 量化值", False), 特殊("-crf", "10 ~ 49", "CRF 质量值，数值越低越清晰", False), 特殊("-xeve-params", "key=value:key=value", "xeve 原生参数", False)),
            无损模式说明:="不支持明确无损模式；最低量化可写 -qp 0。"))

        加入编码器(基础("libxavs", "AVS 软件编码器。", "其他现代编码", 预设数据_v6.视频编码器类型.视频,
            像素格式:=像素("yuv420p"),
            特殊参数:=特殊列表(特殊("-crf", "-1 自动；数值越低越清晰", "恒定质量", False), 特殊("-qp", "-1 自动；0 起", "恒定 QP", False), 特殊("-cplxblur", "-1 自动；0 起", "QP 曲线平滑", False)),
            无损模式说明:="不支持明确无损模式；最低量化可写 -qp 0。"))

        加入编码器(基础("libxavs2", "AVS2 软件编码器。", "其他现代编码", 预设数据_v6.视频编码器类型.视频,
            编码预设:=参数("-speed_level", "9 最慢 ~ 0 最快", "", "9", "8", "7", "6", "5", "4", "3", "2", "1", "0"),
            像素格式:=像素("yuv420p"),
            特殊参数:=特殊列表(特殊("-initial_qp", "1 ~ 63", "初始量化参数", False), 特殊("-qp", "1 ~ 63", "量化参数", False), 特殊("-min_qp", "0 ~ 63", "码率控制最小 QP", False), 特殊("-max_qp", "0 ~ 63", "码率控制最大 QP", False), 特殊("-xavs2-params", "key=value:key=value", "xavs2 原生参数", False)),
            无损模式说明:="不支持明确无损模式；-qp 范围 1 ~ 63。"))

        加入编码器(基础("libuavs3e", "AVS3 软件编码器。", "其他现代编码", 预设数据_v6.视频编码器类型.视频,
            编码预设:=参数("-speed", "0 最慢 ~ 6 最快", "", "0", "1", "2", "3", "4", "5", "6"),
            像素格式:=像素("yuv420p yuv420p10le"),
            特殊参数:=特殊列表(特殊("-rc_type", "0/1/2", "码率控制类型", False, "0", "1", "2"), 特殊("-crf", "1 ~ 63", "恒定质量，数值越低越清晰", False), 特殊("-qp", "1 ~ 63", "量化参数", False)),
            无损模式说明:="不支持明确无损模式；-qp/-crf 范围 1 ~ 63。"))

        加入编码器(基础("liboapv", "APV 软件编码器。", "其他现代编码", 预设数据_v6.视频编码器类型.视频,
            编码预设:=参数("-preset", "placebo 最慢 ~ fastest 最快", "", "placebo", "slow", "medium", "fast", "fastest"),
            像素格式:=像素("gray10le yuv422p10le yuv422p12le yuv444p10le yuv444p12le yuva444p10le yuva444p12le"),
            特殊参数:=特殊列表(特殊("-qp", "0 ~ 63", "量化参数", False), 特殊("-oapv-params", "key=value:key=value", "APV 原生参数", False)),
            无损模式说明:="不支持明确无损模式；最低量化可写 -qp 0。"))

        For Each 名称 In New String() {"mpeg4", "libxvid", "rv20", "rv10", "wmv2", "wmv1"}
            加入编码器(基础(名称, "旧式视频编码器，通常仅为兼容旧设备使用。", "老旧编码", 预设数据_v6.视频编码器类型.视频,
                像素格式:=像素("yuv420p")))
        Next

        初始化图片编码器()
    End Sub

    Private Shared Sub 初始化图片编码器()
        加入编码器(基础("png", "PNG 无损图片编码器。", "PNG", 预设数据_v6.视频编码器类型.图片,
            像素格式:=像素("rgb24 rgba rgb48be rgba64be pal8 gray ya8 gray16be ya16be monob"),
            图片质量:=图片质量("-compression_level", "0 最快 ~ 9 最小", "6"),
            特殊参数:=特殊列表(特殊("-pred", "none/sub/up/avg/paeth/mixed", "PNG 预测方式", False, "none", "sub", "up", "avg", "paeth", "mixed"))))

        加入编码器(基础("apng", "APNG 动图编码器。", "APNG", 预设数据_v6.视频编码器类型.图片,
            像素格式:=像素("rgb24 rgba rgb48be rgba64be pal8 gray ya8 gray16be ya16be"),
            图片质量:=图片质量("-compression_level", "0 最快 ~ 9 最小", "6"),
            必要参数:=特殊列表(特殊("-plays", "0 无限；1 不循环", "APNG 循环次数", True), 特殊("-final_delay", "0 ~ 65535", "末帧延迟", False))))

        加入编码器(基础("mjpeg", "JPEG/JPG 图片编码器。", "JPEG\JPG", 预设数据_v6.视频编码器类型.图片,
            像素格式:=像素("yuvj420p yuvj422p yuvj444p yuv420p yuv422p yuv444p"),
            图片质量:=图片质量("-q:v", "1 清晰 ~ 31 模糊", "2")))

        加入编码器(基础("libwebp", "WebP 静图编码器。", "WEBP 静图", 预设数据_v6.视频编码器类型.图片,
            像素格式:=像素("bgra yuv420p yuva420p"),
            图片质量:=图片质量("-quality", "0 模糊 ~ 100 清晰", "75"),
            特殊参数:=特殊列表(特殊("-lossless", "0/1", "无损模式", False, "0", "1"), 特殊("-preset", "default/photo/picture/drawing/icon/text", "WebP 用途预设", False, "default", "photo", "picture", "drawing", "icon", "text"))))

        加入编码器(基础("libwebp_anim", "WebP 动图编码器。", "WEBP 动图", 预设数据_v6.视频编码器类型.图片,
            像素格式:=像素("bgra yuv420p yuva420p"),
            图片质量:=图片质量("-quality", "0 模糊 ~ 100 清晰", "75"),
            必要参数:=特殊列表(特殊("-loop", "0 无限；1 播放一次", "WebP muxer 循环次数", True)),
            特殊参数:=特殊列表(特殊("-lossless", "0/1", "无损模式", False, "0", "1"))))

        加入编码器(基础("gif", "GIF 动图编码器。", "GIF", 预设数据_v6.视频编码器类型.图片,
            像素格式:=像素("rgb8 bgr8 rgb4_byte bgr4_byte gray pal8"),
            图片质量:=图片质量("调色板滤镜", "1 启用 palettegen/paletteuse", ""),
            必要参数:=特殊列表(特殊("-loop", "-1 不循环；0 无限", "GIF muxer 循环次数", True), 特殊("palettegen/paletteuse", "建议启用", "高质量 GIF 调色板滤镜", True))))

        加入编码器(基础("bmp", "BMP 位图编码器。", "BMP", 预设数据_v6.视频编码器类型.图片,
            像素格式:=像素("bgra bgr24 rgb565le rgb555le rgb444le rgb8 bgr8 rgb4_byte bgr4_byte gray pal8 monob")))

        加入编码器(基础("libopenjpeg", "OpenJPEG JPEG 2000 编码器。", "OpenJPEG", 预设数据_v6.视频编码器类型.图片,
            像素格式:=像素("rgb24 rgba rgb48le rgba64le gbrp gbrp9le gbrp10le gbrp12le gbrp14le gbrp16le gray ya8 gray16le ya16le gray10le gray12le gray14le yuv420p yuv422p yuva420p yuv440p yuv444p yuva422p yuv411p yuv410p yuva444p yuv420p10le yuv422p10le yuv444p10le yuv420p12le yuv422p12le yuv444p12le xyz12le"),
            图片质量:=图片质量("-q:v", "0.0 全损 ~ 1.0 无损", ""),
            配置文件:=参数("-profile:v", "jpeg2000/cinema2k/cinema4k", "", "jpeg2000", "cinema2k", "cinema4k"),
            特殊参数:=特殊列表(特殊("-format", "j2k/jp2", "封装格式", False, "j2k", "jp2"), 特殊("-irreversible", "0/1", "不可逆小波", False, "0", "1"))))

        加入编码器(基础("jpegls", "JPEG-LS 编码器。", "JPEG-LS", 预设数据_v6.视频编码器类型.图片,
            像素格式:=像素("bgr24 rgb24 gray gray16le"),
            特殊参数:=特殊列表(特殊("-pred", "left/plane/median", "预测方式", False, "left", "plane", "median"))))

        加入编码器(基础("libsvtjpegxs", "SVT JPEG XS 编码器占位项。", "SVT JPEG XS", 预设数据_v6.视频编码器类型.图片,
            可用性说明:="当前 FFmpeg 未识别此编码器"))

        加入编码器(基础("hdr", "Radiance RGBE HDR 图片编码器。", "HDR (Radiance RGBE)", 预设数据_v6.视频编码器类型.图片,
            像素格式:=像素("gbrpf32le")))

        加入编码器(基础("tiff", "TIFF 图片编码器。", "TIFF", 预设数据_v6.视频编码器类型.图片,
            像素格式:=像素("rgb24 rgb48le pal8 rgba rgba64le gray ya8 gray16le ya16le monob monow yuv420p yuv422p yuv440p yuv444p yuv410p yuv411p"),
            特殊参数:=特殊列表(特殊("-compression_algo", "packbits/raw/lzw/deflate", "TIFF 压缩算法", False, "packbits", "raw", "lzw", "deflate"))))

        加入编码器(基础("dpx", "DPX 图片编码器。", "DPX", 预设数据_v6.视频编码器类型.图片,
            像素格式:=像素("gray rgb24 rgba abgr gray16le gray16be rgb48le rgb48be rgba64le rgba64be gbrp10le gbrp10be gbrp12le gbrp12be")))

        加入编码器(基础("exr", "OpenEXR 图片编码器。", "OpenEXR", 预设数据_v6.视频编码器类型.图片,
            像素格式:=像素("grayf32le gbrpf32le gbrapf32le"),
            特殊参数:=特殊列表(特殊("-compression", "none/rle/zips/zip", "EXR 压缩方式", False, "none", "rle", "zips", "zip"), 特殊("-format", "half/float", "像素类型", False, "half", "float"))))
    End Sub

    Private Shared Sub 加入分类(类型 As 预设数据_v6.视频编码器类型, 名称 As String, 描述 As String, ParamArray 编码器名称列表() As String)
        Dim 数据 As New 视频编码器分类数据 With {
            .类型 = 类型,
            .名称 = 名称,
            .描述 = 描述,
            .编码器名称列表 = New List(Of String)(编码器名称列表)
        }
        分类列表.Add(数据)
        分类索引(名称) = 数据
    End Sub

    Private Shared Sub 加入编码器(数据 As 视频编码器数据)
        If String.IsNullOrWhiteSpace(数据.命令行编码器名) Then 数据.命令行编码器名 = 数据.名称
        编码器索引(数据.名称) = 数据
    End Sub

    Private Shared Function 基础(名称 As String,
                               描述 As String,
                               分类名称 As String,
                               类型 As 预设数据_v6.视频编码器类型,
                               Optional 命令行编码器名 As String = "",
                               Optional 编码预设 As 编码器参数列表数据 = Nothing,
                               Optional 配置文件 As 编码器参数列表数据 = Nothing,
                               Optional 场景优化 As 编码器参数列表数据 = Nothing,
                               Optional 像素格式 As 编码器参数列表数据 = Nothing,
                               Optional 图片质量 As 图片质量参数数据 = Nothing,
                               Optional 特殊参数 As List(Of 编码器特殊参数数据) = Nothing,
                               Optional 必要参数 As List(Of 编码器特殊参数数据) = Nothing,
                               Optional 默认附加参数 As List(Of 编码器特殊参数数据) = Nothing,
                               Optional 是否复制流 As Boolean = False,
                               Optional 是否禁用 As Boolean = False,
                               Optional 可用性说明 As String = "",
                               Optional 视觉体积均衡点 As String = Nothing,
                               Optional 无损模式说明 As String = Nothing,
                               Optional 支持二次编码 As Boolean = False) As 视频编码器数据
        Dim 视觉体积均衡点文本 = 视觉体积均衡点
        If 视觉体积均衡点文本 Is Nothing Then 视觉体积均衡点文本 = 获取默认视觉体积均衡点(类型, 分类名称, 是否复制流, 是否禁用)
        Dim 无损模式文本 = 无损模式说明
        If 无损模式文本 Is Nothing Then 无损模式文本 = 获取默认无损模式说明(类型, 分类名称, 是否复制流, 是否禁用)

        Return New 视频编码器数据 With {
            .名称 = 名称,
            .命令行编码器名 = 命令行编码器名,
            .描述 = 描述,
            .分类名称 = 分类名称,
            .类型 = 类型,
            .编码预设 = If(编码预设, New 编码器参数列表数据),
            .配置文件 = If(配置文件, New 编码器参数列表数据),
            .场景优化 = If(场景优化, New 编码器参数列表数据),
            .像素格式 = If(像素格式, New 编码器参数列表数据),
            .图片质量 = If(图片质量, New 图片质量参数数据),
            .特殊参数列表 = If(特殊参数, New List(Of 编码器特殊参数数据)),
            .必要参数列表 = If(必要参数, New List(Of 编码器特殊参数数据)),
            .默认附加参数列表 = If(默认附加参数, New List(Of 编码器特殊参数数据)),
            .是否复制流 = 是否复制流,
            .是否禁用 = 是否禁用,
            .可用性说明 = 可用性说明,
            .视觉体积均衡点 = 视觉体积均衡点文本,
            .无损模式说明 = 无损模式文本,
            .支持二次编码 = 支持二次编码
        }
    End Function

    Private Shared Function 获取默认视觉体积均衡点(类型 As 预设数据_v6.视频编码器类型, 分类名称 As String, 是否复制流 As Boolean, 是否禁用 As Boolean) As String
        If 是否复制流 Then Return "不适用：复制流不会重新编码，源文件的视觉质量和体积不由本程序控制。"
        If 是否禁用 Then Return "不适用：禁用视频流不输出画面。"
        If 类型 <> 预设数据_v6.视频编码器类型.视频 Then Return ""
        Select Case 分类名称
            Case "ProRes"
                Return "不适用：ProRes 是剪辑中间格式，不以 VMAF 95~96 的体积均衡为目标。"
            Case "FFv1"
                Return "不适用：FFv1 是无损存档编码，视觉质量目标不是 VMAF 95~96。"
        End Select
        Return ""
    End Function

    Private Shared Function 获取默认无损模式说明(类型 As 预设数据_v6.视频编码器类型, 分类名称 As String, 是否复制流 As Boolean, 是否禁用 As Boolean) As String
        If 是否复制流 Then Return "不重新编码"
        If 是否禁用 Then Return "禁用视频流不输出画面"

        If 类型 = 预设数据_v6.视频编码器类型.图片 Then
            Select Case 分类名称
                Case "PNG", "APNG", "BMP", "DPX", "HDR (Radiance RGBE)"
                    Return "支持：该图片格式按当前编码器用途为无损/未有损压缩。"
                Case "JPEG\JPG"
                    Return "不支持：JPEG/JPG 为有损图片编码。"
                Case "WEBP 静图", "WEBP 动图"
                    Return "支持：使用 -lossless 1。"
                Case "OpenJPEG"
                    Return "支持：使用 -irreversible 0 走可逆 JPEG 2000；避免同时设置有损质量目标。"
                Case "JPEG-LS"
                    Return "支持：JPEG-LS 默认按无损/近无损用途使用；不要额外设置有损近似参数。"
                Case "GIF"
                    Return "有限支持：GIF 是调色板格式，只能无损保存调色板后的 256 色索引画面，真彩源通常会先量化。"
                Case "OpenEXR", "TIFF"
                    Return "支持：常用压缩方式为无损；是否完全无损取决于像素格式和压缩算法。"
            End Select
            Return ""
        End If

        Select Case 分类名称
            Case "FFv1"
                Return "支持：本身就是无损编码，常用 -c:v ffv1 -level 3。"
            Case "ProRes"
                Return "不支持严格数学无损；ProRes 是高码率剪辑中间格式。"
        End Select

        Return "不支持明确无损模式。"
    End Function

    Private Shared Function 创建自定义编码器数据(名称 As String) As 视频编码器数据
        Return 基础(名称, "用户自定义编码器；参数列表由用户自行维护。", "自定义", 预设数据_v6.视频编码器类型.视频)
    End Function

    Private Shared Function 参数(参数名 As String, 值范围说明 As String, 默认值 As String, ParamArray 值列表() As String) As 编码器参数列表数据
        Return New 编码器参数列表数据 With {
            .参数名 = 参数名,
            .值范围说明 = 值范围说明,
            .默认值 = 默认值,
            .值列表 = New List(Of String)(值列表)
        }
    End Function

    Private Shared Function 像素(值 As String) As 编码器参数列表数据
        Return New 编码器参数列表数据 With {
            .参数名 = "-pix_fmt",
            .值范围说明 = "仅列出 FFmpeg 当前编码器支持的像素格式",
            .值列表 = 拆分空格列表(值)
        }
    End Function

    Private Shared Function 图片质量(参数名 As String, 值范围说明 As String, 默认值 As String) As 图片质量参数数据
        Return New 图片质量参数数据 With {
            .参数名 = 参数名,
            .值范围说明 = 值范围说明,
            .默认值 = 默认值
        }
    End Function

    Private Shared Function 特殊(参数名 As String, 值范围说明 As String, 说明 As String, 是否必要 As Boolean, ParamArray 值列表() As String) As 编码器特殊参数数据
        Return New 编码器特殊参数数据 With {
            .参数名 = 参数名,
            .值范围说明 = 值范围说明,
            .说明 = 说明,
            .是否必要 = 是否必要,
            .值列表 = New List(Of String)(值列表)
        }
    End Function

    Private Shared Function 特殊列表(ParamArray 数据() As 编码器特殊参数数据) As List(Of 编码器特殊参数数据)
        Return New List(Of 编码器特殊参数数据)(数据)
    End Function

    Private Shared Function 合并特殊列表(ParamArray 列表组() As List(Of 编码器特殊参数数据)) As List(Of 编码器特殊参数数据)
        Dim 结果 As New List(Of 编码器特殊参数数据)
        For Each 列表 In 列表组
            If 列表 IsNot Nothing Then 结果.AddRange(列表)
        Next
        Return 结果
    End Function

    Private Shared Function NVENC质量参数(cq范围 As String, qp范围 As String) As List(Of 编码器特殊参数数据)
        Return 特殊列表(
            特殊("-rc", "constqp/vbr/cbr/vbr_hq 等，具体可用值随编码器而变", "码率控制模式；-cq 需要 VBR 类模式，-qp 需要 constqp", False, "constqp", "vbr", "cbr", "vbr_hq"),
            特殊("-cq", cq范围, "VBR 恒定质量目标，数值越低越清晰", False),
            特殊("-qp", qp范围, "恒定 QP；通常配合 -rc constqp", False),
            特殊("-init_qpI", qp范围, "I 帧初始 QP", False),
            特殊("-init_qpP", qp范围, "P 帧初始 QP", False),
            特殊("-init_qpB", qp范围, "B 帧初始 QP", False),
            特殊("-qmin", qp范围, "码率控制最小 QP", False),
            特殊("-qmax", qp范围, "码率控制最大 QP", False),
            特殊("-rc-lookahead", "0 ~ INT_MAX", "码率控制前瞻帧数", False),
            特殊("-multipass", "disabled/qres/fullres", "NVENC 多遍内部分析", False, "disabled", "qres", "fullres"),
            特殊("-spatial-aq", "0/1", "空间自适应量化", False, "0", "1"),
            特殊("-temporal-aq", "0/1", "时间自适应量化", False, "0", "1"),
            特殊("-aq-strength", "1 ~ 15", "空间 AQ 强度，默认 8", False),
            特殊("-lookahead_level", "-1 自动；0 ~ 15", "前瞻质量级别", False),
            特殊("-tf_level", "-1 自动；0 起", "时间滤波强度", False))
    End Function

    Private Shared Function QSV质量参数(Optional 是否H264 As Boolean = False) As List(Of 编码器特殊参数数据)
        Dim 结果 = 特殊列表(
            特殊("-global_quality", "1 ~ 51 常见；数值越低越清晰", "QSV 全局质量目标，通常用于 ICQ/LA_ICQ 等模式", False),
            特殊("-extbrc", "-1 自动；0/1", "扩展码率控制", False, "-1", "0", "1"),
            特殊("-look_ahead_depth", "0 ~ 100", "前瞻深度；部分编码器要求启用 extbrc 或 look_ahead", False),
            特殊("-min_qp_i", "-1 自动；0 ~ 51", "I 帧最小 QP", False),
            特殊("-max_qp_i", "-1 自动；0 ~ 51", "I 帧最大 QP", False),
            特殊("-min_qp_p", "-1 自动；0 ~ 51", "P 帧最小 QP", False),
            特殊("-max_qp_p", "-1 自动；0 ~ 51", "P 帧最大 QP", False),
            特殊("-min_qp_b", "-1 自动；0 ~ 51", "B 帧最小 QP", False),
            特殊("-max_qp_b", "-1 自动；0 ~ 51", "B 帧最大 QP", False))
        If 是否H264 Then
            结果.Add(特殊("-look_ahead", "0/1", "H.264 QSV 前瞻 VBR 算法开关", False, "0", "1"))
        End If
        Return 结果
    End Function

    Private Shared Function AMF质量参数(qp范围 As String, Optional 含B帧QP As Boolean = True) As List(Of 编码器特殊参数数据)
        Dim 结果 = 特殊列表(
            特殊("-rc", "cqp/cbr/vbr/latency/qvbr/hqvbr/hqcbr", "AMF 码率控制模式", False, "cqp", "cbr", "vbr", "qvbr", "hqvbr", "hqcbr"),
            特殊("-qvbr_quality_level", "-1 自动；0 ~ 51", "QVBR 质量级别，数值越低越清晰", False),
            特殊("-qp_i", qp范围, "I 帧 QP", False),
            特殊("-qp_p", qp范围, "P 帧 QP", False),
            特殊("-vbaq", "0/1/auto", "基于方差的自适应量化", False, "0", "1"),
            特殊("-high_motion_quality_boost_enable", "0/1/auto", "高运动质量增强", False, "0", "1"),
            特殊("-pa_paq_mode", "-1 自动；0/1", "感知自适应量化", False),
            特殊("-pa_taq_mode", "-1 自动；0 ~ 2", "时间自适应量化", False),
            特殊("-pa_caq_strength", "-1 自动；0 ~ 2", "内容自适应量化强度", False),
            特殊("-pa_lookahead_buffer_depth", "-1 自动；0 ~ 41", "PA 前瞻缓冲深度", False))
        If 含B帧QP Then 结果.Add(特殊("-qp_b", qp范围, "B 帧 QP", False))
        Return 结果
    End Function

    Private Shared Function D3D12VA质量参数(qp范围 As String) As List(Of 编码器特殊参数数据)
        Return 特殊列表(
            特殊("-rc_mode", "auto/CQP/CBR/VBR/QVBR", "码率控制模式", False, "auto", "CQP", "CBR", "VBR", "QVBR"),
            特殊("-qp", qp范围, "恒定 QP", False))
    End Function

    Private Shared Function Vulkan质量参数(qp范围 As String) As List(Of 编码器特殊参数数据)
        Return 特殊列表(
            特殊("-quality", "0 起，数值越高通常越慢", "Vulkan 编码质量/速度取舍", False),
            特殊("-rc_mode", "auto/CQP/CBR/VBR 等，随驱动实现变化", "码率控制模式", False),
            特殊("-qp", qp范围, "显式恒定量化值", False))
    End Function

    Private Shared Function 拆分空格列表(值 As String) As List(Of String)
        If String.IsNullOrWhiteSpace(值) Then Return New List(Of String)
        Return 值.Split({" "c}, StringSplitOptions.RemoveEmptyEntries).ToList()
    End Function

    Public Enum 编码器参数角色
        编码预设
        配置文件
        场景优化
        像素格式
        图片质量
    End Enum

    Public Class 视频编码器分类数据
        Public Property 类型 As 预设数据_v6.视频编码器类型
        Public Property 名称 As String = ""
        Public Property 描述 As String = ""
        Public Property 编码器名称列表 As New List(Of String)
    End Class

    Public Class 视频编码器数据
        Public Property 名称 As String = ""
        Public Property 命令行编码器名 As String = ""
        Public Property 描述 As String = ""
        Public Property 分类名称 As String = ""
        Public Property 类型 As 预设数据_v6.视频编码器类型
        Public Property 编码预设 As New 编码器参数列表数据
        Public Property 配置文件 As New 编码器参数列表数据
        Public Property 场景优化 As New 编码器参数列表数据
        Public Property 像素格式 As New 编码器参数列表数据
        Public Property 图片质量 As New 图片质量参数数据
        Public Property 特殊参数列表 As New List(Of 编码器特殊参数数据)
        Public Property 必要参数列表 As New List(Of 编码器特殊参数数据)
        Public Property 默认附加参数列表 As New List(Of 编码器特殊参数数据)
        Public Property 是否复制流 As Boolean = False
        Public Property 是否禁用 As Boolean = False
        Public Property 可用性说明 As String = ""
        Public Property 视觉体积均衡点 As String = ""
        Public Property 无损模式说明 As String = ""
        Public Property 支持二次编码 As Boolean = False

        Public ReadOnly Property 特殊参数名列表 As List(Of String)
            Get
                Dim 结果 As New List(Of String)
                For Each 单项参数 In {编码预设, 配置文件, 场景优化, 像素格式}
                    If 单项参数 IsNot Nothing AndAlso 单项参数.参数名 <> "" AndAlso Not 结果.Contains(单项参数.参数名) Then 结果.Add(单项参数.参数名)
                Next
                If 图片质量 IsNot Nothing AndAlso 图片质量.参数名 <> "" AndAlso Not 结果.Contains(图片质量.参数名) Then 结果.Add(图片质量.参数名)
                For Each 单项参数 In 特殊参数列表.Concat(必要参数列表).Concat(默认附加参数列表)
                    If 单项参数.参数名 <> "" AndAlso Not 结果.Contains(单项参数.参数名) Then 结果.Add(单项参数.参数名)
                Next
                Return 结果
            End Get
        End Property
    End Class

    Public Class 编码器参数列表数据
        Public Property 参数名 As String = ""
        Public Property 值列表 As New List(Of String)
        Public Property 默认值 As String = ""
        Public Property 值范围说明 As String = ""
        Public Property 值说明 As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)
    End Class

    Public Class 图片质量参数数据
        Public Property 参数名 As String = ""
        Public Property 默认值 As String = ""
        Public Property 值范围说明 As String = ""
    End Class

    Public Class 编码器特殊参数数据
        Public Property 参数名 As String = ""
        Public Property 值列表 As New List(Of String)
        Public Property 默认值 As String = ""
        Public Property 值范围说明 As String = ""
        Public Property 说明 As String = ""
        Public Property 是否必要 As Boolean = False
    End Class

End Class
