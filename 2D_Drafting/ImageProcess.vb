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
    Public Function Canny(ByVal scr As Image, ByVal FirstPtOfEdge As Point, ByVal SecondPtOfEdge As Point) As C_CurveObject
        Dim bmpImage As Bitmap = New Bitmap(scr)
        Dim grayImage As Emgu.CV.Image(Of Gray, Byte) = bmpImage.ToImage(Of Gray, Byte)()
        Dim colorImage As Emgu.CV.Image(Of Bgr, Byte) = bmpImage.ToImage(Of Bgr, Byte)()
        bmpImage.Dispose()

        Dim C_Curve As C_CurveObject() = New C_CurveObject(1000) {}
        For i = 0 To 1000
            C_Curve(i) = New C_CurveObject()
        Next
        Dim Width = scr.Width
        Dim Height = scr.Height
        Dim CropWidth = SecondPtOfEdge.X - FirstPtOfEdge.X
        Dim CropHeight = SecondPtOfEdge.Y - FirstPtOfEdge.Y
        Dim Region As Rectangle = New Rectangle(FirstPtOfEdge.X, FirstPtOfEdge.Y, CropWidth, CropHeight)
        grayImage.ROI = Region

        Dim cropped = grayImage.Copy()
        Dim tempimg As Emgu.CV.Image(Of Emgu.CV.Structure.Gray, Byte) = cropped
        CvInvoke.MedianBlur(cropped, cropped, 3)
        'Emgu.CV.CvInvoke.Imshow("original image", cropped)
        CvInvoke.Canny(cropped, tempimg, 100, 200)

        Dim Edge = tempimg.Data

        Dim totalCnt As Integer = 0
        Dim nX2, nY2, nX3, nY3, nStackNum, tempX, tempY As Integer
        Dim iContinue As Boolean
        'get connected elements
        For x = 0 To CropWidth - 1
            For y = 0 To CropHeight - 1
                Dim OriX = FirstPtOfEdge.X + x
                Dim OriY = FirstPtOfEdge.Y + y

                If Edge(y, x, 0) <> 0 Then
                    Dim Pos = New PointF(OriX / CDbl(Width), OriY / CDbl(Height))
                    C_Curve(totalCnt).CurvePoint(C_Curve(totalCnt).CPointIndx) = Pos
                    C_Curve(totalCnt).CPointIndx += 1
                    Edge(y, x, 0) = 0
                    nX3 = x
                    nY3 = y
                    While (1)
                        nX2 = nX3 - 1
                        nY2 = nY3 - 1
                        iContinue = True
                        nStackNum = 0
                        If nX2 < 0 Or nX2 > CropWidth - 1 Or nY2 < 0 Or nY2 > CropHeight - 1 Then
                            iContinue = False
                        Else
                            If Edge(nY2, nX2, 0) = 0 Then
                                iContinue = False
                            End If
                        End If
                        If iContinue Then
                            Pos = New PointF((FirstPtOfEdge.X + nX2) / CDbl(Width), (FirstPtOfEdge.Y + nY2) / CDbl(Height))
                            C_Curve(totalCnt).CurvePoint(C_Curve(totalCnt).CPointIndx) = Pos
                            C_Curve(totalCnt).CPointIndx += 1
                            Edge(nY2, nX2, 0) = 0
                            nStackNum += 1
                            tempX = nX2 : tempY = nY2
                        End If

                        nX2 = nX3
                        nY2 = nY3 - 1
                        iContinue = True
                        If nX2 < 0 Or nX2 > CropWidth - 1 Or nY2 < 0 Or nY2 > CropHeight - 1 Then
                            iContinue = False
                        Else
                            If Edge(nY2, nX2, 0) = 0 Then
                                iContinue = False
                            End If
                        End If
                        If iContinue Then
                            Pos = New PointF((FirstPtOfEdge.X + nX2) / CDbl(Width), (FirstPtOfEdge.Y + nY2) / CDbl(Height))
                            C_Curve(totalCnt).CurvePoint(C_Curve(totalCnt).CPointIndx) = Pos
                            C_Curve(totalCnt).CPointIndx += 1
                            Edge(nY2, nX2, 0) = 0
                            nStackNum += 1
                            tempX = nX2 : tempY = nY2
                        End If

                        nX2 = nX3 + 1
                        nY2 = nY3 - 1
                        iContinue = True
                        If nX2 < 0 Or nX2 > CropWidth - 1 Or nY2 < 0 Or nY2 > CropHeight - 1 Then
                            iContinue = False
                        Else
                            If Edge(nY2, nX2, 0) = 0 Then
                                iContinue = False
                            End If
                        End If
                        If iContinue Then
                            Pos = New PointF((FirstPtOfEdge.X + nX2) / CDbl(Width), (FirstPtOfEdge.Y + nY2) / CDbl(Height))
                            C_Curve(totalCnt).CurvePoint(C_Curve(totalCnt).CPointIndx) = Pos
                            C_Curve(totalCnt).CPointIndx += 1
                            Edge(nY2, nX2, 0) = 0
                            nStackNum += 1
                            tempX = nX2 : tempY = nY2
                        End If

                        nX2 = nX3 - 1
                        nY2 = nY3
                        iContinue = True
                        If nX2 < 0 Or nX2 > CropWidth - 1 Or nY2 < 0 Or nY2 > CropHeight - 1 Then
                            iContinue = False
                        Else
                            If Edge(nY2, nX2, 0) = 0 Then
                                iContinue = False
                            End If
                        End If
                        If iContinue Then
                            Pos = New PointF((FirstPtOfEdge.X + nX2) / CDbl(Width), (FirstPtOfEdge.Y + nY2) / CDbl(Height))
                            C_Curve(totalCnt).CurvePoint(C_Curve(totalCnt).CPointIndx) = Pos
                            C_Curve(totalCnt).CPointIndx += 1
                            Edge(nY2, nX2, 0) = 0
                            nStackNum += 1
                            tempX = nX2 : tempY = nY2
                        End If

                        nX2 = nX3 + 1
                        nY2 = nY3
                        iContinue = True
                        If nX2 < 0 Or nX2 > CropWidth - 1 Or nY2 < 0 Or nY2 > CropHeight - 1 Then
                            iContinue = False
                        Else
                            If Edge(nY2, nX2, 0) = 0 Then
                                iContinue = False
                            End If
                        End If
                        If iContinue Then
                            Pos = New PointF((FirstPtOfEdge.X + nX2) / CDbl(Width), (FirstPtOfEdge.Y + nY2) / CDbl(Height))
                            C_Curve(totalCnt).CurvePoint(C_Curve(totalCnt).CPointIndx) = Pos
                            C_Curve(totalCnt).CPointIndx += 1
                            Edge(nY2, nX2, 0) = 0
                            nStackNum += 1
                            tempX = nX2 : tempY = nY2
                        End If

                        nX2 = nX3 - 1
                        nY2 = nY3 + 1
                        iContinue = True
                        If nX2 < 0 Or nX2 > CropWidth - 1 Or nY2 < 0 Or nY2 > CropHeight - 1 Then
                            iContinue = False
                        Else
                            If Edge(nY2, nX2, 0) = 0 Then
                                iContinue = False
                            End If
                        End If
                        If iContinue Then
                            Pos = New PointF((FirstPtOfEdge.X + nX2) / CDbl(Width), (FirstPtOfEdge.Y + nY2) / CDbl(Height))
                            C_Curve(totalCnt).CurvePoint(C_Curve(totalCnt).CPointIndx) = Pos
                            C_Curve(totalCnt).CPointIndx += 1
                            Edge(nY2, nX2, 0) = 0
                            nStackNum += 1
                            tempX = nX2 : tempY = nY2
                        End If

                        nX2 = nX3
                        nY2 = nY3 + 1
                        iContinue = True
                        If nX2 < 0 Or nX2 > CropWidth - 1 Or nY2 < 0 Or nY2 > CropHeight - 1 Then
                            iContinue = False
                        Else
                            If Edge(nY2, nX2, 0) = 0 Then
                                iContinue = False
                            End If
                        End If
                        If iContinue Then
                            Pos = New PointF((FirstPtOfEdge.X + nX2) / CDbl(Width), (FirstPtOfEdge.Y + nY2) / CDbl(Height))
                            C_Curve(totalCnt).CurvePoint(C_Curve(totalCnt).CPointIndx) = Pos
                            C_Curve(totalCnt).CPointIndx += 1
                            Edge(nY2, nX2, 0) = 0
                            nStackNum += 1
                            tempX = nX2 : tempY = nY2
                        End If

                        nX2 = nX3 + 1
                        nY2 = nY3 + 1
                        iContinue = True
                        If nX2 < 0 Or nX2 > CropWidth - 1 Or nY2 < 0 Or nY2 > CropHeight - 1 Then
                            iContinue = False
                        Else
                            If Edge(nY2, nX2, 0) = 0 Then
                                iContinue = False
                            End If
                        End If
                        If iContinue Then
                            Pos = New PointF((FirstPtOfEdge.X + nX2) / CDbl(Width), (FirstPtOfEdge.Y + nY2) / CDbl(Height))
                            C_Curve(totalCnt).CurvePoint(C_Curve(totalCnt).CPointIndx) = Pos
                            C_Curve(totalCnt).CPointIndx += 1
                            Edge(nY2, nX2, 0) = 0
                            nStackNum += 1
                            tempX = nX2 : tempY = nY2
                        End If

                        If nStackNum < 1 Then
                            Exit While
                        End If
                        nX3 = tempX : nY3 = tempY
                    End While
                    totalCnt += 1
                    If totalCnt > 1000 Then
                        Exit For
                    End If
                End If
            Next
        Next

        'get the tallest curve
        Dim maxCnt, maxCount As Integer
        maxCnt = 0 : maxCount = 0
        For i = 0 To totalCnt - 1
            If C_Curve(i).CPointIndx > maxCount Then
                maxCnt = i
                maxCount = C_Curve(i).CPointIndx
            End If
        Next
        C_Curve(maxCnt).CPointIndx -= 1
        Return C_Curve(maxCnt)

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

