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
    Public Shared Function 获取全部() As List(Of 预设项)
        Dim result As New List(Of 预设项)

        Dim RTX50_AV1 As New 预设数据_v6
        RTX50_AV1.预设备注 = "老黄 AV1 常规模式标准答案，参考 VMAF = 95~96，最推荐 RTX50 全系使用。此预设仅包含视频参数！"
        RTX50_AV1.视频参数_编码器_类型 = 预设数据_v6.视频编码器类型.视频
        RTX50_AV1.视频参数_编码器_分类名称 = "AV1"
        RTX50_AV1.视频参数_编码器_具体编码 = "av1_nvenc"
        RTX50_AV1.视频参数_编码器_编码预设 = "p7"
        RTX50_AV1.视频参数_比特率_控制方式 = 预设数据_v6.视频全局质量控制方式.VBR
        RTX50_AV1.视频参数_质量控制_参数名 = "cq"
        RTX50_AV1.视频参数_质量控制_值 = "36"
        result.Add(New 预设项("RTX50 AV1", RTX50_AV1))

        Dim RTX50_AV1_UHQ As New 预设数据_v6
        RTX50_AV1_UHQ.预设备注 = "老黄 AV1 UHQ 模式推荐答案，极细微质量损失换来体积大幅降低，参考 VMAF = 94~95，最推荐 RTX50 全系使用。此预设仅包含视频参数！"
        RTX50_AV1_UHQ.视频参数_编码器_类型 = 预设数据_v6.视频编码器类型.视频
        RTX50_AV1_UHQ.视频参数_编码器_分类名称 = "AV1"
        RTX50_AV1_UHQ.视频参数_编码器_具体编码 = "av1_nvenc"
        RTX50_AV1_UHQ.视频参数_编码器_编码预设 = "p7"
        RTX50_AV1_UHQ.视频参数_编码器_场景优化 = "uhq"
        RTX50_AV1_UHQ.视频参数_比特率_控制方式 = 预设数据_v6.视频全局质量控制方式.VBRHQ
        RTX50_AV1_UHQ.视频参数_质量控制_参数名 = "cq"
        RTX50_AV1_UHQ.视频参数_质量控制_值 = "38"
        result.Add(New 预设项("RTX50 AV1 UHQ", RTX50_AV1_UHQ))

        Dim RTX50_AV1_UHQ_EX As New 预设数据_v6
        RTX50_AV1_UHQ_EX.预设备注 = "老黄 AV1 UHQ + 复杂度全开，提升极细微可能对稳定度有帮助但同时也可能不利于评测，参考 VMAF = 94~95，最推荐 RTX50 全系使用。此预设仅包含视频参数！"
        RTX50_AV1_UHQ_EX.视频参数_编码器_类型 = 预设数据_v6.视频编码器类型.视频
        RTX50_AV1_UHQ_EX.视频参数_编码器_分类名称 = "AV1"
        RTX50_AV1_UHQ_EX.视频参数_编码器_具体编码 = "av1_nvenc"
        RTX50_AV1_UHQ_EX.视频参数_编码器_编码预设 = "p7"
        RTX50_AV1_UHQ_EX.视频参数_编码器_场景优化 = "uhq"
        RTX50_AV1_UHQ_EX.视频参数_比特率_控制方式 = 预设数据_v6.视频全局质量控制方式.VBRHQ
        RTX50_AV1_UHQ_EX.视频参数_质量控制_参数名 = "cq"
        RTX50_AV1_UHQ_EX.视频参数_质量控制_值 = "38"
        RTX50_AV1_UHQ_EX.视频参数_质量控制_进阶参数集 = "-multipass fullres -rc-lookahead 240 -spatial-aq 1 -temporal-aq 1 -aq-strength 15 -tf_level -1"
        result.Add(New 预设项("RTX50 AV1 UHQ 超压", RTX50_AV1_UHQ_EX))

        Dim RTX50_HEVC_UHQ As New 预设数据_v6
        RTX50_HEVC_UHQ.预设备注 = "老黄 HEVC UHQ 满分答案，参考 VMAF = 95~96，最推荐 RTX50 全系使用，压缩度大概能摸到 x265 medium 水平。此预设仅包含视频参数！"
        RTX50_HEVC_UHQ.视频参数_编码器_类型 = 预设数据_v6.视频编码器类型.视频
        RTX50_HEVC_UHQ.视频参数_编码器_分类名称 = "H.265/HEVC"
        RTX50_HEVC_UHQ.视频参数_编码器_具体编码 = "hevc_nvenc"
        RTX50_HEVC_UHQ.视频参数_编码器_编码预设 = "p7"
        RTX50_HEVC_UHQ.视频参数_编码器_场景优化 = "uhq"
        RTX50_HEVC_UHQ.视频参数_比特率_控制方式 = 预设数据_v6.视频全局质量控制方式.VBRHQ
        RTX50_HEVC_UHQ.视频参数_质量控制_参数名 = "cq"
        RTX50_HEVC_UHQ.视频参数_质量控制_值 = "28"
        result.Add(New 预设项("RTX50 HEVC UHQ", RTX50_HEVC_UHQ))

        Dim X265_Slow As New 预设数据_v6
        X265_Slow.预设备注 = "x265 默认推荐质量，选用 slow 预设可直接作为普通人的最终压制方案，参考 VMAF ≈ 95。此预设仅包含视频参数！"
        X265_Slow.视频参数_编码器_类型 = 预设数据_v6.视频编码器类型.视频
        X265_Slow.视频参数_编码器_分类名称 = "H.265/HEVC"
        X265_Slow.视频参数_编码器_具体编码 = "libx265"
        X265_Slow.视频参数_编码器_编码预设 = "slow"
        X265_Slow.视频参数_比特率_控制方式 = 预设数据_v6.视频全局质量控制方式.CRF
        X265_Slow.视频参数_质量控制_参数名 = "crf"
        X265_Slow.视频参数_质量控制_值 = "24"
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
        result.Add(New 预设项("x264 slower", X264_Slower))

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
        AVIF_AOMAV1.预设备注 = "AVIF 是将 AV1 用于图片的超高压缩图，非常适合用它来压制你那巨量不再需要后期的图片。"
        AVIF_AOMAV1.输出容器 = ".avif"
        AVIF_AOMAV1.视频参数_编码器_分类名称 = "AV1"
        AVIF_AOMAV1.视频参数_编码器_具体编码 = "libaom-av1"
        AVIF_AOMAV1.视频参数_编码器_编码预设 = "1"
        AVIF_AOMAV1.视频参数_编码器_场景优化 = "ssim"
        AVIF_AOMAV1.视频参数_比特率_控制方式 = 预设数据_v6.视频全局质量控制方式.CRF
        AVIF_AOMAV1.视频参数_质量控制_参数名 = "crf"
        AVIF_AOMAV1.视频参数_质量控制_值 = "18"
        AVIF_AOMAV1.视频参数_质量控制_进阶参数集 = "-aom-params tune=iq -b:v 0 -still-picture 1 -row-mt 1"
        result.Add(New 预设项("AVIF 高压缩图片 AOM AV1", AVIF_AOMAV1))


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
