' '*****************************************************************************************
' Student Names: Laurie Shields (034448142)
'                Mark Lindan (063336143)
' CVB815 - ProductBook.vb
'*******************************************************************************************
Imports System.Text.RegularExpressions

<Serializable()> Public Class ProductBook : Inherits Book(Of Product)

    Protected Overrides Sub Interpret(line As String)
        Dim entry As New Product(line)
        Me.Add(entry)
    End Sub

    Public Function GetProdListNotInOrder(orderlist As List(Of OrderItem)) As String()
        ' Makes a list of book, removes those in the order list, then turns it into a string array.
        Dim tempresult As New List(Of Product)
        For Each item As Product In Book
            tempresult.Add(item)
        Next
        For Each item As OrderItem In orderlist
            tempresult.Remove(item.product)
        Next
        Dim result As New List(Of String)
        For Each item In tempresult
            result.Add(item.ToString)
        Next
        Return result.ToArray
    End Function
End Class
