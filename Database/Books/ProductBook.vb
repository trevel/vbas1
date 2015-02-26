' '*****************************************************************************************
' Student Names: Laurie Shields (034448142)
'                Mark Lindan (063336143)
' CVB815 - ProductBook.vb
' Last Updated On: Feb 21, 2015
'*******************************************************************************************
Imports System.Text.RegularExpressions

<Serializable()> Public Class ProductBook : Inherits Book(Of Product)

    Protected Overrides Sub Interpret(line As String)

        Dim entry As New Product(line)
        Me.Add(entry)
    End Sub

End Class
