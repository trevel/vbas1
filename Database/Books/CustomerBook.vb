' '*****************************************************************************************
' Student Names: Laurie Shields (034448142)
'                Mark Lindan (063336143)
' CVB815 - CustomerBook.vb
' Last Updated On: Feb 21, 2015
'*******************************************************************************************
Imports System.Text.RegularExpressions

<Serializable()> Public Class CustomerBook : Inherits Book(Of Customer)

    Protected addressbook As AddressBook = Nothing

    Public Sub New(addressbook As AddressBook)
        Me.addressbook = addressbook
    End Sub

    Protected Overrides Sub Interpret(line As String)

        Dim entry As New Customer(line)
        Book.Add(entry)
        next_id = entry.GetID
        ' RaiseEvent NewFriend(entry)
    End Sub

End Class

