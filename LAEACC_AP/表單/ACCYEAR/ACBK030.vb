﻿Imports JBC.Printing
Public Class ACBK030
    Dim confid, confunit As String, LoadAfter, Dirty As Boolean
    Dim sqlstr As String
    Dim strAccno As String
    Dim SYear As Integer
    Dim SDate, EDate As String
    'Dim strAccount, strBankname As String   '銀行名稱
    Dim mydataset, myds, mydsD As DataSet
    Dim PageRow As Integer = 37  '每頁印37行

    Private Sub ACBK030_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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
        vxtSAccno.Text = "11401"    '起值
        vxtEAccno.Text = "114019"   '迄值
        If TransPara.TransP("UnitTitle").indexof("嘉南") >= 0 Then
            PageRow = 25  '每頁印30行
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

    Private Sub BtnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrint.Click
        BtnPrint.Enabled = False
        Dim intR As Integer = 0  'control record number
        Dim intI, intD, i As Integer
        Dim AcuDeamt, AcuCramt, AcuDeQty, AcuCrQty As Decimal
        Dim MonDeamt, MonCramt, MonDeQty, MonCrQty As Decimal
        Dim retstr, strRemark, strKind, strUnit As String
        Dim strName5, strName7 As String
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

        Dim printer = New KPrint
        Dim document As New FPDocument("列印材料明細分類帳")
        document.DefaultPageSettings.PaperKind = Printing.PaperKind.B4  'A3:420x297  B4:364X257
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
        Dim ggrid As New FPTable(0, 17, 324, 222, PageRow, 14)
        '宣告一變數儲存是否符合頁碼範圍
        Dim blnIsBelong As Boolean
        Dim blnIsPrint As Boolean = True  '記錄是否有資料頁列印

        'strRemark = "上年度轉入"

        '由acf050逐科目列印 取最明細科目
        Dim sqlstr2 As String
        If up = "00" Then
            sqlstr2 = " a.beg_debit as deamt, a.beg_credit as cramt, g.qty_beg as DeQty, 0 as CrQty "
        Else
            sqlstr2 = " a.deamt" & up & " as deamt, a.cramt" & up & " as cramt, " & _
                      " g.qtyin" & up & " as DeQty, g.qtyout" & up & " as CrQty "
        End If
        sqlstr2 += ",  a.deamt" & mm & " as deamtmm, a.cramt" & mm & " as cramtmm"
        sqlstr = "SELECT a.accno, c.accname, d.accname as name5, g.kind, g.unit, " & sqlstr2 & " FROM ACF050 a " & _
                 " LEFT OUTER JOIN ACCNAME c ON a.ACCNO = c.ACCNO " & _
                 " LEFT OUTER JOIN ACCNAME d ON RTRIM(LEFT(a.ACCNO, 7)) = d.ACCNO " & _
                 " left outer join ACF100 g on a.accno=g.accno and a.accyear=g.accyear " & _
                 " WHERE (a.ACCYEAR =" & SYear & ") AND (a.ACCNO BETWEEN '" & Saccno & "' AND '" & Eaccno & "') AND " & _
                 " (a.ACCNO NOT IN (SELECT LEFT(b.accno, len(a.accno)) AS accno " & _
                 " FROM (SELECT accno FROM acf050 WHERE accyear =" & SYear & "AND " & _
                 " accno BETWEEN '" & Saccno & "' AND '" & Eaccno & "') b " & _
                 " WHERE len(a.accno) < 10 and a.accno = LEFT(b.accno, len(a.accno)) AND a.accno <> b.accno)) " & _
                 " ORDER BY  a.ACCNO"
        mydsD = openmember("", "DataAccno", sqlstr)
        For intD = 0 To mydsD.Tables("DataAccno").Rows.Count - 1
            If rdbPage.Checked Then  '頁次大於控制列印範圍時就結束
                If Val(txtNo.Text) > Val(txtPrintPageE.Text) Then Exit For
            End If

            strAccno = nz(mydsD.Tables("DataAccno").Rows(intD).Item("accno"), "") '列印科目
            strKind = nz(mydsD.Tables("DataAccno").Rows(intD).Item("kind"), "") '材料規格
            strUnit = nz(mydsD.Tables("DataAccno").Rows(intD).Item("unit"), "") '材料單位
            strName7 = nz(mydsD.Tables("DataAccno").Rows(intD).Item("accname"), "") '列印科目
            strName5 = nz(mydsD.Tables("DataAccno").Rows(intD).Item("name5"), "") '列印科目
            '上月累計or上年度轉入
            AcuDeamt = nz(mydsD.Tables("DataAccno").Rows(intD).Item("deamt"), 0)
            AcuCramt = nz(mydsD.Tables("DataAccno").Rows(intD).Item("cramt"), 0)
            AcuDeQty = nz(mydsD.Tables("DataAccno").Rows(intD).Item("DeQty"), 0)     '累計in數量
            AcuCrQty = nz(mydsD.Tables("DataAccno").Rows(intD).Item("CrQty"), 0)    '累計out數量
            If nudSmonth.Value = 1 Then
                MonDeamt = AcuDeamt
                MonCramt = AcuCramt
                MonDeQty = AcuDeQty
                MonCrQty = AcuCrQty
            Else
                MonDeamt = 0 : MonCramt = 0 : MonDeQty = 0 : MonCrQty = 0
                If AcuDeamt = nz(mydsD.Tables("DataAccno").Rows(intD).Item("deamtmm"), 0) And AcuCramt = nz(mydsD.Tables("DataAccno").Rows(intD).Item("cramtmm"), 0) Then
                    GoTo nextfor    '本月無異動傳票
                End If
            End If
            intR = 0

            '找acf020傳票資料(table body record )
            'sqlstr = "SELECT a.*, b.date_2 as date_2, c.accname FROM acf020 a left outer join acf010 b on a.accyear=b.accyear and a.kind=b.kind " & _
            '         "and a.no_2_no=b.no_2_no and a.seq=b.seq and b.item='1' left outer join accname c on a.accno=c.accno " & _
            '         " where b.date_2 between '" & SDate & "' and '" & EDate & _
            '         "' and rtrim(left(a.accno,16))='" & strAccno & "' order by b.date_2, a.kind, a.no_2_no "
            sqlstr = "SELECT a.*, b.date_2 as date_2, c.accname FROM " & _
                     " (select * from acf020 where accyear>=" & SYear & " and rtrim(left(accno,16))='" & strAccno & "') a " & _
                     " left outer join acf010 b on a.accyear=b.accyear and a.kind=b.kind " & _
                     " and a.no_2_no=b.no_2_no and a.seq=b.seq and b.item='1' left outer join accname c on a.accno=c.accno " & _
                     " where b.date_2 between '" & SDate & "' and '" & EDate & _
                     "' order by b.date_2, a.kind, a.no_2_no "

            mydataset = openmember("", "acf020", sqlstr)

            If mydataset.Tables("acf020").Rows.Count > 0 Then
                tempMonth = Month(mydataset.Tables("acf020").Rows(0).Item("date_2"))  '記錄第一筆之月份
            Else   '沒有資料
                If Not (nudSmonth.Value = 1 And AcuDeamt + AcuCramt <> 0) Then  '有上年度轉入數
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
                    textTitle = New FPText("材料明細分類帳", 90, 6)
                    textTitle.HAlignment = FPAlignment.Center
                    textTitle.Font.Size = 14
                    textyear = New FPText("中華民國" & SYear & "年度", 90, 12)
                    textyear.HAlignment = FPAlignment.Center
                    textyear.Font.Size = 11
                    'to be continue
                    textPage = New FPText("第 " & txtNo.Text & " 頁", 300, 12)
                    textName4 = New FPText("科目：" & FormatAccno(strAccno) & "  " & strName5, 0, 6)
                    textName7 = New FPText("名稱：" & strName7 & "  規格：" & strKind & "  計算單位：" & strUnit, 0, 12)
                    grid = New FPTable(0, 17, 324, 222, PageRow, 14)   '新增表格物件x,y,w,h,row,col (單位是公厘)
                    grid.Font.Size = 10
                    grid.ColumnStyles(3).HAlignment = StringAlignment.Far
                    grid.ColumnStyles(4).HAlignment = StringAlignment.Near
                    'grid.ColumnStyles(5).HAlignment = StringAlignment.Far
                    grid.ColumnStyles(6).HAlignment = StringAlignment.Far
                    grid.ColumnStyles(7).HAlignment = StringAlignment.Far
                    grid.ColumnStyles(8).HAlignment = StringAlignment.Far
                    grid.ColumnStyles(9).HAlignment = StringAlignment.Far
                    grid.ColumnStyles(10).HAlignment = StringAlignment.Far
                    grid.ColumnStyles(11).HAlignment = StringAlignment.Far
                    grid.ColumnStyles(12).HAlignment = StringAlignment.Far
                    grid.ColumnStyles(13).HAlignment = StringAlignment.Far
                    grid.ColumnStyles(14).HAlignment = StringAlignment.Far

                    grid.SetLineColor(Color.Blue)   'table line color 
                    grid.ColumnStyles(1).Width = 17    'tot364
                    grid.ColumnStyles(2).Width = 10
                    grid.ColumnStyles(3).Width = 11 '13
                    grid.ColumnStyles(4).Width = 79
                    grid.ColumnStyles(5).Width = 6   '10
                    grid.ColumnStyles(6).Width = 20
                    grid.ColumnStyles(7).Width = 17  '15
                    grid.ColumnStyles(8).Width = 30
                    grid.ColumnStyles(9).Width = 20
                    grid.ColumnStyles(10).Width = 17  '15
                    grid.ColumnStyles(11).Width = 30
                    grid.ColumnStyles(12).Width = 20
                    grid.ColumnStyles(13).Width = 17  '15
                    grid.ColumnStyles(14).Width = 30

                    grid.Cells2D(1, 1).ColSpan = 3 '12,3 隱藏
                    grid.Cells2D(1, 6).ColSpan = 3
                    grid.Cells2D(1, 9).ColSpan = 3
                    grid.Cells2D(1, 12).ColSpan = 3
                    grid.Cells2D(1, 4).RowSpan = 2
                    grid.Cells2D(1, 5).RowSpan = 2
                    grid.Texts2D(1, 1).Text = "傳　   　票"
                    grid.Texts2D(2, 1).Text = "日　期"
                    grid.Texts2D(2, 2).Text = "種類"
                    grid.Texts2D(2, 3).Text = "號 數"
                    grid.Texts2D(1, 4).Text = "　　　　　摘　　　　　　　　要"
                    grid.Texts2D(1, 6).Text = "　收                                    入      ."
                    grid.Texts2D(1, 9).Text = "　發                                    出      ."
                    grid.Texts2D(1, 12).Text = "　餘                                    額      ."
                    grid.Texts2D(1, 5).Text = "收領料單號數"
                    grid.Texts2D(2, 6).Text = "數   量"
                    grid.Texts2D(2, 7).Text = "單 價"
                    grid.Texts2D(2, 8).Text = "金　　　額"
                    grid.Texts2D(2, 9).Text = "數   量"
                    grid.Texts2D(2, 10).Text = "單 價"
                    grid.Texts2D(2, 11).Text = "金　　　額"
                    grid.Texts2D(2, 12).Text = "數   量"
                    grid.Texts2D(2, 13).Text = "單 價"
                    grid.Texts2D(2, 14).Text = "金　　　額"
                    grid.Texts2D(1, 5).Font.Size = 6
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
                    If AcuDeamt <> 0 Or AcuCramt <> 0 Then
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
                        grid.Texts2D(i, 6).Text = Format(AcuDeQty, "###,###,###,###.######")
                        grid.Texts2D(i, 7).Text = Format(rate(AcuDeamt, AcuDeQty * 100, 2), "###,###,###.##")
                        grid.Texts2D(i, 8).Text = Format(AcuDeamt, "###,###,###,##0.00")
                        grid.Texts2D(i, 9).Text = Format(AcuCrQty, "###,###,###,###.######")
                        grid.Texts2D(i, 10).Text = Format(rate(AcuCramt, AcuCrQty * 100, 2), "###,###,###.##")
                        grid.Texts2D(i, 11).Text = Format(AcuCramt, "###,###,###,##0.00")
                        grid.Texts2D(i, 12).Text = Format(AcuDeQty - AcuCrQty, "###,###,###,###.##")
                        grid.Texts2D(i, 13).Text = Format(rate(AcuDeamt - AcuCramt, (AcuDeQty - AcuCrQty) * 100, 2), "###,###,###.##")
                        grid.Texts2D(i, 14).Text = Format(AcuDeamt - AcuCramt, "###,###,###,##0.00")
                    End If
                End If

                With mydataset.Tables("acf020")
                    Do While i < PageRow
                        If intR > .Rows.Count - 1 Then
                            If MonDeamt + MonCramt <> 0 Then
                                i += 1
                                If i > PageRow Then Exit Do
                                strRemark = "　　　　本　　月　　合　　計"
                                grid.Texts2D(i, 4).Text = strRemark
                                grid.Texts2D(i, 6).Text = Format(MonDeQty, "###,###,###,###.######")
                                grid.Texts2D(i, 7).Text = Format(rate(MonDeamt, MonDeQty * 100, 2), "###,###,###.##")
                                grid.Texts2D(i, 8).Text = Format(MonDeamt, "###,###,###,##0.00")
                                grid.Texts2D(i, 9).Text = Format(MonCrQty, "###,###,###,###.######")
                                grid.Texts2D(i, 10).Text = Format(rate(MonCramt, MonCrQty * 100, 2), "###,###,###.##")
                                grid.Texts2D(i, 11).Text = Format(MonCramt, "###,###,###,##0.00")
                                MonDeamt = 0 : MonCramt = 0 : MonDeQty = 0 : MonCrQty = 0
                            End If
                            If AcuDeamt + AcuCramt <> 0 Then
                                i += 1
                                If i > PageRow Then Exit Do
                                strRemark = "　　　　本　　月　　累　　計"
                                grid.Texts2D(i, 4).Text = strRemark
                                grid.Texts2D(i, 6).Text = Format(AcuDeQty, "###,###,###,###.######")
                                grid.Texts2D(i, 7).Text = Format(rate(AcuDeamt, AcuDeQty * 100, 2), "###,###,###.##")
                                grid.Texts2D(i, 8).Text = Format(AcuDeamt, "###,###,###,##0.00")
                                grid.Texts2D(i, 9).Text = Format(AcuCrQty, "###,###,###,###.######")
                                grid.Texts2D(i, 10).Text = Format(rate(AcuCramt, AcuCrQty * 100, 2), "###,###,###.##")
                                grid.Texts2D(i, 11).Text = Format(AcuCramt, "###,###,###,##0.00")
                                grid.Texts2D(i, 12).Text = Format(AcuDeQty - AcuCrQty, "###,###,###,###.##")
                                grid.Texts2D(i, 13).Text = Format(rate(AcuDeamt - AcuCramt, (AcuDeQty - AcuCrQty) * 100, 2), "###,###,###.##")
                                grid.Texts2D(i, 14).Text = Format(AcuDeamt - AcuCramt, "###,###,###,##0.00")
                                If strEnd <> "Y" Or AcuDeamt = AcuCramt Then AcuDeamt = 0 : AcuCramt = 0 : AcuDeQty = 0 : AcuCrQty = 0
                            End If
                            If strEnd = "Y" And AcuDeamt <> AcuCramt Then
                                i += 1
                                If i > PageRow Then Exit Do
                                grid.Texts2D(i, 4).Text = "　　　　結轉下年度"
                                grid.Texts2D(i, 1).Text = ShortDate(EDate)
                                grid.Texts2D(i, 2).Text = "轉"
                                grid.Texts2D(i, 3).Text = Format(intEndNo, "#####")
                                If AcuDeamt > AcuCramt Then
                                    grid.Texts2D(i, 11).Text = FormatNumber(AcuDeamt - AcuCramt, 2)
                                Else
                                    grid.Texts2D(i, 8).Text = FormatNumber(AcuCramt - AcuDeamt, 2)
                                End If
                                AcuDeamt = 0 : AcuCramt = 0 : AcuDeQty = 0 : AcuCrQty = 0
                            End If
                            PageCnt = 1000
                            Exit Do
                            'Exit For
                        End If   'the end of record 

                        If Month(.Rows(intR).Item("date_2")) <> tempMonth Then  '日期不同時要印小計
                            If MonDeamt + MonCramt <> 0 Then
                                i += 1
                                If i > PageRow Then Exit Do
                                strRemark = "　　　　本　　月　　合　　計"
                                grid.Texts2D(i, 4).Text = strRemark
                                grid.Texts2D(i, 6).Text = Format(MonDeQty, "###,###,###,###.######")
                                grid.Texts2D(i, 7).Text = Format(rate(MonDeamt, MonDeQty * 100, 2), "###,###,###.##")
                                grid.Texts2D(i, 8).Text = Format(MonDeamt, "###,###,###,##0.00")
                                grid.Texts2D(i, 9).Text = Format(MonCrQty, "###,###,###,###.######")
                                grid.Texts2D(i, 10).Text = Format(rate(MonCramt, MonCrQty * 100, 2), "###,###,###.##")
                                grid.Texts2D(i, 11).Text = Format(MonCramt, "###,###,###,##0.00")
                                MonDeamt = 0 : MonCramt = 0 : MonDeQty = 0 : MonCrQty = 0
                            End If
                            If AcuDeamt + AcuCramt <> 0 Then
                                i += 1
                                If i > PageRow Then Exit Do
                                strRemark = "　　　　本　　月　　累　　計"
                                grid.Texts2D(i, 4).Text = strRemark
                                grid.Texts2D(i, 6).Text = Format(AcuDeQty, "###,###,###,###.######")
                                grid.Texts2D(i, 7).Text = Format(rate(AcuDeamt, AcuDeQty * 100, 2), "###,###,###.##")
                                grid.Texts2D(i, 8).Text = Format(AcuDeamt, "###,###,###,##0.00")
                                grid.Texts2D(i, 9).Text = Format(AcuCrQty, "###,###,###,###.######")
                                grid.Texts2D(i, 10).Text = Format(rate(AcuCramt, AcuCrQty * 100, 2), "###,###,###.##")
                                grid.Texts2D(i, 11).Text = Format(AcuCramt, "###,###,###,##0.00")
                                grid.Texts2D(i, 12).Text = Format(AcuDeQty - AcuCrQty, "###,###,###,###.##")
                                grid.Texts2D(i, 13).Text = Format(rate(AcuDeamt - AcuCramt, (AcuDeQty - AcuCrQty) * 100, 2), "###,###,###.##")
                                grid.Texts2D(i, 14).Text = Format(AcuDeamt - AcuCramt, "###,###,###,##0.00")
                            End If
                            tempMonth = Month(.Rows(intR).Item("date_2"))
                        End If

                        i += 1
                        If i > PageRow Then Exit Do '換新頁

                        grid.Texts2D(i, 1).Text = ShortDate(.Rows(intR).Item("date_2"))
                        grid.Texts2D(i, 2).Text = IIf(.Rows(intR).Item("kind") = "1", "收", "支")
                        If .Rows(intR).Item("kind") >= "3" Then grid.Texts2D(i, 2).Text = "轉"
                        grid.Texts2D(i, 3).Text = Format(.Rows(intR).Item("no_2_no"), 0)
                        grid.Texts2D(i, 4).Text = nz(.Rows(intR).Item("remark"), "")  '摘要
                        If .Rows(intR).Item("dc") = "1" Then   '借方
                            grid.Texts2D(i, 6).Text = Format(.Rows(intR).Item("mat_qty"), "###,###,###,###.######")
                            grid.Texts2D(i, 7).Text = Format(rate(.Rows(intR).Item("amt"), .Rows(intR).Item("mat_qty") * 100, 2), "###,###,###.##")
                            grid.Texts2D(i, 8).Text = Format(.Rows(intR).Item("amt"), "###,###,###,##0.00")
                            MonDeamt += .Rows(intR).Item("amt")
                            AcuDeamt += .Rows(intR).Item("amt")
                            AcuDeQty += .Rows(intR).Item("mat_qty")
                            MonDeQty += .Rows(intR).Item("mat_qty")
                        Else                                    '貸方
                            grid.Texts2D(i, 9).Text = Format(.Rows(intR).Item("mat_qty"), "###,###,###,###.######")
                            grid.Texts2D(i, 10).Text = Format(rate(.Rows(intR).Item("amt"), .Rows(intR).Item("mat_qty") * 100, 2), "###,###,###.##")
                            grid.Texts2D(i, 11).Text = Format(.Rows(intR).Item("amt"), "###,###,###,##0.00")
                            MonCramt += .Rows(intR).Item("amt")
                            AcuCramt += .Rows(intR).Item("amt")
                            AcuCrQty += .Rows(intR).Item("mat_qty")
                            MonCrQty += .Rows(intR).Item("mat_qty")
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
