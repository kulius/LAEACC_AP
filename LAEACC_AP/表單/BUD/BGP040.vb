Imports JBC.Printing
Public Class BGP040
    Dim confid, confunit As String, LoadAfter, Dirty As Boolean
    Dim sqlstr As String
    Dim mydataset As DataSet
    Dim UserId, UserName, UserUnit As String

    Private Sub BGP040_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        UserId = TransPara.TransP("userid")
        UserUnit = TransPara.TransP("userunit")
        dtpDateS.Value = TransPara.TransP("userdate") '起日
        dtpDateE.Value = dtpDateS.Value               '訖日
        vxtStartNo.Text = "1"   '起值
        vxtEndNo.Text = "1"      '迄值
    End Sub

    Private Sub BtnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrint.Click
        Dim intR As Integer = 0   'control record number
        Dim retstr As String
        Dim totamt1 As Decimal = 0

        Dim printer As FPPrinter = FPPrinter.SharedPrinter
        Dim doc As New FPDocument("列印每日請購推算登記簿")
        doc.DefaultPageSettings.PaperKind = Printing.PaperKind.A4
        doc.DefaultPageSettings.Landscape = True
        doc.DefaultFont = New Font("新細明體", 11) '標楷體
        doc.SetDefaultPageMargin(20, 10, 5, 5)   'left top right bottom

        Dim PageRow As Integer = 25
        Dim PageNo As Integer = 0

        '找資料
        sqlstr = "SELECT bgf020.bgno, bgf020.accyear, BGF020.accno, bgf020.date1, bgf020.date2, bgf020.amt1, bgf020.remark, " & _
                 "bgf020.amt2, bgf020.amt3, bgf020.useableamt, ACCNAME.ACCNAME AS ACCNAME, bgf020.kind,bgf020.subject, bgf020.closemark, " & _
                 "bgf030.date3, bgf030.date4, bgf030.useamt, bgf030.no_1_no  " & _
                 "FROM BGF020 left outer JOIN bgf030 on bgf020.bgno=bgf030.bgno inner join ACCNAME ON BGF020.ACCNO = ACCNAME.ACCNO " & _
                 " WHERE BGF020.date1>='" & FullDate(dtpDateS.Value) & "' AND BGF020.date1<='" & FullDate(dtpDateE.Value) & _
                 "' and accname.staff_no='" & UserId & "'" & _
                 " and bgf020.accno between '" & GetAccno(vxtStartNo.Text) & "' and '" & GetAccno(vxtEndNo.Text) & "'" & _
                 " ORDER BY BGF020.date1, bgf020.bgno"
        mydataset = openmember("", "BGF020", sqlstr)
        For PageCnt As Integer = 1 To 999    '頁次
            Dim page As New FPPage
            Dim text1 As New FPText("推算者:" & UserId)
            page.AddText(text1)
            PageNo += 1
            Dim text3 As New FPText("列印日期:" & ShortDate(Now()) & " 頁次:" & PageNo, 210, 5)
            page.AddText(text3)
            Dim table0 As New FPTable(0, 10, 260, 7 * PageRow, PageRow, 6)
            table0.Font.Name = "標楷體"
            table0.Font.Size = 11
            table0.SetLineColor(Color.DarkBlue)
            table0.OutlineThicken(4)
            table0.ColumnStyles(1).Width = 20
            table0.ColumnStyles(2).Width = 20
            table0.ColumnStyles(3).Width = 40
            table0.ColumnStyles(4).Width = 120
            table0.ColumnStyles(5).Width = 30

            table0.ColumnStyles(3).HAlignment = StringAlignment.Near '整欄左靠
            table0.ColumnStyles(4).HAlignment = StringAlignment.Near '整欄左靠
            table0.ColumnStyles(5).HAlignment = StringAlignment.Far  '整欄右靠
            table0.Texts2D(1, 1).Text = "請購編號"
            table0.Texts2D(1, 2).Text = "請購日期"
            table0.Texts2D(1, 3).Text = "科目"
            table0.Texts2D(1, 4).Text = "　　　　摘　　　　　要"
            table0.Texts2D(1, 5).Text = "請購金額　."
            table0.Texts2D(1, 6).Text = "備註"

            With mydataset.Tables(0)
                For i As Integer = 2 To PageRow
                    If intR > .Rows.Count - 1 Then
                        table0.Texts2D(i, 4).Text = "   合 計" & Str(.Rows.Count) & " 件"
                        table0.Texts2D(i, 5).Text = Format(totamt1, "###,###,###,###.#")
                        PageCnt = 100000
                        Exit For
                    End If
                    table0.Texts2D(i, 1).Text = nz(.Rows(intR)("bgno"), "")
                    table0.Texts2D(i, 2).Text = ShortDate(nz(.Rows(intR)("date1"), ""))
                    table0.Texts2D(i, 3).Text = nz(.Rows(intR)("accno"), "")
                    table0.Texts2D(i, 4).Text = nz(.Rows(intR)("remark"), "")
                    table0.Texts2D(i, 5).Text = Format(nz(.Rows(intR)("amt1"), 0), "###,###,###,###.#")
                    totamt1 += nz(.Rows(intR)("amt1"), 0)
                    intR += 1
                Next
            End With

            page.Add(table0)
            doc.AddPage(page)
        Next


        printer.Document = doc
        printer.IsAutoShowPrintDialog = True '自動顯示印表機設定對話盒
        If rdoPreview.Checked Then
            printer.IsAutoShowPrintPreviewDialog = True
        End If
        printer.Print()
        Me.Close()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class
