Imports LakeUI
Imports Microsoft.VisualBasic.ApplicationServices

Namespace My
    ' The following events are available for MyApplication:
    ' Startup: Raised when the application starts, before the startup form is created.
    ' Shutdown: Raised after all application forms are closed. This event is not raised if the application terminates abnormally.
    ' UnhandledException: Raised if the application encounters an unhandled exception.
    ' StartupNextInstance: Raised when launching a single-instance application and the application is already active.
    ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.

    ' **NEW** ApplyApplicationDefaults: Raised when the application queries default values to be set for the application.

    ' Example:
    ' Private Sub MyApplication_ApplyApplicationDefaults(sender As Object, e As ApplyApplicationDefaultsEventArgs) Handles Me.ApplyApplicationDefaults
    '
    '   ' Setting the application-wide default Font:
    '   e.Font = New Font(FontFamily.GenericSansSerif, 12, FontStyle.Regular)
    '
    '   ' Setting the HighDpiMode for the Application:
    '   e.HighDpiMode = HighDpiMode.PerMonitorV2
    '
    '   ' If a splash dialog is used, this sets the minimum display time:
    '   e.MinimumSplashScreenDisplayTime = 4000
    ' End Sub


    Partial Friend Class MyApplication
        Private Sub MyApplication_Startup(sender As Object, e As StartupEventArgs) Handles Me.Startup
            If PathEquals(System.Windows.Forms.Application.StartupPath, Environment.GetFolderPath(Environment.SpecialFolder.Desktop)) Then
                ExOverlayMsgBox($"{vbCrLf}这是以便携形式发行的单文件应用程序，需要建一个文件夹单独装起来，其会将当前目录作为自己的数据目录！", MsgBoxStyle.Critical, "不要直接放在桌面运行")
                End
            End If
            If e.CommandLine.Contains("fullscreen") Then
                FormMain_v6.FormBorderStyle = FormBorderStyle.None
                FormMain_v6.WindowState = FormWindowState.Maximized
            End If
        End Sub

        Private Sub MyApplication_StartupNextInstance(sender As Object, e As StartupNextInstanceEventArgs) Handles Me.StartupNextInstance
            启动参数响应_v6.处理接收的参数(e.CommandLine.ToList)
        End Sub

        Shared Function NormalizePath(path As String) As String
            If String.IsNullOrWhiteSpace(path) Then Return String.Empty
            Try
                Return IO.Path.GetFullPath(path).TrimEnd(IO.Path.DirectorySeparatorChar, IO.Path.AltDirectorySeparatorChar).ToLowerInvariant()
            Catch
                Return path.TrimEnd("\"c, "/"c).ToLowerInvariant()
            End Try
        End Function
        Shared Function PathEquals(path1 As String, path2 As String) As Boolean
            Return NormalizePath(path1) = NormalizePath(path2)
        End Function

    End Class


End Namespace
