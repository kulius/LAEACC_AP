Public Class BG050
    Dim confid, confunit As String, LoadAfter, Dirty As Boolean
    Dim strBGNo As String    '請購編號
    Dim sYear, lastpos, sSeason, intRel As Integer  '請購年度

    Dim sqlstr As String
    Dim bm As BindingManagerBase, myDataSet, AccnoDataSet, mkDataSet, TempDataSet As DataSet
    Dim UserId, UserName, UserUnit, UserDate As String

    Private Sub BG050_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        UserId = TransPara.TransP("userid")
        UserDate = TransPara.TransP("userDATE")
        sYear = GetYear(UserDate)
        sSeason = Season(UserDate)  '判定季份
        lblNoYear.Text = Format(GetYear(UserDate), "000")
        LoadAfter = True
        TabControl1.Enabled = True
        If TransPara.TransP("UnitTitle").indexof("嘉南") < 0 Then
            lblMark.Visible = False    '嘉南才要顯示憑證補辦攔位
            txtMark.Visible = False
            cboMark.Visible = False
        Else
            '將憑證補辦片語置combobox   
            sqlstr = "SELECT psstr  FROM psname where unit='mark' order by seq"
            mkDataSet = openmember("", "psname", sqlstr)
            If mkDataSet.Tables("psname").Rows.Count = 0 Then
                cboMark.Text = "尚無片語"
            Else
                cboMark.DataSource = mkDataSet.Tables("psname")
                cboMark.DisplayMember = "psstr"  '顯示欄位
                cboMark.ValueMember = "psstr"     '欄位值
                cboMark.SelectionLength = 20
            End If
        End If
        Call LoadGridFunc()
        If bm.Count > 0 Then
            bm.Position = 0
        End If
        txtNO.Focus()
    End Sub

    Sub LoadGridFunc()
        Dim sqlstr, qstr, strD, strC As String
        sqlstr = "SELECT a.BGNO, a.REL, b.ACCYEAR, b.ACCNO, b.DATE1, a.DATE3, a.USEAMT, a.remark," & _
                 " b.date2, a.autono, b.kind, b.AMT1, b.amt2, b.amt3, b.useableamt, b.subject," & _
                 " CASE WHEN len(b.accno)=17 THEN c.accname+'('+d.accname+')'" & _
                     " WHEN len(b.accno)<>17 THEN c.accname END AS accname" & _
                 " FROM BGF030 a INNER JOIN BGF020 b ON a.BGNO = b.BGNO INNER JOIN ACCNAME c ON b.ACCNO = c.ACCNO " & _
                 " LEFT OUTER JOIN accname d ON left(b.accno,16)=d.accno and len(b.accno)=17" & _
                 " WHERE (c.account_NO = '" & Trim(UserId) & "' OR c.account_no = '') AND a.date4 is null" & _
                 " ORDER BY b.accno, a.BGNO"
        myDataSet = openmember("", "bgf020", sqlstr)
        DataGrid1.DataSource = myDataSet
        DataGrid1.DataMember = "bgf020"
        bm = Me.BindingContext(myDataSet, "bgf020")
        TabControl1.SelectedIndex = 0
        txtNO.Focus()
    End Sub

    Sub PutGridToTxt()
        Dim intI, SumUp As Integer
        Dim strI, strColumn1, strColumn2 As String
        If bm.Position < 0 Then Exit Sub
        If IsDBNull(bm.Current("bgno")) Then Exit Sub
        lastpos = bm.Position
        lblBgno.Text = bm.Current("bgno")   '不允許修改bgno,accyear,accno
        strBGNo = bm.Current("BGNO")
        intRel = bm.Current("rel")
        lblYear.Text = bm.Current("accyear")
        lblAccno.Text = bm.Current("accno")
        lblAccname.Text = IIf(IsDBNull(bm.Current("accname")), " ", bm.Current("accname"))
        lblDate1.Text = nz(bm.Current("date1"), "")
        lblDate2.Text = nz(bm.Current("date2"), "")
        lblDate3.Text = nz(bm.Current("date3"), "")
        If bm.Current("kind") = "1" Then
            rdbKind1.Checked = True
        Else
            rdbkind2.Checked = True
        End If
        txtRemark.Text = nz(bm.Current("remark"), " ")
        lblRemark.Text = txtRemark.Text  '用來判定remark是否有異動,要update remark
        txtUseAmt.Text = FormatNumber(nz(bm.Current("useamt"), 0), 0)
        lblUseAmt.Text = FormatNumber(nz(bm.Current("useamt"), 0), 0)
        lblUseableAmt.Text = FormatNumber(nz(bm.Current("useableamt"), 0), 0)
        lblAmt1.Text = FormatNumber(nz(bm.Current("amt1"), 0), 0)
        lblAmt2.Text = FormatNumber(nz(bm.Current("amt2"), 0), 0)
        lblAmt3.Text = FormatNumber(nz(bm.Current("amt3"), 0), 0)
        txtSubject.Text = nz(bm.Current("SUBJECT"), " ")
        lblSubject.Text = nz(bm.Current("subject"), " ") '用來判定subject是否有異動,要update subject
        lblMark.Text = ""   '憑證應補事項
        lblkey.Text = Trim(bm.Current("autono"))
        lblBgamt.Text = FormatNumber(QueryBGAmt(bm.Current("accyear"), bm.Current("accno")), 0)
        lblunUseamt.Text = FormatNumber(QueryUnUseAmt(bm.Current("accyear"), bm.Current("accno"), sSeason) + ValComa(lblUseAmt.Text), 0)
        lblUsedAmt.Text = FormatNumber(QueryUsedAmt(strBGNo) - ValComa(lblUseAmt.Text), 0) '已開支(要扣除此筆開支)
        lblGrade6.Text = ""
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
        Dim strBGNO2 As String
        Dim i As Integer
        strBGNO2 = lblNoYear.Text & Format(Val(txtNO.Text), "00000")
        For i = 0 To bm.Count - 1
            bm.Position = i
            If bm.Current("bgno") = strBGNO2 Then
                Call PutGridToTxt()
                TabControl1.SelectedIndex = 1  'go to the detail screen 
                txtNO.Text = ""
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
        If ValComa(txtUseAmt.Text) = 0 Then Exit Sub
        Dim retstr, sqlstr As String
        If ValComa(txtUseAmt.Text) > ValComa(lblUseAmt.Text) And Mid(lblAccno.Text, 1, 1) <> "4" Then  '主計開支金額>業務單位開支金額
            If ValComa(txtUseAmt.Text) - ValComa(lblUseAmt.Text) > (ValComa(lblUseableAmt.Text) - ValComa(lblUsedAmt.Text)) Then
                MsgBox("開支金額超過請購金額,請退回")
                Exit Sub
            End If
        End If

        '資料處理updte bgf030->date4
        GenUpdsql("date4", UserDate, "D")
        If txtRemark.Text <> lblRemark.Text Then GenUpdsql("remark", txtRemark.Text, "U")
        If ValComa(lblUseAmt.Text) <> ValComa(txtUseAmt.Text) Then GenUpdsql("useamt", txtUseAmt.Text, "N")
        If Len(Trim(txtMark.Text)) > 0 Then
            GenUpdsql("MARK", txtMark.Text, "U")
        End If
        sqlstr = "update BGF030 set " & GenUpdFunc & " where autono=" & lblkey.Text
        retstr = runsql(mastconn, sqlstr)
        If retstr = "sqlok" Then
            myDataSet.Tables("BGF020").Rows.RemoveAt(lastpos)
        Else
            MsgBox("更新bgf030失敗" & sqlstr)
        End If

        '資料處理updte bgf020->subject
        If lblSubject.Text <> txtSubject.Text Then
            sqlstr = "update BGF020 set subject = '" & txtSubject.Text & "' where bgno='" & strBGNo & "'"
            retstr = runsql(mastconn, sqlstr)
            If retstr <> "sqlok" Then
                MsgBox("更新bgf020受款人欄失敗" & sqlstr)
            End If
        End If

        '資料處理(UPDATE BGF010->TOTuse) 
        If ValComa(txtUseAmt.Text) <> ValComa(lblUseAmt.Text) Then  '主計開支金額<>業務單位開支金額
            sqlstr = "update bgf010 set totuse = totuse + " & ValComa(txtUseAmt.Text) & " - " & ValComa(lblUseAmt.Text) & _
                     " WHERE accyear=" & Trim(lblYear.Text) & " AND ACCNO = '" & Trim(lblAccno.Text) & "'"
            retstr = runsql(mastconn, sqlstr)
            If retstr <> "sqlok" Then
                MsgBox("預算檔bgf010更新錯誤" & sqlstr)
            End If
        End If
        TabControl1.SelectedIndex = 0
        txtNO.Focus()
    End Sub

    Private Sub btnReturn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReturn.Click '退回業務單位 
        '退件:退至主計請購審核完成階段(業務單位未開支)
        Dim keyvalue, sqlstr, retstr, updstr As String
        Dim BYear, intRel As Integer '請購的預算年度
        Dim BAccno, strClose As String '請購科目

        '資料處理(UPDATE BGF010->TOTuse要扣除開支數,totPER要加上請購數)
        sqlstr = "update bgf010 set totper = totper + " & ValComa(lblAmt1.Text) & _
                 ", totuse = totuse - " & ValComa(lblUseAmt.Text) & _
                 " WHERE accyear=" & Trim(lblYear.Text) & " AND ACCNO = '" & Trim(lblAccno.Text) & "'"
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then
            MsgBox("預算檔bgf010更新錯誤" & sqlstr)
        End If

        '資料處理(UPDATE BGF020->closemark<>'Y' 表示未支畢)
        sqlstr = "update BGF020 set closemark = '' where bgno='" & strBGNo & "'"
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then
            MsgBox("更新BGF020失敗" & sqlstr)
        End If

        '資料處理(刪除BGF030,刪除開支資料)
        sqlstr = "DELETE FROM BGF030 WHERE autono=" & lblkey.Text
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then
            MsgBox("BGF030資料刪除失敗" + sqlstr)
        End If

        myDataSet.Tables("BGF020").Rows.RemoveAt(lastpos)
        TabControl1.SelectedIndex = 0
        txtNO.Focus()
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        TabControl1.SelectedIndex = 0  '回上畫面
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub txtuseamt_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUseAmt.LostFocus
        If Not IsNumeric(sender.Text) And Trim(sender.text) <> "" Then
            MsgBox("請輸入數字")
            sender.Focus()
        Else
            sender.text = FormatNumber(ValComa(sender.text), 0)
        End If
    End Sub

    Private Sub txtNO_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNO.LostFocus
        If (Not IsNumeric(sender.Text) Or ValComa(sender.text) < 0) And Trim(sender.text) <> "" Then
            MsgBox("請輸入數字")
            sender.Focus()
        End If
    End Sub

    Private Sub btnGrade6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrade6.Click
        '六級開支餘額查詢  
        Dim BYear, I As Integer '請購的預算年度
        Dim BAccno As String '請購科目
        BYear = lblYear.Text
        BAccno = lblAccno.Text
        If Len(BAccno) >= 7 Then
            Dim tempdataset As DataSet
            Dim sqlstr As String
            If sSeason = 1 Then
                sqlstr = "SELECT sum(bg1+up1-totuse) as balance FROM bgf010 "
            End If
            If sSeason = 2 Then
                sqlstr = "SELECT sum(bg1+bg2+up1+up2-totuse) as balance FROM bgf010 "
            End If
            If sSeason = 3 Then
                sqlstr = "SELECT sum(bg1+bg2+bg3+up1+up2+up3-totuse) as balance FROM bgf010 "
            End If
            If sSeason = 4 Then
                sqlstr = "SELECT sum(bg1+bg2+bg3+bg4+up1+up2+up3+up4-totuse) as balance FROM bgf010 "
            End If
            sqlstr += " WHERE accyear=" & BYear & " and left(accno,9)='" & Mid(BAccno, 1, 9) & "'"
            tempdataset = openmember("", "temp", sqlstr)
            If tempdataset.Tables("temp").Rows.Count = 0 Then
                lblGrade6.Text = "0"
            Else
                '要加回本筆開支數
                lblGrade6.Text = FormatNumber(tempdataset.Tables("temp").Rows(0).Item(0) + ValComa(txtUseAmt.Text), 0)
            End If
        Else
            lblGrade6.Text = "請選擇大於六級科目"
        End If
    End Sub

    Private Sub cbomark_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboMark.SelectionChangeCommitted
        txtMark.Text = cboMark.Text
    End Sub
End Class
