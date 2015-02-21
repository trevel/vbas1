' '*****************************************************************************************
' Student Names: Laurie Shields (034448142)
'                Mark Lindan (063336143)
' CVB815 - ImportantDate.vb
' Last Updated On: Feb 21, 2015
'*******************************************************************************************
Imports CSLib
Imports System.IO

<Serializable()> Public Class ImportantDate : Inherits Record

    Private m_birthDate As Date
    Private m_anniversaryDate As Date
    Private m_comments As String

    Sub New(id As UInteger, birthdate As Date?, anniversaryDate As Date?, comments As String)
        ' TODO: Complete member initialization 
        Me.ID = id
        Me.birthDate = birthdate
        Me.anniversaryDate = anniversaryDate
        Me.comments = comments
    End Sub

    Sub New(line As String)
        Dim fields() As String = line.Split(",")
        If fields.Length = fieldcount Then
            Me.ID = Convert.ToUInt32(fields(0))
            Me.birthDate = Convert.ToDateTime(fields(1))
            Me.anniversaryDate = Convert.ToDateTime(fields(2))
            Me.comments = fields(3)
        Else
            Throw New InvalidDataException("File does not contain valid data")
        End If
    End Sub

    Protected Overrides ReadOnly Property fieldcount As UShort
        Get
            Return 4
        End Get
    End Property

    Public Property birthDate As Date?
        Get
            If m_birthDate = Date.MinValue Then
                Return Nothing
            Else
                Return m_birthDate
            End If
        End Get
        Set(value As Date?)
            If (value Is Nothing) Then
                m_birthDate = Date.MinValue
            Else
                If value > Today Then Throw New ArgumentException("Our current understanding of reality does not allow for time travel; please only include people born in the past")
                m_birthDate = value
            End If
        End Set
    End Property

    Public Property anniversaryDate As Date?
        Get
            If m_anniversaryDate = Date.MinValue Then
                Return Nothing
            Else
                Return m_anniversaryDate
            End If
        End Get
        Set(value As Date?)
            If (value Is Nothing) Then
                m_anniversaryDate = Date.MinValue
            Else
                If value > Today Then Throw New ArgumentException("You're a jerk.")
                m_anniversaryDate = value
            End If
        End Set
    End Property
    Public Property comments As String
        Get
            Return m_comments
        End Get
        Set(value As String)
            m_comments = value
        End Set
    End Property


    Public Overrides Function tostring() As String
        Return Me.ID & "," & Me.birthDate & "," & Me.anniversaryDate & "," & Me.comments
    End Function
End Class
