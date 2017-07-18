Public Class BG010BATCH
    Dim confid, confunit As String, LoadAfter, Dirty As Boolean
    Dim intNo, intI As Integer    '請購序號
    Dim sYear, sSeason As Integer   '請購年度
    Dim sqlstr, retstr As String
    Dim RowI, intYear As Integer
    Dim isArea As Boolean = False   '控制是否是嘉南  要有管理處
    Dim bm, bmS As BindingManagerBase, myDataSet, myDataSetS, AccnoDataSet, TempDataSet, psDataSet, subjectDataSet As DataSet
    Dim AreaDs, UnitSDs, UnitEDs As DataSet
    Dim UserId, UserName, UserUnit, UserDate As String
    Dim strAccno As String = ""     '存放請購科目
    Dim strUnitName, strCashier, strAccname As String
    Dim ComeFromPay000 As Boolean = False
    Private Sub BG010Batch_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If TransPara.TransP("UnitTitle").indexof("嘉南") >= 0 Then
            isArea = True
            gbxAreaA.Visible = True
            '將管理處置combobox 
            sqlstr = "SELECT area, area+areaname as areaname  FROM area "
            AreaDs = openmember("", "area", sqlstr)
            If AreaDs.Tables("area").Rows.Count = 0 Then
                cboAreaA.Text = "尚無受款人片語"
            Else
                cboAreaA.DataSource = AreaDs.Tables("area")
                cboAreaA.DisplayMember = "areaname"  '顯示欄位
                cboAreaA.ValueMember = "area"     '欄位值
                'cboareaa.SelectionLength = 60
            End If
        End If
        If TransPara.TransP("UnitTitle").indexof("彰化") >= 0 Then
            txtUnitS.Text = "1"
            txtUnitE.Text = "399"
        End If

        UserId = TransPara.TransP("userid")
        UserDate = TransPara.TransP("userDATE")
        UserUnit = TransPara.TransP("userunit")
        sYear = GetYear(UserDate)      '請購年度
        sSeason = Season(UserDate)  '判定季份
        LoadAfter = True
        dtpDate1a.Value = UserDate

        '將所有可請購科目置combobox 
        Call PutAccnoToCbo()

        '將單位片語置combobox   
        sqlstr = "SELECT psstr  FROM psname where unit='" & UserUnit & "' and seq<>9999 order by psstr"
        psDataSet = openmember("", "psname", sqlstr)
        If psDataSet.Tables("psname").Rows.Count = 0 Then
            cboRemark.Text = "尚無片語"
        Else
            cboRemark.DataSource = psDataSet.Tables("psname")
            cboRemark.DisplayMember = "psstr"  '顯示欄位
            cboRemark.ValueMember = "psstr"     '欄位值
            cboRemark.SelectionLength = 60
        End If
        '將單位置combobox 
        sqlstr = "SELECT unit, unit+shortname as unitname  FROM unittable "
        UnitSDs = openmember("", "unit", sqlstr)
        If UnitSDs.Tables("unit").Rows.Count = 0 Then
            cboUnitS.Text = "尚無受款人片語"
        Else
            cboUnitS.DataSource = UnitSDs.Tables("unit")
            cboUnitS.DisplayMember = "unitname"  '顯示欄位
            cboUnitS.ValueMember = "unit"     '欄位值
        End If
        '將單位置combobox 
        sqlstr = "SELECT unit, unit+shortname as unitname  FROM unittable "
        UnitEDs = openmember("", "unit", sqlstr)
        If UnitEDs.Tables("unit").Rows.Count = 0 Then
            cboUnitE.Text = "尚無受款人片語"
        Else
            cboUnitE.DataSource = UnitEDs.Tables("unit")
            cboUnitE.DisplayMember = "unitname"  '顯示欄位
            cboUnitE.ValueMember = "unit"     '欄位值
        End If
        TabControl1.Enabled = True

        Call LoadGridFunc()

        If myDataSet.Tables("BGF020").Rows.Count > 0 Then
            bm.Position = myDataSet.Tables("BGF020").Rows.Count - 1
            strAccno = Format(nz(bm.Current("accyear"), 0), "000") & " " & Mid(bm.Current("accno") & Space(17), 1, 17)
            cboAccno.SelectedValue = strAccno
            bm.Position = 1
        End If

    End Sub

    Sub PutAccnoToCbo()
        Dim sqlstr, StrBg, strSeasonBg As String
        Select Case sSeason
            Case Is = 1
                StrBg = "Ltrim(str(a.bg1+a.up1-a.totuse))"
                strSeasonBg = "Ltrim(str(a.bg1+a.up1-a.totper-a.totuse))"
            Case Is = 2
                StrBg = "Ltrim(str(a.bg1+a.bg2+a.up1+a.up2-a.totuse))"
                strSeasonBg = "Ltrim(str(a.bg1+a.bg2+a.up1+a.up2-a.totuse-a.totper))"
            Case Is = 3
                StrBg = "Ltrim(str(a.bg1+a.bg2+a.bg3+a.up1+a.up2+a.up3-a.totuse))"
                strSeasonBg = "Ltrim(str(a.bg1+a.bg2+a.bg3+a.up1+a.up2+a.up3-a.totuse-a.totper))"
            Case Is = 4
                StrBg = "Ltrim(str(a.bg1+a.bg2+a.bg3+a.bg4+a.up1+a.up2+a.up3+a.up4-a.totuse))"
                strSeasonBg = "Ltrim(str(a.bg1+a.bg2+a.bg3+a.bg4+a.up1+a.up2+a.up3+a.up4-a.totuse-a.totper))"
        End Select
        sqlstr = "SELECT right('0'+ltrim(str(a.accyear)),3) + ' ' +left(a.accno+space(17),17) as bgf010key, " & _
                 "CASE WHEN len(a.accno)=17 THEN " & _
                    "right('0'+ltrim(str(a.accyear)),3) +' '+left(a.accno+space(17),17)+c.accname+'-'+b.accname" & _
                    "+' 年預餘:'+Ltrim(str(a.bg1+a.bg2+a.bg3+a.bg4+a.up1+a.up2+a.up3+a.up4-a.totper-a.totuse))+" & _
                    "+' 季預餘:'+" & strSeasonBg & "+' 季支餘:'+" & StrBg & _
                 " WHEN len(a.accno)<17 THEN " & _
                    "right('0'+ltrim(str(a.accyear)),3) +' '+left(a.accno+space(17),17)+b.accname" & _
                    "+' 年預餘:'+Ltrim(str(a.bg1+a.bg2+a.bg3+a.bg4+a.up1+a.up2+a.up3+a.up4-a.totper-a.totuse))+" & _
                    "+' 季預餘:'+" & strSeasonBg & "+' 季支餘:'+" & StrBg & _
                 " END AS bgf010data " & _
                 "FROM bgf010 a INNER JOIN ACCNAME b ON a.ACCNO = b.ACCNO INNER JOIN ACCNAME c ON LEFT(a.ACCNO, 16) = c.ACCNO " & _
                 "WHERE a.ctrl<>'Y' AND b.STAFF_NO = '" & Trim(UserId) & "' order by a.accyear,a.accno"

        AccnoDataSet = openmember("", "bgf010", sqlstr)
        If AccnoDataSet.Tables("BGF010").Rows.Count = 0 Then
            cboAccno.Text = "尚無可請購科目"
        Else
            cboAccno.DataSource = AccnoDataSet.Tables("bgf010")
            cboAccno.DisplayMember = "bgf010data"  '顯示欄位
            cboAccno.ValueMember = "bgf010key"     '欄位值
        End If
    End Sub

    Sub LoadGridFunc()
        Dim sqlstr, qstr, strD, strC As String
        sqlstr = "SELECT a.*, b.accname from bgf020 a left outer join accname b on a.accno=b.accno where a.accyear=999" '產生空結構
        myDataSet = openmember("", "BGF020", sqlstr)
        DataGrid1.DataSource = myDataSet
        DataGrid1.DataMember = "BGF020"
        bm = Me.BindingContext(myDataSet, "BGF020")
        TabControl1.SelectedIndex = 0
    End Sub

    Private Sub cboRemark_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboRemark.GotFocus
        intI = cboRemark.FindString(Trim(txtRemarka.Text))  '設定combo起值
        cboRemark.SelectedIndex = intI
    End Sub

    Private Sub cboRemark_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboRemark.SelectionChangeCommitted
        txtRemarka.Text = cboRemark.Text
    End Sub
    '新增一筆請購資料
    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Call LoadGridFunc() '產生空結構
        btnAddbatch.Enabled = True
        DataGrid1.Enabled = True
        intYear = Val(Mid(cboAccno.SelectedValue, 1, 3))            '請購年度
        strAccno = Trim(Mid(cboAccno.SelectedValue, 5, 17))    '請購科目
        strAccname = Trim(Mid(cboAccno.Text, 23, 50)) '請購科目名稱
        If strAccname.IndexOf("年預") > 0 Then
            strAccname = Trim(Mid(strAccname, 1, strAccname.IndexOf("年預")))
        End If
        '由單位檔逐筆增入DATAGRID
        sqlstr = "select * from unittable where unit>='" & txtUnitS.Text & "' and unit<='" & txtUnitE.Text & "'"
        TempDataSet = openmember("", "unittable", sqlstr)
        For RowI = 0 To TempDataSet.Tables("unittable").Rows.Count - 1
            With TempDataSet.Tables("unittable").Rows(RowI)
                strUnitName = .Item("shortname")
                strCashier = .Item("cashier")
                Call GridAdd()
            End With
        Next
        TabControl1.SelectedIndex = 1
        MsgBox("請逐筆輸入請購金額")
    End Sub

    Function GridAdd()
        Dim nr As DataRow
        nr = myDataSet.Tables("BGF020").NewRow()
        nr("bgno") = ""
        nr("accyear") = intYear
        nr("accno") = strAccno
        nr("remark") = Trim(strUnitName) & txtRemarka.Text & "  " & strCashier
        nr("subject") = strCashier
        nr("amt1") = 0
        nr("accname") = strAccname
        'nr("kind") = bm.Current("kind")
        'nr("area") = txtAreaA.Text
        myDataSet.Tables("BGF020").Rows.Add(nr)                          '增行至source grid
    End Function

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        myDataSet.Tables("BGF020").Rows.RemoveAt(bm.Position.ToString)  '將target grid刪行
    End Sub

    '整批新增(insert bgf020)
    Private Sub btnAddbatch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddbatch.Click
        Dim skind, TDC, sDC, strBgno As String  'skind 1:收入傳票 2:支出傳票 sDC 1:借方金額 2:貸方金額
        Dim intTotamt, TbgAmt, intAmt As Decimal
        intTotamt = 0
        For RowI = 0 To bm.Count - 1
            bm.Position = RowI
            If ValComa(nz(bm.Current("amt1"), 0)) = 0 Then
                MsgBox("金額不可為0")
                Exit Sub
            End If
            intTotamt += ValComa(bm.Current("amt1"))
        Next
        TbgAmt = QueryBGAmt(intYear, strAccno)
        If intTotamt > TbgAmt And TransPara.TransP("flow") <> "Y" Then   '超過年度預算餘額
            MsgBox("預算餘額不足")
            Exit Sub
        End If
        '逐筆新增資料至bgf010 
        For RowI = 0 To bm.Count - 1
            bm.Position = RowI
            intAmt = ValComa(bm.Current("amt1"))
            If Mid(strAccno, 1, 1) = "4" Then
                If intAmt > 0 Then   '收入科目  正數表示收入傳票, 貸方金額
                    skind = "1"
                    sDC = "2"
                Else
                    skind = "2"
                    sDC = "1"
                End If
            Else
                If intAmt > 0 Then  '支出科目  正數表示支出傳票, 借方金額
                    skind = "2"
                    sDC = "1"
                Else
                    skind = "1"
                    sDC = "2"
                End If
            End If
            intNo = RequireNO(mastconn, sYear, "B")    '\accservice\service1.asmx 取用得請購編號
            strBgno = Format(sYear, "000") + Format(intNo, "00000")
            bm.Current("bgno") = strBgno   '填入預算編號供user填入文件
            GenInsSql("BGNO", strBgno, "T")
            GenInsSql("accyear", intYear, "N")
            GenInsSql("accno", strAccno, "T")
            GenInsSql("KIND", skind, "T")
            GenInsSql("DC", sDC, "T")
            GenInsSql("DATE1", dtpDate1a.Value.ToShortDateString, "D")
            GenInsSql("REMARK", nz(bm.Current("remark"), ""), "U")
            GenInsSql("AMT1", intAmt, "N")
            GenInsSql("useableAMT", intAmt, "N")
            GenInsSql("subject", nz(bm.Current("subject"), ""), "U")
            GenInsSql("CLOSEMARK", " ", "T")
            If isArea Then   '嘉南
                GenInsSql("area", nz(bm.Current("area"), ""), "T")
            End If
            sqlstr = "insert into BGF020 " & GenInsFunc
            retstr = runsql(mastconn, sqlstr)
            If retstr <> "sqlok" Then
                MsgBox("新增失敗" + sqlstr)
                Exit Sub
            End If
        Next

        'UPDATE BGF010->TOTPER+AMT 
        sqlstr = "update BGF010 set TOTPER = TOTPER + " & intTotamt & " where ACCYEAR=" & intYear & " AND ACCNO='" & strAccno & "'"
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then
            MsgBox("更新BGF010失敗")
        End If

        btnAddbatch.Enabled = False
        DataGrid1.Enabled = False
        MsgBox("請將請購編號寫入文件, 作業完成")
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        If btnAddbatch.Enabled = True Then
            If MsgBox("是否要回到重新設定整批請購科目或摘要等", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                TabControl1.SelectedIndex = 0
            End If
        Else
            TabControl1.SelectedIndex = 0
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub


    '只有嘉南設有管理處
    Private Sub cboarea_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboAreaA.SelectionChangeCommitted
        txtAreaA.Text = cboAreaA.SelectedValue
    End Sub

    Private Sub txtAreaa_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtAreaA.KeyUp
        If Len(txtAreaA.Text) = 2 Then
            cboAreaA.SelectedValue = txtAreaA.Text
        End If
    End Sub

    Private Sub cbounits_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboUnitS.SelectionChangeCommitted
        txtUnitS.Text = cboUnitS.SelectedValue
    End Sub
    Private Sub cbounite_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboUnitE.SelectionChangeCommitted
        txtUnitE.Text = cboUnitE.SelectedValue
    End Sub

    '顯示單位名稱
    Private Sub 為txtunits_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtUnitS.LostFocus
        sqlstr = "select * from unittable where unit='" & txtUnitS.Text & "'"
        TempDataSet = openmember("", "unittable", sqlstr)
        If TempDataSet.Tables("unittable").Rows.Count > 0 Then
            lblshortNameS.Text = TempDataSet.Tables("unittable").Rows(0).Item("shortname")
            lblcashierS.Text = TempDataSet.Tables("unittable").Rows(0).Item("cashier")
        Else
            lblshortNameS.Text = ""
            lblcashierS.Text = ""
        End If
    End Sub
    Private Sub txtunite_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtUnitE.LostFocus
        sqlstr = "select * from unittable where unit='" & txtUnitE.Text & "'"
        TempDataSet = openmember("", "unittable", sqlstr)
        If TempDataSet.Tables("unittable").Rows.Count > 0 Then
            lblshortNameE.Text = TempDataSet.Tables("unittable").Rows(0).Item("shortname")
            lblcashierE.Text = TempDataSet.Tables("unittable").Rows(0).Item("cashier")
        Else
            lblshortNameE.Text = ""
            lblcashierE.Text = ""
        End If
    End Sub
   
End Class
