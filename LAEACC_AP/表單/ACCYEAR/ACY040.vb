Imports JBC.Printing
Imports System.IO
Imports Microsoft.Office.Interop
Public Class ACY040
    Dim confid, confunit As String, LoadAfter, Dirty As Boolean
    Dim SYear As Integer
    Dim myds As DataSet
    Dim xlCells As Excel.Range
    Dim sqlstr, retstr As String
    Dim intAmt As Decimal = 0

    Private Sub ACY040_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dtpDateS.Value = TransPara.TransP("LastDay")
    End Sub

    Private Sub LoadGridFunc()
        '先清空acm010 
        sqlstr = "delete from acm010"
        retstr = runsql(mastconn, sqlstr)
        Dim intAmt1, intAmt2 As Decimal


        '將本年度acf050四級科目 & 上年度acf050四級科目丟入acm010  
        sqlstr = "INSERT INTO ACM010  SELECT c.ACCNO, c.ACCNAME," & _
                " a.DEAMT12 AS amt1, a.CRAMT12 AS amt2, a.beg_debit as amt3, a.beg_credit as amt4, 0 as amt5, 0 as amt6, 0 as amt7 from " & _
         " (select * from accname where len(accno)=5 and substring(accno,1,1)<='3') c left outer join " & _
         " (select * from ACF050 where accyear=" & SYear & " and len(accno)=5 and substring(accno,1,1)<='3') a on c.accno=a.accno " & _
         "left outer join " & _
         "(select accno, deamt12, cramt12 from acf050 where accyear=" & SYear - 1 & " and len(accno)=5 and substring(accno,1,1)<='3') b " & _
        "on c.accno=b.accno "
        '" a.DEAMT12 AS amt1, a.CRAMT12 AS amt2, b.deamt12 as amt3, b.cramt12 as amt4, 0 as amt5, 0 as amt6, 0 as amt7 from " & _
        retstr = runsql(mastconn, sqlstr)

        '將null清為0
        sqlstr = " update acm010 set amt1=0 where amt1 is null"
        retstr = runsql(mastconn, sqlstr)
        sqlstr = " update acm010 set amt2=0 where amt2 is null"
        retstr = runsql(mastconn, sqlstr)
        sqlstr = " update acm010 set amt3=0 where amt3 is null"
        retstr = runsql(mastconn, sqlstr)
        sqlstr = " update acm010 set amt4=0 where amt4 is null"
        retstr = runsql(mastconn, sqlstr)

        '計算上年度本期餘絀intamt
        sqlstr = "select amt3, amt4 from acm010 where accno='32301'"
        myds = openmember("", "acm010", sqlstr)
        intAmt1 = nz(myds.Tables("acm010").Rows(0).Item("amt3"), 0)   '上年度本期餘絀
        intAmt2 = nz(myds.Tables("acm010").Rows(0).Item("amt4"), 0)   '上年度本期餘絀
        '將上年度餘絀併入公積之期初數
        sqlstr = "update acm010 set amt3 = amt3 +" & intAmt1 & ", amt4 = amt4 + " & intAmt2 & " where accno='31201'"
        'MsgBox(sqlstr)
        retstr = runsql(mastconn, sqlstr)


        '計算本期餘絀intamt
        sqlstr = "select sum(cramt12)-sum(deamt12) as amt from acf050 " & _
               "where  ACCYEAR = " & SYear & " AND LEN(ACCNO) = 5 and substring(accno,1,1)>='4'"
        myds = openmember("", "acm010", sqlstr)
        intAmt = nz(myds.Tables("acm010").Rows(0).Item(0), 0)   '本期餘絀

        ''將本年度餘絀四級科目丟入acm010  , 先檢查本期餘絀該科目是否已存在
        'sqlstr = "select * from acm010 where accno='32301'"
        'myds =  openmember("", "acm010", sqlstr)
        'If myds.Tables("acm010").Rows.Count > 0 Then   '已存在   
        '    If intAmt > 0 Then  '>0為貸方
        '        sqlstr = "update acm010 set amt2=amt2+" & intAmt & " where accno='32301'"
        '    Else
        '        sqlstr = "update acm010 set amt1=amt1+" & -intAmt & " where accno='32301'"
        '    End If
        'Else   '不存在
        '    If intAmt <> 0 Then
        '        '將本年度餘絀四級科目丟入acm010
        '        If intAmt > 0 Then   '淨利 amt put to credit(amt2)
        '            sqlstr = "INSERT INTO ACM010  (ACCNO, ACCNAME, amt1, amt2, amt3, amt4, amt5, amt6, amt7) values " & _
        '                                          "'32301', '本期餘絀', 0," & intAmt & ", 0,0,0,0,0,0 "
        '        Else      '虧損amt put to debit(amt1)
        '            sqlstr = "INSERT INTO ACM010  (ACCNO, ACCNAME, amt1, amt2, amt3, amt4, amt5, amt6, amt7) values " & _
        '                                          "'32301', '本期餘絀'," & -intAmt & ", 0,0,0,0,0,0,0 "
        '        End If
        '    End If
        'End If
        'retstr =  runsql(mastconn, sqlstr)

        '上年度本期餘絀已由當年acf050->32301之beg_deamt, beg_cramt算出,置acm010.amt4,amt5 

        '刪全為0
        sqlstr = " delete FROM acm010 where amt1+amt2+AMT3+AMT4=0 ) "
        'sqlstr = " delete FROM acm010 where amt1+amt2+AMT3+AMT4=0 or (left(accno,2)='13' and substring(accno,4,2)='02') "
        retstr = runsql(mastconn, sqlstr)

        '各欄數值計算(本年度amt5,上年度餘額amt6)
        sqlstr = " Update ACM010 SET amt5 = amt1 - amt2, amt6 = amt3 - amt4 where substring(accno,1,1)='1'"
        retstr = runsql(mastconn, sqlstr)
        sqlstr = " Update ACM010 SET amt5 = amt2 - amt1, amt6 = amt4 - amt3 where substring(accno,1,1)>'1'"
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then MsgBox("update acm010 error " & sqlstr)

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
                 "where len(accno)=3 and accno<>'111' " & _
                 "GROUP BY substring(accno, 1, 2)) a left outer join ACCNAME b ON a.accno = b.ACCNO"
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then MsgBox("統計二級 error " & sqlstr)

        ''統計一級
        'sqlstr = "INSERT INTO ACM010 select a.accno, b.accname, a.amt1, a.amt2, a.amt3, " & _
        '         "a.amt4, a.amt5, a.amt6, a.amt7 from " & _
        '         "(SELECT substring(accno, 1, 1) AS accno, SUM(amt1) AS amt1, " & _
        '         "SUM(amt2) AS amt2, SUM(amt3) AS amt3, SUM(amt4) AS amt4, " & _
        '         "SUM(amt5) AS amt5, SUM(amt6) AS amt6, 0 AS amt7 from acm010 " & _
        '         "where len(accno)=5 " & _
        '         "GROUP BY substring(accno, 1, 1)) a left outer join ACCNAME b ON a.accno = b.ACCNO"
        'retstr =  runsql(mastconn, sqlstr)
        'If retstr <> "sqlok" Then MsgBox("統計一級 error " & sqlstr)
        ''sqlstr = "select accno amt5,  amt6 from acm010 where where len(accno)=1"

        '丟入dataset 
        'sqlstr = "SELECT * FROM acm010 order by accno"
        'myds =  openmember("", "acm010", sqlstr)
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
        Dim strAccno As String
        Dim strS, strT As String
        Dim spaces As Integer
        Dim TOT1, TOT2, TOT3, TOTup2 As Decimal
        TOT1 = 0 : TOT2 = 0 : TOT3 = 0 : TOTup2 = 0
        Dim decAmt5, decAmt6, decAmt7 As Decimal
        Dim TempAccno22, TempAccno24, TempAccno32, TempAccno34, TempAccno323 As Decimal
        Dim intAmt1, intAmt2 As Decimal
        Try
            SYear = GetYear(dtpDateS.Value)
            TransPara.TransP("LastDay") = dtpDateS.Value
            Call LoadGridFunc()
            Dim tt1, tt2 As String
            Try
                tt1 = "c:\App\acc\報表樣本\ACy040.xls"
                tt2 = "c:\App\acc\報表\ACY040.xls"
                If Not File.Exists(tt1) Then
                    MsgBox("找不到報表樣本，請洽資訊人員" & vbNewLine & tt1)
                    Exit Sub
                End If
                FileCopy(tt1, tt2)    'copy tt1 to tt2
                xlapp = CreateObject("excel.application")
                xlbooks = xlapp.Workbooks
                xlbook = xlbooks.Open(tt2) '開啟tt2
            Catch ex As Exception
                MsgBox("報表copy " & tt1 & "  to  " & tt2 & "錯誤，是否\報表\ACY040.XLS使用中,否則請洽程式設計人員!")
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

            '放資料
            xlRange = xlCells(7, 2)   '本期餘絀
            xlRange.Value = FormatNumber(intAmt, 2)
            NAR(xlRange)
            '4.折舊
            sqlstr = "SELECT c.ACCNO, c.ACCNAME," & _
                     " a.DEAMT12, a.CRAMT12 , a.beg_debit , a.beg_credit, b.deamt, B.BG1+BG2+BG3+BG4+BG5 AS bgamt from " & _
            " (select * from accname where accno='51101' or accno='5120103' or accno='5220103') c " & _
            "left outer join " & _
            " (select * from ACF050 where accyear=" & SYear & ") a on c.accno=a.accno " & _
            "left outer join " & _
            "(select *  from accbg where accyear=" & SYear & ") b on c.accno=b.accno "
            myds = openmember("", "acm010", sqlstr)
            intAmt1 = 0 : intAmt2 = 0
            For intR = 0 To myds.Tables("acm010").Rows.Count - 1
                With myds.Tables(0).Rows(intR)
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
            Dim intDep As Decimal = intAmt1  '折舊折列數
            xlRange = xlCells(9, 2)   '折舊
            xlRange.Value = FormatNumber(intAmt1, 2)
            NAR(xlRange)
            xlRange = xlCells(9, 3)   '預算數
            xlRange.Value = FormatNumber(intAmt2, 2)
            NAR(xlRange)
            '約當現金
            sqlstr = "SELECT * FROM acm010 where accno='111'"
            myds = openmember("", "acm010", sqlstr)
            If myds.Tables(0).Rows.Count > 0 Then
                xlRange = xlCells(26, 2)  '上年度現金
                xlRange.Value = FormatNumber(nz(myds.Tables(0).Rows(0).Item("amt6"), 0), 2)
                NAR(xlRange)
                xlRange = xlCells(27, 2)  '本年度現金
                xlRange.Value = FormatNumber(nz(myds.Tables(0).Rows(0).Item("amt5"), 0), 2)
                NAR(xlRange)
            End If
            ''累積餘絀
            'sqlstr = "SELECT * FROM acm010 where accno='321'"
            'myds =  openmember("", "acm010", sqlstr)
            'If myds.Tables(0).Rows.Count > 0 Then
            '    xlRange = xlCells(26, 2)  '上年度現金
            '    xlRange.Value = FormatNumber(nz(myds.Tables(0).Rows(0).Item("amt6"), 0), 2)
            '    NAR(xlRange)
            '    xlRange = xlCells(27, 2)  '本年度現金
            '    xlRange.Value = FormatNumber(nz(myds.Tables(0).Rows(0).Item("amt5"), 0), 2)
            '    NAR(xlRange)
            'End If

            Dim cycle, intRow As Integer
            For cycle = 1 To 3
                If cycle = 1 Then
                    sqlstr = "SELECT * FROM acm010 where accno='11' or accno='14' or accno='15' or accno='21' or accno='23' order by accno"
                End If
                If cycle = 2 Then
                    sqlstr = "SELECT * FROM acm010 where accno='12' or accno='13' order by accno"
                End If
                If cycle = 3 Then
                    sqlstr = "SELECT * FROM acm010 where accno='22' or accno='24' or accno='31' or accno='321' order by accno"
                End If
                myds = openmember("", "acm010", sqlstr)
                intAmt1 = 0
                With myds.Tables(0)
                    For intR = 0 To .Rows.Count - 1
                        strAccno = nz(.Rows(intR).Item("accno"), "")
                        If Mid(strAccno, 1, 1) = "1" Then
                            intAmt1 += nz(.Rows(intR).Item("amt6"), 0) - nz(.Rows(intR).Item("amt5"), 0)
                        Else
                            intAmt1 += nz(.Rows(intR).Item("amt5"), 0) - nz(.Rows(intR).Item("amt6"), 0)
                        End If
                        If cycle = 1 Then
                            'TOT1 += intAmt1
                            If strAccno = "11" Then intRow = 10
                            If strAccno = "15" Then intRow = 11
                            If strAccno = "21" Then intRow = 12
                            If strAccno = "23" Then intRow = 13
                        End If
                        If cycle = 2 Then
                            'TOT1 += intAmt1
                            If strAccno = "12" Then intRow = 16
                            If strAccno = "13" Then
                                intRow = 17
                                intAmt1 -= intDep  '固定資產增減數要加上折舊折列數
                            End If
                        End If
                        If cycle = 3 Then
                            'TOT1 += intAmt1
                            If strAccno = "22" Then intRow = 20
                            If strAccno = "24" Then intRow = 21
                            If strAccno = "31" Then intRow = 22
                            If strAccno = "321" Then intRow = 23

                        End If
                        If strAccno <> "14" Then   '14要併入15顯示
                            xlRange = xlCells(intRow, 2)
                            xlRange.Value = FormatNumber(intAmt1, 2)
                            NAR(xlRange)
                            intAmt1 = 0
                        End If
                    Next
                End With
            Next

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
