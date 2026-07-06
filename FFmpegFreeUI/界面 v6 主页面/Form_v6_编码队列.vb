Imports System.IO
Imports LakeUI

Public Class Form_v6_编码队列
    Private DPI As Single = 1.0F
    Private Shared ReadOnly 非任务列理想宽度基准 As Integer() = {82, 70, 66, 136, 54, 98, 145}
    Private Shared ReadOnly 非任务列最小宽度基准 As Integer() = {72, 60, 56, 112, 46, 82, 118}
    Private Shared ReadOnly 非任务列常用文本 As String() = {"正在处理", "100.0%", "1000%", "999 MB - 999 MB", "99", "100.00 Mbps", "9h99m99s - 9h99m99s"}
    Private Const 任务名称列最小宽度基准 As Integer = 96
    Private Const 列宽滚动条预留宽度基准 As Integer = 18
    Private Const 列宽文本预留宽度基准 As Integer = 20

    Private ReadOnly 任务行 As New Dictionary(Of String, UltraDetailListView.ListItem)
    Private ReadOnly 展示策略 As I编码队列展示策略_v6 = New 旧版兼容编码队列展示策略_v6()
    Private WithEvents 列宽调整计时器 As New System.Windows.Forms.Timer With {.Interval = 80}
    Private 上次列宽有效总宽度 As Integer = -1
    Private 上次列宽Dpi As Single = -1
    Private 菜单已初始化 As Boolean = False
    Private 正在刷新列表 As Boolean = False

    Private Sub Form_v6_编码队列_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UltraDetailListView1.Items.Clear()
        UltraDetailListView1.AllowDrop = True
        UltraDetailListView1.AllowDragReorder = True
        调整顶部按钮组居中()
        初始化菜单()
        AddHandler 编码队列_v6.队列已变化, AddressOf 队列已变化
        AddHandler 编码队列_v6.任务已更新, AddressOf 任务已更新
        刷新整表()
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
        列宽调整计时器.Stop()
        列宽调整计时器.Dispose()
    End Sub

    Private Sub 队列已变化()
        UI执行(Sub() 刷新整表())
    End Sub

    Private Sub 任务已更新(任务 As 编码任务_v6)
        If 任务 Is Nothing Then Exit Sub
        UI执行(Sub() 刷新任务行(任务))
    End Sub

    Private Sub UI执行(action As Action)
        If action Is Nothing OrElse IsDisposed Then Exit Sub
        If InvokeRequired Then
            BeginInvoke(action)
        Else
            action()
        End If
    End Sub

    Private Sub 刷新整表()
        If 正在刷新列表 Then Exit Sub
        正在刷新列表 = True
        UltraDetailListView1.BeginUpdate()
        Try
            Dim selectedIds = 获取选中任务ID()
            UltraDetailListView1.Items.Clear()
            任务行.Clear()
            For Each task In 编码队列_v6.获取队列快照()
                Dim item = 创建任务行(task)
                UltraDetailListView1.Items.Add(item)
                任务行(task.ID) = item
                刷新任务行(task)
            Next
            If selectedIds.Count > 0 Then
                Dim index = 编码队列_v6.获取队列快照().FindIndex(Function(x) selectedIds.Contains(x.ID))
                If index >= 0 Then UltraDetailListView1.SelectedIndex = index
            End If
            更新标题()
            Form_v6_编码队列_任务日志.同步队列选择(获取选中任务ID())
        Finally
            UltraDetailListView1.EndUpdate()
            正在刷新列表 = False
        End Try
    End Sub

    Private Function 创建任务行(task As 编码任务_v6) As UltraDetailListView.ListItem
        Dim item As New UltraDetailListView.ListItem With {.Tag = task.ID}
        For i = 0 To 7
            item.SubItems.Add(New UltraDetailListView.ListSubItem())
        Next
        展示策略.应用(task, item)
        Return item
    End Function

    Private Sub 刷新任务行(task As 编码任务_v6)
        If task Is Nothing Then Exit Sub
        Dim item As UltraDetailListView.ListItem = Nothing
        If Not 任务行.TryGetValue(task.ID, item) Then
            刷新整表()
            Exit Sub
        End If

        UltraDetailListView1.BeginUpdate()
        Try
            展示策略.应用(task, item)
            item.InvalidateCache()
        Finally
            UltraDetailListView1.EndUpdate()
        End Try
        更新标题()
    End Sub

    Private Function 获取选中任务ID() As List(Of String)
        Return UltraDetailListView1.SelectedItems.
            Select(Function(item) TryCast(item.Tag, String)).
            Where(Function(id) Not String.IsNullOrWhiteSpace(id)).
            ToList()
    End Function

    Private Sub 更新标题()
        Dim items = 编码队列_v6.获取队列快照()
        Dim running = items.Where(Function(x) x.状态 = 编码任务状态_v6.正在处理 OrElse x.状态 = 编码任务状态_v6.已暂停).Count()
        Dim errors = items.Where(Function(x) x.状态 = 编码任务状态_v6.错误).Count()
        HtmlColorLabel1.Text =
            $"<span style=""color:DarkGray"">总数 </span><span style=""font-size:14pt; font-weight:bold; color:CornflowerBlue"">{items.Count}</span>" &
            $"   <span style=""color:DarkGray"">运行 </span><span style=""font-size:14pt; font-weight:bold; color:YellowGreen"">{running}</span>" &
            $"   <span style=""color:DarkGray"">错误 </span><span style=""font-size:14pt; font-weight:bold; color:{界面配色_v6.错误文本色Html}"">{errors}</span>"
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

    Private Sub 初始化菜单()
        If 菜单已初始化 Then Exit Sub
        菜单已初始化 = True

        添加菜单项(任务菜单, "添加文件到队列", AddressOf 添加文件到队列)
        添加菜单项(任务菜单, "复制选中任务命令行", AddressOf 复制选中任务命令行)
        添加菜单项(任务菜单, "打开任务日志", AddressOf 打开任务日志)
        添加菜单项(任务菜单, "将任务参数覆盖到参数面板", AddressOf 将任务参数覆盖到参数面板)
        添加菜单项(任务菜单, "将参数面板应用到选中任务", AddressOf 将参数面板应用到选中任务)
        添加菜单分割线(任务菜单)
        添加菜单项(任务菜单, "全选", AddressOf 全选任务)
        添加菜单项(任务菜单, "选中所有错误任务", AddressOf 选中错误任务)
        添加菜单项(任务菜单, "上移选中任务", Sub() 编码队列_v6.上移任务(获取选中任务ID()))
        添加菜单项(任务菜单, "下移选中任务", Sub() 编码队列_v6.下移任务(获取选中任务ID()))

        添加菜单项(右键菜单, "开始", Sub() 编码队列_v6.开始任务(获取选中任务ID()))
        添加菜单项(右键菜单, "暂停", Sub() 编码队列_v6.暂停任务(获取选中任务ID()))
        添加菜单项(右键菜单, "恢复", Sub() 编码队列_v6.恢复任务(获取选中任务ID()))
        添加菜单项(右键菜单, "停止", Sub() 编码队列_v6.停止任务(获取选中任务ID()))
        添加菜单项(右键菜单, "重置", Sub() 编码队列_v6.重置任务(获取选中任务ID()))
        添加菜单项(右键菜单, "移除", Sub() 编码队列_v6.移除任务(获取选中任务ID()))
        添加菜单分割线(右键菜单)
        添加菜单项(右键菜单, "定位输出", AddressOf 定位输出)
        添加菜单项(右键菜单, "复制命令行", AddressOf 复制选中任务命令行)
        添加菜单项(右键菜单, "打开任务日志", AddressOf 打开任务日志)
        添加菜单项(右键菜单, "查看任务参数", AddressOf 查看任务参数)
    End Sub

    Private Sub 添加菜单项(menu As LakeUI.ModernContextMenu, text As String, action As Action)
        Dim item As New LakeUI.ModernContextMenu.ModernMenuItem(text)
        AddHandler item.Click, Sub() action()
        menu.Items.Add(item)
    End Sub

    Private Sub 添加菜单分割线(menu As LakeUI.ModernContextMenu)
        menu.Items.Add(New LakeUI.ModernContextMenu.ModernMenuItem() With {.IsSeparator = True})
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
        If UltraDetailListView1.Items.Count > 0 Then UltraDetailListView1.SelectedIndex = 0
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
        Dim target = If(task.状态 = 编码任务状态_v6.未处理, task.输入文件, task.输出文件)
        If File.Exists(target) Then Process.Start("explorer", "/select,""" & target & """")
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
            form.Show(Me)
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
        Else
            Form_v6_编码队列_任务日志.打开或激活(Me, New String() {id})
        End If
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
        初始化菜单()
        任务菜单.Show(ModernButton8, 0, ModernButton8.Height)
    End Sub

    Private Sub UltraDetailListView1_SizeChanged(sender As Object, e As EventArgs) Handles UltraDetailListView1.SizeChanged
        请求校准编码队列列宽()
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

        Dim 非任务列宽 = 获取非任务列理想宽度()
        Dim 非任务列最小宽度 = 获取非任务列最小宽度()
        Dim 任务名称列最小宽度 = 缩放宽度(任务名称列最小宽度基准, DPI)
        Dim 非任务列总宽度 = 非任务列宽.Sum()

        If 有效总宽度 - 非任务列总宽度 < 任务名称列最小宽度 Then
            Dim 非任务列目标宽度 = Math.Max(0, 有效总宽度 - 任务名称列最小宽度)
            非任务列宽 = 压缩列宽(非任务列宽, 非任务列最小宽度, 非任务列目标宽度)
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

    Private Function 获取非任务列理想宽度() As Integer()
        Dim result(非任务列理想宽度基准.Length - 1) As Integer
        For i = 0 To 非任务列理想宽度基准.Length - 1
            Dim 文本宽度 = Math.Max(
                测量列文本宽度(If(UltraDetailListView1.Columns(i + 1).Text, "")),
                测量列文本宽度(非任务列常用文本(i)))
            result(i) = Math.Max(缩放宽度(非任务列理想宽度基准(i), DPI), 文本宽度)
        Next
        Return result
    End Function

    Private Function 获取非任务列最小宽度() As Integer()
        Dim result(非任务列最小宽度基准.Length - 1) As Integer
        For i = 0 To 非任务列最小宽度基准.Length - 1
            result(i) = Math.Max(缩放宽度(非任务列最小宽度基准(i), DPI), 测量列文本宽度(If(UltraDetailListView1.Columns(i + 1).Text, "")))
        Next
        Return result
    End Function

    Private Function 测量列文本宽度(text As String) As Integer
        Return D3D_TextInterop.MeasureWidth(If(text, ""), UltraDetailListView1.Font, DPI) + 缩放宽度(列宽文本预留宽度基准, DPI)
    End Function

    Private Shared Function 压缩列宽(理想宽度 As Integer(), 最小宽度 As Integer(), 目标总宽度 As Integer) As Integer()
        Dim result = DirectCast(理想宽度.Clone(), Integer())
        Dim 理想总宽度 = result.Sum()
        Dim 最小总宽度 = 最小宽度.Sum()

        If 目标总宽度 >= 理想总宽度 Then Return result
        If 目标总宽度 <= 最小总宽度 Then Return DirectCast(最小宽度.Clone(), Integer())

        Dim 需要压缩宽度 = 理想总宽度 - 目标总宽度
        Dim 可压缩宽度 = 理想总宽度 - 最小总宽度
        For i = 0 To result.Length - 1
            Dim 当前可压缩宽度 = result(i) - 最小宽度(i)
            result(i) -= CInt(Math.Floor(当前可压缩宽度 * 需要压缩宽度 / 可压缩宽度))
        Next

        Dim 当前总宽度 = result.Sum()
        Dim index = result.Length - 1
        While 当前总宽度 > 目标总宽度
            If result(index) > 最小宽度(index) Then
                result(index) -= 1
                当前总宽度 -= 1
            End If
            index = If(index = 0, result.Length - 1, index - 1)
        End While

        Return result
    End Function

    Private Shared Function 缩放宽度(value As Integer, dpi As Single) As Integer
        Return Math.Max(1, CInt(Math.Round(value * dpi)))
    End Function

    Private Sub 设置列宽(columnIndex As Integer, width As Integer)
        If UltraDetailListView1.Columns(columnIndex).Width <> width Then UltraDetailListView1.Columns(columnIndex).Width = width
    End Sub
End Class
