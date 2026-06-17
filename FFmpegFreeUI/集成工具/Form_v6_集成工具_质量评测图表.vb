Imports System.Globalization
Imports LakeUI

Public Class Form_v6_集成工具_质量评测图表

    Private Shared 当前窗体 As Form_v6_集成工具_质量评测图表 = Nothing

    Private 数据提供器 As Func(Of String, Dictionary(Of String, List(Of Double))) = Nothing
    Private 分数提供器 As Func(Of String, Dictionary(Of String, String)) = Nothing

    Public Shared Sub 显示(owner As Form, dataProvider As Func(Of String, Dictionary(Of String, List(Of Double))), scoreProvider As Func(Of String, Dictionary(Of String, String)))
        If 当前窗体 Is Nothing OrElse 当前窗体.IsDisposed Then
            当前窗体 = New Form_v6_集成工具_质量评测图表()
        End If

        当前窗体.设置数据源(dataProvider, scoreProvider)
        当前窗体.刷新图表()
        当前窗体.显示到主窗口中心()
    End Sub

    Public Shared Sub 刷新当前图表()
        If 当前窗体 Is Nothing OrElse 当前窗体.IsDisposed Then Exit Sub
        当前窗体.刷新图表()
    End Sub

    Public Shared Sub 关闭当前图表()
        If 当前窗体 Is Nothing OrElse 当前窗体.IsDisposed Then Exit Sub
        当前窗体.Close()
    End Sub

    Private Sub Form_v6_集成工具_质量评测图表_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Icon = FormMain_v6.Icon
        Catch
        End Try

        If FormMain_v6.ThisIsYourWindow1.AttachedForms.Count > 0 Then
            Try
                ModernPanel1.BackColor = Color.Transparent
                ModernPanel1.BackColor1 = Color.Transparent
                FormMain_v6.ThisIsYourWindow1.Attach(Me)
            Catch
            End Try
        End If
    End Sub

    Private Sub 设置数据源(dataProvider As Func(Of String, Dictionary(Of String, List(Of Double))), scoreProvider As Func(Of String, Dictionary(Of String, String)))
        数据提供器 = dataProvider
        分数提供器 = scoreProvider
    End Sub

    Private Sub 显示到主窗口中心()
        Dim basis As Form = FormMain_v6
        If basis Is Nothing OrElse basis.IsDisposed Then basis = Owner

        If Visible Then
            Focus()
        ElseIf basis IsNot Nothing AndAlso Not basis.IsDisposed Then
            Show(basis)
        Else
            Show()
        End If

        Dim basisBounds As Rectangle
        If basis IsNot Nothing AndAlso Not basis.IsDisposed AndAlso basis.Visible Then
            basisBounds = basis.Bounds
        Else
            basisBounds = Screen.FromControl(Me).WorkingArea
        End If

        Dim targetLeft = basisBounds.Left + (basisBounds.Width - Width) \ 2
        Dim targetTop = basisBounds.Top + (basisBounds.Height - Height) \ 2
        Dim workArea = Screen.FromRectangle(New Rectangle(targetLeft, targetTop, Width, Height)).WorkingArea

        Left = Math.Max(workArea.Left, Math.Min(targetLeft, workArea.Right - Width))
        Top = Math.Max(workArea.Top, Math.Min(targetTop, workArea.Bottom - Height))
    End Sub

    Private Sub MCB_指标选择_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_指标选择.SelectedIndexChanged
        刷新图表()
    End Sub

    Private Sub 刷新图表()
        If Ultra2DChart1 Is Nothing OrElse Ultra2DChart1.IsDisposed Then Exit Sub
        Dim metric = 获取当前指标()
        Dim seriesData = If(数据提供器?.Invoke(metric), New Dictionary(Of String, List(Of Double))(StringComparer.CurrentCultureIgnoreCase))

        Ultra2DChart1.BeginUpdate()
        Try
            Ultra2DChart1.ClearData()
            Ultra2DChart1.XAxisTitle = "帧"
            Ultra2DChart1.YAxisTitle = metric
            设置坐标范围(metric)

            Dim maxCount = If(seriesData.Count = 0, 0, seriesData.Max(Function(x) If(x.Value Is Nothing, 0, x.Value.Count)))
            Ultra2DChart1.SetCategories(Enumerable.Range(1, maxCount).Select(Function(i) i.ToString(CultureInfo.InvariantCulture)).ToArray())

            Dim seriesIndex = 0
            For Each entry In seriesData
                Dim values = Enumerable.Range(0, maxCount).
                    Select(Function(i) If(entry.Value IsNot Nothing AndAlso i < entry.Value.Count, 转换图表值(entry.Value(i)), Double.NaN)).
                    ToArray()
                Dim series = Ultra2DChart1.AddSeries(entry.Key, Ultra2DChart.ChartSeriesTypeEnum.Line, values)
                series.Color = 获取系列颜色(seriesIndex)
                series.LineThickness = 2.0F
                series.MarkerShape = Ultra2DChart.MarkerShapeEnum.None
                series.ShowValueLabels = Ultra2DChart.SeriesValueLabelModeEnum.Hide
                seriesIndex += 1
            Next
        Finally
            Ultra2DChart1.EndUpdate()
            更新状态(metric, seriesData)
            Ultra2DChart1.RefreshChart()
        End Try
    End Sub

    Private Sub 设置坐标范围(metric As String)
        If String.Equals(metric, "SSIM", StringComparison.OrdinalIgnoreCase) Then
            Ultra2DChart1.YAxisRangeMode = Ultra2DChart.AxisRangeModeEnum.Fixed
            Ultra2DChart1.YAxisMinimum = 0
            Ultra2DChart1.YAxisMaximum = 1
        ElseIf String.Equals(metric, "VMAF", StringComparison.OrdinalIgnoreCase) Then
            Ultra2DChart1.YAxisRangeMode = Ultra2DChart.AxisRangeModeEnum.Fixed
            Ultra2DChart1.YAxisMinimum = 0
            Ultra2DChart1.YAxisMaximum = 100
        Else
            Ultra2DChart1.YAxisRangeMode = Ultra2DChart.AxisRangeModeEnum.Auto
        End If
    End Sub

    Private Sub 更新状态(metric As String, seriesData As Dictionary(Of String, List(Of Double)))
        If HtmlColorLabel1 Is Nothing OrElse HtmlColorLabel1.IsDisposed Then Exit Sub
        If seriesData Is Nothing OrElse seriesData.Count = 0 Then
            HtmlColorLabel1.Text = "暂无帧级数据"
            Exit Sub
        End If

        Dim scores = If(分数提供器?.Invoke(metric), New Dictionary(Of String, String)(StringComparer.CurrentCultureIgnoreCase))
        Dim parts As New List(Of String)
        Dim index = 0
        For Each entry In seriesData
            Dim scoreText As String = Nothing
            If Not scores.TryGetValue(entry.Key, scoreText) OrElse String.IsNullOrWhiteSpace(scoreText) Then
                scoreText = 格式化最后图表值(metric, entry.Value)
            End If

            If Not String.IsNullOrWhiteSpace(scoreText) Then
                Dim color = 获取系列颜色(index)
                parts.Add($"<span style=""color:{颜色转Html(color)}"">{转义Html文本(scoreText)}</span>")
            End If
            index += 1
        Next

        HtmlColorLabel1.Text = If(parts.Count = 0, "暂无帧级数据", String.Join("    ", parts))
    End Sub

    Private Function 获取系列颜色(index As Integer) As Color
        If Ultra2DChart1 IsNot Nothing AndAlso Ultra2DChart1.Palette.Count > 0 Then
            Return Ultra2DChart1.Palette(Math.Abs(index) Mod Ultra2DChart1.Palette.Count)
        End If
        Return Color.FromArgb(68, 114, 196)
    End Function

    Private Shared Function 颜色转Html(color As Color) As String
        Return $"#{color.R:X2}{color.G:X2}{color.B:X2}"
    End Function

    Private Shared Function 转义Html文本(text As String) As String
        Return System.Net.WebUtility.HtmlEncode(If(text, ""))
    End Function

    Private Shared Function 格式化最后图表值(metric As String, values As List(Of Double)) As String
        If values Is Nothing OrElse values.Count = 0 Then Return ""
        For i = values.Count - 1 To 0 Step -1
            Dim value = values(i)
            If Double.IsNaN(value) OrElse Double.IsInfinity(value) Then Continue For
            If String.Equals(metric, "SSIM", StringComparison.OrdinalIgnoreCase) Then
                Return value.ToString("0.000000", CultureInfo.InvariantCulture)
            End If
            Return value.ToString("0.000", CultureInfo.InvariantCulture)
        Next
        Return ""
    End Function

    Private Function 获取当前指标() As String
        If MCB_指标选择 IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(MCB_指标选择.Text) Then Return MCB_指标选择.Text.Trim()
        If MCB_指标选择 IsNot Nothing AndAlso MCB_指标选择.SelectedItem IsNot Nothing Then Return MCB_指标选择.SelectedItem.ToString()
        Return "PSNR"
    End Function

    Private Shared Function 转换图表值(value As Double) As Double
        If Double.IsNaN(value) OrElse Double.IsInfinity(value) Then Return Double.NaN
        Return value
    End Function

End Class
