Imports System.ComponentModel
Public Class Form1
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Shared Property DPI As Single = Form1.CreateGraphics.DpiX / 96

    Public 是否初始化 As Boolean = False
    Private 上一次窗口状态 As FormWindowState


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        界面控制.初始化()
        上一次窗口状态 = Me.WindowState
    End Sub

    Private Sub Form1_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        界面控制.界面校准()
        重新创建句柄()
        If DPI <> 1 Then DPI变动时校准界面()
    End Sub

    Private Sub Form1_DpiChanged(sender As Object, e As DpiChangedEventArgs) Handles Me.DpiChanged
        DPI = e.DeviceDpiNew / 96
        DPI变动时校准界面()
    End Sub

    Public Sub 重新创建句柄()
        If Not Me.IsHandleCreated Then Me.CreateHandle()
    End Sub

    Private Sub Form1_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        If 是否初始化 = False Then Exit Sub
        If Me.WindowState = FormWindowState.Minimized Then Exit Sub
        If Me.WindowState <> 上一次窗口状态 Then
            界面控制.界面校准()
            上一次窗口状态 = Me.WindowState
        Else
            界面控制.界面校准()
        End If
    End Sub

    Private Sub Form1_Move(sender As Object, e As EventArgs) Handles Me.Move
        If 是否初始化 = False Then Exit Sub
        界面控制.界面校准()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If Me.ListView1.SelectedItems.Count = 1 Then
            Label74.Text = 编码任务.队列(Me.ListView1.SelectedItems(0).Index).实时输出
            Label76.Text = String.Join(vbCrLf, 编码任务.队列(Me.ListView1.SelectedItems(0).Index).错误列表)
        Else
            Label74.Text = ""
            Label76.Text = ""
        End If
    End Sub

    Sub DPI变动时校准界面()
        Me.MinimumSize = New Size(0, 0)
        Me.ClientSize = New Size(1300 * DPI, 800 * DPI)
        Me.MinimumSize = Me.Size
        Me.UiTabControlMenu1.ItemSize = New Size(150 * DPI, 40 * DPI)
        UiPanel1.Height = 50 * DPI
        UiComboBox1.ItemHeight = 30 * DPI
        UiComboBox21.ItemHeight = 30 * DPI
        UiCheckBox25.CheckBoxSize = 20 * DPI
        UiRadioButton1.RadioButtonSize = 20 * DPI
        UiRadioButton2.RadioButtonSize = 20 * DPI
        UiRadioButton3.RadioButtonSize = 20 * DPI
        UiRadioButton4.RadioButtonSize = 20 * DPI
        UiRadioButton5.RadioButtonSize = 20 * DPI
        UiRadioButton6.RadioButtonSize = 20 * DPI
        UiRadioButton7.RadioButtonSize = 20 * DPI
        UiCheckBox6.CheckBoxSize = 20 * DPI
        UiComboBox2.ItemHeight = 30 * DPI
        UiComboBox3.ItemHeight = 30 * DPI
        UiComboBox7.ItemHeight = 30 * DPI
        UiCheckBox3.CheckBoxSize = 20 * DPI
        UiComboBox5.ItemHeight = 30 * DPI
        UiCheckBox2.CheckBoxSize = 20 * DPI
        UiCheckBox5.CheckBoxSize = 20 * DPI
        UiComboBox4.ItemHeight = 30 * DPI
        UiCheckBox4.CheckBoxSize = 20 * DPI
        UiCheckBox8.CheckBoxSize = 20 * DPI
        UiComboBox6.ItemHeight = 30 * DPI
        UiCheckBox17.CheckBoxSize = 20 * DPI
        UiCheckBox9.CheckBoxSize = 20 * DPI
        UiComboBox8.ItemHeight = 30 * DPI
        UiComboBox9.ItemHeight = 30 * DPI
        UiCheckBox7.CheckBoxSize = 20 * DPI
        UiComboBox11.ItemHeight = 30 * DPI
        UiCheckBox10.CheckBoxSize = 20 * DPI
        UiCheckBox12.CheckBoxSize = 20 * DPI
        UiComboBox10.ItemHeight = 30 * DPI
        UiCheckBox13.CheckBoxSize = 20 * DPI
        UiComboBox13.ItemHeight = 30 * DPI
        UiCheckBox15.CheckBoxSize = 20 * DPI
        UiComboBox14.ItemHeight = 30 * DPI
        UiCheckBox14.CheckBoxSize = 20 * DPI
        UiComboBox15.ItemHeight = 30 * DPI
        UiCheckBox16.CheckBoxSize = 20 * DPI
        UiCheckBox20.CheckBoxSize = 20 * DPI
        UiCheckBox29.CheckBoxSize = 20 * DPI
        UiComboBox20.ItemHeight = 30 * DPI
        UiCheckBox27.CheckBoxSize = 20 * DPI
        UiCheckBox26.CheckBoxSize = 20 * DPI

    End Sub

    Private Sub UiButton前往爱发电_Click(sender As Object, e As EventArgs) Handles UiButton前往爱发电.Click
        Dim psi As New ProcessStartInfo With {.FileName = "https://afdian.com/a/1059Studio", .UseShellExecute = True}
        Process.Start(psi)
    End Sub

    Private Sub UiButton哔哩哔哩_Click(sender As Object, e As EventArgs) Handles UiButton哔哩哔哩.Click
        Dim psi As New ProcessStartInfo With {.FileName = "https://space.bilibili.com/319785096", .UseShellExecute = True}
        Process.Start(psi)
    End Sub

    Private Sub UiCheckBoxKeepTimestamp_CheckedChanged(sender As Object, e As EventArgs) Handles UiCheckBoxKeepTimestamp.CheckedChanged
        编码任务.保留时间戳 = UiCheckBoxKeepTimestamp.Checked
    End Sub
End Class
