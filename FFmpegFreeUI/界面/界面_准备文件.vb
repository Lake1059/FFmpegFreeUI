Public Class 界面_准备文件
    Private Sub 界面_准备文件_Load(sender As Object, e As EventArgs) Handles Me.Load
        暗黑列表视图自绘制.绑定列表视图事件(Me.ListView1)
        Me.ListView1.SmallImageList = Form1.ImageList1
        AddHandler Me.UiButton14.Click, AddressOf 界面控制_添加文件.加入编码队列
        AddHandler Me.ListView1.DragEnter, AddressOf 界面控制_添加文件.ListView2_DragEnter
        AddHandler Me.ListView1.DragDrop, AddressOf 界面控制_添加文件.ListView2_DragDrop
        AddHandler Me.UiButton18.Click, AddressOf 界面控制_添加文件.添加文件
        AddHandler Me.UiButton10.Click, AddressOf 界面控制_添加文件.打开文件夹添加全部文件
        AddHandler Me.UiButton11.Click, AddressOf 界面控制_添加文件.批量移除选中项
        AddHandler Me.UiButton12.Click, AddressOf 界面控制_添加文件.移除全部项

        快速移除菜单.Items.AddRange(New ToolStripItem() {
                              New ToolStripSeparator() With {.Tag = "null"},
                              New ToolStripMenuItem("移除常见的非媒体文件", Nothing, AddressOf 移除常见的非媒体文件),
                              New ToolStripSeparator() With {.Tag = "null"}})
        AddHandler Me.UiButton快速移除.Click, Sub()
                                              快速移除菜单.Font = Me.Font
                                              快速移除菜单.Show(Me.UiButton快速移除, New Point(0, Me.UiButton快速移除.Height - 2))
                                          End Sub
        排序菜单.Items.AddRange(New ToolStripItem() {
                      New ToolStripSeparator() With {.Tag = "null"},
                      New ToolStripMenuItem("列表视图的升序", Nothing, AddressOf 列表视图的升序),
                      New ToolStripMenuItem("列表视图的降序", Nothing, AddressOf 列表视图的降序),
                      New ToolStripMenuItem("按照文件大小升序", Nothing, AddressOf 列表视图的升序),
                      New ToolStripMenuItem("按照文件大小降序", Nothing, AddressOf 列表视图的降序),
                      New ToolStripSeparator() With {.Tag = "null"}})
        AddHandler Me.UiButton排序.Click, Sub()
                                            排序菜单.Font = Me.Font
                                            排序菜单.Show(Me.UiButton排序, New Point(0, Me.UiButton排序.Height - 2))
                                        End Sub
    End Sub

    Public Sub 界面校准()
        Dim 总列宽 = Me.ListView1.Width - SystemInformation.VerticalScrollBarWidth * Form1.DPI * 2
        Me.ListView1.Columns(0).Width = 总列宽 * 0.8
        Me.ListView1.Columns(1).Width = 总列宽 * 0.2
    End Sub

    Public Shared ReadOnly 快速移除菜单 As New 暗黑上下文菜单 With {.ShowImageMargin = False, .Font = Form1.Font}

    Sub 移除常见的非媒体文件()
        For i = Me.ListView1.Items.Count - 1 To 0 Step -1
            Dim ext = IO.Path.GetExtension(Me.ListView1.Items(i).Text).ToLower
            Select Case ext
                Case ".txt", ".json", ".xml", ".csv", ".log", ".ini", ".zip", ".rar", ".7z", ".iso", ".dat"
                    Me.ListView1.Items.RemoveAt(i)
            End Select
        Next
    End Sub

    Public Shared ReadOnly 排序菜单 As New 暗黑上下文菜单 With {.ShowImageMargin = False, .Font = Form1.Font}

    Sub 列表视图的升序()
        Dim 项目列表 As New List(Of ListViewItem)
        For Each item As ListViewItem In Me.ListView1.Items
            项目列表.Add(item)
        Next
        项目列表 = 项目列表.OrderBy(Function(x) x.Text).ToList()
        Me.ListView1.Items.Clear()
        For Each item In 项目列表
            Me.ListView1.Items.Add(item)
        Next
    End Sub

    Sub 列表视图的降序()
        Dim 项目列表 As New List(Of ListViewItem)
        For Each item As ListViewItem In Me.ListView1.Items
            项目列表.Add(item)
        Next
        项目列表 = 项目列表.OrderByDescending(Function(x) x.Text).ToList()
        Me.ListView1.Items.Clear()
        For Each item In 项目列表
            Me.ListView1.Items.Add(item)
        Next
    End Sub

    Private Shared Function 获取文件大小字节(路径 As String) As Long
        Try
            Return New IO.FileInfo(路径).Length
        Catch
            Return -1
        End Try
    End Function
    Private Sub 确保大小列(ByVal item As ListViewItem, ByVal 大小 As Long)
        If item.SubItems.Count < 2 Then item.SubItems.Add("")
        If 大小 >= 0 Then
            item.SubItems(1).Text = Format(大小 / 1024 / 1024, "0") & " MB"
        Else
            item.SubItems(1).Text = "未知"
        End If
    End Sub
    Sub 按照文件大小升序()
        Dim 排序结果 = Me.ListView1.Items.Cast(Of ListViewItem)() _
            .Select(Function(it)
                        Dim len = 获取文件大小字节(it.Text)
                        确保大小列(it, len)
                        Return New With {.Item = it, .Key = If(len >= 0, len, Long.MaxValue)}
                    End Function) _
            .OrderBy(Function(x) x.Key) _
            .ThenBy(Function(x) x.Item.Text) _
            .ToList()
        Me.ListView1.BeginUpdate()
        Me.ListView1.Items.Clear()
        For Each x In 排序结果
            Me.ListView1.Items.Add(x.Item)
        Next
        Me.ListView1.EndUpdate()
    End Sub

    Sub 按照文件大小降序()
        Dim 排序结果 = Me.ListView1.Items.Cast(Of ListViewItem)() _
            .Select(Function(it)
                        Dim len = 获取文件大小字节(it.Text)
                        确保大小列(it, len)
                        Return New With {.Item = it, .Key = If(len >= 0, len, Long.MinValue)}
                    End Function) _
            .OrderByDescending(Function(x) x.Key) _
            .ThenBy(Function(x) x.Item.Text) _
            .ToList()
        Me.ListView1.BeginUpdate()
        Me.ListView1.Items.Clear()
        For Each x In 排序结果
            Me.ListView1.Items.Add(x.Item)
        Next
        Me.ListView1.EndUpdate()
    End Sub
End Class
