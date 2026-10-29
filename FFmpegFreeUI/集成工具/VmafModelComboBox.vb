Imports System.Reflection
Imports LakeUI

Public NotInheritable Class VmafModelDisplayItem
    Public Const NonNegHeaderText As String = "下列模型是非neg模型，强烈不推荐"

    Public Property ModelValue As String = ""
    Public Property VersionLabel As String = ""
    Public Property Is4K As Boolean = False
    Public Property ViewingDistanceLabel As String = ""
    Public Property IsHfr As Boolean = False
    Public Property IsHdr As Boolean = False
    ' 运行库中检测到模型名称，不等于已经验证完整评分链；官方目录补充项为 False。
    Public Property IsRuntimeDetected As Boolean = True
    Public Property UseCuda As Boolean = False
    Public Property IsLocal As Boolean = False
    Public Property IsAuto As Boolean = False
    Public Property IsNonNeg As Boolean = False
    Public Property IsSectionHeader As Boolean = False
    Public Property IsCudaAvailable As Boolean = True

    Public ReadOnly Property DisplayText As String
        Get
            If IsSectionHeader Then Return NonNegHeaderText
            If IsAuto Then Return "AUTO"
            If IsLocal Then
                Dim fileName = System.IO.Path.GetFileName(ModelValue)
                Return If(String.IsNullOrWhiteSpace(fileName), "本地模型", $"本地 · {fileName}")
            End If

            Dim parts As New List(Of String)()
            If Not String.IsNullOrWhiteSpace(VersionLabel) Then parts.Add(VersionLabel)
            If Is4K Then parts.Add("4K")
            If IsNonNeg Then
                parts.Add("非neg模型")
            Else
                If Not String.IsNullOrWhiteSpace(ViewingDistanceLabel) Then parts.Add(ViewingDistanceLabel)
                If IsHfr Then parts.Add("HFR")
                If IsHdr Then parts.Add("HDR")
            End If
            If UseCuda Then parts.Add("CUDA")
            Return String.Join(" · ", parts)
        End Get
    End Property
End Class

