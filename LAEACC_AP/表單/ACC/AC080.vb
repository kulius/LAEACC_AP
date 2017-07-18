Imports JBC.Printing
Public Class AC080
    Dim confid, confunit As String, LoadAfter, Dirty As Boolean
    Dim sqlstr As String
    Dim SYear As Integer
    Dim DateS As String
    Dim mydataset, myds, mydsD As DataSet
    Dim UserId, UserName, UserUnit As String
    Dim income2, income3, pay2, pay3, balance2, balance3 As Decimal  '11102 & 11103 當日存款收支及餘額
    Dim Upbalance2, Upbalance3 As Decimal   '11102 & 11103 上日存款收支及餘額
    Dim sNo1, sNo2, sNo3, eNo1, eNo2, eNo3, recno1, recno2, recno3 As Integer '各傳票起訖號
    Dim SumDeamt1, SumDeamt2, SumDeamt3, SumCramt1, SumCramt2, SumCramt3 As Decimal

    Private Sub AC080_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SYear = GetYear(TransPara.TransP("userdate"))
        dtpDateS.Value = TransPara.TransP("userdate")
        dtpDateE.Value = TransPara.TransP("userdate")
        txtNoD.Text = QueryNO(SYear, "D") + 1                 '\accservice\service1.asmx
        If Val(txtNoD.Text) = 1 Then RequireNO(mastconn, SYear, "D")
    End Sub

    Private Sub dtpDateS_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpDateS.ValueChanged
        SYear = GetYear(dtpDateS.Value)
        txtNoD.Text = QueryNO(SYear, "D") + 1
    End Sub

    Private Sub LoadGridFunc()
        Dim intI, intJ As Integer
        Dim sqlstr, qstr, strD, strC As String

        sqlstr = "SELECT a.*,b.accname as accname " & _
                 "FROM acf070 a INNER JOIN ACCNAME b ON a.ACCNO = b.ACCNO " & _
                 " WHERE a.date_2 ='" & DateS & "'" & _
                 " order by a.accno"
        mydataset = openmember("", "ACF070", sqlstr)

        '找收支傳票起訖號
        sqlstr = "SELECT kind, min(no_2_no) as sno, max(no_2_no) as eno, count(*) as recno from acf010 " & _
                 " where date_2='" & DateS & "' and item='9' GROUP BY kind "
        myds = openmember("", "chf020", sqlstr)
        '清統計值
        sNo1 = 0 : eNo1 = 0 : recno1 = 0
        sNo2 = 0 : eNo2 = 0 : recno2 = 0
        sNo3 = 0 : eNo3 = 0 : recno3 = 0
        SumDeamt1 = 0 : SumDeamt2 = 0 : SumDeamt3 = 0
        SumCramt1 = 0 : SumCramt2 = 0 : SumCramt3 = 0
        balance2 = 0 : balance3 = 0 : Upbalance2 = 0 : Upbalance3 = 0
        pay2 = 0 : pay3 = 0 : income2 = 0 : income3 = 0
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
        '找轉帳傳票起訖號
        sqlstr = "SELECT min(no_2_no) as sno, max(no_2_no) as eno, count(*) as recno from acf010 " & _
         " where date_2='" & DateS & "' and kind='3' and item='1'"
        myds = openmember("", "chf020", sqlstr)

        If myds.Tables("chf020").Rows.Count > 0 Then
            sNo3 = nz(myds.Tables("chf020").Rows(0).Item("sno"), 0)
            eNo3 = nz(myds.Tables("chf020").Rows(0).Item("eno"), 0)
            recno3 = nz(myds.Tables("chf020").Rows(0).Item("recno"), 0)
        End If


        '找上日結存
        sqlstr = "SELECT accno, SUM(BALANCE) AS balance FROM " & _
                 "(SELECT b.*, c.accno as accno FROM (SELECT BANK, MAX(DATE_2) AS DATE_2 " & _
                 "FROM CHF030 WHERE DATE_2 < '" & DateS & "' group by bank) a " & _
                 " INNER JOIN CHF030 b ON a.BANK = b.BANK AND a.DATE_2 = b.DATE_2 " & _
                 " left outer join chf020 c on a.bank = c.bank )  derivedtbl " & _
                 "GROUP BY  accno"
        myds = openmember("", "chf020", sqlstr)

        For intI = 0 To myds.Tables("chf020").Rows.Count - 1
            If nz(myds.Tables("chf020").Rows(intI).Item("accno"), "") = "11102" Then
                Upbalance2 = nz(myds.Tables("chf020").Rows(intI).Item("balance"), 0)
            End If
            If nz(myds.Tables("chf020").Rows(intI).Item("accno"), "") = "11103" Then
                Upbalance3 = nz(myds.Tables("chf020").Rows(intI).Item("balance"), 0)
            End If
        Next
        '找本日結存  ()
        'sqlstr = "SELECT accno,  SUM(BALANCE) AS balance FROM " & _
        '         "(SELECT b.*, c.accno FROM (SELECT BANK, MAX(DATE_2) AS DATE_2 " & _
        '         "FROM CHF030 WHERE DATE_2 <= '" & DateS & "' group by bank)  a " & _
        '         " INNER JOIN CHF030 b ON A.BANK = b.BANK AND a.DATE_2 = b.DATE_2 " & _
        '         " left outer join chf020 c on a.bank = c.bank ) derivedtbl " & _
        '         "GROUP BY  accno"
        'myds = ws1.openmember("", "chf020", sqlstr)

        'For intI = 0 To myds.Tables("chf020").Rows.Count - 1
        '    If nz(myds.Tables("chf020").Rows(intI).Item("accno"), "") = "11102" Then
        '        balance2 = nz(myds.Tables("chf020").Rows(intI).Item("balance"), 0)
        '    End If
        '    If nz(myds.Tables("chf020").Rows(intI).Item("accno"), "") = "11103" Then
        '        balance3 = nz(myds.Tables("chf020").Rows(intI).Item("balance"), 0)
        '    End If
        'Next
        '找當日結存收支
        sqlstr = "SELECT  SUM(deamt1) as deamt1, SUM(deamt2) as deamt2, SUM(cramt1) as cramt1, SUM(cramt2) as cramt2 " & _
                 "from acf070 WHERE date_2 = '" & DateS & "' and accno>'11103'"
        myds = openmember("", "chf020", sqlstr)

        For intI = 0 To myds.Tables("chf020").Rows.Count - 1
            If myds.Tables("chf020").Rows.Count > 0 Then
                income2 = nz(myds.Tables("chf020").Rows(0).Item("cramt1"), 0)
                pay2 = nz(myds.Tables("chf020").Rows(intI).Item("deamt1"), 0)
                income3 = nz(myds.Tables("chf020").Rows(0).Item("cramt2"), 0)
                pay3 = nz(myds.Tables("chf020").Rows(intI).Item("deamt2"), 0)
            End If
        Next
        '從acf070總帳金額 扣除acf070其他科目收付額=銀行轉帳額
        With mydataset.Tables("acf070")
            For intJ = 0 To .Rows.Count - 1
                If .Rows(intJ).Item("accno") = "11102" Then
                    .Rows(intJ).Item("deamt1") -= income2 '銀行科目扣除acf070其他科目收付額=銀行轉帳額
                    .Rows(intJ).Item("cramt1") -= pay2
                    income2 = .Rows(intJ).Item("deamt1")
                    pay2 = .Rows(intJ).Item("cramt1")
                    .Rows(intJ).Item("deamt1") = pay2  '銀行科目本身收付借貸要相反
                    .Rows(intJ).Item("cramt1") = income2
                Else
                    If .Rows(intJ).Item("accno") = "11103" Then
                        .Rows(intJ).Item("deamt2") -= income3
                        .Rows(intJ).Item("cramt2") -= pay3
                        income3 = .Rows(intJ).Item("deamt2")
                        pay3 = .Rows(intJ).Item("cramt2")
                        .Rows(intJ).Item("deamt2") = pay3
                        .Rows(intJ).Item("cramt2") = income3
                    Else
                        Exit For
                    End If
                End If
            Next
        End With
        '補本日合計,昨日結存,本日結存,合計
    End Sub

    Private Sub BtnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrint.Click
        Dim intR As Integer = 0  'control record number
        Dim intI, intD As Integer
        Dim intYY, intMM, intDD As Integer
        Dim retstr As String
        Dim pagerow As Integer = 27   '每頁27行
        txtNoD.Text = Val(txtNoD.Text) - 1
        Dim printer As FPPrinter = FPPrinter.SharedPrinter
        Dim document As New FPDocument("列印日計表")
        document.SetDefaultPageMargin(16, 5, 0, 10)    'left,top,right,botton
        document.DefaultPageSettings.Landscape = True    '橫印
        document.DefaultFont = New Font("新細明體", 10) '標楷體

        '取得所有交易日,由交易日逐日列印
        sqlstr = "select date_2 from acf070 where date_2>='" & FullDate(dtpDateS.Value) & _
                 "' and date_2<='" & FullDate(dtpDateE.Value) & _
                 "' group by date_2 order by date_2"
        mydsD = openmember("", "DateTable", sqlstr)
        If mydsD.Tables("DateTable").Rows.Count = 0 Then
            MsgBox("無日計表資料")
            Exit Sub
        End If
        For intD = 0 To mydsD.Tables("DateTable").Rows.Count - 1
            intR = 0
            DateS = FullDate(mydsD.Tables("DateTable").Rows(intD).Item("date_2"))
            Call LoadGridFunc()
            intYY = Val(Mid(DateS, 1, 4)) - 1911
            If Mid(DateS, 5, 5) = "/2/29" Or Mid(DateS, 5, 6) = "/02/29" Then
                intMM = 2
                intDD = 29
            Else
                intMM = Month(DateS)
                intDD = Microsoft.VisualBasic.DateAndTime.Day(DateS)
            End If

            'document.DefaultPageSettings.PaperKind = Printing.PaperKind.A3
            For PageCnt As Integer = 1 To 99    '頁次

                '新增列印頁面,一個列印頁面可以包含多個列印物件,譬如文字,表格,影像等等
                Dim page As New FPPage

                '新增文字物件,要列印在座標 (0,0) (col,row) (單位是公厘)
                Dim textUnit As New FPText(TransPara.TransP("UnitTitle"), 90, 0)
                textUnit.HAlignment = FPAlignment.Center
                textUnit.Font.Size = 12   '改變文字大小
                page.Add(textUnit)
                Dim textTitle As New FPText("日    計    表", 90, 5)
                textTitle.HAlignment = FPAlignment.Center
                textTitle.Font.Size = 12   '改變文字大小
                page.Add(textTitle)
                Dim textday As New FPText("中華民國" & FormatNumber(intYY, "000") & "年" & intMM & _
                    "月" & intDD & "日", 90, 10)
                textday.HAlignment = FPAlignment.Center
                page.Add(textday)
                txtNoD.Text = Val(txtNoD.Text) + 1
                Dim textPage As New FPText("第" & txtNoD.Text & "頁", 245, 10)
                page.Add(textPage)
                Dim textNo1 As New FPText("收入傳票" & Format(sNo1, "00000") & " ～ " & Format(eNo1, "00000"), 0, 0)
                page.Add(textNo1)
                Dim textNo2 As New FPText("支出傳票" & Format(sNo2, "00000") & " ～ " & Format(eNo2, "00000"), 0, 5)
                page.Add(textNo2)
                Dim textNo3 As New FPText("轉帳傳票" & Format(sNo3, "00000") & " ～ " & Format(eNo3, "00000"), 0, 10)
                page.Add(textNo3)

                '新增表格物件,要列印在座標 (15,10),寬度280,高度150,共有22列7欄,(單位是公厘)
                '每欄ㄉ寬度和每列的高度,預設會使用自動平均後的值
                'FPTable 和 FPGrid 這兩個物件的差異在 FPTable內含 FPCell物件,可以設定跨列和跨欄,但FPGrid則無
                Dim grid As New FPTable(0, 15, 263, 162, pagerow, 9)   'x,y,w,h
                grid.SetLineColor(Color.Blue)   'table line color 
                grid.Font.Size = 9
                grid.ColumnStyles(1).HAlignment = StringAlignment.Far
                grid.ColumnStyles(2).HAlignment = StringAlignment.Far
                grid.ColumnStyles(3).HAlignment = StringAlignment.Far
                grid.ColumnStyles(4).HAlignment = StringAlignment.Far
                grid.ColumnStyles(5).HAlignment = StringAlignment.Near
                grid.ColumnStyles(6).HAlignment = StringAlignment.Far
                grid.ColumnStyles(7).HAlignment = StringAlignment.Far
                grid.ColumnStyles(8).HAlignment = StringAlignment.Far
                grid.ColumnStyles(9).HAlignment = StringAlignment.Far
                'grid.RowStyles(2).VAlignment = StringAlignment.Far   'row下靠
                'grid.Texts2D(i, 2).HAlignment = FPAlignment.Far 'column  右靠

                'grid.SetLineColor(Color.Red)   'table line = red 
                grid.ColumnStyles(1).Width = 30
                grid.ColumnStyles(2).Width = 25
                grid.ColumnStyles(3).Width = 27
                grid.ColumnStyles(4).Width = 27
                grid.ColumnStyles(5).Width = 45
                grid.ColumnStyles(6).Width = 27
                grid.ColumnStyles(7).Width = 27
                grid.ColumnStyles(8).Width = 25
                grid.ColumnStyles(9).Width = 30

                grid.Cells2D(1, 1).ColSpan = 4 '1,2,3,4 隱藏
                grid.Cells2D(1, 6).ColSpan = 4 '6,7,8,9 隱藏
                grid.Cells2D(1, 5).RowSpan = 2 '1,2row 隱藏
                grid.Texts2D(1, 1).Text = "收　　　　　　　　　　　　　　　　方　　　　　　　."
                grid.Texts2D(1, 6).Text = "付　　　　　　　　　　　　　　　　方　　　　　　　."
                grid.Texts2D(2, 1).Text = "合　　計"
                grid.Texts2D(2, 2).Text = "轉　　帳"
                grid.Texts2D(2, 3).Text = "專戶存款"
                grid.Texts2D(2, 4).Text = "銀行存款"
                grid.Texts2D(1, 5).Text = "　會計科目及符號"
                grid.Texts2D(2, 6).Text = "銀行存款"
                grid.Texts2D(2, 7).Text = "專戶存款"
                grid.Texts2D(2, 8).Text = "轉　　帳"
                grid.Texts2D(2, 9).Text = "合　　計"

                With mydataset.Tables("acf070")
                    For i As Integer = 3 To pagerow
                        If intR > .Rows.Count - 1 Then
                            If i > pagerow - 3 Then 'must add 4 line 合　計
                                Exit For
                            End If
                            grid.Texts2D(i, 1).Text = FormatNumber(SumCramt1 + SumCramt2 + SumCramt3, 2)
                            grid.Texts2D(i, 2).Text = FormatNumber(SumCramt3, 2)
                            grid.Texts2D(i, 3).Text = FormatNumber(SumCramt2, 2)
                            grid.Texts2D(i, 4).Text = FormatNumber(SumCramt1, 2)
                            grid.Texts2D(i, 5).Text = "　本　日　合　計"
                            grid.Texts2D(i, 6).Text = FormatNumber(SumDeamt1, 2)
                            grid.Texts2D(i, 7).Text = FormatNumber(SumDeamt2, 2)
                            grid.Texts2D(i, 8).Text = FormatNumber(SumDeamt3, 2)
                            grid.Texts2D(i, 9).Text = FormatNumber(SumDeamt1 + SumDeamt2 + SumDeamt3, 2)
                            i += 1
                            grid.Texts2D(i, 1).Text = FormatNumber(Upbalance2 + Upbalance3, 2)
                            'grid.Texts2D(i, 2).Text = ""
                            grid.Texts2D(i, 3).Text = FormatNumber(Upbalance3, 2)
                            grid.Texts2D(i, 4).Text = FormatNumber(Upbalance2, 2)
                            grid.Texts2D(i, 5).Text = "　昨　日　結　存"
                            grid.Texts2D(i, 6).Text = ""
                            grid.Texts2D(i, 7).Text = ""
                            'grid.Texts2D(i, 8).Text = ""
                            grid.Texts2D(i, 9).Text = ""
                            i += 1
                            grid.Texts2D(i, 1).Text = ""
                            'grid.Texts2D(i, 2).Text = ""
                            grid.Texts2D(i, 3).Text = ""
                            grid.Texts2D(i, 4).Text = ""
                            grid.Texts2D(i, 5).Text = "　本　日　結　存"
                            balance2 = Upbalance2 + SumCramt1 - SumDeamt1  '由上日結存+本日共收-本日共支
                            balance3 = Upbalance3 + SumCramt2 - SumDeamt2  '由上日結存+本日共收-本日共支
                            grid.Texts2D(i, 6).Text = FormatNumber(balance2, 2)
                            grid.Texts2D(i, 7).Text = FormatNumber(balance3, 2)
                            grid.Texts2D(i, 8).Text = ""
                            grid.Texts2D(i, 9).Text = FormatNumber(balance2 + balance3, 2)
                            SumCramt1 += Upbalance2
                            SumCramt2 += Upbalance3
                            SumDeamt1 += balance2
                            SumDeamt2 += balance3
                            i += 1
                            grid.Texts2D(i, 1).Text = FormatNumber(SumCramt1 + SumCramt2 + SumCramt3, 2)
                            grid.Texts2D(i, 2).Text = FormatNumber(SumCramt3, 2)
                            grid.Texts2D(i, 3).Text = FormatNumber(SumCramt2, 2)
                            grid.Texts2D(i, 4).Text = FormatNumber(SumCramt1, 2)
                            grid.Texts2D(i, 5).Text = "　合 　　　     計"
                            grid.Texts2D(i, 6).Text = FormatNumber(SumDeamt1, 2)
                            grid.Texts2D(i, 7).Text = FormatNumber(SumDeamt2, 2)
                            grid.Texts2D(i, 8).Text = FormatNumber(SumDeamt3, 2)
                            grid.Texts2D(i, 9).Text = FormatNumber(SumDeamt1 + SumDeamt2 + SumDeamt3, 2)
                            PageCnt = 999    'EXIT FOR THE PAGE 
                            Exit For
                        End If

                        If .Rows(intR).Item("accno") = "11102" And .Rows(intR).Item("deamt1") + .Rows(intR).Item("deamt2") + .Rows(intR).Item("deamt3") + .Rows(intR).Item("cramt1") + .Rows(intR).Item("cramt2") + .Rows(intR).Item("cramt3") = 0 Then
                            intR += 1   '銀行科目沒有金額表示沒有銀行轉帳
                            i -= 1
                            GoTo EndNextBody
                        End If
                        If .Rows(intR).Item("accno") = "11103" And .Rows(intR).Item("cramt1") + .Rows(intR).Item("cramt2") + .Rows(intR).Item("cramt3") + .Rows(intR).Item("deamt1") + .Rows(intR).Item("deamt2") + .Rows(intR).Item("deamt3") = 0 Then
                            intR += 1
                            i -= 1
                            GoTo EndNextBody
                        End If
                        grid.Texts2D(i, 1).Text = FormatNumber(.Rows(intR).Item("cramt1") + .Rows(intR).Item("cramt2") + .Rows(intR).Item("cramt3"), 2)
                        grid.Texts2D(i, 2).Text = FormatNumber(.Rows(intR).Item("cramt3"), 2)
                        grid.Texts2D(i, 3).Text = FormatNumber(.Rows(intR).Item("cramt2"), 2)
                        grid.Texts2D(i, 4).Text = FormatNumber(.Rows(intR).Item("cramt1"), 2)
                        grid.Texts2D(i, 5).Text = FormatAccno(.Rows(intR).Item("accno")) & nz(.Rows(intR).Item("accname"), "")
                        grid.Texts2D(i, 6).Text = FormatNumber(.Rows(intR).Item("deamt1"), 2)
                        grid.Texts2D(i, 7).Text = FormatNumber(.Rows(intR).Item("deamt2"), 2)
                        grid.Texts2D(i, 8).Text = FormatNumber(.Rows(intR).Item("deamt3"), 2)
                        grid.Texts2D(i, 9).Text = FormatNumber(.Rows(intR).Item("deamt1") + .Rows(intR).Item("deamt2") + .Rows(intR).Item("deamt3"), 2)
                        SumDeamt1 += .Rows(intR).Item("deamt1")
                        SumDeamt2 += .Rows(intR).Item("deamt2")
                        SumDeamt3 += .Rows(intR).Item("deamt3")
                        SumCramt1 += .Rows(intR).Item("cramt1")
                        SumCramt2 += .Rows(intR).Item("cramt2")
                        SumCramt3 += .Rows(intR).Item("cramt3")
                        intR += 1
