Public Class AC220
    Dim sDate, skind As String
    Dim sYear As Integer
    Dim myds As DataSet

    Private Sub AC220_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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
        sYear = GetYear(sDate)   '年度
    End Sub

    Private Sub btnChkno_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSureNo.Click
        If Trim(txtChkNo.Text) = "" Then
            MsgBox("請輸入支票號")
            txtChkNo.Focus()
            Exit Sub
        End If
        Dim sqlstr As String
        skind = IIf(rdbKind1.Checked, "1", "2")
        sqlstr = "select * from chf010 where kind='" & skind & _
                 "' and bank='" & txtBank.Text & "' and chkno='" & txtChkNo.Text & "' and accyear=" & sYear
        myds = openmember("", "acf010t", sqlstr)
        If myds.Tables("acf010t").Rows.Count = 0 Then
            MsgBox("無此支票")
            txtChkNo.Focus()
            Exit Sub
        End If
        If IsDBNull(myds.Tables("acf010t").Rows(0).Item("date_2")) Then
            MsgBox("此支票尚未領取,請檢查")
            txtChkNo.Focus()
            Exit Sub
        End If
        If FullDate(myds.Tables("acf010t").Rows(0).Item("date_2")) <> FullDate(sDate) Then
            MsgBox("此功能只提供當日之帳務修改,目前收付款日=" & myds.Tables("acf010t").Rows(0).Item("date_2") & "<>" & FullDate(sDate))
            txtChkNo.Focus()
            Exit Sub
        End If
        With myds.Tables("acf010t")
            txtNewBank.Text = nz(.Rows(0).Item("bank"), "")
            txtNewChkNo.Text = nz(.Rows(0).Item("chkno"), "")
            lblDate_2.Text = ShortDate(.Rows(0).Item("date_2"))
            lblAmt.Text = FormatNumber(nz(.Rows(0).Item("amt"), 0), 2)
            lblNo2.Text = FormatNumber(nz(.Rows(0).Item("start_no"), 0), 0) & "至" & FormatNumber(nz(.Rows(0).Item("end_no"), 0), 0)
            lblRemark.Text = nz(.Rows(0).Item("remark"), "")
            lblChkname.Text = nz(.Rows(0).Item("chkname"), "")
        End With
        btnFinish.Enabled = True
        GroupBox1.Enabled = False
        gbxNew.Enabled = True
    End Sub

    Private Sub btnFinish_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFinish.Click
        If txtBank.Text = txtNewBank.Text And txtChkNo.Text = txtNewChkNo.Text Then
            MsgBox("未異動,本作業不作任何資料修正")
            Exit Sub
        End If
        Dim sqlstr, retstr As String
        sqlstr = "select * from chf010 where kind='" & skind & _
                "' and bank='" & txtNewBank.Text & "' and chkno='" & txtNewChkNo.Text & "' and accyear=" & sYear
        myds = openmember("", "acf010t", sqlstr)
        If myds.Tables("acf010t").Rows.Count > 0 Then
            MsgBox("支票號重複!")
            txtNewChkNo.Focus()
            Exit Sub
        End If

        '更正acf010傳票
        sqlstr = "update acf010 set bank='" & txtNewBank.Text & "', chkno='" & txtNewChkNo.Text & "'" & _
                 " where kind='" & skind & "' and accyear=" & sYear & " and date_2='" & FullDate(sDate) & _
                 "' and bank='" & txtBank.Text & "' and chkno='" & txtChkNo.Text & "'"
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then
            MsgBox("更正acf010傳票失敗,請檢查" & sqlstr)
            Exit Sub
        End If
        If txtBank.Text <> txtNewBank.Text Then   '銀行異動要改傳票科目
            sqlstr = "select accno from chf020 where bank='" & txtNewBank.Text & "'"
            myds = openmember("", "acf010t", sqlstr)
            If myds.Tables("acf010t").Rows.Count > 0 Then
                Dim straccno As String = myds.Tables("acf010t").Rows(0).Item(0)
                sqlstr = "update acf010 set accno='" & straccno & "'" & _
                         " where item='9' and kind='" & skind & "' and accyear=" & sYear & " and date_2='" & FullDate(sDate) & _
                         "' and bank='" & txtNewBank.Text & "' and chkno='" & txtNewChkNo.Text & "'"
                retstr = runsql(mastconn, sqlstr)
                If retstr <> "sqlok" Then
                    MsgBox("更正acf010傳票失敗,請檢查" & sqlstr)
                    Exit Sub
                End If
            End If
        End If

        '更正chf020本日共收 or 本日共付
        If txtBank.Text <> txtNewBank.Text Then
            '扣除原銀行收付
            If skind = "1" Then
                sqlstr = "update chf020 set day_income = day_income - " & ValComa(lblAmt.Text) & " where bank='" & txtBank.Text & "'"
            Else
                sqlstr = "update chf020 set day_pay = day_pay - " & ValComa(lblAmt.Text) & " where bank='" & txtBank.Text & "'"
            End If
            retstr = runsql(mastconn, sqlstr)
            If retstr <> "sqlok" Then
                MsgBox("更正chf020失敗,請檢查" & sqlstr)
            End If
            '加至新銀行收付
            If skind = "1" Then
                sqlstr = "update chf020 set day_income = day_income + " & ValComa(lblAmt.Text)
            Else
                sqlstr = "update chf020 set day_pay = day_pay + " & ValComa(lblAmt.Text)
            End If
            sqlstr &= ", date_2='" & FullDate(sDate) & "', prt_code=' '  where bank='" & txtNewBank.Text & "'"
            retstr = runsql(mastconn, sqlstr)
            If retstr <> "sqlok" Then
                MsgBox("更正chf020失敗,請檢查" & sqlstr)
            End If
        End If

        '更正支票chf010 
        sqlstr = "update chf010 set bank ='" & txtNewBank.Text & "', chkno='" & txtNewChkNo.Text & _
                 "' where bank='" & txtBank.Text & "' and chkno='" & txtChkNo.Text & "' and accyear=" & sYear
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then
            MsgBox("update chf010失敗,請檢查" & sqlstr)
            Exit Sub
        End If
        MsgBox("修改完成")
        GroupBox1.Enabled = True
        btnFinish.Enabled = True
        gbxNew.Enabled = False
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnGiveUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGiveUp.Click
        GroupBox1.Enabled = True
        btnFinish.Enabled = False
        gbxNew.Enabled = False
    End Sub

    Private Sub txtBank_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBank.LostFocus, _
                                     txtChkNo.LostFocus, txtNewBank.LostFocus, txtNewChkNo.LostFocus
        sender.text = Trim(sender.text)
    End Sub
End Class
