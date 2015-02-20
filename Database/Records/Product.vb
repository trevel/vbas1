Public Class Product : Inherits Record
    Protected _Description As String
    Protected _Price As Double
    Protected _Inventory As Integer

    ' Public Sub New(Description As String, Price As Double, Inventory As Integer)

    ' End Sub

    Public Sub New(ID As Integer, Description As String, Price As Double, Inventory As Integer)
        Me.ID = ID
        Me.Description = Description
        Me.Price = Price
        Me.Inventory = Inventory
    End Sub
    Public Property Description As String
        Get
            Return _Description
        End Get
        Set(value As String)
            If value.Length > 3 Then
                Me._Description = value
            Else
                Throw New ArgumentException("Invalid Description")
            End If
        End Set
    End Property
    Public Property Price As Double
        Get
            Return _Price
        End Get
        Set(value As Double)
            If value >= 0 Then
                Me._Price = Price
            Else
                Throw New ArgumentException("Invalid Price. Be more positive")
            End If
        End Set
    End Property
    Public Property Inventory As Integer
        Get
            Return _Inventory
        End Get
        Set(value As Integer)
            If value >= 0 Then
                Me._Inventory = value
            Else
                Throw New ArgumentException("Invalid Inventory.")
            End If
        End Set
    End Property

    Public Sub addInventory(amount As Integer)
        If amount >= 0 Then
            _Inventory += amount
        Else
            Throw New ArgumentException("Can't add less than nothing")
        End If
    End Sub

    Public Sub removeInventory(amount As Integer)
        If amount >= 0 And amount <= _Inventory Then
            _Inventory -= amount
        Else
            Throw New ArgumentException("We don't have enough")
        End If
    End Sub
End Class
