Public Class ACF010
    Dim confid, confunit As String, LoadAfter, Dirty As Boolean
    Dim sqlstr As String
    Dim bm As BindingManagerBase, mydataset As DataSet, mydataset2 As DataSet
    Dim UserId, UserName, UserUnit As String
    Dim Syear As Int32
    Dim Sfile As Char
    Dim Sno, Eno As Int32
    Dim Skind, Ekind As Char

    Private Sub acf010_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Dim myds As DataSet
        LoadAfter = True
        Syear = CInt(nudYear.Text)
        UserName = TransPara.TransP("username")
        UserUnit = TransPara.TransP("userunit")
        UserId = TransPara.TransP("userid")
        'TabControl1.Enabled = False
        RecMove1.Enabled = False
        nudYear.Value = GetYear(Now)

        '彰化不要增修刪
        If TransPara.TransP("UnitTitle").indexof("彰化") >= 0 Then
            '彰化只允許ch418 可modify
            If UserId <> "0418" Then
                GroupBox3.Visible = False
            End If
        End If

        'dtpdate1.CustomFormat = "yyyy/MM/dd"
        'dtpdate1.Format = DateTimePickerFormat.Custom
        'dtpdate1.CustomFormat = "yyyy/MM/dd"
        'dtpdate2.Format = DateTimePickerFormat.Custom
        'dtpdate2.CustomFormat = "yyyy/MM/dd"
    End Sub

    Private Sub BtnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSearch.Click
        Syear = nudYear.Value
        Sfile = IIf(rdbFile1.Checked = True, "1", "2")
        If rdbfile3.Checked = True Then Sfile = "3"
        If Val(TxtEndNo.Text) = 0 Then TxtEndNo.Text = TxtStartNo.Text
        Sno = CInt(Trim(TxtStartNo.Text))
        Eno = CInt(Trim(TxtEndNo.Text))
        If Eno < Sno Then Eno = Sno
        If rdbKind1.Checked = True Then Skind = "1"
        If rdbkind2.Checked = True Then Skind = "2"
        If rdbkind3.Checked = True Then Skind = "3"
        Ekind = Skind
        If Skind = "3" Then Ekind = "4"
        TabControl1.Enabled = True
        RecMove1.Enabled = True
        Call LoadGridFunc()
        If DataGrid1.CurrentRowIndex > 0 Then Call PutGridToTxt()
    End Sub

    Sub LoadGridFunc()
        Dim sqlstr, qstr, sortstr As String
        sqlstr = "SELECT * FROM  acf010 where accyear=" & Syear & " and kind>='" & Skind & "' and kind<='" & Ekind & "' and "
        Select Case Sfile
            Case "1"   '未處理
                sqlstr = sqlstr & "no_1_no>=" & Sno & " and no_1_no<=" & Eno & " and date_2 is null"
                sortstr = " order by accyear, kind, no_1_no, seq, item"    '排序
            Case "2"   '已處理
                sqlstr = sqlstr & "no_2_no>=" & Sno & " and no_2_no<=" & Eno & " and date_2 is not null"
                sortstr = " order by accyear, kind, no_2_no, seq, item"
            Case "3"  '全部
                sqlstr = sqlstr & "no_1_no>=" & Sno & " and no_1_no<=" & Eno
                sortstr = " order by accyear, kind, no_1_no, seq, item"
        End Select
        If txtQremark.Text <> "" Then qstr = " and remark like '%" & Trim(txtQremark.Text) & "%'"
        If txtQaccno.Text <> "" Then qstr = qstr + " and accno like '%" & Trim(txtQaccno.Text) & "%'"
        If ValComa(txtQamt.Text) <> 0 Then qstr = qstr + " and amt =" & ValComa(txtQamt.Text)
        sqlstr = sqlstr + qstr + sortstr
        mydataset = openmember("", "acf010", sqlstr)
        RecMove1.FilesPara = "mastconn=" & mastconn & "|sqlstr=" & sqlstr
        DataGrid1.DataSource = mydataset
        DataGrid1.DataMember = "acf010"
        bm = Me.BindingContext(mydataset, "acf010")
        RecMove1.Setds = bm
        TabControl1.SelectedIndex = 0
    End Sub

    Sub PutGridToTxt()
        txtYear.Text = bm.Current("accyear")
        txtKind.Text = bm.Current("kind")
        txtNo1.Text = bm.Current("no_1_no")
        txtNo2.Text = bm.Current("no_2_no")
        txtSeq.Text = bm.Current("seq")
        txtItem.Text = bm.Current("item")
        txtDate1.Text = IIf(IsDBNull(bm.Current("date_1")), "", bm.Current("date_1"))
        txtDate2.Text = IIf(IsDBNull(bm.Current("date_2")), "", bm.Current("date_2"))
        txtDC.Text = bm.Current("dc")
        vxtAccno.Text = bm.Current("accno")
        txtRemark.Text = IIf(IsDBNull(bm.Current("remark")), "", bm.Current("remark"))
        txtamt.Text = FormatNumber(bm.Current("amt"), 2)
        txtActamt.Text = FormatNumber(bm.Current("act_amt"), 2)
        txtBank.Text = bm.Current("bank")
        txtchkno.Text = IIf(IsDBNull(bm.Current("chkno")), "", bm.Current("chkno"))
        txtBooks.Text = bm.Current("books")
    End Sub

    Sub transjob(ByVal JobName As String, ByVal JobPara As String)
        Dim LastPos As Integer = bm.Position
        Select Case JobName
            Case "刪除記錄"
                Dim keyvalue, sqlstr, retstr As String
                If TabControl1.SelectedIndex = 0 Then
                    keyvalue = bm.Current("autono")
                Else
                    keyvalue = lblkey.Text
                End If

                sqlstr = "delete from acf010 where autono=" & keyvalue
                retstr = runsql(mastconn, sqlstr)
                If retstr = "sqlok" Then
                    mydataset.Tables("acf010").Rows.RemoveAt(JobPara)
                    bm.Position = LastPos
                End If

            Case "回寫記錄cmd"
                Dim keyvalue, sqlstr, retstr, updstr As String
                If TabControl1.SelectedIndex = 0 Then
                    Call PutGridToTxt()
                End If
                keyvalue = lblkey.Text

                RecMove1.GenUpdsql("accyear", Trim(txtYear.Text), "N")
                RecMove1.GenUpdsql("kind", Trim(txtKind.Text), "T")
                RecMove1.GenUpdsql("no_1_no", Trim(txtNo1.Text), "N")
                RecMove1.GenUpdsql("no_2_no", Trim(txtNo2.Text), "N")
                RecMove1.GenUpdsql("seq", Trim(txtSeq.Text), "T")
                RecMove1.GenUpdsql("item", Trim(txtItem.Text), "T")
                RecMove1.GenUpdsql("date_1", txtDate1.Text.ToString, "D")
                RecMove1.GenUpdsql("date_2", txtDate2.Text.ToString, "D")
                RecMove1.GenUpdsql("dc", Trim(txtDC.Text), "T")
                RecMove1.GenUpdsql("accno", GetAccno(vxtAccno.Text), "T")
                RecMove1.GenUpdsql("remark", Trim(txtRemark.Text), "U")
                RecMove1.GenUpdsql("amt", Trim(txtamt.Text), "N")
                RecMove1.GenUpdsql("act_amt", Trim(txtActamt.Text), "N")
                RecMove1.GenUpdsql("bank", Trim(txtBank.Text), "T")
                RecMove1.GenUpdsql("chkno", Trim(txtchkno.Text), "T")
                RecMove1.GenUpdsql("books", Trim(txtBooks.Text), "T")
                sqlstr = "update acf010 set " & RecMove1.genupdfunc & " where autono=" & keyvalue
                retstr = runsql(mastconn, sqlstr)
                If retstr = "sqlok" Then
                    Call LoadGridFunc()
                    bm.Position = LastPos
                Else
                    MsgBox("更新失敗" & sqlstr)
                End If

            Case "新增記錄"
                Dim keyvalue, sqlstr, retstr, updstr As String
                If TabControl1.SelectedIndex = 0 Then
                    Call PutGridToTxt()
                End If
                RecMove1.GenInsSql("accyear", Trim(txtYear.Text), "N")
                RecMove1.GenInsSql("kind", Trim(txtKind.Text), "T")
                RecMove1.GenInsSql("no_1_no", Trim(txtNo1.Text), "N")
                RecMove1.GenInsSql("no_2_no", Trim(txtNo2.Text), "N")
                RecMove1.GenInsSql("seq", Trim(txtSeq.Text), "T")
                RecMove1.GenInsSql("item", Trim(txtItem.Text), "T")
                RecMove1.GenInsSql("date_1", txtDate1.Text.ToString, "D")
                RecMove1.GenInsSql("date_2", txtDate2.Text.ToString, "D")
                RecMove1.GenInsSql("dc", Trim(txtDC.Text), "T")
                RecMove1.GenInsSql("accno", GetAccno(vxtAccno.Text), "T")
                RecMove1.GenInsSql("remark", Trim(txtRemark.Text), "U")
                RecMove1.GenInsSql("amt", txtamt.Text, "N")
                RecMove1.GenInsSql("act_amt", Trim(txtActamt.Text), "N")
                RecMove1.GenInsSql("bank", Trim(txtBank.Text), "T")
                RecMove1.GenInsSql("chkno", Trim(txtchkno.Text), "T")
                RecMove1.GenInsSql("books", Trim(txtBooks.Text), "T")

                sqlstr = "insert into acf010 " & RecMove1.GenInsFunc
                retstr = runsql(mastconn, sqlstr)

                If retstr = "sqlok" Then
                    Call LoadGridFunc()
                    bm.Position = LastPos
                Else
                    MsgBox("新增失敗" & sqlstr)
                End If
            Case "記錄移動"
                Dim keyvalue, sqlstr, retstr As String
                lblkey.Text = bm.Current("autono")
                lblAccname.Text = ""
                Call PutGridToTxt()
                Dirty = False
        End Select
    End Sub

    Private Sub TabControl1_SELECTEDIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        If LoadAfter = False Then Exit Sub
        If Dirty = True Then
            If MsgBox("資料尚未存檔 要放棄嗎?", MsgBoxStyle.DefaultButton1 + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                TabControl1.SelectedIndex = 1
                Dirty = False
            End If
        End If
    End Sub

    Private Sub DataGrid1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.Click
        lblkey.Text = bm.Current("autono")  'keep the old keyvalue
        Call PutGridToTxt()
        Dirty = False
    End Sub

    Private Sub DataGrid1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.DoubleClick
        TabControl1.SelectedIndex = 1
    End Sub


    Private Sub btnAccname_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAccname.Click
        Dim sqlstr As String
        sqlstr = "SELECT ACCNAME FROM ACCNAME WHERE ACCNO = '" & GetAccno(vxtAccno.Text) & "'"
        mydataset2 = openmember("", "accname", sqlstr)
        If mydataset2.Tables("accname").Rows.Count = 0 Then
            lblAccname.Text = "無此代號"
        Else
            lblAccname.Text = mydataset2.Tables("accname").Rows(0).Item(0)
        End If
        mydataset2 = Nothing
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub txtDate1_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDate1.LostFocus, txtDate2.LostFocus
        sender.Text = checkdate(sender.Text)
    End Sub

    Private Sub txtYear_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
             txtYear.LostFocus, txtKind.LostFocus, txtNo1.LostFocus, txtNo2.LostFocus, txtSeq.LostFocus, txtItem.LostFocus, _
             txtDC.LostFocus, TxtStartNo.LostFocus, TxtStartNo.LostFocus, TxtEndNo.LostFocus
        If (Not IsNumeric(sender.Text) Or ValComa(sender.text) < 0) And Trim(sender.text) <> "" Then
            MsgBox("請輸入正數字")
            sender.Focus()
        End If
    End Sub

    Private Sub txtamt_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtQamt.LostFocus, _
                                                                           txtamt.LostFocus, txtActamt.LostFocus
        If (Not IsNumeric(sender.Text) Or ValComa(sender.text) < 0) And Trim(sender.text) <> "" Then
            MsgBox("請輸入正數字")
            sender.Focus()
        Else
            sender.text = FormatNumber(ValComa(sender.text), 2)
        End If
    End Sub
End Class
