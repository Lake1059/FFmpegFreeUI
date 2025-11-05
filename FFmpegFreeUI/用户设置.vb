Imports System.IO
Imports System.Text.Json
Imports Microsoft.VisualBasic.FileIO.FileSystem

Public Class 用户设置
    Public Shared Property 实例对象 As New 用户设置数据结构

    <Serializable>
    Public Class 用户设置数据结构
        Public Property 字体 As String = SystemFonts.DefaultFont.FontFamily.Name
        Public Property 指定处理器核心 As String = ""
        Public Property 自动同时运行任务数量选项 As Integer = 0
        Public Property 有任务时系统保持状态选项 As Integer = 0
        Public Property 提示音选项 As Integer = 0
        Public Property 自动开始任务选项 As Integer = 0
        Public Property 自动重置参数面板的页面选择 As Integer = 0
        Public Property 混淆任务名称 As Integer = 0
        Public Property 工作目录 As String = ""
        Public Property 替代进程文件名 As String = ""
        Public Property 覆盖参数传递 As String = ""
        Public Property 转译模式 As Boolean = False
        Public Property 自动加载预设选项 As 自动加载预设选项枚举 = 自动加载预设选项枚举.不自动加载预设
        Public Property 自动加载预设文件路径 As String = ""
        Public Property 最后的预设数据 As New 预设数据类型
        Public Property V2Tips As Boolean = True
        Public Property 自定义视频编码器列表 As New List(Of String)
    End Class

    Enum 自动加载预设选项枚举
        不自动加载预设 = 0
        自动加载最后的预设文件 = 1
        自动加载指定的预设文件 = 2
        自动加载上次的全部改动 = 3
    End Enum

    Public Shared Sub 退出时保存设置()
        Try
            Select Case 实例对象.自动加载预设选项
                Case 自动加载预设选项枚举.自动加载上次的全部改动
                    实例对象.最后的预设数据 = New 预设数据类型
                    预设管理.储存预设(实例对象.最后的预设数据, Form1.常规流程参数页面)
                Case Else
                    实例对象.最后的预设数据 = Nothing
            End Select

            Dim a = Path.Combine(Application.StartupPath, "Settings.json")
            WriteAllText(a, JsonSerializer.Serialize(实例对象, JsonSO), False)
        Catch ex As Exception
            MsgBox($"保存设置失败：{ex.Message}", MsgBoxStyle.Critical)
        End Try
    End Sub

    Public Shared Sub 启动时加载设置()
        Try
            Dim a = Path.Combine(Application.StartupPath, "Settings.json")
            If Not FileExists(a) Then
                If FontFamily.Families.Any(Function(f) f.Name = "微软雅黑") Then
                    实例对象.字体 = "微软雅黑"
                End If
                退出时保存设置()
            Else
                实例对象 = JsonSerializer.Deserialize(Of 用户设置数据结构)(ReadAllText(a))
            End If

            Form1.UiComboBox字体名称.Text = 实例对象.字体
            Form1.UiTextBox处理器核心.Text = 实例对象.指定处理器核心
            Form1.UiComboBox自动开始最大任务数量.SelectedIndex = 实例对象.自动同时运行任务数量选项
            Form1.UiComboBox有任务时系统状态.SelectedIndex = 实例对象.有任务时系统保持状态选项
            Form1.UiComboBox提示音.SelectedIndex = 实例对象.提示音选项
            Form1.UiComboBox自动开始任务.SelectedIndex = 实例对象.自动开始任务选项
            Form1.UiComboBox自动重置参数面板的页面选择.SelectedIndex = 实例对象.自动重置参数面板的页面选择
            Form1.UiComboBox混淆任务名称.SelectedIndex = 实例对象.混淆任务名称

            Form1.UiTextBoxFFmpeg自定义工作目录.Text = 实例对象.工作目录
            Form1.UiTextBox替代进程的文件名.Text = 实例对象.替代进程文件名
            Form1.UiTextBox覆盖参数传递.Text = 实例对象.覆盖参数传递
            Form1.UiCheckBox转译模式.Checked = 实例对象.转译模式
            Form1.常规流程参数页面.UiComboBox自动加载预设选项.SelectedIndex = 实例对象.自动加载预设选项
            Form1.常规流程参数页面.UiTextBox自动加载的预设文件路径.Text = 实例对象.自动加载预设文件路径
            Select Case Form1.UiComboBox自动开始最大任务数量.SelectedIndex
                Case 0 : 同时运行任务上限 = 1
                Case 1 : 同时运行任务上限 = 2
                Case 2 : 同时运行任务上限 = 3
                Case 3 : 同时运行任务上限 = 4
                Case 4 : 同时运行任务上限 = 5
                Case 5 : 同时运行任务上限 = 6
                Case 6 : 同时运行任务上限 = 7
                Case 7 : 同时运行任务上限 = 8
                Case 8 : 同时运行任务上限 = 9
                Case 9 : 同时运行任务上限 = 10
                Case Else : 同时运行任务上限 = 1
            End Select

        Catch ex As Exception
            MsgBox($"加载设置失败：{ex.Message}", MsgBoxStyle.Critical)
        End Try
    End Sub


End Class
