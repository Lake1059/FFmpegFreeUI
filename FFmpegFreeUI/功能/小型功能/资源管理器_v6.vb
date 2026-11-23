Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Threading

Friend Module 资源管理器_v6
    Private Const COINIT_APARTMENTTHREADED As UInteger = 2UI

    Public Function 定位文件Async(filePaths As IEnumerable(Of String)) As Task(Of List(Of String))
        Dim paths = filePaths.ToArray()
        Dim completion As New TaskCompletionSource(Of List(Of String))(TaskCreationOptions.RunContinuationsAsynchronously)
        ' Shell 路径解析可能访问网络目录，在独立的 STA 线程上完成。
        Dim thread As New Thread(
            Sub()
                Try
                    Marshal.ThrowExceptionForHR(CoInitializeEx(IntPtr.Zero, COINIT_APARTMENTTHREADED))
                    Dim failures As List(Of String)
                    Try
                        failures = 定位文件(paths)
                    Finally
                        CoUninitialize()
                    End Try
                    completion.SetResult(failures)
                Catch ex As Exception
                    completion.TrySetException(ex)
                End Try
            End Sub) With {.IsBackground = True}
        thread.SetApartmentState(ApartmentState.STA)
        thread.Start()
        Return completion.Task
    End Function

    Private Function 定位文件(filePaths As String()) As List(Of String)
        Dim failures As New List(Of String)
        Dim folders = filePaths.Select(Function(filePath) Path.GetFullPath(filePath)).
            Distinct(StringComparer.OrdinalIgnoreCase).
            GroupBy(Function(filePath) Path.GetDirectoryName(filePath), StringComparer.OrdinalIgnoreCase)
        For Each folder In folders
            定位同一目录的文件(folder.Key, folder, failures)
        Next
        Return failures
    End Function

    Private Sub 定位同一目录的文件(folderPath As String, filePaths As IEnumerable(Of String), failures As List(Of String))
        Dim folderPidl As IntPtr = IntPtr.Zero
        Dim absolutePidls As New List(Of IntPtr)
        Dim children As New List(Of IntPtr)
        Try
            If Not 尝试解析路径(folderPath, folderPidl, failures) Then Return
            For Each filePath In filePaths
                Dim pidl As IntPtr = IntPtr.Zero
                If Not 尝试解析路径(filePath, pidl, failures) Then Continue For
                absolutePidls.Add(pidl)
                ' 子项 PIDL 指向完整 PIDL 中的最后一项，调用结束前要保留完整 PIDL。
                children.Add(ILFindLastID(pidl))
            Next
            If children.Count = 0 Then Return
            Dim result = SHOpenFolderAndSelectItems(folderPidl, CUInt(children.Count), children.ToArray(), 0UI)
            If result < 0 Then 添加定位失败(failures, folderPath, result)
        Finally
            For Each pidl In absolutePidls
                Marshal.FreeCoTaskMem(pidl)
            Next
            Marshal.FreeCoTaskMem(folderPidl)
        End Try
    End Sub

    Private Function 尝试解析路径(filePath As String, ByRef pidl As IntPtr, failures As List(Of String)) As Boolean
        Dim attributes As UInteger
        Dim result = SHParseDisplayName(filePath, IntPtr.Zero, pidl, 0UI, attributes)
        If result >= 0 Then Return True
        Marshal.FreeCoTaskMem(pidl)
        pidl = IntPtr.Zero
        添加定位失败(failures, filePath, result)
        Return False
    End Function

    Private Sub 添加定位失败(failures As List(Of String), path As String, result As Integer)
        failures.Add($"{path}（{Marshal.GetExceptionForHR(result, New IntPtr(-1)).Message}）")
    End Sub

    <DllImport("ole32.dll", ExactSpelling:=True)>
    Private Function CoInitializeEx(reserved As IntPtr, coInit As UInteger) As Integer
    End Function

    <DllImport("ole32.dll", ExactSpelling:=True)>
    Private Sub CoUninitialize()
    End Sub

    <DllImport("shell32.dll", CharSet:=CharSet.Unicode, ExactSpelling:=True)>
    Private Function SHParseDisplayName(displayName As String,
                                        bindContext As IntPtr,
                                        ByRef pidl As IntPtr,
                                        attributesIn As UInteger,
                                        ByRef attributesOut As UInteger) As Integer
    End Function

    <DllImport("shell32.dll", ExactSpelling:=True)>
    Private Function ILFindLastID(pidl As IntPtr) As IntPtr
    End Function

    <DllImport("shell32.dll", ExactSpelling:=True)>
    Private Function SHOpenFolderAndSelectItems(folderPidl As IntPtr,
                                                childCount As UInteger,
                                                <[In], MarshalAs(UnmanagedType.LPArray, SizeParamIndex:=1)> childPidls As IntPtr(),
                                                flags As UInteger) As Integer
    End Function
End Module
