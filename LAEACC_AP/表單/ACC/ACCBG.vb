Public Class ACCBG
    Dim confid, confunit As String, LoadAfter, Dirty As Boolean
    Dim sqlstr As String
    Dim bm As BindingManagerBase, mydataset As DataSet
    Dim UserId, UserName, UserUnit As String

    Private Sub ACCBG_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TabControl1.Enabled = False
        RecMove1.Enabled = False
        nudYear.Value = GetYear(Now)
        vxtStartNo.Text = "1"    '起值
        vxtEndNo.Text = "59"      '迄值
        If TransPara.TransP("UnitTitle").indexof("中") >= 0 And Mid(TransPara.TransP("Userunit"), 1, 2) = "05" Then
            btnTrans.Enabled = False '台中主計人員不要預算自動轉入
        End If
    End Sub

    Private Sub BtnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSearch.Click
        TabControl1.Enabled = True
        RecMove1.Enabled = True
        Call LoadGridFunc()
        If DataGrid1.CurrentRowIndex > 0 Then
            bm.Position = 1
            Call PutGridToTxt()
        End If
    End Sub

    Sub LoadGridFunc()
        Dim sqlstr, qstr, strD, strC As String
        sqlstr = "SELECT a.bg1+a.bg2+a.bg3+a.bg4+a.bg5 as bgnet, a.*, b.accname  FROM  ACCBG a LEFT OUTER JOIN accname b" & _
                 " ON a.accno = b.accno WHERE accyear=" & nudYear.Value & " and a.accno>='" & _
                 GetAccno(vxtStartNo.Text) & "' and a.accno<='" & GetAccno(vxtEndNo.Text) & "' ORDER BY a.accyear,a.accno"
        mydataset = openmember("", "ACCBG", sqlstr)
        RecMove1.FilesPara = "mastconn=" & mastconn & "|sqlstr=" & sqlstr
        DataGrid1.DataSource = mydataset
        DataGrid1.DataMember = "ACCBG"
        bm = Me.BindingContext(mydataset, "ACCBG")
        RecMove1.Setds = bm
        TabControl1.SelectedIndex = 0
    End Sub

    Sub PutGridToTxt()
        Dim intI, SumUp As Integer
        Dim strI, strColumn1, strColumn2 As String
        txtAccYear.Text = bm.Current("accyear")
        vxtAccno.Text = bm.Current("accno")
        lblAccname.Text = IIf(IsDBNull(bm.Current("accname")), " ", bm.Current("accname"))
        SumUp = 0
        For intI = 1 To 5
            strI = Format(intI, "0")
            strColumn1 = "bg" & strI
            strColumn2 = "up" & strI
            FindControl(Me, "txtbg" & strI).Text = FormatNumber(bm.Current(strColumn1))                'function findcontrol at vbdataio.vb
            FindControl(Me, "txtup" & strI).Text = FormatNumber(bm.Current(strColumn2))
            FindControl(Me, "lblNet" & strI).Text = FormatNumber(bm.Current(strColumn1) + bm.Current(strColumn2))
            SumUp += bm.Current(strColumn2)
        Next
        lblSumBg.Text = FormatNumber(bm.Current("bgnet"))
        lblSumBg.Text = FormatNumber(SumUp)
        lblSumNet.Text = FormatNumber(bm.Current("bgnet") + SumUp)
        txtDeamt.Text = FormatNumber(nz(bm.Current("deamt"), 0))
        txtCramt.Text = FormatNumber(nz(bm.Current("cramt"), 0))
    End Sub

    Sub transjob(ByVal JobName As String, ByVal JobPara As String) Handles RecMove1.TransJob
        Dim LastPos As Integer = bm.Position
        Select Case JobName
            Case "刪除記錄"
                Dim keyvalue, sqlstr, retstr As String
                If TabControl1.SelectedIndex = 0 Then
                    keyvalue = bm.Current("autono")
                Else
                    keyvalue = Trim(lblkey.Text)
                End If

                sqlstr = "delete from ACCBG where autono=" & keyvalue
                retstr = runsql(mastconn, sqlstr)
                If retstr = "sqlok" Then
                    mydataset.Tables("ACCBG").Rows.RemoveAt(JobPara)
                    mydataset.Tables("ACCBG").Clear()
                    Call LoadGridFunc()
                    bm.Position = LastPos
                End If

            Case "回寫記錄cmd"
                Dim keyvalue, sqlstr, retstr, updstr As String
                If TabControl1.SelectedIndex = 0 Then
                    Call PutGridToTxt()
                End If
                keyvalue = lblkey.Text

                RecMove1.GenUpdsql("accyear", txtAccYear.Text, "N")
                RecMove1.GenUpdsql("accno", GetAccno(vxtAccno.Text), "T")
                RecMove1.GenUpdsql("bg1", txtBg1.Text, "N")
                RecMove1.GenUpdsql("bg2", txtBg2.Text, "N")
                RecMove1.GenUpdsql("bg3", txtBg3.Text, "N")
                RecMove1.GenUpdsql("bg4", txtBg4.Text, "N")
                RecMove1.GenUpdsql("bg5", txtBg5.Text, "N")
                RecMove1.GenUpdsql("up1", txtUp1.Text, "N")
                RecMove1.GenUpdsql("up2", txtUp2.Text, "N")
                RecMove1.GenUpdsql("up3", txtUp3.Text, "N")
                RecMove1.GenUpdsql("up4", txtUp4.Text, "N")
                RecMove1.GenUpdsql("up5", txtUp5.Text, "N")
                RecMove1.GenUpdsql("deamt", txtDeamt.Text, "N")
                RecMove1.GenUpdsql("cramt", txtCramt.Text, "N")
                sqlstr = "update ACCBG set " & RecMove1.genupdfunc & " where autono=" & keyvalue
                retstr = runsql(mastconn, sqlstr)
                If retstr = "sqlok" Then
                    mydataset.Tables("ACCBG").Rows.RemoveAt(JobPara)
                    mydataset.Tables("ACCBG").Clear()
                    Call LoadGridFunc()
                    MsgBox("更新成功")
                    bm.Position = LastPos
                End If

            Case "新增記錄"
                Dim keyvalue, sqlstr, retstr, updstr As String
                If TabControl1.SelectedIndex = 0 Then
                    Call PutGridToTxt()
                End If
                RecMove1.GenInsSql("accyear", txtAccYear.Text, "N")
                RecMove1.GenInsSql("accno", GetAccno(vxtAccno.Text), "T")
                RecMove1.GenInsSql("bg1", txtBg1.Text, "N")
                RecMove1.GenInsSql("bg2", txtBg2.Text, "N")
                RecMove1.GenInsSql("bg3", txtBg3.Text, "N")
                RecMove1.GenInsSql("bg4", txtBg4.Text, "N")
                RecMove1.GenInsSql("bg5", txtBg5.Text, "N")
                RecMove1.GenInsSql("up1", txtUp1.Text, "N")
                RecMove1.GenInsSql("up2", txtUp2.Text, "N")
                RecMove1.GenInsSql("up3", txtUp3.Text, "N")
                RecMove1.GenInsSql("up4", txtUp4.Text, "N")
                RecMove1.GenInsSql("up5", txtUp5.Text, "N")
                RecMove1.GenInsSql("deamt", txtDeamt.Text, "N")
                RecMove1.GenInsSql("cramt", txtCramt.Text, "N")
                sqlstr = "insert into ACCBG " & RecMove1.GenInsFunc
                retstr = runsql(mastconn, sqlstr)

                If retstr = "sqlok" Then
                    mydataset.Tables("ACCBG").Rows.RemoveAt(JobPara)
                    mydataset.Tables("ACCBG").Clear()
                    Call LoadGridFunc()
                    bm.Position = LastPos
                    'MsgBox("新增成功")
                Else
                    MsgBox("新增失敗")
                End If
            Case "記錄移動"
                Dim keyvalue, sqlstr, retstr As String
                lblkey.Text = bm.Current("autono")
                Call PutGridToTxt()
                Dirty = False
        End Select
    End Sub


    Private Sub TabControl1_SELECTEDIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        If LoadAfter = False Then Exit Sub
        ' If TabControl1.SelectedIndex = 1 Then Dirty = False
        If Dirty = True Then
            If MsgBox("資料尚未存檔 要放棄嗎?", MsgBoxStyle.DefaultButton1 + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                TabControl1.SelectedIndex = 1
                Dirty = False
            End If
        End If
    End Sub

    Private Sub DataGrid1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.Click
        lblkey.Text = bm.Current("autono")  'keep the old keyvalue
        Call PutGridToTxt()
        Dirty = False
    End Sub

    Private Sub DataGrid1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.DoubleClick
        TabControl1.SelectedIndex = 1
    End Sub


    Private Sub btnTrans_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTrans.Click
        Dim retstr As String
        Dim tempDs As DataSet
        sqlstr = "select * from accbg where accyear=" & txtYear.Text
        tempDs = openmember("", "bgf020", sqlstr)
        If tempDs.Tables(0).Rows.Count > 0 Then
            If MsgBox("你確定要刪除accbg 年度=" & txtYear.Text & ",再由bgp020轉入嗎?", MsgBoxStyle.YesNo) <> MsgBoxResult.Yes Then
                Exit Sub
            End If
        End If
        tempDs = Nothing
        sqlstr = "delete from accbg where accyear=" & txtYear.Text
        retstr = runsql(mastconn, sqlstr)
        sqlstr = "INSERT INTO ACCBG " & _
                 "SELECT " & txtYear.Text & " AS accyear, accno, bg1, bg2, bg3, bg4, bg5, 0 AS up1, 0 AS up2, 0 AS up3, " & _
                 " 0 AS up4, 0 AS up5,'' AS remark, 0 AS deamt, 0 AS cramt FROM BGP020 WHERE userid = '全部' and len(accno)<=9 order by accno"
        retstr = runsql(mastconn, sqlstr)
        If retstr = "sqlok" Then
            MsgBox("轉入完成")
        End If
        btnTrans.Enabled = False

        btnCancel.Text = "結束"
    End Sub


    Private Sub btnTransTo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTransTo.Click
        Dim retstr, tempDate, strRemark As String
        Dim decAmt As Decimal
        If MsgBox("你確定要刪除accbgbook 年度=" & txtYearTo.Text & IIf(rdbRev.Checked, "收入", "收入") & _
           ",再由accbg轉入嗎?", MsgBoxStyle.YesNo) <> MsgBoxResult.Yes Then
            Exit Sub
        End If
        '先清該年度收入or收入資料
        sqlstr = "delete from accbgbook where accyear=" & txtYearTo.Text & " and left(accno,1)='" & IIf(rdbRev.Checked, "4", "5") & "'"
        retstr = runsql(mastconn, sqlstr)
        '由accbg逐筆按四季資料insert至accbgbook
        sqlstr = "SELECT * from accbg where accyear=" & txtYearTo.Text & " and left(accno,1)='" & IIf(rdbRev.Checked, "4", "5") & _
                 "' and len(accno)>=5"
        mydataset = openmember("", "ACCBG", sqlstr)
        With mydataset.Tables("accbg")
            For intI As Integer = 0 To .Rows.Count - 1
                For intQ As Integer = 1 To 4
                    If intQ = 1 Then
                        tempDate = txtYearTo.Text & "/1/1"
                        decAmt = nz(.Rows(intI).Item("bg1"), 0) + nz(.Rows(intI).Item("up1"), 0)
                        strRemark = "第一季預算分配數"
                    End If
                    If intQ = 2 Then
                        tempDate = txtYearTo.Text & "/4/1"
                        decAmt = nz(.Rows(intI).Item("bg2"), 0) + nz(.Rows(intI).Item("up2"), 0)
                        strRemark = "第二季預算分配數"
                    End If
                    If intQ = 3 Then
                        tempDate = txtYearTo.Text & "/7/1"
                        decAmt = nz(.Rows(intI).Item("bg3"), 0) + nz(.Rows(intI).Item("up3"), 0)
                        strRemark = "第三季預算分配數"
                    End If
                    If intQ = 4 Then
                        tempDate = txtYearTo.Text & "/10/1"
                        decAmt = nz(.Rows(intI).Item("bg4"), 0) + nz(.Rows(intI).Item("up4"), 0)
                        strRemark = "第四季預算分配數"
                    End If
                    If decAmt <> 0 Then   '有預算分配數才insert 
                        GenInsSql("accyear", txtYearTo.Text, "N")
                        GenInsSql("accno", .Rows(intI).Item("accno"), "T")
                        GenInsSql("date_2", tempDate, "D")
                        GenInsSql("kind", " ", "T")
                        GenInsSql("NO_2_NO", 0, "N")
                        GenInsSql("DC", " ", "T")
                        GenInsSql("REMARK", strRemark, "T")
                        GenInsSql("amt", decAmt, "N")
                        GenInsSql("COTN_CODE", " ", "T")
                        sqlstr = "insert into ACCBGBOOK " & GenInsFunc
                        retstr = runsql(mastconn, sqlstr)
                    End If
                Next
            Next
        End With

        MsgBox("轉出完成")
        'btnTransTo.Enabled = False
        btnCancelTo.Text = "結束"
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click, btnCancelTo.Click, btnCancel.Click
        Me.Close()
    End Sub

    Private Sub txtBg1_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBg1.LostFocus, txtUp1.LostFocus, _
                txtBg2.LostFocus, txtUp2.LostFocus, txtBg3.LostFocus, txtUp3.LostFocus, txtBg4.LostFocus, txtUp4.LostFocus, txtBg5.LostFocus, txtUp5.LostFocus
        If Not IsNumeric(sender.text) And Trim(sender.text) <> "" Then
            MsgBox("請輸入正數字")
            sender.focus()
        Else
            Dim strI As String = Mid(sender.name, 6, 1)
            If sender.text <> "" Then
                sender.text = FormatNumber(ValComa(sender.text), 2)
            End If
            FindControl(Me, "lblnet" & strI).Text = FormatNumber(ValComa(FindControl(Me, "txtbg" & strI).Text) + ValComa(FindControl(Me, "txtup" & strI).Text))
            lblSumBg.Text = FormatNumber(ValComa(txtBg1.Text) + ValComa(txtBg2.Text) + ValComa(txtBg3.Text) + ValComa(txtBg4.Text) + ValComa(txtBg5.Text), 2)
            lblSumUp.Text = FormatNumber(ValComa(txtUp1.Text) + ValComa(txtUp2.Text) + ValComa(txtUp3.Text) + ValComa(txtUp4.Text) + ValComa(txtUp5.Text), 2)
            lblSumNet.Text = FormatNumber(ValComa(lblSumBg.Text) + ValComa(lblSumUp.Text), 2)
        End If
    End Sub


    Private Sub txtAccYear_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccYear.LostFocus, _
                           txtDeamt.LostFocus, txtCramt.LostFocus, txtAcuYear.LostFocus
        If Not IsNumeric(sender.text) And Trim(sender.text) <> "" Then
            MsgBox("請輸入正數字")
            sender.focus()
        End If
    End Sub

    '由五級累計至432級  '97/1/31
    Private Sub btnRunAcu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRunAcu.Click
        Dim retstr, strTemp As String

        '先delete   accbg 
        sqlstr = "delete from accbg where accyear=" & txtAcuYear.Text & " and len(accno)<=5 "
        If rdoR.Checked Then
            strTemp = " and left(accno,1)='4' "
        End If
        If rdoP.Checked Then
            strTemp = " and left(accno,1)='5' "
        End If
        If rdoRP.Checked Then
            strTemp = " and left(accno,1)>='4' "
        End If
        sqlstr += strTemp
        retstr = runsql(mastconn, sqlstr)

        '統計至四級
        sqlstr = "INSERT INTO ACCBG select a.accyear, a.accno, a.bg1, a.bg2, a.bg3, a.bg4, a.bg5, " & _
                 "a.up1, a.up2, a.up3, a.up4, a.up5, '' as remark, a.deamt, a.cramt from " & _
                 "(SELECT accyear, substring(accno, 1, 5) AS accno,  " & _
                 "SUM(bg1) AS bg1, SUM(bg2) AS bg2, SUM(bg3) AS bg3, SUM(bg4) AS bg4, sum(bg5) as bg5, " & _
                 "SUM(up1) AS up1, SUM(up2) AS up2, SUM(up3) AS up3, SUM(up4) AS up4, sum(up5) as up5, " & _
                 "SUM(deamt) AS deamt, SUM(cramt) AS cramt from accbg " & _
                 "where accyear=" & txtAcuYear.Text & " and len(accno)=7 " & strTemp & _
                 "GROUP BY accyear, substring(accno, 1, 5)) a "
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then
            MsgBox("統計四級 error " & sqlstr)
            Exit Sub
        End If
        '統計至三級
        sqlstr = "INSERT INTO ACCBG select a.accyear, a.accno, a.bg1, a.bg2, a.bg3, a.bg4, a.bg5, " & _
                 "a.up1, a.up2, a.up3, a.up4, a.up5, '' as remark, a.deamt, a.cramt from " & _
                 "(SELECT accyear, substring(accno, 1, 3) AS accno,  " & _
                 "SUM(bg1) AS bg1, SUM(bg2) AS bg2, SUM(bg3) AS bg3, SUM(bg4) AS bg4, sum(bg5) as bg5, " & _
                 "SUM(up1) AS up1, SUM(up2) AS up2, SUM(up3) AS up3, SUM(up4) AS up4, sum(up5) as up5, " & _
                 "SUM(deamt) AS deamt, SUM(cramt) AS cramt from accbg " & _
                 "where accyear=" & txtAcuYear.Text & " and len(accno)=5 " & strTemp & _
                 "GROUP BY accyear, substring(accno, 1, 3)) a "
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then
            MsgBox("統計三級 error " & sqlstr)
            Exit Sub
        End If        '統計至二級
        sqlstr = "INSERT INTO ACCBG select a.accyear, a.accno, a.bg1, a.bg2, a.bg3, a.bg4, a.bg5, " & _
                 "a.up1, a.up2, a.up3, a.up4, a.up5, '' as remark, a.deamt, a.cramt from " & _
                 "(SELECT accyear, substring(accno, 1, 2) AS accno,  " & _
                 "SUM(bg1) AS bg1, SUM(bg2) AS bg2, SUM(bg3) AS bg3, SUM(bg4) AS bg4, sum(bg5) as bg5, " & _
                 "SUM(up1) AS up1, SUM(up2) AS up2, SUM(up3) AS up3, SUM(up4) AS up4, sum(up5) as up5, " & _
                 "SUM(deamt) AS deamt, SUM(cramt) AS cramt from accbg " & _
                 "where accyear=" & txtAcuYear.Text & " and len(accno)=3 " & strTemp & _
                 "GROUP BY accyear, substring(accno, 1, 2)) a "
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then
            MsgBox("統計二級 error " & sqlstr)
            Exit Sub
        End If
        MsgBox("作業完成")
    End Sub
End Class
