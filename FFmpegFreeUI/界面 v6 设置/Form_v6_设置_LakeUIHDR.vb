Public Class Form_v6_设置_LakeUIHDR

    Private Sub MCB_HDR启用_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_HDR启用.SelectedIndexChanged
        If MCB_HDR启用.SelectedIndex < 0 Then Return
        设置_v6.设置HDR选项(MCB_HDR启用.SelectedIndex, 设置_v6.实例对象.图形DX_HDR显示档位,
                         设置_v6.实例对象.图形DX_HDR矢量颜色映射, 设置_v6.实例对象.图形DX_HDR图片映射)
    End Sub

    Private Sub MCB_HDR显示档位_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_HDR显示档位.SelectedIndexChanged
        If MCB_HDR显示档位.SelectedIndex < 0 Then Return
        设置_v6.设置HDR选项(设置_v6.实例对象.图形DX_HDR启用, MCB_HDR显示档位.SelectedIndex,
                         设置_v6.实例对象.图形DX_HDR矢量颜色映射, 设置_v6.实例对象.图形DX_HDR图片映射)
    End Sub

    Private Sub MCB_HDR矢量颜色_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_HDR矢量颜色.SelectedIndexChanged
        If MCB_HDR矢量颜色.SelectedIndex < 0 Then Return
        设置_v6.设置HDR选项(设置_v6.实例对象.图形DX_HDR启用, 设置_v6.实例对象.图形DX_HDR显示档位,
                         MCB_HDR矢量颜色.SelectedIndex, 设置_v6.实例对象.图形DX_HDR图片映射)
    End Sub

    Private Sub MCB_HDR图片_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_HDR图片.SelectedIndexChanged
        If MCB_HDR图片.SelectedIndex < 0 Then Return
        设置_v6.设置HDR选项(设置_v6.实例对象.图形DX_HDR启用, 设置_v6.实例对象.图形DX_HDR显示档位,
                         设置_v6.实例对象.图形DX_HDR矢量颜色映射, MCB_HDR图片.SelectedIndex)
    End Sub

End Class
