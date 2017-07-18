Public Class PAY025
    Dim sDate, sqlstr, sBank, retstr As String  '開票日
    Dim sYear, intTemp, intI, intNo2, StartNo As Integer
    Dim mydsS As DataSet
    Dim LoadAfter, Dirty As Boolean
    Dim bm As BindingManagerBase
    Dim myDatasetS, mydataset, psDataSet As DataSet
    Dim arrayNo As String

    Private Sub pay025_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim sqlstr As String
        'LoadAfter = True
        intI = 0   '傳票件數
        arrayNo = ""  '將傳票置array
        sDate = TransPara.TransP("sdate2") 'accept pay022 date2 to 開票日
        sYear = GetYear(sDate)   '年度
        sqlstr = "select no_1_no, remark, act_amt, bank, chkno, 0 as no_2_no from acf010 where accyear=0" 'put the struture to the acf010s dataset 
        myDatasetS = openmember("", "acf010s", sqlstr)
        RecMove1.FilesPara = "mastconn=" & mastconn & "|sqlstr=" & sqlstr
        DataGrid1.DataSource = myDatasetS
        DataGrid1.DataMember = "acf010s"
        bm = Me.BindingContext(myDatasetS, "acf010s")
        RecMove1.Setds = bm
        txtNo1.Focus()
        RecMove1.RecIns.Enabled = False
        RecMove1.RecUpdCMD.Enabled = False

        '將所有受款人置combobox   
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
    End Sub

    Private Sub btnSureNo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSureNo.Click
        lblMsg.Text = ""
        If Trim(txtNo1.Text) = "" Then
            MsgBox("請輸入傳票")
            txtNo1.Focus()
            Exit Sub
        End If
        If arrayNo.IndexOf(Trim(txtNo1.Text)) >= 0 Then
            MsgBox("傳票重複")
            txtNo1.Focus()
            Exit Sub
        End If
        sqlstr = "select no_1_no, remark, act_amt, bank, chkno, no_2_no from acf010 where kind='2' and item='9' and accyear=" & _
                  sYear & " and no_1_no=" & Trim(txtNo1.Text)
        mydsS = openmember("", "acf010t", sqlstr)
        If mydsS.Tables("acf010t").Rows.Count = 0 Then
            MsgBox("無此傳票")
            txtNo1.Focus()
            Exit Sub
        End If
        If nz(mydsS.Tables("acf010t").Rows(0).Item("chkno"), "") = "" Then
        Else
            If mydsS.Tables("acf010t").Rows(0).Item("no_2_no") <> 0 Then
                MsgBox("此傳票已作帳" & mydsS.Tables("acf010t").Rows(0).Item("no_2_no"))
                txtNo1.Focus()
                Exit Sub
            Else
                MsgBox("此傳票已開支票" & mydsS.Tables("acf010t").Rows(0).Item("chkno"))
                txtNo1.Focus()
                Exit Sub
            End If
        End If
        If bm.Count > 0 Then   '第二筆以後要檢察銀行要相同
            If mydsS.Tables("acf010t").Rows(0).Item("bank") <> lblBank.Text Then
                MsgBox("銀行不相同,請重輸入傳票" & mydsS.Tables("acf010t").Rows(0).Item("bank"))
                txtNo1.Focus()
                Exit Sub
            End If
        End If
        lblNo1.Text = mydsS.Tables("acf010t").Rows(0).Item("no_1_no")
        lblBank.Text = mydsS.Tables("acf010t").Rows(0).Item("bank")
        lblAct_Amt.Text = FormatNumber(mydsS.Tables("acf010t").Rows(0).Item("act_amt"), 2)
        lblRemark.Text = Trim(mydsS.Tables("acf010t").Rows(0).Item("remark"))
        If bm.Count = 0 Then    '第一筆
            sBank = lblBank.Text '記錄第一筆銀行以便控制銀行要相同
            intTemp = lblRemark.Text.IndexOf("  ")
            If intTemp < 0 Then intTemp = Len(lblRemark.Text) - 7 '找不到則取最後五個字
            txtRemark.Text = lblRemark.Text
            If intTemp <= 0 Then
                intTemp = Len(lblRemark.Text)
                txtChkname.Text = Microsoft.VisualBasic.Right(lblRemark.Text, IIf(intTemp >= 5, 5, intTemp))
            Else
                txtChkname.Text = Trim(Mid(lblRemark.Text, intTemp + 2))      '由摘要取受款人
            End If
            lblMsg.Text = ""
            '取收款編號
            intNo2 = QueryNO(sYear, "5")     '\accservice\service1.asmx
            StartNo = intNo2 + 1
            '支票號
            txtChkNo.Text = Format(intNo2 + 1, "00000")
            '帳戶餘額
            sqlstr = "SELECT * FROM chf020 WHERE bank = '" & lblBank.Text & "'"
            mydataset = openmember("", "chf020", sqlstr)
            If mydataset.Tables("chf020").Rows.Count > 0 Then
                With mydataset.Tables("chf020").Rows(0)
                    lblBankName.Text = .Item("bank") & .Item("bankname")
                    lblBalance.Text = FormatNumber(.Item("balance") + .Item("day_income") - .Item("day_pay") - .Item("unpay") + .Item("credit"), 2)
                End With
            End If
            mydataset = Nothing
        End If

        If ValComa(lblBalance.Text) < ValComa(lblTotAmt.Text) + ValComa(lblAct_Amt.Text) Then
            MsgBox("存款不足")
            txtNo1.Text = ""
            txtNo1.Focus()
            Exit Sub
        End If

        Call AddDataGrid()
        txtNo1.Text = ""
        btnSure.Enabled = True
        txtNo1.Focus()
    End Sub

    '在傳票號輸入完後按enter
    Private Sub txtNo1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNo1.KeyPress
        If (Not IsNumeric(sender.text) Or ValComa(sender.text) < 0) And Trim(sender.text) <> "" Then
            MsgBox("請輸入正數字")
            sender.focus()
        End If
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then   'press the enter key
            Call btnSureNo_Click(New System.Object, New System.EventArgs)
        End If
    End Sub

    Private Sub txtNo1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtNo1.KeyUp
        If Len(txtNo1.Text) = 5 Then   '傳票輸入滿5位後自動enter
            Call btnSureNo_Click(New Object, New System.EventArgs)
        End If
    End Sub

    '按開立支票butten
    Private Sub btnSure_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSure.Click
        lblTotamt2.Text = FormatNumber(ValComa(lblTotAmt.Text), 2)
        TabControl1.SelectedIndex = 1
    End Sub

    Private Sub TabControl1_TabIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.TabIndexChanged
        ' 與btnsure.click相同
        If TabControl1.SelectedIndex = 1 Then lblTotamt2.Text = FormatNumber(ValComa(lblTotAmt.Text), 2)
    End Sub

    Sub AddDataGrid()
        Dim nr As DataRow
        arrayNo = arrayNo & Format(Val(lblNo1.Text), "00000") & ","   '將傳票置入字串
        nr = myDatasetS.Tables("acf010s").NewRow()
        nr("no_1_no") = lblNo1.Text
        nr("remark") = lblRemark.Text
        nr("act_amt") = ValComa(lblAct_Amt.Text)
        nr("bank") = lblBank.Text
        nr("no_2_no") = intNo2 + bm.Count + 1
        myDatasetS.Tables("acf010s").Rows.Add(nr)                          '增行至source grid
        lblTotAmt.Text = FormatNumber(ValComa(lblTotAmt.Text) + ValComa(lblAct_Amt.Text), 2)
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

    Private Sub cboName_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cboName.MouseDown
        intI = cboName.FindString(Trim(txtChkname.Text))
        cboName.SelectedIndex = intI         '將受款人置combo
    End Sub

    Private Sub cboName_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboName.SelectionChangeCommitted
        txtChkname.Text = cboName.Text
    End Sub

    Private Sub btnFinish_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFinish.Click
        Dim intI, intNo As Integer
        Dim tempdataset As DataSet
        If bm.Count = 0 Then
            MsgBox("無傳票資料")
            TabControl1.SelectedIndex = 0
            txtNo1.Focus()
            Exit Sub
        End If

        If txtChkNo.Text = "" Then
            MsgBox("支票碼錯誤")
            txtChkNo.Focus()
            Exit Sub
        End If

        '檢查支票碼重複
        sqlstr = "SELECT chkno FROM chf010 where kind='2' and chkno='" & txtChkNo.Text & "' and accyear=" & sYear
        tempdataset = openmember("", "chf010", sqlstr)
        If tempdataset.Tables("chf010").Rows.Count > 0 Then
            MsgBox("支票碼重複")
            txtChkNo.Focus()
            Exit Sub
        End If
        tempdataset = Nothing

        '將支票號碼寫入acf010->chkno & 新增一筆資料至chf010 
        For intI = 0 To bm.Count - 1
            bm.Position = intI
            intNo = bm.Current("no_1_no")
            intNo2 += 1
            sqlstr = "update acf010 set no_2_no=" & intNo2 & ", date_2 = '" & genLongDate(sDate) & _
                     "', chkno='" & txtChkNo.Text & "', chkseq=" & intI + 1 & " where accyear=" & sYear & " and kind='2' and no_1_no=" & intNo
            retstr = runsql(mastconn, sqlstr)
            If retstr <> "sqlok" Then
                MsgBox("支票號碼填入acf010->chkno失敗,請檢查" & sqlstr)
            End If
            GenUpdsql("no_2_no", intNo2, "N")
            sqlstr = "update acf020 set no_2_no=" & intNo2 & " where accyear=" & sYear & " and kind='2' and no_1_no=" & intNo
            retstr = runsql(mastconn, sqlstr)
            If retstr <> "sqlok" Then MsgBox("填入acf020支款號錯誤" & sqlstr)
            '檢查是否為銀行轉帳,要刪除第一項及第二項
            sqlstr = "select accno from acf010 where kind='2' and item='1' and accyear=" & sYear & " and no_1_no=" & intNo
            mydsS = openmember("", "acf010t", sqlstr)
            If mydsS.Tables("acf010t").Rows.Count > 0 Then
                sqlstr = nz(RTrim(mydsS.Tables("acf010t").Rows(0).Item("accno")), "")
                If sqlstr = "11102" Or sqlstr = "11103" Then
                    sqlstr = "delete from acf010 where kind='2' and item='1' and accyear=" & sYear & " and no_1_no=" & intNo
                    retstr = runsql(mastconn, sqlstr)
                    sqlstr = "delete from acf020 where kind='2' and item='2' and accyear=" & sYear & " and no_1_no=" & intNo
                    retstr = runsql(mastconn, sqlstr)
                End If
            End If
        Next
        GenInsSql("accyear", sYear, "N")
        GenInsSql("kind", "2", "T")
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

        '將支票金額加入本日共支欄(chf020->day_pay) & update chkno
        '列印碼清空
        sqlstr = "update chf020 set day_pay = day_pay + " & ValComa(lblTotAmt.Text) & ", prt_code = ' ', date_2 = '" & _
                 genLongDate(sDate) & "' where bank='" & sBank & "'"
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then
            MsgBox("將支票金額加入本日共支欄(chf020->day_pay)失敗,請檢查" & sqlstr)
        End If

        arrayNo = ""                          '清除該張支票所含之傳票號
        myDatasetS.Tables("acf010s").Clear()  '清除datagrid1資料

        '更正支款編號acfno 
        sqlstr = "update acfno set cont_no=" & intNo2 & " where accyear=" & sYear & " and kind='5'"
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then MsgBox("更正支款編號acfno錯誤" & sqlstr)

        lblMsg.Text = "記帳完成"
        lblTotAmt.Text = ""
        txtNo1.Text = ""
        txtNo1.Focus()
        btnSure.Enabled = False
        TabControl1.SelectedIndex = 0         '回datagrid PAGE 1 
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        TabControl1.SelectedIndex = 0
    End Sub
End Class
