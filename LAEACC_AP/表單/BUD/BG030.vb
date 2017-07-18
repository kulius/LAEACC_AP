Public Class BG030
    Dim confid, confunit As String, LoadAfter, Dirty As Boolean
    Dim strBGNo As String    '請購編號
    Dim sYear, lastpos, sSeason As Integer  '請購年度
    Dim sqlstr As String
    Dim bm As BindingManagerBase, myDataSet, AccnoDataSet, TempDataSet As DataSet
    Dim UserId, UserName, UserUnit, UserDate As String

    Private Sub BG030_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        UserId = TransPara.TransP("userid")
        UserDate = TransPara.TransP("userDATE")
        UserUnit = TransPara.TransP("Userunit")
        sYear = GetYear(UserDate)
        sSeason = Season(UserDate)  '判定季份
        lblNoYear.Text = Format(GetYear(UserDate), "000")
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
        sqlstr = "SELECT a.bgno, a.accyear, a.accno, a.date1, a.amt1," & _
                 "a.amt2, a.amt3, a.useableamt, a.remark, a.kind, a.subject," & _
                 " CASE WHEN len(a.accno)=17 THEN b.accname+'('+c.accname+')'" & _
                 " WHEN len(a.accno)<>17 THEN b.accname END AS accname" & _
                 " FROM BGF020 a INNER JOIN ACCNAME b ON a.ACCNO = b.ACCNO" & _
                 " LEFT OUTER JOIN accname c ON left(a.accno,16)=c.accno and len(a.accno)=17"
        If Mid(UserUnit, 1, 2) = "05" Then   '主計人員
            sqlstr &= " WHERE (b.account_NO = '" & Trim(UserId) & "' OR b.account_NO = '')"
        Else                                 '業務單位人員
            sqlstr &= " WHERE b.STAFF_NO = '" & Trim(UserId) & "'"
        End If
        sqlstr &= " AND a.CLOSEMARK <> 'Y' and a.date2 is not null ORDER BY a.BGNO"

        myDataSet = openmember("", "BGF020", sqlstr)
        DataGrid1.DataSource = myDataSet
        DataGrid1.DataMember = "BGF020"
        bm = Me.BindingContext(myDataSet, "BGF020")
        TabControl1.SelectedIndex = 0
        txtNO.Text = ""
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
        lblYear.Text = bm.Current("accyear")
        lblAccno.Text = bm.Current("accno")
        lblAccname.Text = IIf(IsDBNull(bm.Current("accname")), " ", bm.Current("accname"))
        lblDate1.Text = nz(bm.Current("date1"), "")
        If bm.Current("kind") = "1" Then
            rdbKind1.Checked = True
        Else
            rdbkind2.Checked = True
        End If
        lblRemark.Text = nz(bm.Current("remark"), " ")
        lblAmt1.Text = FormatNumber(nz(bm.Current("amt1"), 0), 0)
        lblUseableAmt.Text = FormatNumber(nz(bm.Current("Useableamt"), 0), 0)  '可支用金額 maybe amt1,amt2 or amt3
        txtAmt2.Text = FormatNumber(nz(bm.Current("amt2"), 0), 0)
        lblAmt2.Text = FormatNumber(nz(bm.Current("amt2"), 0), 0)
        txtAmt3.Text = FormatNumber(nz(bm.Current("amt3"), 0), 0)
        lblAmt3.Text = FormatNumber(nz(bm.Current("amt3"), 0), 0)
        lblRemark.Text = nz(bm.Current("remark"), " ")
        lblkey.Text = Trim(bm.Current("bgno"))
        lblBgamt.Text = FormatNumber(QueryBGAmt(bm.Current("accyear"), bm.Current("accno")), 0)
        lblUnUseamt.Text = FormatNumber(QueryUnUseAmt(bm.Current("accyear"), bm.Current("accno"), sSeason), 0)
        If bm.Current("amt1") <> bm.Current("useableamt") Then lblUseAmt.Text = QueryUsedAmt(strBGNo)
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
        Dim strBGNO As String
        Dim i As Integer
        strBGNO = lblNoYear.Text & Format(Val(txtNO.Text), "00000")
        For i = 0 To bm.Count - 1
            bm.Position = i
            If bm.Current("bgno") = strBGNO Then
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

    Private Sub btnSure_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSure.Click
        If ValComa(txtAmt2.Text) = 0 And ValComa(txtAmt3.Text) = 0 Then Exit Sub
        Dim sqlstr, retstr, updstr, strAmt As String
        Dim tamt As Decimal
        If ValComa(lblAmt2.Text) = ValComa(txtAmt2.Text) And ValComa(lblAmt3.Text) = ValComa(txtAmt3.Text) Then  '金額沒變
            TabControl1.SelectedIndex = 0
            Exit Sub
        End If
        If ValComa(lblAmt2.Text) <> ValComa(txtAmt2.Text) Then GenUpdsql("amt2", txtAmt2.Text, "N")
        If ValComa(lblAmt3.Text) <> ValComa(txtAmt3.Text) Then GenUpdsql("amt3", txtAmt3.Text, "N")
        If ValComa(txtAmt3.Text) <> 0 Then   '先Check 變更金額  because 先有發包才有變更
            GenUpdsql("useableamt", txtAmt3.Text, "N")
            strAmt = txtAmt3.Text
        Else
            If ValComa(txtAmt2.Text) <> 0 Then
                GenUpdsql("useableamt", txtAmt2.Text, "N")
                strAmt = txtAmt2.Text
            Else
                GenUpdsql("useableamt", lblAmt1.Text, "N")
                strAmt = lblAmt1.Text
            End If
        End If
        sqlstr = "update BGF020 set " & GenUpdFunc & " where bgno='" & strBGNo & "'"
        retstr = runsql(mastconn, sqlstr)
        If retstr = "sqlok" Then
            Call LoadGridFunc()
        Else
            MsgBox("更新失敗" & sqlstr)
        End If

        'check 可支用金額不相同時要update BGF010->totper
        If Val(lblUseableAmt.Text) <> Val(strAmt) Then
            sqlstr = "update bgf010 set totper = totper - " & ValComa(lblUseableAmt.Text) & " + " & ValComa(strAmt) & _
                     " WHERE accyear=" & Trim(lblYear.Text) & " AND ACCNO = '" & Trim(lblAccno.Text) & "'"
            retstr = runsql(mastconn, sqlstr)
            If retstr <> "sqlok" Then
                MsgBox("預算檔bgf010更新錯誤" & sqlstr)
            End If
        End If
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        TabControl1.SelectedIndex = 0  '退回
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub txtAmt2_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAmt2.LostFocus, txtAmt3.LostFocus
        If Not IsNumeric(sender.Text) And Trim(sender.text) <> "" Then
            MsgBox("請輸入數字")
            sender.Focus()
        Else
            sender.text = FormatNumber(ValComa(sender.text), 0)
        End If
    End Sub
    Private Sub txtno_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNO.LostFocus
        If Not IsNumeric(sender.Text) And Trim(sender.text) <> "" Then
            MsgBox("請輸入數字")
            sender.Focus()
        End If
    End Sub
End Class
