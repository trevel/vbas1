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
    Const ADDRESS_CSV_PATH = "..\..\..\address.csv"
    Const ORDERLINE_CSV_PATH = "..\..\..\orderline.csv"
    Const CUSTOMER_XML_PATH = "..\..\..\customers.xml"
    Const PRODUCTS_XML_PATH = "..\..\..\products.xml"
    Const ORDERS_XML_PATH = "..\..\..\orders.xml"
    Const ADDRESS_XML_PATH = "..\..\..\address.xml"
    Const ORDERLINE_XML_PATH = "..\..\..\orderline.xml"


    ' Allocate the books

    Dim customerbook As New CustomerBook()
    Dim productbook As New ProductBook()
    Dim WithEvents orderbook As New OrderBook()
    Dim WithEvents orderitembook As New OrderItemBook()
    Dim WithEvents addressbook As New AddressBook()


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
        ' "Add a mailing address? (Y/N)" 
        ' "Add a shipping address? (Y/N)" 
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
        Dim tempStr As String = customerbook.tostring()
        If tempStr = "- Empty -" Then
            Console.WriteLine("No records found")
            Exit Sub
        End If
        Dim lines() As String = tempStr.Split(Environment.NewLine)
        If lines.Count = 0 Then
            Console.WriteLine("No records found")
            Exit Sub
        End If
        Dim recordtoedit As Short = GetChoiceID("Customer to Edit", lines)
        If recordtoedit = -1 Then
            Exit Sub
        End If
        Dim record As Customer = customerbook.GetByID(recordtoedit)
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
                            Console.WriteLine("Current mailing address :" + addressbook.GetByID(record.mailing_address_id).ToString)
                            ' LAURIE :: TODO
                            record.mailing_address_id = Nothing ' New Address(Console.ReadLine().Trim())
                        Case 5
                            Console.WriteLine("Current shipping address: " + addressbook.GetByID(record.shipping_address_id).ToString)
                            ' LAURIE :: TODO
                            record.shipping_address_id = Nothing ' New Address(Console.ReadLine().Trim())
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
        Dim tempStr As String = customerbook.tostring()
        If tempStr = "- Empty -" Then
            Console.WriteLine("No records found")
            Exit Sub
        End If
        Dim lines() As String = tempStr.Split(Environment.NewLine)
        If lines.Count = 0 Then
            Console.WriteLine("No records found")
            Exit Sub
        End If
        Dim recordtoedit As Short = GetChoiceID("Customer to Remove", lines)
        If recordtoedit = -1 Then
            Exit Sub
        End If
        Dim record As Customer = customerbook.GetByID(recordtoedit)
        If record Is Nothing Then
            Console.WriteLine("Customer record not found -- something went wrong")
        Else
            If orderbook.IsOrderForCust(record.ID) = False Then
                customerbook.Remove(record)
            Else
                Console.WriteLine("Customer cannot be removed due to Existing Orders")
            End If
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
        Dim tempStr As String = productbook.tostring()
        If tempStr = "- Empty -" Then
            Console.WriteLine("No records found")
            Exit Sub
        End If
        Dim lines() As String = tempStr.Split(Environment.NewLine)
        If lines.Count = 0 Then
            Console.WriteLine("No records found")
            Exit Sub
        End If
        Dim recordtoedit As Short = GetChoiceID("Product to Edit", lines)
        If recordtoedit = -1 Then
            Exit Sub
        End If
        Dim record As Product = Nothing
        Try
            record = productbook.GetByID(recordtoedit)
        Catch e As InvalidCastException
        End Try
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
        Dim tempStr As String = productbook.tostring()
        If tempStr = "- Empty -" Then
            Console.WriteLine("No records found")
            Exit Sub
        End If
        Dim lines() As String = tempStr.Split(Environment.NewLine)
        If lines.Count = 0 Then
            Console.WriteLine("No records found")
            Exit Sub
        End If
        Dim recordtoedit As Short = GetChoiceID("Product to Remove", lines)
        If recordtoedit = -1 Then
            Exit Sub
        End If
        Dim record As Product = productbook.GetByID(recordtoedit)
        If record Is Nothing Then
            Console.WriteLine("Product record not found -- something went wrong")
        Else
            If orderitembook.IsOrderItemForProduct(record.ID) = False Then
                productbook.Remove(record)
            Else
                Console.WriteLine("Product cannot be removed due to Existing Orders")
            End If
        End If
    End Sub

    Private Sub OrderMenu()
        Do
            Select Case GetChoice("Order Menu", {"Display all Orders", "Add", "Edit", "Delete", "Ship", "View Order Detail", "Return to Main Menu"})
                Case -1, 7
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
                Case 6
                    OrderViewDetail()
            End Select
        Loop
    End Sub

    Private Sub OrderDisplay()
        Console.WriteLine(orderbook.tostring())
    End Sub

    Private Sub OrderViewDetail()
        Dim tempStr As String = orderbook.tostring()
        If tempStr = "- Empty -" Then
            Console.WriteLine("No records found")
            Exit Sub
        End If
        Dim lines() As String = tempStr.Split(Environment.NewLine)
        If lines.Count = 0 Then
            Console.WriteLine("No records found")
            Exit Sub
        End If
        Dim recordtoedit As Short = GetChoiceID("Select an order to view:", lines)
        If recordtoedit = -1 Then
            Exit Sub
        End If
        Dim record As Order = orderbook.GetByID(recordtoedit)
        If record Is Nothing Then
            Console.WriteLine("Order not found -- something went wrong")
        Else
            Console.WriteLine(record.ToString())
            Dim orderlines() As String = orderitembook.GetByOrderID(record.ID)
            For Each item As String In orderlines
                Console.WriteLine(item)
            Next
        End If
    End Sub

    Private Sub OrderAdd()
        Dim odate As Date = Today
        Dim qty As UInteger
        Dim disc As Double = 0
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
        Dim cust_choice As Short = GetChoiceID("Choose a Customer", customers)
        If cust_choice = -1 Then
            Exit Sub
        End If
        Dim cust_record As Customer = customerbook.GetByID(cust_choice)
        If cust_record Is Nothing Then
            Console.WriteLine("Customer record not found -- something went wrong")
            Exit Sub
        End If

        ' set the order date
        Console.WriteLine("Order Date 'yyyy-MM-dd': ")
        If odate = GetDate("Order Date") Is Nothing Then
            odate = Today
        End If

        Console.WriteLine("Enter discount %: ")
        disc = GetDouble("Discount")

        Try
            order = New Order(orderbook.next_id, cust_record.GetID(), odate, disc)
            orderbook.Add(order)
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            Exit Sub
        End Try

        ' now they need to pick some items1
        Do While items.Count <= 10
            Dim product_choice As Short = GetChoiceID("Choose a Product", products)
            If product_choice = -1 Then
                Exit Do
            End If
            Dim prod_record As Product = productbook.GetByID(product_choice)
            If prod_record Is Nothing Then
                Console.WriteLine("Product record not found -- something went wrong")
            Else
                Console.WriteLine("How many would you like at $" & prod_record.Price.ToString("0.00") & " each?")
                qty = GetInteger("Quantity")
                Try
                    Dim newItem As New OrderItem(orderitembook.next_id, order.ID, prod_record.GetID, qty)
                    items.Add(newItem)
                    orderitembook.Add(newItem)
                Catch ex As Exception
                    Console.WriteLine(ex.Message)
                End Try
            End If
        Loop
    End Sub

    Private Sub OrderEdit()
        Dim tempStr As String = orderbook.tostring()
        If tempStr = "- Empty -" Then
            Console.WriteLine("No records found")
            Exit Sub
        End If
        Dim lines() As String = tempStr.Split(Environment.NewLine)
        If lines.Count = 0 Then
            Console.WriteLine("No records found")
            Exit Sub
        End If
        Dim recordtoedit As Short = GetChoiceID("Select an order to modify:", lines)
        If recordtoedit = -1 Then
            Exit Sub
        End If
        Dim record As Order = orderbook.GetByID(recordtoedit)
        If record Is Nothing Then
            Console.WriteLine("Order not found -- something went wrong")
        Else
            Do
                Console.WriteLine(record.ToString)
                Try
                    Select Case GetChoice("Modify Order", {"Change Discount", "Add items", "Remove Items"})
                        Case -1, 4
                            Exit Sub
                        Case 1
                            Console.WriteLine("Current discount: " + record.discount.ToString("0.00") + " %")
                            record.discount = GetDouble("Order Discount")
                        Case 2
                            OrderAddItems(record)
                        Case 3
                            OrderRemoveItems(record)
                    End Select
                Catch ex As ArgumentException
                    Console.WriteLine(ex.Message)
                End Try
            Loop

        End If
    End Sub

    Private Sub OrderAddItems(record As Order)
        ' get a list of products and make sure there are some
        Dim products() As String = productbook.tostring().Split(Environment.NewLine)
        If products.Count = 0 Then
            Console.WriteLine("No products found. You need to add some products before creating orders.")
            Exit Sub
        End If
        Do While record.item_count <= 10
            Dim product_choice As Short = GetChoiceID("Choose a Product", products)
            If product_choice = -1 Then
                Exit Do
            End If
            Dim prod_record As Product = productbook.GetByID(product_choice)
            If prod_record Is Nothing Then
                Console.WriteLine("Product record not found -- something went wrong")
            Else
                Console.WriteLine("How many would you like at $" & prod_record.Price.ToString("0.00") & " each?")
                Dim qty As Integer = GetInteger("Quantity")
                Try
                    orderitembook.Add(New OrderItem(orderitembook.next_id, record.ID, prod_record.GetID, qty))
                Catch ex As Exception
                    Console.WriteLine(ex.Message)
                End Try
            End If
        Loop
    End Sub

    Private Sub OrderRemoveItems(record As Order)
        Dim orderlines() As String = orderitembook.GetByOrderID(record.ID)
        If orderlines.Count = 0 Then
            Console.WriteLine("Order currently has no items")
            Exit Sub
        End If
        Dim recordtoedit As Short = GetChoiceID("Select an item to remove:", orderlines)
        If recordtoedit = -1 Then
            Exit Sub
        End If
        Dim item As OrderItem = orderitembook.GetByID(recordtoedit)
        If item Is Nothing Then
            Console.WriteLine("Order item not found -- something went wrong")
        Else
            If item.has_shipped Then
                Console.WriteLine("That item has already shipped and cannot be removed")
            Else
                orderitembook.Remove(item)
            End If
        End If
    End Sub

    Private Sub OrderRemove()
        Dim tempStr As String = orderbook.tostring()
        If tempStr = "- Empty -" Then
            Console.WriteLine("No records found")
            Exit Sub
        End If
        Dim lines() As String = tempStr.Split(Environment.NewLine)
        If lines.Count = 0 Then
            Console.WriteLine("No records found")
            Exit Sub
        End If
        Dim recordtoedit As Short = GetChoiceID("Select an order to delete:", lines)
        If recordtoedit = -1 Then
            Exit Sub
        End If
        Dim record As Order = orderbook.GetByID(recordtoedit)
        If record Is Nothing Then
            Console.WriteLine("Order not found -- something went wrong")
        Else
            If orderitembook.OrderHasShippedItems(record.ID) = True Then
                Console.WriteLine("This order has items that have already been shipped, therefore it cannot be deleted")
            Else
                orderbook.Remove(record)
            End If
        End If
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

    Private Function GetChoiceID(title As String, choices As String()) As Int16
        Dim inputline As String
        Dim i As Integer = 0
        Console.WriteLine(title)
        Console.WriteLine("----------------")
        For i = 1 To choices.Length
            Console.WriteLine(i & " - " & choices(i - 1).Split("::")(2))
        Next
        Console.WriteLine("----------------")

        Do
            Console.WriteLine("Enter your choice (or Q to quit)")
            inputline = Console.ReadLine().ToUpper()
            If inputline = "Q" Then Return -1
            Try
                Dim choice = Convert.ToInt16(inputline)
                If choice <= choices.Length And choice > 0 Then
                    Return choices(choice - 1).Split("::")(0)
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
                    addressbook = addressbook.Load(ADDRESS_CSV_PATH)
                    customerbook = customerbook.Load(CUSTOMER_CSV_PATH)
                    productbook = productbook.Load(PRODUCTS_CSV_PATH)
                    orderbook = orderbook.Load(ORDERS_CSV_PATH)
                    orderitembook = orderitembook.Load(ORDERLINE_CSV_PATH)
                Case 2
                    addressbook = addressbook.Load(ADDRESS_XML_PATH)
                    customerbook = customerbook.Load(CUSTOMER_XML_PATH)
                    productbook = productbook.Load(PRODUCTS_XML_PATH)
                    orderbook = orderbook.Load(ORDERS_XML_PATH)
                    orderitembook = orderitembook.Load(ORDERLINE_XML_PATH)
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
        If Not addressbook Is Nothing Then
            addressbook.SaveCSV(ADDRESS_CSV_PATH)
            addressbook.SaveXML(ADDRESS_XML_PATH)
        End If
        If Not orderitembook Is Nothing Then
            orderitembook.SaveCSV(ORDERLINE_CSV_PATH)
            orderitembook.SaveXML(ORDERLINE_XML_PATH)
        End If
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

    Private Sub NewAddress(item As Address) Handles addressbook.NewAddress
        If item IsNot Nothing Then
            Dim cust As Customer = customerbook.GetByID(item.cust_id)
            If cust IsNot Nothing Then
                item.customer = cust
                If item.type = Address.AddressType.mailing_address Then
                    cust.mailing_address_id = item.ID
                Else
                    cust.shipping_address_id = item.ID
                End If
            End If
        End If
    End Sub

    Private Sub NewOrderItem(item As OrderItem) Handles orderitembook.NewItem
        If item IsNot Nothing Then
            Dim o As Order = orderbook.GetByID(item.order_id)
            If o IsNot Nothing Then
                item.order = o
                o.item_count = o.item_count + 1
            End If
            Dim p As Product = productbook.GetByID(item.product_id)
            If p IsNot Nothing Then
                item.product = p
            End If
        End If
    End Sub

    Private Sub NewOrder(item As Order) Handles orderbook.NewOrder
        If item IsNot Nothing Then
            Dim c As Customer = customerbook.GetByID(item.customer_id)
            If c IsNot Nothing Then
                item.customer = c
            End If
        End If
    End Sub

    Private Sub DeleteOrder(item As Order) Handles orderbook.DeleteOrder
        orderitembook.RemoveByOrderID(item.ID)
    End Sub

End Module
