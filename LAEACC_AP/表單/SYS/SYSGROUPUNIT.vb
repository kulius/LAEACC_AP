Public Class SYSGROUPUNIT
    Dim confid, confunit As String
    Dim sqlstr As String
    Dim bm, bmsys As BindingManagerBase
    Dim mydataset, tmpdataset As DataSet
    Private Sub SYSGROUPUNIT_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        mastconn = DNS_SYS

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
        btnSearch.PerformClick()
    End Sub



    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        sqlstr = " select a.unit_id,a.unit_name"
        sqlstr += " ,CASE isnull(b.group_id,'')   WHEN '' THEN 'False' ELSE 'True'  END as checked from unit a"
        sqlstr += "  left join unit_groups b on a.unit_id=b.unit_id and b.group_id='" & lblkey.Text & "'"
        sqlstr += " where 1=1"
        sqlstr += " order by checked desc,a.unit_id"

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

        sqlstr = "delete a from unit_groups a left join unit b on a.unit_id=b.unit_id  where a.group_id='" & lblkey.Text & "'"
        retstr = runsql(mastconn, sqlstr)

        If retstr <> "sqlok" Then
            MsgBox("群組權限刪除失敗")
            Exit Sub
        End If

        For inti = 0 To bmsys.Count - 1
            bmsys.Position = inti
            If bmsys.Current("checked") = "True" Then

                GenInsSql("group_id", lblkey.Text, "T")
                GenInsSql("unit_id", bmsys.Current("unit_id"), "T")
                sqlstr = "insert into unit_groups " & GenInsFunc
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
