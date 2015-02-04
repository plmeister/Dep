Imports System.ComponentModel

Public Class Bindings
    Shared _bindings As New List(Of Object)

    Public Shared Property Execute As Action(Of Action) = Sub(a) a.Invoke()

    Private Shared Sub OnTargetExpiry(sender As Object)
        _bindings.Remove(sender)
    End Sub

    Private Shared Sub AddBinding(b As PropertyBinding)
        _bindings.Add(b)
        AddHandler b.TargetExpiry, AddressOf OnTargetExpiry
    End Sub

    Private Shared Function getPropertyName(Of TDest, T)(obj As TDest, propertyExpression As Expressions.Expression(Of Func(Of TDest, T))) As String
        Dim expr = DirectCast(propertyExpression.Body, Expressions.MemberExpression)
        Dim prop = DirectCast(expr.Member, Reflection.PropertyInfo)
        Return prop.Name
    End Function

    Public Shared Sub Add(Of T, TDestination As Class)(sourceComputed As Computed(Of T),
                                                 destinationObject As TDestination, propertyName As String)
        AddBinding(
            New PropertyBinding(Of Computed(Of T), TDestination)(
                sourceComputed, "Value",
                destinationObject, propertyName)
            )
    End Sub

    Public Shared Sub Add(Of T, TDestination As Class)(sourceComputed As Computed(Of T),
                                                 destinationObject As TDestination, propertyExpression As Expressions.Expression(Of Func(Of TDestination, T)))
        AddBinding(
            New PropertyBinding(Of Computed(Of T), TDestination)(
                sourceComputed, "Value",
                destinationObject, getPropertyName(destinationObject, propertyExpression))
            )
    End Sub

    Public Shared Sub Add(Of T, TDest As Class)(sourceObservable As Observable(Of T),
                                          destinationObject As TDest, propertyName As String)
        AddBinding(
            New PropertyBinding(Of Observable(Of T), TDest)(
                sourceObservable, "Value",
                destinationObject, propertyName)
            )
    End Sub

    Public Shared Sub Add(Of T, TDest As Class)(sourceObservable As Observable(Of T),
                                          destinationObject As TDest, propertyExpression As Expressions.Expression(Of Func(Of TDest, T)))
        AddBinding(
            New PropertyBinding(Of Observable(Of T), TDest)(
                sourceObservable, "Value",
                destinationObject, getPropertyName(destinationObject, propertyExpression))
            )
    End Sub

    Public Shared Sub Add(Of T As {INotifyPropertyChanged, Class}, TDest As Class)(sourceObject As T, sourceProperty As String,
                                                                             destObject As TDest, destProperty As String)
        AddBinding(
            New PropertyBinding(Of T, TDest)(
                sourceObject, sourceProperty,
                destObject, destProperty)
            )
    End Sub



    Public Shared Sub Add(Of T As {INotifyPropertyChanged, Class}, TDest)(sourceObject As T, sourceProperty As String,
                                                                    destObservable As Observable(Of TDest))
        AddBinding(
            New PropertyBinding(Of T, Observable(Of TDest))(
                sourceObject, sourceProperty,
                destObservable, "Value")
            )
    End Sub



    Public Shared Sub Add(Of T As {INotifyPropertyChanged, Class}, TDest)(sourceObject As T, sourcePropertyExpression As Expressions.Expression(Of Func(Of T, T)),
                                                                    destObservable As Observable(Of TDest))
        AddBinding(
            New PropertyBinding(Of T, Observable(Of TDest))(
                sourceObject, getPropertyName(sourceObject, sourcePropertyExpression),
                destObservable, "Value")
            )
    End Sub

    Public Shared Sub Add(Of T)(sourceObservable As Observable(Of T),
                          destObservable As Observable(Of T))
        AddBinding(
            New PropertyBinding(Of Observable(Of T), Observable(Of T))(
                sourceObservable, "Value",
                destObservable, "Value")
            )
    End Sub
End Class
