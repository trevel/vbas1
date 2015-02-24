' '*****************************************************************************************
' Student Names: Laurie Shields (034448142)
'                Mark Lindan (063336143)
' CVB815 - OrderBook.vb
' Last Updated On: Feb 21, 2015
'*******************************************************************************************
<Serializable()> Public Class OrderBook : Inherits Book(Of Order)

    Public Event NewOrder(item As Order)

    Protected Overrides Sub Interpret(line As String)

        Dim entry As New Order(line)
        RaiseEvent NewOrder(entry)
        Book.Add(entry)
        next_id = entry.GetID
    End Sub
End Class


