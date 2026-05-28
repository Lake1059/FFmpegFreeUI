Imports LakeUI

Public Class Form_v6_性能监控
    Private Sub Form_v6_性能监控_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ModernComboBox2.SelectedIndex = 0
    End Sub

    Public Sub 开始()
        Me.CpuMonitor1.Running = True
        Me.RamMonitor1.Running = True
        Select Case ModernComboBox2.SelectedIndex
            Case 0 : Timer1.Enabled = True
        End Select
    End Sub

    Public Sub 停止()
        Me.CpuMonitor1.Running = False
        Me.RamMonitor1.Running = False
        Select Case ModernComboBox2.SelectedIndex
            Case 0 : Timer1.Enabled = False
        End Select
    End Sub

    Private Sub CpuMonitor1_SamplerReady(sender As Object, e As EventArgs) Handles CpuMonitor1.SamplerReady
        ModernComboBox1.Items.Clear()
        ModernComboBox1.Items.Add("全部核心")
        For i = 0 To CpuMonitor1.ProcessorGroupCount - 1
            ModernComboBox1.Items.Add(CpuMonitor1.GetProcessorGroupName(i))
        Next
        ModernComboBox1.SelectedIndex = 0
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
        ModernPanel8.Controls.Clear()
        ModernComboBox3.Items.Clear()
        Select Case ModernComboBox2.SelectedIndex
            Case 0
                ModernPanel内置显卡监控面板.Visible = True
            Case 1
                ModernPanel内置显卡监控面板.Visible = False
        End Select
    End Sub

    Private Async Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
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
        Try
            RoundDashBoard4.Maximum = gpus(gi).PowerLimitWatts
        Catch ex As Exception
            RoundDashBoard4.Maximum = 1000
        End Try
        RoundDashBoard4.Value = gpus(gi).PowerWatts
        HtmlColorLabel4.Text = $"功耗 {gpus(gi).PowerWatts:F1} W"

        If ModernPanel8.Controls.Count < 1 Then 添加显卡数据小卡片("驱动版本")
        ModernPanel8.Controls(0).Text = $"{gpus(gi).DriverVersion}"
        If ModernPanel8.Controls.Count < 2 Then 添加显卡数据小卡片("总占用")
        ModernPanel8.Controls(1).Text = $"{gpus(gi).OverallUsage:P1}"
        If ModernPanel8.Controls.Count < 3 Then 添加显卡数据小卡片("核心频率")
        ModernPanel8.Controls(2).Text = $"{格式化频率(gpus(gi).CoreFrequencyHz)}"
        If ModernPanel8.Controls.Count < 4 Then 添加显卡数据小卡片("显存频率")
        ModernPanel8.Controls(3).Text = $"{格式化频率(gpus(gi).MemoryFrequencyHz)}"
        If ModernPanel8.Controls.Count < 5 Then 添加显卡数据小卡片("已用共享显存")
        ModernPanel8.Controls(4).Text = $"{格式化字节(gpus(gi).SharedMemoryUsedBytes)}"

        If gpus(gi).EngineUsages.Count > 0 Then
            Dim gpu_engine As New List(Of KeyValuePair(Of String, Single))
            For Each kv In gpus(gi).EngineUsages
                gpu_engine.Add(New KeyValuePair(Of String, Single)(kv.Key, kv.Value))
            Next

            For i = 0 To gpu_engine.Count - 1
                If ModernPanel8.Controls.Count < 5 + i + 1 Then 添加显卡数据小卡片($"引擎 {gpu_engine(i).Key}")
                ModernPanel8.Controls(5 + i).Text = $"{gpu_engine(i).Value:P1}"
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

    Sub 添加显卡数据小卡片(子文本 As String)
        Dim a As New ModernButton With {
            .BackColor1 = Color.FromArgb(120, 0, 0, 0),
            .BackColor = Color.Transparent,
            .BorderRadius = 10,
            .BorderSize = 0,
            .ForeColor = Color.MediumPurple,
            .MainSubTextSpacing = 3,
            .TextAlign = ModernButton.TextAlignEnum.Left,
            .Size = New Size(ModernPanel4.Width * 1.3, 55 * (DeviceDpi / 96)),
            .Margin = New Padding(0, 0, 10 * (DeviceDpi / 96), 10 * (DeviceDpi / 96)),
            .SubText = 子文本
        }
        ModernPanel8.Controls.Add(a)
        'a.BringToFront()
    End Sub




End Class