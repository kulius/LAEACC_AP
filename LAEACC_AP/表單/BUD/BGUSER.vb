Public Class BGUSER
    Dim confid, confunit As String
    Dim sqlstr As String
    Dim bm As BindingManagerBase, mydataset, tempDs As DataSet
    Dim UserId, UserName, UserUnit As String
    Dim KeyValue As Integer = 0

    Private Sub BGUser_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim myds As DataSet
        UserName = TransPara.TransP("username")
        UserUnit = TransPara.TransP("userunit")
        UserId = TransPara.TransP("userid")
        Label1.Text = UserName & " " & UserUnit
        Call LoadGridFunc()
    End Sub

    Sub LoadGridFunc()
        Dim sqlstr As String
        sqlstr = "SELECT * FROM usertable"
        mydataset = openmember("", "usertable", sqlstr)
        RecMove1.FilesPara = "mastconn=" & mastconn & "|sqlstr=" & sqlstr
        DataGrid1.DataSource = mydataset
        DataGrid1.DataMember = "usertable"
        bm = Me.BindingContext(mydataset, "usertable")
        RecMove1.Setds = bm
    End Sub

    Sub transjob(ByVal JobName As String, ByVal JobPara As String) Handles RecMove1.TransJob
        Dim LastPos As Integer = bm.Position
        'If KeyValue <> LastPos Then
        '    MsgBox("請停留在原筆資料")
        '    Exit Sub
        'End If
        Select Case JobName
            Case "刪除記錄"
                Dim sqlstr, retstr As String
                LastPos = bm.Position
                KeyValue = bm.Current("autono")
                sqlstr = "delete from usertable where autono=" & KeyValue
                retstr = runsql("", sqlstr)
                If retstr = "sqlok" Then
                    'mydataset.Tables("usertable").Rows.RemoveAt(JobPara)
                    'mydataset.Tables("usertable").Clear()
                    Call LoadGridFunc()
                    'bm.Position = LastPos
                End If

            Case "回寫記錄cmd"
                Dim sqlstr, retstr, updstr As String
                Dim Rmark As Integer
                KeyValue = bm.Current("autono")
                GenUpdsql("userid", bm.Current("userid"), "T")
                'RecMove1.GenUpdsql("userpw", bm.Current("userpw"), "T")
                GenUpdsql("username", bm.Current("username"), "U")
                GenUpdsql("userunit", bm.Current("userunit"), "T")
                sqlstr = "update usertable set " & GenUpdFunc & " where autono=" & KeyValue

                retstr = runsql("", sqlstr)
                If retstr = "sqlok" Then
                    'mydataset.Tables("usertable").Rows.RemoveAt(JobPara)
                    'mydataset.Tables("usertable").Clear()
                    Call LoadGridFunc()
                    bm.Position = LastPos
                    MsgBox("更新成功")
                End If


            Case "新增記錄"
                Dim sqlstr, retstr, updstr As String
                GenInsSql("userid", bm.Current("userid"), "T")
                GenInsSql("userpwd", " ", "T")
                GenInsSql("username", bm.Current("username"), "U")
                GenInsSql("userunit", bm.Current("userunit"), "T")
                sqlstr = "insert into usertable " & GenInsFunc
                retstr = runsql("", sqlstr)
                If retstr = "sqlok" Then
                    MsgBox("新增成功")
                    Call LoadGridFunc()
                    bm.Position = LastPos
                Else
                    MsgBox("新增full  " & sqlstr)
                End If
        End Select
        RecMove1.Enabled = False
    End Sub

    Private Sub DataGrid1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.Click
        If bm.Position < 0 Then Exit Sub
        RecMove1.Enabled = True
        KeyValue = nz(bm.Current("autono"), 0)
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuery.Click
        Dim sqlstr As String
        sqlstr = "SELECT a.*, B.USERNAME FROM  (SELECT STAFF_NO  FROM ACCNAME GROUP BY STAFF_NO) A " & _
                 "LEFT OUTER JOIN USERTABLE B ON A.STAFF_NO = B.USERID where a.staff_no is not null and a.staff_no<>'' "
        tempDs = openmember("", "temp", sqlstr)
        DataGrid2.DataSource = tempDs
        DataGrid2.DataMember = "temp"
    End Sub
End Class
