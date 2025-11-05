
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

    Public Property 视频参数_比特率_控制方式 As String = ""
    Public Property 视频参数_质量控制_参数名 As String = ""
    Public Property 视频参数_质量控制_值 As String = ""
    Public Property 视频参数_比特率_基础 As String = ""
    Public Property 视频参数_比特率_最低值 As String = ""
    Public Property 视频参数_比特率_最高值 As String = ""
    Public Property 视频参数_比特率_缓冲区 As String = ""
    Public Property 视频参数_质量控制_进阶参数集 As New List(Of String)

    Public Property 视频参数_色彩管理_像素格式 As String = ""
    Public Property 视频参数_色彩管理_矩阵系数 As String = ""
    Public Property 视频参数_色彩管理_色域 As String = ""
    Public Property 视频参数_色彩管理_传输特性 As String = ""
    Public Property 视频参数_色彩管理_范围 As String = ""
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
    Public Property 视频参数_逐行与隔行_操作 As String = ""
    Public Property 视频参数_画面翻转_角度翻转 As Integer = 0
    Public Property 视频参数_画面翻转_镜像翻转 As Integer = 0

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
    Public Property 流控制_启用保留内嵌字幕流 As Boolean = False
    Public Property 流控制_元数据选项 As Integer = 0
    Public Property 流控制_章节选项 As Integer = 0
    Public Property 流控制_附件选项 As Integer = 0
    Public Property 流控制_启用自动混流同名字幕文件 As Boolean = False

    Public Property 计算机名称 As String = ""
    Public Property 输出位置 As String = ""

End Class
