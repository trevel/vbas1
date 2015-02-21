' Laurie is working on this!
' '*****************************************************************************************
' Student Names: Laurie Shields (034448142)
'                Mark Lindan (063336143)
' CVB815 - Assign1.vb
' Last Updated On: Feb 21, 2015
'*******************************************************************************************
Imports Database
Imports System.IO
Public Module Assign1
    ' Set up the paths for all the files - this could possibly be owned by each book as a static?
    Const CUSTOMER_CSV_PATH = "..\..\..\customers.csv"
    Const PRODUCTS_CSV_PATH = "..\..\..\products.csv"
    Const ORDERS_CSV_PATH = "..\..\..\orders.csv"
    Const CUSTOMER_XML_PATH = "..\..\..\customers.xml"
    Const PRODUCTS_XML_PATH = "..\..\..\products.xml"
    Const ORDERS_XML_PATH = "..\..\..\orders.xml"

    ' Allocate the books
    Dim orderbook As New OrderBook()
    Dim productbook As New ProductBook()
    'Dim customerbook As New CustomerBook()

    Sub Main()
        If LoadData() <> -1 Then
            MainMenu()
        End If
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
            Select Case GetChoice("Customer Menu", {"Display All Customers", "Add", "Edit", "Delete", "Return to Main Menu"})
                Case -1, 5
                    Exit Sub
                Case 1
                    Console.WriteLine("Implement Display all customers functionality!")
                    ' Console.WriteLine(customerbook.tostring())
                Case 2
                    Console.WriteLine("Implement customer add functionality!")
                Case 3
                    Console.WriteLine("Implement customer edit functionality!")
                Case 4
                    Console.WriteLine("Implement customer delete functionality!")
            End Select
        Loop
    End Sub

    Private Sub ProductMenu()
        Do
            Select Case GetChoice("Product Menu", {"Display All Products", "Add", "Edit", "Delete", "Return to Main Menu"})
                Case -1, 5
                    Exit Sub
                Case 1
                    Console.WriteLine(productbook.tostring())
                Case 2
                    Console.WriteLine("Implement product add functionality!")
                Case 3
                    Console.WriteLine("Implement product edit functionality!")
                Case 4
                    Console.WriteLine("Implement product delete functionality!")
            End Select
        Loop
    End Sub

    Private Sub OrderMenu()
        Do
            Select Case GetChoice("Order Menu", {"Display all Orders", "Add", "Edit", "Delete", "Ship", "Return to Main Menu"})
                Case -1, 6
                    Exit Sub
                Case 1
                    Console.WriteLine(orderbook.tostring())
                Case 2
                    Console.WriteLine("Implement order add functionality!")
                Case 3
                    Console.WriteLine("Implement order edit functionality!")
                Case 4
                    Console.WriteLine("Implement order delete functionality!")
                Case 5
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

    Private Function LoadData() As Int16
        ' read the various books from soap and csv
        Try
            Select Case (GetChoice("Load Menu", {"Load from CSV", "Load from XML"}))
                Case -1
                    Return -1
                Case 1
                    ' customerbook.Load(CUSTOMER_CSV_PATH)
                    productbook.Load(PRODUCTS_CSV_PATH)
                    orderbook.Load(ORDERS_CSV_PATH)
                Case 2
                    ' customerbook.Load(CUSTOMER_XML_PATH)
                    productbook.Load(PRODUCTS_XML_PATH)
                    orderbook.Load(ORDERS_XML_PATH)
            End Select
        Catch e As InvalidDataException
            Console.WriteLine("Invalid data file format") ' LAURIE - TODO - add name of file here!!!
            Console.ReadKey()
            Return -1
        Catch e As IOException
            Console.WriteLine("Error reading file - File may not exist")
            Return -1
        End Try
        Return 0
    End Function


    Private Sub SaveData()
        ' write out the various books to csv and soap
        Console.WriteLine("Serialize all the books and save to csv files")
        ' If Not customerbook Is Nothing Then
        '   book.SaveCSV(CUSTOMER_CSV_PATH)
        '   book.SaveXML(CUSTOMER_XML_PATH)
        ' End If
        If Not productbook Is Nothing Then
            productbook.SaveCSV(PRODUCTS_CSV_PATH)
            productbook.SaveXML(PRODUCTS_XML_PATH)
        End If
        If Not orderbook Is Nothing Then
            orderbook.SaveCSV(ORDERS_CSV_PATH)
            orderbook.SaveXML(ORDERS_XML_PATH)
        End If
    End Sub
End Module
