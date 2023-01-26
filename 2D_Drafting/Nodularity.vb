Imports System.Runtime.Remoting
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports Emgu.CV
Imports Emgu.CV.CvEnum
Imports Emgu.CV.DepthAI
Imports Emgu.CV.Structure

Public Structure DataRow
    Public Field As String
    Public TotalCnt As String
    Public NodeCnt As String
    Public Nodularity As String
    Public MaxArea As String
    Public MinArea As String
    Public MaxPer As String
    Public MinPer As String
End Structure
Public Class Nodularity
    Public OriImage As Emgu.CV.Image(Of Bgr, Byte)
    Public GrayImage As Emgu.CV.Image(Of Gray, Byte)
    Public BinaryImage As Emgu.CV.Image(Of Gray, Byte)
    Public ObjListTotal As List(Of BlobObj) = New List(Of BlobObj)
    Public Datarows As List(Of DataRow) = New List(Of DataRow)
    Public DisList As List(Of Double) = New List(Of Double)
    Public ColList As List(Of Integer()) = New List(Of Integer())

    Private IntensityUpper As Integer
    Private IntensityLower As Integer
    Private AreaLimit As Single
    Private RoundUpper As Single
    Private RoundLower As Single

    Private m_field As String
    Private m_totalCnt As Integer
    Private m_NodCnt As Integer
    Private m_NodRer As Double
    Private m_maxPer As Single
    Private m_minPer As Single
    Private m_maxArea As Single
    Private m_minArea As Single

    Private DistingshType As Boolean

    Public Sub New()
        InitializeComponent()
    End Sub
    Private Sub Nodularity_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim scr = Main_Form.origin_image(Main_Form.tab_index).ToBitmap()
            Dim bmpImage As Bitmap = New Bitmap(scr)
            OriImage = bmpImage.ToImage(Of Bgr, Byte)()
            bmpImage.Dispose()
            GrayImage = getGrayScale(OriImage)
            BinaryImage = GrayImage.CopyBlank()
        Catch ex As Exception

        End Try

        ColList.Add(New Integer() {0, 0, 255})
        ColList.Add(New Integer() {255, 0, 0})
    End Sub

    Private Sub GetBinaryImage()
        Try
            ObjListTotal.Clear()
            Dim resizedBinary = BinaryImage.Copy()
            Dim sz = New Size(Main_Form.resized_image(Main_Form.tab_index).Width, Main_Form.resized_image(Main_Form.tab_index).Height)
            CvInvoke.Resize(BinaryImage, resizedBinary, sz)
            Dim BinImg = GetImageFromEmgu(resizedBinary)
            Dim outPut = OverLapSegToOri(Main_Form.resized_image(Main_Form.tab_index).ToBitmap(), BinImg)
            Main_Form.ID_PICTURE_BOX(Main_Form.tab_index).Image = outPut
            Main_Form.current_image(Main_Form.tab_index) = GetMatFromSDImage(outPut)
            BlobDetection(OriImage, BinaryImage, ObjListTotal, AreaLimit)

            Dim maxArea = 0
            Dim minArea = 99999
            For i = 0 To ObjListTotal.Count - 1
                Dim Obj = ObjListTotal(i)
                If maxArea < Obj.Area Then maxArea = Obj.Area
                If minArea > Obj.Area Then minArea = Obj.Area
            Next
            LabMaxArea.Text = maxArea.ToString()
            LabMinArea.Text = minArea.ToString()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Initialize()
        IntensityLower = 0
        IntensityUpper = 255
        RoundLower = 0
        RoundUpper = 1
        m_totalCnt = 0
        m_NodCnt = 0
        m_maxArea = 0
        m_minArea = 0
        m_maxPer = 0
        m_minPer = 0
        AreaLimit = 0
        m_NodRer = 0
        ObjListTotal.Clear()

        TxtField.Text = ""
        TxtTotal.Text = ""
        TxtMaxPer.Text = ""
        TxtMinPer.Text = ""
        TxtMaxArea.Text = ""
        TxtMinArea.Text = ""
        TxtNodCnt.Text = ""
        TxtNodPer.Text = ""
        TxtLimit.Text = ""
        LabMaxArea.Text = ""
        LabMinArea.Text = ""
    End Sub
    Private Sub GetMinMax()
        Dim fieldID = Datarows.Count
        m_totalCnt = ObjListTotal.Count
        m_NodCnt = Main_Form.Obj_Seg.BlobSegObj.BlobList.Count
        m_maxPer = 0
        m_minPer = 9999999
        m_maxArea = 0
        m_minArea = 99999999

        For i = 0 To m_NodCnt - 1
            Dim Obj = Main_Form.Obj_Seg.BlobSegObj.BlobList(i)
            If m_maxPer < Obj.Perimeter Then m_maxPer = Obj.Perimeter
            If m_minPer > Obj.Perimeter Then m_minPer = Obj.Perimeter
            If m_maxArea < Obj.Area Then m_maxArea = Obj.Area
            If m_minArea > Obj.Area Then m_minArea = Obj.Area
        Next
        m_NodRer = Math.Round(m_NodCnt / CSng(m_totalCnt) * 100, 2)

        TxtField.Text = "Field" & fieldID
        TxtTotal.Text = m_totalCnt.ToString()
        TxtMaxPer.Text = m_maxPer.ToString()
        TxtMinPer.Text = m_minPer.ToString()
        TxtMaxArea.Text = m_maxArea.ToString()
        TxtMinArea.Text = m_minArea.ToString()
        TxtNodCnt.Text = m_NodCnt.ToString()
        TxtNodPer.Text = m_NodRer.ToString()
        TxtLimit.Text = (RoundLower * 100).ToString() & "-" & (RoundUpper * 100).ToString()
    End Sub

    Private Sub AddRow()
        Dim row = New DataRow
        Dim fieldID = Datarows.Count
        row.Field = "Field" & fieldID
        row.TotalCnt = m_totalCnt.ToString()
        row.MaxPer = m_maxPer.ToString()
        row.MinPer = m_minPer.ToString()
        row.MaxArea = m_maxArea.ToString()
        row.MinArea = m_minArea.ToString()
        row.NodeCnt = m_NodCnt.ToString()
        row.Nodularity = m_NodRer.ToString()
        Datarows.Add(row)
    End Sub
    Private Sub LoadDataToGridView()
        DataGridView1.Rows.Clear()
        Dim str_item = New String(7) {}

        For i = 0 To Datarows.Count - 1
            Dim row = Datarows(i)
            str_item(0) = row.Field
            str_item(1) = row.TotalCnt
            str_item(2) = row.NodeCnt
            str_item(3) = row.Nodularity
            str_item(4) = row.MaxArea
            str_item(5) = row.MinArea
            str_item(6) = row.MaxPer
            str_item(7) = row.MinPer

            DataGridView1.Rows.Add(str_item)
        Next
    End Sub
    Private Sub BtnAuto_Click(sender As Object, e As EventArgs) Handles BtnAuto.Click
        CvInvoke.Threshold(GrayImage, BinaryImage, 0, 255, ThresholdType.Otsu)
        GetBinaryImage()
    End Sub

    Private Sub BtnManual_Click(sender As Object, e As EventArgs) Handles BtnManual.Click
        Try
            Dim form = New Intensity()
            If form.ShowDialog() = DialogResult.OK Then
                IntensityUpper = form.Upper
                IntensityLower = form.Lower
            End If
            BinaryImage = GetBinaryWith2Thr(GrayImage, IntensityLower, IntensityUpper)
            GetBinaryImage()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BtnSetLimit_Click(sender As Object, e As EventArgs) Handles BtnSetLimit.Click
        Dim form = New RoundnessLimit(IntensityUpper, IntensityLower, AreaLimit, DistingshType)
        If form.ShowDialog() = DialogResult.OK Then
            RoundUpper = form.RoundUpper
            RoundLower = form.RoundLower
            Main_Form.Obj_Seg.BlobSegObj.BlobList.Clear()
            Main_Form.Obj_Seg.BlobSegObj.BlobList = form.ObjList.ToList()
            GetMinMax()
        End If
    End Sub

    Private Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click
        AddRow()
        LoadDataToGridView()
    End Sub

    Private Sub BtnUndo_Click(sender As Object, e As EventArgs) Handles BtnUndo.Click
        Initialize()
        Main_Form.ID_PICTURE_BOX(Main_Form.tab_index).Image = Main_Form.resized_image(Main_Form.tab_index).ToBitmap()
    End Sub

    Private Sub BtnGraph_Click(sender As Object, e As EventArgs) Handles BtnGraph.Click
        DisList.Clear()
        If m_NodRer > 0 Then
            DisList.Add(m_NodRer)
            DisList.Add(100 - m_NodRer)
        End If

        Dim form = New Graph(DisList, ColList)
        form.ShowDialog()
    End Sub

    Private Sub BtnReport_Click(sender As Object, e As EventArgs) Handles BtnReport.Click
        Dim filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*"
        Dim title = "Save"
        SaveListToReport(Main_Form.ID_PICTURE_BOX(Main_Form.tab_index).Image, DataGridView1, filter, title)
    End Sub

    Private Sub BtnExcel_Click(sender As Object, e As EventArgs) Handles BtnExcel.Click
        Dim filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*"
        Dim title = "Save"
        SaveListToExcel(DataGridView1, filter, title)
    End Sub

    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles BtnExit.Click
        Me.Close()
    End Sub

    Private Sub BtnRound_Click(sender As Object, e As EventArgs) Handles BtnRound.Click
        GroupName.Text = "Roundness Limit"
        DistingshType = False
    End Sub

    Private Sub BtnPerArea_Click(sender As Object, e As EventArgs) Handles BtnPerArea.Click
        GroupName.Text = "Perimeter/Area Limit"
        DistingshType = True
    End Sub

    Private Sub BtnRange_Click(sender As Object, e As EventArgs) Handles BtnRange.Click
        Dim form = New RangeDistribution()
        If form.ShowDialog() = DialogResult.OK Then

        End If
    End Sub
End Class