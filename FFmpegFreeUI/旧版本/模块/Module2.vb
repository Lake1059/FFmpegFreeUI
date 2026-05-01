Imports System.Runtime.InteropServices

Module Module2
    <DllImport("user32.dll", CharSet:=CharSet.Auto)>
    Public Function SendMessage(hWnd As IntPtr, msg As Integer, wParam As Integer, lParam As Integer) As IntPtr
    End Function
End Module
