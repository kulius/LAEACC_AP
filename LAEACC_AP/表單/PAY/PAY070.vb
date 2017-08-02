Imports JBC.Printing
Public Class PAY070
    Dim confid, confunit As String, LoadAfter, Dirty As Boolean
    Dim sqlstr As String
    Dim strBank As String
    Dim SYear As Integer
    Dim DateS As String
    'Dim strAccount, strBankname As String   '銀行名稱
    Dim mydataset, myds, mydsD As DataSet
    Dim Upbalance, AcuIncome, AcuPay As Decimal '上日存款餘額,本日收入累計,本日支出累計,本月收入累計,本月支出累計
    Dim PageRow As Integer = 37  '每頁印22行

    Private Sub PAY070_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '找收付日期
        sqlstr = "SELECT date_2 from chf020 where date_2 is not null"
        myds = openmember("", "chf020", sqlstr)
        If myds.Tables("chf020").Rows.Count > 0 Then
            dtpDateS.Value = myds.Tables("chf020").Rows(0).Item("date_2")
            dtpDateE.Value = myds.Tables("chf020").Rows(0).Item("date_2")
        Else
            dtpDateS.Value = TransPara.TransP("userdate")
            dtpDateE.Value = TransPara.TransP("userdate")
        End If
    End Sub

    Private Sub rdbPage_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbPage.CheckedChanged
        If rdbPage.Checked Then
            lblPage.Enabled = True
            txtPrintPageS.Enabled = True
            txtPrintPageE.Enabled = True
        Else
            lblPage.Enabled = False
            txtPrintPageS.Enabled = False
            txtPrintPageE.Enabled = False
        End If
    End Sub

    Private Sub LoadGridFunc()
        Dim intI, intJ As Integer
        Dim sqlstr, qstr, strD, strC As String
        Dim BegDate As String
        Dim begMonth As String

        '找傳票資料(table body record )
        sqlstr = "SELECT a.*, b.ACCNO AS accno2 FROM ACF010 a LEFT OUTER JOIN ACF010 b " & _
                 "ON a.kind = b.kind AND a.accyear = b.accyear AND a.no_2_no = b.no_2_no AND b.item = '1' " & _
                 "WHERE a.ITEM = '9' AND a.DATE_2 >= '" & DateS & "' and a.date_2<='" & FullDate(dtpDateE.Value) & _
                 "' order by a.date_2, a.kind, a.no_2_no "
        mydataset = openmember("", "acf010", sqlstr)

        '找年初結存
        BegDate = FullDate(SYear & "/1/1")   '年度開始日
        sqlstr = "SELECT SUM(b.BALANCE) AS balance FROM " & _
                  " (SELECT BANK, MAX(DATE_2) AS DATE_2 FROM CHF030 " & _
                  "WHERE DATE_2 < '" & BegDate & "'  GROUP BY bank) a " & _
                  "INNER JOIN CHF030 b ON a.BANK = b.BANK AND a.DATE_2 = b.DATE_2"
        myds = openmember("", "chf020", sqlstr)
        If myds.Tables("chf020").Rows.Count > 0 Then
            Upbalance = nz(myds.Tables("chf020").Rows(0).Item("balance"), 0)
        Else
            Upbalance = 0
        End If

        '找上日累計
        sqlstr = "SELECT kind,sum(amt) as amt from acf010 " & _
                 "WHERE DATE_2 < '" & FullDate(dtpDateS.Value) & "' and date_2>='" & BegDate & "' and item='9'" & _
                 " group by kind"
        myds = openmember("", "chf020", sqlstr)
        If myds.Tables("chf020").Rows.Count > 0 Then
            For i As Integer = 0 To myds.Tables("chf020").Rows.Count - 1
                If myds.Tables("chf020").Rows(i).Item("kind") = "1" Then
                    AcuIncome = nz(myds.Tables("chf020").Rows(i).Item("amt"), 0)
                End If
                If myds.Tables("chf020").Rows(i).Item("kind") = "2" Then
                    AcuPay = nz(myds.Tables("chf020").Rows(i).Item("amt"), 0)
                End If
            Next
        Else
            AcuIncome = 0
            AcuPay = 0
        End If
    End Sub

    Private Sub BtnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrint.Click
        Dim intR As Integer = 0  'control record number
        Dim intI, intD, i As Integer
        Dim retstr, strRemark As String
        txtNo.Text = Val(txtNo.Text) - 1
        SYear = GetYear(dtpDateS.Value)
        Dim dayIncome, dayPay As Decimal
        Dim tempDate As Date

        Dim printer = New KPrint
        Dim document As New FPDocument("列印現金出納登記簿")
        document.SetDefaultPageMargin(30, 5, 20, 10)    'left,top,right,botton
        document.DefaultPageSettings.PaperKind = Printing.PaperKind.B4   '420x297
        document.DefaultPageSettings.Landscape = True    '橫印
        document.DefaultFont = New Font("新細明體", 10) '標楷體
        DateS = FullDate(dtpDateS.Value)
        intR = 0

        '找資料
        Call LoadGridFunc()
        dayIncome = 0  '本日收入合計,本日支出合計
        dayPay = 0
        AcuIncome += Upbalance   '目前餘額=acuincome-acupay
        If mydataset.Tables("acf010").Rows.Count > 0 Then
            tempDate = nz(mydataset.Tables("acf010").Rows(0).Item("date_2"), "")  '記錄第一筆之日期
        End If
        Dim page As FPPage
        '以下是符合頁碼範圍所要用到的物件變數
        Dim textUnit As FPText
        Dim textTitle As FPText
        Dim textday As FPText
        Dim textPage As FPText
        Dim grid As FPTable
        '以下是不在頁碼範圍內要用到的物件變數
        Dim ttextUnit As New FPText
        Dim ttextTitle As New FPText
        Dim ttextday As New FPText
        Dim ttextPage As New FPText
        Dim ggrid As New FPTable(0, 17, 324, 222, PageRow, 9)
        '宣告一變數儲存是否符合頁碼範圍
        Dim blnIsBelong As Boolean
        Dim blnIsPrint As Boolean = False '判斷是否有資料頁列印

        For PageCnt As Integer = 1 To 999    '頁次
            txtNo.Text = Val(txtNo.Text) + 1
            blnIsBelong = (rdbAll.Checked = True Or (rdbAll.Checked = False And (Val(txtNo.Text) >= Val(txtPrintPageS.Text) And Val(txtNo.Text) <= Val(txtPrintPageE.Text))))

            '控制列印範圍內的才new
            If blnIsBelong Then
                blnIsPrint = True
                page = New FPPage
                textUnit = New FPText(TransPara.TransP("UnitTitle"), 90, 0)
                textUnit.HAlignment = FPAlignment.Center
                textUnit.Font.Size = 14   '改變文字大小為14點
                textTitle = New FPText("現 金 出 納 登 記 簿", 90, 6)
                textTitle.HAlignment = FPAlignment.Center
                textTitle.Font.Size = 14
                textday = New FPText("中華民國" & FormatNumber(GetYear(dtpDateS.Value), 0) & "年度", 90, 12)
                textday.HAlignment = FPAlignment.Center
                textPage = New FPText("第 " & txtNo.Text & " 頁", 300, 12)
                grid = New FPTable(0, 17, 324, 222, PageRow, 9)  'a3 370,252 '新增表格物件,要列印在座標 (0,17),寬度370,高度252,共有pagerow列9欄,(單位是公厘)
                grid.Font.Size = 10
                'grid.alllinehide   所有格線隱藏
                grid.ColumnStyles(5).HAlignment = StringAlignment.Near
                grid.ColumnStyles(6).HAlignment = StringAlignment.Near
                grid.ColumnStyles(7).HAlignment = StringAlignment.Far
                grid.ColumnStyles(8).HAlignment = StringAlignment.Far
                grid.ColumnStyles(9).HAlignment = StringAlignment.Far
                grid.SetLineColor(Color.Blue)   'table line = red 
                grid.ColumnStyles(1).Width = 18
                grid.ColumnStyles(2).Width = 12
                grid.ColumnStyles(3).Width = 15
                grid.ColumnStyles(4).Width = 15
                grid.ColumnStyles(5).Width = 130
                grid.ColumnStyles(6).Width = 24   '25
                grid.ColumnStyles(7).Width = 36   '45
                grid.ColumnStyles(8).Width = 36   '45
                grid.ColumnStyles(9).Width = 36   '45

                grid.Cells2D(1, 1).ColSpan = 3 '12,3 隱藏
                grid.Cells2D(1, 4).RowSpan = 2
                grid.Cells2D(1, 5).RowSpan = 2
                grid.Cells2D(1, 6).RowSpan = 2
                grid.Cells2D(1, 7).RowSpan = 2
                grid.Cells2D(1, 8).RowSpan = 2
                grid.Cells2D(1, 9).RowSpan = 2
                grid.Texts2D(1, 1).Text = "　　　傳　票　"
                grid.Texts2D(2, 1).Text = "日　期"
                grid.Texts2D(2, 2).Text = "種類"
                grid.Texts2D(2, 3).Text = "號　數"
                grid.Texts2D(1, 4).Text = "科　目"
                grid.Texts2D(1, 5).Text = "　　　　　　　　摘　　　　　　　　　　　要"
                grid.Texts2D(1, 6).Text = " 支票號碼"
                grid.Texts2D(1, 7).Text = "收　　　入　."
                grid.Texts2D(1, 8).Text = "支　　　出　."
                grid.Texts2D(1, 9).Text = "結　　　存　."
            Else
                textUnit = ttextUnit
                textTitle = ttextTitle
                textday = ttextday
                textPage = ttextPage
                grid = ggrid
            End If

            i = 2
            If intR = 0 Then   '上日結存
                If Upbalance <> 0 Or AcuIncome <> 0 Or AcuPay <> 0 Then
                    i += 1
                    If Month(dtpDateS.Value) = 1 And Microsoft.VisualBasic.DateAndTime.Day(dtpDateS.Value) = 1 Then
                        strRemark = "　　上年度轉入"
                    Else
                        strRemark = "　　上日結存"
                    End If
                    grid.Texts2D(i, 5).Text = strRemark
                    grid.Texts2D(i, 7).Text = FormatNumber(AcuIncome, 2)
                    grid.Texts2D(i, 8).Text = FormatNumber(AcuPay, 2)
                    grid.Texts2D(i, 9).Text = FormatNumber(AcuIncome - AcuPay, 2)
                End If
            End If

            With mydataset.Tables("acf010")
                Do While i < PageRow
                    If intR > .Rows.Count - 1 Then
                        If dayIncome + dayPay <> 0 Then
                            i += 1
                            If i > PageRow Then Exit Do
                            strRemark = "　　　　本　　日　　小　　計"
                            grid.Texts2D(i, 5).Text = strRemark
                            grid.Texts2D(i, 7).Text = FormatNumber(dayIncome, 2)
                            grid.Texts2D(i, 8).Text = FormatNumber(dayPay, 2)
                            grid.Texts2D(i, 9).Text = FormatNumber(AcuIncome - AcuPay, 2)
                            dayIncome = 0
                            dayPay = 0
                        End If
                        If AcuIncome + AcuPay <> 0 Then
                            i += 1
                            If i > PageRow Then Exit Do
                            strRemark = "　　　　本　　日　　累　　計"
                            grid.Texts2D(i, 5).Text = strRemark
                            grid.Texts2D(i, 7).Text = FormatNumber(AcuIncome, 2)
                            grid.Texts2D(i, 8).Text = FormatNumber(AcuPay, 2)
                            grid.Texts2D(i, 9).Text = FormatNumber(AcuIncome - AcuPay, 2)
                            AcuIncome = 0
                            AcuPay = 0
                        End If
                        PageCnt = 1000
                        Exit Do
                        Exit For
                    End If   'the end of record 

                    If .Rows(intR).Item("date_2") <> tempDate Then  '日期不同時要印小計
                        If dayIncome + dayPay <> 0 Then
                            i += 1
                            If i > PageRow Then Exit Do
                            strRemark = "　　　　本　　日　　小　　計"
                            grid.Texts2D(i, 5).Text = strRemark
                            grid.Texts2D(i, 7).Text = FormatNumber(dayIncome, 2)
                            grid.Texts2D(i, 8).Text = FormatNumber(dayPay, 2)
                            grid.Texts2D(i, 9).Text = FormatNumber(AcuIncome - AcuPay, 2)
                            dayIncome = 0
                            dayPay = 0
                        End If
                        If AcuIncome + AcuPay <> 0 And AcuIncome + AcuPay <> dayIncome + dayPay Then  'first day do not print 本日累計
                            i += 1
                            If i > PageRow Then Exit Do
                            strRemark = "　　　　本　　日　　累　　計"
                            grid.Texts2D(i, 5).Text = strRemark
                            grid.Texts2D(i, 7).Text = FormatNumber(AcuIncome, 2)
                            grid.Texts2D(i, 8).Text = FormatNumber(AcuPay, 2)
                            grid.Texts2D(i, 9).Text = FormatNumber(AcuIncome - AcuPay, 2)
                        End If
                        tempDate = .Rows(intR).Item("date_2")
                    End If

                    i += 1
                    If i > PageRow Then Exit Do

                    grid.Texts2D(i, 1).Text = ShortDate(.Rows(intR).Item("date_2"))
                    grid.Texts2D(i, 2).Text = IIf(.Rows(intR).Item("kind") = "1", "收", "支")
                    grid.Texts2D(i, 3).Text = Format(.Rows(intR).Item("no_2_no"), 0)
                    If nz(.Rows(intR).Item("accno2"), "") = "" Then
                        grid.Texts2D(i, 4).Text = FormatAccno(nz(.Rows(intR).Item("accno"), ""))
                    Else
                        grid.Texts2D(i, 4).Text = FormatAccno(nz(.Rows(intR).Item("accno2"), ""))
                    End If
                    grid.Texts2D(i, 5).Text = .Rows(intR).Item("remark")
                    grid.Texts2D(i, 6).Text = .Rows(intR).Item("chkno")
                    If .Rows(intR).Item("kind") = "1" Then
                        grid.Texts2D(i, 7).Text = FormatNumber(.Rows(intR).Item("amt"), 2)
                        AcuIncome += .Rows(intR).Item("amt")
                        dayIncome += .Rows(intR).Item("amt")
                    Else
                        grid.Texts2D(i, 8).Text = FormatNumber(.Rows(intR).Item("amt"), 2)
                        AcuPay += .Rows(intR).Item("amt")
                        dayPay += .Rows(intR).Item("amt")
                    End If
                    'grid.Texts2D(i, 9).Text = FormatNumber(AcuIncome - AcuPay, 2)
                    intR += 1
                Loop
            End With

            '控制列印範圍內的才加入
            If blnIsBelong Then
                page.Add(textUnit)
                page.Add(textTitle)
                page.Add(textday)
                page.Add(textPage)
                page.Add(grid)   '加入要列印的表格到列印頁面中
                document.AddPage(page)  '加入要列印的頁面到列印文件中
            End If
        Next
        printer.Document = document
        printer.PrintMode = PrintMode.NormalPrint
        printer.IsAutoShowPrintDialog = True '自動顯示印表機設定對話盒
        If rdoPreview.Checked Then printer.IsAutoShowPrintPreviewDialog = True '自動顯示預覽列印對話盒
        If blnIsPrint Then printer.Print() '開始列印
        Me.Close()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class
