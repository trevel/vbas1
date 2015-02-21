<Serializable()> Public Class OrderBook : Inherits Book(Of Product)

    Protected Overrides ReadOnly Property fieldcount As UShort
        Get
            Return 7 ' LAURIE - TODO
        End Get
    End Property

    Protected Overrides Sub Interpret(fields As String())
        ' Dim entry As New Product(Convert.ToInt16(fields(0)), fields(1), Convert.ToDouble(fields(2)), Convert.ToInt16(fields(3)))
        ' book.Add(entry)
        ' RaiseEvent
    End Sub

End Class


