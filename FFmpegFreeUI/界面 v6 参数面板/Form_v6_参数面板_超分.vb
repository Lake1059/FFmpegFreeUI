Imports System.IO
Imports FFmpegFreeUI.预设数据_v6

Public Class Form_v6_参数面板_超分

    Public 策略组数据 As New List(Of 超分数据单片结构)
    Private _正在选择文件 As Boolean = False

    Private Sub Form_v6_参数面板_超分_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = FormMain_v6.Icon
        SetControlFont(设置_v6.实例对象.字体, Me, , True)
        绑定路径下拉框拖拽(MCB_着色器文件路径)
        刷新策略组列表()
        If FormMain_v6.ThisIsYourWindow1.AttachedForms.Count > 0 Then
            FormMain_v6.ThisIsYourWindow1.Attach(Me)
            ModernPanel1.BackColor = Color.Transparent
            ModernPanel1.BackColor1 = Color.Transparent
        End If
    End Sub

    Private Sub Form_v6_参数面板_超分_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = True
        Me.Hide()
    End Sub

    Public Sub 刷新策略组列表()
        If ModernListBox1 Is Nothing Then Exit Sub
        Dim oldIndex = ModernListBox1.SelectedIndex
        ModernListBox1.Items.Clear()
        For i = 0 To 策略组数据.Count - 1
            ModernListBox1.Items.Add(格式化策略(i, 策略组数据(i)))
        Next
        If ModernListBox1.Items.Count > 0 Then ModernListBox1.SelectedIndex = Math.Min(Math.Max(0, oldIndex), ModernListBox1.Items.Count - 1)
    End Sub

    Private Function 从当前面板创建策略() As 超分数据单片结构
        Return New 超分数据单片结构 With {
            .目标宽度 = MTB_宽度.Text,
            .目标高度 = MTB_高度.Text,
            .上采样算法 = MCB_上采样算法.Text,
            .下采样算法 = MCB_下采样算法.Text,
            .抗振铃强度 = MTB_抗振铃强度.Text,
            .着色器文件路径 = MCB_着色器文件路径.Text
        }
    End Function

    Private Sub 读取策略到面板(策略 As 超分数据单片结构)
        If 策略 Is Nothing Then Exit Sub
        MTB_宽度.Text = 策略.目标宽度
        MTB_高度.Text = 策略.目标高度
        MCB_上采样算法.Text = 策略.上采样算法
        MCB_下采样算法.Text = 策略.下采样算法
        MTB_抗振铃强度.Text = 策略.抗振铃强度
        MCB_着色器文件路径.Text = 策略.着色器文件路径
        MCB_超分总开关.Checked = True
    End Sub

    Private Function 克隆策略(策略 As 超分数据单片结构) As 超分数据单片结构
        If 策略 Is Nothing Then Return New 超分数据单片结构
        Return New 超分数据单片结构 With {
            .目标宽度 = 策略.目标宽度,
            .目标高度 = 策略.目标高度,
            .上采样算法 = 策略.上采样算法,
            .下采样算法 = 策略.下采样算法,
            .抗振铃强度 = 策略.抗振铃强度,
            .着色器文件路径 = 策略.着色器文件路径
        }
    End Function

    Private Function 格式化策略(index As Integer, 策略 As 超分数据单片结构) As String
        If 策略 Is Nothing Then Return $"{index + 1}. 空策略"
        Dim parts As New List(Of String)
        If 策略.目标宽度 <> "" OrElse 策略.目标高度 <> "" Then parts.Add($"{If(策略.目标宽度 = "", "iw", 策略.目标宽度)}x{If(策略.目标高度 = "", "ih", 策略.目标高度)}")
        If 策略.上采样算法 <> "" Then parts.Add("up=" & 策略.上采样算法)
        If 策略.下采样算法 <> "" Then parts.Add("down=" & 策略.下采样算法)
        If 策略.抗振铃强度 <> "" Then parts.Add("anti=" & 策略.抗振铃强度)
        If 策略.着色器文件路径 <> "" Then parts.Add(Path.GetFileName(策略.着色器文件路径))
        If parts.Count = 0 Then parts.Add("空策略")
        Return $"{index + 1}. {String.Join(" | ", parts)}"
    End Function

    Private Sub MB_读取_Click(sender As Object, e As EventArgs) Handles MB_读取.Click
        Dim i = ModernListBox1.SelectedIndex
        If i >= 0 AndAlso i < 策略组数据.Count Then 读取策略到面板(策略组数据(i))
    End Sub

    Private Sub MB_保存_Click(sender As Object, e As EventArgs) Handles MB_保存.Click
        Dim item = 从当前面板创建策略()
        Dim i = ModernListBox1.SelectedIndex
        If i >= 0 AndAlso i < 策略组数据.Count Then
            策略组数据(i) = item
        Else
            策略组数据.Add(item)
            i = 策略组数据.Count - 1
        End If
        刷新策略组列表()
        ModernListBox1.SelectedIndex = i
    End Sub

    Private Sub MB_移除_Click(sender As Object, e As EventArgs) Handles MB_移除.Click
        Dim i = ModernListBox1.SelectedIndex
        If i < 0 OrElse i >= 策略组数据.Count Then Exit Sub
        策略组数据.RemoveAt(i)
        刷新策略组列表()
    End Sub

    Private Sub MB_克隆_Click(sender As Object, e As EventArgs) Handles MB_克隆.Click
        Dim i = ModernListBox1.SelectedIndex
        If i < 0 OrElse i >= 策略组数据.Count Then Exit Sub
        策略组数据.Insert(i + 1, 克隆策略(策略组数据(i)))
        刷新策略组列表()
        ModernListBox1.SelectedIndex = i + 1
    End Sub

    Private Sub ModernListBox1_ItemDoubleClick(sender As Object, e As LakeUI.ModernListBox.ItemEventArgs) Handles ModernListBox1.ItemDoubleClick
        If e.Index >= 0 AndAlso e.Index < 策略组数据.Count Then 读取策略到面板(策略组数据(e.Index))
    End Sub

    Private Sub ModernListBox1_ItemOrderChanged(sender As Object, e As EventArgs) Handles ModernListBox1.ItemOrderChanged
        Dim reordered As New List(Of 超分数据单片结构)
        For Each item In ModernListBox1.Items
            Dim dot = item.IndexOf("."c)
            Dim oldIndex As Integer
            If dot > 0 AndAlso Integer.TryParse(item.Substring(0, dot), oldIndex) Then
                oldIndex -= 1
                If oldIndex >= 0 AndAlso oldIndex < 策略组数据.Count Then reordered.Add(策略组数据(oldIndex))
            End If
        Next
        If reordered.Count = 策略组数据.Count Then
            策略组数据 = reordered
            刷新策略组列表()
        End If
    End Sub

    Private Sub MCB_着色器文件路径_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_着色器文件路径.SelectedIndexChanged
        If _正在选择文件 Then Exit Sub
        If MCB_着色器文件路径.SelectedIndex = 0 Then 选择着色器文件()
    End Sub

    Private Sub 选择着色器文件()
        _正在选择文件 = True
        Try
            Using d As New OpenFileDialog With {.Filter = "着色器文件|*.glsl;*.hook|所有文件|*.*", .Multiselect = False}
                Dim current = MCB_着色器文件路径.Text.Trim()
                If File.Exists(current) Then d.InitialDirectory = Path.GetDirectoryName(current)
                If d.ShowDialog(Me) = DialogResult.OK AndAlso d.FileName <> "" Then
                    MCB_着色器文件路径.Text = d.FileName
                Else
                    MCB_着色器文件路径.SelectedIndex = -1
                    MCB_着色器文件路径.Text = ""
                End If
            End Using
        Finally
            _正在选择文件 = False
        End Try
    End Sub

End Class
