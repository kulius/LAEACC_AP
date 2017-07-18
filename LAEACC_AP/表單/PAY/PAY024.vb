Public Class PAY024
    Dim sDate, sqlstr, retstr As String  '開票日
    Dim sYear As Integer
    Dim LoadAfter, Dirty As Boolean

    Dim mydataset As DataSet

    Private Sub pay024_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim sqlstr As String
        sDate = TransPara.TransP("userdate") 'put the login date to 開票日
        sYear = GetYear(sDate)   '年度
        txtBank.Focus()
    End Sub

    Private Sub txtBank_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBank.KeyUp
        If Len(txtBank.Text) = 2 Then   '銀行輸入滿2位後自動至支票欄
            txtChkNo.Focus()
        End If
    End Sub

    '在支票號輸入完後按enter=按調出支票
    Private Sub txtChkNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtChkNo.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then   'press the enter key
            Call btnChkno_Click(New Object, New System.EventArgs)
        End If
    End Sub

    Private Sub btnChkno_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChkno.Click
        lblMsg.Text = ""
        sqlstr = "select * from chf010 where bank='" & txtBank.Text & "' and chkno='" & txtChkNo.Text & _
                 "' and accyear=" & sYear
        mydataset = openmember("", "chf010", sqlstr)
        If mydataset.Tables("chf010").Rows.Count = 0 Then
            MsgBox("無此支票")
            Exit Sub
        End If
        If mydataset.Tables("chf010").Rows(0).Item("start_no") <> 0 Then
            MsgBox("此支票已入帳不可作廢")
            Exit Sub
        End If
        lblMsg.Text = ""
        txtChkNo.Enabled = False
        txtBank.Enabled = False
        btnChkno.Enabled = False
        lblRemark.Text = mydataset.Tables("chf010").Rows(0).Item("remark")
        lblamt.Text = FormatNumber(mydataset.Tables("chf010").Rows(0).Item("amt"), 0)
        lblChkname.Text = mydataset.Tables("chf010").Rows(0).Item("chkname")
        lblNo1.Text = mydataset.Tables("chf010").Rows(0).Item("no_1_no")

        btnFinish.Visible = True
        btnGiveUp.Visible = True
        btnFinish.Focus()
    End Sub

    Private Sub btnGiveUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGiveUp.Click
        MyBase.Close()
    End Sub

    Private Sub btnFinish_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFinish.Click
        txtChkNo.Text = txtChkNo.Text.ToUpper

        '清除acf010支票號
        sqlstr = "update acf010 set chkno='' where accyear=" & sYear & _
                 " and chkno='" & txtChkNo.Text & "' and kind='2' and item='9'"
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then
            MsgBox("acf010->chkno支票號清除失敗,請檢查" & sqlstr)
        End If

        '刪除支票chf010 
        sqlstr = "delete chf010  where bank='" & txtBank.Text & _
                 "' and chkno='" & txtChkNo.Text & "' and accyear=" & sYear
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then
            MsgBox("刪除支票 chf010失敗,請檢查" & sqlstr)
        End If

        '將支票金額由已開未領欄扣除(chf020->unpay) 
        sqlstr = "update chf020 set unpay = unpay - " & ValComa(lblamt.Text) & " where bank='" & txtBank.Text & "'"
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then
            MsgBox("將支票金額由已開未領欄扣除(chf020->unpay)失敗,請檢查" & sqlstr)
        End If

        '電子轉帳 ADD 
        If Mid(txtChkNo.Text, 1, 2) = "TR" Then
            sqlstr = "delete chf050  where bank='" & txtBank.Text & _
                 "' and vchkno='" & txtChkNo.Text & "' and accyear=" & sYear
            retstr = runsql(mastconn, sqlstr)
            If retstr <> "sqlok" Then
                MsgBox("刪除電子轉帳檔 chf050失敗,請檢查" & sqlstr)
            End If
        End If

        lblMsg.Text = "作廢完成"
        txtChkNo.Enabled = True
        txtBank.Enabled = True
        btnChkno.Enabled = True
        btnFinish.Visible = False
        btnGiveUp.Visible = False
        txtBank.Focus()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class
