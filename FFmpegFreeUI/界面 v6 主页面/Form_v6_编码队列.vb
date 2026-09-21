Imports System.IO
Imports LakeUI

Public Class Form_v6_编码队列
    Private DPI As Single = 1.0F
    Private Shared ReadOnly 非任务列常用文本 As String() = {"正在处理", "100.0%", "1000%", "99.00 GB - 99.0 GB", "质量0", "100.00 Mbps", "99h99m99s - 99h99m99s"}
    Private Const 任务名称列最小宽度基准 As Integer = 96
    Private Const 列宽滚动条预留宽度基准 As Integer = 18
    Private Const 列宽文本预留宽度基准 As Integer = 20

    Private ReadOnly 任务行 As New Dictionary(Of String, UltraDetailListView.ListItem)
    Private ReadOnly 展示策略 As I编码队列展示策略_v6 = New 旧版兼容编码队列展示策略_v6()
    Private WithEvents 列宽调整计时器 As New System.Windows.Forms.Timer With {.Interval = 80}
    Private 上次列宽有效总宽度 As Integer = -1
    Private 上次列宽Dpi As Single = -1
    Private 正在刷新列表 As Boolean = False
    Private ReadOnly 刷新锁 As New Object
    Private ReadOnly 待刷新任务 As New Dictionary(Of String, 编码任务_v6)(StringComparer.Ordinal)
    Private 待刷新整表 As Boolean
    Private 立即刷新已投递 As Boolean
    Private Shared ReadOnly 刷新间隔 As Integer() = {200, 500, 1000, 2000, 3000}
    Private WithEvents 队列刷新计时器 As New System.Windows.Forms.Timer With {.Interval = 1000}

    Private Sub Form_v6_编码队列_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UltraDetailListView1.Items.Clear()
        UltraDetailListView1.AllowDrop = True
        UltraDetailListView1.AllowDragReorder = True
        调整列表交互区域()
        调整顶部按钮组居中()
        绑定菜单事件()
        AddHandler 编码队列_v6.队列已变化, AddressOf 队列已变化
        AddHandler 编码队列_v6.任务已更新, AddressOf 任务已更新
        AddHandler 编码队列_v6.任务需立即刷新, AddressOf 请求立即刷新
        AddHandler 界面主题_v6.主题已更改, AddressOf 刷新整表
        刷新整表()
        队列刷新计时器.Start()
        请求校准编码队列列宽()
    End Sub

    Private Sub Form_v6_编码队列_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        DPI = 获取列宽Dpi比例()
        重置列宽校准缓存()
        请求校准编码队列列宽()
    End Sub

    Private Sub Form_v6_编码队列_DpiChanged(sender As Object, e As DpiChangedEventArgs) Handles Me.DpiChanged
        DPI = 获取列宽Dpi比例()
        重置列宽校准缓存()
        请求校准编码队列列宽()
    End Sub

    Private Sub Form_v6_编码队列_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        RemoveHandler 编码队列_v6.队列已变化, AddressOf 队列已变化
        RemoveHandler 编码队列_v6.任务已更新, AddressOf 任务已更新
        RemoveHandler 编码队列_v6.任务需立即刷新, AddressOf 请求立即刷新
        RemoveHandler 界面主题_v6.主题已更改, AddressOf 刷新整表
        队列刷新计时器.Stop()
        队列刷新计时器.Dispose()
        列宽调整计时器.Stop()
        列宽调整计时器.Dispose()
    End Sub

    Private Sub 队列已变化()
        SyncLock 刷新锁
            待刷新整表 = True
        End SyncLock
        请求立即刷新()
    End Sub

    Private Sub 任务已更新(任务 As 编码任务_v6)
        If 任务 Is Nothing Then Exit Sub
        SyncLock 刷新锁
            待刷新任务(任务.ID) = 任务
        End SyncLock
    End Sub

    Private Sub 队列刷新计时器_Tick(sender As Object, e As EventArgs) Handles 队列刷新计时器.Tick
        队列刷新计时器.Interval = 刷新间隔(Math.Clamp(设置_v6.实例对象.编码队列刷新速度, 0, 刷新间隔.Length - 1))
        刷新待处理更改()
    End Sub

    Private Sub 请求立即刷新()
        If IsDisposed OrElse Disposing OrElse Not IsHandleCreated Then Exit Sub
        SyncLock 刷新锁
            If 立即刷新已投递 Then Exit Sub
            立即刷新已投递 = True
        End SyncLock
        Try
            ' Coalesce a batch of commands into the next UI turn without waiting for the progress timer.
            BeginInvoke(Sub()
                            SyncLock 刷新锁
                                立即刷新已投递 = False
                            End SyncLock
                            If Not IsDisposed AndAlso Not Disposing Then 刷新待处理更改()
                        End Sub)
        Catch ex As InvalidOperationException
            SyncLock 刷新锁
                立即刷新已投递 = False
            End SyncLock
        End Try
    End Sub

    Private Sub 刷新待处理更改()
        Dim reload As Boolean
        Dim changed As List(Of 编码任务_v6)
        SyncLock 刷新锁
            reload = 待刷新整表
            If Not reload AndAlso 待刷新任务.Count = 0 Then Exit Sub
            changed = 待刷新任务.Values.ToList()
            待刷新任务.Clear()
            待刷新整表 = False
        End SyncLock
        If reload Then
            刷新整表()
            Exit Sub
        End If
        UltraDetailListView1.BeginUpdate()
        Try
            For Each task In changed
                刷新任务行(task)
            Next
        Finally
            UltraDetailListView1.EndUpdate()
        End Try
        更新标题(编码队列_v6.获取队列快照())
    End Sub

    Private Sub 刷新整表()
        If 正在刷新列表 Then Exit Sub
        正在刷新列表 = True
        UltraDetailListView1.BeginUpdate()
        Try
            Dim snapshot = 编码队列_v6.获取队列快照()
            Dim oldRows = New Dictionary(Of String, UltraDetailListView.ListItem)(任务行)
            UltraDetailListView1.Items.Clear()
            任务行.Clear()
            For Each task In snapshot
                Dim item As UltraDetailListView.ListItem = Nothing
                If Not oldRows.TryGetValue(task.ID, item) Then item = 创建任务行(task)
                UltraDetailListView1.Items.Add(item)
                任务行(task.ID) = item
                刷新任务行(task)
            Next
            更新标题(snapshot)
        Finally
            UltraDetailListView1.EndUpdate()
            正在刷新列表 = False
        End Try
        Form_v6_编码队列_任务日志.同步队列选择(获取选中任务ID())
    End Sub

    Private Function 创建任务行(task As 编码任务_v6) As UltraDetailListView.ListItem
        Dim item As New UltraDetailListView.ListItem With {.Tag = task.ID}
        For i = 0 To 7
            item.SubItems.Add(New UltraDetailListView.ListSubItem())
        Next
        Return item
    End Function

    Private Sub 刷新任务行(task As 编码任务_v6)
        If task Is Nothing Then Exit Sub
        Dim item As UltraDetailListView.ListItem = Nothing
        If Not 任务行.TryGetValue(task.ID, item) Then Exit Sub
        展示策略.应用(task, item)
        item.InvalidateCache()
    End Sub

    Private Function 获取选中任务ID() As List(Of String)
        Return UltraDetailListView1.SelectedItems.
            Select(Function(item) TryCast(item.Tag, String)).
            Where(Function(id) Not String.IsNullOrWhiteSpace(id)).
            ToList()
    End Function

    Private Sub 更新标题(items As List(Of 编码任务_v6))
        Dim running As Integer = 0
        Dim errors As Integer = 0
        Dim completed As Integer = 0
        For Each task In items
            If task.状态 = 编码任务状态_v6.正在处理 OrElse task.状态 = 编码任务状态_v6.已暂停 Then running += 1
            If task.状态 = 编码任务状态_v6.错误 Then errors += 1
            If task.状态 = 编码任务状态_v6.已完成 Then completed += 1
        Next
        Dim 标签颜色 = System.Drawing.ColorTranslator.ToHtml(界面主题_v6.获取当前主题前景色(Color.DarkGray))
        HtmlColorLabel1.Text =
            $"<span style=""color:{标签颜色}"">数量 </span><span style=""font-size:14pt; font-weight:bold; color:CornflowerBlue"">{completed}/{items.Count}</span>" &
            $"   <span style=""color:{标签颜色}"">运行 </span><span style=""font-size:14pt; font-weight:bold; color:YellowGreen"">{running}</span>" &
            $"   <span style=""color:{标签颜色}"">错误 </span><span style=""font-size:14pt; font-weight:bold; color:{界面配色_v6.错误文本色Html}"">{errors}</span>"
    End Sub

    Private Sub Panel1_SizeChanged(sender As Object, e As EventArgs) Handles Panel1.SizeChanged
        调整顶部按钮组居中()
    End Sub

    Private Sub 调整顶部按钮组居中()
        If Panel1 Is Nothing OrElse JustEmptyControl1 Is Nothing Then Exit Sub

        Dim 按钮组宽度 = ModernButton1.Width + ModernButton2.Width + ModernButton3.Width + ModernButton4.Width + ModernButton5.Width + ModernButton6.Width + ModernButton7.Width
        Dim 按钮组目标左侧 = CInt(Math.Round((Panel1.ClientSize.Width - 按钮组宽度) / 2.0))
        Dim 已占用左侧宽度 = Panel1.Padding.Left + ModernButton8.Width
        Dim 新宽度 = Math.Max(0, 按钮组目标左侧 - 已占用左侧宽度)

        If JustEmptyControl1.Width <> 新宽度 Then JustEmptyControl1.Width = 新宽度
    End Sub

    Private Sub 绑定菜单事件()
        AddHandler 任务菜单.Items(0).Click, AddressOf 添加文件到队列
        AddHandler 任务菜单.Items(1).Click, AddressOf 复制选中任务命令行
        AddHandler 任务菜单.Items(2).Click, AddressOf 打开任务日志
        AddHandler 任务菜单.Items(3).Click, AddressOf 将任务参数覆盖到参数面板
        AddHandler 任务菜单.Items(4).Click, AddressOf 将参数面板应用到选中任务
        AddHandler 任务菜单.Items(6).Click, AddressOf 全选任务
        AddHandler 任务菜单.Items(7).Click, AddressOf 选中错误任务
        AddHandler 任务菜单.Items(8).Click, Sub() 编码队列_v6.上移任务(获取选中任务ID())
        AddHandler 任务菜单.Items(9).Click, Sub() 编码队列_v6.下移任务(获取选中任务ID())

        AddHandler 右键菜单.Items(0).Click, AddressOf 打开任务日志
        AddHandler 右键菜单.Items(1).Click, AddressOf 定位输出
        AddHandler 右键菜单.Items(2).Click, AddressOf 复制选中任务命令行
        AddHandler 右键菜单.Items(3).Click, AddressOf 查看任务参数
    End Sub

    Private Sub 添加文件到队列()
        Using d As New OpenFileDialog With {.Multiselect = True, .Filter = "所有文件|*.*"}
            If d.ShowDialog(Me) <> DialogResult.OK Then Exit Sub
            Dim count = 编码队列_v6.添加来自参数面板的文件(d.FileNames, Form_v6_参数面板)
            If count > 0 Then ExFloatingTip(ModernButton8, $"已添加 {count} 个任务", 1200)
        End Using
    End Sub

    Private Sub 复制选中任务命令行()
        Dim lines As New List(Of String)
        For Each id In 获取选中任务ID()
            Dim task = 编码队列_v6.根据ID获取任务(id)
            If task Is Nothing Then Continue For
            Dim commandText = 编码队列_v6.获取任务实际命令行文本(task)
            If commandText <> "" Then lines.Add(commandText)
        Next
        If lines.Count > 0 Then
            Clipboard.SetText(String.Join(vbCrLf & vbCrLf, lines))
            ExFloatingTip(ModernButton8, "已复制命令行", 1200)
        End If
    End Sub

    Private Sub 将任务参数覆盖到参数面板()
        Dim ids = 获取选中任务ID()
        If ids.Count <> 1 Then Exit Sub
        Dim task = 编码队列_v6.根据ID获取任务(ids(0))
        If task?.预设数据 Is Nothing Then Exit Sub
        预设管理_v6.显示预设(编码队列_v6.克隆预设(task.预设数据), Form_v6_参数面板)
    End Sub

    Private Sub 将参数面板应用到选中任务()
        Dim ids = 获取选中任务ID()
        If ids.Count = 0 Then
            ExFloatingTip(ModernButton8, "请先选择任务", 1200)
            Exit Sub
        End If

        Dim preset = 预设管理_v6.从面板创建预设(Form_v6_参数面板)
        Dim result = 编码队列_v6.同步指定未处理预设任务(ids, preset)
        Dim parts As New List(Of String) From {$"已更新 {result.已更新} 个任务"}
        If result.已跳过非预设任务 > 0 Then parts.Add($"跳过 {result.已跳过非预设任务} 个命令行任务")
        If result.已跳过不可修改任务 > 0 Then parts.Add($"跳过 {result.已跳过不可修改任务} 个已开始或已结束任务")
        ExFloatingTip(ModernButton8, String.Join("，", parts), 1600)
    End Sub

    Private Sub 全选任务()
        UltraDetailListView1.SelectAll()
    End Sub

    Private Sub 选中错误任务()
        Dim snapshot = 编码队列_v6.获取队列快照()
        Dim index = snapshot.FindIndex(Function(x) x.状态 = 编码任务状态_v6.错误)
        If index >= 0 Then
            UltraDetailListView1.SelectedIndex = index
            UltraDetailListView1.EnsureVisible(index)
        End If
    End Sub

    Private Sub 定位输出()
        Dim ids = 获取选中任务ID()
        If ids.Count <> 1 Then Exit Sub
        Dim task = 编码队列_v6.根据ID获取任务(ids(0))
        If task Is Nothing Then Exit Sub

        If task.状态 <> 编码任务状态_v6.未处理 AndAlso File.Exists(task.输出文件) Then
            在资源管理器中定位(task.输出文件)
            Exit Sub
        End If

        If Not File.Exists(task.输入文件) Then
            ExMsgBox(Me, "没有可定位的输出文件，输入文件也不存在。", MsgBoxStyle.Information, "定位输出")
            Exit Sub
        End If

        Dim prompt = If(task.状态 = 编码任务状态_v6.未处理,
                        "任务尚未开始，没有可定位的输出文件。是否改为定位输入文件？",
                        "没有找到输出文件。是否改为定位输入文件？")
        If ExMsgBox(Me, prompt, MsgBoxStyle.YesNo Or MsgBoxStyle.Question, "定位输出") = MsgBoxResult.Yes Then
            在资源管理器中定位(task.输入文件)
        End If
    End Sub

    Private Shared Sub 在资源管理器中定位(filePath As String)
        Dim startInfo As New ProcessStartInfo With {
            .FileName = "explorer.exe",
            .Arguments = "/select,""" & Path.GetFullPath(filePath) & """",
            .UseShellExecute = True
        }
        Process.Start(startInfo)
    End Sub

    Private Sub 打开输出文件(task As 编码任务_v6)
        If task Is Nothing OrElse Not File.Exists(task.输出文件) Then Exit Sub
        Process.Start(New ProcessStartInfo With {
            .FileName = Path.GetFullPath(task.输出文件),
            .UseShellExecute = True
        })
    End Sub

    Private Sub 打开任务日志()
        Form_v6_编码队列_任务日志.打开或激活(Me, 获取选中任务ID())
    End Sub

    Private Sub 查看任务参数()
        Dim ids = 获取选中任务ID()
        If ids.Count = 0 Then
            ExFloatingTip(UltraDetailListView1, "请先选择任务", 1200)
            Exit Sub
        End If

        For Each id In ids
            Dim task = 编码队列_v6.根据ID获取任务(id)
            If task Is Nothing Then Continue For
            Dim form As New Form_v6_编码队列_查看参数(task)
            显示窗体(form, Me)
        Next
    End Sub

    Private Sub 开始选中任务()
        编码队列_v6.开始任务(获取选中任务ID())
    End Sub

    Private Sub 暂停选中任务()
        编码队列_v6.暂停任务(获取选中任务ID())
    End Sub

    Private Sub 恢复选中任务()
        编码队列_v6.恢复任务(获取选中任务ID())
    End Sub

    Private Sub 暂停或恢复选中任务()
        Dim ids = 获取选中任务ID()
        If ids.Count = 0 Then Exit Sub

        Dim 存在暂停任务 = ids.
            Select(Function(id) 编码队列_v6.根据ID获取任务(id)).
            Any(Function(task) task IsNot Nothing AndAlso task.状态 = 编码任务状态_v6.已暂停)
        If 存在暂停任务 Then
            编码队列_v6.恢复任务(ids)
        Else
            编码队列_v6.暂停任务(ids)
        End If
    End Sub

    Private Sub 移除选中任务()
        编码队列_v6.移除任务(获取选中任务ID())
    End Sub

    Private Sub UltraDetailListView1_ItemOrderChanged(sender As Object, e As EventArgs) Handles UltraDetailListView1.ItemOrderChanged
        If 正在刷新列表 Then Exit Sub
        Dim ids = UltraDetailListView1.Items.Select(Function(x) TryCast(x.Tag, String)).Where(Function(x) Not String.IsNullOrWhiteSpace(x)).ToList()
        If Not 编码队列_v6.重新排序(ids) Then
            ExFloatingTip(UltraDetailListView1, "运行中的任务不能排序", 1200)
            刷新整表()
        End If
    End Sub

    Private Sub UltraDetailListView1_AfterLabelEdit(sender As Object, e As UltraDetailListView.LabelEditEventArgs) Handles UltraDetailListView1.AfterLabelEdit
        If e.ColumnIndex <> 0 Then
            e.CancelEdit = True
            Exit Sub
        End If

        Dim id = TryCast(e.Item?.Tag, String)
        If String.IsNullOrWhiteSpace(id) Then
            e.CancelEdit = True
            Exit Sub
        End If

        Dim task = 编码队列_v6.根据ID获取任务(id)
        If task Is Nothing Then
            e.CancelEdit = True
            Exit Sub
        End If

        ' Keep the edited label in the task model so a later queue reorder refresh
        ' does not replace it with the input file name.
        task.任务名称 = If(e.Label, "")
    End Sub

    Private Sub UltraDetailListView1_DragEnter(sender As Object, e As DragEventArgs) Handles UltraDetailListView1.DragEnter
        e.Effect = If(e.Data.GetDataPresent(DataFormats.FileDrop), DragDropEffects.Copy, DragDropEffects.None)
    End Sub

    Private Sub UltraDetailListView1_DragDrop(sender As Object, e As DragEventArgs) Handles UltraDetailListView1.DragDrop
        Dim files = TryCast(e.Data.GetData(DataFormats.FileDrop), String())
        If files Is Nothing OrElse files.Length = 0 Then Exit Sub
        Dim count = 编码队列_v6.添加来自参数面板的文件(files, Form_v6_参数面板)
        If count > 0 Then ExFloatingTip(UltraDetailListView1, $"已添加 {count} 个任务", 1200)
    End Sub

    Private Sub UltraDetailListView1_MouseUp(sender As Object, e As MouseEventArgs) Handles UltraDetailListView1.MouseUp
        If e.Button = MouseButtons.Right Then 右键菜单.Show(UltraDetailListView1, e.X, e.Y)
    End Sub

    Private Sub UltraDetailListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles UltraDetailListView1.SelectedIndexChanged
        If 正在刷新列表 Then Exit Sub
        Form_v6_编码队列_任务日志.同步队列选择(获取选中任务ID())
    End Sub

    Private Sub UltraDetailListView1_ItemDoubleClick(sender As Object, e As UltraDetailListView.ListItemEventArgs) Handles UltraDetailListView1.ItemDoubleClick
        Dim id = TryCast(e.Item?.Tag, String)
        If String.IsNullOrWhiteSpace(id) Then
            打开任务日志()
            Exit Sub
        End If

        Dim task = 编码队列_v6.根据ID获取任务(id)
        If task Is Nothing Then Exit Sub

        Select Case task.状态
            Case 编码任务状态_v6.已完成
                打开输出文件(task)
            Case 编码任务状态_v6.未处理
                Dim form As New Form_v6_编码队列_查看参数(task)
                显示窗体(form, Me)
            Case Else
                Form_v6_编码队列_任务日志.打开或激活(Me, New String() {id})
        End Select
    End Sub

    Private Sub UltraDetailListView1_KeyDown(sender As Object, e As KeyEventArgs) Handles UltraDetailListView1.KeyDown
        Select Case e.KeyCode
            Case Keys.Enter
                开始选中任务()
            Case Keys.Delete
                移除选中任务()
            Case Keys.Space
                暂停或恢复选中任务()
            Case Else
                Exit Sub
        End Select

        e.Handled = True
        e.SuppressKeyPress = True
    End Sub

    Private Sub ModernButton1_Click(sender As Object, e As EventArgs) Handles ModernButton1.Click
        开始选中任务()
    End Sub

    Private Sub ModernButton2_Click(sender As Object, e As EventArgs) Handles ModernButton2.Click
        暂停选中任务()
    End Sub

    Private Sub ModernButton3_Click(sender As Object, e As EventArgs) Handles ModernButton3.Click
        恢复选中任务()
    End Sub

    Private Sub ModernButton4_Click(sender As Object, e As EventArgs) Handles ModernButton4.Click
        编码队列_v6.停止任务(获取选中任务ID())
    End Sub

    Private Sub ModernButton5_Click(sender As Object, e As EventArgs) Handles ModernButton5.Click
        移除选中任务()
    End Sub

    Private Sub ModernButton6_Click(sender As Object, e As EventArgs) Handles ModernButton6.Click
        编码队列_v6.重置任务(获取选中任务ID())
    End Sub

    Private Sub ModernButton7_Click(sender As Object, e As EventArgs) Handles ModernButton7.Click
        定位输出()
    End Sub

    Private Sub ModernButton8_Click(sender As Object, e As EventArgs) Handles ModernButton8.Click
        任务菜单.Show(ModernButton8, 0, ModernButton8.Height)
    End Sub

    Private Sub UltraDetailListView1_SizeChanged(sender As Object, e As EventArgs) Handles UltraDetailListView1.SizeChanged
        调整列表交互区域()
        请求校准编码队列列宽()
    End Sub

    Private Sub 调整列表交互区域()
        If UltraDetailListView1 Is Nothing Then Exit Sub
        UltraDetailListView1.DragSelectZoneWidth = Math.Max(0, UltraDetailListView1.Size.Width \ 2)
    End Sub

    Private Sub UltraDetailListView1_DpiChangedAfterParent(sender As Object, e As EventArgs) Handles UltraDetailListView1.DpiChangedAfterParent
        DPI = 获取列宽Dpi比例()
        重置列宽校准缓存()
        请求校准编码队列列宽()
    End Sub

    Private Sub UltraDetailListView1_FontChanged(sender As Object, e As EventArgs) Handles UltraDetailListView1.FontChanged
        重置列宽校准缓存()
        请求校准编码队列列宽()
    End Sub

    Private Sub 列宽调整计时器_Tick(sender As Object, e As EventArgs) Handles 列宽调整计时器.Tick
        列宽调整计时器.Stop()
        校准编码队列列宽()
    End Sub

    Private Sub 请求校准编码队列列宽()
        If IsDisposed OrElse UltraDetailListView1 Is Nothing Then Exit Sub
        列宽调整计时器.Stop()
        列宽调整计时器.Start()
    End Sub

    Private Sub 重置列宽校准缓存()
        上次列宽有效总宽度 = -1
        上次列宽Dpi = -1
    End Sub

    Private Function 获取列宽Dpi比例() As Single
        Return D3D_D2DInterop.GetCurrentDpiScale(UltraDetailListView1)
    End Function

    Private Sub 校准编码队列列宽()
        If IsDisposed OrElse UltraDetailListView1 Is Nothing OrElse UltraDetailListView1.Columns.Count < 8 Then Exit Sub

        DPI = 获取列宽Dpi比例()
        Dim 有效总宽度 = UltraDetailListView1.ClientSize.Width -
                          UltraDetailListView1.Padding.Left -
                          UltraDetailListView1.Padding.Right -
                          缩放宽度(列宽滚动条预留宽度基准, DPI)

        If 有效总宽度 <= 0 Then Exit Sub
        If 有效总宽度 = 上次列宽有效总宽度 AndAlso Math.Abs(DPI - 上次列宽Dpi) < 0.001F Then Exit Sub
        上次列宽有效总宽度 = 有效总宽度
        上次列宽Dpi = DPI

        Dim 非任务列宽 = 获取非任务列最大宽度()
        Dim 任务名称列最小宽度 = 缩放宽度(任务名称列最小宽度基准, DPI)
        Dim 非任务列总宽度 = 非任务列宽.Sum()

        If 有效总宽度 - 非任务列总宽度 < 任务名称列最小宽度 Then
            Dim 非任务列目标宽度 = Math.Max(0, 有效总宽度 - 任务名称列最小宽度)
            非任务列宽 = 按最大值压缩列宽(非任务列宽, 非任务列目标宽度)
            非任务列总宽度 = 非任务列宽.Sum()
        End If

        UltraDetailListView1.BeginUpdate()
        Try
            设置列宽(0, Math.Max(缩放宽度(60, DPI), 有效总宽度 - 非任务列总宽度))
            For i = 0 To 非任务列宽.Length - 1
                设置列宽(i + 1, 非任务列宽(i))
            Next
        Finally
            UltraDetailListView1.EndUpdate()
        End Try
    End Sub

    Private Function 获取非任务列最大宽度() As Integer()
        Dim result(非任务列常用文本.Length - 1) As Integer
        For i = 0 To 非任务列常用文本.Length - 1
            result(i) = 测量列文本宽度(非任务列常用文本(i))
        Next
        Return result
    End Function

    Private Function 测量列文本宽度(text As String) As Integer
        Dim flags = TextFormatFlags.Left Or TextFormatFlags.Top Or TextFormatFlags.NoPadding Or TextFormatFlags.SingleLine
        Dim size = D3D_TextInterop.MeasureText(If(text, ""), UltraDetailListView1.Font, New Size(Integer.MaxValue, Integer.MaxValue), flags, DPI)
        Return size.Width + 缩放宽度(列宽文本预留宽度基准, DPI)
    End Function

    Private Shared Function 按最大值压缩列宽(最大宽度 As Integer(), 目标总宽度 As Integer) As Integer()
        Dim 最大总宽度 = 最大宽度.Sum()
        If 目标总宽度 >= 最大总宽度 Then Return DirectCast(最大宽度.Clone(), Integer())

        Dim result(最大宽度.Length - 1) As Integer
        If 目标总宽度 <= 0 OrElse 最大总宽度 <= 0 Then Return result

        Dim 缩放比例 = 目标总宽度 / CDbl(最大总宽度)
        For i = 0 To 最大宽度.Length - 1
            result(i) = CInt(Math.Floor(最大宽度(i) * 缩放比例))
        Next
        Return result
    End Function

    Private Shared Function 缩放宽度(value As Integer, dpi As Single) As Integer
        Return Math.Max(1, CInt(Math.Round(value * dpi)))
    End Function

    Private Sub 设置列宽(columnIndex As Integer, width As Integer)
        If UltraDetailListView1.Columns(columnIndex).Width <> width Then UltraDetailListView1.Columns(columnIndex).Width = width
    End Sub
End Class
