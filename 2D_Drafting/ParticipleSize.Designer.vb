<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ParticipleSize
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
        Me.components = New System.ComponentModel.Container()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.RadioAutoDark = New System.Windows.Forms.RadioButton()
        Me.RadioManual = New System.Windows.Forms.RadioButton()
        Me.RadioAutoBri = New System.Windows.Forms.RadioButton()
        Me.BtnFont = New System.Windows.Forms.Button()
        Me.BtnDel = New System.Windows.Forms.Button()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.No = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Height = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Width = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Area = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Ratio = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Roundness = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.BtnExit = New System.Windows.Forms.Button()
        Me.BtnReport = New System.Windows.Forms.Button()
        Me.BtnExcel = New System.Windows.Forms.Button()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.GroupBox1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.RadioAutoDark)
        Me.GroupBox1.Controls.Add(Me.RadioManual)
        Me.GroupBox1.Controls.Add(Me.RadioAutoBri)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(224, 104)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Intensity Range Selection(First Step)"
        '
        'RadioAutoDark
        '
        Me.RadioAutoDark.AutoSize = True
        Me.RadioAutoDark.Location = New System.Drawing.Point(35, 71)
        Me.RadioAutoDark.Name = "RadioAutoDark"
        Me.RadioAutoDark.Size = New System.Drawing.Size(132, 17)
        Me.RadioAutoDark.TabIndex = 3
        Me.RadioAutoDark.TabStop = True
        Me.RadioAutoDark.Text = "Automatic Dark Object"
        Me.RadioAutoDark.UseVisualStyleBackColor = True
        '
        'RadioManual
        '
        Me.RadioManual.AutoSize = True
        Me.RadioManual.Location = New System.Drawing.Point(35, 25)
        Me.RadioManual.Name = "RadioManual"
        Me.RadioManual.Size = New System.Drawing.Size(60, 17)
        Me.RadioManual.TabIndex = 1
        Me.RadioManual.TabStop = True
        Me.RadioManual.Text = "Manual"
        Me.RadioManual.UseVisualStyleBackColor = True
        '
        'RadioAutoBri
        '
        Me.RadioAutoBri.AutoSize = True
        Me.RadioAutoBri.Location = New System.Drawing.Point(35, 48)
        Me.RadioAutoBri.Name = "RadioAutoBri"
        Me.RadioAutoBri.Size = New System.Drawing.Size(136, 17)
        Me.RadioAutoBri.TabIndex = 2
        Me.RadioAutoBri.TabStop = True
        Me.RadioAutoBri.Text = "Automatic Bright Object"
        Me.RadioAutoBri.UseVisualStyleBackColor = True
        '
        'BtnFont
        '
        Me.BtnFont.Location = New System.Drawing.Point(253, 36)
        Me.BtnFont.Name = "BtnFont"
        Me.BtnFont.Size = New System.Drawing.Size(75, 23)
        Me.BtnFont.TabIndex = 1
        Me.BtnFont.Text = "Font"
        Me.BtnFont.UseVisualStyleBackColor = True
        '
        'BtnDel
        '
        Me.BtnDel.Location = New System.Drawing.Point(253, 82)
        Me.BtnDel.Name = "BtnDel"
        Me.BtnDel.Size = New System.Drawing.Size(75, 23)
        Me.BtnDel.TabIndex = 2
        Me.BtnDel.Text = "Delete All"
        Me.BtnDel.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.No, Me.Height, Me.Width, Me.Area, Me.Ratio, Me.Roundness})
        Me.DataGridView1.Location = New System.Drawing.Point(12, 132)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(316, 137)
        Me.DataGridView1.TabIndex = 3
        '
        'No
        '
        Me.No.HeaderText = "No"
        Me.No.Name = "No"
        '
        'Height
        '
        Me.Height.HeaderText = "Height"
        Me.Height.Name = "Height"
        '
        'Width
        '
        Me.Width.HeaderText = "Width"
        Me.Width.Name = "Width"
        '
        'Area
        '
        Me.Area.HeaderText = "Area"
        Me.Area.Name = "Area"
        '
        'Ratio
        '
        Me.Ratio.HeaderText = "Asp.Ratio"
        Me.Ratio.Name = "Ratio"
        '
        'Roundness
        '
        Me.Roundness.HeaderText = "Roundness"
        Me.Roundness.Name = "Roundness"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(24, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(90, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Calibration Name:"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 275)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(316, 107)
        Me.GroupBox2.TabIndex = 5
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "GroupBox2"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(130, 82)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(117, 13)
        Me.Label8.TabIndex = 6
        Me.Label8.Text = "Y: 100/38 Micron/Pixel"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(130, 62)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(117, 13)
        Me.Label7.TabIndex = 10
        Me.Label7.Text = "X: 100/38 Micron/Pixel"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(130, 42)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(39, 13)
        Me.Label6.TabIndex = 9
        Me.Label6.Text = "Micron"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(130, 22)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(32, 13)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "100X"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(24, 82)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(65, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Along Y-axis"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(24, 62)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Along X-axis"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(24, 42)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(67, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Scale Mode:"
        '
        'BtnExit
        '
        Me.BtnExit.Location = New System.Drawing.Point(39, 406)
        Me.BtnExit.Name = "BtnExit"
        Me.BtnExit.Size = New System.Drawing.Size(75, 23)
        Me.BtnExit.TabIndex = 6
        Me.BtnExit.Text = "Exit"
        Me.BtnExit.UseVisualStyleBackColor = True
        '
        'BtnReport
        '
        Me.BtnReport.Location = New System.Drawing.Point(131, 406)
        Me.BtnReport.Name = "BtnReport"
        Me.BtnReport.Size = New System.Drawing.Size(75, 23)
        Me.BtnReport.TabIndex = 7
        Me.BtnReport.Text = "Report"
        Me.BtnReport.UseVisualStyleBackColor = True
        '
        'BtnExcel
        '
        Me.BtnExcel.Location = New System.Drawing.Point(231, 406)
        Me.BtnExcel.Name = "BtnExcel"
        Me.BtnExcel.Size = New System.Drawing.Size(75, 23)
        Me.BtnExcel.TabIndex = 8
        Me.BtnExcel.Text = "To Excel"
        Me.BtnExcel.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        '
        'ParticipleSize
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(340, 441)
        Me.Controls.Add(Me.BtnExcel)
        Me.Controls.Add(Me.BtnReport)
        Me.Controls.Add(Me.BtnExit)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.BtnDel)
        Me.Controls.Add(Me.BtnFont)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "ParticipleSize"
        Me.Text = "Participle Size"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents RadioAutoDark As RadioButton
    Friend WithEvents RadioManual As RadioButton
    Friend WithEvents RadioAutoBri As RadioButton
    Friend WithEvents BtnFont As Button
    Friend WithEvents BtnDel As Button
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents No As DataGridViewTextBoxColumn
    Friend WithEvents Height As DataGridViewTextBoxColumn
    Friend WithEvents Width As DataGridViewTextBoxColumn
    Friend WithEvents Area As DataGridViewTextBoxColumn
    Friend WithEvents Ratio As DataGridViewTextBoxColumn
    Friend WithEvents Roundness As DataGridViewTextBoxColumn
    Friend WithEvents Label1 As Label
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents BtnExit As Button
    Friend WithEvents BtnReport As Button
    Friend WithEvents BtnExcel As Button
    Friend WithEvents Timer1 As Timer
End Class
