Imports System.IO
Imports System.Text.Json
Imports Microsoft.VisualBasic.FileIO.FileSystem

Public Class 设置_v6

    Public Shared Property 实例对象 As New 设置_v6

    Public Property 图形全局SSAA As Integer = 0
    Public Property 图形DX抗锯齿 As Integer = 0
    Public Property 图形DX文字渲染模式 As Integer = 0
    Public Property 图形动画帧率 As Integer = 60
    Public Property 窗口样式 As Integer = 2

    Public Property 字体 As String = SystemFonts.DefaultFont.FontFamily.Name
    Public Property 编码队列的列宽调整逻辑 As Integer = 0

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

    Public Property 替代进程文件名 As String = ""
    Public Property 覆盖参数传递 As String = ""
    Public Property 转译模式 As Boolean = False

    Public Property 更新服务器选择 As Integer = 0

    Public Property 是否参与用户统计 As Boolean = True

    Public Property 是否监听端口 As Boolean = False
    Public Property 监听的端口 As String = "10591"

    Public Property Agent模型来源 As String = ""
    Public Property AgentEndPoint As String = ""
    Public Property AgentApiKey As String = ""
    Public Property AgentModelId As String = ""

    Public Property Agent权限_编辑参数面板 As Boolean = True
    Public Property Agent权限_添加和编辑任务 As Boolean = True
    Public Property Agent权限_访问编码队列 As Boolean = True

    Public Property SP_窗口标题文字 As String = ""
    Public Property SP_起始页面顶栏标题 As String = ""
    Public Property SP_起始页面顶栏副标题 As String = ""
    Public Property SP_窗口边框颜色 As Color = Color.MediumPurple
    Public Property SP_分层阴影颜色 As Color = Color.MediumPurple
    Public Property SP_毛玻璃模式 As Integer = 0
    Public Property SP_毛玻璃背景来源 As Integer = -1
    Public Property SP_毛玻璃噪点颗粒 As Integer = -1

    Public Property 上次回报活跃信息的日期 As Date
    Public Property 自定义视频编码器列表 As New List(Of String)

    Enum 自动加载预设选项枚举
        不自动加载预设 = 0
        自动加载最后的预设文件 = 1
        自动加载指定的预设文件 = 2
        自动加载上次的全部改动 = 3
    End Enum

    Private Shared ReadOnly 设置文件路径 As String = Path.Combine(Application.StartupPath, "Settings.json")

    Public Shared Sub 退出时保存设置()
        Try
            'Select Case 实例对象.自动加载预设选项
            '    Case 自动加载预设选项枚举.自动加载上次的全部改动
            '        实例对象.最后的预设数据 = New 预设数据类型
            '        预设管理.储存预设(实例对象.最后的预设数据, Form1.常规流程参数页面)
            '    Case Else
            '        实例对象.最后的预设数据 = Nothing
            'End Select
            WriteAllText(设置文件路径, JsonSerializer.Serialize(实例对象, JsonSO), False)
        Catch ex As Exception
            MsgBox($"保存设置失败：{ex.Message}", MsgBoxStyle.Critical)
        End Try
    End Sub

    Public Shared Sub 启动时加载设置()
        If Not FileExists(设置文件路径) Then
            If FontFamily.Families.Any(Function(f) f.Name = "微软雅黑") Then
                实例对象.字体 = "微软雅黑"
            End If
            退出时保存设置()
        Else
            实例对象 = JsonSerializer.Deserialize(Of 设置_v6)(ReadAllText(设置文件路径))
        End If
        Form_v6_设置_LakeUI性能选项.MCB_SSAA.SelectedIndex = 实例对象.图形全局SSAA
        Form_v6_设置_LakeUI性能选项.MCB_GPU抗锯齿.SelectedIndex = 实例对象.图形DX抗锯齿
        Form_v6_设置_LakeUI性能选项.MCB_文字渲染模式.SelectedIndex = 实例对象.图形DX文字渲染模式
        Form_v6_设置_LakeUI性能选项.MCB_动画帧率.Text = 实例对象.图形动画帧率
        Form_v6_设置_LakeUI视觉体验.MCB_窗口样式.SelectedIndex = 实例对象.窗口样式

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
        Form_v6_设置_界面显示.MCB_编码队列列宽调整模式.SelectedIndex = 实例对象.编码队列的列宽调整逻辑

        Form_v6_设置_性能调度.MTB_处理器线程.Text = 实例对象.指定处理器核心
        Form_v6_设置_性能调度.MCB_自动开始数量.SelectedIndex = 实例对象.自动同时运行任务数量选项
        Form_v6_设置_性能调度.MCB_编码队列刷新速度.SelectedIndex = 实例对象.编码队列刷新速度

        If FileExists(实例对象.工作目录) Then
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

        Form_v6_设置_转译辅助.MCB_替代进程的文件名.Text = 实例对象.替代进程文件名
        Form_v6_设置_转译辅助.MTB_覆盖参数传递.Text = 实例对象.覆盖参数传递
        Form_v6_设置_转译辅助.MCB_转译模式.Checked = 实例对象.转译模式

        Form_v6_设置_更新选项.MCB_更新服务器.SelectedIndex = 实例对象.更新服务器选择

        Form_v6_设置_隐私设置.BooleanSwitch1.Checked = 实例对象.是否参与用户统计

        Form_v6_设置_远程调用.BooleanSwitch1.Checked = 实例对象.是否监听端口
        Form_v6_设置_远程调用.ModernTextBox1.Text = 实例对象.监听的端口

        Form_v6_设置_个性化.MCB_毛玻璃模式.SelectedIndex = 实例对象.SP_毛玻璃模式
        Form_v6_设置_个性化.MCB_背景来源.SelectedIndex = 实例对象.SP_毛玻璃背景来源
        Form_v6_设置_个性化.MCB_噪点颗粒.SelectedIndex = 实例对象.SP_毛玻璃噪点颗粒


    End Sub

End Class
