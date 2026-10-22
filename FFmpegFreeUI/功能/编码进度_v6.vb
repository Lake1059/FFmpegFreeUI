Imports System.Globalization
Imports System.Text.RegularExpressions

Public Class 编码进度_v6
    Private Shared ReadOnly DurationPattern As New Regex("Duration:\s*(\d+:\d{2}:\d{2}(?:\.\d+)?)", RegexOptions.Compiled)
    Private Shared ReadOnly SizePattern As New Regex("size=\s*(?<value>\d+)\s*(?<unit>[KMG]iB|kB)", RegexOptions.Compiled Or RegexOptions.IgnoreCase)
    Private Shared ReadOnly TimePattern As New Regex("time=\s*(?<value>\d+:\d{2}:\d{2}(?:\.\d+)?)", RegexOptions.Compiled)
    Private Shared ReadOnly ClockPattern As New Regex("^(?<hours>\d+):(?<minutes>[0-5]\d):(?<seconds>[0-5]\d(?:\.\d+)?)$", RegexOptions.Compiled Or RegexOptions.CultureInvariant)
    Private Shared ReadOnly QPattern As New Regex("q=\s*(?<value>[\d\.\-]+)", RegexOptions.Compiled)
    Private Shared ReadOnly BitratePattern As New Regex("bitrate=\s*(?<value>[\d\.]+)\s*kbits/s", RegexOptions.Compiled)
    Private Shared ReadOnly SpeedPattern As New Regex("speed=\s*(?<value>[\d\.eE\+\-]+)\s*x", RegexOptions.Compiled)
    Private Const 最低有效速度 As Double = 0.01

    Public Property 当前阶段 As String = ""
    Public Property 总时长 As TimeSpan = TimeSpan.Zero
    Public Property 当前时间 As TimeSpan = TimeSpan.Zero
    Public Property 百分比 As Double = 0
    Public Property 进度文本 As String = ""
    Public Property 效率文本 As String = ""
    Public Property 输出大小文本 As String = ""
    Public Property 输出大小KB As Long = 0
    Public Property 质量文本 As String = ""
    Public Property 比特率文本 As String = ""
    Public Property 时间文本 As String = ""

    Public Sub 解析FFmpeg输出(line As String, preferredTotal As TimeSpan)
        If String.IsNullOrWhiteSpace(line) Then Exit Sub
        If 总时长 = TimeSpan.Zero AndAlso preferredTotal > TimeSpan.Zero Then 总时长 = preferredTotal
        If 解析Progress键值(line) Then
            更新百分比()
            更新输出大小文本()
            Exit Sub
        End If
        Dim detectedDuration = 提取媒体总时长(line)
        If 总时长 = TimeSpan.Zero AndAlso detectedDuration > TimeSpan.Zero Then 总时长 = detectedDuration
        Dim tm = TimePattern.Match(line)
        If tm.Success Then 当前时间 = 转换时间(tm.Groups("value").Value)

        更新百分比()

        Dim sm = SizePattern.Match(line)
        If sm.Success Then
            Dim size As Long
            If Long.TryParse(sm.Groups("value").Value, NumberStyles.None, CultureInfo.InvariantCulture, size) Then
                输出大小KB = 转换到KB(size, sm.Groups("unit").Value)
            End If
        End If
        更新输出大小文本()
        Dim qm = QPattern.Match(line)
        If qm.Success Then 质量文本 = 格式化质量文本(qm.Groups("value").Value)
        Dim bm = BitratePattern.Match(line)
        If bm.Success Then 比特率文本 = 格式化比特率文本(bm.Groups("value").Value)
        Dim sp = SpeedPattern.Match(line)
        If sp.Success Then
            Dim speed As Double
            If Double.TryParse(sp.Groups("value").Value, NumberStyles.Any, CultureInfo.InvariantCulture, speed) Then
                效率文本 = 格式化效率文本(speed)
                If 总时长.TotalSeconds > 0 AndAlso 当前时间.TotalSeconds > 0 AndAlso
                   Not Double.IsNaN(speed) AndAlso Not Double.IsInfinity(speed) AndAlso speed >= 最低有效速度 Then
                    Dim remain = Math.Max(0, (总时长.TotalSeconds - 当前时间.TotalSeconds) / speed)
                    ' FFmpeg startup samples can report an extremely small speed (for example 0.00x).
                    ' Do not expose the resulting multi-year ETA; wait for a usable sample instead.
                    Dim maximumRemain = Math.Max(TimeSpan.FromDays(30).TotalSeconds, 总时长.TotalSeconds * 100.0R)
                    If Not Double.IsNaN(remain) AndAlso Not Double.IsInfinity(remain) AndAlso remain <= maximumRemain Then
                        时间文本 = 格式化秒(remain)
                    Else
                        时间文本 = ""
                    End If
                Else
                    时间文本 = ""
                End If
            Else
                时间文本 = ""
            End If
        Else
            时间文本 = ""
        End If
    End Sub

    Private Sub 更新百分比()
        If 总时长 > TimeSpan.Zero AndAlso 当前时间 > TimeSpan.Zero Then
            百分比 = Math.Clamp(当前时间.TotalSeconds / 总时长.TotalSeconds, 0, 1)
            进度文本 = $"{百分比 * 100:F1}%"
        Else
            百分比 = 0
            进度文本 = ""
        End If
    End Sub

    Private Function 解析Progress键值(line As String) As Boolean
        Dim separator = line.IndexOf("="c)
        If separator < 0 Then Return False
        Dim key = line.Substring(0, separator).Trim()
        Dim value = line.Substring(separator + 1).Trim()
        Select Case key
            Case "out_time"
                当前时间 = 转换时间(value)
            Case "out_time_us", "out_time_ms"
                ' FFmpeg's legacy out_time_ms field also contains microseconds.
                Dim microseconds As Long
                If Long.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, microseconds) AndAlso microseconds >= 0 AndAlso microseconds <= Long.MaxValue \ 10 Then 当前时间 = TimeSpan.FromTicks(microseconds * 10)
            Case "total_size"
                Dim bytes As Long
                If Long.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, bytes) AndAlso bytes >= 0 Then 输出大小KB = bytes \ 1024
            Case "bitrate"
                比特率文本 = If(value = "N/A", "", 格式化比特率文本(value.Replace("kbits/s", "").Trim()))
            Case "speed"
                Dim speed As Double
                时间文本 = ""
                If Double.TryParse(value.TrimEnd("x"c), NumberStyles.Float, CultureInfo.InvariantCulture, speed) AndAlso Double.IsFinite(speed) AndAlso speed >= 0 Then
                    效率文本 = 格式化效率文本(speed)
                    If speed >= 最低有效速度 AndAlso 总时长 > TimeSpan.Zero AndAlso 当前时间 > TimeSpan.Zero Then
                        Dim remain = Math.Max(0, (总时长 - 当前时间).TotalSeconds / speed)
                        If remain <= Math.Max(TimeSpan.FromDays(30).TotalSeconds, 总时长.TotalSeconds * 100) Then 时间文本 = 格式化秒(remain)
                    End If
                Else
                    效率文本 = ""
                End If
            Case "progress"
                Return value = "continue" OrElse value = "end"
            Case Else
                Return False
        End Select
        Return True
    End Function

    Friend Shared Function 提取媒体总时长(line As String) As TimeSpan
        If String.IsNullOrWhiteSpace(line) Then Return TimeSpan.Zero
        Dim match = DurationPattern.Match(line)
        If Not match.Success Then Return TimeSpan.Zero
        Return 转换时间(match.Groups(1).Value)
    End Function

    Private Shared Function 格式化质量文本(value As String) As String
        Dim q As Double
        If Not Double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, q) Then Return If(value, "")
        If q = 0 OrElse q = -1 Then Return ""
        Return q.ToString("F0", CultureInfo.InvariantCulture)
    End Function

    Private Shared Function 格式化效率文本(speed As Double) As String
        If speed * 100 >= 10000 Then Return speed.ToString("F0", CultureInfo.InvariantCulture) & "x"
        Return $"{speed * 100:F0}%"
    End Function

    Private Shared Function 格式化比特率文本(value As String) As String
        Dim bitrate As Double
        If Not Double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, bitrate) Then Return If(value, "") & " kbps"
        If bitrate >= 10000 Then Return (bitrate / 1000).ToString("F2", CultureInfo.InvariantCulture) & " Mbps"
        Return bitrate.ToString("F0", CultureInfo.InvariantCulture) & " kbps"
    End Function

    Private Sub 更新输出大小文本()
        If 输出大小KB <= 0 Then
            输出大小文本 = ""
            Exit Sub
        End If

        输出大小文本 = 格式化大小KB(输出大小KB)
        If 百分比 <= 0 OrElse 百分比 >= 1 Then Exit Sub

        Dim estimate = 输出大小KB / 百分比
        Dim estimatedSizeKB = If(estimate >= Long.MaxValue, Long.MaxValue, CLng(estimate))
        输出大小文本 &= " - " & 格式化预估大小KB(estimatedSizeKB)
    End Sub

    Public Shared Function 转换时间(value As String) As TimeSpan
        If String.IsNullOrWhiteSpace(value) Then Return TimeSpan.Zero

        ' FFprobe stores durations as plain seconds (for example "3600").
        ' TimeSpan.TryParse treats a plain integer as days, which can inflate
        ' the ETA by 24 hours per unit. Parse numeric values as seconds first.
        Dim seconds As Double
        If Double.TryParse(value.Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, seconds) Then
            If Not Double.IsFinite(seconds) OrElse seconds < 0 OrElse seconds >= TimeSpan.MaxValue.TotalSeconds Then Return TimeSpan.Zero
            Return TimeSpan.FromSeconds(seconds)
        End If

        Dim clock = ClockPattern.Match(value.Trim())
        If clock.Success Then
            Dim hours As Double
            If Not Double.TryParse(clock.Groups("hours").Value, NumberStyles.None, CultureInfo.InvariantCulture, hours) Then Return TimeSpan.Zero
            seconds = hours * 3600 + Integer.Parse(clock.Groups("minutes").Value, CultureInfo.InvariantCulture) * 60 + Double.Parse(clock.Groups("seconds").Value, CultureInfo.InvariantCulture)
            If Not Double.IsFinite(seconds) OrElse seconds >= TimeSpan.MaxValue.TotalSeconds Then Return TimeSpan.Zero
            Return TimeSpan.FromSeconds(seconds)
        End If

        Dim t As TimeSpan
        If TimeSpan.TryParse(value.Trim(), CultureInfo.InvariantCulture, t) AndAlso t >= TimeSpan.Zero Then Return t
        Return TimeSpan.Zero
    End Function

    Private Shared Function 转换到KB(value As Long, unit As String) As Long
        Dim multiplier As Long = 1
        Select Case unit.ToLowerInvariant()
            Case "mib" : multiplier = 1024
            Case "gib" : multiplier = 1024L * 1024L
        End Select
        If value > Long.MaxValue \ multiplier Then Return Long.MaxValue
        Return value * multiplier
    End Function

    Public Shared Function 格式化大小KB(value As Long) As String
        If value >= 1024L * 1024L Then Return $"{value / 1024.0 / 1024.0:F2} GB"
        If value >= 1024L Then Return $"{value / 1024.0:F0} MB"
        Return $"{value} KB"
    End Function

    Private Shared Function 格式化预估大小KB(value As Long) As String
        If value >= 1024L * 1024L Then Return $"{value / 1024.0 / 1024.0:F1} GB"
        If value >= 1024L Then Return $"{value / 1024.0:F0} MB"
        Return $"{value} KB"
    End Function

    Public Shared Function 格式化秒(value As Double) As String
        If Double.IsNaN(value) OrElse Double.IsInfinity(value) OrElse value < 0 Then Return ""
        Dim safeValue = Math.Min(value, TimeSpan.MaxValue.TotalSeconds)
        Dim totalHours = CLng(Math.Floor(safeValue / 3600.0R))
        Dim d = totalHours \ 24
        Dim h = totalHours Mod 24
        Dim m = CLng(Math.Floor((safeValue - totalHours * 3600.0R) / 60.0R))
        Dim s = CLng(Math.Floor(safeValue - totalHours * 3600.0R - m * 60.0R))
        Dim parts As New List(Of String)
        If d > 0 Then parts.Add($"{d}d")
        If h > 0 OrElse d > 0 Then parts.Add($"{h}h")
        If m > 0 OrElse h > 0 OrElse d > 0 Then parts.Add($"{m}m")
        parts.Add($"{s}s")
        Return String.Join("", parts)
    End Function
End Class
