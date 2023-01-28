Public Class Resize
    Public PerHor As Integer
    Public PerVer As Integer
    Public PixHor As Integer
    Public PixVer As Integer
    Public MaintainRatio As Boolean
    Private ImgWidth As Integer
    Private ImgHeight As Integer
    Public RadioState As Boolean

    Private Sub Resize_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ImgWidth = Main_Form.resizedImage.Width
        ImgHeight = Main_Form.resizedImage.Height
    End Sub
    Private Sub RadioPercent_CheckedChanged(sender As Object, e As EventArgs) Handles RadioPercent.CheckedChanged
        If RadioPercent.Checked Then
            NumHor.Value = 100
            NumVer.Value = 100
            PerHor = 100
            PerVer = 100
            RadioState = False
        End If
    End Sub

    Private Sub RadioPixel_CheckedChanged(sender As Object, e As EventArgs) Handles RadioPixel.CheckedChanged
        If RadioPixel.Checked Then
            NumHor.Value = ImgWidth
            NumVer.Value = ImgHeight
            PixHor = ImgWidth
            PixVer = ImgHeight
            RadioState = True
        End If
    End Sub

    Private Sub NumHor_ValueChanged(sender As Object, e As EventArgs) Handles NumHor.ValueChanged
        If MaintainRatio Then
            If RadioPercent.Checked Then
                PerHor = NumHor.Value
                PerVer = NumHor.Value
                NumVer.Value = PerVer
            Else
                PixHor = NumHor.Value
                PixVer = PixHor * ImgHeight / ImgWidth
                NumVer.Value = PixVer
            End If
        Else
            If RadioPercent.Checked Then
                PerHor = NumHor.Value
            Else
                PixHor = NumHor.Value
            End If
        End If
    End Sub

    Private Sub NumVer_ValueChanged(sender As Object, e As EventArgs) Handles NumVer.ValueChanged
        If MaintainRatio Then
            If RadioPercent.Checked Then
                PerHor = NumVer.Value
                PerVer = NumVer.Value
                NumHor.Value = PerVer
            Else
                PixVer = NumVer.Value
                PixHor = PixVer * ImgWidth / ImgHeight
                NumHor.Value = PixHor
            End If
        Else
            If RadioPercent.Checked Then
                PerVer = NumVer.Value
            Else
                PixVer = NumVer.Value
            End If
        End If
    End Sub

    Private Sub CheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox.CheckedChanged
        If CheckBox.Checked Then
            MaintainRatio = True
        Else
            MaintainRatio = False
        End If
    End Sub


End Class