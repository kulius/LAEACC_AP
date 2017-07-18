Imports System.Globalization
Public Class BAIL070
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
        '找出該年度開標日期不為Null值(表示已發包),且保證金檔(bailf010)及保管品檔無履約金(kind='1')收(rp='1')的記錄存在.
        '工程費100,000以下者不用繳交保證金(工程費檔ENF040預算金額BGAMT1合計不含費用名稱FEE為管理費的)
        sqlstr = "SELECT  ENGNO AS 工程代號, ENGNAME AS 工程名稱, COP AS 商號,  OPEN_DATE AS 開標日期  FROM ENF010 WHERE (ENGNO NOT IN "
        sqlstr &= "(SELECT   BAILF010.ENGNO AS 工程代號  FROM BAILF010 WHERE BAILF010.RP = '1' AND BAILF010.KIND = '1' and (balance is null or balance='')"
        sqlstr &= "GROUP BY   bailf010.engno, bailf010.kind, bailf010.rp  HAVING SUM(amt) > 0 UNION SELECT BAILF020.ENGNO AS 工程代號 "
        sqlstr &= "FROM BAILF020 WHERE BAILF020.RP = '1' AND BAILF020.KIND = '1' and (balance is null or balance='') GROUP BY  bailf020.engno, bailf020.kind, bailf020.rp "
        sqlstr &= "HAVING SUM(amt) > 0)) AND (OPEN_DATE IS NOT NULL) AND (EYEAR=" & CStr(intYear) & ")"
        sqlstr &= " and ENGNO not in (SELECT engno FROM enf040 WHERE (FEE <> '管理費') GROUP BY   ENGNO HAVING (SUM(BGAMT1) < 100000))"

        mydataset = openmember("", "bailf010", sqlstr)
        If mydataset.Tables(0).Rows.Count = 0 Then
            MsgBox("資料不存在!")
        End If
        DataGridTableStyle1.MappingName = "bailf010"
        dtgBail.DataSource = mydataset
        dtgBail.DataMember = "bailf010"

    End Sub

    Private Sub btnEnd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnd.Click
        Me.Close()
    End Sub


    Private Sub frmBail070_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtYear.Text = GetYear(Now)   '預設年度為今年度
    End Sub
End Class
