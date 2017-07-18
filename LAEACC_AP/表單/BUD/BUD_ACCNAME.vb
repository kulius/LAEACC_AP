Imports JBC.Printing
Public Class BUD_ACCNAME
    Dim confid, confunit As String, LoadAfter, Dirty As Boolean
    Dim sqlstr As String
    Dim blnArea As Boolean = False
    Dim bm As BindingManagerBase, mydataset As DataSet
    Dim UserId, UserName, UserUnit As String

    Private Sub ACCNAME_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim myds As DataSet
        LoadAfter = True
        UserName = TransPara.TransP("username")
        UserUnit = TransPara.TransP("userunit")
        UserId = TransPara.TransP("userid")
        TabControl1.Enabled = False
        RecMove1.Enabled = False
        vxtStartNo.Text = "1"    '起值
        vxtEndNo.Text = "9"      '迄值
        If TransPara.TransP("UnitTitle").indexof("嘉南") >= 0 Then blnArea = True
    End Sub

    Private Sub BtnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSearch.Click
        TabControl1.Enabled = True
        RecMove1.Enabled = True
        Call LoadGridFunc()
        If DataGrid1.CurrentRowIndex > 0 Then Call PutGridToTxt()
    End Sub

    Sub LoadGridFunc()
        Dim sqlstr, qstr As String
        'sqlstr = "SELECT   *  FROM  accname where accno >='" & vxtStartNo.getTrimData() & "' and accno<='" & vxtEndNo.getTrimData() & "'"
        sqlstr = "SELECT   *  FROM  accname where accno between '" & GetAccno(vxtStartNo.Text) & "' and '" & GetAccno(vxtEndNo.Text) & "'"
        If txtQaccname.Text <> "" Then qstr = " and accname like '%" & Trim(txtQaccname.Text) & "%'"
        If txtQunit.Text <> "" Then qstr = qstr + " and unit like '%" & Trim(txtQunit.Text) & "%'"
        If txtQstaff_no.Text <> "" Then qstr = qstr + " and staff_no like '%" & Trim(txtQstaff_no.Text) & "%'"
        If txtQaccount_no.Text <> "" Then qstr = qstr + " and account_no like '%" & Trim(txtQaccount_no.Text) & "%'"
        If txtQaccount_no.Text <> "" Then qstr = qstr + " and account_no like '%" & Trim(txtQaccount_no.Text) & "%'"
        If blnArea = True Then
            If txtQArea.Text <> "" Then qstr = qstr + " and area = '" & Trim(txtQArea.Text) & "'"
        End If
        If ChkBelong.Checked Then
            qstr = qstr + " and belong<>'B'"
        End If
        sqlstr = sqlstr + qstr
        mydataset = openmember("", "accname", sqlstr)
        RecMove1.FilesPara = "mastconn=" & mastconn & "|sqlstr=" & sqlstr
        DataGrid1.DataSource = mydataset
        DataGrid1.DataMember = "accname"
        bm = Me.BindingContext(mydataset, "accname")

        RecMove1.Setds = bm
        TabControl1.SelectedIndex = 0
    End Sub

    Sub PutGridToTxt()
        vxtAccno.Text = bm.Current("accno")
        txtAccname.Text = nz(bm.Current("accname"), "")
        txtBelong.Text = nz(bm.Current("belong"), "")
        txtUnit.Text = nz(bm.Current("unit"), "")
        txtBank.Text = nz(bm.Current("bank"), "")
        txtStaff_no.Text = nz(bm.Current("staff_no"), "")
        txtAccount_no.Text = nz(bm.Current("account_no"), "")
        vxtBook.Text = nz(bm.Current("bookaccno"), "")
        If blnArea = True Then
            txtarea.Text = nz(bm.Current("area"), "")
        End If
    End Sub

    Sub transjob(ByVal JobName As String, ByVal JobPara As String) Handles RecMove1.TransJob
        Dim LastPos As Integer = bm.Position
        Select Case JobName
            Case "刪除記錄"
                Dim keyvalue, sqlstr, retstr As String
                If TabControl1.SelectedIndex = 0 Then
                    keyvalue = bm.Current("accno")
                Else
                    keyvalue = lblAccno.Text
                End If

                sqlstr = "delete from accname where accno='" & keyvalue & "'"
                retstr = runsql(mastconn, sqlstr)
                If retstr = "sqlok" Then
                    mydataset.Tables("accname").Rows.RemoveAt(JobPara)
                    'mydataset.Tables("accname").Clear()
                    'Call LoadGridFunc()
                    bm.Position = LastPos
                    TabControl1.SelectedIndex = 0
                End If

            Case "回寫記錄cmd"
                Dim keyvalue, sqlstr, retstr, updstr As String
                If TabControl1.SelectedIndex = 0 Then
                    Call PutGridToTxt()
                End If
                keyvalue = lblAccno.Text

                RecMove1.GenUpdsql("accno", GetAccno(vxtAccno.Text), "T")
                RecMove1.GenUpdsql("accname", Trim(txtAccname.Text), "U")
                RecMove1.GenUpdsql("belong", Trim(txtBelong.Text.ToUpper), "T")
                RecMove1.GenUpdsql("bank", Trim(txtBank.Text), "T")
                RecMove1.GenUpdsql("unit", Trim(txtUnit.Text), "T")
                RecMove1.GenUpdsql("staff_no", Trim(txtStaff_no.Text), "T")
                RecMove1.GenUpdsql("account_no", Trim(txtAccount_no.Text), "T")
                RecMove1.GenUpdsql("bookaccno", GetAccno(vxtBook.Text), "T")
                If blnArea = True Then RecMove1.GenUpdsql("area", Trim(txtArea.Text), "T")
                sqlstr = "update accname set " & RecMove1.genupdfunc & " where accno='" & keyvalue & "'"

                retstr = runsql(mastconn, sqlstr)
                If retstr = "sqlok" Then
                    Call LoadGridFunc()
                    'MsgBox("更新成功")
                    bm.Position = LastPos
                Else
                    MsgBox("更新失敗")
                End If

            Case "新增記錄"
                Dim keyvalue, sqlstr, retstr, updstr As String
                If TabControl1.SelectedIndex = 0 Then
                    Call PutGridToTxt()
                End If
                RecMove1.GenInsSql("accno", GetAccno(vxtAccno.Text), "T")
                RecMove1.GenInsSql("accname", txtAccname.Text, "U")
                RecMove1.GenInsSql("belong", txtBelong.Text.ToUpper, "T")
                RecMove1.GenInsSql("bank", txtBank.Text, "T")
                RecMove1.GenInsSql("unit", txtUnit.Text, "T")
                RecMove1.GenInsSql("staff_no", txtStaff_no.Text, "T")
                RecMove1.GenInsSql("account_no", txtAccount_no.Text, "T")
                RecMove1.GenInsSql("bookaccno", GetAccno(vxtBook.Text), "T")
                If blnArea = True Then RecMove1.GenInsSql("area", txtArea.Text, "T")
                sqlstr = "insert into accname " & RecMove1.GenInsFunc
                retstr = runsql(mastconn, sqlstr)

                If retstr = "sqlok" Then
                    Call LoadGridFunc()
                    bm.Position = LastPos
                    'MsgBox("新增成功")
                Else
                    MsgBox("新增失敗")
                End If
            Case "記錄移動"
                Dim keyvalue, sqlstr, retstr As String
                lblAccno.Text = bm.Current("accno")
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
        lblAccno.Text = bm.Current("accno")  'keep the old keyvalue
        Call PutGridToTxt()
        Dirty = False
    End Sub

    Private Sub DataGrid1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.DoubleClick
        TabControl1.SelectedIndex = 1
    End Sub

    Private Sub btnBook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBook.Click
        Dim sqlstr, qstr, retstr, strAccno, strBookAccno As String
        Dim intJ, intI, intLen, intCount As Integer
        sqlstr = "UPDATE ACCNAME SET BOOKACCNO=ACCNO where accno<>bookaccno AND belong<>'B'"   '先將會計科目全搬入記帳科目
        retstr = runsql(mastconn, sqlstr)
        lblFinish.Text = ""
        lblError.Text = ""

        '將所有會計科目置combobox   
        Dim i As Integer
        sqlstr = "SELECT accno,bookaccno FROM ACCNAME WHERE BELONG<>'B'"
        mydataset = openmember("", "ACCNAME", sqlstr)
        If mydataset.Tables("accname").Rows.Count > 1 Then
            cboAccno.DataSource = mydataset.Tables("accname")
            cboAccno.ValueMember = "accno"     '欄位值
            cboAccno.DisplayMember = "bookaccno"  '顯示欄位
        End If
        '處理預算科目
        intCount = 0  '計算更正預算科目個數
        sqlstr = "SELECT accno,bookaccno FROM accname where belong='B'"
        mydataset = openmember("", "accnamet", sqlstr)
        For intI = 0 To (mydataset.Tables("accnamet").Rows.Count - 1)
            strAccno = mydataset.Tables("accnamet").Rows(intI).Item(0)
            strBookAccno = ""
            For intJ = 7 To 4 Step -1
                Select Case intJ
                    Case 7
                        intLen = 16
                    Case 6
                        intLen = 9
                    Case 5
                        intLen = 7
                    Case 4
                        intLen = 5
                End Select
                cboAccno.SelectedValue = Mid(strAccno, 1, intLen)
                If Mid(cboAccno.SelectedValue, 1, 5) = Mid(strAccno, 1, 5) Then 'value=1 表示找到會計科目第一筆(1:資產);不是1,表示找到了.
                    strBookAccno = cboAccno.SelectedValue
                    lblFinish.Text = strBookAccno & "  TO " & strAccno
                    Exit For
                End If
                If intJ = 4 And strBookAccno = "" Then lblError.Text += "錯誤:" & strAccno
            Next
            If strBookAccno <> mydataset.Tables("accnamet").Rows(intI).Item(1) Then
                sqlstr = "update accname set bookaccno = " & strBookAccno & " where accno='" & strAccno & "'"
                retstr = runsql(mastconn, sqlstr)
                intCount += 1
            End If
        Next
        mydataset.Clear()
        lblFinish.Text = "更正完成件數=" & Str(intCount)
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If mydataset.Tables("accname").Rows.Count <= 0 Then
            Exit Sub
        End If
        '列印
        Dim printer As FPPrinter = FPPrinter.SharedPrinter
        Dim doc As New FPDocument("會計科目列印")
        doc.DefaultPageSettings.PaperKind = Printing.PaperKind.A4
        doc.DefaultPageSettings.Landscape = False

        Dim pagecnt, I As Integer
        Dim intI As Integer = 0
        Dim PageRow As Integer = 40

        For pagecnt = 0 To 999
            Dim page As New FPPage
            Dim table0 As New FPTable(0, 10, 230, 10 * (PageRow + 1), PageRow + 1, 5)
            table0.Font.Name = "標楷體"
            table0.Font.Size = 12
            table0.SetLineColor(Color.DarkBlue)
            table0.OutlineThicken(4)
            table0.ColumnStyles(1).Width = 48
            table0.ColumnStyles(2).Width = 80
            table0.ColumnStyles(3).Width = 6
            table0.ColumnStyles(4).Width = 12
            table0.ColumnStyles(5).Width = 12
            'table0.HAlignment = FPAlignment.Near
            'table0.VAlignment = FPAlignment.Center
            table0.ColumnStyles(1).HAlignment = StringAlignment.Near '整欄左靠
            table0.ColumnStyles(2).HAlignment = StringAlignment.Near '整欄左靠
            table0.Texts2D(1, 1).Text = "會計科目"
            table0.Texts2D(1, 2).Text = "會計科目名稱"
            table0.Texts2D(1, 3).Text = "歸屬"
            table0.Texts2D(1, 4).Text = "推算"
            table0.Texts2D(1, 5).Text = "主計"

            For I = 1 To PageRow
                If intI > mydataset.Tables("accname").Rows.Count - 1 Then
                    pagecnt = 1000
                    Exit For
                End If
                table0.Texts2D(I + 1, 1).Text = FormatAccno(mydataset.Tables("accname").Rows(intI)("accno"))
                table0.Texts2D(I + 1, 2).Text = mydataset.Tables("accname").Rows(intI)("accname")
                table0.Texts2D(I + 1, 3).Text = mydataset.Tables("accname").Rows(intI)("belong")
                table0.Texts2D(I + 1, 4).Text = mydataset.Tables("accname").Rows(intI)("staff_no")
                table0.Texts2D(I + 1, 5).Text = mydataset.Tables("accname").Rows(intI)("account_no")
                intI += 1
            Next
            page.Add(table0)
            doc.AddPage(page)
        Next
        printer.Document = doc
        If rdoPreview.Checked Then printer.IsAutoShowPrintPreviewDialog = True
        printer.Print()
        TabControl1.SelectedIndex = 0
    End Sub

    Private Sub btnAutoExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAutoExit.Click
        TabControl1.SelectedIndex = 0
    End Sub

    Private Sub btnAutoAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAutoAdd.Click
        If txtEaccno.Text = "" Or txtSEngno.Text = "" Or txtEEngno.Text = "" Then Exit Sub
        Dim tempds As DataSet
        sqlstr = "select engno, engname FROM enf010 where engno>='" & Trim(txtSEngno.Text) & "' and engno<='" & Trim(txtEEngno.Text) & "'"
        tempds = openmember("", "enf010", sqlstr)
        Dim intI, intJ As Integer
        intJ = 0
        Dim retstr As String
        For intI = 0 To (tempds.Tables(0).Rows.Count - 1)
            With tempds.Tables(0).Rows(intI)
                If nz(.Item("engname"), "") <> "" Then
                    GenInsSql("accno", Mid(Trim(txtEaccno.Text) & Space(9), 1, 9) & .Item("engno"), "T")
                    GenInsSql("accname", .Item("engname"), "U")
                    GenInsSql("staff_no", txtStaffno.Text, "T")
                    GenInsSql("account_no", txtAccountno.Text, "T")
                    GenInsSql("bookaccno", Mid(Trim(txtEaccno.Text) & Space(9), 1, 9) & .Item("engno"), "T")
                    sqlstr = "insert into accname " & GenInsFunc
                    retstr = runsql(mastconn, sqlstr)
                    If retstr = "sqlok" Then intJ += 1
                End If
            End With
        Next
        MsgBox("處理完畢,  新增" & Str(intJ) & "筆")
        TabControl1.SelectedIndex = 0
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        TabControl1.SelectedIndex = 0
    End Sub

    Private Sub btbChange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btbChange.Click
        If Len(Trim(txtNewNo.Text)) = 0 Or Len(Trim(txtOldNo.Text)) = 0 Then
            MsgBox("請輸入員工編號")
            Exit Sub
        End If
        If MsgBox("科目介於" & GetAccno(vxtStartNo.Text) & "至" & GetAccno(vxtEndNo.Text) & "修正" & txtOldNo.Text & "為" & txtNewNo.Text, MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
            Dim retstr As String
            If rdoStaff.Checked Then
                sqlstr = "update accname set staff_no='" & txtNewNo.Text & "' where staff_no='" & txtOldNo.Text & "'"
            Else
                sqlstr = "update accname set account_no='" & txtNewNo.Text & "' where account_no='" & txtOldNo.Text & "'"
            End If
            sqlstr &= " and accno between '" & GetAccno(vxtStartNo.Text) & "' and '" & GetAccno(vxtEndNo.Text) & "'"
            retstr = runsql(mastconn, sqlstr)
            If retstr = "sqlok" Then MsgBox("處理完成")
            TabControl1.SelectedIndex = 0
        End If
    End Sub
End Class
