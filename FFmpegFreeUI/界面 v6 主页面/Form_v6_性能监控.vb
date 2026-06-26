Imports LakeUI

Public Class Form_v6_性能监控

    Private LHM监控窗体 As Form_v6_性能监控_LHM

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
        If ModernComboBox2.SelectedIndex <> 0 Then Exit Sub
        Dim gpus As New List(Of GpuMonitor.GpuInfo)
        Await Task.Run(Sub() gpus = GpuMonitor.Sample().ToList)
        If gpus.Count = 0 Then Exit Sub
        If ModernComboBox3.Items.Count <> gpus.Count Then
            For i = 0 To gpus.Count - 1
                ModernComboBox3.Items.Add(gpus(i).Name)
            Next
            ModernComboBox3.SelectedIndex = 0
        End If
        Dim gi = ModernComboBox3.SelectedIndex
        RoundDashBoard1.Value = gpus(gi).VideoDecoderUsage * 100
        RoundDashBoard2.Value = gpus(gi).VideoEncoderUsage * 100
        RoundDashBoard3.Value = gpus(gi).DedicatedMemoryUsedBytes / gpus(gi).DedicatedVideoMemoryBytes * 100
        HtmlColorLabel3.Text = $"显存 {格式化字节(gpus(gi).DedicatedMemoryUsedBytes)}"
        If gpus(gi).PowerLimitWatts.HasValue Then
            RoundDashBoard4.Maximum = gpus(gi).PowerLimitWatts
        Else
            RoundDashBoard4.Maximum = 1000
        End If
        If gpus(gi).PowerWatts.HasValue Then
            RoundDashBoard4.Value = gpus(gi).PowerWatts
        Else
            RoundDashBoard4.Value = 0
        End If
        If gpus(gi).PowerWatts.HasValue Then
            If gpus(gi).PowerLimitWatts.HasValue Then
                HtmlColorLabel4.Text = $"{gpus(gi).PowerWatts:F1}W / {gpus(gi).PowerLimitWatts}W"
            Else
                HtmlColorLabel4.Text = $"功耗 {gpus(gi).PowerWatts:F1}W"
            End If
        Else
            HtmlColorLabel4.Text = $"未知功耗"
        End If

        EasyStatesPanel1.Items.Clear()

        EasyStatesPanel1.Items.Add($"{gpus(gi).DriverVersion}", "驱动版本")
        EasyStatesPanel1.Items.Add($"{gpus(gi).OverallUsage:P1}", "总占用")
        EasyStatesPanel1.Items.Add($"{格式化频率(gpus(gi).CoreFrequencyHz)}", "核心频率")
        EasyStatesPanel1.Items.Add($"{格式化频率(gpus(gi).MemoryFrequencyHz)}", "显存频率")
        EasyStatesPanel1.Items.Add($"{格式化字节(gpus(gi).SharedMemoryUsedBytes)}", "已用共享显存")

        If gpus(gi).EngineUsages.Count > 0 Then
            Dim gpu_engine As New List(Of KeyValuePair(Of String, Single))
            For Each kv In gpus(gi).EngineUsages
                gpu_engine.Add(New KeyValuePair(Of String, Single)(kv.Key, kv.Value))
            Next

            For i = 0 To gpu_engine.Count - 1
                EasyStatesPanel1.Items.Add($"{gpu_engine(i).Value:P1}", $"引擎 {gpu_engine(i).Key}")
            Next
        End If

    End Sub

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
