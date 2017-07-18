Imports JBC.Printing
Public Class ACBK020
    Dim confid, confunit As String, LoadAfter, Dirty As Boolean
    Dim sqlstr As String
    Dim strAccno As String
    Dim SYear As Integer
    Dim SDate, EDate As String
    'Dim strAccount, strBankname As String   '銀行名稱
    Dim mydataset, myds, mydsD As DataSet
    Dim PageRow As Integer = 37  '每頁印37行

    Private Sub ACBK020_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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
        vxtSAccno.Text = "1"    '起值
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
        Dim AcuDeamt, AcuCramt, acuAct, acuSub, acuTrans As Decimal
        Dim MonDeamt, MonCramt, monAct, monSub, monTrans As Decimal
        Dim first_monTrans As String = "Y"  '第一次月份變動
        Dim tempAcuAct, tempAcuSub, tempAcuTrans As Decimal
        Dim retstr, strRemark As String
        Dim strName4, strName5, strName7 As String
        txtNo.Text = Val(txtNo.Text) - 1
        SYear = nudSyear.Value
        SDate = FullDate(nudSyear.Value & "/" & nudSmonth.Value & "/1")   '起印日
        EDate = FullDate(nudEyear.Value & "/" & nudEmonth.Value & "/" & DateTime.DaysInMonth(nudEyear.Value + 1911, nudEmonth.Value))  '訖印日
        Dim up As String = Format(nudSmonth.Value - 1, "00")  '上月
        Dim mm As String = Format(nudEmonth.Value, "00") '本月
        Dim Saccno As String = GetAccno(vxtSAccno.Text)  '起印科目
        Dim Eaccno As String = GetAccno(vxtEAccno.Text)  '訖印科目
        Dim tempMonth As Integer = 0
        Dim strEnd As String = "N"  '控制列印結轉分錄
        Dim intEndNo As Integer = 0
        If nudEmonth.Value = 12 Then
            If MsgBox("是否要列印結轉下年度 YES/NO", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                strEnd = "Y"
                intEndNo = QueryNO(SYear, "6") + 2  '結轉分錄轉帳編號
            End If
        End If

        Dim printer As FPPrinter = FPPrinter.SharedPrinter
        Dim document As New FPDocument("列印應收應付催收明細分類帳")
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


        '由acf060逐科目列印 取最明細科目7級
        Dim sqlstr2, sqlstr3 As String
        If up = "00" Then
            sqlstr2 = " a.beg_debit as deamt, a.beg_credit as cramt, 0 as act, 0 as sub, 0 as trans"
        Else
            sqlstr2 = " a.deamt" & up & " as deamt, a.cramt" & up & " as cramt, a.act" & up & " as act, a.sub" & up & " as sub, a.trans" & up & " as trans"
        End If
        sqlstr2 += ",  a.deamt" & mm & " as deamtmm, a.cramt" & mm & " as cramtmm"
        sqlstr3 = " (select * from acf060 where accyear=" & SYear & " And " & _
                 " ( left(accno,3)='113' OR left(accno,3)='151' OR left(accno,3)='212')  and " & _
                 " accno BETWEEN '" & Saccno & "' AND '" & Eaccno & "' and len(accno)<17 ) "
        sqlstr = "SELECT a.accno, c.accname, d.accname as name4, e.accname as name5, " & sqlstr2 & " FROM " & sqlstr3 & " a " & _
                 " LEFT OUTER JOIN ACCNAME c ON a.ACCNO = c.ACCNO " & _
                 " LEFT OUTER JOIN ACCNAME d ON RTRIM(LEFT(a.ACCNO, 5)) = d.ACCNO " & _
                 " LEFT OUTER JOIN ACCNAME e ON RTRIM(LEFT(a.ACCNO, 9)) = e.ACCNO " & _
                 " WHERE  " & _
                 " (a.ACCNO NOT IN (SELECT LEFT(b.accno, len(a.accno)) AS accno " & _
                 " FROM " & sqlstr3 & " b " & _
                 " WHERE len(a.accno) < 10 and a.accno = LEFT(b.accno, len(a.accno)) AND a.accno <> b.accno)) " & _
                 " ORDER BY  a.ACCNO"

        'sqlstr = "SELECT a.accno, c.accname, d.accname as name4, e.accname as name5, " & sqlstr2 & " FROM ACF060 a " & _
        '         "LEFT OUTER JOIN ACCNAME c ON a.ACCNO = c.ACCNO " & _
        '         "LEFT OUTER JOIN ACCNAME d ON RTRIM(LEFT(a.ACCNO, 5)) = d.ACCNO " & _
        '         "LEFT OUTER JOIN ACCNAME e ON RTRIM(LEFT(a.ACCNO, 7)) = e.ACCNO " & _
        '         "WHERE (a.ACCYEAR =" & SYear & ") AND (a.ACCNO BETWEEN '" & Saccno & "' AND '" & Eaccno & "') AND " & _
        '         "(a.ACCNO NOT IN (SELECT LEFT(b.accno, len(a.accno)) AS accno " & _
        '         "FROM (SELECT accno FROM acf060 WHERE accyear =" & SYear & " AND  " & _
        '         "( left(accno,3)='113' OR left(accno,3)='151' OR left(accno,3)='212')  and " & _
        '         "accno BETWEEN '" & Saccno & "' AND '" & Eaccno & "') b " & _
        '         "WHERE a.accno = LEFT(b.accno, len(a.accno)) AND a.accno <> b.accno)) AND " & _
        '          "(left(a.accno,3)='113' OR left(a.accno,3)='151' OR left(a.accno,3)='212') " & _
        '        "ORDER BY  a.ACCNO"

        mydsD = openmember("", "DataAccno", sqlstr)
        For intD = 0 To mydsD.Tables("DataAccno").Rows.Count - 1
            If rdbPage.Checked Then  '頁次大於控制列印範圍時就結束
                If Val(txtNo.Text) > Val(txtPrintPageE.Text) Then Exit For
            End If
            With mydsD.Tables("DataAccno").Rows(intD)
                strAccno = .Item("accno")  '列印科目
                strName7 = nz(.Item("accname"), "") '列印科目名稱
                strName4 = nz(.Item("name4"), "")
                strName5 = nz(.Item("name5"), "")
                '上月累計or上年度轉入
                AcuDeamt = nz(.Item("deamt"), 0)
                AcuCramt = nz(.Item("cramt"), 0)
                acuAct = nz(.Item("act"), 0)
                acuSub = nz(.Item("sub"), 0)
                acuTrans = nz(.Item("trans"), 0)
                If nudSmonth.Value = 1 Then
                    MonDeamt = AcuDeamt
                    MonCramt = AcuCramt
                    monAct = acuAct
                    monSub = acuSub
                    monTrans = acuTrans
                Else
                    MonDeamt = 0 : MonCramt = 0 : monAct = 0 : monSub = 0 : monTrans = 0
                    If AcuDeamt = nz(.Item("deamtmm"), 0) And AcuCramt = nz(.Item("cramtmm"), 0) Then
                        GoTo nextfor    '本月無異動傳票
                    End If
                End If
            End With

            intR = 0

            '找acf020傳票資料(table body record )
            'sqlstr = "SELECT a.*, b.date_2 as date_2 FROM acf020 a left outer join acf010 b on a.accyear=b.accyear and a.kind=b.kind " & _
            '         "and a.no_2_no=b.no_2_no and a.seq=b.seq and b.item='1' " & _
            '         " where b.date_2 between '" & SDate & "' and '" & EDate & _
            '         "' and rtrim(left(a.accno,16))='" & strAccno & "' order by b.date_2, a.kind, a.no_2_no "
            sqlstr = "SELECT a.*, b.date_2 as date_2 FROM " & _
                     " (select * from acf020 where accyear>=" & SYear & " and rtrim(left(accno,16))='" & strAccno & "')" & _
                     " a left outer join acf010 b on a.accyear=b.accyear and a.kind=b.kind " & _
                     " and a.no_2_no=b.no_2_no and a.seq=b.seq and b.item='1' " & _
                     " where b.date_2 between '" & SDate & "' and '" & EDate & _
                     "' order by b.date_2, a.kind, a.no_2_no "
            mydataset = openmember("", "acf020", sqlstr)

            If mydataset.Tables("acf020").Rows.Count > 0 Then
                tempMonth = Month(mydataset.Tables("acf020").Rows(0).Item("date_2"))  '記錄第一筆之月份
            Else   '沒有資料
                If Not (nudSmonth.Value = 1 And AcuDeamt + AcuCramt <> 0) Then  '有上年度轉入數
                    GoTo NextFor
                End If
                If nudSmonth.Value = 1 Then tempMonth = 1 '起印=1月時    'whh modi 95/2/14
            End If
            'If nudSmonth.Value = 1 Then tempMonth = 1 '起印=1月時    'whh modi 95/2/14

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
                    textTitle = New FPText(strName5 & "明細分類帳", 90, 6)
                    textTitle.HAlignment = FPAlignment.Center
                    textTitle.Font.Size = 14
                    textyear = New FPText("中華民國" & SYear & "年度", 90, 12)
                    textyear.HAlignment = FPAlignment.Center
                    textyear.Font.Size = 11
                    textPage = New FPText("第 " & txtNo.Text & " 頁", 300, 12)
                    textName4 = New FPText("總帳科目：" & FormatAccno(Mid(strAccno, 1, 5)) & "  " & strName4, 0, 6)
                    textName7 = New FPText("明細科目：" & FormatAccno(strAccno) & "  " & strName7, 0, 12)
                    grid = New FPTable(0, 17, 320, 222, PageRow, 9)   '新增表格物件x,y,w,h,row,col (單位是公厘)  'w:324->320
                    grid.Font.Size = 11
                    grid.ColumnStyles(3).HAlignment = StringAlignment.Far
                    grid.ColumnStyles(4).HAlignment = StringAlignment.Near
                    grid.ColumnStyles(5).HAlignment = StringAlignment.Far
                    grid.ColumnStyles(6).HAlignment = StringAlignment.Far
                    grid.ColumnStyles(7).HAlignment = StringAlignment.Center
                    grid.ColumnStyles(8).HAlignment = StringAlignment.Far

                    grid.SetLineColor(Color.Blue)   'table line color 
                    grid.ColumnStyles(1).Width = 20    'tot364
                    grid.ColumnStyles(2).Width = 12
                    grid.ColumnStyles(3).Width = 15
                    grid.ColumnStyles(4).Width = 145  '147
                    grid.ColumnStyles(5).Width = 36
                    grid.ColumnStyles(6).Width = 36
                    grid.ColumnStyles(7).Width = 10
                    grid.ColumnStyles(8).Width = 36
                    grid.ColumnStyles(9).Width = 10   '12

                    grid.Cells2D(1, 1).ColSpan = 3 '12,3 隱藏
                    grid.Cells2D(1, 4).RowSpan = 2
                    grid.Cells2D(1, 5).RowSpan = 2
                    grid.Cells2D(1, 6).RowSpan = 2
                    grid.Cells2D(1, 7).RowSpan = 2
                    grid.Cells2D(1, 8).RowSpan = 2
                    grid.Cells2D(1, 9).RowSpan = 2
                    grid.Texts2D(1, 1).Text = "傳　   　票"
                    grid.Texts2D(2, 1).Text = "日　期"
                    grid.Texts2D(2, 2).Text = "種類"
                    grid.Texts2D(2, 3).Text = "號 數"
                    grid.Texts2D(1, 4).Text = "　　　　　　　　摘　　　　　　　　　　　要"
                    grid.Texts2D(1, 5).Text = "借      方  ."
                    grid.Texts2D(1, 6).Text = "貸      方  ."
                    grid.Texts2D(1, 7).Text = "借或貸"
                    grid.Texts2D(1, 8).Text = "餘　　　額　."
                    grid.Texts2D(1, 9).Text = "備註"
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
                If intR = 0 Then   '上月累計 or 上年度轉入
                    If AcuDeamt <> 0 Or AcuCramt <> 0 Then
                        '加實收付合計等等  because page beginning, so i 不會達到每頁最大列數
                        If acuAct <> 0 Then
                            i += 1
                            If Mid(strAccno, 1, 1) = "1" Then  '應收催收科目
                                grid.Texts2D(i, 4).Text = "　　上月實收累計"
                                grid.Texts2D(i, 6).Text = FormatNumber(acuAct, 2)
                            Else                               '應付科目
                                grid.Texts2D(i, 4).Text = "　　上月實付累計"
                                grid.Texts2D(i, 5).Text = FormatNumber(acuAct, 2)
                            End If
                        End If
                        If acuSub <> 0 Then
                            i += 1
                            If Mid(strAccno, 1, 1) = "1" Then
                                grid.Texts2D(i, 4).Text = "　　上月減免累計"
                                grid.Texts2D(i, 6).Text = FormatNumber(acuSub, 2)
                            Else
                                grid.Texts2D(i, 4).Text = "　　上月減免累計"
                                grid.Texts2D(i, 5).Text = FormatNumber(acuSub, 2)
                            End If
                        End If
                        If acuTrans <> 0 Then
                            i += 1
                            If Mid(strAccno, 1, 1) = "1" Then
                                grid.Texts2D(i, 4).Text = "　　上月轉催收累計"
                                grid.Texts2D(i, 6).Text = FormatNumber(acuTrans, 2)
                            Else
                                grid.Texts2D(i, 4).Text = "　　上月轉催收累計"
                                grid.Texts2D(i, 6).Text = FormatNumber(acuTrans, 2)
                            End If
                        End If
                        '正常之上月累計
                        i += 1
                        If nudSmonth.Value = 1 Then
                            strRemark = "　　上年度轉入"
                            grid.Texts2D(i, 1).Text = ShortDate(SDate)
                            grid.Texts2D(i, 2).Text = "轉"
                            grid.Texts2D(i, 3).Text = Format(1, "#####")
                        Else
                            strRemark = "　　上月累計"
                        End If
                        grid.Texts2D(i, 4).Text = strRemark
                        grid.Texts2D(i, 5).Text = FormatNumber(AcuDeamt, 2)
                        grid.Texts2D(i, 6).Text = FormatNumber(AcuCramt, 2)
                        grid.Texts2D(i, 7).Text = IIf(AcuDeamt > AcuCramt, "借", "貸")
                        If AcuDeamt = AcuCramt Then grid.Texts2D(i, 7).Text = "平"
                        grid.Texts2D(i, 8).Text = FormatNumber(Math.Abs(AcuDeamt - AcuCramt), 2)
                    End If
                End If
                '開始逐筆填入帳簿
                first_monTrans = "Y"   '第一次月份變動
                With mydataset.Tables("acf020")
                    Do While i < PageRow
                        If intR > .Rows.Count - 1 Then   '最後一筆 eof
                            '加實收付合計等等
                            If monAct <> 0 Then
                                i += 1
                                If i > PageRow Then Exit Do
                                If Mid(strAccno, 1, 1) = "1" Then
                                    grid.Texts2D(i, 4).Text = "　　本月實收合計"
                                    grid.Texts2D(i, 6).Text = FormatNumber(monAct, 2)
                                Else
                                    grid.Texts2D(i, 4).Text = "　　本月實付合計"
                                    grid.Texts2D(i, 5).Text = FormatNumber(monAct, 2)
                                End If
                                monAct = 0
                            End If
                            If acuAct <> 0 Then
                                i += 1
                                If i > PageRow Then Exit Do
                                If Mid(strAccno, 1, 1) = "1" Then
                                    grid.Texts2D(i, 4).Text = "　　本月實收累計"
                                    grid.Texts2D(i, 6).Text = FormatNumber(acuAct, 2)
                                Else
                                    grid.Texts2D(i, 4).Text = "　　本月實付累計"
                                    grid.Texts2D(i, 5).Text = FormatNumber(acuAct, 2)
                                End If
                                acuAct = 0
                            End If
                            '減免
                            If monSub <> 0 Then
                                i += 1
                                If i > PageRow Then Exit Do
                                If Mid(strAccno, 1, 1) = "1" Then
                                    grid.Texts2D(i, 4).Text = "　　本月減免合計"
                                    grid.Texts2D(i, 6).Text = FormatNumber(monSub, 2)
                                Else
                                    grid.Texts2D(i, 4).Text = "　　本月減免合計"
                                    grid.Texts2D(i, 5).Text = FormatNumber(monSub, 2)
                                End If
                                monSub = 0
                            End If
                            If acuSub <> 0 Then
                                i += 1
                                If i > PageRow Then Exit Do
                                If Mid(strAccno, 1, 1) = "1" Then
                                    grid.Texts2D(i, 4).Text = "　　本月減免累計"
                                    grid.Texts2D(i, 6).Text = FormatNumber(acuSub, 2)
                                Else
                                    grid.Texts2D(i, 4).Text = "　　本月減免累計"
                                    grid.Texts2D(i, 5).Text = FormatNumber(acuSub, 2)
                                End If
                                acuSub = 0
                            End If
                            '轉催收
                            If monTrans <> 0 Then
                                i += 1
                                If i > PageRow Then Exit Do
                                If Mid(strAccno, 1, 1) = "1" Then
                                    grid.Texts2D(i, 4).Text = "　　本月轉催收合計"
                                    grid.Texts2D(i, 6).Text = FormatNumber(monTrans, 2)
                                Else
                                    grid.Texts2D(i, 4).Text = "　　本月轉催收合計"
                                    grid.Texts2D(i, 5).Text = FormatNumber(monTrans, 2)
                                End If
                                monTrans = 0
                            End If
                            If acuTrans <> 0 Then
                                i += 1
                                If i > PageRow Then Exit Do
                                If Mid(strAccno, 1, 1) = "1" Then
                                    grid.Texts2D(i, 4).Text = "　　本月轉催收累計"
                                    grid.Texts2D(i, 6).Text = FormatNumber(acuTrans, 2)
                                Else
                                    grid.Texts2D(i, 4).Text = "　　本月轉催收累計"
                                    grid.Texts2D(i, 5).Text = FormatNumber(acuTrans, 2)
                                End If
                                acuTrans = 0
                            End If


                            '正常之月合計
                            If MonDeamt + MonCramt <> 0 Then
                                i += 1
                                If i > PageRow Then Exit Do
                                strRemark = "　　　　本　　月　　合　　計"
                                grid.Texts2D(i, 4).Text = strRemark
                                grid.Texts2D(i, 5).Text = FormatNumber(MonDeamt, 2)
                                grid.Texts2D(i, 6).Text = FormatNumber(MonCramt, 2)
                                MonDeamt = 0 : MonCramt = 0
                            End If
                            If AcuDeamt + AcuCramt <> 0 Then
                                i += 1
                                If i > PageRow Then Exit Do
                                strRemark = "　　　　本　　月　　累　　計"
                                grid.Texts2D(i, 4).Text = strRemark
                                grid.Texts2D(i, 5).Text = FormatNumber(AcuDeamt, 2)
                                grid.Texts2D(i, 6).Text = FormatNumber(AcuCramt, 2)
                                grid.Texts2D(i, 7).Text = IIf(AcuDeamt > AcuCramt, "借", "貸")
                                If AcuDeamt = AcuCramt Then grid.Texts2D(i, 7).Text = "平"
                                grid.Texts2D(i, 8).Text = FormatNumber(Math.Abs(AcuDeamt - AcuCramt), 2)
                                If strEnd <> "Y" Or AcuDeamt = AcuCramt Then AcuDeamt = 0 : AcuCramt = 0
                            End If
                            If strEnd = "Y" And AcuDeamt <> AcuCramt Then
                                i += 1
                                If i > PageRow Then Exit Do
                                grid.Texts2D(i, 4).Text = "　　　　結轉下年度"
                                grid.Texts2D(i, 1).Text = ShortDate(EDate)
                                grid.Texts2D(i, 2).Text = "轉"
                                grid.Texts2D(i, 3).Text = Format(intEndNo, "#####")
                                If AcuDeamt > AcuCramt Then
                                    grid.Texts2D(i, 6).Text = FormatNumber(AcuDeamt - AcuCramt, 2)
                                Else
                                    grid.Texts2D(i, 5).Text = FormatNumber(AcuCramt - AcuDeamt, 2)
                                End If
                                grid.Texts2D(i, 7).Text = "平"
                                grid.Texts2D(i, 8).Text = FormatNumber(0, 2)
                                AcuDeamt = 0 : AcuCramt = 0
                            End If
                            PageCnt = 1000
                            Exit Do
                            'Exit For
                        End If   'the end of record 

                        If Month(.Rows(intR).Item("date_2")) <> tempMonth Then  '月份不同時要印小計
                            If first_monTrans = "Y" Then  '第一次月份變動,藉以控制累計數之列印
                                tempAcuSub = acuSub : tempAcuAct = acuAct : tempAcuTrans = acuTrans
                                first_monTrans = "N"
                            End If
                            '加實收付合計等等
                            If monAct <> 0 Then
                                i += 1
                                If i > PageRow Then Exit Do
                                If Mid(strAccno, 1, 1) = "1" Then
                                    grid.Texts2D(i, 4).Text = "　　本月實收合計"
                                    grid.Texts2D(i, 6).Text = FormatNumber(monAct, 2)
                                Else
                                    grid.Texts2D(i, 4).Text = "　　本月實付合計"
                                    grid.Texts2D(i, 5).Text = FormatNumber(monAct, 2)
                                End If
                                monAct = 0
                            End If
                            If tempAcuAct <> 0 Then
                                i += 1
                                If i > PageRow Then Exit Do
                                If Mid(strAccno, 1, 1) = "1" Then
                                    grid.Texts2D(i, 4).Text = "　　本月實收累計"
                                    grid.Texts2D(i, 6).Text = FormatNumber(tempAcuAct, 2)
                                Else
                                    grid.Texts2D(i, 4).Text = "　　本月實付累計"
                                    grid.Texts2D(i, 5).Text = FormatNumber(tempAcuAct, 2)
                                End If
                                tempAcuAct = 0
                            End If
                            '不能將累計數清為0

                            '減免
                            If monSub <> 0 Then
                                i += 1
                                If i > PageRow Then Exit Do
                                If Mid(strAccno, 1, 1) = "1" Then
                                    grid.Texts2D(i, 4).Text = "　　本月減免合計"
                                    grid.Texts2D(i, 6).Text = FormatNumber(monSub, 2)
                                Else
                                    grid.Texts2D(i, 4).Text = "　　本月減免合計"
                                    grid.Texts2D(i, 5).Text = FormatNumber(monSub, 2)
                                End If
                                monSub = 0
                            End If
                            If tempAcuSub <> 0 Then
                                i += 1
                                If i > PageRow Then Exit Do
                                If Mid(strAccno, 1, 1) = "1" Then
                                    grid.Texts2D(i, 4).Text = "　　本月減免累計"
                                    grid.Texts2D(i, 6).Text = FormatNumber(tempAcuSub, 2)
                                Else
                                    grid.Texts2D(i, 4).Text = "　　本月減免累計"
                                    grid.Texts2D(i, 5).Text = FormatNumber(tempAcuSub, 2)
                                End If
                                tempAcuSub = 0
                            End If

                            '轉催收
                            If monTrans <> 0 Then
                                i += 1
                                If i > PageRow Then Exit Do
                                If Mid(strAccno, 1, 1) = "1" Then
                                    grid.Texts2D(i, 4).Text = "　　本月轉催收合計"
                                    grid.Texts2D(i, 6).Text = FormatNumber(monTrans, 2)
                                Else
                                    grid.Texts2D(i, 4).Text = "　　本月轉催收合計"
                                    grid.Texts2D(i, 5).Text = FormatNumber(monTrans, 2)
                                End If
                                monTrans = 0
                            End If
                            If tempAcuTrans <> 0 Then
                                i += 1
                                If i > PageRow Then Exit Do
                                If Mid(strAccno, 1, 1) = "1" Then
                                    grid.Texts2D(i, 4).Text = "　　本月轉催收累計"
                                    grid.Texts2D(i, 6).Text = FormatNumber(tempAcuTrans, 2)
                                Else
                                    grid.Texts2D(i, 4).Text = "　　本月轉催收累計"
                                    grid.Texts2D(i, 5).Text = FormatNumber(tempAcuTrans, 2)
                                End If
                                tempAcuTrans = 0
                            End If

                            ' '正常之月合計
                            If MonDeamt + MonCramt <> 0 Then
                                i += 1
                                If i > PageRow Then Exit Do
                                strRemark = "　　　　本　　月　　合　　計"
                                grid.Texts2D(i, 4).Text = strRemark
                                grid.Texts2D(i, 5).Text = FormatNumber(MonDeamt, 2)
                                grid.Texts2D(i, 6).Text = FormatNumber(MonCramt, 2)
                                MonDeamt = 0 : MonCramt = 0
                            End If
                            If AcuDeamt + AcuCramt <> 0 Then
                                i += 1
                                If i > PageRow Then Exit Do
                                strRemark = "　　　　本　　月　　累　　計"
                                grid.Texts2D(i, 4).Text = strRemark
                                grid.Texts2D(i, 5).Text = FormatNumber(AcuDeamt, 2)
                                grid.Texts2D(i, 6).Text = FormatNumber(AcuCramt, 2)
                                grid.Texts2D(i, 7).Text = IIf(AcuDeamt > AcuCramt, "借", "貸")
                                If AcuDeamt = AcuCramt Then grid.Texts2D(i, 7).Text = "平"
                                grid.Texts2D(i, 8).Text = FormatNumber(Math.Abs(AcuDeamt - AcuCramt), 2)
                            End If
                            tempMonth = Month(.Rows(intR).Item("date_2"))
                            first_monTrans = "Y"
                        End If

                        i += 1
                        If i > PageRow Then Exit Do '換新頁

                        grid.Texts2D(i, 1).Text = ShortDate(.Rows(intR).Item("date_2"))
                        grid.Texts2D(i, 2).Text = IIf(.Rows(intR).Item("kind") = "1", "收", "支")
                        If .Rows(intR).Item("kind") >= "3" Then grid.Texts2D(i, 2).Text = "轉"
                        grid.Texts2D(i, 3).Text = Format(.Rows(intR).Item("no_2_no"), 0)
                        If Len(.Rows(intR).Item("accno")) = 17 Then  '有8級科目
                            sqlstr = "select * from accname where accno='" & .Rows(intR).Item("accno") & "'"
                            myds = openmember("", "accname", sqlstr)
                            If myds.Tables(0).Rows.Count > 0 Then
                                grid.Texts2D(i, 4).Text = nz(myds.Tables(0).Rows(0).Item("accname"), "") & "  " & nz(.Rows(intR).Item("remark"), "")  '摘要
                            Else
                                grid.Texts2D(i, 4).Text = nz(.Rows(intR).Item("accname"), "") & "  " & nz(.Rows(intR).Item("remark"), "")  '摘要
                            End If
                        Else
                            grid.Texts2D(i, 4).Text = nz(.Rows(intR).Item("remark"), "")  '摘要
                        End If
                        If .Rows(intR).Item("dc") = "1" Then   '借方
                            grid.Texts2D(i, 5).Text = FormatNumber(.Rows(intR).Item("amt"), 2)
                            MonDeamt += .Rows(intR).Item("amt")
                            AcuDeamt += .Rows(intR).Item("amt")
                            If .Rows(intR).Item("cotn_code") = "2" Then   '實收付
                                If Mid(strAccno, 1, 1) = "1" Then '資產
                                    monAct -= .Rows(intR).Item("amt")
                                    acuAct -= .Rows(intR).Item("amt")
                                Else
                                    monAct += .Rows(intR).Item("amt")
                                    acuAct += .Rows(intR).Item("amt")
                                End If
                            End If
                            If .Rows(intR).Item("cotn_code") = "3" Then   '減免
                                If Mid(strAccno, 1, 1) = "1" Then '資產
                                    monSub -= .Rows(intR).Item("amt")
                                    acuSub -= .Rows(intR).Item("amt")
                                Else
                                    monSub += .Rows(intR).Item("amt")
                                    acuSub += .Rows(intR).Item("amt")
                                End If
                            End If
                            If .Rows(intR).Item("cotn_code") = "4" Then   '轉催收
                                If Mid(strAccno, 1, 1) = "1" Then '資產
                                    monTrans -= .Rows(intR).Item("amt")
                                    acuTrans -= .Rows(intR).Item("amt")
                                Else
                                    monTrans += .Rows(intR).Item("amt")
                                    acuTrans += .Rows(intR).Item("amt")
                                End If
                            End If
                        Else                                    '貸方
                            grid.Texts2D(i, 6).Text = FormatNumber(.Rows(intR).Item("amt"), 2)
                            MonCramt += .Rows(intR).Item("amt")
                            AcuCramt += .Rows(intR).Item("amt")
                            If .Rows(intR).Item("cotn_code") = "2" Then   '實收付
                                If Mid(strAccno, 1, 1) = "1" Then '資產
                                    monAct += .Rows(intR).Item("amt")
                                    acuAct += .Rows(intR).Item("amt")
                                Else
                                    monAct -= .Rows(intR).Item("amt")
                                    acuAct -= .Rows(intR).Item("amt")
                                End If
                            End If
                            If .Rows(intR).Item("cotn_code") = "3" Then   '減免
                                If Mid(strAccno, 1, 1) = "1" Then '資產
                                    monSub += .Rows(intR).Item("amt")
                                    acuSub += .Rows(intR).Item("amt")
                                Else
                                    monSub -= .Rows(intR).Item("amt")
                                    acuSub -= .Rows(intR).Item("amt")
                                End If
                            End If
                            If .Rows(intR).Item("cotn_code") = "4" Then   '轉催收
                                If Mid(strAccno, 1, 1) = "1" Then '資產
                                    monTrans += .Rows(intR).Item("amt")
                                    acuTrans += .Rows(intR).Item("amt")
                                Else
                                    monTrans -= .Rows(intR).Item("amt")
                                    acuTrans -= .Rows(intR).Item("amt")
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
