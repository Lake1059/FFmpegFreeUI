Imports System.IO
Imports System.Text.Json
Imports Microsoft.VisualBasic.FileIO.FileSystem

Public Class 设置_v6

    Public Shared Property 实例对象 As New 设置_v6

    Public Property 图形SSAA As Integer = 0
    Public Property 图形平滑模式 As Integer = 0
    Public Property 图形像素偏移模式 As Integer = 0
    Public Property 图形插值模式 As Integer = 0
    Public Property 图形合成质量 As Integer = 0
    Public Property 图形动画帧率 As Integer = 60
    Public Property 窗口样式 As Integer = 2
    Public Property 窗口边框颜色 As Color = Color.MediumPurple
    Public Property 窗口边框厚度 As Integer = 1
    Public Property 窗口标题栏背景颜色 As Color = Color.FromArgb(48, 48, 48)
    Public Property 窗口阴影模式 As Integer = 2
    Public Property 窗口分层阴影颜色 As Color = Color.MediumPurple
    Public Property 窗口分层阴影扩展范围 As Integer = 15
    Public Property 窗口控制按钮位置 As Integer = 0
    Public Property 窗口标题文字对齐方位 As Integer = 1
    Public Property 窗口标题渲染透明背景 As Integer = 0

    Public Property 字体 As String = SystemFonts.DefaultFont.FontFamily.Name
    Public Property 编码队列的列宽调整逻辑 As Integer = 0
    Public Property 指定处理器核心 As String = ""
    Public Property 自动同时运行任务数量选项 As Integer = 0
    Public Property 编码队列刷新速度 As Integer = 2
    Public Property 有任务时系统保持状态选项 As Integer = 0
    Public Property 提示音选项 As Integer = 0
    Public Property 自动开始任务选项 As Integer = 0
    Public Property 自动重置参数面板的页面选择 As Integer = 0
    Public Property 混淆任务名称 As Integer = 0
    Public Property 打开独立参数面板时自动切到预设管理页面 As Integer = 0
    Public Property 任务失败自动删除输出文件 As Integer = 0
    Public Property 工作目录 As String = ""
    Public Property 替代进程文件名 As String = ""
    Public Property 覆盖参数传递 As String = ""
    Public Property 转译模式 As Boolean = False
    Public Property 自动加载预设选项 As 自动加载预设选项枚举 = 自动加载预设选项枚举.不自动加载预设
    Public Property 自动加载预设文件路径 As String = ""
    Public Property 最后的预设数据 As New 预设数据类型
    Public Property 是否参与用户统计 As Boolean = True
    Public Property 是否监听端口 As Boolean = False
    Public Property 监听的端口 As String = ""

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
            Select Case 实例对象.自动加载预设选项
                Case 自动加载预设选项枚举.自动加载上次的全部改动
                    实例对象.最后的预设数据 = New 预设数据类型
                    预设管理.储存预设(实例对象.最后的预设数据, Form1.常规流程参数页面)
                Case Else
                    实例对象.最后的预设数据 = Nothing
            End Select
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
            Exit Sub
        End If



    End Sub

End Class
