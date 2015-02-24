' '*****************************************************************************************
' Student Names: Laurie Shields (034448142)
'                Mark Lindan (063336143)
' CVB815 - OrderItemBook.vb
' Last Updated On: Feb 24, 2015
'**********************s*********************************************************************

<Serializable()> Public Class OrderItemBook : Inherits Book(Of OrderItem)

    Public Event NewItem(entry As OrderItem)

    Protected Overrides Sub Interpret(line As String)
        Dim entry As New OrderItem(line)
        RaiseEvent NewItem(entry)
        Book.Add(entry)
        next_id = entry.GetID
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
        Return result.ToArray
    End Function

    Public Function IsOrderItemForProduct(prod_id As Integer) As Boolean
        For Each item As OrderItem In Book
            If item.product_id = prod_id Then
                Return True
            End If
        Next
        Return False
    End Function
End Class



