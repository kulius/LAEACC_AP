Imports System.Globalization
Public Class BAIL020
    Dim LoadAfter, Dirty As Boolean
    Dim sqlstr As String
    Dim bm As BindingManagerBase, mydataset As DataSet
    Dim UserId, UserName, UserUnit As String
    Dim intKind1r, intKind2r, intKind3r, intKind4r, intKind1p, intKind2p, intKind3p, intKind4p As Integer
    Dim isnullDateS() = {True, True, True, True, True} '預設為true表示日期為null值(保固(證)金期限)
    Dim isnullDateE() = {True, True, True, True, True}
    Dim DateS(4) As Date
    Dim DateE(4) As Date

    Private Sub frmBail020_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim myds As DataSet
        LoadAfter = True
        UserName = TransPara.TransP("username")
        UserUnit = TransPara.TransP("userunit")
        UserId = TransPara.TransP("userid")
        TabPage1.Enabled = False
        lblKind3crS.Visible = False
        dtpkind3crS.Visible = False
        lblKind3crE.Visible = False
        dtpKind3crE.Visible = False
        RecMove1.RecIns.Visible = False
        RecMove1.RecUpdCMD.Visible = False
        RecMove1.RecDel.Visible = False
        dtpDate.Value = TransPara.TransP("UserDATE")
        Dim i As Integer
        For i = 1 To 4
            isnullDateS(i) = True
            isnullDateE(i) = True
        Next

        'Call LoadGrid()
        'Call LoadGrid()
    End Sub

    Private Sub btnInq_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInq.Click
        Dim i As Integer
        Dim strNowIdno As String
        Call bailcount()
        RecMove1.Visible = True
        If TabPage1.Enabled = True Then
            strNowIdno = txtInqEngno.Text.Trim '保留原工程編號
            Call LoadGrid()
            txtInqEngno.Text = strNowIdno      '還原工程編號,因LoadGrid會觸發記錄移動事件,txtInqEngno.txt會變成第一筆記錄
        Else
            RecMove1.Visible = False
        End If
        If Not IsNothing(mydataset) Then
            If mydataset.Tables("bailf010").Rows.Count > 0 Then
                For i = 0 To mydataset.Tables("bailf010").Rows.Count - 1
                    If mydataset.Tables("bailf010").Rows(i).Item("engno") = txtInqEngno.Text.Trim Then
                        bm.Position = i
                        Exit For
                    End If
                Next
                Call RecMove1.IOPOS()
                dtgBail.CurrentRowIndex = bm.Position
            End If
        End If
    End Sub
    Private Sub KindCheckboxSetup()
        'rdbKind1.Enabled = True
        'rdbKind3.Enabled = True

        If intKind1r - intKind1p > 0 Then
            rdbKind1.Enabled = True
        Else
            rdbKind1.Enabled = False
        End If

        If intKind2r - intKind2p > 0 Then
            rdbKind2.Enabled = True
        Else
            rdbKind2.Enabled = False
        End If

        If intKind3r - intKind3p > 0 Then
            rdbKind3.Enabled = True
        Else
            rdbKind3.Enabled = False
        End If

        If intKind4r - intKind4p > 0 Then
            rdbKind4.Enabled = True
        Else
            rdbKind4.Enabled = False
        End If

        If rdbKind1.Enabled = False And rdbKind2.Enabled = False And rdbKind3.Enabled = False And rdbKind4.Enabled = False Then
            MsgBox("無可退回的保證金!")
            'TabPage1.Enabled = False
            StatusBar1.Text = "無可退回的保證金!"
            'enableobj(False)
            TabPage1.Enabled = False
            btnInq.Enabled = True
            txtInqEngno.Enabled = True
            txtInqEngno.Focus()
        End If
    End Sub
    Private Sub txtIdno_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtIdno.LostFocus
        ' sqlstr = "SELECT * FROM  factory WHERE idno='" & Trim(txtIdno.Text) & "'"
        ' mydataset =  openmember("", "factory", sqlstr)
        ' If (mydataset.Tables("factory").Rows.Count > 0) Then
        ' txtCop.Text = nz(mydataset.Tables("factory").Rows(0).Item("shortname"), "")
        ' 'txtTel.Text = nz(mydataset.Tables("factory").Rows(0).Item("tel1"), "")
        ' End If
        'mydataset.Clear()
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Dim retstr, sqlstr As String
        Dim jkind As String
        Dim mydataset1 As DataSet
        If rdbKind1.Checked Then
            jkind = "1"
        ElseIf rdbKind2.Checked Then
            jkind = "2"
        ElseIf rdbKind3.Checked Then
            jkind = "3"
        ElseIf rdbKind4.Checked Then
            jkind = "4"
        End If
        'mydataset.Tables.Clear()
        If txtCop.Text.Length > 25 Then
            StatusBar1.Text = "商號名稱最大長度為25個字元!"
            Exit Sub
        End If
        If txtIdno.Text.Length > 10 Then
            StatusBar1.Text = "商號統編最大長度為10個字元!"
            Exit Sub
        End If
        'sqlstr = "SELECT * FROM bailf010 where engno='" & Trim(txtInqEngno.Text) & "' and kind='" & jkind & "' and rp='2' and rpdate='" & dtpDate.Value.ToString("yyyy/M/d", CultureInfo.InvariantCulture) & "'"
        'mydataset1 =  openmember("", "bailf010", sqlstr)
        'If (mydataset1.Tables("bailf010").Rows.Count > 0) Then
        '    StatusBar1.Text = "資料重複!"
        '    Call restart()
        '    Exit Sub
        'End If
        If rdbKind1.Checked Then
            If intKind1p + Val(txtKindcp.Text) > intKind1r Then
                StatusBar1.Text = "退還金額超過收款金額，請重新輸入!"
                Exit Sub
            End If
        ElseIf rdbKind2.Checked Then
            If intKind2p + Val(txtKindcp.Text) > intKind2r Then
                StatusBar1.Text = "退還金額超過收款金額，請重新輸入!"
                Exit Sub
            End If
        ElseIf rdbKind3.Checked Then
            If intKind3p + Val(txtKindcp.Text) > intKind3r Then
                StatusBar1.Text = "退還金額超過收款金額，請重新輸入!"
                Exit Sub
            End If
        ElseIf rdbKind4.Checked Then
            If intKind4p + Val(txtKindcp.Text) > intKind4r Then
                StatusBar1.Text = "退還金額超過收款金額，請重新輸入!"
                Exit Sub
            End If
        Else
            MsgBox("請選擇保證種類!")
            Exit Sub
        End If
        If IsNumeric(txtKindcp.Text) Then
            If Val(txtKindcp.Text) > 0 Then
                GenInsSql("engno", txtEngno.Text, "T")
                If rdbKind1.Checked Then
                    GenInsSql("kind", "1", "T")
                    If dtpkind3crS.Visible = True Then
                        GenInsSql("date_s", dtpkind3crS.Text, "D")
                    End If
                    If dtpKind3crE.Visible = True Then
                        GenInsSql("date_e", dtpKind3crE.Text, "D")
                    End If
                ElseIf rdbKind2.Checked Then
                    GenInsSql("kind", "2", "T")
                    If dtpkind3crS.Visible = True Then
                        GenInsSql("date_s", dtpkind3crS.Text, "D")
                    End If
                    If dtpKind3crE.Visible = True Then
                        GenInsSql("date_e", dtpKind3crE.Text, "D")
                    End If
                ElseIf rdbKind3.Checked Then
                    GenInsSql("kind", "3", "T")
                    GenInsSql("date_s", dtpkind3crS.Text, "D")
                    GenInsSql("date_e", dtpKind3crE.Text, "D")
                ElseIf rdbKind4.Checked Then
                    GenInsSql("kind", "4", "T")
                    If dtpkind3crS.Visible = True Then
                        GenInsSql("date_s", dtpkind3crS.Text, "D")
                    End If
                    If dtpKind3crE.Visible = True Then
                        GenInsSql("date_e", dtpKind3crE.Text, "D")
                    End If
                End If
                GenInsSql("rpdate", dtpDate.Text, "D")
                GenInsSql("rp", "2", "T")
                GenInsSql("idno", txtIdno.Text, "T")
                GenInsSql("cop", txtCop.Text, "T")
                GenInsSql("amt", txtKindcp.Text, "T")
                sqlstr = "insert into bailf010 " & GenInsFunc
                retstr = runsql(mastconn, sqlstr)

                If retstr = "sqlok" Then
                    backuprtn("", Application.StartupPath, sqlstr)
                    StatusBar1.Text = "資料退還成功!"
                Else
                    MsgBox("資料新增失敗，請檢查輸入值或洽詢程式設計人員!")
                End If
                'enableobj(False)
                If checkBalance() Then  '當餘額為0時刪除該筆記錄
                    mydataset.Tables("bailf010").Rows(bm.Position).Delete()
                    mydataset.Tables("bailf010").AcceptChanges()
                    'dtgBail.DataSource = mydataset
                    'dtgBail.DataMember = "bailf010"
                End If
                TabPage1.Enabled = False
                btnInq.Enabled = True
                txtInqEngno.Enabled = True
                txtInqEngno.Focus()
            Else
                StatusBar1.Text = "請輸入現付金額,必須大於0!"
            End If
        Else
            StatusBar1.Text = "請輸入現收金額,必須為數字!"
        End If
    End Sub
    Private Sub frmBail020_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            SendKeys.Send("{tab}") ' 擷取表單的按鍵，按下ENTER立刻跳至下一個輸入的位置
        End If

    End Sub
    Private Sub enableobj(ByVal isenabled As Boolean)
        Dim ThisControl As System.Windows.Forms.Control
        For Each ThisControl In Me.TabPage1.Controls
            ThisControl.Enabled = isenabled
        Next ThisControl
    End Sub
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
        StatusBar1.Text = ""
        lblKind3crS.Visible = False
        dtpkind3crS.Visible = False
        lblKind3crE.Visible = False
        dtpKind3crE.Visible = False
        lblEngKind3crS.Visible = False
        dtpEngKind3crS.Visible = False
        lblEngKind3crE.Visible = False
        dtpEngKind3crE.Visible = False
        'enableobj(False)
        TabPage1.Enabled = False
        btnInq.Enabled = True
        txtInqEngno.Enabled = True
        txtInqEngno.Focus()
    End Sub

    Private Sub rdbKind3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbKind1.CheckedChanged, rdbKind2.CheckedChanged, rdbKind3.CheckedChanged, rdbKind4.CheckedChanged
        'If (rdbKind3.Checked And intKind3r - intKind3p > 0) Or blVisibleDate_e = True Or blVisibleDate_s = True Then '只有點選保固及有可退回款項才顯示保固期限
        '    If blVisibleDate_s = True Then
        '        lblKind3crS.Visible = True
        '        dtpkind3crS.Visible = True
        '    Else
        '        lblKind3crS.Visible = False
        '        dtpkind3crS.Visible = False
        '    End If
        '    If blVisibleDate_s = True Then
        '        lblKind3crE.Visible = True
        '        dtpKind3crE.Visible = True
        '    Else
        '        lblKind3crE.Visible = False
        '        dtpKind3crE.Visible = False
        '    End If
        'Else
        '    lblKind3crS.Visible = False
        '    dtpkind3crS.Visible = False
        '    lblKind3crE.Visible = False
        '    dtpKind3crE.Visible = False
        'End If
        setDateInputStyle()
    End Sub
    Private Sub restart()
        TabPage1.Enabled = False
        btnInq.Enabled = True
        txtInqEngno.Enabled = True
        txtInqEngno.Focus()
    End Sub

    Private Sub btnEnd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnd.Click
        Me.Close()
    End Sub

    Sub LoadGrid()
        Dim intI As Integer
        Dim mydatasetp As DataSet
        Dim mydatasetInq As DataSet
        Dim datarowp() As DataRow


        sqlstr = "SELECT bailf010.engno as engno,enf010.engname,enf010.cop as cop,sum(amt) as amt  FROM bailf010 inner join enf010 on bailf010.engno=enf010.engno where rp='1'   and (balance is null or balance='') group by bailf010.engno,enf010.engname,enf010.cop order by bailf010.engno" '依工程編號、商號找出保證金收的記錄
        mydataset = openmember("", "bailf010", sqlstr)
        sqlstr = "SELECT bailf010.engno as engno,enf010.engname,enf010.cop as cop,sum(amt) as amt  FROM bailf010 inner join enf010 on bailf010.engno=enf010.engno where rp='2'   and (balance is null or balance='') group by bailf010.engno,enf010.engname,enf010.cop order by bailf010.engno" '依工程編號、商號找出保證金收的記錄
        mydatasetp = openmember("", "bailf010", sqlstr)

        'Dim c As DataColumn = New DataColumn   '工程名稱
        'c.DataType = System.Type.GetType("System.String")
        'c.ColumnName = "engname"
        'c.DefaultValue = ""
        'mydataset.Tables("bailf010").Columns.Add(c)

        'Dim c1 As DataColumn = New DataColumn '到期日
        'c1.DataType = System.Type.GetType("System.String")
        'c1.ColumnName = "date_e"
        'c1.DefaultValue = ""
        'mydataset.Tables("bailf010").Columns.Add(c1)


        For intI = 0 To (mydataset.Tables("bailf010").Rows.Count - 1)
            datarowp = mydatasetp.Tables("bailf010").Select("engno='" & mydataset.Tables("BAILF010").Rows(intI).Item("engno") & "'")
            If datarowp.Length > 0 Then
                mydataset.Tables("bailf010").Rows(intI).Item("amt") = mydataset.Tables("bailf010").Rows(intI).Item("amt") - datarowp(0).Item("amt")
            Else
                mydataset.Tables("bailf010").Rows(intI).Item("amt") = mydataset.Tables("bailf010").Rows(intI).Item("amt")
            End If

            If mydataset.Tables("bailf010").Rows(intI).Item("amt") = 0 Then     '如收支相等(已退還)則不顯示
                mydataset.Tables("bailf010").Rows(intI).Delete()
            Else
                'sqlstr = "SELECT engname FROM enf010 where engno='" & mydataset.Tables("BAILF010").Rows(intI).Item("engno") & "'"
                'mydatasetInq =  openmember("", "enf010", sqlstr)
                '  If (mydatasetInq.Tables("enf010").Rows.Count > 0) Then
                '      mydataset.Tables("BAILF010").Rows(intI).Item("engname") = mydatasetInq.Tables("enf010").Rows(0).Item("engname")
                'End If
                'mydatasetInq.Clear()
                'sqlstr = "SELECT date_e,cop FROM bailf010 where engno='" & mydataset.Tables("BAILF010").Rows(intI).Item("engno") & "' and kind='3' and rp='1' and date_e is not null and (balance is null or balance='') order by date_e desc"
                'mydatasetInq =  openmember("", "bailf010", sqlstr)
                'If (mydatasetInq.Tables("BAILF010").Rows.Count > 0) Then
                'mydataset.Tables("BAILF010").Rows(intI).Item("date_e") = Format(DateAdd(DateInterval.Year, -1911, mydatasetInq.Tables("bailf010").Rows(0).Item("date_e")), "yyy/MM/dd")
                '    If IsDate(mydatasetInq.Tables("bailf010").Rows(0).Item("date_e")) Then
                '    mydataset.Tables("BAILF010").Rows(intI).Item("date_e") = Format(mydatasetInq.Tables("bailf010").Rows(0).Item("date_e"), "yyy/MM/dd")
                'End If
                'mydataset.Tables("BAILF010").Rows(intI).Item("cop") = mydatasetInq.Tables("bailf010").Rows(0).Item("cop")
                'End If
                'mydatasetInq.Clear()
            End If
        Next
        mydatasetp = Nothing
        mydatasetInq = Nothing
        mydataset.Tables("bailf010").AcceptChanges()
        'DataGridTableStyle1.MappingName = "bailf010"
        dtgBail.DataSource = mydataset
        dtgBail.DataMember = "bailf010"
        bm = Me.BindingContext(mydataset, "bailf010")
        RecMove1.Setds = bm

        'If mydataset.Tables("bailf010").Rows.Count > 0 Then
        'btnPrt.Enabled = True
        '    End If
    End Sub

    Sub transjob(ByVal JobName As String, ByVal JobPara As String) Handles RecMove1.TransJob
        Dim LasPos As Integer
        Select Case JobName
            Case "記錄移動"
                'Dim keyvalue, sqlstr, retstr As String
                'keyvalue = bm.Current("autono")
                txtInqEngno.Text = bm.Current("engno")
                Call bailcount()
                dtgBail.CurrentRowIndex = bm.Position

        End Select
    End Sub


    Private Sub dtgBail_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dtgBail.MouseDown
        Dim myGrid As DataGrid = CType(sender, DataGrid)
        Dim hti As System.Windows.Forms.DataGrid.HitTestInfo
        hti = myGrid.HitTest(e.X, e.Y)
        bm.Position = hti.Row
        Call RecMove1.IOPOS()
    End Sub
    Sub bailcount()
        Dim mydataset As DataSet
        Dim intI As Integer
        Dim isdatenullS As Boolean '判斷保固期限起enf010是否有資料
        Dim isdatenullE As Boolean '判斷保固期限訖enf010是否有資料
        Dim i As Integer
        'If txtInqEngno.Text.Trim.Length <> 7 Or Not IsNumeric(txtInqEngno.Text) Then
        '    MsgBox("工程編號輸入錯誤，請重新輸入!")
        '    Exit Sub
        'End If
        For i = 1 To 4
            isnullDateS(i) = True
            isnullDateE(i) = True
        Next
        isdatenullS = False
        isdatenullE = False


        'btnInq.Enabled = False
        'txtInqEngno.Enabled = False
        TabPage1.Enabled = True
        StatusBar1.Text = ""
        txtEngIdno.Text = ""
        txtEngCop.Text = ""
        lblEngname.Text = ""
        lblEngKind3crS.Visible = False
        dtpEngKind3crS.Visible = False
        lblEngKind3crE.Visible = False
        dtpEngKind3crE.Visible = False
        'If rdbKind3.Checked Or blVisibleDate_s = True Or blVisibleDate_e = True Then
        '    If blVisibleDate_s = True Then
        '        lblKind3crS.Visible = True
        '        dtpkind3crS.Visible = True
        '    Else
        '        lblKind3crS.Visible = False
        '        dtpkind3crS.Visible = False
        '    End If
        '    If blVisibleDate_s = True Then
        '        lblKind3crE.Visible = True
        '        dtpKind3crE.Visible = True
        '    Else
        '        lblKind3crE.Visible = False
        '        dtpKind3crE.Visible = False
        '    End If
        'Else
        '    lblKind3crS.Visible = False
        '    dtpkind3crS.Visible = False
        '    lblKind3crE.Visible = False
        '    dtpKind3crE.Visible = False
        'End If
        ' rdbKind1.Checked = True
        txtKindcp.Text = ""
        dtpkind3crS.Value = Now
        dtpKind3crE.Value = Now
        txtEngno.Text = txtInqEngno.Text
        txtEngno.Text = txtInqEngno.Text
        sqlstr = "SELECT engname,idno,cop,test2_date,keep_date FROM enf010 where engno='" & Trim(txtInqEngno.Text) & "'"
        mydataset = openmember("", "enf010", sqlstr)

        If (mydataset.Tables("enf010").Rows.Count > 0) Then
            lblEngname.Text = nz(mydataset.Tables("enf010").Rows(0).Item("engname"), "")
            txtEngIdno.Text = nz(mydataset.Tables("enf010").Rows(0).Item("idno"), "")
            txtIdno.Text = nz(mydataset.Tables("enf010").Rows(0).Item("idno"), "")
            txtEngCop.Text = nz(mydataset.Tables("enf010").Rows(0).Item("cop"), "")
            txtCop.Text = nz(mydataset.Tables("enf010").Rows(0).Item("cop"), "")

            If Not IsDBNull(mydataset.Tables("enf010").Rows(0).Item("test2_date")) Then  '驗收日
                dtpEngKind3crS.Value = mydataset.Tables("enf010").Rows(0).Item("test2_date")
                lblEngKind3crS.Visible = True
                dtpEngKind3crS.Visible = True
                'dtpkind3crS.Value = mydataset.Tables("enf010").Rows(0).Item("test2_date")
                isdatenullS = True

                DateS(3) = mydataset.Tables("enf010").Rows(0).Item("test2_date")
                isnullDateS(3) = False

            End If
            If Not IsDBNull(mydataset.Tables("enf010").Rows(0).Item("keep_date")) Then  '保固期限
                dtpEngKind3crE.Value = mydataset.Tables("enf010").Rows(0).Item("keep_date")
                lblEngKind3crE.Visible = True
                dtpEngKind3crE.Visible = True
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
                                'dateKind3S = mydataset.Tables("bailf010").Rows(intI).Item("date_s")
                                DateS(1) = mydataset.Tables("bailf010").Rows(intI).Item("date_s")
                                isnullDateS(1) = False
                            Else
                                isnullDateS(1) = True
                            End If

                            If Not IsDBNull(mydataset.Tables("bailf010").Rows(intI).Item("date_e")) Then
                                'dateKind3E = mydataset.Tables("bailf010").Rows(intI).Item("date_e")
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

        KindCheckboxSetup()
        '取得商號名稱及統編
        If (mydataset.Tables("bailf010").Rows.Count > 0) Then
            '如保證金檔(bailf010)有商號名稱資料則填覆蓋原工程檔顯示的商號名稱
            If Not IsDBNull(mydataset.Tables("bailf010").Rows(0).Item("cop")) And nz(mydataset.Tables("bailf010").Rows(0).Item("cop"), "") <> "" And txtCop.Text.Trim = "" Then
                txtCop.Text = mydataset.Tables("bailf010").Rows(0).Item("cop")
            End If
            '如保證金檔(bailf010)有商號統編資料則填覆蓋原工程檔顯示的商號統編
            If Not IsDBNull(mydataset.Tables("bailf010").Rows(0).Item("idno")) And nz(mydataset.Tables("bailf010").Rows(0).Item("idno"), "") <> "" And txtIdno.Text.Trim = "" Then
                txtIdno.Text = mydataset.Tables("bailf010").Rows(0).Item("idno")
            End If
        End If
        If rdbKind1.Enabled Then
            rdbKind1.Checked = True
        ElseIf rdbKind3.Enabled Then
            rdbKind3.Checked = True
        ElseIf rdbKind2.Enabled Then
            rdbKind2.Checked = True
        ElseIf rdbKind4.Enabled Then
            rdbKind4.Checked = True
        End If
        setDateInputStyle()
        txtKindcp.Focus()  '輸入焦點停駐在現收部分
    End Sub

    Private Sub txtKindcp_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtKindcp.TextChanged
        lblAmt.Text = Format(Val(txtKindcp.Text), "##,##0")
    End Sub
    Function checkBalance() As Boolean     '檢查該筆退還之後餘額是否為0
        Dim kindamt1 As Integer, kindamt2 As Integer
        If rdbKind1.Checked Then
            kindamt1 = CType(txtKind1r.Text, Int32) - CType(txtKind1p.Text, Int32) - CType(txtKindcp.Text, Int32)
        Else
            kindamt1 = CType(txtKind1r.Text, Int32) - CType(txtKind1p.Text, Int32)
        End If

        If rdbKind3.Checked Then
            kindamt2 = CType(txtKind3r.Text, Int32) - CType(txtKind3p.Text, Int32) - CType(txtKindcp.Text, Int32)
        Else
            kindamt2 = CType(txtKind3r.Text, Int32) - CType(txtKind3p.Text, Int32)
        End If
        checkBalance = kindamt1 + kindamt2 = 0
    End Function

    'Private Sub dtgBail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtgBail.Click
    '    TabControl1.SelectedIndex = 0
    'End Sub

    Private Sub dtgBail_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtgBail.DoubleClick
        TabControl1.SelectedIndex = 0
    End Sub

    Sub setDateInputStyle()    '當為不為保固金時顯示保證金期限,否則顯示保固期限
        Dim i As Integer
        lblKind3crS.Text = "保證金期限"
        If rdbKind1.Checked Then
            If Not isnullDateS(1) And Not isnullDateE(1) Then
                dtpkind3crS.Value = DateS(1)
                dtpKind3crE.Value = DateE(1)
                lblKind3crS.Visible = True
                dtpkind3crS.Visible = True
                lblKind3crE.Visible = True
                dtpKind3crE.Visible = True
            ElseIf Not isnullDateS(1) Then
                dtpkind3crS.Value = DateS(1)
                lblKind3crS.Visible = True
                dtpkind3crS.Visible = True
                lblKind3crE.Visible = False
                dtpKind3crE.Visible = False
            ElseIf Not isnullDateE(1) Then
                dtpKind3crE.Value = DateE(1)
                lblKind3crS.Visible = False
                dtpkind3crS.Visible = False
                lblKind3crE.Visible = True
                dtpKind3crE.Visible = True
            Else
                lblKind3crS.Visible = False
                dtpkind3crS.Visible = False
                lblKind3crE.Visible = False
                dtpKind3crE.Visible = False
            End If
        ElseIf rdbKind2.Checked Then
            If Not isnullDateS(2) And Not isnullDateE(2) Then
                dtpkind3crS.Value = DateS(2)
                dtpKind3crE.Value = DateE(2)
                lblKind3crS.Visible = True
                dtpkind3crS.Visible = True
                lblKind3crE.Visible = True
                dtpKind3crE.Visible = True
            ElseIf Not isnullDateS(2) Then
                dtpkind3crS.Value = DateS(2)
                lblKind3crS.Visible = True
                dtpkind3crS.Visible = True
                lblKind3crE.Visible = False
                dtpKind3crE.Visible = False
            ElseIf Not isnullDateE(2) Then
                dtpKind3crE.Value = DateE(2)
                lblKind3crS.Visible = False
                dtpkind3crS.Visible = False
                lblKind3crE.Visible = True
                dtpKind3crE.Visible = True
            Else
                lblKind3crS.Visible = False
                dtpkind3crS.Visible = False
                lblKind3crE.Visible = False
                dtpKind3crE.Visible = False
            End If
        ElseIf rdbKind3.Checked Then
            lblKind3crS.Text = "保固期限："
            If Not isnullDateS(3) And Not isnullDateE(3) Then
                dtpkind3crS.Value = DateS(3)
                dtpKind3crE.Value = DateE(3)
                lblKind3crS.Visible = True
                dtpkind3crS.Visible = True
                lblKind3crE.Visible = True
                dtpKind3crE.Visible = True
            ElseIf Not isnullDateS(3) Then
                dtpkind3crS.Value = DateS(3)
                lblKind3crS.Visible = True
                dtpkind3crS.Visible = True
                lblKind3crE.Visible = False
                dtpKind3crE.Visible = False
            ElseIf Not isnullDateE(3) Then
                dtpKind3crE.Value = DateE(3)
                lblKind3crS.Visible = False
                dtpkind3crS.Visible = False
                lblKind3crE.Visible = True
                dtpKind3crE.Visible = True
            Else
                lblKind3crS.Visible = False
                dtpkind3crS.Visible = False
                lblKind3crE.Visible = False
                dtpKind3crE.Visible = False
            End If
        ElseIf rdbKind4.Checked Then
            If Not isnullDateS(4) And Not isnullDateE(4) Then
                dtpkind3crS.Value = DateS(4)
                dtpKind3crE.Value = DateE(4)
                lblKind3crS.Visible = True
                dtpkind3crS.Visible = True
                lblKind3crE.Visible = True
                dtpKind3crE.Visible = True
            ElseIf Not isnullDateS(4) Then
                dtpkind3crS.Value = DateS(4)
                lblKind3crS.Visible = True
                dtpkind3crS.Visible = True
                lblKind3crE.Visible = False
                dtpKind3crE.Visible = False
            ElseIf Not isnullDateE(4) Then
                dtpKind3crE.Value = DateE(4)
                lblKind3crS.Visible = False
                dtpkind3crS.Visible = False
                lblKind3crE.Visible = True
                dtpKind3crE.Visible = True
            Else
                lblKind3crS.Visible = False
                dtpkind3crS.Visible = False
                lblKind3crE.Visible = False
                dtpKind3crE.Visible = False
            End If
        End If
    End Sub
End Class
