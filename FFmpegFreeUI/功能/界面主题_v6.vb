Imports System.Collections.Concurrent
Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices
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
    Private ReadOnly LakeUI自带弹窗类型 As New HashSet(Of String)(StringComparer.Ordinal) From {
        "ExMsgBoxForm", "ExInputBoxForm", "ExFloatingBoxForm", "ExFloatingTipForm",
        "ExOverlayBackdropForm", "ExOverlayMsgBoxHostForm", "ExOverlayMsgBoxForm"
    }
    ' 浅色层级直接对应旧深色主题的 48/36/24 三层结构：一级导航、二/三级导航、内容区。
    ' 数值保持克制，避免浅色模式回到接近纯白的一整片，同时所有多级页面共享同一语义层级。
    Private ReadOnly 浅色一级导航背景 As Color = Color.FromArgb(222, 222, 222)
    Private ReadOnly 浅色多级导航背景 As Color = Color.FromArgb(230, 230, 230)
    Private ReadOnly 浅色基础背景 As Color = Color.FromArgb(239, 239, 239)
    Private ReadOnly 浅色表面背景 As Color = Color.White

    <DllImport("dwmapi.dll")>
    Private Function DwmGetColorizationColor(ByRef colorization As UInteger,
                                              <MarshalAs(UnmanagedType.Bool)> ByRef opaqueBlend As Boolean) As Integer
    End Function

    Private _已初始化 As Boolean
    Private _当前浅色 As Boolean
    Private _Windows主题色 As Color = SystemColors.Highlight

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
        Dim accent = 读取Windows主题色()
        If Not 强制刷新 AndAlso 浅色 = _当前浅色 AndAlso accent.ToArgb() = _Windows主题色.ToArgb() Then Return

        _当前浅色 = 浅色
        _Windows主题色 = accent
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

        ' LakeUI 自带的 ExMsgBox / ExInputBox / ExFloating* / ExOverlayMsgBox 都从该全局值读取，
        ' 确保后续新建弹窗与主窗口使用同一圆角策略。
        DwmWindowStyle.PopupCornerMode = mode

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

    Private Function 读取Windows主题色() As Color
        Try
            Dim argb As UInteger = 0UI
            Dim opaque As Boolean = False
            If DwmGetColorizationColor(argb, opaque) = 0 Then
                Dim r = CInt((argb >> 16) And &HFFUI)
                Dim g = CInt((argb >> 8) And &HFFUI)
                Dim b = CInt(argb And &HFFUI)
                Return Color.FromArgb(255, r, g, b)
            End If
        Catch
        End Try
        Return SystemColors.Highlight
    End Function

    Private Function 混合不透明颜色(baseColor As Color, accentColor As Color, accentRatio As Double) As Color
        accentRatio = Math.Clamp(accentRatio, 0.0R, 1.0R)
        Dim baseRatio = 1.0R - accentRatio
        Return Color.FromArgb(
            255,
            CInt(Math.Round(baseColor.R * baseRatio + accentColor.R * accentRatio)),
            CInt(Math.Round(baseColor.G * baseRatio + accentColor.G * accentRatio)),
            CInt(Math.Round(baseColor.B * baseRatio + accentColor.B * accentRatio)))
    End Function

    Private Function 获取浅色导航背景(target As Object) As Color
        Dim stripColor As Color = Color.Empty
        If TypeOf target Is ModernTabListControl Then
            stripColor = DirectCast(target, ModernTabListControl).TabStripBackColor
        ElseIf TypeOf target Is ModernTabControl Then
            stripColor = DirectCast(target, ModernTabControl).TabStripBackColor
        End If

        If stripColor.ToArgb() = 浅色一级导航背景.ToArgb() Then Return 浅色一级导航背景
        If stripColor.ToArgb() = 浅色多级导航背景.ToArgb() Then Return 浅色多级导航背景
        If stripColor.IsEmpty OrElse stripColor.A = 0 Then Return 浅色多级导航背景

        Dim maxChannel = Math.Max(stripColor.R, Math.Max(stripColor.G, stripColor.B))
        Dim minChannel = Math.Min(stripColor.R, Math.Min(stripColor.G, stripColor.B))
        If maxChannel - minChannel > 12 Then Return 浅色多级导航背景

        Dim gray = CInt((CInt(stripColor.R) + CInt(stripColor.G) + CInt(stripColor.B)) / 3)
        ' LakeUI 旧深色层级中根导航默认为 48，二/三级导航显式使用 36。
        Return If(gray >= 44 AndAlso gray <= 56, 浅色一级导航背景, 浅色多级导航背景)
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

    Private Function 属于LakeUI自带弹窗(control As Control) As Boolean
        Dim form = TryCast(control, Form)
        If form Is Nothing Then form = control.FindForm()
        Return form IsNot Nothing AndAlso
               form.GetType().Assembly Is GetType(DwmWindowStyle).Assembly AndAlso
               LakeUI自带弹窗类型.Contains(form.GetType().Name)
    End Function

    Private Sub 应用控件树(control As Control, 强制刷新 As Boolean)
        If control Is Nothing OrElse control.IsDisposed Then Return

        ' LakeUI 自带消息窗在创建时已经消费 Ex*Theme.Current；再次套用“深色→浅色”转换会
        ' 把已经是白色的弹窗反转回黑色，并把黑色遮罩反转成白色。此类顶层窗体完全交给 LakeUI 主题处理。
        If 属于LakeUI自带弹窗(control) Then
            If TypeOf control Is Form Then 应用窗体Dwm外观(DirectCast(control, Form))
            Return
        End If

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
            If form.FormBorderStyle = FormBorderStyle.None Then
                DwmWindowStyle.SuppressSystemBorder(form.Handle)
            End If
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
                pair.Key.SetValue(target, If(浅色, 转换为浅色(pair.Value, pair.Key.Name, target), pair.Value))
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

    Private Function 转换为浅色(original As Color, propertyName As String, Optional target As Object = Nothing) As Color
        If original.IsEmpty OrElse original.A = 0 Then Return original

        Dim name = If(propertyName, String.Empty)

        ' 所有 TabList / TabControl 共用同一套层级：根导航 #DEDEDE，二/三级导航 #E6E6E6，内容区 #EFEFEF。
        ' 不依赖具体页面名称，因此参数面板、设置、集成工具以及后续新增的多级菜单都会保持一致。
        If TypeOf target Is ModernTabListControl OrElse TypeOf target Is ModernTabControl Then
            Dim navBack = 获取浅色导航背景(target)
            Select Case name
                Case "ContentBackColor"
                    Return 浅色基础背景
                Case "TabStripBackColor"
                    Return navBack
                Case "TabItemSelectedBackColor"
                    Return 混合不透明颜色(navBack, _Windows主题色, 0.18R)
                Case "TabItemHoverBackColor"
                    Return 混合不透明颜色(navBack, _Windows主题色, 0.09R)
                Case "IndicatorColor"
                    Return _Windows主题色
            End Select
        End If
        ' 红色关闭按钮悬停/按下状态需要保留亮色图标，不能按普通前景色反转。
        If name.IndexOf("HoverGlyphColor", StringComparison.OrdinalIgnoreCase) >= 0 OrElse
           name.IndexOf("PressedGlyphColor", StringComparison.OrdinalIgnoreCase) >= 0 Then Return original

        Dim maxChannel = Math.Max(original.R, Math.Max(original.G, original.B))
        Dim minChannel = Math.Min(original.R, Math.Min(original.G, original.B))
        Dim neutral = maxChannel - minChannel <= 12
        Dim gray = CInt((CInt(original.R) + CInt(original.G) + CInt(original.B)) / 3)

        Dim isForeground = name.IndexOf("ForeColor", StringComparison.OrdinalIgnoreCase) >= 0 OrElse
                           name.IndexOf("TextColor", StringComparison.OrdinalIgnoreCase) >= 0
        If isForeground Then
            Dim alpha = If(original.A < 255, Math.Max(CInt(original.A), 210), 255)
            Dim candidate As Color
            If neutral Then
                Dim targetGray As Integer
                If gray >= 160 Then
                    targetGray = 55
                ElseIf gray >= 90 Then
                    targetGray = 72
                Else
                    targetGray = Math.Min(gray, 72)
                End If
                candidate = Color.FromArgb(alpha, targetGray, targetGray, targetGray)
            Else
                candidate = Color.FromArgb(alpha, original.R, original.G, original.B)
            End If
            Return 确保浅色前景对比度(candidate, 4.5R)
        End If

        Dim isBorder = name.IndexOf("Border", StringComparison.OrdinalIgnoreCase) >= 0 OrElse
                       name.IndexOf("Separator", StringComparison.OrdinalIgnoreCase) >= 0 OrElse
                       name.IndexOf("LineColor", StringComparison.OrdinalIgnoreCase) >= 0
        If isBorder AndAlso neutral Then
            If original.A < 255 Then
                Return Color.FromArgb(Math.Max(CInt(original.A), 96), 48, 48, 48)
            End If
            Return Color.FromArgb(original.A, 112, 112, 112)
        End If

        If Not neutral Then Return original

        Dim isBackground = name.IndexOf("BackColor", StringComparison.OrdinalIgnoreCase) >= 0
        If isBackground AndAlso original.A = 255 Then
            ' 保留旧深色设计器中的语义灰阶：24=内容底，36=二/三级导航，48=一级导航。
            ' 这样任何新页面只要继续沿用原来的深色层级，就会自动得到一致的浅色层级。
            If gray <= 12 Then Return 浅色表面背景
            If gray >= 20 AndAlso gray <= 28 Then Return 浅色基础背景
            If gray >= 32 AndAlso gray <= 40 Then Return 浅色多级导航背景
            If gray >= 44 AndAlso gray <= 50 Then Return 浅色一级导航背景
            If gray <= 72 Then Return 浅色基础背景
            If gray < 112 Then Return Color.FromArgb(234, 234, 234)
            If gray >= 160 Then Return Color.FromArgb(original.A, gray, gray, gray)
        End If

        Dim mapped As Integer
        If original.A < 255 Then
            mapped = 255 - gray
        ElseIf gray <= 80 OrElse gray >= 160 Then
            mapped = 255 - gray
        Else
            Return original
        End If

        Return Color.FromArgb(original.A, mapped, mapped, mapped)
    End Function

    Private Function 确保浅色前景对比度(color As Color, minimumRatio As Double) As Color
        Dim background = 浅色基础背景
        Dim alpha = CInt(color.A)
        Dim r = CInt(color.R)
        Dim g = CInt(color.G)
        Dim b = CInt(color.B)

        For i = 0 To 24
            Dim candidate = Color.FromArgb(alpha, r, g, b)
            If 计算对比度(candidate, background) >= minimumRatio Then Return candidate
            If i = 8 Then alpha = 255
            r = Math.Max(0, CInt(Math.Floor(r * 0.88R)))
            g = Math.Max(0, CInt(Math.Floor(g * 0.88R)))
            b = Math.Max(0, CInt(Math.Floor(b * 0.88R)))
        Next

        Return Color.FromArgb(255, 45, 45, 45)
    End Function

    Private Function 计算对比度(foreground As Color, background As Color) As Double
        Dim effective = 合成颜色(foreground, background)
        Dim l1 = 相对亮度(effective)
        Dim l2 = 相对亮度(background)
        Return (Math.Max(l1, l2) + 0.05R) / (Math.Min(l1, l2) + 0.05R)
    End Function

    Private Function 合成颜色(foreground As Color, background As Color) As Color
        If foreground.A >= 255 Then Return foreground
        Dim a = foreground.A / 255.0R
        Return Color.FromArgb(
            255,
            CInt(Math.Round(foreground.R * a + background.R * (1.0R - a))),
            CInt(Math.Round(foreground.G * a + background.G * (1.0R - a))),
            CInt(Math.Round(foreground.B * a + background.B * (1.0R - a))))
    End Function

    Private Function 相对亮度(color As Color) As Double
        Dim r = 线性化颜色通道(color.R / 255.0R)
        Dim g = 线性化颜色通道(color.G / 255.0R)
        Dim b = 线性化颜色通道(color.B / 255.0R)
        Return 0.2126R * r + 0.7152R * g + 0.0722R * b
    End Function

    Private Function 线性化颜色通道(value As Double) As Double
        If value <= 0.04045R Then Return value / 12.92R
        Return Math.Pow((value + 0.055R) / 1.055R, 2.4R)
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
            ' 浅色模式的消息弹窗使用纯白实体表面；避免深色毛玻璃 tint 把白色卡片染灰。
            MessageDialogOptions.BackdropEnabled = False
        Else
            ExMsgBoxTheme.Current = ExMsgBoxTheme.CreateDark()
            ExInputBoxTheme.Current = ExInputBoxTheme.CreateDark()
            ExFloatingTipTheme.Current = ExFloatingTipTheme.CreateDark()
            ExFloatingBoxTheme.Current = ExFloatingBoxTheme.CreateDark()
            ExOverlayMsgBoxTheme.Current = ExOverlayMsgBoxTheme.CreateDark()
            MessageDialogOptions.BackdropEnabled = True
        End If
    End Sub
End Module
