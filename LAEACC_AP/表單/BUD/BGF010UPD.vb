Public Class BGF010upd
    Dim confid, confunit As String, LoadAfter, Dirty As Boolean
    Dim sqlstr As String
    Dim bm As BindingManagerBase, mydataset As DataSet
    Dim UserId, UserName, UserUnit As String

    Private Sub BGF010UPD_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim myds As DataSet
        LoadAfter = True
        UserName = TransPara.TransP("username")
        UserUnit = TransPara.TransP("userunit")
        UserId = TransPara.TransP("userid")
        nudYear.Value = GetYear(TransPara.TransP("Userdate"))
        vxtStartNo.Text = "1"    '起值
        vxtEndNo.Text = "9"      '迄值
    End Sub

    Private Sub BtnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSearch.Click
        Dim sqlstr, qstr, retstr, strAccno, strBookAccno As String
        Dim intJ, intI, intSum, intCount, intYear As Integer
        lblFinish1.Text = ""
        lblFinish2.Text = ""
        BtnSearch.Enabled = False
        btnExit.Enabled = False


        '處理預算科目預算請購數
        intCount = 0  '計算更正預算科目個數
        sqlstr = "SELECT A.*, B.SUMAMT AS SUMAMT, B.SUMUSED as sumused FROM BGF010 A " & _
                 "LEFT OUTER JOIN (SELECT ACCNO, SUM(USEABLEAMT) AS SUMAMT, SUM(SUMUSED) AS SUMUSED " & _
                 "FROM (SELECT x.*, y.SUMUSED FROM BGF020 x LEFT OUTER JOIN " & _
                 "(SELECT BGNO, SUM(USEAMT) AS SUMUSED FROM  BGF030 GROUP BY BGNO) Y " & _
                 "ON X.BGNO = Y.BGNO WHERE x.ACCYEAR =" & nudYear.Value & " AND x.ACCNO >= '" & GetAccno(vxtStartNo.Text) & "'" & _
                 " AND X.ACCNO<='" & GetAccno(vxtEndNo.Text) & "' AND x.CLOSEMARK <> 'Y') C GROUP BY   ACCNO) B " & _
                 "ON A.ACCNO = B.ACCNO " & _
                 "WHERE (A.ACCYEAR =" & nudYear.Value & ") AND (A.ACCNO >= '" & GetAccno(vxtStartNo.Text) & "' AND A.ACCNO<='" & GetAccno(vxtEndNo.Text) & "')"

        'sqlstr = "SELECT A.*, B.SUMAMT AS SUMAMT FROM BGF010 A LEFT OUTER JOIN " & _
        '"(SELECT ACCNO, SUM(USEABLEAMT) AS SUMAMT FROM BGF020 WHERE ACCYEAR=" & nudYear.Value & " AND CLOSEMARK<>'Y' " & _
        '" AND ACCNO>='" & vxtStartNo.getTrimData() & "' AND ACCNO<='" & vxtEndNo.getTrimData() & "' GROUP BY ACCNO) B " & _
        '"ON A.ACCNO=B.ACCNO WHERE A.ACCYEAR=" & nudYear.Value & " AND A.ACCNO>='" & vxtStartNo.getTrimData() & "' AND A.ACCNO<='" & vxtEndNo.getTrimData() & "'"
        mydataset = openmember("", "BGF010", sqlstr)
        With mydataset.Tables("BGF010")
            For intI = 0 To (.Rows.Count - 1)
                intYear = .Rows(intI).Item("Accyear")
                strAccno = .Rows(intI).Item("ACCNO")
                If IsDBNull(.Rows(intI).Item("SUMAMT")) Then
                    intSum = 0
                Else
                    intSum = nz(.Rows(intI).Item("SUMAMT"), 0) - nz(.Rows(intI).Item("sumused"), 0) '多次開支->已開支數
                End If
                If .Rows(intI).Item("TOTPER") <> intSum Then
                    sqlstr = "update BGF010 set TOTPER = " & intSum & " where ACCYEAR=" & intYear & " and  accno='" & strAccno & "'"
                    retstr = runsql(mastconn, sqlstr)
                    intCount += 1
                End If
            Next
        End With
        lblFinish1.Text = "預算請購數更正完成件數=" & Str(intCount)
        mydataset.Clear()

        '處理預算科目開支數
        intCount = 0  '計算更正預算科目個數
        sqlstr = "SELECT a.ACCYEAR, a.ACCNO, a.TotUse, b.SUMAMT from bgf010 a left outer join " & _
                 "(SELECT ACCYEAR,ACCNO,SUM(USEAMT) AS SUMAMT FROM " & _
                 "(SELECT BGF020.ACCYEAR AS ACCYEAR,BGF020.ACCNO AS ACCNO,BGF030.USEAMT AS USEAMT FROM BGF030 INNER JOIN BGF020 ON BGF030.BGNO=BGF020.BGNO) derivedtbl" & _
                 " WHERE ACCYEAR=" & nudYear.Value & " AND ACCNO>='" & GetAccno(vxtStartNo.Text) & "' AND ACCNO<='" & GetAccno(vxtEndNo.Text) & _
                 "' group by accyear,accno) b on a.accyear=b.accyear and a.accno=b.accno " & _
                 " WHERE a.ACCYEAR=" & nudYear.Value & " AND a.ACCNO>='" & GetAccno(vxtStartNo.Text) & "' AND a.ACCNO<='" & GetAccno(vxtEndNo.Text) & "' ORDER BY a.ACCNO"
        '處理未決算預算科目預算開支數
        'sqlstr = "SELECT a.ACCYEAR, a.ACCNO, a.TotUse, b.SUMAMT from bgf010 a left outer join " & _
        '        "(SELECT ACCYEAR,ACCNO,SUM(USEAMT) AS SUMAMT FROM " & _
        '        "(SELECT BGF020.ACCYEAR AS ACCYEAR,BGF020.ACCNO AS ACCNO,BGF030.USEAMT AS USEAMT FROM BGF030 INNER JOIN " & _
        '        "BGF020 ON BGF030.BGNO=BGF020.BGNO) derivedtbl" & _
        '        " WHERE ACCYEAR=" & nudYear.Value & " AND ACCNO>='" & vxtStartNo.getTrimData() & "' AND ACCNO<='" & vxtEndNo.getTrimData() & _
        '        "' group by accyear,accno) b on a.accyear=b.accyear and a.accno=b.accno " & _
        '        " WHERE a.ctrl<>'Y' AND a.ACCNO>='" & vxtStartNo.getTrimData() & "' AND a.ACCNO<='" & vxtEndNo.getTrimData() & "' ORDER BY a.ACCNO"
        mydataset = openmember("", "BGF010", sqlstr)
        With mydataset.Tables("BGF010")
            For intI = 0 To (.Rows.Count - 1)
                intYear = .Rows(intI).Item("accyear")
                strAccno = .Rows(intI).Item("ACCNO")
                If IsDBNull(.Rows(intI).Item("SUMAMT")) Then
                    intSum = 0
                Else
                    intSum = .Rows(intI).Item("SUMAMT")
                End If
                If .Rows(intI).Item("TOTUSE") <> intSum Then
                    sqlstr = "update BGF010 set TOTUSE = " & intSum & " where ACCYEAR=" & intYear & " and accno='" & strAccno & "'"
                    retstr = runsql(mastconn, sqlstr)
                    intCount += 1
                End If
            Next
        End With
        lblFinish2.Text = "開支數更正完成件數=" & Str(intCount)
        btnExit.Enabled = True

    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class
