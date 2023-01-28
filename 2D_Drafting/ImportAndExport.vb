

Imports ClosedXML.Excel
Imports Emgu.CV
Imports Emgu.CV.CvEnum
Imports System.IO
Imports System.Runtime.CompilerServices

''' <summary>
''' This class contains all the functions for import and export.
''' </summary>
Public Module ImportAndExport

    ''' <summary>
    ''' Load Image to picture box from file that user choises.
    ''' </summary>
    ''' <paramname="pictureBox">The picture box to load image on.</param>
    ''' <paramname="filter">The filter to be used to get type of images. example ("PNG Files|*.png|BMP Files|*.bmp")</param>
    ''' <paramname="fileDialogTitle">The title for the dialog appears for the user.</param>
    ''' <paramname="origin_image">The list of original input image.</param>
    ''' <paramname="resized_image">The list of image which is resized to fit the picturebox control.</param>
    ''' <paramname="initial_ratio">The ratio of the size of original image and the size of picturebox control.</param>
    ''' <paramname="tab_index">The index of current tab page.</param>
    ''' <paramname="img_import_flag">The flag specify you can import image to target tag.</param>
    <Extension()>
    Public Function LoadImageFromFiles(ByVal pictureBox As PictureBox, ByVal filter As String, ByVal fileDialogTitle As String,
                                          ByRef origin_image_list As List(Of Mat), ByRef resized_image_list As List(Of Mat), ByRef current_image_list As List(Of Mat),
                                          ByRef initial_ratio As Single(), ByVal tab_index As Integer, ByVal img_import_flag As Boolean(),
                                       ByRef file_names As List(Of String)) As Integer
        Dim openFileDialog As OpenFileDialog = New OpenFileDialog()
        openFileDialog.Filter = filter
        openFileDialog.Title = fileDialogTitle

        If tab_index = 0 Then
            openFileDialog.Multiselect = True
        End If


        openFileDialog.FileName = ""
        Dim image_filepaths = New String(24) {}
        If openFileDialog.ShowDialog() = DialogResult.OK Then
            image_filepaths = openFileDialog.FileNames
        Else
            Return 0
        End If

        Dim picturebox_h = pictureBox.Height
        Dim picturebox_w = pictureBox.Width
        Dim i = tab_index
        Dim img_cnt = 0
        While img_cnt < image_filepaths.Length()
            If i > 24 Then Exit While

            If img_import_flag(i) = False Then
                i += 1
                Continue While
            End If

            Dim origin_image = CvInvoke.Imread(image_filepaths(img_cnt), ImreadModes.AnyColor)
            Dim resized_image As Mat = New Mat()
            Dim image_w = origin_image.Cols
            Dim image_h = origin_image.Rows

            Console.WriteLine("image_size:" & image_w.ToString() & ":" & image_h.ToString())

            'If image_w / image_h < picturebox_w / picturebox_h Then
            '    Dim s As Size = New Size(Convert.ToInt32(image_w * picturebox_h / image_h), picturebox_h)
            '    CvInvoke.Resize(origin_image, resized_image, s)
            '    initial_ratio(i) = CSng(picturebox_h) / image_h
            'Else
            '    Dim sz As Size = New Size(picturebox_w, Convert.ToInt32(image_h * picturebox_w / image_w))
            '    CvInvoke.Resize(origin_image, resized_image, sz)
            '    initial_ratio(i) = CSng(picturebox_w) / image_w
            'End If

            DisposeElemOfList(origin_image_list, origin_image, i)
            DisposeElemOfList(resized_image_list, origin_image.Clone(), i)
            DisposeElemOfList(current_image_list, origin_image.Clone(), i)

            file_names(i) = System.IO.Path.GetFileName(image_filepaths(img_cnt))
            i += 1
            img_cnt += 1
        End While
        Return img_cnt
    End Function

    ''' <summary>
    ''' Load Image to picture box from file that user choises.
    ''' </summary>
    ''' <paramname="pictureBox">The picture box to load image on.</param>
    ''' <paramname="filename">The picture box to load image on.</param>
    ''' <paramname="origin_image">The list of original input image.</param>
    ''' <paramname="resized_image">The list of image which is resized to fit the picturebox control.</param>
    ''' <paramname="initial_ratio">The ratio of the size of original image and the size of picturebox control.</param>
    ''' <paramname="tab_index">The index of current tab page.</param>
    <Extension()>
    Public Function LoadImageFromFile(ByVal pictureBox As PictureBox, ByVal filename As String,
                                          ByRef origin_image_list As List(Of Mat), ByRef resized_image_list As List(Of Mat), ByRef current_image_list As List(Of Mat),
                                          ByRef initial_ratio As Single(), ByVal tab_index As Integer, ByRef file_names As List(Of String))


        Dim picturebox_h = pictureBox.Height
        Dim picturebox_w = pictureBox.Width
        Dim i = tab_index

        Dim origin_image = CvInvoke.Imread(filename, ImreadModes.AnyColor)
        Dim resized_image As Mat = New Mat()
        Dim image_w = origin_image.Cols
        Dim image_h = origin_image.Rows

        Console.WriteLine("image_size:" & image_w.ToString() & ":" & image_h.ToString())

        'If image_w / image_h < picturebox_w / picturebox_h Then
        '    Dim s As Size = New Size(Convert.ToInt32(image_w * picturebox_h / image_h), picturebox_h)
        '    CvInvoke.Resize(origin_image, resized_image, s)
        '    initial_ratio(i) = CSng(picturebox_h) / image_h
        'Else
        '    Dim sz As Size = New Size(picturebox_w, Convert.ToInt32(image_h * picturebox_w / image_w))
        '    CvInvoke.Resize(origin_image, resized_image, sz)
        '    initial_ratio(i) = CSng(picturebox_w) / image_w
        'End If

        DisposeElemOfList(origin_image_list, origin_image, i)
        DisposeElemOfList(resized_image_list, origin_image.Clone(), i)
        DisposeElemOfList(current_image_list, origin_image.Clone(), i)

        file_names(i) = System.IO.Path.GetFileName(filename)

    End Function


    ''' <summary>
    ''' Save Image of picture box.
    ''' </summary>
    ''' <paramname="pictureBox">The picture box to save its image</param>
    ''' <paramname="filter">The filter to be used to get type of images. example ("PNG Files|*.png|BMP Files|*.bmp")</param>
    ''' <paramname="saveDialogTitle">The title for the save dialog appears for the user.</param>
    ''' <paramname="object_list">The list of objects which you are going to draw.</param>
    ''' <paramname="graphPen">The pen for drawing objects.</param>
    ''' <paramname="graphPen_line">The pen for drawing lines.</param>
    ''' <paramname="digit">The digit of decimal numbers.</param>
    ''' <paramname="CF">The factor of measuring scale.</param>
    <Extension()>
    Public Sub SaveImageInFile(ByVal pictureBox As PictureBox, ByVal filter As String, ByVal saveDialogTitle As String, ByVal object_list As List(Of MeasureObject), ByVal graphPen As Pen, ByVal graphPen_line As Pen, ByVal digit As Integer, ByVal CF As Double)

        Dim saveFileDialog As SaveFileDialog = New SaveFileDialog()

        saveFileDialog.Title = saveDialogTitle
        saveFileDialog.Filter = filter
        saveFileDialog.FileName = ""

        Dim img = pictureBox.Image
        If saveFileDialog.ShowDialog() = DialogResult.OK Then

            Dim result As Bitmap = New Bitmap(img.Width, img.Height)

            Using graph = Graphics.FromImage(result)
                Dim PointX = 0
                Dim PointY = 0
                Dim Width = img.Width
                Dim Height = img.Height
                graph.DrawImage(img, PointX, PointY, Width, Height)
                DrawObjList2(graph, pictureBox, object_list, graphPen, graphPen_line, digit, CF)
                graph.Flush()

            End Using

            result.Save(saveFileDialog.FileName)
        End If

    End Sub

    ''' <summary>
    ''' Save Image of picture box for Curves.
    ''' </summary>
    ''' <paramname="pictureBox">The picture box to save its image</param>
    ''' <paramname="filename">The filename of picture you are going to save.</param>
    ''' <paramname="object_list">The list of objects which you are going to draw.</param>
    ''' <paramname="graphPen">The pen for drawing objects.</param>
    ''' <paramname="graphPen_line">The pen for drawing lines.</param>
    ''' <paramname="digit">The digit of decimal numbers.</param>
    ''' <paramname="CF">The factor of measuring scale.</param>
    <Extension()>
    Public Sub SaveImageForCurves(ByVal pictureBox As PictureBox, ByVal filename As String, ByVal object_list As List(Of MeasureObject), ByVal graphPen As Pen, ByVal graphPen_line As Pen, ByVal digit As Integer, ByVal CF As Double)

        Dim img = pictureBox.Image
        Dim result As Bitmap = New Bitmap(img.Width, img.Height)

        Using graph = Graphics.FromImage(result)
            Dim PointX = 0
            Dim PointY = 0
            Dim Width = img.Width
            Dim Height = img.Height
            graph.DrawImage(img, PointX, PointY, Width, Height)
            DrawObjList2(graph, pictureBox, object_list, graphPen, graphPen_line, digit, CF)
            graph.Flush()

        End Using

        result.Save(filename)

    End Sub


    ''' <summary>
    ''' Save the information of objects to excel file.
    ''' </summary>
    ''' <paramname="picturebox">The picturbox contains image</param>
    ''' <paramname="obj_list_list">The list of list of objects</param>
    ''' <paramname="filter">The filter to be used to get type of images. example ("PNG Files|*.png|BMP Files|*.bmp")</param>
    ''' <paramname="saveDialogTitle">The title for the save dialog appears for the user.</param>
    ''' <paramname="CF">The factor of measuing scale.</param>
    ''' <paramname="digit">The number of decimals.</param>
    ''' <paramname="unit">The unit in length.</param>
    <Extension()>
    Public Sub SaveListToExcel(ByVal picturebox As PictureBox(), ByVal obj_list_list As List(Of List(Of MeasureObject)), ByVal filter As String, ByVal saveDialogTitle As String, ByVal CF As Double, ByVal digit As Integer, ByVal unit As String)
        Dim SaveFileDialog As SaveFileDialog = New SaveFileDialog()
        SaveFileDialog.Filter = filter
        SaveFileDialog.Title = saveDialogTitle

        Dim xlsx_savepath = ""
        If SaveFileDialog.ShowDialog() = DialogResult.OK Then
            xlsx_savepath = SaveFileDialog.FileName
        End If

        Dim listView As ListView = New ListView()
        listView.Columns.Add("Object")
        listView.Columns.Add("Length")
        listView.Columns.Add("Angle")
        listView.Columns.Add("Radius")
        listView.Columns.Add("Unit")
        listView.Columns.Add("Remarks")


        Using workbook = New XLWorkbook()
            For cnt = 0 To 24
                Dim img = picturebox(cnt).Image
                If img IsNot Nothing Then
                    listView.Clear()
                    listView.LoadObjectList(obj_list_list.ElementAt(cnt), CF, digit, unit)

                    Dim worksheet = workbook.Worksheets.Add("Result Sheet" & cnt.ToString())
                    worksheet.Cell("A1").Value = "Object"
                    worksheet.Cell("B1").Value = "Length"
                    worksheet.Cell("C1").Value = "Angle"
                    worksheet.Cell("D1").Value = "Radius"
                    worksheet.Cell("E1").Value = "Unit"
                    worksheet.Cell("F1").Value = "Remarks"
                    Dim row_count_listbox = listView.Items.Count
                    For i = 0 To row_count_listbox - 1
                        worksheet.Cell("A" & (i + 2).ToString()).Value = listView.Items(i).SubItems(0).Text
                        worksheet.Cell("B" & (i + 2).ToString()).Value = listView.Items(i).SubItems(1).Text
                        worksheet.Cell("C" & (i + 2).ToString()).Value = listView.Items(i).SubItems(2).Text
                        worksheet.Cell("D" & (i + 2).ToString()).Value = listView.Items(i).SubItems(3).Text
                        worksheet.Cell("E" & (i + 2).ToString()).Value = listView.Items(i).SubItems(4).Text
                        worksheet.Cell("F" & (i + 2).ToString()).Value = listView.Items(i).SubItems(5).Text
                    Next
                End If
            Next

            workbook.SaveAs(xlsx_savepath)
        End Using
    End Sub

    ''' <summary>
    ''' Save the information of objects and image to excel file.
    ''' </summary>
    ''' <paramname="picturebox">The picturbox contains image</param>
    ''' <paramname="filter">The filter to be used to get type of images. example ("PNG Files|*.png|BMP Files|*.bmp")</param>
    ''' <paramname="saveDialogTitle">The title for the save dialog appears for the user.</param>
    ''' <paramname="obj_list_list">The list of list of objects.</param>
    ''' <paramname="graphPen">The pen for objects.</param>
    ''' <paramname="graphPen_line">The pen for lines.</param>
    ''' <paramname="digit">The digit of decimal numbers.</param>
    ''' <paramname="CF">The factor of measuring scale.</param>
    ''' <paramname="unit">The unit in length.</param>
    <Extension()>
    Public Sub SaveReportToExcel(ByVal picturebox As PictureBox(), ByVal filter As String, ByVal saveDialogTitle As String, ByVal obj_list_list As List(Of List(Of MeasureObject)), ByVal graphPen As Pen, ByVal graphPen_line As Pen, ByVal digit As Integer, ByVal CF As Double, ByVal unit As String)
        Dim SaveFileDialog As SaveFileDialog = New SaveFileDialog()
        SaveFileDialog.Filter = filter
        SaveFileDialog.Title = saveDialogTitle

        Dim xlsx_savepath = ""
        If SaveFileDialog.ShowDialog() = DialogResult.OK Then
            xlsx_savepath = SaveFileDialog.FileName
        End If

        Dim listView As ListView = New ListView()
        listView.Columns.Add("Object")
        listView.Columns.Add("Length")
        listView.Columns.Add("Angle")
        listView.Columns.Add("Radius")
        listView.Columns.Add("Unit")
        listView.Columns.Add("Remarks")

        Using workbook = New XLWorkbook()
            For i = 0 To 24
                Dim img = picturebox(i).Image
                If img IsNot Nothing Then
                    Dim result As Bitmap = New Bitmap(img.Width, img.Height)

                    Using graph = Graphics.FromImage(result)
                        Dim PointX = 0
                        Dim PointY = 0
                        Dim Width = img.Width
                        Dim Height = img.Height
                        graph.DrawImage(img, PointX, PointY, Width, Height)
                        DrawObjList2(graph, picturebox(i), obj_list_list.ElementAt(i), graphPen, graphPen_line, digit, CF)
                        graph.Flush()
                    End Using

                    Dim ms As MemoryStream = New MemoryStream()
                    result.Save(ms, Imaging.ImageFormat.Png)

                    listView.Clear()
                    listView.LoadObjectList(obj_list_list.ElementAt(i), CF, digit, unit)

                    Dim worksheet = workbook.Worksheets.Add("Result Sheet" & i.ToString())
                    worksheet.Cell("A1").Value = "Object"
                    worksheet.Cell("B1").Value = "Length"
                    worksheet.Cell("C1").Value = "Angle"
                    worksheet.Cell("D1").Value = "Radius"
                    worksheet.Cell("E1").Value = "Unit"
                    worksheet.Cell("F1").Value = "Remarks"
                    Dim row_count_listbox = listView.Items.Count
                    For j = 0 To row_count_listbox - 1
                        worksheet.Cell("A" & (j + 2).ToString()).Value = listView.Items(j).SubItems(0).Text
                        worksheet.Cell("B" & (j + 2).ToString()).Value = listView.Items(j).SubItems(1).Text
                        worksheet.Cell("C" & (j + 2).ToString()).Value = listView.Items(j).SubItems(2).Text
                        worksheet.Cell("D" & (j + 2).ToString()).Value = listView.Items(j).SubItems(3).Text
                        worksheet.Cell("E" & (j + 2).ToString()).Value = listView.Items(j).SubItems(4).Text
                        worksheet.Cell("F" & (j + 2).ToString()).Value = listView.Items(j).SubItems(5).Text
                    Next
                    Dim image = worksheet.AddPicture(ms).MoveTo(worksheet.Cell("G2")) 'the cast is only to be sure
                    'Or the cell you want to bind the picture

                End If
            Next
            workbook.SaveAs(xlsx_savepath)
        End Using
    End Sub

    ''' <summary>
    ''' Save the information of objects to excel file.
    ''' </summary>
    ''' <paramname="listview">The datagridview contains data</param>
    ''' <paramname="filter">The filter to be used to get type of images. example ("PNG Files|*.png|BMP Files|*.bmp")</param>
    ''' <paramname="saveDialogTitle">The title for the save dialog appears for the user.</param>

    Public Sub SaveListToExcel(ByVal listview As DataGridView, ByVal filter As String, ByVal saveDialogTitle As String)
        Dim SaveFileDialog As SaveFileDialog = New SaveFileDialog()
        SaveFileDialog.Filter = filter
        SaveFileDialog.Title = saveDialogTitle

        Dim xlsx_savepath = ""
        If SaveFileDialog.ShowDialog() = DialogResult.OK Then
            xlsx_savepath = SaveFileDialog.FileName
        End If
        Dim NameSet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"

        Using workbook = New XLWorkbook()
            For cnt = 0 To 24
                Dim worksheet = workbook.Worksheets.Add("Result Sheet" & cnt.ToString())
                For i = 0 To listview.Columns.Count - 1
                    worksheet.Cell(NameSet(i) & 1).Value = listview.Columns(i).HeaderText
                Next

                Dim row_count_listbox = listview.Rows.Count
                For i = 0 To row_count_listbox - 1
                    For j = 0 To listview.Columns.Count - 1
                        worksheet.Cell(NameSet(j) & (i + 2).ToString()).Value = listview.Rows(i).Cells(j).Value
                    Next
                Next
            Next

            workbook.SaveAs(xlsx_savepath)
        End Using
    End Sub


    ''' <summary>
    ''' Save the information of objects to excel file.
    ''' </summary>
    ''' <paramname="image">The result image</param>
    ''' <paramname="listview">The datagridview contains data</param>
    ''' <paramname="filter">The filter to be used to get type of images. example ("PNG Files|*.png|BMP Files|*.bmp")</param>
    ''' <paramname="saveDialogTitle">The title for the save dialog appears for the user.</param>

    Public Sub SaveListToReport(img As Image, ByVal listview As DataGridView, ByVal filter As String, ByVal saveDialogTitle As String)
        Dim SaveFileDialog As SaveFileDialog = New SaveFileDialog()
        SaveFileDialog.Filter = filter
        SaveFileDialog.Title = saveDialogTitle

        Dim xlsx_savepath = ""
        If SaveFileDialog.ShowDialog() = DialogResult.OK Then
            xlsx_savepath = SaveFileDialog.FileName
        End If
        Dim NameSet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        Using workbook = New XLWorkbook()
            Dim result As Bitmap = New Bitmap(img.Width, img.Height)

            Using graph = Graphics.FromImage(result)
                Dim PointX = 0
                Dim PointY = 0
                Dim Width = img.Width
                Dim Height = img.Height
                graph.DrawImage(img, PointX, PointY, Width, Height)
                graph.Flush()
            End Using

            Dim ms As MemoryStream = New MemoryStream()
            result.Save(ms, Imaging.ImageFormat.Png)
            Dim worksheet = workbook.Worksheets.Add("Result Sheet")
            For i = 0 To listview.Columns.Count - 1
                worksheet.Cell(NameSet(i) & 1).Value = listview.Columns(i).HeaderText
            Next

            Dim row_count_listbox = listview.Rows.Count
            For i = 0 To row_count_listbox - 1
                For j = 0 To listview.Columns.Count - 1
                    worksheet.Cell(NameSet(j) & (i + 2).ToString()).Value = listview.Rows(i).Cells(j).Value
                Next
            Next
            Dim image = worksheet.AddPicture(ms).MoveTo(worksheet.Cell(NameSet(listview.Columns.Count) & 2)) 'the cast is only to be sure

            workbook.SaveAs(xlsx_savepath)
        End Using
    End Sub

    ''' <summary>
    ''' Save the information of objects to excel file.
    ''' </summary>
    ''' <paramname="image">The result image</param>
    ''' <paramname="listview">The datagridview contains data</param>
    ''' <paramname="filter">The filter to be used to get type of images. example ("PNG Files|*.png|BMP Files|*.bmp")</param>
    ''' <paramname="saveDialogTitle">The title for the save dialog appears for the user.</param>

    Public Sub SaveListToReport(pic As PictureBox, ByVal listview As DataGridView, ByVal filter As String, ByVal saveDialogTitle As String, Obj As SegObject, font As Font)
        Dim SaveFileDialog As SaveFileDialog = New SaveFileDialog()
        SaveFileDialog.Filter = filter
        SaveFileDialog.Title = saveDialogTitle

        Dim xlsx_savepath = ""
        If SaveFileDialog.ShowDialog() = DialogResult.OK Then
            xlsx_savepath = SaveFileDialog.FileName
        End If
        Dim NameSet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        Using workbook = New XLWorkbook()
            Dim result As Bitmap = New Bitmap(pic.Width, pic.Height)

            Using graph = Graphics.FromImage(result)
                Dim PointX = 0
                Dim PointY = 0
                Dim Width = pic.Width
                Dim Height = pic.Height
                graph.DrawImage(pic.Image, PointX, PointY, Width, Height)
                If Obj.measureType = SegType.BlobSegment Then
                    DrawLabelForCount(graph, pic, Obj.BlobSegObj.BlobList, font)
                ElseIf Obj.measureType = SegType.intersection Then
                    IdentifyInterSections(graph, Main_Form.ID_PICTURE_BOX(Main_Form.tab_index).Image, Obj.sectObj.thr_seg, Obj)
                End If

                graph.Flush()
            End Using

            Dim ms As MemoryStream = New MemoryStream()
            result.Save(ms, Imaging.ImageFormat.Png)
            Dim worksheet = workbook.Worksheets.Add("Result Sheet")
            For i = 0 To listview.Columns.Count - 1
                worksheet.Cell(NameSet(i) & 1).Value = listview.Columns(i).HeaderText
            Next

            Dim row_count_listbox = listview.Rows.Count
            For i = 0 To row_count_listbox - 1
                For j = 0 To listview.Columns.Count - 1
                    worksheet.Cell(NameSet(j) & (i + 2).ToString()).Value = listview.Rows(i).Cells(j).Value
                Next
            Next
            Dim image = worksheet.AddPicture(ms).MoveTo(worksheet.Cell(NameSet(listview.Columns.Count) & 2)) 'the cast is only to be sure

            workbook.SaveAs(xlsx_savepath)
        End Using
    End Sub
    ''' <summary>
    ''' Load text file and append data-table to existing list.
    ''' </summary>
    ''' <paramname="filename">The name of text file contains data-table</param>
    ''' <paramname="obj_list">The list of objects</param>
    ''' <paramname="cur_index">The index of current object.</param>

    <Extension()>
    Public Sub AppendDataToObjList(ByVal filename As String, ByVal obj_list As List(Of MeasureObject), ByRef cur_index As Integer)

        Dim items As String() = New String(1) {}
        Dim obj = New MeasureObject()
        ' Create new StreamReader instance with Using block.
        For Each line As String In File.ReadLines(filename)
            ' Read one line from file
            items = line.Split(",")
            obj.name = items(0)
            obj.length = CDbl(items(1))
            obj.measure_type = MeasureType.toCurves
            obj_list.Add(obj)
            cur_index = cur_index + 1
        Next line

    End Sub

End Module
