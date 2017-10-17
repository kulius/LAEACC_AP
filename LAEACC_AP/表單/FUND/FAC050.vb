Public Class FAC050
    Dim sYear, sNo, sMm, sAutono As Integer     '傳票年度 & 傳票編號 & DATE_2->交易月份
    Dim sAmt, sMat_qty As Decimal               '傳票金額 材料數量
    Dim sKind, sDC, sdate, sBankAccno, sSeq As String '傳票種類,傳票借貸,交易日,往來銀行之銀行科目(11102 or 11103)
    Dim sItem, sAccno, sCotn_Code As String     '傳票項次,傳票科目,科目內容碼
    Dim intI, int1, int2, int3 As Integer
    Dim LoadAfter, Dirty As Boolean
    Dim sqlstr As String
    Dim myDatasetS, myDatasetT, mydataset As DataSet

    Private Sub FAC050_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadAfter = True
        dtpDate.Enabled = True
        btnSure.Enabled = True
        dtpDate.Value = TransPara.TransP("UserDate")
        If Month(Now) = 1 Then    'for year beginning,And Microsoft.VisualBasic.DateAndTime.Day(Now) < 10 let's the date=yy/12/31  
            dtpDate.Value = CStr(GetYear(Now) - 1) + "/12/31"
        End If
    End Sub

    Private Sub btnSure_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSure.Click
        btnSure.Enabled = False   '防止user再按
        btnExit.Enabled = False   '防止user按cancel
        Dim sqlstr, qstr, sortstr As String
        Dim intI As Integer
        int1 = 0
        int2 = 0
        int3 = 0
        dtpDate.Enabled = False
        btnSure.Enabled = False

        FullDate(dtpDate.Value.ToShortDateString)
        sqlstr = "SELECT acf010.*, chf020.accno as bankaccno from acf010 LEFT OUTER JOIN chf020 ON acf010.bank=chf020.bank " & _
                 "where acf010.no_2_no<>0 and acf010.books<>'Y' and acf010.date_2<='" & FullDate(dtpDate.Value.ToShortDateString) & "'"
        myDatasetS = openmember("", "acf010s", sqlstr)
        If myDatasetS.Tables("acf010s").Rows.Count = 0 Then
            lblNo.Text = "已全過完帳,沒有資料待處理"
            Exit Sub
        End If
        lblNo.Text = "共有總帳" & myDatasetS.Tables("acf010s").Rows.Count & "筆待處理,處理中請稍待"
        ProgressBar1.Visible = True
        ProgressBar1.Maximum = myDatasetS.Tables("acf010s").Rows.Count
        For intI = 0 To myDatasetS.Tables("acf010s").Rows.Count - 1
            ProgressBar1.Value = intI + 1   '顯示作業進度
            sdate = FullDate(myDatasetS.Tables("acf010s").Rows(intI).Item("date_2").ToShortDateString)   '將重要欄位置public變數
            sYear = Val(Mid(sdate, 1, 4)) - 1911 'Year(sdate) - 1911
            If Mid(sdate, 5, 5) = "/2/29" Or Mid(sdate, 5, 6) = "/02/29" Then
                sMm = 2
            Else
                sMm = Month(sdate)
            End If
            sNo = myDatasetS.Tables("acf010s").Rows(intI).Item("no_2_no")
            sKind = myDatasetS.Tables("acf010s").Rows(intI).Item("kind")
            sItem = myDatasetS.Tables("acf010s").Rows(intI).Item("item")
            sSeq = myDatasetS.Tables("acf010s").Rows(intI).Item("seq")
            sAmt = myDatasetS.Tables("acf010s").Rows(intI).Item("amt")
            sAccno = myDatasetS.Tables("acf010s").Rows(intI).Item("accno")
            sDC = myDatasetS.Tables("acf010s").Rows(intI).Item("dc")
            sBankAccno = nz(myDatasetS.Tables("acf010s").Rows(intI).Item("bankaccno"), " ")
            sAutono = myDatasetS.Tables("acf010s").Rows(intI).Item("autono")
            '統計過帳張數
            If sItem = "1" Then     '轉帳傳票
                If sKind = "3" Then int3 += 1
            End If
            If sItem = "9" Then     '收支傳票
                If sKind = "1" Then int1 += 1
                If sKind = "2" Then int2 += 1
            End If
            'Call Upd_ACf070()   '產生日計檔
            Call Upd_AMT()      '過帳 UPDATE acf050 & acf060 & acf100 
            Call Upd_ACf010()   'update acf010->books = "Y"
        Next
        lblNo.Text = "共有總帳" & myDatasetS.Tables("acf010s").Rows.Count & "筆已處理"

        lblKind1.Text = lblKind1.Text & int1
        lblKind2.Text = lblKind2.Text & int2
        lblKind3.Text = lblKind3.Text & int3 & "    過帳完成"
        myDatasetT = Nothing
        myDatasetS = Nothing
        btnExit.Text = "結束"
        btnExit.Enabled = True
    End Sub

    Sub Upd_AMT()   '過帳
        Dim intK As Integer
        Call UPD_ACF050()   'acf010是總帳科目(四級)五位 
        If sItem = 1 Then  '表示有明細科目資料acf020待過帳
            sqlstr = "SELECT * from acf020 where no_2_no=" & sNo & " and accyear=" & sYear & " and kind='" & sKind & "' and seq='" & sSeq & "'"
            myDatasetT = openmember("", "acf020s", sqlstr)
            If myDatasetT.Tables("acf020s").Rows.Count = 0 Then
                MsgBox("acf020沒有明細科目資料,請檢查" & sKind & "號數" & sNo)
                Exit Sub
            End If
            For intK = 0 To myDatasetT.Tables("acf020s").Rows.Count - 1
                sAmt = myDatasetT.Tables("acf020s").Rows(intK).Item("amt")
                sAccno = myDatasetT.Tables("acf020s").Rows(intK).Item("accno")
                sDC = myDatasetT.Tables("acf020s").Rows(intK).Item("dc")
                sCotn_Code = myDatasetT.Tables("acf020s").Rows(intK).Item("cotn_code")
                sMat_qty = nz(myDatasetT.Tables("acf020s").Rows(intK).Item("Mat_qty"), 0)
                If Grade(sAccno) = 8 Then
                    If Mid(sAccno, 1, 3) = "113" Or Mid(sAccno, 1, 3) = "151" Or Mid(sAccno, 1, 3) = "212" Then
                        Call Upd_ACf060()  '應收 應付 催收 update acf060 
                    Else
                        Call UPD_ACF050()
                    End If
                    'If Mid(sAccno, 1, 3) = "114" Or Mid(sAccno, 1, 3) = "112" Or (Mid(sAccno, 1, 2) = "13" And Mid(sAccno, 1, 5) <> "13701" And Mid(sAccno, 5, 1) <> "2") Then
                    '    Call Upd_ACf100()  '材料 update acf100 
                    'End If
                    sAccno = Trim(Mid(sAccno, 1, 16))  '過完八級後,將科目設為七級
                End If

                If Grade(sAccno) = 7 Then
                    If Mid(sAccno, 1, 3) = "113" Or Mid(sAccno, 1, 3) = "151" Or Mid(sAccno, 1, 3) = "212" Then
                        Call Upd_ACf060()  '應收 應付 催收 update acf060 
                    Else
                        Call UPD_ACF050()
                    End If
                    'If Mid(sAccno, 1, 3) = "114" Or Mid(sAccno, 1, 3) = "112" Or (Mid(sAccno, 1, 2) = "13" And Mid(sAccno, 1, 5) <> "13701" And Mid(sAccno, 5, 1) <> "2") Then
                    '    Call Upd_ACf100()  '材料 update acf100 
                    'End If
                    sAccno = Trim(Mid(sAccno, 1, 9))   '過完七級後,將科目設為6級
                End If

                If Grade(sAccno) = 6 Then
                    If Mid(sAccno, 1, 3) = "113" Or Mid(sAccno, 1, 3) = "151" Or Mid(sAccno, 1, 3) = "212" Then
                        Call Upd_ACf060()  '應收 應付 催收 update acf060 
                    Else
                        Call UPD_ACF050()
                    End If
                    'If Mid(sAccno, 1, 3) = "114" Or Mid(sAccno, 1, 3) = "112" Or (Mid(sAccno, 1, 2) = "13" And Mid(sAccno, 1, 5) <> "13701" And Mid(sAccno, 5, 1) <> "2") Then
                    '    Call Upd_ACf100()  '材料 update acf100 
                    'End If
                    sAccno = Trim(Mid(sAccno, 1, 7))   '過完6級後,將科目設為5級
                End If

                If Grade(sAccno) = 5 Then
                    If Mid(sAccno, 1, 3) = "113" Or Mid(sAccno, 1, 3) = "151" Or Mid(sAccno, 1, 3) = "212" Then
                        Call Upd_ACf060()  '應收 應付 催收 update acf060 
                    Else
                        Call UPD_ACF050()              '過至5級即可,因4級已由acf010過完帳
                    End If
                    'If Mid(sAccno, 1, 3) = "114" Or Mid(sAccno, 1, 3) = "112" Or (Mid(sAccno, 1, 2) = "13" And Mid(sAccno, 1, 5) <> "13701" And Mid(sAccno, 5, 1) <> "2") Then
                    '    Call Upd_ACf100()  '材料 update acf100 
                    'End If
                End If

                '應收 應付 催收要加過4級
                If Mid(sAccno, 1, 3) = "113" Or Mid(sAccno, 1, 3) = "151" Or Mid(sAccno, 1, 3) = "212" Then
                    sAccno = Trim(Mid(sAccno, 1, 5))   '將科目設為4級
                    Call Upd_ACf060()  '應收 應付 催收 update acf060 
                End If

            Next
        End If
    End Sub


    Sub UPD_ACF050()  'update acf050四級科目 & acf100 
        Dim intJ, keyvalue As Integer
        Dim strJ, retstr, fieldname As String
        sqlstr = "SELECT * from acf050 where accyear=" & sYear & " and accno='" & sAccno & "'"
        mydataset = openmember("", "acf050s", sqlstr)
        If mydataset.Tables("acf050s").Rows.Count = 0 Then   '原無該科目餘額則新增一筆
            GenInsSql("accyear", sYear, "N")
            GenInsSql("accno", sAccno, "T")
            sqlstr = "insert into acf050 " & GenInsFunc
            retstr = runsql(mastconn, sqlstr)
            sqlstr = "SELECT * from acf050 where accyear=" & sYear & " and accno='" & sAccno & "'"
            mydataset = openmember("", "acf050s", sqlstr)
        End If
        keyvalue = mydataset.Tables("acf050s").Rows(0).Item("autono")
        For intJ = sMm To 12
            strJ = Format(intJ, "00")
            If sDC = "1" Then '借方  update acf050由交易月份起至12月止,在借方各加入傳票金額
                GenUpdsql("deamt" & strJ, mydataset.Tables("acf050s").Rows(0).Item("deamt" & strJ) + sAmt, "N")
            Else              '貸方  update acf050由交易月份起至12月止,在貸方各加入傳票金額
                GenUpdsql("cramt" & strJ, mydataset.Tables("acf050s").Rows(0).Item("cramt" & strJ) + sAmt, "N")
            End If
        Next
        sqlstr = "update acf050 set " & genupdfunc & " where autono=" & keyvalue
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then MsgBox("acf050 error accno=" & sAccno)
    End Sub

    Sub Upd_ACf060()   '應收 應付 催收 update acf060
        Dim intJ, keyvalue As Integer
        Dim strJ, retstr, fieldname As String
        sqlstr = "SELECT * from acf060 where accyear=" & sYear & " and accno='" & sAccno & "'"
        mydataset = openmember("", "acf060s", sqlstr)
        If mydataset.Tables("acf060s").Rows.Count = 0 Then   '原無該科目餘額則新增一筆
            GenInsSql("accyear", sYear, "N")
            GenInsSql("accno", sAccno, "T")
            sqlstr = "insert into acf060 " & GenInsFunc
            retstr = runsql(mastconn, sqlstr)
            sqlstr = "SELECT * from acf060 where accyear=" & sYear & " and accno='" & sAccno & "'"
            mydataset = openmember("", "acf060s", sqlstr)
        End If
        keyvalue = mydataset.Tables("acf060s").Rows(0).Item("autono")
        For intJ = sMm To 12
            strJ = Format(intJ, "00")
            strJ = Format(intJ, "00")
            If sDC = "1" Then '借方  update acf060由交易月份起至12月止,在借方各加入傳票金額
                GenUpdsql("deamt" & strJ, mydataset.Tables("acf060s").Rows(0).Item("deamt" & strJ) + sAmt, "N")
            Else              '貸方  update acf060由交易月份起至12月止,在貸方各加入傳票金額
                GenUpdsql("cramt" & strJ, mydataset.Tables("acf060s").Rows(0).Item("cramt" & strJ) + sAmt, "N")
            End If
            Select Case sCotn_Code
                Case "1"
                    If (Mid(sAccno, 1, 1) = "1" And sDC = "1") Or (Mid(sAccno, 1, 1) = "2" And sDC = "2") Then '應收數增加,應付數增加
                        GenUpdsql("account" & strJ, mydataset.Tables("acf060s").Rows(0).Item("account" & strJ) + sAmt, "N")
                    Else
                        GenUpdsql("account" & strJ, mydataset.Tables("acf060s").Rows(0).Item("account" & strJ) - sAmt, "N")
                    End If

                Case "2"
                    If (Mid(sAccno, 1, 1) = "1" And sDC = "2") Or (Mid(sAccno, 1, 1) = "2" And sDC = "1") Then '實收數增加,實付數增加
                        GenUpdsql("act" & strJ, mydataset.Tables("acf060s").Rows(0).Item("act" & strJ) + sAmt, "N")
                    Else
                        GenUpdsql("act" & strJ, mydataset.Tables("acf060s").Rows(0).Item("act" & strJ) - sAmt, "N")
                    End If

                Case "3"
                    If (Mid(sAccno, 1, 1) = "1" And sDC = "2") Or (Mid(sAccno, 1, 1) = "2" And sDC = "1") Then '減免數增加,減免數增加
                        GenUpdsql("sub" & strJ, mydataset.Tables("acf060s").Rows(0).Item("sub" & strJ) + sAmt, "N")
                    Else
                        GenUpdsql("sub" & strJ, mydataset.Tables("acf060s").Rows(0).Item("sub" & strJ) - sAmt, "N")
                    End If

                Case "4"
                    If (Mid(sAccno, 1, 1) = "1" And sDC = "2") Or (Mid(sAccno, 1, 1) = "2" And sDC = "1") Then '減免數增加,減免數增加
                        GenUpdsql("trans" & strJ, mydataset.Tables("acf060s").Rows(0).Item("trans" & strJ) + sAmt, "N")
                    Else
                        GenUpdsql("trans" & strJ, mydataset.Tables("acf060s").Rows(0).Item("trans" & strJ) - sAmt, "N")
                    End If

            End Select
        Next
        sqlstr = "update acf060 set " & genupdfunc & " where autono=" & keyvalue
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then MsgBox("acf060 error accno=" & sAccno)
    End Sub

    Sub Upd_ACf100()   '材料 update acf100
        Dim intJ, keyvalue As Integer
        Dim strJ, retstr, fieldname As String
        sqlstr = "SELECT * from acf100 where accyear=" & sYear & " and accno='" & sAccno & "'"
        mydataset = openmember("", "acf100s", sqlstr)
        If mydataset.Tables("acf100s").Rows.Count = 0 Then   '原無該科目餘額則新增一筆
            GenInsSql("accyear", sYear, "N")
            GenInsSql("accno", sAccno, "T")
            sqlstr = "insert into acf100 " & GenInsFunc
            retstr = runsql(mastconn, sqlstr)
            sqlstr = "SELECT * from acf100 where accyear=" & sYear & " and accno='" & sAccno & "'"
            mydataset = openmember("", "acf100s", sqlstr)
        End If
        keyvalue = mydataset.Tables("acf100s").Rows(0).Item("autono")
        For intJ = sMm To 12
            strJ = Format(intJ, "00")
            If sDC = "1" Then '借方  update acf050由交易月份起至12月止,在借方各加入傳票金額
                GenUpdsql("qtyin" & strJ, mydataset.Tables("acf100s").Rows(0).Item("qtyin" & strJ) + sMat_qty, "N")
            Else              '貸方  update acf050由交易月份起至12月止,在貸方各加入傳票金額
                GenUpdsql("qtyout" & strJ, mydataset.Tables("acf100s").Rows(0).Item("qtyout" & strJ) + sMat_qty, "N")
            End If
        Next
        sqlstr = "update acf100 set " & genupdfunc & " where autono=" & keyvalue
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then MsgBox("acf100 error accno=" & sAccno)
    End Sub

    Sub Upd_ACf010()   'update acf010->books = "Y"
        Dim strJ, retstr As String
        sqlstr = "update acf010 set books = 'Y' where autono=" & sAutono
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then MsgBox("acf010 error no_2_no=" & sNo)
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class
