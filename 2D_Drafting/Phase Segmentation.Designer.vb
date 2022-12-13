<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Phase_Segmentation
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
        Me.RadioAll = New System.Windows.Forms.RadioButton()
        Me.RadioSelectedPart = New System.Windows.Forms.RadioButton()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.BtnNew = New System.Windows.Forms.Button()
        Me.BtnDel = New System.Windows.Forms.Button()
        Me.BtnCol = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.NumMin = New System.Windows.Forms.NumericUpDown()
        Me.NumMax = New System.Windows.Forms.NumericUpDown()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.RadioPreAll = New System.Windows.Forms.RadioButton()
        Me.RadioNone = New System.Windows.Forms.RadioButton()
        Me.RadioCurrent = New System.Windows.Forms.RadioButton()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Item = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Area = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AreaPer = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.BtnSelPha = New System.Windows.Forms.Button()
        Me.BtnOriImg = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.BtnGraph = New System.Windows.Forms.Button()
        Me.BtnReport = New System.Windows.Forms.Button()
        Me.BtnExcel = New System.Windows.Forms.Button()
        Me.BtnExit = New System.Windows.Forms.Button()
        Me.ColorDialog1 = New System.Windows.Forms.ColorDialog()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.PicBoxProgress = New System.Windows.Forms.PictureBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.HistogramBox1 = New Emgu.CV.UI.HistogramBox()
        Me.GroupBox1.SuspendLayout()
        CType(Me.NumMin, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumMax, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        CType(Me.PicBoxProgress, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.HistogramBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.RadioAll)
        Me.GroupBox1.Controls.Add(Me.RadioSelectedPart)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(336, 39)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'RadioAll
        '
        Me.RadioAll.AutoSize = True
        Me.RadioAll.Location = New System.Drawing.Point(38, 16)
        Me.RadioAll.Name = "RadioAll"
        Me.RadioAll.Size = New System.Drawing.Size(44, 17)
        Me.RadioAll.TabIndex = 1
        Me.RadioAll.TabStop = True
        Me.RadioAll.Text = "ALL"
        Me.RadioAll.UseVisualStyleBackColor = True
        '
        'RadioSelectedPart
        '
        Me.RadioSelectedPart.AutoSize = True
        Me.RadioSelectedPart.Location = New System.Drawing.Point(196, 16)
        Me.RadioSelectedPart.Name = "RadioSelectedPart"
        Me.RadioSelectedPart.Size = New System.Drawing.Size(113, 17)
        Me.RadioSelectedPart.TabIndex = 0
        Me.RadioSelectedPart.TabStop = True
        Me.RadioSelectedPart.Text = "SELECTED PART"
        Me.RadioSelectedPart.UseVisualStyleBackColor = True
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(12, 57)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(101, 21)
        Me.ComboBox1.TabIndex = 1
        '
        'BtnNew
        '
        Me.BtnNew.Location = New System.Drawing.Point(119, 57)
        Me.BtnNew.Name = "BtnNew"
        Me.BtnNew.Size = New System.Drawing.Size(61, 23)
        Me.BtnNew.TabIndex = 2
        Me.BtnNew.Text = "New"
        Me.BtnNew.UseVisualStyleBackColor = True
        '
        'BtnDel
        '
        Me.BtnDel.Location = New System.Drawing.Point(186, 57)
        Me.BtnDel.Name = "BtnDel"
        Me.BtnDel.Size = New System.Drawing.Size(61, 23)
        Me.BtnDel.TabIndex = 3
        Me.BtnDel.Text = "Del"
        Me.BtnDel.UseVisualStyleBackColor = True
        '
        'BtnCol
        '
        Me.BtnCol.BackColor = System.Drawing.Color.White
        Me.BtnCol.Location = New System.Drawing.Point(320, 57)
        Me.BtnCol.Name = "BtnCol"
        Me.BtnCol.Size = New System.Drawing.Size(28, 23)
        Me.BtnCol.TabIndex = 5
        Me.BtnCol.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(25, 308)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 13)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Min.Range"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(205, 308)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(62, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Max.Range"
        '
        'NumMin
        '
        Me.NumMin.Location = New System.Drawing.Point(90, 304)
        Me.NumMin.Maximum = New Decimal(New Integer() {256, 0, 0, 0})
        Me.NumMin.Name = "NumMin"
        Me.NumMin.Size = New System.Drawing.Size(71, 20)
        Me.NumMin.TabIndex = 9
        '
        'NumMax
        '
        Me.NumMax.Location = New System.Drawing.Point(277, 304)
        Me.NumMax.Maximum = New Decimal(New Integer() {256, 0, 0, 0})
        Me.NumMax.Name = "NumMax"
        Me.NumMax.Size = New System.Drawing.Size(71, 20)
        Me.NumMax.TabIndex = 10
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.RadioPreAll)
        Me.GroupBox2.Controls.Add(Me.RadioNone)
        Me.GroupBox2.Controls.Add(Me.RadioCurrent)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 330)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(335, 45)
        Me.GroupBox2.TabIndex = 11
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Preview"
        '
        'RadioPreAll
        '
        Me.RadioPreAll.AutoSize = True
        Me.RadioPreAll.Location = New System.Drawing.Point(284, 19)
        Me.RadioPreAll.Name = "RadioPreAll"
        Me.RadioPreAll.Size = New System.Drawing.Size(36, 17)
        Me.RadioPreAll.TabIndex = 14
        Me.RadioPreAll.TabStop = True
        Me.RadioPreAll.Text = "All"
        Me.RadioPreAll.UseVisualStyleBackColor = True
        '
        'RadioNone
        '
        Me.RadioNone.AutoSize = True
        Me.RadioNone.Location = New System.Drawing.Point(16, 19)
        Me.RadioNone.Name = "RadioNone"
        Me.RadioNone.Size = New System.Drawing.Size(51, 17)
        Me.RadioNone.TabIndex = 12
        Me.RadioNone.TabStop = True
        Me.RadioNone.Text = "None"
        Me.RadioNone.UseVisualStyleBackColor = True
        '
        'RadioCurrent
        '
        Me.RadioCurrent.AutoSize = True
        Me.RadioCurrent.Location = New System.Drawing.Point(146, 19)
        Me.RadioCurrent.Name = "RadioCurrent"
        Me.RadioCurrent.Size = New System.Drawing.Size(59, 17)
        Me.RadioCurrent.TabIndex = 13
        Me.RadioCurrent.TabStop = True
        Me.RadioCurrent.Text = "Current"
        Me.RadioCurrent.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Item, Me.Area, Me.AreaPer})
        Me.DataGridView1.Location = New System.Drawing.Point(13, 381)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(336, 97)
        Me.DataGridView1.TabIndex = 12
        '
        'Item
        '
        Me.Item.HeaderText = "Column1"
        Me.Item.Name = "Item"
        '
        'Area
        '
        Me.Area.HeaderText = "Area"
        Me.Area.Name = "Area"
        '
        'AreaPer
        '
        Me.AreaPer.HeaderText = "Area(%)"
        Me.AreaPer.Name = "AreaPer"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.BtnSelPha)
        Me.GroupBox3.Controls.Add(Me.BtnOriImg)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Location = New System.Drawing.Point(13, 484)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(335, 96)
        Me.GroupBox3.TabIndex = 13
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Selected Phase"
        '
        'BtnSelPha
        '
        Me.BtnSelPha.Location = New System.Drawing.Point(224, 67)
        Me.BtnSelPha.Name = "BtnSelPha"
        Me.BtnSelPha.Size = New System.Drawing.Size(96, 23)
        Me.BtnSelPha.TabIndex = 4
        Me.BtnSelPha.Text = "Selected Phase"
        Me.BtnSelPha.UseVisualStyleBackColor = True
        '
        'BtnOriImg
        '
        Me.BtnOriImg.Location = New System.Drawing.Point(113, 67)
        Me.BtnOriImg.Name = "BtnOriImg"
        Me.BtnOriImg.Size = New System.Drawing.Size(96, 23)
        Me.BtnOriImg.TabIndex = 3
        Me.BtnOriImg.Text = "Original Image"
        Me.BtnOriImg.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 46)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(191, 13)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "(WARNING:Select All OPTION FIRST)"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 31)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(170, 13)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "and press Selected Phase button. "
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(328, 13)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Please click on Image to select the pixel value for phase distribution "
        '
        'BtnGraph
        '
        Me.BtnGraph.Location = New System.Drawing.Point(12, 603)
        Me.BtnGraph.Name = "BtnGraph"
        Me.BtnGraph.Size = New System.Drawing.Size(75, 23)
        Me.BtnGraph.TabIndex = 14
        Me.BtnGraph.Text = "Graph"
        Me.BtnGraph.UseVisualStyleBackColor = True
        '
        'BtnReport
        '
        Me.BtnReport.Location = New System.Drawing.Point(98, 603)
        Me.BtnReport.Name = "BtnReport"
        Me.BtnReport.Size = New System.Drawing.Size(75, 23)
        Me.BtnReport.TabIndex = 15
        Me.BtnReport.Text = "Report"
        Me.BtnReport.UseVisualStyleBackColor = True
        '
        'BtnExcel
        '
        Me.BtnExcel.Location = New System.Drawing.Point(184, 603)
        Me.BtnExcel.Name = "BtnExcel"
        Me.BtnExcel.Size = New System.Drawing.Size(75, 23)
        Me.BtnExcel.TabIndex = 16
        Me.BtnExcel.Text = "To Excel"
        Me.BtnExcel.UseVisualStyleBackColor = True
        '
        'BtnExit
        '
        Me.BtnExit.Location = New System.Drawing.Point(270, 603)
        Me.BtnExit.Name = "BtnExit"
        Me.BtnExit.Size = New System.Drawing.Size(75, 23)
        Me.BtnExit.TabIndex = 17
        Me.BtnExit.Text = "Exit"
        Me.BtnExit.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        '
        'PicBoxProgress
        '
        Me.PicBoxProgress.BackColor = System.Drawing.Color.White
        Me.PicBoxProgress.Location = New System.Drawing.Point(12, 94)
        Me.PicBoxProgress.Name = "PicBoxProgress"
        Me.PicBoxProgress.Size = New System.Drawing.Size(336, 21)
        Me.PicBoxProgress.TabIndex = 18
        Me.PicBoxProgress.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Gray
        Me.PictureBox1.BackgroundImage = Global._2D_Drafting.My.Resources.Resources.GrayGradient
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox1.Location = New System.Drawing.Point(13, 265)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(335, 19)
        Me.PictureBox1.TabIndex = 6
        Me.PictureBox1.TabStop = False
        '
        'HistogramBox1
        '
        Me.HistogramBox1.Enabled = False
        Me.HistogramBox1.Location = New System.Drawing.Point(12, 121)
        Me.HistogramBox1.Name = "HistogramBox1"
        Me.HistogramBox1.Size = New System.Drawing.Size(336, 138)
        Me.HistogramBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.HistogramBox1.TabIndex = 2
        Me.HistogramBox1.TabStop = False
        '
        'Phase_Segmentation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(362, 638)
        Me.Controls.Add(Me.PicBoxProgress)
        Me.Controls.Add(Me.BtnExit)
        Me.Controls.Add(Me.BtnExcel)
        Me.Controls.Add(Me.BtnReport)
        Me.Controls.Add(Me.BtnGraph)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.NumMax)
        Me.Controls.Add(Me.NumMin)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.HistogramBox1)
        Me.Controls.Add(Me.BtnCol)
        Me.Controls.Add(Me.BtnDel)
        Me.Controls.Add(Me.BtnNew)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "Phase_Segmentation"
        Me.Text = "Phase Segmentation"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.NumMin, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumMax, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.PicBoxProgress, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.HistogramBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents RadioAll As RadioButton
    Friend WithEvents RadioSelectedPart As RadioButton
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents BtnNew As Button
    Friend WithEvents BtnDel As Button
    Friend WithEvents BtnCol As Button
    Friend WithEvents HistogramBox1 As Emgu.CV.UI.HistogramBox
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents NumMin As NumericUpDown
    Friend WithEvents NumMax As NumericUpDown
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents RadioPreAll As RadioButton
    Friend WithEvents RadioNone As RadioButton
    Friend WithEvents RadioCurrent As RadioButton
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents BtnSelPha As Button
    Friend WithEvents BtnOriImg As Button
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents BtnGraph As Button
    Friend WithEvents BtnReport As Button
    Friend WithEvents BtnExcel As Button
    Friend WithEvents BtnExit As Button
    Friend WithEvents PicBoxProgress As PictureBox
    Friend WithEvents Item As DataGridViewTextBoxColumn
    Friend WithEvents Area As DataGridViewTextBoxColumn
    Friend WithEvents AreaPer As DataGridViewTextBoxColumn
    Friend WithEvents ColorDialog1 As ColorDialog
    Friend WithEvents Timer1 As Timer
End Class
