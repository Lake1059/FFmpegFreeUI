Imports System.ComponentModel
Imports LibreHardwareMonitor.Hardware

Public Class 性能统计
    Implements IVisitor, IDisposable

    Private ReadOnly computer As Computer

    Public Property 处理器信息 As New Dictionary(Of String, Object)
    Public Property 显卡信息 As New Dictionary(Of String, Object)

    Public Sub New()
        computer = New Computer() With {
            .IsCpuEnabled = True,
            .IsGpuEnabled = True,
            .IsMemoryEnabled = False,
            .IsMotherboardEnabled = False,
            .IsControllerEnabled = False,
            .IsPsuEnabled = False,
            .IsNetworkEnabled = False,
            .IsStorageEnabled = False
        }
        Try
            computer.Open()
        Catch ex As Exception
        Finally

        End Try
    End Sub

    Public Sub Update()
        Try
            computer.Accept(Me)
        Catch ex As Exception
        End Try
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        computer.Close()
    End Sub

    Public Sub VisitComputer(computer As IComputer) Implements IVisitor.VisitComputer
        For Each hardware In computer.Hardware
            Try
                hardware.Accept(Me)
            Catch ex As UnauthorizedAccessException
            Catch ex As Win32Exception
            End Try
        Next
    End Sub

    Public Sub VisitHardware(hardware As IHardware) Implements IVisitor.VisitHardware
        Try
            hardware.Update()
        Catch ex As UnauthorizedAccessException
            Exit Sub
        Catch ex As Win32Exception
            Exit Sub
        End Try

        Select Case hardware.HardwareType
            Case HardwareType.Cpu
                Dim cpuUsages As New Dictionary(Of String, Object)
                For Each sensor In hardware.Sensors
                    Select Case sensor.SensorType
                        Case SensorType.Load
                            Select Case sensor.Name
                                Case "CPU Total"
                                Case Else
                                    If sensor.Name.Contains("CPU Core #") Then
                                        Dim v1 = 转换最大值100的百分数(sensor.Value.GetValueOrDefault())
                                        cpuUsages($"逻辑核心 {sensor.Name.Replace("CPU Core ", "")} | {v1}") = v1
                                    End If
                            End Select
                    End Select
                Next
                If cpuUsages.Count > 0 Then 处理器信息 = cpuUsages

            Case HardwareType.GpuNvidia, HardwareType.GpuAmd, HardwareType.GpuIntel
                Dim gpuUsages As New Dictionary(Of String, Object)
                For Each sensor In hardware.Sensors
                    Select Case sensor.SensorType
                        Case SensorType.Load
                            Select Case sensor.Name
                                'Case "GPU Memory Controller", "GPU Bus", "GPU Power", "GPU Board Power"
                                'Case "GPU Core"
                                'Case "GPU Video Engine"
                                Case Else
                                    If sensor.Value.GetValueOrDefault() = 0 Then Continue For
                                    Dim v1 = 转换最大值100的百分数(sensor.Value.GetValueOrDefault())
                                    gpuUsages($"{hardware.Name} | {sensor.Name} | {v1}") = v1
                            End Select
                    End Select
                Next
                If gpuUsages.Count > 0 Then 显卡信息 = gpuUsages

        End Select



        'For Each sensor In hardware.Sensors
        '    If sensor.SensorType = SensorType.Power OrElse
        '       sensor.SensorType = SensorType.Temperature OrElse
        '       sensor.SensorType = SensorType.Fan OrElse
        '       sensor.SensorType = SensorType.Data Then
        '        RaiseEvent OtherSensorUpdated(sensor.SensorType, sensor.Name, sensor.Value)
        '    End If
        'Next

        'For Each subHardware In hardware.SubHardware
        '    Try
        '        subHardware.Accept(Me)
        '    Catch ex As UnauthorizedAccessException
        '        ' 权限不足，跳过
        '    Catch ex As Win32Exception
        '        ' 权限不足，跳过
        '    End Try
        'Next
    End Sub

    Public Sub VisitSensor(sensor As ISensor) Implements IVisitor.VisitSensor
        ' 不直接处理单个传感器
    End Sub

    Public Sub VisitParameter(parameter As IParameter) Implements IVisitor.VisitParameter
        ' 不处理参数
    End Sub

    Public Shared Function 转换最大值1的百分数(s As Single) As String
        Return (s * 100).ToString("0") & "%"
    End Function

    Public Shared Function 转换最大值100的百分数(s As Single) As String
        Return s.ToString("0") & "%"
    End Function

    Public Shared Sub 刷新到界面上()
        Form1.性能统计对象.Update()

        If Form1.Panel18.Visible Then
            Dim cpus = Form1.性能统计对象.处理器信息.Keys.ToList
            For i = 0 To cpus.Count - 1
                If i >= Form1.ListView3.Items.Count Then Form1.ListView3.Items.Add(New ListViewItem)
                Form1.ListView3.Items(i).Text = cpus(i)
            Next
        End If

        Dim gpus = Form1.性能统计对象.显卡信息.Keys.ToList
        gpus.Sort()

        For i = 0 To gpus.Count - 1
            If i >= Form1.ListView4.Items.Count Then Form1.ListView4.Items.Add(New ListViewItem)
            Form1.ListView4.Items(i).Text = gpus(i)
        Next
        While Form1.ListView4.Items.Count > gpus.Count
            Form1.ListView4.Items.RemoveAt(Form1.ListView4.Items.Count - 1)
        End While

    End Sub

    Public Shared Sub 绑定性能统计处理器列表视图事件()
        Form1.ListView3.DoubleBuffer
        AddHandler Form1.ListView3.DrawSubItem, Sub(sender, e) 绘制性能统计处理器子项(sender, e)
        AddHandler Form1.ListView3.SelectedIndexChanged, Sub(sender, e) sender.Invalidate(sender.ClientRectangle)
        AddHandler Form1.ListView3.ItemMouseHover, Sub(sender, e) sender.Invalidate(sender.ClientRectangle)
    End Sub
    Public Shared Sub 绘制性能统计处理器子项(哪个列表视图控件 As ListView, e As DrawListViewSubItemEventArgs)
        Try
            Form1.重新创建句柄()
            If Not e.Bounds.IntersectsWith(哪个列表视图控件.ClientRectangle) OrElse e.Bounds.Width = 0 Then Exit Sub
            Dim 项背景色 As Color = If(哪个列表视图控件.SelectedIndices.Contains(e.ItemIndex), 暗黑列表视图自绘制.项被选中时的背景颜色, 哪个列表视图控件.BackColor)
            Dim 文本高度修正 As Integer = (e.Bounds.Height - TextRenderer.MeasureText(e.SubItem.Text, e.SubItem.Font).Height) \ 2
            Dim 文本绘制区 As New Rectangle(e.Bounds.X + 5 * Form1.DPI, e.Bounds.Y + 文本高度修正, e.Bounds.Width, e.Bounds.Height)
            Dim 文字显示所需尺寸 As Size = TextRenderer.MeasureText(e.SubItem.Text, e.SubItem.Font)
            Dim 实际要绘制的文本 As String = e.SubItem.Text
            If 文字显示所需尺寸.Width > (e.Bounds.Width - 3 * Form1.DPI) Then
                Dim 点号所占用的宽度 As Integer = TextRenderer.MeasureText("...", e.SubItem.Font).Width
                Dim 实际文本可用宽度 As Integer = e.Bounds.Width - 点号所占用的宽度
                While TextRenderer.MeasureText(实际要绘制的文本, e.SubItem.Font).Width > 实际文本可用宽度 AndAlso 实际要绘制的文本.Length > 0
                    实际要绘制的文本 = 实际要绘制的文本.Substring(0, 实际要绘制的文本.Length - 1)
                End While
                实际要绘制的文本 &= "..."
            End If
            Using brush As New SolidBrush(项背景色)
                e.Graphics.FillRectangle(brush, e.Bounds)
            End Using

            Dim 进度文本 As String = ""
            If Not Form1.性能统计对象.处理器信息.TryGetValue(e.Item.Text, 进度文本) Then GoTo jx1
            Dim 进度值 As Double = 0
            If 进度文本.EndsWith("%"c) Then
                Dim unused = Double.TryParse(进度文本.AsSpan(0, 进度文本.Length - 1), 进度值)
                进度值 = Math.Max(0, Math.Min(100, 进度值))
            Else
                GoTo jx1
            End If
            If 进度值 <= 0 Then GoTo jx1
            Dim 边距 As Integer = 3 * Form1.DPI
            Dim 高度 As Integer = Math.Max(8 * Form1.DPI, e.Bounds.Height - 6 * Form1.DPI)
            Dim 区域 As New Rectangle(e.Bounds.X + 边距, e.Bounds.Y + (e.Bounds.Height - 高度) \ 2, e.Bounds.Width - 2 * 边距, 高度)
            Dim 进度条颜色 As Color
            Select Case 进度值
                Case < 50 : 进度条颜色 = Color.FromArgb(100, Color.Green)
                Case < 80 : 进度条颜色 = Color.FromArgb(80, Color.CornflowerBlue)
                Case < 90 : 进度条颜色 = Color.FromArgb(128, Color.DarkGoldenrod)
                Case Else : 进度条颜色 = Color.FromArgb(128, Color.OrangeRed)
            End Select
            Using 填充画刷 As New SolidBrush(进度条颜色)
                e.Graphics.FillRectangle(填充画刷, New Rectangle(区域.X, 区域.Y, 区域.Width * (进度值 / 100), 区域.Height))
            End Using
