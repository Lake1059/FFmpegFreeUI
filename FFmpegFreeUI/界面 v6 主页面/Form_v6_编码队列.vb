Public Class Form_v6_编码队列
    Dim DPI As Single = Me.DeviceDpi / 96

    Private Sub Form_v6_编码队列_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Form_v6_编码队列_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        DPI = Me.DeviceDpi / 96
    End Sub

    Private Sub UltraDetailListView1_SizeChanged(sender As Object, e As EventArgs) Handles UltraDetailListView1.SizeChanged
        Dim 有效总宽度 As Integer = UltraDetailListView1.Width - UltraDetailListView1.Padding.Left - UltraDetailListView1.Padding.Right
        Select Case 用户设置.实例对象.界面修正_编码队列的列宽调整逻辑
            Case 0
                UltraDetailListView1.Columns(1).Width = 80 * DPI
                UltraDetailListView1.Columns(2).Width = 70 * DPI
                UltraDetailListView1.Columns(3).Width = 80 * DPI
                UltraDetailListView1.Columns(4).Width = 150 * DPI
                UltraDetailListView1.Columns(5).Width = 55 * DPI
                UltraDetailListView1.Columns(6).Width = 115 * DPI
                UltraDetailListView1.Columns(7).Width = 200 * DPI
            Case 1
                UltraDetailListView1.Columns(1).Width = 有效总宽度 * 0.076
                UltraDetailListView1.Columns(2).Width = 有效总宽度 * 0.071
                UltraDetailListView1.Columns(3).Width = 有效总宽度 * 0.076
                UltraDetailListView1.Columns(4).Width = 有效总宽度 * 0.143
                UltraDetailListView1.Columns(5).Width = 有效总宽度 * 0.053
                UltraDetailListView1.Columns(6).Width = 有效总宽度 * 0.113
                UltraDetailListView1.Columns(7).Width = 有效总宽度 * 0.186
        End Select
        Dim a1 As Integer = 0
        For i = 1 To UltraDetailListView1.Columns.Count - 1
            a1 += UltraDetailListView1.Columns(i).Width
        Next
        UltraDetailListView1.Columns(0).Width = 有效总宽度 - a1 - 30 * DPI

    End Sub
End Class