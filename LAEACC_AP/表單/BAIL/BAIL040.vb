Public Class BAIL040
    Dim LoadAfter, Dirty As Boolean
    Private disabledBackBrush As Brush
    Private disabledTextBrush As Brush
    Private currentRowFont As Font
    Private currentRowBackBrush As Brush

    Dim sqlstr As String
    Dim mydataset As DataSet

    Private Sub frmBail040_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim myds As DataSet
        LoadAfter = True
        TabControl1.Enabled = False

        dtpDate.Value = TransPara.TransP("UserDATE")

    End Sub

    Private Sub btnInq_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInq.Click
        Dim intI As Integer
        Dim mydatasetp As DataSet
        Dim datarowp() As DataRow
        StatusBar1.Text = ""
        'If txtInqEngno.Text.Trim.Length <> 7 Or Not IsNumeric(txtInqEngno.Text) Then
        '    MsgBox("工程編號輸入錯誤，請重新輸入!")
        '    Exit Sub
        'End If
        txtEngno.Text = txtInqEngno.Text
        sqlstr = "SELECT engname FROM enf010 where engno='" & Trim(txtInqEngno.Text) & "'"
        mydataset = openmember("", "enf010", sqlstr)
        If (mydataset.Tables("enf010").Rows.Count > 0) Then
            lblEngname.Text = mydataset.Tables("enf010").Rows(0).Item("engname")
        Else
            Call clearForm()
            MsgBox("工程編號不存在!請重新輸入!")
            Exit Sub
        End If
        mydataset.Clear()
        TabControl1.Enabled = True
        sqlstr = "SELECT * FROM bailf020 where rp='1' and engno='" & Trim(txtInqEngno.Text) & "' and (balance is null or balance='') order by kind,rpdate desc" '依工程編號找出保管品收的記錄
        mydataset = openmember("", "bailf020", sqlstr)

        sqlstr = "SELECT * FROM bailf020 where rp='2' and engno='" & Trim(txtInqEngno.Text) & "' and (balance is null or balance='') order by kind,rpdate desc" '依工程編號找出保管品支的記錄
        mydatasetp = openmember("", "bailf020", sqlstr)

        'dtgSetup()  '設定datagrid

        Dim c As DataColumn = New DataColumn   '種類
        c.DataType = System.Type.GetType("System.String")
        c.ColumnName = "ckind"
        c.DefaultValue = ""
        mydataset.Tables("bailf020").Columns.Add(c)

        Dim c1 As DataColumn = New DataColumn '退還日期
        c1.DataType = System.Type.GetType("System.String")
        c1.ColumnName = "pdate"
        c1.DefaultValue = ""
        mydataset.Tables("bailf020").Columns.Add(c1)

        Dim c2 As DataColumn = New DataColumn '退還
        c2.DataType = System.Type.GetType("System.Boolean")
        c2.ColumnName = "refund"
        c2.DefaultValue = "false"
        mydataset.Tables("bailf020").Columns.Add(c2)


        For intI = 0 To (mydataset.Tables("bailf020").Rows.Count - 1)
            Select Case mydataset.Tables("bailf020").Rows(intI).Item("kind")
                Case Is = "1"
                    mydataset.Tables("BAILF020").Rows(intI).Item("ckind") = "履約"
                Case Is = "2"
                    mydataset.Tables("BAILF020").Rows(intI).Item("ckind") = "押標"
                Case Is = "3"
                    mydataset.Tables("BAILF020").Rows(intI).Item("ckind") = "保固"
                Case Is = "4"
                    mydataset.Tables("BAILF020").Rows(intI).Item("ckind") = "差額"
            End Select

            datarowp = mydatasetp.Tables("Bailf020").Select("kind='" & mydataset.Tables("BAILF020").Rows(intI).Item("kind") & "' and chkno='" & mydataset.Tables("BAILF020").Rows(intI).Item("chkno") & "' and amt=" & mydataset.Tables("BAILF020").Rows(intI).Item("amt"))  '依種類、支票號碼及金額判斷是否退還
            If datarowp.Length > 0 Then
                mydataset.Tables("bailf020").Rows(intI).Item("pdate") = Format(datarowp(0).Item("rpdate"), "yyy/MM/dd")
                mydataset.Tables("bailf020").Rows(intI).Item("refund") = 1
            End If
        Next
        mydatasetp = Nothing
        datarowp = Nothing
        Dim myDataView As DataView = New DataView(mydataset.Tables("bailf020"))
        myDataView.Sort = "pdate asc"    '依退還日期排序
        myDataView.AllowNew = False
        'dtg1.DataSource = mydataset
        'dtg1.DataMember = "bailf020"
        dtg1.DataSource = myDataView
        enableobj(True)

    End Sub
    Private Sub enableobj(ByVal en As Boolean)
        Dim ThisControl As System.Windows.Forms.Control
        For Each ThisControl In Me.TabPage1.Controls
            ThisControl.Enabled = en
        Next ThisControl
    End Sub



    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Dim Rw As DataRow
        Dim retstr, sqlstr As String
        Dim retCount As Integer
        retCount = 0
        For Each Rw In mydataset.Tables("bailf020").Rows
            If Rw("refund").ToString() = "True" And Rw("pdate").ToString() = "" Then  '退還為已鉤選且日期為空字串為新退還資料
                GenInsSql("engno", Rw("engno"), "T")
                GenInsSql("kind", Rw("kind"), "T")
                GenInsSql("rpdate", dtpDate.Text, "D")
                GenInsSql("rp", "2", "T")
                If Not IsDBNull(Rw("idno")) Then
                    GenInsSql("idno", Rw("idno"), "T")
                End If
                If Not IsDBNull(Rw("cop")) Then
                    GenInsSql("cop", Rw("cop"), "T")
                End If
                If Not IsDBNull(Rw("chkno")) Then
                    GenInsSql("chkno", Rw("chkno"), "T")
                End If
                If Not IsDBNull(Rw("bank")) Then
                    GenInsSql("bank", Rw("bank"), "T")
                End If
                GenInsSql("amt", Rw("amt"), "N")
                If Not IsDBNull(Rw("date_s")) Then
                    GenInsSql("date_s", Rw("date_s"), "D")
                End If
                If Not IsDBNull(Rw("date_e")) Then
                    GenInsSql("date_e", Rw("date_e"), "D")
                End If
                sqlstr = "insert into bailf020 " & GenInsFunc
                retstr = runsql(mastconn, sqlstr)
                If retstr = "sqlok" Then
                    'MsgBox("退還成功")
                    backuprtn("", Application.StartupPath, sqlstr)
                    StatusBar1.Text = "退還成功!"
                    retCount += 1
                Else
                    MsgBox("資料退還失敗!")
                End If
            End If
        Next
        StatusBar1.Text = "退還成功!  退還筆數：" & retCount & "筆"
        TabControl1.Enabled = False
        txtInqEngno.Focus()
        'OrderDetail.Refresh()
    End Sub

    'Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
    '    TabControl1.Enabled = False
    '    txtInqEngno.Focus()
    'End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Call clearForm()
    End Sub
    Sub clearForm()
        TabControl1.Enabled = False
        dtpDate.Value = TransPara.TransP("UserDATE")
        txtEngno.Text = ""
        lblEngname.Text = ""
        txtInqEngno.Focus()
        mydataset.Clear()
    End Sub


    Private Sub dtg1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtg1.Click
        Dim selectedCell As System.Windows.Forms.DataGridCell
        selectedCell = dtg1.CurrentCell
        Dim selectedItem As Object
        If selectedCell.ColumnNumber = 0 Then
            Try
                If dtg1.Item(selectedCell.RowNumber, 8) <> "" Then
                    dtg1.Item(selectedCell.RowNumber, selectedCell.ColumnNumber) = True
                End If
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub dtg1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtg1.DoubleClick
        Dim selectedCell As System.Windows.Forms.DataGridCell
        selectedCell = dtg1.CurrentCell
        Dim selectedItem As Object
        If selectedCell.ColumnNumber = 0 Then
            Try
                If dtg1.Item(selectedCell.RowNumber, 8) <> "" Then
                    dtg1.Item(selectedCell.RowNumber, selectedCell.ColumnNumber) = True
                End If
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub btnEnd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnd.Click
        Me.Close()
    End Sub
End Class
