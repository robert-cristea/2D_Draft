Imports System.IO
Imports System.Runtime.CompilerServices
Imports AForge.Video.DirectShow
Imports ClosedXML.Excel
Imports DocumentFormat.OpenXml.Drawing.Wordprocessing
Imports Emgu.CV
Imports Emgu.CV.CvEnum
Imports GeometRi
Imports Size = System.Drawing.Size

''' <summary>
''' This class contains all the functions that been  attched to Controls.
''' </summary>
Public Module ControlsMethods
#Region "PictureBox Methods"
    ''' <summary>
    ''' Update element of list
    ''' </summary>
    ''' <paramname="Src">The list for update.</param>
    ''' <paramname="NewMat">The element Mat which will be used for update.</param>
    ''' <paramname="index">The index of list to update.</param>
    Public Sub DisposeElemOfList(ByRef Src As List(Of Mat), NewMat As Mat, index As Integer)
        Dim oldMat = Src(index)
        Src.RemoveAt(index)
        If oldMat IsNot Nothing Then
            oldMat.Dispose()
        End If

        Src.Insert(index, NewMat)
    End Sub

    ''' <summary>
    ''' calculate ratio of panel size by image size
    ''' </summary>
    ''' <paramname="panel">The panel which involves picturebox.</param>
    ''' <paramname="Ori">The image in picturebox.</param>
    Public Function CalcIntialRatio(panel As Panel, Ori As Mat) As Single
        Dim image_w = Ori.Cols
        Dim image_h = Ori.Rows
        Dim picturebox_w = panel.Width
        Dim picturebox_h = panel.Height
        Dim ratio As Single

        If image_w / image_h < picturebox_w / picturebox_h Then
            ratio = CSng(picturebox_h) / image_h
        Else
            ratio = CSng(picturebox_w) / image_w
        End If
        Return ratio
    End Function

    ''' <summary>
    ''' Zoom image by zoom_factor.
    ''' </summary>
    ''' <paramname="zoom_factor">The factor for zooming in or zooming out.</param>
    ''' <paramname="Scr">The list of original images.</param>

    Public Function ZoomImage(ByVal zoom_factor As Double, ByVal Scr As Mat) As Mat
        Dim ori_img = Scr.Clone()
        Dim s As Size = New Size(Convert.ToInt32(ori_img.Width * zoom_factor), Convert.ToInt32(ori_img.Height * zoom_factor))
        Dim dst_img As Mat = New Mat()
        CvInvoke.Resize(ori_img, dst_img, s)
        ori_img.Dispose()
        Return dst_img
    End Function

    ''' <summary>
    ''' Zoom image into Dst_w * Dst_h.
    ''' </summary>
    ''' <paramname="Src">The source image.</param>
    ''' <paramname="Dst_w">The width of zoomed image.</param>
    ''' <paramname="Dst_h">The height of zoomed image.</param>
    Public Function ZoomImage2(Src As Mat, Dst_w As Integer, Dst_h As Integer) As Mat
        Dim Dst As Mat = New Mat()
        Dim s As Size = New Size(Dst_w, Dst_h)
        CvInvoke.Resize(Src, Dst, s)
        Return Dst
    End Function


    ''' <summary>
    ''' put image in the center of picturebox control.
    ''' </summary>
    ''' <paramname="pictureBox">The pictureBox control which you want to put in the center.</param>
    ''' <paramname="panel">The panel which includes a picturebox control.</param>
    <Extension()>
    Public Function CenteringImage(ByVal pictureBox As PictureBox, ByVal panel As Panel) As Point
        Dim left_top As Point = New Point((panel.Width - pictureBox.Width) / 2, (panel.Height - pictureBox.Height) / 2)
        Dim scroll_pos As Point = New Point With {
.X = (pictureBox.Width - panel.Width) / 2,
.Y = (pictureBox.Height - panel.Height) / 2
}

        pictureBox.Location = left_top

        panel.AutoScrollPosition = scroll_pos
        Return left_top
    End Function

    ''' <summary>
    ''' check if there is any item in the pos where mouse clicked and return the number of object
    ''' </summary>
    ''' <paramname="m_pt">The point of mouse cursor.</param>
    ''' <paramname="obj_list">The list of objects.</param>
    ''' <paramname="width">The width of picturebox.</param>
    ''' <paramname="height">The height of picturebox.</param>
    ''' <paramname="CF">The factor of measuring scale.</param>
    Public Function CheckItemInPos(ByVal m_pt As PointF, ByVal obj_list As List(Of MeasureObject), ByVal width As Integer, ByVal height As Integer, ByVal CF As Double) As Integer
        For Each item In obj_list
            Dim x_low = item.leftTop.X
            Dim x_high = item.rightBottom.X
            Dim y_low = item.leftTop.Y
            Dim y_high = item.rightBottom.Y

            If item.measuringType = MeasureType.lineHorizontal Then
                y_low -= 0.01F
                y_high += 0.01F
            ElseIf item.measuringType = MeasureType.lineVertical Then
                x_low -= 0.01F
                x_high += 0.01F
            ElseIf item.measuringType = MeasureType.measureScale Then
                x_low = item.startPoint.X
                y_low = item.startPoint.Y

                If Equals(item.scaleObject.style, "horizontal") Then
                    x_high = x_low + CSng(item.length / CF)
                    y_high = y_low + 0.01F
                    y_low -= 0.01F
                Else
                    y_high = y_low + CSng(item.length / CF)
                    x_high = x_low + 0.01F
                    x_low -= 0.01F
                End If
            ElseIf item.measuringType = MeasureType.arcFixed Then
                x_low = item.startPoint.X - item.scaleObject.length / (CF * width)
                x_high = item.startPoint.X + item.scaleObject.length / (CF * width)
                y_low = item.startPoint.Y - item.scaleObject.length / (CF * width)
                y_high = item.startPoint.Y + item.scaleObject.length / (CF * width)
            End If

            If m_pt.X > x_low AndAlso m_pt.X < x_high AndAlso m_pt.Y > y_low AndAlso m_pt.Y < y_high Then Return item.objNum
        Next
        Return -1
    End Function

    ''' <summary>
    ''' check if there is any annotation in the pos where mouse clicked and return the number of object
    ''' </summary>
    ''' <paramname="m_pt">The point of mouse cursor.</param>
    ''' <paramname="obj_list">The list of objects.</param>
    ''' <paramname="width">The width of picturebox.</param>
    ''' <paramname="height">The height of picturebox.</param>
    Public Function CheckAnnotation(ByVal m_pt As PointF, ByVal obj_list As List(Of MeasureObject), ByVal width As Integer, ByVal height As Integer) As Integer

        For Each item In obj_list
            If item.measuringType = MeasureType.annotation Then
                Dim limit_x = item.leftTop.X + item.annoObject.size.Width / CSng(width)
                Dim limit_y = item.leftTop.Y + item.annoObject.size.Height / CSng(height)

                If m_pt.X > item.leftTop.X AndAlso m_pt.X < limit_x AndAlso m_pt.Y > item.leftTop.Y AndAlso m_pt.Y < limit_y Then Return item.objNum
            End If

        Next
        Return -1
    End Function


    ''' <summary>
    ''' enable textbox when you clicked on annotation
    ''' </summary>
    ''' <paramname="textbox">The textbox you are goint to type on.</param>
    ''' <paramname="obj_selected">the object whose annotation is selected.</param>
    ''' <paramname="Width">the width of picturebox.</param>
    ''' <paramname="Height">the height of picturebox.</param>
    ''' <paramname="left_top">the left top cornor of picturebox.</param>
    ''' <paramname="scroll_pos">the position of scrollbar.</param>
    <Extension()>
    Public Sub EnableTextBox(ByVal textbox As TextBox, ByVal obj_selected As MeasureObject, ByVal width As Integer, ByVal height As Integer, ByVal left_top As Point, ByVal scroll_pos As Point)
        textbox.Text = obj_selected.annotation
        Dim pos_image As Point = New Point(obj_selected.drawPoint.X * width, obj_selected.drawPoint.Y * height)
        Dim pos_panel As Point = New Point(pos_image.X + left_top.X, pos_image.Y + left_top.Y)
        textbox.Location = pos_image
        Dim rt_size = obj_selected.annoObject.size
        textbox.Visible = True
        textbox.Size = rt_size
    End Sub

    ''' <summary>
    ''' Hightlight selected item
    ''' </summary>
    ''' <paramname="pictureBox">the picturebox control.</param>
    ''' <paramname="item">the object which is selected.</param>
    ''' <paramname="Width">the width of picturebox.</param>
    ''' <paramname="Height">the height of picturebox.</param>

    <Extension()>
    Public Sub HightLightItem(ByVal pictureBox As PictureBox, ByVal item As MeasureObject, ByVal width As Integer, ByVal height As Integer, ByVal CF As Double)
        Dim graph As Graphics = pictureBox.CreateGraphics()
        Dim graphPen As Pen = New Pen(Color.Black, 1)
        graphPen.DashStyle = Drawing2D.DashStyle.Dot

        Dim x_low = item.leftTop.X
        Dim x_high = item.rightBottom.X
        Dim y_low = item.leftTop.Y
        Dim y_high = item.rightBottom.Y

        If item.measuringType = MeasureType.lineHorizontal Then
            y_low -= 0.02F
            y_high += 0.02F
        ElseIf item.measuringType = MeasureType.lineVertical Then
            x_low -= 0.02F
            x_high += 0.02F
        ElseIf item.measuringType = MeasureType.measureScale Then
            x_low = item.startPoint.X
            y_low = item.startPoint.Y

            If Equals(item.scaleObject.style, "horizontal") Then
                y_high = y_low + 0.02F
                y_low -= 0.02F
            Else
                x_high = x_low + 0.02F
                x_low -= 0.02F
            End If
        ElseIf item.measuringType = MeasureType.arcFixed Then
            x_low = item.startPoint.X - item.scaleObject.length / (CF * width)
            x_high = item.startPoint.X + item.scaleObject.length / (CF * width)
            y_low = item.startPoint.Y - item.scaleObject.length / (CF * width)
            y_high = item.startPoint.Y + item.scaleObject.length / (CF * width)
        End If

        Dim x As Integer = x_low * width
        Dim y As Integer = y_low * height
        Dim rt_width = CInt(x_high * width) - x
        Dim rt_height = CInt(y_high * height) - y

        graph.DrawRectangle(graphPen, x, y, rt_width, rt_height)
        graphPen.Dispose()
        graph.Dispose()
    End Sub


    ''' <summary>
    ''' change the location of textbox 
    ''' </summary>
    ''' <paramname="textbox">The textbox you are goint to type on.</param>
    ''' <paramname="start_pt">the left top corner of annotaion in picturebox.</param>
    ''' <paramname="cur_left_top">current location of left top corner of picturebox.</param>
    ''' <paramname="cur_scroll">current location of scrollbar.</param>
    <Extension()>
    Public Sub UpdateLocation(ByVal textbox As TextBox, ByVal start_pt As Point, ByVal cur_left_top As Point, ByVal cur_scroll As Point)
        'Point cur_locate = new Point(start_pt.X + cur_left_top.X - cur_scroll.X, start_pt.Y + cur_left_top.Y - cur_scroll.Y);
        Dim cur_locate As Point = New Point(start_pt.X + cur_left_top.X, start_pt.Y + cur_left_top.Y)
        textbox.Location = start_pt
    End Sub

    ''' <summary>
    ''' disable textbox when foucs leave and update annotation with the text of textbox
    ''' </summary>
    ''' <paramname="textbox">The textbox to disable.</param>
    ''' <paramname="obj_list">the list of objects.</param>
    ''' <paramname="obj_num">the number of object which you are goint to update.</param>
    <Extension()>
    Public Sub DisableTextBox(ByVal textbox As TextBox, ByVal obj_list As List(Of MeasureObject), ByVal obj_num As Integer,
                                  ByVal width As Integer, ByVal height As Integer)
        textbox.Visible = False
        Dim i = 0
        Dim flag = False
        For Each item In obj_list
            If item.objNum = obj_num Then
                flag = True
                Exit For
            End If
            i += 1
        Next
        If flag Then
            Dim temp_obj = obj_list(i)
            temp_obj.annotation = textbox.Text
            temp_obj.annoObject.size = textbox.Size
            temp_obj.rightBottom.X = temp_obj.leftTop.X + textbox.Size.Width / CSng(width)
            temp_obj.rightBottom.Y = temp_obj.leftTop.Y + textbox.Size.Height / CSng(height)
            obj_list.RemoveAt(i)
            obj_list.Insert(i, temp_obj)
        End If
    End Sub

    ''' <summary>
    ''' remove last item from list
    ''' </summary>
    ''' <paramname="obj_list">the list of objects.</param>
    Public Function RemoveObjFromList(ByVal obj_list As List(Of MeasureObject)) As Boolean
        Dim cnt As Integer = obj_list.Count()
        If cnt > 0 Then
            obj_list.RemoveAt(cnt - 1)
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' move selected object in distance of dx, dy.
    ''' </summary>
    ''' <paramname="obj_list">The list of objects.</param>
    ''' <paramname="sel_index">The number of selected object.</param>
    ''' <paramname="dx">The move distance in X axis.</param>
    ''' <paramname="dy">The move distance in Y axis.</param>
    Public Sub MoveObject(ByVal obj_list As List(Of MeasureObject), ByVal sel_index As Integer, ByVal dx As Single, ByVal dy As Single)
        Dim obj_selected = obj_list.ElementAt(sel_index)
        If obj_selected.measuringType <> MeasureType.annotation Then
            obj_selected.startPoint.X += dx
            obj_selected.startPoint.Y += dy
        End If

        obj_selected.middlePoint.X += dx
        obj_selected.middlePoint.Y += dy
        obj_selected.endPoint.X += dx
        obj_selected.endPoint.Y += dy
        obj_selected.drawPoint.X += dx
        obj_selected.drawPoint.Y += dy
        obj_selected.lastPoint.X += dx
        obj_selected.lastPoint.Y += dy
        obj_selected.commonPoint.X += dx
        obj_selected.commonPoint.Y += dy
        obj_selected.leftTop.X += dx
        obj_selected.leftTop.Y += dy
        obj_selected.rightBottom.X += dx
        obj_selected.rightBottom.Y += dy

        If obj_selected.measuringType = MeasureType.lineAlign OrElse obj_selected.measuringType = MeasureType.lineHorizontal OrElse obj_selected.measuringType = MeasureType.lineVertical OrElse obj_selected.measuringType = MeasureType.lineParallel OrElse obj_selected.measuringType = MeasureType.ptToLine Then
            obj_selected.lineObject.draw_pt.X += dx
            obj_selected.lineObject.draw_pt.Y += dy
            obj_selected.lineObject.side_drag.X += dx
            obj_selected.lineObject.side_drag.Y += dy
            obj_selected.lineObject.nor_pt1.X += dx
            obj_selected.lineObject.nor_pt1.Y += dy
            obj_selected.lineObject.nor_pt2.X += dx
            obj_selected.lineObject.nor_pt2.Y += dy
            obj_selected.lineObject.nor_pt3.X += dx
            obj_selected.lineObject.nor_pt3.Y += dy
            obj_selected.lineObject.nor_pt4.X += dx
            obj_selected.lineObject.nor_pt4.Y += dy
            obj_selected.lineObject.nor_pt5.X += dx
            obj_selected.lineObject.nor_pt5.Y += dy
            obj_selected.lineObject.nor_pt6.X += dx
            obj_selected.lineObject.nor_pt6.Y += dy
            obj_selected.lineObject.nor_pt7.X += dx
            obj_selected.lineObject.nor_pt7.Y += dy
            obj_selected.lineObject.nor_pt8.X += dx
            obj_selected.lineObject.nor_pt8.Y += dy
        ElseIf obj_selected.measuringType = MeasureType.lineFixed Then
            obj_selected.lineObject.nor_pt1.X += dx
            obj_selected.lineObject.nor_pt1.Y += dy
            obj_selected.lineObject.nor_pt3.X += dx
            obj_selected.lineObject.nor_pt3.Y += dy
            obj_selected.lineObject.nor_pt5.X += dx
            obj_selected.lineObject.nor_pt5.Y += dy
            obj_selected.lineObject.nor_pt6.X += dx
            obj_selected.lineObject.nor_pt6.Y += dy
        ElseIf obj_selected.measuringType = MeasureType.angle OrElse obj_selected.measuringType = MeasureType.angle2Line OrElse obj_selected.measuringType = MeasureType.angleFixed Then
            obj_selected.angleObject.draw_pt.X += dx
            obj_selected.angleObject.draw_pt.Y += dy
            obj_selected.angleObject.side_drag.X += dx
            obj_selected.angleObject.side_drag.Y += dy
            obj_selected.angleObject.start_pt.X += dx
            obj_selected.angleObject.start_pt.Y += dy
            obj_selected.angleObject.end_pt.X += dx
            obj_selected.angleObject.end_pt.Y += dy
            obj_selected.angleObject.nor_pt1.X += dx
            obj_selected.angleObject.nor_pt1.Y += dy
            obj_selected.angleObject.nor_pt2.X += dx
            obj_selected.angleObject.nor_pt2.Y += dy
            obj_selected.angleObject.nor_pt3.X += dx
            obj_selected.angleObject.nor_pt3.Y += dy
            obj_selected.angleObject.nor_pt4.X += dx
            obj_selected.angleObject.nor_pt4.Y += dy
            obj_selected.angleObject.nor_pt5.X += dx
            obj_selected.angleObject.nor_pt5.Y += dy
            obj_selected.angleObject.nor_pt6.X += dx
            obj_selected.angleObject.nor_pt6.Y += dy
        ElseIf obj_selected.measuringType = MeasureType.arc Then
            obj_selected.arcObject.draw_pt.X += dx
            obj_selected.arcObject.draw_pt.Y += dy
            obj_selected.arcObject.side_drag.X += dx
            obj_selected.arcObject.side_drag.Y += dy
            obj_selected.arcObject.arr_pt1.X += dx
            obj_selected.arcObject.arr_pt1.Y += dy
            obj_selected.arcObject.arr_pt2.X += dx
            obj_selected.arcObject.arr_pt2.Y += dy
            obj_selected.arcObject.arr_pt3.X += dx
            obj_selected.arcObject.arr_pt3.Y += dy
            obj_selected.arcObject.arr_pt4.X += dx
            obj_selected.arcObject.arr_pt4.Y += dy
            obj_selected.arcObject.circle_pt.X += dx
            obj_selected.arcObject.circle_pt.Y += dy
            obj_selected.arcObject.center_pt.X += dx
            obj_selected.arcObject.center_pt.Y += dy
        ElseIf obj_selected.measuringType = MeasureType.arcFixed Then
            obj_selected.arcObject.center_pt.X += dx
            obj_selected.arcObject.center_pt.Y += dy
        ElseIf obj_selected.measuringType = MeasureType.annotation Then
            obj_selected.annoObject.line_pt.X += dx
            obj_selected.annoObject.line_pt.Y += dy
        End If
        obj_list(sel_index) = obj_selected
    End Sub

    ''' <summary>
    ''' correct delta for scale when mouse is moving
    ''' </summary>
    ''' <paramname="dx">delta in X.</param>
    ''' <paramname="dy">delta in Y.</param>
    ''' <paramname="direction">specfiy direction.</param>

    Public Sub CorrectDeltaForScale(ByRef dx As Single, ByRef dy As Single, ByVal direction As String)
        If Equals(direction, "horizontal") Then
            dy = 0
        Else
            dx = 0
        End If
    End Sub

    ''' <summary>
    ''' move selected point in distance of dx, dy.
    ''' </summary>
    ''' <paramname="obj_list">The list of objects.</param>
    ''' <paramname="item_index">The number of selected object.</param>
    ''' <paramname="pt_index">The number of selected point.</param>
    ''' <paramname="dx">The move distance in X axis.</param>
    ''' <paramname="dy">The move distance in Y axis.</param>
    Public Sub MovePoint(ByVal obj_list As List(Of MeasureObject), ByVal item_index As Integer, ByVal pt_index As Integer, ByVal dx As Single, ByVal dy As Single)
        Dim obj_selected = obj_list.ElementAt(item_index)

        If obj_selected.measuringType = MeasureType.measureScale Then CorrectDeltaForScale(dx, dy, obj_selected.scaleObject.style)

        Select Case pt_index
            Case 1
                obj_selected.startPoint.X += dx
                obj_selected.startPoint.Y += dy
            Case 2
                obj_selected.middlePoint.X += dx
                obj_selected.middlePoint.Y += dy
            Case 3
                obj_selected.endPoint.X += dx
                obj_selected.endPoint.Y += dy
            Case 4
                obj_selected.lastPoint.X += dx
                obj_selected.lastPoint.Y += dy
        End Select
        obj_list(item_index) = obj_selected
    End Sub


    ''' <summary>
    ''' modify object selected when mouse down and return whether obj_selected is completed or not.
    ''' </summary>
    ''' <paramname="obj_selected">The object which is currently selected.</param>
    ''' <paramname="cur_measure_type">The current measurement type.</param>
    ''' <paramname="m_pt">The point mouse clicked.</param>
    ''' <paramname="width">The width of original image.</param>
    ''' <paramname="height">The height of original image.</param>
    ''' <paramname="line_infor">The information for drawing lines.</param>
    ''' <paramname="font_infor">The information for font and color.</param>
    ''' <paramname="CF">The factor for measuring scale.</param>

    Public Function ModifyObjSelected(ByRef obj_selected As MeasureObject, objList As List(Of MeasureObject), cur_measure_type As Integer, ByVal m_pt As PointF, ByVal width As Integer, ByVal height As Integer, ByVal line_infor As LineStyle, ByVal font_infor As FontInfor, ByVal CF As Double, unit As String) As Boolean
        obj_selected.intialized = True

        Dim item_set_limit = 0
        If cur_measure_type = MeasureType.lineAlign OrElse cur_measure_type = MeasureType.lineHorizontal OrElse cur_measure_type = MeasureType.lineVertical OrElse cur_measure_type = MeasureType.lineFixed OrElse cur_measure_type = MeasureType.angleFixed Then
            item_set_limit = 3
        ElseIf cur_measure_type = MeasureType.angle OrElse cur_measure_type = MeasureType.arc OrElse cur_measure_type = MeasureType.lineParallel OrElse cur_measure_type = MeasureType.ptToLine Then
            item_set_limit = 4
        ElseIf cur_measure_type = MeasureType.annotation OrElse cur_measure_type = MeasureType.pencil OrElse cur_measure_type = MeasureType.arcFixed Then
            item_set_limit = 2
        ElseIf cur_measure_type = MeasureType.angle2Line Then
            item_set_limit = 5
        ElseIf cur_measure_type = MeasureType.measureScale Then
            item_set_limit = 1
        End If

        If obj_selected.itemSet < item_set_limit Then
            If cur_measure_type = MeasureType.lineAlign OrElse cur_measure_type = MeasureType.lineHorizontal OrElse cur_measure_type = MeasureType.lineVertical OrElse cur_measure_type = MeasureType.measureScale OrElse cur_measure_type = MeasureType.pencil OrElse cur_measure_type = MeasureType.lineFixed Then
                If obj_selected.itemSet = 0 Then
                    obj_selected.startPoint = m_pt
                    obj_selected.itemSet += 1
                ElseIf obj_selected.itemSet = 1 Then
                    obj_selected.endPoint = m_pt
                    obj_selected.itemSet += 1

                    obj_selected.leftTop.X = Math.Min(obj_selected.startPoint.X, obj_selected.endPoint.X)
                    obj_selected.leftTop.Y = Math.Min(obj_selected.startPoint.Y, obj_selected.endPoint.Y)
                    obj_selected.rightBottom.X = Math.Max(obj_selected.startPoint.X, obj_selected.endPoint.X)
                    obj_selected.rightBottom.Y = Math.Max(obj_selected.startPoint.Y, obj_selected.endPoint.Y)
                ElseIf obj_selected.itemSet = 2 Then
                    obj_selected.drawPoint = m_pt
                    obj_selected.itemSet += 1
                End If
            ElseIf cur_measure_type = MeasureType.angle OrElse cur_measure_type = MeasureType.arc OrElse cur_measure_type = MeasureType.lineParallel OrElse cur_measure_type = MeasureType.ptToLine OrElse cur_measure_type = MeasureType.angleFixed Then
                If obj_selected.itemSet = 0 Then
                    obj_selected.startPoint = m_pt
                    obj_selected.itemSet += 1
                ElseIf obj_selected.itemSet = 1 Then
                    obj_selected.middlePoint = m_pt
                    obj_selected.itemSet += 1
                ElseIf obj_selected.itemSet = 2 Then
                    If cur_measure_type = MeasureType.angleFixed Then
                        obj_selected.drawPoint = m_pt
                        obj_selected.itemSet += 1
                    Else
                        obj_selected.endPoint = m_pt
                        obj_selected.itemSet += 1
                    End If

                    If cur_measure_type = MeasureType.arc OrElse cur_measure_type = MeasureType.ptToLine OrElse cur_measure_type = MeasureType.lineParallel Then
                        Dim x_set = {obj_selected.startPoint.X, obj_selected.middlePoint.X, obj_selected.endPoint.X}
                        obj_selected.leftTop.X = GetMinimumInSet(x_set)
                        obj_selected.rightBottom.X = GetMaximumInSet(x_set)
                        Dim y_set = {obj_selected.startPoint.Y, obj_selected.middlePoint.Y, obj_selected.endPoint.Y}
                        obj_selected.leftTop.Y = GetMinimumInSet(y_set)
                        obj_selected.rightBottom.Y = GetMaximumInSet(y_set)
                    End If
                ElseIf obj_selected.itemSet = 3 Then
                    obj_selected.drawPoint = m_pt
                    obj_selected.itemSet += 1
                End If
            ElseIf cur_measure_type = MeasureType.arcFixed Then
                If obj_selected.itemSet = 0 Then
                    obj_selected.startPoint = m_pt
                    obj_selected.itemSet += 1
                ElseIf obj_selected.itemSet = 1 Then
                    obj_selected.drawPoint = m_pt
                    obj_selected.itemSet += 1
                End If

            ElseIf cur_measure_type = MeasureType.annotation Then
                If obj_selected.itemSet = 0 Then
                    obj_selected.startPoint = m_pt
                    obj_selected.itemSet += 1
                ElseIf obj_selected.itemSet = 1 Then
                    obj_selected.drawPoint = m_pt
                    obj_selected.itemSet += 1
                End If

            ElseIf cur_measure_type = MeasureType.angle2Line Then
                If obj_selected.itemSet = 0 Then
                    obj_selected.startPoint = m_pt
                    obj_selected.itemSet += 1
                ElseIf obj_selected.itemSet = 1 Then
                    obj_selected.middlePoint = m_pt
                    obj_selected.itemSet += 1
                ElseIf obj_selected.itemSet = 2 Then
                    obj_selected.endPoint = m_pt
                    obj_selected.itemSet += 1
                ElseIf obj_selected.itemSet = 3 Then
                    obj_selected.lastPoint = m_pt
                    obj_selected.itemSet += 1

                    Dim x_set = {obj_selected.startPoint.X, obj_selected.middlePoint.X, obj_selected.endPoint.X, obj_selected.lastPoint.X}
                    obj_selected.leftTop.X = GetMinimumInSet(x_set)
                    obj_selected.rightBottom.X = GetMaximumInSet(x_set)
                    Dim y_set = {obj_selected.startPoint.Y, obj_selected.middlePoint.Y, obj_selected.endPoint.Y, obj_selected.lastPoint.Y}
                    obj_selected.leftTop.Y = GetMinimumInSet(y_set)
                    obj_selected.rightBottom.Y = GetMaximumInSet(y_set)
                ElseIf obj_selected.itemSet = 4 Then
                    obj_selected.drawPoint = m_pt
                    obj_selected.itemSet += 1
                End If
            End If

            Dim start_point As Point = New Point()
            Dim middle_point As Point = New Point()
            Dim end_point As Point = New Point()
            Dim draw_point As Point = New Point()

            start_point.X = CInt(obj_selected.startPoint.X * width)
            start_point.Y = CInt(obj_selected.startPoint.Y * height)
            middle_point.X = CInt(obj_selected.middlePoint.X * width)
            middle_point.Y = CInt(obj_selected.middlePoint.Y * height)
            end_point.X = CInt(obj_selected.endPoint.X * width)
            end_point.Y = CInt(obj_selected.endPoint.Y * height)
            draw_point.X = CInt(obj_selected.drawPoint.X * width)
            draw_point.Y = CInt(obj_selected.drawPoint.Y * height)
            Dim last_point As Point = New Point()
            last_point.X = CInt(obj_selected.lastPoint.X * width)
            last_point.Y = CInt(obj_selected.lastPoint.Y * height)

            If obj_selected.itemSet = item_set_limit - 1 Then
                If cur_measure_type = MeasureType.lineAlign OrElse cur_measure_type = MeasureType.lineFixed Then
                    If cur_measure_type = MeasureType.lineAlign Then
                        obj_selected.length = Math.Sqrt(Math.Pow(end_point.X - start_point.X, 2) + Math.Pow(end_point.Y - start_point.Y, 2))
                    Else
                        Dim length = Math.Sqrt(Math.Pow(end_point.X - start_point.X, 2) + Math.Pow(end_point.Y - start_point.Y, 2)) * CF
                        obj_selected.endPoint.X = obj_selected.startPoint.X + (obj_selected.endPoint.X - obj_selected.startPoint.X) / length * obj_selected.length * width
                        obj_selected.endPoint.Y = obj_selected.startPoint.Y + (obj_selected.endPoint.Y - obj_selected.startPoint.Y) / length * obj_selected.length * height

                        obj_selected.leftTop.X = Math.Min(obj_selected.startPoint.X, obj_selected.endPoint.X)
                        obj_selected.leftTop.Y = Math.Min(obj_selected.startPoint.Y, obj_selected.endPoint.Y)
                        obj_selected.rightBottom.X = Math.Max(obj_selected.startPoint.X, obj_selected.endPoint.X)
                        obj_selected.rightBottom.Y = Math.Max(obj_selected.startPoint.Y, obj_selected.endPoint.Y)
                    End If

                    Dim angle As Double = 0

                    If start_point.Y >= end_point.Y Then
                        angle = CalcAngleBetweenTwoLines(end_point, start_point, New Point(start_point.X + 10, start_point.Y))
                    Else
                        angle = CalcAngleBetweenTwoLines(start_point, end_point, New Point(end_point.X + 10, end_point.Y))
                    End If

                    obj_selected.angle = angle * 360 / Math.PI / 2
                    GetObjName(objList, obj_selected, unit)
                ElseIf cur_measure_type = MeasureType.lineHorizontal Then
                    obj_selected.length = Math.Abs(CDbl(end_point.X - start_point.X))

                    obj_selected.angle = 0
                    GetObjName(objList, obj_selected, unit)
                ElseIf cur_measure_type = MeasureType.lineVertical Then
                    obj_selected.length = Math.Abs(CDbl(end_point.Y - start_point.Y))

                    obj_selected.angle = 90
                    GetObjName(objList, obj_selected, unit)
                ElseIf cur_measure_type = MeasureType.lineParallel OrElse cur_measure_type = MeasureType.ptToLine Then
                    obj_selected.length = CalcDistFromPointToLine(start_point, middle_point, end_point)

                    Dim angle As Double = 0

                    If start_point.Y >= middle_point.Y Then
                        angle = CalcAngleBetweenTwoLines(middle_point, start_point, New Point(start_point.X + 10, start_point.Y))
                    Else
                        angle = CalcAngleBetweenTwoLines(start_point, middle_point, New Point(middle_point.X + 10, middle_point.Y))
                    End If

                    obj_selected.angle = angle * 360 / Math.PI / 2 + 90

                    GetObjName(objList, obj_selected, unit)
                ElseIf cur_measure_type = MeasureType.angle Then
                    'correct code
                    Dim angle = CalcAngleBetweenTwoLines(start_point, middle_point, end_point)
                    obj_selected.angle = angle * 360 / Math.PI / 2
                    GetObjName(objList, obj_selected, unit)
                ElseIf cur_measure_type = MeasureType.arc Then
                    Dim A = start_point
                    Dim B = middle_point
                    Dim C = end_point
                    Dim d_AB = Math.Sqrt(Math.Pow(B.X - A.X, 2.0R) + Math.Pow(B.Y - A.Y, 2.0R))
                    Dim d_BC = Math.Sqrt(Math.Pow(B.X - C.X, 2.0R) + Math.Pow(B.Y - C.Y, 2.0R))
                    Dim d_AC = Math.Sqrt(Math.Pow(C.X - A.X, 2.0R) + Math.Pow(C.Y - A.Y, 2.0R))
                    If d_AB + d_BC < d_AC + 0.2R And d_AB + d_BC > d_AC - 0.2R Then
                        Return False
                    End If
                    Dim t As Triangle = New Triangle(New Point3d(start_point.X, start_point.Y, 0), New Point3d(middle_point.X, middle_point.Y, 0), New Point3d(end_point.X, end_point.Y, 0))
                    Dim angle_a = t.Angle_A * 360.0R / Math.PI
                    Dim angle_b = t.Angle_B * 360.0R / Math.PI
                    Dim angle_c = t.Angle_C * 360.0R / Math.PI
                    Dim circumcenterpt = t.Circumcenter
                    Dim centerpt = New Point(Convert.ToInt32(circumcenterpt.X), Convert.ToInt32(circumcenterpt.Y))

                    obj_selected.arc = t.Circumcircle.R

                    obj_selected.arcObject.radius = obj_selected.arc / width
                    obj_selected.arcObject.center_pt = New PointF(centerpt.X / CSng(width), centerpt.Y / CSng(height))
                    GetObjName(objList, obj_selected, unit)
                ElseIf cur_measure_type = MeasureType.annotation Then
                    obj_selected.annotation = "annotation"
                    GetObjName(objList, obj_selected, unit)
                ElseIf cur_measure_type = MeasureType.angle2Line Then
                    Dim inter_pt = CalcInterSection(start_point, middle_point, end_point, last_point)
                    If inter_pt.X = 10000 AndAlso inter_pt.Y = 10000 Then
                        obj_selected.angle = 0
                    Else
                        Dim angle = CalcAngleBetweenTwoLines(start_point, inter_pt, end_point)
                        obj_selected.angle = angle * 360 / Math.PI / 2
                        obj_selected.commonPoint = New PointF(CSng(inter_pt.X) / width, CSng(inter_pt.Y) / height)
                    End If
                    GetObjName(objList, obj_selected, unit)
                ElseIf cur_measure_type = MeasureType.angleFixed Then
                    GetObjName(objList, obj_selected, unit)
                ElseIf cur_measure_type = MeasureType.arcFixed Then
                    GetObjName(objList, obj_selected, unit)
                End If
            End If

            obj_selected.lineInfor.line_style = line_infor.line_style
            obj_selected.lineInfor.line_width = line_infor.line_width
            obj_selected.lineInfor.line_color = line_infor.line_color
            obj_selected.fontInfor.font_color = font_infor.font_color
            obj_selected.fontInfor.text_font = font_infor.text_font

            If cur_measure_type = MeasureType.measureScale Then
                end_point = start_point

                Dim length_px As Integer
                If Equals(obj_selected.scaleObject.style, "horizontal") Then
                    length_px = CInt(obj_selected.scaleObject.length / CF)
                    end_point.X += length_px
                    obj_selected.scaleObject.trans_angle = 0
                    obj_selected.length = obj_selected.scaleObject.length / CF
                Else
                    length_px = CInt(obj_selected.scaleObject.length / CF)
                    end_point.Y += length_px
                    obj_selected.scaleObject.trans_angle = 90
                    obj_selected.length = obj_selected.scaleObject.length / CF
                End If

                obj_selected.endPoint = New PointF(end_point.X / CSng(width), end_point.Y / CSng(height))
                GetObjName(objList, obj_selected, unit)
            End If

            If obj_selected.itemSet = item_set_limit Then
                If cur_measure_type = MeasureType.angle Then
                    obj_selected.startPoint = obj_selected.angleObject.start_pt
                    obj_selected.endPoint = obj_selected.angleObject.end_pt
                    Dim x_set = {obj_selected.middlePoint.X, obj_selected.startPoint.X, obj_selected.endPoint.X}
                    Dim y_set = {obj_selected.middlePoint.Y, obj_selected.startPoint.Y, obj_selected.endPoint.Y}
                    obj_selected.leftTop.X = GetMinimumInSet(x_set)
                    obj_selected.leftTop.Y = GetMinimumInSet(y_set)
                    obj_selected.rightBottom.X = GetMaximumInSet(x_set)
                    obj_selected.rightBottom.Y = GetMaximumInSet(y_set)
                End If
                Return True
            Else
                Return False
            End If
        End If
        Return False
    End Function

    ''' <summary>
    ''' modify object selected when mouse move.
    ''' </summary>
    ''' <paramname="obj_list">The list of objects.</param>
    ''' <paramname="item_index">The index of selected item.</param>
    ''' <paramname="width">The width of origianl image.</param>
    ''' <paramname="height">The height of origianl image.</param>

    Public Sub ModifyObjSelected(ByVal obj_list As List(Of MeasureObject), ByVal item_index As Integer, ByVal width As Integer, ByVal height As Integer)
        Dim obj_selected = obj_list.ElementAt(item_index)

        Dim cur_measure_type = obj_selected.measuringType

        If cur_measure_type = MeasureType.lineAlign OrElse cur_measure_type = MeasureType.lineHorizontal OrElse cur_measure_type = MeasureType.lineVertical OrElse cur_measure_type = MeasureType.measureScale OrElse cur_measure_type = MeasureType.pencil Then
            obj_selected.leftTop.X = Math.Min(obj_selected.startPoint.X, obj_selected.endPoint.X)
            obj_selected.leftTop.Y = Math.Min(obj_selected.startPoint.Y, obj_selected.endPoint.Y)
            obj_selected.rightBottom.X = Math.Max(obj_selected.startPoint.X, obj_selected.endPoint.X)
            obj_selected.rightBottom.Y = Math.Max(obj_selected.startPoint.Y, obj_selected.endPoint.Y)
        ElseIf cur_measure_type = MeasureType.arc OrElse cur_measure_type = MeasureType.ptToLine OrElse cur_measure_type = MeasureType.lineParallel Then
            Dim x_set = {obj_selected.startPoint.X, obj_selected.middlePoint.X, obj_selected.endPoint.X}
            obj_selected.leftTop.X = GetMinimumInSet(x_set)
            obj_selected.rightBottom.X = GetMaximumInSet(x_set)
            Dim y_set = {obj_selected.startPoint.Y, obj_selected.middlePoint.Y, obj_selected.endPoint.Y}
            obj_selected.leftTop.Y = GetMinimumInSet(y_set)
            obj_selected.rightBottom.Y = GetMaximumInSet(y_set)
        ElseIf cur_measure_type = MeasureType.angle2Line Then
            Dim x_set = {obj_selected.startPoint.X, obj_selected.middlePoint.X, obj_selected.endPoint.X, obj_selected.lastPoint.X}
            obj_selected.leftTop.X = GetMinimumInSet(x_set)
            obj_selected.rightBottom.X = GetMaximumInSet(x_set)
            Dim y_set = {obj_selected.startPoint.Y, obj_selected.middlePoint.Y, obj_selected.endPoint.Y, obj_selected.lastPoint.Y}
            obj_selected.leftTop.Y = GetMinimumInSet(y_set)
            obj_selected.rightBottom.Y = GetMaximumInSet(y_set)
        ElseIf cur_measure_type = MeasureType.angle Then
            Dim x_set = {obj_selected.startPoint.X, obj_selected.middlePoint.X, obj_selected.endPoint.X}
            obj_selected.leftTop.X = GetMinimumInSet(x_set)
            obj_selected.rightBottom.X = GetMaximumInSet(x_set)
            Dim y_set = {obj_selected.startPoint.Y, obj_selected.middlePoint.Y, obj_selected.endPoint.Y}
            obj_selected.leftTop.Y = GetMinimumInSet(y_set)
            obj_selected.rightBottom.Y = GetMaximumInSet(y_set)
        End If

        Dim start_point As Point = New Point()
        Dim middle_point As Point = New Point()
        Dim end_point As Point = New Point()
        Dim draw_point As Point = New Point()

        start_point.X = CInt(obj_selected.startPoint.X * width)
        start_point.Y = CInt(obj_selected.startPoint.Y * height)
        end_point.X = CInt(obj_selected.endPoint.X * width)
        end_point.Y = CInt(obj_selected.endPoint.Y * height)

        middle_point.X = CInt(obj_selected.middlePoint.X * width)
        middle_point.Y = CInt(obj_selected.middlePoint.Y * height)
        draw_point.X = CInt(obj_selected.drawPoint.X * width)
        draw_point.Y = CInt(obj_selected.drawPoint.Y * height)
        Dim last_point As Point = New Point()
        last_point.X = CInt(obj_selected.lastPoint.X * width)
        last_point.Y = CInt(obj_selected.lastPoint.Y * height)
        If cur_measure_type = MeasureType.lineAlign Then
            obj_selected.length = Math.Sqrt(Math.Pow(end_point.X - start_point.X, 2) + Math.Pow(end_point.Y - start_point.Y, 2))

            Dim angle As Double = 0

            If start_point.Y >= end_point.Y Then
                angle = CalcAngleBetweenTwoLines(end_point, start_point, New Point(start_point.X + 10, start_point.Y))
            Else
                angle = CalcAngleBetweenTwoLines(start_point, end_point, New Point(end_point.X + 10, end_point.Y))
            End If

            obj_selected.angle = angle * 360 / Math.PI / 2
        ElseIf cur_measure_type = MeasureType.lineHorizontal Then
            obj_selected.length = Math.Abs(CDbl(end_point.X - start_point.X))

            obj_selected.angle = 0
        ElseIf cur_measure_type = MeasureType.lineVertical Then
            obj_selected.length = Math.Abs(CDbl(end_point.Y - start_point.Y))

            obj_selected.angle = 90
        ElseIf cur_measure_type = MeasureType.measureScale Then
            obj_selected.length = Math.Sqrt(Math.Pow(end_point.X - start_point.X, 2) + Math.Pow(end_point.Y - start_point.Y, 2))
        ElseIf cur_measure_type = MeasureType.lineParallel OrElse cur_measure_type = MeasureType.ptToLine Then
            obj_selected.length = CalcDistFromPointToLine(start_point, middle_point, end_point)

            Dim angle As Double = 0

            If start_point.Y >= middle_point.Y Then
                angle = CalcAngleBetweenTwoLines(middle_point, start_point, New Point(start_point.X + 10, start_point.Y))
            Else
                angle = CalcAngleBetweenTwoLines(start_point, middle_point, New Point(middle_point.X + 10, middle_point.Y))
            End If
            obj_selected.angle = angle * 360 / Math.PI / 2 + 90
        ElseIf cur_measure_type = MeasureType.angle Then
            'correct code
            Dim angle = CalcAngleBetweenTwoLines(start_point, middle_point, end_point)
            obj_selected.angle = angle * 360 / Math.PI / 2
        ElseIf cur_measure_type = MeasureType.arc Then

            Dim A = start_point
            Dim B = middle_point
            Dim C = end_point
            Dim d_AB = Math.Sqrt(Math.Pow(B.X - A.X, 2.0R) + Math.Pow(B.Y - A.Y, 2.0R))
            Dim d_BC = Math.Sqrt(Math.Pow(B.X - C.X, 2.0R) + Math.Pow(B.Y - C.Y, 2.0R))
            Dim d_AC = Math.Sqrt(Math.Pow(C.X - A.X, 2.0R) + Math.Pow(C.Y - A.Y, 2.0R))
            If d_AB + d_BC < d_AC + 0.2R And d_AB + d_BC > d_AC - 0.2R Then
                Return
            End If
            Dim t As Triangle = New Triangle(New Point3d(start_point.X, start_point.Y, 0), New Point3d(middle_point.X, middle_point.Y, 0), New Point3d(end_point.X, end_point.Y, 0))
            Dim angle_a = t.Angle_A * 360.0R / Math.PI
            Dim angle_b = t.Angle_B * 360.0R / Math.PI
            Dim angle_c = t.Angle_C * 360.0R / Math.PI
            Dim circumcenterpt = t.Circumcenter
            Dim centerpt = New Point(Convert.ToInt32(circumcenterpt.X), Convert.ToInt32(circumcenterpt.Y))

            obj_selected.arc = t.Circumcircle.R

            obj_selected.arcObject.radius = obj_selected.arc / width
            obj_selected.arcObject.center_pt = New PointF(centerpt.X / CSng(width), centerpt.Y / CSng(height))
        ElseIf cur_measure_type = MeasureType.angle2Line Then
            Dim inter_pt = CalcInterSection(start_point, middle_point, end_point, last_point)
            If inter_pt.X = 10000 AndAlso inter_pt.Y = 10000 Then
                obj_selected.angle = 0
            Else
                Dim angle = CalcAngleBetweenTwoLines(start_point, inter_pt, end_point)
                obj_selected.angle = angle * 360 / Math.PI / 2
                obj_selected.commonPoint = New PointF(CSng(inter_pt.X) / width, CSng(inter_pt.Y) / height)
            End If

        End If
        obj_list(item_index) = obj_selected
    End Sub

    ''' <summary>
    ''' Intialize CLine Object when you are movin C_Line.
    ''' </summary>
    ''' <paramname="Obj">The measureObject.</param>
    ''' <paramname="m_pt">The start point of Line Object.</param>
    Public Sub InitializeLineObj(ByRef Obj As MeasureObject, m_pt As PointF, ByVal line_infor As LineStyle, ByVal font_infor As FontInfor)
        Obj.startPoint = m_pt
        Obj.endPoint = m_pt
        Obj.lineObject.nor_pt1 = m_pt
        Obj.lineObject.nor_pt2 = m_pt
        Obj.lineObject.nor_pt3 = m_pt
        Obj.lineObject.nor_pt4 = m_pt
        Obj.lineObject.nor_pt5 = m_pt
        Obj.lineObject.nor_pt6 = m_pt
        Obj.lineObject.nor_pt7 = m_pt
        Obj.lineObject.nor_pt8 = m_pt
        Obj.measuringType = MeasureType.lineAlign
        Obj.lineInfor.line_style = line_infor.line_style
        Obj.lineInfor.line_width = line_infor.line_width
        Obj.lineInfor.line_color = line_infor.line_color
        Obj.fontInfor.font_color = font_infor.font_color
        Obj.fontInfor.text_font = font_infor.text_font

    End Sub

    ''' <summary>
    ''' Intialize Line Object when you are moving C_Line.
    ''' </summary>
    ''' <paramname="Obj">The measureObject.</param>
    ''' <paramname="dx">the offset of X-axis.</param>
    ''' <paramname="dy">the offset of Y-axis.</param>
    ''' <paramname="width">the width of Original Image.</param>
    ''' <paramname="height">the height of Original Image.</param>
    Public Sub DrawLengthBetweenLines(pictureBox As PictureBox, ByRef Obj As MeasureObject, dx As Double, dy As Double, width As Integer, height As Integer, digit As Integer, CF As Double)
        Obj.endPoint.X = Obj.startPoint.X - dx
        Obj.endPoint.Y = Obj.startPoint.Y - dy
        Obj.lineObject.nor_pt2.X = Obj.startPoint.X - dx
        Obj.lineObject.nor_pt2.Y = Obj.startPoint.Y - dy
        Obj.lineObject.nor_pt4.X = Obj.startPoint.X - dx
        Obj.lineObject.nor_pt4.Y = Obj.startPoint.Y - dy

        Dim nor_point3 = New Point(Obj.lineObject.nor_pt3.X * pictureBox.Width, Obj.lineObject.nor_pt3.Y * pictureBox.Height)
        Dim nor_point4 = New Point(Obj.lineObject.nor_pt4.X * pictureBox.Width, Obj.lineObject.nor_pt4.Y * pictureBox.Height)
        Dim n_dist As Integer = CalcDistBetweenPoints(nor_point3, nor_point4)
        n_dist /= 10
        n_dist = Math.Max(n_dist, 5)
        Dim arr_points = New Point(1) {}
        Dim arr_points2 = New Point(1) {}
        arr_points = GetArrowPoints3(nor_point3, nor_point4, n_dist)
        arr_points2 = GetArrowPoints3(nor_point4, nor_point3, n_dist)
        Dim draw_pt = New Point((nor_point3.X + nor_point4.X) / 2, (nor_point3.Y + nor_point4.Y) / 2)
        If dx <> 0 Or dy <> 0 Then
            draw_pt.X += dy / Math.Sqrt(dx * dx + dy * dy) * 10
            draw_pt.X += dx / Math.Sqrt(dx * dx + dy * dy) * 10
        End If

        Dim angle As Double = 0
        If nor_point3.Y >= nor_point4.Y Then
            angle = CalcAngleBetweenTwoLines(nor_point4, nor_point3, New Point(nor_point3.X + 10, nor_point3.Y))
        Else
            angle = CalcAngleBetweenTwoLines(nor_point3, nor_point4, New Point(nor_point4.X + 10, nor_point4.Y))
        End If

        Obj.angle = angle * 360 / Math.PI / 2

        Dim ang_tran As Integer = Obj.angle
        Dim attri = -1
        If Obj.angle > 90 Then
            ang_tran = 180 - ang_tran
            attri = 1
        End If
        Obj.lineObject.nor_pt5 = New PointF(CSng(arr_points(0).X) / pictureBox.Width, CSng(arr_points(0).Y) / pictureBox.Height)
        Obj.lineObject.nor_pt6 = New PointF(CSng(arr_points(1).X) / pictureBox.Width, CSng(arr_points(1).Y) / pictureBox.Height)
        Obj.lineObject.nor_pt7 = New PointF(CSng(arr_points2(0).X) / pictureBox.Width, CSng(arr_points2(0).Y) / pictureBox.Height)
        Obj.lineObject.nor_pt8 = New PointF(CSng(arr_points2(1).X) / pictureBox.Width, CSng(arr_points2(1).Y) / pictureBox.Height)
        Obj.lineObject.draw_pt = New PointF(CSng(draw_pt.X) / pictureBox.Width, CSng(draw_pt.Y) / pictureBox.Height)
        Obj.lineObject.side_drag = Obj.lineObject.nor_pt4
        Obj.lineObject.trans_angle = Convert.ToInt32(attri * ang_tran)
        Obj.length = Math.Sqrt(Math.Pow(Obj.startPoint.X * width - Obj.endPoint.X * width, 2) + Math.Pow(Obj.startPoint.Y * height - Obj.endPoint.Y * height, 2))

        Dim graph As Graphics = pictureBox.CreateGraphics()
        Main_Form.showLegend = True
        DrawObjItem(graph, pictureBox, Obj, digit, CF)
        Main_Form.showLegend = False
        graph.Dispose()
    End Sub

    ''' <summary>
    ''' draw object list to picture box control.
    ''' </summary>
    ''' <paramname="pictureBox">The pictureBox control in which you want to draw object list.</param>
    ''' <paramname="object_list">The list of objects which you are going to draw.</param>
    ''' <paramname="digit">The digit of decimal numbers.</param>
    ''' <paramname="CF">The factor of measuring scale.</param>
    ''' <paramname="flag">The flag determines refresh.</param>

    <Extension()>
    Public Sub DrawObjList(ByVal pictureBox As PictureBox, ByVal object_list As List(Of MeasureObject), ByVal digit As Integer, ByVal CF As Double, ByVal flag As Boolean)
        pictureBox.Refresh()

        Dim graph As Graphics = pictureBox.CreateGraphics()
        DrawObjList2(graph, pictureBox, object_list, digit, CF)

        graph.Dispose()
    End Sub

    ''' <summary>
    ''' draw object to picture box control.
    ''' </summary>
    ''' <paramname="graph">The graphics for drawing object list.</param>
    ''' <paramname="pictureBox">The pictureBox control in which you want to draw object list.</param>
    ''' <paramname="item">The object which you are going to draw.</param>
    ''' <paramname="digit">The digit of decimal numbers.</param>
    ''' <paramname="CF">The factor of measuring scale.</param>
    Public Sub DrawObjItem(ByVal graph As Graphics, ByVal pictureBox As PictureBox, ByVal item As MeasureObject, ByVal digit As Integer, ByVal CF As Double)
        Dim start_point As Point = New Point()
        Dim middle_point As Point = New Point()
        Dim end_point As Point = New Point()
        Dim draw_point As Point = New Point()
        Dim inter_pt As Point = New Point()
        Dim last_point As Point = New Point()

        Dim graphPen2 As Pen = New Pen(item.lineInfor.line_color, item.lineInfor.line_width)
        Dim graphFont = item.fontInfor.text_font
        Dim graphBrush As SolidBrush = New SolidBrush(item.fontInfor.font_color)

        start_point.X = CInt(item.startPoint.X * pictureBox.Width)
        start_point.Y = CInt(item.startPoint.Y * pictureBox.Height)
        middle_point.X = CInt(item.middlePoint.X * pictureBox.Width)
        middle_point.Y = CInt(item.middlePoint.Y * pictureBox.Height)
        end_point.X = CInt(item.endPoint.X * pictureBox.Width)
        end_point.Y = CInt(item.endPoint.Y * pictureBox.Height)
        draw_point.X = CInt(item.drawPoint.X * pictureBox.Width)
        draw_point.Y = CInt(item.drawPoint.Y * pictureBox.Height)
        inter_pt.X = CInt(item.commonPoint.X * pictureBox.Width)
        inter_pt.Y = CInt(item.commonPoint.Y * pictureBox.Height)
        last_point.X = CInt(item.lastPoint.X * pictureBox.Width)
        last_point.Y = CInt(item.lastPoint.Y * pictureBox.Height)

        If item.measuringType = MeasureType.lineAlign OrElse item.measuringType = MeasureType.lineHorizontal OrElse item.measuringType = MeasureType.lineVertical OrElse item.measuringType = MeasureType.lineParallel OrElse item.measuringType = MeasureType.ptToLine OrElse item.measuringType = MeasureType.lineFixed Then
            'graph.DrawLine(graphPen, start_point, end_point);
            If item.measuringType = MeasureType.lineParallel Then end_point = last_point
            Dim nor_pt1, nor_pt2, nor_pt3, nor_pt4, nor_pt5, nor_pt6, nor_pt7, nor_pt8, side_pt, draw_pt As Point
            If item.measuringType = MeasureType.lineFixed Then
                Dim length = Math.Sqrt(Math.Pow(end_point.X - start_point.X, 2) + Math.Pow(end_point.Y - start_point.Y, 2)) * CF

                nor_pt1 = New Point(item.lineObject.nor_pt1.X * pictureBox.Width, item.lineObject.nor_pt1.Y * pictureBox.Height)
                nor_pt3 = New Point(item.lineObject.nor_pt3.X * pictureBox.Width, item.lineObject.nor_pt3.Y * pictureBox.Height)
                nor_pt5 = New Point(item.lineObject.nor_pt5.X * pictureBox.Width, item.lineObject.nor_pt5.Y * pictureBox.Height)
                nor_pt6 = New Point(item.lineObject.nor_pt6.X * pictureBox.Width, item.lineObject.nor_pt6.Y * pictureBox.Height)
                Dim deltaX = item.lineObject.nor_pt1.X
                Dim deltaY = item.lineObject.nor_pt1.Y
                nor_pt2 = New Point((item.lineObject.nor_pt2.X / CF + deltaX) * pictureBox.Width, (item.lineObject.nor_pt2.Y / CF + deltaY) * pictureBox.Height)
                deltaX = item.lineObject.nor_pt3.X
                deltaY = item.lineObject.nor_pt3.Y
                nor_pt4 = New Point((item.lineObject.nor_pt4.X / CF + deltaX) * pictureBox.Width, (item.lineObject.nor_pt4.Y / CF + deltaY) * pictureBox.Height)
                nor_pt7 = New Point(nor_pt4.X - (nor_pt5.X - nor_pt3.X), nor_pt4.Y - (nor_pt5.Y - nor_pt3.Y))
                nor_pt8 = New Point(nor_pt4.X - (nor_pt6.X - nor_pt3.X), nor_pt4.Y - (nor_pt6.Y - nor_pt3.Y))
                side_pt = New Point((item.lineObject.side_drag.X / CF + deltaX) * pictureBox.Width, (item.lineObject.side_drag.Y / CF + deltaY) * pictureBox.Height)
                draw_pt = New Point((item.lineObject.draw_pt.X / CF + deltaX) * pictureBox.Width, (item.lineObject.draw_pt.Y / CF + deltaY) * pictureBox.Height)
            Else
                nor_pt1 = New Point(item.lineObject.nor_pt1.X * pictureBox.Width, item.lineObject.nor_pt1.Y * pictureBox.Height)
                nor_pt2 = New Point(item.lineObject.nor_pt2.X * pictureBox.Width, item.lineObject.nor_pt2.Y * pictureBox.Height)
                nor_pt3 = New Point(item.lineObject.nor_pt3.X * pictureBox.Width, item.lineObject.nor_pt3.Y * pictureBox.Height)
                nor_pt4 = New Point(item.lineObject.nor_pt4.X * pictureBox.Width, item.lineObject.nor_pt4.Y * pictureBox.Height)
                nor_pt5 = New Point(item.lineObject.nor_pt5.X * pictureBox.Width, item.lineObject.nor_pt5.Y * pictureBox.Height)
                nor_pt6 = New Point(item.lineObject.nor_pt6.X * pictureBox.Width, item.lineObject.nor_pt6.Y * pictureBox.Height)
                nor_pt7 = New Point(item.lineObject.nor_pt7.X * pictureBox.Width, item.lineObject.nor_pt7.Y * pictureBox.Height)
                nor_pt8 = New Point(item.lineObject.nor_pt8.X * pictureBox.Width, item.lineObject.nor_pt8.Y * pictureBox.Height)
                side_pt = New Point(item.lineObject.side_drag.X * pictureBox.Width, item.lineObject.side_drag.Y * pictureBox.Height)
                draw_pt = New Point(item.lineObject.draw_pt.X * pictureBox.Width, item.lineObject.draw_pt.Y * pictureBox.Height)
            End If

            graph.DrawLine(graphPen2, start_point, nor_pt1)
            graph.DrawLine(graphPen2, end_point, nor_pt2)
            graph.DrawLine(graphPen2, nor_pt3, nor_pt4)
            graph.DrawLine(graphPen2, nor_pt3, nor_pt5)
            graph.DrawLine(graphPen2, nor_pt3, nor_pt6)
            graph.DrawLine(graphPen2, nor_pt4, nor_pt7)
            graph.DrawLine(graphPen2, nor_pt4, nor_pt8)
            graph.DrawLine(graphPen2, nor_pt4, side_pt)

            graph.RotateTransform(item.lineObject.trans_angle)
            Dim trans_pt = GetRotationTransform(draw_pt, item.lineObject.trans_angle)

            Dim length_decimal = GetDecimalNumber(item.length, digit, CF)
            If item.measuringType = MeasureType.lineFixed Then
                length_decimal = item.scaleObject.length
            End If

            If Main_Form.showLegend = True Then
                Dim output = item.name + " " + length_decimal.ToString()
                Dim textSize As SizeF = graph.MeasureString(output, graphFont)
                graph.DrawString(output, graphFont, graphBrush, New RectangleF(trans_pt.X - textSize.Width / 2, trans_pt.Y - textSize.Height / 2, textSize.Width, textSize.Height))
            Else
                Dim output = item.name
                Dim textSize As SizeF = graph.MeasureString(output, graphFont)
                graph.DrawString(output, graphFont, graphBrush, New RectangleF(trans_pt.X - textSize.Width / 2, trans_pt.Y - textSize.Height / 2, textSize.Width, textSize.Height))
            End If

            graph.RotateTransform(-1 * item.lineObject.trans_angle)
        ElseIf item.measuringType = MeasureType.angle OrElse item.measuringType = MeasureType.angle2Line OrElse item.measuringType = MeasureType.angleFixed Then

            Dim st_pt As Point = New Point(item.angleObject.start_pt.X * pictureBox.Width, item.angleObject.start_pt.Y * pictureBox.Height)
            Dim end_pt As Point = New Point(item.angleObject.end_pt.X * pictureBox.Width, item.angleObject.end_pt.Y * pictureBox.Height)
            Dim nor_pt1 As Point = New Point(item.angleObject.nor_pt1.X * pictureBox.Width, item.angleObject.nor_pt1.Y * pictureBox.Height)
            Dim nor_pt2 As Point = New Point(item.angleObject.nor_pt2.X * pictureBox.Width, item.angleObject.nor_pt2.Y * pictureBox.Height)
            Dim nor_pt3 As Point = New Point(item.angleObject.nor_pt3.X * pictureBox.Width, item.angleObject.nor_pt3.Y * pictureBox.Height)
            Dim nor_pt4 As Point = New Point(item.angleObject.nor_pt4.X * pictureBox.Width, item.angleObject.nor_pt4.Y * pictureBox.Height)
            Dim nor_pt5 As Point = New Point(item.angleObject.nor_pt5.X * pictureBox.Width, item.angleObject.nor_pt5.Y * pictureBox.Height)
            Dim nor_pt6 As Point = New Point(item.angleObject.nor_pt6.X * pictureBox.Width, item.angleObject.nor_pt6.Y * pictureBox.Height)
            Dim side_pt As Point = New Point(item.angleObject.side_drag.X * pictureBox.Width, item.angleObject.side_drag.Y * pictureBox.Height)
            Dim start_angle = item.angleObject.start_angle
            Dim sweep_angle = item.angleObject.sweep_angle
            If item.measuringType = MeasureType.angle2Line Then middle_point = inter_pt
            Dim radius = Convert.ToInt32(item.angleObject.radius * pictureBox.Width)
            graph.DrawArc(graphPen2, New Rectangle(middle_point.X - radius, middle_point.Y - radius, radius * 2, radius * 2), CSng(start_angle), CSng(sweep_angle))

            If item.measuringType = MeasureType.angle OrElse item.measuringType = MeasureType.angleFixed Then
                graph.DrawLine(graphPen2, middle_point, st_pt)
                graph.DrawLine(graphPen2, middle_point, end_pt)
            End If
            graph.DrawLine(graphPen2, nor_pt1, nor_pt2)
            graph.DrawLine(graphPen2, nor_pt1, nor_pt3)
            graph.DrawLine(graphPen2, nor_pt4, nor_pt5)
            graph.DrawLine(graphPen2, nor_pt4, nor_pt6)
            If item.angleObject.side_index = 1 Then
                graph.DrawLine(graphPen2, nor_pt1, side_pt)
            Else
                graph.DrawLine(graphPen2, nor_pt4, side_pt)
            End If

            graph.RotateTransform(item.angleObject.trans_angle)
            Dim draw_pt As Point = New Point(item.angleObject.draw_pt.X * pictureBox.Width, item.angleObject.draw_pt.Y * pictureBox.Height)
            Dim trans_pt = GetRotationTransform(draw_pt, item.angleObject.trans_angle)
            Dim length_decimal = GetDecimalNumber(Math.Abs(item.angleObject.sweep_angle), digit, 1)

            If Main_Form.showLegend = True Then
                Dim output = item.name + " " + length_decimal.ToString()
                Dim textSize As SizeF = graph.MeasureString(output, graphFont)
                graph.DrawString(output, graphFont, graphBrush, New RectangleF(trans_pt.X - textSize.Width / 2, trans_pt.Y - textSize.Height / 2, textSize.Width, textSize.Height))
            Else
                Dim output = item.name
                Dim textSize As SizeF = graph.MeasureString(output, graphFont)
                graph.DrawString(output, graphFont, graphBrush, New RectangleF(trans_pt.X - textSize.Width / 2, trans_pt.Y - textSize.Height / 2, textSize.Width, textSize.Height))
            End If
            graph.RotateTransform(-1 * item.angleObject.trans_angle)
        ElseIf item.measuringType = MeasureType.arc Then
            Dim center_pt As Point = New Point(item.arcObject.center_pt.X * pictureBox.Width, item.arcObject.center_pt.Y * pictureBox.Height)
            Dim circle_pt As Point = New Point(item.arcObject.circle_pt.X * pictureBox.Width, item.arcObject.circle_pt.Y * pictureBox.Height)
            'int radius = Convert.ToInt32(Utils.CalcDistBetweenPoints(center_pt, circle_pt));
            Dim draw_pt As Point = New Point(item.arcObject.draw_pt.X * pictureBox.Width, item.arcObject.draw_pt.Y * pictureBox.Height)
            Dim arr_pt1 As Point = New Point(item.arcObject.arr_pt1.X * pictureBox.Width, item.arcObject.arr_pt1.Y * pictureBox.Height)
            Dim arr_pt2 As Point = New Point(item.arcObject.arr_pt2.X * pictureBox.Width, item.arcObject.arr_pt2.Y * pictureBox.Height)
            Dim arr_pt3 As Point = New Point(item.arcObject.arr_pt3.X * pictureBox.Width, item.arcObject.arr_pt3.Y * pictureBox.Height)
            Dim arr_pt4 As Point = New Point(item.arcObject.arr_pt4.X * pictureBox.Width, item.arcObject.arr_pt4.Y * pictureBox.Height)
            Dim trans_angle As Single = item.arcObject.trans_angle

            'graph.DrawArc(graphPen2, new Rectangle(center_pt.X - radius, center_pt.Y - radius, radius * 2, radius * 2), 0, 360);
            Dim A = start_point
            Dim B = middle_point
            Dim C = end_point
            Dim d_AB = Math.Sqrt(Math.Pow(B.X - A.X, 2.0R) + Math.Pow(B.Y - A.Y, 2.0R))
            Dim d_BC = Math.Sqrt(Math.Pow(B.X - C.X, 2.0R) + Math.Pow(B.Y - C.Y, 2.0R))
            Dim d_AC = Math.Sqrt(Math.Pow(C.X - A.X, 2.0R) + Math.Pow(C.Y - A.Y, 2.0R))
            If d_AB + d_BC < d_AC + 0.2R And d_AB + d_BC > d_AC - 0.2R Then
                graph.DrawLine(graphPen2, A, B)
                graph.DrawLine(graphPen2, B, C)
            Else
                Dim t = New Triangle(New Point3d(start_point.X, start_point.Y, 0R), New Point3d(middle_point.X, middle_point.Y, 0R), New Point3d(end_point.X, end_point.Y, 0R))
                Dim angle_a = t.Angle_A * 360.0R / Math.PI
                Dim angle_b = t.Angle_B * 360.0R / Math.PI
                Dim angle_c = t.Angle_C * 360.0R / Math.PI
                Dim circumcenterpt = t.Circumcenter
                Dim centerpt = New Point(Convert.ToInt32(circumcenterpt.X), Convert.ToInt32(circumcenterpt.Y))
                Dim radius = Convert.ToInt32(t.Circumcircle.R)
                Dim right_cenerpt = New Point(centerpt.X + radius, centerpt.Y)
                Dim M = New PointF(right_cenerpt.X / CSng(pictureBox.Width), right_cenerpt.Y / CSng(pictureBox.Height))
                item = sorting_Points(item, M)
                A = New Point(item.startPoint.X * pictureBox.Width, item.startPoint.Y * pictureBox.Height)
                B = New Point(item.middlePoint.X * pictureBox.Width, item.middlePoint.Y * pictureBox.Height)
                C = New Point(item.endPoint.X * pictureBox.Width, item.endPoint.Y * pictureBox.Height)


                If centerpt.Y = C.Y Then
                    Return
                End If
                Dim p_t = New Triangle(New Point3d(centerpt.X, centerpt.Y, 0R), New Point3d(C.X, C.Y, 0R), New Point3d(centerpt.X + radius, centerpt.Y, 0R))
                Dim rotate_angle = Convert.ToInt32(angle_a + angle_c)
                Dim add_a = 0
                If A.Y < right_cenerpt.Y And B.Y < right_cenerpt.Y And C.Y < right_cenerpt.Y Then
                    If B.X > A.X And B.X > C.X Or B.X < A.X And B.X < C.X Or B.X < A.X And B.X > C.X Then
                        add_a = -Convert.ToInt32(p_t.Angle_A * 180.0R / Math.PI)
                    End If
                ElseIf A.Y < right_cenerpt.Y And B.Y > right_cenerpt.Y And C.Y < right_cenerpt.Y Then
                    add_a = -Convert.ToInt32(p_t.Angle_A * 180.0R / Math.PI)
                ElseIf A.Y > right_cenerpt.Y And B.Y < right_cenerpt.Y And C.Y < right_cenerpt.Y Then
                    add_a = -Convert.ToInt32(p_t.Angle_A * 180.0R / Math.PI)
                ElseIf A.Y < right_cenerpt.Y And B.Y < right_cenerpt.Y And C.Y > right_cenerpt.Y Then
                    add_a = Convert.ToInt32(p_t.Angle_A * 180.0R / Math.PI)
                ElseIf A.Y > right_cenerpt.Y And B.Y < right_cenerpt.Y And C.Y > right_cenerpt.Y Then
                    add_a = Convert.ToInt32(p_t.Angle_A * 180.0R / Math.PI)
                ElseIf A.Y > right_cenerpt.Y And B.Y > right_cenerpt.Y And C.Y > right_cenerpt.Y Then
                    add_a = Convert.ToInt32(p_t.Angle_A * 180.0R / Math.PI)
                ElseIf A.Y < right_cenerpt.Y And B.Y > right_cenerpt.Y And C.Y > right_cenerpt.Y Then
                    add_a = Convert.ToInt32(p_t.Angle_A * 180.0R / Math.PI)
                ElseIf A.Y > right_cenerpt.Y And B.Y > right_cenerpt.Y And C.Y < right_cenerpt.Y Then
                    add_a = -Convert.ToInt32(p_t.Angle_A * 180.0R / Math.PI)
                End If
                graph.DrawArc(graphPen2, New Rectangle(centerpt.X - radius, centerpt.Y - radius, radius * 2, radius * 2), add_a, rotate_angle)

                graph.DrawLine(graphPen2, center_pt, circle_pt)
                graph.DrawLine(graphPen2, center_pt, arr_pt1)
                graph.DrawLine(graphPen2, center_pt, arr_pt2)
                graph.DrawLine(graphPen2, circle_pt, arr_pt3)
                graph.DrawLine(graphPen2, circle_pt, arr_pt4)

                graph.RotateTransform(trans_angle)
                Dim trans_pt = GetRotationTransform(draw_pt, trans_angle)

                Dim length_decimal = GetDecimalNumber(item.arc, digit, CF)

                If Main_Form.showLegend = True Then
                    Dim output = item.name + " " + length_decimal.ToString()
                    Dim textSize As SizeF = graph.MeasureString(output, graphFont)
                    graph.DrawString(output, graphFont, graphBrush, New RectangleF(trans_pt.X - textSize.Width / 2, trans_pt.Y - textSize.Height / 2, textSize.Width, textSize.Height))
                Else
                    Dim output = item.name
                    Dim textSize As SizeF = graph.MeasureString(output, graphFont)
                    graph.DrawString(output, graphFont, graphBrush, New RectangleF(trans_pt.X - textSize.Width / 2, trans_pt.Y - textSize.Height / 2, textSize.Width, textSize.Height))
                End If
                graph.RotateTransform(-1 * trans_angle)
            End If
        ElseIf item.measuringType = MeasureType.annotation Then
            Dim textsize As RectangleF = New RectangleF()
            textsize.X = draw_point.X
            textsize.Y = draw_point.Y
            textsize.Width = item.annoObject.size.Width
            textsize.Height = item.annoObject.size.Height
            graph.DrawString(item.annotation, graphFont, graphBrush, textsize)
            Dim left_top As Point = New Point(item.leftTop.X * pictureBox.Width, item.leftTop.Y * pictureBox.Height)
            Dim line_pt As Point = New Point(item.annoObject.line_pt.X * pictureBox.Width, item.annoObject.line_pt.Y * pictureBox.Height)
            graph.DrawLine(graphPen2, start_point, line_pt)
        ElseIf item.measuringType = MeasureType.pencil Then
            If Equals(item.lineInfor.line_style, "dotted") Then
                graphPen2.DashStyle = Drawing2D.DashStyle.Dot
            ElseIf Equals(item.lineInfor.line_style, "dashed") Then
                Dim dashValues As Single() = {5, 2}
                graphPen2.DashStyle = Drawing2D.DashStyle.Dash
                graphPen2.DashPattern = dashValues
            End If
            graph.DrawLine(graphPen2, start_point, end_point)
        ElseIf item.measuringType = MeasureType.measureScale Then
            Dim nor_pt1 As Point = New Point()
            Dim nor_pt2 As Point = New Point()
            Dim nor_pt3 As Point = New Point()
            Dim nor_pt4 As Point = New Point()
            If Equals(item.scaleObject.style, "horizontal") Then
                end_point.Y = start_point.Y
                nor_pt1.X = start_point.X
                nor_pt1.Y = start_point.Y - 10
                nor_pt2.X = start_point.X
                nor_pt2.Y = start_point.Y + 10
                nor_pt3.X = end_point.X
                nor_pt3.Y = end_point.Y - 10
                nor_pt4.X = end_point.X
                nor_pt4.Y = end_point.Y + 10
            Else
                end_point.X = start_point.X
                nor_pt1.X = start_point.X - 10
                nor_pt1.Y = start_point.Y
                nor_pt2.X = start_point.X + 10
                nor_pt2.Y = start_point.Y
                nor_pt3.X = end_point.X - 10
                nor_pt3.Y = end_point.Y
                nor_pt4.X = end_point.X + 10
                nor_pt4.Y = end_point.Y
            End If

            Dim trans_angle As Single = item.scaleObject.trans_angle
            Dim draw_pt As Point = New Point((start_point.X + end_point.X) / 2, (start_point.Y + end_point.Y) / 2)

            If Equals(item.scaleObject.style, "horizontal") Then
                draw_pt.Y += 20
            Else
                draw_pt.X += 20
            End If
            graph.DrawLine(graphPen2, start_point, end_point)
            graph.DrawLine(graphPen2, nor_pt1, nor_pt2)
            graph.DrawLine(graphPen2, nor_pt3, nor_pt4)

            graph.RotateTransform(trans_angle)
            Dim trans_pt = GetRotationTransform(draw_pt, trans_angle)

            Dim length_decimal = GetDecimalNumber(item.length, digit, CF)
            If Main_Form.showLegend = True Then
                Dim output = item.name + " " + length_decimal.ToString()
                Dim textSize As SizeF = graph.MeasureString(output, graphFont)
                graph.DrawString(output, graphFont, graphBrush, New RectangleF(trans_pt.X - textSize.Width / 2, trans_pt.Y - textSize.Height / 2, textSize.Width, textSize.Height))
            Else
                Dim output = item.name
                Dim textSize As SizeF = graph.MeasureString(output, graphFont)
                graph.DrawString(output, graphFont, graphBrush, New RectangleF(trans_pt.X - textSize.Width / 2, trans_pt.Y - textSize.Height / 2, textSize.Width, textSize.Height))
            End If

            graph.RotateTransform(-1 * trans_angle)
        ElseIf item.measuringType = MeasureType.arcFixed Then
            Dim center_pt As Point = New Point(item.arcObject.center_pt.X * pictureBox.Width, item.arcObject.center_pt.Y * pictureBox.Height)
            Dim deltaX = item.arcObject.center_pt.X
            Dim deltaY = item.arcObject.center_pt.Y
            Dim circle_pt As Point = New Point((item.arcObject.circle_pt.X / CF + deltaX) * pictureBox.Width, (item.arcObject.circle_pt.Y / CF + deltaY) * pictureBox.Height)
            Dim draw_pt As Point = New Point((item.arcObject.draw_pt.X / CF + deltaX) * pictureBox.Width, (item.arcObject.draw_pt.Y / CF + deltaY) * pictureBox.Height)
            Dim arr_pt1 As Point = New Point((item.arcObject.arr_pt1.X / CF + deltaX) * pictureBox.Width, (item.arcObject.arr_pt1.Y / CF + deltaY) * pictureBox.Height)
            Dim arr_pt2 As Point = New Point((item.arcObject.arr_pt2.X / CF + deltaX) * pictureBox.Width, (item.arcObject.arr_pt2.Y / CF + deltaY) * pictureBox.Height)
            Dim arr_pt3 As Point = New Point((item.arcObject.arr_pt3.X / CF + deltaX) * pictureBox.Width, (item.arcObject.arr_pt3.Y / CF + deltaY) * pictureBox.Height)
            Dim arr_pt4 As Point = New Point((item.arcObject.arr_pt4.X / CF + deltaX) * pictureBox.Width, (item.arcObject.arr_pt4.Y / CF + deltaY) * pictureBox.Height)
            Dim trans_angle As Single = item.arcObject.trans_angle

            Dim radius = CInt(item.arc / CF * pictureBox.Width)
            graph.DrawArc(graphPen2, New Rectangle(center_pt.X - radius, center_pt.Y - radius, radius * 2, radius * 2), 0, 360)

            graph.DrawLine(graphPen2, center_pt, circle_pt)
            graph.DrawLine(graphPen2, center_pt, arr_pt1)
            graph.DrawLine(graphPen2, center_pt, arr_pt2)
            graph.DrawLine(graphPen2, circle_pt, arr_pt3)
            graph.DrawLine(graphPen2, circle_pt, arr_pt4)

            graph.RotateTransform(trans_angle)
            Dim trans_pt = GetRotationTransform(draw_pt, trans_angle)

            'Dim length_decimal = item.scaleObject.length
            Dim length_decimal = GetDecimalNumber(item.scaleObject.length, digit, 1)

            If Main_Form.showLegend = True Then
                Dim output = item.name + " " + length_decimal.ToString()
                Dim textSize As SizeF = graph.MeasureString(output, graphFont)
                graph.DrawString(output, graphFont, graphBrush, New RectangleF(trans_pt.X - textSize.Width / 2, trans_pt.Y - textSize.Height / 2, textSize.Width, textSize.Height))
            Else
                Dim output = item.name
                Dim textSize As SizeF = graph.MeasureString(output, graphFont)
                graph.DrawString(output, graphFont, graphBrush, New RectangleF(trans_pt.X - textSize.Width / 2, trans_pt.Y - textSize.Height / 2, textSize.Width, textSize.Height))
            End If
            graph.RotateTransform(-1 * trans_angle)
        Else
            DrawCurveObjItem(graph, pictureBox, item, digit, CF, False)
        End If

        graphPen2.Dispose()
        graphBrush.Dispose()
    End Sub


    ''' <summary>
    ''' draw object list to picture box control.
    ''' </summary>
    ''' <paramname="graph">The graphics for drawing object list.</param>
    ''' <paramname="pictureBox">The pictureBox control in which you want to draw object list.</param>
    ''' <paramname="object_list">The list of objects which you are going to draw.</param>
    ''' <paramname="digit">The digit of decimal numbers.</param>
    ''' <paramname="CF">The factor of measuring scale.</param>
    Public Sub DrawObjList2(ByVal graph As Graphics, ByVal pictureBox As PictureBox, ByVal object_list As List(Of MeasureObject), ByVal digit As Integer, ByVal CF As Double)
        For Each item In object_list
            DrawObjItem(graph, pictureBox, item, digit, CF)
        Next

    End Sub

    ''' <summary>
    ''' draw object selected to picture box control.
    ''' </summary>
    ''' <paramname="pictureBox">The pictureBox control in which you want to draw object list.</param>
    ''' <paramname="obj_selected">The object which you are going to draw.</param>
    ''' <paramname="flag">The flag specifies the object is completed or not.</param>
    <Extension()>
    Public Sub DrawObjSelected(ByVal pictureBox As PictureBox, ByVal obj_selected As MeasureObject, ByVal flag As Boolean)
        Dim graph As Graphics = pictureBox.CreateGraphics()
        Dim graphPen As Pen = New Pen(Color.Red, 1)

        Dim start_point As Point = New Point()
        Dim middle_point As Point = New Point()
        Dim end_point As Point = New Point()
        Dim draw_point As Point = New Point()
        Dim last_point As Point = New Point()

        start_point.X = CInt(obj_selected.startPoint.X * pictureBox.Width)
        start_point.Y = CInt(obj_selected.startPoint.Y * pictureBox.Height)
        end_point.X = CInt(obj_selected.endPoint.X * pictureBox.Width)
        end_point.Y = CInt(obj_selected.endPoint.Y * pictureBox.Height)

        middle_point.X = CInt(obj_selected.middlePoint.X * pictureBox.Width)
        middle_point.Y = CInt(obj_selected.middlePoint.Y * pictureBox.Height)
        draw_point.X = CInt(obj_selected.drawPoint.X * pictureBox.Width)
        draw_point.Y = CInt(obj_selected.drawPoint.Y * pictureBox.Height)
        last_point.X = CInt(obj_selected.lastPoint.X * pictureBox.Width)
        last_point.Y = CInt(obj_selected.lastPoint.Y * pictureBox.Height)

        Dim radius = 2

        For i = 0 To obj_selected.itemSet - 1
            If obj_selected.measuringType = MeasureType.lineAlign OrElse obj_selected.measuringType = MeasureType.lineHorizontal OrElse obj_selected.measuringType = MeasureType.lineVertical OrElse obj_selected.measuringType = MeasureType.pencil OrElse obj_selected.measuringType = MeasureType.measureScale OrElse obj_selected.measuringType = MeasureType.lineAlign OrElse obj_selected.measuringType = MeasureType.arcFixed OrElse obj_selected.measuringType = MeasureType.lineFixed Then
                If i = 0 Then
                    graph.DrawArc(graphPen, New Rectangle(start_point.X - radius, start_point.Y - radius, radius * 2, radius * 2), 0, 360)
                    If obj_selected.measuringType = MeasureType.measureScale Then
                        graph.DrawArc(graphPen, New Rectangle(end_point.X - radius, end_point.Y - radius, radius * 2, radius * 2), 0, 360)
                    End If
                ElseIf i = 1 And obj_selected.measuringType <> MeasureType.arcFixed Then
                    graph.DrawArc(graphPen, New Rectangle(end_point.X - radius, end_point.Y - radius, radius * 2, radius * 2), 0, 360)
                End If
            ElseIf obj_selected.measuringType = MeasureType.angle OrElse obj_selected.measuringType = MeasureType.arc OrElse obj_selected.measuringType = MeasureType.angle2Line OrElse obj_selected.measuringType = MeasureType.lineParallel OrElse obj_selected.measuringType = MeasureType.ptToLine OrElse obj_selected.measuringType = MeasureType.angleFixed Then
                If i = 0 Then
                    graph.DrawArc(graphPen, New Rectangle(start_point.X - radius, start_point.Y - radius, radius * 2, radius * 2), 0, 360)
                ElseIf i = 1 Then
                    graph.DrawArc(graphPen, New Rectangle(middle_point.X - radius, middle_point.Y - radius, radius * 2, radius * 2), 0, 360)
                ElseIf i = 2 Then
                    graph.DrawArc(graphPen, New Rectangle(end_point.X - radius, end_point.Y - radius, radius * 2, radius * 2), 0, 360)
                ElseIf i = 3 Then
                    graph.DrawArc(graphPen, New Rectangle(last_point.X - radius, last_point.Y - radius, radius * 2, radius * 2), 0, 360)
                End If
            End If

        Next
        graph.Dispose()
        graphPen.Dispose()

    End Sub

    ''' <summary>
    ''' Highlight selected point.
    ''' </summary>
    ''' <paramname="pictureBox">The pictureBox control in which you want to draw object list.</param>
    ''' <paramname="obj_selected">The object which you are going to draw.</param>
    ''' <paramname="pt_index">The index of point.</param>
    <Extension()>
    Public Sub HighlightTargetPt(ByVal pictureBox As PictureBox, ByVal obj_selected As MeasureObject, ByVal pt_index As Integer)
        Dim graph As Graphics = pictureBox.CreateGraphics()
        Dim graphPen As Pen = New Pen(Color.Yellow, 2)

        Dim start_point As Point = New Point()
        Dim middle_point As Point = New Point()
        Dim end_point As Point = New Point()
        Dim draw_point As Point = New Point()
        Dim last_point As Point = New Point()

        start_point.X = CInt(obj_selected.startPoint.X * pictureBox.Width)
        start_point.Y = CInt(obj_selected.startPoint.Y * pictureBox.Height)
        end_point.X = CInt(obj_selected.endPoint.X * pictureBox.Width)
        end_point.Y = CInt(obj_selected.endPoint.Y * pictureBox.Height)

        middle_point.X = CInt(obj_selected.middlePoint.X * pictureBox.Width)
        middle_point.Y = CInt(obj_selected.middlePoint.Y * pictureBox.Height)
        draw_point.X = CInt(obj_selected.drawPoint.X * pictureBox.Width)
        draw_point.Y = CInt(obj_selected.drawPoint.Y * pictureBox.Height)
        last_point.X = CInt(obj_selected.lastPoint.X * pictureBox.Width)
        last_point.Y = CInt(obj_selected.lastPoint.Y * pictureBox.Height)

        Dim radius = 3

        Select Case pt_index
            Case 1
                graph.DrawArc(graphPen, New Rectangle(start_point.X - radius, start_point.Y - radius, radius * 2, radius * 2), 0, 360)
            Case 2
                graph.DrawArc(graphPen, New Rectangle(middle_point.X - radius, middle_point.Y - radius, radius * 2, radius * 2), 0, 360)
            Case 3
                graph.DrawArc(graphPen, New Rectangle(end_point.X - radius, end_point.Y - radius, radius * 2, radius * 2), 0, 360)
            Case 4
                graph.DrawArc(graphPen, New Rectangle(last_point.X - radius, last_point.Y - radius, radius * 2, radius * 2), 0, 360)
        End Select
    End Sub

    ''' <summary>
    ''' check if there is point in pos mouse clicked.
    ''' </summary>
    ''' <paramname="pictureBox">The pictureBox control in which you want to draw object list.</param>
    ''' <paramname="obj_selected">The object which you are going to draw.</param>
    ''' <paramname="m_pt">The position of mouse cursor.</param>
    <Extension()>
    Public Function CheckPointInPos(ByVal pictureBox As PictureBox, ByVal obj_selected As MeasureObject, ByVal m_pt As Point) As Integer
        Dim flag = False
        Dim start_point As Point = New Point()
        Dim middle_point As Point = New Point()
        Dim end_point As Point = New Point()
        Dim draw_point As Point = New Point()
        Dim last_point As Point = New Point()

        start_point.X = CInt(obj_selected.startPoint.X * pictureBox.Width)
        start_point.Y = CInt(obj_selected.startPoint.Y * pictureBox.Height)
        end_point.X = CInt(obj_selected.endPoint.X * pictureBox.Width)
        end_point.Y = CInt(obj_selected.endPoint.Y * pictureBox.Height)
        middle_point.X = CInt(obj_selected.middlePoint.X * pictureBox.Width)
        middle_point.Y = CInt(obj_selected.middlePoint.Y * pictureBox.Height)
        draw_point.X = CInt(obj_selected.drawPoint.X * pictureBox.Width)
        draw_point.Y = CInt(obj_selected.drawPoint.Y * pictureBox.Height)
        last_point.X = CInt(obj_selected.lastPoint.X * pictureBox.Width)
        last_point.Y = CInt(obj_selected.lastPoint.Y * pictureBox.Height)

        Dim radius = 5

        For i = 0 To obj_selected.itemSet - 1
            If obj_selected.measuringType = MeasureType.lineAlign OrElse obj_selected.measuringType = MeasureType.lineHorizontal OrElse obj_selected.measuringType = MeasureType.lineVertical OrElse obj_selected.measuringType = MeasureType.pencil OrElse obj_selected.measuringType = MeasureType.measureScale Then
                If i = 0 Then
                    flag = PointInRect(m_pt, New Rectangle(start_point.X - radius, start_point.Y - radius, radius * 2, radius * 2))
                    If flag Then
                        Return 1
                    Else
                        If obj_selected.measuringType = MeasureType.measureScale Then
                            flag = PointInRect(m_pt, New Rectangle(end_point.X - radius, end_point.Y - radius, radius * 2, radius * 2))
                            If flag Then Return 3
                        End If
                    End If
                ElseIf i = 1 Then
                    flag = PointInRect(m_pt, New Rectangle(end_point.X - radius, end_point.Y - radius, radius * 2, radius * 2))
                    If flag Then Return 3
                End If
            ElseIf obj_selected.measuringType = MeasureType.angle OrElse obj_selected.measuringType = MeasureType.arc OrElse obj_selected.measuringType = MeasureType.angle2Line OrElse obj_selected.measuringType = MeasureType.lineParallel OrElse obj_selected.measuringType = MeasureType.ptToLine Then
                If i = 0 Then
                    flag = PointInRect(m_pt, New Rectangle(start_point.X - radius, start_point.Y - radius, radius * 2, radius * 2))
                    If flag Then Return 1
                ElseIf i = 1 Then
                    flag = PointInRect(m_pt, New Rectangle(middle_point.X - radius, middle_point.Y - radius, radius * 2, radius * 2))
                    If flag Then Return 2
                ElseIf i = 2 Then
                    flag = PointInRect(m_pt, New Rectangle(end_point.X - radius, end_point.Y - radius, radius * 2, radius * 2))
                    If flag Then Return 3
                ElseIf i = 3 Then
                    flag = PointInRect(m_pt, New Rectangle(last_point.X - radius, last_point.Y - radius, radius * 2, radius * 2))
                    If flag Then Return 4
                End If
            End If

        Next

        Return -1
    End Function

    ''' <summary>
    ''' calculate start angle sweep angle between two lines
    ''' </summary>
    ''' <paramname="obj_selected">the object currently used.</param>
    ''' <paramname="start_point">The start point of the line.</param>
    ''' <paramname="middle_point">The middle point of the line.</param>
    ''' <paramname="end_point">The end point of the line.</param>
    ''' <paramname="target_point">The point of mouse cursor.</param>
    Public Function CalcStartAndSweepAngle(ByVal obj_selected As MeasureObject, ByVal start_point As Point, ByVal middle_point As Point, ByVal end_point As Point, ByVal target_point As Point) As Double()
        Dim angle = CalcAngleBetweenTwoLines(start_point, middle_point, target_point)
        angle = angle * 360 / Math.PI / 2
        'to calcuate the angle between x-axis and start_point-middle_point line
        Dim basis_pt As Point = New Point(middle_point.X + 10, middle_point.Y)

        Dim angle2 = CalcAngleBetweenTwoLines(basis_pt, middle_point, start_point)
        angle2 = angle2 * 360 / Math.PI / 2

        Dim centerpt = middle_point
        Dim radius = Convert.ToInt32(Math.Sqrt(Math.Pow(target_point.X - middle_point.X, 2) + Math.Pow(target_point.Y - middle_point.Y, 2)))
        Dim radius2 = If(radius - 10 > 1, radius - 10, 1)
        Dim clockwise = CheckAngleDirection(start_point, middle_point, end_point)
        Dim downbasis = CheckAngleDirection(basis_pt, middle_point, start_point)
        Dim start_angle, sweep_angle As Double
        Dim angle_direction As Integer

        If clockwise Then
            angle_direction = 1
        Else
            angle_direction = -1
        End If

        If 0 <= angle AndAlso angle < obj_selected.angle Then
            If downbasis Then
                start_angle = angle2
                sweep_angle = angle_direction * obj_selected.angle
            Else
                start_angle = 360 - angle2
                sweep_angle = angle_direction * obj_selected.angle
            End If
        Else
            If downbasis Then
                If clockwise Then
                    start_angle = angle2 + obj_selected.angle
                    sweep_angle = angle_direction * (180 - obj_selected.angle)
                Else
                    start_angle = 360 + angle2 - obj_selected.angle
                    sweep_angle = angle_direction * (180 - obj_selected.angle)
                End If
            Else
                If clockwise Then
                    start_angle = obj_selected.angle - angle2
                    sweep_angle = angle_direction * (180 - obj_selected.angle)
                Else
                    start_angle = 360 - obj_selected.angle - angle2
                    sweep_angle = angle_direction * (180 - obj_selected.angle)
                End If
            End If
        End If
        Dim ang_dirc As Integer
        If clockwise Then
            ang_dirc = 1
        Else
            ang_dirc = 2
        End If
        Dim angles = {start_angle, sweep_angle, ang_dirc}
        Return angles
    End Function

    ''' <summary>
    ''' calculate start angle sweep angle between two lines
    ''' </summary>
    ''' <paramname="obj_selected">the object currently used.</param>
    ''' <paramname="start_point">The start point of the line.</param>
    ''' <paramname="middle_point">The middle point of the line.</param>
    ''' <paramname="end_point">The end point of the line.</param>
    ''' <paramname="target_point">The point of mouse cursor.</param>
    Public Function CalcStartAndSweepAngleFixed(ByVal obj_selected As MeasureObject, ByVal start_point As Point, ByVal middle_point As Point, ByVal target_point As Point) As Double()
        'to calcuate the angle between x-axis and start_point-middle_point line
        Dim basis_pt As Point = New Point(middle_point.X + 10, middle_point.Y)
        Dim flag = CheckPointOnLine(start_point, middle_point, target_point)
        Dim angle2 = CalcAngleBetweenTwoLines(basis_pt, middle_point, start_point)
        angle2 = angle2 * 360 / Math.PI / 2

        Dim centerpt = middle_point
        Dim radius = Convert.ToInt32(Math.Sqrt(Math.Pow(target_point.X - middle_point.X, 2) + Math.Pow(target_point.Y - middle_point.Y, 2)))
        Dim radius2 = If(radius - 10 > 1, radius - 10, 1)
        Dim clockwise = True
        Dim downbasis = CheckAngleDirection(basis_pt, middle_point, start_point)
        Dim start_angle, sweep_angle As Double
        Dim angle_direction As Integer

        If clockwise Then
            angle_direction = 1
        Else
            angle_direction = -1
        End If

        If flag = 0 Or flag = 1 Then
            If downbasis Then
                start_angle = angle2
                sweep_angle = obj_selected.angle
            Else
                start_angle = 360 - angle2
                sweep_angle = obj_selected.angle
            End If
        ElseIf flag = -1 Then

            If downbasis Then
                start_angle = angle2
                sweep_angle = -1 * (obj_selected.angle)
            Else
                start_angle = 360 - angle2
                sweep_angle = -1 * (obj_selected.angle)
            End If
        End If
        Dim ang_dirc As Integer
        If flag >= 0 Then
            ang_dirc = 1
        Else
            ang_dirc = 2
        End If
        Dim angles = {start_angle, sweep_angle, ang_dirc}
        Return angles
    End Function

    ''' <summary>
    ''' draw temporal line when you are finializing current object.
    ''' </summary>
    ''' <paramname="pictureBox">The pictureBox control in which you want to draw object list.</param>
    ''' <paramname="obj_selected">The object which you are going to draw.</param>
    ''' <paramname="target_point">The point of mouse cursor.</param>
    ''' <paramname="side_drag">the flag which determines side dragging.</param>
    ''' <paramname="digit">the digit of decimal numbers.</param>
    ''' <paramname="CF">the factor of measuring scale.</param>
    ''' <paramname="draw_flag">the flag which determines drawing string.</param>
    <Extension()>
    Public Sub DrawTempFinal(ByVal pictureBox As PictureBox, ByRef obj_selected As MeasureObject, ByVal target_point As Point, ByVal side_drag As Boolean, ByVal digit As Integer, ByVal CF As Double, ByVal draw_flag As Boolean)
        Dim offset As Size = New Size()     'offset from draw point to line consists of start point and end point
        Dim offset2 As Size = New Size()      'offset from end point to final temp
        Dim graph As Graphics = pictureBox.CreateGraphics()
        Dim graphPen As Pen = New Pen(obj_selected.lineInfor.line_color, obj_selected.lineInfor.line_width)
        Dim graphFont = obj_selected.fontInfor.text_font
        Dim graphBrush As SolidBrush = New SolidBrush(obj_selected.fontInfor.font_color)

        Dim start_point As Point = New Point()
        Dim end_point As Point = New Point()
        Dim middle_point As Point = New Point()
        Dim draw_point As Point = New Point()

        Dim last_point As Point = New Point()
        Dim inter_pt As Point = New Point()

        start_point.X = CInt(obj_selected.startPoint.X * pictureBox.Width)
        start_point.Y = CInt(obj_selected.startPoint.Y * pictureBox.Height)
        middle_point.X = CInt(obj_selected.middlePoint.X * pictureBox.Width)
        middle_point.Y = CInt(obj_selected.middlePoint.Y * pictureBox.Height)
        end_point.X = CInt(obj_selected.endPoint.X * pictureBox.Width)
        end_point.Y = CInt(obj_selected.endPoint.Y * pictureBox.Height)
        draw_point.X = CInt(obj_selected.drawPoint.X * pictureBox.Width)
        draw_point.Y = CInt(obj_selected.drawPoint.Y * pictureBox.Height)

        last_point.X = CInt(obj_selected.lastPoint.X * pictureBox.Width)
        last_point.Y = CInt(obj_selected.lastPoint.Y * pictureBox.Height)
        inter_pt.X = CInt(obj_selected.commonPoint.X * pictureBox.Width)
        inter_pt.Y = CInt(obj_selected.commonPoint.Y * pictureBox.Height)

        If obj_selected.measuringType = MeasureType.lineAlign OrElse obj_selected.measuringType = MeasureType.lineHorizontal OrElse obj_selected.measuringType = MeasureType.lineVertical OrElse obj_selected.measuringType = MeasureType.lineParallel OrElse obj_selected.measuringType = MeasureType.ptToLine OrElse obj_selected.measuringType = MeasureType.lineFixed Then
            If (obj_selected.measuringType = MeasureType.lineParallel OrElse obj_selected.measuringType = MeasureType.ptToLine) AndAlso obj_selected.itemSet < 3 Then Return
            If obj_selected.itemSet < 2 Then Return
            'pictureBox.Refresh();

            Dim nor_point1 As Point = New Point()
            Dim nor_point2 As Point = New Point()
            If obj_selected.measuringType = MeasureType.lineAlign OrElse obj_selected.measuringType = MeasureType.lineFixed Then
                offset = GetNormalFromPointToLine(start_point, end_point, target_point)
                nor_point1.X = start_point.X - offset.Width
                nor_point1.Y = start_point.Y - offset.Height
                nor_point2.X = end_point.X - offset.Width
                nor_point2.Y = end_point.Y - offset.Height
            ElseIf obj_selected.measuringType = MeasureType.lineHorizontal Then
                nor_point1.X = start_point.X
                nor_point1.Y = If(target_point.Y + 10 < pictureBox.Height, target_point.Y + 10, target_point.Y)
                nor_point2.X = end_point.X
                nor_point2.Y = If(target_point.Y + 10 < pictureBox.Height, target_point.Y + 10, target_point.Y)
            ElseIf obj_selected.measuringType = MeasureType.lineVertical Then
                nor_point1.X = If(target_point.X + 10 < pictureBox.Width, target_point.X + 10, target_point.X)
                nor_point1.Y = start_point.Y
                nor_point2.X = If(target_point.X + 10 < pictureBox.Width, target_point.X + 10, target_point.X)
                nor_point2.Y = end_point.Y
            ElseIf obj_selected.measuringType = MeasureType.lineParallel Then
                nor_point1 = middle_point
                offset = GetNormalFromPointToLine(start_point, middle_point, end_point)
                last_point.X = start_point.X - offset.Width
                last_point.Y = start_point.Y - offset.Height
                obj_selected.lastPoint = New PointF(CSng(start_point.X - offset.Width) / pictureBox.Width, CSng(start_point.Y - offset.Height) / pictureBox.Height)
                nor_point2 = New Point(middle_point.X - offset.Width, middle_point.Y - offset.Height)
            ElseIf obj_selected.measuringType = MeasureType.ptToLine Then
                nor_point1 = middle_point
                nor_point2 = end_point
            End If

            Dim nor_point3 As Point = New Point()
            Dim nor_point4 As Point = New Point()
            If obj_selected.measuringType = MeasureType.lineAlign OrElse obj_selected.measuringType = MeasureType.lineFixed Then
                offset2 = GetNormalToLineFixedLen(start_point, end_point, target_point, 10, True)

                nor_point3.X = nor_point1.X + offset2.Width
                nor_point3.Y = nor_point1.Y + offset2.Height
                nor_point4.X = nor_point2.X + offset2.Width
                nor_point4.Y = nor_point2.Y + offset2.Height
            ElseIf obj_selected.measuringType = MeasureType.lineHorizontal OrElse obj_selected.measuringType = MeasureType.lineVertical Then
                nor_point3 = nor_point1
                nor_point4 = nor_point2
            ElseIf obj_selected.measuringType = MeasureType.lineParallel Then
                Dim offset3 = GetNormalFromPointToLine(start_point, nor_point1, target_point)
                nor_point3.X = target_point.X + offset3.Width
                nor_point3.Y = target_point.Y + offset3.Height
                Dim X_max = Math.Max(start_point.X, nor_point1.X)
                Dim X_min = Math.Min(start_point.X, nor_point1.X)
                nor_point3.X = Math.Min(Math.Max(nor_point3.X, X_min), X_max)
                Dim Y_max = Math.Max(start_point.Y, nor_point1.Y)
                Dim Y_min = Math.Min(start_point.Y, nor_point1.Y)
                nor_point3.Y = Math.Min(Math.Max(nor_point3.Y, Y_min), Y_max)
                offset3 = GetNormalFromPointToLine(last_point, nor_point2, target_point)
                nor_point4.X = target_point.X + offset3.Width
                nor_point4.Y = target_point.Y + offset3.Height
                X_max = Math.Max(last_point.X, nor_point2.X)
                X_min = Math.Min(last_point.X, nor_point2.X)
                nor_point4.X = Math.Min(Math.Max(nor_point4.X, X_min), X_max)
                Y_max = Math.Max(last_point.Y, nor_point2.Y)
                Y_min = Math.Min(last_point.Y, nor_point2.Y)
                nor_point4.Y = Math.Min(Math.Max(nor_point4.Y, Y_min), Y_max)
            ElseIf obj_selected.measuringType = MeasureType.ptToLine Then
                nor_point3 = end_point
                offset = GetNormalFromPointToLine(start_point, middle_point, end_point)
                nor_point4.X = nor_point3.X + offset.Width
                nor_point4.Y = nor_point3.Y + offset.Height
            End If

            Dim Side_pt = nor_point4
            If side_drag Then
                Dim dist1 = CalcDistFromPointToLine(start_point, nor_point1, target_point)
                Dim dist2 As Double
                If obj_selected.measuringType = MeasureType.lineParallel Then
                    dist2 = CalcDistFromPointToLine(last_point, nor_point2, target_point)
                Else
                    dist2 = CalcDistFromPointToLine(end_point, nor_point2, target_point)
                End If

                Dim sum = CalcDistBetweenPoints(nor_point3, nor_point4)

                If obj_selected.measuringType = MeasureType.ptToLine Then
                    Side_pt.X = CInt((nor_point4.X - nor_point3.X) / sum * dist1 + nor_point4.X)
                    Side_pt.Y = CInt((nor_point4.Y - nor_point3.Y) / sum * dist1 + nor_point4.Y)
                Else
                    If dist1 > dist2 Then
                        Side_pt.X = CInt((nor_point4.X - nor_point3.X) / sum * dist2 + nor_point4.X)
                        Side_pt.Y = CInt((nor_point4.Y - nor_point3.Y) / sum * dist2 + nor_point4.Y)
                    Else
                        Side_pt.X = CInt((nor_point3.X - nor_point4.X) / sum * dist1 + nor_point3.X)
                        Side_pt.Y = CInt((nor_point3.Y - nor_point4.Y) / sum * dist1 + nor_point3.Y)
                    End If
                End If
            End If

            Dim n_dist As Integer = CalcDistBetweenPoints(nor_point3, nor_point4)
            n_dist /= 10
            n_dist = Math.Max(n_dist, 5)

            Dim arr_points = New Point(1) {}
            Dim arr_points2 = New Point(1) {}
            If obj_selected.measuringType = MeasureType.lineParallel OrElse obj_selected.measuringType = MeasureType.ptToLine Then
                arr_points = GetArrowPoints3(nor_point3, nor_point4, n_dist)
                arr_points2 = GetArrowPoints3(nor_point4, nor_point3, n_dist)
            Else
                arr_points = GetArrowPoints(start_point, end_point, nor_point1, nor_point3, n_dist)
                arr_points2 = GetArrowPoints(end_point, start_point, nor_point2, nor_point4, n_dist)
            End If

            'draw text
            Dim ang_tran As Integer = obj_selected.angle
            Dim attri = -1
            If obj_selected.angle > 90 Then
                ang_tran = 180 - ang_tran
                attri = 1
            End If

            'set limit to target point
            Dim draw_pt = target_point
            If obj_selected.measuringType = MeasureType.lineParallel Then
                Dim dist = CalcDistBetweenPoints(start_point, nor_point1)
                draw_pt.X += CInt((nor_point1.X - start_point.X) / dist * 20)
                draw_pt.Y += CInt((nor_point1.Y - start_point.Y) / dist * 20)
                Dim X_min = Math.Min(Math.Min(Math.Min(start_point.X, last_point.X), nor_point1.X), nor_point2.X)
                Dim X_max = Math.Max(Math.Max(Math.Max(start_point.X, last_point.X), nor_point1.X), nor_point2.X)
                Dim Y_min = Math.Min(Math.Min(Math.Min(start_point.Y, last_point.Y), nor_point1.Y), nor_point2.Y)
                Dim Y_max = Math.Max(Math.Max(Math.Max(start_point.Y, last_point.Y), nor_point1.Y), nor_point2.Y)
                draw_pt.X = Math.Min(Math.Max(draw_pt.X, X_min), X_max)
                draw_pt.Y = Math.Min(Math.Max(draw_pt.Y, Y_min), Y_max)
            ElseIf obj_selected.measuringType = MeasureType.ptToLine Then
                Dim offset3 = GetNormalFromPointToLine(nor_point3, nor_point4, target_point, 10)
                If nor_point3.X = nor_point4.X Then
                    If nor_point3.Y > nor_point4.Y Then
                        draw_pt.Y = Math.Min(Math.Max(target_point.Y, nor_point4.Y), nor_point3.Y)
                    Else
                        draw_pt.Y = Math.Min(Math.Max(target_point.Y, nor_point3.Y), nor_point4.Y)
                    End If
                    draw_pt.X = GetXCoordinate(nor_point3, nor_point4, draw_pt)
                    draw_pt.X -= CInt(offset3.Width)
                    draw_pt.Y -= CInt(offset3.Height)
                Else
                    If nor_point3.X > nor_point4.X Then
                        draw_pt.X = Math.Min(Math.Max(target_point.X, nor_point4.X), nor_point3.X)
                    Else
                        draw_pt.X = Math.Min(Math.Max(target_point.X, nor_point3.X), nor_point4.X)
                    End If
                    draw_pt.Y = GetYCoordinate(nor_point3, nor_point4, draw_pt)
                    draw_pt.X -= CInt(offset3.Width)
                    draw_pt.Y -= CInt(offset3.Height)
                End If
            Else
                If obj_selected.measuringType <> MeasureType.lineHorizontal Then
                    If nor_point1.Y > nor_point2.Y Then
                        draw_pt.Y = Math.Min(Math.Max(target_point.Y, nor_point2.Y), nor_point1.Y)
                    Else
                        draw_pt.Y = Math.Min(Math.Max(target_point.Y, nor_point1.Y), nor_point2.Y)
                    End If
                End If
                If obj_selected.measuringType <> MeasureType.lineVertical Then
                    If nor_point1.X > nor_point2.X Then
                        draw_pt.X = Math.Min(Math.Max(target_point.X, nor_point2.X), nor_point1.X)
                    Else
                        draw_pt.X = Math.Min(Math.Max(target_point.X, nor_point1.X), nor_point2.X)
                    End If
                End If
            End If

            If side_drag Then
                offset2 = GetNormalToLineFixedLen(nor_point3, nor_point4, target_point, 10, True)
                draw_pt.X = Side_pt.X - offset2.Width
                draw_pt.Y = Side_pt.Y - offset2.Height

            End If

            If draw_flag Then
                If obj_selected.measuringType = MeasureType.lineParallel Then end_point = last_point
                graph.DrawLine(graphPen, start_point, nor_point1)
                graph.DrawLine(graphPen, end_point, nor_point2)
                graph.DrawLine(graphPen, nor_point3, nor_point4)
                graph.DrawLine(graphPen, nor_point3, arr_points(0))
                graph.DrawLine(graphPen, nor_point3, arr_points(1))
                graph.DrawLine(graphPen, nor_point4, arr_points2(0))
                graph.DrawLine(graphPen, nor_point4, arr_points2(1))
                graph.DrawLine(graphPen, nor_point4, Side_pt)

                graph.RotateTransform(attri * ang_tran)
                Dim trans_pt = GetRotationTransform(draw_pt, attri * ang_tran)
                Dim length_decimal = GetDecimalNumber(obj_selected.length, digit, CF)
                If obj_selected.measuringType = MeasureType.lineFixed Then
                    length_decimal = obj_selected.scaleObject.length
                End If
                Dim textSize As SizeF = graph.MeasureString(length_decimal.ToString(), graphFont)
                graph.DrawString(length_decimal.ToString(), graphFont, graphBrush, trans_pt.X - textSize.Width / 2, trans_pt.Y - textSize.Height / 2)
            End If

            'Initialize line objects

            If obj_selected.measuringType = MeasureType.lineFixed Then
                obj_selected.lineObject.nor_pt1 = New PointF(CSng(nor_point1.X) / pictureBox.Width, CSng(nor_point1.Y) / pictureBox.Height)
                obj_selected.lineObject.nor_pt3 = New PointF(CSng(nor_point3.X) / pictureBox.Width, CSng(nor_point3.Y) / pictureBox.Height)
                obj_selected.lineObject.nor_pt5 = New PointF(CSng(arr_points(0).X) / pictureBox.Width, CSng(arr_points(0).Y) / pictureBox.Height)
                obj_selected.lineObject.nor_pt6 = New PointF(CSng(arr_points(1).X) / pictureBox.Width, CSng(arr_points(1).Y) / pictureBox.Height)
                Dim deltaX = obj_selected.lineObject.nor_pt1.X
                Dim deltaY = obj_selected.lineObject.nor_pt1.Y
                obj_selected.lineObject.nor_pt2 = New PointF((CSng(nor_point2.X) / pictureBox.Width - deltaX) * CF, (CSng(nor_point2.Y) / pictureBox.Height - deltaY) * CF)
                deltaX = obj_selected.lineObject.nor_pt3.X
                deltaY = obj_selected.lineObject.nor_pt3.Y
                obj_selected.lineObject.nor_pt4 = New PointF((CSng(nor_point4.X) / pictureBox.Width - deltaX) * CF, (CSng(nor_point4.Y) / pictureBox.Height - deltaY) * CF)
                obj_selected.lineObject.nor_pt7 = New PointF((CSng(arr_points2(0).X) / pictureBox.Width - deltaX), (CSng(arr_points2(0).Y) / pictureBox.Height - deltaY))
                obj_selected.lineObject.nor_pt8 = New PointF((CSng(arr_points2(1).X) / pictureBox.Width - deltaX), (CSng(arr_points2(1).Y) / pictureBox.Height - deltaY))
                obj_selected.lineObject.draw_pt = New PointF((CSng(draw_pt.X) / pictureBox.Width - deltaX) * CF, (CSng(draw_pt.Y) / pictureBox.Height - deltaY) * CF)
                obj_selected.lineObject.trans_angle = Convert.ToInt32(attri * ang_tran)
                obj_selected.lineObject.side_drag = New PointF((CSng(Side_pt.X) / pictureBox.Width - deltaX) * CF, (CSng(Side_pt.Y) / pictureBox.Height - deltaY) * CF)
            Else
                obj_selected.lineObject.nor_pt1 = New PointF(CSng(nor_point1.X) / pictureBox.Width, CSng(nor_point1.Y) / pictureBox.Height)
                obj_selected.lineObject.nor_pt2 = New PointF(CSng(nor_point2.X) / pictureBox.Width, CSng(nor_point2.Y) / pictureBox.Height)
                obj_selected.lineObject.nor_pt3 = New PointF(CSng(nor_point3.X) / pictureBox.Width, CSng(nor_point3.Y) / pictureBox.Height)
                obj_selected.lineObject.nor_pt4 = New PointF(CSng(nor_point4.X) / pictureBox.Width, CSng(nor_point4.Y) / pictureBox.Height)
                obj_selected.lineObject.nor_pt5 = New PointF(CSng(arr_points(0).X) / pictureBox.Width, CSng(arr_points(0).Y) / pictureBox.Height)
                obj_selected.lineObject.nor_pt6 = New PointF(CSng(arr_points(1).X) / pictureBox.Width, CSng(arr_points(1).Y) / pictureBox.Height)
                obj_selected.lineObject.nor_pt7 = New PointF(CSng(arr_points2(0).X) / pictureBox.Width, CSng(arr_points2(0).Y) / pictureBox.Height)
                obj_selected.lineObject.nor_pt8 = New PointF(CSng(arr_points2(1).X) / pictureBox.Width, CSng(arr_points2(1).Y) / pictureBox.Height)
                obj_selected.lineObject.draw_pt = New PointF(CSng(draw_pt.X) / pictureBox.Width, CSng(draw_pt.Y) / pictureBox.Height)
                obj_selected.lineObject.trans_angle = Convert.ToInt32(attri * ang_tran)
                obj_selected.lineObject.side_drag = New PointF(CSng(Side_pt.X) / pictureBox.Width, CSng(Side_pt.Y) / pictureBox.Height)
            End If

        ElseIf obj_selected.measuringType = MeasureType.angle OrElse obj_selected.measuringType = MeasureType.angleFixed Then
            If obj_selected.measuringType = MeasureType.angle And obj_selected.itemSet < 3 Then Return
            If obj_selected.measuringType = MeasureType.angleFixed And obj_selected.itemSet < 2 Then Return

            Dim angles As Double()
            If obj_selected.measuringType = MeasureType.angle Then
                angles = CalcStartAndSweepAngle(obj_selected, start_point, middle_point, end_point, target_point)
            Else
                angles = CalcStartAndSweepAngleFixed(obj_selected, start_point, middle_point, target_point)
            End If

            Dim start_angle, sweep_angle As Double
            start_angle = angles(0)
            sweep_angle = angles(1)
            Dim ang_dirc As Integer = angles(2)
            Dim clockwise As Boolean
            If ang_dirc = 1 Then
                clockwise = True
            Else
                clockwise = False
            End If

            Dim radius = Convert.ToInt32(Math.Sqrt(Math.Pow(target_point.X - middle_point.X, 2) + Math.Pow(target_point.Y - middle_point.Y, 2)))
            Dim radius2 = If(radius - 10 > 1, radius - 10, 1)
            Dim centerpt = middle_point

            Dim first_point = CalcPositionInCircle(centerpt, radius, start_angle)
            Dim second_point = CalcPositionInCircle(centerpt, radius, start_angle + sweep_angle)

            Dim nor_point1 = CalcPositionInCircle(centerpt, radius2, start_angle)
            Dim nor_point4 = CalcPositionInCircle(centerpt, radius2, start_angle + sweep_angle)

            Dim arr_points = New Point(1) {}
            Dim arr_points2 = New Point(1) {}

            Dim dist As Integer = radius2 / 6
            dist = Math.Max(dist, 5)

            If clockwise Then
                arr_points = GetArrowPoints2(first_point, centerpt, nor_point1, dist)
                arr_points2 = GetArrowPoints2(centerpt, second_point, nor_point4, dist)
            Else
                arr_points = GetArrowPoints2(centerpt, first_point, nor_point1, dist)
                arr_points2 = GetArrowPoints2(second_point, centerpt, nor_point4, dist)
            End If

            Dim Side_pt = nor_point1
            Dim side_index = 1
            Dim draw_pt = CorrectDisplayPosition(target_point, start_point, middle_point, clockwise)
            Dim angle3 As Double = CalcAngleBetweenTwoLines(New Point(middle_point.X + 10, middle_point.Y), middle_point, target_point)
            If side_drag Then
                Dim offset3 As SizeF
                Dim offset4 As SizeF

                Dim angle = CalcAngleBetweenTwoLines(start_point, middle_point, target_point)
                angle = angle * 360 / Math.PI / 2

                If angle > 0 AndAlso angle < obj_selected.angle Then
                    offset3 = GetNormalFromPointToLine(second_point, middle_point, target_point, 50)
                    Side_pt.X = nor_point4.X + CInt(offset3.Width)
                    Side_pt.Y = nor_point4.Y + CInt(offset3.Height)
                    'graph.DrawLine(graphPen, nor_point4, Side_pt);
                    angle3 = CalcAngleBetweenTwoLines(New Point(middle_point.X + 10, middle_point.Y), middle_point, nor_point4)
                    draw_pt = Side_pt
                    offset4 = GetUnitVector(middle_point, nor_point4)
                    draw_pt.X += CInt(offset4.Width * 15)
                    draw_pt.Y += CInt(offset4.Height * 15)
                    side_index = 4
                Else
                    offset3 = GetNormalFromPointToLine(first_point, middle_point, target_point, 50)
                    Side_pt.X = nor_point1.X + CInt(offset3.Width)
                    Side_pt.Y = nor_point1.Y + CInt(offset3.Height)
                    'graph.DrawLine(graphPen, nor_point1, Side_pt);
                    angle3 = CalcAngleBetweenTwoLines(New Point(middle_point.X + 10, middle_point.Y), middle_point, nor_point1)
                    draw_pt = Side_pt
                    offset4 = GetUnitVector(middle_point, nor_point1)
                    draw_pt.X += CInt(offset4.Width * 15)
                    draw_pt.Y += CInt(offset4.Height * 15)
                End If

            End If

            'draw text

            Dim attri = -1
            If CheckAngleDirection(New Point(middle_point.X + 10, middle_point.Y), middle_point, target_point) Then
                If angle3 > Math.PI / 2 Then
                    angle3 -= Math.PI / 2
                    attri = 1
                Else
                    angle3 = Math.PI / 2 - angle3
                    attri = -1
                End If
            Else
                If angle3 > Math.PI / 2 Then
                    angle3 -= Math.PI / 2
                    attri = -1
                Else
                    angle3 = Math.PI / 2 - angle3
                    attri = 1
                End If
            End If

            If draw_flag Then
                graph.DrawArc(graphPen, New Rectangle(middle_point.X - radius2, middle_point.Y - radius2, radius2 * 2, radius2 * 2), CSng(start_angle), CSng(sweep_angle))
                graph.DrawLine(graphPen, middle_point, first_point)
                graph.DrawLine(graphPen, middle_point, second_point)
                graph.DrawLine(graphPen, nor_point1, arr_points(0))
                graph.DrawLine(graphPen, nor_point1, arr_points(1))
                graph.DrawLine(graphPen, nor_point4, arr_points2(0))
                graph.DrawLine(graphPen, nor_point4, arr_points2(1))
                If side_index = 1 Then
                    graph.DrawLine(graphPen, nor_point1, Side_pt)
                Else
                    graph.DrawLine(graphPen, nor_point4, Side_pt)
                End If

                angle3 = angle3 * 360 / (2 * Math.PI)
                graph.RotateTransform(attri * angle3)
                Dim trans_pt = GetRotationTransform(draw_pt, attri * angle3)

                Dim length_decimal = GetDecimalNumber(Math.Abs(sweep_angle), digit, 1)
                Dim textSize As SizeF = graph.MeasureString(length_decimal.ToString(), graphFont)
                graph.DrawString(length_decimal.ToString(), graphFont, graphBrush, trans_pt.X - textSize.Width / 2, trans_pt.Y - textSize.Height / 2)
            End If


            'initialize the angle object
            obj_selected.angleObject.radius = CSng(radius2) / pictureBox.Width
            'obj_selected.angle = sweep_angle;
            obj_selected.angleObject.start_pt = New PointF(CSng(first_point.X) / pictureBox.Width, CSng(first_point.Y) / pictureBox.Height)
            obj_selected.angleObject.end_pt = New PointF(CSng(second_point.X) / pictureBox.Width, CSng(second_point.Y) / pictureBox.Height)
            If obj_selected.measuringType = MeasureType.angleFixed Then
                obj_selected.endPoint = obj_selected.angleObject.end_pt
            End If
            obj_selected.angleObject.nor_pt1 = New PointF(CSng(nor_point1.X) / pictureBox.Width, CSng(nor_point1.Y) / pictureBox.Height)
            obj_selected.angleObject.nor_pt4 = New PointF(CSng(nor_point4.X) / pictureBox.Width, CSng(nor_point4.Y) / pictureBox.Height)
            obj_selected.angleObject.nor_pt2 = New PointF(CSng(arr_points(0).X) / pictureBox.Width, CSng(arr_points(0).Y) / pictureBox.Height)
            obj_selected.angleObject.nor_pt3 = New PointF(CSng(arr_points(1).X) / pictureBox.Width, CSng(arr_points(1).Y) / pictureBox.Height)
            obj_selected.angleObject.nor_pt5 = New PointF(CSng(arr_points2(0).X) / pictureBox.Width, CSng(arr_points2(0).Y) / pictureBox.Height)
            obj_selected.angleObject.nor_pt6 = New PointF(CSng(arr_points2(1).X) / pictureBox.Width, CSng(arr_points2(1).Y) / pictureBox.Height)
            obj_selected.angleObject.start_angle = start_angle
            obj_selected.angleObject.sweep_angle = sweep_angle
            obj_selected.angleObject.draw_pt = New PointF(CSng(draw_pt.X) / pictureBox.Width, CSng(draw_pt.Y) / pictureBox.Height)
            obj_selected.angleObject.trans_angle = Convert.ToInt32(attri * angle3)
            obj_selected.angleObject.side_drag = New PointF(CSng(Side_pt.X) / pictureBox.Width, CSng(Side_pt.Y) / pictureBox.Height)
            obj_selected.angleObject.side_index = side_index

            Dim x_set = {obj_selected.middlePoint.X, obj_selected.startPoint.X, obj_selected.endPoint.X}
            Dim y_set = {obj_selected.middlePoint.Y, obj_selected.startPoint.Y, obj_selected.endPoint.Y}
            obj_selected.leftTop.X = GetMinimumInSet(x_set)
            obj_selected.leftTop.Y = GetMinimumInSet(y_set)
            obj_selected.rightBottom.X = GetMaximumInSet(x_set)
            obj_selected.rightBottom.Y = GetMaximumInSet(y_set)
        ElseIf obj_selected.measuringType = MeasureType.arc Then
            If obj_selected.itemSet < 3 Then Return

            Dim A = start_point
            Dim B = middle_point
            Dim C = end_point
            Dim d_AB = Math.Sqrt(Math.Pow(B.X - A.X, 2.0R) + Math.Pow(B.Y - A.Y, 2.0R))
            Dim d_BC = Math.Sqrt(Math.Pow(B.X - C.X, 2.0R) + Math.Pow(B.Y - C.Y, 2.0R))
            Dim d_AC = Math.Sqrt(Math.Pow(C.X - A.X, 2.0R) + Math.Pow(C.Y - A.Y, 2.0R))
            If d_AB + d_BC < d_AC + 0.2R And d_AB + d_BC > d_AC - 0.2R Then
                graph.DrawLine(graphPen, A, B)
                graph.DrawLine(graphPen, B, C)
            Else
                Dim t = New Triangle(New Point3d(start_point.X, start_point.Y, 0R), New Point3d(middle_point.X, middle_point.Y, 0R), New Point3d(end_point.X, end_point.Y, 0R))
                Dim angle_a = t.Angle_A * 360.0R / Math.PI
                Dim angle_b = t.Angle_B * 360.0R / Math.PI
                Dim angle_c = t.Angle_C * 360.0R / Math.PI
                Dim circumcenterpt = t.Circumcenter
                Dim centerpt = New Point(Convert.ToInt32(circumcenterpt.X), Convert.ToInt32(circumcenterpt.Y))
                Dim radius = Convert.ToInt32(t.Circumcircle.R)
                Dim right_cenerpt = New Point(centerpt.X + radius, centerpt.Y)
                Dim M = New PointF(right_cenerpt.X / CSng(pictureBox.Width), right_cenerpt.Y / CSng(pictureBox.Height))
                Dim item = sorting_Points(obj_selected, M)
                A = New Point(item.startPoint.X * pictureBox.Width, item.startPoint.Y * pictureBox.Height)
                B = New Point(item.middlePoint.X * pictureBox.Width, item.middlePoint.Y * pictureBox.Height)
                C = New Point(item.endPoint.X * pictureBox.Width, item.endPoint.Y * pictureBox.Height)


                If centerpt.Y = C.Y Then
                    Return
                End If
                Dim p_t = New Triangle(New Point3d(centerpt.X, centerpt.Y, 0R), New Point3d(C.X, C.Y, 0R), New Point3d(centerpt.X + radius, centerpt.Y, 0R))
                Dim rotate_angle = Convert.ToInt32(angle_a + angle_c)
                Dim add_a = 0
                If A.Y < right_cenerpt.Y And B.Y < right_cenerpt.Y And C.Y < right_cenerpt.Y Then
                    If B.X > A.X And B.X > C.X Or B.X < A.X And B.X < C.X Or B.X < A.X And B.X > C.X Then
                        add_a = -Convert.ToInt32(p_t.Angle_A * 180.0R / Math.PI)
                    End If
                ElseIf A.Y < right_cenerpt.Y And B.Y > right_cenerpt.Y And C.Y < right_cenerpt.Y Then
                    add_a = -Convert.ToInt32(p_t.Angle_A * 180.0R / Math.PI)
                ElseIf A.Y > right_cenerpt.Y And B.Y < right_cenerpt.Y And C.Y < right_cenerpt.Y Then
                    add_a = -Convert.ToInt32(p_t.Angle_A * 180.0R / Math.PI)
                ElseIf A.Y < right_cenerpt.Y And B.Y < right_cenerpt.Y And C.Y > right_cenerpt.Y Then
                    add_a = Convert.ToInt32(p_t.Angle_A * 180.0R / Math.PI)
                ElseIf A.Y > right_cenerpt.Y And B.Y < right_cenerpt.Y And C.Y > right_cenerpt.Y Then
                    add_a = Convert.ToInt32(p_t.Angle_A * 180.0R / Math.PI)
                ElseIf A.Y > right_cenerpt.Y And B.Y > right_cenerpt.Y And C.Y > right_cenerpt.Y Then
                    add_a = Convert.ToInt32(p_t.Angle_A * 180.0R / Math.PI)
                ElseIf A.Y < right_cenerpt.Y And B.Y > right_cenerpt.Y And C.Y > right_cenerpt.Y Then
                    add_a = Convert.ToInt32(p_t.Angle_A * 180.0R / Math.PI)
                ElseIf A.Y > right_cenerpt.Y And B.Y > right_cenerpt.Y And C.Y < right_cenerpt.Y Then
                    add_a = -Convert.ToInt32(p_t.Angle_A * 180.0R / Math.PI)
                End If
                'graph.DrawArc(graphPen, new Rectangle(centerpt.X - radius, centerpt.Y - radius, radius * 2, radius * 2), add_a, rotate_angle);

                Dim basis_pt As Point = New Point(centerpt.X + 10, centerpt.Y)
                'radius between x-axis and mouse cursor
                Dim angle_mpt = CalcAngleBetweenTwoLines(basis_pt, centerpt, target_point)
                Dim angle_mpt_deg = Convert.ToInt32(angle_mpt * 360 / Math.PI / 2)

                Dim clockwise = CheckAngleDirection(basis_pt, centerpt, target_point)
                If Not clockwise Then angle_mpt_deg = -1 * angle_mpt_deg
                Dim pt_circle = CalcPositionInCircle(centerpt, radius, angle_mpt_deg)
                Dim draw_pt = CalcPositionInCircle(centerpt, radius + 15, angle_mpt_deg)

                'graph.DrawLine(graphPen, centerpt, pt_circle);

                'draw arrows
                Dim dist As Integer = radius / 5
                dist = Math.Min(Math.Max(dist, 5), 20)
                Dim arr_points = GetArrowPoints3(centerpt, pt_circle, dist)
                Dim arr_points2 = GetArrowPoints3(pt_circle, centerpt, dist)

                'draw string
                Dim attri = -1
                If clockwise Then
                    If angle_mpt > Math.PI / 2 Then
                        angle_mpt -= Math.PI / 2
                        attri = 1
                    Else
                        angle_mpt = Math.PI / 2 - angle_mpt
                        attri = -1
                    End If
                Else
                    If angle_mpt > Math.PI / 2 Then
                        angle_mpt -= Math.PI / 2
                        attri = -1
                    Else
                        angle_mpt = Math.PI / 2 - angle_mpt
                        attri = 1
                    End If
                End If


                If draw_flag Then
                    graph.DrawArc(graphPen, New Rectangle(centerpt.X - radius, centerpt.Y - radius, radius * 2, radius * 2), add_a, rotate_angle)

                    graph.DrawLine(graphPen, centerpt, pt_circle)
                    graph.DrawLine(graphPen, centerpt, arr_points(0))
                    graph.DrawLine(graphPen, centerpt, arr_points(1))
                    graph.DrawLine(graphPen, pt_circle, arr_points2(0))
                    graph.DrawLine(graphPen, pt_circle, arr_points2(1))

                    angle_mpt_deg = Convert.ToInt32(angle_mpt * 360 / (2 * Math.PI))
                    graph.RotateTransform(attri * angle_mpt_deg)
                    Dim trans_pt = GetRotationTransform(draw_pt, attri * angle_mpt_deg)

                    Dim length_decimal = GetDecimalNumber(radius, digit, CF)
                    Dim textSize As SizeF = graph.MeasureString(length_decimal.ToString(), graphFont)
                    graph.DrawString(length_decimal.ToString(), graphFont, graphBrush, trans_pt.X - textSize.Width / 2, trans_pt.Y - textSize.Height / 2)
                End If


                'initialize the radius object
                obj_selected.arcObject.center_pt = New PointF(CSng(centerpt.X) / pictureBox.Width, CSng(centerpt.Y) / pictureBox.Height)
                obj_selected.arcObject.circle_pt = New PointF(CSng(pt_circle.X) / pictureBox.Width, CSng(pt_circle.Y) / pictureBox.Height)
                obj_selected.arcObject.arr_pt1 = New PointF(CSng(arr_points(0).X) / pictureBox.Width, CSng(arr_points(0).Y) / pictureBox.Height)
                obj_selected.arcObject.arr_pt2 = New PointF(CSng(arr_points(1).X) / pictureBox.Width, CSng(arr_points(1).Y) / pictureBox.Height)
                obj_selected.arcObject.arr_pt3 = New PointF(CSng(arr_points2(0).X) / pictureBox.Width, CSng(arr_points2(0).Y) / pictureBox.Height)
                obj_selected.arcObject.arr_pt4 = New PointF(CSng(arr_points2(1).X) / pictureBox.Width, CSng(arr_points2(1).Y) / pictureBox.Height)
                obj_selected.arcObject.draw_pt = New PointF(CSng(draw_pt.X) / pictureBox.Width, CSng(draw_pt.Y) / pictureBox.Height)
                obj_selected.arcObject.trans_angle = attri * angle_mpt_deg
            End If
        ElseIf obj_selected.measuringType = MeasureType.arcFixed Then
            If obj_selected.itemSet < 1 Then Return

            Dim centerpt = start_point
            Dim basis_pt As Point = New Point(centerpt.X + 10, centerpt.Y)
            Dim angle_mpt = CalcAngleBetweenTwoLines(basis_pt, centerpt, target_point)
            Dim angle_mpt_deg = Convert.ToInt32(angle_mpt * 360 / Math.PI / 2)

            Dim clockwise = CheckAngleDirection(basis_pt, centerpt, target_point)
            If Not clockwise Then angle_mpt_deg = -1 * angle_mpt_deg
            Dim radius = CInt(obj_selected.arc / CF * pictureBox.Width)
            Dim pt_circle = CalcPositionInCircle(centerpt, radius, angle_mpt_deg)
            Dim draw_pt = CalcPositionInCircle(centerpt, radius + 15, angle_mpt_deg)

            Dim dist As Integer = radius / 5
            dist = Math.Min(Math.Max(dist, 5), 20)
            Dim arr_points = GetArrowPoints3(centerpt, pt_circle, dist)
            Dim arr_points2 = GetArrowPoints3(pt_circle, centerpt, dist)

            Dim attri = -1
            If clockwise Then
                If angle_mpt > Math.PI / 2 Then
                    angle_mpt -= Math.PI / 2
                    attri = 1
                Else
                    angle_mpt = Math.PI / 2 - angle_mpt
                    attri = -1
                End If
            Else
                If angle_mpt > Math.PI / 2 Then
                    angle_mpt -= Math.PI / 2
                    attri = -1
                Else
                    angle_mpt = Math.PI / 2 - angle_mpt
                    attri = 1
                End If
            End If


            If draw_flag Then
                graph.DrawArc(graphPen, New Rectangle(centerpt.X - radius, centerpt.Y - radius, radius * 2, radius * 2), 0, 360)

                graph.DrawLine(graphPen, centerpt, pt_circle)
                graph.DrawLine(graphPen, centerpt, arr_points(0))
                graph.DrawLine(graphPen, centerpt, arr_points(1))
                graph.DrawLine(graphPen, pt_circle, arr_points2(0))
                graph.DrawLine(graphPen, pt_circle, arr_points2(1))

                angle_mpt_deg = Convert.ToInt32(angle_mpt * 360 / (2 * Math.PI))
                graph.RotateTransform(attri * angle_mpt_deg)
                Dim trans_pt = GetRotationTransform(draw_pt, attri * angle_mpt_deg)

                Dim length_decimal = GetDecimalNumber(radius, digit, CF)
                Dim textSize As SizeF = graph.MeasureString(length_decimal.ToString(), graphFont)
                graph.DrawString(length_decimal.ToString(), graphFont, graphBrush, trans_pt.X - textSize.Width / 2, trans_pt.Y - textSize.Height / 2)
            End If


            'initialize the radius object
            obj_selected.arcObject.center_pt = New PointF(CSng(centerpt.X) / pictureBox.Width, CSng(centerpt.Y) / pictureBox.Height)
            Dim deltaX = obj_selected.arcObject.center_pt.X
            Dim deltaY = obj_selected.arcObject.center_pt.Y
            obj_selected.arcObject.circle_pt = New PointF((CSng(pt_circle.X) / pictureBox.Width - deltaX) * CF, (CSng(pt_circle.Y) / pictureBox.Height - deltaY) * CF)
            obj_selected.arcObject.arr_pt1 = New PointF((CSng(arr_points(0).X) / pictureBox.Width - deltaX) * CF, (CSng(arr_points(0).Y) / pictureBox.Height - deltaY) * CF)
            obj_selected.arcObject.arr_pt2 = New PointF((CSng(arr_points(1).X) / pictureBox.Width - deltaX) * CF, (CSng(arr_points(1).Y) / pictureBox.Height - deltaY) * CF)
            obj_selected.arcObject.arr_pt3 = New PointF((CSng(arr_points2(0).X) / pictureBox.Width - deltaX) * CF, (CSng(arr_points2(0).Y) / pictureBox.Height - deltaY) * CF)
            obj_selected.arcObject.arr_pt4 = New PointF((CSng(arr_points2(1).X) / pictureBox.Width - deltaX) * CF, (CSng(arr_points2(1).Y) / pictureBox.Height - deltaY) * CF)
            obj_selected.arcObject.draw_pt = New PointF((CSng(draw_pt.X) / pictureBox.Width - deltaX) * CF, (CSng(draw_pt.Y) / pictureBox.Height - deltaY) * CF)
            obj_selected.arcObject.trans_angle = attri * angle_mpt_deg

        ElseIf obj_selected.measuringType = MeasureType.annotation Then
            If obj_selected.itemSet < 1 Then Return
            graph.DrawString(obj_selected.annotation, graphFont, graphBrush, target_point.X, target_point.Y)
            Dim textSize = graph.MeasureString(obj_selected.annotation, graphFont)

            Dim left_top = target_point
            Dim right_top As Point = New Point(target_point.X + CInt(textSize.Width), target_point.Y)
            Dim left_bottom As Point = New Point(target_point.X, target_point.Y + CInt(textSize.Height))
            Dim right_bottom As Point = New Point(target_point.X + CInt(textSize.Width), target_point.Y + CInt(textSize.Height))
            graph.DrawLine(graphPen, left_top, right_top)
            graph.DrawLine(graphPen, right_top, right_bottom)
            graph.DrawLine(graphPen, right_bottom, left_bottom)
            graph.DrawLine(graphPen, left_bottom, left_top)
            Dim pt_array = {left_top, right_top, left_bottom, right_bottom}
            Dim near_pt = GetShortestPath(start_point, pt_array)
            graph.DrawLine(graphPen, start_point, near_pt)

            'initialize the anno object
            obj_selected.annoObject.size.Width = CInt(textSize.Width)
            obj_selected.annoObject.size.Height = CInt(textSize.Height)
            obj_selected.leftTop = New PointF(CSng(left_top.X) / pictureBox.Width, CSng(left_top.Y) / pictureBox.Height)
            obj_selected.rightBottom = New PointF(CSng(right_bottom.X) / pictureBox.Width, CSng(right_bottom.Y) / pictureBox.Height)

            obj_selected.annoObject.line_pt = New PointF(CSng(near_pt.X) / pictureBox.Width, CSng(near_pt.Y) / pictureBox.Height)
        ElseIf obj_selected.measuringType = MeasureType.angle2Line Then
            If obj_selected.itemSet < 4 Then Return

            middle_point = inter_pt

            Dim angles = CalcStartAndSweepAngle(obj_selected, start_point, middle_point, end_point, target_point)
            Dim start_angle, sweep_angle As Double
            start_angle = angles(0)
            sweep_angle = angles(1)
            Dim ang_dirc As Integer = angles(2)
            Dim clockwise As Boolean
            If ang_dirc = 1 Then
                clockwise = True
            Else
                clockwise = False
            End If

            Dim radius = Convert.ToInt32(Math.Sqrt(Math.Pow(target_point.X - middle_point.X, 2) + Math.Pow(target_point.Y - middle_point.Y, 2)))
            Dim radius2 = If(radius - 10 > 1, radius - 10, 1)
            Dim centerpt = middle_point

            'draw arrows
            Dim first_point = CalcPositionInCircle(centerpt, radius, start_angle)
            Dim second_point = CalcPositionInCircle(centerpt, radius, start_angle + sweep_angle)

            Dim nor_point1 = CalcPositionInCircle(centerpt, radius2, start_angle)
            Dim nor_point4 = CalcPositionInCircle(centerpt, radius2, start_angle + sweep_angle)

            Dim arr_points = New Point(1) {}
            Dim arr_points2 = New Point(1) {}
            If clockwise Then
                arr_points = GetArrowPoints2(first_point, centerpt, nor_point1, 10)
                arr_points2 = GetArrowPoints2(centerpt, second_point, nor_point4, 10)
            Else
                arr_points = GetArrowPoints2(centerpt, first_point, nor_point1, 10)
                arr_points2 = GetArrowPoints2(second_point, centerpt, nor_point4, 10)
            End If

            graph.DrawLine(graphPen, nor_point1, arr_points(0))
            graph.DrawLine(graphPen, nor_point1, arr_points(1))

            graph.DrawLine(graphPen, nor_point4, arr_points2(0))
            graph.DrawLine(graphPen, nor_point4, arr_points2(1))

            'side dragging
            Dim Side_pt = nor_point1
            Dim side_index = 1
            Dim draw_pt = CorrectDisplayPosition(target_point, start_point, middle_point, clockwise)
            Dim angle3 As Double = CalcAngleBetweenTwoLines(New Point(middle_point.X + 10, middle_point.Y), middle_point, target_point)
            If side_drag Then
                Dim offset3 As SizeF
                Dim offset4 As SizeF

                Dim angle = CalcAngleBetweenTwoLines(start_point, middle_point, target_point)
                angle = angle * 360 / Math.PI / 2

                If angle > 0 AndAlso angle < obj_selected.angle Then
                    offset3 = GetNormalFromPointToLine(second_point, middle_point, target_point, 50)
                    Side_pt.X = nor_point4.X + CInt(offset3.Width)
                    Side_pt.Y = nor_point4.Y + CInt(offset3.Height)
                    graph.DrawLine(graphPen, nor_point4, Side_pt)
                    angle3 = CalcAngleBetweenTwoLines(New Point(middle_point.X + 10, middle_point.Y), middle_point, nor_point4)
                    draw_pt = Side_pt
                    offset4 = GetUnitVector(middle_point, nor_point4)
                    draw_pt.X += CInt(offset4.Width * 10)
                    draw_pt.Y += CInt(offset4.Height * 10)
                    side_index = 4
                Else
                    offset3 = GetNormalFromPointToLine(first_point, middle_point, target_point, 50)
                    Side_pt.X = nor_point1.X + CInt(offset3.Width)
                    Side_pt.Y = nor_point1.Y + CInt(offset3.Height)
                    graph.DrawLine(graphPen, nor_point1, Side_pt)
                    angle3 = CalcAngleBetweenTwoLines(New Point(middle_point.X + 10, middle_point.Y), middle_point, nor_point1)
                    draw_pt = Side_pt
                    offset4 = GetUnitVector(middle_point, nor_point1)
                    draw_pt.X += CInt(offset4.Width * 10)
                    draw_pt.Y += CInt(offset4.Height * 10)
                End If

            End If

            'draw text

            Dim attri = -1
            If CheckAngleDirection(New Point(middle_point.X + 10, middle_point.Y), middle_point, target_point) Then
                If angle3 > Math.PI / 2 Then
                    angle3 -= Math.PI / 2
                    attri = 1
                Else
                    angle3 = Math.PI / 2 - angle3
                    attri = -1
                End If
            Else
                If angle3 > Math.PI / 2 Then
                    angle3 -= Math.PI / 2
                    attri = -1
                Else
                    angle3 = Math.PI / 2 - angle3
                    attri = 1
                End If
            End If

            If draw_flag Then
                graph.DrawArc(graphPen, New Rectangle(middle_point.X - radius2, middle_point.Y - radius2, radius2 * 2, radius2 * 2), CSng(start_angle), CSng(sweep_angle))

                graph.DrawLine(graphPen, nor_point1, arr_points(0))
                graph.DrawLine(graphPen, nor_point1, arr_points(1))
                graph.DrawLine(graphPen, nor_point4, arr_points2(0))
                graph.DrawLine(graphPen, nor_point4, arr_points2(1))
                If side_index = 1 Then
                    graph.DrawLine(graphPen, nor_point1, Side_pt)
                Else
                    graph.DrawLine(graphPen, nor_point4, Side_pt)
                End If

                angle3 = angle3 * 360 / (2 * Math.PI)
                graph.RotateTransform(attri * angle3)
                Dim trans_pt = GetRotationTransform(draw_pt, attri * angle3)

                Dim length_decimal = GetDecimalNumber(Math.Abs(sweep_angle), digit, 1)
                Dim textSize As SizeF = graph.MeasureString(length_decimal.ToString(), graphFont)
                graph.DrawString(length_decimal.ToString(), graphFont, graphBrush, trans_pt.X - textSize.Width / 2, trans_pt.Y - textSize.Height / 2)
            End If

            'initialize the angle object
            obj_selected.angleObject.radius = CSng(radius2) / pictureBox.Width
            'obj_selected.angle = sweep_angle;
            obj_selected.angleObject.start_pt = New PointF(CSng(first_point.X) / pictureBox.Width, CSng(first_point.Y) / pictureBox.Height)
            obj_selected.angleObject.end_pt = New PointF(CSng(second_point.X) / pictureBox.Width, CSng(second_point.Y) / pictureBox.Height)
            obj_selected.angleObject.nor_pt1 = New PointF(CSng(nor_point1.X) / pictureBox.Width, CSng(nor_point1.Y) / pictureBox.Height)
            obj_selected.angleObject.nor_pt4 = New PointF(CSng(nor_point4.X) / pictureBox.Width, CSng(nor_point4.Y) / pictureBox.Height)
            obj_selected.angleObject.nor_pt2 = New PointF(CSng(arr_points(0).X) / pictureBox.Width, CSng(arr_points(0).Y) / pictureBox.Height)
            obj_selected.angleObject.nor_pt3 = New PointF(CSng(arr_points(1).X) / pictureBox.Width, CSng(arr_points(1).Y) / pictureBox.Height)
            obj_selected.angleObject.nor_pt5 = New PointF(CSng(arr_points2(0).X) / pictureBox.Width, CSng(arr_points2(0).Y) / pictureBox.Height)
            obj_selected.angleObject.nor_pt6 = New PointF(CSng(arr_points2(1).X) / pictureBox.Width, CSng(arr_points2(1).Y) / pictureBox.Height)
            obj_selected.angleObject.start_angle = start_angle
            obj_selected.angleObject.sweep_angle = sweep_angle
            obj_selected.angleObject.draw_pt = New PointF(CSng(draw_pt.X) / pictureBox.Width, CSng(draw_pt.Y) / pictureBox.Height)
            obj_selected.angleObject.trans_angle = Convert.ToInt32(attri * angle3)
            obj_selected.angleObject.side_drag = New PointF(CSng(Side_pt.X) / pictureBox.Width, CSng(Side_pt.Y) / pictureBox.Height)
            obj_selected.angleObject.side_index = side_index

            Dim x_set = {obj_selected.startPoint.X, obj_selected.middlePoint.X, obj_selected.endPoint.X, obj_selected.lastPoint.X}
            Dim y_set = {obj_selected.startPoint.Y, obj_selected.middlePoint.Y, obj_selected.endPoint.Y, obj_selected.lastPoint.Y}
            obj_selected.leftTop.X = GetMinimumInSet(x_set)
            obj_selected.leftTop.Y = GetMinimumInSet(y_set)
            obj_selected.rightBottom.X = GetMaximumInSet(x_set)
            obj_selected.rightBottom.Y = GetMaximumInSet(y_set)
        End If

        graph.Dispose()
        graphPen.Dispose()
    End Sub

    ''' <summary>
    ''' draw dotted rectangle.
    ''' </summary>
    ''' <paramname="pictureBox">The pictureBox control in which you want to draw object list.</param>
    ''' <paramname="FirstPtOfEdge">The left top corner of selected region.</param>
    ''' <paramname="SecondPtOfEdge">The right bottom corner of selected region.</param>
    <Extension()>
    Public Sub DrawRectangle(ByVal pictureBox As PictureBox, ByVal FirstPtOfEdge As Point, ByVal SecondPtOfEdge As Point)
        Dim graph As Graphics = pictureBox.CreateGraphics()
        Dim graphPen As Pen = New Pen(Color.Black, 1)

        graph.DrawRectangle(graphPen, New Rectangle(FirstPtOfEdge.X, FirstPtOfEdge.Y, SecondPtOfEdge.X - FirstPtOfEdge.X, SecondPtOfEdge.Y - FirstPtOfEdge.Y))

        graphPen.Dispose()
        graph.Dispose()
    End Sub

    Public Sub GetObjName(objectList As List(Of MeasureObject), ByRef objSelected As MeasureObject, unit As String)
        Dim L, A, An, Ar, S, C, P, CP, Pt As Integer
        L = 0 : A = 0 : An = 0 : Ar = 0 : S = 0 : C = 0 : P = 0 : CP = 0 : Pt = 0

        For Each item In objectList
            Select Case item.measuringType
                Case MeasureType.lineAlign
                    L += 1
                Case MeasureType.lineHorizontal
                    L += 1
                Case MeasureType.lineVertical
                    L += 1
                Case MeasureType.lineParallel
                    L += 1
                Case MeasureType.ptToLine
                    L += 1
                Case MeasureType.angle
                    An += 1
                Case MeasureType.angle2Line
                    An += 1
                Case MeasureType.arc
                    Ar += 1
                Case MeasureType.annotation
                    A += 1
                Case MeasureType.pencil
                    L += 1
                Case MeasureType.measureScale
                    S += 1
                Case MeasureType.objMinMax
                    L += 1
                Case MeasureType.lineFixed
                    L += 1
                Case MeasureType.arcFixed
                    Ar += 1
                Case MeasureType.angleFixed
                    An += 1
                Case MeasureType.objCuPoly
                    CP += 1
                Case MeasureType.objCurve
                    C += 1
                Case MeasureType.objLine
                    L += 1
                Case MeasureType.objPoint
                    Pt += 1
                Case MeasureType.objPoly
                    P += 1
            End Select
        Next

        Select Case objSelected.measuringType
            Case MeasureType.lineAlign
                objSelected.name = "L" & L
            Case MeasureType.lineHorizontal
                objSelected.name = "L" & L
            Case MeasureType.lineVertical
                objSelected.name = "L" & L
            Case MeasureType.lineParallel
                objSelected.name = "L" & L
            Case MeasureType.ptToLine
                objSelected.name = "L" & L
            Case MeasureType.angle
                objSelected.name = "An" & An
            Case MeasureType.angle2Line
                objSelected.name = "An" & An
            Case MeasureType.arc
                objSelected.name = "Ar" & Ar
            Case MeasureType.annotation
                objSelected.name = "A" & A
            Case MeasureType.pencil
                objSelected.name = "L" & L
            Case MeasureType.measureScale
                objSelected.name = "S" & S
            Case MeasureType.objMinMax
                objSelected.name = "L" & L
            Case MeasureType.lineFixed
                objSelected.name = "L" & L
            Case MeasureType.arcFixed
                objSelected.name = "Ar" & Ar
            Case MeasureType.angleFixed
                objSelected.name = "An" & An
            Case MeasureType.objCuPoly
                objSelected.name = "CP" & CP
            Case MeasureType.objCurve
                objSelected.name = "C" & C
            Case MeasureType.objLine
                objSelected.name = "L" & L
            Case MeasureType.objPoint
                objSelected.name = "Pt" & Pt
            Case MeasureType.objPoly
                objSelected.name = "P" & P
        End Select
    End Sub
