<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ID_FORM_SCALE
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
        Me.ID_COMBO_SCALE_STYLE = New System.Windows.Forms.ComboBox()
        Me.ID_NUM_SCALE = New System.Windows.Forms.NumericUpDown()
        Me.ID_BTN_OK = New System.Windows.Forms.Button()
        Me.ID_BTN_CANCEL = New System.Windows.Forms.Button()
        Me.ID_TEXT_UNIT = New System.Windows.Forms.TextBox()
        CType(Me.ID_NUM_SCALE, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ID_COMBO_SCALE_STYLE
        '
        Me.ID_COMBO_SCALE_STYLE.FormattingEnabled = True
        Me.ID_COMBO_SCALE_STYLE.Items.AddRange(New Object() {"horizontal", "vertical"})
        Me.ID_COMBO_SCALE_STYLE.Location = New System.Drawing.Point(59, 12)
        Me.ID_COMBO_SCALE_STYLE.Name = "ID_COMBO_SCALE_STYLE"
        Me.ID_COMBO_SCALE_STYLE.Size = New System.Drawing.Size(121, 23)
        Me.ID_COMBO_SCALE_STYLE.TabIndex = 0
        '
        'ID_NUM_SCALE
        '
        Me.ID_NUM_SCALE.Location = New System.Drawing.Point(60, 41)
        Me.ID_NUM_SCALE.Maximum = New Decimal(New Integer() {100000, 0, 0, 0})
        Me.ID_NUM_SCALE.Name = "ID_NUM_SCALE"
        Me.ID_NUM_SCALE.Size = New System.Drawing.Size(77, 23)
        Me.ID_NUM_SCALE.TabIndex = 1
        '
        'ID_BTN_OK
        '
        Me.ID_BTN_OK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.ID_BTN_OK.Location = New System.Drawing.Point(111, 70)
        Me.ID_BTN_OK.Name = "ID_BTN_OK"
        Me.ID_BTN_OK.Size = New System.Drawing.Size(58, 23)
        Me.ID_BTN_OK.TabIndex = 3
        Me.ID_BTN_OK.Text = "OK"
        Me.ID_BTN_OK.UseVisualStyleBackColor = True
        '
        'ID_BTN_CANCEL
        '
        Me.ID_BTN_CANCEL.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.ID_BTN_CANCEL.Location = New System.Drawing.Point(187, 70)
        Me.ID_BTN_CANCEL.Name = "ID_BTN_CANCEL"
        Me.ID_BTN_CANCEL.Size = New System.Drawing.Size(58, 23)
        Me.ID_BTN_CANCEL.TabIndex = 4
        Me.ID_BTN_CANCEL.Text = "Cancel"
        Me.ID_BTN_CANCEL.UseVisualStyleBackColor = True
        '
        'ID_TEXT_UNIT
        '
        Me.ID_TEXT_UNIT.Location = New System.Drawing.Point(145, 41)
        Me.ID_TEXT_UNIT.Name = "ID_TEXT_UNIT"
        Me.ID_TEXT_UNIT.Size = New System.Drawing.Size(35, 23)
        Me.ID_TEXT_UNIT.TabIndex = 5
        '
        'ID_FORM_SCALE
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(251, 98)
        Me.Controls.Add(Me.ID_TEXT_UNIT)
        Me.Controls.Add(Me.ID_BTN_CANCEL)
        Me.Controls.Add(Me.ID_BTN_OK)
        Me.Controls.Add(Me.ID_NUM_SCALE)
        Me.Controls.Add(Me.ID_COMBO_SCALE_STYLE)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "ID_FORM_SCALE"
        Me.Text = "Measure Scale Dialog"
        CType(Me.ID_NUM_SCALE, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ID_COMBO_SCALE_STYLE As ComboBox
    Friend WithEvents ID_NUM_SCALE As NumericUpDown
    Friend WithEvents ID_BTN_OK As Button
    Friend WithEvents ID_BTN_CANCEL As Button
    Friend WithEvents ID_TEXT_UNIT As TextBox
End Class
