Imports System.IO
Imports System.Runtime.InteropServices
Imports AForge.Video
Imports AForge.Video.DirectShow
Imports Emgu.CV
Imports Color = System.Drawing.Color
Imports ComboBox = System.Windows.Forms.ComboBox
Imports Font = System.Drawing.Font
Imports TextBox = System.Windows.Forms.TextBox

'enum for specify the measuring types
Public Enum MeasureType
    line_align = 0
    line_horizontal = 1
    line_vertical = 2
    angle = 3
    radius = 4
    annotation = 5
    angle_far = 6
    line_para = 7
    draw_line = 8
    pt_line = 9
    measure_scale = 10
    Curves = 11
End Enum

'structure for drawing line
Public Structure LineStyle
    Public line_style As String
    Public line_width As Integer
    Public line_color As Color
    Public Sub New(ByVal width As Integer)
        line_style = "dotted"
        line_width = width
        line_color = Color.Black
    End Sub
End Structure

'structure for text font
Public Structure FontInfor
    Public font_color As Color
    Public text_font As Font
    Public Sub New(ByVal height As Integer)
        font_color = Color.Black
        text_font = New Font("Arial", height, FontStyle.Regular)
    End Sub
End Structure

'structure for line_align, line_horizon, line_vertical, line_para, draw_line, pt_line
Public Structure LineObject
    Public nor_pt1 As PointF        'connect with start point
    Public nor_pt2 As PointF        'connect with end point    
    Public nor_pt3 As PointF        'connect with nor_pt4
    Public nor_pt4 As PointF
    Public nor_pt5 As PointF        'connect with nor_pt3
    Public nor_pt6 As PointF        'connect with nor_pt3
    Public nor_pt7 As PointF        'connect with nor_pt4
    Public nor_pt8 As PointF        'connect with nor_pt4
    Public draw_pt As PointF        'point for drawing line
    Public trans_angle As Integer   'angle between measuring line and X-axis
    Public side_drag As PointF      'flag for side drawing
End Structure

'structure for angle and angle_far
Public Structure AngleObject
    Public start_angle As Double        'angle between first line of arc and X-axis
    Public sweep_angle As Double        'angle between first line and second line of arc
    Public radius As Double             'radius of angle
    Public start_pt As PointF           'start point of angle
    Public end_pt As PointF             'end point of angle
    Public nor_pt1 As PointF            'included in first line of angle
    Public nor_pt2 As PointF            'connect to nor_pt1
    Public nor_pt3 As PointF            'connect to nor_pt1
    Public nor_pt4 As PointF            'included in second line of angle
    Public nor_pt5 As PointF            'connect to nor_pt4
    Public nor_pt6 As PointF            'connect to nor_pt4
    Public draw_pt As PointF            'point for drawing text
    Public trans_angle As Integer       'angle between text and X-axis
    Public side_drag As PointF          'flag for side drawing
    Public side_index As Integer        'index of nor_pt for side drawing
End Structure

'Structure for radius
Public Structure RadiusObject
    Public center_pt As PointF      'center point of arc
    Public circle_pt As PointF      'point in circle which is used for drawing radius
    Public draw_pt As PointF        'point for drawing text
    Public arr_pt1 As PointF        'used for drawing arrows
    Public arr_pt2 As PointF        'used for drawing arrows
    Public arr_pt3 As PointF        'used for drawing arrows
    Public arr_pt4 As PointF        'used for drawing arrows
    Public trans_angle As Integer   'angle between text and X-axis
    Public radius As Double         'radius of arc
    Public side_drag As PointF      'flag for side drawing

End Structure

'structure for annotation
Public Structure AnnoObject
    Public line_pt As PointF        'point used for drawing line
    Public size As Size             'size of anno object
End Structure

'structure for measure_scale
Public Structure ScaleObject
    Public style As String          'flag for horizontal or vertical
    Public length As Double         'the length of scale
    Public start_pt As PointF       'start point of scale
    Public end_pt As PointF         'end point of scale
    Public nor_pt1 As PointF        'used for drawing bounds
    Public nor_pt2 As PointF        'used for drawing bounds
    Public nor_pt3 As PointF        'used for drawing bounds
    Public nor_pt4 As PointF        'used for drawing bounds
    Public trans_angle As Integer   'angle between measure object and X-axis

End Structure

'structure for drawing objects
Public Structure MeasureObject
    Public start_point As PointF        'start point of object
    Public middle_point As PointF       'middle point of object
    Public end_point As PointF          'end point of object
    Public last_point As PointF         'optional, used for angle_far, the fourth point
    Public common_point As PointF       'optional, used for angle_far, the common point of first line and second line of angle

    Public draw_point As PointF         'point for drawing text
    Public measure_type As Integer      'measuring type of current object
    Public intialized As Boolean        'flag for specifying that object is initialized or not
    Public item_set As Integer          'the limit of points
    Public length As Double             'the length of object
    Public angle As Double              'optional, used for angle, angle_far
    Public radius As Double             'optional, used for radius
    Public annotation As String         'optional, used for annotation
    Public line_object As LineObject
    Public angle_object As AngleObject
    Public radius_object As RadiusObject
    Public anno_object As AnnoObject
    Public obj_num As Integer           'the order of current object
    Public line_infor As LineStyle      'information of drawing line
    Public scale_object As ScaleObject
    Public font_infor As FontInfor      'information of text font

    Public left_top As PointF           'the left top cornor of object
    Public right_bottom As PointF       'the right bottom cornor of object

    Public name As String               'the name of object
    Public remarks As String            'remarks of object

    Public Sub Refresh()
        start_point.X = 0
        start_point.Y = 0
        middle_point.X = 0
        middle_point.Y = 0
        end_point.X = 0
        end_point.Y = 0
        draw_point.X = 0
        draw_point.Y = 0
        last_point.X = 0
        last_point.Y = 0
        common_point.X = 0
        common_point.Y = 0

        length = 0
        angle = 0
        radius = 0
        annotation = ""
        intialized = False
        item_set = 0

        name = ""
        remarks = ""

    End Sub
End Structure

