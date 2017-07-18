Public Class PAY030
    Dim sDate, oldDate_2, sqlstr, sBank, retstr As String '開票日
    Dim sYear, intTemp, intI, intNo2, StartNo As Integer
    Dim mydsS As DataSet
    Dim LoadAfter, Dirty As Boolean

    Dim bm As BindingManagerBase
    Dim myDatasetS, mydataset, psDataSet As DataSet
    Dim arrayNo As String

    Private Sub pay030_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim sqlstr As String
        'LoadAfter = True
        intI = 0   '傳票件數
        arrayNo = ""  '將傳票置array
        sDate = TransPara.TransP("userdate")    'put the login date to 收付款日
        sYear = GetYear(sDate)   '年度
        sqlstr = "select no_1_no, remark, act_amt, bank, chkno, 0 as no_2_no from acf010 where accyear=0" 'put the struture to the acf010s dataset 
        myDatasetS = openmember("", "acf010s", sqlstr)
        RecMove1.FilesPara = "mastconn=" & mastconn & "|sqlstr=" & sqlstr
        DataGrid1.DataSource = myDatasetS
        DataGrid1.DataMember = "acf010s"
        bm = Me.BindingContext(myDatasetS, "acf010s")
        RecMove1.Setds = bm
        RecMove1.RecIns.Enabled = False
        RecMove1.RecUpdCMD.Enabled = False

        '將所有受款人置combobox   
        sqlstr = "SELECT left(psstr + space(50),50) as psstr FROM psname where unit='0403' order by psstr"
        psDataSet = openmember("", "psname", sqlstr)
        If psDataSet.Tables("psname").Rows.Count = 0 Then
            cboName.Text = "尚無片語"
        Else
            cboName.DataSource = psDataSet.Tables("psname")
            cboName.DisplayMember = "psstr"  '顯示欄位
            cboName.ValueMember = "psstr"     '欄位值
            cboName.SelectionLength = 60
        End If

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
        sYear = GetYear(sDate)   '年度
        Panel1.Visible = False
        'btnSure.Enabled = True
        btnSureNo.Enabled = True
        txtNo1.Enabled = True
        txtNo1.Focus()
    End Sub

    Private Sub btnSureNo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSureNo.Click
        lblMsg.Text = ""
        If Trim(txtNo1.Text) = "" Then
            MsgBox("請輸入傳票")
            txtNo1.Text = ""
            txtNo1.Focus()
            Exit Sub
        End If
        If arrayNo.IndexOf(Trim(txtNo1.Text)) >= 0 Then
            MsgBox("傳票重複")
            txtNo1.Text = ""
            txtNo1.Focus()
            Exit Sub
        End If
        sqlstr = "select no_1_no, remark, act_amt, bank, chkno, no_2_no from acf010 where kind='1' and item='9' and accyear=" & _
                  sYear & " and no_1_no=" & Trim(txtNo1.Text)
        mydsS = openmember("", "acf010t", sqlstr)
        If mydsS.Tables("acf010t").Rows.Count = 0 Then
            MsgBox("無此傳票")
            txtNo1.Text = ""
            txtNo1.Focus()
            Exit Sub
        End If
        If mydsS.Tables("acf010t").Rows(0).Item("no_2_no") <> 0 Then
            MsgBox("此傳票已作帳" & mydsS.Tables("acf010t").Rows(0).Item("no_2_no"))
            txtNo1.Text = ""
            txtNo1.Focus()
            Exit Sub
        End If

        If bm.Count > 0 Then   '第二筆以後要檢察銀行要相同
            If mydsS.Tables("acf010t").Rows(0).Item("bank") <> lblBank.Text Then
                MsgBox("銀行不相同,請重輸入傳票" & mydsS.Tables("acf010t").Rows(0).Item("bank"))
                txtNo1.Text = ""
                txtNo1.Focus()
                Exit Sub
            End If
        End If
        lblNo1.Text = mydsS.Tables("acf010t").Rows(0).Item("no_1_no")
        lblBank.Text = mydsS.Tables("acf010t").Rows(0).Item("bank")
        lblAct_amt.Text = FormatNumber(mydsS.Tables("acf010t").Rows(0).Item("act_amt"), 2)
        lblRemark.Text = Trim(mydsS.Tables("acf010t").Rows(0).Item("remark"))
        If bm.Count = 0 Then    '第一筆
            sBank = lblBank.Text '記錄第一筆銀行以便控制銀行要相同
            '找受款人
            If TransPara.TransP("UnitTitle").indexof("七星") < 0 Then
                intTemp = lblRemark.Text.IndexOf("  ")
                If intTemp <= 0 Then
                    intTemp = Len(lblRemark.Text)
                    txtChkname.Text = Microsoft.VisualBasic.Right(lblRemark.Text, IIf(intTemp >= 5, 5, intTemp))
                Else
                    txtChkname.Text = Trim(Mid(lblRemark.Text, intTemp + 2))      '由摘要取受款人
                End If
            End If
            txtRemark.Text = lblRemark.Text
            lblMsg.Text = ""
            '取收款編號
            intNo2 = QueryNO(sYear, "4")    '\accservice\service1.asmx
            StartNo = intNo2 + 1
            '支票號
            txtChkNo.Text = Format(intNo2 + 1, "00000")
            '帳戶餘額
            sqlstr = "SELECT * FROM chf020 WHERE bank = '" & lblBank.Text & "'"
            mydataset = openmember("", "chf020", sqlstr)
            If mydataset.Tables("chf020").Rows.Count > 0 Then
                With mydataset.Tables("chf020").Rows(0)
                    lblBankName.Text = .Item("bank") & .Item("bankname")
                End With
            End If
            mydataset = Nothing
        End If

        Call AddDataGrid()
        txtNo1.Text = ""
        btnFinish.Enabled = True
        btnFinish.Focus()    '收入一般都是一張傳票一張支票
    End Sub

    '在傳票號輸入完後按enter
    Private Sub txtNo1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNo1.KeyPress
        If (Not IsNumeric(sender.text) Or ValComa(sender.text) < 0) And Trim(sender.text) <> "" Then
            MsgBox("請輸入正數字")
            sender.focus()
        End If
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then   'press the enter key
            'e.Handled = True
            Call btnSureNo_Click(New System.Object, New System.EventArgs)
        End If
    End Sub

    Sub AddDataGrid()
        Dim nr As DataRow
        arrayNo = arrayNo & Format(Val(lblNo1.Text), "00000") & ","   '將傳票置入字串
        nr = myDatasetS.Tables("acf010s").NewRow()
        nr("no_1_no") = lblNo1.Text
        nr("remark") = lblRemark.Text
        nr("act_amt") = ValComa(lblAct_amt.Text)
        nr("bank") = lblBank.Text
        nr("no_2_no") = intNo2 + bm.Count + 1
        myDatasetS.Tables("acf010s").Rows.Add(nr)                          '增行至source grid
        lblTotAmt.Text = FormatNumber(ValComa(lblTotAmt.Text) + ValComa(lblAct_amt.Text), 2)
    End Sub

    Sub transjob(ByVal JobName As String, ByVal JobPara As String)
        Dim LastPos As Integer = bm.Position
        Select Case JobName
            Case "刪除記錄"
                bm.Position = LastPos
                lblMsg.Text = LastPos
                lblTotAmt.Text = FormatNumber(ValComa(lblTotAmt.Text) - bm.Current("Act_Amt"), 2)
                arrayNo = arrayNo.Replace(Format(bm.Current("no_1_no"), "00000") & ",", "") '將傳票置array
                myDatasetS.Tables("acf010s").Rows.RemoveAt(JobPara)

            Case "記錄移動"
                Dirty = False
        End Select
    End Sub

    Private Sub cboName_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboName.GotFocus
        intI = cboName.FindString(Trim(txtChkname.Text))
        cboName.SelectedIndex = intI         '將受款人置combo
    End Sub

    Private Sub cboName_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboName.SelectionChangeCommitted
        txtChkname.Text = cboName.SelectedText
    End Sub

    Private Sub btnFinish_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFinish.Click
        Dim intI, intNo As Integer
        Dim tempdataset As DataSet
        intI = bm.Count
        If bm.Count = 0 Then
            MsgBox("無傳票資料")
            Exit Sub
        End If

        '檢查支票碼重複
        sqlstr = "SELECT chkno FROM chf010 where kind='1' and chkno='" & txtChkNo.Text & "' and accyear=" & sYear
        tempdataset = openmember("", "chf010", sqlstr)
        If tempdataset.Tables("chf010").Rows.Count > 0 Then
            MsgBox("支票碼重複")
            Exit Sub
        End If
        tempdataset = Nothing
        If txtChkNo.Text = "" Then
            MsgBox("支票碼錯誤")
            Exit Sub
        End If

        '將支票號碼填入acf010->chkno , date_2 
        For intI = 0 To bm.Count - 1
            bm.Position = intI
            intNo = bm.Current("no_1_no")
            intNo2 += 1
            sqlstr = "update acf010 set no_2_no=" & intNo2 & ", date_2 = '" & genLongDate(sDate) & _
            "', chkno='" & txtChkNo.Text & "' where accyear=" & sYear & " and kind='1' and no_1_no=" & intNo
            retstr = runsql(mastconn, sqlstr)
            If retstr <> "sqlok" Then
                MsgBox("支票號碼填入acf010->chkno失敗,請檢查" & sqlstr)
            End If
            sqlstr = "update acf020 set no_2_no=" & intNo2 & " where accyear=" & sYear & " and kind='1' and no_1_no=" & intNo
            retstr = runsql(mastconn, sqlstr)
            If retstr <> "sqlok" Then MsgBox("填入acf020支款號錯誤" & sqlstr)
            '檢查是否為銀行轉帳,要刪除第一項及第二項
            sqlstr = "select accno from acf010 where kind='1' and item='1' and accyear=" & sYear & " and no_1_no=" & intNo
            mydsS = openmember("", "acf010t", sqlstr)
            If mydsS.Tables("acf010t").Rows.Count > 0 Then
                If mydsS.Tables("acf010t").Rows(0).Item("accno") = "11102" Or mydsS.Tables("acf010t").Rows(0).Item("accno") = "11103" Then
                    sqlstr = "delete from acf010 where kind='1' and item='1' and accyear=" & sYear & " and no_1_no=" & intNo
                    retstr = runsql(mastconn, sqlstr)
                    sqlstr = "delete from acf020 where kind='1' and item='2' and accyear=" & sYear & " and no_1_no=" & intNo
                    retstr = runsql(mastconn, sqlstr)
                End If
            End If
        Next
        '新增一筆資料至chf010 
        GenInsSql("accyear", sYear, "N")
        GenInsSql("kind", "1", "T")
        GenInsSql("bank", sBank, "T")
        GenInsSql("chkno", txtChkNo.Text, "T")
        GenInsSql("date_1", sDate, "D")
        GenInsSql("date_2", sDate, "D")
        GenInsSql("chkname", txtChkname.Text, "U")
        GenInsSql("remark", txtRemark.Text, "U")
        GenInsSql("amt", lblTotAmt.Text, "N")
        GenInsSql("start_no", StartNo, "N")
        GenInsSql("End_no", intNo2, "N")
        GenInsSql("no_1_no", arrayNo, "T")
        sqlstr = "insert into chf010 " & GenInsFunc
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then
            MsgBox("新增支票寫入chf010失敗,請人工檢查" & sqlstr)
        End If

        '將支票金額加入本日共支欄(chf020->day_income) 
        '列印碼清空
        sqlstr = "update chf020 set day_income = day_income + " & ValComa(lblTotAmt.Text) & ", prt_code = ' ', date_2 = '" & _
                 genLongDate(sDate) & "' where bank='" & sBank & "'"
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then
            MsgBox("將支票金額加入本日共支欄(chf020-day_income)失敗,請檢查" & sqlstr)
        End If

        arrayNo = ""                          '清除該張支票所含之傳票號
        myDatasetS.Tables("acf010s").Clear()  '清除datagrid1資料

        '更正收款編號acfno 
        sqlstr = "update acfno set cont_no=" & intNo2 & " where accyear=" & sYear & " and kind='4'"
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then MsgBox("更正收款編號acfno錯誤" & sqlstr)

        lblMsg.Text = "收款編號" & intNo2 & "記帳完成"
        myDatasetS.Tables("acf010s").Clear()  '清除datagrid1資料
        lblTotAmt.Text = "0"    '清收入合計
        btnFinish.Enabled = False
        txtNo1.Text = ""
        txtNo1.Focus()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class
