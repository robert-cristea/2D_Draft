Imports System.IO
Imports System.Management
Imports System.Net.NetworkInformation
Imports System.Security.Cryptography
Imports Newtonsoft.Json

Public Class licensInfoModel
    Public Property hdd As String
    Public Property cpu As String
    Public Property mac As String
    Public Property sn As String
    Public Property mn As String
    Public Property cname As String
    Public Property cmail As String
    Public Property expdate As String
    Public Property ltype As String
    Public Property feats As New featuresInfo()
End Class
Public Class featuresInfo
    Public Property f1 As String
    Public Property f2 As String
    Public Property f3 As String
    Public Property f4 As String
    Public Property f5 As String
    Public Property f6 As String
    Public Property f7 As String
    Public Property f8 As String
End Class

Public Class DesCryptor
    Private TripleDes As New TripleDESCryptoServiceProvider
    Private Function TruncateHash(ByVal key As String, ByVal length As Integer) As Byte()
        Dim sha1 As New SHA1CryptoServiceProvider
        ' Hash the key.
        Dim keyBytes() As Byte =
            System.Text.Encoding.Unicode.GetBytes(key)
        Dim hash() As Byte = sha1.ComputeHash(keyBytes)
        ' Truncate or pad the hash.
        ReDim Preserve hash(length - 1)
        Return hash
    End Function
    Sub New(ByVal key As String)
        ' Initialize the crypto provider.
        TripleDes.Key = TruncateHash(key, TripleDes.KeySize \ 8)
        TripleDes.IV = TruncateHash("", TripleDes.BlockSize \ 8)
    End Sub
    Public Function EncryptData(ByVal plaintext As String) As String

        ' Convert the plaintext string to a byte array.
        Dim plaintextBytes() As Byte =
            System.Text.Encoding.Unicode.GetBytes(plaintext)

        ' Create the stream.
        Dim ms As New System.IO.MemoryStream
        ' Create the encoder to write to the stream.
        Dim encStream As New CryptoStream(ms,
            TripleDes.CreateEncryptor(),
            System.Security.Cryptography.CryptoStreamMode.Write)

        ' Use the crypto stream to write the byte array to the stream.
        encStream.Write(plaintextBytes, 0, plaintextBytes.Length)
        encStream.FlushFinalBlock()

        ' Convert the encrypted stream to a printable string.
        Return Convert.ToBase64String(ms.ToArray)
    End Function
    Public Function DecryptData(ByVal encryptedtext As String) As String

        ' Convert the encrypted text string to a byte array.
        Dim encryptedBytes() As Byte = Convert.FromBase64String(encryptedtext)

        ' Create the stream.
        Dim ms As New System.IO.MemoryStream
        ' Create the decoder to write to the stream.
        Dim decStream As New CryptoStream(ms,
            TripleDes.CreateDecryptor(),
            System.Security.Cryptography.CryptoStreamMode.Write)

        ' Use the crypto stream to write the byte array to the stream.
        decStream.Write(encryptedBytes, 0, encryptedBytes.Length)
        decStream.FlushFinalBlock()

        ' Convert the plaintext stream to a string.
        Return System.Text.Encoding.Unicode.GetString(ms.ToArray)
    End Function
End Class


Public Enum licState
    NoFile
    Expired
    Trial
    Success
    Incorrect
