Public Class BAIL010
    Dim LoadAfter, Dirty As Boolean

    Dim sqlstr As String
    Dim mydataset As DataSet
    Dim isnullDateS() = {True, True, True, True, True} '預設為true表示日期為null值(保固(證)金期限)
    Dim isnullDateE() = {True, True, True, True, True}
    Dim DateS(4) As Date
    Dim DateE(4) As Date

    Private Sub frmBail010_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim myds As DataSet
        LoadAfter = True
        TabControl1.Enabled = False
        dtpDate.Value = TransPara.TransP("UserDATE")
        Dim i As Integer
        For i = 1 To 4
            isnullDateS(i) = True
            isnullDateE(i) = True
        Next
    End Sub

    Private Sub btnInq_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInq.Click
        Dim intI As Integer
        Dim isdatenullS As Boolean '判斷保固期限起enf010是否有資料
        Dim isdatenullE As Boolean '判斷保固期限訖enf010是否有資料


        'Dim mydataset1 As DataSet
        Dim intKind1r, intKind2r, intKind3r, intKind4r, intKind1p, intKind2p, intKind3p, intKind4p As Integer
        'If txtInqEngno.Text.Trim.Length <> 7 Or Not IsNumeric(txtInqEngno.Text) Then
        '    MsgBox("工程編號輸入錯誤，請重新輸入!")
        '    Exit Sub
        'End If
        isdatenullS = False
        isdatenullE = False
        btnInq.Enabled = False
        txtInqEngno.Enabled = False
        StatusBar1.Text = ""
        txtEngIdno.Text = ""
        txtEngCop.Text = ""
        lblEngname.Text = ""
        StatusBar1.Text = ""
        lblEngKind3crS.Visible = False
        dtpEngKind3crS.Visible = False
        lblEngKind3crE.Visible = False
        dtpEngKind3crE.Visible = False
        TabControl1.Enabled = True
        'lblKind3crS.Visible = False
        'dtpkind3crS.Visible = False
        'lblKind3crE.Visible = False
        'dtpKind3crE.Visible = False
        txtKindcr.Text = ""
        txtEngno.Text = txtInqEngno.Text
        sqlstr = "SELECT engname,idno,cop,test2_date,keep_date FROM enf010 where engno='" & Trim(txtInqEngno.Text) & "'"
        mydataset = openmember("", "enf010", sqlstr)
        If (mydataset.Tables("enf010").Rows.Count > 0) Then
            lblEngname.Text = nz(mydataset.Tables("enf010").Rows(0).Item("engname"), "")
            txtEngIdno.Text = nz(mydataset.Tables("enf010").Rows(0).Item("idno"), "")
            txtIdno.Text = nz(mydataset.Tables("enf010").Rows(0).Item("idno"), "")
            txtEngCop.Text = nz(mydataset.Tables("enf010").Rows(0).Item("cop"), "")
            txtCop.Text = nz(mydataset.Tables("enf010").Rows(0).Item("cop"), "")
            If Not IsDBNull(mydataset.Tables("enf010").Rows(0).Item("test2_date")) Then
                dtpEngKind3crS.Value = mydataset.Tables("enf010").Rows(0).Item("test2_date")
                lblEngKind3crS.Visible = True
                dtpEngKind3crS.Visible = True

                '保固期限起
                'dtpkind3crS.Value = mydataset.Tables("enf010").Rows(0).Item("test2_date")
                isdatenullS = True
                DateS(3) = mydataset.Tables("enf010").Rows(0).Item("test2_date")
                isnullDateS(3) = False

                '    dtpEngKind3crS.Value = Now
                '    dtpEngKind3crS.Visible = True
            End If
            If Not IsDBNull(mydataset.Tables("enf010").Rows(0).Item("keep_date")) Then
                dtpEngKind3crE.Value = mydataset.Tables("enf010").Rows(0).Item("keep_date")
                lblEngKind3crE.Visible = True
                dtpEngKind3crE.Visible = True
                ' Else
                '     dtpEngKind3crE.Value = Now
                '    dtpEngKind3crE.Visible = True

                '保固期限起

                'dtpKind3crE.Value = mydataset.Tables("enf010").Rows(0).Item("keep_date")
                isdatenullE = True

                DateE(3) = mydataset.Tables("enf010").Rows(0).Item("keep_date")
                isnullDateE(3) = False


            End If
        Else
            Call clearForm()
            MsgBox("工程編號不存在!請重新輸入!")
            Exit Sub
        End If
        mydataset.Clear()


        sqlstr = "SELECT * FROM bailf010 where engno='" & Trim(txtInqEngno.Text) & "' and (balance is null or balance='')"
        mydataset = openmember("", "bailf010", sqlstr)
        intKind1r = 0
        intKind2r = 0
        intKind3r = 0
        intKind4r = 0
        intKind1p = 0
        intKind2p = 0
        intKind3p = 0
        intKind4p = 0
        For intI = 0 To (mydataset.Tables("bailf010").Rows.Count - 1)
            Select Case mydataset.Tables("bailf010").Rows(intI).Item("kind")
                Case Is = "1"  '履約金
                    Select Case mydataset.Tables("bailf010").Rows(intI).Item("rp")
                        Case Is = "1" '收
                            intKind1r = intKind1r + mydataset.Tables("bailf010").Rows(intI).Item("amt")
                            If Not IsDBNull(mydataset.Tables("bailf010").Rows(intI).Item("date_s")) Then
                                DateS(1) = mydataset.Tables("bailf010").Rows(intI).Item("date_s")
                                isnullDateS(1) = False
                            Else
                                isnullDateS(1) = True
                            End If

                            If Not IsDBNull(mydataset.Tables("bailf010").Rows(intI).Item("date_e")) Then
                                DateE(1) = mydataset.Tables("bailf010").Rows(intI).Item("date_e")
                                isnullDateE(1) = False
                            Else
                                isnullDateE(1) = True
                            End If
                        Case Is = "2" '支
                            intKind1p = intKind1p + mydataset.Tables("bailf010").Rows(intI).Item("amt")
                    End Select
                Case Is = "2" '押標金
                    Select Case mydataset.Tables("bailf010").Rows(intI).Item("rp")
                        Case Is = "1"
                            intKind2r = intKind2r + mydataset.Tables("bailf010").Rows(intI).Item("amt")
                            If Not IsDBNull(mydataset.Tables("bailf010").Rows(intI).Item("date_s")) Then
                                'dateKind3S = mydataset.Tables("bailf010").Rows(intI).Item("date_s")
                                DateS(2) = mydataset.Tables("bailf010").Rows(intI).Item("date_s")
                                isnullDateS(2) = False
                            Else
                                isnullDateS(2) = True
                            End If

                            If Not IsDBNull(mydataset.Tables("bailf010").Rows(intI).Item("date_e")) Then
                                'dateKind3E = mydataset.Tables("bailf010").Rows(intI).Item("date_e")
                                DateE(2) = mydataset.Tables("bailf010").Rows(intI).Item("date_e")
                                isnullDateE(2) = False
                            Else
                                isnullDateE(2) = True
                            End If
                        Case Is = "2"
                            intKind2p = intKind2p + mydataset.Tables("bailf010").Rows(intI).Item("amt")
                    End Select
                Case Is = "3" '保固金
                    Select Case mydataset.Tables("bailf010").Rows(intI).Item("rp")
                        Case Is = "1"
                            intKind3r = intKind3r + mydataset.Tables("bailf010").Rows(intI).Item("amt")
                            If Not IsDBNull(mydataset.Tables("bailf010").Rows(intI).Item("date_s")) And Not isdatenullS Then
                                'dateKind3S = mydataset.Tables("bailf010").Rows(intI).Item("date_s")
                                DateS(3) = mydataset.Tables("bailf010").Rows(intI).Item("date_s")
                                isnullDateS(3) = False
                                'Else
                                '    isnullDateS(3) = True
                            End If

                            If Not IsDBNull(mydataset.Tables("bailf010").Rows(intI).Item("date_e")) And Not isdatenullE Then
                                'dateKind3E = mydataset.Tables("bailf010").Rows(intI).Item("date_e")
                                DateE(3) = mydataset.Tables("bailf010").Rows(intI).Item("date_e")
                                isnullDateE(3) = False
                                'Else
                                '    isnullDateE(3) = True
                            End If
                        Case Is = "2"
                            intKind3p = intKind3p + mydataset.Tables("bailf010").Rows(intI).Item("amt")
                    End Select
                Case Is = "4" '差額保證金
                    Select Case mydataset.Tables("bailf010").Rows(intI).Item("rp")
                        Case Is = "1"
                            intKind4r = intKind4r + mydataset.Tables("bailf010").Rows(intI).Item("amt")
                            If Not IsDBNull(mydataset.Tables("bailf010").Rows(intI).Item("date_s")) Then
                                'dateKind3S = mydataset.Tables("bailf010").Rows(intI).Item("date_s")
                                DateS(4) = mydataset.Tables("bailf010").Rows(intI).Item("date_s")
                                isnullDateS(4) = False
                            Else
                                isnullDateS(4) = True
                            End If

                            If Not IsDBNull(mydataset.Tables("bailf010").Rows(intI).Item("date_e")) Then
                                'dateKind3E = mydataset.Tables("bailf010").Rows(intI).Item("date_e")
                                DateE(4) = mydataset.Tables("bailf010").Rows(intI).Item("date_e")
                                isnullDateE(4) = False
                            Else
                                isnullDateE(4) = True
                            End If
                        Case Is = "2"
                            intKind4p = intKind4p + mydataset.Tables("bailf010").Rows(intI).Item("amt")
                    End Select
            End Select
        Next
        txtKind1r.Text = Format(intKind1r, "##,##0")
        txtKind2r.Text = Format(intKind2r, "##,##0")
        txtKind3r.Text = Format(intKind3r, "##,##0")
        txtKind4r.Text = Format(intKind4r, "##,##0")
        txtKind1p.Text = Format(intKind1p, "##,##0")
        txtKind2p.Text = Format(intKind2p, "##,##0")
        txtKind3p.Text = Format(intKind3p, "##,##0")
        txtKind4p.Text = Format(intKind4p, "##,##0")
        If (mydataset.Tables("bailf010").Rows.Count > 0) Then
            '如保證金檔(bailf010)有商號統編資料則填覆蓋原工程檔顯示的商號統編
            If Not IsDBNull(mydataset.Tables("bailf010").Rows(0).Item("idno")) And nz((mydataset.Tables("bailf010").Rows(0).Item("idno")), "") <> "" And txtIdno.Text.Trim = "" Then
                txtIdno.Text = mydataset.Tables("bailf010").Rows(0).Item("idno")
            End If
            '如保證金檔(bailf010)有商號名稱資料則填覆蓋原工程檔顯示的商號名稱
            If Not IsDBNull(mydataset.Tables("bailf010").Rows(0).Item("cop")) And nz(mydataset.Tables("bailf010").Rows(0).Item("cop"), "") <> "" And txtCop.Text.Trim = "" Then
                'txtIdno.Text = mydataset.Tables("bailf010").Rows(0).Item("idno")
                txtCop.Text = mydataset.Tables("bailf010").Rows(0).Item("cop")
            End If
        End If

        'If rdbKind3.Checked Then
        '    lblKind3crS.Visible = True
        '    dtpkind3crS.Visible = True
        '    lblKind3crE.Visible = True
        '    dtpKind3crE.Visible = True
        'Else
        '    lblKind3crS.Visible = False
        '    dtpkind3crS.Visible = False
        '    lblKind3crE.Visible = False
        '    dtpKind3crE.Visible = False
        'End If
        enableobj(True)
        txtKindcr.Focus()   '輸入焦點停駐在現收部分
        Call setDateInputStyle()


    End Sub

    Private Sub txtIdno_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtIdno.LostFocus
        ' sqlstr = "SELECT * FROM  factory WHERE idno='" & Trim(txtIdno.Text) & "'"
        ' mydataset =  openmember("", "factory", sqlstr)
        ' If (mydataset.Tables("factory").Rows.Count > 0) Then
        ' txtCop.Text = nz(mydataset.Tables("factory").Rows(0).Item("shortname"), "")
        ' End If
        ' mydataset.Clear()
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Dim retstr, sqlstr As String
        Dim jkind As String
        If rdbKind1.Checked Then
            jkind = "1"
        ElseIf rdbKind2.Checked Then
            jkind = "2"
        ElseIf rdbKind3.Checked Then
            jkind = "3"
        ElseIf rdbKind4.Checked Then
            jkind = "4"
        End If
        '94/07/13 因同一天有押標轉履約及補足履約金部分,故有兩筆，拿掉資料重複判斷。
        'sqlstr = "SELECT * FROM bailf010 where engno='" & Trim(txtInqEngno.Text) & "' and kind='" & jkind & "' and rp='1' and rpdate='" & dtpDate.Value.ToString("yyyy/M/d", CultureInfo.InvariantCulture) & "'"
        'mydataset =  openmember("", "bailf010", sqlstr)
        'If (mydataset.Tables("bailf010").Rows.Count > 0) Then
        ' StatusBar1.Text = "資料重複!"
        ' Call restart()
        ' Exit Sub
        'End If
        If txtCop.Text.Trim.Length > 25 Then
            StatusBar1.Text = "商號名稱最大長度為25個字元!"
            Exit Sub
        End If
        If txtIdno.Text.Length > 10 Then
            StatusBar1.Text = "商號統編最大長度為10個字元!"
            Exit Sub
        End If
        If IsNumeric(txtKindcr.Text) Then
            If Val(txtKindcr.Text) > 0 Then
                GenInsSql("engno", txtEngno.Text, "T")
                'If rdbKind1.Checked Then
                '    GenInsSql("kind", "1", "T")
                'ElseIf rdbKind2.Checked Then
                '    GenInsSql("kind", "2", "T")
                'ElseIf rdbKind3.Checked Then
                '    GenInsSql("kind", "3", "T")
                '    GenInsSql("date_s", dtpkind3crS.Text, "D")
                '    GenInsSql("date_e", dtpKind3crE.Text, "D")
                'ElseIf rdbKind4.Checked Then
                '    GenInsSql("kind", "4", "T")
                'End If
                If rdbKind1.Checked Then              '保管種類
                    GenInsSql("kind", "1", "T")
                    If chk_dtpDateS.Checked = True Then
                        GenInsSql("date_s", dtpkind3crS.Text, "D")
                    End If
                    If chk_dtpDateE.Checked = True Then
                        GenInsSql("date_e", dtpKind3crE.Text, "D")
                    End If
                ElseIf rdbKind2.Checked Then
                    GenInsSql("kind", "2", "T")
                    If chk_dtpDateS.Checked = True Then
                        GenInsSql("date_s", dtpkind3crS.Text, "D")
                    End If
                    If chk_dtpDateE.Checked = True Then
                        GenInsSql("date_e", dtpKind3crE.Text, "D")
                    End If
                ElseIf rdbKind3.Checked Then          '保固品  
                    GenInsSql("kind", "3", "T")
                    GenInsSql("date_s", dtpkind3crS.Text, "D")
                    GenInsSql("date_e", dtpKind3crE.Text, "D")
                ElseIf rdbKind4.Checked Then
                    GenInsSql("kind", "4", "T")
                    If chk_dtpDateS.Checked = True Then
                        GenInsSql("date_s", dtpkind3crS.Text, "D")
                    End If
                    If chk_dtpDateE.Checked = True Then
                        GenInsSql("date_e", dtpKind3crE.Text, "D")
                    End If
                End If
                GenInsSql("rpdate", dtpDate.Text, "D")
                GenInsSql("rp", "1", "T")
                GenInsSql("idno", txtIdno.Text, "T")
                GenInsSql("cop", txtCop.Text.Trim, "T")
                GenInsSql("amt", txtKindcr.Text, "T")
                sqlstr = "insert into bailf010 " & GenInsFunc
                retstr = runsql(mastconn, sqlstr)

                If retstr = "sqlok" Then
                    backuprtn("", Application.StartupPath, sqlstr)
                    StatusBar1.Text = "資料新增成功!"
                Else
                    MsgBox("資料新增失敗，請檢查輸入值或洽詢程式設計人員!")
                End If
                enableobj(False)
                btnInq.Enabled = True
                txtInqEngno.Enabled = True
                txtInqEngno.Focus()
            Else
                StatusBar1.Text = "請輸入現收金額,必須大於0!"
            End If
        Else
            StatusBar1.Text = "請輸入現收金額,必須為數字!"
        End If

    End Sub

    Private Sub frmbail010_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            SendKeys.Send("{tab}") ' 擷取表單的按鍵，按下ENTER立刻跳至下一個輸入的位置
        End If

    End Sub

    'Private Sub frmBail010_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
    '    If e.KeyCode = 13 Then
    '        SendKeys.Send("{tab}") ' 擷取表單的按鍵，按下ENTER立刻跳至下一個輸入的位置
    '    End If
    '
    '   End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Call clearForm()
    End Sub
    Sub clearForm()
        Dim ThisControl As System.Windows.Forms.Control
        For Each ThisControl In Me.TabPage1.Controls
            If ThisControl.Name.Substring(0, 3) = "txt" Then
                ThisControl.Text = ""
            ElseIf ThisControl.Name.Substring(0, 3) = "dtp" Then
                ThisControl.Text = Now
            End If
        Next ThisControl
        dtpDate.Value = TransPara.TransP("UserDATE")
        lblEngname.Text = ""
        'lblKind3crS.Visible = False
        'dtpkind3crS.Visible = False
        'lblKind3crE.Visible = False
        'dtpKind3crE.Visible = False
        lblEngKind3crS.Visible = False
        dtpEngKind3crS.Visible = False
        lblEngKind3crE.Visible = False
        dtpEngKind3crE.Visible = False
        StatusBar1.Text = ""
        enableobj(False)
        btnInq.Enabled = True
        txtInqEngno.Enabled = True
        txtInqEngno.Focus()
    End Sub

    Private Sub enableobj(ByVal en As Boolean)
        Dim ThisControl As System.Windows.Forms.Control
        For Each ThisControl In Me.TabPage1.Controls
            ThisControl.Enabled = en
        Next ThisControl
    End Sub


    Private Sub rdbKind3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbKind3.CheckedChanged
        'If rdbKind3.Checked Then
        '    lblKind3crS.Visible = True
        '    dtpkind3crS.Visible = True
        '    lblKind3crE.Visible = True
        '    dtpKind3crE.Visible = True
        'Else
        '    lblKind3crS.Visible = False
        '    dtpkind3crS.Visible = False
        '    lblKind3crE.Visible = False
        '    dtpKind3crE.Visible = False
        'End If
        Call setDateInputStyle()
    End Sub

    Private Sub restart()
        enableobj(False)
        btnInq.Enabled = True
        txtInqEngno.Enabled = True
        txtInqEngno.Focus()
    End Sub

    Private Sub btnEnd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnd.Click
        Me.Close()
    End Sub

    Private Sub txtKindcr_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtKindcr.TextChanged
        lblAmt.Text = Format(Val(txtKindcr.Text), "##,##0")
    End Sub

    Private Sub chk_dtpDateS_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk_dtpDateS.CheckedChanged
        If chk_dtpDateS.Checked = True Then
            dtpkind3crS.Enabled = True
        Else
            dtpkind3crS.Enabled = False
        End If

    End Sub

    Private Sub chk_dtpDateE_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk_dtpDateE.CheckedChanged
        If chk_dtpDateE.Checked = True Then
            dtpKind3crE.Enabled = True
        Else
            dtpKind3crE.Enabled = False
        End If
    End Sub

    'Sub setDateInputStyle()    '當為履約品時設定為保證品日期輸入樣式,否則輸入保固期限樣式
    'If rdbKind3.Checked Then           '保固品日期顯示(保固期限)
    '    lblKind3crS.Text = "保固期限："
    '    chk_dtpDateS.Visible = False
    '    chk_dtpDateS.Checked = True
    '    dtpkind3crS.Enabled = True
    '    chk_dtpDateE.Visible = False
    '    chk_dtpDateE.Checked = True
    '    dtpKind3crE.Enabled = True
    'Else                                '履約品日期顯示(保管品期限)
    '    lblKind3crS.Text = "保證金期限"
    '    chk_dtpDateS.Visible = True
    '    chk_dtpDateS.Checked = False
    '    dtpkind3crS.Enabled = False
    '    chk_dtpDateE.Visible = True
    '    chk_dtpDateE.Checked = False
    '    dtpKind3crE.Enabled = False
    'End If
    'End Sub

    Sub setDateInputStyle()    '當為不為保固金時顯示保證金期限,否則顯示保固期限
        Dim i As Integer
        lblKind3crS.Text = "保證金期限"
        chk_dtpDateS.Visible = True
        chk_dtpDateE.Visible = True
        If rdbKind1.Checked Then
            If Not isnullDateS(1) And Not isnullDateE(1) Then
                dtpkind3crS.Value = DateS(1)
                dtpKind3crE.Value = DateE(1)
                chk_dtpDateS.Checked = True
                chk_dtpDateE.Checked = True
                dtpkind3crS.Enabled = True
                dtpKind3crE.Enabled = True
            ElseIf Not isnullDateS(1) Then
                dtpkind3crS.Value = DateS(1)
                chk_dtpDateS.Checked = True
                chk_dtpDateE.Checked = False
                dtpkind3crS.Enabled = True
                dtpKind3crE.Enabled = False
            ElseIf Not isnullDateE(1) Then
                dtpKind3crE.Value = DateE(1)
                chk_dtpDateE.Checked = True
                chk_dtpDateS.Checked = False
                dtpkind3crS.Enabled = False
                dtpKind3crE.Enabled = True
            Else
                chk_dtpDateS.Checked = False
                chk_dtpDateE.Checked = False
                dtpkind3crS.Enabled = False
                dtpKind3crE.Enabled = False
            End If
        ElseIf rdbKind2.Checked Then
            If Not isnullDateS(2) And Not isnullDateE(2) Then
                dtpkind3crS.Value = DateS(2)
                dtpKind3crE.Value = DateE(2)
                chk_dtpDateS.Checked = True
                chk_dtpDateE.Checked = True
                dtpkind3crS.Enabled = True
                dtpKind3crE.Enabled = True
            ElseIf Not isnullDateS(2) Then
                dtpkind3crS.Value = DateS(2)
                chk_dtpDateS.Checked = True
                chk_dtpDateE.Checked = False
                dtpkind3crS.Enabled = True
                dtpKind3crE.Enabled = False
            ElseIf Not isnullDateE(2) Then
                dtpKind3crE.Value = DateE(2)
                chk_dtpDateE.Checked = True
                chk_dtpDateS.Checked = False
                dtpkind3crS.Enabled = False
                dtpKind3crE.Enabled = True
            Else
                chk_dtpDateS.Checked = False
                chk_dtpDateE.Checked = False
                dtpkind3crS.Enabled = False
                dtpKind3crE.Enabled = False
            End If
        ElseIf rdbKind3.Checked Then
            chk_dtpDateS.Visible = False
            chk_dtpDateE.Visible = False
            chk_dtpDateS.Checked = True
            chk_dtpDateE.Checked = True
            dtpkind3crS.Enabled = True
            dtpKind3crE.Enabled = True

            lblKind3crS.Text = "保固期限："
            If Not isnullDateS(3) And Not isnullDateE(3) Then
                dtpkind3crS.Value = DateS(3)
                dtpKind3crE.Value = DateE(3)
                'chk_dtpDateS.Checked = True
                'chk_dtpDateE.Checked = True
                'dtpkind3crS.Enabled = True
                'dtpKind3crE.Enabled = True
            ElseIf Not isnullDateS(3) Then
                dtpkind3crS.Value = DateS(3)
                'chk_dtpDateS.Checked = True
                'chk_dtpDateE.Checked = False
                'dtpkind3crS.Enabled = True
                'dtpKind3crE.Enabled = False
            ElseIf Not isnullDateE(3) Then
                dtpKind3crE.Value = DateE(3)
                'chk_dtpDateE.Checked = True
                'chk_dtpDateS.Checked = False
                'dtpkind3crS.Enabled = False
                'dtpKind3crE.Enabled = True
            Else
                'chk_dtpDateS.Checked = True    '96/3/21改true
                'chk_dtpDateE.Checked = True
                'dtpkind3crS.Enabled = True
                'dtpKind3crE.Enabled = True
            End If
        ElseIf rdbKind4.Checked Then
            If Not isnullDateS(4) And Not isnullDateE(4) Then
                dtpkind3crS.Value = DateS(4)
                dtpKind3crE.Value = DateE(4)
                chk_dtpDateS.Checked = True
                chk_dtpDateE.Checked = True
                dtpkind3crS.Enabled = True
                dtpKind3crE.Enabled = True
            ElseIf Not isnullDateS(4) Then
                dtpkind3crS.Value = DateS(4)
                chk_dtpDateS.Checked = True
                chk_dtpDateE.Checked = False
                dtpkind3crS.Enabled = True
                dtpKind3crE.Enabled = False
            ElseIf Not isnullDateE(4) Then
                dtpKind3crE.Value = DateE(4)
                chk_dtpDateE.Checked = True
                chk_dtpDateS.Checked = False
                dtpkind3crS.Enabled = False
                dtpKind3crE.Enabled = True
            Else
                chk_dtpDateS.Checked = False
                chk_dtpDateE.Checked = False
                dtpkind3crS.Enabled = False
                dtpKind3crE.Enabled = False
            End If
        End If
    End Sub

    Private Sub rdbKind1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbKind1.CheckedChanged
        Call setDateInputStyle()
    End Sub

    Private Sub dtpkind3crS_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpkind3crS.ValueChanged
        If rdbKind3.Checked Then
            dtpKind3crE.Value = dtpkind3crS.Value.AddYears(3)  '保固預設加三年
        End If
    End Sub
End Class
