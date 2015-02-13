Public Class Observable(Of T)
    Implements System.ComponentModel.INotifyPropertyChanged

    Dim _value As T
    Public Property Value As T
        Get
            Tracer.Register(Me)
            Return _value
        End Get
        Set(value As T)
            If value.GetHashCode() <> _value.GetHashCode() Then
                _value = value
                ValueChanged()
            End If
        End Set
    End Property

    Protected Sub ValueChanged()
        RaiseEvent PropertyChanged(Me, New System.ComponentModel.PropertyChangedEventArgs("Value"))
    End Sub

    Public Sub New(initialValue As T)
        _value = initialValue
    End Sub

    Public Event PropertyChanged(sender As Object, e As ComponentModel.PropertyChangedEventArgs) Implements ComponentModel.INotifyPropertyChanged.PropertyChanged

    Public Overrides Function ToString() As String
        Return Value.ToString()
    End Function

End Class
