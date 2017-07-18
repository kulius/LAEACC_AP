Public Class CHF030
    Dim confid, confunit As String, LoadAfter, Dirty As Boolean
    Dim sqlstr As String
    Dim bm As BindingManagerBase, mydataset As DataSet, mydataset2 As DataSet
    'Dim UserId, UserName, UserUnit As String
    Dim Skind As Char

    Private Sub CHF030_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Dim myds As DataSet
        LoadAfter = True
        dtpStartDate.Value = Today
        dtpEndDate.Value = Today
        TabControl1.Enabled = False
        RecMove1.Enabled = False
    End Sub

    Private Sub BtnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSearch.Click
        TabControl1.Enabled = True
        RecMove1.Enabled = True
        Call LoadGridFunc()
        If DataGrid1.CurrentRowIndex > 0 Then Call PutGridToTxt()

    End Sub

    Sub LoadGridFunc()
        Dim sqlstr, qstr As String
        sqlstr = "SELECT * FROM CHF030 where bank>='" & txtStartBank.Text & "' and bank<='" & txtEndBank.Text & "' and " & _
        " date_2 >='" & FullDate(dtpStartDate.Value) & "' and date_2 <='" & FullDate(dtpEndDate.Value) & "'"

        '排序
        If rdbSortBank.Checked Then  '銀行順
            sqlstr = sqlstr & " order by bank, date_2"
        Else
            sqlstr = sqlstr & " order by date_2, bank"
        End If

        mydataset = openmember("", "CHF030", sqlstr)
        RecMove1.FilesPara = "mastconn=" & mastconn & "|sqlstr=" & sqlstr
        DataGrid1.DataSource = mydataset
        DataGrid1.DataMember = "CHF030"
        bm = Me.BindingContext(mydataset, "CHF030")
        RecMove1.Setds = bm
        TabControl1.SelectedIndex = 0
    End Sub

    Sub PutGridToTxt()
        If IsDBNull(bm.Current("bank")) Then Exit Sub
        lblkey.Text = bm.Current("autono")
        txtBank.Text = bm.Current("bank")
        dtpDate_2.Value = IIf(IsDBNull(bm.Current("date_2")), " ", bm.Current("date_2"))
        txtDay_income.Text = FormatNumber(bm.Current("day_income"), 2)
        txtDay_pay.Text = FormatNumber(bm.Current("day_pay"), 2)
        txtBalance.Text = FormatNumber(bm.Current("balance"), 2)

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

                sqlstr = "delete from CHF030 where autono=" & keyvalue
                retstr = runsql(mastconn, sqlstr)
                If retstr = "sqlok" Then
                    mydataset.Tables("CHF030").Rows.RemoveAt(JobPara)
                    TabControl1.SelectedIndex = 0
                    bm.Position = LastPos
                End If

            Case "回寫記錄cmd"
                Dim keyvalue, sqlstr, retstr, updstr As String
                'If TabControl1.SelectedIndex = 0 Then
                'Call PutGridToTxt()
                'End If
                keyvalue = lblkey.Text
                RecMove1.GenUpdsql("bank", Trim(txtBank.Text), "T")
                RecMove1.GenUpdsql("date_2", Trim(dtpDate_2.Value.ToShortDateString), "D")
                RecMove1.GenUpdsql("day_income", txtDay_income.Text, "N")
                RecMove1.GenUpdsql("day_pay", txtDay_pay.Text, "N")
                RecMove1.GenUpdsql("balance", txtBalance.Text, "N")
                sqlstr = "update CHF030 set " & RecMove1.genupdfunc & " where autono=" & keyvalue
                retstr = runsql(mastconn, sqlstr)
                If retstr = "sqlok" Then
                    mydataset.Tables("CHF030").Rows.RemoveAt(JobPara)
                    mydataset.Tables("CHF030").Clear()
                    Call LoadGridFunc()
                    bm.Position = LastPos
                    MsgBox("更新成功")
                End If

            Case "新增記錄"
                Dim keyvalue, sqlstr, retstr, updstr As String
                'If TabControl1.SelectedIndex = 0 Then
                'Call PutGridToTxt()
                'End If
                RecMove1.GenInsSql("bank", Trim(txtBank.Text), "T")
                RecMove1.GenInsSql("date_2", Trim(dtpDate_2.Value.ToShortDateString), "D")
                RecMove1.GenInsSql("day_income", txtDay_income.Text, "N")
                RecMove1.GenInsSql("day_pay", txtDay_pay.Text, "N")
                RecMove1.GenInsSql("balance", txtBalance.Text, "N")

                sqlstr = "insert into CHF030 " & RecMove1.GenInsFunc
                retstr = runsql(mastconn, sqlstr)

                If retstr = "sqlok" Then
                    'mydataset.Tables("CHF030").Rows.RemoveAt(JobPara)
                    mydataset.Tables("CHF030").Clear()
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

    Private Sub DataGrid1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataGrid1.Click
        lblkey.Text = bm.Current("autono")  'keep the old keyvalue
        Call PutGridToTxt()
        Dirty = False
    End Sub

    Private Sub DataGrid1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataGrid1.DoubleClick
        Call PutGridToTxt()
        TabControl1.SelectedIndex = 1
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub txtBalance_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBalance.LostFocus, _
                                                                          txtDay_income.LostFocus, txtDay_pay.LostFocus
        If (Not IsNumeric(sender.text) Or ValComa(sender.text) < 0) And Trim(sender.text) <> "" Then
            MsgBox("請輸入正數字")
            sender.focus()
        Else
            sender.text = FormatNumber(ValComa(sender.text), 2)
        End If
    End Sub
End Class
