Imports JBC.Printing
Imports System.IO
Imports Microsoft.Office.Interop
Public Class ACM030
    Dim confid, confunit As String, LoadAfter, Dirty As Boolean
    Dim SYear As Integer
    Dim myds As DataSet
    Dim strGrade7 As String = "N"   '七級科目是否列印?

    Private Sub ACM030_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dtpDateS.Value = TransPara.TransP("LastDay")
    End Sub

    Private Sub LoadGridFunc()
        Dim sqlstr, retstr, tempStr As String
        Dim mm As String = Format(Month(dtpDateS.Value), "00")  '本月
        Dim up As String = Format(Month(dtpDateS.Value) - 1, "00") '上月
        Dim sea As Integer = Season(dtpDateS.Value)      '第幾季
        Dim strBg As String = "a.bg1 + a.up1 "
        Dim inti As Integer
        'check accbg 因為要從accbg開始找資料,所以accbg必須完整
        sqlstr = "select accno from acf050 where accyear=" & SYear & " And Len(ACCNO) between 5 And 9 " & _
        " and (accno not in (select accno from accbg where accyear=" & SYear & " And Len(ACCNO) between 5 And 9)) " & _
        " and left(accno,1)='5'"
        myds = openmember("", "acf050", sqlstr)
        For inti = 0 To myds.Tables(0).Rows.Count - 1
            tempStr = nz(myds.Tables(0).Rows(inti).Item(0), "")
            If tempStr <> "" Then
                sqlstr = "insert into accbg (accyear,accno) values (" & SYear & ", '" & tempStr & "')"
                retstr = runsql("", sqlstr)
            End If
        Next

        '先清空acm010 
        sqlstr = "delete from acm010"
        retstr = runsql(mastconn, sqlstr)

        '將acf050,accbg四五六級科目丟入acm010
        '預算數
        If sea > 1 Then
            For inti = 2 To sea
                strBg &= " + a.bg" & Format(inti, "0") & " + a.up" & Format(inti, "0")
            Next
        End If
        strBg &= " as amt2, "
        '實收數
        If up = "00" Then
            tempStr = " b.deamt01 - b.cramt01 AS amt3, "
        Else
            tempStr = " b.deamt" & mm & " - b.cramt" & mm & " - (b.deamt" & up & " - b.cramt" & up & ") AS amt3, "
        End If
        '先由accbg找資料
        sqlstr = "INSERT INTO ACM010  SELECT a.accno, c.accname, " & _
                   "a.bg1+a.bg2+a.bg3+a.bg4+a.bg5+a.up1+a.up2+a.up3+a.up4+a.up5 AS amt1, " & strBg & _
                   tempStr & " b.deamt" & mm & " - b.cramt" & mm & " as amt4, " & _
                   "0 AS amt5, 0 as amt6, 0 as amt7 from ACCBG a " & _
                   "left outer join acf050 b ON a.accyear = b.accyear and a.accno=b.accno " & _
                   "left outer join ACCNAME c ON a.ACCNO = c.ACCNO " & _
                   "WHERE a.ACCYEAR = " & SYear & " AND LEN(a.ACCNO) >= 5 and substring(a.accno,1,1)='5'"
        If strGrade7.ToUpper = "Y" Then
            sqlstr = sqlstr & " and len(a.accno)<=16"    '取七級
        Else
            sqlstr = sqlstr & " and len(a.accno)<=9"     '取六級
        End If
        retstr = runsql(mastconn, sqlstr)

        '消除null  (當年度皆未有實支數時,amt3 amt4 is null), =null無法作數學運算 
        sqlstr = "update acm010 set amt3=0 where amt3 is null"
        retstr = runsql(mastconn, sqlstr)
        sqlstr = "update acm010 set amt4=0 where amt4 is null"
        retstr = runsql(mastconn, sqlstr)

        sqlstr = " delete from ACM010 where amt1=0 and amt2=0 and amt3=0 and amt4=0 and amt5=0 and amt6=0"
        retstr = runsql(mastconn, sqlstr)
        If strGrade7.ToUpper = "Y" Then
            '計算應收數AMT5
            sqlstr = " update acm010 set amt5=a.amt from " & _
            "(select left(accno,16) as accno, sum(amt) as amt from acf020 where accyear=" & SYear & " and left(accno,1)='5'" & _
            " and cotn_code='A' and dc='1'  and no_2_no > 0 GROUP by left(accno,16)) a where acm010.accno=a.accno "
            retstr = runsql(mastconn, sqlstr)
            '貸方為減少
            sqlstr = " update acm010 set amt5=amt5-a.amt from " & _
            "(select left(accno,16) as accno, sum(amt) as amt from acf020 where accyear=" & SYear & " and left(accno,1)='5'" & _
            " and cotn_code='A' and dc='2' and no_2_no > 0 GROUP by left(accno,16)) a where acm010.accno=a.accno "
            retstr = runsql(mastconn, sqlstr)
        End If
        '計算應收數AMT5
        sqlstr = " update acm010 set amt5=a.amt from " & _
        "(select left(accno,9) as accno, sum(amt) as amt from acf020 where accyear=" & SYear & " and left(accno,1)='5'" & _
        " and cotn_code='A' and dc='1'  and no_2_no > 0 GROUP by left(accno,9)) a where acm010.accno=a.accno "
        retstr = runsql(mastconn, sqlstr)
        '貸方為減少
        sqlstr = " update acm010 set amt5=amt5-a.amt from " & _
        "(select left(accno,9) as accno, sum(amt) as amt from acf020 where accyear=" & SYear & " and left(accno,1)='5'" & _
        " and cotn_code='A' and dc='2' and no_2_no > 0 GROUP by left(accno,9)) a where acm010.accno=a.accno "
        retstr = runsql(mastconn, sqlstr)

        If strGrade7.ToUpper = "Y" Then
            'update 6級
            sqlstr = "update ACM010 set amt5=a.amt from (select left(accno,9) as accno, sum(amt5) as amt from acm010 " & _
                     "where len(accno)>9 GROUP BY left(accno,9)) a where acm010.accno=a.accno"
            retstr = runsql(mastconn, sqlstr)
        End If
        'update 5級
        sqlstr = "update ACM010 set amt5=a.amt from (select left(accno,7) as accno, sum(amt5) as amt from acm010 " & _
                 "where len(accno)=9 GROUP BY left(accno,7)) a where acm010.accno=a.accno"
        retstr = runsql(mastconn, sqlstr)
        'update 4級
        sqlstr = "update ACM010 set amt5=a.amt from (select left(accno,5) as accno, sum(amt5) as amt from acm010 " & _
                 "where len(accno)=7 GROUP BY left(accno,5)) a where acm010.accno=a.accno"
        retstr = runsql(mastconn, sqlstr)

        '未支出之分配數
        sqlstr = " Update ACM010 SET amt3 = amt3 - amt5, amt4=amt4-amt5 "
        retstr = runsql(mastconn, sqlstr)

        '統計三級
        sqlstr = "INSERT INTO ACM010 select a.accno, b.accname, a.amt1, a.amt2, a.amt3, " & _
                 "a.amt4, a.amt5, a.amt6, a.amt7 from " & _
                 "(SELECT substring(accno, 1, 3) AS accno, SUM(amt1) AS amt1, " & _
                 "SUM(amt2) AS amt2, SUM(amt3) AS amt3, SUM(amt4) AS amt4, " & _
                 "SUM(amt5) AS amt5, SUM(amt6) AS amt6, 0 AS amt7 from acm010 where len(accno)=5 " & _
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

        '各欄數值計算(未收入分配數)
        sqlstr = " Update ACM010 SET amt6 = amt2 - amt4 - amt5"
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then MsgBox("update acm010 error " & sqlstr)

        '丟入dataset 
        sqlstr = "SELECT * FROM acm010 order by accno"
        myds = openmember("", "acm010", sqlstr)
    End Sub

    Private Sub BtnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrint.Click
        TransPara.TransP("LastDay") = dtpDateS.Value
        SYear = GetYear(dtpDateS.Value)
        If MsgBox("七級科目是否列印?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            strGrade7 = "Y"
        End If
        Call LoadGridFunc()   '取資料
        If myds.Tables("acm010").Rows.Count <= 1 Then
            MsgBox("無支出資料")
            Exit Sub
        End If
        Dim intR As Integer = 0  'control record number
        Dim strAccno As String
        Dim PageRow As Integer = 27  '每頁印27行
        Dim printer As FPPrinter = FPPrinter.SharedPrinter
        Dim document As New FPDocument("列印支出明細表")
        document.SetDefaultPageMargin(25, 10, 10, 10)   'left,top,right,bottom
        document.DefaultPageSettings.Landscape = True    '橫印
        document.DefaultFont = New Font("新細明體", 11) '標楷體
        For PageCnt As Integer = 1 To 99    '頁次
            Dim page As New FPPage  '新增列印頁面
            Dim textUnit As New FPText(TransPara.TransP("UnitTitle"), 90, 0)  '文字物件,座標90,0 (x,y)
            textUnit.HAlignment = FPAlignment.Center
            textUnit.Font.Size = 14   '改變文字大小為14點
            page.Add(textUnit)
            Dim textTitle As New FPText("支 出 明 細 表", 90, 6)
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
            Dim grid As New FPTable(0, 17, 250, 162, PageRow, 7)   'x,y,w,h,row,col
            grid.Font.Size = 10
            grid.ColumnStyles(1).HAlignment = StringAlignment.Near
            grid.ColumnStyles(2).HAlignment = StringAlignment.Far
            grid.ColumnStyles(3).HAlignment = StringAlignment.Far
            grid.ColumnStyles(4).HAlignment = StringAlignment.Far
            grid.ColumnStyles(5).HAlignment = StringAlignment.Far
            grid.ColumnStyles(6).HAlignment = StringAlignment.Far
            grid.ColumnStyles(7).HAlignment = StringAlignment.Far

            grid.SetLineColor(Color.Blue)   'table line color 
            grid.ColumnStyles(1).Width = 83
            grid.ColumnStyles(2).Width = 28
            grid.ColumnStyles(3).Width = 28
            grid.ColumnStyles(4).Width = 28
            grid.ColumnStyles(5).Width = 28
            grid.ColumnStyles(6).Width = 25
            grid.ColumnStyles(7).Width = 30
            grid.Cells2D(1, 1).RowSpan = 2 '1,2 row join
            grid.Cells2D(1, 2).ColSpan = 2 '2,3 隱藏
            grid.Cells2D(1, 4).ColSpan = 3 '4,5,6 隱藏
            grid.Cells2D(1, 7).RowSpan = 2 '1,2 row隱藏
            grid.Texts2D(1, 1).Text = "　科　　　　　　　　目"
            grid.Texts2D(1, 2).Text = "　預　　　　　算　　　　　數"
            grid.Texts2D(1, 4).Text = "　本　　　　　月　　　　　總　　　　　額"
            grid.Texts2D(1, 7).Text = "未支出分配數"
            grid.Texts2D(2, 2).Text = "全 年 核 定 數"
            grid.Texts2D(2, 3).Text = "本月底止分配數"
            grid.Texts2D(2, 4).Text = "本 月 實 支 數"
            grid.Texts2D(2, 5).Text = "本月底止累計數"
            grid.Texts2D(2, 6).Text = "應　付　數"

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
                tt1 = "c:\App\acc\報表樣本\ACM030.xls"
                tt2 = "c:\App\acc\報表\ACM030.xls"
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
                MsgBox("報表copy from " & tt1 & "  to  " & tt2 & " 錯誤，若 \報表\ACM030.XLS 使用中請先關閉之，否則請洽資訊人員!")
                'Dim log As New LogInfo(LogType.Exception, "拷貝報表時發生錯誤" & vbNewLine & ex.ToString)
                'gLM.Log(log)
                Exit Sub
            End Try

            xlsheets = xlbook.Worksheets
            xlsheet = xlsheets(1)     '支出明細表         
            NAR(xlCells)
            xlCells = xlsheet.Cells

            i = 1    '自第2行開始放
            With myds.Tables("acm010")
                For intR = 0 To .Rows.Count - 2
                    strAccno = nz(.Rows(intR).Item("accno"), "")
                    strAccname = nz(.Rows(intR).Item("accname"), "")
                    i += 1
                    '拷貝目前這列到下一列,使得每列都有相同的格式設定
                    xlRange1 = xlsheet.Range("A" & i & ":L" & i)
                    xlRange2 = xlsheet.Range("A" & i + 1 & ":L" & i + 1)
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
                    xlRange.Value = "0.00"
                    NAR(xlRange)
                    xlRange = xlCells(i, 8)
                    xlRange.Value = nz(.Rows(intR).Item("amt2"), 0)
                    NAR(xlRange)
                    xlRange = xlCells(i, 9)
                    xlRange.Value = nz(.Rows(intR).Item("amt3"), 0)
                    NAR(xlRange)
                    xlRange = xlCells(i, 10)
                    xlRange.Value = nz(.Rows(intR).Item("amt4"), 0)
                    NAR(xlRange)
                    xlRange = xlCells(i, 11)
                    xlRange.Value = nz(.Rows(intR).Item("amt5"), 0)
                    NAR(xlRange)
                    xlRange = xlCells(i, 12)
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
                'If MessageBox.Show("列印完畢,已存入 " & tt2 & vbNewLine & "是否要幫您開啟此份報表?", "", MsgBoxStyle.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) = MsgBoxResult.Yes Then
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
