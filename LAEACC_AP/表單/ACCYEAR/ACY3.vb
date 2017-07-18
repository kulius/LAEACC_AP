Imports JBC.Printing
Imports System.IO
Imports Microsoft.Office.Interop
Public Class ACY3
    Dim confid, confunit As String, LoadAfter, Dirty As Boolean
    Dim SYear As Integer
    Dim myds As DataSet
    Dim act32301, bg32301 As Decimal

    Private Sub ACY3_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If TransPara.TransP("LastDay") <> String.Empty Then
            dtpDateS.Value = TransPara.TransP("LastDay")
        End If
    End Sub

    Private Sub LoadGridFunc()
        Dim sqlstr, retstr As String
        '計算本期損益
        sqlstr = "select sum(deamt12) as deamt, sum(cramt12) as cramt from acf050 where accyear=" & SYear & _
                 " and len(accno)=5 and left(accno,1)>='4' "
        myds = openmember("", "acm010", sqlstr)
        act32301 = nz(myds.Tables(0).Rows(0).Item("cramt"), 0) - nz(myds.Tables(0).Rows(0).Item("deamt"), 0)
        sqlstr = "select left(accno,1) as accno, sum(bg1+bg2+bg3+bg4+bg5) as bg from accbg where accyear=" & SYear & _
                 " and len(accno)=5 and left(accno,1)>='4' group by left(accno,1)"
        myds = openmember("", "acm010", sqlstr)
        bg32301 = 0
        For intI As Integer = 0 To myds.Tables(0).Rows.Count - 1
            With myds.Tables(0).Rows(intI)
                If .Item("accno") = "4" Then
                    bg32301 = nz(.Item("bg"), 0)
                Else
                    bg32301 = bg32301 - nz(.Item("bg"), 0)
                End If
            End With
        Next

        '丟入dataset 
        sqlstr = "select a.*, b.accname as accname,c.deamt,c.cramt from (select * from acf050 where accyear=" & _
                  SYear & " and left(accno,1)='3' and len(accno)=5 ) a " & _
                 " left outer join accname b on a.accno=b.accno " & _
                 " left outer join accbg c on a.accno=c.accno and a.accyear=c.accyear " & _
                 " order by a.accno"
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
                MsgBox("無此資料")
                Exit Sub
            End If

            Dim intR As Integer = 0  'control record number
            Dim intBank As Integer = 0 'control bank detail row number 
            Dim i As Integer = 0   'control excel row number
            Dim strAccno, strAccname As String
            Dim spaces As Integer
            Dim TOT2, TOT3, TOT4, TOT6, TOT7 As Decimal
            TOT2 = 0 : TOT3 = 0 : TOT4 = 0 : TOT6 = 0 : TOT7 = 0
            Dim Amt2, Amt3, Amt4, Amt6, Amt7 As Decimal
            Amt2 = 0 : Amt3 = 0 : Amt4 = 0 : Amt6 = 0 : Amt7 = 0

            Dim tt1, tt2 As String
            Try
                tt1 = "c:\App\acc\報表樣本\ACY3.xls"
                tt2 = "c:\App\acc\報表\ACY3.xls"
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
                MsgBox("報表copy from " & tt1 & "  to  " & tt2 & " 錯誤，若 \報表\ACY3.XLS 使用中請先關閉之，否則請洽資訊人員!")
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


            i = 5    '自第6行開始放
            With myds.Tables("acm010")
                For intR = 0 To .Rows.Count - 1
                    strAccno = nz(.Rows(intR).Item("accno"), "")
                    strAccname = nz(.Rows(intR).Item("accname"), "")
                    i += 1
                    '拷貝目前這列到下一列,使得每列都有相同的格式設定
                    xlRange1 = xlsheet.Range("A" & i & ":I" & i)
                    xlRange2 = xlsheet.Range("A" & i + 1 & ":I" & i + 1)
                    xlRange1.Copy(xlRange2)

                    NAR(xlRange1)
                    NAR(xlRange2)
                    Amt2 = .Rows(intR).Item("beg_credit") - .Rows(intR).Item("beg_debit")
                    Amt3 = .Rows(intR).Item("cramt12") - .Rows(intR).Item("beg_credit")
                    Amt4 = nz(.Rows(intR).Item("cramt"), 0)
                    Amt6 = .Rows(intR).Item("deamt12") - .Rows(intR).Item("beg_debit")
                    Amt7 = nz(.Rows(intR).Item("deamt"), 0)
                    'Amt9 = .Rows(intR).Item("cramt12") - .Rows(intR).Item("deamt12")
                    If strAccno = "32301" Then
                        If act32301 > 0 Then
                            Amt3 += act32301  '決算增加
                        Else
                            Amt6 += -act32301  '決算減少
                        End If
                        If bg32301 > 0 Then
                            Amt4 += bg32301  '預算增加
                        Else
                            Amt7 += -bg32301  '預算減少
                        End If
                    End If

                    '開始填入每列的資料
                    xlRange = xlCells(i, 1)
                    xlRange.Value = FormatAccno(strAccno) & vbCrLf & strAccname
                    NAR(xlRange)
                    xlRange = xlCells(i, 2)  '上年度
                    xlRange.Value = FormatNumber(Amt2, 2)
                    NAR(xlRange)
                    xlRange = xlCells(i, 3)   '決算增加
                    xlRange.Value = FormatNumber(Amt3, 2)
                    NAR(xlRange)
                    xlRange = xlCells(i, 4)   '預算增加
                    xlRange.Value = FormatNumber(Amt4, 2)
                    NAR(xlRange)
                    xlRange = xlCells(i, 5)   '比較增減
                    xlRange.Value = FormatNumber(Amt3 - Amt4, 2)
                    NAR(xlRange)
                    xlRange = xlCells(i, 6)   '決算減少
                    xlRange.Value = FormatNumber(Amt6, 2)
                    NAR(xlRange)
                    xlRange = xlCells(i, 7)    ''預算減少
                    xlRange.Value = FormatNumber(Amt7, 2)
                    NAR(xlRange)
                    xlRange = xlCells(i, 8)   '比較增減
                    xlRange.Value = FormatNumber(Amt6 - Amt7, 2)
                    NAR(xlRange)
                    xlRange = xlCells(i, 9)    ''決算餘額
                    xlRange.Value = FormatNumber(Amt2 + Amt3 - Amt6, 2)
                    NAR(xlRange)

                    'print detail account 
                    If Len(strAccno) = 5 Then '由四級作合計
                        TOT2 += Amt2
                        TOT3 += Amt3
                        TOT4 += Amt4
                        TOT6 += Amt6
                        TOT7 += Amt7
                        'TOT9 += Amt9
                    End If
                Next
            End With

            '合計
            i += 1
            xlRange = xlCells(i, 1)
            xlRange.Value = "    合    計"
            NAR(xlRange)
            xlRange = xlCells(i, 2)
            xlRange.Value = FormatNumber(TOT2, 2)
            NAR(xlRange)
            xlRange = xlCells(i, 3)
            xlRange.Value = FormatNumber(TOT3, 2)
            NAR(xlRange)
            xlRange = xlCells(i, 4)
            xlRange.Value = FormatNumber(TOT4, 2)
            NAR(xlRange)
            xlRange = xlCells(i, 5)
            xlRange.Value = FormatNumber(TOT3 - TOT4, 2)
            NAR(xlRange)
            xlRange = xlCells(i, 6)
            xlRange.Value = FormatNumber(TOT6, 2)
            NAR(xlRange)
            xlRange = xlCells(i, 7)
            xlRange.Value = FormatNumber(TOT7, 2)
            NAR(xlRange)
            xlRange = xlCells(i, 8)
            xlRange.Value = FormatNumber(TOT6 - TOT7, 2)
            NAR(xlRange)
            xlRange = xlCells(i, 9)
            xlRange.Value = FormatNumber(TOT2 + TOT3 - TOT6, 2)
            NAR(xlRange)          '儲存檔案
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
