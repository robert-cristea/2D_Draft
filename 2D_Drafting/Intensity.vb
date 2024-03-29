﻿Imports Emgu.CV
Imports Emgu.CV.Structure

Public Class Intensity
    Public OriImage As Emgu.CV.Image(Of Bgr, Byte)
    Public GrayImage As Emgu.CV.Image(Of Gray, Byte)
    Public BinaryImage As Emgu.CV.Image(Of Gray, Byte)
    Public Upper As Integer
    Public Lower As Integer

    Private Sub Intensity_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim scr = Main_Form.originalImage.ToBitmap()
            Dim bmpImage As Bitmap = New Bitmap(scr)
            OriImage = bmpImage.ToImage(Of Bgr, Byte)()
            bmpImage.Dispose()
            GrayImage = getGrayScale(OriImage)
            BinaryImage = GrayImage.CopyBlank()
        Catch ex As Exception

        End Try
    End Sub

    Public Sub DrawResult()
        Try
            BinaryImage = GetBinaryWith2Thr(GrayImage, Lower, Upper)
            Dim outPut = OverLapSegToOri(OriImage, BinaryImage)
            Dim sz = New Size(Main_Form.resizedImage.Width, Main_Form.resizedImage.Height)
            Dim Resized As Mat = New Mat()
            CvInvoke.Resize(outPut, Resized, sz)

            Main_Form.PictureBox.Image = Resized.ToBitmap()
            Main_Form.currentImage = outPut.Clone().Mat
            outPut.Dispose()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub TrackBar1_Scroll(sender As Object, e As EventArgs) Handles TrackBar1.Scroll
        Upper = TrackBar1.Value
        LabUpper.Text = Upper.ToString()
        DrawResult()
    End Sub

    Private Sub TrackBar2_Scroll(sender As Object, e As EventArgs) Handles TrackBar2.Scroll
        Lower = TrackBar2.Value
        LabLower.Text = Lower.ToString()
        DrawResult()
    End Sub


End Class