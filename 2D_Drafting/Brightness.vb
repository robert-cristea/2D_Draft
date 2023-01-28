Public Class Brightness
    Public brightness As Integer
    Public contrast As Integer
    Public gamma As Integer
    Public pictureBox As PictureBox
    Public OriginImag As Image
    Public InitialImage As Image

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub New(ByVal pictureBox As PictureBox, ByVal cur_image As Image, ByVal brightness As Integer, ByVal contrast As Integer, ByVal gamma As Integer)
        InitializeComponent()
        Me.pictureBox = pictureBox
        OriginImag = cur_image
        Me.brightness = brightness
        Me.contrast = contrast
        Me.gamma = gamma
        ID_LABEL_BRIGHTNESS.Text = brightness.ToString()
        ID_HSCROLL_BRIGHTNESS.Value = brightness
        ID_LABEL_CONTRAST.Text = contrast.ToString()
        ID_HSCROLL_CONTRAST.Value = contrast
        ID_LABEL_GAMMA.Text = gamma.ToString()
        ID_HSCROLL_GAMMA.Value = gamma

        InitialImage = AdjustBrightnessAndContrast(OriginImag, brightness, contrast, gamma)
        pictureBox.Image = InitialImage
    End Sub
    Private Sub ID_HSCROLL_BRIGHTNESS_Scroll(sender As Object, e As ScrollEventArgs) Handles ID_HSCROLL_BRIGHTNESS.Scroll
        brightness = ID_HSCROLL_BRIGHTNESS.Value
        Dim img = AdjustBrightnessAndContrast(OriginImag, brightness, contrast, gamma)
        pictureBox.Image = img
        ID_LABEL_BRIGHTNESS.Text = brightness.ToString()
    End Sub

    Private Sub ID_HSCROLL_CONTRAST_Scroll(sender As Object, e As ScrollEventArgs) Handles ID_HSCROLL_CONTRAST.Scroll
        contrast = ID_HSCROLL_CONTRAST.Value
        Dim img = AdjustBrightnessAndContrast(OriginImag, brightness, contrast, gamma)
        pictureBox.Image = img
        ID_LABEL_CONTRAST.Text = contrast.ToString()
    End Sub

    Private Sub ID_HSCROLL_GAMMA_Scroll(sender As Object, e As ScrollEventArgs) Handles ID_HSCROLL_GAMMA.Scroll
        gamma = ID_HSCROLL_GAMMA.Value
        Dim img = AdjustBrightnessAndContrast(OriginImag, brightness, contrast, gamma)
        pictureBox.Image = img
        ID_LABEL_GAMMA.Text = gamma.ToString()
    End Sub

    Private Sub ID_BTN_RESET_BRIGHTNESS_Click(sender As Object, e As EventArgs) Handles ID_BTN_RESET_BRIGHTNESS.Click
        brightness = 0
        ID_LABEL_BRIGHTNESS.Text = brightness.ToString()
        ID_HSCROLL_BRIGHTNESS.Value = brightness
        Dim img = AdjustBrightnessAndContrast(OriginImag, brightness, contrast, gamma)
        pictureBox.Image = img
    End Sub

    Private Sub ID_BTN_RESET_CONTRAST_Click(sender As Object, e As EventArgs) Handles ID_BTN_RESET_CONTRAST.Click
        contrast = 0
        ID_LABEL_CONTRAST.Text = contrast.ToString()
        ID_HSCROLL_CONTRAST.Value = contrast
        Dim img = AdjustBrightnessAndContrast(OriginImag, brightness, contrast, gamma)
        pictureBox.Image = img
    End Sub

    Private Sub ID_BTN_RESET_GAMMA_Click(sender As Object, e As EventArgs) Handles ID_BTN_RESET_GAMMA.Click
        gamma = 100
        ID_LABEL_GAMMA.Text = gamma.ToString()
        ID_HSCROLL_GAMMA.Value = gamma
        Dim img = AdjustBrightnessAndContrast(OriginImag, brightness, contrast, gamma)
        pictureBox.Image = img
    End Sub
End Class