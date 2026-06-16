Imports LakeUI

Public Class Form_v6_参数面板_滤镜排序

    Private ReadOnly _items As New List(Of 预设数据_v6.滤镜排序单片结构)
    Public 所属参数面板对象 As Form_v6_参数面板

    Private Sub Form_v6_参数面板_滤镜排序_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If UltraDetailListView1.Columns.Count = 3 Then
            UltraDetailListView1.Columns(0).AllowLabelEdit = False
            UltraDetailListView1.Columns(1).AllowLabelEdit = False
            UltraDetailListView1.Columns(2).AllowLabelEdit = True
        End If
        HtmlColorLabel1.Text = "<span style=""font-size:13; color:Silver"">滤镜排序和自定义</span>   每一项的滤镜链片段显示在列表的滤镜内容列，自定义项可直接编辑"
        RefreshList()
    End Sub

    Public Sub 设置排序数据(items As IEnumerable(Of 预设数据_v6.滤镜排序单片结构), Optional refreshUi As Boolean = True)
        _items.Clear()
        If items IsNot Nothing Then
            For Each item In items
                _items.Add(CloneItem(item))
            Next
        End If
        If refreshUi Then RefreshList()
    End Sub

    Public Function 获取排序数据() As List(Of 预设数据_v6.滤镜排序单片结构)
        Return _items.Select(Function(x) CloneItem(x)).ToList()
    End Function

    Public Sub 添加或更新内置滤镜(id As 预设数据_v6.滤镜排序单片结构.标识符枚举,
                               target As 预设数据_v6.滤镜排序单片结构.流类型,
                               name As String,
                               content As String)
        Dim item = _items.FirstOrDefault(Function(x) Not x.是自定义滤镜 AndAlso x.滤镜标识符 = id)
        If item Is Nothing Then
            item = New 预设数据_v6.滤镜排序单片结构 With {.滤镜标识符 = id, .滤镜目标流类型 = target, .显示名称 = name}
            _items.Add(item)
        End If
        item.滤镜目标流类型 = target
        item.显示名称 = name
        item.自定义滤镜内容 = content
        item.是自定义滤镜 = False
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
            .自定义滤镜内容 = ""
        })
        RefreshList()
        通知参数面板刷新()
    End Sub

    Public Sub 删除所选()
        If UltraDetailListView1.SelectedItem Is Nothing Then Exit Sub
        Dim item = TryCast(UltraDetailListView1.SelectedItem.Tag, 预设数据_v6.滤镜排序单片结构)
        If item Is Nothing Then Exit Sub
        Dim 是内置项 = Not item.是自定义滤镜
        Dim 标识符 = item.滤镜标识符
        _items.Remove(item)
        RefreshList()
        If 是内置项 AndAlso 所属参数面板对象 IsNot Nothing Then
            预设管理_v6.重置滤镜对应页面(所属参数面板对象, 标识符)
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
        HtmlColorLabel1.Text = "<span style=""font-size:13; color:Silver"">滤镜排序和自定义</span>   每一项的滤镜链片段显示在列表的滤镜内容列，自定义项可直接编辑"
        RefreshList()
    End Sub

    Public Sub 同步到参数总览(a As 预设数据_v6)
        If a Is Nothing Then Exit Sub
        a.滤镜排序系统 = 获取排序数据().ToArray()
        刷新局部预览(a)
    End Sub

    Private Sub RefreshList()
        UltraDetailListView1.Items.Clear()
        For Each item In _items
            Dim lv As New UltraDetailListView.ListItem()
            lv.SubItems.Add(New UltraDetailListView.ListSubItem With {.Text = If(item.显示名称 <> "", item.显示名称, item.滤镜标识符.ToString())})
            lv.SubItems.Add(New UltraDetailListView.ListSubItem With {.Text = item.滤镜目标流类型.ToString()})
            lv.SubItems.Add(New UltraDetailListView.ListSubItem With {.Text = item.自定义滤镜内容})
            lv.Tag = item
            If item.是自定义滤镜 Then lv.GroupName = "custom"
            UltraDetailListView1.Items.Add(lv)
        Next
    End Sub

    Private Function CloneItem(src As 预设数据_v6.滤镜排序单片结构) As 预设数据_v6.滤镜排序单片结构
        If src Is Nothing Then Return Nothing
        Return New 预设数据_v6.滤镜排序单片结构 With {
            .实例ID = If(src.实例ID, Guid.NewGuid().ToString("N")),
            .显示名称 = src.显示名称,
            .是自定义滤镜 = src.是自定义滤镜,
            .滤镜标识符 = src.滤镜标识符,
            .滤镜目标流类型 = src.滤镜目标流类型,
            .自定义滤镜内容 = src.自定义滤镜内容
        }
    End Function

    Private Sub UltraDetailListView1_ItemOrderChanged(sender As Object, e As EventArgs) Handles UltraDetailListView1.ItemOrderChanged
        Dim reordered As New List(Of 预设数据_v6.滤镜排序单片结构)
        For Each uiItem In UltraDetailListView1.Items
            Dim item = TryCast(uiItem.Tag, 预设数据_v6.滤镜排序单片结构)
            If item IsNot Nothing Then reordered.Add(item)
        Next
        _items.Clear()
        _items.AddRange(reordered)
        通知参数面板刷新()
    End Sub

    Private Sub UltraDetailListView1_AfterLabelEdit(sender As Object, e As UltraDetailListView.LabelEditEventArgs) Handles UltraDetailListView1.AfterLabelEdit
        Dim item = TryCast(e.Item.Tag, 预设数据_v6.滤镜排序单片结构)
        If item Is Nothing Then Exit Sub
        If e.ColumnIndex = 2 Then
            If Not item.是自定义滤镜 Then
                e.CancelEdit = True
                Exit Sub
            End If
            item.自定义滤镜内容 = e.Label.Trim()
            通知参数面板刷新()
        End If
    End Sub

    Private Sub UltraDetailListView1_ItemDoubleClick(sender As Object, e As UltraDetailListView.ListItemEventArgs) Handles UltraDetailListView1.ItemDoubleClick
        Dim item = TryCast(e.Item.Tag, 预设数据_v6.滤镜排序单片结构)
        If item Is Nothing Then Exit Sub
        If item.是自定义滤镜 Then
            item.自定义滤镜内容 = If(item.自定义滤镜内容 = "", "eq=contrast=1.0", item.自定义滤镜内容)
            RefreshList()
            通知参数面板刷新()
        End If
    End Sub

    Private Sub ModernComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ModernComboBox1.SelectedIndexChanged
        If ModernComboBox1.SelectedIndex = 0 Then
            添加自定义滤镜(预设数据_v6.滤镜排序单片结构.流类型.视频)
        ElseIf ModernComboBox1.SelectedIndex = 1 Then
            添加自定义滤镜(预设数据_v6.滤镜排序单片结构.流类型.音频)
        End If
        ModernComboBox1.SelectedIndex = -1
    End Sub

    Private Sub MB_删除滤镜_Click(sender As Object, e As EventArgs) Handles MB_删除滤镜.Click
        删除所选()
    End Sub

    Private Sub 通知参数面板刷新()
        If 所属参数面板对象 Is Nothing OrElse 所属参数面板对象.抑制自动刷新 Then Exit Sub
        预设管理_v6.刷新参数总览(所属参数面板对象)
    End Sub

End Class
