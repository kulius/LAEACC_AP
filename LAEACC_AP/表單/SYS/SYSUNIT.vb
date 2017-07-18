Public Class SYSUNIT
    Dim confid, confunit As String
    Dim sqlstr As String
    Dim bm As BindingManagerBase, mydataset, tmpdataset As DataSet
    Dim UserId, UserName, UserUnit As String

    Private Sub SYSUNIT_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        mastconn = DNS_SYS
        Call LoadGridFunc()
    End Sub

    Sub LoadGridFunc()
        Dim sqlstr, strSearch As String


        sqlstr = "SELECT * FROM unit a"
        sqlstr += " WHERE 1=1"
        sqlstr += strSearch

        mydataset = openmember(mastconn, "unit", sqlstr)
        RecMove1.FilesPara = "mastconn=" & mastconn & "|sqlstr=" & sqlstr
        DataGrid1.DataSource = mydataset
        DataGrid1.DataMember = "unit"
        bm = Me.BindingContext(mydataset, "unit")
        RecMove1.Setds = bm

    End Sub

    Sub transjob(ByVal JobName As String, ByVal JobPara As String) Handles RecMove1.TransJob
        Dim LastPos As Integer = bm.Position
        Select Case JobName
            Case "刪除記錄"
                Dim keyvalue, sqlstr, retstr As String
                LastPos = bm.Position
                keyvalue = bm.Current("unit_id")
                sqlstr = "delete from unit where unit_id='" & keyvalue & "'"
                retstr = runsql(mastconn, sqlstr)
                If retstr = "sqlok" Then
                    mydataset.Tables("unit").Rows.RemoveAt(JobPara)
                    mydataset.Tables("unit").Clear()
                    Call LoadGridFunc()
                    bm.Position = LastPos
                End If

            Case "回寫記錄cmd"
                Dim keyvalue, sqlstr, retstr, updstr As String
                Dim Rmark As Integer
                keyvalue = bm.Current("unit_id")
                '******************************

                RecMove1.GenUpdsql("unit_name", bm.Current("unit_name"), "T")
                RecMove1.GenUpdsql("leader", bm.Current("leader"), "T")
                RecMove1.GenUpdsql("cashier", bm.Current("cashier"), "T")

                sqlstr = "update unit set " & RecMove1.genupdfunc & " where unit_id='" & keyvalue & "'"
                '********************
                retstr = runsql(mastconn, sqlstr)
                If retstr = "sqlok" Then
                    mydataset.Tables("unit").Rows.RemoveAt(JobPara)
                    mydataset.Tables("unit").Clear()
                    Call LoadGridFunc()
                    bm.Position = LastPos
                    MsgBox("更新成功")
                End If


            Case "新增記錄"
                Dim keyvalue, sqlstr, retstr, updstr As String
                RecMove1.GenInsSql("unit_id", bm.Current("unit_id"), "T")
                RecMove1.GenInsSql("unit_name", bm.Current("unit_name"), "T")
                RecMove1.GenInsSql("leader", bm.Current("leader"), "T")
                RecMove1.GenInsSql("cashier", bm.Current("cashier"), "T")
                sqlstr = "insert into unit " & RecMove1.GenInsFunc
                retstr = runsql(mastconn, sqlstr)

                If retstr = "sqlok" Then
                    mydataset.Tables("unit").Rows.RemoveAt(JobPara)
                    mydataset.Tables("unit").Clear()
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
