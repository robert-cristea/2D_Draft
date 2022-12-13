<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RoundnessLimit
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
        Me.TrackUpper = New System.Windows.Forms.TrackBar()
        Me.TrackLower = New System.Windows.Forms.TrackBar()
        Me.LabUpper = New System.Windows.Forms.Label()
        Me.LabLower = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        CType(Me.TrackUpper, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackLower, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(22, 39)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(36, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Upper"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(22, 90)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(36, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Lower"
        '
        'TrackUpper
        '
        Me.TrackUpper.Location = New System.Drawing.Point(81, 30)
        Me.TrackUpper.Maximum = 100
        Me.TrackUpper.Name = "TrackUpper"
        Me.TrackUpper.Size = New System.Drawing.Size(155, 45)
        Me.TrackUpper.TabIndex = 2
        '
        'TrackLower
        '
        Me.TrackLower.Location = New System.Drawing.Point(81, 86)
        Me.TrackLower.Maximum = 100
        Me.TrackLower.Name = "TrackLower"
        Me.TrackLower.Size = New System.Drawing.Size(155, 45)
        Me.TrackLower.TabIndex = 3
        '
        'LabUpper
        '
        Me.LabUpper.AutoSize = True
        Me.LabUpper.Location = New System.Drawing.Point(150, 14)
        Me.LabUpper.Name = "LabUpper"
        Me.LabUpper.Size = New System.Drawing.Size(13, 13)
        Me.LabUpper.TabIndex = 4
        Me.LabUpper.Text = "0"
        '
        'LabLower
        '
        Me.LabLower.AutoSize = True
        Me.LabLower.Location = New System.Drawing.Point(150, 70)
        Me.LabLower.Name = "LabLower"
        Me.LabLower.Size = New System.Drawing.Size(13, 13)
        Me.LabLower.TabIndex = 5
        Me.LabLower.Text = "0"
        '
        'Button1
        '
        Me.Button1.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Button1.Location = New System.Drawing.Point(100, 124)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 6
        Me.Button1.Text = "OK"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button2.Location = New System.Drawing.Point(181, 124)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 7
        Me.Button2.Text = "Cancel"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'RoundnessLimit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(268, 159)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.LabLower)
        Me.Controls.Add(Me.LabUpper)
        Me.Controls.Add(Me.TrackLower)
        Me.Controls.Add(Me.TrackUpper)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "RoundnessLimit"
        Me.Text = "Roundness Limit"
        CType(Me.TrackUpper, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackLower, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents TrackUpper As TrackBar
    Friend WithEvents TrackLower As TrackBar
    Friend WithEvents LabUpper As Label
    Friend WithEvents LabLower As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
End Class
