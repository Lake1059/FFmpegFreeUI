Imports LakeUI
Imports Microsoft.VisualBasic.ApplicationServices
Imports System.IO
Imports System.Runtime.InteropServices

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
        Private Shared ReadOnly 桌面目录KnownFolderId As New Guid("B4BFCC3A-DB2C-424C-B029-7FE99A87C641")
        Private Shared ReadOnly 公共桌面目录KnownFolderId As New Guid("C4AA340D-F20F-4863-AFEF-F87EF2E6BA25")
        Private Shared ReadOnly 下载目录KnownFolderId As New Guid("374DE290-123F-4565-9164-39C4925E467B")
        Private Shared ReadOnly 公共下载目录KnownFolderId As New Guid("3D644C9B-1FB8-4F30-9B45-F670235F79C0")
        Private Shared ReadOnly Internet缓存目录KnownFolderId As New Guid("352481E8-33BE-4251-BA85-6007CAEDCF9D")

        <DllImport("shell32.dll")>
        Private Shared Function SHGetKnownFolderPath(<MarshalAs(UnmanagedType.LPStruct)> ByVal rfid As Guid, ByVal dwFlags As UInteger, ByVal hToken As IntPtr, ByRef ppszPath As IntPtr) As Integer
        End Function

        Private Sub MyApplication_Startup(sender As Object, e As StartupEventArgs) Handles Me.Startup
            Dim 禁止启动位置说明 = 获取禁止启动位置说明(System.Windows.Forms.Application.StartupPath)
            If Not String.IsNullOrEmpty(禁止启动位置说明) Then
                ExOverlayMsgBox(
                    $"{vbCrLf}当前启动位置不受支持：{禁止启动位置说明}{vbCrLf}{vbCrLf}这是以便携形式发行的单文件应用程序，需要建一个文件夹单独装起来，其会将当前目录作为自己的数据目录。{vbCrLf}{vbCrLf}为避免程序数据或更新文件被放到桌面、下载目录或系统缓存目录，已阻止继续运行。",
                    MsgBoxStyle.Critical,
                    "不要直接在桌面、下载目录或缓存目录运行")
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

        Private Shared Function 获取禁止启动位置说明(程序目录 As String) As String
            For Each 规则 In 获取禁止启动目录列表()
                Dim 允许子目录 = String.Equals(规则.Key, "桌面目录", StringComparison.OrdinalIgnoreCase) OrElse
                    String.Equals(规则.Key, "公共桌面目录", StringComparison.OrdinalIgnoreCase)
                Dim 命中规则 = If(允许子目录,
                    路径等于(程序目录, 规则.Value),
                    路径位于或等于(程序目录, 规则.Value))
                If 命中规则 Then
                    Return $"{规则.Key}：{规则.Value}"
                End If
            Next

            Return ""
        End Function

        Private Shared Function 获取禁止启动目录列表() As List(Of KeyValuePair(Of String, String))
            Dim 列表 As New List(Of KeyValuePair(Of String, String))

            添加禁止启动目录(列表, "桌面目录", Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory))
            添加禁止启动目录(列表, "桌面目录", 获取KnownFolder路径(桌面目录KnownFolderId))
            添加禁止启动目录(列表, "公共桌面目录", 获取KnownFolder路径(公共桌面目录KnownFolderId))
            添加禁止启动目录(列表, "下载目录", 获取KnownFolder路径(下载目录KnownFolderId))
            添加禁止启动目录(列表, "下载目录", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads"))
            添加禁止启动目录(列表, "公共下载目录", 获取KnownFolder路径(公共下载目录KnownFolderId))
            添加禁止启动目录(列表, "系统缓存目录", 获取KnownFolder路径(Internet缓存目录KnownFolderId))
            添加禁止启动目录(列表, "系统缓存目录", Path.GetTempPath())
            添加禁止启动目录(列表, "系统缓存目录", Environment.GetEnvironmentVariable("TEMP"))
            添加禁止启动目录(列表, "系统缓存目录", Environment.GetEnvironmentVariable("TMP"))

            Dim Windows目录 = Environment.GetFolderPath(Environment.SpecialFolder.Windows)
            If Not String.IsNullOrWhiteSpace(Windows目录) Then
                添加禁止启动目录(列表, "系统缓存目录", Path.Combine(Windows目录, "Temp"))
            End If

            Return 列表
        End Function

        Private Shared Sub 添加禁止启动目录(列表 As List(Of KeyValuePair(Of String, String)), 名称 As String, 目录路径 As String)
            If String.IsNullOrWhiteSpace(目录路径) Then Return

            Dim 规范路径 As String = ""
            Try
                规范路径 = 规范化目录路径(目录路径)
            Catch
                Return
            End Try

            If String.IsNullOrWhiteSpace(规范路径) Then Return
            If 列表.Any(Function(项目) String.Equals(项目.Value, 规范路径, StringComparison.OrdinalIgnoreCase)) Then Return

            列表.Add(New KeyValuePair(Of String, String)(名称, 规范路径))
        End Sub

        Private Shared Function 获取KnownFolder路径(folderId As Guid) As String
            Dim 路径指针 As IntPtr = IntPtr.Zero
            Try
                If SHGetKnownFolderPath(folderId, 0UI, IntPtr.Zero, 路径指针) <> 0 OrElse 路径指针 = IntPtr.Zero Then Return ""
                Dim 路径 = Marshal.PtrToStringUni(路径指针)
                If String.IsNullOrWhiteSpace(路径) Then Return ""
                Return Path.GetFullPath(路径)
            Catch
                Return ""
            Finally
                If 路径指针 <> IntPtr.Zero Then
                    Marshal.FreeCoTaskMem(路径指针)
                End If
            End Try
        End Function

        Private Shared Function 路径等于(路径1 As String, 路径2 As String) As Boolean
            If String.IsNullOrWhiteSpace(路径1) OrElse String.IsNullOrWhiteSpace(路径2) Then Return False

            Try
                Return String.Equals(规范化目录路径(路径1), 规范化目录路径(路径2), StringComparison.OrdinalIgnoreCase)
            Catch
                Return False
            End Try
        End Function

        Private Shared Function 路径位于或等于(子路径 As String, 父目录 As String) As Boolean
            If String.IsNullOrWhiteSpace(子路径) OrElse String.IsNullOrWhiteSpace(父目录) Then Return False

            Try
                Dim 规范子路径 = 规范化目录路径(子路径)
                Dim 规范父目录 = 规范化目录路径(父目录)
                If String.Equals(规范子路径, 规范父目录, StringComparison.OrdinalIgnoreCase) Then Return True
                If 是文件系统根目录(规范父目录) Then Return False
                Return 规范子路径.StartsWith(确保目录路径以分隔符结尾(规范父目录), StringComparison.OrdinalIgnoreCase)
            Catch
                Return False
            End Try
        End Function

        Private Shared Function 规范化目录路径(目录路径 As String) As String
            Dim 完整路径 = Path.GetFullPath(目录路径)
            Dim 根目录 = Path.GetPathRoot(完整路径)
            Dim 去尾路径 = 完整路径.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)

            If Not String.IsNullOrEmpty(根目录) Then
                Dim 去尾根目录 = 根目录.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
                If String.Equals(去尾路径, 去尾根目录, StringComparison.OrdinalIgnoreCase) Then Return 根目录
            End If

            Return 去尾路径
        End Function

        Private Shared Function 是文件系统根目录(目录路径 As String) As Boolean
            Dim 根目录 = Path.GetPathRoot(Path.GetFullPath(目录路径))
            If String.IsNullOrEmpty(根目录) Then Return False
            Return String.Equals(规范化目录路径(目录路径), 规范化目录路径(根目录), StringComparison.OrdinalIgnoreCase)
        End Function

        Private Shared Function 确保目录路径以分隔符结尾(目录路径 As String) As String
            If String.IsNullOrEmpty(目录路径) Then Return 目录路径
            If 目录路径.EndsWith(Path.DirectorySeparatorChar) OrElse 目录路径.EndsWith(Path.AltDirectorySeparatorChar) Then Return 目录路径
            Return 目录路径 & Path.DirectorySeparatorChar
        End Function

    End Class


End Namespace
