Imports NUnit.Framework

<TestFixture> Public Class ObservableTests
    <Test> Public Sub SetDifferentValueRaisesEvent()
        Dim x = Dep.Factory.Observable("abc")
        Dim eventRaised As Boolean = False
        AddHandler x.PropertyChanged, Sub() eventRaised = True

        x.Value = "def"
        Assert.True(eventRaised)
    End Sub

    <Test> Public Sub SetSameValueDoesNotRaiseEvent()
        Dim x = Dep.Factory.Observable("zzz")
        Dim eventRaised As Boolean = False
        AddHandler x.PropertyChanged, Sub() eventRaised = True
        x.Value = "zzz"
        Assert.False(eventRaised)
    End Sub
End Class
