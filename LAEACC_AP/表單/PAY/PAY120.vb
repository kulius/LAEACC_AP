Imports JBC.Printing
Public Class PAY120
    Dim LoadAfter, Dirty As Boolean
    Dim mydataset As DataSet

    Private Sub pay120_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadAfter = True
        Dim sqlstr, qstr, strD, strC As String
        Dim intI, PageNo As Integer

        sqlstr = "SELECT max(date_2) as date_2 FROM  CHF020"
        mydataset = openmember("", "chf020", sqlstr)
        If mydataset.Tables("chf020").Rows.Count = 0 Or IsDBNull(mydataset.Tables("chf020").Rows(0).Item("date_2")) Then
            dtpDate.Value = Now
        Else
            dtpDate.Value = mydataset.Tables("chf020").Rows(0).Item("date_2")
        End If
    End Sub

    Private Sub btnSure_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSure.Click
        Dim sqlstr, retstr, qstr, strD, strC, skind As String
        Dim intI, intJ, SumAmt, err As Integer

        '找傳票資料(table body record )
        sqlstr = "SELECT * FROM ACF010 " & _
                 "WHERE ITEM = '9' AND DATE_2 = '" & FullDate(dtpDate.Value) & "' and bank='" & txtBank.Text & _
                 "' and amt <> act_amt  order by kind, no_2_no "

        mydataset = openmember("", "acf010", sqlstr)
        If mydataset.Tables("acf010").Rows.Count = 0 Then
            MsgBox("沒有沖收付資料")
            btnSure.Visible = False
            Exit Sub
        End If

        Call PAY120_PRINT()

    End Sub

    Public Sub PAY120_PRINT()
        Dim intR As Integer = 0
        Dim pagerow As Integer = 45
        Dim tempKind As String = ""
        Dim printer = New KPrint
        Dim document As New FPDocument("列印沖收付數")
        document.SetDefaultPageMargin(6, 6, 10, 6)   'left,top,right,botton
        document.DefaultPageSettings.Landscape = False    '直印
        document.DefaultFont = New Font("新細明體", 10) '標楷體
        Dim sum1 As Integer = 0
        Dim sum2 As Integer = 0

        For PageCnt As Integer = 1 To 99    '頁次

            Dim page As New FPPage '新增列印頁面
            page.Font.Size = 10  '設定本頁文字default size=10

            Dim textTitle As New FPText("列印" & txtBank.Text & "銀行沖收付數" & dtpDate.Value.ToShortDateString, 90, 5)
            textTitle.HAlignment = FPAlignment.Center
            textTitle.Font.Size = 12
            page.Add(textTitle)

            Dim grid As New FPTable(0, 10, 200, 270, pagerow, 6)   'x,y,w,h
            grid.Font.Size = 10

            grid.ColumnStyles(3).HAlignment = StringAlignment.Near
            grid.ColumnStyles(4).HAlignment = StringAlignment.Far
            grid.ColumnStyles(5).HAlignment = StringAlignment.Far
            grid.ColumnStyles(6).HAlignment = StringAlignment.Far
            grid.SetLineColor(Color.Blue)   'table line = red 
            grid.ColumnStyles(1).Width = 12
            grid.ColumnStyles(2).Width = 18
            grid.ColumnStyles(3).Width = 95
            grid.ColumnStyles(4).Width = 25
            grid.ColumnStyles(5).Width = 25
            grid.ColumnStyles(6).Width = 25

            grid.Texts2D(1, 1).Text = "種類"
            grid.Texts2D(1, 2).Text = "號　數"
            grid.Texts2D(1, 3).Text = "　　　　　摘　　　　　　要"
            grid.Texts2D(1, 4).Text = "傳票金額"
            grid.Texts2D(1, 5).Text = "沖收付數"
            grid.Texts2D(1, 6).Text = "實收付數"
            Dim i As Integer = 1
            With mydataset.Tables("acf010")
                If intR = 0 Then tempKind = .Rows(0).Item("kind")

                Do While i < pagerow
                    If intR > .Rows.Count - 1 Then
                        If sum1 + sum2 <> 0 Then
                            i += 1
                            If i > pagerow Then Exit Do
                            If tempKind = "1" Then grid.Texts2D(i, 3).Text = "收入傳票合計"
                            If tempKind = "2" Then grid.Texts2D(i, 3).Text = "支出傳票合計"
                            grid.Texts2D(i, 4).Text = FormatNumber(sum1, 0)
                            grid.Texts2D(i, 5).Text = FormatNumber(sum1 - sum2, 0)
                            grid.Texts2D(i, 6).Text = FormatNumber(sum2, 0)
                            sum1 = 0
                            sum2 = 0
                        End If
                        PageCnt = 100
                        Exit Do
                        Exit For
                    End If
                    If .Rows(intR).Item("kind") <> tempKind Then  '傳票不同時要印小計
                        i += 1
                        If i > pagerow Then Exit Do
                        If tempKind = "1" Then grid.Texts2D(i, 3).Text = "收入傳票合計"
                        If tempKind = "2" Then grid.Texts2D(i, 3).Text = "支出傳票合計"
                        grid.Texts2D(i, 4).Text = FormatNumber(sum1, 0)
                        grid.Texts2D(i, 5).Text = FormatNumber(sum1 - sum2, 0)
                        grid.Texts2D(i, 6).Text = FormatNumber(sum2, 0)
                        sum1 = 0
                        sum2 = 0
                        tempKind = .Rows(intR).Item("kind")
                    End If
                    i += 1
                    If i > pagerow Then
                        Exit Do
                    End If
                    grid.Texts2D(i, 1).Text = .Rows(intR).Item("kind")
                    grid.Texts2D(i, 2).Text = .Rows(intR).Item("no_2_no")
                    grid.Texts2D(i, 3).Text = .Rows(intR).Item("remark")
                    grid.Texts2D(i, 4).Text = FormatNumber(.Rows(intR).Item("amt"), 0)
                    grid.Texts2D(i, 5).Text = FormatNumber(.Rows(intR).Item("amt") - .Rows(intR).Item("act_amt"), 0)
                    grid.Texts2D(i, 6).Text = FormatNumber(.Rows(intR).Item("act_amt"), 0)

                    sum1 += .Rows(intR).Item("amt")
                    sum2 += .Rows(intR).Item("act_amt")
                    intR += 1
                Loop
            End With
            page.Add(grid) '加入要列印的表格到列印頁面中
            document.AddPage(page) '加入要列印的頁面到列印文件中
        Next
        printer.Document = document '將要列印的文件送到印表機
        printer.PrintMode = PrintMode.NormalPrint
        If rdoPreview.Checked Then printer.IsAutoShowPrintPreviewDialog = True
        printer.Print()
        mydataset = Nothing
        document = Nothing
        btnSure.Enabled = False
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        MyBase.Close()
    End Sub
End Class
