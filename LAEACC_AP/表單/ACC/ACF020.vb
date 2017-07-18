Public Class ACF020
    Dim confid, confunit As String, LoadAfter, Dirty As Boolean
    Dim sqlstr As String
    Dim bm As BindingManagerBase, mydataset As DataSet, mydataset2 As DataSet
    Dim UserId, UserName, UserUnit As String
    Dim Syear As Int32
    Dim Sfile As Char
    Dim Sno, Eno As Int32
    Dim Skind, Ekind As Char

    Private Sub ACF020_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Dim myds As DataSet
        LoadAfter = True
        Syear = CInt(nudYear.Text)
        UserName = TransPara.TransP("username")
        UserUnit = TransPara.TransP("userunit")
        UserId = TransPara.TransP("userid")
        'TabControl1.Enabled = False
        RecMove1.Enabled = False

        '彰化不要增修刪
        If TransPara.TransP("UnitTitle").indexof("彰化") >= 0 Then
            '彰化只允許ch468 可modify
            If UserId <> "0418" Then
                RecMove1.Visible = False
            End If
        End If

        nudYear.Value = GetYear(Now)
        'Label1.Text = UserName & " " & UserUnit
        'Call LoadGridFunc()

    End Sub

    Private Sub BtnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSearch.Click
        Syear = nudYear.Value
        Sfile = IIf(rdbFile1.Checked = True, "1", "2")
        If rdbFile3.Checked = True Then Sfile = "3"
        If Val(TxtEndNo.Text) = 0 Then TxtEndNo.Text = TxtStartNo.Text

        Sno = CInt(Trim(TxtStartNo.Text))
        Eno = CInt(Trim(TxtEndNo.Text))
        If Eno < Sno Then Eno = Sno
        If rdbKind1.Checked = True Then Skind = "1"
        If rdbkind2.Checked = True Then Skind = "2"
        Ekind = Skind
        If rdbkind3.Checked = True Then
            Skind = "3"
            Ekind = "4"
        End If

        If rdbkind4.Checked = True Then
            Skind = "1"
            Ekind = "4"
        End If
        TabControl1.Enabled = True
        RecMove1.Enabled = True
        Call LoadGridFunc()
        If DataGrid1.CurrentRowIndex > 0 Then Call PutGridToTxt()

    End Sub

    Sub LoadGridFunc()
        Dim sqlstr, qstr, sortstr As String
        sqlstr = "SELECT a.*, b.date_1, b.date_2, b.bank FROM  ACF020 a left outer join acf010 b on a.accyear=b.accyear and a.kind=b.kind and a.no_1_no=b.no_1_no " & _
                 "where a.seq=b.seq and b.item='1' and a.accyear=" & Syear & " and a.kind>='" & Skind & "' and a.kind<='" & Ekind & "' and "
        Select Case Sfile
            Case "1"   '未處理
                sqlstr = sqlstr & "a.no_1_no>=" & Sno & " and a.no_1_no<=" & Eno & " and b.date_2 is null"
                sortstr = " order by a.accyear, a.kind, a.no_1_no, a.seq, a.item"    '排序
            Case "2"   '已處理
                sqlstr = sqlstr & "a.no_2_no>=" & Sno & " and a.no_2_no<=" & Eno & " and b.date_2 is not null"
                sortstr = " order by a.accyear, a.kind, a.no_2_no, a.seq, a.item"
            Case "3"   '全部
                sqlstr = sqlstr & "a.no_1_no>=" & Sno & " and a.no_1_no<=" & Eno
                sortstr = " order by a.accyear, a.kind, a.no_1_no, a.seq, a.item"
        End Select
        If txtQremark.Text <> "" Then qstr = " and a.remark like '%" & Trim(txtQremark.Text) & "%'"
        If txtQaccno.Text <> "" Then qstr = qstr + " and a.accno like '%" & Trim(txtQaccno.Text) & "%'"
        If ValComa(txtQamt.Text) <> 0 Then qstr = qstr + " and a.amt =" & ValComa(txtQamt.Text)
        If Sfile = "2" Then   '已處理
            If rdbSortNo.Checked Then sortstr = " order by a.accyear, a.kind, a.no_2_no, a.seq, a.item"
            If rdbSortDate.Checked Then sortstr = " order by b.date_2, a.accyear, a.kind, a.no_2_no, a.seq, a.item"
            If rdbSortAccno.Checked Then sortstr = " order by a.accno, b.date_2, a.accyear, a.kind, a.no_2_no, a.seq, a.item"
        Else
            If rdbSortNo.Checked Then sortstr = " order by a.accyear, a.kind, a.no_1_no, a.seq, a.item"
            If rdbSortDate.Checked Then sortstr = " order by b.date_1, a.accyear, a.kind, a.no_1_no, a.seq, a.item"
            If rdbSortAccno.Checked Then sortstr = " order by a.accno, b.date_1, a.accyear, a.kind, a.no_1_no, a.seq, a.item"
        End If
        sqlstr = sqlstr + qstr + sortstr
        mydataset = openmember("", "ACF020", sqlstr)
        RecMove1.FilesPara = "mastconn=" & mastconn & "|sqlstr=" & sqlstr
        DataGrid1.DataSource = mydataset
        DataGrid1.DataMember = "ACF020"
        bm = Me.BindingContext(mydataset, "ACF020")
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
        txtDate1.Text = bm.Current("date_1")
        txtDate2.Text = IIf(IsDBNull(bm.Current("date_2")), "", bm.Current("date_2"))
        txtDC.Text = bm.Current("dc")
        vxtaccno.Text = bm.Current("accno")
        txtRemark.Text = bm.Current("remark")
        txtamt.Text = FormatNumber(bm.Current("amt"), 2)
        txtCotn_code.Text = bm.Current("cotn_code")
        txtMat_qty.Text = IIf(IsDBNull(bm.Current("mat_qty")), "", bm.Current("mat_qty"))
        txtMat_pric.Text = IIf(IsDBNull(bm.Current("mat_pric")), "", bm.Current("mat_pric"))
        If Not IsDBNull(bm.Current("other_accno")) Then vxtOtheraccno.Text = bm.Current("other_accno")
        lblAccname.Text = " "
        lblOtheraccname.Text = " "
    End Sub

    Sub transjob(ByVal JobName As String, ByVal JobPara As String) Handles RecMove1.TransJob
        Dim LastPos As Integer = bm.Position
        Select Case JobName
            Case "刪除記錄"
                Dim keyvalue, sqlstr, retstr As String
                If TabControl1.SelectedIndex = 0 Then
                    keyvalue = bm.Current("autono")
                Else
                    keyvalue = lblkey.Text
                End If

                sqlstr = "delete from ACF020 where autono=" & keyvalue
                retstr = runsql(mastconn, sqlstr)
                If retstr = "sqlok" Then
                    mydataset.Tables("ACF020").Rows.RemoveAt(JobPara)
                    mydataset.Tables("ACF020").Clear()
                    Call LoadGridFunc()
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
                RecMove1.GenUpdsql("dc", Trim(txtDC.Text), "T")
                RecMove1.GenUpdsql("accno", GetAccno(vxtaccno.Text), "T")
                RecMove1.GenUpdsql("remark", Trim(txtRemark.Text), "U")
                RecMove1.GenUpdsql("amt", txtamt.Text, "N")
                RecMove1.GenUpdsql("cotn_code", Trim(txtCotn_code.Text), "T")
                RecMove1.GenUpdsql("mat_qty", Trim(txtMat_qty.Text), "N")
                RecMove1.GenUpdsql("mat_pric", Trim(txtMat_pric.Text), "N")
                RecMove1.GenUpdsql("other_accno", GetAccno(vxtOtheraccno.Text), "T")
                sqlstr = "update ACF020 set " & RecMove1.genupdfunc & " where autono=" & keyvalue
                retstr = runsql(mastconn, sqlstr)
                If retstr = "sqlok" Then
                    mydataset.Tables("ACF020").Rows.RemoveAt(JobPara)
                    mydataset.Tables("ACF020").Clear()
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
                RecMove1.GenInsSql("dc", Trim(txtDC.Text), "T")
                RecMove1.GenInsSql("accno", GetAccno(vxtaccno.Text), "T")
                RecMove1.GenInsSql("remark", Trim(txtRemark.Text), "U")
                RecMove1.GenInsSql("amt", txtamt.Text, "N")
                RecMove1.GenInsSql("cotn_code", Trim(txtCotn_code.Text), "T")
                RecMove1.GenInsSql("mat_qty", Trim(txtMat_qty.Text), "N")
                RecMove1.GenInsSql("mat_pric", Trim(txtMat_pric.Text), "N")
                RecMove1.GenInsSql("other_accno", GetAccno(vxtOtheraccno.Text), "T")

                sqlstr = "insert into ACF020 " & RecMove1.GenInsFunc
                retstr = runsql(mastconn, sqlstr)

                If retstr = "sqlok" Then
                    mydataset.Tables("ACF020").Rows.RemoveAt(JobPara)
                    mydataset.Tables("ACF020").Clear()
                    Call LoadGridFunc()
                    bm.Position = LastPos
                Else
                    MsgBox("新增失敗" & sqlstr)
                End If
            Case "記錄移動"
                Dim keyvalue, sqlstr, retstr As String
                lblkey.Text = bm.Current("autono")
                lblAccname.Text = ""
                lblOtheraccname.Text = ""
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
        lblkey.Text = bm.Current("autono")  'keep the old keyvalue
        Call PutGridToTxt()
        Dirty = False
    End Sub

    Private Sub DataGrid1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.DoubleClick
        TabControl1.SelectedIndex = 1
    End Sub


    Private Sub btnAccno_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAccno.Click
        Dim sqlstr As String
        sqlstr = "SELECT ACCNAME FROM ACCNAME WHERE ACCNO = '" & GetAccno(vxtaccno.Text) & "'"
        mydataset2 = openmember("", "accname", sqlstr)
        If mydataset2.Tables("accname").Rows.Count = 0 Then
            lblAccname.Text = "無此代號"
        Else
            lblAccname.Text = mydataset2.Tables("accname").Rows(0).Item(0)
        End If
    End Sub
    Private Sub btnOtheraccno_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOtheraccno.Click
        Dim sqlstr As String
        sqlstr = "SELECT ACCNAME FROM ACCNAME WHERE ACCNO = '" & GetAccno(vxtOtheraccno.Text) & "'"
        mydataset2 = openmember("", "accname", sqlstr)
        If mydataset2.Tables("accname").Rows.Count = 0 Then
            lblOtheraccname.Text = "無此代號"
        Else
            lblOtheraccname.Text = mydataset2.Tables("accname").Rows(0).Item(0)
        End If
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub txtYear_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
             txtYear.LostFocus, txtKind.LostFocus, txtNo1.LostFocus, txtNo2.LostFocus, txtSeq.LostFocus, txtItem.LostFocus, _
             txtDC.LostFocus, TxtStartNo.LostFocus, txtMat_qty.LostFocus, txtMat_qty.LostFocus, TxtEndNo.LostFocus, TxtStartNo.LostFocus
        If (Not IsNumeric(sender.Text) Or ValComa(sender.text) < 0) And Trim(sender.text) <> "" Then
            MsgBox("請輸入正數字")
            sender.Focus()
        End If
    End Sub

    Private Sub txtamt_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtQamt.LostFocus, txtamt.LostFocus
        If (Not IsNumeric(sender.Text) Or ValComa(sender.text) < 0) And Trim(sender.text) <> "" Then
            MsgBox("請輸入正數字")
            sender.Focus()
        Else
            sender.text = FormatNumber(ValComa(sender.text), 2)
        End If
    End Sub
End Class
