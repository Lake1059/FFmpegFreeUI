Imports System.IO
Imports System.Reflection
Imports System.Text
Imports System.Text.Json
Public Class 设置_v6

    Public Shared Property 实例对象 As New 设置_v6

    Public Property 图形DX抗锯齿 As Integer = 0
    Public Property 图形DX文字渲染模式 As Integer = 0
    Public Property 图形DX_SSAA As Integer = 0

    Public Property 图形DX_GPU缓存总预算 As Integer = 2
    Public Property 图形DX_CPU位图缓存总预算 As Integer = 3
    Public Property 图形DX画刷缓存条目上限 As Integer = 6
    Public Property 图形DW字体缓存条目上限 As Integer = 3
    Public Property 图形DX背景穿透脏区策略 As Integer = 4
    Public Property 图形DX_SSAA缓存分桶粒度 As Integer = 1

    Public Property 图形动画帧率 As Integer = 60

    Public Property 图形DX_HDR启用 As Integer = 0
    Public Property 图形DX_HDR显示档位 As Integer = 2
    Public Property 图形DX_HDR矢量颜色映射 As Integer = 0
    Public Property 图形DX_HDR图片映射 As Integer = 0

    Public Property 窗口样式 As Integer = 2
    ''' <summary>0=跟随 Windows 应用浅/深色模式；1=始终浅色模式；2=始终深色模式。</summary>
    Public Property 界面主题 As Integer = 2
    ''' <summary>0=直角；1=圆角。旧配置缺少此字段时，Windows 11 默认圆角，其他系统默认直角。</summary>
    Public Property 窗口圆角 As Integer = If(LakeUI.DwmWindowStyle.IsCornerModeSupported, 1, 0)
    Public Property 启用性能计数器 As Integer = 0

    Public Property 字体 As String = SystemFonts.DefaultFont.FontFamily.Name

    Public Property 指定处理器核心 As String = ""
    Public Property 自动同时运行任务数量选项 As Integer = 0
    Public Property 编码队列刷新速度 As Integer = 2

    Public Property 工作目录 As String = ""
    Public Property 有任务时系统保持状态选项 As Integer = 0
    Public Property 提示音选项 As Integer = 0
    Public Property 自动开始任务选项 As Integer = 0
    Public Property 自动重置参数面板的页面选择 As Integer = 0
    Public Property 混淆任务名称 As Integer = 0
    Public Property 打开独立参数面板时自动切到预设管理页面 As Integer = 0
    Public Property 任务失败自动删除输出文件 As Integer = 0
    Public Property 编码队列显示最新日志行 As Integer = 0
    Public Property 任务日志保留行数选项 As Integer = 1
    Public Property 任务日志性能计数器 As Integer = 0

    Public Property 替代进程文件名 As String = ""
    Public Property 覆盖参数传递 As String = ""
    Public Property 转译模式 As Boolean = False

    Public Shared Function 获取FFmpeg进程文件名() As String
        Dim custom = If(实例对象?.替代进程文件名, "").Trim()
        Return 解析工作目录进程文件(If(custom <> "", custom, "ffmpeg.exe"))
    End Function

    Public Shared Function 获取FFprobe进程文件名() As String
        Return 获取同目录配套进程文件名("ffprobe.exe")
    End Function

    Public Shared Function 获取FFplay进程文件名() As String
        Return 获取同目录配套进程文件名("ffplay.exe")
    End Function

    Public Shared Function 获取有效工作目录() As String
        Dim directory = If(实例对象?.工作目录, "").Trim()
        Return If(System.IO.Directory.Exists(directory), directory, "")
    End Function

    Private Shared Function 获取同目录配套进程文件名(defaultFileName As String) As String
        Dim custom = If(实例对象?.替代进程文件名, "").Trim()
        If custom <> "" Then
            Dim customDirectory = Path.GetDirectoryName(custom)
            If Not String.IsNullOrWhiteSpace(customDirectory) Then
                Dim candidate = Path.Combine(customDirectory, defaultFileName)
                If File.Exists(candidate) Then Return Path.GetFullPath(candidate)
            End If
        End If
        Return 解析工作目录进程文件(defaultFileName)
    End Function

    Private Shared Function 解析工作目录进程文件(processFileName As String) As String
        Dim value = If(processFileName, "").Trim().Trim(""""c)
        If value = "" Then Return value
        If Path.IsPathRooted(value) Then Return value

        Dim workingDirectory = 获取有效工作目录()
        If workingDirectory <> "" Then
            Dim candidate = Path.Combine(workingDirectory, value)
            If File.Exists(candidate) Then Return Path.GetFullPath(candidate)
            If String.IsNullOrWhiteSpace(Path.GetExtension(candidate)) AndAlso File.Exists(candidate & ".exe") Then Return Path.GetFullPath(candidate & ".exe")
        End If
        Return value
    End Function

    ''' <summary>
    ''' 0=github;1=gh-proxy.com;3=FrostLynx;4=MirrorChyan
    ''' </summary>
    ''' <returns></returns>
    Public Property 更新服务器选择 As Integer = 0
    Public Property MirrorChyanCDK As String = ""

    Public Property 用户统计_成功编码任务数 As Long = 0
    Public Property 用户统计_任务执行总时长Ticks As Long = 0
    Public Property 用户统计_首次成功提示已显示 As Boolean = False
    Public Property 用户统计_已提示编码任务百次档位 As Long = 0
    Public Property 用户统计_已提示任务时长240小时档位 As Long = 0

    Public Property 是否监听端口 As Boolean = False
    Public Property 监听的端口 As String = "10591"

    Public Property AgentEndPoint As String = ""
    Public Property AgentApiKey As String = ""
    Public Property Agent附加请求头 As String = ""
    Public Property Agent附加请求Body As String = ""
    Public Property AgentModelId As String = ""
    Public Property Agent推理级别 As String = ""
    ''' <summary>
    ''' 0=本地联网；1=端点联网；2=禁用联网
    ''' </summary>
    Public Property Agent联网设置 As Integer = 0
    Public Property Agent权限级别 As Integer = 0

    Public Property Agent权限_编辑参数面板 As Boolean = True
    Public Property Agent权限_添加和编辑任务 As Boolean = False
    Public Property Agent权限_访问编码队列 As Boolean = False

    Public Property SP_窗口标题文字 As String = ""
    Public Property SP_起始页面顶栏标题 As String = ""
    Public Property SP_起始页面顶栏副标题 As String = ""
    Public Property 个性化_软件图标 As String = ""
    Public Property 个性化_任务完成音效 As String = ""
    Public Property 个性化_任务失败音效 As String = ""
    Public Property 个性化_起始页标题 As String = ""
    Public Property 个性化_起始页副标题 As String = ""
    Public Property 个性化_窗口标题栏 As String = ""
    Public Property 个性化_起始页背景图 As String = ""
    Public Property SP_窗口边框颜色_A As Integer = 255
    Public Property SP_窗口边框颜色_R As Integer = Color.Gray.R
    Public Property SP_窗口边框颜色_G As Integer = Color.Gray.G
    Public Property SP_窗口边框颜色_B As Integer = Color.Gray.B
    Public Property SP_分层阴影颜色_A As Integer = 255
    Public Property SP_分层阴影颜色_R As Integer = Color.Black.R
    Public Property SP_分层阴影颜色_G As Integer = Color.Black.G
    Public Property SP_分层阴影颜色_B As Integer = Color.Black.B
    Public Property SP_边框宽度 As Integer = 1
    Public Property SP_标题栏分割线 As Integer = 0
    Public Property SP_标题栏分割线颜色_A As Integer = 40
    Public Property SP_标题栏分割线颜色_R As Integer = 220
    Public Property SP_标题栏分割线颜色_G As Integer = 220
    Public Property SP_标题栏分割线颜色_B As Integer = 220
    Public Property SP_毛玻璃模式 As Integer = 0
    Public Property SP_毛玻璃背景来源 As Integer = -1
    Public Property SP_毛玻璃噪点颗粒 As Integer = -1

    Private Shared ReadOnly HDR档位选项 As LakeUI.GlobalOptions.HdrOutputProfile() = {
        LakeUI.GlobalOptions.HdrOutputProfile.HDR200, LakeUI.GlobalOptions.HdrOutputProfile.HDR300,
        LakeUI.GlobalOptions.HdrOutputProfile.HDR400, LakeUI.GlobalOptions.HdrOutputProfile.HDR500,
        LakeUI.GlobalOptions.HdrOutputProfile.HDR600, LakeUI.GlobalOptions.HdrOutputProfile.HDR700,
        LakeUI.GlobalOptions.HdrOutputProfile.HDR800, LakeUI.GlobalOptions.HdrOutputProfile.HDR900,
        LakeUI.GlobalOptions.HdrOutputProfile.HDR1000
    }
    Private Shared ReadOnly GPU缓存预算选项MiB As Long() = {0L, 64L, 128L, 256L, 512L, 1024L, 2048L, 4096L, 8192L}
    Private Shared ReadOnly CPU缓存预算选项MiB As Long() = {0L, 32L, 64L, 96L, 128L, 256L, 512L, 1024L, 2048L}
    Private Shared ReadOnly 缓存条目选项 As Integer() = {0, 8, 16, 32, 64, 128, 256, 512, 1024}
    Private Shared ReadOnly 字体缓存条目选项 As Integer() = {0, 32, 64, 128, 256, 512, 1024}
    Private Shared ReadOnly 背景穿透脏区数量选项 As Integer() = {4, 4, 4, 8, 8, 8, 16, 16, 16}
    Private Shared ReadOnly 背景穿透完整重采阈值选项 As Single() = {0.4F, 0.6F, 0.8F, 0.4F, 0.6F, 0.8F, 0.4F, 0.6F, 0.8F}
    Private Shared ReadOnly SSAA缓存分桶粒度选项 As Integer() = {32, 64, 96, 128, 192, 256, 384, 512, 1024}

    Public Enum 图形性能选项
        抗锯齿
        文字渲染
        超采样
        GPU缓存
        画刷缓存
        字体缓存
        CPU缓存
        背景脏区
        超采样缓存
    End Enum

    Public Property 是否询问标记_下载服务器选择 As Boolean = False
    Public Property 自定义视频编码器列表 As New List(Of String)
    ''' <summary>质量评测页面跨启动设置；不保存原视频、对比文件和跑分结果。</summary>
    Public Property 质量评测页面状态 As String = ""

    Private Shared ReadOnly 设置文件路径 As String = Path.Combine(Application.StartupPath, "Settings.json")
    Private Shared ReadOnly 设置文件写入锁 As New Object()

    Public Shared Sub 设置界面主题(index As Integer)
        If index < 0 OrElse index > 2 Then Return
        If 实例对象.界面主题 = index Then Return
        实例对象.界面主题 = index
        界面主题_v6.刷新主题(True)
    End Sub

    Public Shared Sub 设置窗口圆角(index As Integer)
        If index < 0 OrElse index > 1 Then Return
        Dim 选择 = If(LakeUI.DwmWindowStyle.IsCornerModeSupported, index, 0)
        If 实例对象.窗口圆角 = 选择 Then Return
        实例对象.窗口圆角 = 选择
        界面主题_v6.应用窗口圆角设置()
    End Sub

    Public Shared Sub 设置HDR选项(启用 As Integer, 显示档位 As Integer, 矢量颜色 As Integer, 图片 As Integer)
        实例对象.图形DX_HDR启用 = Math.Clamp(启用, 0, 1)
        实例对象.图形DX_HDR显示档位 = If(显示档位 < 0 OrElse 显示档位 >= HDR档位选项.Length, 2, 显示档位)
        实例对象.图形DX_HDR矢量颜色映射 = Math.Clamp(矢量颜色, 0, 1)
        实例对象.图形DX_HDR图片映射 = Math.Clamp(图片, 0, 1)
        应用HDR设置()
    End Sub

    Public Shared Sub 应用HDR设置()
        LakeUI.GlobalOptions.HDR.Enabled = 实例对象.图形DX_HDR启用 = 1
        Dim 档位 = 实例对象.图形DX_HDR显示档位
        LakeUI.GlobalOptions.HDR.Profile = HDR档位选项(If(档位 < 0 OrElse 档位 >= HDR档位选项.Length, 2, 档位))
        LakeUI.GlobalOptions.HDR.MapVectorColors = 实例对象.图形DX_HDR矢量颜色映射 <> 1
        LakeUI.GlobalOptions.HDR.MapImages = 实例对象.图形DX_HDR图片映射 <> 1
        For Each openForm As Form In Application.OpenForms
            openForm.Invalidate(True)
        Next
    End Sub

    Public Shared Sub 设置图形性能选项(选项 As 图形性能选项, index As Integer)
        If index < 0 Then Return
        Select Case 选项
            Case 图形性能选项.抗锯齿 : 实例对象.图形DX抗锯齿 = index
            Case 图形性能选项.文字渲染 : 实例对象.图形DX文字渲染模式 = index
            Case 图形性能选项.超采样 : 实例对象.图形DX_SSAA = index
            Case 图形性能选项.GPU缓存 : 实例对象.图形DX_GPU缓存总预算 = index
            Case 图形性能选项.画刷缓存 : 实例对象.图形DX画刷缓存条目上限 = index
            Case 图形性能选项.字体缓存 : 实例对象.图形DW字体缓存条目上限 = index
            Case 图形性能选项.CPU缓存 : 实例对象.图形DX_CPU位图缓存总预算 = index
            Case 图形性能选项.背景脏区 : 实例对象.图形DX背景穿透脏区策略 = index
            Case 图形性能选项.超采样缓存 : 实例对象.图形DX_SSAA缓存分桶粒度 = index
            Case Else : Return
        End Select
        应用图形性能设置()
    End Sub

    Public Shared Sub 应用图形性能设置()
        LakeUI.GlobalOptions.GlobalAntialiasMode = If(实例对象.图形DX抗锯齿 = 1,
            Vortice.Direct2D1.AntialiasMode.Aliased, Vortice.Direct2D1.AntialiasMode.PerPrimitive)
        Select Case 实例对象.图形DX文字渲染模式
            Case 1 : LakeUI.GlobalOptions.GlobalTextQuality = LakeUI.GlobalOptions.TextQualityMode.Grayscale
            Case 2 : LakeUI.GlobalOptions.GlobalTextQuality = LakeUI.GlobalOptions.TextQualityMode.Aliased
            Case 3 : LakeUI.GlobalOptions.GlobalTextQuality = LakeUI.GlobalOptions.TextQualityMode.Outline
            Case Else : LakeUI.GlobalOptions.GlobalTextQuality = LakeUI.GlobalOptions.TextQualityMode.ClearType
        End Select
        Select Case 实例对象.图形DX_SSAA
            Case 1 : LakeUI.GlobalOptions.GlobalSSAA = LakeUI.GlobalOptions.SuperSamplingScaleEnum.x2
            Case 2 : LakeUI.GlobalOptions.GlobalSSAA = LakeUI.GlobalOptions.SuperSamplingScaleEnum.x3
            Case 3 : LakeUI.GlobalOptions.GlobalSSAA = LakeUI.GlobalOptions.SuperSamplingScaleEnum.x4
            Case Else : LakeUI.GlobalOptions.GlobalSSAA = LakeUI.GlobalOptions.SuperSamplingScaleEnum.OFF
        End Select
        LakeUI.GlobalOptions.GpuCacheBudgetBytes = GPU缓存预算选项MiB(Math.Clamp(实例对象.图形DX_GPU缓存总预算, 0, GPU缓存预算选项MiB.Length - 1)) * 1024L * 1024L
        LakeUI.GlobalOptions.CpuCacheBudgetBytes = CPU缓存预算选项MiB(Math.Clamp(实例对象.图形DX_CPU位图缓存总预算, 0, CPU缓存预算选项MiB.Length - 1)) * 1024L * 1024L
        LakeUI.GlobalOptions.BrushCacheLimit = 缓存条目选项(Math.Clamp(实例对象.图形DX画刷缓存条目上限, 0, 缓存条目选项.Length - 1))
        Dim 字体上限 = 字体缓存条目选项(Math.Clamp(实例对象.图形DW字体缓存条目上限, 0, 字体缓存条目选项.Length - 1))
        LakeUI.GlobalOptions.TextFormatCacheLimit = 字体上限
        LakeUI.GlobalOptions.FontResolveCacheLimit = 字体上限
        Dim 脏区索引 = Math.Clamp(实例对象.图形DX背景穿透脏区策略, 0, 背景穿透脏区数量选项.Length - 1)
        LakeUI.GlobalOptions.BackgroundDirtyRectLimit = 背景穿透脏区数量选项(脏区索引)
        LakeUI.GlobalOptions.BackgroundFullDirtyRatio = 背景穿透完整重采阈值选项(脏区索引)
        LakeUI.GlobalOptions.SsaaBucketSize = SSAA缓存分桶粒度选项(Math.Clamp(实例对象.图形DX_SSAA缓存分桶粒度, 0, SSAA缓存分桶粒度选项.Length - 1))
    End Sub

    Public Shared Sub 初始化界面外观()
        应用图形性能设置()
        应用HDR设置()
        界面主题_v6.初始化()
        界面主题_v6.应用窗口圆角设置()
        LakeUI.MessageDialogOptions.BackdropTintColor = Color.FromArgb(120, 0, 0, 0)
        LakeUI.MessageDialogOptions.BackdropBlurRadius = 30
        LakeUI.MessageDialogOptions.BackdropBlurPasses = 2
        LakeUI.FloatingToolTipForm.BackdropTintColor = Color.FromArgb(120, 0, 0, 0)
        LakeUI.FloatingToolTipForm.BackdropBlurRadius = 30
        LakeUI.FloatingToolTipForm.BackdropBlurPasses = 2
    End Sub

    Public Shared Sub 设置SP窗口边框颜色(颜色 As Color)
        实例对象.SP_窗口边框颜色_A = 颜色.A
        实例对象.SP_窗口边框颜色_R = 颜色.R
        实例对象.SP_窗口边框颜色_G = 颜色.G
        实例对象.SP_窗口边框颜色_B = 颜色.B
        FormMain_v6.ThisIsYourWindow1.BorderColor = 颜色
        FormMain_v6.ThisIsYourWindow1.BorderInactiveColor = 颜色
    End Sub

    Public Shared Sub 设置SP分层阴影颜色(颜色 As Color)
        实例对象.SP_分层阴影颜色_A = 颜色.A
        实例对象.SP_分层阴影颜色_R = 颜色.R
        实例对象.SP_分层阴影颜色_G = 颜色.G
        实例对象.SP_分层阴影颜色_B = 颜色.B
        FormMain_v6.ThisIsYourWindow1.LayerShadowColor = 颜色
    End Sub

    Public Shared Sub 设置SP标题栏分割线颜色(颜色 As Color)
        实例对象.SP_标题栏分割线颜色_A = 颜色.A
        实例对象.SP_标题栏分割线颜色_R = 颜色.R
        实例对象.SP_标题栏分割线颜色_G = 颜色.G
        实例对象.SP_标题栏分割线颜色_B = 颜色.B
        FormMain_v6.ThisIsYourWindow1.CaptionBottomLineColor = 颜色
    End Sub

    Public Shared Sub 设置SP边框宽度(index As Integer)
        If Not SP_UnLock OrElse index < 0 Then Return
        实例对象.SP_边框宽度 = index
        FormMain_v6.ThisIsYourWindow1.BorderSize = index
    End Sub

    Public Shared Sub 设置SP毛玻璃模式(index As Integer)
        If Not SP_UnLock OrElse index < 0 Then Return
        实例对象.SP_毛玻璃模式 = index
        Select Case index
            Case 0
                FormMain_v6.ThisIsYourWindow1.BackdropMode = LakeUI.ThisIsYourWindow.BackdropModeEnum.None
                清除SP自有背景图()
                FormMain_v6.ThisIsYourWindow1.BackdropNoiseOpacity = 0
            Case 1 : FormMain_v6.ThisIsYourWindow1.BackdropBlurPasses = 0
            Case 2
                FormMain_v6.ThisIsYourWindow1.BackdropBlurPasses = 1
                FormMain_v6.ThisIsYourWindow1.BackdropBlurRadius = 10
            Case 3
                FormMain_v6.ThisIsYourWindow1.BackdropBlurPasses = 3
                FormMain_v6.ThisIsYourWindow1.BackdropBlurRadius = 24
        End Select
    End Sub

    Public Shared Sub 设置SP毛玻璃背景来源(index As Integer)
        If Not SP_UnLock Then Return
        实例对象.SP_毛玻璃背景来源 = index
        Select Case index
            Case 0
                FormMain_v6.ThisIsYourWindow1.BackdropMode = LakeUI.ThisIsYourWindow.BackdropModeEnum.Image
                加载SP自定义背景图()
            Case 1
                FormMain_v6.ThisIsYourWindow1.BackdropMode = LakeUI.ThisIsYourWindow.BackdropModeEnum.Auto
                清除SP自有背景图()
        End Select
    End Sub

    Public Shared Sub 设置SP毛玻璃噪点(index As Integer)
        If Not SP_UnLock Then Return
        实例对象.SP_毛玻璃噪点颗粒 = index
        Select Case index
            Case 0 : FormMain_v6.ThisIsYourWindow1.BackdropNoiseOpacity = 0
            Case 1 : FormMain_v6.ThisIsYourWindow1.BackdropNoiseOpacity = 18
            Case 2 : FormMain_v6.ThisIsYourWindow1.BackdropNoiseOpacity = 36
        End Select
    End Sub

    Public Shared Sub 设置自动开始任务(index As Integer)
        If index < 0 Then Return
        实例对象.自动开始任务选项 = index
        编码队列_v6.应用自动开始任务设置(index = 0)
    End Sub

    Public Shared Sub 设置队列最新日志(index As Integer)
        If index < 0 Then Return
        实例对象.编码队列显示最新日志行 = index
        编码队列_v6.刷新显示()
    End Sub

    Public Shared Sub 设置任务日志保留行数(index As Integer)
        If index < 0 Then Return
        实例对象.任务日志保留行数选项 = index
        编码队列_v6.刷新显示()
    End Sub

    Public Shared Sub 设置任务日志性能计数器(index As Integer)
        If index < 0 Then Return
        实例对象.任务日志性能计数器 = index
        Form_v6_编码队列_任务日志.刷新任务性能计数器设置()
    End Sub

    Public Shared Sub 设置自动同时运行任务数量(index As Integer)
        If index < 0 Then Return
        实例对象.自动同时运行任务数量选项 = index
        编码队列_v6.请求调度()
    End Sub

    Public Shared Sub 设置远程监听(启用 As Boolean)
        实例对象.是否监听端口 = 启用
        If 启用 Then
            端口监听_v6.启动客户端()
        Else
            端口监听_v6.停止客户端()
        End If
    End Sub

    Public Shared Sub 设置远程监听端口(端口 As String)
        实例对象.监听的端口 = 端口
        If 实例对象.是否监听端口 AndAlso 端口监听_v6.是否正在运行 Then 端口监听_v6.重启客户端()
    End Sub

    Public Shared Sub 退出时保存设置()
        Try
            保存设置到文件()
        Catch ex As Exception
            MsgBox($"保存设置失败：{ex.Message}", MsgBoxStyle.Critical)
        End Try
    End Sub

    Friend Shared Sub 后台保存设置()
        保存设置到文件()
    End Sub

    Private Shared Sub 保存设置到文件()
        SyncLock 设置文件写入锁
            Dim 临时文件路径 = 设置文件路径 & ".tmp"
            Try
                File.WriteAllText(临时文件路径, JsonSerializer.Serialize(实例对象, JsonSO), New UTF8Encoding(False))
                File.Move(临时文件路径, 设置文件路径, True)
            Finally
                If File.Exists(临时文件路径) Then File.Delete(临时文件路径)
            End Try
        End SyncLock
    End Sub

    Public Shared Sub 启动时加载设置()
        If Not FileIO.FileSystem.FileExists(设置文件路径) Then
            If FontFamily.Families.Any(Function(f) f.Name = "微软雅黑") Then
                实例对象.字体 = "微软雅黑"
            End If
            退出时保存设置()
        Else
            Dim 设置文本 = FileIO.FileSystem.ReadAllText(设置文件路径)
            实例对象 = JsonSerializer.Deserialize(Of 设置_v6)(设置文本)
            迁移旧设置字段(设置文本)
        End If
        设置组合框选中索引(Form_v6_设置_LakeUI性能选项.MCB_GPU抗锯齿, 实例对象.图形DX抗锯齿, 0)
        设置组合框选中索引(Form_v6_设置_LakeUI性能选项.MCB_文字渲染模式, 实例对象.图形DX文字渲染模式, 0)
        设置组合框选中索引(Form_v6_设置_LakeUI性能选项.MCB_SSAA, 实例对象.图形DX_SSAA, 0)

        设置组合框选中索引(Form_v6_设置_LakeUI性能选项.MCB_D2DImage缓存预算, 实例对象.图形DX_GPU缓存总预算, 2)
        设置组合框选中索引(Form_v6_设置_LakeUI性能选项.MCB_D2D每对象画刷缓存数量, 实例对象.图形DX画刷缓存条目上限, 6)
        设置组合框选中索引(Form_v6_设置_LakeUI性能选项.MCB_DW字体相关预算, 实例对象.图形DW字体缓存条目上限, 3)
        设置组合框选中索引(Form_v6_设置_LakeUI性能选项.MCB_超容器背景映射源位图缓存, 实例对象.图形DX_CPU位图缓存总预算, 3)
        设置组合框选中索引(Form_v6_设置_LakeUI性能选项.MCB_超容器背景映射脏区策略极限, 实例对象.图形DX背景穿透脏区策略, 4)
        设置组合框选中索引(Form_v6_设置_LakeUI性能选项.MCB_超容器背景映射条目预算, 实例对象.图形DX_SSAA缓存分桶粒度, 1)
        Form_v6_设置_LakeUI性能选项.MCB_动画帧率.Text = 实例对象.图形动画帧率

        设置组合框选中索引(Form_v6_设置_LakeUIHDR.MCB_HDR启用, 实例对象.图形DX_HDR启用, 0)
        设置组合框选中索引(Form_v6_设置_LakeUIHDR.MCB_HDR显示档位, 实例对象.图形DX_HDR显示档位, 2)
        设置组合框选中索引(Form_v6_设置_LakeUIHDR.MCB_HDR矢量颜色, 实例对象.图形DX_HDR矢量颜色映射, 0)
        设置组合框选中索引(Form_v6_设置_LakeUIHDR.MCB_HDR图片, 实例对象.图形DX_HDR图片映射, 0)

        Form_v6_设置_LakeUI视觉体验.MCB_窗口样式.SelectedIndex = 实例对象.窗口样式
        Form_v6_设置_LakeUI视觉体验.MCB_性能计数器.SelectedIndex = 实例对象.启用性能计数器
        Form_v6_设置_界面显示.加载新增外观设置()

        Form_v6_设置_性能调度.MTB_处理器线程.Text = 实例对象.指定处理器核心
        Form_v6_设置_性能调度.MCB_自动开始数量.SelectedIndex = 实例对象.自动同时运行任务数量选项
        Form_v6_设置_性能调度.MCB_编码队列刷新速度.SelectedIndex = 实例对象.编码队列刷新速度

        If Directory.Exists(实例对象.工作目录) Then
            Form_v6_设置_功能设定.MTB_工作目录.Text = 实例对象.工作目录
        Else
            Form_v6_设置_功能设定.MTB_工作目录.Text = ""
        End If
        Form_v6_设置_功能设定.MCB_有任务时系统状态.SelectedIndex = 实例对象.有任务时系统保持状态选项
        Form_v6_设置_功能设定.MCB_是否启用提示音.SelectedIndex = 实例对象.提示音选项
        Form_v6_设置_功能设定.MCB_是否自动开始任务.SelectedIndex = 实例对象.自动开始任务选项
        Form_v6_设置_功能设定.MCB_是否自动重置参数面板到第一个页面.SelectedIndex = 实例对象.自动重置参数面板的页面选择
        Form_v6_设置_功能设定.MCB_任务名称混淆.SelectedIndex = 实例对象.混淆任务名称
        Form_v6_设置_功能设定.MCB_独立参数面板自动切预设管理.SelectedIndex = 实例对象.打开独立参数面板时自动切到预设管理页面
        Form_v6_设置_功能设定.MCB_任务失败删除文件.SelectedIndex = 实例对象.任务失败自动删除输出文件
        Form_v6_设置_功能设定.MCB_编码队列显示最新日志行.SelectedIndex = Math.Min(Math.Max(实例对象.编码队列显示最新日志行, 0), 1)
        Form_v6_设置_功能设定.MCB_任务日志保留行数.SelectedIndex = Math.Min(Math.Max(实例对象.任务日志保留行数选项, 0), 3)
        Form_v6_设置_功能设定.MCB_任务日志性能计数器.SelectedIndex = Math.Min(Math.Max(实例对象.任务日志性能计数器, 0), 1)

        Form_v6_设置_转译辅助.MCB_替代进程的文件名.Text = 实例对象.替代进程文件名
        Form_v6_设置_转译辅助.MTB_覆盖参数传递.Text = 实例对象.覆盖参数传递
        Form_v6_设置_转译辅助.MCB_转译模式.Checked = 实例对象.转译模式

        If 实例对象.MirrorChyanCDK <> "" Then
            Form_v6_设置_更新选项.MCB_更新服务器.Items(3) = "Mirror酱 付费 CDN"
            Form_v6_设置_更新选项.HtmlColorLabel4.Visible = True
            Form_v6_设置_更新选项.HtmlColorLabel5.Visible = False
        End If
        Form_v6_设置_更新选项.MCB_更新服务器.SelectedIndex = 实例对象.更新服务器选择

        Form_v6_设置_远程调用.BooleanSwitch1.Checked = 实例对象.是否监听端口
        Form_v6_设置_远程调用.ModernTextBox1.Text = 实例对象.监听的端口

        Form_v6_设置_Agent.MTB_自定义地址.Text = 实例对象.AgentEndPoint
        Form_v6_设置_Agent.MTB_APIKEY.Text = 实例对象.AgentApiKey
        Form_v6_设置_Agent.MTB_附加请求头.Text = 实例对象.Agent附加请求头
        Form_v6_设置_Agent.MTB_附加请求Body.Text = 实例对象.Agent附加请求Body
        Form_v6_设置_Agent.刷新SPAgent端点列表()
        Form_v6_Agent.MCB_联网设置.SelectedIndex = Math.Min(Math.Max(AgentNetworkMode.Normalize(实例对象.Agent联网设置), 0), Math.Max(0, Form_v6_Agent.MCB_联网设置.Items.Count - 1))
        Form_v6_Agent.MCB_权限控制.SelectedIndex = Math.Min(Math.Max(实例对象.Agent权限级别, 0), Math.Max(0, Form_v6_Agent.MCB_权限控制.Items.Count - 1))

        Dim 字体列表 As New List(Of String)
        For Each 字体 As FontFamily In FontFamily.Families
            字体列表.Add(字体.Name)
        Next
        字体列表.Sort()
        If 字体列表.Contains("微软雅黑") Then
            Form_v6_设置_界面显示.MCB_全局字体.Font = New Font("微软雅黑", 10)
        ElseIf 字体列表.Contains("Microsoft YaHei UI") Then
            Form_v6_设置_界面显示.MCB_全局字体.Font = New Font("Microsoft YaHei UI", 10)
        End If
        Form_v6_设置_界面显示.MCB_全局字体.Items.AddRange(字体列表.ToArray)
        Form_v6_设置_界面显示.MCB_全局字体.Text = 实例对象.字体

        Dim 起始页面顶栏默认标题 = $"<span style=""font-size:15pt"">FFmpegFreeUI {版本号.获取自身版本号} & LakeUI 5th Render Engine</span>"
        Dim 起始页面顶栏副标题 = "<span style=""font-size:10pt; color:CornflowerBlue"">将 ffmpeg、ffplay、ffprobe 加入环境变量或放置于当前目录即可调用</span>"
        If 实例对象.SP_起始页面顶栏标题 <> "" AndAlso SP_UnLock Then
            Form_v6_起始页面.HtmlColorLabel1.Text = 实例对象.SP_起始页面顶栏标题
        Else
            Form_v6_起始页面.HtmlColorLabel1.Text = 起始页面顶栏默认标题
        End If
        If 实例对象.SP_起始页面顶栏副标题 <> "" AndAlso SP_UnLock Then
            Form_v6_起始页面.HtmlColorLabel1.Text &= "<br>" & 实例对象.SP_起始页面顶栏副标题
        Else
            Form_v6_起始页面.HtmlColorLabel1.Text &= "<br>" & 起始页面顶栏副标题
        End If

        If Not SP_UnLock Then
            设置主窗体图标(CreateIconFromImage(My.Resources.Resource1.AppIcon))
            Exit Sub
        End If

        Form_v6_设置_个性化.HtmlColorLabel1.Text = "感谢您支持 FFmpegFreeUI Supporter Pack"
        Form_v6_设置_个性化.Panel4.Visible = False

        加载SP自定义任务完成音效()
        加载SP自定义任务失败音效()

        If 实例对象.SP_窗口标题文字 <> "" Then FormMain_v6.Text = 实例对象.SP_窗口标题文字

        FormMain_v6.ThisIsYourWindow1.BorderColor = Color.FromArgb(实例对象.SP_窗口边框颜色_A, 实例对象.SP_窗口边框颜色_R, 实例对象.SP_窗口边框颜色_G, 实例对象.SP_窗口边框颜色_B)
        FormMain_v6.ThisIsYourWindow1.BorderInactiveColor = Color.FromArgb(实例对象.SP_窗口边框颜色_A, 实例对象.SP_窗口边框颜色_R, 实例对象.SP_窗口边框颜色_G, 实例对象.SP_窗口边框颜色_B)
        FormMain_v6.ThisIsYourWindow1.LayerShadowColor = Color.FromArgb(实例对象.SP_分层阴影颜色_A, 实例对象.SP_分层阴影颜色_R, 实例对象.SP_分层阴影颜色_G, 实例对象.SP_分层阴影颜色_B)
        应用SP标题栏分割线()
        FormMain_v6.ThisIsYourWindow1.CaptionBottomLineColor = Color.FromArgb(实例对象.SP_标题栏分割线颜色_A, 实例对象.SP_标题栏分割线颜色_R, 实例对象.SP_标题栏分割线颜色_G, 实例对象.SP_标题栏分割线颜色_B)

        Form_v6_设置_个性化.MCB_边框宽度.SelectedIndex = 实例对象.SP_边框宽度
        设置组合框选中索引(Form_v6_设置_个性化.MCB_标题栏分割线, If(实例对象.SP_标题栏分割线 > 0, 1, 0))
        Form_v6_设置_个性化.MCB_毛玻璃模式.SelectedIndex = 实例对象.SP_毛玻璃模式
        Form_v6_设置_个性化.MCB_背景来源.SelectedIndex = 实例对象.SP_毛玻璃背景来源
        Form_v6_设置_个性化.MCB_噪点颗粒.SelectedIndex = 实例对象.SP_毛玻璃噪点颗粒

    End Sub

    Private Shared Sub 设置组合框选中索引(combo As LakeUI.ModernComboBox, index As Integer, Optional fallback As Integer = 0)
        If combo Is Nothing OrElse combo.Items.Count = 0 Then Return
        Dim safeFallback = Math.Min(Math.Max(fallback, 0), combo.Items.Count - 1)
        If index < 0 OrElse index >= combo.Items.Count Then
            combo.SelectedIndex = safeFallback
        Else
            combo.SelectedIndex = index
        End If
    End Sub

    Public Shared Sub 应用SP标题栏分割线()
        Dim window = FormMain_v6.ThisIsYourWindow1
        Dim 当前分割线高度 = Math.Max(0, window.CaptionBottomLineHeight)
        Dim 原标题栏高度 = Math.Max(0, window.CaptionHeight - 当前分割线高度)
        Dim 分割线高度 = If(实例对象.SP_标题栏分割线 > 0, 1, 0)
        window.CaptionBottomLineHeight = 分割线高度
        window.CaptionHeight = 原标题栏高度 + 分割线高度
    End Sub

    Private Shared Sub 迁移旧设置字段(设置文本 As String)
        If String.IsNullOrWhiteSpace(设置文本) Then Exit Sub
        Try
            Using doc = JsonDocument.Parse(设置文本)
                Dim root = doc.RootElement

                Dim textValue As String = ""
                If String.IsNullOrWhiteSpace(实例对象.SP_窗口标题文字) AndAlso 读取文本(root, "个性化_窗口标题栏", textValue) Then 实例对象.SP_窗口标题文字 = textValue
                If String.IsNullOrWhiteSpace(实例对象.SP_起始页面顶栏标题) AndAlso 读取文本(root, "个性化_起始页标题", textValue) Then 实例对象.SP_起始页面顶栏标题 = textValue
                If String.IsNullOrWhiteSpace(实例对象.SP_起始页面顶栏副标题) AndAlso 读取文本(root, "个性化_起始页副标题", textValue) Then 实例对象.SP_起始页面顶栏副标题 = textValue

                If 实例对象.Agent权限级别 = 0 Then
                    Dim boolValue As Boolean
                    If 读取布尔(root, "Agent权限_访问编码队列", boolValue) AndAlso boolValue Then
                        实例对象.Agent权限级别 = 1
                    End If
                    If 读取布尔(root, "Agent权限_添加和编辑任务", boolValue) AndAlso boolValue Then
                        实例对象.Agent权限级别 = 1
                    End If
                End If

            End Using
        Catch
        End Try
    End Sub

    Private Shared Function 读取文本(root As JsonElement, name As String, ByRef value As String) As Boolean
        Dim element As JsonElement
        If Not root.TryGetProperty(name, element) OrElse element.ValueKind <> JsonValueKind.String Then Return False
        value = element.GetString()
        Return Not String.IsNullOrWhiteSpace(value)
    End Function

    Private Shared Function 读取布尔(root As JsonElement, name As String, ByRef value As Boolean) As Boolean
        Dim element As JsonElement
        If Not root.TryGetProperty(name, element) OrElse element.ValueKind <> JsonValueKind.True AndAlso element.ValueKind <> JsonValueKind.False Then Return False
        value = element.GetBoolean()
        Return True
    End Function

    Public Shared ReadOnly 自定义图标路径 As String = IO.Path.Combine(Application.StartupPath, "SP_Icon")
    Public Shared ReadOnly 自定义起始页顶栏背景图路径 As String = IO.Path.Combine(Application.StartupPath, "SP_MainTopPanel")
    Public Shared ReadOnly 自定义背景图路径 As String = IO.Path.Combine(Application.StartupPath, "SP_BackImage")
    Private Shared _当前自有图标 As Image
    Private Shared _当前自有起始页顶栏背景图 As Image
    Private Shared _当前自有背景图 As Image
    Private Shared _默认背景图 As Image

    Public Shared Sub 加载SP自定义任务完成音效()
        If Not SP_UnLock Then Exit Sub
        Sound_Finish = 加载自定义音效(实例对象.个性化_任务完成音效, My.Resources.Resource1.完成)
    End Sub

    Public Shared Sub 加载SP自定义任务失败音效()
        If Not SP_UnLock Then Exit Sub
        Sound_Error = 加载自定义音效(实例对象.个性化_任务失败音效, My.Resources.Resource1.错误)
    End Sub

    Private Shared Function 加载自定义音效(file As String, defaultSound As UnmanagedMemoryStream) As Stream
        If Not String.IsNullOrWhiteSpace(file) AndAlso FileIO.FileSystem.FileExists(file) Then
            Try
                Using fileStream As New FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read)
                    Dim soundStream As New MemoryStream()
                    fileStream.CopyTo(soundStream)
                    soundStream.Position = 0
                    Return soundStream
                End Using
            Catch ex As Exception
                MsgBox($"加载自定义音效失败：{ex.Message}", MsgBoxStyle.Critical)
            End Try
        End If

        Try
            defaultSound.Position = 0
        Catch
        End Try
        Return defaultSound
    End Function

    Public Shared Sub 加载SP自定义图标()
        If Not SP_UnLock Then Exit Sub
        If FileIO.FileSystem.FileExists(自定义图标路径) Then
            Dim image = LoadImageFromFile(自定义图标路径, preserveAnimation:=True)
            设置自有面板图片(Form_v6_起始页面.ModernPanel3, image, _当前自有图标)
            设置主窗体图标(CreateIconFromImage(image))
        Else
            设置主窗体图标(CreateIconFromImage(My.Resources.Resource1.AppIcon))
        End If
    End Sub
    Public Shared Sub 加载SP自定义起始页顶栏背景图()
        If Not SP_UnLock Then Exit Sub
        If FileIO.FileSystem.FileExists(自定义起始页顶栏背景图路径) Then
            设置自有面板图片(Form_v6_起始页面.ModernPanel2,
                         LoadImageFromFile(自定义起始页顶栏背景图路径),
                         _当前自有起始页顶栏背景图)
        End If
    End Sub
    Public Shared Sub 加载SP自定义背景图()
        If FileIO.FileSystem.FileExists(设置_v6.自定义背景图路径) Then
            If Not SP_UnLock Then Exit Sub
            设置自有毛玻璃背景图(LoadImageFromFile(设置_v6.自定义背景图路径))
        Else
            设置默认毛玻璃背景图()
        End If
    End Sub

    Public Shared Sub 清除SP自有背景图()
        Dim oldOwned = _当前自有背景图
        _当前自有背景图 = Nothing
        FormMain_v6.ThisIsYourWindow1.BackdropImage = Nothing
        释放自有图片(oldOwned)
    End Sub

    Private Shared Sub 设置自有面板图片(panel As LakeUI.ModernPanel, newImage As Image, ByRef ownedImage As Image)
        Dim oldOwned = ownedImage
        ownedImage = newImage
        panel.Image = newImage
        If oldOwned IsNot newImage Then 释放自有图片(oldOwned)
    End Sub

    Private Shared Sub 设置自有毛玻璃背景图(newImage As Image)
        Dim oldOwned = _当前自有背景图
        _当前自有背景图 = newImage
        FormMain_v6.ThisIsYourWindow1.BackdropImage = newImage
        If oldOwned IsNot newImage Then 释放自有图片(oldOwned)
    End Sub

    Private Shared Sub 设置默认毛玻璃背景图()
        Dim oldOwned = _当前自有背景图
        _当前自有背景图 = Nothing
        FormMain_v6.ThisIsYourWindow1.BackdropImage = 获取默认背景图()
        释放自有图片(oldOwned)
    End Sub

    Private Shared Function 获取默认背景图() As Image
        If _默认背景图 Is Nothing Then _默认背景图 = My.Resources.Resource1.SP_默认背景图
        Return _默认背景图
    End Function

    Private Shared Sub 释放自有图片(image As Image)
        If image Is Nothing Then Return
        Try : image.Dispose() : Catch : End Try
        Try : ReleaseLoadedImageStream(image) : Catch : End Try
    End Sub

    Private Shared Sub 设置主窗体图标(newIcon As Icon)
        If newIcon Is Nothing Then Return
        FormMain_v6.Icon = newIcon
    End Sub

    Public Shared Sub 启动时读取SP解锁器()
        Dim a As String = Path.Combine(Application.StartupPath, "FFmpegFreeUISupporter_v6.dll")
        If Not FileIO.FileSystem.FileExists(a) Then
            Exit Sub
        End If
        Dim targetType As Type = Assembly.LoadFile(a).GetType("FFmpegFreeUISupporter.Entry")
        Dim method As MethodInfo = targetType.GetMethod("Entry", BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.Static)
        method.Invoke(Nothing, Nothing)
    End Sub
End Class
