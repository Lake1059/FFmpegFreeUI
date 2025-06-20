Imports LibreHardwareMonitor.Hardware
Imports System.ComponentModel

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
                                    'cpuUsages("CPU 总占用") = 转换最大值100的百分数(sensor.Value.GetValueOrDefault())
                                Case Else
                                    If sensor.Name.Contains("CPU Core #") Then cpuUsages("逻辑核心 " & sensor.Name.Replace("CPU Core ", "")) = 转换最大值100的百分数(sensor.Value.GetValueOrDefault())
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
                                Case "GPU Core", "GPU Memory Controller", "GPU Video Engine", "GPU Bus", "GPU Power", "GPU Board Power"
                                Case Else
                                    If sensor.Value.GetValueOrDefault() = 0 Then Continue For
                                    gpuUsages(hardware.Name & " | " & sensor.Name) = 转换最大值100的百分数(sensor.Value.GetValueOrDefault())
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

End Class