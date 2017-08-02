Imports JBC.Printing
Public Class BGP030
    Dim confid, confunit As String, LoadAfter, Dirty As Boolean
    Dim sqlstr As String
    Dim bm, bm2 As BindingManagerBase, mydataset, TempDataSet, userdataset As DataSet
    Dim UserId, UserName, UserUnit As String
    Dim tempAccno As String = ""
    Dim isFirst As Boolean = True
    Dim tempGrade As Integer = 0
    Dim sumBGamt, sumTotper, sumTotuse, sumSubBgamt As Decimal
    Dim myDataview As DataView

    Private Sub BGP030_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        UserId = TransPara.TransP("userid")
        UserUnit = TransPara.TransP("userunit")
        nudYear.Value = GetYear(TransPara.TransP("userdate"))
        If Mid(UserUnit, 1, 2) = "05" Or Mid(UserUnit, 1, 2) = "08" Then
            cboUser.Visible = True
            sqlstr = "SELECT b.staff_no as userid, b.staff_no+USERTABLE.USERNAME as username FROM USERTABLE right outer JOIN" & _
                    "(SELECT STAFF_NO FROM ACCNAME WHERE STAFF_NO IS NOT NULL AND STAFF_NO <> '    ' GROUP BY STAFF_NO) b " & _
                    "ON USERTABLE.USERID = b.STAFF_NO order by usertable.userid"
            userdataset = openmember("", "user", sqlstr)
            If userdataset.Tables("user").Rows.Count = 0 Then
                cboUser.Text = "無user"
            Else
                cboUser.DataSource = userdataset.Tables("user")
                cboUser.ValueMember = "userid"     '欄位值
                cboUser.DisplayMember = "username"  '顯示欄位
            End If
            Dim nr As DataRow
            nr = userdataset.Tables("user").NewRow()
            nr("userid") = "全部"
            nr("username") = "全部"
            userdataset.Tables("user").Rows.Add(nr)
        End If
        vxtStartNo.Text = "1"   '起值
        vxtEndNo.Text = "1"      '迄值
    End Sub

    Private Sub BtnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrint.Click
        Dim intR As Integer = 0  'control record number
        Dim intD, i As Integer
        Dim retstr, UnitName As String
        Dim sum2, sum3, sum4, sum5 As Decimal
        sum2 = 0 : sum3 = 0 : sum4 = 0 : sum5 = 0
        Dim amt2, amt3, amt4, amt5 As Decimal
        amt2 = 0 : amt3 = 0 : amt4 = 0 : amt5 = 0
        Dim printer = New KPrint
        Dim doc As New FPDocument("預算執行統計表列印")
        doc.DefaultPageSettings.PaperKind = Printing.PaperKind.A4
        doc.DefaultPageSettings.Landscape = True
        doc.DefaultFont = New Font("新細明體", 9) '標楷體
        doc.SetDefaultPageMargin(15, 10, 5, 5)   'left top right bottom

        Dim PageRow As Integer = 24

        If Mid(UserUnit, 1, 2) = "05" Or Mid(UserUnit, 1, 2) = "08" Then
            UserId = cboUser.SelectedValue
        End If
        Dim strBg As String = ""
        Dim sSeason As Integer = Season(TransPara.TransP("userDATE")) '判定季份
        Select Case sSeason
            Case Is = 1
                strBg = ", a.bg1+a.up1 as subbgamt "
            Case Is = 2
                strBg = ", a.bg1+a.bg2+a.up1+a.up2 as subbgamt "
            Case Is = 3
                strBg = ", a.bg1+a.bg2+a.bg3+a.up1+a.up2+a.up3 as subbgamt "
            Case Is = 4
                strBg = ", a.bg1+a.bg2+a.bg3+a.bg4+a.up1+a.up2+a.up3+a.up4 as subbgamt "
        End Select
        '丟當年度所有預算科目to mydataset 逐科目列印
        Dim sqlstr, qstr, strD, strC As String
        sqlstr = "SELECT a.accyear,a.accno, " & _
                 "CASE WHEN len(a.accno)=17 THEN c.accname+'－'+b.accname " & _
                     " WHEN len(a.accno)>9 THEN d.accname+'－'+b.accname " & _
                     " WHEN len(a.accno)<=9 THEN b.accname END AS accname, " & _
                     "a.unit, a.bg1+a.bg2+a.bg3+a.bg4+a.bg5+a.up1+a.up2+a.up3+a.up4 as bgamt, " & _
                     " a.totper, a.totUSE, a.ctrl, 'Y' as sumcode "
        sqlstr += strBg
        sqlstr += " FROM bgf010 a INNER JOIN ACCNAME b ON a.ACCNO = b.ACCNO INNER JOIN ACCNAME c ON LEFT(a.ACCNO, 16) = c.ACCNO " & _
         "INNER JOIN ACCNAME d ON LEFT(a.ACCNO, 9) = d.ACCNO WHERE a.accyear=" & nudYear.Value
        If Trim(UserId) <> "全部" Then
            sqlstr += " and b.STAFF_NO = '" & Trim(UserId) & "'"
        End If
        sqlstr += " and a.accno>='" & GetAccno(vxtStartNo.Text) & "' and a.accno<='" & GetAccno(vxtEndNo.Text) & _
                  "' order by a.accno"
        mydataset = openmember("", "BGF010", sqlstr)
        '要統計    'first sum 8 grade
        If rdoSumYes.Checked Then
            Dim cutLen As Integer = 0
            Dim sumGrade As Integer = 0
            With mydataset.Tables(0)
                For intCycle As Integer = 7 To 4 Step -1
                    Select Case intCycle
                        Case 7
                            cutLen = 16
                            tempGrade = 7
                        Case 6
                            cutLen = 9
                            tempGrade = 6
                        Case 5
                            cutLen = 7
                            tempGrade = 5
                        Case 4
                            cutLen = 5
                            tempGrade = 4
                    End Select
                    intR = 0
                    isFirst = True
                    sumBGamt = 0 : sumTotper = 0 : sumTotuse = 0 : sumSubBgamt = 0
                    For intR = 0 To .Rows.Count - 1
                        If .Rows(intR).Item("sumcode") = "Y" And Trim(.Rows(intR).Item("accno")) <> Trim(Mid(.Rows(intR).Item("accno"), 1, cutLen)) Then
                            If isFirst Then
                                tempAccno = Trim(Mid(.Rows(intR).Item("accno"), 1, cutLen))
                                isFirst = False
                            End If
                            If Trim(Mid(.Rows(intR).Item("accno"), 1, cutLen)) <> tempAccno Then
                                If sumBGamt + sumTotper + sumTotuse <> 0 Then '要寫入一筆統計資料
                                    AddSumRecord()
                                    tempAccno = Trim(Mid(.Rows(intR).Item("accno"), 1, cutLen))
                                End If
                            End If
                            sumBGamt += nz(.Rows(intR).Item("bgamt"), 0)
                            sumTotper += nz(.Rows(intR).Item("totper"), 0)
                            sumTotuse += nz(.Rows(intR).Item("totuse"), 0)
                            sumSubBgamt += nz(.Rows(intR).Item("subbgamt"), 0)
                        End If
                    Next
                    If sumBGamt + sumTotper + sumTotuse <> 0 Then 'end 要寫入一筆統計資料
                        AddSumRecord()
                    End If
                Next
            End With
        End If
        myDataview = mydataset.Tables(0).DefaultView
        myDataview.Sort = "accno"

        '找預算單位
        sqlstr = "SELECT b.unitname from usertable a left outer join unittable b on a.userunit=b.unit " & _
                 "where a.userid='" & UserId & "'"
        TempDataSet = openmember("", "user", sqlstr)
        If TempDataSet.Tables("user").Rows.Count = 0 Then
            UnitName = UserId
        Else
            UnitName = nz(TempDataSet.Tables("user").Rows(0).Item("unitname"), "")
        End If
        Dim PageNo As Integer = 0
        intR = 0

        'For intD = 0 To mydataset.Tables("bgf010").Rows.Count - 1
        'intR = 0
        '找資料
        '丟選定之當年度預算科目所有之開支to Grid2 
        'sqlstr = "SELECT bgf020.bgno, bgf020.accyear, BGF020.accno, bgf020.date1, bgf020.date2, bgf020.amt1, bgf020.remark, " & _
        '         "bgf020.amt2, bgf020.amt3, bgf020.useableamt, ACCNAME.ACCNAME AS ACCNAME, bgf020.kind,bgf020.subject, bgf020.closemark, " & _
        '         "bgf030.date3, bgf030.date4, bgf030.useamt, bgf030.no_1_no  " & _
        '         "FROM BGF020 left outer JOIN bgf030 on bgf020.bgno=bgf030.bgno inner join ACCNAME ON BGF020.ACCNO = ACCNAME.ACCNO " & _
        '         " WHERE BGF020.ACCYEAR=" & nudYear.Value & " AND BGF020.accno='" & strAccno & "' ORDER BY BGF020.bgno"
        'mydataset2 =  openmember("", "BGF030", sqlstr)

        For PageCnt As Integer = 1 To 999    '頁次
            Dim page As New FPPage
            Dim text As New FPText(TransPara.TransP("UnitTitle") & nudYear.Value & "年度預算執行統計表", 0, 0)
            text.HAlignment = FPAlignment.Center
            text.Font.Size = 12   '改變文字大小為12點
            page.Add(text)
            Dim text2 As New FPText("預算單位: " & UnitName, 0, 5)
            page.AddText(text2)
            PageNo += 1
            Dim text3 As New FPText("列印日期:" & ShortDate(TransPara.TransP("userdate")) & _
                                    "  頁次:" & PageNo, 210, 5)
            page.AddText(text3)
            Dim table0 As New FPTable(0, 10, 265, 7 * PageRow, PageRow, 7)  '273
            table0.Font.Name = "標楷體"
            table0.Font.Size = 8
            table0.SetLineColor(Color.DarkBlue)
            table0.OutlineThicken(4)
            table0.ColumnStyles(1).Width = 132
            table0.ColumnStyles(2).Width = 22
            table0.ColumnStyles(3).Width = 22
            table0.ColumnStyles(4).Width = 22   '22
            table0.ColumnStyles(5).Width = 22   '22
            table0.ColumnStyles(6).Width = 22   '22
            table0.ColumnStyles(7).Width = 22   '22
            table0.ColumnStyles(1).HAlignment = StringAlignment.Near '整欄左靠
            table0.ColumnStyles(2).HAlignment = StringAlignment.Far  '整欄右靠
            table0.ColumnStyles(3).HAlignment = StringAlignment.Far  '整欄右靠
            table0.ColumnStyles(4).HAlignment = StringAlignment.Far  '整欄右靠
            table0.ColumnStyles(5).HAlignment = StringAlignment.Far  '整欄右靠
            table0.ColumnStyles(6).HAlignment = StringAlignment.Far  '整欄右靠
            table0.ColumnStyles(7).HAlignment = StringAlignment.Far  '整欄右靠
            table0.Texts2D(1, 1).Text = "預算科目及名稱"
            table0.Texts2D(1, 2).Text = "預算總額"
            table0.Texts2D(1, 3).Text = "分配數額"
            table0.Texts2D(1, 4).Text = "請購中金額"
            table0.Texts2D(1, 5).Text = "已開支金額"
            table0.Texts2D(1, 6).Text = "分配數餘額"
            table0.Texts2D(1, 7).Text = "  預算餘額"

            ' With myDataview(intr)       'mydataset.Tables("bgf010")
            For i = 2 To PageRow
                If intR > myDataview.Count - 1 Then
                    table0.Texts2D(i, 1).Text = "    合  計"
                    table0.Texts2D(i, 2).Text = Format(sum2, "###,###,###,###.#")
                    table0.Texts2D(i, 3).Text = Format(sum3, "###,###,###,###.#")
                    table0.Texts2D(i, 4).Text = Format(sum4, "###,###,###,###.#")
                    table0.Texts2D(i, 5).Text = Format(sum5, "###,###,###,###.#")
                    table0.Texts2D(i, 6).Text = Format(sum3 - sum4 - sum5, "###,###,###,###.#")
                    table0.Texts2D(i, 7).Text = Format(sum2 - sum4 - sum5, "###,###,###,###.#")
                    PageCnt = 1000
                    Exit For
                End If
                amt2 = nz(myDataview(intR).Item("bgamt"), 0)     '預算總額
                amt3 = nz(myDataview(intR).Item("subbgamt"), 0)  '分配數
                amt4 = nz(myDataview(intR).Item("totper"), 0)    '請購中
                amt5 = nz(myDataview(intR).Item("totuse"), 0)    '已開支
                If Len(nz(myDataview(intR)("accno"), "")) = 5 Then
                    sum2 += amt2
                    sum3 += amt3
                    sum4 += amt4
                    sum5 += amt5
                End If
                table0.Texts2D(i, 1).Text = nz(myDataview(intR)("accno"), "") & nz(myDataview(intR)("accname"), "")
                table0.Texts2D(i, 2).Text = Format(amt2, "###,###,###,###.#")
                table0.Texts2D(i, 3).Text = Format(amt3, "###,###,###,###.#")
                table0.Texts2D(i, 4).Text = Format(amt4, "###,###,###,###.#")
                table0.Texts2D(i, 5).Text = Format(amt5, "###,###,###,###.#")
                table0.Texts2D(i, 6).Text = Format(amt3 - amt4 - amt5, "###,###,###,###.#")
                table0.Texts2D(i, 7).Text = Format(amt2 - amt4 - amt5, "###,###,###,###.#")
                'table0.Texts2D(i, 3).Font.Size = 10
                intR += 1
            Next
            'End With
            Dim text1 As New FPText("製表" & Space(32) & "股長" & Space(32) & "組室主管" & Space(32) & "主計主任" & Space(32) & "總幹事" & Space(32) & "會長", 0, 180)
            text1.Font.Size = 11   '改變文字大小為12點
            page.Add(text1)

            page.Add(table0)
            doc.AddPage(page)
        Next
        'Next

        printer.Document = doc
        printer.IsAutoShowPrintDialog = True '自動顯示印表機設定對話盒
        If rdoPreview.Checked Then
            printer.IsAutoShowPrintPreviewDialog = True
        End If
        printer.Print()
        Me.Close()
    End Sub

    Function AddSumRecord()
        sqlstr = "SELECT * from accname where accno='" & tempAccno & "'"
        TempDataSet = openmember("", "user", sqlstr)
        If TempDataSet.Tables(0).Rows.Count > 0 And Grade(tempAccno) = tempGrade Then
            '有該統計科目時才寫入統計資料
            Dim nr As DataRow
            nr = mydataset.Tables(0).NewRow()
            nr("accyear") = nudYear.Value
            nr("accno") = tempAccno
            nr("accname") = nz(TempDataSet.Tables(0).Rows(0).Item("accname"), "")
            nr("bgamt") = sumBGamt
            nr("totper") = sumTotper
            nr("totuse") = sumTotuse
            nr("ctrl") = ""
            nr("sumcode") = " "
            nr("subbgamt") = sumSubBgamt
            mydataset.Tables(0).Rows.Add(nr) '增行至dataset
        End If
        sumBGamt = 0 : sumTotper = 0 : sumTotuse = 0 : sumSubBgamt = 0
    End Function

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class
