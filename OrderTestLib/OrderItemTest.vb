Imports Database
Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass()> Public Class OrderItemTest
    Dim o1 As OrderItem = Nothing
    Dim o2 As OrderItem = Nothing

    <TestInitialize()> Public Sub Initialize()
        Assert.IsNull(o1)
        Assert.IsNull(o2)
        o1 = New OrderItem(1, 1, 1, 1)
        o2 = New OrderItem(2, 2, 2, 20)
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
        Dim o As New OrderItem(1, 1, 1, 5)
        ' check that the object was created and all fields are set as expected
        Assert.IsNotNull(o)
        Assert.AreEqual(o.ID, 1)
        Assert.AreEqual(o.order_id, 1)
        Assert.AreEqual(o.product_id, 1)
        'Assert.AreEqual(o.quantity, 5)
    End Sub

    <TestMethod()> Public Sub TestConstructor2()
        Dim o As New OrderItem("12,3,1,1,2015-02-25")
        Dim dateVal As Date = Date.ParseExact("2015-02-25", "yyyy-MM-dd", Nothing)
        ' check that the object was created and all fields are set as expected
        Assert.IsNotNull(o)
        Assert.AreEqual(o.ID, 12)
        Assert.AreEqual(o.order_id, 3)
        Assert.AreEqual(o.product_id, 1)
        'Assert.AreEqual(o.quantity, 1)
        Assert.AreEqual(o.ship_date, dateVal)
    End Sub
End Class