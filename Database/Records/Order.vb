' Laurie is working on this! 
' '*****************************************************************************************
' Student Names: Laurie Shields (034448142)
'                Mark Lindan (063336143)
' CVB815 - Order.vb
' Last Updated On: Feb 21, 2015
'*******************************************************************************************
Imports System.IO

Public Class Order : Inherits Record
    Public Property Customer As Customer
    Public Property Items As ArrayList ' List(Of OrderItem)

    Public Sub New(customer As Customer, Items As ArrayList, Quantity As Integer)
        Me.Customer = customer
        Me.Items = Items

    End Sub

    Public Sub New(line As String)
        Dim fields() As String = line.Split(",")
        If fields.Length = fieldcount Then
            ' assign the fields from the line
        Else
            Throw New InvalidDataException("File does not contain valid data")
        End If
    End Sub

    Protected Overrides ReadOnly Property fieldcount As UShort
        Get
            Return 7  ' TODO - figure out what this should be
        End Get
    End Property

End Class
