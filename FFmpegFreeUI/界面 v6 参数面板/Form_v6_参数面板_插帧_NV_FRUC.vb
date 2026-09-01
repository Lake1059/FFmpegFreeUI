Imports System.Text

Public Class Form_v6_参数面板_插帧_NV_FRUC
    Private Sub Form_v6_参数面板_插帧_NV_FRUC_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = FormMain_v6.Icon
        SetControlFont(设置_v6.实例对象.字体, Me, , True)
        If FormMain_v6.ThisIsYourWindow1.AttachedForms.Count > 0 Then
            FormMain_v6.ThisIsYourWindow1.Attach(Me)
            ModernPanel1.BackColor = Color.Transparent
            ModernPanel1.BackColor1 = Color.Transparent
        End If
    End Sub

    Private Sub Form_v6_参数面板_插帧_NV_FRUC_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Dim s As New StringBuilder
        s.AppendLine("1. 页面【解码参数】设定 -init_hw_device vulkan")
        s.AppendLine("2. 页面【滤镜排序】添加视频滤镜 hwupload")
        s.AppendLine("3. 页面【滤镜排序】添加视频滤镜 hwdownload")
        s.AppendLine("4. 页面【滤镜排序】添加视频滤镜 format=像素格式？")
        s.AppendLine("5. 页面【滤镜排序】调整滤镜顺序，从上到下依次为：hwupload、fruc_vulkan、hwdownload、format")
        s.AppendLine("6. 像素格式不能省略，ffmpeg 自动协商基本上都会报错，必须设置为原片上传给滤镜时的对应格式，例如 yuv420、yuv420p10le，不能一步到位从 yuv420p10le 到 p010le，位深也要对应，不能上传8bit而下载10bit，要让编码器编码对应像素格式直接在色彩管理页面设置即可")
        HCL_CK流程提示.ToolTipText = s.ToString
    End Sub

    Private Sub Form_v6_参数面板_插帧_NV_FRUC_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = True
        Me.Hide()
    End Sub
End Class