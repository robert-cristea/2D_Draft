Public Class ActiveInfo

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub ActiveInfo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Dim lic As New LicenseGenerator()
        'lbl_cpu.Text = lic.getCpuId()
        'lbl_hdd.Text = lic.getDriveSerialNumber()
        'lbl_mac.Text = lic.getMacAddress()
    End Sub
End Class
