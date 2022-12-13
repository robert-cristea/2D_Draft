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
    Public ObjList As List(Of BlobObj) = New List(Of BlobObj)

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub New(Upper As Integer, Lower As Integer, MinArea As Single)
        InitializeComponent()
        IntenUpper = Upper
        IntenLower = Lower
        AreaLimit = MinArea
    End Sub

    Private Sub RoundnessLimit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ObjList.Clear()
        Dim scr = Main_Form.origin_image(Main_Form.tab_index).ToBitmap()
        Dim bmpImage As Bitmap = New Bitmap(scr)
        OriImage = bmpImage.ToImage(Of Bgr, Byte)()
        bmpImage.Dispose()
        GrayImage = getGrayScale(OriImage)
        BinaryImage = GetBinaryWith2Thr(GrayImage, IntenLower, IntenUpper)
    End Sub

    Public Sub DrawResult()
        ObjList.Clear()
        Dim output = BlobDetection(OriImage, BinaryImage, ObjList, AreaLimit, RoundLower, RoundUpper)
        Dim sz = New Size(Main_Form.resized_image(Main_Form.tab_index).Width, Main_Form.resized_image(Main_Form.tab_index).Height)
        CvInvoke.Resize(output, output, sz)
        Dim Image = GetImageFromEmgu(output)
        Main_Form.ID_PICTURE_BOX(Main_Form.tab_index).Image = Image
        Main_Form.current_image(Main_Form.tab_index) = GetMatFromSDImage(Image)
    End Sub

    Private Sub TrackUpper_Scroll(sender As Object, e As EventArgs) Handles TrackUpper.Scroll
        RoundUpper = TrackUpper.Value / CSng(100)
        LabUpper.Text = TrackUpper.Value.ToString()
        DrawResult()
    End Sub

    Private Sub TrackLower_Scroll(sender As Object, e As EventArgs) Handles TrackLower.Scroll
        RoundLower = TrackLower.Value / CSng(100)
        LabLower.Text = TrackLower.Value.ToString()
        DrawResult()
    End Sub
End Class