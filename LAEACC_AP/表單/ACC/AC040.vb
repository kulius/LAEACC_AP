Public Class AC040
    Dim sYear, sNo As Integer    '年度 & 制製票號
    'Dim sFile, sKind As String    '資料來源檔  & 傳票種類
    Dim mydsS As DataSet
    Dim LoadAfter, Dirty As Boolean
    Dim sqlstr As String
    Dim bmS As BindingManagerBase, strAccno4 As String
    Dim myDatasetS, mydataset As DataSet
    Private Sub AC040_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Dim myds As DataSet
        LoadAfter = True
        sYear = GetYear(TransPara.TransP("UserDate"))
        lblYear.Text = sYear
        dtpDate.Value = TransPara.TransP("UserDate")
        lblUseNO.Text = QueryNO(sYear, "6")
        Call LoadGridFunc()
    End Sub

    'Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
    '    sYear = nudYear.Value
    '    lblUseNO.Text = QueryNO(sYear, "6")
    '    TabControl1.Visible = True
    '    If Year(dtpDate.Value) > sYear Then
    '        dtpDate.Value = sYear.ToString & "/12/31"
    '    End If
    '    Call LoadGridFunc()
    'End Sub

    Sub LoadGridFunc()
        '將acf010->no_2_no=0置入source datagrid 
        Dim sqlstr, qstr, sortstr As String
        sqlstr = "SELECT * from acf010 where kind='3' and no_2_no=0 and seq='1' and item='1' and accyear= " & sYear
        myDatasetS = openmember(DNS_ACC, "ac010s", sqlstr)
        dtgSource.DataSource = myDatasetS
        dtgSource.DataMember = "ac010s"
        bmS = Me.BindingContext(myDatasetS, "ac010s")
        TabControl1.SelectedIndex = 0
    End Sub


    Private Sub dtgSource_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtgSource.DoubleClick
        lblNo_1_no.Text = ""
        sNo = bmS.Current("no_1_no")
        If sNo <> 0 Then
            Call put_data()
        End If
    End Sub

    Sub put_data()
        Dim SumAmt As Decimal = 0
        Dim intI As Integer
        Dim strI, sqlstr As String
        Dim tempdataset As DataSet
        Call clsScreen()
        sqlstr = "SELECT a.accyear, a.kind, a.no_1_no, a.seq, a.item, a.accno, a.amt, a.remark, a.date_1 " & _
                 "FROM ACF010 a WHERE a.kind = '3' AND a.seq='1' and a.dc='1' and a.accyear=" & sYear & " and a.no_1_no = " & sNo & _
                 " UNION " & _
                 "(SELECT b.accyear, b.kind, b.no_1_no, b.seq, b.item, b.accno, b.amt, b.remark, c.date_1 " & _
                 " FROM  acf020 b left outer join acf010 c on c.accyear=b.accyear and c.kind=b.kind and c.no_1_no=b.no_1_no " & _
                 "WHERE  b.kind='3' AND  b.seq='1' and b.dc='1' and b.accyear=" & sYear & " and b.NO_1_NO = " & sNo & _
                 ") ORDER BY kind, seq, item"
        tempdataset = openmember(DNS_ACC, "acf010", sqlstr)   '只將借方傳票第一張置入螢幕
        For intI = 0 To tempdataset.Tables("acf010").Rows.Count - 1
            If intI = 0 Then lblNo_1_no.Text = tempdataset.Tables("acf010").Rows(intI).Item("no_1_no")
            If intI = 0 Then lbldate_1.Text = tempdataset.Tables("acf010").Rows(intI).Item("date_1")
            strI = CType(intI + 1, String)
            FindControl(Me, "txtRemark" & strI).Text = tempdataset.Tables("acf010").Rows(intI).Item("remark")
            FindControl(Me, "txtAmt" & strI).Text = FormatNumber(tempdataset.Tables("acf010").Rows(intI).Item("amt"), 2)
            FindControl(Me, "vxtaccno" & strI).Text = tempdataset.Tables("acf010").Rows(intI).Item("accno")
        Next
        tempdataset = Nothing
        lblNo_2_no.Text = QueryNO(sYear, "6") + 1

        TabControl1.SelectedIndex = 1
    End Sub

    '決裁確定
    Private Sub btnFinish_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFinish.Click
        If lblNo_1_no.Text = "" Or sNo = 0 Then Exit Sub
        If GetYear(dtpDate.Value) <> sYear Then
            MsgBox("決裁日期與傳票年度不同")
            Exit Sub
        End If
        Dim sqlstr, retstr, updsqlvalue As String
        Dim intNo As Integer
        intNo = RequireNO(mastconn, sYear, "6")     '轉帳決裁編號控制kind="6"
        sqlstr = "UPDATE acf010 SET date_2='" & FullDate(dtpDate.Value) & "', no_2_no = " & intNo & " WHERE no_1_no = " & sNo & " And accyear = " & sYear & " And kind >= '3'"
        retstr = runsql(mastconn, sqlstr)
        If retstr = "sqlerror" Then MsgBox("轉帳決裁有誤acf010 " & sNo)
        sqlstr = "UPDATE acf020 SET no_2_no = " & intNo & " WHERE no_1_no = " & sNo & " And accyear = " & sYear & " And kind >= '3'"
        retstr = runsql(mastconn, sqlstr)
        If retstr = "sqlerror" Then MsgBox("轉帳決裁有誤acf020 " & sNo)

        '並清空傳票來源
        Dim intAutono As Integer
        myDatasetS.Tables("ac010s").Rows.RemoveAt(bmS.Position.ToString)  '將source grid刪行

        Call clsScreen()
        lblUseNO.Text = Str(intNo)        '顯示實際使用編號
        TabControl1.SelectedIndex = 0     '回datagrid PAGE 1 

    End Sub

    Private Sub clsScreen()    '清傳票螢幕
        Dim intI As Integer
        Dim strI As String
        For intI = 1 To 6
            strI = CType(intI, String)
            FindControl(Me, "vxtaccno" & strI).Text = ""
            If intI > 1 Then FindControl(Me, "txtcode" & strI).Text = ""
            FindControl(Me, "txtremark" & strI).Text = ""
            FindControl(Me, "txtAmt" & strI).Text = ""
        Next
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        TabControl1.SelectedIndex = 0
    End Sub
End Class
