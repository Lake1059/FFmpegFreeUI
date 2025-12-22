
Imports System.Runtime.InteropServices
Imports System.Text

Public Class 界面_播放器
    <DllImport("user32.dll")>
    Private Shared Function EnumWindows(ByVal lpEnumFunc As EnumWindowsProc, ByVal lParam As IntPtr) As Boolean
    End Function

    <DllImport("user32.dll")>
    Private Shared Function GetWindowThreadProcessId(ByVal hWnd As IntPtr, ByRef lpdwProcessId As Integer) As Integer
    End Function

    <DllImport("user32.dll")>
    Private Shared Function IsWindowVisible(ByVal hWnd As IntPtr) As Boolean
    End Function

    <DllImport("user32.dll", CharSet:=CharSet.Auto)>
    Private Shared Function GetWindowText(ByVal hWnd As IntPtr, ByVal lpString As StringBuilder, ByVal nMaxCount As Integer) As Integer
    End Function

    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function SetParent(ByVal hWndChild As IntPtr, ByVal hWndNewParent As IntPtr) As IntPtr
    End Function

    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function SetWindowPos(ByVal hWnd As IntPtr, ByVal hWndInsertAfter As IntPtr,
                                         ByVal X As Integer, ByVal Y As Integer,
                                         ByVal cx As Integer, ByVal cy As Integer,
                                         ByVal uFlags As UInteger) As Boolean
    End Function

    <DllImport("user32.dll")>
    Private Shared Function SetWindowLong(ByVal hWnd As IntPtr, ByVal nIndex As Integer, ByVal dwNewLong As Integer) As Integer
    End Function

    <DllImport("user32.dll")>
    Private Shared Function GetWindowLong(ByVal hWnd As IntPtr, ByVal nIndex As Integer) As Integer
    End Function

    Private Delegate Function EnumWindowsProc(ByVal hWnd As IntPtr, ByVal lParam As IntPtr) As Boolean
    Private Const SWP_NOZORDER As UInteger = &H4
    Private Const SWP_NOACTIVATE As UInteger = &H10
    Private Const SWP_FRAMECHANGED As UInteger = &H20
    Private Const GWL_STYLE As Integer = -16
    Private Const WS_CAPTION As Integer = &HC00000
    Private Const WS_THICKFRAME As Integer = &H40000

    Public ffplayHandle As IntPtr = IntPtr.Zero

    Private Sub 界面_播放器_Load(sender As Object, e As EventArgs) Handles Me.Load
        AddHandler UiButton打开文件.Click, AddressOf 打开
        AddHandler UiButton关闭进程.Click, AddressOf 停止
        AddHandler Form1.ResizeEnd, AddressOf 视频容器尺寸变化事件
    End Sub

    Public ffplayProcess As Process
    Public 是否已经取消播放 As Boolean = False

    Async Sub 播放(文件路径 As String)
        停止()
        是否已经取消播放 = False

        ffplayProcess = New Process
        ffplayProcess.StartInfo.FileName = "ffplay"
        ffplayProcess.StartInfo.WorkingDirectory = If(用户设置.实例对象.工作目录 <> "", 用户设置.实例对象.工作目录, "")
        ffplayProcess.StartInfo.Arguments = $"-x {Panel3.Width} -y {Panel3.Height} -noborder ""{文件路径}"""
        ffplayProcess.StartInfo.UseShellExecute = False
        ffplayProcess.StartInfo.CreateNoWindow = False
        ffplayProcess.Start()

        While True
            Dim Handle As IntPtr
            Await Task.Run(Sub() Handle = FindWindowByTitle(ffplayProcess.Id, 文件路径))
            If Handle <> IntPtr.Zero Then
                ffplayHandle = Handle
                Exit While
            End If
            If 是否已经取消播放 Then Exit Sub
            If ffplayProcess Is Nothing Then Exit Sub
            Await Task.Delay(200)
        End While

        SetParent(ffplayHandle, Panel3.Handle)
        SetWindowPos(ffplayHandle, IntPtr.Zero, 0, 0, Panel3.Width, Panel3.Height, SWP_NOZORDER Or SWP_NOACTIVATE)
        Form1.Focus()
    End Sub

    Sub 视频容器尺寸变化事件()
        If Form1.WindowState = FormWindowState.Minimized Then Exit Sub
        If Form1.UiTabControlMenu1.SelectedTab IsNot Me Then Exit Sub
        If ffplayProcess IsNot Nothing Then
            SetWindowPos(ffplayHandle, IntPtr.Zero, 0, 0, Panel3.Width, Panel3.Height, SWP_NOZORDER Or SWP_NOACTIVATE)
        End If
    End Sub

    Sub 打开()
        Dim a As New OpenFileDialog With {.Multiselect = False}
        If a.ShowDialog() = DialogResult.OK Then 播放(a.FileName)
    End Sub

    Sub 停止()
        If ffplayProcess IsNot Nothing Then
            ffplayProcess?.Kill()
            ffplayProcess?.Dispose()
            ffplayProcess = Nothing
        End If
        ffplayHandle = IntPtr.Zero
        是否已经取消播放 = True
    End Sub

    Public Shared Function GetProcessWindows(ByVal processId As Integer) As List(Of IntPtr)
        Dim windows As New List(Of IntPtr)
        EnumWindows(Function(hWnd As IntPtr, lParam As IntPtr) As Boolean
                        Dim windowProcessId As Integer = 0
                        GetWindowThreadProcessId(hWnd, windowProcessId)
                        If windowProcessId = processId AndAlso IsWindowVisible(hWnd) Then windows.Add(hWnd)
                        Return True
                    End Function, IntPtr.Zero)
        Return windows
    End Function
    Public Shared Function FindWindowByTitle(ByVal processId As Integer, ByVal expectedTitle As String) As IntPtr
        Dim windows = GetProcessWindows(processId)
        For Each hWnd In windows
            Dim titleBuilder As New StringBuilder(256)
            GetWindowText(hWnd, titleBuilder, titleBuilder.Capacity)
            Dim title As String = titleBuilder.ToString()
            If title = expectedTitle OrElse title = IO.Path.GetFileName(expectedTitle) Then
                Return hWnd
            End If
        Next
        Return IntPtr.Zero
    End Function

    Private Overloads Sub Dispose()
        停止()
    End Sub

    Private Sub Panel3_DragEnter(sender As Object, e As DragEventArgs) Handles Panel3.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub Panel3_DragDrop(sender As Object, e As DragEventArgs) Handles Panel3.DragDrop
        Dim files = e.Data.GetData(DataFormats.FileDrop)
        If files.Length > 0 Then 播放(files(0))
    End Sub
End Class
