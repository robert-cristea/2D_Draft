<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Circle
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
        Me.ID_LABEL_ROUND = New System.Windows.Forms.Label()
        Me.ID_LABEL_THR_SEG = New System.Windows.Forms.Label()
        Me.ID_NUM_THR_CIR = New System.Windows.Forms.NumericUpDown()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.No = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PosX = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PosY = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Size = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BtnExcel = New System.Windows.Forms.Button()
        Me.BtnReport = New System.Windows.Forms.Button()
        Me.BtnExit = New System.Windows.Forms.Button()
        Me.ID_SCROLL_ROUNDNESS = New System.Windows.Forms.TrackBar()
        Me.ID_SCROLL_THR_SEG = New System.Windows.Forms.TrackBar()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.LabCircle = New System.Windows.Forms.Label()
        Me.LabBlack = New System.Windows.Forms.Label()
        Me.LabWhite = New System.Windows.Forms.Label()
        CType(Me.ID_NUM_THR_CIR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ID_SCROLL_ROUNDNESS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ID_SCROLL_THR_SEG, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(22, 40)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(114, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Roundness/Circularity:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(22, 92)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(101, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Threshold for Circle:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(22, 144)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(117, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Threshold for Segment:"
        '
        'ID_LABEL_ROUND
        '
        Me.ID_LABEL_ROUND.AutoSize = True
        Me.ID_LABEL_ROUND.Location = New System.Drawing.Point(217, 27)
        Me.ID_LABEL_ROUND.Name = "ID_LABEL_ROUND"
        Me.ID_LABEL_ROUND.Size = New System.Drawing.Size(13, 13)
        Me.ID_LABEL_ROUND.TabIndex = 6
        Me.ID_LABEL_ROUND.Text = "0"
        '
        'ID_LABEL_THR_SEG
        '
        Me.ID_LABEL_THR_SEG.AutoSize = True
        Me.ID_LABEL_THR_SEG.Location = New System.Drawing.Point(217, 129)
        Me.ID_LABEL_THR_SEG.Name = "ID_LABEL_THR_SEG"
        Me.ID_LABEL_THR_SEG.Size = New System.Drawing.Size(13, 13)
        Me.ID_LABEL_THR_SEG.TabIndex = 8
        Me.ID_LABEL_THR_SEG.Text = "0"
        '
        'ID_NUM_THR_CIR
        '
        Me.ID_NUM_THR_CIR.Location = New System.Drawing.Point(177, 92)
        Me.ID_NUM_THR_CIR.Name = "ID_NUM_THR_CIR"
        Me.ID_NUM_THR_CIR.Size = New System.Drawing.Size(69, 20)
        Me.ID_NUM_THR_CIR.TabIndex = 9
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.No, Me.PosX, Me.PosY, Me.Size})
        Me.DataGridView1.Location = New System.Drawing.Point(12, 313)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(308, 155)
        Me.DataGridView1.TabIndex = 10
        '
        'No
        '
        Me.No.HeaderText = "No"
        Me.No.Name = "No"
        '
        'PosX
        '
        Me.PosX.HeaderText = "PosX"
        Me.PosX.Name = "PosX"
        '
        'PosY
        '
        Me.PosY.HeaderText = "PosY"
        Me.PosY.Name = "PosY"
        '
        'Size
        '
        Me.Size.HeaderText = "Size"
        Me.Size.Name = "Size"
        '
        'BtnExcel
        '
        Me.BtnExcel.Location = New System.Drawing.Point(12, 491)
        Me.BtnExcel.Name = "BtnExcel"
        Me.BtnExcel.Size = New System.Drawing.Size(75, 23)
        Me.BtnExcel.TabIndex = 11
        Me.BtnExcel.Text = "To Excel"
        Me.BtnExcel.UseVisualStyleBackColor = True
        '
        'BtnReport
        '
        Me.BtnReport.Location = New System.Drawing.Point(128, 491)
        Me.BtnReport.Name = "BtnReport"
        Me.BtnReport.Size = New System.Drawing.Size(75, 23)
        Me.BtnReport.TabIndex = 12
        Me.BtnReport.Text = "Report"
        Me.BtnReport.UseVisualStyleBackColor = True
        '
        'BtnExit
        '
        Me.BtnExit.Location = New System.Drawing.Point(244, 491)
        Me.BtnExit.Name = "BtnExit"
        Me.BtnExit.Size = New System.Drawing.Size(75, 23)
        Me.BtnExit.TabIndex = 13
        Me.BtnExit.Text = "Exit"
        Me.BtnExit.UseVisualStyleBackColor = True
        '
        'ID_SCROLL_ROUNDNESS
        '
        Me.ID_SCROLL_ROUNDNESS.Location = New System.Drawing.Point(142, 40)
        Me.ID_SCROLL_ROUNDNESS.Maximum = 100
        Me.ID_SCROLL_ROUNDNESS.Name = "ID_SCROLL_ROUNDNESS"
        Me.ID_SCROLL_ROUNDNESS.Size = New System.Drawing.Size(157, 45)
        Me.ID_SCROLL_ROUNDNESS.TabIndex = 14
        '
        'ID_SCROLL_THR_SEG
        '
        Me.ID_SCROLL_THR_SEG.Location = New System.Drawing.Point(145, 144)
        Me.ID_SCROLL_THR_SEG.Maximum = 255
        Me.ID_SCROLL_THR_SEG.Name = "ID_SCROLL_THR_SEG"
        Me.ID_SCROLL_THR_SEG.Size = New System.Drawing.Size(154, 45)
        Me.ID_SCROLL_THR_SEG.TabIndex = 15
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.LabWhite)
        Me.GroupBox1.Controls.Add(Me.LabBlack)
        Me.GroupBox1.Controls.Add(Me.LabCircle)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 175)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(307, 132)
        Me.GroupBox1.TabIndex = 16
        Me.GroupBox1.TabStop = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(51, 26)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(61, 13)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Circle Area:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(51, 61)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(62, 13)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "Black Area:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(51, 96)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(63, 13)
        Me.Label6.TabIndex = 2
        Me.Label6.Text = "White Area:"
        '
        'LabCircle
        '
        Me.LabCircle.AutoSize = True
        Me.LabCircle.Location = New System.Drawing.Point(152, 26)
        Me.LabCircle.Name = "LabCircle"
        Me.LabCircle.Size = New System.Drawing.Size(0, 13)
        Me.LabCircle.TabIndex = 3
        '
        'LabBlack
        '
        Me.LabBlack.AutoSize = True
        Me.LabBlack.Location = New System.Drawing.Point(152, 61)
        Me.LabBlack.Name = "LabBlack"
        Me.LabBlack.Size = New System.Drawing.Size(0, 13)
        Me.LabBlack.TabIndex = 4
        '
        'LabWhite
        '
        Me.LabWhite.AutoSize = True
        Me.LabWhite.Location = New System.Drawing.Point(152, 96)
        Me.LabWhite.Name = "LabWhite"
        Me.LabWhite.Size = New System.Drawing.Size(0, 13)
        Me.LabWhite.TabIndex = 5
        '
        'Circle
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(332, 536)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.ID_SCROLL_THR_SEG)
        Me.Controls.Add(Me.ID_SCROLL_ROUNDNESS)
        Me.Controls.Add(Me.BtnExit)
        Me.Controls.Add(Me.BtnReport)
        Me.Controls.Add(Me.BtnExcel)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.ID_NUM_THR_CIR)
        Me.Controls.Add(Me.ID_LABEL_THR_SEG)
        Me.Controls.Add(Me.ID_LABEL_ROUND)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "Circle"
        Me.Text = "Circle"
        CType(Me.ID_NUM_THR_CIR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ID_SCROLL_ROUNDNESS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ID_SCROLL_THR_SEG, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents ID_LABEL_ROUND As Label
    Friend WithEvents ID_LABEL_THR_SEG As Label
    Friend WithEvents ID_NUM_THR_CIR As NumericUpDown
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents No As DataGridViewTextBoxColumn
    Friend WithEvents PosX As DataGridViewTextBoxColumn
    Friend WithEvents PosY As DataGridViewTextBoxColumn
    Friend WithEvents Size As DataGridViewTextBoxColumn
    Friend WithEvents BtnExcel As Button
    Friend WithEvents BtnReport As Button
    Friend WithEvents BtnExit As Button
    Friend WithEvents ID_SCROLL_ROUNDNESS As TrackBar
    Friend WithEvents ID_SCROLL_THR_SEG As TrackBar
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents LabWhite As Label
    Friend WithEvents LabBlack As Label
    Friend WithEvents LabCircle As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
End Class
