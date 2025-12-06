Imports System.IO
Imports System.Text.Json
Imports System.Windows.Forms

Public Module LanguageManager
    Private LanguageData As New Dictionary(Of String, String)
    Public CurrentLanguageCode As String = "zh-CN"

    ' 动态获取可用语言列表：显示名称 -> 语言代码 (文件名)
    Public Function GetAvailableLanguages() As Dictionary(Of String, String)
        Dim languages As New Dictionary(Of String, String)
        Dim langDir = Path.Combine(Application.StartupPath, "Lang")

        If Not Directory.Exists(langDir) Then
            Return languages
        End If

        Try
            Dim files = Directory.GetFiles(langDir, "*.json")
            For Each file_path In files
                Try
                    Dim fileName = Path.GetFileNameWithoutExtension(file_path)
                    Dim jsonString = File.ReadAllText(file_path)
                    
                    ' 尝试解析 JSON 以获取语言名称
                    ' 我们只需要解析一部分，或者全部解析然后查找 Meta Key
                    Dim tempDict = JsonSerializer.Deserialize(Of Dictionary(Of String, String))(jsonString)
                    
                    Dim langName As String = fileName ' 默认使用文件名
                    If tempDict IsNot Nothing AndAlso tempDict.ContainsKey("_Meta.LanguageName") Then
                        langName = tempDict("_Meta.LanguageName")
                    End If
                    
                    ' 如果名称重复，附加文件名以区分
                    If languages.ContainsKey(langName) Then
                        langName = $"{langName} ({fileName})"
                    End If
                    
                    languages.Add(langName, fileName)

                Catch ex As Exception
                    ' 单个文件解析失败，忽略并继续
                    Debug.WriteLine($"Failed to parse language file {file_path}: {ex.Message}")
                End Try
            Next
        Catch ex As Exception
            Debug.WriteLine($"Error accessing Lang directory: {ex.Message}")
        End Try

        Return languages
    End Function

    Public Sub LoadLanguage(langCode As String)
        CurrentLanguageCode = langCode
        Dim langDir = Path.Combine(Application.StartupPath, "Lang")
        If Not Directory.Exists(langDir) Then
            Directory.CreateDirectory(langDir)
        End If

        Dim filePath = Path.Combine(langDir, langCode & ".json")
        If File.Exists(filePath) Then
            Try
                Dim jsonString = File.ReadAllText(filePath)
                ' 使用 System.Text.Json 反序列化
                LanguageData = JsonSerializer.Deserialize(Of Dictionary(Of String, String))(jsonString)
            Catch ex As Exception
                ' 如果加载失败，保持空字典或只记录错误
                Debug.WriteLine($"Error loading language file: {ex.Message}")
                LanguageData = New Dictionary(Of String, String)
            End Try
        Else
            ' 如果文件不存在，清空字典（使用默认硬编码文本）
            LanguageData = New Dictionary(Of String, String)
        End If
    End Sub

    ''' <summary>
    ''' 获取翻译文本
    ''' </summary>
    ''' <param name="key">字典中的键</param>
    ''' <param name="defaultText">默认文本（如果找不到键时返回）</param>
    ''' <returns></returns>
    Public Function GetText(key As String, Optional defaultText As String = "") As String
        If LanguageData IsNot Nothing AndAlso LanguageData.ContainsKey(key) Then
            Return LanguageData(key)
        End If
        Return If(String.IsNullOrEmpty(defaultText), key, defaultText)
    End Function

    ''' <summary>
    ''' 自动翻译窗体及其控件
    ''' </summary>
    ''' <param name="root">根控件或窗体</param>
    Public Sub ApplyLanguage(root As Control)
        If root Is Nothing Then Exit Sub

        ' 1. 翻译自身
        TranslateControl(root)

        ' 2. 递归翻译子控件
        If root.HasChildren Then
            For Each ctrl As Control In root.Controls
                ApplyLanguage(ctrl)
            Next
        End If

        ' 3. 处理特殊容器控件 (MenuStrip, ContextMenuStrip, ToolStrip)
        If TypeOf root Is Form Then
            Dim frm = DirectCast(root, Form)
            If frm.MainMenuStrip IsNot Nothing Then ApplyLanguageToToolStrip(frm.MainMenuStrip)
        End If

        If TypeOf root Is ToolStrip Then
            ApplyLanguageToToolStrip(DirectCast(root, ToolStrip))
        End If
    End Sub

    Private Sub ApplyLanguageToToolStrip(strip As ToolStrip)
        TranslateControl(strip) ' 翻译 ToolStrip 自己的名字
        For Each item As ToolStripItem In strip.Items
            TranslateToolStripItem(item)
        Next
    End Sub

    Private Sub TranslateToolStripItem(item As ToolStripItem)
        Dim key = GenerateKey(item)
        ' 只有当字典中存在该 key 时才进行替换，避免将文本替换为 Key
        If Not String.IsNullOrEmpty(item.Text) AndAlso LanguageData.ContainsKey(key) Then
             item.Text = LanguageData(key)
        End If
