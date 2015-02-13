Imports NUnit.Framework

<TestFixture> Public Class ComputedTests
    <Test> Public Sub Computed_Works()
        Dim a = Dep.Factory.Observable("abc")
        Dim b = Dep.Factory.Observable("def")
        Dim c = Dep.Factory.Computed(Function() a.Value + b.Value)

        Assert.AreEqual("abcdef", c.Value)
    End Sub

    <Test> Public Sub Computed_DependencyValueChanged()
        Dim a = Dep.Factory.Observable("abc")
        Dim b = Dep.Factory.Observable("def")
        Dim c = Dep.Factory.Computed(Function() a.Value + b.Value)

        a.Value = "bob"
        Assert.AreEqual("bobdef", c.Value)
    End Sub

    <Test> Public Sub Computed_INotifyPropagates()
        Dim a = Dep.Factory.Observable("abc")
        Dim b = Dep.Factory.Observable("def")

        Dim c = Dep.Factory.Computed(Function() a.Value + b.Value)

        Dim x = Dep.Factory.Observable("hello")



        Dim eventRaised As Boolean = False
        AddHandler c.PropertyChanged, Sub() eventRaised = True

        a.Value = "snooze"
        Assert.True(eventRaised)
    End Sub
End Class
