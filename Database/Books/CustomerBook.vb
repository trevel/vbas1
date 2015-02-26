' '*****************************************************************************************
' Student Names: Laurie Shields (034448142)
'                Mark Lindan (063336143)
' CVB815 - CustomerBook.vb
'*******************************************************************************************
Imports System.Text.RegularExpressions

<Serializable()> Public Class CustomerBook : Inherits Book(Of Customer)

    'Public addressbook As New AddressBook

    Public Sub New() ' addressbook As AddressBook)
        ' Me.addressbook = addressbook
    End Sub

    Protected Overrides Sub Interpret(line As String)
        Dim entry As New Customer(line)
        Me.Add(entry)
        ' RaiseEvent NewFriend(entry)
    End Sub

End Class

