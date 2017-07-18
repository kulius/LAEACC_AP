Public Class CHF010
    Dim confid, confunit As String, LoadAfter, Dirty As Boolean
    Dim sqlstr As String
    Dim bm As BindingManagerBase, mydataset As DataSet, mydataset2 As DataSet
    'Dim UserId, UserName, UserUnit As String
    Dim Skind As Char

    Private Sub CHF010_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Dim myds As DataSet
        LoadAfter = True
        dtpStartDate.Value = Today
        dtpEndDate.Value = Today
        TabControl1.Enabled = False
        RecMove1.Enabled = False
    End Sub

    Private Sub BtnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSearch.Click
        '收 or 支
        If rdbKind1.Checked = True Then Skind = "1"
        If rdbkind2.Checked = True Then Skind = "2"

        TabControl1.Enabled = True
        RecMove1.Enabled = True
        Call LoadGridFunc()
        If DataGrid1.CurrentRowIndex > 0 Then Call PutGridToTxt()

    End Sub

    Sub LoadGridFunc()
        Dim sqlstr, qstr As String
        sqlstr = "SELECT * FROM chf010 where kind='" & Skind & "' and bank='" & txtStartBank.Text & "' and "
        '已領 or 未領
        If rdbCheck1.Checked Then  '已領
            sqlstr = sqlstr & " date_2 is not null and date_2 >='" & FullDate(dtpStartDate.Value) & "' and date_2 <='" & FullDate(dtpEndDate.Value) & "'"
        Else '未領()
            sqlstr = sqlstr & " date_2 is null and date_1 >='" & FullDate(dtpStartDate.Value) & "' and date_1 <='" & FullDate(dtpEndDate.Value) & "'"
        End If
        sqlstr = sqlstr & " and chkno>='" & txtStartNo.Text & "' and chkno<='" & txtEndNo.Text & "'"
        If txtQremark.Text <> "" Then qstr = " and remark like '%" & Trim(txtQremark.Text) & "%'"
        If txtQchkname.Text <> "" Then qstr = qstr + " and chkname like '%" & Trim(txtQchkname.Text) & "%'"
        If txtQamt.Text <> "" Then qstr = qstr + " and amt =" & ValComa(txtQamt.Text)
        sqlstr = sqlstr & qstr
        '排序
        If rdbSortBank.Checked Then  '銀行順
            If rdbCheck1.Checked Then
                sqlstr = sqlstr & " order by bank, date_2"  '已領
            Else
                sqlstr = sqlstr & " order by bank, chkno"  '未領
            End If
        Else                         '日期順
            If rdbCheck1.Checked Then
                sqlstr = sqlstr & " order by date_2, bank"   '已領
            Else
                sqlstr = sqlstr & " order by date_1, bank"   '未領
            End If
        End If

        mydataset = openmember("", "chf010", sqlstr)
        RecMove1.FilesPara = "mastconn=" & mastconn & "|sqlstr=" & sqlstr
        DataGrid1.DataSource = mydataset
        DataGrid1.DataMember = "chf010"
        bm = Me.BindingContext(mydataset, "chf010")
        RecMove1.Setds = bm
        TabControl1.SelectedIndex = 0
    End Sub

    Sub PutGridToTxt()
        If IsDBNull(bm.Current("accyear")) Then Exit Sub
        txtYear.Text = nz(bm.Current("accyear"), 0)
        txtKind.Text = nz(bm.Current("kind"), "")
        txtBank.Text = nz(bm.Current("bank"), "")
        txtChkno.Text = nz(bm.Current("chkno"), "")
        txtDate1.Text = nz(bm.Current("date_1"), "")
        txtDate2.Text = nz(bm.Current("date_2"), "")
        txtChkname.Text = nz(bm.Current("chkname"), " ")
        txtamt.Text = FormatNumber(nz(bm.Current("amt"), 0), 2)
        txtRemark.Text = nz(bm.Current("remark"), "")
        txtStart_no.Text = nz(bm.Current("start_no"), 0)
        txtEnd_no.Text = nz(bm.Current("end_no"), 0)
        txtNO1.Text = nz(bm.Current("no_1_no"), 0)
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

                sqlstr = "delete from chf010 where autono=" & keyvalue
                retstr = runsql(mastconn, sqlstr)
                If retstr = "sqlok" Then
                    mydataset.Tables("chf010").Rows.RemoveAt(JobPara)
                    TabControl1.SelectedIndex = 0
                    bm.Position = LastPos
                End If

            Case "回寫記錄cmd"
                Dim keyvalue, sqlstr, retstr, updstr As String
                'If TabControl1.SelectedIndex = 0 Then
                'Call PutGridToTxt()
                'End If
                keyvalue = lblkey.Text
                RecMove1.GenUpdsql("accyear", Trim(txtYear.Text), "N")
                RecMove1.GenUpdsql("kind", Trim(txtKind.Text), "T")
                RecMove1.GenUpdsql("bank", Trim(txtBank.Text), "T")
                RecMove1.GenUpdsql("chkno", txtChkno.Text, "T")
                RecMove1.GenUpdsql("date_1", Trim(txtDate1.Text), "D")
                RecMove1.GenUpdsql("date_2", Trim(txtDate2.Text), "D")
                RecMove1.GenUpdsql("chkname", txtChkname.Text, "U")
                RecMove1.GenUpdsql("remark", Trim(txtRemark.Text), "U")
                RecMove1.GenUpdsql("amt", Trim(txtamt.Text), "N")
                RecMove1.GenUpdsql("start_no", txtStart_no.Text, "N")
                RecMove1.GenUpdsql("End_no", txtEnd_no.Text, "N")
                RecMove1.GenUpdsql("no_1_no", txtNO1.Text, "T")
                sqlstr = "update chf010 set " & RecMove1.genupdfunc & " where autono=" & keyvalue
                retstr = runsql(mastconn, sqlstr)
                If retstr = "sqlok" Then
                    mydataset.Tables("chf010").Rows.RemoveAt(JobPara)
                    mydataset.Tables("chf010").Clear()
                    Call LoadGridFunc()
                    bm.Position = LastPos
                    MsgBox("更新成功")
                End If

            Case "新增記錄"
                Dim keyvalue, sqlstr, retstr, updstr As String
                'If TabControl1.SelectedIndex = 0 Then
                'Call PutGridToTxt()
                'End If
                RecMove1.GenInsSql("accyear", Trim(txtYear.Text), "N")
                RecMove1.GenInsSql("kind", Trim(txtKind.Text), "T")
                RecMove1.GenInsSql("bank", Trim(txtBank.Text), "T")
                RecMove1.GenInsSql("chkno", txtChkno.Text, "T")
                RecMove1.GenInsSql("date_1", Trim(txtDate1.Text), "D")
                RecMove1.GenInsSql("date_2", Trim(txtDate2.Text), "D")
                RecMove1.GenInsSql("chkname", txtChkname.Text, "U")
                RecMove1.GenInsSql("remark", Trim(txtRemark.Text), "U")
                RecMove1.GenInsSql("amt", Trim(txtamt.Text), "N")
                RecMove1.GenInsSql("start_no", txtStart_no.Text, "N")
                RecMove1.GenInsSql("End_no", txtEnd_no.Text, "N")
                RecMove1.GenInsSql("no_1_no", txtNO1.Text, "T")

                sqlstr = "insert into chf010 " & RecMove1.GenInsFunc
                retstr = runsql(mastconn, sqlstr)

                If retstr = "sqlok" Then
                    mydataset.Tables("chf010").Rows.RemoveAt(JobPara)
                    mydataset.Tables("chf010").Clear()
                    Call LoadGridFunc()
                    bm.Position = LastPos
                    'MsgBox("新增成功")
                Else
                    MsgBox("新增失敗")
                End If
            Case "記錄移動"
                Dim keyvalue, sqlstr, retstr As String
                lblkey.Text = bm.Current("autono")
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

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        mydataset = Nothing
        mydataset2 = Nothing
        Me.Close()
    End Sub

    Private Sub txtamt_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtamt.LostFocus, txtQamt.LostFocus
        If (Not IsNumeric(sender.text) Or ValComa(sender.text) < 0) And Trim(sender.text) <> "" Then
            MsgBox("請輸入正數字")
            sender.focus()
        Else
            sender.text = FormatNumber(ValComa(sender.text), 0)
        End If
    End Sub


    'Handles txtNO1.LostFocus, txtEnd_no.LostFocus, txtEndNo.LostFocus, txtKind.LostFocus
    '        If (Not IsNumeric(sender.text) Or ValComa(sender.text) < 0) And Trim(sender.text) <> "" Then
    '            MsgBox("請輸入正數字")
    '            sender.focus()
    '        End If
    '    End Sub
End Class
