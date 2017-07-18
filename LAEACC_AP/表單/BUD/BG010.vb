Public Class BG010
    Dim confid, confunit As String, LoadAfter, Dirty As Boolean
    Dim intNo, intI As Integer    '請購序號
    Dim sYear, sSeason As Integer   '請購年度
    Dim sqlstr As String
    Dim i As Integer
    Dim isArea As Boolean = False   '控制是否是嘉南  要有管理處
    Dim bm, bmS, bmS2 As BindingManagerBase, myDataSet, myDataSetS, myDataSetS2, AccnoDataSet, TempDataSet, psDataSet, subjectDataSet As DataSet
    Dim AreaDs As DataSet
    Dim UserId, UserName, UserUnit, UserDate As String
    Dim strAccno As String = ""     '存放上筆請購科目
    Dim ComeFromPay000 As Boolean = False '控制資料來源自差勤系統
    Dim ComeFromCBGf020 As Boolean = False '控制資料來源自工作站預算系統

    Private Sub BG010_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If TransPara.TransP("UnitTitle").indexof("嘉南") >= 0 Then
            isArea = True
            gbxArea.Visible = True
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

        UserId = TransPara.TransP("userid")
        UserDate = TransPara.TransP("userDATE")
        UserUnit = TransPara.TransP("userunit")
        sYear = GetYear(UserDate)
        sSeason = Season(UserDate)  '判定季份
        LoadAfter = True
        dtpDate1a.Value = UserDate
        txtByear.Text = sYear

        '將所有可請購科目置combobox 
        Call PutAccnoToCbo()

        '將單位片語置combobox   
        'sqlstr = "SELECT left(psstr + space(50),50) as psstr FROM psname where unit='" & UserUnit & "' order by psstr"
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

        '將單位受款人置combobox   seq=9999為受款人專用
        sqlstr = "SELECT psstr  FROM psname where unit='" & UserUnit & "' and seq=9999 order by psstr"
        subjectDataSet = openmember("", "subject", sqlstr)
        If subjectDataSet.Tables("subject").Rows.Count = 0 Then
            cboSubjecta.Text = "尚無受款人片語"
        Else
            cboSubjecta.DataSource = subjectDataSet.Tables("subject")
            cboSubjecta.DisplayMember = "psstr"  '顯示欄位
            cboSubjecta.ValueMember = "psstr"     '欄位值
            cboSubjecta.SelectionLength = 60
        End If

        TabControl1.Enabled = True
        RecMove1.Enabled = False
        'TabControl1.TabPages

        Call LoadGridFunc()

        If myDataSet.Tables("BGF020").Rows.Count > 0 Then
            bm.Position = myDataSet.Tables("BGF020").Rows.Count - 1
            'strAccno = Str(bm.Current("accyear")) & " " & Mid(bm.Current("accno") & Space(17), 1, 17)
            strAccno = Format(nz(bm.Current("accyear"), 0), "000") & " " & Mid(bm.Current("accno") & Space(17), 1, 17)
            cboAccno.SelectedValue = strAccno
            bm.Position = 1
            Call PutGridToTxt()
        End If
        '新竹水利會  formload 先將差勤資料load至datagrid,避免user需大批處理差勤請購時,造成server負擔
        If TransPara.TransP("UnitTitle").indexof("新竹") >= 0 Or _
           TransPara.TransP("UnitTitle").indexof("苗栗") >= 0 Or _
           TransPara.TransP("UnitTitle").indexof("彰化") >= 0 Or _
           TransPara.TransP("UnitTitle").indexof("高雄") >= 0 Or _
           TransPara.TransP("UnitTitle").indexof("桃園") >= 0 Or _
           TransPara.TransP("UnitTitle").indexof("花蓮") >= 0 Or _
           TransPara.TransP("UnitTitle").indexof("嘉南") >= 0 Or _
           TransPara.TransP("UnitTitle").indexof("東") >= 0 Or _
           TransPara.TransP("UnitTitle").indexof("公") >= 0 Or _
           TransPara.TransP("UnitTitle").indexof("屏東") >= 0 Or _
           TransPara.TransP("UnitTitle").indexof("七星") >= 0 Or _
           TransPara.TransP("UnitTitle").indexof("北基") >= 0 Or _
           TransPara.TransP("UnitTitle").indexof("石門") >= 0 Then
            btnPay000.Visible = True
            TabControl1.TabPages(3).Text = " 差勤 "
            sqlstr = "select a.*, b.accname from pay000 a left outer join accname b " & _
                        "on a.accno=b.accno where a.bgno is null and left(a.unit,3)='" & Mid(UserUnit, 1, 3) & _
                        "'  order by a.accyear, a.batno"
            myDataSetS = openmember("", "pay000", sqlstr)
            dtgPay000.DataSource = myDataSetS
            dtgPay000.DataMember = "pay000"
            bmS = Me.BindingContext(myDataSetS, "pay000")
        End If

        '嘉南水利會  formload 先將工作站預算系統資料load至datagrid (判斷caccname.account_no=本會推算人員)
        If TransPara.TransP("UnitTitle").indexof("嘉南") >= 0 Then
            btnCbgf020.Visible = True
            TabControl1.TabPages(4).Text = "管理處請購"
            sqlstr = "select a.*, b.accname, b.bookaccno from cbgf020 a left outer join caccname b " & _
                        "on a.accno=b.accno where a.usebgno is null " & _
                        "and (b.account_no='" & Trim(UserId) & "' or b.account_no='' or b.account_no is null )" & _
                        "order by a.accno"
            myDataSetS2 = openmember("", "cbgf020", sqlstr)
            dtgCbgf020.DataSource = myDataSetS2
            dtgCbgf020.DataMember = "cbgf020"
            bmS2 = Me.BindingContext(myDataSetS2, "cbgf020")
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
        'sqlstr = "SELECT str(a.accyear,3)  &  right('0'+ltrim(str(a.accyear)),3) & right('000'+cast(a.accyear as varchar),3)
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
        sqlstr = "SELECT a.bgno, a.accyear, a.accno, a.date1, a.amt1, a.remark," & _
                 "CASE WHEN len(a.accno)=17 THEN b.accname+'('+c.accname+')' " & _
                     " WHEN len(a.accno)<>17 THEN b.accname END AS accname, a.kind, a.subject  "
        If isArea Then  '嘉南有管理處
            sqlstr = sqlstr & ",  a.area FROM BGF020 a "
        Else
            sqlstr = sqlstr & " FROM BGF020 a "
        End If
        sqlstr = sqlstr & _
                 "INNER JOIN ACCNAME b ON a.ACCNO = b.ACCNO" & _
                 " LEFT OUTER JOIN accname c ON LEFT(a.ACCNO, 16) = c.ACCNO and len(a.accno)=17" & _
                 " WHERE b.STAFF_NO = '" & Trim(UserId) & "' AND a.CLOSEMARK <> 'Y' and a.date2 is null" & _
                 " ORDER BY a.BGNO"
        myDataSet = openmember("", "BGF020", sqlstr)
        RecMove1.FilesPara = "mastconn=" & mastconn & "|sqlstr=" & sqlstr
        DataGrid1.DataSource = myDataSet
        DataGrid1.DataMember = "BGF020"
        bm = Me.BindingContext(myDataSet, "BGF020")
        RecMove1.Setds = bm
        TabControl1.SelectedIndex = 0
        RecMove1.Enabled = False
        lblRecNo.Text = bm.Count
    End Sub

    Sub PutGridToTxt()
        Dim intI, SumUp As Integer
        Dim strI, strColumn1, strColumn2 As String
        If bm.Position < 0 Then Exit Sub
        If IsDBNull(bm.Current("accno")) Then Exit Sub
        lblBgno.Text = nz(bm.Current("bgno"), "")  '不允許修改bgno,accyear,accno
        lblYear.Text = nz(bm.Current("accyear"), "")
        lblAccno.Text = nz(bm.Current("accno"), "")
        lblKind.Text = nz(bm.Current("kind"), "")
        lblAccname.Text = IIf(IsDBNull(bm.Current("accname")), " ", bm.Current("accname"))
        dtpDate1.Value = nz(bm.Current("date1"), "01/1/1")
        txtDate1.Text = nz(bm.Current("date1"), "000/00/00")
        txtRemark.Text = nz(bm.Current("remark"), " ")
        txtAmt.Text = FormatNumber(nz(bm.Current("amt1"), 0), 0)
        lblAmt.Text = FormatNumber(nz(bm.Current("amt1"), 0), 0)
        txtSubject.Text = nz(bm.Current("subject"), " ")
        lblkey.Text = Trim(bm.Current("bgno"))
        If isArea Then
            txtArea.Text = nz(bm.Current("area"), " ")
        End If
        lblGrade6.Text = ""
    End Sub

    Sub transjob(ByVal JobName As String, ByVal JobPara As String) Handles RecMove1.TransJob
        Dim LastPos As Integer = bm.Position
        Select Case JobName
            Case "刪除記錄"
                Dim keyvalue, sqlstr, retstr As String
                If TabControl1.SelectedIndex = 0 Then
                    keyvalue = bm.Current("bgno")
                Else
                    keyvalue = Trim(lblkey.Text)
                End If
                sqlstr = "select * from bgf030 where bgno='" & keyvalue & "'"
                TempDataSet = openmember("", "temp", sqlstr)
                If TempDataSet.Tables("temp").Rows.Count > 0 Then
                    MsgBox("此筆已有分次開支資料:" & TempDataSet.Tables("temp").Rows(0).Item("remark") & _
                    FormatNumber(TempDataSet.Tables("temp").Rows(0).Item("useamt"), 0))
                    Exit Sub
                End If
                sqlstr = "update BGF010 set TOTPER = TOTPER - " & ValComa(lblAmt.Text) & _
                         " where ACCYEAR=" & Trim(lblYear.Text) & " AND ACCNO='" & Trim(lblAccno.Text) & "'"
                retstr = runsql(mastconn, sqlstr)
                If retstr <> "sqlok" Then
                    MsgBox("更新BGF010失敗" & sqlstr)
                End If

                '清PAY000.BGNO   bgno已有年度
                sqlstr = "update PAY000 set BGNO=NULL where BGNO='" & keyvalue & "'"
                retstr = runsql(mastconn, sqlstr)

                sqlstr = "delete from BGF020 where bgno='" & keyvalue & "'"
                retstr = runsql(mastconn, sqlstr)
                If retstr = "sqlok" Then
                    myDataSet.Tables("BGF020").Rows.RemoveAt(JobPara)
                    'myDataSet.Tables("BGF020").Clear()
                    'Call LoadGridFunc()
                    bm.Position = LastPos
                    TabControl1.SelectedIndex = 0
                End If

            Case "回寫記錄cmd"
                Dim keyvalue, sqlstr, retstr, updstr, TDC, sKind, sDc As String
                Dim tBgamt As Decimal

                keyvalue = Trim(lblkey.Text)
                If ValComa(txtAmta.Text) > ValComa(lblAmt.Text) And Mid(lblAccno.Text, 1, 1) <> "4" Then
                    tBgamt = QueryBGAmt(Val(lblYear.Text), lblAccno.Text)
                    If tBgamt + ValComa(lblAmt.Text) - ValComa(txtAmt.Text) < 0 And TransPara.TransP("flow") <> "Y" Then
                        MsgBox("預算餘額不足")
                        Exit Sub
                    End If
                End If

                If ValComa(txtAmta.Text) <> ValComa(lblAmt.Text) Then
                    sqlstr = "update BGF010 set TOTPER = TOTPER - " & ValComa(lblAmt.Text) & " + " & ValComa(txtAmt.Text) & _
                             " where ACCYEAR=" & Trim(lblYear.Text) & " AND ACCNO='" & Trim(lblAccno.Text) & "'"
                    retstr = runsql(mastconn, sqlstr)
                    If retstr <> "sqlok" Then
                        MsgBox("更新BGF010失敗" & sqlstr)
                    End If
                End If
                'If (ValComa(lblAmt.Text) < 0 And ValComa(txtAmt.Text) > 0) Or (ValComa(lblAmt.Text) > 0 And ValComa(txtAmt.Text) < 0) Then
                '    RecMove1.GenUpdsql("kind", IIf(lblKind.Text = "1", "2", "1"), "T")
                'End If
                'If ValComa(txtAmt.Text) < 0 Then  '金額為負數   
                '    If Mid(lblAccno.Text, 1, 1) = "1" Or Mid(lblAccno.Text, 1, 1) = "5" Then  '借方科目
                '        RecMove1.GenUpdsql("DC", "2", "T")
                '    Else
                '        RecMove1.GenUpdsql("DC", "1", "T")
                '    End If
                'End If
                If Mid(lblAccno.Text, 1, 1) = "4" Then   '99/12/6 update 
                    If ValComa(txtAmt.Text) > 0 Then   '收入科目  正數表示收入傳票, 貸方金額
                        sKind = "1"
                        sDc = "2"
                    Else
                        sKind = "2"
                        sDc = "1"
                    End If
                Else
                    If ValComa(txtAmt.Text) > 0 Then  '支出科目  正數表示支出傳票, 借方金額
                        sKind = "2"
                        sDc = "1"
                    Else
                        sKind = "1"
                        sDc = "2"
                    End If
                End If
                RecMove1.GenUpdsql("kind", sKind, "T")
                RecMove1.GenUpdsql("DC", sDc, "T")
                RecMove1.GenUpdsql("date1", txtDate1.Text, "D")
                RecMove1.GenUpdsql("remark", txtRemark.Text, "U")
                RecMove1.GenUpdsql("subject", txtSubject.Text, "U")
                RecMove1.GenUpdsql("amt1", txtAmt.Text, "N")
                RecMove1.GenUpdsql("useableamt", txtAmt.Text, "N")
                If isArea Then
                    RecMove1.GenUpdsql("area", txtArea.Text, "T")
                End If
                sqlstr = "update BGF020 set " & RecMove1.genupdfunc & " where bgno='" & keyvalue & "'"
                retstr = runsql(mastconn, sqlstr)
                If retstr = "sqlok" Then
                    Call LoadGridFunc()
                    'MsgBox("更新成功")
                    bm.Position = LastPos
                    TabControl1.SelectedIndex = 0
                Else
                    MsgBox("更新失敗" & sqlstr)
                End If

            Case "新增記錄"
                lblNo.Text = Format(sYear, "000") + Format(QueryNO(sYear, "B") + 1, "00000") 'vbdataid.vb 讀取請購編號
                TabControl1.SelectedIndex = 2

            Case "記錄移動"
                'Dim keyvalue, sqlstr, retstr As String
                If bm.Position > 0 Then
                    lblkey.Text = nz(bm.Current("bgno"), "")
                    Call PutGridToTxt()
                End If
                Dirty = False
        End Select
    End Sub

    Private Sub cboRemark_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboRemark.GotFocus
        intI = cboRemark.FindString(Trim(txtRemarka.Text))  '設定combo起值
        cboRemark.SelectedIndex = intI
    End Sub

    Private Sub cboRemark_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboRemark.SelectionChangeCommitted
        txtRemarka.Text = cboRemark.Text
    End Sub

    Private Sub cboSubjecta_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSubjecta.GotFocus
        intI = cboSubjecta.FindString(Trim(txtSubjecta.Text))  '設定combo起值
        cboSubjecta.SelectedIndex = intI
    End Sub
    Private Sub cboSubjecta_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSubjecta.SelectionChangeCommitted
        txtSubjecta.Text = cboSubjecta.Text
    End Sub

    '新增一筆請購資料
    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Dim keyvalue, sqlstr, retstr, updstr, skind, TDC, sDC, J As String  'skind 1:收入傳票 2:支出傳票 sDC 1:借方金額 2:貸方金額
        Dim BYear, I As Integer '請購的預算年度
        Dim Tamt, TBGAmt As Decimal
        Dim BAccno, strMsg As String '請購科目

        '檢查
        Try
            If ValComa(txtAmta.Text) = 0 Then
                MsgBox("金額不可為0")
                Exit Sub
            End If

            BYear = Mid(cboAccno.SelectedValue, 1, 3)
            BAccno = Trim(Mid(cboAccno.SelectedValue, 5, 17))
            'strAccno = Str(BYear) & " " & Mid(BAccno + Space(17), 1, 17)
            strAccno = Format(Val(BYear), "000") & " " & Mid(BAccno + Space(17), 1, 17)
            'cboAccno.SelectedValue = strAccno   ' 100/3/2 delete 
            TBGAmt = QueryBGAmt(BYear, BAccno)
            If Mid(BAccno, 1, 1) = "4" Then   '99/12/6 update 
                If ValComa(txtAmta.Text) > 0 Then   '收入科目  正數表示收入傳票, 貸方金額
                    skind = "1"
                    sDC = "2"
                Else
                    skind = "2"
                    sDC = "1"
                End If
            Else
                If ValComa(txtAmta.Text) > 0 Then  '支出科目  正數表示支出傳票, 借方金額
                    skind = "2"
                    sDC = "1"
                Else
                    skind = "1"
                    sDC = "2"
                End If
            End If
            'TDC = IIf(Mid(BAccno, 1, 1) > "1" And Mid(BAccno, 1, 1) < "5", "2", "1")  '科目本身所屬借貸性質
            'sDC = TDC
            'If Val(txtAmta.Text) < 0 Then     '金額為負數時,借貸要相反
            '    sDC = IIf(TDC = "1", "2", "1")
            'End If
            'skind = IIf(TDC = sDC, "2", "1")  '借方科目且為借方金額=支出傳票,貸方科目且為貸方金額=支出傳票,否則為收入傳票
            'If Mid(BAccno, 1, 1) = "4" Then
            '    skind = IIf(TDC = sDC, "1", "2")  '收入科目,為貸方金額=收入傳票,為借方金額=支出傳票
            'End If

            'If ValComa(txtAmta.Text) < 0 Then  '金額為負數   99/11/23 update   
            '    If Mid(BAccno, 1, 1) = "1" Or Mid(BAccno, 1, 1) = "5" Then  '借方科目
            '        RecMove1.GenInsSql("DC", "2", "T")
            '    Else
            '        RecMove1.GenInsSql("DC", "1", "T")
            '    End If
            'Else
            '    RecMove1.GenInsSql("DC", sDC, "T")
            'End If


            If TBGAmt - ValComa(txtAmta.Text) < 0 And TransPara.TransP("flow") <> "Y" And skind <> "1" Then
                MsgBox("預算餘額不足")
                Exit Sub
            End If

            '資料處理(新增一筆至BGF020 & UPDATE BGF010->TOTPER)
            intNo = RequireNO(mastconn, sYear, "B")    '\accservice\service1.asmx 取用得請購編號
            RecMove1.GenInsSql("BGNO", Format(sYear, "000") + Format(intNo, "00000"), "T")
            RecMove1.GenInsSql("accyear", BYear, "N")
            RecMove1.GenInsSql("accno", BAccno, "T")
            RecMove1.GenInsSql("KIND", skind, "T")
            RecMove1.GenInsSql("DC", sDC, "T")
            RecMove1.GenInsSql("DATE1", dtpDate1a.Value.ToShortDateString, "D")
            RecMove1.GenInsSql("REMARK", txtRemarka.Text, "U")
            RecMove1.GenInsSql("AMT1", txtAmta.Text, "N")
            RecMove1.GenInsSql("useableAMT", txtAmta.Text, "N")
            RecMove1.GenInsSql("subject", txtSubjecta.Text, "U")
            RecMove1.GenInsSql("CLOSEMARK", " ", "T")
            If isArea Then
                RecMove1.GenInsSql("area", txtAreaA.Text, "T")
            End If
            sqlstr = "insert into BGF020 " & RecMove1.GenInsFunc
            retstr = runsql(mastconn, sqlstr)
            If retstr = "sqlok" Then
                'Call LoadGridFunc()
                'MsgBox("新增成功")
            Else
                MsgBox("新增失敗" + sqlstr)
                Exit Sub
            End If
        Catch ex As Exception
            MsgBox("資料錯誤,無法新增")
            Exit Sub
        End Try

        'UPDATE BGF010->TOTPER+AMT 
        sqlstr = "update BGF010 set TOTPER = TOTPER + " & ValComa(txtAmta.Text) & " where ACCYEAR=" & BYear & " AND ACCNO='" & BAccno & "'"
        retstr = runsql(mastconn, sqlstr)
        If retstr <> "sqlok" Then
            MsgBox("更新BGF010失敗")
        End If
        If TransPara.TransP("UnitTitle").IndexOf("屏東") < 0 Then '彰化
            MsgBox("作業完成,請購編號=" + Format(sYear, "000") + Format(intNo, "00000") & vbCrLf & "可繼續新增請購")
        Else
            sqlstr = "SELECT * FROM BGF010 WHERE accyear=" & sYear & " and accno='" & BAccno & "'"
            TempDataSet = openmember("", "TEMP", sqlstr)
            strMsg = ""
            If TempDataSet.Tables(0).Rows.Count > 0 Then
                Dim intUseableBg As Integer = 0
                With TempDataSet.Tables(0).Rows(0)
                    Select Case sSeason
                        Case Is = 1
                            intUseableBg = .Item("bg1") + .Item("up1")
                        Case Is = 2
                            intUseableBg = .Item("bg1") + .Item("bg2") + .Item("up1") + .Item("up2")
                        Case Is = 3
                            intUseableBg = .Item("bg1") + .Item("bg2") + .Item("bg3") + .Item("up1") + .Item("up2") + .Item("up3")
                        Case Is = 4
                            intUseableBg = .Item("bg1") + .Item("bg2") + .Item("bg3") + .Item("bg4") + .Item("up1") + .Item("up2") + .Item("up3") + .Item("up4")
                    End Select
                    strMsg = "預算分配累計數=" + FormatNumber(intUseableBg, 0) + vbCrLf + "    本件請購數=" + txtAmta.Text + vbCrLf + _
                             "    累計執行數=" + FormatNumber(.Item("totper") + .Item("totuse"), 0) + vbCrLf + "        剩餘數=" + FormatNumber(intUseableBg - (.Item("totper") + .Item("totuse")), 0)
                End With
            End If
            MsgBox("作業完成,請購編號=" + Format(sYear, "000") + Format(intNo, "00000") + vbCrLf + strMsg & vbCrLf & "可繼續新增請購")
        End If
        If ComeFromPay000 = True And txtBatNo.Text <> "" Then   'And txtPayno.Text <> ""
            TabControl1.SelectedIndex = 3
            'pay000填入bgno
            sqlstr = "update pay000 set bgno = '" & Format(sYear, "000") + Format(intNo, "00000") & _
                     "' where batno='" & txtBatNo.Text & "' and accyear=" & txtByear.Text
            retstr = runsql(mastconn, sqlstr)
            Call Pay000REmove()   '差勤remove datagrid record  & 清空單據編號
        End If
        If ComeFromCBGf020 = True And txtCbgno.Text <> "" Then
            TabControl1.SelectedIndex = 4
            'cbgf020填入usebgno
            sqlstr = "update cbgf020 set usebgno = '" & Format(sYear, "000") + Format(intNo, "00000") & "' where bgno='" & txtCbgno.Text & "'"
            retstr = runsql(mastconn, sqlstr)
            Call Cbgf020REmove()   '次預算remove datagrid record  & 清空單據編號
        End If

        txtUserid.Text = ""
        Call Button1_Click(New System.Object, New System.EventArgs)   'query 新請購號
        Call PutAccnoToCbo()
        cboAccno.SelectedValue = strAccno
        lblUpRecord.Text = "上筆" & Format(sYear, "000") + Format(intNo, "00000") & "已完成新增 " & _
                           vbCrLf & strAccno & " " & txtRemarka.Text & "  $" & FormatNumber(Val(txtAmta.Text), 0)
        'Call LoadGridFunc()
    End Sub

    Private Sub TabControl1_SELECTEDIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        If LoadAfter = False Then Exit Sub
        ' If TabControl1.SelectedIndex = 1 Then Dirty = False
        If Dirty = True Then
            If MsgBox("資料尚未存檔 要放棄嗎?", MsgBoxStyle.DefaultButton1 + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                TabControl1.SelectedIndex = 1
                Dirty = False
            End If
        End If
        If TabControl1.SelectedIndex = 1 Then
            RecMove1.Enabled = True
        Else
            RecMove1.Enabled = False
        End If
    End Sub

    Private Sub DataGrid1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataGrid1.Click
        lblRecNo.Text = bm.Count & "/" & bm.Position
        If bm.Count > 0 Then
            If bm.Position < 0 Then Exit Sub
            If bm.Count = 0 Then Exit Sub
            lblkey.Text = nz(bm.Current("bgno"), "") 'keep the old keyvalue
            Call PutGridToTxt()
        End If
        Dirty = False
    End Sub

    Private Sub DataGrid1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataGrid1.DoubleClick
        lblRecNo.Text = bm.Count & "/" & bm.Position
        If bm.Count > 0 Then
            If bm.Position < 0 Then Exit Sub
            If bm.Count = 0 Then Exit Sub
            lblkey.Text = nz(bm.Current("bgno"), "") 'keep the old keyvalue
            Call PutGridToTxt()
            TabControl1.SelectedIndex = 1
            RecMove1.Enabled = True
        End If
    End Sub

    '新增請購
    Sub btnInsert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInsert.Click
        lblNo.Text = Format(sYear, "000") + Format(QueryNO(sYear, "B") + 1, "00000") 'vbdataio.vb 讀取請購編號
        If strAccno <> "" Then cboAccno.SelectedValue = strAccno 'default = 上筆請購科目
        ComeFromPay000 = False   '非由差勤取得資料
        TabControl1.SelectedIndex = 2
        lblUpRecord.Text = ""
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        lblNo.Text = Format(sYear, "000") + Format(QueryNO(sYear, "B") + 1, "00000") 'vbdataio.vb 讀取請購編號
        'TabControl1.SelectedIndex = 2    '100/3/2 delete 
    End Sub

    Private Sub dtpDate1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpDate1.ValueChanged
        txtDate1.Text = dtpDate1.Value.ToShortDateString
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        '新增請購頁面之離開
        If ComeFromPay000 = True Then
            TabControl1.SelectedIndex = 3
            txtBatNo.Text = ""  'txtPayno.Text = ""
            txtBatNo.Focus()
        Else
            '離開時,重load資料,因為允許連續新增
            TabControl1.SelectedIndex = 0
            Call LoadGridFunc()
        End If
    End Sub

    Private Sub btnAddRemark_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddRemark.Click
        If txtRemarka.Text <> "" Then
            txtRemarka.Text = Trim(txtRemarka.Text)
            Dim ii As Integer
            ii = MsgBox("將 " & txtRemarka.Text & "增入片語檔", MsgBoxStyle.OKCancel)
            If ii = 1 Then  ' click the ok botton
                sqlstr = "insert into psname (unit, seq, psstr) values ('" & UserUnit & "', 0, '" & txtRemarka.Text & "')"
                runsql(mastconn, sqlstr)
            End If
        End If
    End Sub

    Private Sub btnAddSubject_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddSubject.Click
        If txtSubjecta.Text <> "" Then
            txtSubjecta.Text = Trim(txtSubjecta.Text)
            Dim ii As Integer
            ii = MsgBox("將 " & txtSubjecta.Text & "增入片語檔", MsgBoxStyle.OKCancel)
            If ii = 1 Then  ' click the ok botton
                sqlstr = "insert into psname (unit, seq, psstr) values ('" & UserUnit & "', 9999, '" & txtSubjecta.Text & "')"
                runsql(mastconn, sqlstr)
            End If
        End If
    End Sub

    Private Sub txtAmt_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAmt.LostFocus, txtAmta.LostFocus
        If Not IsNumeric(sender.Text) And Trim(sender.text) <> "" Then
            MsgBox("請輸入數字")
            sender.Focus()
        Else
            sender.text = FormatNumber(ValComa(sender.text), 0)
        End If
    End Sub

    Private Sub btnGrade6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrade6.Click
        '六級餘額查詢
        Dim BYear, I As Integer '請購的預算年度
        Dim BAccno As String '請購科目
        BYear = Mid(cboAccno.SelectedValue, 1, 3)
        BAccno = Trim(Mid(cboAccno.SelectedValue, 5, 17))
        If Len(BAccno) >= 7 Then
            Dim tempdataset As DataSet
            Dim sqlstr As String
            sqlstr = "SELECT sum(bg1+bg2+bg3+bg4+up1+up2+up3+up4-totper-totuse) as balance FROM bgf010 " & _
                     " WHERE accyear=" & BYear & " and left(accno,9)='" & Mid(BAccno, 1, 9) & "'"
            tempdataset = openmember("", "temp", sqlstr)
            If tempdataset.Tables("temp").Rows.Count = 0 Then
                lblGrade6.Text = "0"
            Else
                lblGrade6.Text = FormatNumber(tempdataset.Tables("temp").Rows(0).Item(0), 0)
            End If
        Else
            lblGrade6.Text = "請選擇大於六級科目"
        End If
    End Sub

    Private Sub btnPay000_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPay000.Click
        'sqlstr = "select a.*, b.accname from pay000 a left outer join accname b on a.accno=b.accno where a.bgno is null order by a.accno"
        'myDataSetS =  openmember("", "pay000", sqlstr)
        'dtgPay000.DataSource = myDatasetS
        'dtgPay000.DataMember = "pay000"
        'bmS = Me.BindingContext(myDataSetS, "pay000")
        TabControl1.SelectedIndex = 3    'page4
        ComeFromPay000 = True
    End Sub

    Private Sub txtbatno_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBatNo.KeyUp
        If Len(txtBatNo.Text) = 5 Then 'e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then   'press the enter key
            btnPayno.Focus()
        End If
    End Sub

    Private Sub btnPayno_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPayno.Click
        txtBatNo.Text = txtBatNo.Text.ToUpper
        Call find_pay000()
    End Sub

    '由編號找資料(有差勤系統)
    Function find_pay000()
        'sqlstr = "SELECT * FROM pay000 WHERE payno='" & txtPayno.Text & "'"  由payno找資料
        sqlstr = "SELECT * FROM pay000 WHERE accyear=" & txtByear.Text & " and batno='" & txtBatNo.Text & "'"
        TempDataSet = openmember("", "TEMP", sqlstr)

        If TempDataSet.Tables(0).Rows.Count > 0 Then
            If nz(TempDataSet.Tables(0).Rows(0).Item("bgno"), "") <> "" Then
                MsgBox("本單據已推算" & TempDataSet.Tables(0).Rows(0).Item("bgno"))
                txtBatNo.Focus()
                Exit Function
            End If
            With TempDataSet.Tables(0).Rows(0)
                strAccno = Format(Val(Mid(nz(.Item("payno"), ""), 1, 3)), "000") & " " & Mid(nz(.Item("accno"), "") & Space(17), 1, 17)
                'strAccno = Mid(nz(.Item("payno"), ""), 1, 3) & " " & Mid(nz(.Item("accno"), "") & Space(17), 1, 17)
                cboAccno.SelectedValue = strAccno    '差勤會計科目
                '1050907將批號加入至請購事由
                txtRemarka.Text = nz(.Item("batno"), "") & nz(.Item("remark"), "") '& "  " & nz(.Item("name1"), "")
                txtAmta.Text = nz(.Item("amt"), 0)
                txtSubjecta.Text = nz(.Item("name1"), "")
                TabControl1.SelectedIndex = 2
            End With
        Else
            MsgBox("無此單據")
            txtBatNo.Focus()
            Exit Function
        End If
    End Function

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        TabControl1.SelectedIndex = 0
        ComeFromPay000 = False
    End Sub

    Function Pay000REmove()   '差勤datagrid刪除一筆
        For i = 0 To bmS.Count - 1
            bmS.Position = i
            If nz(bmS.Current("batno"), "") = txtBatNo.Text And nz(bmS.Current("accyear"), 0) = Val(txtByear.Text) Then
                myDataSetS.Tables("pay000").Rows.RemoveAt(bmS.Position.ToString)  '將source grid刪行
                txtBatNo.Text = ""
                txtBatNo.Focus()
                Exit Function
            End If
        Next
    End Function


    Private Sub dtgPay000_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtgPay000.Click
        txtByear.Text = nz(bmS.Current("accyear"), 0)
        txtBatNo.Text = nz(bmS.Current("batno"), "")
        btnPayno.Focus()
    End Sub

    Private Sub dtgPay000_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtgPay000.DoubleClick
        Call dtgPay000_Click(New System.Object, New System.EventArgs)
        Call btnPayno_Click(New System.Object, New System.EventArgs)
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

    Private Sub txtAmta_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAmta.KeyPress
        If Asc(e.KeyChar) = 13 Then   'enter key 
            'SendKeys.Send("{TAB}")
            btnAdd.Focus()
        End If

    End Sub

    Private Sub btnUserid_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUserid.Click
        Dim intLens As Integer = 0
        Dim strUserid As String
        If Len(Trim(txtUserid.Text)) > 0 Then
            sqlstr = "select * from usertable where userid='" & Format(Val(txtUserid.Text), "0000") & "'"
            strUserid = Format(Val(txtUserid.Text), "0000")
            TempDataSet = openmember("", "temp", sqlstr)
            If TempDataSet.Tables("temp").Rows.Count > 0 Then
                intLens = txtRemarka.Text.IndexOf("  ")
                If intLens > 0 Then
                    txtRemarka.Text = Mid(txtRemarka.Text, 1, intLens) & "  " & TempDataSet.Tables("temp").Rows(0).Item("username")
                Else
                    txtRemarka.Text = Trim(txtRemarka.Text) & "  " & TempDataSet.Tables("temp").Rows(0).Item("username")
                End If
                txtAmta.Focus()
            Else
                MsgBox("無此編號")
            End If
        End If
    End Sub

    Private Sub btnCbgf020_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCbgf020.Click
        TabControl1.SelectedIndex = 4    'page5
        ComeFromCBGf020 = True
    End Sub

    Private Sub btnCancel5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel5.Click
        TabControl1.SelectedIndex = 0
        ComeFromCBGf020 = False
    End Sub

    Private Sub btnCbgno_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCbgno.Click
        Call find_cbgf020()
    End Sub

    '由編號找資料(管理處預算系統)
    Function find_cbgf020()
        sqlstr = "SELECT a.*, b.bookaccno FROM cbgf020 a left outer join caccname b " & _
                 " on a.accno=b.accno WHERE a.bgno='" & txtCbgno.Text & "'"
        TempDataSet = openmember("", "TEMP", sqlstr)

        If TempDataSet.Tables(0).Rows.Count > 0 Then
            If nz(TempDataSet.Tables(0).Rows(0).Item("usebgno"), "") <> "" Then
                MsgBox("本編號已推算" & TempDataSet.Tables(0).Rows(0).Item("usebgno"))
                txtCbgno.Focus()
                Exit Function
            End If
            With TempDataSet.Tables(0).Rows(0)
                'right('0'+ltrim(str(a.accyear)),3) + ' ' +left(a.accno+space(17),17) as bgf010key,"
                strAccno = Mid(txtCbgno.Text, 1, 3) & " " & nz(.Item("bookaccno"), "")  '符合請購科目combo
                cboAccno.SelectedValue = strAccno    '會計科目
                txtRemarka.Text = nz(.Item("remark"), "")
                txtAmta.Text = nz(.Item("amt1"), 0)
                txtSubjecta.Text = nz(.Item("subject"), "")
                TabControl1.SelectedIndex = 2
            End With
        Else
            MsgBox("無此編號")
            txtCbgno.Focus()
            Exit Function
        End If
    End Function

    Private Sub dtgcbgf020_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtgCbgf020.Click
        txtCbgno.Text = nz(bmS2.Current("bgno"), "")    'put cbgf020.bgno to txtcbgno.text 
        btnCbgno.Focus()
    End Sub

    Private Sub dtgcbgf020_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtgCbgf020.DoubleClick
        Call dtgcbgf020_Click(New System.Object, New System.EventArgs)
        Call btnCbgno_Click(New System.Object, New System.EventArgs)
    End Sub

    Function Cbgf020REmove()   '次預算datagrid刪除一筆
        For i = 0 To bmS2.Count - 1
            bmS2.Position = i
            If bmS2.Current("bgno") = txtCbgno.Text Then
                myDataSetS2.Tables("cbgf020").Rows.RemoveAt(bmS2.Position.ToString)  '將source grid刪行
                txtCbgno.Text = ""
                txtCbgno.Focus()
                Exit Function
            End If
        Next
    End Function
End Class
