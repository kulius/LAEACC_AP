Public Class SYSMENU
    Dim confid, confunit As String
    Dim sqlstr As String
    Dim bm As BindingManagerBase, mydataset, tmpdataset As DataSet
    Dim UserId, UserName, UserUnit As String

    Private Sub SYSMENU_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        mastconn = DNS_SYS
        Call LoadGridFunc()
    End Sub

    Sub LoadGridFunc()
        Dim sqlstr, strSearch As String

        sqlstr = " select a.*,b.s_system_name,c.s_unit_name from a_sys_nunit_item a"
        sqlstr += " left join a_sys_name b on a.s_system_id=b.s_system_id"
        sqlstr += " left join a_sys_nunit c on a.s_unit_id=c.s_unit_id and a.s_system_id=c.s_system_id"

        mydataset = openmember(mastconn, "menu", sqlstr)
        RecMove1.FilesPara = "mastconn=" & mastconn & "|sqlstr=" & sqlstr
        DataGrid1.DataSource = mydataset
        DataGrid1.DataMember = "menu"
        bm = Me.BindingContext(mydataset, "menu")
        RecMove1.Setds = bm

    End Sub

    Sub transjob(ByVal JobName As String, ByVal JobPara As String) Handles RecMove1.TransJob
        Dim LastPos As Integer = bm.Position
        Select Case JobName
            Case "刪除記錄"
                Dim keyvalue, sqlstr, retstr As String
                LastPos = bm.Position
                keyvalue = bm.Current("s_unitem_id")
                sqlstr = "delete from a_sys_nunit_item where s_unitem_id='" & keyvalue & "'"
                retstr = runsql(mastconn, sqlstr)
                If retstr = "sqlok" Then
                    mydataset.Tables("menu").Rows.RemoveAt(JobPara)
                    mydataset.Tables("menu").Clear()
                    Call LoadGridFunc()
                    bm.Position = LastPos
                End If

            Case "回寫記錄cmd"
                Dim keyvalue, sqlstr, retstr, updstr As String
                Dim Rmark As Integer
                keyvalue = bm.Current("s_unitem_id")
                '******************************

                RecMove1.GenUpdsql("s_system_id", bm.Current("s_system_id"), "T")
                RecMove1.GenUpdsql("s_unit_id", bm.Current("s_unit_id"), "T")
                RecMove1.GenUpdsql("s_item_name", bm.Current("s_item_name"), "T")
                RecMove1.GenUpdsql("sort", bm.Current("sort"), "T")

                sqlstr = "update a_sys_nunit_item set " & RecMove1.genupdfunc & " where s_unitem_id='" & keyvalue & "'"
                '********************
                retstr = runsql(mastconn, sqlstr)
                If retstr = "sqlok" Then
                    mydataset.Tables("menu").Rows.RemoveAt(JobPara)
                    mydataset.Tables("menu").Clear()
                    Call LoadGridFunc()
                    bm.Position = LastPos
                    MsgBox("更新成功")
                End If


            Case "新增記錄"
                Dim keyvalue, sqlstr, retstr, updstr As String
                RecMove1.GenInsSql("s_unitem_id", bm.Current("s_unitem_id"), "T")
                RecMove1.GenUpdsql("s_system_id", bm.Current("s_system_id"), "T")
                RecMove1.GenUpdsql("s_unit_id", bm.Current("s_unit_id"), "T")
                RecMove1.GenUpdsql("s_item_name", bm.Current("s_item_name"), "T")
                RecMove1.GenUpdsql("sort", bm.Current("sort"), "T")

                sqlstr = "insert a_sys_nunit_item users " & RecMove1.GenInsFunc
                retstr = runsql(mastconn, sqlstr)

                If retstr = "sqlok" Then
                    mydataset.Tables("menu").Rows.RemoveAt(JobPara)
                    mydataset.Tables("menu").Clear()
                    Call LoadGridFunc()
                    bm.Position = LastPos
                    MsgBox("新增成功")
                End If
        End Select
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs)
        Call LoadGridFunc()
    End Sub






    Private Sub btnExit_Click_1(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class
