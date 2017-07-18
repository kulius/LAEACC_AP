Public Class AC150
    Dim confid, confunit As String, LoadAfter, Dirty As Boolean
    Dim sqlstr As String
    Dim mydataset, AccnoDataset, mydatasetT As DataSet
    Dim UserId, UserName, UserUnit As String
    Dim bm As BindingManagerBase
    Private Sub ac150_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        nudYear.Value = GetYear(Now)
        '將科目置combobox
        sqlstr = "SELECT accno, left(accno+space(17),17)+accname as accname FROM accname where belong<>'B' order by accno"
        AccnoDataset = openmember("", "accname", sqlstr)
        If accnoDataset.Tables("accname").Rows.Count = 0 Then
            cboAccno.Text = "無科目"
        Else
            cboAccno.DataSource = accnoDataset.Tables("accname")
            cboAccno.DisplayMember = "accname"  '顯示欄位
            cboAccno.ValueMember = "accno"     '欄位值
        End If
    End Sub

    Private Sub BtnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSearch.Click
        Dim sqlstr As String
        Dim intI As Integer
        Dim intD, intC As Decimal
        '先將查詢科目置datagrid
        sqlstr = "SELECT accname.accno, accname.accname, acf050.beg_debit, acf050.beg_credit, acf050.deamt12, acf050.cramt12," & _
                 " 0 as acf020d, 0 as acf020c, 0 as balanced, 0 as balancec FROM  accname left outer join acf050 " & _
                 " on accname.accno=acf050.accno and acf050.accyear=" & nudYear.Value & " WHERE accname.accno like '" & GetAccno(vxtAccno.Text) & "%' " & _
                 " order by accname.accno"
        'sqlstr = "SELECT accname.accno, accname.accname, acf050.beg_debit, acf050.beg_credit, acf050.deamt12, acf050.cramt12," & _
        '         " 0 as acf020d, 0 as acf020c, 0 as balanced, 0 as balancec FROM  accname inner join acf050 " & _
        '         " on accname.accno=acf050.accno WHERE accname.accno like '" & cboAccno.SelectedValue & "%' " & _
        '         " and acf050.accyear=" & nudYear.Value
        mydataset = openmember("", "acf050", sqlstr)
        DataGrid1.DataSource = mydataset
        DataGrid1.DataMember = "acf050"
        bm = Me.BindingContext(mydataset, "acf050")

        '再由datagrid逐一計算未過帳部份
        For inti = 0 To bm.Count - 1
            bm.Position = inti
            sqlstr = "SELECT sum(acf020.amt) as amt  FROM  acf020 inner join acf010 " & _
                     "ON ACF010.ACCYEAR = ACF020.ACCYEAR AND ACF010.NO_1_NO = ACF020.NO_1_NO AND " & _
                     "ACF010.KIND = ACF020.KIND AND ACF010.ITEM = '1' " & _
                     "WHERE acf020.accyear>=" & nudYear.Value & " and acf020.accno='" & _
                     bm.Current("accno") & "' and acf020.dc='1' and ACF010.BOOKS <> 'Y'"
            mydatasetT = openmember("", "acf020", sqlstr)
            If mydatasetT.Tables("acf020").Rows.Count > 0 Then
                bm.Current("acf020d") = IIf(IsDBNull(mydatasetT.Tables("acf020").Rows(0).Item("amt")), 0, mydatasetT.Tables("acf020").Rows(0).Item("amt"))
            End If
            sqlstr = "SELECT sum(acf020.amt) as amt  FROM  acf020 inner join acf010 " & _
                     "ON ACF010.ACCYEAR = ACF020.ACCYEAR AND ACF010.NO_1_NO = ACF020.NO_1_NO AND " & _
                     "ACF010.KIND = ACF020.KIND AND ACF010.ITEM = '1' " & _
                     "WHERE acf020.accyear>=" & nudYear.Value & " and acf020.accno='" & _
                     bm.Current("accno") & "' and acf020.dc='2' and ACF010.BOOKS <> 'Y'"
            mydatasetT = openmember("", "acf020", sqlstr)
            If mydatasetT.Tables("acf020").Rows.Count > 0 Then
                bm.Current("acf020c") = IIf(IsDBNull(mydatasetT.Tables("acf020").Rows(0).Item("amt")), 0, mydatasetT.Tables("acf020").Rows(0).Item("amt"))
            End If
        Next
        For inti = 0 To bm.Count - 1
            bm.Position = intI
            If IsDBNull(bm.Current("deamt12")) Then bm.Current("deamt12") = 0
            If IsDBNull(bm.Current("cramt12")) Then bm.Current("cramt12") = 0
            intD = bm.Current("deamt12") + bm.Current("acf020d")
            intC = bm.Current("cramt12") + bm.Current("acf020c")
            If intD > intC Then bm.Current("balanced") = intD - intC
            If intC > intD Then bm.Current("balancec") = intC - intD
        Next
    End Sub


    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub cboAccno_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboAccno.SelectionChangeCommitted
        vxtAccno.Text = cboAccno.SelectedValue
        vxtAccno.Focus()
    End Sub
End Class
