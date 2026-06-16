Public Class 音频编码器数据库_v6

    Private Shared ReadOnly 编码器列表 As New List(Of 音频编码器数据)
    Private Shared ReadOnly ID索引 As New Dictionary(Of String, 音频编码器数据)(StringComparer.OrdinalIgnoreCase)
    Private Shared ReadOnly 显示名称索引 As New Dictionary(Of String, 音频编码器数据)(StringComparer.OrdinalIgnoreCase)
    Private Shared ReadOnly 旧版命令索引 As New Dictionary(Of String, 音频编码器数据)(StringComparer.OrdinalIgnoreCase)

    Shared Sub New()
        初始化编码器()
    End Sub

    Public Shared ReadOnly Property 全部编码器 As IReadOnlyList(Of 音频编码器数据)
        Get
            Return 编码器列表.AsReadOnly()
        End Get
    End Property

    Public Shared Function 获取编码器数据(私有ID As String) As 音频编码器数据
        Dim value As 音频编码器数据 = Nothing
        If ID索引.TryGetValue(If(私有ID, ""), value) Then Return value
        Return Nothing
    End Function

    Public Shared Function 获取编码器数据_按显示名称(显示名称 As String) As 音频编码器数据
        Dim value As 音频编码器数据 = Nothing
        If 显示名称索引.TryGetValue(If(显示名称, ""), value) Then Return value
        Return Nothing
    End Function

    Public Shared Function 获取私有ID(显示名称 As String) As String
        Dim 数据 = 获取编码器数据_按显示名称(显示名称)
        If 数据 Is Nothing Then Return ""
        Return 数据.私有ID
    End Function

    Public Shared Function 获取显示名称(私有ID As String) As String
        Dim 数据 = 获取编码器数据(私有ID)
        If 数据 Is Nothing Then Return ""
        Return 数据.显示名称
    End Function

    Public Shared Function 解析为私有ID(值 As String) As String
        If String.IsNullOrWhiteSpace(值) Then Return ""

        Dim 数据 = 获取编码器数据(值)
        If 数据 IsNot Nothing Then Return 数据.私有ID

        数据 = 获取编码器数据_按显示名称(值)
        If 数据 IsNot Nothing Then Return 数据.私有ID

        If 旧版命令索引.TryGetValue(值.Trim(), 数据) Then Return 数据.私有ID
        Return ""
    End Function

    Public Shared Function 获取命令行编码器名(私有ID As String) As String
        Dim 数据 = 获取编码器数据(私有ID)
        If 数据 Is Nothing Then Return ""
        Return 数据.命令行编码器名
    End Function

    Public Shared Function 获取命令行片段(私有ID As String) As String
        Dim 数据 = 获取编码器数据(私有ID)
        If 数据 Is Nothing OrElse 数据.命令行编码器名 = "" Then Return ""
        If 数据.是否禁用 Then Return "-an"

        Dim 参数 As New List(Of String) From {$"-c:a {数据.命令行编码器名}"}
        For Each 单项参数 In 数据.默认附加参数列表
            参数.Add(单项参数.转命令行文本())
        Next
        Return String.Join(" ", 参数.Where(Function(x) x <> ""))
    End Function

    Public Shared Function 获取质量参数名列表() As List(Of String)
        Dim result As New List(Of String) From {""}
        Dim exists As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
        exists.Add("")

        For Each 编码器 In 编码器列表
            For Each 参数项 As 音频编码器参数数据 In 编码器.质量参数列表.Concat(编码器.特殊参数列表.Where(AddressOf 是质量参数下拉框参数))
                Dim 参数名 = If(参数项.参数名, "").Trim()
                If 参数名 = "" OrElse exists.Contains(参数名) Then Continue For
                result.Add(参数名)
                exists.Add(参数名)
            Next
        Next

        Return result
    End Function

    Private Shared Function 是质量参数下拉框参数(参数 As 音频编码器参数数据) As Boolean
        If 参数 Is Nothing Then Return False
        Select Case If(参数.参数名, "").Trim()
            Case "-aac_at_mode", "-aac_at_quality", "-abr"
                Return True
        End Select
        Return False
    End Function

    Private Shared Sub 初始化编码器()
        加入编码器(基础("", "", "", "不指定音频编码器。",
            VBR说明:="未指定编码器，VBR/CBR 参数不适用。"))
        加入编码器(基础("audio.copy", "复制流", "copy", "复制音频流，不重新编码。",
            VBR说明:="复制源音频码率控制方式，不重新设置 VBR/CBR。",
            是否复制流:=True))
        加入编码器(基础("audio.disable", "禁用", "-an", "禁用音频流输出。",
            VBR说明:="禁用音频输出，不适用。",
            是否禁用:=True))

        加入编码器(基础("aac.native", "AAC", "aac", "FFmpeg 原生 AAC 编码器，兼容性好，适合作为通用默认选择。",
            质量参数:=参数列表(参数("-q:a", "VBR 质量，常用 0.1 ~ 2.0，越高越好"), 参数("-b:a", "CBR/ABR 目标比特率")),
            特殊参数:=参数列表(参数("-aac_coder", "twoloop/fast", "twoloop 质量优先，fast 速度优先"), 参数("-aac_ms", "auto/0/1", "强制 M/S 立体声编码"), 参数("-aac_pns", "0/1", "感知噪声替代")),
            VBR说明:="支持。使用 -q:a 设置 VBR 质量；填写 -b:a 时按目标比特率编码。",
            支持说明:="采样格式：fltp；采样率：7350/8000/11025/12000/16000/22050/24000/32000/44100/48000/64000/88200/96000。"))

        加入编码器(基础("aac.fdk", "FDK AAC", "libfdk_aac", "Fraunhofer FDK AAC，低码率表现好；需要当前 FFmpeg 编译启用 libfdk_aac。",
            质量参数:=参数列表(参数("-vbr", "1 ~ 5，越高越好；0 关闭 VBR"), 参数("-b:a", "CBR/ABR 比特率")),
            特殊参数:=参数列表(参数("-afterburner", "0/1", "提高质量，默认 1"), 参数("-signaling", "default/implicit/explicit_sbr/explicit_hierarchical", "SBR/PS 信令方式"), 参数("-latm", "0/1", "输出 LATM/LOAS"), 参数("-frame_length", "-1 或 1024", "帧长度")),
            VBR说明:="支持。使用 -vbr 1 ~ 5，0 为关闭；也可用 -b:a 走目标比特率。",
            支持说明:="采样格式：s16；采样率：8000/11025/12000/16000/22050/24000/32000/44100/48000/64000/88200/96000；声道布局：mono、stereo、3.0、4.0、5.0、5.1、6.1(back)、7.1(wide)、7.1、5.1.2(back)。"))

        加入编码器(基础("aac.fdk.he", "FDK AAC HE", "libfdk_aac", "FDK AAC High-Efficiency 配置，适合低码率语音、直播或小体积音频。",
            默认附加参数:=参数列表(参数("-profile:a", "aac_he", "固定使用 HE-AAC")),
            质量参数:=参数列表(参数("-vbr", "1 ~ 5，越高越好；0 关闭 VBR"), 参数("-b:a", "CBR/ABR 比特率")),
            特殊参数:=参数列表(参数("-afterburner", "0/1", "提高质量，默认 1"), 参数("-signaling", "default/implicit/explicit_sbr/explicit_hierarchical", "SBR/PS 信令方式"), 参数("-latm", "0/1", "输出 LATM/LOAS")),
            VBR说明:="支持。使用 -vbr 1 ~ 5，0 为关闭；HE-AAC 主要面向低码率，建议结合 -b:a 做目标码率控制。",
            支持说明:="采样格式：s16；采样率：8000 ~ 96000；声道布局同 libfdk_aac，但 HE-AAC 常用于 mono/stereo 低码率场景。",
            旧版命令文本:="libfdk_aac -profile:a aac_he"))

        加入编码器(基础("aac.fdk.hev2", "FDK AAC HE v2", "libfdk_aac", "FDK AAC HE v2，面向更低码率的立体声场景。",
            默认附加参数:=参数列表(参数("-profile:a", "aac_he_v2", "固定使用 HE-AAC v2")),
            质量参数:=参数列表(参数("-vbr", "1 ~ 5，越高越好；0 关闭 VBR"), 参数("-b:a", "CBR/ABR 比特率")),
            特殊参数:=参数列表(参数("-afterburner", "0/1", "提高质量，默认 1"), 参数("-signaling", "default/implicit/explicit_sbr/explicit_hierarchical", "SBR/PS 信令方式"), 参数("-latm", "0/1", "输出 LATM/LOAS")),
            VBR说明:="支持。使用 -vbr 1 ~ 5，0 为关闭；HE v2 面向极低码率立体声，通常配合 -b:a 使用。",
            支持说明:="采样格式：s16；采样率：8000 ~ 96000；声道布局同 libfdk_aac，HE v2 主要用于 stereo。",
            旧版命令文本:="libfdk_aac -profile:a aac_he_v2"))

        加入编码器(基础("aac.audiotoolbox", "AudioToolbox AAC", "aac_at", "Apple AudioToolbox AAC，适合想要接近 qaac/Apple AAC 的输出时优先尝试。",
            质量参数:=参数列表(参数("-q:a", "全局质量；FFmpeg 未枚举固定范围，给定时 auto 模式走 VBR"), 参数("-b:a", "CBR/ABR/CVBR 目标比特率")),
            特殊参数:=AudioToolbox参数(),
            VBR说明:="支持。使用 -aac_at_mode vbr/cvbr 指定模式；auto 模式下给定 -q:a 会走 VBR。",
            支持说明:="采样格式：s16/u8；声道布局：mono、stereo、3.0、4.0、5.0(side)、5.1(side)、6.0、6.1、7.0、7.1(wide)、quad、octagonal。"))

        加入编码器(基础("mp3.lame", "LAME MP3", "libmp3lame", "LAME MP3，老设备兼容性最好。",
            质量参数:=参数列表(参数("-q:a", "VBR 质量 0 最高 ~ 9 最低"), 参数("-b:a", "CBR/ABR 目标比特率")),
            特殊参数:=参数列表(参数("-abr", "0/1", "启用 ABR"), 参数("-joint_stereo", "0/1", "联合立体声"), 参数("-reservoir", "0/1", "比特储备")),
            VBR说明:="支持。使用 -q:a 0 ~ 9；若要 ABR，使用 -abr 1 并填写 -b:a。",
            支持说明:="采样格式：s32p/fltp/s16p；采样率：8000/11025/12000/16000/22050/24000/32000/44100/48000；声道布局：mono、stereo。"))

        加入编码器(基础("opus.libopus", "Opus", "libopus", "Opus，适合语音、直播、低码率和现代网页播放。",
            质量参数:=参数列表(参数("-b:a", "目标比特率；常用 24k ~ 256k，最高约 512k")),
            特殊参数:=参数列表(参数("-vbr", "off/on/constrained", "VBR 模式"), 参数("-application", "voip/audio/lowdelay", "用途模式"), 参数("-frame_duration", "2.5 ~ 120 ms", "帧时长"), 参数("-packet_loss", "0 ~ 100", "预期丢包百分比"), 参数("-fec", "0/1", "带内前向纠错"), 参数("-dtx", "0/1", "非连续发送")),
            VBR说明:="支持且默认 on。使用 -vbr on/off/constrained；质量主要由 -b:a 的目标码率决定。",
            支持说明:="采样格式：s16/flt；采样率：8000/12000/16000/24000/48000。"))

        加入编码器(基础("flac.native", "FLAC", "flac", "FLAC 无损压缩，适合音乐归档。",
            质量参数:=参数列表(参数("-compression_level", "0 最快 ~ 12 最小")),
            特殊参数:=参数列表(参数("-lpc_type", "none/fixed/levinson/cholesky", "LPC 算法"), 参数("-min_partition_order", "-1 ~ 8", "最小分区阶数"), 参数("-max_partition_order", "-1 ~ 8", "最大分区阶数"), 参数("-ch_mode", "auto/indep/left_side/right_side/mid_side", "立体声去相关模式")),
            VBR说明:="不支持有损 VBR。FLAC 是无损编码，使用 -compression_level 0 ~ 12 控制压缩强度。",
            支持说明:="采样格式：s16/s32。"))

        加入编码器(基础("alac.native", "ALAC", "alac", "Apple Lossless，无损压缩，适合苹果生态。",
            特殊参数:=参数列表(参数("-min_prediction_order", "1 ~ 30", "最小预测阶数"), 参数("-max_prediction_order", "1 ~ 30", "最大预测阶数")),
            VBR说明:="不支持有损 VBR。ALAC 是无损编码，输出大小由源内容和预测参数决定。",
            支持说明:="采样格式：s16p/s32p；声道布局：mono、stereo、3.0、4.0、5.0、5.1、6.1(back)、7.1(wide)。"))

        加入编码器(基础("alac.audiotoolbox", "AudioToolbox ALAC", "alac_at", "AudioToolbox ALAC，无损音频编码器。",
            特殊参数:=AudioToolbox参数(),
            VBR说明:="不适用。ALAC 是无损编码；AudioToolbox 通用码率模式参数存在，但对无损 ALAC 不建议作为 VBR 控制使用。",
            支持说明:="采样格式：s16/u8。"))

        加入编码器(基础("pcm.s16le", "WAV 16bit", "pcm_s16le", "16-bit PCM，常规 WAV 无损未压缩格式。",
            VBR说明:="不支持。PCM 是未压缩固定采样格式，码率由采样率、位深和声道数决定。",
            支持说明:="采样格式：s16。"))
        加入编码器(基础("pcm.s24le", "WAV 24bit", "pcm_s24le", "24-bit PCM，常用于音频制作和中间文件。",
            VBR说明:="不支持。PCM 是未压缩固定采样格式，码率由采样率、位深和声道数决定。",
            支持说明:="采样格式：s32 输入，输出 24-bit PCM。"))
        加入编码器(基础("pcm.s32le", "WAV 32bit", "pcm_s32le", "32-bit PCM，适合需要更高整数精度的中间文件。",
            VBR说明:="不支持。PCM 是未压缩固定采样格式，码率由采样率、位深和声道数决定。",
            支持说明:="采样格式：s32。"))
        加入编码器(基础("pcm.s64le", "WAV 64bit", "pcm_s64le", "64-bit PCM，体积极大，通常仅用于特殊工作流。",
            VBR说明:="不支持。PCM 是未压缩固定采样格式，码率由采样率、位深和声道数决定。",
            支持说明:="采样格式：s64。"))

        加入编码器(基础("pcm.alaw.audiotoolbox", "AudioToolbox PCM A-law", "pcm_alaw_at", "AudioToolbox G.711 A-law PCM。",
            特殊参数:=AudioToolbox参数(),
            VBR说明:="不适用。G.711 A-law 是固定 8-bit 对数量化 PCM。",
            支持说明:="采样格式：s16/u8。"))

        加入编码器(基础("pcm.mulaw.audiotoolbox", "AudioToolbox PCM mu-law", "pcm_mulaw_at", "AudioToolbox G.711 mu-law PCM。",
            特殊参数:=AudioToolbox参数(),
            VBR说明:="不适用。G.711 mu-law 是固定 8-bit 对数量化 PCM。",
            支持说明:="采样格式：s16/u8。"))

        加入编码器(基础("ac3.native", "ATSC A/52A (AC3)", "ac3", "AC-3，常见于 DVD、电视广播和家庭影院兼容场景。",
            质量参数:=参数列表(参数("-b:a", "固定码率；常用 192k ~ 640k，AC-3 最高 640k")),
            特殊参数:=参数列表(参数("-dialnorm", "-31 ~ -1", "对白归一化"), 参数("-dmix_mode", "notindicated/ltrt/loro/dplii", "首选立体声下混模式"), 参数("-dsur_mode", "notindicated/on/off", "Dolby Surround 标记"), 参数("-stereo_rematrixing", "0/1", "立体声重矩阵")),
            VBR说明:="不支持。AC-3 使用固定码率，设置 -b:a。",
            支持说明:="采样格式：fltp；采样率：32000/44100/48000；声道布局：mono、stereo、3.0、quad、4.0、5.0、5.1 等。"))

        加入编码器(基础("eac3.native", "ATSC A/52B (EAC3)", "eac3", "E-AC-3，AC-3 的扩展版本，适合更高码率和流媒体场景。",
            质量参数:=参数列表(参数("-b:a", "固定码率；常用 384k ~ 1536k，更高码率视封装/播放器而定")),
            特殊参数:=参数列表(参数("-dialnorm", "-31 ~ -1", "对白归一化"), 参数("-dmix_mode", "notindicated/ltrt/loro/dplii", "首选立体声下混模式"), 参数("-dsur_mode", "notindicated/on/off", "Dolby Surround 标记"), 参数("-stereo_rematrixing", "0/1", "立体声重矩阵")),
            VBR说明:="不支持。E-AC-3 使用固定码率，设置 -b:a。",
            支持说明:="采样格式：fltp；采样率：32000/44100/48000；声道布局：mono、stereo、3.0、quad、4.0、5.0、5.1 等。"))

        加入编码器(基础("dts.dca", "DTS Coherent Acoustics", "dca", "DTS Coherent Acoustics；FFmpeg 标记为实验性编码器，必要时添加 -strict -2。",
            质量参数:=参数列表(参数("-b:a", "固定码率；常用 768k ~ 1536k")),
            特殊参数:=参数列表(参数("-dca_adpcm", "0/1", "使用 ADPCM 编码"), 参数("-strict", "-2", "实验性编码器可能需要")),
            VBR说明:="不支持。DCA 使用固定码率，设置 -b:a。",
            支持说明:="采样格式：s32；采样率：8000/11025/12000/16000/22050/24000/32000/44100/48000；声道布局：mono、stereo、quad(side)、5.0(side)、5.1(side)。"))

        加入编码器(基础("truehd.native", "TrueHD", "truehd", "Dolby TrueHD；FFmpeg 标记为实验性编码器，必要时添加 -strict -2。",
            特殊参数:=参数列表(参数("-max_interval", "8 ~ 128", "新 header 最大间隔帧数"), 参数("-lpc_type", "levinson/cholesky", "LPC 算法"), 参数("-prediction_order", "0 ~ 4", "预测阶数搜索方法"), 参数("-strict", "-2", "实验性编码器可能需要")),
            VBR说明:="不支持有损 VBR。TrueHD 是无损编码，输出大小由源内容决定。",
            支持说明:="采样格式：s16p/s32p；采样率：44100/48000/88200/96000/176400/192000；声道布局：mono、stereo、2.1、3.0、3.1、4.0、4.1、5.0(side)、5.1(side)。"))

        加入编码器(基础("ilbc.audiotoolbox", "AudioToolbox iLBC", "ilbc_at", "AudioToolbox iLBC，面向语音通信的低码率编码器。",
            特殊参数:=AudioToolbox参数(),
            VBR说明:="通常不作为 VBR 使用；可通过 AudioToolbox 通用 -aac_at_mode 参数尝试码率模式。",
            支持说明:="采样格式：s16/u8。"))

        加入编码器(基础("tta.native", "True Audio", "tta", "TTA 无损音频编码器。",
            VBR说明:="不支持有损 VBR。TTA 是无损编码，输出大小由源内容决定。",
            支持说明:="采样格式：u8/s16/s32。"))

        加入编码器(基础("vorbis.libvorbis", "Vorbis (ogg)", "libvorbis", "libvorbis，适合 OGG/Vorbis 兼容场景。",
            质量参数:=参数列表(参数("-q:a", "VBR 质量 -1 ~ 10，越高越好"), 参数("-b:a", "目标比特率；未给 -q:a 时用于码率管理")),
            特殊参数:=参数列表(参数("-iblock", "-15 ~ 0", "瞬态块偏置")),
            VBR说明:="支持。推荐使用 -q:a -1 ~ 10；填写 -b:a 时进入目标码率/码率管理路径。",
            支持说明:="采样格式：fltp。"))

        加入编码器(基础("ra.real144", "RealAudio 1.0 (14.4K)", "real_144", "RealAudio 1.0 老旧编码器，仅为兼容特殊旧格式。",
            VBR说明:="不支持。RealAudio 1.0 是固定 14.4k 级别的旧格式。",
            支持说明:="用于旧 RealAudio 兼容，能力有限。"))

        加入编码器(基础("wavpack.native", "WavPack", "wavpack", "WavPack 无损/混合音频编码器。",
            特殊参数:=参数列表(参数("-joint_stereo", "0/1/auto", "联合立体声"), 参数("-optimize_mono", "0/1", "优化单声道")),
            VBR说明:="不使用常规 VBR。默认无损；混合/有损场景通常通过目标比特率或自定义参数控制。",
            支持说明:="采样格式：u8p/s16p/s32p/fltp。"))

        加入编码器(基础("mp2.twolame", "LAME MP2", "libtwolame", "libtwolame MP2，适合老式广播和兼容场景。",
            质量参数:=参数列表(参数("-b:a", "固定码率；常用 128k ~ 384k")),
            特殊参数:=参数列表(参数("-mode", "auto/stereo/joint_stereo/dual_channel/mono", "声道模式"), 参数("-psymodel", "-1 ~ 4", "心理声学模型"), 参数("-error_protection", "0/1", "CRC 错误保护"), 参数("-energy_levels", "0/1", "写入能量级别")),
            VBR说明:="不支持。libtwolame MP2 使用固定码率，设置 -b:a。",
            支持说明:="采样格式：flt/fltp/s16/s16p；采样率：16000/22050/24000/32000/44100/48000；声道布局：mono、stereo。"))

        加入编码器(基础("amr.nb.opencore", "AMR-NB", "libopencore_amrnb", "AMR-NB 窄带语音编码器。",
            质量参数:=参数列表(参数("-b:a", "离散码率：4.75k/5.15k/5.9k/6.7k/7.4k/7.95k/10.2k/12.2k")),
            特殊参数:=参数列表(参数("-dtx", "0/1", "舒适噪声/非连续发送")),
            VBR说明:="不支持。AMR-NB 使用离散固定码率，设置 -b:a。",
            支持说明:="采样格式：s16；通常需要 8000 Hz mono。"))

        加入编码器(基础("amr.wb.vo", "AMR-WB", "libvo_amrwbenc", "AMR-WB 宽带语音编码器。",
            质量参数:=参数列表(参数("-b:a", "离散码率：6.6k/8.85k/12.65k/14.25k/15.85k/18.25k/19.85k/23.05k/23.85k")),
            特殊参数:=参数列表(参数("-dtx", "0/1", "舒适噪声/非连续发送")),
            VBR说明:="不支持。AMR-WB 使用离散固定码率，设置 -b:a。",
            支持说明:="采样格式：s16；通常需要 16000 Hz mono。"))



    End Sub

    Private Shared Sub 加入编码器(数据 As 音频编码器数据)
        编码器列表.Add(数据)
        ID索引(数据.私有ID) = 数据
        显示名称索引(数据.显示名称) = 数据

        If 数据.旧版命令文本 <> "" Then 旧版命令索引(数据.旧版命令文本) = 数据
        If 数据.命令行编码器名 <> "" AndAlso Not 旧版命令索引.ContainsKey(数据.命令行编码器名) Then 旧版命令索引(数据.命令行编码器名) = 数据
    End Sub

    Private Shared Function 基础(私有ID As String,
                               显示名称 As String,
                               命令行编码器名 As String,
                               描述 As String,
                               Optional 质量参数 As List(Of 音频编码器参数数据) = Nothing,
                               Optional 特殊参数 As List(Of 音频编码器参数数据) = Nothing,
                               Optional 默认附加参数 As List(Of 音频编码器参数数据) = Nothing,
                               Optional VBR说明 As String = "",
                               Optional 支持说明 As String = "",
                               Optional 旧版命令文本 As String = "",
                               Optional 是否复制流 As Boolean = False,
                               Optional 是否禁用 As Boolean = False) As 音频编码器数据
        Return New 音频编码器数据 With {
            .私有ID = 私有ID,
            .显示名称 = 显示名称,
            .命令行编码器名 = 命令行编码器名,
            .描述 = 描述,
            .质量参数列表 = If(质量参数, New List(Of 音频编码器参数数据)),
            .特殊参数列表 = If(特殊参数, New List(Of 音频编码器参数数据)),
            .默认附加参数列表 = If(默认附加参数, New List(Of 音频编码器参数数据)),
            .VBR说明 = VBR说明,
            .支持说明 = 支持说明,
            .旧版命令文本 = 旧版命令文本,
            .是否复制流 = 是否复制流,
            .是否禁用 = 是否禁用
        }
    End Function

    Private Shared Function 参数(参数名 As String, 值范围说明 As String, Optional 说明 As String = "", Optional 默认值 As String = "") As 音频编码器参数数据
        Return New 音频编码器参数数据 With {
            .参数名 = 参数名,
            .值范围说明 = 值范围说明,
            .说明 = 说明,
            .默认值 = 默认值
        }
    End Function

    Private Shared Function 参数列表(ParamArray 数据() As 音频编码器参数数据) As List(Of 音频编码器参数数据)
        Return New List(Of 音频编码器参数数据)(数据)
    End Function

    Private Shared Function AudioToolbox参数() As List(Of 音频编码器参数数据)
        Return 参数列表(
            参数("-aac_at_mode", "auto/cbr/abr/cvbr/vbr", "码率控制模式；auto 在给定 -q:a 时使用 VBR"),
            参数("-aac_at_quality", "0 ~ 2", "质量与速度控制，0 默认"))
    End Function

    Public Class 音频编码器数据
        Public Property 私有ID As String = ""
        Public Property 显示名称 As String = ""
        Public Property 命令行编码器名 As String = ""
        Public Property 描述 As String = ""
        Public Property 质量参数列表 As New List(Of 音频编码器参数数据)
        Public Property 特殊参数列表 As New List(Of 音频编码器参数数据)
        Public Property 默认附加参数列表 As New List(Of 音频编码器参数数据)
        Public Property VBR说明 As String = ""
        Public Property 支持说明 As String = ""
        Public Property 旧版命令文本 As String = ""
        Public Property 是否复制流 As Boolean = False
        Public Property 是否禁用 As Boolean = False

        Public ReadOnly Property 下拉提示文本 As String
            Get
                Dim 片段 As New List(Of String)
                If 描述 <> "" Then 片段.Add(描述)
                If 命令行编码器名 <> "" Then 片段.Add(If(是否禁用, "命令：-an", $"命令：-c:a {命令行编码器名}"))
                If 默认附加参数列表.Count > 0 Then 片段.Add("默认附加：" & String.Join(" ", 默认附加参数列表.Select(Function(x) x.转命令行文本())))
                If VBR说明 <> "" Then 片段.Add("VBR：" & VBR说明)
                If 质量参数列表.Count > 0 Then 片段.Add("质量/码率：" & String.Join("；", 质量参数列表.Select(Function(x) x.转说明文本())))
                If 特殊参数列表.Count > 0 Then 片段.Add("参数：" & String.Join("；", 特殊参数列表.Select(Function(x) x.转说明文本())))
                If 支持说明 <> "" Then 片段.Add("支持：" & 支持说明)
                If 私有ID <> "" Then 片段.Add("ID：" & 私有ID)
                Return String.Join(vbCrLf & vbCrLf, 片段)
            End Get
        End Property
    End Class

    Public Class 音频编码器参数数据
        Public Property 参数名 As String = ""
        Public Property 值范围说明 As String = ""
        Public Property 说明 As String = ""
        Public Property 默认值 As String = ""

        Public Function 转说明文本() As String
            Dim 文本 = $"{参数名} {值范围说明}".Trim()
            If 说明 <> "" Then 文本 &= $"（{说明}）"
            Return 文本
        End Function

        Public Function 转命令行文本() As String
            If 参数名 = "" Then Return ""
            If 默认值 = "" Then Return 参数名
            Return $"{参数名} {默认值}"
        End Function
    End Class

End Class
