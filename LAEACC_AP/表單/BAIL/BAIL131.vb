Imports System.Globalization
Imports JBC.Printing
Imports System.IO
Imports Microsoft.Office.Interop
Public Class BAIL131
    Dim LoadAfter, Dirty As Boolean
    Dim sqlstr As String
    Dim mydataset As DataSet
    Dim tt1, tt2 As String
    Dim mypath As String
    Dim i, j As Integer
    Dim BG_ADD1, BG_SUB1, BG_ADD2, BG_SUB2, TOT_UP1, TOT_ADD1, TOT_SUB1, TOT_UP2, TOT_ADD2, TOT_SUB2 As Integer
    Dim BG_ADD11, BG_SUB11, TOT_UP11, TOT_ADD11, TOT_SUB11 As Integer
    Dim rp As String, engno As String, kind As String, amt As Integer, rpdate As Date, engname As String, cop As String
    Dim INCOME, PAY, UP_IN, UP_PAY As Integer, jname As String
    Dim chkno, bank As String
    Dim amt1, amt2, amt3, amt4, amt5, amt6, amt7, amt8 As Integer
    Dim strYY As String ', strYYbef As String
    Dim strEngno As String, strKind As String, strengname As String, strbank As String, strcop As String, strchkno As String, strdate As String, tempdate As Date
    Dim FirstRec As Boolean
    Dim table1 As DataTable
    Dim dv As DataView

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrt.Click

        Dim xlapp As Excel.Application
        Dim xlbook As Excel.Workbook
        Dim xlbooks As Excel.Workbooks
        Dim xlsheet As Excel.Worksheet
        Dim xlsheets As Excel.Sheets
        Dim xlRange As Excel.Range
        Dim xlRange1 As Excel.Range
        Dim xlRange2 As Excel.Range
        Dim xlCells As Excel.Range
        Try
            i = 0 : j = 0
            TOT_UP1 = 0 : TOT_ADD1 = 0 : TOT_SUB1 = 0 : TOT_UP2 = 0 : TOT_ADD2 = 0 : TOT_SUB2 = 0
            rp = "" : engno = "" : kind = "" : amt = 0 : engname = "" : cop = "" : jname = ""
            INCOME = 0 : PAY = 0 : UP_IN = 0 : UP_PAY = 0
            chkno = "" : bank = "" : strYY = ""
            amt1 = 0 : amt2 = 0 : amt3 = 0 : amt4 = 0 : amt5 = 0 : amt6 = 0 : amt7 = 0 : amt8 = 0
            strEngno = "" : strKind = "" : strengname = "" : strbank = "" : strcop = "" : strchkno = ""

            FirstRec = True
            BG_ADD1 = Val(txtBgAdd1.Text)
            BG_SUB1 = Val(txtBgSub1.Text)
            BG_ADD2 = Val(txtBgAdd2.Text)
            BG_SUB2 = Val(txtBgSub2.Text)
            strYY = Trim(Str(Val(txtYY.Text) + 1911))
            table1 = createTable("bail020")
            '保管品
            sqlstr = "SELECT bailf020.engno as engno,bailf020.kind as kind,bailf020.rpdate as rpdate ,bailf020.chkno as chkno,bailf020.rp as rp,bailf020.amt as amt,bailf020.bank as bank,enf010.engname as engname,enf010.cop as cop  FROM bailf020 inner join enf010 on bailf020.engno=enf010.engno where (balance is null or balance='') order by bailf020.engno,bailf020.kind,bailf020.bank,bailf020.chkno"
            mydataset = openmember("", "bailf020", sqlstr)
            For i = 0 To mydataset.Tables("bailf020").Rows.Count - 1
                'pBar1.PerformStep()
                rp = nz(mydataset.Tables("bailf020").Rows(i).Item("rp"), "")
                kind = nz(mydataset.Tables("bailf020").Rows(i).Item("kind"), "")
                amt = nz(mydataset.Tables("bailf020").Rows(i).Item("amt"), 0)
                rpdate = nz(mydataset.Tables("bailf020").Rows(i).Item("rpdate"), "1911/1/1")
                engno = nz(mydataset.Tables("bailf020").Rows(i).Item("engno"), "")
                engname = nz(mydataset.Tables("bailf020").Rows(i).Item("engname"), "")
                chkno = Trim(nz(mydataset.Tables("bailf020").Rows(i).Item("chkno"), ""))
                bank = Trim(nz(mydataset.Tables("bailf020").Rows(i).Item("bank"), ""))
                If Not IsDBNull(mydataset.Tables("bailf020").Rows(i).Item("cop")) Then
                    cop = mydataset.Tables("bailf020").Rows(i).Item("cop")
                    If cop.Length > 4 Then
                        cop = cop.Substring(0, 4)
                    End If
                Else
                    cop = ""
                End If
                'If engname.Length > 20 Then
                '    engname = engname.Substring(0, 20)
                'End If

                If rpdate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture) > strYY & "/12/31" Then
                Else

                    If FirstRec Then
                        strEngno = engno
                        strKind = kind
                        strengname = engname
                        strbank = bank
                        strcop = cop
                        strchkno = chkno
                        tempdate = rpdate
                        FirstRec = False
                    End If
                    If rdbcourse1.Checked Then
                        If engno <> strEngno Or kind <> strKind Then
                            If INCOME - PAY = 0 And UP_IN - UP_PAY = 0 Then
                                INCOME = 0
                                PAY = 0
                                UP_IN = 0
                                UP_PAY = 0
                            Else
                                Call Bail020List()
                            End If
                            'INCOME = 0
                            'PAY = 0
                            strEngno = engno
                            strKind = kind
                            strengname = engname
                            strbank = bank
                            strcop = cop
                            strchkno = chkno
                            tempdate = rpdate
                        End If
                    Else
                        If engno <> strEngno Or kind <> strKind Or bank <> strbank Or chkno <> strchkno Then
                            If INCOME - PAY = 0 And UP_IN - UP_PAY = 0 Then
                                INCOME = 0
                                PAY = 0
                                UP_IN = 0
                                UP_PAY = 0
                            Else
                                Call Bail020List()
                            End If
                            'INCOME = 0
                            'PAY = 0
                            strEngno = engno
                            strKind = kind
                            strengname = engname
                            strbank = bank
                            strcop = cop
                            strchkno = chkno
                            tempdate = rpdate
                        End If
                    End If
                    If tempdate > rpdate Then
                        tempdate = rpdate             '取最小日期 
                    End If

                    If rp = "1" Then
                        INCOME = INCOME + amt
                    Else
                        PAY = PAY + amt
                    End If
                    If rpdate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture) < strYY & "/01/01" Then     '        '上一年度
                        If rp = "1" Then
                            UP_IN = UP_IN + amt
                        Else
                            UP_PAY = UP_PAY + amt
                        End If
                    End If

                End If
                'If strbank <> bank And strchkno <> chkno Then
                '   strBnkChkno &= " " & bank.Trim & "(" & chkno.Trim & ")"
                'End If

                '960122 mark test
                'If rp = "1" Then
                '    INCOME = INCOME + amt
                'Else
                '    PAY = PAY + amt
                'End If
            Next
            'If mydataset.Tables("bailf020").Rows.Count > 0 And table1.Rows.Count > 0 Then
            '    Call Bail020List()
            'End If
            'xlapp = CreateObject("excel.application")
            mypath = Application.StartupPath
            Try
                tt1 = "c:\App\bail\ReportData\bailf100sample.xls" 'mypath + "\bailf100sample.xls"
                tt2 = "c:\App\bail\Report\bailf130.xls" ' mypath + "\bailf100.xls"
                FileCopy(tt1, tt2)
                xlapp = CreateObject("Excel.Application")
                xlapp.DisplayAlerts = False  '隱藏所有警告或確認訊息視窗,否則某些指令會失敗,譬如刪除工作表
                xlbooks = xlapp.Workbooks
                xlbook = xlbooks.Open(tt2)   '開啟tt2
            Catch ex As Exception
                MsgBox("報表產生錯誤，請洽程式設計人員!", , "保證金系統")
                Exit Sub
            End Try

            xlsheets = xlbook.Worksheets
            xlsheet = xlsheets(1)
            NAR(xlCells)
            xlCells = xlsheet.Cells

            xlRange = xlCells(1, 1)
            xlRange.Value = TransPara.TransP("UnitTitle")
            NAR(xlRange)

            xlRange = xlCells(2, 1)
            xlRange.Value = "雜項資產增減餘額明細表"
            NAR(xlRange)

            sqlstr = "SELECT a.*, b.ACCNAME AS accname " & _
                     "FROM (SELECT * FROM ACF050 WHERE (ACCNO LIKE '15301%') AND (ACCYEAR =" & txtYY.Text & ")) a " & _
                     "LEFT OUTER JOIN ACCNAME b ON a.ACCNO = b.ACCNO ORDER BY  a.ACCNO"
            mydataset = openmember("", "bailf010", sqlstr)
            With mydataset.Tables("bailf010")
                'pBar1.Visible = True
                'pBar1.Minimum = 1
                'pBar1.Maximum = .Rows.Count - 1
                'pBar1.Value = 1
                'pBar1.Step = 1
                For i = 0 To .Rows.Count - 1
                    'pBar1.PerformStep()
                    If nz(.Rows(i).Item("accno"), "") = "15301" Then
                        TOT_UP1 = Val(nz(.Rows(i).Item("beg_debit"), "")) - Val(nz(.Rows(i).Item("beg_credit"), ""))
                        TOT_ADD1 = Val(nz(.Rows(i).Item("deamt12"), "")) - TOT_UP1
                        TOT_SUB1 = Val(nz(.Rows(i).Item("cramt12"), ""))
                        xlRange = xlCells(3, 1)
                        xlRange.Value = "科目編號:1-53"
                        NAR(xlRange)

                        xlRange = xlCells(3, 2)
                        'xlRange.Value = "              中華民國" & txtYY.Text & "年01月01日起至" & txtYY.Text & "年12月31日止"
                        xlRange.Value = "                     中華民國" & txtYY.Text & "年度"
                        NAR(xlRange)
                        xlRange = xlCells(6, 1)
                        xlRange.Value = "1-5301 存出保證金"
                        NAR(xlRange)
                        xlRange = xlCells(6, 2)
                        xlRange.Value = TOT_UP1  '上年度決算結轉數
                        NAR(xlRange)
                        xlRange = xlCells(6, 3)
                        xlRange.Value = TOT_ADD1
                        NAR(xlRange)
                        xlRange = xlCells(6, 4)
                        xlRange.Value = BG_ADD1
                        NAR(xlRange)
                        xlRange = xlCells(6, 5)
                        xlRange.Value = TOT_ADD1 - BG_ADD1
                        NAR(xlRange)
                        xlRange = xlCells(6, 6)
                        xlRange.Value = TOT_SUB1
                        NAR(xlRange)
                        xlRange = xlCells(6, 7)
                        xlRange.Value = BG_SUB1
                        NAR(xlRange)
                        xlRange = xlCells(6, 8)
                        xlRange.Value = TOT_SUB1 - BG_SUB1
                        NAR(xlRange)
                        xlRange = xlCells(6, 9)
                        xlRange.Value = TOT_UP1 + TOT_ADD1 - TOT_SUB1
                        NAR(xlRange)

                        'xlsheet.Cells(3, 1).value = "科目符號:1-53"
                        'xlsheet.Cells(3, 2).value = "              中華民國" & txtYY.Text & "年01月01日起至" & txtYY.Text & "年12月31日止"
                        'xlsheet.Cells(6, 1).value = "1-5301 存出保證金"
                        'xlsheet.Cells(6, 2).value = TOT_UP1 '上年度決算結轉數
                        'xlsheet.Cells(6, 3).value = TOT_ADD1
                        'xlsheet.Cells(6, 4).value = BG_ADD1
                        'xlsheet.Cells(6, 5).value = TOT_ADD1 - BG_ADD1
                        'xlsheet.Cells(6, 6).value = TOT_SUB1
                        'xlsheet.Cells(6, 7).value = BG_SUB1
                        'xlsheet.Cells(6, 8).value = TOT_SUB1 - BG_SUB1
                        'xlsheet.Cells(6, 9).value = TOT_UP1 + TOT_ADD1 - TOT_SUB1
                    Else
                        TOT_UP11 = Val(nz(.Rows(i).Item("beg_debit"), "")) - Val(nz(.Rows(i).Item("beg_credit"), ""))
                        TOT_ADD11 = Val(nz(.Rows(i).Item("deamt12"), "")) - TOT_UP11
                        TOT_SUB11 = Val(nz(.Rows(i).Item("cramt12"), ""))
                        If BG_ADD1 > 0 Then
                            If BG_ADD1 > TOT_ADD11 Then
                                BG_ADD11 = TOT_ADD11               '預算增加
                            Else
                                BG_ADD11 = BG_ADD1
                            End If
                        End If
                        If BG_SUB1 > 0 Then
                            If BG_SUB1 > TOT_SUB11 Then
                                BG_SUB11 = TOT_SUB11           '預算減少
                            Else
                                BG_SUB11 = BG_SUB1
                            End If
                        End If
                        xlRange = xlCells(i + 6, 1)
                        xlRange.Value = nz(.Rows(i).Item("accname"), "")
                        NAR(xlRange)
                        xlRange = xlCells(i + 6, 2)
                        xlRange.Value = TOT_UP11 '上年度決算結轉數
                        NAR(xlRange)
                        xlRange = xlCells(i + 6, 3)
                        xlRange.Value = TOT_ADD11
                        NAR(xlRange)
                        xlRange = xlCells(i + 6, 4)
                        xlRange.Value = BG_ADD11
                        NAR(xlRange)
                        xlRange = xlCells(i + 6, 5)
                        xlRange.Value = TOT_ADD11 - BG_ADD11
                        NAR(xlRange)
                        xlRange = xlCells(i + 6, 6)
                        xlRange.Value = TOT_SUB11
                        NAR(xlRange)
                        xlRange = xlCells(i + 6, 7)
                        xlRange.Value = BG_SUB11
                        NAR(xlRange)
                        xlRange = xlCells(i + 6, 8)
                        xlRange.Value = TOT_SUB11 - BG_SUB11
                        NAR(xlRange)
                        xlRange = xlCells(i + 6, 9)
                        xlRange.Value = TOT_UP11 + TOT_ADD11 - TOT_SUB11
                        NAR(xlRange)
                        'xlsheet.Cells(i + 6, 1).value = "科目符號:1-53"
                        'xlsheet.Cells(i + 6, 2).value = "              中華民國" & txtYY.Text & "年01月01日起至" & txtYY.Text & "年12月31日止"
                        'xlsheet.Cells(i + 6, 1).value = nz(.Rows(i).Item("accname"), "")
                        'xlsheet.Cells(i + 6, 2).value = TOT_UP11 '上年度決算結轉數
                        'xlsheet.Cells(i + 6, 3).value = TOT_ADD11
                        'xlsheet.Cells(i + 6, 4).value = BG_ADD11
                        'xlsheet.Cells(i + 6, 5).value = TOT_ADD11 - BG_ADD11
                        'xlsheet.Cells(i + 6, 6).value = TOT_SUB11
                        'xlsheet.Cells(i + 6, 7).value = BG_SUB11
                        'xlsheet.Cells(i + 6, 8).value = TOT_SUB11 - BG_SUB11
                        'xlsheet.Cells(i + 6, 9).value = TOT_UP11 + TOT_ADD11 - TOT_SUB11
                    End If
                Next
            End With
            xlRange = xlCells(i + 6, 1)
            xlRange.Value = "1-5303 保管品"
            NAR(xlRange)
            xlRange = xlCells(i + 6, 2)
            xlRange.Value = TOT_UP2
            NAR(xlRange)
            xlRange = xlCells(i + 6, 3)
            xlRange.Value = TOT_ADD2
            NAR(xlRange)
            xlRange = xlCells(i + 6, 4)
            xlRange.Value = BG_ADD2
            NAR(xlRange)
            xlRange = xlCells(i + 6, 5)
            xlRange.Value = TOT_ADD2 - BG_ADD2
            NAR(xlRange)
            xlRange = xlCells(i + 6, 6)
            xlRange.Value = TOT_SUB2
            NAR(xlRange)
            xlRange = xlCells(i + 6, 7)
            xlRange.Value = BG_SUB2
            NAR(xlRange)
            xlRange = xlCells(i + 6, 8)
            xlRange.Value = TOT_SUB2 - BG_SUB2
            NAR(xlRange)
            xlRange = xlCells(i + 6, 9)
            xlRange.Value = TOT_UP2 + TOT_ADD2 - TOT_SUB2
            NAR(xlRange)
            'xlsheet.Cells(i + 6, 1).value = "1-5303 保管品"
            'xlsheet.Cells(i + 6, 2).value = TOT_UP2
            'xlsheet.Cells(i + 6, 3).value = TOT_ADD2
            'xlsheet.Cells(i + 6, 4).value = BG_ADD2
            'xlsheet.Cells(i + 6, 5).value = TOT_ADD2 - BG_ADD2
            'xlsheet.Cells(i + 6, 6).value = TOT_SUB2
            'xlsheet.Cells(i + 6, 7).value = BG_SUB2
            'xlsheet.Cells(i + 6, 8).value = TOT_SUB2 - BG_SUB2
            'xlsheet.Cells(i + 6, 9).value = TOT_UP2 + TOT_ADD2 - TOT_SUB2
            ' Display the ProgressBar control.
            pBar1.Visible = True
            ' Set Minimum to 1 to represent the first file being copied.
            pBar1.Minimum = 0
            ' Set Maximum to the total number of files to copy.
            If table1.Rows.Count > 0 Then
                pBar1.Maximum = table1.Rows.Count - 1
            End If
            ' Set the initial value of the ProgressBar.
            pBar1.Value = 0
            ' Set the Step property to a value of 1 to represent each file being copied.
            pBar1.Step = 1
            dv = table1.DefaultView
            If chkEngno_4.Checked Then
                dv.Sort = "engno_4,engno"
            End If
            For j = 0 To dv.Count - 1
                pBar1.PerformStep()
                xlRange = xlCells(j + i + 7, 1)
                xlRange.Value = dv(j).Item("jname") 'debts1.Item(j).jname
                NAR(xlRange)
                xlRange = xlCells(j + i + 7, 2)
                xlRange.Value = dv(j).Item("amt1") 'debts1.Item(j).amt1  '上年度結轉
                NAR(xlRange)
                xlRange = xlCells(j + i + 7, 3)
                xlRange.Value = dv(j).Item("amt2")  'debts1.Item(j).amt2   '本年度增加 決算
                NAR(xlRange)
                xlRange = xlCells(j + i + 7, 4)
                xlRange.Value = dv(j).Item("amt3")  'debts1.Item(j).amt3    '           預算
                NAR(xlRange)
                xlRange = xlCells(j + i + 7, 5)
                xlRange.Value = dv(j).Item("amt4")  'debts1.Item(j).amt4   '           比較
                NAR(xlRange)
                xlRange = xlCells(j + i + 7, 6)
                xlRange.Value = dv(j).Item("amt5")   'debts1.Item(j).amt5    '本年度減少 決算
                NAR(xlRange)
                xlRange = xlCells(j + i + 7, 7)
                xlRange.Value = dv(j).Item("amt6")   'debts1.Item(j).amt6   '           比較
                NAR(xlRange)
                xlRange = xlCells(j + i + 7, 8)
                xlRange.Value = dv(j).Item("amt7")   'debts1.Item(j).amt7   '           比較
                NAR(xlRange)
                xlRange = xlCells(j + i + 7, 9)
                xlRange.Value = dv(j).Item("amt8")   'debts1.Item(j).amt8   '本年底餘額
                NAR(xlRange)
                'xlsheet.Cells(j + i + 7, 1).value = debts1.Item(j).jname
                'xlsheet.Cells(j + i + 7, 2).value = debts1.Item(j).amt1
                'xlsheet.Cells(j + i + 7, 3).value = debts1.Item(j).amt2
                'xlsheet.Cells(j + i + 7, 4).value = debts1.Item(j).amt3
                'xlsheet.Cells(j + i + 7, 5).value = debts1.Item(j).amt4
                'xlsheet.Cells(j + i + 7, 6).value = debts1.Item(j).amt5
                'xlsheet.Cells(j + i + 7, 7).value = debts1.Item(j).amt6
                'xlsheet.Cells(j + i + 7, 8).value = debts1.Item(j).amt7
                'xlsheet.Cells(j + i + 7, 9).value = debts1.Item(j).amt8
            Next
            xlRange = xlCells(j + i + 7, 1)
            xlRange.Value = "    合  計"
            NAR(xlRange)
            xlRange = xlCells(j + i + 7, 2)
            xlRange.Value = TOT_UP1 + TOT_UP2
            NAR(xlRange)
            xlRange = xlCells(j + i + 7, 3)
            xlRange.Value = TOT_ADD1 + TOT_ADD2
            NAR(xlRange)
            xlRange = xlCells(j + i + 7, 4)
            xlRange.Value = BG_ADD1 + BG_ADD2
            NAR(xlRange)
            xlRange = xlCells(j + i + 7, 5)
            xlRange.Value = (TOT_ADD1 - BG_ADD1) + (TOT_ADD2 - BG_ADD2)
            NAR(xlRange)
            xlRange = xlCells(j + i + 7, 6)
            xlRange.Value = TOT_SUB1 + TOT_SUB2
            NAR(xlRange)
            xlRange = xlCells(j + i + 7, 7)
            xlRange.Value = BG_SUB1 + BG_SUB2
            NAR(xlRange)
            xlRange = xlCells(j + i + 7, 8)
            xlRange.Value = (TOT_SUB1 - BG_SUB1) + (TOT_SUB2 - BG_SUB2)
            NAR(xlRange)
            xlRange = xlCells(j + i + 7, 9)
            xlRange.Value = (TOT_UP1 + TOT_ADD1 - TOT_SUB1) + (TOT_UP2 + TOT_ADD2 - TOT_SUB2)
            NAR(xlRange)
            'xlsheet.Cells(j + i + 7, 1).value = "    合  計"
            'xlsheet.Cells(j + i + 7, 2).value = TOT_UP1 + TOT_UP2
            'xlsheet.Cells(j + i + 7, 3).value = TOT_ADD1 + TOT_ADD2
            'xlsheet.Cells(j + i + 7, 4).value = BG_ADD1 + BG_ADD2
            'xlsheet.Cells(j + i + 7, 5).value = (TOT_ADD1 - BG_ADD1) + (TOT_ADD2 - BG_ADD2)
            'xlsheet.Cells(j + i + 7, 6).value = TOT_SUB1 + TOT_SUB2
            'xlsheet.Cells(j + i + 7, 7).value = BG_SUB1 + BG_SUB2
            'xlsheet.Cells(j + i + 7, 8).value = (TOT_SUB1 - BG_SUB1) + (TOT_SUB2 - BG_SUB2)
            'xlsheet.Cells(j + i + 7, 9).value = (TOT_UP1 + TOT_ADD1 - TOT_SUB1) + (TOT_UP2 + TOT_ADD2 - TOT_SUB2)
            xlbook.Save()

            If rdbSave.Checked = True Then
                MsgBox("檔案儲存路徑：" & "C:\App\bail\Report\bailf130.xls", , "保證金系統")
            Else
                xlbook.PrintOut()
            End If
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
        'xlbook.Close()
        'xlsheet = Nothing
        'lbook = Nothing
        'xlbooks = Nothing
        'lapp.Quit()
        'lapp = Nothing
        'C.Collect()

    End Sub

    Sub Bail020List()         '保管品
        If Year(tempdate) >= 1911 Then
            jname = CType(Year(tempdate) - 1911, String)
        Else
            jname = Year(tempdate).ToString
        End If
        'If rdbcourse2.Checked Then
        'jname = "(" & jname & ")" & Trim(strengname) & Chr(10) & Trim(strbank) & " " & Trim(strchkno) & " 定期存單 " & Trim(strcop)
        If rdbcourse2.Checked Then   '保證銀行、支票號
            If strchkno.IndexOf("保證") >= 0 Then
                jname = "(" & jname & ")" & Trim(strengname) & Chr(10) & Trim(strbank) & " " & Trim(strchkno) & " " & Trim(strcop)

            Else
                jname = "(" & jname & ")" & Trim(strengname) & Chr(10) & Trim(strbank) & " " & Trim(strchkno) & " 定存 " & Trim(strcop)
            End If
        Else
            '依工程
            jname = "(" & jname & ")" & Trim(strengname) & Chr(10) & " " & Trim(strcop)
        End If

        If chkAddEngno.Checked Then
            jname = strEngno & jname
        End If
        'Else
        '    If kind = "1" Then
        '        jname = jname & Trim(strengname) & " " & "履約品" & " " & Trim(strcop) & " " & Trim(strchkno)
        '    ElseIf kind = "2" Then
        '        jname = jname & Trim(strengname) & " " & "押標品" & " " & Trim(strcop) & " " & Trim(strchkno)
        '    ElseIf kind = "3" Then
        '        jname = jname & Trim(strengname) & " " & "保固品" & " " & Trim(strcop) & " " & Trim(strchkno)
        '    Else
        '        jname = jname & Trim(strengname) & " " & "差額保證品" & " " & Trim(strcop) & " " & Trim(strchkno)
        '    End If
        'End If
        amt1 = 0 : amt2 = 0 : amt3 = 0 : amt4 = 0 : amt5 = 0 : amt6 = 0 : amt7 = 0 : amt8 = 0
        amt1 = UP_IN - UP_PAY    '上年度結轉
        If INCOME - PAY - amt1 > 0 Then
            amt2 = INCOME - PAY - amt1    '本年度增加
        Else
            amt5 = amt1 - INCOME + PAY    '本年度減少
        End If

        If BG_ADD2 > 0 Then
            If BG_ADD2 > amt2 Then
                amt3 = amt2               '預算增加
            Else
                amt3 = BG_ADD2
            End If
        End If
        If BG_SUB2 > 0 Then
            If BG_SUB2 > amt5 Then
                amt6 = amt5               '預算減少
            Else
                amt6 = BG_SUB2
            End If
        End If

        amt4 = amt2 - amt3
        amt7 = amt6 - amt5
        amt8 = INCOME - PAY
        'adebt.jname = jname
        'adebt.amt1 = amt1                '上年度結轉
        'adebt.amt2 = amt2                '本年度增加 決算
        'adebt.amt3 = amt3                '           預算
        'adebt.amt4 = amt4                '           比較
        'adebt.amt5 = amt5                '本年度減少 決算
        'adebt.amt6 = amt6                '           預算
        'adebt.amt7 = Math.Abs(amt7)      '           比較   
        'adebt.amt8 = amt8
        'debts1.Add(adebt)                '本年底餘額

        Dim row As DataRow
        row = table1.NewRow
        row.Item("jname") = jname
        row.Item("amt1") = amt1
        row.Item("amt2") = amt2
        row.Item("amt3") = amt3
        row.Item("amt4") = amt4
        row.Item("amt5") = amt5
        row.Item("amt6") = amt6
        row.Item("amt7") = Math.Abs(amt7)
        row.Item("amt8") = amt8
        row.Item("engno") = strEngno
        row.Item("engno_4") = strEngno.Substring(3, 1)
        table1.Rows.Add(row)

        TOT_UP2 = TOT_UP2 + amt1
        TOT_ADD2 = TOT_ADD2 + amt2
        TOT_SUB2 = TOT_SUB2 + amt5
        INCOME = 0 : PAY = 0 : UP_IN = 0 : UP_PAY = 0
    End Sub
    Function createTable(ByVal tablename As String) As DataTable
        Dim mytable As DataTable
        mytable = New DataTable(tablename)
        '建立一個 DataColumn 物件
        Dim myColumn As New DataColumn

        ' 設定 DataColumn 物件的各個屬性以便定義欄位的結構描述

        With myColumn
            .ColumnName = "jname"
            .DataType = System.Type.GetType("System.String")
            '.AutoIncrement = True
            '.AutoIncrementSeed = 1
            '.AutoIncrementStep = 1
            '.ReadOnly = True
        End With
        mytable.Columns.Add(myColumn)

        Dim myColumn1 As New DataColumn
        With myColumn1
            .ColumnName = "amt1"
            .DataType = System.Type.GetType("System.Int32")
        End With
        mytable.Columns.Add(myColumn1)

        Dim myColumn2 As New DataColumn
        With myColumn2
            .ColumnName = "amt2"
            .DataType = System.Type.GetType("System.Int32")
        End With
        mytable.Columns.Add(myColumn2)

        Dim myColumn3 As New DataColumn
        With myColumn3
            .ColumnName = "amt3"
            .DataType = System.Type.GetType("System.Int32")
        End With
        mytable.Columns.Add(myColumn3)

        Dim myColumn4 As New DataColumn
        With myColumn4
            .ColumnName = "amt4"
            .DataType = System.Type.GetType("System.Int32")
        End With
        mytable.Columns.Add(myColumn4)

        Dim myColumn5 As New DataColumn
        With myColumn5
            .ColumnName = "amt5"
            .DataType = System.Type.GetType("System.Int32")
        End With
        mytable.Columns.Add(myColumn5)


        Dim myColumn6 As New DataColumn
        With myColumn6
            .ColumnName = "amt6"
            .DataType = System.Type.GetType("System.Int32")
        End With
        mytable.Columns.Add(myColumn6)

        Dim myColumn7 As New DataColumn
        With myColumn7
            .ColumnName = "amt7"
            .DataType = System.Type.GetType("System.Int32")
        End With
        mytable.Columns.Add(myColumn7)


        Dim myColumn8 As New DataColumn
        With myColumn8
            .ColumnName = "amt8"
            .DataType = System.Type.GetType("System.Int32")
        End With
        mytable.Columns.Add(myColumn8)

        Dim myColumn9 As New DataColumn
        With myColumn9
            .ColumnName = "engno"
            .DataType = System.Type.GetType("System.String")
        End With
        mytable.Columns.Add(myColumn9)


        Dim myColumn10 As New DataColumn
        With myColumn10
            .ColumnName = "engno_4"
            .DataType = System.Type.GetType("System.String")
        End With
        mytable.Columns.Add(myColumn10)
        Return mytable
    End Function
    Private Sub frmBail130_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim myds As DataSet
        LoadAfter = True
        txtYY.Text = Year(Now)   '預設年度為今年度

    End Sub

    Private Sub btnEnd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnd.Click
        Me.Close()
    End Sub


    Private Sub NAR(ByVal o As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(o)
        Catch
        Finally
            o = Nothing
        End Try
    End Sub


End Class
