
Imports System.Drawing
Imports System.Runtime.CompilerServices
Imports System.Windows.Forms.DataFormats
Imports DocumentFormat.OpenXml.Bibliography
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
    ''' <paramname="flag">The flag specifies whether the item is selected or not.</param>
    Public Sub DrawCurveObjItem(ByVal graph As Graphics, ByVal pictureBox As PictureBox, ByVal item As MeasureObject, ByVal flag As Boolean)
        Dim graphPen As Pen
        If flag = True Then
            graphPen = New Pen(Color.Red, item.line_infor.line_width)
        Else
            graphPen = New Pen(item.line_infor.line_color, item.line_infor.line_width)
        End If
        Dim PenRed = New Pen(Color.Red, item.line_infor.line_width)

        Dim graphFont = item.font_infor.text_font
        Dim graphBrush As SolidBrush = New SolidBrush(item.font_infor.font_color)

        If item.measure_type = MeasureType.C_Line Then
            Dim obj = item.curve_object.LineItem(0)
            Dim FirstPt As Point = New Point(obj.FirstPointOfLine.X * pictureBox.Width, obj.FirstPointOfLine.Y * pictureBox.Height)
            Dim SecondPt As Point = New Point(obj.SecndPointOfLine.X * pictureBox.Width, obj.SecndPointOfLine.Y * pictureBox.Height)
            If item.obj_num = Main_Form.LRealSelectArrayIndx Then
                graph.DrawLine(PenRed, CInt(FirstPt.X), CInt(FirstPt.Y), CInt(SecondPt.X), CInt(SecondPt.Y))
            Else
                graph.DrawLine(graphPen, CInt(FirstPt.X), CInt(FirstPt.Y), CInt(SecondPt.X), CInt(SecondPt.Y))
            End If

            'draw label
            Dim DrawPt = New Point(CInt(obj.LDrawPos.X * pictureBox.Width), CInt(obj.LDrawPos.Y * pictureBox.Height))
            graph.DrawString(item.name, graphFont, graphBrush, New RectangleF(DrawPt.X, DrawPt.Y, 30, 20))
        ElseIf item.measure_type = MeasureType.C_Point Then
            Dim obj = item.curve_object.PointItem(0)
            Dim PointPt = New Point(CInt(obj.PointPoint.X * pictureBox.Width), CInt(obj.PointPoint.Y * pictureBox.Height))
            If item.obj_num = Main_Form.PRealSelectArrayIndx Then
                graph.DrawEllipse(PenRed, New Rectangle(PointPt.X, PointPt.Y, 2, 2))
            Else
                graph.DrawEllipse(graphPen, New Rectangle(PointPt.X, PointPt.Y, 2, 2))
            End If
            Dim DrawPt = New Point(CInt(obj.PDrawPos.X * pictureBox.Width), CInt(obj.PDrawPos.Y * pictureBox.Height))
            graph.DrawString(item.name, graphFont, graphBrush, New RectangleF(DrawPt.X, DrawPt.Y, 30, 20))
        ElseIf item.measure_type = MeasureType.C_Poly Then
            Dim obj = item.curve_object.PolyItem(0)
            For i = 0 To obj.PolyPointIndx - 1
                Dim startPt = New Point(CInt(obj.PolyPoint(i).X * pictureBox.Width), CInt(obj.PolyPoint(i).Y * pictureBox.Height))
                Dim EndPt = New Point(CInt(obj.PolyPoint(i + 1).X * pictureBox.Width), CInt(obj.PolyPoint(i + 1).Y * pictureBox.Height))
                If item.obj_num = Main_Form.PolyRealSelectArrayIndx Then
                    graph.DrawLine(PenRed, CInt(startPt.X), CInt(startPt.Y), CInt(EndPt.X), CInt(EndPt.Y))
                Else
                    graph.DrawLine(graphPen, CInt(startPt.X), CInt(startPt.Y), CInt(EndPt.X), CInt(EndPt.Y))
                End If
            Next
            Dim DrawPt = New Point(CInt(obj.PolyDrawPos.X * pictureBox.Width), CInt(obj.PolyDrawPos.Y * pictureBox.Height))
            graph.DrawString(item.name, graphFont, graphBrush, New RectangleF(DrawPt.X, DrawPt.Y, 40, 20))

        ElseIf item.measure_type = MeasureType.C_Curve Then
            Dim obj = item.curve_object.CurveItem(0)
            For i = 0 To obj.CPointIndx - 1
                Dim startPt = New Point(CInt(obj.CurvePoint(i).X * pictureBox.Width), CInt(obj.CurvePoint(i).Y * pictureBox.Height))
                Dim EndPt = New Point(CInt(obj.CurvePoint(i + 1).X * pictureBox.Width), CInt(obj.CurvePoint(i + 1).Y * pictureBox.Height))
                If item.obj_num = Main_Form.CRealSelectArrayIndx Then
                    graph.DrawLine(PenRed, CInt(startPt.X), CInt(startPt.Y), CInt(EndPt.X), CInt(EndPt.Y))
                Else
                    graph.DrawLine(graphPen, CInt(startPt.X), CInt(startPt.Y), CInt(EndPt.X), CInt(EndPt.Y))
                End If
            Next
            Dim DrawPt = New Point(CInt(obj.CDrawPos.X * pictureBox.Width), CInt(obj.CDrawPos.Y * pictureBox.Height))
            graph.DrawString(item.name, graphFont, graphBrush, New RectangleF(DrawPt.X, DrawPt.Y, 30, 20))
        ElseIf item.measure_type = MeasureType.C_CuPoly Then
            Dim obj = item.curve_object.CuPolyItem(0)
            For i = 1 To obj.CuPolyPointIndx_j
                For j = 0 To obj.CuPolyPointIndx_k(i) - 2
                    Dim startPt = New Point(CInt(obj.CuPolyPoint(i, j).X * pictureBox.Width), CInt(obj.CuPolyPoint(i, j).Y * pictureBox.Height))
                    Dim EndPt = New Point(CInt(obj.CuPolyPoint(i, j + 1).X * pictureBox.Width), CInt(obj.CuPolyPoint(i, j + 1).Y * pictureBox.Height))
                    If item.obj_num = Main_Form.CuPolyRealSelectArrayIndx Then
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
                    If item.obj_num = Main_Form.CuPolyRealSelectArrayIndx Then
                        graph.DrawLine(PenRed, CInt(startPt.X), CInt(startPt.Y), CInt(EndPt.X), CInt(EndPt.Y))
                    Else
                        graph.DrawLine(graphPen, CInt(startPt.X), CInt(startPt.Y), CInt(EndPt.X), CInt(EndPt.Y))
                    End If
                End If
            Next
            Dim DrawPt = New Point(CInt(obj.CuPolyDrawPos.X * pictureBox.Width), CInt(obj.CuPolyDrawPos.Y * pictureBox.Height))
            graph.DrawString(item.name, graphFont, graphBrush, New RectangleF(DrawPt.X, DrawPt.Y, 30, 20))
        End If
        PenRed.Dispose()
        graphBrush.Dispose()
        graphPen.Dispose()
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
    Public Sub DrawPolyObj(ByVal pictureBox As PictureBox, ByVal line_infor As LineStyle, ByVal Obj As C_PolyObject)
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
    Public Sub DrawCurveObj(ByVal pictureBox As PictureBox, ByVal line_infor As LineStyle, ByVal Obj As C_CurveObject)
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
    Public Sub DrawCuPolyObj(ByVal pictureBox As PictureBox, ByVal line_infor As LineStyle, ByVal Obj As C_CuPolyObject)
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
            If item.measure_type < MeasureType.C_Line Then
                Continue For
            End If
            If item.measure_type = MeasureType.C_CuPoly Then
                Dim obj = item.curve_object.CuPolyItem(0)
                For i = 0 To obj.CuPolyPointIndx_j
                    For j = 0 To obj.CuPolyPointIndx_k(i) - 1
                        If m_pt.X > obj.CuPolyPoint(i, j).X - 0.01 And m_pt.X < obj.CuPolyPoint(i, j).X + 0.01 And m_pt.Y > obj.CuPolyPoint(i, j).Y - 0.01 And m_pt.Y < obj.CuPolyPoint(i, j).Y + 0.01 Then
                            index = item.obj_num
                            Exit For
                        End If
                    Next

                    If i >= 1 Then
                        Dim startPt = New Point(obj.CuPolyPoint(i - 1, obj.CuPolyPointIndx_k(i - 1)).X * pictureBox.Width, obj.CuPolyPoint(i - 1, obj.CuPolyPointIndx_k(i - 1)).Y * pictureBox.Height)
                        Dim EndPt = New Point(obj.CuPolyPoint(i, 0).X * pictureBox.Width, obj.CuPolyPoint(i, 0).Y * pictureBox.Height)
                        Dim dist = Find_BPointLineDistance(startPt.X, startPt.Y, EndPt.X, EndPt.Y, mPt.X, mPt.Y)

                        If dist < 5 And Main_Form.OutPointFlag = False Then
                            index = item.obj_num
                        End If
                    End If
                Next
            ElseIf item.measure_type = MeasureType.C_Curve Then
                Dim obj = item.curve_object.CurveItem(0)
                For i = 0 To obj.CPointIndx - 1
                    If m_pt.X > obj.CurvePoint(i).X - 0.01 And m_pt.X < obj.CurvePoint(i).X + 0.01 And m_pt.Y > obj.CurvePoint(i).Y - 0.01 And m_pt.Y < obj.CurvePoint(i).Y + 0.01 Then
                        index = item.obj_num
                        Exit For
                    End If
                Next
            ElseIf item.measure_type = MeasureType.C_Line Then
                Dim obj = item.curve_object.LineItem(0)
                Dim startPt = New Point(obj.FirstPointOfLine.X * pictureBox.Width, obj.FirstPointOfLine.Y * pictureBox.Height)
                Dim EndPt = New Point(obj.SecndPointOfLine.X * pictureBox.Width, obj.SecndPointOfLine.Y * pictureBox.Height)
                Dim dist = Find_BPointLineDistance(startPt.X, startPt.Y, EndPt.X, EndPt.Y, mPt.X, mPt.Y)
                If dist < 5 And Main_Form.OutPointFlag = False Then
                    index = item.obj_num
                End If
            ElseIf item.measure_type = MeasureType.C_Point Then
                Dim obj = item.curve_object.PointItem(0)
                If m_pt.X < obj.PointPoint.X + 0.01 And m_pt.X > obj.PointPoint.X - 0.01 And m_pt.Y < obj.PointPoint.Y + 0.01 And m_pt.Y > obj.PointPoint.Y - 0.01 Then
                    index = item.obj_num
                End If
            ElseIf item.measure_type = MeasureType.C_Poly Then
                Dim obj = item.curve_object.PolyItem(0)
                If obj.PolyPointIndx > 0 Then
                    Dim dist = Find_BPointPolyDistance(obj, mPt, pictureBox.Width, pictureBox.Height)
                    If dist < 5 Then
                        index = item.obj_num
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
    Public Sub DrawCurveObjSelected(ByVal pictureBox As PictureBox, ByVal item As MeasureObject)
        Dim graph As Graphics = pictureBox.CreateGraphics()
        DrawCurveObjItem(graph, pictureBox, item, True)
        graph.Dispose()
    End Sub

#End Region

#Region "Initialize"
    ''' <summary>
    ''' Initialize variables.
    ''' </summary>
    Public Sub initVar()

        Main_Form.CurvePreviousPoint = Nothing
        Main_Form.CReadySelectFalg = False
        Main_Form.LReadySelectArrayIndx = -1
        Main_Form.LRealSelectArrayIndx = -1
        Main_Form.PolyDrawEndFlag = False
        Main_Form.PReadySelectArrayIndx = -1
        Main_Form.PRealSelectArrayIndx = -1
        Main_Form.PolyReadySelectArrayIndx = -1
        Main_Form.PolyRealSelectArrayIndx = -1
        Main_Form.CReadySelectArrayIndx = -1
        Main_Form.CRealSelectArrayIndx = -1
        Main_Form.CuPolyReadySelectArrayIndx = -1
        Main_Form.CuPolyRealSelectArrayIndx = -1
        Main_Form.dumyPoint.X = -1
        Main_Form.dumyPoint.Y = -1

        Main_Form.PRealSelectArrayIndx_L = -1
        Main_Form.CRealSelectArrayIndx_L = -1
        Main_Form.PolyRealSelectArrayIndx_L = -1
        Main_Form.PolyRealSelectArrayIndx_L = -1
        Main_Form.PReadySelectArrayIndx_L = -1
        Main_Form.CReadySelectArrayIndx_L = -1
        Main_Form.PolyReadySelectArrayIndx_L = -1
        Main_Form.PolyReadySelectArrayIndx_L = -1
        Main_Form.CuPolyRealSelectArrayIndx_L = -1
        Main_Form.CuPolyReadySelectArrayIndx_L = -1

        Main_Form.COutPointFlag = False

    End Sub
#End Region
End Module