Public Class Main_Form
    Private origin_image As List(Of Mat) = New List(Of Mat)()           'original image
    Private resized_image As List(Of Mat) = New List(Of Mat)()          'the image which is resized to fit the picturebox control
    Private current_image As List(Of Mat) = New List(Of Mat)()          'the image which is currently used
    Private initial_ratio As Single() = New Single(24) {}               'the ratio of resized_image and original image
    Private zoom_factor As Double() = New Double(24) {}                 'the zooming factor
    Private cur_measure_type As Integer                                 'current measurement type
    Private cur_measure_type_prev As Integer                            'backup of current measurement type
    Private cur_obj_num As Integer() = New Integer(24) {}               'the number of current object
    Private obj_selected As MeasureObject = New MeasureObject()         'current measurement object
    Private object_list As List(Of List(Of MeasureObject)) = New List(Of List(Of MeasureObject))()        'the list of measurement objects
    Private ID_MY_TEXTBOX As TextBox() = New TextBox(24) {}             'textbox for editing annotation
    Private left_top As Point = New Point()                             'the position left top cornor of picture control in panel
    Private scroll_pos As Point = New Point()                           'the position of scroll bar
    Private anno_num As Integer                                         'the number of annotation object in the list
    Private graphFont As Font                                           'the font for text
    Private undo_num As Integer = 0                                     'count number of undo clicked and reset
    Private graphPen As Pen = New Pen(Color.Black, 1)                   'pen for drawing objects
    Private graphPen_line As Pen = New Pen(Color.Black, 1)              'pen for drawing lines
    Private dashValues As Single() = {5, 2}                             'format dash style of line
    Private line_infor As LineStyle = New LineStyle(1)                  'include the information of style, width, color ...
    Private side_drag As Boolean = False                                'flag of side drawing
    Private scale_style As String = "horizontal"                        'the style of measuring scale horizontal or vertical
    Private scale_value As Integer = 0                                  'the value of measuring scale
    Private scale_unit As String = "cm"                                 'unit of measuring scale may be cm, mm, ...
    Private ID_TAG_PAGE As TabPage() = New TabPage(24) {}               'tab includes panel
    Private ID_PANEL As Panel() = New Panel(24) {}                      'panel includes picturebox
    Private ID_PICTURE_BOX As PictureBox() = New PictureBox(24) {}      'picturebox for drawing objects
    Private tab_index As Integer = 0                                    'selected index of tab control
    Private CF As Double = 1.0                                          'the ratio of per pixel by per unit
    Private digit As Integer                                            'The digit of decimal numbers
    Private font_infor As FontInfor = New FontInfor(10)                 'include the information font and color
    Private brightness As Integer() = New Integer(24) {}                'brightness of current image
    Private contrast As Integer() = New Integer(24) {}                  'contrast of current image
    Private gamma As Integer() = New Integer(24) {}                     'gamma of current image
    Private sel_index As Integer = -1                                   'selected index for object
    Private m_cur_drag As PointF = New PointF()                         'the position of mouse cursor
    Private redraw_flag As Boolean                                      'flag for redrawing objects
    Private sel_pt_index As Integer = -1                                'selected index of a point of object
    Private tag_page_flag As Boolean() = New Boolean(24) {}             'specify that target tag page is opened
    Private img_import_flag As Boolean() = New Boolean(24) {}           'specify that you can import image in target tag
    Private name_list As List(Of String) = New List(Of String)          'specify the list of item names
    Private CF_list As List(Of String) = New List(Of String)            'specify the names of CF
    Private CF_num As List(Of Double) = New List(Of Double)             'specify the values of CF

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
    Private file_counter As Integer = 0                                 'the count of captured images
    Private camera_state As Boolean = False                             'the state of camera is opened or not
    Public imagepath As String = ""                                     'path of folder storing captured images
    Private flag As Boolean = False                                     'flag for live image

    'member variable for keygen
    Dim licState As licState                                            'the state of this program is licensed or not
    Public licModel As New licensInfoModel
    Dim licGen As New LicGen
    Dim licpath As String = "active.lic"                                'the path of license file
    Private path As String

    'member variable for setting.ini
    Private ini_path As String = "C:\Users\Public\Documents\setting.ini"    'the path of setting.ini
    Private ini As IniFile

    'member variable for Curves
    Private exe_path As String = "WindowsApp1.exe"
    Private ToCurveImg_path As String = "C:\Users\Public\Documents\ToCurve.bmp"    'the path of image for Curves
    Private ReturnedImg_path As String = "C:\Users\Public\Documents\To2D.bmp"       'the path of image returned from Curves
    Private ReturnedTxt_path As String = "C:\Users\Public\Documents\To2D.txt"    'the path of text file contains data-table

    Public Sub New()
        InitializeComponent()
        InitializeCustomeComeponent()

        anno_num = -1
        cur_measure_type = -1
        cur_measure_type_prev = -1
        graphPen_line.DashStyle = Drawing2D.DashStyle.Dot
        ID_BTN_CUR_COL.BackColor = Color.Black
        ID_BTN_TEXT_COL.BackColor = Color.Black
        ID_COMBO_LINE_SHAPE.SelectedIndex = 0
        Dim mat As Mat = Nothing

        For i = 0 To 24

            Dim list As List(Of MeasureObject) = New List(Of MeasureObject)()
            initial_ratio(i) = 1
            object_list.Add(list)
            cur_obj_num(i) = 0
            origin_image.Add(mat)
            resized_image.Add(mat)
            current_image.Add(mat)
            gamma(i) = 100
            zoom_factor(i) = 1.0
        Next

    End Sub

    Private Const EM_GETLINECOUNT As Integer = &HBA
    <DllImport("user32", EntryPoint:="SendMessageA", CharSet:=CharSet.Ansi, SetLastError:=True, ExactSpelling:=True)>
    Private Shared Function SendMessage(ByVal hwnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
    End Function

    <DllImport("user32.dll")>
    Private Shared Function SetCapture(ByVal hWnd As Integer) As IntPtr
    End Function

    <DllImport("user32.dll")>
    Private Shared Function ReleaseCapture() As Long
    End Function

    <DllImport("user32.dll")>
    Private Shared Function GetCapture() As IntPtr
    End Function

#Region "Main Form Methods"
    'Initialize custome controls
    Private Sub InitializeCustomeComeponent()
        graphFont = New Font("Arial", 10, FontStyle.Regular)
        For i = 0 To 24

            ID_TAG_PAGE(i) = New TabPage()
            ID_PANEL(i) = New Panel()
            ID_PICTURE_BOX(i) = New PictureBox()
            ID_MY_TEXTBOX(i) = New TextBox()

            ID_TAG_CTRL.Controls.Add(ID_TAG_PAGE(i))

            ID_TAG_PAGE(i).Location = New Point(4, 24)
            ID_TAG_PAGE(i).Name = "ID_TAG_PAGE" & i.ToString()
            ID_TAG_PAGE(i).Padding = New Padding(3)
            ID_TAG_PAGE(i).Size = New Size(800, 600)
            ID_TAG_PAGE(i).Text = "Image" & (i + 1).ToString()
            ID_TAG_PAGE(i).UseVisualStyleBackColor = True
            ID_TAG_PAGE(i).Controls.Add(ID_PANEL(i))

            ID_PANEL(i).Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
            ID_PANEL(i).AutoScroll = True
            ID_PANEL(i).AutoSizeMode = AutoSizeMode.GrowAndShrink
            ID_PANEL(i).BackColor = Color.Gray
            ID_PANEL(i).Location = New Point(0, 1)
            ID_PANEL(i).Name = "ID_PANEL" & i.ToString()
            ID_PANEL(i).Size = New Size(800, 600)
            AddHandler ID_PANEL(i).Scroll, New ScrollEventHandler(AddressOf ID_PANEL_Scroll)
            AddHandler ID_PANEL(i).SizeChanged, New EventHandler(AddressOf ID_PANEL_SizeChanged)
            AddHandler ID_PANEL(i).MouseWheel, New MouseEventHandler(AddressOf ID_PANEL_MouseWheel)
            ID_PANEL(i).Controls.Add(ID_PICTURE_BOX(i))

            ID_PICTURE_BOX(i).BackColor = Color.Gray
            ID_PICTURE_BOX(i).Location = New Point(0, -1)
            ID_PICTURE_BOX(i).Name = "ID_PICTURE_BOX" & i.ToString()
            ID_PICTURE_BOX(i).Size = New Size(800, 600)
            ID_PICTURE_BOX(i).SizeMode = PictureBoxSizeMode.AutoSize
            ID_PICTURE_BOX(i).TabIndex = 0
            ID_PICTURE_BOX(i).TabStop = False
            ID_PICTURE_BOX(i).Image = Nothing
            AddHandler ID_PICTURE_BOX(i).MouseDown, New MouseEventHandler(AddressOf ID_PICTURE_BOX_MouseDown)
            AddHandler ID_PICTURE_BOX(i).MouseMove, New MouseEventHandler(AddressOf ID_PICTURE_BOX_MouseMove)
            AddHandler ID_PICTURE_BOX(i).MouseDoubleClick, New MouseEventHandler(AddressOf ID_PICTURE_BOX_MouseDoubleClick)
            AddHandler ID_PICTURE_BOX(i).MouseUp, New MouseEventHandler(AddressOf ID_PICTURE_BOX_MouseUp)

            AddHandler ID_PICTURE_BOX(i).Paint, New PaintEventHandler(AddressOf ID_PICTURE_BOX_Paint)

            ID_PICTURE_BOX(i).Controls.Add(ID_MY_TEXTBOX(i))

            ID_MY_TEXTBOX(i).Name = "ID_MY_TEXTBOX"
            ID_MY_TEXTBOX(i).Multiline = True
            ID_MY_TEXTBOX(i).AutoSize = False
            ID_MY_TEXTBOX(i).Visible = False
            ID_MY_TEXTBOX(i).Font = graphFont
            AddHandler ID_MY_TEXTBOX(i).TextChanged, New EventHandler(AddressOf ID_MY_TEXTBOX_TextChanged)
        Next

        'remove unnessary tab pages
        For i = 1 To 24
            ID_TAG_CTRL.TabPages.Remove(ID_TAG_PAGE(i))
            tag_page_flag(i) = False
            img_import_flag(i) = True
        Next

        tag_page_flag(0) = True
        img_import_flag(0) = True


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
    End Sub

    'get setting information from ini file
    Private Sub GetInforFromIni()

        If IO.File.Exists(ini_path) Then
            ini = New IniFile(ini_path)
            Dim Keys As ArrayList = ini.GetKeys("Config")
            Dim myEnumerator As System.Collections.IEnumerator = Keys.GetEnumerator()
            While myEnumerator.MoveNext()
                If myEnumerator.Current.Name = "unit" Then
                    scale_unit = myEnumerator.Current.value
                Else
                    digit = CInt(myEnumerator.Current.value)
                End If
            End While
            ID_NUM_DIGIT.Value = digit

            Keys.Clear()
            Keys = ini.GetKeys("CF")
            Dim cnt As Integer = 0
            Dim index As Integer = 0
            myEnumerator = Keys.GetEnumerator()
            While myEnumerator.MoveNext()
                If myEnumerator.Current.Name = "index" Then
                    index = CInt(myEnumerator.Current.value)
                Else
                    Dim line As String = myEnumerator.Current.value
                    Dim parse_num = line.IndexOf(":")
                    Dim CF_key = line.Substring(0, parse_num)
                    Dim CF_val = CDbl(line.Substring(parse_num + 1))

                    CF_list.Add(CF_key)
                    CF_num.Add(CF_val)
                    ID_COMBOBOX_CF.Items.Add(CF_key)
                End If

            End While

            Keys.Clear()
            Keys = ini.GetKeys("name")
            cnt = 0
            myEnumerator = Keys.GetEnumerator()
            While myEnumerator.MoveNext()
                Dim line As String = myEnumerator.Current.value
                name_list.Add(line)
            End While

            ID_COMBOBOX_CF.SelectedIndex = index
        Else
            'set default value when ini file does not exist in document folder
            scale_unit = "cm"
            digit = 0
            ID_NUM_DIGIT.Value = digit

            Dim CF_cnt = 9

            CF_list.Add("1.0X")
            CF_num.Add(1.0)
            CF_list.Add("1.25X")
            CF_num.Add(1.25)
            CF_list.Add("1.5X")
            CF_num.Add(1.5)
            CF_list.Add("2.0X")
            CF_num.Add(2.0)
            CF_list.Add("2.5X")
            CF_num.Add(2.5)
            CF_list.Add("3.5X")
            CF_num.Add(3.5)
            CF_list.Add("5.0X")
            CF_num.Add(5.0)
            CF_list.Add("7.5X")
            CF_num.Add(7.5)
            CF_list.Add("10.0X")
            CF_num.Add(10.0)

            For i = 0 To CF_list.Count - 1
                ID_COMBOBOX_CF.Items.Add(CF_list(i))
            Next

            Dim name_cnt = 4
            name_list.Add("Line")
            name_list.Add("Angle")
            name_list.Add("Arc")
            name_list.Add("Scale")

            ID_COMBOBOX_CF.SelectedIndex = 0
        End If

    End Sub

    'check license information when main dialog is loading
    Private Sub Main_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Init()
            Initialize_Button_Colors()
            Timer1.Interval = 30
            Timer1.Start()
            GetInforFromIni()

        Catch ex As Exception

            'ID_GROUP_BOX_CONTROL.Enabled = False
            MessageBox.Show(ex.Message.ToString())

        End Try

        Try
            OpenCamera()
            SelectResolution(videoDevice, CameraResolutionsCB)
            If Not My.Settings.camresindex.Equals("") Then
                CameraResolutionsCB.SelectedIndex = My.Settings.camresindex + 1
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        If My.Settings.imagefilepath.Equals("") Then
            imagepath = "MyImages"
            My.Settings.imagefilepath = imagepath
            My.Settings.Save()
            txtbx_imagepath.Text = imagepath
        Else
            imagepath = My.Settings.imagefilepath
            txtbx_imagepath.Text = My.Settings.imagefilepath
        End If


        DeleteImages(imagepath)
        Createdirectory(imagepath)
    End Sub

    'change the color of button when it is clicked
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If cur_measure_type_prev <> cur_measure_type Then
            Initialize_Button_Colors()
            cur_measure_type_prev = cur_measure_type
            Select Case cur_measure_type
                Case 0
                    ID_BTN_LINE_ALIGN.BackColor = Color.DodgerBlue
                Case 1
                    ID_BTN_LINE_HOR.BackColor = Color.DodgerBlue
                Case 2
                    ID_BTN_LINE_VER.BackColor = Color.DodgerBlue
                Case 3
                    ID_BTN_ARC.BackColor = Color.DodgerBlue
                Case 4
                    ID_BTN_RADIUS.BackColor = Color.DodgerBlue
                Case 5
                    ID_BTN_ANNOTATION.BackColor = Color.DodgerBlue
                Case 6
                    ID_BTN_ANGLE.BackColor = Color.DodgerBlue
                Case 7
                    ID_BTN_LINE_PARA.BackColor = Color.DodgerBlue
                Case 8
                    ID_BTN_PENCIL.BackColor = Color.DodgerBlue
                Case 9
                    ID_BTN_P_LINE.BackColor = Color.DodgerBlue
                Case 10
                    ID_BTN_SCALE.BackColor = Color.DodgerBlue
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
            ID_PICTURE_BOX(0).Image = Nothing
            ID_PICTURE_BOX_CAM.Image = Nothing
        Catch excpt As Exception
            MessageBox.Show(excpt.Message)
        End Try
    End Sub

    'import image and draw it to picturebox
    'format variables
    Private Sub ID_MENU_OPEN_Click(sender As Object, e As EventArgs) Handles ID_MENU_OPEN.Click
        cur_measure_type = -1

        Dim filter = "JPEG Files|*.jpg|PNG Files|*.png|BMP Files|*.bmp|All Files|*.*"
        Dim title = "Open"

        Dim start As Integer = tab_index
        img_import_flag(tab_index) = True

        Dim img_cnt = ID_PICTURE_BOX(0).LoadImageFromFiles(filter, title, origin_image, resized_image, initial_ratio, start, img_import_flag)

        If img_cnt >= 1 Then
            ID_PICTURE_BOX(tab_index).Image = Nothing
        End If
        Dim added_tag = 0
        While added_tag < img_cnt
            If start > 24 Then Exit While

            If tag_page_flag(start) = True AndAlso ID_PICTURE_BOX(start).Image IsNot Nothing Then
                start = start + 1
                Continue While
            End If

            If tag_page_flag(start) = False Then
                ID_TAG_CTRL.TabPages.Add(ID_TAG_PAGE(start))
                tag_page_flag(start) = True
            End If

            Dim img = resized_image.ElementAt(start)
            ID_PICTURE_BOX(start).Invoke(New Action(Sub() ID_PICTURE_BOX(start).Image = img.ToBitmap()))

            left_top = ID_PICTURE_BOX(start).CenteringImage(ID_PANEL(start))
            current_image(start) = img
            cur_obj_num(start) = 0
            Enumerable.ElementAt(Of List(Of MeasureObject))(object_list, start).Clear()
            brightness(start) = 0
            contrast(start) = 0
            gamma(start) = 100
            img_import_flag(start) = False

            start = start + 1
            added_tag = added_tag + 1
        End While

        start = start - 1
        ID_LISTVIEW.LoadObjectList(object_list.ElementAt(start), CF, digit, scale_unit, name_list)
        ID_TAG_CTRL.SelectedTab = ID_TAG_PAGE(start)
    End Sub

    'export image to jpg
    Private Sub ID_MENU_SAVE_Click(sender As Object, e As EventArgs) Handles ID_MENU_SAVE.Click
        Dim filter = "JPEG Files|*.jpg"
        Dim title = "Save"
        ID_PICTURE_BOX(tab_index).SaveImageInFile(filter, title, object_list.ElementAt(tab_index), graphPen, graphPen_line, digit, CF)
    End Sub

    'save object information as excel file
    Private Sub ID_MENU_SAVE_XLSX_Click(sender As Object, e As EventArgs) Handles ID_MENU_SAVE_XLSX.Click
        Dim filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*"
        Dim title = "Save"
        ID_PICTURE_BOX.SaveListToExcel(object_list, filter, title, CF, digit, scale_unit)
    End Sub

    'save object list and image as excel file
    Private Sub ID_MENU_EXPORT_REPORT_Click(sender As Object, e As EventArgs) Handles ID_MENU_EXPORT_REPORT.Click
        Dim filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*"
        Dim title = "Save"
        ID_PICTURE_BOX.SaveReportToExcel(filter, title, object_list, graphPen, graphPen_line, digit, CF, scale_unit)
    End Sub

    'exit the program
    Private Sub ID_MENU_EXIT_Click(sender As Object, e As EventArgs) Handles ID_MENU_EXIT.Click
        Call Application.Exit()
    End Sub

    'set current measurement type as line_align
    'reset the current object
    Private Sub ID_BTN_LINE_ALIGN_Click(sender As Object, e As EventArgs) Handles ID_BTN_LINE_ALIGN.Click
        cur_measure_type = MeasureType.line_align
        obj_selected.measure_type = cur_measure_type
        obj_selected.Refresh()
    End Sub

    'set current measurement type as line_horizontal
    'reset the current object
    Private Sub ID_BTN_LINE_HOR_Click(sender As Object, e As EventArgs) Handles ID_BTN_LINE_HOR.Click
        cur_measure_type = MeasureType.line_horizontal
        obj_selected.measure_type = cur_measure_type
        obj_selected.Refresh()
    End Sub

    'set current measurement type as line_vertical
    'reset the current object
    Private Sub ID_BTN_LINE_VER_Click(sender As Object, e As EventArgs) Handles ID_BTN_LINE_VER.Click
        cur_measure_type = MeasureType.line_vertical
        obj_selected.measure_type = cur_measure_type
        obj_selected.Refresh()

    End Sub

    'set current measurement type as line parallel
    'reset the current object
    Private Sub ID_BTN_LINE_PARA_Click(sender As Object, e As EventArgs) Handles ID_BTN_LINE_PARA.Click
        cur_measure_type = MeasureType.line_para
        obj_selected.measure_type = cur_measure_type
        obj_selected.Refresh()

    End Sub

    'set current measurement type as angle
    'reset the current object
    Private Sub ID_BTN_ARC_Click(sender As Object, e As EventArgs) Handles ID_BTN_ARC.Click
        cur_measure_type = MeasureType.angle
        obj_selected.measure_type = cur_measure_type
        obj_selected.Refresh()

    End Sub

    'set current measurement type as angle far
    'reset the current object
    Private Sub ID_BTN_ANGLE_Click(sender As Object, e As EventArgs) Handles ID_BTN_ANGLE.Click
        cur_measure_type = MeasureType.angle_far
        obj_selected.measure_type = cur_measure_type
        obj_selected.Refresh()

    End Sub

    'set current measurement type as radius
    'reset the current object
    Private Sub ID_BTN_RADIUS_Click(sender As Object, e As EventArgs) Handles ID_BTN_RADIUS.Click
        cur_measure_type = MeasureType.radius
        obj_selected.measure_type = cur_measure_type
        obj_selected.Refresh()

    End Sub

    'set current measurement type as annotation
    'reset the current object
    Private Sub ID_BTN_ANNOTATION_Click(sender As Object, e As EventArgs) Handles ID_BTN_ANNOTATION.Click
        cur_measure_type = MeasureType.annotation
        obj_selected.measure_type = cur_measure_type
        obj_selected.Refresh()

    End Sub

    'set current measurement type as draw line
    'reset the current object
    Private Sub ID_BTN_PENCIL_Click(sender As Object, e As EventArgs) Handles ID_BTN_PENCIL.Click
        cur_measure_type = MeasureType.draw_line
        obj_selected.measure_type = cur_measure_type
        obj_selected.Refresh()

    End Sub

    'set current measurement type as point to line
    'reset the current object
    Private Sub ID_BTN_P_LINE_Click(sender As Object, e As EventArgs) Handles ID_BTN_P_LINE.Click
        cur_measure_type = MeasureType.pt_line
        obj_selected.measure_type = cur_measure_type
        obj_selected.Refresh()

    End Sub

    'set measureing scale 
    'set current measurement type as measuring scale
    'reset the current object
    Private Sub ID_BTN_SCALE_Click(sender As Object, e As EventArgs) Handles ID_BTN_SCALE.Click

        Dim form As ID_FORM_SCALE = New ID_FORM_SCALE(scale_unit)
        If form.ShowDialog() = DialogResult.OK Then
            scale_style = form.scale_style
            scale_value = form.scale_value
            scale_unit = form.scale_unit

            cur_measure_type = MeasureType.measure_scale
            obj_selected.measure_type = cur_measure_type
            obj_selected.Refresh()
            obj_selected.scale_object.style = scale_style
            obj_selected.scale_object.length = scale_value
        End If

    End Sub

    'zoom in image and draw it to picturebox
    Private Sub ID_BTN_ZOON_IN_Click(sender As Object, e As EventArgs) Handles ID_BTN_ZOON_IN.Click
        zoom_factor(tab_index) *= 1.1
        Dim ratio = zoom_factor(tab_index) * initial_ratio(tab_index)
        current_image(tab_index) = ZoomImage(ratio, origin_image, current_image, tab_index)
        ID_PICTURE_BOX(tab_index).Invoke(New Action(Sub() ID_PICTURE_BOX(tab_index).Image = Enumerable.ElementAt(current_image, tab_index).ToBitmap()))
        left_top = ID_PICTURE_BOX(tab_index).CenteringImage(ID_PANEL(tab_index))
        scroll_pos.X = ID_PANEL(tab_index).HorizontalScroll.Value
        scroll_pos.Y = ID_PANEL(tab_index).VerticalScroll.Value
        ID_PICTURE_BOX(tab_index).DrawObjList(object_list.ElementAt(tab_index), graphPen, graphPen_line, digit, CF, False)
        Dim flag = False
        If sel_index >= 0 Then flag = True
        ID_PICTURE_BOX(tab_index).DrawObjSelected(obj_selected, flag)
        If ID_MY_TEXTBOX(tab_index).Visible = True Then
            Dim obj_anno = object_list.ElementAt(tab_index).ElementAt(anno_num)
            Dim st_pt As Point = New Point(obj_anno.draw_point.X * ID_PICTURE_BOX(tab_index).Width, obj_anno.draw_point.Y * ID_PICTURE_BOX(tab_index).Height)
            ID_MY_TEXTBOX(tab_index).UpdateLocation(st_pt, left_top, scroll_pos)
        End If
    End Sub

    'zoom out image and draw it to picturebox
    Private Sub ID_BTN_ZOOM_OUT_Click(sender As Object, e As EventArgs) Handles ID_BTN_ZOOM_OUT.Click
        zoom_factor(tab_index) /= 1.1
        Dim ratio = zoom_factor(tab_index) * initial_ratio(tab_index)
        'zoom_factor = 1 / 1.1
        current_image(tab_index) = ZoomImage(ratio, origin_image, current_image, tab_index)

        ID_PICTURE_BOX(tab_index).Invoke(New Action(Sub() ID_PICTURE_BOX(tab_index).Image = Enumerable.ElementAt(current_image, tab_index).ToBitmap()))

        left_top = ID_PICTURE_BOX(tab_index).CenteringImage(ID_PANEL(tab_index))
        scroll_pos.X = ID_PANEL(tab_index).HorizontalScroll.Value
        scroll_pos.Y = ID_PANEL(tab_index).VerticalScroll.Value
        ID_PICTURE_BOX(tab_index).DrawObjList(object_list.ElementAt(tab_index), graphPen, graphPen_line, digit, CF, False)
        Dim flag = False
        If sel_index >= 0 Then flag = True
        ID_PICTURE_BOX(tab_index).DrawObjSelected(obj_selected, flag)
        If ID_MY_TEXTBOX(tab_index).Visible = True Then
            Dim obj_anno = object_list.ElementAt(tab_index).ElementAt(anno_num)
            Dim st_pt As Point = New Point(obj_anno.draw_point.X * ID_PICTURE_BOX(tab_index).Width, obj_anno.draw_point.Y * ID_PICTURE_BOX(tab_index).Height)
            ID_MY_TEXTBOX(tab_index).UpdateLocation(st_pt, left_top, scroll_pos)
        End If
    End Sub

    'undo last object and last row of listview
    Private Sub ID_BTN_UNDO_Click(sender As Object, e As EventArgs) Handles ID_BTN_UNDO.Click
        If undo_num > 0 Then
            obj_selected.Refresh()
            sel_index = -1
            sel_pt_index = -1
            Dim flag = RemoveObjFromList(object_list.ElementAt(tab_index))
            If flag = True Then
                ID_PICTURE_BOX(tab_index).DrawObjList(object_list.ElementAt(tab_index), graphPen, graphPen_line, digit, CF, False)
                ID_LISTVIEW.LoadObjectList(object_list.ElementAt(tab_index), CF, digit, scale_unit, name_list)
                undo_num -= 1
                cur_obj_num(tab_index) -= 1
            End If
        End If
    End Sub

    'reset current object
    Private Sub ID_BTN_RESEL_Click(sender As Object, e As EventArgs) Handles ID_BTN_RESEL.Click
        obj_selected.Refresh()
        ID_PICTURE_BOX(tab_index).DrawObjList(object_list.ElementAt(tab_index), graphPen, graphPen_line, digit, CF, False)
    End Sub

    'reset digit
    'reload image and obj_list
    Private Sub ID_NUM_DIGIT_ValueChanged(sender As Object, e As EventArgs) Handles ID_NUM_DIGIT.ValueChanged
        digit = CInt(ID_NUM_DIGIT.Value)
        ID_PICTURE_BOX(tab_index).DrawObjList(object_list.ElementAt(tab_index), graphPen, graphPen_line, digit, CF, False)
        ID_LISTVIEW.LoadObjectList(object_list.ElementAt(tab_index), CF, digit, scale_unit, name_list)
    End Sub

    'set the width of line
    Private Sub ID_NUM_LINE_WIDTH_ValueChanged(sender As Object, e As EventArgs) Handles ID_NUM_LINE_WIDTH.ValueChanged
        line_infor.line_width = CInt(ID_NUM_LINE_WIDTH.Value)
    End Sub

    'change the color of LineStyle object
    Private Sub ID_BTN_COL_PICKER_Click(sender As Object, e As EventArgs) Handles ID_BTN_COL_PICKER.Click
        Dim clrDialog As ColorDialog = New ColorDialog()

        'show the colour dialog and check that user clicked ok
        If clrDialog.ShowDialog() = DialogResult.OK Then
            'save the colour that the user chose
            line_infor.line_color = clrDialog.Color
            ID_BTN_CUR_COL.BackColor = clrDialog.Color
        End If
    End Sub

    'set the line style
    Private Sub ID_COMBO_LINE_SHAPE_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ID_COMBO_LINE_SHAPE.SelectedIndexChanged
        Dim comboIndex = ID_COMBO_LINE_SHAPE.SelectedIndex
        If comboIndex = 0 Then
            graphPen_line.DashStyle = Drawing2D.DashStyle.Dot
            'obj_selected.line_shape = "dotted";
            line_infor.line_style = "dotted"
        ElseIf comboIndex = 1 Then
            graphPen_line.DashPattern = dashValues
            'obj_selected.line_shape = "dashed";
            line_infor.line_style = "dashed"
        End If
    End Sub

    'set text fore color
    Private Sub ID_BTN_TEXT_COL_PICKER_Click(sender As Object, e As EventArgs) Handles ID_BTN_TEXT_COL_PICKER.Click
        Dim clrDialog As ColorDialog = New ColorDialog()

        'show the colour dialog and check that user clicked ok
        If clrDialog.ShowDialog() = DialogResult.OK Then
            'save the colour that the user chose
            font_infor.font_color = clrDialog.Color
            ID_BTN_TEXT_COL.BackColor = clrDialog.Color
        End If
    End Sub

    'set text font
    Private Sub ID_BTN_TEXT_FONT_Click(sender As Object, e As EventArgs) Handles ID_BTN_TEXT_FONT.Click
        Dim fontDialog As FontDialog = New FontDialog()

        If fontDialog.ShowDialog() = DialogResult.OK Then
            font_infor.text_font = fontDialog.Font
        End If
    End Sub

    'redraw objects
    Private Sub ID_PANEL_Scroll(sender As Object, e As ScrollEventArgs)
        ID_PICTURE_BOX(tab_index).DrawObjList(object_list.ElementAt(tab_index), graphPen, graphPen_line, digit, CF, False)
        Dim flag = False
        If sel_index >= 0 Then flag = True
        ID_PICTURE_BOX(tab_index).DrawObjSelected(obj_selected, flag)
    End Sub

    'keep the image in the center when the panel size in changed
    'redraw objects
    Private Sub ID_PANEL_SizeChanged(sender As Object, e As EventArgs)
        left_top = ID_PICTURE_BOX(tab_index).CenteringImage(ID_PANEL(tab_index))
        scroll_pos.X = ID_PANEL(tab_index).HorizontalScroll.Value
        scroll_pos.Y = ID_PANEL(tab_index).VerticalScroll.Value
        ID_PICTURE_BOX(tab_index).DrawObjList(object_list.ElementAt(tab_index), graphPen, graphPen_line, digit, CF, False)
        Dim flag = False
        If sel_index >= 0 Then flag = True
        ID_PICTURE_BOX(tab_index).DrawObjSelected(obj_selected, flag)
        If ID_MY_TEXTBOX(tab_index).Visible = True Then
            Dim obj_anno = object_list.ElementAt(tab_index).ElementAt(anno_num)
            Dim st_pt As Point = New Point(obj_anno.draw_point.X * ID_PICTURE_BOX(tab_index).Width, obj_anno.draw_point.Y * ID_PICTURE_BOX(tab_index).Height)
            ID_MY_TEXTBOX(tab_index).UpdateLocation(st_pt, left_top, scroll_pos)
        End If
    End Sub

    'redraw objects
    Private Sub ID_PANEL_MouseWheel(sender As Object, e As MouseEventArgs)
        Dim flag = False
        If sel_index >= 0 Then flag = True
        ID_PICTURE_BOX(tab_index).DrawObjList(object_list.ElementAt(tab_index), graphPen, graphPen_line, digit, CF, False)
        ID_PICTURE_BOX(tab_index).DrawObjSelected(obj_selected, flag)
    End Sub

    'update object selected
    'when mouse is clicked on annotation insert textbox there to you can edit it
    'draw objects and load list of objects to listview
    Private Sub ID_PICTURE_BOX_MouseDown(sender As Object, e As MouseEventArgs)
        If ID_PICTURE_BOX(tab_index).Image Is Nothing OrElse current_image(tab_index) Is Nothing Then
            Return
        End If
        SetCapture(CInt(ID_PICTURE_BOX(tab_index).Handle))
        Dim m_pt As PointF = New Point()
        m_pt.X = CSng(e.X) / ID_PICTURE_BOX(tab_index).Width
        m_pt.Y = CSng(e.Y) / ID_PICTURE_BOX(tab_index).Height
        m_pt.X = Math.Min(Math.Max(m_pt.X, 0), 1)
        m_pt.Y = Math.Min(Math.Max(m_pt.Y, 0), 1)
        m_cur_drag = m_pt

        Dim m_pt2 As Point = New Point(e.X, e.Y)


        If cur_measure_type >= 0 Then
            Dim completed = ModifyObjSelected(obj_selected, cur_measure_type, m_pt, Enumerable.ElementAt(origin_image, tab_index).Width, Enumerable.ElementAt(origin_image, tab_index).Height, line_infor, font_infor, CF)

            If completed Then
                obj_selected.obj_num = cur_obj_num(tab_index)
                object_list(tab_index).Add(obj_selected)
                ID_PICTURE_BOX(tab_index).DrawObjList(object_list.ElementAt(tab_index), graphPen, graphPen_line, digit, CF, False)
                ID_LISTVIEW.LoadObjectList(object_list.ElementAt(tab_index), CF, digit, scale_unit, name_list)
                obj_selected.Refresh()
                cur_measure_type = -1
                cur_obj_num(tab_index) += 1
                If undo_num < 2 Then undo_num += 1
            Else
                ID_PICTURE_BOX(tab_index).DrawObjList(object_list.ElementAt(tab_index), graphPen, graphPen_line, digit, CF, False)
                ID_PICTURE_BOX(tab_index).DrawObjSelected(obj_selected, False)
            End If
        Else
            'select point of selected object
            If sel_index >= 0 Then
                sel_pt_index = ID_PICTURE_BOX(tab_index).CheckPointInPos(object_list.ElementAt(tab_index).ElementAt(sel_index), m_pt2)
                If sel_pt_index >= 0 Then
                    ID_PICTURE_BOX(tab_index).DrawObjList(object_list.ElementAt(tab_index), graphPen, graphPen_line, digit, CF, False)
                    ID_PICTURE_BOX(tab_index).HightLightItem(object_list.ElementAt(tab_index).ElementAt(sel_index), ID_PICTURE_BOX(tab_index).Width, ID_PICTURE_BOX(tab_index).Height, CF)
                    ID_PICTURE_BOX(tab_index).DrawObjSelected(object_list.ElementAt(tab_index).ElementAt(sel_index), True)
                    ID_PICTURE_BOX(tab_index).HighlightTargetPt(object_list.ElementAt(tab_index).ElementAt(sel_index), sel_pt_index)
                    Return
                End If
            End If

            sel_index = CheckItemInPos(m_pt, object_list.ElementAt(tab_index), ID_PICTURE_BOX(tab_index).Width, ID_PICTURE_BOX(tab_index).Height, CF)
            If sel_index >= 0 Then
                ID_PICTURE_BOX(tab_index).DrawObjList(object_list.ElementAt(tab_index), graphPen, graphPen_line, digit, CF, False)
                ID_PICTURE_BOX(tab_index).HightLightItem(object_list.ElementAt(tab_index).ElementAt(sel_index), ID_PICTURE_BOX(tab_index).Width, ID_PICTURE_BOX(tab_index).Height, CF)
                ID_PICTURE_BOX(tab_index).DrawObjSelected(object_list.ElementAt(tab_index).ElementAt(sel_index), True)
            Else
                If anno_num >= 0 Then
                    ID_MY_TEXTBOX(tab_index).DisableTextBox(object_list.ElementAt(tab_index), anno_num, ID_PICTURE_BOX(tab_index).Width, ID_PICTURE_BOX(tab_index).Height)
                    ID_LISTVIEW.LoadObjectList(object_list.ElementAt(tab_index), CF, digit, scale_unit, name_list)
                    anno_num = -1
                End If
                ID_PICTURE_BOX(tab_index).DrawObjList(object_list.ElementAt(tab_index), graphPen, graphPen_line, digit, CF, False)
            End If
        End If
    End Sub

    'release capture
    Private Sub ID_PICTURE_BOX_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs)
        Call ReleaseCapture()

    End Sub


    'draw temporal objects according to mouse cursor
    Private Sub ID_PICTURE_BOX_MouseMove(sender As Object, e As MouseEventArgs)
        If cur_measure_type >= 0 Then
            Dim m_pt As Point = New Point(e.X, e.Y)
            ID_PICTURE_BOX(tab_index).DrawObjList(object_list.ElementAt(tab_index), graphPen, graphPen_line, digit, CF, False)
            ID_PICTURE_BOX(tab_index).DrawObjSelected(obj_selected, False)
            ID_PICTURE_BOX(tab_index).DrawTempFinal(obj_selected, m_pt, side_drag, digit, CF, True)
        ElseIf GetCapture() = ID_PICTURE_BOX(tab_index).Handle AndAlso sel_index >= 0 Then
            Dim m_pt As PointF = New PointF()
            m_pt.X = CSng(e.X) / ID_PICTURE_BOX(tab_index).Width
            m_pt.Y = CSng(e.Y) / ID_PICTURE_BOX(tab_index).Height
            m_pt.X = Math.Min(Math.Max(m_pt.X, 0), 1)
            m_pt.Y = Math.Min(Math.Max(m_pt.Y, 0), 1)
            Dim dx = m_pt.X - m_cur_drag.X
            Dim dy = m_pt.Y - m_cur_drag.Y
            m_cur_drag = m_pt

            If sel_pt_index >= 0 Then

                ID_PICTURE_BOX(tab_index).Refresh()
                MovePoint(object_list.ElementAt(tab_index), sel_index, sel_pt_index, dx, dy)
                ModifyObjSelected(object_list.ElementAt(tab_index), sel_index, Enumerable.ElementAt(origin_image, tab_index).Width, Enumerable.ElementAt(origin_image, tab_index).Height)
                Dim obj = object_list.ElementAt(tab_index).ElementAt(sel_index)
                Dim target_pt As Point = New Point()
                If obj.measure_type = MeasureType.angle Then

                    Dim start_point As Point = New Point()
                    Dim end_point As Point = New Point()
                    Dim middle_point As Point = New Point()

                    start_point.X = CInt(obj.start_point.X * ID_PICTURE_BOX(tab_index).Width)
                    start_point.Y = CInt(obj.start_point.Y * ID_PICTURE_BOX(tab_index).Height)
                    middle_point.X = CInt(obj.middle_point.X * ID_PICTURE_BOX(tab_index).Width)
                    middle_point.Y = CInt(obj.middle_point.Y * ID_PICTURE_BOX(tab_index).Height)
                    end_point.X = CInt(obj.end_point.X * ID_PICTURE_BOX(tab_index).Width)
                    end_point.Y = CInt(obj.end_point.Y * ID_PICTURE_BOX(tab_index).Height)

                    target_pt.X = (start_point.X + end_point.X) / 2
                    target_pt.Y = (start_point.Y + end_point.Y) / 2
                    Dim angles = CalcStartAndSweepAngle(obj, start_point, middle_point, end_point, target_pt)
                    Dim start_angle, sweep_angle As Double
                    start_angle = angles(0)
                    sweep_angle = angles(1)
                    Dim angle As Integer = CInt(2 * start_angle + sweep_angle) / 2
                    Dim radius = CInt(obj.angle_object.radius * ID_PICTURE_BOX(tab_index).Width) + 10
                    target_pt = CalcPositionInCircle(middle_point, radius, angle)
                Else
                    target_pt = New Point(obj.draw_point.X * ID_PICTURE_BOX(tab_index).Width, obj.draw_point.Y * ID_PICTURE_BOX(tab_index).Height)
                End If
                ID_PICTURE_BOX(tab_index).DrawTempFinal(obj, target_pt, side_drag, digit, CF, False)
                object_list(tab_index)(sel_index) = obj
                ID_PICTURE_BOX(tab_index).DrawObjList(object_list.ElementAt(tab_index), graphPen, graphPen_line, digit, CF, False)
                ID_LISTVIEW.LoadObjectList(object_list.ElementAt(tab_index), CF, digit, scale_unit, name_list)
            Else
                MoveObject(object_list.ElementAt(tab_index), sel_index, dx, dy)
                ID_PICTURE_BOX(tab_index).DrawObjList(object_list.ElementAt(tab_index), graphPen, graphPen_line, digit, CF, False)
            End If
            ID_PICTURE_BOX(tab_index).HightLightItem(object_list.ElementAt(tab_index).ElementAt(sel_index), ID_PICTURE_BOX(tab_index).Width, ID_PICTURE_BOX(tab_index).Height, CF)
            ID_PICTURE_BOX(tab_index).DrawObjSelected(object_list.ElementAt(tab_index).ElementAt(sel_index), True)
        End If
    End Sub

    'select annotation
    Private Sub ID_PICTURE_BOX_MouseDoubleClick(ByVal sender As Object, ByVal e As MouseEventArgs)
        Dim m_pt As PointF = New Point()
        m_pt.X = CSng(e.X) / ID_PICTURE_BOX(tab_index).Width
        m_pt.Y = CSng(e.Y) / ID_PICTURE_BOX(tab_index).Height
        m_pt.X = Math.Min(Math.Max(m_pt.X, 0), 1)
        m_pt.Y = Math.Min(Math.Max(m_pt.Y, 0), 1)

        Dim an_num = CheckAnnotation(m_pt, object_list.ElementAt(tab_index), ID_PICTURE_BOX(tab_index).Width, ID_PICTURE_BOX(tab_index).Height)
        If an_num >= 0 AndAlso Enumerable.ElementAt(Of MeasureObject)(Enumerable.ElementAt(Of List(Of MeasureObject))(object_list, tab_index), an_num).measure_type = MeasureType.annotation Then
            ID_MY_TEXTBOX(tab_index).Font = font_infor.text_font
            ID_MY_TEXTBOX(tab_index).EnableTextBox(object_list.ElementAt(tab_index).ElementAt(an_num), ID_PICTURE_BOX(tab_index).Width, ID_PICTURE_BOX(tab_index).Height, left_top, scroll_pos)
            anno_num = an_num
        End If
    End Sub

    'draw objects to picturebox when ID_FORM_BRIGHTNESS is actived
    Private Sub ID_PICTURE_BOX_Paint(ByVal sender As Object, ByVal e As PaintEventArgs)
        If redraw_flag Then ID_PICTURE_BOX(tab_index).DrawObjList(object_list.ElementAt(tab_index), graphPen, graphPen_line, digit, CF, True)
    End Sub

    'change the size of textbox when the text is changed
    Private Sub ID_MY_TEXTBOX_TextChanged(sender As Object, e As EventArgs)
        Dim textBox = CType(sender, TextBox)
        Dim numberOfLines = SendMessage(textBox.Handle.ToInt32(), EM_GETLINECOUNT, 0, 0)
        textBox.Height = (textBox.Font.Height + 2) * numberOfLines
    End Sub

    'set tab_index
    'reload image and object list
    Private Sub ID_TAG_CTRL_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ID_TAG_CTRL.SelectedIndexChanged
        Dim tab_name As String = ID_TAG_CTRL.SelectedTab.Name
        tab_name = tab_name.Substring(11)
        tab_index = CInt(tab_name)
        ID_MY_TEXTBOX(tab_index).Visible = False
        left_top = ID_PICTURE_BOX(tab_index).CenteringImage(ID_PANEL(tab_index))
        ID_PICTURE_BOX(tab_index).DrawObjList(object_list.ElementAt(tab_index), graphPen, graphPen_line, digit, CF, False)
        ID_LISTVIEW.LoadObjectList(object_list.ElementAt(tab_index), CF, digit, scale_unit, name_list)
        obj_selected.Refresh()
        cur_measure_type = -1
        sel_index = -1
    End Sub

    'set brightness, contrast and gamma to current image
    Private Sub ID_BTN_BRIGHTNESS_Click(sender As Object, e As EventArgs) Handles ID_BTN_BRIGHTNESS.Click
        redraw_flag = True
        Dim form As ID_FORM_BRIGHTNESS = New ID_FORM_BRIGHTNESS(ID_PICTURE_BOX(tab_index), Enumerable.ElementAt(resized_image, tab_index).ToBitmap(), brightness(tab_index), contrast(tab_index), gamma(tab_index))
        Dim image = origin_image(tab_index).Clone().ToBitmap()
        Dim InitialImage = AdjustBrightnessAndContrast(image, brightness(tab_index), contrast(tab_index), gamma(tab_index))
        If form.ShowDialog() = DialogResult.OK Then
            brightness(tab_index) = form.brightness
            contrast(tab_index) = form.contrast
            gamma(tab_index) = form.gamma
            current_image(tab_index) = GetMatFromSDImage(ID_PICTURE_BOX(tab_index).Image)

            'Dim UpdatedImage = AdjustBrightnessAndContrast(image, brightness(tab_index), contrast(tab_index), gamma(tab_index))
            'origin_image(tab_index) = GetMatFromSDImage(UpdatedImage)
        Else
            ID_PICTURE_BOX(tab_index).Image = form.InitialImage
            'origin_image(tab_index) = GetMatFromSDImage(InitialImage)
        End If
        redraw_flag = False
        ID_PICTURE_BOX(tab_index).DrawObjList(object_list.ElementAt(tab_index), graphPen, graphPen_line, digit, CF, False)
    End Sub

    'add tag page at the end
    Private Sub ID_BTN_TAB_ADD_Click(sender As Object, e As EventArgs) Handles ID_BTN_TAB_ADD.Click
        If tab_index >= 24 Then
            Return
        End If

        tab_index += 1
        If tag_page_flag(tab_index) = False Then
            tag_page_flag(tab_index) = True

            ID_PICTURE_BOX(tab_index).Image = Nothing
            current_image(tab_index) = Nothing
            resized_image(tab_index) = Nothing
            origin_image(tab_index) = Nothing
            cur_obj_num(tab_index) = 0
            Enumerable.ElementAt(Of List(Of MeasureObject))(object_list, tab_index).Clear()
            brightness(tab_index) = 0
            contrast(tab_index) = 0
            gamma(tab_index) = 100
            img_import_flag(tab_index) = True

            ID_TAG_CTRL.TabPages.Add(ID_TAG_PAGE(tab_index))
            ID_TAG_CTRL.SelectedTab = ID_TAG_PAGE(tab_index)
        End If
    End Sub

    'remove last tag page
    Private Sub ID_BTN_TAB_REMOVE_Click(sender As Object, e As EventArgs) Handles ID_BTN_TAB_REMOVE.Click
        If tab_index < 0 Then
            Return
        End If

        If tag_page_flag(tab_index) = True Then

            current_image(tab_index) = Nothing
            resized_image(tab_index) = Nothing
            origin_image(tab_index) = Nothing
            cur_obj_num(tab_index) = 0
            Enumerable.ElementAt(Of List(Of MeasureObject))(object_list, tab_index).Clear()
            brightness(tab_index) = 0
            contrast(tab_index) = 0
            gamma(tab_index) = 100
            img_import_flag(tab_index) = True
            ID_PICTURE_BOX(tab_index).Image = Nothing

            If tab_index = 0 Then
                ID_TAG_CTRL.SelectedIndex = 0
            Else
                tag_page_flag(tab_index) = False
                Dim cur_index = ID_TAG_CTRL.SelectedIndex
                ID_TAG_CTRL.TabPages.Remove(ID_TAG_PAGE(tab_index))
                ID_TAG_CTRL.SelectedIndex = cur_index - 1
            End If

        End If

    End Sub

    'save setting information to setting.ini
    'close camera
    Private Sub Main_Form_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        On Error Resume Next

        If IO.File.Exists(ini_path) Then
            If ini IsNot Nothing Then
                ini.ChangeValue("unit", "Config", scale_unit)
                ini.ChangeValue("digit", "Config", digit.ToString())
                ini.ChangeValue("index", "CF", ID_COMBOBOX_CF.SelectedIndex)
                ini.Save(ini_path)
            End If
        Else
            'set default value when ini file does Not exist in document folder
            ini = New IniFile(ini_path)
            ini.AddSection("Config")
            ini.AddKey("unit", scale_unit, "Config")
            ini.AddKey("digit", digit.ToString(), "Config")

            ini.AddSection("CF")
            ini.AddKey("index", ID_COMBOBOX_CF.SelectedIndex, "CF")
            For i = 0 To CF_list.Count - 1
                Dim key = "No" & (i + 1)
                Dim value = CF_list(i) & ":" & CF_num(i)
                ini.AddKey(key, value, "CF")
            Next
            ini.AddSection("name")
            ini.AddKey("No1", "Line", "name")
            ini.AddKey("No2", "Angle", "name")
            ini.AddKey("No3", "Arc", "name")
            ini.AddKey("No4", "Scale", "name")
            ini.Sort()
            ini.Save(ini_path)
        End If

        If videoDevice Is Nothing Then
        ElseIf videoDevice.IsRunning Then
            videoDevice.SignalToStop()
            RemoveHandler videoDevice.NewFrame, New NewFrameEventHandler(AddressOf Device_NewFrame)
            videoDevice = Nothing
        End If
        camera_state = False
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
                Dim obj_list = object_list.ElementAt(tab_index)
                Dim obj = obj_list.ElementAt(e.RowIndex)
                obj.name = cell.Value
                obj_list(e.RowIndex) = obj
                object_list(tab_index) = obj_list

            End If
        ElseIf e.ColumnIndex = 5 Then
            Dim cell = TryCast(ID_LISTVIEW.Rows(e.RowIndex).Cells(e.ColumnIndex), DataGridViewTextBoxCell)

            If cell IsNot Nothing AndAlso Not Equals(e.FormattedValue.ToString(), String.Empty) Then

                If ID_LISTVIEW.IsCurrentCellDirty Then
                    ID_LISTVIEW.CommitEdit(DataGridViewDataErrorContexts.Commit)
                End If

                cell.Value = e.FormattedValue
                Dim obj_list = object_list.ElementAt(tab_index)
                Dim obj = obj_list.ElementAt(e.RowIndex)
                obj.remarks = cell.Value
                obj_list(e.RowIndex) = obj
                object_list(tab_index) = obj_list

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
        CF = CF_num(index)
        ID_PICTURE_BOX(tab_index).DrawObjList(object_list.ElementAt(tab_index), graphPen, graphPen_line, digit, CF, False)
        ID_LISTVIEW.LoadObjectList(object_list.ElementAt(tab_index), CF, digit, scale_unit, name_list)
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
            side_drag = True
        Else
            side_drag = False
        End If
    End Sub

    'display setting.ini
    Private Sub ID_MENU_SETTING_INFO_Click(sender As Object, e As EventArgs) Handles ID_MENU_SETTING_INFO.Click

        Try
            Dim alive As System.Diagnostics.Process
            If System.IO.File.Exists(ini_path) = True Then
                alive = Process.Start(ini_path)
            Else
                'set default value when ini file does Not exist in document folder
                ini = New IniFile(ini_path)
                ini.AddSection("Config")
                ini.AddKey("unit", scale_unit, "Config")
                ini.AddKey("digit", digit.ToString(), "Config")

                ini.AddSection("CF")
                ini.AddKey("index", ID_COMBOBOX_CF.SelectedIndex, "CF")
                For i = 0 To CF_list.Count - 1
                    Dim key = "No" & (i + 1)
                    Dim value = CF_list(i) & ":" & CF_num(i)
                    ini.AddKey(key, value, "CF")
                Next
                ini.AddSection("name")
                ini.AddKey("No1", "Line", "name")
                ini.AddKey("No2", "Angle", "name")
                ini.AddKey("No3", "Arc", "name")
                ini.AddKey("No4", "Scale", "name")
                ini.Sort()
                ini.Save(ini_path)
                alive = Process.Start(ini_path)
            End If

            alive.WaitForExit()
            name_list.Clear()
            ID_COMBOBOX_CF.Items.Clear()
            CF_list.Clear()
            CF_num.Clear()
            GetInforFromIni()
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
                          ID_PICTURE_BOX(0).Image = newImage.Clone()
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
        camera_state = True
    End Sub

    'close camera
    Private Sub CloseCamera()

        If videoDevice Is Nothing Then
        ElseIf videoDevice.IsRunning Then
            videoDevice.SignalToStop()
            RemoveHandler videoDevice.NewFrame, New NewFrameEventHandler(AddressOf Device_NewFrame)
            videoDevice.Source = Nothing
        End If
        camera_state = False
    End Sub

    'capture image and add it to ID_LISTVIEW_IMAGE
    Private Sub ID_BTN_CAPTURE_Click(sender As Object, e As EventArgs) Handles ID_BTN_CAPTURE.Click

        Try

            Dim img1 As Image = ID_PICTURE_BOX_CAM.Image.Clone()

            Createdirectory(imagepath)
            If photoList.Images.Count <= 0 Then
                file_counter = photoList.Images.Count + 1
            Else
                file_counter = Convert.ToInt32(IO.Path.GetFileNameWithoutExtension(photoList.Images.Keys.Item(photoList.Images.Count - 1).ToString()).Split("_")(1)) + 1
            End If

            img1.Save(imagepath & "\\test_" & (file_counter) & ".jpeg", Imaging.ImageFormat.Jpeg)
            photoList.ImageSize = New Size(160, 120)
            photoList.Images.Add("\\test_" & (file_counter) & ".jpeg", img1)
            ID_LISTVIEW_IMAGE.LargeImageList = photoList
            'img1.Dispose()
            ID_LISTVIEW_IMAGE.Items.Clear()
            For index = 0 To photoList.Images.Count - 1
                Dim item As New ListViewItem With {
                    .ImageIndex = index,
                        .Tag = imagepath & photoList.Images.Keys.Item(index).ToString(),
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
        file_counter = 0
        ID_LISTVIEW_IMAGE.Clear()
        ID_LISTVIEW_IMAGE.Items.Clear()
        photoList.Images.Clear()
        ID_PICTURE_BOX_CAM.Image = Nothing
        ID_PICTURE_BOX(tab_index).Image = Nothing
        DeleteImages(imagepath)
    End Sub

    'stop camera and display the selected image to ID_PICTURE_BOX
    Private Sub ID_LISTVIEW_IMAGE_DoubleClick(sender As Object, e As EventArgs) Handles ID_LISTVIEW_IMAGE.DoubleClick
        Try
            flag = True
            ID_PICTURE_BOX(tab_index).Image = Nothing

            Dim itemSelected As Integer = GetListViewSelectedItemIndex(ID_LISTVIEW_IMAGE)
            SetListViewSelectedItem(ID_LISTVIEW_IMAGE, itemSelected)
            Dim Image As Image = Image.FromFile(ID_LISTVIEW_IMAGE.SelectedItems(0).Tag)
            ID_PICTURE_BOX_CAM.Image = Image

            ID_PICTURE_BOX(tab_index).LoadImageFromFile(ID_LISTVIEW_IMAGE.SelectedItems(0).Tag, origin_image, resized_image,
                                                         initial_ratio, tab_index)

            Dim img = resized_image.ElementAt(tab_index)

            ID_PICTURE_BOX(tab_index).Image = img.ToBitmap()

            left_top = ID_PICTURE_BOX(tab_index).CenteringImage(ID_PANEL(tab_index))

            current_image(tab_index) = img
            cur_obj_num(tab_index) = 0
            Enumerable.ElementAt(Of List(Of MeasureObject))(object_list, tab_index).Clear()
            brightness(tab_index) = 0
            contrast(tab_index) = 0
            gamma(tab_index) = 100
            img_import_flag(tab_index) = False
            ID_LISTVIEW.LoadObjectList(object_list.ElementAt(tab_index), CF, digit, scale_unit, name_list)

            ID_TAG_CTRL.SelectedTab = ID_TAG_PAGE(tab_index)
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
            imagepath = txtbx_imagepath.Text
            My.Settings.imagefilepath = imagepath
            My.Settings.Save()
            Createdirectory(imagepath)
        End If
    End Sub

    'delete all captured images
    Private Sub btn_delete_Click(sender As Object, e As EventArgs) Handles btn_delete.Click

        For Each v As ListViewItem In ID_LISTVIEW_IMAGE.SelectedItems
            ID_LISTVIEW_IMAGE.Items.Remove(v)
            photoList.Images.RemoveAt(v.ImageIndex)
            Dim FileDelete As String = v.Tag
            If File.Exists(FileDelete) = True Then
                File.Delete(FileDelete)
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
                    Createdirectory(imagepath)
                    If photoList.Images.Count <= 0 Then
                        file_counter = photoList.Images.Count + 1
                    Else
                        file_counter = Convert.ToInt32(IO.Path.GetFileNameWithoutExtension(photoList.Images.Keys.Item(photoList.Images.Count - 1).ToString()).Split("_")(1)) + 1
                    End If

                    img1.Save(imagepath & "\\test_" & (file_counter) & ".jpeg", Imaging.ImageFormat.Jpeg)
                    photoList.ImageSize = New Size(200, 150)
                    photoList.Images.Add("\\test_" & (file_counter) & ".jpeg", img1)
                    ID_LISTVIEW_IMAGE.LargeImageList = photoList
                    img1.Dispose()
                    ID_LISTVIEW_IMAGE.Items.Clear()
                    For index = 0 To photoList.Images.Count - 1
                        Dim item As New ListViewItem With {
                        .ImageIndex = index,
                            .Tag = imagepath & photoList.Images.Keys.Item(index).ToString(),
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
    'check for license
    Private Sub Init()
        licState = licGen.getLicState(licModel)
        MessageBox.Show("License State : " + licState.ToString)
        If licState = licState.Success Then
            ID_GROUP_BOX_CONTROL.Enabled = True
        Else
            ID_GROUP_BOX_CONTROL.Enabled = False
        End If
        path = Application.StartupPath + "img\image1.jpg"
    End Sub

    'show activate dialog
    Private Sub ID_MENU_ACTIVATE_Click(sender As Object, e As EventArgs) Handles ID_MENU_ACTIVATE.Click
        Dim ad As New ActiveInfo
        If ad.ShowDialog() = DialogResult.OK Then
            Dim OfDLicense As New OpenFileDialog()
            OfDLicense.Filter = "License (*.lic)|*.lic"
            OfDLicense.FilterIndex = 1
            OfDLicense.RestoreDirectory = True
            Dim dest As String = System.IO.Path.Combine(Application.StartupPath, licpath)
            If OfDLicense.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                Dim file = New FileInfo(OfDLicense.FileName)
                file = file.CopyTo(dest, True)

            End If
        End If


        Init()
    End Sub

    'show license info dialog
    Private Sub ID_MENU_LICENSE_INFO_Click(sender As Object, e As EventArgs) Handles ID_MENU_LICENSE_INFO.Click

        Dim ld As New LicInfo()
        ld.mParent = Me
        If licState = licState.NoFile Or licState = licState.Incorrect Then
            ld.bInfo = False
            ld.serial = licGen.getSn
            ld.machine = licGen.getMn

        Else
            ld.bInfo = True
            ld.serial = licModel.sn
            ld.machine = licModel.mn
            ld.customer = licModel.cname
            ld.email = licModel.cmail

        End If

        ld.ShowDialog()
    End Sub
#End Region

#Region "Curves Methods"
    Private Sub ID_MENU_TO_CURVES_Click(sender As Object, e As EventArgs) Handles ID_MENU_TO_CURVES.Click

        If System.IO.File.Exists(exe_path) = True Then
            Try
                ID_PICTURE_BOX(tab_index).SaveImageForCurves(ToCurveImg_path, object_list.ElementAt(tab_index), graphPen, graphPen_line, digit, CF)
                Dim alive = Process.Start(exe_path)

                alive.WaitForExit()
                ID_PICTURE_BOX(tab_index).LoadImageFromFile(ReturnedImg_path, origin_image, resized_image,
                                                         initial_ratio, tab_index)
                Dim img = resized_image.ElementAt(tab_index)
                ID_PICTURE_BOX(tab_index).Image = img.ToBitmap()
                left_top = ID_PICTURE_BOX(tab_index).CenteringImage(ID_PANEL(tab_index))
                current_image(tab_index) = img
                AppendDataToObjList(ReturnedTxt_path, object_list(tab_index), cur_obj_num(tab_index))
                ID_LISTVIEW.LoadObjectList(object_list.ElementAt(tab_index), CF, digit, scale_unit, name_list)

            Catch ex As Exception

                MessageBox.Show(ex.Message.ToString())
            End Try
        End If

    End Sub

#End Region


End Class
