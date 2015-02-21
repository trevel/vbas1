' Laurie is working on this! 
' '*****************************************************************************************
' Student Names: Laurie Shields (034448142)
'                Mark Lindan (063336143)
' CVB815 - OrderItem.vb
' Last Updated On: Feb 21, 2015
'*******************************************************************************************
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
