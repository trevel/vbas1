' '*****************************************************************************************
' Student Names: Laurie Shields (034448142)
'                Mark Lindan (063336143)
' CVB815 - Book.vb
' Last Updated On: Feb 21, 2015
'*******************************************************************************************
Imports System.IO
Imports CSLib
Imports System.Text.RegularExpressions
Imports System.Text

<Serializable()> Public MustInherit Class Book(Of T As IRecord)

    Protected Book As New ArrayList
    Protected header As String
    Protected _next_id As Int16 = 1

    Public Sub New()

    End Sub

    Public Property next_id As Int16
        Get
            Return _next_id
        End Get
        Set(value As Int16)
            _next_id = value
            If value >= _next_id Then
                _next_id = value + 1
            End If
        End Set
    End Property

    Public Overridable Sub Add(item As T)
        Book.Add(item)
        next_id = item.GetID
    End Sub

    Public Overridable Sub Remove(item As T)
        Book.Remove(item)
    End Sub

    Public Function GetByID(id As Integer?) As T
        If id Is Nothing Then Return Nothing
        For Each item As T In Book
            If item.GetID() = id Then Return item
        Next
        Return Nothing
    End Function

    Public Overridable Function GetCSVBySearch(pattern As String) As String()
        Dim result As New List(Of String)
        For Each item As T In Book
            Dim record As String = item.GetCSV()
            If Regex.IsMatch(record, pattern) Then
                result.Add(record)
            End If
        Next
        Return result.ToArray()
    End Function

    Public Function Load(path As String) As Book(Of T)
        If Not File.Exists(path) Then Throw New FileNotFoundException(path + " not found")
        If path.ToUpper().EndsWith(".CSV") Then
            Return LoadCSV(path)
        ElseIf path.ToUpper().EndsWith(".XML") Then
            Return LoadXML(path)
        Else
            Throw New FileLoadException("Not a parseable filetype")
        End If
    End Function
    Protected Function LoadCSV(Path As String) As Book(Of T)
        Dim line As String = Nothing

        Using sr As New StreamReader(Path)
            ' first line is the header; for some reason we're trusting that it's right, and we're reading the right file. We like to live dangerously
            header = sr.ReadLine()

            ' Read the first line of data
            line = sr.ReadLine

            ' Loop over each line in file
            Do While (Not line Is Nothing)
                If line.Trim() <> "" Then
                    Try
                        Interpret(line)
                    Catch e As ArgumentException
                        Console.WriteLine(e.Message)
                    End Try
                End If

                ' Read in the next line.
                line = sr.ReadLine
            Loop
            Return Me
        End Using
    End Function

    Protected MustOverride Sub Interpret(line As String)

    Public Sub SaveCSV(Path As String)
        Using sw As New StreamWriter(Path)
            ' first line is the header; good thing we saved it earlier, huh?
            sw.WriteLine(header)
            For Each item As T In Book
                Dim csv As String = item.GetCSV
                If csv IsNot Nothing Then
                    sw.WriteLine(item.GetCSV)
                End If
            Next
        End Using
    End Sub

    Public Sub SaveXML(Path As String)
        Using fs As New FileStream(Path, FileMode.OpenOrCreate)
            Dim f As New System.Runtime.Serialization.Formatters.Soap.SoapFormatter()
            f.Serialize(fs, Me)
        End Using
    End Sub

    Public Shared Function LoadXML(Path As String) As Book(Of T)
        Using fs As New FileStream(Path, FileMode.Open)
            Dim f As New System.Runtime.Serialization.Formatters.Soap.SoapFormatter()
            Return f.Deserialize(fs)
        End Using
        Return Nothing
    End Function

    ' Used for display purposes only
    Public Overrides Function tostring() As String
        If Book.Count = 0 Then Return "- Empty -"
        Dim Buffer As String = ""
        Dim s As New StringBuilder
        '    s.Append(header)
        For Each item In Book
            s.Append(Buffer)
            Buffer = vbNewLine
            s.Append(item.ToString())
        Next
        Return s.ToString()
    End Function
End Class
