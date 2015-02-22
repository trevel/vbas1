' Laurie is working on this! 
' '*****************************************************************************************
' Student Names: Laurie Shields (034448142)
'                Mark Lindan (063336143)
' CVB815 - Order.vb
' Last Updated On: Feb 21, 2015
'*******************************************************************************************
Imports System.IO

<Serializable()> Public Class Order : Inherits Record
    Public Property Customer As Customer
    Public Property Items As ArrayList ' List(Of OrderItem)

    Public Sub New(customer As Customer, Items As ArrayList, Quantity As Integer)
        Me.Customer = customer
        Me.Items = Items
    End Sub

    Public Sub New(csv As String)
        InterpretCSV(csv)
    End Sub

    Protected Overrides ReadOnly Property fieldcount As UShort
        Get
            Return 7  ' TODO - figure out what this should be
        End Get
    End Property

    Public Overrides Function GetCSV() As String
        Throw New NotImplementedException
        Return Nothing
    End Function

    Public Overrides Sub InterpretCSV(csv As String)
        Throw New NotImplementedException
    End Sub

End Class