''' <summary>
''' VMAF 专用 ModernComboBox。下拉弹层、滚动、悬停、选中状态和输入处理复用 LakeUI，
''' 本类负责模型胶囊、本地模型入口及提示窗固定宽度适配。
''' </summary>
Public Class VmafModelComboBox
    Inherits ModernComboBox

    Private Const 浏览本地模型文本 As String = "浏览本地模型文件 …"
    Private Const 胶囊间距逻辑值 As Single = 6.0F
    Private Const 版本胶囊宽度逻辑值 As Single = 46.0F
    Private Const 分辨率胶囊宽度逻辑值 As Single = 42.0F
    Private Const 观看距离胶囊宽度逻辑值 As Single = 52.0F
    Private Const Hfr胶囊宽度逻辑值 As Single = 48.0F
    Private Const Hdr胶囊宽度逻辑值 As Single = 48.0F
    Private Const Cuda胶囊宽度逻辑值 As Single = 58.0F
    Private Const 胶囊高度逻辑值 As Single = 22.0F
    Private Const 非Neg胶囊宽度逻辑值 As Single = 观看距离胶囊宽度逻辑值 + Hfr胶囊宽度逻辑值 + Hdr胶囊宽度逻辑值 + 胶囊间距逻辑值 * 2

    Private Enum 胶囊类别
        版本
        旧版
        分辨率
        观看距离
        Hfr
        Hdr
        Cuda
        非Neg
        观看距离近
        观看距离远
    End Enum

    Private Shared ReadOnly 浅色胶囊配色 As (背景 As Color, 文字 As Color)() = {
        (ColorTranslator.FromHtml("#DCEBFA"), ColorTranslator.FromHtml("#27598A")),
        (ColorTranslator.FromHtml("#E4E7EB"), ColorTranslator.FromHtml("#536174")),
        (ColorTranslator.FromHtml("#D9EFEC"), ColorTranslator.FromHtml("#22665F")),
        (ColorTranslator.FromHtml("#E5D5F2"), ColorTranslator.FromHtml("#61417D")),
        (ColorTranslator.FromHtml("#FBEACD"), ColorTranslator.FromHtml("#805815")),
        (ColorTranslator.FromHtml("#F7DEEB"), ColorTranslator.FromHtml("#8C3E65")),
        (ColorTranslator.FromHtml("#E3EFD9"), ColorTranslator.FromHtml("#426B28")),
        (ColorTranslator.FromHtml("#F9E0DA"), ColorTranslator.FromHtml("#924333")),
        (ColorTranslator.FromHtml("#DCC6EE"), ColorTranslator.FromHtml("#50306F")),
        (ColorTranslator.FromHtml("#F2E9FA"), ColorTranslator.FromHtml("#6D5285"))
    }
    Private Shared ReadOnly 深色胶囊配色 As (背景 As Color, 文字 As Color)() = {
        (ColorTranslator.FromHtml("#263C55"), ColorTranslator.FromHtml("#C7DEFF")),
        (ColorTranslator.FromHtml("#3A414B"), ColorTranslator.FromHtml("#DEE3EB")),
        (ColorTranslator.FromHtml("#25473F"), ColorTranslator.FromHtml("#BDE8DE")),
        (ColorTranslator.FromHtml("#5A436D"), ColorTranslator.FromHtml("#F0DFFD")),
        (ColorTranslator.FromHtml("#503D22"), ColorTranslator.FromHtml("#F5D69B")),
        (ColorTranslator.FromHtml("#503043"), ColorTranslator.FromHtml("#F3C9DF")),
        (ColorTranslator.FromHtml("#344A2A"), ColorTranslator.FromHtml("#D2E9BE")),
        (ColorTranslator.FromHtml("#56362F"), ColorTranslator.FromHtml("#FFD6C9")),
        (ColorTranslator.FromHtml("#4B375A"), ColorTranslator.FromHtml("#E7D2FA")),
        (ColorTranslator.FromHtml("#755C89"), ColorTranslator.FromHtml("#F9EEFF"))
    }

    Public Event BrowseRequested As EventHandler

    Private ReadOnly _displayItems As New List(Of VmafModelDisplayItem)()
    Private _updatingItems As Boolean = False
    Private _handlingBrowse As Boolean = False
    Private _lastModelIndex As Integer = -1
    Private _正在定位提示 As Boolean
    Private Shared ReadOnly 下拉窗体字段 As FieldInfo = GetType(ModernComboBox).GetField("_dropDownForm", BindingFlags.Instance Or BindingFlags.NonPublic)
    Private Shared ReadOnly 提示窗体字段 As FieldInfo = 下拉窗体字段?.FieldType.GetField("_tipForm", BindingFlags.Instance Or BindingFlags.NonPublic)

    Public ReadOnly Property Models As IReadOnlyList(Of VmafModelDisplayItem)
        Get
            Return _displayItems.AsReadOnly()
        End Get
    End Property

    Public Sub New()
        MyBase.New()
        AddHandler SelectedIndexChanged, AddressOf 处理模型选择变化
        AddHandler TextChanged, AddressOf 记录有效模型选择
        AddHandler DropDownOpened, AddressOf 准备模型提示
    End Sub

    Private Sub 准备模型提示(sender As Object, e As EventArgs)
        Dim popup = TryCast(下拉窗体字段?.GetValue(Me), Form)
        If popup Is Nothing OrElse 提示窗体字段 Is Nothing Then Return
        AddHandler popup.MouseMove, Sub(s, args) 准备固定宽度提示(popup)
        准备固定宽度提示(popup)
    End Sub

    Private Sub 准备固定宽度提示(popup As Form)
        If popup.IsDisposed Then Return
        Dim tip = TryCast(提示窗体字段.GetValue(popup), FloatingToolTipForm)
        If tip Is Nothing OrElse tip.IsDisposed Then
            ' LakeUI 5.116 没有固定宽度选项，复用原提示窗，仅预设其尺寸约束。
            tip = New FloatingToolTipForm(Me)
            提示窗体字段.SetValue(popup, tip)
            AddHandler tip.LocationChanged, Sub(s, args) 定位模型提示(tip, popup)
            AddHandler tip.SizeChanged, Sub(s, args) 定位模型提示(tip, popup)
        End If
        Dim handle = tip.Handle
        Dim width = CInt(Math.Round(ToolTipMaxWidth * DpiScale(), MidpointRounding.AwayFromZero))
        tip.MinimumSize = New Size(width, 1)
        ' 只固定宽度，高度仍由 LakeUI 按换行计算；0 会被最小高度钳制。
        tip.MaximumSize = New Size(width, Integer.MaxValue)
    End Sub

    Private Sub 定位模型提示(tip As Form, popup As Form)
        If _正在定位提示 OrElse tip.IsDisposed OrElse popup.IsDisposed Then Return
        _正在定位提示 = True
        Try
            Dim area = 获取模型提示可见区域(tip)
            Dim gap = CInt(Math.Round(ToolTipGap * DpiScale(), MidpointRounding.AwayFromZero))
            Dim right = popup.Right + gap
            Dim left = popup.Left - gap - tip.Width
            Dim x = If(ToolTipSide = ModernComboBox.ToolTipSideEnum.Left, left, right)
            If x < area.Left Then x = right
            If x + tip.Width > area.Right Then x = left
            x = Math.Clamp(x, area.Left, Math.Max(area.Left, area.Right - tip.Width))
            Dim y = Math.Clamp(tip.Top, area.Top, Math.Max(area.Top, area.Bottom - tip.Height))
            Dim location As New Point(x, y)
            If tip.Location <> location Then tip.Location = location
        Finally
            _正在定位提示 = False
        End Try
    End Sub

    Private Function 获取模型提示可见区域(tip As Form) As Rectangle
        Dim area = Screen.FromControl(Me).WorkingArea
        Dim host = TryCast(TopLevelControl, Form)
        If host Is Nothing OrElse host.IsDisposed Then Return area
        Dim visible = Rectangle.Intersect(area, host.RectangleToScreen(host.ClientRectangle))
        ' 优先留在主窗口内；窗口小到容纳不下提示时，仍以显示器工作区为边界。
        If visible.Width >= tip.Width AndAlso visible.Height >= tip.Height Then Return visible
        Return area
    End Function

    Public ReadOnly Property SelectedModel As VmafModelDisplayItem
        Get
            Dim modelIndex = SelectedIndex - 1
            If modelIndex < 0 OrElse modelIndex >= _displayItems.Count Then Return Nothing
            Return If(_displayItems(modelIndex).IsSectionHeader, Nothing, _displayItems(modelIndex))
        End Get
    End Property

    Public ReadOnly Property SelectedModelValue As String
        Get
            Return If(SelectedModel?.ModelValue, "")
        End Get
    End Property

    Public ReadOnly Property SelectedUsesCuda As Boolean
        Get
            Return SelectedModel IsNot Nothing AndAlso SelectedModel.UseCuda
        End Get
    End Property

    Public ReadOnly Property FirstModelIndex As Integer
        Get
            Return If(_displayItems.Count > 0, 1, -1)
        End Get
    End Property

    Public Sub ReplaceDisplayItems(itemsToShow As IEnumerable(Of VmafModelDisplayItem))
        Dim displayItems = If(itemsToShow, Enumerable.Empty(Of VmafModelDisplayItem)()).
            Where(Function(item) item IsNot Nothing AndAlso Not item.IsAuto AndAlso Not item.IsSectionHeader).ToList()
        _updatingItems = True
        Try
            _displayItems.Clear()
            Items.Clear()
            Items.Add(浏览本地模型文本)
            AddDisplayItem(New VmafModelDisplayItem With {.ModelValue = "AUTO", .VersionLabel = "AUTO", .IsAuto = True}, False)

            For Each item In displayItems.Where(Function(value) Not value.IsNonNeg).Concat(displayItems.Where(Function(value) value.IsNonNeg))
                AddDisplayItem(item, False)
            Next

            SelectedIndex = -1
            Text = ""
            _lastModelIndex = -1
            SelectedIndex = FirstModelIndex
        Finally
            _updatingItems = False
        End Try
    End Sub

    Public Function AddDisplayItem(item As VmafModelDisplayItem, Optional selectItem As Boolean = True) As Integer
        If item Is Nothing OrElse item.IsSectionHeader Then Return -1
        EnsureBrowseItem()

        Dim existing = FindModelIndex(item.ModelValue, item.UseCuda)
        If existing >= 0 Then
            If selectItem Then SelectedIndex = existing
            Return existing
        End If

        Dim previous = SelectedModel
        Dim wasUpdating = _updatingItems
        Dim comboIndex As Integer
        _updatingItems = True
        Try
            Dim headerIndex = _displayItems.FindIndex(Function(value) value.IsSectionHeader)
            If item.IsNonNeg AndAlso headerIndex < 0 Then
                Dim header As New VmafModelDisplayItem With {.IsSectionHeader = True}
                _displayItems.Add(header)
                Items.Add(header.DisplayText)
                headerIndex = _displayItems.Count - 1
            End If
            Dim insertIndex = If(Not item.IsNonNeg AndAlso headerIndex >= 0, headerIndex, _displayItems.Count)
            _displayItems.Insert(insertIndex, item)
            Items.Insert(insertIndex + 1, item.DisplayText)
            comboIndex = insertIndex + 1
            If selectItem Then
                SelectedIndex = comboIndex
            ElseIf previous IsNot Nothing Then
                SelectedIndex = FindModelIndex(previous.ModelValue, previous.UseCuda)
            End If
        Finally
            _updatingItems = wasUpdating
        End Try
        If Not wasUpdating AndAlso SelectedModel IsNot Nothing Then _lastModelIndex = SelectedIndex
        Return comboIndex
    End Function

    Public Function FindModelIndex(modelValue As String, useCuda As Boolean) As Integer
        For i = 0 To _displayItems.Count - 1
            Dim item = _displayItems(i)
            If Not item.IsSectionHeader AndAlso item.UseCuda = useCuda AndAlso
               String.Equals(item.ModelValue, modelValue, StringComparison.OrdinalIgnoreCase) Then
                Return i + 1
            End If
        Next
        Return -1
    End Function

    Private Sub EnsureBrowseItem()
        If Items.Count > 0 Then Return
        Items.Add(浏览本地模型文本)
    End Sub

    Private Function 是分组提示行(index As Integer) As Boolean
        Return index > 0 AndAlso index <= _displayItems.Count AndAlso _displayItems(index - 1).IsSectionHeader
    End Function

    Private Sub 记录有效模型选择(sender As Object, e As EventArgs)
        If _updatingItems OrElse _handlingBrowse Then Return
        If 是分组提示行(SelectedIndex) Then
            ' LakeUI 在下拉关闭后才触发选择事件；先阻止说明行成为当前模型。
            _handlingBrowse = True
            Try
                SelectedIndex = If(_lastModelIndex > 0 AndAlso _lastModelIndex < Items.Count, _lastModelIndex, FirstModelIndex)
            Finally
                _handlingBrowse = False
            End Try
        ElseIf SelectedIndex > 0 Then
            _lastModelIndex = SelectedIndex
        End If
    End Sub

    Protected Overrides Sub OnKeyDown(e As KeyEventArgs)
        If DroppedDown AndAlso (e.KeyCode = Keys.Up OrElse e.KeyCode = Keys.Down) Then
            Dim direction = If(e.KeyCode = Keys.Down, 1, -1)
            If 是分组提示行(SelectedIndex + direction) Then
                SelectedIndex += direction * 2
                e.Handled = True
                Return
            End If
        End If
        MyBase.OnKeyDown(e)
    End Sub

    Protected Overrides Sub OnMouseWheel(e As MouseEventArgs)
        If Not DroppedDown AndAlso e.Delta <> 0 Then
            Dim direction = If(e.Delta < 0, 1, -1)
            If 是分组提示行(SelectedIndex + direction) Then
                SelectedIndex += direction * 2
                Return
            End If
        End If
        MyBase.OnMouseWheel(e)
    End Sub

    Private Sub 处理模型选择变化(sender As Object, e As EventArgs)
        If _updatingItems OrElse _handlingBrowse Then Return
        If 是分组提示行(SelectedIndex) Then
            记录有效模型选择(sender, e)
            Return
        End If

        If SelectedIndex > 0 Then
            _lastModelIndex = SelectedIndex
            Return
        End If
        If SelectedIndex <> 0 Then Return

        ' “浏览”是命令项而非模型。LakeUI 负责完成标准下拉选择与关闭，再恢复原模型并触发文件选择。
        _handlingBrowse = True
        Try
            SelectedIndex = If(_lastModelIndex > 0 AndAlso _lastModelIndex < Items.Count,
                               _lastModelIndex,
                               FirstModelIndex)
        Finally
            _handlingBrowse = False
        End Try
        RaiseEvent BrowseRequested(Me, EventArgs.Empty)
    End Sub

    Protected Overrides Function RenderCustomSelectionContentGpu(context As D3D_PaintContext,
                                                                  bounds As RectangleF) As Boolean
        Dim item = SelectedModel
        If item Is Nothing OrElse item.IsLocal OrElse item.IsAuto OrElse item.VersionLabel.StartsWith("vmaf_", StringComparison.OrdinalIgnoreCase) Then Return False

        Dim scale = DpiScale()
        Dim gap = 胶囊间距逻辑值 * scale
        Dim x = Math.Max(CSng(Padding.Left), 8.0F * scale)
        Dim rightLimit = Math.Max(x, bounds.Right - Height - 4.0F * scale)
        Dim capsuleHeight = Math.Min(胶囊高度逻辑值 * scale, Math.Max(1.0F, bounds.Height - 8.0F * scale))
        Dim y = bounds.Y + (bounds.Height - capsuleHeight) / 2.0F
        Dim textColor = If(Enabled, ForeColor, Color.FromArgb(150, ForeColor))
        DrawSelectionCapsule(context, item.VersionLabel, 版本胶囊宽度逻辑值 * scale, x, y, capsuleHeight, rightLimit, gap, textColor, If(item.VersionLabel = "V0", 胶囊类别.旧版, 胶囊类别.版本))
        If item.Is4K Then DrawSelectionCapsule(context, "4K", 分辨率胶囊宽度逻辑值 * scale, x, y, capsuleHeight, rightLimit, gap, textColor, 胶囊类别.分辨率)
        If item.IsNonNeg Then
            DrawSelectionCapsule(context, "非neg模型", 非Neg胶囊宽度逻辑值 * scale, x, y, capsuleHeight, rightLimit, gap, textColor, 胶囊类别.非Neg)
        Else
            If Not String.IsNullOrWhiteSpace(item.ViewingDistanceLabel) Then DrawSelectionCapsule(context, item.ViewingDistanceLabel, 观看距离胶囊宽度逻辑值 * scale, x, y, capsuleHeight, rightLimit, gap, textColor, 获取观看距离胶囊类别(item.ViewingDistanceLabel))
            If item.IsHfr Then DrawSelectionCapsule(context, "HFR", Hfr胶囊宽度逻辑值 * scale, x, y, capsuleHeight, rightLimit, gap, textColor, 胶囊类别.Hfr)
            If item.IsHdr Then DrawSelectionCapsule(context, "HDR", Hdr胶囊宽度逻辑值 * scale, x, y, capsuleHeight, rightLimit, gap, textColor, 胶囊类别.Hdr)
        End If
        If item.UseCuda Then DrawSelectionCapsule(context, "CUDA", Cuda胶囊宽度逻辑值 * scale, x, y, capsuleHeight, rightLimit, gap, textColor, 胶囊类别.Cuda)
        Return True
    End Function

    Protected Overrides Function RenderCustomDropDownItemGpu(context As D3D_PaintContext,
                                                                     itemIndex As Integer,
                                                                     itemRect As RectangleF,
                                                                     textColor As Color) As Boolean
        If itemIndex <= 0 Then Return False

        Dim modelIndex = itemIndex - 1
        If modelIndex < 0 OrElse modelIndex >= _displayItems.Count Then Return False
        Dim item = _displayItems(modelIndex)
        If item.IsLocal OrElse item.IsAuto OrElse item.IsSectionHeader OrElse item.VersionLabel.StartsWith("vmaf_", StringComparison.OrdinalIgnoreCase) Then Return False

        Dim scale = DpiScale()
        Dim gap = 胶囊间距逻辑值 * scale
        Dim x = itemRect.X + DropDownPadding.Left + 4.0F
        Dim capsuleHeight = Math.Min(胶囊高度逻辑值 * scale, Math.Max(1.0F, itemRect.Height - 10.0F * scale))
        Dim y = itemRect.Y + (itemRect.Height - capsuleHeight) / 2.0F
        ' 每一分类使用固定列宽；非 NEG 合并距离/HFR/HDR 三列，CUDA 的列位不变。
        DrawDropDownCapsuleSlot(context, item.VersionLabel, 版本胶囊宽度逻辑值 * scale, x, y, capsuleHeight, gap, textColor, If(item.VersionLabel = "V0", 胶囊类别.旧版, 胶囊类别.版本))
        DrawDropDownCapsuleSlot(context, If(item.Is4K, "4K", ""), 分辨率胶囊宽度逻辑值 * scale, x, y, capsuleHeight, gap, textColor, 胶囊类别.分辨率)
        If item.IsNonNeg Then
            DrawDropDownCapsuleSlot(context, "非neg模型", 非Neg胶囊宽度逻辑值 * scale, x, y, capsuleHeight, gap, textColor, 胶囊类别.非Neg)
        Else
            DrawDropDownCapsuleSlot(context, item.ViewingDistanceLabel, 观看距离胶囊宽度逻辑值 * scale, x, y, capsuleHeight, gap, textColor, 获取观看距离胶囊类别(item.ViewingDistanceLabel))
            DrawDropDownCapsuleSlot(context, If(item.IsHfr, "HFR", ""), Hfr胶囊宽度逻辑值 * scale, x, y, capsuleHeight, gap, textColor, 胶囊类别.Hfr)
            DrawDropDownCapsuleSlot(context, If(item.IsHdr, "HDR", ""), Hdr胶囊宽度逻辑值 * scale, x, y, capsuleHeight, gap, textColor, 胶囊类别.Hdr)
        End If
        DrawDropDownCapsuleSlot(context, If(item.UseCuda, "CUDA", ""), Cuda胶囊宽度逻辑值 * scale, x, y, capsuleHeight, gap, textColor, 胶囊类别.Cuda)
        Return True
    End Function

    Private Sub DrawSelectionCapsule(context As D3D_PaintContext,
                                     text As String,
                                     width As Single,
                                     ByRef x As Single,
                                     y As Single,
                                     height As Single,
                                     rightLimit As Single,
                                     gap As Single,
                                     textColor As Color,
                                     kind As 胶囊类别)
        If String.IsNullOrWhiteSpace(text) OrElse x + width > rightLimit Then Return
        DrawCapsule(context, text, New RectangleF(x, y, width, height), textColor, kind)
        x += width + gap
    End Sub

    Private Sub DrawDropDownCapsuleSlot(context As D3D_PaintContext,
                                        text As String,
                                        width As Single,
                                        ByRef x As Single,
                                        y As Single,
                                        height As Single,
                                        gap As Single,
                                        textColor As Color,
                                        kind As 胶囊类别)
        If Not String.IsNullOrWhiteSpace(text) Then
            DrawCapsule(context, text, New RectangleF(x, y, width, height), textColor, kind)
        End If
        x += width + gap
    End Sub

    Private Sub DrawCapsule(context As D3D_PaintContext,
                            text As String,
                            rect As RectangleF,
                            textColor As Color,
                            kind As 胶囊类别)
        Dim colors = 获取胶囊配色(kind, textColor.GetBrightness() < 0.5F)
        Dim foreground = If(Enabled, colors.文字, Color.FromArgb(150, colors.文字))
        context.FillRoundedRectangle(rect, rect.Height / 2.0F, Color.FromArgb(180, colors.背景))
        context.DrawText(text, Font, foreground, rect,
                         Vortice.DirectWrite.TextAlignment.Center,
                         Vortice.DirectWrite.ParagraphAlignment.Center)
    End Sub

    Private Shared Function 获取观看距离胶囊类别(distance As String) As 胶囊类别
        Select Case If(distance, "").ToUpperInvariant()
            Case "1.5H"
                Return 胶囊类别.观看距离近
            Case "5H"
                Return 胶囊类别.观看距离远
            Case Else
                Return 胶囊类别.观看距离
        End Select
    End Function

    Private Shared Function 获取胶囊配色(kind As 胶囊类别, 浅色 As Boolean) As (背景 As Color, 文字 As Color)
        Return If(浅色, 浅色胶囊配色, 深色胶囊配色)(CInt(kind))
    End Function

    Private Function DpiScale() As Single
        Return Math.Max(1.0F, DeviceDpi / 96.0F)
    End Function
End Class
