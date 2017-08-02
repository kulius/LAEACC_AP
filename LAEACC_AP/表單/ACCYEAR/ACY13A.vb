Imports JBC.Printing
Imports System.IO
Imports Microsoft.Office.Interop
Public Class ACY13A
    Dim confid, confunit As String, LoadAfter, Dirty As Boolean
    Dim SYear As Integer
    Dim myds, myds2 As DataSet

    Private Sub ACY13A_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If TransPara.TransP("LastDay") <> String.Empty Then
            dtpDateS.Value = TransPara.TransP("LastDay")
        End If
    End Sub

    Private Sub LoadGridFunc()
        Dim sqlstr, retstr As String
        Dim intR As Integer = 0
        Dim intAmt As Decimal = 0
        Dim strAccno, strOther As String
        'clear data 
        sqlstr = "delete from acy13a"
        retstr = runsql("", sqlstr)
        'create data 
        sqlstr = "select * from acf020 where accyear=" & SYear & " and left(accno,2)='13' " & _
         " and substring(accno,4,2)<>'02' and left(accno,5)<>'13701' " & _
         " and not (remark like '%減損%' or remark like '%報廢%') order by accno "
        myds = openmember("", "acm010", sqlstr)
        For intR = 0 To myds.Tables(0).Rows.Count - 1  '逐筆處理
            With myds.Tables(0).Rows(intR)
                If Mid(.Item("accno"), 1, 5) = "13101" And .Item("dc") = "2" Then '已有固定資產變賣表示
                    GoTo EXITFOR
                End If
                If .Item("DC") = "2" Then    '貸方
                    intAmt = -nz(.Item("amt"), 0)
                Else
                    intAmt = nz(.Item("amt"), 0)
                End If
                strAccno = Mid(nz(.Item("accno"), ""), 1, 7)
                strOther = nz(.Item("other_accno"), "")
                sqlstr = "select * from acy13a where accno='" & strAccno & _
                         "' and other_accno='" & strOther & "'"
                myds2 = openmember("", "acy13a", sqlstr)
                If myds2.Tables(0).Rows.Count > 0 And strOther <> "" Then '資料已存在並且有相關科目(金額加總)
                    sqlstr = "update acy13a set amt2=amt2+" & intAmt & " where accno='" & _
                         strAccno & "' and other_accno='" & strOther & "'"
                    retstr = runsql("", sqlstr)
                Else  '資料不存在或是無相關科目必須逐筆增入acy13a
                    'GenInsSql("accyear", SYear, "N")
                    GenInsSql("accno", strAccno, "T")
                    GenInsSql("other_accno", strOther, "T")
                    GenInsSql("remark", nz(.Item("remark"), ""), "T")
                    GenInsSql("qty", nz(.Item("mat_qty"), ""), "N")
                    GenInsSql("amt2", intAmt, "N")
                    GenInsSql("dc", nz(.Item("dc"), ""), "T")
                    sqlstr = "insert into ACY13A " & GenInsFunc
                    retstr = runsql(mastconn, sqlstr)
                End If
            End With
