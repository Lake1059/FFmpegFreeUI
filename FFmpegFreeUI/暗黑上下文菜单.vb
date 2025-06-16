Imports System.ComponentModel
Imports System.ComponentModel.Design

<ToolboxItem(True)>
<Designer("System.Windows.Forms.Design.ComponentDesigner, System.Design", GetType(IDesigner))>
Public Class 暗黑上下文菜单
    Inherits ContextMenuStrip

    Public Sub New()
        Renderer = New 暗黑上下文菜单自绘制()
    End Sub




End Class
