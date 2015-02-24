' '*****************************************************************************************
' Student Names: Laurie Shields (034448142)
'                Mark Lindan (063336143)
' CVB815 - Record.vb
' Last Updated On: Feb 21, 2015
'*******************************************************************************************
Imports System.Text.RegularExpressions
Imports CSLib

<Serializable()> Public MustInherit Class Record : Implements IValidator : Implements IRecord

    Protected m_ID As UInteger
    Protected MustOverride ReadOnly Property fieldcount As UInt16

    Public MustOverride Sub InterpretCSV(csv As String)

    Public MustOverride Function GetCSV() As String Implements IRecord.GetCSV

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

    Public Property active As Boolean = False

    Protected Function IsValid(testData As String, searchstring As String) As Boolean Implements IValidator.IsValid
        Return Regex.IsMatch(testData, searchstring)
    End Function

    Public Overrides Function ToString() As String
        Return Me.ID & "::" & GetCSV()
    End Function
End Class
