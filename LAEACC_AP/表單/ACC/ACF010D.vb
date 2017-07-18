Public Class ACF010D
    Dim sDate, skind As String
    Dim sYear As Integer
    Dim myds As DataSet
    Private Sub ACF010D_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtYear.Text = GetYear(Date.Today)   '年度
    End Sub

    Private Sub btnChkno_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSureNo.Click
        If Trim(txtNo1.Text) = "" Or Val(txtYear.Text) = 0 Then
            MsgBox("請輸入正確傳票")
            txtNo1.Focus()
            Exit Sub
        End If
        sYear = Val(txtYear.Text)
        Dim sqlstr As String
        If rdbKind1.Checked Then skind = "1"
        If rdbKind2.Checked Then skind = "2"
        If rdbKind3.Checked Then skind = "3"
        If skind <= "2" Then
            sqlstr = "select * from acf010 where kind='" & skind & "' and item='9'"
        Else
            sqlstr = "select * from acf010 where kind='" & skind & "' and item='1' "
        End If
        sqlstr &= " and accyear=" & sYear & " and no_1_no=" & Trim(txtNo1.Text)
        myds = openmember("", "acf010t", sqlstr)
        If myds.Tables("acf010t").Rows.Count = 0 Then
            MsgBox("無此傳票")
            txtNo1.Text = ""
            txtNo1.Focus()
            Exit Sub
        End If
        If Not IsDBNull(myds.Tables(0).Rows(0).Item("date_2")) Then
            MsgBox("此傳票已作帳" & myds.Tables(0).Rows(0).Item("date_2").toshortdatestring & "  第" & nz(myds.Tables(0).Rows(0).Item("no_2_no"), 0) & "號")
            txtNo1.Text = ""
            txtNo1.Focus()
            Exit Sub
        End If
        If skind <= "2" Then
            If Not IsDBNull(myds.Tables(0).Rows(0).Item("chkno")) Then
                If nz(myds.Tables(0).Rows(0).Item("chkno"), "") <> "" Then
                    MsgBox("此傳票已開支票,號碼=" & nz(myds.Tables(0).Rows(0).Item("chkno"), ""))
                    txtNo1.Text = ""
                    txtNo1.Focus()
                    Exit Sub
                End If
            End If
        End If
        lblAccno.Text = nz(myds.Tables(0).Rows(0).Item("accno"), "")
        lblAmt.Text = FormatNumber(nz(myds.Tables(0).Rows(0).Item("amt"), 0), 2)
        lblRemark.Text = nz(myds.Tables(0).Rows(0).Item("remark"), "")

        btnFinish.Visible = True
        btnGiveUp.Visible = True
        GroupBox1.Enabled = False
    End Sub

    Private Sub btnFinish_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFinish.Click
        If MsgBox("確定要刪除嗎? yes/no", MsgBoxStyle.YesNo) <> MsgBoxResult.Yes Then
            Exit Sub
        End If

        Dim sqlstr, retstr As String
        '更正acf010傳票
        If skind <= "2" Then
            sqlstr = "delete acf020 where kind='" & skind & "' and accyear=" & sYear & " and no_1_no=" & Trim(txtNo1.Text)
        Else
            sqlstr = "delete acf020 where kind>='3' and accyear=" & sYear & " and no_1_no=" & Trim(txtNo1.Text)
        End If
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then
            MsgBox("刪除acf020傳票失敗,請檢查" & sqlstr)
            Exit Sub
        End If
        If skind <= "2" Then
            sqlstr = "delete acf010 where kind='" & skind & "' and accyear=" & sYear & " and no_1_no=" & Trim(txtNo1.Text)
        Else
            sqlstr = "delete acf010 where kind>='3' and accyear=" & sYear & " and no_1_no=" & Trim(txtNo1.Text)
        End If
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then
            MsgBox("更正acf010傳票失敗,請檢查" & sqlstr)
            Exit Sub
        End If
        MsgBox("作業完成")
        Me.Close()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnGiveUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGiveUp.Click
        GroupBox1.Enabled = True
        btnFinish.Visible = False
        btnGiveUp.Visible = False
    End Sub

    Private Sub txtSubAmt2_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNo1.LostFocus
        If (Not IsNumeric(sender.text) Or ValComa(sender.text) < 0) And Trim(sender.text) <> "" Then
            MsgBox("請輸入正數字")
            sender.focus()
        End If
    End Sub
End Class
