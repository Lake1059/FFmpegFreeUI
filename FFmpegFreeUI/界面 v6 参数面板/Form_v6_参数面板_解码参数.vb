Imports System.Diagnostics
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Threading.Tasks
Imports Vortice.DXGI

Public Class Form_v6_参数面板_解码参数

    Private Async Sub MB_查看当前系统的显卡索引_Click(sender As Object, e As EventArgs) Handles MB_查看当前系统的显卡索引.Click
        MB_查看当前系统的显卡索引.Enabled = False
        Dim 原文字 = MB_查看当前系统的显卡索引.Text
        MB_查看当前系统的显卡索引.Text = "正在查询显卡索引..."
        Try
            Dim ffmpeg = 设置_v6.获取FFmpeg进程文件名()
            Dim 工作目录 = 设置_v6.获取有效工作目录()
            Dim 内容 = Await Task.Run(Async Function()
                                        Dim 结果 As New StringBuilder()
                                        结果.AppendLine()
                                        结果.AppendLine("NVIDIA CUDA / NVENC 索引（当前 FFmpeg）")
                                        结果.AppendLine(Await 查询CUDA索引Async(ffmpeg, 工作目录))
                                        结果.AppendLine()
                                        结果.AppendLine("Direct3D / DXGI 适配器索引")
                                        结果.AppendLine(查询DXGI索引())
                                        结果.AppendLine()
                                        结果.AppendLine("CUDA：-hwaccel cuda、-hwaccel_device N 或 -init_hw_device cuda:N")
                                        结果.AppendLine("D3D11VA：-hwaccel d3d11va -hwaccel_device N")
                                        结果.Append("ffmpeg 使用的 GPU 索引号与 nvidia-smi 等驱动工具不一致！")
                                        Return 结果.ToString()
                                    End Function)
            If IsDisposed OrElse Disposing Then Return
            LakeUI.ExOverlayMsgBox(FormMain_v6, 内容, , "当前系统的显卡索引")
        Catch ex As Exception
            If Not IsDisposed AndAlso Not Disposing Then
                LakeUI.ExOverlayMsgBox(FormMain_v6, "无法查询显卡索引：" & ex.Message, MsgBoxStyle.Critical, "显卡索引查询失败")
            End If
        Finally
            If Not IsDisposed AndAlso Not Disposing Then
                MB_查看当前系统的显卡索引.Text = 原文字
                MB_查看当前系统的显卡索引.Enabled = True
            End If
        End Try
    End Sub

    Private Shared Async Function 查询CUDA索引Async(ffmpeg As String, 工作目录 As String) As Task(Of String)
        Try
            Using process As New Process(), timeout As New CancellationTokenSource(TimeSpan.FromSeconds(15))
                process.StartInfo = New ProcessStartInfo With {
                    .FileName = ffmpeg,
                    .WorkingDirectory = 工作目录,
                    .Arguments = "-hide_banner -nostdin -loglevel verbose -f lavfi -i color=s=64x64 -frames:v 1 -c:v h264_nvenc -gpu list -f null -",
                    .UseShellExecute = False,
                    .CreateNoWindow = True,
                    .RedirectStandardOutput = True,
                    .RedirectStandardError = True,
                    .StandardOutputEncoding = Encoding.UTF8,
                    .StandardErrorEncoding = Encoding.UTF8
                }
                process.Start()
                Dim stdout = process.StandardOutput.ReadToEndAsync()
                Dim stderr = process.StandardError.ReadToEndAsync()
                Dim 已超时 = False
                Try
                    Await process.WaitForExitAsync(timeout.Token)
                Catch ex As OperationCanceledException
                    If Not process.HasExited Then process.Kill(True)
                    已超时 = True
                End Try
                Dim 标准错误 = Await stderr
                Dim 标准输出 = Await stdout
                If 已超时 Then Return "查询超时（15 秒），请检查 FFmpeg 和 NVIDIA 驱动。"
                Dim 输出 = 标准错误 & vbCrLf & 标准输出
                ' -gpu list 枚举后会主动结束，不能以退出码判断查询失败。
                Dim 显卡 As New List(Of String)
                For Each item As Match In Regex.Matches(输出, "\[\s*GPU\s+#(?<index>\d+)\s*-\s*<\s*(?<name>[^>]+?)\s*>")
                    Dim 行 = item.Groups("index").Value & "：" & item.Groups("name").Value.Trim()
                    If Not 显卡.Contains(行) Then 显卡.Add(行)
                Next
                If 显卡.Count > 0 Then Return String.Join(vbCrLf, 显卡)
                Dim 日志 = 输出.Split({vbCr, vbLf}, StringSplitOptions.RemoveEmptyEntries)
                Return "未能取得 CUDA 索引（可能无 NVIDIA 显卡、驱动不可用，或此 FFmpeg 不支持 NVENC/lavfi）。" & vbCrLf &
                    String.Join(vbCrLf, 日志.TakeLast(5)).Trim()
            End Using
        Catch ex As Exception
            Return "CUDA 查询失败：" & ex.Message
        End Try
    End Function

    Private Shared Function 查询DXGI索引() As String
        Try
            Dim 显卡 As New List(Of String)
            Using factory = DXGI.CreateDXGIFactory1(Of IDXGIFactory1)()
                Dim index As UInteger = 0
                Dim adapter As IDXGIAdapter1 = Nothing
                While factory.EnumAdapters1(index, adapter).Success
                    Using adapter
                        Dim 描述 = adapter.Description1
                        Dim 后缀 = If((描述.Flags And AdapterFlags.Software) <> 0, "（软件适配器）", "")
                        显卡.Add(index.ToString() & "：" & 描述.Description.TrimEnd(ChrW(0)) & 后缀)
                    End Using
                    index += 1UI
                End While
            End Using
            Return If(显卡.Count > 0, String.Join(vbCrLf, 显卡), "未发现 DXGI 适配器。")
        Catch ex As Exception
            Return "DXGI 查询失败：" & ex.Message
        End Try
    End Function

End Class
