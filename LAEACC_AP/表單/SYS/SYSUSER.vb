Public Class SYSUSER

    Dim confid, confunit As String
    Dim sqlstr As String
    Dim bm As BindingManagerBase, mydataset, tmpdataset As DataSet
    Dim UserId, UserName, UserUnit As String

    Private Sub SYSUSER_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        mastconn = DNS_SYS

        '將科目置combobox
        sqlstr = "SELECT unit_id, unit_name FROM unit union select top 1 '','' from unit  order by unit_id"
        tmpdataset = openmember(DNS_SYS, "unit", sqlstr)
        If tmpdataset.Tables("unit").Rows.Count = 0 Then
            cbounit.Text = "尚無單位"
        Else
            cbounit.DataSource = tmpdataset.Tables("unit")
            cbounit.DisplayMember = "unit_name"  '顯示欄位
            cbounit.ValueMember = "unit_id"     '欄位值
        End If

        Call LoadGridFunc()
    End Sub

    Sub LoadGridFunc()
        Dim sqlstr, strSearch As String

        If cbounit.SelectedValue <> "" Then
            strSearch = " and a.unit_id='" + cbounit.SelectedValue + "'"
        End If



        sqlstr = "SELECT a.user_id,a.password,a.name,a.employee_id,a.unit_id,b.unit_name FROM users a"
        sqlstr += " left join unit b on a.unit_id=b.unit_id "
        sqlstr += " WHERE 1=1"
        sqlstr += strSearch

        mydataset = openmember(mastconn, "users", sqlstr)
        RecMove1.FilesPara = "mastconn=" & mastconn & "|sqlstr=" & sqlstr
        DataGrid1.DataSource = mydataset
        DataGrid1.DataMember = "users"
        bm = Me.BindingContext(mydataset, "users")
        RecMove1.Setds = bm

    End Sub

    Sub transjob(ByVal JobName As String, ByVal JobPara As String) Handles RecMove1.TransJob
        Dim LastPos As Integer = bm.Position
        Select Case JobName
            Case "刪除記錄"
                Dim keyvalue, sqlstr, retstr As String
                LastPos = bm.Position
                keyvalue = bm.Current("user_id")
                sqlstr = "delete from users where user_id='" & keyvalue & "'"
                retstr = runsql(mastconn, sqlstr)
                If retstr = "sqlok" Then
                    mydataset.Tables("users").Rows.RemoveAt(JobPara)
                    mydataset.Tables("users").Clear()
                    Call LoadGridFunc()
                    bm.Position = LastPos
                End If

            Case "回寫記錄cmd"
                Dim keyvalue, sqlstr, retstr, updstr As String
                Dim Rmark As Integer
                keyvalue = bm.Current("user_id")
                '******************************

                RecMove1.GenUpdsql("password", bm.Current("password"), "T")
                RecMove1.GenUpdsql("name", bm.Current("name"), "U")
                RecMove1.GenUpdsql("employee_id", bm.Current("employee_id"), "T")
                RecMove1.GenUpdsql("unit_id", bm.Current("unit_id"), "T")

                sqlstr = "update users set " & RecMove1.genupdfunc & " where user_id='" & keyvalue & "'"
                '********************
                retstr = runsql(mastconn, sqlstr)
                If retstr = "sqlok" Then
                    mydataset.Tables("users").Rows.RemoveAt(JobPara)
                    mydataset.Tables("users").Clear()
                    Call LoadGridFunc()
                    bm.Position = LastPos
                    MsgBox("更新成功")
                End If


            Case "新增記錄"
                Dim keyvalue, sqlstr, retstr, updstr As String
                RecMove1.GenInsSql("user_id", bm.Current("user_id"), "T")
                RecMove1.GenInsSql("password", bm.Current("password"), "T")
                RecMove1.GenInsSql("name", bm.Current("name"), "U")
                RecMove1.GenInsSql("employee_id", bm.Current("employee_id"), "T")
                RecMove1.GenInsSql("unit_id", bm.Current("unit_id"), "T")
                sqlstr = "insert into users " & RecMove1.GenInsFunc
                retstr = runsql(mastconn, sqlstr)

                If retstr = "sqlok" Then
                    mydataset.Tables("users").Rows.RemoveAt(JobPara)
                    mydataset.Tables("users").Clear()
                    Call LoadGridFunc()
                    bm.Position = LastPos
                    MsgBox("新增成功")
                End If
        End Select
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Call LoadGridFunc()
    End Sub



    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnFinish_Click(sender As Object, e As EventArgs) Handles btnFinish.Click
        Dim sqlstr, retstr As String
        Dim intI As Integer
        Dim intD, intC As Decimal
        '先將查詢科目置datagrid
        sqlstr = "SELECT  * " & _
         " FROM  Auth_User "

        mydataset = openmember(DNS_AUTH, "Auth_User", sqlstr)
        DataGrid1.DataSource = mydataset
        DataGrid1.DataMember = "Auth_User"
        bm = Me.BindingContext(mydataset, "Auth_User")

        For inti = 0 To bm.Count - 1
            bm.Position = inti

            sqlstr = "delete from users where user_id='" & bm.Current("user_id") & "'"
            retstr = runsql(mastconn, sqlstr)

            RecMove1.GenInsSql("user_id", bm.Current("user_id"), "T")
            RecMove1.GenInsSql("password", bm.Current("user_password"), "T")
            RecMove1.GenInsSql("name", bm.Current("user_name"), "T")
            RecMove1.GenInsSql("employee_id", bm.Current("employee_id"), "T")
            RecMove1.GenInsSql("unit_id", bm.Current("unit_id"), "T")
            RecMove1.GenInsSql("login", "Y", "T")
            sqlstr = "insert into users " & RecMove1.GenInsFunc
            retstr = runsql(mastconn, sqlstr)

        Next

        MsgBox("匯入成功")
        Call LoadGridFunc()
    End Sub

End Class
