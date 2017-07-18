Public Class BG999
    Dim confid, confunit As String, LoadAfter, Dirty As Boolean
    Dim sqlstr As String
    Dim bm As BindingManagerBase, myDataSet As DataSet
    Dim UserId, UserName, UserUnit, UserDate As String
    Dim lastpos As Integer

    Private Sub BG999_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        UserId = TransPara.TransP("userid")
        LoadAfter = True
        TabControl1.Enabled = True
        Call LoadGridFunc()
        If DataGrid1.CurrentRowIndex > 0 Then
            bm.Position = 1
            Call PutGridToTxt()
        End If
    End Sub

    Sub LoadGridFunc()
        Dim sqlstr, qstr, strD, strC As String
        sqlstr = "SELECT a.ACCYEAR, a.ACCNO, a.unit, (a.bg1+a.bg2+a.bg3+a.bg4+a.bg5+a.up1+a.up2+a.up3+a.up4) as BGSUM, a.totper, a.totuse, " & _
                 " a.flow, a.ENGNO, (c.accname + '-' + b.ACCNAME) as accname FROM (BGF010 a left JOIN ACCNAME b ON" & _
                 " a.ACCNO = b.ACCNO) LEFT JOIN accname c ON left(a.accno,16)=c.accno WHERE b.STAFF_NO = '" & Trim(UserId) & _
                 "' AND a.CTRL <> 'Y' ORDER BY a.ACCYEAR, a.ACCNO"
        myDataSet = openmember("", "BGF010", sqlstr)
        DataGrid1.DataSource = myDataSet
        DataGrid1.DataMember = "BGF010"
        bm = Me.BindingContext(myDataSet, "BGF010")
        TabControl1.SelectedIndex = 0
    End Sub

    Sub PutGridToTxt()
        Dim intI, SumUp As Integer
        Dim strI, strColumn1, strColumn2 As String
        If bm.Position = -1 Then Exit Sub
        If IsDBNull(bm.Current("accno")) Then Exit Sub
        lastpos = bm.Position
        lblAccyear.Text = bm.Current("accyear")
        lblAccno.Text = bm.Current("accno")
        lblAccname.Text = IIf(IsDBNull(bm.Current("accname")), " ", bm.Current("accname"))
        lblUnit.Text = nz(bm.Current("UNIT"), " ")
        lblBgsum.Text = Format(nz(bm.Current("BGSUM"), 0), "###,###,###.#")
        lblTotper.Text = Format(nz(bm.Current("TOTPER"), 0), "###,###,###.#")
        lblTotuse.Text = Format(nz(bm.Current("TOTUSE"), 0), "###,###,###.#")
        lblFlow.Text = nz(bm.Current("FLOW"), " ")
        lblEngno.Text = nz(bm.Current("ENGNO"), " ")
        lblUnUseAmt.Text = Format(Val(bm.Current("bgsum")) - Val(bm.Current("totper")) - Val(bm.Current("totuse")), "###,###,###.#")
    End Sub


    Private Sub TabControl1_SELECTEDIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        If LoadAfter = False Then Exit Sub
        ' If TabControl1.SelectedIndex = 1 Then Dirty = False
        If Dirty = True Then
            If MsgBox("資料尚未存檔 要放棄嗎?", MsgBoxStyle.DefaultButton1 + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                TabControl1.SelectedIndex = 1
                Dirty = False
            End If
        End If
    End Sub

    Private Sub DataGrid1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataGrid1.DoubleClick
        Call PutGridToTxt()
        TabControl1.SelectedIndex = 1  'go to the detail screen 
        Dirty = False
    End Sub


    Private Sub btnSure_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSure.Click
        Dim sqlstr, retstr, updstr, strAmt As String
        Dim tamt As Decimal
        If MsgBox("確定此科目不再開支,要辦理決算嗎? 是 / 否 ", MsgBoxStyle.DefaultButton1 + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
            Exit Sub
        End If

        sqlstr = "update BGF010 set ctrl = 'Y' where ACCNO='" & lblAccno.Text & "' and accyear=" & lblAccyear.Text
        retstr = runsql(mastconn, sqlstr)
        If retstr = "sqlok" Then
            myDataSet.Tables("BGF010").Rows.RemoveAt(lastpos)
        Else
            MsgBox("更新BGF010失敗" & retstr)
        End If
        TabControl1.SelectedIndex = 0  '回清單
    End Sub


    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        TabControl1.SelectedIndex = 0  '退回
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub StatusBar1_PanelClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.StatusBarPanelClickEventArgs) Handles StatusBar1.PanelClick

    End Sub
End Class
