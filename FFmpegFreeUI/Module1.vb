Imports System.IO
Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices
Imports System.Text.Json
Imports System.Threading
Module Module1

    Public Sound_Finish As Stream = My.Resources.Resource1.完成
    Public Sound_Error As Stream = My.Resources.Resource1.错误

    <DllImport("user32.dll")>
    Public Function ReleaseCapture() As Boolean
    End Function
    <DllImport("user32.dll")>
    Public Function SendMessage(hWnd As IntPtr, wMsg As Integer, wParam As Integer, lParam As Integer) As Integer
    End Function
    Private Const WM_NCLBUTTONDOWN As Integer = &HA1
    Private Const HTCAPTION As Integer = 2
    Sub 绑定拖动控件移动窗体(s As Control)
        AddHandler s.MouseDown, Sub(s1 As Object, e1 As MouseEventArgs)
                                    If e1.Button = MouseButtons.Left Then
                                        ReleaseCapture()
                                        Dim unused = SendMessage(s1.FindForm().Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0)
                                    End If
                                End Sub
    End Sub

    Public Property UI同步上下文 As SynchronizationContext = SynchronizationContext.Current

    Public Sub 界面线程执行(d As SendOrPostCallback)
        If UI同步上下文 IsNot Nothing Then
            UI同步上下文.Post(d, Nothing)
        Else
            Form1.重新创建句柄()
            Form1.Invoke(Sub() UI同步上下文 = SynchronizationContext.Current, Nothing)
            UI同步上下文.Post(d, Nothing)
        End If
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
        Select Case 用户设置.实例对象.有任务时系统保持状态选项
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

    Public Function 根据标签宽度计算显示高度(标签控件 As Label) As Integer
        Dim g As Graphics = Form1.CreateGraphics()
        Dim size As SizeF = g.MeasureString(标签控件.Text, 标签控件.Font, 标签控件.Width - 标签控件.Padding.Left - 标签控件.Padding.Right)
        g.Dispose()
        Return size.Height + 标签控件.Padding.Top + 标签控件.Padding.Bottom
    End Function

    Public Function LoadImageFromFile(File As String) As Image
        Using fs As New FileStream(File, FileMode.Open, FileAccess.Read)
            Return Image.FromStream(fs)
            fs.Dispose()
        End Using
    End Function

    Public Sub 显示窗体(哪个窗口 As Form, 以谁为基准显示 As Form)
        If 哪个窗口.Visible = True Then
            哪个窗口.Focus()
            哪个窗口.Left = (以谁为基准显示.Width - 哪个窗口.Width) * 0.5 + 以谁为基准显示.Left
            哪个窗口.Top = (以谁为基准显示.Height - 哪个窗口.Height) * 0.5 + 以谁为基准显示.Top
        Else
            哪个窗口.Left = (以谁为基准显示.Width - 哪个窗口.Width) * 0.5 + 以谁为基准显示.Left
            哪个窗口.Top = (以谁为基准显示.Height - 哪个窗口.Height) * 0.5 + 以谁为基准显示.Top
            哪个窗口.Show(以谁为基准显示)
        End If
    End Sub

    Public Sub 根据标签宽度计算并设置显示高度(标签控件 As Label)
        Dim g As Graphics = 标签控件.CreateGraphics()
        Dim size As SizeF = g.MeasureString(标签控件.Text, 标签控件.Font, 标签控件.Width - 标签控件.Padding.Left - 标签控件.Padding.Right)
        g.Dispose()
        标签控件.Height = size.Height + 标签控件.Padding.Top + 标签控件.Padding.Bottom
    End Sub

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
            If ExcludeContorl IsNot Nothing Then
                If ExcludeContorl.Contains(ctrl) Then Continue For
            End If
            Dim controlType As Type = c.GetType()
            Dim propInfo As PropertyInfo = controlType.GetProperty("Font")
            If propInfo IsNot Nothing Then ctrl.Font = New Font(FontName, ctrl.Font.Size, ctrl.Font.Style)
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
            Dim bounds As Rectangle = Form1.ClientRectangle
            Dim bitmap As New Bitmap(bounds.Width, bounds.Height)
            Using g As Graphics = Graphics.FromImage(bitmap)
                g.CopyFromScreen(Form1.PointToScreen(bounds.Location), Point.Empty, bounds.Size)
                g.CompositingMode = Drawing2D.CompositingMode.SourceOver
                Using brush As New SolidBrush(Color.FromArgb(180, 0, 0, 0))
                    g.FillRectangle(brush, 0, 0, bitmap.Width, bitmap.Height)
                End Using
            End Using
            Return bitmap
        Catch ex As Exception
            Return New Bitmap(Form1.Width, Form1.Height)
        End Try
    End Function
End Module
