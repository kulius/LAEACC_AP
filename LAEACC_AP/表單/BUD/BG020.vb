Public Class BG020
    Dim confid, confunit As String, LoadAfter, Dirty As Boolean
    Dim strBGNo As String    '請購編號
    Dim sYear, lastpos, sSeason As Integer  '請購年度
    Dim sqlstr As String
    Dim bm, bm0 As BindingManagerBase, myDataSet, myDataSet0, mkDataSet, AccnoDataSet, TempDataSet As DataSet
    Dim UserId, UserName, UserUnit, UserDate As String
    Dim direty As Boolean = False '定義直接輸入請購編號

    Private Sub BG020_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        UserId = TransPara.TransP("userid")
        UserDate = TransPara.TransP("userDATE")
        sYear = GetYear(UserDate)
        sSeason = Season(UserDate)  '判定季份
        lblNoYear.Text = Format(GetYear(UserDate), "000")
        LoadAfter = True
        TabControl1.Enabled = True
        If TransPara.TransP("UnitTitle").indexof("中") >= 0 Then
            direty = True  '台中要直接輸入請購編號
        End If
        If TransPara.TransP("UnitTitle").indexof("嘉南") < 0 Then
            lblMark.Visible = False    '嘉南才要顯示憑證補辦攔位
            txtMark.Visible = False
            cboMark.Visible = False
        Else
            '將憑證補辦片語置combobox   
            sqlstr = "SELECT psstr  FROM psname where unit='mark' order by seq"
            mkDataSet = openmember("", "psname", sqlstr)
            If mkDataSet.Tables("psname").Rows.Count = 0 Then
                cbomark.Text = "尚無片語"
            Else
                cbomark.DataSource = mkDataSet.Tables("psname")
                cbomark.DisplayMember = "psstr"  '顯示欄位
                cbomark.ValueMember = "psstr"     '欄位值
                cboMark.SelectionLength = 20
            End If
        End If
        If direty = False Then
            Call LoadGrid0Func()
        Else
            LoadGridFunc()
        End If

        'If DataGrid0.CurrentRowIndex > 0 Then
        '    bm0.Position = 1
        '    Call PutGridToTxt()
        'End If
    End Sub

    Sub LoadGrid0Func()
        Dim sqlstr, qstr, strD, strC As String
        sqlstr = "SELECT a.*, " & _
                 "CASE WHEN len(a.accno)=17 THEN b.accname+'('+c.accname+')' " & _
                     " WHEN len(a.accno)<>17 THEN b.accname END AS accname " & _
                 "FROM (select accyear, accno FROM BGF020 WHERE closemark <> 'Y' AND date2 IS NULL " & _
                 "GROUP BY accyear, accno) a " & _
                 "LEFT OUTER JOIN ACCNAME b ON a.ACCNO = b.ACCNO " & _
                 " LEFT OUTER JOIN accname c ON left(a.accno,16)=c.accno and len(a.accno)=17 " & _
                 "WHERE (b.ACCOUNT_NO = '" & Trim(UserId) & "' OR b.ACCOUNT_NO = '')" & _
                 "ORDER BY  a.ACCYEAR, a.ACCNO"
        myDataSet0 = openmember("", "BGF010", sqlstr)
        DataGrid0.DataSource = myDataSet0
        DataGrid0.DataMember = "BGF010"
        bm0 = Me.BindingContext(myDataSet0, "BGF010")
        TabControl1.SelectedIndex = 0  '94/2/5
        txtNO.Text = ""
        txtNO.Focus()
    End Sub

    Sub LoadGridFunc()
        Dim sqlstr, qstr, strD, strC As String
        If direty = True Then   '直接輸入請購編號 要全部置datagrid 
            sqlstr = "SELECT a.bgno, a.accyear, a.accno, a.date1, a.amt1, a.useableamt, a.remark, a.kind,a.subject," & _
                     "CASE WHEN len(a.accno)=17 THEN b.accname+'('+c.accname+')' " & _
                         " WHEN len(a.accno)<>17 THEN b.accname END AS accname " & _
                     "FROM BGF020 a INNER JOIN ACCNAME b ON a.ACCNO = b.ACCNO" & _
                     " LEFT OUTER JOIN accname c ON left(a.accno,16)=c.accno and len(a.accno)=17" & _
                     " WHERE b.account_NO = '" & Trim(UserId) & "' AND a.CLOSEMARK <> 'Y' and a.date2 is null" & _
                     " ORDER BY a.accno, a.bgno"
        Else   '要分科目置datagrid 
            sqlstr = "SELECT a.bgno, a.accyear, a.accno, a.date1, a.amt1, a.useableamt, a.remark, a.kind,a.subject," & _
                     "CASE WHEN len(a.accno)=17 THEN b.accname+'('+c.accname+')' " & _
                         " WHEN len(a.accno)<>17 THEN b.accname END AS accname " & _
                     "FROM BGF020 a INNER JOIN ACCNAME b ON a.ACCNO = b.ACCNO" & _
                     " LEFT OUTER JOIN accname c ON left(a.accno,16)=c.accno and len(a.accno)=17" & _
                     " WHERE a.accyear=" & bm0.Current("accyear") & " and a.accno='" & bm0.Current("accno") & _
                     "' AND a.CLOSEMARK <> 'Y' and a.date2 is null" & _
                     " ORDER BY a.accno, a.bgno"
        End If
        myDataSet = openmember("", "BGF020", sqlstr)
        DataGrid1.DataSource = myDataSet
        DataGrid1.DataMember = "BGF020"
        bm = Me.BindingContext(myDataSet, "BGF020")
        TabControl1.SelectedIndex = 1  '94/2/5
        txtNO.Text = ""
        txtNO.Focus()
    End Sub

    Sub PutGridToTxt()
        Dim intI, SumUp As Integer
        Dim strI, strColumn1, strColumn2 As String
        If bm.Position < 0 Then Exit Sub
        If IsDBNull(bm.Current("accno")) Then Exit Sub
        lastpos = bm.Position
        lblBgno.Text = nz(bm.Current("bgno"), " ")  '不允許修改bgno,accyear,accno
        strBGNo = nz(bm.Current("BGNO"), " ")
        lblYear.Text = nz(bm.Current("accyear"), 0)
        lblAccno.Text = nz(bm.Current("accno"), " ")
        lblAccname.Text = IIf(IsDBNull(bm.Current("accname")), " ", bm.Current("accname"))
        lblDate1.Text = nz(bm.Current("date1"), "")
        If bm.Current("kind") = "1" Then
            rdbKind1.Checked = True
        Else
            rdbkind2.Checked = True
        End If
        txtRemark.Text = nz(bm.Current("remark"), " ")
        lblRemark.Text = nz(bm.Current("remark"), " ")
        txtAmt1.Text = FormatNumber(nz(bm.Current("useableamt"), 1), 0)  '審核要以可支用金額,because 可能有一次請購多次開支
        lblUseableAmt.Text = FormatNumber(nz(bm.Current("useableamt"), 0), 0)
        lblAmt1.Text = FormatNumber(nz(bm.Current("amt1"), 0), 0)
        txtSubject.Text = nz(bm.Current("SUBJECT"), " ")
        lblSubject.Text = nz(bm.Current("subject"), " ")
        txtMark.Text = ""  '憑證應補事項
        lblkey.Text = nz(Trim(bm.Current("bgno")), " ")
        If Trim(lblkey.Text) <> "" Then
            lblBgamt.Text = FormatNumber(QueryBGAmt(bm.Current("accyear"), bm.Current("accno")), 0)
            lblunUseamt.Text = FormatNumber(QueryUnUseAmt(bm.Current("accyear"), bm.Current("accno"), sSeason), 0)  '當季開支餘額
        End If
        lblGrade6.Text = ""
    End Sub

    Private Sub TabControl1_SELECTEDIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        'If LoadAfter = False Then Exit Sub
        If TabControl1.SelectedIndex = 2 Then   '94/2/5
            If bm.Position >= 0 Then
                Call DataGrid1_DoubleClick(New System.Object, New System.EventArgs)
            End If
        End If
    End Sub
    Private Sub DataGrid0_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataGrid0.DoubleClick
        If bm0.Position < 0 Then Exit Sub
        lblkey.Text = nz(bm0.Current("accno"), "") 'keep the old keyvalue
        If lblkey.Text <> "" Then
            Call LoadGridFunc()
            TabControl1.SelectedIndex = 1  '至明細清單
        End If
    End Sub
    Private Sub DataGrid1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataGrid1.DoubleClick
        If bm.Position < 0 Then Exit Sub
        lblkey.Text = nz(bm.Current("bgno"), "") 'keep the old keyvalue
        If lblkey.Text <> "" Then
            Call PutGridToTxt()
            TabControl1.SelectedIndex = 2  'go to the detail screen 
        End If
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
                TabControl1.SelectedIndex = 2  'go to the detail screen 
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

    Private Sub btnSure_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSure.Click
        If ValComa(txtAmt1.Text) = 0 Then Exit Sub
        Dim strRemark As String
        strRemark = Trim(txtRemark.Text)
        'If strRemark.IndexOf("設施") <= -1 And (strRemark.IndexOf("旅費") > -1 Or _
        '   strRemark.IndexOf("影印") > -1 Or strRemark.IndexOf("薪") > -1 Or _
        '   strRemark.IndexOf("獎金") > -1 Or strRemark.IndexOf("退休金") > -1 Or _
        '   strRemark.IndexOf("辦公費") > -1 Or strRemark.IndexOf("值日") > -1 Or _
        '   strRemark.IndexOf("勞保") > -1 Or strRemark.IndexOf("電費") > -1) Then
        '補助費不設為直接開支
        If strRemark.IndexOf("小組辦公") > -1 Or strRemark.IndexOf("旅費") > -1 Or _
           strRemark.IndexOf("影印") > -1 Or strRemark.IndexOf("薪") > -1 Or _
           strRemark.IndexOf("獎金") > -1 Or strRemark.IndexOf("退休金") > -1 Or _
           strRemark.IndexOf("辦公費") > -1 Or strRemark.IndexOf("值日") > -1 Or _
           strRemark.IndexOf("生育補助") > -1 Or strRemark.IndexOf("結婚補助") > -1 Or _
           strRemark.IndexOf("教育補助") > -1 Or strRemark.IndexOf("殮葬補助") > -1 Or _
           strRemark.IndexOf("休假") > -1 Or strRemark.IndexOf("喪葬補助") > -1 Or _
           strRemark.IndexOf("勞保") > -1 Or strRemark.IndexOf("電費") > -1 Then
            If MsgBox("此筆應為直接開支 確定嗎? (不直接開支請選NO)", MsgBoxStyle.DefaultButton1 + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Call DirectUse()    '主計直接開支
            Else
                Call Account_Check1() '主計請購登計
            End If
        Else
            Call Account_Check1()  '主計請購登計
        End If
        'bm.Position = 1
        TabControl1.SelectedIndex = 1   '回第二層
        txtNO.Text = ""
        txtNO.Focus()
    End Sub

    Private Sub btnDirect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDirect.Click
        If MsgBox("確定要直接開支嗎?)", MsgBoxStyle.DefaultButton1 + MsgBoxStyle.YesNo) <> MsgBoxResult.Yes Then
            Exit Sub
        End If

        If Val(txtAmt1.Text) = 0 Then Exit Sub
        Call DirectUse()    '主計直接開支
        TabControl1.SelectedIndex = 1   '回第二層
        txtNO.Text = ""
        txtNO.Focus()
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        TabControl1.SelectedIndex = 0  '退回
    End Sub

    Sub Account_Check1()   '主計請購登記
        Dim sqlstr, retstr, updstr, TDC As String

        '更新請購檔BGF020
        If Trim(lblRemark.Text) <> Trim(txtRemark.Text) Then GenUpdsql("remark", txtRemark.Text, "U")
        If ValComa(lblUseableAmt.Text) <> ValComa(txtAmt1.Text) Then
            GenUpdsql("amt1", txtAmt1.Text, "N")      '以主計審核金額當請購金額
            GenUpdsql("useableamt", txtAmt1.Text, "N") '以主計審核金額當可支用金額
        End If
        GenUpdsql("date2", UserDate, "D")  '以LOGIN日期當審核日
        If Trim(lblSubject.Text) <> Trim(txtSubject.Text) Then GenUpdsql("SUBJECT", txtSubject.Text, "U")
        sqlstr = "update BGF020 set " & GenUpdFunc & " where bgno='" & strBGNo & "'"
        retstr = runsql(mastconn, sqlstr)
        If retstr = "sqlok" Then
            myDataSet.Tables("BGF020").Rows.RemoveAt(lastpos)   '自datagrid中刪除
        Else
            MsgBox("更新失敗" & sqlstr)
        End If

        'check 請購金額不相同 update bgf010 
        If ValComa(lblUseableAmt.Text) <> ValComa(txtAmt1.Text) Then
            sqlstr = "update bgf010 set totper = totper - " & ValComa(lblUseableAmt.Text) & " + " & ValComa(txtAmt1.Text) & _
                     " WHERE accyear=" & Trim(lblYear.Text) & " AND ACCNO = '" & Trim(lblAccno.Text) & "'"
            retstr = runsql(mastconn, sqlstr)
            If retstr <> "sqlok" Then
                MsgBox("預算檔bgf010更新錯誤" & lblYear.Text & "  " & lblAccno.Text)
            End If
        End If
    End Sub

    Sub DirectUse()  '主計直接開支
        Dim keyvalue, sqlstr, retstr, updstr, skind, TDC, sDC, J As String
        Dim BYear, SumBg, I, Tamt, intRel As Integer '請購的預算年度
        Dim BAccno, strClose As String '請購科目
        Dim sumRtot As Decimal
        If Mid(lblAccno.Text, 1, 1) <> "4" Then
            If ValComa(txtAmt1.Text) > 0 And ValComa(lblunUseamt.Text) - ValComa(txtAmt1.Text) < 0 And TransPara.TransP("flow") <> "Y" Then  '不可溢支
                MsgBox("當季開支餘額不足,請退回")
                Exit Sub
            End If
            If ValComa(txtAmt1.Text) > 0 And ValComa(lblAmt1.Text) < ValComa(txtAmt1.Text) Then
                MsgBox("開支數不可大於請購數,請修正")
                Exit Sub
            End If
        End If
        If Mid(Trim(lblAccno.Text), 1, 5) = "21302" And TransPara.TransP("flow") <> "Y" Then   '代收款還要檢查已收入金額是否足夠
            sqlstr = "SELECT sum(amt) as amt  FROM BGF050 WHERE left(accno,16)='" & Mid(Trim(lblAccno.Text), 1, 16) & "'"
            TempDataSet = openmember("", "TEMP", sqlstr)
            sumRtot = nz(TempDataSet.Tables("temp").Rows(0).Item(0), 0)
            sqlstr = "SELECT sum(totuse) as amt2  FROM BGF010 WHERE accyear=" & Trim(lblYear.Text) & " and left(accno,16)='" & Mid(Trim(lblAccno.Text), 1, 16) & "'"
            TempDataSet = openmember("", "TEMP", sqlstr)
            If sumRtot - nz(TempDataSet.Tables("temp").Rows(0).Item(0), 0) - ValComa(txtAmt1.Text) < 0 Then
                MsgBox("代收款收入不足開支,請退回")
                Exit Sub
            End If
        End If

        strClose = "Y"  '已開支完畢   (直接開支不可再分次開支)
        'If Val(txtAmt1.Text) < Val(lblUseableAmt.Text) Then
        '    If MsgBox("是否要將請購餘額留待下次開支?", MsgBoxStyle.DefaultButton1 + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
        '        strClose = "N"   '尚可開支
        '    End If
        'End If

        '資料處理(UPDATE BGF010->TOTuse,totPER)
        If strClose = "Y" Then
            sqlstr = "update bgf010 set totper = totper - " & ValComa(lblUseableAmt.Text)
        Else
            sqlstr = "update bgf010 set totper = totper - " & ValComa(txtAmt1.Text)
        End If
        sqlstr = sqlstr & ", totuse = totuse + " & ValComa(txtAmt1.Text) & " where accyear=" & Trim(lblYear.Text) & " AND ACCNO = '" & Trim(lblAccno.Text) & "'"
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then
            MsgBox("預算檔bgf010更新錯誤" & lblYear.Text & "  " & lblAccno.Text)
        End If

        '資料處理(UPDATE BGF020->date2)
        'GenUpdsql("amt1", txtAmt1.Text, "N")  '以主計審核金額當請購金額
        If Trim(lblRemark.Text) <> Trim(txtRemark.Text) Then GenUpdsql("remark", txtRemark.Text, "U")
        If Trim(lblSubject.Text) <> Trim(txtSubject.Text) Then GenUpdsql("SUBJECT", txtSubject.Text, "U")
        GenUpdsql("date2", UserDate, "D")  '以LOGIN日期當審核日
        If strClose = "Y" Then
            GenUpdsql("closemark", "Y", "T")
        End If
        sqlstr = "update BGF020 set " & GenUpdFunc & " where bgno='" & strBGNo & "'"
        retstr = runsql(mastconn, sqlstr)
        If retstr = "sqlok" Then
            myDataSet.Tables("BGF020").Rows.RemoveAt(lastpos)   '自datagrid中刪除
        Else
            MsgBox("更新失敗" & sqlstr)
        End If

        '資料處理(新增一筆至BGF030)
        sqlstr = "SELECT max(REL) as rel  FROM BGF030 WHERE BGNO='" & strBGNo & "'"
        TempDataSet = openmember("", "TEMP", sqlstr)

        intRel = 0
        If Not IsDBNull(TempDataSet.Tables("TEMP").Rows(0).Item(0)) Then
            intRel = TempDataSet.Tables("TEMP").Rows(0).Item(0)
        End If
        intRel += 1
        TempDataSet = Nothing
        sqlstr = GenInsFunc          'clear the insert fields 
        GenInsSql("BGNO", strBGNo, "T")
        GenInsSql("rel", intRel, "N")
        GenInsSql("date3", UserDate, "D")
        GenInsSql("date4", UserDate, "D")
        GenInsSql("USEAMT", txtAmt1.Text, "N")
        GenInsSql("REMARK", txtRemark.Text, "U")
        If Len(Trim(txtMark.Text)) > 0 Then
            GenInsSql("MARK", txtMark.Text, "U")
        End If
        GenInsSql("NO_1_NO", 0, "N")
        sqlstr = "insert into BGF030 " & GenInsFunc
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then
            MsgBox("資料寫入BGF030失敗" & sqlstr)
        End If
    End Sub

    Private Sub btnBack0_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack0.Click
        TabControl1.SelectedIndex = 0
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub txtAmt1_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAmt1.LostFocus
        If Not IsNumeric(sender.Text) And Trim(sender.text) <> "" Then
            MsgBox("請輸入數字")
            sender.Focus()
        Else
            sender.text = FormatNumber(ValComa(sender.text), 0)
        End If
    End Sub

    Private Sub txtno_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNO.LostFocus
        If (Not IsNumeric(sender.Text) Or ValComa(sender.text) < 0) And Trim(sender.text) <> "" Then
            MsgBox("請輸入正數字")
            sender.Focus()
        End If
    End Sub

    Private Sub btnGrade6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrade6.Click
        '六級餘額查詢   this function=bg010.vb 
        Dim BYear, I As Integer '請購的預算年度
        Dim BAccno As String '請購科目
        BYear = lblYear.Text
        BAccno = lblAccno.Text
        If Len(BAccno) >= 7 Then
            Dim tempdataset As DataSet
            Dim sqlstr As String
            sqlstr = "SELECT sum(bg1+bg2+bg3+bg4+up1+up2+up3+up4-totper-totuse) as balance FROM bgf010 " & _
                     " WHERE accyear=" & BYear & " and left(accno,9)='" & Mid(BAccno, 1, 9) & "'"
            tempdataset = openmember("", "temp", sqlstr)
            If tempdataset.Tables("temp").Rows.Count = 0 Then
                lblGrade6.Text = "0"
            Else
                lblGrade6.Text = FormatNumber(tempdataset.Tables("temp").Rows(0).Item(0), 0)
            End If
        Else
            lblGrade6.Text = "請選擇大於六級科目"
        End If
    End Sub

    Private Sub cbomark_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboMark.SelectionChangeCommitted
        txtMark.Text = cboMark.Text
    End Sub
End Class
