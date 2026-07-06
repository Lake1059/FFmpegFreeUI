Public Class Form_v6_设置_LakeUIHDR

    Private Shared ReadOnly HDR档位选项 As LakeUI.GlobalOptions.HdrOutputProfile() = {
        LakeUI.GlobalOptions.HdrOutputProfile.HDR200,
        LakeUI.GlobalOptions.HdrOutputProfile.HDR300,
        LakeUI.GlobalOptions.HdrOutputProfile.HDR400,
        LakeUI.GlobalOptions.HdrOutputProfile.HDR500,
        LakeUI.GlobalOptions.HdrOutputProfile.HDR600,
        LakeUI.GlobalOptions.HdrOutputProfile.HDR700,
        LakeUI.GlobalOptions.HdrOutputProfile.HDR800,
        LakeUI.GlobalOptions.HdrOutputProfile.HDR900,
        LakeUI.GlobalOptions.HdrOutputProfile.HDR1000
    }

    Private Sub MCB_HDR启用_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_HDR启用.SelectedIndexChanged
        If MCB_HDR启用.SelectedIndex < 0 Then Return
        设置_v6.实例对象.图形DX_HDR启用 = 限制索引(MCB_HDR启用.SelectedIndex, 2)
        应用HDR设置()
    End Sub

    Private Sub MCB_HDR显示档位_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_HDR显示档位.SelectedIndexChanged
        If MCB_HDR显示档位.SelectedIndex < 0 Then Return
        设置_v6.实例对象.图形DX_HDR显示档位 = 限制索引(MCB_HDR显示档位.SelectedIndex, HDR档位选项.Length)
        应用HDR设置()
    End Sub

    Private Sub MCB_HDR矢量颜色_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_HDR矢量颜色.SelectedIndexChanged
        If MCB_HDR矢量颜色.SelectedIndex < 0 Then Return
        设置_v6.实例对象.图形DX_HDR矢量颜色映射 = 限制索引(MCB_HDR矢量颜色.SelectedIndex, 2)
        应用HDR设置()
    End Sub

    Private Sub MCB_HDR图片_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_HDR图片.SelectedIndexChanged
        If MCB_HDR图片.SelectedIndex < 0 Then Return
        设置_v6.实例对象.图形DX_HDR图片映射 = 限制索引(MCB_HDR图片.SelectedIndex, 2)
        应用HDR设置()
    End Sub

    Private Sub 应用HDR设置()
        LakeUI.GlobalOptions.HDR.Enabled = 设置_v6.实例对象.图形DX_HDR启用 = 1
        LakeUI.GlobalOptions.HDR.Profile = HDR档位选项(限制索引(设置_v6.实例对象.图形DX_HDR显示档位, HDR档位选项.Length, 2))
        LakeUI.GlobalOptions.HDR.MapVectorColors = 设置_v6.实例对象.图形DX_HDR矢量颜色映射 <> 1
        LakeUI.GlobalOptions.HDR.MapImages = 设置_v6.实例对象.图形DX_HDR图片映射 <> 1
        请求所有窗体重绘()
    End Sub

    Private Shared Function 限制索引(index As Integer, count As Integer, Optional fallback As Integer = 0) As Integer
        If count <= 0 Then Return 0
        Dim safeFallback = Math.Min(Math.Max(fallback, 0), count - 1)
        If index < 0 OrElse index >= count Then Return safeFallback
        Return index
    End Function

    Private Shared Sub 请求所有窗体重绘()
        For Each openForm As Form In Application.OpenForms
            openForm.Invalidate(True)
        Next
    End Sub
End Class
