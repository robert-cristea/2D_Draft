﻿Imports Emgu.CV
Imports Emgu.CV.Structure

Public Class Intersection
    Public thr_seg As Integer
    Public ResizedImg As Emgu.CV.Image(Of Bgr, Byte)
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub Intersection_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Main_Form.objSeg.Refresh()
        Dim scr = Main_Form.resizedImage.ToBitmap()
        Dim bmpImage As Bitmap = New Bitmap(scr)
        ResizedImg = bmpImage.ToImage(Of Bgr, Byte)()
        bmpImage.Dispose()
    End Sub

    Private Sub LoadDataToGridView()
        DataGridView1.Rows.Clear()

        Dim str_item = New String(2) {}

        Dim maxLine = Math.Max(Main_Form.objSeg.sectObj.horLine, Main_Form.objSeg.sectObj.verLine)
        For j = 0 To maxLine - 1
            str_item(0) = (j + 1).ToString
            str_item(1) = "0"
            str_item(2) = "0"
            If j < Main_Form.objSeg.sectObj.horLine Then
                str_item(1) = Main_Form.objSeg.sectObj.horSectCnt(j)
            End If
            If j < Main_Form.objSeg.sectObj.verLine Then
                str_item(2) = Main_Form.objSeg.sectObj.verSectCnt(j)
            End If
            DataGridView1.Rows.Add(str_item)
        Next
    End Sub

    Private Sub ID_SCROLL_THR_SEG_Scroll(sender As Object, e As EventArgs) Handles ID_SCROLL_THR_SEG.Scroll
        Try
            thr_seg = ID_SCROLL_THR_SEG.Value
            ID_LABEL_THR_SEG.Text = thr_seg.ToString()
            Main_Form.objSeg.sectObj.threshold = thr_seg

            Dim percent_black = 0
            Dim percent_white = 0

            Dim image = Main_Form.resizedImage.ToBitmap()
            Dim output = SegmentIntoBlackAndWhite(image, thr_seg, Main_Form.objSeg, percent_black, percent_white)
            Main_Form.PictureBox.Image = output

            Dim ResizedMat = GetMatFromSDImage(output)
            Dim sz = New Size(Main_Form.originalImage.Width, Main_Form.originalImage.Height)
            CvInvoke.Resize(ResizedMat, Main_Form.currentImage, sz)
            ResizedMat.Dispose()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ID_BTN_EDGE_Click(sender As Object, e As EventArgs) Handles ID_BTN_EDGE.Click
        Try
            thr_seg = ID_SCROLL_THR_SEG.Value

            Dim image = Main_Form.resizedImage.ToBitmap()
            Dim edge = GetEdgeFromBinary(image, thr_seg)

            Dim bmpImage As Bitmap = New Bitmap(edge)
            Dim edgeEmgu As Emgu.CV.Image(Of Gray, Byte) = bmpImage.ToImage(Of Gray, Byte)()
            bmpImage.Dispose()

            Dim output = OverLapSegToOri(ResizedImg, edgeEmgu)
            Main_Form.PictureBox.Image = output.ToBitmap()

            Dim sz = New Size(Main_Form.originalImage.Width, Main_Form.originalImage.Height)
            CvInvoke.Resize(output, Main_Form.currentImage, sz)
            output.Dispose()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub BtnCount_Click(sender As Object, e As EventArgs) Handles BtnCount.Click
        Try
            Main_Form.objSeg.sectObj.horLine = ID_NUM_HORIZON.Value
            Main_Form.objSeg.sectObj.verLine = ID_NUM_VERTICAL.Value
            Dim image = Main_Form.resizedImage.ToBitmap()
            IdentifyInterSections(Main_Form.PictureBox, image, thr_seg, Main_Form.objSeg)
            LoadDataToGridView()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BtnExcel_Click(sender As Object, e As EventArgs) Handles BtnExcel.Click
        Dim filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*"
        Dim title = "Save"
        SaveListToExcel(DataGridView1, filter, title)
    End Sub

    Private Sub BtnReport_Click(sender As Object, e As EventArgs) Handles BtnReport.Click
        Dim filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*"
        Dim title = "Save"
        SaveListToReport(Main_Form.PictureBox, DataGridView1, filter, title, Main_Form.objSeg, Font)
    End Sub

    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles BtnExit.Click
        Me.Close()
    End Sub


End Class