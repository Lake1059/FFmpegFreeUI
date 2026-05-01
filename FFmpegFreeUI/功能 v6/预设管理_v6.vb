Public Class 预设管理_v6

    Private Shared ReadOnly separator As String() = {","}

    Public Shared Property 音频编码器排序表 As New List(Of String) From {
        "",
        "copy",
        "-an",
        "aac",
        "libfdk_aac",
        "libfdk_aac -profile:a aac_he",
        "libfdk_aac -profile:a aac_he_v2",
        "libmp3lame",
        "libopus",
        "flac",
        "alac",
        "pcm_s16le",
        "pcm_s24le",
        "pcm_s32le",
        "pcm_s64le",
        "ac3",
        "eac3",
        "dca",
        "truehd",
        "tta",
        "libvorbis",
        "real_144",
        "wavpack",
        "libtwolame",
        "libopencore_amrnb",
        "libvo_amrwbenc"
    }

    Public Shared Sub 显示预设(a As 预设数据类型, ui As Form_v6_参数面板)

    End Sub

    Public Shared Sub 储存预设(ByRef a As 预设数据类型, ui As Form_v6_参数面板)

    End Sub

    Public Shared Sub 重置面板(ui As Form_v6_参数面板)

    End Sub

    Public Shared Function 将预设数据转换为命令行(a As 预设数据_v6, 输入文件 As String, 输出文件 As String) As String

    End Function

    Public Shared Sub 显示参数总览(MTB As LakeUI.ModernTextBox, a As 预设数据_v6)

    End Sub



End Class
