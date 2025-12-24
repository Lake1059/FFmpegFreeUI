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
        Public Property 显存GB As Single
        Public Property _3D As Integer
        Public Property Copy As Integer
        Public Property Core As Integer
        Public Property 功耗P As Integer
        Public Property 核心温度 As Single
        Public Property 显存温度 As Single
        Public Property 风扇转速 As Single
        Public Property 功耗W As Single
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
                                    Case sensor.Name.Contains("GPU Board Power")
                                        gpuInfo.功耗P = 转换最大值100的百分数(sensor.Value.GetValueOrDefault)
                                    Case Else
                                        gpuInfo.其他(sensor.Name) = sensor.Value.GetValueOrDefault()
                                End Select

                            Case SensorType.SmallData
                                Select Case True
                                    Case sensor.Name.Contains("Dedicated Memory Used")
                                        gpuInfo.显存GB = Format(sensor.Value.GetValueOrDefault / 1024, "0.0")
                                End Select

                            Case SensorType.Temperature
                                Select Case True
                                    Case sensor.Name.Contains("Core")
                                        gpuInfo.核心温度 = sensor.Value.GetValueOrDefault
                                End Select

                            Case SensorType.Fan
                                Select Case True
                                    Case sensor.Name.Contains("Fan")
                                        gpuInfo.风扇转速 = sensor.Value.GetValueOrDefault
                                End Select

                            Case SensorType.Power
                                Select Case True
                                    Case sensor.Name.Contains("Package")
                                        gpuInfo.功耗W = sensor.Value.GetValueOrDefault
                                End Select

                        End Select
                    Next
                    显卡信息表(hardware.Name) = gpuInfo

                Case HardwareType.Memory
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
        Form1.性能监控页面.Label22.Text = $"已提交 {Format(内存信息.虚拟内存已用MB / 1024, "0.0")}G"

        If 显卡信息表.Count = 0 Then Exit Sub
        If Not 显卡信息表.ContainsKey(Form1.性能监控页面.LinkLabel1.Text) Then Form1.性能监控页面.LinkLabel1.Text = 显卡信息表.Keys(0)

        Form1.性能监控页面.UiRoundProcess解码核心.Value = 显卡信息表(Form1.性能监控页面.LinkLabel1.Text).解码核心
        Form1.性能监控页面.UiRoundProcess解码核心.ProcessColor = 设置GPU占用的颜色(显卡信息表(Form1.性能监控页面.LinkLabel1.Text).解码核心)
        Form1.性能监控页面.UiRoundProcess编码核心.Value = 显卡信息表(Form1.性能监控页面.LinkLabel1.Text).编码核心
        Form1.性能监控页面.UiRoundProcess编码核心.ProcessColor = 设置GPU占用的颜色(显卡信息表(Form1.性能监控页面.LinkLabel1.Text).编码核心)
        Form1.性能监控页面.UiRoundProcessPCIE带宽.Value = 显卡信息表(Form1.性能监控页面.LinkLabel1.Text).PCIE带宽
        Form1.性能监控页面.UiRoundProcessPCIE带宽.ProcessColor = 设置GPU占用的颜色(显卡信息表(Form1.性能监控页面.LinkLabel1.Text).PCIE带宽)
        Form1.性能监控页面.Label8.Text = "显存 " & 显卡信息表(Form1.性能监控页面.LinkLabel1.Text).显存GB & "G"
        Form1.性能监控页面.UiRoundProcess显存.Value = 显卡信息表(Form1.性能监控页面.LinkLabel1.Text).显存
        Form1.性能监控页面.UiRoundProcess显存.ProcessColor = 设置GPU占用的颜色(显卡信息表(Form1.性能监控页面.LinkLabel1.Text).显存)
        Form1.性能监控页面.UiRoundProcess3D.Value = 显卡信息表(Form1.性能监控页面.LinkLabel1.Text)._3D
        Form1.性能监控页面.UiRoundProcess3D.ProcessColor = 设置GPU占用的颜色(显卡信息表(Form1.性能监控页面.LinkLabel1.Text)._3D)
        Form1.性能监控页面.UiRoundProcessCopy.Value = 显卡信息表(Form1.性能监控页面.LinkLabel1.Text).Copy
        Form1.性能监控页面.UiRoundProcessCopy.ProcessColor = 设置GPU占用的颜色(显卡信息表(Form1.性能监控页面.LinkLabel1.Text).Copy)
        Form1.性能监控页面.Label12.Text = "温度 " & 显卡信息表(Form1.性能监控页面.LinkLabel1.Text).核心温度 & "°C"
        Form1.性能监控页面.UiRoundProcess温度.Value = 显卡信息表(Form1.性能监控页面.LinkLabel1.Text).核心温度
        Form1.性能监控页面.UiRoundProcess温度.ProcessColor = 设置GPU占用的颜色(Form1.性能监控页面.UiRoundProcess温度.Value)
        Form1.性能监控页面.Label10.Text = 显卡信息表(Form1.性能监控页面.LinkLabel1.Text).风扇转速 & " RPM"
        Form1.性能监控页面.UiRoundProcess风扇转速.Value = 显卡信息表(Form1.性能监控页面.LinkLabel1.Text).风扇转速 / 3200 * 100
        Form1.性能监控页面.UiRoundProcess风扇转速.ProcessColor = 设置GPU占用的颜色(Form1.性能监控页面.UiRoundProcess风扇转速.Value)
        Form1.性能监控页面.Label17.Text = "功耗 " & Format(显卡信息表(Form1.性能监控页面.LinkLabel1.Text).功耗W, "0") & "W"
        Form1.性能监控页面.UiRoundProcess功耗.Value = 显卡信息表(Form1.性能监控页面.LinkLabel1.Text).功耗P
        Form1.性能监控页面.UiRoundProcess功耗.ProcessColor = 设置GPU占用的颜色(Form1.性能监控页面.UiRoundProcess功耗.Value)

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

End Class