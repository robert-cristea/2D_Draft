Public Class ID_FORM_SCALE
    Public scale_style As String
    Public scale_value As Integer
    Public scale_unit As String
    Public Sub New(ByVal unit As String)
        InitializeComponent()
        scale_style = "horizontal"
        ID_COMBO_SCALE_STYLE.SelectedIndex = 0
        scale_value = 0
        scale_unit = unit
        ID_TEXT_UNIT.Text = scale_unit
    End Sub
    Private Sub ID_COMBO_SCALE_STYLE_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ID_COMBO_SCALE_STYLE.SelectedIndexChanged
        Dim comboIndex = ID_COMBO_SCALE_STYLE.SelectedIndex
        If comboIndex = 0 Then
            scale_style = "horizontal"
        Else
            scale_style = "vertical"
        End If
    End Sub

    Private Sub ID_NUM_SCALE_ValueChanged(sender As Object, e As EventArgs) Handles ID_NUM_SCALE.ValueChanged
        scale_value = CInt(ID_NUM_SCALE.Value)
    End Sub

    Private Sub ID_TEXT_UNIT_TextChanged(sender As Object, e As EventArgs) Handles ID_TEXT_UNIT.TextChanged
        scale_unit = ID_TEXT_UNIT.Text
    End Sub
End Class