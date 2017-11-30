Imports CHIA.CheckPrint
Public Class PAY021
    Dim sDate, sqlstr, sBank, retstr, errFlag As String   '開票日
    Dim sYear, intTemp, intI, intQ As Integer
    Dim intChkForm As Integer = 1
    Dim mydsS As DataSet
    Dim LoadAfter, Dirty As Boolean
    Dim bm As BindingManagerBase
    Dim myDatasetS, mydataset, psDataSet, dsSeq As DataSet
    Dim arrayNo As String
    Dim intCnt As Integer = 0
    'Friend WithEvents RecMove1 As pay.RecMove '定義傳票筆數
    Dim strCheckForm As String = "PAPER"    '設定支票是紙張PAPER or 電子轉帳TRANS

    Private Sub pay021_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim sqlstr As String
        'LoadAfter = True
        intI = 0
        arrayNo = ""  '將傳票置字串
        sDate = TransPara.TransP("userdate") 'put the login date to 開票日
        sYear = GetYear(sDate)   '年度
        sqlstr = "select no_1_no, remark, act_amt, bank, chkno from acf010 where accyear=0" 'put the struture to the acf010s dataset 
        myDatasetS = openmember("", "acf010s", sqlstr)
        RecMove1.FilesPara = "mastconn=" & mastconn & "|sqlstr=" & sqlstr
        DataGrid1.DataSource = myDatasetS
        DataGrid1.DataMember = "acf010s"
        bm = Me.BindingContext(myDatasetS, "acf010s")
        RecMove1.Setds = bm
        RecMove1.RecIns.Enabled = False
        RecMove1.RecUpdCMD.Enabled = False

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
        txtNo1.Focus()

        If TransPara.TransP("UnitTitle").indexof("嘉南") >= 0 Then
            GroupBox1.Visible = True
            Label17.Visible = True
            lblAreaName.Visible = True
            lblarea.Visible = True
        Else
            GroupBox1.Visible = False
            Label17.Visible = False
            lblAreaName.Visible = False
            lblarea.Visible = False
        End If
    End Sub

    Private Sub btnSureNo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSureNo.Click
        If Trim(txtNo1.Text) = "" Then
            MsgBox("請輸入傳票")
            txtNo1.Focus()
            Exit Sub
        End If
        If arrayNo.IndexOf(Format(Val(txtNo1.Text), "00000")) >= 0 Then
            MsgBox("傳票重複")
            txtNo1.Text = ""
            txtNo1.Focus()
            Exit Sub
        End If
        sqlstr = "select date_2, no_1_no, no_2_no, remark, act_amt, bank, chkno from acf010 where kind='2' and item='9' and accyear=" & _
                  sYear & " and no_1_no=" & ValComa(Trim(txtNo1.Text))
        mydsS = openmember("", "acf010t", sqlstr)
        If mydsS.Tables("acf010t").Rows.Count = 0 Then
            MsgBox("無此傳票")
            txtNo1.Text = ""
            txtNo1.Focus()
            Exit Sub
        End If
        If Not IsDBNull(mydsS.Tables("acf010t").Rows(0).Item("date_2")) Then
            MsgBox("此傳票已入帳," & mydsS.Tables("acf010t").Rows(0).Item("no_2_no") & "號")
            txtNo1.Text = ""
            txtNo1.Focus()
            Exit Sub
        End If
        If Not IsDBNull(mydsS.Tables("acf010t").Rows(0).Item("chkno")) Then
            If Trim(mydsS.Tables("acf010t").Rows(0).Item("chkno")) <> "" Then
                MsgBox("此傳票已開支票" & mydsS.Tables("acf010t").Rows(0).Item("chkno"))
                txtNo1.Text = ""
                txtNo1.Focus()
                Exit Sub
            End If
        End If

        If intCnt > 0 Then   '第二筆以後要檢察銀行要相同
            If mydsS.Tables("acf010t").Rows(0).Item("bank") <> lblBank.Text Then
                MsgBox("銀行不相同,請重輸入傳票" & mydsS.Tables("acf010t").Rows(0).Item("bank"))
                txtNo1.Text = ""
                txtNo1.Focus()
                Exit Sub
            End If
        End If
        lblNo1.Text = mydsS.Tables("acf010t").Rows(0).Item("no_1_no")
        lblBank.Text = mydsS.Tables("acf010t").Rows(0).Item("bank")
        If TransPara.TransP("UnitTitle").indexof("嘉南") >= 0 Then
            lblarea.Text = nz(mydsS.Tables("acf010t").Rows(0).Item("area"), "")
            lblAreaName.Text = nz(mydsS.Tables("acf010t").Rows(0).Item("areaname"), "")
        End If


        lblAct_Amt.Text = FormatNumber(mydsS.Tables("acf010t").Rows(0).Item("act_amt"), 0)
        lblRemark.Text = Trim(mydsS.Tables("acf010t").Rows(0).Item("remark"))
        If intCnt = 0 Then    '第一筆
            sBank = lblBank.Text '記錄第一筆銀行以便控制銀行要相同
            intTemp = lblRemark.Text.IndexOf("  ")   '由摘要空二格取受款人
            'If intTemp <= 0 Then intTemp = Len(lblRemark.Text) - 7 '找不到則取最後五個字
            txtRemark.Text = lblRemark.Text
            If intTemp <= 0 Then
                intTemp = Len(lblRemark.Text)
                txtChkname.Text = Microsoft.VisualBasic.Right(lblRemark.Text, IIf(intTemp >= 5, 5, intTemp))
            Else
                txtChkname.Text = Trim(Mid(lblRemark.Text, intTemp + 2))      '由摘要取受款人
            End If
            lblMsg.Text = ""
            sqlstr = "SELECT * FROM chf020 WHERE bank = '" & lblBank.Text & "'"
            mydataset = openmember("", "chf020", sqlstr)
            If mydataset.Tables("chf020").Rows.Count > 0 Then
                With mydataset.Tables("chf020").Rows(0)
                    lblBankName.Text = .Item("bank") & .Item("bankname")
                    '帳戶餘額
                    lblBalance.Text = FormatNumber(.Item("balance") + .Item("day_income") - .Item("day_pay") - .Item("unpay") + .Item("credit"), 2)
                    '支票號+1
                    txtChkNo.Text = nz(.Item("chkno"), " ")
                    txtChkNo.Text = AddCheckNo(txtChkNo.Text)
                    intChkForm = nz(.Item("chkform"), 1)  '未定支票格式者則給土銀格式
                End With
            End If
            btnSure.Visible = True
            btnFinish.Visible = True
            mydataset = Nothing

            '電子 add
            If TransPara.TransP("blnUseTR") = True Then
                btnSureTR.Visible = True   '顯示"開立電子支票"button 
            End If

        End If

        If ValComa(lblBalance.Text) < ValComa(lblTotAmt.Text) + ValComa(lblAct_Amt.Text) Then
            MsgBox("存款不足,存款餘額:" & lblBalance.Text & ",不足支付" & (ValComa(lblTotAmt.Text) + ValComa(lblAct_Amt.Text)))
            txtNo1.Text = ""
            txtNo1.Focus()
            Exit Sub
        End If
        Call AddDataGrid()
        txtNo1.Text = ""
        txtNo1.Focus()
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

    '按開立支票button
    Private Sub btnSure_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSure.Click
        lblTotamt2.Text = FormatNumber(ValComa(lblTotAmt.Text), 0)
        TabControl1.SelectedIndex = 1

        strCheckForm = "PAPER"  '電子轉帳 add
        lblChkTR.Text = ""      '電子轉帳 add
    End Sub

    '電子轉帳 add ( 按開立轉帳支票butten)
    Private Sub btnSureTR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSureTR.Click
        Call btnSure_Click(New System.Object, New System.EventArgs)  '必須先執行
        strCheckForm = "TRANS"  '再設定為TRANS
        lblChkTR.Text = "本支票採電子轉帳付款,支票號由系統自動編號"
        txtChkNo.Text = "TR" & Format(sYear, "000") & Format(QueryNO(sYear, "T") + 1, "00000") '取ACFNO KIND='T' 號數 TRyyyXXXXX
    End Sub

    Private Sub TabControl1_TabIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.TabIndexChanged
        ' 與btnsure.click相同
        If TabControl1.SelectedIndex = 1 Then lblTotamt2.Text = FormatNumber(ValComa(lblTotAmt.Text), 0)
    End Sub

    Sub AddDataGrid()
        intCnt += 1
        Dim nr As DataRow
        arrayNo = arrayNo & Format(Val(lblNo1.Text), "00000") & ","   '將傳票置入字串(最後放入資料庫時,會將逗號取消掉)
        nr = myDatasetS.Tables("acf010s").NewRow()
        nr("no_1_no") = lblNo1.Text
        nr("remark") = lblRemark.Text
        nr("act_amt") = ValComa(lblAct_Amt.Text)
        nr("bank") = lblBank.Text

        If TransPara.TransP("UnitTitle").indexof("嘉南") >= 0 Then
            nr("area") = lblarea.Text
            nr("areaname") = lblAreaName.Text
        End If


        myDatasetS.Tables("acf010s").Rows.Add(nr)                          '增行至source grid
        lblTotAmt.Text = FormatNumber(ValComa(lblTotAmt.Text) + ValComa(lblAct_Amt.Text), 0)
        lblTotNo.Text = FormatNumber(intCnt, 0)
    End Sub

    Sub transjob(ByVal JobName As String, ByVal JobPara As String) Handles RecMove1.TransJob
        Dim LastPos As Integer = bm.Position
        Select Case JobName
            Case "刪除記錄"
                bm.Position = LastPos
                lblMsg.Text = LastPos
                lblTotAmt.Text = FormatNumber(ValComa(lblTotAmt.Text) - bm.Current("Act_Amt"), 0)
                arrayNo = arrayNo.Replace(Format(bm.Current("no_1_no"), "00000") & ",", "") '將傳票置array
                myDatasetS.Tables("acf010s").Rows.RemoveAt(JobPara)
                intCnt -= 1   '筆數減1
                lblTotNo.Text = FormatNumber(intCnt, 0)
            Case "記錄移動"
                Dirty = False
        End Select
    End Sub

    Private Sub cboName_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboName.SelectionChangeCommitted
        txtChkname.Text = cboName.Text
    End Sub

    Private Sub cboName_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cboName.MouseDown
        intI = cboName.FindString(Trim(txtChkname.Text))
        cboName.SelectedIndex = intI         '將受款人置combo
    End Sub

    Private Sub btnAddPsname_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddPsname.Click
        If txtChkname.Text <> "" Then
            txtChkname.Text = Trim(txtChkname.Text)
            Dim ii As Integer
            ii = MsgBox("將 " & txtChkname.Text & "增入片語檔", MsgBoxStyle.OKCancel)
            If ii = 1 Then  ' click the ok botton
                sqlstr = "insert into psname (unit, seq, psstr) values ('0403', 0, N'" & txtChkname.Text & "')"
                runsql(mastconn, sqlstr)
            End If
        End If
    End Sub

    Private Sub btnFinish_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFinish.Click
        If lblTotAmt.Text = "" Or intCnt = 0 Then   '金額=0 or 沒有傳票
            TabControl1.SelectedIndex = 0
            Exit Sub
        End If
        Dim intI, intNo As Integer
        Dim Bankbalance As Decimal
        Dim tempdataset As DataSet

        '電子轉帳 add 
        If strCheckForm = "TRANS" Then
            '真正取轉帳支票號
            txtChkNo.Text = "TR" & Format(sYear, "000") & Format(RequireNO(mastconn, sYear, "T"), "00000")  '取ACFNO KIND='T' 號數 TRyyyXXXXX
        End If

        '檢查支票碼重複
        sqlstr = "SELECT chkno FROM chf010 where kind='2' and chkno='" & txtChkNo.Text & "' and accyear=" & sYear
        tempdataset = openmember("", "chf010", sqlstr)
        If tempdataset.Tables("chf010").Rows.Count > 0 Then
            MsgBox("支票碼重複")
            Exit Sub
        End If

        ' 新增一筆資料至chf010
        GenInsSql("accyear", sYear, "N")
        GenInsSql("kind", "2", "T")
        GenInsSql("bank", sBank, "T")
        GenInsSql("chkno", txtChkNo.Text, "T")
        GenInsSql("date_1", sDate, "D")
        GenInsSql("chkname", txtChkname.Text, "U")
        GenInsSql("remark", txtRemark.Text, "U")
        GenInsSql("amt", lblTotAmt.Text, "N")
        GenInsSql("no_1_no", arrayNo, "T")
        sqlstr = "insert into chf010 " & GenInsFunc
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then
            MsgBox("新增支票寫入chf010失敗,請檢查" & sqlstr)
            Exit Sub
        End If

        tempdataset = Nothing

        '將支票號碼寫入acf010->chkno  item='9'  
        For intI = 1 To intCnt    '傳票張數
            intNo = Val(Mid(arrayNo, (intI - 1) * 6 + 1, 5))
            If intNo = 0 Then Exit For
            sqlstr = "update acf010 set chkno='" & txtChkNo.Text & "', chkseq=" & intI & " where accyear=" & sYear & _
                     " and no_1_no=" & intNo & " and kind='2' and item='9'"
            retstr = runsql(mastconn, sqlstr)
            If retstr <> "sqlok" Then
                MsgBox("支票號碼寫入acf010->chkno失敗,請檢查" & sqlstr)
            End If
        Next

        '將支票金額加入已開未領欄(chf020->unpay) & update chkno

        '電子轉帳 add & mod
        If strCheckForm = "TRANS" Then
            sqlstr = "update chf020 set unpay = unpay + '" & ValComa(lblTotAmt.Text) & "' where bank='" & sBank & "'"
        Else
            sqlstr = "update chf020 set unpay = unpay + '" & ValComa(lblTotAmt.Text) & "', chkno = '" & _
                      txtChkNo.Text & "' where bank='" & sBank & "'"
        End If
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then
            MsgBox("將支票金額加入已開未領欄(chf020->unpay)失敗,請檢查" & sqlstr)
        End If
        Bankbalance = ValComa(lblBalance.Text) - ValComa(lblTotAmt.Text)

        '電子轉帳 add & mod
        If strCheckForm = "TRANS" Then
            ' 新增一筆資料至chf050
            GenInsSql("accyear", sYear, "N")
            GenInsSql("vchkno", txtChkNo.Text, "T")
            GenInsSql("date_1", sDate, "D")
            GenInsSql("bank", sBank, "T")
            sqlstr = "insert into chf050 " & GenInsFunc
            retstr = runsql(mastconn, sqlstr)
            If retstr <> "sqlok" Then
                MsgBox("新增支票寫入chf050失敗,請檢查" & sqlstr)
                Exit Sub
            End If
        Else   '原程式
            '列印支票
            PrintCheck(sBank, lblBankName.Text, sDate, txtChkname.Text, ValComa(lblTotAmt.Text), txtChkNo.Text & txtRemark.Text, Bankbalance) '列印支票 at vbdataio.vb
        End If


        lblMsg.Text = " 上張支票" & txtChkNo.Text & " 處理完畢"
        arrayNo = ""                          '清除該張支票所含之傳票號
        myDatasetS.Tables("acf010s").Clear()  '清除datagrid1資料
        lblTotAmt.Text = ""
        txtChkNo.Text = ""
        btnSure.Visible = False
        btnSureTR.Visible = False '電子轉帳  add 
        btnFinish.Visible = False
        intCnt = 0       '傳票筆數=0
        lblTotNo.Text = ""
        TabControl1.SelectedIndex = 0         '回datagrid PAGE 1
        Me.WindowState = FormWindowState.Maximized   '畫面強制最大化
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        TabControl1.SelectedIndex = 0
    End Sub

    Private Sub btnSurePaySeq_Click(sender As Object, e As EventArgs) Handles btnSurePaySeq.Click
        intQ = 0 '匯款單傳票張數
        sqlstr = "SELECT no_1_no FROM transpay where accyear=" & sYear & _
                 " and payseq=" & txtPaySeq.Text & " and bank='" & txtBank.Text & _
                 "' and area='" & txtArea.Text & "' and item='2' order by no_1_no"
        dsSeq = openmember("", "transpay", sqlstr)
        With dsSeq.Tables("transpay")
            If .Rows.Count <= 0 Then
                MsgBox("無此資料")
                Exit Sub
            End If
            For intQ = 0 To .Rows.Count - 1
                txtNo1.Text = nz(.Rows(intQ).Item("no_1_no"), "")
                Call btnSureNo_Click(New System.Object, New System.EventArgs)
                If errFlag <> "N" Then
                    Exit Sub
                End If
            Next
        End With
    End Sub
End Class
