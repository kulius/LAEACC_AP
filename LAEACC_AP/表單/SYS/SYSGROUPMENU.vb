Public Class SYSGROUPMENU
    Dim confid, confunit As String
    Dim sqlstr As String
    Dim bm, bmsys As BindingManagerBase
    Dim mydataset, tmpdataset As DataSet
    Private Sub SYSGROUPMENU_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        mastconn = DNS_SYS

        '將科目置combobox
        sqlstr = "SELECT * from a_sys_name order by sort"
        tmpdataset = openmember(DNS_SYS, "a_sys_name", sqlstr)
        If tmpdataset.Tables("a_sys_name").Rows.Count = 0 Then
            cbosys.Text = "尚無單位"
        Else
            cbosys.DataSource = tmpdataset.Tables("a_sys_name")
            cbosys.DisplayMember = "s_system_name"  '顯示欄位
            cbosys.ValueMember = "s_system_id"     '欄位值
        End If

        Call LoadGridFunc()
    End Sub

    Sub LoadGridFunc()
        Dim sqlstr As String

        sqlstr = "SELECT * FROM groups "

        mydataset = openmember(mastconn, "groups", sqlstr)
        DataGrid1.DataSource = mydataset
        DataGrid1.DataMember = "groups"
        bm = Me.BindingContext(mydataset, "groups")

    End Sub


    Private Sub DataGrid1_DoubleClick(sender As Object, e As EventArgs) Handles DataGrid1.DoubleClick
        If bm.Count > 0 Then
            If bm.Position < 0 Then Exit Sub
            If bm.Count = 0 Then Exit Sub
            lblkey.Text = nz(bm.Current("group_id"), "") 'keep the old keyvalue
            Call PutGridToTxt()
            TabControl1.SelectedIndex = 1
        End If
    End Sub

    Sub PutGridToTxt()
        If bm.Position < 0 Then Exit Sub
        txtgroup_id.Text = nz(bm.Current("group_id"), "")  '不允許修改bgno,accyear,accno
        txtgroup_name.Text = nz(bm.Current("group_name"), "")
    End Sub



    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        sqlstr = " select b.s_system_name,c.s_unit_name,a.s_item_name,a.sort,a.s_unitem_id"
        sqlstr += "  ,CASE isnull(d.group_id,'')   WHEN '' THEN 'False' ELSE 'True'  END as checked  from a_sys_nunit_item a"
        sqlstr += " left join a_sys_name b on a.s_system_id=b.s_system_id"
        sqlstr += " left join a_sys_nunit c on a.s_unit_id=c.s_unit_id and a.s_system_id=c.s_system_id"
        sqlstr += " left join groups_power d on a.s_unitem_id=d.s_unitem_id and d.group_id='" & lblkey.Text & "'"
        sqlstr += " where 1=1"
        sqlstr += IIf(cbosys.SelectedValue = "", "", " and a.s_system_id='" + cbosys.SelectedValue + "'")
        sqlstr += " order by a.s_system_id,a.s_unit_id,a.sort"

        mydataset = openmember(mastconn, "item", sqlstr)
        DataGridView1.DataSource = mydataset
        DataGridView1.DataMember = "item"
        bmsys = Me.BindingContext(mydataset, "item")
    End Sub

    Private Sub btnall_Click(sender As Object, e As EventArgs) Handles btnall.Click
        For inti = 0 To bmsys.Count - 1
            bmsys.Position = inti
            bmsys.Current("checked") = "True"
        Next
    End Sub

    Private Sub btnnoall_Click(sender As Object, e As EventArgs) Handles btnnoall.Click
        For inti = 0 To bmsys.Count - 1
            bmsys.Position = inti
            bmsys.Current("checked") = "False"
        Next
    End Sub

    Private Sub btnFinish_Click(sender As Object, e As EventArgs) Handles btnFinish.Click
        Dim sqlstr, retstr As String

        sqlstr = "delete a from groups_power a left join a_sys_nunit_item b on a.s_unitem_id=b.s_unitem_id  where a.group_id='" & lblkey.Text & "'"
        sqlstr += IIf(cbosys.SelectedValue = "", "", " and b.s_system_id='" + cbosys.SelectedValue + "'")
        retstr = runsql(mastconn, sqlstr)

        If retstr <> "sqlok" Then
            MsgBox("群組權限刪除失敗")
            Exit Sub
        End If

        For inti = 0 To bmsys.Count - 1
            bmsys.Position = inti
            If bmsys.Current("checked") = "True" Then

                GenInsSql("group_id", lblkey.Text, "T")
                GenInsSql("s_unitem_id", bmsys.Current("s_unitem_id"), "T")
                sqlstr = "insert into groups_power " & GenInsFunc
                retstr = runsql(mastconn, sqlstr)
                If retstr <> "sqlok" Then
                    MsgBox("新增full  " & sqlstr)
                    Exit Sub
                End If

            End If
        Next

      
        MsgBox("設定成功")
        Call btnSearch_Click(sender, e)
    End Sub
End Class
