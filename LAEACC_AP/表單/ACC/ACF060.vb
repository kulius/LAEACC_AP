Imports JBC.Printing
Public Class ACF060
    Dim confid, confunit As String, LoadAfter, Dirty As Boolean

    Dim sqlstr As String
    Dim bm As BindingManagerBase, mydataset As DataSet
    Dim UserId, UserName, UserUnit As String
    Dim strI As String
    Dim intI As Integer
    Private Sub ACf060_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TabControl1.Enabled = False
        RecMove1.Enabled = False
        nudYear.Value = GetYear(Now)
        nudMM.Value = Month(Now)
        vxtStartNo.Text = "1"   '起值
        vxtEndNo.Text = "9"      '迄值
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
        Dim strD, strC As String
        strD = "DEAMT" & Format(nudMM.Value, "00")
        strC = "CRAMT" & Format(nudMM.Value, "00")
        Dim sqlstr, qstr, str1, str2, str3, str4 As String
        sqlstr = "SELECT a.*, beg_debit - beg_credit as begamt, a." & strD & " - a." & strC & " as netamt, b.accname " & _
                 "FROM  acf060 a LEFT OUTER JOIN accname b ON a.accno = b.accno " & _
                 "WHERE accyear=" & nudYear.Value & " and a.accno>='" & _
                 GetAccno(vxtStartNo.Text) & "' and a.accno<='" & GetAccno(vxtEndNo.Text) & "' ORDER BY a.accyear,a.accno"

        mydataset = openmember("", "ACf060", sqlstr)
        RecMove1.FilesPara = "mastconn=" & mastconn & "|sqlstr=" & sqlstr
        DataGrid1.DataSource = mydataset
        DataGrid1.DataMember = "ACf060"
        bm = Me.BindingContext(mydataset, "ACf060")
        RecMove1.Setds = bm
        TabControl1.SelectedIndex = 0
    End Sub

    Sub PutGridToTxt()
        txtAccYear.Text = bm.Current("accyear")
        vxtAccno.Text = bm.Current("accno")
        lblAccname.Text = nz(bm.Current("accname"), " ")
        txtBeg_debit.Text = FormatNumber(bm.Current("beg_debit"), 2)
        txtBeg_credit.Text = FormatNumber(bm.Current("beg_credit"), 2)
        For intI = 1 To 12
            strI = Format(intI, "00")
            FindControl(Me, "txtdeamt" & strI).Text = FormatNumber(bm.Current("deamt" & strI), 2)
            FindControl(Me, "txtcramt" & strI).Text = FormatNumber(bm.Current("cramt" & strI), 2)
            FindControl(Me, "txtaccount" & strI).Text = FormatNumber(bm.Current("account" & strI), 2)
            FindControl(Me, "txtact" & strI).Text = FormatNumber(bm.Current("act" & strI), 2)
            FindControl(Me, "txtsub" & strI).Text = FormatNumber(bm.Current("sub" & strI), 2)
            FindControl(Me, "txttrans" & strI).Text = FormatNumber(bm.Current("trans" & strI), 2)
            FindControl(Me, "lbldcNet" & strI).Text = FormatNumber(bm.Current("deamt" & strI) - bm.Current("cramt" & strI), 2)
        Next
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

                sqlstr = "delete from ACf060 where autono=" & keyvalue
                retstr = runsql(mastconn, sqlstr)
                If retstr = "sqlok" Then
                    mydataset.Tables("ACf060").Rows.RemoveAt(JobPara)
                    mydataset.Tables("ACf060").Clear()
                    Call LoadGridFunc()
                    bm.Position = LastPos
                End If

            Case "回寫記錄cmd"
                Dim keyvalue, sqlstr, retstr, updstr As String
                If TabControl1.SelectedIndex = 0 Then
                    Call PutGridToTxt()
                End If
                keyvalue = lblkey.Text

                RecMove1.GenUpdsql("accyear", txtAccYear.Text, "N")
                RecMove1.GenUpdsql("accno", GetAccno(vxtAccno.Text), "T")
                RecMove1.GenUpdsql("BEG_DEBIT", txtBeg_debit.Text, "N")
                RecMove1.GenUpdsql("BEG_CREDIT", txtBeg_credit.Text, "N")
                For intI = 1 To 12
                    strI = Format(intI, "00")
                    RecMove1.GenUpdsql("deamt" & strI, FindControl(Me, "txtdeamt" & strI).Text, "N")
                    RecMove1.GenUpdsql("cramt" & strI, FindControl(Me, "txtcramt" & strI).Text, "N")
                    RecMove1.GenUpdsql("account" & strI, FindControl(Me, "txtaccount" & strI).Text, "N")
                    RecMove1.GenUpdsql("act" & strI, FindControl(Me, "txtact" & strI).Text, "N")
                    RecMove1.GenUpdsql("sub" & strI, FindControl(Me, "txtsub" & strI).Text, "N")
                    RecMove1.GenUpdsql("trans" & strI, FindControl(Me, "txttrans" & strI).Text, "N")
                Next
                sqlstr = "update ACf060 set " & RecMove1.genupdfunc & " where autono=" & keyvalue
                retstr = runsql(mastconn, sqlstr)
                If retstr = "sqlok" Then
                    mydataset.Tables("ACf060").Rows.RemoveAt(JobPara)
                    mydataset.Tables("ACf060").Clear()
                    Call LoadGridFunc()
                    MsgBox("更新成功")
                    bm.Position = LastPos
                End If

            Case "新增記錄"
                Dim keyvalue, sqlstr, retstr, updstr As String
                Dim strColumn1, strColumn2, strColumn3, strColumn4, strD, strC As String
                If TabControl1.SelectedIndex = 0 Then
                    Call PutGridToTxt()
                End If
                RecMove1.GenInsSql("accyear", txtAccYear.Text, "N")
                RecMove1.GenInsSql("accno", GetAccno(vxtAccno.Text), "T")
                RecMove1.GenInsSql("BEG_DEBIT", txtBeg_debit.Text, "N")
                RecMove1.GenInsSql("BEG_CREDIT", txtBeg_credit.Text, "N")
                For intI = 1 To 12
                    strI = Format(intI, "00")
                    RecMove1.GenInsSql("deamt" & strI, FindControl(Me, "txtdeamt" & strI).Text, "N")
                    RecMove1.GenInsSql("cramt" & strI, FindControl(Me, "txtcramt" & strI).Text, "N")
                    RecMove1.GenInsSql("account" & strI, FindControl(Me, "txtaccount" & strI).Text, "N")
                    RecMove1.GenInsSql("act" & strI, FindControl(Me, "txtact" & strI).Text, "N")
                    RecMove1.GenInsSql("sub" & strI, FindControl(Me, "txtsub" & strI).Text, "N")
                    RecMove1.GenInsSql("trans" & strI, FindControl(Me, "txttrans" & strI).Text, "N")
                Next

                sqlstr = "insert into ACf060 " & RecMove1.GenInsFunc
                retstr = runsql(mastconn, sqlstr)

                If retstr = "sqlok" Then
                    mydataset.Tables("ACf060").Rows.RemoveAt(JobPara)
                    mydataset.Tables("ACf060").Clear()
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
        Me.Close()
    End Sub
    Private Sub txtDeamt01_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDeamt01.LostFocus, txtCramt01.LostFocus, _
            txtDeamt02.LostFocus, txtCramt02.LostFocus, txtDeamt03.LostFocus, txtCramt03.LostFocus, txtDeamt04.LostFocus, txtCramt04.LostFocus, _
            txtDeamt05.LostFocus, txtCramt05.LostFocus, txtDeamt06.LostFocus, txtCramt06.LostFocus, txtDeamt07.LostFocus, txtCramt07.LostFocus, _
            txtDeamt08.LostFocus, txtCramt08.LostFocus, txtDeamt09.LostFocus, txtCramt09.LostFocus, txtDeamt10.LostFocus, txtCramt10.LostFocus, _
            txtDeamt11.LostFocus, txtCramt11.LostFocus, txtDeamt12.LostFocus, txtCramt12.LostFocus
        If (Not IsNumeric(sender.text) Or ValComa(sender.text) < 0) And Trim(sender.text) <> "" Then
            MsgBox("請輸入正數字")
            sender.focus()
        Else
            Dim strI As String = Mid(sender.name, 9, 2)
            If sender.text <> "" Then
                sender.text = FormatNumber(ValComa(sender.text), 2)
            End If
            FindControl(Me, "lblDcNet" & strI).Text = FormatNumber(ValComa(FindControl(Me, "txtdeamt" & strI).Text) - ValComa(FindControl(Me, "txtcramt" & strI).Text))
        End If
    End Sub

    Private Sub txtAccount01_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccount01.LostFocus, txtAct01.LostFocus, txtSub01.LostFocus, txtTrans01.LostFocus, _
                txtAccount02.LostFocus, txtAct02.LostFocus, txtSub02.LostFocus, txtTrans02.LostFocus, txtAccount08.LostFocus, txtAct08.LostFocus, txtSub08.LostFocus, txtTrans08.LostFocus, _
                txtAccount03.LostFocus, txtAct03.LostFocus, txtSub03.LostFocus, txtTrans03.LostFocus, txtAccount09.LostFocus, txtAct09.LostFocus, txtSub09.LostFocus, txtTrans09.LostFocus, _
                txtAccount04.LostFocus, txtAct04.LostFocus, txtSub04.LostFocus, txtTrans04.LostFocus, txtAccount10.LostFocus, txtAct10.LostFocus, txtSub10.LostFocus, txtTrans10.LostFocus, _
                txtAccount05.LostFocus, txtAct05.LostFocus, txtSub05.LostFocus, txtTrans05.LostFocus, txtAccount11.LostFocus, txtAct11.LostFocus, txtSub11.LostFocus, txtTrans11.LostFocus, _
                txtAccount06.LostFocus, txtAct06.LostFocus, txtSub06.LostFocus, txtTrans06.LostFocus, txtAccount12.LostFocus, txtAct12.LostFocus, txtSub12.LostFocus, txtTrans12.LostFocus, _
                txtAccount07.LostFocus, txtAct07.LostFocus, txtSub07.LostFocus, txtTrans07.LostFocus
        If Not IsNumeric(sender.text) And Trim(sender.text) <> "" Then
            MsgBox("請輸入數字")
            sender.focus()
        Else
            If sender.text <> "" Then
                sender.text = FormatNumber(ValComa(sender.text), 2)
            End If
        End If
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If mydataset.Tables("acf060").Rows.Count <= 0 Then
            Exit Sub
        End If
        Dim sqlstr, qstr, strD, strC As String
        strD = "DEAMT" & Format(nudMM.Value, "00")
        strC = "CRAMT" & Format(nudMM.Value, "00")
        '列印
        Dim printer = New KPrint
        Dim doc As New FPDocument("會計科目餘額列印")
        doc.DefaultPageSettings.PaperKind = Printing.PaperKind.A4
        doc.DefaultPageSettings.Landscape = False
        doc.SetDefaultPageMargin(10, 10, 0, 10)    'left,top,right,botton   10, 0, 10, 10
        doc.DefaultFont = New Font("新細明體", 9) '標楷體
        Dim pagecnt, I As Integer
        Dim intI As Integer = 0
        Dim PageRow As Integer = 38
        Dim textTitle As FPText
        textTitle = New FPText("會計科目餘額明細    " & nudYear.Value & "年" & nudMM.Value & "月", 50, 5)   '帳簿抬頭
        'textTitle.HAlignment = FPAlignment.Center
        'textTitle.Font.Size = 12
        For pagecnt = 0 To 999
            Dim page As New FPPage
            Dim table0 As New FPTable(0, 10, 190, 7 * (PageRow + 1), PageRow + 1, 5)
            'table0.Font.Name = "標楷體"
            table0.Font.Size = 9
            table0.SetLineColor(Color.DarkBlue)
            table0.OutlineThicken(4)
            table0.ColumnStyles(1).Width = 70
            table0.ColumnStyles(2).Width = 30
            table0.ColumnStyles(3).Width = 30
            table0.ColumnStyles(4).Width = 30
            table0.ColumnStyles(5).Width = 30
            'table0.HAlignment = FPAlignment.Near
            'table0.VAlignment = FPAlignment.Center
            'table0.ColumnStyles(1).HAlignment = StringAlignment.Near '整欄左靠
            table0.ColumnStyles(1).HAlignment = StringAlignment.Near '整欄左靠
            table0.ColumnStyles(2).HAlignment = StringAlignment.Far '整欄右靠
            table0.ColumnStyles(3).HAlignment = StringAlignment.Far '整欄右靠
            table0.ColumnStyles(4).HAlignment = StringAlignment.Far '整欄右靠
            table0.ColumnStyles(5).HAlignment = StringAlignment.Far '整欄右靠
            table0.Texts2D(1, 1).Text = "會計科目名稱"
            table0.Texts2D(1, 2).Text = "年初餘額"
            table0.Texts2D(1, 3).Text = "本月借方總額"
            table0.Texts2D(1, 4).Text = "本月貸方總額"
            table0.Texts2D(1, 5).Text = "本月餘額"
            With mydataset.Tables("acf060")
                For I = 1 To PageRow
                    If intI > .Rows.Count - 1 Then
                        pagecnt = 1000
                        Exit For
                    End If
                    table0.Texts2D(I + 1, 1).Text = FormatAccno(.Rows(intI)("accno")) & _
                                                    nz(.Rows(intI)("accname"), "")
                    table0.Texts2D(I + 1, 2).Text = FormatNumber(nz(.Rows(intI)("begamt"), 0), 2)
                    table0.Texts2D(I + 1, 3).Text = FormatNumber(nz(.Rows(intI)(strD), 0), 2)
                    table0.Texts2D(I + 1, 4).Text = FormatNumber(nz(.Rows(intI)(strC), 0), 2)
                    table0.Texts2D(I + 1, 5).Text = FormatNumber(nz(.Rows(intI)("netamt"), 0), 2)
                    intI += 1
                Next
            End With

            page.Add(textTitle)
            page.Add(table0)
            doc.AddPage(page)
        Next
        printer.Document = doc
        If rdoPreview.Checked Then printer.IsAutoShowPrintPreviewDialog = True
        printer.Print()
        TabControl1.SelectedIndex = 0
    End Sub
End Class
