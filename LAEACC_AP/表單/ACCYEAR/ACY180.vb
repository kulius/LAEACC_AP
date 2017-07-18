Imports JBC.Printing
Imports System.IO
Imports Microsoft.Office.Interop
Public Class ACY180
    Dim confid, confunit As String, LoadAfter, Dirty As Boolean
    Dim SYear As Integer
    Dim myds As DataSet

    Private Sub ACY170_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If TransPara.TransP("LastDay") <> String.Empty Then
            dtpDateS.Value = TransPara.TransP("LastDay")
        End If
    End Sub

    Private Sub LoadGridFunc()
        Dim sqlstr, retstr As String
        Dim intAmt As Integer
        Dim Sdate, Edate As Date

        '先清空acm010 
        sqlstr = "delete from acm010"
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then MsgBox("delete acm010 error " & sqlstr)

        '注意:如果四級科目下一定有明細(也就是不一定有五級科目),要改這個sql語法,還有統計的sql語法,以及列印明細的程式碼
        '產生所需資料並新增到acm010 (9碼)
        sqlstr = "insert into acm010 "
        sqlstr += "select c.accno, c.accname "
        sqlstr += ",case when a.deamt12 is null then 0 else a.deamt12 end as amt1 "
        sqlstr += ",case when a.cramt12 is null then 0 else a.cramt12 end as amt2 "
        sqlstr += ",case when d.amt3 is null then 0 else d.amt3 end as amt3 "
        sqlstr += ",case when d.amt4 is null then 0 else d.amt4 end as amt4 "
        sqlstr += ",(case when a.deamt12 is null then 0 else a.deamt12 end)-(case when a.cramt12 is null then 0 else a.cramt12 end)-(case when e.amt6 is null then 0 else e.amt6 end) as amt5 "
        sqlstr += ",case when e.amt6 is null then 0 else e.amt6 end as amt6 "
        sqlstr += ",case when d.amt7 is null then 0 else d.amt7 end as amt7 from "
        sqlstr += "(select * from accname where len(accno)=9 and substring(accno,1,1)='5') c "
        sqlstr += "left outer join "
        sqlstr += "(select accno, deamt12, cramt12 from acf050 where "
        sqlstr += "accyear=" & SYear & " and len(accno)=9 and substring(accno,1,1)='5') a "
        sqlstr += "on c.accno=a.accno "
        sqlstr += "left outer join "
        sqlstr += "(select accno, (bg1+bg2+bg3+bg4+bg5) as amt3,(up1+up2+up3+up4+up5) as amt4, (bg1+bg2+bg3+bg4+bg5+up1+up2+up3+up4+up5) as amt7 "
        sqlstr += "from accbg where accyear=" & SYear & " and len(accno)=9 and substring(accno,1,1)='5') d "
        sqlstr += "on c.accno=d.accno "
        sqlstr += "left outer join "
        sqlstr += "(select b.accno, sum(b.amt) as amt6 from "
        sqlstr += "(select left(accno,9) as accno, case dc when 2 then amt*-1 else amt end as amt "
        sqlstr += "from acf020 where accyear=" & SYear & " and left(accno,1)='5' and cotn_code='A' and no_2_no > 0) b "
        sqlstr += "group by b.accno) e "
        sqlstr += "on c.accno=e.accno "
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then MsgBox("insert acm010 error (9碼)" & vbNewLine & sqlstr)

        '產生所需資料並新增到acm010 (7碼)
        sqlstr = "insert into acm010 "
        sqlstr += "select c.accno, c.accname "
        sqlstr += ",case when a.deamt12 is null then 0 else a.deamt12 end as amt1 "
        sqlstr += ",case when a.cramt12 is null then 0 else a.cramt12 end as amt2 "
        sqlstr += ",case when d.amt3 is null then 0 else d.amt3 end as amt3 "
        sqlstr += ",case when d.amt4 is null then 0 else d.amt4 end as amt4 "
        sqlstr += ",(case when a.deamt12 is null then 0 else a.deamt12 end)-(case when a.cramt12 is null then 0 else a.cramt12 end)-(case when e.amt6 is null then 0 else e.amt6 end) as amt5 "
        sqlstr += ",case when e.amt6 is null then 0 else e.amt6 end as amt6 "
        sqlstr += ",case when d.amt7 is null then 0 else d.amt7 end as amt7 from "
        sqlstr += "(select * from accname where len(accno)=7 and substring(accno,1,1)='5') c "
        sqlstr += "left outer join "
        sqlstr += "(select accno, deamt12, cramt12 from acf050 where "
        sqlstr += "accyear=" & SYear & " and len(accno)=7 and substring(accno,1,1)='5') a "
        sqlstr += "on c.accno=a.accno "
        sqlstr += "left outer join "
        sqlstr += "(select accno, (bg1+bg2+bg3+bg4+bg5) as amt3,(up1+up2+up3+up4+up5) as amt4, (bg1+bg2+bg3+bg4+bg5+up1+up2+up3+up4+up5) as amt7 "
        sqlstr += "from accbg where accyear=" & SYear & " and len(accno)=7 and substring(accno,1,1)='5') d "
        sqlstr += "on c.accno=d.accno "
        sqlstr += "left outer join "
        sqlstr += "(select b.accno, sum(b.amt) as amt6 from "
        sqlstr += "(select left(accno,7) as accno, case dc when 2 then amt*-1 else amt end as amt "
        sqlstr += "from acf020 where accyear=" & SYear & " and left(accno,1)='5' and cotn_code='A' and no_2_no > 0) b "
        sqlstr += "group by b.accno) e "
        sqlstr += "on c.accno=e.accno "
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then MsgBox("insert acm010 error (7碼)" & vbNewLine & sqlstr)

        '清空全部amt皆為0的資料列
        sqlstr = "delete acm010 where amt1+amt2+amt3+amt4+amt5+amt6+amt7=0"
        retstr = runsql(mastconn, sqlstr)

        '統計5級
        sqlstr = "insert into acm010 select a.accno, case when b.accname is null then '' else b.accname end as accname, a.amt1, a.amt2, a.amt3, " & _
                 "a.amt4, a.amt5, a.amt6, a.amt7 from " & _
                 "(select substring(accno, 1, 7) as accno, sum(amt1) as amt1, " & _
                 "sum(amt2) as amt2, sum(amt3) as amt3, sum(amt4) as amt4, " & _
                 "sum(amt5) as amt5, sum(amt6) as amt6, sum(amt7) as amt7 from acm010 " & _
                 "where len(accno)=9 " & _
                 "group by substring(accno, 1, 7)) a left outer join ACCNAME b on a.accno = b.accno " & _
                 "where a.accno not in (select accno from acm010)" '要過濾掉已經存在的7碼會計科目
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then MsgBox("統計5級 error " & vbNewLine & sqlstr)

        '統計4級
        sqlstr = "insert into acm010 select a.accno, case when b.accname is null then '' else b.accname end as accname, a.amt1, a.amt2, a.amt3, " & _
                 "a.amt4, a.amt5, a.amt6, a.amt7 from " & _
                 "(select substring(accno, 1, 5) as accno, sum(amt1) as amt1, " & _
                 "sum(amt2) as amt2, sum(amt3) as amt3, sum(amt4) as amt4, " & _
                 "sum(amt5) as amt5, sum(amt6) as amt6, sum(amt7) as amt7 from acm010 " & _
                 "where len(accno)=7 " & _
                 "group by substring(accno, 1, 5)) a left outer join ACCNAME b on a.accno = b.accno"
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then MsgBox("統計4級 error " & vbNewLine & sqlstr)

        '統計3級
        sqlstr = "insert into acm010 select a.accno, case when b.accname is null then '' else b.accname end as accname, a.amt1, a.amt2, a.amt3, " & _
                 "a.amt4, a.amt5, a.amt6, a.amt7 from " & _
                 "(select substring(accno, 1, 3) as accno, sum(amt1) as amt1, " & _
                 "sum(amt2) as amt2, sum(amt3) as amt3, sum(amt4) as amt4, " & _
                 "sum(amt5) as amt5, sum(amt6) as amt6, sum(amt7) as amt7 from acm010 " & _
                 "where len(accno)=5 " & _
                 "group by substring(accno, 1, 3)) a left outer join ACCNAME b on a.accno = b.accno"
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then MsgBox("統計3級 error " & vbNewLine & sqlstr)

        '統計2級
        sqlstr = "insert into acm010 select a.accno, case when b.accname is null then '' else b.accname end as accname, a.amt1, a.amt2, a.amt3, " & _
                 "a.amt4, a.amt5, a.amt6, a.amt7 from " & _
                 "(select substring(accno, 1, 2) as accno, sum(amt1) as amt1, " & _
                 "sum(amt2) as amt2, sum(amt3) as amt3, sum(amt4) as amt4, " & _
                 "sum(amt5) as amt5, sum(amt6) as amt6, sum(amt7) as amt7 from acm010 " & _
                 "where len(accno)=3 " & _
                 "group by substring(accno, 1,2)) a left outer join ACCNAME b on a.accno = b.accno"
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then MsgBox("統計2級 error " & vbNewLine & sqlstr)

        '統計1級
        sqlstr = "insert into acm010 select a.accno, case when b.accname is null then '' else b.accname end as accname, a.amt1, a.amt2, a.amt3, " & _
                 "a.amt4, a.amt5, a.amt6, a.amt7 from " & _
                 "(select substring(accno, 1, 1) as accno, sum(amt1) as amt1, " & _
                 "sum(amt2) as amt2, sum(amt3) as amt3, sum(amt4) as amt4, " & _
                 "sum(amt5) as amt5, sum(amt6) as amt6, sum(amt7) as amt7 from acm010 " & _
                 "where len(accno)=2 " & _
                 "group by substring(accno, 1,1)) a left outer join ACCNAME b on a.accno = b.accno"
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then MsgBox("統計1級 error " & vbNewLine & sqlstr)


        '丟入dataset 
        sqlstr = "SELECT * FROM acm010 order by accno"
        myds = openmember("", "acm010", sqlstr)
    End Sub

    Private Sub BtnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrint.Click

        Dim xlapp As Excel.Application
        Dim xlbook As Excel.Workbook
        Dim xlbooks As Excel.Workbooks
        Dim xlsheet As Excel.Worksheet
        Dim xlsheets As Excel.Sheets
        Dim xlSheetDetail As Excel.Worksheet
        Dim xlSheetBase As Excel.Worksheet
        Dim xlShape As Excel.Shape
        Dim xlShapes As Excel.Shapes
        Dim xlChars As Excel.Characters
        Dim xlSelection As Object
        Dim xlRange As Excel.Range
        Dim xlRange1 As Excel.Range
        Dim xlRange2 As Excel.Range

        Try
            SYear = GetYear(dtpDateS.Value)
            TransPara.TransP("LastDay") = dtpDateS.Value

            Call LoadGridFunc()

            If myds.Tables("acm010").Rows.Count = 0 Then
                MsgBox("無此資料")
                Exit Sub
            End If

            Dim intR As Integer = 0  'control record number
            Dim i, ii As Integer    'control excel row , i是總表的row no. , ii是明細表的row no.
            Dim strAccno, strAccname As String
            Dim strS, strT As String
            Dim spaces As Integer
            Dim TOT1, TOT2, TOT3, TOT4, TOT5 As Decimal
            Dim dTOT1, dTOT2, dTOT3, dTOT4, dTOT5 As Decimal
            TOT1 = 0 : TOT2 = 0 : TOT3 = 0 : TOT4 = 0 : TOT5 = 0
            Dim decAmt3, decAmt4, decAmt5, decAmt6, decAmt7 As Decimal

            Dim tt1, tt2 As String
            Try
                tt1 = "c:\App\acc\報表樣本\ACY180.xls"
                tt2 = "c:\App\acc\報表\ACY180.xls"
                If Not File.Exists(tt1) Then
                    MsgBox("找不到報表樣本，請洽資訊人員" & vbNewLine & tt1)
                    Exit Sub
                End If
                FileCopy(tt1, tt2)    'copy tt1 to tt2
                xlapp = CreateObject("Excel.Application")
                xlapp.DisplayAlerts = False  '隱藏所有警告或確認訊息視窗,否則某些指令會失敗,譬如刪除工作表
                xlbooks = xlapp.Workbooks
                xlbook = xlbooks.Open(tt2)   '開啟tt2
            Catch ex As Exception
                MsgBox("報表copy from " & tt1 & "  to  " & tt2 & " 錯誤，若 \報表\ACY180.XLS 使用中請先關閉之，否則請洽資訊人員!")
                'Dim log As New LogInfo(LogType.Exception, "拷貝報表時發生錯誤" & vbNewLine & ex.ToString)
                'gLM.Log(log)
                Exit Sub
            End Try

            xlsheets = xlbook.Worksheets
            xlsheet = xlsheets(1)     '總表
            xlSheetBase = xlsheets(2) '支出明細表的樣本
            xlSheetDetail = xlsheets(2)

            '公司名稱
            If TransPara.TransP("UnitTitle") <> "" Then
                xlRange = xlsheet.Range("A1")
                xlRange.Value = TransPara.TransP("UnitTitle")
                NAR(xlRange)
            End If
            '年度
            xlRange = xlsheet.Range("A3")
            xlRange.Value = "中華民國 " & SYear & " 年度"
            NAR(xlRange)

            i = 5    '自第6行開始放
            With myds.Tables("acm010")
                '第一列是合計資料
                If .Rows(0).Item("accno") = "5" Then
                    TOT1 = nz(.Rows(0).Item("amt5"), 0)  '本年度實支出總額
                    TOT2 = nz(.Rows(0).Item("amt6"), 0)  '上年度權責支出總額
                    TOT3 = nz(.Rows(0).Item("amt7"), 0)  '本年度支出預算總額
                    TOT4 = nz(.Rows(0).Item("amt3"), 0)
                    TOT5 = nz(.Rows(0).Item("amt4"), 0)
                End If
                For intR = 1 To .Rows.Count - 1

                    strAccno = nz(.Rows(intR).Item("accno"), "")
                    strAccname = nz(.Rows(intR).Item("accname"), "")
                    decAmt3 = nz(.Rows(intR).Item("amt3"), 0)
                    decAmt4 = nz(.Rows(intR).Item("amt4"), 0)
                    decAmt5 = nz(.Rows(intR).Item("amt5"), 0)
                    decAmt6 = nz(.Rows(intR).Item("amt6"), 0)
                    decAmt7 = nz(.Rows(intR).Item("amt7"), 0)

                    '每次遇到五碼時,代表下面一定有七碼和九碼的明細
                    If Len(strAccno) = 5 Then
                        '新增工作表,用來輸出明細
                        xlSheetBase.Copy(, xlSheetDetail)
                        Dim ind As Integer
                        ind = xlSheetDetail.Index + 1
                        NAR(xlSheetDetail) '此行為必要,否則excel.exe不會自動關閉
                        xlSheetDetail = xlsheets(ind)
                        xlSheetDetail.Name = strAccname
                        '公司名稱
                        If TransPara.TransP("UnitTitle") <> "" Then
                            xlRange = xlSheetDetail.Range("A1")
                            xlRange.Value = TransPara.TransP("UnitTitle")
                            NAR(xlRange)
                        End If
                        '標題
                        xlRange = xlSheetDetail.Range("A2")
                        xlRange.Value = strAccname & "明細表"
                        NAR(xlRange)
                        '年度
                        xlRange = xlSheetDetail.Range("A3")
                        xlRange.Value = "中華民國 " & SYear & " 年度"
                        NAR(xlRange)
                        '科目編號 (文字方塊)
                        xlShapes = xlSheetDetail.Shapes '= "科目編號：" & FormatAccno(strAccno)
                        xlShape = xlShapes.Item(1)
                        xlShape.Select()
                        xlSelection = xlapp.Selection
                        xlChars = xlSelection.Characters
                        xlChars.Text = "科目編號：" & FormatAccno(strAccno) & vbNewLine & "承辦單位："
                        NAR(xlSelection)
                        NAR(xlChars)
                        NAR(xlShape)
                        NAR(xlShapes)
                        '此五碼會計科目的總額
                        dTOT1 = decAmt5
                        dTOT2 = decAmt6
                        dTOT3 = decAmt7
                        dTOT4 = decAmt3
                        dTOT5 = decAmt4
                        ii = 5
                    End If

                    '輸出明細表
                    If Len(strAccno) = 7 Or Len(strAccno) = 9 Then
                        ii += 1
                        '拷貝目前這列到下一列,使得每列都有相同的格式設定
                        xlRange1 = xlSheetDetail.Range("A" & ii & ":I" & ii)
                        xlRange2 = xlSheetDetail.Range("A" & ii + 1 & ":I" & ii + 1)
                        xlRange1.Copy(xlRange2)
                        NAR(xlRange1)
                        NAR(xlRange2)
                        '計算縮排所需空格數
                        Select Case Len(strAccno)
                            Case 7
                                spaces = 0
                            Case 9
                                spaces = 4
                            Case Else
                                spaces = 0
                        End Select
                        '開始填入每列的資料
                        xlRange = xlSheetDetail.Range("A" & ii)
                        xlRange.Value = Space(spaces) & FormatAccno(strAccno) & vbCrLf & Space(spaces) & strAccname
                        NAR(xlRange)
                        xlRange = xlSheetDetail.Range("B" & ii)
                        xlRange.Value = FormatNumber(decAmt5, 2)
                        NAR(xlRange)
                        xlRange = xlSheetDetail.Range("C" & ii)
                        xlRange.Value = FormatNumber(decAmt6, 2)
                        NAR(xlRange)
                        xlRange = xlSheetDetail.Range("D" & ii)
                        xlRange.Value = FormatNumber(decAmt5 + decAmt6, 2)
                        NAR(xlRange)
                        xlRange = xlSheetDetail.Range("E" & ii)
                        xlRange.Value = FormatNumber(decAmt3, 0)
                        NAR(xlRange)
                        xlRange = xlSheetDetail.Range("F" & ii)
                        xlRange.Value = FormatNumber(decAmt4, 0)
                        NAR(xlRange)
                        xlRange = xlSheetDetail.Range("G" & ii)
                        xlRange.Value = FormatNumber(decAmt7, 0)
                        NAR(xlRange)
                        xlRange = xlSheetDetail.Range("H" & ii)
                        xlRange1 = xlSheetDetail.Range("D" & ii)
                        xlRange2 = xlSheetDetail.Range("G" & ii)
                        xlRange.Value = FormatNumber(xlRange1.Value - xlRange2.Value, 2)
                        NAR(xlRange)
                        NAR(xlRange1)
                        NAR(xlRange2)

                        '判斷此科目是否已經讀取到尾端
                        Dim isEnd As Boolean
                        isEnd = False
                        If intR = .Rows.Count - 1 Then
                            isEnd = True
                        Else
                            If Len(nz(.Rows(intR + 1).Item("accno"), "")) < 7 Then
                                isEnd = True
                            End If
                        End If
                        '輸出明細表的合計
                        If isEnd Then
                            ii += 1
                            xlRange = xlSheetDetail.Range("A" & ii)
                            xlRange.Value = vbCrLf & "    合    計"
                            NAR(xlRange)
                            xlRange = xlSheetDetail.Range("B" & ii)
                            xlRange.Value = FormatNumber(dTOT1, 2)
                            NAR(xlRange)
                            xlRange = xlSheetDetail.Range("C" & ii)
                            xlRange.Value = FormatNumber(dTOT2, 2)
                            NAR(xlRange)
                            xlRange = xlSheetDetail.Range("D" & ii)
                            xlRange.Value = FormatNumber(dTOT1 + dTOT2, 2)
                            NAR(xlRange)
                            xlRange = xlSheetDetail.Range("E" & ii)
                            xlRange.Value = FormatNumber(dTOT4, 0)
                            NAR(xlRange)
                            xlRange = xlSheetDetail.Range("F" & ii)
                            xlRange.Value = FormatNumber(dTOT5, 0)
                            NAR(xlRange)
                            xlRange = xlSheetDetail.Range("G" & ii)
                            xlRange.Value = FormatNumber(dTOT3, 0)
                            NAR(xlRange)
                            xlRange = xlSheetDetail.Range("H" & ii)
                            xlRange1 = xlSheetDetail.Range("D" & ii)
                            xlRange2 = xlSheetDetail.Range("G" & ii)
                            xlRange.Value = FormatNumber(xlRange1.Value - xlRange2.Value, 2)
                            NAR(xlRange)
                            NAR(xlRange1)
                            NAR(xlRange2)
                        End If
                    End If

                    '輸出總表
                    If Len(strAccno) <> 7 And Len(strAccno) <> 9 Then
                        i += 1
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
                        '開始填入每列的資料
                        xlRange = xlsheet.Range("A" & i)
                        xlRange.Value = Space(spaces) & FormatAccno(strAccno) & vbCrLf & Space(spaces) & strAccname
                        NAR(xlRange)
                        xlRange = xlsheet.Range("B" & i)
                        xlRange.Value = FormatNumber(decAmt5, 2)
                        NAR(xlRange)
                        xlRange = xlsheet.Range("C" & i)
                        xlRange.Value = FormatNumber(decAmt6, 2)
                        NAR(xlRange)
                        xlRange = xlsheet.Range("D" & i)
                        xlRange.Value = FormatNumber(decAmt5 + decAmt6, 2)
                        NAR(xlRange)
                        xlRange = xlsheet.Range("E" & i)
                        xlRange.Value = FormatNumber(rate(decAmt5 + decAmt6, TOT1 + TOT2, 2), 2)
                        NAR(xlRange)
                        xlRange = xlsheet.Range("F" & i)
                        xlRange.Value = FormatNumber(decAmt3, 0)
                        NAR(xlRange)
                        xlRange = xlsheet.Range("G" & i)
                        xlRange.Value = FormatNumber(decAmt4, 0)
                        NAR(xlRange)
                        xlRange = xlsheet.Range("H" & i)
                        xlRange.Value = FormatNumber(decAmt7, 0)
                        NAR(xlRange)
                        xlRange = xlsheet.Range("I" & i)
                        xlRange.Value = FormatNumber(rate(decAmt7, TOT3, 2), 2)
                        NAR(xlRange)
                        xlRange = xlsheet.Range("J" & i)
                        xlRange1 = xlsheet.Range("D" & i)
                        xlRange2 = xlsheet.Range("H" & i)
                        xlRange.Value = FormatNumber(xlRange1.Value - xlRange2.Value, 2)
                        NAR(xlRange)
                        NAR(xlRange1)
                        NAR(xlRange2)
                        ''xlsheet.Cells(i, 9).value = FormatNumber(rate(xlsheet.Cells(i, 8).value, xlsheet.Cells(i, 6).value, 2), 0)
                    End If
                Next
            End With

            '總表的合計
            i += 1
            xlRange = xlsheet.Range("A" & i)
            xlRange.Value = vbCrLf & "    合    計"
            NAR(xlRange)
            xlRange = xlsheet.Range("B" & i)
            xlRange.Value = FormatNumber(TOT1, 2)
            NAR(xlRange)
            xlRange = xlsheet.Range("C" & i)
            xlRange.Value = FormatNumber(TOT2, 2)
            NAR(xlRange)
            xlRange = xlsheet.Range("D" & i)
            xlRange.Value = FormatNumber(TOT1 + TOT2, 2)
            NAR(xlRange)
            xlRange = xlsheet.Range("E" & i)
            xlRange.Value = FormatNumber(rate(TOT1 + TOT2, TOT1 + TOT2, 0), 0)
            NAR(xlRange)
            xlRange = xlsheet.Range("F" & i)
            xlRange.Value = FormatNumber(TOT4, 0)
            NAR(xlRange)
            xlRange = xlsheet.Range("G" & i)
            xlRange.Value = FormatNumber(TOT5, 0)
            NAR(xlRange)
            xlRange = xlsheet.Range("H" & i)
            xlRange.Value = FormatNumber(TOT3, 0)
            NAR(xlRange)
            xlRange = xlsheet.Range("I" & i)
            xlRange.Value = FormatNumber(rate(TOT3, TOT3, 0), 0)
            NAR(xlRange)
            xlRange = xlsheet.Range("J" & i)
            xlRange1 = xlsheet.Range("D" & i)
            xlRange2 = xlsheet.Range("H" & i)
            xlRange.Value = FormatNumber(xlRange1.Value - xlRange2.Value, 2)
            NAR(xlRange)
            NAR(xlRange1)
            NAR(xlRange2)
            ''xlsheet.Cells(i, 9).value = FormatNumber(rate(xlsheet.Cells(i, 8).value, xlsheet.Cells(i, 6).value, 2), 0)

            '恢復焦點到第一張工作表的第一個儲存格
            xlsheet.Activate()
            xlRange = xlsheet.Range("A1")
            xlRange.Activate()
            NAR(xlRange)
            xlSheetBase.Delete() '刪除樣本工作表

            '儲存檔案
            xlbook.Save()
            If rdoPrint.Checked Then
                'xlapp.Visible = True
                'xlbook.PrintPreview() '不可用預覽列印,否則當使用者關閉excel視窗之後就會導致finally區段關閉excel.exe時出現error
                xlbook.PrintOut()  '直接列印
            End If

            If MessageBox.Show("列印完畢,已存入 " & tt2 & vbNewLine & "是否要幫您開啟此份報表?", "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = MsgBoxResult.Yes Then
                '不可用 Process.Start(tt2) 否則會造成用同一個excel process開啟報表,導致finally區段關閉excel.exe時會出現error
                Process.Start("excel.exe", tt2)
            End If

            Me.Close()

        Finally
            '釋放各物件所佔用的記憶體,要按照以下順序
            NAR(xlRange)
            NAR(xlRange1)
            NAR(xlRange2)
            '如果有宣告 xlRange3 在這也要記得釋放記憶體
            NAR(xlSelection)
            NAR(xlChars)
            NAR(xlShape)
            NAR(xlShapes)
            NAR(xlsheet)
            NAR(xlSheetDetail)
            NAR(xlSheetBase)
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
