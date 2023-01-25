Imports System.Windows.Forms.DataVisualization.Charting

Public Class Graph
    Private PointVal As List(Of Double) = New List(Of Double)
    Private PointCol As List(Of Integer()) = New List(Of Integer())
    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub New(val As List(Of Double), col As List(Of Integer()))
        InitializeComponent()
        PointVal = val.ToList()
        PointCol = col.ToList
    End Sub

    Private Sub Graph_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Chart1.Series.Clear()
        Chart1.Series.Add("Data")

        For i = 0 To PointVal.Count
            Dim dp = New DataPoint()
            If i = 0 Then
                dp.SetValueXY(i, 0)
            Else
                dp.SetValueXY(i, PointVal(i - 1))
                'dp.Color = Color.FromName(PointCol(i - 1))
                dp.Color = Color.FromArgb(PointCol(i - 1)(2), PointCol(i - 1)(1), PointCol(i - 1)(0))
            End If

            Chart1.Series("Data").Points.Add(dp)
            dp.Dispose()
        Next
    End Sub

    Private Sub UpdateGraph()

        If RadioBar.Checked Then
            Chart1.Series("Data").ChartType = SeriesChartType.Column
        Else
            Chart1.Series("Data").ChartType = SeriesChartType.Line
        End If
    End Sub

    Private Sub RadioBar_CheckedChanged(sender As Object, e As EventArgs) Handles RadioBar.CheckedChanged
        UpdateGraph()
    End Sub

    Private Sub RadioLine_CheckedChanged(sender As Object, e As EventArgs) Handles RadioLine.CheckedChanged
        UpdateGraph()
    End Sub

    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles BtnExit.Click
        Me.Close()
    End Sub
End Class