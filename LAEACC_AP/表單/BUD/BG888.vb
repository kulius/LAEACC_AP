Public Class BG888
    Dim confid, confunit As String, LoadAfter, Dirty As Boolean
    Dim strBGNo As String    '請購編號
    Dim sYear, lastpos, sSeason As Integer  '請購年度
    Dim sqlstr As String
    Dim bm As BindingManagerBase, myDataSet, AccnoDataSet, TempDataSet As DataSet
    Dim UserId, UserName, UserUnit, UserDate As String

    Private Sub BG888_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        UserId = TransPara.TransP("userid")
        UserDate = TransPara.TransP("userDATE")
        sYear = GetYear(UserDate)
        sSeason = Season(UserDate)  '判定季份
        If sSeason = 1 Then
            MsgBox("請以舊年度年底日期辦理")
            Me.Close()
        End If
        lblNoYear.Text = Format(GetYear(UserDate), "000")
        lblSeason.Text = "第" & sSeason & "季"
        LoadAfter = True
        TabControl1.Enabled = True
        '將下年度應付科目置combo
        sqlstr = "SELECT accno as accno, accno+'  '+accname as accname FROM ACCNAME " & _
                 "WHERE STAFF_NO = '" & Trim(UserId) & "' AND left(accno,5)='21202' and substring(accno,10,3)='" & _
                 Format(GetYear(UserDate), "000") & "' order by accno"
        AccnoDataSet = openmember("", "accno", sqlstr)
        If AccnoDataSet.Tables(0).Rows.Count = 0 Then
            cboAccno.Text = "尚無下年度應付科目"
            MsgBox("無下年度應付科目(2-1202-  -  -yyyxxxx),請先請主計單位建立")
        Else
            cboAccno.DataSource = AccnoDataSet.Tables(0)
            cboAccno.DisplayMember = "accname"  '顯示欄位
            cboAccno.ValueMember = "accno"     '欄位值
        End If

        Call LoadGridFunc()
        If DataGrid1.CurrentRowIndex > 0 Then
            bm.Position = 1
            Call PutGridToTxt()
        End If
    End Sub


    Sub LoadGridFunc()
        Dim sqlstr, qstr, strD, strC As String
        sqlstr = "SELECT a.bgno, a.accyear, a.accno, a.date1, a.date2, a.amt1, a.remark," & _
                 " a.amt2, a.amt3, a.useableamt, a.kind,a.subject," & _
                 " CASE WHEN len(a.accno)=17 THEN b.accname+'('+c.accname+')'" & _
                     " WHEN len(a.accno)<>17 THEN b.accname END AS accname" & _
                 " FROM BGF020 a INNER JOIN ACCNAME b ON  a.ACCNO = b.ACCNO" & _
                 " LEFT OUTER JOIN accname c ON left(a.accno,16)=c.accno and len(a.accno)=17" & _
                 " WHERE b.STAFF_NO = '" & Trim(UserId) & "' AND a.CLOSEMARK <> 'Y' and a.date2 is not null" & _
                 " and left(b.accno,1)='5' and a.accyear=" & sYear & " ORDER BY a.BGNO"
        myDataSet = openmember("", "BGF020", sqlstr)
        DataGrid1.DataSource = myDataSet
        DataGrid1.DataMember = "BGF020"
        bm = Me.BindingContext(myDataSet, "BGF020")
        TabControl1.SelectedIndex = 0
    End Sub

    Sub PutGridToTxt()
        Dim intI, SumUp As Integer
        Dim strI, strColumn1, strColumn2 As String
        If bm.Position < 0 Then Exit Sub
        If IsDBNull(bm.Current("bgno")) Then Exit Sub
        lblkey.Text = bm.Current("bgno")  'keep the old keyvalue
        lastpos = bm.Position
        lblBgno.Text = bm.Current("bgno") '不允許修改bgno,accyear,accno
        strBGNo = bm.Current("BGNO")
        lblBgamt.Text = FormatNumber(QueryBGAmt(bm.Current("accyear"), bm.Current("accno")), 1)
        lblunUseamt.Text = FormatNumber(QueryUnUseAmt(bm.Current("accyear"), bm.Current("accno"), sSeason), 1)
        'lblUsedAmt.Text = FormatNumber(QueryUsedAmt(strBGNo), 1) '本筆已開支額
        lblYear.Text = bm.Current("accyear")
        lblAccno.Text = bm.Current("accno")
        lblAccname.Text = IIf(IsDBNull(bm.Current("accname")), " ", bm.Current("accname"))
        lblDate1.Text = nz(bm.Current("date1"), "")
        lblDate2.Text = nz(bm.Current("date2"), "")
        txtRemark.Text = nz(bm.Current("remark"), " ")
        txtSubject.Text = nz(bm.Current("subject"), " ")
        SumUp = nz(bm.Current("useableamt"), 0)
        txtUseAmt.Text = FormatNumber(nz(bm.Current("useableamt"), 0))  '開支要以可支用金額
        lblUseableAmt.Text = FormatNumber(nz(bm.Current("useableamt"), 0), 1)
        lblAmt1.Text = FormatNumber(nz(bm.Current("amt1"), 0), 1)
        lblAmt2.Text = FormatNumber(nz(bm.Current("amt2"), 0), 1)
        lblAmt3.Text = FormatNumber(nz(bm.Current("amt3"), 0), 1)
        lblkey.Text = Trim(bm.Current("bgno"))
        lblNewBgno.Text = Format(sYear + 1, "000") + Format(QueryNO(sYear + 1, "B") + 1, "00000") 'vbdataio.vb 讀取new year請購編號
    End Sub

    Private Sub TabControl1_SELECTEDIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        If TabControl1.SelectedIndex = 1 Then
            Call DataGrid1_DoubleClick(New System.Object, New System.EventArgs)
        End If
    End Sub

    Private Sub DataGrid1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataGrid1.DoubleClick
        Call PutGridToTxt()
        TabControl1.SelectedIndex = 1  'go to the detail screen 
    End Sub

    Private Sub btnNo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNo.Click
        If txtNO.Text = "" Then Exit Sub
        Dim strNO As String
        Dim i As Integer
        strNO = lblNoYear.Text & Format(Val(txtNO.Text), "00000")
        For i = 0 To bm.Count - 1
            bm.Position = i
            If bm.Current("bgno") = strNO Then
                Call PutGridToTxt()
                TabControl1.SelectedIndex = 1  'go to the detail screen 
                Exit Sub
            End If
        Next
    End Sub

    Private Sub txtNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNO.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then   'press the enter key
            Call btnNo_Click(New System.Object, New System.EventArgs)
        End If
    End Sub

    Public Sub btnReflesh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReflesh.Click
        Call LoadGridFunc()
    End Sub

    Private Sub btnSure_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSure.Click  '一次開支
        'If ValComa(txtUseAmt.Text) = 0 Then Exit Sub
        Dim strRemark As String
        If ValComa(txtUseAmt.Text) = 0 Then
            If MsgBox("確定本筆不開支", MsgBoxStyle.YesNo) <> MsgBoxResult.Yes Then Exit Sub
        End If
        If ValComa(lblunUseamt.Text) < ValComa(txtUseAmt.Text) And TransPara.TransP("flow") <> "Y" Then   '不可溢支
            MsgBox("本季開支餘額不足,請退回")
            Exit Sub
        End If
        If ValComa(lblUseableAmt.Text) < ValComa(txtUseAmt.Text) And Mid(lblAccno.Text, 1, 1) <> "4" Then  '收入不必控制
            MsgBox("開支金額超過請購金額,請退回")
            Exit Sub
        End If
        Call UnitEndUse() '業務單位一次開支
        bm.Position = 1
        TabControl1.SelectedIndex = 0
        txtNO.Text = ""
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        TabControl1.SelectedIndex = 0  '退回
    End Sub

    Sub UnitEndUse()  '業務單位一次開支(最後一次開支)
        Dim keyvalue, sqlstr, retstr, updstr As String
        Dim intRel As Integer '開支次序
        Dim strClose As String '請購科目

        '舊年度資料處理(UPDATE BGF010->TOTuse,totPER)
        sqlstr = "update bgf010 set totper = totper - " & ValComa(lblUseableAmt.Text) & _
                 ", totuse = totuse + " & ValComa(txtUseAmt.Text) & _
                 " WHERE accyear=" & Trim(lblYear.Text) & " AND ACCNO = '" & Trim(lblAccno.Text) & "'"
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then
            MsgBox("預算檔bgf010更新錯誤" & sqlstr)
        End If

        '舊年度資料處理(UPDATE BGF020)
        GenUpdsql("kind", "3", "T") '轉帳借方
        GenUpdsql("CLOSEMARK", "Y", "T")
        GenUpdsql("subject", txtSubject.Text, "U")
        'GenInsSql("REMARK", Trim(txtRemark.Text) & "保留數轉帳", "U")
        sqlstr = "update BGF020 set " & GenUpdFunc & " where bgno='" & strBGNo & "'"
        retstr = runsql(mastconn, sqlstr)
        If retstr = "sqlok" Then
            myDataSet.Tables("BGF020").Rows.RemoveAt(lastpos)
        Else
            MsgBox("更新失敗" & sqlstr)
        End If

        '舊年度資料處理(新增一筆至BGF030)
        If Val(txtUseAmt.Text) = 0 Then Exit Sub '開支金額=0   不新增BGF030
        sqlstr = "SELECT max(REL) as rel  FROM BGF030 WHERE BGNO='" & strBGNo & "'"
        TempDataSet = openmember("", "TEMP", sqlstr)
        intRel = 0
        If TempDataSet.Tables("TEMP").Rows.Count > 0 And Not IsDBNull(TempDataSet.Tables("TEMP").Rows(0).Item(0)) Then
            intRel = TempDataSet.Tables("TEMP").Rows(0).Item(0)
        End If
        intRel += 1
        TempDataSet = Nothing

        sqlstr = GenInsFunc
        GenInsSql("BGNO", strBGNo, "T")
        GenInsSql("rel", intRel, "N")
        GenInsSql("date3", UserDate, "D")
        GenInsSql("date4", UserDate, "D")  '也將主計審核日填上
        GenInsSql("USEAMT", txtUseAmt.Text, "N")
        GenInsSql("REMARK", Trim(txtRemark.Text) & "保留數轉帳", "U")
        GenInsSql("NO_1_NO", 0, "N")   '不可置0  否則會出現在開傳票畫面上,so 以1取代   '100/1/5 update = 0 
        sqlstr = "insert into BGF030 " & GenInsFunc
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then
            'MsgBox("保留作業完成,  " + strBGNo)
            MsgBox("資料寫入BGF030失敗" + sqlstr)
            'Exit Sub
        End If



        '增入新年度資料
        '新年度資料處理(新增一筆至bgf010 & BGF020 & UPDATE BGF010->TOTPER)
        '取用new請購編號
        Dim intNo As Integer
        intNo = RequireNO("", sYear + 1, "B")
        GenInsSql("BGNO", Format(sYear + 1, "000") + Format(intNo, "00000"), "T")
        GenInsSql("accyear", sYear + 1, "N")
        GenInsSql("accno", cboAccno.SelectedValue, "T")
        GenInsSql("KIND", "2", "T")
        GenInsSql("DC", "1", "T")
        GenInsSql("DATE1", UserDate, "D")
        GenInsSql("DATE2", UserDate, "D")
        GenInsSql("REMARK", txtRemark.Text, "U")
        GenInsSql("AMT1", ValComa(txtUseAmt.Text), "N")
        GenInsSql("useableAMT", ValComa(txtUseAmt.Text), "N")
        GenInsSql("subject", txtSubject.Text, "U")
        GenInsSql("CLOSEMARK", " ", "T")
        sqlstr = "insert into BGF020 " & GenInsFunc
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then
            MsgBox("新增失敗" + sqlstr)
            'Exit Sub
        End If

        '新增一筆至保留原因檔bgf888
        sqlstr = GenInsFunc
        GenInsSql("BGNO", strBGNo, "T")  '記錄舊年度請購編號
        GenInsSql("dateopen", txtDateopen.Text, "T")  '權責發生日
        GenInsSql("dateclose", txtDateclose.Text, "T") '預計完工日
        GenInsSql("reason", txtReason.Text, "T")       '保留原因
        GenInsSql("newbgno", Format(sYear + 1, "000") + Format(intNo, "00000"), "T")  '新請購編號
        sqlstr = "insert into BGF888 " & GenInsFunc
        retstr = runsql(mastconn, sqlstr)


        'insert  BGF010
        GenInsSql("accyear", sYear + 1, "N")
        GenInsSql("accno", cboAccno.SelectedValue, "T")
        GenInsSql("DC", "2", "T")
        GenInsSql("systemdate", Format(Now(), "yyyy/MM/dd HH:mm:ss"), "D")
        sqlstr = "insert into BGF010 " & GenInsFunc    '假若資料已存在,則不會insert
        retstr = runsql("", sqlstr)
        'UPDATE BGF010->TOTPER+AMT 
        sqlstr = "update BGF010 set bg1=bg1+" & ValComa(txtUseAmt.Text) & ", TOTPER = TOTPER + " & ValComa(txtUseAmt.Text) & _
                 " where ACCYEAR=" & sYear + 1 & " AND ACCNO='" & cboAccno.SelectedValue & "'"
        retstr = runsql(mastconn, sqlstr)
        MsgBox("作業完成,請購編號=" + Format(sYear + 1, "000") + Format(intNo, "00000"))
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub txtuseamt_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUseAmt.LostFocus
        If Not IsNumeric(sender.Text) And Trim(sender.text) <> "" Then
            MsgBox("請輸入數字")
            sender.Focus()
        Else
            sender.text = FormatNumber(ValComa(sender.text), 1)
        End If
    End Sub

    Private Sub txtNO_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNO.LostFocus
        If (Not IsNumeric(sender.Text) Or ValComa(sender.text) < 0) And Trim(sender.text) <> "" Then
            MsgBox("請輸入數字")
            sender.Focus()
        End If
    End Sub
End Class
