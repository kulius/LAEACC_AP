Imports JBC.Printing
Public Class PAY080
    Dim sqlstr As String
    Dim strBank As String
    Dim SYear As Integer
    Dim DateS As String
    'Dim strAccount, strBankname As String   '銀行名稱
    Dim mydataset, myds, mydsD As DataSet
    Dim Upbalance, balance, AcuIncome, AcuPay, monIncome, monPay, dayIncome, dayPay As Decimal '上日存款餘額,本日收入累計,本日支出累計,本月收入累計,本月支出累計
    Dim PageRow As Integer = 37  '每頁印22行

    Private Sub PAY080_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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
        '找支票資料(table body record )
        sqlstr = "SELECT * FROM chf010 where date_2>='" & DateS & _
                 "' and date_2<='" & FullDate(dtpDateE.Value) & "' and bank='" & strBank & _
                 "' order by bank, date_2, kind, chkno"
        mydataset = openmember("", "chf010", sqlstr)

        '找上日結存
        BegDate = FullDate(SYear & "/1/1")    '年度開始日
        sqlstr = "SELECT b.* FROM (SELECT BANK, MAX(DATE_2) AS DATE_2 " & _
                 "FROM CHF030 WHERE DATE_2 < '" & BegDate & "' and bank='" & strBank & "' group by bank) a" & _
                 " INNER JOIN CHF030 b ON a.BANK = b.BANK AND a.DATE_2 = b.DATE_2"
        myds = openmember("", "chf020", sqlstr)
        If myds.Tables("chf020").Rows.Count > 0 Then
            Upbalance = nz(myds.Tables("chf020").Rows(0).Item("balance"), 0)
        Else
            Upbalance = 0
        End If

        '找本月累計
        sqlstr = "SELECT sum(day_income) as day_income, sum(day_pay) as day_pay from chf030 " & _
                 "WHERE DATE_2 < '" & DateS & "' and date_2>='" & BegDate & "' and bank='" & strBank & "'"
        myds = openmember("", "chf020", sqlstr)
        If myds.Tables("chf020").Rows.Count > 0 Then
            AcuIncome = nz(myds.Tables("chf020").Rows(0).Item("day_income"), 0)
            AcuPay = nz(myds.Tables("chf020").Rows(0).Item("day_pay"), 0)
        Else
            AcuIncome = 0
            AcuPay = 0
        End If
        '本日合計
        dayIncome = 0 : dayPay = 0

        '找本月合計
        begMonth = FullDate(SYear & "/" & Month(dtpDateS.Value) & "/1")
        sqlstr = "SELECT  sum(day_income) as day_income, sum(day_pay) as day_pay from chf030 " & _
                 "WHERE DATE_2 < '" & DateS & "' and date_2>='" & begMonth & "' and bank='" & strBank & "'"
        myds = openmember("", "chf020", sqlstr)
        If myds.Tables("chf020").Rows.Count > 0 Then
            monIncome = nz(myds.Tables("chf020").Rows(0).Item("day_income"), 0)
            monPay = nz(myds.Tables("chf020").Rows(0).Item("day_pay"), 0)
        Else
            monIncome = 0
            monPay = 0
        End If
    End Sub

    Private Sub BtnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrint.Click
        Dim intR As Integer = 0  'control record number
        Dim intI, intD, i As Integer
        Dim retstr, strRemark As String
        txtNo.Text = Val(txtNo.Text) - 1
        SYear = GetYear(dtpDateS.Value)
        Dim tempDay As Date
        Dim tempMonth As Integer

        Dim printer As FPPrinter = FPPrinter.SharedPrinter
        Dim document As New FPDocument("列印存款明細分戶帳 ")
        document.DefaultPageSettings.PaperKind = Printing.PaperKind.B4   'B4:364X257  A3:420x297
        document.DefaultPageSettings.Landscape = True    '橫印
        document.SetDefaultPageMargin(30, 5, 10, 10)    'left,top,right,botton
        document.DefaultFont = New Font("新細明體", 10) '標楷體

        '以下是符合頁碼範圍所要用到的物件變數
        Dim textUnit As FPText
        Dim textTitle As FPText
        Dim textday As FPText
        Dim textPage As FPText
        Dim textNo1 As FPText
        Dim textNo2 As FPText
        Dim textNo3 As FPText
        Dim grid As FPTable
        '以下是不在頁碼範圍內要用到的物件變數
        Dim ttextUnit As New FPText
        Dim ttextTitle As New FPText
        Dim ttextday As New FPText
        Dim ttextPage As New FPText
        Dim ttextNo1 As New FPText
        Dim ttextNo2 As New FPText
        Dim ttextNo3 As New FPText
        Dim ggrid As New FPTable(0, 17, 322, 222, PageRow, 10)   'a3:375,252
        '宣告一變數儲存是否符合頁碼範圍
        Dim blnIsBelong As Boolean
        Dim blnIsPrint As Boolean = False '判斷是否有資料頁列印

        DateS = FullDate(dtpDateS.Value)
        'strRemark = "上年度轉入"

        '由CHF020逐戶列印
        sqlstr = "select * from chf020 where bank>='" & txtSbank.Text & _
                  "' and bank<='" & txtEbank.Text & "' order by bank"
        mydsD = openmember("", "DateTable", sqlstr)
        For intD = 0 To mydsD.Tables("DateTable").Rows.Count - 1
            strBank = mydsD.Tables("DateTable").Rows(intD).Item("bank")  '列印銀行
            intR = 0
            '找資料
            Call LoadGridFunc()
            AcuIncome += Upbalance   '目前餘額=acuincome-acupay
            If mydataset.Tables("chf010").Rows.Count > 0 Then
                tempDay = mydataset.Tables("chf010").Rows(0).Item("date_2")  '記錄第一筆之日期
                tempMonth = Month(mydataset.Tables("chf010").Rows(0).Item("date_2"))  '記錄第一筆之日期
            Else   '沒有資料
                If Not (Month(DateS) = 1 And Microsoft.VisualBasic.DateAndTime.Day(dtpDateS.Value) = 1) Then  '1/1年初
                    GoTo NextFor
                Else '是1/1年初,but結存數=0
                    If Upbalance = 0 Then GoTo nextfor
                End If
            End If

            Dim page As FPPage
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
                    textTitle = New FPText("存 款 明 細 分 戶 帳", 90, 6)
                    textTitle.HAlignment = FPAlignment.Center
                    textTitle.Font.Size = 14
                    textday = New FPText("中華民國" & FormatNumber(GetYear(dtpDateS.Value), 0) & "年度", 90, 12)
                    textday.HAlignment = FPAlignment.Center
                    textday.Font.Size = 11
                    textPage = New FPText("第 " & txtNo.Text & " 頁", 300, 12)
                    textNo1 = New FPText("銀行代號：" & strBank, 0, 2)
                    textNo2 = New FPText("帳　　號：" & mydsD.Tables("DateTable").Rows(intD).Item("account"), 0, 7)
                    textNo3 = New FPText("帳戶名稱：" & mydsD.Tables("DateTable").Rows(intD).Item("bankname"), 0, 12)
                    grid = New FPTable(0, 17, 322, 222, PageRow, 10) '324->318  '新增表格物件x,y,w,h,row,col (單位是公厘)
                    grid.Font.Size = 10   'FPTable(0, 17, 318, 222, PageRow, 10)
                    grid.ColumnStyles(3).HAlignment = StringAlignment.Near
                    grid.ColumnStyles(4).HAlignment = StringAlignment.Near
                    grid.ColumnStyles(5).HAlignment = StringAlignment.Near
                    grid.ColumnStyles(6).HAlignment = StringAlignment.Near
                    grid.ColumnStyles(7).HAlignment = StringAlignment.Far
                    grid.ColumnStyles(8).HAlignment = StringAlignment.Far
                    grid.ColumnStyles(9).HAlignment = StringAlignment.Far
                    grid.SetLineColor(Color.Blue)   'table line = red 
                    grid.ColumnStyles(1).Width = 17
                    grid.ColumnStyles(2).Width = 10
                    grid.ColumnStyles(3).Width = 22
                    grid.ColumnStyles(4).Width = 103
                    grid.ColumnStyles(5).Width = 22  '24
                    grid.ColumnStyles(6).Width = 38  '30
                    grid.ColumnStyles(7).Width = 32  '34
                    grid.ColumnStyles(8).Width = 32  '34 
                    grid.ColumnStyles(9).Width = 32
                    grid.ColumnStyles(10).Width = 14  '16

                    grid.Cells2D(1, 1).ColSpan = 3 '12,3 隱藏
                    grid.Cells2D(1, 4).RowSpan = 2
                    grid.Cells2D(1, 5).RowSpan = 2
                    grid.Cells2D(1, 6).RowSpan = 2
                    grid.Cells2D(1, 7).RowSpan = 2
                    grid.Cells2D(1, 8).RowSpan = 2
                    grid.Cells2D(1, 9).RowSpan = 2
                    grid.Cells2D(1, 10).RowSpan = 2
                    grid.Texts2D(1, 1).Text = "　　傳　　票　"
                    grid.Texts2D(2, 1).Text = "日　期"
                    grid.Texts2D(2, 2).Text = "種類"
                    grid.Texts2D(2, 3).Text = "起 訖 號 數"
                    grid.Texts2D(1, 4).Text = "　　　　　　　　摘　　　　　　　　　　　要"
                    grid.Texts2D(1, 5).Text = " 支票號碼"
                    grid.Texts2D(1, 6).Text = "受  款  人　"
                    grid.Texts2D(1, 7).Text = "存　　　入　 ."
                    grid.Texts2D(1, 8).Text = "支　　　出　 ."
                    grid.Texts2D(1, 9).Text = "餘　　　額　 ."
                    grid.Texts2D(1, 10).Text = "銀行付款日期"

                Else
                    textUnit = ttextUnit
                    textTitle = ttextTitle
                    textday = ttextday
                    textPage = ttextPage
                    textNo1 = ttextNo1
                    textNo2 = ttextNo2
                    textNo3 = ttextNo3
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
                        grid.Texts2D(i, 4).Text = strRemark
                        grid.Texts2D(i, 7).Text = FormatNumber(AcuIncome, 2)
                        grid.Texts2D(i, 8).Text = FormatNumber(AcuPay, 2)
                        grid.Texts2D(i, 9).Text = FormatNumber(AcuIncome - AcuPay, 2)
                    End If
                End If

                With mydataset.Tables("chf010")
                    Do While i < PageRow
                        If intR > .Rows.Count - 1 Then
                            If dayIncome + dayPay <> 0 Then
                                i += 1
                                If i > PageRow Then Exit Do
                                strRemark = "　　　　本　　日　　合　　計"
                                grid.Texts2D(i, 4).Text = strRemark
                                grid.Texts2D(i, 7).Text = FormatNumber(dayIncome, 2)
                                grid.Texts2D(i, 8).Text = FormatNumber(dayPay, 2)
                                grid.Texts2D(i, 9).Text = FormatNumber(AcuIncome - AcuPay, 2)
                                dayIncome = 0
                                dayPay = 0
                            End If
                            If monIncome + monPay <> 0 Then
                                i += 1
                                If i > PageRow Then Exit Do
                                strRemark = "　　　　本　　月　　合　　計"
                                grid.Texts2D(i, 4).Text = strRemark
                                grid.Texts2D(i, 7).Text = FormatNumber(monIncome, 2)
                                grid.Texts2D(i, 8).Text = FormatNumber(monPay, 2)
                                grid.Texts2D(i, 9).Text = FormatNumber(AcuIncome - AcuPay, 2)
                                monIncome = 0
                                monPay = 0
                            End If
                            If AcuIncome + AcuPay <> 0 Then
                                i += 1
                                If i > PageRow Then Exit Do
                                strRemark = "　　　　本　　月　　累　　計"
                                grid.Texts2D(i, 4).Text = strRemark
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

                        If .Rows(intR).Item("date_2") <> tempDay Then  '日期不同時要印小計
                            If dayIncome + dayPay <> 0 Then
                                i += 1
                                If i > PageRow Then Exit Do
                                strRemark = "　　　　本　　日　　合　　計"
                                grid.Texts2D(i, 4).Text = strRemark
                                grid.Texts2D(i, 7).Text = FormatNumber(dayIncome, 2)
                                grid.Texts2D(i, 8).Text = FormatNumber(dayPay, 2)
                                grid.Texts2D(i, 9).Text = FormatNumber(AcuIncome - AcuPay, 2)
                                dayIncome = 0
                                dayPay = 0
                            End If
                            If monIncome + monPay <> 0 Then
                                i += 1
                                If i > PageRow Then Exit Do
                                strRemark = "　　　　本　　月　　合　　計"
                                grid.Texts2D(i, 4).Text = strRemark
                                grid.Texts2D(i, 7).Text = FormatNumber(monIncome, 2)
                                grid.Texts2D(i, 8).Text = FormatNumber(monPay, 2)
                                grid.Texts2D(i, 9).Text = FormatNumber(AcuIncome - AcuPay, 2)
                                If tempMonth <> Month(.Rows(intR).Item("date_2")) Then
                                    monIncome = 0
                                    monPay = 0
                                    tempMonth = Month(.Rows(intR).Item("date_2"))
                                End If
                            End If
                            If AcuIncome + AcuPay <> 0 Then
                                i += 1
                                If i > PageRow Then Exit Do
                                strRemark = "　　　　本　　月　　累　　計"
                                grid.Texts2D(i, 4).Text = strRemark
                                grid.Texts2D(i, 7).Text = FormatNumber(AcuIncome, 2)
                                grid.Texts2D(i, 8).Text = FormatNumber(AcuPay, 2)
                                grid.Texts2D(i, 9).Text = FormatNumber(AcuIncome - AcuPay, 2)
                            End If
                            tempDay = .Rows(intR).Item("date_2")
                        End If

                        i += 1
                        If i > PageRow Then Exit Do

                        grid.Texts2D(i, 1).Text = ShortDate(.Rows(intR).Item("date_2"))
                        grid.Texts2D(i, 2).Text = IIf(.Rows(intR).Item("kind") = "1", "收", "支")
                        If .Rows(intR).Item("start_no") <> .Rows(intR).Item("end_no") Then
                            grid.Texts2D(i, 3).Text = Format(.Rows(intR).Item("start_no"), 0) & "-" & Format(.Rows(intR).Item("end_no"), 0)
                        Else
                            grid.Texts2D(i, 3).Text = Format(.Rows(intR).Item("start_no"), 0)
                        End If
                        grid.Texts2D(i, 4).Text = Mid(.Rows(intR).Item("remark"), 1, 28)   '摘要印28字
                        grid.Texts2D(i, 5).Text = .Rows(intR).Item("chkno")
                        grid.Texts2D(i, 6).Text = Mid(nz(.Rows(intR).Item("chkname"), ""), 1, 10) '受款人只印7字
                        If .Rows(intR).Item("kind") = "1" Then
                            grid.Texts2D(i, 7).Text = FormatNumber(.Rows(intR).Item("amt"), 2)
                            AcuIncome += .Rows(intR).Item("amt")
                            monIncome += .Rows(intR).Item("amt")
                            dayIncome += .Rows(intR).Item("amt")
                        Else
                            grid.Texts2D(i, 8).Text = FormatNumber(.Rows(intR).Item("amt"), 2)
                            AcuPay += .Rows(intR).Item("amt")
                            monPay += .Rows(intR).Item("amt")
                            dayPay += .Rows(intR).Item("amt")
                        End If
                        grid.Texts2D(i, 9).Text = FormatNumber(AcuIncome - AcuPay, 2)

                        intR += 1
                    Loop
                End With

                '控制列印範圍內的才加入
                If blnIsBelong Then
                    page.Add(textUnit)       '加入要列印的文字到列印頁面中
                    page.Add(textTitle)
                    page.Add(textday)
                    page.Add(textPage)
                    page.Add(textNo1)
                    page.Add(textNo2)
                    page.Add(textNo3)
                    page.Add(grid)          '加入要列印的表格到列印頁面中
                    document.AddPage(page)  '加入要列印的頁面到列印文件中
                End If
            Next
NextFor: Next

        printer.Document = document
        printer.PrintMode = PrintMode.NormalPrint
        printer.IsAutoShowPrintDialog = True '自動顯示印表機設定對話盒
        If rdoPreview.Checked Then printer.IsAutoShowPrintPreviewDialog = True '自動顯示預覽列印對話盒
        If blnIsPrint Then printer.Print() '開始列印
        Me.Close()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        mydataset = Nothing
        myds = Nothing
        Me.Close()
    End Sub
End Class
