' '*****************************************************************************************
' Student Names: Laurie Shields (034448142)
'                Mark Lindan (063336143)
' CVB815 - DateBook.vb
' Last Updated On: Feb 21, 2015
'*******************************************************************************************
<Serializable()> Public Class DateBook : Inherits Book(Of ImportantDate)

    Protected Overrides Sub Interpret(line As String)
        Dim entry As New ImportantDate(line)
        Book.Add(entry)
    End Sub
End Class
