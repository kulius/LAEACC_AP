Public Class BGF050
    Dim confid, confunit As String, LoadAfter, Dirty As Boolean
    Dim sqlstr As String
    Dim bm As BindingManagerBase, mydataset, psDataSet, AccnoDataSet As DataSet
    Dim UserId, UserName, UserUnit As String

    Private Sub BGF050_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TabControl1.Enabled = False
        RecMove1.Enabled = False
        nudYear.Value = GetYear(TransPara.TransP("Userdate"))
        UserUnit = TransPara.TransP("Userunit")
        UserId = TransPara.TransP("Userid")
        sqlstr = "SELECT accno,accno+accname as accnamet from accname where left(accno,5)='21302' and len(accno)<=16 "
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
        sqlstr = "SELECT  a.*, b.accname " & _
                 " FROM  BGF050 a" & _
                 " LEFT OUTER JOIN accname b ON a.accno = b.accno" & _
                 " WHERE a.accyear=" & nudYear.Value & " order by a.date2 desc"
        mydataset = openmember("", "BGF050", sqlstr)
        RecMove1.FilesPara = "mastconn=" & mastconn & "|sqlstr=" & sqlstr
        DataGrid1.DataSource = mydataset
        DataGrid1.DataMember = "BGF050"
        bm = Me.BindingContext(mydataset, "BGF050")
        RecMove1.Setds = bm
        TabControl1.SelectedIndex = 0

    End Sub

    Sub PutGridToTxt()
        Dim intI, SumUp As Integer
        Dim strI, strColumn1, strColumn2 As String
        If bm.Position = -1 Then Exit Sub
        If IsDBNull(bm.Current("accno")) Then Exit Sub
        If Not IsDBNull(bm.Current("accno")) Then cboAccno.SelectedValue = bm.Current("accno")
        dtpDate2.Value = bm.Current("date2")
        txtRemark.Text = nz(bm.Current("remark"), "")
        txtAmt.Text = Format(nz(bm.Current("amt"), 0), "###,###,###,##0")
        txtNo1.Text = nz(bm.Current("no_1_no"), 0)
        If Not IsDBNull(bm.Current("autono")) Then lblkey.Text = Trim(bm.Current("autono"))
    End Sub

    Sub transjob(ByVal JobName As String, ByVal JobPara As String) Handles RecMove1.TransJob
        Dim LastPos As Integer = bm.Position
        Select Case JobName
            Case "刪除記錄"
                Dim keyvalue, sqlstr, retstr As String
                If TabControl1.SelectedIndex = 0 Then
                    keyvalue = bm.Current("autono")
                Else
                    keyvalue = Trim(lblkey.Text)
                End If

                sqlstr = "delete from BGF050 where autono=" & keyvalue
                retstr = runsql(mastconn, sqlstr)
                If retstr = "sqlok" Then
                    mydataset.Tables("BGF050").Rows.RemoveAt(JobPara)
                    'mydataset.Tables("BGF050").Clear()
                    'Call LoadGridFunc()
                    bm.Position = LastPos
                End If

            Case "回寫記錄cmd"
                Dim keyvalue, sqlstr, retstr, updstr, TDC As String
                If TabControl1.SelectedIndex = 0 Then
                    Call PutGridToTxt()
                End If
                keyvalue = Trim(lblkey.Text)
                RecMove1.GenUpdsql("accyear", GetYear(dtpDate2.Value), "N")
                RecMove1.GenUpdsql("date2", dtpDate2.Value.ToShortDateString, "D")
                RecMove1.GenUpdsql("accno", cboAccno.SelectedValue, "T")
                RecMove1.GenUpdsql("remark", txtRemark.Text, "U")
                RecMove1.GenUpdsql("Amt", txtAmt.Text, "N")
                RecMove1.GenUpdsql("no_1_no", txtNo1.Text, "N")
                sqlstr = "update BGF050 set " & RecMove1.genupdfunc & " where autono=" & keyvalue
                retstr = runsql(mastconn, sqlstr)
                If retstr = "sqlok" Then
                    mydataset.Tables("BGF050").Clear()
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
                If ValComa(txtAmt.Text) = 0 Then
                    Exit Sub
                End If
                RecMove1.GenInsSql("accyear", GetYear(dtpDate2.Value), "N")
                RecMove1.GenInsSql("date2", dtpDate2.Value.ToShortDateString, "D")
                RecMove1.GenInsSql("accno", cboAccno.SelectedValue, "T")
                RecMove1.GenInsSql("remark", txtRemark.Text, "U")
                RecMove1.GenInsSql("Amt", txtAmt.Text, "N")
                RecMove1.GenInsSql("no_1_no", txtNo1.Text, "N")
                sqlstr = "insert into BGF050 " & RecMove1.GenInsFunc
                retstr = runsql(mastconn, sqlstr)

                If retstr = "sqlok" Then
                    mydataset.Tables("BGF050").Clear()
                    Call LoadGridFunc()
                    If LastPos > 0 Then bm.Position = LastPos
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

    Private Sub txtAmt_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAmt.LostFocus
        If Not IsNumeric(sender.Text) And Trim(sender.text) <> "" Then
            MsgBox("請輸入數字")
            sender.Focus()
        Else
            sender.text = FormatNumber(ValComa(sender.text), 0)
        End If
    End Sub

    Private Sub txtno_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNo1.LostFocus
        If (Not IsNumeric(sender.Text) Or ValComa(sender.text) < 0) And Trim(sender.text) <> "" Then
            MsgBox("請輸入數字")
            sender.Focus()
        End If
    End Sub
End Class
