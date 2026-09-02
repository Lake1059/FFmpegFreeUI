Imports System.ComponentModel
Imports System.IO
Imports System.Reflection
Imports LakeUI

Public Class 其他初始化

    Public Shared Sub 执行()
        LakeUI.MessageDialogOptions.BackdropEnabled = Not 界面主题_v6.当前为浅色模式
        LakeUI.MessageDialogOptions.BackdropTintColor = Color.FromArgb(120, 0, 0, 0)
        LakeUI.MessageDialogOptions.BackdropBlurRadius = 30
        LakeUI.MessageDialogOptions.BackdropBlurPasses = 2
        LakeUI.FloatingToolTipForm.BackdropEnabled = True
        LakeUI.FloatingToolTipForm.BackdropTintColor = Color.FromArgb(120, 0, 0, 0)
        LakeUI.FloatingToolTipForm.BackdropBlurRadius = 30
        LakeUI.FloatingToolTipForm.BackdropBlurPasses = 2
    End Sub

End Class

Partial Public Class FormMain_v6
    Private Sub 绑定主页面选项卡()
        Dim 页面绑定 As (索引 As Integer, 页面 As Control, 面板 As ModernPanel)() = {
            (0, Form_v6_起始页面, Form_v6_起始页面.ModernPanel1), (1, Form_v6_编码队列, Form_v6_编码队列.ModernPanel1),
            (3, Form_v6_准备文件, Form_v6_准备文件.ModernPanel1), (4, Form_v6_参数面板, Form_v6_参数面板.ModernPanel1),
            (5, Form_v6_Agent, Form_v6_Agent.ModernPanel1), (8, Form_v6_媒体信息, Form_v6_媒体信息.ModernPanel1),
            (9, Form_v6_调试播放器, Form_v6_调试播放器.ModernPanel1), (10, Form_v6_性能监控, Form_v6_性能监控.ModernPanel1),
            (11, Form_v6_集成工具, Form_v6_集成工具.ModernPanel1), (13, Form_v6_设置, Form_v6_设置.ModernPanel1),
            (14, Form_v6_支持者, Form_v6_支持者.ModernPanel1)
        }
        For Each 绑定项 In 页面绑定
            ModernTabListControl1.Items(绑定项.索引).BoundControl = 绑定项.页面
            绑定选项卡(绑定项.面板)
        Next
    End Sub

    Private Sub 应用窗口样式()
        Select Case 设置_v6.实例对象.窗口样式
            Case 1
                DwmWindowStyle.SetDarkMode(Handle, True)
            Case 2
                ThisIsYourWindow1.Attach(Me)
                If SP_UnLock AndAlso 设置_v6.实例对象.SP_毛玻璃模式 > 0 Then
                    应用毛玻璃控件设置()
                    Dim 边距 = CInt(10 * DeviceDpi / 96)
                    For Each 面板 As ModernPanel In {Form_v6_起始页面.ModernPanel1, Form_v6_准备文件.ModernPanel1, Form_v6_社区_个人中心.ModernPanel1, Form_v6_媒体信息.ModernPanel1, Form_v6_调试播放器.ModernPanel1}
                        If 面板 IsNot Nothing AndAlso Not 面板.IsDisposed Then 面板.Padding = New Padding(边距, 边距, 面板.Padding.Right, 面板.Padding.Bottom)
                    Next
                End If
        End Select
    End Sub

    Public Sub 应用毛玻璃控件设置()
        If Not SP_UnLock OrElse 设置_v6.实例对象.窗口样式 <> 2 OrElse 设置_v6.实例对象.SP_毛玻璃模式 <= 0 Then Return
        For Each 选项卡 As ModernTabListControl In {ModernTabListControl1, Form_v6_参数面板.ModernTabListControl1, Form_v6_集成工具.ModernTabListControl1, Form_v6_设置.ModernTabListControl1}
            If 选项卡 Is Nothing OrElse 选项卡.IsDisposed Then Continue For
            选项卡.TabStripBackColor = Color.Transparent
            选项卡.ContentBackColor = Color.Transparent
        Next
        For Each 选项卡 As ModernTabControl In {Form_v6_参数面板.私有界面_自定义参数.ModernTabControl1, Form_v6_参数面板.私有界面_附加内容.ModernTabControl1}
            If 选项卡 Is Nothing OrElse 选项卡.IsDisposed Then Continue For
            选项卡.TabStripBackColor = Color.Transparent
            选项卡.ContentBackColor = Color.Transparent
        Next
    End Sub

    Private Sub 询问国内更新服务器()
        If 设置_v6.实例对象.是否询问标记_下载服务器选择 OrElse Not Globalization.RegionInfo.CurrentRegion.EnglishName.Trim().Contains("china", StringComparison.OrdinalIgnoreCase) Then Return
        If ExOverlayMsgBox(Me, $"{vbCrLf}检测到当前系统区域为国内，是否选择使用国内镜像站作为下载更新服务器？详细信息可前往设置查看。", MsgBoxStyle.YesNo, "建议国内用户使用镜像服务器") = MsgBoxResult.Yes Then
            设置_v6.实例对象.更新服务器选择 = 2
            Form_v6_设置_更新选项.MCB_更新服务器.SelectedIndex = 设置_v6.实例对象.更新服务器选择
        End If
        设置_v6.实例对象.是否询问标记_下载服务器选择 = True
    End Sub

    Public Sub 绑定选项卡(选项卡的根面板容器 As ModernPanel)
        If 选项卡的根面板容器 Is Nothing OrElse Not SP_UnLock OrElse 设置_v6.实例对象.SP_毛玻璃模式 <= 0 Then Return
        选项卡的根面板容器.BackColor = Color.Transparent
        选项卡的根面板容器.BackColor1 = Color.Transparent
        选项卡的根面板容器.BackgroundSource = Me
    End Sub

    Public Sub 添加插件选项卡(选项卡标题 As String, 面板 As Control)
        Dim 标题 = If(选项卡标题, String.Empty).Trim()
        If String.IsNullOrEmpty(标题) OrElse 面板 Is Nothing Then Return
        面板.Dock = DockStyle.Fill
        Dim 选项卡 As ModernTabListControl.ModernTabPage = Nothing
        If 插件选项卡页.TryGetValue(标题, 选项卡) Then
            选项卡.BoundControl = 面板
        Else
            选项卡 = New ModernTabListControl.ModernTabPage With {.Text = 标题, .BoundControl = 面板}
            ModernTabListControl1.Items.Insert(获取插件选项卡插入位置(), 选项卡)
            插件选项卡页(标题) = 选项卡
        End If
        绑定选项卡(查找可绑定背景映射的插件ModernPanel(面板))
    End Sub

    Private Function 获取插件选项卡插入位置() As Integer
        Dim 已到插件区域 As Boolean
        For 索引 = 0 To ModernTabListControl1.Items.Count - 1
            Dim 选项卡 = ModernTabListControl1.Items(索引)
            If 已到插件区域 AndAlso 选项卡.IsSeparator Then Return 索引
            If String.Equals(选项卡.Text, "集成的工具", StringComparison.CurrentCultureIgnoreCase) Then 已到插件区域 = True
        Next
        Return ModernTabListControl1.Items.Count
    End Function

    Private Function 查找可绑定背景映射的插件ModernPanel(根控件 As Control) As ModernPanel
        If 根控件 Is Nothing Then Return Nothing
        Dim 当前类型 As Type = 根控件.GetType()
        While 当前类型 IsNot Nothing
            Dim 字段 = 当前类型.GetField("ModernPanel1", BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.Instance)
            If 字段 IsNot Nothing Then
                Dim 面板 = TryCast(字段.GetValue(根控件), ModernPanel)
                If 插件ModernPanel可绑定背景映射(面板) Then Return 面板
            End If
            Dim 属性 = 当前类型.GetProperty("ModernPanel1", BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.Instance)
            If 属性 IsNot Nothing Then
                Dim 面板 = TryCast(属性.GetValue(根控件), ModernPanel)
                If 插件ModernPanel可绑定背景映射(面板) Then Return 面板
            End If
            当前类型 = 当前类型.BaseType
        End While
        Return 查找子控件中的插件ModernPanel(根控件)
    End Function

    Private Function 查找子控件中的插件ModernPanel(控件 As Control) As ModernPanel
        If 控件 Is Nothing Then Return Nothing
        Dim 面板 = TryCast(控件, ModernPanel)
        If 插件ModernPanel可绑定背景映射(面板) Then Return 面板
        For Each 子控件 As Control In 控件.Controls
            Dim 子面板 = 查找子控件中的插件ModernPanel(子控件)
            If 子面板 IsNot Nothing Then Return 子面板
        Next
        Return Nothing
    End Function

    Private Shared Function 插件ModernPanel可绑定背景映射(面板 As ModernPanel) As Boolean
        Return 面板 IsNot Nothing AndAlso String.Equals(面板.Name, "ModernPanel1", StringComparison.Ordinal) AndAlso 面板.Dock = DockStyle.Fill
    End Function

    Private Async Sub FormMain_v6_Closing(发送者 As Object, 事件参数 As CancelEventArgs) Handles Me.FormClosing
        事件参数.Cancel = False
        Dim 更新器路径 = Path.Combine(Application.StartupPath, "Updater.exe")
        If Not 退出确认已完成 AndAlso Not 确认退出方式(更新器路径, 事件参数) Then Return
        If Not 退出里程碑检查已完成 Then
            事件参数.Cancel = True
            If 退出里程碑检查进行中 Then Return
            退出里程碑检查进行中 = True
            Try
                Await 用户使用统计_v6.退出时后台检查Async(Me)
            Catch
            Finally
                退出里程碑检查进行中 = False
                退出里程碑检查已完成 = True
            End Try
            BeginInvoke(Sub() Close())
            Return
        End If
        If 退出时清除所有任务 AndAlso 编码队列_v6.获取进行中任务数量() > 0 Then 编码队列_v6.停止所有进行中任务()
        端口监听_v6.停止客户端()
        设置_v6.退出时保存设置()
        If 退出时启动更新器 Then Process.Start(更新器路径)
        If Form_v6_调试播放器.ffplayHandle <> IntPtr.Zero Then Form_v6_调试播放器.停止()
    End Sub

    <CodeAnalysis.SuppressMessage("Performance", "CA1861:不要将常量数组作为参数", Justification:="<挂起>")>
    Private Function 确认退出方式(更新器路径 As String, 事件参数 As CancelEventArgs) As Boolean
        退出时启动更新器 = UpdateAvailable AndAlso FileIO.FileSystem.FileExists(更新器路径)
        If UpdateAvailable AndAlso Not 退出时启动更新器 AndAlso ExOverlayMsgBox(Me, "程序目录下没有更新器，这是意外情况，仍旧退出？", MsgBoxStyle.YesNo) <> MsgBoxResult.Yes Then 事件参数.Cancel = True : Return False
        Dim 进行中任务数量 = 编码队列_v6.获取进行中任务数量()
        Dim 未处理任务数量 = 编码队列_v6.获取未处理任务数量()
        退出时清除所有任务 = True
        If 进行中任务数量 > 0 OrElse 未处理任务数量 > 0 Then
            Dim 提示内容 As New List(Of String)
            If 进行中任务数量 > 0 Then 提示内容.Add($"当前仍有 {进行中任务数量} 个任务正在处理、暂停或等待自动开始。")
            If 未处理任务数量 > 0 Then 提示内容.Add($"当前可保留的未执行任务有 {未处理任务数量} 个。")
            提示内容.Add("请选择退出方式。")
            Select Case ExOverlayMsgBox(Me, String.Join(vbCrLf, 提示内容), {"保留未执行的任务并退出", "清除所有任务然后退出", "取消退出操作"}, "确认退出", MsgBoxStyle.Question, 2)
                Case 0
                    Try
                        编码队列_v6.保存未处理任务缓存()
                        退出时清除所有任务 = False
                    Catch 异常 As Exception
                        ExOverlayMsgBox(Me, "保存未执行任务失败：" & 异常.Message, MsgBoxStyle.Critical, "无法退出")
                        事件参数.Cancel = True : Return False
                    End Try
                Case 1
                    编码队列_v6.删除未处理任务缓存()
                Case Else
                    事件参数.Cancel = True : Return False
            End Select
        End If
        退出确认已完成 = True
        Return True
    End Function

    <CodeAnalysis.SuppressMessage("Performance", "CA1861:不要将常量数组作为参数", Justification:="退出恢复按钮由对话框 API 要求数组")>
    Private Sub 检查并询问加载未处理任务缓存()
        If Not 编码队列_v6.存在未处理任务缓存() Then Return
        Dim 任务数量 = 编码队列_v6.读取未处理任务缓存任务数量()
        If 任务数量 <= 0 Then 编码队列_v6.删除未处理任务缓存() : Return
        Dim 选择结果 = ExOverlayMsgBox(Me, $"检测到上次退出时保留了 {任务数量} 个未执行任务。是否加载到编码队列？", {"加载", "不加载"}, "恢复未执行任务", MsgBoxStyle.Question, 0)
        If 选择结果 = 0 Then
            Try
                Dim 已恢复数量 = 编码队列_v6.加载未处理任务缓存()
                If 已恢复数量 > 0 Then ExFloatingTip(Me, $"已加载 {已恢复数量} 个未执行任务", 1800)
            Catch 异常 As Exception
                ExOverlayMsgBox(Me, "加载未执行任务失败：" & 异常.Message, MsgBoxStyle.Critical, "恢复失败")
            End Try
        Else
            编码队列_v6.删除未处理任务缓存()
        End If
    End Sub
End Class
