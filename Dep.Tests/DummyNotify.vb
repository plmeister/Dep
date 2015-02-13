Public Class DummyNotify(Of T)
    Implements System.ComponentModel.INotifyPropertyChanged

    Dim _value As T
    Public Property Value As T
        Get
            Return _value
        End Get
        Set(value As T)
            _value = value
            RaiseEvent PropertyChanged(Me, New System.ComponentModel.PropertyChangedEventArgs("Value"))
        End Set
    End Property

    Public Sub New(value As T)
        _value = value
    End Sub

    Public Event PropertyChanged(sender As Object, e As ComponentModel.PropertyChangedEventArgs) Implements ComponentModel.INotifyPropertyChanged.PropertyChanged
End Class
