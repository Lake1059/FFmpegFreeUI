Imports System.Globalization
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Threading

Public Class Form_v6_调试播放器
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

    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function PostMessage(ByVal hWnd As IntPtr, ByVal msg As Integer, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As Boolean
    End Function

    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function GetClientRect(ByVal hWnd As IntPtr, ByRef lpRect As RECT) As Boolean
    End Function

    Private Delegate Function EnumWindowsProc(ByVal hWnd As IntPtr, ByVal lParam As IntPtr) As Boolean
    Private Structure RECT
        Public Left As Integer
        Public Top As Integer
        Public Right As Integer
        Public Bottom As Integer
    End Structure

    Private Const SWP_NOZORDER As UInteger = &H4
    Private Const SWP_NOACTIVATE As UInteger = &H10
    Private Const SWP_FRAMECHANGED As UInteger = &H20
    Private Const GWL_STYLE As Integer = -16
    Private Const WS_CAPTION As Integer = &HC00000
    Private Const WS_THICKFRAME As Integer = &H40000
    Private Const WS_CHILD As Integer = &H40000000
    Private Const WS_POPUP As Integer = -2147483648
    Private Const WM_MOUSEMOVE As Integer = &H200
    Private Const WM_RBUTTONDOWN As Integer = &H204
    Private Const WM_RBUTTONUP As Integer = &H205
    Private Const 进度界面刷新间隔毫秒 As Integer = 1000
    Private Shared ReadOnly MK_RBUTTON As New IntPtr(&H2)
    Private Shared ReadOnly DurationRegex As New Regex("Duration:\s*(?<time>\d+:\d{2}:\d{2}(?:\.\d+)?)", RegexOptions.Compiled Or RegexOptions.IgnoreCase)
    Private Shared ReadOnly FfplayProgressRegex As New Regex("^\s*(?<seconds>\d+(?:\.\d+)?)\s+(?:[AM]-V:|fd=)", RegexOptions.Compiled Or RegexOptions.IgnoreCase)

    Public ffplayHandle As IntPtr = IntPtr.Zero
    Private 输出读取取消源 As CancellationTokenSource
    Private ffplay输出读取任务 As Task
    Private ReadOnly 进度状态锁 As New Object()
    Private 当前总时长 As TimeSpan = TimeSpan.Zero
    Private 当前播放位置 As TimeSpan = TimeSpan.Zero
    Private 最新ffplay状态文本 As String = ""
    Private 上次进度界面刷新时间 As DateTime = DateTime.MinValue

    Private Sub Form_v6_调试播放器_Load(sender As Object, e As EventArgs) Handles Me.Load
        AddHandler MB_打开.Click, AddressOf 打开
        AddHandler MB_关闭.Click, AddressOf 停止
        AddHandler ExcellentProgressBar1.MouseDown, AddressOf 进度条_MouseDown
        AddHandler Me.ResizeEnd, AddressOf 视频容器尺寸变化事件
        AddHandler ModernPanel2.SizeChanged, AddressOf 视频容器尺寸变化事件
        重置进度显示()
    End Sub

    Public ffplayProcess As Process
    Public 是否已经取消播放 As Boolean = False

    Async Sub 播放(文件路径 As String)
        停止()
        是否已经取消播放 = False
        重置进度显示()

        ffplayProcess = New Process
        ffplayProcess.StartInfo.FileName = "ffplay"
        ffplayProcess.StartInfo.WorkingDirectory = If(设置_v6.实例对象.工作目录 <> "", 设置_v6.实例对象.工作目录, "")
        ffplayProcess.StartInfo.Arguments = $"-x {ModernPanel2.Width} -y {ModernPanel2.Height} ""{文件路径}"""
        ffplayProcess.StartInfo.UseShellExecute = False
        ffplayProcess.StartInfo.CreateNoWindow = True
        ffplayProcess.StartInfo.RedirectStandardOutput = True
        ffplayProcess.StartInfo.RedirectStandardError = True
        ffplayProcess.StartInfo.StandardOutputEncoding = Encoding.UTF8
        ffplayProcess.StartInfo.StandardErrorEncoding = Encoding.UTF8
        ffplayProcess.Start()
        输出读取取消源 = New CancellationTokenSource()
        Dim 当前进程 = ffplayProcess
        ffplay输出读取任务 = 读取ffplay输出Async(当前进程, 输出读取取消源.Token)

        While True
            If 是否已经取消播放 Then Exit Sub
            If ffplayProcess IsNot 当前进程 OrElse 当前进程 Is Nothing Then Exit Sub
            Try
                If 当前进程.HasExited Then Exit Sub
            Catch
                Exit Sub
            End Try

            Dim Handle As IntPtr = Await Task.Run(Function() FindWindowByTitle(当前进程.Id, 文件路径))
            If Handle <> IntPtr.Zero Then
                ffplayHandle = Handle
                Exit While
            End If
            If ffplayProcess Is Nothing Then Exit Sub
            Await Task.Delay(200)
        End While

        If 是否已经取消播放 OrElse ffplayProcess IsNot 当前进程 Then Exit Sub
        设置为播放容器子窗口()
        SetParent(ffplayHandle, ModernPanel2.Handle)
        设置播放窗口尺寸()
        FormMain_v6.Focus()
    End Sub

    Sub 视频容器尺寸变化事件()
        设置播放窗口尺寸()
    End Sub

    Private Sub 设置为播放容器子窗口()
        Dim style = GetWindowLong(ffplayHandle, GWL_STYLE)
        style = (style And Not (WS_CAPTION Or WS_THICKFRAME Or WS_POPUP)) Or WS_CHILD
        SetWindowLong(ffplayHandle, GWL_STYLE, style)
    End Sub

    Private Sub 设置播放窗口尺寸()
        If ffplayProcess Is Nothing OrElse ffplayHandle = IntPtr.Zero Then Exit Sub

        Dim size = ModernPanel2.ClientSize
        If size.Width <= 0 OrElse size.Height <= 0 Then Exit Sub

        SetWindowPos(ffplayHandle, IntPtr.Zero, 0, 0, size.Width, size.Height,
                     SWP_NOZORDER Or SWP_NOACTIVATE Or SWP_FRAMECHANGED)
    End Sub

    Sub 打开()
        Dim a As New OpenFileDialog With {.Multiselect = False}
        If a.ShowDialog() = DialogResult.OK Then 播放(a.FileName)
    End Sub

    Sub 停止()
        输出读取取消源?.Cancel()
        If ffplayProcess IsNot Nothing Then
            Try
                If Not ffplayProcess.HasExited Then ffplayProcess.Kill()
            Catch
            End Try
            Try
                ffplayProcess.Dispose()
            Catch
            End Try
            ffplayProcess = Nothing
        End If
        输出读取取消源?.Dispose()
        输出读取取消源 = Nothing
        ffplay输出读取任务 = Nothing
        ffplayHandle = IntPtr.Zero
        是否已经取消播放 = True
        重置进度显示()
    End Sub

    Private Async Function 读取ffplay输出Async(process As Process, token As CancellationToken) As Task
        If process Is Nothing Then Exit Function
        Try
            Await Task.WhenAll(
                读取ffplay输出流Async(process.StandardError, token),
                读取ffplay输出流Async(process.StandardOutput, token))
        Catch ex As OperationCanceledException
        Catch ex As ObjectDisposedException
        Catch ex As IOException
        Catch ex As InvalidOperationException
        End Try
    End Function

    Private Async Function 读取ffplay输出流Async(reader As StreamReader, token As CancellationToken) As Task
        If reader Is Nothing Then Exit Function
        Dim buffer(2047) As Char
        Dim lineBuilder As New StringBuilder()

        Try
            While Not token.IsCancellationRequested
                Dim count = Await reader.ReadAsync(buffer, 0, buffer.Length)
                If count <= 0 Then Exit While

                For i = 0 To count - 1
                    Dim ch = buffer(i)
                    If ch = ControlChars.Cr OrElse ch = ControlChars.Lf Then
                        提交ffplay输出行(lineBuilder, token)
                    Else
                        lineBuilder.Append(ch)
                    End If
                Next
            End While
            提交ffplay输出行(lineBuilder, token)
        Catch ex As ObjectDisposedException
        Catch ex As IOException
        Catch ex As InvalidOperationException
        End Try
    End Function

    Private Sub 提交ffplay输出行(lineBuilder As StringBuilder, token As CancellationToken)
        If token.IsCancellationRequested OrElse lineBuilder Is Nothing OrElse lineBuilder.Length = 0 Then Exit Sub
        Dim line = lineBuilder.ToString()
        lineBuilder.Clear()
        处理ffplay输出行(line, token)
    End Sub

    Private Sub 处理ffplay输出行(line As String, token As CancellationToken)
        If token.IsCancellationRequested Then Exit Sub
        Dim text = If(line, "").Trim()
        If text = "" Then Exit Sub

        Dim total As TimeSpan
        Dim current As TimeSpan
        SyncLock 进度状态锁
            Dim durationMatch = DurationRegex.Match(text)
            If durationMatch.Success Then
                Dim parsedTotal = 编码进度_v6.转换时间(durationMatch.Groups("time").Value)
                If parsedTotal > TimeSpan.Zero Then 当前总时长 = parsedTotal
            End If

            Dim progressMatch = FfplayProgressRegex.Match(text)
            If progressMatch.Success Then
                Dim seconds As Double
                If Double.TryParse(progressMatch.Groups("seconds").Value, NumberStyles.Any, CultureInfo.InvariantCulture, seconds) Then
                    当前播放位置 = TimeSpan.FromSeconds(Math.Max(0, seconds))
                End If
            End If

            total = 当前总时长
            current = 当前播放位置
        End SyncLock

        更新进度显示(text, current, total, token)
    End Sub

    Private Sub 更新进度显示(实时文本 As String, 当前时间 As TimeSpan, 总时长 As TimeSpan, token As CancellationToken, Optional 强制刷新 As Boolean = False)
        If token.IsCancellationRequested OrElse IsDisposed Then Exit Sub
        Dim 状态文本 = 格式化ffplay状态文本(实时文本)
        If Not 强制刷新 AndAlso 状态文本 <> "" AndAlso Not 允许刷新进度界面() Then Exit Sub

        If InvokeRequired Then
            Try
                BeginInvoke(Sub() 更新进度显示(实时文本, 当前时间, 总时长, token, True))
            Catch
            End Try
            Exit Sub
        End If
        If token.IsCancellationRequested OrElse IsDisposed Then Exit Sub

        If 状态文本 <> "" Then 最新ffplay状态文本 = 状态文本

        If 总时长.TotalSeconds > 0 Then
            Dim safeSeconds = Math.Min(Math.Max(当前时间.TotalSeconds, 0), 总时长.TotalSeconds)
            Dim maximumSeconds = Math.Max(1, CInt(Math.Ceiling(总时长.TotalSeconds)))
            Dim currentSeconds = Math.Max(0, Math.Min(maximumSeconds, CInt(Math.Round(safeSeconds))))
            If ExcellentProgressBar1.Maximum <> maximumSeconds Then ExcellentProgressBar1.Maximum = maximumSeconds
            ExcellentProgressBar1.Value = currentSeconds
            ExcellentProgressBar1.Text = 生成进度条文本(TimeSpan.FromSeconds(safeSeconds), 总时长)
        ElseIf 当前时间.TotalSeconds > 0 Then
            ExcellentProgressBar1.Value = 0
            ExcellentProgressBar1.Text = 生成进度条文本(当前时间, TimeSpan.Zero)
        End If
    End Sub

    Private Sub 重置进度显示()
        SyncLock 进度状态锁
            当前总时长 = TimeSpan.Zero
            当前播放位置 = TimeSpan.Zero
            最新ffplay状态文本 = ""
            上次进度界面刷新时间 = DateTime.MinValue
        End SyncLock

        If IsDisposed Then Exit Sub
        If InvokeRequired Then
            Try
                BeginInvoke(Sub() 重置进度显示())
            Catch
            End Try
            Exit Sub
        End If

        ExcellentProgressBar1.Maximum = 1
        ExcellentProgressBar1.Value = 0
        ExcellentProgressBar1.Text = "00:00 / --:--"
    End Sub

    Private Sub 进度条_MouseDown(sender As Object, e As MouseEventArgs)
        If e.Button <> MouseButtons.Left Then Exit Sub
        If ffplayHandle = IntPtr.Zero Then Exit Sub

        Dim total As TimeSpan
        SyncLock 进度状态锁
            total = 当前总时长
        End SyncLock
        If total.TotalSeconds <= 0 Then Exit Sub

        Dim contentLeft = ExcellentProgressBar1.Padding.Left
        Dim contentRight = ExcellentProgressBar1.ClientSize.Width - ExcellentProgressBar1.Padding.Right
        Dim contentWidth = contentRight - contentLeft
        If contentWidth <= 0 Then Exit Sub

        Dim safeX = Math.Min(Math.Max(e.X, contentLeft), contentRight)
        Dim ratio = 量化播放位置比例((safeX - contentLeft) / CDbl(contentWidth))
        模拟ffplay右键定位(ratio)

        Dim target = TimeSpan.FromSeconds(total.TotalSeconds * Math.Min(Math.Max(ratio, 0), 1))
        SyncLock 进度状态锁
            当前播放位置 = target
        End SyncLock
        更新进度显示("", target, total, CancellationToken.None, True)
    End Sub

    Private Sub 模拟ffplay右键定位(ratio As Double)
        If ffplayHandle = IntPtr.Zero Then Exit Sub

        Dim rect As New RECT()
        Dim width = ModernPanel2.ClientSize.Width
        Dim height = ModernPanel2.ClientSize.Height
        If GetClientRect(ffplayHandle, rect) Then
            width = Math.Max(1, rect.Right - rect.Left)
            height = Math.Max(1, rect.Bottom - rect.Top)
        End If

        Dim safeRatio = 量化播放位置比例(ratio)
        Dim x = CInt(Math.Round(safeRatio * Math.Max(0, width - 1)))
        Dim y = Math.Max(0, height \ 2)
        Dim lParam = MakeLParam(x, y)
        PostMessage(ffplayHandle, WM_MOUSEMOVE, IntPtr.Zero, lParam)
        PostMessage(ffplayHandle, WM_RBUTTONDOWN, MK_RBUTTON, lParam)
        PostMessage(ffplayHandle, WM_RBUTTONUP, IntPtr.Zero, lParam)
    End Sub

    Private Shared Function MakeLParam(x As Integer, y As Integer) As IntPtr
        Return New IntPtr(((y And &HFFFF) << 16) Or (x And &HFFFF))
    End Function

    Private Function 允许刷新进度界面() As Boolean
        Dim now = DateTime.UtcNow
        SyncLock 进度状态锁
            If (now - 上次进度界面刷新时间).TotalMilliseconds < 进度界面刷新间隔毫秒 Then Return False
            上次进度界面刷新时间 = now
            Return True
        End SyncLock
    End Function

    Private Shared Function 量化播放位置比例(ratio As Double) As Double
        Dim safeRatio = Math.Min(Math.Max(ratio, 0), 1)
        Dim percent = Math.Round(safeRatio * 100, 2, MidpointRounding.AwayFromZero)
        Return percent / 100.0
    End Function

    Private Shared Function 格式化播放时间(value As TimeSpan) As String
        Dim totalSeconds = CInt(Math.Floor(Math.Max(0, value.TotalSeconds)))
        Dim h = totalSeconds \ 3600
        Dim m = (totalSeconds Mod 3600) \ 60
        Dim s = totalSeconds Mod 60
        If h > 0 Then Return $"{h:00}:{m:00}:{s:00}"
        Return $"{m:00}:{s:00}"
    End Function

    Private Function 生成进度条文本(当前时间 As TimeSpan, 总时长 As TimeSpan) As String
        Dim text = $"{格式化播放时间(当前时间)} / {If(总时长 > TimeSpan.Zero, 格式化播放时间(总时长), "--:--")}"
        If 最新ffplay状态文本 <> "" Then text &= $" | {最新ffplay状态文本}"
        Return text
    End Function

    Private Shared Function 格式化ffplay状态文本(line As String) As String
        Dim text = If(line, "").Trim()
        If text = "" OrElse Not FfplayProgressRegex.IsMatch(text) Then Return ""

        text = Regex.Replace(text, "\b([AM]-V):\s*", "$1=", RegexOptions.IgnoreCase)
        text = Regex.Replace(text, "\s*=\s*", "=")
        text = Regex.Replace(text, "\s+", " ")
        Return text.Trim()
    End Function

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

    Private Sub ModernPanel2_DragEnter(sender As Object, e As DragEventArgs) Handles ModernPanel2.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub ModernPanel2_DragDrop(sender As Object, e As DragEventArgs) Handles ModernPanel2.DragDrop
        Dim files = e.Data.GetData(DataFormats.FileDrop)
        If files.Length > 0 Then 播放(files(0))
    End Sub
End Class
