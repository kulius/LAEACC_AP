Imports JBC.Printing
Public Class BGP050
    Dim confid, confunit As String, LoadAfter, Dirty As Boolean
    Dim sqlstr As String
    Dim bm, bm2 As BindingManagerBase, mydataset, mydataset2, TempDataSet, userdataset As DataSet
    Dim UserId, UserName, UserUnit As String

    Private Sub BGP050_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        UserId = TransPara.TransP("userid")
        UserUnit = TransPara.TransP("userunit")
        nudYear.Value = GetYear(TransPara.TransP("userdate"))
        If Mid(UserUnit, 1, 2) = "05" Or Mid(UserUnit, 1, 2) = "08" Then
            cboUser.Visible = True
            sqlstr = "SELECT b.staff_no as userid, b.staff_no+USERTABLE.USERNAME as username FROM USERTABLE right outer JOIN" & _
                    "(SELECT STAFF_NO FROM ACCNAME WHERE STAFF_NO IS NOT NULL AND STAFF_NO <> '    ' GROUP BY STAFF_NO) b " & _
                    "ON USERTABLE.USERID = b.STAFF_NO order by usertable.userid"
            userdataset = openmember("", "user", sqlstr)
            If userdataset.Tables("user").Rows.Count = 0 Then
                cboUser.Text = "無user"
            Else
                cboUser.DataSource = userdataset.Tables("user")
                cboUser.ValueMember = "userid"     '欄位值
                cboUser.DisplayMember = "username"  '顯示欄位
            End If
        End If
        vxtStartNo.Text = "1"   '起值
        vxtEndNo.Text = "1"      '迄值
    End Sub

    Private Sub BtnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrint.Click
        Dim intR As Integer = 0  'control record number
        Dim intD, i As Integer
        Dim retstr, strRemark, strAccno, strAccname As String
        Dim bgamt, used, unuse, TotUnuse, TotUsed, TotUnuse1, TotUnuse2, TotUnuse3 As Decimal

        Dim printer = New KPrint
        Dim doc As New FPDocument("推算簿列印")
        doc.DefaultPageSettings.PaperKind = Printing.PaperKind.A4
        doc.DefaultPageSettings.Landscape = True
        doc.DefaultFont = New Font("新細明體", 10) '標楷體
        doc.SetDefaultPageMargin(10, 10, 5, 5)   'left top right bottom

        Dim PageRow As Integer = 25

        If Mid(UserUnit, 1, 2) = "05" Or Mid(UserUnit, 1, 2) = "08" Then
            UserId = cboUser.SelectedValue
        End If

        '丟當年度所有預算科目to mydataset 逐科目列印
        Dim sqlstr, qstr, strD, strC As String
        sqlstr = "SELECT a.accyear,a.accno, " & _
                 "CASE WHEN len(a.accno)=17 THEN c.accname+'－'+b.accname " & _
                     " WHEN len(a.accno)>9 THEN d.accname+'－'+b.accname " & _
                     " WHEN len(a.accno)<=9 THEN b.accname END AS accname, " & _
         "a.unit, a.bg1+a.bg2+a.bg3+a.bg4+a.bg5+a.up1+a.up2+a.up3+a.up4 as bgamt, a.totper, a.totUSE, a.ctrl  " & _
         "FROM bgf010 a INNER JOIN ACCNAME b ON a.ACCNO = b.ACCNO INNER JOIN ACCNAME c ON LEFT(a.ACCNO, 16) = c.ACCNO " & _
         "INNER JOIN ACCNAME d ON LEFT(a.ACCNO, 9) = d.ACCNO " & _
         "WHERE a.accyear=" & nudYear.Value & " and b.STAFF_NO = '" & Trim(UserId) & _
         "' and a.accno>='" & GetAccno(vxtStartNo.Text) & "' and a.accno<='" & GetAccno(vxtEndNo.Text) & _
         "' order by a.accno"
        mydataset = openmember("", "BGF010", sqlstr)
        Dim PageNo As Integer = 0
        For intD = 0 To mydataset.Tables("bgf010").Rows.Count - 1
            strAccno = mydataset.Tables("bgf010").Rows(intD).Item("accno")  '列印銀行
            strAccname = nz(mydataset.Tables("bgf010").Rows(intD).Item("accname"), "")
            bgamt = nz(mydataset.Tables("bgf010").Rows(intD).Item("bgamt"), 0)
            used = nz(mydataset.Tables("bgf010").Rows(intD).Item("totuse"), 0)
            unuse = nz(mydataset.Tables("bgf010").Rows(intD).Item("totper"), 0)
            TotUnuse = 0 : TotUsed = 0
            TotUnuse1 = 0 : TotUnuse2 = 0 : TotUnuse3 = 0
            intR = 0
            '找資料
            '丟選定之當年度預算科目所有之開支to Grid2 
            sqlstr = "SELECT bgf020.bgno, bgf020.accyear, BGF020.accno, bgf020.date1, bgf020.date2, bgf020.amt1, bgf020.remark, " & _
                     "bgf020.amt2, bgf020.amt3, bgf020.useableamt, ACCNAME.ACCNAME AS ACCNAME, bgf020.kind,bgf020.subject, bgf020.closemark, " & _
                     "bgf030.date3, bgf030.date4, bgf030.useamt, bgf030.no_1_no  " & _
                     "FROM BGF020 left outer JOIN bgf030 on bgf020.bgno=bgf030.bgno inner join ACCNAME ON BGF020.ACCNO = ACCNAME.ACCNO " & _
                     " WHERE BGF020.ACCYEAR=" & nudYear.Value & " AND BGF020.accno='" & strAccno & "' ORDER BY BGF020.bgno"
            mydataset2 = openmember("", "BGF030", sqlstr)

            For PageCnt As Integer = 1 To 999    '頁次
                Dim page As New FPPage
                Dim text1 As New FPText("預算科目: " & FormatAccno(strAccno) & " " & strAccname, 0, 0)
                page.AddText(text1)
                Dim text2 As New FPText("總預算:" & Format(bgamt, "###,###,###,###.#") & _
                                      "   請購中:" & Format(unuse, "###,###,###,###.#") & _
                                      "   已開支:" & Format(used, "###,###,###,###.#") & _
                                      "   預算餘額:" & Format(bgamt - unuse - used, " ###,###,###,###.#"), 0, 5)
                page.AddText(text2)
                PageNo += 1
                Dim text3 As New FPText("列印日期:" & ShortDate(TransPara.TransP("userdate")) & _
                                        "  頁次:" & PageNo, 220, 5)
                page.AddText(text3)
                Dim table0 As New FPTable(0, 10, 270, 7 * PageRow, PageRow, 10) '(0, 10, 260, 7 * PageRow, PageRow, 8)
                table0.Font.Name = "標楷體"
                table0.Font.Size = 10   '11
                table0.SetLineColor(Color.DarkBlue)
                table0.OutlineThicken(4)
                table0.ColumnStyles(1).Width = 17    '20
                table0.ColumnStyles(2).Width = 17    '20
                table0.ColumnStyles(3).Width = 90   '101
                table0.ColumnStyles(4).Width = 25    '28
                table0.ColumnStyles(5).Width = 25
                table0.ColumnStyles(6).Width = 25
                table0.ColumnStyles(7).Width = 17    '20
                table0.ColumnStyles(8).Width = 25    '28
                table0.ColumnStyles(9).Width = 17    '30
                table0.ColumnStyles(10).Width = 12    '13
                table0.ColumnStyles(3).HAlignment = StringAlignment.Near '整欄左靠
                table0.ColumnStyles(4).HAlignment = StringAlignment.Far  '整欄右靠
                table0.ColumnStyles(5).HAlignment = StringAlignment.Far  '整欄右靠
                table0.ColumnStyles(6).HAlignment = StringAlignment.Far  '整欄右靠
                table0.ColumnStyles(8).HAlignment = StringAlignment.Far  '整欄右靠
                table0.ColumnStyles(9).HAlignment = StringAlignment.Near '整欄左靠
                table0.ColumnStyles(10).HAlignment = StringAlignment.Far  '整欄右靠
                table0.Texts2D(1, 1).Text = "請購編號"
                table0.Texts2D(1, 2).Text = "請購日期"
                table0.Texts2D(1, 3).Text = "　摘　　　　　要"
                table0.Texts2D(1, 4).Text = "請購金額."
                table0.Texts2D(1, 5).Text = "發包金額."
                table0.Texts2D(1, 6).Text = "變更金額."
                table0.Texts2D(1, 7).Text = "開支日期"
                table0.Texts2D(1, 8).Text = "開支金額."
                table0.Texts2D(1, 9).Text = "受款人"
                table0.Texts2D(1, 10).Text = "傳票"
                With mydataset2.Tables("bgf030")
                    For i = 2 To PageRow
                        If intR > .Rows.Count - 1 Then
                            table0.Texts2D(i, 3).Text = "    合  計"
                            'table0.Texts2D(i, 4).Text = Format(unuse, "###,###,###,###.#") '直接取用bgf010數據
                            table0.Texts2D(i, 4).Text = Format(TotUnuse1, "###,###,###,###.#")
                            table0.Texts2D(i, 5).Text = Format(TotUnuse2, "###,###,###,###.#")
                            table0.Texts2D(i, 6).Text = Format(TotUnuse3, "###,###,###,###.#")
                            table0.Texts2D(i, 8).Text = Format(TotUsed, "###,###,###,###.#")
                            PageCnt = 1000
                            Exit For
                        End If
                        table0.Texts2D(i, 1).Text = nz(.Rows(intR)("bgno"), "")
                        table0.Texts2D(i, 2).Text = ShortDate(nz(.Rows(intR)("date1"), ""))
                        table0.Texts2D(i, 3).Text = nz(.Rows(intR)("remark"), "")
                        table0.Texts2D(i, 4).Text = Format(nz(.Rows(intR)("amt1"), 0), "###,###,###,###.#")
                        table0.Texts2D(i, 5).Text = Format(nz(.Rows(intR)("amt2"), 0), "###,###,###,###.#")
                        table0.Texts2D(i, 6).Text = Format(nz(.Rows(intR)("amt3"), 0), "###,###,###,###.#")
                        'table0.Texts2D(i, 4).Text = Format(nz(.Rows(intR)("useableamt"), 0), "###,###,###,###.#")
                        table0.Texts2D(i, 7).Text = ShortDate(nz(.Rows(intR)("date3"), ""))
                        table0.Texts2D(i, 8).Text = Format(nz(.Rows(intR)("useamt"), 0), "###,###,###,###.#")
                        table0.Texts2D(i, 9).Text = Mid(nz(.Rows(intR)("subject"), ""), 1, 4)  '7個字
                        table0.Texts2D(i, 10).Text = Format(nz(.Rows(intR)("no_1_no"), 0), "#####")
                        table0.Texts2D(i, 3).Font.Size = 9
                        If nz(.Rows(intR)("useamt"), 0) = 0 Then
                            'TotUnuse += nz(.Rows(intR)("useableamt"), 0)
                            If nz(.Rows(intR)("amt3"), 0) <> 0 Then
                                TotUnuse3 += nz(.Rows(intR)("amt3"), 0)
                            Else
                                If nz(.Rows(intR)("amt2"), 0) <> 0 Then
                                    TotUnuse2 += nz(.Rows(intR)("amt2"), 0)
                                Else
                                    TotUnuse1 += nz(.Rows(intR)("amt1"), 0)
                                End If
                            End If
                        Else
                            TotUsed += nz(.Rows(intR)("useamt"), 0)
                        End If
                        intR += 1
                    Next
                End With

                page.Add(table0)
                doc.AddPage(page)
            Next
        Next

        printer.Document = doc
        printer.IsAutoShowPrintDialog = True '自動顯示印表機設定對話盒
        If rdoPreview.Checked Then
            printer.IsAutoShowPrintPreviewDialog = True
        End If
        printer.Print()
        Me.Close()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class
