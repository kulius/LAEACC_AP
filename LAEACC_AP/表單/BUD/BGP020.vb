Imports JBC.Printing
Public Class BGP020
    Dim confid, confunit As String, LoadAfter, Dirty As Boolean
    Dim sqlstr, strAccno As String
    Dim bm As BindingManagerBase, mydataset, myds, userdataset As DataSet
    Dim UserId, UserName, UserUnit As String
    Dim intBg1, intBg2, intBg3, intBg4, intBg5 As Decimal
    Dim PageRow As Integer = 22  '每頁印22行

    Private Sub BGP020_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TabControl1.Enabled = False
        UserId = TransPara.TransP("userid")
        UserUnit = TransPara.TransP("userunit")
        nudYear.Value = GetYear(TransPara.TransP("userdate"))
        vxtStartNo.Text = "4"    '起值
        vxtEndNo.Text = "59"      '迄值
        If Mid(UserUnit, 1, 2) = "05" Then
            cboUser.Visible = True
            sqlstr = "SELECT b.staff_no as userid, b.staff_no+USERTABLE.USERNAME as username FROM USERTABLE right outer JOIN" & _
                    "(SELECT STAFF_NO FROM ACCNAME WHERE STAFF_NO IS NOT NULL AND STAFF_NO <> '    ' GROUP BY STAFF_NO) b " & _
                    "ON USERTABLE.USERID = b.STAFF_NO order by usertable.userid"
            userdataset = openmember("", "user", sqlstr)
            If userdataset.Tables("user").Rows.Count = 0 Then
                cboUser.Text = "無user"
            Else
                cboUser.DataSource = userdataset.Tables("user")
                cboUser.ValueMember = "userid"     '欄位值
                cboUser.DisplayMember = "username"  '顯示欄位
            End If
            Dim nr As DataRow
            nr = userdataset.Tables("user").NewRow()
            nr("userid") = "全部"
            nr("username") = "全部"
            userdataset.Tables("user").Rows.Add(nr)
        End If
    End Sub

    Private Sub BtnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSearch.Click
        TabControl1.Enabled = True
        If Mid(UserUnit, 1, 2) = "05" Then
            UserId = cboUser.SelectedValue
        End If
        Call LoadGridFunc()
        Call PutGridToTable()
        BtnPrint.Enabled = True
    End Sub

    Sub LoadGridFunc()
        '丟當年度所有預算科目to Grid1 
        Dim sqlstr, qstr, strD, strC As String
        sqlstr = "SELECT a.accno,b.accname as accname, " & _
                 "a.bg1 as bg1, a.bg2 as bg2, a.bg3 as bg3, a.bg4 as bg4, a.bg5 as bg5 " & _
                 "FROM bgf010 a INNER JOIN ACCNAME b ON a.ACCNO = b.ACCNO " & _
                 " WHERE a.accyear=" & nudYear.Value & " and a.accno>='" & _
                  GetAccno(vxtStartNo.Text) & "' and a.accno<='" & GetAccno(vxtEndNo.Text) & "'"
        If UserId = "全部" Then
            sqlstr = sqlstr & " order by a.accno"
        Else
            sqlstr = sqlstr & " and b.STAFF_NO = '" & Trim(UserId) & "' order by a.accno"
        End If
        mydataset = openmember("", "BGF010", sqlstr)
        DataGrid1.DataSource = mydataset
        DataGrid1.DataMember = "BGF010"
        bm = Me.BindingContext(mydataset, "BGF010")
        TabControl1.SelectedIndex = 0
    End Sub

    Sub PutGridToTable()
        Dim strAccno, tAccno As String
        Dim intG As Integer
        Dim retstr As String
        Dim dBG1, dBG2, dBG3, dBG4, dBG5 As Decimal

        sqlstr = "delete BGP020 where userid='" & UserId & "'"    '先清空tempfile 
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then
            MsgBox("刪除BGp020失敗" & sqlstr)
        End If
        '將datagrid逐筆insert to bgp020 
        For intI As Integer = 0 To bm.Count - 1
            bm.Position = intI
            strAccno = bm.Current("accno")
            dBG1 = bm.Current("bg1")
            dBG2 = bm.Current("bg2")
            dBG3 = bm.Current("bg3")
            dBG4 = bm.Current("bg4")
            dBG5 = bm.Current("bg5")
            GenInsSql("accno", strAccno, "T")
            GenInsSql("bg1", dBG1, "N")
            GenInsSql("bg2", dBG2, "N")
            GenInsSql("bg3", dBG3, "N")
            GenInsSql("bg4", dBG4, "N")
            GenInsSql("bg5", dBG5, "N")
            GenInsSql("userid", UserId, "T")
            sqlstr = "insert into BGP020 " & GenInsFunc
            retstr = runsql(mastconn, sqlstr)
            intG = Grade(strAccno)
            For intJ As Integer = 2 To intG - 1
                If intJ = 7 Then
                    tAccno = Trim(Mid(strAccno, 1, 16))
                End If
                If intJ = 6 Then
                    tAccno = Trim(Mid(strAccno, 1, 9))
                End If
                If intJ = 5 Then
                    tAccno = Trim(Mid(strAccno, 1, 7))
                End If
                If intJ = 4 Then
                    tAccno = Trim(Mid(strAccno, 1, 5))
                End If
                If intJ = 3 Then
                    tAccno = Trim(Mid(strAccno, 1, 3))
                End If
                If intJ = 2 Then
                    tAccno = Trim(Mid(strAccno, 1, 2))
                End If
                If intJ = Grade(tAccno) And intJ <> intG Then
                    sqlstr = "SELECT * from BGP020 where accno='" & tAccno & "' and userid='" & UserId & "'"
                    mydataset = openmember("", "bgp020", sqlstr)
                    If mydataset.Tables("bgp020").Rows.Count = 0 Then   '原無該科目餘額則新增一筆
                        GenInsSql("accno", tAccno, "T")
                        GenInsSql("userid", UserId, "T")
                        sqlstr = "insert into bgp020 " & GenInsFunc
                        retstr = runsql(mastconn, sqlstr)
                        If retstr <> "sqlok" Then
                            MsgBox("insert  BGp020失敗" & sqlstr)
                        End If
                    End If
                    sqlstr = "update bgp020 set bg1=bg1+" & dBG1 & ", bg2=bg2+" & dBG2 & ", bg3=bg3+" & dBG3 & _
                            ", bg4=bg4+" & dBG4 & ", bg5=bg5+" & dBG5 & _
                            " where accno='" & tAccno & "'"
                    retstr = runsql(mastconn, sqlstr)
                End If
            Next
        Next
    End Sub

    Private Sub BtnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrint.Click
        Dim intGrade As Integer = 0
        If Mid(UserUnit, 1, 2) = "05" Then
            UserId = cboUser.SelectedValue
        End If
        sqlstr = "SELECT a.*,b.accname from bgp020 a inner join accname b ON a.accno = b.ACCNO "
        If UserId = "全部" Then
            sqlstr = sqlstr & " where a.userid='全部'"  ' and len(a.accno)<=9 order by a.accno"
        Else
            sqlstr = sqlstr & " where a.userid='" & UserId & "'"
        End If
        Dim intLen As Integer = 17
        Select Case nudGrade.Value
            Case 4
                intLen = 5
            Case 5
                intLen = 7
            Case 6
                intLen = 9
            Case 7
                intLen = 16
            Case 8
                intLen = 17
        End Select
        sqlstr = sqlstr & " and len(a.accno)<=" & FormatNumber(intLen, 0) & " order by a.accno"

        'mydataset.Tables("BGP020").Clear()
        mydataset = openmember("", "BGP020", sqlstr)
        If mydataset.Tables("BGP020").Rows.Count = 0 Then
            MsgBox("無資料")
            Exit Sub
        End If

        Dim intR As Integer = -1        '資料行

        '取得印表機物件
        '在WinForm使用 FPPrinter.SharedPrinter 或是 New FPPrinter 來取得
        '在WebForm則建議使用 New FPPrinter 來取得,因為多人同時使用必須建立不同的印表機實體
        Dim printer As FPPrinter = FPPrinter.SharedPrinter

        '新增一份列印文件,一份列印文件可以包含多個列印頁面
        '座標預設單位是公厘,預設列印模式是一般列印(非套表列印),預設是使用預設印表機列印,預設紙張是A4,預設來源紙匣是自動進紙
        '預設上下左右邊界都是25.4公厘(一英吋),預設是直式彩色模式列印,預設字體是細明體,大小12點,預設筆刷是實心黑色筆刷
        '參數 documentName 傳入 "測試列印" 是會顯示在印表機佇列中的文件名稱
        Dim document As New FPDocument("預算分配表")
        'document.DefaultPageSettings.PaperKind = Printing.PaperKind.A3
        document.DefaultPageSettings.Landscape = True    '橫印
        document.DefaultPageSettings.Margins.Top = 10
        document.DefaultPageSettings.Margins.Right = 5
        document.DefaultPageSettings.Margins.Left = 10
        document.DefaultPageSettings.Margins.Bottom = 10
        document.DefaultFont = New Font("新細明體", 10) '標楷體

        For PageCnt As Integer = 1 To 99    '頁次

            '新增列印頁面,一個列印頁面可以包含多個列印物件,譬如文字,表格,影像等等
            Dim page As New FPPage

            '新增文字物件,要列印在座標 (0,0) (col,row) (單位是公厘)
            Dim text As New FPText(TransPara.TransP("UnitTitle") & nudYear.Value & "年度預算分配表", 0, 0)
            text.HAlignment = FPAlignment.Center
            text.Font.Size = 14   '改變文字大小為14點
            If UserId <> "全部" Then
                myds = openmember("", "user", "select * from UNITTABLE where unit='" & TransPara.TransP("userUnit") & "'")
                If myds.Tables("user").Rows.Count > 0 Then
                    Dim text2 As New FPText("預算單位:" & myds.Tables("user").Rows(0).Item("unitname"), 14, 5)
                    page.Add(text2)
                End If
            End If
            Dim text3 As New FPText("第" & PageCnt & "頁", 250, 5)
            page.Add(text3)

            '新增表格物件,要列印在座標 (15,10),寬度280,高度150,共有22列7欄,(單位是公厘)
            '每欄ㄉ寬度和每列的高度,預設會使用自動平均後的值
            'FPTable 和 FPGrid 這兩個物件的差異在 FPTable內含 FPCell物件,可以設定跨列和跨欄,但FPGrid則無
            Dim grid As New FPTable(15, 10, 270, 180, 22, 7)   '262->270
            grid.Font.Size = 11
            grid.ColumnStyles(1).HAlignment = StringAlignment.Near
            grid.ColumnStyles(2).HAlignment = StringAlignment.Far
            grid.ColumnStyles(3).HAlignment = StringAlignment.Far
            grid.ColumnStyles(4).HAlignment = StringAlignment.Far
            grid.ColumnStyles(5).HAlignment = StringAlignment.Far
            grid.ColumnStyles(6).HAlignment = StringAlignment.Far
            grid.ColumnStyles(7).HAlignment = StringAlignment.Far
            'grid.RowStyles(2).VAlignment = StringAlignment.Far   'row下靠
            'grid.Texts2D(i, 2).HAlignment = FPAlignment.Far 'column  右靠

            'grid.SetLineColor(Color.Red)   'table line = red 
            grid.ColumnStyles(1).Width = 122  '97
            grid.ColumnStyles(2).Width = 25
            grid.ColumnStyles(3).Width = 25
            grid.ColumnStyles(4).Width = 25
            grid.ColumnStyles(5).Width = 25
            grid.ColumnStyles(6).Width = 25
            grid.ColumnStyles(7).Width = 23
            grid.Texts2D(1, 1).Text = "會計科目及符號"
            'grid.cells2d(1,1).ColSpan=2  '1,2 隱藏
            'grid.Texts2D(1, 1).SetTextColor(Color.Red)  'set cell color 

            grid.Texts2D(1, 2).Text = "核定預算數"
            grid.Texts2D(1, 3).Text = "第一季分配"
            grid.Texts2D(1, 4).Text = "第二季分配"
            grid.Texts2D(1, 5).Text = "第三季分配"
            grid.Texts2D(1, 6).Text = "第四季分配"
            grid.Texts2D(1, 7).Text = "保留數"

            With mydataset.Tables("bgp020")
                For i As Integer = 2 To PageRow
                    intR += 1
                    If intR > .Rows.Count - 1 Then
                        PageCnt = 999    'EXIT FOR THE PAGE 
                        Exit For
                    End If
                    strAccno = nz(.Rows(intR).Item("accno"), "")
                    intGrade = Grade(strAccno)  'Space((Grade(strAccno) - 2) * 2)

                    intGrade -= 1
                    If intGrade <= 0 Then intGrade = 1
                    grid.Texts2D(i, 1).Text = Space(intGrade * 2) & FormatAccno(strAccno) & nz(.Rows(intR).Item("accname"), "")
                    grid.Texts2D(i, 2).Text = Format(nz(.Rows(intR).Item("bg1"), 0) + nz(.Rows(intR).Item("bg2"), 0) + nz(.Rows(intR).Item("bg3"), 0) + nz(.Rows(intR).Item("bg4"), 0) + nz(.Rows(intR).Item("bg5"), 0), "###,###,###")
                    grid.Texts2D(i, 3).Text = Format(nz(.Rows(intR).Item("bg1"), 0), "###,###,###")
                    grid.Texts2D(i, 4).Text = Format(nz(.Rows(intR).Item("bg2"), 0), "###,###,###")
                    grid.Texts2D(i, 5).Text = Format(nz(.Rows(intR).Item("bg3"), 0), "###,###,###")
                    grid.Texts2D(i, 6).Text = Format(nz(.Rows(intR).Item("bg4"), 0), "###,###,###")
                    grid.Texts2D(i, 7).Text = Format(nz(.Rows(intR).Item("bg5"), 0), "###,###,###")
                Next
            End With

            'grid.Texts2D(1, 2).SetTextColor(Color.Blue) '設定文字顏色
            'grid.Texts2D(1, 2).HAlignment = FPAlignment.Near '設定水平對齊=靠左 (預設是置中對齊)
            'grid.Texts2D(1, 2).VAlignment = FPAlignment.Far '設定垂直對齊=靠下 (預設是置中對齊)

            Dim text1 As New FPText("製表" & Space(32) & "股長" & Space(32) & "組室主管" & Space(32) & "主計主任" & Space(32) & "總幹事" & Space(32) & "會長", 15, 195)
            text1.Font.Size = 11   '改變文字大小為12點
            page.Add(text1)

            '加入要列印的文字到列印頁面中
            page.Add(text)
            '加入要列印的表格到列印頁面中
            page.Add(grid)

            '加入要列印的頁面到列印文件中
            document.AddPage(page)
        Next


        '將要列印的文件送到印表機
        printer.Document = document

        '指定印表機名稱,沒指定的話就會從預設印表機印出
        'printer.PrinterName = ""

        '指定列印模式,共有四種,沒指定的話預設是NormalPrint一般列印,FitPrint是套表列印
        'PrintToPDFPrinter是要列印到能產生PDF文件的印表機, 關於PDF請參考WebForm範例
        'PrintToRemotingPDFPrinter 是要讓Client端沒安裝PDF Printer也能產生PDF文件,但目前此功能還沒完成
        printer.PrintMode = PrintMode.NormalPrint

        '以下三種對話盒可以全部顯示也可以都不要顯示就會直接從印表機印出
        '自動顯示頁面設定對話盒
        'printer.IsAutoShowPageSetupDialog = True
        '自動顯示印表機設定對話盒
        'printer.IsAutoShowPrintDialog = True
        '自動顯示預覽列印對話盒
        printer.IsAutoShowPrintPreviewDialog = True

        '開始列印
        printer.Print()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class
