Imports System.Globalization
Imports System.IO
Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Text.Json
Imports System.Threading

Module Module1

    Public Sound_Finish As Stream = My.Resources.Resource1.完成
    Public Sound_Error As Stream = My.Resources.Resource1.错误
    Public SP_UnLock As Boolean = False
    Public UpdateAvailable As Boolean = False

    <DllImport("user32.dll")>
    Public Function ReleaseCapture() As Boolean
    End Function
    <DllImport("user32.dll")>
    Public Function SendMessage(hWnd As IntPtr, wMsg As Integer, wParam As Integer, lParam As Integer) As Integer
    End Function
    Private Const WM_NCLBUTTONDOWN As Integer = &HA1
    Private Const HTCAPTION As Integer = 2
    Sub 绑定拖动控件移动窗体(s As Control)
        AddHandler s.MouseDown, Sub(s1, e1)
                                    Select Case e1.Button
                                        Case MouseButtons.Left
                                            ReleaseCapture()
                                            Dim unused = SendMessage(s1.FindForm().Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0)
                                        Case MouseButtons.Right
                                            Dim form = s1.FindForm()
                                            If form Is Nothing OrElse form.FormBorderStyle <> FormBorderStyle.None Then Exit Sub
                                            Select Case form.WindowState
                                                Case FormWindowState.Maximized
                                                    form.WindowState = FormWindowState.Normal
                                                Case FormWindowState.Normal
                                                    form.WindowState = FormWindowState.Maximized
                                            End Select
                                    End Select
                                End Sub
    End Sub

    Public Property UI同步上下文 As SynchronizationContext = SynchronizationContext.Current

    Public Sub 界面线程执行(d As SendOrPostCallback)
        If UI同步上下文 IsNot Nothing Then
            UI同步上下文.Post(d, Nothing)
        Else
            If FormMain_v6 IsNot Nothing AndAlso FormMain_v6.IsHandleCreated AndAlso Not FormMain_v6.IsDisposed Then
                FormMain_v6.BeginInvoke(Sub() d(Nothing))
            Else
                d(Nothing)
            End If
        End If
    End Sub

    Public Enum 路径下拉框拖拽模式
        文件路径
        文件夹路径
    End Enum

    Private Class 路径下拉框拖拽配置
        Public Property 模式 As 路径下拉框拖拽模式
        Public Property 拖拽完成 As Action(Of LakeUI.ModernComboBox, String)
    End Class

    Private ReadOnly 路径下拉框拖拽配置表 As New Dictionary(Of LakeUI.ModernComboBox, 路径下拉框拖拽配置)

    Public Sub 绑定路径下拉框拖拽(下拉框 As LakeUI.ModernComboBox,
                            Optional 模式 As 路径下拉框拖拽模式 = 路径下拉框拖拽模式.文件路径,
                            Optional 拖拽完成 As Action(Of LakeUI.ModernComboBox, String) = Nothing)
        If 下拉框 Is Nothing Then Exit Sub
        下拉框.AllowDrop = True
        路径下拉框拖拽配置表(下拉框) = New 路径下拉框拖拽配置 With {.模式 = 模式, .拖拽完成 = 拖拽完成}

        RemoveHandler 下拉框.DragEnter, AddressOf 路径下拉框_DragEnter
        RemoveHandler 下拉框.DragDrop, AddressOf 路径下拉框_DragDrop
        RemoveHandler 下拉框.Disposed, AddressOf 路径下拉框_Disposed
        AddHandler 下拉框.DragEnter, AddressOf 路径下拉框_DragEnter
        AddHandler 下拉框.DragDrop, AddressOf 路径下拉框_DragDrop
        AddHandler 下拉框.Disposed, AddressOf 路径下拉框_Disposed
    End Sub

    Public Function 规范化文件夹路径(路径 As String) As String
        Dim result = If(路径, "").Trim()
        If result = "" Then Return ""
        Try
            Dim root = Path.GetPathRoot(result)
            If root <> "" AndAlso String.Equals(result, root, StringComparison.OrdinalIgnoreCase) AndAlso
               root.Length = 3 AndAlso root(1) = ":"c AndAlso (root(2) = "\"c OrElse root(2) = "/"c) Then
                Return root
            End If
        Catch
        End Try
        Return result.TrimEnd("\"c, "/"c)
    End Function

    Private Sub 路径下拉框_DragEnter(sender As Object, e As DragEventArgs)
        e.Effect = If(e.Data IsNot Nothing AndAlso e.Data.GetDataPresent(DataFormats.FileDrop), DragDropEffects.Copy, DragDropEffects.None)
    End Sub

    Private Sub 路径下拉框_DragDrop(sender As Object, e As DragEventArgs)
        Dim combo = TryCast(sender, LakeUI.ModernComboBox)
        If combo Is Nothing OrElse e.Data Is Nothing Then Exit Sub

        Dim files = TryCast(e.Data.GetData(DataFormats.FileDrop), String())
        If files Is Nothing OrElse files.Length = 0 Then Exit Sub

        Dim 配置 As 路径下拉框拖拽配置 = Nothing
        If Not 路径下拉框拖拽配置表.TryGetValue(combo, 配置) Then
            配置 = New 路径下拉框拖拽配置 With {.模式 = 路径下拉框拖拽模式.文件路径}
        End If

        Dim 路径 = 从拖拽数据获取路径(files(0), 配置.模式)
        If 路径 = "" Then Exit Sub
        combo.Text = 路径
        配置.拖拽完成?.Invoke(combo, 路径)
    End Sub

    Private Function 从拖拽数据获取路径(原始路径 As String, 模式 As 路径下拉框拖拽模式) As String
        Dim p = If(原始路径, "").Trim()
        If p = "" Then Return ""

        Select Case 模式
            Case 路径下拉框拖拽模式.文件夹路径
                If Directory.Exists(p) Then Return 规范化文件夹路径(p)
                If File.Exists(p) Then Return 规范化文件夹路径(Path.GetDirectoryName(p))
            Case Else
                If File.Exists(p) Then Return p
        End Select

        Return ""
    End Function

    Private Sub 路径下拉框_Disposed(sender As Object, e As EventArgs)
        Dim combo = TryCast(sender, LakeUI.ModernComboBox)
        If combo IsNot Nothing Then 路径下拉框拖拽配置表.Remove(combo)
    End Sub

    Public 同时运行任务上限 As Integer = 1

    <Extension>
    Public Sub DoubleBuffer(control As Control)
        Dim propertyInfo As PropertyInfo = control.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance Or BindingFlags.NonPublic)
        propertyInfo?.SetValue(control, True, Nothing)
    End Sub
    <Extension>
    Public Function IsEqual(page1 As TabPage, page2 As TabPage) As Boolean
        If page1 Is page2 Then Return True
        If page1 Is Nothing OrElse page2 Is Nothing Then Return False
        Return page1.Name = page2.Name
    End Function


    Public JsonSO As New JsonSerializerOptions With {
        .WriteIndented = True,
        .PropertyNamingPolicy = Nothing,
        .DictionaryKeyPolicy = Nothing,
        .Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    }

    Public Sub 设置富文本框行高(RichTextBoxObject As Control, LineHeight As Integer)
        Dim fmt As New PARAFORMAT2()
        fmt.cbSize = Marshal.SizeOf(fmt)
        fmt.bLineSpacingRule = 4
        fmt.dyLineSpacing = LineHeight
        fmt.dwMask = PFM_LINESPACING
        SendMessage(New HandleRef(RichTextBoxObject, RichTextBoxObject.Handle), EM_SETPARAFORMAT, 4, fmt)
    End Sub

    Public Const WM_USER As Integer = &H400
    Public Const EM_GETPARAFORMAT As Integer = WM_USER + 61
    Public Const EM_SETPARAFORMAT As Integer = WM_USER + 71
    Public Const PFM_LINESPACING As UInteger = &H100

    <StructLayout(LayoutKind.Sequential)>
    Public Structure PARAFORMAT2
        Public cbSize As Integer
        Public dwMask As UInteger
        Public wNumbering As Short
        Public wReserved As Short
        Public dxStartIndent As Integer
        Public dxRightIndent As Integer
        Public dxOffset As Integer
        Public wAlignment As Short
        Public cTabCount As Short
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=32)>
        Public rgxTabs() As Integer
        ' PARAFORMAT2 增量字段
        Public dySpaceBefore As Integer
        Public dySpaceAfter As Integer
        Public dyLineSpacing As Integer
        Public sStyle As Short
        Public bLineSpacingRule As Byte
        Public bOutlineLevel As Byte
        Public wShadingWeight As Short
        Public wShadingStyle As Short
        Public wNumberingStart As Short
        Public wNumberingStyle As Short
        Public wNumberingTab As Short
        Public wBorderSpace As Short
        Public wBorderWidth As Short
        Public wBorders As Short
    End Structure


    <DllImport("user32.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
    Public Function SendMessage(hWnd As HandleRef, msg As Integer, wParam As IntPtr, ByRef lParam As PARAFORMAT2) As IntPtr
    End Function

    <DllImport("ntdll.dll", SetLastError:=True)>
    Public Function NtSuspendProcess(processHandle As IntPtr) As Integer
    End Function
    <DllImport("ntdll.dll", SetLastError:=True)>
    Public Function NtResumeProcess(processHandle As IntPtr) As Integer
    End Function

    <DllImport("kernel32.dll", SetLastError:=True)>
    Public Function SetThreadExecutionState(esFlags As EXECUTION_STATE) As EXECUTION_STATE
    End Function
    <Flags>
    Public Enum EXECUTION_STATE As UInteger
        ES_SYSTEM_REQUIRED = &H1
        ES_DISPLAY_REQUIRED = &H2
        ES_CONTINUOUS = &H80000000UI
    End Enum

    Public Sub 设定系统状态()
        Select Case 设置_v6.实例对象.有任务时系统保持状态选项
            Case 0
                SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS Or EXECUTION_STATE.ES_SYSTEM_REQUIRED)
            Case 1
                SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS Or EXECUTION_STATE.ES_SYSTEM_REQUIRED Or EXECUTION_STATE.ES_DISPLAY_REQUIRED)
            Case 2
                SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS)
        End Select
    End Sub

    Public Sub 恢复系统状态()
        Dim unused = SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS)
    End Sub

    Public Sub 根据文本设置标签高度(标签控件 As Label)
        Using g As Graphics = 标签控件.CreateGraphics()
            Dim availableWidth As Integer = 标签控件.Width - 标签控件.Padding.Left - 标签控件.Padding.Right
            Dim size As SizeF = g.MeasureString(标签控件.Text, 标签控件.Font, availableWidth)
            Dim dpiScale = If(标签控件.FindForm() IsNot Nothing, 标签控件.FindForm().DeviceDpi / 96.0F, 1.0F)
            标签控件.Height = CInt(Math.Ceiling(size.Height)) + 标签控件.Padding.Top + 标签控件.Padding.Bottom + CInt(2 * dpiScale)
        End Using
    End Sub

    Public Sub 根据文本设置按钮宽度(控件 As Control, Optional 额外加成 As Integer = 0)
        Using g As Graphics = 控件.CreateGraphics()
            Dim size As SizeF = g.MeasureString(控件.Text, 控件.Font)
            控件.Width = CInt(Math.Ceiling(size.Width)) + 控件.Padding.Left + 控件.Padding.Right + 额外加成
        End Using
    End Sub

    Public Function LoadImageFromFile(File As String) As Image
        Dim ms As New MemoryStream(System.IO.File.ReadAllBytes(File))
        Return Image.FromStream(ms)
    End Function

    Public Sub 显示窗体(哪个窗口 As Form, 以谁为基准显示 As Form)
        If 哪个窗口.Visible = True Then
            哪个窗口.Focus()
            哪个窗口.Left = (以谁为基准显示.Width - 哪个窗口.Width) * 0.5 + 以谁为基准显示.Left
            哪个窗口.Top = (以谁为基准显示.Height - 哪个窗口.Height) * 0.5 + 以谁为基准显示.Top
        Else
            哪个窗口.Show(以谁为基准显示)
            哪个窗口.Left = (以谁为基准显示.Width - 哪个窗口.Width) * 0.5 + 以谁为基准显示.Left
            哪个窗口.Top = (以谁为基准显示.Height - 哪个窗口.Height) * 0.5 + 以谁为基准显示.Top
        End If
    End Sub

    'Public Sub 根据标签宽度计算并设置显示高度(标签控件 As Label)
    '    Dim g As Graphics = 标签控件.CreateGraphics()
    '    Dim size As SizeF = g.MeasureString(标签控件.Text, 标签控件.Font, 标签控件.Width - 标签控件.Padding.Left - 标签控件.Padding.Right)
    '    g.Dispose()
    '    标签控件.Height = size.Height + 标签控件.Padding.Top + 标签控件.Padding.Bottom
    'End Sub

    ''' <summary>
    ''' 根据核心编号列表生成 ProcessorAffinity 掩码
    ''' </summary>
    ''' <param name="cores">逻辑核心编号，从 0 开始</param>
    ''' <returns>IntPtr 类型的掩码</returns>
    Function GetAffinityMask(cores As Integer()) As IntPtr
        Dim mask As Long = 0
        For Each core In cores
            If core >= 0 AndAlso core < Environment.ProcessorCount Then
                mask = mask Or CLng(1) << core
            Else
                Throw New ArgumentOutOfRangeException($"核心编号 {core} 无效。系统共有 {Environment.ProcessorCount} 个核心（从 0 开始）。")
            End If
        Next
        Return New IntPtr(mask)
    End Function

    Public Sub SetControlFont(FontName As String, c As Control, Optional ExcludeContorl As Control() = Nothing, Optional Self As Boolean = False)
        If Self Then c.Font = New Font(FontName, c.Font.Size)
        For Each ctrl As Control In c.Controls
            If ExcludeContorl?.Contains(ctrl) Then Continue For
            Dim propInfo As PropertyInfo = ctrl.GetType().GetProperty("Font", BindingFlags.Public Or BindingFlags.NonPublic)
            If propInfo IsNot Nothing Then
                propInfo.SetValue(ctrl, New Font(FontName, ctrl.Font.Size, ctrl.Font.Style))
            Else
                Try
                    ctrl.Font = New Font(FontName, ctrl.Font.Size, ctrl.Font.Style)
                Catch ex As Exception
                End Try
            End If
            ctrl.GetType().GetProperty("AnimationFPS", BindingFlags.Public Or BindingFlags.NonPublic)?.SetValue(ctrl, 设置_v6.实例对象.图形动画帧率)
            ctrl.GetType().GetProperty("DropDownAnimationFPS", BindingFlags.Public Or BindingFlags.NonPublic)?.SetValue(ctrl, 设置_v6.实例对象.图形动画帧率)
            If ctrl.HasChildren Then SetControlFont(FontName, ctrl, ExcludeContorl)
        Next
    End Sub

    <DllImport("winmm.dll", CharSet:=CharSet.Auto)>
    Public Function PlaySound(pszSound As String, hmod As IntPtr, fdwSound As UInteger) As Boolean
    End Function

    Public Function 转译模式处理路径(p As String) As String
        Dim a = p
        Dim root As String = Path.GetPathRoot(a)
        If Not String.IsNullOrEmpty(root) Then
            a = a.Substring(root.Length)
        End If
        a = a.Replace("\", "/").Replace("//", "/")
        If Not a.StartsWith("/"c) Then a = "/" & a
        Return a
    End Function

    Public Function 截取画面_对话框背景专用() As Bitmap
        Try
            Dim bounds As Rectangle = FormMain_v6.ClientRectangle
            Dim bitmap As New Bitmap(bounds.Width, bounds.Height)
            Using g As Graphics = Graphics.FromImage(bitmap)
                g.CopyFromScreen(FormMain_v6.PointToScreen(bounds.Location), Point.Empty, bounds.Size)
                g.CompositingMode = Drawing2D.CompositingMode.SourceOver
                Using brush As New SolidBrush(Color.FromArgb(180, 0, 0, 0))
                    g.FillRectangle(brush, 0, 0, bitmap.Width, bitmap.Height)
                End Using
            End Using
            Return bitmap
        Catch ex As Exception
            Return New Bitmap(FormMain_v6.Width, FormMain_v6.Height)
        End Try
    End Function

    Public Function 随机字符串生成(长度 As Integer, Optional 包含数字 As Boolean = True, Optional 包含大写字母 As Boolean = True, Optional 包含小写字母 As Boolean = True) As String
        If 长度 <= 0 Then
            Return ""
            Exit Function
        End If
        If Not 包含数字 AndAlso Not 包含大写字母 AndAlso Not 包含小写字母 Then
            Return ""
            Exit Function
        End If
        Dim numbers As String = "0123456789"
        Dim upperCase As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        Dim lowerCase As String = "abcdefghijklmnopqrstuvwxyz"
        Dim validChars As New StringBuilder()
        If 包含数字 Then validChars.Append(numbers)
        If 包含大写字母 Then validChars.Append(upperCase)
        If 包含小写字母 Then validChars.Append(lowerCase)
        If validChars.Length = 0 Then
            Return "无效的字符集"
        End If
        Dim rnd As New Random()
        Dim result As New StringBuilder(长度)
        For i As Integer = 1 To 长度
            Dim randomIndex As Integer = rnd.Next(0, validChars.Length)
            result.Append(validChars(randomIndex))
        Next
        Return result.ToString()
    End Function

    Public Function 将时间字符串转换为时间类型(timeStr As String) As TimeSpan
        Try
            If String.IsNullOrWhiteSpace(timeStr) Then Return TimeSpan.Zero
            Dim numericValue As Double
            If Double.TryParse(timeStr.Trim(), numericValue) Then
                Return TimeSpan.FromSeconds(numericValue)
            End If
            Dim hours As Integer = 0
            Dim minutes As Integer = 0
            Dim seconds As Integer = 0
            Dim milliseconds As Integer = 0
            Dim timeParts = timeStr.Split(":"c)
            Select Case timeParts.Length
                Case 1 ' 只有秒数，可能包含小数点
                    Dim secValue As Double
                    If Double.TryParse(timeParts(0), secValue) Then
                        Return TimeSpan.FromSeconds(secValue)
                    End If
                Case 2 ' MM:SS 格式
                    If Integer.TryParse(timeParts(0), minutes) And
                   Double.TryParse(timeParts(1), numericValue) Then
                        seconds = CInt(Math.Floor(numericValue))
                        milliseconds = CInt((numericValue - seconds) * 1000)
                    End If
                Case 3 ' HH:MM:SS 格式
                    Integer.TryParse(timeParts(0), hours)
                    Integer.TryParse(timeParts(1), minutes)
                    Dim secPart = timeParts(2)
                    If secPart.Contains("."c) Then
                        Dim secMs = secPart.Split("."c)
                        Integer.TryParse(secMs(0), seconds)
                        Dim msStr = secMs(1).PadRight(3, "0"c)
                        If msStr.Length >= 3 Then
                            Integer.TryParse(msStr.AsSpan(0, 3), milliseconds)
                        End If
                    Else
                        Integer.TryParse(secPart, seconds)
                    End If
                Case Else
                    Return TimeSpan.Zero
            End Select
            Return New TimeSpan(0, hours, minutes, seconds, milliseconds)
        Catch
            Return TimeSpan.Zero
        End Try
    End Function

    Public Function 将时间类型转换为时间字符串(timespan As TimeSpan) As String
        If timespan < TimeSpan.Zero Then timespan = TimeSpan.Zero
        Return String.Format("{0:D2}:{1:D2}:{2:D2}.{3:D3}",
                            CInt(Math.Floor(timespan.TotalHours)),
                            timespan.Minutes,
                            timespan.Seconds,
                            timespan.Milliseconds)
    End Function

    <DllImport("psapi.dll")>
    Public Function EmptyWorkingSet(hProcess As IntPtr) As Boolean
    End Function
    Public Sub 回收自身内存占用()
        GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, True, True)
        GC.WaitForPendingFinalizers()
        EmptyWorkingSet(Process.GetCurrentProcess.Handle)
    End Sub

    Public Function 混淆字符_喵(input As String) As String
        Return New String("喵", input.Length)
    End Function

    <DllImport("dwmapi.dll")>
    Function DwmSetWindowAttribute(hwnd As IntPtr, attr As Integer, <MarshalAs(UnmanagedType.Bool)> ByRef attrValue As Boolean, attrSize As Integer) As Integer
    End Function
    Const DWMWA_USE_IMMERSIVE_DARK_MODE As Integer = 20
    Public Sub 启用Win32API深色模式(窗口句柄 As IntPtr)
        Try
            Dim unused = DwmSetWindowAttribute(窗口句柄, DWMWA_USE_IMMERSIVE_DARK_MODE, True, Marshal.SizeOf(Of Boolean)())
        Catch ex As Exception
        End Try
    End Sub

    Public Function 扫描单层文件(文件夹路径 As String, 扩展名 As String) As List(Of String)
        Dim a As New List(Of String)
        Dim mFileInfo As System.IO.FileInfo
        Dim mDirInfo As New System.IO.DirectoryInfo(文件夹路径)
        For Each mFileInfo In mDirInfo.GetFiles(扩展名)
            a.Add(mFileInfo.FullName)
        Next
        Return a
    End Function

    Public Sub 在RTF输出文本(RTF As System.Windows.Forms.RichTextBox, 文本 As String, 颜色 As Color)
        If String.IsNullOrEmpty(文本) Then Exit Sub
        Dim 文本长度 = Len(文本)
        If RTF.TextLength > 0 Then
            RTF.AppendText(vbCrLf & 文本)
        Else
            RTF.AppendText(文本)
        End If
        Dim 添加起始位 As Integer = RTF.TextLength - 文本长度
        RTF.Select(添加起始位, 文本长度)
        RTF.SelectionColor = 颜色
        RTF.Select(RTF.TextLength, 0)
    End Sub

    Public Function 将路径转换为FFmpeg滤镜接受的格式(path As String) As String
        If String.IsNullOrEmpty(path) Then
            Return path
        End If
        If path.StartsWith("\\") Then
            Dim pathAfterPrefix As String = path.Substring(2)
            pathAfterPrefix = pathAfterPrefix.Replace("\", "\\")
            Return "\\" & pathAfterPrefix
        End If
        If path.Length >= 2 AndAlso path(1) = ":"c Then
            Dim driveLetter As String = path.Substring(0, 1)
            Dim pathAfterDrive As String = path.Substring(2)
            pathAfterDrive = pathAfterDrive.Replace("\", "\\")
            Return driveLetter & "\:" & pathAfterDrive
        End If
        Return path.Replace("\", "\\")
    End Function

    Public Sub 在富文本框输出(RTF As RichTextBox, 文本 As String)
        Dim 文本长度 = Len(文本)
        RTF.AppendText(If(RTF.Text <> "", vbCrLf, "") & 文本)
        Dim 添加起始位 As Integer = RTF.TextLength - 文本长度
        RTF.Select(添加起始位, 文本长度)
        RTF.SelectionColor = 识别FF单行输出并调整文字颜色(文本, RTF.ForeColor)
        RTF.Select(RTF.TextLength, 0)
    End Sub

    Public Function 识别FF单行输出并调整文字颜色(单行输出 As String, 默认颜色 As Color) As Color
        If String.IsNullOrEmpty(单行输出) Then Return 默认颜色
        If 编码队列_v6.错误输出匹配字符串列表.Any(Function(keyword) 单行输出.Contains(keyword, StringComparison.OrdinalIgnoreCase)) Then
            Return Color.IndianRed
        End If
        Select Case True
            Case 单行输出.Contains("Input #", StringComparison.OrdinalIgnoreCase)
                Return Color.LightGreen
            Case 单行输出.Contains("Duration:", StringComparison.OrdinalIgnoreCase)
                Return Color.CornflowerBlue
            Case 单行输出.Contains("Stream #", StringComparison.OrdinalIgnoreCase)
                Return Color.Plum
            Case 单行输出.Contains("Output #", StringComparison.OrdinalIgnoreCase)
                Return Color.Goldenrod
        End Select
        Return 默认颜色
    End Function

    Public Function 转换HTML颜色到ffmpeg接受的格式(HTML颜色 As String, Optional 透明度 As String = "") As String
        If String.IsNullOrWhiteSpace(HTML颜色) Then Return ""
        Dim parsedColor As Color
        Try
            parsedColor = ColorTranslator.FromHtml(HTML颜色.Trim())
        Catch
            Return ""
        End Try
        Dim a As Byte = parsedColor.A
        Dim r As Byte = parsedColor.R
        Dim g As Byte = parsedColor.G
        Dim b As Byte = parsedColor.B
        Dim Ap As String = 透明度.Trim()
        If Ap = "" Then Ap = "0"
        Dim alphaValue As Byte
        If Byte.TryParse(Ap, NumberStyles.Integer, CultureInfo.InvariantCulture, alphaValue) Then
            a = alphaValue
        ElseIf Byte.TryParse(Ap, NumberStyles.HexNumber, CultureInfo.InvariantCulture, alphaValue) Then
            a = alphaValue
        Else
            Return ""
        End If

        Return $"&H{a:X2}{b:X2}{g:X2}{r:X2}"
    End Function

End Module
