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
End Class



