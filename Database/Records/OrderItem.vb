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
    Protected _product As Integer
    Protected _quantity As UInteger
    Protected _ship_date As Date

    Public Property product As Integer
        Get
            Return _product
        End Get
        Set(value As Integer)
            If value > 0 Then
                _product = value
            End If
        End Set
    End Property
    Public Property quantity As UInteger
        Get
            Return _quantity
        End Get
        Set(value As UInteger)
            If value > 0 Then
                _quantity = value
            End If
        End Set
    End Property

    Public Property ship_date As Date
        Get
            Return _ship_date
        End Get
        Set(value As Date)
            _ship_date = value
        End Set
    End Property

    Public Sub New(product As Integer, quantity As UInteger)
        Me.product = product
        Me.quantity = quantity
        Me.ship_date = Nothing
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
        Return Me.ID & "," & Me.product & "," & Me.quantity & "," & Format(Me.ship_date, "yyyy-MM-dd")
    End Function

    Public Overrides Sub InterpretCSV(csv As String)
        Dim fields() As String = csv.Split(",")
        If fields.Length = fieldcount Then
            Me.ID = fields(0)
            Me.product = Integer.Parse(fields(1))
            Me.quantity = Integer.Parse(fields(2))
            Me.ship_date = Date.ParseExact(fields(3), "yyyy-MM-dd", Nothing)
        Else
            Throw New InvalidDataException("File does not contain valid data")
        End If
    End Sub
End Class
