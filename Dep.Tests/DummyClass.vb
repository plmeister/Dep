Public Class DummyClass(Of T)
    Public Property ObservableProperty As Dep.Observable(Of T)

    Public Sub New(value As T)
        ObservableProperty = New Dep.Observable(Of T)(value)
    End Sub
End Class
