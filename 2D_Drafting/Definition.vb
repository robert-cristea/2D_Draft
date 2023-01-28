Public Enum MeasureType
    initState = -1
    lineAlign = 0
    lineHorizontal = 1
    lineVertical = 2
    angle = 3
    arc = 4
    annotation = 5
    angle2Line = 6
    lineParallel = 7
    pencil = 8
    ptToLine = 9
    measureScale = 10
    arcFixed = 11
    lineFixed = 12
    angleFixed = 13
    objLine = 14
    objPoly = 15
    objPoint = 16
    objCurve = 17
    objCuPoly = 18
    objSel = 19
    objMinMax = 20
    toCurves = 21
End Enum

'structure for drawing line
Public Structure LineStyle
    Public line_style As String
    Public line_width As Integer
    Public line_color As Color
    Public Sub New(ByVal width As Integer)
        line_style = "dotted"
        line_width = width
        line_color = Color.Black
    End Sub
End Structure

'structure for text font
Public Structure FontInfor
    Public font_color As Color
    Public text_font As Font
    Public Sub New(ByVal height As Integer)
        font_color = Color.Black
        text_font = New Font("Arial", height, FontStyle.Regular)
    End Sub
End Structure

'structure for line_align, line_horizon, line_vertical, line_para, draw_line, pt_line
Public Structure LineObject
    Public nor_pt1 As PointF        'connect with start point
    Public nor_pt2 As PointF        'connect with end point    
    Public nor_pt3 As PointF        'connect with nor_pt4
    Public nor_pt4 As PointF
    Public nor_pt5 As PointF        'connect with nor_pt3
    Public nor_pt6 As PointF        'connect with nor_pt3
    Public nor_pt7 As PointF        'connect with nor_pt4
    Public nor_pt8 As PointF        'connect with nor_pt4
    Public draw_pt As PointF        'point for drawing line
    Public trans_angle As Integer   'angle between measuring line and X-axis
    Public side_drag As PointF      'flag for side drawing
End Structure

'structure for angle and angle_far
Public Structure AngleObject
    Public start_angle As Double        'angle between first line of arc and X-axis
    Public sweep_angle As Double        'angle between first line and second line of arc
    Public radius As Double             'radius of angle
    Public start_pt As PointF           'start point of angle
    Public end_pt As PointF             'end point of angle
    Public nor_pt1 As PointF            'included in first line of angle
    Public nor_pt2 As PointF            'connect to nor_pt1
    Public nor_pt3 As PointF            'connect to nor_pt1
    Public nor_pt4 As PointF            'included in second line of angle
    Public nor_pt5 As PointF            'connect to nor_pt4
    Public nor_pt6 As PointF            'connect to nor_pt4
    Public draw_pt As PointF            'point for drawing text
    Public trans_angle As Integer       'angle between text and X-axis
    Public side_drag As PointF          'flag for side drawing
    Public side_index As Integer        'index of nor_pt for side drawing
End Structure

'Structure for radius
Public Structure RadiusObject
    Public center_pt As PointF      'center point of arc
    Public circle_pt As PointF      'point in circle which is used for drawing radius
    Public draw_pt As PointF        'point for drawing text
    Public arr_pt1 As PointF        'used for drawing arrows
    Public arr_pt2 As PointF        'used for drawing arrows
    Public arr_pt3 As PointF        'used for drawing arrows
    Public arr_pt4 As PointF        'used for drawing arrows
    Public trans_angle As Integer   'angle between text and X-axis
    Public radius As Double         'radius of arc
    Public side_drag As PointF      'flag for side drawing

End Structure

'structure for annotation
Public Structure AnnoObject
    Public line_pt As PointF        'point used for drawing line
    Public size As Size             'size of anno object
End Structure

'structure for measure_scale
Public Structure ScaleObject
    Public style As String          'flag for horizontal or vertical
    Public length As Double         'the length of scale
    Public start_pt As PointF       'start point of scale
    Public end_pt As PointF         'end point of scale
    Public nor_pt1 As PointF        'used for drawing bounds
    Public nor_pt2 As PointF        'used for drawing bounds
    Public nor_pt3 As PointF        'used for drawing bounds
    Public nor_pt4 As PointF        'used for drawing bounds
    Public trans_angle As Integer   'angle between measure object and X-axis

End Structure

Public Structure C_LineObject
    Public FirstPointOfLine As PointF
    Public SecndPointOfLine As PointF
    Public LDrawPos As PointF

    Public Sub Refresh()
        FirstPointOfLine.X = 0
        FirstPointOfLine.Y = 0
        SecndPointOfLine.X = 0
        SecndPointOfLine.Y = 0
        LDrawPos.X = 0
        LDrawPos.Y = 0
    End Sub
End Structure

Public Structure C_PointObject
    Public PointPoint As PointF
    Public PDrawPos As PointF

    Public Sub Refresh()
        PointPoint.X = 0
        PointPoint.Y = 0
        PDrawPos.X = 0
        PDrawPos.Y = 0
    End Sub
End Structure

'structure for drawing objects
Public Structure MeasureObject
    Public start_point As PointF        'start point of object
    Public middle_point As PointF       'middle point of object
    Public end_point As PointF          'end point of object
    Public last_point As PointF         'optional, used for angle_far, the fourth point
    Public common_point As PointF       'optional, used for angle_far, the common point of first line and second line of angle

    Public draw_point As PointF         'point for drawing text
    Public measure_type As Integer      'measuring type of current object
    Public intialized As Boolean        'flag for specifying that object is initialized or not
    Public item_set As Integer          'the limit of points
    Public length As Double             'the length of object
    Public angle As Double              'optional, used for angle, angle_far
    Public radius As Double             'optional, used for radius
    Public annotation As String         'optional, used for annotation
    Public line_object As LineObject
    Public angle_object As AngleObject
    Public radius_object As RadiusObject
    Public anno_object As AnnoObject
    Public obj_num As Integer           'the order of current object
    Public line_infor As LineStyle      'information of drawing line
    Public scale_object As ScaleObject
    Public font_infor As FontInfor      'information of text font

    Public left_top As PointF           'the left top cornor of object
    Public right_bottom As PointF       'the right bottom cornor of object

    Public name As String               'the name of object
    Public remarks As String            'remarks of object

    Public curve_object As CurveObject
    Public dot_flag As Boolean
    Public Sub Refresh()
        start_point.X = 0
        start_point.Y = 0
        middle_point.X = 0
        middle_point.Y = 0
        end_point.X = 0
        end_point.Y = 0
        draw_point.X = 0
        draw_point.Y = 0
        last_point.X = 0
        last_point.Y = 0
        common_point.X = 0
        common_point.Y = 0

        length = 0
        angle = 0
        radius = 0
        annotation = ""
        intialized = False
        item_set = 0

        name = ""
        remarks = ""
        dot_flag = False

        measure_type = MeasureType.initState
    End Sub
End Structure

Public Enum SegType
    circle = 0
    intersection = 1
    phaseSegmentation = 2
    BlobSegment = 3
End Enum


Public Structure SegObject
    Public measureType As Integer
    Public circleObj As CircleObj
    Public sectObj As InterSectionObj
    Public phaseSegObj As PhaseSeg
    Public BlobSegObj As BlobSeg

    Public Sub Refresh()
        circleObj.Refresh()
        sectObj.Refresh()
        phaseSegObj.Refresh()
        BlobSegObj.Refresh()
    End Sub
End Structure

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