EXITFOR:
        Next

        '再補未完工程尚未完工
        sqlstr = "SELECT a.*, b.accname as accname FROM (select * from ACF050 WHERE ACCYEAR=" & SYear & " and left(accno,5)='13701' and " & _
                 "len(accno)>9 and len(accno)<=16 and deamt12<>cramt12 ) a left outer join accname b on a.accno=b.accno "
        myds = openmember("", "acf050", sqlstr)
        For intR = 0 To myds.Tables(0).Rows.Count - 1
            With myds.Tables(0).Rows(intR)
                strOther = nz(.Item("accno"), "")
                sqlstr = "select * from acy13a where other_accno='" & strOther & "'"
                myds2 = openmember("", "acy13a", sqlstr)
                If myds2.Tables(0).Rows.Count > 0 Then '資料已存在
                    sqlstr = "update acy13a set amt2=amt2+" & (.Item("deamt12") - .Item("cramt12")) & _
                             " where other_accno='" & strOther & "'"
                    retstr = runsql("", sqlstr)
                Else  '資料不存在
                    If .Item("accname").indexof("排水") > 0 Then
                        strAccno = "1320102"   '排水工程
                    Else
                        If .Item("accname").indexof("新建") > 0 Then
                            strAccno = "1330101"  '房屋建築工程
                        Else
                            strAccno = "1320101"  '灌溉工程
                        End If
                    End If
                    GenInsSql("accno", strAccno, "T")
                    GenInsSql("other_accno", strOther, "T")
                    GenInsSql("amt2", .Item("deamt12") - .Item("cramt12"), "N")
                    sqlstr = "insert into ACY13A " & GenInsFunc
                    retstr = runsql(mastconn, sqlstr)
                End If
            End With
        Next

        '找上年度決算數
        sqlstr = "update acy13a set amt1=b.beg_debit-b.beg_credit from acf050 b where b.accyear=" & SYear & _
                 " and b.accno=acy13a.other_accno and acy13A.other_accno<>'' "
        retstr = runsql("", sqlstr)
        sqlstr = "update acy13a set amt2=amt2-amt1 where amt1>0"  '決算數要分上年度及本年度
        retstr = runsql("", sqlstr)

        '找本年度預算數
        sqlstr = "SELECT a.accno, a.bg1+a.bg2+a.bg3+a.bg4+a.bg5+a.up1+a.up2+a.up3+a.up4+a.up4 as bg, " & _
                 " b.accname as accname FROM (select * from accbg where accyear=" & SYear & " and left(accno,5)='13701' and " & _
                 "len(accno)>9 and len(accno)<=16 ) a left outer join accname b on a.accno=b.accno "
        myds = openmember("", "accbg", sqlstr)
        For intR = 0 To myds.Tables(0).Rows.Count - 1
            With myds.Tables(0).Rows(intR)
                sqlstr = "select * from acy13a where other_accno='" & nz(.Item("accno"), "") & "'"
                myds2 = openmember("", "acy13a", sqlstr)

                If myds2.Tables(0).Rows.Count > 0 Then '資料已存在
                    sqlstr = "update acy13a set amt5=" & nz(.Item("bg"), 0) & " where other_accno='" & nz(.Item("accno"), "") & "'"
                    retstr = runsql("", sqlstr)
                Else  '資料不存在
                    If .Item("accname").indexof("排水") > 0 Then
                        strAccno = "1320102"
                    Else
                        If .Item("accname").indexof("新建") > 0 Then
                            strAccno = "1330101"
                        Else
                            strAccno = "1320101"
                        End If
                    End If
                    GenInsSql("accno", strAccno, "T")
                    GenInsSql("other_accno", nz(.Item("accno"), ""), "T")
                    GenInsSql("amt5", nz(.Item("bg"), 0), "N")
                    sqlstr = "insert into ACY13A " & GenInsFunc
                    retstr = runsql(mastconn, sqlstr)
                End If
            End With
        Next

        '找上年度預算數
        sqlstr = "SELECT * from acy13a where amt1<>0 "
        myds = openmember("", "acy13a", sqlstr)
        For intR = 0 To myds.Tables(0).Rows.Count - 1
            With myds.Tables(0).Rows(intR)
                strOther = nz(.Item("other_accno"), "")
                sqlstr = "select sum(bg1+bg2+bg3+bg4+bg5+up1+up2+up3+up4+up4) as bg " & _
                         "from accbg where accyear<" & SYear & " and accno='" & strOther & _
                         "' group by accno"
                myds2 = openmember("", "accbg", sqlstr)
                If myds2.Tables(0).Rows.Count > 0 Then
                    If nz(myds2.Tables(0).Rows(0).Item(0), 0) > 0 Then
                        sqlstr = "update acy13a set amt4=" & nz(myds2.Tables(0).Rows(0).Item(0), 0) & _
                                 " where other_accno='" & strOther & "'"
                        retstr = runsql("", sqlstr)
                    End If
                End If
            End With
        Next
        '統計四級科目
        sqlstr = "INSERT INTO acy13a  (accno, other_accno, amt1, amt2, amt3, amt4, amt5) " & _
                 "(SELECT LEFT(ACCNO, 5) AS accno, '' as other_accno, SUM(AMT1) AS amt1, SUM(AMT2) AS amt2, " & _
                 "SUM(AMT3) AS amt3, SUM(AMT4) AS amt4, SUM(AMT5) AS amt5  FROM ACY13A GROUP BY LEFT(ACCNO, 5))"
        retstr = runsql("", sqlstr)

        '丟入dataset 
        sqlstr = "select a.*, b.accname as name1, c.accname as name2, d.unit " & _
                 "from (select * from acy13a ) a " & _
                 " left outer join accname b on a.accno=b.accno " & _
                 " left outer join accname c on a.other_accno=c.accno " & _
                 " left outer join acf100 d on a.accno=d.accno and d.accyear=" & SYear & _
                 " order by a.accno, a.other_accno"
        myds = openmember("", "acm010", sqlstr)
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
        Dim xlCells As Excel.Range

        Try
            SYear = GetYear(dtpDateS.Value)
            TransPara.TransP("LastDay") = dtpDateS.Value

            Call LoadGridFunc()

            If myds.Tables("acm010").Rows.Count = 0 Then
                MsgBox("無資料")
                Exit Sub
            End If

            Dim intR As Integer = 0  'control record number
            Dim intD As Integer = 0 'control ACF020 detail row number 
            Dim i As Integer = 0   'control excel row number
            Dim strAccno, strAccname, strRemark As String
            Dim spaces As Integer
            Dim TOT1, TOT2, TOT3, TOT4, TOT5 As Decimal
            TOT1 = 0 : TOT2 = 0 : TOT3 = 0 : TOT4 = 0 : TOT5 = 0
            Dim sqlstr As String

            Dim tt1, tt2 As String
            Try
                tt1 = "c:\App\acc\報表樣本\ACY13A.xls"
                tt2 = "c:\App\acc\報表\ACY13A.xls"
                If Not File.Exists(tt1) Then
                    AppReport_Copy("acc", "ACY13A.xls", tt1)
                    'MsgBox("找不到報表樣本，請洽資訊人員" & vbNewLine & tt1)
                    'Exit Sub
                End If
                FileCopy(tt1, tt2)    'copy tt1 to tt2
                xlapp = CreateObject("Excel.Application")
                xlapp.DisplayAlerts = False  '隱藏所有警告或確認訊息視窗,否則某些指令會失敗,譬如刪除工作表
                xlbooks = xlapp.Workbooks
                xlbook = xlbooks.Open(tt2)   '開啟tt2
            Catch ex As Exception
                MsgBox("報表copy from " & tt1 & "  to  " & tt2 & " 錯誤，若 \報表\ACY13A.XLS 使用中請先關閉之，否則請洽資訊人員!")
                'Dim log As New LogInfo(LogType.Exception, "拷貝報表時發生錯誤" & vbNewLine & ex.ToString)
                'gLM.Log(log)
                Exit Sub
            End Try

            xlsheets = xlbook.Worksheets
            xlsheet = xlsheets(1)     '總表
            NAR(xlCells)
            xlCells = xlsheet.Cells

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

            i = 6    '自第6行開始放
            With myds.Tables("acm010")
                For intR = 0 To .Rows.Count - 1
                    strAccno = nz(.Rows(intR).Item("accno"), "")
                    strAccname = Mid(strAccno, 9, 3) & nz(.Rows(intR).Item("name1"), "")
                    If nz(.Rows(intR).Item("name2"), "") <> "" Then  '資產名稱
                        strRemark = nz(.Rows(intR).Item("name2"), "")
                    Else
                        strRemark = nz(.Rows(intR).Item("remark"), "")
                    End If
                    i += 1
                    '拷貝目前這列到下一列,使得每列都有相同的格式設定
                    xlRange1 = xlsheet.Range("A" & i & ":L" & i)
                    xlRange2 = xlsheet.Range("A" & i + 1 & ":L" & i + 1)
                    xlRange1.Copy(xlRange2)

                    NAR(xlRange1)
                    NAR(xlRange2)

                    '開始填入每列的資料
                    Select Case Grade(strAccno)
                        Case 4
                            spaces = 0
                        Case 5
                            spaces = 2
                        Case 6
                            spaces = 4
                        Case Else
                            spaces = 6
                    End Select
                    xlRange = xlCells(i, 1)
                    xlRange.Value = Space(spaces) & FormatAccno(strAccno) & vbCrLf & Space(spaces) & strAccname
                    NAR(xlRange)
                    xlRange = xlCells(i, 2)
                    xlRange.Value = strRemark
                    NAR(xlRange)
                    xlRange = xlCells(i, 3)
                    xlRange.Value = nz(.Rows(intR).Item("unit"), "")
                    NAR(xlRange)
                    xlRange = xlCells(i, 4)
                    xlRange.Value = nz(.Rows(intR).Item("qty"), "")
                    NAR(xlRange)
                    xlRange = xlCells(i, 5)
                    xlRange.Value = FormatNumber(.Rows(intR).Item("amt1"), 0)
                    NAR(xlRange)
                    xlRange = xlCells(i, 6)
                    xlRange.Value = FormatNumber(.Rows(intR).Item("amt2"), 0)
                    NAR(xlRange)
                    xlRange = xlCells(i, 7)
                    xlRange.Value = FormatNumber(.Rows(intR).Item("amt3"), 0)
                    NAR(xlRange)
                    xlRange = xlCells(i, 9)
                    xlRange.Value = FormatNumber(.Rows(intR).Item("amt4"), 0)
                    NAR(xlRange)
                    xlRange = xlCells(i, 10)
                    xlRange.Value = FormatNumber(.Rows(intR).Item("amt5"), 0)
                    NAR(xlRange)
                    xlRange = xlCells(i, 13)
                    xlRange.Value = nz(.Rows(intR).Item("other_accno"), "")
                    NAR(xlRange)

                    If Len(strAccno) = 5 Then     '由四級作合計
                        TOT1 += nz(.Rows(intR).Item("amt1"), 0)
                        TOT2 += nz(.Rows(intR).Item("amt2"), 0)
                        TOT3 += nz(.Rows(intR).Item("amt3"), 0)
                        TOT4 += nz(.Rows(intR).Item("amt4"), 0)
                        TOT5 += nz(.Rows(intR).Item("amt5"), 0)
                    End If
                Next
            End With

            '合計
            i += 1
            xlRange = xlCells(i, 1)
            xlRange.Value = "    合           計"
            NAR(xlRange)
            xlRange = xlCells(i, 5)
            xlRange.Value = FormatNumber(TOT1, 0)
            NAR(xlRange)
            xlRange = xlCells(i, 6)
            xlRange.Value = FormatNumber(TOT2, 0)
            NAR(xlRange)
            xlRange = xlCells(i, 7)
            xlRange.Value = FormatNumber(TOT3, 0)
            NAR(xlRange)
            xlRange = xlCells(i, 9)
            xlRange.Value = FormatNumber(TOT4, 0)
            NAR(xlRange)
            xlRange = xlCells(i, 10)
            xlRange.Value = FormatNumber(TOT5, 0)
            NAR(xlRange)

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
            NAR(xlCells)
            NAR(xlRange)
            NAR(xlRange1)
            NAR(xlRange2)
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
