Imports LakeUI
Imports LibreHardwareMonitor.Hardware

Public Class Form_v6_性能监控_LHM
    Implements IVisitor

    Private ReadOnly computer As Computer
    Private initialized As Boolean
    Private cpuTemperatureValue As Double
    Private gpuInfoTable As New Dictionary(Of String, GpuInfo)
    Private hostGpuComboBox As ModernComboBox
    Private hostCpuComboBox As ModernComboBox

    Public Sub New()
        InitializeComponent()

        computer = New Computer With {
            .IsCpuEnabled = True,
            .IsGpuEnabled = True,
            .IsMemoryEnabled = False,
            .IsNetworkEnabled = False,
            .IsStorageEnabled = False
        }
    End Sub

    Public ReadOnly Property RootPanel As Control
        Get
            ModernPanel1.Dock = DockStyle.Fill
            Return ModernPanel1
        End Get
    End Property

    Public Sub InitializeLhm(gpuComboBox As ModernComboBox, cpuComboBox As ModernComboBox)
        hostGpuComboBox = gpuComboBox
        hostCpuComboBox = cpuComboBox
        If initialized Then Exit Sub
        initialized = True

        InitializeDashboard()
        Try
            computer.Open()
        Catch ex As Exception
            EasyStatesPanel1.Items.Clear()
            EasyStatesPanel1.Items.Add("无法启动", "LibreHardwareMonitor")
        End Try
    End Sub

    Public Sub StartMonitoring()
        If Not initialized Then Exit Sub
        Timer1.Enabled = True
    End Sub

    Public Sub StopMonitoring()
        Timer1.Enabled = False
        RestoreCpuItemText()
    End Sub

    Private Sub Form_v6_性能监控_LHM_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeDashboard()
    End Sub

    Private Sub InitializeDashboard()
        For Each dash In AllDashboards()
            dash.Maximum = 100
            dash.Value = 0
        Next
        RoundDashBoard8.Maximum = 3200
        EasyStatesPanel1.Items.Clear()
    End Sub

    Private Sub Form_v6_性能监控_LHM_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged, ModernPanel1.SizeChanged
        ModernPanel4.Width = (Panel1.Width - JustEmptyControl2.Width * 3) / 4
        ModernPanel5.Width = ModernPanel4.Width
        ModernPanel6.Width = ModernPanel4.Width
        ModernPanel2.Width = (Panel2.Width - JustEmptyControl1.Width * 4) / 5
        ModernPanel3.Width = ModernPanel2.Width
        ModernPanel8.Width = ModernPanel2.Width
        ModernPanel9.Width = ModernPanel2.Width
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        SampleAndRefresh()
    End Sub

    Private Sub Form_v6_性能监控_LHM_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Timer1.Enabled = False
        computer?.Close()
    End Sub

    Public Sub SampleAndRefresh()
        If Not initialized Then Exit Sub

        gpuInfoTable.Clear()
        Try
            computer.Accept(Me)
        Catch ex As Exception
            Exit Sub
        End Try

        RefreshGpuComboBox()
        RefreshCpuTemperature()
        RefreshGpuInfo()
    End Sub

    Public Sub VisitComputer(computer As IComputer) Implements IVisitor.VisitComputer
        For Each hardware In computer.Hardware
            hardware.Accept(Me)
        Next
    End Sub

    Public Sub VisitHardware(hardware As IHardware) Implements IVisitor.VisitHardware
        Try
            hardware.Update()
            For Each subHardware In hardware.SubHardware
                subHardware.Update()
            Next
        Catch ex As Exception
            Exit Sub
        End Try

        Select Case hardware.HardwareType
            Case HardwareType.Cpu
                ReadCpuSensors(hardware)
            Case HardwareType.GpuNvidia, HardwareType.GpuAmd, HardwareType.GpuIntel
                ReadGpuSensors(hardware)
        End Select
    End Sub

    Private Sub ReadCpuSensors(hardware As IHardware)
        Dim cpuTemperatureSensor = hardware.Sensors.
            Where(Function(sensor) sensor.SensorType = SensorType.Temperature AndAlso IsCpuOverallTemperatureSensor(sensor.Name)).
            OrderBy(Function(sensor) GetCpuOverallTemperaturePriority(sensor.Name)).
            FirstOrDefault()

        If cpuTemperatureSensor IsNot Nothing Then cpuTemperatureValue = cpuTemperatureSensor.Value.GetValueOrDefault()
    End Sub

    Private Shared Function IsCpuOverallTemperatureSensor(sensorName As String) As Boolean
        If String.IsNullOrWhiteSpace(sensorName) Then Return False

        Return sensorName.Equals("CPU Package", StringComparison.OrdinalIgnoreCase) OrElse
            sensorName.Contains("Package", StringComparison.OrdinalIgnoreCase) OrElse
            sensorName.Contains("Tctl", StringComparison.OrdinalIgnoreCase) OrElse
            sensorName.Contains("Tdie", StringComparison.OrdinalIgnoreCase)
    End Function

    Private Shared Function GetCpuOverallTemperaturePriority(sensorName As String) As Integer
        If sensorName.Equals("CPU Package", StringComparison.OrdinalIgnoreCase) Then Return 0
        If sensorName.Contains("Package", StringComparison.OrdinalIgnoreCase) Then Return 1
        If sensorName.Contains("Tctl/Tdie", StringComparison.OrdinalIgnoreCase) Then Return 2
        If sensorName.Contains("Tctl", StringComparison.OrdinalIgnoreCase) Then Return 3
        If sensorName.Contains("Tdie", StringComparison.OrdinalIgnoreCase) Then Return 4
        Return 5
    End Function

    Private Sub ReadGpuSensors(hardware As IHardware)
        Dim gpuInfo As New GpuInfo With {.Name = hardware.Name}

        For Each sensor In hardware.Sensors
            Dim value = sensor.Value.GetValueOrDefault()
            Select Case sensor.SensorType
                Case SensorType.Load
                    Dim loadValue = ToPercent(value)
                    Select Case True
                        Case sensor.Name.Contains("Video Decode")
                            gpuInfo.VideoDecode = loadValue
                        Case sensor.Name.Contains("Video Encode")
                            gpuInfo.VideoEncode = loadValue
                        Case sensor.Name.EndsWith("Bus")
                            gpuInfo.PcieBus = loadValue
                        Case sensor.Name.EndsWith("Memory")
                            gpuInfo.Memory = loadValue
                        Case sensor.Name.EndsWith("3D")
                            gpuInfo.ThreeD = loadValue
                        Case sensor.Name.EndsWith("Copy")
                            gpuInfo.Copy = loadValue
                        Case sensor.Name.EndsWith("Core")
                            gpuInfo.Core = loadValue
                        Case sensor.Name.Contains("GPU Board Power")
                            gpuInfo.PowerPercent = loadValue
                    End Select
                    gpuInfo.Loads(sensor.Name) = loadValue
                Case SensorType.SmallData
                    If sensor.Name.Contains("Dedicated Memory Used") Then gpuInfo.MemoryUsedGb = value / 1024
                Case SensorType.Temperature
                    If sensor.Name.Contains("Core") Then gpuInfo.CoreTemperature = value
                Case SensorType.Fan
                    If sensor.Name.Contains("Fan") Then gpuInfo.FanRpm = value
                Case SensorType.Power
                    If sensor.Name.Contains("Package") OrElse sensor.Name.Contains("Power") Then gpuInfo.PowerWatt = value
            End Select
        Next

        gpuInfoTable(hardware.Name) = gpuInfo
    End Sub

    Private Sub RefreshGpuComboBox()
        If hostGpuComboBox Is Nothing Then Exit Sub

        Dim gpuNames = gpuInfoTable.Keys.ToList()
        If hostGpuComboBox.Items.Count <> gpuNames.Count OrElse gpuNames.Any(Function(gpuName) Not hostGpuComboBox.Items.Contains(gpuName)) Then
            Dim selectedText = If(hostGpuComboBox.SelectedItem?.ToString(), "")
            hostGpuComboBox.Items.Clear()
            For Each gpuName In gpuNames
                hostGpuComboBox.Items.Add(gpuName)
            Next
            If hostGpuComboBox.Items.Count > 0 Then
                Dim oldIndex = hostGpuComboBox.Items.IndexOf(selectedText)
                hostGpuComboBox.SelectedIndex = If(oldIndex >= 0, oldIndex, 0)
            End If
        ElseIf hostGpuComboBox.SelectedIndex < 0 AndAlso hostGpuComboBox.Items.Count > 0 Then
            hostGpuComboBox.SelectedIndex = 0
        End If
    End Sub

    Private Sub RefreshCpuTemperature()
        If hostCpuComboBox Is Nothing OrElse hostCpuComboBox.Items.Count <= 1 Then Exit Sub

        Dim baseCpuItemText = StripCpuTemperature(hostCpuComboBox.Items(1))
        If cpuTemperatureValue > 0 Then
            hostCpuComboBox.Items(1) = $"{baseCpuItemText}  {cpuTemperatureValue:F0}°C"
        ElseIf Not String.IsNullOrEmpty(baseCpuItemText) Then
            hostCpuComboBox.Items(1) = baseCpuItemText
        End If
    End Sub

    Private Sub RestoreCpuItemText()
        If hostCpuComboBox Is Nothing OrElse hostCpuComboBox.Items.Count <= 1 Then Exit Sub
        hostCpuComboBox.Items(1) = StripCpuTemperature(hostCpuComboBox.Items(1))
    End Sub

    Private Sub RefreshGpuInfo()
        If gpuInfoTable.Count = 0 Then
            ClearGpuDashboards()
            Exit Sub
        End If

        Dim gpuName = GetSelectedGpuName()
        If String.IsNullOrEmpty(gpuName) OrElse Not gpuInfoTable.ContainsKey(gpuName) Then
            gpuName = gpuInfoTable.Keys.First()
        End If

        Dim gpuInfo = gpuInfoTable(gpuName)
        SetDashboardValue(RoundDashBoard1, gpuInfo.VideoDecode)
        SetDashboardValue(RoundDashBoard2, gpuInfo.VideoEncode)
        SetDashboardValue(RoundDashBoard3, gpuInfo.PcieBus)
        SetDashboardValue(RoundDashBoard4, gpuInfo.Memory)
        SetDashboardValue(RoundDashBoard5, gpuInfo.ThreeD)
        SetDashboardValue(RoundDashBoard6, gpuInfo.Copy)
        SetDashboardValue(RoundDashBoard7, gpuInfo.CoreTemperature)
        RoundDashBoard8.Maximum = Math.Max(3200, Math.Ceiling(gpuInfo.FanRpm / 100) * 100)
        SetDashboardValue(RoundDashBoard8, gpuInfo.FanRpm)
        SetDashboardValue(RoundDashBoard9, gpuInfo.PowerPercent)

        HtmlColorLabel4.Text = $"显存 {gpuInfo.MemoryUsedGb:F1}G"
        HtmlColorLabel7.Text = $"温度 {gpuInfo.CoreTemperature:F0}°C"
        HtmlColorLabel8.Text = $"{gpuInfo.FanRpm:F0} RPM"
        HtmlColorLabel9.Text = $"功耗 {gpuInfo.PowerWatt:F0}W"

        RefreshGpuEngineInfo(gpuInfo)
    End Sub

    Private Sub ClearGpuDashboards()
        For Each dash In AllDashboards()
            dash.Value = 0
        Next
        EasyStatesPanel1.Items.Clear()
    End Sub

    Private Function GetSelectedGpuName() As String
        If hostGpuComboBox Is Nothing OrElse hostGpuComboBox.SelectedIndex < 0 Then Return ""
        Return hostGpuComboBox.SelectedItem?.ToString()
    End Function

    Private Shared Function StripCpuTemperature(text As String) As String
        If String.IsNullOrEmpty(text) Then Return ""
        Dim temperatureStart = text.LastIndexOf("  ", StringComparison.Ordinal)
        If temperatureStart > 0 AndAlso text.EndsWith("°C") Then Return text.Substring(0, temperatureStart)
        Return text
    End Function

    Private Sub RefreshGpuEngineInfo(gpuInfo As GpuInfo)
        EasyStatesPanel1.Items.BeginUpdate()
        Try
            EasyStatesPanel1.Items.Clear()
            For Each item In gpuInfo.Loads
                EasyStatesPanel1.Items.Add($"{item.Value}%", item.Key)
            Next
        Finally
            EasyStatesPanel1.Items.EndUpdate()
        End Try
    End Sub

    Private Shared Sub SetDashboardValue(dash As RoundDashBoard, value As Double)
        If Double.IsNaN(value) OrElse Double.IsInfinity(value) Then value = 0
        dash.Value = Math.Max(0, Math.Min(dash.Maximum, value))
    End Sub

    Private Function AllDashboards() As IEnumerable(Of RoundDashBoard)
        Return {RoundDashBoard1, RoundDashBoard2, RoundDashBoard3, RoundDashBoard4, RoundDashBoard5, RoundDashBoard6, RoundDashBoard7, RoundDashBoard8, RoundDashBoard9}
    End Function

    Private Shared Function ToPercent(value As Single) As Integer
        Return CInt(Math.Max(0, Math.Min(100, Math.Round(value))))
    End Function

    Public Sub VisitSensor(sensor As ISensor) Implements IVisitor.VisitSensor
    End Sub

    Public Sub VisitParameter(parameter As IParameter) Implements IVisitor.VisitParameter
    End Sub

    Private Class GpuInfo
        Public Property Name As String = ""
        Public Property VideoDecode As Integer
        Public Property VideoEncode As Integer
        Public Property PcieBus As Integer
        Public Property Memory As Integer
        Public Property MemoryUsedGb As Single
        Public Property ThreeD As Integer
        Public Property Copy As Integer
        Public Property Core As Integer
        Public Property PowerPercent As Integer
        Public Property CoreTemperature As Single
        Public Property FanRpm As Single
        Public Property PowerWatt As Single
        Public Property Loads As New Dictionary(Of String, Integer)
    End Class
End Class
