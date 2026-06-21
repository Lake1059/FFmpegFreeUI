Public Class Form_v6_编码队列_任务日志
    Private Shared 当前实例 As Form_v6_编码队列_任务日志

    Private 当前任务ID As String = ""
    Private 当前显示模式 As 编码任务日志显示模式_v6 = 编码任务日志显示模式_v6.最新输出不含进度
    Private 已显示最小序号 As Long = 0
    Private 已显示最大序号 As Long = 0
    Private 已显示日志结构版本号 As Long = 0
    Private 当前阶段名 As String = ""
    Private 正在重载 As Boolean = False
    Private 待刷新当前任务 As Boolean = False
    Private 待强制重载 As Boolean = False
    Private ReadOnly 刷新计时器 As New Timer With {.Interval = 500}
    Private ReadOnly 性能计数器刷新计时器 As New Timer With {.Interval = 1000}
    Private 当前性能计数器 As LakeUI.MainAppUsageCounter
    Private 当前性能计数器进程ID As Integer = 0

    Public Shared Sub 打开或激活(owner As IWin32Window, selectedIds As IEnumerable(Of String))
        If 当前实例 Is Nothing OrElse 当前实例.IsDisposed Then
            当前实例 = New Form_v6_编码队列_任务日志()
            AddHandler 当前实例.FormClosed, Sub() 当前实例 = Nothing
        End If
        当前实例.同步选择(selectedIds)
        If 当前实例.Visible Then
            当前实例.Activate()
        Else
            当前实例.居中到v6主窗口()
            当前实例.Show(owner)
        End If
    End Sub

    Public Shared Sub 同步队列选择(selectedIds As IEnumerable(Of String))
        If 当前实例 Is Nothing OrElse 当前实例.IsDisposed Then Exit Sub
        当前实例.同步选择(selectedIds)
    End Sub

    Public Shared Sub 刷新任务性能计数器设置()
        If 当前实例 Is Nothing OrElse 当前实例.IsDisposed Then Exit Sub
        当前实例.UI执行(Sub()
                      Dim task = If(当前实例.当前任务ID = "", Nothing, 编码队列_v6.根据ID获取任务(当前实例.当前任务ID))
                      当前实例.更新任务性能计数器(task)
                  End Sub)
    End Sub

    Private Sub Form_v6_编码队列_任务日志_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = FormMain_v6.Icon
        SetControlFont(设置_v6.实例对象.字体, Me, , True)
        If FormMain_v6.ThisIsYourWindow1.AttachedForms.Count > 0 Then
            FormMain_v6.ThisIsYourWindow1.Attach(Me)
            ModernPanel1.BackColor = Color.Transparent
            ModernPanel1.BackColor1 = Color.Transparent
        End If
        BeginInvoke(Sub() 居中到v6主窗口())
        ModernTextBox1.SyntaxHighlighter = FFmpeg输出语法高亮器_v6.默认实例
        ModernTextBox1.EnableSyntaxHighlight = True
        ModernTextBox1.MaxUndoCount = 0
        ModernTextBox1.PreserveScrollPosition = Not ModernCheckBox1.Checked
        If MCB_显示模式.SelectedIndex < 0 Then MCB_显示模式.SelectedIndex = 1
        应用任务性能计数器可见性()
        AddHandler 刷新计时器.Tick, AddressOf 刷新计时器_Tick
        AddHandler 性能计数器刷新计时器.Tick, AddressOf 性能计数器刷新计时器_Tick
        AddHandler 编码队列_v6.任务已更新, AddressOf 任务已更新
        AddHandler 编码队列_v6.队列已变化, AddressOf 队列已变化
        If 当前任务ID = "" Then
            刷新空状态("未选择任务")
        Else
            刷新当前任务(True)
        End If
    End Sub

    Private Sub Form_v6_编码队列_任务日志_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        RemoveHandler 编码队列_v6.任务已更新, AddressOf 任务已更新
        RemoveHandler 编码队列_v6.队列已变化, AddressOf 队列已变化
        RemoveHandler 刷新计时器.Tick, AddressOf 刷新计时器_Tick
        RemoveHandler 性能计数器刷新计时器.Tick, AddressOf 性能计数器刷新计时器_Tick
        刷新计时器.Stop()
        刷新计时器.Dispose()
        性能计数器刷新计时器.Stop()
        性能计数器刷新计时器.Dispose()
        释放当前性能计数器()
    End Sub

    Private Sub 任务已更新(任务 As 编码任务_v6)
        If 任务 Is Nothing OrElse 任务.ID <> 当前任务ID Then Exit Sub
        UI执行(Sub() 请求刷新当前任务(False))
    End Sub

    Private Sub 队列已变化()
        UI执行(Sub()
                 If 当前任务ID <> "" AndAlso 编码队列_v6.根据ID获取任务(当前任务ID) Is Nothing Then
                     刷新空状态("任务已移除")
                 Else
                     刷新当前任务(False)
                 End If
             End Sub)
    End Sub

    Private Sub UI执行(action As Action)
        If action Is Nothing OrElse IsDisposed Then Exit Sub
        If InvokeRequired Then
            BeginInvoke(action)
        Else
            action()
        End If
    End Sub

    Private Sub 请求刷新当前任务(forceReload As Boolean)
        If forceReload Then 待强制重载 = True
        待刷新当前任务 = True
        If Not 刷新计时器.Enabled Then 刷新计时器.Start()
    End Sub

    Private Sub 刷新计时器_Tick(sender As Object, e As EventArgs)
        刷新计时器.Stop()
        If Not 待刷新当前任务 Then Exit Sub

        Dim forceReload = 待强制重载
        待刷新当前任务 = False
        待强制重载 = False
        刷新当前任务(forceReload)
    End Sub

    Public Sub 同步选择(selectedIds As IEnumerable(Of String))
        UI执行(Sub()
                 Dim ids = If(selectedIds, Array.Empty(Of String)()).Where(Function(x) Not String.IsNullOrWhiteSpace(x)).Distinct().ToList()
                 If ids.Count <> 1 Then
                     刷新空状态(If(ids.Count = 0, "未选择任务", "多选时不显示任务日志"))
                     Exit Sub
                 End If

                 Dim task = 编码队列_v6.根据ID获取任务(ids(0))
                 If task Is Nothing Then
                     刷新空状态("任务已移除")
                     Exit Sub
                 End If

                 If 当前任务ID <> task.ID Then
                     当前任务ID = task.ID
                     刷新当前任务(True)
                 Else
                     刷新当前任务(False)
                 End If
             End Sub)
    End Sub

    Private Sub 刷新空状态(message As String)
        当前任务ID = ""
        已显示最小序号 = 0
        已显示最大序号 = 0
        已显示日志结构版本号 = 0
        当前阶段名 = ""
        待刷新当前任务 = False
        待强制重载 = False
        刷新计时器.Stop()
        ModernTextBox1.Clear()
        Text = "编码队列任务日志"
        更新任务性能计数器(Nothing)
    End Sub

    Private Sub 刷新当前任务(forceReload As Boolean)
        If 正在重载 Then Exit Sub
        If 当前任务ID = "" Then Exit Sub

        Dim task = 编码队列_v6.根据ID获取任务(当前任务ID)
        If task Is Nothing Then
            刷新空状态("任务已移除")
            Exit Sub
        End If
        更新任务性能计数器(task)

        Dim mode = 获取当前显示模式()
        Dim stageName = If(task.当前步骤?.显示名称, "")
        If mode = 编码任务日志显示模式_v6.当前阶段输出 AndAlso stageName <> 当前阶段名 Then forceReload = True
        当前显示模式 = mode
        当前阶段名 = stageName

        Dim entries = task.获取日志快照(mode)
        If forceReload OrElse task.日志结构版本号 <> 已显示日志结构版本号 OrElse (entries.Count = 0 AndAlso 已显示最大序号 > 0) OrElse (entries.Count > 0 AndAlso 已显示最小序号 > 0 AndAlso entries(0).序号 > 已显示最小序号) Then
            重载日志(task, entries, Not forceReload AndAlso Not ModernCheckBox1.Checked)
            Exit Sub
        End If

        Dim appended As Boolean = False
        For Each entry In entries.Where(Function(x) x.序号 > 已显示最大序号)
            ModernTextBox1.AppendLine(entry.文本)
            appended = True
            If 已显示最小序号 = 0 Then 已显示最小序号 = entry.序号
            已显示最大序号 = Math.Max(已显示最大序号, entry.序号)
        Next
        已显示日志结构版本号 = task.日志结构版本号
        更新标题与状态(task, entries.Count)
        If appended AndAlso ModernCheckBox1.Checked Then ModernTextBox1.ScrollToBottom()
    End Sub

    Private Sub 重载日志(task As 编码任务_v6, entries As List(Of 编码任务日志条目_v6), preserveScrollPosition As Boolean)
        正在重载 = True
        Dim originalPreserveScrollPosition = ModernTextBox1.PreserveScrollPosition
        Try
            ModernTextBox1.PreserveScrollPosition = preserveScrollPosition
            ModernTextBox1.Text = String.Join(vbCrLf, entries.Select(Function(entry) 获取日志条目显示文本(entry)))
            已显示最小序号 = 0
            已显示最大序号 = 0
            For Each entry In entries
                If 已显示最小序号 = 0 Then 已显示最小序号 = entry.序号
                已显示最大序号 = Math.Max(已显示最大序号, entry.序号)
            Next
            已显示日志结构版本号 = task.日志结构版本号
            更新标题与状态(task, entries.Count)
            If ModernCheckBox1.Checked Then ModernTextBox1.ScrollToBottom()
        Finally
            ModernTextBox1.PreserveScrollPosition = originalPreserveScrollPosition
            正在重载 = False
        End Try
    End Sub

    Private Shared Function 获取日志条目显示文本(entry As 编码任务日志条目_v6) As String
        Return If(entry?.文本, "").Replace(vbCr, "").Replace(vbLf, "")
    End Function

    Private Sub 更新标题与状态(task As 编码任务_v6, lineCount As Integer)
        Text = "任务日志 - " & 获取任务显示名称(task)
    End Sub

    Private Sub 应用任务性能计数器可见性()
        Dim visible = 设置_v6.实例对象.任务日志性能计数器 = 0
        Panel2.Visible = visible
        If visible Then
            If Not 性能计数器刷新计时器.Enabled Then 性能计数器刷新计时器.Start()
        Else
            性能计数器刷新计时器.Stop()
            释放当前性能计数器()
            MB_任务性能计数器.Text = ""
        End If
    End Sub

    Private Sub 性能计数器刷新计时器_Tick(sender As Object, e As EventArgs)
        Dim task = If(当前任务ID = "", Nothing, 编码队列_v6.根据ID获取任务(当前任务ID))
        更新任务性能计数器(task)
    End Sub

    Private Sub 更新任务性能计数器(task As 编码任务_v6)
        应用任务性能计数器可见性()
        If Not Panel2.Visible Then Exit Sub

        Dim processId = If(task Is Nothing, 0, task.当前进程ID)
        If processId <= 0 Then
            释放当前性能计数器()
            MB_任务性能计数器.Text = "当前任务没有活动进程"
            Exit Sub
        End If

        If 当前性能计数器 Is Nothing OrElse 当前性能计数器进程ID <> processId Then
            释放当前性能计数器()
            Try
                当前性能计数器 = LakeUI.MainAppUsageCounter.Start(processId, True)
                当前性能计数器进程ID = processId
            Catch ex As Exception
                当前性能计数器 = Nothing
                当前性能计数器进程ID = 0
                MB_任务性能计数器.Text = "任务性能计数器不可用"
                Exit Sub
            End Try
        End If

        Try
            Dim snapshot = 当前性能计数器.GetSnapshot(True)
            MB_任务性能计数器.Text = $"CPU {snapshot.CpuUsagePercent:F1}%   |   RAM {snapshot.ActivePrivateWorkingSetBytes / 1024 / 1024:F0}M / {snapshot.CommitSizeBytes / 1024 / 1024:F0}M   |   GPU {snapshot.GpuUsagePercent:F1}% {snapshot.GpuDedicatedMemoryBytes / 1024 / 1024:F0}M + {snapshot.GpuSharedMemoryBytes / 1024 / 1024:F0}M"
        Catch
            释放当前性能计数器()
            MB_任务性能计数器.Text = "当前任务没有活动进程"
        End Try
    End Sub

    Private Sub 释放当前性能计数器()
        If 当前性能计数器 IsNot Nothing Then
            Try
                当前性能计数器.Dispose()
            Catch
            End Try
        End If
        当前性能计数器 = Nothing
        当前性能计数器进程ID = 0
    End Sub

    Private Shared Function 获取任务显示名称(task As 编码任务_v6) As String
        If task Is Nothing Then Return ""
        If task.任务名称 <> "" Then Return task.任务名称
        Return IO.Path.GetFileName(task.输入文件)
    End Function

    Private Sub 居中到v6主窗口()
        Try
            Dim mainForm As Form = FormMain_v6
            Dim baseBounds As Rectangle
            If mainForm IsNot Nothing AndAlso Not mainForm.IsDisposed AndAlso mainForm.Visible AndAlso mainForm.WindowState <> FormWindowState.Minimized Then
                baseBounds = mainForm.Bounds
            Else
                baseBounds = Screen.FromPoint(Cursor.Position).WorkingArea
            End If

            Dim screenArea = Screen.FromRectangle(baseBounds).WorkingArea
            Dim x = baseBounds.Left + CInt(Math.Round((baseBounds.Width - Width) / 2.0))
            Dim y = baseBounds.Top + CInt(Math.Round((baseBounds.Height - Height) / 2.0))
            x = Math.Max(screenArea.Left, Math.Min(x, screenArea.Right - Width))
            y = Math.Max(screenArea.Top, Math.Min(y, screenArea.Bottom - Height))
            StartPosition = FormStartPosition.Manual
            Location = New Point(x, y)
        Catch
        End Try
    End Sub

    Private Function 获取当前显示模式() As 编码任务日志显示模式_v6
        Select Case MCB_显示模式.SelectedIndex
            Case 1 : Return 编码任务日志显示模式_v6.最新输出不含进度
            Case 2 : Return 编码任务日志显示模式_v6.仅错误信息
            Case 3 : Return 编码任务日志显示模式_v6.当前阶段输出
            Case Else : Return 编码任务日志显示模式_v6.全部输出
        End Select
    End Function

    Private Sub MCB_显示模式_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_显示模式.SelectedIndexChanged
        待刷新当前任务 = False
        待强制重载 = False
        刷新计时器.Stop()
        刷新当前任务(True)
    End Sub

    Private Sub ModernCheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles ModernCheckBox1.CheckedChanged
        ModernTextBox1.PreserveScrollPosition = Not ModernCheckBox1.Checked
        If ModernCheckBox1.Checked Then ModernTextBox1.ScrollToBottom()
    End Sub

    Private Sub ModernButton1_Click(sender As Object, e As EventArgs) Handles MB_复制当前视图.Click
        If String.IsNullOrWhiteSpace(ModernTextBox1.Text) Then Exit Sub
        Clipboard.SetText(ModernTextBox1.Text)
    End Sub
End Class
