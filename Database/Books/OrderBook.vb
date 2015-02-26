' '*****************************************************************************************
' Student Names: Laurie Shields (034448142)
'                Mark Lindan (063336143)
' CVB815 - OrderBook.vb
'*******************************************************************************************
<Serializable()> Public Class OrderBook : Inherits Book(Of Order)

    Public Event NewOrder(item As Order)
    Public Event DeleteOrder(item As Order)

    Protected Overrides Sub Interpret(line As String)
        Dim entry As New Order(line)
        Me.Add(entry)
    End Sub

    Public Overrides Sub Add(item As Order)
        MyBase.Add(item)
        RaiseEvent NewOrder(item)
    End Sub
    Public Overrides Sub Remove(item As Order)
        MyBase.Remove(item)
        RaiseEvent DeleteOrder(item)
    End Sub

    Public Function DoesCustHaveOrder(cust As Integer) As Boolean
        For Each item As Order In Book
            If item.customer_id = cust Then
                Return True
            End If
        Next
        Return False
    End Function

End Class


