Public Class Form_v6_参数面板_色彩管理

    Private Sub Form_v6_参数面板_色彩管理_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        初始化下拉框浮动提示()
    End Sub

    Private Sub 初始化下拉框浮动提示()
        设置下拉框浮动提示(MCB_像素格式预先转换, {
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "yuv420p", .ToolTipText = "YUV 4:2:0 8-bit 平面格式，兼容性最好。SDR 常用；HDR 会损失精度，不推荐作为 HDR 中间格式。"},
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "yuv420p10le", .ToolTipText = "YUV 4:2:0 10-bit 平面格式，HDR10/PQ 和 HLG 常用，软件编码器兼容性较好。"},
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "yuv422p", .ToolTipText = "YUV 4:2:2 8-bit 平面格式，保留更多色度采样，常用于采集或中间流程。"},
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "yuv422p10le", .ToolTipText = "YUV 4:2:2 10-bit 平面格式，适合高质量中间流程或支持 4:2:2 的专业编码。"},
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "yuv444p", .ToolTipText = "YUV 4:4:4 8-bit 平面格式，不做色度抽样，文件和码率压力更大。"},
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "yuv444p10le", .ToolTipText = "YUV 4:4:4 10-bit 平面格式，高质量中间流程使用；编码器和播放器兼容性要求更高。"},
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "p010le", .ToolTipText = "10-bit 4:2:0 半平面格式，常见于硬件编码器和 HDR 工作流，如 NVENC、QSV、AMF。"}
        })

        设置下拉框浮动提示(MCB_色彩管理_选择滤镜, {
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "zscale", .ToolTipText = "基于 zimg 的 CPU 滤镜，可做矩阵、原色、传输特性和范围转换。适合稳定的标准色彩转换。"},
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "libplacebo", .ToolTipText = "基于 libplacebo 的 GPU 渲染滤镜。支持输出色彩配置、HDR 峰值检测和色调映射，HDR 转 SDR 或复杂映射更适合它。"}
        })

        设置下拉框浮动提示(MCB_色彩管理_矩阵系数, {
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "auto", .ToolTipText = "自动或沿用输入；当前生成命令时会视作不显式指定。"},
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "bt709", .ToolTipText = "BT.709 亮度/色度矩阵，HD SDR 视频最常见。"},
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "bt2020nc", .ToolTipText = "BT.2020 非恒定亮度矩阵，HDR10/PQ 和 HLG 最常用。zscale 中对应 2020_ncl。"},
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "bt2020c", .ToolTipText = "BT.2020 恒定亮度矩阵，兼容性和实际使用少于 bt2020nc，只有明确需要时再选。"},
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "rgb", .ToolTipText = "RGB 色彩空间，不使用常规 YUV 亮度/色度矩阵。适合 RGB 流程，普通 YUV 视频不要误选。"},
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "gbr", .ToolTipText = "GBR 平面 RGB 格式常用标记，适合 RGB/GBR 流程，普通 YUV 视频不要误选。"},
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "bt470bg", .ToolTipText = "BT.470BG / BT.601 625 行矩阵，PAL/SECAM SD 内容常见。"},
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "smpte170m", .ToolTipText = "SMPTE 170M / BT.601 525 行矩阵，NTSC SD 内容常见。"},
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "smpte240m", .ToolTipText = "SMPTE 240M 矩阵，早期 HDTV 标准，现代内容较少使用。"},
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "fcc", .ToolTipText = "FCC 历史矩阵，旧素材或兼容性场景才可能需要。"},
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "ictcp", .ToolTipText = "ICtCp 色彩表示，BT.2100 HDR/WCG 相关。需要滤镜、编码器和播放器链路明确支持。"},
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "ycgco", .ToolTipText = "YCgCo 颜色变换，部分编码或无损/中间流程可能使用，普通视频输出较少使用。"},
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "xyz", .ToolTipText = "CIE XYZ / 数字影院相关颜色表示，通常只用于特殊或影院中间流程。"}
        })

        设置下拉框浮动提示(MCB_色彩管理_色域, {
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "auto", .ToolTipText = "自动或沿用输入；当前生成命令时会视作不显式指定。"},
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "bt709", .ToolTipText = "BT.709 原色，HD SDR 视频最常见，也常用于网络 SDR 输出。"},
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "bt2020", .ToolTipText = "BT.2020 宽色域，HDR10/PQ 和 HLG 的常用原色。通常搭配 bt2020nc、smpte2084 或 arib-std-b67。"},
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "smpte428", .ToolTipText = "SMPTE 428 / D-Cinema 相关原色，常与影院或 XYZ 流程相关。"},
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "smpte431", .ToolTipText = "SMPTE 431 / DCI-P3 影院原色，白点不同于常见显示器 P3-D65。"},
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "smpte432", .ToolTipText = "SMPTE 432 / Display P3 原色，常见于 P3-D65 显示和部分 Apple 生态内容。"},
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "film", .ToolTipText = "胶片相关原色，主要用于历史或特殊素材。"},
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "bt470m", .ToolTipText = "BT.470M 原色，旧 NTSC 1953 相关标准。"},
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "bt470bg", .ToolTipText = "BT.470BG 原色，旧 PAL/SECAM 625 行内容常见。"},
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "smpte170m", .ToolTipText = "SMPTE 170M 原色，NTSC/BT.601 525 行 SD 视频常见。"},
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "smpte240m", .ToolTipText = "SMPTE 240M 原色，早期 HDTV 标准，现代内容较少使用。"},
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "jedec-p22", .ToolTipText = "JEDEC P22 荧光粉原色，旧 CRT/显示标准相关。"},
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "ebu3213", .ToolTipText = "EBU Tech 3213 原色，欧洲广播旧标准相关。"}
        })

        设置下拉框浮动提示(MCB_色彩管理_传输特性, {
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "auto", .ToolTipText = "自动或沿用输入；当前生成命令时会视作不显式指定。"},
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "bt709", .ToolTipText = "BT.709 SDR 传输特性，HD SDR 视频最常见。"},
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "bt2020-10", .ToolTipText = "BT.2020 10-bit 传输特性，不等同于 HDR PQ/HLG；通常用于 BT.2020 SDR 或兼容流程。"},
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "bt2020-12", .ToolTipText = "BT.2020 12-bit 传输特性，不等同于 HDR PQ/HLG；用于 12-bit BT.2020 流程。"},
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "smpte2084", .ToolTipText = "SMPTE ST 2084 PQ，HDR10、HDR10+、Dolby Vision 基础层常用。通常搭配 bt2020、bt2020nc、tv 和 10-bit 像素格式。"},
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "bt470m", .ToolTipText = "BT.470M 传输特性，旧 NTSC 相关标准。"},
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "bt470bg", .ToolTipText = "BT.470BG 传输特性，旧 PAL/SECAM 相关标准。"},
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "log", .ToolTipText = "对数传输曲线，主要用于特殊中间流程或旧素材；普通成片很少直接使用。"},
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "log_sqrt", .ToolTipText = "平方根对数传输曲线，特殊或旧式流程使用，普通成片很少直接使用。"},
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "linear", .ToolTipText = "线性光传输。常作为色调映射或合成的中间状态，直接输出给普通播放器通常不合适。"},
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "bt1361e", .ToolTipText = "BT.1361 扩展色域传输特性，历史和兼容性用途为主。"},
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "iec61966-2-1", .ToolTipText = "sRGB 传输特性，常见于图片、网页、桌面图形和部分全范围 RGB 流程。"},
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "iec61966-2-4", .ToolTipText = "IEC 61966-2-4 / xvYCC 相关传输特性，普通视频输出较少使用。"},
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "smpte170m", .ToolTipText = "SMPTE 170M 传输特性，NTSC/BT.601 SD 视频常见。"},
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "smpte240m", .ToolTipText = "SMPTE 240M 传输特性，早期 HDTV 标准，现代内容较少使用。"},
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "gamma22", .ToolTipText = "固定 2.2 gamma，常见于显示和旧素材假设，但不是标准 HDR 曲线。"},
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "gamma28", .ToolTipText = "固定 2.8 gamma，常见于旧 PAL/SECAM 相关假设。"},
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "arib-std-b67", .ToolTipText = "ARIB STD-B67 HLG，广播 HDR 常用。通常搭配 bt2020、bt2020nc、tv 和 10-bit 像素格式。"}
        })

        设置下拉框浮动提示(MCB_色彩管理_色彩范围, {
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "tv 有限 16~235", .ToolTipText = "有限范围，也称 TV/MPEG/studio range。大多数 SDR、HDR10、HLG 视频都使用此范围。"},
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "pc 全范围 0~255", .ToolTipText = "全范围，也称 PC/JPEG/full range。常见于 RGB、截图、部分相机或中间文件；误用会导致黑位和白位错误。"}
        })

        设置下拉框浮动提示(MCB_色彩管理_色调映射算法, {
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "auto", .ToolTipText = "由 libplacebo 根据内部启发式自动选择算法，适合不知道该选哪一个时使用。"},
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "clip", .ToolTipText = "不做真正的色调映射，只裁剪超出目标范围的亮度和颜色。速度快，但高光信息会直接丢失。"},
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "st2094-40", .ToolTipText = "SMPTE ST 2094-40 EETF，面向 HDR10+ 动态元数据的曲线。需要源信息准确才有意义。"},
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "st2094-10", .ToolTipText = "SMPTE ST 2094-10 EETF，会考虑平均亮度和最大最小亮度，适合有相关元数据的 HDR 内容。"},
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "bt.2390", .ToolTipText = "ITU-R BT.2390 推荐的高光 roll-off 曲线，常用于 HDR 映射到较低峰值显示或 SDR。"},
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "bt.2446a", .ToolTipText = "ITU-R BT.2446 方法 A，面向制作良好的 HDR 源，可用于正向和反向色调映射。"},
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "spline", .ToolTipText = "简单样条曲线，在 PQ 空间使用一个枢轴点压缩亮度；可用于正向和反向色调映射。"},
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "reinhard", .ToolTipText = "经典全局非线性色调映射，结果稳定但可能压低整体对比度。"},
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "mobius", .ToolTipText = "Reinhard 的改进形式，暗部附近保留线性段，默认设置在色彩准确性和高光保留之间较均衡。"},
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "hable", .ToolTipText = "电影感曲线，暗部和高光细节保留较好，但会明显改变平均亮度和观感。"},
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "gamma", .ToolTipText = "用幂函数拟合源和目标范围，细节保留较稳，但画面可能显得偏灰或不够鲜亮。"},
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "linear", .ToolTipText = "在 PQ 空间线性拉伸输入到输出范围，细节保留直接，但平均亮度变化可能很明显。"}
        })

        设置下拉框浮动提示(MCB_色彩管理_色彩空间操作方式, {
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "写入元数据并转换", .ToolTipText = "同时生成转换滤镜和输出流色彩元数据。标准转色彩空间时推荐此项，播放器更容易按目标标准识别。"},
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "仅写入元数据", .ToolTipText = "只写 -colorspace、-color_primaries、-color_trc、-color_range，不改变画面数值。适合修正缺失或错误标签。"},
            New LakeUI.ModernComboBox.ToolTipEntry With {.ItemText = "仅转换", .ToolTipText = "只生成转换滤镜，不写输出流色彩元数据。适合后续参数或封装流程另行处理标签的情况。"}
        })
    End Sub

    Private Shared Sub 设置下拉框浮动提示(组合框 As LakeUI.ModernComboBox, 提示列表 As LakeUI.ModernComboBox.ToolTipEntry())
        组合框.ItemToolTips.Clear()
        组合框.ItemToolTips.AddRange(提示列表)
    End Sub

End Class
