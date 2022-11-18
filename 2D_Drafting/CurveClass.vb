Public Class C_CurveObject
    Public CurvePoint(1000) As PointF
    Public CDrawPos As PointF
    Public CPointIndx As Integer

    Public Sub Refresh()
        CDrawPos.X = 0
        CDrawPos.Y = 0
        CPointIndx = 0
        For i = 0 To 1000
            CurvePoint(i).X = 0
            CurvePoint(i).Y = 0
        Next
    End Sub
End Class

Public Class C_PolyObject
    Public PolyDrawPos As PointF
    Public PolyPointIndx As Integer
    Public PolyPoint(50) As PointF

    Public Sub Refresh()
        PolyPointIndx = 0
        PolyDrawPos.X = 0
        PolyDrawPos.Y = 0
        For i = 0 To 50
            PolyPoint(i).X = 0
            PolyPoint(i).Y = 0
        Next
    End Sub
End Class

Public Class C_CuPolyObject
    Public CuPolyPoint(30, 1000) As PointF
    Public CuPolyDrawPos As PointF
    Public CuPolyPointIndx_j As Integer
    Public CuPolyPointIndx_k(30) As Integer

    Public Sub Refresh()
        CuPolyPointIndx_j = 0
        CuPolyDrawPos.X = 0
        CuPolyDrawPos.Y = 0
        For i = 0 To 30
            CuPolyPointIndx_k(i) = 0
            For j = 0 To 1000
                CuPolyPoint(i, j).X = 0
                CuPolyPoint(i, j).Y = 0
            Next
        Next
    End Sub
End Class

Public Class CurveObject
    Public CuPolyItem As List(Of C_CuPolyObject) = New List(Of C_CuPolyObject)()
    Public PolyItem As List(Of C_PolyObject) = New List(Of C_PolyObject)()
    Public PointItem As List(Of C_PointObject) = New List(Of C_PointObject)()
    Public LineItem As List(Of C_LineObject) = New List(Of C_LineObject)()
    Public CurveItem As List(Of C_CurveObject) = New List(Of C_CurveObject)()

End Class