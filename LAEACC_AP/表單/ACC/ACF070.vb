Public Class ACF070
    Dim confid, confunit As String, LoadAfter, Dirty As Boolean
    Dim sqlstr As String
    Dim bm As BindingManagerBase, mydataset As DataSet
    Dim UserId, UserName, UserUnit As String

    Private Sub acf070_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TabControl1.Enabled = False
        RecMove1.Enabled = False
        dtpStartDate.Value = Today
        dtpEndDate.Value = Today
        vxtStartNo.text = "1"    '起值
        vxtEndNo.text = "9"     '迄值
    End Sub

    Private Sub BtnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSearch.Click
        TabControl1.Enabled = True
        RecMove1.Enabled = True
        Call LoadGridFunc()
        If DataGrid1.CurrentRowIndex > 0 Then Call PutGridToTxt()
    End Sub

    Sub LoadGridFunc()
        Dim sqlstr, qstr As String

        sqlstr = "SELECT a.*,  b.accname  FROM  acf070 a LEFT OUTER JOIN accname b ON a.accno = b.accno" & _
                 " WHERE a.date_2 >='" & FullDate(dtpStartDate.Value) & "' and a.date_2 <='" & FullDate(dtpEndDate.Value) & _
                 "' and a.accno>='" & GetAccno(vxtStartNo.Text) & "' and a.accno<='" & GetAccno(vxtEndNo.Text) & "'"

        If rdbdate.Checked Then
            sqlstr &= " ORDER BY a.date_2, a.accno"
        Else
            sqlstr &= " ORDER BY a.accno, a.date_2"
        End If
        mydataset = openmember("", "acf070", sqlstr)

        RecMove1.FilesPara = "mastconn=" & mastconn & "|sqlstr=" & sqlstr
        DataGrid1.DataSource = mydataset
        DataGrid1.DataMember = "acf070"
        bm = Me.BindingContext(mydataset, "acf070")
        RecMove1.Setds = bm
        TabControl1.SelectedIndex = 0
    End Sub

    Sub PutGridToTxt()
        Dim intI As Integer
        Dim strI, subj, cont1 As String
        dtpDate.Value = bm.Current("date_2")
        vxtAccno.Text = bm.Current("accno")
        lblAccname.Text = IIf(IsDBNull(bm.Current("accname")), " ", bm.Current("accname"))
        txtDeAmt1.Text = FormatNumber(bm.Current("deamt1"), 2)
        txtDeAmt2.Text = FormatNumber(bm.Current("deamt2"), 2)
        txtDeAmt3.Text = FormatNumber(bm.Current("deamt3"), 2)
        txtCrAmt1.Text = FormatNumber(bm.Current("cramt1"), 2)
        txtCrAmt2.Text = FormatNumber(bm.Current("cramt2"), 2)
        txtCrAmt3.Text = FormatNumber(bm.Current("cramt3"), 2)
        lblSumDebit.Text = FormatNumber(bm.Current("deamt1") + bm.Current("deamt2") + bm.Current("deamt3"), 2)
        lblSumCredit.Text = FormatNumber(bm.Current("cramt1") + bm.Current("cramt2") + bm.Current("cramt3"), 2)
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

                sqlstr = "delete from acf070 where autono=" & keyvalue
                retstr = runsql(mastconn, sqlstr)
                If retstr = "sqlok" Then
                    mydataset.Tables("acf070").Rows.RemoveAt(JobPara)
                    mydataset.Tables("acf070").Clear()
                    Call LoadGridFunc()
                    bm.Position = LastPos
                End If

            Case "回寫記錄cmd"
                Dim keyvalue, sqlstr, retstr, updstr As String
                If TabControl1.SelectedIndex = 0 Then
                    Call PutGridToTxt()
                End If
                keyvalue = lblkey.Text

                RecMove1.GenUpdsql("date_2", dtpDate.Value.ToShortDateString, "D")
                RecMove1.GenUpdsql("accno", GetAccno(vxtAccno.Text), "T")
                RecMove1.GenUpdsql("deamt1", txtDeAmt1.Text, "N")
                RecMove1.GenUpdsql("Deamt2", txtDeAmt2.Text, "N")
                RecMove1.GenUpdsql("Deamt3", txtDeAmt3.Text, "N")
                RecMove1.GenUpdsql("cramt1", txtCrAmt1.Text, "N")
                RecMove1.GenUpdsql("cramt2", txtCrAmt2.Text, "N")
                RecMove1.GenUpdsql("cramt3", txtCrAmt3.Text, "N")
                sqlstr = "update acf070 set " & RecMove1.genupdfunc & " where autono=" & keyvalue
                retstr = runsql(mastconn, sqlstr)
                If retstr = "sqlok" Then
                    mydataset.Tables("acf070").Rows.RemoveAt(JobPara)
                    mydataset.Tables("acf070").Clear()
                    Call LoadGridFunc()
                    MsgBox("更新成功")
                    bm.Position = LastPos
                End If

            Case "新增記錄"
                Dim keyvalue, sqlstr, retstr, updstr As String
                If TabControl1.SelectedIndex = 0 Then
                    Call PutGridToTxt()
                End If
                RecMove1.GenInsSql("date_2", dtpDate.Value.ToShortDateString, "D")
                RecMove1.GenInsSql("accno", GetAccno(vxtAccno.Text), "T")
                RecMove1.GenInsSql("deamt1", txtDeAmt1.Text, "N")
                RecMove1.GenInsSql("Deamt2", txtDeAmt2.Text, "N")
                RecMove1.GenInsSql("Deamt3", txtDeAmt3.Text, "N")
                RecMove1.GenInsSql("cramt1", txtCrAmt1.Text, "N")
                RecMove1.GenInsSql("cramt2", txtCrAmt2.Text, "N")
                RecMove1.GenInsSql("cramt3", txtCrAmt3.Text, "N")

                sqlstr = "insert into acf070 " & RecMove1.GenInsFunc
                retstr = runsql(mastconn, sqlstr)

                If retstr = "sqlok" Then
                    mydataset.Tables("acf070").Rows.RemoveAt(JobPara)
                    mydataset.Tables("acf070").Clear()
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

    Private Sub txtDeAmt1_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDeAmt1.LostFocus, txtDeAmt2.LostFocus, txtDeAmt3.LostFocus, _
                                                                       txtCrAmt1.LostFocus, txtCrAmt2.LostFocus, txtCrAmt3.LostFocus
        If (Not IsNumeric(sender.text) Or ValComa(sender.text) < 0) And Trim(sender.text) <> "" Then
            MsgBox("請輸入正數字")
            sender.focus()
        Else
            Dim strI As String = Mid(sender.name, 9, 1)
            If sender.text <> "" Then
                sender.text = FormatNumber(ValComa(sender.text), 2)
            End If
            lblSumDebit.Text = FormatNumber(ValComa(txtDeAmt1.Text) + ValComa(txtDeAmt2.Text) + ValComa(txtDeAmt3.Text), 2)
            lblSumCredit.Text = FormatNumber(ValComa(txtCrAmt1.Text) + ValComa(txtCrAmt2.Text) + ValComa(txtCrAmt3.Text), 2)
        End If
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class
