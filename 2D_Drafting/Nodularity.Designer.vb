<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Nodularity
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
        Me.LabMinArea = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.LabMaxArea = New System.Windows.Forms.Label()
        Me.BtnManual = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.BtnAuto = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.BtnPerArea = New System.Windows.Forms.Button()
        Me.BtnRound = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.GroupName = New System.Windows.Forms.GroupBox()
        Me.TxtLimit = New System.Windows.Forms.TextBox()
        Me.BtnSetLimit = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.TxtField = New System.Windows.Forms.TextBox()
        Me.TxtTotal = New System.Windows.Forms.TextBox()
        Me.TxtNodCnt = New System.Windows.Forms.TextBox()
        Me.TxtNodPer = New System.Windows.Forms.TextBox()
        Me.TxtNodRat = New System.Windows.Forms.TextBox()
        Me.TxtMaxArea = New System.Windows.Forms.TextBox()
        Me.TxtMinArea = New System.Windows.Forms.TextBox()
        Me.TxtMaxPer = New System.Windows.Forms.TextBox()
        Me.TxtMinPer = New System.Windows.Forms.TextBox()
        Me.BtnAdd = New System.Windows.Forms.Button()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Fields = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TotalCnt = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NoduleCnt = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nodulari = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MaxArea = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MinArea = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MaxPer = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MinPer = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BtnRange = New System.Windows.Forms.Button()
        Me.BtnUndo = New System.Windows.Forms.Button()
        Me.BtnGraph = New System.Windows.Forms.Button()
        Me.BtnReport = New System.Windows.Forms.Button()
        Me.BtnExcel = New System.Windows.Forms.Button()
        Me.BtnExit = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupName.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.LabMinArea)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.LabMaxArea)
        Me.GroupBox1.Controls.Add(Me.BtnManual)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.BtnAuto)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(220, 102)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Detection-First Step"
        '
        'LabMinArea
        '
        Me.LabMinArea.AutoSize = True
        Me.LabMinArea.Location = New System.Drawing.Point(67, 77)
        Me.LabMinArea.Name = "LabMinArea"
        Me.LabMinArea.Size = New System.Drawing.Size(0, 13)
        Me.LabMinArea.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 77)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Min Area:"
        '
        'LabMaxArea
        '
        Me.LabMaxArea.AutoSize = True
        Me.LabMaxArea.Location = New System.Drawing.Point(67, 56)
        Me.LabMaxArea.Name = "LabMaxArea"
        Me.LabMaxArea.Size = New System.Drawing.Size(0, 13)
        Me.LabMaxArea.TabIndex = 1
        '
        'BtnManual
        '
        Me.BtnManual.Location = New System.Drawing.Point(117, 28)
        Me.BtnManual.Name = "BtnManual"
        Me.BtnManual.Size = New System.Drawing.Size(86, 23)
        Me.BtnManual.TabIndex = 2
        Me.BtnManual.Text = "Set Threshold"
        Me.BtnManual.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 56)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Max Area:"
        '
        'BtnAuto
        '
        Me.BtnAuto.Location = New System.Drawing.Point(21, 28)
        Me.BtnAuto.Name = "BtnAuto"
        Me.BtnAuto.Size = New System.Drawing.Size(75, 23)
        Me.BtnAuto.TabIndex = 1
        Me.BtnAuto.Text = "Automatic"
        Me.BtnAuto.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.BtnPerArea)
        Me.GroupBox2.Controls.Add(Me.BtnRound)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Location = New System.Drawing.Point(238, 14)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(146, 100)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Second Step"
        '
        'BtnPerArea
        '
        Me.BtnPerArea.Location = New System.Drawing.Point(28, 65)
        Me.BtnPerArea.Name = "BtnPerArea"
        Me.BtnPerArea.Size = New System.Drawing.Size(90, 23)
        Me.BtnPerArea.TabIndex = 4
        Me.BtnPerArea.Text = "Perimeter/Area"
        Me.BtnPerArea.UseVisualStyleBackColor = True
        '
        'BtnRound
        '
        Me.BtnRound.Location = New System.Drawing.Point(28, 33)
        Me.BtnRound.Name = "BtnRound"
        Me.BtnRound.Size = New System.Drawing.Size(90, 23)
        Me.BtnRound.TabIndex = 3
        Me.BtnRound.Text = "Roundness"
        Me.BtnRound.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(20, 17)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(76, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Distinguish By:"
        '
        'GroupName
        '
        Me.GroupName.Controls.Add(Me.TxtLimit)
        Me.GroupName.Controls.Add(Me.BtnSetLimit)
        Me.GroupName.Controls.Add(Me.Label5)
        Me.GroupName.Controls.Add(Me.TextBox1)
        Me.GroupName.Controls.Add(Me.Label4)
        Me.GroupName.Location = New System.Drawing.Point(12, 120)
        Me.GroupName.Name = "GroupName"
        Me.GroupName.Size = New System.Drawing.Size(372, 89)
        Me.GroupName.TabIndex = 2
        Me.GroupName.TabStop = False
        Me.GroupName.Text = "Roundness Limit"
        '
        'TxtLimit
        '
        Me.TxtLimit.Location = New System.Drawing.Point(148, 51)
        Me.TxtLimit.Name = "TxtLimit"
        Me.TxtLimit.ReadOnly = True
        Me.TxtLimit.Size = New System.Drawing.Size(174, 20)
        Me.TxtLimit.TabIndex = 7
        '
        'BtnSetLimit
        '
        Me.BtnSetLimit.Location = New System.Drawing.Point(40, 51)
        Me.BtnSetLimit.Name = "BtnSetLimit"
        Me.BtnSetLimit.Size = New System.Drawing.Size(75, 23)
        Me.BtnSetLimit.TabIndex = 6
        Me.BtnSetLimit.Text = "Set Limit.."
        Me.BtnSetLimit.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(254, 25)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(39, 13)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "Micron"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(148, 22)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(100, 20)
        Me.TextBox1.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(37, 25)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(89, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Min Nodule Area:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(18, 223)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(32, 13)
        Me.Label6.TabIndex = 3
        Me.Label6.Text = "Field:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(18, 249)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(73, 13)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "Total Objects:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(18, 275)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(80, 13)
        Me.Label8.TabIndex = 5
        Me.Label8.Text = "Nodules Count:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(18, 301)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(65, 13)
        Me.Label9.TabIndex = 6
        Me.Label9.Text = "Nodularity%:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(18, 327)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(84, 13)
        Me.Label10.TabIndex = 7
        Me.Label10.Text = "Nodules/mm sq:"
        Me.Label10.Visible = False
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(205, 223)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(55, 13)
        Me.Label11.TabIndex = 8
        Me.Label11.Text = "Max Area:"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(205, 249)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(52, 13)
        Me.Label12.TabIndex = 9
        Me.Label12.Text = "Min Area:"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(205, 275)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(77, 13)
        Me.Label13.TabIndex = 10
        Me.Label13.Text = "Max Perimeter:"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(205, 301)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(74, 13)
        Me.Label14.TabIndex = 11
        Me.Label14.Text = "Min Perimeter:"
        '
        'TxtField
        '
        Me.TxtField.Location = New System.Drawing.Point(108, 220)
        Me.TxtField.Name = "TxtField"
        Me.TxtField.Size = New System.Drawing.Size(65, 20)
        Me.TxtField.TabIndex = 12
        '
        'TxtTotal
        '
        Me.TxtTotal.Location = New System.Drawing.Point(108, 246)
        Me.TxtTotal.Name = "TxtTotal"
        Me.TxtTotal.Size = New System.Drawing.Size(65, 20)
        Me.TxtTotal.TabIndex = 13
        '
        'TxtNodCnt
        '
        Me.TxtNodCnt.Location = New System.Drawing.Point(108, 272)
        Me.TxtNodCnt.Name = "TxtNodCnt"
        Me.TxtNodCnt.Size = New System.Drawing.Size(65, 20)
        Me.TxtNodCnt.TabIndex = 14
        '
        'TxtNodPer
        '
        Me.TxtNodPer.Location = New System.Drawing.Point(108, 298)
        Me.TxtNodPer.Name = "TxtNodPer"
        Me.TxtNodPer.Size = New System.Drawing.Size(65, 20)
        Me.TxtNodPer.TabIndex = 15
        '
        'TxtNodRat
        '
        Me.TxtNodRat.Location = New System.Drawing.Point(108, 324)
        Me.TxtNodRat.Name = "TxtNodRat"
        Me.TxtNodRat.Size = New System.Drawing.Size(65, 20)
        Me.TxtNodRat.TabIndex = 16
        Me.TxtNodRat.Visible = False
        '
        'TxtMaxArea
        '
        Me.TxtMaxArea.Location = New System.Drawing.Point(291, 220)
        Me.TxtMaxArea.Name = "TxtMaxArea"
        Me.TxtMaxArea.Size = New System.Drawing.Size(65, 20)
        Me.TxtMaxArea.TabIndex = 17
        '
        'TxtMinArea
        '
        Me.TxtMinArea.Location = New System.Drawing.Point(291, 246)
        Me.TxtMinArea.Name = "TxtMinArea"
        Me.TxtMinArea.Size = New System.Drawing.Size(65, 20)
        Me.TxtMinArea.TabIndex = 18
        '
        'TxtMaxPer
        '
        Me.TxtMaxPer.Location = New System.Drawing.Point(291, 272)
        Me.TxtMaxPer.Name = "TxtMaxPer"
        Me.TxtMaxPer.Size = New System.Drawing.Size(65, 20)
        Me.TxtMaxPer.TabIndex = 19
        '
        'TxtMinPer
        '
        Me.TxtMinPer.Location = New System.Drawing.Point(291, 298)
        Me.TxtMinPer.Name = "TxtMinPer"
        Me.TxtMinPer.Size = New System.Drawing.Size(65, 20)
        Me.TxtMinPer.TabIndex = 20
        '
        'BtnAdd
        '
        Me.BtnAdd.Location = New System.Drawing.Point(219, 324)
        Me.BtnAdd.Name = "BtnAdd"
        Me.BtnAdd.Size = New System.Drawing.Size(75, 23)
        Me.BtnAdd.TabIndex = 21
        Me.BtnAdd.Text = "Add"
        Me.BtnAdd.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Fields, Me.TotalCnt, Me.NoduleCnt, Me.Nodulari, Me.MaxArea, Me.MinArea, Me.MaxPer, Me.MinPer})
        Me.DataGridView1.Location = New System.Drawing.Point(12, 353)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(372, 86)
        Me.DataGridView1.TabIndex = 22
        '
        'Fields
        '
        Me.Fields.HeaderText = "Fields"
        Me.Fields.Name = "Fields"
        '
        'TotalCnt
        '
        Me.TotalCnt.HeaderText = "Total Count"
        Me.TotalCnt.Name = "TotalCnt"
        '
        'NoduleCnt
        '
        Me.NoduleCnt.HeaderText = "Nodule Count"
        Me.NoduleCnt.Name = "NoduleCnt"
        '
        'Nodulari
        '
        Me.Nodulari.HeaderText = "Nodularity"
        Me.Nodulari.Name = "Nodulari"
        '
        'MaxArea
        '
        Me.MaxArea.HeaderText = "Max Area"
        Me.MaxArea.Name = "MaxArea"
        '
        'MinArea
        '
        Me.MinArea.HeaderText = "Min Area"
        Me.MinArea.Name = "MinArea"
        '
        'MaxPer
        '
        Me.MaxPer.HeaderText = "Max Perimeter"
        Me.MaxPer.Name = "MaxPer"
        '
        'MinPer
        '
        Me.MinPer.HeaderText = "Min Perimeter"
        Me.MinPer.Name = "MinPer"
        '
        'BtnRange
        '
        Me.BtnRange.Location = New System.Drawing.Point(12, 445)
        Me.BtnRange.Name = "BtnRange"
        Me.BtnRange.Size = New System.Drawing.Size(58, 23)
        Me.BtnRange.TabIndex = 23
        Me.BtnRange.Text = "Range"
        Me.BtnRange.UseVisualStyleBackColor = True
        '
        'BtnUndo
        '
        Me.BtnUndo.Location = New System.Drawing.Point(74, 445)
        Me.BtnUndo.Name = "BtnUndo"
        Me.BtnUndo.Size = New System.Drawing.Size(58, 23)
        Me.BtnUndo.TabIndex = 24
        Me.BtnUndo.Text = "Undo"
        Me.BtnUndo.UseVisualStyleBackColor = True
        '
        'BtnGraph
        '
        Me.BtnGraph.Location = New System.Drawing.Point(136, 445)
        Me.BtnGraph.Name = "BtnGraph"
        Me.BtnGraph.Size = New System.Drawing.Size(58, 23)
        Me.BtnGraph.TabIndex = 25
        Me.BtnGraph.Text = "Graph"
        Me.BtnGraph.UseVisualStyleBackColor = True
        '
        'BtnReport
        '
        Me.BtnReport.Location = New System.Drawing.Point(198, 445)
        Me.BtnReport.Name = "BtnReport"
        Me.BtnReport.Size = New System.Drawing.Size(58, 23)
        Me.BtnReport.TabIndex = 26
        Me.BtnReport.Text = "Report"
        Me.BtnReport.UseVisualStyleBackColor = True
        '
        'BtnExcel
        '
        Me.BtnExcel.Location = New System.Drawing.Point(260, 445)
        Me.BtnExcel.Name = "BtnExcel"
        Me.BtnExcel.Size = New System.Drawing.Size(58, 23)
        Me.BtnExcel.TabIndex = 27
        Me.BtnExcel.Text = "To Excel"
        Me.BtnExcel.UseVisualStyleBackColor = True
        '
        'BtnExit
        '
        Me.BtnExit.Location = New System.Drawing.Point(322, 445)
        Me.BtnExit.Name = "BtnExit"
        Me.BtnExit.Size = New System.Drawing.Size(58, 23)
        Me.BtnExit.TabIndex = 28
        Me.BtnExit.Text = "Exit"
        Me.BtnExit.UseVisualStyleBackColor = True
        '
        'Nodularity
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(396, 476)
        Me.Controls.Add(Me.BtnExit)
        Me.Controls.Add(Me.BtnExcel)
        Me.Controls.Add(Me.BtnReport)
        Me.Controls.Add(Me.BtnGraph)
        Me.Controls.Add(Me.BtnUndo)
        Me.Controls.Add(Me.BtnRange)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.BtnAdd)
        Me.Controls.Add(Me.TxtMinPer)
        Me.Controls.Add(Me.TxtMaxPer)
        Me.Controls.Add(Me.TxtMinArea)
        Me.Controls.Add(Me.TxtMaxArea)
        Me.Controls.Add(Me.TxtNodRat)
        Me.Controls.Add(Me.TxtNodPer)
        Me.Controls.Add(Me.TxtNodCnt)
        Me.Controls.Add(Me.TxtTotal)
        Me.Controls.Add(Me.TxtField)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.GroupName)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "Nodularity"
        Me.Text = "Nodularity"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupName.ResumeLayout(False)
        Me.GroupName.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents LabMinArea As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents LabMaxArea As Label
    Friend WithEvents BtnManual As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents BtnAuto As Button
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents BtnPerArea As Button
    Friend WithEvents BtnRound As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents GroupName As GroupBox
    Friend WithEvents TxtLimit As TextBox
    Friend WithEvents BtnSetLimit As Button
    Friend WithEvents Label5 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents TxtField As TextBox
    Friend WithEvents TxtTotal As TextBox
    Friend WithEvents TxtNodCnt As TextBox
    Friend WithEvents TxtNodPer As TextBox
    Friend WithEvents TxtNodRat As TextBox
    Friend WithEvents TxtMaxArea As TextBox
    Friend WithEvents TxtMinArea As TextBox
    Friend WithEvents TxtMaxPer As TextBox
    Friend WithEvents TxtMinPer As TextBox
    Friend WithEvents BtnAdd As Button
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents Fields As DataGridViewTextBoxColumn
    Friend WithEvents TotalCnt As DataGridViewTextBoxColumn
    Friend WithEvents NoduleCnt As DataGridViewTextBoxColumn
    Friend WithEvents Nodulari As DataGridViewTextBoxColumn
    Friend WithEvents MaxArea As DataGridViewTextBoxColumn
    Friend WithEvents MinArea As DataGridViewTextBoxColumn
    Friend WithEvents MaxPer As DataGridViewTextBoxColumn
    Friend WithEvents MinPer As DataGridViewTextBoxColumn
    Friend WithEvents BtnRange As Button
    Friend WithEvents BtnUndo As Button
    Friend WithEvents BtnGraph As Button
    Friend WithEvents BtnReport As Button
    Friend WithEvents BtnExcel As Button
    Friend WithEvents BtnExit As Button
End Class
