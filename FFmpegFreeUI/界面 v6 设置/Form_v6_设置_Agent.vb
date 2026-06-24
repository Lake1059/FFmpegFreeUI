Imports LakeUI

Public Class Form_v6_设置_Agent
    Private _正在加载 As Boolean = False
    Private _spEndpointItems As New List(Of 网络功能.AgentSpEndpointInfo)

    Private Sub Form_v6_设置_Agent_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _正在加载 = True
        Try
            MTB_自定义地址.Text = 设置_v6.实例对象.AgentEndPoint
            MTB_APIKEY.Text = 设置_v6.实例对象.AgentApiKey
            MTB_APIKEY.PasswordChar = "●"c
            MTB_附加请求头.Text = 设置_v6.实例对象.Agent附加请求头
            MTB_附加请求Body.Text = 设置_v6.实例对象.Agent附加请求Body
            刷新SPAgent端点列表()
        Finally
            _正在加载 = False
        End Try
    End Sub

    Public Sub 刷新SPAgent端点列表()
        Dim oldLoading = _正在加载
        _正在加载 = True
        Try
            _spEndpointItems = 网络功能.获取SPAgent端点列表()

            ModernComboBox1.Items.Clear()
            ModernComboBox1.Items.Add("使用上面的自定义端点")
            For Each endpoint In _spEndpointItems
                ModernComboBox1.Items.Add(endpoint.DisplayName)
            Next

            ModernComboBox1.SelectedIndex = 获取当前下拉框索引()
            更新SP端点状态文字()
        Finally
            _正在加载 = oldLoading
        End Try
    End Sub

    Private Sub MTB_自定义地址_TextChanged(sender As Object, e As EventArgs) Handles MTB_自定义地址.TextChanged
        If _正在加载 Then Exit Sub
        设置_v6.实例对象.AgentEndPoint = MTB_自定义地址.Text.Trim()
    End Sub

    Private Sub MTB_APIKEY_TextChanged(sender As Object, e As EventArgs) Handles MTB_APIKEY.TextChanged
        If _正在加载 Then Exit Sub
        设置_v6.实例对象.AgentApiKey = MTB_APIKEY.Text.Trim()
    End Sub

    Private Sub MTB_附加请求头_TextChanged(sender As Object, e As EventArgs) Handles MTB_附加请求头.TextChanged
        If _正在加载 Then Exit Sub
        设置_v6.实例对象.Agent附加请求头 = MTB_附加请求头.Text
    End Sub

    Private Sub MTB_附加请求Body_TextChanged(sender As Object, e As EventArgs) Handles MTB_附加请求Body.TextChanged
        If _正在加载 Then Exit Sub
        设置_v6.实例对象.Agent附加请求Body = MTB_附加请求Body.Text
    End Sub

    Private Sub MTB_附加请求头_LostFocus(sender As Object, e As EventArgs) Handles MTB_附加请求头.LostFocus
        Try
            Dim unused = AgentEndpointClient.ParseAdditionalHeaders(MTB_附加请求头.Text)
        Catch ex As Exception
            ExFloatingTip(MTB_附加请求头, ex.Message, 2600)
        End Try
    End Sub

    Private Sub MTB_附加请求Body_LostFocus(sender As Object, e As EventArgs) Handles MTB_附加请求Body.LostFocus
        Try
            Dim unused = AgentEndpointClient.ParseExtraBody(MTB_附加请求Body.Text)
        Catch ex As Exception
            ExFloatingTip(MTB_附加请求Body, ex.Message, 2600)
        End Try
    End Sub

    Private Sub ModernComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ModernComboBox1.SelectedIndexChanged
        If _正在加载 Then Exit Sub
        网络功能.选择SPAgent端点(ModernComboBox1.SelectedIndex - 1)
        Form_v6_Agent.请求刷新模型列表()
    End Sub

    Private Function 获取当前下拉框索引() As Integer
        Dim currentEndpoint = 网络功能.获取当前SPAgent端点()
        If currentEndpoint Is Nothing Then Return 0

        For index As Integer = 0 To _spEndpointItems.Count - 1
            If String.Equals(_spEndpointItems(index).DisplayName, currentEndpoint.DisplayName, StringComparison.Ordinal) AndAlso
               String.Equals(_spEndpointItems(index).Address, currentEndpoint.Address, StringComparison.OrdinalIgnoreCase) Then
                Return index + 1
            End If
        Next

        网络功能.选择SPAgent端点(-1)
        Return 0
    End Function

    Private Sub 更新SP端点状态文字()
        If 网络功能.是否正在获取SPAgent端点 Then
            HtmlColorLabel2.Text = "<span style=""font-size:13; color:Silver"">SP 用户专属端点</span>   正在后台获取可用端点"
        ElseIf _spEndpointItems.Count > 0 Then
            HtmlColorLabel2.Text = $"<span style=""font-size:13; color:Silver"">SP 用户专属端点</span>   已获取 {_spEndpointItems.Count} 个可用端点"
        Else
            HtmlColorLabel2.Text = "<span style=""font-size:13; color:Silver"">SP 用户专属端点</span>   暂无可用端点"
        End If
    End Sub
End Class
