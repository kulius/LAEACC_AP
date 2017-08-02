Imports JBC.Printing
Imports System.IO
Imports Microsoft.Office.Interop
Public Class ACM100
    Dim confid, confunit As String, LoadAfter, Dirty As Boolean

    Private Sub ACM100_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dtpDateS.Value = TransPara.TransP("LastDay")
    End Sub

    Private Sub BtnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrint.Click
        TransPara.TransP("LastDay") = dtpDateS.Value
        Dim SYear As Integer = GetYear(dtpDateS.Value)


        Dim printer = New KPrint
        Dim document As New FPDocument("列印月報封面")
        document.SetDefaultPageMargin(10, 10, 10, 10)   'left,top,right,bottom
        document.DefaultPageSettings.Landscape = True    '橫印
        document.DefaultFont = New Font("標楷體", 11) '標楷體新細明體
        Dim page As New FPPage  '新增列印頁面

        Dim textUnit As New FPText(TransPara.TransP("UnitTitle"), 90, 15)  '文字物件,座標90,0 (x,y)
        textUnit.HAlignment = FPAlignment.Center
        textUnit.Font.Size = 32   '改變文字大小為36點
        page.Add(textUnit)

        Dim textyear As New FPText("中華民國" & SYear & "年度", 90, 40)
        textyear.HAlignment = FPAlignment.Center
        textyear.Font.Size = 26  '改變文字大小為14點
        page.Add(textyear)

        Dim textday As New FPText("自民國" & SYear & "年" & Month(dtpDateS.Value) & "月1日起至" & _
                           SYear & "年" & Month(dtpDateS.Value) & "月" & Microsoft.VisualBasic.DateAndTime.Day(dtpDateS.Value) & "日止", 90, 55)
        textday.HAlignment = FPAlignment.Center
        textday.Font.Size = 26  '改變文字大小為14點
        page.Add(textday)

        Dim textTitle As New FPText("會　計　月　報　表", 90, 90)
        textTitle.HAlignment = FPAlignment.Center
        textTitle.Font.Size = 48
        textTitle.Font.Bold = True
        page.Add(textTitle)

        'Dim lastline As String = "會  長" & Space(32) & "主辦主計人員" & Space(32) & "覆  核" & Space(32) & "製  表"
        Dim lastline As String = "製  表" & Space(32) & "覆  核" & Space(32) & "主辦主計人員" & Space(32) & "會  長"
        Dim textjude As New FPText(lastline, 20, 160)
        textjude.HAlignment = FPAlignment.Center
        textjude.Font.Size = 14   '改變文字大小為14點
        page.Add(textjude)

        document.AddPage(page)  '加入要列印的頁面到列印文件中

        printer.Document = document   '將要列印的文件送到印表機
        printer.PrintMode = PrintMode.NormalPrint
        printer.IsAutoShowPrintDialog = True '自動顯示印表機設定對話盒
        If rdoPreview.Checked = True Then printer.IsAutoShowPrintPreviewDialog = True
        printer.Print()
        Me.Close()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class
