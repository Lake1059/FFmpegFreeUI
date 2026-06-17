Imports System.Globalization
Imports System.IO
Imports System.Text
Imports System.Text.Json
Imports LakeUI

Public Class 预设管理_v6

    Private Shared ReadOnly separator As String() = {","}
    Public Const 输入占位符 As String = "<输入文件>"
    Public Const 输出占位符 As String = "<输出文件>"
    Public Const 媒体总时长占位符 As String = "<媒体总时长>"

    Private Class 附加输出片段
        Public Property 额外输入 As New List(Of String)
        Public Property 输出前 As New List(Of String)
    End Class

    Public Shared ReadOnly Property 音频编码器排序表 As List(Of String)
        Get
            Return 音频编码器数据库_v6.全部编码器.Select(Function(x) x.私有ID).ToList()
        End Get
    End Property

    Public Shared ReadOnly Property 预设根目录 As String
        Get
            Return Path.Combine(Application.StartupPath, "Preset_v6")
        End Get
    End Property

    Public Shared Function 获取预设目录(来源 As String) As String
        Select Case If(来源, "").Trim()
            Case "开发者内置"
                Return Path.Combine(预设根目录, "BuiltIn")
            Case "从社区下载"
                Return Path.Combine(预设根目录, "Community")
            Case Else
                Return Path.Combine(预设根目录, "User")
        End Select
    End Function

    Public Shared Function 新建预设名称() As String
        Return $"预设-{Date.Now:yyyyMMdd-HHmmss}"
    End Function

    Public Shared Function 安全文件名(名称 As String) As String
        Dim 结果 = If(名称, "").Trim()
        If 结果 = "" Then 结果 = 新建预设名称()
        For Each c In Path.GetInvalidFileNameChars()
            结果 = 结果.Replace(c, "_"c)
        Next
        Return 结果
    End Function

    Public Shared Function 读取预设文件(文件路径 As String) As 预设数据_v6
        If Not File.Exists(文件路径) Then Return Nothing
        Dim 数据 = JsonSerializer.Deserialize(Of 预设数据_v6)(File.ReadAllText(文件路径), JsonSO)
        初始化空集合(数据)
        Return 数据
    End Function

    Public Shared Sub 写入预设文件(文件路径 As String, 数据 As 预设数据_v6)
        初始化空集合(数据)
        Directory.CreateDirectory(Path.GetDirectoryName(文件路径))
        File.WriteAllText(文件路径, JsonSerializer.Serialize(数据, JsonSO), Encoding.UTF8)
    End Sub

    Public Shared Sub 初始化空集合(a As 预设数据_v6)
        If a Is Nothing Then Exit Sub
        If a.视频参数_超分_直接面板 Is Nothing Then a.视频参数_超分_直接面板 = New 预设数据_v6.超分数据单片结构
        If a.视频参数_超分_滤镜叠加策略组 Is Nothing Then a.视频参数_超分_滤镜叠加策略组 = Array.Empty(Of 预设数据_v6.超分数据单片结构)()
        If a.滤镜排序系统 Is Nothing Then a.滤镜排序系统 = Array.Empty(Of 预设数据_v6.滤镜排序单片结构)()
        If a.元数据_要写入的信息 Is Nothing Then a.元数据_要写入的信息 = Array.Empty(Of 预设数据_v6.元数据单片结构)()
        If a.附件_要写入的附件 Is Nothing Then a.附件_要写入的附件 = Array.Empty(Of 预设数据_v6.附件单片结构)()
        If a.流控制_将视频参数应用于指定流 Is Nothing Then a.流控制_将视频参数应用于指定流 = Array.Empty(Of String)()
        If a.流控制_将音频参数应用于指定流 Is Nothing Then a.流控制_将音频参数应用于指定流 = Array.Empty(Of String)()
        If a.流控制_将字幕参数应用于指定流 Is Nothing Then a.流控制_将字幕参数应用于指定流 = Array.Empty(Of String)()
        If a.视频参数_烧录字幕_字幕格式优先级 Is Nothing Then a.视频参数_烧录字幕_字幕格式优先级 = New List(Of 预设数据_v6.烧字幕格式)
        If a.视频参数_烧录字幕_主要颜色 Is Nothing Then a.视频参数_烧录字幕_主要颜色 = New 预设数据_v6.烧字幕专用颜色类型
        If a.视频参数_烧录字幕_次要颜色 Is Nothing Then a.视频参数_烧录字幕_次要颜色 = New 预设数据_v6.烧字幕专用颜色类型
        If a.视频参数_烧录字幕_描边颜色 Is Nothing Then a.视频参数_烧录字幕_描边颜色 = New 预设数据_v6.烧字幕专用颜色类型
        If a.视频参数_烧录字幕_背景颜色 Is Nothing Then a.视频参数_烧录字幕_背景颜色 = New 预设数据_v6.烧字幕专用颜色类型
        迁移旧自定义滤镜字段到排序系统(a)
    End Sub

    Private Shared Sub 迁移旧自定义滤镜字段到排序系统(a As 预设数据_v6)
        If a Is Nothing Then Exit Sub
        Dim 排序 = If(a.滤镜排序系统, Array.Empty(Of 预设数据_v6.滤镜排序单片结构)()).ToList()
        If Not String.IsNullOrWhiteSpace(a.自定义参数_视频滤镜) Then
            Dim 内容 = a.自定义参数_视频滤镜.Trim()
            If Not 排序.Any(Function(x) x.滤镜标识符 = 预设数据_v6.滤镜排序单片结构.标识符枚举.自定义视频滤镜 AndAlso String.Equals(x.自定义滤镜内容, 内容, StringComparison.Ordinal)) Then
                排序.Add(New 预设数据_v6.滤镜排序单片结构 With {
                    .显示名称 = "自定义视频滤镜",
                    .是自定义滤镜 = True,
                    .允许在排序页直接编辑 = False,
                    .滤镜标识符 = 预设数据_v6.滤镜排序单片结构.标识符枚举.自定义视频滤镜,
                    .滤镜目标流类型 = 预设数据_v6.滤镜排序单片结构.流类型.视频,
                    .自定义滤镜内容 = 内容
                })
            End If
            a.自定义参数_视频滤镜 = ""
        End If
        If Not String.IsNullOrWhiteSpace(a.自定义参数_音频滤镜) Then
            Dim 内容 = a.自定义参数_音频滤镜.Trim()
            If Not 排序.Any(Function(x) x.滤镜标识符 = 预设数据_v6.滤镜排序单片结构.标识符枚举.自定义音频滤镜 AndAlso String.Equals(x.自定义滤镜内容, 内容, StringComparison.Ordinal)) Then
                排序.Add(New 预设数据_v6.滤镜排序单片结构 With {
                    .显示名称 = "自定义音频滤镜",
                    .是自定义滤镜 = True,
                    .允许在排序页直接编辑 = False,
                    .滤镜标识符 = 预设数据_v6.滤镜排序单片结构.标识符枚举.自定义音频滤镜,
                    .滤镜目标流类型 = 预设数据_v6.滤镜排序单片结构.流类型.音频,
                    .自定义滤镜内容 = 内容
                })
            End If
            a.自定义参数_音频滤镜 = ""
        End If
        a.滤镜排序系统 = 排序.ToArray()
    End Sub

    Public Shared Sub 显示预设(a As 预设数据_v6, ui As Form_v6_参数面板)
        If a Is Nothing OrElse ui Is Nothing Then Exit Sub
        Dim oldSuppress = ui.抑制自动刷新
        ui.抑制自动刷新 = True
        Try
            初始化空集合(a)
            显示输出文件设置(a, ui)
            显示解码参数(a, ui)
            显示视频编码器(a, ui)
            显示画面帧(a, ui)
            显示质量(a, ui)
            显示色彩管理(a, ui)
            显示音频参数(a, ui)
            显示剪辑(a, ui)
            显示视频帧服务器(a, ui)
            显示自定义参数(a, ui)
            显示流控制(a, ui)
            显示附加内容(a, ui)
            ui.私有界面_滤镜排序.设置排序数据(Array.Empty(Of 预设数据_v6.滤镜排序单片结构)(), False)
            If a.滤镜排序系统.Length > 0 Then
                ui.私有界面_滤镜排序.设置排序数据(a.滤镜排序系统.ToList(), False)
            End If
            同步全部内置滤镜到排序(ui, False)
            ui.私有界面_预设管理.ModernTextBox4.Text = a.预设备注
            ui.私有界面_预设管理.ModernCheckBox1.Checked = a.额外保存输出位置
            刷新参数总览(ui)
        Finally
            ui.抑制自动刷新 = oldSuppress
        End Try
    End Sub

    Public Shared Sub 储存预设(ByRef a As 预设数据_v6, ui As Form_v6_参数面板)
        If a Is Nothing Then a = New 预设数据_v6
        If ui Is Nothing Then Exit Sub
        初始化空集合(a)
        a.预设备注 = ui.私有界面_预设管理.ModernTextBox4.Text.Trim()
        a.额外保存输出位置 = ui.私有界面_预设管理.ModernCheckBox1.Checked

        储存输出文件设置(a, ui)
        储存解码参数(a, ui)
        储存视频编码器(a, ui)
        储存画面帧(a, ui)
        储存质量(a, ui)
        储存色彩管理(a, ui)
        储存音频参数(a, ui)
        储存剪辑(a, ui)
        储存视频帧服务器(a, ui)
        储存自定义参数(a, ui)
        储存流控制(a, ui)
        储存附加内容(a, ui)
        ui.私有界面_滤镜排序.刷新局部预览(a)
        a.滤镜排序系统 = ui.私有界面_滤镜排序.获取排序数据().ToArray()
    End Sub

    Public Shared Function 从面板创建预设(ui As Form_v6_参数面板) As 预设数据_v6
        Dim a As New 预设数据_v6
        储存预设(a, ui)
        Return a
    End Function

    Public Shared Sub 重置面板(ui As Form_v6_参数面板)
        If ui Is Nothing Then Exit Sub
        显示预设(New 预设数据_v6, ui)
        ui.私有界面_预设管理.ModernTextBox3.Text = ""
    End Sub

    Public Shared Function 将预设数据转换为命令行(a As 预设数据_v6, 输入文件 As String, 输出文件 As String) As String
        Return 生成命令行(a, 输入文件, 输出文件, 预设数据_v6.命令行阶段.普通单次).命令行
    End Function

    Public Shared Function 生成命令行展示文本(a As 预设数据_v6, 输入文件 As String, 输出文件 As String) As String
        Dim 阶段列表 = 生成阶段化命令行(a, 输入文件, 输出文件)
        If 阶段列表.Count = 0 Then Return ""

        Dim lines As New List(Of String)
        For Each item In 阶段列表
            If lines.Count > 0 Then lines.Add("")
            lines.Add($"{获取命令行进程名(item.阶段)} {item.命令行}")
        Next
        Return String.Join(vbCrLf, lines)
    End Function

    Private Shared Function 获取命令行进程名(阶段 As 预设数据_v6.命令行阶段) As String
        If 阶段 = 预设数据_v6.命令行阶段.FFprobe获取时长 Then Return "ffprobe"
        Return "ffmpeg"
    End Function

    Public Shared Function 生成阶段化命令行(a As 预设数据_v6,
                                  输入文件 As String,
                                  输出文件 As String,
                                  Optional 媒体总时长 As String = "") As List(Of 预设数据_v6.命令行生成结果)
        初始化空集合(a)
        Dim 结果 As New List(Of 预设数据_v6.命令行生成结果)
        If a Is Nothing Then Return 结果

        If a.剪辑区间_方法 = 预设数据_v6.剪辑方法.掐头去尾 AndAlso String.IsNullOrWhiteSpace(媒体总时长) Then
            结果.Add(生成命令行(a, 输入文件, 输出文件, 预设数据_v6.命令行阶段.FFprobe获取时长))
        End If

        If 可以生成二次编码(a) Then
            结果.Add(生成命令行(a, 输入文件, 输出文件, 预设数据_v6.命令行阶段.二次编码第一遍, 媒体总时长))
            结果.Add(生成命令行(a, 输入文件, 输出文件, 预设数据_v6.命令行阶段.二次编码第二遍, 媒体总时长))
        Else
            结果.Add(生成命令行(a, 输入文件, 输出文件, 预设数据_v6.命令行阶段.普通单次, 媒体总时长))
        End If

        Return 结果
    End Function

    Public Shared Function 生成命令行(a As 预设数据_v6,
                                  输入文件 As String,
                                  输出文件 As String,
                                  Optional 阶段 As 预设数据_v6.命令行阶段 = 预设数据_v6.命令行阶段.普通单次,
                                  Optional 媒体总时长 As String = "") As 预设数据_v6.命令行生成结果
        Dim 结果 As New 预设数据_v6.命令行生成结果 With {.阶段 = 阶段}
        If a Is Nothing Then Return 结果
        初始化空集合(a)

        If a.自定义参数_完全自己写.Trim() <> "" Then
            结果.命令行 = a.自定义参数_完全自己写.
                Replace("<InputFile>", 输入文件).
                Replace("<OutputFile>", 输出文件).
                Replace(输入占位符, 输入文件).
                Replace(输出占位符, 输出文件)
            Return 结果
        End If

        If 阶段 = 预设数据_v6.命令行阶段.FFprobe获取时长 Then
            结果.命令行 = $"-v error -show_entries format=duration -of default=noprint_wrappers=1:nokey=1 {Q(输入文件)}"
            结果.说明 = "获取媒体总时长"
            Return 结果
        End If

        Dim 前置 As New List(Of String)
        Dim 输入后 As New List(Of String)
        Dim 额外输入 As New List(Of String)
        Dim 输出前 As New List(Of String)
        Dim 输出后 As New List(Of String)

        AddRaw(前置, a.自定义参数_开头参数)
        AddRaw(输入后, a.自定义参数_之前参数)
        AddRaw(输出前, a.自定义参数_之后参数)
        AddRaw(输出后, a.自定义参数_最后参数)
        AddRaw(输入后, 生成剪辑输入参数(a, 阶段, 媒体总时长, 结果))
        AddRaw(输入后, 生成解码参数(a))
        Dim 主输入参数 = 生成主输入参数(a, 输入文件)

        If (阶段 = 预设数据_v6.命令行阶段.二次编码第一遍 OrElse 阶段 = 预设数据_v6.命令行阶段.二次编码第二遍) AndAlso Not 当前编码器支持二次编码(a) Then
            结果.说明 = "当前视频编码器未标记支持二次编码，已按普通单次生成。"
            阶段 = 预设数据_v6.命令行阶段.普通单次
            结果.阶段 = 阶段
        End If
        If (阶段 = 预设数据_v6.命令行阶段.二次编码第一遍 OrElse 阶段 = 预设数据_v6.命令行阶段.二次编码第二遍) AndAlso Not 二次编码基础码率有效(a) Then
            结果.说明 = "二次编码需要填写基础码率，已按普通单次生成。"
            阶段 = 预设数据_v6.命令行阶段.普通单次
            结果.阶段 = 阶段
        End If

        Dim 滤镜图 = 生成滤镜图(a, 阶段 = 预设数据_v6.命令行阶段.二次编码第一遍, 输入文件)
        结果.滤镜图 = 滤镜图.滤镜图
        结果.映射参数 = 滤镜图.映射参数
        If 滤镜图.滤镜图 <> "" Then
            输出前.Add($"-filter_complex {Q(滤镜图.滤镜图)}")
        End If
        AddRaw(输出前, 滤镜图.映射参数)

        Dim 编码参数 = 生成编码参数(a, 阶段, 滤镜图.编码视频选择器, 滤镜图.编码音频选择器, 滤镜图.视频输出来自滤镜, 滤镜图.音频输出来自滤镜, 输入文件)
        AddRaw(输出前, 编码参数)
        If 阶段 = 预设数据_v6.命令行阶段.二次编码第一遍 Then 输出前.Add("-an")

        Dim 附加片段 = 生成元数据章节附件片段(a, 滤镜图.编码视频选择器.Count)
        额外输入.AddRange(附加片段.额外输入)
        输出前.AddRange(附加片段.输出前)
        AddRaw(输出前, a.自定义参数_视频参数)
        AddRaw(输出前, a.自定义参数_音频参数)

        Dim 丢弃输出 = 阶段 = 预设数据_v6.命令行阶段.二次编码第一遍 OrElse a.输出_输出文件参数使用方法 = 预设数据_v6.输出文件参数使用方法.声明丢弃输出
        Dim 输出目标 = If(丢弃输出, "NUL", 输出文件)
        Dim 命令 As New List(Of String)
        命令.AddRange(前置.Where(Function(x) x <> ""))
        命令.Add("-hide_banner")
        命令.Add("-y")
        命令.AddRange(输入后.Where(Function(x) x <> ""))
        命令.AddRange(主输入参数.Where(Function(x) x <> ""))
        命令.AddRange(额外输入.Where(Function(x) x <> ""))
        命令.AddRange(输出前.Where(Function(x) x <> ""))
        If 丢弃输出 Then
            命令.Add("-f null")
            命令.Add("NUL")
        Else
            If a.输出_输出文件参数使用方法 <> 预设数据_v6.输出文件参数使用方法.不附加 Then
                命令.Add(Q(输出目标))
            End If
        End If
        命令.AddRange(输出后.Where(Function(x) x <> ""))
        结果.命令行 = String.Join(" ", 命令.Where(Function(x) Not String.IsNullOrWhiteSpace(x)))
        Return 结果
    End Function

    Public Shared Function 生成排序页预览(a As 预设数据_v6) As String
        Dim 图 = 生成滤镜图(a)
        Dim 片段 As New List(Of String)
        If 图.滤镜图 <> "" Then 片段.Add($"-filter_complex {Q(图.滤镜图)}")
        If 图.映射参数 <> "" Then 片段.Add(图.映射参数)
        Return String.Join(" ", 片段)
    End Function

    Public Shared Sub 显示参数总览(MTB As ModernTextBox, a As 预设数据_v6)
        If MTB Is Nothing Then Exit Sub
        MTB.EnableSyntaxHighlight = False
        初始化空集合(a)

        Dim sb As New StringBuilder

        If Not String.IsNullOrWhiteSpace(a.自定义参数_完全自己写) Then
            添加总览文本行(sb, "正在使用完全自己写参数模式，其他参数均不会生效")
            添加总览文本行(sb, "完全自己写参数：" & a.自定义参数_完全自己写)
            设置参数总览文本(MTB, sb.ToString())
            Exit Sub
        End If

        If a.视频参数_视频帧服务器_使用AviSynth Then
            添加总览文本行(sb, "正在使用 AviSynth，请不要直接将此任务的命令行直接复制到其他地方运行，因为与任务对应的脚本文件还未生成，至少运行一次任务再尝试外部操作！")
            If Not String.IsNullOrWhiteSpace(a.视频参数_视频帧服务器_avs脚本文件) Then
                添加总览文本行(sb, "AviSynth 指定模板：" & a.视频参数_视频帧服务器_avs脚本文件)
            Else
                添加总览文本行(sb, "没有指定 AviSynth 模板")
            End If
        End If
        If a.视频参数_视频帧服务器_使用VapourSynth Then
            添加总览文本行(sb, "正在使用 VapourSynth，请不要直接将此任务的命令行直接复制到其他地方运行，因为与任务对应的脚本文件还未生成，至少运行一次任务再尝试外部操作！")
            If Not String.IsNullOrWhiteSpace(a.视频参数_视频帧服务器_vpy脚本文件) Then
                添加总览文本行(sb, "VapourSynth 指定模板：" & a.视频参数_视频帧服务器_vpy脚本文件)
            Else
                添加总览文本行(sb, "没有指定 VapourSynth 模板")
            End If
        End If

        If String.IsNullOrWhiteSpace(a.输出容器) Then
            添加总览文本行(sb, "警告：没有指定输出后缀/输出容器，常规输出文件无法生成正确扩展名")
        Else
            添加总览文本行(sb, "输出容器：" & a.输出容器)
        End If
        If a.输出_输出文件参数使用方法 <> 预设数据_v6.输出文件参数使用方法.正常使用 Then 添加总览文本行(sb, "输出文件参数使用方法：" & 格式化枚举名称(a.输出_输出文件参数使用方法))
        If a.输出_自动命名选项 <> 预设数据_v6.自动命名选项.附加_递增时间戳 Then 添加总览文本行(sb, "自动命名方式：" & 格式化枚举名称(a.输出_自动命名选项))
        添加总览文本行(sb, "输出文件开头文本：" & a.输出命名_开头文本)
        添加总览文本行(sb, "输出文件替代文本：" & a.输出命名_替代文本)
        添加总览文本行(sb, "输出文件结尾文本：" & a.输出命名_结尾文本)
        If a.输出命名_保留创建时间 Then 添加总览文本行(sb, "输出命名：保留创建时间")
        If a.输出命名_保留修改时间 Then 添加总览文本行(sb, "输出命名：保留修改时间")
        If a.输出命名_保留访问时间 Then 添加总览文本行(sb, "输出命名：保留访问时间")
        If a.额外保存输出位置 Then 添加总览文本行(sb, "额外保存输出位置：启用")
        添加总览文本行(sb, "输出位置：" & a.输出位置)

        添加总览文本行(sb, "解码器：" & a.解码参数_解码器)
        添加总览文本行(sb, "CPU 解码线程数：" & a.解码参数_CPU解码线程数)
        添加总览文本行(sb, "解码数据格式：" & a.解码参数_解码数据格式)
        If Not String.IsNullOrWhiteSpace(a.解码参数_指定硬件的参数名) Then
            If Not String.IsNullOrWhiteSpace(a.解码参数_指定硬件的参数) Then
                添加总览文本行(sb, "指定解码硬件参数：-" & a.解码参数_指定硬件的参数名.TrimStart("-"c) & " " & a.解码参数_指定硬件的参数)
            Else
                添加总览文本行(sb, "必须指定解码硬件的参数，那不是选了就能用的")
            End If
        End If

        If a.视频参数_编码器_类型 <> 预设数据_v6.视频编码器类型.未选择 Then 添加总览文本行(sb, "视频编码器类型：" & 格式化枚举名称(a.视频参数_编码器_类型))
        添加总览文本行(sb, "视频编码类别：" & a.视频参数_编码器_分类名称)
        添加总览文本行(sb, "视频编码器：" & a.视频参数_编码器_具体编码)
        添加总览文本行(sb, "图片编码器质量：" & a.视频参数_编码器_图片编码器质量值)
        添加总览文本行(sb, "视频编码预设：" & a.视频参数_编码器_编码预设)
        添加总览文本行(sb, "视频编码配置文件：" & a.视频参数_编码器_配置文件)
        添加总览文本行(sb, "视频编码场景优化：" & a.视频参数_编码器_场景优化)
        添加总览文本行(sb, "视频编码 GPU：" & a.视频参数_编码器_gpu)
        添加总览文本行(sb, "视频编码线程数：" & a.视频参数_编码器_threads)

        添加总览文本行(sb, "视频分辨率：" & a.视频参数_分辨率)
        添加总览文本行(sb, "视频分辨率自动计算：宽 = " & a.视频参数_分辨率自动计算_宽度)
        添加总览文本行(sb, "视频分辨率自动计算：高 = " & a.视频参数_分辨率自动计算_高度)
        添加总览文本行(sb, "画面裁剪：" & a.视频参数_分辨率_裁剪滤镜参数)
        添加总览文本行(sb, "输出帧率：" & a.视频参数_帧速率)

        If 已设置(a.视频参数_抽帧_max, a.视频参数_抽帧_keep, a.视频参数_抽帧_hi, a.视频参数_抽帧_lo, a.视频参数_抽帧_frac) Then
            添加总览文本行(sb, "抽帧：" & JoinNamed("；", ("max", a.视频参数_抽帧_max), ("keep", a.视频参数_抽帧_keep), ("hi", a.视频参数_抽帧_hi), ("lo", a.视频参数_抽帧_lo), ("frac", a.视频参数_抽帧_frac)))
        End If

        If Not String.IsNullOrWhiteSpace(a.视频参数_插帧_目标帧率) Then
            添加总览文本行(sb, "插帧目标帧率：" & a.视频参数_插帧_目标帧率)
            添加总览文本行(sb, "插帧模式：" & a.视频参数_插帧_插帧模式)
            添加总览文本行(sb, "运动估计模式：" & a.视频参数_插帧_运动估计模式)
            添加总览文本行(sb, "运动估计算法：" & a.视频参数_插帧_运动估计算法)
            添加总览文本行(sb, "运动补偿模式：" & a.视频参数_插帧_运动补偿模式)
            If a.视频参数_插帧_可变块大小的运动补偿 Then 添加总览文本行(sb, "插帧：已启用可变块大小运动补偿")
            添加总览文本行(sb, "插帧块大小：" & a.视频参数_插帧_块大小)
            添加总览文本行(sb, "插帧搜索范围：" & a.视频参数_插帧_搜索范围)
            添加总览文本行(sb, "场景变化检测强度：" & a.视频参数_插帧_场景变化检测强度)
        End If

        If Not String.IsNullOrWhiteSpace(a.视频参数_动态模糊_连续混合帧数) Then
            添加总览文本行(sb, "动态模糊连续混合帧数：" & a.视频参数_动态模糊_连续混合帧数)
            添加总览文本行(sb, "动态模糊每帧权重：" & a.视频参数_动态模糊_每帧权重)
            添加总览文本行(sb, "动态模糊输出缩放系数：" & a.视频参数_动态模糊_输出缩放系数)
            添加总览文本行(sb, "动态模糊处理颜色平面：" & a.视频参数_动态模糊_处理颜色平面)
        End If

        If a.视频参数_超分_滤镜叠加策略组 IsNot Nothing AndAlso a.视频参数_超分_滤镜叠加策略组.Length > 0 Then
            Dim 超分序号 = 1
            For Each 单片 In a.视频参数_超分_滤镜叠加策略组
                Dim 文本 = 格式化超分单片(单片)
                If 文本 <> "" Then
                    添加总览文本行(sb, $"超分 {超分序号}：{文本}")
                    超分序号 += 1
                End If
            Next
        ElseIf 超分单片有设置(a.视频参数_超分_直接面板) Then
            添加总览文本行(sb, "超分：" & 格式化超分单片(a.视频参数_超分_直接面板))
        End If

        If a.视频参数_降噪_方式 <> 预设数据_v6.降噪方式.未选择 Then
            添加总览文本行(sb, "降噪方式：" & 格式化枚举名称(a.视频参数_降噪_方式))
            添加总览文本行(sb, "降噪参数1：" & a.视频参数_降噪_参数1)
            添加总览文本行(sb, "降噪参数2：" & a.视频参数_降噪_参数2)
            添加总览文本行(sb, "降噪参数3：" & a.视频参数_降噪_参数3)
            添加总览文本行(sb, "降噪参数4：" & a.视频参数_降噪_参数4)
        End If
        If a.视频参数_锐化_方式 <> 预设数据_v6.锐化方式.未选择 Then
            添加总览文本行(sb, "锐化方式：" & 格式化枚举名称(a.视频参数_锐化_方式))
            添加总览文本行(sb, "锐化参数1：" & a.视频参数_锐化_参数1)
            添加总览文本行(sb, "锐化参数2：" & a.视频参数_锐化_参数2)
            添加总览文本行(sb, "锐化参数3：" & a.视频参数_锐化_参数3)
        End If
        If a.视频参数_胶片颗粒_方式 <> 预设数据_v6.胶片颗粒方式.未选择 Then
            添加总览文本行(sb, "胶片颗粒方式：" & 格式化枚举名称(a.视频参数_胶片颗粒_方式))
            添加总览文本行(sb, "胶片颗粒参数1：" & a.视频参数_胶片颗粒_参数1)
            添加总览文本行(sb, "胶片颗粒参数2：" & a.视频参数_胶片颗粒_参数2)
            添加总览文本行(sb, "胶片颗粒参数3：" & a.视频参数_胶片颗粒_参数3)
            添加总览文本行(sb, "胶片颗粒参数4：" & a.视频参数_胶片颗粒_参数4)
        End If
        If a.视频参数_平滑断层_方式 <> 预设数据_v6.平滑断层方式.未选择 Then
            添加总览文本行(sb, "平滑断层方式：" & 格式化枚举名称(a.视频参数_平滑断层_方式))
            添加总览文本行(sb, "平滑断层参数1：" & a.视频参数_平滑断层_参数1)
            添加总览文本行(sb, "平滑断层参数2：" & a.视频参数_平滑断层_参数2)
            添加总览文本行(sb, "平滑断层参数3：" & a.视频参数_平滑断层_参数3)
            添加总览文本行(sb, "平滑断层参数4：" & a.视频参数_平滑断层_参数4)
        End If
        If a.视频参数_处理扫描方式 <> 预设数据_v6.扫描方式.未选择 Then 添加总览文本行(sb, "扫描方式：" & 格式化枚举名称(a.视频参数_处理扫描方式))
        If a.视频参数_画面翻转_角度翻转 <> 预设数据_v6.画面翻转角度.未选择 Then 添加总览文本行(sb, "角度翻转：" & 格式化枚举名称(a.视频参数_画面翻转_角度翻转))
        If a.视频参数_画面翻转_镜像翻转 <> 预设数据_v6.画面翻转镜像.未选择 Then 添加总览文本行(sb, "镜像翻转：" & 格式化枚举名称(a.视频参数_画面翻转_镜像翻转))

        If a.视频参数_烧录字幕_滤镜选择 <> 预设数据_v6.烧字幕滤镜.未选择 OrElse Not String.IsNullOrWhiteSpace(a.视频参数_烧录字幕_自己写滤镜取代所有设置) Then
            Dim 字幕 As New List(Of String)
            If a.视频参数_烧录字幕_滤镜选择 <> 预设数据_v6.烧字幕滤镜.未选择 Then 字幕.Add("滤镜：" & 格式化枚举名称(a.视频参数_烧录字幕_滤镜选择))
            If a.视频参数_烧录字幕_字幕来源是外部文件 <> 预设数据_v6.烧字幕来源.未选择 Then 字幕.Add("字幕来源：" & 格式化枚举名称(a.视频参数_烧录字幕_字幕来源是外部文件))
            If a.视频参数_烧录字幕_字幕格式优先级 IsNot Nothing Then
                Dim 字幕格式 = a.视频参数_烧录字幕_字幕格式优先级.Where(Function(x) x <> 预设数据_v6.烧字幕格式.未选择).Select(Function(x) 格式化枚举名称(x)).Where(Function(x) x <> "").ToList()
                If 字幕格式.Count > 0 Then 字幕.Add("字幕格式优先级：" & String.Join("，", 字幕格式))
            End If
            If a.视频参数_烧录字幕_外部字幕文件名 <> "" Then 字幕.Add("外部字幕文件名：" & a.视频参数_烧录字幕_外部字幕文件名)
            If a.视频参数_烧录字幕_外部字幕文件夹位置 <> "" Then 字幕.Add("外部字幕文件夹位置：" & a.视频参数_烧录字幕_外部字幕文件夹位置)
            If a.视频参数_烧录字幕_指定内嵌的流 <> "" Then 字幕.Add("指定内嵌的流：" & a.视频参数_烧录字幕_指定内嵌的流)
            If a.视频参数_烧录字幕_字体文件夹 <> "" Then 字幕.Add("字体文件夹：" & a.视频参数_烧录字幕_字体文件夹)
            If a.视频参数_烧录字幕_基本样式_名称 <> "" Then 字幕.Add("字体：" & a.视频参数_烧录字幕_基本样式_名称)
            If a.视频参数_烧录字幕_基本样式_大小 > 0 Then 字幕.Add("大小：" & a.视频参数_烧录字幕_基本样式_大小.ToString(CultureInfo.InvariantCulture))
            If a.视频参数_烧录字幕_基本样式_粗体 Then 字幕.Add("粗体")
            If a.视频参数_烧录字幕_基本样式_斜体 Then 字幕.Add("斜体")
            If a.视频参数_烧录字幕_基本样式_下划线 Then 字幕.Add("下划线")
            If a.视频参数_烧录字幕_基本样式_删除线 Then 字幕.Add("删除线")
            If a.视频参数_烧录字幕_边框样式 <> 预设数据_v6.烧字幕边框样式.未选择 Then 字幕.Add("边框样式：" & 格式化枚举名称(a.视频参数_烧录字幕_边框样式))
            If a.视频参数_烧录字幕_描边宽度 <> "" Then 字幕.Add("描边宽度：" & a.视频参数_烧录字幕_描边宽度)
            If a.视频参数_烧录字幕_阴影距离 <> "" Then 字幕.Add("阴影距离：" & a.视频参数_烧录字幕_阴影距离)
            添加字幕颜色总览(字幕, "主要颜色", a.视频参数_烧录字幕_主要颜色)
            添加字幕颜色总览(字幕, "次要颜色", a.视频参数_烧录字幕_次要颜色)
            添加字幕颜色总览(字幕, "描边颜色", a.视频参数_烧录字幕_描边颜色)
            添加字幕颜色总览(字幕, "背景颜色", a.视频参数_烧录字幕_背景颜色)
            If a.视频参数_烧录字幕_对齐方位 <> 预设数据_v6.烧字幕对齐方位.未选择 Then 字幕.Add("对齐方位：" & 格式化枚举名称(a.视频参数_烧录字幕_对齐方位))
            If a.视频参数_烧录字幕_垂直边距 <> "" Then 字幕.Add("垂直边距：" & a.视频参数_烧录字幕_垂直边距)
            If a.视频参数_烧录字幕_左边距 <> "" Then 字幕.Add("左边距：" & a.视频参数_烧录字幕_左边距)
            If a.视频参数_烧录字幕_右边距 <> "" Then 字幕.Add("右边距：" & a.视频参数_烧录字幕_右边距)
            If a.视频参数_烧录字幕_字距 <> "" Then 字幕.Add("字距：" & a.视频参数_烧录字幕_字距)
            If a.视频参数_烧录字幕_行距 <> "" Then 字幕.Add("行距：" & a.视频参数_烧录字幕_行距)
            If a.视频参数_烧录字幕_补充样式 <> "" Then 字幕.Add("补充样式：" & a.视频参数_烧录字幕_补充样式)
            If a.视频参数_烧录字幕_自己写滤镜取代所有设置 <> "" Then 字幕.Add("自己写滤镜：" & a.视频参数_烧录字幕_自己写滤镜取代所有设置)
            If 字幕.Count > 0 Then 添加总览文本行(sb, "烧录字幕：" & String.Join("；", 字幕))
        End If

        If a.视频参数_比特率_控制方式 <> 预设数据_v6.视频全局质量控制方式.未选择 Then 添加总览文本行(sb, "质量/比特率控制方法：" & 格式化质量控制方式(a.视频参数_比特率_控制方式))
        If a.视频参数_比特率_控制方式 = 预设数据_v6.视频全局质量控制方式.TPE AndAlso 全局质量参数已设置(a) Then
            添加总览文本行(sb, "警告：二次编码不兼容 CRF/CQ/QP/global_quality 等全局质量参数；实际命令会忽略这些参数，请使用基础码率")
        End If
        If a.视频参数_比特率_控制方式 = 预设数据_v6.视频全局质量控制方式.TPE AndAlso Not 二次编码基础码率有效(a) Then
            添加总览文本行(sb, "警告：二次编码需要填写基础码率；未填写时会按普通单次编码生成命令")
        End If
        If Not String.IsNullOrWhiteSpace(a.视频参数_质量控制_参数名) Then
            Dim 参数名 = a.视频参数_质量控制_参数名.TrimStart("-"c)
            If Not String.IsNullOrWhiteSpace(a.视频参数_质量控制_值) Then
                添加总览文本行(sb, "质量控制参数：" & 参数名 & " = " & a.视频参数_质量控制_值)
            Else
                添加总览文本行(sb, "质量控制参数：" & 参数名 & "（未填写值）")
            End If
        ElseIf Not String.IsNullOrWhiteSpace(a.视频参数_质量控制_值) Then
            添加总览文本行(sb, "质量控制值：" & a.视频参数_质量控制_值)
        End If
        添加总览文本行(sb, "基础码率：" & a.视频参数_比特率_基础)
        添加总览文本行(sb, "最低码率：" & a.视频参数_比特率_最低值)
        添加总览文本行(sb, "最高码率：" & a.视频参数_比特率_最高值)
        添加总览文本行(sb, "缓冲区大小：" & a.视频参数_比特率_缓冲区)
        添加总览文本行(sb, "进阶质量控制：" & a.视频参数_质量控制_进阶参数集)

        添加总览文本行(sb, "像素格式：" & a.视频参数_色彩管理_像素格式)
        添加总览文本行(sb, "预先转换像素格式：" & a.视频参数_色彩管理_像素格式预先转换)
        添加总览文本行(sb, "色彩转换滤镜：" & a.视频参数_色彩管理_滤镜选择)
        添加总览文本行(sb, "矩阵系数 & 颜色格式：" & a.视频参数_色彩管理_矩阵系数)
        添加总览文本行(sb, "色域：" & a.视频参数_色彩管理_色域)
        添加总览文本行(sb, "传输特性：" & a.视频参数_色彩管理_传输特性)
        添加总览文本行(sb, "色彩范围：" & a.视频参数_色彩管理_范围)
        添加总览文本行(sb, "色调映射算法：" & a.视频参数_色彩管理_色调映射算法)
        添加总览文本行(sb, "色彩管理处理方式：" & a.视频参数_色彩管理_处理方式)
        If a.视频参数_色彩管理_启用调整亮度 Then 添加总览文本行(sb, "亮度调整：" & If(a.视频参数_色彩管理_亮度 = "", "已启用", a.视频参数_色彩管理_亮度))
        If a.视频参数_色彩管理_启用调整对比度 Then 添加总览文本行(sb, "对比度调整：" & If(a.视频参数_色彩管理_对比度 = "", "已启用", a.视频参数_色彩管理_对比度))
        If a.视频参数_色彩管理_启用调整饱和度 Then 添加总览文本行(sb, "饱和度调整：" & If(a.视频参数_色彩管理_饱和度 = "", "已启用", a.视频参数_色彩管理_饱和度))
        If a.视频参数_色彩管理_启用调整伽马 Then 添加总览文本行(sb, "伽马调整：" & If(a.视频参数_色彩管理_伽马 = "", "已启用", a.视频参数_色彩管理_伽马))

        添加总览文本行(sb, "音频编码器：" & 获取音频编码器总览显示名(a.音频参数_编码器_具体编码))
        添加总览文本行(sb, "音频比特率：" & a.音频参数_比特率)
        If Not String.IsNullOrWhiteSpace(a.音频参数_质量参数名) Then 添加总览文本行(sb, "音频质量控制：" & a.音频参数_质量参数名 & If(a.音频参数_质量值 = "", "（未填写值）", "=" & a.音频参数_质量值))
        If String.IsNullOrWhiteSpace(a.音频参数_质量参数名) AndAlso Not String.IsNullOrWhiteSpace(a.音频参数_质量值) Then 添加总览文本行(sb, "音频质量值：" & a.音频参数_质量值)
        添加总览文本行(sb, "声道布局：" & a.音频参数_声道数)
        添加总览文本行(sb, "音频位深度：" & a.音频参数_位深度)
        添加总览文本行(sb, "采样率：" & a.音频参数_采样率)
        If a.音频参数_响度标准化_启用调整目标响度 Then 添加总览文本行(sb, "响度标准化目标：" & If(a.音频参数_响度标准化_目标响度 = "", "已启用", a.音频参数_响度标准化_目标响度))
        If a.音频参数_响度标准化_启用调整动态范围 Then 添加总览文本行(sb, "响度动态范围：" & If(a.音频参数_响度标准化_动态范围 = "", "已启用", a.音频参数_响度标准化_动态范围))
        If a.音频参数_响度标准化_启用调整峰值电平 Then 添加总览文本行(sb, "响度峰值电平：" & If(a.音频参数_响度标准化_峰值电平 = "", "已启用", a.音频参数_响度标准化_峰值电平))

        If a.剪辑区间_方法 <> 预设数据_v6.剪辑方法.未知 Then 添加总览文本行(sb, "剪辑区间方法：" & 格式化剪辑方法(a.剪辑区间_方法))
        If a.剪辑区间_方法 = 预设数据_v6.剪辑方法.未知 AndAlso (a.剪辑区间_入点 <> "" OrElse a.剪辑区间_出点 <> "") Then 添加总览文本行(sb, "警告：指定了剪辑范围却没有指定剪辑方法，不会进行剪辑")
        添加总览文本行(sb, "剪辑入点：" & a.剪辑区间_入点)
        添加总览文本行(sb, "剪辑出点：" & a.剪辑区间_出点)
        If Not String.IsNullOrWhiteSpace(a.剪辑区间_向前解码多久秒) Then 添加总览文本行(sb, "快速响应的精剪向前解码 " & a.剪辑区间_向前解码多久秒 & " 秒")

        添加总览文本行(sb, "自定义开头参数：" & a.自定义参数_开头参数)
        添加总览文本行(sb, "自定义之前参数：" & a.自定义参数_之前参数)
        添加总览文本行(sb, "自定义视频滤镜：" & a.自定义参数_视频滤镜)
        添加总览文本行(sb, "自定义音频滤镜：" & a.自定义参数_音频滤镜)
        添加总览文本行(sb, "自定义视频参数：" & a.自定义参数_视频参数)
        添加总览文本行(sb, "自定义音频参数：" & a.自定义参数_音频参数)
        添加总览文本行(sb, "自定义之后参数：" & a.自定义参数_之后参数)
        添加总览文本行(sb, "自定义最后参数：" & a.自定义参数_最后参数)

        If a.流控制_将视频参数应用于指定流.Length > 0 Then 添加总览文本行(sb, "应用视频参数到流：" & 格式化字符串数组(a.流控制_将视频参数应用于指定流))
        If a.流控制_启用保留其他视频流 Then 添加总览文本行(sb, "已选择保留其他视频流")
        If a.流控制_将音频参数应用于指定流.Length > 0 Then 添加总览文本行(sb, "应用音频参数到流：" & 格式化字符串数组(a.流控制_将音频参数应用于指定流))
        If a.流控制_启用保留其他音频流 Then 添加总览文本行(sb, "已选择保留其他音频流")
        If a.流控制_将字幕参数应用于指定流.Length > 0 Then
            添加总览文本行(sb, "使用这些文件的这些字幕：" & 格式化字符串数组(a.流控制_将字幕参数应用于指定流))
            If a.流控制_如何操作指定的字幕 > 0 Then 添加总览文本行(sb, "字幕操作：" & 格式化字幕流操作(a.流控制_如何操作指定的字幕))
        End If
        If a.流控制_启用保留其他字幕流 Then 添加总览文本行(sb, "已选择保留其他字幕流")
        If a.流控制_自动混流SRT Then 添加总览文本行(sb, "自动混流同名 SRT 字幕文件")
        If a.流控制_自动混流ASS Then 添加总览文本行(sb, "自动混流同名 ASS 字幕文件")
        If a.流控制_自动混流SSA Then 添加总览文本行(sb, "自动混流同名 SSA 字幕文件")
        If a.流控制_自动混流的字幕转为MOVTEXT Then 添加总览文本行(sb, "自动混流的字幕转为 mov_text 编码")
        If a.流控制_元数据选项 > 0 Then 添加总览文本行(sb, "元数据选项：" & 格式化元数据选项(a.流控制_元数据选项))
        If a.流控制_章节选项 > 0 Then 添加总览文本行(sb, "章节选项：" & 格式化章节选项(a.流控制_章节选项))
        If a.流控制_附件选项 > 0 Then 添加总览文本行(sb, "附件选项：" & 格式化附件选项(a.流控制_附件选项))

        Dim 元数据文本 = 格式化元数据总览(a.元数据_要写入的信息)
        If 元数据文本 <> "" Then 添加总览文本行(sb, "写入元数据：" & 元数据文本)
        If a.章节_来源 <> 预设数据_v6.章节来源.未选择 Then 添加总览文本行(sb, "章节来源：" & 格式化枚举名称(a.章节_来源))
        添加总览文本行(sb, "章节文件：" & a.章节_文件路径)
        Dim 附件文本 = 格式化附件总览(a.附件_要写入的附件)
        If 附件文本 <> "" Then 添加总览文本行(sb, "写入附件：" & 附件文本)

        If a.滤镜排序系统 IsNot Nothing AndAlso a.滤镜排序系统.Length > 0 Then
            Dim index = 1
            For Each f In a.滤镜排序系统
                If f Is Nothing Then Continue For
                If Not f.是自定义滤镜 AndAlso f.滤镜标识符 = 预设数据_v6.滤镜排序单片结构.标识符枚举.未设置 Then Continue For
                Dim 名称 = If(f.显示名称 <> "", f.显示名称, 格式化枚举名称(f.滤镜标识符))
                Dim 片段 = 获取滤镜片段(a, f)
                Dim 流类型文本 = If(f.滤镜目标流类型 = 预设数据_v6.滤镜排序单片结构.流类型.未设定, "", $" [{格式化枚举名称(f.滤镜目标流类型)}]")
                添加总览文本行(sb, $"滤镜排序 {index}：{名称}{流类型文本} {片段}".Trim())
                index += 1
            Next
        End If

        设置参数总览文本(MTB, If(sb.Length > 0, sb.ToString(), "未设置参数"))
    End Sub

    Private Shared Sub 添加总览文本行(sb As StringBuilder, 文本 As String)
        If sb Is Nothing OrElse String.IsNullOrWhiteSpace(文本) Then Exit Sub
        文本 = 文本.Trim()
        If 文本.EndsWith("："c) OrElse 文本.EndsWith("="c) Then Exit Sub
        sb.AppendLine(文本)
    End Sub

    Private Shared Sub 设置参数总览文本(MTB As ModernTextBox, 文本 As String)
        If MTB Is Nothing Then Exit Sub
        MTB.Clear()
        Dim 内容 = If(文本, "").Trim()
        If 内容 = "" Then 内容 = "未设置参数"
        For Each line In 内容.Replace(vbCrLf, vbLf).Replace(vbCr, vbLf).Split({vbLf}, StringSplitOptions.None)
            MTB.AppendLine(line, 获取参数总览行颜色(line))
        Next
    End Sub

    Private Shared Function 获取参数总览行颜色(文本 As String) As Color
        Dim line = If(文本, "")
        If line.StartsWith("警告：", StringComparison.Ordinal) Then Return Color.IndianRed
        If line.Contains("没有指定输出容器", StringComparison.Ordinal) OrElse line.Contains("没有指定输出后缀", StringComparison.Ordinal) Then Return Color.IndianRed
        If line.Contains("没有指定 AviSynth 模板", StringComparison.Ordinal) OrElse line.Contains("没有指定 VapourSynth 模板", StringComparison.Ordinal) Then Return Color.IndianRed
        If line.Contains("必须指定解码硬件的参数", StringComparison.Ordinal) Then Return Color.IndianRed
        If line.Contains("要出事", StringComparison.Ordinal) Then Return Color.IndianRed
        Return Color.Empty
    End Function

    Private Shared Function 已设置(ParamArray 值() As String) As Boolean
        Return 值 IsNot Nothing AndAlso 值.Any(Function(x) Not String.IsNullOrWhiteSpace(x))
    End Function

    Private Shared Function 格式化枚举名称(value As [Enum]) As String
        If value Is Nothing Then Return ""
        Return value.ToString().Replace("_", " ")
    End Function

    Private Shared Function 格式化质量控制方式(value As 预设数据_v6.视频全局质量控制方式) As String
        Select Case value
            Case 预设数据_v6.视频全局质量控制方式.CRF : Return "CRF"
            Case 预设数据_v6.视频全局质量控制方式.VBR : Return "VBR"
            Case 预设数据_v6.视频全局质量控制方式.VBRHQ : Return "VBR HQ"
            Case 预设数据_v6.视频全局质量控制方式.CQP : Return "CQP"
            Case 预设数据_v6.视频全局质量控制方式.CBR : Return "CBR"
            Case 预设数据_v6.视频全局质量控制方式.TPE : Return "TPE"
        End Select
        Return ""
    End Function

    Private Shared Function 格式化剪辑方法(value As 预设数据_v6.剪辑方法) As String
        Select Case value
            Case 预设数据_v6.剪辑方法.粗剪 : Return "粗剪 (立即响应)"
            Case 预设数据_v6.剪辑方法.精剪从头解码 : Return "精剪 (从头解码)"
            Case 预设数据_v6.剪辑方法.精剪空降解码 : Return "精剪 (快速响应)"
            Case 预设数据_v6.剪辑方法.Trim滤镜 : Return "Trim 滤镜"
            Case 预设数据_v6.剪辑方法.掐头去尾 : Return "掐头去尾"
        End Select
        Return ""
    End Function

    Private Shared Function 获取音频编码器总览显示名(私有ID As String) As String
        Dim 文本 = If(私有ID, "").Trim()
        If 文本 = "" Then Return ""
        Dim 显示名 = 音频编码器数据库_v6.获取显示名称(文本)
        If 显示名 <> "" Then Return 显示名
        Return 文本
    End Function

    Private Shared Function 超分单片有设置(单片 As 预设数据_v6.超分数据单片结构) As Boolean
        If 单片 Is Nothing Then Return False
        Return 已设置(单片.目标宽度, 单片.目标高度, 单片.上采样算法, 单片.下采样算法, 单片.抗振铃强度, 单片.着色器文件路径)
    End Function

    Private Shared Function 格式化超分单片(单片 As 预设数据_v6.超分数据单片结构) As String
        If 单片 Is Nothing Then Return ""
        Dim 片段 As New List(Of String)
        If 单片.目标宽度 <> "" OrElse 单片.目标高度 <> "" Then 片段.Add($"目标分辨率：{单片.目标宽度} x {单片.目标高度}")
        If 单片.上采样算法 <> "" Then 片段.Add("上采样算法：" & 单片.上采样算法)
        If 单片.下采样算法 <> "" Then 片段.Add("下采样算法：" & 单片.下采样算法)
        If 单片.抗振铃强度 <> "" Then 片段.Add("抗振铃强度：" & 单片.抗振铃强度)
        If 单片.着色器文件路径 <> "" Then 片段.Add("着色器：" & 单片.着色器文件路径)
        Return String.Join("；", 片段)
    End Function

    Private Shared Sub 添加字幕颜色总览(列表 As List(Of String), 名称 As String, 颜色 As 预设数据_v6.烧字幕专用颜色类型)
        If 列表 Is Nothing OrElse 颜色 Is Nothing Then Exit Sub
        If Not 字幕颜色已设置(颜色) Then Exit Sub
        列表.Add($"{名称}：&H{颜色.A:X2}{颜色.B:X2}{颜色.G:X2}{颜色.R:X2}")
    End Sub

    Private Shared Function 字幕颜色已设置(颜色 As 预设数据_v6.烧字幕专用颜色类型) As Boolean
        If 颜色 Is Nothing Then Return False
        Return 颜色.已设置 OrElse 颜色.A <> 255 OrElse 颜色.R <> 0 OrElse 颜色.G <> 0 OrElse 颜色.B <> 0
    End Function

    Private Shared Sub 写入字幕颜色(目标 As 预设数据_v6.烧字幕专用颜色类型, 已设置 As Boolean, 颜色 As Color)
        If 目标 Is Nothing Then Exit Sub
        目标.已设置 = 已设置
        If 已设置 Then
            目标.A = 颜色.A
            目标.R = 颜色.R
            目标.G = 颜色.G
            目标.B = 颜色.B
        Else
            目标.A = 255
            目标.R = 0
            目标.G = 0
            目标.B = 0
        End If
    End Sub

    Private Shared Sub 读取字幕颜色(来源 As 预设数据_v6.烧字幕专用颜色类型, 设置动作 As Action(Of Color, Boolean))
        If 设置动作 Is Nothing Then Exit Sub
        If Not 字幕颜色已设置(来源) Then
            设置动作.Invoke(Color.Black, False)
            Exit Sub
        End If
        设置动作.Invoke(Color.FromArgb(限制颜色通道(来源.A), 限制颜色通道(来源.R), 限制颜色通道(来源.G), 限制颜色通道(来源.B)), True)
    End Sub

    Private Shared Function 限制颜色通道(value As Integer) As Integer
        Return Math.Min(255, Math.Max(0, value))
    End Function

    Private Shared Function 格式化字符串数组(值 As String()) As String
        If 值 Is Nothing Then Return ""
        Return String.Join(",", 值.Select(Function(x) If(x, "").Trim()).Where(Function(x) x <> ""))
    End Function

    Private Shared Function 格式化字幕流操作(value As Integer) As String
        Select Case value
            Case 1 : Return "复制流"
            Case 2 : Return "转为 mov_text 编码"
            Case 3 : Return "转为 srt 编码"
            Case 4 : Return "转为 ass 编码"
            Case 5 : Return "转为 ssa 编码"
        End Select
        Return value.ToString(CultureInfo.InvariantCulture)
    End Function

    Private Shared Function 格式化元数据选项(value As Integer) As String
        Select Case value
            Case 1 : Return "保留元数据"
            Case 2 : Return "清除元数据"
            Case 3 : Return "清除更多元数据"
        End Select
        Return value.ToString(CultureInfo.InvariantCulture)
    End Function

    Private Shared Function 格式化章节选项(value As Integer) As String
        Select Case value
            Case 1 : Return "保留章节"
            Case 2 : Return "清除章节"
        End Select
        Return value.ToString(CultureInfo.InvariantCulture)
    End Function

    Private Shared Function 格式化附件选项(value As Integer) As String
        Select Case value
            Case 1 : Return "保留附件"
            Case 2 : Return "清除附件"
        End Select
        Return value.ToString(CultureInfo.InvariantCulture)
    End Function

    Private Shared Function 格式化元数据总览(列表 As 预设数据_v6.元数据单片结构()) As String
        If 列表 Is Nothing Then Return ""
        Dim 片段 As New List(Of String)
        For Each item In 列表
            If item Is Nothing Then Continue For
            If String.IsNullOrWhiteSpace(item.字段) AndAlso String.IsNullOrWhiteSpace(item.值) Then Continue For
            If String.IsNullOrWhiteSpace(item.字段) Then
                片段.Add(item.值)
            ElseIf String.IsNullOrWhiteSpace(item.值) Then
                片段.Add(item.字段)
            Else
                片段.Add($"{item.字段}={item.值}")
            End If
        Next
        Return String.Join("；", 片段)
    End Function

    Private Shared Function 格式化附件总览(列表 As 预设数据_v6.附件单片结构()) As String
        If 列表 Is Nothing Then Return ""
        Dim 片段 As New List(Of String)
        For Each item In 列表
            If item Is Nothing Then Continue For
            If item.类型 = 预设数据_v6.附件单片结构.附件类型.未选择 AndAlso String.IsNullOrWhiteSpace(item.文件路径) Then Continue For
            Dim 类型 = If(item.类型 = 预设数据_v6.附件单片结构.附件类型.未选择, "", 格式化枚举名称(item.类型))
            If 类型 <> "" AndAlso item.文件路径 <> "" Then
                片段.Add($"{类型}：{item.文件路径}")
            ElseIf 类型 <> "" Then
                片段.Add(类型)
            Else
                片段.Add(item.文件路径)
            End If
        Next
        Return String.Join("；", 片段)
    End Function

    Public Shared Sub 刷新参数总览(ui As Form_v6_参数面板)
        If ui Is Nothing Then Exit Sub
        Dim a = 从面板创建预设(ui)
        显示参数总览(ui.私有界面_参数总览.ModernTextBox1, a)
        ui.私有界面_参数总览.ModernTextBox2.Text = 生成命令行展示文本(a, 输入占位符, 输出占位符)
        ui.私有界面_滤镜排序.刷新局部预览(a)
    End Sub

    Public Shared Sub 同步全部内置滤镜到排序(ui As Form_v6_参数面板, Optional 刷新 As Boolean = True)
        If ui Is Nothing Then Exit Sub
        Dim 排序页 = ui.私有界面_滤镜排序
        Dim a = 从面板创建预设但不读排序(ui)

        更新排序项(排序页, a, 预设数据_v6.滤镜排序单片结构.标识符枚举.裁剪, Not String.IsNullOrWhiteSpace(a.视频参数_分辨率_裁剪滤镜参数))
        更新排序项(排序页, a, 预设数据_v6.滤镜排序单片结构.标识符枚举.缩放, Not String.IsNullOrWhiteSpace(a.视频参数_分辨率) OrElse Not String.IsNullOrWhiteSpace(a.视频参数_分辨率自动计算_宽度) OrElse Not String.IsNullOrWhiteSpace(a.视频参数_分辨率自动计算_高度))
        更新排序项(排序页, a, 预设数据_v6.滤镜排序单片结构.标识符枚举.抽帧, Not String.IsNullOrWhiteSpace(a.视频参数_抽帧_max) OrElse Not String.IsNullOrWhiteSpace(a.视频参数_抽帧_keep) OrElse Not String.IsNullOrWhiteSpace(a.视频参数_抽帧_hi) OrElse Not String.IsNullOrWhiteSpace(a.视频参数_抽帧_lo) OrElse Not String.IsNullOrWhiteSpace(a.视频参数_抽帧_frac))
        更新排序项(排序页, a, 预设数据_v6.滤镜排序单片结构.标识符枚举.插帧, Not String.IsNullOrWhiteSpace(a.视频参数_插帧_目标帧率))
        更新排序项(排序页, a, 预设数据_v6.滤镜排序单片结构.标识符枚举.动态模糊, Not String.IsNullOrWhiteSpace(a.视频参数_动态模糊_连续混合帧数))
        更新排序项(排序页, a, 预设数据_v6.滤镜排序单片结构.标识符枚举.超分, 超分单片有设置(a.视频参数_超分_直接面板) OrElse a.视频参数_超分_滤镜叠加策略组.Length > 0)
        更新排序项(排序页, a, 预设数据_v6.滤镜排序单片结构.标识符枚举.降噪, a.视频参数_降噪_方式 <> 预设数据_v6.降噪方式.未选择)
        更新排序项(排序页, a, 预设数据_v6.滤镜排序单片结构.标识符枚举.锐化, a.视频参数_锐化_方式 <> 预设数据_v6.锐化方式.未选择)
        更新排序项(排序页, a, 预设数据_v6.滤镜排序单片结构.标识符枚举.胶片颗粒, a.视频参数_胶片颗粒_方式 <> 预设数据_v6.胶片颗粒方式.未选择)
        更新排序项(排序页, a, 预设数据_v6.滤镜排序单片结构.标识符枚举.平滑断层, a.视频参数_平滑断层_方式 <> 预设数据_v6.平滑断层方式.未选择)
        更新排序项(排序页, a, 预设数据_v6.滤镜排序单片结构.标识符枚举.扫描方式, a.视频参数_处理扫描方式 <> 预设数据_v6.扫描方式.未选择)
        更新排序项(排序页, a, 预设数据_v6.滤镜排序单片结构.标识符枚举.画面翻转, a.视频参数_画面翻转_角度翻转 <> 预设数据_v6.画面翻转角度.未选择 OrElse a.视频参数_画面翻转_镜像翻转 <> 预设数据_v6.画面翻转镜像.未选择)
        更新排序项(排序页, a, 预设数据_v6.滤镜排序单片结构.标识符枚举.烧录字幕, a.视频参数_烧录字幕_滤镜选择 <> 预设数据_v6.烧字幕滤镜.未选择 OrElse a.视频参数_烧录字幕_自己写滤镜取代所有设置 <> "")
        更新排序项(排序页, a, 预设数据_v6.滤镜排序单片结构.标识符枚举.色彩转换, a.视频参数_色彩管理_滤镜选择 <> "" OrElse a.视频参数_色彩管理_像素格式预先转换 <> "")
        更新排序项(排序页, a, 预设数据_v6.滤镜排序单片结构.标识符枚举.调色, a.视频参数_色彩管理_启用调整亮度 OrElse a.视频参数_色彩管理_启用调整对比度 OrElse a.视频参数_色彩管理_启用调整饱和度 OrElse a.视频参数_色彩管理_启用调整伽马)
        更新排序项(排序页, a, 预设数据_v6.滤镜排序单片结构.标识符枚举.音频响度标准化, a.音频参数_响度标准化_启用调整目标响度 OrElse a.音频参数_响度标准化_启用调整动态范围 OrElse a.音频参数_响度标准化_启用调整峰值电平)
        更新排序项(排序页, a, 预设数据_v6.滤镜排序单片结构.标识符枚举.音频格式转换, a.音频参数_声道数 <> "")
        更新排序项(排序页, a, 预设数据_v6.滤镜排序单片结构.标识符枚举.音频重采样, a.音频参数_采样率 <> "")

        If 刷新 Then 刷新参数总览(ui)
    End Sub

    Public Shared Sub 重置滤镜对应页面(ui As Form_v6_参数面板, 标识符 As 预设数据_v6.滤镜排序单片结构.标识符枚举)
        If ui Is Nothing Then Exit Sub
        Select Case 标识符
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.裁剪
                ui.私有界面_画面帧.ModernTextBox1.Text = ""
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.缩放
                ui.私有界面_画面帧.ModernComboBox1.Text = ""
                ui.私有界面_画面帧.ModernComboBox2.Text = ""
                ui.私有界面_画面帧.ModernComboBox3.Text = ""
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.抽帧
                ui.私有界面_画面帧.私有窗口_抽帧参数.ModernTextBox1.Text = ""
                ui.私有界面_画面帧.私有窗口_抽帧参数.ModernTextBox2.Text = ""
                ui.私有界面_画面帧.私有窗口_抽帧参数.ModernTextBox3.Text = ""
                ui.私有界面_画面帧.私有窗口_抽帧参数.ModernComboBox1.Text = ""
                ui.私有界面_画面帧.私有窗口_抽帧参数.ModernComboBox2.Text = ""
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.插帧
                With ui.私有界面_画面帧.私有窗口_插帧参数
                    .MCB_插帧总开关.Checked = False
                    .MTB_目标帧率.Text = ""
                    .MCB_插帧模式.Text = ""
                    .MCB_运动估计模式.Text = ""
                    .MCB_运动估计算法.Text = ""
                    .MCB_运动补偿模式.Text = ""
                    .MCB_可变块大小的运动补偿.Checked = False
                    .MTB_块大小.Text = ""
                    .MTB_搜索范围.Text = ""
                    .MTB_场景变化检测强度.Text = ""
                End With
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.动态模糊
                With ui.私有界面_画面帧.私有窗口_动态模糊
                    .MCB_动态模糊总开关.Checked = False
                    .MTB_连续混合帧数.Text = ""
                    .MTB_每帧的权重.Text = ""
                    .MTB_输出缩放系数.Text = ""
                    .MTB_处理哪些颜色平面.Text = ""
                End With
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.降噪
                ui.私有界面_画面帧.私有窗口_降噪.MCB_插帧总开关.Checked = False
                ui.私有界面_画面帧.私有窗口_降噪.MCB_滤镜选择.SelectedIndex = -1
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.锐化
                ui.私有界面_画面帧.私有窗口_锐化.MCB_锐化总开关.Checked = False
                ui.私有界面_画面帧.私有窗口_锐化.MCB_滤镜选择.SelectedIndex = -1
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.胶片颗粒
                ui.私有界面_画面帧.私有窗口_胶片颗粒.MCB_胶片颗粒总开关.Checked = False
                ui.私有界面_画面帧.私有窗口_胶片颗粒.MCB_滤镜选择.SelectedIndex = -1
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.平滑断层
                ui.私有界面_画面帧.私有窗口_平滑断层.MCB_平滑断层总开关.Checked = False
                ui.私有界面_画面帧.私有窗口_平滑断层.MCB_滤镜选择.SelectedIndex = -1
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.扫描方式
                ui.私有界面_画面帧.私有窗口_扫描方式.MCB_扫描方式总开关.Checked = False
                ui.私有界面_画面帧.私有窗口_扫描方式.MCB_扫描方式.SelectedIndex = -1
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.画面翻转
                ui.私有界面_画面帧.私有窗口_画面翻转.MCB_画面翻转总开关.Checked = False
                ui.私有界面_画面帧.私有窗口_画面翻转.MCB_角度翻转.SelectedIndex = -1
                ui.私有界面_画面帧.私有窗口_画面翻转.MCB_镜像翻转.SelectedIndex = -1
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.烧录字幕
                ui.私有界面_画面帧.私有窗口_烧录字幕.重置所有选项()
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.超分
                With ui.私有界面_画面帧.私有窗口_着色器超分
                    .MCB_超分总开关.Checked = False
                    .MTB_宽度.Text = ""
                    .MTB_高度.Text = ""
                    .MCB_上采样算法.Text = ""
                    .MCB_下采样算法.Text = ""
                    .MTB_抗振铃强度.Text = ""
                    .MCB_着色器文件路径.Text = ""
                    .策略组数据.Clear()
                    .刷新策略组列表()
                End With
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.色彩转换
                With ui.私有界面_色彩管理
                    .MCB_像素格式预先转换.Text = ""
                    .MCB_色彩管理_选择滤镜.Text = ""
                    .MCB_色彩管理_矩阵系数.Text = ""
                    .MCB_色彩管理_色域.Text = ""
                    .MCB_色彩管理_传输特性.Text = ""
                    .MCB_色彩管理_色彩范围.Text = ""
                    .MCB_色彩管理_色调映射算法.Text = ""
                    .MCB_色彩管理_色彩空间操作方式.Text = ""
                End With
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.调色
                With ui.私有界面_色彩管理
                    .MCB_亮度.Checked = False
                    .MCB_对比度.Checked = False
                    .MCB_饱和度.Checked = False
                    .MCB_伽马.Checked = False
                End With
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.音频响度标准化
                With ui.私有界面_音频参数
                    .MCB_目标响度.Checked = False
                    .MCB_动态范围.Checked = False
                    .MCB_峰值电平.Checked = False
                End With
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.音频格式转换
                ui.私有界面_音频参数.声道布局.Text = ""
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.音频重采样
                ui.私有界面_音频参数.MCB_采样率.Text = ""
        End Select
        刷新参数总览(ui)
    End Sub

    Private Shared Function 从面板创建预设但不读排序(ui As Form_v6_参数面板) As 预设数据_v6
        Dim a As New 预设数据_v6
        If ui Is Nothing Then Return a
        储存输出文件设置(a, ui)
        储存解码参数(a, ui)
        储存视频编码器(a, ui)
        储存画面帧(a, ui)
        储存质量(a, ui)
        储存色彩管理(a, ui)
        储存音频参数(a, ui)
        储存剪辑(a, ui)
        储存视频帧服务器(a, ui)
        储存自定义参数(a, ui)
        储存流控制(a, ui)
        储存附加内容(a, ui)
        Return a
    End Function

    Private Shared Sub 更新排序项(排序页 As Form_v6_参数面板_滤镜排序, a As 预设数据_v6, 标识符 As 预设数据_v6.滤镜排序单片结构.标识符枚举, 启用 As Boolean)
        If 启用 Then
            排序页.添加或更新内置滤镜(标识符, 获取目标流类型(标识符), 获取滤镜显示名称(标识符), 获取滤镜片段(a, New 预设数据_v6.滤镜排序单片结构 With {.滤镜标识符 = 标识符}))
        Else
            排序页.移除内置滤镜(标识符)
        End If
    End Sub

    Private Shared Function 获取目标流类型(标识符 As 预设数据_v6.滤镜排序单片结构.标识符枚举) As 预设数据_v6.滤镜排序单片结构.流类型
        Select Case 标识符
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.音频响度标准化,
                 预设数据_v6.滤镜排序单片结构.标识符枚举.音频格式转换,
                 预设数据_v6.滤镜排序单片结构.标识符枚举.音频重采样,
                 预设数据_v6.滤镜排序单片结构.标识符枚举.自定义音频滤镜
                Return 预设数据_v6.滤镜排序单片结构.流类型.音频
            Case Else
                Return 预设数据_v6.滤镜排序单片结构.流类型.视频
        End Select
    End Function

    Public Shared Function 获取滤镜显示名称(标识符 As 预设数据_v6.滤镜排序单片结构.标识符枚举) As String
        If 标识符 = 预设数据_v6.滤镜排序单片结构.标识符枚举.未设置 Then Return "未设置"
        Return 标识符.ToString()
    End Function

    Public Shared Function 获取滤镜片段(a As 预设数据_v6, item As 预设数据_v6.滤镜排序单片结构, Optional 输入文件 As String = 输入占位符) As String
        If item Is Nothing Then Return ""
        If item.是自定义滤镜 OrElse item.滤镜标识符 = 预设数据_v6.滤镜排序单片结构.标识符枚举.自定义视频滤镜 OrElse item.滤镜标识符 = 预设数据_v6.滤镜排序单片结构.标识符枚举.自定义音频滤镜 Then
            Return item.自定义滤镜内容.Trim()
        End If

        Select Case item.滤镜标识符
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.裁剪
                Return a.视频参数_分辨率_裁剪滤镜参数.Trim()
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.缩放
                Return 构造缩放滤镜(a)
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.抽帧
                Return 构造抽帧滤镜(a)
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.插帧
                Return 构造插帧滤镜(a)
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.动态模糊
                Return 构造动态模糊滤镜(a)
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.超分
                Return 构造超分滤镜(a)
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.降噪
                Return 构造降噪滤镜(a)
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.锐化
                Return 构造锐化滤镜(a)
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.胶片颗粒
                Return 构造胶片颗粒滤镜(a)
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.平滑断层
                Return 构造平滑断层滤镜(a)
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.扫描方式
                Return 构造扫描方式滤镜(a)
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.画面翻转
                Return 构造翻转滤镜(a)
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.烧录字幕
                Return 构造烧字幕滤镜(a, 输入文件)
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.色彩转换
                Return 构造色彩转换滤镜(a)
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.调色
                Return 构造调色滤镜(a)
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.音频响度标准化
                Return 构造响度滤镜(a)
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.音频格式转换
                Return If(a.音频参数_声道数 <> "", $"aformat=channel_layouts={a.音频参数_声道数}", "")
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.音频重采样
                Return If(a.音频参数_采样率 <> "", $"aresample={a.音频参数_采样率}", "")
        End Select
        Return ""
    End Function

    Private Shared Function 生成滤镜图(a As 预设数据_v6, Optional 仅视频 As Boolean = False, Optional 输入文件 As String = 输入占位符) As 滤镜图结果
        Dim 排序 = If(a.滤镜排序系统, Array.Empty(Of 预设数据_v6.滤镜排序单片结构)()).ToList()
        Dim 视频链 = 排序.Where(Function(x) x.滤镜目标流类型 = 预设数据_v6.滤镜排序单片结构.流类型.视频).
            Select(Function(x) 清理线性滤镜片段(获取滤镜片段(a, x, 输入文件))).
            Where(Function(x) x <> "").
            ToList()
        Dim 音频链 = If(仅视频, New List(Of String), 排序.Where(Function(x) x.滤镜目标流类型 = 预设数据_v6.滤镜排序单片结构.流类型.音频).
            Select(Function(x) 清理线性滤镜片段(获取滤镜片段(a, x, 输入文件))).
            Where(Function(x) x <> "").
            ToList())

        Dim 视频流 = 规范流列表(a.流控制_将视频参数应用于指定流, "v")
        Dim 音频流 = If(仅视频, New List(Of String), 规范流列表(a.流控制_将音频参数应用于指定流, "a"))
        If 视频流.Count = 0 AndAlso (视频链.Count > 0 OrElse a.视频参数_编码器_具体编码 <> "") Then 视频流.Add("0:v:0")
        If Not 仅视频 AndAlso 音频流.Count = 0 AndAlso (音频链.Count > 0 OrElse a.音频参数_编码器_具体编码 <> "") Then 音频流.Add("0:a:0")

        Dim 图段 As New List(Of String)
        Dim 映射 As New List(Of String)
        Dim 编码视频选择器 As New List(Of String)
        Dim 编码音频选择器 As New List(Of String)
        Dim 保留其他视频 = a.流控制_启用保留其他视频流
        Dim 保留其他音频 = Not 仅视频 AndAlso a.流控制_启用保留其他音频流
        Dim 保留其他字幕 = Not 仅视频 AndAlso a.流控制_启用保留其他字幕流
        Dim 视频编码器 = 视频编码器数据库_v6.获取编码器数据(a.视频参数_编码器_具体编码)
        Dim 音频编码器 = 音频编码器数据库_v6.获取编码器数据(a.音频参数_编码器_具体编码)

        Dim 字幕流 = If(仅视频, New List(Of String), 规范流列表(a.流控制_将字幕参数应用于指定流, "s"))

        If 视频链.Count > 0 OrElse Not 保留其他视频 Then
            For i = 0 To 视频流.Count - 1
                If 视频链.Count > 0 Then
                    Dim label = $"vout{i}"
                    添加线性滤镜链图段(图段, 视频流(i), 视频链, label, $"v{i}")
                    映射.Add($"-map [{label}]")
                Else
                    映射.Add($"-map {视频流(i)}")
                End If
                编码视频选择器.Add($"v:{i}")
            Next
        Else
            For Each stream In 视频流
                编码视频选择器.Add(获取保留映射输出流选择器(stream, "v"))
            Next
        End If

        If 音频链.Count > 0 OrElse Not 保留其他音频 Then
            For i = 0 To 音频流.Count - 1
                If 音频链.Count > 0 Then
                    Dim label = $"aout{i}"
                    添加线性滤镜链图段(图段, 音频流(i), 音频链, label, $"a{i}")
                    映射.Add($"-map [{label}]")
                Else
                    映射.Add($"-map {音频流(i)}")
                End If
                编码音频选择器.Add($"a:{i}")
            Next
        Else
            For Each stream In 音频流
                编码音频选择器.Add(获取保留映射输出流选择器(stream, "a"))
            Next
        End If

        If Not 保留其他字幕 Then
            For Each s In 字幕流
                映射.Add($"-map {s}")
            Next
        End If

        If 保留其他视频 Then
            映射.Add("-map 0:v?")
            If 视频链.Count > 0 Then
                For Each stream In 视频流
                    映射.Add($"-map -{stream}?")
                Next
            End If
            If 视频链.Count = 0 OrElse (视频编码器 IsNot Nothing AndAlso Not 视频编码器.是否复制流 AndAlso Not 视频编码器.是否禁用 AndAlso 视频编码器.命令行编码器名 <> "") Then
                映射.Add("-c:v copy")
            End If
        End If
        If 保留其他音频 Then
            映射.Add("-map 0:a?")
            If 音频链.Count > 0 Then
                For Each stream In 音频流
                    映射.Add($"-map -{stream}?")
                Next
            End If
            If 音频链.Count = 0 OrElse (音频编码器 IsNot Nothing AndAlso Not 音频编码器.是否复制流 AndAlso Not 音频编码器.是否禁用 AndAlso 音频编码器.命令行编码器名 <> "") Then
                映射.Add("-c:a copy")
            End If
        End If
        If 保留其他字幕 Then
            映射.Add("-map 0:s?")
            If 字幕流.Count > 0 Then
                For Each stream In 字幕流
                    映射.Add($"-map -{stream}?")
                Next
                For Each stream In 字幕流
                    映射.Add($"-map {stream}")
                Next
            End If
            映射.Add("-c:s copy")
        End If

        Dim 字幕参数 = 获取字幕编码参数(a.流控制_如何操作指定的字幕)
        If 字幕参数 <> "" AndAlso 字幕流.Count > 0 Then
            If 保留其他字幕 Then
                For Each selector In 字幕流.Select(Function(x) 获取保留映射输出流选择器(x, "s")).Distinct(StringComparer.OrdinalIgnoreCase)
                    映射.Add($"-c:{selector} {字幕参数}")
                Next
            Else
                映射.Add($"-c:s {字幕参数}")
            End If
        End If

        Return New 滤镜图结果 With {
            .滤镜图 = String.Join(";", 图段),
            .映射参数 = String.Join(" ", 映射.Distinct(StringComparer.Ordinal)),
            .编码视频选择器 = 编码视频选择器.Where(Function(x) x <> "").Distinct(StringComparer.OrdinalIgnoreCase).ToList(),
            .编码音频选择器 = 编码音频选择器.Where(Function(x) x <> "").Distinct(StringComparer.OrdinalIgnoreCase).ToList(),
            .视频输出来自滤镜 = 视频链.Count > 0,
            .音频输出来自滤镜 = 音频链.Count > 0
        }
    End Function

    Private Shared Function 清理线性滤镜片段(片段 As String) As String
        Dim result = If(片段, "").Trim()
        While result.StartsWith(","c)
            result = result.Substring(1).TrimStart()
        End While
        While result.EndsWith(","c)
            result = result.Substring(0, result.Length - 1).TrimEnd()
        End While
        Return result
    End Function

    Private Shared Sub 添加线性滤镜链图段(图段 As List(Of String), 输入标签 As String, 滤镜链 As List(Of String), 输出标签 As String, 中间标签前缀 As String)
        Dim 当前标签 = 输入标签
        For i = 0 To 滤镜链.Count - 1
            Dim 下一个标签 = If(i = 滤镜链.Count - 1, 输出标签, $"{中间标签前缀}step{i}")
            图段.Add($"[{当前标签}]{滤镜链(i)}[{下一个标签}]")
            当前标签 = 下一个标签
        Next
    End Sub

    Private Shared Function 获取保留映射输出流选择器(stream As String, 类型 As String) As String
        Dim s = If(stream, "").Trim().TrimEnd("?"c)
        Dim parts = s.Split(":"c, StringSplitOptions.RemoveEmptyEntries)
        If parts.Length >= 2 AndAlso String.Equals(parts(parts.Length - 2), 类型, StringComparison.OrdinalIgnoreCase) Then
            Dim index As Integer
            If Integer.TryParse(parts(parts.Length - 1), index) Then Return $"{类型}:{index}"
        End If
        If parts.Length >= 2 AndAlso String.Equals(parts(parts.Length - 1), 类型, StringComparison.OrdinalIgnoreCase) Then Return 类型
        Return 类型
    End Function

    Private Shared Function 生成编码参数(a As 预设数据_v6, 阶段 As 预设数据_v6.命令行阶段, 视频选择器 As List(Of String), 音频选择器 As List(Of String), 视频来自滤镜 As Boolean, 音频来自滤镜 As Boolean, 输入文件 As String) As String
        Dim parts As New List(Of String)
        Dim 编码器 = 视频编码器数据库_v6.获取编码器数据(a.视频参数_编码器_具体编码)
        If 编码器 IsNot Nothing AndAlso 编码器.命令行编码器名 <> "" Then
            If 编码器.是否禁用 Then
                parts.Add("-vn")
            Else
                添加按流视频编码参数(parts, 编码器, If(视频选择器, New List(Of String)), 视频来自滤镜)
            End If
        End If
        添加按流视频附加参数(parts, a, 阶段, If(视频选择器, New List(Of String)))

        Dim 音频 = 音频编码器数据库_v6.获取编码器数据(a.音频参数_编码器_具体编码)
        If 音频 IsNot Nothing AndAlso 阶段 <> 预设数据_v6.命令行阶段.二次编码第一遍 Then
            添加按流音频编码参数(parts, 音频, a, If(音频选择器, New List(Of String)), 音频来自滤镜)
        End If

        If 阶段 = 预设数据_v6.命令行阶段.二次编码第一遍 OrElse 阶段 = 预设数据_v6.命令行阶段.二次编码第二遍 Then
            parts.Add(If(阶段 = 预设数据_v6.命令行阶段.二次编码第一遍, "-pass 1", "-pass 2"))
            parts.Add("-passlogfile " & Q(生成二次编码日志路径(输入文件)))
        End If

        Return String.Join(" ", parts.Where(Function(x) x <> ""))
    End Function

    Private Shared Sub 添加按流视频编码参数(parts As List(Of String), 编码器 As 视频编码器数据库_v6.视频编码器数据, 选择器 As List(Of String), 来自滤镜 As Boolean)
        Dim targets = 规范输出流选择器列表(选择器)
        If targets.Count = 0 Then
            parts.Add($"-c:v {编码器.命令行编码器名}")
            Exit Sub
        End If
        If 编码器.是否复制流 AndAlso 来自滤镜 Then Exit Sub
        For Each target In targets
            parts.Add($"-c:{target} {编码器.命令行编码器名}")
        Next
    End Sub

    Private Shared Sub 添加按流视频附加参数(parts As List(Of String), a As 预设数据_v6, 阶段 As 预设数据_v6.命令行阶段, 选择器 As List(Of String))
        Dim targets = 规范输出流选择器列表(选择器, True)
        Dim 编码器 = 视频编码器数据库_v6.获取编码器数据(a.视频参数_编码器_具体编码)
        For Each target In targets
            If 编码器 IsNot Nothing Then
                添加编码器参数(parts, 编码器.编码预设.参数名, a.视频参数_编码器_编码预设, target)
                添加编码器参数(parts, 编码器.配置文件.参数名, a.视频参数_编码器_配置文件, target)
                添加编码器参数(parts, 编码器.场景优化.参数名, a.视频参数_编码器_场景优化, target)
                添加编码器参数(parts, 编码器.图片质量.参数名, a.视频参数_编码器_图片编码器质量值, target)
            End If
            添加编码器参数(parts, "-pix_fmt", a.视频参数_色彩管理_像素格式, target)
            添加编码器参数(parts, "-gpu", a.视频参数_编码器_gpu, target)
            添加编码器参数(parts, "-threads", a.视频参数_编码器_threads, target)
            parts.AddRange(生成质量参数(a, 阶段, target))
        Next
    End Sub

    Private Shared Sub 添加按流音频编码参数(parts As List(Of String), 音频 As 音频编码器数据库_v6.音频编码器数据, a As 预设数据_v6, 选择器 As List(Of String), 来自滤镜 As Boolean)
        If 音频.是否禁用 Then
            parts.Add("-an")
            Exit Sub
        End If
        If 音频.是否复制流 AndAlso 来自滤镜 Then Exit Sub
        Dim targets = 规范输出流选择器列表(选择器, True)
        For Each target In targets
            If 音频.命令行编码器名 <> "" Then
                parts.Add($"{应用输出流选择器("-c:a", target)} {音频.命令行编码器名}")
            End If
            For Each 单项参数 In 音频.默认附加参数列表
                添加音频编码器默认参数(parts, 单项参数, target)
            Next
            添加编码器参数(parts, "-b:a", a.音频参数_比特率, target)
            添加编码器参数(parts, a.音频参数_质量参数名, a.音频参数_质量值, target)
            添加编码器参数(parts, "-sample_fmt", a.音频参数_位深度, target)
        Next
    End Sub

    Private Shared Function 生成质量参数(a As 预设数据_v6, 阶段 As 预设数据_v6.命令行阶段, Optional 输出流选择器 As String = "") As List(Of String)
        Dim parts As New List(Of String)
        Select Case a.视频参数_比特率_控制方式
            Case 预设数据_v6.视频全局质量控制方式.CRF
                添加编码器参数(parts, $"-{If(a.视频参数_质量控制_参数名 = "", "crf", a.视频参数_质量控制_参数名)}", a.视频参数_质量控制_值, 输出流选择器)
            Case 预设数据_v6.视频全局质量控制方式.CQP
                添加编码器参数(parts, $"-{If(a.视频参数_质量控制_参数名 = "", "qp", a.视频参数_质量控制_参数名)}", a.视频参数_质量控制_值, 输出流选择器)
            Case 预设数据_v6.视频全局质量控制方式.VBR, 预设数据_v6.视频全局质量控制方式.VBRHQ, 预设数据_v6.视频全局质量控制方式.CBR
                添加视频质量参数(parts, a, 输出流选择器)
                添加编码器参数(parts, "-b:v", a.视频参数_比特率_基础, 输出流选择器)
                添加编码器参数(parts, "-minrate", a.视频参数_比特率_最低值, 输出流选择器)
                添加编码器参数(parts, "-maxrate", a.视频参数_比特率_最高值, 输出流选择器)
                添加编码器参数(parts, "-bufsize", a.视频参数_比特率_缓冲区, 输出流选择器)
            Case 预设数据_v6.视频全局质量控制方式.TPE
                If 阶段 = 预设数据_v6.命令行阶段.二次编码第一遍 OrElse 阶段 = 预设数据_v6.命令行阶段.二次编码第二遍 OrElse 二次编码基础码率有效(a) Then
                    添加编码器参数(parts, "-b:v", a.视频参数_比特率_基础, 输出流选择器)
                    添加编码器参数(parts, "-minrate", a.视频参数_比特率_最低值, 输出流选择器)
                    添加编码器参数(parts, "-maxrate", a.视频参数_比特率_最高值, 输出流选择器)
                    添加编码器参数(parts, "-bufsize", a.视频参数_比特率_缓冲区, 输出流选择器)
                Else
                    添加视频质量参数(parts, a, 输出流选择器)
                End If
        End Select
        AddRaw(parts, a.视频参数_质量控制_进阶参数集)
        Return parts
    End Function

    Private Shared Sub 添加视频质量参数(parts As List(Of String), a As 预设数据_v6, 输出流选择器 As String)
        If String.IsNullOrWhiteSpace(a.视频参数_质量控制_参数名) OrElse String.IsNullOrWhiteSpace(a.视频参数_质量控制_值) Then Exit Sub
        添加编码器参数(parts, $"-{a.视频参数_质量控制_参数名.TrimStart("-"c)}", a.视频参数_质量控制_值, 输出流选择器)
    End Sub

    Public Shared Function 当前编码器支持二次编码(a As 预设数据_v6) As Boolean
        Dim 编码器 = 视频编码器数据库_v6.获取编码器数据(If(a?.视频参数_编码器_具体编码, ""))
        Return 编码器 IsNot Nothing AndAlso 编码器.支持二次编码
    End Function

    Private Shared Function 可以生成二次编码(a As 预设数据_v6) As Boolean
        Return a IsNot Nothing AndAlso
               a.视频参数_比特率_控制方式 = 预设数据_v6.视频全局质量控制方式.TPE AndAlso
               当前编码器支持二次编码(a) AndAlso
               二次编码基础码率有效(a)
    End Function

    Private Shared Function 二次编码基础码率有效(a As 预设数据_v6) As Boolean
        Return a IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(a.视频参数_比特率_基础)
    End Function

    Private Shared Function 全局质量参数已设置(a As 预设数据_v6) As Boolean
        Return a IsNot Nothing AndAlso
               (Not String.IsNullOrWhiteSpace(a.视频参数_质量控制_参数名) OrElse
                Not String.IsNullOrWhiteSpace(a.视频参数_质量控制_值))
    End Function

    Private Shared Function 生成二次编码日志路径(输入文件 As String) As String
        Dim source = If(输入文件, "").Trim()
        If source = "" OrElse (source.StartsWith("<"c) AndAlso source.EndsWith(">"c)) Then Return "3fui-v6-passlog"

        Dim dir = Path.GetDirectoryName(source)
        If String.IsNullOrWhiteSpace(dir) Then dir = Environment.CurrentDirectory
        Dim baseName = Path.GetFileNameWithoutExtension(source)
        If String.IsNullOrWhiteSpace(baseName) Then baseName = "input"
        Return Path.Combine(dir, "3fui-v6-passlog-" & 清理二次编码日志文件名(baseName))
    End Function

    Private Shared Function 清理二次编码日志文件名(value As String) As String
        Dim result = If(value, "").Trim()
        For Each c In Path.GetInvalidFileNameChars()
            result = result.Replace(c, "_"c)
        Next
        If result = "" Then result = "input"
        Return result
    End Function

    Private Shared Function 生成剪辑输入参数(a As 预设数据_v6, 阶段 As 预设数据_v6.命令行阶段, 媒体总时长 As String, 结果 As 预设数据_v6.命令行生成结果) As String
        Select Case a.剪辑区间_方法
            Case 预设数据_v6.剪辑方法.粗剪, 预设数据_v6.剪辑方法.精剪空降解码
                Return $"{If(a.剪辑区间_入点 <> "", "-ss " & a.剪辑区间_入点, "")} {If(a.剪辑区间_出点 <> "", "-to " & a.剪辑区间_出点, "")}".Trim()
            Case 预设数据_v6.剪辑方法.掐头去尾
                结果.需要媒体总时长 = True
                Dim duration = If(String.IsNullOrWhiteSpace(媒体总时长), 媒体总时长占位符, 媒体总时长)
                Dim t = $"({duration}-{If(a.剪辑区间_入点 = "", "0", a.剪辑区间_入点)}-{If(a.剪辑区间_出点 = "", "0", a.剪辑区间_出点)})"
                Return $"{If(a.剪辑区间_入点 <> "", "-ss " & a.剪辑区间_入点, "")} -t {t}".Trim()
            Case Else
                Return ""
        End Select
    End Function

    Private Shared Function 生成解码参数(a As 预设数据_v6) As String
        Dim parts As New List(Of String)
        If a.解码参数_解码器 <> "" Then parts.Add($"-c:v {a.解码参数_解码器}")
        If a.解码参数_CPU解码线程数 <> "" Then parts.Add($"-threads {a.解码参数_CPU解码线程数}")
        If a.解码参数_解码数据格式 <> "" Then parts.Add($"-hwaccel_output_format {a.解码参数_解码数据格式}")
        If a.解码参数_指定硬件的参数名 <> "" AndAlso a.解码参数_指定硬件的参数 <> "" Then parts.Add($"{a.解码参数_指定硬件的参数名} {a.解码参数_指定硬件的参数}")
        Return String.Join(" ", parts)
    End Function

    Private Shared Function 生成主输入参数(a As 预设数据_v6, 输入文件 As String) As List(Of String)
        Dim parts As New List(Of String)
        If a.视频参数_视频帧服务器_使用AviSynth AndAlso a.视频参数_视频帧服务器_avs脚本文件.Trim() <> "" Then
            parts.Add("-i")
            parts.Add(Q(派生脚本路径(输入文件, ".avs")))
        ElseIf a.视频参数_视频帧服务器_使用VapourSynth AndAlso a.视频参数_视频帧服务器_vpy脚本文件.Trim() <> "" Then
            parts.Add("-f vapoursynth")
            parts.Add("-i")
            parts.Add(Q(派生脚本路径(输入文件, Path.GetExtension(a.视频参数_视频帧服务器_vpy脚本文件))))
        Else
            parts.Add("-i")
            parts.Add(Q(输入文件))
        End If
        Return parts
    End Function

    Private Shared Function 派生脚本路径(输入文件 As String, ext As String) As String
        If 输入文件 = 输入占位符 OrElse 输入文件 = "<InputFile>" Then
            Return "<InputFileWithOutExtension>" & If(String.IsNullOrWhiteSpace(ext), ".vpy", ext)
        End If
        Dim dir = Path.GetDirectoryName(输入文件)
        Dim name = Path.GetFileNameWithoutExtension(输入文件)
        If String.IsNullOrWhiteSpace(dir) Then Return name & If(String.IsNullOrWhiteSpace(ext), ".vpy", ext)
        Return Path.Combine(dir, name & If(String.IsNullOrWhiteSpace(ext), ".vpy", ext))
    End Function

    Private Shared Function 生成元数据章节附件片段(a As 预设数据_v6, 当前视频输出数量 As Integer) As 附加输出片段
        Dim result As New 附加输出片段
        Select Case a.流控制_元数据选项
            Case 1
                result.输出前.Add("-map_metadata 0")
            Case 2
                result.输出前.Add("-map_metadata -1")
            Case 3
                result.输出前.Add("-map_metadata 0 -movflags +use_metadata_tags")
        End Select

        For Each item In If(a.元数据_要写入的信息, Array.Empty(Of 预设数据_v6.元数据单片结构)())
            If item Is Nothing OrElse item.字段.Trim() = "" Then Continue For
            result.输出前.Add($"-metadata {item.字段.Trim()}={QMetadata(item.值)}")
        Next

        Select Case a.流控制_章节选项
            Case 1
                result.输出前.Add("-map_chapters 0")
            Case 2
                result.输出前.Add("-map_chapters -1")
        End Select

        If a.章节_来源 <> 预设数据_v6.章节来源.未选择 AndAlso a.章节_文件路径.Trim() <> "" Then
            Dim inputIndex = 1 + result.额外输入.Where(Function(x) x = "-i").Count()
            Select Case a.章节_来源
                Case 预设数据_v6.章节来源.文本文档
                    result.额外输入.Add("-f ffmetadata")
                    result.额外输入.Add("-i")
                    result.额外输入.Add(Q(a.章节_文件路径.Trim()))
                    result.输出前.Add($"-map_metadata {inputIndex}")
                    result.输出前.Add($"-map_chapters {inputIndex}")
                Case 预设数据_v6.章节来源.媒体文件
                    result.额外输入.Add("-i")
                    result.额外输入.Add(Q(a.章节_文件路径.Trim()))
                    result.输出前.Add($"-map_chapters {inputIndex}")
            End Select
        End If

        Select Case a.流控制_附件选项
            Case 1
                result.输出前.Add("-map 0:t? -c:t copy")
        End Select

        Dim 额外输入索引 = 1 + result.额外输入.Where(Function(x) x = "-i").Count()
        Dim 封面序号 As Integer = 0
        Dim 附件序号 As Integer = 0
        For Each item In If(a.附件_要写入的附件, Array.Empty(Of 预设数据_v6.附件单片结构)())
            If item Is Nothing OrElse item.文件路径.Trim() = "" OrElse item.类型 = 预设数据_v6.附件单片结构.附件类型.未选择 Then Continue For
            Select Case item.类型
                Case 预设数据_v6.附件单片结构.附件类型.MP4封面图
                    result.额外输入.Add("-i")
                    result.额外输入.Add(Q(item.文件路径.Trim()))
                    Dim 输出视频序号 = 当前视频输出数量 + 封面序号
                    result.输出前.Add($"-map {额外输入索引}:v:0")
                    result.输出前.Add($"-c:v:{输出视频序号} copy")
                    result.输出前.Add($"-disposition:v:{输出视频序号} attached_pic")
                    额外输入索引 += 1
                    封面序号 += 1
                Case 预设数据_v6.附件单片结构.附件类型.MKV封面图,
                     预设数据_v6.附件单片结构.附件类型.图片,
                     预设数据_v6.附件单片结构.附件类型.字体文件,
                     预设数据_v6.附件单片结构.附件类型.文本文档
                    result.输出前.Add($"-attach {Q(item.文件路径.Trim())}")
                    result.输出前.Add($"-metadata:s:t:{附件序号} mimetype={获取附件Mimetype(item.文件路径.Trim(), item.类型)}")
                    If item.类型 = 预设数据_v6.附件单片结构.附件类型.MKV封面图 Then
                        result.输出前.Add($"-metadata:s:t:{附件序号} filename=cover{Path.GetExtension(item.文件路径.Trim())}")
                    End If
                    附件序号 += 1
            End Select
        Next

        Return result
    End Function

    Private Shared Sub 添加编码器参数(parts As List(Of String), 参数名 As String, 值 As String)
        添加编码器参数(parts, 参数名, 值, "")
    End Sub

    Private Shared Sub 添加编码器参数(parts As List(Of String), 参数名 As String, 值 As String, 输出流选择器 As String)
        If String.IsNullOrWhiteSpace(参数名) OrElse String.IsNullOrWhiteSpace(值) Then Exit Sub
        parts.Add($"{应用输出流选择器(参数名, 输出流选择器)} {值}")
    End Sub

    Private Shared Sub 添加音频编码器默认参数(parts As List(Of String), 单项参数 As 音频编码器数据库_v6.音频编码器参数数据, 输出流选择器 As String)
        If 单项参数 Is Nothing OrElse String.IsNullOrWhiteSpace(单项参数.参数名) Then Exit Sub
        Dim 参数名 = 应用输出流选择器(单项参数.参数名, 输出流选择器)
        If String.IsNullOrWhiteSpace(单项参数.默认值) Then
            parts.Add(参数名)
        Else
            parts.Add($"{参数名} {单项参数.默认值}")
        End If
    End Sub

    Private Shared Function 规范输出流选择器列表(选择器 As List(Of String), Optional 空列表表示全局 As Boolean = False) As List(Of String)
        If 选择器 Is Nothing OrElse 选择器.Count = 0 Then
            If 空列表表示全局 Then Return New List(Of String) From {""}
            Return New List(Of String)
        End If
        Return 选择器.Where(Function(x) Not String.IsNullOrWhiteSpace(x)).Distinct(StringComparer.OrdinalIgnoreCase).ToList()
    End Function

    Private Shared Function 应用输出流选择器(参数名 As String, 输出流选择器 As String) As String
        If String.IsNullOrWhiteSpace(参数名) OrElse String.IsNullOrWhiteSpace(输出流选择器) Then Return 参数名
        Dim p = 参数名.Trim()
        If Not p.StartsWith("-", StringComparison.Ordinal) Then Return p
        Dim firstSpace = p.IndexOf(" "c)
        Dim head = If(firstSpace >= 0, p.Substring(0, firstSpace), p)
        Dim tail = If(firstSpace >= 0, p.Substring(firstSpace), "")
        Dim colon = head.IndexOf(":"c)
        If colon >= 0 Then
            Return head.Substring(0, colon) & ":" & 输出流选择器 & tail
        End If
        Return head & ":" & 输出流选择器 & tail
    End Function

    Private Shared Function 获取字幕编码参数(index As Integer) As String
        Select Case index
            Case 1
                Return "copy"
            Case 2
                Return "mov_text"
            Case 3
                Return "srt"
            Case 4
                Return "ass"
            Case 5
                Return "ssa"
            Case Else
                Return ""
        End Select
    End Function

    Private Shared Sub AddRaw(parts As List(Of String), value As String)
        If Not String.IsNullOrWhiteSpace(value) Then parts.Add(value.Trim().Replace(vbCrLf, " ").Replace(vbLf, " ").Replace(vbCr, " "))
    End Sub

    Private Shared Function Q(value As String) As String
        If value Is Nothing Then value = ""
        If value.StartsWith("<"c) AndAlso value.EndsWith(">"c) Then Return value
        Return $"""{value.Replace("""", "\""")}"""
    End Function

    Private Shared Function QMetadata(value As String) As String
        If value Is Nothing Then value = ""
        If value = "" Then Return """"""
        If value.Any(Function(c) Char.IsWhiteSpace(c) OrElse c = """"c OrElse c = "\"c) Then Return Q(value)
        Return value
    End Function

    Private Shared Function 获取附件Mimetype(file As String, 类型 As 预设数据_v6.附件单片结构.附件类型) As String
        Dim ext = Path.GetExtension(If(file, "")).TrimStart("."c).ToLowerInvariant()
        Select Case ext
            Case "jpg", "jpeg"
                Return "image/jpeg"
            Case "png"
                Return "image/png"
            Case "webp"
                Return "image/webp"
            Case "bmp"
                Return "image/bmp"
            Case "gif"
                Return "image/gif"
            Case "ttf"
                Return "application/x-truetype-font"
            Case "otf"
                Return "application/vnd.ms-opentype"
            Case "ttc"
                Return "application/x-font-ttf"
            Case "woff"
                Return "font/woff"
            Case "woff2"
                Return "font/woff2"
            Case "txt", "md", "nfo"
                Return "text/plain"
            Case "json"
                Return "application/json"
            Case "xml"
                Return "application/xml"
            Case Else
                Select Case 类型
                    Case 预设数据_v6.附件单片结构.附件类型.字体文件
                        Return "application/octet-stream"
                    Case 预设数据_v6.附件单片结构.附件类型.文本文档
                        Return "text/plain"
                    Case Else
                        Return "application/octet-stream"
                End Select
        End Select
    End Function

    Private Shared Function 规范流列表(values As IEnumerable(Of String), 类型 As String) As List(Of String)
        Dim result As New List(Of String)
        If values Is Nothing Then Return result
        For Each raw In values
            For Each item In If(raw, "").Split(separator, StringSplitOptions.RemoveEmptyEntries)
                Dim s = item.Trim()
                If s = "" Then Continue For
                If s.Contains(":") Then
                    result.Add(s)
                Else
                    result.Add($"0:{类型}:{s}")
                End If
            Next
        Next
        Return result.Distinct().ToList()
    End Function

    Private Shared Function SplitTextList(value As String) As String()
        If String.IsNullOrWhiteSpace(value) Then Return Array.Empty(Of String)()
        Return value.Split(separator, StringSplitOptions.RemoveEmptyEntries).Select(Function(x) x.Trim()).Where(Function(x) x <> "").ToArray()
    End Function

    Private Shared Function SelectedIndexToEnum(Of T As Structure)(index As Integer) As T
        Dim result As T = Nothing
        [Enum].TryParse(index.ToString(), result)
        Return result
    End Function

    Private Shared Function EnumToIndex(value As [Enum]) As Integer
        Return Convert.ToInt32(value, CultureInfo.InvariantCulture)
    End Function

    Private Shared Function TrackValue(track As Object) As String
        If track Is Nothing Then Return ""
        Dim p = track.GetType().GetProperty("Value")
        If p Is Nothing Then Return ""
        Dim v = p.GetValue(track)
        If v Is Nothing Then Return ""
        Return Convert.ToString(v, CultureInfo.InvariantCulture)
    End Function

    Private Shared Sub SetTrackValue(track As Object, value As String)
        If track Is Nothing OrElse value = "" Then Exit Sub
        Dim p = track.GetType().GetProperty("Value")
        If p Is Nothing Then Exit Sub
        Dim d As Double
        If Double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, d) OrElse Double.TryParse(value, d) Then p.SetValue(track, d)
    End Sub

    Private Shared Function 构造缩放滤镜(a As 预设数据_v6) As String
        If a.视频参数_分辨率 <> "" Then Return $"scale={a.视频参数_分辨率.Replace("x", ":")}"
        If a.视频参数_分辨率自动计算_宽度 <> "" OrElse a.视频参数_分辨率自动计算_高度 <> "" Then Return $"scale={If(a.视频参数_分辨率自动计算_宽度 = "", "-2", a.视频参数_分辨率自动计算_宽度)}:{If(a.视频参数_分辨率自动计算_高度 = "", "-2", a.视频参数_分辨率自动计算_高度)}"
        Return ""
    End Function

    Private Shared Function 构造抽帧滤镜(a As 预设数据_v6) As String
        Dim opts As New List(Of String)
        If a.视频参数_抽帧_max <> "" Then opts.Add("max=" & a.视频参数_抽帧_max)
        If a.视频参数_抽帧_keep <> "" Then opts.Add("keep=" & a.视频参数_抽帧_keep)
        If a.视频参数_抽帧_hi <> "" Then opts.Add("hi=" & a.视频参数_抽帧_hi)
        If a.视频参数_抽帧_lo <> "" Then opts.Add("lo=" & a.视频参数_抽帧_lo)
        If a.视频参数_抽帧_frac <> "" Then opts.Add("frac=" & a.视频参数_抽帧_frac)
        Return If(opts.Count > 0, "mpdecimate=" & String.Join(":", opts), "")
    End Function

    Private Shared Function 构造插帧滤镜(a As 预设数据_v6) As String
        If a.视频参数_插帧_目标帧率 = "" Then Return ""
        Dim opts As New List(Of String) From {$"fps={a.视频参数_插帧_目标帧率}"}
        If a.视频参数_插帧_插帧模式 <> "" Then opts.Add("mi_mode=" & a.视频参数_插帧_插帧模式)
        If a.视频参数_插帧_运动估计模式 <> "" Then opts.Add("me_mode=" & a.视频参数_插帧_运动估计模式)
        If a.视频参数_插帧_运动估计算法 <> "" Then opts.Add("me=" & a.视频参数_插帧_运动估计算法)
        If a.视频参数_插帧_运动补偿模式 <> "" Then opts.Add("mc_mode=" & a.视频参数_插帧_运动补偿模式)
        If a.视频参数_插帧_块大小 <> "" Then opts.Add("mb_size=" & a.视频参数_插帧_块大小)
        If a.视频参数_插帧_搜索范围 <> "" Then opts.Add("search_param=" & a.视频参数_插帧_搜索范围)
        If a.视频参数_插帧_场景变化检测强度 <> "" Then opts.Add("scd_threshold=" & a.视频参数_插帧_场景变化检测强度)
        Return "minterpolate=" & String.Join(":", opts)
    End Function

    Private Shared Function 构造动态模糊滤镜(a As 预设数据_v6) As String
        If a.视频参数_动态模糊_连续混合帧数 = "" Then Return ""
        Dim opts As New List(Of String) From {$"frames={a.视频参数_动态模糊_连续混合帧数}"}
        If a.视频参数_动态模糊_每帧权重 <> "" Then opts.Add("weights=" & a.视频参数_动态模糊_每帧权重)
        If a.视频参数_动态模糊_输出缩放系数 <> "" Then opts.Add("scale=" & a.视频参数_动态模糊_输出缩放系数)
        If a.视频参数_动态模糊_处理颜色平面 <> "" Then opts.Add("planes=" & a.视频参数_动态模糊_处理颜色平面)
        Return "tmix=" & String.Join(":", opts)
    End Function

    Private Shared Function 构造超分滤镜(a As 预设数据_v6) As String
        Dim list As New List(Of 预设数据_v6.超分数据单片结构)
        If a.视频参数_超分_滤镜叠加策略组 IsNot Nothing AndAlso a.视频参数_超分_滤镜叠加策略组.Length > 0 Then
            list.AddRange(a.视频参数_超分_滤镜叠加策略组)
        ElseIf a.视频参数_超分_直接面板 IsNot Nothing Then
            list.Add(a.视频参数_超分_直接面板)
        End If
        Dim filters As New List(Of String)
        For Each s In list
            If s Is Nothing Then Continue For
            Dim libplaceboOpts As New List(Of String)
            If s.目标宽度 <> "" OrElse s.目标高度 <> "" Then
                libplaceboOpts.Add("w=" & If(s.目标宽度 = "", "iw", s.目标宽度))
                libplaceboOpts.Add("h=" & If(s.目标高度 = "", "ih", s.目标高度))
            End If
            If s.上采样算法 <> "" Then libplaceboOpts.Add("upscaler=" & s.上采样算法)
            If s.下采样算法 <> "" Then libplaceboOpts.Add("downscaler=" & s.下采样算法)
            If s.抗振铃强度 <> "" Then libplaceboOpts.Add("antiringing=" & s.抗振铃强度)
            If s.着色器文件路径 <> "" Then libplaceboOpts.Add($"custom_shader_path='{转义字幕滤镜值(s.着色器文件路径)}'")
            If libplaceboOpts.Any(Function(x) x.StartsWith("upscaler=", StringComparison.Ordinal) OrElse x.StartsWith("downscaler=", StringComparison.Ordinal) OrElse x.StartsWith("antiringing=", StringComparison.Ordinal) OrElse x.StartsWith("custom_shader_path=", StringComparison.Ordinal)) Then
                filters.Add("libplacebo=" & String.Join(":", libplaceboOpts))
            ElseIf s.目标宽度 <> "" OrElse s.目标高度 <> "" Then
                filters.Add($"scale={If(s.目标宽度 = "", "-2", s.目标宽度)}:{If(s.目标高度 = "", "-2", s.目标高度)}")
            End If
        Next
        Return String.Join(",", filters)
    End Function

    Private Shared Function 构造降噪滤镜(a As 预设数据_v6) As String
        Select Case a.视频参数_降噪_方式
            Case 预设数据_v6.降噪方式.hqdn3d
                Return $"hqdn3d={JoinNonEmpty(":", a.视频参数_降噪_参数1, a.视频参数_降噪_参数2, a.视频参数_降噪_参数3, a.视频参数_降噪_参数4)}".TrimEnd("="c)
            Case 预设数据_v6.降噪方式.nlmeans
                Return $"nlmeans={JoinNamed(":", ("s", a.视频参数_降噪_参数1), ("p", a.视频参数_降噪_参数2), ("pc", a.视频参数_降噪_参数3), ("r", a.视频参数_降噪_参数4))}".TrimEnd("="c)
            Case 预设数据_v6.降噪方式.atadenoise
                Return $"atadenoise={JoinNamed(":", ("0a", a.视频参数_降噪_参数1), ("0b", a.视频参数_降噪_参数2), ("1a", a.视频参数_降噪_参数3), ("1b", a.视频参数_降噪_参数4))}".TrimEnd("="c)
            Case 预设数据_v6.降噪方式.bm3d
                Return $"bm3d={JoinNamed(":", ("sigma", a.视频参数_降噪_参数1), ("block", a.视频参数_降噪_参数2), ("bstep", a.视频参数_降噪_参数3), ("group", a.视频参数_降噪_参数4))}".TrimEnd("="c)
            Case 预设数据_v6.降噪方式.bilateral_cuda
                Return $"bilateral_cuda={JoinNamed(":", ("sigmaS", a.视频参数_降噪_参数1), ("sigmaR", a.视频参数_降噪_参数2), ("window_size", a.视频参数_降噪_参数3))}".TrimEnd("="c)
        End Select
        Return ""
    End Function

    Private Shared Function 构造锐化滤镜(a As 预设数据_v6) As String
        Select Case a.视频参数_锐化_方式
            Case 预设数据_v6.锐化方式.cas
                Return $"cas={JoinNamed(":", ("strength", a.视频参数_锐化_参数1), ("planes", a.视频参数_锐化_参数2))}".TrimEnd("="c)
            Case 预设数据_v6.锐化方式.unsharp
                Return $"unsharp={JoinNonEmpty(":", a.视频参数_锐化_参数1, a.视频参数_锐化_参数2, a.视频参数_锐化_参数3)}".TrimEnd("="c)
            Case 预设数据_v6.锐化方式.sharpen_npp
                Return If(a.视频参数_锐化_参数1 <> "", $"sharpen_npp={a.视频参数_锐化_参数1}", "sharpen_npp")
        End Select
        Return ""
    End Function

    Private Shared Function 构造胶片颗粒滤镜(a As 预设数据_v6) As String
        Select Case a.视频参数_胶片颗粒_方式
            Case 预设数据_v6.胶片颗粒方式.noise_全平面动态均匀颗粒
                Return $"noise=alls={a.视频参数_胶片颗粒_参数1}:allf=t+u:all_seed={a.视频参数_胶片颗粒_参数2}"
            Case 预设数据_v6.胶片颗粒方式.noise_亮度为主动态颗粒
                Return $"noise=c0s={a.视频参数_胶片颗粒_参数1}:c0f=t+u:c1s={a.视频参数_胶片颗粒_参数2}:c1f=t+u:c2s={a.视频参数_胶片颗粒_参数2}:c2f=t+u:all_seed={a.视频参数_胶片颗粒_参数3}"
            Case 预设数据_v6.胶片颗粒方式.noise_柔和平均颗粒
                Return $"noise=alls={a.视频参数_胶片颗粒_参数1}:allf=t+a+u:all_seed={a.视频参数_胶片颗粒_参数2}"
            Case 预设数据_v6.胶片颗粒方式.libplacebo_应用片源胶片颗粒元数据
                Return "libplacebo=apply_filmgrain=true"
        End Select
        Return ""
    End Function

    Private Shared Function 构造平滑断层滤镜(a As 预设数据_v6) As String
        Select Case a.视频参数_平滑断层_方式
            Case 预设数据_v6.平滑断层方式.deband_标准去色带, 预设数据_v6.平滑断层方式.deband_强力去色带
                Return $"deband=1thr={a.视频参数_平滑断层_参数1}:2thr={a.视频参数_平滑断层_参数1}:3thr={a.视频参数_平滑断层_参数1}:range={a.视频参数_平滑断层_参数2}:direction={a.视频参数_平滑断层_参数3}:blur=1:coupling={a.视频参数_平滑断层_参数4}"
            Case 预设数据_v6.平滑断层方式.gradfun_快速渐变平滑
                Return $"gradfun=strength={a.视频参数_平滑断层_参数1}:radius={a.视频参数_平滑断层_参数2}"
            Case 预设数据_v6.平滑断层方式.libplacebo_GPU去色带加颗粒
                Return $"libplacebo=deband=true:deband_iterations={a.视频参数_平滑断层_参数1}:deband_threshold={a.视频参数_平滑断层_参数2}:deband_radius={a.视频参数_平滑断层_参数3}:deband_grain={a.视频参数_平滑断层_参数4}"
        End Select
        Return ""
    End Function

    Private Shared Function 构造扫描方式滤镜(a As 预设数据_v6) As String
        Select Case a.视频参数_处理扫描方式
            Case 预设数据_v6.扫描方式.yadif_单帧输入_自动场序_空间检查, 预设数据_v6.扫描方式.PAL_标准反交错
                Return "yadif=mode=send_frame:parity=auto:deint=all"
            Case 预设数据_v6.扫描方式.yadif_单帧输入_顶场优先_空间检查
                Return "yadif=mode=send_frame:parity=tff:deint=all"
            Case 预设数据_v6.扫描方式.yadif_单帧输入_底场优先_空间检查
                Return "yadif=mode=send_frame:parity=bff:deint=all"
            Case 预设数据_v6.扫描方式.tinterlace_顶场优先
                Return "tinterlace=mode=interleave_top"
            Case 预设数据_v6.扫描方式.tinterlace_底场优先
                Return "tinterlace=mode=interleave_bottom"
            Case 预设数据_v6.扫描方式.NTSC_标准IVTC_胶片32Pulldown转逐行
                Return "fieldmatch,yadif=deint=interlaced,decimate"
            Case 预设数据_v6.扫描方式.NTSC_纯隔行_非胶片转逐行, 预设数据_v6.扫描方式.PAL_标准反交错_双倍帧率
                Return "yadif=mode=send_field:parity=auto:deint=all"
            Case 预设数据_v6.扫描方式.NTSC_自动检测Pulldown至25fps
                Return "pullup=jl=1:jr=1,fps=25"
            Case 预设数据_v6.扫描方式.PAL_高质量反交错
                Return "bwdif=mode=send_frame:parity=auto:deint=all"
            Case 预设数据_v6.扫描方式.PAL_高质量反交错_双倍帧率
                Return "bwdif=mode=send_field:parity=auto:deint=all"
            Case 预设数据_v6.扫描方式.yadif_cuda_自动场序
                Return "yadif_cuda=mode=send_frame:parity=auto:deint=all"
            Case 预设数据_v6.扫描方式.bwdif_cuda_自动场序
                Return "bwdif_cuda=mode=send_frame:parity=auto:deint=all"
        End Select
        Return ""
    End Function

    Private Shared Function 构造翻转滤镜(a As 预设数据_v6) As String
        Dim filters As New List(Of String)
        Select Case a.视频参数_画面翻转_角度翻转
            Case 预设数据_v6.画面翻转角度.顺时针旋转90度 : filters.Add("transpose=1")
            Case 预设数据_v6.画面翻转角度.顺时针旋转180度 : filters.AddRange({"transpose=1", "transpose=1"})
            Case 预设数据_v6.画面翻转角度.顺时针旋转270度 : filters.AddRange({"transpose=1", "transpose=1", "transpose=1"})
            Case 预设数据_v6.画面翻转角度.逆时针旋转90度 : filters.Add("transpose=2")
            Case 预设数据_v6.画面翻转角度.逆时针旋转180度 : filters.AddRange({"transpose=2", "transpose=2"})
            Case 预设数据_v6.画面翻转角度.逆时针旋转270度 : filters.AddRange({"transpose=2", "transpose=2", "transpose=2"})
        End Select
        Select Case a.视频参数_画面翻转_镜像翻转
            Case 预设数据_v6.画面翻转镜像.水平镜像 : filters.Add("hflip")
            Case 预设数据_v6.画面翻转镜像.垂直镜像 : filters.Add("vflip")
        End Select
        Return String.Join(",", filters)
    End Function

    Private Shared Function 构造烧字幕滤镜(a As 预设数据_v6, 输入文件 As String) As String
        If a.视频参数_烧录字幕_自己写滤镜取代所有设置 <> "" Then Return a.视频参数_烧录字幕_自己写滤镜取代所有设置
        If a.视频参数_烧录字幕_滤镜选择 = 预设数据_v6.烧字幕滤镜.未选择 Then Return ""
        Dim name = If(a.视频参数_烧录字幕_滤镜选择 = 预设数据_v6.烧字幕滤镜.ass, "ass", "subtitles")

        Dim 滤镜参数列表 As New List(Of String)
        Dim 样式参数列表 As New List(Of String)

        If a.视频参数_烧录字幕_字幕来源是外部文件 = 预设数据_v6.烧字幕来源.外部字幕文件 Then
            Dim 字幕文件 = 解析外部字幕文件(a, 输入文件)
            If 字幕文件 <> "" Then 滤镜参数列表.Add($"filename='{转义字幕滤镜值(字幕文件)}'")
        End If
        If a.视频参数_烧录字幕_字幕来源是外部文件 = 预设数据_v6.烧字幕来源.内嵌的流 AndAlso a.视频参数_烧录字幕_指定内嵌的流 <> "" Then
            滤镜参数列表.Add($"filename='{转义字幕滤镜值(输入文件)}'")
            滤镜参数列表.Add($"stream_index={a.视频参数_烧录字幕_指定内嵌的流}")
        End If

        If a.视频参数_烧录字幕_字体文件夹 <> "" Then 滤镜参数列表.Add($"fontsdir='{转义字幕滤镜值(a.视频参数_烧录字幕_字体文件夹)}'")
        If a.视频参数_烧录字幕_基本样式_名称 <> "" Then 样式参数列表.Add($"FontName={a.视频参数_烧录字幕_基本样式_名称}")
        If a.视频参数_烧录字幕_基本样式_大小 <> 0 Then 样式参数列表.Add($"FontSize={a.视频参数_烧录字幕_基本样式_大小.ToString(CultureInfo.InvariantCulture)}")
        If a.视频参数_烧录字幕_基本样式_粗体 Then 样式参数列表.Add("Bold=-1")
        If a.视频参数_烧录字幕_基本样式_斜体 Then 样式参数列表.Add("Italic=-1")
        If a.视频参数_烧录字幕_基本样式_下划线 Then 样式参数列表.Add("Underline=-1")
        If a.视频参数_烧录字幕_基本样式_删除线 Then 样式参数列表.Add("StrikeOut=-1")
        Select Case a.视频参数_烧录字幕_边框样式
            Case 预设数据_v6.烧字幕边框样式.边框_阴影 : 样式参数列表.Add("BorderStyle=1")
            Case 预设数据_v6.烧字幕边框样式.背景框 : 样式参数列表.Add("BorderStyle=3")
        End Select
        If a.视频参数_烧录字幕_描边宽度 <> "" Then 样式参数列表.Add($"Outline={a.视频参数_烧录字幕_描边宽度}")
        If a.视频参数_烧录字幕_阴影距离 <> "" Then 样式参数列表.Add($"Shadow={a.视频参数_烧录字幕_阴影距离}")
        添加字幕颜色样式(样式参数列表, "PrimaryColour", a.视频参数_烧录字幕_主要颜色)
        添加字幕颜色样式(样式参数列表, "SecondaryColour", a.视频参数_烧录字幕_次要颜色)
        添加字幕颜色样式(样式参数列表, "OutlineColour", a.视频参数_烧录字幕_描边颜色)
        添加字幕颜色样式(样式参数列表, "BackColour", a.视频参数_烧录字幕_背景颜色)
        If a.视频参数_烧录字幕_对齐方位 <> 预设数据_v6.烧字幕对齐方位.未选择 Then 样式参数列表.Add($"Alignment={CInt(a.视频参数_烧录字幕_对齐方位)}")
        If a.视频参数_烧录字幕_垂直边距 <> "" Then 样式参数列表.Add($"MarginV={a.视频参数_烧录字幕_垂直边距}")
        If a.视频参数_烧录字幕_左边距 <> "" Then 样式参数列表.Add($"MarginL={a.视频参数_烧录字幕_左边距}")
        If a.视频参数_烧录字幕_右边距 <> "" Then 样式参数列表.Add($"MarginR={a.视频参数_烧录字幕_右边距}")
        If a.视频参数_烧录字幕_字距 <> "" Then 样式参数列表.Add($"Spacing={a.视频参数_烧录字幕_字距}")
        If a.视频参数_烧录字幕_行距 <> "" Then 样式参数列表.Add($"LineSpacing={a.视频参数_烧录字幕_行距}")
        If a.视频参数_烧录字幕_补充样式 <> "" Then 样式参数列表.Add(a.视频参数_烧录字幕_补充样式)
        If 样式参数列表.Count > 0 Then 滤镜参数列表.Add($"force_style='{String.Join(",", 样式参数列表).Replace("'", "\'")}'")

        Return If(滤镜参数列表.Count > 0, $"{name}={String.Join(":", 滤镜参数列表)}", name)
    End Function

    Private Shared Function 解析外部字幕文件(a As 预设数据_v6, 输入文件 As String) As String
        If 输入文件 = 输入占位符 OrElse 输入文件 = "<InputFile>" OrElse String.IsNullOrWhiteSpace(输入文件) Then
            Return "<字幕文件 | 预览模式专用字符>"
        End If

        Dim 字幕位置 = If(String.IsNullOrWhiteSpace(a.视频参数_烧录字幕_外部字幕文件夹位置), Path.GetDirectoryName(输入文件), a.视频参数_烧录字幕_外部字幕文件夹位置)
        Dim 字幕文件名 = If(String.IsNullOrWhiteSpace(a.视频参数_烧录字幕_外部字幕文件名), Path.GetFileNameWithoutExtension(输入文件), a.视频参数_烧录字幕_外部字幕文件名)
        If String.IsNullOrWhiteSpace(字幕文件名) Then Return ""

        Dim 格式列表 = If(a.视频参数_烧录字幕_字幕格式优先级, New List(Of 预设数据_v6.烧字幕格式)).
            Where(Function(x) x <> 预设数据_v6.烧字幕格式.未选择).
            Distinct().
            ToList()
        If 格式列表.Count = 0 Then 格式列表.AddRange({预设数据_v6.烧字幕格式.SRT, 预设数据_v6.烧字幕格式.ASS, 预设数据_v6.烧字幕格式.SSA})

        Dim 候选 As New List(Of String)
        For Each 格式 In 格式列表
            Dim ext = "." & 格式.ToString().ToLowerInvariant()
            候选.Add(If(String.IsNullOrWhiteSpace(字幕位置), 字幕文件名 & ext, Path.Combine(字幕位置, 字幕文件名 & ext)))
        Next
        Dim 已存在 = 候选.FirstOrDefault(Function(x) File.Exists(x))
        Return If(已存在, 候选.FirstOrDefault())
    End Function

    Private Shared Sub 添加字幕颜色样式(列表 As List(Of String), 名称 As String, 颜色 As 预设数据_v6.烧字幕专用颜色类型)
        If Not 字幕颜色已设置(颜色) Then Exit Sub
        列表.Add($"{名称}=&H{限制颜色通道(颜色.A):X2}{限制颜色通道(颜色.B):X2}{限制颜色通道(颜色.G):X2}{限制颜色通道(颜色.R):X2}")
    End Sub

    Private Shared Function 转义字幕滤镜值(value As String) As String
        If value Is Nothing Then Return ""
        If value.StartsWith("<"c) AndAlso value.EndsWith(">"c) Then Return value
        Return 将路径转换为FFmpeg滤镜接受的格式(value).Replace("'", "\'")
    End Function

    Private Shared Function 构造色彩转换滤镜(a As 预设数据_v6) As String
        Dim opts As New List(Of String)
        If a.视频参数_色彩管理_矩阵系数 <> "" Then opts.Add("matrix=" & a.视频参数_色彩管理_矩阵系数)
        If a.视频参数_色彩管理_色域 <> "" Then opts.Add("primaries=" & a.视频参数_色彩管理_色域)
        If a.视频参数_色彩管理_传输特性 <> "" Then opts.Add("transfer=" & a.视频参数_色彩管理_传输特性)
        If a.视频参数_色彩管理_范围 <> "" Then opts.Add("range=" & a.视频参数_色彩管理_范围)
        If opts.Count = 0 Then Return ""
        Dim filterName = If(a.视频参数_色彩管理_滤镜选择 = "", "colorspace", a.视频参数_色彩管理_滤镜选择)
        Return filterName & "=" & String.Join(":", opts)
    End Function

    Private Shared Function 构造调色滤镜(a As 预设数据_v6) As String
        Dim opts As New List(Of String)
        If a.视频参数_色彩管理_启用调整亮度 AndAlso a.视频参数_色彩管理_亮度 <> "" Then opts.Add("brightness=" & a.视频参数_色彩管理_亮度)
        If a.视频参数_色彩管理_启用调整对比度 AndAlso a.视频参数_色彩管理_对比度 <> "" Then opts.Add("contrast=" & a.视频参数_色彩管理_对比度)
        If a.视频参数_色彩管理_启用调整饱和度 AndAlso a.视频参数_色彩管理_饱和度 <> "" Then opts.Add("saturation=" & a.视频参数_色彩管理_饱和度)
        If a.视频参数_色彩管理_启用调整伽马 AndAlso a.视频参数_色彩管理_伽马 <> "" Then opts.Add("gamma=" & a.视频参数_色彩管理_伽马)
        Return If(opts.Count > 0, "eq=" & String.Join(":", opts), "")
    End Function

    Private Shared Function 构造响度滤镜(a As 预设数据_v6) As String
        Dim opts As New List(Of String)
        If a.音频参数_响度标准化_启用调整目标响度 AndAlso a.音频参数_响度标准化_目标响度 <> "" Then opts.Add("I=" & a.音频参数_响度标准化_目标响度)
        If a.音频参数_响度标准化_启用调整动态范围 AndAlso a.音频参数_响度标准化_动态范围 <> "" Then opts.Add("LRA=" & a.音频参数_响度标准化_动态范围)
        If a.音频参数_响度标准化_启用调整峰值电平 AndAlso a.音频参数_响度标准化_峰值电平 <> "" Then opts.Add("TP=" & a.音频参数_响度标准化_峰值电平)
        Return If(opts.Count > 0, "loudnorm=" & String.Join(":", opts), "")
    End Function

    Private Shared Function JoinNonEmpty(delimiter As String, ParamArray values() As String) As String
        Return String.Join(delimiter, values.Where(Function(x) Not String.IsNullOrWhiteSpace(x)))
    End Function

    Private Shared Function JoinNamed(delimiter As String, ParamArray values() As (Name As String, Value As String)) As String
        Return String.Join(delimiter, values.Where(Function(x) Not String.IsNullOrWhiteSpace(x.Value)).Select(Function(x) $"{x.Name}={x.Value}"))
    End Function

    Private Class 滤镜图结果
        Public Property 滤镜图 As String = ""
        Public Property 映射参数 As String = ""
        Public Property 编码视频选择器 As New List(Of String)
        Public Property 编码音频选择器 As New List(Of String)
        Public Property 视频输出来自滤镜 As Boolean = False
        Public Property 音频输出来自滤镜 As Boolean = False
    End Class

    Private Shared Sub 储存输出文件设置(a As 预设数据_v6, ui As Form_v6_参数面板)
        With ui.私有界面_输出文件设置
            a.输出容器 = .MTB_后缀.Text
            a.输出_输出文件参数使用方法 = SelectedIndexToEnum(Of 预设数据_v6.输出文件参数使用方法)(Math.Max(0, .MCB_输出文件参数使用方法.SelectedIndex))
            a.输出_自动命名选项 = SelectedIndexToEnum(Of 预设数据_v6.自动命名选项)(Math.Max(0, .MCB_自动命名方式.SelectedIndex))
            a.输出命名_开头文本 = .MTB_开头文本.Text
            a.输出命名_替代文本 = .MTB_替代文件名.Text
            a.输出命名_结尾文本 = .MTB_结尾文本.Text
            a.输出命名_保留创建时间 = .MCB_保留创建时间.Checked
            a.输出命名_保留修改时间 = .MCB_保留修改时间.Checked
            a.输出命名_保留访问时间 = .MCB_保留访问时间.Checked
            Dim 输出位置文本 = .MCB_输出位置.Text.Trim()
            If a.额外保存输出位置 AndAlso Directory.Exists(输出位置文本) Then
                a.计算机名称 = Environment.MachineName
                a.输出位置 = 输出位置文本
            Else
                a.计算机名称 = ""
                a.输出位置 = ""
            End If
        End With
    End Sub

    Private Shared Sub 显示输出文件设置(a As 预设数据_v6, ui As Form_v6_参数面板)
        With ui.私有界面_输出文件设置
            .MTB_后缀.Text = a.输出容器
            .MCB_输出文件参数使用方法.SelectedIndex = EnumToIndex(a.输出_输出文件参数使用方法)
            .MCB_自动命名方式.SelectedIndex = EnumToIndex(a.输出_自动命名选项)
            .MTB_开头文本.Text = a.输出命名_开头文本
            .MTB_替代文件名.Text = a.输出命名_替代文本
            .MTB_结尾文本.Text = a.输出命名_结尾文本
            .MCB_保留创建时间.Checked = a.输出命名_保留创建时间
            .MCB_保留修改时间.Checked = a.输出命名_保留修改时间
            .MCB_保留访问时间.Checked = a.输出命名_保留访问时间
            If a.额外保存输出位置 AndAlso a.计算机名称 = Environment.MachineName AndAlso Directory.Exists(a.输出位置) Then
                .MCB_输出位置.Text = a.输出位置
            Else
                .MCB_输出位置.SelectedIndex = 0
            End If
        End With
    End Sub

    Private Shared Sub 储存解码参数(a As 预设数据_v6, ui As Form_v6_参数面板)
        With ui.私有界面_解码参数
            a.解码参数_解码器 = .ModernComboBox1.Text
            a.解码参数_CPU解码线程数 = .ModernTextBox1.Text
            a.解码参数_解码数据格式 = .ModernComboBox2.Text
            a.解码参数_指定硬件的参数名 = .ModernComboBox3.Text
            a.解码参数_指定硬件的参数 = .ModernTextBox2.Text
        End With
    End Sub

    Private Shared Sub 显示解码参数(a As 预设数据_v6, ui As Form_v6_参数面板)
        With ui.私有界面_解码参数
            .ModernComboBox1.Text = a.解码参数_解码器
            .ModernTextBox1.Text = a.解码参数_CPU解码线程数
            .ModernComboBox2.Text = a.解码参数_解码数据格式
            .ModernComboBox3.Text = a.解码参数_指定硬件的参数名
            .ModernTextBox2.Text = a.解码参数_指定硬件的参数
        End With
    End Sub

    Private Shared Sub 储存视频编码器(a As 预设数据_v6, ui As Form_v6_参数面板)
        With ui.私有界面_视频编码器
            a.视频参数_编码器_类型 = SelectedIndexToEnum(Of 预设数据_v6.视频编码器类型)(Math.Max(0, .MCB_视频编码器类型.SelectedIndex))
            a.视频参数_编码器_分类名称 = .MCB_视频编码器分类.Text
            a.视频参数_编码器_具体编码 = .MCB_具体编码器.Text
            a.视频参数_编码器_编码预设 = .MCB_编码预设.Text
            a.视频参数_编码器_配置文件 = .MCB_配置文件.Text
            a.视频参数_编码器_场景优化 = .MCB_场景优化.Text
            a.视频参数_编码器_gpu = .MTB_gpu.Text
            a.视频参数_编码器_threads = .MTB_threads.Text
            a.视频参数_编码器_图片编码器质量值 = .MTB_图片编码器质量值.Text
        End With
    End Sub

    Private Shared Sub 显示视频编码器(a As 预设数据_v6, ui As Form_v6_参数面板)
        With ui.私有界面_视频编码器
            .MCB_视频编码器类型.SelectedIndex = EnumToIndex(a.视频参数_编码器_类型)
            Dim 分类已选中 = 设置组合框文本并尝试选中(.MCB_视频编码器分类, a.视频参数_编码器_分类名称)
            If 分类已选中 Then
                设置组合框文本并尝试选中(.MCB_具体编码器, a.视频参数_编码器_具体编码)
            Else
                .MCB_具体编码器.Text = a.视频参数_编码器_具体编码
            End If
            .MCB_编码预设.Text = a.视频参数_编码器_编码预设
            .MCB_配置文件.Text = a.视频参数_编码器_配置文件
            .MCB_场景优化.Text = a.视频参数_编码器_场景优化
            .MTB_gpu.Text = a.视频参数_编码器_gpu
            .MTB_threads.Text = a.视频参数_编码器_threads
            .MTB_图片编码器质量值.Text = a.视频参数_编码器_图片编码器质量值
        End With
    End Sub

    Private Shared Function 设置组合框文本并尝试选中(combo As ModernComboBox, text As String) As Boolean
        If combo Is Nothing Then Return False
        text = If(text, "")
        For i = 0 To combo.Items.Count - 1
            Dim itemText = If(combo.Items(i), "").ToString()
            If String.Equals(itemText, text, StringComparison.Ordinal) Then
                combo.SelectedIndex = i
                Return True
            End If
        Next
        combo.Text = text
        Return False
    End Function

    Private Shared Sub 储存画面帧(a As 预设数据_v6, ui As Form_v6_参数面板)
        With ui.私有界面_画面帧
            a.视频参数_分辨率 = .ModernComboBox1.Text
            a.视频参数_分辨率自动计算_宽度 = .ModernComboBox2.Text
            a.视频参数_分辨率自动计算_高度 = .ModernComboBox3.Text
            a.视频参数_帧速率 = .ModernComboBox4.Text
            a.视频参数_分辨率_裁剪滤镜参数 = .ModernTextBox1.Text
            储存滤镜子窗口(a, .私有窗口_抽帧参数, .私有窗口_插帧参数, .私有窗口_动态模糊, .私有窗口_着色器超分, .私有窗口_降噪, .私有窗口_锐化, .私有窗口_胶片颗粒, .私有窗口_平滑断层, .私有窗口_扫描方式, .私有窗口_画面翻转, .私有窗口_烧录字幕)
        End With
    End Sub

    Private Shared Sub 显示画面帧(a As 预设数据_v6, ui As Form_v6_参数面板)
        With ui.私有界面_画面帧
            .ModernComboBox1.Text = a.视频参数_分辨率
            .ModernComboBox2.Text = a.视频参数_分辨率自动计算_宽度
            .ModernComboBox3.Text = a.视频参数_分辨率自动计算_高度
            .ModernComboBox4.Text = a.视频参数_帧速率
            .ModernTextBox1.Text = a.视频参数_分辨率_裁剪滤镜参数
            显示滤镜子窗口(a, .私有窗口_抽帧参数, .私有窗口_插帧参数, .私有窗口_动态模糊, .私有窗口_着色器超分, .私有窗口_降噪, .私有窗口_锐化, .私有窗口_胶片颗粒, .私有窗口_平滑断层, .私有窗口_扫描方式, .私有窗口_画面翻转, .私有窗口_烧录字幕)
        End With
    End Sub

    Private Shared Sub 储存滤镜子窗口(a As 预设数据_v6,
                                  抽帧 As Form_v6_参数面板_抽帧参数,
                                  插帧 As Form_v6_参数面板_插帧参数,
                                  动态模糊 As Form_v6_参数面板_动态模糊,
                                  超分 As Form_v6_参数面板_超分,
                                  降噪 As Form_v6_参数面板_降噪,
                                  锐化 As Form_v6_参数面板_锐化,
                                  胶片颗粒 As Form_v6_参数面板_胶片颗粒,
                                  平滑断层 As Form_v6_参数面板_平滑断层,
                                  扫描 As Form_v6_参数面板_扫描方式,
                                  翻转 As Form_v6_参数面板_画面翻转,
                                  烧字幕 As Form_v6_参数面板_烧录字幕)
        a.视频参数_抽帧_max = 抽帧.ModernTextBox1.Text
        a.视频参数_抽帧_keep = 抽帧.ModernTextBox2.Text
        a.视频参数_抽帧_hi = 抽帧.ModernTextBox3.Text
        a.视频参数_抽帧_lo = 抽帧.ModernComboBox1.Text
        a.视频参数_抽帧_frac = 抽帧.ModernComboBox2.Text

        a.视频参数_插帧_目标帧率 = If(插帧.MCB_插帧总开关.Checked, 插帧.MTB_目标帧率.Text, "")
        a.视频参数_插帧_插帧模式 = 插帧.MCB_插帧模式.Text
        a.视频参数_插帧_运动估计模式 = 插帧.MCB_运动估计模式.Text
        a.视频参数_插帧_运动估计算法 = 插帧.MCB_运动估计算法.Text
        a.视频参数_插帧_运动补偿模式 = 插帧.MCB_运动补偿模式.Text
        a.视频参数_插帧_可变块大小的运动补偿 = 插帧.MCB_可变块大小的运动补偿.Checked
        a.视频参数_插帧_块大小 = 插帧.MTB_块大小.Text
        a.视频参数_插帧_搜索范围 = 插帧.MTB_搜索范围.Text
        a.视频参数_插帧_场景变化检测强度 = 插帧.MTB_场景变化检测强度.Text

        a.视频参数_动态模糊_连续混合帧数 = If(动态模糊.MCB_动态模糊总开关.Checked, 动态模糊.MTB_连续混合帧数.Text, "")
        a.视频参数_动态模糊_每帧权重 = 动态模糊.MTB_每帧的权重.Text
        a.视频参数_动态模糊_输出缩放系数 = 动态模糊.MTB_输出缩放系数.Text
        a.视频参数_动态模糊_处理颜色平面 = 动态模糊.MTB_处理哪些颜色平面.Text

        If 超分.MCB_超分总开关.Checked Then
            a.视频参数_超分_直接面板 = New 预设数据_v6.超分数据单片结构 With {.目标宽度 = 超分.MTB_宽度.Text, .目标高度 = 超分.MTB_高度.Text, .上采样算法 = 超分.MCB_上采样算法.Text, .下采样算法 = 超分.MCB_下采样算法.Text, .抗振铃强度 = 超分.MTB_抗振铃强度.Text, .着色器文件路径 = 超分.MCB_着色器文件路径.Text}
        Else
            a.视频参数_超分_直接面板 = New 预设数据_v6.超分数据单片结构
        End If
        a.视频参数_超分_滤镜叠加策略组 = 超分.策略组数据.ToArray()

        a.视频参数_降噪_方式 = If(降噪.MCB_插帧总开关.Checked, SelectedIndexToEnum(Of 预设数据_v6.降噪方式)(Math.Max(0, 降噪.MCB_滤镜选择.SelectedIndex)), 预设数据_v6.降噪方式.未选择)
        a.视频参数_降噪_参数1 = TrackValue(降噪.ETB_降噪参数1)
        a.视频参数_降噪_参数2 = TrackValue(降噪.ETB_降噪参数2)
        a.视频参数_降噪_参数3 = TrackValue(降噪.ETB_降噪参数3)
        a.视频参数_降噪_参数4 = TrackValue(降噪.ETB_降噪参数4)

        a.视频参数_锐化_方式 = If(锐化.MCB_锐化总开关.Checked, SelectedIndexToEnum(Of 预设数据_v6.锐化方式)(Math.Max(0, 锐化.MCB_滤镜选择.SelectedIndex)), 预设数据_v6.锐化方式.未选择)
        a.视频参数_锐化_参数1 = TrackValue(锐化.ETB_锐化参数1)
        a.视频参数_锐化_参数2 = TrackValue(锐化.ETB_锐化参数2)
        a.视频参数_锐化_参数3 = TrackValue(锐化.ETB_锐化参数3)

        a.视频参数_胶片颗粒_方式 = If(胶片颗粒.MCB_胶片颗粒总开关.Checked, SelectedIndexToEnum(Of 预设数据_v6.胶片颗粒方式)(Math.Max(0, 胶片颗粒.MCB_滤镜选择.SelectedIndex)), 预设数据_v6.胶片颗粒方式.未选择)
        a.视频参数_胶片颗粒_参数1 = TrackValue(胶片颗粒.ETB_胶片颗粒参数1)
        a.视频参数_胶片颗粒_参数2 = TrackValue(胶片颗粒.ETB_胶片颗粒参数2)
        a.视频参数_胶片颗粒_参数3 = TrackValue(胶片颗粒.ETB_胶片颗粒参数3)
        a.视频参数_胶片颗粒_参数4 = TrackValue(胶片颗粒.ETB_胶片颗粒参数4)

        a.视频参数_平滑断层_方式 = If(平滑断层.MCB_平滑断层总开关.Checked, SelectedIndexToEnum(Of 预设数据_v6.平滑断层方式)(Math.Max(0, 平滑断层.MCB_滤镜选择.SelectedIndex)), 预设数据_v6.平滑断层方式.未选择)
        a.视频参数_平滑断层_参数1 = TrackValue(平滑断层.ETB_平滑断层参数1)
        a.视频参数_平滑断层_参数2 = TrackValue(平滑断层.ETB_平滑断层参数2)
        a.视频参数_平滑断层_参数3 = TrackValue(平滑断层.ETB_平滑断层参数3)
        a.视频参数_平滑断层_参数4 = TrackValue(平滑断层.ETB_平滑断层参数4)

        a.视频参数_处理扫描方式 = If(扫描.MCB_扫描方式总开关.Checked, SelectedIndexToEnum(Of 预设数据_v6.扫描方式)(Math.Max(0, 扫描.MCB_扫描方式.SelectedIndex)), 预设数据_v6.扫描方式.未选择)
        a.视频参数_画面翻转_角度翻转 = If(翻转.MCB_画面翻转总开关.Checked, SelectedIndexToEnum(Of 预设数据_v6.画面翻转角度)(Math.Max(0, 翻转.MCB_角度翻转.SelectedIndex)), 预设数据_v6.画面翻转角度.未选择)
        a.视频参数_画面翻转_镜像翻转 = If(翻转.MCB_画面翻转总开关.Checked, SelectedIndexToEnum(Of 预设数据_v6.画面翻转镜像)(Math.Max(0, 翻转.MCB_镜像翻转.SelectedIndex)), 预设数据_v6.画面翻转镜像.未选择)

        a.视频参数_烧录字幕_滤镜选择 = If(烧字幕.MCB_插帧总开关.Checked, SelectedIndexToEnum(Of 预设数据_v6.烧字幕滤镜)(Math.Max(0, 烧字幕.MCB_滤镜选择.SelectedIndex)), 预设数据_v6.烧字幕滤镜.未选择)
        a.视频参数_烧录字幕_字幕来源是外部文件 = SelectedIndexToEnum(Of 预设数据_v6.烧字幕来源)(Math.Max(0, 烧字幕.MCB_字幕来源.SelectedIndex))
        a.视频参数_烧录字幕_字幕格式优先级 = New List(Of 预设数据_v6.烧字幕格式) From {
            SelectedIndexToEnum(Of 预设数据_v6.烧字幕格式)(Math.Max(0, 烧字幕.MCB_后缀优先级1.SelectedIndex)),
            SelectedIndexToEnum(Of 预设数据_v6.烧字幕格式)(Math.Max(0, 烧字幕.MCB_后缀优先级2.SelectedIndex)),
            SelectedIndexToEnum(Of 预设数据_v6.烧字幕格式)(Math.Max(0, 烧字幕.MCB_后缀优先级3.SelectedIndex))
        }
        a.视频参数_烧录字幕_外部字幕文件名 = 烧字幕.MTB_字幕文件多余字符.Text
        a.视频参数_烧录字幕_外部字幕文件夹位置 = 烧字幕.MCB_字幕文件路径.Text
        a.视频参数_烧录字幕_指定内嵌的流 = 烧字幕.MTB_内嵌的流索引.Text
        Select Case a.视频参数_烧录字幕_字幕来源是外部文件
            Case 预设数据_v6.烧字幕来源.外部字幕文件
                a.视频参数_烧录字幕_指定内嵌的流 = ""
            Case 预设数据_v6.烧字幕来源.内嵌的流
                a.视频参数_烧录字幕_外部字幕文件名 = ""
                a.视频参数_烧录字幕_外部字幕文件夹位置 = ""
            Case Else
                a.视频参数_烧录字幕_外部字幕文件名 = ""
                a.视频参数_烧录字幕_外部字幕文件夹位置 = ""
                a.视频参数_烧录字幕_指定内嵌的流 = ""
        End Select
        If 烧字幕.基本样式已设置 Then
            Dim f = 烧字幕.基本样式字体
            a.视频参数_烧录字幕_基本样式_名称 = f.Name
            a.视频参数_烧录字幕_基本样式_大小 = f.Size
            a.视频参数_烧录字幕_基本样式_粗体 = f.Bold
            a.视频参数_烧录字幕_基本样式_斜体 = f.Italic
            a.视频参数_烧录字幕_基本样式_下划线 = f.Underline
            a.视频参数_烧录字幕_基本样式_删除线 = f.Strikeout
        Else
            a.视频参数_烧录字幕_基本样式_名称 = ""
            a.视频参数_烧录字幕_基本样式_大小 = 0
            a.视频参数_烧录字幕_基本样式_粗体 = False
            a.视频参数_烧录字幕_基本样式_斜体 = False
            a.视频参数_烧录字幕_基本样式_下划线 = False
            a.视频参数_烧录字幕_基本样式_删除线 = False
        End If
        a.视频参数_烧录字幕_边框样式 = SelectedIndexToEnum(Of 预设数据_v6.烧字幕边框样式)(Math.Max(0, 烧字幕.MCB_边框类型.SelectedIndex))
        a.视频参数_烧录字幕_字体文件夹 = 烧字幕.MCB_字体文件夹路径.Text
        a.视频参数_烧录字幕_描边宽度 = 烧字幕.MTB_描边宽度.Text
        a.视频参数_烧录字幕_阴影距离 = 烧字幕.MTB_阴影距离.Text
        写入字幕颜色(a.视频参数_烧录字幕_主要颜色, 烧字幕.主要颜色已设置, 烧字幕.主要颜色)
        写入字幕颜色(a.视频参数_烧录字幕_次要颜色, 烧字幕.次要颜色已设置, 烧字幕.次要颜色)
        写入字幕颜色(a.视频参数_烧录字幕_描边颜色, 烧字幕.描边颜色已设置, 烧字幕.描边颜色)
        写入字幕颜色(a.视频参数_烧录字幕_背景颜色, 烧字幕.背景颜色已设置, 烧字幕.背景颜色)
        a.视频参数_烧录字幕_对齐方位 = SelectedIndexToEnum(Of 预设数据_v6.烧字幕对齐方位)(Math.Max(0, 烧字幕.MCB_对齐方位.SelectedIndex))
        a.视频参数_烧录字幕_垂直边距 = 烧字幕.MTB_垂直边距.Text
        a.视频参数_烧录字幕_左边距 = 烧字幕.MTB_左边距.Text
        a.视频参数_烧录字幕_右边距 = 烧字幕.MTB_右边距.Text
        a.视频参数_烧录字幕_字距 = 烧字幕.MTB_字距.Text
        a.视频参数_烧录字幕_行距 = 烧字幕.MTB_行距.Text
        a.视频参数_烧录字幕_补充样式 = 烧字幕.MTB_补充样式.Text
        a.视频参数_烧录字幕_自己写滤镜取代所有设置 = 烧字幕.MTB_自己写整个滤镜.Text
    End Sub

    Private Shared Sub 显示滤镜子窗口(a As 预设数据_v6,
                                  抽帧 As Form_v6_参数面板_抽帧参数,
                                  插帧 As Form_v6_参数面板_插帧参数,
                                  动态模糊 As Form_v6_参数面板_动态模糊,
                                  超分 As Form_v6_参数面板_超分,
                                  降噪 As Form_v6_参数面板_降噪,
                                  锐化 As Form_v6_参数面板_锐化,
                                  胶片颗粒 As Form_v6_参数面板_胶片颗粒,
                                  平滑断层 As Form_v6_参数面板_平滑断层,
                                  扫描 As Form_v6_参数面板_扫描方式,
                                  翻转 As Form_v6_参数面板_画面翻转,
                                  烧字幕 As Form_v6_参数面板_烧录字幕)
        抽帧.ModernTextBox1.Text = a.视频参数_抽帧_max
        抽帧.ModernTextBox2.Text = a.视频参数_抽帧_keep
        抽帧.ModernTextBox3.Text = a.视频参数_抽帧_hi
        抽帧.ModernComboBox1.Text = a.视频参数_抽帧_lo
        抽帧.ModernComboBox2.Text = a.视频参数_抽帧_frac

        插帧.MCB_插帧总开关.Checked = a.视频参数_插帧_目标帧率 <> ""
        插帧.MTB_目标帧率.Text = a.视频参数_插帧_目标帧率
        插帧.MCB_插帧模式.Text = a.视频参数_插帧_插帧模式
        插帧.MCB_运动估计模式.Text = a.视频参数_插帧_运动估计模式
        插帧.MCB_运动估计算法.Text = a.视频参数_插帧_运动估计算法
        插帧.MCB_运动补偿模式.Text = a.视频参数_插帧_运动补偿模式
        插帧.MCB_可变块大小的运动补偿.Checked = a.视频参数_插帧_可变块大小的运动补偿
        插帧.MTB_块大小.Text = a.视频参数_插帧_块大小
        插帧.MTB_搜索范围.Text = a.视频参数_插帧_搜索范围
        插帧.MTB_场景变化检测强度.Text = a.视频参数_插帧_场景变化检测强度

        动态模糊.MCB_动态模糊总开关.Checked = a.视频参数_动态模糊_连续混合帧数 <> ""
        动态模糊.MTB_连续混合帧数.Text = a.视频参数_动态模糊_连续混合帧数
        动态模糊.MTB_每帧的权重.Text = a.视频参数_动态模糊_每帧权重
        动态模糊.MTB_输出缩放系数.Text = a.视频参数_动态模糊_输出缩放系数
        动态模糊.MTB_处理哪些颜色平面.Text = a.视频参数_动态模糊_处理颜色平面

        超分.MCB_超分总开关.Checked = 超分单片有设置(a.视频参数_超分_直接面板)
        If a.视频参数_超分_直接面板 IsNot Nothing Then
            超分.MTB_宽度.Text = a.视频参数_超分_直接面板.目标宽度
            超分.MTB_高度.Text = a.视频参数_超分_直接面板.目标高度
            超分.MCB_上采样算法.Text = a.视频参数_超分_直接面板.上采样算法
            超分.MCB_下采样算法.Text = a.视频参数_超分_直接面板.下采样算法
            超分.MTB_抗振铃强度.Text = a.视频参数_超分_直接面板.抗振铃强度
            超分.MCB_着色器文件路径.Text = a.视频参数_超分_直接面板.着色器文件路径
        End If
        超分.策略组数据 = If(a.视频参数_超分_滤镜叠加策略组, Array.Empty(Of 预设数据_v6.超分数据单片结构)()).ToList()
        超分.刷新策略组列表()

        降噪.MCB_插帧总开关.Checked = a.视频参数_降噪_方式 <> 预设数据_v6.降噪方式.未选择
        降噪.MCB_滤镜选择.SelectedIndex = EnumToIndex(a.视频参数_降噪_方式)
        SetTrackValue(降噪.ETB_降噪参数1, a.视频参数_降噪_参数1)
        SetTrackValue(降噪.ETB_降噪参数2, a.视频参数_降噪_参数2)
        SetTrackValue(降噪.ETB_降噪参数3, a.视频参数_降噪_参数3)
        SetTrackValue(降噪.ETB_降噪参数4, a.视频参数_降噪_参数4)

        锐化.MCB_锐化总开关.Checked = a.视频参数_锐化_方式 <> 预设数据_v6.锐化方式.未选择
        锐化.MCB_滤镜选择.SelectedIndex = EnumToIndex(a.视频参数_锐化_方式)
        SetTrackValue(锐化.ETB_锐化参数1, a.视频参数_锐化_参数1)
        SetTrackValue(锐化.ETB_锐化参数2, a.视频参数_锐化_参数2)
        SetTrackValue(锐化.ETB_锐化参数3, a.视频参数_锐化_参数3)

        胶片颗粒.MCB_胶片颗粒总开关.Checked = a.视频参数_胶片颗粒_方式 <> 预设数据_v6.胶片颗粒方式.未选择
        胶片颗粒.MCB_滤镜选择.SelectedIndex = EnumToIndex(a.视频参数_胶片颗粒_方式)
        SetTrackValue(胶片颗粒.ETB_胶片颗粒参数1, a.视频参数_胶片颗粒_参数1)
        SetTrackValue(胶片颗粒.ETB_胶片颗粒参数2, a.视频参数_胶片颗粒_参数2)
        SetTrackValue(胶片颗粒.ETB_胶片颗粒参数3, a.视频参数_胶片颗粒_参数3)
        SetTrackValue(胶片颗粒.ETB_胶片颗粒参数4, a.视频参数_胶片颗粒_参数4)

        平滑断层.MCB_平滑断层总开关.Checked = a.视频参数_平滑断层_方式 <> 预设数据_v6.平滑断层方式.未选择
        平滑断层.MCB_滤镜选择.SelectedIndex = EnumToIndex(a.视频参数_平滑断层_方式)
        SetTrackValue(平滑断层.ETB_平滑断层参数1, a.视频参数_平滑断层_参数1)
        SetTrackValue(平滑断层.ETB_平滑断层参数2, a.视频参数_平滑断层_参数2)
        SetTrackValue(平滑断层.ETB_平滑断层参数3, a.视频参数_平滑断层_参数3)
        SetTrackValue(平滑断层.ETB_平滑断层参数4, a.视频参数_平滑断层_参数4)

        扫描.MCB_扫描方式总开关.Checked = a.视频参数_处理扫描方式 <> 预设数据_v6.扫描方式.未选择
        扫描.MCB_扫描方式.SelectedIndex = EnumToIndex(a.视频参数_处理扫描方式)
        翻转.MCB_画面翻转总开关.Checked = a.视频参数_画面翻转_角度翻转 <> 预设数据_v6.画面翻转角度.未选择 OrElse a.视频参数_画面翻转_镜像翻转 <> 预设数据_v6.画面翻转镜像.未选择
        翻转.MCB_角度翻转.SelectedIndex = EnumToIndex(a.视频参数_画面翻转_角度翻转)
        翻转.MCB_镜像翻转.SelectedIndex = EnumToIndex(a.视频参数_画面翻转_镜像翻转)

        烧字幕.MCB_插帧总开关.Checked = a.视频参数_烧录字幕_滤镜选择 <> 预设数据_v6.烧字幕滤镜.未选择
        烧字幕.MCB_滤镜选择.SelectedIndex = EnumToIndex(a.视频参数_烧录字幕_滤镜选择)
        烧字幕.MCB_字幕来源.SelectedIndex = EnumToIndex(a.视频参数_烧录字幕_字幕来源是外部文件)
        Dim 字幕格式 = If(a.视频参数_烧录字幕_字幕格式优先级, New List(Of 预设数据_v6.烧字幕格式))
        烧字幕.MCB_后缀优先级1.SelectedIndex = If(字幕格式.Count > 0, EnumToIndex(字幕格式(0)), 0)
        烧字幕.MCB_后缀优先级2.SelectedIndex = If(字幕格式.Count > 1, EnumToIndex(字幕格式(1)), 0)
        烧字幕.MCB_后缀优先级3.SelectedIndex = If(字幕格式.Count > 2, EnumToIndex(字幕格式(2)), 0)
        烧字幕.MTB_字幕文件多余字符.Text = a.视频参数_烧录字幕_外部字幕文件名
        烧字幕.MCB_字幕文件路径.Text = a.视频参数_烧录字幕_外部字幕文件夹位置
        烧字幕.MTB_内嵌的流索引.Text = a.视频参数_烧录字幕_指定内嵌的流
        烧字幕.设置基本样式(a.视频参数_烧录字幕_基本样式_名称,
                         a.视频参数_烧录字幕_基本样式_大小,
                         a.视频参数_烧录字幕_基本样式_粗体,
                         a.视频参数_烧录字幕_基本样式_斜体,
                         a.视频参数_烧录字幕_基本样式_下划线,
                         a.视频参数_烧录字幕_基本样式_删除线)
        烧字幕.MCB_边框类型.SelectedIndex = EnumToIndex(a.视频参数_烧录字幕_边框样式)
        烧字幕.MCB_字体文件夹路径.Text = a.视频参数_烧录字幕_字体文件夹
        烧字幕.MTB_描边宽度.Text = a.视频参数_烧录字幕_描边宽度
        烧字幕.MTB_阴影距离.Text = a.视频参数_烧录字幕_阴影距离
        读取字幕颜色(a.视频参数_烧录字幕_主要颜色, AddressOf 烧字幕.设置主要颜色)
        读取字幕颜色(a.视频参数_烧录字幕_次要颜色, AddressOf 烧字幕.设置次要颜色)
        读取字幕颜色(a.视频参数_烧录字幕_描边颜色, AddressOf 烧字幕.设置描边颜色)
        读取字幕颜色(a.视频参数_烧录字幕_背景颜色, AddressOf 烧字幕.设置背景颜色)
        烧字幕.MCB_对齐方位.SelectedIndex = EnumToIndex(a.视频参数_烧录字幕_对齐方位)
        烧字幕.MTB_垂直边距.Text = a.视频参数_烧录字幕_垂直边距
        烧字幕.MTB_左边距.Text = a.视频参数_烧录字幕_左边距
        烧字幕.MTB_右边距.Text = a.视频参数_烧录字幕_右边距
        烧字幕.MTB_字距.Text = a.视频参数_烧录字幕_字距
        烧字幕.MTB_行距.Text = a.视频参数_烧录字幕_行距
        烧字幕.MTB_补充样式.Text = a.视频参数_烧录字幕_补充样式
        烧字幕.MTB_自己写整个滤镜.Text = a.视频参数_烧录字幕_自己写滤镜取代所有设置
    End Sub

    Private Shared Sub 储存质量(a As 预设数据_v6, ui As Form_v6_参数面板)
        With ui.私有界面_质量
            a.视频参数_比特率_控制方式 = SelectedIndexToEnum(Of 预设数据_v6.视频全局质量控制方式)(Math.Max(0, .MCB_全局质量控制方式.SelectedIndex))
            a.视频参数_质量控制_参数名 = .MCB_质量参数名称.Text.TrimStart("-"c)
            a.视频参数_质量控制_值 = .MTB_质量值.Text
            a.视频参数_比特率_基础 = .MTB_基础比特率.Text
            a.视频参数_比特率_最低值 = .MTB_最低比特率.Text
            a.视频参数_比特率_最高值 = .MTB_最高比特率.Text
            a.视频参数_比特率_缓冲区 = .MTB_缓冲区.Text
            a.视频参数_质量控制_进阶参数集 = .ModernTextBox6.Text
        End With
        If a.视频参数_比特率_控制方式 = 预设数据_v6.视频全局质量控制方式.TPE AndAlso Not 当前编码器支持二次编码(a) Then
            a.视频参数_比特率_控制方式 = 预设数据_v6.视频全局质量控制方式.未选择
        End If
    End Sub

    Private Shared Sub 显示质量(a As 预设数据_v6, ui As Form_v6_参数面板)
        With ui.私有界面_质量
            .MCB_全局质量控制方式.SelectedIndex = EnumToIndex(a.视频参数_比特率_控制方式)
            .MCB_质量参数名称.Text = If(a.视频参数_质量控制_参数名 <> "" AndAlso Not a.视频参数_质量控制_参数名.StartsWith("-"), "-" & a.视频参数_质量控制_参数名, a.视频参数_质量控制_参数名)
            .MTB_质量值.Text = a.视频参数_质量控制_值
            .MTB_基础比特率.Text = a.视频参数_比特率_基础
            .MTB_最低比特率.Text = a.视频参数_比特率_最低值
            .MTB_最高比特率.Text = a.视频参数_比特率_最高值
            .MTB_缓冲区.Text = a.视频参数_比特率_缓冲区
            .ModernTextBox6.Text = a.视频参数_质量控制_进阶参数集
        End With
    End Sub

    Private Shared Sub 储存色彩管理(a As 预设数据_v6, ui As Form_v6_参数面板)
        With ui.私有界面_色彩管理
            a.视频参数_色彩管理_像素格式 = .MCB_像素格式.Text
            a.视频参数_色彩管理_像素格式预先转换 = .MCB_像素格式预先转换.Text
            a.视频参数_色彩管理_滤镜选择 = .MCB_色彩管理_选择滤镜.Text
            a.视频参数_色彩管理_矩阵系数 = .MCB_色彩管理_矩阵系数.Text
            a.视频参数_色彩管理_色域 = .MCB_色彩管理_色域.Text
            a.视频参数_色彩管理_传输特性 = .MCB_色彩管理_传输特性.Text
            a.视频参数_色彩管理_范围 = .MCB_色彩管理_色彩范围.Text
            a.视频参数_色彩管理_色调映射算法 = .MCB_色彩管理_色调映射算法.Text
            a.视频参数_色彩管理_处理方式 = .MCB_色彩管理_色彩空间操作方式.Text
            a.视频参数_色彩管理_启用调整亮度 = .MCB_亮度.Checked
            a.视频参数_色彩管理_亮度 = TrackValue(.ETB_亮度)
            a.视频参数_色彩管理_启用调整对比度 = .MCB_对比度.Checked
            a.视频参数_色彩管理_对比度 = TrackValue(.ETB_对比度)
            a.视频参数_色彩管理_启用调整饱和度 = .MCB_饱和度.Checked
            a.视频参数_色彩管理_饱和度 = TrackValue(.ETB_饱和度)
            a.视频参数_色彩管理_启用调整伽马 = .MCB_伽马.Checked
            a.视频参数_色彩管理_伽马 = TrackValue(.ETB_伽马)
        End With
    End Sub

    Private Shared Sub 显示色彩管理(a As 预设数据_v6, ui As Form_v6_参数面板)
        With ui.私有界面_色彩管理
            .MCB_像素格式.Text = a.视频参数_色彩管理_像素格式
            .MCB_像素格式预先转换.Text = a.视频参数_色彩管理_像素格式预先转换
            .MCB_色彩管理_选择滤镜.Text = a.视频参数_色彩管理_滤镜选择
            .MCB_色彩管理_矩阵系数.Text = a.视频参数_色彩管理_矩阵系数
            .MCB_色彩管理_色域.Text = a.视频参数_色彩管理_色域
            .MCB_色彩管理_传输特性.Text = a.视频参数_色彩管理_传输特性
            .MCB_色彩管理_色彩范围.Text = a.视频参数_色彩管理_范围
            .MCB_色彩管理_色调映射算法.Text = a.视频参数_色彩管理_色调映射算法
            .MCB_色彩管理_色彩空间操作方式.Text = a.视频参数_色彩管理_处理方式
            .MCB_亮度.Checked = a.视频参数_色彩管理_启用调整亮度
            SetTrackValue(.ETB_亮度, a.视频参数_色彩管理_亮度)
            .MCB_对比度.Checked = a.视频参数_色彩管理_启用调整对比度
            SetTrackValue(.ETB_对比度, a.视频参数_色彩管理_对比度)
            .MCB_饱和度.Checked = a.视频参数_色彩管理_启用调整饱和度
            SetTrackValue(.ETB_饱和度, a.视频参数_色彩管理_饱和度)
            .MCB_伽马.Checked = a.视频参数_色彩管理_启用调整伽马
            SetTrackValue(.ETB_伽马, a.视频参数_色彩管理_伽马)
        End With
    End Sub

    Private Shared Sub 储存音频参数(a As 预设数据_v6, ui As Form_v6_参数面板)
        With ui.私有界面_音频参数
            a.音频参数_编码器_具体编码 = 音频编码器数据库_v6.获取私有ID(.MCB_音频编码器.Text)
            If a.音频参数_编码器_具体编码 = "" Then a.音频参数_编码器_具体编码 = .MCB_音频编码器.Text
            a.音频参数_比特率 = .MCB_比特率.Text
            a.音频参数_质量参数名 = .MCB_质量参数名.Text
            a.音频参数_质量值 = .MCB_质量值.Text
            a.音频参数_声道数 = .声道布局.Text
            a.音频参数_位深度 = .MCB_位深度.Text
            a.音频参数_采样率 = .MCB_采样率.Text
            a.音频参数_响度标准化_启用调整目标响度 = .MCB_目标响度.Checked
            a.音频参数_响度标准化_目标响度 = TrackValue(.ETB_目标响度)
            a.音频参数_响度标准化_启用调整动态范围 = .MCB_动态范围.Checked
            a.音频参数_响度标准化_动态范围 = TrackValue(.ETB_动态范围)
            a.音频参数_响度标准化_启用调整峰值电平 = .MCB_峰值电平.Checked
            a.音频参数_响度标准化_峰值电平 = TrackValue(.ETB_峰值电平)
        End With
    End Sub

    Private Shared Sub 显示音频参数(a As 预设数据_v6, ui As Form_v6_参数面板)
        With ui.私有界面_音频参数
            Dim 音频编码器显示名称 = 音频编码器数据库_v6.获取显示名称(a.音频参数_编码器_具体编码)
            If 音频编码器显示名称 = "" Then 音频编码器显示名称 = a.音频参数_编码器_具体编码
            设置组合框文本并尝试选中(.MCB_音频编码器, 音频编码器显示名称)
            .MCB_比特率.Text = a.音频参数_比特率
            设置组合框文本并尝试选中(.MCB_质量参数名, a.音频参数_质量参数名)
            .MCB_质量值.Text = a.音频参数_质量值
            .声道布局.Text = a.音频参数_声道数
            .MCB_位深度.Text = a.音频参数_位深度
            .MCB_采样率.Text = a.音频参数_采样率
            .MCB_目标响度.Checked = a.音频参数_响度标准化_启用调整目标响度
            SetTrackValue(.ETB_目标响度, a.音频参数_响度标准化_目标响度)
            .MCB_动态范围.Checked = a.音频参数_响度标准化_启用调整动态范围
            SetTrackValue(.ETB_动态范围, a.音频参数_响度标准化_动态范围)
            .MCB_峰值电平.Checked = a.音频参数_响度标准化_启用调整峰值电平
            SetTrackValue(.ETB_峰值电平, a.音频参数_响度标准化_峰值电平)
        End With
    End Sub

    Private Shared Sub 储存剪辑(a As 预设数据_v6, ui As Form_v6_参数面板)
        With ui.私有界面_剪辑区间
            a.剪辑区间_方法 = SelectedIndexToEnum(Of 预设数据_v6.剪辑方法)(Math.Max(0, .MCB_剪辑模式.SelectedIndex))
            a.剪辑区间_入点 = .MTB_入点.Text
            a.剪辑区间_出点 = .MTB_出点.Text
            a.剪辑区间_向前解码多久秒 = .MCB_向前解码秒数.Text
        End With
    End Sub

    Private Shared Sub 显示剪辑(a As 预设数据_v6, ui As Form_v6_参数面板)
        With ui.私有界面_剪辑区间
            .MCB_剪辑模式.SelectedIndex = EnumToIndex(a.剪辑区间_方法)
            .MTB_入点.Text = a.剪辑区间_入点
            .MTB_出点.Text = a.剪辑区间_出点
            .MCB_向前解码秒数.Text = a.剪辑区间_向前解码多久秒
        End With
    End Sub

    Private Shared Sub 储存自定义参数(a As 预设数据_v6, ui As Form_v6_参数面板)
        a.自定义参数_视频参数 = ui.私有界面_流自定义参数.ModernTextBox1.Text
        a.自定义参数_音频参数 = ui.私有界面_流自定义参数.ModernTextBox2.Text
        a.自定义参数_开头参数 = ui.私有界面_在位置插入参数.ModernTextBox1.Text
        a.自定义参数_之前参数 = ui.私有界面_在位置插入参数.ModernTextBox2.Text
        a.自定义参数_之后参数 = ui.私有界面_在位置插入参数.ModernTextBox3.Text
        a.自定义参数_最后参数 = ui.私有界面_在位置插入参数.ModernTextBox4.Text
        a.自定义参数_完全自己写 = ui.私有界面_完全自己写模式.ModernTextBox1.Text
    End Sub

    Private Shared Sub 显示自定义参数(a As 预设数据_v6, ui As Form_v6_参数面板)
        ui.私有界面_流自定义参数.ModernTextBox1.Text = a.自定义参数_视频参数
        ui.私有界面_流自定义参数.ModernTextBox2.Text = a.自定义参数_音频参数
        ui.私有界面_在位置插入参数.ModernTextBox1.Text = a.自定义参数_开头参数
        ui.私有界面_在位置插入参数.ModernTextBox2.Text = a.自定义参数_之前参数
        ui.私有界面_在位置插入参数.ModernTextBox3.Text = a.自定义参数_之后参数
        ui.私有界面_在位置插入参数.ModernTextBox4.Text = a.自定义参数_最后参数
        ui.私有界面_完全自己写模式.ModernTextBox1.Text = a.自定义参数_完全自己写
    End Sub

    Private Shared Sub 储存视频帧服务器(a As 预设数据_v6, ui As Form_v6_参数面板)
        With ui.私有界面_视频帧服务器
            a.视频参数_视频帧服务器_使用AviSynth = .BooleanSwitch1.Checked AndAlso .ModernComboBox1.Text.Trim() <> ""
            a.视频参数_视频帧服务器_avs脚本文件 = .ModernComboBox1.Text.Trim()
            a.视频参数_视频帧服务器_使用VapourSynth = .BooleanSwitch2.Checked AndAlso .ModernComboBox2.Text.Trim() <> ""
            a.视频参数_视频帧服务器_vpy脚本文件 = .ModernComboBox2.Text.Trim()
            If a.视频参数_视频帧服务器_使用AviSynth Then a.视频参数_视频帧服务器_使用VapourSynth = False
        End With
    End Sub

    Private Shared Sub 显示视频帧服务器(a As 预设数据_v6, ui As Form_v6_参数面板)
        With ui.私有界面_视频帧服务器
            .BooleanSwitch1.Checked = a.视频参数_视频帧服务器_使用AviSynth
            .ModernComboBox1.Text = a.视频参数_视频帧服务器_avs脚本文件
            .BooleanSwitch2.Checked = a.视频参数_视频帧服务器_使用VapourSynth AndAlso Not a.视频参数_视频帧服务器_使用AviSynth
            .ModernComboBox2.Text = a.视频参数_视频帧服务器_vpy脚本文件
        End With
    End Sub

    Private Shared Sub 储存附加内容(a As 预设数据_v6, ui As Form_v6_参数面板)
        a.元数据_要写入的信息 = ui.私有界面_元数据.获取数据().ToArray()
        With ui.私有界面_章节
            a.章节_来源 = SelectedIndexToEnum(Of 预设数据_v6.章节来源)(Math.Max(0, .ModernComboBox1.SelectedIndex))
            a.章节_文件路径 = .ModernComboBox2.Text.Trim()
        End With
        a.附件_要写入的附件 = ui.私有界面_附件.获取数据().ToArray()
    End Sub

    Private Shared Sub 显示附加内容(a As 预设数据_v6, ui As Form_v6_参数面板)
        ui.私有界面_元数据.设置数据(a.元数据_要写入的信息)
        With ui.私有界面_章节
            .ModernComboBox1.SelectedIndex = EnumToIndex(a.章节_来源)
            .ModernComboBox2.Text = a.章节_文件路径
        End With
        ui.私有界面_附件.设置数据(a.附件_要写入的附件)
    End Sub

    Private Shared Sub 储存流控制(a As 预设数据_v6, ui As Form_v6_参数面板)
        With ui.私有界面_流控制
            a.流控制_将视频参数应用于指定流 = SplitTextList(.ModernTextBox1.Text)
            a.流控制_启用保留其他视频流 = .ModernCheckBox1.Checked
            a.流控制_将音频参数应用于指定流 = SplitTextList(.ModernTextBox2.Text)
            a.流控制_启用保留其他音频流 = .ModernCheckBox2.Checked
            a.流控制_将字幕参数应用于指定流 = SplitTextList(.ModernTextBox3.Text)
            a.流控制_如何操作指定的字幕 = Math.Max(0, .ModernComboBox2.SelectedIndex)
            a.流控制_启用保留其他字幕流 = .ModernCheckBox3.Checked
            a.流控制_自动混流SRT = .ModernCheckBox4.Checked
            a.流控制_自动混流ASS = .ModernCheckBox5.Checked
            a.流控制_自动混流SSA = .ModernCheckBox6.Checked
            a.流控制_自动混流的字幕转为MOVTEXT = .ModernCheckBox7.Checked
            a.流控制_元数据选项 = Math.Max(0, .ModernComboBox1.SelectedIndex)
            a.流控制_章节选项 = Math.Max(0, .ModernComboBox3.SelectedIndex)
            a.流控制_附件选项 = Math.Max(0, .ModernComboBox4.SelectedIndex)
        End With
    End Sub

    Private Shared Sub 显示流控制(a As 预设数据_v6, ui As Form_v6_参数面板)
        With ui.私有界面_流控制
            .ModernTextBox1.Text = String.Join(",", a.流控制_将视频参数应用于指定流)
            .ModernCheckBox1.Checked = a.流控制_启用保留其他视频流
            .ModernTextBox2.Text = String.Join(",", a.流控制_将音频参数应用于指定流)
            .ModernCheckBox2.Checked = a.流控制_启用保留其他音频流
            .ModernTextBox3.Text = String.Join(",", a.流控制_将字幕参数应用于指定流)
            .ModernComboBox2.SelectedIndex = a.流控制_如何操作指定的字幕
            .ModernCheckBox3.Checked = a.流控制_启用保留其他字幕流
            .ModernCheckBox4.Checked = a.流控制_自动混流SRT
            .ModernCheckBox5.Checked = a.流控制_自动混流ASS
            .ModernCheckBox6.Checked = a.流控制_自动混流SSA
            .ModernCheckBox7.Checked = a.流控制_自动混流的字幕转为MOVTEXT
            .ModernComboBox1.SelectedIndex = a.流控制_元数据选项
            .ModernComboBox3.SelectedIndex = a.流控制_章节选项
            .ModernComboBox4.SelectedIndex = a.流控制_附件选项
        End With
    End Sub

End Class
