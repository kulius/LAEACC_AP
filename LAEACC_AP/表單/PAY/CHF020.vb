Public Class CHF020
    Dim confid, confunit As String, LoadAfter, Dirty As Boolean
    Dim sqlstr As String
    Dim bm As BindingManagerBase, mydataset As DataSet
    Dim UserId, UserName, UserUnit As String

    Private Sub CHF020_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call LoadGridFunc()
        If DataGrid1.CurrentRowIndex > 0 Then Call PutGridToTxt()
    End Sub

    Private Sub BtnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSearch.Click
        Call LoadGridFunc()
        If DataGrid1.CurrentRowIndex > 0 Then Call PutGridToTxt()
    End Sub

    Sub LoadGridFunc()
        Dim sqlstr, qstr As String
        sqlstr = "SELECT a.*,  b.accname  FROM  CHF020 a LEFT OUTER JOIN accname b ON a.accno = b.accno order by a.bank"

        mydataset = openmember("", "CHF020", sqlstr)   ' openmember("", "CHF020", sqlstr)

        RecMove1.FilesPara = "mastconn=" & mastconn & "|sqlstr=" & sqlstr
        DataGrid1.DataSource = mydataset
        DataGrid1.DataMember = "CHF020"
        bm = Me.BindingContext(mydataset, "CHF020")
        RecMove1.Setds = bm
        TabControl1.SelectedIndex = 0
    End Sub

    Sub PutGridToTxt()
        Dim intI As Integer
        Dim strI, subj, cont1 As String
        If IsDBNull(bm.Current("bank")) Then Exit Sub
        lblkey.Text = bm.Current("bank")
        txtBank.Text = bm.Current("bank")
        txtDate_2.Text = nz(bm.Current("date_2"), "  ")
        vxtAccno.Text = bm.Current("accno")
        lblAccname.Text = nz(bm.Current("accname"), "")
        txtAccount.Text = nz(bm.Current("account"), "")
        txtBankname.Text = nz(bm.Current("bankname"), "")
        txtPrt_code.Text = nz(bm.Current("Prt_code"), " ")
        txtChkno.Text = nz(bm.Current("chkno"), " ")
        txtRemark.Text = nz(bm.Current("remark"), " ")
        txtBalance.Text = FormatNumber(bm.Current("balance"), 2)
        txtDay_income.Text = FormatNumber(bm.Current("day_income"), 2)
        txtDay_pay.Text = FormatNumber(bm.Current("day_pay"), 2)
        txtCredit.Text = FormatNumber(bm.Current("credit"), 2)
        txtUnpay.Text = FormatNumber(bm.Current("unpay"), 2)
        lblBalance.Text = FormatNumber(bm.Current("balance") + bm.Current("day_income") - bm.Current("day_pay"), 2)
        txtChkForm.Text = FormatNumber(nz(bm.Current("chkform"), 0), 0)
    End Sub

    Public Sub transjob(ByVal JobName As String, ByVal JobPara As String) Handles RecMove1.TransJob
        Dim LastPos As Integer = bm.Position
        Select Case JobName
            Case "刪除記錄"
                Dim keyvalue, sqlstr, retstr As String
                If TabControl1.SelectedIndex = 0 Then
                    keyvalue = bm.Current("bank")
                Else
                    keyvalue = Trim(lblkey.Text)
                End If

                sqlstr = "delete from CHF020 where bank='" & keyvalue & "'"
                retstr = runsql(mastconn, sqlstr)
                If retstr = "sqlok" Then
                    mydataset.Tables("CHF020").Rows.RemoveAt(JobPara)
                    mydataset.Tables("CHF020").Clear()
                    Call LoadGridFunc()
                    bm.Position = LastPos
                End If
                TabControl1.SelectedIndex = 0

            Case "回寫記錄cmd"
                Dim keyvalue, sqlstr, retstr, updstr As String
                If TabControl1.SelectedIndex = 0 Then
                    Call PutGridToTxt()
                End If
                keyvalue = lblkey.Text
                RecMove1.GenUpdsql("bank", txtBank.Text, "T")
                RecMove1.GenUpdsql("date_2", txtDate_2.Text, "D")
                RecMove1.GenUpdsql("accno", GetAccno(vxtAccno.Text), "T")
                RecMove1.GenUpdsql("account", txtAccount.Text, "T")
                RecMove1.GenUpdsql("bankname", txtBankname.Text, "U")
                RecMove1.GenUpdsql("prt_code", txtPrt_code.Text, "T")
                RecMove1.GenUpdsql("chkno", txtChkno.Text, "T")
                RecMove1.GenUpdsql("remark", txtRemark.Text, "U")
                RecMove1.GenUpdsql("balance", txtBalance.Text, "N")
                RecMove1.GenUpdsql("day_income", txtDay_income.Text, "N")
                RecMove1.GenUpdsql("day_pay", txtDay_pay.Text, "N")
                RecMove1.GenUpdsql("unpay", txtUnpay.Text, "N")
                RecMove1.GenUpdsql("credit", txtCredit.Text, "N")
                RecMove1.GenUpdsql("chkform", txtChkForm.Text, "N")
                sqlstr = "update CHF020 set " & RecMove1.genupdfunc & " where bank='" & keyvalue & "'"
                retstr = runsql(mastconn, sqlstr)
                If retstr = "sqlok" Then
                    mydataset.Tables("CHF020").Rows.RemoveAt(JobPara)
                    mydataset.Tables("CHF020").Clear()
                    Call LoadGridFunc()
                    MsgBox("更新成功")
                    bm.Position = LastPos
                End If
                TabControl1.SelectedIndex = 0


            Case "新增記錄"
                Dim keyvalue, sqlstr, retstr, updstr As String
                If TabControl1.SelectedIndex = 0 Then
                    Call PutGridToTxt()
                End If

                RecMove1.GenInsSql("bank", txtBank.Text, "T")
                RecMove1.GenInsSql("date_2", txtDate_2.Text, "D")
                RecMove1.GenInsSql("accno", GetAccno(vxtAccno.Text), "T")
                RecMove1.GenInsSql("account", txtAccount.Text, "T")
                RecMove1.GenInsSql("bankname", txtBankname.Text, "U")
                RecMove1.GenInsSql("prt_code", txtPrt_code.Text, "T")
                RecMove1.GenInsSql("chkno", txtChkno.Text, "T")
                RecMove1.GenInsSql("remark", txtRemark.Text, "U")
                RecMove1.GenInsSql("balance", txtBalance.Text, "N")
                RecMove1.GenInsSql("day_income", txtDay_income.Text, "N")
                RecMove1.GenInsSql("day_pay", txtDay_pay.Text, "N")
                RecMove1.GenInsSql("unpay", txtUnpay.Text, "N")
                RecMove1.GenInsSql("credit", txtCredit.Text, "N")
                RecMove1.GenInsSql("chkform", txtChkForm.Text, "N")

                sqlstr = "insert into CHF020 " & RecMove1.GenInsFunc
                retstr = runsql(mastconn, sqlstr)

                If retstr = "sqlok" Then
                    mydataset.Tables("CHF020").Rows.RemoveAt(JobPara)
                    mydataset.Tables("CHF020").Clear()
                    Call LoadGridFunc()
                    bm.Position = LastPos
                    'MsgBox("新增成功")
                Else
                    MsgBox("新增失敗")
                End If
                TabControl1.SelectedIndex = 0

            Case "記錄移動"
                Dim keyvalue, sqlstr, retstr As String
                lblkey.Text = bm.Current("bank")
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
        lblkey.Text = bm.Current("bank")  'keep the old keyvalue
        Call PutGridToTxt()
        Dirty = False
    End Sub

    Private Sub DataGrid1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataGrid1.DoubleClick
        lblkey.Text = bm.Current("bank")  'keep the old keyvalue
        Call PutGridToTxt()
        Dirty = False
        TabControl1.SelectedIndex = 1
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub txtamt_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCredit.LostFocus, _
                            txtDay_income.LostFocus, txtDay_pay.LostFocus, txtUnpay.LostFocus, txtBalance.LostFocus, txtChkForm.LostFocus
        If (Not IsNumeric(sender.text) Or ValComa(sender.text) < 0) And Trim(sender.text) <> "" Then
            MsgBox("請輸入正數字")
            sender.focus()
        Else
            If sender.name <> "txtChkForm" Then sender.text = FormatNumber(ValComa(sender.text), 2)
        End If
    End Sub
End Class
