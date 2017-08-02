Imports JBC.Printing
Public Class AC100
    Dim confid, confunit As String, LoadAfter, Dirty As Boolean
    Dim sqlstr As String
    Dim intEno1, intEno2, intEno3 As Integer
    'Dim strAccount, strBankname As String   '銀行名稱
    Dim mydataset, myds, mydsD As DataSet, mastconn As String
    Dim PageRow As Integer = 37  '每頁印37行

    Private Sub AC100_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '找未印資料
        nudYear.Value = GetYear(Now)
        sqlstr = "select * from acnop"
        myds = openmember("", "acnop", sqlstr)
        With myds.Tables("acnop")
            For intI As Integer = 0 To .Rows.Count - 1
                If .Rows(intI).Item("kind") = "1" Then txtSno1.Text = nz(.Rows(intI).Item("endno"), 0) + 1
                If .Rows(intI).Item("kind") = "2" Then txtSno2.Text = nz(.Rows(intI).Item("endno"), 0) + 1
                If .Rows(intI).Item("kind") = "3" Then txtSno3.Text = nz(.Rows(intI).Item("endno"), 0) + 1
            Next
        End With
        Call btnYear_Click(New System.Object, New System.EventArgs)
    End Sub

    Private Sub btnYear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnYear.Click
        '取號次控制檔記錄之最後號碼
        sqlstr = "select * from acfno where accyear=" & nudYear.Value & " and kind<='3'"
        myds = openmember("", "acnop", sqlstr)
        With myds.Tables("acnop")
            For intI As Integer = 0 To .Rows.Count - 1
                If .Rows(intI).Item("kind") = "1" Then txtEno1.Text = nz(.Rows(intI).Item("cont_no"), 0)
                If .Rows(intI).Item("kind") = "2" Then txtEno2.Text = nz(.Rows(intI).Item("cont_no"), 0)
                If .Rows(intI).Item("kind") = "3" Then txtEno3.Text = nz(.Rows(intI).Item("cont_no"), 0)
            Next
        End With
    End Sub

    Private Sub rdbPage_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbPage.CheckedChanged
        If rdbPage.Checked Then
            lblPage.Enabled = True
            txtPrintPageS.Enabled = True
            txtPrintPageE.Enabled = True
        Else
            lblPage.Enabled = False
            txtPrintPageS.Enabled = False
            txtPrintPageE.Enabled = False
        End If
    End Sub

    Private Sub BtnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrint.Click
        Dim intR As Integer = 0  'control record number
        Dim intI, intD, i, intTempNo As Integer
        Dim retstr, strRemark As String
        Dim intPage As Integer = 0
        intEno1 = 0 : intEno1 = 0 : intEno1 = 0
        Dim tempDate As Date

        Dim printer = New KPrint
        Dim document As New FPDocument("列印日記帳")
        document.SetDefaultPageMargin(15, 15, 15, 10)    'left,top,right,botton
        document.DefaultPageSettings.PaperKind = Printing.PaperKind.A4   'b4:364*257 a4:210*297
        document.DefaultPageSettings.Landscape = False    '直印
        document.DefaultFont = New Font("新細明體", 10) '標楷體
        intR = 0

        '找資料(由acf010逐張傳票列印)
        sqlstr = " select a.* from " & _
                 "(SELECT date_1, kind, no_1_no, seq, item, accno, remark, amt, dc, bank FROM acf010 where accyear=" & nudYear.Value & _
                 " and ((kind='1' and no_1_no between " & ValComa(txtSno1.Text) & " and " & ValComa(txtEno1.Text) & ")" & _
                 " or (kind='2' and no_1_no between " & ValComa(txtSno2.Text) & " and " & ValComa(txtEno2.Text) & ")" & _
                 " or (kind='3' and no_1_no between " & ValComa(txtSno3.Text) & " and " & ValComa(txtEno3.Text) & "))) a " & _
                 " order by a.date_1, a.kind, a.no_1_no, a.dc, a.seq, a.item "

        mydataset = openmember("", "acf010", sqlstr)
        If mydataset.Tables("acf010").Rows.Count > 0 Then
            tempDate = nz(mydataset.Tables("acf010").Rows(0).Item("date_1"), "")  '記錄第一筆之日期
        End If
        Dim page As FPPage
        '以下是符合頁碼範圍所要用到的物件變數
        Dim textUnit As FPText
        Dim textTitle As FPText
        Dim textday As FPText
        Dim textbott As FPText
        Dim textPage As FPText
        Dim grid As FPTable
        '以下是不在頁碼範圍內要用到的物件變數
        Dim ttextUnit As New FPText
        Dim ttextTitle As New FPText
        Dim ttextday As New FPText
        Dim ttextbott As New FPText
        Dim ttextPage As New FPText
        Dim ggrid As New FPTable(0, 17, 324, 222, PageRow, 8)
        '宣告一變數儲存是否符合頁碼範圍
        Dim blnIsBelong As Boolean
        Dim blnIsPrint As Boolean = False '判斷是否有資料頁列印
        For PageCnt As Integer = 1 To 999    '頁次
            intPage += 1
            blnIsBelong = (rdbAll.Checked = True Or (rdbAll.Checked = False And intPage >= Val(txtPrintPageS.Text) And intPage <= Val(txtPrintPageE.Text)))
            '控制列印範圍內的才new
            If blnIsBelong Then
                blnIsPrint = True
                page = New FPPage
                textUnit = New FPText(TransPara.TransP("UnitTitle"), 90, 0)
                textUnit.HAlignment = FPAlignment.Center
                textUnit.Font.Size = 12   '改變文字大小為14點
                textTitle = New FPText("傳票交付簿", 90, 6)
                textTitle.HAlignment = FPAlignment.Center
                textTitle.Font.Size = 12
                textday = New FPText("中華民國" & nudYear.Value & "年度", 90, 12)
                textday.HAlignment = FPAlignment.Center
                textbott = New FPText("出納人員簽收:", 0, 270)
                textbott.HAlignment = FPAlignment.Center
                textPage = New FPText("第 " & intPage & " 頁", 170, 12)
                grid = New FPTable(0, 17, 180, 240, PageRow, 6)  'a3 370,252 '新增表格物件,要列印在座標 (0,17),寬度324,高度222,共有pagerow列8欄,(單位是公厘)
                grid.Font.Size = 10
                'grid.alllinehide   所有格線隱藏
                grid.ColumnStyles(3).HAlignment = StringAlignment.Far
                grid.ColumnStyles(4).HAlignment = StringAlignment.Near
                grid.ColumnStyles(5).HAlignment = StringAlignment.Near
                grid.ColumnStyles(6).HAlignment = StringAlignment.Far
                grid.SetLineColor(Color.Blue)   'table line = blue 
                grid.ColumnStyles(1).Width = 17
                grid.ColumnStyles(2).Width = 10
                grid.ColumnStyles(3).Width = 14
                grid.ColumnStyles(4).Width = 24
                grid.ColumnStyles(5).Width = 90
                grid.ColumnStyles(6).Width = 25

                grid.Cells2D(1, 1).ColSpan = 3 '12,3 隱藏
                grid.Cells2D(1, 4).RowSpan = 2
                grid.Cells2D(1, 5).RowSpan = 2
                grid.Cells2D(1, 6).RowSpan = 2
                grid.Texts2D(1, 1).Text = "　　傳　　　票　"
                grid.Texts2D(2, 1).Text = "日　期"
                grid.Texts2D(2, 2).Text = "種類"
                grid.Texts2D(2, 3).Text = "號 數"
                grid.Texts2D(1, 4).Text = "科  目"
                grid.Texts2D(1, 5).Text = "　　摘　　　　　　　要"
                grid.Texts2D(1, 6).Text = "金    額 ."
            Else
                textUnit = ttextUnit
                textTitle = ttextTitle
                textday = ttextday
                textbott = ttextbott
                textPage = ttextPage
                grid = ggrid
            End If

            i = 2
            intTempNo = 0   '控制是否要印傳票日期 種類 號數

            With mydataset.Tables("acf010")
                Do While i < PageRow  '每頁印37行
                    If intR > .Rows.Count - 1 Then
                        PageCnt = 1000
                        Exit Do
                        Exit For
                    End If   'the end of record 
                    '控制換頁
                    i += 1
                    If i > PageRow Then Exit Do
                    If .Rows(intR).Item("no_1_no") <> intTempNo Then
                        grid.Texts2D(i, 1).Text = ShortDate(.Rows(intR).Item("date_1"))
                        If .Rows(intR).Item("kind") = "1" Then
                            grid.Texts2D(i, 2).Text = "收"
                            If intEno1 < .Rows(intR).Item("no_1_no") Then intEno1 = .Rows(intR).Item("no_1_no")
                        End If
                        If .Rows(intR).Item("kind") = "2" Then
                            grid.Texts2D(i, 2).Text = "支"
                            If intEno2 < .Rows(intR).Item("no_1_no") Then intEno2 = .Rows(intR).Item("no_1_no")
                        End If
                        If .Rows(intR).Item("kind") >= "3" Then
                            grid.Texts2D(i, 2).Text = "轉"
                            If intEno3 < .Rows(intR).Item("no_1_no") Then intEno3 = .Rows(intR).Item("no_1_no")
                        End If
                        grid.Texts2D(i, 3).Text = Format(.Rows(intR).Item("no_1_no"), 0)
                        intTempNo = .Rows(intR).Item("no_1_no")
                    End If
                    grid.Texts2D(i, 5).Text = .Rows(intR).Item("remark")
                    'If .Rows(intR).Item("item") = "9" Then   '銀行資料在第9項顯示即可
                    '    grid.Texts2D(i, 6).Text = .Rows(intR).Item("bank")
                    'End If
                    If .Rows(intR).Item("dc") = "1" Then   '借方
                        grid.Texts2D(i, 4).Text = FormatAccno(nz(.Rows(intR).Item("accno"), ""))
                    Else
                        grid.Texts2D(i, 4).Text = Space(4) & FormatAccno(nz(.Rows(intR).Item("accno"), ""))
                    End If
                    grid.Texts2D(i, 6).Text = FormatNumber(.Rows(intR).Item("amt"), 0)
                    intR += 1
                Loop
            End With

            '控制列印範圍內的才加入
            If blnIsBelong Then
                page.Add(textUnit)
                page.Add(textTitle)
                page.Add(textday)
                page.Add(textbott)
                page.Add(textPage)
                page.Add(grid)   '加入要列印的表格到列印頁面中
                document.AddPage(page)  '加入要列印的頁面到列印文件中
            End If
        Next
        If document.PageCount > 0 Then
            printer.Document = document
            printer.PrintMode = PrintMode.NormalPrint
            printer.IsAutoShowPrintDialog = True '自動顯示印表機設定對話盒
            If rdoPreview.Checked Then printer.IsAutoShowPrintPreviewDialog = True '自動顯示預覽列印對話盒
            If blnIsPrint Then printer.Print() '開始列印

            '列印後將最後號碼寫入acnop 
            If intEno1 > 0 Then
                sqlstr = "update acnop set eno=" & intEno1 & " where kind='1'"
                retstr = runsql("", sqlstr)
            End If
            If intEno2 > 0 Then
                sqlstr = "update acnop set eno=" & intEno2 & " where kind='2'"
                retstr = runsql("", sqlstr)
            End If
            If intEno3 > 0 Then
                sqlstr = "update acnop set eno=" & intEno3 & " where kind='3'"
                retstr = runsql("", sqlstr)
            End If
        End If
        Me.Close()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        mydataset = Nothing
        myds = Nothing
        Me.Close()
    End Sub
End Class
