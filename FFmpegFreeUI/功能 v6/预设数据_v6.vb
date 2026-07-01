<Serializable()>
Public Class 预设数据_v6
    Public Property 预设备注 As String = ""
    Public Property 预设文件版本 As Integer = 6
    <System.Text.Json.Serialization.JsonIgnore()>
    Public Property 额外保存输出位置 As Boolean = False
    '==================================================
    Public Property 输出容器 As String = ""
    '==================================================
    Enum 输出文件参数使用方法
        正常使用 = 0
        不附加 = 1
        声明丢弃输出 = 2
    End Enum
    Public Property 输出_输出文件参数使用方法 As 输出文件参数使用方法 = 输出文件参数使用方法.正常使用
    Public Property 输出目录 As String = ""
    Enum 自动命名选项
        不使用自动命名 = 0
        附加_递增时间戳 = 1
        附加_递增数字 = 2
        附加_3FUI = 3
        常规压片_附加编码器和质量参数 = 4
        附加_随机8位数字 = 5
        附加_随机8位字母 = 6
        附加_随机8位数字和字母组合 = 7
        附加_随机16位数字 = 8
        附加_随机16位字母 = 9
        附加_随机16位数字和字母组合 = 10
        附加_2位结尾序号 = 11
        附加_3位结尾序号 = 12
    End Enum
    Public Property 输出_自动命名选项 As 自动命名选项 = 自动命名选项.附加_递增时间戳
    Public Property 输出命名_开头文本 As String = ""
    Public Property 输出命名_替代文本 As String = ""
    Public Property 输出命名_结尾文本 As String = ""
    Public Property 输出命名_保留创建时间 As Boolean = False
    Public Property 输出命名_保留修改时间 As Boolean = False
    Public Property 输出命名_保留访问时间 As Boolean = False
    '==================================================
    Public Property 解码参数_解码器 As String = ""
    Public Property 解码参数_CPU解码线程数 As String = ""
    Public Property 解码参数_解码数据格式 As String = ""
    Public Property 解码参数_指定硬件的参数名 As String = ""
    Public Property 解码参数_指定硬件的参数 As String = ""
    '==================================================
    Enum 视频编码器类型
        未选择 = 0
        视频 = 1
        图片 = 2
    End Enum
    Public Property 视频参数_编码器_类型 As 视频编码器类型 = 视频编码器类型.未选择
    Public Property 视频参数_编码器_分类名称 As String = ""
    Public Property 视频参数_编码器_具体编码 As String = ""
    Public Property 视频参数_编码器_图片编码器质量值 As String = ""
    Public Property 视频参数_编码器_编码预设 As String = ""
    Public Property 视频参数_编码器_配置文件 As String = ""
    Public Property 视频参数_编码器_场景优化 As String = ""
    Public Property 视频参数_编码器_gpu As String = ""
    Public Property 视频参数_编码器_threads As String = ""
    '==================================================
    Public Property 视频参数_分辨率 As String = ""
    Public Property 视频参数_分辨率自动计算_宽度 As String = ""
    Public Property 视频参数_分辨率自动计算_高度 As String = ""
    Public Property 视频参数_分辨率_裁剪滤镜参数 As String = ""
    '==================================================
    Public Property 视频参数_帧速率 As String = ""
    '==================================================
    Public Property 视频参数_抽帧_max As String = ""
    Public Property 视频参数_抽帧_keep As String = ""
    Public Property 视频参数_抽帧_hi As String = ""
    Public Property 视频参数_抽帧_lo As String = ""
    Public Property 视频参数_抽帧_frac As String = ""
    '==================================================
    Public Property 视频参数_插帧_目标帧率 As String = ""
    Public Property 视频参数_插帧_插帧模式 As String = ""
    Public Property 视频参数_插帧_运动估计模式 As String = ""
    Public Property 视频参数_插帧_运动估计算法 As String = ""
    Public Property 视频参数_插帧_运动补偿模式 As String = ""
    Public Property 视频参数_插帧_可变块大小的运动补偿 As Boolean = False
    Public Property 视频参数_插帧_块大小 As String = ""
    Public Property 视频参数_插帧_搜索范围 As String = ""
    Public Property 视频参数_插帧_场景变化检测强度 As String = ""
    '==================================================
    'tmix滤镜
    Public Property 视频参数_动态模糊_连续混合帧数 As String = ""
    Public Property 视频参数_动态模糊_每帧权重 As String = ""
    Public Property 视频参数_动态模糊_输出缩放系数 As String = ""
    Public Property 视频参数_动态模糊_处理颜色平面 As String = ""
    '==================================================
    Class 超分数据单片结构
        Public Property 目标宽度 As String = ""
        Public Property 目标高度 As String = ""
        Public Property 上采样算法 As String = ""
        Public Property 下采样算法 As String = ""
        Public Property 抗振铃强度 As String = ""
        Public Property 着色器文件路径 As String = ""
    End Class
    ' 当策略组中有条目时，按照组依次生成滤镜，不管直接面板
    ' 反之策略组是空的，直接按照面板生成单条滤镜
    Public Property 视频参数_超分_直接面板 As 超分数据单片结构
    Public Property 视频参数_超分_滤镜叠加策略组 As 超分数据单片结构()
    '==================================================
    Enum 降噪方式
        未选择 = 0
        hqdn3d = 1
        nlmeans = 2
        atadenoise = 3
        bm3d = 4
        bilateral_cuda = 5
    End Enum
    Public Property 视频参数_降噪_方式 As 降噪方式 = 降噪方式.未选择
    Public Property 视频参数_降噪_参数1 As String = ""
    Public Property 视频参数_降噪_参数2 As String = ""
    Public Property 视频参数_降噪_参数3 As String = ""
    Public Property 视频参数_降噪_参数4 As String = ""
    '==================================================
    Enum 锐化方式
        未选择 = 0
        cas = 1
        unsharp = 2
    End Enum
    Public Property 视频参数_锐化_方式 As 锐化方式 = 锐化方式.未选择
    Public Property 视频参数_锐化_参数1 As String = ""
    Public Property 视频参数_锐化_参数2 As String = ""
    Public Property 视频参数_锐化_参数3 As String = ""
    '==================================================
    Enum 胶片颗粒方式
        未选择 = 0
        noise_全平面动态均匀颗粒 = 1        ' noise=alls={参数1}:allf=t+u:all_seed={参数2}
        noise_亮度为主动态颗粒 = 2        ' noise=c0s={参数1}:c0f=t+u:c1s={参数2}:c1f=t+u:c2s={参数2}:c2f=t+u:all_seed={参数3}
        noise_柔和平均颗粒 = 3        ' noise=alls={参数1}:allf=t+a+u:all_seed={参数2}
        libplacebo_应用片源胶片颗粒元数据 = 4        ' libplacebo=apply_filmgrain=true
    End Enum
    Public Property 视频参数_胶片颗粒_方式 As 胶片颗粒方式 = 胶片颗粒方式.未选择
    Public Property 视频参数_胶片颗粒_参数1 As String = ""
    Public Property 视频参数_胶片颗粒_参数2 As String = ""
    Public Property 视频参数_胶片颗粒_参数3 As String = ""
    Public Property 视频参数_胶片颗粒_参数4 As String = ""
    '==================================================
    Enum 平滑断层方式
        未选择 = 0
        deband_标准去色带 = 1        ' deband=1thr={参数1}:2thr={参数1}:3thr={参数1}:range={参数2}:direction={参数3}:blur=1:coupling={参数4}
        deband_强力去色带 = 2        ' deband=1thr={参数1}:2thr={参数1}:3thr={参数1}:range={参数2}:direction={参数3}:blur=1:coupling={参数4}
        gradfun_快速渐变平滑 = 3        ' gradfun=strength={参数1}:radius={参数2}
        libplacebo_GPU去色带加颗粒 = 4        ' libplacebo=deband=true:deband_iterations={参数1}:deband_threshold={参数2}:deband_radius={参数3}:deband_grain={参数4}
    End Enum
    Public Property 视频参数_平滑断层_方式 As 平滑断层方式 = 平滑断层方式.未选择
    Public Property 视频参数_平滑断层_参数1 As String = ""
    Public Property 视频参数_平滑断层_参数2 As String = ""
    Public Property 视频参数_平滑断层_参数3 As String = ""
    Public Property 视频参数_平滑断层_参数4 As String = ""
    '==================================================
    Enum 扫描方式
        未选择 = 0
        yadif_单帧输入_自动场序_空间检查 = 1        ' yadif=mode=send_frame:parity=auto:deint=all
        yadif_单帧输入_顶场优先_空间检查 = 2        ' yadif=mode=send_frame:parity=tff:deint=all
        yadif_单帧输入_底场优先_空间检查 = 3        ' yadif=mode=send_frame:parity=bff:deint=all
        tinterlace_顶场优先 = 4        ' tinterlace=mode=interleave_top
        tinterlace_底场优先 = 5        ' tinterlace=mode=interleave_bottom
        NTSC_标准IVTC_胶片32Pulldown转逐行 = 6        ' fieldmatch,yadif=deint=interlaced,decimate
        NTSC_纯隔行_非胶片转逐行 = 7        ' yadif=mode=send_field:parity=auto:deint=all
        NTSC_自动检测Pulldown至25fps = 8        ' pullup=jl=1:jr=1,fps=25
        PAL_标准反交错 = 9        ' yadif=mode=send_frame:parity=auto:deint=all
        PAL_标准反交错_双倍帧率 = 10        ' yadif=mode=send_field:parity=auto:deint=all
        PAL_高质量反交错 = 11        ' bwdif=mode=send_frame:parity=auto:deint=all
        PAL_高质量反交错_双倍帧率 = 12        ' bwdif=mode=send_field:parity=auto:deint=all
        yadif_cuda_自动场序 = 13        ' yadif_cuda=mode=send_frame:parity=auto:deint=all
        bwdif_cuda_自动场序 = 14        ' bwdif_cuda=mode=send_frame:parity=auto:deint=all
    End Enum
    Public Property 视频参数_处理扫描方式 As 扫描方式 = 扫描方式.未选择
    '==================================================
    Enum 画面翻转角度
        未选择 = 0
        顺时针旋转90度 = 1        ' transpose=1
        顺时针旋转180度 = 2        ' transpose=1,transpose=1
        顺时针旋转270度 = 3        ' transpose=1,transpose=1,transpose=1
        逆时针旋转90度 = 4        ' transpose=2
        逆时针旋转180度 = 5        ' transpose=2,transpose=2
        逆时针旋转270度 = 6        ' transpose=2,transpose=2,transpose=2
    End Enum
    Public Property 视频参数_画面翻转_角度翻转 As 画面翻转角度 = 画面翻转角度.未选择
    Enum 画面翻转镜像
        未选择 = 0
        水平镜像 = 1        ' hflip
        垂直镜像 = 2        ' vflip
    End Enum
    Public Property 视频参数_画面翻转_镜像翻转 As 画面翻转镜像 = 画面翻转镜像.未选择
    '==================================================
    Enum 烧字幕滤镜
        未选择 = 0
        subtitles = 1
        ass = 2
    End Enum
    Public Property 视频参数_烧录字幕_滤镜选择 As 烧字幕滤镜 = 烧字幕滤镜.未选择
    Enum 烧字幕来源
        未选择 = 0
        外部字幕文件 = 1
        内嵌的流 = 2
    End Enum
    Public Property 视频参数_烧录字幕_字幕来源是外部文件 As 烧字幕来源 = 烧字幕来源.未选择
    Enum 烧字幕格式
        未选择 = 0
        SRT = 1
        ASS = 2
        SSA = 3
    End Enum
    Public Property 视频参数_烧录字幕_字幕格式优先级 As New List(Of 烧字幕格式)
    Public Property 视频参数_烧录字幕_外部字幕文件名 As String = ""
    Public Property 视频参数_烧录字幕_外部字幕文件夹位置 As String = ""
    Public Property 视频参数_烧录字幕_指定内嵌的流 As String = ""
    Public Property 视频参数_烧录字幕_基本样式_名称 As String = ""
    Public Property 视频参数_烧录字幕_基本样式_大小 As Double
    Public Property 视频参数_烧录字幕_基本样式_粗体 As Boolean = False
    Public Property 视频参数_烧录字幕_基本样式_斜体 As Boolean = False
    Public Property 视频参数_烧录字幕_基本样式_下划线 As Boolean = False
    Public Property 视频参数_烧录字幕_基本样式_删除线 As Boolean = False
    Enum 烧字幕边框样式
        未选择 = 0
        边框_阴影 = 1
        背景框 = 2
    End Enum
    Public Property 视频参数_烧录字幕_边框样式 As 烧字幕边框样式 = 烧字幕边框样式.未选择
    Public Property 视频参数_烧录字幕_描边宽度 As String = ""
    Public Property 视频参数_烧录字幕_阴影距离 As String = ""
    Class 烧字幕专用颜色类型
        Public Property 已设置 As Boolean = False
        Public Property A As Integer = 255
        Public Property R As Integer = 0
        Public Property G As Integer = 0
        Public Property B As Integer = 0
    End Class
    Public Property 视频参数_烧录字幕_主要颜色 As New 烧字幕专用颜色类型
    Public Property 视频参数_烧录字幕_次要颜色 As New 烧字幕专用颜色类型
    Public Property 视频参数_烧录字幕_描边颜色 As New 烧字幕专用颜色类型
    Public Property 视频参数_烧录字幕_背景颜色 As New 烧字幕专用颜色类型
    Public Property 视频参数_烧录字幕_字体文件夹 As String = ""
    Enum 烧字幕对齐方位
        未选择 = 0
        左下角 = 1
        底部居中 = 2
        右下角 = 3
        左居中 = 4
        正中 = 5
        右居中 = 6
        左上角 = 7
        顶部居中 = 8
        右上角 = 9
    End Enum
    Public Property 视频参数_烧录字幕_对齐方位 As 烧字幕对齐方位 = 烧字幕对齐方位.未选择
    Public Property 视频参数_烧录字幕_垂直边距 As String = ""
    Public Property 视频参数_烧录字幕_左边距 As String = ""
    Public Property 视频参数_烧录字幕_右边距 As String = ""
    Public Property 视频参数_烧录字幕_字距 As String = ""
    Public Property 视频参数_烧录字幕_行距 As String = ""
    Public Property 视频参数_烧录字幕_补充样式 As String = ""
    Public Property 视频参数_烧录字幕_自己写滤镜取代所有设置 As String = ""
    '==================================================
    Enum 视频全局质量控制方式
        未选择 = 0
        CRF = 1
        VBR = 2
        CQP = 4
        CBR = 5
        TPE = 6
    End Enum
    Public Property 视频参数_比特率_控制方式 As 视频全局质量控制方式 = 视频全局质量控制方式.未选择
    Public Property 视频参数_质量控制_参数名 As String = ""
    Public Property 视频参数_质量控制_值 As String = ""
    Public Property 视频参数_比特率_基础 As String = ""
    Public Property 视频参数_比特率_最低值 As String = ""
    Public Property 视频参数_比特率_最高值 As String = ""
    Public Property 视频参数_比特率_缓冲区 As String = ""
    Public Property 视频参数_质量控制_进阶参数集 As String = ""
    '==================================================
    Public Property 视频参数_色彩管理_像素格式 As String = ""
    Public Property 视频参数_色彩管理_像素格式预先转换 As String = ""
    Public Property 视频参数_色彩管理_滤镜选择 As String = ""
    Public Property 视频参数_色彩管理_矩阵系数 As String = ""
    Public Property 视频参数_色彩管理_色域 As String = ""
    Public Property 视频参数_色彩管理_传输特性 As String = ""
    Public Property 视频参数_色彩管理_范围 As String = ""
    Public Property 视频参数_色彩管理_色调映射算法 As String = ""
    Public Property 视频参数_色彩管理_处理方式 As String = ""
    Public Property 视频参数_色彩管理_启用调整亮度 As Boolean = False
    Public Property 视频参数_色彩管理_亮度 As String = ""
    Public Property 视频参数_色彩管理_启用调整对比度 As Boolean = False
    Public Property 视频参数_色彩管理_对比度 As String = ""
    Public Property 视频参数_色彩管理_启用调整饱和度 As Boolean = False
    Public Property 视频参数_色彩管理_饱和度 As String = ""
    Public Property 视频参数_色彩管理_启用调整伽马 As Boolean = False
    Public Property 视频参数_色彩管理_伽马 As String = ""

    '==================================================
    Public Property 视频参数_视频帧服务器_使用AviSynth As Boolean = False
    Public Property 视频参数_视频帧服务器_avs脚本文件 As String = ""
    Public Property 视频参数_视频帧服务器_使用VapourSynth As Boolean = False
    Public Property 视频参数_视频帧服务器_vpy脚本文件 As String = ""
    '==================================================
    Public Property 音频参数_编码器_代号 As String = ""
    Public Property 音频参数_比特率 As String = ""
    Public Property 音频参数_质量参数名 As String = ""
    Public Property 音频参数_质量值 As String = ""
    Public Property 音频参数_声道数 As String = ""
    Public Property 音频参数_位深度 As String = ""
    Public Property 音频参数_采样率 As String = ""
    Public Property 音频参数_响度标准化_启用调整目标响度 As Boolean = False
    Public Property 音频参数_响度标准化_目标响度 As String = ""
    Public Property 音频参数_响度标准化_启用调整动态范围 As Boolean = False
    Public Property 音频参数_响度标准化_动态范围 As String = ""
    Public Property 音频参数_响度标准化_启用调整峰值电平 As Boolean = False
    Public Property 音频参数_响度标准化_峰值电平 As String = ""
    '==================================================
    Public Enum 命令行阶段
        普通单次 = 0
        FFprobe获取时长 = 1
        二次编码第一遍 = 2
        二次编码第二遍 = 3
    End Enum

    Class 命令行生成结果
        Public Property 阶段 As 命令行阶段 = 命令行阶段.普通单次
        Public Property 命令行 As String = ""
        Public Property 滤镜图 As String = ""
        Public Property 映射参数 As String = ""
        Public Property 输出滤镜参数 As String = ""
        Public Property 需要媒体总时长 As Boolean = False
        Public Property 说明 As String = ""
    End Class

    Class 滤镜排序单片结构
        Public Property 实例ID As String = Guid.NewGuid().ToString("N")
        Public Property 显示名称 As String = ""
        Public Property 是自定义滤镜 As Boolean = False
        Public Property 允许在排序页直接编辑 As Boolean = False
        Public Property 滤镜标识符 As 标识符枚举 = 标识符枚举.未设置
        Public Property 滤镜目标流类型 As 流类型 = 流类型.未设定
        Public Property 自定义滤镜内容 As String = ""
        Enum 标识符枚举
            未设置 = 0
            裁剪 = 1
            缩放 = 2
            抽帧 = 3
            插帧 = 4
            动态模糊 = 5
            超分 = 6
            降噪 = 7
            锐化 = 8
            胶片颗粒 = 9
            平滑断层 = 10
            扫描方式 = 11
            画面翻转 = 12
            烧录字幕 = 13
            色彩转换 = 14
            调色 = 15
            像素格式预先转换 = 16
            音频响度标准化 = 51
            音频格式转换 = 52
            音频重采样 = 53
            自定义视频滤镜 = 101
            自定义音频滤镜 = 102
        End Enum
        Enum 流类型
            未设定 = 0
            视频 = 1
            音频 = 2
        End Enum
    End Class
    Public Property 滤镜排序系统 As 滤镜排序单片结构() = Array.Empty(Of 滤镜排序单片结构)()
    '==================================================
    Public Property 自定义参数_视频滤镜 As String = ""
    Public Property 自定义参数_音频滤镜 As String = ""
    Public Property 自定义参数_视频参数 As String = ""
    Public Property 自定义参数_音频参数 As String = ""
    Public Property 自定义参数_开头参数 As String = ""
    Public Property 自定义参数_之前参数 As String = ""
    Public Property 自定义参数_之后参数 As String = ""
    Public Property 自定义参数_最后参数 As String = ""
    Public Property 自定义参数_完全自己写 As String = ""
    '==================================================
    Enum 剪辑方法
        未知 = 0
        粗剪 = 1
        精剪从头解码 = 2
        精剪空降解码 = 3
        Trim滤镜 = 4
        掐头去尾 = 5 ' 需要ffprobe，然后将入点和出点作为掐头的时间和尾部截取的时间，如果无法获取到媒体时间，则终止任务
    End Enum
    Public Property 剪辑区间_方法 As 剪辑方法 = 剪辑方法.未知
    Public Property 剪辑区间_入点 As String = ""
    Public Property 剪辑区间_出点 As String = ""
    Public Property 剪辑区间_向前解码多久秒 As String = ""
    '==================================================
    Public Property 流控制_启用保留其他视频流 As Boolean = False
    Public Property 流控制_将视频参数应用于指定流 As String() = Array.Empty(Of String)()
    Public Property 流控制_启用保留其他音频流 As Boolean = False
    Public Property 流控制_将音频参数应用于指定流 As String() = Array.Empty(Of String)()
    Public Property 流控制_将字幕参数应用于指定流 As String() = Array.Empty(Of String)()
    Public Property 流控制_如何操作指定的字幕 As Integer = 0
    Public Property 流控制_启用保留其他字幕流 As Boolean = False
    Public Property 流控制_自动混流SRT As Boolean = False
    Public Property 流控制_自动混流ASS As Boolean = False
    Public Property 流控制_自动混流SSA As Boolean = False
    Public Property 流控制_自动混流的字幕转为MOVTEXT As Boolean = False
    Public Property 流控制_元数据选项 As Integer = 0
    Public Property 流控制_章节选项 As Integer = 0
    Public Property 流控制_附件选项 As Integer = 0
    '==================================================
    Class 元数据单片结构
        Public Property 字段 As String = ""
        Public Property 值 As String = ""
    End Class
    Public Property 元数据_要写入的信息 As 元数据单片结构() = Array.Empty(Of 元数据单片结构)()
    '==================================================
    Enum 章节来源
        未选择 = 0
        文本文档 = 1
        媒体文件 = 2
    End Enum
    Public Property 章节_来源 As 章节来源 = 章节来源.未选择
    Public Property 章节_文件路径 As String = ""
    '==================================================
    Class 附件单片结构
        Public Property 类型 As 附件类型 = 附件类型.未选择
        Public Property 文件路径 As String = ""
        Enum 附件类型
            未选择 = 0
            图片 = 1
            MP4封面图 = 2
            MKV封面图 = 3
            字体文件 = 4
            文本文档 = 5
        End Enum
    End Class
    Public Property 附件_要写入的附件 As 附件单片结构() = Array.Empty(Of 附件单片结构)()
    '==================================================
    Public Property 计算机名称 As String = ""
    Public Property 输出位置 As String = ""
    Public Property 输出位置_保留子文件夹结构起始点 As String = ""
    <System.Text.Json.Serialization.JsonIgnore()>
    Public Property 运行时使用输出位置 As Boolean = False

End Class
