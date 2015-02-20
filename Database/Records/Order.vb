' Laurie is working on this! 

Public Class Order : Inherits Record
    Public Property Customer As Customer
    Public Property Items As ArrayList ' List(Of OrderItem)

    Public Sub New(customer As Customer, Items As ArrayList, Quantity As Integer)
        Me.Customer = customer
        Me.Items = Items

    End Sub


End Class
