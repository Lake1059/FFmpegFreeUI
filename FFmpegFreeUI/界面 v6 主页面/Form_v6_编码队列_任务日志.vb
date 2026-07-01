Imports System.Text

Public Class Form_v6_编码队列_任务日志
    Private Shared 当前实例 As Form_v6_编码队列_任务日志

    Private 当前任务ID As String = ""
    Private 当前显示模式 As 编码任务日志显示模式_v6 = 编码任务日志显示模式_v6.最新输出不含进度
    Private 已显示日志版本号 As Long = 0
    Private 已显示最小序号 As Long = 0
    Private 已显示最大序号 As Long = 0
    Private 已显示行数 As Integer = 0
    Private 已显示日志结构版本号 As Long = 0
    Private 当前阶段名 As String = ""
    Private 正在重载 As Boolean = False
    Private 窗体已加载 As Boolean = False
    Private 待刷新当前任务 As Boolean = False
    Private 待强制重载 As Boolean = False
    Private 日志刷新令牌 As Integer = 0
    Private 日志刷新取消源 As Threading.CancellationTokenSource
    Private ReadOnly 刷新计时器 As New Timer With {.Interval = 500}
    Private ReadOnly 性能计数器刷新计时器 As New Timer With {.Interval = 1000}
    Private 当前性能计数器 As LakeUI.MainAppUsageCounter
    Private 当前性能计数器进程ID As Integer = 0

    Private Class 日志刷新结果
        Public Property 任务ID As String = ""
        Public Property 任务显示名称 As String = ""
        Public Property 显示模式 As 编码任务日志显示模式_v6
        Public Property 阶段名 As String = ""
        Public Property 日志版本号 As Long = 0
        Public Property 日志结构版本号 As Long = 0
        Public Property 最小序号 As Long = 0
        Public Property 最大序号 As Long = 0
        Public Property 行数 As Integer = 0
        Public Property 强制重载 As Boolean = False
        Public Property 需要重载 As Boolean = False
        Public Property 重载文本 As String = ""
        Public Property 追加行 As New List(Of String)

        Public ReadOnly Property 有内容变化 As Boolean
            Get
                Return 需要重载 OrElse 追加行.Count > 0
            End Get
        End Property
    End Class

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
        窗体已加载 = True
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
        日志刷新令牌 += 1
        取消当前日志刷新()
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
                     请求刷新当前任务(False)
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
                     重置已显示日志状态()
                     If 窗体已加载 Then
                         ModernTextBox1.Clear()
                         更新标题与状态(获取任务显示名称(task), 0)
                         刷新当前任务(True)
                     End If
                 Else
                     If 窗体已加载 Then 刷新当前任务(False)
                 End If
             End Sub)
    End Sub

    Private Sub 刷新空状态(message As String)
        当前任务ID = ""
        重置已显示日志状态()
        当前阶段名 = ""
        待刷新当前任务 = False
        待强制重载 = False
        刷新计时器.Stop()
        日志刷新令牌 += 1
        取消当前日志刷新()
        ModernTextBox1.Clear()
        Text = "编码队列任务日志"
        更新任务性能计数器(Nothing)
    End Sub

    Private Sub 刷新当前任务(forceReload As Boolean)
        If 当前任务ID = "" OrElse Not 窗体已加载 Then Exit Sub

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

        Dim versionInfo = task.获取日志版本信息()
        If Not forceReload AndAlso versionInfo.日志版本号 = 已显示日志版本号 AndAlso versionInfo.日志结构版本号 = 已显示日志结构版本号 Then
            更新标题与状态(获取任务显示名称(task), 已显示行数)
            Exit Sub
        End If

        启动后台日志刷新(
            task,
            task.ID,
            获取任务显示名称(task),
            mode,
            stageName,
            forceReload,
            已显示日志版本号,
            已显示日志结构版本号,
            已显示最小序号,
            已显示最大序号,
            已显示行数)
    End Sub

    Private Sub 启动后台日志刷新(task As 编码任务_v6,
                            taskId As String,
                            taskDisplayName As String,
                            mode As 编码任务日志显示模式_v6,
                            stageName As String,
                            forceReload As Boolean,
                            displayedLogVersion As Long,
                            displayedStructureVersion As Long,
                            displayedMinSequence As Long,
                            displayedMaxSequence As Long,
                            displayedLineCount As Integer)
        日志刷新令牌 += 1
        Dim requestToken = 日志刷新令牌
        取消当前日志刷新()
        日志刷新取消源 = New Threading.CancellationTokenSource()
        Dim cancellationToken = 日志刷新取消源.Token

        Threading.Tasks.Task.Run(
            Sub()
                Try
                    Dim result = 构建日志刷新结果(
                        task,
                        taskId,
                        taskDisplayName,
                        mode,
                        stageName,
                        forceReload,
                        displayedLogVersion,
                        displayedStructureVersion,
                        displayedMinSequence,
                        displayedMaxSequence,
                        displayedLineCount,
                        cancellationToken)

                    If cancellationToken.IsCancellationRequested Then Exit Sub
                    UI执行(Sub()
                             If cancellationToken.IsCancellationRequested OrElse requestToken <> 日志刷新令牌 Then Exit Sub
                             应用日志刷新结果(result)
                         End Sub)
                Catch ex As OperationCanceledException
                Catch
                End Try
            End Sub,
            cancellationToken)
    End Sub

    Private Shared Function 构建日志刷新结果(task As 编码任务_v6,
                                      taskId As String,
                                      taskDisplayName As String,
                                      mode As 编码任务日志显示模式_v6,
                                      stageName As String,
                                      forceReload As Boolean,
                                      displayedLogVersion As Long,
                                      displayedStructureVersion As Long,
                                      displayedMinSequence As Long,
                                      displayedMaxSequence As Long,
                                      displayedLineCount As Integer,
                                      cancellationToken As Threading.CancellationToken) As 日志刷新结果
        cancellationToken.ThrowIfCancellationRequested()

        Dim snapshot = task.获取日志快照数据(mode, stageName)
        Dim entries = If(snapshot?.条目, New List(Of 编码任务日志条目_v6))
        Dim minSequence As Long = 0
        Dim maxSequence As Long = 0
        If entries.Count > 0 Then
            minSequence = entries(0).序号
            maxSequence = entries(entries.Count - 1).序号
        End If

        Dim result As New 日志刷新结果 With {
            .任务ID = taskId,
            .任务显示名称 = taskDisplayName,
            .显示模式 = mode,
            .阶段名 = stageName,
            .日志版本号 = snapshot.日志版本号,
            .日志结构版本号 = snapshot.日志结构版本号,
            .最小序号 = minSequence,
            .最大序号 = maxSequence,
            .行数 = entries.Count,
            .强制重载 = forceReload
        }

        Dim canAppend = Not forceReload
        If canAppend AndAlso displayedLineCount > 0 Then
            canAppend =
                entries.Count >= displayedLineCount AndAlso
                entries.Count > 0 AndAlso
                minSequence = displayedMinSequence AndAlso
                entries(displayedLineCount - 1).序号 = displayedMaxSequence
        End If

        result.需要重载 = Not canAppend
        If result.需要重载 Then
            result.重载文本 = 构建日志文本(entries, cancellationToken)
        Else
            For i = displayedLineCount To entries.Count - 1
                cancellationToken.ThrowIfCancellationRequested()
                result.追加行.Add(获取日志条目显示文本(entries(i)))
            Next
        End If

        Return result
    End Function

    Private Shared Function 构建日志文本(entries As List(Of 编码任务日志条目_v6), cancellationToken As Threading.CancellationToken) As String
        If entries Is Nothing OrElse entries.Count = 0 Then Return ""
        Dim sb As New StringBuilder()
        For i = 0 To entries.Count - 1
            cancellationToken.ThrowIfCancellationRequested()
            If i > 0 Then sb.AppendLine()
            sb.Append(获取日志条目显示文本(entries(i)))
        Next
        Return sb.ToString()
    End Function

    Private Sub 应用日志刷新结果(result As 日志刷新结果)
        If result Is Nothing OrElse 当前任务ID <> result.任务ID Then Exit Sub
        正在重载 = True
        Dim originalPreserveScrollPosition = ModernTextBox1.PreserveScrollPosition
        Try
            Dim shouldScrollToBottom = ModernCheckBox1.Checked AndAlso result.有内容变化
            If result.需要重载 Then
                ModernTextBox1.PreserveScrollPosition = Not result.强制重载 AndAlso Not shouldScrollToBottom
                ModernTextBox1.Text = result.重载文本
            Else
                ModernTextBox1.PreserveScrollPosition = Not shouldScrollToBottom
                For Each line In result.追加行
                    ModernTextBox1.AppendLine(line)
                Next
            End If

            已显示日志版本号 = result.日志版本号
            已显示日志结构版本号 = result.日志结构版本号
            已显示最小序号 = result.最小序号
            已显示最大序号 = result.最大序号
            已显示行数 = result.行数
            更新标题与状态(result.任务显示名称, result.行数)
            If shouldScrollToBottom Then 延迟滚动到底部()
        Finally
            ModernTextBox1.PreserveScrollPosition = originalPreserveScrollPosition
            正在重载 = False
        End Try
    End Sub

    Private Sub 取消当前日志刷新()
        If 日志刷新取消源 Is Nothing Then Exit Sub
        Try
            日志刷新取消源.Cancel()
        Catch
        End Try
        Try
            日志刷新取消源.Dispose()
        Catch
        End Try
        日志刷新取消源 = Nothing
    End Sub

    Private Sub 重置已显示日志状态()
        已显示日志版本号 = 0
        已显示最小序号 = 0
        已显示最大序号 = 0
        已显示行数 = 0
        已显示日志结构版本号 = 0
    End Sub

    Private Shared Function 获取日志条目显示文本(entry As 编码任务日志条目_v6) As String
        Return If(entry?.文本, "").Replace(vbCr, "").Replace(vbLf, "")
    End Function

    Private Sub 延迟滚动到底部()
        If Not ModernCheckBox1.Checked Then Exit Sub
        BeginInvoke(Sub()
                        If IsDisposed OrElse Not ModernCheckBox1.Checked Then Exit Sub
                        ModernTextBox1.ScrollToBottom()
                    End Sub)
    End Sub

    Private Sub 更新标题与状态(taskDisplayName As String, lineCount As Integer)
        Text = "任务日志 - " & taskDisplayName
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
