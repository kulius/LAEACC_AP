﻿Imports JBC.Printing
Public Class FAC030
    Dim sYear, sNo, OldNo As Integer    '年度 & 制製票號
    Dim sFile, strAccno, strAccnoD, strAccnoC As String  '資料來源檔  & 傳票種類
    Dim sDate As DateTime        '製票日期
    Dim mydsS, mydsT As DataSet
    Dim LoadAfter, Dirty As Boolean

    Dim sqlstr As String
    Dim bmS, bmT As BindingManagerBase, strAccno4 As String
    Dim myDatasetS, myDatasetT, mydataset, tempdataset, psDataset, accnoDataset As DataSet
    Dim ac010kind As String
    Dim strObject As String
    Dim DAryAccno(8, 5) As String  '宣告DEBIT 9*6 array
    Dim DAryRemark(8, 5) As String
    Dim DAryAmt(8, 5) As Decimal
    Dim DAryCode(8, 5) As String
    Dim DAryOther(8, 5) As String
    Dim DAryQty(8, 5) As Decimal
    Dim CAryAccno(8, 5) As String  '宣告CREDIT 9*6 array
    Dim CAryRemark(8, 5) As String
    Dim CAryAmt(8, 5) As Decimal
    Dim CAryCode(8, 5) As String
    Dim CAryOther(8, 5) As String
    Dim CAryQty(8, 5) As Decimal
    Dim DebitPage, CreditPage As Integer   '借貸頁次
    Dim intI, intJ, intD, intC As Integer
    Dim checkError As Boolean = False
    Public DNS_ACC As String = INI_Read("CONFIG", "SET", "DNS_ACC")
    Public mastconn As String = INI_Read("CONFIG", "SET", "DNS_ACC")

    Private Sub FAC030_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Dim myds As DataSet
        sDate = TransPara.TransP("UserDate")
        ac010kind = TransPara.TransP("ac010kind")  '定開立傳票 or 修改傳票
        sYear = GetYear(sDate)
        lblYear.Text = Format(sYear, "000")
        If ac010kind = "開立傳票" Then
            gbxModify.Visible = False
            gbxCreate.Visible = True
            btnFirstScreen.TabStop = True
            Me.Text = "開立轉帳傳票"
        Else
            gbxModify.Visible = True
            gbxCreate.Visible = False
            gbxModify.Location = New Point(8, 0)
            Me.Text = "修改轉帳傳票"
            txtOldNo.Focus()
        End If
        LoadAfter = True
        TabControl1.Visible = False

        '將單位片語置combobox   
        sqlstr = "SELECT psstr  FROM psname where unit='0502' order by psstr"
        psDataset = openmember("", "psname", sqlstr)
        If psDataset.Tables("psname").Rows.Count = 0 Then
            cboRemark.Text = "尚無片語"
        Else
            cboRemark.DataSource = psDataset.Tables("psname")
            cboRemark.DisplayMember = "psstr"  '顯示欄位
            cboRemark.ValueMember = "psstr"     '欄位值
            cboRemark.SelectionLength = 60
        End If
        '將科目置combobox
        sqlstr = "SELECT accno, left(accno+space(17),17)+accname as accname FROM accname where belong<>'B' order by accno"
        accnoDataset = openmember("", "accname", sqlstr)
        If accnoDataset.Tables("accname").Rows.Count = 0 Then
            cboAccno.Text = "尚無可請購科目"
        Else
            cboAccno.DataSource = accnoDataset.Tables("accname")
            cboAccno.DisplayMember = "accname"  '顯示欄位
            cboAccno.ValueMember = "accno"     '欄位值
        End If
        Call Begin_Data()
    End Sub

    Private Sub Begin_Data()
        For intI = 0 To 8
            For intJ = 0 To 5
                DAryAccno(intI, intJ) = ""
                DAryRemark(intI, intJ) = ""
                DAryAmt(intI, intJ) = 0
                DAryCode(intI, intJ) = ""
                DAryOther(intI, intJ) = ""
                DAryQty(intI, intJ) = 0
                CAryAccno(intI, intJ) = ""
                CAryRemark(intI, intJ) = ""
                CAryAmt(intI, intJ) = 0
                CAryCode(intI, intJ) = ""
                CAryOther(intI, intJ) = ""
                CAryQty(intI, intJ) = 0
            Next
        Next
        DebitPage = 0
        CreditPage = 0
        btnPageUp.Enabled = False
        btnPageDown.Enabled = True
        btnDebit.Visible = False
        btnCredit.Visible = True
    End Sub

    '搜尋資料 load預算or保證品資料
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        sFile = IIf(rdbFile1.Checked, "1", "2")
        If rdbFile3.Checked Then sFile = "3" '空白資料
        lblFile.Text = IIf(sFile = "1", "請輸入預算轉帳編號", "請輸入保證品工程編號")
        TabControl1.SelectedIndex = IIf(sFile = "3", 1, 0)
        TabControl1.Visible = True
        If sFile <= "2" Then Call LoadGridFunc()
        If ac010kind = "開立傳票" Then
            gbxCreate.Visible = False
        End If
    End Sub

    Private Sub btnFirstScreen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFirstScreen.Click
        If ac010kind = "開立傳票" Then
            gbxCreate.Visible = True
        End If
    End Sub

    Private Sub txtoldNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtOldNo.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then   'press the enter key
            Call btnOldNo_Click(New System.Object, New System.EventArgs)
        End If
    End Sub

    '修改傳票(舊傳票號輸入)(將acf010 data put to array)
    Private Sub btnOldNo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOldNo.Click 'read舊傳票資料
        If txtOldNo.Text = "" Then Exit Sub
        Call Begin_Data()
        sYear = GetYear(sDate)
        Dim SumAmt As Decimal = 0
        Dim intI As Integer
        Dim strI, sqlstr As String
        Dim tempdataset As DataSet
        '總帳
        sqlstr = "SELECT * from acf010 where accyear=" & sYear & " and kind>='3'" & _
                 " and no_1_no=" & txtOldNo.Text & " order by dc, item"
        tempdataset = openmember("", "ac010s", sqlstr)
        If tempdataset.Tables("ac010s").Rows.Count <= 0 Then
            MsgBox("無此傳票")
            txtOldNo.Focus()
            Exit Sub
        End If
        If tempdataset.Tables("ac010s").Rows(0)("no_2_no") <> 0 Then
            MsgBox("此傳票已作帳,傳票號=" & tempdataset.Tables("ac010s").Rows(0)("no_2_no"))
            txtOldNo.Focus()
            Exit Sub
        End If

        With tempdataset.Tables("ac010s")
            lblDate1.Text = ShortDate(.Rows(0).Item("date_1"))  '原製票日期
            Dim intPage As Integer
            For intI = 0 To .Rows.Count - 1
                intPage = .Rows(intI)("seq") - 1
                If .Rows(intI)("dc") = "1" Then
                    DAryAccno(intPage, 0) = .Rows(intI)("accno")
                    DAryRemark(intPage, 0) = .Rows(intI)("remark")
                    DAryAmt(intPage, 0) = .Rows(intI)("amt")
                Else
                    CAryAccno(intPage, 0) = .Rows(intI)("accno")
                    CAryRemark(intPage, 0) = .Rows(intI)("remark")
                    CAryAmt(intPage, 0) = .Rows(intI)("amt")
                End If
            Next
        End With
        '明細帳
        sqlstr = "SELECT * from acf020 where accyear=" & sYear & " and kind>='3'" & _
                 " and no_1_no=" & txtOldNo.Text & " order by dc, item"
        tempdataset = openmember("", "ac010s", sqlstr)
        With tempdataset.Tables("ac010s")
            Dim intPage, intItem As Integer
            For intI = 0 To .Rows.Count - 1
                intPage = .Rows(intI)("seq") - 1
                intItem = .Rows(intI)("item") - 1
                If .Rows(intI)("dc") = "1" Then
                    DAryAccno(intPage, intItem) = .Rows(intI)("accno")
                    DAryRemark(intPage, intItem) = .Rows(intI)("remark")
                    DAryAmt(intPage, intItem) = .Rows(intI)("amt")
                    DAryCode(intPage, intItem) = nz(.Rows(intI)("cotn_code"), "")
                    DAryOther(intPage, intItem) = nz(.Rows(intI)("other_accno"), "")
                    DAryQty(intPage, intItem) = nz(.Rows(intI)("mat_qty"), 0)
                Else
                    CAryAccno(intPage, intItem) = .Rows(intI)("accno")
                    CAryRemark(intPage, intItem) = .Rows(intI)("remark")
                    CAryAmt(intPage, intItem) = .Rows(intI)("amt")
                    CAryCode(intPage, intItem) = nz(.Rows(intI)("cotn_code"), "")
                    CAryOther(intPage, intItem) = nz(.Rows(intI)("other_accno"), "")
                    CAryQty(intPage, intItem) = nz(.Rows(intI)("mat_qty"), 0)
                End If
            Next
        End With
        tempdataset = Nothing
        DebitPage = 0
        CreditPage = 0
        Call Load_Debit()
        TabControl1.Visible = True
        TabControl1.SelectedIndex = 1
    End Sub

    Sub LoadGridFunc()
        '將bgf030->no_1_no=0置入source datagrid 
        Dim sqlstr, qstr, sortstr As String
        If sFile = "1" Then   '資料來源:預算資料(取no_1_no=0 and 已開支(date4開支日期<>null) 借方kind='3'  貸方kind='4')
            sqlstr = "SELECT bgf030.bgno, abs(BGF030.useamt) as useamt, bgf030.remark,bgf030.autono, " & _
                     "bgf020.subject, accname.bookaccno as accno, bgf020.kind as kind " & _
                    "FROM BGF030  LEFT OUTER JOIN BGF020 ON BGF030.bgno=BGF020.bgno " & _
                    "LEFT OUTER JOIN ACCNAME ON BGF020.ACCNO = ACCNAME.ACCNO " & _
                    "WHERE BGF030.date4 is not null and year(bgf030.date4)=" & sYear + 1911 & _
                    " and BGF030.no_1_no = 0 and BGF020.kind>='3' order by bgf030.bgno"
        Else    '資料來源:保證品資料 (取no_1_no=0 and 收入或支出資料決定借方貸方傳票)23102存入保證品,對方科目15303保管品 
            sqlstr = "SELECT bailf020.engno as bgno,'23102' as accno, bailf020.rp as kind, " & _
                     " enf010.engname as remark, bailf020.amt as useamt, " & _
                     " bailf020.cop as subject, bailf020.autono as autono " & _
                     " FROM bailf020  LEFT OUTER JOIN enf010 ON bailf020.engno=enf010.engno " & _
                     " WHERE bailf020.no_1_no = 0 and year(bailf020.rpdate)=" & _
                      sYear + 1911 & " ORDER BY bailf020.engno"
        End If
        myDatasetS = openmember("", "ac010s", sqlstr)
        dtgSource.DataSource = myDatasetS
        dtgSource.DataMember = "ac010s"
        bmS = Me.BindingContext(myDatasetS, "ac010s")
        myDatasetT = myDatasetS.Copy()
        myDatasetT.Clear()
        dtgTarget.DataSource = myDatasetT
        dtgTarget.DataMember = "ac010s"
        bmT = Me.BindingContext(myDatasetT, "ac010s")
        'TabControl1.SelectedIndex = 0
        lblUseNO.Text = Str(QueryNO(sYear, "3"))
        lblNo_1_no.Text = Val(lblUseNO.Text) + 1
    End Sub


    Private Sub dtgTarget_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtgTarget.DoubleClick
        If bmT.Position < 0 Then Exit Sub
        If IsDBNull(bmT.Current("accno")) Then Exit Sub
        Dim nr As DataRow
        nr = myDatasetS.Tables("ac010s").NewRow()
        nr("bgno") = bmT.Current("bgno")
        nr("accno") = bmT.Current("accno")
        nr("kind") = bmT.Current("kind")
        nr("remark") = bmT.Current("remark")
        nr("subject") = bmT.Current("subject")
        nr("useamt") = bmT.Current("useamt")
        nr("autono") = bmT.Current("autono")
        myDatasetS.Tables("ac010s").Rows.Add(nr)                          '增行至source grid
        myDatasetT.Tables("ac010s").Rows.RemoveAt(bmT.Position.ToString)  '將target grid刪行
    End Sub

    Private Sub dtgSource_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtgSource.DoubleClick
        'If bmT.Count < 5 Then    '只置五筆記錄
        '    If bmT.Count = 0 Then    '記錄第一筆會計科目以便控制四級科目相同
        '        strAccno4 = Mid(bmS.Current("accno"), 1, 5)
        '    Else
        '        If Mid(bmS.Current("accno"), 1, 5) <> strAccno4 Then
        '            MsgBox("四級科目不相同")
        '            Exit Sub
        '        End If
        '    End If
        If bmS.Position < 0 Then Exit Sub
        If IsDBNull(bmS.Current("accno")) Then Exit Sub
        Dim nr As DataRow
        nr = myDatasetT.Tables("ac010s").NewRow()
        nr("bgno") = bmS.Current("bgno")
        nr("accno") = bmS.Current("accno")
        nr("kind") = bmS.Current("kind")
        nr("remark") = nz(bmS.Current("remark"), Space(10))
        nr("subject") = bmS.Current("subject")
        nr("useamt") = bmS.Current("useamt")
        nr("autono") = bmS.Current("autono")
        myDatasetT.Tables("ac010s").Rows.Add(nr)                          '增行至target grid
        myDatasetS.Tables("ac010s").Rows.RemoveAt(bmS.Position.ToString)  '將source grid刪行
        'End If
    End Sub

    '由user key in 請購編號
    Private Sub btnNo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNo.Click
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
    Private Sub txtNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNo.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then   'press the enter key
            Call btnNo_Click(New System.Object, New System.EventArgs)
        End If
    End Sub

    '傳票來源確定(先將資料置array, then put to screen)
    Private Sub btnSure_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSure.Click
        Dim SumAmt As Decimal = 0
        Dim intI As Integer
        Dim strI, sqlstr As String
        Dim tempdataset As DataSet
        'Call Begin_Data()  'clear array
        intD = 0  'debit record number 
        intC = 0   'credit record number 
        strAccnoD = ""
        strAccnoC = ""
        For intI = 0 To bmT.Count - 1
            bmT.Position = intI
            strAccno = bmT.Current("accno")
            If bmT.Current("kind") = "3" Or bmT.Current("kind") = "2" Then    '轉帳借方"3" or 保證品支出"2"(De:2-3102 Cr:1-5303) 
                Call Add_debit()
                If bmT.Current("kind") = "2" Then
                    strAccno = "15303"
                    Call Add_credit()
                End If
            Else           '轉帳貸方"4" or 保證品收入"1"(De:1-5303 Cr:2-3101)
                Call Add_credit()
                If bmT.Current("kind") = "1" Then
                    strAccno = "15303"
                    Call Add_debit()
                End If
            End If
        Next
        Call clsScreen()
        DebitPage = 0
        Call Load_Debit()
        TabControl1.SelectedIndex = 1
    End Sub

    Sub Add_debit()
        If intD > 5 Then  '0,1,2,3,4,5 六項一頁
            DebitPage += 1
            intD = 0
        End If
        If strAccnoD <> "" And Mid(strAccno, 1, 5) <> Mid(strAccnoD, 1, 5) Then  '四級不同換頁
            DebitPage += 1
            intD = 0
        End If
        strAccnoD = strAccno
        If intD = 0 Then   '第一項要加總帳項
            DAryAccno(DebitPage, intD) = Mid(strAccno, 1, 5)
            DAryRemark(DebitPage, intD) = RTrim(bmT.Current("remark")) & "  " & nz(bmT.Current("subject"), "")
            If strAccno = "15303" Then
                DAryRemark(DebitPage, intD) = RTrim(bmT.Current("remark")) & "保證品收入" & "  " & nz(bmT.Current("subject"), "")
            End If
            If strAccno = "23102" Then
                DAryRemark(DebitPage, intD) = RTrim(bmT.Current("remark")) & "保證品退還" & "  " & nz(bmT.Current("subject"), "")
            End If
            DAryAmt(DebitPage, intD) = bmT.Current("useamt")
            intD = 1
        End If
        DAryAccno(DebitPage, intD) = strAccno
        If strAccno = "15303" Then
            DAryRemark(DebitPage, intD) = RTrim(bmT.Current("remark")) & "保證品收入" & "  " & nz(bmT.Current("subject"), "")
        Else
            If strAccno = "23102" Then
                DAryRemark(DebitPage, intD) = RTrim(bmT.Current("remark")) & "保證品退還" & "  " & nz(bmT.Current("subject"), "")
            Else
                DAryRemark(DebitPage, intD) = RTrim(bmT.Current("remark")) & "  " & nz(bmT.Current("subject"), "")
            End If
        End If
        DAryAmt(DebitPage, intD) = bmT.Current("useamt")
        intD += 1
    End Sub

    Sub Add_credit()
        If intC > 5 Then  '0,1,2,3,4,5 六項一頁
            CreditPage += 1
            intC = 0
        End If
        If strAccnoC <> "" And Mid(strAccno, 1, 5) <> Mid(strAccnoC, 1, 5) Then  '四級不同換頁
            CreditPage += 1
            intC = 0
        End If
        strAccnoC = strAccno
        If intC = 0 Then   '第一項要加總帳項
            CAryAccno(CreditPage, intC) = Mid(strAccno, 1, 5)
            CAryRemark(CreditPage, intC) = RTrim(bmT.Current("remark")) & "  " & nz(bmT.Current("subject"), "")
            If strAccno = "15303" Then
                CAryRemark(CreditPage, intC) = RTrim(bmT.Current("remark")) & "保證品退還" & "  " & nz(bmT.Current("subject"), "")
            End If
            If strAccno = "23102" Then
                CAryRemark(CreditPage, intC) = RTrim(bmT.Current("remark")) & "保證品收入" & "  " & nz(bmT.Current("subject"), "")
            End If
            CAryAmt(CreditPage, intC) = bmT.Current("useamt")
            intC = 1
        End If
        CAryAccno(CreditPage, intC) = strAccno
        If strAccno = "15303" Then
            CAryRemark(CreditPage, intC) = RTrim(bmT.Current("remark")) & "保證品退還" & "  " & nz(bmT.Current("subject"), "")
        Else
            If strAccno = "23102" Then
                CAryRemark(CreditPage, intC) = RTrim(bmT.Current("remark")) & "保證品收入" & "  " & nz(bmT.Current("subject"), "")
            Else
                CAryRemark(CreditPage, intC) = RTrim(bmT.Current("remark")) & "  " & nz(bmT.Current("subject"), "")
            End If
        End If
        CAryAmt(CreditPage, intC) = bmT.Current("useamt")
        intC += 1
    End Sub

    Private Sub btnFinish_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFinish.Click
        If lblDC.Text = "轉帳借方" Then
            Call Save_Debit()
        Else
            Call Save_Credit()
        End If

        Dim SumDebit, SumCredit As Decimal


        '檢查借貸平衡
        For intI = 0 To 8  '共9頁
            SumDebit += DAryAmt(intI, 0)   '0代表第一項(總帳項)
            SumCredit += CAryAmt(intI, 0)
        Next
        If SumDebit <> SumCredit Then
            MsgBox("借貸不平衡" & SumDebit & "<>" & SumCredit)
            Exit Sub
        End If

        '修改傳票應先刪除原資料
        Dim retstr As String
        If ac010kind = "修改傳票" Then
            sqlstr = "DELETE acf010 where accyear=" & sYear & " and kind>='3' and no_1_no=" & txtOldNo.Text
            retstr = runsql(mastconn, sqlstr)
            If retstr <> "sqlok" Then MsgBox("ACF010刪除失敗" & sqlstr)
            sqlstr = "DELETE acf020 where accyear=" & sYear & " and kind>='3' and no_1_no=" & txtOldNo.Text
            retstr = runsql(mastconn, sqlstr)
            If retstr <> "sqlok" Then MsgBox("ACF020刪除失敗" & sqlstr)
        End If

        '寫入資料表acf010傳票總帳檔 & 寫入資料表acf020傳票明細
        Dim intNo As Integer
        Dim strdate1 As String
        If ac010kind = "修改傳票" Then
            intNo = Val(txtOldNo.Text)                 '製票編號
            strdate1 = lblDate1.Text                   '製票日期
        Else
            intNo = RequireNO(mastconn, sYear, "3")    '\accservice\service1.asmx
            strdate1 = sDate.ToShortDateString         '製票日期
        End If

        Dim IsError As Boolean = False  '計錄insert是否有失敗
        For intI = 0 To 8
            If DAryAccno(intI, 0) = "" Then Exit For
            '總帳項
            GenInsSql("accyear", sYear, "N")
            GenInsSql("kind", "3", "T")
            GenInsSql("no_1_no", intNo, "N")
            GenInsSql("no_2_no", 0, "N")
            GenInsSql("SEQ", CType(intI + 1, String), "T")
            GenInsSql("item", "1", "T")  '總帳項為1
            GenInsSql("date_1", strdate1, "D")   '製票日期
            GenInsSql("systemdate", Now.ToShortDateString, "D")   '系統日期(藉以記錄user實際update日期)
            GenInsSql("dc", "1", "T")
            GenInsSql("accno", DAryAccno(intI, 0), "T")
            GenInsSql("remark", DAryRemark(intI, 0), "U")   '摘要
            GenInsSql("amt", DAryAmt(intI, 0), "N")
            GenInsSql("act_amt", 0, "N")
            GenInsSql("bank", "  ", "T")
            GenInsSql("books", " ", "T")                 '過帳碼
            sqlstr = "insert into acf010 " & GenInsFunc
            retstr = runsql(mastconn, sqlstr)
            If retstr <> "sqlok" Then
                MsgBox("ACF010新增失敗" & sqlstr)
                IsError = True
            End If
            '明細帳項
            For intJ = 1 To 5
                If DAryAccno(intI, intJ) = "" Then Exit For
                GenInsSql("accyear", sYear, "N")
                GenInsSql("kind", "3", "T")                    '借方傳票kind="3"
                GenInsSql("no_1_no", intNo, "N")
                GenInsSql("no_2_no", 0, "N")
                GenInsSql("SEQ", CType(intI + 1, String), "T")    '頁次
                GenInsSql("item", CType(intJ + 1, String), "T")   '項次
                GenInsSql("dc", "1", "T")
                GenInsSql("accno", DAryAccno(intI, intJ), "T")
                GenInsSql("remark", DAryRemark(intI, intJ), "U")
                GenInsSql("amt", DAryAmt(intI, intJ), "N")
                GenInsSql("cotn_code", DAryCode(intI, intJ), "T")
                GenInsSql("mat_qty", DAryQty(intI, intJ), "N")                '材料數量
                If DAryAmt(intI, intJ) <> 0 And DAryQty(intI, intJ) <> 0 Then GenInsSql("mat_pric", DAryAmt(intI, intJ) / DAryQty(intI, intJ), "N") '材料單價
                GenInsSql("other_accno", DAryOther(intI, intJ), "T")          '相關科目
                sqlstr = "insert into acf020 " & GenInsFunc
                retstr = runsql(mastconn, sqlstr)
                If retstr <> "sqlok" Then
                    MsgBox("ACF020新增失敗" & sqlstr)
                End If
            Next
        Next
        For intI = 0 To 8
            If CAryAccno(intI, 0) = "" Then Exit For
            '總帳項
            GenInsSql("accyear", sYear, "N")
            GenInsSql("kind", "4", "T")
            GenInsSql("no_1_no", intNo, "N")
            GenInsSql("no_2_no", 0, "N")
            GenInsSql("SEQ", CType(intI + 1, String), "T")
            GenInsSql("item", "1", "T")  '總帳項為1
            GenInsSql("date_1", sDate.ToShortDateString, "D")   '製票日期
            GenInsSql("systemdate", Now.ToShortDateString, "D")   '系統日期(藉以記錄user實際update日期)
            GenInsSql("dc", "2", "T")
            GenInsSql("accno", CAryAccno(intI, 0), "T")
            GenInsSql("remark", CAryRemark(intI, 0), "U")   '摘要
            GenInsSql("amt", CAryAmt(intI, 0), "N")
            GenInsSql("act_amt", 0, "N")
            GenInsSql("bank", "  ", "T")
            GenInsSql("books", " ", "T")                 '過帳碼
            sqlstr = "insert into acf010 " & GenInsFunc
            retstr = runsql(mastconn, sqlstr)
            If retstr <> "sqlok" Then
                MsgBox("ACF020新增失敗" & sqlstr)
                IsError = True
            End If
            '明細帳項
            For intJ = 1 To 5
                If CAryAccno(intI, intJ) = "" Then Exit For
                GenInsSql("accyear", sYear, "N")
                GenInsSql("kind", "4", "T")                    '借方傳票kind="3"
                GenInsSql("no_1_no", intNo, "N")
                GenInsSql("no_2_no", 0, "N")
                GenInsSql("SEQ", CType(intI + 1, String), "T")    '頁次
                GenInsSql("item", CType(intJ + 1, String), "T")   '項次
                GenInsSql("dc", "2", "T")
                GenInsSql("accno", CAryAccno(intI, intJ), "T")
                GenInsSql("remark", CAryRemark(intI, intJ), "U")
                GenInsSql("amt", CAryAmt(intI, intJ), "N")
                GenInsSql("cotn_code", CAryCode(intI, intJ), "T")
                GenInsSql("mat_qty", CAryQty(intI, intJ), "N")               '材料數量
                If CAryAmt(intI, intJ) <> 0 And CAryQty(intI, intJ) <> 0 Then GenInsSql("mat_pric", CAryAmt(intI, intJ) / CAryQty(intI, intJ), "N") '材料單價
                GenInsSql("other_accno", CAryOther(intI, intJ), "T")          '相關科目
                sqlstr = "insert into acf020 " & GenInsFunc
                retstr = runsql(mastconn, sqlstr)
                If retstr <> "sqlok" Then
                    MsgBox("ACF020新增失敗" & sqlstr)
                    IsError = True
                End If
            Next
        Next

        If IsError Then '新增失敗
            sqlstr = "delete from acf010 where accyear=" & sYear & " and no_1_no=" & intNo & " and kind>='3' " & _
                     " and no_2_no=0 and date_2='" & FullDate(strdate1) & "'"   '以確切dele方才的insert 防止刪除原有資料 
            retstr = runsql(mastconn, sqlstr)
            If retstr <> "sqlok" Then
                MsgBox("刪除 ACF010失敗" & sqlstr)
            End If
            sqlstr = "delete from acf020 where accyear=" & sYear & " and no_1_no=" & intNo & " and kind>='3' " & _
                     " and no_2_no=0 "   '以確切dele方才的insert 防止刪除原有資料 
            retstr = runsql(mastconn, sqlstr)
            If retstr <> "sqlok" Then
                MsgBox("刪除 ACF020失敗" & sqlstr)
            End If
            Exit Sub
        End If

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
                    '回寫編號至bailf020->no_1_no,並清空傳票內容選入區
                    Dim intAutono As Integer
                    bmT.Position = 0
                    Do While bmT.Count > 0
                        intAutono = bmT.Current("autono")
                        sqlstr = "UPDATE bailf020 SET no_1_no = " & intNo & " WHERE autono = " & intAutono
                        retstr = runsql(mastconn, sqlstr)
                        myDatasetT.Tables("ac010s").Rows.RemoveAt(bmT.Position.ToString)   'to delete source grid 
                    Loop
                End If
            End If
            lblUseNO.Text = Str(intNo)     '顯示實際使用編號
        Else
            TabControl1.Visible = False
        End If
        PrintTransSlip(sYear, intNo, TransPara.TransP("UnitTitle"))     '列印傳票 at printslip.vb
        Call Begin_Data()  'clear array
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


    Private Sub clsScreen()    '清傳票螢幕
        Dim intI As Integer
        Dim strI As String
        For intI = 1 To 6
            strI = CType(intI, String)
            CType(FindControl(Me, "vxtaccno" & strI), MaskedTextBox).text = ("")
            FindControl(Me, "txtremark" & strI).Text = ""
            FindControl(Me, "txtAmt" & strI).Text = ""
            If intI > 1 Then FindControl(Me, "txtcode" & strI).Text = ""
            If intI > 1 Then FindControl(Me, "lblaccname" & strI).Text = ""
            'If intI > 1 Then FindControl(Me, "txtQty" & strI).Text = ""
            If intI > 1 Then CType(FindControl(Me, "vxtother" & strI), MaskedTextBox).text = ("")
        Next
        gbxQty.Visible = False
        Call disableOther()
    End Sub

    Sub enableOther()   '相關科目
        vxtOther2.Visible = True
        vxtOther3.Visible = True
        vxtOther4.Visible = True
        vxtOther5.Visible = True
        vxtOther6.Visible = True
    End Sub

    Sub disableOther()
        vxtOther2.Visible = False
        vxtOther3.Visible = False
        vxtOther4.Visible = False
        vxtOther5.Visible = False
        vxtOther6.Visible = False
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnReturn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReturn.Click
        If ac010kind = "開立傳票" Then
            TabControl1.SelectedIndex = 0
        Else
            TabControl1.Visible = False
        End If
    End Sub

    Private Sub btnOther_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOther.Click
        Call enableOther()
    End Sub

    '顯示會計科目名稱
    Private Sub vxtAccno2_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles vxtAccno2.LostFocus, vxtAccno3.LostFocus, vxtAccno4.LostFocus, vxtAccno5.LostFocus, vxtAccno6.LostFocus
        If sender.Text.Replace("-", "").Trim = "" Then
            Exit Sub
        End If
        Dim strObjectName As String
        sqlstr = "SELECT accname FROM accname WHERE ACCNO = '" & sender.Text.Replace("-", "").Trim & "'"
        tempdataset = openmember("", "accname", sqlstr)
        If tempdataset.Tables("accname").Rows.Count <= 0 Then
            MsgBox("無此科目")
            sender.Focus()
        Else
            strObjectName = sender.name
            FindControl(Me, "lblaccname" & Mid(strObjectName, 9, 1)).Text = tempdataset.Tables("accname").Rows(0).Item(0)
        End If
        If Mid(strObjectName, 9, 1) >= "2" Then '明細項為應收113 應付212 催收151
            strAccno = Mid(sender.Text.Replace("-", "").Trim, 1, 5)
            If Mid(strAccno, 1, 3) = "113" Or Mid(strAccno, 1, 3) = "212" Or _
               Mid(strAccno, 1, 3) = "151" Then
                If FindControl(Me, "txtCode" & Mid(strObjectName, 9, 1)).Text.Trim = "" Then
                    FindControl(Me, "txtCode" & Mid(strObjectName, 9, 1)).Text = "2"  '給實收付數碼2
                    If txtRemark1.Text.IndexOf("保留") >= 0 Then
                        FindControl(Me, "txtCode" & Mid(strObjectName, 9, 1)).Text = "1"  '給應收付數碼1
                    End If
                    If txtRemark1.Text.IndexOf("註銷") >= 0 Or txtRemark1.Text.IndexOf("減免") >= 0 Then
                        FindControl(Me, "txtCode" & Mid(strObjectName, 9, 1)).Text = "3"  '給減免數碼3
                    End If
                    If txtRemark1.Text.IndexOf("轉催收") >= 0 Then
                        FindControl(Me, "txtCode" & Mid(strObjectName, 9, 1)).Text = "4"  '給減免數碼3
                    End If
                End If
            End If
        End If
        tempdataset = Nothing
    End Sub

    Private Sub btnCopy1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy1.Click
        'copy第一行 科目 摘要 金額 至第二行
        If vxtAccno2.Text.Replace("-", "").Trim = "" Then vxtAccno2.text = (vxtAccno1.Text.Replace("-", "").Trim)
        txtRemark2.Text = txtRemark1.Text
        If ValComa(txtAmt2.Text) = 0 Then txtAmt2.Text = txtAmt1.Text
    End Sub
    Private Sub btnCopy5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy5.Click
        'copy第一行 科目 摘要 金額 至第2,3,4,5,6行
        If vxtAccno2.Text.Replace("-", "").Trim = "" Then vxtAccno2.text = (vxtAccno1.Text.Replace("-", "").Trim)
        txtRemark2.Text = txtRemark1.Text
        txtRemark3.Text = txtRemark1.Text
        txtRemark4.Text = txtRemark1.Text
        txtRemark5.Text = txtRemark1.Text
        txtRemark6.Text = txtRemark1.Text
        If ValComa(txtAmt2.Text) = 0 Then txtAmt2.Text = txtAmt1.Text
    End Sub
    Private Sub btnPs1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPs1.Click, btnPs2.Click, btnPs3.Click, btnPs4.Click, btnPs5.Click, btnPs6.Click
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
    Private Sub cboRemark_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboRemark.SelectionChangeCommitted
        FindControl(Me, "txtremark" & strObject).Text = cboRemark.Text
        cboRemark.Visible = False
    End Sub

    Private Sub btnNo2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNo2.Click, btnNo3.Click, btnNo4.Click, btnNo5.Click, btnNo6.Click
        Dim strAccno As String
        Dim intX, intY, intI As Integer
        strObject = Mid(sender.name, 6, 1)
        '將combo移至科目的position
        intX = CType(FindControl(Me, "vxtaccno" & strObject), MaskedTextBox).Location.X
        intY = CType(FindControl(Me, "vxtaccno" & strObject), MaskedTextBox).Location.Y
        cboAccno.Visible = True
        cboAccno.Location = New Point(intX, intY)
        strAccno = CType(FindControl(Me, "vxtaccno" & strObject), MaskedTextBox).Text.Replace("-", "").Trim
        intI = cboAccno.FindString(strAccno)  '設定combo起值
        cboAccno.SelectedIndex = intI
        cboAccno.Focus()
        cboAccno.DroppedDown = True
    End Sub
    Private Sub cboaccno_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboAccno.SelectionChangeCommitted
        CType(FindControl(Me, "vxtaccno" & strObject), MaskedTextBox).text = (Trim(cboAccno.SelectedValue))
        cboAccno.Visible = False
    End Sub

    Private Sub Check_data()  '檢查螢幕資料合理性
        Dim SumAmt As Decimal = 0
        Dim strI, strOther, strCode As String
        checkError = False   '不合理時給true 
        '檢查資料
        vxtAccno1.text = (Mid(vxtAccno2.Text.Replace("-", "").Trim, 1, 5))   '由第二項科目設定第一項科目
        If txtRemark1.Text = "" Then txtRemark1.Text = txtRemark2.Text
        strAccno4 = vxtAccno1.Text.Replace("-", "").Trim
        For intI = 2 To 6
            strI = CType(intI, String)
            strCode = FindControl(Me, "txtcode" & strI).Text.ToUpper
            strAccno = CType(FindControl(Me, "vxtAccno" & strI), MaskedTextBox).Text.Replace("-", "").Trim
            If Mid(strAccno, 1, 1) = "" Or ValComa(FindControl(Me, "txtamt" & strI).Text) = 0 Then  '科目空白or金額=0
                Exit For
            End If
            If Mid(strAccno, 1, 5) <> strAccno4 Then
                MsgBox("四級科目不相同")
                checkError = True
                Exit Sub
            End If
            '固定資產及材料要有數量
            'If Mid(strAccno, 1, 3) = "114" Or Mid(strAccno, 1, 3) = "112" Or _
            '   (Mid(strAccno, 1, 2) = "13" And Mid(strAccno, 1, 5) <> "13701" And Mid(strAccno, 1, 5) <> "13201" And Mid(strAccno, 5, 1) <> "2") Then
            '    If ValComa(FindControl(Me, "txtqty" & strI).Text) = 0 Then
            '        MsgBox("請輸入數量")
            '        Exit Sub
            '    End If
            'End If

            sqlstr = "SELECT accno FROM accname WHERE belong<>'B' AND ACCNO LIKE '" & strAccno & "%'"
            tempdataset = openmember("", "accname", sqlstr)
            If tempdataset.Tables("accname").Rows.Count = 0 Then
                MsgBox("無此科目")
                checkError = True
                FindControl(Me, "txtaccno" & strI).Focus()
                Exit Sub
            Else
                If strCode <> "E" Then    '內容別='E' 是收補助款或退回補助款  可由七級記帳
                    If tempdataset.Tables("accname").Rows.Count > 1 Then
                        If Grade(strAccno) < Grade(tempdataset.Tables("accname").Rows(1).Item(0)) Then  '找級數 grade() at vbdataid.vb
                            MsgBox("請輸入至最細科目")
                            checkError = True
                            Exit Sub
                        End If
                    End If
                End If
            End If
            'other_accno
            strOther = CType(FindControl(Me, "vxtother" & strI), MaskedTextBox).Text.Replace("-", "").Trim
            sqlstr = "SELECT accno FROM accname WHERE belong<>'B' AND ACCNO LIKE '" & strOther & "%'"
            tempdataset = openmember("", "accname", sqlstr)
            If tempdataset.Tables("accname").Rows.Count = 0 Then
                MsgBox("無此相關科目")
                FindControl(Me, "vxtother" & strI).Focus()
                checkError = True
                Exit Sub
            End If
            tempdataset = Nothing

            'If Mid(strAccno4, 1, 3) = "212" Or Mid(strAccno4, 1, 3) = "113" Or Mid(strAccno4, 1, 3) = "151" Then
            '    If strCode < "1" Or strCode > "4" Then
            '        MsgBox("請輸入內容別")
            '        checkError = True
            '        FindControl(Me, "txtcode" & strI).Focus()
            '        Exit Sub
            '    End If
            'End If
            SumAmt += ValComa(FindControl(Me, "txtAmt" & strI).Text) '合計總帳金額
        Next
        txtAmt1.Text = FormatNumber(SumAmt, 2)
        If SumAmt = 0 And strAccno4 <> "" Then
            MsgBox("金額不可=0")
            checkError = True
        End If

    End Sub

    Sub Save_Debit()  '將借方screen put array data
        Call Check_data()
        If checkError = True Then
            Exit Sub
        End If
        Dim strI As String
        For intI = 0 To 5
            strI = CType(intI + 1, String)
            DAryAccno(DebitPage, intI) = CType(FindControl(Me, "vxtaccno" & strI), MaskedTextBox).Text.Replace("-", "").Trim
            DAryRemark(DebitPage, intI) = FindControl(Me, "txtRemark" & strI).Text
            DAryAmt(DebitPage, intI) = ValComa(FindControl(Me, "txtAmt" & strI).Text)
            If intI > 0 Then
                DAryCode(DebitPage, intI) = FindControl(Me, "txtcode" & strI).Text.ToUpper
                DAryOther(DebitPage, intI) = CType(FindControl(Me, "vxtother" & strI), MaskedTextBox).Text.Replace("-", "").Trim
                'DAryQty(DebitPage, intI) = ValComa(FindControl(Me, "txtQty" & strI).Text)
            End If
        Next
    End Sub

    Sub Save_Credit()  '將貸方screen put array data
        Call Check_data()
        If checkError = True Then
            Exit Sub
        End If
        Dim strI As String
        For intI = 0 To 5
            strI = CType(intI + 1, String)
            CAryAccno(CreditPage, intI) = CType(FindControl(Me, "vxtaccno" & strI), MaskedTextBox).Text.Replace("-", "").Trim
            CAryRemark(CreditPage, intI) = FindControl(Me, "txtRemark" & strI).Text
            CAryAmt(CreditPage, intI) = ValComa(FindControl(Me, "txtAmt" & strI).Text)
            If intI > 0 Then
                CAryCode(CreditPage, intI) = FindControl(Me, "txtcode" & strI).Text.ToUpper
                CAryOther(CreditPage, intI) = CType(FindControl(Me, "vxtother" & strI), MaskedTextBox).Text.Replace("-", "").Trim
                'CAryQty(CreditPage, intI) = ValComa(FindControl(Me, "txtQty" & strI).Text)
            End If
        Next
    End Sub

    Sub Load_Debit()  '將借方array data put to screen 
        Dim strI As String
        For intI = 0 To 5
            strI = CType(intI + 1, String)
            CType(FindControl(Me, "vxtaccno" & strI), MaskedTextBox).text = (DAryAccno(DebitPage, intI))
            FindControl(Me, "txtRemark" & strI).Text = Trim(DAryRemark(DebitPage, intI))
            FindControl(Me, "txtAmt" & strI).Text = FormatNumber(DAryAmt(DebitPage, intI), 2)
            If intI > 0 Then
                FindControl(Me, "txtcode" & strI).Text = Trim(DAryCode(DebitPage, intI))
                FindControl(Me, "lblaccname" & strI).Text = ""   '清上一畫面科目名稱
                CType(FindControl(Me, "vxtother" & strI), MaskedTextBox).text = (DAryOther(DebitPage, intI))
                'FindControl(Me, "txtqty" & strI).Text = DAryQty(DebitPage, intI)
            End If
        Next
        lblPage.Text = DebitPage + 1
    End Sub

    Sub Load_Credit()   '將貸方array data put to screen 
        Dim strI As String
        For intI = 0 To 5
            strI = CType(intI + 1, String)
            CType(FindControl(Me, "vxtaccno" & strI), MaskedTextBox).text = (CAryAccno(CreditPage, intI))
            FindControl(Me, "txtRemark" & strI).Text = Trim(CAryRemark(CreditPage, intI))
            FindControl(Me, "txtAmt" & strI).Text = FormatNumber(CAryAmt(CreditPage, intI), 2)
            If intI > 0 Then
                FindControl(Me, "txtcode" & strI).Text = Trim(CAryCode(CreditPage, intI))
                FindControl(Me, "lblaccname" & strI).Text = ""   '清上一畫面科目名稱
                CType(FindControl(Me, "vxtother" & strI), MaskedTextBox).text = (CAryOther(CreditPage, intI))
                'FindControl(Me, "txtqty" & strI).Text = CAryQty(CreditPage, intI)
            End If
        Next
        lblPage.Text = CreditPage + 1
    End Sub


    '上頁
    Private Sub btnPageUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPageUp.Click
        If lblDC.Text = "轉帳借方" Then
            Call Save_Debit()  '先儲存本頁資料
            If checkError = True Then Exit Sub '資料檢查有問題
            If DebitPage >= 1 Then
                DebitPage -= 1
                Call Load_Debit()
                If DebitPage = 0 Then btnPageUp.Enabled = False
            End If
        Else
            Call Save_Credit()
            If checkError = True Then Exit Sub
            If CreditPage >= 1 Then
                CreditPage -= 1
                Call Load_Credit()
                If CreditPage = 0 Then btnPageUp.Enabled = False
            End If
        End If
        btnPageDown.Enabled = True
    End Sub

    '下頁
    Private Sub btnPagedown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPageDown.Click
        If lblDC.Text = "轉帳借方" Then
            Call Save_Debit()  '先儲存本頁資料
            If checkError = True Then Exit Sub '資料檢查有問題
            If DebitPage < 8 Then
                DebitPage += 1
                Call Load_Debit()
                If DebitPage = 8 Then btnPageDown.Enabled = False
            End If
        Else
            Call Save_Credit()  '先儲存本頁資料
            If checkError = True Then Exit Sub '資料檢查有問題
            If CreditPage < 8 Then
                CreditPage += 1
                Call Load_Credit()
                If CreditPage = 8 Then btnPageDown.Enabled = False
            End If
        End If
        btnPageUp.Enabled = True
    End Sub

    Private Sub btnDebit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDebit.Click
        Call Save_Credit()
        lblDC.Text = "轉帳借方"
        btnDebit.Visible = False
        btnCredit.Visible = True
        Call Load_Debit()
        btnPageUp.Enabled = True
        btnPageDown.Enabled = True
        If DebitPage = 0 Then btnPageUp.Enabled = False
        If DebitPage = 8 Then btnPageDown.Enabled = False
    End Sub

    Private Sub btnCredit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCredit.Click
        Call Save_Debit()
        lblDC.Text = "轉帳貸方"
        btnDebit.Visible = True
        btnCredit.Visible = False
        Call Load_Credit()
        btnPageUp.Enabled = True
        btnPageDown.Enabled = True
        If CreditPage = 0 Then   '第一頁時
            btnPageUp.Enabled = False
            If Trim(txtRemark1.Text) = "" Then
                If txtRemark1.Text = "" Then txtRemark1.Text = DAryRemark(0, 0) '第一頁時, copy 借方第一頁摘要
                If txtRemark2.Text = "" Then txtRemark2.Text = DAryRemark(0, 1)
                If txtRemark3.Text = "" Then txtRemark3.Text = DAryRemark(0, 2)
                If txtRemark4.Text = "" Then txtRemark4.Text = DAryRemark(0, 3)
                If txtRemark5.Text = "" Then txtRemark5.Text = DAryRemark(0, 4)
                If txtRemark6.Text = "" Then txtRemark6.Text = DAryRemark(0, 5)
            End If
        End If
        If CreditPage = 8 Then btnPageDown.Enabled = False
    End Sub


    '列印轉帳傳票
    Private Sub PrintTransSlip(ByVal sYear As Integer, ByVal No1 As Integer, ByVal orgName As String)
        Dim printer As FPPrinter = FPPrinter.SharedPrinter
        Dim doc As FPDocument
        Dim page As FPPage
        Dim intI, intJ, I As Integer
        Dim J As Integer = 0
        Dim LastJ As Integer = 0   '記錄上頁筆數
        Dim strI As String
        Dim strName7, strName8 As String  '七級科目名稱 八級科目名稱

        sqlstr = "SELECT a.*, c.accname as accname from acf010 a" & _
                 " left outer join accname c on a.accno=c.accno " & _
                 " where accyear=" & sYear & " and kind>='3' and no_1_no=" & No1 & _
                 " order by a.kind,a.seq,a.item"
        tempdataset = openmember("", "ac010s", sqlstr)
        If tempdataset.Tables("ac010s").Rows.Count <= 0 Then
            Exit Sub
        End If

        '明細帳
        sqlstr = "SELECT a.*, b.accname as name6, c.accname as name7, d.accname as name8 " & _
                 " from acf020 a " & _
                 " LEFT OUTER JOIN accname b ON Rtrim(SUBSTRING(a.ACCNO,1,9)) = b.ACCNO" & _
                 " LEFT OUTER JOIN accname c ON Rtrim(SUBSTRING(a.ACCNO,1,16)) = c.ACCNO and len(a.accno)>9 " & _
                 " LEFT OUTER JOIN accname d ON a.ACCNO = d.ACCNO and len(a.accno)=17 " & _
                 "where accyear=" & sYear & " and kind>='3' and no_1_no=" & No1 & _
                 " order by a.kind, a.seq, a.item"
        mydataset = openmember("", "acf020", sqlstr)

        Dim allDoc As New FPDocument("轉帳傳票")
        allDoc.DefaultPageSettings.PaperKind = Printing.PaperKind.B5
        allDoc.SetDefaultPageMargin(15, 25, 0, 0)    'SET MARGIN LEFT TOP RIGHT BOTTON
        allDoc.DefaultPageSettings.Landscape = True

        For I = 0 To tempdataset.Tables("ac010s").Rows.Count - 1
            doc = GetTransSlipDoc()   '取得空白的轉帳傳票   at printslip.vb
            page = doc.GetPage(0)
            allDoc.InsertDocument(doc) '將空白的轉帳傳票加入總文件
            '設定空白收入傳票上的機關名稱
            page.GetText("機關名稱").Text = orgName
            page.GetText("轉帳種類").Text = IIf(tempdataset.Tables("ac010s").Rows(I)("kind") = "3", "借 方", "貸 方")
            '設定第0個table中,與製票相關的屬性
            page.GetTable(0).GetText("轉帳年").Text = FormatNumber(GetYear(tempdataset.Tables("ac010s").Rows(I)("date_1")), 0)
            page.GetTable(0).GetText("轉帳月").Text = Month(tempdataset.Tables("ac010s").Rows(I)("date_1"))
            page.GetTable(0).GetText("轉帳日").Text = Microsoft.VisualBasic.DateAndTime.Day(tempdataset.Tables("ac010s").Rows(I)("date_1"))
            If tempdataset.Tables("ac010s").Rows.Count > 2 Then
                page.GetTable(0).GetText("轉帳編號").Text = No1 & "-" & tempdataset.Tables("ac010s").Rows(I)("seq")   '標別頁次
            Else
                page.GetTable(0).GetText("轉帳編號").Text = No1
            End If

            '設定第2個table中,與會計科目相關的屬性
            page.GetTable(2).Texts2D(2, 2).Text = FormatAccno(tempdataset.Tables("ac010s").Rows(I)("accno")) & vbCrLf & tempdataset.Tables("ac010s").Rows(I)("accname") '"在此放入總帳科目"
            'page.GetTable(2).Texts2D(2, 3).Text = tempdataset.Tables("ac010s").Rows(I)("remark")   '總帳項不印摘要
            page.GetTable(2).Texts2D(2, 4).Text = FormatNumber(tempdataset.Tables("ac010s").Rows(I)("amt"), 2)

            intJ = 0  '控制該頁項次
            With mydataset.Tables("acf020")
                For J = LastJ To .Rows.Count - 1
                    If .Rows(J)("kind") <> tempdataset.Tables("ac010s").Rows(I)("kind") Then Exit For
                    If .Rows(J)("seq") <> tempdataset.Tables("ac010s").Rows(I)("seq") Then Exit For
                    strName7 = nz(.Rows(J)("name7"), "")
                    strName8 = nz(.Rows(J)("name8"), "")
                    If Len(strName7) + Len(strName8) > 28 Then
                        strName7 = Mid(strName7, 1, 28 - Len(strName8))
                    End If
                    'intI = CType(intJ + 3, String)   '明細項由第三項開始
                    page.GetTable(2).Texts2D(intJ + 3, 2).Text = FormatAccno(.Rows(J)("accno")) & vbCrLf & .Rows(J)("name6") '"在此放入總帳科目"
                    page.GetTable(2).Texts2D(intJ + 3, 3).Text = strName7 & " " & strName8 & vbCrLf & nz(.Rows(J)("remark"), "")
                    page.GetTable(2).Texts2D(intJ + 3, 4).Text = FormatNumber(.Rows(J)("amt"), 2)
                    page.GetTable(3).Texts2D(intJ + 3, 1).Text = nz(.Rows(J)("cotn_code"), "") '設定第4個table中的內容碼
                    If nz(.Rows(J)("mat_qty"), 0) <> 0 Then
                        page.GetTable(2).Texts2D(intJ + 3, 3).Text += "  " & _
                        Format(.Rows(J)("mat_qty"), "###,###,###.######") & " x " & Format(.Rows(J)("mat_pric"), "###,###,###.####")
                    End If
                    intJ += 1
                Next
                LastJ = J    '記錄下頁第一筆
            End With
        Next
        printer.Document = allDoc
        If TransPara.TransP("Print") = "Preview" Then printer.IsAutoShowPrintPreviewDialog = True
        printer.Print()
    End Sub

    Private Sub txtAmt2_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAmt2.LostFocus, txtAmt3.LostFocus, _
                                                                             txtAmt4.LostFocus, txtAmt5.LostFocus, txtAmt6.LostFocus
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

    Private Sub txtQty2_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtQty2.LostFocus, _
                                      txtQty3.LostFocus, txtQty4.LostFocus, txtQty5.LostFocus, txtQty6.LostFocus
        If (Not IsNumeric(sender.text) Or ValComa(sender.text) < 0) And Trim(sender.text) <> "" Then
            MsgBox("請輸入正數字")
            sender.focus()
        Else
            sender.text = Format(ValComa(sender.text), "###,###,###.######")
        End If
    End Sub

    Private Sub txtno_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNo.LostFocus, txtOldNo.LostFocus
        If (Not IsNumeric(sender.text) Or ValComa(sender.text) < 0) And Trim(sender.text) <> "" Then
            MsgBox("請輸入正數字")
            sender.focus()
        End If
    End Sub

    Private Sub txtCode2_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCode2.LostFocus, _
                                         txtCode3.LostFocus, txtCode4.LostFocus, txtCode5.LostFocus, txtCode6.LostFocus
        If Trim(sender.text) <> "" Then
            If Not ((sender.text >= "1" And sender.text <= "4") Or sender.text = "A" Or sender.text = "E") Then
                MsgBox("請輸入1-4,A,E 內容別代碼")
                sender.focus()
            End If
        End If
    End Sub

    Private Sub txtAmt1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAmt1.KeyPress, _
            txtAmt2.KeyPress, txtAmt3.KeyPress, txtAmt4.KeyPress, txtAmt5.KeyPress, txtAmt6.KeyPress, _
            txtCode2.KeyPress, txtCode3.KeyPress, txtCode4.KeyPress, txtCode5.KeyPress, txtCode6.KeyPress, _
            txtQty2.KeyPress, txtQty3.KeyPress, txtQty4.KeyPress, txtQty5.KeyPress, txtQty6.KeyPress, _
            txtRemark1.KeyPress, txtRemark2.KeyPress, txtRemark3.KeyPress, txtRemark4.KeyPress, txtRemark5.KeyPress, txtRemark6.KeyPress, _
            vxtAccno1.KeyPress, vxtAccno2.KeyPress, vxtAccno3.KeyPress, vxtAccno4.KeyPress, vxtAccno5.KeyPress, vxtAccno6.KeyPress, _
             vxtOther2.KeyPress, vxtOther3.KeyPress, vxtOther4.KeyPress, vxtOther5.KeyPress, vxtOther6.KeyPress
        If Asc(e.KeyChar) = 13 Then
            SendKeys.Send("{TAB}")
        End If
    End Sub
End Class