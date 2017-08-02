Imports JBC.Printing
Imports System.IO
Imports Microsoft.Office.Interop
Public Class ACY130
    Dim confid, confunit As String, LoadAfter, Dirty As Boolean
    Dim SYear, SNo, ENo As Integer
    Dim Edate, Sdate As String
    Dim myds As DataSet
    Dim sqlstr As String

    Private Sub ACY130_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '找當年度
        sqlstr = "SELECT max(accyear) as accyear FROM acf050"
        myds = openmember("", "acf050", sqlstr)
        If myds.Tables("acf050").Rows.Count > 0 Then
            nudYear.Value = myds.Tables("acf050").Rows(0).Item("accyear")
        End If
    End Sub

    Private Sub BtnSure_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSure.Click
        SYear = nudYear.Value
        If SYear <= 0 Then
            Exit Sub
        End If
        BtnSure.Enabled = False

        LoadGridFunc()   '產生並計算轉帳傳票資料至ACM020

        SNo = QueryNO(SYear, "3") + 1   '取得結帳分錄轉帳傳票製票編號
        ENo = QueryNO(SYear, "6") + 1   '取得結帳分錄轉帳傳票轉帳編號
        Sdate = SYear + 1911 + 1 & "/1/1"
        Edate = SYear + 1911 & "/12/31"

        '列印結帳分錄 
        sqlstr = "SELECT * FROM ACM020 order by accno"
        myds = openmember("", "ACM020", sqlstr)
        PrintTransSlip("1", Edate, SNo, ENo, TransPara.TransP("UnitTitle")) '結帳分錄--收支結轉本期餘絀
        PrintTransSlip("2", Edate, SNo + 1, ENo + 1, TransPara.TransP("UnitTitle")) '結帳分錄--實帳戶結轉下期
        PrintTransSlip("3", Sdate, 1, 1, TransPara.TransP("UnitTitle"))   '開帳分錄--實帳戶由上期轉入
    End Sub

    Private Sub LoadGridFunc()
        Dim sqlstr, retstr, tempStr As String
        Dim mm As String = "12"  '本月

        '先清空ACM020 
        sqlstr = "delete from ACM020"
        retstr = runsql(mastconn, sqlstr)

        '將acf050四級科目丟入ACM020
        sqlstr = "INSERT INTO ACM020  SELECT a.ACCNO, b.ACCNAME, " & _
                 " a.DEAMT" & mm & " AS AMT1, a.CRAMT" & mm & " AS AMT2, 0 as amt3, ' ' AS DC " & _
                 "FROM ACF050 a left outer join ACCNAME b ON a.ACCNO = b.ACCNO " & _
                 "WHERE a.ACCYEAR = " & SYear & " AND LEN(a.ACCNO) = 5 AND a.ACCNO<>'32301'"
        retstr = runsql(mastconn, sqlstr)

        '各欄數值計算(年底餘額)
        sqlstr = "Update ACM020 SET AMT1 = AMT1 - AMT2, AMT2 = 0 WHERE AMT1 > AMT2"
        retstr = runsql(mastconn, sqlstr)
        sqlstr = "Update ACM020 SET AMT2 = AMT2 - AMT1, AMT1 = 0 WHERE AMT2 > AMT1"
        retstr = runsql(mastconn, sqlstr)
        sqlstr = "Update ACM020 SET AMT2 = 0, AMT1 = 0 WHERE AMT2 = AMT1"
        retstr = runsql(mastconn, sqlstr)

        sqlstr = "delete from ACM020 where AMT2=0 and AMT1=0 "  'delete 餘額=0
        retstr = runsql(mastconn, sqlstr)
        sqlstr = "select sum(AMT1) as AMT1, sum(AMT2) as AMT2 from ACM020 where left(accno,1)>='4'"
        myds = openmember("", "ACM020", sqlstr)

        '計算本期餘絀
        Dim Tot4, Tot5 As Decimal
        If myds.Tables("ACM020").Rows.Count > 0 Then
            Tot5 = nz(myds.Tables("ACM020").Rows(0).Item("AMT1"), 0)   'debit 
            Tot4 = nz(myds.Tables("ACM020").Rows(0).Item("AMT2"), 0)   'credit  
            If Tot5 > Tot4 Then sqlstr = "insert into ACM020 (ACCNO, accname,AMT1, AMT2, AMT3, DC) values (" & _
               "'32301', '本期餘絀', " & Tot5 - Tot4 & ", 0, 0, '1')" '短絀
            If Tot4 > Tot5 Then sqlstr = "insert into ACM020 (ACCNO, accname, AMT1, AMT2, AMT3, DC) values (" & _
                "'32301', '本期餘絀', 0, " & Tot4 - Tot5 & ", 0, '2')" '結餘
            retstr = runsql(mastconn, sqlstr)
        End If
        sqlstr = "update acm020 set dc='1', amt3=amt1-amt2 where amt1>amt2"  'debit dc='1'
        retstr = runsql(mastconn, sqlstr)
        sqlstr = "update acm020 set dc='2', amt3=amt2-amt1 where amt2>amt1"  'credit dc='2'
        retstr = runsql(mastconn, sqlstr)
    End Sub

    '列印轉帳傳票
    Private Sub PrintTransSlip(ByVal kind As String, ByVal sdate As Date, ByVal No1 As Integer, ByVal No2 As Integer, ByVal orgName As String)
        Dim printer = New KPrint
        Dim doc As FPDocument
        Dim page As FPPage
        Dim intI, intJ, I As Integer
        Dim intTotDebit, intTotCredit As Decimal
        Dim J As Integer = 0
        Dim LastJ As Integer = 0   '記錄上頁筆數
        Dim strI, retstr, strRemark As String
        If kind = "1" Then strRemark = "結轉本期餘絀"
        If kind = "2" Then strRemark = "結轉下年度"
        If kind = "3" Then strRemark = "由上年度轉入"

        ' 先load data to dataset 
        If kind = "1" Then   '結轉本期餘絀
            '先要將32301.dc 借貸要相反,order才正確
            sqlstr = "select dc from acm020 where accno='32301'"
            myds = openmember("", "acm020", sqlstr)
            If myds.Tables("acm020").Rows.Count > 0 Then
                If myds.Tables("acm020").Rows(0).Item("dc") = "1" Then
                    sqlstr = "update acm020 set dc='2' where accno='32301'"
                Else
                    sqlstr = "update acm020 set dc='1' where accno='32301'"
                End If
                retstr = runsql(mastconn, sqlstr)
            End If
            '先計算借貸方合計數
            sqlstr = "select dc, sum(amt3) as amt from acm020 where left(accno,1)>='4' or accno='32301'" & _
                     " group by dc"
            myds = openmember("", "acm020", sqlstr)
            For intI = 0 To myds.Tables("acm020").Rows.Count - 1
                If myds.Tables("acm020").Rows(intI).Item("dc") = "1" Then
                    intTotCredit = nz(myds.Tables("acm020").Rows(intI).Item("amt"), 0)
                Else
                    intTotDebit = nz(myds.Tables("acm020").Rows(intI).Item("amt"), 0)
                End If
            Next
            sqlstr = "SELECT * from acM020 where left(accno,1)>='4' or accno='32301'" & _
                     " order by dc DESC, accno"
        End If
        If kind = "2" Then  '結轉下年度
            '先要將32301.dc 借貸要正確
            sqlstr = "select * from acm020 where accno='32301'"
            myds = openmember("", "acm020", sqlstr)
            If myds.Tables("acm020").Rows.Count > 0 Then
                If myds.Tables("acm020").Rows(0).Item("amt1") > myds.Tables("acm020").Rows(0).Item("amt2") Then
                    sqlstr = "update acm020 set dc='1' where accno='32301'"
                Else
                    sqlstr = "update acm020 set dc='2' where accno='32301'"
                End If
                retstr = runsql(mastconn, sqlstr)
            End If
            '先計算借貸方合計數
            sqlstr = "select dc, sum(amt3) as amt from acm020 where left(accno,1)<='3'" & _
                     " group by dc"
            myds = openmember("", "acm020", sqlstr)
            For intI = 0 To myds.Tables("acm020").Rows.Count - 1
                If myds.Tables("acm020").Rows(intI).Item("dc") = "1" Then
                    intTotCredit = nz(myds.Tables("acm020").Rows(intI).Item("amt"), 0)
                Else
                    intTotDebit = nz(myds.Tables("acm020").Rows(intI).Item("amt"), 0)
                End If
            Next
            sqlstr = "SELECT * from acM020 where left(accno,1)<='3' order by dc DESC, accno"
        End If
        If kind = "3" Then   '由上年度轉入
            '先計算借貸方合計數
            sqlstr = "select dc, sum(amt3) as amt from acm020 where left(accno,1)<='3'" & _
                     "group by dc"
            myds = openmember("", "acm020", sqlstr)
            For intI = 0 To myds.Tables("acm020").Rows.Count - 1
                If myds.Tables("acm020").Rows(intI).Item("dc") = "1" Then
                    intTotDebit = nz(myds.Tables("acm020").Rows(intI).Item("amt"), 0)
                Else
                    intTotCredit = nz(myds.Tables("acm020").Rows(intI).Item("amt"), 0)
                End If
            Next
            sqlstr = "SELECT * from acM020 where left(accno,1)<='3' order by dc, accno"
        End If
        myds = openmember("", "acm020", sqlstr)
        If myds.Tables("acm020").Rows.Count <= 0 Then
            Exit Sub
        End If

        '置於明細帳
        Dim allDoc As New FPDocument("轉帳傳票")
        allDoc.DefaultPageSettings.PaperKind = Printing.PaperKind.B5
        allDoc.SetDefaultPageMargin(15, 25, 0, 0)    'SET MARGIN LEFT TOP RIGHT BOTTON
        allDoc.DefaultPageSettings.Landscape = True
        Dim strDC As String = "1"  'first to print 轉帳借方 
        Dim strTempDC As String = myds.Tables("acm020").Rows(0).Item("dc")
        Dim intPage As Integer = 0
        For I = 0 To 999
            intPage += 1
            doc = GetTransSlipDoc()   '取得空白的轉帳傳票
            page = doc.GetPage(0)
            allDoc.InsertDocument(doc) '將空白的轉帳傳票加入總文件
            '設定空白收入傳票上的機關名稱
            page.GetText("機關名稱").Text = orgName
            page.GetText("轉帳種類").Text = IIf(strDC = "1", "借 方", "貸 方")
            '設定第0個table中,與製票相關的屬性
            page.GetTable(0).GetText("轉帳年").Text = FormatNumber(GetYear(sdate) - 1911, 0)
            page.GetTable(0).GetText("轉帳月").Text = Month(sdate)
            page.GetTable(0).GetText("轉帳日").Text = Microsoft.VisualBasic.DateAndTime.Day(sdate)
            page.GetTable(0).GetText("轉帳編號").Text = No1 & "-" & intPage   '標別頁次
            'page.GetTable(1).Texts2D(2, 1).Text = "轉帳編號：轉 字第　　　　　　　 號"
            page.GetTable(1).Texts2D(2, 1).Text = "轉帳編號：轉 字第   " & Str(No2) & "-" & intPage & " 　號"


            '設定第2個table總帳項不印
            If intPage = 1 Then
                If strDC = "1" Then
                    page.GetTable(2).Texts2D(2, 3).Text = "轉帳借方合計"   '總帳項摘要
                    page.GetTable(2).Texts2D(2, 4).Text = FormatNumber(intTotDebit, 2)
                Else
                    page.GetTable(2).Texts2D(2, 3).Text = "轉帳貸方合計"   '總帳項摘要
                    page.GetTable(2).Texts2D(2, 4).Text = FormatNumber(intTotCredit, 2)
                End If
            End If

            intJ = 0  '控制該頁項次
            With myds.Tables("acm020")
                For J = LastJ To .Rows.Count - 1
                    If .Rows(J)("dc") <> strTempDC Then
                        strTempDC = .Rows(J)("dc")
                        intPage = 0
                        strDC = "2" 'and then to print 轉帳貸方
                        Exit For
                    End If
                    If intJ > 4 Then Exit For
                    'intI = CType(intJ + 3, String)   '明細項由第三項開始
                    page.GetTable(2).Texts2D(intJ + 3, 2).Text = FormatAccno(.Rows(J)("accno")) & vbCrLf & .Rows(J)("accname") '"在此放入總帳科目"
                    page.GetTable(2).Texts2D(intJ + 3, 3).Text = strRemark
                    page.GetTable(2).Texts2D(intJ + 3, 4).Text = FormatNumber(.Rows(J)("amt3"), 2)
                    intJ += 1
                Next
                LastJ = J    '記錄下頁第一筆
                If LastJ > .Rows.Count - 1 Then I = 1000 'eof 
            End With
        Next
        printer.Document = allDoc
        'If TransPara.TransP("Print") = "Preview" Then printer.IsAutoShowPrintPreviewDialog = True
        If rdoPreview.Checked Then printer.IsAutoShowPrintPreviewDialog = True
        printer.Print()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class
