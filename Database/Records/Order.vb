' Laurie is working on this! 
' '*****************************************************************************************
' Student Names: Laurie Shields (034448142)
'                Mark Lindan (063336143)
' CVB815 - Order.vb
' Last Updated On: Feb 21, 2015
'*******************************************************************************************
Imports System.IO

<Serializable()> Public Class Order : Inherits Record
    Protected _customer As Customer
    Protected _order_date As Date
    Protected _items As ArrayList ' List(Of OrderItem)
    Protected _discount As Double

    Public Sub New(id As Int16, cust As Customer, odate As Date, disc As Double, Items As ArrayList)
        Me.ID = id
        Me.customer = customer
        Me.order_date = odate
        Me.discount = disc
        ' Me.Items = Nothing  ' LAURIE :: TODO
    End Sub

    Public Sub New(csv As String)
        InterpretCSV(csv)
    End Sub

    Public Property customer As Customer
        Get
            Return _customer
        End Get
        Set(value As Customer)
            Me._customer = value
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

    Public Overrides Function GetCSV() As String
        Return Me.ID & "," & Format(Me.order_date, "yyyy-MM-dd") & "," & Me.discount.ToString("0.00") & "," & Me.customer.ID
    End Function

    Public Overrides Sub InterpretCSV(csv As String)
        Throw New NotImplementedException
    End Sub

End Class
