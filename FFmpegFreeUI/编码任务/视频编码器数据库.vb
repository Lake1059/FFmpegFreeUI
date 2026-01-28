Public Class 视频编码器数据库
    Public Shared Property 字典 As New Dictionary(Of String, 视频编码器数据单片结构)

    Public Class 视频编码器数据单片结构
        Public Property Preset As New List(Of String)
        Public Property Profile As New List(Of String)
        Public Property Tune As New List(Of String)
        Public Property Pix_fmt As New List(Of String)
    End Class


    Public Shared Sub 初始化()
        字典.Add("copy", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {""}
})
        字典.Add("a64multi", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"gray"}
})
        字典.Add("a64multi5", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"gray"}
})
        字典.Add("alias_pix", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"bgr24", "gray"}
})
        字典.Add("amv", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"yuvj420p"}
})
        字典.Add("apng", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"rgb24", "rgba", "rgb48be", "rgba64be", "pal8", "gray", "ya8", "gray16be", "ya16be"}
})
        字典.Add("liboapv", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {"default", "placebo", "slow", "medium", "fast", "fastest"},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"gray10le", "yuv422p10le", "yuv422p12le", "yuv444p10le", "yuv444p12le", "yuva444p10le", "yuva444p12le"}
})
        字典.Add("asv1", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"yuv420p"}
})
        字典.Add("asv2", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"yuv420p"}
})
        字典.Add("libaom-av1", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {"0", "1", "2", "3", "4", "5", "6", "7", "8"},
.Profile = New List(Of String) From {"0", "1", "2"},
.Tune = New List(Of String) From {"psnr", "ssim"},
.Pix_fmt = New List(Of String) From {"yuv420p", "yuv422p", "yuv444p", "gbrp", "yuv420p10le", "yuv422p10le", "yuv444p10le", "yuv420p12le", "yuv422p12le", "yuv444p12le", "gbrp10le", "gbrp12le", "gray", "gray10le", "gray12le"}
})
        字典.Add("librav1e", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {},
.Profile = New List(Of String) From {},
.Tune = New List(Of String) From {},
.Pix_fmt = New List(Of String) From {"yuv420p", "yuvj420p", "yuv420p10le", "yuv420p12le", "yuv422p", "yuvj422p", "yuv422p10le", "yuv422p12le", "yuv444p", "yuvj444p", "yuv444p10le", "yuv444p12le"}
})
        字典.Add("libsvtav1", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13"},
.Profile = New List(Of String) From {"high", "main", "professional"},
.Tune = New List(Of String) From {},
.Pix_fmt = New List(Of String) From {"yuv420p", "yuv420p10le"}
})
        字典.Add("av1_nvenc", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {"p7", "p6", "p5", "p4", "p3", "p2", "p1", "fast", "medium", "slow", "default"},
.Profile = New List(Of String) From {"high", "main", "professional"},
.Tune = New List(Of String) From {"hq", "ll", "lossless", "uhq", "ull"},
.Pix_fmt = New List(Of String) From {"yuv420p", "nv12", "p010le", "yuv444p", "p016le", "nv16", "p210le", "p216le", "yuv444p10msble", "yuv444p16le", "bgr0", "bgra", "rgb0", "rgba", "x2rgb10le", "x2bgr10le", "gbrp", "gbrp10msble", "gbrp16le", "cuda", "d3d11"}
})
        字典.Add("av1_qsv", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {"veryslow", "slower", "slow", "medium", "fast", "faster", "veryfast"},
.Profile = New List(Of String) From {"main", "unknown"},
.Tune = New List(Of String) From {},
.Pix_fmt = New List(Of String) From {"nv12", "p010le", "qsv"}
})
        字典.Add("av1_amf", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {"speed", "balanced", "quality", "high_quality"},
.Profile = New List(Of String) From {"main"},
.Tune = New List(Of String) From {},
.Pix_fmt = New List(Of String) From {"nv12", "yuv420p", "d3d11", "dxva2_vld", "p010le", "amf", "bgr0", "rgb0", "bgra", "argb", "rgba", "x2bgr10le", "rgbaf16le"}
})
        字典.Add("av1_mf", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"nv12", "yuv420p", "d3d11"}
})
        字典.Add("av1_vaapi", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {"high", "main", "professional"},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"vaapi"}
})
        字典.Add("av1_vulkan", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {},
.Profile = New List(Of String) From {"high", "main", "professional"},
.Tune = New List(Of String) From {"default", "hq", "ll", "lossless", "ull"},
.Pix_fmt = New List(Of String) From {"vulkan"}
})
        字典.Add("avrp", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"gbrp10le"}
})
        字典.Add("libxavs2", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"yuv420p"}
})
        字典.Add("avui", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"uyvy422"}
})
        字典.Add("bitpacked", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"yuv422p10le"}
})
        字典.Add("bmp", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"bgra", "bgr24", "rgb565le", "rgb555le", "rgb444le", "rgb8", "bgr8", "rgb4_byte", "bgr4_byte", "gray", "pal8", "monob"}
})
        字典.Add("cfhd", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"yuv422p10le", "gbrp12le", "gbrap12le"}
})
        字典.Add("cinepak", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"rgb24", "gray"}
})
        字典.Add("cljr", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"yuv411p"}
})
        字典.Add("vc2", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"yuv420p", "yuv422p", "yuv444p", "yuv420p10le", "yuv422p10le", "yuv444p10le", "yuv420p12le", "yuv422p12le", "yuv444p12le"}
})
        字典.Add("dnxhd", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {"dnxhd", "dnxhr_444", "dnxhr_hq", "dnxhr_hqx", "dnxhr_lb", "dnxhr_sq"},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"yuv422p", "yuv422p10le", "yuv444p10le", "gbrp10le"}
})
        字典.Add("dpx", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"gray", "rgb24", "rgba", "abgr", "gray16le", "gray16be", "rgb48le", "rgb48be", "rgba64le", "rgba64be", "gbrp10le", "gbrp10be", "gbrp12le", "gbrp12be"}
})
        字典.Add("dvvideo", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"yuv411p", "yuv422p", "yuv420p"}
})
        字典.Add("dxv", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"rgba"}
})
        字典.Add("exr", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"grayf32le", "gbrpf32le", "gbrapf32le"}
})
        字典.Add("ffv1", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"yuv420p", "yuva420p", "yuva422p", "yuv444p", "yuva444p", "yuv440p", "yuv422p", "yuv411p", "yuv410p", "bgr0", "bgra", "yuv420p16le", "yuv422p16le", "yuv444p16le", "yuv444p9le", "yuv422p9le", "yuv420p9le", "yuv420p10le", "yuv422p10le", "yuv444p10le", "yuv420p12le", "yuv422p12le", "yuv444p12le", "yuva444p16le", "yuva422p16le", "yuva420p16le", "yuva444p12le", "yuva422p12le", "yuva444p10le", "yuva422p10le", "yuva420p10le", "yuva444p9le", "yuva422p9le", "yuva420p9le", "gray16le", "gray", "gbrp9le", "gbrp10le", "gbrp12le", "gbrp14le", "gbrap14le", "gbrap10le", "gbrap12le", "ya8", "gray10le", "gray12le", "gray14le", "gbrp16le", "rgb48le", "gbrap16le", "rgba64le", "gray9le", "yuv420p14le", "yuv422p14le", "yuv444p14le", "yuv440p10le", "yuv440p12le", "yaf16le", "grayf16le", "gbrpf16le", "gbrpf32le"}
})
        字典.Add("ffv1_vulkan", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"vulkan"}
})
        字典.Add("ffvhuff", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"yuv420p", "yuv422p", "yuv444p", "yuv411p", "yuv410p", "yuv440p", "gbrp", "gbrp9le", "gbrp10le", "gbrp12le", "gbrp14le", "gbrp16le", "gray", "gray16le", "yuva420p", "yuva422p", "yuva444p", "gbrap", "yuv420p9le", "yuv420p10le", "yuv420p12le", "yuv420p14le", "yuv420p16le", "yuv422p9le", "yuv422p10le", "yuv422p12le", "yuv422p14le", "yuv422p16le", "yuv444p9le", "yuv444p10le", "yuv444p12le", "yuv444p14le", "yuv444p16le", "yuva420p9le", "yuva420p10le", "yuva420p16le", "yuva422p9le", "yuva422p10le", "yuva422p16le", "yuva444p9le", "yuva444p10le", "yuva444p16le", "rgb24", "bgra"}
})
        字典.Add("fits", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"gbrap16be", "gbrp16be", "gbrp", "gbrap", "gray16be", "gray"}
})
        字典.Add("flashsv", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"bgr24"}
})
        字典.Add("flashsv2", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"bgr24"}
})
        字典.Add("flv", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"yuv420p"}
})
        字典.Add("gif", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"rgb8", "bgr8", "rgb4_byte", "bgr4_byte", "gray", "pal8"}
})
        字典.Add("h261", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"yuv420p"}
})
        字典.Add("h263", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"yuv420p"}
})
        字典.Add("h263p", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"yuv420p"}
})
        字典.Add("libx264", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {"ultrafast", "superfast", "veryfast", "faster", "fast", "medium", "slow", "slower", "veryslow"},
.Profile = New List(Of String) From {"baseline", "high", "high10", "high422", "high444", "main"},
.Tune = New List(Of String) From {"animation", "fastdecode", "film", "grain", "psnr", "ssim", "stillimage", "zerolatency"},
.Pix_fmt = New List(Of String) From {"yuv420p", "yuvj420p", "yuv422p", "yuvj422p", "yuv444p", "yuvj444p", "nv12", "nv16", "nv21", "yuv420p10le", "yuv422p10le", "yuv444p10le", "nv20le", "gray", "gray10le"}
})
        字典.Add("libx264rgb", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"bgr0", "bgr24", "rgb24"}
})
        字典.Add("libopenh264", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {"constrained_baseline", "high", "main"},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"yuv420p", "yuvj420p"}
})
        字典.Add("h264_amf", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {"quality", "speed", "balanced"},
.Profile = New List(Of String) From {"constrained_baseline", "constrained_high", "high", "main"},
.Tune = New List(Of String) From {"high_quality", "lowlatency", "lowlatency_high_quality", "transcoding", "ultralowlatency", "webcam"},
.Pix_fmt = New List(Of String) From {"nv12", "yuv420p", "d3d11", "dxva2_vld", "p010le", "amf", "bgr0", "rgb0", "bgra", "argb", "rgba", "x2bgr10le", "rgbaf16le"}
})
        字典.Add("h264_mf", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"nv12", "yuv420p", "d3d11"}
})
        字典.Add("h264_nvenc", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {"p7", "p6", "p5", "p4", "p3", "p2", "p1", "losslesshp", "lossless", "llhp", "llhq", "ll", "bd", "hq", "hp", "fast", "medium", "slow", "default"},
.Profile = New List(Of String) From {"baseline", "high", "high10", "high422", "high444p", "main"},
.Tune = New List(Of String) From {"hq", "ll", "lossless", "ull"},
.Pix_fmt = New List(Of String) From {"yuv420p", "nv12", "p010le", "yuv444p", "p016le", "nv16", "p210le", "p216le", "yuv444p10msble", "yuv444p16le", "bgr0", "bgra", "rgb0", "rgba", "x2rgb10le", "x2bgr10le", "gbrp", "gbrp10msble", "gbrp16le", "cuda", "d3d11"}
})
        字典.Add("h264_qsv", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {"veryslow", "slower", "slow", "medium", "fast", "faster", "veryfast"},
.Profile = New List(Of String) From {"baseline", "high", "main", "unknown"},
.Tune = New List(Of String) From {},
.Pix_fmt = New List(Of String) From {"nv12", "qsv"}
})
        字典.Add("h264_vaapi", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {"constrained_baseline", "high", "high10", "main"},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"vaapi"}
})
        字典.Add("h264_vulkan", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {},
.Profile = New List(Of String) From {"constrained_baseline", "high", "high444p", "main"},
.Tune = New List(Of String) From {"default", "hq", "ll", "lossless", "ull"},
.Pix_fmt = New List(Of String) From {"vulkan"}
})
        字典.Add("hap", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"rgba"}
})
        字典.Add("hdr", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"gbrpf32le"}
})
        字典.Add("libx265", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {"ultrafast", "superfast", "veryfast", "faster", "fast", "medium", "slow", "slower", "veryslow"},
.Profile = New List(Of String) From {"main", "mainstillpicture"},
.Tune = New List(Of String) From {"animation", "fastdecode", "grain", "psnr", "ssim", "stillimage", "zerolatency"},
.Pix_fmt = New List(Of String) From {"yuv420p", "yuvj420p", "yuv422p", "yuvj422p", "yuv444p", "yuvj444p", "gbrp", "yuv420p10le", "yuv422p10le", "yuv444p10le", "gbrp10le", "yuv420p12le", "yuv422p12le", "yuv444p12le", "gbrp12le", "gray", "gray10le", "gray12le", "yuva420p", "yuva420p10le"}
})
        字典.Add("hevc_amf", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {"speed", "balanced", "quality"},
.Profile = New List(Of String) From {"main", "main10"},
.Tune = New List(Of String) From {"high_quality", "lowlatency", "lowlatency_high_quality", "transcoding", "ultralowlatency", "webcam"},
.Pix_fmt = New List(Of String) From {"nv12", "yuv420p", "d3d11", "dxva2_vld", "p010le", "amf", "bgr0", "rgb0", "bgra", "argb", "rgba", "x2bgr10le", "rgbaf16le"}
})
        字典.Add("hevc_d3d12va", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {},
.Profile = New List(Of String) From {"main", "main10"},
.Tune = New List(Of String) From {},
.Pix_fmt = New List(Of String) From {"d3d12"}
})
        字典.Add("hevc_mf", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"nv12", "yuv420p", "d3d11"}
})
        字典.Add("hevc_nvenc", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {"p7", "p6", "p5", "p4", "p3", "p2", "p1", "losslesshp", "lossless", "llhp", "llhq", "ll", "bd", "hq", "hp", "fast", "medium", "slow", "default"},
.Profile = New List(Of String) From {"main", "main10", "mv", "rext"},
.Tune = New List(Of String) From {"hq", "ll", "lossless", "uhq", "ull"},
.Pix_fmt = New List(Of String) From {"yuv420p", "nv12", "p010le", "yuv444p", "p016le", "nv16", "p210le", "p216le", "yuv444p10msble", "yuv444p16le", "bgr0", "bgra", "rgb0", "rgba", "x2rgb10le", "x2bgr10le", "gbrp", "gbrp10msble", "gbrp16le", "cuda", "d3d11"}
})
        字典.Add("hevc_qsv", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {"veryslow", "slower", "slow", "medium", "fast", "faster", "veryfast"},
.Profile = New List(Of String) From {"main", "main10", "mainsp", "rext", "scc", "unknown"},
.Tune = New List(Of String) From {},
.Pix_fmt = New List(Of String) From {"nv12", "p010le", "p012le", "yuyv422", "y210le", "qsv", "bgra", "x2rgb10le", "vuyx", "xv30le"}
})
        字典.Add("hevc_vaapi", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {"main", "main10", "rext"},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"vaapi"}
})
        字典.Add("hevc_vulkan", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {},
.Profile = New List(Of String) From {"main", "main10", "rext"},
.Tune = New List(Of String) From {"default", "hq", "ll", "lossless", "ull"},
.Pix_fmt = New List(Of String) From {"vulkan"}
})
        字典.Add("libkvazaar", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"yuv420p"}
})
        字典.Add("huffyuv", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"yuv422p", "rgb24", "bgra"}
})
        字典.Add("jpeg2000", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"rgb24", "rgb48le", "gbrp", "gbrp9le", "gbrp10le", "gbrp12le", "gbrp14le", "gbrp16le", "gray", "gray9le", "gray10le", "gray12le", "gray14le", "gray16le", "yuv420p", "yuv420p9le", "yuv420p10le", "yuv420p12le", "yuv420p14le", "yuv420p16le", "yuv422p", "yuv422p9le", "yuv422p10le", "yuv422p12le", "yuv422p14le", "yuv422p16le", "yuv444p", "yuv444p9le", "yuv444p10le", "yuv444p12le", "yuv444p14le", "yuv444p16le", "yuv440p", "yuv440p10le", "yuv440p12le", "yuv411p", "yuv410p", "ya8", "ya16le", "rgba", "rgba64le", "gbrap", "gbrap10le", "gbrap12le", "gbrap16le", "yuva420p", "yuva420p9le", "yuva420p10le", "yuva420p16le", "yuva422p", "yuva422p9le", "yuva422p10le", "yuva422p16le", "yuva444p", "yuva444p9le", "yuva444p10le", "yuva444p16le", "pal8"}
})
        字典.Add("libopenjpeg", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {"cinema2k", "cinema4k", "jpeg2000"},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"rgb24", "rgba", "rgb48le", "rgba64le", "gbrp", "gbrp9le", "gbrp10le", "gbrp12le", "gbrp14le", "gbrp16le", "gray", "ya8", "gray16le", "ya16le", "gray10le", "gray12le", "gray14le", "yuv420p", "yuv422p", "yuva420p", "yuv440p", "yuv444p", "yuva422p", "yuv411p", "yuv410p", "yuva444p", "yuv420p9le", "yuv422p9le", "yuv444p9le", "yuva420p9le", "yuva422p9le", "yuva444p9le", "yuv420p10le", "yuv422p10le", "yuv444p10le", "yuva420p10le", "yuva422p10le", "yuva444p10le", "yuv420p12le", "yuv422p12le", "yuv444p12le", "yuv420p14le", "yuv422p14le", "yuv444p14le", "yuv420p16le", "yuv422p16le", "yuv444p16le", "yuva420p16le", "yuva422p16le", "yuva444p16le", "xyz12le"}
})
        字典.Add("jpegls", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"bgr24", "rgb24", "gray", "gray16le"}
})
        字典.Add("libjxl", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"rgb24", "rgba", "rgb48le", "rgba64le", "rgbf32le", "rgbaf32le", "gray", "ya8", "gray16le", "ya16le", "grayf32le"}
})
        字典.Add("libjxl_anim", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"rgb24", "rgba", "rgb48le", "rgba64le", "rgbf32le", "rgbaf32le", "gray", "ya8", "gray16le", "ya16le", "grayf32le"}
})
        字典.Add("ljpeg", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"bgr24", "bgra", "bgr0", "yuvj420p", "yuvj444p", "yuvj422p", "yuv420p", "yuv444p", "yuv422p"}
})
        字典.Add("magicyuv", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"gbrp", "gbrap", "yuv422p", "yuv420p", "yuv444p", "yuva444p", "gray"}
})
        字典.Add("mjpeg", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"yuvj420p", "yuvj422p", "yuvj444p", "yuv420p", "yuv422p", "yuv444p"}
})
        字典.Add("mjpeg_qsv", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"nv12", "yuyv422", "bgra", "qsv"}
})
        字典.Add("mjpeg_vaapi", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"vaapi"}
})
        字典.Add("mpeg1video", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"yuv420p"}
})
        字典.Add("mpeg2video", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"yuv420p", "yuv422p"}
})
        字典.Add("mpeg2_qsv", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {"veryslow", "slower", "slow", "medium", "fast", "faster", "veryfast"},
.Profile = New List(Of String) From {"high", "main", "simple", "unknown"},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"nv12", "qsv"}
})
        字典.Add("mpeg2_vaapi", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {"main", "simple"},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"vaapi"}
})
        字典.Add("mpeg4", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"yuv420p"}
})
        字典.Add("libxvid", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"yuv420p"}
})
        字典.Add("msmpeg4v2", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"yuv420p"}
})
        字典.Add("msmpeg4", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"yuv420p"}
})
        字典.Add("msrle", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"pal8"}
})
        字典.Add("msvideo1", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"rgb555le"}
})
        字典.Add("pam", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"rgb24", "rgba", "rgb48be", "rgba64be", "gray", "ya8", "gray16be", "ya16be", "monob"}
})
        字典.Add("pbm", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"monow"}
})
        字典.Add("pcx", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"rgb24", "rgb8", "bgr8", "rgb4_byte", "bgr4_byte", "gray", "pal8", "monob"}
})
        字典.Add("pfm", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"gbrpf32le", "grayf32le", "gbrpf32be", "grayf32be"}
})
        字典.Add("pgm", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"gray", "gray16be"}
})
        字典.Add("pgmyuv", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"yuv420p", "yuv420p16be"}
})
        字典.Add("phm", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"gbrpf32le", "grayf32le"}
})
        字典.Add("png", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"rgb24", "rgba", "rgb48be", "rgba64be", "pal8", "gray", "ya8", "gray16be", "ya16be", "monob"}
})
        字典.Add("ppm", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"rgb24", "rgb48be"}
})
        字典.Add("prores", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"yuv422p10le", "yuv444p10le", "yuva444p10le"}
})
        字典.Add("prores_aw", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {},
.Profile = New List(Of String) From {"4444", "4444xq", "auto", "hq", "lt", "proxy", "standard"},
.Tune = New List(Of String) From {},
.Pix_fmt = New List(Of String) From {"yuv422p10le", "yuv444p10le", "yuva444p10le"}
})
        字典.Add("prores_ks", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {},
.Profile = New List(Of String) From {"4444", "4444xq", "auto", "hq", "lt", "proxy", "standard"},
.Tune = New List(Of String) From {},
.Pix_fmt = New List(Of String) From {"yuv422p10le", "yuv444p10le", "yuva444p10le"}
})
        字典.Add("qoi", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"rgba", "rgb24"}
})
        字典.Add("qtrle", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"rgb24", "rgb555be", "argb", "gray"}
})
        字典.Add("r10k", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"gbrp10le"}
})
        字典.Add("r210", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"gbrp10le"}
})
        字典.Add("rawvideo", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {""}
})
        字典.Add("roqvideo", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"yuvj444p"}
})
        字典.Add("rpza", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"rgb555le"}
})
        字典.Add("rv10", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"yuv420p"}
})
        字典.Add("rv20", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"yuv420p"}
})
        字典.Add("sgi", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"rgb24", "rgba", "rgb48le", "rgb48be", "rgba64le", "rgba64be", "gray16le", "gray16be", "gray"}
})
        字典.Add("smc", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"pal8"}
})
        字典.Add("snow", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"yuv420p", "yuv410p", "yuv444p", "gray"}
})
        字典.Add("speedhq", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"yuv420p", "yuv422p", "yuv444p"}
})
        字典.Add("sunrast", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"bgr24", "pal8", "gray", "monow"}
})
        字典.Add("svq1", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"yuv410p"}
})
        字典.Add("targa", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"bgr24", "bgra", "rgb555le", "gray", "pal8"}
})
        字典.Add("libtheora", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"yuv420p", "yuv422p", "yuv444p"}
})
        字典.Add("tiff", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"rgb24", "rgb48le", "pal8", "rgba", "rgba64le", "gray", "ya8", "gray16le", "ya16le", "monob", "monow", "yuv420p", "yuv422p", "yuv440p", "yuv444p", "yuv410p", "yuv411p"}
})
        字典.Add("utvideo", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"gbrp", "gbrap", "yuv422p", "yuv420p", "yuv444p"}
})
        字典.Add("v210", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"yuv422p10le", "yuv422p"}
})
        字典.Add("v308", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"yuv444p"}
})
        字典.Add("v408", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"yuva444p"}
})
        字典.Add("v410", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"yuv444p10le"}
})
        字典.Add("vbn", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"rgba", "rgb24"}
})
        字典.Add("vnull", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {""}
})
        字典.Add("libvpx", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {"psnr", "ssim"},
.Pix_fmt = New List(Of String) From {"yuv420p", "yuva420p"}
})
        字典.Add("vp8_vaapi", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"vaapi"}
})
        字典.Add("libvpx-vp9", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {"0", "1", "2", "3", "4", "5"},
.Profile = New List(Of String) From {},
.Tune = New List(Of String) From {"psnr", "ssim"},
.Pix_fmt = New List(Of String) From {"yuv420p", "yuva420p", "yuv422p", "yuv440p", "yuv444p", "yuv420p10le", "yuv422p10le", "yuv440p10le", "yuv444p10le", "yuv420p12le", "yuv422p12le", "yuv440p12le", "yuv444p12le", "gbrp", "gbrp10le", "gbrp12le"}
})
        字典.Add("vp9_vaapi", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"vaapi"}
})
        字典.Add("vp9_qsv", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {"veryslow", "slower", "slow", "medium", "fast", "faster", "veryfast"},
.Profile = New List(Of String) From {"profile0", "profile1", "profile2", "profile3", "unknown"},
.Tune = New List(Of String) From {},
.Pix_fmt = New List(Of String) From {"nv12", "p010le", "vuyx", "qsv", "xv30le"}
})
        字典.Add("libvvenc", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {"slower", "slow", "medium", "fast", "faster"},
.Profile = New List(Of String) From {"main", "main10"},
.Tune = New List(Of String) From {},
.Pix_fmt = New List(Of String) From {"yuv420p10le"}
})
        字典.Add("wbmp", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"monob"}
})
        字典.Add("libwebp_anim", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {"text", "icon", "drawing", "photo", "picture", "default", "none"},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"bgra", "yuv420p", "yuva420p"}
})
        字典.Add("libwebp", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {"text", "icon", "drawing", "photo", "picture", "default", "none"},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"bgra", "yuv420p", "yuva420p"}
})
        字典.Add("wmv1", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"yuv420p"}
})
        字典.Add("wmv2", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"yuv420p"}
})
        字典.Add("wrapped_avframe", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {""}
})
        字典.Add("xbm", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"monow"}
})
        字典.Add("xface", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"monow"}
})
        字典.Add("xwd", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"bgra", "rgba", "argb", "abgr", "rgb24", "bgr24", "rgb565be", "rgb565le", "bgr565be", "bgr565le", "rgb555be", "rgb555le", "bgr555be", "bgr555le", "rgb8", "bgr8", "rgb4_byte", "bgr4_byte", "pal8", "gray", "monow"}
})
        字典.Add("y41p", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"yuv411p"}
})
        字典.Add("yuv4", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"yuv420p"}
})
        字典.Add("zlib", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"bgr24"}
})
        字典.Add("zmbv", New 视频编码器数据单片结构 With {
.Preset = New List(Of String) From {""},
.Profile = New List(Of String) From {""},
.Tune = New List(Of String) From {""},
.Pix_fmt = New List(Of String) From {"pal8", "rgb555le", "rgb565le", "bgr0"}
})
    End Sub

End Class
