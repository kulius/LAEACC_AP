Imports JBC.Printing
Imports System.IO
Imports Microsoft.Office.Interop
Public Class ACY13E
    Dim confid, confunit As String, LoadAfter, Dirty As Boolean
    Dim SYear As Integer
    Dim myds As DataSet

    Private Sub ACY13E_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If TransPara.TransP("LastDay") <> String.Empty Then
            dtpDateS.Value = TransPara.TransP("LastDay")
        End If
    End Sub

    Private Sub LoadGridFunc()
        Dim sqlstr, retstr As String
        '丟入dataset 
        sqlstr = "select a.*, d.beg_debit as acubeg_debit, d.beg_credit as acubeg_credit, " & _
                 " d.deamt12 as acudeamt12, d.cramt12 as acucramt12 " & _
                 "from (select * from acf050 where accyear=" & SYear & _
                 " and left(accno,2)='13' and len(accno)=5 and substring(accno,4,2)<>'02' and left(accno,5)<>'13701' and left(accno,5)<>'13101') a " & _
                 " left outer join acf050 d on left(a.accno,3)+'02'=d.accno and a.accyear=d.accyear " & _
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
                MsgBox("無資料")
                Exit Sub
            End If

            Dim intR As Integer = 0  'control record number
            'Dim intBank As Integer = 0 'control bank detail row number 
            Dim i As Integer = 0   'control excel row number
            Dim strAccno, strAccname As String
            Dim spaces As Integer
            'Dim TOT7 As Decimal = 0
            'Dim TOT8 As Decimal = 0
            Dim sqlstr As String
            Dim tempds As DataSet

            Dim tt1, tt2 As String
            Try
                tt1 = "c:\App\acc\報表樣本\ACY13E.xls"
                tt2 = "c:\App\acc\報表\ACY13E.xls"
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
                MsgBox("報表copy from " & tt1 & "  to  " & tt2 & " 錯誤，若 \報表\ACY13E.XLS 使用中請先關閉之，否則請洽資訊人員!")
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
                    'strAccname = nz(.Rows(intR).Item("accname"), "")
                    'i += 1
                    '拷貝目前這列到下一列,使得每列都有相同的格式設定
                    'xlRange1 = xlsheet.Range("A" & i & ":K" & i)
                    'xlRange2 = xlsheet.Range("A" & i + 1 & ":K" & i + 1)
                    'xlRange1.Copy(xlRange2)

                    NAR(xlRange1)
                    NAR(xlRange2)
                    Select Case strAccno
                        Case "13201"
                            i = 6
                        Case "13301"
                            i = 8
                        Case "13401"
                            i = 10
                        Case "13501"
                            i = 12
                        Case "13601"
                            i = 14
                        Case Else
                            i = 18
                    End Select
                    '開始填入每列的資料

                    xlRange = xlCells(i, 3) '上年度原值
                    xlRange.Value = FormatNumber(.Rows(intR).Item("beg_debit") - .Rows(intR).Item("beg_credit"), 2)
                    NAR(xlRange)
                    xlRange = xlCells(i, 4) '年初累計折舊 
                    xlRange.Value = FormatNumber(nz(.Rows(intR).Item("acubeg_credit"), 0) - nz(.Rows(intR).Item("acubeg_debit"), 0), 2)
                    NAR(xlRange)
                    xlRange = xlCells(i, 7) '本年折舊 
                    xlRange.Value = FormatNumber(nz(.Rows(intR).Item("acucramt12"), 0) - nz(.Rows(intR).Item("acubeg_credit"), 0), 2)
                    NAR(xlRange)
                    xlRange = xlCells(i, 8) '本年報廢 
                    xlRange.Value = FormatNumber(nz(.Rows(intR).Item("acudeamt12"), 0) - nz(.Rows(intR).Item("acubeg_debit"), 0), 2)
                    NAR(xlRange)
                Next
            End With

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
