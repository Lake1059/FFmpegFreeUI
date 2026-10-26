Imports LakeUI

Public Class Form_v6_Agent
    Public Shared Event 正在推理任务数量已变化()

    Private _controller As Agent页面控制器_v6

    Private ReadOnly Property AgentController As Agent页面控制器_v6
        Get
            ' 控件初始化期间也可能触发事件，按需创建并始终复用同一控制器。
            If _controller Is Nothing Then _controller = New Agent页面控制器_v6(Me)
            Return _controller
        End Get
    End Property

    Private Sub Form_v6_Agent_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AgentController.Form_v6_Agent_Load(sender, e)
    End Sub

    Private Sub ModernListBox1_ItemForeColorNeeded(sender As Object, e As ModernListBox.ItemForeColorEventArgs) Handles ModernListBox1.ItemForeColorNeeded
        AgentController.ModernListBox1_ItemForeColorNeeded(sender, e)
    End Sub

    Private Sub Form_v6_Agent_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        AgentController.Form_v6_Agent_FormClosing(sender, e)
    End Sub

    Private Sub Form_v6_Agent_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _controller?.Form_v6_Agent_Disposed(sender, e)
    End Sub

    Private Sub MB_新对话_Click(sender As Object, e As EventArgs) Handles MB_新对话.Click
        AgentController.MB_新对话_Click(sender, e)
    End Sub

    Private Sub MB_删除对话_Click(sender As Object, e As EventArgs) Handles MB_删除对话.Click
        AgentController.MB_删除对话_Click(sender, e)
    End Sub

    Private Async Sub MB_发送_Click(sender As Object, e As EventArgs) Handles MB_发送.Click
        Await AgentController.MB_发送_Click(sender, e)
    End Sub

    Private Async Sub ModernTextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles ModernTextBox1.KeyDown
        Await AgentController.ModernTextBox1_KeyDown(sender, e)
    End Sub

    Private Sub ModernListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ModernListBox1.SelectedIndexChanged
        AgentController.ModernListBox1_SelectedIndexChanged(sender, e)
    End Sub

    Private Sub ModernListBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles ModernListBox1.KeyDown
        AgentController.ModernListBox1_KeyDown(sender, e)
    End Sub

    Private Sub ModernListBox1_ItemOrderChanged(sender As Object, e As EventArgs) Handles ModernListBox1.ItemOrderChanged
        AgentController.ModernListBox1_ItemOrderChanged(sender, e)
    End Sub

    Private Sub MB_操作提示_Click(sender As Object, e As EventArgs) Handles MB_操作提示.Click
        AgentController.MB_操作提示_Click(sender, e)
    End Sub

    Private Sub AgentButtonLayoutChanged(sender As Object, e As EventArgs) Handles Panel4.SizeChanged, Panel1.SizeChanged, JustEmptyControl5.SizeChanged, JustEmptyControl6.SizeChanged
        AgentController.AgentButtonLayoutChanged(sender, e)
    End Sub

    Private Sub AgentRoom1_LinkClicked(sender As Object, e As AgentRoom.LinkClickedEventArgs) Handles AgentRoom1.LinkClicked
        AgentController.AgentRoom1_LinkClicked(sender, e)
    End Sub

    Private Sub MB_页面用量_Click(sender As Object, e As EventArgs) Handles MB_页面用量.Click
        AgentController.MB_页面用量_Click(sender, e)
    End Sub

    Public Async Sub 请求刷新模型列表()
        Await AgentController.请求刷新模型列表()
    End Sub

    Public Async Sub 检查并刷新模型列表()
        Await AgentController.检查并刷新模型列表()
    End Sub

    Public Function 获取正在推理任务数量() As Integer
        Return AgentController.获取正在推理任务数量()
    End Function

    Friend Sub 通知正在推理任务数量已变化()
        RaiseEvent 正在推理任务数量已变化()
    End Sub

    Private Sub MCB_模型选择_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_模型选择.SelectedIndexChanged
        AgentController.MCB_模型选择_SelectedIndexChanged(sender, e)
    End Sub

    Private Sub MCB_推理级别_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_推理级别.SelectedIndexChanged
        AgentController.MCB_推理级别_SelectedIndexChanged(sender, e)
    End Sub

    Private Sub MCB_联网设置_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_联网设置.SelectedIndexChanged
        AgentController.MCB_联网设置_SelectedIndexChanged(sender, e)
    End Sub

    Private Sub MCB_权限控制_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_权限控制.SelectedIndexChanged
        AgentController.MCB_权限控制_SelectedIndexChanged(sender, e)
    End Sub

    Private Async Sub MB_重载连接_Click(sender As Object, e As EventArgs) Handles MB_重载连接.Click
        Await AgentController.MB_重载连接_Click(sender, e)
    End Sub

    Private Sub ModernTextBox1_TextChanged(sender As Object, e As EventArgs) Handles ModernTextBox1.TextChanged
        AgentController.ModernTextBox1_TextChanged(sender, e)
    End Sub

    Private Sub ModernListBox2_DragEnter(sender As Object, e As DragEventArgs) Handles ModernListBox2.DragEnter
        AgentController.ModernListBox2_DragEnter(sender, e)
    End Sub

    Private Sub ModernListBox2_DragDrop(sender As Object, e As DragEventArgs) Handles ModernListBox2.DragDrop
        AgentController.ModernListBox2_DragDrop(sender, e)
    End Sub

    Private Sub ModernListBox2_MouseUp(sender As Object, e As MouseEventArgs) Handles ModernListBox2.MouseUp
        AgentController.ModernListBox2_MouseUp(sender, e)
    End Sub

    Private Sub ModernListBox2_ItemDoubleClick(sender As Object, e As LakeUI.ModernListBox.ItemEventArgs) Handles ModernListBox2.ItemDoubleClick
        AgentController.ModernListBox2_ItemDoubleClick(sender, e)
    End Sub

    Private Sub ModernListBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles ModernListBox2.KeyDown
        AgentController.ModernListBox2_KeyDown(sender, e)
    End Sub

    Private Sub ModernListBox2_ItemOrderChanged(sender As Object, e As EventArgs) Handles ModernListBox2.ItemOrderChanged
        AgentController.ModernListBox2_ItemOrderChanged(sender, e)
    End Sub
End Class
