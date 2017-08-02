Imports JBC.Printing
Public Class ACBK120
    Dim confid, confunit As String, LoadAfter, Dirty As Boolean
    Dim sqlstr As String
    Dim strAccno As String
    Dim SYear As Integer
    Dim SDate, EDate As String
    Dim intCol As Integer = 19   '宣告欄位數為9欄 (1個收入欄及9個費用別)  假若要更動欄位數則要更改intcol & grid.ColumnStyles(n).Width
    '宣告陣列最大值=19 方便日後程式更動欄位數 because can't dim 變數 array
    Dim AryAccno(19) As String  '宣告每件工程之帳簿有1個收入欄及19個費用別 accno科目代號
    Dim Aryaccname(19) As String  '宣告每件工程之帳簿有1個收入欄及19個費用別 accname科目名稱
    Dim AryBgAmt(19) As Decimal   '宣告每件工程之帳簿有1個收入欄及19個費用別 預算數 
    Dim AryAcuamt(19) As Integer   '定義各欄年累計數
    Dim AryMonamt(19) As Integer   '定義各欄月累計數
    Dim mydataset, myds, mydsD, mydsAccno As DataSet
    Dim PageRow As Integer = 30  '每頁印30行
    Dim blnDoubleLine As Boolean = False    '控制9欄費用別 or 18欄費用別(each row must put 2 lines )

    Private Sub ACBK120_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '找過帳日期
        Dim sqlstr As String
        sqlstr = "SELECT max(date_2) as date_2  from acf010 where books='Y'"
        myds = openmember("", "acf010s", sqlstr)
        If myds.Tables("acf010s").Rows.Count > 0 Then
            nudEmonth.Value = Month(myds.Tables("acf010s").Rows(0).Item("date_2"))
            nudSmonth.Value = nudEmonth.Value
            nudSyear.Value = GetYear(myds.Tables("acf010s").Rows(0).Item("date_2"))
            nudEyear.Value = nudSyear.Value
        End If
        vxtSAccno.Text = "21302"    '起值
        vxtEAccno.Text = "213029"   '迄值
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

    Private Sub BtnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrint.Click
        BtnPrint.Enabled = False
        Dim intR As Integer = 0  'control record number
        Dim intI, intD, i As Integer
        Dim intColumn As Integer   '該8級科目置於第幾欄
        Dim AcuTot, MonTot, TotAmt, intAmt As Decimal
        AcuTot = 0 : MonTot = 0 : TotAmt = 0
        Dim retstr, strRemark, strDC As String
        'Dim strName4, strName7 As String
        txtNo.Text = Val(txtNo.Text) - 1
        SYear = nudSyear.Value
        SDate = FullDate(Format(nudSyear.Value, "0000") & "/" & Format(nudSmonth.Value, "00") & "/1") '起印日
        EDate = FullDate(Format(nudEyear.Value, "0000") & "/" & Format(nudEmonth.Value, "00") & "/" & DateTime.DaysInMonth(nudEyear.Value + 1911, nudEmonth.Value)) '訖印日
        Dim up As String = Format(nudSmonth.Value - 1, "00")  '上月
        Dim mm As String = Format(nudEmonth.Value, "00") '本月
        Dim Saccno As String = GetAccno(vxtSAccno.Text)  '起印科目
        Dim Eaccno As String = GetAccno(vxtEAccno.Text)  '訖印科目
        Dim tempMonth As Integer = 0

        Dim printer = New KPrint
        Dim document As New FPDocument("列印代收款專案計畫明細帳")
        document.DefaultPageSettings.PaperKind = Printing.PaperKind.B4   'A3:420x297  B4:364X257
        document.DefaultPageSettings.Landscape = True    '橫印
        document.SetDefaultPageMargin(30, 0, 10, 10)    'left,top,right,botton(30, 5, 10, 10)
        document.DefaultFont = New Font("新細明體", 11) '標楷體

        '以下是符合頁碼範圍所要用到的物件變數
        Dim textUnit As FPText
        Dim textTitle As FPText
        Dim textyear As FPText
        Dim textPage As FPText
        Dim textName7 As FPText
        Dim grid As FPTable
        '以下是不在頁碼範圍內要用到的物件變數
        Dim ttextUnit As New FPText
        Dim ttextTitle As New FPText
        Dim ttextyear As New FPText
        Dim ttextPage As New FPText
        Dim ttextName7 As New FPText
        Dim ggrid As New FPTable(0, 17, 314, 222, PageRow, 10)   'just dim 不須列印之grid, the value do not use 
        '宣告一變數儲存是否符合頁碼範圍
        Dim blnIsBelong As Boolean
        Dim blnIsPrint As Boolean = True  '記錄是否有資料頁列印
        Dim strBg As String

        'strRemark = "上年度轉入"

        '由acf050逐科目列印 取最明細科目(8級科目必須與7級科目合併列印)  只取ACCNAME.BELONG='C'是補助科目
        Dim sqlstr2 As String
        If up = "00" Then
            sqlstr2 = " a.beg_debit as deamt, a.beg_credit as cramt"
        Else
            sqlstr2 = " a.deamt" & up & " as deamt, a.cramt" & up & " as cramt"
        End If
        sqlstr2 += ",  a.deamt" & mm & " as deamtmm, a.cramt" & mm & " as cramtmm"
        sqlstr = "SELECT a.accno, c.accname, " & sqlstr2 & " FROM " & _
                 " (SELECT * FROM acf050 WHERE accyear =" & SYear & " AND LEN(ACCNO) < 17 AND LEN(ACCNO)>9 AND " & _
                 " accno BETWEEN '" & Saccno & "' AND '" & Eaccno & "') a " & _
                 " LEFT OUTER JOIN ACCNAME c ON a.ACCNO = c.ACCNO " & _
                 " WHERE c.belong='C'" & _
                 " ORDER BY  a.ACCNO"
        mydsD = openmember("", "DataAccno", sqlstr)
        For intD = 0 To mydsD.Tables("DataAccno").Rows.Count - 1
            If rdbPage.Checked Then  '頁次大於控制列印範圍時就結束
                If Val(txtNo.Text) > Val(txtPrintPageE.Text) Then Exit For
            End If

            strAccno = RTrim(nz(mydsD.Tables("DataAccno").Rows(intD).Item("accno"), "")) '列印科目
            '上月累計or上年度轉入
            'AryAcuamt(0) = nz(mydsD.Tables("DataAccno").Rows(intD).Item("deamt"), 0) - nz(mydsD.Tables("DataAccno").Rows(intD).Item("cramt"), 0)
            If nudSmonth.Value = 1 Then  '由年初開始列印
                'AryMonamt(0) = AryAcuamt(0)
            Else
                AryMonamt(0) = 0
                If nz(mydsD.Tables("DataAccno").Rows(intD).Item("deamt"), 0) = nz(mydsD.Tables("DataAccno").Rows(intD).Item("deamtmm"), 0) And _
                   nz(mydsD.Tables("DataAccno").Rows(intD).Item("cramt"), 0) = nz(mydsD.Tables("DataAccno").Rows(intD).Item("cramtmm"), 0) Then
                    GoTo nextfor    '本月無異動傳票不印
                End If
            End If

            '找7級科目有幾個8級科目 put to array  預算數由bgf010找  不分年度(有些工程是跨年度)
            strBg = ", b.bg1+b.bg2+b.bg3+b.bg4+b.bg5+b.up1+b.up2+b.up3+b.up4+b.up5 as bgtot"   '找各費用之預算數
            sqlstr = "SELECT c.accno, c.accname," & sqlstr2 & strBg & " FROM " & _
                     " (SELECT * FROM accname WHERE left(accno,16)='" & strAccno & "') c " & _
                     " LEFT OUTER JOIN acf050 a ON a.ACCNO = c.ACCNO and a.accyear=" & SYear & _
                     " LEFT OUTER JOIN bgf010 b ON c.ACCNO = b.ACCNO " & _
                     " ORDER BY  c.ACCNO"
            mydsAccno = openmember("", "TitleAccno", sqlstr)
            If mydsAccno.Tables("TitleAccno").Rows.Count > 10 Then   '大於10欄要列印雙行
                blnDoubleLine = True
            Else
                blnDoubleLine = False
            End If
            Call Clear_Array()  '清欄位抬頭陣列資料
            '將期初數&科目&科目名稱等資料 put to 欄位抬頭陣列
            intColumn = 0    '置放之欄位
            With mydsAccno.Tables("titleaccno")
                For intI = 0 To .Rows.Count - 1
                    intColumn = intI
                    If Len(nz(.Rows(intI).Item("accno"), "")) < 17 Then  '七級科目至array(0)置收入欄
                        intColumn = 0
                    End If
                    If intColumn > intCol Then
                        intColumn = intCol   '超過最大陣列值則置最後一個
                    End If
                    AryAccno(intColumn) = .Rows(intI).Item("accno")
                    Aryaccname(intColumn) = IIf(Aryaccname(intColumn) = "", .Rows(intI).Item("accname"), Aryaccname(intColumn) & "及其他")
                    AryBgAmt(intColumn) = AryBgAmt(intColumn) + nz(.Rows(intI).Item("bgtot"), 0)
                    AryAcuamt(intColumn) = AryAcuamt(intColumn) + nz(.Rows(intI).Item("deamt"), 0) - nz(.Rows(intI).Item("cramt"), 0)
                Next
            End With

            '找起印日前之收入數
            sqlstr = "SELECT a.dc, sum(a.amt) as amt FROM " & _
                     " (select * from acf020 where accno='" & strAccno & "') a " & _
                     " left outer join acf010 b on a.accyear=b.accyear and a.kind=b.kind " & _
                     " and a.no_2_no=b.no_2_no and a.seq=b.seq and b.item='1' " & _
                     " where b.date_2 < '" & SDate & "' group by a.dc"
            AryAcuamt(0) = 0
            mydsAccno = openmember("", "temp", sqlstr)
            For intI = 0 To mydsAccno.Tables("temp").Rows.Count - 1
                If mydsAccno.Tables("temp").Rows(intI).Item("dc") = "1" Then   'debit=退還 
                    AryAcuamt(0) = AryAcuamt(0) - mydsAccno.Tables("temp").Rows(intI).Item("amt")
                Else                                                           'credit=收入
                    AryAcuamt(0) = AryAcuamt(0) + mydsAccno.Tables("temp").Rows(intI).Item("amt")
                End If
            Next

            If nudSmonth.Value = 1 Then  '由年初開始列印 則元月累計數=上年度轉入數
                For intI = 0 To intCol
                    AryMonamt(intI) = AryAcuamt(intI)
                Next
            End If

            intR = 0     '該7級科目所有傳票資料
            '找acf020傳票資料
            sqlstr = "SELECT a.accyear, a.accno, b.date_2 as date_2, a.kind, a.no_2_no, a.seq, " & _
                     " a.item, a.remark, a.amt, a.cotn_code, a.dc FROM " & _
                     " (select * from acf020 where accyear>=" & SYear & " and left(accno,16)='" & strAccno & "') a " & _
                     " left outer join acf010 b on a.accyear=b.accyear and a.kind=b.kind " & _
                     " and a.no_2_no=b.no_2_no and a.seq=b.seq and b.item='1' " & _
                     " where b.date_2 between '" & SDate & "' and '" & EDate & "'" & _
                     " order by date_2, a.kind, a.no_2_no, a.seq, a.item "
            mydataset = openmember("", "acf020", sqlstr)

            If mydataset.Tables("acf020").Rows.Count > 0 Then
                tempMonth = Month(mydataset.Tables("acf020").Rows(0).Item("date_2"))  '記錄第一筆之月份
            Else   '沒有資料
                GoTo NextFor
            End If

            Dim page As FPPage
            For PageCnt As Integer = 1 To 999    '頁次
                txtNo.Text = Val(txtNo.Text) + 1
                If rdbPage.Checked Then  '頁次大於控制列印範圍時就結束
                    If Val(txtNo.Text) > Val(txtPrintPageE.Text) Then Exit For
                End If
                blnIsBelong = (rdbAll.Checked = True Or (rdbAll.Checked = False And (Val(txtNo.Text) >= Val(txtPrintPageS.Text) And Val(txtNo.Text) <= Val(txtPrintPageE.Text))))

                '控制列印範圍內的才new
                If blnIsBelong Then
                    blnIsPrint = True
                    page = New FPPage
                    textUnit = New FPText(TransPara.TransP("UnitTitle"), 90, 0)
                    textUnit.HAlignment = FPAlignment.Center
                    textUnit.Font.Size = 14   '改變文字大小為14點
                    textTitle = New FPText(Aryaccname(0), 90, 6)   '帳簿抬頭
                    textTitle.HAlignment = FPAlignment.Center
                    textTitle.Font.Size = 14
                    textyear = New FPText("中華民國" & SYear & "年度", 90, 12)
                    textyear.HAlignment = FPAlignment.Center
                    textyear.Font.Size = 10
                    textPage = New FPText("第 " & txtNo.Text & " 頁", 300, 12)
                    'textName4 = New FPText("總帳科目：" & FormatAccno(Mid(strAccno, 1, 5)) & "  " & strName4, 0, 6)
                    textName7 = New FPText("明細科目：" & FormatAccno(strAccno), 0, 10)
                    If blnDoubleLine = True Then
                        PageRow = 27   '每頁印27行 because each row put 2 lines
                    Else
                        PageRow = 30   '每頁印30行
                    End If
                    grid = New FPTable(0, 17, 324, 227, PageRow, 16)   '新增表格物件x,y,w,h,row,col (單位是公厘)
                    grid.Font.Size = 9    '整個表格字體大小  '10       '(0, 17, 324, 222, PageRow, 16)   

                    grid.ColumnStyles(3).HAlignment = StringAlignment.Far    '號數
                    grid.ColumnStyles(4).HAlignment = StringAlignment.Near   '摘要
                    grid.SetLineColor(Color.Blue)   'table line color 
                    '定欄位寬tot324
                    grid.ColumnStyles(1).Width = 14    '假若要更動欄位數則要更改 '14
                    grid.ColumnStyles(2).Width = 6      '6
                    grid.ColumnStyles(3).Width = 12     '12
                    grid.ColumnStyles(4).Width = 52    '100
                    For intI = 5 To 16     '第五欄至第十六欄為金額欄
                        grid.ColumnStyles(intI).Width = 20   'array=5 : width=24
                        grid.ColumnStyles(intI).HAlignment = StringAlignment.Far
                    Next

                    grid.Cells2D(1, 1).ColSpan = 3 '12,3 隱藏
                    grid.Cells2D(1, 4).RowSpan = 2
                    grid.Cells2D(1, 5).RowSpan = 2
                    grid.Cells2D(1, 15).RowSpan = 2
                    grid.Cells2D(1, 16).RowSpan = 2
                    grid.Texts2D(1, 1).Text = "傳　   　票"
                    grid.Texts2D(2, 1).Text = "日　期"
                    grid.Texts2D(2, 2).Text = "種類"
                    grid.Texts2D(2, 3).Text = "號 數"
                    'grid.Texts2D(1, 7).Text = "支           出            數            ."
                    grid.Texts2D(1, 4).Text = "　　摘　　　　要"
                    grid.Texts2D(1, 5).Text = "收   入   數 ."
                    grid.Texts2D(1, 15).Text = "合       計 ."
                    grid.Texts2D(1, 16).Text = "餘       額 ."
                    If blnDoubleLine = True Then
                        For intI = 1 To intCol
                            If intI <= 9 Then     '印成兩行
                                grid.Texts2D(1, intI + 5).Text = Aryaccname(intI) & vbCrLf & Format(AryBgAmt(intI), "###,###,###,###")
                            Else
                                grid.Texts2D(2, intI - 9 + 5).Text = Aryaccname(intI) & vbCrLf & Format(AryBgAmt(intI), "###,###,###,###")
                            End If
                        Next
                    Else
                        For intI = 1 To 9
                            grid.Texts2D(1, intI + 5).Text = Aryaccname(intI)  '自第6欄起放array
                            grid.Texts2D(2, intI + 5).Text = Format(AryBgAmt(intI), "###,###,###,###") '自第6欄起放array
                        Next
                    End If
                Else
                    textUnit = ttextUnit
                    textTitle = ttextTitle
                    textyear = ttextyear
                    textPage = ttextPage
                    textName7 = ttextName7
                    grid = ggrid
                End If

                i = 2
                If intR = 0 Then   '上月累計
                    If AryAcuamt(0) + AryAcuamt(1) + AryAcuamt(2) + AryAcuamt(3) + AryAcuamt(4) + AryAcuamt(5) + _
                       AryAcuamt(6) + AryAcuamt(7) + AryAcuamt(8) + AryAcuamt(9) + _
                       AryAcuamt(10) + AryAcuamt(11) + AryAcuamt(12) + AryAcuamt(13) + AryAcuamt(14) + AryAcuamt(15) + _
                       AryAcuamt(16) + AryAcuamt(17) + AryAcuamt(18) + AryAcuamt(19) <> 0 Then
                        i += 1
                        If nudSmonth.Value = 1 Then
                            strRemark = "　　上年度轉入數"
                        Else
                            strRemark = "　　上月累計"
                        End If
                        grid.Texts2D(i, 4).Text = strRemark
                        AcuTot = 0
                        grid.Texts2D(i, 5).Text = Format(AryAcuamt(0), "###,###,###,###.##")  '收入欄
                        For intI = 1 To 9
                            If blnDoubleLine = True Then
                                grid.Texts2D(i, intI + 5).Text = Format(AryAcuamt(intI), "###,###,###,###.##") & _
                                             vbCrLf & Format(AryAcuamt(intI + 9), "###,###,###,###.##")
                            Else
                                grid.Texts2D(i, intI + 5).Text = Format(AryAcuamt(intI), "###,###,###,###.##")  '自第五欄起放array
                            End If
                        Next
                        For intI = 1 To intCol
                            AcuTot = AcuTot + AryAcuamt(intI) '支出合計
                        Next
                        grid.Texts2D(i, 15).Text = Format(AcuTot, "###,###,###,###.##")
                        grid.Texts2D(i, 16).Text = Format(AryAcuamt(0) - AcuTot, "###,###,###,###.##")   '餘額
                    End If
                    MonTot = 0
                    If nudSmonth.Value = 1 Then MonTot = AcuTot : tempMonth = 1
                End If
                With mydataset.Tables("acf020")
                    Do While i < PageRow
                        If intR > .Rows.Count - 1 Then   'end of file 
                            If MonTot <> 0 Then
                                i += 1
                                If i > PageRow Then Exit Do
                                strRemark = "　　　　本　　月　　合　　計"
                                grid.Texts2D(i, 4).Text = strRemark
                                grid.Texts2D(i, 5).Text = Format(AryMonamt(0), "###,###,###,###.##")
                                For intI = 1 To 9
                                    If blnDoubleLine = True Then
                                        grid.Texts2D(i, intI + 5).Text = Format(AryMonamt(intI), "###,###,###,###.##") & _
                                                                vbCrLf & Format(AryMonamt(intI + 9), "###,###,###,###.##")
                                    Else
                                        grid.Texts2D(i, intI + 5).Text = Format(AryMonamt(intI), "###,###,###,###.##")
                                    End If
                                Next
                                MonTot = 0
                                For intI = 1 To intCol
                                    MonTot = MonTot + AryMonamt(intI) '支出合計
                                Next
                                grid.Texts2D(i, 15).Text = Format(MonTot, "###,###,###,###.##")
                                '必須清0,否則當此行為最後一行時,次頁會再印
                                MonTot = 0
                                For intI = 0 To intCol
                                    AryMonamt(intI) = 0
                                Next
                            End If
                            If AcuTot <> 0 Then
                                i += 1
                                If i > PageRow Then Exit Do
                                strRemark = "　　　　本　　月　　累　　計"
                                grid.Texts2D(i, 4).Text = strRemark
                                grid.Texts2D(i, 5).Text = Format(AryAcuamt(0), "###,###,###,###.##")
                                For intI = 1 To 9
                                    If blnDoubleLine = True Then
                                        grid.Texts2D(i, intI + 5).Text = Format(AryAcuamt(intI), "###,###,###,###.##") & _
                                                            vbCrLf & Format(AryAcuamt(intI + 9), "###,###,###,###.##")
                                    Else
                                        grid.Texts2D(i, intI + 5).Text = Format(AryAcuamt(intI), "###,###,###,###.##")
                                    End If
                                Next
                                AcuTot = 0
                                For intI = 1 To intCol
                                    AcuTot = AcuTot + AryAcuamt(intI) '支出合計
                                Next
                                grid.Texts2D(i, 15).Text = Format(AcuTot, "###,###,###,###.##")
                                grid.Texts2D(i, 16).Text = Format(AryAcuamt(0) - AcuTot, "###,###,###,###.##")
                                '必須清0,否則當此行為最後一行時,次頁會再印
                                AcuTot = 0
                            End If
                            PageCnt = 1000
                            Exit Do  'Exit 
                        End If   'the end of record 

                        If Month(.Rows(intR).Item("date_2")) <> tempMonth Then  '日期不同時要印小計
                            If MonTot <> 0 Then
                                i += 1
                                If i > PageRow Then Exit Do
                                strRemark = "　　　　本　　月　　合　　計"
                                grid.Texts2D(i, 4).Text = strRemark
                                grid.Texts2D(i, 5).Text = Format(AryMonamt(0), "###,###,###,###.##")
                                For intI = 1 To 9
                                    If blnDoubleLine = True Then
                                        grid.Texts2D(i, intI + 5).Text = Format(AryMonamt(intI), "###,###,###,###.##") & _
                                                            vbCrLf & Format(AryMonamt(intI + 9), "###,###,###,###.##")
                                    Else
                                        grid.Texts2D(i, intI + 5).Text = Format(AryMonamt(intI), "###,###,###,###.##")
                                    End If
                                Next
                                MonTot = 0
                                For intI = 1 To intCol
                                    MonTot = MonTot + AryMonamt(intI) '支出合計
                                Next
                                grid.Texts2D(i, 15).Text = Format(MonTot, "###,###,###,###.##")
                                '月合計必須清0
                                MonTot = 0
                                For intI = 0 To intCol
                                    AryMonamt(intI) = 0
                                Next
                            End If
                            If AcuTot <> 0 Then
                                i += 1
                                If i > PageRow Then Exit Do
                                strRemark = "　　　　本　　月　　累　　計"
                                grid.Texts2D(i, 4).Text = strRemark
                                grid.Texts2D(i, 5).Text = Format(AryAcuamt(0), "###,###,###,###.##")
                                For intI = 1 To 9
                                    If blnDoubleLine = True Then
                                        grid.Texts2D(i, intI + 5).Text = Format(AryAcuamt(intI), "###,###,###,###.##") & _
                                                            vbCrLf & Format(AryAcuamt(intI + 9), "###,###,###,###.##")
                                    Else
                                        grid.Texts2D(i, intI + 5).Text = Format(AryAcuamt(intI), "###,###,###,###.##")
                                    End If
                                Next
                                AcuTot = 0
                                For intI = 1 To intCol
                                    AcuTot = AcuTot + AryAcuamt(intI) '支出合計
                                Next
                                grid.Texts2D(i, 15).Text = Format(AcuTot, "###,###,###,###.##")
                                grid.Texts2D(i, 16).Text = Format(AryAcuamt(0) - AcuTot, "###,###,###,###.##")
                                '月累計必須清0
                                AcuTot = 0
                            End If
                            tempMonth = Month(.Rows(intR).Item("date_2"))
                        End If

                        i += 1
                        If i > PageRow Then Exit Do '換新頁

                        '逐筆資料列印
                        grid.Texts2D(i, 1).Text = ShortDate(.Rows(intR).Item("date_2"))
                        If .Rows(intR).Item("kind") = "1" Then grid.Texts2D(i, 2).Text = "收"
                        If .Rows(intR).Item("kind") = "2" Then grid.Texts2D(i, 2).Text = "支"
                        If .Rows(intR).Item("kind") >= "3" Then grid.Texts2D(i, 2).Text = "轉"
                        grid.Texts2D(i, 3).Text = Format(nz(.Rows(intR).Item("no_2_no"), 0), "#####")
                        grid.Texts2D(i, 4).Text = nz(.Rows(intR).Item("remark"), "")  '摘要
                        '找該放欄位
                        sqlstr = nz(.Rows(intR).Item("accno"), "")
                        intI = Array.IndexOf(AryAccno, nz(.Rows(intR).Item("accno"), ""))
                        If intI < 0 Then    '>=0為找得到
                            intI = intCol    '找不到則置第9欄
                        End If
                        strDC = nz(.Rows(intR).Item("dc"), "")   '傳票借貸
                        intAmt = nz(.Rows(intR).Item("amt"), 0)  '傳票金額
                        MonTot += intAmt   '最主要在控制是否要印月合計
                        AcuTot += intAmt   '最主要在控制是否要印月累計  (一定要單獨判斷,因有時會分作兩頁列印)
                        If intI = 0 Then '收入欄
                            If strDC = "1" Then intAmt = -intAmt '2-1302 at debit is 減少 
                        Else             '支出欄
                            If strDC = "2" Then intAmt = -intAmt '2-1302 at credit is 減少
                        End If
                        AryMonamt(intI) += intAmt
                        AryAcuamt(intI) += intAmt
                        If blnDoubleLine = True Then
                            If intI <= 9 Then
                                grid.Texts2D(i, intI + 5).Text = Format(intAmt, "###,###,###,###.##") & vbCrLf & ""
                            Else
                                grid.Texts2D(i, intI - 9 + 5).Text = "" & vbCrLf & Format(intAmt, "###,###,###,###.##")
                            End If
                        Else
                            grid.Texts2D(i, intI + 5).Text = Format(intAmt, "###,###,###,###.##")
                        End If
                        intR += 1
                    Loop
                End With

                '控制列印範圍內的才加入
                If blnIsBelong Then
                    page.Add(textUnit)       '加入要列印的文字到列印頁面中
                    page.Add(textTitle)
                    page.Add(textyear)
                    page.Add(textPage)
                    page.Add(textName7)
                    page.Add(grid)          '加入要列印的表格到列印頁面中
                    document.AddPage(page)  '加入要列印的頁面到列印文件中
                End If
            Next
