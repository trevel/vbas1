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
    Dim customerbook As New CustomerBook()

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
                    CustomerDisplay()
                Case 2
                    CustomerAdd()
                Case 3
                    CustomerEdit()
                Case 4
                    CustomerRemove()
            End Select
        Loop
    End Sub

    Private Sub CustomerDisplay()
        Console.WriteLine(customerbook.tostring())
    End Sub

    Private Sub CustomerAdd()
        Dim input As String
        Dim credit_limit As Double = 0
        Console.WriteLine("Enter Customer Name")
        Dim name As String = Console.ReadLine().Trim()
        Console.WriteLine("Enter Customer Email Address")
        Dim email As String = Console.ReadLine().Trim()
        Console.WriteLine("Enter Customer Phone number")
        Dim phone_number As String = Console.ReadLine().Trim()
        Console.WriteLine("Enter Customer Mailing Address")
        Dim mail_address As String = Console.ReadLine().Trim()
        Console.WriteLine("Enter Customer Shipping Address")
        Dim ship_address As String = Console.ReadLine().Trim()
        Console.WriteLine("Enter Customer Credit Limit")
        Do
            input = Console.ReadLine().Trim()
            Try
                credit_limit = Convert.ToDouble(input)
                Exit Do
            Catch ex As FormatException
                Console.WriteLine("Please enter a number for credit limit")
            End Try
        Loop

        ' LAURIE :: TODO need to construct the address objects...all we have is a string from the console
        Dim mailing_address As Address = Nothing
        Dim shipping_address As Address = Nothing

        ' LAURIE :: TODO - fix the addresses
        Try
            customerbook.Add(New Customer(name, email, 1, 1, phone_number, credit_limit))
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Private Sub CustomerEdit()
        Dim results As List(Of String) = Nothing
        Dim searchString As String = GetSearchString("Enter the customer name (Regular expressions are allowed)")
        Console.WriteLine("Implement customer edit functionality!")
        ' LAURIE - TODO - need to deal with searchfield here
        ' results = customerbook.Search(searchString, searchField)
        If results Is Nothing OrElse results.Count = 0 Then
            Console.WriteLine("No records found")
            Exit Sub
        End If
        Dim recordtoedit As Short = GetChoice("Customer Search Results", results.ToArray)
        If recordtoedit = -1 Then
            Exit Sub
        End If
        Dim record As Customer = customerbook.GetByID(results(recordtoedit - 1).Split(",")(0))
        If record Is Nothing Then
            Console.WriteLine("Customer record not found -- something went wrong")
        Else
            Dim input As String
            Console.WriteLine(results(recordtoedit - 1))
            Select Case GetChoice("Edit Customer", {"Edit Name", "Edit Email Address", "Edit Phone Number", "Edit Mailing Address", "Edit Shipping Address", "Edit Credit Limit", "Exit"})
                Case -1, 7
                    Exit Sub
                Case 1
                    Console.WriteLine("Current name: " + record.name.ToString)
                    record.name = Console.ReadLine().Trim()
                Case 2
                    Console.WriteLine("Current email address: " + record.email.ToString)
                    record.email = Console.ReadLine().Trim()
                Case 3
                    Console.WriteLine("Current phone number: " + record.phone_number.ToString)
                    record.phone_number = Console.ReadLine().Trim()
                Case 4
                    Console.WriteLine("Current mailing address :" + record.mailing_address.ToString)
                    ' LAURIE :: TODO
                    record.mailing_address = Nothing ' New Address(Console.ReadLine().Trim())
                Case 5
                    Console.WriteLine("Current shipping address: " + record.shipping_address.ToString)
                    ' LAURIE :: TODO
                    record.shipping_address = Nothing ' New Address(Console.ReadLine().Trim())
                Case 6
                    Console.WriteLine("Current credit limit: " + record.credit_limit.ToString)
                    Do
                        input = Console.ReadLine().Trim()
                        Try
                            record.credit_limit = Convert.ToDouble(input)
                            Exit Do
                        Catch ex As FormatException
                            Console.WriteLine("Please enter a number for credit limit")
                        End Try
                    Loop
            End Select
        End If
    End Sub

    Private Sub CustomerRemove()
        Dim results As List(Of String) = Nothing
        Dim searchString As String = GetSearchString("Enter the customer name (Regular expressions are allowed)")
        ' LAURIE :: TODO - need to deal with searchfield here
        'results = customerbook.Search(searchString, searchField)
        If results Is Nothing OrElse results.Count = 0 Then
            Console.WriteLine("No records found")
            Exit Sub
        End If
        Dim recordtoedit As Short = GetChoice("Customer delete menu", results.ToArray)
        If recordtoedit = -1 Then
            Exit Sub
        End If
        Dim record As Customer = customerbook.GetByID(results(recordtoedit - 1).Split(",")(0))
        If record Is Nothing Then
            Console.WriteLine("Customer record not found -- something went wrong")
        Else
            customerbook.Remove(record)
            Console.WriteLine("Implement customer delete functionality!")
        End If
    End Sub

    Private Sub ProductMenu()
        Do
            Select Case GetChoice("Product Menu", {"Display All Products", "Add", "Edit", "Delete", "Return to Main Menu"})
                Case -1, 5
                    Exit Sub
                Case 1
                    ProductDisplay()
                Case 2
                    ProductAdd()
                Case 3
                    ProductEdit()
                Case 4
                    ProductRemove()
            End Select
        Loop
    End Sub

    Private Sub ProductDisplay()
        Console.WriteLine(productbook.tostring())
    End Sub

    Private Sub ProductAdd()
        Console.WriteLine("Implement product add functionality!")
    End Sub

    Private Sub ProductEdit()
        Console.WriteLine("Implement product edit functionality!")
    End Sub

    Private Sub ProductRemove()
        Console.WriteLine("Implement product delete functionality!")
    End Sub

    Private Sub OrderMenu()
        Do
            Select Case GetChoice("Order Menu", {"Display all Orders", "Add", "Edit", "Delete", "Ship", "Return to Main Menu"})
                Case -1, 6
                    Exit Sub
                Case 1
                    OrderDisplay()
                Case 2
                    OrderAdd()
                Case 3
                    OrderEdit()
                Case 4
                    OrderRemove()
                Case 5
                    OrderShip()
            End Select
        Loop
    End Sub

    Private Sub OrderDisplay()
        Console.WriteLine(orderbook.tostring())
    End Sub

    Private Sub OrderAdd()
        Console.WriteLine("Implement order add functionality!")
    End Sub

    Private Sub OrderEdit()
        Console.WriteLine("Implement order edit functionality!")
    End Sub

    Private Sub OrderRemove()
        Console.WriteLine("Implement order delete functionality!")
    End Sub

    Private Sub OrderShip()
        Console.WriteLine("Implement order ship functionality!")
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

    ' Gets user input for what search string they want to search by
    Private Function GetSearchString(msg As String) As String
        Dim inputline As String
        Do
            If msg Is Nothing Then
                Console.WriteLine("Enter the search string (Regular expressions are allowed)")
            Else
                Console.WriteLine(msg)
            End If
            inputline = Console.ReadLine()
            If inputline <> "" Then Return inputline
        Loop
    End Function

    Private Function LoadData() As Int16
        ' read the various books from soap and csv
        Try
            Select Case (GetChoice("Load Menu", {"Load from CSV", "Load from XML"}))
                Case -1
                    Return -1
                Case 1
                    customerbook.Load(CUSTOMER_CSV_PATH)
                    productbook.Load(PRODUCTS_CSV_PATH)
                    orderbook.Load(ORDERS_CSV_PATH)
                Case 2
                    customerbook.Load(CUSTOMER_XML_PATH)
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
        If Not customerbook Is Nothing Then
            customerbook.SaveCSV(CUSTOMER_CSV_PATH)
            customerbook.SaveXML(CUSTOMER_XML_PATH)
        End If
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
