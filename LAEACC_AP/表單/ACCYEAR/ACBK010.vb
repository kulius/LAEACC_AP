Imports JBC.Printing
Public Class ACBK010
    Dim confid, confunit As String, LoadAfter, Dirty As Boolean
    Dim sqlstr As String
    Dim strAccno As String
    Dim SYear As Integer
    Dim SDate, EDate As String
    'Dim strAccount, strBankname As String   '銀行名稱
    Dim mydataset, myds, mydsD As DataSet
    Dim PageRow As Integer = 37  '每頁印37行

    Private Sub ACBK010_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '找過帳日期
        Dim sqlstr As String
        sqlstr = "SELECT max(date_2) as date_2  from acf010 where books='Y'"
        myds = openmember("", "acf010s", sqlstr)
        If myds.Tables("acf010s").Rows.Count > 0 Then
            nudEmonth.Value = Month(myds.Tables("acf010s").Rows(0).Item("date_2"))
            nudSmonth.Value = nudEmonth.Value
            nudSyear.Value = GetYear(myds.Tables("acf010s").Rows(0).Item("date_2"))
            nudEyear.Value = nudSyear.Value
        End If
        vxtSAccno.Text = "1"    '起值
        vxtEAccno.Text = "59"   '迄值
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
        BtnPrint.Enabled = False
        Dim intR As Integer = 0  'control record number
        Dim intI, intD, i As Integer
        Dim AcuDeamt, AcuCramt As Decimal
        Dim MonDeamt, MonCramt As Decimal
        Dim Deamt, Cramt As Decimal
        Dim retstr, strRemark As String
        Dim strName4 As String
        txtNo.Text = Val(txtNo.Text) - 1
        SYear = nudSyear.Value
        SDate = FullDate(nudSyear.Value & "/" & nudSmonth.Value & "/1")   '起印日
        EDate = FullDate(nudEyear.Value & "/" & nudEmonth.Value & "/" & DateTime.DaysInMonth(nudEyear.Value + 1911, nudEmonth.Value))  '訖印日
        Dim up As String = Format(nudSmonth.Value - 1, "00")  '上月
        Dim Saccno As String = GetAccno(vxtSAccno.Text)  '起印科目
        Dim Eaccno As String = GetAccno(vxtEAccno.Text)  '訖印科目
        Dim tempMonth As Integer = 0
        Dim strEnd As String = "N"  '控制列印結轉分錄
        Dim intEndNo As Integer = 0
        If nudEmonth.Value = 12 Then
            If MsgBox("是否要列印結轉下年度 YES/NO", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                strEnd = "Y"
                intEndNo = QueryNO(SYear, "6") + 1  '結轉分錄轉帳編號
            End If
        End If

        Dim printer = New KPrint
        Dim document As New FPDocument("列印總分類帳")
        document.DefaultPageSettings.PaperKind = Printing.PaperKind.B4   'A3:420x297  B4:364X257
        document.DefaultPageSettings.Landscape = True    '橫印
        document.SetDefaultPageMargin(30, 5, 10, 10)    'left,top,right,botton
        document.DefaultFont = New Font("新細明體", 11) '標楷體

        '以下是符合頁碼範圍所要用到的物件變數
        Dim textUnit As FPText
        Dim textTitle As FPText
        Dim textyear As FPText
        Dim textPage As FPText
        Dim textName4 As FPText
        Dim grid As FPTable
        '以下是不在頁碼範圍內要用到的物件變數
        Dim ttextUnit As New FPText
        Dim ttextTitle As New FPText
        Dim ttextyear As New FPText
        Dim ttextPage As New FPText
        Dim ttextName4 As New FPText
        Dim ggrid As New FPTable(0, 17, 314, 222, PageRow, 10)
        '宣告一變數儲存是否符合頁碼範圍
        Dim blnIsBelong As Boolean
        Dim blnIsPrint As Boolean = True  '記錄是否有資料頁列印


        '由acf050逐科目列印 取4級科目
        Dim sqlstr2 As String
        If up = "00" Then
            sqlstr2 = " a.beg_debit as deamt, a.beg_credit as cramt"
        Else
            sqlstr2 = " a.deamt" & up & " as deamt, a.cramt" & up & " as cramt"
        End If
        sqlstr = "select b.accname as accname, a.accno, " & sqlstr2 & " FROM ACF050 a " & _
                 " left outer join accname b on a.accno=b.accno " & _
                 " where len(a.accno)=5 and a.accyear=" & SYear & _
                 " and a.accno BETWEEN '" & Saccno & "' AND '" & Eaccno & "'" & _
                 " ORDER BY a.accno"
        mydsD = openmember("", "DataAccno", sqlstr)
        For intD = 0 To mydsD.Tables("DataAccno").Rows.Count - 1
            If rdbPage.Checked Then  '頁次大於控制列印範圍時就結束
                If Val(txtNo.Text) > Val(txtPrintPageE.Text) Then Exit For
            End If

            strAccno = nz(mydsD.Tables("DataAccno").Rows(intD).Item("accno"), "") '列印科目
            strName4 = nz(mydsD.Tables("DataAccno").Rows(intD).Item("accname"), "") '列印科目
            '上月累計or上年度轉入
            AcuDeamt = nz(mydsD.Tables("DataAccno").Rows(intD).Item("deamt"), 0)
            AcuCramt = nz(mydsD.Tables("DataAccno").Rows(intD).Item("cramt"), 0)
            If nudSmonth.Value = 1 Then
                MonDeamt = AcuDeamt
                MonCramt = AcuCramt
            Else
                MonDeamt = 0 : MonCramt = 0
            End If
            intR = 0

            '找acf070日計檔資料(table body record )
            sqlstr = "SELECT * FROM acf070 " & _
                     " where date_2 between '" & SDate & "' and '" & EDate & "' and accno='" & strAccno & _
                     "' order by date_2"
            mydataset = openmember("", "acf070", sqlstr)

            If mydataset.Tables("acf070").Rows.Count > 0 Then
                tempMonth = Month(mydataset.Tables("acf070").Rows(0).Item("date_2"))  '記錄第一筆之月份
            Else   '沒有資料
                If Not (nudSmonth.Value = 1 And AcuDeamt + AcuCramt <> 0) Then  '有上年度轉入數
                    GoTo NextFor
                End If
            End If
            If nudSmonth.Value = 1 Then tempMonth = 1 '起印=1月時 

            Dim page As FPPage
            For PageCnt As Integer = 1 To 999    '頁次
                txtNo.Text = Val(txtNo.Text) + 1
                If rdbPage.Checked Then  '頁次大於控制列印範圍時就結束
                    If Val(txtNo.Text) > Val(txtPrintPageE.Text) Then Exit For
                End If
                blnIsBelong = (rdbAll.Checked = True Or (rdbAll.Checked = False And (Val(txtNo.Text) >= Val(txtPrintPageS.Text) And Val(txtNo.Text) <= Val(txtPrintPageE.Text))))

                '控制列印範圍內的才new
                If blnIsBelong Then
                    blnIsPrint = True
                    page = New FPPage
                    textUnit = New FPText(TransPara.TransP("UnitTitle"), 90, 0)
                    textUnit.HAlignment = FPAlignment.Center
                    textUnit.Font.Size = 14   '改變文字大小為14點
                    textTitle = New FPText("總 分 類 帳", 90, 6)
                    textTitle.HAlignment = FPAlignment.Center
                    textTitle.Font.Size = 14
                    textyear = New FPText("中華民國" & SYear & "年度", 90, 12)
                    textyear.HAlignment = FPAlignment.Center
                    textyear.Font.Size = 11
                    textPage = New FPText("第 " & txtNo.Text & " 頁", 300, 12)
                    textName4 = New FPText("科目：" & FormatAccno(strAccno) & "  " & strName4, 0, 12)
                    grid = New FPTable(0, 17, 324, 222, PageRow, 7)   '新增表格物件x,y,w,h,row,col (單位是公厘)
                    grid.Font.Size = 11
                    grid.ColumnStyles(2).HAlignment = StringAlignment.Near
                    grid.ColumnStyles(3).HAlignment = StringAlignment.Far
                    grid.ColumnStyles(4).HAlignment = StringAlignment.Far
                    grid.ColumnStyles(5).HAlignment = StringAlignment.Center
                    grid.ColumnStyles(6).HAlignment = StringAlignment.Far

                    grid.SetLineColor(Color.Blue)   'table line color 
                    grid.ColumnStyles(1).Width = 30    'tot364
                    grid.ColumnStyles(2).Width = 100
                    grid.ColumnStyles(3).Width = 45
                    grid.ColumnStyles(4).Width = 45
                    grid.ColumnStyles(5).Width = 15
                    grid.ColumnStyles(6).Width = 45
                    grid.ColumnStyles(7).Width = 44

                    grid.Texts2D(1, 1).Text = "傳 票 日 期"
                    grid.Texts2D(1, 2).Text = "　　　　　摘　　　　　　　要"
                    grid.Texts2D(1, 3).Text = "借      方  ."
                    grid.Texts2D(1, 4).Text = "貸      方  ."
                    grid.Texts2D(1, 5).Text = "借或貸"
                    grid.Texts2D(1, 6).Text = "餘　　　額　."
                    grid.Texts2D(1, 7).Text = "備      註"
                Else
                    textUnit = ttextUnit
                    textTitle = ttextTitle
                    textyear = ttextyear
                    textPage = ttextPage
                    textName4 = ttextName4
                    grid = ggrid
                End If

                i = 1
                If intR = 0 Then   '上月累計
                    If AcuDeamt <> 0 Or AcuCramt <> 0 Then
                        i += 1
                        If nudSmonth.Value = 1 Then
                            grid.Texts2D(i, 2).Text = "　　上年度轉入"
                            grid.Texts2D(i, 1).Text = ShortDate(SDate)
                        Else
                            grid.Texts2D(i, 2).Text = "　　上月累計"
                        End If
                        grid.Texts2D(i, 3).Text = FormatNumber(AcuDeamt, 2)
                        grid.Texts2D(i, 4).Text = FormatNumber(AcuCramt, 2)
                        grid.Texts2D(i, 5).Text = IIf(AcuDeamt > AcuCramt, "借", "貸")
                        If AcuDeamt = AcuCramt Then grid.Texts2D(i, 5).Text = "平"
                        grid.Texts2D(i, 6).Text = FormatNumber(Math.Abs(AcuDeamt - AcuCramt), 2)
                    End If
                End If

                With mydataset.Tables("acf070")
                    Do While i < PageRow
                        If intR > .Rows.Count - 1 Then
                            If MonDeamt + MonCramt <> 0 Then
                                i += 1
                                If i > PageRow Then Exit Do
                                grid.Texts2D(i, 2).Text = "　　　　本　　月　　合　　計"
                                grid.Texts2D(i, 3).Text = FormatNumber(MonDeamt, 2)
                                grid.Texts2D(i, 4).Text = FormatNumber(MonCramt, 2)
                                MonDeamt = 0 : MonCramt = 0
                            End If
                            If AcuDeamt + AcuCramt <> 0 Then
                                i += 1
                                If i > PageRow Then Exit Do
                                grid.Texts2D(i, 2).Text = "　　　　本　　月　　累　　計"
                                grid.Texts2D(i, 3).Text = FormatNumber(AcuDeamt, 2)
                                grid.Texts2D(i, 4).Text = FormatNumber(AcuCramt, 2)
                                grid.Texts2D(i, 5).Text = IIf(AcuDeamt > AcuCramt, "借", "貸")
                                If AcuDeamt = AcuCramt Then grid.Texts2D(i, 5).Text = "平"
                                grid.Texts2D(i, 6).Text = FormatNumber(Math.Abs(AcuDeamt - AcuCramt), 2)
                                If strEnd <> "Y" Or AcuDeamt = AcuCramt Then AcuDeamt = 0 : AcuCramt = 0
                            End If
                            If strEnd = "Y" And AcuDeamt <> AcuCramt Then
                                i += 1
                                If i > PageRow Then Exit Do
                                If Mid(strAccno, 1, 1) > "3" Then
                                    grid.Texts2D(i, 2).Text = "　　　　結轉本期餘絀"
                                Else
                                    grid.Texts2D(i, 2).Text = "　　　　結轉下年度"
                                End If
                                grid.Texts2D(i, 1).Text = ShortDate(EDate)
                                If AcuDeamt > AcuCramt Then
                                    grid.Texts2D(i, 4).Text = FormatNumber(AcuDeamt - AcuCramt, 2)
                                Else
                                    grid.Texts2D(i, 3).Text = FormatNumber(AcuCramt - AcuDeamt, 2)
                                End If
                                grid.Texts2D(i, 5).Text = "平"
                                grid.Texts2D(i, 6).Text = FormatNumber(0, 2)
                                AcuDeamt = 0 : AcuCramt = 0
                            End If
                            PageCnt = 1000
                            Exit Do
                            'Exit For
                        End If   'the end of record 

                        If Month(.Rows(intR).Item("date_2")) <> tempMonth Then  '日期不同時要印小計
                            If MonDeamt + MonCramt <> 0 Then
                                i += 1
                                If i > PageRow Then Exit Do
                                grid.Texts2D(i, 2).Text = "　　　　本　　月　　合　　計"
                                grid.Texts2D(i, 3).Text = FormatNumber(MonDeamt, 2)
                                grid.Texts2D(i, 4).Text = FormatNumber(MonCramt, 2)
                                MonDeamt = 0 : MonCramt = 0
                            End If
                            If AcuDeamt + AcuCramt <> 0 Then
                                i += 1
                                If i > PageRow Then Exit Do
                                grid.Texts2D(i, 2).Text = "　　　　本　　月　　累　　計"
                                grid.Texts2D(i, 3).Text = FormatNumber(AcuDeamt, 2)
                                grid.Texts2D(i, 4).Text = FormatNumber(AcuCramt, 2)
                                grid.Texts2D(i, 5).Text = IIf(AcuDeamt > AcuCramt, "借", "貸")
                                If AcuDeamt = AcuCramt Then grid.Texts2D(i, 5).Text = "平"
                                grid.Texts2D(i, 6).Text = FormatNumber(Math.Abs(AcuDeamt - AcuCramt), 2)
                            End If
                            tempMonth = Month(.Rows(intR).Item("date_2"))
                        End If

                        i += 1
                        If i > PageRow Then Exit Do '換新頁

                        grid.Texts2D(i, 1).Text = ShortDate(.Rows(intR).Item("date_2"))
                        grid.Texts2D(i, 2).Text = " 本日小計"
                        Deamt = nz(.Rows(intR).Item("deamt1"), 0) + nz(.Rows(intR).Item("deamt2"), 0) + nz(.Rows(intR).Item("deamt3"), 0)
                        Cramt = nz(.Rows(intR).Item("cramt1"), 0) + nz(.Rows(intR).Item("cramt2"), 0) + nz(.Rows(intR).Item("cramt3"), 0)
                        grid.Texts2D(i, 3).Text = FormatNumber(Deamt, 2)
                        grid.Texts2D(i, 4).Text = FormatNumber(Cramt, 2)
                        MonDeamt += Deamt
                        MonCramt += Cramt
                        AcuDeamt += Deamt
                        AcuCramt += Cramt
                        intR += 1
                    Loop
                End With

                '控制列印範圍內的才加入
                If blnIsBelong Then
                    page.Add(textUnit)       '加入要列印的文字到列印頁面中
                    page.Add(textTitle)
                    page.Add(textyear)
                    page.Add(textPage)
                    page.Add(textName4)
                    page.Add(grid)          '加入要列印的表格到列印頁面中
                    document.AddPage(page)  '加入要列印的頁面到列印文件中
                End If
            Next
NextFor: Next
        If document.PageCount > 0 Then
            printer.Document = document
            printer.PrintMode = PrintMode.NormalPrint
            printer.IsAutoShowPrintDialog = True '自動顯示印表機設定對話盒
            If rdoPreview.Checked Then printer.IsAutoShowPrintPreviewDialog = True '自動顯示預覽列印對話盒
            If blnIsPrint Then printer.Print() '開始列印
        End If
        Me.Close()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        mydataset = Nothing
        myds = Nothing
        Me.Close()
    End Sub

    Private Sub txtNo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNo.LostFocus, txtPrintPageS.LostFocus, txtPrintPageE.LostFocus
        If (Not IsNumeric(sender.Text) Or ValComa(sender.text) < 0) And Trim(sender.text) <> "" Then
            MsgBox("請輸入正數字")
            sender.Focus()
        End If
    End Sub
End Class
