Imports JBC.Printing
Imports System.IO
Imports Microsoft.Office.Interop
Public Class ACY140
    Dim confid, confunit As String, LoadAfter, Dirty As Boolean
    Dim SYear, TYear As Integer
    Dim myds, TempDs As DataSet
    Dim sqlstr As String

    Private Sub ACy140_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        sqlstr = "SELECT max(accyear) as accyear FROM acf050"
        myds = openmember("", "acf050", sqlstr)
        If myds.Tables("acf050").Rows.Count > 0 Then
            nudYear.Value = myds.Tables("acf050").Rows(0).Item("accyear") + 1
        End If
    End Sub

    Private Sub BtnSure_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSure.Click
        SYear = nudYear.Value - 1
        TYear = nudYear.Value
        If SYear <= 0 Or TYear <= 0 Then
            Exit Sub
        End If
        BtnSure.Enabled = False
        If MsgBox("你確定要將" & SYear & "年度餘額結轉至" & TYear & "年度?", MsgBoxStyle.YesNo) <> MsgBoxResult.Yes Then
            Exit Sub
        End If
        lblMsg.Text = "結轉acf050中, 請稍待----"
        Call TransAcf050()   '結轉acf050
        lblMsg.Text = "結轉acf060中, 請稍待----"
        Call TransAcf060()  '結轉acf060
        lblMsg.Text = "結轉acf100中, 請稍待----"
        Call TransAcf100()  '結轉acf100
        lblMsg.Text = "結轉完成"
        MsgBox("結轉完成")
        Me.Close()
    End Sub

    Private Sub TransAcf050()
        Dim sqlstr, retstr, tempStr, strAccno, strTempAccno, strJ As String
        Dim intJ As Integer
        Dim Deamt, Cramt, Tot4, Tot5 As Decimal
        sqlstr = "SELECT accno, deamt12, cramt12 from acf050 where accyear = " & SYear & " order by accno"
        myds = openmember("", "acf050", sqlstr)
        For intI As Integer = 0 To 10000
            If intI = myds.Tables("acf050").Rows.Count Then   'eof 
                strAccno = "32301"  '收入支出結轉本期損益
                If Tot4 > Tot5 Then
                    Cramt = Tot4 - Tot5
                    Deamt = 0
                Else
                    Deamt = Tot5 - Tot4
                    Cramt = 0
                End If
                intI = 99999    '不再回迴圈
            Else
                strAccno = nz(myds.Tables("acf050").Rows(intI).Item("accno"), "")
                Deamt = nz(myds.Tables("acf050").Rows(intI).Item("deamt12"), 0)
                Cramt = nz(myds.Tables("acf050").Rows(intI).Item("cramt12"), 0)
            End If
            If Deamt = Cramt Then   '餘額=0
                If Grade(strAccno) = 7 Then  '七級餘額=0
                    strTempAccno = strAccno  '要控制8級不必轉
                End If
                GoTo NextFor
            End If

            If Grade(strAccno) = 8 And Trim(Mid(strAccno, 1, 16)) = strTempAccno Then
                GoTo NextFor
            End If

            If Mid(strAccno, 1) >= "4" Then
                If Grade(strAccno) = 4 Then
                    If Mid(strAccno, 1, 1) = "4" Then
                        Tot4 = Tot4 + Cramt - Deamt '收入合計
                    End If
                    If Mid(strAccno, 1, 1) = "5" Then Tot5 = Tot5 + Deamt - Cramt '支出合計
                End If
                GoTo nextfor   '收入支出要結轉本期損益
            End If

            If Grade(strAccno) >= 7 And Mid(strAccno, 1, 5) = "13701" Then
                '屬未完工程 七級科目以下轉借貸總額
            Else
                If Grade(strAccno) >= 7 And Mid(strAccno, 1, 5) = "21302" And rdoTot.Checked = True Then
                    '屬代收款 七級科目以下且選擇轉借貸總額
                Else
                    '轉淨額
                    If Deamt > Cramt Then
                        Deamt -= Cramt
                        Cramt = 0
                    Else
                        Cramt -= Deamt
                        Deamt = 0
                    End If
                End If
            End If
            sqlstr = "SELECT * FROM ACF050 WHERE ACCYEAR=" & TYear & " AND ACCNO='" & strAccno & "'"
            TempDs = openmember("", "acf050", sqlstr)
            If TempDs.Tables("acf050").Rows.Count > 0 Then   '該筆資料已存在
                sqlstr = "update acf050 set beg_debit = " & Deamt & ", beg_credit=" & Cramt
                For intJ = 1 To 12
                    strJ = Format(intJ, "00")
                    sqlstr = sqlstr & ", deamt" & strJ & "= deamt" & strJ & " +" & Deamt & ", cramt" & strJ & "= cramt" & strJ & " + " & Cramt
                Next
                sqlstr = sqlstr & " where accyear=" & TYear & " and accno='" & strAccno & "'"
            Else
                GenInsSql("accyear", TYear, "N")
                GenInsSql("accno", strAccno, "T")
                GenInsSql("BEG_DEBIT", Deamt, "N")
                GenInsSql("BEG_CREDIT", Cramt, "N")
                For intJ = 1 To 12
                    strJ = Format(intJ, "00")
                    GenInsSql("Deamt" & strJ, Deamt, "N")
                    GenInsSql("Cramt" & strJ, Cramt, "N")
                Next
                sqlstr = "insert into acf050 " & GenInsFunc
            End If
            retstr = runsql(mastconn, sqlstr)
            If retstr <> "sqlok" Then
                MsgBox("更改acf050 錯誤" & sqlstr)
            End If
