<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LenDiameter
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
        Me.ID_TEXT_FIXED = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ID_BTN_OK = New System.Windows.Forms.Button()
        Me.ID_BTN_CANCEL = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'ID_TEXT_FIXED
        '
        Me.ID_TEXT_FIXED.Location = New System.Drawing.Point(123, 33)
        Me.ID_TEXT_FIXED.Name = "ID_TEXT_FIXED"
        Me.ID_TEXT_FIXED.Size = New System.Drawing.Size(100, 20)
        Me.ID_TEXT_FIXED.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(29, 36)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(88, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Length(Diameter)"
        '
        'ID_BTN_OK
        '
        Me.ID_BTN_OK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.ID_BTN_OK.Location = New System.Drawing.Point(81, 84)
        Me.ID_BTN_OK.Name = "ID_BTN_OK"
        Me.ID_BTN_OK.Size = New System.Drawing.Size(75, 23)
        Me.ID_BTN_OK.TabIndex = 2
        Me.ID_BTN_OK.Text = "OK"
        Me.ID_BTN_OK.UseVisualStyleBackColor = True
        '
        'ID_BTN_CANCEL
        '
        Me.ID_BTN_CANCEL.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.ID_BTN_CANCEL.Location = New System.Drawing.Point(162, 84)
        Me.ID_BTN_CANCEL.Name = "ID_BTN_CANCEL"
        Me.ID_BTN_CANCEL.Size = New System.Drawing.Size(75, 23)
        Me.ID_BTN_CANCEL.TabIndex = 3
        Me.ID_BTN_CANCEL.Text = "Cancel"
        Me.ID_BTN_CANCEL.UseVisualStyleBackColor = True
        '
        'Form3
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(249, 119)
        Me.Controls.Add(Me.ID_BTN_CANCEL)
        Me.Controls.Add(Me.ID_BTN_OK)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ID_TEXT_FIXED)
        Me.Name = "Form3"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ID_TEXT_FIXED As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents ID_BTN_OK As Button
    Friend WithEvents ID_BTN_CANCEL As Button
End Class
