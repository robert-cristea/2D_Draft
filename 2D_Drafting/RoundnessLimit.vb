Imports Emgu.CV
Imports Emgu.CV.Dnn
Imports Emgu.CV.Structure

Public Class RoundnessLimit
    Public OriImage As Emgu.CV.Image(Of Bgr, Byte)
    Public GrayImage As Emgu.CV.Image(Of Gray, Byte)
    Public BinaryImage As Emgu.CV.Image(Of Gray, Byte)
    Public IntenUpper As Integer
    Public IntenLower As Integer
    Public RoundUpper As Single
    Public RoundLower As Single
    Public AreaLimit As Single
    Public PerVsAreaRatioLower As Single
    Public PerVsAreaRatioUpper As Single
    Public ObjList As List(Of BlobObj) = New List(Of BlobObj)
    Public DistingshType As Boolean

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub New(Upper As Integer, Lower As Integer, MinArea As Single, DistType As Boolean)
        InitializeComponent()
        IntenUpper = Upper
        IntenLower = Lower
        AreaLimit = MinArea
        DistingshType = DistType
        If DistingshType = False Then
            Me.Text = "Roundness Limit"
        Else
            Me.Text = "Perimeter/Area Limit"
        End If
    End Sub

    Private Sub RoundnessLimit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ObjList.Clear()
            Dim scr = Main_Form.originalImage.ToBitmap()
            Dim bmpImage As Bitmap = New Bitmap(scr)
            OriImage = bmpImage.ToImage(Of Bgr, Byte)()
            bmpImage.Dispose()
            GrayImage = getGrayScale(OriImage)
            BinaryImage = GetBinaryWith2Thr(GrayImage, IntenLower, IntenUpper)
        Catch ex As Exception

        End Try
    End Sub

    Public Sub DrawResult()
        Try
            ObjList.Clear()
            Dim output As Emgu.CV.Image(Of Bgr, Byte)
            If DistingshType = False Then
                output = BlobDetection(OriImage, BinaryImage, ObjList, AreaLimit, RoundLower, RoundUpper)
            Else
                output = BlobDetection(OriImage, BinaryImage, ObjList, AreaLimit, 0, 1, PerVsAreaRatioLower, PerVsAreaRatioUpper)
            End If

            Dim sz = New Size(Main_Form.resizedImage.Width, Main_Form.resizedImage.Height)
            Dim Resized As Mat = New Mat()
            CvInvoke.Resize(output, Resized, sz)

            Main_Form.PictureBox.Image = Resized.ToBitmap()
            Main_Form.currentImage = output.Clone().Mat
            output.Dispose()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TrackUpper_Scroll(sender As Object, e As EventArgs) Handles TrackUpper.Scroll
        RoundUpper = TrackUpper.Value / CSng(100)
        PerVsAreaRatioUpper = RoundUpper
        LabUpper.Text = TrackUpper.Value.ToString()
        DrawResult()
    End Sub

    Private Sub TrackLower_Scroll(sender As Object, e As EventArgs) Handles TrackLower.Scroll
        RoundLower = TrackLower.Value / CSng(100)
        PerVsAreaRatioLower = RoundLower
        LabLower.Text = TrackLower.Value.ToString()
        DrawResult()
    End Sub
End Class