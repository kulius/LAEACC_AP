Public Class PAY022
    Dim sDate, oldDate_2, sqlstr, retstr As String '開票日
    Dim sYear, intI, intNo2, StartNo As Integer   'intno支款號
    Dim LoadAfter, Dirty, blnUseTR As Boolean
    Dim mydataset, myDatasetS As DataSet

    Private Sub pay022_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim sqlstr As String
        sDate = TransPara.TransP("userdate")    'put the login date to 收付款日

        '先由chf020找目前收付款日   
        sqlstr = "SELECT date_2 FROM chf020 where date_2 is not null"
        mydataset = openmember("", "chf020", sqlstr)
        If mydataset.Tables("chf020").Rows.Count > 0 Then
            oldDate_2 = mydataset.Tables("chf020").Rows(0).Item("date_2")  '目前收付款日
            dtpDate_2.Value = oldDate_2
        Else
            oldDate_2 = ""  '表示上日已作結存並已新開帳
            dtpDate_2.Value = sDate
            lblMsgDate.Text = "新結存日"
        End If

        If TransPara.TransP("blnUseTR") Then   '有使用電子轉帳
            blnUseTR = True
        Else
            blnUseTR = False
        End If
    End Sub

    Private Sub btnDate_2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDate_2.Click
        If oldDate_2 <> "" Then
            If CDate(dtpDate_2.Value) <> CDate(oldDate_2) Then
                lblMsgDate.Text = "本日未開帳"
                Exit Sub
            End If
        End If

        '新結存日  找最近收付款日(控制目前收付款日不可小於最近收付款日)
        If oldDate_2 = "" Then
            sqlstr = "SELECT MAX(date_2) AS DATE_2 FROM chf030 "
            mydataset = openmember("", "chf030", sqlstr)
            If mydataset.Tables("chf030").Rows.Count > 0 Then
                If dtpDate_2.Value < mydataset.Tables("chf030").Rows(0).Item("date_2") Then
                    lblMsgDate.Text = "目前收付款日不可小於最近收付款日" & mydataset.Tables("chf030").Rows(0).Item("date_2").toshortdatestring
                    Exit Sub
                End If
            End If
        End If
        sDate = dtpDate_2.Value.ToShortDateString
        lblDate_2.Text = sDate
        TransPara.TransP("sdate2") = sDate  'put the date2 to memory for pay025 use 
        sYear = GetYear(sDate)   '年度
        Panel1.Visible = False
        txtBank.Enabled = True
        txtChkNo.Enabled = True
        btnChkno.Visible = True
        If blnUseTR Then btnChkNo2.Visible = True
        btnPay025.Enabled = True
        txtBank.Focus()
    End Sub

    '調出電子支票  103/2/27 add
    Private Sub btnChkNo2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChkNo2.Click
        If Not IsNumeric(txtChkNo.Text) Then
            MsgBox("請輸入數字即可,請重輸入")
            Exit Sub
        End If
        txtChkNo.Text = "TR" & Format(sYear, "000") & Format(Val(txtChkNo.Text), "00000") '轉成電子支票格式  TRyyyXXXXX
        Call btnChkno_Click(New Object, New System.EventArgs)
    End Sub

    Private Sub btnChkno_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChkno.Click
        lblMsg.Text = ""
        sqlstr = "select * from chf010 where bank='" & txtBank.Text & "' and chkno='" & txtChkNo.Text & _
         "' and kind='2' and accyear=" & sYear
        mydataset = openmember("", "chf010", sqlstr)
        If mydataset.Tables("chf010").Rows.Count = 0 Then
            MsgBox("無此支票")
            txtChkNo.Focus()
            Exit Sub
        End If
        If mydataset.Tables("chf010").Rows(0).Item("start_no") <> 0 Then
            MsgBox("此支票已入帳")
            txtChkNo.Focus()
            Exit Sub
        End If

        lblamt.Text = FormatNumber(mydataset.Tables("chf010").Rows(0).Item("amt"), 0)
        lblChkname.Text = mydataset.Tables("chf010").Rows(0).Item("chkname")

        '取支款編號
        Dim intNo As Integer
        intNo2 = QueryNO(sYear, "5")    '\accservice\service1.asmx
        StartNo = intNo2 + 1
        sqlstr = "select no_1_no, remark, act_amt, bank, 0 as no_2_no from acf010 where accyear=" & sYear & _
                 " and chkno='" & txtChkNo.Text & "' and kind='2' and item='9' order by chkseq"
        myDatasetS = openmember("", "acf010s", sqlstr)
        For intI = 0 To myDatasetS.Tables("acf010s").Rows.Count - 1
            myDatasetS.Tables("acf010s").Rows(intI).Item("no_2_no") = intNo2 + intI + 1
        Next
        DataGrid1.DataSource = myDatasetS
        DataGrid1.DataMember = "acf010s"
        btnFinish.Visible = True
        btnGiveUp.Visible = True
        btnChkno.Visible = False
        btnChkNo2.Visible = False  '103/2/27  add
        txtBank.Enabled = False
        txtChkNo.Enabled = False
        btnPay025.Enabled = False
        btnFinish.Focus()
    End Sub

    Private Sub txtBank_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBank.KeyUp
        If Len(txtBank.Text) = 2 Then txtChkNo.Focus()
    End Sub

    '在支票號輸入完後按enter=按調出支票
    Private Sub txtChkNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtChkNo.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then   'press the enter key
            Call btnChkno_Click(New Object, New System.EventArgs)
        End If
    End Sub

    Private Sub txtChkNo_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtChkNo.KeyUp
        If Len(txtChkNo.Text) = 10 Then   '支票輸入滿10位後自動enter
            Call btnChkno_Click(New Object, New System.EventArgs)
        End If
    End Sub

    Private Sub btnGiveUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGiveUp.Click
        btnFinish.Visible = False
        btnGiveUp.Visible = False
        btnChkno.Visible = True
        If blnUseTR Then btnChkNo2.Visible = True '103/2/27 add
        txtBank.Enabled = True
        txtChkNo.Enabled = True
        btnPay025.Enabled = True
        txtBank.Text = ""
        txtChkNo.Text = ""
        txtBank.Focus()
    End Sub

    Private Sub btnFinish_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFinish.Click
        Dim intNo1 As Integer
        txtChkNo.Text = txtChkNo.Text.ToUpper

        For intI = 0 To myDatasetS.Tables("acf010s").Rows.Count - 1
            intNo1 = myDatasetS.Tables("acf010s").Rows(intI).Item("no_1_no")
            intNo2 += 1
            sqlstr = "update acf010 set no_2_no=" & intNo2 & ", date_2 = '" & genLongDate(sDate) & _
                     "'  where accyear=" & sYear & " and kind='2' and no_1_no=" & intNo1
            retstr = runsql(mastconn, sqlstr)
            If retstr <> "sqlok" Then MsgBox("填入acf010付款日及支款號錯誤" & sqlstr)
            sqlstr = "update acf020 set no_2_no=" & intNo2 & " where accyear=" & sYear & " and kind='2' and no_1_no=" & intNo1
            retstr = runsql(mastconn, sqlstr)
            If retstr <> "sqlok" Then MsgBox("填入acf020支款號錯誤" & sqlstr)
            '檢查是否為銀行轉帳,要刪除第一項及第二項
            sqlstr = "select accno from acf010 where kind='2' and item='1' and accyear=" & sYear & " and no_1_no=" & intNo1
            mydataset = openmember("", "acf010t", sqlstr)
            If mydataset.Tables("acf010t").Rows.Count > 0 Then
                sqlstr = nz(RTrim(mydataset.Tables("acf010t").Rows(0).Item("accno")), "")
                If sqlstr = "11102" Or sqlstr = "11103" Then
                    sqlstr = "delete from acf010 where kind='2' and item='1' and accyear=" & sYear & " and no_1_no=" & intNo1
                    retstr = runsql(mastconn, sqlstr)
                    sqlstr = "delete from acf020 where kind='2' and item='2' and accyear=" & sYear & " and no_1_no=" & intNo1
                    retstr = runsql(mastconn, sqlstr)
                End If
            End If
        Next

        '更正chf020 支票金額由unpay扣除,並加入本日共支
        sqlstr = "update chf020 set unpay = unpay - " & ValComa(lblamt.Text) & ", day_pay = day_pay + " & _
                 ValComa(lblamt.Text) & ", prt_code = ' ', date_2 = '" & genLongDate(sDate) & "' where bank='" & txtBank.Text & "'"
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then MsgBox("支票金額由unpay扣除,並加入本日共支錯誤" & sqlstr)

        '更正chf010 
        sqlstr = "update chf010 set date_2 = '" & genLongDate(sDate) & "', start_no= " & StartNo & _
                  ", end_no = " & intNo2 & " where bank='" & txtBank.Text & "' and chkno='" & txtChkNo.Text & _
                  "' and kind='2' and accyear=" & sYear
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then MsgBox("填入acf010付款日及支款號錯誤" & sqlstr)

        '更正支款編號acfno 
        sqlstr = "update acfno set cont_no=" & intNo2 & " where accyear=" & sYear & " and kind='5'"
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then MsgBox("更正支款編號acfno錯誤" & sqlstr)

        '填上該電子支票付款日期 chf050.date_2  103/2/27 add
        If Mid(txtChkNo.Text, 1, 2) = "TR" Then
            sqlstr = "update chf050 set date_2='" & genLongDate(sDate) & "' where bank='" & txtBank.Text & _
                             "' and vchkno='" & txtChkNo.Text & "' and accyear=" & sYear
            retstr = runsql(mastconn, sqlstr)
            If retstr <> "sqlok" Then
                MsgBox("chf050填上付款日期錯誤" & sqlstr)
            End If
        End If

        lblMsg.Text = "記帳完成"
        myDatasetS.Tables("acf010s").Clear()  '清除datagrid1資料
        txtBank.Text = ""
        txtChkNo.Text = ""
        txtBank.Enabled = True
        txtChkNo.Enabled = True
        btnChkno.Visible = True
        If blnUseTR Then btnChkNo2.Visible = True '103/2/27 add
        btnPay025.Enabled = True
        btnFinish.Visible = False
        btnGiveUp.Visible = False
        txtBank.Focus()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnPay025_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPay025.Click
        Dim pay025 As Form
        pay025 = New pay025
        pay025.Show()
    End Sub
End Class
