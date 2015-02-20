' Laurie is working on this! 

Public Class OrderItem
    Public Property Product As Product
    Public Property quantity As Integer
    Public Property shipped As Boolean

    Public Sub New(product As Product, quantity As Integer)
        Me.Product = product
        Me.quantity = quantity
        Me.shipped = False
    End Sub
End Class
