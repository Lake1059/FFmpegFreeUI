Public Class 界面_准备文件
    Private Sub 界面_准备文件_Load(sender As Object, e As EventArgs) Handles Me.Load
        暗黑列表视图自绘制.绑定列表视图事件(Me.ListView2)
        AddHandler Me.UiButton14.Click, AddressOf 界面控制_添加文件.加入编码队列
        AddHandler Me.ListView2.DragEnter, AddressOf 界面控制_添加文件.ListView2_DragEnter
        AddHandler Me.ListView2.DragDrop, AddressOf 界面控制_添加文件.ListView2_DragDrop
        AddHandler Me.UiButton18.Click, AddressOf 界面控制_添加文件.添加文件
        AddHandler Me.UiButton10.Click, AddressOf 界面控制_添加文件.打开文件夹添加全部文件
        AddHandler Me.UiButton11.Click, AddressOf 界面控制_添加文件.批量移除选中项
        AddHandler Me.UiButton12.Click, AddressOf 界面控制_添加文件.移除全部项


    End Sub

    Public Sub 界面校准()
        Me.ListView2.Columns(0).Width = Me.ListView2.Width - SystemInformation.VerticalScrollBarWidth * Form1.DPI * 2
    End Sub


End Class
