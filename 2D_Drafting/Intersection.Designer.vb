<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Intersection
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
        Me.ID_NUM_HORIZON = New System.Windows.Forms.NumericUpDown()
        Me.ID_NUM_VERTICAL = New System.Windows.Forms.NumericUpDown()
        Me.ID_BTN_EDGE = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ID_LABEL_THR_SEG = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.No = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.HorCnt = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.VerLine = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BtnExcel = New System.Windows.Forms.Button()
        Me.BtnReport = New System.Windows.Forms.Button()
        Me.BtnExit = New System.Windows.Forms.Button()
        Me.BtnCount = New System.Windows.Forms.Button()
        Me.ID_SCROLL_THR_SEG = New System.Windows.Forms.TrackBar()
        CType(Me.ID_NUM_HORIZON, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ID_NUM_VERTICAL, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ID_SCROLL_THR_SEG, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ID_NUM_HORIZON
        '
        Me.ID_NUM_HORIZON.Location = New System.Drawing.Point(170, 84)
        Me.ID_NUM_HORIZON.Name = "ID_NUM_HORIZON"
        Me.ID_NUM_HORIZON.Size = New System.Drawing.Size(71, 20)
        Me.ID_NUM_HORIZON.TabIndex = 1
        '
        'ID_NUM_VERTICAL
        '
        Me.ID_NUM_VERTICAL.Location = New System.Drawing.Point(170, 134)
        Me.ID_NUM_VERTICAL.Name = "ID_NUM_VERTICAL"
        Me.ID_NUM_VERTICAL.Size = New System.Drawing.Size(71, 20)
        Me.ID_NUM_VERTICAL.TabIndex = 2
        '
        'ID_BTN_EDGE
        '
        Me.ID_BTN_EDGE.Location = New System.Drawing.Point(328, 30)
        Me.ID_BTN_EDGE.Name = "ID_BTN_EDGE"
        Me.ID_BTN_EDGE.Size = New System.Drawing.Size(75, 23)
        Me.ID_BTN_EDGE.TabIndex = 3
        Me.ID_BTN_EDGE.Text = "Edge"
        Me.ID_BTN_EDGE.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(28, 35)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(117, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Threshold for Segment:"
        '
        'ID_LABEL_THR_SEG
        '
        Me.ID_LABEL_THR_SEG.AutoSize = True
        Me.ID_LABEL_THR_SEG.Location = New System.Drawing.Point(252, 18)
        Me.ID_LABEL_THR_SEG.Name = "ID_LABEL_THR_SEG"
        Me.ID_LABEL_THR_SEG.Size = New System.Drawing.Size(13, 13)
        Me.ID_LABEL_THR_SEG.TabIndex = 5
        Me.ID_LABEL_THR_SEG.Text = "0"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(28, 84)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(85, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Horizontal Lines:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(28, 134)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(73, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Vertical Lines:"
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.No, Me.HorCnt, Me.VerLine})
        Me.DataGridView1.Location = New System.Drawing.Point(12, 172)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(412, 143)
        Me.DataGridView1.TabIndex = 8
        '
        'No
        '
        Me.No.HeaderText = "No"
        Me.No.Name = "No"
        '
        'HorCnt
        '
        Me.HorCnt.HeaderText = "Horizontal Lines"
        Me.HorCnt.Name = "HorCnt"
        Me.HorCnt.Width = 150
        '
        'VerLine
        '
        Me.VerLine.HeaderText = "Vertical Lines"
        Me.VerLine.Name = "VerLine"
        Me.VerLine.Width = 150
        '
        'BtnExcel
        '
        Me.BtnExcel.Location = New System.Drawing.Point(31, 335)
        Me.BtnExcel.Name = "BtnExcel"
        Me.BtnExcel.Size = New System.Drawing.Size(75, 23)
        Me.BtnExcel.TabIndex = 9
        Me.BtnExcel.Text = "To Excel"
        Me.BtnExcel.UseVisualStyleBackColor = True
        '
        'BtnReport
        '
        Me.BtnReport.Location = New System.Drawing.Point(179, 335)
        Me.BtnReport.Name = "BtnReport"
        Me.BtnReport.Size = New System.Drawing.Size(75, 23)
        Me.BtnReport.TabIndex = 10
        Me.BtnReport.Text = "Report"
        Me.BtnReport.UseVisualStyleBackColor = True
        '
        'BtnExit
        '
        Me.BtnExit.Location = New System.Drawing.Point(327, 335)
        Me.BtnExit.Name = "BtnExit"
        Me.BtnExit.Size = New System.Drawing.Size(75, 23)
        Me.BtnExit.TabIndex = 11
        Me.BtnExit.Text = "Exit"
        Me.BtnExit.UseVisualStyleBackColor = True
        '
        'BtnCount
        '
        Me.BtnCount.Location = New System.Drawing.Point(328, 129)
        Me.BtnCount.Name = "BtnCount"
        Me.BtnCount.Size = New System.Drawing.Size(75, 23)
        Me.BtnCount.TabIndex = 12
        Me.BtnCount.Text = "Count"
        Me.BtnCount.UseVisualStyleBackColor = True
        '
        'ID_SCROLL_THR_SEG
        '
        Me.ID_SCROLL_THR_SEG.Location = New System.Drawing.Point(170, 35)
        Me.ID_SCROLL_THR_SEG.Maximum = 255
        Me.ID_SCROLL_THR_SEG.Name = "ID_SCROLL_THR_SEG"
        Me.ID_SCROLL_THR_SEG.Size = New System.Drawing.Size(152, 45)
        Me.ID_SCROLL_THR_SEG.TabIndex = 13
        '
        'Intersection
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(436, 370)
        Me.Controls.Add(Me.ID_SCROLL_THR_SEG)
        Me.Controls.Add(Me.BtnCount)
        Me.Controls.Add(Me.BtnExit)
        Me.Controls.Add(Me.BtnReport)
        Me.Controls.Add(Me.BtnExcel)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.ID_LABEL_THR_SEG)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ID_BTN_EDGE)
        Me.Controls.Add(Me.ID_NUM_VERTICAL)
        Me.Controls.Add(Me.ID_NUM_HORIZON)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "Intersection"
        Me.Text = "Intersection"
        CType(Me.ID_NUM_HORIZON, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ID_NUM_VERTICAL, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ID_SCROLL_THR_SEG, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ID_NUM_HORIZON As NumericUpDown
    Friend WithEvents ID_NUM_VERTICAL As NumericUpDown
    Friend WithEvents ID_BTN_EDGE As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents ID_LABEL_THR_SEG As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents BtnExcel As Button
    Friend WithEvents BtnReport As Button
    Friend WithEvents BtnExit As Button
    Friend WithEvents No As DataGridViewTextBoxColumn
    Friend WithEvents HorCnt As DataGridViewTextBoxColumn
    Friend WithEvents VerLine As DataGridViewTextBoxColumn
    Friend WithEvents BtnCount As Button
    Friend WithEvents ID_SCROLL_THR_SEG As TrackBar
End Class