End Enum
Public Class LicGen
    Dim cha2rep() As String = {"A", "B", "C", "D", "E", "F", "G", "H", "J", "K"}
    Dim licpath As String = "active.lic"
    Public lModel As New licensInfoModel
    Private cryptor As DesCryptor
    Sub New()
        cryptor = New DesCryptor("sdhjgbvfgfhguilzox")
        lModel.mac = getMacAddress()
        lModel.hdd = getDriveSerialNumber()
        lModel.cpu = getCpuId()
        lModel.sn = getSn()
        lModel.mn = getMn()
    End Sub
    Sub New(ByVal mac As String, ByVal hdd As String, ByVal cpu As String, ByVal ids As String, ByVal idm As String)
        cryptor = New DesCryptor("sdhjgbvfgfhguilzox")
        lModel.mac = mac
        lModel.hdd = hdd
        lModel.cpu = cpu
        lModel.sn = ids
        lModel.mn = idm
    End Sub
    Public Sub createLicFile()
        Dim str As String = JsonConvert.SerializeObject(lModel)
        str = cryptor.EncryptData(str)
        Dim SfDLicense As New SaveFileDialog()

        SfDLicense.Filter = "License (*.lic)|*.lic"
        SfDLicense.FilterIndex = 1
        SfDLicense.RestoreDirectory = True

        If SfDLicense.ShowDialog() = DialogResult.OK Then
            Dim sw As StreamWriter = New StreamWriter(SfDLicense.FileName)
            sw.Write(str)
            sw.Close()
            MessageBox.Show("The license file has been successfully created!")
        End If
    End Sub

    Public Function getLicState(ByRef lmodel As licensInfoModel) As licState
        If File.Exists(licpath) Then
            Dim sr As StreamReader = New StreamReader(licpath)
            Dim str As String = sr.ReadToEnd()
            str = cryptor.DecryptData(str)
            Try
                lmodel = JsonConvert.DeserializeObject(Of licensInfoModel)(str)
            Catch ex As Exception
                MessageBox.Show("LicenseFile format incorrect !")
                Return licState.Incorrect
            End Try


            If DateTime.Parse(lmodel.expdate) > DateTime.Now Then
                'If lmodel.ltype = "Trial" Then
                '    Return licState.Trial
                'Else
                '    Return licState.Success
                'End If
                If lmodel.hdd = getDriveSerialNumber() AndAlso lmodel.cpu = getCpuId() AndAlso lmodel.mac = getMacAddress() Then
                    Return licState.Success
                Else
                    Return licState.Incorrect
                End If
            Else
                    Return licState.Expired
            End If
            sr.Close()
        Else
            Return licState.NoFile
        End If
        Return licState.Success
    End Function
    Public Shared Function GetComputerID() As Long
        Dim objMOS As New ManagementObjectSearcher("Select * From Win32_Processor")
        For Each objMO As Management.ManagementObject In objMOS.Get
            GetComputerID += objMO("ProcessorID").GetHashCode
        Next
        GetComputerID += My.Computer.Name.GetHashCode
    End Function

    Public Function getSn() As String
        Dim uid As Long = GetComputerID()
        Dim str As String = "0" + uid.ToString
        str = str.Substring(0, 5)
        Return str
    End Function
    Public Function getMn() As String
        Dim uid As Long = GetComputerID()
        Dim i As Integer
        Dim rts As String = ""
        Dim str As String = "0" + uid.ToString
        str = str.Substring(5, 5)
        For i = 0 To 4
            rts = rts + cha2rep(Int16.Parse(str.Substring(i, 1)))
        Next

        Return rts

    End Function
    Public Function getMacAddress() As String
        Try
            Dim adapters As NetworkInterface() = NetworkInterface.GetAllNetworkInterfaces()
            Dim adapter As NetworkInterface
            Dim myMac As String = String.Empty

            For Each adapter In adapters
                Select Case adapter.NetworkInterfaceType
                'Exclude Tunnels, Loopbacks and PPP
                    Case NetworkInterfaceType.Tunnel, NetworkInterfaceType.Loopback, NetworkInterfaceType.Ppp
                    Case Else
                        If Not adapter.GetPhysicalAddress.ToString = String.Empty And Not adapter.GetPhysicalAddress.ToString = "00000000000000E0" Then
                            myMac = adapter.GetPhysicalAddress.ToString
                            Exit For ' Got a mac so exit for
                        End If

                End Select
            Next adapter

            Return myMac
        Catch ex As Exception
            Return String.Empty
        End Try
    End Function

    Public Function getDriveSerialNumber() As String

        Dim DriveSerial As Long
        Dim fso As Object, Drv As Object
        'Create a FileSystemObject object
        fso = CreateObject("Scripting.FileSystemObject")

        Drv = fso.GetDrive(fso.GetDriveName("D:\"))

        With Drv
            If .IsReady Then
                DriveSerial = .SerialNumber
            Else    '"Drive Not Ready!"
                DriveSerial = -1
            End If
        End With

        'Clean up
        Drv = Nothing
        fso = Nothing
        getDriveSerialNumber = Hex(DriveSerial)
    End Function
    Public Function getCpuId() As String
        Dim computer As String = "."
        Dim wmi As Object = GetObject("winmgmts:" &
        "{impersonationLevel=impersonate}!\\" &
        computer & "\root\cimv2")
        Dim processors As Object = wmi.ExecQuery("Select * from " &
        "Win32_Processor")

        Dim cpu_ids As String = ""
        For Each cpu As Object In processors
            cpu_ids = cpu_ids & ", " & cpu.ProcessorId
        Next cpu
        If cpu_ids.Length > 0 Then cpu_ids =
        cpu_ids.Substring(2)

        Return cpu_ids
    End Function
End Class
