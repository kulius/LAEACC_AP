Imports JBC.Printing
Public Class BGP060
    Dim confid, confunit As String, LoadAfter, Dirty As Boolean
    Dim sqlstr As String
    Dim syear As Integer
    Dim intLoop As Integer = 0 '控制第幾次取資料
    Dim mydataset, bgdataset As DataSet
    Dim UserId, UserName, UserUnit As String
    'Dim xlCells As Excel.Range

    Private Sub BGP060_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        nudYear.Value = GetYear(TransPara.TransP("userdate"))
        nudMon.Value = Month(TransPara.TransP("userdate"))
    End Sub

    Sub loadData()
        '費用類
        If intLoop = 1 Then
            sqlstr = "SELECT c.*, d.ACCNAME AS accname from " & _
                     "(SELECT LEFT(a.ACCNO, 5) AS accno, LEFT(b.UNIT, 2) AS unit, " & _
                     "SUM(a.BG1 + a.BG2 + a.BG3 + a.BG4 + a.BG5+a.up1+a.up2+a.up3+a.up4+a.up5) AS bg, " & _
                     "sum(a.totper) as totper, sum(a.totuse) as totuse FROM  BGF010 a " & _
                     "LEFT OUTER JOIN ACCNAME b ON a.ACCNO = b.ACCNO " & _
                     "WHERE (a.ACCYEAR = " & syear & " and len(a.accno)>=5 and a.accno>='51102' and left(b.unit,2)>'00') " & _
                     "GROUP BY LEFT(a.ACCNO,5), LEFT(b.UNIT,2)) c LEFT OUTER JOIN " & _
                     "ACCNAME d ON c.accno = d.ACCNO " & _
                     "ORDER BY  c.accno, c.unit"
        End If
        '長期負債
        If intLoop = 2 Then
            sqlstr = "SELECT c.*, d.ACCNAME AS accname from " & _
                     "(SELECT LEFT(a.ACCNO, 5) AS accno, LEFT(b.UNIT, 2) AS unit, " & _
                     "SUM(a.BG1 + a.BG2 + a.BG3 + a.BG4 + a.BG5+a.up1+a.up2+a.up3+a.up4+a.up5) AS bg, " & _
                     "sum(a.totper) as totper, sum(a.totuse) as totuse FROM  BGF010 a " & _
                     "LEFT OUTER JOIN ACCNAME b ON a.ACCNO = b.ACCNO " & _
                     "WHERE (a.ACCYEAR = " & syear & " and len(a.accno)>=5 and left(a.accno,5)='22101' and left(b.unit,2)>'00') " & _
                     "GROUP BY LEFT(a.ACCNO,5), LEFT(b.UNIT,2)) c LEFT OUTER JOIN " & _
                     "ACCNAME d ON c.accno = d.ACCNO " & _
                     "ORDER BY  c.accno, c.unit"
        End If
        '固定資產
        If intLoop = 3 Then
            '先計算13701預算數  (因13701預算數不計入13內)
            sqlstr = "SELECT c.*, d.ACCNAME AS accname from " & _
                     "(SELECT LEFT(a.ACCNO, 5) AS accno, LEFT(b.UNIT, 2) AS unit, " & _
                     "SUM(a.BG1 + a.BG2 + a.BG3 + a.BG4 + a.BG5+a.up1+a.up2+a.up3+a.up4+a.up5) AS bg, " & _
                     "sum(a.totper) as totper, sum(a.totuse) as totuse FROM  BGF010 a " & _
                     "LEFT OUTER JOIN ACCNAME b ON a.ACCNO = b.ACCNO " & _
                     "WHERE (a.ACCYEAR = " & syear & " and len(a.accno)>=5 and left(a.accno,5)='13701' and left(b.unit,2)>'00') " & _
                     "GROUP BY LEFT(a.ACCNO,5), LEFT(b.UNIT,2)) c LEFT OUTER JOIN " & _
                     "ACCNAME d ON c.accno = d.ACCNO " & _
                     "ORDER BY  c.unit"
            bgdataset = openmember("", "BG13701", sqlstr)

            sqlstr = "SELECT c.*, d.ACCNAME AS accname from " & _
                     "(SELECT LEFT(a.ACCNO, 2) AS accno, LEFT(b.UNIT, 2) AS unit, " & _
                     "SUM(a.BG1 + a.BG2 + a.BG3 + a.BG4 + a.BG5+a.up1+a.up2+a.up3+a.up4+a.up5) AS bg, " & _
                     "sum(a.totper) as totper, sum(a.totuse) as totuse FROM  BGF010 a " & _
                     "LEFT OUTER JOIN ACCNAME b ON a.ACCNO = b.ACCNO " & _
                     "WHERE (a.ACCYEAR = " & syear & " and len(a.accno)>=5 and left(a.accno,2)='13' and left(b.unit,2)>'00') " & _
                     "GROUP BY LEFT(a.ACCNO,2), LEFT(b.UNIT,2)) c LEFT OUTER JOIN " & _
                     "ACCNAME d ON c.accno = d.ACCNO " & _
                     "ORDER BY  c.accno, c.unit"
        End If

        mydataset = openmember("", "BGF010", sqlstr)
    End Sub


    Private Sub BtnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrint.Click
        '    Dim xlapp As Excel.Application
        '    Dim xlbook As Excel.Workbook
        '    Dim xlbooks As Excel.Workbooks
        '    Dim xlsheet As Excel.Worksheet
        '    Dim xlsheets As Excel.Sheets        '  Dim xlRange As Excel.Range
        '    Dim xlRange As Excel.Range
        '    Dim xlRange1 As Excel.Range
        '    Dim xlRange2 As Excel.Range

        '    Dim intR As Integer = 0  'control record number
        '    Dim intCol, intRow As Integer   ' table column, row
        '    Dim strUnit As String
        '    Dim intUnit As Integer
        '    Dim intD, i, intJ As Integer
        '    Dim intBg As Decimal = 0
        '    Dim retstr, strAccno As String

        '    syear = nudYear.Value

        '    'sqlstr = "SELECT c.*, d.ACCNAME AS accname from " & _
        '    '         "(SELECT LEFT(a.ACCNO, 5) AS accno, LEFT(b.UNIT, 2) AS unit, " & _
        '    '         "SUM(a.BG1 + a.BG2 + a.BG3 + a.BG4 + a.BG5+a.up1+a.up2+a.up3+a.up4+a.up5) AS bg, " & _
        '    '         "sum(a.totper) as totper, sum(a.totuse) as totuse FROM  BGF010 a " & _
        '    '         "LEFT OUTER JOIN ACCNAME b ON a.ACCNO = b.ACCNO " & _
        '    '         "WHERE (a.ACCYEAR = " & syear & " and len(a.accno)>=5 and a.accno>='51102' and left(b.unit,2)>'00') " & _
        '    '         "GROUP BY LEFT(a.ACCNO,5), LEFT(b.UNIT,2)) c LEFT OUTER JOIN " & _
        '    '         "ACCNAME d ON c.accno = d.ACCNO " & _
        '    '         "ORDER BY  c.accno, c.unit"
        '    'mydataset =  openmember("", "BGF010", sqlstr)
        '    'If mydataset.Tables(0).Rows.Count <= 0 Then Exit Sub '無資料

        '    Try
        '        Dim tt1, tt2 As String
        '        Try
        '            tt1 = "c:\App\acc\報表樣本\預算執行報告表.xls"
        '            tt2 = "c:\App\acc\報表\預算執行報告表.xls"
        '            If Not File.Exists(tt1) Then
        '                MsgBox("找不到報表樣本" & tt1 & "，請洽資訊人員" & vbNewLine & tt1)
        '                Exit Sub
        '            End If
        '            FileCopy(tt1, tt2)    'copy tt1 to tt2
        '            xlapp = CreateObject("excel.application")
        '            xlbooks = xlapp.Workbooks
        '            xlbook = xlbooks.Open(tt2) '開啟tt2
        '        Catch ex As Exception
        '            MsgBox(ex.Message)
        '            'MsgBox("報表copy " & tt1 & "  to  " & tt2 & "錯誤，是否" & tt2 & " 使用中,請先關閉!")
        '            Exit Sub
        '        End Try
        '        xlsheets = xlbook.Worksheets
        '        xlsheet = xlsheets(1)
        '        NAR(xlCells)
        '        xlCells = xlsheet.Cells

        '        '公司名稱
        '        If TransPara.TransP("UnitTitle") <> "" Then
        '            xlRange = xlsheet.Range("A1")
        '            xlRange.Value = TransPara.TransP("UnitTitle")
        '            NAR(xlRange)
        '        End If
        '        xlRange = xlsheet.Range("A2")
        '        xlRange.Value = syear & "年度預算執行報告表  (1月至" & nudMon.Value & "月)"
        '        NAR(xlRange)

        '        intCol = 2   '自第3欄開始放
        '        strAccno = ""
        '        For intLoop = 1 To 3
        '            Call loadData()
        '            With mydataset.Tables("bgf010")
        '                For intR = 0 To .Rows.Count - 1
        '                    If intLoop = 1 And strAccno <> .Rows(intR).Item("accno") Then
        '                        intCol += 1
        '                        xlRange = xlCells(3, intCol)
        '                        xlRange.Value = .Rows(intR).Item("accname")
        '                        NAR(xlRange)
        '                        strAccno = .Rows(intR).Item("accno")
        '                    End If
        '                    If intLoop = 2 Then intCol = 14
        '                    If intLoop = 3 Then intCol = 15
        '                    '取得放置行
        '                    strUnit = .Rows(intR).Item("unit")
        '                    If strUnit <= "09" And strUnit >= "01" Then
        '                        intUnit = Val(strUnit)
        '                    Else
        '                        If strUnit = "0A" Then  '灌推中心
        '                            intUnit = 10
        '                        Else
        '                            intUnit = 11
        '                        End If
        '                    End If
        '                    i = (intUnit * 3) + 1
        '                    xlRange = xlCells(i, intCol)
        '                    intBg = nz(.Rows(intR).Item("bg"), 0)
        '                    If intLoop = 3 Then   '扣除預算數
        '                        For intJ = 0 To bgdataset.Tables("BG13701").Rows.Count - 1
        '                            If nz(bgdataset.Tables("BG13701").Rows(intJ).Item("unit"), "") = strUnit Then
        '                                intBg -= nz(bgdataset.Tables("BG13701").Rows(intJ).Item("bg"), 0)
        '                                Exit For
        '                            End If
        '                        Next
        '                    End If
        '                    xlRange.Value = FormatNumber(intBg, 0)
        '                    NAR(xlRange)
        '                    i += 1
        '                    xlRange = xlCells(i, intCol)
        '                    xlRange.Value = FormatNumber(nz(.Rows(intR).Item("totper"), 0), 0)
        '                    NAR(xlRange)
        '                    i += 1
        '                    xlRange = xlCells(i, intCol)
        '                    xlRange.Value = FormatNumber(nz(.Rows(intR).Item("totuse"), 0), 0)
        '                    NAR(xlRange)
        '                Next
        '            End With
        '        Next

        '        '儲存檔案
        '        xlbook.Save()
        '        If rdoPrint.Checked Then
        '            'xlapp.Visible = True
        '            'xlbook.PrintPreview() '不可用預覽列印,否則當使用者關閉excel視窗之後就會導致finally區段關閉excel.exe時出現error
        '            xlbook.PrintOut()  '直接列印
        '        End If

        '        If MessageBox.Show("列印完畢,已存入 " & tt2 & vbNewLine & "是否要幫您開啟此份報表?", "", MsgBoxStyle.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) = MsgBoxResult.Yes Then
        '            '不可用 Process.Start(tt2) 否則會造成用同一個excel process開啟報表,導致finally區段關閉excel.exe時會出現error
        '            Process.Start("excel.exe", tt2)
        '        End If

        '        Me.Close()

        '    Finally
        '        '釋放各物件所佔用的記憶體,要按照以下順序
        '        NAR(xlCells)
        '        NAR(xlRange)
        '        NAR(xlRange1)
        '        NAR(xlRange2)

        '        NAR(xlsheet)
        '        NAR(xlsheets)
        '        If Not xlbook Is Nothing Then xlbook.Close(False)
        '        NAR(xlbook)
        '        NAR(xlbooks)
        '        If Not xlapp Is Nothing Then xlapp.Quit()
        '        NAR(xlapp)
        '        GC.Collect()
        '    End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

End Class
