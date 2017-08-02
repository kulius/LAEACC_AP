Imports JBC.Printing
Imports System.IO
Imports Microsoft.Office.Interop
Public Class ACM070
    Dim confid, confunit As String, LoadAfter, Dirty As Boolean
    Dim SYear As Integer
    Dim myds As DataSet
    Private Sub ACM070_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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

        '將acf020材料科目置入acm070(kind='1',"2","3","4")
        sqlstr = "INSERT INTO ACM070 " & _
        "SELECT d.ACCNO, c.ACCNAME AS accname, e.KIND, '購入' AS remark, e.UNIT, d.AMT1, " & _
        " d.AMT2, 0 AS amt3, 0 AS amt4  FROM " & _
        "(SELECT A.ACCNO, SUM(A.MAT_QTY) AS AMT1, SUM(A.AMT) AS AMT2 FROM ACF020 A LEFT OUTER JOIN " & _
        " ACF010 b ON A.KIND = b.KIND AND A.NO_1_NO = b.NO_1_NO and b.item='1' AND A.ACCYEAR = b.ACCYEAR " & _
        " WHERE SUBSTRING(A.ACCNO, 1, 3) = '114' AND b.DATE_2 >= '" & sdate & "'" & _
        " AND b.DATE_2 <= '" & FullDate(dtpDateS.Value) & "' AND A.KIND = '2' " & _
        " GROUP BY A.ACCNO) d LEFT OUTER JOIN ACF100 e ON d.ACCNO = e.ACCNO AND e.ACCYEAR = " & SYear & _
        " LEFT OUTER JOIN ACCNAME c ON substring(e.ACCNO,1,7) = c.ACCNO"
        retstr = runsql(mastconn, sqlstr)

        sqlstr = "INSERT INTO ACM070 " & _
        "SELECT d.ACCNO, c.ACCNAME AS accname, e.KIND, '科目調整' AS remark, e.UNIT, d.AMT1, " & _
        " d.AMT2, 0 AS amt3, 0 AS amt4  FROM " & _
        "(SELECT A.ACCNO, SUM(A.MAT_QTY) AS AMT1, SUM(A.AMT) AS AMT2 FROM ACF020 A LEFT OUTER JOIN " & _
        " ACF010 b ON A.KIND = b.KIND AND A.NO_1_NO = b.NO_1_NO and b.item='1' AND A.ACCYEAR = b.ACCYEAR " & _
        " WHERE SUBSTRING(A.ACCNO, 1, 3) = '114' AND b.DATE_2 >= '" & sdate & "'" & _
        " AND b.DATE_2 <= '" & FullDate(dtpDateS.Value) & "' AND A.KIND = '3' and a.seq=b.seq" & _
        " GROUP BY A.ACCNO) d LEFT OUTER JOIN ACF100 e ON d.ACCNO = e.ACCNO AND e.ACCYEAR = " & SYear & _
        " LEFT OUTER JOIN ACCNAME c ON substring(e.ACCNO,1,7) = c.ACCNO"
        retstr = runsql(mastconn, sqlstr)

        sqlstr = "INSERT INTO ACM070 " & _
        "SELECT d.ACCNO, c.ACCNAME AS accname, e.KIND, '領用' AS remark, e.UNIT, 0 AS amt1, 0 AS amt2, d.AMT3, " & _
        " d.AMT4  FROM " & _
        "(SELECT A.ACCNO, SUM(A.MAT_QTY) AS AMT3, SUM(A.AMT) AS AMT4 FROM ACF020 A LEFT OUTER JOIN " & _
        " ACF010 b ON A.KIND = b.KIND AND A.NO_1_NO = b.NO_1_NO and b.item='1' AND A.ACCYEAR = b.ACCYEAR " & _
        " WHERE SUBSTRING(A.ACCNO, 1, 3) = '114' AND b.DATE_2 between '" & sdate & "' and '" & _
         FullDate(dtpDateS.Value) & "' AND A.KIND = '4' and a.seq=b.seq " & _
        " GROUP BY A.ACCNO) d LEFT OUTER JOIN ACF100 e ON d.ACCNO = e.ACCNO AND e.ACCYEAR = " & SYear & _
         " LEFT OUTER JOIN ACCNAME c ON substring(e.ACCNO,1,7) = c.ACCNO"
        retstr = runsql(mastconn, sqlstr)

        sqlstr = "INSERT INTO ACM070 " & _
        "SELECT d.ACCNO, c.ACCNAME AS accname, e.KIND, '退回' AS remark, e.UNIT, 0 AS amt1, 0 AS amt2, d.AMT3, " & _
        " d.AMT4  FROM " & _
        "(SELECT A.ACCNO, SUM(A.MAT_QTY) AS AMT3, SUM(A.AMT) AS AMT4 FROM ACF020 A LEFT OUTER JOIN " & _
        " ACF010 b ON A.KIND = b.KIND AND A.NO_1_NO = b.NO_1_NO and b.item='1' AND A.ACCYEAR = b.ACCYEAR " & _
        " WHERE SUBSTRING(A.ACCNO, 1, 3) = '114' AND b.DATE_2 >= '" & sdate & "'" & _
        " AND b.DATE_2 <= '" & FullDate(dtpDateS.Value) & "' AND A.KIND = '1' " & _
        " GROUP BY A.ACCNO) d LEFT OUTER JOIN ACF100 e ON d.ACCNO = e.ACCNO AND e.ACCYEAR = " & SYear & _
        " LEFT OUTER JOIN ACCNAME c ON substring(e.ACCNO,1,7) = c.ACCNO"
        retstr = runsql(mastconn, sqlstr)


        '合計
        sqlstr = "INSERT INTO ACM070 " & _
                 "SELECT '9' AS accno,'合        計' as accname,'  ' as kind, '  ' as remark, " & _
                 "'  ' as unit, SUM(amt1) AS amt1, SUM(amt2) AS amt2, SUM(amt3) AS amt3, SUM(amt4) AS amt4 " & _
                 " from acm070"
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then MsgBox("合計 error " & sqlstr)
        '丟入dataset 
        sqlstr = "SELECT * FROM acm070 order by accno"
        myds = openmember("", "acm010", sqlstr)
    End Sub

    Private Sub BtnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrint.Click
        TransPara.TransP("LastDay") = dtpDateS.Value
        SYear = GetYear(dtpDateS.Value)
        Call LoadGridFunc()
        If myds.Tables("acm010").Rows.Count <= 1 Then
            MsgBox("無材料增減資料")
            Exit Sub
        End If
        Dim intR As Integer = 0  'control record number
        Dim strAccno As String
        Dim PageRow As Integer = 14  '每頁印27行
        Dim printer = New KPrint
        Dim document As New FPDocument("材料增減表")
        document.SetDefaultPageMargin(25, 10, 10, 10)   'left,top,right,bottom
        document.DefaultPageSettings.Landscape = True    '橫印
        document.DefaultFont = New Font("新細明體", 11) '標楷體
        For PageCnt As Integer = 1 To 99    '頁次
            Dim page As New FPPage  '新增列印頁面
            Dim textUnit As New FPText(TransPara.TransP("UnitTitle"), 90, 0)  '文字物件,座標90,0 (x,y)
            textUnit.HAlignment = FPAlignment.Center
            textUnit.Font.Size = 14   '改變文字大小為14點
            page.Add(textUnit)
            Dim textTitle As New FPText("材 料 增 減 表", 90, 6)
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
            Dim grid As New FPTable(0, 17, 250, 162, PageRow, 9)   'x,y,w,h,row,col
            grid.Font.Size = 10
            grid.ColumnStyles(1).HAlignment = StringAlignment.Near
            grid.ColumnStyles(2).HAlignment = StringAlignment.Near
            grid.ColumnStyles(3).HAlignment = StringAlignment.Near
            grid.ColumnStyles(4).HAlignment = StringAlignment.Near
            grid.ColumnStyles(5).HAlignment = StringAlignment.Far
            grid.ColumnStyles(6).HAlignment = StringAlignment.Far
            grid.ColumnStyles(7).HAlignment = StringAlignment.Far
            grid.ColumnStyles(8).HAlignment = StringAlignment.Far
            grid.ColumnStyles(9).HAlignment = StringAlignment.Far

            grid.SetLineColor(Color.Blue)   'table line color 
            grid.ColumnStyles(1).Width = 50
            grid.ColumnStyles(2).Width = 50
            grid.ColumnStyles(3).Width = 25
            grid.ColumnStyles(4).Width = 15
            grid.ColumnStyles(5).Width = 25
            grid.ColumnStyles(6).Width = 25
            grid.ColumnStyles(7).Width = 25
            grid.ColumnStyles(8).Width = 25
            grid.ColumnStyles(9).Width = 10
            grid.Cells2D(1, 1).RowSpan = 2 '1,2 row join
            grid.Cells2D(1, 2).RowSpan = 2 '1,2 row join
            grid.Cells2D(1, 3).RowSpan = 2 '1,2 row join
            grid.Cells2D(1, 4).RowSpan = 2 '2,3 隱藏
            grid.Cells2D(1, 5).ColSpan = 2 '4,5,6 隱藏
            grid.Cells2D(1, 7).ColSpan = 2 '4,5,6 隱藏
            grid.Cells2D(1, 9).RowSpan = 2 '1,2 row隱藏
            grid.Texts2D(1, 1).Text = "　　明　細　科　目"
            grid.Texts2D(1, 2).Text = "　　名　稱　規　格"
            grid.Texts2D(1, 3).Text = "　摘　　要"
            grid.Texts2D(1, 4).Text = "單位"
            grid.Texts2D(1, 5).Text = "本　　月　　增　　加"
            grid.Texts2D(1, 7).Text = "本　　月　　減　　少"
            grid.Texts2D(2, 5).Text = "數　　量"
            grid.Texts2D(2, 6).Text = "金　　　額"
            grid.Texts2D(2, 7).Text = "數　　量"
            grid.Texts2D(2, 8).Text = "金　　　額"
            grid.Texts2D(1, 9).Text = "備註"

            With myds.Tables("acm010")
                For i As Integer = 3 To PageRow
                    If intR > .Rows.Count - 1 Then
                        PageCnt = 999    'EXIT FOR THE PAGE 
                        Exit For
                    End If
                    strAccno = Mid(.Rows(intR).Item("accno"), 1, 7)
                    grid.Texts2D(i, 1).Text = FormatAccno(strAccno) & vbCrLf & nz(.Rows(intR).Item("accname"), "")
                    grid.Texts2D(i, 2).Text = nz(.Rows(intR).Item("kind"), "")
                    grid.Texts2D(i, 3).Text = nz(.Rows(intR).Item("remark"), "")
                    grid.Texts2D(i, 4).Text = nz(.Rows(intR).Item("unit"), "")
                    grid.Texts2D(i, 5).Text = FormatNumber(nz(.Rows(intR).Item("amt1"), 0), 2)
                    grid.Texts2D(i, 6).Text = FormatNumber(nz(.Rows(intR).Item("amt2"), 0), 2)
                    grid.Texts2D(i, 7).Text = FormatNumber(nz(.Rows(intR).Item("amt3"), 0), 2)
                    grid.Texts2D(i, 8).Text = FormatNumber(nz(.Rows(intR).Item("amt4"), 0), 2)
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

End Class
