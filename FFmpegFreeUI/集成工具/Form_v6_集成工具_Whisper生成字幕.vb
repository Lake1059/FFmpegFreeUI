Imports System.Globalization
Imports System.IO
Imports System.Text
Imports LakeUI

Public Class Form_v6_集成工具_Whisper生成字幕

    Private Const 浏览文件项 As String = "浏览 ..."
    Private Const 打开模型下载页项 As String = "打开模型下载页"
    Private Const 打开VAD下载页项 As String = "打开 VAD 下载页"
    Private Const Whisper模型下载页 As String = "https://huggingface.co/ggerganov/whisper.cpp/tree/main"
    Private Const VAD模型下载页 As String = "https://huggingface.co/ggml-org/whisper-vad/tree/main"
    Private Const 推荐VAD模型下载地址 As String = "https://huggingface.co/ggml-org/whisper-vad/resolve/main/ggml-silero-v6.2.0.bin"

    Private 正在选择路径 As Boolean = False
    Private 上次自动输出文件 As String = ""

    Private Sub Form_v6_集成工具_Whisper生成字幕_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        初始化控件()
        绑定路径下拉框拖拽(MCB_输入文件, 路径下拉框拖拽模式.文件路径, Sub(combo, 路径) 输入文件已改变(路径))
        绑定路径下拉框拖拽(MCB_选择模型文件, 路径下拉框拖拽模式.文件路径)
        绑定路径下拉框拖拽(MCB_VAD模型文件, 路径下拉框拖拽模式.文件路径)
        绑定文件拖入(Me)
        绑定文件拖入(ModernPanel1)
        绑定文件拖入(Panel2)
        绑定文件拖入(Panel1)
        绑定文件拖入(Panel9)
        绑定文件拖入(Panel11)
    End Sub

    Private Sub 初始化控件()
        MCB_翻译成英语.ClickAnywhere = True

        填充下拉框(MCB_输入文件, 浏览文件项)
        填充下拉框(MCB_选择模型文件, 浏览文件项, 打开模型下载页项)
        填充下拉框(MCB_VAD模型文件, 浏览文件项, 打开VAD下载页项)
        填充下拉框(MCB_GPU加速选项, "自动", "启用", "禁用")
        填充下拉框(MCB_要识别的语言, "auto", "zh", "en", "ja", "ko", "fr", "de", "es", "ru", "it", "pt", "vi")
        填充下拉框(MCB_输出类型, "srt", "text", "json")

        设置下拉框选中值(MCB_GPU加速选项, "自动")
        设置下拉框选中值(MCB_要识别的语言, "auto")
        设置下拉框选中值(MCB_输出类型, "srt")

        配置滑条(ETB_音频缓存长度, 1, 60, 20, 0, 1, 5)
        配置滑条(ETB_字幕片段最大字符数, 0, 100, 42, 0, 1, 10)
        配置滑条(ETB_VAD阈值, 0, 1, 0.5, 2, 0.05, 0.1)
        配置滑条(ETB_VAD最短语音持续时间, 0, 5, 0.1, 2, 0.05, 0.1)
        配置滑条(ETB_VAD最短静音持续时间, 0, 5, 0.5, 2, 0.05, 0.1)

        MTB_音频流索引.Text = "0"
        MTB_GPU设备索引.Text = "0"
        MTB_音频流索引.WaterText = "0"
        MTB_GPU设备索引.WaterText = "0"

        HtmlColorLabel1.ToolTipText = "使用 FFmpeg 的 whisper 音频滤镜生成字幕。当前任务会把识别文本写到 destination 指定的输出文件，媒体输出本身丢弃到 null。"
        HtmlColorLabel2.ToolTipText = "Whisper 模型不是 FFmpeg 内置，需要选择 whisper.cpp 的 ggml 模型文件。" & vbCrLf &
                                      "模型下载页：" & Whisper模型下载页 & vbCrLf &
                                      "常用模型：ggml-base.bin、ggml-small.bin、ggml-medium.bin、ggml-large-v3.bin、ggml-large-v3-turbo.bin。" & vbCrLf &
                                      "英文专用模型带 .en；q5_0 是量化版，体积更小。"
        HtmlColorLabel4.ToolTipText = "对应 whisper 滤镜的 queue。值越大，上下文越完整，通常识别质量更好；值越小，处理响应更快。离线生成字幕推荐 10 到 20 秒以上。"
        MCB_字幕片段最大字符数.ToolTipText = "对应 whisper 滤镜的 max_len。0 表示不限制；SRT 字幕常用 32 到 48 个字符。"
        HtmlColorLabel7.ToolTipText = "对应 whisper 滤镜的 language。auto 自动检测；中文填 zh，英文填 en，也可以直接输入 whisper.cpp 支持的语言代码。"
        HtmlColorLabel12.ToolTipText = "VAD 是可选的语音活动检测模型，不设置也能识别。" & vbCrLf &
                                       "设置后会按语音/静音边界切分，长音频和带大量静音的内容通常更稳。" & vbCrLf &
                                       "推荐下载：" & 推荐VAD模型下载地址
        HtmlColorLabel11.ToolTipText = "对应 vad_threshold。值越低越容易判定为语音，可能减少漏字但会增加噪声误检。默认 0.5。"
        HtmlColorLabel10.ToolTipText = "对应 vad_min_speech_duration。低于此时长的语音片段会被忽略，默认 0.1 秒。"
        HtmlColorLabel9.ToolTipText = "对应 vad_min_silence_duration。静音达到此时长才切分片段，默认 0.5 秒。"
        HtmlColorLabel13.ToolTipText = "对应 use_gpu 和 gpu_device。自动表示不写 use_gpu，使用 FFmpeg 滤镜默认值。"
        HtmlColorLabel15.ToolTipText = "对应 whisper 滤镜的 format，可输出 text、srt 或 json。"
        HtmlColorLabel14.ToolTipText = "对应 whisper 滤镜的 destination。字幕文件由滤镜写出，FFmpeg 的媒体输出会丢弃到 null。"

        MCB_选择模型文件.ItemToolTips.Add(打开模型下载页项, Whisper模型下载页)
        MCB_VAD模型文件.ItemToolTips.Add(打开VAD下载页项, VAD模型下载页)
        MCB_GPU加速选项.ItemToolTips.Add("自动", "不显式写 use_gpu，使用 FFmpeg whisper 滤镜默认值。")
        MCB_GPU加速选项.ItemToolTips.Add("启用", "写入 use_gpu=true，并使用右侧 GPU 设备索引。")
        MCB_GPU加速选项.ItemToolTips.Add("禁用", "写入 use_gpu=false，使用 CPU 处理。")
        MCB_输出类型.ItemToolTips.Add("srt", "生成 SRT 字幕文件。")
        MCB_输出类型.ItemToolTips.Add("text", "生成纯文本转写文件。")
        MCB_输出类型.ItemToolTips.Add("json", "生成 whisper.cpp JSON 输出。")
    End Sub

    Private Shared Sub 填充下拉框(combo As ModernComboBox, ParamArray values() As String)
        If combo Is Nothing Then Exit Sub
        combo.Items.Clear()
        For Each value In values
            combo.Items.Add(value)
        Next
    End Sub

    Private Shared Sub 设置下拉框选中值(combo As ModernComboBox, value As String)
        If combo Is Nothing Then Exit Sub
        For i = 0 To combo.Items.Count - 1
            If String.Equals(combo.Items(i).ToString(), value, StringComparison.OrdinalIgnoreCase) Then
                combo.SelectedIndex = i
                Exit Sub
            End If
        Next
        combo.Text = value
    End Sub

    Private Shared Sub 配置滑条(trackBar As ExcellentTrackBar, minimum As Double, maximum As Double, value As Double, decimalPlaces As Integer, smallChange As Double, largeChange As Double)
        If trackBar Is Nothing Then Exit Sub
        trackBar.Minimum = minimum
        trackBar.Maximum = maximum
        trackBar.SmallChange = smallChange
        trackBar.LargeChange = largeChange
        trackBar.ThumbTextDecimalPlaces = decimalPlaces
        trackBar.Value = Math.Max(minimum, Math.Min(maximum, value))
    End Sub

    Private Sub 绑定文件拖入(target As Control)
        If target Is Nothing Then Exit Sub
        target.AllowDrop = True
        RemoveHandler target.DragEnter, AddressOf 文件拖入事件
        RemoveHandler target.DragDrop, AddressOf 文件放下事件
        AddHandler target.DragEnter, AddressOf 文件拖入事件
        AddHandler target.DragDrop, AddressOf 文件放下事件
    End Sub

    Private Sub 文件拖入事件(sender As Object, e As DragEventArgs)
        e.Effect = If(获取拖入文件(e).Count > 0, DragDropEffects.Copy, DragDropEffects.None)
    End Sub

    Private Sub 文件放下事件(sender As Object, e As DragEventArgs)
        Dim files = 获取拖入文件(e)
        If files.Count = 0 Then Exit Sub
        MCB_输入文件.Text = files(0)
        输入文件已改变(files(0))
    End Sub

    Private Function 获取拖入文件(e As DragEventArgs) As List(Of String)
        If e Is Nothing OrElse e.Data Is Nothing OrElse Not e.Data.GetDataPresent(DataFormats.FileDrop) Then Return New List(Of String)
        Dim paths = TryCast(e.Data.GetData(DataFormats.FileDrop), String())
        If paths Is Nothing Then Return New List(Of String)
        Dim result As New List(Of String)
        For Each raw In paths
            If String.IsNullOrWhiteSpace(raw) Then Continue For
            Try
                If File.Exists(raw) Then
                    result.Add(raw)
                ElseIf Directory.Exists(raw) Then
                    result.AddRange(Directory.GetFiles(raw, "*", SearchOption.TopDirectoryOnly).OrderBy(Function(x) x, StringComparer.CurrentCultureIgnoreCase))
                End If
            Catch ex As Exception
                ExFloatingTip(ModernPanel1, $"读取路径失败：{ex.Message}", 2200)
            End Try
        Next
        Return result
    End Function

    Private Sub MCB_输入文件_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_输入文件.SelectedIndexChanged
        If 正在选择路径 Then Exit Sub
        If MCB_输入文件.SelectedIndex = 0 Then 选择输入文件()
    End Sub

    Private Sub MCB_选择模型文件_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_选择模型文件.SelectedIndexChanged
        If 正在选择路径 Then Exit Sub
        Select Case MCB_选择模型文件.Text
            Case 浏览文件项
                选择模型文件()
            Case 打开模型下载页项
                打开网址(Whisper模型下载页, MCB_选择模型文件)
        End Select
    End Sub

    Private Sub MCB_VAD模型文件_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_VAD模型文件.SelectedIndexChanged
        If 正在选择路径 Then Exit Sub
        Select Case MCB_VAD模型文件.Text
            Case 浏览文件项
                选择VAD模型文件()
            Case 打开VAD下载页项
                打开网址(VAD模型下载页, MCB_VAD模型文件)
        End Select
    End Sub

    Private Sub MCB_输出类型_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_输出类型.SelectedIndexChanged
        If 正在选择路径 Then Exit Sub
        生成自动输出文件名(False)
    End Sub

    Private Sub MCB_输入文件_TextChanged(sender As Object, e As EventArgs) Handles MCB_输入文件.TextChanged
        If 正在选择路径 Then Exit Sub
        输入文件已改变(MCB_输入文件.Text)
    End Sub

    Private Sub 输入文件已改变(file As String)
        If 正在选择路径 Then Exit Sub
        生成自动输出文件名(False)
    End Sub

    Private Sub 选择输入文件()
        正在选择路径 = True
        Try
            Using d As New OpenFileDialog With {.Multiselect = False, .Filter = "媒体文件|*.mp4;*.mkv;*.mov;*.avi;*.flv;*.webm;*.mp3;*.m4a;*.wav;*.flac;*.aac;*.ogg;*.opus|所有文件|*.*"}
                Dim 当前路径 = MCB_输入文件.Text.Trim()
                If File.Exists(当前路径) Then d.FileName = 当前路径
                If d.ShowDialog(Me) = DialogResult.OK Then
                    MCB_输入文件.Text = d.FileName
                    输入文件已改变(d.FileName)
                Else
                    MCB_输入文件.SelectedIndex = -1
                End If
            End Using
        Finally
            正在选择路径 = False
        End Try
    End Sub

    Private Sub 选择模型文件()
        正在选择路径 = True
        Try
            Using d As New OpenFileDialog With {.Multiselect = False, .Filter = "Whisper 模型|ggml-*.bin;*.bin;*.gguf|所有文件|*.*"}
                Dim 当前路径 = MCB_选择模型文件.Text.Trim()
                If File.Exists(当前路径) Then d.FileName = 当前路径
                If d.ShowDialog(Me) = DialogResult.OK Then
                    MCB_选择模型文件.Text = d.FileName
                Else
                    MCB_选择模型文件.SelectedIndex = -1
                End If
            End Using
        Finally
            正在选择路径 = False
        End Try
    End Sub

    Private Sub 选择VAD模型文件()
        正在选择路径 = True
        Try
            Using d As New OpenFileDialog With {.Multiselect = False, .Filter = "VAD 模型|ggml-silero*.bin;*.bin;*.gguf|所有文件|*.*"}
                Dim 当前路径 = MCB_VAD模型文件.Text.Trim()
                If File.Exists(当前路径) Then d.FileName = 当前路径
                If d.ShowDialog(Me) = DialogResult.OK Then
                    MCB_VAD模型文件.Text = d.FileName
                Else
                    MCB_VAD模型文件.SelectedIndex = -1
                End If
            End Using
        Finally
            正在选择路径 = False
        End Try
    End Sub

    Private Sub MB_输出文件另存为_Click(sender As Object, e As EventArgs) Handles MB_输出文件另存为.Click
        Dim format = 获取输出格式()
        Dim currentOutput = MTB_输出文件.Text.Trim()
        Using d As New SaveFileDialog With {
            .Filter = 获取输出过滤器(format),
            .FileName = If(currentOutput <> "", Path.GetFileName(currentOutput), 获取默认输出文件名())
        }
            Dim outputDir = If(currentOutput = "", "", Path.GetDirectoryName(currentOutput))
            If outputDir <> "" AndAlso Directory.Exists(outputDir) Then d.InitialDirectory = outputDir
            If d.ShowDialog(Me) = DialogResult.OK Then
                MTB_输出文件.Text = d.FileName
                上次自动输出文件 = ""
            End If
        End Using
    End Sub

    Private Sub MB_重置页面_Click(sender As Object, e As EventArgs) Handles MB_重置页面.Click
        正在选择路径 = True
        Try
            MCB_输入文件.SelectedIndex = -1
            MCB_输入文件.Text = ""
            MCB_选择模型文件.SelectedIndex = -1
            MCB_选择模型文件.Text = ""
            MCB_VAD模型文件.SelectedIndex = -1
            MCB_VAD模型文件.Text = ""
            MTB_输出文件.Text = ""
            MTB_音频流索引.Text = "0"
            MTB_GPU设备索引.Text = "0"
            MCB_翻译成英语.Checked = False
            设置下拉框选中值(MCB_GPU加速选项, "自动")
            设置下拉框选中值(MCB_要识别的语言, "auto")
            设置下拉框选中值(MCB_输出类型, "srt")
            ETB_音频缓存长度.Value = 20
            ETB_字幕片段最大字符数.Value = 42
            ETB_VAD阈值.Value = 0.5
            ETB_VAD最短语音持续时间.Value = 0.1
            ETB_VAD最短静音持续时间.Value = 0.5
            上次自动输出文件 = ""
        Finally
            正在选择路径 = False
        End Try
        ExFloatingTip(MB_重置页面, "已重置页面", 1200)
    End Sub

    Private Sub MB_添加到编码队列_Click(sender As Object, e As EventArgs) Handles MB_添加到编码队列.Click
        If Not 验证任务参数() Then Exit Sub

        Dim input = MCB_输入文件.Text.Trim()
        Dim output = MTB_输出文件.Text.Trim()
        Dim args = 构造FFmpeg参数(input, output)
        Dim taskName = $"Whisper 生成字幕 {Path.GetFileNameWithoutExtension(output)}"

        插件管理.使用命令行添加任务到编码队列(args, taskName, output, input)
        FormMain_v6.ModernTabListControl1.SelectedIndex = 2
        ExFloatingTip(MB_添加到编码队列, "已添加到编码队列", 1200)
    End Sub

    Private Function 验证任务参数() As Boolean
        Dim input = MCB_输入文件.Text.Trim()
        If input = "" Then
            ExFloatingTip(MCB_输入文件, "请先选择输入文件", 1800)
            Return False
        End If
        If Not File.Exists(input) Then
            ExFloatingTip(MCB_输入文件, "输入文件不存在", 1800)
            Return False
        End If

        Dim model = MCB_选择模型文件.Text.Trim()
        If model = "" OrElse model = 浏览文件项 OrElse model = 打开模型下载页项 Then
            ExFloatingTip(MCB_选择模型文件, "请选择 Whisper 模型文件", 1800)
            Return False
        End If
        If Not File.Exists(model) Then
            ExFloatingTip(MCB_选择模型文件, "Whisper 模型文件不存在", 1800)
            Return False
        End If

        Dim vadModel = MCB_VAD模型文件.Text.Trim()
        If vadModel = 浏览文件项 OrElse vadModel = 打开VAD下载页项 Then vadModel = ""
        If vadModel <> "" AndAlso Not File.Exists(vadModel) Then
            ExFloatingTip(MCB_VAD模型文件, "VAD 模型文件不存在；不使用 VAD 可以留空", 2200)
            Return False
        End If

        Dim audioIndex As Integer
        If Not 验证非负整数文本(MTB_音频流索引.Text, "音频流索引", MTB_音频流索引, audioIndex, True) Then Return False

        Dim gpuIndex As Integer
        If 获取GPU模式() <> "auto" AndAlso Not 验证非负整数文本(MTB_GPU设备索引.Text, "GPU 设备索引", MTB_GPU设备索引, gpuIndex, False) Then Return False

        If 获取输出格式() = "" Then
            ExFloatingTip(MCB_输出类型, "输出类型只能是 srt、text 或 json", 1800)
            Return False
        End If

        Dim output = MTB_输出文件.Text.Trim()
        If output = "" Then
            ExFloatingTip(MTB_输出文件, "请设置输出文件", 1800)
            Return False
        End If

        Dim outputDir = Path.GetDirectoryName(output)
        If outputDir <> "" AndAlso Not Directory.Exists(outputDir) Then
            ExFloatingTip(MTB_输出文件, "输出目录不存在", 1800)
            Return False
        End If

        If String.Equals(Path.GetFullPath(input), Path.GetFullPath(output), StringComparison.OrdinalIgnoreCase) Then
            ExFloatingTip(MTB_输出文件, "输出文件不能覆盖输入文件", 2200)
            Return False
        End If

        Return True
    End Function

    Private Function 验证非负整数文本(value As String, label As String, target As Control, ByRef result As Integer, allowEmpty As Boolean) As Boolean
        Dim text = If(value, "").Trim()
        If text = "" AndAlso allowEmpty Then
            result = -1
            Return True
        End If
        If Integer.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, result) AndAlso result >= 0 Then Return True
        ExFloatingTip(target, $"{label}必须是 0 或更大的整数", 1800)
        Return False
    End Function

    Private Function 构造FFmpeg参数(input As String, output As String) As String
        Dim arg As New StringBuilder("-hide_banner -nostdin -y ")
        arg.Append("-i ").Append(引用参数(input)).Append(" "c)

        Dim audioIndex As Integer
        If 验证非负整数文本(MTB_音频流索引.Text, "音频流索引", MTB_音频流索引, audioIndex, True) AndAlso audioIndex >= 0 Then
            arg.Append("-map 0:a:").Append(audioIndex.ToString(CultureInfo.InvariantCulture)).Append(" "c)
        End If

        arg.Append("-vn -sn -dn ")
        arg.Append("-af ").Append(引用参数(构造Whisper滤镜(output))).Append(" "c)
        arg.Append("-f null -")
        Return arg.ToString()
    End Function

    Private Function 构造Whisper滤镜(output As String) As String
        Dim opts As New List(Of String) From {
            "model=" & 引用过滤器参数(MCB_选择模型文件.Text.Trim())
        }

        Dim language = If(MCB_要识别的语言.Text, "").Trim()
        If language = "" Then language = "auto"
        opts.Add("language=" & 转义过滤器值(language))
        opts.Add("translate=" & If(MCB_翻译成英语.Checked, "true", "false"))
        opts.Add("queue=" & 格式化数字(ETB_音频缓存长度.Value))

        Select Case 获取GPU模式()
            Case "true"
                opts.Add("use_gpu=true")
                opts.Add("gpu_device=" & MTB_GPU设备索引.Text.Trim())
            Case "false"
                opts.Add("use_gpu=false")
        End Select

        opts.Add("destination=" & 引用过滤器参数(output))
        opts.Add("format=" & 获取输出格式())

        Dim maxLen = CInt(Math.Round(ETB_字幕片段最大字符数.Value))
        If maxLen > 0 Then opts.Add("max_len=" & maxLen.ToString(CultureInfo.InvariantCulture))

        Dim vadModel = MCB_VAD模型文件.Text.Trim()
        If vadModel <> "" AndAlso vadModel <> 浏览文件项 AndAlso vadModel <> 打开VAD下载页项 Then
            opts.Add("vad_model=" & 引用过滤器参数(vadModel))
            opts.Add("vad_threshold=" & 格式化数字(ETB_VAD阈值.Value))
            opts.Add("vad_min_speech_duration=" & 格式化数字(ETB_VAD最短语音持续时间.Value))
            opts.Add("vad_min_silence_duration=" & 格式化数字(ETB_VAD最短静音持续时间.Value))
        End If

        Return "whisper=" & String.Join(":", opts)
    End Function

    Private Function 获取GPU模式() As String
        Select Case If(MCB_GPU加速选项.Text, "").Trim()
            Case "启用"
                Return "true"
            Case "禁用"
                Return "false"
            Case Else
                Return "auto"
        End Select
    End Function

    Private Function 获取输出格式() As String
        Select Case If(MCB_输出类型.Text, "").Trim().ToLowerInvariant()
            Case "srt"
                Return "srt"
            Case "text", "txt"
                Return "text"
            Case "json"
                Return "json"
            Case Else
                Return ""
        End Select
    End Function

    Private Function 获取输出扩展名(format As String) As String
        Select Case format
            Case "srt"
                Return ".srt"
            Case "json"
                Return ".json"
            Case Else
                Return ".txt"
        End Select
    End Function

    Private Function 获取输出过滤器(format As String) As String
        Select Case format
            Case "srt"
                Return "SRT 字幕|*.srt|所有文件|*.*"
            Case "json"
                Return "JSON 文件|*.json|所有文件|*.*"
            Case Else
                Return "文本文件|*.txt|所有文件|*.*"
        End Select
    End Function

    Private Function 获取默认输出文件名() As String
        Dim input = MCB_输入文件.Text.Trim()
        Dim baseName = If(File.Exists(input), Path.GetFileNameWithoutExtension(input), "whisper")
        Return baseName & 获取输出扩展名(获取输出格式())
    End Function

    Private Sub 生成自动输出文件名(force As Boolean)
        If 正在选择路径 Then Exit Sub
        Dim input = MCB_输入文件.Text.Trim()
        If Not File.Exists(input) Then Exit Sub

        Dim current = MTB_输出文件.Text.Trim()
        If Not force AndAlso current <> "" AndAlso Not String.Equals(current, 上次自动输出文件, StringComparison.OrdinalIgnoreCase) Then Exit Sub

        Dim format = 获取输出格式()
        If format = "" Then Exit Sub

        Dim output = Path.Combine(Path.GetDirectoryName(input), Path.GetFileNameWithoutExtension(input) & 获取输出扩展名(format))
        MTB_输出文件.Text = output
        上次自动输出文件 = output
    End Sub

    Private Shared Function 引用参数(value As String) As String
        Return $"""{If(value, "").Replace("""", "\""")}"""
    End Function

    Private Shared Function 引用过滤器参数(value As String) As String
        Return "'" & 转义过滤器值(value) & "'"
    End Function

    Private Shared Function 转义过滤器值(value As String) As String
        Return 将路径转换为FFmpeg滤镜接受的格式(If(value, "")).Replace("'", "\'")
    End Function

    Private Shared Function 格式化数字(value As Double) As String
        Return value.ToString("0.###", CultureInfo.InvariantCulture)
    End Function

    Private Sub 打开网址(url As String, combo As ModernComboBox)
        Try
            Process.Start(New ProcessStartInfo(url) With {.UseShellExecute = True})
        Catch ex As Exception
            ExFloatingTip(combo, $"打开链接失败：{ex.Message}", 2200)
        Finally
            combo.SelectedIndex = -1
            combo.Text = ""
        End Try
    End Sub

End Class
