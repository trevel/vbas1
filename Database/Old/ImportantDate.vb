Imports CSLib

<Serializable()> Public Class ImportantDate : Inherits Record

    Const fieldcount = 4
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
