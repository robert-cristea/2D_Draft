Imports System.Runtime.Remoting
Imports Emgu.CV
Imports Emgu.CV.Structure
Imports GeometRi

Public Class Phase_Segmentation
    Public ObjSel As MeasureObject
    Public OriImage As Emgu.CV.Image(Of Bgr, Byte)
    Public GrayImage As Emgu.CV.Image(Of Gray, Byte)
    Public GrayCrop As Emgu.CV.Image(Of Gray, Byte)
    Public RadioStateSeg As Integer
    Public RadioStatePreview As Integer
    Public PhaseNum As Integer
    Public PhaseVal As List(Of Integer) = New List(Of Integer)
    Public PhaseCol As List(Of String) = New List(Of String)
    Public PhaseArea As List(Of Integer) = New List(Of Integer)
    Private PhaseSel As List(Of Integer) = New List(Of Integer)
    Private MouseDownflag As Boolean
    Private CurSelPhaseLine As Integer
    Private CurSelPhase As Integer
    Private MouseClicked As Integer
    Private IntialPhaseVal As Integer
    Private SetCol As String
    Private SelectPart As Boolean
    Private PrevSelectPart As Boolean
    Private FirstPt As Point
    Private SecondPt As Point

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub New(obj As SegObject)
        InitializeComponent()

        PhaseNum = PhaseVal.Count
    End Sub

    Private Sub Phase_Segmentation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RadioStateSeg = 0
        RadioStatePreview = 2
        RadioAll.Checked = True
        RadioPreAll.Checked = True

        Try
            Timer1.Interval = 30
            Timer1.Start()
            Dim scr = Main_Form.resized_image(Main_Form.tab_index).ToBitmap()
            Dim bmpImage As Bitmap = New Bitmap(scr)
            OriImage = bmpImage.ToImage(Of Bgr, Byte)()
            bmpImage.Dispose()
            GrayImage = getGrayScale(OriImage)
            HistogramBox1.GenerateHistograms(GrayImage, 256)
            HistogramBox1.Refresh()
            MouseDownflag = False
            If PhaseVal.Count > 0 Then
                For i = 0 To PhaseVal.Count
                    Dim Item = "Phase" & i
                    ComboBox1.Items.Add(Item)
                Next
            End If

            DrawResult()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If Main_Form.EdgeRegionDrawed And Main_Form.EdgeRegionDrawReady Then
            SelectPart = True
            If Math.Abs(Main_Form.SecondPtOfEdge.X - Main_Form.FirstPtOfEdge.X) > 10 And Math.Abs(Main_Form.SecondPtOfEdge.Y - Main_Form.FirstPtOfEdge.Y) > 10 Then
                FirstPt = Main_Form.FirstPtOfEdge
                SecondPt = Main_Form.SecondPtOfEdge
            End If
        ElseIf RadioStateSeg = 0 Then
                SelectPart = False
        End If

        If PrevSelectPart <> SelectPart Then
            If SelectPart Then
                Dim Rect = New Rectangle(FirstPt.X, FirstPt.Y, SecondPt.X - FirstPt.X, SecondPt.Y - FirstPt.Y)
                GrayCrop = GrayImage.Copy()
                GrayCrop.ROI = Rect
                HistogramBox1.GenerateHistograms(GrayCrop, 256)
                HistogramBox1.Refresh()
            Else
                HistogramBox1.GenerateHistograms(GrayImage, 256)
                HistogramBox1.Refresh()
            End If
            DrawResult()
        End If

        PrevSelectPart = SelectPart

    End Sub
    Private Sub CheckRadioStateSeg()
        If RadioAll.Checked = True Then
            RadioStateSeg = 0
            Main_Form.EdgeRegionDrawReady = False
        Else
            RadioStateSeg = 1
            Main_Form.EdgeRegionDrawReady = True
        End If
    End Sub

    Private Sub CheckRadioStatePreview()
        PhaseSel.Clear()
        If RadioNone.Checked = True Then
            RadioStatePreview = 0
            For i = 0 To PhaseCol.Count - 1
                PhaseSel.Add(0)
            Next
        ElseIf RadioCurrent.Checked = True Then
            RadioStatePreview = 1
            For i = 0 To PhaseCol.Count - 1
                If i = CurSelPhase Then
                    PhaseSel.Add(1)
                Else
                    PhaseSel.Add(0)
                End If
            Next
        Else
            RadioStatePreview = 2
            For i = 0 To PhaseCol.Count - 1
                PhaseSel.Add(1)
            Next
        End If
        PrviewSegmentation()
    End Sub
    Private Sub RadioAll_CheckedChanged(sender As Object, e As EventArgs) Handles RadioAll.CheckedChanged
        CheckRadioStateSeg()
    End Sub

    Private Sub RadioSelectedPart_CheckedChanged(sender As Object, e As EventArgs) Handles RadioSelectedPart.CheckedChanged
        CheckRadioStateSeg()
    End Sub

    Private Sub RadioNone_CheckedChanged(sender As Object, e As EventArgs) Handles RadioNone.CheckedChanged
        DrawResult()
    End Sub

    Private Sub RadioCurrent_CheckedChanged(sender As Object, e As EventArgs) Handles RadioCurrent.CheckedChanged
        DrawResult()
    End Sub

    Private Sub RadioPreAll_CheckedChanged(sender As Object, e As EventArgs) Handles RadioPreAll.CheckedChanged
        DrawResult()
    End Sub

    Private Sub LoadDataToGridView()
        DataGridView1.Rows.Clear()
        Dim str_item = New String(2) {}
        Dim squrare = Main_Form.resized_image(Main_Form.tab_index).Width * Main_Form.resized_image(Main_Form.tab_index).Height

        For i = 0 To PhaseCol.Count - 1
            str_item(0) = ComboBox1.Items(i).ToString
            str_item(1) = PhaseArea(i).ToString
            str_item(2) = Math.Round(CDbl(PhaseArea(i) / squrare) * 100, 2).ToString()

            DataGridView1.Rows.Add(str_item)
        Next
    End Sub

    Private Sub PrviewSegmentation()
        Dim image = Main_Form.resized_image(Main_Form.tab_index).ToBitmap()
        Dim flag As Boolean = False
        If Main_Form.EdgeRegionDrawed And Main_Form.EdgeRegionDrawReady Then flag = True
        Dim output = MultiSegment(image, PhaseVal, PhaseCol, PhaseArea, PhaseSel, FirstPt, SecondPt, flag)
        Main_Form.ID_PICTURE_BOX(Main_Form.tab_index).Image = output
        Main_Form.current_image(Main_Form.tab_index) = GetMatFromSDImage(output)
    End Sub
    Private Sub DrawResult()
        DrawProcess(PicBoxProgress, PhaseVal, PhaseCol)
        CheckRadioStatePreview()
        LoadDataToGridView()
    End Sub

    Private Sub BtnNew_Click(sender As Object, e As EventArgs) Handles BtnNew.Click
        Dim cur_col As String
        Dim item As String
        If PhaseNum = 0 Then
            PhaseVal.Add(0)
            PhaseVal.Add(128)
            cur_col = Main_Form.Col_list(PhaseNum + 1)
            If SetCol = "" Then
                PhaseCol.Add(cur_col)
            Else
                PhaseCol.Add(SetCol)
            End If

            item = "Phase" & PhaseNum
            ComboBox1.Items.Add(item)
            ComboBox1.SelectedIndex = PhaseNum
            NumMin.Value = 0
            NumMax.Value = 128

        Else
            If PhaseVal(PhaseVal.Count - 1) = 256 Then
                Return
            End If
            PhaseVal.Add(256)
            cur_col = Main_Form.Col_list(PhaseNum + 1)
            If SetCol = "" Then
                PhaseCol.Add(cur_col)
            Else
                PhaseCol.Add(SetCol)
            End If
            item = "Phase" & PhaseNum
            ComboBox1.Items.Add(item)
            ComboBox1.SelectedIndex = PhaseNum
            NumMin.Value = CInt(PhaseVal(PhaseVal.Count - 2))
            NumMax.Value = 256

        End If
        PhaseNum += 1
        SetCol = ""
        DrawResult()
        BtnCol.BackColor = Color.FromName(PhaseCol(PhaseCol.Count - 1))

    End Sub

    Private Sub BtnDel_Click(sender As Object, e As EventArgs) Handles BtnDel.Click
        Dim cur_col As String = "white"
        Dim item As String
        If PhaseNum > 0 Then
            PhaseVal.RemoveAt(PhaseVal.Count - 1)
            PhaseCol.RemoveAt(PhaseCol.Count - 1)
            If PhaseCol.Count - 1 >= 0 Then
                cur_col = PhaseCol(PhaseCol.Count - 1)
            End If

            ComboBox1.Items.RemoveAt(ComboBox1.Items.Count - 1)
            PhaseNum -= 1
            ComboBox1.SelectedIndex = ComboBox1.Items.Count - 1
            If PhaseNum < 1 Then
                NumMin.Value = 0
            Else
                NumMin.Value = CInt(PhaseVal(PhaseVal.Count - 2))
            End If
            If PhaseNum > 0 Then
                NumMax.Value = CInt(PhaseVal(PhaseVal.Count - 1))
                'Else
                '    NumMax.Value = 255
            End If
            If PhaseCol.Count = 0 Then
                PhaseVal.Clear()
            End If
        End If

        DrawResult()
        BtnCol.BackColor = Color.FromName(cur_col)
    End Sub

    Private Sub UpdateNumValue()
        If CurSelPhase = CurSelPhaseLine Then
            NumMin.Value = PhaseVal(CurSelPhaseLine)
            NumMax.Value = PhaseVal(CurSelPhaseLine + 1)
        Else
            NumMin.Value = PhaseVal(CurSelPhaseLine - 1)
            NumMax.Value = PhaseVal(CurSelPhaseLine)
        End If
    End Sub
    Private Sub PicBoxProgress_MouseDown(sender As Object, e As MouseEventArgs) Handles PicBoxProgress.MouseDown
        MouseDownflag = True
        MouseClicked = e.X * 256 / PicBoxProgress.Width
        Dim minDis = 256

        For i = 0 To PhaseVal.Count - 1
            If Math.Abs(MouseClicked - PhaseVal(i)) < minDis Then
                Dim dis = MouseClicked - PhaseVal(i)
                minDis = Math.Abs(dis)
                CurSelPhaseLine = i
                IntialPhaseVal = PhaseVal(i)
                If dis > 0 And CurSelPhaseLine <> PhaseVal.Count - 1 Then
                    CurSelPhase = i
                Else
                    CurSelPhase = i - 1
                End If
            End If
        Next
        If CurSelPhase < 0 Then
            CurSelPhase = 0
        End If
        ComboBox1.SelectedIndex = CurSelPhase
        UpdateNumValue()
    End Sub

    Private Sub PicBoxProgress_MouseMove(sender As Object, e As MouseEventArgs) Handles PicBoxProgress.MouseMove
        If MouseDownflag Then
            Dim curPos = e.X * 256 / PicBoxProgress.Width
            Dim delta = curPos - MouseClicked

            PhaseVal(CurSelPhaseLine) = IntialPhaseVal + delta
            If CurSelPhaseLine + 1 < PhaseVal.Count Then
                PhaseVal(CurSelPhaseLine) = Math.Min(PhaseVal(CurSelPhaseLine), PhaseVal(CurSelPhaseLine + 1) - 1)
            Else
                PhaseVal(CurSelPhaseLine) = Math.Min(PhaseVal(CurSelPhaseLine), 256)
            End If
            If CurSelPhaseLine - 1 >= 0 Then
                PhaseVal(CurSelPhaseLine) = Math.Max(PhaseVal(CurSelPhaseLine), PhaseVal(CurSelPhaseLine - 1) + 1)
            Else
                PhaseVal(CurSelPhaseLine) = Math.Max(PhaseVal(CurSelPhaseLine), 1)
            End If

            DrawResult()

            UpdateNumValue()
        End If
    End Sub

    Private Sub PicBoxProgress_MouseUp(sender As Object, e As MouseEventArgs) Handles PicBoxProgress.MouseUp
        MouseDownflag = False
        MouseClicked = 0
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        CurSelPhase = ComboBox1.SelectedIndex
        NumMin.Value = PhaseVal(CurSelPhase)
        NumMax.Value = PhaseVal(CurSelPhase + 1)
        CheckRadioStatePreview()
    End Sub

    Private Sub ComboBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles ComboBox1.KeyDown
        Dim item = ComboBox1.Text
        ComboBox1.Items(CurSelPhase) = item
        DataGridView1.Rows(CurSelPhase).Cells(0).Value = item
    End Sub

    Private Sub NumMin_ValueChanged(sender As Object, e As EventArgs) Handles NumMin.ValueChanged
        PhaseVal(CurSelPhase) = NumMin.Value
        DrawResult()
    End Sub

    Private Sub NumMax_ValueChanged(sender As Object, e As EventArgs) Handles NumMax.ValueChanged
        If PhaseVal.Count > 0 Then
            PhaseVal(CurSelPhase + 1) = NumMax.Value
        End If
        DrawResult()
    End Sub

    Private Sub BtnExcel_Click(sender As Object, e As EventArgs) Handles BtnExcel.Click
        Dim filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*"
        Dim title = "Save"
        SaveListToExcel(DataGridView1, filter, title)
    End Sub

    Private Sub BtnReport_Click(sender As Object, e As EventArgs) Handles BtnReport.Click
        Dim filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*"
        Dim title = "Save"
        SaveListToReport(Main_Form.ID_PICTURE_BOX(Main_Form.tab_index).Image, DataGridView1, filter, title)
    End Sub

    Private Sub BtnCol_Click(sender As Object, e As EventArgs) Handles BtnCol.Click
        Dim clrDialog As ColorDialog = New ColorDialog()
        'show the colour dialog and check that user clicked ok
        If clrDialog.ShowDialog() = DialogResult.OK Then
            'save the colour that the user chose
            'If PhaseCol.Count > 0 Then
            '    PhaseCol(CurSelPhase) = clrDialog.Color.Name
            'End If
            SetCol = clrDialog.Color.Name
            BtnCol.BackColor = clrDialog.Color
            DrawResult()
        End If
    End Sub

    Private Sub BtnOriImg_Click(sender As Object, e As EventArgs) Handles BtnOriImg.Click
        RadioNone.Checked = True
        CheckRadioStatePreview()
    End Sub

    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles BtnExit.Click
        Me.Close()
    End Sub

    Private Sub BtnSelPha_Click(sender As Object, e As EventArgs) Handles BtnSelPha.Click
        CurSelPhase = GetCurrentSelPhase(GrayImage, Main_Form.m_cur_drag, PhaseVal)

        RadioCurrent.Checked = True
        CheckRadioStatePreview()
    End Sub

    Private Sub Phase_Segmentation_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

    End Sub

    Private Sub BtnGraph_Click(sender As Object, e As EventArgs) Handles BtnGraph.Click
        Dim area_percent As List(Of Double) = New List(Of Double)
        Dim square = OriImage.Width * OriImage.Height
        For i = 0 To PhaseArea.Count - 1
            area_percent.Add(CDbl(PhaseArea(i) / square) * 100)
        Next
        Dim form = New Graph(area_percent, PhaseCol)
        form.ShowDialog()
    End Sub
End Class