﻿Imports System.IO
Imports CSLib

'*****************************************************************************************
' Student Names: Laurie Shields (034448142)
'                Mark Lindan (063336143)
' CVB815 - Address.vb
' Last Updated On: Feb 21, 2015
'*******************************************************************************************
Public Class Address : Inherits Record : Implements IRecord

    Public Enum AddressType
        mailing_address
        shipping_address
    End Enum

    Protected _street As String
    Protected _city As String
    Protected _province As String
    Protected _postal_code As String
    Public Property type As AddressType
    Public Sub New(csv As String)
        InterpretCSV(csv)
    End Sub

    Public Sub New(id, street, city, province, postal_code)
        Me.New(id, street, city, province, postal_code, AddressType.mailing_address)
    End Sub

    Public Sub New(id, street, city, province, postalcode, type)
        Me.ID = id
        Me.street = street
        Me.city = city
        Me.province = province
        Me.postal_code = postal_code
        Me.type = type
    End Sub

    Protected Overrides ReadOnly Property fieldcount As UShort
        Get
            Return 6
        End Get
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
            If IsValid(value, "^[A-Za-z ]$") Then
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

    Public Overrides Function GetCSV() As String
        Return Me.ID & "," & Me.street & "," & Me.city & "," & Me.province & "," & Me.postal_code & "," & Me.type
    End Function

    Public Overrides Sub InterpretCSV(csv As String)
        Dim fields() As String = csv.Split(",")
        If fields.Length = fieldcount Then
            Me.ID = fields(0)
            Me.street = fields(1)
            Me.city = fields(2)
            Me.province = fields(3)
            Me.postal_code = fields(4)
            Me.type = fields(5)
        Else
            Throw New InvalidDataException("File does not contain valid data")
        End If
    End Sub

End Class