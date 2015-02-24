' '*****************************************************************************************
' Student Names: Laurie Shields (034448142)
'                Mark Lindan (063336143)
' CVB815 - AddressBook.vb
' Last Updated On: Feb 21, 2015
'*******************************************************************************************
<Serializable()> Public Class AddressBook : Inherits Book(Of Address)
    Public Property customerbook As CustomerBook = Nothing

    Protected Overrides Sub Interpret(line As String)
        Dim entry As New Address(line)
        Book.Add(entry)
        ' RaiseEvent NewFriend(entry)
    End Sub

    Public Function GetByCustID(cust_id As Integer) As String()
        Dim result As New List(Of String)
        For Each item As Address In Book
            If item.cust_id = cust_id Then
                result.Add(item.ToString)
            End If
        Next
        Return result.ToArray
    End Function

    Public Function GetShippingAddress(cust_id As Integer) As Address
        Dim mailing As Address = Nothing
        For Each item As Address In Book
            If item.cust_id = cust_id Then
                If item.type = Address.AddressType.shipping_address Then
                    Return item
                Else
                    ' if we don't find a shipping address, we'll use the mailing address
                    mailing = item
                End If
            End If
        Next
        ' if we found a mailing address but no shipping address, use the mailing address; otherwise return nothing
        Return mailing
    End Function
End Class

