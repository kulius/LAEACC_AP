Public Class SYSGROUP
    Dim confid, confunit As String
    Dim sqlstr As String
    Dim bm As BindingManagerBase, mydataset, tmpdataset As DataSet
    Dim UserId, UserName, UserUnit As String

    Private Sub SYSGROUP_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        mastconn = DNS_SYS
        Call LoadGridFunc()
    End Sub

    Sub LoadGridFunc()
        Dim sqlstr, strSearch As String

        sqlstr = "SELECT * FROM groups "

        mydataset = openmember(mastconn, "groups", sqlstr)
        RecMove1.FilesPara = "mastconn=" & mastconn & "|sqlstr=" & sqlstr
        DataGrid1.DataSource = mydataset
        DataGrid1.DataMember = "groups"
        bm = Me.BindingContext(mydataset, "groups")
        RecMove1.Setds = bm

    End Sub

    Sub transjob(ByVal JobName As String, ByVal JobPara As String) Handles RecMove1.TransJob
        Dim LastPos As Integer = bm.Position
        Select Case JobName
            Case "刪除記錄"
                Dim keyvalue, sqlstr, retstr As String
                LastPos = bm.Position
                keyvalue = bm.Current("group_id")
                sqlstr = "delete from groups where group_id='" & keyvalue & "'"
                retstr = runsql(mastconn, sqlstr)
                If retstr = "sqlok" Then
                    mydataset.Tables("groups").Rows.RemoveAt(JobPara)
                    mydataset.Tables("groups").Clear()
                    Call LoadGridFunc()
                    bm.Position = LastPos
                End If

            Case "回寫記錄cmd"
                Dim keyvalue, sqlstr, retstr, updstr As String
                Dim Rmark As Integer
                keyvalue = bm.Current("group_id")
                '******************************

                RecMove1.GenUpdsql("group_name", bm.Current("group_name"), "T")

                sqlstr = "update groups set " & RecMove1.genupdfunc & " where group_id='" & keyvalue & "'"
                '********************
                retstr = runsql(mastconn, sqlstr)
                If retstr = "sqlok" Then
                    mydataset.Tables("groups").Rows.RemoveAt(JobPara)
                    mydataset.Tables("groups").Clear()
                    Call LoadGridFunc()
                    bm.Position = LastPos
                    MsgBox("更新成功")
                End If


            Case "新增記錄"
                Dim keyvalue, sqlstr, retstr, updstr As String
                RecMove1.GenInsSql("group_id", bm.Current("group_id"), "T")
                RecMove1.GenUpdsql("group_name", bm.Current("group_name"), "T")

                sqlstr = "insert groups users " & RecMove1.GenInsFunc
                retstr = runsql(mastconn, sqlstr)

                If retstr = "sqlok" Then
                    mydataset.Tables("groups").Rows.RemoveAt(JobPara)
                    mydataset.Tables("groups").Clear()
                    Call LoadGridFunc()
                    bm.Position = LastPos
                    MsgBox("新增成功")
                End If
        End Select
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs)
        Call LoadGridFunc()
    End Sub



    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class
