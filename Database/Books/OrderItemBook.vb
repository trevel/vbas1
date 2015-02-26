' '*****************************************************************************************
' Student Names: Laurie Shields (034448142)
'                Mark Lindan (063336143)
' CVB815 - OrderItemBook.vb
' Last Updated On: Feb 24, 2015
'**********************s*********************************************************************

<Serializable()> Public Class OrderItemBook : Inherits Book(Of OrderItem)

    Public Event NewItem(entry As OrderItem)
    Public Event DeleteItem(item As OrderItem)

    Protected Overrides Sub Interpret(line As String)
        Dim entry As New OrderItem(line)
        RaiseEvent NewItem(entry)
        Me.Add(entry)
    End Sub

    Public Overrides Sub Add(item As OrderItem)
        MyBase.Add(item)
        RaiseEvent NewItem(item)
    End Sub

    Public Sub RemoveByOrderID(order_id As Integer)
        Dim item As OrderItem
        Dim index As Integer
        For index = Book.Count - 1 To 0 Step -1
            item = Book.Item(index)
            If item.order_id = order_id Then
                Me.Remove(item)
            End If
        Next
    End Sub

    Public Function OrderHasShippedItems(order_id As Integer) As Boolean
        For Each item As OrderItem In Book
            If item.order_id = order_id Then
                If item.has_shipped = True Then
                    Return True
                End If
            End If
        Next
        Return False
    End Function

    Public Function GetByOrderID(order_id As Integer) As String()
        Dim result As New List(Of String)
        For Each item As OrderItem In Book
            If item.order_id = order_id Then
                result.Add(item.ToString)
            End If
        Next
        Return result.ToArray()
    End Function

    Public Function IsOrderItemForProduct(prod_id As Integer) As Boolean
        For Each item As OrderItem In Book
            If item.product_id = prod_id Then
                Return True
            End If
        Next
        Return False
    End Function

    Public Function GetOrdersThatCanShip() As String()
        Dim orders As New List(Of Order)
        Dim result As New List(Of String)
        For Each item As OrderItem In Book
            If item.quantity <= item.product.Inventory And item.has_shipped = False Then
                ' item is shippable
                If orders.Contains(item.order) = False Then
                    orders.Add(item.order)
                End If
            End If
        Next
        For Each item As Order In orders
            result.Add(item.ToString())
        Next
        If result.Count = 0 Then
            Return Nothing
        End If
        Return result.ToArray()
    End Function

    Public Sub ShipAllItemsByOrderId(order_id)
        For Each item As OrderItem In Book
            If item.order_id = order_id And item.quantity <= item.product.Inventory Then
                item.ship_date = Today
                item.product.removeInventory(item.quantity)
            End If
        Next
    End Sub
End Class



