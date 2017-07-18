Imports System.Globalization
Public Class BAIL080
    Private Sub btnInq_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInq.Click
        Dim sqlstr As String
        Dim mydataset As DataSet
        Dim intYear As Integer
        Try
            intYear = CType(txtYear.Text, Int16)
        Catch ex As Exception
            MsgBox("年度輸入錯誤，請重新輸入!")
            Exit Sub
        End Try
        '找出保證品期限已過者(保證品期限小於今日日期且履約品尚未退回者) 96/1/18將(KIND = '3')拿掉,以配合其他有保證品期限的
        'sqlstr = "SELECT  ENGNO, ENGNAME, COP " & _
        '        "FROM    ENF010 " & _
        '        "WHERE  (ENGNO IN (SELECT ENGNO FROM BAILF020 WHERE (DATE_E < '" & Today.ToString("yyyy/M/d", CultureInfo.InvariantCulture) & "') AND (DATE_E IS NOT NULL) AND " & _
        '        "(KIND = '3') AND (balance IS NULL OR balance = '') GROUP BY   ENGNO HAVING (SUM(actamt) > 0))) " & _
        '        "AND (EYEAR=" & CStr(intYear) & ") ORDER BY  ENGNO "
        sqlstr = "SELECT  ENGNO, ENGNAME, COP " & _
                "FROM    ENF010 " & _
                "WHERE  (ENGNO IN (SELECT ENGNO FROM BAILF020 WHERE (DATE_E < '" & Today.ToString("yyyy/M/d", CultureInfo.InvariantCulture) & "') AND (DATE_E IS NOT NULL) AND " & _
                " (balance IS NULL OR balance = '') GROUP BY   ENGNO,kind HAVING (SUM(actamt) > 0))) " & _
                "AND (EYEAR=" & CStr(intYear) & ") ORDER BY  ENGNO "

        mydataset = openmember("", "bailf020", sqlstr)
        If mydataset.Tables(0).Rows.Count = 0 Then
            MsgBox("資料不存在!")
        End If
        DataGridTableStyle1.MappingName = "bailf020"
        dtgBail.DataSource = mydataset
        dtgBail.DataMember = "bailf020"
    End Sub

    Private Sub btnEnd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnd.Click
        Me.Close()
    End Sub

    Private Sub frmBail080_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtYear.Text = GetYear(Now)   '預設年度為今年度
    End Sub
End Class
