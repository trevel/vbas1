﻿' '*****************************************************************************************
' Student Names: Laurie Shields (034448142)
'                Mark Lindan (063336143)
' CVB815 - Book.vb
' Last Updated On: Feb 21, 2015
'*******************************************************************************************
Imports System.IO
Imports CSLib

<Serializable()> Public MustInherit Class Book(Of T As IRecord)

    Protected Book As New ArrayList
    Protected header As String


    Public Sub New()

    End Sub

    Public Event NewEntry(item As T)

    Public Sub Add(item As T)
        Book.Add(item)
    End Sub

    Public Sub Remove(item As T)
        Book.Remove(item)
    End Sub

    Public Function GetByID(id As Integer) As T
        For Each item As T In Book
            If item.GetID() = id Then Return item
        Next
        Return Nothing
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
                sw.WriteLine(item.ToString)
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

    Public Overrides Function tostring() As String
        If Book.Count = 0 Then Return "- Empty -"
        Dim s As String = header
        For Each item In Book
            s &= Environment.NewLine & item.ToString()
        Next
        Return s
    End Function
End Class
