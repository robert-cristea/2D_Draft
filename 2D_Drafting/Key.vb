Public Class Key
    Public Name As String
    Public Value As String
    Public IsCommented As Boolean
    
    Public Sub New(ByVal KeyName As String, ByVal KeyValue As String, Optional ByVal KeyIsCommented As Boolean = False)
        Name = KeyName
        Value = KeyValue
        IsCommented = KeyIsCommented
    End Sub

    Public Overloads Overrides Function Equals(ByVal obj As Object) As Boolean
        If obj Is Nothing Or Not Me.GetType() Is obj.GetType() Then
            Return False
        End If
        Dim k As Key = CType(obj, Key)
        Return Me.Name = k.Name And Me.Value = k.Value And Me.IsCommented = k.IsCommented
    End Function

    Public Overrides Function GetHashCode() As Integer
        Dim s As String = Name + Value + IsCommented
        Return s.GetHashCode()
    End Function
End Class