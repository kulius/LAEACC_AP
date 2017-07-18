Imports JBC.Printing
Public Class PAY091
    Dim LoadAfter, Dirty As Boolean
    Dim myDataSet, myds As DataSet
    Dim bm As BindingManagerBase
    Dim SBank As String
    Dim sdate As Date
    Dim SYear As Integer
    Dim sNo1 As Integer = 0
    Dim eNo1 As Integer = 0
    Dim sNo2 As Integer = 0
    Dim eNo2 As Integer = 0
    Dim recno1 As Integer = 0
    Dim recno2 As Integer = 0

    Private Sub pay091_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadAfter = True
        Dim sqlstr, qstr, strD, strC As String
        Dim intI, PageNo As Integer
        dtpDate.Value = Now
    End Sub

    Private Sub btnSure_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSure.Click
        Dim sqlstr, retstr, qstr, strD, strC, skind As String
        Dim intI, intJ, SumAmt, err As Integer
        sdate = dtpDate.Value.ToShortDateString
        sqlstr = "SELECT b.*, c.BANKNAME AS bankname, c.ACCNO AS accno FROM " & _
                 "(SELECT MAX(date_2) AS date_2, bank FROM CHF030 WHERE DATE_2<='" & FullDate(dtpDate.Value) & "' GROUP BY BANK) a " & _
                 "INNER JOIN CHF030 b ON a.bank = b.BANK AND a.date_2 = b.DATE_2 " & _
                 "left outer join CHF020 c ON a.bank = c.BANK ORDER BY c.ACCNO, b.bank"
        myDataSet = openmember("", "chf020", sqlstr)
        If myDataSet.Tables("chf020").Rows.Count = 0 Then
            MsgBox("沒有結存資料")
            btnSure.Visible = False
            Exit Sub
        Else
            DataGrid1.DataSource = myDataSet
            DataGrid1.DataMember = "chf020"
            bm = Me.BindingContext(myDataSet, "chf020")
        End If

        '找各傳票起訖號
        sqlstr = "SELECT kind, min(no_2_no) as sno, max(no_2_no) as eno, count(*) as recno from acf010 " & _
                 " where date_2='" & FullDate(dtpDate.Value) & "' and item='9' and kind<='2' GROUP BY kind "
        myds = openmember("", "chf020", sqlstr)
        With myds.Tables("chf020")
            For intI = 0 To .Rows.Count - 1
                If .Rows(intI).Item("kind") = "1" Then
                    sNo1 = nz(.Rows(intI).Item("sno"), 0)
                    eNo1 = nz(.Rows(intI).Item("eno"), 0)
                    recno1 = nz(.Rows(intI).Item("recno"), 0)
                End If
                If .Rows(intI).Item("kind") = "2" Then
                    sNo2 = nz(.Rows(intI).Item("sno"), 0)
                    eNo2 = nz(.Rows(intI).Item("eno"), 0)
                    recno2 = nz(.Rows(intI).Item("recno"), 0)
                End If
            Next
        End With

        txtPageNo.Text = Val(txtPageNo.Text) - 1  '頁次在列印前先減1, because each page will add 1 
        'Dim a As New PAY090
        'Call a.PAY090_PRINT()

        Call PAY091_PRINT()

        myDataSet = Nothing
    End Sub

    Public Sub PAY091_PRINT()
        Dim intR As Integer = 0
        Dim sumamt1 As Decimal = 0
        Dim sumamt2 As Decimal = 0
        Dim sumamt3 As Decimal = 0
        Dim totamt1 As Decimal = 0
        Dim totamt2 As Decimal = 0
        Dim totamt3 As Decimal = 0
        Dim amt1 As Decimal = 0
        Dim amt2 As Decimal = 0
        Dim amt3 As Decimal = 0
        Dim strAccno As String = ""
        Dim sqlstr, strbankname As String
        'Dim pagerow As Integer = 25

        Dim pagerow As Integer = 5  '銀行筆數要再加合計三行表頭二行,讓全部印在一頁
        With myDataSet.Tables("chf020")
            For intR = 0 To .Rows.Count - 1
                If Not (FullDate(.Rows(intR).Item("date_2")) < FullDate(dtpDate.Value) And .Rows(intR).Item("balance") = 0) Then
                    pagerow += 1
                End If
            Next
        End With
        intR = 0

        Dim printer As FPPrinter = FPPrinter.SharedPrinter
        Dim document As New FPDocument("列印現金結存日報表")
        document.SetDefaultPageMargin(20, 0, 5, 5)   'left,top,right,botton
        document.DefaultPageSettings.Landscape = True    '橫印
        document.DefaultFont = New Font("標楷體", 10) '標楷體  新細明體

        strAccno = nz(myDataSet.Tables("chf020").Rows(intR).Item("accno"), "11102")

        For PageCnt As Integer = 1 To 99    '頁次
            'Call pay090_head()
            Dim page As New FPPage '新增列印頁面
            page.Font.Size = 10  '設定本頁文字default size=10

            Dim textUnit As New FPText(TransPara.TransP("UnitTitle"), 90, 0) '新增文字物件,要列印在座標 (col,row) 
            textUnit.HAlignment = FPAlignment.Center
            textUnit.Font.Size = 12   '改變文字大小為14點
            page.Add(textUnit)
            Dim textTitle As New FPText("現金結存日報表", 90, 5)
            textTitle.HAlignment = FPAlignment.Center
            textTitle.Font.Size = 12
            page.Add(textTitle)
            Dim textday As New FPText("中華民國" & FormatNumber(GetYear(sdate), "000") & "年" & Month(sdate) & _
                "月" & Microsoft.VisualBasic.DateAndTime.Day(sdate) & "日", 90, 10)
            textday.HAlignment = FPAlignment.Center
            page.Add(textday)
            txtPageNo.Text = Val(txtPageNo.Text) + 1
            Dim textPage As New FPText("第" & txtPageNo.Text & "頁", 240, 10)
            page.Add(textPage)
            Dim textNo1 As New FPText("收入傳票自第" & Format(sNo1, "00000") & "號至第" & Format(eNo1, "00000") & "號共" & recno1 & "張", 0, 5)
            page.Add(textNo1)
            Dim textNo2 As New FPText("支出傳票自第" & Format(sNo2, "00000") & "號至第" & Format(eNo2, "00000") & "號共" & recno2 & "張", 0, 10)
            page.Add(textNo2)

            '新增表格物件,要列印在座標 (15,10),寬度280,高度150,共有22列7欄,(單位是公厘)
            '每欄ㄉ寬度和每列的高度,預設會使用自動平均後的值
            'FPTable 和 FPGrid 這兩個物件的差異在 FPTable內含 FPCell物件,可以設定跨列和跨欄,但FPGrid則無
            Dim grid As New FPTable(0, 15, 260, 162, pagerow, 7)   'x,y,w,h
            grid.Font.Size = 10
            '設bankname = small size =10
            grid.ColumnStyles(1).HAlignment = StringAlignment.Far
            grid.ColumnStyles(2).HAlignment = StringAlignment.Far
            grid.ColumnStyles(3).HAlignment = StringAlignment.Far
            grid.ColumnStyles(4).HAlignment = StringAlignment.Near
            grid.ColumnStyles(5).HAlignment = StringAlignment.Far
            grid.ColumnStyles(6).HAlignment = StringAlignment.Far
            grid.ColumnStyles(7).HAlignment = StringAlignment.Far
            grid.SetLineColor(Color.Red)   'table line = red 
            grid.ColumnStyles(1).Width = 35
            grid.ColumnStyles(2).Width = 30
            grid.ColumnStyles(3).Width = 35
            grid.ColumnStyles(4).Width = 60
            grid.ColumnStyles(5).Width = 30
            grid.ColumnStyles(6).Width = 35
            grid.ColumnStyles(7).Width = 35
            grid.Cells2D(1, 1).ColSpan = 3 '1,2,3 隱藏
            grid.Cells2D(1, 5).ColSpan = 3 '5,6,7 隱藏
            grid.Cells2D(1, 4).RowSpan = 2 '2 row join 
            grid.Texts2D(1, 1).Text = "收　　　　　　方　　　　　　金　　　　　　額"
            grid.Texts2D(1, 4).Text = "　　　存    款    帳    戶"
            grid.Texts2D(1, 5).Text = "付　　　　　　方　　　　　　金　　　　　　額"
            grid.Texts2D(2, 1).Text = "昨　日　結　存"
            grid.Texts2D(2, 2).Text = "本　日　共　收"
            grid.Texts2D(2, 3).Text = "合　　　　　計"
            grid.Texts2D(2, 5).Text = "本　日　共　支"
            grid.Texts2D(2, 6).Text = "本　日　結　存"
            grid.Texts2D(2, 7).Text = "合　　　　　計"
            Dim i As Integer = 2 '控制行數
            With myDataSet.Tables("chf020")
                Do While i < pagerow
                    If intR <= .Rows.Count - 1 Then
                        If FullDate(.Rows(intR).Item("date_2")) < FullDate(dtpDate.Value) And .Rows(intR).Item("balance") = 0 Then
                            intR += 1     '只取該日餘額即可,because day_income, day_pay belong another day's 
                            GoTo Endloop
                        End If
                    End If
                    If intR > .Rows.Count - 1 Then
                        If sumamt1 + sumamt2 + sumamt3 <> 0 Then
                            i += 1
                            If i > pagerow Then Exit Do
                            sqlstr = "SELECT * from accname where accno='" & strAccno & "'"
                            myds = openmember("", "accname", sqlstr)
                            If myds.Tables("accname").Rows.Count > 0 Then
                                strbankname = "    " & myds.Tables("accname").Rows(0).Item("accname") & "合計"
                            Else
                                strbankname = "    " & strAccno & "合計"
                            End If
                            grid.Texts2D(i, 1).Text = FormatNumber(sumamt3 + sumamt2 - sumamt1, 2)
                            grid.Texts2D(i, 2).Text = FormatNumber(sumamt1, 2)
                            grid.Texts2D(i, 3).Text = FormatNumber(sumamt3 + sumamt2, 2)
                            grid.Texts2D(i, 4).Text = strbankname
                            grid.Texts2D(i, 5).Text = FormatNumber(sumamt2, 2)
                            grid.Texts2D(i, 6).Text = FormatNumber(sumamt3, 2)
                            grid.Texts2D(i, 7).Text = FormatNumber(sumamt2 + sumamt3, 2)
                            sumamt1 = 0
                            sumamt2 = 0
                            sumamt3 = 0
                        End If
                        If totamt1 + totamt2 + totamt3 <> 0 Then
                            strbankname = "　　合　　　　　　　　計"
                            i += 1
                            If i > pagerow Then Exit Do
                            grid.Texts2D(i, 1).Text = FormatNumber(totamt3 + totamt2 - totamt1, 2)
                            grid.Texts2D(i, 2).Text = FormatNumber(totamt1, 2)
                            grid.Texts2D(i, 3).Text = FormatNumber(totamt3 + totamt2, 2)
                            grid.Texts2D(i, 4).Text = strbankname
                            grid.Texts2D(i, 5).Text = FormatNumber(totamt2, 2)
                            grid.Texts2D(i, 6).Text = FormatNumber(totamt3, 2)
                            grid.Texts2D(i, 7).Text = FormatNumber(totamt2 + totamt3, 2)
                        End If
                        PageCnt = 100
                        Exit Do
                        Exit For
                    End If
                    If nz(.Rows(intR).Item("accno"), "") <> strAccno Then '科目不同時要印小計
                        i += 1
                        If i > pagerow Then Exit Do
                        sqlstr = "SELECT * from accname where accno='" & strAccno & "'"
                        myds = openmember("", "accname", sqlstr)
                        If myds.Tables("accname").Rows.Count > 0 Then
                            strbankname = "    " & myds.Tables("accname").Rows(0).Item("accname") & "合計"
                        Else
                            strbankname = "    " & strAccno & "合計"
                        End If
                        grid.Texts2D(i, 1).Text = FormatNumber(sumamt3 + sumamt2 - sumamt1, 2)
                        grid.Texts2D(i, 2).Text = FormatNumber(sumamt1, 2)
                        grid.Texts2D(i, 3).Text = FormatNumber(sumamt3 + sumamt2, 2)
                        grid.Texts2D(i, 4).Text = strbankname
                        grid.Texts2D(i, 5).Text = FormatNumber(sumamt2, 2)
                        grid.Texts2D(i, 6).Text = FormatNumber(sumamt3, 2)
                        grid.Texts2D(i, 7).Text = FormatNumber(sumamt2 + sumamt3, 2)
                        sumamt1 = 0
                        sumamt2 = 0
                        sumamt3 = 0

                        strAccno = nz(.Rows(intR).Item("accno"), "")
                    End If
                    strbankname = nz(.Rows(intR).Item("bankname"), "")
                    If FullDate(.Rows(intR).Item("date_2")) < FullDate(dtpDate.Value) Then
                        amt1 = 0
                        amt2 = 0
                        amt3 = .Rows(intR).Item("balance")
                    Else
                        amt1 = .Rows(intR).Item("day_income")
                        amt2 = .Rows(intR).Item("day_pay")
                        amt3 = .Rows(intR).Item("balance")
                    End If
                    i += 1
                    If i > pagerow Then
                        Exit Do
                    End If
                    grid.Texts2D(i, 1).Text = FormatNumber(amt3 + amt2 - amt1, 2)
                    grid.Texts2D(i, 2).Text = IIf(amt1 = 0, "", FormatNumber(amt1, 2))
                    grid.Texts2D(i, 3).Text = FormatNumber(amt2 + amt3, 2)
                    grid.Texts2D(i, 4).Text = strbankname
                    grid.Texts2D(i, 5).Text = IIf(amt2 = 0, "", FormatNumber(amt2, 2))
                    grid.Texts2D(i, 6).Text = FormatNumber(amt3, 2)
                    grid.Texts2D(i, 7).Text = FormatNumber(amt2 + amt3, 2)

                    sumamt1 += amt1
                    sumamt2 += amt2
                    sumamt3 += amt3
                    totamt1 += amt1
                    totamt2 += amt2
                    totamt3 += amt3
                    intR += 1
EndLoop:        Loop
            End With
            page.Add(grid) '加入要列印的表格到列印頁面中
            Dim str As String
            If TransPara.TransP("UnitTitle").IndexOf("公") >= 0 Then  '榴公決裁加總幹事
                str = "製  表　　　　　 　主辦出納人員　　　　　　　覆核" & _
                      "　　　　　  主辦主計人員　　　　　　　總幹事　　　　 　會　長"
            Else
                str = "製  表　　　　　　　  　主辦出納人員　　　　　　　　　　覆核" & _
                      " 　　　　　　　  主辦主計人員　　　　　　  　　會　長"
            End If
            Dim textBotton As New FPText(str, 0, 180)
            page.Add(textBotton)   '加印決裁行
            document.AddPage(page) '加入要列印的頁面到列印文件中
        Next
        printer.Document = document '將要列印的文件送到印表機
        printer.PrintMode = PrintMode.NormalPrint
        printer.IsAutoShowPrintDialog = True '自動顯示印表機設定對話盒
        If rdoPreview.Checked Then printer.IsAutoShowPrintPreviewDialog = True
        printer.Print()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        MyBase.Close()
    End Sub
End Class
