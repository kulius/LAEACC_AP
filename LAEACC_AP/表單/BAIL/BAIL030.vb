Imports System.Globalization
Public Class BAIL030
    Dim sqlstr As String
    Dim mydataset As DataSet
    Dim LoadAfter, Dirty As Boolean

    Private Sub frmBail030_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim myds As DataSet
        LoadAfter = True
        TabControl1.Enabled = False
        dtpDate.Value = TransPara.TransP("UserDATE")

        'enableobj(False)

    End Sub

    Private Sub btnInq_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInq.Click
        Dim intI As Integer
        Dim isdatenullS As Boolean '判斷保固期限起enf010是否有資料
        Dim isdatenullE As Boolean '判斷保固期限訖enf010是否有資料
        Dim mydatasetp As DataSet
        Dim datarowp() As DataRow
        'If txtInqEngno.Text.Trim.Length <> 7 Or Not IsNumeric(txtInqEngno.Text) Then
        '    MsgBox("工程編號輸入錯誤，請重新輸入!")
        '    Exit Sub
        'End If
        cleartext()
        isdatenullS = False
        isdatenullE = False

        btnInq.Enabled = False
        txtInqEngno.Enabled = False
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

                dtpkind3crS.Value = mydataset.Tables("enf010").Rows(0).Item("test2_date")
                isdatenullS = True
            End If
            If Not IsDBNull(mydataset.Tables("enf010").Rows(0).Item("keep_date")) Then
                dtpEngKind3crE.Value = mydataset.Tables("enf010").Rows(0).Item("keep_date")
                lblEngKind3crE.Visible = True
                dtpEngKind3crE.Visible = True

                dtpKind3crE.Value = mydataset.Tables("enf010").Rows(0).Item("keep_date")
                isdatenullE = True
            End If
        Else
            Call clearForm()
            MsgBox("工程編號不存在!請重新輸入!")
            Exit Sub
        End If
        mydataset.Clear()
        TabControl1.Enabled = True
        sqlstr = "SELECT * FROM bailf020 where rp='1' and engno='" & Trim(txtInqEngno.Text) & "' and (balance is null or balance='')" '依工程編號找出保管品收的記錄
        mydataset = openmember("", "bailf020", sqlstr)

        sqlstr = "SELECT * FROM bailf020 where rp='2' and engno='" & Trim(txtInqEngno.Text) & "' and (balance is null or balance='')" '依工程編號找出保管品支的記錄
        mydatasetp = openmember("", "bailf020", sqlstr)

        Dim c As DataColumn = New DataColumn
        c.DataType = System.Type.GetType("System.String")
        c.ColumnName = "ckind"
        c.DefaultValue = ""
        mydataset.Tables("bailf020").Columns.Add(c)

        Dim c1 As DataColumn = New DataColumn
        c1.DataType = System.Type.GetType("System.String")
        c1.ColumnName = "pdate"
        mydataset.Tables("bailf020").Columns.Add(c1)
        c1.DefaultValue = ""
        For intI = 0 To (mydataset.Tables("bailf020").Rows.Count - 1)
            Select Case mydataset.Tables("bailf020").Rows(intI).Item("kind")
                Case Is = "1"
                    mydataset.Tables("BAILF020").Rows(intI).Item("ckind") = "履約"
                Case Is = "2"
                    mydataset.Tables("BAILF020").Rows(intI).Item("ckind") = "押標"
                Case Is = "3"
                    If Not IsDBNull(mydataset.Tables("bailf020").Rows(intI).Item("date_s")) And Not isdatenullS Then
                        dtpkind3crS.Value = mydataset.Tables("bailf020").Rows(intI).Item("date_s")
                    End If

                    If Not IsDBNull(mydataset.Tables("bailf020").Rows(intI).Item("date_e")) And Not isdatenullE Then
                        dtpKind3crE.Value = mydataset.Tables("bailf020").Rows(intI).Item("date_e")
                    End If
                    mydataset.Tables("BAILF020").Rows(intI).Item("ckind") = "保固"
                Case Is = "4"
                    mydataset.Tables("BAILF020").Rows(intI).Item("ckind") = "差額"
            End Select

            datarowp = mydatasetp.Tables("Bailf020").Select("kind='" & mydataset.Tables("BAILF020").Rows(intI).Item("kind") & "' and chkno='" & mydataset.Tables("BAILF020").Rows(intI).Item("chkno") & "' and amt=" & mydataset.Tables("BAILF020").Rows(intI).Item("amt"))  '依種類、支票號碼及金額判斷是否退還
            If datarowp.Length > 0 Then
                mydataset.Tables("bailf020").Rows(intI).Item("pdate") = Format(datarowp(0).Item("rpdate"), "yyy/MM/dd")
            End If
        Next
        mydatasetp = Nothing
        datarowp = Nothing
        dtg1.DataSource = mydataset
        dtg1.DataMember = "bailf020"
        If (mydataset.Tables("bailf020").Rows.Count > 0) Then
            '如保管品檔(bailf020)有商號名稱資料則填覆蓋原工程檔顯示的商號名稱
            If Not IsDBNull(mydataset.Tables("bailf020").Rows(0).Item("cop")) And nz(mydataset.Tables("bailf020").Rows(0).Item("cop"), "") <> "" And txtCop.Text.Trim = "" Then
                txtCop.Text = mydataset.Tables("bailf020").Rows(0).Item("cop")
            End If
            '如保管品檔(bailf020)有商號統編資料則填覆蓋原工程檔顯示的商號統編
            If Not IsDBNull(mydataset.Tables("bailf020").Rows(0).Item("idno")) And nz(mydataset.Tables("bailf020").Rows(0).Item("idno"), "") <> "" And txtIdno.Text.Trim = "" Then
                txtIdno.Text = mydataset.Tables("bailf020").Rows(0).Item("idno")
            End If
        End If
        enableobj(True)
        txtBankr.Focus()  '輸入焦點停駐在行庫部分
        'If rdbKind3.Checked Then         '顯示保固品保固期限，若為保管品(如保證書)為保管品的期限
        'lblKind3crS.Text = "保管品期限"
        'dtpkind3crS.Visible = True
        ' dtpKind3crE.Visible = True
        'Else
        '    lblKind3crS.Visible = False
        '    dtpkind3crS.Visible = False
        '    lblKind3crE.Visible = False
        '    dtpKind3crE.Visible = False
        'End If
    End Sub

    Private Sub txtIdno_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtIdno.LostFocus
        'sqlstr = "SELECT * FROM  factory WHERE idno='" & Trim(txtIdno.Text) & "'"
        ' mydataset =  openmember("", "factory", sqlstr)
        ' If (mydataset.Tables("factory").Rows.Count > 0) Then
        ' txtCop.Text = nz(mydataset.Tables("factory").Rows(0).Item("shortname"), "")
        ' End If
        ' mydataset.Clear()
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
        'enableobj(False)
        StatusBar1.Text = ""
        'lblKind3crS.Visible = False
        'dtpkind3crS.Visible = False
        'lblKind3crE.Visible = False
        'dtpKind3crE.Visible = False
        'lblEngKind3crS.Visible = False
        'dtpEngKind3crS.Visible = False
        'lblEngKind3crE.Visible = False
        'dtpEngKind3crE.Visible = False
        btnInq.Enabled = True
        txtInqEngno.Enabled = True
        TabControl1.Enabled = False
        txtInqEngno.Focus()
        mydataset.Clear()
    End Sub
    Private Sub enableobj(ByVal en As Boolean)
        Dim ThisControl As System.Windows.Forms.Control
        For Each ThisControl In Me.TabPage1.Controls
            If ThisControl.Name <> "dtpkind3crS" And ThisControl.Name <> "dtpKind3crE" Then   '除了保固期限日期外的其他tabpage1的元件
                ThisControl.Enabled = en
            End If
        Next ThisControl
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
        If txtCop.Text.Length > 25 Then
            StatusBar1.Text = "商號名稱最大長度為25個字元!"
            Exit Sub
        End If
        If txtIdno.Text.Length > 10 Then
            StatusBar1.Text = "商號統編最大長度為10個字元!"
            Exit Sub
        End If
        'mydataset.Tables.Clear()
        '94/07/13 因同一天有押標轉履約及補足履約品部分,故有兩筆，拿掉資料重複判斷。
        'sqlstr = "SELECT * FROM bailf020 where engno='" & Trim(txtInqEngno.Text) & "' and kind='" & jkind & "' and rp='1' and rpdate='" & dtpDate.Value.ToString("yyyy/M/d", CultureInfo.InvariantCulture) & "' and chkno='" & Trim(txtChknor.Text) & "'"
        'mydataset =  openmember("", "bailf020", sqlstr)
        'If (mydataset.Tables("bailf020").Rows.Count > 0) Then
        'StatusBar1.Text = "資料重複!"
        'Call restart()
        'Exit Sub
        'End If
        If txtBankr.Text.Trim <> "" And txtChknor.Text.Trim <> "" And Val(txtAmtr.Text) > 0 Then

            If chkPeriod.Checked Then
                Dim intPeriod As Integer

                For intPeriod = 1 To 4
                    GenInsSql("engno", txtEngno.Text, "T")
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
                    GenInsSql("cop", txtCop.Text, "T")
                    GenInsSql("chkno", txtChknor.Text & "-" & Trim(intPeriod.ToString), "T")
                    GenInsSql("bank", txtBankr.Text, "T")
                    GenInsSql("amt", (Val(txtAmtr.Text) / 4).ToString, "N")
                    sqlstr = "insert into bailf020 " & GenInsFunc
                    retstr = sqlstr
                    retstr = runsql(mastconn, sqlstr)
                    ClearInsField_Insvalue()  '清除InsField及Insvalue值
                Next

            Else
                GenInsSql("engno", txtEngno.Text, "T")
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
                GenInsSql("cop", txtCop.Text, "T")
                GenInsSql("chkno", txtChknor.Text, "T")
                GenInsSql("bank", txtBankr.Text, "T")
                GenInsSql("amt", txtAmtr.Text, "N")
                sqlstr = "insert into bailf020 " & GenInsFunc
                retstr = runsql(mastconn, sqlstr)

            End If

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
            StatusBar1.Text = "資料輸入不完整，請檢查輸入值!"
        End If
    End Sub
    Private Sub cleartext()
        StatusBar1.Text = ""
        txtBankr.Text = ""
        txtChknor.Text = ""
        txtAmtr.Text = ""
    End Sub

    Private Sub rdbKind3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbKind3.CheckedChanged
        setDateInputStyle()
    End Sub

    Private Sub btnGetIdCop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetIdCop.Click
        txtIdno.Text = txtEngIdno.Text
        If txtEngCop.Text.Length >= 5 Then
            txtCop.Text = txtEngCop.Text.Substring(0, 4)
        Else
            txtCop.Text = txtEngCop.Text
        End If
        If dtpEngKind3crS.Visible = True Then
            dtpkind3crS.Value = dtpEngKind3crS.Value
        End If
        If dtpEngKind3crE.Visible = True Then
            dtpKind3crE.Value = dtpEngKind3crE.Value
        End If
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

    Private Sub txtAmtr_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAmtr.TextChanged
        lblAmt.Text = Format(Val(txtAmtr.Text), "##,##0")
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

    Private Sub rdbKind1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbKind1.CheckedChanged
        setDateInputStyle()
    End Sub
    Sub setDateInputStyle()    '當為履約品時設定為保證品日期輸入樣式,否則輸入保固期限樣式
        If rdbKind3.Checked Then           '保固品日期顯示(保固期限)
            lblKind3crS.Text = "保固期限："
            chk_dtpDateS.Visible = False
            chk_dtpDateS.Checked = True
            dtpkind3crS.Enabled = True
            chk_dtpDateE.Visible = False
            chk_dtpDateE.Checked = True
            dtpKind3crE.Enabled = True
        Else                                '履約品日期顯示(保管品期限)
            lblKind3crS.Text = "保管品期限"
            chk_dtpDateS.Visible = True
            chk_dtpDateS.Checked = False
            dtpkind3crS.Enabled = False
            chk_dtpDateE.Visible = True
            chk_dtpDateE.Checked = False
            dtpKind3crE.Enabled = False
        End If
    End Sub


    Private Sub dtpkind3crS_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpkind3crS.ValueChanged
        If rdbKind3.Checked Then
            dtpKind3crE.Value = dtpkind3crS.Value.AddYears(3)  '保固預設加三年
        End If

    End Sub
End Class
