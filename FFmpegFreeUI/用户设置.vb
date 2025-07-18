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
        Public Property 工作目录 As String = ""
        Public Property 替代进程文件名 As String = ""
        Public Property 覆盖参数传递 As String = ""
        Public Property 转译模式 As Boolean = False
    End Class

    Public Shared Sub 保存()
        Try
            Dim a = Path.Combine(Application.StartupPath, "Settings.json")
            WriteAllText(a, JsonSerializer.Serialize(实例对象, JSON序列化选项), False)
        Catch ex As Exception
            MsgBox($"保存设置失败：{ex.Message}", MsgBoxStyle.Critical)
        End Try
    End Sub

    Public Shared Sub 加载()
        Try
            Dim a = Path.Combine(Application.StartupPath, "Settings.json")
            If Not FileExists(a) Then
                '检查系统中是否有指定字体
                If FontFamily.Families.Any(Function(f) f.Name = "微软雅黑") Then
                    实例对象.字体 = "微软雅黑"
                End If
                保存()
            Else
                实例对象 = JsonSerializer.Deserialize(Of 用户设置数据结构)(ReadAllText(a))
            End If

            Form1.UiComboBox字体名称.Text = 实例对象.字体
            Form1.UiTextBox处理器核心.Text = 实例对象.指定处理器核心
            Form1.UiComboBox自动开始最大任务数量.SelectedIndex = 实例对象.自动同时运行任务数量选项
            Form1.UiComboBox有任务时系统状态.SelectedIndex = 实例对象.有任务时系统保持状态选项
            Form1.UiComboBox提示音.SelectedIndex = 实例对象.提示音选项
            Form1.UiTextBoxFFmpeg自定义工作目录.Text = 实例对象.工作目录
            Form1.UiTextBox替代进程的文件名.Text = 实例对象.替代进程文件名
            Form1.UiTextBox覆盖参数传递.Text = 实例对象.覆盖参数传递
            Form1.UiCheckBox转译模式.Checked = 实例对象.转译模式

            Select Case Form1.UiComboBox自动开始最大任务数量.SelectedIndex
                Case 0 : 同时运行任务上限 = 1
                Case 1 : 同时运行任务上限 = 2
                Case 2 : 同时运行任务上限 = 3
                Case 3 : 同时运行任务上限 = 5
                Case 4 : 同时运行任务上限 = 10
                Case Else : 同时运行任务上限 = 1
            End Select

        Catch ex As Exception
            MsgBox($"加载设置失败：{ex.Message}", MsgBoxStyle.Critical)
        End Try
    End Sub


End Class
