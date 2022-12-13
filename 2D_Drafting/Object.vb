Public Class InterSectionObj
    Public horLine As Integer
    Public verLine As Integer
    Public horSectCnt(100) As Integer
    Public verSectCnt(100) As Integer
    Public horSectPos(100, 1000) As PointF
    Public verSectPos(100, 1000) As PointF
    Public thr_seg As Integer

    Public Sub Refresh()
        thr_seg = 0
        horLine = 0
        verLine = 0
        For i = 0 To 100
            horSectCnt(i) = 0
            verSectCnt(i) = 0
            For j = 0 To 1000
                horSectPos(i, j).X = 0
                horSectPos(i, j).Y = 0
                verSectPos(i, j).X = 0
                verSectPos(i, j).Y = 0
            Next
        Next
    End Sub
End Class

Public Class CircleObj
    Public pos(1000) As PointF
    Public size(1000) As Double
    Public Cnt As Integer

    Public Sub Refresh()
        Cnt = 0
        For i = 0 To 1000
            pos(i).X = 0
            pos(i).Y = 0
            size(i) = 0
        Next
    End Sub
End Class

Public Class PhaseSeg
    Public PhaseVal As List(Of Integer) = New List(Of Integer)
    Public PhaseCol As List(Of String) = New List(Of String)

    Public Sub Refresh()
        PhaseVal.Clear()
        PhaseCol.Clear()
    End Sub
End Class

Public Class BlobSeg
    Public BlobList As List(Of BlobObj) = New List(Of BlobObj)

    Public Sub Refresh()
        BlobList.Clear()
    End Sub
End Class
Public Structure BlobObj
    Public pos As PointF
    Public height As Double
    Public Width As Double
    Public Area As Double
    Public Perimeter As Double
    Public Ratio As Double
    Public roundness As Double
    Public topLeft As PointF
    Public rightBottom As PointF
End Structure