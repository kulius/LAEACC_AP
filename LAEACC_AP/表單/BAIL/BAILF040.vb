Imports System.Globalization
Imports JBC.Printing
Imports System.IO
Imports Microsoft.Office.Interop
Public Class BAILF040
    Dim LoadAfter, Dirty As Boolean, cboFirst As Boolean

    Public Class Idname
        Private myIdName As String
        Private myName As String

        Public Sub New(ByVal strName As String, ByVal strIdName As String)
            MyBase.New()
            Me.myIdName = strIdName
            Me.myName = strName
        End Sub

        Public ReadOnly Property IdName() As String
            Get
                Return myIdName
            End Get
        End Property

        Public ReadOnly Property CName() As String
            Get
                Return myName
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return Me.IdName & " - " & Me.CName
        End Function
    End Class
    Dim sqlstr, sqlstr1 As String
    Dim bm As BindingManagerBase, mydataset As DataSet
    Dim UserId, UserName, UserUnit As String
    Dim oldEngno As String, oldKind As String

    Sub LoadGridFunc()
        cboFirst = False
        RecMove1.FilesPara = "mastconn=" & mastconn & "|sqlstr=" & sqlstr
        DataGrid1.DataSource = mydataset
        DataGrid1.DataMember = "bailf020"
        bm = Me.BindingContext(mydataset, "bailf020")
        RecMove1.Setds = bm
        txtId.DataBindings.Clear()
        txtEngno.DataBindings.Clear()
        lblEngname.DataBindings.Clear()
        cboKind.DataBindings.Clear()
        dtpDate.DataBindings.Clear()
        cboRp.DataBindings.Clear()
        txtIdno.DataBindings.Clear()
        txtCop.DataBindings.Clear()
        txtAmt.DataBindings.Clear()
        dtpDateS.DataBindings.Clear()
        dtpDateE.DataBindings.Clear()
        txtNo1No.DataBindings.Clear()
        txtChkno.DataBindings.Clear()
        txtBank.DataBindings.Clear()
        txtBalance.DataBindings.Clear()
        txtId.DataBindings.Add(New Binding("Text", mydataset, "bailf020.autono"))
        txtEngno.DataBindings.Add(New Binding("Text", mydataset, "bailf020.engno"))
        lblEngname.DataBindings.Add(New Binding("Text", mydataset, "bailf020.engname"))
        cboKind.DataBindings.Add(New Binding("SelectedValue", mydataset, "bailf020.kind"))
        dtpDate.DataBindings.Add(New Binding("Text", mydataset, "bailf020.rpdate"))
        cboRp.DataBindings.Add(New Binding("SelectedValue", mydataset, "bailf020.Rp"))
        txtIdno.DataBindings.Add(New Binding("Text", mydataset, "bailf020.Idno"))
        txtCop.DataBindings.Add(New Binding("Text", mydataset, "bailf020.Cop"))
        txtAmt.DataBindings.Add(New Binding("Text", mydataset, "bailf020.Amt"))
        dtpDateS.DataBindings.Add(New Binding("Text", mydataset, "bailf020.date_s"))
        dtpDateE.DataBindings.Add(New Binding("Text", mydataset, "bailf020.date_e"))
        txtNo1No.DataBindings.Add(New Binding("Text", mydataset, "bailf020.no_1_no"))
        txtChkno.DataBindings.Add(New Binding("text", mydataset, "bailf020.chkno"))
        txtBank.DataBindings.Add(New Binding("text", mydataset, "bailf020.bank"))
        txtBalance.DataBindings.Add(New Binding("text", mydataset, "bailf020.balance"))
        cboFirst = True
    End Sub
    Sub transjob(ByVal JobName As String, ByVal JobPara As String) Handles RecMove1.TransJob
        Dim LasPos As Integer
        Select Case JobName
            Case "記錄移動"
                oldEngno = bm.Current("engno") 'txtEngno.Text
                oldKind = bm.Current("kind")   'cboKind.SelectedValue
                check_dtpdate()
            Case "刪除記錄"
                Dim keyvalue, sqlstr, retstr As String
                If bm.Current("rp") = 1 Then
                    If amtCheck(bm.Current("autono"), JobName) Then
                        MsgBox("保證金支出金額大於收入金額，請檢查輸入值!")
                        Exit Sub
                    End If
                End If
                keyvalue = bm.Current("autono")
                LasPos = bm.Position
                sqlstr = "delete from bailf020 where autono='" & keyvalue & "'"
                retstr = runsql(mastconn, sqlstr)
                If retstr = "sqlok" Then
                    backuprtn("", Application.StartupPath, sqlstr)
                    'mydataset.Tables("bailf020").Rows.RemoveAt(JobPara)
                    mydataset.Tables("bailf020").Clear()
                    mydataset = openmember("", "bailf020", sqlstr1)
                    clearForm()
                    Call LoadGridFunc()
                    'clearForm()
                    bm.Position = LasPos
                    Call RecMove1.IOPOS()
                End If

            Case "回寫記錄cmd"
                Dim keyvalue, sqlstr, retstr, updstr As String
                Dim obm As Integer
                If Not checkInput() Then Exit Sub
                If amtCheck(bm.Current("autono"), JobName) Then
                    MsgBox("保證金支出金額大於收入金額，請檢查輸入值!")
                    Exit Sub
                End If
                keyvalue = bm.Current("autono")
                '******************************
                LasPos = bm.Position
                'RecMove1.genupdsql("seq", bm.Current("seq"), "N")
                'RecMove1.GenUpdsql("id", bm.Current("id"), "N")
                RecMove1.GenUpdsql("engno", bm.Current("engno"), "T")
                RecMove1.GenUpdsql("kind", bm.Current("kind"), "T")
                RecMove1.GenUpdsql("rpdate", bm.Current("rpdate"), "D")
                RecMove1.GenUpdsql("rp", bm.Current("rp"), "T")
                RecMove1.GenUpdsql("idno", bm.Current("idno"), "T")
                RecMove1.GenUpdsql("cop", bm.Current("cop"), "TU")
                RecMove1.GenUpdsql("chkno", bm.Current("chkno"), "T")
                RecMove1.GenUpdsql("bank", bm.Current("bank"), "T")
                RecMove1.GenUpdsql("amt", bm.Current("amt"), "N")
                If bm.Current("kind") = 3 Then  '只有保固品有保固期限,94/6/27改如為履約品為保管品的期限
                    RecMove1.GenUpdsql("date_s", dtpDateS.Text, "D")
                    RecMove1.GenUpdsql("date_e", dtpDateE.Text, "D")
                Else                                  '履約品為保管品的期限
                    If chk_dtpDateS.Checked = False Then
                        RecMove1.GenUpdsql("date_s", DBNull.Value, "D")
                    Else
                        RecMove1.GenUpdsql("date_s", dtpDateS.Text, "D")
                    End If
                    If chk_dtpDateE.Checked = False Then
                        RecMove1.GenUpdsql("date_e", DBNull.Value, "D")
                    Else
                        RecMove1.GenUpdsql("date_e", dtpDateE.Text, "D")
                    End If
                End If
                RecMove1.GenUpdsql("no_1_no", bm.Current("no_1_no"), "N")

                sqlstr = "update bailf020 set " & RecMove1.genupdfunc & " where autono=" & keyvalue
                '********************



                retstr = runsql(mastconn, sqlstr)
                If retstr = "sqlok" Then
                    backuprtn("", Application.StartupPath, sqlstr)
                    'mydataset.Tables("bailf020").Rows.RemoveAt(JobPara)
                    mydataset.Tables("bailf020").Clear()
                    mydataset = openmember("", "bailf020", sqlstr1)
                    Call LoadGridFunc() ' 94/2/15 mark 因重新load後不會停留在同一筆
                    bm.Position = LasPos
                    Call RecMove1.IOPOS()
                    MsgBox("更新成功")
                End If


            Case "新增記錄"
                Dim keyvalue, sqlstr, retstr, updstr As String
                If Not checkInput() Then Exit Sub
                If cboRp.SelectedValue = 2 Then
                    If amtCheck(bm.Current("autono"), JobName) Then
                        MsgBox("保證金支出金額大於收入金額，請檢查輸入值!")
                        Exit Sub
                    End If
                End If

                If bm Is Nothing OrElse bm.Position = -1 Then
                    LasPos = -1
                    sqlstr1 = "SELECT   BAILF020.ENGNO, ENF010.ENGNAME, BAILF020.KIND, BAILF020.RPDATE,bailf020.chkno,bailf020.bank,BAILF020.RP, BAILF020.IDNO, BAILF020.COP, BAILF020.AMT, BAILF020.DATE_S, BAILF020.DATE_E, BAILF020.NO_1_NO,bailf020.autono,bailf020.balance FROM BAILF020 left JOIN ENF010 ON BAILF020.ENGNO = ENF010.ENGNO where enf010.engno='" & txtEngno.Text.Trim & "'" ' and (balance is null or balance='') "
                    RecMove1.GenInsSql("engno", txtEngno.Text.Trim, "T")
                    RecMove1.GenInsSql("kind", cboKind.SelectedValue, "T")
                    RecMove1.GenInsSql("rpdate", dtpDate.Value.Date, "D")
                    RecMove1.GenInsSql("rp", cboRp.SelectedValue, "T")
                    RecMove1.GenInsSql("idno", txtIdno.Text.Trim, "T")
                    RecMove1.GenInsSql("cop", txtCop.Text.Trim, "T")
                    RecMove1.GenInsSql("chkno", txtChkno.Text.Trim, "T")
                    RecMove1.GenInsSql("bank", txtBank.Text.Trim, "T")
                    RecMove1.GenInsSql("amt", txtAmt.Text.Trim, "N")
                    If cboKind.SelectedValue = "3" Then   '只有保固金有保固期限
                        RecMove1.GenInsSql("date_s", dtpDateS.Value.Date, "D")
                        RecMove1.GenInsSql("date_e", dtpDateE.Value.Date, "D")

                    Else                                  '履約品為保管品的期限
                        If chk_dtpDateS.Checked = False Then
                            RecMove1.GenInsSql("date_s", DBNull.Value, "D")
                        Else
                            RecMove1.GenInsSql("date_s", dtpDateS.Text, "D")
                        End If
                        If chk_dtpDateE.Checked = False Then
                            RecMove1.GenInsSql("date_e", DBNull.Value, "D")
                        Else
                            RecMove1.GenInsSql("date_e", dtpDateE.Text, "D")
                        End If
                    End If
                    RecMove1.GenInsSql("no_1_no", txtNo1No.Text, "N")
                Else
                    LasPos = bm.Position
                    sqlstr1 = "SELECT   BAILF020.ENGNO, ENF010.ENGNAME, BAILF020.KIND, BAILF020.RPDATE,bailf020.chkno,bailf020.bank,BAILF020.RP, BAILF020.IDNO, BAILF020.COP, BAILF020.AMT, BAILF020.DATE_S, BAILF020.DATE_E, BAILF020.NO_1_NO,bailf020.autono,bailf020.balance FROM BAILF020 left JOIN ENF010 ON BAILF020.ENGNO = ENF010.ENGNO where enf010.engno='" & txtEngno.Text.Trim & "'" ' and (balance is null or balance='') "
                    RecMove1.GenInsSql("engno", bm.Current("engno"), "T")
                    RecMove1.GenInsSql("kind", bm.Current("kind"), "T")
                    RecMove1.GenInsSql("rpdate", bm.Current("rpdate"), "D")
                    RecMove1.GenInsSql("rp", bm.Current("rp"), "T")
                    RecMove1.GenInsSql("idno", bm.Current("idno"), "T")
                    RecMove1.GenInsSql("cop", bm.Current("cop"), "TU")
                    RecMove1.GenInsSql("chkno", bm.Current("chkno"), "T")
                    RecMove1.GenInsSql("bank", bm.Current("bank"), "T")
                    RecMove1.GenInsSql("amt", bm.Current("amt"), "N")
                    If bm.Current("kind") = "3" Then   '只有保固品有保固期限,94/6/27改如為履約品為保管品的期限
                        RecMove1.GenInsSql("date_s", bm.Current("date_s"), "D")
                        RecMove1.GenInsSql("date_e", bm.Current("date_e"), "D")
                    Else                                '如為履約品為保管品的期限
                        If chk_dtpDateS.Checked = False Then
                            RecMove1.GenInsSql("date_s", DBNull.Value, "D")
                        Else
                            RecMove1.GenInsSql("date_s", dtpDateS.Text, "D")
                        End If
                        If chk_dtpDateE.Checked = False Then
                            RecMove1.GenInsSql("date_e", DBNull.Value, "D")
                        Else
                            RecMove1.GenInsSql("date_e", dtpDateE.Text, "D")
                        End If
                    End If
                    RecMove1.GenInsSql("no_1_no", bm.Current("no_1_no"), "N")
                End If
                sqlstr = "insert into bailf020 " & RecMove1.GenInsFunc
                ' sqlstr = "insert into psname (seq,psstr,unit) values(" & bm.Current("seq") & ",'" & bm.Current("psstr") & "','" & bm.Current("unit") & "')"
                retstr = runsql(mastconn, sqlstr)

                If retstr = "sqlok" Then
                    backuprtn("", Application.StartupPath, sqlstr)
                    'mydataset.Tables("bailf020").Rows.RemoveAt(JobPara)
                    If Not mydataset Is Nothing Then
                        mydataset.Tables("bailf020").Clear()
                    End If
                    mydataset = openmember("", "bailf020", sqlstr1)
                    Call LoadGridFunc()
                    bm.Position = LasPos
                    Call RecMove1.IOPOS()
                    MsgBox("新增成功")
                Else
                    MsgBox("新增失敗!")
                End If
        End Select
    End Sub

    Function checkInput() As Boolean
        'Dim blok As Boolean
        'blok = True
        If txtEngno.Text.Trim = "" Then 'Or txtEngno.Text.Trim.Length <> 7 Then
            MsgBox("工程編號輸入錯誤!")
            checkInput = False
        ElseIf txtCop.Text.Trim = "" Or txtCop.Text.Trim.Length > 25 Then
            MsgBox("商號名稱輸入錯誤!")
            checkInput = False
        ElseIf Not IsNumeric(txtAmt.Text) Or Val(txtAmt.Text.Trim) <= 0 Then
            MsgBox("金額輸入錯誤!")
            checkInput = False
        ElseIf txtChkno.Text.Trim = "" Then
            MsgBox("支票號輸入錯誤!")
            checkInput = False
        ElseIf txtBank.Text.Trim = "" Then
            MsgBox("行庫輸入錯誤!")
            checkInput = False
        Else
            checkInput = True
        End If
    End Function
    Private Sub DataGrid1_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ' If IsDBNull(bm.Current("unit")) Then
        ' bm.Current("unit") = UserUnit

        'End If
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

    Private Sub DataGrid1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'txtEngno.Text = bm.Current("engno")
        'txtEngname.Text = bm.Current("name")
        'Seq.Text = bm.Current("seq")
        Dirty = False
    End Sub



    Sub clearForm()
        txtId.Text = ""
        txtEngno.Text = ""
        cboKind.SelectedValue = "1"
        dtpDate.Text = Today.ToString
        cboRp.SelectedValue = "1"
        txtIdno.Text = ""
        txtCop.Text = ""
        txtChkno.Text = ""
        txtBank.Text = ""
        txtAmt.Text = ""
        dtpDateS.Text = Today.ToString
        dtpDateE.Text = Today.ToString
        txtNo1No.Text = ""
        txtBalance.Text = ""

    End Sub

    Sub check_dtpdate()
        If bm.Position <> -1 Then
            If IsDBNull(bm.Current("date_s")) Then '因DatetimePicker繫結欄位時,如欄位值為DBnull時會顯示為今日日期，故改為不顯示
                chk_dtpDateS.Checked = False
                dtpDateS.Enabled = False
                'MsgBox("dtpdateS.Checked is " & dtpDateS.Checked.ToString)
            Else
                chk_dtpDateS.Checked = True
                dtpDateS.Enabled = True
            End If

            If IsDBNull(bm.Current("date_e")) Then
                chk_dtpDateE.Checked = False
                dtpDateE.Enabled = False
            Else
                chk_dtpDateE.Checked = True
                dtpDateE.Enabled = True
            End If
        End If
    End Sub


    Private Sub frmBailf020_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim myds As DataSet
        LoadAfter = True
        UserName = TransPara.TransP("username")
        UserUnit = TransPara.TransP("userunit")
        UserId = TransPara.TransP("userid")
        'Label1.Text = UserName & " " & UserUnit
        Dim RpIdnames As New ArrayList
        RpIdnames.Add(New Idname("收", "1"))
        RpIdnames.Add(New Idname("支", "2"))
        cboRp.DataSource = RpIdnames
        cboRp.DisplayMember = "CName"
        cboRp.ValueMember = "IdName"

        Dim RpIdnames_1 As New ArrayList
        RpIdnames_1.Add(New Idname("收", "1"))
        RpIdnames_1.Add(New Idname("支", "2"))
        cboInqRp.DataSource = RpIdnames_1
        cboInqRp.DisplayMember = "CName"
        cboInqRp.ValueMember = "IdName"

        Dim KindIdnames As New ArrayList
        KindIdnames.Add(New Idname("履約", "1"))
        KindIdnames.Add(New Idname("押標", "2"))
        KindIdnames.Add(New Idname("保固", "3"))
        KindIdnames.Add(New Idname("差額", "4"))
        cboKind.DataSource = KindIdnames
        cboKind.DisplayMember = "CName"
        cboKind.ValueMember = "IdName"

        Dim KindIdnames_1 As New ArrayList
        KindIdnames_1.Add(New Idname("履約", "1"))
        KindIdnames_1.Add(New Idname("押標", "2"))
        KindIdnames_1.Add(New Idname("保固", "3"))
        KindIdnames_1.Add(New Idname("差額", "4"))
        cboInqKind.DataSource = KindIdnames_1
        cboInqKind.DisplayMember = "CName"
        cboInqKind.ValueMember = "IdName"

        dtpRpdateStart.Value = TransPara.TransP("UserDATE")
        dtpRpdateStop.Value = TransPara.TransP("UserDATE")
        dtpDate.Value = TransPara.TransP("UserDATE")

        Dirty = False
        StatusBar1.Text = ""
        If TransPara.TransP("Userunit") = "0403" Then
            RecMove1.Enabled = False
        Else
            RecMove1.Enabled = True
        End If
        btnPrt.Enabled = False


    End Sub



    Private Sub DataGrid1_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        Dim myGrid As DataGrid = CType(sender, DataGrid)
        Dim hti As System.Windows.Forms.DataGrid.HitTestInfo
        hti = myGrid.HitTest(e.X, e.Y)
        bm.Position = hti.Row
        Call RecMove1.IOPOS()
    End Sub


    Private Sub btnEnd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnd.Click
        Me.Close()
    End Sub

    Function amtCheck(ByVal currentRec As Integer, ByVal currentJob As String) As Boolean
        'Dim intI As Integer
        Dim mydatase As DataSet
        Dim engno As String
        Dim kind As String
        Dim amt1 As Integer
        Dim amt2 As Integer
        If currentJob = "回寫記錄cmd" Then   '檢查更修改前的工程編號及保證種是否支出大於收入
            sqlstr = "SELECT engno,sum(amt) as amt  FROM bailf020 where rp='1' and kind='" & oldKind & "' and engno='" & oldEngno & "' and autono<>" & currentRec & " and (balance is null or balance='') group by engno"      '依工程編號找出保證金收的記錄
            mydataset = openmember("", "bailf020", sqlstr)
            If mydataset.Tables("bailf020").Rows.Count > 0 Then
                amt1 = mydataset.Tables("bailf020").Rows(0).Item("amt")
            End If
            mydataset.Clear()
            sqlstr = "SELECT engno,sum(amt) as amt  FROM bailf020 where rp='2' and kind='" & oldKind & "' and engno='" & oldEngno & "' and autono<>" & currentRec & " and (balance is null or balance='') group by engno" '依工程編號找出保證金支的記錄
            mydataset = openmember("", "bailf020", sqlstr)
            If mydataset.Tables("bailf020").Rows.Count > 0 Then
                amt2 = mydataset.Tables("bailf020").Rows(0).Item("amt")
            End If
            'If amt2 > amt1 Then             '97/5/12 remark
            '    'msgBox("保證金支出金額大於收入金額，請檢查輸入值!")
            '    amtCheck = amt2 > amt1
            '    Exit Function
            'End If
        End If
        If currentJob = "刪除記錄" Then
            engno = oldEngno 'currentEngno
            kind = oldKind   'currentkind
        Else
            engno = txtEngno.Text
            kind = cboKind.SelectedValue
        End If
        If currentJob = "新增記錄" Then
            sqlstr = "SELECT engno,sum(amt) as amt  FROM bailf020 where rp='1' and kind='" & kind & "' and engno='" & engno & "' and (balance is null or balance='') group by engno"      '依工程編號找出保證金收的記錄
        Else
            sqlstr = "SELECT engno,sum(amt) as amt  FROM bailf020 where rp='1' and kind='" & kind & "' and engno='" & engno & "' and autono<>" & currentRec & " and (balance is null or balance='') group by engno"      '依工程編號找出保證金收的記錄
        End If
        mydataset = openmember("", "bailf020", sqlstr)
        If mydataset.Tables("bailf020").Rows.Count > 0 Then
            amt1 = mydataset.Tables("bailf020").Rows(0).Item("amt")
        End If
        mydataset.Clear()
        If currentJob = "新增記錄" Then
            sqlstr = "SELECT engno,sum(amt) as amt  FROM bailf020 where rp='2' and kind='" & kind & "' and engno='" & engno & "' and (balance is null or balance='') group by engno" '依工程編號找出保證金支的記錄
        Else
            sqlstr = "SELECT engno,sum(amt) as amt  FROM bailf020 where rp='2' and kind='" & kind & "' and engno='" & engno & "' and autono<>" & currentRec & " and (balance is null or balance='') group by engno" '依工程編號找出保證金支的記錄
        End If
        mydataset = openmember("", "bailf020", sqlstr)
        If mydataset.Tables("bailf020").Rows.Count > 0 Then
            amt2 = mydataset.Tables("bailf020").Rows(0).Item("amt")
        End If
        If currentJob <> "刪除記錄" Then
            If cboRp.SelectedValue = 1 Then
                amt1 = amt1 + Val(txtAmt.Text)
            Else
                amt2 = amt2 + Val(txtAmt.Text)
            End If
        End If
        amtCheck = amt2 > amt1

    End Function


    Private Sub txtAmt_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAmt.TextChanged
        lblamt.Text = Format(Val(txtAmt.Text), "##,##0")
    End Sub

    'Private Sub cboKind_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboKind.SelectedIndexChanged
    '    If cboKind.SelectedIndex = 1 Then  '種類保固顯示保固期限,履約金為保證品期限
    '        dtpDateS.Visible = True
    '        dtpDateE.Visible = True
    '    Else
    '        dtpDateS.Visible = False
    '        dtpDateE.Visible = False
    '    End If
    'End Sub

    Private Sub TabPage2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabPage2.Click

    End Sub

    Private Sub btnInq_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInq.Click
        Dim sqlstr As String
        Dim countchk As Int16
        Dim txtSEngno As String
        Dim txtEEngno As String
        Dim txtTempEngno As String
        Dim dateSRpdate As Date
        Dim dateERpdate As Date
        Dim dateTemp As Date

        If rdbEngnoInq.Checked Then
            'rdbSort1.Checked = True
            If txtEngnoStop.Text.Trim = "" Then txtEngnoStop.Text = txtEngnoStart.Text
            txtSEngno = txtEngnoStart.Text.Trim  '開始編號
            txtEEngno = txtEngnoStop.Text.Trim   '結束編號
            If txtEEngno < txtSEngno Then
                txtTempEngno = txtSEngno
                txtSEngno = txtEEngno
                txtEEngno = txtTempEngno
            End If
            If txtEngnoStart.Text.Trim <> "" And txtEngnoStop.Text.Trim <> "" Then
                sqlstr = "SELECT   BAILF020.ENGNO, ENF010.ENGNAME, BAILF020.KIND, BAILF020.RPDATE,bailf020.chkno,bailf020.bank,BAILF020.RP, BAILF020.IDNO, BAILF020.COP, BAILF020.AMT, BAILF020.DATE_S, BAILF020.DATE_E, BAILF020.NO_1_NO,bailf020.autono,bailf020.balance FROM BAILF020 left JOIN ENF010 ON BAILF020.ENGNO = ENF010.ENGNO where enf010.engno>='" & txtSEngno & "' and enf010.engno<='" & txtEEngno & "' and (balance is null or balance='') "

            Else
                MsgBox("請檢查輸入值!")
                Exit Sub
            End If
        Else
            'rdbSort4.Checked = True
            dateSRpdate = dtpRpdateStart.Value  '開如日期
            dateERpdate = dtpRpdateStop.Value   '結束日期
            If dateERpdate < dateSRpdate Then
                dateTemp = dateSRpdate
                dateSRpdate = dateERpdate
                dateERpdate = dateTemp
            End If
            'If txtEngnoStart.Text.Trim <> "" And txtEngnoStop.Text.Trim <> "" Then
            sqlstr = "SELECT   BAILF020.ENGNO, ENF010.ENGNAME, BAILF020.KIND, BAILF020.RPDATE,bailf020.chkno,bailf020.bank,BAILF020.RP, BAILF020.IDNO, BAILF020.COP, BAILF020.AMT, BAILF020.DATE_S, BAILF020.DATE_E, BAILF020.NO_1_NO,bailf020.autono,bailf020.balance FROM BAILF020 left JOIN ENF010 ON BAILF020.ENGNO = ENF010.ENGNO where bailf020.rpdate>='" & dateSRpdate.ToString("yyyy/M/d", CultureInfo.InvariantCulture) & "' and bailf020.rpdate<='" & dateERpdate.ToString("yyyy/M/d", CultureInfo.InvariantCulture) & "' and (balance is null or balance='') "

            'End If
        End If

        If chkKind.Checked Then
            GenWheresql("bailf020.kind", cboInqKind.SelectedValue, "T")
            countchk += 1
        End If

        If chkRp.Checked Then
            GenWheresql("bailf020.rp", cboInqRp.SelectedValue, "T")
            countchk += 1
        End If

        If chkIdno.Checked Then
            GenWheresql("bailf020.idno", txtInqIdno.Text, "T")
            countchk += 1
        End If

        If chkAmt.Checked Then
            GenWheresql("bailf020.amt", txtInqAmt.Text, "N")
            countchk += 1
        End If

        If chkChkno.Checked Then
            GenWheresql("bailf020.chkno", txtInqChkno.Text, "T")
            countchk += 1
        End If

        If chkCop.Checked Then
            'GenWheresql("bailf020.cop", txtInqCop.Text, "T")
            countchk += 1
        End If
        If countchk > 0 Then
            If (Not txtInqIdno.Text.Trim.Length > 0 And chkIdno.Checked Or Not IsNumeric(txtInqAmt.Text) And Val(txtInqAmt.Text) >= 0 And chkAmt.Checked Or Not txtInqCop.Text.Trim.Length > 0 And chkCop.Checked Or Not txtInqChkno.Text.Trim.Length > 0 And chkChkno.Checked) Then
                MsgBox("請檢查輸入值!")
                Exit Sub
            Else
                cboFirst = False
                If chkCop.Checked And countchk = 1 Then
                    sqlstr &= "and  bailf020.cop like '%" & txtInqCop.Text.Trim & "%'"
                ElseIf chkCop.Checked Then
                    sqlstr &= " and bailf020.cop like '%" & txtInqCop.Text.Trim & "%'"
                    sqlstr &= " and " & genwherefunc
                Else
                    sqlstr &= " and " & genwherefunc
                End If
            End If
        End If
        If rdbSort1.Checked Then
            sqlstr &= " order by bailf020.engno,bailf020.kind,bailf020.rpdate,bailf020.chkno,bailf020.autono"
        ElseIf rdbSort2.Checked Then
            sqlstr &= " order by bailf020.idno,bailf020.engno,bailf020.kind,bailf020.rpdate,bailf020.autono"
        ElseIf rdbSort3.Checked Then
            sqlstr &= " order by bailf020.kind,bailf020.engno,bailf020.rpdate,bailf020.chkno,bailf020.autono"
        ElseIf rdbSort4.Checked Then
            sqlstr &= " order by bailf020.rpdate,bailf020.engno,bailf020.kind,bailf020.chkno,bailf020.autono"
        End If
        sqlstr1 = sqlstr
        mydataset = openmember("", "bailf020", sqlstr)

        Call LoadGridFunc()
        If mydataset.Tables("bailf020").Rows.Count = 0 Then
            MsgBox("查詢的資料不存在!")
            clearForm()
        End If
        TabControl1.SelectedIndex = 0
        btnPrt.Enabled = True
    End Sub
    Private Sub txtInqAmt_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtInqAmt.Validated
        If Not IsNumeric(txtInqAmt.Text) And Val(txtInqAmt.Text) >= 0 Then
            ErrorProvider1.SetError(Me.txtInqAmt, "請輸入數字!")
        Else
            ErrorProvider1.SetError(Me.txtInqAmt, "")
        End If
    End Sub

    Private Sub chkAmt_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAmt.CheckedChanged
        If Not IsNumeric(txtInqAmt.Text) And Val(txtInqAmt.Text) >= 0 And chkAmt.Checked Then
            ErrorProvider1.SetError(Me.txtInqAmt, "請輸入數字!")
        Else
            ErrorProvider1.SetError(Me.txtInqAmt, "")
        End If
    End Sub

    Private Sub chkIdno_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkIdno.CheckedChanged
        If Not txtInqIdno.Text.Trim.Length > 0 And chkIdno.Checked Then
            ErrorProvider1.SetError(Me.txtInqIdno, "請輸入統一編號!")
        Else
            ErrorProvider1.SetError(Me.txtInqIdno, "")
        End If
    End Sub

    Private Sub txtInqIdno_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtInqIdno.Validated
        If Not txtInqIdno.Text.Trim.Length > 0 And chkIdno.Checked Then
            ErrorProvider1.SetError(Me.txtInqIdno, "請輸入統一編號!")
        Else
            ErrorProvider1.SetError(Me.txtInqIdno, "")
        End If
    End Sub


    Private Sub chkChkno_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkChkno.CheckedChanged
        If Not txtInqChkno.Text.Trim.Length > 0 And chkChkno.Checked Then
            ErrorProvider1.SetError(Me.txtInqChkno, "請輸入工程代號!")
        Else
            ErrorProvider1.SetError(Me.txtInqChkno, "")
        End If
    End Sub

    Private Sub txtInqChkno_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtInqChkno.Validated
        If Not txtInqChkno.Text.Trim.Length > 0 And chkChkno.Checked Then
            ErrorProvider1.SetError(Me.txtInqChkno, "請輸入工程代號!")
        Else
            ErrorProvider1.SetError(Me.txtInqChkno, "")
        End If
    End Sub

    Private Sub rdbEngnoInq_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbEngnoInq.CheckedChanged
        rdbSort1.Checked = True
    End Sub

    Private Sub rdbRpdateInq_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbRpdateInq.CheckedChanged
        rdbSort4.Checked = True
    End Sub

    Private Sub chk_dtpDateS_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk_dtpDateS.CheckedChanged
        If chk_dtpDateS.Checked = True Then
            dtpDateS.Enabled = True
        Else
            dtpDateS.Enabled = False
        End If
    End Sub

    Private Sub chk_dtpDateE_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk_dtpDateE.CheckedChanged
        If chk_dtpDateE.Checked = True Then
            dtpDateE.Enabled = True
        Else
            dtpDateE.Enabled = False
        End If
    End Sub

    Private Sub cboKind_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboKind.SelectedIndexChanged
        If cboKind.SelectedIndex = 2 Then  '種類保固顯示保固期限,履約金為保證品期限
            Label8.Text = "保固期限起"
            Label9.Text = "保固期限訖"
            chk_dtpDateS.Visible = False
            chk_dtpDateS.Checked = True
            dtpDateS.Enabled = True
            chk_dtpDateE.Visible = False
            chk_dtpDateE.Checked = True
            dtpDateE.Enabled = True
        Else                               '選履約品時
            Label8.Text = "保證品期限起"
            Label9.Text = "保證品期限訖"
            chk_dtpDateS.Visible = True
            chk_dtpDateS.Checked = False
            dtpDateS.Enabled = False
            chk_dtpDateE.Visible = True
            chk_dtpDateE.Checked = False
            dtpDateE.Enabled = False
        End If
    End Sub

    Private Sub DataGrid1_CurrentCellChanged1(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.CurrentCellChanged
        ' DataGrid1.Select(DataGrid1.CurrentCell.RowNumber)
        bm.Position = DataGrid1.CurrentCell.RowNumber
        Call RecMove1.IOPOS()
        check_dtpdate()
    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        clearForm()
    End Sub

    Private Sub DataGrid1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.DoubleClick
        TabControl1.SelectedIndex = 1
    End Sub



    Private Sub btnPrt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrt.Click
        Dim response As MsgBoxStyle
        btnPrt.Enabled = False
        response = (MsgBox("是否直接列印!", MsgBoxStyle.YesNoCancel, "查詢未退之保證金保管品"))
        If response = MsgBoxResult.Cancel Then
            btnPrt.Enabled = True
            Exit Sub
        Else
            Call printfile(response)
        End If
        btnPrt.Enabled = True
    End Sub
    Sub printfile(ByVal NowPrinting As MsgBoxStyle)
        Dim xlapp As Excel.Application
        Dim xlbook As Excel.Workbook
        Dim xlbooks As Excel.Workbooks
        Dim xlsheet As Excel.Worksheet
        Dim xlsheets As Excel.Sheets
        Dim xlRange As Excel.Range
        Dim xlRange1 As Excel.Range
        Dim xlRange2 As Excel.Range
        Dim xlCells As Excel.Range
        Dim tt1, tt2 As String
        Dim mypath As String
        Dim i, j As Integer
        Try
            mypath = Application.StartupPath
            Try
                tt1 = "c:\App\bail\ReportData\bailf040sample.xls" 'mypath + "\bailf060sample.xls"
                tt2 = "c:\App\bail\Report\bailf040.xls"           'mypath + "\bailf060.xls"
                FileCopy(tt1, tt2)
                xlapp = CreateObject("Excel.Application")
                xlapp.DisplayAlerts = False  '隱藏所有警告或確認訊息視窗,否則某些指令會失敗,譬如刪除工作表
                xlbooks = xlapp.Workbooks
                xlbook = xlbooks.Open(tt2)   '開啟tt2
            Catch
                MsgBox("報表產生錯誤，請洽程式設計人員!", , "保證金系統")
                Exit Sub
            End Try
            xlsheets = xlbook.Worksheets
            xlsheet = xlsheets(1)
            NAR(xlCells)
            xlCells = xlsheet.Cells

            xlRange = xlCells(1, 1)
            xlRange.Value = "查詢保管品"
            NAR(xlRange)
            For i = 0 To mydataset.Tables("bailf020").Rows.Count - 1
                'For j = 0 To 4
                xlRange = xlCells(i + 3, 1)
                xlRange.Value = i + 1
                NAR(xlRange)
                xlRange = xlCells(i + 3, 2)
                xlRange.Value = nz(mydataset.Tables("bailf020").Rows(i).Item("engno"), "")
                NAR(xlRange)
                xlRange = xlCells(i + 3, 3)
                xlRange.Value = nz(Trim(mydataset.Tables("bailf020").Rows(i).Item("engname")), "")
                NAR(xlRange)
                xlRange = xlCells(i + 3, 4)
                xlRange.Value = nz(mydataset.Tables("bailf020").Rows(i).Item("rpdate"), "")
                NAR(xlRange)





                xlRange = xlCells(i + 3, 5)
                If nz(mydataset.Tables("bailf020").Rows(i).Item("rp"), "") = "1" Then
                    xlRange.Value = "收"
                ElseIf nz(mydataset.Tables("bailf020").Rows(i).Item("rp"), "") = "2" Then
                    xlRange.Value = "支"
                End If

                NAR(xlRange)

                xlRange = xlCells(i + 3, 6)
                If nz(mydataset.Tables("bailf020").Rows(i).Item("kind"), "") = "1" Then
                    xlRange.Value = "履約品"
                ElseIf nz(mydataset.Tables("bailf020").Rows(i).Item("kind"), "") = "2" Then
                    xlRange.Value = "押標品"
                ElseIf nz(mydataset.Tables("bailf020").Rows(i).Item("kind"), "") = "3" Then
                    xlRange.Value = "保固品"
                ElseIf nz(mydataset.Tables("bailf020").Rows(i).Item("kind"), "") = "4" Then
                    xlRange.Value = "差額保證品"
                End If
                NAR(xlRange)




                xlRange = xlCells(i + 3, 7)
                xlRange.Value = nz(mydataset.Tables("bailf020").Rows(i).Item("date_s"), "")
                NAR(xlRange)
                xlRange = xlCells(i + 3, 8)
                xlRange.Value = nz(mydataset.Tables("bailf020").Rows(i).Item("cop"), "")
                NAR(xlRange)
                xlRange = xlCells(i + 3, 9)
                xlRange.Value = nz(mydataset.Tables("bailf020").Rows(i).Item("amt"), "")
                NAR(xlRange)
                xlRange = xlCells(i + 3, 10)
                xlRange.Value = nz(mydataset.Tables("bailf020").Rows(i).Item("chkno"), "")
                NAR(xlRange)

                'Next
            Next


            If NowPrinting = MsgBoxResult.Yes Then
                xlbook.PrintOut()
            Else
                MsgBox("檔案儲存路徑：" & "C:\App\bail\Report\bailf040.xls", , "保證金系統")
            End If
            xlbook.Save()
        Finally
            '釋放各物件所佔用的記憶體,要按照以下順序
            NAR(xlCells)
            NAR(xlRange)
            NAR(xlRange1)
            NAR(xlRange2)
            '如果有宣告 xlRange3 在這也要記得釋放記憶體
            NAR(xlsheet)
            NAR(xlsheets)
            If Not xlbook Is Nothing Then xlbook.Close(False)
            NAR(xlbook)
            NAR(xlbooks)
            If Not xlapp Is Nothing Then xlapp.Quit()
            NAR(xlapp)
            GC.Collect()
        End Try

    End Sub
End Class
