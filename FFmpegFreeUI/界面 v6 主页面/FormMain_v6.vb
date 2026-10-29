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
        设置_v6.初始化界面外观()
        '网络功能.启动时后台获取SPAgent端点()

        设置_v6.加载SP自定义图标()
        设置_v6.加载SP自定义起始页顶栏背景图()
        设置_v6.加载SP自定义背景图()
        绑定主页面选项卡()
        Form_v6_参数面板.初始化默认参数()
        AddHandler 编码队列_v6.队列已变化, AddressOf 请求刷新任务数量选项卡
        AddHandler 编码队列_v6.任务已更新, AddressOf 编码任务已更新
        AddHandler Form_v6_Agent.正在推理任务数量已变化, AddressOf 请求刷新任务数量选项卡
        刷新任务数量选项卡()
        应用窗口样式()

        ModernTabListControl1.SelectedIndex = 0
        ModernTextBox1.Parent = ModernTabListControl1
        插件管理.启动时加载插件()
        If 设置_v6.实例对象.是否监听端口 Then 端口监听_v6.启动客户端()
    End Sub

    Private Sub FormMain_v6_FormClosed(发送者 As Object, 事件参数 As FormClosedEventArgs) Handles Me.FormClosed
        RemoveHandler 编码队列_v6.队列已变化, AddressOf 请求刷新任务数量选项卡
        RemoveHandler 编码队列_v6.任务已更新, AddressOf 编码任务已更新
        RemoveHandler Form_v6_Agent.正在推理任务数量已变化, AddressOf 请求刷新任务数量选项卡
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

    Private Sub 编码任务已更新(任务 As 编码任务_v6)
        请求刷新任务数量选项卡()
    End Sub

    Private Sub 请求刷新任务数量选项卡()
        If IsDisposed OrElse Disposing OrElse Not IsHandleCreated Then Return
        If InvokeRequired Then
            Try
                BeginInvoke(Sub() 刷新任务数量选项卡())
            Catch ex As InvalidOperationException
            End Try
            Return
        End If
        刷新任务数量选项卡()
    End Sub

    Private Sub 刷新任务数量选项卡()
        If IsDisposed OrElse Disposing OrElse ModernTabListControl1 Is Nothing Then Return
        Dim queueCount = 编码队列_v6.获取进行中任务数量()
        Dim agentCount = Form_v6_Agent.获取正在推理任务数量()
        ModernTabListControl1.Items(1).Text = If(queueCount > 0, $"编码队列 ({queueCount})", "编码队列")
        ModernTabListControl1.Items(5).Text = If(agentCount > 0, $"Agent 智能体 ({agentCount})", "Agent 智能体")
    End Sub
End Class
