Imports System.IO
Imports System.Reflection
Imports System.Text.Json
Public Class 插件管理

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
        Task.Run(AddressOf 编码任务.检查是否有可以开始的任务)
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
        Task.Run(AddressOf 编码任务.检查是否有可以开始的任务)
    End Sub

    Shared Sub 将自定义界面名称刷新到下拉框中()
        For Each item In 由插件加载的自定义界面
            Form1.UiComboBox3.Items.Add(item.Key)
        Next
    End Sub




End Class
