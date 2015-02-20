Imports System.Text.RegularExpressions

<Serializable()> Public Class ProductBook : Inherits Book(Of Product)

    Protected Overrides ReadOnly Property fieldcount As UShort
        Get
            Return 4
        End Get
    End Property

    Protected Overrides Sub Interpret(fields As String())
        Dim entry As New Product(Convert.ToInt16(fields(0)), fields(1), Convert.ToDouble(fields(2)), Convert.ToInt16(fields(3)))
        Book.Add(entry)
        ' RaiseEvent NewFriend(entry)
    End Sub

End Class

