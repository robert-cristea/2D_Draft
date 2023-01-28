
Imports System.Drawing


''' <summary>
''' This class contains utilities of Curves.
''' </summary>
Public Module C_Utils


    ''' <summary>
    ''' get center point of medium edge of polygen.
    ''' </summary>
    ''' <paramname="PolyA">The index of polygen object.</param>
    Function PolyGetPos(PolyA As PolyObj) As PointF
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
    Function CuPolyGetPos(CuPolyA As CuPolyObj) As PointF
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
    Function LGetPos(LA As LineObj) As PointF
        Dim tempP As PointF
        tempP.X = (LA.FirstPointOfLine.X + LA.SecndPointOfLine.X) / 2
        tempP.Y = (LA.FirstPointOfLine.Y + LA.SecndPointOfLine.Y) / 2
        Return tempP
    End Function

    ''' <summary>
    ''' get center point of Curve object.
    ''' </summary>
    ''' <paramname="CA">The index of Curve object.</param>
    Function CGetPos(CA As CurveObj) As PointF

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
    Sub Clear(ByRef C_line As LineObj)
        Dim tempPt = New PointF(0, 0)
        C_line.FirstPointOfLine = tempPt
        C_line.SecndPointOfLine = tempPt
        C_line.LDrawPos = tempPt
    End Sub

    ''' <summary>
    ''' clear C_Point Object.
    ''' </summary>
    ''' <paramname="C_Point">The C_Point object.</param>
    Sub Clear(ByRef C_Point As PointObj)
        Dim tempPt = New PointF(0, 0)
        C_Point.PointPoint = tempPt
        C_Point.PDrawPos = tempPt
    End Sub

    ''' <summary>
    ''' clear C_Poly Object.
    ''' </summary>
    ''' <paramname="C_Poly">The C_Poly object.</param>
    Sub Clear(ByRef C_Poly As PolyObj)
        Dim tempPt = New PointF(0, 0)
        C_Poly.PolyDrawPos = tempPt
        C_Poly.PolyPointIndx = 0
        For i = 0 To 49
            C_Poly.PolyPoint(i) = tempPt
        Next
    End Sub

    ''' <summary>
    ''' Clone C_Poly Object.
    ''' </summary>
    ''' <paramname="C_Poly">The C_Poly object.</param>
    Function ClonePolyObj(ByVal C_Poly As PolyObj) As PolyObj
        Dim Obj As PolyObj = New PolyObj()
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
    Function CloneLineObj(ByVal Line As LineObj) As LineObj
        Dim Obj As LineObj = New LineObj()
        Obj.FirstPointOfLine = Line.FirstPointOfLine
        Obj.SecndPointOfLine = Line.SecndPointOfLine
        Obj.LDrawPos = Line.LDrawPos

        Return Obj
    End Function

    ''' <summary>
    ''' Clone C_Curve Object.
    ''' </summary>
    ''' <paramname="Src">The source object.</param>
    Function CloneCurveObj(ByVal Src As CurveObj) As CurveObj
        Dim Obj As CurveObj = New CurveObj()
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
    Function ClonePointObj(ByVal Src As PointObj) As PointObj
        Dim Obj As PointObj = New PointObj()
        Obj.PDrawPos = Src.PDrawPos
        Obj.PointPoint = Src.PointPoint
        Return Obj
    End Function

    ''' <summary>
    ''' Clone C_Cupoly Object.
    ''' </summary>
    ''' <paramname="Src">The source object.</param>
    Function CloneCuPolyObj(ByVal Src As CuPolyObj) As CuPolyObj
        Dim Obj As CuPolyObj = New CuPolyObj()
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
    Sub Clear(ByRef C_Curve As CurveObj)
        Dim tempPt = New PointF(0, 0)
        C_Curve.CDrawPos = tempPt
        C_Curve.CPointIndx = 0
        For i = 0 To 9999
            C_Curve.CurvePoint(i) = tempPt
        Next
    End Sub

    ''' <summary>
    ''' clear C_CuPoly Object.
    ''' </summary>
    ''' <paramname="C_CuPoly">The C_CuPoly object.</param>
    Sub Clear(ByRef C_CuPoly As CuPolyObj)
        Dim tempPt = New PointF(0, 0)
        C_CuPoly.CuPolyDrawPos = tempPt
        C_CuPoly.CuPolyPointIndx_j = 0
        For i = 0 To 29
            For j = 0 To 9999
                C_CuPoly.CuPolyPoint(i, j) = tempPt
            Next
            C_CuPoly.CuPolyPointIndx_k(i) = 0
        Next
    End Sub

    ''' <summary>
    ''' clear Curve Object.
    ''' </summary>
    ''' <paramname="Curve">The Curve object.</param>
    Sub Clear(ByRef Curve As CurveObject)
        Curve.CuPolyItem.Clear()
        Curve.PolyItem.Clear()
        Curve.PointItem.Clear()
        Curve.LineItem.Clear()
        Curve.CurveItem.Clear()
    End Sub

    Sub SetLineAndFont(ByRef item As MeasureObject, ByVal line_infor As LineStyle, ByVal font_infor As FontInfor)
        item.lineInfor.line_style = line_infor.line_style
        item.lineInfor.line_width = line_infor.line_width
        item.lineInfor.line_color = line_infor.line_color
        item.fontInfor.font_color = font_infor.font_color
        item.fontInfor.text_font = font_infor.text_font
    End Sub

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
    ''' calculate distance between two points.
    ''' </summary>
    ''' <paramname="X1">X-coordinate of first point.</param>
    ''' <paramname="Y1">Y-coordinate of first point.</param>
    ''' <paramname="X2">X-coordinate of second point.</param>
    ''' <paramname="Y2">Y-coordinate of second point.</param>
    Public Function Find_TwoPointDistance(X1 As Integer, Y1 As Integer, X2 As Integer, Y2 As Integer) As Integer
        Dim distance As Integer
        distance = Math.Round(Math.Sqrt((X1 - X2) * (X1 - X2) + (Y1 - Y2) * (Y1 - Y2)))
        Return distance
    End Function

    ''' <summary>
    ''' get Y-coordinate of Point lies in certain line.
    ''' </summary>
    ''' <paramname="P1">first point of line.</param>
    ''' <paramname="P2">second point of line.</param>
    ''' <paramname="x3">X-coordinate of target point.</param>
    Public Function GetLineEq(P1 As Point, P2 As Point, x3 As Integer) As Integer
        Dim kk As Double
        Dim bb As Double
        kk = (P1.Y - P2.Y) / (P1.X - P2.X)
        bb = P1.Y - kk * P1.X
        Return Math.Round(kk * x3 + bb)
    End Function

    ''' <summary>
    ''' calculate distance between point and line.
    ''' </summary>
    ''' <paramname="X1">X-coordinate of first point of line.</param>
    ''' <paramname="Y1">Y-coordinate of first point of line.</param>
    ''' <paramname="X2">X-coordinate of second point of line.</param>
    ''' <paramname="Y2">Y-coordinate of second point of line.</param>
    ''' <paramname="Xp">X-coordinate of target point.</param>
    ''' <paramname="Yp">Y-coordinate of target point.</param>
    Public Function Find_BPointLineDistance(X1 As Integer, Y1 As Integer, X2 As Integer, Y2 As Integer, Xp As Integer, Yp As Integer) As Integer
        Dim a, b, a1, b1, Xs, Ys As Double
        Dim distance As Integer
        Main_Form.OutPointFlag = False
        If Y2 <> Y1 Then
            If X2 <> X1 Then
                a = (Y2 - Y1) / (X2 - X1) : b = Y1 - a * X1
                a1 = -1 / a : b1 = Yp - a1 * Xp
                Xs = (b - b1) / (a1 - a) : Ys = a1 * Xs + b1
                distance = Math.Round(Math.Sqrt((Xp - Xs) * (Xp - Xs) + (Yp - Ys) * (Yp - Ys)))
            Else
                Xs = X1 : Ys = Yp
                distance = Math.Abs(Xs - Xp)
            End If

            If Y1 < Y2 And Ys < Y1 Then Main_Form.OutPointFlag = True : Main_Form.DotX = X1 : Main_Form.DotY = Y1
            If Y2 < Y1 And Ys < Y2 Then Main_Form.OutPointFlag = True : Main_Form.DotX = X2 : Main_Form.DotY = Y2
            If Y1 > Y2 And Ys > Y1 Then Main_Form.OutPointFlag = True : Main_Form.DotX = X1 : Main_Form.DotY = Y1
            If Y2 > Y1 And Ys > Y2 Then Main_Form.OutPointFlag = True : Main_Form.DotX = X2 : Main_Form.DotY = Y2
        Else
            If X1 < X2 And Xs < X1 Then Main_Form.OutPointFlag = True : Main_Form.DotX = X1 : Main_Form.DotY = Y1
            If X2 < X1 And Xs < X2 Then Main_Form.OutPointFlag = True : Main_Form.DotX = X2 : Main_Form.DotY = Y2
            If X1 > X2 And Xs > X1 Then Main_Form.OutPointFlag = True : Main_Form.DotX = X1 : Main_Form.DotY = Y1
            If X2 > X1 And Xs > X2 Then Main_Form.OutPointFlag = True : Main_Form.DotX = X2 : Main_Form.DotY = Y2

            Ys = Y1 : Xs = Xp
            distance = Math.Abs(Ys - Yp)
        End If

        Main_Form.XsLinePoint = Xs : Main_Form.YsLinePoint = Ys
        If Main_Form.OutPointFlag = True Then distance = Find_TwoPointDistance(Main_Form.DotX, Main_Form.DotY, Xp, Yp) : Main_Form.XsLinePoint = Main_Form.DotX : Main_Form.YsLinePoint = Main_Form.DotY

        Return distance
    End Function

    ''' <summary>
    ''' calculate distance between point and line.
    ''' </summary>
    ''' <paramname="X1">X-coordinate of first point of line.</param>
    ''' <paramname="Y1">Y-coordinate of first point of line.</param>
    ''' <paramname="X2">X-coordinate of second point of line.</param>
    ''' <paramname="Y2">Y-coordinate of second point of line.</param>
    ''' <paramname="Xp">X-coordinate of target point.</param>
    ''' <paramname="Yp">Y-coordinate of target point.</param>
    Public Function pFind_BPointLineDistance(X1 As Integer, Y1 As Integer, X2 As Integer, Y2 As Integer, Xp As Integer, Yp As Integer) As Integer
        Dim a, b, a1, b1, Xs, Ys As Double
        Dim distance As Integer
        Main_Form.OutPointFlag = False
        If Y2 <> Y1 Then
            If X2 <> X1 Then
                a = (Y2 - Y1) / (X2 - X1) : b = Y1 - a * X1
                a1 = -1 / a : b1 = Yp - a1 * Xp
                Xs = (b - b1) / (a1 - a) : Ys = a1 * Xs + b1
                distance = Math.Round(Math.Sqrt((Xp - Xs) * (Xp - Xs) + (Yp - Ys) * (Yp - Ys)))
            Else
                Xs = X1 : Ys = Yp
                distance = Math.Abs(Xs - Xp)
            End If

            If Y1 < Y2 And Ys < Y1 Then Main_Form.OutPointFlag = True : Main_Form.DotX = X1 : Main_Form.DotY = Y1
            If Y2 < Y1 And Ys < Y2 Then Main_Form.OutPointFlag = True : Main_Form.DotX = X2 : Main_Form.DotY = Y2
            If Y1 > Y2 And Ys > Y1 Then Main_Form.OutPointFlag = True : Main_Form.DotX = X1 : Main_Form.DotY = Y1
            If Y2 > Y1 And Ys > Y2 Then Main_Form.OutPointFlag = True : Main_Form.DotX = X2 : Main_Form.DotY = Y2
        Else
            If X1 < X2 And Xs < X1 Then Main_Form.OutPointFlag = True : Main_Form.DotX = X1 : Main_Form.DotY = Y1
            If X2 < X1 And Xs < X2 Then Main_Form.OutPointFlag = True : Main_Form.DotX = X2 : Main_Form.DotY = Y2
            If X1 > X2 And Xs > X1 Then Main_Form.OutPointFlag = True : Main_Form.DotX = X1 : Main_Form.DotY = Y1
            If X2 > X1 And Xs > X2 Then Main_Form.OutPointFlag = True : Main_Form.DotX = X2 : Main_Form.DotY = Y2

            Ys = Y1 : Xs = Xp
            distance = Math.Abs(Ys - Yp)
        End If
        Main_Form.XsLinePoint = Xs : Main_Form.YsLinePoint = Ys
        'If OutPointFlag = True Then distance = Find_TwoPointDistance(DotX, DotY, Xp, Yp) : XsLinePoint = DotX : YsLinePoint = DotY

        Return distance
    End Function

    ''' <summary>
    ''' calculate distance between point and polygen.
    ''' </summary>
    ''' <paramname="Polyi">The polygen object.</param>
    ''' <paramname="m_pt">coordinate of target point.</param>
    ''' <paramname="width">the width of picturebox.</param>
    ''' <paramname="width">the height of picturebox.</param>
    Public Function Find_BPointPolyDistance(Polyi As PolyObj, mPt As Point, width As Integer, height As Integer) As Integer
        Dim tempDis, minTempDis As Integer
        Dim FirstPt = New Point(Polyi.PolyPoint(0).X * width, Polyi.PolyPoint(0).Y * height)
        minTempDis = CalcDistBetweenPoints(FirstPt, mPt)

        For j = 0 To Polyi.PolyPointIndx - 2
            Dim StartPt = New Point(Polyi.PolyPoint(j).X * width, Polyi.PolyPoint(j).Y * height)
            Dim EndPt = New Point(Polyi.PolyPoint(j + 1).X * width, Polyi.PolyPoint(j + 1).Y * height)

            tempDis = Find_BPointLineDistance(StartPt.X, StartPt.Y, EndPt.X, EndPt.Y, mPt.X, mPt.Y)

            If Main_Form.OutPointFlag = False Then
                If minTempDis > tempDis Then
                    minTempDis = tempDis
                    Main_Form.PXs = Main_Form.XsLinePoint
                    Main_Form.PYs = Main_Form.YsLinePoint
                End If
            End If
        Next
        For j = 1 To Polyi.PolyPointIndx
            Dim StartPt = New Point(Polyi.PolyPoint(j).X * width, Polyi.PolyPoint(j).Y * height)
            tempDis = CalcDistBetweenPoints(StartPt, mPt)

            If minTempDis > tempDis Then
                minTempDis = tempDis
                Main_Form.PXs = StartPt.X
                Main_Form.PYs = StartPt.Y
            End If
        Next
        Return minTempDis
    End Function

    ''' <summary>
    ''' calculate distance between point and polygen.
    ''' </summary>
    ''' <paramname="Polyi">The polygen object.</param>
    ''' <paramname="m_pt">coordinate of target point.</param>
    ''' <paramname="width">the width of picturebox.</param>
    ''' <paramname="width">the height of picturebox.</param>
    Public Function pFind_BPointPolyDistance(Polyi As PolyObj, mPt As Point, width As Integer, height As Integer) As Integer
        Dim tempDis, minTempDis As Integer
        Dim FirstPolyPoint = New Point(Polyi.PolyPoint(0).X * width, Polyi.PolyPoint(0).Y * height)
        Dim SecondPolyPoint = New Point(Polyi.PolyPoint(1).X * width, Polyi.PolyPoint(1).Y * height)

        minTempDis = pFind_BPointLineDistance(FirstPolyPoint.X, FirstPolyPoint.Y, SecondPolyPoint.X, SecondPolyPoint.Y, mPt.X, mPt.Y)
        Main_Form.POutFlag = Main_Form.OutPointFlag
        Main_Form.PXs = Main_Form.XsLinePoint
        Main_Form.PYs = Main_Form.YsLinePoint
        Main_Form.PDotX = Main_Form.DotX
        Main_Form.PDotY = Main_Form.DotY
        For j = 0 To Polyi.PolyPointIndx - 1
            Dim PolyPoint1 = New Point(Polyi.PolyPoint(j).X * width, Polyi.PolyPoint(j).Y * height)
            Dim PolyPoint2 = New Point(Polyi.PolyPoint(j + 1).X * width, Polyi.PolyPoint(j + 1).Y * height)
            tempDis = pFind_BPointLineDistance(PolyPoint1.X, PolyPoint1.Y, PolyPoint2.X, PolyPoint2.Y, mPt.X, mPt.Y)
            If minTempDis > tempDis Then
                minTempDis = tempDis
                Main_Form.PXs = Main_Form.XsLinePoint
                Main_Form.PYs = Main_Form.YsLinePoint
                Main_Form.PDotX = Main_Form.DotX
                Main_Form.PDotY = Main_Form.DotY
                Main_Form.POutFlag = Main_Form.OutPointFlag
            End If
        Next
        Return minTempDis
    End Function

    ''' <summary>
    ''' calculate perpendicular distance between point and polygen.
    ''' </summary>
    ''' <paramname="Polyi">The polygen object.</param>
    ''' <paramname="m_pt">coordinate of target point.</param>
    ''' <paramname="width">the width of picturebox.</param>
    ''' <paramname="width">the height of picturebox.</param>
    Public Function pFind_BPointPolyMaxDistance(Polyi As PolyObj, mPt As Point, width As Integer, height As Integer) As Integer
        Dim tempDis, maxTempDis As Integer
        Main_Form.POutFlag = False
        Dim FirstPolyPoint = New Point(Polyi.PolyPoint(0).X * width, Polyi.PolyPoint(0).Y * height)
        Dim SecondPolyPoint = New Point(Polyi.PolyPoint(1).X * width, Polyi.PolyPoint(1).Y * height)
        maxTempDis = pFind_BPointLineDistance(FirstPolyPoint.X, FirstPolyPoint.Y, SecondPolyPoint.X, SecondPolyPoint.Y, mPt.X, mPt.Y)
        Main_Form.POutFlag = Main_Form.OutPointFlag
        Main_Form.PXs = Main_Form.XsLinePoint
        Main_Form.PYs = Main_Form.YsLinePoint
        Main_Form.PDotX = Main_Form.DotX
        Main_Form.PDotY = Main_Form.DotY
        For j = 0 To Polyi.PolyPointIndx - 1
            Dim PolyPoint1 = New Point(Polyi.PolyPoint(j).X * width, Polyi.PolyPoint(j).Y * height)
            Dim PolyPoint2 = New Point(Polyi.PolyPoint(j + 1).X * width, Polyi.PolyPoint(j + 1).Y * height)
            tempDis = pFind_BPointLineDistance(PolyPoint1.X, PolyPoint1.Y, PolyPoint2.X, PolyPoint2.Y, mPt.X, mPt.Y)
            If maxTempDis < tempDis Then
                maxTempDis = tempDis
                Main_Form.PXs = Main_Form.XsLinePoint
                Main_Form.PYs = Main_Form.YsLinePoint
                Main_Form.PDotX = Main_Form.DotX
                Main_Form.PDotY = Main_Form.DotY
                Main_Form.POutFlag = Main_Form.OutPointFlag
            End If
        Next
        Return maxTempDis
    End Function
End Module
