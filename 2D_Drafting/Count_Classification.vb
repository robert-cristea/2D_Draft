Imports System.Runtime.Remoting
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports Emgu.CV
Imports Emgu.CV.CvEnum
Imports Emgu.CV.DepthAI
Imports Emgu.CV.Structure

Public Class Count_Classification
    Public OriImage As Emgu.CV.Image(Of Bgr, Byte)
    Public GrayImage As Emgu.CV.Image(Of Gray, Byte)
    Public BinaryImage As Emgu.CV.Image(Of Gray, Byte)

    Private RadioState As Integer
    Private RadioState2 As Integer
    Private Upper As Integer
    Private Lower As Integer
    Private AreaLimit As Single
    Private font = New Font("Arial", 10, FontStyle.Regular)

    Private totalCnt As Integer
    Private maxPer As Single
    Private minPer As Single
    Private maxArea As Single
    Private minArea As Single

    Private DisList As List(Of Double) = New List(Of Double)

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub Count_Classification_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

    Private Sub GetDistribution()
        Dim delta As Single
        If RadioState2 = 0 Then
            delta = (maxArea - minArea) / 4
            TxtFrom1.Text = minArea.ToString()
            TxtTo1.Text = (minArea + delta).ToString()
            TxtFrom2.Text = (minArea + delta).ToString()
            TxtTo2.Text = (minArea + 2 * delta).ToString()
            TxtFrom3.Text = (minArea + 2 * delta).ToString()
            TxtTo3.Text = (minArea + 3 * delta).ToString()
            TxtFrom4.Text = (minArea + 3 * delta).ToString()
            TxtTo4.Text = (maxArea).ToString()
        Else
            delta = (maxPer - minPer) / 4
            TxtFrom1.Text = minPer.ToString()
            TxtTo1.Text = (minPer + delta).ToString()
            TxtFrom2.Text = (minPer + delta).ToString()
            TxtTo2.Text = (minPer + 2 * delta).ToString()
            TxtFrom3.Text = (minPer + 2 * delta).ToString()
            TxtTo3.Text = (minPer + 3 * delta).ToString()
            TxtFrom4.Text = (minPer + 3 * delta).ToString()
            TxtTo4.Text = (maxPer).ToString()
        End If
    End Sub
    Private Sub GetMinMax()
        totalCnt = Main_Form.Obj_Seg.BlobSegObj.BlobList.Count
        maxPer = 0
        minPer = 9999999
        maxArea = 0
        minArea = 99999999

        For i = 0 To totalCnt - 1
            Dim Obj = Main_Form.Obj_Seg.BlobSegObj.BlobList(i)
            If maxPer < Obj.Perimeter Then maxPer = Obj.Perimeter
            If minPer > Obj.Perimeter Then minPer = Obj.Perimeter
            If maxArea < Obj.Area Then maxArea = Obj.Area
            If minArea > Obj.Area Then minArea = Obj.Area
        Next
        LabTotal.Text = totalCnt.ToString()
        LabMaxPer.Text = maxPer.ToString()
        LabMinPer.Text = minPer.ToString()
        LabMaxArea.Text = maxArea.ToString()
        LabMinArea.Text = minArea.ToString()
        GetDistribution()
    End Sub
    Private Sub GetBinaryImage()
        Try
            If RadioManual.Checked Then
                RadioState = 0
                Dim form = New Intensity()
                If form.ShowDialog() = DialogResult.OK Then
                    Upper = form.Upper
                    Lower = form.Lower
                End If
                BinaryImage = GetBinaryWith2Thr(GrayImage, Lower, Upper)
            ElseIf RadioAutoBright.Checked Then
                RadioState = 1
                CvInvoke.Threshold(GrayImage, BinaryImage, 0, 255, ThresholdType.Otsu)
            ElseIf RadioAutoDark.Checked Then
                RadioState = 2
                CvInvoke.Threshold(GrayImage, BinaryImage, 0, 255, ThresholdType.Otsu)
                BinaryImage = InvertBinary(BinaryImage)
                'CvInvoke.Imshow("1", BinaryImage)
            End If

            Dim resizedBinary = BinaryImage.Copy()
            Dim sz = New Size(Main_Form.resizedImage.Width, Main_Form.resizedImage.Height)
            CvInvoke.Resize(BinaryImage, resizedBinary, sz)
            Dim BinImg = GetImageFromEmgu(resizedBinary)
            Dim outPut = OverLapSegToOri(Main_Form.resizedImage.ToBitmap(), BinImg)
            Main_Form.ID_PICTURE_BOX(Main_Form.tab_index).Image = outPut
            Main_Form.currentImage = GetMatFromSDImage(outPut)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub LoadDataToGridView()
        DataGridView1.Rows.Clear()
        Dim str_item = New String(6) {}

        For i = 0 To Main_Form.Obj_Seg.BlobSegObj.BlobList.Count - 1
            Dim Obj = Main_Form.Obj_Seg.BlobSegObj.BlobList(i)
            str_item(0) = (i + 1).ToString()
            str_item(1) = Obj.height.ToString()
            str_item(2) = Obj.Width.ToString()
            str_item(3) = Obj.Area.ToString()
            str_item(4) = Obj.Perimeter.ToString()
            Dim ratio = Obj.Width / Obj.height
            str_item(5) = Math.Round(ratio, 2).ToString()
            str_item(6) = Obj.roundness.ToString()

            DataGridView1.Rows.Add(str_item)
        Next
    End Sub
    Private Sub RadioManual_CheckedChanged(sender As Object, e As EventArgs) Handles RadioManual.CheckedChanged
        If RadioManual.Checked Then
            GetBinaryImage()
        End If

    End Sub

    Private Sub RadioAutoBright_CheckedChanged(sender As Object, e As EventArgs) Handles RadioAutoBright.CheckedChanged
        If RadioAutoBright.Checked Then
            GetBinaryImage()
        End If

    End Sub

    Private Sub RadioAutoDark_CheckedChanged(sender As Object, e As EventArgs) Handles RadioAutoDark.CheckedChanged
        If RadioAutoDark.Checked Then
            GetBinaryImage()
        End If

    End Sub

    Private Sub BtnCount_Click(sender As Object, e As EventArgs) Handles BtnCount.Click
        If TxtLimit.Text = "" Then
            Return
        End If
        Try
            Main_Form.Obj_Seg.BlobSegObj.BlobList.Clear()
            AreaLimit = CSng(TxtLimit.Text)
            If AreaLimit Then
                Dim output = BlobDetection(OriImage, BinaryImage, Main_Form.Obj_Seg.BlobSegObj.BlobList, AreaLimit)
                DrawLabelForCount(Main_Form.ID_PICTURE_BOX(Main_Form.tab_index), Main_Form.Obj_Seg.BlobSegObj.BlobList, font)
                GetMinMax()
                LoadDataToGridView()
                Pic1.BackColor = Color.LimeGreen
                Pic2.BackColor = Color.Yellow
                Pic3.BackColor = Color.Aqua
                Pic4.BackColor = Color.Fuchsia
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub RadioArea_CheckedChanged(sender As Object, e As EventArgs) Handles RadioArea.CheckedChanged
        If RadioArea.Checked = True Then
            RadioState2 = 0
            GetDistribution()
        End If
    End Sub

    Private Sub RadioLength_CheckedChanged(sender As Object, e As EventArgs) Handles RadioLength.CheckedChanged
        If RadioLength.Checked = True Then
            RadioState2 = 1
            GetDistribution()
        End If
    End Sub

    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click
        DisList.Clear()

        Dim delta As Single
        Dim Cnt As Integer() = New Integer(3) {}

        If RadioState2 = 0 Then
            delta = (maxArea - minArea) / 4
            For i = 0 To totalCnt - 1
                Dim Obj = Main_Form.Obj_Seg.BlobSegObj.BlobList(i)
                If Obj.Area > minArea And Obj.Area <= minArea + delta Then
                    Cnt(0) += 1
                ElseIf Obj.Area > minArea + delta And Obj.Area <= minArea + 2 * delta Then
                    Cnt(1) += 1
                ElseIf Obj.Area > minArea + 2 * delta And Obj.Area <= minArea + 3 * delta Then
                    Cnt(2) += 1
                ElseIf Obj.Area > minArea + 3 * delta And Obj.Area <= maxArea Then
                    Cnt(3) += 1
                End If
            Next
        Else
            delta = (maxPer - minPer) / 4
            For i = 0 To totalCnt - 1
                Dim Obj = Main_Form.Obj_Seg.BlobSegObj.BlobList(i)
                If Obj.Perimeter > minPer And Obj.Perimeter <= minPer + delta Then
                    Cnt(0) += 1
                ElseIf Obj.Perimeter > minPer + delta And Obj.Perimeter <= minPer + 2 * delta Then
                    Cnt(1) += 1
                ElseIf Obj.Perimeter > minPer + 2 * delta And Obj.Perimeter <= minPer + 3 * delta Then
                    Cnt(2) += 1
                ElseIf Obj.Perimeter > minPer + 3 * delta And Obj.Perimeter <= maxPer Then
                    Cnt(3) += 1
                End If
            Next
        End If

        For i = 0 To 3
            DisList.Add(Cnt(i) / CSng(totalCnt) * 100)
        Next

        TxtPercent1.Text = DisList(0).ToString()
        TxtPercent2.Text = DisList(1).ToString()
        TxtPercent3.Text = DisList(2).ToString()
        TxtPercent4.Text = DisList(3).ToString()
    End Sub

    Private Sub Intialize()
        DisList.Clear()

        TxtFrom1.Text = "0"
        TxtTo1.Text = "0"
        TxtFrom2.Text = "0"
        TxtTo2.Text = "0"
        TxtFrom3.Text = "0"
        TxtTo3.Text = "0"
        TxtFrom4.Text = "0"
        TxtTo4.Text = "0"

        TxtPercent1.Text = "00.00"
        TxtPercent2.Text = "00.00"
        TxtPercent3.Text = "00.00"
        TxtPercent4.Text = "00.00"

        LabTotal.Text = "0"
        LabMaxPer.Text = "00.00"
        LabMinPer.Text = "00.00"
        LabMaxArea.Text = "00.00"
        LabMinArea.Text = "00.00"

        Main_Form.ID_PICTURE_BOX(Main_Form.tab_index).Image = Main_Form.resizedImage.ToBitmap()
        Main_Form.currentImage = Main_Form.resizedImage
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        Intialize()
    End Sub

    Private Sub BtnFont_Click(sender As Object, e As EventArgs) Handles BtnFont.Click
        Dim fontDialog As FontDialog = New FontDialog()

        If fontDialog.ShowDialog() = DialogResult.OK Then
            font = fontDialog.Font
        End If
        DrawLabelForCount(Main_Form.ID_PICTURE_BOX(Main_Form.tab_index), Main_Form.Obj_Seg.BlobSegObj.BlobList, font)
    End Sub

    Private Sub BtnReport_Click(sender As Object, e As EventArgs) Handles BtnReport.Click
        Dim filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*"
        Dim title = "Save"
        SaveListToReport(Main_Form.ID_PICTURE_BOX(Main_Form.tab_index), DataGridView1, filter, title, Main_Form.Obj_Seg, font)
    End Sub

    Private Sub BtnExcel_Click(sender As Object, e As EventArgs) Handles BtnExcel.Click
        Dim filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*"
        Dim title = "Save"
        SaveListToExcel(DataGridView1, filter, title)
    End Sub

    Private Sub BtnGraph_Click(sender As Object, e As EventArgs) Handles BtnGraph.Click
        Dim ColList As List(Of Integer()) = New List(Of Integer())

        Dim Col = Color.FromName("LimeGreen")
        ColList.Add(New Integer() {Col.B, Col.G, Col.R})

        Col = Color.FromName("Yellow")
        ColList.Add(New Integer() {Col.B, Col.G, Col.R})

        Col = Color.FromName("Aqua")
        ColList.Add(New Integer() {Col.B, Col.G, Col.R})

        Col = Color.FromName("Fuchsia")
        ColList.Add(New Integer() {Col.B, Col.G, Col.R})

        Dim form = New Graph(DisList, ColList)
        form.ShowDialog()
    End Sub

    Private Sub Count_Classification_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Main_Form.Controls.Remove(Me)
    End Sub
End Class