Imports System.IO
Imports System.Text.Json
Imports Microsoft.VisualBasic.FileIO.FileSystem

Public Class 用户设置
    Public Shared Property 实例对象 As New 用户设置数据结构

    <Serializable>
    Public Class 用户设置数据结构
        Public Property 字体 As String = SystemFonts.DefaultFont.FontFamily.Name
        Public Property 语言 As String = "zh"
        Public Property 界面修正_选项卡文字增加左侧空格 As Integer = 0
        Public Property 界面修正_增加使用文字渲染尺寸来调节的标签的尺寸 As Integer = 0
        Public Property 界面修正_校准列表视图的项高度 As Integer = 0
        Public Property 界面修正_编码队列的列宽调整逻辑 As Integer = 0
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

        Public Property 个性化_软件图标 As String = ""
        Public Property 个性化_任务完成音效 As String = ""
        Public Property 个性化_任务失败音效 As String = ""
        Public Property 个性化_起始页标题 As String = ""
        Public Property 个性化_起始页副标题 As String = ""
        Public Property 个性化_窗口标题栏 As String = ""
        Public Property 个性化_起始页背景图 As String = ""
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

            Select Case 实例对象.语言
                Case "zh" : Form1.设置页面.UiComboBox语言选择.SelectedIndex = 0
                Case "en" : Form1.设置页面.UiComboBox语言选择.SelectedIndex = 1
            End Select
            加载多语言()
            Form1.设置页面.UiComboBox字体名称.Text = 实例对象.字体
            If Form1.设置页面.UiComboBox字体名称.Items.Contains("微软雅黑") Then Form1.设置页面.UiComboBox字体名称.Font = New Font("微软雅黑", Form1.设置页面.UiComboBox字体名称.Font.Size)
            SetControlFont(用户设置.实例对象.字体, Form1, {Form1.设置页面.UiComboBox字体名称}, True)
            编码队列右键菜单.重设字体()
            编码队列管理选项.重设字体()

            If 用户设置.实例对象.界面修正_选项卡文字增加左侧空格 > 0 Then
                For Each page As TabPage In Form1.UiTabControlMenu1.TabPages
                    page.Text = Space(用户设置.实例对象.界面修正_选项卡文字增加左侧空格) & page.Text
                Next
                For Each page As TabPage In Form1.常规流程参数页面.UiTabControlMenu1.TabPages
                    page.Text = Space(用户设置.实例对象.界面修正_选项卡文字增加左侧空格) & page.Text
                Next
                For Each page As TabPage In Form1.设置页面.UiTabControlMenu1.TabPages
                    page.Text = Space(用户设置.实例对象.界面修正_选项卡文字增加左侧空格) & page.Text
                Next
            End If
            Form1.设置页面.UiComboBox修正_选项卡文字增加左侧空格.Text = 实例对象.界面修正_选项卡文字增加左侧空格
            Form1.设置页面.UiComboBox修正_增加标签尺寸.Text = 实例对象.界面修正_增加使用文字渲染尺寸来调节的标签的尺寸
            Form1.设置页面.UiComboBox修正_列表视图项高度.Text = 实例对象.界面修正_校准列表视图的项高度
            Form1.设置页面.UiComboBox修正_编码队列列宽调整逻辑.SelectedIndex = 实例对象.界面修正_编码队列的列宽调整逻辑

            Form1.设置页面.UiTextBox处理器核心.Text = 实例对象.指定处理器核心
            Form1.设置页面.UiComboBox自动开始最大任务数量.SelectedIndex = 实例对象.自动同时运行任务数量选项
            Form1.设置页面.UiComboBox编码队列刷新速度.SelectedIndex = 实例对象.编码队列刷新速度

            Form1.设置页面.UiComboBox有任务时系统状态.SelectedIndex = 实例对象.有任务时系统保持状态选项
            Form1.设置页面.UiComboBox提示音.SelectedIndex = 实例对象.提示音选项
            Form1.设置页面.UiComboBox自动开始任务.SelectedIndex = 实例对象.自动开始任务选项
            Form1.设置页面.UiComboBox自动重置参数面板的页面选择.SelectedIndex = 实例对象.自动重置参数面板的页面选择
            Form1.设置页面.UiComboBox混淆任务名称.SelectedIndex = 实例对象.混淆任务名称
            Form1.设置页面.UiComboBox独立参数面板自动切换页面.SelectedIndex = 实例对象.打开独立参数面板时自动切到预设管理页面
            Form1.设置页面.UiComboBox任务失败自动删除输出文件.SelectedIndex = 实例对象.任务失败自动删除输出文件

            Form1.设置页面.UiTextBoxFFmpeg自定义工作目录.Text = 实例对象.工作目录
            Form1.设置页面.UiTextBox替代进程的文件名.Text = 实例对象.替代进程文件名
            Form1.设置页面.UiTextBox覆盖参数传递.Text = 实例对象.覆盖参数传递
            Form1.设置页面.UiCheckBox转译模式.Checked = 实例对象.转译模式

            Form1.设置页面.UiSwitch参与用户统计.Active = 实例对象.是否参与用户统计

            Form1.设置页面.UiSwitch端口监听.Active = 实例对象.是否监听端口
            Form1.设置页面.UiTextBox监听的端口.Text = 实例对象.监听的端口

            Form1.常规流程参数页面.UiComboBox自动加载预设选项.SelectedIndex = 实例对象.自动加载预设选项
            Form1.常规流程参数页面.UiTextBox自动加载的预设文件路径.Text = 实例对象.自动加载预设文件路径
            Select Case Form1.设置页面.UiComboBox自动开始最大任务数量.SelectedIndex
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

            Form1.ImageList1.ImageSize = New Size(1, 30 * Form1.DPI + 实例对象.界面修正_校准列表视图的项高度 * Form1.DPI)

            Select Case 实例对象.编码队列刷新速度
                Case 0 : Form1.任务进度更新计时器.Interval = 200
                Case 1 : Form1.任务进度更新计时器.Interval = 500
                Case 2 : Form1.任务进度更新计时器.Interval = 1000
                Case 3 : Form1.任务进度更新计时器.Interval = 1500
                Case 4 : Form1.任务进度更新计时器.Interval = 2000
            End Select

        Catch ex As Exception
        MsgBox($"加载设置失败：{ex.Message}", MsgBoxStyle.Critical)
        End Try
    End Sub


End Class
