Public Class AC050R
    Dim myDatasetS, myDatasetT, mydataset As DataSet
    Private Sub AC050R_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim sqlstr As String
        sqlstr = "SELECT max(date_2) as date_2  from acf010 where books='Y'"
        myDatasetS = openmember("", "acf010s", sqlstr)
        If myDatasetS.Tables("acf010s").Rows.Count = 0 Then
            lblMsg.Text = "沒有資料"
            Exit Sub
        Else
            nudMonth.Value = Month(myDatasetS.Tables("acf010s").Rows(0).Item("date_2"))
            lblYear.Text = GetYear(myDatasetS.Tables("acf010s").Rows(0).Item("date_2"))
        End If
        btnSure.Enabled = True
    End Sub

    Private Sub btnSure_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSure.Click
        btnSure.Enabled = False   '防止user再按
        Dim sqlstr, strUpdate, retstr, strJ, strDates As String
        Dim intI, intJ As Integer
        Dim smm As Integer = nudMonth.Value
        If smm < 1 Then Exit Sub
        Dim up As String = Format(smm - 1, "00")   '以上月餘額為填入數
        strDates = FullDate(lblYear.Text & "/" & smm & "/1")
        If MsgBox("起日=" & strDates, MsgBoxStyle.YesNo) <> MsgBoxResult.Yes Then
            Exit Sub
        End If
        lblMsg.Text = "acf010處理中請稍待"   '清books即可
        sqlstr = "update acf010 set books =' ' where date_2>='" & strDates & "' and books='Y'"
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then MsgBox("acf010處理 error " & sqlstr)

        lblMsg.Text = "acf070處理中請稍待"
        sqlstr = "delete from acf070 where date_2>='" & strDates & "'"
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then MsgBox("acf070處理 error " & sqlstr)

        lblMsg.Text = "acf050處理中請稍待"
        If up = "00" Then
            strUpdate = ""
            For intJ = smm To 12
                strJ = Format(intJ, "00")
                strUpdate &= " deamt" & strJ & "= beg_debit, cramt" & strJ & "= beg_credit,"
            Next
        Else
            strUpdate = ""
            For intJ = smm To 12
                strJ = Format(intJ, "00")
                strUpdate &= " deamt" & strJ & "= deamt" & up & ", cramt" & strJ & "= cramt" & up & ","
            Next
        End If
        sqlstr = "update acf050 set " & cutright1(strUpdate, ",") & " where accyear=" & Val(lblYear.Text)
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then MsgBox("acf050處理 error " & sqlstr)

        lblMsg.Text = "acf060處理中請稍待"
        If up = "00" Then
            strUpdate = ""
            For intJ = smm To 12
                strJ = Format(intJ, "00")
                strUpdate &= " deamt" & strJ & "= beg_debit, cramt" & strJ & "= beg_credit," & _
                             " account" & strJ & "=abs(beg_debit-beg_credit)," & _
                             " act" & strJ & "= 0, sub" & strJ & "=0, trans" & strJ & "=0,"
            Next
        Else
            strUpdate = ""
            For intJ = smm To 12
                strJ = Format(intJ, "00")
                strUpdate &= " deamt" & strJ & "= deamt" & up & ", cramt" & strJ & "= cramt" & up & _
                             ", account" & strJ & "=account" & up & ", act" & strJ & "=act" & up & _
                             ", sub" & strJ & "=sub" & up & ", trans" & strJ & "=trans" & up & ","
            Next
        End If
        sqlstr = "update acf060 set " & cutright1(strUpdate, ",") & " where accyear=" & Val(lblYear.Text)
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then MsgBox("acf060處理 error " & sqlstr)


        lblMsg.Text = "acf100處理中請稍待"
        If up = "00" Then
            strUpdate = ""
            For intJ = smm To 12
                strJ = Format(intJ, "00")
                strUpdate &= " qtyin" & strJ & "= qty_beg, qtyout" & strJ & "= 0,"
            Next
        Else
            strUpdate = ""
            For intJ = smm To 12
                strJ = Format(intJ, "00")
                strUpdate &= " qtyin" & strJ & "= qtyin" & up & ", qtyout" & strJ & "= qtyout" & up & ","
            Next
        End If
        sqlstr = "update acf100 set " & cutright1(strUpdate, ",") & " where accyear=" & Val(lblYear.Text)
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then MsgBox("acf100處理 error " & sqlstr)

        lblMsg.Text = "處理完成"
        myDatasetT = Nothing
        myDatasetS = Nothing
        btnExit.Text = "結束"
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class
