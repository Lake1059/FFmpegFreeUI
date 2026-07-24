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
        Return New List(Of String) From {"", "-crf", "-cq", "-global_quality", "-qp"}
    End Function

    Private Shared Sub 初始化分类()
        加入分类(预设数据_v6.视频编码器类型.视频, "复制流", "不重编码，保留原视频；修复封装或元数据问题时适用。图片流也属于视频流。", "copy")
        加入分类(预设数据_v6.视频编码器类型.视频, "AV2", "等待 FFmpeg 添加支持。", "av2", "avmenc")
        加入分类(预设数据_v6.视频编码器类型.视频, "H.266/VVC", "目前仅 libvvenc，默秒全，国家电网战略合作伙伴。", "libvvenc")
        加入分类(预设数据_v6.视频编码器类型.视频, "AV1", "当前适合个人存储的高压缩编码；建议优先考虑 NVIDIA 和 Intel。", "libaom-av1", "libsvtav1", "av1_nvenc", "av1_qsv", "av1_amf", "av1_d3d12va", "librav1e", "av1_vulkan")
        加入分类(预设数据_v6.视频编码器类型.视频, "H.265/HEVC", "目前主流编码，要对外发布优先考虑。", "libx265", "hevc_nvenc", "hevc_qsv", "hevc_amf", "hevc_d3d12va", "hevc_vulkan", "libkvazaar", "libsvt_hevc")
        加入分类(预设数据_v6.视频编码器类型.视频, "H.264/AVC", "建议仅作为旧设备兼容备选，优先考虑 HEVC。", "libx264", "libx264rgb", "libopenh264", "h264_nvenc", "h264_qsv", "h264_amf", "h264_d3d12va", "h264_vulkan")
        加入分类(预设数据_v6.视频编码器类型.视频, "ProRes", "高码率剪辑中间格式，不适合最终存储。", "prores_ks", "prores_aw")
        加入分类(预设数据_v6.视频编码器类型.视频, "VP9 # VP8", "Google 视频格式。", "libvpx-vp9", "libsvt_vp9", "vp9_qsv", "libvpx")
        加入分类(预设数据_v6.视频编码器类型.视频, "FFv1", "博物馆级别无损存档格式。", "ffv1 -level 3", "ffv1 -level 1", "ffv1_vulkan")
        加入分类(预设数据_v6.视频编码器类型.视频, "其他现代编码", "较新或生态较弱的编码。", "libxeve", "libxavs", "libxavs2", "libuavs3e", "liboapv")
        加入分类(预设数据_v6.视频编码器类型.视频, "老旧编码", "仅用于旧设备。", "mpeg4", "libxvid", "rv20", "rv10", "wmv2", "wmv1")
        加入分类(预设数据_v6.视频编码器类型.视频, "禁用", "不输出视频流。", "-vn")
        加入分类(预设数据_v6.视频编码器类型.视频, "自定义", "在配置文件中定义；重启后生效。手动编辑前请退出程序。")

        加入分类(预设数据_v6.视频编码器类型.图片, "PNG", "无损静图；质量值控制压缩速度。", "png")
        加入分类(预设数据_v6.视频编码器类型.图片, "APNG", "PNG 动图；循环和末帧延迟在必要参数中设置。", "apng")
        加入分类(预设数据_v6.视频编码器类型.图片, "JPEG\JPG", "常用有损静图；质量值越小越清晰。", "mjpeg")
        加入分类(预设数据_v6.视频编码器类型.图片, "WEBP 静图", "支持有损、无损和用途预设。", "libwebp")
        加入分类(预设数据_v6.视频编码器类型.图片, "WEBP 动图", "WebP 动图；循环次数在必要参数中设置。", "libwebp_anim")
        加入分类(预设数据_v6.视频编码器类型.图片, "GIF", "调色板动图；建议启用调色板滤镜。", "gif")
        加入分类(预设数据_v6.视频编码器类型.图片, "BMP", "未压缩位图，体积大、兼容性好。", "bmp")
        加入分类(预设数据_v6.视频编码器类型.图片, "OpenJPEG", "JPEG 2000，适合归档和影院规范。", "libopenjpeg")
        加入分类(预设数据_v6.视频编码器类型.图片, "JPEG-LS", "无损或近无损图片。", "jpegls")
        加入分类(预设数据_v6.视频编码器类型.图片, "SVT JPEG XS", "需要含 libsvtjpegxs 的 FFmpeg。", "libsvtjpegxs")
        加入分类(预设数据_v6.视频编码器类型.图片, "HDR (Radiance RGBE)", "Radiance RGBE HDR 图片。", "hdr")
        加入分类(预设数据_v6.视频编码器类型.图片, "TIFF", "TIFF 静图；压缩算法可单独设置。", "tiff")
        加入分类(预设数据_v6.视频编码器类型.图片, "DPX", "电影制作常用图像序列。", "dpx")
        加入分类(预设数据_v6.视频编码器类型.图片, "OpenEXR", "高动态范围图像，常用于 VFX 和线性工作流。", "exr")
    End Sub

    Private Shared Sub 初始化编码器()
        加入编码器(基础("copy", "复制视频流，不重新编码。", "复制流", 预设数据_v6.视频编码器类型.视频, 是否复制流:=True))
        加入编码器(基础("-vn", "禁用视频流输出。", "禁用", 预设数据_v6.视频编码器类型.视频, 是否禁用:=True))

        加入编码器(基础("av2", "", "AV2", 预设数据_v6.视频编码器类型.视频))
        加入编码器(基础("avmenc", "", "AV2", 预设数据_v6.视频编码器类型.视频))

        加入编码器(基础("libvvenc", "VVC/H.266 软件编码。", "H.266/VVC", 预设数据_v6.视频编码器类型.视频,
            编码预设:=参数("-preset", "slower 最慢 ~ faster 最快", "faster", "slower", "slow", "medium", "fast", "faster"),
            像素格式:=像素("yuv420p10le"),
            特殊参数:=特殊列表(特殊("-qp 恒定量化：-1=自动，0~63；越低越清晰"), 特殊("-qpa 感知自适应量化，默认开启，0/1"), 特殊("-vvenc-params vvenc 原生参数：key=value:key=value")),
            支持二次编码:=True))

        加入编码器(基础("libaom-av1", "AOM AV1 软件编码，压缩强、速度慢。", "AV1", 预设数据_v6.视频编码器类型.视频,
            编码预设:=参数("-cpu-used", "0 最慢 ~ 8 最快", "5", "0", "1", "2", "3", "4", "5", "6", "7", "8"),
            配置文件:=参数("-profile:v", "0 main / 1 high / 2 professional", "", "0", "1", "2"),
            场景优化:=参数("-tune", "psnr 或 ssim", "", "psnr", "ssim"),
            像素格式:=像素("yuv420p yuv422p yuv444p gbrp yuv420p10le yuv422p10le yuv444p10le yuv420p12le yuv422p12le yuv444p12le gbrp10le gbrp12le gray gray10le gray12le"),
            特殊参数:=特殊列表(特殊("-crf 恒定质量：-1=自动，0~63；越低越清晰"), 特殊("-usage 用途：good=质量，realtime=实时，allintra=全帧内"), 特殊("-lag-in-frames alt-ref 前瞻：-1=自动，0 起；越大延迟越高"), 特殊("-aq-mode 自适应量化：-1=自动，0=关闭，1=方差，2=复杂度，3=循环刷新"), 特殊("-arnr-strength alt-ref 降噪：-1=自动，0~6；越高越强"), 特殊("-aom-params libaom 原生参数：key=value:key=value")),
            无损模式说明:="使用 -crf 0 -b:v 0，并避免有损像素格式转换。",
            支持二次编码:=True))

        加入编码器(基础("libsvtav1", "SVT-AV1 软件编码，速度与压缩效率均衡。", "AV1", 预设数据_v6.视频编码器类型.视频,
            编码预设:=参数("-preset", "-2 自动；0 最慢 ~ 13 最快", "6", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13"),
            配置文件:=参数("-profile:v", "main/high/professional", "", "main", "high", "professional"),
            像素格式:=像素("yuv420p yuv420p10le"),
            特殊参数:=特殊列表(特殊("-crf 恒定质量：0~63；越低越清晰"), 特殊("-qp 固定 QP：0~63；越低越清晰"), 特殊("-svtav1-params SVT-AV1 原生参数：key=value:key=value")),
            无损模式说明:="不支持。"))

        加入编码器(基础("av1_nvenc", "NVIDIA NVENC AV1 硬件编码。", "AV1", 预设数据_v6.视频编码器类型.视频,
            编码预设:=参数("-preset", "p7 最慢最好 ~ p1 最快", "p7", "p7", "p6", "p5", "p4", "p3", "p2", "p1"),
            场景优化:=参数("-tune", "hq/uhq/ll/ull/lossless", "", "hq", "uhq", "ll", "ull", "lossless"),
            像素格式:=像素("yuv420p nv12 p010le yuv444p p016le nv16 p210le p216le yuv444p10msble yuv444p16le bgr0 bgra rgb0 rgba x2rgb10le x2bgr10le gbrp gbrp10msble gbrp16le cuda d3d11"),
            特殊参数:=合并特殊列表(特殊列表(特殊("-gpu GPU：-2=list 列出设备，-1=any 自动，0 起为编号")), NVENC质量参数("0=自动；1~63，越低越清晰", "-1=自动；0~255，越低越清晰")),
            视觉体积均衡点:="RTX 50 参考：标准 cq 36；UHQ cq 38；极简内容可试 cq 42。",
            无损模式说明:="使用 -tune lossless；也可用 -rc constqp -qp 0，取决于硬件和驱动。"))

        加入编码器(基础("av1_qsv", "Intel Quick Sync AV1 硬件编码。", "AV1", 预设数据_v6.视频编码器类型.视频,
            编码预设:=参数("-preset", "veryslow 最慢 ~ veryfast 最快", "veryslow", "veryslow", "slower", "slow", "medium", "fast", "faster", "veryfast"),
            配置文件:=参数("-profile:v", "main", "", "main"),
            像素格式:=像素("nv12 p010le qsv"),
            特殊参数:=合并特殊列表(QSV质量参数(), 特殊列表(特殊("-qsv_params QSV 原生参数：key=value:key=value")))))

        加入编码器(基础("av1_amf", "AMD AMF AV1 硬件编码。", "AV1", 预设数据_v6.视频编码器类型.视频,
            编码预设:=参数("-quality", "high_quality 最好 ~ speed 最快", "high_quality", "high_quality", "quality", "balanced", "speed"),
            配置文件:=参数("-profile:v", "main", "", "main"),
            场景优化:=参数("-usage", "transcoding/low latency/high quality", "", "transcoding", "ultralowlatency", "lowlatency", "high_quality", "lowlatency_high_quality"),
            像素格式:=像素("nv12 yuv420p d3d11 dxva2_vld p010le amf bgr0 rgb0 bgra argb rgba x2bgr10le rgbaf16le"),
            特殊参数:=AMF质量参数("-1=自动；0~255，越低越清晰"),
            无损模式说明:="不支持；最低量化可用 -rc cqp -qp_i 0 -qp_p 0 -qp_b 0。"))

        加入编码器(基础("av1_d3d12va", "D3D12VA AV1 硬件编码器占位项。", "AV1", 预设数据_v6.视频编码器类型.视频,
            配置文件:=参数("-profile:v", "main/high/professional", "", "main", "high", "professional"),
            像素格式:=像素("d3d12")))

        加入编码器(基础("librav1e", "rav1e AV1 软件编码。", "AV1", 预设数据_v6.视频编码器类型.视频,
            编码预设:=参数("-speed", "0 最慢 ~ 10 最快", "", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10"),
            像素格式:=像素("yuv420p yuvj420p yuv420p10le yuv420p12le yuv422p yuvj422p yuv422p10le yuv422p12le yuv444p yuvj444p yuv444p10le yuv444p12le"),
            特殊参数:=特殊列表(特殊("-qp 固定量化：-1=自动，0~255；越低越清晰"), 特殊("-rav1e-params rav1e 原生参数：key=value:key=value")),
            无损模式说明:="使用 -qp 0，并避免有损像素格式转换。"))

        加入编码器(基础("av1_vulkan", "Vulkan AV1 硬件编码。", "AV1", 预设数据_v6.视频编码器类型.视频,
            编码预设:=参数("-quality", "5 最慢 ~ 0 最快", "", "5", "4", "3", "2", "1", "0"),
            配置文件:=参数("-profile:v", "main/high/professional", "", "main", "high", "professional"),
            场景优化:=参数("-tune", "default/hq/ll/ull/lossless", "", "default", "hq", "ll", "ull", "lossless"),
            像素格式:=像素("vulkan"),
            特殊参数:=合并特殊列表(Vulkan质量参数("-1=自动；0~255，越低越清晰"), 特殊列表(特殊("-usage 用途：transcode=转码，stream=直播，record=录制，conference=会议"))),
            无损模式说明:="使用 -tune lossless；建议配合 -rc_mode cqp -qp 0，取决于 Vulkan 驱动。"))

        加入编码器(基础("libx265", "x265 HEVC 软件编码。", "H.265/HEVC", 预设数据_v6.视频编码器类型.视频,
            编码预设:=参数("-preset", "veryslow 最慢 ~ ultrafast 最快", "slow", "veryslow", "slower", "slow", "medium", "fast", "faster", "veryfast", "superfast", "ultrafast"),
            配置文件:=参数("-profile:v", "main/main10/main12/mainstillpicture", "", "main", "main10", "main12", "mainstillpicture"),
            场景优化:=参数("-tune", "psnr/ssim/grain/fastdecode/zerolatency", "", "psnr", "ssim", "grain", "fastdecode", "zerolatency", "animation", "stillimage"),
            像素格式:=像素("yuv420p yuvj420p yuv422p yuvj422p yuv444p yuvj444p gbrp yuv420p10le yuv422p10le yuv444p10le gbrp10le yuv420p12le yuv422p12le yuv444p12le gbrp12le gray gray10le gray12le yuva420p yuva420p10le"),
            特殊参数:=特殊列表(特殊("-crf 恒定质量：-1=自动，常用 0~51；越低越清晰"), 特殊("-qp 固定 QP：-1=自动，0 起；越低越清晰"), 特殊("-x265-params x265 原生参数：key=value:key=value")),
            无损模式说明:="使用 -x265-params lossless=1。",
            支持二次编码:=True))

        加入编码器(基础("hevc_nvenc", "NVIDIA NVENC HEVC 硬件编码。", "H.265/HEVC", 预设数据_v6.视频编码器类型.视频,
            编码预设:=参数("-preset", "p7 最慢最好 ~ p1 最快；lossless/losslesshp 为无损模式", "p7", "p7", "p6", "p5", "p4", "p3", "p2", "p1", "lossless", "losslesshp"),
            配置文件:=参数("-profile:v", "main/main10/rext", "", "main", "main10", "rext"),
            场景优化:=参数("-tune", "hq/uhq/ll/ull/lossless", "", "hq", "uhq", "ll", "ull", "lossless"),
            像素格式:=像素("yuv420p nv12 p010le yuv444p p016le nv16 p210le p216le yuv444p10msble yuv444p16le bgr0 bgra rgb0 rgba x2rgb10le x2bgr10le gbrp gbrp10msble gbrp16le cuda d3d11"),
            特殊参数:=合并特殊列表(特殊列表(特殊("-gpu GPU：-2=list 列出设备，-1=any 自动，0 起为编号"), 特殊("-tier HEVC 层级：main/high")), NVENC质量参数("0=自动；1~51，越低越清晰", "-1=自动；0~51，越低越清晰")),
            视觉体积均衡点:="RTX 50 参考：标准 cq 26；UHQ cq 28。",
            无损模式说明:="使用 -tune lossless 或 -preset lossless/losslesshp；也可用 -rc constqp -qp 0。"))

        加入编码器(基础("hevc_qsv", "Intel Quick Sync HEVC 硬件编码。", "H.265/HEVC", 预设数据_v6.视频编码器类型.视频,
            编码预设:=参数("-preset", "veryslow 最慢 ~ veryfast 最快", "veryslow", "veryslow", "slower", "slow", "medium", "fast", "faster", "veryfast"),
            配置文件:=参数("-profile:v", "main/main10/mainsp/rext/scc", "", "main", "main10", "mainsp", "rext", "scc"),
            像素格式:=像素("nv12 p010le p012le yuyv422 y210le qsv bgra x2rgb10le vuyx xv30le"),
            特殊参数:=合并特殊列表(QSV质量参数(), 特殊列表(特殊("-tier HEVC tier，main/high")))))

        加入编码器(基础("hevc_amf", "AMD AMF HEVC 硬件编码。", "H.265/HEVC", 预设数据_v6.视频编码器类型.视频,
            编码预设:=参数("-quality", "quality 最好 ~ speed 最快", "quality", "quality", "balanced", "speed"),
            配置文件:=参数("-profile:v", "main/main10", "", "main", "main10"),
            场景优化:=参数("-usage", "transcoding/low latency/high quality", "", "transcoding", "ultralowlatency", "lowlatency", "webcam", "high_quality", "lowlatency_high_quality"),
            像素格式:=像素("nv12 yuv420p d3d11 dxva2_vld p010le amf bgr0 rgb0 bgra argb rgba x2bgr10le rgbaf16le"),
            特殊参数:=合并特殊列表(AMF质量参数("-1=自动；0~51，越低越清晰", False), 特殊列表(特殊("-profile_tier HEVC 层级：main/high"))),
            无损模式说明:="不支持；最低量化可用 -rc cqp -qp_i 0 -qp_p 0。"))

        加入编码器(基础("hevc_d3d12va", "D3D12VA HEVC 硬件编码。", "H.265/HEVC", 预设数据_v6.视频编码器类型.视频,
            配置文件:=参数("-profile:v", "main/main10", "", "main", "main10"),
            像素格式:=像素("d3d12"),
            特殊参数:=D3D12VA质量参数("0~52；越低越清晰")))

        加入编码器(基础("hevc_vulkan", "Vulkan HEVC 硬件编码。", "H.265/HEVC", 预设数据_v6.视频编码器类型.视频,
            编码预设:=参数("-quality", "5 最慢 ~ 0 最快", "", "5", "4", "3", "2", "1", "0"),
            配置文件:=参数("-profile:v", "main/main10/rext", "", "main", "main10", "rext"),
            场景优化:=参数("-tune", "default/hq/ll/ull/lossless", "", "default", "hq", "ll", "ull", "lossless"),
            像素格式:=像素("vulkan"),
            特殊参数:=Vulkan质量参数("-1=自动；0~255，越低越清晰"),
            无损模式说明:="使用 -tune lossless；建议配合 -rc_mode cqp -qp 0，取决于 Vulkan 驱动。"))

        加入编码器(基础("libkvazaar", "Kvazaar HEVC 软件编码。", "H.265/HEVC", 预设数据_v6.视频编码器类型.视频,
            像素格式:=像素("yuv420p"),
            特殊参数:=特殊列表(特殊("-kvazaar-params Kvazaar 原生参数：key=value,key=value")),
            无损模式说明:="使用 -kvazaar-params lossless=1。"))

        加入编码器(基础("libsvt_hevc", "SVT-HEVC 软件编码。", "H.265/HEVC", 预设数据_v6.视频编码器类型.视频,
            编码预设:=参数("-preset", "0 最慢 ~ 12 最快", "7", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12"),
            配置文件:=参数("-profile:v", "1 ~ 4", "", "1", "2", "3", "4"),
            场景优化:=参数("-tune", "sq/oq/vmaf", "", "sq", "oq", "vmaf"),
            像素格式:=像素("yuv420p yuv420p10le yuv422p yuv422p10le yuv444p yuv444p10le"),
            特殊参数:=特殊列表(特殊("-qp QP：0~51，越低越清晰"), 特殊("-rc 码率模式：cqp=固定 QP，vbr=目标码率"), 特殊("-la_depth 前瞻：-1=自动，0~256；越大延迟和内存占用越高"), 特殊("-thread_count 线程数：0=自动，1 起为固定值"))))

        加入编码器(基础("libx264", "x264 H.264 软件编码。", "H.264/AVC", 预设数据_v6.视频编码器类型.视频,
            编码预设:=参数("-preset", "veryslow 最慢 ~ ultrafast 最快", "slower", "veryslow", "slower", "slow", "medium", "fast", "faster", "veryfast", "superfast", "ultrafast"),
            配置文件:=参数("-profile:v", "baseline/main/high/high10/high422/high444", "", "baseline", "main", "high", "high10", "high422", "high444"),
            场景优化:=参数("-tune", "film/animation/grain/stillimage/psnr/ssim/fastdecode/zerolatency", "", "film", "animation", "grain", "stillimage", "psnr", "ssim", "fastdecode", "zerolatency"),
            像素格式:=像素("yuv420p yuvj420p yuv422p yuvj422p yuv444p yuvj444p nv12 nv16 nv21 yuv420p10le yuv422p10le yuv444p10le nv20le gray gray10le"),
            特殊参数:=特殊列表(特殊("-crf 恒定质量：-1=自动，常用 0~51；越低越清晰"), 特殊("-crf_max CRF+VBV 的最低质量限制：-1=自动，常用 0~51；越低限制越严格"), 特殊("-qp 固定 QP：-1=自动，0 起；越低越清晰"), 特殊("-aq-mode 自适应量化：-1=自动，0=关闭，1=方差，2=自动方差，3=暗场景自动方差"), 特殊("-aq-strength AQ 强度：-1=自动，0 起；越高越强"), 特殊("-x264-params x264 原生参数：key=value:key=value")),
            无损模式说明:="使用 -qp 0 或 -crf 0。",
            支持二次编码:=True))

        加入编码器(基础("libx264rgb", "x264 RGB 输入编码。", "H.264/AVC", 预设数据_v6.视频编码器类型.视频,
            编码预设:=参数("-preset", "veryslow 最慢 ~ ultrafast 最快", "slower", "veryslow", "slower", "slow", "medium", "fast", "faster", "veryfast", "superfast", "ultrafast"),
            配置文件:=参数("-profile:v", "high/high444", "", "high", "high444"),
            场景优化:=参数("-tune", "film/animation/grain/stillimage/psnr/ssim/fastdecode/zerolatency", "", "film", "animation", "grain", "stillimage", "psnr", "ssim", "fastdecode", "zerolatency"),
            像素格式:=像素("bgr0 bgr24 rgb24"),
            特殊参数:=特殊列表(特殊("-crf 恒定质量：-1=自动，常用 0~51；越低越清晰"), 特殊("-crf_max CRF+VBV 的最低质量限制：-1=自动，常用 0~51；越低限制越严格"), 特殊("-qp 固定 QP：-1=自动，0 起；越低越清晰"), 特殊("-aq-mode 自适应量化：-1=自动，0=关闭，1=方差，2=自动方差，3=暗场景自动方差"), 特殊("-aq-strength AQ 强度：-1=自动，0 起；越高越强"), 特殊("-x264-params x264 原生参数：key=value:key=value")),
            无损模式说明:="使用 -qp 0 或 -crf 0。",
            支持二次编码:=True))

        加入编码器(基础("libopenh264", "OpenH264，适合基础兼容需求。", "H.264/AVC", 预设数据_v6.视频编码器类型.视频,
            配置文件:=参数("-profile:v", "constrained_baseline/main/high", "", "constrained_baseline", "main", "high"),
            像素格式:=像素("yuv420p yuvj420p"),
            特殊参数:=特殊列表(特殊("-rc_mode 码率模式：quality=质量优先，bitrate=目标码率，buffer=缓冲约束，timestamp=按时间戳调节"))))

        加入编码器(基础("h264_nvenc", "NVIDIA NVENC H.264 硬件编码。", "H.264/AVC", 预设数据_v6.视频编码器类型.视频,
            编码预设:=参数("-preset", "p7 最慢最好 ~ p1 最快；lossless/losslesshp 为无损模式", "p7", "p7", "p6", "p5", "p4", "p3", "p2", "p1", "lossless", "losslesshp"),
            配置文件:=参数("-profile:v", "baseline/main/high/high10/high422/high444p", "", "baseline", "main", "high", "high10", "high422", "high444p"),
            场景优化:=参数("-tune", "hq/ll/ull/lossless", "", "hq", "ll", "ull", "lossless"),
            像素格式:=像素("yuv420p nv12 p010le yuv444p p016le nv16 p210le p216le yuv444p10msble yuv444p16le bgr0 bgra rgb0 rgba x2rgb10le x2bgr10le gbrp gbrp10msble gbrp16le cuda d3d11"),
            特殊参数:=合并特殊列表(特殊列表(特殊("-gpu GPU：-2=list 列出设备，-1=any 自动，0 起为编号")), NVENC质量参数("0=自动；1~51，越低越清晰", "-1=自动；0~51，越低越清晰")),
            无损模式说明:="使用 -tune lossless 或 -preset lossless/losslesshp；也可用 -rc constqp -qp 0。"))

        加入编码器(基础("h264_qsv", "Intel Quick Sync H.264 硬件编码。", "H.264/AVC", 预设数据_v6.视频编码器类型.视频,
            编码预设:=参数("-preset", "veryslow 最慢 ~ veryfast 最快", "veryslow", "veryslow", "slower", "slow", "medium", "fast", "faster", "veryfast"),
            配置文件:=参数("-profile:v", "baseline/main/high", "", "baseline", "main", "high"),
            像素格式:=像素("nv12 qsv"),
            特殊参数:=QSV质量参数(True)))

        加入编码器(基础("h264_amf", "AMD AMF H.264 硬件编码。", "H.264/AVC", 预设数据_v6.视频编码器类型.视频,
            编码预设:=参数("-quality", "quality 最好 ~ speed 最快", "quality", "quality", "balanced", "speed"),
            配置文件:=参数("-profile:v", "main/high/constrained_baseline/constrained_high", "", "main", "high", "constrained_baseline", "constrained_high"),
            场景优化:=参数("-usage", "transcoding/low latency/high quality", "", "transcoding", "ultralowlatency", "lowlatency", "webcam", "high_quality", "lowlatency_high_quality"),
            像素格式:=像素("nv12 yuv420p d3d11 dxva2_vld p010le amf bgr0 rgb0 bgra argb rgba x2bgr10le rgbaf16le"),
            特殊参数:=AMF质量参数("-1=自动；0~51，越低越清晰"),
            无损模式说明:="不支持；最低量化可用 -rc cqp -qp_i 0 -qp_p 0 -qp_b 0。"))

        加入编码器(基础("h264_d3d12va", "D3D12VA H.264 硬件编码。", "H.264/AVC", 预设数据_v6.视频编码器类型.视频,
            配置文件:=参数("-profile:v", "main/high/high10", "", "main", "high", "high10"),
            像素格式:=像素("d3d12"),
            特殊参数:=合并特殊列表(D3D12VA质量参数("0~52；越低越清晰"), 特殊列表(特殊("-coder 熵编码：cabac=压缩更好、解码更重，cavlc=兼容且简单")))))

        加入编码器(基础("h264_vulkan", "Vulkan H.264 硬件编码。", "H.264/AVC", 预设数据_v6.视频编码器类型.视频,
            编码预设:=参数("-quality", "5 最慢 ~ 0 最快", "", "5", "4", "3", "2", "1", "0"),
            配置文件:=参数("-profile:v", "constrained_baseline/main/high/high444p", "", "constrained_baseline", "main", "high", "high444p"),
            场景优化:=参数("-tune", "default/hq/ll/ull/lossless", "", "default", "hq", "ll", "ull", "lossless"),
            像素格式:=像素("vulkan"),
            特殊参数:=Vulkan质量参数("-1=自动；0~255，越低越清晰"),
            无损模式说明:="使用 -tune lossless；建议配合 -rc_mode cqp -qp 0，取决于 Vulkan 驱动。"))

        加入编码器(基础("prores_ks", "Apple ProRes，剪辑中间格式。", "ProRes", 预设数据_v6.视频编码器类型.视频,
            配置文件:=参数("-profile:v", "proxy/lt/standard/hq/4444/4444xq", "", "auto", "proxy", "lt", "standard", "hq", "4444", "4444xq"),
            像素格式:=像素("yuv422p10le yuv444p10le yuva444p10le"),
            特殊参数:=特殊列表(特殊("-quant_mat 量化矩阵：auto/default/proxy/lt/standard/hq"))))

        加入编码器(基础("prores_aw", "Apple ProRes，功能较简。", "ProRes", 预设数据_v6.视频编码器类型.视频,
            像素格式:=像素("yuv422p10le yuv444p10le yuva444p10le")))

        加入编码器(基础("libvpx-vp9", "libvpx VP9 软件编码器；速度由 -cpu-used 控制。", "VP9 # VP8", 预设数据_v6.视频编码器类型.视频,
            编码预设:=参数("-cpu-used", "-8 最慢 ~ 8 最快", "", "0", "1", "2", "3", "4", "5", "6", "7", "8"),
            场景优化:=参数("-tune", "psnr 或 ssim", "", "psnr", "ssim"),
            像素格式:=像素("yuv420p yuva420p yuv422p yuva422p yuv440p yuv444p yuva444p yuv420p10le yuva420p10le yuv422p10le yuva422p10le yuv440p10le yuv444p10le yuva444p10le yuv420p12le yuv422p12le yuv440p12le yuv444p12le yuva444p12le gbrp gbrap gbrp10le gbrap10le gbrp12le gbrap12le"),
            特殊参数:=特殊列表(特殊("-crf 恒定质量：-1=自动，0~63；越低越清晰。通常配合 -b:v 0"), 特殊("-lossless 无损模式：-1=自动，0=关闭，1=开启"), 特殊("-deadline 编码策略：best=最慢最好，good=均衡，realtime=实时"), 特殊("-lag-in-frames alt-ref 前瞻：-1=自动，0 起；越大延迟越高"), 特殊("-auto-alt-ref alt-ref：-1=自动，0=关闭，1 及以上=不同策略；二次编码常用"), 特殊("-aq-mode 自适应量化：-1=自动，0=关闭，1=方差，2=复杂度，3=循环刷新"), 特殊("-arnr-strength alt-ref 降噪：-1=自动，0 起；越高越强"), 特殊("-tune-content 内容：default=视频，screen=屏幕")),
            无损模式说明:="使用 -lossless 1。"))

        加入编码器(基础("libsvt_vp9", "SVT-VP9 软件编码。", "VP9 # VP8", 预设数据_v6.视频编码器类型.视频,
            编码预设:=参数("-preset", "0 最慢 ~ 9 最快", "", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9"),
            场景优化:=参数("-tune", "vq/ssim/vmaf", "", "vq", "ssim", "vmaf"),
            像素格式:=像素("yuv420p")))

        加入编码器(基础("vp9_qsv", "Intel Quick Sync VP9 硬件编码。", "VP9 # VP8", 预设数据_v6.视频编码器类型.视频,
            编码预设:=参数("-preset", "veryslow 最慢 ~ veryfast 最快", "veryslow", "veryslow", "slower", "slow", "medium", "fast", "faster", "veryfast"),
            配置文件:=参数("-profile:v", "profile0/profile1/profile2/profile3", "", "profile0", "profile1", "profile2", "profile3"),
            像素格式:=像素("nv12 p010le vuyx qsv xv30le"),
            特殊参数:=QSV质量参数()))

        加入编码器(基础("libvpx", "libvpx VP8 软件编码器；速度由 -cpu-used 控制。", "VP9 # VP8", 预设数据_v6.视频编码器类型.视频,
            编码预设:=参数("-cpu-used", "-16 最慢 ~ 16 最快", "", "0", "1", "2", "3", "4", "5", "6", "7", "8"),
            场景优化:=参数("-tune", "psnr 或 ssim", "", "psnr", "ssim"),
            像素格式:=像素("yuv420p yuva420p"),
            特殊参数:=特殊列表(特殊("-crf 恒定质量：-1=自动，0~63；越低越清晰。通常配合 -b:v 0"), 特殊("-deadline 编码策略：best=最慢最好，good=均衡，realtime=实时"), 特殊("-lag-in-frames alt-ref 前瞻：-1=自动，0 起；越大延迟越高"), 特殊("-auto-alt-ref alt-ref：-1=自动，0=关闭，1/2=不同策略；二次编码常用"), 特殊("-arnr-strength alt-ref 降噪：-1=自动，0 起；越高越强")),
            无损模式说明:="不支持。"))

        加入编码器(基础("ffv1 -level 3", "FFv1 level 3，推荐的无损存档级别。", "FFv1", 预设数据_v6.视频编码器类型.视频,
            命令行编码器名:="ffv1",
            默认附加参数:=特殊列表(特殊("-level FFv1 级别：3，必要")),
            像素格式:=像素("yuv420p yuva420p yuva422p yuv444p yuva444p yuv440p yuv422p yuv411p yuv410p bgr0 bgra yuv420p16le yuv422p16le yuv444p16le yuv444p9le yuv422p9le yuv420p9le yuv420p10le yuv422p10le yuv444p10le yuv420p12le yuv422p12le yuv444p12le yuva444p16le yuva422p16le yuva420p16le gray16le gray gbrp9le gbrp10le gbrp12le gbrp14le gbrp16le rgb48le rgba64le gray10le gray12le gray14le"),
            特殊参数:=特殊列表(特殊("-coder 熵编码：rice/range_def/range_tab"), 特殊("-qtable 无损量化表：default/8bit/greater8bit"))))

        加入编码器(基础("ffv1 -level 1", "FFv1 level 1，旧版兼容选项。", "FFv1", 预设数据_v6.视频编码器类型.视频,
            命令行编码器名:="ffv1",
            默认附加参数:=特殊列表(特殊("-level FFv1 级别：1，必要")),
            像素格式:=像素("yuv420p yuva420p yuv444p bgr0 bgra yuv420p16le yuv444p16le gray16le gray"),
            特殊参数:=特殊列表(特殊("-coder 熵编码：rice/range_def/range_tab"), 特殊("-qtable 无损量化表：default/8bit/greater8bit"))))

        加入编码器(基础("ffv1_vulkan", "FFv1 Vulkan 硬件编码。", "FFv1", 预设数据_v6.视频编码器类型.视频,
            像素格式:=像素("vulkan"),
            特殊参数:=特殊列表(特殊("-level FFv1 级别：3=现代无损存档，1=旧兼容；可用 1/3/4"), 特殊("-coder 熵编码：rice=简单快速，range_def/range_tab=范围编码"), 特殊("-qtable 无损量化表：default=自动，8bit/greater8bit 按位深选择"), 特殊("-force_pcm 所有切片使用 PCM，仅特殊兼容场景使用，0/1"))))

        加入编码器(基础("libxeve", "MPEG-5 EVC 软件编码。", "其他现代编码", 预设数据_v6.视频编码器类型.视频,
            编码预设:=参数("-preset", "placebo 最慢 ~ fast 最快", "", "placebo", "slow", "medium", "fast"),
            配置文件:=参数("-profile:v", "baseline/main", "", "baseline", "main"),
            场景优化:=参数("-tune", "none/zerolatency/psnr", "", "none", "zerolatency", "psnr"),
            像素格式:=像素("yuv420p yuv420p10le"),
            特殊参数:=特殊列表(特殊("-rc_mode 码率模式：CQP=固定 QP，ABR=平均码率，CRF=恒定质量"), 特殊("-qp 固定 QP：0~51，越低越清晰"), 特殊("-crf 恒定质量：10~49，越低越清晰"), 特殊("-xeve-params xeve 原生参数：key=value:key=value")),
            无损模式说明:="不支持；最低量化可用 -qp 0。"))

        加入编码器(基础("libxavs", "AVS 软件编码。", "其他现代编码", 预设数据_v6.视频编码器类型.视频,
            像素格式:=像素("yuv420p"),
            特殊参数:=特殊列表(特殊("-crf 恒定质量：-1=自动；越低越清晰"), 特殊("-qp 固定 QP：-1=自动，0 起；越低越清晰"), 特殊("-cplxblur QP 曲线平滑：-1=自动，0 起；越高越平滑")),
            无损模式说明:="不支持；最低量化可用 -qp 0。"))

        加入编码器(基础("libxavs2", "AVS2 软件编码。", "其他现代编码", 预设数据_v6.视频编码器类型.视频,
            编码预设:=参数("-speed_level", "9 最慢 ~ 0 最快", "", "9", "8", "7", "6", "5", "4", "3", "2", "1", "0"),
            像素格式:=像素("yuv420p"),
            特殊参数:=特殊列表(特殊("-initial_qp 初始 QP：1~63，越低越清晰"), 特殊("-qp QP：1~63，越低越清晰"), 特殊("-min_qp 最小 QP：0~63，限制最高质量"), 特殊("-max_qp 最大 QP：0~63，限制最低质量"), 特殊("-xavs2-params xavs2 原生参数：key=value:key=value")),
            无损模式说明:="不支持；-qp 范围为 1~63。"))

        加入编码器(基础("libuavs3e", "AVS3 软件编码。", "其他现代编码", 预设数据_v6.视频编码器类型.视频,
            编码预设:=参数("-speed", "0 最慢 ~ 6 最快", "", "0", "1", "2", "3", "4", "5", "6"),
            像素格式:=像素("yuv420p yuv420p10le"),
            特殊参数:=特殊列表(特殊("-rc_type 码率类型：0=默认/固定 QP；1/2 的含义随版本变化"), 特殊("-crf 恒定质量：1~63，越低越清晰"), 特殊("-qp QP：1~63，越低越清晰")),
            无损模式说明:="不支持；-qp/-crf 范围为 1~63。"))

        加入编码器(基础("liboapv", "APV 软件编码。", "其他现代编码", 预设数据_v6.视频编码器类型.视频,
            编码预设:=参数("-preset", "placebo 最慢 ~ fastest 最快", "", "placebo", "slow", "medium", "fast", "fastest"),
            像素格式:=像素("gray10le yuv422p10le yuv422p12le yuv444p10le yuv444p12le yuva444p10le yuva444p12le"),
            特殊参数:=特殊列表(特殊("-qp QP：0~63，越低越清晰"), 特殊("-oapv-params APV 原生参数：key=value:key=value")),
            无损模式说明:="不支持；最低量化可用 -qp 0。"))

        For Each 名称 In New String() {"mpeg4", "libxvid", "rv20", "rv10", "wmv2", "wmv1"}
            加入编码器(基础(名称, "旧式编码器，仅用于旧设备兼容。", "老旧编码", 预设数据_v6.视频编码器类型.视频,
                像素格式:=像素("yuv420p")))
        Next

        初始化图片编码器()
    End Sub

    Private Shared Sub 初始化图片编码器()
        加入编码器(基础("png", "PNG 无损静图。", "PNG", 预设数据_v6.视频编码器类型.图片,
            像素格式:=像素("rgb24 rgba rgb48be rgba64be pal8 gray ya8 gray16be ya16be monob"),
            图片质量:=图片质量("-compression_level", "压缩强度：0=最快，9=体积最小", "6"),
            特殊参数:=特殊列表(特殊("-pred PNG 预测：none/sub/up/avg/paeth/mixed"))))

        加入编码器(基础("apng", "APNG 动图。", "APNG", 预设数据_v6.视频编码器类型.图片,
            像素格式:=像素("rgb24 rgba rgb48be rgba64be pal8 gray ya8 gray16be ya16be"),
            图片质量:=图片质量("-compression_level", "压缩强度：0=最快，9=体积最小", "6"),
            必要参数:=特殊列表(特殊("-plays 循环次数：0=无限，1=不循环，必要"), 特殊("-final_delay 末帧延迟：0~65535"))))

        加入编码器(基础("mjpeg", "JPEG/JPG 有损静图。", "JPEG\JPG", 预设数据_v6.视频编码器类型.图片,
            像素格式:=像素("yuvj420p yuvj422p yuvj444p yuv420p yuv422p yuv444p"),
            图片质量:=图片质量("-q:v", "质量：1=最清晰，31=最模糊", "2")))

        加入编码器(基础("libwebp", "WebP 静图。", "WEBP 静图", 预设数据_v6.视频编码器类型.图片,
            像素格式:=像素("bgra yuv420p yuva420p"),
            图片质量:=图片质量("-quality", "质量：0=最模糊，100=最清晰", "75"),
            特殊参数:=特殊列表(特殊("-lossless 无损模式：0=关闭，1=开启；开启后 quality 控制压缩强度"), 特殊("-preset 用途：photo=照片，picture=图片，drawing=线稿，icon=图标，text=文字"))))

        加入编码器(基础("libwebp_anim", "WebP 动图。", "WEBP 动图", 预设数据_v6.视频编码器类型.图片,
            像素格式:=像素("bgra yuv420p yuva420p"),
            图片质量:=图片质量("-quality", "质量：0=最模糊，100=最清晰", "75"),
            必要参数:=特殊列表(特殊("-loop 循环次数：0=无限，1=播放一次，必要")),
            特殊参数:=特殊列表(特殊("-lossless 无损模式：0=关闭，1=开启；开启后 quality 控制压缩强度"))))

        加入编码器(基础("gif", "GIF 调色板动图。", "GIF", 预设数据_v6.视频编码器类型.图片,
            像素格式:=像素("rgb8 bgr8 rgb4_byte bgr4_byte gray pal8"),
            图片质量:=图片质量("调色板滤镜", "启用：palettegen/paletteuse", ""),
            必要参数:=特殊列表(特殊("-loop 循环次数：-1=不循环，0=无限，必要"), 特殊("palettegen/paletteuse 调色板滤镜，建议启用，必要"))))

        加入编码器(基础("bmp", "BMP 未压缩位图。", "BMP", 预设数据_v6.视频编码器类型.图片,
            像素格式:=像素("bgra bgr24 rgb565le rgb555le rgb444le rgb8 bgr8 rgb4_byte bgr4_byte gray pal8 monob")))

        加入编码器(基础("libopenjpeg", "OpenJPEG JPEG 2000。", "OpenJPEG", 预设数据_v6.视频编码器类型.图片,
            像素格式:=像素("rgb24 rgba rgb48le rgba64le gbrp gbrp9le gbrp10le gbrp12le gbrp14le gbrp16le gray ya8 gray16le ya16le gray10le gray12le gray14le yuv420p yuv422p yuva420p yuv440p yuv444p yuva422p yuv411p yuv410p yuva444p yuv420p10le yuv422p10le yuv444p10le yuv420p12le yuv422p12le yuv444p12le xyz12le"),
            图片质量:=图片质量("-q:v", "质量：0.0=有损最大，1.0=无损", ""),
            配置文件:=参数("-profile:v", "jpeg2000/cinema2k/cinema4k", "", "jpeg2000", "cinema2k", "cinema4k"),
            特殊参数:=特殊列表(特殊("-format 封装：j2k=裸码流，jp2=JP2 文件"), 特殊("-irreversible 小波：0=可逆/无损，1=不可逆/有损"))))

        加入编码器(基础("jpegls", "JPEG-LS 无损/近无损图片。", "JPEG-LS", 预设数据_v6.视频编码器类型.图片,
            像素格式:=像素("bgr24 rgb24 gray gray16le"),
            特殊参数:=特殊列表(特殊("-pred 预测方式：left/plane/median"))))

        加入编码器(基础("libsvtjpegxs", "SVT JPEG XS 编码器占位项。", "SVT JPEG XS", 预设数据_v6.视频编码器类型.图片))

        加入编码器(基础("hdr", "Radiance RGBE HDR 图片。", "HDR (Radiance RGBE)", 预设数据_v6.视频编码器类型.图片,
            像素格式:=像素("gbrpf32le")))

        加入编码器(基础("tiff", "TIFF 静图。", "TIFF", 预设数据_v6.视频编码器类型.图片,
            像素格式:=像素("rgb24 rgb48le pal8 rgba rgba64le gray ya8 gray16le ya16le monob monow yuv420p yuv422p yuv440p yuv444p yuv410p yuv411p"),
            特殊参数:=特殊列表(特殊("-compression_algo 压缩算法：packbits/raw/lzw/deflate"))))

        加入编码器(基础("dpx", "DPX 图像序列。", "DPX", 预设数据_v6.视频编码器类型.图片,
            像素格式:=像素("gray rgb24 rgba abgr gray16le gray16be rgb48le rgb48be rgba64le rgba64be gbrp10le gbrp10be gbrp12le gbrp12be")))

        加入编码器(基础("exr", "OpenEXR 高动态范围图片。", "OpenEXR", 预设数据_v6.视频编码器类型.图片,
            像素格式:=像素("grayf32le gbrpf32le gbrapf32le"),
            特殊参数:=特殊列表(特殊("-compression 压缩：none/rle/zips/zip"), 特殊("-format 像素类型：half/float"))))
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
        If 是否复制流 OrElse 是否禁用 Then Return ""
        If 类型 <> 预设数据_v6.视频编码器类型.视频 Then Return ""
        Select Case 分类名称
            Case "ProRes"
                Return "不适用，ProRes 是剪辑中间格式。"
            Case "FFv1"
                Return "不适用，FFv1 是无损存档编码。"
        End Select
        Return ""
    End Function

    Private Shared Function 获取默认无损模式说明(类型 As 预设数据_v6.视频编码器类型, 分类名称 As String, 是否复制流 As Boolean, 是否禁用 As Boolean) As String
        If 是否复制流 OrElse 是否禁用 Then Return ""

        If 类型 = 预设数据_v6.视频编码器类型.图片 Then
            Select Case 分类名称
                Case "PNG", "APNG", "BMP", "DPX", "HDR (Radiance RGBE)"
                    Return "格式本身为无损或未压缩。"
                Case "JPEG\JPG"
                    Return "JPEG/JPG 为有损格式。"
                Case "WEBP 静图", "WEBP 动图"
                    Return "使用 -lossless 1。"
                Case "OpenJPEG"
                    Return "使用 -irreversible 0；不要同时设置有损质量目标。"
                Case "JPEG-LS"
                    Return "默认无损/近无损；不要额外设置有损近似参数。"
                Case "GIF"
                    Return "仅能无损保存量化后的 256 色调色板画面。"
                Case "OpenEXR", "TIFF"
                    Return "常用压缩为无损；实际取决于像素格式和算法。"
            End Select
            Return ""
        End If

        Select Case 分类名称
            Case "FFv1"
                Return "本身无损；常用 -c:v ffv1 -level 3。"
            Case "ProRes"
                Return "不支持严格数学无损；ProRes 是高码率中间格式。"
        End Select

        Return "不支持明确的无损模式。"
    End Function

    Private Shared Function 创建自定义编码器数据(名称 As String) As 视频编码器数据
        Return 基础(名称, "用户自定义编码器，参数由用户维护。", "自定义", 预设数据_v6.视频编码器类型.视频)
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
            .值范围说明 = "可用的常见像素格式",
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

    Private Shared Function 特殊(描述 As String) As 编码器特殊参数数据
        Dim 标准描述 = 规范化特殊参数描述(描述)
        Dim 参数名 = 提取特殊参数名(标准描述)
        Return New 编码器特殊参数数据 With {
            .参数名 = 参数名,
            .描述 = 提取特殊参数说明(标准描述, 参数名)
        }
    End Function

    Private Shared Function 规范化特殊参数描述(描述 As String) As String
        Dim 文本 = If(描述, "").Trim()
        If 文本 = "" Then Return ""

        文本 = 文本.Replace("；", "，").Replace(";", "，")
        文本 = System.Text.RegularExpressions.Regex.Replace(文本, "\s*~\s*", "~")
        文本 = System.Text.RegularExpressions.Regex.Replace(文本, "\s+", " ")

        Do While 文本.Contains("，，")
            文本 = 文本.Replace("，，", "，")
        Loop

        Return 文本.Trim(" "c, "，"c)
    End Function

    Private Shared Function 提取特殊参数名(描述 As String) As String
        Dim 文本 = If(描述, "").Trim()
        If 文本 = "" Then Return ""

        Dim 分隔位置 = 文本.IndexOfAny({" "c, ControlChars.Tab})
        If 分隔位置 < 0 Then Return 文本
        Return 文本.Substring(0, 分隔位置).Trim()
    End Function

    Private Shared Function 提取特殊参数说明(描述 As String, 参数名 As String) As String
        Dim 文本 = If(描述, "").Trim()
        If 文本 = "" OrElse 参数名 = "" OrElse 文本.Length <= 参数名.Length Then Return ""
        Return 文本.Substring(参数名.Length).Trim()
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
            特殊("-rc 码率模式：constqp 用 -qp；vbr 用 -cq；cbr 用 -b:v"),
            特殊("-cq 恒定质量（需 -rc vbr）：" & cq范围),
            特殊("-qmin 最小 QP，限制最高质量：" & qp范围),
            特殊("-qmax 最大 QP，限制最低质量：" & qp范围),
            特殊("-rc-lookahead 前瞻帧数：0 起；越大延迟和显存占用越高"),
            特殊("-multipass 分析精度：disabled=单遍，qres=四分辨率，fullres=全分辨率"),
            特殊("-spatial-aq 空间自适应量化，0/1"),
            特殊("-temporal-aq 时间自适应量化，0/1"),
            特殊("-aq-strength 空间 AQ 强度：1~15，默认 8；越高越激进"),
            特殊("-lookahead_level 前瞻级别：-1=自动，0~15；越高越慢"),
            特殊("-tf_level 时间滤波：-1=自动，0 起；越高越强"))
    End Function

    Private Shared Function QSV质量参数(Optional 是否H264 As Boolean = False) As List(Of 编码器特殊参数数据)
        Dim 结果 = 特殊列表(
            特殊("-global_quality ICQ/LA_ICQ 质量：常用 1~51；越低越清晰"),
            特殊("-extbrc 扩展码率控制：-1=自动，0=关闭，1=开启"),
            特殊("-look_ahead_depth 前瞻深度：0~100；越大延迟和显存占用越高"))
        If 是否H264 Then
            结果.Add(特殊("-look_ahead H.264 前瞻码率控制；启用后配合 -look_ahead_depth，0/1"))
        End If
        Return 结果
    End Function

    Private Shared Function AMF质量参数(qp范围 As String, Optional 含B帧QP As Boolean = True) As List(Of 编码器特殊参数数据)
        Dim 结果 = 特殊列表(
            特殊("-rc 码率模式：cqp=固定 QP，cbr/vbr=目标码率，qvbr/hqvbr=质量优先"),
            特殊("-qvbr_quality_level QVBR 质量（需 qvbr/hqvbr）：-1=自动，0~51；越低越清晰"),
            特殊("-qp_i I 帧 QP：" & qp范围),
            特殊("-qp_p P 帧 QP：" & qp范围),
            特殊("-vbaq 方差自适应量化：auto=驱动决定，0=关闭，1=开启"),
            特殊("-high_motion_quality_boost_enable 高运动质量增强：auto=驱动决定，0/1"),
            特殊("-pa_paq_mode 感知自适应量化：-1=自动，0=关闭，1=内容自适应"),
            特殊("-pa_taq_mode 时间自适应量化：-1=自动，0=关闭，1/2=模式"),
            特殊("-pa_caq_strength 内容自适应量化强度：-1=自动，0=低，1=中，2=高"),
            特殊("-pa_lookahead_buffer_depth 前瞻缓冲：-1=自动，0~41；越大延迟和显存占用越高"))
        If 含B帧QP Then 结果.Add(特殊("-qp_b B 帧 QP：" & qp范围))
        Return 结果
    End Function

    Private Shared Function D3D12VA质量参数(qp范围 As String) As List(Of 编码器特殊参数数据)
        Return 特殊列表(
            特殊("-rc_mode 码率模式：auto/CQP/CBR/VBR/QVBR；CQP=固定 QP，CBR=固定码率，QVBR=质量优先"),
            特殊("-qp 固定 QP（配合 CQP）：" & qp范围))
    End Function

    Private Shared Function Vulkan质量参数(qp范围 As String) As List(Of 编码器特殊参数数据)
        Return 特殊列表(
            特殊("-quality 质量/速度取舍：0 起；越高通常越慢、搜索越充分"),
            特殊("-rc_mode 码率模式：CQP=固定 QP，CBR=固定码率，VBR=目标码率；可用值取决于驱动"),
            特殊("-qp 固定 QP（配合 CQP）：" & qp范围))
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
        Public Property 描述 As String = ""
    End Class

End Class
