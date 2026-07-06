Public Class 其他初始化

    Public Shared Sub 执行()
        LakeUI.MessageDialogOptions.BackdropEnabled = True
        LakeUI.MessageDialogOptions.BackdropTintColor = Color.FromArgb(120, 0, 0, 0)
        LakeUI.MessageDialogOptions.BackdropBlurRadius = 30
        LakeUI.MessageDialogOptions.BackdropBlurPasses = 2
        LakeUI.FloatingToolTipForm.BackdropEnabled = True
        LakeUI.FloatingToolTipForm.BackdropTintColor = Color.FromArgb(120, 0, 0, 0)
        LakeUI.FloatingToolTipForm.BackdropBlurRadius = 30
        LakeUI.FloatingToolTipForm.BackdropBlurPasses = 2
    End Sub

End Class