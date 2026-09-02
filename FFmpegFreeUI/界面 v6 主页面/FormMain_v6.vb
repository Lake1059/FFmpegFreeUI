Imports System.ComponentModel
Imports System.IO
Imports LakeUI

Public Class FormMain_v6
    Private ReadOnly 插件选项卡页 As New Dictionary(Of String, ModernTabListControl.ModernTabPage)(StringComparer.CurrentCultureIgnoreCase)
    Private 退出确认已完成 As Boolean
    Private 退出时清除所有任务 As Boolean = True
    Private 退出时启动更新器 As Boolean
    Private 退出里程碑检查进行中 As Boolean
    Private 退出里程碑检查已完成 As Boolean

    Private Sub FormMain_v6_Load(发送者 As Object, 事件参数 As EventArgs) Handles Me.Load
        UI同步上下文 = Threading.SynchronizationContext.Current
        设置_v6.启动时读取SP解锁器()
        设置_v6.启动时加载设置()
        界面主题_v6.初始化()
        界面主题_v6.应用窗口圆角设置()
        网络功能.启动时后台获取SPAgent端点()

        设置_v6.加载SP自定义图标()
        设置_v6.加载SP自定义起始页顶栏背景图()
        设置_v6.加载SP自定义背景图()
        绑定主页面选项卡()
        应用窗口样式()

        ModernTabListControl1.SelectedIndex = 0
        ModernTextBox1.Parent = ModernTabListControl1
        其他初始化.执行()
        插件管理.启动时加载插件()
        If 设置_v6.实例对象.是否监听端口 Then 端口监听_v6.启动客户端()
    End Sub

    Private Sub FormMain_v6_Shown(发送者 As Object, 事件参数 As EventArgs) Handles Me.Shown
        ModernTabListControl1.Focus()
        Application.DoEvents()
        启动参数响应_v6.处理首次启动参数()
        询问国内更新服务器()
        网络功能.检查软件本体更新()
        网络功能.检查更新器更新()
        网络功能.获取新闻列表()

        If 设置_v6.实例对象.启用性能计数器 = 0 Then
            MainAppUsageCounter.Start()
            PrecisionTimer1.Start()
        End If

        检查并询问加载未处理任务缓存()
        用户使用统计_v6.启动时后台检查(Me)
    End Sub

    Private Sub ModernTabListControl1_SelectedIndexChanged(发送者 As Object, 事件参数 As EventArgs) Handles ModernTabListControl1.SelectedIndexChanged
        Select Case ModernTabListControl1.SelectedIndex
            Case 5
                Form_v6_性能监控.停止()
                Form_v6_Agent.检查并刷新模型列表()
            Case 10
                Form_v6_性能监控.开始()
            Case Else
                Form_v6_性能监控.停止()
        End Select
    End Sub

    Private Sub PrecisionTimer1_Tick(发送者 As Object, 事件参数 As EventArgs) Handles PrecisionTimer1.Tick
        ThisIsYourWindow1.TitleTextPrivateProtocol = String.Concat(
            "<Title>",
            $"   |   CPU {MainAppUsageCounter.GetCpuUsagePercent():F1}%",
            $"   |   RAM {MainAppUsageCounter.GetActivePrivateWorkingSetBytes() / 1024 / 1024:F0}M / {MainAppUsageCounter.GetCommitSizeBytes() / 1024 / 1024:F0}M",
            $"   |   GPU {MainAppUsageCounter.GetGpuUsagePercent():F1}% {MainAppUsageCounter.GetGpuDedicatedMemoryBytes() / 1024 / 1024:F0}M + {MainAppUsageCounter.GetGpuSharedMemoryBytes() / 1024 / 1024:F0}M")
    End Sub
End Class
