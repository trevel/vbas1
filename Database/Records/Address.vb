Imports System.IO
Imports CSLib

'*****************************************************************************************
' Student Names: Laurie Shields (034448142)
'                Mark Lindan (063336143)
' CVB815 - Address.vb
' Last Updated On: Feb 21, 2015
'*******************************************************************************************
<Serializable()> Public Class Address : Inherits Record : Implements IRecord

    Public Enum AddressType
        mailing_address
        shipping_address
    End Enum

    Protected _cust_id As Integer
    Protected _street As String
    Protected _city As String
    Protected _province As String
    Protected _postal_code As String
    Public Property type As AddressType
    Public Property customer As Customer
    Public Sub New(csv As String)
        InterpretCSV(csv)
    End Sub

    Public Sub New(id As Integer, cust_id As Integer, street As String, city As String, province As String, postal_code As String)
        Me.New(id, cust_id, street, city, province, postal_code, AddressType.mailing_address)
    End Sub

    Public Sub New(id As Integer, cust_id As Integer, street As String, city As String, province As String, postal_code As String, type As AddressType)
        Me.ID = id
        Me.cust_id = cust_id
        Me.street = street
        Me.city = city
        Me.province = province
        Me.postal_code = postal_code
        Me.type = type
    End Sub

    Protected Overrides ReadOnly Property fieldcount As UShort
        Get
            Return 7
        End Get
    End Property

    Public Property cust_id As Integer
        Get
            Return _cust_id
        End Get
        Set(value As Integer)
            If value >= 0 Then
                _cust_id = value
            End If
        End Set
    End Property

    Public Property street As String
        Get
            Return _street
        End Get
        Set(value As String)
            If IsValid(value, "^[0-9]") Then
                Me._street = value
            Else
                Throw New ArgumentException("Invalid Street")
            End If
        End Set
    End Property
    Public Property city As String
        Get
            Return _city
        End Get
        Set(value As String)
            If IsValid(value, "^[A-Za-z ]+$") Then
                Me._city = value
            Else
                Throw New ArgumentException("Invalid City")
            End If
        End Set
    End Property
    Public Property province As String
        Get
            Return _province
        End Get
        Set(value As String)
            If IsValid(value, "^[A-Za-z]{2}$") Then
                Me._province = value
            Else
                Throw New ArgumentException("Invalid Province")
            End If
        End Set
    End Property
    Public Property postal_code As String
        Get
            Return _postal_code
        End Get
        Set(value As String)
            If IsValid(value, "^[A-Za-z][0-9][A-Za-z][ ]?[0-9][A-Za-z][0-9]$") Then
                Me._postal_code = value.ToUpper
            Else
                Throw New ArgumentException("Invalid Postal Code")
            End If
        End Set
    End Property

    Public Overrides Sub InterpretCSV(csv As String)
        Dim fields() As String = csv.Split(",")
        If fields.Length = fieldcount Then
            Me.ID = fields(0)
            Me.cust_id = fields(1)
            Me.street = fields(2)
            Me.city = fields(3)
            Me.province = fields(4)
            Me.postal_code = fields(5)
            Me.type = fields(6)
        Else
            Throw New InvalidDataException("File does not contain valid data")
        End If
    End Sub

    Public Overrides Function GetCSV() As String
        Return Me.ID & "," & Me.cust_id & "," & Me.street & "," & Me.city & "," & Me.province & "," & Me.postal_code & "," & Me.type
    End Function

    Public Overrides Function ToString() As String
        If customer IsNot Nothing Then
            Return Me.ID & "::" & Me.customer.name & "," & Me.street & "," & Me.city & "," & Me.province & "," & Me.postal_code
        Else
            Return Me.ID & "::" & Me.cust_id & "," & Me.street & "," & Me.city & "," & Me.province & "," & Me.postal_code
        End If
    End Function
End Class
