﻿Imports JBC.Printing
Imports System.IO
Imports System.Math
Imports Microsoft.VisualBasic
Imports Microsoft.Office.Interop
Public Class ACY010
    Dim confid, confunit As String, LoadAfter, Dirty As Boolean
    Dim SYear As Integer
    Dim myds As DataSet
    Dim xlCells As Excel.Range

    Private Sub ACY010_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dtpDateS.Value = TransPara.TransP("LastDay")
    End Sub
    Private Sub LoadGridFunc()
        Dim sqlstr, retstr As String
        Dim intAmt As Decimal = 0
        '先清空acm010 
        sqlstr = "delete from acm010"
        retstr = runsql(mastconn, sqlstr)

        '將本年度acf050四級科目 & 上年度acf050四級科目丟入acm010  
        sqlstr = "INSERT INTO ACM010  SELECT c.ACCNO, c.ACCNAME," & _
                " a.DEAMT12 AS amt1, a.CRAMT12 AS amt2, a.beg_debit as amt3, a.beg_credit as amt4, 0 as amt5, 0 as amt6, 0 as amt7 from " & _
                " (select * from ACF050 where accyear=" & SYear & " and len(accno)=5 and substring(accno,1,1)<='3') a left outer join " & _
                " accname c on a.accno=c.accno "
        '" (select * from accname where len(accno)=5 and substring(accno,1,1)<='3') c on c.accno=a.accno  " & _
        '"left outer join " & _
        '"(select accno, deamt12, cramt12 from acf050 where accyear=" & SYear - 1 & " and len(accno)=5 and substring(accno,1,1)<='3') b " & _
        '"on c.accno=b.accno "

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

        '計算本期餘絀intamt
        sqlstr = "select sum(cramt12)-sum(deamt12) as amt from acf050 " & _
               "where  ACCYEAR = " & SYear & " AND LEN(ACCNO) = 5 and substring(accno,1,1)>='4'"
        myds = openmember("", "acm010", sqlstr)
        intAmt = nz(myds.Tables("acm010").Rows(0).Item(0), 0)
        '將本年度餘絀四級科目丟入acm010  , 先檢查本期餘絀該科目是否已存在
        sqlstr = "select * from acm010 where accno='32301'"
        myds = openmember("", "acm010", sqlstr)
        If myds.Tables("acm010").Rows.Count > 0 Then   '已存在   
            If intAmt > 0 Then  '>0為貸方
                sqlstr = "update acm010 set amt2=amt2+" & intAmt & " where accno='32301'"
            Else
                sqlstr = "update acm010 set amt1=amt1+" & -intAmt & " where accno='32301'"
            End If
        Else   '不存在
            If intAmt <> 0 Then
                '將本年度餘絀四級科目丟入acm010
                If intAmt > 0 Then   '淨利 amt put to credit(amt2)
                    sqlstr = "INSERT INTO ACM010  (ACCNO, ACCNAME, amt1, amt2, amt3, amt4, amt5, amt6, amt7) values " & _
                                                  "'32301', '本期餘絀', 0," & intAmt & ", 0,0,0,0,0,0 "
                Else      '虧損amt put to debit(amt1)
                    sqlstr = "INSERT INTO ACM010  (ACCNO, ACCNAME, amt1, amt2, amt3, amt4, amt5, amt6, amt7) values " & _
                                                  "'32301', '本期餘絀'," & -intAmt & ", 0,0,0,0,0,0,0 "
                End If
            End If
        End If
        retstr = runsql(mastconn, sqlstr)

        '上年度本期餘絀已由當年acf050->32301之beg_deamt, beg_cramt算出,置acm010.amt4,amt5
        '將上年度本期餘絀32301轉至餘絀撥補
        sqlstr = "select * from acf010 where accyear=" & SYear & " and kind>='3' and accno='32301'"
        myds = openmember("", "acm010", sqlstr)
        If myds.Tables("acm010").Rows.Count > 0 Then
            Dim strKind, strAccno As String
            Dim intNo2 As Integer = 0
            Dim intAmt3, intAmt4 As Decimal
            strKind = IIf(myds.Tables("acm010").Rows(0).Item("kind") = "3", "4", "3")
            intNo2 = nz(myds.Tables("acm010").Rows(0).Item("no_2_no"), 0)   '轉帳編號
            If intNo2 > 0 Then
                sqlstr = "select * from acf010 where accyear=" & SYear & _
                         " and kind='" & strKind & "' and no_2_no=" & intNo2
                myds = openmember("", "acm010", sqlstr)
                If myds.Tables("acm010").Rows.Count > 0 Then
                    strAccno = nz(myds.Tables("acm010").Rows(0).Item("accno"), "")   '餘絀撥補之科目
                    '將上年度之本期餘絀補補至該科目
                    sqlstr = "select * from acm010 where accno='32301'"
                    myds = openmember("", "acm010", sqlstr)
                    If myds.Tables("acm010").Rows.Count > 0 Then
                        intAmt3 = myds.Tables("acm010").Rows(0).Item("amt3")
                        intAmt4 = myds.Tables("acm010").Rows(0).Item("amt4")
                        sqlstr = "update acm010 set amt3=amt3 + " & intAmt3 & ", amt4=amt4 + " & intAmt4 & _
                                 " where accno='" & strAccno & "'"
                        retstr = runsql(mastconn, sqlstr)
                        sqlstr = "update acm010 set amt3=0, amt4=0 where accno='32301'"
                        retstr = runsql(mastconn, sqlstr)
                    End If
                End If
            End If
        End If

        '刪全為0
        sqlstr = " delete FROM acm010 where amt1+amt2+AMT3+AMT4=0"
        retstr = runsql(mastconn, sqlstr)

        '各欄數值計算(本年度,上年度餘額)
        'sqlstr = " Update ACM010 SET amt5 = amt1 - amt2, amt6 = amt3 - amt4 where substring(accno,1,1)='1'"
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
                 "where len(accno)=3 " & _
                 "GROUP BY substring(accno, 1, 2)) a left outer join ACCNAME b ON a.accno = b.ACCNO"
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then MsgBox("統計二級 error " & sqlstr)

        '統計一級
        sqlstr = "INSERT INTO ACM010 select a.accno, b.accname, a.amt1, a.amt2, a.amt3, " & _
                 "a.amt4, a.amt5, a.amt6, a.amt7 from " & _
                 "(SELECT substring(accno, 1, 1) AS accno, SUM(amt1) AS amt1, " & _
                 "SUM(amt2) AS amt2, SUM(amt3) AS amt3, SUM(amt4) AS amt4, " & _
                 "SUM(amt5) AS amt5, SUM(amt6) AS amt6, 0 AS amt7 from acm010 " & _
                 "where len(accno)=5 " & _
                 "GROUP BY substring(accno, 1, 1)) a left outer join ACCNAME b ON a.accno = b.ACCNO"
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then MsgBox("統計一級 error " & sqlstr)
        'sqlstr = "select accno amt5,  amt6 from acm010 where where len(accno)=1"

        '丟入dataset 
        sqlstr = "SELECT * FROM acm010 order by accno"
        myds = openmember("", "acm010", sqlstr)
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
        Dim TOTyy, TOTup, TOTyy2, TOTup2 As Decimal
        TOTyy = 0 : TOTup = 0 : TOTyy2 = 0 : TOTup2 = 0
        Dim decAmt5, decAmt6, decAmt7 As Decimal
        Dim TempAccno22, TempAccno24, TempAccno32, TempAccno34, TempAccno323 As Decimal

        Try
            SYear = GetYear(dtpDateS.Value)
            TransPara.TransP("LastDay") = dtpDateS.Value

            Call LoadGridFunc()
            If myds.Tables("acm010").Rows.Count <= 1 Then
                MsgBox("無此資料")
                Exit Sub
            End If

            Dim tt1, tt2 As String
            Try
                tt1 = "c:\App\acc\報表樣本\ACy010.xls"
                tt2 = "c:\App\acc\報表\ACY010.xls"
                If Not File.Exists(tt1) Then
                    MsgBox("找不到報表樣本，請洽資訊人員" & vbNewLine & tt1)
                    Exit Sub
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
            xlRange.Value = "中華民國 " & SYear & " 年" & Month(dtpDateS.Value) & "月" & DateTime.DaysInMonth(SYear + 1911, Month(dtpDateS.Value)) & "日"
            NAR(xlRange)

            i = 5    '自第6行開始放
            With myds.Tables("acm010")
                TOTyy = nz(.Rows(0).Item("amt5"), 0) '本年度資產總額
                TOTup = nz(.Rows(0).Item("amt6"), 0)  '上年度資產總額
                TOTyy2 = 0 : TOTup2 = 0
                Dim strTempAccno As String = Mid(.Rows(0).Item("accno"), 1, 1)
                For intR = 0 To .Rows.Count - 1
                    i += 1   '自第6行開始放
                    '拷貝目前這列到下一列,使得每列都有相同的格式設定
                    xlRange1 = xlsheet.Range("A" & i & ":H" & i)
                    xlRange2 = xlsheet.Range("A" & i + 1 & ":H" & i + 1)
                    xlRange1.Copy(xlRange2)
                    NAR(xlRange1)
                    NAR(xlRange2)

                    strAccno = .Rows(intR).Item("accno")
                    decAmt5 = nz(.Rows(intR).Item("amt5"), 0)
                    decAmt6 = nz(.Rows(intR).Item("amt6"), 0)
                    ''格式測試
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

                    If Mid(strAccno, 1, 1) <> strTempAccno Then
                        If strTempAccno = "1" Then    '列印資產合計
                            xlRange = xlCells(i, 1)
                            xlRange.Value = vbCrLf & "資產總額"
                            NAR(xlRange)
                            xlRange = xlCells(i, 3)
                            xlRange.Value = FormatNumber(TOTyy, 2)
                            NAR(xlRange)
                            xlRange = xlCells(i, 4)
                            xlRange.Value = FormatNumber(100, 0)
                            NAR(xlRange)
                            xlRange = xlCells(i, 5)
                            xlRange.Value = FormatNumber(TOTup, 2)
                            NAR(xlRange)
                            xlRange = xlCells(i, 6)
                            xlRange.Value = FormatNumber(100, 0)
                            NAR(xlRange)
                            xlRange = xlCells(i, 7)
                            xlRange.Value = FormatNumber(TOTyy - TOTup, 2)
                            NAR(xlRange)
                            xlRange = xlCells(i, 8)
                            xlRange.Value = FormatNumber(rate(TOTyy - TOTup, TOTup, 2), 2)
                            NAR(xlRange)
                            i += 1
                            '拷貝目前這列到下一列,使得每列都有相同的格式設定
                            xlRange1 = xlsheet.Range("A" & i & ":H" & i)
                            xlRange2 = xlsheet.Range("A" & i + 1 & ":H" & i + 1)
                            xlRange1.Copy(xlRange2)
                            NAR(xlRange1)
                            NAR(xlRange2)
                        ElseIf strTempAccno = "2" Then
                            xlRange = xlCells(i, 1)
                            xlRange.Value = vbCrLf & "負債合計"
                            NAR(xlRange)
                            xlRange = xlCells(i, 3)
                            xlRange.Value = FormatNumber(TempAccno22, 2)
                            NAR(xlRange)
                            xlRange = xlCells(i, 4)
                            xlRange.Value = FormatNumber(rate(TempAccno22, TOTyy, 2), 2) 'rate(TempAccno22, TempAccno22, 2)
                            NAR(xlRange)
                            xlRange = xlCells(i, 5)
                            xlRange.Value = FormatNumber(TempAccno24, 2)
                            NAR(xlRange)
                            xlRange = xlCells(i, 6)
                            xlRange.Value = FormatNumber(rate(TempAccno24, TOTup, 2), 2) 'rate(TempAccno24, TempAccno24, 2)
                            NAR(xlRange)
                            xlRange = xlCells(i, 7)
                            xlRange.Value = FormatNumber(TempAccno22 - TempAccno24, 2)
                            NAR(xlRange)
                            xlRange = xlCells(i, 8)
                            xlRange.Value = FormatNumber(rate(TempAccno22 - TempAccno24, TempAccno24, 2), 2) 'rate(xlRange.Value, TempAccno24, 2)
                            NAR(xlRange)
                            i += 1
                            '拷貝目前這列到下一列,使得每列都有相同的格式設定
                            xlRange1 = xlsheet.Range("A" & i & ":H" & i)
                            xlRange2 = xlsheet.Range("A" & i + 1 & ":H" & i + 1)
                            xlRange1.Copy(xlRange2)
                            NAR(xlRange1)
                            NAR(xlRange2)
                        End If
                        strTempAccno = Mid(strAccno, 1, 1)
                    End If
                    '-----放置欄位
                    xlRange = xlCells(i, 1)
                    If (strAccno = "13202" Or strAccno = "13302" Or strAccno = "13402" Or strAccno = "13502" Or strAccno = "13602") Then
                        xlRange.Value = Space(spaces) & "減:" & FormatAccno(strAccno) & vbCrLf & Space(spaces) & nz(.Rows(intR).Item("accname"), "")
                    Else
                        xlRange.Value = Space(spaces) & FormatAccno(strAccno) & vbCrLf & Space(spaces) & nz(.Rows(intR).Item("accname"), "")
                    End If
                    NAR(xlRange)
                    If strAccno = "2" Or strAccno = "3" Then
                        TOTyy2 += nz(.Rows(0).Item("amt5"), 0)
                        TOTup2 += nz(.Rows(0).Item("amt6"), 0)
                        If strAccno = "2" Then
                            TempAccno22 = decAmt5
                            TempAccno24 = decAmt6
                        Else
                            TempAccno32 = decAmt5
                            TempAccno34 = decAmt6
                        End If
                    End If
                    '本年度
                    If Len(strAccno) = 5 Then
                        xlRange = xlCells(i, 2)  '小計
                    Else
                        xlRange = xlCells(i, 3)  '合計
                    End If
                    If decAmt5 = 0 Then
                        xlRange.Value = ""
                    Else
                        xlRange.Value = FormatNumber(decAmt5, 2) 'FormatNumber(Math.Abs(decAmt5), 2)
                        If Len(strAccno) = 5 And Mid(strAccno, 1, 2) = "13" And Mid(strAccno, 4, 2) = "02" Then '備抵折舊 "13202" "13302" "13402"  "13502" "13602"
                            xlRange.Value = FormatNumber(-decAmt5, 2)  '不以減號表示
                        End If
                    End If
                    NAR(xlRange)
                    xlRange = xlCells(i, 4)   '百分比
                    xlRange.Value = FormatNumber(rate(decAmt5, TOTyy, 2), 2)
                    NAR(xlRange)
                    '上年度
                    xlRange = xlCells(i, 5)
                    If decAmt6 = 0 Then
                        xlRange.Value = ""
                    Else
                        xlRange.Value = FormatNumber(decAmt6, 2)
                        If Len(strAccno) = 5 And Mid(strAccno, 1, 2) = "13" And Mid(strAccno, 4, 2) = "02" Then '備抵折舊 
                            xlRange.Value = FormatNumber(-decAmt6, 2)  '不以減號表示
                        End If
                    End If
                    NAR(xlRange)
                    xlRange = xlCells(i, 6)  '百分比
                    xlRange.Value = FormatNumber(rate(decAmt6, TOTup, 2), 2)
                    NAR(xlRange)
                    '比較增減
                    xlRange = xlCells(i, 7)
                    If (decAmt5 - decAmt6) = 0 Then
                        xlRange.Value = ""
                    Else
                        xlRange.Value = FormatNumber(decAmt5 - decAmt6, 2)
                        If Len(strAccno) = 5 And Mid(strAccno, 1, 2) = "13" And Mid(strAccno, 4, 2) = "02" Then '備抵折舊 
                            xlRange.Value = FormatNumber(-(decAmt5 - decAmt6), 2)  '不以減號表示
                        End If
                    End If
                    NAR(xlRange)
                    xlRange = xlCells(i, 8)  '增減百分比
                    If (decAmt5 - decAmt6) = 0 Then
                        xlRange.Value = ""
                    Else
                        xlRange.Value = FormatNumber(rate(decAmt5 - decAmt6, decAmt6, 2), 2)
                    End If
                    NAR(xlRange)
                Next
            End With
            i += 1
            '拷貝目前這列到下一列,使得每列都有相同的格式設定
            xlRange1 = xlsheet.Range("A" & i & ":H" & i)
            xlRange2 = xlsheet.Range("A" & i + 1 & ":H" & i + 1)
            xlRange1.Copy(xlRange2)
            NAR(xlRange1)
            NAR(xlRange2)

            xlRange = xlCells(i, 1)
            xlRange.Value = vbCrLf & "淨值合計"
            NAR(xlRange)
            xlRange = xlCells(i, 3)
            xlRange.Value = FormatNumber(TempAccno32, 2)
            NAR(xlRange)
            xlRange = xlCells(i, 4)
            xlRange.Value = FormatNumber(rate(TempAccno32, TOTyy, 2), 2)
            NAR(xlRange)
            xlRange = xlCells(i, 5)
            xlRange.Value = FormatNumber(TempAccno34, 2)
            NAR(xlRange)

            xlRange = xlCells(i, 6)
            xlRange.Value = FormatNumber(rate(TempAccno34, TOTup, 2), 2)
            NAR(xlRange)

            xlRange = xlCells(i, 7)
            xlRange.Value = FormatNumber(TempAccno32 - TempAccno34, 2)
            xlRange1 = xlCells(i, 8)
            xlRange1.Value = FormatNumber(rate(xlRange.Value, TempAccno34, 2), 2)
            NAR(xlRange)
            NAR(xlRange1)
            i += 1
            '拷貝目前這列到下一列,使得每列都有相同的格式設定
            xlRange1 = xlsheet.Range("A" & i & ":H" & i)
            xlRange2 = xlsheet.Range("A" & i + 1 & ":H" & i + 1)
            xlRange1.Copy(xlRange2)
            NAR(xlRange1)
            NAR(xlRange2)

            ' xlsheet.Cells(i, 1) = "負債及淨值總額"
            xlRange = xlCells(i, 1)
            xlRange.Value = vbCrLf & "負債及淨值總額"
            NAR(xlRange)
            xlRange = xlCells(i, 3)
            xlRange.Value = FormatNumber(TempAccno22 + TempAccno32, 2)
            NAR(xlRange)
            xlRange = xlCells(i, 4)
            xlRange.Value = FormatNumber(rate((TempAccno22 + TempAccno32), TOTyy, 2), 2)
            NAR(xlRange)
            xlRange = xlCells(i, 5)
            xlRange.Value = FormatNumber(TempAccno24 + TempAccno34, 2)
            NAR(xlRange)
            xlRange = xlCells(i, 6)
            xlRange.Value = FormatNumber(rate((TempAccno24 + TempAccno34), TOTup, 2), 2)
            NAR(xlRange)
            xlRange = xlCells(i, 7)
            xlRange.Value = FormatNumber((TempAccno22 + TempAccno32) - (TempAccno24 + TempAccno34), 2)
            xlRange1 = xlCells(i, 8)
            xlRange1.Value = FormatNumber(rate(xlRange.Value, (TempAccno24 + TempAccno34), 2), 2)
            NAR(xlRange)
            NAR(xlRange1)
            i += 1
            xlRange = xlCells(i, 1)
            xlRange.Value = "註：預設年度決算發生短絀時，以撥用收入公積31101彌補之                                                                                                                                                      "
            NAR(xlRange)
            '儲存檔案
            xlbook.Save()
            If rdoPrint.Checked Then
                'xlapp.Visible = True
                'xlbook.PrintPreview() '不可用預覽列印,否則當使用者關閉excel視窗之後就會導致finally區段關閉excel.exe時出現error
                xlbook.PrintOut()  '直接列印
            End If
            Dim result3 As DialogResult = MessageBox.Show("列印完畢,已存入 " & tt2 & vbNewLine & "是否要幫您開啟此份報表?", _
                 "", _
                 MessageBoxButtons.YesNoCancel, _
                 MessageBoxIcon.Question, _
                 MessageBoxDefaultButton.Button2)
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
