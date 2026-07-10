Imports System.Globalization
Imports System.IO
Imports System.Text
Imports System.Text.Json
Imports LakeUI

Partial Public Class 预设管理_v6

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
                Return ""
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

    Public Shared Function 预设包含输出位置(数据 As 预设数据_v6) As Boolean
        Return 数据 IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(数据.输出位置)
    End Function

    Public Shared Function 可使用预设输出位置(数据 As 预设数据_v6) As Boolean
        If 数据 Is Nothing Then Return False
        Dim 输出位置文本 = If(数据.输出位置, "").Trim()
        If 输出位置文本 = "" Then Return False
        Dim 保存机器 = If(数据.计算机名称, "").Trim()
        If 保存机器 <> "" AndAlso Not String.Equals(保存机器, Environment.MachineName, StringComparison.OrdinalIgnoreCase) Then Return False
        Return Directory.Exists(输出位置文本)
    End Function

    Public Shared Sub 写入预设文件(文件路径 As String, 数据 As 预设数据_v6, Optional 保存输出位置 As Boolean? = Nothing)
        初始化空集合(数据)
        Directory.CreateDirectory(Path.GetDirectoryName(文件路径))
        Dim 原计算机名称 = 数据.计算机名称
        Dim 原输出位置 = 数据.输出位置
        Dim 原保留子文件夹结构起始点 = 数据.输出位置_保留子文件夹结构起始点
        Dim 原额外保存输出位置 = 数据.额外保存输出位置
        Dim 原运行时使用输出位置 = 数据.运行时使用输出位置
        Dim 应保存输出位置 = If(保存输出位置.HasValue, 保存输出位置.Value, 预设包含输出位置(数据))
        Try
            数据.额外保存输出位置 = False
            数据.运行时使用输出位置 = False
            If Not 应保存输出位置 Then
                数据.计算机名称 = ""
                数据.输出位置 = ""
                数据.输出位置_保留子文件夹结构起始点 = ""
            End If
            File.WriteAllText(文件路径, JsonSerializer.Serialize(数据, JsonSO), Encoding.UTF8)
        Finally
            数据.计算机名称 = 原计算机名称
            数据.输出位置 = 原输出位置
            数据.输出位置_保留子文件夹结构起始点 = 原保留子文件夹结构起始点
            数据.额外保存输出位置 = 原额外保存输出位置
            数据.运行时使用输出位置 = 原运行时使用输出位置
        End Try
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
        同步内置滤镜排序数据(a)
    End Sub
End Class
