<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Brightness
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ID_HSCROLL_BRIGHTNESS = New System.Windows.Forms.HScrollBar()
        Me.ID_HSCROLL_CONTRAST = New System.Windows.Forms.HScrollBar()
        Me.ID_HSCROLL_GAMMA = New System.Windows.Forms.HScrollBar()
        Me.ID_BTN_RESET_BRIGHTNESS = New System.Windows.Forms.Button()
        Me.ID_BTN_RESET_CONTRAST = New System.Windows.Forms.Button()
        Me.ID_BTN_RESET_GAMMA = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.ID_LABEL_BRIGHTNESS = New System.Windows.Forms.Label()
        Me.ID_LABEL_CONTRAST = New System.Windows.Forms.Label()
        Me.ID_LABEL_GAMMA = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(46, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(62, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Brightness"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(46, 98)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(49, 15)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Gamma"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(46, 62)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(52, 15)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Contrast"
        '
        'ID_HSCROLL_BRIGHTNESS
        '
        Me.ID_HSCROLL_BRIGHTNESS.LargeChange = 1
        Me.ID_HSCROLL_BRIGHTNESS.Location = New System.Drawing.Point(111, 31)
        Me.ID_HSCROLL_BRIGHTNESS.Minimum = -100
        Me.ID_HSCROLL_BRIGHTNESS.Name = "ID_HSCROLL_BRIGHTNESS"
        Me.ID_HSCROLL_BRIGHTNESS.Size = New System.Drawing.Size(139, 10)
        Me.ID_HSCROLL_BRIGHTNESS.TabIndex = 3
        '
        'ID_HSCROLL_CONTRAST
        '
        Me.ID_HSCROLL_CONTRAST.LargeChange = 1
        Me.ID_HSCROLL_CONTRAST.Location = New System.Drawing.Point(111, 66)
        Me.ID_HSCROLL_CONTRAST.Minimum = -100
        Me.ID_HSCROLL_CONTRAST.Name = "ID_HSCROLL_CONTRAST"
        Me.ID_HSCROLL_CONTRAST.Size = New System.Drawing.Size(139, 10)
        Me.ID_HSCROLL_CONTRAST.TabIndex = 4
        '
        'ID_HSCROLL_GAMMA
        '
        Me.ID_HSCROLL_GAMMA.LargeChange = 1
        Me.ID_HSCROLL_GAMMA.Location = New System.Drawing.Point(111, 101)
        Me.ID_HSCROLL_GAMMA.Maximum = 200
        Me.ID_HSCROLL_GAMMA.Minimum = 1
        Me.ID_HSCROLL_GAMMA.Name = "ID_HSCROLL_GAMMA"
        Me.ID_HSCROLL_GAMMA.Size = New System.Drawing.Size(139, 10)
        Me.ID_HSCROLL_GAMMA.TabIndex = 5
        Me.ID_HSCROLL_GAMMA.Value = 100
        '
        'ID_BTN_RESET_BRIGHTNESS
        '
        Me.ID_BTN_RESET_BRIGHTNESS.Location = New System.Drawing.Point(253, 28)
        Me.ID_BTN_RESET_BRIGHTNESS.Name = "ID_BTN_RESET_BRIGHTNESS"
        Me.ID_BTN_RESET_BRIGHTNESS.Size = New System.Drawing.Size(55, 23)
        Me.ID_BTN_RESET_BRIGHTNESS.TabIndex = 6
        Me.ID_BTN_RESET_BRIGHTNESS.Text = "Reset"
        Me.ID_BTN_RESET_BRIGHTNESS.UseVisualStyleBackColor = True
        '
        'ID_BTN_RESET_CONTRAST
        '
        Me.ID_BTN_RESET_CONTRAST.Location = New System.Drawing.Point(253, 62)
        Me.ID_BTN_RESET_CONTRAST.Name = "ID_BTN_RESET_CONTRAST"
        Me.ID_BTN_RESET_CONTRAST.Size = New System.Drawing.Size(55, 23)
        Me.ID_BTN_RESET_CONTRAST.TabIndex = 7
        Me.ID_BTN_RESET_CONTRAST.Text = "Reset"
        Me.ID_BTN_RESET_CONTRAST.UseVisualStyleBackColor = True
        '
        'ID_BTN_RESET_GAMMA
        '
        Me.ID_BTN_RESET_GAMMA.Location = New System.Drawing.Point(253, 98)
        Me.ID_BTN_RESET_GAMMA.Name = "ID_BTN_RESET_GAMMA"
        Me.ID_BTN_RESET_GAMMA.Size = New System.Drawing.Size(55, 23)
        Me.ID_BTN_RESET_GAMMA.TabIndex = 8
        Me.ID_BTN_RESET_GAMMA.Text = "Reset"
        Me.ID_BTN_RESET_GAMMA.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Button4.Location = New System.Drawing.Point(170, 136)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(75, 23)
        Me.Button4.TabIndex = 9
        Me.Button4.Text = "OK"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button5.Location = New System.Drawing.Point(267, 136)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(75, 23)
        Me.Button5.TabIndex = 10
        Me.Button5.Text = "Cancel"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'ID_LABEL_BRIGHTNESS
        '
        Me.ID_LABEL_BRIGHTNESS.AutoSize = True
        Me.ID_LABEL_BRIGHTNESS.Location = New System.Drawing.Point(173, 16)
        Me.ID_LABEL_BRIGHTNESS.Name = "ID_LABEL_BRIGHTNESS"
        Me.ID_LABEL_BRIGHTNESS.Size = New System.Drawing.Size(13, 15)
        Me.ID_LABEL_BRIGHTNESS.TabIndex = 11
        Me.ID_LABEL_BRIGHTNESS.Text = "0"
        '
        'ID_LABEL_CONTRAST
        '
        Me.ID_LABEL_CONTRAST.AutoSize = True
        Me.ID_LABEL_CONTRAST.Location = New System.Drawing.Point(173, 51)
        Me.ID_LABEL_CONTRAST.Name = "ID_LABEL_CONTRAST"
        Me.ID_LABEL_CONTRAST.Size = New System.Drawing.Size(13, 15)
        Me.ID_LABEL_CONTRAST.TabIndex = 12
        Me.ID_LABEL_CONTRAST.Text = "0"
        '
        'ID_LABEL_GAMMA
        '
        Me.ID_LABEL_GAMMA.AutoSize = True
        Me.ID_LABEL_GAMMA.Location = New System.Drawing.Point(170, 86)
        Me.ID_LABEL_GAMMA.Name = "ID_LABEL_GAMMA"
        Me.ID_LABEL_GAMMA.Size = New System.Drawing.Size(25, 15)
        Me.ID_LABEL_GAMMA.TabIndex = 13
        Me.ID_LABEL_GAMMA.Text = "100"
        '
        'ID_FORM_BRIGHTNESS
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(365, 171)
        Me.Controls.Add(Me.ID_LABEL_GAMMA)
        Me.Controls.Add(Me.ID_LABEL_CONTRAST)
        Me.Controls.Add(Me.ID_LABEL_BRIGHTNESS)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.ID_BTN_RESET_GAMMA)
        Me.Controls.Add(Me.ID_BTN_RESET_CONTRAST)
        Me.Controls.Add(Me.ID_BTN_RESET_BRIGHTNESS)
        Me.Controls.Add(Me.ID_HSCROLL_GAMMA)
        Me.Controls.Add(Me.ID_HSCROLL_CONTRAST)
        Me.Controls.Add(Me.ID_HSCROLL_BRIGHTNESS)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "ID_FORM_BRIGHTNESS"
        Me.Text = "ID_FORM_BRIGHTNESS"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents ID_HSCROLL_BRIGHTNESS As HScrollBar
    Friend WithEvents ID_HSCROLL_CONTRAST As HScrollBar
    Friend WithEvents ID_HSCROLL_GAMMA As HScrollBar
    Friend WithEvents ID_BTN_RESET_BRIGHTNESS As Button
    Friend WithEvents ID_BTN_RESET_CONTRAST As Button
    Friend WithEvents ID_BTN_RESET_GAMMA As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents Button5 As Button
    Friend WithEvents ID_LABEL_BRIGHTNESS As Label
    Friend WithEvents ID_LABEL_CONTRAST As Label
    Friend WithEvents ID_LABEL_GAMMA As Label
End Class
