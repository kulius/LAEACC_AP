Imports JBC.Printing


Public Class BGP888
    Dim confid, confunit As String, LoadAfter, Dirty As Boolean
    Dim sqlstr As String
    Dim syear As Integer
    Dim mydataset, TempDataSet, userdataset As DataSet
    Dim UserId, UserName, UserUnit As String
    ' Dim xlCells As Excel.Range

    Private Sub BGP888_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Season(TransPara.TransP("userDATE")) = 1 Then
            MsgBox("請以舊年度年底日期辦理")
            Me.Close()
        End If
        syear = GetYear(TransPara.TransP("userDATE"))
        UserId = TransPara.TransP("userid")
        UserUnit = TransPara.TransP("userunit")
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
                Dim nr As DataRow
                nr = userdataset.Tables("user").NewRow()
                nr("userid") = "全部"
                nr("username") = "全部"
                userdataset.Tables("user").Rows.Add(nr)
            End If
        End If
        vxtStartNo.Text = "5"    '起值
        vxtEndNo.Text = "1"      '迄值
    End Sub

    Private Sub BtnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrint.Click
        'Dim xlapp As Excel.Application
        'Dim xlbook As Excel.Workbook
        'Dim xlbooks As Excel.Workbooks
        'Dim xlsheet As Excel.Worksheet
        'Dim xlsheets As Excel.Sheets        '  Dim xlRange As Excel.Range
        'Dim xlRange As Excel.Range
        'Dim xlRange1 As Excel.Range
        'Dim xlRange2 As Excel.Range
        ''Dim xlRange3 As Excel.Range

        'Dim intR As Integer = 0  'control record number
        'Dim intSEQ As Integer = 0
        'Dim intD, i, SumAmt1, SumAmt2, SubAmt1, SubAmt2 As Integer
        'Dim retstr, strRemark, strAccno, strAccname As String
        'Dim bgamt, used, unuse As Decimal
        'Dim UnitName As String = ""
        'Dim tempAccno As String = ""
        'SumAmt1 = 0 : SumAmt2 = 0
        'SubAmt1 = 0 : SubAmt2 = 0

        ''Dim printer = New KPrint
        ''Dim doc As New FPDocument("保留明細表列印")
        ''doc.DefaultPageSettings.PaperKind = Printing.PaperKind.A4
        ''doc.DefaultPageSettings.Landscape = True
        ''doc.DefaultFont = New Font("新細明體", 9) '標楷體
        ''doc.SetDefaultPageMargin(20, 10, 5, 5)   'left top right bottom

        ''Dim PageRow As Integer = 19

        'If Mid(UserUnit, 1, 2) = "05" Or Mid(UserUnit, 1, 2) = "08" Then
        '    UserId = cboUser.SelectedValue
        'End If
        ''找組室名稱
        'If UserId <> "全部" Then
        '    sqlstr = "select * from unittable where unit='" & Mid(TransPara.TransP("userunit"), 1, 3) & "'"
        '    TempDataSet = openmember("", "unit", sqlstr)
        '    If TempDataSet.Tables(0).Rows.Count > 0 Then
        '        UnitName = nz(TempDataSet.Tables(0).Rows(0).Item("unitname"), "")
        '    End If
        'End If
        ''old year = c new year=d 
        ''sqlstr = "SELECT  c.*, d.BGNO AS bgno2, d.ACCNO AS accno2, e.accname,f.staff_no, g.dateopen, g.dateclose, g.reason " & _
        ''         "FROM BGF020 d INNER JOIN " & _
        ''         "(SELECT b.*,a.useamt FROM BGF030 a INNER JOIN BGF020 b ON a.BGNO = b.BGNO " & _
        ''         "WHERE  (a.REMARK LIKE '%保留數轉帳%') and b.accyear=" & syear & ") c " & _
        ''         "ON LEFT(c.REMARK, 6) = LEFT(d.REMARK, 6) AND c.USEAMT = d.AMT1 AND d.ACCYEAR = " & syear + 1 & _
        ''         "left outer join accname e on left(c.accno,9)=e.accno " & _
        ''         "left outer join accname f on c.accno=f.accno " & _
        ''         "left outer join bgf888 g on c.bgno=g.bgno " & _
        ''         "WHERE c.accno between '" & GetAccno(vxtStartNo.text) & "' and '" & Getaccno(vxtEndNo.text) & "' "
        'sqlstr = "SELECT BGF888.*, BGF020.*, BGF030.DATE4, bgf030.useamt, e.accname, f.staff_no " & _
        '         "FROM  BGF888 LEFT OUTER JOIN " & _
        '         "BGF020 ON BGF888.bgno = BGF020.BGNO LEFT OUTER JOIN " & _
        '         "BGF030 ON BGF888.bgno = BGF030.BGNO LEFT OUTER JOIN " & _
        '         "ACCNAME e ON LEFT(BGF020.ACCNO, 9) = e.ACCNO LEFT OUTER JOIN " & _
        '         "ACCNAME f ON BGF020.ACCNO = f.ACCNO " & _
        '         "WHERE bgf020.accno between '" & GetAccno(vxtStartNo.Text) & "' and '" & GetAccno(vxtEndNo.Text) & "' " & _
        '         " and YEAR(BGF030.DATE4) = " & syear + 1911

        'If UserId <> "全部" Then
        '    sqlstr += " and f.staff_no='" & UserId & "' "
        'End If
        'sqlstr += " ORDER BY left(bgf020.accno,9), bgf888.newbgno"  '"ORDER BY c.accno, c.remark, D.bgno"
        'mydataset = openmember("", "BGF020", sqlstr)


        'If mydataset.Tables(0).Rows.Count <= 0 Then Exit Sub '無資料
        'Try
        '    Dim tt1, tt2 As String
        '    Try
        '        tt1 = "c:\App\acc\報表樣本\BGP888預算保留表.xls"  '"c:\App\acc\報表樣本\BGP888預算保留表.xls"
        '        tt2 = "c:\App\acc\報表\預算保留表.xls"
        '        If Not File.Exists(tt1) Then
        '            MsgBox("找不到報表樣本" & tt1 & "，請洽資訊人員" & vbNewLine & tt1)
        '            Exit Sub
        '        End If
        '        FileCopy(tt1, tt2)    'copy tt1 to tt2
        '        xlapp = CreateObject("excel.application")
        '        xlbooks = xlapp.Workbooks
        '        xlbook = xlbooks.Open(tt2) '開啟tt2
        '    Catch ex As Exception
        '        MsgBox(ex.Message)
        '        'MsgBox("報表copy " & tt1 & "  to  " & tt2 & "錯誤，是否" & tt2 & " 使用中,請先關閉!")
        '        Exit Sub
        '    End Try
        '    xlsheets = xlbook.Worksheets
        '    xlsheet = xlsheets(1)
        '    NAR(xlCells)
        '    xlCells = xlsheet.Cells

        '    '公司名稱
        '    If TransPara.TransP("UnitTitle") <> "" Then
        '        xlRange = xlsheet.Range("A1")
        '        xlRange.Value = TransPara.TransP("UnitTitle") & syear & "年度預算保留申請統計表"
        '        NAR(xlRange)
        '    End If
        '    xlRange = xlsheet.Range("A2")
        '    xlRange.Value = "填報日期:" & ShortDate(TransPara.TransP("userdate"))
        '    NAR(xlRange)

        '    With mydataset.Tables("bgf020")
        '        i = 3    '自第4行開始放
        '        For intR = 0 To .Rows.Count - 1
        '            i += 1
        '            '拷貝目前這列到下一列,使得每列都有相同的格式設定
        '            xlRange1 = xlsheet.Range("A" & i & ":L" & i)
        '            xlRange2 = xlsheet.Range("A" & i + 1 & ":L" & i + 1)
        '            xlRange1.Copy(xlRange2)
        '            NAR(xlRange1)
        '            NAR(xlRange2)
        '            If intR = 0 Then tempAccno = Mid(.Rows(intR)("accno"), 1, 9)
        '            If tempAccno <> Mid(.Rows(intR)("accno"), 1, 9) Then
        '                xlRange = xlCells(i, 3)
        '                xlRange.Value = "    小  計"
        '                NAR(xlRange)
        '                xlRange = xlCells(i, 4)
        '                xlRange.Value = Format(SubAmt1, "###,###,###,###")
        '                NAR(xlRange)
        '                xlRange = xlCells(i, 5)
        '                xlRange.Value = Format(SubAmt2, "###,###,###,###")
        '                NAR(xlRange)
        '                SubAmt1 = 0 : SubAmt2 = 0
        '                tempAccno = Mid(.Rows(intR)("accno"), 1, 9)
        '                i += 1
        '                xlRange1 = xlsheet.Range("A" & i & ":L" & i)
        '                xlRange2 = xlsheet.Range("A" & i + 1 & ":L" & i + 1)
        '                xlRange1.Copy(xlRange2)
        '                NAR(xlRange1)
        '                NAR(xlRange2)
        '            End If
        '            intSEQ += 1   '序號
        '            xlRange = xlCells(i, 1)
        '            xlRange.Value = Format(intSEQ, "####")
        '            NAR(xlRange)
        '            xlRange = xlCells(i, 2)
        '            xlRange.Value = FormatAccno(Mid(.Rows(intR)("accno"), 1, 9)) & vbNewLine & nz(.Rows(intR)("accname"), "")
        '            NAR(xlRange)
        '            xlRange = xlCells(i, 3)
        '            xlRange.Value = nz(.Rows(intR)("remark"), "")
        '            NAR(xlRange)
        '            xlRange = xlCells(i, 4)
        '            xlRange.Value = Format(nz(.Rows(intR)("amt1"), 0), "###,###,###,###")
        '            NAR(xlRange)
        '            xlRange = xlCells(i, 5)
        '            xlRange.Value = Format(nz(.Rows(intR)("useamt"), 0), "###,###,###,###")
        '            NAR(xlRange)
        '            xlRange = xlCells(i, 6)
        '            xlRange.Value = nz(.Rows(intR)("subject"), "")
        '            NAR(xlRange)
        '            xlRange = xlCells(i, 7)
        '            xlRange.Value = nz(.Rows(intR)("dateOpen"), "")
        '            NAR(xlRange)
        '            xlRange = xlCells(i, 8)
        '            xlRange.Value = nz(.Rows(intR)("dateClose"), "")
        '            NAR(xlRange)
        '            xlRange = xlCells(i, 9)
        '            xlRange.Value = UnitName
        '            '找組室名稱
        '            If UserId = "全部" Then
        '                sqlstr = "select b.* from (select * from accname where accno='" & .Rows(intR)("accno") & "') a " & _
        '                               " left outer join unittable b on a.unit=b.unit"
        '                TempDataSet = openmember("", "unit", sqlstr)
        '                If TempDataSet.Tables(0).Rows.Count > 0 Then
        '                    xlRange.Value = nz(TempDataSet.Tables(0).Rows(0).Item("unitname"), "")
        '                End If
        '            End If
        '            NAR(xlRange)
        '            xlRange = xlCells(i, 10)
        '            xlRange.Value = nz(.Rows(intR)("reason"), "")
        '            NAR(xlRange)
        '            xlRange = xlCells(i, 11)
        '            xlRange.Value = nz(.Rows(intR)("bgno"), "")
        '            NAR(xlRange)
        '            xlRange = xlCells(i, 12)
        '            xlRange.Value = nz(.Rows(intR)("newbgno"), "")
        '            NAR(xlRange)
        '            SumAmt1 += nz(.Rows(intR)("amt1"), 0)
        '            SumAmt2 += nz(.Rows(intR)("useamt"), 0)
        '            SubAmt1 += nz(.Rows(intR)("amt1"), 0)
        '            SubAmt2 += nz(.Rows(intR)("useamt"), 0)
        '        Next
        '        i += 1
        '        xlRange = xlCells(i, 3)
        '        xlRange.Value = "    合  計"
        '        NAR(xlRange)
        '        xlRange = xlCells(i, 4)
        '        xlRange.Value = Format(SumAmt1, "###,###,###,###")
        '        NAR(xlRange)
        '        xlRange = xlCells(i, 5)
        '        xlRange.Value = Format(SumAmt2, "###,###,###,###")
        '        NAR(xlRange)
        '    End With

        '    '儲存檔案
        '    xlbook.Save()
        '    If rdoPrint.Checked Then
        '        'xlapp.Visible = True
        '        'xlbook.PrintPreview() '不可用預覽列印,否則當使用者關閉excel視窗之後就會導致finally區段關閉excel.exe時出現error
        '        xlbook.PrintOut()  '直接列印
        '    End If

        '    If MessageBox.Show("列印完畢,已存入 " & tt2 & vbNewLine & "是否要幫您開啟此份報表?", "", MsgBoxStyle.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) = MsgBoxResult.Yes Then
        '        '不可用 Process.Start(tt2) 否則會造成用同一個excel process開啟報表,導致finally區段關閉excel.exe時會出現error
        '        Process.Start("excel.exe", tt2)
        '    End If

        '    Me.Close()

        'Finally
        '    '釋放各物件所佔用的記憶體,要按照以下順序
        '    NAR(xlCells)
        '    NAR(xlRange)
        '    NAR(xlRange1)
        '    NAR(xlRange2)

        '    NAR(xlsheet)
        '    NAR(xlsheets)
        '    If Not xlbook Is Nothing Then xlbook.Close(False)
        '    NAR(xlbook)
        '    NAR(xlbooks)
        '    If Not xlapp Is Nothing Then xlapp.Quit()
        '    NAR(xlapp)
        '    GC.Collect()
        'End Try


        ' ''to be continue 95/1/6
        ''Dim PageNo As Integer = 0
        ''For PageCnt As Integer = 1 To 999    '頁次
        ''    Dim page As New FPPage
        ''    Dim text1 As New FPText(TransPara.TransP("unitTitle") & syear & "年度預算保留申請統計表", 80, 0) '& FormatAccno(strAccno) & " " & strAccname, 0, 0)
        ''    text1.Font.Size = 12
        ''    page.AddText(text1)
        ''    PageNo += 1
        ''    Dim text2 As New FPText("填報日期:" & ShortDate(TransPara.TransP("userdate")) & _
        ''                            "  頁次:" & PageNo, 210, 5)
        ''    page.AddText(text2)
        ''    Dim table0 As New FPTable(0, 10, 260, 9 * PageRow, PageRow, 12)
        ''    'table0.Font.Name = "標楷體"
        ''    table0.Font.Size = 9
        ''    table0.SetLineColor(Color.DarkBlue)
        ''    table0.OutlineThicken(4)
        ''    table0.ColumnStyles(1).Width = 8
        ''    table0.ColumnStyles(2).Width = 19
        ''    table0.ColumnStyles(3).Width = 78
        ''    table0.ColumnStyles(4).Width = 18
        ''    table0.ColumnStyles(5).Width = 18
        ''    table0.ColumnStyles(6).Width = 29
        ''    table0.ColumnStyles(7).Width = 15
        ''    table0.ColumnStyles(8).Width = 15
        ''    table0.ColumnStyles(9).Width = 15
        ''    table0.ColumnStyles(10).Width = 15
        ''    table0.ColumnStyles(11).Width = 15
        ''    table0.ColumnStyles(12).Width = 15
        ''    table0.ColumnStyles(2).HAlignment = StringAlignment.Near '整欄左靠
        ''    table0.ColumnStyles(3).HAlignment = StringAlignment.Near '整欄左靠
        ''    table0.ColumnStyles(4).HAlignment = StringAlignment.Far '整欄左靠
        ''    table0.ColumnStyles(5).HAlignment = StringAlignment.Far  '整欄右靠
        ''    table0.ColumnStyles(6).HAlignment = StringAlignment.Near  '整欄右靠
        ''    table0.ColumnStyles(11).HAlignment = StringAlignment.Near '整欄左靠
        ''    table0.ColumnStyles(12).HAlignment = StringAlignment.Near '整欄左靠
        ''    table0.Texts2D(1, 1).Text = "序號"
        ''    table0.Texts2D(1, 2).Text = "預算科目"
        ''    table0.Texts2D(1, 3).Text = "工程名稱"
        ''    table0.Texts2D(1, 4).Text = "原預算金額"
        ''    table0.Texts2D(1, 5).Text = "保留金額"
        ''    table0.Texts2D(1, 6).Text = "債權人"
        ''    table0.Texts2D(1, 7).Text = "權責發生日"
        ''    table0.Texts2D(1, 8).Text = "預計完成日"
        ''    table0.Texts2D(1, 9).Text = "承辦單位"
        ''    table0.Texts2D(1, 10).Text = "保留原因"
        ''    table0.Texts2D(1, 11).Text = "原請購號"
        ''    table0.Texts2D(1, 12).Text = "轉請購號"
        ''    With mydataset.Tables("bgf020")
        ''        For i = 2 To PageRow
        ''            If intR > .Rows.Count - 1 Then
        ''                table0.Texts2D(i, 3).Text = "    合  計"
        ''                table0.Texts2D(i, 4).Text = Format(SumAmt1, "###,###,###,###")
        ''                table0.Texts2D(i, 5).Text = Format(SumAmt2, "###,###,###,###")
        ''                PageCnt = 1000
        ''                Exit For
        ''            End If
        ''            If intR = 0 Then tempAccno = Mid(.Rows(intR)("accno"), 1, 9)
        ''            If tempAccno <> Mid(.Rows(intR)("accno"), 1, 9) Then
        ''                table0.Texts2D(i, 3).Text = "    小  計"
        ''                table0.Texts2D(i, 4).Text = Format(SubAmt1, "###,###,###,###")
        ''                table0.Texts2D(i, 5).Text = Format(SubAmt2, "###,###,###,###")
        ''                SubAmt1 = 0 : SubAmt2 = 0
        ''                tempAccno = Mid(.Rows(intR)("accno"), 1, 9)
        ''                i += 1
        ''                If i > PageRow Then Exit For '跳頁
        ''            End If
        ''            intSEQ += 1
        ''            table0.Texts2D(i, 1).Text = Format(intSEQ, "####")
        ''            table0.Texts2D(i, 2).Text = FormatAccno(Mid(.Rows(intR)("accno"), 1, 9))
        ''            table0.Texts2D(i, 3).Text = nz(.Rows(intR)("remark"), "")  'ShortDate(nz(.Rows(intR)("date1"), ""))
        ''            table0.Texts2D(i, 4).Text = Format(nz(.Rows(intR)("amt1"), 0), "###,###,###,###")
        ''            table0.Texts2D(i, 5).Text = Format(nz(.Rows(intR)("useamt"), 0), "###,###,###,###")
        ''            table0.Texts2D(i, 6).Text = nz(.Rows(intR)("subject"), "")
        ''            table0.Texts2D(i, 7).Text = nz(.Rows(intR)("dateOpen"), "")
        ''            table0.Texts2D(i, 8).Text = nz(.Rows(intR)("dateClose"), "")
        ''            table0.Texts2D(i, 9).Text = UnitName
        ''            table0.Texts2D(i, 10).Text = nz(.Rows(intR)("reason"), "")
        ''            table0.Texts2D(i, 11).Text = nz(.Rows(intR)("bgno"), "")
        ''            table0.Texts2D(i, 12).Text = nz(.Rows(intR)("bgno2"), "")
        ''            ''權責發生日等資料由工程登記找
        ''            'If UserId = "全部" Then
        ''            '    sqlstr = "select * from enf010 where eyear=" & syear & " and left(engname,6)='" & _
        ''            '            Mid(nz(.Rows(intR)("remark"), ""), 1, 6) & "'"
        ''            'Else
        ''            '    sqlstr = "select * from enf010 where eyear=" & syear & " and userid='" & UserId & _
        ''            '           "' and left(engname,6)='" & Mid(nz(.Rows(intR)("remark"), ""), 1, 6) & "'"
        ''            'End If
        ''            'TempDataSet =  openmember("", "enf010", sqlstr)
        ''            'If TempDataSet.Tables(0).Rows.Count > 0 Then
        ''            '    With TempDataSet.Tables(0).Rows(0)
        ''            '        If Not IsDBNull(.Item("open_date")) Then   '權責發生日
        ''            '            table0.Texts2D(i, 7).Text = ShortDate(.Item("open_date"))
        ''            '        End If
        ''            '        If IsDBNull(.Item("end_date")) Then  '預計完成日期
        ''            '            table0.Texts2D(i, 10).Text = "施工中"
        ''            '        Else
        ''            '            table0.Texts2D(i, 8).Text = ShortDate(.Item("end_date"))
        ''            '            If Year(.Item("end_date")) <= syear Then  '預計完成日期在本年度者判為驗收中
        ''            '                table0.Texts2D(i, 10).Text = "驗收中"
        ''            '            Else
        ''            '                table0.Texts2D(i, 10).Text = "施工中"
        ''            '            End If
        ''            '        End If
        ''            '    End With
        ''            'End If
        ''            '找組室名稱
        ''            If UserId = "全部" Then
        ''                sqlstr = "select b.* from (select * from accname where accno='" & .Rows(intR)("accno") & "') a " & _
        ''                               " left outer join unittable b on a.unit=b.unit"
        ''                TempDataSet =  openmember("", "unit", sqlstr)
        ''                If TempDataSet.Tables(0).Rows.Count > 0 Then
        ''                    UnitName = nz(TempDataSet.Tables(0).Rows(0).Item("unitname"), "")
        ''                    table0.Texts2D(i, 9).Text = UnitName
        ''                End If
        ''            End If
        ''            SumAmt1 += nz(.Rows(intR)("amt1"), 0)
        ''            SumAmt2 += nz(.Rows(intR)("useamt"), 0)
        ''            SubAmt1 += nz(.Rows(intR)("amt1"), 0)
        ''            SubAmt2 += nz(.Rows(intR)("useamt"), 0)
        ''            intR += 1
        ''        Next
        ''    End With

        ''    page.Add(table0)
        ''    doc.AddPage(page)
        ''Next


        ''printer.Document = doc
        ''printer.IsAutoShowPrintDialog = True '自動顯示印表機設定對話盒
        ''If rdoPreview.Checked Then
        ''    printer.IsAutoShowPrintPreviewDialog = True
        ''End If
        ''printer.Print()
        ''Me.Close()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class
