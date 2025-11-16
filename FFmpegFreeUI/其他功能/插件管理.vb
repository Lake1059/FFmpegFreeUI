Imports System.IO
Imports System.Reflection
Imports System.Text.Json
Imports Sunny.UI
Public Class 插件管理

    Public Shared Property 是否已解锁个性化功能 As Boolean = False

    Public Shared Sub 启动时读取个性化功能解锁器()
        Dim a As String = Path.Combine(Application.StartupPath, "FFmpegFreeUISupporter.dll")
        If Not FileIO.FileSystem.FileExists(a) Then
            Form1.加载自定义图标(Nothing)
            Form1.设置页面.Panel1.Visible = True
            Form1.设置页面.Panel3.Visible = False
            Exit Sub
        End If
        Dim 程序集 As Assembly = Assembly.LoadFile(a)
        Dim 获取类型 As Type = 程序集.GetType(程序集.GetName.Name & ".Entry")
        Dim 创建实例 As Object = Activator.CreateInstance(获取类型)
        Dim 实现方法 As MethodInfo = 获取类型.GetMethod("Entry")
        实现方法.Invoke(创建实例, Array.Empty(Of Object)())
        If 是否已解锁个性化功能 Then
            Form1.设置页面.Panel1.Visible = False
            Form1.设置页面.Panel3.Visible = True

            If FileIO.FileSystem.FileExists(用户设置.实例对象.个性化_软件图标) Then
                Form1.设置页面.Label9.Text = 用户设置.实例对象.个性化_软件图标
                Form1.加载自定义图标(用户设置.实例对象.个性化_软件图标)
            Else
                Form1.设置页面.Label9.Text = ""
                Form1.加载自定义图标(Nothing)
                用户设置.实例对象.个性化_软件图标 = ""
            End If


            If FileIO.FileSystem.FileExists(用户设置.实例对象.个性化_任务完成音效) Then
                Form1.设置页面.Label11.Text = 用户设置.实例对象.个性化_任务完成音效
                Form1.加载自定义任务完成音效(用户设置.实例对象.个性化_任务完成音效)
            Else
                Form1.设置页面.Label11.Text = ""
                用户设置.实例对象.个性化_任务完成音效 = ""
            End If

            If FileIO.FileSystem.FileExists(用户设置.实例对象.个性化_任务失败音效) Then
                Form1.设置页面.Label10.Text = 用户设置.实例对象.个性化_任务失败音效
                Form1.加载自定义任务失败音效(用户设置.实例对象.个性化_任务失败音效)
            Else
                Form1.设置页面.Label10.Text = ""
                用户设置.实例对象.个性化_任务失败音效 = ""
            End If

            If 用户设置.实例对象.个性化_起始页标题 <> "" Then
                Form1.设置页面.UiTextBox1.Text = 用户设置.实例对象.个性化_起始页标题
                Form1.Label主标题.Text = 用户设置.实例对象.个性化_起始页标题
            Else
                Form1.设置页面.UiTextBox1.Text = ""
            End If

            If 用户设置.实例对象.个性化_起始页副标题 <> "" Then
                Form1.设置页面.UiTextBox2.Text = 用户设置.实例对象.个性化_起始页副标题
                Form1.Label副标题.Text = 用户设置.实例对象.个性化_起始页副标题
            Else
                Form1.设置页面.UiTextBox2.Text = ""
            End If

            If 用户设置.实例对象.个性化_窗口标题栏 <> "" Then
                Form1.设置页面.UiTextBox3.Text = 用户设置.实例对象.个性化_窗口标题栏
                Form1.Text = 用户设置.实例对象.个性化_窗口标题栏
            Else
                Form1.设置页面.UiTextBox3.Text = ""
            End If

            If FileIO.FileSystem.FileExists(用户设置.实例对象.个性化_起始页背景图) Then
                Form1.设置页面.Label14.Text = 用户设置.实例对象.个性化_起始页背景图
                Form1.Panel5.BackgroundImage = LoadImageFromFile(用户设置.实例对象.个性化_起始页背景图)
            Else
                Form1.设置页面.Label14.Text = ""
                用户设置.实例对象.个性化_起始页背景图 = ""
            End If

        Else
            Form1.加载自定义图标(Nothing)
            Form1.设置页面.Panel1.Visible = True
            Form1.设置页面.Panel3.Visible = False
        End If
    End Sub

    Public Shared Property 由插件加载的自定义界面 As New Dictionary(Of String, Control)

    Public Shared Sub 启动时加载插件()
        Dim 插件文件夹路径 As String = Path.Combine(Application.StartupPath, "Plugin")
        If Not FileIO.FileSystem.DirectoryExists(插件文件夹路径) Then Exit Sub

        For Each 插件文件 In IO.Directory.GetFiles(插件文件夹路径, "*.3fui.dll")
            Dim 程序集 = Assembly.LoadFrom(插件文件)
            Dim Entry类 As Type = 程序集.GetType(程序集.GetName.Name & ".Entry")

            If Entry类 Is Nothing Then
                MsgBox($"{插件文件} 不包含 Entry 类，无法加载此插件", MsgBoxStyle.Critical)
                Continue For
            End If

            Dim Entry类的实例 As Object = Activator.CreateInstance(Entry类)

            If Entry类.GetMethod("SetHost_AddCustomWinformPanel") IsNot Nothing Then
                Dim myCallback As Action(Of String, Control) = AddressOf 添加自定义Winform面板
                Entry类.GetMethod("SetHost_AddCustomWinformPanel").Invoke(Entry类的实例, New Object() {myCallback})
            End If
            If Entry类.GetMethod("SetHost_AddCustomWpfPanel") IsNot Nothing Then
                Dim myCallback As Action(Of String, System.Windows.UIElement) = AddressOf 添加自定义Wpf面板
                Entry类.GetMethod("SetHost_AddCustomWpfPanel").Invoke(Entry类的实例, New Object() {myCallback})
            End If

            If Entry类.GetMethod("SetHost_AddMissionToQueueWithArgs") IsNot Nothing Then
                Dim myCallback As Action(Of String, String, String, String) = AddressOf 使用命令行添加任务到编码队列
                Entry类.GetMethod("SetHost_AddMissionToQueueWithArgs").Invoke(Entry类的实例, New Object() {myCallback})
            End If

            If Entry类.GetMethod("SetHost_AddMissionToQueueWith3fuiFile") IsNot Nothing Then
                Dim myCallback As Action(Of String, String, String, String) = AddressOf 使用预设文件添加任务到编码队列
                Entry类.GetMethod("SetHost_AddMissionToQueueWith3fuiFile").Invoke(Entry类的实例, New Object() {myCallback})
            End If

            Dim Entry方法 As MethodInfo = Entry类.GetMethod("Entry")
            If Entry方法 Is Nothing Then
                MsgBox($"{插件文件} 找不到在 Entry 类中的 Entry 方法（需要是共享\静态的，VB 使用 Shared，C# 使用 static），此插件未执行初始化", MsgBoxStyle.Exclamation)
                Continue For
            End If
            Entry方法.Invoke(Nothing, Nothing)
        Next

        将自定义界面名称刷新到下拉框中()

    End Sub

    Public Shared Sub 添加自定义Winform面板(Name As String, C As Control)
        由插件加载的自定义界面(Name) = C
    End Sub
    Public Shared Sub 添加自定义Wpf面板(Name As String, UIE As System.Windows.UIElement)
        Dim host As New Integration.ElementHost With {.Child = UIE}
        由插件加载的自定义界面(Name) = host
    End Sub

    Public Shared Sub 使用命令行添加任务到编码队列(FFmpegArg As String, FileName As String, OutputPath As String, Optional InputPath As String = "")
        Dim m As New 编码任务.单片任务 With {.命令行 = FFmpegArg, .输出文件 = OutputPath}
        If InputPath <> "" Then m.输入文件 = InputPath
        Dim i2 As New ListViewItem(FileName)
        i2.SubItems.AddRange("未处理", "", "", "", "", "", "")
        Form1.ListView1.Items.Add(i2)
        m.列表视图项 = i2
        编码任务.队列.Add(m)
        编码任务.检查并开始新任务的定时器.Enabled = True
    End Sub

    Public Shared Sub 使用预设文件添加任务到编码队列(File_3FUI_JsonPath As String, FileName As String, OutputPath As String, Optional InputPath As String = "")
        If Not FileIO.FileSystem.FileExists(File_3FUI_JsonPath) Then
            MsgBox("指定的预设文件不存在", MsgBoxStyle.Critical)
            Exit Sub
        End If
        Dim m As New 编码任务.单片任务 With {.预设数据 = JsonSerializer.Deserialize(Of 预设数据类型)(File.ReadAllText(File_3FUI_JsonPath)), .输出文件 = OutputPath}
        If InputPath <> "" Then m.输入文件 = InputPath
        Dim i2 As New ListViewItem(FileName)
        i2.SubItems.AddRange("未处理", "", "", "", "", "", "")
        Form1.ListView1.Items.Add(i2)
        m.列表视图项 = i2
        编码任务.队列.Add(m)
        编码任务.检查并开始新任务的定时器.Enabled = True
    End Sub

    Shared Sub 将自定义界面名称刷新到下拉框中()
        For Each item In 由插件加载的自定义界面
            Form1.UiComboBox3.Items.Add(item.Key)
        Next
    End Sub





End Class
