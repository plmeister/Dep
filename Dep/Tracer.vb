Imports System.ComponentModel

Public Class Tracer
    Shared _dependencies As New List(Of INotifyPropertyChanged)
    Public Shared ReadOnly Property Dependencies
        Get
            Return _dependencies
        End Get
    End Property

    Public Shared Sub Start()
        _dependencies.Clear()
        _enabled = True
    End Sub

    Public Shared Sub [Stop]()
        _enabled = False
    End Sub

    Shared _enabled As Boolean = False
    Public Shared Sub Register(obj As INotifyPropertyChanged)
        If Not _enabled Then Exit Sub
        _dependencies.Add(obj)
    End Sub
End Class
