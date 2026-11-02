Imports System.IO
Imports System.Text

' 用户内容保存在程序目录，避免随会话清理或升级覆盖。
Public NotInheritable Class Agent自定义内容_v6
    Private Sub New()
    End Sub

    Public Shared ReadOnly Property Agent目录 As String
        Get
            Return Path.Combine(Application.StartupPath, "Agent")
        End Get
    End Property

    Public Shared ReadOnly Property 系统提示词路径 As String
        Get
            Return Path.Combine(Agent目录, "SystemPrompt.md")
        End Get
    End Property

    Public Shared ReadOnly Property 技能目录 As String
        Get
            Return Path.Combine(Agent目录, "Skills")
        End Get
    End Property

    Public Shared Sub 初始化()
        Directory.CreateDirectory(技能目录)
        If File.Exists(系统提示词路径) Then Return
        ' CreateNew 保证即使多个入口同时初始化，也不会清空用户内容。
        Try
            Using stream As New FileStream(系统提示词路径, FileMode.CreateNew, FileAccess.Write, FileShare.Read)
            End Using
        Catch ex As IOException When File.Exists(系统提示词路径)
        End Try
    End Sub

    Public Shared Function 读取系统提示词() As String
        If Not File.Exists(系统提示词路径) Then 初始化()
        ' 读取失败应明确报错，不能静默发送与用户预期不同的系统提示词。
        Dim content = File.ReadAllText(系统提示词路径, Encoding.UTF8)
        If Not 有有效字符(content) Then Return ""
        Return content
    End Function

    Private Shared Function 有有效字符(content As String) As Boolean
        Return content IsNot Nothing AndAlso content.Any(
            Function(c) Not Char.IsWhiteSpace(c) AndAlso Not Char.IsControl(c) AndAlso
                Char.GetUnicodeCategory(c) <> Globalization.UnicodeCategory.Format)
    End Function

End Class
