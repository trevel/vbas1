' '*****************************************************************************************
' Student Names: Laurie Shields (034448142)
'                Mark Lindan (063336143)
' CVB815 - OrderItem.vb
' Last Updated On: Feb 21, 2015
'*******************************************************************************************
Imports CSLib
Imports System.IO

<Serializable()> Public Class OrderItem : Inherits Record : Implements IRecord

    Protected _order_id As Integer = 0
    Protected _product_id As Integer
    Protected _quantity As UInteger
    Protected _ship_date As Date
    Protected _has_shipped As Boolean

    Public Property order As Order
    Public Property product As Product

    Public Property product_id As Integer
        Get
            Return _product_id
        End Get
        Set(value As Integer)
            If value > 0 Then
                _product_id = value
            End If
        End Set
    End Property

    Public Property order_id As Integer
        Get
            Return _order_id
        End Get
        Set(value As Integer)
            If value > 0 Then
                _order_id = value
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

    Public Property ship_date As Date?
        Get
            If _has_shipped Then
                Return _ship_date
            Else
                Return Nothing
            End If

        End Get
        Set(value As Date?)
            If value Is Nothing Then
                _ship_date = Date.MinValue
                _has_shipped = False
            Else
                _has_shipped = True
                _ship_date = value
            End If
        End Set
    End Property

    Public Sub New(id As Integer, order_id As Integer, product As Integer, quantity As UInteger)
        Me.ID = id
        Me.order_id = order_id
        Me.product_id = product
        Me.quantity = quantity
        Me.ship_date = Nothing
    End Sub

    Public Sub New(csv As String)
        InterpretCSV(csv)
    End Sub

    Public ReadOnly Property has_shipped As Boolean
        Get
            Return _has_shipped
        End Get
    End Property

    Protected Overrides ReadOnly Property fieldcount As UShort
        Get
            Return 5
        End Get
    End Property

    Public Overrides Sub InterpretCSV(csv As String)
        Dim fields() As String = csv.Split(",")
        If fields.Length = fieldcount Then
            Me.ID = fields(0)
            Me.order_id = Integer.Parse(fields(1))
            Me.product_id = Integer.Parse(fields(2))
            Me.quantity = Integer.Parse(fields(3))
            If fields(4) = "" Then
                Me.ship_date = Nothing
            Else
                Me.ship_date = Date.ParseExact(fields(4), "yyyy-MM-dd", Nothing)
            End If
        Else
            Throw New InvalidDataException("File does not contain valid data")
        End If
    End Sub

    Public Overrides Function GetCSV() As String
        If Me.order_id > 0 Then ' this allows us to eliminate orphaned order items that may not be attached to an order
            Return Me.ID & "," & Me.order_id & "," & Me.product_id & "," & Me.quantity & "," & Format(Me.ship_date, "yyyy-MM-dd")
        Else
            Return Nothing
        End If
    End Function

    Public Overrides Function ToString() As String
        Dim dateStr As String
        If Me.has_shipped = True Then
            dateStr = Format(Me.ship_date, "yyyy-MM-dd")
        Else
            dateStr = "Not Shipped"
        End If
        Return Me.ID & "::" & product.Description & "," & Me.quantity & "," & dateStr
    End Function
End Class
