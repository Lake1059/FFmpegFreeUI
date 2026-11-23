Friend NotInheritable Class 编码队列任务窗口组_v6(Of TWindow As Form)
    Private ReadOnly 窗口 As New Dictionary(Of String, TWindow)(StringComparer.Ordinal)
    Private ReadOnly 创建窗口 As Func(Of 编码任务_v6, TWindow)

    Public Sub New(factory As Func(Of 编码任务_v6, TWindow))
        创建窗口 = factory
    End Sub

    Public Function 获取已打开窗口() As TWindow()
        Return 窗口.Values.ToArray()
    End Function

    Public Sub 打开或激活(owner As Form, tasks As IEnumerable(Of 编码任务_v6))
        Dim mainForm = If(TryCast(owner.TopLevelControl, Form), owner)
        For Each task In tasks
            Dim id = task.ID
            Dim existing As TWindow = Nothing
            If 窗口.TryGetValue(id, existing) AndAlso Not existing.IsDisposed Then
                If existing.WindowState = FormWindowState.Minimized Then existing.WindowState = FormWindowState.Normal
                existing.Activate()
                Continue For
            End If

            Dim newWindow = 创建窗口(task)
            窗口(id) = newWindow
            AddHandler newWindow.Disposed, Sub() 窗口.Remove(id)
            Try
                newWindow.StartPosition = FormStartPosition.Manual
                newWindow.Location = 获取窗口位置(newWindow, mainForm)
                ' Load 时窗口已根据所在显示器完成 DPI 缩放。
                AddHandler newWindow.Load, Sub() newWindow.Location = 获取窗口位置(newWindow, mainForm)
                界面主题_v6.应用当前主题到窗体(newWindow)
                newWindow.Show(mainForm)
            Catch
                newWindow.Dispose()
                Throw
            End Try
        Next
    End Sub

    Private Function 获取窗口位置(window As Form, mainForm As Form) As Point
        Dim ownerVisible = mainForm.Visible AndAlso mainForm.WindowState <> FormWindowState.Minimized
        Dim area = If(ownerVisible, Screen.FromControl(mainForm), Screen.FromPoint(Cursor.Position)).WorkingArea
        Dim bounds = If(ownerVisible, mainForm.Bounds, area)
        Dim right = Math.Max(area.Left, area.Right - window.Width)
        Dim bottom = Math.Max(area.Top, area.Bottom - window.Height)
        Dim center As New Point(Math.Clamp(bounds.Left + (bounds.Width - window.Width) \ 2, area.Left, right),
                                Math.Clamp(bounds.Top + (bounds.Height - window.Height) \ 2, area.Top, bottom))
        Dim occupied = 窗口.Values.
            Where(Function(other) other IsNot window AndAlso other.Visible).
            Select(Function(other) If(other.WindowState = FormWindowState.Normal, other.Location, other.RestoreBounds.Location)).
            ToHashSet()
        If Not occupied.Contains(center) Then Return center

        Dim spacing = Math.Max(1, CInt(28 * mainForm.DeviceDpi / 96.0))
        Dim xDirection = If(right - center.X >= center.X - area.Left, 1, -1)
        Dim yDirection = If(bottom - center.Y >= center.Y - area.Top, 1, -1)
        Dim maxXSteps = Math.Max(right - center.X, center.X - area.Left) \ spacing
        Dim maxYSteps = Math.Max(bottom - center.Y, center.Y - area.Top) \ spacing
        For index = 1 To Math.Max(maxXSteps, maxYSteps)
            Dim position As New Point(center.X + xDirection * Math.Min(index, maxXSteps) * spacing,
                                      center.Y + yDirection * Math.Min(index, maxYSteps) * spacing)
            If Not occupied.Contains(position) Then Return position
        Next

        ' 错开到屏幕边缘后，从工作区的另一侧继续排列。
        For y = area.Top To bottom Step spacing
            For x = area.Left To right Step spacing
                Dim position As New Point(x, y)
                If Not occupied.Contains(position) Then Return position
            Next
        Next
        Return center
    End Function
End Class
