Imports JBC.Printing
Imports System.IO
Imports Microsoft.Office.Interop
Imports System.Drawing.Printing
Public Class ACM010
    Dim confid, confunit As String, LoadAfter, Dirty As Boolean
    Dim SYear As Integer
    Dim myds As DataSet


    Private Sub ACM010_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dtpDateS.Value = TransPara.TransP("LastDay")
    End Sub

    Private Sub LoadGridFunc()
        Dim sqlstr, retstr, tempStr As String
        Dim mm As String = Format(Month(dtpDateS.Value), "00")  '本月
        Dim up As String = Format(Month(dtpDateS.Value) - 1, "00") '上月

        '先清空acm010 
        sqlstr = "delete from acm010"
        retstr = runsql(mastconn, sqlstr)

        '將acf050四級科目丟入acm010
        If up = "00" Then
            tempStr = " a.beg_debit AS amt1, a.beg_credit AS amt2, "
        Else
            tempStr = " a.DEAMT" & up & " AS amt1, a.CRAMT" & up & " AS amt2, "
        End If

        sqlstr = "INSERT INTO ACM010  SELECT a.ACCNO, b.ACCNAME," & tempStr & _
                 " a.DEAMT" & mm & " AS amt3, a.CRAMT" & mm & " AS amt4, " & _
                 " a.DEAMT" & mm & " AS amt5, a.CRAMT" & mm & " AS amt6, 0 AS amt7 " & _
                 "FROM ACF050 a left outer join ACCNAME b ON a.ACCNO = b.ACCNO " & _
                 "WHERE a.ACCYEAR = " & SYear & " AND LEN(a.ACCNO) = 5"
        retstr = runsql(mastconn, sqlstr)

        '各欄數值計算(本月總額)
        sqlstr = " Update ACM010 SET amt3 = amt3 - amt1, amt4 = amt4 - amt2"
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then MsgBox("update acm010 error " & sqlstr)
        '各欄數值計算(本月餘額)
        sqlstr = "Update ACM010 SET amt5 = amt5 - amt6, amt6 = 0 WHERE amt5 > amt6"
        retstr = runsql(mastconn, sqlstr)
        sqlstr = "Update ACM010 SET amt6 = amt6 - amt5, amt5 = 0 WHERE amt6 > amt5"
        retstr = runsql(mastconn, sqlstr)
        sqlstr = "Update ACM010 SET amt6 = 0, amt5 = 0 WHERE amt6 = amt5"
        retstr = runsql(mastconn, sqlstr)
        '各欄數值計算(上月餘額)
        sqlstr = "Update ACM010 SET amt2 = amt2 - amt1, amt1 = 0 WHERE amt2 > amt1"
        retstr = runsql(mastconn, sqlstr)
        sqlstr = "Update ACM010 SET amt1 = amt1 - amt2, amt2 = 0 WHERE amt1 > amt2"
        retstr = runsql(mastconn, sqlstr)
        sqlstr = "Update ACM010 SET amt1 = 0, amt2 = 0 WHERE amt1 = amt2"
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then MsgBox("update acm010 error " & sqlstr)

        '統計三級
        sqlstr = "INSERT INTO ACM010 select a.accno, b.accname, a.amt1, a.amt2, a.amt3, " & _
                 "a.amt4, a.amt5, a.amt6, a.amt7 from " & _
                 "(SELECT substring(accno, 1, 3) AS accno, SUM(amt1) AS amt1, " & _
                 "SUM(amt2) AS amt2, SUM(amt3) AS amt3, SUM(amt4) AS amt4, " & _
                 "SUM(amt5) AS amt5, SUM(amt6) AS amt6, 0 AS amt7 from acm010 " & _
                 "GROUP BY substring(accno, 1, 3)) a left outer join ACCNAME b ON a.accno = b.ACCNO"
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then MsgBox("統計三級 error " & sqlstr)

        '統計二級
        sqlstr = "INSERT INTO ACM010 select a.accno, b.accname, a.amt1, a.amt2, a.amt3, " & _
                 "a.amt4, a.amt5, a.amt6, a.amt7 from " & _
                 "(SELECT substring(accno, 1, 2) AS accno, SUM(amt1) AS amt1, " & _
                 "SUM(amt2) AS amt2, SUM(amt3) AS amt3, SUM(amt4) AS amt4, " & _
                 "SUM(amt5) AS amt5, SUM(amt6) AS amt6, 0 AS amt7 from acm010 " & _
                 "where len(accno)=3 " & _
                 "GROUP BY substring(accno, 1, 2)) a left outer join ACCNAME b ON a.accno = b.ACCNO"
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then MsgBox("統計二級 error " & sqlstr)
        '統計一級
        sqlstr = "INSERT INTO ACM010 select a.accno, b.accname, a.amt1, a.amt2, a.amt3, " & _
                 "a.amt4, a.amt5, a.amt6, a.amt7 from " & _
                 "(SELECT substring(accno, 1, 1) AS accno, SUM(amt1) AS amt1, " & _
                 "SUM(amt2) AS amt2, SUM(amt3) AS amt3, SUM(amt4) AS amt4, " & _
                 "SUM(amt5) AS amt5, SUM(amt6) AS amt6, 0 AS amt7 from acm010 " & _
                 "where len(accno)=2 " & _
                 "GROUP BY substring(accno, 1, 1)) a left outer join ACCNAME b ON a.accno = b.ACCNO"
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then MsgBox("統計一級 error " & sqlstr)
        '合計
        sqlstr = "INSERT INTO ACM010 " & _
                 "SELECT '9' AS accno,'合             計' as accname, SUM(amt1) AS amt1, " & _
                 "SUM(amt2) AS amt2, SUM(amt3) AS amt3, SUM(amt4) AS amt4, " & _
                 "SUM(amt5) AS amt5, SUM(amt6) AS amt6, 0 AS amt7 from acm010 " & _
                 "where len(accno)=1 "
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then MsgBox("合計 error " & sqlstr)
        '丟入dataset 
        sqlstr = "SELECT * FROM acm010 order by accno"
        myds = openmember("", "acm010", sqlstr)
    End Sub

    Private Sub BtnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrint.Click
        TransPara.TransP("LastDay") = dtpDateS.Value
        SYear = GetYear(dtpDateS.Value)
        Call LoadGridFunc()
        If myds.Tables("acm010").Rows.Count <= 1 Then
            MsgBox("無此資料")
            Exit Sub
        End If
        Dim intR As Integer = 0  'control record number
        Dim strAccno As String
        Dim PageRow As Integer = 27  '每頁印27行
        Dim printer = New KPrint
        Dim document As New FPDocument("列印總分類帳彙總表")
        document.SetDefaultPageMargin(25, 10, 10, 10)   'left,top,right,bottom
        document.DefaultPageSettings.Landscape = True    '橫印
        document.DefaultFont = New Font("新細明體", 11) '標楷體
        For PageCnt As Integer = 1 To 99    '頁次
            Dim page As New FPPage  '新增列印頁面
            Dim textUnit As New FPText(TransPara.TransP("UnitTitle"), 90, 0)  '文字物件,座標90,0 (x,y)
            textUnit.HAlignment = FPAlignment.Center
            textUnit.Font.Size = 14   '改變文字大小為14點
            page.Add(textUnit)
            Dim textTitle As New FPText("總 分 類 帳 彙 總 表", 90, 6)
            textTitle.HAlignment = FPAlignment.Center
            textTitle.Font.Size = 14   '改變文字大小為14點
            page.Add(textTitle)
            Dim textday As New FPText("中華民國" & SYear & "年" & Month(dtpDateS.Value) & "月1日起至" & _
                SYear & "年" & Month(dtpDateS.Value) & "月" & Microsoft.VisualBasic.DateAndTime.Day(dtpDateS.Value) & "日止", 90, 12)
            textday.HAlignment = FPAlignment.Center
            textday.Font.Size = 11   '改變文字大小為14點
            page.Add(textday)
            Dim textPage As New FPText("第" & PageCnt & "頁", 235, 12)
            page.Add(textPage)

            '新增表格物件
            '每欄ㄉ寬度和每列的高度,預設會使用自動平均後的值
            'FPTable 和 FPGrid 這兩個物件的差異在 FPTable內含 FPCell物件,可以設定跨列和跨欄,但FPGrid則無
            Dim grid As New FPTable(0, 17, 254, PageRow * 6, PageRow, 7) 'x,y,w,h,row,col
            grid.Font.Size = 10
            grid.ColumnStyles(1).HAlignment = StringAlignment.Near
            grid.ColumnStyles(2).HAlignment = StringAlignment.Far
            grid.ColumnStyles(3).HAlignment = StringAlignment.Far
            grid.ColumnStyles(4).HAlignment = StringAlignment.Far
            grid.ColumnStyles(5).HAlignment = StringAlignment.Far
            grid.ColumnStyles(6).HAlignment = StringAlignment.Far
            grid.ColumnStyles(7).HAlignment = StringAlignment.Far

            grid.SetLineColor(Color.Blue)   'table line color 
            grid.ColumnStyles(1).Width = 62
            grid.ColumnStyles(2).Width = 32
            grid.ColumnStyles(3).Width = 32
            grid.ColumnStyles(4).Width = 32
            grid.ColumnStyles(5).Width = 32
            grid.ColumnStyles(6).Width = 32
            grid.ColumnStyles(7).Width = 32
            grid.Cells2D(1, 1).RowSpan = 2 '1,2 row join
            grid.Cells2D(1, 2).ColSpan = 2 '2,3 隱藏
            grid.Cells2D(1, 4).ColSpan = 2 '4,5 隱藏
            grid.Cells2D(1, 6).ColSpan = 2 '6,7 隱藏
            grid.Texts2D(1, 1).Text = "會　計　科　目　及　符　號"
            grid.Texts2D(1, 2).Text = "　上　　　月　　　餘　　　額　"
            grid.Texts2D(1, 4).Text = "　本　　　月　　　總　　　額　"
            grid.Texts2D(1, 6).Text = "　本　　　月　　　餘　　　額　"
            grid.Texts2D(2, 2).Text = "借        　　方"
            grid.Texts2D(2, 3).Text = "貸        　　方"
            grid.Texts2D(2, 4).Text = "借        　　方"
            grid.Texts2D(2, 5).Text = "貸        　　方"
            grid.Texts2D(2, 6).Text = "借        　　方"
            grid.Texts2D(2, 7).Text = "貸        　　方"

            With myds.Tables("acm010")
                For i As Integer = 3 To PageRow
                    If intR > .Rows.Count - 1 Then
                        PageCnt = 999    'EXIT FOR THE PAGE 
                        Exit For
                    End If
                    strAccno = .Rows(intR).Item("accno")
                    grid.Texts2D(i, 1).Text = Space((Grade(strAccno) - 1) * 2) & FormatAccno(strAccno) & nz(.Rows(intR).Item("accname"), "")
                    grid.Texts2D(i, 2).Text = FormatNumber(nz(.Rows(intR).Item("amt1"), 0), 2)
                    grid.Texts2D(i, 3).Text = FormatNumber(nz(.Rows(intR).Item("amt2"), 0), 2)
                    grid.Texts2D(i, 4).Text = FormatNumber(nz(.Rows(intR).Item("amt3"), 0), 2)
                    grid.Texts2D(i, 5).Text = FormatNumber(nz(.Rows(intR).Item("amt4"), 0), 2)
                    grid.Texts2D(i, 6).Text = FormatNumber(nz(.Rows(intR).Item("amt5"), 0), 2)
                    grid.Texts2D(i, 7).Text = FormatNumber(nz(.Rows(intR).Item("amt6"), 0), 2)
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
    End Sub




    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
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
            SYear = GetYear(dtpDateS.Value)
            TransPara.TransP("LastDay") = dtpDateS.Value

            Call LoadGridFunc()

            If myds.Tables("acm010").Rows.Count = 0 Then
                MsgBox("無此資料")
                Exit Sub
            End If

            Dim intR As Integer = 0  'control record number
            Dim i, ii As Integer    'control excel row , i是總表的row no. , ii是明細表的row no.
            Dim strAccno, strAccname As String
            Dim strS, strT As String
            Dim spaces As Integer
            Dim tt1, tt2 As String
            Try
                tt1 = "c:\App\acc\報表樣本\ACM010.xls"
                tt2 = "c:\App\acc\報表\ACM010.xls"
                If Not File.Exists(tt1) Then
                    MsgBox("找不到報表樣本，請洽資訊人員" & vbNewLine & tt1)
                    Exit Sub
                End If
                FileCopy(tt1, tt2)    'copy tt1 to tt2
                xlapp = CreateObject("Excel.Application")
                xlapp.DisplayAlerts = False  '隱藏所有警告或確認訊息視窗,否則某些指令會失敗,譬如刪除工作表
                xlbooks = xlapp.Workbooks
                xlbook = xlbooks.Open(tt2)   '開啟tt2
            Catch ex As Exception
                MsgBox("報表copy from " & tt1 & "  to  " & tt2 & " 錯誤，若 \報表\ACM010.XLS 使用中請先關閉之，否則請洽資訊人員!")
                ' Dim log As New LogInfo(LogType.Exception, "拷貝報表時發生錯誤" & vbNewLine & ex.ToString)
                ' gLM.Log(log)
                Exit Sub
            End Try

            xlsheets = xlbook.Worksheets
            xlsheet = xlsheets(1)     '總分類帳彙總表         
            NAR(xlCells)
            xlCells = xlsheet.Cells

            i = 1    '自第2行開始放
            With myds.Tables("acm010")
                For intR = 0 To .Rows.Count - 2
                    strAccno = nz(.Rows(intR).Item("accno"), "")
                    strAccname = nz(.Rows(intR).Item("accname"), "")
                    i += 1
                    '拷貝目前這列到下一列,使得每列都有相同的格式設定
                    xlRange1 = xlsheet.Range("A" & i & ":K" & i)
                    xlRange2 = xlsheet.Range("A" & i + 1 & ":K" & i + 1)
                    xlRange1.Copy(xlRange2)
                    NAR(xlRange1)
                    NAR(xlRange2)
                    '開始填入每列的資料
                    xlRange = xlCells(i, 1)
                    If TransPara.TransP("UnitTitle") <> "" Then
                        xlRange.Value = Trim(Replace(Replace(TransPara.TransP("UnitTitle"), "臺灣", ""), "農田", ""))
                    Else
                        xlRange.Value = "水利會"
                    End If
                    NAR(xlRange)
                    xlRange = xlCells(i, 2)
                    xlRange.Value = GetYear(dtpDateS.Value)
                    NAR(xlRange)
                    xlRange = xlCells(i, 3)
                    xlRange.Value = Month(dtpDateS.Value)
                    NAR(xlRange)
                    xlRange = xlCells(i, 4)
                    xlRange.Value = FormatAccno(strAccno)
                    NAR(xlRange)
                    xlRange = xlCells(i, 5)
                    xlRange.Value = nz(.Rows(intR).Item("accname"), "")
                    NAR(xlRange)
                    xlRange = xlCells(i, 6)
                    xlRange.Value = nz(.Rows(intR).Item("amt1"), 0)
                    NAR(xlRange)
                    xlRange = xlCells(i, 7)
                    xlRange.Value = nz(.Rows(intR).Item("amt2"), 0)
                    NAR(xlRange)
                    xlRange = xlCells(i, 8)
                    xlRange.Value = nz(.Rows(intR).Item("amt3"), 0)
                    NAR(xlRange)
                    xlRange = xlCells(i, 9)
                    xlRange.Value = nz(.Rows(intR).Item("amt4"), 0)
                    NAR(xlRange)
                    xlRange = xlCells(i, 10)
                    xlRange.Value = nz(.Rows(intR).Item("amt5"), 0)
                    NAR(xlRange)
                    xlRange = xlCells(i, 11)
                    xlRange.Value = nz(.Rows(intR).Item("amt6"), 0)
                    NAR(xlRange)
                Next
            End With

            '儲存檔案
            xlbook.Save()
            If rdoPrint.Checked Then
                'xlapp.Visible = True
                'xlbook.PrintPreview() '不可用預覽列印,否則當使用者關閉excel視窗之後就會導致finally區段關閉excel.exe時出現error
                xlbook.PrintOut()  '直接列印
            End If

            Dim result3 As DialogResult = MessageBox.Show("列印完畢,已存入 " & tt2 & vbNewLine & "是否要幫您開啟此份報表?", _
            "", _
            MessageBoxButtons.YesNoCancel, _
            MessageBoxIcon.Question, _
            MessageBoxDefaultButton.Button2)
            If result3 = DialogResult.Yes Then
                '不可用 Process.Start(tt2) 否則會造成用同一個excel process開啟報表,導致finally區段關閉excel.exe時會出現error
                Process.Start("excel.exe", tt2)
            End If

            Me.Close()

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
End Class
