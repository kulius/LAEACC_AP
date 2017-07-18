Imports CHIA.CheckPrint
Imports System.Data.SqlClient
Public Class PAY023
    Dim sDate, sqlstr, retstr As String  '開票日
    Dim sYear, intI As Integer
    Dim intChkForm As Integer = 1
    Dim BankBalance As Decimal
    Dim LoadAfter, Dirty As Boolean

    Dim mydataset, psDataSet As DataSet

    Private Sub pay023_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim sqlstr As String
        'LoadAfter = True
        intI = 0   '傳票件數
        sDate = TransPara.TransP("userdate") 'put the login date to 開票日
        sYear = GetYear(sDate)   '年度

        '將所有受款人置combobox   
        'sqlstr = "SELECT left(psstr + space(50),50) as psstr FROM psname where unit='0403' order by psstr"
        sqlstr = "SELECT psstr FROM psname where unit='0403' order by psstr"
        psDataSet = openmember("", "psname", sqlstr)
        If psDataSet.Tables("psname").Rows.Count = 0 Then
            cboName.Text = "尚無片語"
        Else
            cboName.DataSource = psDataSet.Tables("psname")
            cboName.DisplayMember = "psstr"  '顯示欄位
            cboName.ValueMember = "psstr"     '欄位值
            cboName.SelectionLength = 60
        End If
        txtBank.Focus()
    End Sub

    Private Sub txtBank_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBank.KeyUp
        If Len(txtBank.Text) = 2 Then   '銀行輸入滿2位後自動至支票欄
            txtChkNo.Focus()
        End If
    End Sub

    Private Sub btnChkno_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChkno.Click
        lblMsg.Text = ""
        sqlstr = "select * from chf010 where bank='" & txtBank.Text & "' and chkno='" & txtChkNo.Text & _
                 "' and accyear=" & sYear
        mydataset = openmember("", "chf010", sqlstr)
        If mydataset.Tables("chf010").Rows.Count = 0 Then
            MsgBox("無此支票")
            txtChkNo.Focus()
            Exit Sub
        End If
        If mydataset.Tables("chf010").Rows(0).Item("start_no") <> 0 Then
            MsgBox("此支票已入帳不可更動")
            txtChkNo.Focus()
            Exit Sub
        End If
        txtChkNo.Enabled = False
        txtBank.Enabled = False
        btnChkno.Enabled = False
        txtRemark.Text = mydataset.Tables("chf010").Rows(0).Item("remark")
        lblamt.Text = FormatNumber(mydataset.Tables("chf010").Rows(0).Item("amt"), 0)
        txtChkname.Text = mydataset.Tables("chf010").Rows(0).Item("chkname")
        lblNo1.Text = mydataset.Tables("chf010").Rows(0).Item("no_1_no")
        lblChkname.Text = txtChkname.Text  '記錄原先資料
        lblRemark.Text = txtRemark.Text     '記錄原先資料

        '電子轉帳 ADD & mod 
        txtNewChkno.Enabled = True
        If Mid(txtChkNo.Text, 1, 2).ToUpper = "TR" Then
            '支票號不可變動
            txtNewChkno.Enabled = False
            txtNewChkno.Text = txtChkNo.Text.ToUpper
        Else
            '取新支票號
            sqlstr = "SELECT * FROM chf020 WHERE bank = '" & txtBank.Text & "'"
            mydataset = openmember("", "chf020", sqlstr)
            If mydataset.Tables("chf020").Rows.Count > 0 Then
                With mydataset.Tables("chf020").Rows(0)
                    lblBankname.Text = .Item("bank") & .Item("bankname")
                    BankBalance = .Item("balance") + .Item("day_income") - .Item("day_pay") - .Item("unpay") + nz(.Item("credit"), 0)
                    '支票號+1
                    txtNewChkno.Text = nz(.Item("chkno"), "")
                    txtNewChkno.Text = AddCheckNo(txtNewChkno.Text)
                    intChkForm = nz(.Item("chkform"), 1)
                End With
            End If
        End If

        mydataset = Nothing

        txtRemark.Visible = True
        cboName.Visible = True
        txtChkname.Visible = True
        btnFinish.Visible = True
        btnGiveUp.Visible = True
        txtNewChkno.Visible = True
        txtNewChkno.Focus()
    End Sub

    Private Sub txtChkNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtChkNo.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then   'press the enter key
            Call btnChkno_Click(New System.Object, New System.EventArgs)
        End If
    End Sub


    Private Sub btnGiveUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGiveUp.Click
        Call GiveUp()
    End Sub

    Private Sub cboName_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cboName.MouseDown
        intI = cboName.FindString(Trim(txtChkname.Text))
        cboName.SelectedIndex = intI         '將受款人置combo
    End Sub

    Private Sub cboName_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboName.SelectionChangeCommitted
        txtChkname.Text = cboName.Text
    End Sub

    Private Sub btnFinish_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFinish.Click
        txtChkNo.Text = txtChkNo.Text.ToUpper
        txtNewChkno.Text = txtNewChkno.Text.ToUpper

        If txtChkNo.Text = txtNewChkno.Text And txtChkname.Text = lblChkname.Text And txtRemark.Text = lblRemark.Text Then
            If Mid(txtChkNo.Text, 1, 2) <> "TR" Then
                PrintCheck(txtBank.Text, lblBankname.Text, sDate, txtChkname.Text, ValComa(lblamt.Text), txtRemark.Text, BankBalance) '列印支票 at vbdataio.vb
            End If
            Call GiveUp()
            Exit Sub
        End If

        '檢查支票碼重複
        If txtChkNo.Text <> txtNewChkno.Text Then
            '由一般支票轉為電子支票
            If Mid(txtChkNo.Text, 1, 2) <> "TR" And Mid(txtNewChkno.Text, 1, 2) = "TR" Then
                If Len(RTrim(txtNewChkno.Text)) < 10 Or Not IsNumeric(Mid(txtNewChkno.Text, 6, 5)) Then
                    MsgBox("請輸入TR" & Format(sYear, "000") & "00000" & "的支票格式")
                    Exit Sub
                End If
                sqlstr = "SELECT chkno FROM chf010 where chkno='" & txtNewChkno.Text & "'"
                mydataset = openmember("", "chf010", sqlstr)
                If mydataset.Tables("chf010").Rows.Count > 0 Or Val(Mid(txtNewChkno.Text, 6, 5)) = 0 Then '有重複
                    '取新轉帳支票號
                    txtNewChkno.Text = "TR" & Format(sYear, "000") & Format(RequireNO(mastconn, sYear, "T"), "00000")
                Else
                    '沒重複也要判斷號數不可大於控制檔
                    sqlstr = "SELECT * FROM acfno where kind='T' and accyear=" & sYear
                    mydataset = openmember("", "acfno", sqlstr)
                    If mydataset.Tables("acfno").Rows.Count > 0 Then
                        If Val(Mid(txtNewChkno.Text, 6, 5)) - 1 > nz(mydataset.Tables("acfno").Rows(0).Item("cont_no"), 0) Then
                            txtNewChkno.Text = "TR" & Format(sYear, "000") & Format(RequireNO(mastconn, sYear, "T"), "00000")
                        End If
                    End If
                End If
            End If
            sqlstr = "SELECT chkno FROM chf010 where chkno='" & txtNewChkno.Text & "'"
            mydataset = openmember("", "chf010", sqlstr)
            If mydataset.Tables("chf010").Rows.Count > 0 Then
                MsgBox("支票碼重複")
                txtNewChkno.Focus()
                Exit Sub
            End If
            mydataset = Nothing

            '資料處理
            sqlstr = "update acf010 set chkno='" & txtNewChkno.Text.ToUpper & "' where accyear=" & sYear & _
                     " and chkno='" & txtChkNo.Text & "' and kind='2' and item='9'"
            retstr = runsql(mastconn, sqlstr)
            If retstr <> "sqlok" Then
                MsgBox("支票號碼寫入acf010->chkno失敗,請檢查" & sqlstr)
            End If

            '記錄最後一張支票號
            If Mid(txtNewChkno.Text, 1, 2) <> "TR" Then
                sqlstr = "update chf020 set chkno = '" & txtNewChkno.Text & "' where bank='" & txtBank.Text & "'"
                retstr = runsql(mastconn, sqlstr)
            Else
                GenInsSql("accyear", sYear, "N")
                GenInsSql("vchkno", txtNewChkno.Text, "T")
                GenInsSql("date_1", sDate, "D")
                GenInsSql("bank", txtBank.Text, "T")
                sqlstr = "insert into chf050 " & GenInsFunc
                retstr = runsql(mastconn, sqlstr)
                If retstr <> "sqlok" Then
                    MsgBox("新增支票寫入chf050失敗,請檢查" & sqlstr)
                Else
                    MsgBox("新電子支票=" & txtNewChkno.Text)
                End If
            End If
        End If

        '更正chf010 
        sqlstr = "update chf010 set chkno = '" & txtNewChkno.Text.ToUpper & "', chkname = N'" & txtChkname.Text & _
                 "', remark = N'" & txtRemark.Text & "' where bank='" & txtBank.Text & _
                 "' and chkno='" & txtChkNo.Text & "' and accyear=" & sYear
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then
            MsgBox("update chf010失敗,請檢查" & sqlstr)
        End If

        lblMsg.Text = "修改完成"

        '電子轉帳 MOD
        If Mid(txtNewChkno.Text, 1, 2) <> "TR" Then
            PrintCheck(txtBank.Text, lblBankname.Text, sDate, txtChkname.Text, ValComa(lblamt.Text), txtNewChkno.Text & txtRemark.Text, BankBalance)  '列印支票 at vbdataio.vb
        End If

        Call GiveUp()
    End Sub

    Sub GiveUp()
        txtChkNo.Enabled = True
        txtBank.Enabled = True
        btnChkno.Enabled = True
        txtBank.Focus()
        txtRemark.Visible = False
        cboName.Visible = False
        txtChkname.Visible = False
        btnFinish.Visible = False
        btnGiveUp.Visible = False
        txtNewChkno.Visible = False
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class
