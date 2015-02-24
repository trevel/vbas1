' '*****************************************************************************************
' Student Names: Laurie Shields (034448142)
'                Mark Lindan (063336143)
' CVB815 - OrderItemBook.vb
' Last Updated On: Feb 24, 2015
'**********************s*********************************************************************

<Serializable()> Public Class OrderItemBook : Inherits Book(Of OrderItem)

    Public Event NewItem(entry As OrderItem)

    Protected Overrides Sub Interpret(line As String)
        Dim entry As New OrderItem(line)
        RaiseEvent NewItem(entry)
        Book.Add(entry)
        next_id = entry.GetID
    End Sub

    Public Function GetByOrderID(order_id As Integer) As String()
        Dim result As New List(Of String)
        For Each item As OrderItem In Book
            If item.order_id = order_id Then
                result.Add(item.ToString)
            End If
        Next
        Return result.ToArray
    End Function

End Class



