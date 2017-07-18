Public Class SYSGROUPUSER
    Dim confid, confunit As String
    Dim sqlstr As String
    Dim bm, bmsys As BindingManagerBase
    Dim mydataset, tmpdataset As DataSet
    Private Sub SYSGROUPUSER_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        mastconn = DNS_SYS

        '將科目置combobox
        sqlstr = "SELECT * from unit order by unit_id"
        tmpdataset = openmember(DNS_SYS, "unit", sqlstr)
        If tmpdataset.Tables("unit").Rows.Count = 0 Then
            cbounit_id.Text = "尚無單位"
        Else
            cbounit_id.DataSource = tmpdataset.Tables("unit")
            cbounit_id.DisplayMember = "unit_name"  '顯示欄位
            cbounit_id.ValueMember = "unit_id"     '欄位值
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
        sqlstr = " select a.user_id,a.name,a.employee_id,a.unit_id,b.unit_name "
        sqlstr += " ,CASE isnull(c.group_id,'')   WHEN '' THEN 'False' ELSE 'True'  END as checked from users a"
        sqlstr += " left join unit b on a.unit_id=b.unit_id"
        sqlstr += " left join users_groups c on a.user_id=c.user_id and c.group_id='" & lblkey.Text & "'"
        sqlstr += " where 1=1"
        sqlstr += IIf(cbounit_id.SelectedValue = "", "", " and a.unit_id='" + cbounit_id.SelectedValue + "'")
        sqlstr += " order by checked desc,a.user_id"

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

        sqlstr = "delete a from users_groups a left join users b on a.user_id=b.user_id  where a.group_id='" & lblkey.Text & "'"
        sqlstr += IIf(cbounit_id.SelectedValue = "", "", " and b.unit_id='" + cbounit_id.SelectedValue + "'")
        retstr = runsql(mastconn, sqlstr)

        If retstr <> "sqlok" Then
            MsgBox("群組使用者刪除失敗")
            Exit Sub
        End If

        For inti = 0 To bmsys.Count - 1
            bmsys.Position = inti
            If bmsys.Current("checked") = "True" Then

                GenInsSql("group_id", lblkey.Text, "T")
                GenInsSql("user_id", bmsys.Current("user_id"), "T")
                sqlstr = "insert into users_groups " & GenInsFunc
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
