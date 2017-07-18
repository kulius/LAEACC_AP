Imports JBC.Printing
Public Class CHF040
    Dim confid, confunit As String, LoadAfter, Dirty As Boolean
    Dim sqlstr As String
    Dim bm As BindingManagerBase, mydataset, PsDataSet As DataSet
    Dim UserId, UserName, UserUnit As String
    Dim intI As Integer

    Private Sub CHF040_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dtpDateS.Value = Year(TransPara.TransP("UserDATE")) & "/1/1"

        '將所有受款人置combobox   
        sqlstr = "SELECT psstr FROM psname where unit='0409' order by psstr"
        PsDataSet = openmember("", "psname", sqlstr)
        If PsDataSet.Tables("psname").Rows.Count = 0 Then
            cboName.Text = "尚無片語"
        Else
            cboName.DataSource = PsDataSet.Tables("psname")
            cboName.DisplayMember = "psstr"  '顯示欄位
            cboName.ValueMember = "psstr"     '欄位值
            cboName.SelectionLength = 60
        End If
        Call LoadGridFunc()
    End Sub

    Private Sub cboName_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboName.SelectionChangeCommitted
        txtName.Text = cboName.Text
    End Sub

    Private Sub cboName_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cboName.MouseDown
        intI = cboName.FindString(Trim(txtName.Text))
        cboName.SelectedIndex = intI         '將受款人置combo
    End Sub

    Private Sub BtnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSearch.Click
        Call LoadGridFunc()
        If DataGrid1.CurrentRowIndex > 0 Then Call PutGridToTxt()
    End Sub

    Sub LoadGridFunc()
        Dim sqlstr, qstr As String
        sqlstr = "SELECT * FROM  CHF040 where rdate >='" & FullDate(dtpDateS.Value) & "' order by rdate desc"

        mydataset = openmember("", "CHF040", sqlstr)

        RecMove1.FilesPara = "mastconn=" & mastconn & "|sqlstr=" & sqlstr
        DataGrid1.DataSource = mydataset
        DataGrid1.DataMember = "CHF040"
        bm = Me.BindingContext(mydataset, "CHF040")
        RecMove1.Setds = bm
        TabControl1.SelectedIndex = 0
    End Sub

    Sub PutGridToTxt()
        Dim intI As Integer
        Dim strI, subj, cont1 As String
        If IsDBNull(bm.Current("rdate")) Then Exit Sub
        lblkey.Text = bm.Current("autono")
        dtpRDate.Value = bm.Current("rdate")
        txtName.Text = nz(bm.Current("name"), "  ")
        txtAmt.Text = FormatNumber(nz(bm.Current("amt"), 0), 0)
        txtBecause.Text = nz(bm.Current("because"), "")
        txtRemark.Text = nz(bm.Current("remark"), " ")
    End Sub

    Public Sub transjob(ByVal JobName As String, ByVal JobPara As String) Handles RecMove1.TransJob
        Dim LastPos As Integer = bm.Position
        Select Case JobName
            Case "刪除記錄"
                Dim keyvalue, sqlstr, retstr As String
                If TabControl1.SelectedIndex = 0 Then
                    keyvalue = bm.Current("autono")
                Else
                    keyvalue = Trim(lblkey.Text)
                End If

                sqlstr = "delete from CHF040 where autono=" & keyvalue
                retstr = runsql(mastconn, sqlstr)
                If retstr = "sqlok" Then
                    mydataset.Tables("CHF040").Rows.RemoveAt(JobPara)
                    mydataset.Tables("CHF040").Clear()
                    Call LoadGridFunc()
                    bm.Position = LastPos
                End If
                TabControl1.SelectedIndex = 0

            Case "回寫記錄cmd"
                Dim keyvalue, sqlstr, retstr, updstr As String
                If TabControl1.SelectedIndex = 0 Then
                    Call PutGridToTxt()
                End If
                keyvalue = lblkey.Text
                RecMove1.GenUpdsql("rdate", dtpRDate.Value.ToShortDateString, "D")
                RecMove1.GenUpdsql("name", txtName.Text, "U")
                RecMove1.GenUpdsql("because", txtBecause.Text, "U")
                RecMove1.GenUpdsql("remark", txtRemark.Text, "U")
                RecMove1.GenUpdsql("amt", txtAmt.Text, "N")
                sqlstr = "update CHF040 set " & RecMove1.genupdfunc & " where autono=" & keyvalue
                retstr = runsql(mastconn, sqlstr)
                If retstr = "sqlok" Then
                    mydataset.Tables("CHF040").Rows.RemoveAt(JobPara)
                    mydataset.Tables("CHF040").Clear()
                    Call LoadGridFunc()
                    bm.Position = LastPos
                    Call Print_receive()
                End If
                TabControl1.SelectedIndex = 0


            Case "新增記錄"
                Dim keyvalue, sqlstr, retstr, updstr As String
                If TabControl1.SelectedIndex = 0 Then
                    Call PutGridToTxt()
                End If
                RecMove1.GenInsSql("rdate", dtpRDate.Value.ToShortDateString, "D")
                RecMove1.GenInsSql("name", txtName.Text, "U")
                RecMove1.GenInsSql("because", txtBecause.Text, "U")
                RecMove1.GenInsSql("remark", txtRemark.Text, "U")
                RecMove1.GenInsSql("amt", txtAmt.Text, "N")
                sqlstr = "insert into CHF040 " & RecMove1.GenInsFunc
                retstr = runsql(mastconn, sqlstr)

                If retstr = "sqlok" Then
                    mydataset.Tables("CHF040").Rows.RemoveAt(JobPara)
                    mydataset.Tables("CHF040").Clear()
                    Call LoadGridFunc()
                    bm.Position = LastPos
                    Call Print_receive()
                Else
                    MsgBox("新增失敗" & sqlstr)
                End If
                TabControl1.SelectedIndex = 0

            Case "記錄移動"
                Dim keyvalue, sqlstr, retstr As String
                lblkey.Text = bm.Current("autono")
                Call PutGridToTxt()
                Dirty = False
        End Select
    End Sub


    Private Sub TabControl1_SELECTEDIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        If LoadAfter = False Then Exit Sub
        ' If TabControl1.SelectedIndex = 1 Then Dirty = False
        If Dirty = True Then
            If MsgBox("資料尚未存檔 要放棄嗎?", MsgBoxStyle.DefaultButton1 + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                TabControl1.SelectedIndex = 1
                Dirty = False
            End If
        End If
    End Sub


    Private Sub DataGrid1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataGrid1.Click
        lblkey.Text = bm.Current("autono")  'keep the old keyvalue
        Call PutGridToTxt()
        Dirty = False
    End Sub

    Private Sub DataGrid1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataGrid1.DoubleClick
        lblkey.Text = bm.Current("autono")  'keep the old keyvalue
        Call PutGridToTxt()
        Dirty = False
        TabControl1.SelectedIndex = 1
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnAddPsname_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddPsname.Click
        If txtName.Text <> "" Then
            txtName.Text = Trim(txtName.Text)
            Dim ii As Integer
            ii = MsgBox("將 " & txtName.Text & "增入片語檔", MsgBoxStyle.OkCancel)
            If ii = 1 Then  ' click the ok botton
                sqlstr = "insert into psname (unit, seq, psstr) values ('0409', 0, N'" & txtName.Text & "')"
                runsql(mastconn, sqlstr)
            End If
        End If
    End Sub

    Private Sub Print_receive()
        Dim printer As FPPrinter = FPPrinter.SharedPrinter
        Dim document As New FPDocument("列印收據")
        '102/5/7修改彰化收據格式
        If TransPara.TransP("UnitTitle").indexof("彰化") > 0 Then
            document.SetDefaultPageMargin(0, 34, 10, 10)    'left,top,right,botton
        Else
            document.SetDefaultPageMargin(0, 38, 10, 10) 'left,top,right,botton
        End If
        document.DefaultPageSettings.PaperKind = Printing.PaperKind.A3   '420x297
        document.DefaultPageSettings.Landscape = False    '橫印  true

        document.DefaultFont = New Font("新細明體", 11) '標楷體
        Dim Amt As Integer = ValComa(txtAmt.Text)   '收據金額
        Dim topY As Integer = 0

        Dim page As New FPPage

        '日期
        '102/5/7修改彰化收據格式
        Dim textday As FPText
        Dim posX As Integer = 99
        If TransPara.TransP("UnitTitle").indexof("彰化") > 0 Then
            posX = 97
        End If
        textday = New FPText(FormatNumber(GetYear(dtpRDate.Value), 0) & Space(11) & _
           FormatNumber(Month(dtpRDate.Value), 0) & Space(11) & _
           Microsoft.VisualBasic.DateAndTime.Day(dtpRDate.Value), posX, topY)

        '102/5/7修改結束


        textday.Font.Size = 11
        page.Add(textday)

        '表身
        topY += 18
        Dim grid As New FPTable(0, topY, 222, 30, 1, 5) '新增表格物件,要列印在座標 (0,56),寬度222,高度30,共有1row列5欄,(單位是公厘)
        grid.Font.Size = 11
        grid.AllLineHide()  '(所有格線隱藏)
        grid.ColumnStyles(1).HAlignment = StringAlignment.Near
        grid.ColumnStyles(3).HAlignment = StringAlignment.Center
        grid.ColumnStyles(4).HAlignment = StringAlignment.Near
        grid.ColumnStyles(5).HAlignment = StringAlignment.Near
        grid.ColumnStyles(1).Width = 45 '繳款人
        grid.ColumnStyles(2).Width = 27 '收入科目
        grid.ColumnStyles(3).Width = 40 '金額
        grid.ColumnStyles(4).Width = 47 '事由
        grid.ColumnStyles(5).Width = 60 '備註
        grid.Texts2D(1, 1).Text = txtName.Text
        grid.Texts2D(1, 3).Text = "$" & FormatNumber(Amt, 0)
        grid.Texts2D(1, 4).Text = txtBecause.Text
        grid.Texts2D(1, 5).Text = txtRemark.Text
        page.Add(grid)   '加入要列印的表格到列印頁面中

        '列印收據金額(中文)
        topY += 33
        Dim aText As New FPText(GetChineseMoney(Amt) & "元整", 59, topY)
        aText.Font.Size = 14
        page.AddText(aText)

        document.AddPage(page)  '加入要列印的頁面到列印文件中
        printer.Document = document

        printer.PrintMode = PrintMode.NormalPrint
        If rdoPreview.Checked Then printer.IsAutoShowPrintPreviewDialog = True '自動顯示預覽列印對話盒
        printer.Print()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Call Print_receive()
    End Sub

    Private Sub txtAmt_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAmt.LostFocus
        If (Not IsNumeric(sender.text) Or ValComa(sender.text) < 0) And Trim(sender.text) <> "" Then
            MsgBox("請輸入正數字")
            sender.focus()
        Else
            sender.text = FormatNumber(ValComa(sender.text), 0)
        End If
    End Sub
End Class
