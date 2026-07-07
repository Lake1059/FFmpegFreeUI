Partial Public Class 网络功能

    Public Shared ReadOnly Property SPAgent端点最后错误 As String
        Get
            Return 网络功能_v6_SPAgent端点.SPAgent端点最后错误
        End Get
    End Property

    Public Shared ReadOnly Property 是否正在获取SPAgent端点 As Boolean
        Get
            Return 网络功能_v6_SPAgent端点.是否正在获取SPAgent端点
        End Get
    End Property

    Public Shared Sub 启动时后台获取SPAgent端点()
        网络功能_v6_SPAgent端点.启动时后台获取SPAgent端点()
    End Sub

    Public Shared Function 获取SPAgent端点列表() As List(Of AgentSpEndpointInfo)
        Return 网络功能_v6_SPAgent端点.获取SPAgent端点列表()
    End Function

    Public Shared Sub 选择SPAgent端点(端点列表索引 As Integer)
        网络功能_v6_SPAgent端点.选择SPAgent端点(端点列表索引)
    End Sub

    Public Shared Function 获取当前SPAgent端点() As AgentSpEndpointInfo
        Return 网络功能_v6_SPAgent端点.获取当前SPAgent端点()
    End Function

    Public Shared Function 创建Agent端点客户端() As AgentEndpointClient
        Return 网络功能_v6_SPAgent端点.创建Agent端点客户端()
    End Function

    Public Shared Property 当前是否正在进行获取新闻列表 As Boolean
        Get
            Return 网络功能_v6_新闻列表.当前是否正在进行获取新闻列表
        End Get
        Set(value As Boolean)
            网络功能_v6_新闻列表.当前是否正在进行获取新闻列表 = value
        End Set
    End Property

    Public Shared Sub 获取新闻列表()
        网络功能_v6_新闻列表.获取新闻列表()
    End Sub

    Public Shared Sub 创建一个新闻内容(标题 As String, 标题颜色 As String, 副标题 As String, 行为 As String, 内容 As String)
        网络功能_v6_新闻列表.创建一个新闻内容(标题, 标题颜色, 副标题, 行为, 内容)
    End Sub

    Public Shared Sub 设置新闻列表获取失败(原因 As String)
        网络功能_v6_新闻列表.设置新闻列表获取失败(原因)
    End Sub

    Public Shared Property 当前是否正在进行本体更新 As Boolean
        Get
            Return 网络功能_v6_软件本体更新.当前是否正在进行本体更新
        End Get
        Set(value As Boolean)
            网络功能_v6_软件本体更新.当前是否正在进行本体更新 = value
        End Set
    End Property

    Public Shared Property 检查软件本体更新最后一次错误 As String
        Get
            Return 网络功能_v6_软件本体更新.检查软件本体更新最后一次错误
        End Get
        Set(value As String)
            网络功能_v6_软件本体更新.检查软件本体更新最后一次错误 = value
        End Set
    End Property

    Public Shared ReadOnly Property 检查软件本体更新下载位置 As String
        Get
            Return 网络功能_v6_软件本体更新.检查软件本体更新下载位置
        End Get
    End Property

    Public Shared Sub 检查软件本体更新()
        网络功能_v6_软件本体更新.检查软件本体更新()
    End Sub

    Public Shared Property 当前是否正在进行更新器更新 As Boolean
        Get
            Return 网络功能_v6_更新器更新.当前是否正在进行更新器更新
        End Get
        Set(value As Boolean)
            网络功能_v6_更新器更新.当前是否正在进行更新器更新 = value
        End Set
    End Property

    Public Shared Property 检查更新器更新最后一次错误 As String
        Get
            Return 网络功能_v6_更新器更新.检查更新器更新最后一次错误
        End Get
        Set(value As String)
            网络功能_v6_更新器更新.检查更新器更新最后一次错误 = value
        End Set
    End Property

    Public Shared ReadOnly Property 检查更新器更新下载位置 As String
        Get
            Return 网络功能_v6_更新器更新.检查更新器更新下载位置
        End Get
    End Property

    Public Shared Sub 检查更新器更新(Optional 强制更新 As Boolean = False)
        网络功能_v6_更新器更新.检查更新器更新(强制更新)
    End Sub

End Class