NextFor: Next
        If document.PageCount > 0 Then
            printer.Document = document
            printer.PrintMode = PrintMode.NormalPrint
            printer.IsAutoShowPrintDialog = True '自動顯示印表機設定對話盒
            If rdoPreview.Checked Then printer.IsAutoShowPrintPreviewDialog = True '自動顯示預覽列印對話盒
            If blnIsPrint Then printer.Print() '開始列印
        End If
        Me.Close()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        mydataset = Nothing
        myds = Nothing
        Me.Close()
    End Sub

    Private Sub txtNo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNo.LostFocus, txtPrintPageS.LostFocus, txtPrintPageE.LostFocus
        If (Not IsNumeric(sender.Text) Or ValComa(sender.text) < 0) And Trim(sender.text) <> "" Then
            MsgBox("請輸入正數字")
            sender.Focus()
        End If
    End Sub

    Private Sub Clear_Array()
        Dim intI As Integer
        For intI = 0 To intCol
            AryAccno(intI) = ""    '宣告每件工程之帳簿有1個收入欄及9個費用別 accno科目代號
            Aryaccname(intI) = ""  '宣告每件工程之帳簿有1個收入欄及9個費用別 accname科目名稱
            AryBgAmt(intI) = 0     '宣告每件工程之帳簿有1個收入欄及9個費用別 預算數 
            AryAcuamt(intI) = 0       '宣告每件工程之帳簿有1個收入欄及9個費用別 各欄年累計數 
            AryMonamt(intI) = 0       '宣告每件工程之帳簿有1個收入欄及9個費用別 各欄月累計數 
        Next
    End Sub
End Class
