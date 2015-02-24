' Laurie is working on this! 
' '*****************************************************************************************
' Student Names: Laurie Shields (034448142)
'                Mark Lindan (063336143)
' CVB815 - Order.vb
' Last Updated On: Feb 21, 2015
'*******************************************************************************************
Imports System.IO

<Serializable()> Public Class Order : Inherits Record
    Protected _customer_id As Integer
    Protected _order_date As Date
    Protected _items As ArrayList ' List(Of OrderItem)
    Protected _discount As Double
    Public Property customer As Customer

    Public Sub New(id As Integer, cust As Integer, odate As Date, disc As Double, Items As ArrayList)
        Me.ID = id
        Me.customer_id = cust
        Me.order_date = odate
        Me.discount = disc
        ' Me.Items = Nothing  ' LAURIE :: TODO
    End Sub

    Public Sub New(csv As String)
        InterpretCSV(csv)
    End Sub

    Public Property customer_id As Integer
        Get
            Return _customer_id
        End Get
        Set(value As Integer)
            If (value > 0) Then
                Me._customer_id = value
            End If
        End Set
    End Property

    Public Property order_date As Date
        Get
            Return _order_date
        End Get
        Set(value As Date)
            If value <= Today Then
                Me._order_date = value
            Else
                Throw New ArgumentException("Order Date cannot be in the future")
            End If
        End Set
    End Property

    Public Property discount As Double
        Get
            Return _discount
        End Get
        Set(value As Double)
            If value >= 0 Then
                Me._discount = value
            Else
                Throw New ArgumentException("Invalid discount")
            End If
        End Set
    End Property

    Protected Overrides ReadOnly Property fieldcount As UShort
        Get
            Return 4
        End Get
    End Property

    Public Function Ship() As Boolean
        Return False
    End Function

    Public Overrides Function GetCSV() As String
        Return Me.ID & "," & Format(Me.order_date, "yyyy-MM-dd") & "," & Me.discount.ToString("0.00") & "," & Me.customer_id
    End Function

    Public Overrides Sub InterpretCSV(csv As String)
        Dim fields() As String = csv.Split(",")
        If fields.Length = fieldcount Then
            Me.ID = Integer.Parse(fields(0))
            Me.order_date = Date.ParseExact(fields(1), "yyyy-MM-dd", Nothing)
            Me.discount = Double.Parse(fields(2))
            Me.customer_id = Integer.Parse(fields(3))
        Else
            Throw New InvalidDataException("File does not contain valid data")
        End If
    End Sub
End Class
