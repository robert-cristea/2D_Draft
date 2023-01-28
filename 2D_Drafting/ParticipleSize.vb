Imports Emgu.CV
Imports Emgu.CV.CvEnum
Imports Emgu.CV.Structure

Public Class ParticipleSize
    Public OriImage As Emgu.CV.Image(Of Bgr, Byte)
    Public GrayImage As Emgu.CV.Image(Of Gray, Byte)
    Public BinaryImage As Emgu.CV.Image(Of Gray, Byte)
    Public ObjListTemp As List(Of BlobObj) = New List(Of BlobObj)
    Private RadioState As Integer
    Private Upper As Integer
    Private Lower As Integer
    Private font = New Font("Arial", 10, FontStyle.Regular)

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub ParticipleSize_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim scr = Main_Form.originalImage.ToBitmap()
            Dim bmpImage As Bitmap = New Bitmap(scr)
            OriImage = bmpImage.ToImage(Of Bgr, Byte)()
            bmpImage.Dispose()
            GrayImage = getGrayScale(OriImage)
            BinaryImage = GrayImage.CopyBlank()
            Timer1.Interval = 30
            Timer1.Start()
        Catch ex As Exception

        End Try
    End Sub

    Private Function GetSelectedBlob() As Integer
        Dim index = -1
        Dim mPtX = Main_Form.mCurDragPt.X
        Dim mPtY = Main_Form.mCurDragPt.Y
        For i = 0 To ObjListTemp.Count - 1
            Dim minX = ObjListTemp(i).topLeft.X
            Dim minY = ObjListTemp(i).topLeft.Y
            Dim maxX = ObjListTemp(i).rightBottom.X
            Dim maxY = ObjListTemp(i).rightBottom.Y
            If mPtX > minX And mPtX < maxX And mPtY > minY And mPtY < maxY Then
                index = i
                Exit For
            End If
        Next
        Return index
    End Function

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If Main_Form.MouseDownFlag Then
            Dim output = BlobDetection(OriImage, BinaryImage, ObjListTemp, 0, 0, 1)
            Dim index = GetSelectedBlob()
            If index >= 0 Then
                Main_Form.objSeg.BlobSegObj.BlobList.Add(ObjListTemp(index))
            End If
            DrawLabelForCount(Main_Form.PictureBox, Main_Form.objSeg.BlobSegObj.BlobList, font)
            LoadDataToGridView()
            Main_Form.MouseDownFlag = False
        End If
    End Sub
    Private Sub GetBinaryImage()
        Try
            If RadioState = 0 Then
                Dim form = New Intensity()
                If form.ShowDialog() = DialogResult.OK Then
                    Upper = form.Upper
                    Lower = form.Lower
                End If
                BinaryImage = GetBinaryWith2Thr(GrayImage, Lower, Upper)
            ElseIf RadioState = 1 Then
                CvInvoke.Threshold(GrayImage, BinaryImage, 0, 255, ThresholdType.Otsu)
            ElseIf RadioState = 2 Then
                CvInvoke.Threshold(GrayImage, BinaryImage, 0, 255, ThresholdType.Otsu)
                BinaryImage = InvertBinary(BinaryImage)
                'CvInvoke.Imshow("1", BinaryImage)
            End If

            Dim resizedBinary = BinaryImage.Copy()
            Dim sz = New Size(Main_Form.resizedImage.Width, Main_Form.resizedImage.Height)
            CvInvoke.Resize(BinaryImage, resizedBinary, sz)
            Dim BinImg = GetImageFromEmgu(resizedBinary)
            Dim outPut = OverLapSegToOri(Main_Form.resizedImage.ToBitmap(), BinImg)
            Main_Form.PictureBox.Image = outPut
            Main_Form.currentImage = GetMatFromSDImage(outPut)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub LoadDataToGridView()
        DataGridView1.Rows.Clear()
        Dim str_item = New String(5) {}

        For i = 0 To Main_Form.objSeg.BlobSegObj.BlobList.Count - 1
            Dim Obj = Main_Form.objSeg.BlobSegObj.BlobList(i)
            str_item(0) = (i + 1).ToString()
            str_item(1) = Obj.height.ToString()
            str_item(2) = Obj.Width.ToString()
            str_item(3) = Obj.Area.ToString()
            Dim ratio = Obj.Width / Obj.height
            str_item(4) = Math.Round(ratio, 2).ToString()
            str_item(5) = Obj.roundness.ToString()

            DataGridView1.Rows.Add(str_item)
        Next
    End Sub
    Private Sub RadioManual_CheckedChanged(sender As Object, e As EventArgs) Handles RadioManual.CheckedChanged
        If RadioManual.Checked Then
            RadioState = 0
            GetBinaryImage()
        End If
    End Sub

    Private Sub RadioAutoBri_CheckedChanged(sender As Object, e As EventArgs) Handles RadioAutoBri.CheckedChanged
        If RadioAutoBri.Checked Then
            RadioState = 1
            GetBinaryImage()
        End If
    End Sub

    Private Sub RadioAutoDark_CheckedChanged(sender As Object, e As EventArgs) Handles RadioAutoDark.CheckedChanged
        If RadioAutoDark.Checked Then
            RadioState = 2
            GetBinaryImage()
        End If
    End Sub

    Private Sub BtnFont_Click(sender As Object, e As EventArgs) Handles BtnFont.Click
        Dim fontDialog As FontDialog = New FontDialog()

        If fontDialog.ShowDialog() = DialogResult.OK Then
            font = fontDialog.Font
        End If
        DrawLabelForCount(Main_Form.PictureBox, Main_Form.objSeg.BlobSegObj.BlobList, font)
    End Sub

    Private Sub BtnDel_Click(sender As Object, e As EventArgs) Handles BtnDel.Click
        Main_Form.objSeg.Refresh()
        DrawLabelForCount(Main_Form.PictureBox, Main_Form.objSeg.BlobSegObj.BlobList, font)
        LoadDataToGridView()
    End Sub

    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles BtnExit.Click
        Me.Close()
    End Sub

    Private Sub BtnReport_Click(sender As Object, e As EventArgs) Handles BtnReport.Click
        Dim filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*"
        Dim title = "Save"
        SaveListToReport(Main_Form.PictureBox, DataGridView1, filter, title, Main_Form.objSeg, font)
    End Sub

    Private Sub BtnExcel_Click(sender As Object, e As EventArgs) Handles BtnExcel.Click
        Dim filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*"
        Dim title = "Save"
        SaveListToExcel(DataGridView1, filter, title)
    End Sub

    Private Sub ParticipleSize_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Main_Form.Controls.Remove(Me)
    End Sub
End Class