
Public Class Form_v6_参数面板_视频编码器

    Public 所属参数面板对象 As Form_v6_参数面板
    Private MCB_像素格式 As LakeUI.ModernComboBox

    Private Sub Form_v6_参数面板_视频编码器_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MCB_像素格式 = 所属参数面板对象.私有界面_色彩管理.MCB_像素格式
    End Sub

    Private Sub MCB_视频编码器类型_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_视频编码器类型.SelectedIndexChanged
        MCB_视频编码器分类.Items.Clear()
        MCB_视频编码器分类.ItemToolTips.Clear()
        MCB_视频编码器分类.Text = ""
        MCB_具体编码器.Items.Clear()
        MCB_具体编码器.ItemToolTips.Clear()
        MCB_编码预设.Items.Clear()
        MCB_编码预设.Text = ""
        MCB_配置文件.Items.Clear()
        MCB_配置文件.Text = ""
        MCB_场景优化.Items.Clear()
        MCB_场景优化.Text = ""
        MCB_像素格式.Items.Clear()
        MCB_像素格式.Text = ""
        Select Case MCB_视频编码器类型.SelectedIndex
            Case 1
                MCB_视频编码器分类.Items.AddRange(New List(Of String) From {"复制流", "AV2", "H.266/VVC", "AV1", "H.265/HEVC", "H.264/AVC", "ProRes", "VP9 && VP8", "FFv1", "其他现代编码", "老旧编码", "禁用", "自定义"})
                MCB_视频编码器分类.ItemToolTips.AddRange(New Dictionary(Of String, String) From {
                            {"复制流", "复制流可以解决多数因为元数据或轻度数据异常问题导致的播放故障，常见于进度条无法拖动、无法获取时长等经典问题。在 ffmpeg 的定义中，图片也是视频，如果你正在编码音乐文件，也要选择这里的复制流来保留专辑图等图像信息。"},
                            {"AV2", "等待 FFmpeg 添加支持。"},
                            {"H.266/VVC", "超前，默秒全，代价是极高的性能要求，名副其实的国家电网战略合作伙伴；截止 2026 年，libx266 仍未发布，只有 libvvenc 一个选择。"},
                            {"AV1", "AOMedia Video 1 作为完全免费开源的高压缩编码，在当前非常适合个人存储，其压缩度比 HEVC 更高，但相应的性能消耗也更大。此编码格式强烈建议优先考虑最新显卡编码。"},
                            {"H.265/HEVC", "HEVC 作为当前最主流最全能的编码，正在被全球压制组、视频平台等渠道广泛使用。截止 2026 年，红绿蓝三家的 HEVC 压缩度距离最新版本的 x265 还有非常明显的距离。"},
                            {"H.264/AVC", "除非目标设备不支持新的编码，否则不应该考虑 AVC。如果要使用此编码格式进行压制工作，请只选择 libx264，红绿蓝三家的压缩度都拉完了，除非你完全不在乎压缩度。"},
                            {"ProRes", "ProRes 是苹果的无损格式，旨在提高剪辑效率，最终存储不应使用，而且 ProRes 本就是几乎没有压缩度可言的编码。"},
                            {"VP9 && VP8", "VP 系列是谷歌的高压缩格式，个人编码这些远不如 265 来得实在，也因为只有谷歌玩的好，经常出现 YouTube 的 VP9 暴打 AV1 的场面。"},
                            {"FFv1", "FFv1 是 FFmpeg 推出的理论无损格式，用于博物馆级别的存档，它保证了每一个像素的信息都是正确的，也就是完全没有压缩度可言，个人存储不应使用。"},
                            {"其他现代编码", "这些编码的压缩度并不差，但目前没有发展起来，效率非常不尽人意。"},
                            {"老旧编码", "在找本世纪 10 年代甚至上个世纪流行的编码？它们都在这。"},
                            {"禁用", "扔掉视频流，就选这个。"},
                            {"自定义", "在 3FUI 设置文件中添加自定义编码器，然后重启软件即可在这里看到，当然在保存之前你应该先退出 3FUI，否则你的改动就全没了，因为退出保存设置的时候不会再读一遍。"}})
                MCB_视频编码器分类.MaxDropDownItems = 15
                MCB_视频编码器分类.SelectedIndex = 0
            Case 2
                MCB_视频编码器分类.Items.AddRange(New List(Of String) From {"PNG", "APNG", "JPEG\JPG", "WEBP 静图", "WEBP 动图", "GIF", "BMP", "FFv1", "OpenJPEG", "JPEG-LS", "SVT JPEG XS", "HDR (Radiance RGBE)", "TIFF", "DPX", "OpenEXR"})
                MCB_视频编码器分类.ItemToolTips.AddRange(New Dictionary(Of String, String) From {
                            {"PNG", "1 最快 ~ 最慢 9"},
                            {"APNG", "1 最快 ~ 最慢 9"},
                            {"JPEG\JPG", "1 清晰 ~ 模糊 31"},
                            {"WEBP", "0 模糊 ~ 清晰 100"},
                            {"GIF", "写 1 来启用调色板生成"},
                            {"OpenJPEG", "0.0 全损 ~ 无损 1.0"}})
                MCB_视频编码器分类.MaxDropDownItems = 10
                MCB_视频编码器分类.SelectedIndex = 0
        End Select
    End Sub

    Private Sub MCB_视频编码器分类_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_视频编码器分类.SelectedIndexChanged
        MCB_具体编码器.Items.Clear()
        MCB_具体编码器.ItemToolTips.Clear()
        MCB_编码预设.Items.Clear()
        MCB_编码预设.Text = ""
        MCB_配置文件.Items.Clear()
        MCB_配置文件.Text = ""
        MCB_场景优化.Items.Clear()
        MCB_场景优化.Text = ""
        MCB_像素格式.Items.Clear()
        MCB_像素格式.Text = ""
        Select Case MCB_视频编码器分类.Text
            Case "复制流"
                MCB_具体编码器.Items.Add("copy")
            Case "AV2"
                MCB_具体编码器.Items.AddRange(New List(Of String) From {"libaom-av2"})
            Case "H.266/VVC"
                MCB_具体编码器.Items.AddRange(New List(Of String) From {"libvvenc"})
            Case "AV1"
                MCB_具体编码器.Items.AddRange(New List(Of String) From {"libaom-av1", "libsvtav1", "av1_nvenc", "av1_qsv", "av1_amf", "av1_d3d12va", "librav1e", "av1_vulkan"})
            Case "H.265/HEVC"
                MCB_具体编码器.Items.AddRange(New List(Of String) From {"libx265", "hevc_nvenc", "hevc_qsv", "hevc_amf", "hevc_d3d12va", "hevc_vulkan", "libkvazaar"})
            Case "H.264/AVC"
                MCB_具体编码器.Items.AddRange(New List(Of String) From {"libx264", "h264_nvenc", "h264_qsv", "h264_amf", "h264_d3d12va", "h264_vulkan"})
            Case "ProRes"
                MCB_具体编码器.Items.AddRange(New List(Of String) From {"prores_ks"})
            Case "VP9 && VP8"
                MCB_具体编码器.Items.AddRange(New List(Of String) From {"libvpx-vp9", "libsvt_vp9", "vp9_qsv", "libvpx"})
            Case "FFv1"
                MCB_具体编码器.Items.AddRange(New List(Of String) From {"ffv1 -level 3", "ffv1 -level 1", "ffv1_vulkan"})
            Case "其他现代编码"
                MCB_具体编码器.Items.AddRange(New List(Of String) From {"libxeve", "libxavs", "libxavs2"})
            Case "老旧编码"
                MCB_具体编码器.Items.AddRange(New List(Of String) From {"mpeg4", "libxvid", "rv20", "rv10", "wmv2", "wmv1"})
            Case "禁用"
                MCB_具体编码器.Items.Add("-vn")
            Case "自定义"
                For Each item In 设置_v6.实例对象.自定义视频编码器列表
                    MCB_具体编码器.Items.Add(item)
                Next
            Case "PNG" : MCB_具体编码器.Items.Add("png")
            Case "APNG" : MCB_具体编码器.Items.Add("apng")
            Case "JPEG\JPG" : MCB_具体编码器.Items.Add("mjpeg")
            Case "WEBP 静图" : MCB_具体编码器.Items.Add("libwebp")
            Case "WEBP 动图" : MCB_具体编码器.Items.Add("libwebp_anim")
            Case "GIF" : MCB_具体编码器.Items.Add("gif")
            Case "BMP" : MCB_具体编码器.Items.Add("bmp")
            Case "OpenJPEG" : MCB_具体编码器.Items.Add("libopenjpeg")
            Case "JPEG-LS" : MCB_具体编码器.Items.Add("jpegls")
            Case "SVT JPEG XS" : MCB_具体编码器.Items.Add("libsvtjpegxs")
            Case "HDR (Radiance RGBE)" : MCB_具体编码器.Items.Add("hdr")
            Case "TIFF" : MCB_具体编码器.Items.Add("tiff")
            Case "DPX" : MCB_具体编码器.Items.Add("dpx")
            Case "OpenEXR" : MCB_具体编码器.Items.Add("exr")
        End Select
        If MCB_具体编码器.Items.Count > 0 Then MCB_具体编码器.SelectedIndex = 0
    End Sub

    Private Sub MCB_具体编码器_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_具体编码器.SelectedIndexChanged
        MCB_编码预设.Items.Clear()
        MCB_编码预设.Text = ""
        MCB_配置文件.Items.Clear()
        MCB_配置文件.Text = ""
        MCB_场景优化.Items.Clear()
        MCB_场景优化.Text = ""
        MCB_像素格式.Items.Clear()
        MCB_像素格式.Text = ""
        Select Case MCB_具体编码器.Text
            Case "libvvenc"
                MCB_编码预设.Items.AddRange(New List(Of String) From {"", "slower", "slow", "medium", "fast", "faster"})
                MCB_配置文件.Items.AddRange(New List(Of String) From {"", "main", "main10"})
                MCB_场景优化.Items.AddRange(New List(Of String) From {})
                MCB_像素格式.Items.AddRange(Split("yuv420p yuv420p10le", " "))
                MCB_编码预设.Text = "faster"
            Case "libaom-av1"
                MCB_编码预设.Items.AddRange(New List(Of String) From {"", "0", "1", "2", "3", "4", "5", "6", "7", "8"})
                MCB_配置文件.Items.AddRange(New List(Of String) From {"", "0", "1", "2"})
                MCB_场景优化.Items.AddRange(New List(Of String) From {"", "psnr", "ssim", "qmt"})
                MCB_像素格式.Items.AddRange(Split("yuv420p yuv422p yuv444p gbrp yuv420p10le yuv422p10le yuv444p10le yuv420p12le yuv422p12le yuv444p12le gbrp10le gbrp12le gray gray10le gray12le", " "))
                MCB_编码预设.Text = "5"
            Case "libsvtav1"
                MCB_编码预设.Items.AddRange(New List(Of String) From {"", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13"})
                MCB_配置文件.Items.AddRange(New List(Of String) From {"", "main", "high", "professional"})
                MCB_场景优化.Items.AddRange(New List(Of String) From {})
                MCB_像素格式.Items.AddRange(Split("yuv420p yuv420p10le", " "))
                MCB_编码预设.Text = "6"
            Case "av1_nvenc"
                MCB_编码预设.Items.AddRange(New List(Of String) From {"", "p7", "p6", "p5", "p4", "p3", "p2", "p1"})
                MCB_配置文件.Items.AddRange(New List(Of String) From {})
                MCB_场景优化.Items.AddRange(New List(Of String) From {"", "hq", "uhq", "ll", "ull", "lossless"})
                MCB_像素格式.Items.AddRange(Split("yuv420p nv12 p010le yuv444p p016le nv16 p210le p216le yuv444p10msble yuv444p16le bgr0 bgra rgb0 rgba x2rgb10le x2bgr10le gbrp gbrp10msble gbrp16le cuda d3d11", " "))
                MCB_编码预设.Text = "p7"
            Case "av1_qsv"
                MCB_编码预设.Items.AddRange(New List(Of String) From {"", "veryslow", "slower", "slow", "medium", "fast", "faster", "veryfast"})
                MCB_配置文件.Items.AddRange(New List(Of String) From {"", "0", "1"})
                MCB_场景优化.Items.AddRange(New List(Of String) From {})
                MCB_像素格式.Items.AddRange(Split("nv12 p010le qsv", " "))
                MCB_编码预设.Text = "veryslow"
            Case "av1_amf"
                MCB_编码预设.Items.AddRange(New List(Of String) From {"", "high_quality", "quality", "balanced", "speed"})
                MCB_配置文件.Items.AddRange(New List(Of String) From {"", "main"})
                MCB_场景优化.Items.AddRange(New List(Of String) From {})
                MCB_像素格式.Items.AddRange(Split("nv12 yuv420p d3d11 dxva2_vld p010le amf bgr0 rgb0 bgra argb rgba x2bgr10le rgbaf16le", " "))
                MCB_编码预设.Text = "high_quality"
            Case "av1_d3d12va"
                MCB_编码预设.Items.AddRange(New List(Of String) From {})
                MCB_配置文件.Items.AddRange(New List(Of String) From {"", "main", "high", "professional"})
                MCB_场景优化.Items.AddRange(New List(Of String) From {})
                MCB_像素格式.Items.AddRange(Split("d3d12", " "))
            Case "librav1e"
                MCB_编码预设.Items.AddRange(New List(Of String) From {})
                MCB_配置文件.Items.AddRange(New List(Of String) From {})
                MCB_场景优化.Items.AddRange(New List(Of String) From {})
                MCB_像素格式.Items.AddRange(Split("yuv420p yuvj420p yuv420p10le yuv420p12le yuv422p yuvj422p yuv422p10le yuv422p12le yuv444p yuvj444p yuv444p10le yuv444p12le", " "))
            Case "av1_vulkan"
                MCB_编码预设.Items.AddRange(New List(Of String) From {})
                MCB_配置文件.Items.AddRange(New List(Of String) From {"", "main", "main10", "professional"})
                MCB_场景优化.Items.AddRange(New List(Of String) From {"", "hq", "ll", "ull", "lossless"})
                MCB_像素格式.Items.AddRange(Split("vulkan", " "))
            Case "libx265"
                MCB_编码预设.Items.AddRange(New List(Of String) From {"", "veryslow", "slower", "slow", "medium", "fast", "faster", "veryfast", "superfast", "ultrafast"})
                MCB_配置文件.Items.AddRange(New List(Of String) From {"", "main", "mainstillpicture"})
                MCB_场景优化.Items.AddRange(New List(Of String) From {"", "psnr", "ssim", "grain", "fastdecode", "zerolatency", "animation", "stillimage"})
                MCB_像素格式.Items.AddRange(Split("yuv420p yuvj420p yuv422p yuvj422p yuv444p yuvj444p gbrp yuv420p10le yuv422p10le yuv444p10le gbrp10le yuv420p12le yuv422p12le yuv444p12le gbrp12le gray gray10le gray12le yuva420p yuva420p10le", " "))
                MCB_编码预设.Text = "slow"
            Case "hevc_nvenc"
                MCB_编码预设.Items.AddRange(New List(Of String) From {"", "p7", "p6", "p5", "p4", "p3", "p2", "p1"})
                MCB_配置文件.Items.AddRange(New List(Of String) From {"", "main", "rext"})
                MCB_场景优化.Items.AddRange(New List(Of String) From {"", "hq", "uhq", "ll", "ull", "lossless"})
                MCB_像素格式.Items.AddRange(Split("yuv420p nv12 p010le yuv444p p016le nv16 p210le p216le yuv444p16le bgr0 bgra rgb0 rgba x2rgb10le x2bgr10le gbrp gbrp16le cuda d3d11", " "))
                MCB_编码预设.Text = "p7"
            Case "hevc_qsv"
                MCB_编码预设.Items.AddRange(New List(Of String) From {"", "veryslow", "slower", "slow", "medium", "fast", "faster", "veryfast"})
                MCB_配置文件.Items.AddRange(New List(Of String) From {"", "main", "mainsp", "rext", "scc"})
                MCB_场景优化.Items.AddRange(New List(Of String) From {})
                MCB_像素格式.Items.AddRange(Split("nv12 p010le p012le yuyv422 y210le qsv bgra x2rgb10le vuyx xv30le", " "))
                MCB_编码预设.Text = "veryslow"
            Case "hevc_amf"
                MCB_编码预设.Items.AddRange(New List(Of String) From {"", "quality", "balanced", "speed"})
                MCB_配置文件.Items.AddRange(New List(Of String) From {"", "main"})
                MCB_场景优化.Items.AddRange(New List(Of String) From {"", "transcoding", "ultralowlatency", "lowlatency", "webcam", "high_quality", "lowlatency_high_quality"})
                MCB_像素格式.Items.AddRange(Split("nv12 yuv420p d3d11 dxva2_vld p010le amf bgr0 rgb0 bgra argb rgba x2bgr10le rgbaf16le", " "))
                MCB_编码预设.Text = "quality"
            Case "hevc_d3d12va"
                MCB_编码预设.Items.AddRange(New List(Of String) From {})
                MCB_配置文件.Items.AddRange(New List(Of String) From {"", "main", "main10"})
                MCB_场景优化.Items.AddRange(New List(Of String) From {})
                MCB_像素格式.Items.AddRange(Split("d3d12", " "))
            Case "hevc_vulkan"
                MCB_编码预设.Items.AddRange(New List(Of String) From {})
                MCB_配置文件.Items.AddRange(New List(Of String) From {"", "main", "main10", "rext"})
                MCB_场景优化.Items.AddRange(New List(Of String) From {"", "hq", "ll", "ull", "lossless"})
                MCB_像素格式.Items.AddRange(Split("vulkan", " "))
            Case "libx264"
                MCB_编码预设.Items.AddRange(New List(Of String) From {"", "veryslow", "slower", "slow", "medium", "fast", "faster", "veryfast", "superfast", "ultrafast"})
                MCB_配置文件.Items.AddRange(New List(Of String) From {"", "baseline", "main", "high", "high10", "high422", "high444"})
                MCB_场景优化.Items.AddRange(New List(Of String) From {"", "film", "animation", "grain", "stillimage", "psnr", "ssim", "fastdecode", "zerolatency"})
                MCB_像素格式.Items.AddRange(Split("yuv420p yuvj420p yuv422p yuvj422p yuv444p yuvj444p nv12 nv16 nv21 yuv420p10le yuv422p10le yuv444p10le nv20le gray gray10le", " "))
                MCB_编码预设.Text = "slower"
            Case "h264_nvenc"
                MCB_编码预设.Items.AddRange(New List(Of String) From {"", "p7", "p6", "p5", "p4", "p3", "p2", "p1"})
                MCB_配置文件.Items.AddRange(New List(Of String) From {"", "baseline", "main", "high", "high10", "high422", "high444p"})
                MCB_场景优化.Items.AddRange(New List(Of String) From {"", "hq", "ll", "ull", "lossless"})
                MCB_像素格式.Items.AddRange(Split("yuv420p nv12 p010le yuv444p p016le nv16 p210le p216le yuv444p16le bgr0 bgra rgb0 rgba x2rgb10le x2bgr10le gbrp gbrp16le cuda d3d11", " "))
                MCB_编码预设.Text = "p7"
            Case "h264_qsv"
                MCB_编码预设.Items.AddRange(New List(Of String) From {"", "veryslow", "slower", "slow", "medium", "fast", "faster", "veryfast"})
                MCB_配置文件.Items.AddRange(New List(Of String) From {"", "baseline", "main", "high"})
                MCB_场景优化.Items.AddRange(New List(Of String) From {})
                MCB_像素格式.Items.AddRange(Split("nv12 qsv", " "))
                MCB_编码预设.Text = "veryslow"
            Case "h264_amf"
                MCB_编码预设.Items.AddRange(New List(Of String) From {"", "quality", "balanced", "speed"})
                MCB_配置文件.Items.AddRange(New List(Of String) From {"", "main", "high", "constrained_baseline", "constrained_high"})
                MCB_场景优化.Items.AddRange(New List(Of String) From {"", "transcoding", "ultralowlatency", "lowlatency", "webcam", "high_quality", "lowlatency_high_quality"})
                MCB_像素格式.Items.AddRange(Split("nv12 yuv420p d3d11 dxva2_vld p010le amf bgr0 rgb0 bgra argb rgba x2bgr10le rgbaf16le", " "))
                MCB_编码预设.Text = "quality"
            Case "h264_vulkan"
                MCB_编码预设.Items.AddRange(New List(Of String) From {})
                MCB_配置文件.Items.AddRange(New List(Of String) From {"", "main", "main10", "rext", "constrained_baseline"})
                MCB_场景优化.Items.AddRange(New List(Of String) From {"", "hq", "ll", "ull", "lossless"})
                MCB_像素格式.Items.AddRange(Split("vulkan", " "))
            Case "prores_ks"
                MCB_编码预设.Items.AddRange(New List(Of String) From {})
                MCB_配置文件.Items.AddRange(New List(Of String) From {"", "auto", "proxy", "lt", "standard", "hq", "4444", "4444xq"})
                MCB_场景优化.Items.AddRange(New List(Of String) From {})
                MCB_像素格式.Items.AddRange(Split("yuv422p10le yuv444p10le yuva444p10le", " "))
            Case "prores_aw"
                MCB_编码预设.Items.AddRange(New List(Of String) From {})
                MCB_配置文件.Items.AddRange(New List(Of String) From {"", "auto", "proxy", "lt", "standard", "hq", "4444", "4444xq"})
                MCB_场景优化.Items.AddRange(New List(Of String) From {})
                MCB_像素格式.Items.AddRange(Split("yuv422p10le yuv444p10le yuva444p10le", " "))
            Case "libxeve"
                MCB_编码预设.Items.AddRange(New List(Of String) From {"", "default", "slow", "medium", "fast"})
                MCB_配置文件.Items.AddRange(New List(Of String) From {"", "main", "baseline"})
                MCB_场景优化.Items.AddRange(New List(Of String) From {"", "psnr", "zerolatency", "none"})
                MCB_像素格式.Items.AddRange(Split("yuv420p yuv420p10le", " "))
            Case "libvpx-vp9"
                MCB_编码预设.Items.AddRange(New List(Of String) From {"", "0", "1", "2", "3", "4", "5"})
                MCB_配置文件.Items.AddRange(New List(Of String) From {})
                MCB_场景优化.Items.AddRange(New List(Of String) From {"", "psnr", "ssim"})
                MCB_像素格式.Items.AddRange(Split("yuv420p yuva420p yuv422p yuv440p yuv444p yuv420p10le yuv422p10le yuv440p10le yuv444p10le yuv420p12le yuv422p12le yuv440p12le yuv444p12le gbrp gbrp10le gbrp12le", " "))
            Case "libsvt_vp9"
                MCB_编码预设.Items.AddRange(New List(Of String) From {"", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9"})
                MCB_配置文件.Items.AddRange(New List(Of String) From {})
                MCB_场景优化.Items.AddRange(New List(Of String) From {"", "vq", "ssim", "vmaf"})
                MCB_像素格式.Items.AddRange(Split("yuv420p", " "))
            Case "vp9_qsv"
                MCB_编码预设.Items.AddRange(New List(Of String) From {"", "veryslow", "slower", "slow", "medium", "fast", "faster", "veryfast"})
                MCB_配置文件.Items.AddRange(New List(Of String) From {"", "profile0", "profile1", "profile2", "profile3"})
                MCB_场景优化.Items.AddRange(New List(Of String) From {})
                MCB_像素格式.Items.AddRange(Split("nv12 p010le vuyx qsv xv30le", " "))
        End Select
    End Sub

    Private Sub MCB_编码预设_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_编码预设.SelectedIndexChanged

    End Sub

    Private Sub MCB_配置文件_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_配置文件.SelectedIndexChanged

    End Sub

    Private Sub MCB_场景优化_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_场景优化.SelectedIndexChanged

    End Sub

End Class