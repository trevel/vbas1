﻿' Laurie is working on this!
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
    Const ADDRESS_CSV_PATH = "..\..\..\address.csv"
    Const ORDERLINE_CSV_PATH = "..\..\..\orderline.csv"
    Const CUSTOMER_XML_PATH = "..\..\..\customers.xml"
    Const PRODUCTS_XML_PATH = "..\..\..\products.xml"
    Const ORDERS_XML_PATH = "..\..\..\orders.xml"
    Const ADDRESS_XML_PATH = "..\..\..\address.xml"
    Const ORDERLINE_XML_PATH = "..\..\..\orderline.xml"


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
        credit_limit = GetDouble("Credit Limit")

        ' LAURIE :: TODO need to construct the address objects...all we have is a string from the console
        Dim mailing_address As Address = Nothing
        Dim shipping_address As Address = Nothing

        ' LAURIE :: TODO - fix the addresses
        Try
            customerbook.Add(New Customer(customerbook.next_id, name, email, 1, 1, phone_number, credit_limit))
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Private Sub CustomerEdit()
        Dim lines() As String = customerbook.tostring().Split(Environment.NewLine)
        If lines.Count = 0 Then
            Console.WriteLine("No records found")
            Exit Sub
        End If
        Dim recordtoedit As Short = GetChoice("Customer to Edit", lines)
        If recordtoedit = -1 Then
            Exit Sub
        End If
        Dim record As Customer = customerbook.GetByID(lines(recordtoedit - 1).Split(",")(0))
        If record Is Nothing Then
            Console.WriteLine("Customer record not found -- something went wrong")
        Else
            Do
                Console.WriteLine(record.ToString)
                Try
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
                            record.credit_limit = GetDouble("Credit Limit")
                    End Select
                Catch ex As ArgumentException
                    Console.WriteLine(ex.Message)
                End Try
            Loop
        End If
    End Sub

    Private Sub CustomerRemove()
        Dim lines() As String = customerbook.tostring().Split(Environment.NewLine)
        If lines.Count = 0 Then
            Console.WriteLine("No records found")
            Exit Sub
        End If
        Dim recordtoedit As Short = GetChoice("Customer to Remove", lines)
        If recordtoedit = -1 Then
            Exit Sub
        End If
        Dim record As Customer = customerbook.GetByID(lines(recordtoedit - 1).Split(",")(0))
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
        Dim price As Double = 0
        Dim inv As Integer = 0
        Console.WriteLine("Enter Product Description")
        Dim desc As String = Console.ReadLine().Trim()
        Console.WriteLine("Enter Product Price")
        price = GetDouble("Product Price")
        Console.WriteLine("Enter Product Inventory")
        inv = GetInteger("Product Inventory")
        Try
            productbook.Add(New Product(productbook.next_id, desc, price, inv))
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Private Sub ProductEdit()
        Dim lines() As String = productbook.tostring().Split(Environment.NewLine)
        If lines.Count = 0 Then
            Console.WriteLine("No records found")
            Exit Sub
        End If
        Dim recordtoedit As Short = GetChoice("Product to Edit", lines)
        If recordtoedit = -1 Then
            Exit Sub
        End If
        Dim record As Product = productbook.GetByID(lines(recordtoedit - 1).Split(",")(0))
        If record Is Nothing Then
            Console.WriteLine("Product record not found -- something went wrong")
        Else
            Do
                Console.WriteLine(record.ToString)
                Try
                    Select Case GetChoice("Edit Product", {"Edit Description", "Edit Price", "Edit Inventory", "Exit"})
                        Case -1, 4
                            Exit Sub
                        Case 1
                            Console.WriteLine("Current Description: " + record.Description.ToString)
                            record.Description = Console.ReadLine().Trim()
                        Case 2
                            Console.WriteLine("Current price: " + record.Price.ToString("$0.00"))
                            record.Price = GetDouble("Product Price")
                        Case 3
                            Console.WriteLine("Current Inventory: " + record.Inventory.ToString)
                            record.Inventory = GetInteger("Product Inventory")
                    End Select
                Catch ex As ArgumentException
                    Console.WriteLine(ex.Message)
                End Try
            Loop
        End If
    End Sub

    Private Sub ProductRemove()
        Dim lines() As String = productbook.tostring().Split(Environment.NewLine)
        If lines.Count = 0 Then
            Console.WriteLine("No records found")
            Exit Sub
        End If
        Dim recordtoedit As Short = GetChoice("Product to Remove", lines)
        If recordtoedit = -1 Then
            Exit Sub
        End If
        Dim record As Product = productbook.GetByID(lines(recordtoedit - 1).Split(",")(0))
        If record Is Nothing Then
            Console.WriteLine("Product record not found -- something went wrong")
        Else
            productbook.Remove(record)
        End If
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
        Dim input As String
        Dim odate As Date = Today
        Dim qty As UInteger
        Dim disc As Double = 0
        Dim can_ship As Boolean = False
        Dim order As Order = Nothing
        Dim items As New ArrayList

        ' get a list of customers - and make sure there are some
        Dim customers() As String = customerbook.tostring().Split(Environment.NewLine)
        If customers.Count = 0 Then
            Console.WriteLine("No customers found. You need to add some customers before creating orders.")
            Exit Sub
        End If

        ' get a list of products and make sure there are some
        Dim products() As String = productbook.tostring().Split(Environment.NewLine)
        If products.Count = 0 Then
            Console.WriteLine("No products found. You need to add some products before creating orders.")
            Exit Sub
        End If

        ' Let them pick a customer for the order
        Dim cust_choice As Short = GetChoice("Choose a Customer", customers)
        If cust_choice = -1 Then
            Exit Sub
        End If
        Dim cust_record As Customer = customerbook.GetByID(customers(cust_choice - 1).Split(",")(0))
        If cust_record Is Nothing Then
            Console.WriteLine("Customer record not found -- something went wrong")
            Exit Sub
        End If

        ' set the order date
        Console.WriteLine("Order Date 'yyyy-MM-dd': ")
        If odate = GetDate("Order Date") Is Nothing Then
            odate = Today
        End If

        Do While items.Count <= 10
            ' now they need to pick some items1
            Dim product_choice As Short = GetChoice("Choose a Product", products)
            If product_choice = -1 Then
                Exit Do
            End If
            Dim prod_record As Product = productbook.GetByID(products(product_choice - 1).Split(",")(0))
            If prod_record Is Nothing Then
                Console.WriteLine("Product record not found -- something went wrong")
            Else
                Console.WriteLine("How many would you like at $" & prod_record.Price.ToString("0.00") & " each?")
                qty = GetInteger("Quantity")
                Try
                    items.Add(New OrderItem(prod_record.GetID, qty))
                    If qty <= prod_record.Inventory Then
                        can_ship = True
                    End If
                Catch ex As Exception
                    Console.WriteLine(ex.Message)
                End Try
            End If
        Loop
        Console.WriteLine("Enter discount %: ")
        disc = GetDouble("Discount")

        Try
            order = New Order(orderbook.next_id, cust_record.GetID(), odate, disc, items)
            orderbook.Add(order)
            If can_ship = True Then
                Console.WriteLine("Some items are available to ship. Do you want to go ahead with shipping all available items? (Y/N)")
                input = Console.ReadLine().Trim().ToUpper()
                If input = "Y" AndAlso Not order Is Nothing Then
                    order.Ship()
                End If
            End If
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try


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

    Function GetDate(message As String) As Date?
        Console.WriteLine("Please enter " + message + " date or N to not enter a date.")
        Dim line As String = ""
        Dim result As Date
        Do While line = ""
            line = Console.ReadLine()
            If line.Substring(0, 1).ToUpper() = "N" Then Return Nothing
            Try
                result = Date.ParseExact(line, "yyyy-MM-dd", Nothing)
                Return result
            Catch ex As FormatException
                Console.WriteLine("I didn't understand that date; enter N to not enter a date, or the " + message + " date in yyyy-mm-dd format.")
                line = ""
            End Try
        Loop
    End Function

    Private Function GetInteger(msg As String) As Integer
        Dim input As String
        Do
            Input = Console.ReadLine().Trim()
            Try
                Return Integer.Parse(input)
            Catch ex As FormatException
                Console.WriteLine("Please enter a number for " & msg)
            End Try
        Loop
    End Function

    Private Function GetDouble(msg As String) As Double
        Dim input As String
        Do
            input = Console.ReadLine().Trim()
            Try
                Return Convert.ToDouble(input)
            Catch ex As FormatException
                Console.WriteLine("Please enter a number for " & msg)
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
                    customerbook = customerbook.Load(CUSTOMER_CSV_PATH)
                    productbook = productbook.Load(PRODUCTS_CSV_PATH)
                    orderbook = orderbook.Load(ORDERS_CSV_PATH)
                Case 2
                    customerbook = customerbook.Load(CUSTOMER_XML_PATH)
                    productbook = productbook.Load(PRODUCTS_XML_PATH)
                    orderbook = orderbook.Load(ORDERS_XML_PATH)
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
        'If Not AddressBook Is Nothing Then
        '    AddressBook.SaveCSV(ADDRESS_CSV_PATH)
        '    AddressBook.SaveXML(ADDRESS_XML_PATH)
        'End If
        'If Not OrderLineBook Is Nothing Then
        '    OrderLineBook.SaveCSV(ORDERLINE_CSV_PATH)
        '    OrderLineBook.SaveXML(ORDERLINE_XML_PATH)
        'End If
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
