Imports NUnit.Framework

<TestFixture> Public Class ComputedTests
    <Test> Public Sub Computed_Works()
        Dim a As New Dep.Observable(Of String)("abc")
        Dim b As New Dep.Observable(Of String)("def")
        Dim c As New Dep.Computed(Of String)(Function() a.Value + b.Value)

        Assert.AreEqual("abcdef", c.Value)
    End Sub

    <Test> Public Sub Computed_DependencyValueChanged()
        Dim a As New Dep.Observable(Of String)("abc")
        Dim b As New Dep.Observable(Of String)("def")
        Dim c As New Dep.Computed(Of String)(Function() a.Value + b.Value)

        a.Value = "bob"
        Assert.AreEqual("bobdef", c.Value)
    End Sub

    <Test> Public Sub Computed_INotifyPropogates()
        Dim a As New Dep.Observable(Of String)("abc")
        Dim b As New Dep.Observable(Of String)("def")

        Dim c As New Dep.Computed(Of String)(Function() a.Value + b.Value)


        Dim eventRaised As Boolean = False
        AddHandler c.PropertyChanged, Sub() eventRaised = True

        a.Value = "snooze"
        Assert.True(eventRaised)
    End Sub
End Class
