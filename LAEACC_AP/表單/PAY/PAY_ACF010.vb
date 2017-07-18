Public Class PAY_ACF010
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
        TabControl1.Enabled = False
        nudYear.Value = GetYear(Now)
    End Sub

    Private Sub BtnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSearch.Click
        Syear = nudYear.Value
        Sfile = IIf(rdbFile1.Checked = True, "1", "2")
        If rdbfile3.Checked = True Then Sfile = "3"
        Sno = CInt(Trim(TxtStartNo.Text))
        Eno = CInt(Trim(TxtEndNo.Text))
        If rdbKind1.Checked = True Then Skind = "1"
        If rdbkind2.Checked = True Then Skind = "2"
        If rdbkind3.Checked = True Then Skind = "3"
        Ekind = Skind
        If Skind = "3" Then Ekind = "4"
        TabControl1.Enabled = True
        Call LoadGridFunc()
        If DataGrid1.CurrentRowIndex > 0 Then Call PutGridToTxt()
    End Sub

    Sub LoadGridFunc()
        Dim sqlstr, qstr, sortstr As String
        sqlstr = "SELECT * FROM  acf010 where accyear=" & Syear & " and kind>='" & Skind & "' and kind<='" & Ekind & "' and "
        Select Case Sfile
            Case "1"   '未處理
                sqlstr = sqlstr & "no_1_no>=" & Trim(TxtStartNo.Text) & " and no_1_no<=" & Trim(TxtEndNo.Text) & " and date_2 is null"
                sortstr = " order by accyear, kind, no_1_no, seq, item"    '排序
            Case "2"   '已處理
                sqlstr = sqlstr & "no_2_no>=" & Trim(TxtStartNo.Text) & " and no_2_no<=" & Trim(TxtEndNo.Text) & " and date_2 is not null"
                sortstr = " order by accyear, kind, no_2_no, seq, item"
            Case "3"  '全部
                sqlstr = sqlstr & "no_1_no>=" & Trim(TxtStartNo.Text) & " and no_1_no<=" & Trim(TxtEndNo.Text)
                sortstr = " order by accyear, kind, no_1_no, seq, item"
        End Select
        If Trim(txtQremark.Text) <> "" Then qstr = " and remark like '%" & Trim(txtQremark.Text) & "%'"
        If Trim(txtQaccno.Text) <> "" Then qstr = qstr + " and accno like '%" & Trim(txtQaccno.Text) & "%'"
        If ValComa(txtQamt.Text) <> 0 Then qstr = qstr + " and amt =" & ValComa(txtQamt.Text)
        If Trim(txtQBank.Text) <> "" Then qstr = qstr + " and bank ='" & txtQBank.Text & "'"
        sqlstr = sqlstr + qstr + sortstr
        mydataset = openmember("", "acf010", sqlstr)
        DataGrid1.DataSource = mydataset
        DataGrid1.DataMember = "acf010"
        bm = Me.BindingContext(mydataset, "acf010")
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
End Class