#If DEBUG Then
        If Not String.IsNullOrEmpty(item.Text) AndAlso Not LanguageData.ContainsKey(key) Then
            Dim logMsg = $"[i18n Missing] Key: {key}, Text: {item.Text}"
            Debug.WriteLine(logMsg)
            File.AppendAllText(Path.Combine(Application.StartupPath, "i18n_missing.log"), logMsg & Environment.NewLine)
        End If
#End If

        ' 递归处理下拉菜单
        If TypeOf item Is ToolStripDropDownItem Then
            For Each subItem As ToolStripItem In DirectCast(item, ToolStripDropDownItem).DropDownItems
                TranslateToolStripItem(subItem)
            Next
        End If
    End Sub

    Private Sub TranslateControl(ctrl As Control)
        Dim key = GenerateKey(ctrl)
        
        ' 翻译 Text 属性
        ' 只有当字典中存在该 key 时才进行替换，避免将文本替换为 Key
        If Not String.IsNullOrEmpty(ctrl.Text) AndAlso Not String.IsNullOrEmpty(ctrl.Name) AndAlso LanguageData.ContainsKey(key) Then
            ctrl.Text = LanguageData(key)
        End If
#If DEBUG Then
        If Not String.IsNullOrEmpty(ctrl.Text) AndAlso Not String.IsNullOrEmpty(ctrl.Name) AndAlso Not LanguageData.ContainsKey(key) Then
            Dim logMsg = $"[i18n Missing] Key: {key}, Text: {ctrl.Text}"
            Debug.WriteLine(logMsg)
            File.AppendAllText(Path.Combine(Application.StartupPath, "i18n_missing.log"), logMsg & Environment.NewLine)
        End If
#End If

        ' 特殊处理 ListView Columns
        If TypeOf ctrl Is ListView Then
            Dim lv = DirectCast(ctrl, ListView)
            For Each col As ColumnHeader In lv.Columns
                Dim colKey = $"{key}.Column_{col.Index}"
                If LanguageData.ContainsKey(colKey) Then
                     col.Text = LanguageData(colKey)
                End If
#If DEBUG Then
                If Not LanguageData.ContainsKey(colKey) Then
                    Dim logMsg = $"[i18n Missing] Key: {colKey}, Text: {col.Text}"
                    Debug.WriteLine(logMsg)
                    File.AppendAllText(Path.Combine(Application.StartupPath, "i18n_missing.log"), logMsg & Environment.NewLine)
                End If
#End If
            Next
        End If
    End Sub

    Private Function GenerateKey(component As Object) As String
        Dim name As String = ""
        Dim formName As String = ""

        If TypeOf component Is Control Then
            Dim ctrl = DirectCast(component, Control)
            name = ctrl.Name
            ' 改进：如果是 UserControl 里的控件，FindForm 可能会找到主窗体 Form1，
            ' 但我们希望 Key 是 "界面_设置.Button1" 而不是 "Form1.Button1"
            ' 所以我们优先查找 Parent 是否是 UserControl
            Dim parent = ctrl.Parent
            While parent IsNot Nothing
                If TypeOf parent Is UserControl Then
                    formName = parent.Name ' 使用 UserControl 的名字作为前缀
                    Exit While
                ElseIf TypeOf parent Is Form Then
                    formName = parent.Name
                    Exit While
                End If
                parent = parent.Parent
            End While
            
            ' 如果上面没找到（比如顶级 Form），尝试 FindForm
            If String.IsNullOrEmpty(formName) Then
                Dim form = ctrl.FindForm()
                If form IsNot Nothing Then formName = form.Name
            End If

        ElseIf TypeOf component Is ToolStripItem Then
            Dim item = DirectCast(component, ToolStripItem)
            name = item.Name
            ' ToolStripItem 比较麻烦，因为它不是 Control。
            ' 尝试找 Owner
            Dim owner = item.Owner
            If owner IsNot Nothing Then
                ' Owner 是 ToolStrip，再找 ToolStrip 的 Parent
                Dim parent = owner.Parent
                 While parent IsNot Nothing
                    If TypeOf parent Is UserControl Then
                        formName = parent.Name
                        Exit While
                    ElseIf TypeOf parent Is Form Then
                        formName = parent.Name
                        Exit While
                    End If
                    parent = parent.Parent
                End While
                 If String.IsNullOrEmpty(formName) Then
                    Dim form = owner.FindForm()
                    If form IsNot Nothing Then formName = form.Name
                End If
            End If
        End If

        If String.IsNullOrEmpty(formName) Then
            formName = "Global"
        End If

        Return $"{formName}.{name}"
    End Function

End Module
