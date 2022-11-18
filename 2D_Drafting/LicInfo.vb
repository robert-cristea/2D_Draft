Imports System.IO
Public Class LicInfo

    Public customer As String
    Public email As String
    Public type As String
    Public serial As String
    Public machine As String
    Public bInfo As Boolean
    Public expdate As String
    Public mParent As Main_Form
    Public cicleen As String
    Public thrsen As String

    Public f3 As String
    Public f4 As String
    Public f5 As String
    Public f6 As String
    Public f7 As String
    Public f8 As String

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub LicInfo_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If bInfo = True Then
            txt_cname.Text = customer
            txt_cmail.Text = email
            cbx_ltype.Text = type
            'dtexp.Value = DateTime.Parse(expdate)
            dtexp.Value = DateTime.Now
            txt_sn.Text = serial
            txt_mn.Text = machine
            tehigh(lbl_fc, mParent.licModel.feats.f1)
            tehigh(lbl_ft, mParent.licModel.feats.f2)
            tehigh(lbl_f3, mParent.licModel.feats.f3)
            tehigh(lbl_f4, mParent.licModel.feats.f4)
            tehigh(lbl_f5, mParent.licModel.feats.f5)
            tehigh(lbl_f6, mParent.licModel.feats.f6)
            tehigh(lbl_f7, mParent.licModel.feats.f7)
            tehigh(lbl_f8, mParent.licModel.feats.f8)


        Else
            txt_cname.Text = ""
            txt_cmail.Text = ""
            cbx_ltype.Text = "Standard"
            dtexp.Value = DateTime.Now
            txt_sn.Text = serial
            txt_mn.Text = machine
        End If

    End Sub

    Private Sub tehigh(ByRef lbx As Label, ByVal str As String)
        If str = "True" Then
            lbx.ForeColor = Color.Green
            lbx.Text = "Actived"
        Else
            lbx.ForeColor = Color.Red
            lbx.Text = "Deactived"
        End If
    End Sub

    Private Sub chk_export_CheckedChanged(sender As Object, e As EventArgs) Handles chk_export.CheckedChanged
        updateUI()
    End Sub
    Private Sub updateUI()

        btn_export.Visible = chk_export.Checked
        chk_f1.Enabled = chk_export.Checked
        chk_f2.Enabled = chk_export.Checked
        chk_f3.Enabled = chk_export.Checked
        chk_f4.Enabled = chk_export.Checked
        chk_f5.Enabled = chk_export.Checked
        chk_f6.Enabled = chk_export.Checked
        chk_f7.Enabled = chk_export.Checked
        chk_f8.Enabled = chk_export.Checked
        txt_cmail.Enabled = chk_export.Checked
        txt_cname.Enabled = chk_export.Checked
        txt_mn.Enabled = chk_export.Checked
        txt_sn.Enabled = chk_export.Checked
        cbx_ltype.Enabled = chk_export.Checked
        dtexp.Enabled = chk_export.Checked

    End Sub

    Private Sub btn_export_Click(sender As Object, e As EventArgs) Handles btn_export.Click

        If String.IsNullOrEmpty(txt_cname.Text) = True Then
            MessageBox.Show("Input Name!")
            Return
        End If
        If String.IsNullOrEmpty(txt_cmail.Text) = True Then
            MessageBox.Show("Input Email!")
            Return
        End If
        Dim SfDLicense As New SaveFileDialog()
        SfDLicense.Filter = "Req file (*.req)|*.req"
        SfDLicense.FilterIndex = 1
        SfDLicense.RestoreDirectory = True
        If SfDLicense.ShowDialog() = DialogResult.OK Then

            Dim strreq As String = ""
            Dim lic As New LicGen
            strreq += txt_cname.Text + vbCrLf
            strreq += txt_cmail.Text + vbCrLf

            strreq += txt_sn.Text + vbCrLf
            strreq += txt_mn.Text + vbCrLf

            strreq += lic.getDriveSerialNumber + vbCrLf
            strreq += lic.getCpuId + vbCrLf
            strreq += lic.getMacAddress + vbCrLf

            Dim cryptor As DesCryptor
            cryptor = New DesCryptor("cryptedWords")
            strreq = cryptor.EncryptData(strreq)


            Dim sw As StreamWriter
            Try
                sw = New StreamWriter(SfDLicense.FileName)
                sw.Write(strreq)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            Finally
                If sw IsNot Nothing Then
                    sw.Close()
                End If
            End Try



            MessageBox.Show("The Request File has been successfully created!")
        End If

    End Sub
End Class
