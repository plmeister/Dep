# Dep
.net property dependency tracking and binding

Dep is a very lightweight library (at least currently) which takes some inspiration from knockoutjs's observable and computed objects

Basic usage:
```vb.net
Public Class MyModel
  Public Property FirstName As Dep.Observable(Of String)("John")
  Public Property Surname As Dep.Observable(Of String)("Smith")
  Public Property FullName As Dep.Computed(Of String)(Function() FirstName.Value + " " + Surname.Value)
End Class
```

Observables' values implement System.ComponentModel.INotifyPropertyChanged so raise an event when the value changes - this can be used by normal Windows Forms data binding, or you can catch the event yourself

```vb.net
Dim x As New MyModel()
AddHandler x.FullName.PropertyChanged, Sub() Console.WriteLine(x.FullName)
x.FirstName.Value = "Peter"
```
