
Imports Database
Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass()> Public Class CustomerTest

    Dim cust1 As Customer = Nothing
    Dim cust2 As Customer = Nothing

    <TestMethod()> Public Sub TestMethod1()
    End Sub

    <TestInitialize()> Public Sub Initialize()
        Assert.IsNull(cust1)
        Assert.IsNull(cust2)
        cust1 = New Customer(1, "One", "one@one.com", 1, 2, "123-232-1243", 5342)
        cust2 = New Customer(2, "Two", "Jacob@twotwo.comcom", 3, 0, "123-232-3423", 2.22)
        'objects are created and references are updated accordingly
        Assert.IsNotNull(cust1)
        Assert.IsNotNull(cust2)
    End Sub

    ' called after each test case
    <TestCleanup()> Public Sub CleanUp()
        Assert.IsNotNull(cust1)
        Assert.IsNotNull(cust2)
        cust1 = Nothing
        cust2 = Nothing
        ' objects are cleaned up
        Assert.IsNull(cust1)
        Assert.IsNull(cust2)
    End Sub

    ' Test Other Constructor
    <TestMethod()> Public Sub TestConstructor1()
        Dim cust As New Customer("3,joe,joe@three.com,213-231-1232,53,4,5")
        ' check that the object was created and all fields are set as expected
        Assert.IsNotNull(cust)

        Assert.AreEqual(cust.ID, 3)
        Assert.AreEqual(cust.mailing_address_id, 4)
        Assert.AreEqual(cust.name, "joe")
        Assert.AreEqual(cust.phone_number, "213-231-1232")
        Assert.AreEqual(cust.credit_limit, 53.0)
        Assert.AreEqual(cust.email, "joe@three.com")
    End Sub

    ' Try an invalid name
    <TestMethod(), ExpectedException(GetType(ArgumentException))> Public Sub cust_Name_fail_1()
        cust1.name = "13"
        Assert.AreNotEqual(cust1.name, "13")
    End Sub

    <TestMethod(), ExpectedException(GetType(ArgumentException))> Public Sub cust_Name_fail_2()
        cust1.name = ""
        Assert.AreNotEqual(cust1.name, "")
    End Sub

    <TestMethod()> Public Sub cust_Name_pass_1()
        Assert.AreNotEqual(cust1.name, "Bubba")
        cust1.name = "Bubba"
        Assert.AreEqual(cust1.name, "Bubba")
    End Sub

    <TestMethod()> Public Sub cust_Name_pass_2()
        Assert.AreNotEqual(cust1.name, "Billy Joe")
        cust1.name = "Billy Joe"
    End Sub

    <TestMethod()> Public Sub cust_Phone_pass_1()
        cust1.phone_number = "(905) 123-3122"
        Assert.AreEqual(cust1.phone_number, "(905) 123-3122")
    End Sub

    <TestMethod()> Public Sub cust_Phone_pass_2()
        Dim value As String = "123-312-8324"
        cust1.phone_number = value
        Assert.AreEqual(cust1.phone_number, value)
    End Sub


    <TestMethod(), ExpectedException(GetType(ArgumentException))> Public Sub cust_Phone_fail_1()
        cust1.phone_number = "231-1234-312"
        Assert.AreNotEqual(cust1.phone_number, "213-123-3122")
    End Sub
    <TestMethod(), ExpectedException(GetType(ArgumentException))> Public Sub cust_Phone_fail_2()
        Dim Value = "123-3122"
        cust1.phone_number = Value
        Assert.AreNotEqual(cust1.phone_number, Value)
    End Sub

    <TestMethod()> Public Sub cust_email_pass_1()
        Dim value = "joe@email.com"
        cust1.email = value
        Assert.AreEqual(cust1.email, value)
    End Sub
    <TestMethod()> Public Sub cust_email_pass_2()
        Dim value = "joe.32l.here@place.ca"
        cust1.email = value
        Assert.AreEqual(cust1.email, value)
    End Sub

    <TestMethod(), ExpectedException(GetType(ArgumentException))> Public Sub cust_email_fail_1()
        Dim Value = "joe.soil.place.ca"
        cust1.email = Value
        Assert.AreNotEqual(cust1.email, Value)
    End Sub
End Class
