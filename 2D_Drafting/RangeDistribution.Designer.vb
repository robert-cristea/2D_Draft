<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RangeDistribution
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
        Me.RadioArea = New System.Windows.Forms.RadioButton()
        Me.RadioRound = New System.Windows.Forms.RadioButton()
        Me.RadioRatio = New System.Windows.Forms.RadioButton()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.TextFrom1 = New System.Windows.Forms.TextBox()
        Me.TextFrom2 = New System.Windows.Forms.TextBox()
        Me.TextFrom3 = New System.Windows.Forms.TextBox()
        Me.TextFrom4 = New System.Windows.Forms.TextBox()
        Me.TextFrom5 = New System.Windows.Forms.TextBox()
        Me.TextFrom6 = New System.Windows.Forms.TextBox()
        Me.TextFrom7 = New System.Windows.Forms.TextBox()
        Me.TextFrom8 = New System.Windows.Forms.TextBox()
        Me.TextTo8 = New System.Windows.Forms.TextBox()
        Me.TextTo7 = New System.Windows.Forms.TextBox()
        Me.TextTo6 = New System.Windows.Forms.TextBox()
        Me.TextTo5 = New System.Windows.Forms.TextBox()
        Me.TextTo4 = New System.Windows.Forms.TextBox()
        Me.TextTo3 = New System.Windows.Forms.TextBox()
        Me.TextTo2 = New System.Windows.Forms.TextBox()
        Me.TextTo1 = New System.Windows.Forms.TextBox()
        Me.TextCnt8 = New System.Windows.Forms.TextBox()
        Me.TextCnt7 = New System.Windows.Forms.TextBox()
        Me.TextCnt6 = New System.Windows.Forms.TextBox()
        Me.TextCnt5 = New System.Windows.Forms.TextBox()
        Me.TextCnt4 = New System.Windows.Forms.TextBox()
        Me.TextCnt3 = New System.Windows.Forms.TextBox()
        Me.TextCnt2 = New System.Windows.Forms.TextBox()
        Me.TextCnt1 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.BtnReCalc = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.RadioRatio)
        Me.GroupBox1.Controls.Add(Me.RadioArea)
        Me.GroupBox1.Controls.Add(Me.RadioRound)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(302, 54)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'RadioArea
        '
        Me.RadioArea.AutoSize = True
        Me.RadioArea.Location = New System.Drawing.Point(17, 19)
        Me.RadioArea.Name = "RadioArea"
        Me.RadioArea.Size = New System.Drawing.Size(47, 17)
        Me.RadioArea.TabIndex = 1
        Me.RadioArea.TabStop = True
        Me.RadioArea.Text = "Area"
        Me.RadioArea.UseVisualStyleBackColor = True
        '
        'RadioRound
        '
        Me.RadioRound.AutoSize = True
        Me.RadioRound.Location = New System.Drawing.Point(104, 19)
        Me.RadioRound.Name = "RadioRound"
        Me.RadioRound.Size = New System.Drawing.Size(79, 17)
        Me.RadioRound.TabIndex = 2
        Me.RadioRound.TabStop = True
        Me.RadioRound.Text = "Roundness"
        Me.RadioRound.UseVisualStyleBackColor = True
        '
        'RadioRatio
        '
        Me.RadioRatio.AutoSize = True
        Me.RadioRatio.Location = New System.Drawing.Point(199, 19)
        Me.RadioRatio.Name = "RadioRatio"
        Me.RadioRatio.Size = New System.Drawing.Size(96, 17)
        Me.RadioRatio.TabIndex = 3
        Me.RadioRatio.TabStop = True
        Me.RadioRatio.Text = "Perimeter/Area"
        Me.RadioRatio.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.TextCnt8)
        Me.GroupBox2.Controls.Add(Me.TextCnt7)
        Me.GroupBox2.Controls.Add(Me.TextCnt6)
        Me.GroupBox2.Controls.Add(Me.TextCnt5)
        Me.GroupBox2.Controls.Add(Me.TextCnt4)
        Me.GroupBox2.Controls.Add(Me.TextCnt3)
        Me.GroupBox2.Controls.Add(Me.TextCnt2)
        Me.GroupBox2.Controls.Add(Me.TextCnt1)
        Me.GroupBox2.Controls.Add(Me.TextTo8)
        Me.GroupBox2.Controls.Add(Me.TextTo7)
        Me.GroupBox2.Controls.Add(Me.TextTo6)
        Me.GroupBox2.Controls.Add(Me.TextTo5)
        Me.GroupBox2.Controls.Add(Me.TextTo4)
        Me.GroupBox2.Controls.Add(Me.TextTo3)
        Me.GroupBox2.Controls.Add(Me.TextTo2)
        Me.GroupBox2.Controls.Add(Me.TextTo1)
        Me.GroupBox2.Controls.Add(Me.TextFrom8)
        Me.GroupBox2.Controls.Add(Me.TextFrom7)
        Me.GroupBox2.Controls.Add(Me.TextFrom6)
        Me.GroupBox2.Controls.Add(Me.TextFrom5)
        Me.GroupBox2.Controls.Add(Me.TextFrom4)
        Me.GroupBox2.Controls.Add(Me.TextFrom3)
        Me.GroupBox2.Controls.Add(Me.TextFrom2)
        Me.GroupBox2.Controls.Add(Me.TextFrom1)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 72)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(302, 246)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        '
        'TextFrom1
        '
        Me.TextFrom1.Location = New System.Drawing.Point(17, 34)
        Me.TextFrom1.Name = "TextFrom1"
        Me.TextFrom1.Size = New System.Drawing.Size(78, 20)
        Me.TextFrom1.TabIndex = 0
        '
        'TextFrom2
        '
        Me.TextFrom2.Location = New System.Drawing.Point(17, 60)
        Me.TextFrom2.Name = "TextFrom2"
        Me.TextFrom2.Size = New System.Drawing.Size(78, 20)
        Me.TextFrom2.TabIndex = 1
        '
        'TextFrom3
        '
        Me.TextFrom3.Location = New System.Drawing.Point(17, 86)
        Me.TextFrom3.Name = "TextFrom3"
        Me.TextFrom3.Size = New System.Drawing.Size(78, 20)
        Me.TextFrom3.TabIndex = 2
        '
        'TextFrom4
        '
        Me.TextFrom4.Location = New System.Drawing.Point(17, 112)
        Me.TextFrom4.Name = "TextFrom4"
        Me.TextFrom4.Size = New System.Drawing.Size(78, 20)
        Me.TextFrom4.TabIndex = 3
        '
        'TextFrom5
        '
        Me.TextFrom5.Location = New System.Drawing.Point(17, 138)
        Me.TextFrom5.Name = "TextFrom5"
        Me.TextFrom5.Size = New System.Drawing.Size(78, 20)
        Me.TextFrom5.TabIndex = 4
        '
        'TextFrom6
        '
        Me.TextFrom6.Location = New System.Drawing.Point(17, 164)
        Me.TextFrom6.Name = "TextFrom6"
        Me.TextFrom6.Size = New System.Drawing.Size(78, 20)
        Me.TextFrom6.TabIndex = 5
        '
        'TextFrom7
        '
        Me.TextFrom7.Location = New System.Drawing.Point(17, 190)
        Me.TextFrom7.Name = "TextFrom7"
        Me.TextFrom7.Size = New System.Drawing.Size(78, 20)
        Me.TextFrom7.TabIndex = 6
        '
        'TextFrom8
        '
        Me.TextFrom8.Location = New System.Drawing.Point(17, 216)
        Me.TextFrom8.Name = "TextFrom8"
        Me.TextFrom8.Size = New System.Drawing.Size(78, 20)
        Me.TextFrom8.TabIndex = 7
        '
        'TextTo8
        '
        Me.TextTo8.Location = New System.Drawing.Point(112, 216)
        Me.TextTo8.Name = "TextTo8"
        Me.TextTo8.Size = New System.Drawing.Size(78, 20)
        Me.TextTo8.TabIndex = 15
        '
        'TextTo7
        '
        Me.TextTo7.Location = New System.Drawing.Point(112, 190)
        Me.TextTo7.Name = "TextTo7"
        Me.TextTo7.Size = New System.Drawing.Size(78, 20)
        Me.TextTo7.TabIndex = 14
        '
        'TextTo6
        '
        Me.TextTo6.Location = New System.Drawing.Point(112, 164)
        Me.TextTo6.Name = "TextTo6"
        Me.TextTo6.Size = New System.Drawing.Size(78, 20)
        Me.TextTo6.TabIndex = 13
        '
        'TextTo5
        '
        Me.TextTo5.Location = New System.Drawing.Point(112, 138)
        Me.TextTo5.Name = "TextTo5"
        Me.TextTo5.Size = New System.Drawing.Size(78, 20)
        Me.TextTo5.TabIndex = 12
        '
        'TextTo4
        '
        Me.TextTo4.Location = New System.Drawing.Point(112, 112)
        Me.TextTo4.Name = "TextTo4"
        Me.TextTo4.Size = New System.Drawing.Size(78, 20)
        Me.TextTo4.TabIndex = 11
        '
        'TextTo3
        '
        Me.TextTo3.Location = New System.Drawing.Point(112, 86)
        Me.TextTo3.Name = "TextTo3"
        Me.TextTo3.Size = New System.Drawing.Size(78, 20)
        Me.TextTo3.TabIndex = 10
        '
        'TextTo2
        '
        Me.TextTo2.Location = New System.Drawing.Point(112, 60)
        Me.TextTo2.Name = "TextTo2"
        Me.TextTo2.Size = New System.Drawing.Size(78, 20)
        Me.TextTo2.TabIndex = 9
        '
        'TextTo1
        '
        Me.TextTo1.Location = New System.Drawing.Point(112, 34)
        Me.TextTo1.Name = "TextTo1"
        Me.TextTo1.Size = New System.Drawing.Size(78, 20)
        Me.TextTo1.TabIndex = 8
        '
        'TextCnt8
        '
        Me.TextCnt8.Location = New System.Drawing.Point(208, 216)
        Me.TextCnt8.Name = "TextCnt8"
        Me.TextCnt8.Size = New System.Drawing.Size(78, 20)
        Me.TextCnt8.TabIndex = 23
        '
        'TextCnt7
        '
        Me.TextCnt7.Location = New System.Drawing.Point(208, 190)
        Me.TextCnt7.Name = "TextCnt7"
        Me.TextCnt7.Size = New System.Drawing.Size(78, 20)
        Me.TextCnt7.TabIndex = 22
        '
        'TextCnt6
        '
        Me.TextCnt6.Location = New System.Drawing.Point(208, 164)
        Me.TextCnt6.Name = "TextCnt6"
        Me.TextCnt6.Size = New System.Drawing.Size(78, 20)
        Me.TextCnt6.TabIndex = 21
        '
        'TextCnt5
        '
        Me.TextCnt5.Location = New System.Drawing.Point(208, 138)
        Me.TextCnt5.Name = "TextCnt5"
        Me.TextCnt5.Size = New System.Drawing.Size(78, 20)
        Me.TextCnt5.TabIndex = 20
        '
        'TextCnt4
        '
        Me.TextCnt4.Location = New System.Drawing.Point(208, 112)
        Me.TextCnt4.Name = "TextCnt4"
        Me.TextCnt4.Size = New System.Drawing.Size(78, 20)
        Me.TextCnt4.TabIndex = 19
        '
        'TextCnt3
        '
        Me.TextCnt3.Location = New System.Drawing.Point(208, 86)
        Me.TextCnt3.Name = "TextCnt3"
        Me.TextCnt3.Size = New System.Drawing.Size(78, 20)
        Me.TextCnt3.TabIndex = 18
        '
        'TextCnt2
        '
        Me.TextCnt2.Location = New System.Drawing.Point(208, 60)
        Me.TextCnt2.Name = "TextCnt2"
        Me.TextCnt2.Size = New System.Drawing.Size(78, 20)
        Me.TextCnt2.TabIndex = 17
        '
        'TextCnt1
        '
        Me.TextCnt1.Location = New System.Drawing.Point(208, 34)
        Me.TextCnt1.Name = "TextCnt1"
        Me.TextCnt1.Size = New System.Drawing.Size(78, 20)
        Me.TextCnt1.TabIndex = 16
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(25, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(33, 13)
        Me.Label1.TabIndex = 24
        Me.Label1.Text = "From:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(119, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(23, 13)
        Me.Label2.TabIndex = 25
        Me.Label2.Text = "To:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(211, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(75, 13)
        Me.Label3.TabIndex = 26
        Me.Label3.Text = "Nodule Count:"
        '
        'BtnReCalc
        '
        Me.BtnReCalc.Location = New System.Drawing.Point(158, 324)
        Me.BtnReCalc.Name = "BtnReCalc"
        Me.BtnReCalc.Size = New System.Drawing.Size(75, 23)
        Me.BtnReCalc.TabIndex = 2
        Me.BtnReCalc.Text = "Recalculate"
        Me.BtnReCalc.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Button2.Location = New System.Drawing.Point(239, 324)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 3
        Me.Button2.Text = "OK"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'RangeDistribution
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(326, 355)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.BtnReCalc)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "RangeDistribution"
        Me.Text = "Range Distribution"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents RadioRatio As RadioButton
    Friend WithEvents RadioArea As RadioButton
    Friend WithEvents RadioRound As RadioButton
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents TextCnt8 As TextBox
    Friend WithEvents TextCnt7 As TextBox
    Friend WithEvents TextCnt6 As TextBox
    Friend WithEvents TextCnt5 As TextBox
    Friend WithEvents TextCnt4 As TextBox
    Friend WithEvents TextCnt3 As TextBox
    Friend WithEvents TextCnt2 As TextBox
    Friend WithEvents TextCnt1 As TextBox
    Friend WithEvents TextTo8 As TextBox
    Friend WithEvents TextTo7 As TextBox
    Friend WithEvents TextTo6 As TextBox
    Friend WithEvents TextTo5 As TextBox
    Friend WithEvents TextTo4 As TextBox
    Friend WithEvents TextTo3 As TextBox
    Friend WithEvents TextTo2 As TextBox
    Friend WithEvents TextTo1 As TextBox
    Friend WithEvents TextFrom8 As TextBox
    Friend WithEvents TextFrom7 As TextBox
    Friend WithEvents TextFrom6 As TextBox
    Friend WithEvents TextFrom5 As TextBox
    Friend WithEvents TextFrom4 As TextBox
    Friend WithEvents TextFrom3 As TextBox
    Friend WithEvents TextFrom2 As TextBox
    Friend WithEvents TextFrom1 As TextBox
    Friend WithEvents BtnReCalc As Button
    Friend WithEvents Button2 As Button
End Class
