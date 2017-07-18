Imports JBC.Printing
Imports System.IO
Imports Microsoft.Office.Interop
Public Class ACY111
    Dim confid, confunit As String, LoadAfter, Dirty As Boolean
    Dim SYear As Integer
    Dim myds, mydsBank As DataSet

    Private Sub ACY111_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If TransPara.TransP("LastDay") <> String.Empty Then
            dtpDateS.Value = TransPara.TransP("LastDay")
        End If
    End Sub

    Private Sub LoadGridFunc()
        Dim sqlstr, retstr As String
        '丟入dataset 
        sqlstr = "select a.*, b.accname as accname from (select * from acf050 where accyear=" & _
                  SYear & " and left(accno,3)='111' and len(accno)=5) a " & _
                 " left outer join accname b on a.accno=b.accno " & _
                 "where (a.deamt12 <> a.cramt12) order by a.accno"
        myds = openmember("", "acm010", sqlstr)
        '找年底各帳戶結存
        sqlstr = "SELECT b.*, c.BANKNAME AS bankname, c.ACCNO AS accno, c.remark as remark from " & _
                 "(SELECT MAX(date_2) AS date_2, bank FROM CHF030 WHERE DATE_2<='" & FullDate(dtpDateS.Value) & "' GROUP BY BANK) a " & _
                 "INNER JOIN CHF030 b ON a.bank = b.BANK AND a.date_2 = b.DATE_2 " & _
                 "left outer join CHF020 c ON a.bank = c.BANK " & _
                 " ORDER BY c.ACCNO, b.bank"
        mydsBank = openmember("", "chf030", sqlstr)
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
            'Dim spaces As Integer
            Dim TOT As Decimal = 0

            Dim tt1, tt2 As String
            Try
                tt1 = "c:\App\acc\報表樣本\ACY111.xls"
                tt2 = "c:\App\acc\報表\ACY111.xls"
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
                MsgBox("報表copy from " & tt1 & "  to  " & tt2 & " 錯誤，若 \報表\ACY111.XLS 使用中請先關閉之，否則請洽資訊人員!")
                'Dim log As New LogInfo(LogType.Exception, "拷貝報表時發生錯誤" & vbNewLine & ex.ToString)
                'gLM.Log(log)
                Exit Sub
            End Try

            xlsheets = xlbook.Worksheets
            xlsheet = xlsheets(1)     '總表
            NAR(xlCells)
            xlCells = xlsheet.Cells
            'xlSheetBase = xlsheets(2) '收入明細表的樣本
            'xlSheetDetail = xlsheets(2)

            '公司名稱
            If TransPara.TransP("UnitTitle") <> "" Then
                xlRange = xlsheet.Range("A1")
                xlRange.Value = TransPara.TransP("UnitTitle")
                NAR(xlRange)
            End If
            '年度
            xlRange = xlsheet.Range("A3")
            xlRange.Value = "中華民國 " & SYear & " 年  12  月 31  日"
            NAR(xlRange)


            i = 5    '自第6行開始放
            With myds.Tables("acm010")
                For intR = 0 To .Rows.Count - 1
                    strAccno = nz(.Rows(intR).Item("accno"), "")
                    strAccname = nz(.Rows(intR).Item("accname"), "")
                    i += 1
                    '拷貝目前這列到下一列,使得每列都有相同的格式設定
                    xlRange1 = xlsheet.Range("A" & i & ":E" & i)
                    xlRange2 = xlsheet.Range("A" & i + 1 & ":E" & i + 1)
                    xlRange1.Copy(xlRange2)

                    NAR(xlRange1)
                    NAR(xlRange2)
                    '開始填入每列的資料

                    xlRange = xlCells(i, 1)
                    xlRange.Value = FormatAccno(strAccno) & vbCrLf & strAccname
                    NAR(xlRange)
                    xlRange = xlCells(i, 4)
                    xlRange.Value = FormatNumber(.Rows(intR).Item("deAmt12") - .Rows(intR).Item("crAmt12"), 2)
                    NAR(xlRange)

                    'print detail account 
                    If Len(strAccno) = 5 Then
                        TOT += .Rows(intR).Item("deAmt12") - .Rows(intR).Item("crAmt12") '由四級作合計
                        For intBank = 0 To mydsBank.Tables("chf030").Rows.Count - 1
                            If nz(mydsBank.Tables("chf030").Rows(intBank).Item("accno"), "") = strAccno And _
                               mydsBank.Tables("chf030").Rows(intBank).Item("balance") <> 0 Then
                                i += 1
                                '拷貝目前這列到下一列,使得每列都有相同的格式設定
                                xlRange1 = xlsheet.Range("A" & i & ":E" & i)
                                xlRange2 = xlsheet.Range("A" & i + 1 & ":E" & i + 1)
                                xlRange1.Copy(xlRange2)
                                NAR(xlRange1)
                                NAR(xlRange2)
                                xlRange = xlCells(i, 1)
                                xlRange.Value = FormatAccno(strAccno) & vbCrLf & strAccname
                                NAR(xlRange)
                                xlRange = xlCells(i, 2)
                                xlRange.Value = mydsBank.Tables("chf030").Rows(intBank).Item("bankname")
                                NAR(xlRange)
                                xlRange = xlCells(i, 3)
                                xlRange.Value = FormatNumber(mydsBank.Tables("chf030").Rows(intBank).Item("balance"), 2)
                                NAR(xlRange)
                                xlRange = xlCells(i, 5)
                                xlRange.Value = nz(mydsBank.Tables("chf030").Rows(intBank).Item("remark"), "")
                                NAR(xlRange)
                            End If
                        Next
                    End If
                Next
            End With

            '合計
            i += 1
            '拷貝目前這列到下一列,使得每列都有相同的格式設定
            'xlRange1 = xlsheet.Range("A" & i & ":E" & i)
            'xlRange2 = xlsheet.Range("A" & i + 1 & ":E" & i + 1)
            'xlRange1.Copy(xlRange2)
            'NAR(xlRange1)
            'NAR(xlRange2)
            xlRange = xlCells(i, 1)
            xlRange.Value = "    合    計"
            NAR(xlRange)
            xlRange = xlCells(i, 4)
            xlRange.Value = FormatNumber(TOT, 2)
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
            NAR(xlCells)
            NAR(xlRange)
            NAR(xlRange1)
            NAR(xlRange2)
            '如果有宣告 xlRange3 在這也要記得釋放記憶體
            ' NAR(xlSelection)
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
