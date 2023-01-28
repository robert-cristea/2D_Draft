Public Class ID_FORM_SCALE
    Public scaleStyle As String
    Public scaleValue As Integer
    Public scaleUnit As String
    Public Sub New(ByVal unit As String)
        InitializeComponent()
        scaleStyle = "horizontal"
        ID_COMBO_SCALE_STYLE.SelectedIndex = 0
        scaleValue = 0
        scaleUnit = unit
        ID_TEXT_UNIT.Text = scaleUnit
    End Sub
    Private Sub ID_COMBO_SCALE_STYLE_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ID_COMBO_SCALE_STYLE.SelectedIndexChanged
        Dim comboIndex = ID_COMBO_SCALE_STYLE.SelectedIndex
        If comboIndex = 0 Then
            scaleStyle = "horizontal"
        Else
            scaleStyle = "vertical"
        End If
    End Sub

    Private Sub ID_NUM_SCALE_ValueChanged(sender As Object, e As EventArgs) Handles ID_NUM_SCALE.ValueChanged
        scaleValue = CInt(ID_NUM_SCALE.Value)
    End Sub

    Private Sub ID_TEXT_UNIT_TextChanged(sender As Object, e As EventArgs) Handles ID_TEXT_UNIT.TextChanged
        scaleUnit = ID_TEXT_UNIT.Text
    End Sub
End Class