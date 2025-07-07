Imports System.Windows
Imports System.Windows.Forms

Public Class Entry

    Public Shared Sub Entry()
        '初始化的代码都写这里，在 3FUI 启动后执行
        AddCustomWinformPanel()
        AddCustomWpfPanel()
        '需要注意的是我不会在其他时候刷新自定义面板列表
        '所以你必须在这个 Entry 里添加自定义面板
        '否则无法在下拉框中找到你添加的自定义面板
    End Sub

    '##################################################
    '########## 添加自定义 WinForm 面板的回调函数 ##########
    '##################################################
    Public Shared Property HostCall_AddCustomWinformPanel As Action(Of String, Control)
    Public Shared Sub SetHost_AddCustomWinformPanel(action As Object)
        HostCall_AddCustomWinformPanel = CType(action, Action(Of String, Control))
    End Sub
    Public Shared Sub AddCustomWinformPanel()
        '将自定义 WinForm 面板添加到主界面
        HostCall_AddCustomWinformPanel.Invoke("插件示例：WinForm 控件", New UserControlWinform1)
    End Sub

    '##################################################
    '########## 添加自定义 WPF 面板的回调函数 ##########
    '##################################################
    Public Shared Property HostCall_AddCustomWpfPanel As Action(Of String, UIElement)
    Public Shared Sub SetHost_AddCustomWpfPanel(action As Object)
        HostCall_AddCustomWpfPanel = CType(action, Action(Of String, UIElement))
    End Sub
    Public Shared Sub AddCustomWpfPanel()
        '将自定义 WPF 面板添加到主界面
        HostCall_AddCustomWpfPanel.Invoke("插件示例：WPF 控件", New UserControlWpf1)
    End Sub

    '##################################################
    '########## 添加任务到编码队列的回调函数 ##########
    '##################################################
    Public Shared Property HostCall_AddMissionToQueue As Action(Of String, String, String)
    Public Shared Sub SetHost_AddMissionToQueue(action As Object)
        HostCall_AddMissionToQueue = CType(action, Action(Of String, String, String))
    End Sub
    Public Shared Sub AddMissionToQueue()
        '将任务添加到编码队列中
        '在你的自定义控件中调用这个来添加任务
        HostCall_AddMissionToQueue.Invoke("给 ffmpeg 的参数，不要以 ffmpeg 开始", "在编码队列里显示的文件名，也可以用来显示其他信息", "输出文件的路径在哪，用于编码队列中的定位输出功能")
    End Sub

End Class