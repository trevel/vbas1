Imports System.Text.RegularExpressions
Imports CSLib

<Serializable()> Public MustInherit Class Record : Implements IValidator : Implements IRecord

    Protected m_ID As UInteger

    Public Sub New()

    End Sub

    Public Function GetID() As Integer Implements IRecord.GetID
        Return Me.ID
    End Function

    Public Property ID As Integer
        Get
            Return m_ID
        End Get
        Set(value As Integer)
            m_ID = value
        End Set
    End Property

    Protected Function IsValid(testData As String, searchstring As String) As Boolean Implements IValidator.IsValid
        Return Regex.IsMatch(testData, searchstring)
    End Function


End Class
