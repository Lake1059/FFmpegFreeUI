Public Class 预设管理_v6

    Private Shared ReadOnly separator As String() = {","}

    Public Shared ReadOnly Property 音频编码器排序表 As List(Of String)
        Get
            Return 音频编码器数据库_v6.全部编码器.Select(Function(x) x.私有ID).ToList()
        End Get
    End Property

    Public Shared Sub 显示预设(a As 预设数据类型, ui As Form_v6_参数面板)

    End Sub

    Public Shared Sub 储存预设(ByRef a As 预设数据类型, ui As Form_v6_参数面板)

    End Sub

    Public Shared Sub 重置面板(ui As Form_v6_参数面板)

    End Sub

    Public Shared Function 将预设数据转换为命令行(a As 预设数据_v6, 输入文件 As String, 输出文件 As String) As String

    End Function

    Public Shared Sub 显示参数总览(MTB As LakeUI.ModernTextBox, a As 预设数据_v6)

    End Sub



End Class
