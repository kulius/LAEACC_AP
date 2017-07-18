Public Class BG060
    Dim confid, confunit As String, LoadAfter, Dirty As Boolean
    Dim strBGNo As String    '請購編號
    Dim sYear, lastpos, sSeason, intRel, intBGCNO As Integer '請購年度
    Dim sqlstr As String
    Dim bm, bm2 As BindingManagerBase, myDataSet, myDataSet2, AccnoDataSet, TempDataSet As DataSet
    Dim UserId, UserName, UserUnit, UserDate As String

    Private Sub BG060_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        UserId = TransPara.TransP("userid")
        UserDate = TransPara.TransP("userDATE")
        sYear = GetYear(UserDate)
        sSeason = Season(UserDate)  '判定季份
        Call PutAccnoToCbo()  '置會計科目愉combo
        LoadAfter = True
        TabControl1.Enabled = True
        Call LoadGridFunc()
        If DataGrid1.CurrentRowIndex > 0 Then
            bm.Position = 1
            Call PutGridToTxt()
        End If
    End Sub


    Sub LoadGridFunc()
        Dim sqlstr, qstr, strD, strC As String
        sqlstr = "SELECT a.bgcno, a.accyear, a.datec, b.KIND, SUM(abs(c.USEAMT)) AS useamt " & _
                 "FROM BGF040 a INNER JOIN BGF020 b ON a.bgno = b.BGNO " & _
                 "INNER JOIN BGF030 c ON a.bgno = c.BGNO " & _
                 "WHERE (a.no_1_no = 0 and a.accyear=" & sYear & ") GROUP BY  a.bgcno, a.accyear, a.datec, b.KIND " & _
                 "ORDER BY a.bgcno"
        '取正數合計,借貸才能balance
        myDataSet = openmember("", "bgf040", sqlstr)
        DataGrid1.DataSource = myDataSet
        DataGrid1.DataMember = "bgf040"
        bm = Me.BindingContext(myDataSet, "bgf040")
        TabControl1.SelectedIndex = 0
    End Sub

    Sub PutGridToTxt()
        Dim intI, SumUp As Integer
        Dim strI, strColumn1, strColumn2 As String
        If bm.Position < 0 Then Exit Sub
        lastpos = bm.Position
        intBGCNO = bm.Current("BGCNO")
        lblYear.Text = bm.Current("accyear")
        lblDatec.Text = bm.Current("DATEC")
        Call LoadGrid2Func()
        Call PutGrid2ToTxt()   'put the first record to text 
    End Sub

    Sub LoadGrid2Func()
        Dim sqlstr, qstr, strD, strC As String
        sqlstr = "SELECT b.BGNO, b.ACCYEAR, b.ACCNO, b.KIND, c.REMARK, abs(c.USEAMT) as useamt, " & _
                 "CASE WHEN len(b.accno)=17 THEN d.accname+'('+e.accname+')' " & _
                     " WHEN len(b.accno)<>17 THEN d.accname END AS accname " & _
                 "FROM BGF040 a " & _
                 "INNER JOIN BGF020 b ON a.bgno = b.BGNO " & _
                 "INNER JOIN BGF030 c ON b.BGNO = c.BGNO " & _
                 "INNER JOIN ACCNAME d ON b.ACCNO = d.ACCNO " & _
                 "LEFT OUTER JOIN accname e ON LEFT(b.ACCNO, 16) = e.ACCNO and len(b.accno)=17 " & _
                 "WHERE a.bgcno = " & Str(intBGCNO) & " and a.accyear=" & sYear & _
                 " ORDER BY  b.KIND"
        myDataSet2 = openmember("", "bgf020", sqlstr)
        DataGrid2.DataSource = myDataSet2
        DataGrid2.DataMember = "bgf020"
        bm2 = Me.BindingContext(myDataSet2, "bgf020")
        TabControl1.SelectedIndex = 1
    End Sub

    Sub PutGrid2ToTxt()
        Dim intI, SumUp As Integer
        Dim strI, strColumn1, strColumn2 As String
        If bm2.Position < 0 Then Exit Sub
        If IsDBNull(bm2.Current("bgno")) Then Exit Sub
        lastpos = bm2.Position
        lblBgno.Text = bm2.Current("bgno")   '不允許修改bgno
        strBGNo = bm2.Current("BGNO")
        If bm2.Current("kind") = "3" Then
            rdbKind1.Checked = True
        Else
            rdbkind2.Checked = True
        End If
        lblKind.Text = bm2.Current("kind")
        lblaccYear.Text = bm2.Current("accyear")
        lblAccno.Text = nz(bm2.Current("accno"), " ")
        cboAccno.SelectedValue = Mid("0" + LTrim(Str(bm2.Current("accyear"))), 1, 3) + Mid(bm2.Current("accno") + Space(17), 1, 17) '設定科目選定值
        txtRemark.Text = nz(bm2.Current("remark"), " ")
        txtUseAmt.Text = Format(nz(bm2.Current("useamt"), 0), "###,###,###.#")
        lblUseAmt.Text = Format(nz(bm2.Current("useamt"), 0), "###,###,###.#")
    End Sub

    Sub PutAccnoToCbo()
        Dim sqlstr As String
        sqlstr = "SELECT right('0'+ltrim(str(a.accyear)),3)" & _
                 "+left(a.accno+space(17),17) as bgf010key, " & _
                 "right('0'+ltrim(str(a.accyear)),3)+left(a.accno+space(17),17)+c.accname+'-'+" & _
                 "b.accname+str(a.bg1+a.bg2+a.bg3+a.bg4+a.up1+a.up2+a.up3+a.up4-a.totper-a.totuse) as bgf010data " & _
                 "FROM bgf010 a INNER JOIN ACCNAME b ON a.ACCNO = b.ACCNO " & _
                 "INNER JOIN ACCNAME c ON LEFT(a.ACCNO, 16) = c.ACCNO WHERE a.ctrl<>'Y' order by bgf010key"
        AccnoDataSet = openmember("", "bgf010", sqlstr)
        If AccnoDataSet.Tables("BGF010").Rows.Count = 0 Then
            cboAccno.Text = "尚無可請購科目"
        Else
            cboAccno.DataSource = AccnoDataSet.Tables("bgf010")
            cboAccno.DisplayMember = "bgf010data"  '顯示欄位
            cboAccno.ValueMember = "bgf010key"     '欄位值
        End If
    End Sub


    Private Sub TabControl1_SELECTEDIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        If LoadAfter = False Then Exit Sub
        ' If TabControl1.SelectedIndex = 1 Then Dirty = False
        If Dirty = True Then
            If MsgBox("資料尚未存檔 要放棄嗎?", MsgBoxStyle.DefaultButton1 + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                TabControl1.SelectedIndex = 1
                Dirty = False
            End If
        End If
    End Sub

    Private Sub DataGrid1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataGrid1.DoubleClick
        If bm.Position < 0 Then Exit Sub
        lblkey.Text = bm.Current("bgcno")  'keep the old keyvalue
        lblBgcno.Text = bm.Current("bgcno")  'keep the old keyvalue
        lblDatec.Text = bm.Current("datec")
        Call PutGridToTxt()
    End Sub

    Private Sub DataGrid2_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid2.DoubleClick
        If bm2.Position < 0 Then Exit Sub
        lblBgno.Text = bm2.Current("bgno")  'keep the old keyvalue
        Call PutGrid2ToTxt()
    End Sub

    Private Sub DataGrid2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid2.Click
        If bm2.Position < 0 Then Exit Sub
        lblBgno.Text = bm2.Current("bgno")  'same to doubleclick
        Call PutGrid2ToTxt()
    End Sub

    '新增轉帳
    Public Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        lblYear.Text = sYear
        lblDatec.Text = UserDate
        intBGCNO = QueryNO(sYear, "C") + 1
        lblBgcno.Text = intBGCNO
        cboAccno.Focus()
        Call LoadGrid2Func()
    End Sub

    Private Sub btnInsert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInsert.Click
        Dim retstr, sqlstr, strBgno As String
        Dim intNO As Integer
        If ValComa(txtUseAmt.Text) <= 0 Then
            MsgBox("金額不可為零或負數")
            Exit Sub
        End If

        If myDataSet2.Tables("bgf020").Rows.Count = 0 Then
            intBGCNO = RequireNO(mastconn, sYear, "C")     '當第一筆轉帳明細時,才確實取用轉帳編號
            lblBgcno.Text = intBGCNO
        End If

        intNO = RequireNO(mastconn, sYear, "B") '取得請購編號
        Dim BYear, I As Integer '請購的預算年度
        Dim BAccno, Bkind As String '請購科目
        Dim Bamt As Decimal
        BYear = Mid(cboAccno.SelectedValue, 1, 3)
        BAccno = Trim(Mid(cboAccno.SelectedValue, 4, 17))
        strBgno = Format(sYear, "000") + Format(intNO, "00000")
        Bkind = IIf(rdbKind1.Checked, "3", "4")

        Bamt = ValComa(txtUseAmt.Text)
        If Bkind = "3" Then  '借方
            If Mid(BAccno, 1, 1) = "3" Or Mid(BAccno, 1, 1) = "4" Then Bamt = -ValComa(txtUseAmt.Text)
        Else  '2負債科目亦同支出處理
            If Mid(BAccno, 1, 1) = "5" Or Mid(BAccno, 1, 1) = "1" Or Mid(BAccno, 1, 1) = "2" Then Bamt = -ValComa(txtUseAmt.Text)
        End If
        'Bamt = IIf(Bkind = Accnokind, ValComa(txtUseAmt.Text), -ValComa(txtUseAmt.Text))  '借方科目在轉帳貸方時,表示減少,反之亦同

        '資料處理(新增一筆至BGF020 & BGF030 & bgf040)
        sqlstr = GenInsFunc
        GenInsSql("BGNO", strBgno, "T")
        GenInsSql("accyear", BYear, "N")
        GenInsSql("accno", BAccno, "T")
        GenInsSql("KIND", Bkind, "T")
        GenInsSql("DC", IIf(Bkind = "3", "1", "2"), "T")
        GenInsSql("DATE1", lblDatec.Text, "D")
        GenInsSql("DATE2", lblDatec.Text, "D")
        GenInsSql("REMARK", txtRemark.Text, "U")
        GenInsSql("AMT1", Bamt, "N")
        GenInsSql("useableAMT", Bamt, "N")
        GenInsSql("subject", " ", "U")
        GenInsSql("CLOSEMARK", "Y", "T")
        sqlstr = "insert into BGF020 " & GenInsFunc
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then
            MsgBox("新增失敗" + sqlstr)
            Exit Sub
        End If

        '資料處理INSERT bgf030-
        GenInsSql("BGNO", strBgno, "T")
        GenInsSql("rel", 0, "N")
        GenInsSql("date3", lblDatec.Text, "D")
        GenInsSql("date4", lblDatec.Text, "D")
        GenInsSql("USEAMT", Bamt, "N")
        GenInsSql("REMARK", txtRemark.Text, "U")
        GenInsSql("NO_1_NO", 0, "N")
        sqlstr = "insert into BGF030 " & GenInsFunc
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then
            MsgBox("資料寫入BGF030失敗" + sqlstr)
            Exit Sub
        End If

        '資料處理INSERT bgf040
        GenInsSql("accyear", sYear, "N")
        GenInsSql("BGCNO", lblBgcno.Text, "N")
        GenInsSql("BGNO", strBgno, "T")
        GenInsSql("DATEC", lblDatec.Text, "D")
        GenInsSql("NO_1_NO", 0, "N")
        sqlstr = "insert into BGF040 " & GenInsFunc
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then
            MsgBox("資料寫入BGF040失敗" + sqlstr)
            Exit Sub
        End If


        '資料處理(UPDATE BGF010->TOTuse)
        'If rdbKind1.Checked Then  'debit 
        sqlstr = "update bgf010 set totuse = totuse + " & Bamt & _
                 " WHERE accyear=" & Str(BYear) & " AND ACCNO = '" & Trim(BAccno) & "'"
        'Else
        '    sqlstr = "update bgf010 set totuse = totuse - " & Bamt & _
        '            " WHERE accyear=" & Str(BYear) & " AND ACCNO = '" & Trim(BAccno) & "'"
        'End If
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then
            MsgBox("預算檔bgf010更新錯誤" & sqlstr)
        End If
        MsgBox("新增完成,請購編號＝" & strBgno)
        Call LoadGrid2Func()
    End Sub

    Private Sub btnModify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModify.Click
        Dim retstr, sqlstr As String
        Dim intNO As Integer
        If ValComa(txtUseAmt.Text) <= 0 Then
            MsgBox("金額不可為零或負數")
            Exit Sub
        End If
        Dim BYear, I As Integer '請購的預算年度
        Dim BAccno, Bkind, Accnokind As String '請購科目
        Dim Bamt As Decimal
        BYear = Mid(cboAccno.SelectedValue, 1, 3)
        BAccno = Trim(Mid(cboAccno.SelectedValue, 4, 17))
        Bkind = IIf(rdbKind1.Checked, "3", "4")
        Bamt = ValComa(txtUseAmt.Text)
        If Bkind = "3" Then  '借方
            If Mid(BAccno, 1, 1) = "3" Or Mid(BAccno, 1, 1) = "4" Then Bamt = -ValComa(txtUseAmt.Text)
        Else    '2負債科目亦同支出處理
            If Mid(BAccno, 1, 1) = "5" Or Mid(BAccno, 1, 1) = "2" Or Mid(BAccno, 1, 1) = "1" Then Bamt = -ValComa(txtUseAmt.Text)
        End If

        '資料處理(update BGF020 & BGF030)
        GenUpdsql("accyear", BYear, "N")
        GenUpdsql("accno", BAccno, "T")
        GenUpdsql("KIND", Bkind, "T")
        GenUpdsql("DC", IIf(Bkind = "3", "1", "2"), "T")
        GenUpdsql("REMARK", txtRemark.Text, "U")
        GenUpdsql("AMT1", Bamt, "N")
        GenUpdsql("useableAMT", Bamt, "N")
        sqlstr = "update BGF020 set " & GenUpdFunc & " where bgno='" & lblBgno.Text & "'"
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then
            MsgBox("修改失敗" + sqlstr)
            Exit Sub
        End If

        '資料處理update bgf030-
        GenUpdsql("USEAMT", Bamt, "N")
        GenUpdsql("REMARK", txtRemark.Text, "U")
        sqlstr = "update BGF030 set " & GenUpdFunc & " where bgno='" & lblBgno.Text & "'"
        retstr = runsql(mastconn, sqlstr)
        If retstr = "sqlok" Then
            MsgBox("作業完成,  " + strBGNo)
        Else
            MsgBox("資料update BGF030失敗" + sqlstr)
            Exit Sub
        End If


        '資料處理(UPDATE BGF010->TOTuse)
        '先還原舊資料
        If lblKind.Text = "3" Then   'debit 
            sqlstr = "update bgf010 set totuse = totuse - " & ValComa(txtUseAmt.Text) & _
                     " WHERE accyear=" & lblaccYear.Text & " AND ACCNO = '" & Trim(lblAccno.Text) & "'"
        Else
            sqlstr = "update bgf010 set totuse = totuse + " & ValComa(txtUseAmt.Text) & _
                    " WHERE accyear=" & lblaccYear.Text & " AND ACCNO = '" & Trim(lblAccno.Text) & "'"
        End If
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then
            MsgBox("預算檔bgf010更新錯誤" & sqlstr)
        End If

        '再update新資料
        'If rdbKind1.Checked Then  'debit 
        sqlstr = "update bgf010 set totuse = totuse + " & Bamt & _
                 " WHERE accyear=" & Str(BYear) & " AND ACCNO = '" & Trim(BAccno) & "'"
        'Else
        '    sqlstr = "update bgf010 set totuse = totuse - " & Trim(txtUseAmt.Text) & _
        '             " WHERE accyear=" & Str(BYear) & " AND ACCNO = '" & Trim(BAccno) & "'"
        'End If
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then
            MsgBox("預算檔bgf010更新錯誤" & sqlstr)
        End If
        Call LoadGrid2Func()
    End Sub


    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim retstr, sqlstr As String
        Dim intNO As Integer

        '資料處理delete bgf020 & bgf030
        sqlstr = "delete from BGF020 where bgno='" & lblBgno.Text & "'"
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then
            MsgBox("BGF020刪除失敗" + sqlstr)
            Exit Sub
        End If
        sqlstr = "delete from BGF030 where bgno='" & lblBgno.Text & "'"
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then
            MsgBox("BGF030刪除失敗" + sqlstr)
            Exit Sub
        End If
        sqlstr = "delete from BGF040 where bgcno='" & lblBgcno.Text & "' and bgno='" & lblBgno.Text & "'"
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then
            MsgBox("BGF040刪除失敗" + sqlstr)
            Exit Sub
        End If

        '資料處理(UPDATE BGF010->TOTuse)
        '還原舊資料
        If lblKind.Text = "3" Then   'debit 
            sqlstr = "update bgf010 set totuse = totuse - " & valcoma(txtUseAmt.Text) & _
                     " WHERE accyear=" & lblaccYear.Text & " AND ACCNO = '" & Trim(lblAccno.Text) & "'"
        Else
            sqlstr = "update bgf010 set totuse = totuse + " & ValComa(txtUseAmt.Text) & _
                    " WHERE accyear=" & lblaccYear.Text & " AND ACCNO = '" & Trim(lblAccno.Text) & "'"
        End If
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then
            MsgBox("預算檔bgf010更新錯誤" & sqlstr)
        End If
        Call LoadGrid2Func()
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Dim SumDebit, SumCredit As Decimal
        Dim sqlstr As String
        sqlstr = "select sum(useamt) as sumdebit from bgf040 where bgcno=" & Str(intBGCNO) & " inner join bgf030 on bgf040.bgno=bgf030.bgno and bgf030.kind='3'"
        TabControl1.SelectedIndex = 0  '退回
    End Sub

    Private Sub btnReflesh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReflesh.Click
        Call LoadGridFunc()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub txtUseAmt_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUseAmt.LostFocus
        If Not IsNumeric(sender.Text) And Trim(sender.text) <> "" Then
            MsgBox("請輸入數字")
            sender.Focus()
        Else
            sender.text = FormatNumber(ValComa(sender.text), 1)
        End If
    End Sub
End Class
