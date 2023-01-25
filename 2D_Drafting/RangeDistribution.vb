Public Class RangeDistribution
    Private RadioState As Integer
    Private minArea As Single = 9999999
    Private maxArea As Single = 0
    Private minRound As Single = 1
    Private maxRound As Single = 0
    Private minRatio As Single = 9999999
    Private maxRatio As Single = 0

    Private Sub RangeDistribution_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        For i = 0 To Main_Form.Obj_Seg.BlobSegObj.BlobList.Count - 1
            Dim Obj = Main_Form.Obj_Seg.BlobSegObj.BlobList(i)
            If minArea > Obj.Area Then
                minArea = Obj.Area
            End If
            If maxArea < Obj.Area Then
                maxArea = Obj.Area
            End If
            If minRound > Obj.roundness Then
                minRound = Obj.roundness
            End If
            If maxRound < Obj.roundness Then
                maxRound = Obj.roundness
            End If
            Dim ratio = Obj.Perimeter / Obj.Area
            If minRatio > ratio Then
                minRatio = ratio
            End If
            If maxRatio < ratio Then
                maxRatio = ratio
            End If
        Next
    End Sub

    Private Sub RadioArea_CheckedChanged(sender As Object, e As EventArgs) Handles RadioArea.CheckedChanged
        If RadioArea.Checked Then
            Dim dist = (maxArea - minArea) / 9
            Dim Cnt As Integer() = New Integer(7) {}
            For i = 0 To 7
                Cnt(i) = 0
            Next

            For i = 0 To Main_Form.Obj_Seg.BlobSegObj.BlobList.Count - 1
                Dim Obj = Main_Form.Obj_Seg.BlobSegObj.BlobList(i)
                If Obj.Area < minArea + dist Then
                    Cnt(0) += 1
                ElseIf minArea + dist <= Obj.Area And Obj.Area < minArea + 2 * dist Then
                    Cnt(1) += 1
                ElseIf minArea + 2 * dist <= Obj.Area And Obj.Area < minArea + 3 * dist Then
                    Cnt(2) += 1
                ElseIf minArea + 3 * dist <= Obj.Area And Obj.Area < minArea + 4 * dist Then
                    Cnt(3) += 1
                ElseIf minArea + 4 * dist <= Obj.Area And Obj.Area < minArea + 5 * dist Then
                    Cnt(4) += 1
                ElseIf minArea + 5 * dist <= Obj.Area And Obj.Area < minArea + 6 * dist Then
                    Cnt(5) += 1
                ElseIf minArea + 6 * dist <= Obj.Area And Obj.Area < minArea + 7 * dist Then
                    Cnt(6) += 1
                ElseIf minArea + 7 * dist <= Obj.Area And Obj.Area <= maxArea Then
                    Cnt(7) += 1
                End If
            Next

            TextFrom1.Text = minArea.ToString()
            TextTo1.Text = (minArea + dist).ToString()
            TextFrom2.Text = (minArea + dist).ToString()
            TextTo2.Text = (minArea + 2 * dist).ToString()
            TextFrom3.Text = (minArea + 2 * dist).ToString()
            TextTo3.Text = (minArea + 3 * dist).ToString()
            TextFrom4.Text = (minArea + 3 * dist).ToString()
            TextTo4.Text = (minArea + 4 * dist).ToString()
            TextFrom5.Text = (minArea + 4 * dist).ToString()
            TextTo5.Text = (minArea + 5 * dist).ToString()
            TextFrom6.Text = (minArea + 5 * dist).ToString()
            TextTo6.Text = (minArea + 6 * dist).ToString()
            TextFrom7.Text = (minArea + 6 * dist).ToString()
            TextTo7.Text = (minArea + 7 * dist).ToString()
            TextFrom8.Text = (minArea + 7 * dist).ToString()
            TextTo8.Text = maxArea.ToString()

            TextCnt1.Text = Cnt(0).ToString
            TextCnt2.Text = Cnt(1).ToString
            TextCnt3.Text = Cnt(2).ToString
            TextCnt4.Text = Cnt(3).ToString
            TextCnt5.Text = Cnt(4).ToString
            TextCnt6.Text = Cnt(5).ToString
            TextCnt7.Text = Cnt(6).ToString
            TextCnt8.Text = Cnt(7).ToString
        End If
    End Sub

    Private Sub RadioRound_CheckedChanged(sender As Object, e As EventArgs) Handles RadioRound.CheckedChanged
        If RadioRound.Checked Then
            Dim dist = (maxRound - minRound) / 9
            Dim Cnt As Integer() = New Integer(7) {}
            For i = 0 To 7
                Cnt(i) = 0
            Next

            For i = 0 To Main_Form.Obj_Seg.BlobSegObj.BlobList.Count - 1
                Dim Obj = Main_Form.Obj_Seg.BlobSegObj.BlobList(i)
                If Obj.roundness < minRound + dist Then
                    Cnt(0) += 1
                ElseIf minRound + dist <= Obj.roundness And Obj.roundness < minRound + 2 * dist Then
                    Cnt(1) += 1
                ElseIf minRound + 2 * dist <= Obj.roundness And Obj.roundness < minRound + 3 * dist Then
                    Cnt(2) += 1
                ElseIf minRound + 3 * dist <= Obj.roundness And Obj.roundness < minRound + 4 * dist Then
                    Cnt(3) += 1
                ElseIf minRound + 4 * dist <= Obj.roundness And Obj.roundness < minRound + 5 * dist Then
                    Cnt(4) += 1
                ElseIf minRound + 5 * dist <= Obj.roundness And Obj.roundness < minRound + 6 * dist Then
                    Cnt(5) += 1
                ElseIf minRound + 6 * dist <= Obj.roundness And Obj.roundness < minRound + 7 * dist Then
                    Cnt(6) += 1
                ElseIf minRound + 7 * dist <= Obj.roundness And Obj.roundness <= maxRound Then
                    Cnt(7) += 1
                End If
            Next

            TextFrom1.Text = minRound.ToString()
            TextTo1.Text = (minRound + dist).ToString()
            TextFrom2.Text = (minRound + dist).ToString()
            TextTo2.Text = (minRound + 2 * dist).ToString()
            TextFrom3.Text = (minRound + 2 * dist).ToString()
            TextTo3.Text = (minRound + 3 * dist).ToString()
            TextFrom4.Text = (minRound + 3 * dist).ToString()
            TextTo4.Text = (minRound + 4 * dist).ToString()
            TextFrom5.Text = (minRound + 4 * dist).ToString()
            TextTo5.Text = (minRound + 5 * dist).ToString()
            TextFrom6.Text = (minRound + 5 * dist).ToString()
            TextTo6.Text = (minRound + 6 * dist).ToString()
            TextFrom7.Text = (minRound + 6 * dist).ToString()
            TextTo7.Text = (minRound + 7 * dist).ToString()
            TextFrom8.Text = (minRound + 7 * dist).ToString()
            TextTo8.Text = maxRound.ToString()

            TextCnt1.Text = Cnt(0).ToString
            TextCnt2.Text = Cnt(1).ToString
            TextCnt3.Text = Cnt(2).ToString
            TextCnt4.Text = Cnt(3).ToString
            TextCnt5.Text = Cnt(4).ToString
            TextCnt6.Text = Cnt(5).ToString
            TextCnt7.Text = Cnt(6).ToString
            TextCnt8.Text = Cnt(7).ToString
        End If
    End Sub

    Private Sub RadioRatio_CheckedChanged(sender As Object, e As EventArgs) Handles RadioRatio.CheckedChanged
        If RadioRatio.Checked Then
            Dim dist = (maxRatio - minRatio) / 9
            Dim Cnt As Integer() = New Integer(7) {}
            For i = 0 To 7
                Cnt(i) = 0
            Next

            For i = 0 To Main_Form.Obj_Seg.BlobSegObj.BlobList.Count - 1
                Dim Obj = Main_Form.Obj_Seg.BlobSegObj.BlobList(i)
                Dim ratio = Obj.Perimeter / Obj.Area
                If ratio < minRatio + dist Then
                    Cnt(0) += 1
                ElseIf minRatio + dist <= ratio And ratio < minRatio + 2 * dist Then
                    Cnt(1) += 1
                ElseIf minRatio + 2 * dist <= ratio And ratio < minRatio + 3 * dist Then
                    Cnt(2) += 1
                ElseIf minRatio + 3 * dist <= ratio And ratio < minRatio + 4 * dist Then
                    Cnt(3) += 1
                ElseIf minRatio + 4 * dist <= ratio And ratio < minRatio + 5 * dist Then
                    Cnt(4) += 1
                ElseIf minRatio + 5 * dist <= ratio And ratio < minRatio + 6 * dist Then
                    Cnt(5) += 1
                ElseIf minRatio + 6 * dist <= ratio And ratio < minRatio + 7 * dist Then
                    Cnt(6) += 1
                ElseIf minRatio + 7 * dist <= ratio And ratio <= maxRatio Then
                    Cnt(7) += 1
                End If
            Next

            TextFrom1.Text = minRatio.ToString()
            TextTo1.Text = (minRatio + dist).ToString()
            TextFrom2.Text = (minRatio + dist).ToString()
            TextTo2.Text = (minRatio + 2 * dist).ToString()
            TextFrom3.Text = (minRatio + 2 * dist).ToString()
            TextTo3.Text = (minRatio + 3 * dist).ToString()
            TextFrom4.Text = (minRatio + 3 * dist).ToString()
            TextTo4.Text = (minRatio + 4 * dist).ToString()
            TextFrom5.Text = (minRatio + 4 * dist).ToString()
            TextTo5.Text = (minRatio + 5 * dist).ToString()
            TextFrom6.Text = (minRatio + 5 * dist).ToString()
            TextTo6.Text = (minRatio + 6 * dist).ToString()
            TextFrom7.Text = (minRatio + 6 * dist).ToString()
            TextTo7.Text = (minRatio + 7 * dist).ToString()
            TextFrom8.Text = (minRatio + 7 * dist).ToString()
            TextTo8.Text = maxRatio.ToString()

            TextCnt1.Text = Cnt(0).ToString
            TextCnt2.Text = Cnt(1).ToString
            TextCnt3.Text = Cnt(2).ToString
            TextCnt4.Text = Cnt(3).ToString
            TextCnt5.Text = Cnt(4).ToString
            TextCnt6.Text = Cnt(5).ToString
            TextCnt7.Text = Cnt(6).ToString
            TextCnt8.Text = Cnt(7).ToString
        End If
    End Sub


End Class