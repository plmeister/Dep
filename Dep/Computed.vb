Imports System.ComponentModel

Public Class Computed(Of T)
    Inherits Observable(Of T)

    Dim _f As Func(Of T)
    Public Overloads ReadOnly Property Value As T
        Get
            Return _f()
        End Get
    End Property

    Dim _lock As New Object

    Public Sub New(f As Func(Of T))
        MyBase.New(f())
        _f = f
        Trace()
    End Sub

    Protected Sub Trace()
        Dim dependencies As New List(Of INotifyPropertyChanged)
        SyncLock _lock
            Tracer.Start()
            _f()
            dependencies.AddRange(Tracer.Dependencies)
            Tracer.Stop()
        End SyncLock

        For Each x In dependencies
            AddHandler x.PropertyChanged, Sub() ValueChanged()
        Next
    End Sub

    Public Overrides Function ToString() As String
        Return Value().ToString()
    End Function
End Class
