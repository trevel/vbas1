' Laurie is working on this! 
' '*****************************************************************************************
' Student Names: Laurie Shields (034448142)
'                Mark Lindan (063336143)
' CVB815 - OrderItem.vb
' Last Updated On: Feb 21, 2015
'*******************************************************************************************
Imports CSLib
Imports System.IO

Public Class OrderItem : Inherits Record : Implements IRecord
    Public Property Product As Integer
    Public Property quantity As Integer
    Public Property shipped As Boolean

    Public Sub New(product As Integer, quantity As Integer)
        Me.Product = product
        Me.quantity = quantity
        Me.shipped = False
    End Sub
    Public Sub New(csv As String)
        InterpretCSV(csv)
    End Sub

    Protected Overrides ReadOnly Property fieldcount As UShort
        Get
            Return 4
        End Get
    End Property

    Public Overrides Function GetCSV() As String
        Return Me.ID & "," & Me.Product & "," & Me.quantity & "," & Me.shipped
    End Function

    Public Overrides Sub InterpretCSV(csv As String)
        Dim fields() As String = csv.Split(",")
        If fields.Length = fieldcount Then
            Me.ID = fields(0)
            Me.Product = Integer.Parse(fields(1))
            Me.quantity = Integer.Parse(fields(2))
            Me.shipped = Boolean.Parse(fields(3))
        Else
            Throw New InvalidDataException("File does not contain valid data")
        End If
    End Sub

End Class
