
Imports System.IO
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports AForge.Video
Imports AForge.Video.DirectShow
Imports Emgu.CV
Imports Color = System.Drawing.Color
Imports ComboBox = System.Windows.Forms.ComboBox
Imports Font = System.Drawing.Font
Imports TextBox = System.Windows.Forms.TextBox

Public Class Main_Form
    Public originalImage As Mat                                         'original image
    Public resizedImage As Mat                                          'the image which is resized to fit the picturebox control
    Public currentImage As Mat                                          'the image which is currently used
    Public initialRatio As Single                                      'the ratio of resized_image and original image
    Public zoomFactor As Double                                        'the zooming factor
    Public curMeasureType As Integer                                   'current measurement type
    Public MeasureTypePrev As Integer                                  'backup of current measurement type
    Public curObjNum As Integer                                        'the number of current object
    Public objSelected As MeasureObject = New MeasureObject()         'current measurement object
    Public objSelected2 As MeasureObject = New MeasureObject()         'current measurement object
    Public objectList As List(Of MeasureObject) = New List(Of MeasureObject)()        'the list of measurement objects
    Public ID_MY_TEXTBOX As TextBox = New TextBox()                                    'textbox for editing annotation
    Public leftTop As Point = New Point()                             'the position left top cornor of picture control in panel
    Public scrollPos As Point = New Point()                           'the position of scroll bar
    Public annoNum As Integer                                         'the number of annotation object in the list
    Public graphFont As Font                                           'the font for text
    Public undoNum As Integer = 0                                     'count number of undo clicked and reset
    Public dashValues As Single() = {5, 2}                             'format dash style of line
    Public lineInfor As LineStyle = New LineStyle(1)                  'include the information of style, width, color ...
    Public sideDrag As Boolean = False                                'flag of side drawing
    Public showLegend As Boolean = False                              'flag of show legend
    Public scaleStyle As String = "horizontal"                        'the style of measuring scale horizontal or vertical
    Public scaleValue As Integer = 0                                  'the value of measuring scale
    Public scaleUnit As String = "cm"                                 'unit of measuring scale may be cm, mm, ...
    Public CF As Double = 1.0                                          'the ratio of per pixel by per unit
    Public digit As Integer                                            'The digit of decimal numbers
    Public fontInfor As FontInfor = New FontInfor(10)                 'include the information font and color
    Public brightness As Integer                                       'brightness of current image
    Public contrast As Integer                                           'contrast of current image
    Public gamma As Integer                                             'gamma of current image
    Public selIndex As Integer = -1                                   'selected index for object
    Public mCurDragPt As PointF = New PointF()                         'the position of mouse cursor
    Public redrawFlag As Boolean                                      'flag for redrawing objects
    Public selPtIndex As Integer = -1                                'selected index of a point of object
    Public nameList As List(Of String) = New List(Of String)          'specify the list of item names
    Public CFList As List(Of String) = New List(Of String)            'specify the names of CF
    Public CFNum As List(Of Double) = New List(Of Double)             'specify the values of CF
    Public menuClick As Boolean = False                               'specify whether the menu item is clicked

    'member variable for webcam
    Private videoDevices As FilterInfoCollection                        'usable video devices
    Private videoDevice As VideoCaptureDevice                           'video device currently used 
    Private snapshotCapabilities As VideoCapabilities()
    Private ReadOnly listCamera As ArrayList = New ArrayList()
    Private Shared needSnapshot As Boolean = False
    Private newImage As Bitmap = Nothing                                'used for capturing frame of webcam
    Private ReadOnly _devicename As String = "MultitekHDCam"            'device name
    'Private ReadOnly _devicename As String = "USB Camera"
    'Private ReadOnly _devicename As String = "Lenovo FHD Webcam"
    Private ReadOnly photoList As New System.Windows.Forms.ImageList    'list of captured images
    Private fileCounter As Integer = 0                                 'the count of captured images
    Private cameraState As Boolean = False                             'the state of camera is opened or not
    Public imagePath As String = ""                                     'path of folder storing captured images
    Private flag As Boolean = False                                     'flag for live image

    'member variable for keygen
    Dim licState As licState                                            'the state of this program is licensed or not
    Public licModel As New licensInfoModel
    Dim licGen As New LicGen
    Dim licpath As String = "active.lic"                                'the path of license file
    Private path As String

    'member variable for setting.ini
    Private caliPath As String = "C:\Users\Public\Documents\Calibration.ini"    'the path of setting.ini
    Private configPath As String = "C:\Users\Public\Documents\Config.ini"    'the path of setting.ini
    Private legendPath As String = "C:\Users\Public\Documents\Legend.ini"    'the path of setting.ini
    Private caliIni As IniFile
    Private configIni As IniFile
    Private legendIni As IniFile

    Public PolyDrawEndFlag As Boolean          'flag specifies that end point of polygen is drawed
    Public CuPolyDrawEndFlag As Boolean        'flag specifies that end point of Curve&polygen is drawed
    Public dumyPoint As Point                  'temp point 

    Public CReadySelectFalg As Boolean                                 'flag specifies whether curve&poly object is ready to select or not. when mouse cursor is in range of object, this becomes true, otherwise this becomes false
    Public CReadySelectArrayIndx As Integer                            'the candidate index of curve object for selection
    Public CRealSelectArrayIndx As Integer                             'the real index of curve object which is selected
    Public CReadySelectArrayIndx_L As Integer                          'the candidate index of label of curve object for selection
    Public CRealSelectArrayIndx_L As Integer                           'the real index of label of curve object which is selected

    Public CuPolyReadySelectArrayIndx As Integer                       'the candidate index of curve&poly object for selection
    Public CuPolyRealSelectArrayIndx As Integer                        'the real index of curve&poly object which is selected
    Public CuPolyReadySelectArrayIndx_L As Integer                     'the candidate index of label of curve&poly object for selection
    Public CuPolyRealSelectArrayIndx_L As Integer                      'the real index of label of curve&poly object which is selected

    Public PolyReadySelectArrayIndx As Integer                         'the candidate index of polygen object for selection
    Public PolyRealSelectArrayIndx As Integer                          'the real index of polygen object which is selected
    Public PolyReadySelectArrayIndx_L As Integer                       'the candidate index of label of polygen object for selection
    Public PolyRealSelectArrayIndx_L As Integer                        'the real index of label of polygen object which is selected

    Public LReadySelectArrayIndx As Integer                            'the candidate index of line object for selection
    Public LRealSelectArrayIndx As Integer                             'the real index of line object which is selected
    Public LReadySelectArrayIndx_L As Integer                          'the candidate index of label of line object for selection
    Public LRealSelectArrayIndx_L As Integer                           'the real index of label of line object which is selected

    Public PReadySelectArrayIndx As Integer                            'the candidate index of point object for selection
    Public PRealSelectArrayIndx As Integer                             'the real index of point object which is selected
    Public PReadySelectArrayIndx_L As Integer                          'the candidate index of label of point object for selection
    Public PRealSelectArrayIndx_L As Integer                           'the real index of label of point object which is selected

    Public CurvePreviousPoint As System.Nullable(Of Point) = Nothing           'previous point of curve object
    Public LinePreviousPoint As System.Nullable(Of Point) = Nothing            'previous point of Line object
    Public PointPreviousPoint As System.Nullable(Of Point) = Nothing           'previous point of Point object
    Public PolyPreviousPoint As System.Nullable(Of Point) = Nothing            'previous point of polygen object
    Public CuPolyPreviousPoint As System.Nullable(Of Point) = Nothing          'previous point of curve&poly object
    Public MousePosPoint As System.Nullable(Of Point) = Nothing                'the position of mouse cursor
    Public XsLinePoint As Integer                                      'X-coordinate of foot of perpendicular
    Public YsLinePoint As Integer                                      'Y-coordinate of foot of perpendicular
    Public PXs, PYs As Integer                                         'points used for drawing max, min lines
    Public FinalPXs, FinalPYs As Integer
    Public DotX, DotY, CDotX, CDotY As Integer                         'points used for dotted lines
    Public OutPointFlag As Boolean                                     'flag specifies whether the foot of perpendicular is in range of object or not
    Public COutPointFlag As Boolean                                    'flag specifies whether the foot of perpendicular is in range of curve&poly object or not
    Public PDotX As Integer                                            'X-coordinate of point which is used for drawing dotted line in case of polygen object
    Public PDotY As Integer                                            'Y-coordinate of point which is used for drawing dotted line in case of polygen object
    Public POutFlag As Boolean                                         'flag specifies whether the foot of perpendicular is in range of polygen object or not

    Private C_PolyObj As PolyObj = New PolyObj()
    Private C_PointObj As PointObj = New PointObj()
    Private C_LineObj As LineObj = New LineObj()
    Private C_CuPolyObj As CuPolyObj = New CuPolyObj()
    Private C_CurveObj As CurveObj = New CurveObj()
    Public curveObjSelIndex As Integer
    Private moveLine As Boolean
    Private StartPtOfMove As PointF = New PointF()
    Private EndPtOfMove As PointF = New PointF()

    'member variables for edge detection
    Public EdgeRegionDrawReady As Boolean
    Public EdgeRegionDrawed As Boolean
    Public FirstPtOfEdge As Point = New Point()
    Public SecondPtOfEdge As Point = New Point()
    Public MouseDownFlag As Boolean
    Public colorList As List(Of Integer()) = New List(Of Integer())
    Public objSeg As SegObject = New SegObject()



    Public Sub New()
        InitializeComponent()
        InitializeCustomeComeponent()
    End Sub

    Private Const EM_GETLINECOUNT As Integer = &HBA
    <DllImport("user32", EntryPoint:="SendMessageA", CharSet:=CharSet.Ansi, SetLastError:=True, ExactSpelling:=True)>
    Private Shared Function SendMessage(ByVal hwnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
    End Function


#Region "Main Form Methods"

    ''' <summary>
    ''' Initialize variables.
    ''' </summary>
    Public Sub initVar()

        CurvePreviousPoint = Nothing
        CReadySelectFalg = False
        LReadySelectArrayIndx = -1
        LRealSelectArrayIndx = -1
        PolyDrawEndFlag = False
        PReadySelectArrayIndx = -1
        PRealSelectArrayIndx = -1
        PolyReadySelectArrayIndx = -1
        PolyRealSelectArrayIndx = -1
        CReadySelectArrayIndx = -1
        CRealSelectArrayIndx = -1
        CuPolyReadySelectArrayIndx = -1
        CuPolyRealSelectArrayIndx = -1
        dumyPoint.X = -1
        dumyPoint.Y = -1

        PRealSelectArrayIndx_L = -1
        CRealSelectArrayIndx_L = -1
        PolyRealSelectArrayIndx_L = -1
        PolyRealSelectArrayIndx_L = -1
        PReadySelectArrayIndx_L = -1
        CReadySelectArrayIndx_L = -1
        PolyReadySelectArrayIndx_L = -1
        PolyReadySelectArrayIndx_L = -1
        CuPolyRealSelectArrayIndx_L = -1
        CuPolyReadySelectArrayIndx_L = -1

        COutPointFlag = False

        annoNum = -1
        curMeasureType = -1
        MeasureTypePrev = -1
        ID_BTN_CUR_COL.BackColor = Color.Black
        ID_BTN_TEXT_COL.BackColor = Color.Black
        ID_COMBO_LINE_SHAPE.SelectedIndex = 0

        initialRatio = 1
        curObjNum = 0
        brightness = 0
        contrast = 0
        gamma = 100
        zoomFactor = 1.0

        PictureBox.Image = Nothing
        objSelected.Refresh()
        objectList.Clear()
        curMeasureType = -1
        selIndex = -1
        curveObjSelIndex = -1

    End Sub

    'Initialize custome controls
    Private Sub InitializeCustomeComeponent()
        graphFont = New Font("Arial", 10, FontStyle.Regular)
        AddHandler PanelPic.MouseWheel, New MouseEventHandler(AddressOf PanelPic_MouseWheel)
        PictureBox.Controls.Add(ID_MY_TEXTBOX)
        ID_MY_TEXTBOX.Name = "ID_MY_TEXTBOX"
        ID_MY_TEXTBOX.Multiline = True
        ID_MY_TEXTBOX.AutoSize = False
        ID_MY_TEXTBOX.Visible = False
        ID_MY_TEXTBOX.Font = graphFont
        AddHandler ID_MY_TEXTBOX.TextChanged, New EventHandler(AddressOf ID_MY_TEXTBOX_TextChanged)
    End Sub

    'Initialize the color of measuring buttons
    Private Sub Initialize_Button_Colors()
        ID_BTN_ANGLE.BackColor = Color.LightBlue
        ID_BTN_ANNOTATION.BackColor = Color.LightBlue
        ID_BTN_ARC.BackColor = Color.LightBlue
        ID_BTN_LINE_ALIGN.BackColor = Color.LightBlue
        ID_BTN_LINE_HOR.BackColor = Color.LightBlue
        ID_BTN_LINE_PARA.BackColor = Color.LightBlue
        ID_BTN_LINE_VER.BackColor = Color.LightBlue
        ID_BTN_PENCIL.BackColor = Color.LightBlue
        ID_BTN_P_LINE.BackColor = Color.LightBlue
        ID_BTN_RADIUS.BackColor = Color.LightBlue
        ID_BTN_SCALE.BackColor = Color.LightBlue
        ID_BTN_C_LINE.BackColor = Color.LightBlue
        ID_BTN_C_POLY.BackColor = Color.LightBlue
        ID_BTN_C_POINT.BackColor = Color.LightBlue
        ID_BTN_C_CURVE.BackColor = Color.LightBlue
        ID_BTN_C_CUPOLY.BackColor = Color.LightBlue
        ID_BTN_C_SEL.BackColor = Color.LightBlue
    End Sub

    'get setting information from ini file
    Private Sub GetCalibrationInfo()
        If IO.File.Exists(caliPath) Then
            caliIni = New IniFile(caliPath)
            Dim Keys As ArrayList = caliIni.GetKeys("CF")
            Dim cnt As Integer = 0
            Dim index As Integer = 0
            Dim myEnumerator As System.Collections.IEnumerator = Keys.GetEnumerator()
            myEnumerator = Keys.GetEnumerator()
            While myEnumerator.MoveNext()
                If myEnumerator.Current.Name = "index" Then
                    index = CInt(myEnumerator.Current.value)
                Else
                    Dim line As String = myEnumerator.Current.value
                    Dim parse_num = line.IndexOf(":")
                    Dim CF_key = line.Substring(0, parse_num)
                    Dim CF_val = CDbl(line.Substring(parse_num + 1))

                    CFList.Add(CF_key)
                    CFNum.Add(CF_val)
                    ID_COMBOBOX_CF.Items.Add(CF_key)
                End If
            End While
            ID_COMBOBOX_CF.SelectedIndex = index
        Else
            CFList.Add("1.0X")
            CFNum.Add(1.0)
            CFList.Add("1.25X")
            CFNum.Add(1.25)
            CFList.Add("1.5X")
            CFNum.Add(1.5)
            CFList.Add("2.0X")
            CFNum.Add(2.0)
            CFList.Add("2.5X")
            CFNum.Add(2.5)
            CFList.Add("3.5X")
            CFNum.Add(3.5)
            CFList.Add("5.0X")
            CFNum.Add(5.0)
            CFList.Add("7.5X")
            CFNum.Add(7.5)
            CFList.Add("10.0X")
            CFNum.Add(10.0)

            For i = 0 To CFList.Count - 1
                ID_COMBOBOX_CF.Items.Add(CFList(i))
            Next
        End If
    End Sub

    Private Sub GetConfigInfo()
        If IO.File.Exists(configPath) Then
            configIni = New IniFile(configPath)
            Dim Keys As ArrayList = configIni.GetKeys("Config")
            Dim myEnumerator As System.Collections.IEnumerator = Keys.GetEnumerator()
            While myEnumerator.MoveNext()
                If myEnumerator.Current.Name = "unit" Then
                    scaleUnit = myEnumerator.Current.value
                Else
                    digit = CInt(myEnumerator.Current.value)
                End If
            End While
            ID_NUM_DIGIT.Value = digit
        Else
            scaleUnit = "cm"
            digit = 0
            ID_NUM_DIGIT.Value = digit
        End If
    End Sub
    Private Sub GetLegendInfo()
        If IO.File.Exists(legendPath) Then
            legendIni = New IniFile(legendPath)
            Dim Keys As ArrayList = legendIni.GetKeys("name")
            Dim myEnumerator As System.Collections.IEnumerator = Keys.GetEnumerator()
            Dim cnt As Integer = 0
            myEnumerator = Keys.GetEnumerator()
            While myEnumerator.MoveNext()
                Dim line As String = myEnumerator.Current.value
                nameList.Add(line)
            End While
        Else
            nameList.Add("Line")
            nameList.Add("Angle")
            nameList.Add("Arc")
            nameList.Add("Scale")
        End If

    End Sub

    Private Sub DrawAndCenteringImage()
        leftTop = PictureBox.CenteringImage(PanelPic)
        scrollPos.X = PanelPic.HorizontalScroll.Value
        scrollPos.Y = PanelPic.VerticalScroll.Value
        PictureBox.DrawObjList(objectList, digit, CF, False)
        Dim flag = False
        If selIndex >= 0 Then flag = True
        PictureBox.DrawObjSelected(objSelected, flag)
        If ID_MY_TEXTBOX.Visible = True And objectList.Count > 0 Then
            Dim obj_anno = objectList.ElementAt(annoNum)
            Dim st_pt As Point = New Point(obj_anno.drawPoint.X * PictureBox.Width, obj_anno.drawPoint.Y * PictureBox.Height)
            ID_MY_TEXTBOX.UpdateLocation(st_pt, leftTop, scrollPos)
        End If
    End Sub

    'check license information when main dialog is loading
    Private Sub Main_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Initialize_Button_Colors()
            Timer1.Interval = 30
            Timer1.Start()
            GetCalibrationInfo()
            GetConfigInfo()
            GetLegendInfo()
            initVar()
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString())
        End Try

        If My.Settings.imagefilepath.Equals("") Then
            imagePath = "MyImages"
            My.Settings.imagefilepath = imagePath
            My.Settings.Save()
            txtbx_imagepath.Text = imagePath
        Else
            imagePath = My.Settings.imagefilepath
            txtbx_imagepath.Text = My.Settings.imagefilepath
        End If

        objSeg.circleObj = New CircleObj()
        objSeg.sectObj = New InterSectionObj()
        objSeg.phaseSegObj = New PhaseSegObj()
        objSeg.BlobSegObj = New BlobSegObj()
        Dim colType As Type = GetType(System.Drawing.Color)

        For Each prop As PropertyInfo In colType.GetProperties()
            If prop.PropertyType Is GetType(System.Drawing.Color) Then
                Dim Col = Color.FromName(prop.Name)
                Dim Col_Array = New Integer() {Col.B, Col.G, Col.R}
                colorList.Add(Col_Array)
            End If
        Next

        DeleteImages(imagePath)
        Createdirectory(imagePath)
    End Sub

    'change the color of button when it is clicked
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If MeasureTypePrev <> curMeasureType Then
            Initialize_Button_Colors()
            MeasureTypePrev = curMeasureType
            Select Case curMeasureType
                Case MeasureType.lineAlign
                    If menuClick = False Then ID_BTN_LINE_ALIGN.BackColor = Color.DodgerBlue
                    ID_STATUS_LABEL.Text = "Calculates a line through two input points."
                Case MeasureType.lineHorizontal
                    If menuClick = False Then ID_BTN_LINE_HOR.BackColor = Color.DodgerBlue
                    ID_STATUS_LABEL.Text = "Calculates a horizontal line through two input points."
                Case MeasureType.lineVertical
                    If menuClick = False Then ID_BTN_LINE_VER.BackColor = Color.DodgerBlue
                    ID_STATUS_LABEL.Text = "Calculates a vertical line through two input points."
                Case MeasureType.angle
                    If menuClick = False Then ID_BTN_ARC.BackColor = Color.DodgerBlue
                    ID_STATUS_LABEL.Text = "Calculates angle through three points in degree."
                Case MeasureType.arc
                    If menuClick = False Then ID_BTN_RADIUS.BackColor = Color.DodgerBlue
                    ID_STATUS_LABEL.Text = "Calculates a arc through three input points."
                Case MeasureType.annotation
                    If menuClick = False Then ID_BTN_ANNOTATION.BackColor = Color.DodgerBlue
                    ID_STATUS_LABEL.Text = "Add a annotation."
                Case MeasureType.angle2Line
                    If menuClick = False Then ID_BTN_ANGLE.BackColor = Color.DodgerBlue
                    ID_STATUS_LABEL.Text = "Calculates angle through two lines in degree."
                Case MeasureType.lineParallel
                    If menuClick = False Then ID_BTN_LINE_PARA.BackColor = Color.DodgerBlue
                    ID_STATUS_LABEL.Text = "Calculates a line through two parallel lines."
                Case MeasureType.pencil
                    If menuClick = False Then ID_BTN_PENCIL.BackColor = Color.DodgerBlue
                    ID_STATUS_LABEL.Text = "Draw a line through two input points."
                Case MeasureType.ptToLine
                    If menuClick = False Then ID_BTN_P_LINE.BackColor = Color.DodgerBlue
                    ID_STATUS_LABEL.Text = "Calculates a line between a point and a line."
                Case MeasureType.measureScale
                    If menuClick = False Then ID_BTN_SCALE.BackColor = Color.DodgerBlue
                    ID_STATUS_LABEL.Text = "Insert a measuring scale."
                Case MeasureType.objLine
                    If menuClick = False Then ID_BTN_C_LINE.BackColor = Color.DodgerBlue
                    ID_STATUS_LABEL.Text = "Draw a line."
                Case MeasureType.objPoly
                    If menuClick = False Then ID_BTN_C_POLY.BackColor = Color.DodgerBlue
                    ID_STATUS_LABEL.Text = "Draw a polygen."
                Case MeasureType.objPoint
                    If menuClick = False Then ID_BTN_C_POINT.BackColor = Color.DodgerBlue
                    ID_STATUS_LABEL.Text = "Draw a point."
                Case MeasureType.objCurve
                    If menuClick = False Then ID_BTN_C_CURVE.BackColor = Color.DodgerBlue
                    ID_STATUS_LABEL.Text = "Draw a curve."
                Case MeasureType.objCuPoly
                    If menuClick = False Then ID_BTN_C_CUPOLY.BackColor = Color.DodgerBlue
                    ID_STATUS_LABEL.Text = "Draw a curve&polygen."
                Case MeasureType.objSel
                    If menuClick = False Then ID_BTN_C_SEL.BackColor = Color.DodgerBlue
                    ID_STATUS_LABEL.Text = "select objects."
            End Select

        End If

    End Sub

    'open camera
    Private Sub ID_MENU_OPEN_CAM_Click(sender As Object, e As EventArgs) Handles ID_MENU_OPEN_CAM.Click
        Try
            OpenCamera()
            SelectResolution(videoDevice, CameraResolutionsCB)
            If Not My.Settings.camresindex.Equals("") Then
                CameraResolutionsCB.SelectedIndex = My.Settings.camresindex + 1
            End If

        Catch excpt As Exception
            MessageBox.Show(excpt.Message)
        End Try

    End Sub

    'close camera
    Private Sub ID_MENU_CLOSE_CAM_Click(sender As Object, e As EventArgs) Handles ID_MENU_CLOSE_CAM.Click
        Try
            CloseCamera()
            PictureBox.Image = Nothing
            ID_PICTURE_BOX_CAM.Image = Nothing

        Catch excpt As Exception
            MessageBox.Show(excpt.Message)
        End Try

    End Sub

    'import image and draw it to picturebox
    'format variables
    Private Sub ID_MENU_OPEN_Click(sender As Object, e As EventArgs) Handles ID_MENU_OPEN.Click
        initVar()
        Dim filter = "JPEG Files|*.jpg|PNG Files|*.png|BMP Files|*.bmp|All Files|*.*"
        Dim title = "Open"

        PictureBox.LoadImageFromFiles(filter, title, originalImage, resizedImage, currentImage)
        PictureBox.Invoke(New Action(Sub() PictureBox.Image = originalImage.ToBitmap()))
        DrawAndCenteringImage()
        ID_LISTVIEW.LoadObjectList(objectList, CF, digit, scaleUnit, nameList)
    End Sub

    'export image to jpg
    Private Sub ID_MENU_SAVE_Click(sender As Object, e As EventArgs) Handles ID_MENU_SAVE.Click
        Dim filter = "JPEG Files|*.jpg"
        Dim title = "Save"
        PictureBox.SaveImageInFile(filter, title, objectList, digit, CF)
    End Sub

    'save object information as excel file
    Private Sub ID_MENU_SAVE_XLSX_Click(sender As Object, e As EventArgs) Handles ID_MENU_SAVE_XLSX.Click
        Dim filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*"
        Dim title = "Save"
        PictureBox.SaveListToExcel(objectList, filter, title, CF, digit, scaleUnit)
    End Sub

    'save object list and image as excel file
    Private Sub ID_MENU_EXPORT_REPORT_Click(sender As Object, e As EventArgs) Handles ID_MENU_EXPORT_REPORT.Click
        Dim filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*"
        Dim title = "Save"
        PictureBox.SaveReportToExcel(filter, title, objectList, digit, CF, scaleUnit)
    End Sub

    'exit the program
    Private Sub ID_MENU_EXIT_Click(sender As Object, e As EventArgs) Handles ID_MENU_EXIT.Click
        Call Application.Exit()
    End Sub

    'set current measurement type as line_align
    'reset the current object
    Private Sub ID_BTN_LINE_ALIGN_Click(sender As Object, e As EventArgs) Handles ID_BTN_LINE_ALIGN.Click
        menuClick = False
        objSelected.Refresh()
        curMeasureType = MeasureType.lineAlign
        objSelected.measuringType = curMeasureType
    End Sub

    Private Sub LINEToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LINEToolStripMenuItem.Click
        menuClick = True
        objSelected.Refresh()
        curMeasureType = MeasureType.lineAlign
        objSelected.measuringType = curMeasureType
    End Sub

    'set current measurement type as line_horizontal
    'reset the current object
    Private Sub ID_BTN_LINE_HOR_Click(sender As Object, e As EventArgs) Handles ID_BTN_LINE_HOR.Click
        menuClick = False
        objSelected.Refresh()
        curMeasureType = MeasureType.lineHorizontal
        objSelected.measuringType = curMeasureType
    End Sub

    Private Sub HORIZONTALLINEToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HORIZONTALLINEToolStripMenuItem.Click
        menuClick = True
        objSelected.Refresh()
        curMeasureType = MeasureType.lineHorizontal
        objSelected.measuringType = curMeasureType
    End Sub

    'set current measurement type as line_vertical
    'reset the current object
    Private Sub ID_BTN_LINE_VER_Click(sender As Object, e As EventArgs) Handles ID_BTN_LINE_VER.Click
        menuClick = False
        objSelected.Refresh()
        curMeasureType = MeasureType.lineVertical
        objSelected.measuringType = curMeasureType
    End Sub

    Private Sub VERTICALLINEToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VERTICALLINEToolStripMenuItem.Click
        menuClick = True
        objSelected.Refresh()
        curMeasureType = MeasureType.lineVertical
        objSelected.measuringType = curMeasureType
    End Sub

    'set current measurement type as line parallel
    'reset the current object
    Private Sub ID_BTN_LINE_PARA_Click(sender As Object, e As EventArgs) Handles ID_BTN_LINE_PARA.Click
        menuClick = False
        objSelected.Refresh()
        curMeasureType = MeasureType.lineParallel
        objSelected.measuringType = curMeasureType
    End Sub

    Private Sub PARALLELLINEToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PARALLELLINEToolStripMenuItem.Click
        menuClick = True
        objSelected.Refresh()
        curMeasureType = MeasureType.lineParallel
        objSelected.measuringType = curMeasureType
    End Sub

    'set current measurement type as angle
    'reset the current object
    Private Sub ID_BTN_ARC_Click(sender As Object, e As EventArgs) Handles ID_BTN_ARC.Click
        menuClick = False
        objSelected.Refresh()
        curMeasureType = MeasureType.angle
        objSelected.measuringType = curMeasureType
    End Sub

    Private Sub ANGLETHROUGHTHREEPOINTSToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ANGLETHROUGHTHREEPOINTSToolStripMenuItem.Click
        menuClick = True
        objSelected.Refresh()
        curMeasureType = MeasureType.angle
        objSelected.measuringType = curMeasureType
    End Sub

    'set current measurement type as angle far
    'reset the current object
    Private Sub ID_BTN_ANGLE_Click(sender As Object, e As EventArgs) Handles ID_BTN_ANGLE.Click
        menuClick = False
        objSelected.Refresh()
        curMeasureType = MeasureType.angle2Line
        objSelected.measuringType = curMeasureType
    End Sub

    Private Sub ANGLETHROUGHTWOLINESToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ANGLETHROUGHTWOLINESToolStripMenuItem.Click
        menuClick = True
        objSelected.Refresh()
        curMeasureType = MeasureType.angle2Line
        objSelected.measuringType = curMeasureType
    End Sub

    'set current measurement type as radius
    'reset the current object
    Private Sub ID_BTN_RADIUS_Click(sender As Object, e As EventArgs) Handles ID_BTN_RADIUS.Click
        menuClick = False
        objSelected.Refresh()
        curMeasureType = MeasureType.arc
        objSelected.measuringType = curMeasureType
    End Sub

    Private Sub ARCToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ARCToolStripMenuItem.Click
        menuClick = True
        objSelected.Refresh()
        curMeasureType = MeasureType.arc
        objSelected.measuringType = curMeasureType
    End Sub

    'set current measurement type as annotation
    'reset the current object
    Private Sub ID_BTN_ANNOTATION_Click(sender As Object, e As EventArgs) Handles ID_BTN_ANNOTATION.Click
        menuClick = False
        objSelected.Refresh()
        curMeasureType = MeasureType.annotation
        objSelected.measuringType = curMeasureType
    End Sub

    Private Sub ANNOTATIONToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ANNOTATIONToolStripMenuItem.Click
        menuClick = True
        objSelected.Refresh()
        curMeasureType = MeasureType.annotation
        objSelected.measuringType = curMeasureType
    End Sub

    'set current measurement type as draw line
    'reset the current object
    Private Sub ID_BTN_PENCIL_Click(sender As Object, e As EventArgs) Handles ID_BTN_PENCIL.Click
        menuClick = False
        objSelected.Refresh()
        curMeasureType = MeasureType.pencil
        objSelected.measuringType = curMeasureType
    End Sub

    'set current measurement type as point to line
    'reset the current object
    Private Sub ID_BTN_P_LINE_Click(sender As Object, e As EventArgs) Handles ID_BTN_P_LINE.Click
        menuClick = False
        objSelected.Refresh()
        curMeasureType = MeasureType.ptToLine
        objSelected.measuringType = curMeasureType
    End Sub

    Private Sub DISTANCEFROMPOINTTOLINEToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DISTANCEFROMPOINTTOLINEToolStripMenuItem.Click
        menuClick = True
        objSelected.Refresh()
        curMeasureType = MeasureType.ptToLine
        objSelected.measuringType = curMeasureType
    End Sub

    'set measureing scale 
    'set current measurement type as measuring scale
    'reset the current object
    Private Sub ID_BTN_SCALE_Click(sender As Object, e As EventArgs) Handles ID_BTN_SCALE.Click
        menuClick = False
        Dim form As ID_FORM_SCALE = New ID_FORM_SCALE(scaleUnit)
        If form.ShowDialog() = DialogResult.OK Then
            scaleStyle = form.scaleStyle
            scaleValue = form.scaleValue
            scaleUnit = form.scaleUnit

            objSelected.Refresh()
            curMeasureType = MeasureType.measureScale
            objSelected.measuringType = curMeasureType

            objSelected.scaleObject.style = scaleStyle
            objSelected.scaleObject.length = scaleValue
        End If

    End Sub

    'set current measurement type as circle_fixed
    Private Sub ANGLEOFFIXEDDIAMETERToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ANGLEOFFIXEDDIAMETERToolStripMenuItem.Click
        menuClick = True
        objSelected.Refresh()
        curMeasureType = MeasureType.arcFixed
        objSelected.measuringType = curMeasureType

        ID_STATUS_LABEL.Text = "Drawing a circle which has fixed radius"
        Dim form = New LenDiameter()
        If form.ShowDialog() = DialogResult.OK Then
            objSelected.scaleObject.length = CSng(form.ID_TEXT_FIXED.Text)
            objSelected.arc = objSelected.scaleObject.length / PictureBox.Width
        End If
    End Sub

    'set current measurement type as line_fixed
    Private Sub LINEOFFIXEDLENGTHToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LINEOFFIXEDLENGTHToolStripMenuItem.Click
        menuClick = True
        objSelected.Refresh()
        curMeasureType = MeasureType.lineFixed
        objSelected.measuringType = curMeasureType

        ID_STATUS_LABEL.Text = "Drawing a line which has fixed length"
        Dim form = New LenDiameter()
        If form.ShowDialog() = DialogResult.OK Then
            objSelected.scaleObject.length = CSng(form.ID_TEXT_FIXED.Text)
            objSelected.length = objSelected.scaleObject.length / PictureBox.Width
        End If
    End Sub

    'set current measurement type as angle_fixed
    Private Sub FIXEDANGLEToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FIXEDANGLEToolStripMenuItem.Click
        menuClick = True
        objSelected.Refresh()
        curMeasureType = MeasureType.angleFixed
        objSelected.measuringType = curMeasureType

        ID_STATUS_LABEL.Text = "Drawing a angle which has fixed angle"
        Dim form = New LenDiameter()
        If form.ShowDialog() = DialogResult.OK Then
            objSelected.angle = CSng(form.ID_TEXT_FIXED.Text)
        End If
    End Sub

    'set moveLine to ture so that you can move curves line object
    Private Sub MOVELINEOBJECTToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MOVELINEOBJECTToolStripMenuItem.Click
        moveLine = True
        ID_STATUS_LABEL.Text = "Duplicating Curve Line Object"
    End Sub


    'zoom image
    Private Sub Zoom_Image()
        Try
            Dim ratio = zoomFactor
            Dim zoomed = ZoomImage(ratio, currentImage)
            resizedImage = ZoomImage(ratio, originalImage)

            Dim Image = zoomed.ToBitmap()
            Dim Adjusted = AdjustBrightnessAndContrast(Image, brightness, contrast, gamma)

            PictureBox.Image = Adjusted
            DrawAndCenteringImage()

            zoomed.Dispose()
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString())
        End Try
    End Sub

    'zoom in image and draw it to picturebox
    Private Sub ID_BTN_ZOON_IN_Click(sender As Object, e As EventArgs) Handles ID_BTN_ZOON_IN.Click
        menuClick = False
        zoomFactor *= 1.1
        Zoom_Image()
        ID_STATUS_LABEL.Text = "Zoom In"
    End Sub

    'zoom out image and draw it to picturebox
    Private Sub ID_BTN_ZOOM_OUT_Click(sender As Object, e As EventArgs) Handles ID_BTN_ZOOM_OUT.Click
        menuClick = False
        zoomFactor /= 1.1
        Zoom_Image()
        ID_STATUS_LABEL.Text = "Zoom Out"
    End Sub

    'zoom in
    Private Sub ZOOMINToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ZOOMINToolStripMenuItem.Click
        menuClick = True
        zoomFactor *= 1.1
        Zoom_Image()
        ID_STATUS_LABEL.Text = "Zoom In"
    End Sub

    'zoom out
    Private Sub ZOOMOUTToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ZOOMOUTToolStripMenuItem.Click
        menuClick = True
        zoomFactor /= 1.1
        Zoom_Image()
        ID_STATUS_LABEL.Text = "Zoom Out"
    End Sub

    Private Sub ZOOMORIGINALToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ZOOMORIGINALToolStripMenuItem.Click
        menuClick = True
        zoomFactor = 1.0
        Zoom_Image()
        ID_STATUS_LABEL.Text = "Zoom Original"
    End Sub

    Private Sub ZOOMFITToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ZOOMFITToolStripMenuItem.Click
        menuClick = True
        zoomFactor = CalcIntialRatio(PanelPic, originalImage)
        Zoom_Image()
        ID_STATUS_LABEL.Text = "Zoom Fit"
    End Sub

    'resize image into Dst_w * Dst_h
    Private Sub CustomeResize(Dst_w As Single, Dst_h As Single, Optional State As Boolean = False)
        Try
            If State = False Then
                zoomFactor = 1.0
            End If

            Dim Dst_w_ori, Dst_h_ori, Dst_w_cur, Dst_h_cur, Dst_w_res, Dst_h_res As Integer
            If State Then
                Dst_w_ori = originalImage.Width * Dst_w
                Dst_h_ori = originalImage.Height * Dst_h
                Dst_w_cur = originalImage.Width * Dst_w
                Dst_h_cur = originalImage.Height * Dst_h
                Dst_w_res = resizedImage.Width * Dst_w
                Dst_h_res = resizedImage.Height * Dst_h
            Else
                Dst_w_ori = Dst_w
                Dst_h_ori = Dst_h
                Dst_w_cur = Dst_w
                Dst_h_cur = Dst_h
                Dst_w_res = Dst_w
                Dst_h_res = Dst_h
            End If
            originalImage = ZoomImage2(originalImage, Dst_w_ori, Dst_h_ori)
            currentImage = ZoomImage2(currentImage, Dst_w_cur, Dst_h_cur)
            resizedImage = ZoomImage2(resizedImage, Dst_w_res, Dst_h_res)

            Dim Image = currentImage.ToBitmap()
            Dim Adjusted = AdjustBrightnessAndContrast(Image, brightness, contrast, gamma)

            PictureBox.Image = Adjusted
            DrawAndCenteringImage()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString())
        End Try
    End Sub

    'resize image
    Private Sub RESIZEToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RESIZEToolStripMenuItem.Click
        Try
            Dim form As New Resize
            If form.ShowDialog = DialogResult.OK Then
                If form.RadioState Then
                    CustomeResize(CSng(form.PixHor), CSng(form.PixVer))
                Else
                    CustomeResize(form.PerHor / 100.0, form.PerVer / 100.0, True)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString())
        End Try

    End Sub

    'undo last object and last row of listview
    Private Sub Undo()
        If undoNum > 0 Then
            objSelected.Refresh()
            selIndex = -1
            selPtIndex = -1
            curveObjSelIndex = -1
            Dim flag = RemoveObjFromList(objectList)
            If flag = True Then
                PictureBox.DrawObjList(objectList, digit, CF, False)
                ID_LISTVIEW.LoadObjectList(objectList, CF, digit, scaleUnit, nameList)
                undoNum -= 1
                curObjNum -= 1
            End If
        End If
    End Sub

    'undo last object
    Private Sub ID_BTN_UNDO_Click(sender As Object, e As EventArgs) Handles ID_BTN_UNDO.Click
        menuClick = False
        Undo()
        ID_STATUS_LABEL.Text = "Undo"
    End Sub

    'undo last object
    Private Sub UNDOToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UNDOToolStripMenuItem.Click
        menuClick = True
        Undo()
        ID_STATUS_LABEL.Text = "Undo"
    End Sub

    'reset current object
    Private Sub ID_BTN_RESEL_Click(sender As Object, e As EventArgs) Handles ID_BTN_RESEL.Click
        menuClick = False
        objSelected.Refresh()
        PictureBox.DrawObjList(objectList, digit, CF, False)
        ID_STATUS_LABEL.Text = "Reselect"
    End Sub

    Private Sub RESELECTToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RESELECTToolStripMenuItem.Click
        menuClick = True
        objSelected.Refresh()
        PictureBox.DrawObjList(objectList, digit, CF, False)
        ID_STATUS_LABEL.Text = "Reselect"
    End Sub

    'reset digit
    'reload image and obj_list
    Private Sub ID_NUM_DIGIT_ValueChanged(sender As Object, e As EventArgs) Handles ID_NUM_DIGIT.ValueChanged
        menuClick = False
        digit = CInt(ID_NUM_DIGIT.Value)
        PictureBox.DrawObjList(objectList, digit, CF, False)
        ID_LISTVIEW.LoadObjectList(objectList, CF, digit, scaleUnit, nameList)
        ID_STATUS_LABEL.Text = "Changing the digit of decimal numbers."
    End Sub

    'set the width of line
    Private Sub ID_NUM_LINE_WIDTH_ValueChanged(sender As Object, e As EventArgs) Handles ID_NUM_LINE_WIDTH.ValueChanged
        menuClick = False
        lineInfor.line_width = CInt(ID_NUM_LINE_WIDTH.Value)
        ID_STATUS_LABEL.Text = "Changing the width of drawing line."
    End Sub

    'change the color of LineStyle object
    Private Sub ID_BTN_COL_PICKER_Click(sender As Object, e As EventArgs) Handles ID_BTN_COL_PICKER.Click
        menuClick = False
        Dim clrDialog As ColorDialog = New ColorDialog()

        'show the colour dialog and check that user clicked ok
        If clrDialog.ShowDialog() = DialogResult.OK Then
            'save the colour that the user chose
            lineInfor.line_color = clrDialog.Color
            ID_BTN_CUR_COL.BackColor = clrDialog.Color
        End If
        ID_STATUS_LABEL.Text = "Changing the color of drawing line."
    End Sub

    'set the line style
    Private Sub ID_COMBO_LINE_SHAPE_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ID_COMBO_LINE_SHAPE.SelectedIndexChanged
        menuClick = False
        Dim comboIndex = ID_COMBO_LINE_SHAPE.SelectedIndex
        If comboIndex = 0 Then
            lineInfor.line_style = "dotted"
        ElseIf comboIndex = 1 Then
            lineInfor.line_style = "dashed"
        End If
        ID_STATUS_LABEL.Text = "Changing the shape of drawing line."
    End Sub

    'set text fore color
    Private Sub ID_BTN_TEXT_COL_PICKER_Click(sender As Object, e As EventArgs) Handles ID_BTN_TEXT_COL_PICKER.Click
        Dim clrDialog As ColorDialog = New ColorDialog()

        'show the colour dialog and check that user clicked ok
        If clrDialog.ShowDialog() = DialogResult.OK Then
            'save the colour that the user chose
            fontInfor.font_color = clrDialog.Color
            ID_BTN_TEXT_COL.BackColor = clrDialog.Color
        End If
        ID_STATUS_LABEL.Text = "Changing the color of text."
    End Sub

    'set text font
    Private Sub ID_BTN_TEXT_FONT_Click(sender As Object, e As EventArgs) Handles ID_BTN_TEXT_FONT.Click
        Dim fontDialog As FontDialog = New FontDialog()

        If fontDialog.ShowDialog() = DialogResult.OK Then
            fontInfor.text_font = fontDialog.Font
        End If
        ID_STATUS_LABEL.Text = "Changing the font of text."
    End Sub

    'detect edge of selected region
    Private Sub EDGEDETECTToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EDGEDETECTToolStripMenuItem.Click
        EdgeRegionDrawReady = True
        objSelected.Refresh()
        objSelected.measuringType = MeasureType.objCurve
        ID_STATUS_LABEL.Text = "Detect edge."
    End Sub

    'redraw objects
    Private Sub PanelPic_Scroll(sender As Object, e As ScrollEventArgs) Handles PanelPic.Scroll
        PictureBox.DrawObjList(objectList, digit, CF, False)
        Dim flag = False
        If selIndex >= 0 Then flag = True
        PictureBox.DrawObjSelected(objSelected, flag)
    End Sub

    'keep the image in the center when the panel size in changed
    'redraw objects
    Private Sub PanelPic_SizeChanged(sender As Object, e As EventArgs) Handles PanelPic.SizeChanged
        DrawAndCenteringImage()
    End Sub

    'redraw objects
    Private Sub PanelPic_MouseWheel(sender As Object, e As MouseEventArgs)
        Dim flag = False
        If selIndex >= 0 Then flag = True
        PictureBox.DrawObjList(objectList, digit, CF, False)
        PictureBox.DrawObjSelected(objSelected, flag)
    End Sub

    'update object selected
    'when mouse is clicked on annotation insert textbox there to you can edit it
    'draw objects and load list of objects to listview
    Private Sub PictureBox_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBox.MouseDown
        If PictureBox.Image Is Nothing OrElse currentImage Is Nothing Then
            Return
        End If
        If e.Button = MouseButtons.Left Then
            MouseDownFlag = True
            Dim m_pt As PointF = New PointF()
            m_pt.X = CSng(e.X) / PictureBox.Width
            m_pt.Y = CSng(e.Y) / PictureBox.Height
            m_pt.X = Math.Min(Math.Max(m_pt.X, 0), 1)
            m_pt.Y = Math.Min(Math.Max(m_pt.Y, 0), 1)
            mCurDragPt = m_pt

            Dim m_pt2 As Point = New Point(e.X, e.Y)

            If curMeasureType >= 0 Then
                If curMeasureType < MeasureType.objLine Then
                    Dim completed = ModifyObjSelected(objSelected, curMeasureType, m_pt, originalImage.Width, originalImage.Height, lineInfor, fontInfor, CF)

                    If completed Then
                        objSelected.objNum = curObjNum
                        objectList.Add(objSelected)
                        PictureBox.DrawObjList(objectList, digit, CF, False)
                        ID_LISTVIEW.LoadObjectList(objectList, CF, digit, scaleUnit, nameList)
                        objSelected.Refresh()
                        curMeasureType = -1
                        curObjNum += 1
                        If undoNum < 2 Then undoNum += 1
                    Else
                        PictureBox.DrawObjList(objectList, digit, CF, False)
                        PictureBox.DrawObjSelected(objSelected, False)
                    End If
                Else    'Curve objects
                    If curMeasureType = MeasureType.objPoly Then
                        If PolyPreviousPoint IsNot Nothing Then
                            C_PolyObj.PolyPoint(C_PolyObj.PolyPointIndx) = m_pt
                            C_PolyObj.PolyPointIndx += 1
                            PolyPreviousPoint = Nothing
                        End If
                    ElseIf curMeasureType = MeasureType.objCuPoly Then
                        CuPolyDrawEndFlag = False
                        C_CuPolyObj.CuPolyPointIndx_j += 1
                        C_CuPolyObj.CuPolyPoint(C_CuPolyObj.CuPolyPointIndx_j, 0) = m_pt
                    ElseIf curMeasureType = MeasureType.objPoint Then
                        C_PointObj.PointPoint = m_pt
                    ElseIf curMeasureType = MeasureType.objLine Then
                        If LinePreviousPoint Is Nothing Then
                            LinePreviousPoint = e.Location
                            C_LineObj.FirstPointOfLine = m_pt
                        End If
                    ElseIf curMeasureType = MeasureType.objSel Then
                        If curveObjSelIndex >= 0 Then
                            Dim obj = objectList.ElementAt(curveObjSelIndex)
                            If obj.measuringType = MeasureType.objCuPoly Then
                                CuPolyRealSelectArrayIndx = curveObjSelIndex
                            ElseIf obj.measuringType = MeasureType.objCurve Then
                                CRealSelectArrayIndx = curveObjSelIndex
                            ElseIf obj.measuringType = MeasureType.objLine Then
                                LRealSelectArrayIndx = curveObjSelIndex
                            ElseIf obj.measuringType = MeasureType.objPoint Then
                                PRealSelectArrayIndx = curveObjSelIndex
                            ElseIf obj.measuringType = MeasureType.objPoly Then
                                PolyRealSelectArrayIndx = curveObjSelIndex
                            End If
                            PictureBox.DrawObjList(objectList, digit, CF, False)
                            DrawCurveObjSelected(PictureBox, obj, digit, CF)
                        End If
                    End If

                End If

            Else
                'select point of selected object
                If selIndex >= 0 Then
                    selPtIndex = PictureBox.CheckPointInPos(objectList.ElementAt(selIndex), m_pt2)
                    If selPtIndex >= 0 Then
                        PictureBox.DrawObjList(objectList, digit, CF, False)
                        PictureBox.HightLightItem(objectList.ElementAt(selIndex), PictureBox.Width, PictureBox.Height, CF)
                        PictureBox.DrawObjSelected(objectList.ElementAt(selIndex), True)
                        PictureBox.HighlightTargetPt(objectList.ElementAt(selIndex), selPtIndex)
                        Return
                    End If
                End If

                selIndex = CheckItemInPos(m_pt, objectList, PictureBox.Width, PictureBox.Height, CF)
                If selIndex >= 0 Then
                    PictureBox.DrawObjList(objectList, digit, CF, False)
                    PictureBox.HightLightItem(objectList.ElementAt(selIndex), PictureBox.Width, PictureBox.Height, CF)
                    PictureBox.DrawObjSelected(objectList.ElementAt(selIndex), True)
                Else
                    If annoNum >= 0 Then
                        ID_MY_TEXTBOX.DisableTextBox(objectList, annoNum, PictureBox.Width, PictureBox.Height)
                        ID_LISTVIEW.LoadObjectList(objectList, CF, digit, scaleUnit, nameList)
                        annoNum = -1
                    End If
                    PictureBox.DrawObjList(objectList, digit, CF, False)
                End If
            End If

            If EdgeRegionDrawReady = True Then
                FirstPtOfEdge = m_pt2
                EdgeRegionDrawed = False
            End If

            If moveLine = True And curveObjSelIndex >= 0 Then
                Dim obj = objectList.ElementAt(curveObjSelIndex)
                If obj.measuringType = MeasureType.objLine Then
                    StartPtOfMove = m_pt
                    C_LineObj.Refresh()
                    C_LineObj = CloneLineObj(obj.curveObject.LineItem(0))
                    objSelected2.Refresh()
                    InitializeLineObj(objSelected2, C_LineObj.LDrawPos, lineInfor, fontInfor)
                End If
            End If
        Else    'right click
            If curMeasureType = MeasureType.objPoly Then
                PolyPreviousPoint = Nothing
                C_PolyObj.PolyDrawPos = PolyGetPos(C_PolyObj)
                Dim tempObj = ClonePolyObj(C_PolyObj)
                objSelected.curveObject = New CurveObject()
                objSelected.curveObject.PolyItem.Add(tempObj)
                objSelected.name = "PL" & curObjNum
                AddCurveToList()
                C_PolyObj.Refresh()
                PolyDrawEndFlag = True
            ElseIf curMeasureType = MeasureType.objCuPoly Then
                CuPolyPreviousPoint = Nothing
                C_CuPolyObj.CuPolyDrawPos = CuPolyGetPos(C_CuPolyObj)
                Dim tempObj = CloneCuPolyObj(C_CuPolyObj)
                objSelected.curveObject = New CurveObject()
                objSelected.curveObject.CuPolyItem.Add(tempObj)
                objSelected.name = "CP" & curObjNum
                AddCurveToList()
                C_CuPolyObj.Refresh()
                CuPolyDrawEndFlag = True
            End If
        End If
    End Sub

    'draw temporal objects according to mouse cursor
    Private Sub PictureBox_MouseMove(sender As Object, e As MouseEventArgs) Handles PictureBox.MouseMove
        Dim m_pt As PointF = New PointF()
        m_pt.X = CSng(e.X) / PictureBox.Width
        m_pt.Y = CSng(e.Y) / PictureBox.Height
        m_pt.X = Math.Min(Math.Max(m_pt.X, 0), 1)
        m_pt.Y = Math.Min(Math.Max(m_pt.Y, 0), 1)
        Dim dx = m_pt.X - mCurDragPt.X
        Dim dy = m_pt.Y - mCurDragPt.Y

        Dim m_pt2 = New Point(e.X, e.Y)

        If MouseDownFlag Then
            If curMeasureType < 0 Then
                If selIndex >= 0 Then
                    mCurDragPt = m_pt
                    If selPtIndex >= 0 Then
                        PictureBox.Refresh()
                        MovePoint(objectList, selIndex, selPtIndex, dx, dy)
                        ModifyObjSelected(objectList, selIndex, originalImage.Width, originalImage.Height)
                        Dim obj = objectList.ElementAt(selIndex)
                        Dim target_pt As Point = New Point()
                        If obj.measuringType = MeasureType.angle Then

                            Dim start_point As Point = New Point()
                            Dim end_point As Point = New Point()
                            Dim middle_point As Point = New Point()

                            start_point.X = CInt(obj.startPoint.X * PictureBox.Width)
                            start_point.Y = CInt(obj.startPoint.Y * PictureBox.Height)
                            middle_point.X = CInt(obj.middlePoint.X * PictureBox.Width)
                            middle_point.Y = CInt(obj.middlePoint.Y * PictureBox.Height)
                            end_point.X = CInt(obj.endPoint.X * PictureBox.Width)
                            end_point.Y = CInt(obj.endPoint.Y * PictureBox.Height)

                            target_pt.X = (start_point.X + end_point.X) / 2
                            target_pt.Y = (start_point.Y + end_point.Y) / 2
                            Dim angles = CalcStartAndSweepAngle(obj, start_point, middle_point, end_point, target_pt)
                            Dim start_angle, sweep_angle As Double
                            start_angle = angles(0)
                            sweep_angle = angles(1)
                            Dim angle As Integer = CInt(2 * start_angle + sweep_angle) / 2
                            Dim radius = CInt(obj.angleObject.radius * PictureBox.Width) + 10
                            target_pt = CalcPositionInCircle(middle_point, radius, angle)
                        Else
                            target_pt = New Point(obj.drawPoint.X * PictureBox.Width, obj.drawPoint.Y * PictureBox.Height)
                        End If
                        PictureBox.DrawTempFinal(obj, target_pt, sideDrag, digit, CF, False)
                        objectList(selIndex) = obj
                        PictureBox.DrawObjList(objectList, digit, CF, False)
                        ID_LISTVIEW.LoadObjectList(objectList, CF, digit, scaleUnit, nameList)

                    Else
                        MoveObject(objectList, selIndex, dx, dy)
                        PictureBox.DrawObjList(objectList, digit, CF, False)
                    End If
                    PictureBox.HightLightItem(objectList.ElementAt(selIndex), PictureBox.Width, PictureBox.Height, CF)
                    PictureBox.DrawObjSelected(objectList.ElementAt(selIndex), True)
                End If
            End If

            If curMeasureType = MeasureType.objCurve Then
                If CurvePreviousPoint Is Nothing Then
                    CurvePreviousPoint = e.Location
                    C_CurveObj.CurvePoint(0) = m_pt
                Else
                    PictureBox.DrawObjList(objectList, digit, CF, False)
                    DrawCurveObj(PictureBox, lineInfor, C_CurveObj)
                    DrawLineBetweenTwoPoints(PictureBox, lineInfor, CurvePreviousPoint.Value, e.Location)
                    C_CurveObj.CPointIndx += 1
                    CurvePreviousPoint = e.Location
                    C_CurveObj.CurvePoint(C_CurveObj.CPointIndx) = m_pt
                End If
            ElseIf curMeasureType = MeasureType.objLine Then
                If LinePreviousPoint IsNot Nothing Then
                    C_LineObj.SecndPointOfLine = m_pt
                    PictureBox.DrawObjList(objectList, digit, CF, False)
                    DrawLineBetweenTwoPoints(PictureBox, lineInfor, LinePreviousPoint.Value, e.Location)
                End If
            ElseIf curMeasureType = MeasureType.objCuPoly Then
                If CuPolyDrawEndFlag = False Then
                    If CuPolyPreviousPoint Is Nothing Then
                        CuPolyPreviousPoint = e.Location
                        C_CuPolyObj.CuPolyPoint(C_CuPolyObj.CuPolyPointIndx_j, 0) = m_pt
                    Else
                        PictureBox.DrawObjList(objectList, digit, CF, False)
                        DrawCuPolyObj(PictureBox, lineInfor, C_CuPolyObj)
                        DrawLineBetweenTwoPoints(PictureBox, lineInfor, CuPolyPreviousPoint.Value, e.Location)
                        C_CuPolyObj.CuPolyPointIndx_k(C_CuPolyObj.CuPolyPointIndx_j) += 1
                        CuPolyPreviousPoint = e.Location
                        C_CuPolyObj.CuPolyPoint(C_CuPolyObj.CuPolyPointIndx_j, C_CuPolyObj.CuPolyPointIndx_k(C_CuPolyObj.CuPolyPointIndx_j)) = m_pt
                    End If
                End If
            End If

            If EdgeRegionDrawReady = True Then
                SecondPtOfEdge = m_pt2
                PictureBox.DrawObjList(objectList, digit, CF, False)
                PictureBox.DrawRectangle(FirstPtOfEdge, SecondPtOfEdge)
            End If

            If moveLine = True And curveObjSelIndex >= 0 And StartPtOfMove.X <> 0 And StartPtOfMove.Y <> 0 Then
                EndPtOfMove = m_pt
                Dim Obj = objectList.ElementAt(curveObjSelIndex).curveObject.LineItem(0)
                C_LineObj.FirstPointOfLine.X = (EndPtOfMove.X - StartPtOfMove.X) + Obj.FirstPointOfLine.X
                C_LineObj.FirstPointOfLine.Y = (EndPtOfMove.Y - StartPtOfMove.Y) + Obj.FirstPointOfLine.Y
                C_LineObj.SecndPointOfLine.X = (EndPtOfMove.X - StartPtOfMove.X) + Obj.SecndPointOfLine.X
                C_LineObj.SecndPointOfLine.Y = (EndPtOfMove.Y - StartPtOfMove.Y) + Obj.SecndPointOfLine.Y
                PictureBox.DrawObjList(objectList, digit, CF, False)
                DrawLineObject(PictureBox, C_LineObj)
                Dim Delta = GetNormalFromPointToLine(New Point(Obj.FirstPointOfLine.X * PictureBox.Width, Obj.FirstPointOfLine.Y * PictureBox.Height),
                                                     New Point(Obj.SecndPointOfLine.X * PictureBox.Width, Obj.SecndPointOfLine.Y * PictureBox.Height), m_pt2)
                DrawLengthBetweenLines(PictureBox, objSelected2, CDbl(Delta.Width / PictureBox.Width), CDbl(Delta.Height / PictureBox.Height), originalImage.Width, originalImage.Height, digit, CF)
            End If
        Else    'mouse is not clicked

            If selIndex >= 0 Then
                PictureBox.HightLightItem(objectList.ElementAt(selIndex), PictureBox.Width, PictureBox.Height, CF)
                PictureBox.DrawObjSelected(objectList.ElementAt(selIndex), True)
            End If

            If curMeasureType >= 0 Then
                If curMeasureType < MeasureType.objLine Then
                    Dim temp As Point = New Point(e.X, e.Y)
                    PictureBox.DrawObjList(objectList, digit, CF, False)
                    PictureBox.DrawObjSelected(objSelected, False)
                    PictureBox.DrawTempFinal(objSelected, temp, sideDrag, digit, CF, True)
                ElseIf curMeasureType = MeasureType.objPoly Then
                    If PolyPreviousPoint Is Nothing Then
                        PolyPreviousPoint = e.Location
                        Dim ptF = New PointF(e.X / CSng(PictureBox.Width), e.Y / CSng(PictureBox.Height))
                        C_PolyObj.PolyPoint(C_PolyObj.PolyPointIndx) = ptF
                    Else
                        If C_PolyObj.PolyPointIndx >= 1 Then
                            PictureBox.DrawObjList(objectList, digit, CF, False)
                            DrawPolyObj(PictureBox, lineInfor, C_PolyObj)
                            DrawLineBetweenTwoPoints(PictureBox, lineInfor, PolyPreviousPoint.Value, e.Location)
                        End If
                    End If
                ElseIf curMeasureType = MeasureType.objCuPoly Then
                    If CuPolyDrawEndFlag = False Then
                        Dim temp As Point
                        If C_CuPolyObj.CuPolyPointIndx_j > 0 Then
                            Dim tempF = C_CuPolyObj.CuPolyPoint(C_CuPolyObj.CuPolyPointIndx_j, C_CuPolyObj.CuPolyPointIndx_k(C_CuPolyObj.CuPolyPointIndx_j))
                            temp = New Point(tempF.X * PictureBox.Width, tempF.Y * PictureBox.Height)
                        Else
                            temp = dumyPoint
                        End If
                        If temp <> dumyPoint Then
                            PictureBox.DrawObjList(objectList, digit, CF, False)
                            DrawCuPolyObj(PictureBox, lineInfor, C_CuPolyObj)
                            DrawLineBetweenTwoPoints(PictureBox, lineInfor, temp, e.Location)
                        End If
                    End If
                ElseIf curMeasureType = MeasureType.objSel Then
                    curveObjSelIndex = CheckCurveItemInPos(PictureBox, m_pt, objectList)
                    If curveObjSelIndex >= 0 Then
                        Dim obj = objectList.ElementAt(curveObjSelIndex)
                        PictureBox.DrawObjList(objectList, digit, CF, False)
                        DrawCurveObjSelected(PictureBox, obj, digit, CF)
                    End If
                End If
            End If

            If moveLine Then
                curveObjSelIndex = CheckCurveItemInPos(PictureBox, m_pt, objectList)
                If curveObjSelIndex >= 0 Then
                    Dim obj = objectList.ElementAt(curveObjSelIndex)
                    PictureBox.DrawObjList(objectList, digit, CF, False)
                    DrawCurveObjSelected(PictureBox, obj, digit, CF)
                End If
            End If
        End If
    End Sub

    'release capture
    Private Sub PictureBox_MouseUp(sender As Object, e As MouseEventArgs) Handles PictureBox.MouseUp
        MouseDownFlag = False

        If curMeasureType = MeasureType.objPoint Then
            C_PointObj.PDrawPos = PGetPos(C_PointObj.PointPoint)
            Dim tempObj = ClonePointObj(C_PointObj)
            objSelected.curveObject = New CurveObject()
            objSelected.curveObject.PointItem.Add(tempObj)
            objSelected.name = "P" & curObjNum
            AddCurveToList()
            C_PointObj.Refresh()
        ElseIf curMeasureType = MeasureType.objLine Then
            If C_LineObj.SecndPointOfLine.X <> 0 And C_LineObj.SecndPointOfLine.Y <> 0 Then
                LinePreviousPoint = Nothing
                C_LineObj.LDrawPos = LGetPos(C_LineObj)
                Dim tempObj = CloneLineObj(C_LineObj)
                objSelected.curveObject = New CurveObject()
                objSelected.curveObject.LineItem.Add(tempObj)
                objSelected.name = "L" & curObjNum
                AddCurveToList()
                C_LineObj.Refresh()
            End If
        ElseIf curMeasureType = MeasureType.objCurve Then
            CurvePreviousPoint = Nothing
            C_CurveObj.CDrawPos = CGetPos(C_CurveObj)
            Dim tempObj = CloneCurveObj(C_CurveObj)
            objSelected.curveObject = New CurveObject()
            objSelected.curveObject.CurveItem.Add(tempObj)
            objSelected.name = "C" & curObjNum
            AddCurveToList()
            C_CurveObj.Refresh()
        ElseIf curMeasureType = MeasureType.objCuPoly Then
            CuPolyPreviousPoint = Nothing
        End If

        If EdgeRegionDrawReady = True And SecondPtOfEdge.X <> 0 And SecondPtOfEdge.Y <> 0 Then
            'run code for detect edge
            If objSelected.measuringType = MeasureType.objCurve Then
                Dim input As Image = resizedImage.ToBitmap()
                Dim Adjusted = AdjustBrightnessAndContrast(input, brightness, contrast, gamma)
                C_CurveObj = Canny(Adjusted, FirstPtOfEdge, SecondPtOfEdge)

                CurvePreviousPoint = Nothing
                C_CurveObj.CDrawPos = CGetPos(C_CurveObj)
                Dim tempObj = CloneCurveObj(C_CurveObj)
                objSelected.curveObject = New CurveObject()
                objSelected.curveObject.CurveItem.Add(tempObj)
                objSelected.name = "C" & curObjNum
                AddCurveToList()
                C_CurveObj.Refresh()
                EdgeRegionDrawReady = False
                FirstPtOfEdge.X = 0
                FirstPtOfEdge.Y = 0
                SecondPtOfEdge.X = 0
                SecondPtOfEdge.Y = 0
                Dim form = New ToCurve()
                Dim result = form.ShowDialog()
                If result = DialogResult.Cancel Then
                    Undo()
                    undoNum += 1

                ElseIf result = DialogResult.Retry Then
                    Undo()
                    undoNum += 1
                    EdgeRegionDrawReady = True
                    objSelected.measuringType = MeasureType.objCurve
                End If
            Else
                EdgeRegionDrawed = True
            End If
        End If

        If moveLine = True And EndPtOfMove.X <> 0 And EndPtOfMove.Y <> 0 Then
            objSelected2.objNum = curObjNum
            objectList.Add(objSelected2)
            objSelected2.Refresh()
            curMeasureType = -1
            curObjNum += 1
            If undoNum < 2 Then undoNum += 1

            C_LineObj.LDrawPos = LGetPos(C_LineObj)
            Dim tempObj = CloneLineObj(C_LineObj)
            objSelected.curveObject = New CurveObject()
            objSelected.curveObject.LineItem.Add(tempObj)
            objSelected.name = "L" & curObjNum
            AddCurveToList()
            C_LineObj.Refresh()
            StartPtOfMove.X = 0
            StartPtOfMove.Y = 0
            EndPtOfMove.X = 0
            EndPtOfMove.Y = 0
            moveLine = False
        End If
    End Sub

    'select annotation
    Private Sub PictureBox_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles PictureBox.MouseDoubleClick
        Dim m_pt As PointF = New Point()
        m_pt.X = CSng(e.X) / PictureBox.Width
        m_pt.Y = CSng(e.Y) / PictureBox.Height
        m_pt.X = Math.Min(Math.Max(m_pt.X, 0), 1)
        m_pt.Y = Math.Min(Math.Max(m_pt.Y, 0), 1)

        Dim an_num = CheckAnnotation(m_pt, objectList, PictureBox.Width, PictureBox.Height)
        If an_num >= 0 AndAlso objectList(an_num).measuringType = MeasureType.annotation Then
            ID_MY_TEXTBOX.Font = fontInfor.text_font
            ID_MY_TEXTBOX.EnableTextBox(objectList.ElementAt(an_num), PictureBox.Width, PictureBox.Height, leftTop, scrollPos)
            annoNum = an_num
        End If
    End Sub

    'draw objects to picturebox when ID_FORM_BRIGHTNESS is actived
    Private Sub PictureBox_Paint(sender As Object, e As PaintEventArgs) Handles PictureBox.Paint
        If redrawFlag Then PictureBox.DrawObjList(objectList, digit, CF, True)
    End Sub

    'change the size of textbox when the text is changed
    Private Sub ID_MY_TEXTBOX_TextChanged(sender As Object, e As EventArgs)
        Dim textBox = CType(sender, TextBox)
        Dim numberOfLines = SendMessage(textBox.Handle.ToInt32(), EM_GETLINECOUNT, 0, 0)
        textBox.Height = (textBox.Font.Height + 2) * numberOfLines
    End Sub


    'set brightness, contrast and gamma to current image
    Private Sub ID_BTN_BRIGHTNESS_Click(sender As Object, e As EventArgs) Handles ID_BTN_BRIGHTNESS.Click
        redrawFlag = True
        Dim ratio = zoomFactor
        Dim zoomed = ZoomImage(ratio, currentImage)
        Dim Image = zoomed.ToBitmap()
        Dim form As Brightness = New Brightness(PictureBox, Image, brightness, contrast, gamma)

        Dim InitialImage = AdjustBrightnessAndContrast(Image, brightness, contrast, gamma)
        If form.ShowDialog() = DialogResult.OK Then
            brightness = form.brightness
            contrast = form.contrast
            gamma = form.gamma

        Else
            PictureBox.Image = form.InitialImage
        End If
        redrawFlag = False
        PictureBox.DrawObjList(objectList, digit, CF, False)
    End Sub

    'save setting information to setting.ini
    'close camera
    Private Sub Main_Form_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        On Error Resume Next

        If IO.File.Exists(caliPath) Then
            If caliIni IsNot Nothing Then
                caliIni.ChangeValue("index", "CF", ID_COMBOBOX_CF.SelectedIndex)
                caliIni.Save(caliPath)
            End If
        Else
            'set default value when ini file does Not exist in document folder
            caliIni = New IniFile(caliPath)

            caliIni.AddSection("CF")
            caliIni.AddKey("index", ID_COMBOBOX_CF.SelectedIndex, "CF")
            For i = 0 To CFList.Count - 1
                Dim key = "No" & (i + 1)
                Dim value = CFList(i) & ":" & CFNum(i)
                caliIni.AddKey(key, value, "CF")
            Next

            caliIni.Sort()
            caliIni.Save(caliPath)
        End If

        If IO.File.Exists(configPath) Then
            If configIni IsNot Nothing Then
                configIni.ChangeValue("unit", "Config", scaleUnit)
                configIni.ChangeValue("digit", "Config", digit.ToString())
                configIni.Save(configPath)
            End If
        Else
            configIni = New IniFile(configPath)
            configIni.AddSection("Config")
            configIni.AddKey("unit", scaleUnit, "Config")
            configIni.AddKey("digit", digit.ToString(), "Config")
            configIni.Sort()
            configIni.Save(configPath)
        End If

        If IO.File.Exists(legendPath) Then

        Else
            legendIni = New IniFile(legendPath)
            legendIni.AddSection("name")
            legendIni.AddKey("No1", "Line", "name")
            legendIni.AddKey("No2", "Angle", "name")
            legendIni.AddKey("No3", "Arc", "name")
            legendIni.AddKey("No4", "Scale", "name")
            legendIni.Sort()
            legendIni.Save(legendPath)
        End If

        If videoDevice Is Nothing Then
        ElseIf videoDevice.IsRunning Then
            videoDevice.SignalToStop()
            RemoveHandler videoDevice.NewFrame, New NewFrameEventHandler(AddressOf Device_NewFrame)
            videoDevice = Nothing
        End If
        cameraState = False
    End Sub

    'Data grid events make combobox column editable
    Private Sub ID_LISTVIEW_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles ID_LISTVIEW.EditingControlShowing

        If ID_LISTVIEW.CurrentCell Is ID_LISTVIEW(0, 0) Then
            Dim cb As ComboBox = TryCast(e.Control, ComboBox)

            If cb IsNot Nothing Then
                RemoveHandler cb.SelectedIndexChanged, AddressOf CB_SelectedIndexChanged

                ' Following line needed for initial setup.
                cb.DropDownStyle = If(cb.SelectedIndex = 0, ComboBoxStyle.DropDown, ComboBoxStyle.DropDownList)

                AddHandler cb.SelectedIndexChanged, AddressOf CB_SelectedIndexChanged
            End If
        End If

    End Sub

    'make only first item of combobox item of datagridview editable
    Private Sub CB_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim cb As ComboBox = TryCast(sender, ComboBox)
        cb.DropDownStyle = If(cb.SelectedIndex = 0, ComboBoxStyle.DropDown, ComboBoxStyle.DropDownList)
    End Sub

    'update first and fifth item of datagridview and update the object 
    Private Sub ID_LISTVIEW_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs) Handles ID_LISTVIEW.CellValidating
        If e.ColumnIndex = 0 Then

            Dim cell = TryCast(ID_LISTVIEW.Rows(e.RowIndex).Cells(e.ColumnIndex), DataGridViewComboBoxCell)

            If cell IsNot Nothing AndAlso Not Equals(e.FormattedValue.ToString(), String.Empty) AndAlso Not cell.Items.Contains(e.FormattedValue) Then
                cell.Items(0) = e.FormattedValue

                If ID_LISTVIEW.IsCurrentCellDirty Then
                    ID_LISTVIEW.CommitEdit(DataGridViewDataErrorContexts.Commit)
                End If

                cell.Value = e.FormattedValue
                Dim obj_list = objectList
                Dim obj = obj_list.ElementAt(e.RowIndex)
                obj.name = cell.Value
                obj_list(e.RowIndex) = obj
                objectList = obj_list

            End If
        ElseIf e.ColumnIndex = 5 Then
            Dim cell = TryCast(ID_LISTVIEW.Rows(e.RowIndex).Cells(e.ColumnIndex), DataGridViewTextBoxCell)

            If cell IsNot Nothing AndAlso Not Equals(e.FormattedValue.ToString(), String.Empty) Then

                If ID_LISTVIEW.IsCurrentCellDirty Then
                    ID_LISTVIEW.CommitEdit(DataGridViewDataErrorContexts.Commit)
                End If

                cell.Value = e.FormattedValue
                Dim obj_list = objectList
                Dim obj = obj_list.ElementAt(e.RowIndex)
                obj.remarks = cell.Value
                obj_list(e.RowIndex) = obj
                objectList = obj_list

            End If
        End If
    End Sub

    'handles exception for datagridview
    Private Sub ID_LISTVIEW_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles ID_LISTVIEW.DataError
        If e.ColumnIndex = 0 AndAlso e.RowIndex = 0 Then
            e.Cancel = True
        End If
    End Sub

    'update CF value
    'redraw objects
    'reload object list to datagridView
    Private Sub ID_COMBOBOX_CF_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ID_COMBOBOX_CF.SelectedIndexChanged
        Dim index = ID_COMBOBOX_CF.SelectedIndex()
        CF = CFNum(index)
        PictureBox.DrawObjList(objectList, digit, CF, False)
        ID_LISTVIEW.LoadObjectList(objectList, CF, digit, scaleUnit, nameList)
        ID_STATUS_LABEL.Text = "Changing CF."
    End Sub

    'show About dialog
    Private Sub ID_MENU_ABOUT_Click(sender As Object, e As EventArgs) Handles ID_MENU_ABOUT.Click
        Dim ad As New About
        If ad.ShowDialog() = DialogResult.OK Then

        End If
    End Sub

    'side dragging for small thickness when ID_CHECK_SIDE is checked
    Private Sub ID_CHECK_SIDE_CheckedChanged(sender As Object, e As EventArgs) Handles ID_CHECK_SIDE.CheckedChanged
        If ID_CHECK_SIDE.Checked = True Then
            sideDrag = True
        Else
            sideDrag = False
        End If
    End Sub

    'show legend when ID_CHECK_showLegend is checked
    Private Sub ID_CHECK_showLegend_CheckedChanged(sender As Object, e As EventArgs) Handles ID_CHECK_SHOW_LEGEND.CheckedChanged
        If ID_CHECK_SHOW_LEGEND.Checked = True Then
            showLegend = True
        Else
            showLegend = False
        End If
        PictureBox.DrawObjList(objectList, digit, CF, False)
    End Sub

    'open calibration ini file
    Private Sub CALIBRATIONINFOToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CALIBRATIONINFOToolStripMenuItem.Click
        Try
            Dim alive As System.Diagnostics.Process
            If System.IO.File.Exists(caliPath) = False Then
                caliIni = New IniFile(caliPath)
                caliIni.AddSection("CF")
                caliIni.AddKey("index", ID_COMBOBOX_CF.SelectedIndex, "CF")
                For i = 0 To CFList.Count - 1
                    Dim key = "No" & (i + 1)
                    Dim value = CFList(i) & ":" & CFNum(i)
                    caliIni.AddKey(key, value, "CF")
                Next
                caliIni.Sort()
                caliIni.Save(caliPath)
            End If
            alive = Process.Start(caliPath)
            alive.WaitForExit()
            ID_COMBOBOX_CF.Items.Clear()
            CFList.Clear()
            CFNum.Clear()
            GetCalibrationInfo()
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString())
        End Try
    End Sub

    'open config ini file
    Private Sub CONFIGINFOToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CONFIGINFOToolStripMenuItem.Click
        Try
            Dim alive As System.Diagnostics.Process
            If System.IO.File.Exists(configPath) = False Then
                configIni = New IniFile(configPath)
                configIni.AddSection("Config")
                configIni.AddKey("unit", scaleUnit, "Config")
                configIni.AddKey("digit", digit.ToString(), "Config")

                configIni.Sort()
                configIni.Save(configPath)
            End If
            alive = Process.Start(configPath)
            alive.WaitForExit()
            GetConfigInfo()
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString())
        End Try
    End Sub

    'open legend ini file
    Private Sub LEGENDINFOToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LEGENDINFOToolStripMenuItem.Click
        Try
            Dim alive As System.Diagnostics.Process
            If System.IO.File.Exists(legendPath) = False Then
                legendIni = New IniFile(legendPath)
                legendIni.AddSection("name")
                legendIni.AddKey("No1", "Line", "name")
                legendIni.AddKey("No2", "Angle", "name")
                legendIni.AddKey("No3", "Arc", "name")
                legendIni.AddKey("No4", "Scale", "name")

                legendIni.Sort()
                legendIni.Save(legendPath)
            End If
            alive = Process.Start(legendPath)
            alive.WaitForExit()
            nameList.Clear()
            GetLegendInfo()
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString())
        End Try
    End Sub

#End Region

#Region "Webcam Methods"

    'pop one frame from webcam and display it to pictureboxs
    Public Sub Device_NewFrame(sender As Object, eventArgs As AForge.Video.NewFrameEventArgs)
        On Error Resume Next

        Me.Invoke(Sub()
                      newImage = DirectCast(eventArgs.Frame.Clone(), Bitmap)

                      If flag = False Then
                          PictureBox.Image = newImage.Clone()
                      End If
                      ID_PICTURE_BOX_CAM.Image = newImage.Clone()
                      newImage?.Dispose()
                  End Sub)

    End Sub

    'open camera
    Private Sub OpenCamera()
        Dim cameraInt As Int32 = CheckPerticularCamera(videoDevices, _devicename)
        If (cameraInt < 0) Then
            MessageBox.Show("Compatible Camera not found..")
            Exit Sub
        End If

        videoDevices = New FilterInfoCollection(FilterCategory.VideoInputDevice)
        videoDevice = New VideoCaptureDevice(videoDevices(Convert.ToInt32(cameraInt)).MonikerString)
        If Not My.Settings.camresindex.Equals("") Then
            videoDevice.VideoResolution = videoDevice.VideoCapabilities(Convert.ToInt32(My.Settings.camresindex))
        End If
        AddHandler videoDevice.NewFrame, New NewFrameEventHandler(AddressOf Device_NewFrame)
        videoDevice.Start()
        cameraState = True
    End Sub

    'close camera
    Private Sub CloseCamera()

        If videoDevice Is Nothing Then
        ElseIf videoDevice.IsRunning Then
            videoDevice.SignalToStop()
            RemoveHandler videoDevice.NewFrame, New NewFrameEventHandler(AddressOf Device_NewFrame)
            videoDevice.Source = Nothing
        End If
        cameraState = False
    End Sub

    'capture image and add it to ID_LISTVIEW_IMAGE
    Private Sub ID_BTN_CAPTURE_Click(sender As Object, e As EventArgs) Handles ID_BTN_CAPTURE.Click

        Try

            If ID_PICTURE_BOX_CAM.Image Is Nothing Then
                Return
            End If
            Dim img1 As Image = ID_PICTURE_BOX_CAM.Image.Clone()

            Createdirectory(imagePath)
            If photoList.Images.Count <= 0 Then
                fileCounter = photoList.Images.Count + 1
            Else
                fileCounter = Convert.ToInt32(IO.Path.GetFileNameWithoutExtension(photoList.Images.Keys.Item(photoList.Images.Count - 1).ToString()).Split("_")(1)) + 1
            End If

            img1.Save(imagePath & "\\test_" & (fileCounter) & ".jpeg", Imaging.ImageFormat.Jpeg)
            photoList.ImageSize = New Size(160, 120)
            photoList.Images.Add("\\test_" & (fileCounter) & ".jpeg", img1)
            ID_LISTVIEW_IMAGE.LargeImageList = photoList
            'img1.Dispose()
            ID_LISTVIEW_IMAGE.Items.Clear()
            For index = 0 To photoList.Images.Count - 1
                Dim item As New ListViewItem With {
                    .ImageIndex = index,
                        .Tag = imagePath & photoList.Images.Keys.Item(index).ToString(),
                        .Text = IO.Path.GetFileNameWithoutExtension(photoList.Images.Keys.Item(index).ToString())
                }
                ID_LISTVIEW_IMAGE.Items.Add(item)
            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    'clear all image list and items in ID_LISTVIEW_CAM
    Private Sub ID_BTN_CLEAR_ALL_Click(sender As Object, e As EventArgs) Handles ID_BTN_CLEAR_ALL.Click
        fileCounter = 0
        ID_LISTVIEW_IMAGE.Clear()
        ID_LISTVIEW_IMAGE.Items.Clear()
        photoList.Images.Clear()
        ID_PICTURE_BOX_CAM.Image = Nothing
        PictureBox.Image = Nothing
        DeleteImages(imagePath)
    End Sub

    'stop camera and display the selected image to ID_PICTURE_BOX
    Private Sub ID_LISTVIEW_IMAGE_DoubleClick(sender As Object, e As EventArgs) Handles ID_LISTVIEW_IMAGE.DoubleClick
        Try
            flag = True

            Dim itemSelected As Integer = GetListViewSelectedItemIndex(ID_LISTVIEW_IMAGE)
            SetListViewSelectedItem(ID_LISTVIEW_IMAGE, itemSelected)
            Dim Image As Image = Image.FromFile(ID_LISTVIEW_IMAGE.SelectedItems(0).Tag)
            ID_PICTURE_BOX_CAM.Image = Image

            initVar()

            PictureBox.LoadImageFromFile(ID_LISTVIEW_IMAGE.SelectedItems(0).Tag, originalImage, resizedImage, currentImage)

            PictureBox.Image = originalImage.ToBitmap()
            DrawAndCenteringImage()

            ID_LISTVIEW.LoadObjectList(objectList, CF, digit, scaleUnit, nameList)

        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
    End Sub

    'display property window for the video capture
    Private Sub Btn_CameraProperties_Click(sender As Object, e As EventArgs) Handles Btn_CameraProperties.Click

        If videoDevice Is Nothing Then
            MsgBox("Please start Camera First")

        ElseIf videoDevice.IsRunning Then
            videoDevice.DisplayPropertyPage(Me.Handle)
        End If
    End Sub

    'set flag for live image so that live images can be displayed to tab
    Private Sub btn_live_Click(sender As Object, e As EventArgs) Handles btn_live.Click
        flag = False

    End Sub

    'change the resolution of webcam
    Private Sub CameraResolutionsCB_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CameraResolutionsCB.SelectedIndexChanged

        If CameraResolutionsCB.SelectedIndex > 0 Then
            My.Settings.camresindex = CameraResolutionsCB.SelectedIndex - 1
            My.Settings.Save()
            CloseCamera()
            Threading.Thread.Sleep(500)
            OpenCamera()
        End If

    End Sub

    'set the path of directory for captured images
    Private Sub btn_setpath_Click(sender As Object, e As EventArgs) Handles btn_setpath.Click
        Dim dialog = New FolderBrowserDialog With {
            .SelectedPath = Application.StartupPath
        }
        If DialogResult.OK = dialog.ShowDialog() Then
            txtbx_imagepath.Text = dialog.SelectedPath & "\MyImages"
            imagePath = txtbx_imagepath.Text
            My.Settings.imagefilepath = imagePath
            My.Settings.Save()
            Createdirectory(imagePath)
        End If
    End Sub

    'delete all captured images
    Private Sub btn_delete_Click(sender As Object, e As EventArgs) Handles btn_delete.Click

        For Each v As ListViewItem In ID_LISTVIEW_IMAGE.SelectedItems
            ID_LISTVIEW_IMAGE.Items.Remove(v)
            photoList.Images.RemoveAt(v.ImageIndex)
            Dim FileDelete As String = v.Tag
            If File.Exists(FileDelete) = True Then
                'File.Delete(FileDelete)
            End If
        Next

    End Sub

    'open browser and load captured images
    Private Sub btn_browse_Click(sender As Object, e As EventArgs) Handles btn_browse.Click
        Dim ofd As New OpenFileDialog With {
            .Filter = "Image File (*.ico;*.jpg;*.jpeg;*.bmp;*.gif;*.png)|*.jpg;*.jpeg;*.bmp;*.gif;*.png;*.ico",
            .Multiselect = True,
            .FilterIndex = 1
        }

        If ofd.ShowDialog() = DialogResult.OK Then
            Try
                Dim files As String() = ofd.FileNames
                For Each file In files
                    Dim img1 As New Bitmap(file)
                    Createdirectory(imagePath)
                    If photoList.Images.Count <= 0 Then
                        fileCounter = photoList.Images.Count + 1
                    Else
                        fileCounter = Convert.ToInt32(IO.Path.GetFileNameWithoutExtension(photoList.Images.Keys.Item(photoList.Images.Count - 1).ToString()).Split("_")(1)) + 1
                    End If

                    img1.Save(imagePath & "\\test_" & (fileCounter) & ".jpeg", Imaging.ImageFormat.Jpeg)
                    photoList.ImageSize = New Size(200, 150)
                    photoList.Images.Add("\\test_" & (fileCounter) & ".jpeg", img1)
                    ID_LISTVIEW_IMAGE.LargeImageList = photoList
                    img1.Dispose()
                    ID_LISTVIEW_IMAGE.Items.Clear()
                    For index = 0 To photoList.Images.Count - 1
                        Dim item As New ListViewItem With {
                        .ImageIndex = index,
                            .Tag = imagePath & photoList.Images.Keys.Item(index).ToString(),
                            .Text = IO.Path.GetFileNameWithoutExtension(photoList.Images.Keys.Item(index).ToString())
                    }
                        ID_LISTVIEW_IMAGE.Items.Add(item)
                    Next

                Next

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If


    End Sub
#End Region

#Region "Keygen Methods"

#End Region

#Region "Curves Methods"

    ''' <summary>
    ''' set current measurement type as C_Line
    ''' </summary>
    Private Sub ID_BTN_C_LINE_Click(sender As Object, e As EventArgs) Handles ID_BTN_C_LINE.Click
        menuClick = False
        objSelected.Refresh()
        curMeasureType = MeasureType.objLine
        objSelected.measuringType = curMeasureType

    End Sub
    Private Sub LINEToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles LINEToolStripMenuItem1.Click
        menuClick = True
        objSelected.Refresh()
        curMeasureType = MeasureType.objLine
        objSelected.measuringType = curMeasureType

    End Sub

    ''' <summary>
    ''' set current measurement type as C_Poly
    ''' </summary>
    Private Sub ID_BTN_C_POLY_Click(sender As Object, e As EventArgs) Handles ID_BTN_C_POLY.Click
        menuClick = False
        objSelected.Refresh()
        curMeasureType = MeasureType.objPoly
        objSelected.measuringType = curMeasureType

    End Sub

    Private Sub POLYGENToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles POLYGENToolStripMenuItem.Click
        menuClick = True
        objSelected.Refresh()
        curMeasureType = MeasureType.objPoly
        objSelected.measuringType = curMeasureType

    End Sub

    ''' <summary>
    ''' set current measurement type as C_Point
    ''' </summary>
    Private Sub ID_BTN_C_POINT_Click(sender As Object, e As EventArgs) Handles ID_BTN_C_POINT.Click
        menuClick = False
        objSelected.Refresh()
        curMeasureType = MeasureType.objPoint
        objSelected.measuringType = curMeasureType

    End Sub

    Private Sub POINTToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles POINTToolStripMenuItem.Click
        menuClick = True
        objSelected.Refresh()
        curMeasureType = MeasureType.objPoint
        objSelected.measuringType = curMeasureType

    End Sub

    ''' <summary>
    ''' set current measurement type as C_Curve
    ''' </summary>
    Private Sub ID_BTN_C_CURVE_Click(sender As Object, e As EventArgs) Handles ID_BTN_C_CURVE.Click
        menuClick = False
        objSelected.Refresh()
        curMeasureType = MeasureType.objCurve
        objSelected.measuringType = curMeasureType

    End Sub

    Private Sub CURVEToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CURVEToolStripMenuItem.Click
        menuClick = True
        objSelected.Refresh()
        curMeasureType = MeasureType.objCurve
        objSelected.measuringType = curMeasureType

    End Sub

    ''' <summary>
    ''' set current measurement type as C_Cupoly
    ''' </summary>
    Private Sub ID_BTN_C_CUPOLY_Click(sender As Object, e As EventArgs) Handles ID_BTN_C_CUPOLY.Click
        menuClick = False
        objSelected.Refresh()
        curMeasureType = MeasureType.objCuPoly
        objSelected.measuringType = curMeasureType

    End Sub

    Private Sub CURVEPOLYGENToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CURVEPOLYGENToolStripMenuItem.Click
        menuClick = True
        objSelected.Refresh()
        curMeasureType = MeasureType.objCuPoly
        objSelected.measuringType = curMeasureType

    End Sub

    ''' <summary>
    ''' set current measurement type as C_Sel
    ''' </summary>
    Private Sub ID_BTN_C_SEL_Click(sender As Object, e As EventArgs) Handles ID_BTN_C_SEL.Click
        menuClick = False
        objSelected.Refresh()
        curMeasureType = MeasureType.objSel
        objSelected.measuringType = curMeasureType

    End Sub

    ''' <summary>
    ''' Add Curve object to obj list
    ''' </summary>
    Private Sub AddCurveToList()
        AddMaxMinToList()

        PictureBox.DrawObjList(objectList, digit, CF, False)
        ID_LISTVIEW.LoadObjectList(objectList, CF, digit, scaleUnit, nameList)
    End Sub

    Private Sub AddMaxMinToList()
        objSelected.objNum = curObjNum
        SetLineAndFont(objSelected, lineInfor, fontInfor)
        objectList.Add(objSelected)
        objSelected.Refresh()
        curMeasureType = -1
        curObjNum += 1
        If undoNum < 2 Then undoNum += 1
    End Sub

    ''' <summary>
    ''' calculate minimum distance between two selected objects
    ''' </summary>
    Private Sub MinCalcBtn_Click(sender As Object, e As EventArgs) Handles MinCalcBtn.Click
        ID_STATUS_LABEL.Text = "Calculate minimum distance between selected objects."

        If CuPolyRealSelectArrayIndx >= 0 And LRealSelectArrayIndx >= 0 Then
            Dim obj1 = objectList.ElementAt(CuPolyRealSelectArrayIndx)
            Dim obj2 = objectList.ElementAt(LRealSelectArrayIndx)
            objSelected = CalcMinBetweenCuPolyAndLine(obj1, obj2, PictureBox.Width, PictureBox.Height)
            AddMaxMinToList()
        End If
        If CuPolyRealSelectArrayIndx >= 0 And PRealSelectArrayIndx >= 0 Then
            Dim obj1 = objectList.ElementAt(CuPolyRealSelectArrayIndx)
            Dim obj2 = objectList.ElementAt(PRealSelectArrayIndx)
            objSelected = CalcMinBetweenCuPolyAndPoint(obj1, obj2, PictureBox.Width, PictureBox.Height)
            AddMaxMinToList()
        End If
        If CRealSelectArrayIndx >= 0 And LRealSelectArrayIndx >= 0 Then
            Dim obj1 = objectList.ElementAt(CRealSelectArrayIndx)
            Dim obj2 = objectList.ElementAt(LRealSelectArrayIndx)
            objSelected = CalcMinBetweenCurveAndLine(obj1, obj2, PictureBox.Width, PictureBox.Height)
            AddMaxMinToList()
        End If
        If CRealSelectArrayIndx >= 0 And PRealSelectArrayIndx >= 0 Then
            Dim obj1 = objectList.ElementAt(CRealSelectArrayIndx)
            Dim obj2 = objectList.ElementAt(PRealSelectArrayIndx)
            objSelected = CalcMinBetweenCurveAndPoint(obj1, obj2, PictureBox.Width, PictureBox.Height)
            AddMaxMinToList()
        End If
        If PRealSelectArrayIndx >= 0 And LRealSelectArrayIndx >= 0 Then
            Dim obj1 = objectList.ElementAt(PRealSelectArrayIndx)
            Dim obj2 = objectList.ElementAt(LRealSelectArrayIndx)
            objSelected = CalcMinBetweenPointAndLine(obj1, obj2, PictureBox.Width, PictureBox.Height)
            AddMaxMinToList()
        End If
        If PRealSelectArrayIndx >= 0 And PolyRealSelectArrayIndx >= 0 Then
            Dim obj1 = objectList.ElementAt(PRealSelectArrayIndx)
            Dim obj2 = objectList.ElementAt(PolyRealSelectArrayIndx)
            objSelected = CalcMinBetweenPointAndPoly(obj1, obj2, PictureBox.Width, PictureBox.Height)
            AddMaxMinToList()
        End If
        If LRealSelectArrayIndx >= 0 And PolyRealSelectArrayIndx >= 0 Then
            Dim obj1 = objectList.ElementAt(LRealSelectArrayIndx)
            Dim obj2 = objectList.ElementAt(PolyRealSelectArrayIndx)
            objSelected = CalcMinBetweenLineAndPoly(obj1, obj2, PictureBox.Width, PictureBox.Height)
            AddMaxMinToList()
        End If
        If CRealSelectArrayIndx >= 0 And PolyRealSelectArrayIndx >= 0 Then
            Dim obj1 = objectList.ElementAt(CRealSelectArrayIndx)
            Dim obj2 = objectList.ElementAt(PolyRealSelectArrayIndx)
            objSelected = CalcMinBetweenCurveAndPoly(obj1, obj2, PictureBox.Width, PictureBox.Height)
            AddMaxMinToList()
        End If

        PictureBox.DrawObjList(objectList, digit, CF, False)
        ID_LISTVIEW.LoadObjectList(objectList, CF, digit, scaleUnit, nameList)
    End Sub

    ''' <summary>
    ''' calculate maximum distance between two selected objects
    ''' </summary>
    Private Sub MaxCalcBtn_Click(sender As Object, e As EventArgs) Handles MaxCalcBtn.Click
        ID_STATUS_LABEL.Text = "Calculate maximum distance between selected objects."

        If CuPolyRealSelectArrayIndx >= 0 And LRealSelectArrayIndx >= 0 Then
            Dim obj1 = objectList.ElementAt(CuPolyRealSelectArrayIndx)
            Dim obj2 = objectList.ElementAt(LRealSelectArrayIndx)
            objSelected = CalcMaxBetweenCuPolyAndLine(obj1, obj2, PictureBox.Width, PictureBox.Height)
            AddMaxMinToList()
        End If
        If CuPolyRealSelectArrayIndx >= 0 And PRealSelectArrayIndx >= 0 Then
            Dim obj1 = objectList.ElementAt(CuPolyRealSelectArrayIndx)
            Dim obj2 = objectList.ElementAt(PRealSelectArrayIndx)
            objSelected = CalcMaxBetweenCuPolyAndPoint(obj1, obj2, PictureBox.Width, PictureBox.Height)
            AddMaxMinToList()
        End If
        If CRealSelectArrayIndx >= 0 And LRealSelectArrayIndx >= 0 Then
            Dim obj1 = objectList.ElementAt(CRealSelectArrayIndx)
            Dim obj2 = objectList.ElementAt(LRealSelectArrayIndx)
            objSelected = CalcMaxBetweenCurveAndLine(obj1, obj2, PictureBox.Width, PictureBox.Height)
            AddMaxMinToList()
        End If
        If CRealSelectArrayIndx >= 0 And PRealSelectArrayIndx >= 0 Then
            Dim obj1 = objectList.ElementAt(CRealSelectArrayIndx)
            Dim obj2 = objectList.ElementAt(PRealSelectArrayIndx)
            objSelected = CalcMaxBetweenCurveAndPoint(obj1, obj2, PictureBox.Width, PictureBox.Height)
            AddMaxMinToList()
        End If
        If CRealSelectArrayIndx >= 0 And PolyRealSelectArrayIndx >= 0 Then
            Dim obj1 = objectList.ElementAt(CRealSelectArrayIndx)
            Dim obj2 = objectList.ElementAt(PolyRealSelectArrayIndx)
            objSelected = CalcMaxBetweenCurveAndPoly(obj1, obj2, PictureBox.Width, PictureBox.Height)
            AddMaxMinToList()
        End If
        If LRealSelectArrayIndx >= 0 And PRealSelectArrayIndx >= 0 Then
            Dim obj1 = objectList.ElementAt(LRealSelectArrayIndx)
            Dim obj2 = objectList.ElementAt(PRealSelectArrayIndx)
            objSelected = CalcMaxBetweenLineAndPoint(obj1, obj2, PictureBox.Width, PictureBox.Height)
            AddMaxMinToList()
        End If
        If LRealSelectArrayIndx >= 0 And PolyRealSelectArrayIndx >= 0 Then
            Dim obj1 = objectList.ElementAt(LRealSelectArrayIndx)
            Dim obj2 = objectList.ElementAt(PolyRealSelectArrayIndx)
            objSelected = CalcMaxBetweenLineAndPoly(obj1, obj2, PictureBox.Width, PictureBox.Height)
            AddMaxMinToList()
        End If
        If PolyRealSelectArrayIndx >= 0 And PRealSelectArrayIndx >= 0 Then
            Dim obj1 = objectList.ElementAt(PolyRealSelectArrayIndx)
            Dim obj2 = objectList.ElementAt(PRealSelectArrayIndx)
            objSelected = CalcMaxBetweenPolyAndPoint(obj1, obj2, PictureBox.Width, PictureBox.Height)
            AddMaxMinToList()
        End If
        PictureBox.DrawObjList(objectList, digit, CF, False)
        ID_LISTVIEW.LoadObjectList(objectList, CF, digit, scaleUnit, nameList)
    End Sub

    ''' <summary>
    ''' calculate minimum perpendicular distance between two selected objects
    ''' </summary>
    Private Sub PerMin_Click(sender As Object, e As EventArgs) Handles PerMin.Click
        ID_STATUS_LABEL.Text = "Calculate perpendicular minimum distance between selected objects."

        If CuPolyRealSelectArrayIndx >= 0 And LRealSelectArrayIndx >= 0 Then
            Dim obj1 = objectList.ElementAt(CuPolyRealSelectArrayIndx)
            Dim obj2 = objectList.ElementAt(LRealSelectArrayIndx)
            objSelected = CalcPMinBetweenCuPolyAndLine(obj1, obj2, PictureBox.Width, PictureBox.Height)
            AddMaxMinToList()
        End If
        If CuPolyRealSelectArrayIndx >= 0 And PRealSelectArrayIndx >= 0 Then
            Dim obj1 = objectList.ElementAt(CuPolyRealSelectArrayIndx)
            Dim obj2 = objectList.ElementAt(PRealSelectArrayIndx)
            objSelected = CalcPMinBetweenCuPolyAndPoint(obj1, obj2, PictureBox.Width, PictureBox.Height)
            AddMaxMinToList()
        End If
        If CRealSelectArrayIndx >= 0 And LRealSelectArrayIndx >= 0 Then
            Dim obj1 = objectList.ElementAt(CRealSelectArrayIndx)
            Dim obj2 = objectList.ElementAt(LRealSelectArrayIndx)
            objSelected = CalcPMinBetweenCurveAndLine(obj1, obj2, PictureBox.Width, PictureBox.Height)
            AddMaxMinToList()
        End If
        If CRealSelectArrayIndx >= 0 And PRealSelectArrayIndx >= 0 Then
            Dim obj1 = objectList.ElementAt(CRealSelectArrayIndx)
            Dim obj2 = objectList.ElementAt(PRealSelectArrayIndx)
            objSelected = CalcPMinBetweenCurveAndPoint(obj1, obj2, PictureBox.Width, PictureBox.Height)
            AddMaxMinToList()
        End If
        If PRealSelectArrayIndx >= 0 And LRealSelectArrayIndx >= 0 Then
            Dim obj1 = objectList.ElementAt(PRealSelectArrayIndx)
            Dim obj2 = objectList.ElementAt(LRealSelectArrayIndx)
            objSelected = CalcPMinBetweenPointAndLine(obj1, obj2, PictureBox.Width, PictureBox.Height)
            AddMaxMinToList()
        End If
        If PRealSelectArrayIndx >= 0 And PolyRealSelectArrayIndx >= 0 Then
            Dim obj1 = objectList.ElementAt(PRealSelectArrayIndx)
            Dim obj2 = objectList.ElementAt(PolyRealSelectArrayIndx)
            objSelected = CalcPMinBetweenPointAndPoly(obj1, obj2, PictureBox.Width, PictureBox.Height)
            AddMaxMinToList()
        End If
        If LRealSelectArrayIndx >= 0 And PolyRealSelectArrayIndx >= 0 Then
            Dim obj1 = objectList.ElementAt(LRealSelectArrayIndx)
            Dim obj2 = objectList.ElementAt(PolyRealSelectArrayIndx)
            objSelected = CalcPMinBetweenLineAndPoly(obj1, obj2, PictureBox.Width, PictureBox.Height)
            AddMaxMinToList()
        End If
        If CRealSelectArrayIndx >= 0 And PolyRealSelectArrayIndx >= 0 Then
            Dim obj1 = objectList.ElementAt(CRealSelectArrayIndx)
            Dim obj2 = objectList.ElementAt(PolyRealSelectArrayIndx)
            objSelected = CalcPMinBetweenCurveAndPoly(obj1, obj2, PictureBox.Width, PictureBox.Height)
            AddMaxMinToList()
        End If

        PictureBox.DrawObjList(objectList, digit, CF, False)
        ID_LISTVIEW.LoadObjectList(objectList, CF, digit, scaleUnit, nameList)
    End Sub

    ''' <summary>
    ''' calculate maximum perpendicular distance between two selected objects
    ''' </summary>
    Private Sub PerMax_Click(sender As Object, e As EventArgs) Handles PerMax.Click
        ID_STATUS_LABEL.Text = "Calculate perpendicular maximum distance between selected objects."

        If CuPolyRealSelectArrayIndx >= 0 And LRealSelectArrayIndx >= 0 Then
            Dim obj1 = objectList.ElementAt(CuPolyRealSelectArrayIndx)
            Dim obj2 = objectList.ElementAt(LRealSelectArrayIndx)
            objSelected = CalcPMaxBetweenCuPolyAndLine(obj1, obj2, PictureBox.Width, PictureBox.Height)
            AddMaxMinToList()
        End If
        If CuPolyRealSelectArrayIndx >= 0 And PRealSelectArrayIndx >= 0 Then
            Dim obj1 = objectList.ElementAt(CuPolyRealSelectArrayIndx)
            Dim obj2 = objectList.ElementAt(PRealSelectArrayIndx)
            objSelected = CalcPMaxBetweenCuPolyAndPoint(obj1, obj2, PictureBox.Width, PictureBox.Height)
            AddMaxMinToList()
        End If
        If CRealSelectArrayIndx >= 0 And LRealSelectArrayIndx >= 0 Then
            Dim obj1 = objectList.ElementAt(CRealSelectArrayIndx)
            Dim obj2 = objectList.ElementAt(LRealSelectArrayIndx)
            objSelected = CalcPMaxBetweenCurveAndLine(obj1, obj2, PictureBox.Width, PictureBox.Height)
            AddMaxMinToList()
        End If
        If CRealSelectArrayIndx >= 0 And PRealSelectArrayIndx >= 0 Then
            Dim obj1 = objectList.ElementAt(CRealSelectArrayIndx)
            Dim obj2 = objectList.ElementAt(PRealSelectArrayIndx)
            objSelected = CalcPMaxBetweenCurveAndPoint(obj1, obj2, PictureBox.Width, PictureBox.Height)
            AddMaxMinToList()
        End If
        If CRealSelectArrayIndx >= 0 And PolyRealSelectArrayIndx >= 0 Then
            Dim obj1 = objectList.ElementAt(CRealSelectArrayIndx)
            Dim obj2 = objectList.ElementAt(PolyRealSelectArrayIndx)
            objSelected = CalcPMaxBetweenCurveAndPoly(obj1, obj2, PictureBox.Width, PictureBox.Height)
            AddMaxMinToList()
        End If
        If LRealSelectArrayIndx >= 0 And PRealSelectArrayIndx >= 0 Then
            Dim obj1 = objectList.ElementAt(LRealSelectArrayIndx)
            Dim obj2 = objectList.ElementAt(PRealSelectArrayIndx)
            objSelected = CalcPMaxBetweenLineAndPoint(obj1, obj2, PictureBox.Width, PictureBox.Height)
            AddMaxMinToList()
        End If
        If LRealSelectArrayIndx >= 0 And PolyRealSelectArrayIndx >= 0 Then
            Dim obj1 = objectList.ElementAt(LRealSelectArrayIndx)
            Dim obj2 = objectList.ElementAt(PolyRealSelectArrayIndx)
            objSelected = CalcPMaxBetweenLineAndPoly(obj1, obj2, PictureBox.Width, PictureBox.Height)
            AddMaxMinToList()
        End If
        If PolyRealSelectArrayIndx >= 0 And PRealSelectArrayIndx >= 0 Then
            Dim obj1 = objectList.ElementAt(PolyRealSelectArrayIndx)
            Dim obj2 = objectList.ElementAt(PRealSelectArrayIndx)
            objSelected = CalcPMaxBetweenPolyAndPoint(obj1, obj2, PictureBox.Width, PictureBox.Height)
            AddMaxMinToList()
        End If
        PictureBox.DrawObjList(objectList, digit, CF, False)
        ID_LISTVIEW.LoadObjectList(objectList, CF, digit, scaleUnit, nameList)
    End Sub
#End Region

#Region "Segmentation Tool"

    ''' <summary>
    ''' open Circle dialog and detect circles
    ''' </summary>
    Private Sub CIRCLEDETECTIONToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CIRCLEDETECTIONToolStripMenuItem.Click
        objSeg.Refresh()
        objSeg.measureType = SegType.circle
        Dim form = New Circle()
        form.Show()
    End Sub

    ''' <summary>
    ''' Open intersect dialog and detect intersections
    ''' </summary>
    Private Sub INTERSECTIONToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles INTERSECTIONToolStripMenuItem.Click
        objSeg.Refresh()
        objSeg.measureType = SegType.interSect
        Dim form = New Intersection()
        form.Show()
    End Sub

    ''' <summary>
    ''' open phase segmentation dialog 
    ''' </summary>
    Private Sub PHASESEGMENTATIONToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PHASESEGMENTATIONToolStripMenuItem.Click
        objSeg.Refresh()
        objSeg.measureType = SegType.phaseSegment
        Dim form = New Phase_Segmentation()
        form.Show()
    End Sub

    ''' <summary>
    ''' open countAndClassification dialog
    ''' </summary>
    Private Sub COUNTCLASSIFICATIONToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles COUNTCLASSIFICATIONToolStripMenuItem.Click
        objSeg.Refresh()
        objSeg.measureType = SegType.blobSegment
        Dim form = New CountAndClassification()
        form.Show()
    End Sub

    ''' <summary>
    ''' open Participle Size dialog
    ''' </summary>
    Private Sub PARTICIPLESIZEToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PARTICIPLESIZEToolStripMenuItem.Click
        objSeg.Refresh()
        objSeg.measureType = SegType.blobSegment
        Dim form = New ParticipleSize()
        form.Show()
    End Sub

    ''' <summary>
    ''' open countAndClassification dialog
    ''' </summary>
    Private Sub NODULARITYToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NODULARITYToolStripMenuItem.Click
        objSeg.Refresh()
        objSeg.measureType = SegType.blobSegment
        Dim form = New Nodularity()
        form.Show()
    End Sub
#End Region
End Class
