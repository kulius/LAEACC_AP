Imports JBC.Printing
Public Class BGQ100
    Dim confid, confunit As String, LoadAfter, Dirty As Boolean
    Dim sqlstr As String
    Dim bm, bm2, bm3 As BindingManagerBase, mydataset, mydataset2, mydataset3, TempDataSet, userdataset As DataSet
    Dim UserId, UserName, UserUnit As String
    Dim intUnitLen As Integer = 0

    Private Sub BGQ100_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TabControl1.Enabled = False
        UserId = TransPara.TransP("userid")
        UserUnit = TransPara.TransP("userunit")
        intUnitLen = Len(UserUnit)
        nudYear.Value = GetYear(TransPara.TransP("userdate"))
        nudQyear.Value = nudYear.Value
        'If Mid(UserUnit, 1, 2) = "05" Or Mid(UserUnit, 1, 2) = "08" Then
        '    'cboUser.Visible = True
        '    txtRemark.Visible = True     '主計才可修改摘要 受款人 傳票號
        '    txtRemark3.Visible = True    '主計才可修改摘要 受款人 傳票號
        '    txtSubject.Visible = True
        '    txtNo_1_no.Visible = True
        '    sqlstr = "SELECT b.staff_no as userid, b.staff_no+USERTABLE.USERNAME as username FROM USERTABLE right outer JOIN" & _
        ' "(SELECT STAFF_NO FROM ACCNAME WHERE STAFF_NO IS NOT NULL AND STAFF_NO <> '    ' GROUP BY STAFF_NO) b " & _
        ' "ON USERTABLE.USERID = b.STAFF_NO order by usertable.userid"
        '    userdataset =  openmember("", "user", sqlstr)
        '    If userdataset.Tables("user").Rows.Count = 0 Then
        '        cboUser.Text = "無user"
        '    Else
        '        cboUser.DataSource = userdataset.Tables("user")
        '        cboUser.ValueMember = "userid"     '欄位值
        '        cboUser.DisplayMember = "username"  '顯示欄位
        '    End If
        'End If
    End Sub

    Private Sub BtnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSearch.Click
        TabControl1.Enabled = True
        'If Mid(UserUnit, 1, 2) = "05" Or Mid(UserUnit, 1, 2) = "08" Then
        '    UserId = cboUser.SelectedValue
        'End If
        Call LoadGridFunc()
        If DataGrid1.CurrentRowIndex > 0 Then
            bm.Position = 1
            Call PutGridToGrid2()
        End If
    End Sub

    Sub LoadGridFunc()
        '丟當年度所有預算科目to Grid1 
        Dim sqlstr, qstr, strD, strC As String
        sqlstr = "SELECT a.accyear,a.accno, " & _
                 "CASE WHEN len(a.accno)=17 THEN c.accname+'－'+b.accname " & _
                     " WHEN len(a.accno)>9 THEN d.accname+'－'+b.accname " & _
                     " WHEN len(a.accno)<=9 THEN b.accname END AS accname, " & _
         "a.unit, a.bg1+a.bg2+a.bg3+a.bg4+a.bg5+a.up1+a.up2+a.up3+a.up4 as bgamt, a.totper, a.totUSE, a.ctrl, a.flow  " & _
         "FROM bgf010 a INNER JOIN ACCNAME b ON a.ACCNO = b.ACCNO INNER JOIN ACCNAME c ON LEFT(a.ACCNO, 16) = c.ACCNO " & _
         "INNER JOIN ACCNAME d ON LEFT(a.ACCNO, 9) = d.ACCNO " & _
         "WHERE a.accyear=" & nudYear.Value & " and left(b.unit," & intUnitLen & ") = '" & Trim(UserUnit) & "' order by a.accno"
        mydataset = openmember("", "BGF010", sqlstr)
        DataGrid1.DataSource = mydataset
        DataGrid1.DataMember = "BGF010"
        bm = Me.BindingContext(mydataset, "BGF010")
        TabControl1.SelectedIndex = 0
    End Sub

    Sub PutGridToGrid2()
        '丟選定之當年度預算科目所有之開支to Grid2 
        Dim sqlstr, qstr, strD, strC As String
        Dim i, TOTBG, TOTUSE As Integer
        If bm.Position < 0 Then Exit Sub
        lblAccno1.Text = nz(bm.Current("accno"), "")
        lblAccname1.Text = nz(bm.Current("accname"), "")
        lblBgamt.Text = Format(nz(bm.Current("bgamt"), 0), "###,###,###")
        lblUsed.Text = Format(nz(bm.Current("totuse"), 0), "###,###,###")
        lblUnuse.Text = Format(nz(bm.Current("totper"), 0), "###,###,###")
        lblNet.Text = Format((nz(bm.Current("bgamt"), 0) - nz(bm.Current("totuse"), 0) - nz(bm.Current("totper"), 0)), "###,###,###")
        sqlstr = "SELECT bgf020.bgno, bgf020.accyear, BGF020.accno, bgf020.date1, bgf020.date2, bgf020.amt1, bgf020.remark, " & _
                 "bgf020.amt2, bgf020.amt3, bgf020.useableamt, ACCNAME.ACCNAME AS ACCNAME, bgf020.kind,bgf020.subject, bgf020.closemark, " & _
                 "bgf030.rel, bgf030.date3, bgf030.date4, bgf030.useamt, bgf030.no_1_no,bgf030.autono, bgf030.remark as remark3 " & _
                 "FROM BGF020 left outer JOIN bgf030 on bgf020.bgno=bgf030.bgno inner join ACCNAME ON BGF020.ACCNO = ACCNAME.ACCNO " & _
                 " WHERE BGF020.ACCYEAR=" & nudYear.Value & " AND BGF020.accno='" & lblAccno1.Text & "' ORDER BY BGF020.bgno"
        mydataset2 = openmember("", "BGF030", sqlstr)
        DataGrid2.DataSource = mydataset2
        DataGrid2.DataMember = "BGF030"
        bm2 = Me.BindingContext(mydataset2, "BGF030")
        If bm2.Count <= 0 Then
            btnPrint.Enabled = False
        Else
            btnPrint.Enabled = True
        End If
        TabControl1.SelectedIndex = 0
    End Sub

    Sub PutGridToTxt()
        Dim intI, SumUp As Integer
        Dim strI, strColumn1, strColumn2 As String
        If bm2.Position < 0 Then Exit Sub
        lblBgno.Text = bm2.Current("bgno")   '不允許修改bgno,accyear,accno
        lblRel.Text = Format(nz(bm2.Current("rel"), 0), "##")
        lblYear.Text = bm2.Current("accyear")
        lblAccno.Text = bm2.Current("accno")
        lblAccname.Text = IIf(IsDBNull(bm2.Current("accname")), " ", bm2.Current("accname"))
        lblDate1.Text = nz(bm2.Current("date1"), "")
        lblDate2.Text = nz(bm2.Current("date2"), "")
        lblDate3.Text = nz(bm2.Current("date3"), "")
        lblDate4.Text = nz(bm2.Current("date4"), "")
        lblAutoNo.Text = nz(bm2.Current("autono"), "0")

        If bm2.Current("kind") = "1" Then
            rdbKind1.Checked = True
        Else
            rdbkind2.Checked = True
        End If
        lblRemark.Text = nz(bm2.Current("remark"), " ")
        lblRemark3.Text = nz(bm2.Current("remark3"), " ")
        lblUseAmt.Text = Format(nz(bm2.Current("useamt"), 0), "###,###,###")  '開支要金額
        lblAmt1.Text = Format(nz(bm2.Current("amt1"), 0), "###,###,###")
        lblAmt2.Text = Format(nz(bm2.Current("amt2"), 0), "###,###,###")
        lblAmt3.Text = Format(nz(bm2.Current("amt3"), 0), "###,###,###")
        lblSubject.Text = nz(bm2.Current("SUBJECT"), "") 'Mid(nz(bm2.Current("SUBJECT"), ""), 1, 7)
        lblNo_1_no.Text = Format(nz(bm2.Current("no_1_no"), 0), "#####")
        lblkey.Text = Trim(bm2.Current("bgno"))
        'txtRemark.Text = lblRemark.Text
        'txtRemark3.Text = lblRemark3.Text
        'txtSubject.Text = lblSubject.Text
        'txtNo_1_no.Text = lblNo_1_no.Text
    End Sub

    Private Sub DataGrid1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataGrid1.DoubleClick
        Call PutGridToGrid2()
        TabControl1.SelectedIndex = 1
    End Sub

    Private Sub DataGrid2_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataGrid2.DoubleClick
        Call PutGridToTxt()
        TabControl1.SelectedIndex = 2
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        TabControl1.SelectedIndex = 1
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnBack1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack1.Click
        TabControl1.SelectedIndex = 0
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If mydataset2.Tables("bgf030").Rows.Count <= 0 Then
            Exit Sub
        End If
        Dim blnSub As Boolean = False '依摘要前2字小計
        Dim blnMon As Boolean = False '依請購月份小計
        Dim strSub As String = ""
        Dim intMon As Integer = 0
        Dim subUnuse As Decimal = 0
        Dim subUsed As Decimal = 0
        Dim TotUnuse, TotUsed As Decimal
        TotUnuse = 0 : TotUsed = 0
        If MsgBox("是否要依摘要前2字小計", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            blnSub = True
        Else
            If MsgBox("是否要依請購月份小計", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                blnMon = True
            End If
        End If

        '列印
        Dim printer = New KPrint
        Dim doc As New FPDocument("推算簿列印")
        doc.DefaultPageSettings.PaperKind = Printing.PaperKind.A4
        doc.DefaultPageSettings.Landscape = True
        doc.DefaultFont = New Font("新細明體", 11) '標楷體
        doc.SetDefaultPageMargin(20, 10, 5, 5)   'left top right bottom

        Dim pagecnt, I As Integer
        Dim intI As Integer = 0
        Dim PageRow As Integer = 25
        For pagecnt = 0 To 9999
            Dim page As New FPPage
            Dim text1 As New FPText("預算科目: " & FormatAccno(lblAccno1.Text) & " " & lblAccname1.Text, 0, 0)
            page.AddText(text1)
            Dim text2 As New FPText("總預算:" & lblBgamt.Text & "   請購中:" & lblUnuse.Text & _
                                    "   已開支:" & lblUsed.Text & "    預算餘額:" & _
               Format(ValComa(lblBgamt.Text) - ValComa(lblUsed.Text) - ValComa(lblUnuse.Text), "###,###,###,###"), 0, 5)
            page.AddText(text2)
            Dim text3 As New FPText("列印日期:" & ShortDate(TransPara.TransP("userdate")) & "  頁次:" & pagecnt + 1, 210, 5)
            page.AddText(text3)
            Dim table0 As New FPTable(0, 10, 260, 7 * PageRow, PageRow, 7)
            table0.Font.Name = "標楷體"
            table0.Font.Size = 11
            table0.SetLineColor(Color.DarkBlue)
            table0.OutlineThicken(4)
            table0.ColumnStyles(1).Width = 20
            table0.ColumnStyles(2).Width = 20
            table0.ColumnStyles(3).Width = 110
            table0.ColumnStyles(4).Width = 30
            table0.ColumnStyles(5).Width = 20
            table0.ColumnStyles(6).Width = 30
            table0.ColumnStyles(7).Width = 30
            'table0.HAlignment = FPAlignment.Near
            'table0.VAlignment = FPAlignment.Center
            table0.ColumnStyles(3).HAlignment = StringAlignment.Near '整欄左靠
            table0.ColumnStyles(4).HAlignment = StringAlignment.Far  '整欄右靠
            table0.ColumnStyles(6).HAlignment = StringAlignment.Far  '整欄右靠
            table0.ColumnStyles(7).HAlignment = StringAlignment.Near '整欄左靠
            table0.Texts2D(1, 1).Text = "請購編號"
            table0.Texts2D(1, 2).Text = "請購日期"
            table0.Texts2D(1, 3).Text = "　　　　摘　　　　　要"
            table0.Texts2D(1, 4).Text = "請購金額　."
            table0.Texts2D(1, 5).Text = "開支日期"
            table0.Texts2D(1, 6).Text = "開支金額　."
            table0.Texts2D(1, 7).Text = " 受 款 人"

            For I = 2 To PageRow
                bm2.Position = intI
                If intI > bm2.Count - 1 Then
                    If blnSub Or intMon Then
                        If blnSub Then table0.Texts2D(I, 3).Text = "    小  計"
                        If blnMon Then table0.Texts2D(I, 3).Text = "    月  計"
                        table0.Texts2D(I, 4).Text = Format(subUnuse, "###,###,###,###")
                        table0.Texts2D(I, 6).Text = Format(subUsed, "###,###,###,###")
                        I += 1
                        If I > PageRow Then GoTo exitfor
                    End If
                    table0.Texts2D(I, 3).Text = "    合  計"
                    'table0.Texts2D(I, 4).Text = Format(ValComa(lblUnuse.Text), "###,###,###,###")
                    table0.Texts2D(I, 4).Text = Format(TotUnuse, "###,###,###,###")
                    table0.Texts2D(I, 6).Text = Format(TotUsed, "###,###,###,###")
                    pagecnt = 10000
                    Exit For
                End If

                If intI = 0 Then
                    strSub = Mid(nz(bm2.Current("remark"), ""), 1, 2)
                    'If nz(bm2.Current("date1"), "") <> "" Then
                    intMon = Month(bm2.Current("date1"))
                    'End If
                End If
                If blnSub Then  '依摘要前2字小計
                    If Mid(nz(bm2.Current("remark"), ""), 1, 2) <> strSub Then
                        table0.Texts2D(I, 3).Text = "    小  計"
                        table0.Texts2D(I, 4).Text = Format(subUnuse, "###,###,###,###")
                        table0.Texts2D(I, 6).Text = Format(subUsed, "###,###,###,###")
                        subUnuse = 0 : subUsed = 0
                        strSub = Mid(nz(bm2.Current("remark"), ""), 1, 2)
                        I += 1
                        If I > PageRow Then GoTo exitfor
                    End If
                    If nz(bm2.Current("useamt"), 0) <> 0 Then
                        subUsed += nz(bm2.Current("useamt"), 0)  '已開支
                    Else
                        subUnuse += nz(bm2.Current("useableamt"), 0)   'nz(bm2.Current("amt1"), 0)
                    End If
                End If
                If blnMon Then  '依請購月份小計
                    If Month(bm2.Current("date1")) <> intMon Then
                        table0.Texts2D(I, 3).Text = "    月  計"
                        table0.Texts2D(I, 4).Text = Format(subUnuse, "###,###,###,###")
                        table0.Texts2D(I, 6).Text = Format(subUsed, "###,###,###,###")
                        subUnuse = 0 : subUsed = 0
                        intMon = Month(bm2.Current("date1"))
                        I += 1
                        If I > PageRow Then GoTo exitfor
                    End If
                    If nz(bm2.Current("useamt"), 0) <> 0 Then
                        subUsed += nz(bm2.Current("useamt"), 0)  '已開支
                    Else
                        subUnuse += nz(bm2.Current("useableamt"), 0)   'nz(bm2.Current("amt1"), 0)
                    End If
                End If
                table0.Texts2D(I, 1).Text = nz(bm2.Current("bgno"), "")
                table0.Texts2D(I, 2).Text = ShortDate(nz(bm2.Current("date1"), ""))
                table0.Texts2D(I, 3).Text = nz(bm2.Current("remark"), "")
                table0.Texts2D(I, 4).Text = Format(nz(bm2.Current("useableamt"), 0), "###,###,###,###")
                table0.Texts2D(I, 5).Text = ShortDate(nz(bm2.Current("date3"), ""))
                table0.Texts2D(I, 6).Text = Format(nz(bm2.Current("useamt"), 0), "###,###,###,###")
                table0.Texts2D(I, 7).Text = Mid(nz(bm2.Current("subject"), ""), 1, 7)
                table0.Texts2D(I, 3).Font.Size = 10
                If nz(bm2.Current("useamt"), 0) = 0 Then
                    TotUnuse += nz(bm2.Current("useableamt"), 0)
                Else
                    TotUsed += nz(bm2.Current("useamt"), 0)
                End If
                intI += 1
ExitFor:    Next
            page.Add(table0)
            doc.AddPage(page)
        Next
        printer.Document = doc
        printer.IsAutoShowPrintDialog = True '自動顯示印表機設定對話盒
        If MsgBox("要直接列印嗎? (win98請選直接列印) yes/no)", MsgBoxStyle.DefaultButton1 + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
            printer.IsAutoShowPrintPreviewDialog = True
        End If
        printer.Print()
        TabControl1.SelectedIndex = 0
    End Sub


    Private Sub btnQbgno_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQbgno.Click
        If txtQbgno.Text = "" Or Len(txtQbgno.Text) <> 8 Then
            MsgBox("請輸入8碼請購編號")
            Exit Sub
        End If
        sqlstr = "SELECT a.*, b.* from bgf020 a left outer join bgf030 b on a.bgno=b.bgno " & _
                 " where a.bgno='" & txtQbgno.Text & "'"
        mydataset2 = openmember("", "BGF020", sqlstr)
        If mydataset2.Tables(0).Rows.Count > 0 Then
            With mydataset2.Tables(0)
                lblQyear.Text = nz(.Rows(0).Item("accyear"), 0)
                lblQAccno.Text = nz(.Rows(0).Item("accno"), "")
                lblQRemark.Text = nz(.Rows(0).Item("remark"), "")
                lblQAmt1.Text = FormatNumber(nz(.Rows(0).Item("amt1"), 0), 0)
                lblQAmt2.Text = FormatNumber(nz(.Rows(0).Item("amt2"), 0), 0)
                lblQAmt3.Text = FormatNumber(nz(.Rows(0).Item("amt3"), 0), 0)
                lblQUseAmt.Text = FormatNumber(nz(.Rows(0).Item("useamt"), 0), 0)
                lblQDate1.Text = ShortDate(nz(.Rows(0).Item("date1"), ""))
                lblQDate2.Text = ShortDate(nz(.Rows(0).Item("date2"), ""))
                lblQDate3.Text = ShortDate(nz(.Rows(0).Item("date3"), ""))
                lblQDate4.Text = ShortDate(nz(.Rows(0).Item("date4"), ""))
            End With
            sqlstr = "SELECT a.*, b.USERNAME FROM ACCNAME a LEFT OUTER JOIN USERTABLE b ON a.STAFF_NO = b.USERID WHERE a.ACCNO ='" & lblQAccno.Text & "'"
            mydataset2 = openmember("", "BGF020", sqlstr)
            If mydataset2.Tables(0).Rows.Count > 0 Then
                lblQUserId.Text = nz(mydataset2.Tables(0).Rows(0).Item("username"), "")
                lblQAccname.Text = nz(mydataset2.Tables(0).Rows(0).Item("accname"), "")
            End If
        End If
    End Sub


    Private Sub btnQsearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQsearch.Click
        If Trim(txtQremark.Text) = "" And ValComa(txtQamt.Text) = 0 Then Exit Sub
        sqlstr = "SELECT bgf020.bgno, bgf020.accyear, BGF020.accno, bgf020.date1, bgf020.date2, bgf020.amt1, bgf020.remark, " & _
                 "bgf020.amt2, bgf020.amt3, bgf020.useableamt, ACCNAME.ACCNAME AS ACCNAME, bgf020.kind,bgf020.subject, bgf020.closemark, " & _
                 "bgf030.rel, bgf030.date3, bgf030.date4, bgf030.useamt, bgf030.no_1_no " & _
                 "FROM BGF020 left outer JOIN bgf030 on bgf020.bgno=bgf030.bgno inner join ACCNAME ON BGF020.ACCNO = ACCNAME.ACCNO " & _
                 " WHERE BGF020.ACCYEAR=" & nudQyear.Value
        If Trim(txtQremark.Text) <> "" Then
            sqlstr = sqlstr & " AND BGF020.remark like '%" & Trim(txtQremark.Text) & "%'"
        End If
        If ValComa(txtQamt.Text) <> 0 Then
            sqlstr = sqlstr & " AND (BGF020.amt1=" & ValComa(txtQamt.Text) & " or bgf030.useamt=" & ValComa(txtQamt.Text) & ")"
        End If
        If Mid(TransPara.TransP("Userunit"), 1, 2) <> "05" Then sqlstr += " and left(accname.unit," & intUnitLen & ")='" & TransPara.TransP("Userunit") & "'"
        sqlstr += " ORDER BY BGF020.bgno"
        mydataset3 = openmember("", "BGF030", sqlstr)
        DataGridQuery.DataSource = mydataset3
        DataGridQuery.DataMember = "BGF030"
        bm3 = Me.BindingContext(mydataset3, "BGF030")
    End Sub

    Private Sub DataGridQuery_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridQuery.DoubleClick
        txtQbgno.Text = bm3.Current("bgno")
        TabControl1.SelectedIndex = 3
        btnGoQuery.Visible = True
        Call btnQbgno_Click(New System.Object, New System.EventArgs)
    End Sub

    Private Sub btnGoQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGoQuery.Click
        btnGoQuery.Visible = False
        TabControl1.SelectedIndex = 4
    End Sub

End Class
