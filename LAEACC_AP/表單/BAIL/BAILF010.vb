Imports System.Globalization
Public Class BAILF010
    Dim confid, confunit As String, LoadAfter, Dirty As Boolean, cboFirst As Boolean

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
    Dim oldEngno As String, oldKind As String

    Private Sub frmBailf010_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim myds As DataSet
        LoadAfter = True
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
    End Sub
    Sub LoadGridFunc()
        'Dim sqlstr As String
        'sqlstr = "SELECT   *  FROM  psname where unit='" & TransPara.TransP("userunit") & "'"
        cboFirst = False
        'sqlstr = "SELECT * FROM bailf010"
        'mydataset =  openmember("", "bailf010", sqlstr)

        RecMove1.FilesPara = "mastconn=" & mastconn & "|sqlstr=" & sqlstr
        DataGrid1.DataSource = mydataset
        DataGrid1.DataMember = "bailf010"
        bm = Me.BindingContext(mydataset, "bailf010")
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
        txtBalance.DataBindings.Clear()

        txtId.DataBindings.Add(New Binding("Text", mydataset, "bailf010.autono"))
        txtEngno.DataBindings.Add(New Binding("Text", mydataset, "bailf010.engno"))
        lblEngname.DataBindings.Add(New Binding("Text", mydataset, "bailf010.engname"))
        cboKind.DataBindings.Add(New Binding("SelectedValue", mydataset, "bailf010.kind"))
        dtpDate.DataBindings.Add(New Binding("Text", mydataset, "bailf010.rpdate"))
        cboRp.DataBindings.Add(New Binding("SelectedValue", mydataset, "bailf010.Rp"))
        'txtRp.Text = cboRP.ValueMember.ToString
        'txtRp.DataBindings.Add(New Binding("Text", cboRP, "Value"))
        txtIdno.DataBindings.Add(New Binding("Text", mydataset, "bailf010.Idno"))
        txtCop.DataBindings.Add(New Binding("Text", mydataset, "bailf010.Cop"))
        txtAmt.DataBindings.Add(New Binding("Text", mydataset, "bailf010.Amt"))

        dtpDateS.DataBindings.Add(New Binding("Text", mydataset, "bailf010.date_s"))
        dtpDateE.DataBindings.Add(New Binding("Text", mydataset, "bailf010.date_e"))
        txtNo1No.DataBindings.Add(New Binding("Text", mydataset, "bailf010.no_1_no"))
        txtBalance.DataBindings.Add(New Binding("Text", mydataset, "bailf010.balance"))
        cboFirst = True



    End Sub
    Sub transjob(ByVal JobName As String, ByVal JobPara As String) Handles RecMove1.TransJob
        Dim LasPos As Integer
        Select Case JobName
            Case "記錄移動"
                oldEngno = bm.Current("engno") 'txtEngno.Text
                oldKind = bm.Current("kind")   'cboKind.SelectedValue
                Call check_dtpdate()
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
                sqlstr = "delete from bailf010 where autono='" & keyvalue & "'"
                retstr = runsql(mastconn, sqlstr)
                If retstr = "sqlok" Then
                    backuprtn("", Application.StartupPath, sqlstr)
                    'mydataset.Tables("bailf010").Rows.RemoveAt(JobPara)
                    mydataset.Tables("bailf010").Clear()
                    mydataset = openmember("", "bailf010", sqlstr1)
                    clearForm()
                    Call LoadGridFunc()
                    'clearForm()
                    bm.Position = LasPos
                    Call RecMove1.IOPOS()
                End If

            Case "回寫記錄cmd"
                Dim keyvalue, sqlstr, retstr, updstr As String
                If Not checkInput() Then Exit Sub
                If amtCheck(bm.Current("autono"), JobName) Then
                    MsgBox("保證金支出金額大於收入金額，請檢查輸入值!")
                    Exit Sub
                End If
                LasPos = bm.Position
                keyvalue = bm.Current("autono")
                '******************************

                'RecMove1.genupdsql("seq", bm.Current("seq"), "N")
                'RecMove1.GenUpdsql("id", bm.Current("id"), "N")
                RecMove1.GenUpdsql("engno", bm.Current("engno"), "T")
                RecMove1.GenUpdsql("kind", bm.Current("kind"), "T")
                RecMove1.GenUpdsql("rpdate", bm.Current("rpdate"), "D")
                RecMove1.GenUpdsql("rp", bm.Current("rp"), "T")
                RecMove1.GenUpdsql("idno", bm.Current("idno"), "T")
                RecMove1.GenUpdsql("cop", bm.Current("cop"), "TU")
                RecMove1.GenUpdsql("amt", bm.Current("amt"), "N")
                'If bm.Current("kind") = "3" Then   '只有保固金有保固期限
                '    RecMove1.GenUpdsql("date_s", bm.Current("date_s"), "D")
                '    RecMove1.GenUpdsql("date_e", bm.Current("date_e"), "D")
                'Else
                '    RecMove1.GenUpdsql("date_s", DBNull.Value, "D")
                '    RecMove1.GenUpdsql("date_e", DBNull.Value, "D")
                'End If
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

                sqlstr = "update bailf010 set " & RecMove1.genupdfunc & " where autono=" & keyvalue
                '********************



                retstr = runsql(mastconn, sqlstr)
                If retstr = "sqlok" Then
                    backuprtn("", Application.StartupPath, sqlstr)
                    'mydataset.Tables("bailf010").Rows.RemoveAt(JobPara)
                    mydataset.Tables("bailf010").Clear()
                    mydataset = openmember("", "bailf010", sqlstr1)
                    Call LoadGridFunc()  ' 94/2/15 mark 因重新load後不會停留在同一筆
                    bm.Position = LasPos
                    Call RecMove1.IOPOS()
                    MsgBox("更新成功")
                Else
                    MsgBox("儲存錯誤!")
                End If


                'sqlstr = "update psname set seq=@seq,psstr=@psstr where autono=@autono"
                'Dim updpara(10) As String
                'updpara(0) = "@seq," & bm.Current("seq") & ",Char"
                'updpara(1) = "@psstr," & bm.Current("psstr") & ",Char"
                'sqlstr = "update 縣市別 set 縣市別=@縣市別 where 縣市編號=@縣市編號"
                'retstr = updfunc(mastconn, sqlstr, updpara)
                'MsgBox("更新完成")
                'Exit Sub
            Case "新增記錄"
                Dim keyvalue, sqlstr, retstr, updstr As String
                If Not checkInput() Then Exit Sub
                If cboRp.SelectedValue = 2 Then
                    If amtCheck(bm.Current("autono"), JobName) Then
                        MsgBox("保證金支出金額大於收入金額，請檢查輸入值!")
                        Exit Sub
                    End If
                End If
                'RecMove1.GenInsSql("seq", bm.Current("seq"), "N")
                'RecMove1.GenInsSql("id", bm.Current("id"), "N")
                If bm Is Nothing OrElse bm.Position = -1 Then
                    LasPos = -1
                    sqlstr1 = "SELECT   BAILF010.ENGNO, ENF010.ENGNAME, BAILF010.KIND, BAILF010.RPDATE,BAILF010.RP, BAILF010.IDNO, BAILF010.COP, BAILF010.AMT, BAILF010.DATE_S, BAILF010.DATE_E, BAILF010.NO_1_NO,bailf010.autono,bailf010.balance FROM BAILF010 left JOIN ENF010 ON BAILF010.ENGNO = ENF010.ENGNO where enf010.engno='" & txtEngno.Text.Trim & "'" ' and (balance is null or balance='')"
                    RecMove1.GenInsSql("engno", txtEngno.Text.Trim, "T")
                    RecMove1.GenInsSql("kind", cboKind.SelectedValue, "T")
                    RecMove1.GenInsSql("rpdate", dtpDate.Value.Date, "D")
                    RecMove1.GenInsSql("rp", cboRp.SelectedValue, "T")
                    RecMove1.GenInsSql("idno", txtIdno.Text.Trim, "T")
                    RecMove1.GenInsSql("cop", txtCop.Text.Trim, "TU")
                    RecMove1.GenInsSql("amt", txtAmt.Text.Trim, "N")
                    If cboKind.SelectedValue = "3" Then   '只有保固金有保固期限
                        RecMove1.GenInsSql("date_s", dtpDateS.Value.Date, "D")
                        RecMove1.GenInsSql("date_e", dtpDateE.Value.Date, "D")
                    Else
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
                    sqlstr = "SELECT   BAILF010.ENGNO, ENF010.ENGNAME, BAILF010.KIND, BAILF010.RPDATE,BAILF010.RP, BAILF010.IDNO, BAILF010.COP, BAILF010.AMT, BAILF010.DATE_S, BAILF010.DATE_E, BAILF010.NO_1_NO,bailf010.autono,bailf010.balance FROM BAILF010 left JOIN ENF010 ON BAILF010.ENGNO = ENF010.ENGNO where enf010.engno='" & txtEngno.Text.Trim & "'" ' and (balance is null or balance='')"
                    RecMove1.GenInsSql("engno", bm.Current("engno"), "T")
                    RecMove1.GenInsSql("kind", bm.Current("kind"), "T")
                    RecMove1.GenInsSql("rpdate", bm.Current("rpdate"), "D")
                    RecMove1.GenInsSql("rp", bm.Current("rp"), "T")
                    RecMove1.GenInsSql("idno", bm.Current("idno"), "T")
                    RecMove1.GenInsSql("cop", bm.Current("cop"), "TU")
                    RecMove1.GenInsSql("amt", bm.Current("amt"), "N")
                    'If bm.Current("kind") = "3" Then   '只有保固金有保固期限
                    '    RecMove1.GenInsSql("date_s", bm.Current("date_s"), "D")
                    '    RecMove1.GenInsSql("date_e", bm.Current("date_e"), "D")
                    'End If
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
                sqlstr = "insert into bailf010 " & RecMove1.GenInsFunc
                ' sqlstr = "insert into psname (seq,psstr,unit) values(" & bm.Current("seq") & ",'" & bm.Current("psstr") & "','" & bm.Current("unit") & "')"
                retstr = runsql(mastconn, sqlstr)

                If retstr = "sqlok" Then
                    backuprtn("", Application.StartupPath, sqlstr)
                    'mydataset.Tables("bailf010").Rows.RemoveAt(JobPara)
                    If Not mydataset Is Nothing Then
                        mydataset.Tables("bailf010").Clear()
                    End If
                    mydataset = openmember("", "bailf010", sqlstr1)
                    Call LoadGridFunc()
                    bm.Position = LasPos
                    Call RecMove1.IOPOS()
                    MsgBox("新增成功")
                Else
                    MsgBox("新增失敗!")
                End If
        End Select
    End Sub
    Sub clearForm()
        txtId.Text = ""
        txtEngno.Text = ""
        cboKind.SelectedValue = "1"
        dtpDate.Text = Today.ToString
        cboRp.SelectedValue = "1"
        txtIdno.Text = ""
        txtCop.Text = ""
        lblamt.Text = ""
        txtAmt.Text = ""
        dtpDateS.Text = Today.ToString
        dtpDateE.Text = Today.ToString
        txtNo1No.Text = ""
        txtBalance.Text = ""

    End Sub
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






    'Private Sub txtId_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtId.TextChanged
    '    If bm.Position <> -1 Then
    '        If IsDBNull(bm.Current("date_s")) Then '因DatetimePicker繫結欄位時,如欄位值為DBnull時會顯示為今日日期，故改為不顯示
    '            dtpDateS.Visible = False
    '        Else
    '            dtpDateS.Visible = True
    '        End If

    '        If IsDBNull(bm.Current("date_e")) Then
    '            dtpDateE.Visible = False
    '        Else
    '            dtpDateE.Visible = True
    '        End If
    '    End If

    'End Sub



    Private Sub DataGrid1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles DataGrid1.MouseDown
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
        If currentJob = "回寫記錄cmd" Then   '檢查更修改前的工程編號及保證種類是否支出大於收入
            sqlstr = "SELECT engno,sum(amt) as amt  FROM bailf010 where rp='1' and kind='" & oldKind & "' and engno='" & oldEngno & "' and autono<>" & currentRec & " and (balance is null or balance='') group by engno"      '依工程編號找出保證金收的記錄
            mydataset = openmember("", "bailf010", sqlstr)
            If mydataset.Tables("bailf010").Rows.Count > 0 Then
                amt1 = mydataset.Tables("bailf010").Rows(0).Item("amt")
            End If
            mydataset.Clear()
            sqlstr = "SELECT engno,sum(amt) as amt  FROM bailf010 where rp='2' and kind='" & oldKind & "' and engno='" & oldEngno & "' and autono<>" & currentRec & " and (balance is null or balance='') group by engno" '依工程編號找出保證金支的記錄
            mydataset = openmember("", "bailf010", sqlstr)
            If mydataset.Tables("bailf010").Rows.Count > 0 Then
                amt2 = mydataset.Tables("bailf010").Rows(0).Item("amt")
            End If


            'If amt2 > amt1 Then    97/5/12 remark
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
            sqlstr = "SELECT engno,sum(amt) as amt  FROM bailf010 where rp='1' and kind='" & kind & "' and engno='" & engno & "' and (balance is null or balance='') group by engno"      '依工程編號找出保證金收的記錄
        Else
            sqlstr = "SELECT engno,sum(amt) as amt  FROM bailf010 where rp='1' and kind='" & kind & "' and engno='" & engno & "' and autono<>" & currentRec & " and (balance is null or balance='') group by engno"      '依工程編號找出保證金收的記錄
        End If
        mydataset = openmember("", "bailf010", sqlstr)
        If mydataset.Tables("bailf010").Rows.Count > 0 Then
            amt1 = mydataset.Tables("bailf010").Rows(0).Item("amt")
        End If
        mydataset.Clear()
        If currentJob = "新增記錄" Then
            sqlstr = "SELECT engno,sum(amt) as amt  FROM bailf010 where rp='2' and kind='" & kind & "' and engno='" & engno & "' and (balance is null or balance='') group by engno" '依工程編號找出保證金支的記錄
        Else
            sqlstr = "SELECT engno,sum(amt) as amt  FROM bailf010 where rp='2' and kind='" & kind & "' and engno='" & engno & "' and autono<>" & currentRec & " and (balance is null or balance='') group by engno" '依工程編號找出保證金支的記錄
        End If
        mydataset = openmember("", "bailf010", sqlstr)
        If mydataset.Tables("bailf010").Rows.Count > 0 Then
            amt2 = mydataset.Tables("bailf010").Rows(0).Item("amt")
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
        Else
            checkInput = True
        End If
    End Function

    Private Sub cboKind_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboKind.SelectedIndexChanged
        'If cboKind.SelectedIndex = 2 Then   '保固金
        '    dtpDateS.Visible = True
        '    dtpDateE.Visible = True
        'Else
        '    dtpDateS.Visible = False
        '    dtpDateE.Visible = False
        'End If
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
            Label8.Text = "保證金期限起"
            Label9.Text = "保證金期限訖"
            chk_dtpDateS.Visible = True
            chk_dtpDateS.Checked = False
            dtpDateS.Enabled = False
            chk_dtpDateE.Visible = True
            chk_dtpDateE.Checked = False
            dtpDateE.Enabled = False
        End If
    End Sub

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
            txtSEngno = txtEngnoStart.Text.Trim  '開始工程編號
            txtEEngno = txtEngnoStop.Text.Trim   '結束工程編號
            If txtEEngno < txtSEngno Then
                txtTempEngno = txtSEngno
                txtSEngno = txtEEngno
                txtEEngno = txtTempEngno
            End If
            If txtEngnoStart.Text.Trim <> "" And txtEngnoStop.Text.Trim <> "" Then
                sqlstr = "SELECT BAILF010.ENGNO, ENF010.ENGNAME, BAILF010.KIND, BAILF010.RP, BAILF010.IDNO, BAILF010.COP, BAILF010.AMT, BAILF010.NO_1_NO,bailf010.autono,bailf010.balance"
                sqlstr &= " ,RPDATE"
                sqlstr &= " ,DATE_S"
                sqlstr &= " ,DATE_E"
                sqlstr &= " FROM BAILF010"
                sqlstr &= " left JOIN ENF010 ON BAILF010.ENGNO = ENF010.ENGNO"
                sqlstr &= " where enf010.engno>='" & txtSEngno & "' and enf010.engno<='" & txtEEngno & "' and (balance is null or balance='')"
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
            sqlstr = "SELECT BAILF010.ENGNO, ENF010.ENGNAME, BAILF010.KIND, BAILF010.RP, BAILF010.IDNO, BAILF010.COP, BAILF010.AMT, BAILF010.NO_1_NO,bailf010.autono,bailf010.balance"
            sqlstr &= " ,RPDATE"
            sqlstr &= " ,DATE_S"
            sqlstr &= " ,DATE_E"
            sqlstr &= " FROM BAILF010"
            sqlstr &= " left JOIN ENF010 ON BAILF010.ENGNO = ENF010.ENGNO"
            sqlstr &= " where bailf010.rpdate>='" & dateSRpdate.ToString("yyyy/M/d", CultureInfo.InvariantCulture) & "' and bailf010.rpdate<='" & dateERpdate.ToString("yyyy/M/d", CultureInfo.InvariantCulture) & "' and (balance is null or balance='')"

            'End If
        End If

        If chkKind.Checked Then
            GenWheresql("bailf010.kind", cboInqKind.SelectedValue, "T")
            countchk += 1
        End If

        If chkRp.Checked Then
            GenWheresql("bailf010.rp", cboInqRp.SelectedValue, "T")
            countchk += 1
        End If

        If chkIdno.Checked Then
            GenWheresql("bailf010.idno", txtInqIdno.Text, "T")
            countchk += 1
        End If

        If chkAmt.Checked Then
            GenWheresql("bailf010.amt", txtInqAmt.Text, "N")
            countchk += 1
        End If

        If chkCop.Checked Then
            'GenWheresql("bailf010.cop", txtInqCop.Text, "T")
            countchk += 1
        End If
        If countchk > 0 Then
            If (Not txtInqIdno.Text.Trim.Length > 0 And chkIdno.Checked Or Not IsNumeric(txtInqAmt.Text) And Val(txtInqAmt.Text) >= 0 And chkAmt.Checked Or Not txtInqCop.Text.Trim.Length > 0 And chkCop.Checked) Then
                MsgBox("請檢查輸入值!")
                Exit Sub
            Else
                cboFirst = False
                If chkCop.Checked And countchk = 1 Then
                    sqlstr &= "and bailf010.cop like '%" & txtInqCop.Text.Trim & "%'"
                ElseIf chkCop.Checked Then
                    sqlstr &= " and bailf010.cop like '%" & txtInqCop.Text.Trim & "%'"
                    sqlstr &= " and " & genwherefunc
                Else
                    sqlstr &= " and " & genwherefunc
                End If
            End If
        End If

        If rdbSort1.Checked Then
            sqlstr &= " order by bailf010.engno,bailf010.kind,bailf010.rpdate,bailf010.autono"
        ElseIf rdbSort2.Checked Then
            sqlstr &= " order by bailf010.idno,bailf010.engno,bailf010.kind,bailf010.rpdate,bailf010.autono"
        ElseIf rdbSort3.Checked Then
            sqlstr &= " order by bailf010.kind,bailf010.engno,bailf010.rpdate,bailf010.autono"
        ElseIf rdbSort4.Checked Then
            sqlstr &= " order by bailf010.rpdate,bailf010.engno,bailf010.kind,bailf010.autono"
        End If

        sqlstr1 = sqlstr
        mydataset = openmember("", "bailf010", sqlstr)

        Call LoadGridFunc()
        If mydataset.Tables("bailf010").Rows.Count = 0 Then
            MsgBox("查詢的資料不存在!")
            clearForm()
        End If
        TabControl1.SelectedIndex = 0
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

    Private Sub chkCop_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkCop.CheckedChanged
        If Not txtInqCop.Text.Trim.Length > 0 And chkCop.Checked Then
            ErrorProvider1.SetError(Me.txtInqCop, "請輸入廠商名稱!")
        Else
            ErrorProvider1.SetError(Me.txtInqCop, "")
        End If
    End Sub

    Private Sub txtInqCop_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtInqCop.Validated
        If Not txtInqCop.Text.Trim.Length > 0 And chkCop.Checked Then
            ErrorProvider1.SetError(Me.txtInqCop, "請輸入廠商名稱!")
        Else
            ErrorProvider1.SetError(Me.txtInqCop, "")
        End If
    End Sub


    Private Sub rdbEngnoInq_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbEngnoInq.CheckedChanged
        rdbSort1.Checked = True
    End Sub

    Private Sub rdbRpdateInq_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbRpdateInq.CheckedChanged
        rdbSort4.Checked = True
    End Sub

    Private Sub DataGrid1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.DoubleClick
        TabControl1.SelectedIndex = 1
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
    Private Sub DataGrid1_CurrentCellChanged1(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.CurrentCellChanged
        ' DataGrid1.Select(DataGrid1.CurrentCell.RowNumber)
        bm.Position = DataGrid1.CurrentCell.RowNumber
        Call RecMove1.IOPOS()
        check_dtpdate()
    End Sub
End Class
