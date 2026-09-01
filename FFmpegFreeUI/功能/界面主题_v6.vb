Imports System.Collections.Concurrent
Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports System.Text.RegularExpressions
Imports LakeUI
Imports Microsoft.Win32

Public Module 界面主题_v6
    Private Const Windows个性化注册表路径 As String = "Software\Microsoft\Windows\CurrentVersion\Themes\Personalize"
    Private Const Windows应用浅色键名 As String = "AppsUseLightTheme"

    Private ReadOnly 颜色属性缓存 As New ConcurrentDictionary(Of Type, PropertyInfo())
    Private ReadOnly 控件快照表 As New ConditionalWeakTable(Of Object, 控件主题快照)
    Private ReadOnly 已挂接控件表 As New ConditionalWeakTable(Of Control, Object)
    Private ReadOnly 挂接标记 As New Object()
    Private ReadOnly Html颜色表达式 As New Regex("(?i)(color\s*:\s*)(#[0-9a-f]{6}|[a-z]+)", RegexOptions.Compiled Or RegexOptions.CultureInvariant)

    Private _已初始化 As Boolean
    Private _当前浅色 As Boolean

    Private NotInheritable Class 控件主题快照
        Public ReadOnly 颜色 As New Dictionary(Of PropertyInfo, Color)
        Public Property Html文本 As String
        Public Property 有Html颜色 As Boolean
    End Class

    Public ReadOnly Property 当前为浅色模式 As Boolean
        Get
            Return _当前浅色
        End Get
    End Property

    ''' <summary>初始化系统主题监听，并立即将当前 Windows“应用模式”应用到已加载界面。</summary>
    Public Sub 初始化()
        If _已初始化 Then
            刷新主题(True)
            Return
        End If

        _已初始化 = True
        AddHandler SystemEvents.UserPreferenceChanged, AddressOf 系统首选项已更改
        AddHandler Application.Idle, AddressOf 应用空闲
        AddHandler Application.ApplicationExit, AddressOf 应用退出
        刷新主题(True)
    End Sub

    ''' <summary>0=跟随 Windows 应用模式；1=始终使用深色。</summary>
    Public Sub 刷新主题(Optional 强制刷新 As Boolean = False)
        Dim 浅色 = If(设置_v6.实例对象.界面主题 = 1, False, 读取Windows应用浅色模式())
        If Not 强制刷新 AndAlso 浅色 = _当前浅色 Then Return

        _当前浅色 = 浅色
        应用LakeUI对话框主题(浅色)

        Dim forms = Application.OpenForms.Cast(Of Form).ToArray()
        Dim mainForm = forms.OfType(Of FormMain_v6)().FirstOrDefault()
        If mainForm IsNot Nothing Then
            ' ThisIsYourWindow 是非可视 Component，不属于 WinForms Control 树，需要单独套用主题。
            应用对象颜色(mainForm.ThisIsYourWindow1, 浅色)
        End If
        For Each form In forms
            应用控件树(form, True)
        Next
    End Sub

    ''' <summary>根据当前设置统一应用圆角。LakeUI 特别呈现由 ThisIsYourWindow 管理，其余窗口直接使用 DWM。</summary>
    Public Sub 应用窗口圆角设置()
        Dim 支持圆角 = DwmWindowStyle.IsCornerModeSupported
        Dim mode = If(支持圆角 AndAlso 设置_v6.实例对象.窗口圆角 = 1,
                      DwmWindowStyle.CornerMode.Round,
                      DwmWindowStyle.CornerMode.Square)

        Try
            FormMain_v6.ThisIsYourWindow1.WindowCornerMode = mode
        Catch
        End Try

        If 设置_v6.实例对象.窗口样式 = 2 Then Return
        For Each form In Application.OpenForms.Cast(Of Form).ToArray()
            If form.IsDisposed OrElse Not form.IsHandleCreated Then Continue For
            Try
                DwmWindowStyle.SetCornerMode(form.Handle, mode)
            Catch
            End Try
        Next
    End Sub

    Public Function 读取Windows应用浅色模式() As Boolean
        Try
            Using key = Registry.CurrentUser.OpenSubKey(Windows个性化注册表路径, False)
                Dim value = key?.GetValue(Windows应用浅色键名, 1)
                Return Convert.ToInt32(value, Globalization.CultureInfo.InvariantCulture) <> 0
            End Using
        Catch
            ' Windows 在未显式配置时默认使用浅色应用模式。
            Return True
        End Try
    End Function

    Private Sub 系统首选项已更改(sender As Object, e As UserPreferenceChangedEventArgs)
        If Not _已初始化 OrElse 设置_v6.实例对象.界面主题 <> 0 Then Return
        界面线程执行(
            Sub(state)
                If _已初始化 Then 刷新主题(False)
            End Sub)
    End Sub

    Private Sub 应用空闲(sender As Object, e As EventArgs)
        If Not _已初始化 Then Return
        For Each form In Application.OpenForms.Cast(Of Form).ToArray()
            应用控件树(form, False)
        Next
    End Sub

    Private Sub 应用退出(sender As Object, e As EventArgs)
        If Not _已初始化 Then Return
        _已初始化 = False
        RemoveHandler SystemEvents.UserPreferenceChanged, AddressOf 系统首选项已更改
        RemoveHandler Application.Idle, AddressOf 应用空闲
        RemoveHandler Application.ApplicationExit, AddressOf 应用退出
    End Sub

    Private Sub 应用控件树(control As Control, 强制刷新 As Boolean)
        If control Is Nothing OrElse control.IsDisposed Then Return

        Dim marker As Object = Nothing
        Dim 首次挂接 = Not 已挂接控件表.TryGetValue(control, marker)
        If 首次挂接 Then
            已挂接控件表.Add(control, 挂接标记)
            AddHandler control.ControlAdded, AddressOf 控件已添加
        ElseIf Not 强制刷新 Then
            ' 已挂接的树会通过 ControlAdded 捕获新增子控件；空闲扫描无需反复遍历整棵 UI 树。
            Return
        End If

        If 首次挂接 OrElse 强制刷新 Then
            应用对象颜色(control, _当前浅色)
            If TypeOf control Is Form Then 应用窗体Dwm外观(DirectCast(control, Form))
            control.Invalidate()
        End If

        For Each child As Control In control.Controls
            应用控件树(child, 强制刷新)
        Next
    End Sub

    Private Sub 控件已添加(sender As Object, e As ControlEventArgs)
        If Not _已初始化 OrElse e.Control Is Nothing Then Return
        应用控件树(e.Control, True)
    End Sub

    Private Sub 应用窗体Dwm外观(form As Form)
        If form Is Nothing OrElse form.IsDisposed OrElse Not form.IsHandleCreated Then Return
        Try
            DwmWindowStyle.SetDarkMode(form.Handle, Not _当前浅色)
        Catch
        End Try

        If 设置_v6.实例对象.窗口样式 = 2 Then Return
        Try
            Dim mode = If(DwmWindowStyle.IsCornerModeSupported AndAlso 设置_v6.实例对象.窗口圆角 = 1,
                          DwmWindowStyle.CornerMode.Round,
                          DwmWindowStyle.CornerMode.Square)
            DwmWindowStyle.SetCornerMode(form.Handle, mode)
        Catch
        End Try
    End Sub

    Private Sub 应用对象颜色(target As Object, 浅色 As Boolean)
        Dim snapshot = 获取或创建快照(target)
        For Each pair In snapshot.颜色
            Try
                pair.Key.SetValue(target, If(浅色, 转换为浅色(pair.Value, pair.Key.Name), pair.Value))
            Catch
            End Try
        Next

        If snapshot.有Html颜色 AndAlso TypeOf target Is HtmlColorLabel Then
            Try
                DirectCast(target, HtmlColorLabel).Text = If(浅色, 转换Html为浅色(snapshot.Html文本), snapshot.Html文本)
            Catch
            End Try
        End If
    End Sub

    Private Function 获取或创建快照(target As Object) As 控件主题快照
        Dim existing As 控件主题快照 = Nothing
        If 控件快照表.TryGetValue(target, existing) Then Return existing

        Dim snapshot As New 控件主题快照()
        For Each prop In 获取颜色属性(target.GetType())
            Try
                snapshot.颜色(prop) = DirectCast(prop.GetValue(target), Color)
            Catch
            End Try
        Next

        If TypeOf target Is HtmlColorLabel Then
            Try
                snapshot.Html文本 = DirectCast(target, HtmlColorLabel).Text
                snapshot.有Html颜色 = Not String.IsNullOrEmpty(snapshot.Html文本) AndAlso snapshot.Html文本.IndexOf("color:", StringComparison.OrdinalIgnoreCase) >= 0
            Catch
            End Try
        End If

        控件快照表.Add(target, snapshot)
        Return snapshot
    End Function

    Private Function 获取颜色属性(type As Type) As PropertyInfo()
        Return 颜色属性缓存.GetOrAdd(
            type,
            Function(t)
                Return t.GetProperties(BindingFlags.Instance Or BindingFlags.Public).
                    Where(Function(p) p.PropertyType Is GetType(Color) AndAlso
                                      p.CanRead AndAlso p.CanWrite AndAlso
                                      p.SetMethod IsNot Nothing AndAlso p.SetMethod.IsPublic AndAlso
                                      p.GetIndexParameters().Length = 0).
                    ToArray()
            End Function)
    End Function

    Private Function 转换为浅色(original As Color, propertyName As String) As Color
        If original.IsEmpty OrElse original.A = 0 Then Return original

        Dim maxChannel = Math.Max(original.R, Math.Max(original.G, original.B))
        Dim minChannel = Math.Min(original.R, Math.Min(original.G, original.B))
        If maxChannel - minChannel > 12 Then Return original

        Dim gray = CInt((CInt(original.R) + CInt(original.G) + CInt(original.B)) / 3)
        Dim mapped As Integer

        If original.A < 255 Then
            mapped = 255 - gray
        ElseIf propertyName.IndexOf("BackColor", StringComparison.OrdinalIgnoreCase) >= 0 AndAlso gray < 112 Then
            mapped = Math.Clamp(255 - gray \ 4, 0, 255)
        ElseIf propertyName.IndexOf("ForeColor", StringComparison.OrdinalIgnoreCase) >= 0 AndAlso gray > 128 Then
            mapped = 255 - gray
        ElseIf gray <= 80 OrElse gray >= 160 Then
            mapped = 255 - gray
        Else
            Return original
        End If

        Return Color.FromArgb(original.A, mapped, mapped, mapped)
    End Function

    Private Function 转换Html为浅色(text As String) As String
        If String.IsNullOrEmpty(text) Then Return text
        Return Html颜色表达式.Replace(
            text,
            Function(match)
                Try
                    Dim original = ColorTranslator.FromHtml(match.Groups(2).Value)
                    Dim mapped = 转换为浅色(original, "ForeColor")
                    If mapped.ToArgb() = original.ToArgb() Then Return match.Value
                    Return match.Groups(1).Value & $"#{mapped.R:X2}{mapped.G:X2}{mapped.B:X2}"
                Catch
                    Return match.Value
                End Try
            End Function)
    End Function

    Private Sub 应用LakeUI对话框主题(浅色 As Boolean)
        If 浅色 Then
            ExMsgBoxTheme.Current = ExMsgBoxTheme.CreateLight()
            ExInputBoxTheme.Current = ExInputBoxTheme.CreateLight()
            ExFloatingTipTheme.Current = ExFloatingTipTheme.CreateLight()
            ExFloatingBoxTheme.Current = ExFloatingBoxTheme.CreateLight()
            ExOverlayMsgBoxTheme.Current = ExOverlayMsgBoxTheme.CreateLight()
        Else
            ExMsgBoxTheme.Current = ExMsgBoxTheme.CreateDark()
            ExInputBoxTheme.Current = ExInputBoxTheme.CreateDark()
            ExFloatingTipTheme.Current = ExFloatingTipTheme.CreateDark()
            ExFloatingBoxTheme.Current = ExFloatingBoxTheme.CreateDark()
            ExOverlayMsgBoxTheme.Current = ExOverlayMsgBoxTheme.CreateDark()
        End If
    End Sub
End Module