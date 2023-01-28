
Imports System.Drawing
Imports System.Runtime.CompilerServices
Imports System.Windows.Forms.DataFormats
Imports System.Windows.Interop
Imports DocumentFormat.OpenXml.Bibliography
Imports DocumentFormat.OpenXml.Vml.Office
Imports Emgu.CV
Imports Emgu.CV.XFeatures2D
''' <summary>
''' This class contains all the functions that been  attched to Controls of Curves.
''' </summary>
Public Module C_ControlMethods

#Region "PictureBox Methods"

    ''' <summary>
    ''' draw object to picture box control.
    ''' </summary>
    ''' <paramname="graph">The graphics for drawing object list.</param>
    ''' <paramname="pictureBox">The pictureBox control in which you want to draw object list.</param>
    ''' <paramname="item">The object which you are going to draw.</param>
    ''' <paramname="digit">The digit of decimal numbers.</param>
    ''' <paramname="CF">The factor of measuring scale.</param>
    ''' <paramname="flag">The flag specifies whether the item is selected or not.</param>
    Public Sub DrawCurveObjItem(ByVal graph As Graphics, ByVal pictureBox As PictureBox, ByVal item As MeasureObject, digit As Integer, CF As Double, ByVal flag As Boolean)
        Dim graphPen As Pen
        If flag = True Then
            graphPen = New Pen(Color.Red, item.lineInfor.line_width)
        Else
            graphPen = New Pen(item.lineInfor.line_color, item.lineInfor.line_width)
        End If
        Dim PenRed = New Pen(Color.Red, item.lineInfor.line_width)

        Dim graphFont = item.fontInfor.text_font
        Dim graphBrush As SolidBrush = New SolidBrush(item.fontInfor.font_color)

        If item.measuringType = MeasureType.objLine Then
            Dim obj = item.curveObject.LineItem(0)
            Dim FirstPt As Point = New Point(obj.FirstPointOfLine.X * pictureBox.Width, obj.FirstPointOfLine.Y * pictureBox.Height)
            Dim SecondPt As Point = New Point(obj.SecndPointOfLine.X * pictureBox.Width, obj.SecndPointOfLine.Y * pictureBox.Height)
            If item.objNum = Main_Form.LRealSelectArrayIndx Then
                graph.DrawLine(PenRed, CInt(FirstPt.X), CInt(FirstPt.Y), CInt(SecondPt.X), CInt(SecondPt.Y))
            Else
                graph.DrawLine(graphPen, CInt(FirstPt.X), CInt(FirstPt.Y), CInt(SecondPt.X), CInt(SecondPt.Y))
            End If

            'draw label
            Dim DrawPt = New Point(CInt(obj.LDrawPos.X * pictureBox.Width), CInt(obj.LDrawPos.Y * pictureBox.Height))
            Dim textSize As SizeF = graph.MeasureString(item.name, graphFont)
            graph.DrawString(item.name, graphFont, graphBrush, New RectangleF(DrawPt.X, DrawPt.Y, textSize.Width, textSize.Height))

        ElseIf item.measuringType = MeasureType.objPoint Then
            Dim obj = item.curveObject.PointItem(0)
            Dim PointPt = New Point(CInt(obj.PointPoint.X * pictureBox.Width), CInt(obj.PointPoint.Y * pictureBox.Height))
            If item.objNum = Main_Form.PRealSelectArrayIndx Then
                graph.DrawEllipse(PenRed, New Rectangle(PointPt.X, PointPt.Y, 2, 2))
            Else
                graph.DrawEllipse(graphPen, New Rectangle(PointPt.X, PointPt.Y, 2, 2))
            End If
            Dim DrawPt = New Point(CInt(obj.PDrawPos.X * pictureBox.Width), CInt(obj.PDrawPos.Y * pictureBox.Height))
            Dim textSize As SizeF = graph.MeasureString(item.name, graphFont)
            graph.DrawString(item.name, graphFont, graphBrush, New RectangleF(DrawPt.X, DrawPt.Y, textSize.Width, textSize.Height))

        ElseIf item.measuringType = MeasureType.objPoly Then
            Dim obj = item.curveObject.PolyItem(0)
            For i = 0 To obj.PolyPointIndx - 1
                Dim startPt = New Point(CInt(obj.PolyPoint(i).X * pictureBox.Width), CInt(obj.PolyPoint(i).Y * pictureBox.Height))
                Dim EndPt = New Point(CInt(obj.PolyPoint(i + 1).X * pictureBox.Width), CInt(obj.PolyPoint(i + 1).Y * pictureBox.Height))
                If item.objNum = Main_Form.PolyRealSelectArrayIndx Then
                    graph.DrawLine(PenRed, CInt(startPt.X), CInt(startPt.Y), CInt(EndPt.X), CInt(EndPt.Y))
                Else
                    graph.DrawLine(graphPen, CInt(startPt.X), CInt(startPt.Y), CInt(EndPt.X), CInt(EndPt.Y))
                End If
            Next
            Dim DrawPt = New Point(CInt(obj.PolyDrawPos.X * pictureBox.Width), CInt(obj.PolyDrawPos.Y * pictureBox.Height))
            Dim textSize As SizeF = graph.MeasureString(item.name, graphFont)
            graph.DrawString(item.name, graphFont, graphBrush, New RectangleF(DrawPt.X, DrawPt.Y, textSize.Width, textSize.Height))


        ElseIf item.measuringType = MeasureType.objCurve Then
            Dim obj = item.curveObject.CurveItem(0)
            For i = 0 To obj.CPointIndx - 1
                Dim startPt = New Point(CInt(obj.CurvePoint(i).X * pictureBox.Width), CInt(obj.CurvePoint(i).Y * pictureBox.Height))
                Dim EndPt = New Point(CInt(obj.CurvePoint(i + 1).X * pictureBox.Width), CInt(obj.CurvePoint(i + 1).Y * pictureBox.Height))
                If item.objNum = Main_Form.CRealSelectArrayIndx Then
                    graph.DrawLine(PenRed, CInt(startPt.X), CInt(startPt.Y), CInt(EndPt.X), CInt(EndPt.Y))
                Else
                    graph.DrawLine(graphPen, CInt(startPt.X), CInt(startPt.Y), CInt(EndPt.X), CInt(EndPt.Y))
                End If
            Next
            Dim DrawPt = New Point(CInt(obj.CDrawPos.X * pictureBox.Width), CInt(obj.CDrawPos.Y * pictureBox.Height))
            Dim textSize As SizeF = graph.MeasureString(item.name, graphFont)
            graph.DrawString(item.name, graphFont, graphBrush, New RectangleF(DrawPt.X, DrawPt.Y, textSize.Width, textSize.Height))

        ElseIf item.measuringType = MeasureType.objCuPoly Then
            Dim obj = item.curveObject.CuPolyItem(0)
            For i = 1 To obj.CuPolyPointIndx_j
                For j = 0 To obj.CuPolyPointIndx_k(i) - 2
                    Dim startPt = New Point(CInt(obj.CuPolyPoint(i, j).X * pictureBox.Width), CInt(obj.CuPolyPoint(i, j).Y * pictureBox.Height))
                    Dim EndPt = New Point(CInt(obj.CuPolyPoint(i, j + 1).X * pictureBox.Width), CInt(obj.CuPolyPoint(i, j + 1).Y * pictureBox.Height))
                    If item.objNum = Main_Form.CuPolyRealSelectArrayIndx Then
                        graph.DrawLine(PenRed, CInt(startPt.X), CInt(startPt.Y), CInt(EndPt.X), CInt(EndPt.Y))
                    Else
                        graph.DrawLine(graphPen, CInt(startPt.X), CInt(startPt.Y), CInt(EndPt.X), CInt(EndPt.Y))
                    End If
                Next
                If i > 1 Then
                    Dim k_index = 0
                    If obj.CuPolyPointIndx_k(i - 1) > 1 Then
                        k_index = obj.CuPolyPointIndx_k(i - 1) - 1
                    End If
                    Dim startPt = New Point(CInt(obj.CuPolyPoint(i - 1, k_index).X * pictureBox.Width), CInt(obj.CuPolyPoint(i - 1, k_index).Y * pictureBox.Height))
                    Dim EndPt = New Point(CInt(obj.CuPolyPoint(i, 0).X * pictureBox.Width), CInt(obj.CuPolyPoint(i, 0).Y * pictureBox.Height))
                    If item.objNum = Main_Form.CuPolyRealSelectArrayIndx Then
                        graph.DrawLine(PenRed, CInt(startPt.X), CInt(startPt.Y), CInt(EndPt.X), CInt(EndPt.Y))
                    Else
                        graph.DrawLine(graphPen, CInt(startPt.X), CInt(startPt.Y), CInt(EndPt.X), CInt(EndPt.Y))
                    End If
                End If
            Next
            Dim DrawPt = New Point(CInt(obj.CuPolyDrawPos.X * pictureBox.Width), CInt(obj.CuPolyDrawPos.Y * pictureBox.Height))
            Dim textSize As SizeF = graph.MeasureString(item.name, graphFont)
            graph.DrawString(item.name, graphFont, graphBrush, New RectangleF(DrawPt.X, DrawPt.Y, textSize.Width, textSize.Height))

        ElseIf item.measuringType = MeasureType.objMinMax Then
            Dim startPt = New Point(item.startPoint.X * pictureBox.Width, item.startPoint.Y * pictureBox.Height)
            Dim EndPt = New Point(item.endPoint.X * pictureBox.Width, item.endPoint.Y * pictureBox.Height)
            graph.DrawLine(graphPen, CInt(startPt.X), CInt(startPt.Y), CInt(EndPt.X), CInt(EndPt.Y))
            If item.dotFlag Then
                Dim MidPt = New Point(item.middlePoint.X * pictureBox.Width, item.middlePoint.Y * pictureBox.Height)
                graphPen.DashStyle = Drawing2D.DashStyle.Dot
                graph.DrawLine(graphPen, CInt(startPt.X), CInt(startPt.Y), CInt(MidPt.X), CInt(MidPt.Y))
            End If
            Dim DrawPt = New Point((startPt.X + EndPt.X) / 2, (startPt.Y + EndPt.Y) / 2)
            Dim length = GetDecimalNumber(item.length, digit, CF)
            If Main_Form.showLegend = True Then
                Dim output = item.name + " " + length.ToString()
                Dim textSize As SizeF = graph.MeasureString(output, graphFont)
                graph.DrawString(output, graphFont, graphBrush, New RectangleF(DrawPt.X, DrawPt.Y, textSize.Width, textSize.Height))
            Else
                Dim output = item.name
                Dim textSize As SizeF = graph.MeasureString(output, graphFont)
                graph.DrawString(output, graphFont, graphBrush, New RectangleF(DrawPt.X, DrawPt.Y, textSize.Width, textSize.Height))
            End If
        End If
        PenRed.Dispose()
        graphBrush.Dispose()
        graphPen.Dispose()
    End Sub

    ''' <summary>
    ''' draw C_Line Object.
    ''' </summary>
    ''' <paramname="pictureBox">The pictureBox control in which you want to draw object list.</param>
    ''' <paramname="LineObj">The LineObj you are going to draw.</param>
    Public Sub DrawLineObject(ByVal pictureBox As PictureBox, ByVal LineObj As LineObj)
        Dim graph As Graphics = pictureBox.CreateGraphics()
        Dim graphPen = New Pen(Color.Red, 2)
        Dim startPt = New Point(LineObj.FirstPointOfLine.X * pictureBox.Width, LineObj.FirstPointOfLine.Y * pictureBox.Height)
        Dim EndPt = New Point(LineObj.SecndPointOfLine.X * pictureBox.Width, LineObj.SecndPointOfLine.Y * pictureBox.Height)
        graph.DrawLine(graphPen, startPt, EndPt)
        graphPen.Dispose()
        graph.Dispose()
    End Sub

    ''' <summary>
    ''' draw line between two points.
    ''' </summary>
    ''' <paramname="pictureBox">The pictureBox control in which you want to draw object list.</param>
    ''' <paramname="line_infor">The information about drawing line.</param>
    ''' <paramname="pt1">The first point to draw.</param>
    ''' <paramname="pt2">The second point to draw.</param>
    Public Sub DrawLineBetweenTwoPoints(ByVal pictureBox As PictureBox, ByVal line_infor As LineStyle, ByVal pt1 As Point, ByVal pt2 As Point)
        Dim graphPen As Pen = New Pen(line_infor.line_color, line_infor.line_width)
        Dim graph As Graphics = pictureBox.CreateGraphics()
        graph.DrawLine(graphPen, pt1, pt2)
        graph.Dispose()
    End Sub

    ''' <summary>
    ''' draw poly object.
    ''' </summary>
    ''' <paramname="pictureBox">The pictureBox control in which you want to draw object list.</param>
    ''' <paramname="line_infor">The information about drawing line.</param>
    ''' <paramname="Obj">The poly object.</param>
    Public Sub DrawPolyObj(ByVal pictureBox As PictureBox, ByVal line_infor As LineStyle, ByVal Obj As PolyObj)
        Dim graphPen As Pen = New Pen(line_infor.line_color, line_infor.line_width)
        Dim graph As Graphics = pictureBox.CreateGraphics()
        For i = 0 To Obj.PolyPointIndx - 1
            Dim pt1 = New Point(Obj.PolyPoint(i).X * pictureBox.Width, Obj.PolyPoint(i).Y * pictureBox.Height)
            Dim pt2 = New Point(Obj.PolyPoint(i + 1).X * pictureBox.Width, Obj.PolyPoint(i + 1).Y * pictureBox.Height)
            graph.DrawLine(graphPen, pt1, pt2)
        Next
        graph.Dispose()
    End Sub

    ''' <summary>
    ''' draw curve object.
    ''' </summary>
    ''' <paramname="pictureBox">The pictureBox control in which you want to draw object list.</param>
    ''' <paramname="line_infor">The information about drawing line.</param>
    ''' <paramname="obj">The curve object.</param>
    Public Sub DrawCurveObj(ByVal pictureBox As PictureBox, ByVal line_infor As LineStyle, ByVal Obj As CurveObj)
        Dim graphPen As Pen = New Pen(line_infor.line_color, line_infor.line_width)
        Dim graph As Graphics = pictureBox.CreateGraphics()
        For i = 0 To Obj.CPointIndx - 1
            Dim pt1 = New Point(Obj.CurvePoint(i).X * pictureBox.Width, Obj.CurvePoint(i).Y * pictureBox.Height)
            Dim pt2 = New Point(Obj.CurvePoint(i + 1).X * pictureBox.Width, Obj.CurvePoint(i + 1).Y * pictureBox.Height)
            graph.DrawLine(graphPen, pt1, pt2)
        Next
        graph.Dispose()
    End Sub

    ''' <summary>
    ''' draw Cupoly object.
    ''' </summary>
    ''' <paramname="pictureBox">The pictureBox control in which you want to draw object list.</param>
    ''' <paramname="line_infor">The information about drawing line.</param>
    ''' <paramname="obj">The cupoly object.</param>
    Public Sub DrawCuPolyObj(ByVal pictureBox As PictureBox, ByVal line_infor As LineStyle, ByVal Obj As CuPolyObj)
        Dim graphPen As Pen = New Pen(line_infor.line_color, line_infor.line_width)
        Dim graph As Graphics = pictureBox.CreateGraphics()

        For i = 1 To Obj.CuPolyPointIndx_j
            For j = 0 To Obj.CuPolyPointIndx_k(i) - 2
                Dim startPt = New Point(CInt(Obj.CuPolyPoint(i, j).X * pictureBox.Width), CInt(Obj.CuPolyPoint(i, j).Y * pictureBox.Height))
                Dim EndPt = New Point(CInt(Obj.CuPolyPoint(i, j + 1).X * pictureBox.Width), CInt(Obj.CuPolyPoint(i, j + 1).Y * pictureBox.Height))
                graph.DrawLine(graphPen, CInt(startPt.X), CInt(startPt.Y), CInt(EndPt.X), CInt(EndPt.Y))
            Next
            If i > 1 Then
                Dim k_index = 0
                If Obj.CuPolyPointIndx_k(i - 1) > 1 Then
                    k_index = Obj.CuPolyPointIndx_k(i - 1) - 1
                End If
                Dim startPt = New Point(CInt(Obj.CuPolyPoint(i - 1, k_index).X * pictureBox.Width), CInt(Obj.CuPolyPoint(i - 1, k_index).Y * pictureBox.Height))
                Dim EndPt = New Point(CInt(Obj.CuPolyPoint(i, 0).X * pictureBox.Width), CInt(Obj.CuPolyPoint(i, 0).Y * pictureBox.Height))
                graph.DrawLine(graphPen, CInt(startPt.X), CInt(startPt.Y), CInt(EndPt.X), CInt(EndPt.Y))
            End If
        Next
        graph.Dispose()
    End Sub

    ''' <summary>
    ''' check if there is any item in the pos where mouse clicked and return the number of object
    ''' </summary>
    ''' <paramname="pictureBox">The pictureBox control in which you want to draw object list.</param>
    ''' <paramname="m_pt">The point of mouse cursor.</param>
    ''' <paramname="obj_list">The list of objects.</param>
    Public Function CheckCurveItemInPos(ByVal pictureBox As PictureBox, ByVal m_pt As PointF, ByVal obj_list As List(Of MeasureObject)) As Integer
        Dim index = -1
        Dim mPt = New Point(m_pt.X * pictureBox.Width, m_pt.Y * pictureBox.Height)
        For Each item In obj_list
            If item.measuringType < MeasureType.objLine Then
                Continue For
            End If
            If item.measuringType = MeasureType.objCuPoly Then
                Dim obj = item.curveObject.CuPolyItem(0)
                For i = 0 To obj.CuPolyPointIndx_j
                    For j = 0 To obj.CuPolyPointIndx_k(i) - 1
                        If m_pt.X > obj.CuPolyPoint(i, j).X - 0.01 And m_pt.X < obj.CuPolyPoint(i, j).X + 0.01 And m_pt.Y > obj.CuPolyPoint(i, j).Y - 0.01 And m_pt.Y < obj.CuPolyPoint(i, j).Y + 0.01 Then
                            index = item.objNum
                            Exit For
                        End If
                    Next

                    If i >= 1 Then
                        Dim startPt = New Point(obj.CuPolyPoint(i - 1, obj.CuPolyPointIndx_k(i - 1)).X * pictureBox.Width, obj.CuPolyPoint(i - 1, obj.CuPolyPointIndx_k(i - 1)).Y * pictureBox.Height)
                        Dim EndPt = New Point(obj.CuPolyPoint(i, 0).X * pictureBox.Width, obj.CuPolyPoint(i, 0).Y * pictureBox.Height)
                        Dim dist = Find_BPointLineDistance(startPt.X, startPt.Y, EndPt.X, EndPt.Y, mPt.X, mPt.Y)

                        If dist < 5 And Main_Form.OutPointFlag = False Then
                            index = item.objNum
                        End If
                    End If
                Next
            ElseIf item.measuringType = MeasureType.objCurve Then
                Dim obj = item.curveObject.CurveItem(0)
                For i = 0 To obj.CPointIndx - 1
                    If m_pt.X > obj.CurvePoint(i).X - 0.01 And m_pt.X < obj.CurvePoint(i).X + 0.01 And m_pt.Y > obj.CurvePoint(i).Y - 0.01 And m_pt.Y < obj.CurvePoint(i).Y + 0.01 Then
                        index = item.objNum
                        Exit For
                    End If
                Next
            ElseIf item.measuringType = MeasureType.objLine Then
                Dim obj = item.curveObject.LineItem(0)
                Dim startPt = New Point(obj.FirstPointOfLine.X * pictureBox.Width, obj.FirstPointOfLine.Y * pictureBox.Height)
                Dim EndPt = New Point(obj.SecndPointOfLine.X * pictureBox.Width, obj.SecndPointOfLine.Y * pictureBox.Height)
                Dim dist = Find_BPointLineDistance(startPt.X, startPt.Y, EndPt.X, EndPt.Y, mPt.X, mPt.Y)
                If dist < 5 And Main_Form.OutPointFlag = False Then
                    index = item.objNum
                End If
            ElseIf item.measuringType = MeasureType.objPoint Then
                Dim obj = item.curveObject.PointItem(0)
                If m_pt.X < obj.PointPoint.X + 0.01 And m_pt.X > obj.PointPoint.X - 0.01 And m_pt.Y < obj.PointPoint.Y + 0.01 And m_pt.Y > obj.PointPoint.Y - 0.01 Then
                    index = item.objNum
                End If
            ElseIf item.measuringType = MeasureType.objPoly Then
                Dim obj = item.curveObject.PolyItem(0)
                If obj.PolyPointIndx > 0 Then
                    Dim dist = Find_BPointPolyDistance(obj, mPt, pictureBox.Width, pictureBox.Height)
                    If dist < 5 Then
                        index = item.objNum
                    End If
                End If
            End If
            If index >= 0 Then
                Exit For
            End If
        Next

        Return index
    End Function

    ''' <summary>
    ''' draw object selected to picture box control.
    ''' </summary>
    ''' <paramname="pictureBox">The pictureBox control in which you want to draw object list.</param>
    ''' <paramname="item">The object which you are going to draw.</param>
    Public Sub DrawCurveObjSelected(ByVal pictureBox As PictureBox, ByVal item As MeasureObject, digit As Integer, CF As Double)
        Dim graph As Graphics = pictureBox.CreateGraphics()
        DrawCurveObjItem(graph, pictureBox, item, digit, CF, True)
        graph.Dispose()
    End Sub


#End Region



#Region "Min"
    ''' <summary>
    ''' calculate min distance between two objects.
    ''' </summary>
    ''' <paramname="Obj1">The selected object.</param>
    ''' <paramname="Obj2">The selected object.</param>
    ''' <paramname="width">The width of picturebox.</param>
    ''' <paramname="height">The height of picturebox.</param>
    Public Function CalcMinBetweenCuPolyAndLine(ByVal Obj1 As MeasureObject, ByVal Obj2 As MeasureObject, ByVal width As Integer, ByVal height As Integer) As MeasureObject
        Dim result = New MeasureObject()
        Dim dMinLineCurve, dLineCurve, minIndex_j, minIndex_k, MSx, MSy As Integer
        Dim LineMidExistFlag, CuPolyLOutflag As Boolean
        LineMidExistFlag = False : CuPolyLOutflag = False
        Dim CuPolyItem = Obj1.curveObject.CuPolyItem(0)
        Dim LineItem = Obj2.curveObject.LineItem(0)
        Dim FirstPointofLine = New Point(LineItem.FirstPointOfLine.X * width, LineItem.FirstPointOfLine.Y * height)
        Dim SecndPointOfLine = New Point(LineItem.SecndPointOfLine.X * width, LineItem.SecndPointOfLine.Y * height)
        Dim FirstEdgeOfCuPoly = New Point(CuPolyItem.CuPolyPoint(1, 0).X * width, CuPolyItem.CuPolyPoint(1, 0).Y * height)
        dMinLineCurve = Find_BPointLineDistance(FirstPointofLine.X, FirstPointofLine.Y, SecndPointOfLine.X, SecndPointOfLine.Y, FirstEdgeOfCuPoly.X, FirstEdgeOfCuPoly.Y)
        minIndex_j = 1 : minIndex_k = 0 : Main_Form.COutPointFlag = False
        For j = 1 To CuPolyItem.CuPolyPointIndx_j
            For k = 0 To CuPolyItem.CuPolyPointIndx_k(j)
                Dim KEdgeOfCuPoly = New Point(CuPolyItem.CuPolyPoint(j, k).X * width, CuPolyItem.CuPolyPoint(j, k).Y * height)
                dLineCurve = Find_BPointLineDistance(FirstPointofLine.X, FirstPointofLine.Y, SecndPointOfLine.X, SecndPointOfLine.Y, KEdgeOfCuPoly.X, KEdgeOfCuPoly.Y)
                If Main_Form.OutPointFlag = True Then Main_Form.COutPointFlag = True : Main_Form.CDotX = Main_Form.DotX : Main_Form.CDotY = Main_Form.DotY
                If dMinLineCurve > dLineCurve Then LineMidExistFlag = False : dMinLineCurve = dLineCurve : minIndex_j = j : minIndex_k = k : 
            Next
            If j > 1 Then
                Dim sP, eP As Point
                sP = New Point(CuPolyItem.CuPolyPoint(j - 1, CuPolyItem.CuPolyPointIndx_k(j - 1)).X * width, CuPolyItem.CuPolyPoint(j - 1, CuPolyItem.CuPolyPointIndx_k(j - 1)).Y * height)
                eP = New Point(CuPolyItem.CuPolyPoint(j, 0).X * width, CuPolyItem.CuPolyPoint(j, 0).Y * height)

                If sP.X = eP.X Then
                    If sP.Y < eP.Y Then
                        For mpy = sP.Y To eP.Y
                            dLineCurve = Find_BPointLineDistance(FirstPointofLine.X, FirstPointofLine.Y, SecndPointOfLine.X, SecndPointOfLine.Y, sP.X, mpy)
                            If dMinLineCurve > dLineCurve Then
                                LineMidExistFlag = True
                                dMinLineCurve = dLineCurve
                                MSx = sP.X
                                MSy = mpy
                                If Main_Form.OutPointFlag = True Then Main_Form.COutPointFlag = True : Main_Form.CDotX = Main_Form.DotX : Main_Form.CDotY = Main_Form.DotY
                            End If

                        Next
                    Else
                        For mpy = eP.Y To sP.Y
                            dLineCurve = Find_BPointLineDistance(FirstPointofLine.X, FirstPointofLine.Y, SecndPointOfLine.X, SecndPointOfLine.Y, sP.X, mpy)

                            If dMinLineCurve > dLineCurve Then
                                LineMidExistFlag = True
                                dMinLineCurve = dLineCurve
                                MSx = sP.X
                                MSy = mpy
                                If Main_Form.OutPointFlag = True Then Main_Form.COutPointFlag = True : Main_Form.CDotX = Main_Form.DotX : Main_Form.CDotY = Main_Form.DotY
                            End If

                        Next
                    End If
                ElseIf sP.Y = eP.Y Then
                    If sP.X < eP.X Then
                        For mpx = sP.X To eP.X
                            dLineCurve = Find_BPointLineDistance(FirstPointofLine.X, FirstPointofLine.Y, SecndPointOfLine.X, SecndPointOfLine.Y, mpx, sP.Y)

                            If dMinLineCurve > dLineCurve Then
                                LineMidExistFlag = True
                                dMinLineCurve = dLineCurve
                                MSx = mpx
                                MSy = sP.Y
                                If Main_Form.OutPointFlag = True Then Main_Form.COutPointFlag = True : Main_Form.CDotX = Main_Form.DotX : Main_Form.CDotY = Main_Form.DotY
                            End If
                        Next
                    Else
                        For mpX = eP.X To sP.X
                            dLineCurve = Find_BPointLineDistance(FirstPointofLine.X, FirstPointofLine.Y, SecndPointOfLine.X, SecndPointOfLine.Y, mpX, sP.Y)
                            If dMinLineCurve > dLineCurve Then
                                LineMidExistFlag = True
                                dMinLineCurve = dLineCurve
                                MSx = mpX
                                MSy = sP.Y
                                If Main_Form.OutPointFlag = True Then Main_Form.COutPointFlag = True : Main_Form.CDotX = Main_Form.DotX : Main_Form.CDotY = Main_Form.DotY
                            End If
                        Next
                    End If
                Else
                    If sP.X < eP.X Then
                        For mpx = sP.X To eP.X
                            dLineCurve = Find_BPointLineDistance(FirstPointofLine.X, FirstPointofLine.Y, SecndPointOfLine.X, SecndPointOfLine.Y, mpx, GetLineEq(sP, eP, mpx))

                            If dMinLineCurve > dLineCurve Then
                                LineMidExistFlag = True
                                dMinLineCurve = dLineCurve
                                MSx = mpx
                                MSy = GetLineEq(sP, eP, mpx)
                                If Main_Form.OutPointFlag = True Then Main_Form.COutPointFlag = True : Main_Form.CDotX = Main_Form.DotX : Main_Form.CDotY = Main_Form.DotY
                            End If
                        Next
                    Else
                        For mpX = eP.X To sP.X
                            dLineCurve = Find_BPointLineDistance(FirstPointofLine.X, FirstPointofLine.Y, SecndPointOfLine.X, SecndPointOfLine.Y, mpX, GetLineEq(sP, eP, mpX))

                            If dMinLineCurve > dLineCurve Then
                                LineMidExistFlag = True
                                dMinLineCurve = dLineCurve
                                MSx = mpX
                                MSy = GetLineEq(sP, eP, mpX)
                                If Main_Form.OutPointFlag = True Then Main_Form.COutPointFlag = True : Main_Form.CDotX = Main_Form.DotX : Main_Form.CDotY = Main_Form.DotY
                            End If
                        Next
                    End If
                End If
            End If
        Next

        If LineMidExistFlag = False Then
            Dim minEdge = New Point(CuPolyItem.CuPolyPoint(minIndex_j, minIndex_k).X * width, CuPolyItem.CuPolyPoint(minIndex_j, minIndex_k).Y * height)
            dLineCurve = Find_BPointLineDistance(FirstPointofLine.X, FirstPointofLine.Y, SecndPointOfLine.X, SecndPointOfLine.Y, minEdge.X, minEdge.Y)
            If Main_Form.OutPointFlag = True Then Main_Form.COutPointFlag = True : Main_Form.CDotX = Main_Form.DotX : Main_Form.CDotY = Main_Form.DotY
            MSx = minEdge.X : MSy = minEdge.Y
        Else
            dLineCurve = Find_BPointLineDistance(FirstPointofLine.X, FirstPointofLine.Y, SecndPointOfLine.X, SecndPointOfLine.Y, MSx, MSy)
            If Main_Form.OutPointFlag = True Then Main_Form.COutPointFlag = True : Main_Form.CDotX = Main_Form.DotX : Main_Form.CDotY = Main_Form.DotY
        End If

        result.name = Obj1.name & "To" & Obj2.name & "Min"
        result.length = dLineCurve
        result.measuringType = MeasureType.objMinMax
        result.startPoint = New PointF(MSx / CSng(width), MSy / CSng(height))
        result.endPoint = New PointF(Main_Form.XsLinePoint / CSng(width), Main_Form.YsLinePoint / CSng(height))
        Return result
    End Function

    ''' <summary>
    ''' calculate min distance between two objects.
    ''' </summary>
    ''' <paramname="Obj1">The selected object.</param>
    ''' <paramname="Obj2">The selected object.</param>
    ''' <paramname="width">The width of picturebox.</param>
    ''' <paramname="height">The height of picturebox.</param>
    Public Function CalcMinBetweenCuPolyAndPoint(ByVal Obj1 As MeasureObject, ByVal Obj2 As MeasureObject, ByVal width As Integer, ByVal height As Integer) As MeasureObject
        Dim result As MeasureObject = New MeasureObject()
        Dim dMinPointCuPoly, dPointCuPoly, minIndexPCuPoly_j, minIndexPCuPoly_k, minExistFlag, Cx, Cy As Integer
        Dim CuPolyItem = Obj1.curveObject.CuPolyItem(0)
        Dim PointItem = Obj2.curveObject.PointItem(0)

        Dim PointPoint = New Point(PointItem.PointPoint.X * width, PointItem.PointPoint.Y * height)
        Dim FirstEdgeOfCuPoly = New Point(CuPolyItem.CuPolyPoint(1, 0).X * width, CuPolyItem.CuPolyPoint(1, 0).Y * height)
        dMinPointCuPoly = Find_TwoPointDistance(PointPoint.X, PointPoint.Y, FirstEdgeOfCuPoly.X, FirstEdgeOfCuPoly.Y)
        minIndexPCuPoly_j = 1 : minIndexPCuPoly_k = 0
        minExistFlag = False
        For j = 1 To CuPolyItem.CuPolyPointIndx_j
            For k = 0 To CuPolyItem.CuPolyPointIndx_k(j)
                Dim KEdgeOfCuPoly = New Point(CuPolyItem.CuPolyPoint(j, k).X * width, CuPolyItem.CuPolyPoint(j, k).Y * height)
                dPointCuPoly = Find_TwoPointDistance(PointPoint.X, PointPoint.Y, KEdgeOfCuPoly.X, KEdgeOfCuPoly.Y)
                If dMinPointCuPoly > dPointCuPoly Then dMinPointCuPoly = dPointCuPoly : minIndexPCuPoly_j = j : minIndexPCuPoly_k = k
            Next
            If j > 1 Then
                Dim sP, eP As Point
                sP = New Point(CuPolyItem.CuPolyPoint(j - 1, CuPolyItem.CuPolyPointIndx_k(j - 1)).X * width, CuPolyItem.CuPolyPoint(j - 1, CuPolyItem.CuPolyPointIndx_k(j - 1)).Y * height)
                eP = New Point(CuPolyItem.CuPolyPoint(j, 0).X * width, CuPolyItem.CuPolyPoint(j, 0).Y * height)

                dPointCuPoly = Find_BPointLineDistance(sP.X, sP.Y, eP.X, eP.Y, PointPoint.X, PointPoint.Y)
                If dMinPointCuPoly > dPointCuPoly Then dMinPointCuPoly = dPointCuPoly : minExistFlag = True : Cx = Main_Form.XsLinePoint : Cy = Main_Form.YsLinePoint
            End If
        Next

        If minExistFlag Then
            result.endPoint = New PointF(Cx / CSng(width), Cy / CSng(height))
        Else
            result.endPoint = CuPolyItem.CuPolyPoint(minIndexPCuPoly_j, minIndexPCuPoly_k)
        End If
        result.name = Obj1.name & "To" & Obj2.name & "Min"
        result.startPoint = PointItem.PointPoint
        result.measuringType = MeasureType.objMinMax
        result.length = dMinPointCuPoly
        Return result
    End Function

    ''' <summary>
    ''' calculate min distance between two objects.
    ''' </summary>
    ''' <paramname="Obj1">The selected object.</param>
    ''' <paramname="Obj2">The selected object.</param>
    ''' <paramname="width">The width of picturebox.</param>
    ''' <paramname="height">The height of picturebox.</param>
    Public Function CalcMinBetweenCurveAndLine(ByVal Obj1 As MeasureObject, ByVal Obj2 As MeasureObject, ByVal width As Integer, ByVal height As Integer) As MeasureObject
        Dim result = New MeasureObject()
        Dim CurveItem = Obj1.curveObject.CurveItem(0)
        Dim LineItem = Obj2.curveObject.LineItem(0)
        Dim dMinLineCurve, dLineCurve, minIndex As Integer
        Dim FirstPointofLine = New Point(LineItem.FirstPointOfLine.X * width, LineItem.FirstPointOfLine.Y * height)
        Dim SecndPointOfLine = New Point(LineItem.SecndPointOfLine.X * width, LineItem.SecndPointOfLine.Y * height)
        Dim FirstCurvePoint = New Point(CurveItem.CurvePoint(0).X * width, CurveItem.CurvePoint(0).Y * height)
        dMinLineCurve = Find_BPointLineDistance(FirstPointofLine.X, FirstPointofLine.Y, SecndPointOfLine.X, SecndPointOfLine.Y, FirstCurvePoint.X, FirstCurvePoint.Y)
        minIndex = 0 : Main_Form.COutPointFlag = False

        For i = 0 To CurveItem.CPointIndx
            Dim CurvePoint = New Point(CurveItem.CurvePoint(i).X * width, CurveItem.CurvePoint(i).Y * height)
            dLineCurve = Find_BPointLineDistance(FirstPointofLine.X, FirstPointofLine.Y, SecndPointOfLine.X, SecndPointOfLine.Y, CurvePoint.X, CurvePoint.Y)
            If Main_Form.OutPointFlag = True Then Main_Form.COutPointFlag = True : Main_Form.CDotX = Main_Form.DotX : Main_Form.CDotY = Main_Form.DotY
            If dMinLineCurve > dLineCurve Then dMinLineCurve = dLineCurve : minIndex = i
        Next
        Dim MinCurvePoint = New Point(CurveItem.CurvePoint(minIndex).X * width, CurveItem.CurvePoint(minIndex).Y * height)
        dLineCurve = Find_BPointLineDistance(FirstPointofLine.X, FirstPointofLine.Y, SecndPointOfLine.X, SecndPointOfLine.Y, MinCurvePoint.X, MinCurvePoint.Y)

        If Main_Form.COutPointFlag = True Then
            result.middlePoint = New PointF(Main_Form.CDotX / CSng(width), Main_Form.CDotY / CSng(height))
            result.dotFlag = True
        End If
        result.startPoint = New PointF(Main_Form.XsLinePoint / CSng(width), Main_Form.YsLinePoint / CSng(height))
        result.endPoint = CurveItem.CurvePoint(minIndex)
        result.name = Obj1.name & "To" & Obj2.name & "Min"
        result.length = dLineCurve
        result.measuringType = MeasureType.objMinMax
        Return result
    End Function

    ''' <summary>
    ''' calculate min distance between two objects.
    ''' </summary>
    ''' <paramname="Obj1">The selected object.</param>
    ''' <paramname="Obj2">The selected object.</param>
    ''' <paramname="width">The width of picturebox.</param>
    ''' <paramname="height">The height of picturebox.</param>
    Public Function CalcMinBetweenCurveAndPoint(ByVal Obj1 As MeasureObject, ByVal Obj2 As MeasureObject, ByVal width As Integer, ByVal height As Integer) As MeasureObject
        Dim result As MeasureObject = New MeasureObject()
        Dim CurveItem = Obj1.curveObject.CurveItem(0)
        Dim PointItem = Obj2.curveObject.PointItem(0)

        Dim dMinPointCurve, dPointCurve, minIndexP As Integer
        Dim PointPoint = New Point(PointItem.PointPoint.X * width, PointItem.PointPoint.Y * height)
        Dim FirstCurvePoint = New Point(CurveItem.CurvePoint(0).X * width, CurveItem.CurvePoint(0).Y * height)
        dMinPointCurve = Find_TwoPointDistance(PointPoint.X, PointPoint.Y, FirstCurvePoint.X, FirstCurvePoint.Y)
        minIndexP = 0
        For i = 0 To CurveItem.CPointIndx
            Dim CurvePoint = New Point(CurveItem.CurvePoint(i).X * width, CurveItem.CurvePoint(i).Y * height)
            dPointCurve = Find_TwoPointDistance(PointPoint.X, PointPoint.Y, CurvePoint.X, CurvePoint.Y)

            If dMinPointCurve > dPointCurve Then dMinPointCurve = dPointCurve : minIndexP = i
        Next
        result.startPoint = PointItem.PointPoint
        result.endPoint = CurveItem.CurvePoint(minIndexP)
        result.length = dMinPointCurve
        result.name = Obj1.name & "To" & Obj2.name & "Min"
        result.measuringType = MeasureType.objMinMax
        Return result
    End Function

    ''' <summary>
    ''' calculate min distance between two objects.
    ''' </summary>
    ''' <paramname="Obj1">The selected object.</param>
    ''' <paramname="Obj2">The selected object.</param>
    ''' <paramname="width">The width of picturebox.</param>
    ''' <paramname="height">The height of picturebox.</param>
    Public Function CalcMinBetweenPointAndLine(ByVal Obj1 As MeasureObject, ByVal Obj2 As MeasureObject, ByVal width As Integer, ByVal height As Integer) As MeasureObject
        Dim result As MeasureObject = New MeasureObject()
        Dim PointItem = Obj1.curveObject.PointItem(0)
        Dim LineItem = Obj2.curveObject.LineItem(0)

        Dim dLinePoint As Integer
        Dim FirstPointofLine = New Point(LineItem.FirstPointOfLine.X * width, LineItem.FirstPointOfLine.Y * height)
        Dim SecndPointOfLine = New Point(LineItem.SecndPointOfLine.X * width, LineItem.SecndPointOfLine.Y * height)
        Dim PointPoint = New Point(PointItem.PointPoint.X * width, PointItem.PointPoint.Y * height)
        dLinePoint = Find_BPointLineDistance(FirstPointofLine.X, FirstPointofLine.Y, SecndPointOfLine.X, SecndPointOfLine.Y, PointPoint.X, PointPoint.Y)

        result.startPoint = PointItem.PointPoint
        result.endPoint = New PointF(Main_Form.XsLinePoint / CSng(width), Main_Form.YsLinePoint / CSng(height))
        result.length = dLinePoint
        result.name = Obj1.name & "To" & Obj2.name & "Min"
        result.measuringType = MeasureType.objMinMax
        Return result
    End Function

    ''' <summary>
    ''' calculate min distance between two objects.
    ''' </summary>
    ''' <paramname="Obj1">The selected object.</param>
    ''' <paramname="Obj2">The selected object.</param>
    ''' <paramname="width">The width of picturebox.</param>
    ''' <paramname="height">The height of picturebox.</param>
    Public Function CalcMinBetweenPointAndPoly(ByVal Obj1 As MeasureObject, ByVal Obj2 As MeasureObject, ByVal width As Integer, ByVal height As Integer) As MeasureObject
        Dim result As MeasureObject = New MeasureObject()
        Dim PointItem = Obj1.curveObject.PointItem(0)
        Dim PolyItem = Obj2.curveObject.PolyItem(0)

        Dim PointPoint = New Point(PointItem.PointPoint.X * width, PointItem.PointPoint.Y * height)
        Dim dPolyPoint As Integer
        dPolyPoint = Find_BPointPolyDistance(PolyItem, PointPoint, width, height)

        result.startPoint = PointItem.PointPoint
        result.endPoint = New PointF(Main_Form.PXs / CSng(width), Main_Form.PYs / CSng(height))
        result.length = dPolyPoint
        result.name = Obj1.name & "To" & Obj2.name & "Min"
        result.measuringType = MeasureType.objMinMax
        Return result
    End Function

    ''' <summary>
    ''' calculate min distance between two objects.
    ''' </summary>
    ''' <paramname="Obj1">The selected object.</param>
    ''' <paramname="Obj2">The selected object.</param>
    ''' <paramname="width">The width of picturebox.</param>
    ''' <paramname="height">The height of picturebox.</param>
    Public Function CalcMinBetweenLineAndPoly(ByVal Obj1 As MeasureObject, ByVal Obj2 As MeasureObject, ByVal width As Integer, ByVal height As Integer) As MeasureObject
        Dim result As MeasureObject = New MeasureObject()
        Dim LineItem = Obj1.curveObject.LineItem(0)
        Dim PolyItem = Obj2.curveObject.PolyItem(0)
        Dim FirstPointofLine = New Point(LineItem.FirstPointOfLine.X * width, LineItem.FirstPointOfLine.Y * height)
        Dim SecndPointOfLine = New Point(LineItem.SecndPointOfLine.X * width, LineItem.SecndPointOfLine.Y * height)

        Dim dLinePoly, minLinePoly, LPx, LPy, LPx1, LPy1 As Integer
        minLinePoly = Find_BPointPolyDistance(PolyItem, FirstPointofLine, width, height)
        LPx = FirstPointofLine.X : LPy = FirstPointofLine.Y
        LPx1 = Main_Form.PXs : LPy1 = Main_Form.PYs
        dLinePoly = Find_BPointPolyDistance(PolyItem, SecndPointOfLine, width, height)
        If minLinePoly > dLinePoly Then minLinePoly = dLinePoly : LPx = SecndPointOfLine.X : LPy = SecndPointOfLine.Y : LPx1 = Main_Form.PXs : LPy1 = Main_Form.PYs

        For j = 0 To PolyItem.PolyPointIndx
            Dim PolyPoint = New Point(PolyItem.PolyPoint(j).X * width, PolyItem.PolyPoint(j).Y * height)
            dLinePoly = Find_BPointLineDistance(FirstPointofLine.X, FirstPointofLine.Y, SecndPointOfLine.X, SecndPointOfLine.Y, PolyPoint.X, PolyPoint.Y)
            If minLinePoly > dLinePoly Then
                minLinePoly = dLinePoly
                LPx = Main_Form.XsLinePoint : LPy = Main_Form.YsLinePoint
                LPx1 = PolyPoint.X : LPy1 = PolyPoint.Y
            End If
        Next

        result.startPoint = New PointF(LPx / CSng(width), LPy / CSng(height))
        result.endPoint = New PointF(LPx1 / CSng(width), LPy1 / CSng(height))
        result.length = minLinePoly
        result.name = Obj1.name & "To" & Obj2.name & "Min"
        result.measuringType = MeasureType.objMinMax
        Return result
    End Function

    ''' <summary>
    ''' calculate min distance between two objects.
    ''' </summary>
    ''' <paramname="Obj1">The selected object.</param>
    ''' <paramname="Obj2">The selected object.</param>
    ''' <paramname="width">The width of picturebox.</param>
    ''' <paramname="height">The height of picturebox.</param>
    Public Function CalcMinBetweenCurveAndPoly(ByVal Obj1 As MeasureObject, ByVal Obj2 As MeasureObject, ByVal width As Integer, ByVal height As Integer) As MeasureObject
        Dim result As MeasureObject = New MeasureObject()
        Dim CurveItem = Obj1.curveObject.CurveItem(0)
        Dim PolyItem = Obj2.curveObject.PolyItem(0)
        Dim FirstCurvePoint = New Point(CurveItem.CurvePoint(0).X * width, CurveItem.CurvePoint(0).Y * height)

        Dim dPolyCurve, mindPolyCurve, minIdx, PPx, PPy As Integer
        mindPolyCurve = Find_BPointPolyDistance(PolyItem, FirstCurvePoint, width, height)
        minIdx = 0 : PPx = Main_Form.PXs : PPy = Main_Form.PYs
        For j = 0 To CurveItem.CPointIndx
            Dim CurvePoint = New Point(CurveItem.CurvePoint(j).X * width, CurveItem.CurvePoint(j).Y * height)
            dPolyCurve = Find_BPointPolyDistance(PolyItem, CurvePoint, width, height)
            If mindPolyCurve > dPolyCurve Then mindPolyCurve = dPolyCurve : minIdx = j : PPx = Main_Form.PXs : PPy = Main_Form.PYs
        Next

        result.startPoint = CurveItem.CurvePoint(minIdx)
        result.endPoint = New PointF(PPx / CSng(width), PPy / CSng(height))
        result.length = mindPolyCurve
        result.name = Obj1.name & "To" & Obj2.name & "Min"
        result.measuringType = MeasureType.objMinMax
        Return result
    End Function
#End Region

#Region "Max"
    ''' <summary>
    ''' calculate max distance between two objects.
    ''' </summary>
    ''' <paramname="Obj1">The selected object.</param>
    ''' <paramname="Obj2">The selected object.</param>
    ''' <paramname="width">The width of picturebox.</param>
    ''' <paramname="height">The height of picturebox.</param>
    Public Function CalcMaxBetweenCuPolyAndLine(ByVal Obj1 As MeasureObject, ByVal Obj2 As MeasureObject, ByVal width As Integer, ByVal height As Integer) As MeasureObject
        Dim result = New MeasureObject()
        Dim CuPolyItem = Obj1.curveObject.CuPolyItem(0)
        Dim LineItem = Obj2.curveObject.LineItem(0)
        Dim FirstPointofLine = New Point(LineItem.FirstPointOfLine.X * width, LineItem.FirstPointOfLine.Y * height)
        Dim SecndPointOfLine = New Point(LineItem.SecndPointOfLine.X * width, LineItem.SecndPointOfLine.Y * height)
        Dim FirstEdgeOfCuPoly = New Point(CuPolyItem.CuPolyPoint(1, 0).X * width, CuPolyItem.CuPolyPoint(1, 0).Y * height)

        Dim maxCL_Dis, CL_Dis, maxIdx_j, maxIdx_k As Integer
        Dim CrosPoint As Point
        maxCL_Dis = Find_TwoPointDistance(FirstEdgeOfCuPoly.X, FirstEdgeOfCuPoly.Y, FirstPointofLine.X, FirstPointofLine.Y)

        CrosPoint.X = FirstPointofLine.X
        CrosPoint.Y = FirstPointofLine.Y
        maxIdx_j = 1 : maxIdx_k = 0

        For j = 1 To CuPolyItem.CuPolyPointIndx_j
            For k = 0 To CuPolyItem.CuPolyPointIndx_k(j)
                Dim KEdgeOfCuPoly = New Point(CuPolyItem.CuPolyPoint(j, k).X * width, CuPolyItem.CuPolyPoint(j, k).Y * height)
                CL_Dis = Find_TwoPointDistance(KEdgeOfCuPoly.X, KEdgeOfCuPoly.Y, FirstPointofLine.X, FirstPointofLine.Y)
                If maxCL_Dis < CL_Dis Then
                    maxCL_Dis = CL_Dis
                    maxIdx_j = j : maxIdx_k = k
                    CrosPoint.X = FirstPointofLine.X
                    CrosPoint.Y = FirstPointofLine.Y
                End If
            Next
        Next
        For j = 1 To CuPolyItem.CuPolyPointIndx_j
            For k = 0 To CuPolyItem.CuPolyPointIndx_k(j)
                Dim KEdgeOfCuPoly = New Point(CuPolyItem.CuPolyPoint(j, k).X * width, CuPolyItem.CuPolyPoint(j, k).Y * height)
                CL_Dis = Find_TwoPointDistance(KEdgeOfCuPoly.X, KEdgeOfCuPoly.Y, SecndPointOfLine.X, SecndPointOfLine.Y)
                If maxCL_Dis < CL_Dis Then
                    maxCL_Dis = CL_Dis
                    maxIdx_j = j : maxIdx_k = k
                    CrosPoint.X = SecndPointOfLine.X
                    CrosPoint.Y = SecndPointOfLine.Y
                End If
            Next
        Next

        result.name = Obj1.name & "To" & Obj2.name & "Max"
        result.length = maxCL_Dis
        result.measuringType = MeasureType.objMinMax
        result.startPoint = CuPolyItem.CuPolyPoint(maxIdx_j, maxIdx_k)
        result.endPoint = New PointF(CrosPoint.X / CSng(width), CrosPoint.Y / CSng(height))
        Return result
    End Function

    ''' <summary>
    ''' calculate max distance between two objects.
    ''' </summary>
    ''' <paramname="Obj1">The selected object.</param>
    ''' <paramname="Obj2">The selected object.</param>
    ''' <paramname="width">The width of picturebox.</param>
    ''' <paramname="height">The height of picturebox.</param>
    Public Function CalcMaxBetweenCuPolyAndPoint(ByVal Obj1 As MeasureObject, ByVal Obj2 As MeasureObject, ByVal width As Integer, ByVal height As Integer) As MeasureObject
        Dim result = New MeasureObject()
        Dim CuPolyItem = Obj1.curveObject.CuPolyItem(0)
        Dim PointItem = Obj2.curveObject.PointItem(0)

        Dim PointPoint = New Point(PointItem.PointPoint.X * width, PointItem.PointPoint.Y * height)
        Dim FirstEdgeOfCuPoly = New Point(CuPolyItem.CuPolyPoint(1, 0).X * width, CuPolyItem.CuPolyPoint(1, 0).Y * height)

        Dim dMaxPointCuPoly, dPointCuPoly, maxIndexPCuPoly_j, maxIndexPCuPoly_k, maxExistFlag, Cx, Cy As Integer
        dMaxPointCuPoly = Find_TwoPointDistance(PointPoint.X, PointPoint.Y, FirstEdgeOfCuPoly.X, FirstEdgeOfCuPoly.Y)
        maxIndexPCuPoly_j = 1 : maxIndexPCuPoly_k = 0
        For j = 1 To CuPolyItem.CuPolyPointIndx_j
            For k = 0 To CuPolyItem.CuPolyPointIndx_k(j)
                Dim KEdgeOfCuPoly = New Point(CuPolyItem.CuPolyPoint(j, k).X * width, CuPolyItem.CuPolyPoint(j, k).Y * height)
                dPointCuPoly = Find_TwoPointDistance(PointPoint.X, PointPoint.Y, KEdgeOfCuPoly.X, KEdgeOfCuPoly.Y)
                If dMaxPointCuPoly < dPointCuPoly Then dMaxPointCuPoly = dPointCuPoly : maxIndexPCuPoly_j = j : maxIndexPCuPoly_k = k
            Next
        Next

        result.name = Obj1.name & "To" & Obj2.name & "Max"
        result.length = dMaxPointCuPoly
        result.measuringType = MeasureType.objMinMax
        result.startPoint = CuPolyItem.CuPolyPoint(maxIndexPCuPoly_j, maxIndexPCuPoly_k)
        result.endPoint = PointItem.PointPoint
        Return result
    End Function

    ''' <summary>
    ''' calculate max distance between two objects.
    ''' </summary>
    ''' <paramname="Obj1">The selected object.</param>
    ''' <paramname="Obj2">The selected object.</param>
    ''' <paramname="width">The width of picturebox.</param>
    ''' <paramname="height">The height of picturebox.</param>
    Public Function CalcMaxBetweenCurveAndLine(ByVal Obj1 As MeasureObject, ByVal Obj2 As MeasureObject, ByVal width As Integer, ByVal height As Integer) As MeasureObject
        Dim result = New MeasureObject()
        Dim CurveItem = Obj1.curveObject.CurveItem(0)
        Dim LineItem = Obj2.curveObject.LineItem(0)

        Dim FirstPointofLine = New Point(LineItem.FirstPointOfLine.X * width, LineItem.FirstPointOfLine.Y * height)
        Dim SecndPointOfLine = New Point(LineItem.SecndPointOfLine.X * width, LineItem.SecndPointOfLine.Y * height)
        Dim FirstCurvePoint = New Point(CurveItem.CurvePoint(0).X * width, CurveItem.CurvePoint(0).Y * height)

        Dim maxCL_Dis, CL_Dis, maxIdx As Integer
        Dim CrosPoint As Point
        maxCL_Dis = Find_TwoPointDistance(FirstCurvePoint.X, FirstCurvePoint.Y, FirstPointofLine.X, FirstPointofLine.Y)
        CrosPoint.X = FirstPointofLine.X
        CrosPoint.Y = FirstPointofLine.Y
        maxIdx = 0
        For j = 0 To CurveItem.CPointIndx
            Dim CurvePoint = New Point(CurveItem.CurvePoint(j).X * width, CurveItem.CurvePoint(j).Y * height)
            CL_Dis = Find_TwoPointDistance(CurvePoint.X, CurvePoint.Y, FirstPointofLine.X, FirstPointofLine.Y)
            If maxCL_Dis < CL_Dis Then
                maxCL_Dis = CL_Dis
                maxIdx = j
                CrosPoint.X = FirstPointofLine.X
                CrosPoint.Y = FirstPointofLine.Y
            End If
        Next
        For j = 0 To CurveItem.CPointIndx
            Dim CurvePoint = New Point(CurveItem.CurvePoint(j).X * width, CurveItem.CurvePoint(j).Y * height)
            CL_Dis = Find_TwoPointDistance(CurvePoint.X, CurvePoint.Y, SecndPointOfLine.X, SecndPointOfLine.Y)
            If maxCL_Dis < CL_Dis Then
                maxCL_Dis = CL_Dis
                maxIdx = j
                CrosPoint.X = SecndPointOfLine.X
                CrosPoint.Y = SecndPointOfLine.Y
            End If
        Next

        result.name = Obj1.name & "To" & Obj2.name & "Max"
        result.length = maxCL_Dis
        result.measuringType = MeasureType.objMinMax
        result.startPoint = CurveItem.CurvePoint(maxIdx)
        result.endPoint = New PointF(CrosPoint.X / CSng(width), CrosPoint.Y / CSng(height))
        Return result

    End Function

    ''' <summary>
    ''' calculate max distance between two objects.
    ''' </summary>
    ''' <paramname="Obj1">The selected object.</param>
    ''' <paramname="Obj2">The selected object.</param>
    ''' <paramname="width">The width of picturebox.</param>
    ''' <paramname="height">The height of picturebox.</param>
    Public Function CalcMaxBetweenCurveAndPoint(ByVal Obj1 As MeasureObject, ByVal Obj2 As MeasureObject, ByVal width As Integer, ByVal height As Integer) As MeasureObject
        Dim result As MeasureObject = New MeasureObject()
        Dim CurveItem = Obj1.curveObject.CurveItem(0)
        Dim PointItem = Obj2.curveObject.PointItem(0)

        Dim PointPoint = New Point(PointItem.PointPoint.X * width, PointItem.PointPoint.Y * height)
        Dim FirstCurvePoint = New Point(CurveItem.CurvePoint(0).X * width, CurveItem.CurvePoint(0).Y * height)

        Dim maxCL_Dis, CL_Dis, maxIdx As Integer
        Dim CrosPoint As Point
        maxCL_Dis = Find_TwoPointDistance(FirstCurvePoint.X, FirstCurvePoint.Y, PointPoint.X, PointPoint.Y)
        CrosPoint.X = PointPoint.X
        CrosPoint.Y = PointPoint.Y
        maxIdx = 0
        For j = 0 To CurveItem.CPointIndx
            Dim CurvePoint = New Point(CurveItem.CurvePoint(j).X * width, CurveItem.CurvePoint(j).Y * height)
            CL_Dis = Find_TwoPointDistance(CurvePoint.X, CurvePoint.Y, PointPoint.X, PointPoint.Y)
            If maxCL_Dis < CL_Dis Then
                maxCL_Dis = CL_Dis
                maxIdx = j
                CrosPoint.X = PointPoint.X
                CrosPoint.Y = PointPoint.Y
            End If
        Next

        result.name = Obj1.name & "To" & Obj2.name & "Max"
        result.length = maxCL_Dis
        result.measuringType = MeasureType.objMinMax
        result.startPoint = CurveItem.CurvePoint(maxIdx)
        result.endPoint = New PointF(CrosPoint.X / CSng(width), CrosPoint.Y / CSng(height))
        Return result
    End Function

    ''' <summary>
    ''' calculate max distance between two objects.
    ''' </summary>
    ''' <paramname="Obj1">The selected object.</param>
    ''' <paramname="Obj2">The selected object.</param>
    ''' <paramname="width">The width of picturebox.</param>
    ''' <paramname="height">The height of picturebox.</param>
    Public Function CalcMaxBetweenCurveAndPoly(ByVal Obj1 As MeasureObject, ByVal Obj2 As MeasureObject, ByVal width As Integer, ByVal height As Integer) As MeasureObject
        Dim result As MeasureObject = New MeasureObject()
        Dim CurveItem = Obj1.curveObject.CurveItem(0)
        Dim PolyItem = Obj2.curveObject.PolyItem(0)
        Dim FirstCurvePoint = New Point(CurveItem.CurvePoint(0).X * width, CurveItem.CurvePoint(0).Y * height)
        Dim FirstPolyPoint = New Point(PolyItem.PolyPoint(0).X * width, PolyItem.PolyPoint(0).Y * height)

        Dim maxCL_Dis, CL_Dis, maxIdx As Integer
        Dim CrosPoint As Point
        maxCL_Dis = Find_TwoPointDistance(FirstCurvePoint.X, FirstCurvePoint.Y, FirstPolyPoint.X, FirstPolyPoint.Y)

        CrosPoint.X = FirstPolyPoint.X
        CrosPoint.Y = FirstPolyPoint.Y
        maxIdx = 0
        For j = 0 To CurveItem.CPointIndx
            For k = 0 To PolyItem.PolyPointIndx
                Dim CurvePoint = New Point(CurveItem.CurvePoint(j).X * width, CurveItem.CurvePoint(j).Y * height)
                Dim PolyPoint = New Point(PolyItem.PolyPoint(k).X * width, PolyItem.PolyPoint(k).Y * height)
                CL_Dis = Find_TwoPointDistance(CurvePoint.X, CurvePoint.Y, PolyPoint.X, PolyPoint.Y)
                If maxCL_Dis < CL_Dis Then
                    maxCL_Dis = CL_Dis
                    maxIdx = j
                    CrosPoint.X = PolyPoint.X
                    CrosPoint.Y = PolyPoint.Y
                End If
            Next
        Next

        result.name = Obj1.name & "To" & Obj2.name & "Max"
        result.length = maxCL_Dis
        result.measuringType = MeasureType.objMinMax
        result.startPoint = CurveItem.CurvePoint(maxIdx)
        result.endPoint = New PointF(CrosPoint.X / CSng(width), CrosPoint.Y / CSng(height))
        Return result

    End Function

    ''' <summary>
    ''' calculate max distance between two objects.
    ''' </summary>
    ''' <paramname="Obj1">The selected object.</param>
    ''' <paramname="Obj2">The selected object.</param>
    ''' <paramname="width">The width of picturebox.</param>
    ''' <paramname="height">The height of picturebox.</param>
    Public Function CalcMaxBetweenLineAndPoint(ByVal Obj1 As MeasureObject, ByVal Obj2 As MeasureObject, ByVal width As Integer, ByVal height As Integer) As MeasureObject
        Dim result As MeasureObject = New MeasureObject()
        Dim PointItem = Obj2.curveObject.PointItem(0)
        Dim LineItem = Obj1.curveObject.LineItem(0)

        Dim FirstPointofLine = New Point(LineItem.FirstPointOfLine.X * width, LineItem.FirstPointOfLine.Y * height)
        Dim SecndPointOfLine = New Point(LineItem.SecndPointOfLine.X * width, LineItem.SecndPointOfLine.Y * height)
        Dim PointPoint = New Point(PointItem.PointPoint.X * width, PointItem.PointPoint.Y * height)

        Dim maxCL_Dis, CL_Dis As Integer
        Dim CrosPoint As Point
        maxCL_Dis = Find_TwoPointDistance(PointPoint.X, PointPoint.Y, FirstPointofLine.X, FirstPointofLine.Y)
        CrosPoint.X = FirstPointofLine.X
        CrosPoint.Y = FirstPointofLine.Y
        CL_Dis = Find_TwoPointDistance(PointPoint.X, PointPoint.Y, SecndPointOfLine.X, SecndPointOfLine.Y)
        If maxCL_Dis < CL_Dis Then
            maxCL_Dis = CL_Dis
            CrosPoint.X = SecndPointOfLine.X
            CrosPoint.Y = SecndPointOfLine.Y
        End If

        result.name = Obj1.name & "To" & Obj2.name & "Max"
        result.length = maxCL_Dis
        result.measuringType = MeasureType.objMinMax
        result.startPoint = PointItem.PointPoint
        result.endPoint = New PointF(CrosPoint.X / CSng(width), CrosPoint.Y / CSng(height))
        Return result
    End Function

    ''' <summary>
    ''' calculate max distance between two objects.
    ''' </summary>
    ''' <paramname="Obj1">The selected object.</param>
    ''' <paramname="Obj2">The selected object.</param>
    ''' <paramname="width">The width of picturebox.</param>
    ''' <paramname="height">The height of picturebox.</param>
    Public Function CalcMaxBetweenLineAndPoly(ByVal Obj1 As MeasureObject, ByVal Obj2 As MeasureObject, ByVal width As Integer, ByVal height As Integer) As MeasureObject
        Dim result As MeasureObject = New MeasureObject()
        Dim LineItem = Obj1.curveObject.LineItem(0)
        Dim PolyItem = Obj2.curveObject.PolyItem(0)
        Dim FirstPointofLine = New Point(LineItem.FirstPointOfLine.X * width, LineItem.FirstPointOfLine.Y * height)
        Dim SecndPointOfLine = New Point(LineItem.SecndPointOfLine.X * width, LineItem.SecndPointOfLine.Y * height)

        Dim maxCL_Dis, CL_Dis, maxIdx As Integer
        Dim CrosPoint As Point
        Dim PolyPoint = New Point(PolyItem.PolyPoint(0).X * width, PolyItem.PolyPoint(0).Y * height)
        maxCL_Dis = Find_TwoPointDistance(PolyPoint.X, PolyPoint.Y, FirstPointofLine.X, FirstPointofLine.Y)
        CrosPoint.X = FirstPointofLine.X
        CrosPoint.Y = FirstPointofLine.Y
        maxIdx = 0
        For j = 0 To PolyItem.PolyPointIndx
            PolyPoint = New Point(PolyItem.PolyPoint(j).X * width, PolyItem.PolyPoint(j).Y * height)
            CL_Dis = Find_TwoPointDistance(PolyPoint.X, PolyPoint.Y, FirstPointofLine.X, FirstPointofLine.Y)
            If maxCL_Dis < CL_Dis Then
                maxCL_Dis = CL_Dis
                maxIdx = j
                CrosPoint.X = FirstPointofLine.X
                CrosPoint.Y = FirstPointofLine.Y
            End If
        Next
        For j = 0 To PolyItem.PolyPointIndx
            PolyPoint = New Point(PolyItem.PolyPoint(j).X * width, PolyItem.PolyPoint(j).Y * height)
            CL_Dis = Find_TwoPointDistance(PolyPoint.X, PolyPoint.Y, SecndPointOfLine.X, SecndPointOfLine.Y)
            If maxCL_Dis < CL_Dis Then
                maxCL_Dis = CL_Dis
                maxIdx = j
                CrosPoint.X = SecndPointOfLine.X
                CrosPoint.Y = SecndPointOfLine.Y
            End If
        Next

        result.name = Obj1.name & "To" & Obj2.name & "Max"
        result.length = maxCL_Dis
        result.measuringType = MeasureType.objMinMax
        result.startPoint = PolyItem.PolyPoint(maxIdx)
        result.endPoint = New PointF(CrosPoint.X / CSng(width), CrosPoint.Y / CSng(height))
        Return result

    End Function

    ''' <summary>
    ''' calculate max distance between two objects.
    ''' </summary>
    ''' <paramname="Obj1">The selected object.</param>
    ''' <paramname="Obj2">The selected object.</param>
    ''' <paramname="width">The width of picturebox.</param>
    ''' <paramname="height">The height of picturebox.</param>
    Public Function CalcMaxBetweenPolyAndPoint(ByVal Obj1 As MeasureObject, ByVal Obj2 As MeasureObject, ByVal width As Integer, ByVal height As Integer) As MeasureObject
        Dim result As MeasureObject = New MeasureObject()
        Dim PointItem = Obj2.curveObject.PointItem(0)
        Dim PolyItem = Obj1.curveObject.PolyItem(0)

        Dim PointPoint = New Point(PointItem.PointPoint.X * width, PointItem.PointPoint.Y * height)
        Dim maxCL_Dis, CL_Dis, maxIdx As Integer
        Dim CrosPoint As Point
        Dim PolyPoint = New Point(PolyItem.PolyPoint(0).X * width, PolyItem.PolyPoint(0).Y * height)
        maxCL_Dis = Find_TwoPointDistance(PolyPoint.X, PolyPoint.Y, PointPoint.X, PointPoint.Y)
        CrosPoint.X = PointPoint.X
        CrosPoint.Y = PointPoint.Y
        maxIdx = 0
        For j = 0 To PolyItem.PolyPointIndx
            PolyPoint = New Point(PolyItem.PolyPoint(j).X * width, PolyItem.PolyPoint(j).Y * height)
            CL_Dis = Find_TwoPointDistance(PolyPoint.X, PolyPoint.Y, PointPoint.X, PointPoint.Y)
            If maxCL_Dis < CL_Dis Then
                maxCL_Dis = CL_Dis
                maxIdx = j
                CrosPoint.X = PointPoint.X
                CrosPoint.Y = PointPoint.Y
            End If
        Next

        result.name = Obj1.name & "To" & Obj2.name & "Max"
        result.length = maxCL_Dis
        result.measuringType = MeasureType.objMinMax
        result.startPoint = PolyItem.PolyPoint(maxIdx)
        result.endPoint = New PointF(CrosPoint.X / CSng(width), CrosPoint.Y / CSng(height))
        Return result
    End Function

#End Region

#Region "P Min"

    ''' <summary>
    ''' calculate min distance between two objects.
    ''' </summary>
    ''' <paramname="Obj1">The selected object.</param>
    ''' <paramname="Obj2">The selected object.</param>
    ''' <paramname="width">The width of picturebox.</param>
    ''' <paramname="height">The height of picturebox.</param>
    Public Function CalcPMinBetweenCuPolyAndLine(ByVal Obj1 As MeasureObject, ByVal Obj2 As MeasureObject, ByVal width As Integer, ByVal height As Integer) As MeasureObject
        Dim result = New MeasureObject()
        Dim dMinLineCurve, dLineCurve, minIndex_j, minIndex_k, MSx, MSy As Integer
        Dim LineMidExistFlag, CuPolyLOutflag As Boolean
        LineMidExistFlag = False : CuPolyLOutflag = False
        Dim CuPolyItem = Obj1.curveObject.CuPolyItem(0)
        Dim LineItem = Obj2.curveObject.LineItem(0)
        Dim FirstPointofLine = New Point(LineItem.FirstPointOfLine.X * width, LineItem.FirstPointOfLine.Y * height)
        Dim SecndPointOfLine = New Point(LineItem.SecndPointOfLine.X * width, LineItem.SecndPointOfLine.Y * height)
        Dim FirstEdgeOfCuPoly = New Point(CuPolyItem.CuPolyPoint(1, 0).X * width, CuPolyItem.CuPolyPoint(1, 0).Y * height)
        dMinLineCurve = pFind_BPointLineDistance(FirstPointofLine.X, FirstPointofLine.Y, SecndPointOfLine.X, SecndPointOfLine.Y, FirstEdgeOfCuPoly.X, FirstEdgeOfCuPoly.Y)
        minIndex_j = 1 : minIndex_k = 0 : Main_Form.COutPointFlag = False
        For j = 1 To CuPolyItem.CuPolyPointIndx_j
            For k = 0 To CuPolyItem.CuPolyPointIndx_k(j)
                Dim KEdgeOfCuPoly = New Point(CuPolyItem.CuPolyPoint(j, k).X * width, CuPolyItem.CuPolyPoint(j, k).Y * height)
                dLineCurve = pFind_BPointLineDistance(FirstPointofLine.X, FirstPointofLine.Y, SecndPointOfLine.X, SecndPointOfLine.Y, KEdgeOfCuPoly.X, KEdgeOfCuPoly.Y)
                If Main_Form.OutPointFlag = True Then Main_Form.COutPointFlag = True : Main_Form.CDotX = Main_Form.DotX : Main_Form.CDotY = Main_Form.DotY
                If dMinLineCurve > dLineCurve Then LineMidExistFlag = False : dMinLineCurve = dLineCurve : minIndex_j = j : minIndex_k = k : 
            Next
            If j > 1 Then
                Dim sP, eP As Point
                sP = New Point(CuPolyItem.CuPolyPoint(j - 1, CuPolyItem.CuPolyPointIndx_k(j - 1)).X * width, CuPolyItem.CuPolyPoint(j - 1, CuPolyItem.CuPolyPointIndx_k(j - 1)).Y * height)
                eP = New Point(CuPolyItem.CuPolyPoint(j, 0).X * width, CuPolyItem.CuPolyPoint(j, 0).Y * height)

                If sP.X = eP.X Then
                    If sP.Y < eP.Y Then
                        For mpy = sP.Y To eP.Y
                            dLineCurve = pFind_BPointLineDistance(FirstPointofLine.X, FirstPointofLine.Y, SecndPointOfLine.X, SecndPointOfLine.Y, sP.X, mpy)
                            If dMinLineCurve > dLineCurve Then
                                LineMidExistFlag = True
                                dMinLineCurve = dLineCurve
                                MSx = sP.X
                                MSy = mpy
                                If Main_Form.OutPointFlag = True Then Main_Form.COutPointFlag = True : Main_Form.CDotX = Main_Form.DotX : Main_Form.CDotY = Main_Form.DotY
                            End If

                        Next
                    Else
                        For mpy = eP.Y To sP.Y
                            dLineCurve = pFind_BPointLineDistance(FirstPointofLine.X, FirstPointofLine.Y, SecndPointOfLine.X, SecndPointOfLine.Y, sP.X, mpy)

                            If dMinLineCurve > dLineCurve Then
                                LineMidExistFlag = True
                                dMinLineCurve = dLineCurve
                                MSx = sP.X
                                MSy = mpy
                                If Main_Form.OutPointFlag = True Then Main_Form.COutPointFlag = True : Main_Form.CDotX = Main_Form.DotX : Main_Form.CDotY = Main_Form.DotY
                            End If

                        Next
                    End If
                ElseIf sP.Y = eP.Y Then
                    If sP.X < eP.X Then
                        For mpx = sP.X To eP.X
                            dLineCurve = pFind_BPointLineDistance(FirstPointofLine.X, FirstPointofLine.Y, SecndPointOfLine.X, SecndPointOfLine.Y, mpx, sP.Y)

                            If dMinLineCurve > dLineCurve Then
                                LineMidExistFlag = True
                                dMinLineCurve = dLineCurve
                                MSx = mpx
                                MSy = sP.Y
                                If Main_Form.OutPointFlag = True Then Main_Form.COutPointFlag = True : Main_Form.CDotX = Main_Form.DotX : Main_Form.CDotY = Main_Form.DotY
                            End If
                        Next
                    Else
                        For mpX = eP.X To sP.X
                            dLineCurve = pFind_BPointLineDistance(FirstPointofLine.X, FirstPointofLine.Y, SecndPointOfLine.X, SecndPointOfLine.Y, mpX, sP.Y)
                            If dMinLineCurve > dLineCurve Then
                                LineMidExistFlag = True
                                dMinLineCurve = dLineCurve
                                MSx = mpX
                                MSy = sP.Y
                                If Main_Form.OutPointFlag = True Then Main_Form.COutPointFlag = True : Main_Form.CDotX = Main_Form.DotX : Main_Form.CDotY = Main_Form.DotY
                            End If
                        Next
                    End If
                Else
                    If sP.X < eP.X Then
                        For mpx = sP.X To eP.X
                            dLineCurve = pFind_BPointLineDistance(FirstPointofLine.X, FirstPointofLine.Y, SecndPointOfLine.X, SecndPointOfLine.Y, mpx, GetLineEq(sP, eP, mpx))

                            If dMinLineCurve > dLineCurve Then
                                LineMidExistFlag = True
                                dMinLineCurve = dLineCurve
                                MSx = mpx
                                MSy = GetLineEq(sP, eP, mpx)
                                If Main_Form.OutPointFlag = True Then Main_Form.COutPointFlag = True : Main_Form.CDotX = Main_Form.DotX : Main_Form.CDotY = Main_Form.DotY
                            End If
                        Next
                    Else
                        For mpX = eP.X To sP.X
                            dLineCurve = pFind_BPointLineDistance(FirstPointofLine.X, FirstPointofLine.Y, SecndPointOfLine.X, SecndPointOfLine.Y, mpX, GetLineEq(sP, eP, mpX))

                            If dMinLineCurve > dLineCurve Then
                                LineMidExistFlag = True
                                dMinLineCurve = dLineCurve
                                MSx = mpX
                                MSy = GetLineEq(sP, eP, mpX)
                                If Main_Form.OutPointFlag = True Then Main_Form.COutPointFlag = True : Main_Form.CDotX = Main_Form.DotX : Main_Form.CDotY = Main_Form.DotY
                            End If
                        Next
                    End If
                End If
            End If
        Next

        If LineMidExistFlag = False Then
            Dim minEdge = New Point(CuPolyItem.CuPolyPoint(minIndex_j, minIndex_k).X * width, CuPolyItem.CuPolyPoint(minIndex_j, minIndex_k).Y * height)
            dLineCurve = pFind_BPointLineDistance(FirstPointofLine.X, FirstPointofLine.Y, SecndPointOfLine.X, SecndPointOfLine.Y, minEdge.X, minEdge.Y)
            If Main_Form.OutPointFlag = True Then Main_Form.COutPointFlag = True : Main_Form.CDotX = Main_Form.DotX : Main_Form.CDotY = Main_Form.DotY
            MSx = minEdge.X : MSy = minEdge.Y
        Else
            dLineCurve = pFind_BPointLineDistance(FirstPointofLine.X, FirstPointofLine.Y, SecndPointOfLine.X, SecndPointOfLine.Y, MSx, MSy)
            If Main_Form.OutPointFlag = True Then Main_Form.COutPointFlag = True : Main_Form.CDotX = Main_Form.DotX : Main_Form.CDotY = Main_Form.DotY
        End If

        result.name = Obj1.name & "To" & Obj2.name & "PMin"
        result.length = dLineCurve
        result.measuringType = MeasureType.objMinMax
        result.startPoint = New PointF(MSx / CSng(width), MSy / CSng(height))
        result.endPoint = New PointF(Main_Form.XsLinePoint / CSng(width), Main_Form.YsLinePoint / CSng(height))
        Return result
    End Function

    ''' <summary>
    ''' calculate min distance between two objects.
    ''' </summary>
    ''' <paramname="Obj1">The selected object.</param>
    ''' <paramname="Obj2">The selected object.</param>
    ''' <paramname="width">The width of picturebox.</param>
    ''' <paramname="height">The height of picturebox.</param>
    Public Function CalcPMinBetweenCuPolyAndPoint(ByVal Obj1 As MeasureObject, ByVal Obj2 As MeasureObject, ByVal width As Integer, ByVal height As Integer) As MeasureObject
        Dim result As MeasureObject = New MeasureObject()
        Dim dMinPointCuPoly, dPointCuPoly, minIndexPCuPoly_j, minIndexPCuPoly_k, minExistFlag, Cx, Cy As Integer
        Dim CuPolyItem = Obj1.curveObject.CuPolyItem(0)
        Dim PointItem = Obj2.curveObject.PointItem(0)

        Dim PointPoint = New Point(PointItem.PointPoint.X * width, PointItem.PointPoint.Y * height)
        Dim FirstEdgeOfCuPoly = New Point(CuPolyItem.CuPolyPoint(1, 0).X * width, CuPolyItem.CuPolyPoint(1, 0).Y * height)
        dMinPointCuPoly = Find_TwoPointDistance(PointPoint.X, PointPoint.Y, FirstEdgeOfCuPoly.X, FirstEdgeOfCuPoly.Y)
        minIndexPCuPoly_j = 1 : minIndexPCuPoly_k = 0
        minExistFlag = False
        For j = 1 To CuPolyItem.CuPolyPointIndx_j
            For k = 0 To CuPolyItem.CuPolyPointIndx_k(j)
                Dim KEdgeOfCuPoly = New Point(CuPolyItem.CuPolyPoint(j, k).X * width, CuPolyItem.CuPolyPoint(j, k).Y * height)
                dPointCuPoly = Find_TwoPointDistance(PointPoint.X, PointPoint.Y, KEdgeOfCuPoly.X, KEdgeOfCuPoly.Y)
                If dMinPointCuPoly > dPointCuPoly Then dMinPointCuPoly = dPointCuPoly : minIndexPCuPoly_j = j : minIndexPCuPoly_k = k
            Next
            If j > 1 Then
                Dim sP, eP As Point
                sP = New Point(CuPolyItem.CuPolyPoint(j - 1, CuPolyItem.CuPolyPointIndx_k(j - 1)).X * width, CuPolyItem.CuPolyPoint(j - 1, CuPolyItem.CuPolyPointIndx_k(j - 1)).Y * height)
                eP = New Point(CuPolyItem.CuPolyPoint(j, 0).X * width, CuPolyItem.CuPolyPoint(j, 0).Y * height)

                dPointCuPoly = Find_BPointLineDistance(sP.X, sP.Y, eP.X, eP.Y, PointPoint.X, PointPoint.Y)
                If dMinPointCuPoly > dPointCuPoly Then dMinPointCuPoly = dPointCuPoly : minExistFlag = True : Cx = Main_Form.XsLinePoint : Cy = Main_Form.YsLinePoint
            End If
        Next

        If minExistFlag Then
            result.endPoint = New PointF(Cx / CSng(width), Cy / CSng(height))
        Else
            result.endPoint = CuPolyItem.CuPolyPoint(minIndexPCuPoly_j, minIndexPCuPoly_k)
        End If
        result.name = Obj1.name & "To" & Obj2.name & "PMin"
        result.startPoint = PointItem.PointPoint
        result.measuringType = MeasureType.objMinMax
        result.length = dMinPointCuPoly
        Return result
    End Function

    ''' <summary>
    ''' calculate min distance between two objects.
    ''' </summary>
    ''' <paramname="Obj1">The selected object.</param>
    ''' <paramname="Obj2">The selected object.</param>
    ''' <paramname="width">The width of picturebox.</param>
    ''' <paramname="height">The height of picturebox.</param>
    Public Function CalcPMinBetweenCurveAndLine(ByVal Obj1 As MeasureObject, ByVal Obj2 As MeasureObject, ByVal width As Integer, ByVal height As Integer) As MeasureObject
        Dim result = New MeasureObject()
        Dim CurveItem = Obj1.curveObject.CurveItem(0)
        Dim LineItem = Obj2.curveObject.LineItem(0)
        Dim dMinLineCurve, dLineCurve, minIndex As Integer
        Dim FirstPointofLine = New Point(LineItem.FirstPointOfLine.X * width, LineItem.FirstPointOfLine.Y * height)
        Dim SecndPointOfLine = New Point(LineItem.SecndPointOfLine.X * width, LineItem.SecndPointOfLine.Y * height)
        Dim FirstCurvePoint = New Point(CurveItem.CurvePoint(0).X * width, CurveItem.CurvePoint(0).Y * height)
        dMinLineCurve = pFind_BPointLineDistance(FirstPointofLine.X, FirstPointofLine.Y, SecndPointOfLine.X, SecndPointOfLine.Y, FirstCurvePoint.X, FirstCurvePoint.Y)
        minIndex = 0 : Main_Form.COutPointFlag = False

        For i = 0 To CurveItem.CPointIndx
            Dim CurvePoint = New Point(CurveItem.CurvePoint(i).X * width, CurveItem.CurvePoint(i).Y * height)
            dLineCurve = pFind_BPointLineDistance(FirstPointofLine.X, FirstPointofLine.Y, SecndPointOfLine.X, SecndPointOfLine.Y, CurvePoint.X, CurvePoint.Y)
            If Main_Form.OutPointFlag = True Then Main_Form.COutPointFlag = True : Main_Form.CDotX = Main_Form.DotX : Main_Form.CDotY = Main_Form.DotY
            If dMinLineCurve > dLineCurve Then dMinLineCurve = dLineCurve : minIndex = i
        Next
        Dim MinCurvePoint = New Point(CurveItem.CurvePoint(minIndex).X * width, CurveItem.CurvePoint(minIndex).Y * height)
        dLineCurve = pFind_BPointLineDistance(FirstPointofLine.X, FirstPointofLine.Y, SecndPointOfLine.X, SecndPointOfLine.Y, MinCurvePoint.X, MinCurvePoint.Y)
        If Main_Form.OutPointFlag = True Then Main_Form.COutPointFlag = True : Main_Form.CDotX = Main_Form.DotX : Main_Form.CDotY = Main_Form.DotY

        If Main_Form.COutPointFlag = True Then
            result.middlePoint = New PointF(Main_Form.CDotX / CSng(width), Main_Form.CDotY / CSng(height))
            result.dotFlag = True
        End If
        result.startPoint = New PointF(Main_Form.XsLinePoint / CSng(width), Main_Form.YsLinePoint / CSng(height))
        result.endPoint = CurveItem.CurvePoint(minIndex)
        result.name = Obj1.name & "To" & Obj2.name & "PMin"
        result.length = dLineCurve
        result.measuringType = MeasureType.objMinMax
        Return result
    End Function

    ''' <summary>
    ''' calculate min distance between two objects.
    ''' </summary>
    ''' <paramname="Obj1">The selected object.</param>
    ''' <paramname="Obj2">The selected object.</param>
    ''' <paramname="width">The width of picturebox.</param>
    ''' <paramname="height">The height of picturebox.</param>
    Public Function CalcPMinBetweenCurveAndPoint(ByVal Obj1 As MeasureObject, ByVal Obj2 As MeasureObject, ByVal width As Integer, ByVal height As Integer) As MeasureObject
        Dim result As MeasureObject = New MeasureObject()
        Dim CurveItem = Obj1.curveObject.CurveItem(0)
        Dim PointItem = Obj2.curveObject.PointItem(0)

        Dim dMinPointCurve, dPointCurve, minIndexP As Integer
        Dim PointPoint = New Point(PointItem.PointPoint.X * width, PointItem.PointPoint.Y * height)
        Dim FirstCurvePoint = New Point(CurveItem.CurvePoint(0).X * width, CurveItem.CurvePoint(0).Y * height)
        dMinPointCurve = Find_TwoPointDistance(PointPoint.X, PointPoint.Y, FirstCurvePoint.X, FirstCurvePoint.Y)
        minIndexP = 0
        For i = 0 To CurveItem.CPointIndx
            Dim CurvePoint = New Point(CurveItem.CurvePoint(i).X * width, CurveItem.CurvePoint(i).Y * height)
            dPointCurve = Find_TwoPointDistance(PointPoint.X, PointPoint.Y, CurvePoint.X, CurvePoint.Y)

            If dMinPointCurve > dPointCurve Then dMinPointCurve = dPointCurve : minIndexP = i
        Next
        result.startPoint = PointItem.PointPoint
        result.endPoint = CurveItem.CurvePoint(minIndexP)
        result.length = dMinPointCurve
        result.name = Obj1.name & "To" & Obj2.name & "PMin"
        result.measuringType = MeasureType.objMinMax
        Return result
    End Function

    ''' <summary>
    ''' calculate min distance between two objects.
    ''' </summary>
    ''' <paramname="Obj1">The selected object.</param>
    ''' <paramname="Obj2">The selected object.</param>
    ''' <paramname="width">The width of picturebox.</param>
    ''' <paramname="height">The height of picturebox.</param>
    Public Function CalcPMinBetweenPointAndLine(ByVal Obj1 As MeasureObject, ByVal Obj2 As MeasureObject, ByVal width As Integer, ByVal height As Integer) As MeasureObject
        Dim result As MeasureObject = New MeasureObject()
        Dim PointItem = Obj1.curveObject.PointItem(0)
        Dim LineItem = Obj2.curveObject.LineItem(0)

        Dim dLinePoint As Integer
        Dim FirstPointofLine = New Point(LineItem.FirstPointOfLine.X * width, LineItem.FirstPointOfLine.Y * height)
        Dim SecndPointOfLine = New Point(LineItem.SecndPointOfLine.X * width, LineItem.SecndPointOfLine.Y * height)
        Dim PointPoint = New Point(PointItem.PointPoint.X * width, PointItem.PointPoint.Y * height)
        dLinePoint = pFind_BPointLineDistance(FirstPointofLine.X, FirstPointofLine.Y, SecndPointOfLine.X, SecndPointOfLine.Y, PointPoint.X, PointPoint.Y)

        result.endPoint = PointItem.PointPoint
        If Main_Form.OutPointFlag = True Then
            result.middlePoint = New PointF(Main_Form.DotX / CSng(width), Main_Form.DotY / CSng(height))
            result.dotFlag = True
        End If
        result.startPoint = New PointF(Main_Form.XsLinePoint / CSng(width), Main_Form.YsLinePoint / CSng(height))
        result.length = dLinePoint
        result.name = Obj1.name & "To" & Obj2.name & "PMin"
        result.measuringType = MeasureType.objMinMax
        Return result
    End Function

    ''' <summary>
    ''' calculate min distance between two objects.
    ''' </summary>
    ''' <paramname="Obj1">The selected object.</param>
    ''' <paramname="Obj2">The selected object.</param>
    ''' <paramname="width">The width of picturebox.</param>
    ''' <paramname="height">The height of picturebox.</param>
    Public Function CalcPMinBetweenPointAndPoly(ByVal Obj1 As MeasureObject, ByVal Obj2 As MeasureObject, ByVal width As Integer, ByVal height As Integer) As MeasureObject
        Dim result As MeasureObject = New MeasureObject()
        Dim PointItem = Obj1.curveObject.PointItem(0)
        Dim PolyItem = Obj2.curveObject.PolyItem(0)

        Dim PointPoint = New Point(PointItem.PointPoint.X * width, PointItem.PointPoint.Y * height)
        Dim dPolyPoint As Integer
        dPolyPoint = pFind_BPointPolyDistance(PolyItem, PointPoint, width, height)

        If Main_Form.POutFlag = True Then
            result.middlePoint = New PointF(Main_Form.PDotX / CSng(width), Main_Form.PDotY / CSng(height))
            result.dotFlag = True
        End If
        result.endPoint = PointItem.PointPoint
        result.startPoint = New PointF(Main_Form.PXs / CSng(width), Main_Form.PYs / CSng(height))
        result.length = dPolyPoint
        result.name = Obj1.name & "To" & Obj2.name & "PMin"
        result.measuringType = MeasureType.objMinMax
        Return result
    End Function

    ''' <summary>
    ''' calculate min distance between two objects.
    ''' </summary>
    ''' <paramname="Obj1">The selected object.</param>
    ''' <paramname="Obj2">The selected object.</param>
    ''' <paramname="width">The width of picturebox.</param>
    ''' <paramname="height">The height of picturebox.</param>
    Public Function CalcPMinBetweenLineAndPoly(ByVal Obj1 As MeasureObject, ByVal Obj2 As MeasureObject, ByVal width As Integer, ByVal height As Integer) As MeasureObject
        Dim result As MeasureObject = New MeasureObject()
        Dim LineItem = Obj1.curveObject.LineItem(0)
        Dim PolyItem = Obj2.curveObject.PolyItem(0)
        Dim FirstPointofLine = New Point(LineItem.FirstPointOfLine.X * width, LineItem.FirstPointOfLine.Y * height)
        Dim SecndPointOfLine = New Point(LineItem.SecndPointOfLine.X * width, LineItem.SecndPointOfLine.Y * height)

        Dim dLinePoly, minLinePoly, LPx, LPy, LPx1, LPy1, LPDotx, LPDoty As Integer
        Dim PLOutFlag As Boolean

        Dim FirstPolyPoint = New Point(PolyItem.PolyPoint(0).X * width, PolyItem.PolyPoint(0).Y * height)
        minLinePoly = pFind_BPointLineDistance(FirstPointofLine.X, FirstPointofLine.Y, SecndPointOfLine.X, SecndPointOfLine.Y, FirstPolyPoint.X, FirstPolyPoint.Y)
        LPx = Main_Form.XsLinePoint : LPy = Main_Form.YsLinePoint
        LPDotx = Main_Form.DotX : LPDoty = Main_Form.DotY
        LPx1 = FirstPolyPoint.X : LPy1 = FirstPolyPoint.Y
        PLOutFlag = Main_Form.OutPointFlag

        For j = 0 To PolyItem.PolyPointIndx
            Dim PolyPoint = New Point(PolyItem.PolyPoint(j).X * width, PolyItem.PolyPoint(j).Y * height)
            dLinePoly = pFind_BPointLineDistance(FirstPointofLine.X, FirstPointofLine.Y, SecndPointOfLine.X, SecndPointOfLine.Y, PolyPoint.X, PolyPoint.Y)
            If minLinePoly > dLinePoly Then
                minLinePoly = dLinePoly
                LPx = Main_Form.XsLinePoint : LPy = Main_Form.YsLinePoint
                LPDotx = Main_Form.DotX : LPDoty = Main_Form.DotY
                LPx1 = PolyPoint.X : LPy1 = PolyPoint.Y
                PLOutFlag = Main_Form.OutPointFlag
            End If
        Next

        If PLOutFlag = True Then
            result.middlePoint = New PointF(LPDotx / CSng(width), LPDoty / CSng(height))
            result.dotFlag = True
        End If
        result.startPoint = New PointF(LPx / CSng(width), LPy / CSng(height))
        result.endPoint = New PointF(LPx1 / CSng(width), LPy1 / CSng(height))
        result.length = minLinePoly
        result.name = Obj1.name & "To" & Obj2.name & "PMin"
        result.measuringType = MeasureType.objMinMax
        Return result
    End Function

    ''' <summary>
    ''' calculate min distance between two objects.
    ''' </summary>
    ''' <paramname="Obj1">The selected object.</param>
    ''' <paramname="Obj2">The selected object.</param>
    ''' <paramname="width">The width of picturebox.</param>
    ''' <paramname="height">The height of picturebox.</param>
    Public Function CalcPMinBetweenCurveAndPoly(ByVal Obj1 As MeasureObject, ByVal Obj2 As MeasureObject, ByVal width As Integer, ByVal height As Integer) As MeasureObject
        Dim result As MeasureObject = New MeasureObject()
        Dim CurveItem = Obj1.curveObject.CurveItem(0)
        Dim PolyItem = Obj2.curveObject.PolyItem(0)
        Dim FirstCurvePoint = New Point(CurveItem.CurvePoint(0).X * width, CurveItem.CurvePoint(0).Y * height)

        Dim dPolyCurve, mindPolyCurve, minIdx, PPx, PPy, PPDotx, PPDoty As Integer
        Dim PPOutFlag As Integer
        mindPolyCurve = pFind_BPointPolyDistance(PolyItem, FirstCurvePoint, width, height)
        minIdx = 0 : PPx = Main_Form.PXs : PPy = Main_Form.PYs : PPDotx = Main_Form.PDotX : PPDoty = Main_Form.PDotY : PPOutFlag = Main_Form.POutFlag
        For j = 0 To CurveItem.CPointIndx
            Dim CurvePoint = New Point(CurveItem.CurvePoint(j).X * width, CurveItem.CurvePoint(j).Y * height)
            dPolyCurve = pFind_BPointPolyDistance(PolyItem, CurvePoint, width, height)
            If mindPolyCurve > dPolyCurve Then
                mindPolyCurve = dPolyCurve
                minIdx = j
                PPx = Main_Form.PXs
                PPy = Main_Form.PYs
                PPDotx = Main_Form.PDotX : PPDoty = Main_Form.PDotY : PPOutFlag = Main_Form.POutFlag
            End If
        Next

        If PPOutFlag = True Then
            result.middlePoint = New PointF(PPDotx / CSng(width), PPDoty / CSng(height))
            result.dotFlag = True
        End If
        result.endPoint = CurveItem.CurvePoint(minIdx)
        result.startPoint = New PointF(PPx / CSng(width), PPy / CSng(height))
        result.length = mindPolyCurve
        result.name = Obj1.name & "To" & Obj2.name & "PMin"
        result.measuringType = MeasureType.objMinMax
        Return result
    End Function


#End Region

#Region "P Max"
    ''' <summary>
    ''' calculate max distance between two objects.
    ''' </summary>
    ''' <paramname="Obj1">The selected object.</param>
    ''' <paramname="Obj2">The selected object.</param>
    ''' <paramname="width">The width of picturebox.</param>
    ''' <paramname="height">The height of picturebox.</param>
    Public Function CalcPMaxBetweenCuPolyAndLine(ByVal Obj1 As MeasureObject, ByVal Obj2 As MeasureObject, ByVal width As Integer, ByVal height As Integer) As MeasureObject
        Dim result = New MeasureObject()
        Dim CuPolyItem = Obj1.curveObject.CuPolyItem(0)
        Dim LineItem = Obj2.curveObject.LineItem(0)
        Dim FirstPointofLine = New Point(LineItem.FirstPointOfLine.X * width, LineItem.FirstPointOfLine.Y * height)
        Dim SecndPointOfLine = New Point(LineItem.SecndPointOfLine.X * width, LineItem.SecndPointOfLine.Y * height)
        Dim FirstEdgeOfCuPoly = New Point(CuPolyItem.CuPolyPoint(1, 0).X * width, CuPolyItem.CuPolyPoint(1, 0).Y * height)

        Dim dMaxLineCurve, dLineCurve, maxIndex_j, maxIndex_k As Integer
        dMaxLineCurve = pFind_BPointLineDistance(FirstPointofLine.X, FirstPointofLine.Y, SecndPointOfLine.X, SecndPointOfLine.Y, FirstEdgeOfCuPoly.X, FirstEdgeOfCuPoly.Y)

        maxIndex_j = 1 : maxIndex_k = 0 : Main_Form.COutPointFlag = False

        For j = 1 To CuPolyItem.CuPolyPointIndx_j
            For k = 0 To CuPolyItem.CuPolyPointIndx_k(j)
                Dim KEdgeOfCuPoly = New Point(CuPolyItem.CuPolyPoint(j, k).X * width, CuPolyItem.CuPolyPoint(j, k).Y * height)
                dLineCurve = pFind_BPointLineDistance(FirstPointofLine.X, FirstPointofLine.Y, SecndPointOfLine.X, SecndPointOfLine.Y, KEdgeOfCuPoly.X, KEdgeOfCuPoly.Y)

                If Main_Form.OutPointFlag = True Then Main_Form.COutPointFlag = True : Main_Form.CDotX = Main_Form.DotX : Main_Form.CDotY = Main_Form.DotY
                If dMaxLineCurve < dLineCurve Then dMaxLineCurve = dLineCurve : maxIndex_j = j : maxIndex_k = k : 
            Next
        Next
        Dim CuPolyPoint = New Point(CuPolyItem.CuPolyPoint(maxIndex_j, maxIndex_k).X * width, CuPolyItem.CuPolyPoint(maxIndex_j, maxIndex_k).Y * height)
        dLineCurve = pFind_BPointLineDistance(FirstPointofLine.X, FirstPointofLine.Y, SecndPointOfLine.X, SecndPointOfLine.Y, CuPolyPoint.X, CuPolyPoint.Y)
        If Main_Form.OutPointFlag = True Then Main_Form.COutPointFlag = True : Main_Form.CDotX = Main_Form.DotX : Main_Form.CDotY = Main_Form.DotY

        If Main_Form.COutPointFlag = True Then
            result.middlePoint = New PointF(Main_Form.CDotX / CSng(width), Main_Form.CDotY / CSng(height))
            result.dotFlag = True
        End If
        result.name = Obj1.name & "To" & Obj2.name & "PMax"
        result.length = dMaxLineCurve
        result.measuringType = MeasureType.objMinMax
        result.endPoint = CuPolyItem.CuPolyPoint(maxIndex_j, maxIndex_k)
        result.startPoint = New PointF(Main_Form.XsLinePoint / CSng(width), Main_Form.YsLinePoint / CSng(height))
        Return result
    End Function

    ''' <summary>
    ''' calculate max distance between two objects.
    ''' </summary>
    ''' <paramname="Obj1">The selected object.</param>
    ''' <paramname="Obj2">The selected object.</param>
    ''' <paramname="width">The width of picturebox.</param>
    ''' <paramname="height">The height of picturebox.</param>
    Public Function CalcPMaxBetweenCuPolyAndPoint(ByVal Obj1 As MeasureObject, ByVal Obj2 As MeasureObject, ByVal width As Integer, ByVal height As Integer) As MeasureObject
        Dim result = New MeasureObject()
        Dim CuPolyItem = Obj1.curveObject.CuPolyItem(0)
        Dim PointItem = Obj2.curveObject.PointItem(0)

        Dim PointPoint = New Point(PointItem.PointPoint.X * width, PointItem.PointPoint.Y * height)
        Dim FirstEdgeOfCuPoly = New Point(CuPolyItem.CuPolyPoint(1, 0).X * width, CuPolyItem.CuPolyPoint(1, 0).Y * height)

        Dim dMaxPointCuPoly, dPointCuPoly, maxIndexPCuPoly_j, maxIndexPCuPoly_k, maxExistFlag, Cx, Cy As Integer
        dMaxPointCuPoly = Find_TwoPointDistance(PointPoint.X, PointPoint.Y, FirstEdgeOfCuPoly.X, FirstEdgeOfCuPoly.Y)
        maxIndexPCuPoly_j = 1 : maxIndexPCuPoly_k = 0
        For j = 1 To CuPolyItem.CuPolyPointIndx_j
            For k = 0 To CuPolyItem.CuPolyPointIndx_k(j)
                Dim KEdgeOfCuPoly = New Point(CuPolyItem.CuPolyPoint(j, k).X * width, CuPolyItem.CuPolyPoint(j, k).Y * height)
                dPointCuPoly = Find_TwoPointDistance(PointPoint.X, PointPoint.Y, KEdgeOfCuPoly.X, KEdgeOfCuPoly.Y)
                If dMaxPointCuPoly < dPointCuPoly Then dMaxPointCuPoly = dPointCuPoly : maxIndexPCuPoly_j = j : maxIndexPCuPoly_k = k
            Next
        Next

        result.name = Obj1.name & "To" & Obj2.name & "PMax"
        result.length = dMaxPointCuPoly
        result.measuringType = MeasureType.objMinMax
        result.startPoint = CuPolyItem.CuPolyPoint(maxIndexPCuPoly_j, maxIndexPCuPoly_k)
        result.endPoint = PointItem.PointPoint
        Return result
    End Function

    ''' <summary>
    ''' calculate max distance between two objects.
    ''' </summary>
    ''' <paramname="Obj1">The selected object.</param>
    ''' <paramname="Obj2">The selected object.</param>
    ''' <paramname="width">The width of picturebox.</param>
    ''' <paramname="height">The height of picturebox.</param>
    Public Function CalcPMaxBetweenCurveAndLine(ByVal Obj1 As MeasureObject, ByVal Obj2 As MeasureObject, ByVal width As Integer, ByVal height As Integer) As MeasureObject
        Dim result = New MeasureObject()
        Dim CurveItem = Obj1.curveObject.CurveItem(0)
        Dim LineItem = Obj2.curveObject.LineItem(0)

        Dim FirstPointofLine = New Point(LineItem.FirstPointOfLine.X * width, LineItem.FirstPointOfLine.Y * height)
        Dim SecndPointOfLine = New Point(LineItem.SecndPointOfLine.X * width, LineItem.SecndPointOfLine.Y * height)
        Dim FirstCurvePoint = New Point(CurveItem.CurvePoint(0).X * width, CurveItem.CurvePoint(0).Y * height)

        Dim dMaxLineCurve, dLineCurve, maxIndex As Integer

        dMaxLineCurve = pFind_BPointLineDistance(FirstPointofLine.X, FirstPointofLine.Y, SecndPointOfLine.X, SecndPointOfLine.Y, FirstCurvePoint.X, FirstCurvePoint.Y)
        maxIndex = 0 : Main_Form.COutPointFlag = False
        For i = 0 To CurveItem.CPointIndx
            Dim CurvePoint = New Point(CurveItem.CurvePoint(i).X * width, CurveItem.CurvePoint(i).Y * height)
            dLineCurve = pFind_BPointLineDistance(FirstPointofLine.X, FirstPointofLine.Y, SecndPointOfLine.X, SecndPointOfLine.Y, CurvePoint.X, CurvePoint.Y)
            If Main_Form.OutPointFlag = True Then Main_Form.COutPointFlag = True : Main_Form.CDotX = Main_Form.DotX : Main_Form.CDotY = Main_Form.DotY
            If dMaxLineCurve < dLineCurve Then dMaxLineCurve = dLineCurve : maxIndex = i
        Next
        Dim CurvePoint1 = New Point(CurveItem.CurvePoint(maxIndex).X * width, CurveItem.CurvePoint(maxIndex).Y * height)
        dLineCurve = pFind_BPointLineDistance(FirstPointofLine.X, FirstPointofLine.Y, SecndPointOfLine.X, SecndPointOfLine.Y, CurvePoint1.X, CurvePoint1.Y)
        If Main_Form.OutPointFlag = True Then Main_Form.COutPointFlag = True : Main_Form.CDotX = Main_Form.DotX : Main_Form.CDotY = Main_Form.DotY

        If Main_Form.COutPointFlag = True Then
            result.middlePoint = New PointF(Main_Form.CDotX / CSng(width), Main_Form.CDotY / CSng(height))
            result.dotFlag = True
        End If
        result.name = Obj1.name & "To" & Obj2.name & "PMax"
        result.length = dMaxLineCurve
        result.measuringType = MeasureType.objMinMax
        result.endPoint = CurveItem.CurvePoint(maxIndex)
        result.startPoint = New PointF(Main_Form.XsLinePoint / CSng(width), Main_Form.YsLinePoint / CSng(height))
        Return result

    End Function

    ''' <summary>
    ''' calculate max distance between two objects.
    ''' </summary>
    ''' <paramname="Obj1">The selected object.</param>
    ''' <paramname="Obj2">The selected object.</param>
    ''' <paramname="width">The width of picturebox.</param>
    ''' <paramname="height">The height of picturebox.</param>
    Public Function CalcPMaxBetweenCurveAndPoint(ByVal Obj1 As MeasureObject, ByVal Obj2 As MeasureObject, ByVal width As Integer, ByVal height As Integer) As MeasureObject
        Dim result As MeasureObject = New MeasureObject()
        Dim CurveItem = Obj1.curveObject.CurveItem(0)
        Dim PointItem = Obj2.curveObject.PointItem(0)

        Dim PointPoint = New Point(PointItem.PointPoint.X * width, PointItem.PointPoint.Y * height)
        Dim FirstCurvePoint = New Point(CurveItem.CurvePoint(0).X * width, CurveItem.CurvePoint(0).Y * height)

        Dim maxCL_Dis, CL_Dis, maxIdx As Integer
        Dim CrosPoint As Point
        maxCL_Dis = Find_TwoPointDistance(FirstCurvePoint.X, FirstCurvePoint.Y, PointPoint.X, PointPoint.Y)
        CrosPoint.X = PointPoint.X
        CrosPoint.Y = PointPoint.Y
        maxIdx = 0
        For j = 0 To CurveItem.CPointIndx
            Dim CurvePoint = New Point(CurveItem.CurvePoint(j).X * width, CurveItem.CurvePoint(j).Y * height)
            CL_Dis = Find_TwoPointDistance(CurvePoint.X, CurvePoint.Y, PointPoint.X, PointPoint.Y)
            If maxCL_Dis < CL_Dis Then
                maxCL_Dis = CL_Dis
                maxIdx = j
                CrosPoint.X = PointPoint.X
                CrosPoint.Y = PointPoint.Y
            End If
        Next

        result.name = Obj1.name & "To" & Obj2.name & "PMax"
        result.length = maxCL_Dis
        result.measuringType = MeasureType.objMinMax
        result.startPoint = CurveItem.CurvePoint(maxIdx)
        result.endPoint = New PointF(CrosPoint.X / CSng(width), CrosPoint.Y / CSng(height))
        Return result
    End Function

    ''' <summary>
    ''' calculate max distance between two objects.
    ''' </summary>
    ''' <paramname="Obj1">The selected object.</param>
    ''' <paramname="Obj2">The selected object.</param>
    ''' <paramname="width">The width of picturebox.</param>
    ''' <paramname="height">The height of picturebox.</param>
    Public Function CalcPMaxBetweenCurveAndPoly(ByVal Obj1 As MeasureObject, ByVal Obj2 As MeasureObject, ByVal width As Integer, ByVal height As Integer) As MeasureObject
        Dim result As MeasureObject = New MeasureObject()
        Dim CurveItem = Obj1.curveObject.CurveItem(0)
        Dim PolyItem = Obj2.curveObject.PolyItem(0)
        Dim FirstCurvePoint = New Point(CurveItem.CurvePoint(0).X * width, CurveItem.CurvePoint(0).Y * height)
        Dim FirstPolyPoint = New Point(PolyItem.PolyPoint(0).X * width, PolyItem.PolyPoint(0).Y * height)

        Dim dPolyCurve, maxdPolyCurve, maxIdx, PPx, PPy, PPDotx, PPDoty As Integer
        Dim PPOutFlag As Boolean
        maxdPolyCurve = pFind_BPointPolyMaxDistance(PolyItem, FirstCurvePoint, width, height)
        maxIdx = 0 : PPx = Main_Form.PXs : PPy = Main_Form.PYs : PPDotx = Main_Form.PDotX : PPDoty = Main_Form.PDotY : PPOutFlag = Main_Form.POutFlag

        For j = 0 To CurveItem.CPointIndx
            Dim CurvePoint = New Point(CurveItem.CurvePoint(j).X * width, CurveItem.CurvePoint(j).Y * height)
            dPolyCurve = pFind_BPointPolyMaxDistance(PolyItem, CurvePoint, width, height)
            If maxdPolyCurve < dPolyCurve Then
                maxdPolyCurve = dPolyCurve
                maxIdx = j
                PPx = Main_Form.PXs
                PPy = Main_Form.PYs
                PPDotx = Main_Form.PDotX : PPDoty = Main_Form.PDotY : PPOutFlag = Main_Form.POutFlag
            End If
        Next

        If PPOutFlag = True Then
            result.middlePoint = New PointF(PPDotx / CSng(width), PPDoty / CSng(height))
            result.dotFlag = True
        End If
        result.name = Obj1.name & "To" & Obj2.name & "PMax"
        result.length = maxdPolyCurve
        result.measuringType = MeasureType.objMinMax
        result.endPoint = CurveItem.CurvePoint(maxIdx)
        result.startPoint = New PointF(PPx / CSng(width), PPy / CSng(height))
        Return result

    End Function

    ''' <summary>
    ''' calculate max distance between two objects.
    ''' </summary>
    ''' <paramname="Obj1">The selected object.</param>
    ''' <paramname="Obj2">The selected object.</param>
    ''' <paramname="width">The width of picturebox.</param>
    ''' <paramname="height">The height of picturebox.</param>
    Public Function CalcPMaxBetweenLineAndPoint(ByVal Obj1 As MeasureObject, ByVal Obj2 As MeasureObject, ByVal width As Integer, ByVal height As Integer) As MeasureObject
        Dim result As MeasureObject = New MeasureObject()
        Dim PointItem = Obj2.curveObject.PointItem(0)
        Dim LineItem = Obj1.curveObject.LineItem(0)

        Dim FirstPointofLine = New Point(LineItem.FirstPointOfLine.X * width, LineItem.FirstPointOfLine.Y * height)
        Dim SecndPointOfLine = New Point(LineItem.SecndPointOfLine.X * width, LineItem.SecndPointOfLine.Y * height)
        Dim PointPoint = New Point(PointItem.PointPoint.X * width, PointItem.PointPoint.Y * height)

        Dim dLinePoint As Integer
        dLinePoint = pFind_BPointLineDistance(FirstPointofLine.X, FirstPointofLine.Y, SecndPointOfLine.X, SecndPointOfLine.Y, PointPoint.X, PointPoint.Y)

        If Main_Form.OutPointFlag = True Then
            result.middlePoint = New PointF(Main_Form.DotX / CSng(width), Main_Form.DotY / CSng(height))
            result.dotFlag = True
        End If
        result.name = Obj1.name & "To" & Obj2.name & "PMax"
        result.length = dLinePoint
        result.measuringType = MeasureType.objMinMax
        result.endPoint = PointItem.PointPoint
        result.startPoint = New PointF(Main_Form.XsLinePoint / CSng(width), Main_Form.YsLinePoint / CSng(height))
        Return result
    End Function

    ''' <summary>
    ''' calculate max distance between two objects.
    ''' </summary>
    ''' <paramname="Obj1">The selected object.</param>
    ''' <paramname="Obj2">The selected object.</param>
    ''' <paramname="width">The width of picturebox.</param>
    ''' <paramname="height">The height of picturebox.</param>
    Public Function CalcPMaxBetweenLineAndPoly(ByVal Obj1 As MeasureObject, ByVal Obj2 As MeasureObject, ByVal width As Integer, ByVal height As Integer) As MeasureObject
        Dim result As MeasureObject = New MeasureObject()
        Dim LineItem = Obj1.curveObject.LineItem(0)
        Dim PolyItem = Obj2.curveObject.PolyItem(0)
        Dim FirstPointofLine = New Point(LineItem.FirstPointOfLine.X * width, LineItem.FirstPointOfLine.Y * height)
        Dim SecndPointOfLine = New Point(LineItem.SecndPointOfLine.X * width, LineItem.SecndPointOfLine.Y * height)

        Dim dLinePoly, maxLinePoly, LPx, LPy, LPx1, LPy1, LPDotx, LPDoty As Integer
        Dim PLOutFlag As Boolean
        Dim PolyPoint = New Point(PolyItem.PolyPoint(0).X * width, PolyItem.PolyPoint(0).Y * height)

        maxLinePoly = pFind_BPointLineDistance(FirstPointofLine.X, FirstPointofLine.Y, SecndPointOfLine.X, SecndPointOfLine.Y, PolyPoint.X, PolyPoint.Y)
        LPx = Main_Form.XsLinePoint : LPy = Main_Form.YsLinePoint
        LPDotx = Main_Form.DotX : LPDoty = Main_Form.DotY
        LPx1 = PolyPoint.X : LPy1 = PolyPoint.Y
        PLOutFlag = Main_Form.OutPointFlag

        For j = 0 To PolyItem.PolyPointIndx
            PolyPoint = New Point(PolyItem.PolyPoint(j).X * width, PolyItem.PolyPoint(j).Y * height)
            dLinePoly = pFind_BPointLineDistance(FirstPointofLine.X, FirstPointofLine.Y, SecndPointOfLine.X, SecndPointOfLine.Y, PolyPoint.X, PolyPoint.Y)
            If maxLinePoly < dLinePoly Then
                maxLinePoly = dLinePoly
                LPx = Main_Form.XsLinePoint : LPy = Main_Form.YsLinePoint
                LPDotx = Main_Form.DotX : LPDoty = Main_Form.DotY
                LPx1 = PolyPoint.X : LPy1 = PolyPoint.Y
                PLOutFlag = Main_Form.OutPointFlag
            End If
        Next

        If PLOutFlag = True Then
            result.middlePoint = New PointF(LPDotx / CSng(width), LPDoty / CSng(height))
            result.dotFlag = True
        End If
        result.name = Obj1.name & "To" & Obj2.name & "PMax"
        result.length = maxLinePoly
        result.measuringType = MeasureType.objMinMax
        result.startPoint = New PointF(LPx / CSng(width), LPy / CSng(height))
        result.endPoint = New PointF(LPx1 / CSng(width), LPy1 / CSng(height))
        Return result

    End Function

    ''' <summary>
    ''' calculate max distance between two objects.
    ''' </summary>
    ''' <paramname="Obj1">The selected object.</param>
    ''' <paramname="Obj2">The selected object.</param>
    ''' <paramname="width">The width of picturebox.</param>
    ''' <paramname="height">The height of picturebox.</param>
    Public Function CalcPMaxBetweenPolyAndPoint(ByVal Obj1 As MeasureObject, ByVal Obj2 As MeasureObject, ByVal width As Integer, ByVal height As Integer) As MeasureObject
        Dim result As MeasureObject = New MeasureObject()
        Dim PointItem = Obj2.curveObject.PointItem(0)
        Dim PolyItem = Obj1.curveObject.PolyItem(0)

        Dim PointPoint = New Point(PointItem.PointPoint.X * width, PointItem.PointPoint.Y * height)
        Dim dPolyPoint As Integer
        Dim PolyPoint = New Point(PolyItem.PolyPoint(0).X * width, PolyItem.PolyPoint(0).Y * height)

        dPolyPoint = pFind_BPointPolyMaxDistance(PolyItem, PointPoint, width, height)

        If Main_Form.POutFlag = True Then
            result.middlePoint = New PointF(Main_Form.PDotX / CSng(width), Main_Form.PDotY / CSng(height))
            result.dotFlag = True
        End If
        result.name = Obj1.name & "To" & Obj2.name & "PMax"
        result.length = dPolyPoint
        result.measuringType = MeasureType.objMinMax
        result.endPoint = PointItem.PointPoint
        result.startPoint = New PointF(Main_Form.PXs / CSng(width), Main_Form.PYs / CSng(height))
        Return result
    End Function

#End Region
End Module
