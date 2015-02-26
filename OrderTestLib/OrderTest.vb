Imports Database
Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass()> Public Class OrderTest
    Dim o1 As Order = Nothing
    Dim o2 As Order = Nothing

    <TestInitialize()> Public Sub Initialize()
        Assert.IsNull(o1)
        Assert.IsNull(o2)
        o1 = New Order(1, 1, Today, 0)
        o2 = New Order(2, 2, Today, 10)
        'objects are created and references are updated accordingly
        Assert.IsNotNull(o1)
        Assert.IsNotNull(o2)
    End Sub

    ' called after each test case
    <TestCleanup()> Public Sub CleanUp()
        Assert.IsNotNull(o1)
        Assert.IsNotNull(o2)
        o1 = Nothing
        o2 = Nothing
        ' objects are cleaned up
        Assert.IsNull(o1)
        Assert.IsNull(o2)
    End Sub

    ' Test Other Constructor
    <TestMethod()> Public Sub TestConstructor1()
        Dim o As New Order(1, 1, Today, 10)
        ' check that the object was created and all fields are set as expected
        Assert.IsNotNull(o)

        Assert.AreEqual(o.ID, 1)
        Assert.AreEqual(o.customer_id, 1)
        Assert.AreEqual(o.order_date, Today)
        Assert.AreEqual(o.discount, 10.0)

    End Sub

    ' Test order_date Property with a date in future
    <TestMethod(), ExpectedException(GetType(ArgumentException))> Public Sub BadOrderDateExceptionTest()
        o1.order_date = DateTime.Today.AddDays(1)
        Assert.AreNotEqual(o1.order_date, DateTime.Today.AddDays(1))
    End Sub

End Class