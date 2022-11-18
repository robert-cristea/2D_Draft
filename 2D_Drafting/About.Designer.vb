<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class About
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.Lbl_About_Name = New System.Windows.Forms.Label()
        Me.Lbl_About_MultitekIN = New System.Windows.Forms.Label()
        Me.Lbl_About_TagLine = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(217, 118)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(3, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'Lbl_About_Name
        '
        Me.Lbl_About_Name.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_About_Name.ForeColor = System.Drawing.SystemColors.Desktop
        Me.Lbl_About_Name.Location = New System.Drawing.Point(32, 25)
        Me.Lbl_About_Name.Name = "Lbl_About_Name"
        Me.Lbl_About_Name.Size = New System.Drawing.Size(316, 29)
        Me.Lbl_About_Name.TabIndex = 1
        Me.Lbl_About_Name.Text = "MTRND"
        '
        'Lbl_About_MultitekIN
        '
        Me.Lbl_About_MultitekIN.AutoSize = True
        Me.Lbl_About_MultitekIN.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_About_MultitekIN.Location = New System.Drawing.Point(85, 95)
        Me.Lbl_About_MultitekIN.Name = "Lbl_About_MultitekIN"
        Me.Lbl_About_MultitekIN.Size = New System.Drawing.Size(181, 20)
        Me.Lbl_About_MultitekIN.TabIndex = 2
        Me.Lbl_About_MultitekIN.Text = "Webcam DRAFT Tool"
        '
        'Lbl_About_TagLine
        '
        Me.Lbl_About_TagLine.AutoSize = True
        Me.Lbl_About_TagLine.BackColor = System.Drawing.SystemColors.Desktop
        Me.Lbl_About_TagLine.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_About_TagLine.ForeColor = System.Drawing.SystemColors.InactiveCaption
        Me.Lbl_About_TagLine.Location = New System.Drawing.Point(69, 63)
        Me.Lbl_About_TagLine.Name = "Lbl_About_TagLine"
        Me.Lbl_About_TagLine.Size = New System.Drawing.Size(215, 16)
        Me.Lbl_About_TagLine.TabIndex = 3
        Me.Lbl_About_TagLine.Text = "Customised Drafting Solutions"
        '
        'About
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(375, 159)
        Me.Controls.Add(Me.Lbl_About_TagLine)
        Me.Controls.Add(Me.Lbl_About_MultitekIN)
        Me.Controls.Add(Me.Lbl_About_Name)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "About"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "About"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents Lbl_About_Name As Label
    Friend WithEvents Lbl_About_MultitekIN As Label
    Friend WithEvents Lbl_About_TagLine As Label
End Class
