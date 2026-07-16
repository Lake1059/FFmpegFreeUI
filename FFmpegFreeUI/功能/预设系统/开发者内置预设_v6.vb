Imports System.Text.Json

Public Class 开发者内置预设_v6

    Public Class 预设项
        Public Sub New(名称 As String, 数据 As 预设数据_v6)
            Me.名称 = If(名称, "").Trim()
            Me.数据 = If(数据, New 预设数据_v6)
            预设管理_v6.初始化空集合(Me.数据)
        End Sub

        Public Property 名称 As String
        Public Property 数据 As 预设数据_v6
    End Class

    <CodeAnalysis.SuppressMessage("Style", "IDE0017:简化对象初始化", Justification:="<挂起>")>
    <CodeAnalysis.SuppressMessage("Performance", "CA1861:不要将常量数组作为参数", Justification:="<挂起>")>
    Public Shared Function 获取全部() As List(Of 预设项)
        Dim result As New List(Of 预设项)

        Dim RTX50_AV1 As New 预设数据_v6
        RTX50_AV1.预设备注 = "老黄 AV1 常规模式标准答案，参考 VMAF = 95~96，最推荐 RTX50 全系使用。此预设仅包含视频参数！"
        RTX50_AV1.解码参数_解码器 = "cuda"
        RTX50_AV1.视频参数_编码器_类型 = 预设数据_v6.视频编码器类型.视频
        RTX50_AV1.视频参数_编码器_分类名称 = "AV1"
        RTX50_AV1.视频参数_编码器_具体编码 = "av1_nvenc"
        RTX50_AV1.视频参数_编码器_编码预设 = "p7"
        RTX50_AV1.视频参数_比特率_控制方式 = 预设数据_v6.视频全局质量控制方式.VBR
        RTX50_AV1.视频参数_质量控制_参数名 = "cq"
        RTX50_AV1.视频参数_质量控制_值 = "36"
        RTX50_AV1.视频参数_色彩管理_像素格式 = "p010le"
        result.Add(New 预设项("RTX50 AV1", RTX50_AV1))

        Dim RTX50_AV1_UHQ As New 预设数据_v6
        RTX50_AV1_UHQ.预设备注 = "老黄 AV1 UHQ 模式推荐答案，极细微质量损失换来体积大幅降低，参考 VMAF = 94~95，最推荐 RTX50 全系使用。此预设仅包含视频参数！"
        RTX50_AV1_UHQ.解码参数_解码器 = "cuda"
        RTX50_AV1_UHQ.视频参数_编码器_类型 = 预设数据_v6.视频编码器类型.视频
        RTX50_AV1_UHQ.视频参数_编码器_分类名称 = "AV1"
        RTX50_AV1_UHQ.视频参数_编码器_具体编码 = "av1_nvenc"
        RTX50_AV1_UHQ.视频参数_编码器_编码预设 = "p7"
        RTX50_AV1_UHQ.视频参数_编码器_场景优化 = "uhq"
        RTX50_AV1_UHQ.视频参数_比特率_控制方式 = 预设数据_v6.视频全局质量控制方式.VBR
        RTX50_AV1_UHQ.视频参数_质量控制_参数名 = "cq"
        RTX50_AV1_UHQ.视频参数_质量控制_值 = "38"
        RTX50_AV1_UHQ.视频参数_色彩管理_像素格式 = "p010le"
        result.Add(New 预设项("RTX50 AV1 UHQ", RTX50_AV1_UHQ))

        Dim RTX50_AV1_UHQ_EX As New 预设数据_v6
        RTX50_AV1_UHQ_EX.预设备注 = "老黄 AV1 UHQ + 复杂度全开，提升极细微可能对稳定度有帮助但同时也可能不利于评测，参考 VMAF = 94~95，最推荐 RTX50 全系使用。此预设仅包含视频参数！"
        RTX50_AV1_UHQ_EX.解码参数_解码器 = "cuda"
        RTX50_AV1_UHQ_EX.视频参数_编码器_类型 = 预设数据_v6.视频编码器类型.视频
        RTX50_AV1_UHQ_EX.视频参数_编码器_分类名称 = "AV1"
        RTX50_AV1_UHQ_EX.视频参数_编码器_具体编码 = "av1_nvenc"
        RTX50_AV1_UHQ_EX.视频参数_编码器_编码预设 = "p7"
        RTX50_AV1_UHQ_EX.视频参数_编码器_场景优化 = "uhq"
        RTX50_AV1_UHQ_EX.视频参数_比特率_控制方式 = 预设数据_v6.视频全局质量控制方式.VBR
        RTX50_AV1_UHQ_EX.视频参数_质量控制_参数名 = "cq"
        RTX50_AV1_UHQ_EX.视频参数_质量控制_值 = "38"
        RTX50_AV1_UHQ_EX.视频参数_质量控制_进阶参数集 = "-multipass fullres -rc-lookahead 240 -spatial-aq 1 -temporal-aq 1 -aq-strength 15 -tf_level -1"
        RTX50_AV1_UHQ_EX.视频参数_色彩管理_像素格式 = "p010le"
        result.Add(New 预设项("RTX50 AV1 UHQ 超压", RTX50_AV1_UHQ_EX))

        Dim RTX50_HEVC_UHQ As New 预设数据_v6
        RTX50_HEVC_UHQ.预设备注 = "老黄 HEVC UHQ 满分答案，参考 VMAF = 95~96，最推荐 RTX50 全系使用，压缩度大概能摸到 x265 medium 水平。此预设仅包含视频参数！"
        RTX50_HEVC_UHQ.解码参数_解码器 = "cuda"
        RTX50_HEVC_UHQ.视频参数_编码器_类型 = 预设数据_v6.视频编码器类型.视频
        RTX50_HEVC_UHQ.视频参数_编码器_分类名称 = "H.265/HEVC"
        RTX50_HEVC_UHQ.视频参数_编码器_具体编码 = "hevc_nvenc"
        RTX50_HEVC_UHQ.视频参数_编码器_编码预设 = "p7"
        RTX50_HEVC_UHQ.视频参数_编码器_场景优化 = "uhq"
        RTX50_HEVC_UHQ.视频参数_比特率_控制方式 = 预设数据_v6.视频全局质量控制方式.VBR
        RTX50_HEVC_UHQ.视频参数_质量控制_参数名 = "cq"
        RTX50_HEVC_UHQ.视频参数_质量控制_值 = "28"
        RTX50_HEVC_UHQ.视频参数_色彩管理_像素格式 = "p010le"
        result.Add(New 预设项("RTX50 HEVC UHQ", RTX50_HEVC_UHQ))

        Dim SVTAV1HDR_CRF36 As New 预设数据_v6
        SVTAV1HDR_CRF36.预设备注 = "采用 HDR 分支的 libsvtav1，可能与常规的分支有些许差别。注意在编码高分辨率视频时会占用大量内存，4K 通常建议 32GB 及以上内存条。此预设仅包含视频参数！"
        SVTAV1HDR_CRF36.视频参数_编码器_类型 = 预设数据_v6.视频编码器类型.视频
        SVTAV1HDR_CRF36.视频参数_编码器_分类名称 = "AV1"
        SVTAV1HDR_CRF36.视频参数_编码器_具体编码 = "libsvtav1"
        SVTAV1HDR_CRF36.视频参数_编码器_编码预设 = "6"
        SVTAV1HDR_CRF36.视频参数_比特率_控制方式 = 预设数据_v6.视频全局质量控制方式.CRF
        SVTAV1HDR_CRF36.视频参数_质量控制_参数名 = "crf"
        SVTAV1HDR_CRF36.视频参数_质量控制_值 = "36"
        SVTAV1HDR_CRF36.视频参数_质量控制_进阶参数集 = "-svtav1-params tune=0:keyint=10s:enable-variance-boost=1:variance-boost-strength=1:film-grain=4:film-grain-denoise=1:sharpness=1:ac-bias=1:lp=4"
        SVTAV1HDR_CRF36.视频参数_色彩管理_像素格式 = "yuv420p10le"
        result.Add(New 预设项("SVT-AV1-HDR 适用平衡点", SVTAV1HDR_CRF36))

        Dim X265_Slow As New 预设数据_v6
        X265_Slow.预设备注 = "x265 默认推荐质量，选用 slow 预设可直接作为普通人的最终压制方案，参考 VMAF ≈ 95。此预设仅包含视频参数！"
        X265_Slow.视频参数_编码器_类型 = 预设数据_v6.视频编码器类型.视频
        X265_Slow.视频参数_编码器_分类名称 = "H.265/HEVC"
        X265_Slow.视频参数_编码器_具体编码 = "libx265"
        X265_Slow.视频参数_编码器_编码预设 = "slow"
        X265_Slow.视频参数_比特率_控制方式 = 预设数据_v6.视频全局质量控制方式.CRF
        X265_Slow.视频参数_质量控制_参数名 = "crf"
        X265_Slow.视频参数_质量控制_值 = "24"
        X265_Slow.视频参数_色彩管理_像素格式 = "yuv420p10le"
        result.Add(New 预设项("x265 默认推荐最终压制方案", X265_Slow))

        Dim X264_Slower As New 预设数据_v6
        X264_Slower.预设备注 = "x264 默认推荐质量，考虑到 264 较为过时，所以选用 slower 预设，参考 VMAF ≈ 95。此预设仅包含视频参数！"
        X264_Slower.视频参数_编码器_类型 = 预设数据_v6.视频编码器类型.视频
        X264_Slower.视频参数_编码器_分类名称 = "H.264/AVC"
        X264_Slower.视频参数_编码器_具体编码 = "libx264"
        X264_Slower.视频参数_编码器_编码预设 = "slower"
        X264_Slower.视频参数_比特率_控制方式 = 预设数据_v6.视频全局质量控制方式.CRF
        X264_Slower.视频参数_质量控制_参数名 = "crf"
        X264_Slower.视频参数_质量控制_值 = "23"
        X264_Slower.视频参数_色彩管理_像素格式 = "yuv420p10le"
        result.Add(New 预设项("x264 最终压制", X264_Slower))

        Dim M4A_HDAudio As New 预设数据_v6
        M4A_HDAudio.预设备注 = "在找全能音乐压制方案？别看了就这个！三个愿望一次满足：接近无损的质量 + 小体积 + 元数据支持（歌曲信息以及专辑图等）。代价是你需要去找个自编译带 FDK AAC 的 ffmpeg，而且好像只支持 16 位深，反正听歌是足够了。"
        M4A_HDAudio.输出容器 = ".m4a"
        M4A_HDAudio.视频参数_编码器_分类名称 = "复制流"
        M4A_HDAudio.视频参数_编码器_具体编码 = "copy"
        M4A_HDAudio.音频参数_编码器_代号 = "aac.fdk"
        M4A_HDAudio.音频参数_质量参数名 = "-vbr"
        M4A_HDAudio.音频参数_质量值 = "5"
        result.Add(New 预设项("FDK AAC 压制音频到 M4A", M4A_HDAudio))

        Dim AVIF_AOMAV1 As New 预设数据_v6
        AVIF_AOMAV1.预设备注 = "AVIF 是将 AV1 用于图片的超高压缩图，非常适合用它来压制你那巨量不再需要后期的图片，crf18 基本上能稳定卡 VMAF ≈ 96，如果实在追求质量可以自己改 crf12。其他几个 AV1 编码器也可以输出到 AVIF 图片，如有需要可以自行研究，这里只收录 AOM 预设，都压图片了也不差那点时间了。"
        AVIF_AOMAV1.输出容器 = ".avif"
        AVIF_AOMAV1.视频参数_编码器_分类名称 = "AV1"
        AVIF_AOMAV1.视频参数_编码器_具体编码 = "libaom-av1"
        AVIF_AOMAV1.视频参数_编码器_编码预设 = "1"
        AVIF_AOMAV1.视频参数_比特率_控制方式 = 预设数据_v6.视频全局质量控制方式.CRF
        AVIF_AOMAV1.视频参数_质量控制_参数名 = "crf"
        AVIF_AOMAV1.视频参数_质量控制_值 = "18"
        AVIF_AOMAV1.视频参数_质量控制_进阶参数集 = "-still-picture 1 -row-mt 1"
        result.Add(New 预设项("AVIF 高压缩图片 AOM AV1", AVIF_AOMAV1))

        Dim WindowsICO As New 预设数据_v6
        WindowsICO.预设备注 = "从一张图片生成符合 Windows 使用习惯的多尺寸 ICO，内含 16、24、32、48、64、128、256 七个尺寸。小尺寸使用 BMP + BGRA 保留透明通道，256 尺寸使用 PNG + RGBA 压缩体积；非正方形输入会等比缩放并透明补边。"
        WindowsICO.输出容器 = ".ico"
        WindowsICO.自定义参数_完全自己写 = String.Join(" ", New String() {
            "-hide_banner -y",
            "-i ""<InputFile>""",
            "-filter_complex ""[0:v]split=7[s16][s24][s32][s48][s64][s128][s256];[s16]scale=w=16:h=16:force_original_aspect_ratio=decrease:flags=lanczos,pad=16:16:(ow-iw)/2:(oh-ih)/2:color=black@0,format=bgra[v16];[s24]scale=w=24:h=24:force_original_aspect_ratio=decrease:flags=lanczos,pad=24:24:(ow-iw)/2:(oh-ih)/2:color=black@0,format=bgra[v24];[s32]scale=w=32:h=32:force_original_aspect_ratio=decrease:flags=lanczos,pad=32:32:(ow-iw)/2:(oh-ih)/2:color=black@0,format=bgra[v32];[s48]scale=w=48:h=48:force_original_aspect_ratio=decrease:flags=lanczos,pad=48:48:(ow-iw)/2:(oh-ih)/2:color=black@0,format=bgra[v48];[s64]scale=w=64:h=64:force_original_aspect_ratio=decrease:flags=lanczos,pad=64:64:(ow-iw)/2:(oh-ih)/2:color=black@0,format=bgra[v64];[s128]scale=w=128:h=128:force_original_aspect_ratio=decrease:flags=lanczos,pad=128:128:(ow-iw)/2:(oh-ih)/2:color=black@0,format=bgra[v128];[s256]scale=w=256:h=256:force_original_aspect_ratio=decrease:flags=lanczos,pad=256:256:(ow-iw)/2:(oh-ih)/2:color=black@0,format=rgba[v256]""",
            "-map ""[v16]"" -map ""[v24]"" -map ""[v32]"" -map ""[v48]"" -map ""[v64]"" -map ""[v128]"" -map ""[v256]""",
            "-frames:v 1",
            "-c:v:0 bmp -c:v:1 bmp -c:v:2 bmp -c:v:3 bmp -c:v:4 bmp -c:v:5 bmp -c:v:6 png",
            "-f ico",
            """<OutputFile>"""
        })
        result.Add(New 预设项("Windows 标准多尺寸 ICO 图标", WindowsICO))

        Dim HDR4000_To_HDR1000 As New 预设数据_v6
        HDR4000_To_HDR1000.预设备注 = $"电影 HDR 源专用的最低滤镜预设，用于类似《星际穿越》这类现代专业电影片源：PQ 传输、BT.2020 色域、limited range、母版峰值约 4000 nit。默认将 4000 nit 高光通过 linear-light tonemap 压到 1000 nit HDR10，并保留 BT.2020/PQ 输出链路。{vbCrLf & vbCrLf}自定义目标最大亮度时，只改滤镜排序中的两个 npl 和 tonemap 的 peak：目标 1000 nit 时 npl=1000、peak=4000/1000=4；目标 400 nit 时 npl=400、peak=4000/400=10；若源峰值不是 4000，则按 源峰值/目标峰值 重新计算 peak。{vbCrLf & vbCrLf}需要改成 HLG 时只改传输特性：HLG 输入把第一段 zscale 的 transferin=smpte2084 改为 transferin=arib-std-b67；HLG 输出把倒数第二段 zscale 的 transfer=smpte2084 改为 transfer=arib-std-b67；若输入输出都用 HLG，则两处都改，BT.2020、bt2020nc、limited range 通常保持不变。{vbCrLf & vbCrLf}此预设故意使用 zscale 明确声明输入/输出 transfer、matrix、primaries、range，并使用 gbrpf32le 作为 tonemap 的 32 位浮点线性 RGB 工作格式，以适配电影级 HDR10 片源。此预设只通过滤镜排序加入自定义视频滤镜，不设置编码器、码率、容器或 HDR 元数据；输出的 master-display / max-cll 请在其他参数中按目标亮度单独填写。"
        HDR4000_To_HDR1000.视频参数_色彩管理_矩阵系数 = "bt2020nc"
        HDR4000_To_HDR1000.视频参数_色彩管理_色域 = "bt2020"
        HDR4000_To_HDR1000.视频参数_色彩管理_传输特性 = "smpte2084"
        HDR4000_To_HDR1000.视频参数_色彩管理_范围 = "tv"
        HDR4000_To_HDR1000.视频参数_色彩管理_处理方式 = "仅写入元数据"
        HDR4000_To_HDR1000.滤镜排序系统 = New 预设数据_v6.滤镜排序单片结构() {
            New 预设数据_v6.滤镜排序单片结构 With {
                .显示名称 = "HDR 转线性 RGB",
                .是自定义滤镜 = True,
                .允许在排序页直接编辑 = True,
                .滤镜标识符 = 预设数据_v6.滤镜排序单片结构.标识符枚举.自定义视频滤镜,
                .滤镜目标流类型 = 预设数据_v6.滤镜排序单片结构.流类型.视频,
                .自定义滤镜内容 = "zscale=transferin=smpte2084:matrixin=bt2020nc:primariesin=bt2020:rangein=tv:transfer=linear:matrix=gbr:primaries=bt2020:npl=1000"
            },
            New 预设数据_v6.滤镜排序单片结构 With {
                .显示名称 = "转 32 位浮点 RGB",
                .是自定义滤镜 = True,
                .允许在排序页直接编辑 = True,
                .滤镜标识符 = 预设数据_v6.滤镜排序单片结构.标识符枚举.自定义视频滤镜,
                .滤镜目标流类型 = 预设数据_v6.滤镜排序单片结构.流类型.视频,
                .自定义滤镜内容 = "format=gbrpf32le"
            },
            New 预设数据_v6.滤镜排序单片结构 With {
                .显示名称 = "压缩亮度",
                .是自定义滤镜 = True,
                .允许在排序页直接编辑 = True,
                .滤镜标识符 = 预设数据_v6.滤镜排序单片结构.标识符枚举.自定义视频滤镜,
                .滤镜目标流类型 = 预设数据_v6.滤镜排序单片结构.流类型.视频,
                .自定义滤镜内容 = "tonemap=tonemap=mobius:peak=4:param=0.3:desat=0"
            },
            New 预设数据_v6.滤镜排序单片结构 With {
                .显示名称 = "线性 RGB 转 HDR",
                .是自定义滤镜 = True,
                .允许在排序页直接编辑 = True,
                .滤镜标识符 = 预设数据_v6.滤镜排序单片结构.标识符枚举.自定义视频滤镜,
                .滤镜目标流类型 = 预设数据_v6.滤镜排序单片结构.流类型.视频,
                .自定义滤镜内容 = "zscale=transferin=linear:matrixin=gbr:primariesin=bt2020:transfer=smpte2084:matrix=bt2020nc:primaries=bt2020:range=tv:npl=1000"
            },
            New 预设数据_v6.滤镜排序单片结构 With {
                .显示名称 = "输出 10bit 4:2:0",
                .是自定义滤镜 = True,
                .允许在排序页直接编辑 = True,
                .滤镜标识符 = 预设数据_v6.滤镜排序单片结构.标识符枚举.自定义视频滤镜,
                .滤镜目标流类型 = 预设数据_v6.滤镜排序单片结构.流类型.视频,
                .自定义滤镜内容 = "format=yuv420p10le"
            },
            New 预设数据_v6.滤镜排序单片结构 With {
                .显示名称 = "清除旧 HDR 静态元数据",
                .是自定义滤镜 = True,
                .允许在排序页直接编辑 = True,
                .滤镜标识符 = 预设数据_v6.滤镜排序单片结构.标识符枚举.自定义视频滤镜,
                .滤镜目标流类型 = 预设数据_v6.滤镜排序单片结构.流类型.视频,
                .自定义滤镜内容 = "sidedata=delete:type=MASTERING_DISPLAY_METADATA,sidedata=delete:type=CONTENT_LIGHT_LEVEL"
            }
        }
        result.Add(New 预设项("HDR 电影 4000nit 转 1000nit", HDR4000_To_HDR1000))


        Return result
    End Function

    Public Shared Function 克隆预设数据(source As 预设数据_v6) As 预设数据_v6
        If source Is Nothing Then Return Nothing
        Dim json = JsonSerializer.Serialize(source, JsonSO)
        Dim result = JsonSerializer.Deserialize(Of 预设数据_v6)(json, JsonSO)
        预设管理_v6.初始化空集合(result)
        Return result
    End Function

End Class
