Public Class SectionComparer
    Implements IComparer

    Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements System.Collections.IComparer.Compare
        Dim s1 As String = LCase(x.Name)
        Dim s2 As String = LCase(y.Name)
        Return s1.CompareTo(s2)
    End Function
End Class