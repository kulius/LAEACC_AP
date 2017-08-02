Imports JBC.Printing
Public Class ACBK060
    Dim confid, confunit As String, LoadAfter, Dirty As Boolean
    Dim sqlstr As String
    Dim strAccno As String
    Dim SYear As Integer
    Dim SDate, EDate As String
    'Dim strAccount, strBankname As String   '銀行名稱
    Dim mydataset, myds, mydsD As DataSet
    Dim PageRow As Integer = 37  '每頁印37行

    Private Sub ACBK060_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '找過帳日期
        Dim sqlstr As String
        sqlstr = "SELECT max(date_2) as date_2  from acf010 where books='Y'"
        myds = openmember("", "acf010s", sqlstr)
        If myds.Tables("acf010s").Rows.Count > 0 Then
            nudEmonth.Value = Month(myds.Tables("acf010s").Rows(0).Item("date_2"))
            nudSmonth.Value = nudEmonth.Value
            nudSyear.Value = Year(myds.Tables("acf010s").Rows(0).Item("date_2"))
            nudEyear.Value = nudSyear.Value
        End If
        vxtSAccno.Text = "5"    '起值
        vxtEAccno.Text = "59"   '迄值
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
        Dim AcuAmt, AcuAccount, AcuBGtot, AcuBGamt As Decimal
        Dim MonAmt, MonAccount, MonBgtot, MonBGamt As Decimal
        Dim retstr, strRemark As String
        Dim strName4, strName7 As String
        txtNo.Text = Val(txtNo.Text) - 1
        SYear = nudSyear.Value
        SDate = FullDate(Format(nudSyear.Value, "0000") & "/" & Format(nudSmonth.Value, "00") & "/1") '起印日
        EDate = FullDate(Format(nudEyear.Value, "0000") & "/" & Format(nudEmonth.Value, "00") & "/" & DateTime.DaysInMonth(nudEyear.Value + 1911, nudEmonth.Value)) '訖印日
        Dim up As String = Format(nudSmonth.Value - 1, "00")  '上月
        Dim mm As String = Format(nudEmonth.Value, "00") '本月
        Dim Saccno As String = GetAccno(vxtSAccno.Text)  '起印科目
        Dim Eaccno As String = GetAccno(vxtEAccno.Text)  '訖印科目
        Dim tempMonth As Integer = 0
        Dim strEnd As String = "N"  '控制列印結轉分錄
        Dim intEndNo As Integer = 0
        If nudEmonth.Value = 12 Then
            If MsgBox("是否要列印結轉本期餘絀 YES/NO", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                strEnd = "Y"
                intEndNo = QueryNO(SYear, "6") + 1  '結轉分錄轉帳編號
            End If
        End If

        Dim printer = New KPrint
        Dim document As New FPDocument("列印支出明細分類帳 ")
        document.DefaultPageSettings.PaperKind = Printing.PaperKind.B4   'A3:420x297  B4:364X257
        document.DefaultPageSettings.Landscape = True    '橫印
        document.SetDefaultPageMargin(30, 5, 10, 10)    'left,top,right,botton
        document.DefaultFont = New Font("新細明體", 11) '標楷體

        '以下是符合頁碼範圍所要用到的物件變數
        Dim textUnit As FPText
        Dim textTitle As FPText
        Dim textyear As FPText
        Dim textPage As FPText
        Dim textName4 As FPText
        Dim textName7 As FPText
        Dim grid As FPTable
        '以下是不在頁碼範圍內要用到的物件變數
        Dim ttextUnit As New FPText
        Dim ttextTitle As New FPText
        Dim ttextyear As New FPText
        Dim ttextPage As New FPText
        Dim ttextName4 As New FPText
        Dim ttextName7 As New FPText
        Dim ggrid As New FPTable(0, 17, 314, 222, PageRow, 10)
        '宣告一變數儲存是否符合頁碼範圍
        Dim blnIsBelong As Boolean
        Dim blnIsPrint As Boolean = True  '記錄是否有資料頁列印

        'strRemark = "上年度轉入"

        '由acf050逐科目列印 取最明細科目(8級科目必須與7級科目合併列印)
        Dim sqlstr2, sqlstr3 As String
        Dim sea, Q As Integer
        If up = "00" Then
            sqlstr2 = " a.beg_debit as deamt, a.beg_credit as cramt"
            sqlstr3 = " f.bg1+f.bg2+f.bg3+f.bg4+f.bg5+f.up1+f.up2+f.up3+f.up4+f.up5 as bgtot, 0 AS bgamt, "
        Else
            sqlstr2 = " a.deamt" & up & " as deamt, a.cramt" & up & " as cramt"
            sea = Season(SYear + 1911 & "/" & up & "/1")
            For Q = 1 To sea
                sqlstr3 += "f.bg" & Format(Q, "0") & "+" & "f.up" & Format(Q, "0") & "+"
            Next
            sqlstr3 = cutright1(sqlstr3, "+")
            sqlstr3 = "f.bg1+f.bg2+f.bg3+f.bg4+f.bg5+f.up1+f.up2+f.up3+f.up4+f.up5 as bgtot, " & sqlstr3 & " as bgamt, "
        End If
        sqlstr2 += ",  a.deamt" & mm & " as deamtmm, a.cramt" & mm & " as cramtmm"
        'sqlstr = "SELECT a.accno, c.accname, d.accname as name4, " & sqlstr3 & sqlstr2 & " FROM ACF050 a " & _
        '         "LEFT OUTER JOIN ACCNAME c ON a.ACCNO = c.ACCNO " & _
        '         "LEFT OUTER JOIN ACCNAME d ON RTRIM(LEFT(a.ACCNO, 5)) = d.ACCNO " & _
        '         "left outer join accbg f on a.accno=f.accno and f.accyear=" & SYear & _
        '         "WHERE (a.ACCYEAR =" & SYear & ") AND (a.ACCNO BETWEEN '" & Saccno & "' AND '" & Eaccno & "') AND " & _
        '         "(a.ACCNO NOT IN (SELECT LEFT(b.accno, len(a.accno)) AS accno " & _
        '         "FROM (SELECT accno FROM acf050 WHERE accyear =" & SYear & "AND " & _
        '         "accno BETWEEN '" & Saccno & "' AND '" & Eaccno & "') b " & _
        '         "WHERE a.accno = LEFT(b.accno, len(a.accno)) AND a.accno <> b.accno)) " & _
        '         "ORDER BY  a.ACCNO"
        '先找帳簿列印科目
        sqlstr = "select * from accname a where (accno not in " & _
                 " (select left(b.accno, len(a.accno)) AS accno FROM " & _
                 " (select accno FROM accname where accno BETWEEN '" & Saccno & "' AND '" & Eaccno & "' AND belong <> 'B') b " & _
                 " where a.accno = left(b.accno, len(a.accno)) AND a.accno <> b.accno)) " & _
                 " AND (belong <> 'B') and accno BETWEEN '" & Saccno & "' AND '" & Eaccno & "'" & _
                 " order by a.accno "
        mydsD = openmember("", "DataAccno", sqlstr)
        '逐科目列印
        For intD = 0 To mydsD.Tables("DataAccno").Rows.Count - 1
            If rdbPage.Checked Then  '頁次大於控制列印範圍時就結束
                If Val(txtNo.Text) > Val(txtPrintPageE.Text) Then Exit For
            End If
            strAccno = RTrim(nz(mydsD.Tables("DataAccno").Rows(intD).Item("accno"), "")) '列印科目
            sqlstr = "SELECT g.accno, c.accname, d.accname as name4, " & sqlstr3 & sqlstr2 & " FROM accname g " & _
                     " left outer join acf050 a on a.accno=g.accno and a.accyear=" & SYear & _
                     " LEFT OUTER JOIN ACCNAME c ON g.ACCNO = c.ACCNO " & _
                     " LEFT OUTER JOIN ACCNAME d ON RTRIM(LEFT(g.ACCNO, 5)) = d.ACCNO " & _
                     " left outer join accbg f on g.accno=f.accno and f.accyear=" & SYear & _
                     " WHERE g.accno='" & strAccno & "'"
            mydataset = openmember("", "acf050", sqlstr)
            If mydataset.Tables(0).Rows.Count <= 0 Then GoTo nextfor
            With mydataset.Tables(0).Rows(0)
                strName7 = nz(.Item("accname"), "") '列印科目名稱
                If Mid(strAccno, 8, 2).Trim <> "" And Len(strAccno) > 9 Then  '六級
                    sqlstr = "select * from accname where accno='" & Mid(strAccno, 1, 9) & "'"
                    myds = openmember("", "accname", sqlstr)
                    If myds.Tables("accname").Rows.Count > 0 Then
                        strName7 = nz(myds.Tables("accname").Rows(0).Item("accname"), "") + "－" + strName7
                    End If
                End If
                strName4 = nz(.Item("name4"), "") '列印科目名稱
                '上月累計
                AcuAmt = nz(.Item("deamt"), 0) - nz(.Item("cramt"), 0)
                AcuAccount = 0
                AcuBGtot = nz(.Item("bgtot"), 0)
                AcuBGamt = nz(.Item("bgamt"), 0)   '分配數
                If nudSmonth.Value = 1 Then
                    MonAmt = AcuAmt
                    MonBGamt = AcuBGamt
                Else
                    MonAmt = 0 : MonBGamt = 0
                    'If nz(.Item("deamt"), 0) = nz(.Item("deamtmm"), 0) And _
                    '   nz(.Item("cramt"), 0) = nz(.Item("cramtmm"), 0) Then
                    '    GoTo nextfor    '本月無異動傳票
                    'End If
                End If
                If AcuBGtot = 0 And nz(.Item("deamtmm"), 0) = 0 And nz(.Item("cramtmm"), 0) = 0 Then
                    GoTo nextfor    '皆無預算與傳票資料
                End If
            End With


            intR = 0

            '找acf020傳票資料並要併入accbgbook資料(table body record )
            sqlstr = "SELECT a.accyear, a.accno, b.date_2 as date_2, a.kind, a.no_2_no, a.seq, a.item, a.remark, a.amt, a.cotn_code FROM " & _
                     " (select * from acf020 where accyear>=" & SYear & " and accno='" & strAccno & "') a " & _
                     " left outer join acf010 b on a.accyear=b.accyear and a.kind=b.kind " & _
                     " and a.no_2_no=b.no_2_no and a.seq=b.seq and b.item='1' " & _
                     " where b.date_2 between '" & SDate & "' and '" & EDate & "'" & _
                     " union " & _
                     " select c.accyear, c.accno, c.date_2 AS date_2, c.kind, c.no_2_no,0 as seq, 0 as item, c.remark, c.amt, c.cotn_code " & _
                     " from accbgbook c where c.date_2 between '" & SDate & "' and '" & EDate & "' and c.accno='" & strAccno & _
                     "' order by date_2, a.kind, a.no_2_no, a.seq, a.item "
            mydataset = openmember("", "acf020", sqlstr)

            If mydataset.Tables("acf020").Rows.Count > 0 Then
                tempMonth = Month(mydataset.Tables("acf020").Rows(0).Item("date_2"))  '記錄第一筆之月份
            Else   '沒有資料
                If Not (nudSmonth.Value = 1 And AcuBGtot <> 0) Then  '有預算核定數
                    GoTo NextFor
                End If
            End If
            If nudSmonth.Value = 1 Then tempMonth = 1 '起印=1月時 

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
                    textTitle = New FPText("支 出 明 細 分 類 帳", 90, 6)
                    textTitle.HAlignment = FPAlignment.Center
                    textTitle.Font.Size = 14
                    textyear = New FPText("中華民國" & SYear & "年度", 90, 12)
                    textyear.HAlignment = FPAlignment.Center
                    textyear.Font.Size = 11
                    textPage = New FPText("第 " & txtNo.Text & " 頁", 300, 12)
                    textName4 = New FPText("總帳科目：" & FormatAccno(Mid(strAccno, 1, 5)) & "  " & strName4, 0, 6)
                    textName7 = New FPText("明細科目：" & FormatAccno(strAccno) & "  " & strName7, 0, 12)
                    grid = New FPTable(0, 17, 316, 222, PageRow, 9)   '新增表格物件x,y,w,h,row,col (單位是公厘)
                    grid.Font.Size = 10
                    grid.ColumnStyles(3).HAlignment = StringAlignment.Far
                    grid.ColumnStyles(4).HAlignment = StringAlignment.Near
                    grid.ColumnStyles(5).HAlignment = StringAlignment.Far
                    grid.ColumnStyles(6).HAlignment = StringAlignment.Far
                    grid.ColumnStyles(7).HAlignment = StringAlignment.Far
                    grid.ColumnStyles(8).HAlignment = StringAlignment.Far
                    grid.ColumnStyles(9).HAlignment = StringAlignment.Far

                    grid.SetLineColor(Color.Blue)   'table line color 
                    grid.ColumnStyles(1).Width = 18    'tot364
                    grid.ColumnStyles(2).Width = 10
                    grid.ColumnStyles(3).Width = 12
                    grid.ColumnStyles(4).Width = 120
                    grid.ColumnStyles(5).Width = 30
                    grid.ColumnStyles(6).Width = 30
                    grid.ColumnStyles(7).Width = 32
                    grid.ColumnStyles(8).Width = 32
                    grid.ColumnStyles(9).Width = 32

                    grid.Cells2D(1, 1).ColSpan = 3 '12,3 隱藏
                    grid.Cells2D(1, 4).RowSpan = 2
                    grid.Cells2D(1, 5).RowSpan = 2
                    grid.Cells2D(1, 6).RowSpan = 2
                    grid.Cells2D(1, 7).ColSpan = 2
                    grid.Cells2D(1, 9).RowSpan = 2
                    grid.Texts2D(1, 1).Text = "傳　   　票"
                    grid.Texts2D(2, 1).Text = "日　期"
                    grid.Texts2D(2, 2).Text = "種類"
                    grid.Texts2D(2, 3).Text = "號 數"
                    grid.Texts2D(1, 7).Text = "支           出            數            ."
                    grid.Texts2D(1, 4).Text = "　　　　　　　　摘　　　　　　　　　　　要"
                    grid.Texts2D(1, 5).Text = "預   算   數   ."
                    grid.Texts2D(1, 6).Text = "預 算 分 配 數 ."
                    grid.Texts2D(2, 7).Text = "實   付   數   ."
                    grid.Texts2D(2, 8).Text = "應   付   數　 ."
                    grid.Texts2D(1, 9).Text = "未支出之分配數"
                Else
                    textUnit = ttextUnit
                    textTitle = ttextTitle
                    textyear = ttextyear
                    textPage = ttextPage
                    textName4 = ttextName4
                    textName7 = ttextName7
                    grid = ggrid
                End If

                i = 2
                If intR = 0 Then   '上月累計
                    If AcuBGtot <> 0 Or AcuAmt <> 0 Then
                        i += 1
                        If nudSmonth.Value = 1 Then
                            strRemark = "　　核定預算數"
                            MonBgtot = AcuBGtot
                        Else
                            strRemark = "　　上月累計"
                            grid.Texts2D(i, 6).Text = FormatNumber(AcuBGamt, 0)
                            grid.Texts2D(i, 7).Text = FormatNumber(AcuAmt, 2)
                            grid.Texts2D(i, 8).Text = FormatNumber(AcuAccount, 2)
                            grid.Texts2D(i, 9).Text = FormatNumber(AcuBGamt - AcuAmt - AcuAccount, 2)
                        End If
                        grid.Texts2D(i, 4).Text = strRemark
                        grid.Texts2D(i, 5).Text = FormatNumber(AcuBGtot, 0)
                    End If
                End If

                With mydataset.Tables("acf020")
                    Do While i < PageRow
                        If intR > .Rows.Count - 1 Then
                            If MonBgtot <> 0 Or MonBGamt <> 0 Or MonAmt <> 0 Or MonAccount <> 0 Then
                                i += 1
                                If i > PageRow Then Exit Do
                                strRemark = "　　　　本　　月　　合　　計"
                                grid.Texts2D(i, 4).Text = strRemark
                                grid.Texts2D(i, 5).Text = FormatNumber(MonBgtot, 0)
                                grid.Texts2D(i, 6).Text = FormatNumber(MonBGamt, 0)
                                grid.Texts2D(i, 7).Text = FormatNumber(MonAmt, 2)
                                grid.Texts2D(i, 8).Text = FormatNumber(MonAccount, 2)
                                MonBgtot = 0 : MonBGamt = 0 : MonAmt = 0 : MonAccount = 0 '必須清0,否則當此行為最後一行時,次頁會再印
                            End If
                            If AcuBGtot <> 0 Or AcuBGamt <> 0 Or AcuAmt <> 0 Or AcuAccount <> 0 Then
                                i += 1
                                If i > PageRow Then Exit Do
                                strRemark = "　　　　本　　月　　累　　計"
                                grid.Texts2D(i, 4).Text = strRemark
                                grid.Texts2D(i, 5).Text = FormatNumber(AcuBGtot, 0)
                                grid.Texts2D(i, 6).Text = FormatNumber(AcuBGamt, 0)
                                grid.Texts2D(i, 7).Text = FormatNumber(AcuAmt, 2)
                                grid.Texts2D(i, 8).Text = FormatNumber(AcuAccount, 2)
                                grid.Texts2D(i, 9).Text = FormatNumber(AcuBGamt - AcuAmt - AcuAccount, 2)
                            End If
                            If strEnd = "Y" And AcuAmt + AcuAccount > 0 Then
                                i += 1
                                If i > PageRow Then Exit Do
                                grid.Texts2D(i, 4).Text = "　　　　結轉本期餘絀"
                                grid.Texts2D(i, 1).Text = ShortDate(EDate)
                                grid.Texts2D(i, 2).Text = "轉"
                                grid.Texts2D(i, 3).Text = Format(intEndNo, "#####")
                                'grid.Texts2D(i, 6).Text = FormatNumber(AcuBGamt, 0)
                                grid.Texts2D(i, 7).Text = FormatNumber(AcuAmt, 2)
                                grid.Texts2D(i, 8).Text = FormatNumber(AcuAccount, 2)
                                'grid.Texts2D(i, 9).Text = FormatNumber(AcuBGamt - AcuAmt - AcuAccount, 2)
                            End If
                            PageCnt = 1000
                            Exit Do  'Exit 
                        End If   'the end of record 

                        If Month(.Rows(intR).Item("date_2")) <> tempMonth Then  '日期不同時要印小計
                            If MonBgtot <> 0 Or MonBGamt <> 0 Or MonAmt <> 0 Or MonAccount <> 0 Then
                                i += 1
                                If i > PageRow Then Exit Do
                                strRemark = "　　　　本　　月　　合　　計"
                                grid.Texts2D(i, 4).Text = strRemark
                                grid.Texts2D(i, 5).Text = FormatNumber(MonBgtot, 0)
                                grid.Texts2D(i, 6).Text = FormatNumber(MonBGamt, 0)
                                grid.Texts2D(i, 7).Text = FormatNumber(MonAmt, 2)
                                grid.Texts2D(i, 8).Text = FormatNumber(MonAccount, 2)
                                MonBgtot = 0 : MonBGamt = 0 : MonAmt = 0 : MonAccount = 0
                            End If
                            If AcuBGtot <> 0 Or AcuBGamt <> 0 Or AcuAmt <> 0 Or AcuAccount <> 0 Then
                                i += 1
                                If i > PageRow Then Exit Do
                                strRemark = "　　　　本　　月　　累　　計"
                                grid.Texts2D(i, 4).Text = strRemark
                                grid.Texts2D(i, 5).Text = FormatNumber(AcuBGtot, 0)
                                grid.Texts2D(i, 6).Text = FormatNumber(AcuBGamt, 0)
                                grid.Texts2D(i, 7).Text = FormatNumber(AcuAmt, 2)
                                grid.Texts2D(i, 8).Text = FormatNumber(AcuAccount, 2)
                                grid.Texts2D(i, 9).Text = FormatNumber(AcuBGamt - AcuAmt - AcuAccount, 2)
                            End If
                            tempMonth = Month(.Rows(intR).Item("date_2"))
                        End If

                        i += 1
                        If i > PageRow Then Exit Do '換新頁

                        grid.Texts2D(i, 1).Text = ShortDate(.Rows(intR).Item("date_2"))
                        If .Rows(intR).Item("kind") = "1" Then grid.Texts2D(i, 2).Text = "收"
                        If .Rows(intR).Item("kind") = "2" Then grid.Texts2D(i, 2).Text = "支"
                        If .Rows(intR).Item("kind") >= "3" Then grid.Texts2D(i, 2).Text = "轉"
                        grid.Texts2D(i, 3).Text = Format(nz(.Rows(intR).Item("no_2_no"), 0), "#####")
                        grid.Texts2D(i, 4).Text = nz(.Rows(intR).Item("remark"), "")  '摘要

                        If nz(.Rows(intR).Item("no_2_no"), 0) = 0 And RTrim(nz(.Rows(intR).Item("kind"), "")) = "" Then '預算資料
                            grid.Texts2D(i, 6).Text = FormatNumber(.Rows(intR).Item("amt"), 0)
                            MonBGamt += .Rows(intR).Item("amt")
                            AcuBGamt += .Rows(intR).Item("amt")
                        Else
                            If .Rows(intR).Item("kind") = "1" Or .Rows(intR).Item("kind") = "4" Then   '收 or 貸方
                                If .Rows(intR).Item("cotn_code") = "A" Then   '應付數(保留)
                                    grid.Texts2D(i, 8).Text = FormatNumber(-.Rows(intR).Item("amt"), 2)
                                    MonAccount -= .Rows(intR).Item("amt")
                                    AcuAccount -= .Rows(intR).Item("amt")
                                Else
                                    grid.Texts2D(i, 7).Text = FormatNumber(-.Rows(intR).Item("amt"), 2)
                                    MonAmt -= .Rows(intR).Item("amt")
                                    AcuAmt -= .Rows(intR).Item("amt")
                                End If
                            Else                                    '借方
                                If .Rows(intR).Item("cotn_code") = "A" Then  '應付數(保留)
                                    grid.Texts2D(i, 8).Text = FormatNumber(.Rows(intR).Item("amt"), 2)
                                    MonAccount += .Rows(intR).Item("amt")
                                    AcuAccount += .Rows(intR).Item("amt")
                                Else
                                    grid.Texts2D(i, 7).Text = FormatNumber(.Rows(intR).Item("amt"), 2)
                                    MonAmt += .Rows(intR).Item("amt")
                                    AcuAmt += .Rows(intR).Item("amt")
                                End If
                            End If
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
                    page.Add(textName4)
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
End Class
