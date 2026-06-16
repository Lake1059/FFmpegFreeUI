Imports System.IO
Imports LakeUI
Imports Microsoft.WindowsAPICodePack.Dialogs

Public Class Form_v6_参数面板_烧录字幕

    Private Class 字幕颜色状态
        Public Property 已设置 As Boolean = False
        Public Property 颜色 As Color = Color.Black
    End Class

    Private _基本样式字体 As Font = Nothing
    Private ReadOnly _主要颜色 As New 字幕颜色状态()
    Private ReadOnly _次要颜色 As New 字幕颜色状态()
    Private ReadOnly _描边颜色 As New 字幕颜色状态()
    Private ReadOnly _背景颜色 As New 字幕颜色状态()
    Private _正在选择路径 As Boolean = False

    Public ReadOnly Property 基本样式已设置 As Boolean
        Get
            Return _基本样式字体 IsNot Nothing
        End Get
    End Property

    Public ReadOnly Property 基本样式字体 As Font
        Get
            Return _基本样式字体
        End Get
    End Property

    Public ReadOnly Property 主要颜色已设置 As Boolean
        Get
            Return _主要颜色.已设置
        End Get
    End Property

    Public ReadOnly Property 主要颜色 As Color
        Get
            Return _主要颜色.颜色
        End Get
    End Property

    Public ReadOnly Property 次要颜色已设置 As Boolean
        Get
            Return _次要颜色.已设置
        End Get
    End Property

    Public ReadOnly Property 次要颜色 As Color
        Get
            Return _次要颜色.颜色
        End Get
    End Property

    Public ReadOnly Property 描边颜色已设置 As Boolean
        Get
            Return _描边颜色.已设置
        End Get
    End Property

    Public ReadOnly Property 描边颜色 As Color
        Get
            Return _描边颜色.颜色
        End Get
    End Property

    Public ReadOnly Property 背景颜色已设置 As Boolean
        Get
            Return _背景颜色.已设置
        End Get
    End Property

    Public ReadOnly Property 背景颜色 As Color
        Get
            Return _背景颜色.颜色
        End Get
    End Property

    Private Sub Form_v6_参数面板_烧录字幕_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = FormMain_v6.Icon
        SetControlFont(设置_v6.实例对象.字体, Me, , True)
        初始化状态按钮()
        MCB_字幕文件路径.AllowDrop = True
        MCB_字体文件夹路径.AllowDrop = True
        If FormMain_v6.ThisIsYourWindow1.AttachedForms.Count > 0 Then
            FormMain_v6.ThisIsYourWindow1.Attach(Me)
            ModernPanel1.BackColor = Color.Transparent
            ModernPanel1.BackColor1 = Color.Transparent
        End If
    End Sub

    Private Sub Form_v6_参数面板_烧录字幕_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = True
        Me.Hide()
    End Sub

    Private Sub Form_v6_参数面板_烧录字幕_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
        Panel1.Width = 320 * DPI()
        MCB_滤镜选择.Width = (Panel1.Width - JustEmptyControl1.Width) * 0.5
        MCB_后缀优先级1.Width = (Panel1.Width - JustEmptyControl2.Width - JustEmptyControl3.Width) / 3
        MCB_后缀优先级2.Width = MCB_后缀优先级1.Width
        Panel2.Width = 350 * DPI()
        Panel3.Width = 150 * DPI()
    End Sub

    Function DPI() As Double
        Return Me.DeviceDpi / 96
    End Function

    Public Sub 设置基本样式(字体 As Font)
        If _基本样式字体 IsNot Nothing Then
            _基本样式字体.Dispose()
            _基本样式字体 = Nothing
        End If
        If 字体 IsNot Nothing Then _基本样式字体 = DirectCast(字体.Clone(), Font)
        更新基本样式按钮()
    End Sub

    Public Sub 设置基本样式(名称 As String, 大小 As Double, 粗体 As Boolean, 斜体 As Boolean, 下划线 As Boolean, 删除线 As Boolean)
        If String.IsNullOrWhiteSpace(名称) OrElse 大小 <= 0 Then
            清除基本样式()
            Exit Sub
        End If

        Dim 样式 = FontStyle.Regular
        If 粗体 Then 样式 = 样式 Or FontStyle.Bold
        If 斜体 Then 样式 = 样式 Or FontStyle.Italic
        If 下划线 Then 样式 = 样式 Or FontStyle.Underline
        If 删除线 Then 样式 = 样式 Or FontStyle.Strikeout

        Try
            Using f As New Font(名称, CSng(大小), 样式)
                设置基本样式(f)
            End Using
        Catch
            清除基本样式()
        End Try
    End Sub

    Public Sub 清除基本样式()
        设置基本样式(CType(Nothing, Font))
    End Sub

    Public Sub 设置主要颜色(颜色 As Color, Optional 已设置 As Boolean = True)
        设置颜色(_主要颜色, MB_设置主要颜色, 颜色, 已设置)
    End Sub

    Public Sub 设置次要颜色(颜色 As Color, Optional 已设置 As Boolean = True)
        设置颜色(_次要颜色, MB_设置次要颜色, 颜色, 已设置)
    End Sub

    Public Sub 设置描边颜色(颜色 As Color, Optional 已设置 As Boolean = True)
        设置颜色(_描边颜色, MB_设置描边颜色, 颜色, 已设置)
    End Sub

    Public Sub 设置背景颜色(颜色 As Color, Optional 已设置 As Boolean = True)
        设置颜色(_背景颜色, MB_设置背景颜色, 颜色, 已设置)
    End Sub

    Public Sub 清除主要颜色()
        设置主要颜色(Color.Black, False)
    End Sub

    Public Sub 清除次要颜色()
        设置次要颜色(Color.Black, False)
    End Sub

    Public Sub 清除描边颜色()
        设置描边颜色(Color.Black, False)
    End Sub

    Public Sub 清除背景颜色()
        设置背景颜色(Color.Black, False)
    End Sub

    Public Sub 重置所有选项()
        MCB_插帧总开关.Checked = False
        MCB_滤镜选择.SelectedIndex = 0
        MCB_字幕来源.SelectedIndex = 0
        MCB_后缀优先级1.SelectedIndex = 0
        MCB_后缀优先级2.SelectedIndex = 0
        MCB_后缀优先级3.SelectedIndex = 0
        MTB_字幕文件多余字符.Text = ""
        MCB_字幕文件路径.Text = ""
        MTB_内嵌的流索引.Text = ""
        清除基本样式()
        MCB_边框类型.SelectedIndex = 0
        MTB_描边宽度.Text = ""
        MTB_阴影距离.Text = ""
        清除主要颜色()
        清除次要颜色()
        清除描边颜色()
        清除背景颜色()
        MCB_字体文件夹路径.Text = ""
        MCB_对齐方位.SelectedIndex = 0
        MTB_垂直边距.Text = ""
        MTB_左边距.Text = ""
        MTB_右边距.Text = ""
        MTB_字距.Text = ""
        MTB_行距.Text = ""
        MTB_补充样式.Text = ""
        MTB_自己写整个滤镜.Text = ""
    End Sub

    Private Sub 初始化状态按钮()
        For Each btn In {MB_设置基本样式, MB_设置主要颜色, MB_设置次要颜色, MB_设置描边颜色, MB_设置背景颜色}
            btn.MainSubTextSpacing = 2
            btn.SubTextSize = 9
            btn.SubTextForeColor = Color.FromArgb(150, 255, 255, 255)
        Next
        更新基本样式按钮()
        更新颜色按钮(MB_设置主要颜色, _主要颜色)
        更新颜色按钮(MB_设置次要颜色, _次要颜色)
        更新颜色按钮(MB_设置描边颜色, _描边颜色)
        更新颜色按钮(MB_设置背景颜色, _背景颜色)
    End Sub

    Private Sub 更新基本样式按钮()
        If _基本样式字体 Is Nothing Then
            MB_设置基本样式.SubText = "未设置"
            MB_设置基本样式.SubTextForeColor = Color.FromArgb(150, 255, 255, 255)
            Exit Sub
        End If

        Dim 样式 As New List(Of String)
        If _基本样式字体.Bold Then 样式.Add("粗体")
        If _基本样式字体.Italic Then 样式.Add("斜体")
        If _基本样式字体.Underline Then 样式.Add("下划线")
        If _基本样式字体.Strikeout Then 样式.Add("删除线")
        Dim 样式文本 = If(样式.Count > 0, String.Join(" / ", 样式), "常规")
        MB_设置基本样式.SubText = $"{_基本样式字体.Name}, {_基本样式字体.Size.ToString("0.##", Globalization.CultureInfo.InvariantCulture)} pt, {样式文本}"
        MB_设置基本样式.SubTextForeColor = Color.Silver
    End Sub

    Private Sub 设置颜色(状态 As 字幕颜色状态, 按钮 As ModernButton, 颜色 As Color, 已设置 As Boolean)
        状态.已设置 = 已设置
        状态.颜色 = If(已设置, 颜色, Color.Black)
        更新颜色按钮(按钮, 状态)
    End Sub

    Private Sub 更新颜色按钮(按钮 As ModernButton, 状态 As 字幕颜色状态)
        If Not 状态.已设置 Then
            按钮.SubText = "未设置"
            按钮.SubTextForeColor = Color.FromArgb(150, 255, 255, 255)
            Exit Sub
        End If

        Dim c = 状态.颜色
        按钮.SubText = $"&H{c.A:X2}{c.B:X2}{c.G:X2}{c.R:X2}  ARGB({c.A}, {c.R}, {c.G}, {c.B})"
        按钮.SubTextForeColor = Color.Silver
    End Sub

    Private Sub MB_设置基本样式_Click(sender As Object, e As EventArgs) Handles MB_设置基本样式.Click
        Dim d As New ModernFontDialog With {.SelectedFont = If(_基本样式字体, Me.Font), .Icon = FormMain_v6.Icon}
        FormMain_v6.ThisIsYourWindow1.Attach(d)
        If d.ShowDialog(Me) = DialogResult.OK Then 设置基本样式(d.SelectedFont)
    End Sub

    Private Sub MB_清除基本样式_Click(sender As Object, e As EventArgs) Handles MB_清除基本样式.Click
        清除基本样式()
    End Sub

    Private Sub MB_设置主要颜色_Click(sender As Object, e As EventArgs) Handles MB_设置主要颜色.Click
        打开颜色对话框(_主要颜色, AddressOf 设置主要颜色)
    End Sub

    Private Sub MB_清除主要颜色_Click(sender As Object, e As EventArgs) Handles MB_清除主要颜色.Click
        清除主要颜色()
    End Sub

    Private Sub MB_设置次要颜色_Click(sender As Object, e As EventArgs) Handles MB_设置次要颜色.Click
        打开颜色对话框(_次要颜色, AddressOf 设置次要颜色)
    End Sub

    Private Sub MB_清除次要颜色_Click(sender As Object, e As EventArgs) Handles MB_清除次要颜色.Click
        清除次要颜色()
    End Sub

    Private Sub MB_设置描边颜色_Click(sender As Object, e As EventArgs) Handles MB_设置描边颜色.Click
        打开颜色对话框(_描边颜色, AddressOf 设置描边颜色)
    End Sub

    Private Sub MB_清除描边颜色_Click(sender As Object, e As EventArgs) Handles MB_清除描边颜色.Click
        清除描边颜色()
    End Sub

    Private Sub MB_设置背景颜色_Click(sender As Object, e As EventArgs) Handles MB_设置背景颜色.Click
        打开颜色对话框(_背景颜色, AddressOf 设置背景颜色)
    End Sub

    Private Sub MB_清楚背景颜色_Click(sender As Object, e As EventArgs) Handles MB_清楚背景颜色.Click
        清除背景颜色()
    End Sub

    Private Sub 打开颜色对话框(状态 As 字幕颜色状态, 设置动作 As Action(Of Color, Boolean))
        Dim d As New ModernColorDialog With {.SelectedColor = If(状态.已设置, 状态.颜色, Color.White), .Icon = FormMain_v6.Icon}
        FormMain_v6.ThisIsYourWindow1.Attach(d)
        If d.ShowDialog(Me) = DialogResult.OK Then 设置动作.Invoke(d.SelectedColor, True)
    End Sub

    Private Sub MCB_字幕来源_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_字幕来源.SelectedIndexChanged
        Select Case MCB_字幕来源.SelectedIndex
            Case 1
                MTB_内嵌的流索引.Text = ""
            Case 2
                MTB_字幕文件多余字符.Text = ""
                MCB_字幕文件路径.Text = ""
        End Select
    End Sub

    Private Sub MCB_字幕文件路径_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_字幕文件路径.SelectedIndexChanged
        If _正在选择路径 Then Exit Sub
        If MCB_字幕文件路径.SelectedIndex = 0 Then 选择文件夹(MCB_字幕文件路径)
    End Sub

    Private Sub MCB_字体文件夹路径_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_字体文件夹路径.SelectedIndexChanged
        If _正在选择路径 Then Exit Sub
        If MCB_字体文件夹路径.SelectedIndex = 0 Then 选择文件夹(MCB_字体文件夹路径)
    End Sub

    Private Sub 路径_DragEnter(sender As Object, e As DragEventArgs) Handles MCB_字幕文件路径.DragEnter, MCB_字体文件夹路径.DragEnter
        e.Effect = If(e.Data.GetDataPresent(DataFormats.FileDrop), DragDropEffects.Copy, DragDropEffects.None)
    End Sub

    Private Sub 路径_DragDrop(sender As Object, e As DragEventArgs) Handles MCB_字幕文件路径.DragDrop, MCB_字体文件夹路径.DragDrop
        Dim combo = TryCast(sender, ModernComboBox)
        If combo Is Nothing Then Exit Sub
        Dim files = TryCast(e.Data.GetData(DataFormats.FileDrop), String())
        If files Is Nothing OrElse files.Length = 0 Then Exit Sub
        Dim p = files(0)
        If Directory.Exists(p) Then
            combo.Text = p
        ElseIf File.Exists(p) Then
            combo.Text = Path.GetDirectoryName(p)
        End If
    End Sub

    Private Sub 选择文件夹(combo As ModernComboBox)
        _正在选择路径 = True
        Try
            Dim dialog As New CommonOpenFileDialog With {.IsFolderPicker = True}
            Dim 当前路径 = combo.Text.Trim()
            If Directory.Exists(当前路径) Then dialog.InitialDirectory = 当前路径
            If dialog.ShowDialog() = CommonFileDialogResult.Ok Then
                combo.Text = dialog.FileName
            Else
                combo.SelectedIndex = -1
                combo.Text = ""
            End If
        Finally
            _正在选择路径 = False
        End Try
    End Sub

End Class
