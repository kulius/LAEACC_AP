Public Class BGQ030
    Dim confid, confunit As String, LoadAfter, Dirty As Boolean
    Dim sqlstr As String
    Dim mydataset As DataSet

    Private Sub BGQ030_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim sqlstr, qstr, strD, strC As String
        sqlstr = "SELECT engno, engname, userid, cop from enf010 where engname like '%" & txtEngName.Text & "%'" & _
                 " order by engno"
        mydataset = openmember("", "BGF010", sqlstr)
        DataGrid1.DataSource = mydataset
        DataGrid1.DataMember = "BGF010"
    End Sub
End Class
