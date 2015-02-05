Imports System.ComponentModel
Imports System.Reflection.IntrospectionExtensions

Public Class PropertyBinding
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly", Justification:="no need for an eventargs parameter")>
    Public Event TargetExpiry(sender As Object)

    Protected Sub TargetHasExpired()
        RaiseEvent TargetExpiry(Me)
    End Sub

End Class

Public Class PropertyBinding(Of TSource As {INotifyPropertyChanged, Class}, TDestination As Class)
    Inherits PropertyBinding

    Public Property SourceObject As WeakReference(Of TSource)
    Public Property DestinationObject As WeakReference(Of TDestination)

    Dim _source As String
    Public Property SourceProperty As String
        Get
            Return _source
        End Get
        Set(value As String)
            _source = value
            _sourcePropInfo = GetType(TSource).GetTypeInfo().GetDeclaredProperty(value)
        End Set
    End Property

    Dim _dest As String
    Public Property DestinationProperty As String
        Get
            Return _dest
        End Get
        Set(value As String)
            _dest = value
            _destPropInfo = GetType(TDestination).GetTypeInfo().GetDeclaredProperty(value)
        End Set
    End Property

    Dim _sourcePropInfo As Reflection.PropertyInfo = Nothing
    Dim _destPropInfo As Reflection.PropertyInfo = Nothing

    Public Sub New(sourceObject As TSource, sourceProperty As String, destinationObject As TDestination, destinationProperty As String)
        Me.SourceObject = New WeakReference(Of TSource)(sourceObject)
        Me.DestinationObject = New WeakReference(Of TDestination)(destinationObject)

        Me.SourceProperty = sourceProperty
        Me.DestinationProperty = destinationProperty

        AddHandler sourceObject.PropertyChanged, AddressOf SourcePropertyChanged
        Bindings.Execute.Invoke(Sub() _destPropInfo.SetValue(destinationObject, _sourcePropInfo.GetValue(sourceObject)))
    End Sub

    Private Sub SourcePropertyChanged(sender As Object, e As PropertyChangedEventArgs)
        If e.PropertyName <> SourceProperty Then Exit Sub

        Dim target As TDestination = Nothing
        If DestinationObject.TryGetTarget(target) Then
            Bindings.Execute.Invoke(Sub() _destPropInfo.SetValue(target, _sourcePropInfo.GetValue(sender)))
        Else
            Dim source As TSource = Nothing
            If SourceObject.TryGetTarget(source) Then
                RemoveHandler source.PropertyChanged, AddressOf SourcePropertyChanged
            End If
            TargetHasExpired()
        End If
    End Sub

End Class
