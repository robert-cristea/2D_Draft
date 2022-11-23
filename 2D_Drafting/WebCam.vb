

Imports AForge.Video
Imports AForge.Video.DirectShow
Imports AxVIDEOCAPLib
''' <summary>
''' This class contains all the functions for webcam
''' </summary>
Public Module WebCam
    ''' <summary>
    ''' create directory for captured images
    ''' </summary>
    ''' <paramname="imagepath">The path of directory you are goint to create.</param>
    Public Sub Createdirectory(ByVal imagepath As String)
        If Not IO.Directory.Exists(imagepath) Then
            IO.Directory.CreateDirectory(imagepath)
        End If
    End Sub

    ''' <summary>
    ''' delete directory for captured images
    ''' </summary>
    ''' <paramname="imagepath">The path of directory you are goint to delete.</param>
    Public Sub DeleteImages(ByVal imagepath As String)
        Dim FileDelete As String = imagepath
        If System.IO.Directory.Exists(FileDelete) = True Then
            'System.IO.Directory.Delete(FileDelete, True)
        End If
    End Sub

    ''' <summary>
    ''' set the list of usable resolution 
    ''' </summary>
    ''' <paramname="videoDevice">the device is connected.</param>
    ''' <paramname="CameraResolutionsCB">the combobox whose items are the usable resolutions of videoDevice.</param>
    Public Sub SelectResolution(ByVal videoDevice As AxVIDEOCAPLib.AxVideoCap, ByRef CameraResolutionsCB As ComboBox)

        If videoDevice IsNot Nothing Then
            CameraResolutionsCB.Items.Clear()
            CameraResolutionsCB.Enabled = True
            CameraResolutionsCB.Items.Add("Choose Resolution")

            For i = 0 To videoDevice.GetVideoFormatCount - 1
                CameraResolutionsCB.Items.Add(videoDevice.GetVideoFormatName(i))
            Next

            If CameraResolutionsCB.Items.Count > 1 Then
                CameraResolutionsCB.SelectedIndex = 1
            Else
                CameraResolutionsCB.SelectedIndex = 0
            End If
        Else
            CameraResolutionsCB.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' set the list of usable video input 
    ''' </summary>
    ''' <paramname="videoDevice">the device is connected.</param>
    ''' <paramname="CameraResolutionsCB">the combobox whose items are the usable resolutions of videoDevice.</param>
    Public Sub SelectVideoInput(ByVal videoDevice As AxVIDEOCAPLib.AxVideoCap, ByRef CameraResolutionsCB As ComboBox)

        If videoDevice IsNot Nothing Then
            CameraResolutionsCB.Items.Clear()
            CameraResolutionsCB.Enabled = True
            CameraResolutionsCB.Items.Add("Choose Video")

            For i = 0 To videoDevice.GetVideoInputCount - 1
                CameraResolutionsCB.Items.Add(videoDevice.GetVideoInputName(i))
            Next

            If CameraResolutionsCB.Items.Count > 1 Then
                CameraResolutionsCB.SelectedIndex = 1
            Else
                CameraResolutionsCB.SelectedIndex = 0
            End If
        Else
            CameraResolutionsCB.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' check perticular camera from usable cameras
    ''' </summary>
    ''' <paramname="videoDevices">usable cameras</param>
    ''' <paramname="_devicename">the name of particular videoDevice.</param>
    Public Function CheckPerticularCamera(ByRef videoDevices As FilterInfoCollection, ByVal _devicename As String) As Integer

        videoDevices = New FilterInfoCollection(FilterCategory.VideoInputDevice)
        Dim list As New List(Of String)

        Dim idx As Integer = -1
        Dim i As Integer = 0
        If videoDevices.Count <> 0 Then
            For Each device As FilterInfo In videoDevices
                Dim str_data As String = (i & "_" & device.Name)
                list.Add(str_data)
                i += 1
            Next
        End If

        For index = 0 To list.Count - 1
            If list.Item(index).Contains(_devicename) And Not _devicename.Equals("Integrated Webcam") Then
                'If list.Item(index).Contains(_devicename) Then
                idx = list.Item(index).Split("_")(0)
                Exit For
            End If
        Next
        Return idx
    End Function

    ''' <summary>
    ''' get selected index
    ''' </summary>
    ''' <paramname="lv">The listview containing captured images.</param>
    Public Function GetListViewSelectedItemIndex(lv As ListView) As Integer
        Return If(lv.SelectedItems.Count > 0, lv.SelectedIndices(0), 0)
    End Function

    ''' <summary>
    ''' set the item of listview
    ''' </summary>
    ''' <paramname="lv">The listview containing captured images.</param>
    ''' <paramname="index">The index of item.</param>
    Public Sub SetListViewSelectedItem(lv As ListView, index As Integer)
        If lv.Items.Count = 0 Then Return
        lv.SelectedItems.Clear()
        If Not lv.Focused Then lv.Focus()
        lv.EnsureVisible(index)
        lv.Items(index).Selected = True
    End Sub
End Module
