
<Serializable()>
Public Class 预设数据类型
    Public Property 输出容器 As String = ""

    Public Property 输出命名_使用自动命名 As Boolean = False
    Public Property 输出命名_自动命名选项 As Integer = 0
    Public Property 输出命名_不使用输出文件参数 As Boolean = False
    Public Property 输出命名_开头文本 As String = ""
    Public Property 输出命名_替代文本 As String = ""
    Public Property 输出命名_结尾文本 As String = ""
    Public Property 输出命名_保留创建时间 As Boolean = False
    Public Property 输出命名_保留修改时间 As Boolean = False
    Public Property 输出命名_保留访问时间 As Boolean = False

    Public Property 解码参数_解码器 As String = ""
    Public Property 解码参数_CPU解码线程数 As String = ""
    Public Property 解码参数_解码数据格式 As String = ""

    Public Property 解码参数_指定硬件的参数名 As String = ""
    Public Property 解码参数_指定硬件的参数 As String = ""

    Public Property 视频参数_编码器_类别 As String = ""
    Public Property 视频参数_编码器_具体编码 As String = ""
    Public Property 视频参数_编码器_编码预设 As String = ""
    Public Property 视频参数_编码器_配置文件 As String = ""
    Public Property 视频参数_编码器_场景优化 As String = ""
    Public Property 视频参数_编码器_gpu As String = ""
    Public Property 视频参数_编码器_threads As String = ""

    Public Property 视频参数_分辨率 As String = ""
    Public Property 视频参数_分辨率自动计算_宽度 As String = ""
    Public Property 视频参数_分辨率自动计算_高度 As String = ""
    Public Property 视频参数_分辨率_裁剪滤镜参数 As String = ""
    Public Property 视频参数_帧速率 As String = ""
    Public Property 视频参数_帧速率_抽帧最大变化比例 As String = ""

    Public Property 视频参数_插帧_目标帧率 As String = ""
    Public Property 视频参数_插帧_插帧模式 As String = ""
    Public Property 视频参数_插帧_运动估计模式 As String = ""
    Public Property 视频参数_插帧_运动估计算法 As String = ""
    Public Property 视频参数_插帧_运动补偿模式 As String = ""
    Public Property 视频参数_插帧_可变块大小的运动补偿 As Boolean = False
    Public Property 视频参数_插帧_块大小 As String = ""
    Public Property 视频参数_插帧_搜索范围 As String = ""
    Public Property 视频参数_插帧_场景变化检测强度 As String = ""

    Public Property 视频参数_帧混合_指定帧率 As String = ""
    Public Property 视频参数_帧混合_混合模式 As String = ""
    Public Property 视频参数_帧混合_混合比例 As String = ""

    Public Property 视频参数_超分_目标宽度 As String = ""
    Public Property 视频参数_超分_目标高度 As String = ""
    Public Property 视频参数_超分_上采样算法 As String = ""
    Public Property 视频参数_超分_下采样算法 As String = ""
    Public Property 视频参数_超分_抗振铃强度 As String = ""
    Public Property 视频参数_超分_着色器列表 As New List(Of String)

    Public Property 视频参数_烧录字幕_滤镜选择 As String = ""
    Public Property 视频参数_烧录字幕_字幕格式优先级 As New List(Of Integer) From {-1, -1, -1}
    Public Property 视频参数_烧录字幕_字幕来源是外部文件 As Boolean = False
    Public Property 视频参数_烧录字幕_外部字幕文件名 As String = ""
    Public Property 视频参数_烧录字幕_外部字幕文件夹位置 As String = ""
    Public Property 视频参数_烧录字幕_字幕来源是内嵌的流 As Boolean = False
    Public Property 视频参数_烧录字幕_指定内嵌的流 As String = ""
    Public Property 视频参数_烧录字幕_字体文件夹 As String = ""
    Public Property 视频参数_烧录字幕_基本样式_名称 As String = ""
    Public Property 视频参数_烧录字幕_基本样式_大小 As Single
    Public Property 视频参数_烧录字幕_基本样式_粗体 As Boolean = False
    Public Property 视频参数_烧录字幕_基本样式_斜体 As Boolean = False
    Public Property 视频参数_烧录字幕_基本样式_下划线 As Boolean = False
    Public Property 视频参数_烧录字幕_基本样式_删除线 As Boolean = False
    Public Property 视频参数_烧录字幕_边框样式 As Integer = -1
    Public Property 视频参数_烧录字幕_描边宽度 As String = ""
    Public Property 视频参数_烧录字幕_阴影距离 As String = ""
    Public Property 视频参数_烧录字幕_主要颜色 As Color = Color.Transparent
    Public Property 视频参数_烧录字幕_主要颜色_透明度 As String = ""
    Public Property 视频参数_烧录字幕_次要颜色 As Color = Color.Transparent
    Public Property 视频参数_烧录字幕_次要颜色_透明度 As String = ""
    Public Property 视频参数_烧录字幕_描边颜色 As Color = Color.Transparent
    Public Property 视频参数_烧录字幕_描边颜色_透明度 As String = ""
    Public Property 视频参数_烧录字幕_背景颜色 As Color = Color.Transparent
    Public Property 视频参数_烧录字幕_背景颜色_透明度 As String = ""
    Public Property 视频参数_烧录字幕_对齐方位 As Integer = -1
    Public Property 视频参数_烧录字幕_垂直边距 As String = ""
    Public Property 视频参数_烧录字幕_左边距 As String = ""
    Public Property 视频参数_烧录字幕_右边距 As String = ""
    Public Property 视频参数_烧录字幕_字距 As String = ""
    Public Property 视频参数_烧录字幕_行距 As String = ""
    Public Property 视频参数_烧录字幕_视频分辨率 As String = ""
    Public Property 视频参数_烧录字幕_自定义样式 As String = ""
    Public Property 视频参数_烧录字幕_自定义滤镜参数 As String = ""

    Public Property 视频参数_比特率_控制方式 As String = ""
    Public Property 视频参数_质量控制_参数名 As String = ""
    Public Property 视频参数_质量控制_值 As String = ""
    Public Property 视频参数_比特率_基础 As String = ""
    Public Property 视频参数_比特率_最低值 As String = ""
    Public Property 视频参数_比特率_最高值 As String = ""
    Public Property 视频参数_比特率_缓冲区 As String = ""
    Public Property 视频参数_质量控制_进阶参数集 As New List(Of String)

    Public Property 视频参数_色彩管理_像素格式 As String = ""
    Public Property 视频参数_色彩管理_滤镜选择 As String = ""
    Public Property 视频参数_色彩管理_矩阵系数 As String = ""
    Public Property 视频参数_色彩管理_色域 As String = ""
    Public Property 视频参数_色彩管理_传输特性 As String = ""
    Public Property 视频参数_色彩管理_范围 As String = ""
    Public Property 视频参数_色彩管理_色调映射算法 As String = ""
    Public Property 视频参数_色彩管理_处理方式 As String = ""
    Public Property 视频参数_色彩管理_亮度 As String = ""
    Public Property 视频参数_色彩管理_对比度 As String = ""
    Public Property 视频参数_色彩管理_饱和度 As String = ""
    Public Property 视频参数_色彩管理_伽马 As String = ""

    Public Property 视频参数_降噪_方式 As String = ""
    Public Property 视频参数_降噪_参数1 As String = ""
    Public Property 视频参数_降噪_参数2 As String = ""
    Public Property 视频参数_降噪_参数3 As String = ""
    Public Property 视频参数_降噪_参数4 As String = ""
    Public Property 视频参数_锐化_水平尺寸 As String = ""
    Public Property 视频参数_锐化_垂直尺寸 As String = ""
    Public Property 视频参数_锐化_锐化强度 As String = ""
    Public Property 视频参数_逐行与隔行 As Integer = 0
    Public Property 视频参数_画面翻转_角度翻转 As Integer = 0
    Public Property 视频参数_画面翻转_镜像翻转 As Integer = 0

    Public Property 视频参数_视频帧服务器_使用AviSynth As Boolean = False
    Public Property 视频参数_视频帧服务器_avs脚本文件 As String = ""
    Public Property 视频参数_视频帧服务器_使用VapourSynth As Boolean = False
    Public Property 视频参数_视频帧服务器_vpy脚本文件 As String = ""

    Public Property 音频参数_编码器_具体编码 As String = ""
    Public Property 音频参数_比特率 As String = ""
    Public Property 音频参数_质量参数名 As String = ""
    Public Property 音频参数_质量值 As String = ""
    Public Property 音频参数_声道数 As String = ""
    Public Property 音频参数_采样率 As String = ""
    Public Property 音频参数_响度标准化_目标响度 As String = ""
    Public Property 音频参数_响度标准化_动态范围 As String = ""
    Public Property 音频参数_响度标准化_峰值电平 As String = ""

    Public Property 图片参数_编码器_编码名称 As String = ""
    Public Property 图片参数_编码器_质量值 As String = ""

    Public Property 自定义参数_视频滤镜 As String = ""
    Public Property 自定义参数_音频滤镜 As String = ""
    Public Property 自定义参数_filter_complex As String = ""
    Public Property 自定义参数_视频参数 As String = ""
    Public Property 自定义参数_音频参数 As String = ""
    Public Property 自定义参数_开头参数 As String = ""
    Public Property 自定义参数_之前参数 As String = ""
    Public Property 自定义参数_之后参数 As String = ""
    Public Property 自定义参数_最后参数 As String = ""
    Public Property 自定义参数_完全自己写 As String = ""

    Public Property 剪辑区间_方法 As Integer = 0
    Public Property 剪辑区间_入点 As String = ""
    Public Property 剪辑区间_出点 As String = ""
    Public Property 剪辑区间_向前解码多久秒 As String = ""

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


    Public Property 计算机名称 As String = ""
    Public Property 输出位置 As String = ""

End Class
