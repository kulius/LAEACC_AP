Public Class AC210
    Dim skind As String
    Dim sdate As DateTime
    Dim sYear As Integer
    Dim myds As DataSet
    Private Sub AC210_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim sqlstr As String
        '先由chf020找目前收付款日   
        sqlstr = "SELECT date_2 FROM chf020 where date_2 is not null"
        myds = openmember("", "chf020", sqlstr)
        If myds.Tables("chf020").Rows.Count > 0 Then
            sDate = myds.Tables("chf020").Rows(0).Item("date_2")  '目前收付款日
        Else
            MsgBox("目前無收付款日,本功能只提供當日之帳務修改")
            Me.Close()
        End If
        sYear = GetYear(sdate)   '年度
    End Sub

    Private Sub btnChkno_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSureNo.Click
        If Trim(txtNo2.Text) = "" Then
            MsgBox("請輸入傳票")
            txtNo2.Focus()
            Exit Sub
        End If
        Dim sqlstr As String
        skind = IIf(rdbKind1.Checked, "1", "2")
        sqlstr = "select date_2, no_1_no, remark, amt, act_amt, bank, chkno, remark from acf010 where kind='" & skind & "' and item='9' and accyear=" & _
          sYear & " and no_2_no=" & Trim(txtNo2.Text)
        myds = openmember("", "acf010t", sqlstr)
        If myds.Tables("acf010t").Rows.Count = 0 Then
            MsgBox("無此傳票")
            txtNo2.Text = ""
            txtNo2.Focus()
            Exit Sub
        End If
        If FullDate(myds.Tables("acf010t").Rows(0).Item("date_2")) <> FullDate(sDate) Then
            MsgBox("此功能只提供當日之帳務修改,目前收付款日=" & sDate & ", 本傳票收付日期" & myds.Tables("acf010t").Rows(0).Item("date_2"))
            txtNo2.Text = ""
            txtNo2.Focus()
            Exit Sub
        End If
        If nz(myds.Tables("acf010t").Rows(0).Item("chkno"), "") = "" Then
            MsgBox("此傳票無支票號碼,請檢查")
            txtNo2.Text = ""
            txtNo2.Focus()
            Exit Sub
        End If
        With myds.Tables("acf010t")
            lblBank.Text = nz(.Rows(0).Item("bank"), "")
            lblAmt.Text = FormatNumber(nz(.Rows(0).Item("amt"), 0), 2)
            lblActAmt.Text = FormatNumber(nz(.Rows(0).Item("act_amt"), 0), 2)
            lblSubAmt.Text = FormatNumber(nz(.Rows(0).Item("amt"), 0) - nz(.Rows(0).Item("act_amt"), 0), 2)
            lblRemark.Text = nz(.Rows(0).Item("remark"), "")
            lblChkno.Text = nz(.Rows(0).Item("chkno"), "")
        End With

        sqlstr = "select start_no, end_no from chf010 where bank='" & lblBank.Text & "' and chkno='" & lblChkno.Text & _
                 "' and accyear=" & sYear
        myds = openmember("", "chf010", sqlstr)
        If myds.Tables("chf010").Rows.Count = 0 Then
            MsgBox("支票檔無此支票,請檢查")
            txtNo2.Focus()
            Exit Sub
        End If
        lblNo1.Text = nz(myds.Tables("chf010").Rows(0).Item("start_no"), "") & "至" & nz(myds.Tables("chf010").Rows(0).Item("end_no"), "")
        btnFinish.Enabled = True
        GroupBox1.Enabled = False
    End Sub

    Private Sub btnFinish_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFinish.Click
        If ValComa(lblSubAmt.Text) = ValComa(txtSubAmt2.Text) Then
            MsgBox("沖付數未異動,本作業不作任何資料修正")
            Exit Sub
        End If
        If ValComa(txtSubAmt2.Text) > ValComa(lblAmt.Text) Then
            MsgBox("沖付數不可大於總帳金額")
            Exit Sub
        End If
        Dim sqlstr, retstr As String
        Dim intAmt As Integer
        lblActAmt2.Text = FormatNumber(ValComa(lblAmt.Text) - ValComa(txtSubAmt2.Text), 2) '新實收付數
        intAmt = ValComa(lblActAmt.Text) - ValComa(lblActAmt2.Text)  '實收付數差額
        '更正acf010傳票
        sqlstr = "update acf010 set act_amt=" & ValComa(lblActAmt2.Text) & " where kind='" & skind & "' and accyear=" & _
          sYear & " and no_2_no=" & Trim(txtNo2.Text)
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then
            MsgBox("更正acf010傳票失敗,請檢查" & sqlstr)
            Exit Sub
        End If

        '更正chf020本日共收 or 本日共付
        If skind = "1" Then
            sqlstr = "update chf020 set day_income = day_income - " & intAmt & " where bank='" & lblBank.Text & "'"
        Else
            sqlstr = "update chf020 set day_pay = day_pay - " & intAmt & " where bank='" & lblBank.Text & "'"
        End If
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then
            MsgBox("更正chf020失敗,請檢查" & sqlstr)
            Exit Sub
        End If

        '更正支票chf010           
        sqlstr = "update chf010 set amt = amt - " & intAmt & " where bank='" & lblBank.Text & "' and chkno='" & _
                 lblChkno.Text & "' and accyear=" & sYear
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then
            MsgBox("update chf010失敗,請檢查" & sqlstr)
            Exit Sub
        End If
        MsgBox("修改完成")
        GroupBox1.Enabled = False
        btnFinish.Enabled = False     '2011/4/19 update 
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnGiveUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGiveUp.Click
        GroupBox1.Enabled = True
        btnFinish.Enabled = False
    End Sub

    Private Sub txtSubAmt2_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSubAmt2.LostFocus, txtNo2.LostFocus
        If (Not IsNumeric(sender.text) Or ValComa(sender.text) < 0) And Trim(sender.text) <> "" Then
            MsgBox("請輸入正數字")
            sender.focus()
        End If
    End Sub
End Class
