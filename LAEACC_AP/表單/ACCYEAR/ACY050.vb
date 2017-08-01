Imports JBC.Printing
Imports System.IO
Imports Microsoft.Office.Interop
Public Class ACY050
    Dim confid, confunit As String, LoadAfter, Dirty As Boolean
    Dim SYear As Integer
    Dim myds4, myds5 As DataSet
    Dim xlCells As Excel.Range
    Dim sqlstr, retstr As String
    Private Sub ACY050_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dtpDateS.Value = TransPara.TransP("LastDay")
    End Sub

    Private Sub LoadGridFunc()
        Dim intAmt As Decimal = 0
        '先清空acm010 
        sqlstr = "delete from acm010"
        retstr = runsql(mastconn, sqlstr)

        '將本年度acf050四級科目 & 預算數四級科目丟入acm010  
        sqlstr = "INSERT INTO ACM010  SELECT c.ACCNO, c.ACCNAME," & _
                " a.DEAMT12 AS amt1, a.CRAMT12 AS amt2, B.BG1+BG2+BG3+BG4+BG5 AS amt3, 0 AS AMT4, 0 as amt5, 0 as amt6, 0 as amt7 from " & _
         " (select * from accname where len(accno)=5 and substring(accno,1,1)>='4') c left outer join " & _
         " (select * from ACF050 where accyear=" & SYear & " and len(accno)=5 ) a on c.accno=a.accno " & _
         "left outer join " & _
         "(select *  from accbg where accyear=" & SYear & " and len(accno)=5 ) b on c.accno=b.accno "
        retstr = runsql(mastconn, sqlstr)
        '將null清為0
        sqlstr = " update acm010 set amt1=0 where amt1 is null"
        retstr = runsql(mastconn, sqlstr)
        sqlstr = " update acm010 set amt2=0 where amt2 is null"
        retstr = runsql(mastconn, sqlstr)
        sqlstr = " update acm010 set amt3=0 where amt3 is null"
        retstr = runsql(mastconn, sqlstr)

        '各欄數值計算(本年度決算數amt1,預算數amt3)
        sqlstr = " Update ACM010 SET amt1 = amt2 - amt1, amt2 = 0 where substring(accno,1,1)='4'"
        retstr = runsql(mastconn, sqlstr)
        sqlstr = " Update ACM010 SET amt1 = amt1 - amt2, amt2 = 0 where substring(accno,1,1)='5'"
        retstr = runsql(mastconn, sqlstr)
        'If retstr <> "sqlok" Then MsgBox("update acm010 error " & sqlstr)

        '刪全為0
        sqlstr = "delete FROM acm010 where amt1=0 and amt2=0 and amt3=0 and amt4=0"
        retstr = runsql(mastconn, sqlstr)

        ''統計三級
        sqlstr = "INSERT INTO ACM010 select a.accno, b.accname, a.amt1, a.amt2, a.amt3, " & _
                 "a.amt4, a.amt5, a.amt6, a.amt7 from " & _
                 "(SELECT substring(accno, 1, 3) AS accno, SUM(amt1) AS amt1, " & _
                 "SUM(amt2) AS amt2, SUM(amt3) AS amt3, SUM(amt4) AS amt4, " & _
                 "SUM(amt5) AS amt5, SUM(amt6) AS amt6, 0 AS amt7 from acm010 " & _
                 "GROUP BY substring(accno, 1, 3)) a left outer join ACCNAME b ON a.accno = b.ACCNO"
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then MsgBox("統計三級 error " & sqlstr)

        ''統計二級
        sqlstr = "INSERT INTO ACM010 select a.accno, b.accname, a.amt1, a.amt2, a.amt3, " & _
                 "a.amt4, a.amt5, a.amt6, a.amt7 from " & _
                 "(SELECT substring(accno, 1, 2) AS accno, SUM(amt1) AS amt1, " & _
                 "SUM(amt2) AS amt2, SUM(amt3) AS amt3, SUM(amt4) AS amt4, " & _
                 "SUM(amt5) AS amt5, SUM(amt6) AS amt6, 0 AS amt7 from acm010 " & _
                 "where len(accno)=3 " & _
                 "GROUP BY substring(accno, 1, 2)) a left outer join ACCNAME b ON a.accno = b.ACCNO"
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then MsgBox("統計二級 error " & sqlstr)

        '丟入dataset
        '收入
        sqlstr = "SELECT * FROM acm010 where substring(accno,1,1)='4' order by accno "
        myds4 = openmember("", "acm010", sqlstr)
        '支出
        sqlstr = "SELECT * FROM acm010 where substring(accno,1,1)='5' order by accno "
        myds5 = openmember("", "acm010", sqlstr)
    End Sub

    Private Sub BtnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrint.Click
        Dim xlapp As Excel.Application
        Dim xlbook As Excel.Workbook
        Dim xlbooks As Excel.Workbooks
        Dim xlsheet As Excel.Worksheet
        Dim xlsheets As Excel.Sheets        '  Dim xlRange As Excel.Range
        Dim xlRange As Excel.Range
        Dim xlRange1 As Excel.Range
        Dim xlRange2 As Excel.Range
        Dim xlRange3 As Excel.Range
        Dim intR As Integer = 0 'control record number
        Dim intTempR As Integer = 0
        Dim i As Integer = 0     'control excel row 
        Dim strAccno, strAccname As String
        Dim strS, strT As String
        Dim spaces As Integer
        Dim Tot41, Tot42, TOT51, TOT52 As Decimal
        Tot41 = 0 : Tot42 = 0 : TOT51 = 0 : TOT52 = 0
        Dim Sum41, Sum42, Sum51, Sum52 As Decimal
        Sum41 = 0 : Sum42 = 0 : Sum51 = 0 : Sum52 = 0
        Dim intAmt1, intAmt2 As Decimal
        'Dim TempAccno22, TempAccno24, TempAccno32, TempAccno34, TempAccno323 As Decimal

        Try
            SYear = GetYear(dtpDateS.Value)
            TransPara.TransP("LastDay") = dtpDateS.Value

            Call LoadGridFunc()
            If myds4.Tables("acm010").Rows.Count <= 1 Or myds5.Tables("acm010").Rows.Count <= 1 Then
                MsgBox("無此資料")
                Exit Sub
            End If

            Dim tt1, tt2 As String
            Try
                tt1 = "c:\App\acc\報表樣本\ACy050.xls"
                tt2 = "c:\App\acc\報表\ACY050.xls"
                If Not File.Exists(tt1) Then
                    AppReport_Copy("acc", "ACY050.xls", tt1)
                    'MsgBox("找不到報表樣本，請洽資訊人員" & vbNewLine & tt1)
                    'Exit Sub
                End If
                FileCopy(tt1, tt2)    'copy tt1 to tt2
                xlapp = CreateObject("excel.application")
                xlbooks = xlapp.Workbooks
                xlbook = xlbooks.Open(tt2) '開啟tt2
            Catch ex As Exception
                MsgBox("報表copy " & tt1 & "  to  " & tt2 & "錯誤，是否\報表\ACY010.XLS使用中,否則請洽程式設計人員!")
                Exit Sub
            End Try
            xlsheets = xlbook.Worksheets
            xlsheet = xlsheets(1)
            NAR(xlCells)
            xlCells = xlsheet.Cells

            '公司名稱
            If TransPara.TransP("UnitTitle") <> "" Then
                xlRange = xlsheet.Range("A1")
                xlRange.Value = TransPara.TransP("UnitTitle")
                NAR(xlRange)
            End If
            xlRange = xlsheet.Range("A3")
            xlRange.Value = "中華民國 " & SYear & " 年度"
            NAR(xlRange)
            Dim LastR As Integer = myds5.Tables("acm010").Rows.Count - 1 '定義最大筆數,以決定excel行數
            If myds4.Tables("acm010").Rows.Count - 1 > LastR Then
                LastR = myds4.Tables("acm010").Rows.Count - 1
            End If

            i = 5    '自第6行開始放
            For intR = 0 To LastR
                i += 1   '自第6行開始放
                '拷貝目前這列到下一列,使得每列都有相同的格式設定
                xlRange1 = xlsheet.Range("A" & i & ":J" & i)
                xlRange2 = xlsheet.Range("A" & i + 1 & ":J" & i + 1)
                xlRange1.Copy(xlRange2)
                NAR(xlRange1)
                NAR(xlRange2)
                If intR <= myds4.Tables("acm010").Rows.Count - 1 Then  '收入資料
                    strAccno = nz(myds4.Tables("acm010").Rows(intR).Item("accno"), "")
                    strAccname = nz(myds4.Tables("acm010").Rows(intR).Item("accname"), "")
                    intAmt1 = nz(myds4.Tables("acm010").Rows(intR).Item("amt1"), 0)  '決算數
                    intAmt2 = nz(myds4.Tables("acm010").Rows(intR).Item("amt3"), 0)  '預算數
                    '計算縮排所需空格數
                    Select Case Len(strAccno)
                        Case 2
                            spaces = 0
                        Case 3
                            spaces = 4
                        Case 5
                            spaces = 8
                        Case Else
                            spaces = 0
                    End Select
                    xlRange = xlCells(i, 2)
                    xlRange.Value = Space(spaces) & FormatAccno(strAccno) & vbCrLf & Space(spaces) & strAccname
                    NAR(xlRange)
                    xlRange = xlCells(i, 3)
                    xlRange.Value = FormatNumber(intAmt1, 2)
                    NAR(xlRange)
                    xlRange = xlCells(i, 4)
                    xlRange.Value = FormatNumber(intAmt2, 2)
                    NAR(xlRange)
                    NAR(xlRange)
                    xlRange = xlCells(i, 5)
                    xlRange.Value = FormatNumber(intAmt1 - intAmt2, 2)
                    NAR(xlRange)
                    If Len(strAccno) = 5 Then
                        Tot41 += intAmt1
                        Tot42 += intAmt2
                    End If
                End If
                If intR <= myds5.Tables("acm010").Rows.Count - 1 Then  '收入資料
                    strAccno = nz(myds5.Tables("acm010").Rows(intR).Item("accno"), "")
                    strAccname = nz(myds5.Tables("acm010").Rows(intR).Item("accname"), "")
                    intAmt1 = nz(myds5.Tables("acm010").Rows(intR).Item("amt1"), 0)  '決算數
                    intAmt2 = nz(myds5.Tables("acm010").Rows(intR).Item("amt3"), 0)  '預算數
                    '計算縮排所需空格數
                    Select Case Len(strAccno)
                        Case 2
                            spaces = 0
                        Case 3
                            spaces = 4
                        Case 5
                            spaces = 8
                        Case Else
                            spaces = 0
                    End Select
                    xlRange = xlCells(i, 7)
                    xlRange.Value = Space(spaces) & FormatAccno(strAccno) & vbCrLf & Space(spaces) & strAccname
                    NAR(xlRange)
                    xlRange = xlCells(i, 8)
                    xlRange.Value = FormatNumber(intAmt1, 2)
                    NAR(xlRange)
                    xlRange = xlCells(i, 9)
                    xlRange.Value = FormatNumber(intAmt2, 2)
                    NAR(xlRange)
                    xlRange = xlCells(i, 10)
                    xlRange.Value = FormatNumber(intAmt1 - intAmt2, 2)
                    NAR(xlRange)
                    If Len(strAccno) = 5 Then
                        TOT51 += intAmt1
                        TOT52 += intAmt2
                    End If
                End If
            Next

            i += 1
            '拷貝目前這列到下一列,使得每列都有相同的格式設定
            xlRange1 = xlsheet.Range("A" & i & ":J" & i)
            xlRange2 = xlsheet.Range("A" & i + 1 & ":J" & i + 1)
            xlRange1.Copy(xlRange2)
            NAR(xlRange1)
            NAR(xlRange2)

            xlRange = xlCells(i, 2)
            xlRange.Value = vbCrLf & "小  計"
            NAR(xlRange)
            xlRange = xlCells(i, 3)
            xlRange.Value = FormatNumber(Tot41, 2)
            NAR(xlRange)
            xlRange = xlCells(i, 4)
            xlRange.Value = FormatNumber(Tot42, 2)
            NAR(xlRange)
            xlRange = xlCells(i, 5)
            xlRange.Value = FormatNumber(Tot41 - Tot42, 2)
            NAR(xlRange)

            xlRange = xlCells(i, 7)
            xlRange.Value = vbCrLf & "小  計"
            NAR(xlRange)
            xlRange = xlCells(i, 8)
            xlRange.Value = FormatNumber(TOT51, 2)
            NAR(xlRange)
            xlRange = xlCells(i, 9)
            xlRange.Value = FormatNumber(TOT52, 2)
            NAR(xlRange)
            xlRange = xlCells(i, 10)
            xlRange.Value = FormatNumber(TOT51 - TOT52, 2)
            NAR(xlRange)

            xlRange = xlCells(6, 1)
            xlRange.Value = "事業及事業外收入（經常收入）"
            NAR(xlRange)
            xlRange = xlCells(6, 6)
            xlRange.Value = "事業及事業外支出（經常支出）"
            NAR(xlRange)
            'xlRange = xlCells("A6:A10")  '合併儲存格
            'xlRange.Select()
            'xlRange.MergeCells = True
            'NAR(xlRange)
            Sum41 = Tot41 : Sum42 = Tot42 : Sum51 = TOT51 : Sum52 = TOT52

            LastR = i '記錄最後excel之列數
            '先劃線
            Dim j As Integer
            For j = 1 To 11
                i += 1
                '拷貝目前這列到下一列,使得每列都有相同的格式設定
                xlRange1 = xlsheet.Range("A" & i & ":J" & i)
                xlRange2 = xlsheet.Range("A" & i + 1 & ":J" & i + 1)
                xlRange1.Copy(xlRange2)
                NAR(xlRange1)
                NAR(xlRange2)
            Next
            Tot41 = 0 : Tot42 = 0 : TOT51 = 0 : TOT52 = 0
            '資本支出
            xlRange = xlCells(LastR + 1, 1)
            xlRange.Value = "資本收入"
            NAR(xlRange)
            xlRange = xlCells(LastR + 1, 6)
            xlRange.Value = "資本支出"
            NAR(xlRange)
            sqlstr = "SELECT c.ACCNO, c.ACCNAME," & _
                     " a.DEAMT12, a.CRAMT12 , a.beg_debit , a.beg_credit, b.deamt from " & _
            " (select * from accname where len(accno)=5 and substring(accno,1,2)='13' and substring(accno,4,2)<>'02') c " & _
            "left outer join " & _
            " (select * from ACF050 where accyear=" & SYear & ") a on c.accno=a.accno " & _
            "left outer join " & _
            "(select *  from accbg where accyear=" & SYear & ") b on c.accno=b.accno "
            myds4 = openmember("", "acm010", sqlstr)
            i = LastR + 2

            For j = 0 To myds4.Tables("acm010").Rows.Count - 1
                With myds4.Tables(0).Rows(j)
                    intAmt1 = nz(.Item("deamt12"), 0) - nz(.Item("beg_debit"), 0)
                    intAmt2 = nz(.Item("deamt"), 0)
                    If intAmt1 <> 0 Or intAmt2 <> 0 Then
                        xlRange = xlCells(i, 7)
                        xlRange.Value = Space(8) & FormatAccno(nz(.Item("Accno"), "")) & vbCrLf & Space(8) & nz(.Item("Accname"), "")
                        NAR(xlRange)
                        xlRange = xlCells(i, 8)
                        xlRange.Value = FormatNumber(intAmt1, 2)
                        NAR(xlRange)
                        xlRange = xlCells(i, 9)
                        xlRange.Value = FormatNumber(intAmt2, 2)
                        NAR(xlRange)
                        xlRange = xlCells(i, 10)
                        xlRange.Value = FormatNumber(intAmt1 - intAmt2, 2)
                        NAR(xlRange)
                        TOT51 += intAmt1
                        TOT52 += intAmt2
                        i += 1
                    End If
                End With
            Next
            xlRange = xlCells(LastR + 1, 7)
            xlRange.Value = "1.固定資產之改良擴充"
            NAR(xlRange)
            xlRange = xlCells(i, 8)
            xlRange.Value = FormatNumber(TOT51, 2)
            NAR(xlRange)
            xlRange = xlCells(i, 9)
            xlRange.Value = FormatNumber(TOT52, 2)
            NAR(xlRange)
            xlRange = xlCells(i, 10)
            xlRange.Value = FormatNumber(TOT51 - TOT52, 2)
            NAR(xlRange)
            '長期借款
            sqlstr = "SELECT c.ACCNO, c.ACCNAME," & _
                     " a.DEAMT12, a.CRAMT12 , a.beg_debit , a.beg_credit, b.deamt, b.cramt from " & _
            " (select * from accname where accno='22101') c " & _
            "left outer join " & _
            " (select * from ACF050 where accyear=" & SYear & ") a on c.accno=a.accno " & _
            "left outer join " & _
            "(select *  from accbg where accyear=" & SYear & ") b on c.accno=b.accno "
            myds4 = openmember("", "acm010", sqlstr)
            If myds4.Tables("acm010").Rows.Count > 0 Then
                With myds4.Tables(0).Rows(0)
                    '長期借款之償還
                    intAmt1 = nz(.Item("deamt12"), 0) - nz(.Item("beg_debit"), 0)
                    intAmt2 = nz(.Item("deamt"), 0)
                    If intAmt1 <> 0 Or intAmt2 <> 0 Then
                        xlRange = xlCells(i, 7)
                        xlRange.Value = Space(8) & FormatAccno(nz(.Item("Accno"), "")) & vbCrLf & Space(8) & nz(.Item("Accname"), "")
                        NAR(xlRange)
                        xlRange = xlCells(i, 8)
                        xlRange.Value = FormatNumber(intAmt1, 2)
                        NAR(xlRange)
                        xlRange = xlCells(i, 9)
                        xlRange.Value = FormatNumber(intAmt2, 2)
                        NAR(xlRange)
                        xlRange = xlCells(i, 10)
                        xlRange.Value = FormatNumber(intAmt1 - intAmt2, 2)
                        NAR(xlRange)
                        TOT51 += intAmt1
                        TOT52 += intAmt2
                        i += 1
                    End If
                    '長期借款之舉借
                    intAmt1 = nz(.Item("cramt12"), 0) - nz(.Item("beg_credit"), 0)
                    intAmt2 = nz(.Item("cramt"), 0)
                    If intAmt1 <> 0 Or intAmt2 <> 0 Then
                        xlRange = xlCells(LastR + 3, 2)
                        xlRange.Value = "2.長期借款之增加"
                        NAR(xlRange)
                        xlRange = xlCells(LastR + 3, 8)
                        xlRange.Value = FormatNumber(intAmt1, 2)
                        NAR(xlRange)
                        xlRange = xlCells(LastR + 3, 9)
                        xlRange.Value = FormatNumber(intAmt2, 2)
                        NAR(xlRange)
                        xlRange = xlCells(LastR + 3, 10)
                        xlRange.Value = FormatNumber(intAmt1 - intAmt2, 2)
                        NAR(xlRange)
                        xlRange = xlCells(LastR + 4, 8)
                        xlRange.Value = Space(8) & FormatAccno(nz(.Item("Accno"), "")) & vbCrLf & Space(8) & nz(.Item("Accname"), "")
                        NAR(xlRange)
                        xlRange = xlCells(LastR + 4, 8)
                        xlRange.Value = FormatNumber(intAmt1, 2)
                        NAR(xlRange)
                        xlRange = xlCells(LastR + 4, 9)
                        xlRange.Value = FormatNumber(intAmt2, 2)
                        NAR(xlRange)
                        xlRange = xlCells(LastR + 4, 10)
                        xlRange.Value = FormatNumber(intAmt1 - intAmt2, 2)
                        NAR(xlRange)
                        Tot41 += intAmt1
                        Tot42 += intAmt2
                    End If
                End With
            End If
            '年度之結餘
            xlRange = xlCells(i, 7)
            xlRange.Value = "3.年度之結餘" & vbCrLf & Space(8) & "資金之淨增"
            NAR(xlRange)
            xlRange = xlCells(i, 8)
            xlRange.Value = "自行調整"
            NAR(xlRange)
            i += 1
            xlRange = xlCells(i, 7)
            xlRange.Value = Space(8) & vbCrLf & Space(8) & "小  計"
            NAR(xlRange)
            xlRange = xlCells(i, 8)
            xlRange.Value = FormatNumber(TOT51, 2)
            NAR(xlRange)
            xlRange = xlCells(i, 9)
            xlRange.Value = FormatNumber(TOT52, 2)
            NAR(xlRange)
            xlRange = xlCells(i, 10)
            xlRange.Value = FormatNumber(TOT51 - TOT52, 2)
            NAR(xlRange)
            Sum51 += TOT51
            Sum52 += TOT52
            i += 1
            xlRange = xlCells(i, 7)
            xlRange.Value = Space(8) & vbCrLf & Space(8) & "合  計"
            NAR(xlRange)
            xlRange = xlCells(i, 8)
            xlRange.Value = FormatNumber(Sum51, 2)
            NAR(xlRange)
            xlRange = xlCells(i, 9)
            xlRange.Value = FormatNumber(Sum52, 2)
            NAR(xlRange)
            xlRange = xlCells(i, 10)
            xlRange.Value = FormatNumber(Sum51 - Sum52, 2)
            NAR(xlRange)
            Dim SumRecNo As Integer = i  '記錄合計之所在列數


            '資本支出

            '受贈公積
            sqlstr = "SELECT c.ACCNO, c.ACCNAME," & _
                     " a.DEAMT12, a.CRAMT12 , a.beg_debit , a.beg_credit, b.deamt, b.cramt from " & _
            " (select * from accname where accno='31102') c " & _
            "left outer join " & _
            " (select * from ACF050 where accyear=" & SYear & ") a on c.accno=a.accno " & _
            "left outer join " & _
            "(select *  from accbg where accyear=" & SYear & ") b on c.accno=b.accno "
            myds4 = openmember("", "acm010", sqlstr)
            intAmt1 = 0 : intAmt2 = 0
            For j = 0 To myds4.Tables("acm010").Rows.Count - 1
                With myds4.Tables(0).Rows(0)
                    intAmt1 = nz(.Item("cramt12"), 0) - nz(.Item("beg_credit"), 0) - (nz(.Item("deamt12"), 0) - nz(.Item("beg_debit"), 0))
                    intAmt2 = nz(.Item("cramt"), 0)
                    If intAmt1 <> 0 Or intAmt2 <> 0 Then
                        xlRange = xlCells(LastR + 1, 2)
                        xlRange.Value = "1.受贈公積之增加"
                        NAR(xlRange)
                        xlRange = xlCells(LastR + 1, 3)
                        xlRange.Value = FormatNumber(intAmt1, 2)
                        NAR(xlRange)
                        xlRange = xlCells(LastR + 1, 4)
                        xlRange.Value = FormatNumber(intAmt2, 2)
                        NAR(xlRange)
                        xlRange = xlCells(LastR + 1, 5)
                        xlRange.Value = FormatNumber(intAmt1 - intAmt2, 2)
                        NAR(xlRange)
                        xlRange = xlCells(LastR + 2, 2)
                        xlRange.Value = Space(8) & FormatAccno(nz(.Item("Accno"), "")) & vbCrLf & Space(8) & nz(.Item("Accname"), "")
                        NAR(xlRange)
                        xlRange = xlCells(LastR + 2, 3)
                        xlRange.Value = FormatNumber(intAmt1, 2)
                        NAR(xlRange)
                        xlRange = xlCells(LastR + 2, 4)
                        xlRange.Value = FormatNumber(intAmt2, 2)
                        NAR(xlRange)
                        xlRange = xlCells(LastR + 2, 5)
                        xlRange.Value = FormatNumber(intAmt1 - intAmt2, 2)
                        NAR(xlRange)
                    End If
                End With
            Next
            If Tot41 <> 0 Or Tot42 <> 0 Then  '有長期借款之增加
                i = LastR + 4
            Else
                i = LastR + 2
            End If
            Tot41 += intAmt1  '受贈公積
            Tot42 += intAmt2
            '3.年度短絀撥補財源
            sqlstr = "SELECT c.ACCNO, c.ACCNAME," & _
                     " a.DEAMT12, a.CRAMT12 , a.beg_debit , a.beg_credit, b.deamt from " & _
            " (select * from accname where accno='31101' or accno='32101') c " & _
            "left outer join " & _
            " (select * from ACF050 where accyear=" & SYear & ") a on c.accno=a.accno " & _
            "left outer join " & _
            "(select *  from accbg where accyear=" & SYear & ") b on c.accno=b.accno "
            myds4 = openmember("", "acm010", sqlstr)
            xlRange = xlCells(i, 2)
            xlRange.Value = "3.年度之短絀" & vbCrLf & "    撥補財源"
            NAR(xlRange)
            xlRange = xlCells(i, 3)
            xlRange.Value = "自行調整"
            NAR(xlRange)
            i += 1
            For j = 0 To myds4.Tables("acm010").Rows.Count - 1
                With myds4.Tables(0).Rows(j)
                    intAmt1 = nz(.Item("deamt12"), 0) - nz(.Item("beg_debit"), 0)
                    intAmt2 = nz(.Item("deamt"), 0)
                    If intAmt1 <> 0 Or intAmt2 <> 0 Then
                        xlRange = xlCells(i, 2)
                        xlRange.Value = Space(8) & FormatAccno(nz(.Item("Accno"), "")) & vbCrLf & Space(8) & nz(.Item("Accname"), "")
                        NAR(xlRange)
                        xlRange = xlCells(i, 3)
                        xlRange.Value = FormatNumber(intAmt1, 2)
                        NAR(xlRange)
                        xlRange = xlCells(i, 4)
                        xlRange.Value = FormatNumber(intAmt2, 2)
                        NAR(xlRange)
                        xlRange = xlCells(i, 5)
                        xlRange.Value = FormatNumber(intAmt1 - intAmt2, 2)
                        NAR(xlRange)
                        Tot41 += intAmt1
                        Tot42 += intAmt2
                        i += 1
                    End If
                End With
            Next
            '4.折舊
            sqlstr = "SELECT c.ACCNO, c.ACCNAME," & _
                     " a.DEAMT12, a.CRAMT12 , a.beg_debit , a.beg_credit, b.deamt, B.BG1+BG2+BG3+BG4+BG5 AS bgamt from " & _
            " (select * from accname where accno='51101' or accno='5120103' or accno='5220103' or accno='42202') c " & _
            "left outer join " & _
            " (select * from ACF050 where accyear=" & SYear & ") a on c.accno=a.accno " & _
            "left outer join " & _
            "(select *  from accbg where accyear=" & SYear & ") b on c.accno=b.accno "
            myds4 = openmember("", "acm010", sqlstr)
            intAmt1 = 0 : intAmt2 = 0
            For j = 0 To myds4.Tables("acm010").Rows.Count - 1
                With myds4.Tables(0).Rows(j)
                    strAccno = nz(.Item("accno"), "")
                    If Mid(strAccno, 1, 1) = "5" Then
                        intAmt1 += nz(.Item("deamt12"), 0) - nz(.Item("cramt12"), 0)
                        intAmt2 += nz(.Item("bgamt"), 0)
                    Else
                        intAmt1 -= nz(.Item("cramt12"), 0) - nz(.Item("deamt12"), 0)  '42201認列折舊收入
                        intAmt2 -= nz(.Item("bgamt"), 0)
                    End If
                End With
            Next

            xlRange = xlCells(i, 2)
            xlRange.Value = "4.抵銷提列之折舊" & vbCrLf & "    累計折舊"
            NAR(xlRange)
            xlRange = xlCells(i, 3)
            xlRange.Value = FormatNumber(intAmt1, 2)
            NAR(xlRange)
            xlRange = xlCells(i, 4)
            xlRange.Value = FormatNumber(intAmt2, 2)
            NAR(xlRange)
            xlRange = xlCells(i, 5)
            xlRange.Value = FormatNumber(intAmt1 - intAmt2, 2)
            NAR(xlRange)
            i += 1
            If i < SumRecNo - 1 Then
                i = SumRecNo - 1
            End If
            xlRange = xlCells(i, 2)
            xlRange.Value = Space(8) & vbCrLf & Space(8) & "小  計"
            NAR(xlRange)
            i += 1
            xlRange = xlCells(i, 2)
            xlRange.Value = Space(8) & vbCrLf & Space(8) & "合  計"
            NAR(xlRange)
            xlRange = xlCells(i, 3)
            xlRange.Value = FormatNumber(Sum51, 2)
            NAR(xlRange)
            xlRange = xlCells(i, 4)
            xlRange.Value = FormatNumber(Sum52, 2)
            NAR(xlRange)
            xlRange = xlCells(i, 5)
            xlRange.Value = FormatNumber(Sum51 - Sum52, 2)
            NAR(xlRange)



            '儲存檔案
            xlbook.Save()
            If rdoPrint.Checked Then
                'xlapp.Visible = True
                'xlbook.PrintPreview() '不可用預覽列印,否則當使用者關閉excel視窗之後就會導致finally區段關閉excel.exe時出現error
                xlbook.PrintOut()  '直接列印
            End If
            Dim result3 As DialogResult = MessageBox.Show("列印完畢,已存入 " & tt2 & vbNewLine & "是否要幫您開啟此份報表?", "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If result3 = DialogResult.Yes Then
                'If MessageBox.Show("列印完畢,已存入 " & tt2 & vbNewLine & "是否要幫您開啟此份報表?", "", MsgBoxStyle.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) = MsgBoxResult.Yes Then
                '不可用 Process.Start(tt2) 否則會造成用同一個excel process開啟報表,導致finally區段關閉excel.exe時會出現error
                Process.Start("excel.exe", tt2)
            End If

            Me.Close()

        Finally
            '釋放各物件所佔用的記憶體,要按照以下順序
            ' NAR(xlRange)
            NAR(xlCells)
            NAR(xlRange)
            NAR(xlRange1)
            NAR(xlRange2)
            NAR(xlRange3)
            '如果有宣告 xlRange3 在這也要記得釋放記憶體
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


    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class
