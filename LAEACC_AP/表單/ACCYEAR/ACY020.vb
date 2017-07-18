Imports JBC.Printing
Imports System.IO
Imports System.Math
Imports Microsoft.VisualBasic
Imports Microsoft.Office.Interop
Public Class ACY020
    Dim SYear As Integer
    Dim sAmt5, sAmt6, sAmt7, TOTamt5, Totamt6, Totamt7 As Decimal
    Dim myds As DataSet
    Dim xlCells As Excel.Range

    Private Sub ACY020_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dtpDateS.Value = TransPara.TransP("LastDay")
        If TransPara.TransP("UnitTitle").indexof("嘉南") >= 0 Then
            lblchianan.Visible = True
        End If
    End Sub
    Private Sub LoadGridFunc()
        Dim sqlstr, retstr As String
        Dim intAmt As Integer
        '嘉南   
        If TransPara.TransP("UnitTitle").indexof("嘉南") >= 0 Then
            sqlstr = "UPDATE ACY020 SET ACCNAME=C.ACCNAME FROM accname c where acy020.accno=c.accno "
            retstr = runsql(mastconn, sqlstr)
            sqlstr = "UPDATE ACY020 SET AMT1=a.DEAMT12, AMT2=a.CRAMT12 FROM acf050 a where acy020.accno=a.accno and a.accyear= " & SYear
            retstr = runsql(mastconn, sqlstr)
            sqlstr = "UPDATE ACY020 SET AMT3=b.deamt12, AMT4=b.cramt12 FROM acf050 b where acy020.accno=b.accno and b.accyear= " & SYear - 1
            retstr = runsql(mastconn, sqlstr)
            sqlstr = "UPDATE ACY020 SET AMT7=b.amt7 from (select accno, (bg1+bg2+bg3+bg4+bg5+up1+up2+up3+up4+up5) as amt7 " & _
                     "from accbg where accyear=" & SYear & ") b where b.accno=acy020.accno )"
            retstr = runsql(mastconn, sqlstr)

            '計算風景區餘絀
            sqlstr = "update acy020 set amt1=b.amt11, amt2=b.amt12, amt3=b.amt13, amt4=b.amt14, amt7=b.amt17 from " & _
                     "(select sum(amt1) as amt11, sum(amt2) as amt12, sum(amt3) as amt13, sum(amt4) as amt14, sum(amt7) as amt17 " & _
                     "from acy020 where left(accno,1)='4' group by left(accno,1)) b where acy020.accno='32301    I'"
            retstr = runsql(mastconn, sqlstr)
            sqlstr = "update acy020 set amt1=amt1+b.amt11, amt2=amt2+b.amt12, amt3=amt3+b.amt13, amt4=amt4+b.amt14, amt7=amt7-b.amt17 " & _
                     " from (select sum(amt1) as amt11, sum(amt2) as amt12, sum(amt3) as amt13, sum(amt4) as amt14, sum(amt7) as amt17 " & _
                     "from acy020 where left(accno,1)='5' group by left(accno,1)) b where acy020.accno='32301    I'"
            retstr = runsql(mastconn, sqlstr)
            sqlstr = "select * from acy020 where accno='32301    I'"
            myds = openmember("", "acm010", sqlstr)
            If myds.Tables("acm010").Rows.Count > 0 Then
                With myds.Tables("acm010")
                    sAmt5 = nz(.Rows(0).Item("amt2"), 0) - nz(.Rows(0).Item("amt1"), 0)  '本年度餘絀
                    sAmt6 = nz(.Rows(0).Item("amt4"), 0) - nz(.Rows(0).Item("amt3"), 0)  '上年度餘絀
                    sAmt7 = nz(.Rows(0).Item("amt7"), 0)                                 '本年度預算餘絀
                End With
                sqlstr = "update acy020 set amt5=" & sAmt5 & ", amt6=" & sAmt6 & ", amt7=" & sAmt7 & " where accno='32301    I'"
                retstr = runsql(mastconn, sqlstr)
            End If
        End If

        '先清空acm010 
        sqlstr = "delete from acm010"
        retstr = runsql(mastconn, sqlstr)

        '將本年度acf050四級科目put amt1,amt2 & 上年度acf050四級科目put amt3, amt4,本年度預算accbg四級科目put amt7  INSERT to acm010  
        sqlstr = "INSERT INTO ACM010  SELECT C.ACCNO, c.ACCNAME," & _
         " a.DEAMT12 AS amt1, a.CRAMT12 AS amt2, b.deamt12 as amt3, b.cramt12 as amt4, 0 as amt5, 0 as amt6, d.amt7 as amt7 FROM " & _
         "(select * from accname where len(accno)=5 and substring(accno,1,1)>='4') c left outer join " & _
         "(select accno, deamt12, cramt12 from acf050 where accyear=" & SYear & " and len(accno)=5 and substring(accno,1,1)>='4') a " & _
         " on c.accno=a.accno left outer join " & _
         "(select accno, deamt12, cramt12 from acf050 where accyear=" & SYear - 1 & " and len(accno)=5 and substring(accno,1,1)>='4') b " & _
         "on c.accno=b.accno left outer join " & _
        " (select accno, (bg1+bg2+bg3+bg4+bg5+up1+up2+up3+up4+up5) as amt7 from accbg where accyear=" & SYear & " and len(accno)=5 and substring(accno,1,1)>='4') d " & _
        " on c.accno=d.accno "
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
        sqlstr = " update acm010 set amt7=0 where amt7 is null"
        retstr = runsql(mastconn, sqlstr)
        sqlstr = " delete acm010 where amt1+amt2+AMT3+AMT4+amt7=0"
        retstr = runsql(mastconn, sqlstr)


        '各欄數值計算(本年度,上年度餘額)
        sqlstr = " Update ACM010 SET amt5 = amt1 - amt2, amt6 = amt3 - amt4 where substring(accno,1,1)='5'"

        retstr = runsql(mastconn, sqlstr)
        sqlstr = " Update ACM010 SET amt5 = amt2 - amt1, amt6 = amt4 - amt3 where substring(accno,1,1)='4'"
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then MsgBox("update acm010 error " & sqlstr)

        ''統計三級()
        sqlstr = "INSERT INTO ACM010 select a.accno, b.accname, a.amt1, a.amt2, a.amt3, " & _
                 "a.amt4, a.amt5, a.amt6, a.amt7 from " & _
                 "(SELECT substring(accno, 1, 3) AS accno, SUM(amt1) AS amt1, " & _
                 "SUM(amt2) AS amt2, SUM(amt3) AS amt3, SUM(amt4) AS amt4, " & _
                 "SUM(amt5) AS amt5, SUM(amt6) AS amt6, sum(amt7) AS amt7 from acm010 " & _
                 "GROUP BY substring(accno, 1, 3)) a left outer join ACCNAME b ON a.accno = b.ACCNO"
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then MsgBox("統計三級 error " & sqlstr)

        ''統計二級
        sqlstr = "INSERT INTO ACM010 select a.accno, b.accname, a.amt1, a.amt2, a.amt3, " & _
                 "a.amt4, a.amt5, a.amt6, a.amt7 from " & _
                 "(SELECT substring(accno, 1, 2) AS accno, SUM(amt1) AS amt1, " & _
                 "SUM(amt2) AS amt2, SUM(amt3) AS amt3, SUM(amt4) AS amt4, " & _
                 "SUM(amt5) AS amt5, SUM(amt6) AS amt6, sum(amt7) AS amt7 from acm010 " & _
                 "where len(accno)=3 " & _
                 "GROUP BY substring(accno, 1, 2)) a left outer join ACCNAME b ON a.accno = b.ACCNO"
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then MsgBox("統計二級 error " & sqlstr)


        '統計一級
        sqlstr = "INSERT INTO ACM010 select a.accno, b.accname, a.amt1, a.amt2, a.amt3, " & _
                 "a.amt4, a.amt5, a.amt6, a.amt7 from " & _
                 "(SELECT substring(accno, 1, 1) AS accno, SUM(amt1) AS amt1, " & _
                 "SUM(amt2) AS amt2, SUM(amt3) AS amt3, SUM(amt4) AS amt4, " & _
                 "SUM(amt5) AS amt5, SUM(amt6) AS amt6, sum(amt7) AS amt7 from acm010 " & _
                 "where len(accno)=5 " & _
                 "GROUP BY substring(accno, 1, 1)) a left outer join ACCNAME b ON a.accno = b.ACCNO"
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then MsgBox("統計一級 error " & sqlstr)
        sqlstr = "select accno amt5,  amt6 from acm010 where where len(accno)=1"

        '嘉南   
        If TransPara.TransP("UnitTitle").indexof("嘉南") >= 0 Then
            sqlstr = "select amt5, amt6, amt7 from acm010 where accno='4'"
            myds = openmember("", "acm010", sqlstr)
            If myds.Tables(0).Rows.Count > 0 Then
                TOTamt5 = nz(myds.Tables(0).Rows(0).Item("amt5"), 0)
                Totamt6 = nz(myds.Tables(0).Rows(0).Item("amt6"), 0)
                Totamt7 = nz(myds.Tables(0).Rows(0).Item("amt7"), 0)
            End If
            sqlstr = "select amt5, amt6, amt7 from acm010 where accno='5'"
            myds = openmember("", "acm010", sqlstr)
            If myds.Tables(0).Rows.Count > 0 Then
                TOTamt5 = TOTamt5 - nz(myds.Tables(0).Rows(0).Item("amt5"), 0)
                Totamt6 = Totamt6 - nz(myds.Tables(0).Rows(0).Item("amt6"), 0)
                Totamt7 = Totamt7 - nz(myds.Tables(0).Rows(0).Item("amt7"), 0)
            End If
            sqlstr = "update acy020 set amt5=" & TOTamt5 - sAmt5 & ", amt6=" & Totamt6 - sAmt6 & ", amt7=" & Totamt7 - sAmt7 & " where accno='32301    00'"
            retstr = runsql(mastconn, sqlstr)
        End If

        '丟入dataset
        sqlstr = "SELECT * FROM acm010 order by accno"
        myds = openmember("", "acm010", sqlstr)


    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub BtnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrint.Click
        Dim xlapp As Excel.Application
        Dim xlbook As Excel.Workbook
        Dim xlbooks As Excel.Workbooks
        Dim xlsheet As Excel.Worksheet
        Dim xlsheets As Excel.Sheets
        Dim xlRange As Excel.Range
        Dim xlRange1 As Excel.Range
        Dim xlRange2 As Excel.Range
        Dim xlRange3 As Excel.Range
        Dim xlRange4 As Excel.Range
        Dim xlRange5 As Excel.Range


        Try

            SYear = GetYear(dtpDateS.Value)
            TransPara.TransP("LastDay") = dtpDateS.Value

            Call LoadGridFunc()
            If myds.Tables("acm010").Rows.Count <= 1 Then
                MsgBox("無此資料")
                Exit Sub
            End If



            Dim intR As Integer = 0  'control record number
            Dim i As Integer = 0 'control excel row 
            Dim strAccno As String
            Dim strS, strT As String
            Dim TOTyya, TOTupa, TOTbga, TOTyy2a, TOTup2a, TOTbg2a As Decimal
            Dim TOTyy, TOTup, TOTbg, TOTyy2, TOTup2, TOTbg2 As Decimal
            Dim Temp141, Temp151, Temp142, Temp152, Temp241, Temp251, Temp242, Temp252 As Decimal
            Dim Rate41, Rate51, Rate42, Rate52 As Decimal
            Dim Rate741, Rate751, Rate742, Rate752 As Decimal
            Dim Rate1141, Rate1151, Rate1142, Rate1152 As Decimal
            Dim spaces As Integer
            TOTyy = 0 : TOTup = 0 : TOTbg = 0 : TOTyy2 = 0 : TOTup2 = 0 : TOTbg2 = 0
            Dim decAmt5, decAmt6, decAmt7 As Decimal
            xlapp = CreateObject("excel.application")
            Dim tt1, tt2 As String
            Try
                tt1 = "c:\App\acc\報表樣本\ACY020.xls"
                tt2 = "c:\App\acc\報表\ACY020.xls"
                If Not File.Exists(tt1) Then
                    MsgBox("找不到報表樣本，請洽資訊人員" & vbNewLine & tt1)
                    Exit Sub
                End If
                FileCopy(tt1, tt2)    'copy tt1 to tt2
                'xlbook = xlapp.Workbooks.Open(tt2)   '開啟tt2
                xlbooks = xlapp.Workbooks
                xlbook = xlbooks.Open(tt2) '開啟tt2
            Catch ex As Exception
                MsgBox("報表copy " & tt1 & "  to  " & tt2 & "錯誤，是否\報表\ACY020.XLS使用中,否則請洽程式設計人員!")
                Exit Sub
            End Try
            '   xlsheet = xlbook.Worksheets(1)
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


            i = 5    '自第6行開始放
            With myds.Tables("acm010")
                If .Rows(intR).Item("accno") = "4" Then
                    TOTyya = nz(.Rows(intR).Item("amt5"), 0)  '本年度收入總額 以此為100%
                    TOTupa = nz(.Rows(intR).Item("amt6"), 0)  '上年度收入總額 以此為100%
                    TOTbga = nz(.Rows(intR).Item("amt7"), 0)  '本年度收入預算總額
                End If
                For intR = 0 To .Rows.Count - 1
                    If .Rows(intR).Item("accno") = "5" Then
                        TOTyy2a = nz(.Rows(intR).Item("amt5"), 0)  '本年度收入總額
                        TOTup2a = nz(.Rows(intR).Item("amt6"), 0)  '上年度收入總額
                        TOTbg2a = nz(.Rows(intR).Item("amt7"), 0)  '本年度收入預算總額
                        Exit For
                    End If
                Next

                ' Dim strTempAccno As String = Mid(.Rows(0).Item("accno"), 1, 1)

                '科目4-1
                For intR = 0 To .Rows.Count - 1
                    If Mid(.Rows(intR).Item("accno"), 1, 2) = "41" Then
                        i += 1   '自第6行開始放
                        strAccno = .Rows(intR).Item("accno")
                        decAmt5 = nz(.Rows(intR).Item("amt5"), 0)
                        decAmt6 = nz(.Rows(intR).Item("amt6"), 0)
                        decAmt7 = nz(.Rows(intR).Item("amt7"), 0)
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
                        '拷貝目前這列到下一列,使得每列都有相同的格式設定
                        xlRange1 = xlsheet.Range("A" & i & ":K" & i)
                        xlRange2 = xlsheet.Range("A" & i + 1 & ":K" & i + 1)
                        xlRange1.Copy(xlRange2)
                        NAR(xlRange1)
                        NAR(xlRange2)

                        If Grade(strAccno) < 1 Then
                            xlRange = xlCells(i, 3)
                            xlRange.Value = FormatAccno(strAccno) & vbCrLf & Space(spaces) & nz(.Rows(intR).Item("accname"), "")
                            NAR(xlRange)
                        Else
                            xlRange = xlCells(i, 3)
                            xlRange.Value = Space((Grade(strAccno) - 1) * 2) & FormatAccno(strAccno) & vbCrLf & Space(spaces) & nz(.Rows(intR).Item("accname"), "")
                            NAR(xlRange)
                        End If
                        If .Rows(intR).Item("accno") = "41" Then
                            TOTyy = nz(.Rows(intR).Item("amt5"), 0)
                            TOTup = nz(.Rows(intR).Item("amt6"), 0)
                            TOTbg = nz(.Rows(intR).Item("amt7"), 0)
                            '合計'
                            Temp141 = FormatNumber(decAmt5, 2)
                            Temp241 = FormatNumber(decAmt7, 2)
                            Rate41 = FormatNumber(rate(decAmt6, TOTupa, 2), 2)
                            Rate741 = FormatNumber(rate(decAmt5, TOTyya, 2), 2)
                            Rate1141 = FormatNumber(rate(decAmt7, TOTbga, 2), 2)
                        End If
                        xlRange = xlCells(i, 1)
                        xlRange.Value = FormatNumber(decAmt6, 2)
                        NAR(xlRange)
                        xlRange = xlCells(i, 2)
                        xlRange.Value = FormatNumber(rate(decAmt6, TOTupa, 2), 2)
                        NAR(xlRange)
                        If Grade(strAccno) = 4 Then
                            xlRange = xlCells(i, 4)   '小計
                        Else
                            xlRange = xlCells(i, 5)   '合計'
                        End If
                        xlRange.Value = FormatNumber(decAmt5, 2)
                        NAR(xlRange)

                        xlRange = xlCells(i, 6) '決算數百分比
                        xlRange.Value = FormatNumber(rate(decAmt5, TOTyya, 2), 2)
                        NAR(xlRange)


                        If Grade(strAccno) = 4 Then
                            xlRange = xlCells(i, 7)  '小計
                        Else
                            xlRange = xlCells(i, 8)  '合計'
                        End If
                        xlRange.Value = FormatNumber(decAmt7, 0)
                        NAR(xlRange)

                        xlRange = xlCells(i, 9)   '預算數百分比
                        xlRange.Value = FormatNumber(rate(decAmt7, TOTbga, 2), 2)
                        NAR(xlRange)
                        xlRange1 = xlCells(i, 10)  '比較增減
                        xlRange1.Value = FormatNumber(decAmt5 - decAmt7, 2)
                        'NAR(xlRange)
                        xlRange = xlCells(i, 11)
                        xlRange.Value = FormatNumber(rate(decAmt5 - decAmt7, decAmt7, 2), 2)
                        NAR(xlRange)
                        NAR(xlRange1)
                    End If
                Next
                '科目5-1
                For intR = 0 To .Rows.Count - 1
                    If Mid(.Rows(intR).Item("accno"), 1, 2) = "51" Then
                        i += 1   '自第6行開始放
                        strAccno = .Rows(intR).Item("accno")
                        decAmt5 = nz(.Rows(intR).Item("amt5"), 0)
                        decAmt6 = nz(.Rows(intR).Item("amt6"), 0)
                        decAmt7 = nz(.Rows(intR).Item("amt7"), 0)
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
                        '拷貝目前這列到下一列,使得每列都有相同的格式設定
                        xlRange1 = xlsheet.Range("A" & i & ":K" & i)
                        xlRange2 = xlsheet.Range("A" & i + 1 & ":K" & i + 1)
                        xlRange1.Copy(xlRange2)
                        NAR(xlRange1)
                        NAR(xlRange2)

                        If Grade(strAccno) < 1 Then
                            xlRange = xlCells(i, 3)
                            xlRange.Value = FormatAccno(strAccno) & vbCrLf & Space(spaces) & nz(.Rows(intR).Item("accname"), "")
                            NAR(xlRange)
                        Else
                            xlRange = xlCells(i, 3)
                            xlRange.Value = Space((Grade(strAccno) - 1) * 2) & FormatAccno(strAccno) & vbCrLf & Space(spaces) & nz(.Rows(intR).Item("accname"), "")
                            NAR(xlRange)
                        End If
                        If strAccno = "51" Then '支出
                            xlRange = xlCells(i, 3)
                            xlRange.Value = "減:" & FormatAccno(strAccno) & vbCrLf & Space(spaces) & nz(.Rows(intR).Item("accname"), "")
                            NAR(xlRange)
                            TOTyy2 = nz(.Rows(intR).Item("amt5"), 0)   '本年度支出總額
                            TOTup2 = nz(.Rows(intR).Item("amt6"), 0)   '上年度支出總額
                            TOTbg2 = nz(.Rows(intR).Item("amt7"), 0)   '本年度支出預算總額
                            '合計
                            Temp151 = FormatNumber(decAmt5, 2)
                            Temp251 = FormatNumber(decAmt7, 2)
                            Rate51 = FormatNumber(rate(decAmt6, TOTupa, 2), 2)
                            Rate751 = FormatNumber(rate(decAmt5, TOTyya, 2), 2)
                            Rate1151 = FormatNumber(rate(decAmt7, TOTbga, 2), 2)
                        End If

                        xlRange = xlCells(i, 1)
                        xlRange.Value = FormatNumber(decAmt6, 2)
                        NAR(xlRange)
                        xlRange = xlCells(i, 2)
                        xlRange.Value = FormatNumber(rate(decAmt6, TOTupa, 2), 2)
                        NAR(xlRange)
                        If Grade(strAccno) = 4 Then
                            xlRange = xlCells(i, 4)  '小計
                        Else
                            xlRange = xlCells(i, 5)   '合計                            
                        End If
                        xlRange.Value = FormatNumber(decAmt5, 2)
                        NAR(xlRange)

                        xlRange = xlCells(i, 6)   '百分比
                        xlRange.Value = FormatNumber(rate(decAmt5, TOTyya, 2), 2)
                        NAR(xlRange)

                        If Grade(strAccno) = 4 Then
                            xlRange = xlCells(i, 7)
                        Else
                            xlRange = xlCells(i, 8)
                        End If
                        xlRange.Value = FormatNumber(decAmt7, 0)
                        NAR(xlRange)

                        xlRange = xlCells(i, 9)
                        xlRange.Value = FormatNumber(rate(decAmt7, TOTbga, 2), 2)
                        NAR(xlRange)
                        xlRange1 = xlCells(i, 10)
                        xlRange1.Value = FormatNumber(decAmt5 - decAmt7, 0)
                        ' NAR(xlRange)
                        xlRange = xlCells(i, 11)
                        xlRange.Value = FormatNumber(rate(decAmt5 - decAmt7, decAmt7, 2), 2)
                        NAR(xlRange)
                        NAR(xlRange1)
                    End If
                Next
                '事業餘絀
                i += 1
                '拷貝目前這列到下一列,使得每列都有相同的格式設定
                xlRange1 = xlsheet.Range("A" & i & ":K" & i)
                xlRange2 = xlsheet.Range("A" & i + 1 & ":K" & i + 1)
                xlRange1.Copy(xlRange2)
                NAR(xlRange1)
                NAR(xlRange2)
                xlRange = xlCells(i, 3)
                xlRange.Value = vbCrLf & "事業餘絀"
                NAR(xlRange)
                xlRange3 = xlCells(i, 1)
                xlRange3.Value = FormatNumber(TOTup - TOTup2)
                xlRange = xlCells(i, 2)
                xlRange.Value = FormatNumber(rate(TOTup - TOTup2, TOTupa, 2), 2) 'Rate41 - Rate51
                NAR(xlRange)
                xlRange = xlCells(i, 5)
                xlRange.Value = FormatNumber(Temp141 - Temp151, 2)
                xlRange5 = xlCells(i, 6)
                xlRange5.Value = FormatNumber(rate(Temp141 - Temp151, TOTyya, 2), 2) 'Rate741 - Rate751
                NAR(xlRange5)
                xlRange1 = xlCells(i, 8)
                xlRange1.Value = FormatNumber(Temp241 - Temp251)
                xlRange5 = xlCells(i, 9)
                xlRange5.Value = FormatNumber(rate(Temp241 - Temp251, TOTbga, 2), 2) 'Rate1141 - Rate1151
                NAR(xlRange5)
                xlRange2 = xlCells(i, 10)
                xlRange2.Value = FormatNumber(xlRange.Value - xlRange1.Value, 2)
                NAR(xlRange)
                xlRange = xlCells(i, 11)
                xlRange.Value = FormatNumber(rate(xlRange2.Value, xlRange1.Value, 2), 2)
                NAR(xlRange1)
                NAR(xlRange2)
                NAR(xlRange)

                '科目4-2
                For intR = 0 To .Rows.Count - 1
                    If Mid(.Rows(intR).Item("accno"), 1, 2) = "42" Then
                        i += 1   '自第6行開始放
                        strAccno = .Rows(intR).Item("accno")
                        decAmt5 = nz(.Rows(intR).Item("amt5"), 0)
                        decAmt6 = nz(.Rows(intR).Item("amt6"), 0)
                        decAmt7 = nz(.Rows(intR).Item("amt7"), 0)
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
                        '拷貝目前這列到下一列,使得每列都有相同的格式設定
                        xlRange1 = xlsheet.Range("A" & i & ":K" & i)
                        xlRange2 = xlsheet.Range("A" & i + 1 & ":K" & i + 1)
                        xlRange1.Copy(xlRange2)
                        NAR(xlRange1)
                        NAR(xlRange2)

                        If Grade(strAccno) < 1 Then
                            xlRange = xlCells(i, 3)
                            xlRange.Value = FormatAccno(strAccno) & vbCrLf & Space(spaces) & nz(.Rows(intR).Item("accname"), "")
                            NAR(xlRange)
                        Else
                            xlRange = xlCells(i, 3)
                            xlRange.Value = Space((Grade(strAccno) - 1) * 2) & FormatAccno(strAccno) & vbCrLf & Space(spaces) & nz(.Rows(intR).Item("accname"), "")
                            NAR(xlRange)
                        End If
                        If strAccno = "42" Then
                            xlRange = xlCells(i, 3)
                            xlRange.Value = "加:" & FormatAccno(strAccno) & vbCrLf & Space(spaces) & nz(.Rows(intR).Item("accname"), "")
                            NAR(xlRange)
                            TOTyy = nz(.Rows(intR).Item("amt5"), 0)   '本年度支出總額
                            TOTup = nz(.Rows(intR).Item("amt6"), 0)   '上年度支出總額
                            TOTbg = nz(.Rows(intR).Item("amt7"), 0)   '本年度支出預算總額
                            '合計
                            Temp142 = FormatNumber(decAmt5, 2)
                            Temp242 = FormatNumber(decAmt7, 2)
                            Rate42 = FormatNumber(rate(decAmt6, TOTupa, 2), 2)
                            Rate742 = FormatNumber(rate(decAmt5, TOTyya, 2), 2)
                            Rate1142 = FormatNumber(rate(decAmt7, TOTbga, 2), 2)
                        End If

                        xlRange = xlCells(i, 1)
                        xlRange.Value = FormatNumber(decAmt6, 2)
                        NAR(xlRange)
                        xlRange = xlCells(i, 2)
                        xlRange.Value = FormatNumber(rate(decAmt6, TOTupa, 2), 2)
                        NAR(xlRange)
                        If Grade(strAccno) = 4 Then
                            xlRange = xlCells(i, 4)
                        Else
                            xlRange = xlCells(i, 5)
                        End If
                        xlRange.Value = FormatNumber(decAmt5, 2)
                        NAR(xlRange)

                        xlRange = xlCells(i, 6)
                        xlRange.Value = FormatNumber(rate(decAmt5, TOTyya, 2), 2)
                        NAR(xlRange)

                        If Grade(strAccno) = 4 Then
                            xlRange = xlCells(i, 7)
                        Else
                            xlRange = xlCells(i, 8)
                        End If
                        xlRange.Value = FormatNumber(decAmt7, 0)
                        NAR(xlRange)

                        xlRange = xlCells(i, 9)
                        xlRange.Value = FormatNumber(rate(decAmt7, TOTbga, 2), 2)
                        NAR(xlRange)
                        xlRange1 = xlCells(i, 10)
                        xlRange1.Value = FormatNumber(decAmt5 - decAmt7, 2)
                        'NAR(xlRange)
                        xlRange = xlCells(i, 11)
                        xlRange.Value = FormatNumber(rate(decAmt5 - decAmt7, decAmt7, 2), 2)
                        NAR(xlRange)
                        NAR(xlRange)
                    End If
                Next
                '科目5-2
                For intR = 0 To .Rows.Count - 1
                    If Mid(.Rows(intR).Item("accno"), 1, 2) = "52" Then
                        i += 1   '自第6行開始放
                        strAccno = .Rows(intR).Item("accno")
                        decAmt5 = nz(.Rows(intR).Item("amt5"), 0)
                        decAmt6 = nz(.Rows(intR).Item("amt6"), 0)
                        decAmt7 = nz(.Rows(intR).Item("amt7"), 0)
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
                        '拷貝目前這列到下一列,使得每列都有相同的格式設定
                        xlRange1 = xlsheet.Range("A" & i & ":K" & i)
                        xlRange2 = xlsheet.Range("A" & i + 1 & ":K" & i + 1)
                        xlRange1.Copy(xlRange2)
                        NAR(xlRange1)
                        NAR(xlRange2)

                        If Grade(strAccno) < 1 Then
                            xlRange = xlCells(i, 3)
                            xlRange.Value = FormatAccno(strAccno) & vbCrLf & Space(spaces) & nz(.Rows(intR).Item("accname"), "")
                            NAR(xlRange)
                        Else
                            xlRange = xlCells(i, 3)
                            xlRange.Value = Space((Grade(strAccno) - 1) * 2) & FormatAccno(strAccno) & vbCrLf & Space(spaces) & nz(.Rows(intR).Item("accname"), "")
                            NAR(xlRange)
                        End If
                        If strAccno = "52" Then
                            xlRange = xlCells(i, 3)
                            xlRange.Value = "減:" & FormatAccno(strAccno) & vbCrLf & Space(spaces) & nz(.Rows(intR).Item("accname"), "")
                            NAR(xlRange)
                            TOTyy2 = nz(.Rows(intR).Item("amt5"), 0)   '本年度支出總額
                            TOTup2 = nz(.Rows(intR).Item("amt6"), 0)   '上年度支出總額
                            TOTbg2 = nz(.Rows(intR).Item("amt7"), 0)   '本年度支出預算總額
                            '合計
                            Temp152 = FormatNumber(decAmt5, 2)
                            Temp252 = FormatNumber(decAmt7, 2)
                            Rate42 = FormatNumber(rate(decAmt6, TOTupa, 2), 2)
                            Rate742 = FormatNumber(rate(decAmt5, TOTyya, 2), 2)
                            Rate1142 = FormatNumber(rate(decAmt7, TOTbga, 2), 2)
                        End If

                        xlRange = xlCells(i, 1)
                        xlRange.Value = FormatNumber(decAmt6, 2)
                        NAR(xlRange)
                        xlRange = xlCells(i, 2)
                        xlRange.Value = FormatNumber(rate(decAmt6, TOTupa, 2), 2)
                        NAR(xlRange)
                        If Grade(strAccno) = 4 Then
                            xlRange = xlCells(i, 4)
                        Else
                            xlRange = xlCells(i, 5)
                        End If
                        xlRange.Value = FormatNumber(decAmt5, 2)
                        NAR(xlRange)

                        xlRange = xlCells(i, 6)
                        xlRange.Value = FormatNumber(rate(decAmt5, TOTyya, 2), 2)
                        NAR(xlRange)

                        If Grade(strAccno) = 4 Then
                            xlRange = xlCells(i, 7)
                        Else
                            xlRange = xlCells(i, 8)
                        End If
                        xlRange.Value = FormatNumber(decAmt7, 0)
                        NAR(xlRange)

                        xlRange = xlCells(i, 9)
                        xlRange.Value = FormatNumber(rate(decAmt7, TOTbga, 2), 2)
                        NAR(xlRange)
                        xlRange1 = xlCells(i, 10)
                        xlRange1.Value = FormatNumber(decAmt5 - decAmt7, 2)
                        'NAR(xlRange)
                        xlRange = xlCells(i, 11)
                        xlRange.Value = FormatNumber(rate(decAmt5 - decAmt7, decAmt7, 2), 2)
                        NAR(xlRange)
                        NAR(xlRange1)
                    End If
                Next
                '事業餘外絀
                i += 1
                '拷貝目前這列到下一列,使得每列都有相同的格式設定
                xlRange1 = xlsheet.Range("A" & i & ":K" & i)
                xlRange2 = xlsheet.Range("A" & i + 1 & ":K" & i + 1)
                xlRange1.Copy(xlRange2)
                NAR(xlRange1)
                NAR(xlRange2)

                xlRange = xlCells(i, 3)
                xlRange.Value = vbCrLf & "事業外餘絀"
                NAR(xlRange)
                xlRange4 = xlCells(i, 1)
                xlRange4.Value = FormatNumber(TOTup - TOTup2)
                xlRange = xlCells(i, 2)
                xlRange.Value = FormatNumber(rate(TOTup - TOTup2, TOTupa, 2), 2) 'Rate42 - Rate52
                NAR(xlRange)
                xlRange = xlCells(i, 5)
                xlRange.Value = FormatNumber(Temp142 - Temp152, 2)
                xlRange5 = xlCells(i, 6)
                xlRange5.Value = FormatNumber(rate(Temp142 - Temp152, TOTyya, 2), 2) 'Rate742 - Rate752
                NAR(xlRange5)
                xlRange1 = xlCells(i, 8)
                xlRange1.Value = FormatNumber(Temp242 - Temp252)
                xlRange5 = xlCells(i, 9)
                xlRange5.Value = FormatNumber(rate(Temp242 - Temp252, TOTbga, 2), 2) 'Rate1142 - Rate1152
                NAR(xlRange5)
                xlRange2 = xlCells(i, 10)
                xlRange2.Value = FormatNumber(xlRange.Value - xlRange1.Value, 2)
                NAR(xlRange)
                xlRange = xlCells(i, 11)
                xlRange.Value = FormatNumber(rate(xlRange2.Value, xlRange1.Value, 2), 2)
                NAR(xlRange1)
                NAR(xlRange2)
                NAR(xlRange)
            End With
            i += 1
            xlRange1 = xlsheet.Range("A" & i & ":K" & i)
            xlRange2 = xlsheet.Range("A" & i + 1 & ":K" & i + 1)
            xlRange1.Copy(xlRange2)
            NAR(xlRange1)
            NAR(xlRange2)

            xlRange = xlCells(i, 3)
            xlRange.Value = vbCrLf & "本期餘絀"
            NAR(xlRange)
            xlRange = xlCells(i, 1)
            xlRange.Value = FormatNumber(xlRange3.Value + xlRange4.Value, 2)
            NAR(xlRange)
            xlRange = xlCells(i, 2)
            xlRange.Value = FormatNumber(rate(xlRange3.Value + xlRange4.Value, TOTupa, 2), 2)
            NAR(xlRange3)
            NAR(xlRange4)
            NAR(xlRange)
            xlRange4 = xlCells(i, 5)
            xlRange4.Value = FormatNumber(TOTyya - TOTyy2a, 2)
            'NAR(xlRange)
            xlRange = xlCells(i, 6)
            xlRange.Value = FormatNumber(rate(xlRange4.Value, TOTyya, 2), 2)

            NAR(xlRange)
            xlRange2 = xlCells(i, 8)
            xlRange2.Value = FormatNumber(TOTbga - TOTbg2a, 2)
            xlRange = xlCells(i, 9)
            xlRange.Value = FormatNumber(rate(xlRange2.Value, TOTbga, 2), 2)
            NAR(xlRange)
            xlRange1 = xlCells(i, 10)
            xlRange1.Value = FormatNumber(xlRange4.Value - xlRange2.Value, 2)
            NAR(xlRange4)
            xlRange = xlCells(i, 11)
            xlRange.Value = FormatNumber(rate(xlRange1.Value, xlRange2.Value, 2), 2)
            NAR(xlRange2)
            NAR(xlRange1)
            NAR(xlRange)

            '嘉南   

            If TransPara.TransP("UnitTitle").indexof("嘉南") >= 0 Then
                i += 1
                xlRange1 = xlsheet.Range("A" & i & ":K" & i)
                xlRange2 = xlsheet.Range("A" & i + 1 & ":K" & i + 1)
                xlRange1.Copy(xlRange2)
                NAR(xlRange1)
                NAR(xlRange2)

                xlRange = xlCells(i, 3)
                xlRange.Value = vbCrLf & "本期餘絀(三年輪灌區)"
                NAR(xlRange)
                xlRange = xlCells(i, 1)
                xlRange.Value = FormatNumber(TOTupa - TOTup2a - sAmt6, 2)
                NAR(xlRange)
                xlRange = xlCells(i, 2)
                xlRange.Value = FormatNumber(rate(TOTupa - TOTup2a - sAmt6, TOTupa, 2), 2)
                NAR(xlRange3)
                NAR(xlRange4)
                NAR(xlRange)
                xlRange4 = xlCells(i, 4)
                xlRange4.Value = FormatNumber(TOTyya - TOTyy2a - sAmt5, 2)
                'NAR(xlRange)
                xlRange = xlCells(i, 6)
                xlRange.Value = FormatNumber(rate(xlRange4.Value - sAmt5, TOTyya, 2), 2)

                NAR(xlRange)
                xlRange2 = xlCells(i, 7)
                xlRange2.Value = FormatNumber(TOTbga - TOTbg2a - sAmt7, 2)
                xlRange = xlCells(i, 9)
                xlRange.Value = FormatNumber(rate(xlRange2.Value - sAmt7, TOTbga, 2), 2)
                NAR(xlRange)
                xlRange1 = xlCells(i, 10)
                xlRange1.Value = FormatNumber(xlRange4.Value - xlRange2.Value, 2)
                NAR(xlRange4)
                xlRange = xlCells(i, 11)
                xlRange.Value = FormatNumber(rate(xlRange1.Value, xlRange2.Value, 2), 2)
                NAR(xlRange2)
                NAR(xlRange1)
                NAR(xlRange)
                i += 1
                xlRange = xlCells(i, 3)
                xlRange.Value = vbCrLf & "本期餘絀(風景區)"
                NAR(xlRange)
                xlRange = xlCells(i, 1)
                xlRange.Value = FormatNumber(sAmt6, 2)
                NAR(xlRange)
                xlRange = xlCells(i, 2)
                xlRange.Value = FormatNumber(rate(sAmt6, TOTupa, 2), 2)
                NAR(xlRange3)
                NAR(xlRange4)
                NAR(xlRange)
                xlRange4 = xlCells(i, 4)
                xlRange4.Value = FormatNumber(sAmt5, 2)
                'NAR(xlRange)
                xlRange = xlCells(i, 6)
                xlRange.Value = FormatNumber(rate(xlRange4.Value - sAmt5, TOTyya, 2), 2)

                NAR(xlRange)
                xlRange2 = xlCells(i, 7)
                xlRange2.Value = FormatNumber(sAmt7, 2)
                xlRange = xlCells(i, 9)
                xlRange.Value = FormatNumber(rate(xlRange2.Value - sAmt7, TOTbga, 2), 2)
                NAR(xlRange)
                xlRange1 = xlCells(i, 10)
                xlRange1.Value = FormatNumber(xlRange4.Value - xlRange2.Value, 2)
                NAR(xlRange4)
                xlRange = xlCells(i, 11)
                xlRange.Value = FormatNumber(rate(xlRange1.Value, xlRange2.Value, 2), 2)
                NAR(xlRange2)
                NAR(xlRange1)
                NAR(xlRange)
            End If

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
            NAR(xlCells)
            NAR(xlRange)
            NAR(xlRange1)
            NAR(xlRange2)
            NAR(xlRange3)
            NAR(xlRange4)
            NAR(xlRange5)

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
End Class
