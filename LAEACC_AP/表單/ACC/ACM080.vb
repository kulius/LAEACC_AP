Imports JBC.Printing
Imports System.IO
Imports Microsoft.Office.Interop
Public Class ACM080
    Dim confid, confunit As String, LoadAfter, Dirty As Boolean
    Dim SYear As Integer
    Dim myds As DataSet

    Private Sub ACM080_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dtpDateS.Value = TransPara.TransP("LastDay")
    End Sub

    Private Sub LoadGridFunc()
        Dim sqlstr, retstr, tempStr, sdate As String
        Dim mm As String = Format(Month(dtpDateS.Value), "00")  '本月
        Dim up As String = Format(Month(dtpDateS.Value) - 1, "00") '上月
        SYear = GetYear(dtpDateS.Value)
        sdate = SYear + 1911 & "/" & Val(mm) & "/1"
        '先清空acm010 
        sqlstr = "delete from acm070"
        retstr = runsql(mastconn, sqlstr)

        '將acf020材料科目置入acm070(kind='1',"2","3","4")    'substring(a.accno,5,1)='2'是備抵折舊
        sqlstr = "INSERT INTO ACM070 " & _
        "SELECT d.ACCNO, c.ACCNAME AS accname, e.KIND, '購入' AS remark, e.UNIT, d.AMT1, " & _
        " d.AMT2, 0 AS amt3, 0 AS amt4  FROM " & _
        "(SELECT A.ACCNO, SUM(A.MAT_QTY) AS AMT1, SUM(A.AMT) AS AMT2 FROM ACF020 A LEFT OUTER JOIN " & _
        " ACF010 b ON A.KIND = b.KIND AND A.NO_1_NO = b.NO_1_NO and a.seq=b.seq and b.item='1' AND A.ACCYEAR = b.ACCYEAR " & _
        " WHERE SUBSTRING(A.ACCNO, 1, 2) = '13' and SUBSTRING(A.ACCNO, 1, 5) <> '13701' and substring(a.accno,5,1)<>'2' AND b.DATE_2 >= '" & sdate & "'" & _
        " AND b.DATE_2 <= '" & FullDate(dtpDateS.Value) & "' AND A.KIND = '2'" & _
        " GROUP BY A.ACCNO) d LEFT OUTER JOIN ACF100 e ON d.ACCNO = e.ACCNO AND e.ACCYEAR = " & SYear & _
        " LEFT OUTER JOIN ACCNAME c ON rtrim(left(d.ACCNO,7)) = c.ACCNO"
        retstr = runsql(mastconn, sqlstr)

        sqlstr = "INSERT INTO ACM070 " & _
        "SELECT d.ACCNO, c.ACCNAME AS accname, e.KIND, '科目調整' AS remark, e.UNIT, d.AMT1, " & _
        " d.AMT2, 0 AS amt3, 0 AS amt4  FROM " & _
        "(SELECT A.ACCNO, SUM(A.MAT_QTY) AS AMT1, SUM(A.AMT) AS AMT2 FROM ACF020 A LEFT OUTER JOIN " & _
        " ACF010 b ON A.KIND = b.KIND AND A.NO_1_NO = b.NO_1_NO and a.seq=b.seq and b.item='1' AND A.ACCYEAR = b.ACCYEAR " & _
        " WHERE SUBSTRING(A.ACCNO, 1, 2) = '13' and SUBSTRING(A.ACCNO, 1, 5) <> '13701' and substring(a.accno,5,1)<>'2' AND b.DATE_2 >= '" & sdate & "'" & _
        " AND b.DATE_2 <= '" & FullDate(dtpDateS.Value) & "' AND A.KIND = '3'" & _
        " GROUP BY A.ACCNO) d LEFT OUTER JOIN ACF100 e ON d.ACCNO = e.ACCNO AND e.ACCYEAR = " & SYear & _
        " LEFT OUTER JOIN ACCNAME c ON rtrim(left(d.ACCNO,7)) = c.ACCNO"
        retstr = runsql(mastconn, sqlstr)

        sqlstr = "INSERT INTO ACM070 " & _
        "SELECT d.ACCNO, c.ACCNAME AS accname, e.KIND, '報廢' AS remark, e.UNIT, 0 AS amt1, 0 AS amt2, d.AMT3, " & _
        " d.AMT4  FROM " & _
        "(SELECT A.ACCNO, SUM(A.MAT_QTY) AS AMT3, SUM(A.AMT) AS AMT4 FROM ACF020 A LEFT OUTER JOIN " & _
        " ACF010 b ON A.KIND = b.KIND AND A.NO_1_NO = b.NO_1_NO and a.seq=b.seq and b.item='1' AND A.ACCYEAR = b.ACCYEAR " & _
        " WHERE SUBSTRING(A.ACCNO, 1, 2) = '13' and SUBSTRING(A.ACCNO, 1, 5) <> '13701' and substring(a.accno,5,1)<>'2' AND b.DATE_2 >= '" & sdate & "'" & _
        " AND b.DATE_2 <= '" & FullDate(dtpDateS.Value) & "' AND A.KIND = '4'" & _
        " GROUP BY A.ACCNO) d LEFT OUTER JOIN ACF100 e ON d.ACCNO = e.ACCNO AND e.ACCYEAR = " & SYear & _
         " LEFT OUTER JOIN ACCNAME c ON rtrim(left(d.ACCNO,7)) = c.ACCNO"
        retstr = runsql(mastconn, sqlstr)

        sqlstr = "INSERT INTO ACM070 " & _
        "SELECT d.ACCNO, c.ACCNAME AS accname, e.KIND, '變賣' AS remark, e.UNIT, 0 AS amt1, 0 AS amt2, d.AMT3, " & _
        " d.AMT4  FROM " & _
        "(SELECT A.ACCNO, SUM(A.MAT_QTY) AS AMT3, SUM(A.AMT) AS AMT4 FROM ACF020 A LEFT OUTER JOIN " & _
        " ACF010 b ON A.KIND = b.KIND AND A.NO_1_NO = b.NO_1_NO and a.seq=b.seq and b.item='1' AND A.ACCYEAR = b.ACCYEAR " & _
        " WHERE SUBSTRING(A.ACCNO, 1, 2) = '13' and SUBSTRING(A.ACCNO, 1, 5) <> '13701' and substring(a.accno,5,1)<>'2' AND b.DATE_2 >= '" & sdate & "'" & _
        " AND b.DATE_2 <= '" & FullDate(dtpDateS.Value) & "' AND A.KIND = '1'" & _
        " GROUP BY A.ACCNO) d LEFT OUTER JOIN ACF100 e ON d.ACCNO = e.ACCNO AND e.ACCYEAR = " & SYear & _
        " LEFT OUTER JOIN ACCNAME c ON rtrim(left(d.ACCNO,7)) = c.ACCNO"
        retstr = runsql(mastconn, sqlstr)


        '合計
        sqlstr = "INSERT INTO ACM070 " & _
                 "SELECT '9' AS accno,'合        計' as accname,'  ' as kind, '  ' as remark, " & _
                 "'  ' as unit, SUM(amt1) AS amt1, SUM(amt2) AS amt2, SUM(amt3) AS amt3, SUM(amt4) AS amt4 " & _
                 " from acm070"
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then MsgBox("合計 error " & sqlstr)
        '丟入dataset 
        'sqlstr = "SELECT a.*, b.unit FROM acm070 a left outer join acf100 b on a.accno=b.accno and b.accyear=" & _
        '          SYear & " order by a.accno"
        sqlstr = "SELECT * FROM acm070 order by accno"
        myds = openmember("", "acm010", sqlstr)
    End Sub

    Private Sub BtnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrint.Click
        If rdbExcelYes.Checked Then
            Call Print_excel()
        Else
            TransPara.TransP("LastDay") = dtpDateS.Value
            SYear = GetYear(dtpDateS.Value)
            Call LoadGridFunc()
            If myds.Tables("acm010").Rows.Count <= 1 Then
                MsgBox("無固定資產增減資料")
                Exit Sub
            End If

            Dim intR As Integer = 0  'control record number
            Dim strAccno As String
            Dim PageRow As Integer = 14  '每頁印14行
            Dim printer = New KPrint
            Dim document As New FPDocument("固定資產增減表")
            document.SetDefaultPageMargin(25, 10, 10, 10)   'left,top,right,bottom
            document.DefaultPageSettings.Landscape = True    '橫印
            document.DefaultFont = New Font("新細明體", 11) '標楷體
            For PageCnt As Integer = 1 To 99    '頁次
                Dim page As New FPPage  '新增列印頁面
                Dim textUnit As New FPText(TransPara.TransP("UnitTitle"), 90, 0)  '文字物件,座標90,0 (x,y)
                textUnit.HAlignment = FPAlignment.Center
                textUnit.Font.Size = 14   '改變文字大小為14點
                page.Add(textUnit)
                Dim textTitle As New FPText("固 定 資 產 增 減 表", 90, 6)
                textTitle.HAlignment = FPAlignment.Center
                textTitle.Font.Size = 14
                page.Add(textTitle)
                Dim textday As New FPText("中華民國" & SYear & "年" & Month(dtpDateS.Value) & "月1日起至" & _
                    SYear & "年" & Month(dtpDateS.Value) & "月" & Microsoft.VisualBasic.DateAndTime.Day(dtpDateS.Value) & "日止", 90, 12)
                textday.HAlignment = FPAlignment.Center
                textday.Font.Size = 11
                page.Add(textday)
                Dim textPage As New FPText("第" & PageCnt & "頁", 235, 12)
                page.Add(textPage)

                '新增表格物件
                '每欄ㄉ寬度和每列的高度,預設會使用自動平均後的值
                'FPTable 和 FPGrid 這兩個物件的差異在 FPTable內含 FPCell物件,可以設定跨列和跨欄,但FPGrid則無
                Dim grid As New FPTable(0, 17, 250, 162, PageRow, 15)   'x,y,w,h,row,col
                grid.Font.Size = 9
                grid.ColumnStyles(1).HAlignment = StringAlignment.Near
                grid.ColumnStyles(2).HAlignment = StringAlignment.Near
                grid.ColumnStyles(3).HAlignment = StringAlignment.Near
                grid.ColumnStyles(4).HAlignment = StringAlignment.Far
                'grid.ColumnStyles(5).HAlignment = StringAlignment.Far
                grid.ColumnStyles(6).HAlignment = StringAlignment.Far
                grid.ColumnStyles(7).HAlignment = StringAlignment.Far
                grid.ColumnStyles(9).HAlignment = StringAlignment.Far
                grid.ColumnStyles(11).HAlignment = StringAlignment.Far

                grid.SetLineColor(Color.Blue)   'table line color 
                grid.ColumnStyles(1).Width = 34
                grid.ColumnStyles(2).Width = 20
                grid.ColumnStyles(3).Width = 12
                grid.ColumnStyles(4).Width = 20   '數    量
                grid.ColumnStyles(5).Width = 10   '單價
                grid.ColumnStyles(6).Width = 20   '金      額
                grid.ColumnStyles(7).Width = 16
                grid.ColumnStyles(8).Width = 12  '規格
                grid.ColumnStyles(9).Width = 20
                grid.ColumnStyles(10).Width = 10
                grid.ColumnStyles(11).Width = 20
                grid.ColumnStyles(12).Width = 16
                grid.ColumnStyles(13).Width = 16
                grid.ColumnStyles(14).Width = 16
                grid.ColumnStyles(15).Width = 8
                grid.Cells2D(1, 1).RowSpan = 2 '1,2 row join
                grid.Cells2D(1, 2).RowSpan = 2
                grid.Cells2D(1, 3).RowSpan = 2
                grid.Cells2D(1, 15).RowSpan = 2
                grid.Cells2D(1, 4).ColSpan = 5 '5,6,7,8,9 隱藏
                grid.Cells2D(1, 9).ColSpan = 6
                grid.Texts2D(1, 1).Text = "　　科 目 名 稱"
                grid.Texts2D(1, 2).Text = " 摘   要"
                grid.Texts2D(1, 3).Text = "單位"
                grid.Texts2D(1, 4).Text = "   本              月              增              加             ."
                grid.Texts2D(1, 9).Text = "   本                 月                 減                 少           ."
                grid.Texts2D(2, 4).Text = "數    量"
                grid.Texts2D(2, 5).Text = "單價"
                grid.Texts2D(2, 6).Text = "金      額"
                grid.Texts2D(2, 7).Text = "使用年限"
                grid.Texts2D(2, 8).Text = "規    格"
                grid.Texts2D(2, 9).Text = "數    量"
                grid.Texts2D(2, 10).Text = "單價"
                grid.Texts2D(2, 11).Text = "金      額"
                grid.Texts2D(2, 12).Text = "購置日期"
                grid.Texts2D(2, 13).Text = "已提折舊"
                grid.Texts2D(2, 14).Text = "資產淨值"
                grid.Texts2D(1, 15).Text = "備註"

                With myds.Tables("acm010")
                    For i As Integer = 3 To PageRow
                        If intR > .Rows.Count - 1 Then
                            PageCnt = 999    'EXIT FOR THE PAGE 
                            Exit For
                        End If
                        strAccno = Mid(.Rows(intR).Item("accno"), 1, 7)
                        grid.Texts2D(i, 1).Text = FormatAccno(strAccno) & vbCrLf & nz(.Rows(intR).Item("accname"), "")
                        grid.Texts2D(i, 2).Text = nz(.Rows(intR).Item("remark"), "")  '摘要
                        grid.Texts2D(i, 3).Text = nz(.Rows(intR).Item("unit"), "")    '單位
                        grid.Texts2D(i, 4).Text = Format(nz(.Rows(intR).Item("amt1"), 0), "###,###,###.######")  '數量
                        grid.Texts2D(i, 6).Text = Format(nz(.Rows(intR).Item("amt2"), 0), "###,###,###.##")  '金額
                        grid.Texts2D(i, 8).Text = nz(.Rows(intR).Item("kind"), "")                  '規格
                        grid.Texts2D(i, 9).Text = Format(nz(.Rows(intR).Item("amt3"), 0), "###,###,###.######")  '數量
                        grid.Texts2D(i, 11).Text = Format(nz(.Rows(intR).Item("amt4"), 0), "###,###,###.##") '金額
                        intR += 1
                    Next
                End With
                page.Add(grid)  '加入要列印的表格到列印頁面中
                document.AddPage(page)  '加入要列印的頁面到列印文件中
            Next
            printer.Document = document   '將要列印的文件送到印表機
            printer.PrintMode = PrintMode.NormalPrint
            printer.IsAutoShowPrintDialog = True '自動顯示印表機設定對話盒
            If rdoPreview.Checked = True Then printer.IsAutoShowPrintPreviewDialog = True
            printer.Print()
            Me.Close()
        End If
    End Sub

    Private Sub Print_excel()
        TransPara.TransP("LastDay") = dtpDateS.Value
        SYear = GetYear(dtpDateS.Value)
        Call LoadGridFunc()
        If myds.Tables("acm010").Rows.Count <= 1 Then
            MsgBox("無固定資產增減資料")
            Exit Sub
        End If

        Dim xlapp As Excel.Application
        Dim xlbook As Excel.Workbook
        Dim xlbooks As Excel.Workbooks
        Dim xlsheet As Excel.Worksheet
        Dim xlsheets As Excel.Sheets
        Dim xlRange As Excel.Range
        Dim xlRange1 As Excel.Range
        Dim xlRange2 As Excel.Range
        Dim xlCells As Excel.Range
        Try
            Dim intR As Integer = 0  'control record number
            Dim i As Integer = 0 'control excel row 
            Dim strAccno As String
            Dim strS, strT As String
            Dim TOTyy, TOTup As Decimal
            Dim decAmt5, decAmt6 As Decimal
            xlapp = CreateObject("excel.application")
            xlapp.DisplayAlerts = False

            Dim tt1, tt2 As String
            Try
                tt1 = "c:\App\ACC\報表樣本\ACM080.xls"
                tt2 = "c:\App\ACC\報表\ACM080.xls"
                If Not File.Exists(tt1) Then
                    AppReport_Copy("acc", "ACM080.xls", tt1)
                    'MsgBox("找不到報表樣本，請洽資訊人員" & vbNewLine & tt1)
                    'Exit Sub
                End If
                FileCopy(tt1, tt2)    'copy tt1 to tt2

                xlapp = CreateObject("Excel.Application")
                xlapp.DisplayAlerts = False  '隱藏所有警告或確認訊息視窗,否則某些指令會失敗,譬如刪除工作表
                xlbooks = xlapp.Workbooks
                xlbook = xlbooks.Open(tt2)   '開啟tt2

            Catch ex As Exception
                MsgBox("報表copy " & tt1 & "  to  " & tt2 & "產生錯誤，請洽程式設計人員!")
                Exit Sub
            End Try

            xlsheets = xlbook.Worksheets
            xlsheet = xlsheets(1)
            NAR(xlCells)
            xlCells = xlsheet.Cells

            xlRange = xlsheet.Range("A1")
            xlRange.Value = TransPara.TransP("UnitTitle")
            NAR(xlRange)
            xlRange = xlsheet.Range("A3")
            xlRange.Value = "中華民國" & SYear & "年" & Month(dtpDateS.Value) & "月1日起至" & _
                    SYear & "年" & Month(dtpDateS.Value) & "月" & Microsoft.VisualBasic.DateAndTime.Day(dtpDateS.Value) & "日止"
            NAR(xlRange)

            i = 5    '自第6行開始放
            With myds.Tables("acm010")
                For intR = 0 To .Rows.Count - 1
                    i += 1   '自第6行開始放
                    strAccno = .Rows(intR).Item("accno")
                    '拷貝目前這列到下一列,使得每列都有相同的格式設定
                    xlRange1 = xlsheet.Range("A" & i & ":K" & i)
                    xlRange2 = xlsheet.Range("A" & i + 1 & ":K" & i + 1)
                    xlRange1.Copy(xlRange2)
                    NAR(xlRange1)
                    NAR(xlRange2)

                    'xlsheet.Cells(i, 1) = FormatAccno(strAccno) & vbCrLf & nz(.Rows(intR).Item("accname"), "")
                    'xlsheet.Cells(i, 2) = nz(.Rows(intR).Item("remark"), "")  '摘要
                    'xlsheet.Cells(i, 3) = nz(.Rows(intR).Item("unit"), "")    '單位
                    'xlsheet.Cells(i, 4) = FormatNumber(nz(.Rows(intR).Item("amt1"), 0), 2)  '數量
                    'xlsheet.Cells(i, 6) = FormatNumber(nz(.Rows(intR).Item("amt2"), 0), 2)  '金額
                    'xlsheet.Cells(i, 8) = nz(.Rows(intR).Item("kind"), "")                  '規格
                    'xlsheet.Cells(i, 9) = FormatNumber(nz(.Rows(intR).Item("amt3"), 0), 2)  '數量
                    'xlsheet.Cells(i, 11) = FormatNumber(nz(.Rows(intR).Item("amt4"), 0), 2) '金額

                    xlRange = xlCells(i, 1)
                    xlRange.Value = FormatAccno(strAccno) & vbCrLf & nz(.Rows(intR).Item("accname"), "")
                    NAR(xlRange)
                    xlRange = xlCells(i, 2)
                    xlRange.Value = nz(.Rows(intR).Item("remark"), "")  '摘要
                    NAR(xlRange)
                    xlRange = xlCells(i, 3)
                    xlRange.Value = nz(.Rows(intR).Item("unit"), "")    '單位
                    NAR(xlRange)
                    xlRange = xlCells(i, 4)
                    xlRange.Value = FormatNumber(nz(.Rows(intR).Item("amt1"), 0), 2)  '數量
                    NAR(xlRange)
                    xlRange = xlCells(i, 6)
                    xlRange.Value = FormatNumber(nz(.Rows(intR).Item("amt2"), 0), 2)  '金額
                    NAR(xlRange)
                    xlRange = xlCells(i, 8)
                    xlRange.Value = nz(.Rows(intR).Item("kind"), "")                  '規格
                    NAR(xlRange)
                    xlRange = xlCells(i, 9)
                    xlRange.Value = FormatNumber(nz(.Rows(intR).Item("amt3"), 0), 2)  '數量
                    NAR(xlRange)
                    xlRange = xlCells(i, 11)
                    xlRange.Value = FormatNumber(nz(.Rows(intR).Item("amt4"), 0), 2) '金額
                    NAR(xlRange)
                    intR += 1
                Next
            End With

            xlbook.Save()

            Dim result3 As DialogResult = MessageBox.Show("列印完畢,已存入 " & tt2 & vbNewLine & "是否要幫您開啟此份報表?", _
                "", _
                MessageBoxButtons.YesNoCancel, _
                MessageBoxIcon.Question, _
                MessageBoxDefaultButton.Button2)
            If result3 = DialogResult.Yes Then

                'If MessageBox.Show("列印完畢,已存入 " & tt2 & vbNewLine & "是否要幫您開啟此份報表?", "", MsgBoxStyle.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) = MsgBoxResult.Yes Then
                Process.Start("excel.exe", tt2)
            End If

            'If rdoPrint.Checked Then
            '    xlbook.PrintOut()
            'End If

            Me.Close()

            'xlsheet = Nothing
            'xlbook = Nothing
            'xlapp.Quit()

            'xlapp.Application.Quit()
            'xlapp = Nothing
            'GC.Collect()
            'MsgBox("列印完畢,已存入c:\app\acc\報表\ACM080.xls")
            'Me.Close()

        Finally
            '釋放各物件所佔用的記憶體,要按照以下順序
            NAR(xlCells)
            NAR(xlRange)
            NAR(xlRange1)
            NAR(xlRange2)
            NAR(xlsheet)
            NAR(xlsheets)
            If Not xlbook Is Nothing Then xlbook.Close(False)
            NAR(xlbook)
            NAR(xlbooks)
            If Not xlapp Is Nothing Then xlapp.Quit()
            NAR(xlapp)
            GC.Collect()
        End Try

    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class
