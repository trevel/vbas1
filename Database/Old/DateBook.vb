<Serializable()> Public Class DateBook : Inherits Book(Of ImportantDate)

    Protected Overrides Sub Interpret(fields() As String)
        Book.Add(New ImportantDate(Convert.ToUInt32(fields(0)), Convert.ToDateTime(fields(1)), Convert.ToDateTime(fields(2)), fields(3)))
    End Sub

    Protected Overrides ReadOnly Property fieldcount As UShort
        Get
            Return 4
        End Get
    End Property
End Class
