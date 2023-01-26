Imports System.Reflection
Imports System.Runtime.CompilerServices


''' <summary>
''' This class contains utilities.
''' </summary>
Public Module Utils
    'stop pressing button using keyboard 
    Private ReadOnly SetStyle As Action(Of Control, ControlStyles, Boolean) = CType([Delegate].CreateDelegate(GetType(Action(Of Control, ControlStyles, Boolean)), GetType(Control).GetMethod("SetStyle", BindingFlags.NonPublic Or BindingFlags.Instance, Nothing, {GetType(ControlStyles), GetType(Boolean)}, Nothing)), Action(Of Control, ControlStyles, Boolean))

    <Extension()>
    Public Sub DisableSelect(ByVal target As Control)
        SetStyle(target, ControlStyles.Selectable, False)
    End Sub
    ''' <summary>
    ''' Get normal vector from point m_pt to line.
    ''' </summary>
    ''' <paramname="start_point">The start point of line.</param>
    ''' <paramname="end_point">The end point of line.</param>
    ''' <paramname="m_pt">The point out of line.</param>
    Public Function GetNormalFromPointToLine(ByVal start_point As Point, ByVal end_point As Point, ByVal m_pt As Point) As Size
        Dim offset As Size = New Size(0, 0)

        Dim a = end_point.Y - start_point.Y
        Dim b = start_point.X - end_point.X
        Dim c = end_point.X * start_point.Y - end_point.Y * start_point.X

        If a <> 0 OrElse b <> 0 Then
            Dim dist = Math.Abs((a * m_pt.X + b * m_pt.Y + c) / Math.Sqrt(a * a + b * b))
            offset.Width = Convert.ToInt32(a / Math.Sqrt(a * a + b * b) * dist)
            offset.Height = Convert.ToInt32(b / Math.Sqrt(a * a + b * b) * dist)

            Dim val = a * m_pt.X + b * m_pt.Y + c
            If val > 0 Then
                offset.Width *= -1
                offset.Height *= -1
            End If
        End If

        Return offset
    End Function

    ''' <summary>
    ''' Get normal vector from point m_pt to line whose length is l_length.
    ''' </summary>
    ''' <paramname="start_point">The start point of line.</param>
    ''' <paramname="end_point">The end point of line.</param>
    ''' <paramname="m_pt">The point out of line.</param>
    ''' <paramname="l_length">The length of normal vector.</param>
    Public Function GetNormalFromPointToLine(ByVal start_point As Point, ByVal end_point As Point, ByVal m_pt As Point, ByVal l_length As Integer) As SizeF
        Dim offset As Size = New Size(0, 0)

        Dim a = end_point.Y - start_point.Y
        Dim b = start_point.X - end_point.X
        Dim c = end_point.X * start_point.Y - end_point.Y * start_point.X

        If a <> 0 OrElse b <> 0 Then

            offset.Width = Convert.ToInt32(a / Math.Sqrt(a * a + b * b) * l_length)
            offset.Height = Convert.ToInt32(b / Math.Sqrt(a * a + b * b) * l_length)

            Dim val = a * m_pt.X + b * m_pt.Y + c
            If val > 0 Then
                offset.Width *= -1
                offset.Height *= -1
            End If
        End If

        Return offset
    End Function

    ''' <summary>
    ''' Get Y position .
    ''' </summary>
    ''' <paramname="start_point">The start point of line.</param>
    ''' <paramname="end_point">The end point of line.</param>
    ''' <paramname="target_point">target point.</param>
    Public Function GetYCoordinate(ByVal start_point As Point, ByVal end_point As Point, ByVal target_point As Point) As Integer

        Dim a = end_point.Y - start_point.Y
        Dim b = start_point.X - end_point.X
        Dim c = end_point.X * start_point.Y - end_point.Y * start_point.X
        Dim y = target_point.Y

        If b <> 0 Then
            y = (0 - c - a * target_point.X) / b
        End If

        Return y
    End Function

    ''' <summary>
    ''' Get X position .
    ''' </summary>
    ''' <paramname="start_point">The start point of line.</param>
    ''' <paramname="end_point">The end point of line.</param>
    ''' <paramname="target_point">The target point.</param>
    Public Function GetXCoordinate(ByVal start_point As Point, ByVal end_point As Point, ByVal target_point As Point) As Integer

        Dim a = end_point.Y - start_point.Y
        Dim b = start_point.X - end_point.X
        Dim c = end_point.X * start_point.Y - end_point.Y * start_point.X
        Dim x = target_point.X

        If a <> 0 Then
            x = (0 - c - b * target_point.Y) / a
        End If

        Return x
    End Function
    ''' <summary>
    ''' Get unit vector
    ''' </summary>
    ''' <paramname="start_point">The start point of line.</param>
    ''' <paramname="end_point">The end point of line.</param>
    Public Function GetUnitVector(ByVal start_point As Point, ByVal end_point As Point) As SizeF
        Dim offset As SizeF = New SizeF(0, 0)

        Dim sum As Double = (end_point.X - start_point.X) * (end_point.X - start_point.X) + (end_point.Y - start_point.Y) * (end_point.Y - start_point.Y)
        sum = Math.Sqrt(sum)

        offset.Width = CSng((end_point.X - start_point.X) / sum)
        offset.Height = CSng((end_point.Y - start_point.Y) / sum)

        Return offset
    End Function

    ''' <summary>
    ''' calculate distance from point to line.
    ''' </summary>
    ''' <paramname="start_point">The start point of line.</param>
    ''' <paramname="end_point">The end point of line.</param>
    ''' <paramname="m_pt">The point out of line.</param>
    Public Function CalcDistFromPointToLine(ByVal start_point As Point, ByVal end_point As Point, ByVal m_pt As Point) As Double
        Dim dist As Double = 0
        Dim a = end_point.Y - start_point.Y
        Dim b = start_point.X - end_point.X
        Dim c = end_point.X * start_point.Y - end_point.Y * start_point.X

        If a <> 0 OrElse b <> 0 Then
            dist = Math.Abs((a * m_pt.X + b * m_pt.Y + c) / Math.Sqrt(a * a + b * b))
        End If

        Return dist
    End Function


    ''' <summary>
    ''' Get normal vector to line whose length is fixed.
    ''' </summary>
    ''' <paramname="start_point">The start point of line.</param>
    ''' <paramname="end_point">The end point of line.</param>
    ''' <paramname="m_pt">The Point of mouse cursor.</param>
    ''' <paramname="length">The length of normal.</param>
    ''' <paramname="flag">The flag for decide to change direction.</param>
    Public Function GetNormalToLineFixedLen(ByVal start_point As Point, ByVal end_point As Point, ByVal m_pt As Point, ByVal length As Integer, ByVal flag As Boolean) As Size
        Dim offset As Size = New Size(0, 0)

        Dim a = end_point.Y - start_point.Y
        Dim b = start_point.X - end_point.X
        Dim c = end_point.X * start_point.Y - end_point.Y * start_point.X

        If a <> 0 OrElse b <> 0 Then
            offset.Width = Convert.ToInt32(a / Math.Sqrt(a * a + b * b) * length)
            offset.Height = Convert.ToInt32(b / Math.Sqrt(a * a + b * b) * length)

            Dim val = a * m_pt.X + b * m_pt.Y + c
            If val > 0 AndAlso flag Then
                offset.Width *= -1
                offset.Height *= -1
            End If
        End If

        Return offset
    End Function


    ''' <summary>
    ''' Get arrow points according to target point m_pt.
    ''' </summary>
    ''' <paramname="start_point">The start point of original line.</param>
    ''' <paramname="end_point">The end point of orginal line.</param>
    ''' <paramname="end_nor">The end point of normal line.</param>
    ''' <paramname="m_pt">The target point of normal line.</param>
    ''' <paramname="length">The distance between arrow point and target point.</param>
    Public Function GetArrowPoints(ByVal start_point As Point, ByVal end_point As Point, ByVal end_nor As Point, ByVal m_pt As Point, ByVal length As Integer) As Point()
        Dim arr_points = New Point(1) {}
        arr_points(0) = m_pt
        arr_points(1) = m_pt

        Dim v_offset = GetNormalToLineFixedLen(start_point, end_nor, m_pt, length, False)
        Dim h_offset As Size = New Size()
        Dim a = end_nor.Y - start_point.Y
        Dim b = start_point.X - end_nor.X

        If a <> 0 OrElse b <> 0 Then
            h_offset.Width = Convert.ToInt32(-1 * b / Math.Sqrt(a * a + b * b) * length / 3)
            h_offset.Height = Convert.ToInt32(a / Math.Sqrt(a * a + b * b) * length / 3)

            Dim a2 = end_point.Y - start_point.Y
            Dim b2 = start_point.X - end_point.X
            Dim c = end_point.X * start_point.Y - end_point.Y * start_point.X

            Dim val = a2 * end_nor.X + b2 * end_nor.Y + c
            If val > 0 Then
                v_offset.Width *= -1
                v_offset.Height *= -1
            End If

            arr_points(0).X = m_pt.X + v_offset.Width + h_offset.Width
            arr_points(0).Y = m_pt.Y + v_offset.Height + h_offset.Height
            arr_points(1).X = m_pt.X + v_offset.Width - h_offset.Width
            arr_points(1).Y = m_pt.Y + v_offset.Height - h_offset.Height
        End If

        Return arr_points

    End Function

    ''' <summary>
    ''' Get arrow points according to target point m_pt.
    ''' </summary>
    ''' <paramname="start_point">The start point of original line.</param>
    ''' <paramname="end_nor">The end point of normal line.</param>
    ''' <paramname="m_pt">The target point of normal line.</param>
    ''' <paramname="length">The distance between arrow point and target point.</param>
    Public Function GetArrowPoints2(ByVal start_point As Point, ByVal end_nor As Point, ByVal m_pt As Point, ByVal length As Integer) As Point()
        Dim arr_points = New Point(1) {}
        arr_points(0) = m_pt
        arr_points(1) = m_pt

        Dim v_offset = GetNormalToLineFixedLen(start_point, end_nor, m_pt, length, False)
        Dim h_offset As Size = New Size()
        Dim a = end_nor.Y - start_point.Y
        Dim b = start_point.X - end_nor.X

        If a <> 0 OrElse b <> 0 Then
            h_offset.Width = Convert.ToInt32(-1 * b / Math.Sqrt(a * a + b * b) * length / 3)
            h_offset.Height = Convert.ToInt32(a / Math.Sqrt(a * a + b * b) * length / 3)

            arr_points(0).X = m_pt.X + v_offset.Width + h_offset.Width
            arr_points(0).Y = m_pt.Y + v_offset.Height + h_offset.Height
            arr_points(1).X = m_pt.X + v_offset.Width - h_offset.Width
            arr_points(1).Y = m_pt.Y + v_offset.Height - h_offset.Height
        End If

        Return arr_points

    End Function

    ''' <summary>
    ''' Get arrow points from a line.
    ''' </summary>
    ''' <paramname="start_point">The start point of line.</param>
    ''' <paramname="end_point">The end point of line.</param>
    ''' <paramname="length">The distance between arrow point and target point.</param>
    Public Function GetArrowPoints3(ByVal start_point As Point, ByVal end_point As Point, ByVal length As Integer) As Point()
        Dim arr_points = New Point(1) {}
        arr_points(0) = start_point
        arr_points(1) = start_point

        Dim v_offset As Size = New Size(end_point.X - start_point.X, end_point.Y - start_point.Y)
        Dim v_length = Math.Sqrt(v_offset.Width * v_offset.Width + v_offset.Height * v_offset.Height)
        If v_length <> 0 Then
            v_offset.Width = Convert.ToInt32(v_offset.Width * length / v_length)
            v_offset.Height = Convert.ToInt32(v_offset.Height * length / v_length)

            Dim h_offset As Size = New Size()
            Dim a = end_point.Y - start_point.Y
            Dim b = start_point.X - end_point.X

            If a <> 0 OrElse b <> 0 Then
                h_offset.Width = Convert.ToInt32(a / Math.Sqrt(a * a + b * b) * length / 3)
                h_offset.Height = Convert.ToInt32(b / Math.Sqrt(a * a + b * b) * length / 3)

                arr_points(0).X = start_point.X + v_offset.Width + h_offset.Width
                arr_points(0).Y = start_point.Y + v_offset.Height + h_offset.Height
                arr_points(1).X = start_point.X + v_offset.Width - h_offset.Width
                arr_points(1).Y = start_point.Y + v_offset.Height - h_offset.Height
            End If
        End If
        Return arr_points

    End Function

    ''' <summary>
    ''' check whether the angle is clockwise or not non-clockwise.
    ''' </summary>
    ''' <paramname="start_point">The start point of angle.</param>
    ''' <paramname="middle_point">The middle point of angle.</param>
    ''' <paramname="end_point">The end point of angle.</param>

    Public Function CheckAngleDirection(ByVal start_point As Point, ByVal middle_point As Point, ByVal end_point As Point) As Boolean
        Dim a = middle_point.Y - start_point.Y
        Dim b = start_point.X - middle_point.X
        Dim c = middle_point.X * start_point.Y - middle_point.Y * start_point.X

        Dim val = a * end_point.X + b * end_point.Y + c
        If val > 0 Then
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' check whether the angle is clockwise or not non-clockwise.
    ''' </summary>
    ''' <paramname="start_point">The start point of line.</param>
    ''' <paramname="middle_point">The point you are going to check.</param>
    ''' <paramname="end_point">The end point of line.</param>
    Public Function CheckThreePointsInLine(ByVal start_point As Point, ByVal middle_point As Point, ByVal end_point As Point) As Boolean
        Dim a = end_point.Y - start_point.Y
        Dim b = start_point.X - end_point.X
        Dim c = end_point.X * start_point.Y - end_point.Y * start_point.X

        If a <> 0 OrElse b <> 0 Then
            Dim dist = Math.Abs((a * middle_point.X + b * middle_point.Y + c) / Math.Sqrt(a * a + b * b))
            If dist > 3.0 Then Return True

        End If
        Return False
    End Function

    ''' <summary>
    ''' check whether target point is above on the line or not.
    ''' </summary>
    ''' <paramname="start_point">The start point of line.</param>
    ''' <paramname="middle_point">The point you are going to check.</param>
    ''' <paramname="end_point">The end point of line.</param>
    Public Function CheckPointOnLine(ByVal start_point As Point, ByVal middle_point As Point, ByVal end_point As Point) As Integer
        Dim a = middle_point.Y - start_point.Y
        Dim b = start_point.X - middle_point.X
        Dim c = middle_point.X * start_point.Y - middle_point.Y * start_point.X

        If a <> 0 OrElse b <> 0 Then
            Dim dist = a * end_point.X + b * end_point.Y + c
            If dist > 0 Then
                Return 1
            ElseIf dist = 0 Then
                Return 0
            Else
                Return -1
            End If
        End If
        Return 10
    End Function

    ''' <summary>
    ''' calculate angle between two lines.
    ''' </summary>
    ''' <paramname="start_point">The start point of first line.</param>
    ''' <paramname="middle_point">The point that overlapped.</param>
    ''' <paramname="end_point">The start point of second line.</param>
    Public Function CalcAngleBetweenTwoLines(ByVal start_point As Point, ByVal middle_point As Point, ByVal end_point As Point) As Double
        Dim dx21 As Double = start_point.X - middle_point.X
        Dim dy21 As Double = start_point.Y - middle_point.Y
        Dim dx31 As Double = end_point.X - middle_point.X
        Dim dy31 As Double = end_point.Y - middle_point.Y
        Dim m12 = Math.Sqrt(dx21 * dx21 + dy21 * dy21)
        Dim m13 = Math.Sqrt(dx31 * dx31 + dy31 * dy31)
        Dim val = (dx21 * dx31 + dy21 * dy31) / (m12 * m13)
        If val < -1 Then
            val = -1
        ElseIf val > 1 Then
            val = 1
        End If
        If Double.IsNaN(val) Then
            Return 0
        End If
        Dim theta = Math.Acos(val)
        Return theta
    End Function

    'correct code
    ''' <summary>
    ''' check whether the target point is in middle of two lines or not.
    ''' </summary>
    ''' <paramname="target_point">The target point for checking.</param>
    ''' <paramname="start_point1">The start point of first line.</param>
    ''' <paramname="end_point1">The end point of first line.</param>
    ''' <paramname="start_point2">The start point of second line.</param>
    ''' <paramname="end_point2">The end point of second line.</param>
    Public Function CheckPointPos(ByVal target_point As Point, ByVal start_point1 As Point, ByVal end_point1 As Point, ByVal start_point2 As Point, ByVal end_point2 As Point) As Boolean
        Dim a1 = end_point1.Y - start_point1.Y
        Dim b1 = start_point1.X - end_point1.X
        Dim c1 = end_point1.X * start_point1.Y - end_point1.Y * start_point1.X

        Dim a2 = end_point2.Y - start_point2.Y
        Dim b2 = start_point2.X - end_point2.X
        Dim c2 = end_point2.X * start_point2.Y - end_point2.Y * start_point2.X

        Dim val1 = a1 * target_point.X + b1 * target_point.Y + c1
        Dim val2 = a2 * target_point.X + b2 * target_point.Y + c2

        If val1 > 0 AndAlso val2 < 0 OrElse val1 < 0 AndAlso val2 > 0 Then Return False

        Return True
    End Function

    'correct code
    ''' <summary>
    ''' calculate intersection of two lines.
    ''' </summary>
    ''' <paramname="start_point1">The start point of first line.</param>
    ''' <paramname="end_point1">The end point of first line.</param>
    ''' <paramname="start_point2">The start point of second line.</param>
    ''' <paramname="end_point2">The end point of second line.</param>
    Public Function CalcInterSection(ByVal start_point1 As Point, ByVal end_point1 As Point, ByVal start_point2 As Point, ByVal end_point2 As Point) As Point
        Dim inter_pt As Point = New Point(10000, 10000)
        Dim a1 = end_point1.Y - start_point1.Y
        Dim b1 = start_point1.X - end_point1.X
        Dim c1 = end_point1.X * start_point1.Y - end_point1.Y * start_point1.X

        Dim a2 = end_point2.Y - start_point2.Y
        Dim b2 = start_point2.X - end_point2.X
        Dim c2 = end_point2.X * start_point2.Y - end_point2.Y * start_point2.X

        If b1 * a2 - b2 * a1 <> 0 Then
            inter_pt.Y = (c2 * a1 - c1 * a2) / (b1 * a2 - b2 * a1)
            If a1 <> 0 Then
                inter_pt.X = (0 - b1 * inter_pt.Y - c1) / a1
            Else
                inter_pt.X = (0 - b2 * inter_pt.Y - c2) / a2
            End If
        End If
        Return inter_pt
    End Function

    ''' <summary>
    ''' calculate the position of target point in a circle.
    ''' </summary>
    ''' <paramname="centerpt">The center point of circle.</param>
    ''' <paramname="radius">The radius of the circle.</param>
    ''' <paramname="angle">Angle in degrees measured clockwise from the x-axis to the target point.</param>
    Public Function CalcPositionInCircle(ByVal centerpt As Point, ByVal radius As Integer, ByVal angle As Integer) As Point
        Dim target_point As Point = New Point()
        Dim ang_radian = angle / 360 * Math.PI * 2
        Dim offset_x = Convert.ToInt32(radius * Math.Cos(ang_radian))
        Dim offset_y = Convert.ToInt32(radius * Math.Sin(ang_radian))
        target_point.X = centerpt.X + offset_x
        target_point.Y = centerpt.Y + offset_y
        Return target_point
    End Function

    ''' <summary>
    ''' get the position targetpt in transformed coordinates.
    ''' </summary>
    ''' <paramname="targetpt">The point in original coordinates.</param>
    ''' <paramname="angle">Angle in degrees for rotation.</param>
    Public Function GetRotationTransform(ByVal targetpt As Point, ByVal angle As Integer) As Point
        Dim transpt As Point = New Point()
        Dim ang_radian = angle / 360 * Math.PI * 2
        transpt.X = Convert.ToInt32(targetpt.X * Math.Cos(ang_radian) + targetpt.Y * Math.Sin(ang_radian))
        transpt.Y = Convert.ToInt32(-1 * targetpt.X * Math.Sin(ang_radian) + targetpt.Y * Math.Cos(ang_radian))

        Return transpt
    End Function

    ''' <summary>
    ''' correct the position of string while mouse cursor is changing.
    ''' </summary>
    ''' <paramname="m_pt">The point of mouse cursor.</param>
    ''' <paramname="start_point">The start point of line.</param>
    ''' <paramname="end_point">The end point of line.</param>
    ''' <paramname="flag">The flag specify the direction of angle.</param>
    Public Function CorrectDisplayPosition(ByVal m_pt As Point, ByVal start_point As Point, ByVal end_point As Point, ByVal flag As Boolean) As Point
        Dim res_pt = m_pt
        Dim a = end_point.Y - start_point.Y
        Dim b = start_point.X - end_point.X
        Dim c = end_point.X * start_point.Y - end_point.Y * start_point.X

        Dim val = a * m_pt.X + b * m_pt.Y + c
        If flag AndAlso val < 0 Then
            If b <> 0 Then
                res_pt.Y = Convert.ToInt32(0 - c - a * m_pt.X) / b
            Else
                res_pt.X = Convert.ToInt32(0 - c - b * m_pt.Y) / a
            End If
        ElseIf Not flag AndAlso val > 0 Then
            If b <> 0 Then
                res_pt.Y = Convert.ToInt32(0 - c - a * m_pt.X) / b
            Else
                res_pt.X = Convert.ToInt32(0 - c - b * m_pt.Y) / a
            End If
        End If
        Return res_pt
    End Function

    ''' <summary>
    ''' calculate the distance between two points.
    ''' </summary>
    ''' <paramname="start_point">The start point of line.</param>
    ''' <paramname="end_point">The end point of line.</param>
    Public Function CalcDistBetweenPoints(ByVal start_point As Point, ByVal end_point As Point) As Double
        Dim size As Size = New Size(end_point.X - start_point.X, end_point.Y - start_point.Y)
        Dim dist As Double = size.Width * size.Width + size.Height * size.Height
        dist = Math.Sqrt(dist)
        Return dist
    End Function

    ''' <summary>
    ''' get the point which is the nearest from the target point.
    ''' </summary>
    ''' <paramname="target_point">The target point.</param>
    ''' <paramname="pt_array">The list of points from which you are going to find the nearest.</param>
    Public Function GetShortestPath(ByVal target_point As Point, ByVal pt_array As Point()) As Point
        Dim min_dist As Double = 1000000000
        Dim near_pt = pt_array(0)
        For Each pt In pt_array
            Dim dist = CalcDistBetweenPoints(target_point, pt)
            If dist < min_dist Then
                min_dist = dist
                near_pt = pt
            End If
        Next
        Return near_pt
    End Function

    ''' <summary>
    ''' get double type value which has digit decimal numbers.
    ''' </summary>
    ''' <paramname="scr">The input of function.</param>
    ''' <paramname="digit">The numbers of decimals.</param>
    ''' <paramname="CF">The factor of multiply.</param>
    Public Function GetDecimalNumber(ByVal scr As Double, ByVal digit As Integer, ByVal CF As Double) As Double
        Dim dst = scr * CF
        Dim test = dst * Math.Pow(10, digit)
        If test - CInt(test) < 0.1 And digit <> 0 Then
            Dim eplison = 1.0 / Math.Pow(10, digit)
            dst += eplison
        End If
        Return Math.Round(dst, digit)
    End Function

    ''' <summary>
    ''' get minimum value among elements in a set.
    ''' </summary>
    ''' <paramname="elem">The elements of set.</param>

    Public Function GetMinimumInSet(ByVal elem As Single()) As Single
        Dim minimun = 1.0E+8F
        For i = 0 To elem.Length - 1
            minimun = Math.Min(minimun, elem(i))
        Next
        Return minimun
    End Function

    ''' <summary>
    ''' get maximum value among elements in a set.
    ''' </summary>
    ''' <paramname="elem">The elements of set.</param>

    Public Function GetMaximumInSet(ByVal elem As Single()) As Single
        Dim maximum = 0.0F
        For i = 0 To elem.Length - 1
            maximum = Math.Max(maximum, elem(i))
        Next
        Return maximum
    End Function

    ''' <summary>
    ''' check the point is in the rect
    ''' </summary>
    ''' <paramname="pt">The position of point.</param>
    ''' <paramname="rect">The rect.</param>

    Public Function PointInRect(ByVal pt As Point, ByVal rect As Rectangle) As Boolean
        If pt.X > rect.X AndAlso pt.X < rect.X + rect.Width AndAlso pt.Y > rect.Y AndAlso pt.Y < rect.Y + rect.Height Then Return True

        Return False
    End Function

    ''' <summary>
    ''' swap start point and end point
    ''' </summary>
    ''' <paramname="obj">The object for considering.</param>
    Public Function swap_AC(ByVal obj As MeasureObject) As MeasureObject
        Dim swap = obj
        Dim A = obj.start_point
        Dim B = obj.middle_point
        Dim C = obj.end_point
        Dim T = New PointF()
        T = A
        A = C
        C = T
        swap.start_point = A
        swap.middle_point = B
        swap.end_point = C
        Return swap
    End Function

    ''' <summary>
    ''' sorting points in a arc
    ''' </summary>
    ''' <paramname="obj">The object for considering.</param>
    ''' <paramname="mid_pt">The point locates right of the center.</param>
    Public Function sorting_Points(ByVal obj As MeasureObject, ByVal mid_pt As PointF) As MeasureObject
        Dim sort = obj
        Dim A = obj.start_point
        Dim B = obj.middle_point
        Dim C = obj.end_point
        Dim M = mid_pt
        If A.Y <= M.Y And C.Y <= M.Y And B.Y <= M.Y Then
            If B.X <= A.X And B.X <= C.X And C.X <= A.X Then
                sort = swap_AC(obj)
            ElseIf B.X > A.X And B.X > C.X And C.X <= A.X Then
                sort = swap_AC(obj)
            ElseIf B.X > A.X And B.X <= C.X Then
                sort = swap_AC(obj)
            End If
        ElseIf A.Y <= M.Y And C.Y <= M.Y And B.Y > M.Y Then
            If A.X > C.X Then
                sort = swap_AC(obj)
            End If
        ElseIf A.Y <= M.Y And B.Y <= M.Y And C.Y > M.Y Then
            If A.X <= B.X Then
                sort = swap_AC(obj)
            End If
        ElseIf A.Y > M.Y And B.Y <= M.Y And C.Y <= M.Y Then
            If B.X <= C.X Then
                sort = swap_AC(obj)
            End If
        ElseIf A.Y > M.Y And B.Y <= M.Y And C.Y > M.Y Then
            If A.X <= C.X Then
                sort = swap_AC(obj)
            End If
        ElseIf A.Y > M.Y And B.Y > M.Y And C.Y > M.Y Then
            If B.X <= A.X And B.X <= C.X And C.X <= A.X Then
                sort = swap_AC(obj)
            ElseIf B.X > A.X And B.X > C.X And C.X > A.X Then
                sort = swap_AC(obj)
            ElseIf B.X <= A.X And B.X > C.X Then
                sort = swap_AC(obj)
            End If
        ElseIf A.Y > M.Y And B.Y > M.Y And C.Y <= M.Y Then
            If A.X > B.X Then
                sort = swap_AC(obj)
            End If
        ElseIf A.Y <= M.Y And B.Y > M.Y And C.Y > M.Y Then
            If B.X > C.X Then

                sort = swap_AC(obj)
            End If
        End If
        Return sort

    End Function

    Public Sub CopyIntegerArray(ByRef Dst As Integer(), Src As Integer(), Len As Integer)
        For i = 0 To Len - 1
            Dst(i) = Src(i)
        Next
    End Sub



End Module

