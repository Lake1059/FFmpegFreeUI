Imports LakeUI

Public Class Form_v6_性能监控

    Private LHM监控窗体 As Form_v6_性能监控_LHM
    Private ReadOnly GPU显示名到键 As New Dictionary(Of String, String)(StringComparer.Ordinal)
    Private GPU采样中 As Boolean = False

    Private Sub Form_v6_性能监控_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ModernComboBox2.SelectedIndex = 0
    End Sub

    Public Sub 开始()
        Me.CpuMonitor1.Running = True
        Me.RamMonitor1.Running = True
        Me.Timer1.Enabled = ModernComboBox2.SelectedIndex = 0
        If ModernComboBox2.SelectedIndex = 1 Then LHM监控窗体?.StartMonitoring()
    End Sub

    Public Sub 停止()
        Me.CpuMonitor1.Running = False
        Me.RamMonitor1.Running = False
        Me.Timer1.Enabled = False
        LHM监控窗体?.StopMonitoring()
    End Sub

    Private Sub CpuMonitor1_SamplerReady(sender As Object, e As EventArgs) Handles CpuMonitor1.SamplerReady
        ModernComboBox1.Items.Clear()
        ModernComboBox1.Items.Add("全部核心")
        For i = 0 To CpuMonitor1.ProcessorGroupCount - 1
            ModernComboBox1.Items.Add(CpuMonitor1.GetProcessorGroupName(i))
        Next
        ModernComboBox1.SelectedIndex = 1
    End Sub

    Private Sub ModernComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ModernComboBox1.SelectedIndexChanged
        CpuMonitor1.DisplayedGroup = ModernComboBox1.SelectedIndex - 1
    End Sub

    Private Sub Form_v6_性能监控_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
        Panel2.Width = Me.Width * 0.4
        RamMonitor1.Height = 150 * (DeviceDpi / 96)
        ModernPanel4.Width = (Panel1.Width - JustEmptyControl2.Width * 3) / 4
        ModernPanel5.Width = ModernPanel4.Width
        ModernPanel6.Width = ModernPanel4.Width
    End Sub

    Private Sub ModernComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ModernComboBox2.SelectedIndexChanged
        EasyStatesPanel1.Items.Clear()
        ModernComboBox3.Items.Clear()
        Select Case ModernComboBox2.SelectedIndex
            Case 0
                ModernPanel内置显卡监控面板.Visible = True
                If LHM监控窗体 IsNot Nothing Then LHM监控窗体.RootPanel.Visible = False
                Timer1.Enabled = True
                LHM监控窗体?.StopMonitoring()
            Case 1
                ModernPanel内置显卡监控面板.Visible = False
                加载LHM组件()
                If LHM监控窗体 IsNot Nothing Then
                    Dim lhmPanel = LHM监控窗体.RootPanel
                    lhmPanel.Visible = True
                    Panel4.Controls.SetChildIndex(lhmPanel, 0)
                    Panel4.Controls.SetChildIndex(Panel3, Panel4.Controls.Count - 1)
                    Timer1.Enabled = False
                    LHM监控窗体.StartMonitoring()
                End If
        End Select
    End Sub

    Private Sub 加载LHM组件()
        If LHM监控窗体 IsNot Nothing Then Exit Sub

        LHM监控窗体 = New Form_v6_性能监控_LHM()
        LHM监控窗体.InitializeLhm(ModernComboBox3, ModernComboBox1)

        Dim lhmPanel = LHM监控窗体.RootPanel
        lhmPanel.Dock = DockStyle.Fill
        lhmPanel.Visible = False
        If lhmPanel.Parent IsNot Nothing Then lhmPanel.Parent.Controls.Remove(lhmPanel)
        Panel4.Controls.Add(lhmPanel)
        Panel4.Controls.SetChildIndex(lhmPanel, 0)
        Panel4.Controls.SetChildIndex(Panel3, Panel4.Controls.Count - 1)
    End Sub

    Private Async Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If ModernComboBox2.SelectedIndex <> 0 OrElse GPU采样中 Then Exit Sub
        GPU采样中 = True
        Dim gpus As New List(Of GpuMonitor.GpuInfo)
        Try
            Await Task.Run(Sub() gpus = GpuMonitor.Sample().OrderBy(Function(x) x.Index).ToList())
            If gpus.Count = 0 Then Exit Sub

            刷新GPU选择列表(gpus)
            Dim gpuKey = 获取选中GPU键()
            Dim gpu = gpus.FirstOrDefault(Function(x) 构建GPU键(x) = gpuKey)
            If gpu Is Nothing Then gpu = gpus(0)

            ' GpuMonitor explicitly reports video utilization as Nullable when a driver has no sensor.
            RoundDashBoard1.Value = gpu.VideoDecoderUsage.GetValueOrDefault() * 100
            RoundDashBoard2.Value = gpu.VideoEncoderUsage.GetValueOrDefault() * 100
            RoundDashBoard3.Value = If(gpu.DedicatedVideoMemoryBytes = 0UL, 0, gpu.DedicatedMemoryUsedBytes / CDbl(gpu.DedicatedVideoMemoryBytes) * 100)
            HtmlColorLabel3.Text = $"显存 {格式化字节(gpu.DedicatedMemoryUsedBytes)}"

            Dim powerLimit = gpu.PowerLimitWatts.GetValueOrDefault()
            If powerLimit <= 0 Then powerLimit = 1000
            Dim power = gpu.PowerWatts.GetValueOrDefault()
            RoundDashBoard4.Maximum = powerLimit
            RoundDashBoard4.Value = power
            If gpu.PowerWatts.HasValue Then
                If gpu.PowerLimitWatts.HasValue AndAlso gpu.PowerLimitWatts.GetValueOrDefault() > 0 Then
                    HtmlColorLabel4.Text = $"{power:F1}W / {gpu.PowerLimitWatts.GetValueOrDefault():F1}W"
                Else
                    HtmlColorLabel4.Text = $"功耗 {power:F1}W"
                End If
            Else
                HtmlColorLabel4.Text = "未知功耗"
            End If

            EasyStatesPanel1.Items.Clear()

            EasyStatesPanel1.Items.Add($"{If(gpu.DriverVersion, "未知")}", "驱动版本")
            EasyStatesPanel1.Items.Add($"{gpu.OverallUsage:P1}", "总占用")
            EasyStatesPanel1.Items.Add($"{格式化频率(gpu.CoreFrequencyHz)}", "核心频率")
            EasyStatesPanel1.Items.Add($"{格式化频率(gpu.MemoryFrequencyHz)}", "显存频率")
            EasyStatesPanel1.Items.Add($"{格式化字节(gpu.SharedMemoryUsedBytes)}", "已用共享显存")

            If gpu.EngineUsages IsNot Nothing AndAlso gpu.EngineUsages.Count > 0 Then
                For Each kv In gpu.EngineUsages.OrderBy(Function(x) x.Key, StringComparer.OrdinalIgnoreCase)
                    EasyStatesPanel1.Items.Add($"{kv.Value:P1}", $"引擎 {kv.Key}")
                Next
            End If
        Catch
            ' A failed driver query should leave the prior sample visible instead of breaking the timer.
        Finally
            GPU采样中 = False
        End Try
    End Sub

    Private Sub 刷新GPU选择列表(gpus As List(Of GpuMonitor.GpuInfo))
        Dim selectedKey = 获取选中GPU键()
        Dim displayItems = 构建GPU显示项(gpus)
        Dim needsRebuild = ModernComboBox3.Items.Count <> displayItems.Count OrElse
            displayItems.Any(Function(x) Not ModernComboBox3.Items.Contains(x.DisplayName))

        GPU显示名到键.Clear()
        For Each item In displayItems
            GPU显示名到键(item.DisplayName) = item.Key
        Next
        If Not needsRebuild Then Return

        ModernComboBox3.Items.Clear()
        For Each item In displayItems
            ModernComboBox3.Items.Add(item.DisplayName)
        Next

        Dim selectedIndex = displayItems.FindIndex(Function(x) x.Key = selectedKey)
        ModernComboBox3.SelectedIndex = If(selectedIndex >= 0, selectedIndex, 0)
    End Sub

    Private Function 获取选中GPU键() As String
        Dim displayName = If(ModernComboBox3.SelectedItem, "").ToString()
        Dim key As String = ""
        Return If(GPU显示名到键.TryGetValue(displayName, key), key, "")
    End Function

    Private Shared Function 构建GPU键(gpu As GpuMonitor.GpuInfo) As String
        Return $"{gpu.LuidHigh:X8}:{gpu.LuidLow:X8}"
    End Function

    Private Shared Function 构建GPU显示项(gpus As IEnumerable(Of GpuMonitor.GpuInfo)) As List(Of (DisplayName As String, Key As String))
        Dim result As New List(Of (DisplayName As String, Key As String))
        For Each group In gpus.GroupBy(Function(x) If(x.Name, ""), StringComparer.OrdinalIgnoreCase)
            Dim ordinal = 0
            For Each gpu In group.OrderBy(Function(x) x.Index)
                ordinal += 1
                Dim name = If(String.IsNullOrWhiteSpace(gpu.Name), $"GPU {gpu.Index}", gpu.Name)
                If group.Count() > 1 Then name &= $" #{ordinal}"
                result.Add((name, 构建GPU键(gpu)))
            Next
        Next
        Return result
    End Function

    Private Shared Function 格式化字节(bytes As ULong) As String
        If bytes >= 1073741824UL Then Return $"{bytes / 1073741824.0:F1} GB"
        If bytes >= 1048576UL Then Return $"{bytes / 1048576.0:F0} MB"
        If bytes >= 1024UL Then Return $"{bytes / 1024.0:F0} KB"
        Return $"{bytes} B"
    End Function

    Private Shared Function 格式化频率(hz As ULong?) As String
        If Not hz.HasValue Then Return "N/A"
        If hz.Value >= 1000000000UL Then Return $"{hz.Value / 1000000000.0:F2} GHz"
        If hz.Value >= 1000000UL Then Return $"{hz.Value / 1000000.0:F0} MHz"
        Return $"{hz.Value} Hz"
    End Function

End Class
