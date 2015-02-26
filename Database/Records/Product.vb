' '*****************************************************************************************
' Student Names: Laurie Shields (034448142)
'                Mark Lindan (063336143)
' CVB815 - Product.vb
'*******************************************************************************************
Imports System.IO
<Serializable()> Public Class Product : Inherits Record
    Protected _Description As String
    Protected _Price As Double
    Protected _Inventory As Integer

    Public Shared Event MoreInventory(prod As Product)

    ' Public Sub New(Description As String, Price As Double, Inventory As Integer)

    ' End Sub

    Public Sub New(ID As Integer, Description As String, Price As Double, Inventory As Integer)
        Me.ID = ID
        Me.Description = Description
        Me.Price = Price
        Me.Inventory = Inventory
    End Sub


    Public Sub New(line As String)
        InterpretCSV(line)
    End Sub

    Protected Overrides ReadOnly Property fieldcount As UShort
        Get
            Return 4
        End Get
    End Property

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
                Me._Price = value
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
                If value > Me._Inventory Then
                    addInventory(value - Me._Inventory)
                ElseIf value <= Me._Inventory Then
                    removeInventory(Me._Inventory - value)
                End If
                Me._Inventory = value
            Else
                Throw New ArgumentException("Invalid Inventory.")
            End If
        End Set
    End Property

    Public Sub addInventory(amount As Integer)
        If amount >= 0 Then
            _Inventory += amount
            RaiseEvent MoreInventory(Me)
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


    Public Overrides Sub InterpretCSV(csv As String)
        Dim fields() As String = csv.Split(",")
        If fields.Length = fieldcount Then
            Me.ID = fields(0)
            Me.Description = fields(1)
            Me.Price = Convert.ToDouble(fields(2))
            Me.Inventory = Convert.ToInt16(fields(3))
        Else
            Throw New InvalidDataException("File does not contain valid data")
        End If
    End Sub

    Public Overrides Function GetCSV() As String
        Return Me.ID & "," & Me.Description & "," & Me.Price.ToString("f2") & "," & Me.Inventory
    End Function

    Public Overrides Function ToString() As String
        Return Me.ID & "::" & Me.Description & "," & Me.Price.ToString("f2") & "," & Me.Inventory
    End Function
End Class
