' '*****************************************************************************************
' Student Names: Laurie Shields (034448142)
'                Mark Lindan (063336143)
' CVB815 - Customer.vb
' Last Updated On: Feb 21, 2015
'*******************************************************************************************
Imports System.Net.Mail
Imports System.IO
Imports CSLib

<Serializable()> Public Class Customer : Inherits Record : Implements IRecord

    Protected _name As String
    Protected _email As String
    Protected _phone_number As String
    Protected _credit_limit As Double
    Protected _mailing_address_id As Integer
    Protected _shipping_address_id As Integer

    Protected Friend Property mailing_address As Address
    Protected Friend Property shipping_address As Address

    Public Sub New(id As Int16, name As String, email As String, mailing_address As Integer, shipping_address As Integer, phone_number As String, credit_limit As Double)
        Me.ID = id
        Me.name = name
        Me.email = email
        Me.mailing_address_id = mailing_address
        Me.shipping_address_id = shipping_address
        Me.phone_number = phone_number
        Me.credit_limit = credit_limit
    End Sub

    Public Sub New(csv As String)
        InterpretCSV(csv)
    End Sub

    Protected Overrides ReadOnly Property fieldcount As UShort
        Get
            Return 7
        End Get
    End Property

    Public Property name As String
        Get
            Return _name
        End Get
        Set(value As String)
            If IsValid(value, "^[A-Za-z ]+$") Then
                Me._name = value
            Else
                Throw New ArgumentException("Invalid name")
            End If
        End Set
    End Property

    Public Property email As String
        Get
            Return _email
        End Get
        Set(value As String)
            If IsValid(value) Then
                Me._email = value
            Else
                Throw New ArgumentException("Invalid email")
            End If
        End Set
    End Property

    Public Property phone_number As String
        Get
            Return _phone_number
        End Get
        Set(value As String)
            If IsValid(value, "^([0-9]( |-)?)?(\(?[0-9]{3}\)?|[0-9]{3})( |-)?([0-9]{3}( |-)?[0-9]{4}|[a-zA-Z0-9]{7})$") Then
                Me._phone_number = value
            Else
                Throw New ArgumentException("Invalid phone number")
            End If
        End Set
    End Property

    Public Property credit_limit As Double
        Get
            Return _credit_limit
        End Get
        Set(value As Double)
            If value >= 0 Then
                Me._credit_limit = value
            Else
                Throw New ArgumentException("Invalid credit limit")
            End If
        End Set
    End Property

    Public Property mailing_address_id As Integer
        Get
            Return _mailing_address_id
        End Get
        Set(value As Integer)
            If value >= 0 Then
                _mailing_address_id = value

            End If
        End Set
    End Property

    Public Property shipping_address_id As Integer
        Get
            Return _shipping_address_id
        End Get
        Set(value As Integer)
            If value >= 0 Then
                _shipping_address_id = value
            End If
        End Set
    End Property

    Private Overloads Function IsValid(emailaddress As String) As Boolean
        Try
            Dim m As New MailAddress(emailaddress)
            Return True
        Catch ex As FormatException
            Return False
        End Try
    End Function

    Public Overrides Function GetCSV() As String
        Return Me.ID & "," & Me.name & "," & Me.email & "," & Me.phone_number & "," & Me.credit_limit.ToString("0.00") & "," & Me.mailing_address_id & "," & Me.shipping_address_id
    End Function

    Public Overrides Sub InterpretCSV(csv As String)
        Dim fields() As String = csv.Split(",")
        If fields.Length = fieldcount Then
            ' TODO :: assign the fields from the line
            Me.ID = Integer.Parse(fields(0))
            Me.name = fields(1)
            Me.email = fields(2)
            Me.phone_number = fields(3)
            Me.credit_limit = Double.Parse(fields(4))
            Me.mailing_address_id = Integer.Parse(fields(5))
            Me.shipping_address_id = Integer.Parse(fields(6))
        Else
            Throw New InvalidDataException("File does not contain valid data")
        End If

    End Sub

End Class
