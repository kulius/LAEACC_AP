Public Class PAY_PSNAME
    Dim confid, confunit As String
    Dim sqlstr As String
    Dim bm As BindingManagerBase, mydataset As DataSet
    Dim UserId, UserName, UserUnit As String

    Private Sub PsName_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim myds As DataSet
        UserName = TransPara.TransP("username")
        UserUnit = TransPara.TransP("userunit")
        UserId = TransPara.TransP("userid")
        Label1.Text = UserName & " " & UserUnit
        Call LoadGridFunc()
    End Sub

    Sub LoadGridFunc()
        Dim sqlstr As String
        sqlstr = "SELECT   *  FROM  psname where unit='" & TransPara.TransP("userunit") & "'"

        If Len(txtQpsstr.Text) > 0 Then
            sqlstr += " and psstr like '%" & txtQpsstr.Text & "%'  "
        End If
        sqlstr = sqlstr & " order by unit, seq"
        mydataset = openmember("", "psname", sqlstr)
        RecMove1.FilesPara = "mastconn=" & mastconn & "|sqlstr=" & sqlstr
        DataGrid1.DataSource = mydataset
        DataGrid1.DataMember = "psname"
        bm = Me.BindingContext(mydataset, "psname")
        RecMove1.Setds = bm

    End Sub

    Sub transjob(ByVal JobName As String, ByVal JobPara As String)
        Dim LastPos As Integer = bm.Position
        Select Case JobName
            Case "刪除記錄"
                Dim keyvalue, sqlstr, retstr As String
                LastPos = bm.Position
                keyvalue = bm.Current("autono")
                sqlstr = "delete from Psname where autono=" & keyvalue
                retstr = runsql(mastconn, sqlstr)
                If retstr = "sqlok" Then
                    mydataset.Tables("psname").Rows.RemoveAt(JobPara)
                    mydataset.Tables("psname").Clear()
                    Call LoadGridFunc()
                    bm.Position = LastPos
                End If

            Case "回寫記錄cmd"
                Dim keyvalue, sqlstr, retstr, updstr As String
                Dim Rmark As Integer
                keyvalue = bm.Current("autono")
                '******************************

                RecMove1.GenUpdsql("seq", bm.Current("seq"), "N")
                RecMove1.GenUpdsql("psstr", bm.Current("psstr"), "U")
                RecMove1.GenUpdsql("unit", bm.Current("unit"), "T")

                sqlstr = "update psname set " & RecMove1.genupdfunc & " where autono=" & keyvalue
                '********************
                retstr = runsql(mastconn, sqlstr)
                If retstr = "sqlok" Then
                    mydataset.Tables("psname").Rows.RemoveAt(JobPara)
                    mydataset.Tables("psname").Clear()
                    Call LoadGridFunc()
                    bm.Position = LastPos
                    'MsgBox("更新成功")
                End If


            Case "新增記錄"
                Dim keyvalue, sqlstr, retstr, updstr As String
                RecMove1.GenInsSql("seq", bm.Current("seq"), "N")
                RecMove1.GenInsSql("psstr", bm.Current("psstr"), "U")
                RecMove1.GenInsSql("unit", bm.Current("Unit"), "T")
                sqlstr = "insert into psname " & RecMove1.GenInsFunc
                retstr = runsql(mastconn, sqlstr)

                If retstr = "sqlok" Then
                    mydataset.Tables("psname").Rows.RemoveAt(JobPara)
                    mydataset.Tables("psname").Clear()
                    Call LoadGridFunc()
                    bm.Position = LastPos
                    MsgBox("新增成功")
                End If
        End Select
    End Sub
    Private Sub DataGrid1_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.CurrentCellChanged
        If IsDBNull(bm.Current("unit")) Then
            bm.Current("unit") = UserUnit
        End If
    End Sub

    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        Call LoadGridFunc()
        TabControl1.SelectedIndex = 0
    End Sub

    Private Sub DataGrid1_Navigate(sender As Object, ne As NavigateEventArgs) Handles DataGrid1.Navigate

    End Sub
End Class
