Imports LakeUI

Public Class Form_v6_参数面板_滤镜排序

    Private ReadOnly _items As New List(Of 预设数据_v6.滤镜排序单片结构)
    Private _isRefreshingList As Boolean
    Public 所属参数面板对象 As Form_v6_参数面板
    Private Shared ReadOnly 内置滤镜优先级表 As New Dictionary(Of 预设数据_v6.滤镜排序单片结构.标识符枚举, Integer) From {
        {预设数据_v6.滤镜排序单片结构.标识符枚举.像素格式预先转换, 0},
        {预设数据_v6.滤镜排序单片结构.标识符枚举.裁剪, 100},
        {预设数据_v6.滤镜排序单片结构.标识符枚举.缩放, 110},
        {预设数据_v6.滤镜排序单片结构.标识符枚举.抽帧, 120},
        {预设数据_v6.滤镜排序单片结构.标识符枚举.插帧, 130},
        {预设数据_v6.滤镜排序单片结构.标识符枚举.动态模糊, 140},
        {预设数据_v6.滤镜排序单片结构.标识符枚举.超分, 150},
        {预设数据_v6.滤镜排序单片结构.标识符枚举.降噪, 160},
        {预设数据_v6.滤镜排序单片结构.标识符枚举.锐化, 170},
        {预设数据_v6.滤镜排序单片结构.标识符枚举.胶片颗粒, 180},
        {预设数据_v6.滤镜排序单片结构.标识符枚举.平滑断层, 190},
        {预设数据_v6.滤镜排序单片结构.标识符枚举.扫描方式, 200},
        {预设数据_v6.滤镜排序单片结构.标识符枚举.画面翻转, 210},
        {预设数据_v6.滤镜排序单片结构.标识符枚举.烧录字幕, 220},
        {预设数据_v6.滤镜排序单片结构.标识符枚举.色彩转换, 300},
        {预设数据_v6.滤镜排序单片结构.标识符枚举.调色, 310},
        {预设数据_v6.滤镜排序单片结构.标识符枚举.音频响度标准化, 1000},
        {预设数据_v6.滤镜排序单片结构.标识符枚举.音频格式转换, 1010},
        {预设数据_v6.滤镜排序单片结构.标识符枚举.音频重采样, 1020}
    }
    Private Shared ReadOnly 默认置顶内置滤镜 As New HashSet(Of 预设数据_v6.滤镜排序单片结构.标识符枚举) From {
        预设数据_v6.滤镜排序单片结构.标识符枚举.像素格式预先转换
    }

    Private Sub Form_v6_参数面板_滤镜排序_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UDLV_滤镜排序列表.MultiSelect = True
        调整列表交互区域()
        If UDLV_滤镜排序列表.Columns.Count = 3 Then
            UDLV_滤镜排序列表.Columns(0).AllowLabelEdit = False
            UDLV_滤镜排序列表.Columns(1).AllowLabelEdit = False
            UDLV_滤镜排序列表.Columns(2).AllowLabelEdit = False
        End If
        RefreshList()
    End Sub

    Public Sub 设置排序数据(items As IEnumerable(Of 预设数据_v6.滤镜排序单片结构), Optional refreshUi As Boolean = True)
        _items.Clear()
        If items IsNot Nothing Then
            For Each item In items
                _items.Add(CloneItem(item))
            Next
        End If
        规范化流类型顺序()
        If refreshUi Then RefreshList()
    End Sub

    Public Function 获取排序数据() As List(Of 预设数据_v6.滤镜排序单片结构)
        规范化流类型顺序()
        Return _items.Select(Function(x) CloneItem(x)).ToList()
    End Function

    Public Sub 添加或更新内置滤镜(id As 预设数据_v6.滤镜排序单片结构.标识符枚举,
                               target As 预设数据_v6.滤镜排序单片结构.流类型,
                               name As String,
                               content As String)
        Dim item = _items.FirstOrDefault(Function(x) Not x.是自定义滤镜 AndAlso x.滤镜标识符 = id)
        If item Is Nothing Then
            item = New 预设数据_v6.滤镜排序单片结构 With {.滤镜标识符 = id, .滤镜目标流类型 = target, .显示名称 = name}
            Dim insertIndex = 获取内置滤镜默认插入位置(item)
            If insertIndex >= 0 AndAlso insertIndex < _items.Count Then
                _items.Insert(insertIndex, item)
            Else
                _items.Add(item)
            End If
        End If
        item.滤镜目标流类型 = target
        item.显示名称 = name
        item.自定义滤镜内容 = content
        item.是自定义滤镜 = False
        规范化流类型顺序()
        RefreshList()
    End Sub

    Public Sub 移除内置滤镜(id As 预设数据_v6.滤镜排序单片结构.标识符枚举)
        _items.RemoveAll(Function(x) Not x.是自定义滤镜 AndAlso x.滤镜标识符 = id)
        RefreshList()
    End Sub

    Public Sub 添加自定义滤镜(target As 预设数据_v6.滤镜排序单片结构.流类型)
        _items.Add(New 预设数据_v6.滤镜排序单片结构 With {
            .滤镜标识符 = If(target = 预设数据_v6.滤镜排序单片结构.流类型.音频,
                             预设数据_v6.滤镜排序单片结构.标识符枚举.自定义音频滤镜,
                             预设数据_v6.滤镜排序单片结构.标识符枚举.自定义视频滤镜),
            .滤镜目标流类型 = target,
            .显示名称 = If(target = 预设数据_v6.滤镜排序单片结构.流类型.音频, "自定义音频滤镜", "自定义视频滤镜"),
            .是自定义滤镜 = True,
            .允许在排序页直接编辑 = True,
            .自定义滤镜内容 = ""
        })
        规范化流类型顺序()
        RefreshList()
        通知参数面板刷新()
    End Sub

    Public Sub 删除所选()
        Dim selectedItems = UDLV_滤镜排序列表.SelectedItems.Cast(Of UltraDetailListView.ListItem)().ToList()
        If selectedItems.Count = 0 Then Exit Sub

        Dim itemsToDelete = selectedItems.
            Select(Function(x) TryCast(x.Tag, 预设数据_v6.滤镜排序单片结构)).
            Where(Function(x) x IsNot Nothing).
            Distinct().
            ToList()
        If itemsToDelete.Count = 0 Then Exit Sub

        Dim resetIds = itemsToDelete.
            Where(Function(x) Not 可直接编辑滤镜内容(x)).
            Select(Function(x) x.滤镜标识符).
            Distinct().
            ToList()

        For Each item In itemsToDelete
            _items.Remove(item)
        Next
        RefreshList()

        If resetIds.Count > 0 AndAlso 所属参数面板对象 IsNot Nothing Then
            For Each 标识符 In resetIds
                预设管理_v6.重置滤镜对应页面(所属参数面板对象, 标识符, False)
            Next
            通知参数面板刷新()
        Else
            通知参数面板刷新()
        End If
    End Sub

    Public Sub 刷新局部预览(a As 预设数据_v6)
        If a IsNot Nothing Then
            For Each item In _items
                If Not item.是自定义滤镜 Then
                    item.自定义滤镜内容 = 预设管理_v6.获取滤镜片段(a, item)
                End If
            Next
        End If
        RefreshList()
    End Sub

    Public Sub 同步到参数总览(a As 预设数据_v6)
        If a Is Nothing Then Exit Sub
        a.滤镜排序系统 = 获取排序数据().ToArray()
        刷新局部预览(a)
    End Sub

    Private Sub RefreshList()
        规范化流类型顺序()
        _isRefreshingList = True
        UDLV_滤镜排序列表.BeginUpdate()
        Try
            UDLV_滤镜排序列表.Items.Clear()
            For Each item In _items
                Dim lv As New UltraDetailListView.ListItem()
                Dim 标识符项 As New UltraDetailListView.ListSubItem With {.Text = If(item.显示名称 <> "", item.显示名称, item.滤镜标识符.ToString())}
                If 可直接编辑滤镜内容(item) Then 标识符项.ForeColor = Color.Orange
                lv.SubItems.Add(标识符项)
                lv.SubItems.Add(New UltraDetailListView.ListSubItem With {.Text = item.滤镜目标流类型.ToString()})
                lv.SubItems.Add(New UltraDetailListView.ListSubItem With {.Text = item.自定义滤镜内容})
                lv.Tag = item
                UDLV_滤镜排序列表.Items.Add(lv)
            Next
        Finally
            UDLV_滤镜排序列表.EndUpdate()
            _isRefreshingList = False
        End Try
        校准列表列宽()
    End Sub

    Private Function CloneItem(src As 预设数据_v6.滤镜排序单片结构) As 预设数据_v6.滤镜排序单片结构
        If src Is Nothing Then Return Nothing
        Return New 预设数据_v6.滤镜排序单片结构 With {
            .实例ID = If(src.实例ID, Guid.NewGuid().ToString("N")),
            .显示名称 = src.显示名称,
            .是自定义滤镜 = src.是自定义滤镜,
            .允许在排序页直接编辑 = src.允许在排序页直接编辑,
            .滤镜标识符 = src.滤镜标识符,
            .滤镜目标流类型 = src.滤镜目标流类型,
            .自定义滤镜内容 = src.自定义滤镜内容
        }
    End Function

    Private Sub UltraDetailListView1_ItemOrderChanged(sender As Object, e As EventArgs) Handles UDLV_滤镜排序列表.ItemOrderChanged
        If _isRefreshingList Then Exit Sub
        同步列表项到排序数据()
        通知参数面板刷新()
    End Sub

    Private Sub 同步列表项到排序数据()
        Dim reordered As New List(Of 预设数据_v6.滤镜排序单片结构)
        For Each uiItem In UDLV_滤镜排序列表.Items
            Dim item = TryCast(uiItem.Tag, 预设数据_v6.滤镜排序单片结构)
            If item IsNot Nothing Then reordered.Add(item)
        Next
        _items.Clear()
        _items.AddRange(reordered)
        If 规范化流类型顺序() Then RefreshList()
    End Sub

    Private Sub 移动选中项(direction As Integer)
        Dim selected = UDLV_滤镜排序列表.SelectedItems
        If selected.Count = 0 OrElse direction = 0 Then Exit Sub

        Dim changed = False
        Dim selectedSet As New HashSet(Of UltraDetailListView.ListItem)(selected)
        UDLV_滤镜排序列表.BeginUpdate()
        Try
            If direction < 0 Then
                For i = 1 To UDLV_滤镜排序列表.Items.Count - 1
                    If selectedSet.Contains(UDLV_滤镜排序列表.Items(i)) AndAlso Not selectedSet.Contains(UDLV_滤镜排序列表.Items(i - 1)) Then
                        Dim moving = UDLV_滤镜排序列表.Items(i)
                        UDLV_滤镜排序列表.Items.RemoveAt(i)
                        UDLV_滤镜排序列表.Items.Insert(i - 1, moving)
                        changed = True
                    End If
                Next
            Else
                For i = UDLV_滤镜排序列表.Items.Count - 2 To 0 Step -1
                    If selectedSet.Contains(UDLV_滤镜排序列表.Items(i)) AndAlso Not selectedSet.Contains(UDLV_滤镜排序列表.Items(i + 1)) Then
                        Dim moving = UDLV_滤镜排序列表.Items(i)
                        UDLV_滤镜排序列表.Items.RemoveAt(i)
                        UDLV_滤镜排序列表.Items.Insert(i + 1, moving)
                        changed = True
                    End If
                Next
            End If
        Finally
            UDLV_滤镜排序列表.EndUpdate()
        End Try
        If Not changed Then Exit Sub

        同步列表项到排序数据()
        通知参数面板刷新()
    End Sub

    Private Sub UltraDetailListView1_KeyDown(sender As Object, e As KeyEventArgs) Handles UDLV_滤镜排序列表.KeyDown
        Select Case e.KeyCode
            Case Keys.F3
                移动选中项(-1)
                e.Handled = True
                e.SuppressKeyPress = True
            Case Keys.F4
                移动选中项(1)
                e.Handled = True
                e.SuppressKeyPress = True
            Case Keys.Delete
                删除所选()
                e.Handled = True
                e.SuppressKeyPress = True
        End Select
    End Sub

    Private Sub UltraDetailListView1_AfterLabelEdit(sender As Object, e As UltraDetailListView.LabelEditEventArgs) Handles UDLV_滤镜排序列表.AfterLabelEdit
        Dim item = TryCast(e.Item.Tag, 预设数据_v6.滤镜排序单片结构)
        If item Is Nothing Then Exit Sub
        If e.ColumnIndex = 2 Then
            If Not 可直接编辑滤镜内容(item) Then
                e.CancelEdit = True
                Exit Sub
            End If
            item.自定义滤镜内容 = If(e.Label, "").Trim()
            通知参数面板刷新()
        End If
    End Sub

    Private Sub UltraDetailListView1_ItemDoubleClick(sender As Object, e As UltraDetailListView.ListItemEventArgs) Handles UDLV_滤镜排序列表.ItemDoubleClick
        Dim item = TryCast(e.Item.Tag, 预设数据_v6.滤镜排序单片结构)
        If item Is Nothing Then Exit Sub
        If 可直接编辑滤镜内容(item) Then
            UDLV_滤镜排序列表.Columns(2).AllowLabelEdit = True
            Try
                UDLV_滤镜排序列表.开始标签编辑(e.DisplayRowIndex, 2)
            Finally
                UDLV_滤镜排序列表.Columns(2).AllowLabelEdit = False
            End Try
        End If
    End Sub

    Private Sub MB_删除滤镜_Click(sender As Object, e As EventArgs) Handles MB_删除滤镜.Click
        删除所选()
    End Sub

    Private Sub 通知参数面板刷新()
        If 所属参数面板对象 Is Nothing OrElse 所属参数面板对象.抑制自动刷新 Then Exit Sub
        所属参数面板对象.请求刷新参数状态()
    End Sub

    Private Sub Form_v6_参数面板_滤镜排序_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
        校准列表列宽()
    End Sub

    Private Sub UltraDetailListView1_SizeChanged(sender As Object, e As EventArgs) Handles UDLV_滤镜排序列表.SizeChanged
        调整列表交互区域()
        校准列表列宽()
    End Sub

    Private Function 可直接编辑滤镜内容(item As 预设数据_v6.滤镜排序单片结构) As Boolean
        Return item IsNot Nothing AndAlso item.是自定义滤镜 AndAlso item.允许在排序页直接编辑
    End Function

    Private Function 获取内置滤镜默认插入位置(newItem As 预设数据_v6.滤镜排序单片结构) As Integer
        If 默认置顶内置滤镜.Contains(newItem.滤镜标识符) Then Return 0

        Dim newPriority As Integer
        If Not 内置滤镜优先级表.TryGetValue(newItem.滤镜标识符, newPriority) Then Return _items.Count

        Dim lastSameTypeIndex = -1
        For i = 0 To _items.Count - 1
            Dim current = _items(i)
            If current Is Nothing Then Continue For
            If current.滤镜目标流类型 <> newItem.滤镜目标流类型 Then Continue For
            lastSameTypeIndex = i
            Dim currentPriority As Integer
            If Not 内置滤镜优先级表.TryGetValue(current.滤镜标识符, currentPriority) Then Continue For
            If currentPriority > newPriority Then Return i
        Next
        If lastSameTypeIndex >= 0 Then Return lastSameTypeIndex + 1
        Return _items.Count
    End Function

    Private Function 规范化流类型顺序() As Boolean
        Dim normalized = _items.
            Where(Function(x) x IsNot Nothing).
            OrderBy(Function(x) 获取流类型排序权重(x)).
            ToList()

        Dim changed = normalized.Count <> _items.Count
        If Not changed Then
            For i = 0 To _items.Count - 1
                If Not Object.ReferenceEquals(_items(i), normalized(i)) Then
                    changed = True
                    Exit For
                End If
            Next
        End If
        If Not changed Then Return False

        _items.Clear()
        _items.AddRange(normalized)
        Return True
    End Function

    Private Shared Function 获取流类型排序权重(item As 预设数据_v6.滤镜排序单片结构) As Integer
        If item IsNot Nothing AndAlso item.滤镜目标流类型 = 预设数据_v6.滤镜排序单片结构.流类型.音频 Then Return 1
        Return 0
    End Function

    Private Sub 调整列表交互区域()
        If UDLV_滤镜排序列表 Is Nothing Then Exit Sub
        UDLV_滤镜排序列表.DragSelectZoneWidth = Math.Max(0, UDLV_滤镜排序列表.Size.Width \ 2)
    End Sub

    Private Sub 校准列表列宽()
        If UDLV_滤镜排序列表 Is Nothing OrElse UDLV_滤镜排序列表.Columns.Count < 3 Then Exit Sub

        Dim 可用宽度 = UDLV_滤镜排序列表.ClientSize.Width
        If 可用宽度 <= 0 Then 可用宽度 = UDLV_滤镜排序列表.Width
        If 可用宽度 <= 0 Then Exit Sub

        可用宽度 = Math.Max(0, 可用宽度 - CInt(24 * DeviceDpi / 96.0))
        Dim 标识符列宽 = Math.Min(CInt(220 * DeviceDpi / 96.0), Math.Max(CInt(130 * DeviceDpi / 96.0), CInt(可用宽度 * 0.22)))
        Dim 流类型列宽 = Math.Min(CInt(120 * DeviceDpi / 96.0), Math.Max(CInt(80 * DeviceDpi / 96.0), CInt(可用宽度 * 0.12)))
        Dim 内容列宽 = Math.Max(CInt(240 * DeviceDpi / 96.0), 可用宽度 - 标识符列宽 - 流类型列宽)

        UDLV_滤镜排序列表.Columns(0).Width = 标识符列宽
        UDLV_滤镜排序列表.Columns(1).Width = 流类型列宽
        UDLV_滤镜排序列表.Columns(2).Width = 内容列宽
    End Sub

    Private Sub MB_添加自定义视频滤镜_Click(sender As Object, e As EventArgs) Handles MB_添加自定义视频滤镜.Click
        添加自定义滤镜(预设数据_v6.滤镜排序单片结构.流类型.视频)
    End Sub

    Private Sub MB_添加自定义音频滤镜_Click(sender As Object, e As EventArgs) Handles MB_添加自定义音频滤镜.Click
        添加自定义滤镜(预设数据_v6.滤镜排序单片结构.流类型.音频)
    End Sub
End Class
