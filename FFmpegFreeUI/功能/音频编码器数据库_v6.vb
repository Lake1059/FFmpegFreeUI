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
        Dim exists As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase) From {""}

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
            Case "-aac_at_mode", "-aac_at_quality", "-aac_nmr_speed", "-abr"
                Return True
        End Select
        Return False
    End Function

    Private Shared Sub 初始化编码器()
        加入编码器(基础("", "", "", "沿用 FFmpeg 默认音频编码器。"))
        加入编码器(基础("audio.copy", "复制流", "copy", "直接复制源音频，不重新编码。",
            是否复制流:=True))
        加入编码器(基础("audio.disable", "禁用", "-an", "不输出音频流。",
            是否禁用:=True))

        加入编码器(基础("aac.native", "AAC", "aac", "FFmpeg 原生 AAC，兼容性好，默认使用 NMR 算法。",
            质量参数:=参数列表(参数("-q:a", "可变质量：0.1~2.0 常用；越高越清晰、体积越大"), 参数("-b:a", "目标码率：如 128k/192k/320k")),
            特殊参数:=参数列表(参数("-aac_coder", "nmr/twoloop/fast", "编码算法：nmr=默认，twoloop=旧版质量优先，fast=旧版速度优先"), 参数("-aac_ms", "auto/0/1", "M/S 立体声：auto=自动，0=关闭，1=强制"), 参数("-aac_is", "auto/0/1", "强度立体声：auto=自动"), 参数("-aac_pns", "0/1", "感知噪声替代，低码率时可能有益"), 参数("-aac_tns", "0/1", "时域噪声整形"), 参数("-aac_pce", "0/1", "写入 PCE 声道配置")),
            支持说明:="输入：fltp；采样率：7.35~96 kHz 的标准 AAC 档位。"))

        加入编码器(基础("aac.nmr", "NMR AAC", "aac", "原生 AAC 的 NMR 模式；不是独立的 nmraac 编码器。",
            默认附加参数:=参数列表(参数("-aac_coder", "nmr", "固定使用 NMR", 默认值:="nmr")),
            质量参数:=参数列表(参数("-q:a", "可变质量：0.1~2.0 常用；越高越清晰、体积越大"), 参数("-b:a", "目标码率：如 96k/128k/192k/320k")),
            特殊参数:=参数列表(参数("-aac_nmr_speed", "0~4", "NMR 搜索速度：0=最慢且质量最高，4=最快"), 参数("-aac_ms", "auto/0/1", "M/S 立体声：auto=自动，0=关闭，1=强制"), 参数("-aac_is", "auto/0/1", "强度立体声：auto=自动"), 参数("-aac_pns", "0/1", "感知噪声替代，低码率时可能有益"), 参数("-aac_tns", "0/1", "时域噪声整形"), 参数("-aac_pce", "0/1", "写入 PCE 声道配置")),
            支持说明:="输入：fltp；采样率：7.35~96 kHz 的标准 AAC 档位。",
            旧版命令文本:="nmraac|aac -aac_coder nmr|aac -aac_coder:a nmr"))

        加入编码器(基础("aac.fdk", "FDK AAC", "libfdk_aac", "Fraunhofer FDK AAC；需要含 libfdk_aac 的 FFmpeg。",
            质量参数:=参数列表(参数("-vbr", "质量模式：0=关闭；1~5，越高越清晰、体积越大"), 参数("-b:a", "目标码率：如 64k/96k/128k/192k")),
            特殊参数:=参数列表(参数("-afterburner", "0/1", "质量后处理，默认 1"), 参数("-signaling", "default/implicit/explicit_sbr/explicit_hierarchical", "SBR/PS 信令：default=自动，implicit=兼容，explicit_sbr=显式 SBR，explicit_hierarchical=分层"), 参数("-latm", "0/1", "输出 LATM/LOAS"), 参数("-frame_length", "-1/1024", "AAC 帧长：-1=自动")),
            支持说明:="输入：s16；采样率：8~96 kHz；声道：mono 至 7.1 等布局。"))

        加入编码器(基础("aac.fdk.he", "FDK AAC HE", "libfdk_aac", "HE-AAC，适合低码率 mono/stereo。",
            默认附加参数:=参数列表(参数("-profile:a", "aac_he", "固定为 HE-AAC", 默认值:="aac_he")),
            质量参数:=参数列表(参数("-vbr", "质量模式：0=关闭；1~5，越高越清晰、体积越大"), 参数("-b:a", "目标码率：低码率常用 24k~96k")),
            特殊参数:=参数列表(参数("-afterburner", "0/1", "质量后处理，默认 1"), 参数("-signaling", "default/implicit/explicit_sbr/explicit_hierarchical", "SBR/PS 信令：default=自动，implicit=兼容，explicit_sbr=显式 SBR，explicit_hierarchical=分层"), 参数("-latm", "0/1", "输出 LATM/LOAS")),
            支持说明:="输入：s16；采样率：8~96 kHz；推荐 mono/stereo。",
            旧版命令文本:="libfdk_aac -profile:a aac_he"))

        加入编码器(基础("aac.fdk.hev2", "FDK AAC HE v2", "libfdk_aac", "HE-AAC v2，适合极低码率立体声。",
            默认附加参数:=参数列表(参数("-profile:a", "aac_he_v2", "固定为 HE-AAC v2", 默认值:="aac_he_v2")),
            质量参数:=参数列表(参数("-vbr", "质量模式：0=关闭；1~5，越高越清晰、体积越大"), 参数("-b:a", "目标码率：常用 16k~64k")),
            特殊参数:=参数列表(参数("-afterburner", "0/1", "质量后处理，默认 1"), 参数("-signaling", "default/implicit/explicit_sbr/explicit_hierarchical", "SBR/PS 信令：default=自动，implicit=兼容，explicit_sbr=显式 SBR，explicit_hierarchical=分层"), 参数("-latm", "0/1", "输出 LATM/LOAS")),
            支持说明:="输入：s16；采样率：8~96 kHz；推荐 stereo。",
            旧版命令文本:="libfdk_aac -profile:a aac_he_v2"))

        加入编码器(基础("aac.audiotoolbox", "AudioToolbox AAC", "aac_at", "Apple AudioToolbox AAC 编码器。",
            质量参数:=参数列表(参数("-q:a", "可变质量：配合 -aac_at_mode auto/vbr；范围由 AudioToolbox 决定"), 参数("-b:a", "目标码率：如 128k/192k/256k")),
            特殊参数:=AudioToolbox参数(),
            支持说明:="输入：s16/u8；声道：mono 至 7.1 等布局。"))

        加入编码器(基础("mp3.lame", "LAME MP3", "libmp3lame", "LAME MP3，旧设备兼容性好。",
            质量参数:=参数列表(参数("-q:a", "可变质量：0=最高，9=最低；常用 0~4"), 参数("-b:a", "目标码率：如 128k/192k/320k")),
            特殊参数:=参数列表(参数("-abr", "0/1", "平均码率：设为 1 后配合 -b:a"), 参数("-joint_stereo", "0/1", "联合立体声，适合中低码率"), 参数("-reservoir", "0/1", "启用比特储备，允许帧间分配码率")),
            支持说明:="输入：s16p/fltp/s32p；采样率：8~48 kHz；mono/stereo。"))

        加入编码器(基础("opus.libopus", "Opus", "libopus", "现代网络音频，适合语音、直播和低码率。",
            质量参数:=参数列表(参数("-b:a", "目标码率：语音 24k~64k；音乐 96k~256k；最高约 512k")),
            特殊参数:=参数列表(参数("-vbr", "off/on/constrained", "码率模式：off=固定，on=可变（默认），constrained=受限可变"), 参数("-application", "voip/audio/lowdelay", "用途：voip=语音，audio=音乐，lowdelay=低延迟"), 参数("-frame_duration", "2.5~120 ms", "帧长，默认 20 ms；越短延迟越低、效率越差"), 参数("-packet_loss", "0~100 (%)", "预期丢包率，用于鲁棒性优化"), 参数("-fec", "0/1", "带内前向纠错；丢包率大于 0 时才有意义"), 参数("-dtx", "0/1", "静音时减少发送")),
            支持说明:="输入：s16/flt；采样率：8/12/16/24/48 kHz。"))

        加入编码器(基础("flac.native", "FLAC", "flac", "无损压缩，适合音乐归档。",
            质量参数:=参数列表(参数("-compression_level", "压缩强度：0=最快、体积较大；12=最慢、体积较小")),
            特殊参数:=参数列表(参数("-lpc_type", "none/fixed/levinson/cholesky", "LPC：none=不用，fixed=固定预测，levinson/cholesky=更充分搜索"), 参数("-min_partition_order", "-1/0~8", "最小分区阶数，-1=自动"), 参数("-max_partition_order", "-1/0~8", "最大分区阶数，-1=自动"), 参数("-ch_mode", "auto/indep/left_side/right_side/mid_side", "立体声去相关：auto=自动，indep=独立，mid_side=中侧")),
            支持说明:="输入：s16/s32。"))

        加入编码器(基础("alac.native", "ALAC", "alac", "Apple 无损音频，适合苹果设备。",
            特殊参数:=参数列表(参数("-min_prediction_order", "1~30", "最小预测阶数；越高搜索越充分"), 参数("-max_prediction_order", "1~30", "最大预测阶数；越高搜索越充分")),
            支持说明:="输入：s16p/s32p；声道：mono 至 7.1。"))

        加入编码器(基础("alac.audiotoolbox", "AudioToolbox ALAC", "alac_at", "Apple AudioToolbox ALAC 无损编码器。",
            特殊参数:=AudioToolbox参数(),
            支持说明:="输入：s16/u8。"))

        加入编码器(基础("pcm.s16le", "WAV 16bit", "pcm_s16le", "未压缩 16-bit PCM WAV；码率由采样率、位深和声道数决定。",
            支持说明:="输入：s16。"))
        加入编码器(基础("pcm.s24le", "WAV 24bit", "pcm_s24le", "未压缩 24-bit PCM，常用于制作中间文件。",
            支持说明:="输入：s32；输出：24-bit PCM。"))
        加入编码器(基础("pcm.s32le", "WAV 32bit", "pcm_s32le", "未压缩 32-bit PCM，适合高精度中间文件。",
            支持说明:="输入：s32。"))
        加入编码器(基础("pcm.s64le", "WAV 64bit", "pcm_s64le", "未压缩 64-bit PCM，体积很大，仅适合特殊流程。",
            支持说明:="输入：s64。"))

        加入编码器(基础("pcm.alaw.audiotoolbox", "AudioToolbox PCM A-law", "pcm_alaw_at", "AudioToolbox G.711 A-law，固定 8-bit 对数量化 PCM。",
            特殊参数:=AudioToolbox参数(),
            支持说明:="输入：s16/u8。"))

        加入编码器(基础("pcm.mulaw.audiotoolbox", "AudioToolbox PCM mu-law", "pcm_mulaw_at", "AudioToolbox G.711 mu-law，固定 8-bit 对数量化 PCM。",
            特殊参数:=AudioToolbox参数(),
            支持说明:="输入：s16/u8。"))

        加入编码器(基础("ac3.native", "ATSC A/52A (AC3)", "ac3", "AC-3，适合 DVD、电视和家庭影院兼容。",
            质量参数:=参数列表(参数("-b:a", "固定码率：192k~640k，最高 640k")),
            特殊参数:=参数列表(参数("-dialnorm", "-31~-1 dB", "对白归一化；常用 -31，不主动衰减"), 参数("-dmix_mode", "notindicated/ltrt/loro/dplii", "立体声下混：ltrt=Lt/Rt，loro=Lo/Ro，dplii=Pro Logic II"), 参数("-dsur_mode", "notindicated/on/off", "Dolby Surround 标记"), 参数("-stereo_rematrixing", "0/1", "立体声重矩阵，通常保持开启")),
            支持说明:="输入：fltp；采样率：32/44.1/48 kHz；声道：mono 至 5.1。"))

        加入编码器(基础("eac3.native", "ATSC A/52B (EAC3)", "eac3", "E-AC-3，AC-3 的扩展格式，适合流媒体和高码率。",
            质量参数:=参数列表(参数("-b:a", "固定码率：常用 384k~1536k；更高值取决于封装和播放器")),
            特殊参数:=参数列表(参数("-dialnorm", "-31~-1 dB", "对白归一化；常用 -31，不主动衰减"), 参数("-dmix_mode", "notindicated/ltrt/loro/dplii", "立体声下混：ltrt=Lt/Rt，loro=Lo/Ro，dplii=Pro Logic II"), 参数("-dsur_mode", "notindicated/on/off", "Dolby Surround 标记"), 参数("-stereo_rematrixing", "0/1", "立体声重矩阵，通常保持开启")),
            支持说明:="输入：fltp；采样率：32/44.1/48 kHz；声道：mono 至 5.1。"))

        加入编码器(基础("dts.dca", "DTS Coherent Acoustics", "dca", "DTS 编码器；实验性实现，必要时加 -strict -2。",
            质量参数:=参数列表(参数("-b:a", "固定码率：常用 768k~1536k")),
            特殊参数:=参数列表(参数("-dca_adpcm", "0/1", "ADPCM 模式，仅特殊兼容场景使用"), 参数("-strict", "-2", "允许实验性编码器")),
            支持说明:="输入：s32；采样率：8~48 kHz；声道：mono、stereo、quad、5.0、5.1。"))

        加入编码器(基础("truehd.native", "TrueHD", "truehd", "Dolby TrueHD 无损编码器；实验性实现，必要时加 -strict -2。",
            特殊参数:=参数列表(参数("-max_interval", "8~128", "新 header 最大间隔帧数；越大 header 越少"), 参数("-lpc_type", "levinson/cholesky", "LPC 算法；cholesky 搜索更充分"), 参数("-prediction_order", "0~4", "预测阶数；越高搜索越充分"), 参数("-strict", "-2", "允许实验性编码器")),
            支持说明:="输入：s16p/s32p；采样率：44.1~192 kHz；声道：mono 至 5.1。"))

        加入编码器(基础("ilbc.audiotoolbox", "AudioToolbox iLBC", "ilbc_at", "AudioToolbox iLBC，低码率语音编码。",
            特殊参数:=AudioToolbox参数(),
            支持说明:="输入：s16/u8。"))

        加入编码器(基础("tta.native", "True Audio", "tta", "TTA 无损音频编码器。",
            支持说明:="输入：u8/s16/s32。"))

        加入编码器(基础("vorbis.libvorbis", "Vorbis (ogg)", "libvorbis", "OGG/Vorbis 兼容场景。",
            质量参数:=参数列表(参数("-q:a", "可变质量：-1~10；越高越清晰、体积越大"), 参数("-b:a", "目标码率：未设置 -q:a 时用于码率控制")),
            特殊参数:=参数列表(参数("-iblock", "-15~0", "瞬态块偏置；越低越强调瞬态保护")),
            支持说明:="输入：fltp。"))

        加入编码器(基础("ra.real144", "RealAudio 1.0 (14.4K)", "real_144", "仅用于旧 RealAudio 兼容；固定 14.4k，能力有限。"))

        加入编码器(基础("wavpack.native", "WavPack", "wavpack", "WavPack 无损/混合编码；默认无损，混合模式请使用自定义参数。",
            特殊参数:=参数列表(参数("-joint_stereo", "0/1/auto", "联合立体声：auto=自动"), 参数("-optimize_mono", "0/1", "仅对单声道有效")),
            支持说明:="输入：u8p/s16p/s32p/fltp。"))

        加入编码器(基础("mp2.twolame", "LAME MP2", "libtwolame", "MP2，适合旧广播和兼容场景。",
            质量参数:=参数列表(参数("-b:a", "固定码率：常用 128k~384k")),
            特殊参数:=参数列表(参数("-mode", "auto/stereo/joint_stereo/dual_channel/mono", "声道模式：auto=自动，joint_stereo=联合立体声，dual_channel=双单声道"), 参数("-psymodel", "-1/0~4", "心理声学模型；-1=自动，数值越高越复杂"), 参数("-error_protection", "0/1", "CRC 错误保护，略增体积"), 参数("-energy_levels", "0/1", "写入能量级别信息")),
            支持说明:="输入：flt/fltp/s16/s16p；采样率：16~48 kHz；mono/stereo。"))

        加入编码器(基础("amr.nb.opencore", "AMR-NB", "libopencore_amrnb", "AMR-NB 窄带语音编码器。",
            质量参数:=参数列表(参数("-b:a", "离散码率：4.75k/5.15k/5.9k/6.7k/7.4k/7.95k/10.2k/12.2k")),
            特殊参数:=参数列表(参数("-dtx", "0/1", "静音时发送舒适噪声以降低码率")),
            支持说明:="输入：s16；通常为 8 kHz mono。"))

        加入编码器(基础("amr.wb.vo", "AMR-WB", "libvo_amrwbenc", "AMR-WB 宽带语音编码器。",
            质量参数:=参数列表(参数("-b:a", "离散码率：6.6k/8.85k/12.65k/14.25k/15.85k/18.25k/19.85k/23.05k/23.85k")),
            特殊参数:=参数列表(参数("-dtx", "0/1", "静音时发送舒适噪声以降低码率")),
            支持说明:="输入：s16；通常为 16 kHz mono。"))



    End Sub

    Private Shared Sub 加入编码器(数据 As 音频编码器数据)
        编码器列表.Add(数据)
        ID索引(数据.私有ID) = 数据
        显示名称索引(数据.显示名称) = 数据

        For Each 旧版命令 In If(数据.旧版命令文本, "").Split({"|"c}, StringSplitOptions.RemoveEmptyEntries)
            Dim 命令文本 = 旧版命令.Trim()
            If 命令文本 <> "" Then 旧版命令索引(命令文本) = 数据
        Next
        If 数据.命令行编码器名 <> "" AndAlso Not 旧版命令索引.ContainsKey(数据.命令行编码器名) Then 旧版命令索引(数据.命令行编码器名) = 数据
    End Sub

    Private Shared Function 基础(私有ID As String,
                               显示名称 As String,
                               命令行编码器名 As String,
                               描述 As String,
                               Optional 质量参数 As List(Of 音频编码器参数数据) = Nothing,
                               Optional 特殊参数 As List(Of 音频编码器参数数据) = Nothing,
                                Optional 默认附加参数 As List(Of 音频编码器参数数据) = Nothing,
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
            参数("-aac_at_mode", "auto/cbr/abr/cvbr/vbr", "码率模式：质量用 auto/vbr/cvbr 并设 -q:a；目标码率用 cbr/abr 并设 -b:a"),
            参数("-aac_at_quality", "0~2", "质量/速度级别；0=默认"))
    End Function

    Public Class 音频编码器数据
        Public Property 私有ID As String = ""
        Public Property 显示名称 As String = ""
        Public Property 命令行编码器名 As String = ""
        Public Property 描述 As String = ""
        Public Property 质量参数列表 As New List(Of 音频编码器参数数据)
        Public Property 特殊参数列表 As New List(Of 音频编码器参数数据)
        Public Property 默认附加参数列表 As New List(Of 音频编码器参数数据)
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
                添加参数分组(片段, "质量/码率", 质量参数列表)
                添加参数分组(片段, "进阶参数", 特殊参数列表)
                If 支持说明 <> "" Then 片段.Add("支持：" & 支持说明)
                If 私有ID <> "" Then 片段.Add("ID：" & 私有ID)
                Return String.Join(vbCrLf & vbCrLf, 片段)
            End Get
        End Property

        Private Shared Sub 添加参数分组(片段 As List(Of String), 标题 As String, 参数列表 As List(Of 音频编码器参数数据))
            If 参数列表 Is Nothing OrElse 参数列表.Count = 0 Then Exit Sub
            Dim 行 As New List(Of String) From {标题 & "："}
            For Each 参数项 In 参数列表
                If 参数项 Is Nothing Then Continue For
                Dim 文本 = 参数项.转说明文本()
                If 文本 <> "" Then 行.Add("  " & 文本)
            Next
            If 行.Count > 1 Then 片段.Add(String.Join(vbCrLf, 行))
        End Sub
    End Class

    Public Class 音频编码器参数数据
        Public Property 参数名 As String = ""
        Public Property 值范围说明 As String = ""
        Public Property 说明 As String = ""
        Public Property 默认值 As String = ""

        Public Function 转说明文本() As String
            Dim 范围说明 = 补全通用值说明(值范围说明)
            Dim 文本 = $"{参数名} {范围说明}".Trim()
            If 说明 <> "" Then 文本 &= $"（{说明}）"
            Return 文本
        End Function

        Public Function 转命令行文本() As String
            If 参数名 = "" Then Return ""
            If 默认值 = "" Then Return 参数名
            Return $"{参数名} {默认值}"
        End Function

        Private Shared Function 补全通用值说明(值范围说明 As String) As String
            Dim 文本 = If(值范围说明, "").Trim()
            If 文本 = "" Then Return ""
            If 文本.Contains("0/1") AndAlso
               Not 文本.Contains("0=关闭") AndAlso
               Not 文本.Contains("0 关闭") AndAlso
               Not 文本.Contains("1 不循环") Then
                文本 &= "；0=关闭，1=开启"
            End If
            Return 文本
        End Function
    End Class

End Class
