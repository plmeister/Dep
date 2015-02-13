Public Class Factory
    Public Shared Function Observable(Of T)(value As T) As Observable(Of T)
        Return New Observable(Of T)(value)
    End Function

    Public Shared Function Computed(Of T)(f As Func(Of T)) As Computed(Of T)
        Return New Computed(Of T)(f)
    End Function

End Class
