' '*****************************************************************************************
' Student Names: Laurie Shields (034448142)
'                Mark Lindan (063336143)
' CVB815 - CustomerBook.vb
'*******************************************************************************************
Imports System.Text.RegularExpressions

<Serializable()> Public Class CustomerBook : Inherits Book(Of Customer)

    Public Sub New()

    End Sub

    Protected Overrides Sub Interpret(line As String)
        Dim entry As New Customer(line)
        Me.Add(entry)
    End Sub

End Class

