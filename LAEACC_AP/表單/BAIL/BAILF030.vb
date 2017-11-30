Imports System.Globalization
Public Class BAILF030
    Dim LoadAfter, Dirty As Boolean, cboFirst As Boolean
    Dim sqlstr, sqlstr1 As String
    Dim bm As BindingManagerBase, mydataset As DataSet
    Dim UserId, UserName, UserUnit As String

    Private Sub frmBailf030_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim myds As DataSet
        LoadAfter = True
        UserName = TransPara.TransP("username")
        UserUnit = TransPara.TransP("userunit")
        UserId = TransPara.TransP("userid")
        dtpRpdateStart.Value = TransPara.TransP("UserDATE")
        dtpRpdateStop.Value = TransPara.TransP("UserDATE")
        Dirty = False
        StatusBar1.Text = ""

    End Sub
    Sub LoadGridFunc()
        RecMove1.FilesPara = "mastconn=" & mastconn & "|sqlstr=" & sqlstr
        DataGrid1.DataSource = mydataset
        DataGrid1.DataMember = "enf010"
        bm = Me.BindingContext(mydataset, "enf010")
        RecMove1.Setds = bm
        txtId.DataBindings.Clear()
        txtEngno.DataBindings.Clear()
        txtEngname.DataBindings.Clear()
        txtEyear.DataBindings.Clear()
        txtIdno.DataBindings.Clear()
        txtCop.DataBindings.Clear()
        dtpOpenDate.DataBindings.Clear()
        dtpTest2Date.DataBindings.Clear()
        dtpKeepDate.DataBindings.Clear()
        txtId.DataBindings.Add(New Binding("Text", mydataset, "enf010.autono"))
        txtEngno.DataBindings.Add(New Binding("Text", mydataset, "enf010.engno"))
        txtEngname.DataBindings.Add(New Binding("Text", mydataset, "enf010.engname"))
        txtEyear.DataBindings.Add(New Binding("Text", mydataset, "enf010.eyear"))
        txtIdno.DataBindings.Add(New Binding("Text", mydataset, "enf010.Idno"))
        txtCop.DataBindings.Add(New Binding("Text", mydataset, "enf010.Cop"))
        dtpOpenDate.DataBindings.Add(New Binding("Text", mydataset, "enf010.open_date"))
        dtpTest2Date.DataBindings.Add(New Binding("Text", mydataset, "enf010.test2_date"))
        dtpKeepDate.DataBindings.Add(New Binding("Text", mydataset, "enf010.keep_date"))
    End Sub
    Sub clearForm()
        txtId.Text = ""
        txtEngno.Text = ""
        txtIdno.Text = ""
        txtCop.Text = ""
        txtEyear.Text = ""
        txtEngname.Text = ""
        dtpOpenDate.Text = Today.ToString
        dtpTest2Date.Text = Today.ToString
        dtpKeepDate.Text = Today.ToString
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
            If txtEngnoStop.Text.Trim = "" Then txtEngnoStop.Text = txtEngnoStart.Text
            txtSEngno = txtEngnoStart.Text.Trim  '開始日期
            txtEEngno = txtEngnoStop.Text.Trim   '結束日期
            If txtEEngno < txtSEngno Then
                txtTempEngno = txtSEngno
                txtSEngno = txtEEngno
                txtEEngno = txtTempEngno
            End If
            If txtEngnoStart.Text.Trim <> "" And txtEngnoStop.Text.Trim <> "" Then
                sqlstr = "SELECT ENGNO, ENGNAME, EYEAR, IDNO, COP, OPEN_DATE, TEST2_DATE,KEEP_DATE, AUTONO FROM ENF010 WHERE ENGNO>='" & txtSEngno & "' AND ENGNO<='" & txtEEngno & "' order by engno"

            Else
                MsgBox("請檢查輸入值!")
                Exit Sub
            End If
        ElseIf rdbRpdateInq.Checked Then
            dateSRpdate = dtpRpdateStart.Value  '開如日期
            dateERpdate = dtpRpdateStop.Value   '結束日期
            If dateERpdate < dateSRpdate Then
                dateTemp = dateSRpdate
                dateSRpdate = dateERpdate
                dateERpdate = dateTemp
            End If
            sqlstr = "SELECT ENGNO, ENGNAME, EYEAR, IDNO, COP, OPEN_DATE, TEST2_DATE,KEEP_DATE, AUTONO FROM ENF010 where open_date>='" & dateSRpdate.ToString("yyyy/M/d", CultureInfo.InvariantCulture) & "' and open_date<='" & dateERpdate.ToString("yyyy/M/d", CultureInfo.InvariantCulture) & "' order by open_date"
        ElseIf rdbCopInq.Checked Then
            sqlstr = "SELECT ENGNO, ENGNAME, EYEAR, IDNO, COP, OPEN_DATE, TEST2_DATE,KEEP_DATE, AUTONO FROM ENF010 WHERE COP like '%" & txtCopInq.Text.Trim & "%' order by engno"
        ElseIf rdbEngNameInq.Checked Then
            sqlstr = "SELECT ENGNO, ENGNAME, EYEAR, IDNO, COP, OPEN_DATE, TEST2_DATE,KEEP_DATE, AUTONO FROM ENF010 WHERE ENGNAME like '%" & txtEngNameInq.Text.Trim & "%' order by engno"
        End If
        sqlstr1 = sqlstr
        mydataset = openmember("", "enf010", sqlstr)

        Call LoadGridFunc()
        If mydataset.Tables("enf010").Rows.Count = 0 Then
            MsgBox("查詢的資料不存在!")
            clearForm()
        End If
        TabControl1.SelectedIndex = 0
    End Sub

    Function checkInput() As Boolean  '表單輸入資料檢查
        If txtEngno.Text.Trim = "" Then 'Or txtEngno.Text.Trim.Length <> 7 Then
            MsgBox("工程編號輸入錯誤!")
            checkInput = False
        ElseIf txtCop.Text.Trim = "" Or txtCop.Text.Trim.Length > 25 Then
            MsgBox("商號名稱輸入錯誤!")
            checkInput = False
        ElseIf txtEngname.Text.Trim = "" Or txtEngname.Text.Trim.Length > 40 Then
            MsgBox("工程名稱輸入錯誤!")
            checkInput = False
        ElseIf Not IsNumeric(txtEyear.Text) Or Val(txtEyear.Text.Trim) <= 0 Then
            MsgBox("年度輸入錯誤!")
            checkInput = False
        Else
            checkInput = True
        End If
    End Function
    Sub check_dtpdate()
        If bm.Position <> -1 Then
            If IsDBNull(bm.Current("open_date")) Then '因DatetimePicker繫結欄位時,如欄位值為DBnull時會顯示為今日日期，故改為不顯示
                chkOpenDate.Checked = False
                dtpOpenDate.Enabled = False
            Else
                chkOpenDate.Checked = True
                dtpOpenDate.Enabled = True
            End If

            If IsDBNull(bm.Current("test2_date")) Then
                chkTest2Date.Checked = False
                dtpTest2Date.Enabled = False
            Else
                chkTest2Date.Checked = True
                dtpTest2Date.Enabled = True
            End If

            If IsDBNull(bm.Current("keep_date")) Then '因DatetimePicker繫結欄位時,如欄位值為DBnull時會顯示為今日日期，故改為不顯示
                chkKeepDate.Checked = False
                dtpKeepDate.Enabled = False
            Else
                chkKeepDate.Checked = True
                dtpKeepDate.Enabled = True
            End If
        End If
    End Sub
    Sub transjob(ByVal JobName As String, ByVal JobPara As String) Handles RecMove1.TransJob
        Dim LasPos As Integer
        Select Case JobName
            Case "記錄移動"
                ' oldEngno = bm.Current("engno") 'txtEngno.Text
                ' oldKind = bm.Current("kind")   'cboKind.SelectedValue
                check_dtpdate()
            Case "刪除記錄"
                Dim keyvalue, sqlstr, retstr As String
                keyvalue = bm.Current("autono")
                LasPos = bm.Position
                sqlstr = "delete from enf010 where autono='" & keyvalue & "'"
                retstr = runsql(mastconn, sqlstr)
                If retstr = "sqlok" Then
                    backuprtn("", Application.StartupPath, sqlstr)
                    'mydataset.Tables("bailf020").Rows.RemoveAt(JobPara)
                    mydataset.Tables("enf010").Clear()
                    mydataset = openmember("", "enf010", sqlstr1)
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
                keyvalue = bm.Current("autono")
                '******************************
                LasPos = bm.Position
                'RecMove1.genupdsql("seq", bm.Current("seq"), "N")
                'RecMove1.GenUpdsql("id", bm.Current("id"), "N")
                RecMove1.GenUpdsql("engno", bm.Current("engno"), "T")
                RecMove1.GenUpdsql("engname", bm.Current("engname"), "TU")
                RecMove1.GenUpdsql("eyear", bm.Current("eyear"), "N")
                RecMove1.GenUpdsql("idno", bm.Current("idno"), "T")
                RecMove1.GenUpdsql("cop", bm.Current("cop"), "TU")
                If chkOpenDate.Checked Then
                    RecMove1.GenUpdsql("open_date", bm.Current("open_date"), "D")
                Else
                    RecMove1.GenUpdsql("open_date", DBNull.Value, "D")
                End If
                If chkTest2Date.Checked Then
                    RecMove1.GenUpdsql("test2_date", bm.Current("test2_date"), "D")
                Else
                    RecMove1.GenUpdsql("test2_date", DBNull.Value, "D")
                End If
                If chkKeepDate.Checked Then
                    RecMove1.GenUpdsql("keep_date", bm.Current("keep_date"), "D")
                Else
                    RecMove1.GenUpdsql("keep_date", DBNull.Value, "D")
                End If


                sqlstr = "update enf010 set " & RecMove1.genupdfunc & " where autono=" & keyvalue
                '********************



                retstr = runsql(mastconn, sqlstr)
                If retstr = "sqlok" Then
                    backuprtn("", Application.StartupPath, sqlstr)
                    'mydataset.Tables("bailf020").Rows.RemoveAt(JobPara)
                    mydataset.Tables("enf010").Clear()
                    mydataset = openmember("", "enf010", sqlstr1)
                    Call LoadGridFunc() ' 94/2/15 mark 因重新load後不會停留在同一筆
                    bm.Position = LasPos
                    Call RecMove1.IOPOS()
                    MsgBox("更新成功")
                End If


            Case "新增記錄"
                Dim keyvalue, sqlstr, retstr, updstr As String
                If Not checkInput() Then Exit Sub
                'RecMove1.GenInsSql("seq", bm.Current("seq"), "N")
                'RecMove1.GenInsSql("id", bm.Current("id"), "N")
                If bm Is Nothing OrElse bm.Position = -1 Then
                    LasPos = -1
                    sqlstr1 = "SELECT ENGNO, ENGNAME, EYEAR, IDNO, COP, OPEN_DATE, TEST2_DATE,KEEP_DATE, AUTONO FROM ENF010 WHERE ENGNO='" & txtEngno.Text.Trim & "'"
                    RecMove1.GenInsSql("engno", txtEngno.Text, "T")
                    RecMove1.GenInsSql("engname", txtEngname.Text, "T")
                    RecMove1.GenInsSql("eyear", txtEyear.Text, "N")
                    RecMove1.GenInsSql("idno", txtIdno.Text, "T")
                    RecMove1.GenInsSql("cop", txtCop.Text, "T")
                    If chkOpenDate.Checked Then
                        RecMove1.GenInsSql("open_date", dtpOpenDate.Text, "D")
                    Else
                        RecMove1.GenInsSql("open_date", DBNull.Value, "D")
                    End If
                    If chkTest2Date.Checked Then
                        RecMove1.GenInsSql("test2_date", dtpTest2Date.Text, "D")
                    Else
                        RecMove1.GenInsSql("test2_date", DBNull.Value, "D")
                    End If
                    If chkKeepDate.Checked Then
                        RecMove1.GenInsSql("keep_date", dtpKeepDate.Text, "D")
                    Else
                        RecMove1.GenInsSql("keep_date", DBNull.Value, "D")
                    End If
                Else
                    LasPos = bm.Position
                    sqlstr1 = "SELECT ENGNO, ENGNAME, EYEAR, IDNO, COP, OPEN_DATE, TEST2_DATE,KEEP_DATE, AUTONO FROM ENF010 WHERE ENGNO='" & txtEngno.Text.Trim & "'"
                    RecMove1.GenInsSql("engno", bm.Current("engno"), "T")
                    RecMove1.GenInsSql("engname", bm.Current("engname"), "TU")
                    RecMove1.GenInsSql("eyear", bm.Current("eyear"), "N")
                    RecMove1.GenInsSql("idno", bm.Current("idno"), "T")
                    RecMove1.GenInsSql("cop", bm.Current("cop"), "TU")
                    If chkOpenDate.Checked Then
                        RecMove1.GenInsSql("open_date", bm.Current("open_date"), "D")
                    Else
                        RecMove1.GenInsSql("open_date", DBNull.Value, "D")
                    End If
                    If chkTest2Date.Checked Then
                        RecMove1.GenInsSql("test2_date", bm.Current("test2_date"), "D")
                    Else
                        RecMove1.GenInsSql("test2_date", DBNull.Value, "D")
                    End If
                    If chkKeepDate.Checked Then
                        RecMove1.GenInsSql("keep_date", bm.Current("keep_date"), "D")
                    Else
                        RecMove1.GenInsSql("keep_date", DBNull.Value, "D")
                    End If
                End If
                sqlstr = "insert into enf010 " & RecMove1.GenInsFunc
                ' sqlstr = "insert into psname (seq,psstr,unit) values(" & bm.Current("seq") & ",'" & bm.Current("psstr") & "','" & bm.Current("unit") & "')"
                retstr = runsql(mastconn, sqlstr)

                If retstr = "sqlok" Then
                    backuprtn("", Application.StartupPath, sqlstr)
                    'mydataset.Tables("bailf020").Rows.RemoveAt(JobPara)
                    If Not mydataset Is Nothing Then
                        mydataset.Tables("enf010").Clear()
                    End If
                    mydataset = openmember("", "enf010", sqlstr1)
                    Call LoadGridFunc()
                    bm.Position = LasPos
                    Call RecMove1.IOPOS()
                    MsgBox("新增成功")
                Else
                    MsgBox("新增失敗!")
                End If
        End Select
    End Sub

    Private Sub chkOpenDate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOpenDate.CheckedChanged
        If chkOpenDate.Checked = True Then
            dtpOpenDate.Enabled = True
        Else
            dtpOpenDate.Enabled = False
        End If
    End Sub

    Private Sub chkTest2Date_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTest2Date.CheckedChanged
        If chkTest2Date.Checked = True Then
            dtpTest2Date.Enabled = True
        Else
            dtpTest2Date.Enabled = False
        End If
    End Sub

    Private Sub chkKeepDate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkKeepDate.CheckedChanged
        If chkKeepDate.Checked = True Then
            dtpKeepDate.Enabled = True
        Else
            dtpKeepDate.Enabled = False
        End If
    End Sub

    Private Sub btnEnd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnd.Click
        Me.Close()
    End Sub

    Private Sub DataGrid1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.DoubleClick
        TabControl1.SelectedIndex = 1
    End Sub

End Class