NextFor: Next

    End Sub

    Private Sub TransAcf060()
        Dim sqlstr, retstr, tempStr, strAccno, strTempAccno, strJ As String
        Dim intJ As Integer
        Dim Deamt, Cramt, Amt1 As Decimal
        sqlstr = "SELECT accno, deamt12, cramt12 from acf060 where accyear = " & SYear & " order by accno"
        myds = openmember("", "acf060", sqlstr)
        For intI As Integer = 0 To 10000
            If intI >= myds.Tables("acf060").Rows.Count Then   'eof 
                Exit For
            End If
            strAccno = nz(myds.Tables("acf060").Rows(intI).Item("accno"), "")
            Deamt = nz(myds.Tables("acf060").Rows(intI).Item("deamt12"), 0)
            Cramt = nz(myds.Tables("acf060").Rows(intI).Item("cramt12"), 0)
            If Deamt = Cramt Then   '餘額=0
                GoTo NextFor
            End If
            If Mid(strAccno, 1, 1) = "1" Then
                Amt1 = Deamt - Cramt  '應收數
            Else
                Amt1 = Cramt - Deamt '應付數
            End If
            If Deamt > Cramt Then
                Deamt -= Cramt
                Cramt = 0
            Else
                Cramt -= Deamt
                Deamt = 0
            End If

            sqlstr = "SELECT * FROM ACF060 WHERE ACCYEAR=" & TYear & " AND ACCNO='" & strAccno & "'"
            TempDs = openmember("", "acf060", sqlstr)
            If TempDs.Tables("acf060").Rows.Count > 0 Then   '該筆資料已存在,以餘額相加方式處理
                sqlstr = "update acf060 set beg_debit = " & Deamt & ", beg_credit=" & Cramt
                For intJ = 1 To 12
                    strJ = Format(intJ, "00")
                    sqlstr = sqlstr & ", deamt" & strJ & "= deamt" & strJ & " +" & Deamt & _
                             ", cramt" & strJ & "= cramt" & strJ & " + " & Cramt & _
                             ", account" & strJ & "= account" & strJ & " + " & Amt1
                Next
                sqlstr = sqlstr & " where accyear=" & TYear & " and accno='" & strAccno & "'"
            Else
                GenInsSql("accyear", TYear, "N")
                GenInsSql("accno", strAccno, "T")
                GenInsSql("BEG_DEBIT", Deamt, "N")
                GenInsSql("BEG_CREDIT", Cramt, "N")
                For intJ = 1 To 12
                    strJ = Format(intJ, "00")
                    GenInsSql("Deamt" & strJ, Deamt, "N")
                    GenInsSql("Cramt" & strJ, Cramt, "N")
                    GenInsSql("account" & strJ, Amt1, "N")
                Next
                sqlstr = "insert into acf060 " & GenInsFunc
            End If
            retstr = runsql(mastconn, sqlstr)
            If retstr <> "sqlok" Then
                MsgBox("更改acf060 錯誤" & sqlstr)
            End If
NextFor: Next
    End Sub

    Private Sub TransAcf100()
        Dim sqlstr, retstr, tempStr, strAccno, strTempAccno, strJ As String
        Dim intJ As Integer
        Dim Deamt, Cramt As Decimal
        sqlstr = "SELECT accno, kind, unit, qtyin12, qtyout12 from acf100 where accyear = " & SYear & " order by accno"
        myds = openmember("", "acf100", sqlstr)

        For intI As Integer = 0 To 10000
            If intI >= myds.Tables("acf100").Rows.Count Then   'eof 
                Exit For
            End If
            With myds.Tables(0).Rows(intI)
                strAccno = nz(.Item("accno"), "")
                Deamt = nz(.Item("qtyin12"), 0)
                Cramt = nz(.Item("qtyout12"), 0)
            End With
            If Deamt = Cramt Then   '餘額=0
                GoTo NextFor
            End If
            Deamt -= Cramt
            Cramt = 0

            sqlstr = "SELECT * FROM acf100 WHERE ACCYEAR=" & TYear & " AND ACCNO='" & strAccno & "'"
            TempDs = openmember("", "acf100", sqlstr)
            If TempDs.Tables("acf100").Rows.Count > 0 Then   '該筆資料已存在
                sqlstr = "update acf100 set qty_begin = " & Deamt
                For intJ = 1 To 12
                    strJ = Format(intJ, "00")
                    sqlstr = sqlstr & ", qtyin" & strJ & "= qtyin" & strJ & " +" & Deamt
                Next
                sqlstr = sqlstr & " where accyear=" & TYear & " and accno='" & strAccno & "'"
            Else
                GenInsSql("accyear", TYear, "N")
                GenInsSql("accno", strAccno, "T")
                GenInsSql("QTY_BEG", Deamt, "N")
                GenInsSql("kind", nz(myds.Tables("acf100").Rows(intI).Item("kind"), ""), "T")
                GenInsSql("UNIT", nz(myds.Tables("acf100").Rows(intI).Item("unit"), ""), "T")
                For intJ = 1 To 12
                    strJ = Format(intJ, "00")
                    GenInsSql("qtyin" & strJ, Deamt, "N")
                Next
                sqlstr = "insert into acf100 " & GenInsFunc
            End If
            retstr = runsql(mastconn, sqlstr)
            If retstr <> "sqlok" Then
                MsgBox("更改acf100 錯誤" & sqlstr)
            End If
NextFor: Next
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

End Class