EndNextBody:        Next
                End With
                'retstr = "主辦主計人員" & Space(60) & "覆核" & Space(60) & "過帳" & Space(60) & "製表"
                retstr = "製表" & Space(60) & "過帳" & Space(60) & "覆核" & Space(60) & "主辦主計人員"
                Dim textBotton As New FPText(retstr, 0, 180)
                page.Add(textBotton)

                page.Add(grid) '加入要列印的表格到列印頁面中
                document.AddPage(page) '加入要列印的頁面到列印文件中
            Next
        Next

        '記錄號數控制檔,kind='D'之已印頁次
        sqlstr = "update acfno set cont_no=" & txtNoD.Text & " where accyear=" & SYear & " and kind='D'"
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then
            MsgBox("更新號數控制檔錯誤" & sqlstr)
        End If

        printer.Document = document '將要列印的文件送到印表機
        printer.PrintMode = PrintMode.NormalPrint
        'printer.IsAutoShowPageSetupDialog = True   '設定紙張規格及直橫式
        printer.IsAutoShowPrintDialog = True '自動顯示印表機設定對話盒
        If rdoPreview.Checked Then printer.IsAutoShowPrintPreviewDialog = True '自動顯示預覽列印對話盒
        printer.Print() '開始列印
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub txtNoD_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNoD.LostFocus
        If (Not IsNumeric(sender.text) Or ValComa(sender.text) < 0) And Trim(sender.text) <> "" Then
            MsgBox("請輸入正數字")
            sender.focus()
        End If
    End Sub
End Class
