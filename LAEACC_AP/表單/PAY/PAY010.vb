Public Class PAY010
    Dim LoadAfter, Dirty As Boolean
    Dim sqlstr As String
    Dim myDataSet, myDataSet2 As DataSet
    Dim SBank, SDate As String
    Dim IncomeAmt, PayAmt, DayBalance As Decimal

    Private Sub pay010_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadAfter = True
        Call LoadGridFunc()
    End Sub

    Sub LoadGridFunc()
        Dim sqlstr, qstr, strD, strC As String
        Dim intI As Integer
        sqlstr = "SELECT * FROM  CHF020 where day_income<>0 or day_pay<>0 or date_2 is not null order by bank"
        myDataSet = openmember("", "chf020", sqlstr)
        If myDataSet.Tables("chf020").Rows.Count = 0 Then
            lblMsg.Text = "已開帳完成"
            MsgBox("已開帳完成")
            btnSure.Visible = False
            Exit Sub
        Else
            lblDate2.Text = myDataSet.Tables("chf020").Rows(0).Item("date_2").toshortdatestring
            DataGrid1.DataSource = myDataSet
            DataGrid1.DataMember = "chf020"
        End If
        For intI = 0 To myDataSet.Tables("chf020").Rows.Count - 1
            If myDataSet.Tables("chf020").Rows(intI).Item("prt_code") <> "Y" Then
                btnSure.Visible = False
                lblMsg.Text = "尚未列印結存日計表"
                MsgBox("尚未列印結存日計表")
                Exit Sub
            End If
        Next
    End Sub


    Private Sub btnSure_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSure.Click
        Dim retstr As String
        Dim intI As Integer
        For intI = 0 To myDataSet.Tables("chf020").Rows.Count - 1
            With myDataSet.Tables("chf020").Rows(intI)
                Sbank = .Item("bank")
                IncomeAmt = .Item("day_income")
                PayAmt = .Item("day_pay")
                SDate = .Item("date_2").toshortdatestring
                DayBalance = .Item("balance") + IncomeAmt - PayAmt
            End With
            Call upd_chf030()   '寫入歷史檔
        Next
        myDataSet = Nothing
        myDataSet2 = Nothing
        'update chf020 
        sqlstr = "update chf020 set balance=balance+day_income-day_pay, day_income=0, day_pay=0 ,date_2 = null where day_pay<>0 or day_income<>0 or date_2 is not null"
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then MsgBox("update chf020 error " & sqlstr)
        lblMsg.Text = "作業完成"
        btnSure.Visible = False
    End Sub

    Sub upd_chf030()
        Dim retstr As String
        sqlstr = "SELECT * from chf030 where bank='" & SBank & "' and date_2='" & FullDate(SDate) & "'"
        myDataSet2 = openmember("", "chf030", sqlstr)
        If myDataSet2.Tables("chf030").Rows.Count = 0 Then   '原無該日資料則新增一筆
            GenInsSql("bank", SBank, "T")
            GenInsSql("date_2", SDate, "D")
            GenInsSql("day_income", IncomeAmt, "N")
            GenInsSql("day_PAY", PayAmt, "N")
            GenInsSql("balance", DayBalance, "N")
            sqlstr = "insert into chf030 " & GenInsFunc
            retstr = runsql(mastconn, sqlstr)
            If retstr <> "sqlok" Then MsgBox("寫入歷史檔 error " & sqlstr)
        Else
            GenUpdsql("day_income", myDataSet2.Tables("chf030").Rows(0).Item("day_income") + IncomeAmt, "N")
            GenUpdsql("day_pay", myDataSet2.Tables("chf030").Rows(0).Item("day_pay") + PayAmt, "N")
            GenUpdsql("balance", myDataSet2.Tables("chf030").Rows(0).Item("balance") + IncomeAmt - PayAmt, "N")
            sqlstr = "update chf030 set " & genupdfunc & " where bank='" & SBank & "' and date_2='" & FullDate(SDate) & "'"
            retstr = runsql(mastconn, sqlstr)
            If retstr <> "sqlok" Then MsgBox("update chf030 error " & sqlstr)
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        MyBase.Close()
    End Sub
End Class
