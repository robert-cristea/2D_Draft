Public Class Section
    Inherits ArrayList
    Public Name As String
    Public IsCommented As Boolean

    Public Sub New(ByVal SectionName As String, Optional ByVal SectionIsCommented As Boolean = False)
        Name = SectionName
        IsCommented = SectionIsCommented
    End Sub

    Public Overloads Overrides Function Equals(ByVal obj As Object) As Boolean
        If obj Is Nothing Or Not Me.GetType() Is obj.GetType() Then
            Return False
        End If
        Dim s As Section = CType(obj, Section)
        Return Me.Name = s.Name And Me.IsCommented = s.IsCommented
    End Function

    Public Overrides Function GetHashCode() As Integer
        Dim s As String = Name
        Return s.GetHashCode()
    End Function
End Class