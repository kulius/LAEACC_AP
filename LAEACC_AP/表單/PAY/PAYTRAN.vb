Public Class PAYTRAN
    Dim confid, confunit As String, LoadAfter, Dirty As Boolean
    Dim sqlstr As String
    Dim mydataset As DataSet

    Private Sub PAYTRAN_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim myds As DataSet
        '在會計科目='5'的主計審核者欄標示  Y OR N
        sqlstr = "SELECT * FROM ACCNAME WHERE ACCNO='5'"
        mydataset = openmember("", "ACCNAME", sqlstr)
        If mydataset.Tables(0).Rows.Count <= 0 Then
            MsgBox("貴會會計科目檔少了科目=5的科目,請主計人員先行補上 ")
            Me.Close()
        End If
        If Mid(nz(mydataset.Tables(0).Rows(0).Item("BANK"), ""), 1, 2) = "TR" Then
            rdoYes.Checked = True
            rdoNo.Checked = False
            lblmsg.Text = "目前已設定使用電子轉帳"
        Else
            rdoYes.Checked = False
            rdoNo.Checked = True
            lblmsg.Text = "目前不使用電子轉帳"
        End If
    End Sub

    Private Sub BtnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSearch.Click
        Dim sqlstr, retstr As String
        Dim strYes As String

        '在會計科目='5'的主計審核者欄標示  Y OR N
        If rdoYes.Checked Then
            strYes = "TR"
        Else
            strYes = "  "
        End If
        sqlstr = "UPDATE ACCNAME SET BANK='" & strYes & "' WHERE ACCNO='5'"
        retstr = runsql(mastconn, sqlstr)
        If retstr = "sqlok" Then
            If strYes = "TR" Then
                MsgBox("已控制為--使用電子轉帳")
            Else
                MsgBox("已控制不使用電子轉帳")
            End If
        End If
        Me.Close()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class
