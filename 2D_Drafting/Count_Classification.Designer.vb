<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Count_Classification
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
        Me.RadioManual = New System.Windows.Forms.RadioButton()
        Me.RadioAutoBright = New System.Windows.Forms.RadioButton()
        Me.RadioAutoDark = New System.Windows.Forms.RadioButton()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.LabTotal = New System.Windows.Forms.Label()
        Me.LabMaxPer = New System.Windows.Forms.Label()
        Me.LabMinPer = New System.Windows.Forms.Label()
        Me.LabMaxArea = New System.Windows.Forms.Label()
        Me.LabMinArea = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TxtLimit = New System.Windows.Forms.TextBox()
        Me.BtnCount = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.TxtFrom1 = New System.Windows.Forms.TextBox()
        Me.TxtFrom2 = New System.Windows.Forms.TextBox()
        Me.TxtFrom3 = New System.Windows.Forms.TextBox()
        Me.TxtFrom4 = New System.Windows.Forms.TextBox()
        Me.TxtTo1 = New System.Windows.Forms.TextBox()
        Me.TxtTo2 = New System.Windows.Forms.TextBox()
        Me.TxtTo3 = New System.Windows.Forms.TextBox()
        Me.TxtTo4 = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.TxtPercent1 = New System.Windows.Forms.TextBox()
        Me.TxtPercent2 = New System.Windows.Forms.TextBox()
        Me.TxtPercent3 = New System.Windows.Forms.TextBox()
        Me.TxtPercent4 = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.RadioArea = New System.Windows.Forms.RadioButton()
        Me.RadioLength = New System.Windows.Forms.RadioButton()
        Me.BtnOK = New System.Windows.Forms.Button()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.BtnCancel = New System.Windows.Forms.Button()
        Me.BtnFont = New System.Windows.Forms.Button()
        Me.BtnReport = New System.Windows.Forms.Button()
        Me.BtnGraph = New System.Windows.Forms.Button()
        Me.BtnExcel = New System.Windows.Forms.Button()
        Me.Pic4 = New System.Windows.Forms.PictureBox()
        Me.Pic3 = New System.Windows.Forms.PictureBox()
        Me.Pic2 = New System.Windows.Forms.PictureBox()
        Me.Pic1 = New System.Windows.Forms.PictureBox()
        Me.No = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Length = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Width = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Area = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Perimeter = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Ratio = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Roundness = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FontDialog1 = New System.Windows.Forms.FontDialog()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Pic4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Pic3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Pic2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Pic1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.RadioAutoDark)
        Me.GroupBox1.Controls.Add(Me.RadioManual)
        Me.GroupBox1.Controls.Add(Me.RadioAutoBright)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(137, 110)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Intensity Range"
        '
        'RadioManual
        '
        Me.RadioManual.AutoSize = True
        Me.RadioManual.Location = New System.Drawing.Point(20, 19)
        Me.RadioManual.Name = "RadioManual"
        Me.RadioManual.Size = New System.Drawing.Size(60, 17)
        Me.RadioManual.TabIndex = 1
        Me.RadioManual.TabStop = True
        Me.RadioManual.Text = "Manual"
        Me.RadioManual.UseVisualStyleBackColor = True
        '
        'RadioAutoBright
        '
        Me.RadioAutoBright.AutoSize = True
        Me.RadioAutoBright.Location = New System.Drawing.Point(20, 51)
        Me.RadioAutoBright.Name = "RadioAutoBright"
        Me.RadioAutoBright.Size = New System.Drawing.Size(102, 17)
        Me.RadioAutoBright.TabIndex = 2
        Me.RadioAutoBright.TabStop = True
        Me.RadioAutoBright.Text = "Automatic Bright"
        Me.RadioAutoBright.UseVisualStyleBackColor = True
        '
        'RadioAutoDark
        '
        Me.RadioAutoDark.AutoSize = True
        Me.RadioAutoDark.Location = New System.Drawing.Point(20, 83)
        Me.RadioAutoDark.Name = "RadioAutoDark"
        Me.RadioAutoDark.Size = New System.Drawing.Size(98, 17)
        Me.RadioAutoDark.TabIndex = 3
        Me.RadioAutoDark.TabStop = True
        Me.RadioAutoDark.Text = "Automatic Dark"
        Me.RadioAutoDark.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.LabMinArea)
        Me.GroupBox2.Controls.Add(Me.LabMinPer)
        Me.GroupBox2.Controls.Add(Me.LabMaxArea)
        Me.GroupBox2.Controls.Add(Me.LabMaxPer)
        Me.GroupBox2.Controls.Add(Me.LabTotal)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Location = New System.Drawing.Point(155, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(193, 110)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Description"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(62, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Total Count"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 37)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(98, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Maximum Perimeter"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(16, 55)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(95, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Minimum Perimeter"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(16, 73)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(76, 13)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "Maximum Area"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(16, 91)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(73, 13)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Minimum Area"
        '
        'LabTotal
        '
        Me.LabTotal.AutoSize = True
        Me.LabTotal.Location = New System.Drawing.Point(156, 19)
        Me.LabTotal.Name = "LabTotal"
        Me.LabTotal.Size = New System.Drawing.Size(13, 13)
        Me.LabTotal.TabIndex = 7
        Me.LabTotal.Text = "0"
        '
        'LabMaxPer
        '
        Me.LabMaxPer.AutoSize = True
        Me.LabMaxPer.Location = New System.Drawing.Point(135, 37)
        Me.LabMaxPer.Name = "LabMaxPer"
        Me.LabMaxPer.Size = New System.Drawing.Size(34, 13)
        Me.LabMaxPer.TabIndex = 8
        Me.LabMaxPer.Text = "00.00"
        '
        'LabMinPer
        '
        Me.LabMinPer.AutoSize = True
        Me.LabMinPer.Location = New System.Drawing.Point(135, 55)
        Me.LabMinPer.Name = "LabMinPer"
        Me.LabMinPer.Size = New System.Drawing.Size(34, 13)
        Me.LabMinPer.TabIndex = 9
        Me.LabMinPer.Text = "00.00"
        '
        'LabMaxArea
        '
        Me.LabMaxArea.AutoSize = True
        Me.LabMaxArea.Location = New System.Drawing.Point(135, 73)
        Me.LabMaxArea.Name = "LabMaxArea"
        Me.LabMaxArea.Size = New System.Drawing.Size(34, 13)
        Me.LabMaxArea.TabIndex = 2
        Me.LabMaxArea.Text = "00.00"
        '
        'LabMinArea
        '
        Me.LabMinArea.AutoSize = True
        Me.LabMinArea.Location = New System.Drawing.Point(135, 91)
        Me.LabMinArea.Name = "LabMinArea"
        Me.LabMinArea.Size = New System.Drawing.Size(34, 13)
        Me.LabMinArea.TabIndex = 3
        Me.LabMinArea.Text = "00.00"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(29, 135)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(73, 13)
        Me.Label6.TabIndex = 2
        Me.Label6.Text = "Min Area Limit"
        '
        'TxtLimit
        '
        Me.TxtLimit.Location = New System.Drawing.Point(108, 132)
        Me.TxtLimit.Name = "TxtLimit"
        Me.TxtLimit.Size = New System.Drawing.Size(100, 20)
        Me.TxtLimit.TabIndex = 3
        '
        'BtnCount
        '
        Me.BtnCount.Location = New System.Drawing.Point(237, 130)
        Me.BtnCount.Name = "BtnCount"
        Me.BtnCount.Size = New System.Drawing.Size(62, 23)
        Me.BtnCount.TabIndex = 4
        Me.BtnCount.Text = "Count"
        Me.BtnCount.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.BtnOK)
        Me.GroupBox3.Controls.Add(Me.RadioLength)
        Me.GroupBox3.Controls.Add(Me.RadioArea)
        Me.GroupBox3.Controls.Add(Me.Label14)
        Me.GroupBox3.Controls.Add(Me.Label13)
        Me.GroupBox3.Controls.Add(Me.Label12)
        Me.GroupBox3.Controls.Add(Me.Label11)
        Me.GroupBox3.Controls.Add(Me.Pic4)
        Me.GroupBox3.Controls.Add(Me.Pic3)
        Me.GroupBox3.Controls.Add(Me.Pic2)
        Me.GroupBox3.Controls.Add(Me.Pic1)
        Me.GroupBox3.Controls.Add(Me.TxtPercent4)
        Me.GroupBox3.Controls.Add(Me.TxtPercent3)
        Me.GroupBox3.Controls.Add(Me.TxtPercent2)
        Me.GroupBox3.Controls.Add(Me.TxtPercent1)
        Me.GroupBox3.Controls.Add(Me.Label10)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Controls.Add(Me.Label8)
        Me.GroupBox3.Controls.Add(Me.Label7)
        Me.GroupBox3.Controls.Add(Me.TxtTo4)
        Me.GroupBox3.Controls.Add(Me.TxtTo3)
        Me.GroupBox3.Controls.Add(Me.TxtTo2)
        Me.GroupBox3.Controls.Add(Me.TxtTo1)
        Me.GroupBox3.Controls.Add(Me.TxtFrom4)
        Me.GroupBox3.Controls.Add(Me.TxtFrom3)
        Me.GroupBox3.Controls.Add(Me.TxtFrom2)
        Me.GroupBox3.Controls.Add(Me.TxtFrom1)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 157)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(336, 131)
        Me.GroupBox3.TabIndex = 5
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Distribution Range"
        '
        'TxtFrom1
        '
        Me.TxtFrom1.Location = New System.Drawing.Point(6, 19)
        Me.TxtFrom1.Name = "TxtFrom1"
        Me.TxtFrom1.Size = New System.Drawing.Size(56, 20)
        Me.TxtFrom1.TabIndex = 0
        '
        'TxtFrom2
        '
        Me.TxtFrom2.Location = New System.Drawing.Point(6, 45)
        Me.TxtFrom2.Name = "TxtFrom2"
        Me.TxtFrom2.Size = New System.Drawing.Size(56, 20)
        Me.TxtFrom2.TabIndex = 1
        '
        'TxtFrom3
        '
        Me.TxtFrom3.Location = New System.Drawing.Point(6, 71)
        Me.TxtFrom3.Name = "TxtFrom3"
        Me.TxtFrom3.Size = New System.Drawing.Size(56, 20)
        Me.TxtFrom3.TabIndex = 2
        '
        'TxtFrom4
        '
        Me.TxtFrom4.Location = New System.Drawing.Point(6, 97)
        Me.TxtFrom4.Name = "TxtFrom4"
        Me.TxtFrom4.Size = New System.Drawing.Size(56, 20)
        Me.TxtFrom4.TabIndex = 6
        '
        'TxtTo1
        '
        Me.TxtTo1.Location = New System.Drawing.Point(87, 19)
        Me.TxtTo1.Name = "TxtTo1"
        Me.TxtTo1.Size = New System.Drawing.Size(56, 20)
        Me.TxtTo1.TabIndex = 7
        '
        'TxtTo2
        '
        Me.TxtTo2.Location = New System.Drawing.Point(87, 45)
        Me.TxtTo2.Name = "TxtTo2"
        Me.TxtTo2.Size = New System.Drawing.Size(56, 20)
        Me.TxtTo2.TabIndex = 8
        '
        'TxtTo3
        '
        Me.TxtTo3.Location = New System.Drawing.Point(87, 71)
        Me.TxtTo3.Name = "TxtTo3"
        Me.TxtTo3.Size = New System.Drawing.Size(56, 20)
        Me.TxtTo3.TabIndex = 9
        '
        'TxtTo4
        '
        Me.TxtTo4.Location = New System.Drawing.Point(87, 97)
        Me.TxtTo4.Name = "TxtTo4"
        Me.TxtTo4.Size = New System.Drawing.Size(56, 20)
        Me.TxtTo4.TabIndex = 6
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(66, 22)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(20, 13)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "To"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(66, 48)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(20, 13)
        Me.Label8.TabIndex = 10
        Me.Label8.Text = "To"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(66, 74)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(20, 13)
        Me.Label9.TabIndex = 11
        Me.Label9.Text = "To"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(66, 100)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(20, 13)
        Me.Label10.TabIndex = 12
        Me.Label10.Text = "To"
        '
        'TxtPercent1
        '
        Me.TxtPercent1.Location = New System.Drawing.Point(149, 19)
        Me.TxtPercent1.Name = "TxtPercent1"
        Me.TxtPercent1.Size = New System.Drawing.Size(38, 20)
        Me.TxtPercent1.TabIndex = 13
        '
        'TxtPercent2
        '
        Me.TxtPercent2.Location = New System.Drawing.Point(149, 45)
        Me.TxtPercent2.Name = "TxtPercent2"
        Me.TxtPercent2.Size = New System.Drawing.Size(38, 20)
        Me.TxtPercent2.TabIndex = 14
        '
        'TxtPercent3
        '
        Me.TxtPercent3.Location = New System.Drawing.Point(149, 71)
        Me.TxtPercent3.Name = "TxtPercent3"
        Me.TxtPercent3.Size = New System.Drawing.Size(38, 20)
        Me.TxtPercent3.TabIndex = 15
        '
        'TxtPercent4
        '
        Me.TxtPercent4.Location = New System.Drawing.Point(149, 97)
        Me.TxtPercent4.Name = "TxtPercent4"
        Me.TxtPercent4.Size = New System.Drawing.Size(38, 20)
        Me.TxtPercent4.TabIndex = 16
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(216, 23)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(45, 13)
        Me.Label11.TabIndex = 21
        Me.Label11.Text = "Range1"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(216, 48)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(45, 13)
        Me.Label12.TabIndex = 22
        Me.Label12.Text = "Range2"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(216, 75)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(45, 13)
        Me.Label13.TabIndex = 23
        Me.Label13.Text = "Range3"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(216, 100)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(45, 13)
        Me.Label14.TabIndex = 24
        Me.Label14.Text = "Range4"
        '
        'RadioArea
        '
        Me.RadioArea.AutoSize = True
        Me.RadioArea.Location = New System.Drawing.Point(266, 33)
        Me.RadioArea.Name = "RadioArea"
        Me.RadioArea.Size = New System.Drawing.Size(47, 17)
        Me.RadioArea.TabIndex = 25
        Me.RadioArea.TabStop = True
        Me.RadioArea.Text = "Area"
        Me.RadioArea.UseVisualStyleBackColor = True
        '
        'RadioLength
        '
        Me.RadioLength.AutoSize = True
        Me.RadioLength.Location = New System.Drawing.Point(266, 59)
        Me.RadioLength.Name = "RadioLength"
        Me.RadioLength.Size = New System.Drawing.Size(69, 17)
        Me.RadioLength.TabIndex = 26
        Me.RadioLength.TabStop = True
        Me.RadioLength.Text = "Perimeter"
        Me.RadioLength.UseVisualStyleBackColor = True
        '
        'BtnOK
        '
        Me.BtnOK.Location = New System.Drawing.Point(267, 90)
        Me.BtnOK.Name = "BtnOK"
        Me.BtnOK.Size = New System.Drawing.Size(58, 23)
        Me.BtnOK.TabIndex = 27
        Me.BtnOK.Text = "OK"
        Me.BtnOK.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.No, Me.Length, Me.Width, Me.Area, Me.Perimeter, Me.Ratio, Me.Roundness})
        Me.DataGridView1.Location = New System.Drawing.Point(12, 294)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(336, 108)
        Me.DataGridView1.TabIndex = 6
        '
        'BtnCancel
        '
        Me.BtnCancel.Location = New System.Drawing.Point(12, 408)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(62, 23)
        Me.BtnCancel.TabIndex = 7
        Me.BtnCancel.Text = "Cancel"
        Me.BtnCancel.UseVisualStyleBackColor = True
        '
        'BtnFont
        '
        Me.BtnFont.Location = New System.Drawing.Point(80, 408)
        Me.BtnFont.Name = "BtnFont"
        Me.BtnFont.Size = New System.Drawing.Size(62, 23)
        Me.BtnFont.TabIndex = 8
        Me.BtnFont.Text = "Font"
        Me.BtnFont.UseVisualStyleBackColor = True
        '
        'BtnReport
        '
        Me.BtnReport.Location = New System.Drawing.Point(148, 408)
        Me.BtnReport.Name = "BtnReport"
        Me.BtnReport.Size = New System.Drawing.Size(62, 23)
        Me.BtnReport.TabIndex = 9
        Me.BtnReport.Text = "Report"
        Me.BtnReport.UseVisualStyleBackColor = True
        '
        'BtnGraph
        '
        Me.BtnGraph.Location = New System.Drawing.Point(216, 408)
        Me.BtnGraph.Name = "BtnGraph"
        Me.BtnGraph.Size = New System.Drawing.Size(62, 23)
        Me.BtnGraph.TabIndex = 10
        Me.BtnGraph.Text = "Graph"
        Me.BtnGraph.UseVisualStyleBackColor = True
        '
        'BtnExcel
        '
        Me.BtnExcel.Location = New System.Drawing.Point(284, 408)
        Me.BtnExcel.Name = "BtnExcel"
        Me.BtnExcel.Size = New System.Drawing.Size(62, 23)
        Me.BtnExcel.TabIndex = 11
        Me.BtnExcel.Text = "To Excel"
        Me.BtnExcel.UseVisualStyleBackColor = True
        '
        'Pic4
        '
        Me.Pic4.Location = New System.Drawing.Point(193, 100)
        Me.Pic4.Name = "Pic4"
        Me.Pic4.Size = New System.Drawing.Size(20, 15)
        Me.Pic4.TabIndex = 20
        Me.Pic4.TabStop = False
        '
        'Pic3
        '
        Me.Pic3.Location = New System.Drawing.Point(193, 73)
        Me.Pic3.Name = "Pic3"
        Me.Pic3.Size = New System.Drawing.Size(20, 15)
        Me.Pic3.TabIndex = 19
        Me.Pic3.TabStop = False
        '
        'Pic2
        '
        Me.Pic2.Location = New System.Drawing.Point(193, 48)
        Me.Pic2.Name = "Pic2"
        Me.Pic2.Size = New System.Drawing.Size(20, 15)
        Me.Pic2.TabIndex = 18
        Me.Pic2.TabStop = False
        '
        'Pic1
        '
        Me.Pic1.Location = New System.Drawing.Point(193, 21)
        Me.Pic1.Name = "Pic1"
        Me.Pic1.Size = New System.Drawing.Size(20, 15)
        Me.Pic1.TabIndex = 17
        Me.Pic1.TabStop = False
        '
        'No
        '
        Me.No.HeaderText = "No"
        Me.No.Name = "No"
        '
        'Length
        '
        Me.Length.HeaderText = "Height"
        Me.Length.Name = "Length"
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
        'Perimeter
        '
        Me.Perimeter.HeaderText = "Perimeter"
        Me.Perimeter.Name = "Perimeter"
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
        'Count_Classification
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(360, 450)
        Me.Controls.Add(Me.BtnExcel)
        Me.Controls.Add(Me.BtnGraph)
        Me.Controls.Add(Me.BtnReport)
        Me.Controls.Add(Me.BtnFont)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.BtnCount)
        Me.Controls.Add(Me.TxtLimit)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "Count_Classification"
        Me.Text = "Count and Classification"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Pic4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Pic3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Pic2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Pic1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents RadioAutoDark As RadioButton
    Friend WithEvents RadioManual As RadioButton
    Friend WithEvents RadioAutoBright As RadioButton
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents LabMinArea As Label
    Friend WithEvents LabMinPer As Label
    Friend WithEvents LabMaxArea As Label
    Friend WithEvents LabMaxPer As Label
    Friend WithEvents LabTotal As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents TxtLimit As TextBox
    Friend WithEvents BtnCount As Button
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents BtnOK As Button
    Friend WithEvents RadioLength As RadioButton
    Friend WithEvents RadioArea As RadioButton
    Friend WithEvents Label14 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Pic4 As PictureBox
    Friend WithEvents Pic3 As PictureBox
    Friend WithEvents Pic2 As PictureBox
    Friend WithEvents Pic1 As PictureBox
    Friend WithEvents TxtPercent4 As TextBox
    Friend WithEvents TxtPercent3 As TextBox
    Friend WithEvents TxtPercent2 As TextBox
    Friend WithEvents TxtPercent1 As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents TxtTo4 As TextBox
    Friend WithEvents TxtTo3 As TextBox
    Friend WithEvents TxtTo2 As TextBox
    Friend WithEvents TxtTo1 As TextBox
    Friend WithEvents TxtFrom4 As TextBox
    Friend WithEvents TxtFrom3 As TextBox
    Friend WithEvents TxtFrom2 As TextBox
    Friend WithEvents TxtFrom1 As TextBox
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents BtnCancel As Button
    Friend WithEvents BtnFont As Button
    Friend WithEvents BtnReport As Button
    Friend WithEvents BtnGraph As Button
    Friend WithEvents BtnExcel As Button
    Friend WithEvents No As DataGridViewTextBoxColumn
    Friend WithEvents Length As DataGridViewTextBoxColumn
    Friend WithEvents Width As DataGridViewTextBoxColumn
    Friend WithEvents Area As DataGridViewTextBoxColumn
    Friend WithEvents Perimeter As DataGridViewTextBoxColumn
    Friend WithEvents Ratio As DataGridViewTextBoxColumn
    Friend WithEvents Roundness As DataGridViewTextBoxColumn
    Friend WithEvents FontDialog1 As FontDialog
End Class
