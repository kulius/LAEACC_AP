Imports JBC.Printing
Imports System.IO
Imports Microsoft.Office.Interop
Public Class ACP010
    Dim confid, confunit As String, LoadAfter, Dirty As Boolean
    Dim SYear As Integer
    Dim myds As DataSet

    Private Sub ACP010_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        nudYear.Value = GetYear(TransPara.TransP("LastDay"))
    End Sub

    Private Sub LoadGridFunc()
        Dim sqlstr, retstr, tempStr As String

        '先清空acm010 
        sqlstr = "delete from acm010"
        retstr = runsql(mastconn, sqlstr)

        '將本年度acf050四級科目丟入acm010
        sqlstr = "INSERT INTO ACM010  SELECT a.ACCNO, c.ACCNAME," & tempStr & _
         " a.DEAMT12 AS amt1, a.CRAMT12 AS amt2, b.deamt12 as amt3, b.cramt12 as amt4, 0 as amt5, 0 as amt6, 0 as amt7 " & _
         " FROM ACF050 a left outer join " & _
         " (select accno, deamt12, cramt12 from acf050 where accyear=" & SYear - 1 & " and len(accno)=5 and substring(accno,1,1)<='3') b " & _
         " on a.accno=b.accno left outer join ACCNAME c ON a.ACCNO = c.ACCNO " & _
         " WHERE a.ACCYEAR = " & SYear & " AND LEN(a.ACCNO) = 5 and substring(a.accno,1,1)<='3'"

        retstr = runsql(mastconn, sqlstr)

        '各欄數值計算(本年度,上年度餘額)
        sqlstr = " Update ACM010 SET amt5 = amt1 - amt2, amt6 = amt3 - amt4 where substring(accno,1,1)='1'"
        retstr = runsql(mastconn, sqlstr)
        sqlstr = " Update ACM010 SET amt5 = amt2 - amt1, amt6 = amt4 - amt3 where substring(accno,1,1)>'1'"
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then MsgBox("update acm010 error " & sqlstr)

        '統計三級
        sqlstr = "INSERT INTO ACM010 select a.accno, b.accname, a.amt1, a.amt2, a.amt3, " & _
                 " a.amt4, a.amt5, a.amt6, a.amt7 from " & _
                 " (SELECT substring(accno, 1, 3) AS accno, SUM(amt1) AS amt1, " & _
                 " SUM(amt2) AS amt2, SUM(amt3) AS amt3, SUM(amt4) AS amt4, " & _
                 " SUM(amt5) AS amt5, SUM(amt6) AS amt6, 0 AS amt7 from acm010 " & _
                 " GROUP BY substring(accno, 1, 3)) a left outer join ACCNAME b ON a.accno = b.ACCNO"
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then MsgBox("統計三級 error " & sqlstr)

        '統計二級
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
                 "where len(accno)=2 " & _
                 "GROUP BY substring(accno, 1, 1)) a left outer join ACCNAME b ON a.accno = b.ACCNO"
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then MsgBox("統計一級 error " & sqlstr)
        sqlstr = "select accno amt5,  amt6 from acm010 where where len(accno)=1"

        '丟入dataset 
        sqlstr = "SELECT * FROM acm010 order by accno"
        myds = openmember("", "acm010", sqlstr)
    End Sub

    Private Sub BtnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrint.Click
        Dim xlapp As Excel.Application
        Dim xlbook As Excel.Workbook
        Dim xlsheet As Excel.Worksheet
        SYear = nudYear.Value
        Call LoadGridFunc()
        If myds.Tables("acm010").Rows.Count <= 1 Then
            MsgBox("無此資料")
            Exit Sub
        End If
        Dim intR As Integer = 0  'control record number
        Dim i As Integer = 0 'control excel row 
        Dim strAccno As String
        Dim strS, strT As String
        Dim TOTyy, TOTup As Decimal
        Dim decAmt5, decAmt6 As Decimal
        xlapp = CreateObject("excel.application")
        Dim tt1, tt2 As String
        Try
            tt1 = "c:\App\ACC\報表樣本\ACP010.xls"
            tt2 = "c:\App\ACC\報表\ACP010.xls"
            FileCopy(tt1, tt2)    'copy tt1 to tt2
            xlbook = xlapp.Workbooks.Open(tt2)   '開啟tt2
        Catch ex As Exception
            MsgBox("報表copy " & tt1 & "  to  " & tt2 & "產生錯誤，請洽程式設計人員!")
            Exit Sub
        End Try
        xlsheet = xlbook.Worksheets(1)
        xlsheet.Cells(1, 1).value = "表3-3    " & SYear & "年度資產負債平衡表"
        xlsheet.Cells(2, 1).value = TransPara.TransP("UnitTitle")
        xlsheet.Cells(2, 4).value = "中華民國" & SYear & "年12月31日"

        i = 4    '自第5行開始放
        With myds.Tables("acm010")
            TOTyy = nz(.Rows(0).Item("amt5"), 0)  '本年度資產總額
            TOTyy = nz(.Rows(0).Item("amt6"), 0)  '上年度資產總額
            For intR = 0 To .Rows.Count - 1
                i += 1   '自第5行開始放
                'strS = Format(i, "0") & ":" & Format(i, "0")
                'strT = Format(i + 1, "0") & ":" & Format(i + 1, "0")
                'xlsheet.Rows("5:5").Select()
                'xlsheet.Rows(strs).Select()

                'xlsheet.Selection.Copy()
                'xlsheet.Rows(strT).Select()
                'xlsheet.ActiveSheet.Paste()
                strAccno = .Rows(intR).Item("accno")
                decAmt5 = nz(.Rows(intR).Item("amt5"), 0)
                decAmt6 = nz(.Rows(intR).Item("amt6"), 0)
                If strAccno = "2" Then    '列印資產合計
                    xlsheet.Cells(i, 1) = "資產總額"
                    xlsheet.Cells(i, 2).value = FormatNumber(TOTyy, 2)
                    xlsheet.Cells(i, 3).value = FormatNumber(rate(TOTyy, TOTyy, 2), 2)
                    xlsheet.Cells(i, 4).value = FormatNumber(TOTup, 2)
                    xlsheet.Cells(i, 5).value = FormatNumber(rate(TOTup, TOTup, 0), 2)
                    xlsheet.Cells(i, 6).value = FormatNumber(TOTyy - TOTup, 2)
                    xlsheet.Cells(i, 7).value = FormatNumber(rate(TOTyy, TOTup, 2), 2)
                    i += 1
                End If
                If Grade(strAccno) <= 1 Then
                    xlsheet.Cells(i, 1).value = FormatAccno(strAccno) & nz(.Rows(intR).Item("accname"), "")
                Else
                    xlsheet.Cells(i, 1).value = Space((Grade(strAccno) - 1) * 2) & FormatAccno(strAccno) & nz(.Rows(intR).Item("accname"), "")
                End If
                xlsheet.Cells(i, 2).value = FormatNumber(decAmt5, 2)
                xlsheet.Cells(i, 3).value = FormatNumber(rate(decAmt5, TOTyy, 2), 2)
                xlsheet.Cells(i, 4).value = FormatNumber(decAmt6, 2)
                xlsheet.Cells(i, 5).value = FormatNumber(rate(decAmt6, TOTup, 0), 2)
                xlsheet.Cells(i, 6).value = FormatNumber(decAmt5 - decAmt6, 2)
                xlsheet.Cells(i, 7).value = FormatNumber(rate(decAmt5, decAmt6, 2), 2)
            Next
        End With
        xlsheet.Cells(i, 1) = "負債及淨值總額"
        xlsheet.Cells(i, 2).value = FormatNumber(TOTyy, 2)
        xlsheet.Cells(i, 3).value = FormatNumber(rate(TOTyy, TOTyy, 2), 2)
        xlsheet.Cells(i, 4).value = FormatNumber(TOTup, 2)
        xlsheet.Cells(i, 5).value = FormatNumber(rate(TOTup, TOTup, 0), 2)
        xlsheet.Cells(i, 6).value = FormatNumber(TOTyy - TOTup, 2)
        xlsheet.Cells(i, 7).value = FormatNumber(rate(TOTyy, TOTup, 2), 2)

        xlbook.Save()

        If rdoPrint.Checked Then
            xlbook.PrintOut()
        End If
        xlbook.Close()
        xlsheet = Nothing
        xlbook = Nothing
        xlapp.Quit()
        xlapp = Nothing
        MsgBox("列印完畢,已存入c:\app\acc\報表\acp010.xls")
        Me.Close()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class
