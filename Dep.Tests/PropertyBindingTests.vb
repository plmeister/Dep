Imports NUnit.Framework

<TestFixture> Public Class PropertyBindingTests

    <Test> Public Sub Binding_ObservableToObservable_init()
        Dim a As New DummyClass(Of String)("hello")
        Dim b As New DummyClass(Of String)("goodbye")

        Dep.Bindings.Add(a.ObservableProperty, b.ObservableProperty)
        Assert.AreEqual(a.ObservableProperty.Value, b.ObservableProperty.Value)
    End Sub

    <Test> Public Sub Binding_ObservableToObservable_change()
        Dim a As New DummyClass(Of String)("hello")
        Dim b As New DummyClass(Of String)("goodbye")

        Dep.Bindings.Add(a.ObservableProperty, b.ObservableProperty)

        a.ObservableProperty.Value = "zzz"

        Assert.AreEqual("zzz", b.ObservableProperty.Value)
    End Sub

    <Test> Public Sub Binding_INotifyToObservable()
        Dim src As New DummyNotify(Of String)("hello")
        Dim dst As New DummyClass(Of String)("goodbye")

        Dep.Bindings.Add(src, "Value", dst.ObservableProperty)

        Assert.AreEqual(src.Value, dst.ObservableProperty.Value)
    End Sub

    <Test> Public Sub Binding_ObservableToBasicPropertyByString_init()
        Dim src As New DummyClass(Of String)("abc")
        Dim dst As New DummyNotify(Of String)("def")

        Dep.Bindings.Add(src.ObservableProperty, dst, "Value")

        Assert.AreEqual(src.ObservableProperty.Value, dst.Value)
    End Sub


End Class