Imports JBC.Printing
Public Class ACF050
    Dim confid, confunit As String, LoadAfter, Dirty As Boolean
    Dim sqlstr As String
    Dim bm As BindingManagerBase, mydataset As DataSet
    Dim UserId, UserName, UserUnit As String
    Private Sub acf050_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TabControl1.Enabled = False
        RecMove1.Enabled = False
        nudYear.Value = GetYear(TransPara.TransP("UserDate"))
        nudMM.Value = Month(TransPara.TransP("UserDate"))
        vxtStartNo.Text = "1"    '起值
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
        Dim sqlstr, qstr, strD, strC As String
        strD = "DEAMT" & Format(nudMM.Value, "00")
        strC = "CRAMT" & Format(nudMM.Value, "00")
        sqlstr = "SELECT a.*, beg_debit - beg_credit as begamt, a." & strD & " - a." & strC & " as netamt, b.accname  FROM  acf050 a LEFT OUTER JOIN accname b" & _
                 " ON a.accno = b.accno WHERE accyear=" & nudYear.Value & " and a.accno>='" & _
                 GetAccno(vxtStartNo.Text) & "' and a.accno<='" & GetAccno(vxtEndNo.Text) & "' ORDER BY a.accyear,a.accno"
        mydataset = openmember("", "acf050", sqlstr)
        RecMove1.FilesPara = "mastconn=" & mastconn & "|sqlstr=" & sqlstr
        DataGrid1.DataSource = mydataset
        DataGrid1.DataMember = "acf050"
        bm = Me.BindingContext(mydataset, "acf050")
        RecMove1.Setds = bm
        TabControl1.SelectedIndex = 0
    End Sub

    Sub PutGridToTxt()
        Dim intI As Integer = 0
        Dim strI, strColumn1, strColumn2 As String
        txtAccYear.Text = bm.Current("accyear")
        vxtAccno.Text = bm.Current("accno")
        lblAccname.Text = IIf(IsDBNull(bm.Current("accname")), " ", bm.Current("accname"))
        txtBeg_debit.Text = FormatNumber(bm.Current("beg_debit"), 2)
        txtBeg_credit.Text = FormatNumber(bm.Current("beg_credit"), 2)

        For intI = 1 To 12
            strI = Format(intI, "00")
            strColumn1 = "DeAmt" & strI
            strColumn2 = "CrAmt" & strI
            FindControl(Me, "txtdeamt" & strI).Text = FormatNumber(bm.Current(strColumn1), 2)                'function findcontrol at vbdataio.vb
            FindControl(Me, "txtcramt" & strI).Text = FormatNumber(bm.Current(strColumn2), 2)
            FindControl(Me, "lblNet" & strI).Text = FormatNumber(bm.Current(strColumn1) - bm.Current(strColumn2), 2)
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

                sqlstr = "delete from acf050 where autono=" & keyvalue
                retstr = runsql(mastconn, sqlstr)
                If retstr = "sqlok" Then
                    mydataset.Tables("acf050").Rows.RemoveAt(JobPara)
                    mydataset.Tables("acf050").Clear()
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
                RecMove1.GenUpdsql("deamt01", txtDeamt01.Text, "N")
                RecMove1.GenUpdsql("Deamt02", txtDeamt02.Text, "N")
                RecMove1.GenUpdsql("Deamt03", txtDeamt03.Text, "N")
                RecMove1.GenUpdsql("Deamt04", txtDeamt04.Text, "N")
                RecMove1.GenUpdsql("Deamt05", txtDeamt05.Text, "N")
                RecMove1.GenUpdsql("Deamt06", txtDeamt06.Text, "N")
                RecMove1.GenUpdsql("Deamt07", txtDeamt07.Text, "N")
                RecMove1.GenUpdsql("Deamt08", txtDeamt08.Text, "N")
                RecMove1.GenUpdsql("Deamt09", txtDeamt09.Text, "N")
                RecMove1.GenUpdsql("Deamt10", txtDeamt10.Text, "N")
                RecMove1.GenUpdsql("Deamt11", txtDeamt11.Text, "N")
                RecMove1.GenUpdsql("Deamt12", txtDeamt12.Text, "N")
                RecMove1.GenUpdsql("cramt01", txtCramt01.Text, "N")
                RecMove1.GenUpdsql("cramt02", txtCramt02.Text, "N")
                RecMove1.GenUpdsql("cramt03", txtCramt03.Text, "N")
                RecMove1.GenUpdsql("cramt04", txtCramt04.Text, "N")
                RecMove1.GenUpdsql("cramt05", txtCramt05.Text, "N")
                RecMove1.GenUpdsql("cramt06", txtCramt06.Text, "N")
                RecMove1.GenUpdsql("cramt07", txtCramt07.Text, "N")
                RecMove1.GenUpdsql("cramt08", txtCramt08.Text, "N")
                RecMove1.GenUpdsql("cramt09", txtCramt09.Text, "N")
                RecMove1.GenUpdsql("cramt10", txtCramt10.Text, "N")
                RecMove1.GenUpdsql("cramt11", txtCramt11.Text, "N")
                RecMove1.GenUpdsql("cramt12", txtCramt12.Text, "N")
                sqlstr = "update acf050 set " & RecMove1.genupdfunc & " where autono=" & keyvalue
                retstr = runsql(mastconn, sqlstr)
                If retstr = "sqlok" Then
                    mydataset.Tables("acf050").Rows.RemoveAt(JobPara)
                    mydataset.Tables("acf050").Clear()
                    Call LoadGridFunc()
                    MsgBox("更新成功")
                    bm.Position = LastPos
                End If

            Case "新增記錄"
                Dim keyvalue, sqlstr, retstr, updstr As String
                If TabControl1.SelectedIndex = 0 Then
                    Call PutGridToTxt()
                End If
                RecMove1.GenInsSql("accyear", txtAccYear.Text, "N")
                RecMove1.GenInsSql("accno", GetAccno(vxtAccno.Text), "T")
                RecMove1.GenInsSql("BEG_DEBIT", txtBeg_debit.Text, "N")
                RecMove1.GenInsSql("BEG_CREDIT", txtBeg_credit.Text, "N")
                RecMove1.GenInsSql("deamt01", txtDeamt01.Text, "N")
                RecMove1.GenInsSql("Deamt02", txtDeamt02.Text, "N")
                RecMove1.GenInsSql("Deamt03", txtDeamt03.Text, "N")
                RecMove1.GenInsSql("Deamt04", txtDeamt04.Text, "N")
                RecMove1.GenInsSql("Deamt05", txtDeamt05.Text, "N")
                RecMove1.GenInsSql("Deamt06", txtDeamt06.Text, "N")
                RecMove1.GenInsSql("Deamt07", txtDeamt07.Text, "N")
                RecMove1.GenInsSql("Deamt08", txtDeamt08.Text, "N")
                RecMove1.GenInsSql("Deamt09", txtDeamt09.Text, "N")
                RecMove1.GenInsSql("Deamt10", txtDeamt10.Text, "N")
                RecMove1.GenInsSql("Deamt11", txtDeamt11.Text, "N")
                RecMove1.GenInsSql("Deamt12", txtDeamt12.Text, "N")
                RecMove1.GenInsSql("cramt01", txtCramt01.Text, "N")
                RecMove1.GenInsSql("cramt02", txtCramt02.Text, "N")
                RecMove1.GenInsSql("cramt03", txtCramt03.Text, "N")
                RecMove1.GenInsSql("cramt04", txtCramt04.Text, "N")
                RecMove1.GenInsSql("cramt05", txtCramt05.Text, "N")
                RecMove1.GenInsSql("cramt06", txtCramt06.Text, "N")
                RecMove1.GenInsSql("cramt07", txtCramt07.Text, "N")
                RecMove1.GenInsSql("cramt08", txtCramt08.Text, "N")
                RecMove1.GenInsSql("cramt09", txtCramt09.Text, "N")
                RecMove1.GenInsSql("cramt10", txtCramt10.Text, "N")
                RecMove1.GenInsSql("cramt11", txtCramt11.Text, "N")
                RecMove1.GenInsSql("cramt12", txtCramt12.Text, "N")

                sqlstr = "insert into acf050 " & RecMove1.GenInsFunc
                retstr = runsql(mastconn, sqlstr)

                If retstr = "sqlok" Then
                    mydataset.Tables("acf050").Rows.RemoveAt(JobPara)
                    mydataset.Tables("acf050").Clear()
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

    Private Sub txtBeg_debit_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBeg_debit.LostFocus, txtBeg_credit.LostFocus
        If (Not IsNumeric(sender.text) Or ValComa(sender.text) < 0) And Trim(sender.text) <> "" Then
            MsgBox("請輸入正數字")
            sender.focus()
        Else
            If sender.text <> "" Then
                sender.text = FormatNumber(ValComa(sender.text), 2)
            End If
            lblNet00.Text = FormatNumber(ValComa(txtBeg_debit.Text) - ValComa(txtBeg_credit.Text), 2)
        End If
    End Sub

    Private Sub txtDeamt01_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDeamt01.LostFocus
        If (Not IsNumeric(sender.text) Or ValComa(sender.text) < 0) And Trim(sender.text) <> "" Then
            MsgBox("請輸入正數字")
            sender.focus()
        Else
            Dim strI As String = Mid(sender.name, 9, 2)
            If sender.text <> "" Then
                sender.text = FormatNumber(ValComa(sender.text), 2)
            End If
            FindControl(Me, "lblnet" & strI).Text = FormatNumber(ValComa(FindControl(Me, "txtdeamt" & strI).Text) - ValComa(FindControl(Me, "txtcramt" & strI).Text))
        End If
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If mydataset.Tables("acf050").Rows.Count <= 0 Then
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
            With mydataset.Tables("acf050")
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
