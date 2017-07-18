Public Class ACFNO
    Dim confid, confunit As String
    Dim sqlstr As String
    Dim bm As BindingManagerBase, mydataset As DataSet
    Dim UserId, UserName, UserUnit As String

    Private Sub PsForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call LoadGridFunc()
        Call PutKeyToLbl()
    End Sub

    Sub LoadGridFunc()
        Dim sqlstr As String
        sqlstr = "SELECT   *  FROM  Acfno"
        mydataset = openmember("", "Acfno", sqlstr)
        RecMove1.FilesPara = "mastconn=" & mastconn & "|sqlstr=" & sqlstr
        DataGrid1.DataSource = mydataset
        DataGrid1.DataMember = "Acfno"
        bm = Me.BindingContext(mydataset, "Acfno")
        RecMove1.Setds = bm
    End Sub

    Sub transjob(ByVal JobName As String, ByVal JobPara As String) Handles RecMove1.TransJob
        Dim LastPos As Integer = bm.Position
        Select Case JobName
            Case "刪除記錄"
                Dim sqlstr, retstr As String
                LastPos = bm.Position
                sqlstr = "delete from Acfno where accyear=" & Val(Trim(lblkey1.Text)) & " and kind = '" & Trim(lblkey2.Text) & "'"
                retstr = runsql(mastconn, sqlstr)
                If retstr = "sqlok" Then
                    mydataset.Tables("Acfno").Rows.RemoveAt(JobPara)
                    mydataset.Tables("Acfno").Clear()
                    Call LoadGridFunc()
                    bm.Position = LastPos
                End If

            Case "回寫記錄cmd"
                Dim sqlstr, retstr, updstr As String
                'Dim Rmark As Integer

                RecMove1.GenUpdsql("accyear", bm.Current("accyear"), "N")
                RecMove1.GenUpdsql("kind", bm.Current("kind"), "T")
                RecMove1.GenUpdsql("cont_no", bm.Current("cont_no"), "N")

                sqlstr = "update Acfno set " & RecMove1.genupdfunc & " where accyear=" & Val(Trim(lblkey1.Text)) & " and kind = '" & Trim(lblkey2.Text) & "'"
                retstr = runsql(mastconn, sqlstr)
                If retstr = "sqlok" Then
                    mydataset.Tables("Acfno").Rows.RemoveAt(JobPara)
                    mydataset.Tables("Acfno").Clear()
                    Call LoadGridFunc()
                    bm.Position = LastPos
                Else
                    MsgBox("更新失敗")
                End If


            Case "新增記錄"
                Dim keyvalue, sqlstr, retstr, updstr As String
                RecMove1.GenInsSql("accyear", bm.Current("accyear"), "N")
                RecMove1.GenInsSql("kind", bm.Current("kind"), "T")
                RecMove1.GenInsSql("cont_no", bm.Current("cont_no"), "N")
                sqlstr = "insert into Acfno " & RecMove1.GenInsFunc
                retstr = runsql(mastconn, sqlstr)

                If retstr = "sqlok" Then
                    mydataset.Tables("Acfno").Rows.RemoveAt(JobPara)
                    mydataset.Tables("Acfno").Clear()
                    Call LoadGridFunc()
                    bm.Position = LastPos
                Else
                    MsgBox("新增失敗")
                End If
            Case "記錄移動"
                Dim keyvalue, sqlstr, retstr As String
                Call PutKeyToLbl()
        End Select
    End Sub

    Sub PutKeyToLbl()
        lblkey1.Text = bm.Current("accyear")
        lblkey2.Text = bm.Current("kind")
    End Sub

    Private Sub DataGrid1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.Click
        Call PutKeyToLbl()
    End Sub
End Class
