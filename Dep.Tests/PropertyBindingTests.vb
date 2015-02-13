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

    <Test> Public Sub Binding_Execute_Init()
        Dim a As New Dep.Observable(Of String)("a")
        Dim b As New Dep.Observable(Of String)("b")

        Dim executed As Boolean = False
        Dim oldExecute = Bindings.Execute
        Bindings.Execute = Sub(ac As Action)
                               executed = True
                               ac()
                           End Sub

        Dep.Bindings.Add(a, b)
        Bindings.Execute = oldExecute
        Assert.True(executed)
    End Sub

    <Test> Public Sub Binding_Execute_Change()
        Dim a As New Dep.Observable(Of String)("a")
        Dim b As New Dep.Observable(Of String)("b")

        Dim executed As Boolean = False
        Dim oldExecute = Bindings.Execute

        Dep.Bindings.Add(a, b)
        Bindings.Execute = Sub(ac As Action)
                               executed = True
                               ac()
                           End Sub
        a.Value = "z"
        Bindings.Execute = oldExecute
        Assert.True(executed)
    End Sub
End Class