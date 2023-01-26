<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Resize
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.RadioPercent = New System.Windows.Forms.RadioButton()
        Me.RadioPixel = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.NumHor = New System.Windows.Forms.NumericUpDown()
        Me.NumVer = New System.Windows.Forms.NumericUpDown()
        Me.CheckBox = New System.Windows.Forms.CheckBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        CType(Me.NumHor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumVer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.RadioPixel)
        Me.GroupBox1.Controls.Add(Me.RadioPercent)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(217, 40)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'RadioPercent
        '
        Me.RadioPercent.AutoSize = True
        Me.RadioPercent.Location = New System.Drawing.Point(40, 14)
        Me.RadioPercent.Name = "RadioPercent"
        Me.RadioPercent.Size = New System.Drawing.Size(80, 17)
        Me.RadioPercent.TabIndex = 0
        Me.RadioPercent.TabStop = True
        Me.RadioPercent.Text = "Percentage"
        Me.RadioPercent.UseVisualStyleBackColor = True
        '
        'RadioPixel
        '
        Me.RadioPixel.AutoSize = True
        Me.RadioPixel.Location = New System.Drawing.Point(144, 14)
        Me.RadioPixel.Name = "RadioPixel"
        Me.RadioPixel.Size = New System.Drawing.Size(52, 17)
        Me.RadioPixel.TabIndex = 1
        Me.RadioPixel.TabStop = True
        Me.RadioPixel.Text = "Pixels"
        Me.RadioPixel.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(33, 73)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(57, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Horizontal:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(33, 104)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(45, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Vertical:"
        '
        'NumHor
        '
        Me.NumHor.Location = New System.Drawing.Point(142, 71)
        Me.NumHor.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.NumHor.Name = "NumHor"
        Me.NumHor.Size = New System.Drawing.Size(57, 20)
        Me.NumHor.TabIndex = 3
        '
        'NumVer
        '
        Me.NumVer.Location = New System.Drawing.Point(142, 102)
        Me.NumVer.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.NumVer.Name = "NumVer"
        Me.NumVer.Size = New System.Drawing.Size(57, 20)
        Me.NumVer.TabIndex = 4
        '
        'CheckBox
        '
        Me.CheckBox.AutoSize = True
        Me.CheckBox.Location = New System.Drawing.Point(36, 130)
        Me.CheckBox.Name = "CheckBox"
        Me.CheckBox.Size = New System.Drawing.Size(130, 17)
        Me.CheckBox.TabIndex = 5
        Me.CheckBox.Text = "Maintain Aspect Ratio"
        Me.CheckBox.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Button1.Location = New System.Drawing.Point(79, 158)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 6
        Me.Button1.Text = "OK"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button2.Location = New System.Drawing.Point(160, 158)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 7
        Me.Button2.Text = "Cancel"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(11, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(22, 13)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "By:"
        '
        'Resize
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(241, 190)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.CheckBox)
        Me.Controls.Add(Me.NumVer)
        Me.Controls.Add(Me.NumHor)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "Resize"
        Me.Text = "Resize"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.NumHor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumVer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label3 As Label
    Friend WithEvents RadioPixel As RadioButton
    Friend WithEvents RadioPercent As RadioButton
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents NumHor As NumericUpDown
    Friend WithEvents NumVer As NumericUpDown
    Friend WithEvents CheckBox As CheckBox
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
End Class
