Public Class BGF010
    Dim confid, confunit As String, LoadAfter, Dirty As Boolean
    Dim sqlstr As String
    Dim bm As BindingManagerBase, mydataset, psDataSet, AccnoDataSet As DataSet
    Dim UserId, UserName, UserUnit As String
    Dim strYes As String = "N" '不允許使用者異動

    Private Sub BGF010_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '先由會計科目找異動可否控制碼
        sqlstr = "SELECT * FROM ACCNAME WHERE ACCNO='5'"
        mydataset = openmember("", "ACCNAME", sqlstr)
        If mydataset.Tables(0).Rows.Count <= 0 Then
            MsgBox("貴會會計科目檔少了科目=5的科目,請主計人員先行補上 ")
            Me.Close()
        End If
        If Mid(nz(mydataset.Tables(0).Rows(0).Item("account_no"), ""), 1, 1) = "Y" Then
            strYes = "Y"
        Else
            strYes = "N"
        End If
        TabControl1.Enabled = False
        RecMove1.Enabled = False
        nudYear.Value = GetYear(TransPara.TransP("Userdate"))
        UserUnit = TransPara.TransP("Userunit")
        UserId = TransPara.TransP("Userid")
        vxtStartNo.Text = "1"    '起值
        vxtEndNo.Text = "59"      '迄值
        If Mid(UserUnit, 1, 2) = "05" Then
            'sqlstr = "SELECT accno,accno+accname as accnamet from accname where NOT (staff_no='' OR staff_no is null) and outyear=0 "
            sqlstr = "SELECT accno,accno+accname as accnamet from accname where outyear=0 "
        Else
            sqlstr = "SELECT accno,accno+accname as accnamet from accname where staff_no='" & UserId & "' and outyear=0"
        End If
        AccnoDataSet = openmember("", "accname", sqlstr)
        If AccnoDataSet.Tables("accname").Rows.Count = 0 Then
            cboAccno.Text = "尚無預算科目"
        Else
            cboAccno.DataSource = AccnoDataSet.Tables("accname")
            cboAccno.DisplayMember = "accnamet"  '顯示欄位
            cboAccno.ValueMember = "accno"     '欄位值
        End If

    End Sub

    Private Sub BtnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSearch.Click
        TabControl1.Enabled = True
        RecMove1.Enabled = True
        Call LoadGridFunc()
        If DataGrid1.CurrentRowIndex > 0 Then
            bm.Position = 1
            Call PutGridToTxt()
        End If
    End Sub

    Sub LoadGridFunc()
        Dim sqlstr, qstr, strD, strC As String
        sqlstr = "SELECT a.bg1+a.bg2+a.bg3+a.bg4+a.bg5 as bgnet, a.*, " & _
                 "CASE WHEN len(a.accno) = 17 THEN c.ACCNAME+'-'+b.accname " & _
                 " WHEN len(a.accno) <> 17 THEN b.accname END AS accname" & _
                 " FROM  BGF010 a" & _
                 " LEFT OUTER JOIN accname b ON a.accno = b.accno" & _
                 " left outer join accname c ON LEFT(a.ACCNO, 16) = c.ACCNO and len(a.accno)=17 " & _
                 " WHERE accyear=" & nudYear.Value & " and a.accno>='" & _
                  GetAccno(vxtStartNo.Text) & "' and a.accno<='" & GetAccno(vxtEndNo.Text) & "'"
        If Mid(UserUnit, 1, 2) = "05" Then   '主計人員
            sqlstr = sqlstr & " ORDER BY a.accyear,a.accno"
        Else
            sqlstr = sqlstr & " and b.STAFF_NO = '" & Trim(UserId) & _
                     "' ORDER BY a.accyear,a.accno"
            'If Month(TransPara.TransP("Userdate")) > 1 And Month(TransPara.TransP("Userdate")) < 12 Then RecMove1.Enabled = False '超過1月後不許USER更動預算
            If strYes <> "Y" Then
                RecMove1.Enabled = False '不可異動
                lblMsgMod.Text = "目前主計室控制業務單位不可修改預算資料"
            Else
                RecMove1.Enabled = True '可異動
            End If
        End If
        mydataset = openmember("", "BGF010", sqlstr)
        RecMove1.FilesPara = "mastconn=" & mastconn & "|sqlstr=" & sqlstr
        DataGrid1.DataSource = mydataset
        DataGrid1.DataMember = "BGF010"
        bm = Me.BindingContext(mydataset, "BGF010")
        RecMove1.Setds = bm
        TabControl1.SelectedIndex = 0

    End Sub

    Sub PutGridToTxt()
        Dim intI, SumUp As Integer
        Dim strI, strColumn1, strColumn2 As String
        If bm.Position = -1 Then Exit Sub
        If IsDBNull(bm.Current("accno")) Then Exit Sub
        txtAccYear.Text = nz(bm.Current("accyear"), 0)
        lblYear.Text = txtAccYear.Text
        If Not IsDBNull(bm.Current("accno")) Then cboAccno.SelectedValue = bm.Current("accno")
        lblAccno.Text = cboAccno.SelectedValue
        txtUnit.Text = nz(bm.Current("unit"), "")
        txtTotper.Text = Format(nz(bm.Current("totper"), 0), "###,###,##0")
        txtTotuse.Text = Format(nz(bm.Current("totuse"), 0), "###,###,##0")
        txtFlow.Text = nz(bm.Current("flow"), "")
        txtCtrl.Text = nz(bm.Current("ctrl"), "")
        txtEngno.Text = nz(bm.Current("engno"), "")
        If Not IsDBNull(bm.Current("autono")) Then lblkey.Text = Trim(bm.Current("autono"))

        SumUp = 0
        For intI = 1 To 5
            strI = Format(intI, "0")
            strColumn1 = "bg" & strI
            strColumn2 = "up" & strI
            FindControl(Me, "txtbg" & strI).Text = FormatNumber(bm.Current(strColumn1), 0)               'function findcontrol at vbdataio.vb
            FindControl(Me, "txtup" & strI).Text = FormatNumber(bm.Current(strColumn2), 0)
            FindControl(Me, "lblNet" & strI).Text = FormatNumber(bm.Current(strColumn1) + bm.Current(strColumn2), 0)
            SumUp += bm.Current(strColumn2)
        Next
        lblSumBg.Text = FormatNumber(nz(bm.Current("bgnet"), 0), 0)
        lblSumBg.Text = FormatNumber(SumUp, 0)
        lblSumNet.Text = FormatNumber(nz(bm.Current("bgnet"), 0) + SumUp, 0)
        lbldate.Text = nz(bm.Current("systemdate"), Space(1))
    End Sub

    Sub transjob(ByVal JobName As String, ByVal JobPara As String) Handles RecMove1.TransJob
        Dim LastPos As Integer = bm.Position
        Select Case JobName
            Case "刪除記錄"
                Dim keyvalue, sqlstr, retstr As String
                Dim tempds As DataSet
                If TabControl1.SelectedIndex = 0 Then
                    keyvalue = bm.Current("autono")
                Else
                    keyvalue = Trim(lblkey.Text)
                End If
                sqlstr = "select count(*) as recno from bgf020 where accyear=" & lblYear.Text & " and accno='" & lblAccno.Text & "'"
                tempds = openmember("", "recno", sqlstr)
                If tempds.Tables(0).Rows(0).Item(0) > 0 Then  '該科目已有開支資料
                    MsgBox("該科目已有開支資料,筆數=" & tempds.Tables(0).Rows(0).Item(0))
                    Exit Sub
                End If
                sqlstr = "delete from BGF010 where autono=" & keyvalue
                retstr = runsql(mastconn, sqlstr)
                If retstr = "sqlok" Then
                    mydataset.Tables("BGF010").Rows.RemoveAt(JobPara)
                    mydataset.Tables("BGF010").Clear()
                    Call LoadGridFunc()
                    bm.Position = LastPos
                End If

            Case "回寫記錄cmd"
                Dim keyvalue, sqlstr, retstr, updstr, TDC As String
                If TabControl1.SelectedIndex = 0 Then
                    Call PutGridToTxt()
                End If
                keyvalue = Trim(lblkey.Text)
                TDC = IIf(Mid(cboAccno.SelectedValue, 1, 1) > "1" And Mid(cboAccno.SelectedValue, 1, 1) < "5", "2", "1")
                'RecMove1.GenUpdsql("accyear", txtAccYear.Text, "N")
                'RecMove1.GenUpdsql("accno", cboAccno.SelectedValue, "T")
                RecMove1.GenUpdsql("bg1", txtBg1.Text, "N")
                RecMove1.GenUpdsql("bg2", txtBg2.Text, "N")
                RecMove1.GenUpdsql("bg3", txtBg3.Text, "N")
                RecMove1.GenUpdsql("bg4", txtBg4.Text, "N")
                RecMove1.GenUpdsql("bg5", txtBg5.Text, "N")
                RecMove1.GenUpdsql("up1", txtUp1.Text, "N")
                RecMove1.GenUpdsql("up2", txtUp2.Text, "N")
                RecMove1.GenUpdsql("up3", txtUp3.Text, "N")
                RecMove1.GenUpdsql("up4", txtUp4.Text, "N")
                RecMove1.GenUpdsql("up5", txtUp5.Text, "N")
                RecMove1.GenUpdsql("unit", txtUnit.Text, "T")
                RecMove1.GenUpdsql("DC", TDC, "T")
                RecMove1.GenUpdsql("flow", txtFlow.Text, "T")
                RecMove1.GenUpdsql("ctrl", txtCtrl.Text, "T")
                RecMove1.GenUpdsql("engno", txtEngno.Text, "T")
                RecMove1.GenUpdsql("totper", txtTotper.Text, "N")
                RecMove1.GenUpdsql("totuse", txtTotuse.Text, "N")
                RecMove1.GenUpdsql("systemdate", Format(Now(), "yyyy/MM/dd HH:mm:ss"), "D")

                sqlstr = "update BGF010 set " & RecMove1.genupdfunc & " where autono=" & keyvalue
                retstr = runsql(mastconn, sqlstr)
                If retstr = "sqlok" Then
                    'mydataset.Tables("BGF010").Rows.RemoveAt(JobPara)
                    mydataset.Tables("BGF010").Clear()
                    Call LoadGridFunc()
                    'MsgBox("更新成功")
                    bm.Position = LastPos
                Else
                    MsgBox("更新失敗" & sqlstr)
                End If

            Case "新增記錄"
                Dim keyvalue, sqlstr, retstr, updstr, TDC As String
                If TabControl1.SelectedIndex = 0 Then
                    Call PutGridToTxt()
                End If
                sqlstr = "SELECT accno FROM bgf010 where accyear=" & txtAccYear.Text & " and accno='" & cboAccno.SelectedValue & "'"
                psDataSet = openmember("", "accname", sqlstr)
                If psDataSet.Tables("accname").Rows.Count > 0 Then
                    MsgBox("資料重複, " & "accyear=" & txtAccYear.Text & " and accno='" & cboAccno.SelectedValue & "'")
                    Exit Sub
                End If
                TDC = IIf(Mid(cboAccno.SelectedValue, 1, 1) > "1" And Mid(cboAccno.SelectedValue, 1, 1) < "5", "2", "1")
                RecMove1.GenInsSql("accyear", txtAccYear.Text, "N")
                RecMove1.GenInsSql("accno", cboAccno.SelectedValue, "T")
                RecMove1.GenInsSql("bg1", txtBg1.Text, "N")
                RecMove1.GenInsSql("bg2", txtBg2.Text, "N")
                RecMove1.GenInsSql("bg3", txtBg3.Text, "N")
                RecMove1.GenInsSql("bg4", txtBg4.Text, "N")
                RecMove1.GenInsSql("bg5", txtBg5.Text, "N")
                RecMove1.GenInsSql("up1", txtUp1.Text, "N")
                RecMove1.GenInsSql("up2", txtUp2.Text, "N")
                RecMove1.GenInsSql("up3", txtUp3.Text, "N")
                RecMove1.GenInsSql("up4", txtUp4.Text, "N")
                RecMove1.GenInsSql("up5", txtUp5.Text, "N")
                RecMove1.GenInsSql("DC", TDC, "T")
                RecMove1.GenInsSql("flow", txtFlow.Text, "T")
                RecMove1.GenInsSql("ctrl", txtCtrl.Text, "T")
                RecMove1.GenInsSql("unit", txtUnit.Text, "T")
                RecMove1.GenInsSql("engno", txtEngno.Text, "T")
                RecMove1.GenInsSql("totper", 0, "N")   '新增時總請購及開支=0
                RecMove1.GenInsSql("totuse", 0, "N")
                RecMove1.GenInsSql("systemdate", Format(Now(), "yyyy/MM/dd HH:mm:ss"), "D")
                sqlstr = "insert into BGF010 " & RecMove1.GenInsFunc
                retstr = runsql(mastconn, sqlstr)

                If retstr = "sqlok" Then
                    'mydataset.Tables("BGF010").Rows.RemoveAt(JobPara)
                    mydataset.Tables("BGF010").Clear()
                    Call LoadGridFunc()
                    If LastPos > 0 Then bm.Position = LastPos
                    'MsgBox("新增成功")
                Else
                    MsgBox("新增失敗" + sqlstr)
                End If
            Case "記錄移動"
                Dim keyvalue, sqlstr, retstr As String
                lblkey.Text = nz(bm.Current("autono"), 0)
                Call PutGridToTxt()
                Dirty = False
        End Select
    End Sub


    Private Sub TabControl1_SELECTEDIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        If LoadAfter = False Then Exit Sub
        ' If TabControl1.SelectedIndex = 1 Then Dirty = False
        If Dirty = True Then
            If MsgBox("資料尚未存檔 要放棄嗎?", MsgBoxStyle.DefaultButton1 + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                TabControl1.SelectedIndex = 1
                Dirty = False
            End If
        End If
    End Sub

    Private Sub DataGrid1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.Click
        If bm.Position < 0 Then Exit Sub
        lblkey.Text = nz(bm.Current("autono"), 0) 'keep the old keyvalue
        If lblkey.Text <> "0" Then Call PutGridToTxt()
        Dirty = False
    End Sub

    Private Sub DataGrid1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.DoubleClick
        If bm.Position < 0 Then Exit Sub
        lblkey.Text = nz(bm.Current("autono"), 0) 'keep the old keyvalue
        If lblkey.Text <> "0" Then
            Call PutGridToTxt()
            TabControl1.SelectedIndex = 1
        End If
        Dirty = False
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnFour_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFour.Click
        Dim sumbg, sbg As Integer
        sumbg = ValComa(txtBg1.Text) + ValComa(txtBg2.Text) + ValComa(txtBg3.Text) + ValComa(txtBg4.Text) - ValComa(txtBg5.Text)
        sbg = Math.Round(sumbg / 4, 0)
        txtBg1.Text = FormatNumber(sbg, 0)
        txtBg2.Text = FormatNumber(sbg, 0)
        txtBg3.Text = FormatNumber(sbg, 0)
        txtBg4.Text = FormatNumber(sumbg - (sbg * 3), 0)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        txtBg1.Text = "0"
        txtBg2.Text = "0"
        txtBg3.Text = "0"
        txtBg4.Text = "0"
    End Sub

    Private Sub txtBg1_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBg1.LostFocus, txtUp1.LostFocus, _
                txtBg2.LostFocus, txtUp2.LostFocus, txtBg3.LostFocus, txtUp3.LostFocus, txtBg4.LostFocus, txtUp4.LostFocus, txtBg5.LostFocus, txtUp5.LostFocus
        If Not IsNumeric(sender.text) And Trim(sender.text) <> "" Then
            MsgBox("請輸入正數字")
            sender.focus()
        Else
            Dim strI As String = Mid(sender.name, 6, 1)
            If sender.text <> "" Then
                sender.text = FormatNumber(ValComa(sender.text), 0)
            End If
            FindControl(Me, "lblnet" & strI).Text = FormatNumber(ValComa(FindControl(Me, "txtbg" & strI).Text) + ValComa(FindControl(Me, "txtup" & strI).Text), 0)
            lblSumBg.Text = FormatNumber(ValComa(txtBg1.Text) + ValComa(txtBg2.Text) + ValComa(txtBg3.Text) + ValComa(txtBg4.Text) + ValComa(txtBg5.Text), 0)
            lblSumUp.Text = FormatNumber(ValComa(txtUp1.Text) + ValComa(txtUp2.Text) + ValComa(txtUp3.Text) + ValComa(txtUp4.Text) + ValComa(txtUp5.Text), 0)
            lblSumNet.Text = FormatNumber(ValComa(lblSumBg.Text) + ValComa(lblSumUp.Text), 0)
        End If
    End Sub

    Private Sub txtAccYear_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccYear.LostFocus, txtTotper.LostFocus, txtTotuse.LostFocus
        If (Not IsNumeric(sender.text) Or ValComa(sender.text) < 0) And Trim(sender.text) <> "" Then
            MsgBox("請輸入正數字")
            sender.focus()
        Else
            sender.text = Format(ValComa(sender.text), "###,###,###,###")
        End If
        'If sender.name = "txtAccYear" Then
        '    If ValComa(txtAccYear.Text) > Year(Date.Today) Then    '允許使用者建下年度預算
        '        If RecMove1.Enabled = False Then RecMove1.Enabled = True
        '    End If
        'End If
    End Sub

    Private Sub btnCboAccno_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCboAccno.Click
        If Mid(UserUnit, 1, 2) = "05" Then
            sqlstr = "SELECT accno,accno+accname as accnamet from accname where NOT (staff_no='' OR staff_no is null) and outyear=0 "
        Else
            sqlstr = "SELECT accno,accno+accname as accnamet from accname where staff_no='" & UserId & "' and outyear=0"
        End If
        AccnoDataSet = openmember("", "accname", sqlstr)
        If AccnoDataSet.Tables("accname").Rows.Count = 0 Then
            cboAccno.Text = "尚無預算科目"
        Else
            cboAccno.DataSource = AccnoDataSet.Tables("accname")
            cboAccno.DisplayMember = "accnamet"  '顯示欄位
            cboAccno.ValueMember = "accno"     '欄位值
        End If
    End Sub
End Class
