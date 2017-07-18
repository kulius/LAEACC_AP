Imports System.Globalization
Imports JBC.Printing
Imports System.IO
Imports Microsoft.Office.Interop
Public Class BAIL050
    Dim LoadAfter, Dirty As Boolean
    Dim sqlstr As String
    Dim mydataset As DataSet

    Private Sub frmBail050_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim myds As DataSet
        LoadAfter = True
    End Sub

    Private Sub btnInq_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInq.Click
        'TabControl1.Enabled = True
        Dim intI As Integer
        Dim mydatasetp As DataSet
        Dim mydatasetInq As DataSet
        Dim datarowp() As DataRow
        ' Creates and initializes a DateTimeFormatInfo associated with the en-US culture.
        ' Sets a DateTime to April 3, 2002 of the Gregorian calendar.
        'Dim myDT As New DateTime(2002, 4, 3, New GregorianCalendar)

        'myDT = Today
        ' MsgBox("檔案儲存路徑：" & Application.StartupPath, , "保證金保管品管理系統")
        If rdbBail.Checked Then '查保證金
            'dtgBail.Visible = False
            'dtgSafekeep.Visible = True
            'Dim dtgStyle As New DataGridTableStyle
            'dtgStyle.MappingName = "bailf010"
            'dtgBail.TableStyles.Add(dtgStyle)
            'sqlstr = "SELECT engno,sum(amt) as amt  FROM bailf010 where rp='1' and kind='3' and (date_e<'" & Today.ToString("yyyy/M/d", CultureInfo.InvariantCulture) & "' or date_e is null) and (balance is null or balance='') group by engno order by engno"    '依工程編號找出保證金保固收的記錄
            'mydataset =  openmember("", "bailf010", sqlstr)
            'sqlstr = "SELECT engno,sum(amt) as amt  FROM bailf010 where rp='2' and kind='3' and (date_e<'" & Today.ToString("yyyy/M/d", CultureInfo.InvariantCulture) & "' or date_e is null) and (balance is null or balance='') group by engno order by engno" '依工程編號找出保證金保固支的記錄
            'mydatasetp =  openmember("", "bailf010", sqlstr)


            'sqlstr = "SELECT engno,sum(amt) as amt  FROM bailf010 where rp='1' and  (date_e<'" & Today.ToString("yyyy/M/d", CultureInfo.InvariantCulture) & "' or date_e is null) and (balance is null or balance='') group by engno order by engno"    '依工程編號找出保證金保固收的記錄
            'mydataset =  openmember("", "bailf010", sqlstr)
            'sqlstr = "SELECT engno,sum(amt) as amt  FROM bailf010 where rp='2' and  (date_e<'" & Today.ToString("yyyy/M/d", CultureInfo.InvariantCulture) & "' or date_e is null) and (balance is null or balance='') group by engno order by engno" '依工程編號找出保證金保固支的記錄
            'mydatasetp =  openmember("", "bailf010", sqlstr)

            '96/1/19拿掉kind3及date_e null條件
            sqlstr = "SELECT engno,sum(amt) as amt  FROM bailf010 where rp='1' and  (date_e<'" & Today.ToString("yyyy/M/d", CultureInfo.InvariantCulture) & "' ) and (balance is null or balance='') group by engno order by engno"    '依工程編號找出保證金保固收的記錄
            mydataset = openmember("", "bailf010", sqlstr)
            sqlstr = "SELECT engno,sum(amt) as amt  FROM bailf010 where rp='2' and  (date_e<'" & Today.ToString("yyyy/M/d", CultureInfo.InvariantCulture) & "' ) and (balance is null or balance='') group by engno order by engno" '依工程編號找出保證金保固支的記錄
            mydatasetp = openmember("", "bailf010", sqlstr)

            Dim c As DataColumn = New DataColumn   '工程名稱
            c.DataType = System.Type.GetType("System.String")
            c.ColumnName = "engname"
            c.DefaultValue = ""
            mydataset.Tables("bailf010").Columns.Add(c)

            Dim c1 As DataColumn = New DataColumn '到期日
            c1.DataType = System.Type.GetType("System.String")
            c1.ColumnName = "date_e"
            c1.DefaultValue = ""
            mydataset.Tables("bailf010").Columns.Add(c1)

            Dim c2 As DataColumn = New DataColumn   '商號
            c2.DataType = System.Type.GetType("System.String")
            c2.ColumnName = "cop"
            c2.DefaultValue = ""
            mydataset.Tables("bailf010").Columns.Add(c2)

            For intI = 0 To (mydataset.Tables("bailf010").Rows.Count - 1)
                datarowp = mydatasetp.Tables("bailf010").Select("engno='" & mydataset.Tables("BAILF010").Rows(intI).Item("engno") & "'")     '依工程代號找出已付的金額
                If datarowp.Length > 0 Then
                    mydataset.Tables("bailf010").Rows(intI).Item("amt") = mydataset.Tables("bailf010").Rows(intI).Item("amt") - datarowp(0).Item("amt")  '已收金額減去已付金額
                Else
                    mydataset.Tables("bailf010").Rows(intI).Item("amt") = mydataset.Tables("bailf010").Rows(intI).Item("amt")
                End If

                If mydataset.Tables("bailf010").Rows(intI).Item("amt") = 0 Then
                    mydataset.Tables("bailf010").Rows(intI).Delete()   '去除已付清保固金的項目
                Else
                    sqlstr = "SELECT engname,cop FROM enf010 where engno='" & mydataset.Tables("BAILF010").Rows(intI).Item("engno") & "'"   '查工程名稱、商號
                    mydatasetInq = openmember("", "enf010", sqlstr)
                    If (mydatasetInq.Tables("enf010").Rows.Count > 0) Then
                        mydataset.Tables("BAILF010").Rows(intI).Item("engname") = mydatasetInq.Tables("enf010").Rows(0).Item("engname")
                        mydataset.Tables("BAILF010").Rows(intI).Item("cop") = mydatasetInq.Tables("enf010").Rows(0).Item("cop")
                    End If
                    mydatasetInq.Clear()
                    '取得保固期限及商號
                    sqlstr = "SELECT date_e,cop FROM bailf010 where engno='" & mydataset.Tables("BAILF010").Rows(intI).Item("engno") & "'  and rp='1' and (date_e<'" & Today.ToString("yyyy/M/d", CultureInfo.InvariantCulture) & "' or date_e is null) order by date_e desc"  'and kind='3'  96/4/11修改  因另外有租約期限
                    mydatasetInq = openmember("", "bailf010", sqlstr)
                    If (mydatasetInq.Tables("bailf010").Rows.Count > 0) Then
                        If IsDate(mydatasetInq.Tables("bailf010").Rows(0).Item("date_e")) Then
                            mydataset.Tables("BAILF010").Rows(intI).Item("date_e") = Format(mydatasetInq.Tables("bailf010").Rows(0).Item("date_e"), "yyy/MM/dd")
                        End If
                        '如保證金檔(bailf010)有商號資料存在則覆蓋工程檔商號資料
                        If Not IsDBNull(mydatasetInq.Tables("BAILF010").Rows(0).Item("cop")) AndAlso Trim(mydatasetInq.Tables("BAILF010").Rows(0).Item("cop")) <> "" Then  '當enf010無商號資料時，由bailf010抓取
                            mydataset.Tables("BAILF010").Rows(intI).Item("cop") = mydatasetInq.Tables("bailf010").Rows(0).Item("cop")
                        End If
                        mydatasetInq.Clear()
                    End If
                End If
            Next
            mydataset.Tables("bailf010").AcceptChanges()
            DataGridTableStyle1.MappingName = "bailf010"
            dtgBail.DataSource = mydataset
            dtgBail.DataMember = "bailf010"
            If mydataset.Tables("bailf010").Rows.Count > 0 Then
                btnPrt.Enabled = True
            End If
        Else                     '查保管品

            'sqlstr = "SELECT engno,sum(amt) as amt  FROM bailf020 where rp='1' and kind='3' and (date_e<'" & Today.ToString("yyyy/M/d", CultureInfo.InvariantCulture) & "' or date_e is null) and (balance is null or balance='') group by engno order by engno" '依工程編號找出保管品保固收的記錄
            'mydataset =  openmember("", "bailf020", sqlstr)
            'sqlstr = "SELECT engno,sum(amt) as amt  FROM bailf020 where rp='2' and kind='3' and (date_e<'" & Today.ToString("yyyy/M/d", CultureInfo.InvariantCulture) & "' or date_e is null) and (balance is null or balance='') group by engno order by engno" '依工程編號找出保管品保固支的記錄
            'mydatasetp =  openmember("", "bailf020", sqlstr)

            'sqlstr = "SELECT engno,sum(amt) as amt  FROM bailf020 where rp='1' and  (date_e<'" & Today.ToString("yyyy/M/d", CultureInfo.InvariantCulture) & "' or date_e is null) and (balance is null or balance='') group by engno order by engno" '依工程編號找出保管品保固收的記錄
            'mydataset =  openmember("", "bailf020", sqlstr)
            'sqlstr = "SELECT engno,sum(amt) as amt  FROM bailf020 where rp='2' and  (date_e<'" & Today.ToString("yyyy/M/d", CultureInfo.InvariantCulture) & "' or date_e is null) and (balance is null or balance='') group by engno order by engno" '依工程編號找出保管品保固支的記錄
            'mydatasetp =  openmember("", "bailf020", sqlstr)
            '96/1/19拿掉kind3及date_e null條件
            sqlstr = "SELECT engno,sum(amt) as amt  FROM bailf020 where rp='1' and  (date_e<'" & Today.ToString("yyyy/M/d", CultureInfo.InvariantCulture) & "' or date_e is null) and (balance is null or balance='') group by engno order by engno" '依工程編號找出保管品保固收的記錄
            mydataset = openmember("", "bailf020", sqlstr)
            sqlstr = "SELECT engno,sum(amt) as amt  FROM bailf020 where rp='2' and  (date_e<'" & Today.ToString("yyyy/M/d", CultureInfo.InvariantCulture) & "' or date_e is null) and (balance is null or balance='') group by engno order by engno" '依工程編號找出保管品保固支的記錄
            mydatasetp = openmember("", "bailf020", sqlstr)


            Dim c As DataColumn = New DataColumn   '工程名稱
            c.DataType = System.Type.GetType("System.String")
            c.ColumnName = "engname"
            c.DefaultValue = ""
            mydataset.Tables("bailf020").Columns.Add(c)

            Dim c1 As DataColumn = New DataColumn '到期日
            c1.DataType = System.Type.GetType("System.String")
            c1.ColumnName = "date_e"
            c1.DefaultValue = ""
            mydataset.Tables("bailf020").Columns.Add(c1)

            Dim c2 As DataColumn = New DataColumn   '商號
            c2.DataType = System.Type.GetType("System.String")
            c2.ColumnName = "cop"
            c2.DefaultValue = ""
            mydataset.Tables("bailf020").Columns.Add(c2)

            For intI = 0 To (mydataset.Tables("bailf020").Rows.Count - 1)
                datarowp = mydatasetp.Tables("bailf020").Select("engno='" & mydataset.Tables("BAILF020").Rows(intI).Item("engno") & "'")     '依工程編號找出已付的金額
                If datarowp.Length > 0 Then
                    mydataset.Tables("bailf020").Rows(intI).Item("amt") = mydataset.Tables("bailf020").Rows(intI).Item("amt") - datarowp(0).Item("amt")
                Else
                    mydataset.Tables("bailf020").Rows(intI).Item("amt") = mydataset.Tables("bailf020").Rows(intI).Item("amt")
                End If

                If mydataset.Tables("bailf020").Rows(intI).Item("amt") = 0 Then
                    mydataset.Tables("bailf020").Rows(intI).Delete()            '去除已退清保固品的項目
                Else
                    sqlstr = "SELECT engname,cop FROM enf010 where engno='" & mydataset.Tables("BAILF020").Rows(intI).Item("engno") & "'"
                    mydatasetInq = openmember("", "enf010", sqlstr)
                    If (mydatasetInq.Tables("enf010").Rows.Count > 0) Then
                        mydataset.Tables("BAILF020").Rows(intI).Item("engname") = mydatasetInq.Tables("enf010").Rows(0).Item("engname")
                        mydataset.Tables("BAILF020").Rows(intI).Item("cop") = mydatasetInq.Tables("enf010").Rows(0).Item("cop")
                    End If
                    mydatasetInq.Clear()
                    sqlstr = "SELECT date_e,cop FROM bailf020 where engno='" & mydataset.Tables("BAILF020").Rows(intI).Item("engno") & "' and kind='3' and rp='1' and (date_e<'" & Today.ToString("yyyy/M/d", CultureInfo.InvariantCulture) & "' or date_e is null) order by date_e desc"
                    mydatasetInq = openmember("", "bailf020", sqlstr)
                    If (mydatasetInq.Tables("bailf020").Rows.Count > 0) Then
                        'mydataset.Tables("BAILF020").Rows(intI).Item("date_e") = Format(DateAdd(DateInterval.Year, -1911, mydatasetInq.Tables("bailf020").Rows(0).Item("date_e")), "yyy/MM/dd")
                        If IsDate(mydatasetInq.Tables("bailf020").Rows(0).Item("date_e")) Then
                            mydataset.Tables("BAILF020").Rows(intI).Item("date_e") = Format(mydatasetInq.Tables("bailf020").Rows(0).Item("date_e"), "yyy/MM/dd")
                        End If
                        If Not IsDBNull(mydatasetInq.Tables("BAILF020").Rows(0).Item("cop")) AndAlso Trim(mydatasetInq.Tables("BAILF020").Rows(0).Item("cop")) <> "" Then
                            mydataset.Tables("BAILF020").Rows(intI).Item("cop") = mydatasetInq.Tables("bailf020").Rows(0).Item("cop")
                        End If
                        mydatasetInq.Clear()
                    End If
                End If
            Next
            mydatasetInq = Nothing
            mydatasetp = Nothing
            mydataset.Tables("bailf020").AcceptChanges()
            DataGridTableStyle1.MappingName = "bailf020"
            dtgBail.DataSource = mydataset
            dtgBail.DataMember = "bailf020"
            If mydataset.Tables("bailf020").Rows.Count > 0 Then
                btnPrt.Enabled = True
            End If

        End If
    End Sub

    Private Sub btnPrt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrt.Click
        btnPrt.Enabled = False
        Dim response As MsgBoxStyle

        response = (MsgBox("是否直接列印!", MsgBoxStyle.YesNoCancel, "查詢保管品保固期限已過尚未退還者"))
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
                tt1 = "c:\App\bail\ReportData\bailf050sample.xls" 'mypath + "\bailf050sample.xls"
                tt2 = "c:\App\bail\Report\bailf050.xls" 'mypath + "\bailf050.xls"
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

            If rdbBail.Checked Then '查保證金
                xlRange = xlCells(1, 1)
                xlRange.Value = "查詢保證金保固期限已過尚未退還者"
                NAR(xlRange)
                For i = 0 To mydataset.Tables("bailf010").Rows.Count - 1
                    For j = 0 To 4
                        xlRange = xlCells(i + 3, 1)
                        xlRange.Value = nz(mydataset.Tables("bailf010").Rows(i).Item("engno"), "")
                        NAR(xlRange)
                        xlRange = xlCells(i + 3, 2)
                        xlRange.Value = nz(Trim(mydataset.Tables("bailf010").Rows(i).Item("engname")), "")
                        NAR(xlRange)
                        xlRange = xlCells(i + 3, 3)
                        xlRange.Value = nz(mydataset.Tables("bailf010").Rows(i).Item("date_e"), "")
                        NAR(xlRange)
                        xlRange = xlCells(i + 3, 4)
                        xlRange.Value = nz(mydataset.Tables("bailf010").Rows(i).Item("cop"), "")
                        NAR(xlRange)
                        xlRange = xlCells(i + 3, 5)
                        xlRange.Value = nz(mydataset.Tables("bailf010").Rows(i).Item("amt"), "")
                        NAR(xlRange)
                        'xlsheet.Cells(i + 3, 1).value = mydataset.Tables("bailf010").Rows(i).Item("engno")
                        'xlsheet.Cells(i + 3, 2).value = Trim(mydataset.Tables("bailf010").Rows(i).Item("engname"))
                        'xlsheet.Cells(i + 3, 3).value = mydataset.Tables("bailf010").Rows(i).Item("date_e")
                        'xlsheet.Cells(i + 3, 4).value = mydataset.Tables("bailf010").Rows(i).Item("cop")
                        'xlsheet.Cells(i + 3, 5).value = mydataset.Tables("bailf010").Rows(i).Item("amt")
                    Next
                Next
            Else                   '查保管品
                xlRange = xlCells(1, 1)
                xlRange.Value = "查詢保管品保固期限已過尚未退還者"
                NAR(xlRange)
                For i = 0 To mydataset.Tables("bailf020").Rows.Count - 1
                    For j = 0 To 4
                        xlRange = xlCells(i + 3, 1)
                        xlRange.Value = mydataset.Tables("bailf020").Rows(i).Item("engno")
                        NAR(xlRange)
                        xlRange = xlCells(i + 3, 2)
                        xlRange.Value = Trim(mydataset.Tables("bailf020").Rows(i).Item("engname"))
                        NAR(xlRange)
                        xlRange = xlCells(i + 3, 3)
                        xlRange.Value = mydataset.Tables("bailf020").Rows(i).Item("date_e")
                        NAR(xlRange)
                        xlRange = xlCells(i + 3, 4)
                        xlRange.Value = mydataset.Tables("bailf020").Rows(i).Item("cop")
                        NAR(xlRange)
                        xlRange = xlCells(i + 3, 5)
                        xlRange.Value = mydataset.Tables("bailf020").Rows(i).Item("amt")
                        NAR(xlRange)
                        'xlsheet.Cells(i + 3, 1).value = mydataset.Tables("bailf020").Rows(i).Item("engno")
                        'xlsheet.Cells(i + 3, 2).value = Trim(mydataset.Tables("bailf020").Rows(i).Item("engname"))
                        'xlsheet.Cells(i + 3, 3).value = mydataset.Tables("bailf020").Rows(i).Item("date_e")
                        'xlsheet.Cells(i + 3, 4).value = mydataset.Tables("bailf020").Rows(i).Item("cop")
                        'xlsheet.Cells(i + 3, 5).value = mydataset.Tables("bailf020").Rows(i).Item("amt")
                    Next
                Next
            End If

            If NowPrinting = MsgBoxResult.Yes Then
                xlbook.PrintOut()
            Else
                MsgBox("檔案儲存路徑： " & "C:\App\bail\Report\bailf050.xls", , "保證金系統")
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


    Private Sub btnEnd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnd.Click
        Me.Close()
    End Sub

    Private Sub rdbBail_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbBail.CheckedChanged
        btnPrt.Enabled = False
    End Sub
End Class
