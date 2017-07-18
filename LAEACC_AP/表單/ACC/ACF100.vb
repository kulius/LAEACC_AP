Public Class ACF100
    Dim confid, confunit As String, LoadAfter, Dirty As Boolean
    Dim sqlstr As String
    Dim bm As BindingManagerBase, mydataset As DataSet
    Dim UserId, UserName, UserUnit As String
    Private Sub ACf100_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TabControl1.Enabled = False
        RecMove1.Enabled = False
        nudYear.Value = GetYear(Now)
        nudMM.Value = Month(Now)
        vxtStartNo.Text = "11201"    '起值
        vxtEndNo.Text = "136019"      '迄值
    End Sub

    Private Sub BtnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSearch.Click
        TabControl1.Enabled = True
        RecMove1.Enabled = True
        Call LoadGridFunc()
        If DataGrid1.CurrentRowIndex > 0 Then
            bm.Position = 1
            Call PutGridToTxt()
        End If
    End Sub

    Sub LoadGridFunc()
        Dim sqlstr, qstr, strD, strC As String
        strD = "QTYIN" & Format(nudMM.Value, "00")
        strC = "QTYOUT" & Format(nudMM.Value, "00")
        sqlstr = "SELECT a.*, a." & strD & " - a." & strC & " as netamt, b.accname  FROM  ACf100 a LEFT OUTER JOIN accname b" & _
                 " ON a.accno = b.accno WHERE accyear=" & nudYear.Value & " and a.accno>='" & _
                 GetAccno(vxtStartNo.Text) & "' and a.accno<='" & GetAccno(vxtEndNo.Text) & "' ORDER BY a.accyear, a.accno"
        mydataset = openmember("", "ACf100", sqlstr)
        RecMove1.FilesPara = "mastconn=" & mastconn & "|sqlstr=" & sqlstr
        DataGrid1.DataSource = mydataset
        DataGrid1.DataMember = "ACf100"
        bm = Me.BindingContext(mydataset, "ACf100")
        RecMove1.Setds = bm
        TabControl1.SelectedIndex = 0
    End Sub

    Sub PutGridToTxt()
        Dim intI As Integer
        Dim strI As String
        txtAccYear.Text = bm.Current("accyear")
        vxtAccno.Text = bm.Current("accno")
        lblAccname.Text = IIf(IsDBNull(bm.Current("accname")), " ", bm.Current("accname"))
        txtQty_Beg.Text = Format(bm.Current("qty_beg"), "###,###,###,###.######")
        txtKind.Text = nz(bm.Current("kind"), "")
        txtUnit.Text = nz(bm.Current("unit"), "")

        For intI = 1 To 12
            strI = Format(intI, "00")
            FindControl(Me, "txtqtyin" & strI).Text = Format(bm.Current("qtyin" & strI), "###,###,###,###.######")
            FindControl(Me, "txtqtyout" & strI).Text = Format(bm.Current("qtyout" & strI), "###,###,###,###.######")
            FindControl(Me, "lblNet" & strI).Text = Format(bm.Current("qtyin" & strI) - bm.Current("qtyout" & strI), "###,###,###,###.######")
        Next
    End Sub

    Sub transjob(ByVal JobName As String, ByVal JobPara As String) Handles RecMove1.TransJob
        Dim LastPos As Integer = bm.Position
        Select Case JobName
            Case "刪除記錄"
                Dim keyvalue, sqlstr, retstr As String
                If TabControl1.SelectedIndex = 0 Then
                    keyvalue = bm.Current("autono")
                Else
                    keyvalue = Trim(lblkey.Text)
                End If

                sqlstr = "delete from ACf100 where autono=" & keyvalue
                retstr = runsql(mastconn, sqlstr)
                If retstr = "sqlok" Then
                    mydataset.Tables("ACf100").Rows.RemoveAt(JobPara)
                    mydataset.Tables("ACf100").Clear()
                    Call LoadGridFunc()
                    bm.Position = LastPos
                End If

            Case "回寫記錄cmd"
                Dim keyvalue, sqlstr, retstr, updstr As String
                If TabControl1.SelectedIndex = 0 Then
                    Call PutGridToTxt()
                End If
                keyvalue = lblkey.Text

                RecMove1.GenUpdsql("accyear", txtAccYear.Text, "N")
                RecMove1.GenUpdsql("accno", GetAccno(vxtAccno.Text), "T")
                RecMove1.GenUpdsql("QTY_BEG", txtQTY_BEG.Text, "N")
                RecMove1.GenUpdsql("kind", txtKind.Text, "T")
                RecMove1.GenUpdsql("UNIT", txtUnit.Text, "T")
                RecMove1.GenUpdsql("qtyin01", txtQtyin01.Text, "N")
                RecMove1.GenUpdsql("qtyin02", txtQtyin02.Text, "N")
                RecMove1.GenUpdsql("qtyin03", txtQtyin03.Text, "N")
                RecMove1.GenUpdsql("qtyin04", txtQtyin04.Text, "N")
                RecMove1.GenUpdsql("qtyin05", txtQtyin05.Text, "N")
                RecMove1.GenUpdsql("qtyin06", txtQtyin06.Text, "N")
                RecMove1.GenUpdsql("qtyin07", txtQtyin07.Text, "N")
                RecMove1.GenUpdsql("qtyin08", txtQtyin08.Text, "N")
                RecMove1.GenUpdsql("qtyin09", txtQtyin09.Text, "N")
                RecMove1.GenUpdsql("qtyin10", txtQtyin10.Text, "N")
                RecMove1.GenUpdsql("qtyin11", txtQtyin11.Text, "N")
                RecMove1.GenUpdsql("qtyin12", txtQtyin12.Text, "N")
                RecMove1.GenUpdsql("qtyout01", txtQtyout01.Text, "N")
                RecMove1.GenUpdsql("qtyout02", txtQtyout02.Text, "N")
                RecMove1.GenUpdsql("qtyout03", txtQtyout03.Text, "N")
                RecMove1.GenUpdsql("qtyout04", txtQtyout04.Text, "N")
                RecMove1.GenUpdsql("qtyout05", txtQtyout05.Text, "N")
                RecMove1.GenUpdsql("qtyout06", txtQtyout06.Text, "N")
                RecMove1.GenUpdsql("qtyout07", txtQtyout07.Text, "N")
                RecMove1.GenUpdsql("qtyout08", txtQtyout08.Text, "N")
                RecMove1.GenUpdsql("qtyout09", txtQtyout09.Text, "N")
                RecMove1.GenUpdsql("qtyout10", txtQtyout10.Text, "N")
                RecMove1.GenUpdsql("qtyout11", txtQtyout11.Text, "N")
                RecMove1.GenUpdsql("qtyout12", txtQtyout12.Text, "N")
                sqlstr = "update ACf100 set " & RecMove1.genupdfunc & " where autono=" & keyvalue
                retstr = runsql(mastconn, sqlstr)
                If retstr = "sqlok" Then
                    mydataset.Tables("ACf100").Rows.RemoveAt(JobPara)
                    mydataset.Tables("ACf100").Clear()
                    Call LoadGridFunc()
                    MsgBox("更新成功")
                    bm.Position = LastPos
                End If

            Case "新增記錄"
                Dim keyvalue, sqlstr, retstr, updstr As String
                If TabControl1.SelectedIndex = 0 Then
                    Call PutGridToTxt()
                End If
                RecMove1.GenInsSql("accyear", txtAccYear.Text, "N")
                RecMove1.GenInsSql("accno", GetAccno(vxtAccno.Text), "T")
                RecMove1.GenInsSql("QTY_BEG", txtQTY_BEG.Text, "N")
                RecMove1.GenInsSql("kind", txtKind.Text, "U")
                RecMove1.GenInsSql("UNIT", txtUnit.Text, "U")
                RecMove1.GenInsSql("qtyin01", txtQtyin01.Text, "N")
                RecMove1.GenInsSql("qtyin02", txtQtyin02.Text, "N")
                RecMove1.GenInsSql("qtyin03", txtQtyin03.Text, "N")
                RecMove1.GenInsSql("qtyin04", txtQtyin04.Text, "N")
                RecMove1.GenInsSql("qtyin05", txtQtyin05.Text, "N")
                RecMove1.GenInsSql("qtyin06", txtQtyin06.Text, "N")
                RecMove1.GenInsSql("qtyin07", txtQtyin07.Text, "N")
                RecMove1.GenInsSql("qtyin08", txtQtyin08.Text, "N")
                RecMove1.GenInsSql("qtyin09", txtQtyin09.Text, "N")
                RecMove1.GenInsSql("qtyin10", txtQtyin10.Text, "N")
                RecMove1.GenInsSql("qtyin11", txtQtyin11.Text, "N")
                RecMove1.GenInsSql("qtyin12", txtQtyin12.Text, "N")
                RecMove1.GenInsSql("qtyout01", txtQtyout01.Text, "N")
                RecMove1.GenInsSql("qtyout02", txtQtyout02.Text, "N")
                RecMove1.GenInsSql("qtyout03", txtQtyout03.Text, "N")
                RecMove1.GenInsSql("qtyout04", txtQtyout04.Text, "N")
                RecMove1.GenInsSql("qtyout05", txtQtyout05.Text, "N")
                RecMove1.GenInsSql("qtyout06", txtQtyout06.Text, "N")
                RecMove1.GenInsSql("qtyout07", txtQtyout07.Text, "N")
                RecMove1.GenInsSql("qtyout08", txtQtyout08.Text, "N")
                RecMove1.GenInsSql("qtyout09", txtQtyout09.Text, "N")
                RecMove1.GenInsSql("qtyout10", txtQtyout10.Text, "N")
                RecMove1.GenInsSql("qtyout11", txtQtyout11.Text, "N")
                RecMove1.GenInsSql("qtyout12", txtQtyout12.Text, "N")

                sqlstr = "insert into ACf100 " & RecMove1.GenInsFunc
                retstr = runsql(mastconn, sqlstr)

                If retstr = "sqlok" Then
                    mydataset.Tables("ACf100").Rows.RemoveAt(JobPara)
                    mydataset.Tables("ACf100").Clear()
                    Call LoadGridFunc()
                    bm.Position = LastPos
                    'MsgBox("新增成功")
                Else
                    MsgBox("新增失敗")
                End If
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

    Private Sub DataGrid1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.Click
        lblkey.Text = bm.Current("autono")  'keep the old keyvalue
        Call PutGridToTxt()
        Dirty = False
    End Sub

    Private Sub DataGrid1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.DoubleClick
        TabControl1.SelectedIndex = 1
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub txtQtyin01_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtQtyin01.LostFocus, txtQtyout01.LostFocus, _
                                           txtQtyin02.LostFocus, txtQtyout02.LostFocus, txtQtyin08.LostFocus, txtQtyout08.LostFocus, _
                                           txtQtyin03.LostFocus, txtQtyout03.LostFocus, txtQtyin09.LostFocus, txtQtyout09.LostFocus, _
                                           txtQtyin04.LostFocus, txtQtyout04.LostFocus, txtQtyin10.LostFocus, txtQtyout10.LostFocus, _
                                           txtQtyin05.LostFocus, txtQtyout05.LostFocus, txtQtyin11.LostFocus, txtQtyout11.LostFocus, _
                                           txtQtyin06.LostFocus, txtQtyout06.LostFocus, txtQtyin12.LostFocus, txtQtyout12.LostFocus, _
                                           txtQtyin07.LostFocus, txtQtyout07.LostFocus
        If (Not IsNumeric(sender.text) Or ValComa(sender.text) < 0) And Trim(sender.text) <> "" Then
            MsgBox("請輸入正數字")
            sender.focus()
        Else
            Dim strI As String = Microsoft.VisualBasic.Right(sender.name, 2)
            If sender.text <> "" Then
                sender.text = Format(ValComa(sender.text), "###,###,###,###.######")
            End If
            FindControl(Me, "lblNet" & strI).Text = Format(ValComa(FindControl(Me, "txtqtyin" & strI).Text) - ValComa(FindControl(Me, "txtqtyout" & strI).Text), "###,###,###,###.######")
        End If
    End Sub
End Class
