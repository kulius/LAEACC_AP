Public Class UNITTABLE
    Dim confid, confunit As String
    Dim sqlstr As String
    Dim bm As BindingManagerBase, mydataset, tempDs As DataSet
    Dim strUnit As String

    Private Sub UnitTable_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim myds As DataSet
        Call LoadGridFunc()
    End Sub

    Sub LoadGridFunc()
        Dim sqlstr As String
        sqlstr = "SELECT * FROM unittable"
        mydataset = openmember("", "unittable", sqlstr)
        RecMove1.FilesPara = "mastconn=" & mastconn & "|sqlstr=" & sqlstr
        DataGrid1.DataSource = mydataset
        DataGrid1.DataMember = "unittable"
        bm = Me.BindingContext(mydataset, "unittable")
        RecMove1.Setds = bm
    End Sub

    Sub transjob(ByVal JobName As String, ByVal JobPara As String) Handles RecMove1.TransJob
        Dim LastPos As Integer = bm.Position
        Select Case JobName
            Case "刪除記錄"
                Dim keyvalue, sqlstr, retstr As String
                LastPos = bm.Position
                'keyvalue = bm.Current("unit")
                sqlstr = "delete from unittable where unit='" & strUnit & "'"
                retstr = runsql("", sqlstr)
                If retstr = "sqlok" Then
                    Call LoadGridFunc()
                End If

            Case "回寫記錄cmd"
                Dim keyvalue, sqlstr, retstr, updstr As String
                Dim Rmark As Integer
                'keyvalue = bm.Current("unit")
                GenUpdsql("unit", nz(bm.Current("unit"), ""), "T")
                GenUpdsql("unitname", nz(bm.Current("unitname"), ""), "U")
                GenUpdsql("shortname", nz(bm.Current("shortname"), ""), "U")
                GenUpdsql("leader", nz(bm.Current("leader"), ""), "U")
                GenUpdsql("cashier", nz(bm.Current("cashier"), ""), "U")
                sqlstr = "update unittable set " & GenUpdFunc & " where unit='" & strUnit & "'"
                retstr = runsql("", sqlstr)
                If retstr = "sqlok" Then
                    Call LoadGridFunc()
                    bm.Position = LastPos
                    MsgBox("更新成功")
                End If

            Case "新增記錄"
                Dim keyvalue, sqlstr, retstr, updstr As String
                GenInsSql("unit", nz(bm.Current("unit"), ""), "T")
                GenInsSql("unitname", nz(bm.Current("unitname"), ""), "U")
                GenInsSql("shortname", nz(bm.Current("shortname"), ""), "U")
                GenInsSql("leader", nz(bm.Current("leader"), ""), "U")
                GenInsSql("cashier", nz(bm.Current("cashier"), ""), "U")
                sqlstr = "insert into unittable " & GenInsFunc
                retstr = runsql("", sqlstr)
                If retstr = "sqlok" Then
                    MsgBox("新增成功")
                    Call LoadGridFunc()
                    bm.Position = LastPos
                Else
                    MsgBox("新增錯誤  " & sqlstr)
                End If
        End Select
        RecMove1.Enabled = False
    End Sub

    Private Sub DataGrid1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.Click
        If bm.Position < 0 Then Exit Sub
        strUnit = nz(bm.Current("UNIT"), "") 'keep the old keyvalue
        RecMove1.Enabled = True
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class
