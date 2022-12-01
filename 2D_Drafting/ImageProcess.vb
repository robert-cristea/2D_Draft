Imports System.IO
Imports Emgu.CV
Imports Emgu.CV.Face
Imports Emgu.CV.Structure
Imports Image = System.Drawing.Image

''' <summary>
''' This class contains all the functions for image processing.
''' </summary>
Public Module ImageProcess
    ''' <summary>
    ''' Clip the range of input
    ''' </summary>
    ''' <paramname="input">The input of function.</param>
    Public Function ColorRange(ByVal input As Double) As Byte
        input = Math.Min(Math.Max(input, 0), 255)
        Return input
    End Function

    ''' <summary>
    ''' return the value of interpolation function
    ''' </summary>
    ''' <paramname="x">The input of function.</param>
    Public Function Interpolation(ByVal x As Double) As Double
        x = (x - 100) / 100.0
        Dim y = Math.Pow(0.04, x)
        Return y
    End Function
    ''' <summary>
    ''' Adjust the brightness and contrast of image
    ''' </summary>
    ''' <paramname="src">The source image.</param>
    ''' <paramname="brightness">The brightness of dst image.</param>
    ''' <paramname="contrast">The contrast of dst image.</param>
    ''' <paramname="gamma">The gamma for gamma correction.</param>
    Public Function AdjustBrightnessAndContrast(ByVal scr As Image, ByVal brightness As Integer, ByVal contrast As Integer, ByVal gamma As Integer) As Image
        If brightness = 0 AndAlso contrast = 0 AndAlso gamma = 100 Then Return scr
        Dim bmpImage As Bitmap = New Bitmap(scr)
        Dim colorImage As Emgu.CV.Image(Of Bgr, Byte) = bmpImage.ToImage(Of Bgr, Byte)()
        bmpImage.Dispose()

        Dim B = brightness / 255.0
        Dim lC = contrast / 255.0
        Dim K = Math.Tan((45 + 44 * lC) / 180 * Math.PI)
        Dim G = Interpolation(gamma)

        Dim BCtrans = New Byte(255) {}

        For i = 0 To 255
            Dim val1 = (i - 127.5 * (1 - B)) * K + 127.5 * (1 + B)
            Dim val2 = Math.Pow(ColorRange(val1) / 255.0, G) * 255.0
            BCtrans(i) = ColorRange(val2)
        Next

        Dim color_data = colorImage.Data

        For y = 0 To colorImage.Height - 1
            For x = 0 To colorImage.Width - 1
                For c = 0 To 2
                    '''brightness and contrast
                    'double pixelValue = (color_data[y, x, c] - 127.5 * (1 - B)) * K + 127.5 * (1 + B);
                    '''gamma correction
                    'pixelValue = Math.Pow((ColorRange(pixelValue) / 255.0), G) * 255.0;
                    color_data(y, x, c) = BCtrans(color_data(y, x, c))
                Next

            Next
        Next


        Dim array As Byte() = colorImage.ToJpegData()

        colorImage.Dispose()
        Dim stream As MemoryStream = New MemoryStream(array)
        Dim img = Image.FromStream(stream)

        Return img
    End Function

    ''' <summary>
    ''' detect edge of selected region
    ''' </summary>
    ''' <paramname="src">The source image.</param>
    ''' <paramname="FirstPtOfEdge">The left top cornor of selected region.</param>
    ''' <paramname="SecondPtOfEdge">The right bottom cornor of selected region.</param>
    Public Function Canny(ByVal scr As Image, ByVal FirstPtOfEdge As Point, ByVal SecondPtOfEdge As Point) As Image
        Dim bmpImage As Bitmap = New Bitmap(scr)
        Dim grayImage As Emgu.CV.Image(Of Gray, Byte) = bmpImage.ToImage(Of Gray, Byte)()
        Dim colorImage As Emgu.CV.Image(Of Bgr, Byte) = bmpImage.ToImage(Of Bgr, Byte)()
        bmpImage.Dispose()

        Dim Width = scr.Width
        Dim Height = scr.Height
        Dim CropWidth = SecondPtOfEdge.X - FirstPtOfEdge.X
        Dim CropHeight = SecondPtOfEdge.Y - FirstPtOfEdge.Y
        Dim Region As Rectangle = New Rectangle(FirstPtOfEdge.X, FirstPtOfEdge.Y, CropWidth, CropHeight)
        grayImage.ROI = Region

        Dim cropped = grayImage.Copy()
        Dim tempimg As Emgu.CV.Image(Of Emgu.CV.Structure.Gray, Byte) = cropped
        CvInvoke.Canny(cropped, tempimg, 100, 200)

        Dim Data = colorImage.Data
        Dim Edge = tempimg.Data

        For x = 0 To CropWidth - 1
            For y = 0 To CropHeight - 1
                Dim OriX = FirstPtOfEdge.X + x
                Dim OriY = FirstPtOfEdge.Y + y

                If Edge(y, x, 0) <> 0 Then
                    Data(OriY, OriX, 0) = 0
                    Data(OriY, OriX, 1) = 0
                    Data(OriY, OriX, 2) = 255
                End If

            Next
        Next

        'Emgu.CV.CvInvoke.Imshow("original image", colorImage)
        Dim array As Byte() = colorImage.ToJpegData()

        colorImage.Dispose()
        Dim stream As MemoryStream = New MemoryStream(array)
        Dim img = Image.FromStream(stream)

        Return img
    End Function

    ''' <summary>
    ''' Convert system.drawing.Image to Mat
    ''' </summary>
    ''' <paramname="image">The source image.</param>
    Public Function GetMatFromSDImage(ByVal image As Image) As Mat
        If image Is Nothing Then
            Return Nothing
        End If
        Dim stride = 0
        Dim bmp As Bitmap = New Bitmap(image)

        Dim rect As Rectangle = New Rectangle(0, 0, bmp.Width, bmp.Height)
        Dim bmpData = bmp.LockBits(rect, Imaging.ImageLockMode.ReadWrite, bmp.PixelFormat)

        Dim pf = bmp.PixelFormat
        If pf = Imaging.PixelFormat.Format32bppArgb Then
            stride = bmp.Width * 4
        Else
            stride = bmp.Width * 3
        End If

        Dim cvImage As Emgu.CV.Image(Of Bgra, Byte) = New Emgu.CV.Image(Of Bgra, Byte)(bmp.Width, bmp.Height, stride, bmpData.Scan0)

        bmp.UnlockBits(bmpData)

        Return cvImage.Mat
    End Function
End Module

