'structure for indentifing measurement types
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

'structure for lineAlign, lineHorizontal, lineVertical, lineParallel, pencil, ptToLine
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

'structure for angle and angle2Line
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

'Structure for arc
Public Structure ArcObject
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


'structure for Object which involves all child objects 
Public Structure MeasureObject
    Public startPoint As PointF        'start point of object
    Public middlePoint As PointF       'middle point of object
    Public endPoint As PointF          'end point of object
    Public lastPoint As PointF         'optional, used for angle2Line, the fourth point
    Public commonPoint As PointF       'optional, used for angle2Line, the common point of first line and second line of angle
    Public drawPoint As PointF         'point for drawing text
    Public measuringType As Integer      'measuring type of current object
    Public intialized As Boolean        'flag for specifying that object is initialized or not
    Public itemSet As Integer          'the limit of points
    Public length As Double             'the length of object
    Public angle As Double              'optional, used for angle, angle2Line
    Public arc As Double                'optional, used for arc
    Public annotation As String         'optional, used for annotation
    Public lineObject As LineObject
    Public angleObject As AngleObject
    Public arcObject As ArcObject
    Public annoObject As AnnoObject
    Public objNum As Integer           'the order of current object
    Public lineInfor As LineStyle      'information of drawing line
    Public scaleObject As ScaleObject
    Public fontInfor As FontInfor      'information of text font
    Public leftTop As PointF           'the left top cornor of object
    Public rightBottom As PointF       'the right bottom cornor of object
    Public name As String
    Public description As String               'the name of object
    Public parameter As String            'parameter of object
    Public spec As String               'specification of object
    Public judgement As String

    Public curveObject As CurveObject
    Public dotFlag As Boolean
    Public Sub Refresh()
        startPoint.X = 0
        startPoint.Y = 0
        middlePoint.X = 0
        middlePoint.Y = 0
        endPoint.X = 0
        endPoint.Y = 0
        drawPoint.X = 0
        drawPoint.Y = 0
        lastPoint.X = 0
        lastPoint.Y = 0
        commonPoint.X = 0
        commonPoint.Y = 0

        length = 0
        angle = 0
        arc = 0
        annotation = ""
        intialized = False
        itemSet = 0

        name = ""
        description = ""
        parameter = ""
        spec = ""
        judgement = ""
        dotFlag = False

        measuringType = MeasureType.initState
    End Sub
End Structure

'enum for indentifying the types of detection or segmentation
Public Enum SegType
    circle = 0
    interSect = 1
    phaseSegment = 2
    blobSegment = 3
End Enum

'structure for object for detection or segmentation
Public Structure SegObject
    Public measureType As Integer
    Public circleObj As CircleObj
    Public sectObj As InterSectionObj
    Public phaseSegObj As PhaseSegObj
    Public BlobSegObj As BlobSegObj

    Public Sub Refresh()
        circleObj.Refresh()
        sectObj.Refresh()
        phaseSegObj.Refresh()
        BlobSegObj.Refresh()
    End Sub
End Structure

'Structure of Object which is used for intersection detection
Public Class InterSectionObj
    Public horLine As Integer
    Public verLine As Integer
    Public horSectCnt(100) As Integer
    Public verSectCnt(100) As Integer
    Public horSectPos(100, 1000) As PointF
    Public verSectPos(100, 1000) As PointF
    Public threshold As Integer

    Public Sub Refresh()
        threshold = 0
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

'structure for Object which is used for circle detection
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

'structure for phase segmentation
Public Class PhaseSegObj
    Public PhaseVal As List(Of Integer) = New List(Of Integer)
    Public PhaseCol As List(Of String) = New List(Of String)

    Public Sub Refresh()
        PhaseVal.Clear()
        PhaseCol.Clear()
    End Sub
End Class

'structure for blob detection
Public Class BlobSegObj
    Public BlobList As List(Of BlobObj) = New List(Of BlobObj)

    Public Sub Refresh()
        BlobList.Clear()
    End Sub
End Class

'structure for blob object
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

'the set of objects for measuring min, max, perpendicular min and perpendicular max distances 
Public Class CurveObject
    Public CuPolyItem As List(Of CuPolyObj) = New List(Of CuPolyObj)()
    Public PolyItem As List(Of PolyObj) = New List(Of PolyObj)()
    Public PointItem As List(Of PointObj) = New List(Of PointObj)()
    Public LineItem As List(Of LineObj) = New List(Of LineObj)()
    Public CurveItem As List(Of CurveObj) = New List(Of CurveObj)()
End Class

'object of line object for min, max, perpendicular min and perpendicular max distances 
Public Structure LineObj
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

'object of point object for min, max, perpendicular min and perpendicular max distances 
Public Structure PointObj
    Public PointPoint As PointF
    Public PDrawPos As PointF

    Public Sub Refresh()
        PointPoint.X = 0
        PointPoint.Y = 0
        PDrawPos.X = 0
        PDrawPos.Y = 0
    End Sub
End Structure

'object of curve object for min, max, perpendicular min and perpendicular max distances 
Public Class CurveObj
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

'object of polyminal object for min, max, perpendicular min and perpendicular max distances 
Public Class PolyObj
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

'object of curve&polyminal object for min, max, perpendicular min and perpendicular max distances 
Public Class CuPolyObj
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

