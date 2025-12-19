Imports LibreHardwareMonitor.Hardware

Public Class 性能统计
    Implements IVisitor, IDisposable
    Private ReadOnly computer As Computer
    Public Sub New()
        computer = New Computer() With {
            .IsCpuEnabled = True,
            .IsGpuEnabled = True,
            .IsMemoryEnabled = True,
            .IsNetworkEnabled = False,
            .IsStorageEnabled = False
        }
        Try
            computer.Open()
        Catch ex As Exception
            Exit Sub
        End Try
    End Sub
    Public Sub Dispose() Implements IDisposable.Dispose
        GC.SuppressFinalize(Me)
    End Sub

    Public Shared Property 处理器逻辑核心占用表 As New Dictionary(Of String, Integer)
    Public Shared Property 内存信息 As New 内存信息结构
    Public Class 内存信息结构
        Public Property 物理内存已用MB As Single
        Public Property 物理内存可用MB As Single
        Public Property 物理内存总大小MB As Single
        Public Property 物理内存使用率 As Integer
        Public Property 虚拟内存已用MB As Single
        Public Property 虚拟内存可用MB As Single
        Public Property 虚拟内存总大小MB As Single
        Public Property 虚拟内存使用率 As Integer
    End Class
    Public Shared Property 显卡信息表 As New Dictionary(Of String, 显卡信息单卡结构)
    Public Class 显卡信息单卡结构
        Public Property 解码核心 As Integer
        Public Property 编码核心 As Integer
        Public Property PCIE带宽 As Integer
        Public Property 显存 As Integer
        Public Property _3D As Integer
        Public Property Copy As Integer
        Public Property Core As Integer
        Public Property 核心功耗 As Integer
        Public Property 整卡功耗 As Integer
        Public Property 其他 As New Dictionary(Of String, Integer)
    End Class

    Public Sub Update()
        Try
            computer.Accept(Me)
        Catch ex As Exception
            Exit Sub
        End Try
    End Sub

    Public Sub VisitComputer(computer As IComputer) Implements IVisitor.VisitComputer
        Try
            For Each hardware In computer.Hardware
                hardware.Accept(Me)
            Next
        Catch ex As Exception
            Exit Sub
        End Try
    End Sub

    Public Sub VisitHardware(hardware As IHardware) Implements IVisitor.VisitHardware
        Try
            hardware.Update()
        Catch ex As Exception
            Exit Sub
        End Try

        Try


            Select Case hardware.HardwareType
                Case HardwareType.Cpu
                    Dim cpuUsages As New Dictionary(Of String, Integer)
                    For Each sensor In hardware.Sensors
                        Select Case sensor.SensorType
                            Case SensorType.Load
                                Select Case sensor.Name
                                    Case "CPU Total"
                                    Case Else
                                        If sensor.Name.Contains("CPU Core #") Then
                                            cpuUsages(sensor.Name.Replace("CPU Core ", "")) = 转换最大值100的百分数(sensor.Value.GetValueOrDefault())
                                        End If
                                End Select
                        End Select
                    Next
                    If cpuUsages.Count > 0 Then 处理器逻辑核心占用表 = cpuUsages

                Case HardwareType.GpuNvidia, HardwareType.GpuAmd, HardwareType.GpuIntel
                    Dim gpuInfo As New 显卡信息单卡结构
                    For Each sensor In hardware.Sensors
                        Select Case sensor.SensorType
                            Case SensorType.Load
                                Select Case True
                                    Case sensor.Name.Contains("Video Decode")
                                        gpuInfo.解码核心 = 转换最大值100的百分数(sensor.Value.GetValueOrDefault)
                                    Case sensor.Name.Contains("Video Encode")
                                        gpuInfo.编码核心 = 转换最大值100的百分数(sensor.Value.GetValueOrDefault)
                                    Case sensor.Name.EndsWith("Bus")
                                        gpuInfo.PCIE带宽 = 转换最大值100的百分数(sensor.Value.GetValueOrDefault)
                                    Case sensor.Name.EndsWith("Memory")
                                        gpuInfo.显存 = 转换最大值100的百分数(sensor.Value.GetValueOrDefault)
                                    Case sensor.Name.EndsWith("3D")
                                        gpuInfo._3D = 转换最大值100的百分数(sensor.Value.GetValueOrDefault)
                                    Case sensor.Name.EndsWith("Copy")
                                        gpuInfo.Copy = 转换最大值100的百分数(sensor.Value.GetValueOrDefault)
                                    Case sensor.Name.EndsWith("Core")
                                        gpuInfo.Core = 转换最大值100的百分数(sensor.Value.GetValueOrDefault)
                                    Case sensor.Name = "GPU Power"
                                        gpuInfo.核心功耗 = 转换最大值100的百分数(sensor.Value.GetValueOrDefault)
                                    Case sensor.Name = "GPU Board Power"
                                        gpuInfo.整卡功耗 = 转换最大值100的百分数(sensor.Value.GetValueOrDefault)
                                    Case Else
                                        gpuInfo.其他(sensor.Name) = sensor.Value.GetValueOrDefault()
                                End Select
                        End Select
                    Next
                    显卡信息表(hardware.Name) = gpuInfo

                Case HardwareType.Memory
                    'For Each sensor In hardware.Sensors
                    '    Debug.WriteLine($"Type: {sensor.SensorType}, Name: {sensor.Name}, Value: {sensor.Value}")
                    'Next
                    Dim memInfo As New 内存信息结构
                    For Each sensor In hardware.Sensors
                        Select Case sensor.SensorType
                            Case SensorType.Data
                                Select Case sensor.Name
                                    Case "Memory Used"
                                        memInfo.物理内存已用MB = sensor.Value.GetValueOrDefault() * 1024
                                    Case "Memory Available"
                                        memInfo.物理内存可用MB = sensor.Value.GetValueOrDefault() * 1024
                                    Case "Virtual Memory Used"
                                        memInfo.虚拟内存已用MB = sensor.Value.GetValueOrDefault() * 1024
                                    Case "Virtual Memory Available"
                                        memInfo.虚拟内存可用MB = sensor.Value.GetValueOrDefault() * 1024
                                    Case Else
                                        Debug.WriteLine(sensor.Name)
                                End Select
                            Case SensorType.Load
                                Select Case sensor.Name
                                    Case "Memory"
                                        memInfo.物理内存使用率 = 转换最大值100的百分数(sensor.Value.GetValueOrDefault())
                                    Case "Virtual Memory"
                                        memInfo.虚拟内存使用率 = 转换最大值100的百分数(sensor.Value.GetValueOrDefault())
                                    Case Else
                                        Debug.WriteLine(sensor.Name)
                                End Select
                        End Select
                    Next
                    If memInfo.物理内存已用MB > 0 AndAlso memInfo.物理内存可用MB > 0 Then
                        memInfo.物理内存总大小MB = memInfo.物理内存已用MB + memInfo.物理内存可用MB
                    ElseIf memInfo.物理内存已用MB > 0 AndAlso memInfo.物理内存使用率 > 0 Then
                        memInfo.物理内存总大小MB = (memInfo.物理内存已用MB * 100) / memInfo.物理内存使用率
                    End If
                    If memInfo.虚拟内存已用MB > 0 AndAlso memInfo.虚拟内存可用MB > 0 Then
                        memInfo.虚拟内存总大小MB = memInfo.虚拟内存已用MB + memInfo.虚拟内存可用MB
                    ElseIf memInfo.虚拟内存已用MB > 0 AndAlso memInfo.虚拟内存使用率 > 0 Then
                        memInfo.虚拟内存总大小MB = (memInfo.虚拟内存已用MB * 100) / memInfo.虚拟内存使用率
                    End If
                    内存信息 = memInfo

            End Select
        Catch ex As Exception
            Exit Sub
        End Try

    End Sub

    Public Shared Function 转换最大值1的百分数(s As Single) As Integer
        Dim a = s * 100
        Return Integer.Parse(a.ToString("0"))
    End Function

    Public Shared Function 转换最大值100的百分数(s As Single) As Integer
        Return Integer.Parse(s.ToString("0"))
    End Function

    Public Shared Sub 刷新到界面上()
        Form1.性能统计对象.Update()

        Dim cpus = 处理器逻辑核心占用表.Keys.ToList
        For i = 0 To 处理器逻辑核心占用表.Count - 1
            If i >= Form1.性能监控页面.FlowLayoutPanel1.Controls.Count Then
                Dim cpuTile As New Label With {
                    .AutoSize = False,
                    .Margin = New Padding(0, 0, 2 * Form1.DPI, 2 * Form1.DPI),
                    .Text = 处理器逻辑核心占用表(cpus(i)),
                    .TextAlign = ContentAlignment.MiddleCenter,
                    .Size = New Size(40 * Form1.DPI, 40 * Form1.DPI),
                    .Font = New Font(用户设置.实例对象.字体, 10),
                    .BackColor = 设置CPU占用的颜色(处理器逻辑核心占用表(cpus(i)))
                }

                If 处理器逻辑核心占用表(cpus(i)) >= 100 Then
                    cpuTile.ForeColor = Color.IndianRed
                Else
                    cpuTile.ForeColor = Color.Silver
                End If
                CPU浮动提示器.SetToolTip(cpuTile, $"逻辑核心 {cpus(i)}")
                Form1.性能监控页面.FlowLayoutPanel1.Controls.Add(cpuTile)
            Else
                Dim cpuTile As Label = CType(Form1.性能监控页面.FlowLayoutPanel1.Controls(i), Label)
                cpuTile.Text = 处理器逻辑核心占用表(cpus(i))
                cpuTile.BackColor = 设置CPU占用的颜色(处理器逻辑核心占用表(cpus(i)))
                If 处理器逻辑核心占用表(cpus(i)) >= 100 Then
                    cpuTile.ForeColor = Color.IndianRed
                Else
                    cpuTile.ForeColor = Color.Silver
                End If
            End If
        Next

        Form1.性能监控页面.UiRoundProcess物理内存.Value = 内存信息.物理内存使用率
        Form1.性能监控页面.UiRoundProcess物理内存.ProcessColor = 设置GPU占用的颜色(内存信息.物理内存使用率)
        Form1.性能监控页面.Label20.Text = $"物理已用 {Format(内存信息.物理内存已用MB / 1024, "0.0")}G"
        Form1.性能监控页面.UiRoundProcess虚拟内存.Value = 内存信息.虚拟内存使用率
        Form1.性能监控页面.UiRoundProcess虚拟内存.ProcessColor = 设置GPU占用的颜色(内存信息.虚拟内存使用率)
        Form1.性能监控页面.Label22.Text = $"虚拟已用 {Format(内存信息.虚拟内存已用MB / 1024, "0.0")}G"

        If 显卡信息表.Count = 0 Then Exit Sub
        If Not 显卡信息表.ContainsKey(Form1.性能监控页面.LinkLabel1.Text) Then Form1.性能监控页面.LinkLabel1.Text = 显卡信息表.Keys(0)

        Form1.性能监控页面.UiRoundProcess解码核心.Value = 显卡信息表(Form1.性能监控页面.LinkLabel1.Text).解码核心
        Form1.性能监控页面.UiRoundProcess解码核心.ProcessColor = 设置GPU占用的颜色(显卡信息表(Form1.性能监控页面.LinkLabel1.Text).解码核心)
        Form1.性能监控页面.UiRoundProcess编码核心.Value = 显卡信息表(Form1.性能监控页面.LinkLabel1.Text).编码核心
        Form1.性能监控页面.UiRoundProcess编码核心.ProcessColor = 设置GPU占用的颜色(显卡信息表(Form1.性能监控页面.LinkLabel1.Text).编码核心)
        Form1.性能监控页面.UiRoundProcessPCIE带宽.Value = 显卡信息表(Form1.性能监控页面.LinkLabel1.Text).PCIE带宽
        Form1.性能监控页面.UiRoundProcessPCIE带宽.ProcessColor = 设置GPU占用的颜色(显卡信息表(Form1.性能监控页面.LinkLabel1.Text).PCIE带宽)
        Form1.性能监控页面.UiRoundProcess显存.Value = 显卡信息表(Form1.性能监控页面.LinkLabel1.Text).显存
        Form1.性能监控页面.UiRoundProcess显存.ProcessColor = 设置GPU占用的颜色(显卡信息表(Form1.性能监控页面.LinkLabel1.Text).显存)
        Form1.性能监控页面.UiRoundProcess3D.Value = 显卡信息表(Form1.性能监控页面.LinkLabel1.Text)._3D
        Form1.性能监控页面.UiRoundProcess3D.ProcessColor = 设置GPU占用的颜色(显卡信息表(Form1.性能监控页面.LinkLabel1.Text)._3D)
        Form1.性能监控页面.UiRoundProcessCopy.Value = 显卡信息表(Form1.性能监控页面.LinkLabel1.Text).Copy
        Form1.性能监控页面.UiRoundProcessCopy.ProcessColor = 设置GPU占用的颜色(显卡信息表(Form1.性能监控页面.LinkLabel1.Text).Copy)
        Form1.性能监控页面.UiRoundProcessCore.Value = 显卡信息表(Form1.性能监控页面.LinkLabel1.Text).Core
        Form1.性能监控页面.UiRoundProcessCore.ProcessColor = 设置GPU占用的颜色(显卡信息表(Form1.性能监控页面.LinkLabel1.Text).Core)
        Form1.性能监控页面.UiRoundProcess核心功耗.Value = 显卡信息表(Form1.性能监控页面.LinkLabel1.Text).核心功耗
        Form1.性能监控页面.UiRoundProcess核心功耗.ProcessColor = 设置GPU占用的颜色(显卡信息表(Form1.性能监控页面.LinkLabel1.Text).核心功耗)
        Form1.性能监控页面.UiRoundProcess整卡功耗.Value = 显卡信息表(Form1.性能监控页面.LinkLabel1.Text).整卡功耗
        Form1.性能监控页面.UiRoundProcess整卡功耗.ProcessColor = 设置GPU占用的颜色(显卡信息表(Form1.性能监控页面.LinkLabel1.Text).整卡功耗)

        Dim 其他信息文本 As New List(Of String)
        For Each kvp In 显卡信息表(Form1.性能监控页面.LinkLabel1.Text).其他
            其他信息文本.Add($"{kvp.Value}% {kvp.Key}")
        Next
        Form1.性能监控页面.Label7.Text = String.Join(vbCrLf, 其他信息文本)
    End Sub

    Public Shared ReadOnly CPU浮动提示器 As New ToolTip With {.Active = True, .IsBalloon = False, .UseAnimation = True}

    Public Shared Function 设置CPU占用的颜色(占用率数字 As Integer) As Color
        Select Case 占用率数字
            Case 0
                Return ColorTranslator.FromWin32(RGB(48, 48, 48))
            Case < 50
                Return Color.FromArgb(100, Color.Green)
            Case < 80
                Return Color.FromArgb(80, Color.CornflowerBlue)
            Case < 90
                Return Color.FromArgb(128, Color.DarkGoldenrod)
            Case Else
                Return Color.FromArgb(128, Color.OrangeRed)
        End Select
    End Function

    Public Shared Function 设置GPU占用的颜色(占用率数字 As Integer) As Color
        Select Case 占用率数字
            Case < 50
                Return Color.FromArgb(150, Color.LimeGreen)
            Case < 80
                Return Color.FromArgb(150, Color.DeepSkyBlue)
            Case < 90
                Return Color.FromArgb(150, Color.Orange)
            Case Else
                Return Color.FromArgb(150, Color.OrangeRed)
        End Select
    End Function

    Public Sub VisitSensor(sensor As ISensor) Implements IVisitor.VisitSensor

    End Sub

    Public Sub VisitParameter(parameter As IParameter) Implements IVisitor.VisitParameter

    End Sub


    '    Public Shared Sub 绑定性能统计处理器列表视图事件()
    '        Form1.ListView3.DoubleBuffer
    '        AddHandler Form1.ListView3.DrawSubItem, Sub(sender, e) 绘制性能统计处理器子项(sender, e)
    '        AddHandler Form1.ListView3.SelectedIndexChanged, Sub(sender, e) sender.Invalidate(sender.ClientRectangle)
    '        AddHandler Form1.ListView3.ItemMouseHover, Sub(sender, e) sender.Invalidate(sender.ClientRectangle)
    '    End Sub
    '    Public Shared Sub 绘制性能统计处理器子项(哪个列表视图控件 As ListView, e As DrawListViewSubItemEventArgs)
    '        Try
    '            Form1.重新创建句柄()
    '            If Not e.Bounds.IntersectsWith(哪个列表视图控件.ClientRectangle) OrElse e.Bounds.Width = 0 Then Exit Sub
    '            Dim 项背景色 As Color = If(哪个列表视图控件.SelectedIndices.Contains(e.ItemIndex), 暗黑列表视图自绘制.项被选中时的背景颜色, 哪个列表视图控件.BackColor)
    '            Dim 文本高度修正 As Integer = (e.Bounds.Height - TextRenderer.MeasureText(e.SubItem.Text, e.SubItem.Font).Height) \ 2
    '            Dim 文本绘制区 As New Rectangle(e.Bounds.X + 5 * Form1.DPI, e.Bounds.Y + 文本高度修正, e.Bounds.Width, e.Bounds.Height)
    '            Dim 文字显示所需尺寸 As Size = TextRenderer.MeasureText(e.SubItem.Text, e.SubItem.Font)
    '            Dim 实际要绘制的文本 As String = e.SubItem.Text
    '            If 文字显示所需尺寸.Width > (e.Bounds.Width - 3 * Form1.DPI) Then
    '                Dim 点号所占用的宽度 As Integer = TextRenderer.MeasureText("...", e.SubItem.Font).Width
    '                Dim 实际文本可用宽度 As Integer = e.Bounds.Width - 点号所占用的宽度
    '                While TextRenderer.MeasureText(实际要绘制的文本, e.SubItem.Font).Width > 实际文本可用宽度 AndAlso 实际要绘制的文本.Length > 0
    '                    实际要绘制的文本 = 实际要绘制的文本.Substring(0, 实际要绘制的文本.Length - 1)
    '                End While
    '                实际要绘制的文本 &= "..."
    '            End If
    '            Using brush As New SolidBrush(项背景色)
    '                e.Graphics.FillRectangle(brush, e.Bounds)
    '            End Using

    '            Dim 进度文本 As String = ""
    '            If Not Form1.性能统计对象.处理器信息.TryGetValue(e.Item.Text, 进度文本) Then GoTo jx1
    '            Dim 进度值 As Double = 0
    '            If 进度文本.EndsWith("%"c) Then
    '                Dim unused = Double.TryParse(进度文本.AsSpan(0, 进度文本.Length - 1), 进度值)
    '                进度值 = Math.Max(0, Math.Min(100, 进度值))
    '            Else
    '                GoTo jx1
    '            End If
    '            If 进度值 <= 0 Then GoTo jx1
    '            Dim 边距 As Integer = 3 * Form1.DPI
    '            Dim 高度 As Integer = Math.Max(8 * Form1.DPI, e.Bounds.Height - 6 * Form1.DPI)
    '            Dim 区域 As New Rectangle(e.Bounds.X + 边距, e.Bounds.Y + (e.Bounds.Height - 高度) \ 2, e.Bounds.Width - 2 * 边距, 高度)
    '            Dim 进度条颜色 As Color
    '            Select Case 进度值
    '                Case < 50 : 进度条颜色 = Color.FromArgb(100, Color.Green)
    '                Case < 80 : 进度条颜色 = Color.FromArgb(80, Color.CornflowerBlue)
    '                Case < 90 : 进度条颜色 = Color.FromArgb(128, Color.DarkGoldenrod)
    '                Case Else : 进度条颜色 = Color.FromArgb(128, Color.OrangeRed)
    '            End Select
    '            Using 填充画刷 As New SolidBrush(进度条颜色)
    '                e.Graphics.FillRectangle(填充画刷, New Rectangle(区域.X, 区域.Y, 区域.Width * (进度值 / 100), 区域.Height))
    '            End Using
    'jx1:
    '            TextRenderer.DrawText(e.Graphics, 实际要绘制的文本.Replace("&", "&&"), e.SubItem.Font, 文本绘制区, e.Item.ForeColor, Color.Transparent, TextFormatFlags.Default)
    '        Catch ex As Exception
    '        End Try
    '    End Sub
    '    Public Shared Sub 绑定性能统计显卡列表视图事件()
    '        Form1.ListView4.DoubleBuffer
    '        AddHandler Form1.ListView4.DrawSubItem, Sub(sender, e) 绘制性能统计显卡子项(sender, e)
    '        AddHandler Form1.ListView4.SelectedIndexChanged, Sub(sender, e) sender.Invalidate(sender.ClientRectangle)
    '        AddHandler Form1.ListView4.ItemMouseHover, Sub(sender, e) sender.Invalidate(sender.ClientRectangle)
    '    End Sub
    '    Public Shared Sub 绘制性能统计显卡子项(哪个列表视图控件 As ListView, e As DrawListViewSubItemEventArgs)
    '        Try
    '            Form1.重新创建句柄()
    '            If Not e.Bounds.IntersectsWith(哪个列表视图控件.ClientRectangle) OrElse e.Bounds.Width = 0 Then Exit Sub
    '            Dim 项背景色 As Color = If(哪个列表视图控件.SelectedIndices.Contains(e.ItemIndex), 暗黑列表视图自绘制.项被选中时的背景颜色, 哪个列表视图控件.BackColor)
    '            Dim 文本高度修正 As Integer = (e.Bounds.Height - TextRenderer.MeasureText(e.SubItem.Text, e.SubItem.Font).Height) \ 2
    '            Dim 文本绘制区 As New Rectangle(e.Bounds.X + 5 * Form1.DPI, e.Bounds.Y + 文本高度修正, e.Bounds.Width, e.Bounds.Height)
    '            Dim 文字显示所需尺寸 As Size = TextRenderer.MeasureText(e.SubItem.Text, e.SubItem.Font)
    '            Dim 实际要绘制的文本 As String = e.SubItem.Text
    '            If 文字显示所需尺寸.Width > (e.Bounds.Width - 3 * Form1.DPI) Then
    '                Dim 点号所占用的宽度 As Integer = TextRenderer.MeasureText("...", e.SubItem.Font).Width
    '                Dim 实际文本可用宽度 As Integer = e.Bounds.Width - 点号所占用的宽度
    '                While TextRenderer.MeasureText(实际要绘制的文本, e.SubItem.Font).Width > 实际文本可用宽度 AndAlso 实际要绘制的文本.Length > 0
    '                    实际要绘制的文本 = 实际要绘制的文本.Substring(0, 实际要绘制的文本.Length - 1)
    '                End While
    '                实际要绘制的文本 &= "..."
    '            End If
    '            Using brush As New SolidBrush(项背景色)
    '                e.Graphics.FillRectangle(brush, e.Bounds)
    '            End Using

    '            Dim 进度文本 As String = Form1.性能统计对象.显卡信息(e.Item.Text)
    '            Dim 进度值 As Double = 0
    '            If 进度文本.EndsWith("%"c) Then
    '                Dim unused = Double.TryParse(进度文本.AsSpan(0, 进度文本.Length - 1), 进度值)
    '                进度值 = Math.Max(0, Math.Min(100, 进度值))
    '            Else
    '                GoTo jx1
    '            End If
    '            If 进度值 <= 0 Then GoTo jx1
    '            Dim 边距 As Integer = 3 * Form1.DPI
    '            Dim 高度 As Integer = Math.Max(8 * Form1.DPI, e.Bounds.Height - 6 * Form1.DPI)
    '            Dim 区域 As New Rectangle(e.Bounds.X + 边距, e.Bounds.Y + (e.Bounds.Height - 高度) \ 2, e.Bounds.Width - 2 * 边距, 高度)
    '            Dim 进度条颜色 As Color
    '            Select Case 进度值
    '                Case < 50 : 进度条颜色 = Color.FromArgb(100, Color.Green)
    '                Case < 80 : 进度条颜色 = Color.FromArgb(80, Color.CornflowerBlue)
    '                Case < 90 : 进度条颜色 = Color.FromArgb(128, Color.DarkGoldenrod)
    '                Case Else : 进度条颜色 = Color.FromArgb(128, Color.OrangeRed)
    '            End Select
    '            Using 填充画刷 As New SolidBrush(进度条颜色)
    '                e.Graphics.FillRectangle(填充画刷, New Rectangle(区域.X, 区域.Y, 区域.Width * (进度值 / 100), 区域.Height))
    '            End Using
    'jx1:
    '            TextRenderer.DrawText(e.Graphics, 实际要绘制的文本.Replace("&", "&&"), e.SubItem.Font, 文本绘制区, e.Item.ForeColor, Color.Transparent, TextFormatFlags.Default)
    '        Catch ex As Exception
    '        End Try
    '    End Sub


End Class