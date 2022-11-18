
Imports System.Drawing

''' <summary>
''' This class contains utilities of Curves.
''' </summary>
Public Module C_Utils


    ''' <summary>
    ''' get center point of medium edge of polygen.
    ''' </summary>
    ''' <paramname="PolyA">The index of polygen object.</param>
    Function PolyGetPos(PolyA As C_PolyObject) As PointF
        Dim tempP As PointF
        Dim kk As Integer
        kk = PolyA.PolyPointIndx / 2
        tempP.X = (PolyA.PolyPoint(kk).X + PolyA.PolyPoint(kk + 1).X) / 2
        tempP.Y = (PolyA.PolyPoint(kk).Y + PolyA.PolyPoint(kk + 1).Y) / 2
        Return tempP
    End Function

    ''' <summary>
    ''' get center point of medium edge of Curve&Poly object.
    ''' </summary>
    ''' <paramname="CuPolyA">The index of Curve&Poly object.</param>
    Function CuPolyGetPos(CuPolyA As C_CuPolyObject) As PointF
        Dim temp As Integer
        temp = Math.Round(CuPolyA.CuPolyPointIndx_j / 2)
        If temp = 0 Then temp = 1
        Dim k_index = 0
        If CuPolyA.CuPolyPointIndx_k(temp) > 1 Then
            k_index = CuPolyA.CuPolyPointIndx_k(temp) - 1
        End If
        Return CuPolyA.CuPolyPoint(temp, k_index)
    End Function

    ''' <summary>
    ''' get center point of line object.
    ''' </summary>
    ''' <paramname="LA">The index of Line object.</param>
    Function LGetPos(LA As C_LineObject) As PointF
        Dim tempP As PointF
        tempP.X = (LA.FirstPointOfLine.X + LA.SecndPointOfLine.X) / 2
        tempP.Y = (LA.FirstPointOfLine.Y + LA.SecndPointOfLine.Y) / 2
        Return tempP
    End Function

    ''' <summary>
    ''' get center point of Curve object.
    ''' </summary>
    ''' <paramname="CA">The index of Curve object.</param>
    Function CGetPos(CA As C_CurveObject) As PointF

        Return CA.CurvePoint((CA.CPointIndx / 2))
    End Function

    ''' <summary>
    ''' set point for drawing label.
    ''' </summary>
    ''' <paramname="PP">The index of Point object.</param>
    Function PGetPos(PP As PointF) As PointF
        Dim tempP As PointF
        If PP.X > 0.01 Then
            tempP.X = PP.X - 0.01
        Else
            tempP.X = PP.X
        End If

        If PP.Y > 0.02 Then
            tempP.Y = PP.Y - 0.02
        Else
            tempP.Y = PP.Y
        End If
        Return tempP
    End Function

    ''' <summary>
    ''' clear C_Line Object.
    ''' </summary>
    ''' <paramname="C_line">The C_Line object.</param>
    Function Clear(ByRef C_line As C_LineObject)
        Dim tempPt = New PointF(0, 0)
        C_line.FirstPointOfLine = tempPt
        C_line.SecndPointOfLine = tempPt
        C_line.LDrawPos = tempPt
    End Function

    ''' <summary>
    ''' clear C_Point Object.
    ''' </summary>
    ''' <paramname="C_Point">The C_Point object.</param>
    Function Clear(ByRef C_Point As C_PointObject)
        Dim tempPt = New PointF(0, 0)
        C_Point.PointPoint = tempPt
        C_Point.PDrawPos = tempPt
    End Function

    ''' <summary>
    ''' clear C_Poly Object.
    ''' </summary>
    ''' <paramname="C_Poly">The C_Poly object.</param>
    Function Clear(ByRef C_Poly As C_PolyObject)
        Dim tempPt = New PointF(0, 0)
        C_Poly.PolyDrawPos = tempPt
        C_Poly.PolyPointIndx = 0
        For i = 0 To 49
            C_Poly.PolyPoint(i) = tempPt
        Next
    End Function

    ''' <summary>
    ''' Clone C_Poly Object.
    ''' </summary>
    ''' <paramname="C_Poly">The C_Poly object.</param>
    Function ClonePolyObj(ByVal C_Poly As C_PolyObject) As C_PolyObject
        Dim Obj As C_PolyObject = New C_PolyObject()
        Obj.PolyPointIndx = C_Poly.PolyPointIndx
        Obj.PolyDrawPos = C_Poly.PolyDrawPos
        For i = 0 To 50
            Obj.PolyPoint(i) = C_Poly.PolyPoint(i)
        Next
        Return Obj
    End Function

    ''' <summary>
    ''' Clone C_Line Object.
    ''' </summary>
    ''' <paramname="Line">The C_Poly object.</param>
    Function CloneLineObj(ByVal Line As C_LineObject) As C_LineObject
        Dim Obj As C_LineObject = New C_LineObject()
        Obj.FirstPointOfLine = Line.FirstPointOfLine
        Obj.SecndPointOfLine = Line.SecndPointOfLine
        Obj.LDrawPos = Line.LDrawPos

        Return Obj
    End Function

    ''' <summary>
    ''' Clone C_Curve Object.
    ''' </summary>
    ''' <paramname="Src">The source object.</param>
    Function CloneCurveObj(ByVal Src As C_CurveObject) As C_CurveObject
        Dim Obj As C_CurveObject = New C_CurveObject()
        Obj.CDrawPos = Src.CDrawPos
        Obj.CPointIndx = Src.CPointIndx
        For i = 0 To Src.CPointIndx
            Obj.CurvePoint(i) = Src.CurvePoint(i)
        Next
        Return Obj
    End Function

    ''' <summary>
    ''' Clone C_Point Object.
    ''' </summary>
    ''' <paramname="Src">The source object.</param>
    Function ClonePointObj(ByVal Src As C_PointObject) As C_PointObject
        Dim Obj As C_PointObject = New C_PointObject()
        Obj.PDrawPos = Src.PDrawPos
        Obj.PointPoint = Src.PointPoint
        Return Obj
    End Function

    ''' <summary>
    ''' Clone C_Cupoly Object.
    ''' </summary>
    ''' <paramname="Src">The source object.</param>
    Function CloneCuPolyObj(ByVal Src As C_CuPolyObject) As C_CuPolyObject
        Dim Obj As C_CuPolyObject = New C_CuPolyObject()
        Obj.CuPolyDrawPos = Src.CuPolyDrawPos
        Obj.CuPolyPointIndx_j = Src.CuPolyPointIndx_j
        For i = 0 To Obj.CuPolyPointIndx_j
            Obj.CuPolyPointIndx_k(i) = Src.CuPolyPointIndx_k(i)
            For j = 0 To Obj.CuPolyPointIndx_k(i)
                Obj.CuPolyPoint(i, j) = Src.CuPolyPoint(i, j)
            Next
        Next
        Return Obj
    End Function

    ''' <summary>
    ''' clear C_Curve Object.
    ''' </summary>
    ''' <paramname="C_Curve">The C_Curve object.</param>
    Function Clear(ByRef C_Curve As C_CurveObject)
        Dim tempPt = New PointF(0, 0)
        C_Curve.CDrawPos = tempPt
        C_Curve.CPointIndx = 0
        For i = 0 To 9999
            C_Curve.CurvePoint(i) = tempPt
        Next
    End Function

    ''' <summary>
    ''' clear C_CuPoly Object.
    ''' </summary>
    ''' <paramname="C_CuPoly">The C_CuPoly object.</param>
    Function Clear(ByRef C_CuPoly As C_CuPolyObject)
        Dim tempPt = New PointF(0, 0)
        C_CuPoly.CuPolyDrawPos = tempPt
        C_CuPoly.CuPolyPointIndx_j = 0
        For i = 0 To 29
            For j = 0 To 9999
                C_CuPoly.CuPolyPoint(i, j) = tempPt
            Next
            C_CuPoly.CuPolyPointIndx_k(i) = 0
        Next
    End Function

    ''' <summary>
    ''' clear Curve Object.
    ''' </summary>
    ''' <paramname="Curve">The Curve object.</param>
    Function Clear(ByRef Curve As CurveObject)
        Curve.CuPolyItem.Clear()
        Curve.PolyItem.Clear()
        Curve.PointItem.Clear()
        Curve.LineItem.Clear()
        Curve.CurveItem.Clear()
    End Function

    Function SetLineAndFont(ByRef item As MeasureObject, ByVal line_infor As LineStyle, ByVal font_infor As FontInfor)
        item.line_infor.line_style = line_infor.line_style
        item.line_infor.line_width = line_infor.line_width
        item.line_infor.line_color = line_infor.line_color
        item.font_infor.font_color = font_infor.font_color
        item.font_infor.text_font = font_infor.text_font
    End Function

    ''' <summary>
    ''' Initiate label indexs.
    ''' </summary>
    Public Sub init_LabelVar()
        Main_Form.PRealSelectArrayIndx_L = -1
        Main_Form.PReadySelectArrayIndx_L = -1
        Main_Form.CReadySelectArrayIndx_L = -1
        Main_Form.CRealSelectArrayIndx_L = -1
        Main_Form.PolyReadySelectArrayIndx_L = -1
        Main_Form.PolyRealSelectArrayIndx_L = -1
        Main_Form.LReadySelectArrayIndx_L = -1
        Main_Form.LRealSelectArrayIndx_L = -1
        Main_Form.CuPolyReadySelectArrayIndx_L = -1
        Main_Form.CuPolyRealSelectArrayIndx_L = -1
    End Sub

    ''' <summary>
    ''' calculate distance between point and polygen.
    ''' </summary>
    ''' <paramname="Polyi">The polygen object.</param>
    ''' <paramname="m_pt">coordinate of target point.</param>
    ''' <paramname="width">the width of picturebox.</param>
    ''' <paramname="width">the height of picturebox.</param>
    Public Function Find_BPointPolyDistance(Polyi As C_PolyObject, mPt As Point, width As Integer, height As Integer) As Integer
        Dim tempDis, minTempDis As Integer
        Dim FirstPt = New Point(Polyi.PolyPoint(0).X * width, Polyi.PolyPoint(0).Y * height)
        minTempDis = CalcDistBetweenPoints(FirstPt, mPt)

        For j = 0 To Polyi.PolyPointIndx - 2
            Dim StartPt = New Point(Polyi.PolyPoint(j).X * width, Polyi.PolyPoint(j).Y * height)
            Dim EndPt = New Point(Polyi.PolyPoint(j + 1).X * width, Polyi.PolyPoint(j + 1).Y * height)

            tempDis = CalcDistFromPointToLine(StartPt, EndPt, mPt)

            If minTempDis > tempDis Then
                minTempDis = tempDis
            End If
        Next
        For j = 1 To Polyi.PolyPointIndx
            Dim StartPt = New Point(Polyi.PolyPoint(j).X * width, Polyi.PolyPoint(j).Y * height)
            tempDis = CalcDistBetweenPoints(StartPt, mPt)

            If minTempDis > tempDis Then minTempDis = tempDis
        Next
        Return minTempDis
    End Function
End Module
