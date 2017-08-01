Imports JBC.Printing
Imports System.IO
Imports Microsoft.Office.Interop
Public Class ACY030
    Dim SYear As Integer
    Dim myds, AccnoDataset As DataSet
    Dim xlCells As Excel.Range

    Private Sub ACY030_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dtpDateS.Value = TransPara.TransP("LastDay")
        Dim sqlstr As String
        '將科目置combobox
        sqlstr = "SELECT accno, accno+accname as accname FROM accname where belong<>'B' and " & _
                 "left(accno,1)='3' and substring(accno,4,2)='01' and accno<='32101' order by accno"
        accnoDataset = openmember("", "accname", sqlstr)
        If accnoDataset.Tables("accname").Rows.Count = 0 Then
            cboAccno.Text = "尚無科目"
        Else
            cboAccno.DataSource = accnoDataset.Tables("accname")
            cboAccno.DisplayMember = "accname"  '顯示欄位
            cboAccno.ValueMember = "accno"     '欄位值
        End If
    End Sub
    Private Sub LoadGridFunc()
        Dim sqlstr As String

        'ACM010 已於ACY020產生

        '丟入dataset 
        sqlstr = "SELECT * FROM acm010 WHERE len(accno)=1 order by accno"
        myds = openmember("", "acm010", sqlstr)
    End Sub

    Private Sub BtnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrint.Click
        Dim xlapp As Excel.Application
        Dim xlbook As Excel.Workbook
        Dim xlbooks As Excel.Workbooks
        Dim xlsheet As Excel.Worksheet
        Dim xlsheets As Excel.Sheets
        Dim xlRange As Excel.Range

        Dim intR As Integer = 0  'control record number
        Dim TOTyy, TOTbg, TOTyy2, TOTbg2, Net1, Net2, Net3 As Decimal
        TOTyy = 0 : TOTbg = 0 : TOTyy2 = 0 : TOTbg2 = 0 : Net1 = 0 : Net2 = 0 : Net3 = 0
        xlapp = CreateObject("excel.application")
        Dim tt1, tt2 As String

        Try

            SYear = GetYear(dtpDateS.Value)
            Call LoadGridFunc()
            If myds.Tables("acm010").Rows.Count <= 1 Then
                MsgBox("無此資料,請先執行餘絀計算表")
                Exit Sub
            End If

            Try
                tt1 = "c:\App\acc\報表樣本\ACY030.xls"
                tt2 = "c:\App\acc\報表\ACY030.xls"
                If Not File.Exists(tt1) Then
                    AppReport_Copy("acc", "ACY030.xls", tt1)
                    'MsgBox("找不到報表樣本，請洽資訊人員" & vbNewLine & tt1)
                    'Exit Sub
                End If
                FileCopy(tt1, tt2)    'copy tt1 to tt2
                xlbooks = xlapp.Workbooks
                xlbook = xlbooks.Open(tt2) '開啟tt2

            Catch ex As Exception
                MsgBox("報表copy " & tt1 & "  to  " & tt2 & "錯誤，是否\報表\ACY030.XLS使用中,否則請洽程式設計人員!")
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
            xlRange = xlsheet.Range("F7")   '撥補之科目
            xlRange.Value = Mid(cboAccno.SelectedValue, 1, 1) & "-" & Mid(cboAccno.SelectedValue, 2, 4) & vbCrLf & Mid(cboAccno.Text, 6, 10)
            NAR(xlRange)

            With myds.Tables("acm010")
                For intR = 0 To .Rows.Count - 1
                    If .Rows(intR).Item("accno") = "4" Then
                        TOTyy = nz(.Rows(intR).Item("amt5"), 0)  '本年度收入總額
                        'TOTup = nz(.Rows(intR).Item("amt6"), 0)  '上年度收入總額
                        TOTbg = nz(.Rows(intR).Item("amt7"), 0)  '本年度收入預算總額
                    Else
                        TOTyy2 = nz(.Rows(intR).Item("amt5"), 0)   '本年度支出總額
                        'TOTup2 = nz(.Rows(intR).Item("amt6"), 0)   '上年度支出總額
                        TOTbg2 = nz(.Rows(intR).Item("amt7"), 0)   '本年度支出預算總額
                    End If
                Next
            End With
            Net1 = TOTyy - TOTyy2  '決算數本期結餘
            Net2 = TOTbg - TOTbg2  '預算數本期結餘
            Net3 = Net1 - Net2     '餘絀之增減

            If TOTyy - TOTyy2 < 0 Then  '本期短絀
                Net1 = -Net1
                Net2 = -Net2
                Net3 = Net1 - Net2
                xlRange = xlsheet.Cells(4, 7)   '表頭
                xlRange.Value = "填  補  數"
                NAR(xlRange)
                xlRange = xlsheet.Cells(6, 1)
                xlRange.Value = "短       絀"
                NAR(xlRange)
                xlRange = xlsheet.Cells(6, 6)
                xlRange.Value = "填  補  短  絀"
                NAR(xlRange)
                xlRange = xlsheet.Cells(7, 1)
                xlRange.Value = " 本年度短絀"
                NAR(xlRange)
                'Net3 = -Net3   '短絀之增減
            End If
            xlRange = xlsheet.Cells(6, 2)
            xlRange.Value = FormatNumber(Net1, 2)   'Math.Abs(TOTyy - TOTyy2)
            NAR(xlRange)
            xlRange = xlsheet.Cells(6, 3)
            xlRange.Value = FormatNumber(Net2, 2)
            NAR(xlRange)
            xlRange = xlsheet.Cells(6, 4)
            xlRange.Value = FormatNumber(Net3, 2)
            NAR(xlRange)
            xlRange = xlsheet.Cells(6, 5)
            xlRange.Value = FormatNumber(rate(Net3, Net2, 0), 2)
            xlRange = xlsheet.Cells(6, 7)
            xlRange.Value = FormatNumber(Net1, 2)
            NAR(xlRange)
            xlRange = xlsheet.Cells(6, 8)
            xlRange.Value = FormatNumber(Net2, 2)
            NAR(xlRange)
            xlRange = xlsheet.Cells(6, 9)
            xlRange.Value = FormatNumber(Net3, 2)
            NAR(xlRange)
            xlRange = xlsheet.Cells(6, 10)
            xlRange.Value = FormatNumber(rate(Net3, Net2, 0), 2)
            NAR(xlRange)


            '儲存檔案
            xlbook.Save()
            If rdoPrint.Checked Then
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