#End Region

#Region "DataGrid Methods"
    ''' <summary>
    ''' set the items of combobox column of datagridview
    ''' </summary>
    ''' <paramname="name">The string which is display.</param>
    ''' <paramname="name_list">The list of strings which are included in combobox item.</param>
    Public Function SetComboItemContent(name As String, ByVal name_list As List(Of String)) As DataGridViewComboBoxCell
        Dim cell As DataGridViewComboBoxCell = New DataGridViewComboBoxCell()
        Dim str_array As String() = New String(name_list.Count - 1) {}
        Dim cnt As Integer = 0
        If name <> "" Then
            str_array(cnt) = name
            cnt += 1
        End If

        For i = 0 To str_array.Length() - 1
            If name <> name_list(i) Then
                str_array(cnt) = name_list(i)
                cnt += 1
            End If
        Next
        cell.Items.AddRange(str_array)
        cell.Value = cell.Items(0)
        cell.ReadOnly = False
        Return cell
    End Function


    ''' <summary>
    ''' Loads all the data of objects.
    ''' </summary>
    ''' <paramname="listView">The list view to load objects on.</param>
    ''' <paramname="circles">The list of objects which you are going to load.</param>
    ''' <paramname="CF">The factor of measurig scale.</param>
    ''' <paramname="digit">The digit of decimal numbers.</param>
    ''' <paramname="unit">The unit in length.</param>
    ''' <paramname="name_list">The list of names which will included in combobox item.</param>
    <Extension()>
    Public Sub LoadObjectList(ByVal listView As DataGridView, ByVal object_list As List(Of MeasureObject), ByVal CF As Double, ByVal digit As Integer, ByVal unit As String, ByVal name_list As List(Of String))
        listView.Rows.Clear()
        If object_list.Count > 0 Then
            Dim i = 0
            Dim length As Double

            For Each item In object_list
                Dim str_item = New String(6) {}
                str_item(0) = (i + 1).ToString()
                str_item(1) = item.name
                str_item(2) = ""
                str_item(3) = item.parameter
                str_item(4) = item.spec
                str_item(5) = ""
                str_item(6) = item.judgement

                Select Case item.measuringType
                    Case MeasureType.lineAlign
                        length = GetDecimalNumber(item.length, digit, CF)
                        str_item(5) = length.ToString() & " " & unit

                    Case MeasureType.lineHorizontal
                        length = GetDecimalNumber(item.length, digit, CF)
                        str_item(5) = length.ToString() & " " & unit

                    Case MeasureType.lineVertical
                        length = GetDecimalNumber(item.length, digit, CF)
                        str_item(5) = length.ToString() & " " & unit

                    Case MeasureType.lineParallel
                        length = GetDecimalNumber(item.length, digit, CF)
                        str_item(5) = length.ToString() & " " & unit

                    Case MeasureType.ptToLine
                        length = GetDecimalNumber(item.length, digit, CF)
                        str_item(5) = length.ToString() & " " & unit

                    Case MeasureType.angle
                        length = GetDecimalNumber(item.angleObject.sweep_angle, digit, 1)
                        str_item(5) = length.ToString() & " degree"

                    Case MeasureType.angle2Line
                        length = GetDecimalNumber(item.angleObject.sweep_angle, digit, 1)
                        str_item(5) = length.ToString() & " degree"

                    Case MeasureType.arc
                        length = GetDecimalNumber(item.arc, digit, CF)
                        str_item(5) = length.ToString() & " " & unit

                    Case MeasureType.measureScale
                        length = GetDecimalNumber(item.length, digit, CF)
                        str_item(5) = length.ToString() & " " & unit

                    Case MeasureType.objMinMax
                        length = GetDecimalNumber(item.length, digit, CF)
                        str_item(5) = length.ToString() & " " & unit

                    Case MeasureType.lineFixed
                        length = item.scaleObject.length
                        str_item(5) = length.ToString() & " " & unit

                    Case MeasureType.arcFixed
                        length = GetDecimalNumber(item.scaleObject.length, digit, 1)
                        str_item(5) = length.ToString() & " " & unit

                    Case MeasureType.angleFixed
                        length = item.scaleObject.length
                        str_item(5) = length.ToString() & " degree"

                End Select

                listView.Rows.Add(str_item)
                listView.Rows(i).Cells(2) = SetComboItemContent(item.description, name_list)
                i += 1
            Next
        End If

    End Sub
#End Region


#Region "HScrollBar Methods"
    ''' <summary>
    ''' Display the HScrollBar value to the Label.
    ''' </summary>
    ''' <paramname="label">The label you want to display the value of HScrollBar.</param>
    ''' <paramname="hScrollBar">The hScrollBar whose value is changed by user.</param>

    <Extension()>
    Public Sub DisplayDataToLabel(ByVal label As Label, ByVal hScrollBar As HScrollBar)
        label.Text = "" & hScrollBar.Value
    End Sub
#End Region
End Module