jx1:
            TextRenderer.DrawText(e.Graphics, 实际要绘制的文本.Replace("&", "&&"), e.SubItem.Font, 文本绘制区, e.Item.ForeColor, Color.Transparent, TextFormatFlags.Default)
        Catch ex As Exception
        End Try
    End Sub
    Public Shared Sub 绑定性能统计显卡列表视图事件()
        Form1.ListView4.DoubleBuffer
        AddHandler Form1.ListView4.DrawSubItem, Sub(sender, e) 绘制性能统计显卡子项(sender, e)
        AddHandler Form1.ListView4.SelectedIndexChanged, Sub(sender, e) sender.Invalidate(sender.ClientRectangle)
        AddHandler Form1.ListView4.ItemMouseHover, Sub(sender, e) sender.Invalidate(sender.ClientRectangle)
    End Sub
    Public Shared Sub 绘制性能统计显卡子项(哪个列表视图控件 As ListView, e As DrawListViewSubItemEventArgs)
        Try
            Form1.重新创建句柄()
            If Not e.Bounds.IntersectsWith(哪个列表视图控件.ClientRectangle) OrElse e.Bounds.Width = 0 Then Exit Sub
            Dim 项背景色 As Color = If(哪个列表视图控件.SelectedIndices.Contains(e.ItemIndex), 暗黑列表视图自绘制.项被选中时的背景颜色, 哪个列表视图控件.BackColor)
            Dim 文本高度修正 As Integer = (e.Bounds.Height - TextRenderer.MeasureText(e.SubItem.Text, e.SubItem.Font).Height) \ 2
            Dim 文本绘制区 As New Rectangle(e.Bounds.X + 5 * Form1.DPI, e.Bounds.Y + 文本高度修正, e.Bounds.Width, e.Bounds.Height)
            Dim 文字显示所需尺寸 As Size = TextRenderer.MeasureText(e.SubItem.Text, e.SubItem.Font)
            Dim 实际要绘制的文本 As String = e.SubItem.Text
            If 文字显示所需尺寸.Width > (e.Bounds.Width - 3 * Form1.DPI) Then
                Dim 点号所占用的宽度 As Integer = TextRenderer.MeasureText("...", e.SubItem.Font).Width
                Dim 实际文本可用宽度 As Integer = e.Bounds.Width - 点号所占用的宽度
                While TextRenderer.MeasureText(实际要绘制的文本, e.SubItem.Font).Width > 实际文本可用宽度 AndAlso 实际要绘制的文本.Length > 0
                    实际要绘制的文本 = 实际要绘制的文本.Substring(0, 实际要绘制的文本.Length - 1)
                End While
                实际要绘制的文本 &= "..."
            End If
            Using brush As New SolidBrush(项背景色)
                e.Graphics.FillRectangle(brush, e.Bounds)
            End Using

            Dim 进度文本 As String = Form1.性能统计对象.显卡信息(e.Item.Text)
            Dim 进度值 As Double = 0
            If 进度文本.EndsWith("%"c) Then
                Dim unused = Double.TryParse(进度文本.AsSpan(0, 进度文本.Length - 1), 进度值)
                进度值 = Math.Max(0, Math.Min(100, 进度值))
            Else
                GoTo jx1
            End If
            If 进度值 <= 0 Then GoTo jx1
            Dim 边距 As Integer = 3 * Form1.DPI
            Dim 高度 As Integer = Math.Max(8 * Form1.DPI, e.Bounds.Height - 6 * Form1.DPI)
            Dim 区域 As New Rectangle(e.Bounds.X + 边距, e.Bounds.Y + (e.Bounds.Height - 高度) \ 2, e.Bounds.Width - 2 * 边距, 高度)
            Dim 进度条颜色 As Color
            Select Case 进度值
                Case < 50 : 进度条颜色 = Color.FromArgb(100, Color.Green)
                Case < 80 : 进度条颜色 = Color.FromArgb(80, Color.CornflowerBlue)
                Case < 90 : 进度条颜色 = Color.FromArgb(128, Color.DarkGoldenrod)
                Case Else : 进度条颜色 = Color.FromArgb(128, Color.OrangeRed)
            End Select
            Using 填充画刷 As New SolidBrush(进度条颜色)
                e.Graphics.FillRectangle(填充画刷, New Rectangle(区域.X, 区域.Y, 区域.Width * (进度值 / 100), 区域.Height))
            End Using
jx1:
            TextRenderer.DrawText(e.Graphics, 实际要绘制的文本.Replace("&", "&&"), e.SubItem.Font, 文本绘制区, e.Item.ForeColor, Color.Transparent, TextFormatFlags.Default)
        Catch ex As Exception
        End Try
    End Sub
End Class