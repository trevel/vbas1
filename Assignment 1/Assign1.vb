' Laurie is working on this! 
Imports System.IO
Public Module Assign1

    Sub Main()
        LoadData()
        MainMenu()
        Console.WriteLine("Press almost any key to continue")
        Console.ReadKey()
        Console.Clear()

    End Sub

    Private Function MainMenu() As Int16
        Do
            Select Case GetChoice("Main Menu", {"Customers", "Products", "Orders", "Save", "Quit"})
                Case -1, 5
                    Return -1
                Case 1
                    CustomerMenu()
                Case 2
                    ProductMenu()
                Case 3
                    OrderMenu()
                Case 4
                    SaveData()
            End Select
        Loop
    End Function

    Private Sub CustomerMenu()
        Do
            Select Case GetChoice("Customer Menu", {"Add", "Edit", "Delete", "Return to Main Menu"})
                Case -1, 4
                    Exit Sub
                Case 1
                    Console.WriteLine("Implement customer add functionality!")
                Case 2
                    Console.WriteLine("Implement customer edit functionality!")
                Case 3
                    Console.WriteLine("Implement customer delete functionality!")
            End Select
        Loop
    End Sub

    Private Sub ProductMenu()
        Do
            Select Case GetChoice("Product Menu", {"Add", "Edit", "Delete", "Return to Main Menu"})
                Case -1, 4
                    Exit Sub
                Case 1
                    Console.WriteLine("Implement product add functionality!")
                Case 2
                    Console.WriteLine("Implement product edit functionality!")
                Case 3
                    Console.WriteLine("Implement product delete functionality!")
            End Select
        Loop
    End Sub

    Private Sub OrderMenu()
        Do
            Select Case GetChoice("Order Menu", {"Add", "Edit", "Delete", "Ship", "Return to Main Menu"})
                Case -1, 5
                    Exit Sub
                Case 1
                    Console.WriteLine("Implement order add functionality!")
                Case 2
                    Console.WriteLine("Implement order edit functionality!")
                Case 3
                    Console.WriteLine("Implement order delete functionality!")
                Case 4
                    Console.WriteLine("Implement order ship functionality!")
            End Select
        Loop
    End Sub

    Private Function GetChoice(title As String, choices As String()) As Int16
        Dim inputline As String
        Dim i As Integer = 0
        Console.WriteLine(title)
        Console.WriteLine("----------------")
        For i = 1 To choices.Length
            Console.WriteLine(i & " - " & choices(i - 1))
        Next
        Console.WriteLine("----------------")

        Do
            Console.WriteLine("Enter your choice (or Q to quit)")
            inputline = Console.ReadLine().ToUpper()
            If inputline = "Q" Then Return -1
            Try
                Dim choice = Convert.ToInt16(inputline)
                If choice <= choices.Length And choice > 0 Then
                    Return choice
                End If
            Catch ex As FormatException
                Console.WriteLine("Invalid Choice")
            End Try
        Loop
    End Function

    Private Sub LoadData()
        ' read the various books from soap and csv
        Console.WriteLine("DeSerialize all the books and load from csv files")
    End Sub

    Private Sub SaveData()
        ' write out the various books to csv and soap
        Console.WriteLine("Serialize all the books and save to csv files")
    End Sub
End Module
