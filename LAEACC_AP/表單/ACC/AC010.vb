Imports JBC.Printing
Imports System.TextSystem.Object
Public Class AC010
    Dim sYear, sNo, OldNo As Integer    '年度 & 制製票號
    Dim sFile, sKind As String    '資料來源檔  & 傳票種類
    Dim mydsS, mydsT As DataSet
    Dim LoadAfter, Dirty As Boolean
    Dim sqlstr As String
    Dim bmS, bmT As BindingManagerBase, strAccno4 As String
    Dim myDatasetS, myDatasetT, mydataset, tempdataset, psDataset, accnoDataset As DataSet
    Dim ac010kind As String
    Dim strObject As String
    Dim DNS_ACC As String = INI_Read("CONFIG", "SET", "DNS_ACC")
    Dim mastconn As String = INI_Read("CONFIG", "SET", "DNS_ACC")
    Private Sub AC010_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'dtpDate.Format = DateTimePickerFormat.Custom
        'dtpDate.CustomFormat = String.Format("{0}/MM/dd", dtpDate.Value.AddYears(-1911).Year.ToString("00"))



        ac010kind = TransPara.TransP("ac010kind")  '定開立傳票 or 修改傳票
        If ac010kind = "開立傳票" Then
            gbxModify.Visible = False
            gbxCreate.Visible = True
        Else
            gbxModify.Visible = True
            gbxCreate.Visible = False
            txtOldNo.Focus()
        End If
        If TransPara.TransP("UnitTitle").indexof("彰化") >= 0 Then btnIntCopy.Visible = False '彰化傳票印一份botton不顯示

        LoadAfter = True
        TabControl1.Visible = False
        dtpDate.Value = TransPara.TransP("UserDate")
        'dtpDate.Value = Now
        'If Month(Now) = 1 And Microsoft.VisualBasic.DateAndTime.Day(Now) < 10 Then       'for year beginning, let's the date=yy/12/31  
        '    dtpDate.Value = CStr(Year(Now) - 1) + "/12/31"
        'End If
        '將所有銀行置combobox   
        Dim sqlstr As String
        Dim i As Integer
        sqlstr = "SELECT bank,bank + bankname + str(balance+day_income-day_pay-unpay,14) as cbank FROM chf020"
        mydataset = openmember(DNS_ACC, "chf020", sqlstr)
        If mydataset.Tables("chf020").Rows.Count = 0 Then
            cboBank.Text = "尚無銀行代號"
        Else
            cboBank.DataSource = mydataset.Tables("chf020")
            cboBank.DisplayMember = "cbank"  '顯示欄位
            cboBank.ValueMember = "bank"     '欄位值
        End If
        '將單位片語置combobox   
        sqlstr = "SELECT psstr  FROM psname where left(unit,3)='050' order by psstr"
        psDataset = openmember(DNS_ACC, "psname", sqlstr)
        If psDataset.Tables("psname").Rows.Count = 0 Then
            cboRemark.Text = "尚無片語"
        Else
            cboRemark.DataSource = psDataset.Tables("psname")
            cboRemark.DisplayMember = "psstr"  '顯示欄位
            cboRemark.ValueMember = "psstr"     '欄位值
            cboRemark.SelectionLength = 60
        End If
        '將科目置combobox
        sqlstr = "SELECT accno, left(accno+space(17),17)+accname as accname FROM accname where belong<>'B' and outyear=0 order by accno"
        accnoDataset = openmember(DNS_ACC, "accname", sqlstr)
        If accnoDataset.Tables("accname").Rows.Count = 0 Then
            cboAccno.Text = "尚無可請購科目"
        Else
            cboAccno.DataSource = accnoDataset.Tables("accname")
            cboAccno.DisplayMember = "accname"  '顯示欄位
            cboAccno.ValueMember = "accno"     '欄位值
        End If

        dtgTarget.AutoGenerateColumns = False
        dtgSource.AutoGenerateColumns = False

        Me.WindowState = FormWindowState.Maximized

    End Sub

    Private Sub dtpDate_ValueChanged(sender As Object, e As EventArgs) Handles dtpDate.ValueChanged
        'dtpDate.Format = DateTimePickerFormat.Custom
        'dtpDate.CustomFormat = String.Format("{0}/MM/dd", dtpDate.Value.AddYears(-1911).Year.ToString("00"))
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        sYear = GetYear(dtpDate.Value)
        '傳票資料來源
        If rdbFile1.Checked Then sFile = "1"
        If rdbFile2.Checked Then sFile = "2"
        If rdbFile3.Checked Then sFile = "3"
        '傳票總類(1收2支)
        sKind = IIf(rdbKind1.Checked, "1", "2")

        lblYear.Text = Format(sYear, "000")
        If sFile = "1" Then lblFile.Text = "請輸入請購編號"
        If sFile = "2" Then lblFile.Text = "請輸入保證金編號"
        If sKind = "1" Then TabPage2.BackColor = System.Drawing.Color.RosyBrown 'Thistle 'MistyRose
        If sKind = "2" Then TabPage2.BackColor = System.Drawing.Color.DarkSeaGreen
        TabControl1.Visible = True

        If sFile = "3" Then
            '清空輸入欄位值
            vxtAccno1.Text = "" : vxtAccno2.Text = "" : vxtAccno3.Text = "" : vxtAccno4.Text = "" : vxtAccno5.Text = "" : vxtAccno6.Text = ""
            txtRemark1.Text = "" : txtRemark2.Text = "" : txtRemark3.Text = "" : txtRemark4.Text = "" : txtRemark5.Text = "" : txtRemark6.Text = ""
            txtAmt1.Text = "" : txtAmt2.Text = "" : txtAmt3.Text = "" : txtAmt4.Text = "" : txtAmt5.Text = "" : txtAmt6.Text = ""
            txtCode2.Text = "" : txtCode3.Text = "" : txtCode4.Text = "" : txtCode5.Text = "" : txtCode6.Text = ""
            txtSubAmt.Text = "" : cboBank.SelectedValue = ""
            txtQty2.Text = "" : txtQty3.Text = "" : txtQty4.Text = "" : txtQty5.Text = "" : txtQty6.Text = ""

            '產生預設值
            Select Case INI_Read("BASIC", "LOGIN", "FIRM")
                Case "石門"
                    cboBank.SelectedValue = IIf(sKind = "1", "30", "06") '銀行帳號
            End Select

            TabControl1.SelectedIndex = 1   '由空白開始開傳票時,直接至傳票畫面
            'If Not IsDBNull(myDatasetS) Then myDatasetS.Clear() '清空傳票來源
        Else
            Call LoadGridFunc()
        End If
        txtNo.Focus()
    End Sub

    Sub LoadGridFunc()
        '將bgf030->no_1_no=0置入source datagrid 
        Dim sqlstr, qstr, sortstr As String
        If sFile = "1" Then   '資料來源:預算資料(取no_1_no=0 and 已開支(date4開支日期<>null) and 收入或支出資料決定於開立何種傳票)
            sqlstr = "SELECT BGF030.*, bgf020.subject, accname.bookaccno as accno, bgf020.kind as kind " & _
                   "FROM BGF030  LEFT OUTER JOIN BGF020 ON BGF030.bgno=BGF020.bgno " & _
                   "LEFT OUTER JOIN ACCNAME ON BGF020.ACCNO = ACCNAME.ACCNO " & _
                   "WHERE BGF030.date4 is not null and year(bgf030.date4)=" & sYear + 1911 & _
                   " and BGF030.no_1_no = 0 and (BGF020.kind='" & sKind & "' or bgf020.dc='"
            If sKind = "1" Then sqlstr += "2') order by bgf030.bgno " 'INCLUDE 預算轉帳
            If sKind = "2" Then sqlstr += "1') order by bgf030.bgno "
        Else    '資料來源:保證金資料 (取no_1_no=0 and 收入或支出資料決定於開立何種傳票)
            If TransPara.TransP("unittitle").indexof("臺中") >= 0 Then   '要將工程代號置摘要
                sqlstr = "SELECT bailf010.engno as bgno,'23101' as accno,bailf010.engno+enf010.engname as remark, "
            Else
                sqlstr = "SELECT bailf010.engno as bgno,'23101' as accno, enf010.engname as remark, "
            End If
            sqlstr &= " bailf010.amt as useamt, bailf010.cop as subject, bailf010.kind as kind, bailf010.autono as autono " & _
                     " FROM bailf010  LEFT OUTER JOIN enf010 ON bailf010.engno=enf010.engno" & _
                     " where bailf010.rp='" & sKind & "' and bailf010.no_1_no = 0" & _
                     " and year(bailf010.rpdate)=" & sYear + 1911 & _
                     " order by bailf010.engno"
        End If
        myDatasetS = openmember(DNS_ACC, "ac010s", sqlstr)
        dtgSource.DataSource = myDatasetS
        dtgSource.DataMember = "ac010s"
        bmS = Me.BindingContext(myDatasetS, "ac010s")
        myDatasetT = myDatasetS.Copy()
        myDatasetT.Clear()
        dtgTarget.DataSource = myDatasetT
        dtgTarget.DataMember = "ac010s"
        bmT = Me.BindingContext(myDatasetT, "ac010s")
        TabControl1.SelectedIndex = 0
        lblUseNO.Text = Str(QueryNO(sYear, sKind))
        lblNOkind.Text = " 上張編號:" & IIf(sKind = "1", "收", "支")
    End Sub

    Private Sub btnNo_Click(sender As Object, e As EventArgs) Handles btnNo.Click
        If txtNo.Text = "" Then Exit Sub
        Dim strBGNO As String
        Dim i As Integer
        strBGNO = Format(sYear, "000") & Format(Val(txtNo.Text), "00000")
        For i = 0 To bmS.Count - 1
            bmS.Position = i
            If bmS.Current("bgno") = strBGNO Then
                Call dtgSource_DoubleClick(New Object, New System.EventArgs)
                txtNo.Text = ""
                txtNo.Focus()
                Exit Sub
            End If
        Next
    End Sub


    Private Sub dtgSource_DoubleClick(sender As Object, e As EventArgs) Handles dtgSource.DoubleClick
        Dim inti As Integer
        inti = bmS.Position
        If bmS.Position < 0 Then Exit Sub
        If IsDBNull(bmS.Current("accno")) Then Exit Sub
        If bmT.Count < 5 Then    '只置五筆記錄
            If bmT.Count = 0 Then    '記錄第一筆會計科目以便控制四級科目相同
                strAccno4 = Mid(bmS.Current("accno"), 1, 5)
            Else
                If Mid(bmS.Current("accno"), 1, 5) <> strAccno4 Then
                    MsgBox("四級科目不相同")
                    Exit Sub
                End If
            End If
            Dim nr As DataRow
            nr = myDatasetT.Tables("ac010s").NewRow()
            nr("bgno") = bmS.Current("bgno")
            nr("accno") = bmS.Current("accno")
            nr("remark") = nz(bmS.Current("remark"), Space(10))
            nr("subject") = bmS.Current("subject")
            nr("useamt") = bmS.Current("useamt")
            nr("kind") = bmS.Current("kind")
            nr("autono") = bmS.Current("autono")
            myDatasetT.Tables("ac010s").Rows.Add(nr)                          '增行至target grid
            myDatasetS.Tables("ac010s").Rows.RemoveAt(bmS.Position.ToString)  '將source grid刪行
        End If
    End Sub

    Private Sub dtgTarget_DoubleClick(sender As Object, e As EventArgs) Handles dtgTarget.DoubleClick
        Dim inti As Integer
        inti = bmT.Position
        If bmT.Position < 0 Then Exit Sub
        If IsDBNull(bmT.Current("accno")) Then Exit Sub
        Dim nr As DataRow
        nr = myDatasetS.Tables("ac010s").NewRow()
        nr("bgno") = bmT.Current("bgno")
        nr("accno") = bmT.Current("accno")
        nr("remark") = bmT.Current("remark")
        nr("subject") = bmT.Current("subject")
        nr("useamt") = bmT.Current("useamt")
        nr("kind") = bmT.Current("kind")
        nr("autono") = bmT.Current("autono")
        myDatasetS.Tables("ac010s").Rows.Add(nr)                          '增行至source grid
        myDatasetT.Tables("ac010s").Rows.RemoveAt(bmT.Position.ToString)  '將target grid刪行
    End Sub

    Private Sub btnSure_Click(sender As Object, e As EventArgs) Handles btnSure.Click
        Call clsScreen()
        Dim SumAmt As Decimal = 0
        Dim intI As Integer
        Dim strI, strRemark, sqlstr, sbank, strAccno As String
        Dim tempdataset As DataSet

        bmT.Position = 0
        lblNo_1_no.Text = Val(lblUseNO.Text) + 1
        lblNowNO.Text = "製票編號:" & IIf(sKind = "1", "收", "支")
        TabControl1.SelectedIndex = 1
        If bmT.Count = 0 Then Exit Sub
        vxtAccno1.Text = Mid(bmT.Current("accno"), 1, 5)
        '固定資產及材料要有數量
        strAccno = Mid(bmT.Current("accno"), 1, 5)
        If Mid(strAccno, 1, 3) = "114" Or Mid(strAccno, 1, 3) = "112" Or _
           (Mid(strAccno, 1, 2) = "13" And strAccno <> "13701" And strAccno <> "13201" And Mid(strAccno, 5, 1) <> "2") Then
            gbxQty.Visible = True
        End If
        '受贈公積要有相關科目
        If Mid(strAccno, 1, 5) = "31102" Then
            Call enableOther()
        End If
        '總帳
        txtRemark1.Text = Trim(bmT.Current("remark")) & "  " & IIf(IsDBNull(bmT.Current("subject")), "  ", bmT.Current("subject"))
        '取消刪除前面是xx站
        'If Len(txtRemark1.Text) > 25 And Mid(txtRemark1.Text, 3, 1) = "站" Then     'dele摘要前面是xx站
        '    If txtRemark1.Text.IndexOf("水門看守") < 0 Then    '水門看守不要刪除前面是xx站
        '        txtRemark1.Text = Mid(txtRemark1.Text, 4)   '總帳行摘要
        '    End If
        'End If
        '明細帳
        For intI = 0 To bmT.Count - 1
            strI = CType(intI + 2, String)
            strRemark = nz(bmT.Current("remark"), "")
            '取消刪除前面是xx站
            'If Not (strRemark.IndexOf("月額") >= 0 Or strRemark.IndexOf("值班") >= 0 Or strRemark.IndexOf("水門看守") >= 0) Then
            '    '因為預算系統有依前三字排序的需求,但傳票不需記錄站
            '    If Len(strRemark) > 25 And Mid(strRemark, 3, 1) = "站" Then
            '        strRemark = Mid(strRemark, 4)
            '    End If
            'End If

            FindControl(Me, "txtRemark" & strI).Text = strRemark
            If IsDBNull(bmT.Current("subject")) = False Then
                If InStr(bmT.Current("remark"), bmT.Current("subject")) = 0 Then
                    If sFile = "2" Then   '保證金資料
                        If sKind = "1" Then '收入傳票
                            Select Case bmT.Current("kind")
                                Case "1"
                                    FindControl(Me, "txtRemark" & strI).Text = Trim(bmT.Current("remark")) & "履約金收入" & "  " & bmT.Current("subject")
                                Case "2"
                                    FindControl(Me, "txtRemark" & strI).Text = Trim(bmT.Current("remark")) & "押標金收入" & "  " & bmT.Current("subject")
                                Case "3"
                                    FindControl(Me, "txtRemark" & strI).Text = Trim(bmT.Current("remark")) & "保固金收入" & "  " & bmT.Current("subject")
                                Case "4"
                                    FindControl(Me, "txtRemark" & strI).Text = Trim(bmT.Current("remark")) & "差額保證金收入" & "  " & bmT.Current("subject")
                            End Select
                        Else   '支出傳票
                            Select Case bmT.Current("kind")
                                Case "1"
                                    FindControl(Me, "txtRemark" & strI).Text = Trim(bmT.Current("remark")) & "履約金退還" & "  " & bmT.Current("subject")
                                Case "2"
                                    FindControl(Me, "txtRemark" & strI).Text = Trim(bmT.Current("remark")) & "押標金退還" & "  " & bmT.Current("subject")
                                Case "3"
                                    FindControl(Me, "txtRemark" & strI).Text = Trim(bmT.Current("remark")) & "保固金退還" & "  " & bmT.Current("subject")
                                Case "4"
                                    FindControl(Me, "txtRemark" & strI).Text = Trim(bmT.Current("remark")) & "差額保證金退還" & "  " & bmT.Current("subject")
                            End Select
                        End If
                    Else
                        FindControl(Me, "txtRemark" & strI).Text = Trim(bmT.Current("remark")) & "  " & bmT.Current("subject")
                    End If
                End If
            End If
            FindControl(Me, "txtAmt" & strI).Text = FormatNumber(Math.Abs(bmT.Current("useamt")), 2)
            CType(FindControl(Me, "vxtaccno" & strI), MaskedTextBox).Text = bmT.Current("accno")  'function findcontrol at vbdataio.vb
            SumAmt += Math.Abs(bmT.Current("useamt"))   '合計總帳金額
            bmT.Position += 1
        Next
        If sFile = "2" Then '保證金資料 
            txtRemark1.Text = txtRemark2.Text   '抓明細第一行摘要置總帳行摘要
        End If
        txtAmt1.Text = FormatNumber(SumAmt, 2)

        '設定銀行
        bmT.Position = 0
        sbank = ""
        sqlstr = "SELECT bank FROM accname WHERE ACCNO = '" & Trim(Mid(bmT.Current("accno"), 1, 16)) & "'"
        tempdataset = openmember(DNS_ACC, "accname", sqlstr)
        If nz(tempdataset.Tables("accname").Rows(0).Item(0), "").ToString.Trim() <> "" Then
            sbank = nz(tempdataset.Tables("accname").Rows(0).Item(0), "").ToString.Trim()
        Else
            sbank = dbGetSingleRow(DNS_ACC, "ACF010", "BANK", "ACCYEAR = '" & GetYear(dtpDate.Value) & "'", "autono DESC")
        End If
        '科目未設銀行時,依app.config定義
        If Trim(sbank) = "" Then
            If sKind = "1" Then
                sbank = "01"
            Else
                sbank = "01"
            End If
        End If

        '產生預設值
        Select Case INI_Read("BASIC", "LOGIN", "FIRM")
            Case "石門"
                cboBank.SelectedValue = IIf(sKind = "1", "30", "06") '銀行帳號
            Case "高雄"
                cboBank.SelectedValue = "01"
            Case Else
                cboBank.SelectedValue = sbank                        '設定科目銀行選定值
        End Select

        'If Trim(sbank) = "" And TransPara.TransP("UnitTitle").indexof("彰化") >= 0 Then
        '    If sKind = "1" Then cboBank.SelectedValue = "04" '彰化水利會收入皆以04 
        '    If sKind = "2" Then cboBank.SelectedValue = "01" '彰化水利會支出皆以01
        'End If
        'If Trim(sbank) = "" And TransPara.TransP("UnitTitle").indexof("公") >= 0 Then
        '    cboBank.SelectedValue = "04"
        'End If

        tempdataset = Nothing
        lblDate1.Text = dtpDate.Value.ToShortDateString
    End Sub

    Private Sub clsScreen()    '清傳票螢幕
        Dim intI As Integer
        Dim strI As String
        For intI = 1 To 6
            strI = CType(intI, String)
            CType(FindControl(Me, "vxtaccno" & strI), MaskedTextBox).Text = ""
            If intI > 1 Then FindControl(Me, "txtcode" & strI).Text = ""
            FindControl(Me, "txtremark" & strI).Text = ""
            FindControl(Me, "txtAmt" & strI).Text = ""
            If intI >= 2 Then FindControl(Me, "lblAccname" & strI).Text = ""
            If intI >= 2 Then FindControl(Me, "txtQty" & strI).Text = ""
            If intI >= 2 Then CType(FindControl(Me, "vxtother" & strI), MaskedTextBox).Text = ""
        Next
        txtSubAmt.Text = ""
        gbxQty.Visible = False
        Call disableOther()
    End Sub
    Sub enableOther()   '相關科目
        vxtOther2.Visible = True
        vxtOther3.Visible = True
        vxtOther4.Visible = True
        vxtOther5.Visible = True
        vxtOther6.Visible = True
        btnF42.Visible = True
        btnF43.Visible = True
        btnF44.Visible = True
        btnF45.Visible = True
        btnF46.Visible = True
    End Sub

    Sub disableOther()
        vxtOther2.Visible = False
        vxtOther3.Visible = False
        vxtOther4.Visible = False
        vxtOther5.Visible = False
        vxtOther6.Visible = False
        btnF42.Visible = False
        btnF43.Visible = False
        btnF44.Visible = False
        btnF45.Visible = False
        btnF46.Visible = False
    End Sub



    Private Sub btnNo5_Click(sender As Object, e As EventArgs) Handles btnNo6.Click, btnNo5.Click, btnNo4.Click, btnNo3.Click, btnNo2.Click
        Dim strAccno As String
        Dim intX, intY, intI As Integer
        strObject = Mid(sender.name, 6, 1)

        '將combo移至科目的position及預設值
        intX = CType(FindControl(Me, "vxtaccno" & strObject), MaskedTextBox).Location.X
        intY = CType(FindControl(Me, "vxtaccno" & strObject), MaskedTextBox).Location.Y
        cboAccno.Visible = True
        cboAccno.Location = New Point(intX, intY)
        strAccno = Trim(CType(FindControl(Me, "vxtaccno" & strObject), MaskedTextBox).Text)
        cboAccno.SelectedValue = strAccno.Replace("-", "")
        cboAccno.Focus()
        cboAccno.DroppedDown = True
    End Sub

    Private Sub btnF42_Click(sender As Object, e As EventArgs) Handles btnF46.Click, btnF45.Click, btnF44.Click, btnF43.Click, btnF42.Click
        Dim strItem, strName As String
        strItem = Mid(sender.name, 6, 1) 'btnF42
        'If FindControl(Me, "vxtOther" & strItem).getTrimData() = "" Then
        '    Exit Sub
        'End If
        Select Case strItem
            Case "2"
                strName = vxtOther2.Text
            Case "3"
                strName = vxtOther3.Text
            Case "4"
                strName = vxtOther4.Text
            Case "5"
                strName = vxtOther5.Text
            Case "6"
                strName = vxtOther6.Text
        End Select
        If strName = "" Then
            Exit Sub
        End If
        sqlstr = "SELECT accname FROM accname WHERE ACCNO = '" & strName & "'"
        tempdataset = openmember(DNS_ACC, "accname", sqlstr)
        If tempdataset.Tables("accname").Rows.Count > 0 Then
            FindControl(Me, "txtRemark" & strItem).Text = tempdataset.Tables("accname").Rows(0).Item(0) + FindControl(Me, "txtRemark" & strItem).Text
        End If
        tempdataset = Nothing
    End Sub

    Private Sub btnPs1_Click(sender As Object, e As EventArgs) Handles btnPs6.Click, btnPs5.Click, btnPs4.Click, btnPs3.Click, btnPs2.Click, btnPs1.Click
        Dim strRemark As String
        Dim intX, intY, intI As Integer
        strObject = Mid(sender.name, 6, 1)
        '將combo移至摘要的position
        intX = FindControl(Me, "txtremark" & strObject).Location.X
        intY = FindControl(Me, "txtremark" & strObject).Location.Y
        cboRemark.Visible = True
        cboRemark.Location = New Point(intX, intY)
        strRemark = FindControl(Me, "txtremark" & strObject).Text
        intI = cboRemark.FindString(strRemark)  '設定combo起值
        cboRemark.SelectedIndex = intI
        cboRemark.Focus()
        cboRemark.DroppedDown = True  '展開下拉式清單
    End Sub

    Private Sub btnIntCopy_Click(sender As Object, e As EventArgs) Handles btnIntCopy.Click
        If btnIntCopy.Text = "傳票印一份" Then
            btnIntCopy.Text = "傳票印二份"
        Else
            btnIntCopy.Text = "傳票印一份"
        End If
    End Sub

    Private Sub btnReturn_Click(sender As Object, e As EventArgs) Handles btnReturn.Click
        If ac010kind = "開立傳票" Then
            TabControl1.SelectedIndex = 0
        Else
            TabControl1.Visible = False
        End If
    End Sub

    Private Sub btnOther_Click(sender As Object, e As EventArgs) Handles btnOther.Click
        Call enableOther()
    End Sub

    Private Sub vxtAccno1_Leave(sender As Object, e As EventArgs) Handles vxtAccno6.Leave, vxtAccno5.Leave, vxtAccno4.Leave, vxtAccno3.Leave, vxtAccno2.Leave, vxtAccno1.Leave
        If Trim(nz(sender.text.Replace("-", ""), "")) = "" Then
            Exit Sub
        End If
        'End If
        Dim strObjectName, strAccno As String
        sqlstr = "SELECT accname,bank FROM accname WHERE ACCNO = '" & Trim(sender.text.Replace("-", "")) & "'"
        tempdataset = openmember(DNS_ACC, "accname", sqlstr)
        If tempdataset.Tables("accname").Rows.Count <= 0 Then
            MsgBox("無此科目")
            sender.Focus()
        Else
            strObjectName = sender.name
            FindControl(Me, "lblaccname" & Mid(strObjectName, 9, 1)).Text = tempdataset.Tables("accname").Rows(0).Item(0)
            If Mid(strObjectName, 9, 1) = "2" Then '由第二項科目設定銀行設定值
                If Trim(tempdataset.Tables("accname").Rows(0)("bank")) <> "" Then
                    cboBank.SelectedValue = tempdataset.Tables("accname").Rows(0)("bank")   '設定銀行設定值
                    'Else
                    '    If sKind = "1" Then cboBank.SelectedValue = "04" '彰化水利會收入皆以04 
                    '    If sKind = "2" Then cboBank.SelectedValue = "01" '彰化水利會支出皆以01
                End If
                '由第二項科目設定固定資產及材料要有數量
                strAccno = Trim(Mid(vxtAccno2.Text.Replace("-", ""), 1, 5))
                If Mid(strAccno, 1, 3) = "114" Or Mid(strAccno, 1, 3) = "112" Or _
                   (Mid(strAccno, 1, 2) = "13" And Mid(strAccno, 1, 5) <> "13701" And Mid(strAccno, 1, 5) <> "13201" And Mid(strAccno, 5, 1) <> "2") Then
                    gbxQty.Visible = True
                End If
                '由第二項科目設定相關科目
                If Mid(strAccno, 1, 5) = "31102" Then
                    Call enableOther()
                End If
            End If
        End If
        tempdataset = Nothing
    End Sub

    Private Sub btnCopy1_Click(sender As Object, e As EventArgs) Handles btnCopy1.Click
        'copy第一行 科目 摘要 金額 至第二行
        'If nz(vxtAccno2.Text.Trim(), "") = "" Then vxtAccno2.Text = vxtAccno1.Text.Trim()

        vxtAccno2.Text = vxtAccno1.Text.Trim() & Mid(vxtAccno2.Text.Trim(), 7, Len(vxtAccno2.Text.Trim()) - 6)
        txtRemark2.Text = txtRemark1.Text
        'If ValComa(txtAmt2.Text) = 0 Then txtAmt2.Text = txtAmt1.Text
        txtAmt2.Text = txtAmt1.Text
    End Sub

    Private Sub btnCopy5_Click(sender As Object, e As EventArgs) Handles btnCopy5.Click
        'copy第一行 科目 摘要 金額 至第2,3,4,5,6行
        'If nz(vxtAccno2.Text.Trim(), "") = "" Then vxtAccno2.Text = vxtAccno1.Text.Trim
        'If nz(vxtAccno3.Text.Trim(), "") = "" Then vxtAccno3.Text = vxtAccno1.Text.Trim
        'If nz(vxtAccno4.Text.Trim(), "") = "" Then vxtAccno4.Text = vxtAccno1.Text.Trim
        'If nz(vxtAccno5.Text.Trim(), "") = "" Then vxtAccno5.Text = vxtAccno1.Text.Trim
        'If nz(vxtAccno6.Text.Trim(), "") = "" Then vxtAccno6.Text = vxtAccno1.Text.Trim

        vxtAccno2.Text = vxtAccno1.Text.Trim & Mid(vxtAccno2.Text.Trim(), 7, Len(vxtAccno2.Text.Trim()) - 6)
        vxtAccno3.Text = vxtAccno1.Text.Trim & Mid(vxtAccno3.Text.Trim(), 7, Len(vxtAccno3.Text.Trim()) - 6)
        vxtAccno4.Text = vxtAccno1.Text.Trim & Mid(vxtAccno4.Text.Trim(), 7, Len(vxtAccno4.Text.Trim()) - 6)
        vxtAccno5.Text = vxtAccno1.Text.Trim & Mid(vxtAccno5.Text.Trim(), 7, Len(vxtAccno5.Text.Trim()) - 6)
        vxtAccno6.Text = vxtAccno1.Text.Trim & Mid(vxtAccno6.Text.Trim(), 7, Len(vxtAccno6.Text.Trim()) - 6)

        txtRemark2.Text = txtRemark1.Text
        txtRemark3.Text = txtRemark1.Text
        txtRemark4.Text = txtRemark1.Text
        txtRemark5.Text = txtRemark1.Text
        txtRemark6.Text = txtRemark1.Text
        'If ValComa(txtAmt2.Text) = 0 Then txtAmt2.Text = txtAmt1.Text
        txtAmt2.Text = txtAmt1.Text
        txtAmt3.Text = txtAmt1.Text
        txtAmt4.Text = txtAmt1.Text
        txtAmt5.Text = txtAmt1.Text
        txtAmt6.Text = txtAmt1.Text

    End Sub



    Private Sub cboAccno_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cboAccno.SelectionChangeCommitted
        CType(FindControl(Me, "vxtaccno" & strObject), MaskedTextBox).Text = Trim(cboAccno.SelectedValue)
        cboAccno.Visible = False
    End Sub

    Private Sub cboRemark_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cboRemark.SelectionChangeCommitted
        FindControl(Me, "txtremark" & strObject).Text = cboRemark.SelectedValue
        cboRemark.Visible = False
    End Sub

    Private Sub btnFinish_Click(sender As Object, e As EventArgs) Handles btnFinish.Click
        Dim intI, intJ, SumAmt, AryAmt(5) As Decimal
        Dim strI, strJ, strAccno, strOther, AryRemark(5), retstr, strCode As String
        Dim AryCode() = {" ", " ", " ", " ", " "}  '將傳票螢幕資料置入array
        Dim AryAccno() = {"", "", "", "", ""}
        Dim AryOther() = {"", "", "", "", ""}
        Dim AryQty() = {"", "", "", "", ""}
        Dim tempdataset As DataSet

        '防呆
        If cboBank.SelectedValue = "" Then MsgBox("銀行帳號未選擇") : Exit Sub

        '檢查資料
        SumAmt = 0
        vxtAccno1.Text = Trim(Mid(Trim(vxtAccno2.Text.Replace("-", "")), 1, 5))   '由第二項科目設定第一項科目
        If txtRemark1.Text = "" Then txtRemark1.Text = txtRemark2.Text
        strAccno4 = Trim(vxtAccno1.Text.Replace("-", ""))

        If strAccno4 = "" Then Exit Sub '都沒有資料

        For intI = 2 To 6
            strI = CType(intI, String)
            strAccno = Trim(CType(FindControl(Me, "vxtAccno" & strI), MaskedTextBox).Text.Replace("-", ""))
            If Mid(strAccno, 1, 1) = "" Or ValComa(FindControl(Me, "txtamt" & strI).Text) = 0 Then  '科目空白or金額=0
                Exit For
            End If
            strCode = FindControl(Me, "txtcode" & strI).Text.ToUpper
            AryAccno(intI - 2) = strAccno
            If Mid(strAccno, 1, 5) <> strAccno4 Then
                MsgBox("四級科目不相同")
                Exit Sub
            End If
            '檢查數量
            If Mid(strAccno, 1, 3) = "114" Or Mid(strAccno, 1, 3) = "112" Or _
               (Mid(strAccno, 1, 2) = "13" And Mid(strAccno, 1, 5) <> "13701" And Mid(strAccno, 1, 5) <> "13201" And Mid(strAccno, 5, 1) <> "2") Then
                If ValComa(FindControl(Me, "txtQty" & strI).Text) = 0 Then
                    MsgBox("請輸入數量")
                    gbxQty.Visible = True
                    Exit Sub
                End If
            End If
            sqlstr = "SELECT accno FROM accname WHERE belong<>'B' AND ACCNO LIKE '" & strAccno & "%'"
            tempdataset = openmember(DNS_ACC, "accname", sqlstr)
            If tempdataset.Tables("accname").Rows.Count = 0 Then
                MsgBox("無此科目")
                FindControl(Me, "txtaccno" & strI).Focus()
                Exit Sub
            Else
                If strCode <> "E" Then     '內容別='E' 是收補助款或退回補助款  可由七級記帳
                    If tempdataset.Tables("accname").Rows.Count > 1 Then
                        If Grade(strAccno) < Grade(tempdataset.Tables("accname").Rows(1).Item(0)) Then  '找級數 grade() at vbdataid.vb
                            MsgBox("請輸入至最明細科目")
                            Exit Sub
                        End If
                    End If
                End If
            End If
            'other_accno
            strOther = Trim(CType(FindControl(Me, "vxtother" & strI), MaskedTextBox).Text.Replace("-", ""))
            AryOther(intI - 2) = strOther
            sqlstr = "SELECT accno FROM accname WHERE belong<>'B' AND ACCNO LIKE '" & strOther & "%'"
            tempdataset = openmember(DNS_ACC, "accname", sqlstr)
            If tempdataset.Tables("accname").Rows.Count = 0 Then
                MsgBox("無此相關科目")
                FindControl(Me, "vxtother" & strI).Focus()
                Exit Sub
            End If
            If Mid(strAccno, 1, 5) = "31102" And strOther = "" Then
                MsgBox("請注意第" & Str(intI) & "項相關科目未輸入")
            End If
            tempdataset = Nothing

            AryCode(intI - 2) = strCode
            If Mid(strAccno4, 1, 3) = "212" Or Mid(strAccno4, 1, 3) = "113" Or Mid(strAccno4, 1, 3) = "151" Then
                If AryCode(intI - 2) < "1" Or AryCode(intI - 2) > "4" Then
                    MsgBox("請輸入內容別")
                    FindControl(Me, "txtcode" & strI).Focus()
                    Exit Sub
                End If
            End If
            AryRemark(intI - 2) = FindControl(Me, "txtRemark" & strI).Text
            AryAmt(intI - 2) = ValComa(FindControl(Me, "txtAmt" & strI).Text)
            AryQty(intI - 2) = ValComa(FindControl(Me, "txtqty" & strI).Text)
            SumAmt += AryAmt(intI - 2) '合計總帳金額
        Next
        If SumAmt <= 0 Then
            MsgBox("金額不可=0")
            Exit Sub
        End If
        If SumAmt - ValComa(txtSubAmt.Text) < 0 Then
            MsgBox("實收付金額不可小於0")
            Exit Sub
        End If

        If ac010kind = "修改傳票" Then
            sqlstr = "DELETE acf010 where accyear=" & sYear & " and kind='" & sKind & "' and no_1_no=" & txtOldNo.Text
            retstr = runsql(mastconn, sqlstr)
            If retstr <> "sqlok" Then MsgBox("ACF010刪除失敗" & sqlstr)
            sqlstr = "DELETE acf020 where accyear=" & sYear & " and kind='" & sKind & "' and no_1_no=" & txtOldNo.Text
            retstr = runsql(mastconn, sqlstr)
            If retstr <> "sqlok" Then MsgBox("ACF020刪除失敗" & sqlstr)
        End If

        '寫入資料表acf010傳票總帳檔
        Dim strDc As String
        Dim intNo As Integer
        Dim strdate1 As String
        If ac010kind = "修改傳票" Then
            intNo = Val(txtOldNo.Text)
            strdate1 = CDate(lblDate1.Text).ToShortDateString   '原製票日期
        Else
            intNo = RequireNO(mastconn, sYear, sKind)    '\accservice\service1.asmx
            strdate1 = dtpDate.Value.ToShortDateString   '製票日期
        End If
        For intI = 1 To 2
            GenInsSql("accyear", sYear, "N")
            GenInsSql("kind", sKind, "T")
            GenInsSql("no_1_no", intNo, "N")
            GenInsSql("no_2_no", 0, "N")
            GenInsSql("SEQ", "0", "T")   '頁次='0'
            GenInsSql("item", IIf(intI = 1, "1", "9"), "T")  '總帳項為1,9
            GenInsSql("date_1", strdate1, "D")   '製票日期
            GenInsSql("systemdate", Now.ToShortDateString, "D")   '系統日期(藉以記錄user實際update日期)
            If (sKind = "1" And intI = 2) Or (sKind = "2" And intI = 1) Then
                strDc = "1"      '收入傳票9項為借方,支出傳票時1項為借方
            Else
                strDc = "2"      '收入傳票1項為貸方,支出傳票時9項為貸方
            End If
            GenInsSql("dc", strDc, "T")
            If intI = 1 Then
                strAccno = Trim(vxtAccno1.Text.Replace("-", ""))
            Else
                sqlstr = "SELECT accno FROM chf020 WHERE bank = '" & cboBank.SelectedValue & "'"
                tempdataset = openmember(DNS_ACC, "chf020", sqlstr)
                If tempdataset.Tables("chf020").Rows.Count > 0 Then
                    strAccno = tempdataset.Tables("chf020").Rows(0).Item(0)    '第9項會計科目由chf020該銀行記錄之會計科目決定
                End If
                tempdataset = Nothing
            End If
            GenInsSql("accno", strAccno, "T")
            GenInsSql("remark", Trim(txtRemark1.Text), "U")   '摘要
            GenInsSql("amt", SumAmt, "N")
            GenInsSql("act_amt", SumAmt - Val(txtSubAmt.Text), "N")
            GenInsSql("bank", cboBank.SelectedValue, "T")
            GenInsSql("books", " ", "T")                 '過帳碼
            sqlstr = "insert into acf010 " & GenInsFunc
            retstr = runsql(mastconn, sqlstr)
            If retstr <> "sqlok" Then
                MsgBox("ACF010新增失敗" & sqlstr)
                If intI = 2 Then   '已經insert總帳項1
                    sqlstr = "delete from acf010 where accyear=" & sYear & " and no_1_no=" & intNo & " and kind='" & sKind & _
                             "' and no_2_no=0 and date_2='" & FullDate(strdate1) & "'"   '以確切dele方才的insert 防止刪除原有資料 
                    retstr = runsql(mastconn, sqlstr)
                    If retstr <> "sqlok" Then
                        MsgBox("刪除 ACF010失敗" & sqlstr)
                    End If
                End If
                Exit Sub    '不再新增acf020 
            End If
        Next

        '寫入資料表acf020傳票明細
        For intI = 0 To 4
            If AryAccno(intI) = "" Or AryAmt(intI) = 0 Then Exit For
            GenInsSql("accyear", sYear, "N")
            GenInsSql("kind", sKind, "T")                    '收入傳票kind="1",支出傳票kind="2"
            GenInsSql("no_1_no", intNo, "N")
            GenInsSql("no_2_no", 0, "N")
            GenInsSql("SEQ", "0", "T")                       '收入,支出傳票無頁次
            GenInsSql("item", CType(intI + 2, String), "T")
            GenInsSql("dc", IIf(sKind = "1", "2", "1"), "T") '收入傳票時1-6項為貸方,支出傳票時1-6項為借方
            GenInsSql("accno", AryAccno(intI), "T")
            GenInsSql("remark", AryRemark(intI), "U")
            GenInsSql("amt", AryAmt(intI), "N")
            GenInsSql("cotn_code", AryCode(intI), "T")
            GenInsSql("mat_qty", AryQty(intI), "N")                 '材料數量
            If AryAmt(intI) <> 0 And AryQty(intI) <> 0 Then GenInsSql("mat_pric", AryAmt(intI) / AryQty(intI), "N") '材料單價
            GenInsSql("other_accno", AryOther(intI), "T")           '相關科目
            sqlstr = "insert into acf020 " & GenInsFunc
            retstr = runsql(mastconn, sqlstr)
            If retstr <> "sqlok" Then
                MsgBox("ACF020新增失敗" & sqlstr)
                If intI >= 2 Then   '已經insert acf020 
                    sqlstr = "delete from acf020 where accyear=" & sYear & " and no_1_no=" & intNo & " and kind='" & sKind & _
                              "' and no_2_no=0 "   '以確切dele方才的insert 防止刪除原有資料 
                    retstr = runsql(mastconn, sqlstr)
                    If retstr <> "sqlok" Then
                        MsgBox("刪除 ACF020失敗" & sqlstr)
                    End If
                    sqlstr = "delete from acf010 where accyear=" & sYear & " and no_1_no=" & intNo & " and kind='" & sKind & _
                                        "' and no_2_no=0 and date_2='" & FullDate(strdate1) & "'"   '以確切dele方才的insert 防止刪除原有資料 
                    retstr = runsql(mastconn, sqlstr)
                    If retstr <> "sqlok" Then
                        MsgBox("刪除 ACF020失敗" & sqlstr)
                    End If
                End If
                Exit Sub
            End If
        Next

        '回寫編號至傳票來源
        If ac010kind = "開立傳票" Then
            If sFile = "1" Then   '回寫編號至BGF030->no_1_no,並清空傳票內容選入區
                Dim intAutono As Integer
                bmT.Position = 0
                Do While bmT.Count > 0
                    intAutono = bmT.Current("autono")
                    sqlstr = "UPDATE bgf030 SET no_1_no = " & intNo & " WHERE autono = " & intAutono
                    retstr = runsql(mastconn, sqlstr)
                    myDatasetT.Tables("ac010s").Rows.RemoveAt(bmT.Position.ToString)   'to delete source grid 
                Loop
            Else
                If sFile = "2" Then
                    '回寫編號至bailf010->no_1_no,並清空傳票內容選入區
                    Dim intAutono As Integer
                    bmT.Position = 0
                    Do While bmT.Count > 0
                        intAutono = bmT.Current("autono")
                        sqlstr = "UPDATE bailf010 SET no_1_no = " & intNo & " WHERE autono = " & intAutono
                        retstr = runsql(mastconn, sqlstr)
                        myDatasetT.Tables("ac010s").Rows.RemoveAt(bmT.Position.ToString)   'to delete source grid 
                    Loop
                End If
            End If
            lblUseNO.Text = Str(intNo)     '顯示實際使用編號
            lblNOkind.Text = " 上張編號:" & IIf(sKind = "1", "收", "支")
        Else
            TabControl1.Visible = False
            lblMOdMsg.Text = txtOldNo.Text & "號傳票已修正完成"
            txtOldNo.Text = ""
        End If

        '列印傳票
        Select Case INI_Read("BASIC", "LOGIN", "FIRM")
            Case "石門"
                Dim result As Integer = MessageBox.Show("是否直接印出傳票", "系統訊息", MessageBoxButtons.YesNoCancel)

                If result = DialogResult.Yes Then
                    If btnIntCopy.Text = "傳票印一份" Then
                        PrintIncomeSlip(sYear, sKind, intNo, TransPara.TransP("UnitTitle"), 0) '傳票印一份
                    Else
                        PrintIncomeSlip(sYear, sKind, intNo, TransPara.TransP("UnitTitle"), 1) '傳票印二份 (正本)
                        PrintIncomeSlip(sYear, sKind, intNo, TransPara.TransP("UnitTitle"), 2) '傳票印二份 (副本)
                    End If
                End If
            Case Else
                If btnIntCopy.Text = "傳票印一份" Then
                    PrintIncomeSlip(sYear, sKind, intNo, TransPara.TransP("UnitTitle"), 0) '傳票印一份
                Else
                    PrintIncomeSlip(sYear, sKind, intNo, TransPara.TransP("UnitTitle"), 1) '傳票印二份 (正本)
                    PrintIncomeSlip(sYear, sKind, intNo, TransPara.TransP("UnitTitle"), 2) '傳票印二份 (副本)
                End If
        End Select

        'If sKind = 1 Then PrintIncomeSlip(sYear, sKind, intNo, TransPara.TransP("UnitTitle")) ' 印收入傳票 at printslip.vb
        'If sKind = 2 Then PrintIncomeSlip(sYear, sKind, intNo, TransPara.TransP("UnitTitle")) ' 印支出傳票 at printslip.vb
        Call clsScreen()               '清傳票螢幕
        If ac010kind = "開立傳票" Then
            If sFile = "3" Then   '由空白搜尋時  轉至空白傳票頁
                TabControl1.SelectedIndex = 1  '回空白傳票頁
            Else
                TabControl1.SelectedIndex = 0  '回傳票來源頁datagrid PAGE 1
                txtNo.Focus()
            End If
        End If
    End Sub

    '列印收入傳票
    Private Sub PrintIncomeSlip(ByVal sYear As Integer, ByVal sKind As String, ByVal No1 As Integer, ByVal orgName As String, ByVal intCopy As Integer)
        Dim printer As FPPrinter = FPPrinter.SharedPrinter
        'Dim printer = New KPrint
        Dim doc As FPDocument
        Dim page As FPPage
        Dim intI, intJ As Integer
        Dim strName7, strName8 As String   '七級科目名稱 八級科目名稱
        Dim strI As String
        Dim decAmt As Decimal
        sqlstr = "SELECT a.*, b.bankname as bankname,c.accname as accname from acf010 a" & _
                 " left outer join chf020 b on a.bank=b.bank " & _
                 " left outer join accname c on a.accno=c.accno " & _
                 " where accyear=" & sYear & " and kind='" & sKind & _
                 "' and no_1_no=" & No1 & " order by item"
        tempdataset = openmember(DNS_ACC, "ac010s", sqlstr)
        If tempdataset.Tables("ac010s").Rows.Count <= 0 Then
            Exit Sub
        End If

        '取得空白的收入傳票
        If sKind = "1" Then doc = GetIncomeSlipDoc()
        If sKind = "2" Then doc = GetPaySlipDoc()
        page = doc.GetPage(0)
        '設定空白收入傳票上的機關名稱
        page.GetText("機關名稱").Text = orgName
        Select Case intCopy
            Case 1
                page.GetText("正副本").Text = "正本"
            Case 2
                page.GetText("正副本").Text = "副本"
            Case 0
                page.GetText("正副本").Text = ""
        End Select
        '設定第0個table中,與製票相關的屬性
        page.GetTable(0).GetText("製票年").Text = FormatNumber(GetYear(tempdataset.Tables("ac010s").Rows(0)("date_1")), 0)
        page.GetTable(0).GetText("製票月").Text = Month(tempdataset.Tables("ac010s").Rows(0)("date_1"))
        page.GetTable(0).GetText("製票日").Text = Microsoft.VisualBasic.DateAndTime.Day(tempdataset.Tables("ac010s").Rows(0)("date_1"))
        page.GetTable(0).GetText("製票編號").Text = No1
        '設定第2個table中,與會計科目相關的屬性
        page.GetTable(2).Texts2D(2, 2).Text = FormatAccno(tempdataset.Tables("ac010s").Rows(0)("accno")) & vbCrLf & tempdataset.Tables("ac010s").Rows(0)("accname") '"在此放入總帳科目"
        'page.GetTable(2).Texts2D(2, 3).Text = tempdataset.Tables("ac010s").Rows(0)("remark")    '總帳項不印摘要
        page.GetTable(2).Texts2D(2, 4).Text = FormatNumber(tempdataset.Tables("ac010s").Rows(0)("amt"), 2)
        '設定第3個table中,與沖收數相關的屬性
        decAmt = tempdataset.Tables("ac010s").Rows(0)("amt") - tempdataset.Tables("ac010s").Rows(0)("act_amt")
        page.GetTable(3).Texts2D(2, 1).Text = IIf(decAmt = 0, "－", FormatNumber(decAmt, 2))
        decAmt = tempdataset.Tables("ac010s").Rows(0)("act_amt")
        page.GetTable(3).Texts2D(2, 2).Text = IIf(decAmt = 0, "－", FormatNumber(decAmt, 2))
        page.GetTable(3).Texts2D(2, 3).Text = tempdataset.Tables("ac010s").Rows(0)("bank") & tempdataset.Tables("ac010s").Rows(0)("bankname")
        '明細帳
        sqlstr = "SELECT a.*, b.accname as name6, c.accname as name7, d.accname as name8 " & _
                 " from acf020 a " & _
                 " LEFT OUTER JOIN accname b ON Rtrim(SUBSTRING(a.ACCNO,1,9)) = b.ACCNO" & _
                 " LEFT OUTER JOIN accname c ON Rtrim(SUBSTRING(a.ACCNO,1,16)) = c.ACCNO and len(a.accno)>9 " & _
                 " LEFT OUTER JOIN accname d ON a.ACCNO = d.ACCNO and len(a.accno)=17 " & _
                 "where accyear=" & sYear & " and kind='" & sKind & _
                 "' and no_1_no=" & No1 & " order by item"

        tempdataset = openmember(DNS_ACC, "ac010s", sqlstr)
        With tempdataset.Tables("ac010s")
            For intI = 0 To .Rows.Count - 1
                intJ = CType(intI + 3, String)
                strName7 = nz(.Rows(intI)("name7"), "")
                strName8 = nz(.Rows(intI)("name8"), "")
                'If Len(strName7) + Len(strName8) > 28 Then
                '    strName7 = Mid(strName7, 1, 28 - Len(strName8))
                'End If
                page.GetTable(2).Texts2D(intI + 3, 2).Text = FormatAccno(.Rows(intI)("accno")) & vbCrLf & .Rows(intI)("name6") '"在此放入總帳科目"
                page.GetTable(2).Texts2D(intI + 3, 3).Text = strName7 & " " & strName8 & vbCrLf & nz(.Rows(intI)("remark"), "")
                page.GetTable(2).Texts2D(intI + 3, 4).Text = FormatNumber(.Rows(intI)("amt"), 2)
                page.GetTable(4).Texts2D(intI + 3, 1).Text = .Rows(intI)("cotn_code") '設定第4個table中的內容碼
                If nz(.Rows(intI)("mat_qty"), 0) <> 0 Then
                    page.GetTable(2).Texts2D(intI + 3, 3).Text += "  " & _
                    Format(.Rows(intI)("mat_qty"), "###,###,###,###.######") & " x " & Format(.Rows(intI)("mat_pric"), "###,###,###,###.####")
                End If
            Next
        End With

        Try
            printer.Document = doc
            If TransPara.TransP("Print") = "Preview" Then printer.IsAutoShowPrintPreviewDialog = True
            printer.Print()
        Catch ex As Exception

        End Try


    End Sub

    Private Sub btnOldNo_Click(sender As Object, e As EventArgs) Handles btnOldNo.Click
        lblMOdMsg.Text = ""
        If txtOldNo.Text = "" Then Exit Sub
        Dim strAccno As String
        Call clsScreen()
        sYear = GetYear(dtpDate.Value)
        sKind = IIf(rdbKind3.Checked, "1", "2")
        If sKind = "1" Then TabPage2.BackColor = System.Drawing.Color.RosyBrown 'Thistle 'MistyRose
        If sKind = "2" Then TabPage2.BackColor = System.Drawing.Color.DarkSeaGreen
        Dim SumAmt As Decimal = 0
        Dim intI As Integer
        Dim strI, sqlstr As String
        Dim tempdataset As DataSet
        '總帳
        sqlstr = "SELECT * from acf010 where accyear=" & sYear & " and kind='" & sKind & _
                 "' and no_1_no=" & txtOldNo.Text & " order by item"
        tempdataset = openmember(DNS_ACC, "ac010s", sqlstr)
        If tempdataset.Tables("ac010s").Rows.Count <= 0 Then
            MsgBox("無此傳票")
            Exit Sub
        End If
        If tempdataset.Tables("ac010s").Rows(0)("no_2_no") <> 0 Then
            MsgBox("此傳票已作帳,傳票號=" & tempdataset.Tables("ac010s").Rows(0)("no_2_no"))
            Exit Sub
        End If
        If nz(tempdataset.Tables("ac010s").Rows(1)("chkno"), "") <> "" Then   '支票號寫在item='9'
            MsgBox("此傳票已開支票=" & tempdataset.Tables("ac010s").Rows(1)("chkno"))

            Exit Sub
        End If
        lblDate1.Text = tempdataset.Tables("ac010s").Rows(0)("date_1")         '製票日期
        cboBank.SelectedValue = tempdataset.Tables("ac010s").Rows(0)("bank")   '設定科目銀行選定值
        txtSubAmt.Text = tempdataset.Tables("ac010s").Rows(0)("amt") - tempdataset.Tables("ac010s").Rows(0)("act_amt")
        txtRemark1.Text = tempdataset.Tables("ac010s").Rows(0)("remark")
        vxtAccno1.Text = tempdataset.Tables("ac010s").Rows(0)("accno")
        txtAmt1.Text = FormatNumber(tempdataset.Tables("ac010s").Rows(0)("amt"), 2)
        strAccno = tempdataset.Tables("ac010s").Rows(0)("accno")
        '固定資產及材料要有數量
        If Mid(strAccno, 1, 3) = "114" Or Mid(strAccno, 1, 3) = "112" Or _
           (Mid(strAccno, 1, 2) = "13" And Mid(strAccno, 1, 5) <> "13701" And Mid(strAccno, 1, 5) <> "13201" And Mid(strAccno, 5, 1) <> "2") Then
            gbxQty.Visible = True
        End If
        '明細帳
        sqlstr = "SELECT * from acf020 where accyear=" & sYear & " and kind='" & sKind & "' and no_1_no=" & _
                 txtOldNo.Text & " order by item"
        tempdataset = openmember(DNS_ACC, "ac010s", sqlstr)
        With tempdataset.Tables("ac010s")
            For intI = 0 To .Rows.Count - 1
                strI = CType(intI + 2, String)
                FindControl(Me, "txtRemark" & strI).Text = Trim(.Rows(intI)("remark"))
                FindControl(Me, "txtAmt" & strI).Text = FormatNumber(.Rows(intI)("amt"), 2)
                FindControl(Me, "txtQty" & strI).Text = Format(.Rows(intI)("mat_qty"), "###,###,###.######")
                FindControl(Me, "txtcode" & strI).Text = .Rows(intI)("cotn_code")
                CType(FindControl(Me, "vxtaccno" & strI), MaskedTextBox).Text = .Rows(intI)("accno")
                If Not IsDBNull(tempdataset.Tables("ac010s").Rows(0)("other_accno")) Then
                    CType(FindControl(Me, "vxtother" & strI), MaskedTextBox).Text = .Rows(intI)("other_accno")
                End If
            Next
        End With
        strI = nz(tempdataset.Tables("ac010s").Rows(0)("other_accno"), "")
        If strI <> "" Then
            Call enableOther()
        End If
        tempdataset = Nothing
        TabControl1.Visible = True
        TabControl1.SelectedIndex = 1
    End Sub

    Private Sub txtOldNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOldNo.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then   'press the enter key
            Call btnOldNo_Click(New System.Object, New System.EventArgs)
        End If
    End Sub

    Private Sub vxtAccno1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles vxtOther6.KeyPress, vxtOther5.KeyPress, vxtOther4.KeyPress, vxtOther3.KeyPress, vxtOther2.KeyPress, vxtAccno6.KeyPress, vxtAccno5.KeyPress, vxtAccno4.KeyPress, vxtAccno3.KeyPress, vxtAccno2.KeyPress, vxtAccno1.KeyPress, txtSubAmt.KeyPress, txtRemark6.KeyPress, txtRemark5.KeyPress, txtRemark4.KeyPress, txtRemark3.KeyPress, txtRemark2.KeyPress, txtRemark1.KeyPress, txtQty6.KeyPress, txtQty5.KeyPress, txtQty4.KeyPress, txtQty3.KeyPress, txtQty2.KeyPress, txtCode6.KeyPress, txtCode5.KeyPress, txtCode4.KeyPress, txtCode3.KeyPress, txtCode2.KeyPress, txtAmt6.KeyPress, txtAmt5.KeyPress, txtAmt4.KeyPress, txtAmt3.KeyPress, txtAmt2.KeyPress, txtAmt1.KeyPress
        If Asc(e.KeyChar) = 13 Then   'enter key 
            SendKeys.Send("{TAB}")
        End If

    End Sub

    Private Sub vxtAccno1_KeyUp(sender As Object, e As KeyEventArgs) Handles vxtAccno1.KeyUp
        If Trim(vxtAccno1.Text.Replace("-", "")) = "" Then Exit Sub
        If Len(Trim(vxtAccno1.Text.Replace("-", ""))) = 5 Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNo.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then   'press the enter key
            Call btnNo_Click(New System.Object, New System.EventArgs)
        End If
    End Sub

    Private Sub txtRemark1_TextChanged(sender As Object, e As EventArgs) Handles txtRemark1.TextChanged, txtRemark6.TextChanged, txtRemark5.TextChanged, txtRemark4.TextChanged, txtRemark3.TextChanged, txtRemark2.TextChanged
        'If System.Text.Encoding.GetEncoding(950).GetByteCount(sender.Text) > 50 Then
        '    Dim linestr As Byte() = System.Text.Encoding.GetEncoding(950).GetBytes(sender.Text)
        '    sender.Text = System.Text.Encoding.GetEncoding(950).GetString(linestr, 0, 50)
        '    sender.SelectionStart = sender.Text.Length
        '    sender.ScrollToCaret()
        '    sender.Focus()
        'End If
    End Sub

    Private Sub txtAmt1_TextChanged(sender As Object, e As EventArgs) Handles txtAmt1.TextChanged
        '防呆
        'If txtAmt1.Text <> "" Then If IsNumeric(txtAmt1.Text.Replace(",", "")) = False Then Exit Sub
    End Sub
    Private Sub txtAmt_TextChanged(sender As Object, e As EventArgs) Handles txtAmt2.TextChanged, txtAmt3.TextChanged, txtAmt4.TextChanged, txtAmt5.TextChanged, txtAmt6.TextChanged
        '防呆
        'If txtAmt2.Text <> "" Then If IsNumeric(txtAmt2.Text.Replace(",", "")) = False Then Exit Sub
        'If txtAmt3.Text <> "" Then If IsNumeric(txtAmt3.Text.Replace(",", "")) = False Then Exit Sub
        'If txtAmt4.Text <> "" Then If IsNumeric(txtAmt4.Text.Replace(",", "")) = False Then Exit Sub
        'If txtAmt5.Text <> "" Then If IsNumeric(txtAmt5.Text.Replace(",", "")) = False Then Exit Sub
        'If txtAmt6.Text <> "" Then If IsNumeric(txtAmt6.Text.Replace(",", "")) = False Then Exit Sub

        'Dim intAmt As Double = 0
        'If txtAmt2.Text <> "" Then intAmt += txtAmt2.Text.Replace(",", "")
        'If txtAmt3.Text <> "" Then intAmt += txtAmt3.Text.Replace(",", "")
        'If txtAmt4.Text <> "" Then intAmt += txtAmt4.Text.Replace(",", "")
        'If txtAmt5.Text <> "" Then intAmt += txtAmt5.Text.Replace(",", "")
        'If txtAmt6.Text <> "" Then intAmt += txtAmt6.Text.Replace(",", "")

        'txtAmt1.Text = FormatNumber(intAmt, 2)

        'If sender.text <> "" Then
        '    sender.text = FormatNumber(ValComa(sender.text), 2)
        'End If
    End Sub

    Private Sub txtAmt2_Leave(sender As Object, e As EventArgs) Handles txtAmt6.Leave, txtAmt5.Leave, txtAmt4.Leave, txtAmt3.Leave, txtAmt2.Leave
        If (Not IsNumeric(sender.text) Or ValComa(sender.text) < 0) And Trim(sender.text) <> "" Then
            MsgBox("請輸入正數字")
            sender.focus()
        Else
            If sender.text <> "" Then
                sender.text = FormatNumber(ValComa(sender.text), 2)
            End If
            txtAmt1.Text = FormatNumber(ValComa(txtAmt2.Text) + ValComa(txtAmt3.Text) + ValComa(txtAmt4.Text) + ValComa(txtAmt5.Text) + ValComa(txtAmt6.Text), 2)
        End If
    End Sub

    Private Sub txtQty2_Leave(sender As Object, e As EventArgs) Handles txtQty6.Leave, txtQty5.Leave, txtQty4.Leave, txtQty3.Leave, txtQty2.Leave
        If (Not IsNumeric(sender.text) Or ValComa(sender.text) < 0) And Trim(sender.text) <> "" Then
            MsgBox("請輸入正數字")
            sender.focus()
        Else
            sender.text = Format(ValComa(sender.text), "###,###,###.######")
        End If
    End Sub

    Private Sub txtCode2_Leave(sender As Object, e As EventArgs) Handles txtCode6.Leave, txtCode5.Leave, txtCode4.Leave, txtCode3.Leave, txtCode2.Leave
        If Trim(sender.text) <> "" Then
            If Not ((sender.text >= "1" And sender.text <= "4") Or sender.text = "A" Or sender.text = "E") Then
                MsgBox("請輸入1-4,A,E 內容別代碼")
                sender.focus()
            End If
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
End Class