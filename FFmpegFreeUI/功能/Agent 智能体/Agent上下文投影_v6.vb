Public NotInheritable Class AgentContextProjection
    Private Sub New()
    End Sub

    Public Shared Function ResolveBoundaryIndex(messages As IList(Of AgentMessageData),
                                                checkpoint As AgentContextCheckpointData) As Integer
        If checkpoint Is Nothing OrElse
           String.IsNullOrWhiteSpace(checkpoint.Summary) OrElse
           String.IsNullOrWhiteSpace(checkpoint.CoveredThroughMessageId) OrElse
           messages Is Nothing Then
            Return -1
        End If

        For i = 0 To messages.Count - 1
            Dim message = messages(i)
            If message Is Nothing OrElse
               Not String.Equals(message.Id, checkpoint.CoveredThroughMessageId, StringComparison.OrdinalIgnoreCase) Then
                Continue For
            End If

            If i + 1 < messages.Count AndAlso
               String.Equals(If(messages(i + 1)?.Role, ""), "tool", StringComparison.OrdinalIgnoreCase) Then
                Return -1
            End If
            Return i
        Next

        Return -1
    End Function

    Public Shared Function BuildModelContext(messages As IList(Of AgentMessageData),
                                             checkpoint As AgentContextCheckpointData) As List(Of AgentMessageData)
        Dim source = If(messages, Array.Empty(Of AgentMessageData)()).
            Where(Function(x) x IsNot Nothing).
            ToList()
        Dim boundaryIndex = ResolveBoundaryIndex(source, checkpoint)
        If boundaryIndex < 0 Then Return source

        Dim result As New List(Of AgentMessageData) From {
            BuildCheckpointMessage(checkpoint)
        }
        result.AddRange(source.Skip(boundaryIndex + 1))
        Return result
    End Function

    Public Shared Function BuildCheckpointMessage(checkpoint As AgentContextCheckpointData) As AgentMessageData
        Dim modelText = If(String.IsNullOrWhiteSpace(checkpoint?.ModelId), "", $"，模型 {checkpoint.ModelId}")
        Dim timeText = If(checkpoint Is Nothing OrElse checkpoint.UpdatedAt = DateTime.MinValue, "", $"，时间 {checkpoint.UpdatedAt:yyyy-MM-dd HH:mm:ss}")
        Return New AgentMessageData With {
            .Role = "system",
            .Content = $"长期上下文检查点（覆盖到消息 {checkpoint.CoveredThroughMessageId}{modelText}{timeText}）：{vbCrLf}{checkpoint.Summary.Trim()}"
        }
    End Function
End Class
