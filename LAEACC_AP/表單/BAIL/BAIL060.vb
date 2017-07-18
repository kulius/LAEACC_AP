Imports System.Globalization
Imports JBC.Printing
Imports System.IO
Imports Microsoft.Office.Interop
Public Class BAIL060
    Dim LoadAfter, Dirty As Boolean
    Dim sqlstr As String
    Dim bm As BindingManagerBase, mydataset As DataSet
    Dim UserId, UserName, UserUnit As String

    Private Sub frmBail060_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim myds As DataSet
        LoadAfter = True
    End Sub

    Private Sub btnInq_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInq.Click
        Dim intI As Integer
        Dim k As Integer
        Dim msgYN
        Dim strMsg As String
        Dim strDate As String
        Dim mydatasetp As DataSet
        Dim mydatasetInq As DataSet
        Dim datarowp() As DataRow
        If txtInqCop.Text.Trim = "" Then
            If rdbBail.Checked Then
                strMsg = "未設定查詢條件，是否列出全部未退之保證金？"
            Else
                strMsg = "未設定查詢條件，是否列出全部未退之保管品？"
            End If
            msgYN = MsgBox(strMsg, MsgBoxStyle.YesNoCancel)
            If msgYN <> MsgBoxResult.Yes Then
                'MsgBox("請檢查輸入值!")
                Exit Sub
            End If
        End If



        '單位代碼
        Dim sql As String
        Dim ds As DataSet
        Dim dr, dr1 As DataRow
        Try
            sql = "SELECT unit_id, unit_name FROM Auth_Unit ORDER BY  unit_id"
            ds = openmember("stafconn", "auth_unit", sql)

            dr = ds.Tables(0).NewRow
            dr.Item(0) = "0010"
            dr.Item(1) = "工務組"
            ds.Tables(0).Rows.Add(dr)

            dr1 = ds.Tables(0).NewRow
            dr1.Item(0) = "0020"
            dr1.Item(1) = "管理組"
            ds.Tables(0).Rows.Add(dr1)
        Catch ex As Exception

        End Try



        If rdbBail.Checked Then '查保證金

            If rdbCop.Checked Then
                sqlstr = "select a.*, b.UNIT AS unit from (SELECT bailf010.engno as engno,enf010.cop as cop,sum(amt) as amt  FROM bailf010 inner join enf010 on bailf010.engno=enf010.engno where rp='1'  and enf010.cop like '%" & txtInqCop.Text & "%' and (balance is null or balance='') group by bailf010.engno,enf010.cop ) a left outer join enf010 b on a.engno=b.engno order by a.engno" '依工程編號、商號找出保證金收的記錄
                mydataset = openmember("", "bailf010", sqlstr)
                sqlstr = "select a.*, b.UNIT AS unit from (SELECT bailf010.engno as engno,enf010.cop as cop,sum(amt) as amt  FROM bailf010 inner join enf010 on bailf010.engno=enf010.engno where rp='2'  and enf010.cop like '%" & txtInqCop.Text & "%' and (balance is null or balance='') group by bailf010.engno,enf010.cop ) a left outer join enf010 b on a.engno=b.engno order by a.engno" '依工程編號、商號找出保證金收的記錄
                mydatasetp = openmember("", "bailf010", sqlstr)
            Else
                sqlstr = "select a.*, b.UNIT AS unit from (SELECT bailf010.engno as engno,enf010.cop as cop,sum(amt) as amt  FROM bailf010 inner join enf010 on bailf010.engno=enf010.engno where rp='1'  and enf010.idno='" & txtInqCop.Text & "' and (balance is null or balance='') group by bailf010.engno,enf010.cop ) a left outer join enf010 b on a.engno=b.engno order by a.engno" '依工程編號、商號找出保證金收的記錄
                mydataset = openmember("", "bailf010", sqlstr)
                sqlstr = "select a.*, b.UNIT AS unit from (SELECT bailf010.engno as engno,enf010.cop as cop,sum(amt) as amt  FROM bailf010 inner join enf010 on bailf010.engno=enf010.engno where rp='2'  and enf010.idno='" & txtInqCop.Text & "' and (balance is null or balance='') group by bailf010.engno,enf010.cop ) a left outer join enf010 b on a.engno=b.engno order by a.engno" '依工程編號、商號找出保證金收的記錄

                mydatasetp = openmember("", "bailf010", sqlstr)
            End If
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

            Dim c2 As DataColumn = New DataColumn '原因 (未逾期)
            c2.DataType = System.Type.GetType("System.String")
            c2.ColumnName = "reason"
            c2.DefaultValue = ""
            mydataset.Tables("bailf010").Columns.Add(c2)

            For intI = 0 To (mydataset.Tables("bailf010").Rows.Count - 1)
                datarowp = mydatasetp.Tables("bailf010").Select("engno='" & mydataset.Tables("BAILF010").Rows(intI).Item("engno") & "' and cop='" & mydataset.Tables("BAILF010").Rows(intI).Item("cop") & "'")
                If datarowp.Length > 0 Then
                    mydataset.Tables("bailf010").Rows(intI).Item("amt") = mydataset.Tables("bailf010").Rows(intI).Item("amt") - datarowp(0).Item("amt")
                Else
                    mydataset.Tables("bailf010").Rows(intI).Item("amt") = mydataset.Tables("bailf010").Rows(intI).Item("amt")
                End If

                If mydataset.Tables("bailf010").Rows(intI).Item("amt") = 0 Then
                    mydataset.Tables("bailf010").Rows(intI).Delete()
                Else
                    sqlstr = "SELECT engname FROM enf010 where engno='" & mydataset.Tables("BAILF010").Rows(intI).Item("engno") & "'"
                    mydatasetInq = openmember("", "enf010", sqlstr)
                    If (mydatasetInq.Tables("enf010").Rows.Count > 0) Then
                        mydataset.Tables("BAILF010").Rows(intI).Item("engname") = mydatasetInq.Tables("enf010").Rows(0).Item("engname")
                    End If
                    mydatasetInq.Clear()
                    '取得保固期限
                    sqlstr = "SELECT date_e,cop FROM bailf010 where engno='" & mydataset.Tables("BAILF010").Rows(intI).Item("engno") & "'  and rp='1' and date_e is not null and (balance is null or balance='') order by date_e desc"  'and kind='3'  96/4/11修改  因另外有租約期限
                    mydatasetInq = openmember("", "bailf010", sqlstr)
                    If (mydatasetInq.Tables("BAILF010").Rows.Count > 0) Then
                        'mydataset.Tables("BAILF010").Rows(intI).Item("date_e") = Format(DateAdd(DateInterval.Year, -1911, mydatasetInq.Tables("bailf010").Rows(0).Item("date_e")), "yyy/MM/dd")
                        If IsDate(mydatasetInq.Tables("bailf010").Rows(0).Item("date_e")) Then
                            mydataset.Tables("BAILF010").Rows(intI).Item("date_e") = Format(mydatasetInq.Tables("bailf010").Rows(0).Item("date_e"), "yyy/MM/dd")
                            If mydatasetInq.Tables("bailf010").Rows(0).Item("date_e") > TransPara.TransP("UserDATE") Then
                                mydataset.Tables("BAILF010").Rows(intI).Item("reason") = "未逾期"
                            Else
                                mydataset.Tables("BAILF010").Rows(intI).Item("reason") = ""
                            End If
                        End If
                        'mydataset.Tables("BAILF010").Rows(intI).Item("cop") = mydatasetInq.Tables("bailf010").Rows(0).Item("cop")
                    End If
                    If (mydatasetInq.Tables("BAILF010").Rows.Count > 1) Then
                        strDate = ""
                        For k = 0 To mydatasetInq.Tables("BAILF010").Rows.Count - 1
                            strDate = strDate & " " & Format(mydatasetInq.Tables("bailf010").Rows(k).Item("date_e"), "yyy/MM/dd")
                        Next
                        If IsDate(mydatasetInq.Tables("bailf010").Rows(0).Item("date_e")) Then
                            mydataset.Tables("BAILF010").Rows(intI).Item("date_e") = strDate 'Format(mydatasetInq.Tables("bailf010").Rows(0).Item("date_e"), "yyy/MM/dd")
                            If mydatasetInq.Tables("bailf010").Rows(0).Item("date_e") < TransPara.TransP("UserDATE") Then
                                mydataset.Tables("BAILF010").Rows(intI).Item("reason") = ""
                            Else
                                mydataset.Tables("BAILF010").Rows(intI).Item("reason") = "?" '有二筆不同日期到期的保固
                            End If
                        End If
                    End If

                    '取得所屬單位
                    Dim j As Integer
                    For j = 0 To ds.Tables(0).Rows.Count - 1
                        If Not IsDBNull(mydataset.Tables("BAILF010").Rows(intI).Item("unit")) Then
                            If mydataset.Tables("BAILF010").Rows(intI).Item("unit") = ds.Tables(0).Rows(j).Item("unit_id") Then
                                mydataset.Tables("BAILF010").Rows(intI).Item("unit") = ds.Tables(0).Rows(j).Item("unit_name")
                            End If
                        End If
                    Next
                    '---------------------------------------------------------------------------
                    mydatasetInq.Clear()
                End If
            Next
            mydatasetp = Nothing
            mydatasetInq = Nothing
            mydataset.Tables("bailf010").AcceptChanges()
            DataGridTableStyle1.MappingName = "bailf010"
            dtgBail.DataSource = mydataset
            dtgBail.DataMember = "bailf010"
            If mydataset.Tables("bailf010").Rows.Count > 0 Then
                btnPrt.Enabled = True
            End If
        Else                     '查保管品

            If rdbCop.Checked Then
                sqlstr = "select a.*, b.UNIT AS unit from (SELECT bailf020.engno as engno,enf010.cop as cop,sum(amt) as amt  FROM bailf020 inner join enf010 on bailf020.engno=enf010.engno where rp='1'  and enf010.cop like '%" & txtInqCop.Text & "%' and (balance is null or balance='') group by bailf020.engno,enf010.cop ) a left outer join enf010 b on a.engno=b.engno order by a.engno" '依工程編號、商號找出保證金收的記錄
                mydataset = openmember("", "bailf020", sqlstr)
                sqlstr = "select a.*, b.UNIT AS unit from (SELECT bailf020.engno as engno,enf010.cop as cop,sum(amt) as amt  FROM bailf020 inner join enf010 on bailf020.engno=enf010.engno where rp='2'  and enf010.cop like '%" & txtInqCop.Text & "%' and (balance is null or balance='') group by bailf020.engno,enf010.cop ) a left outer join enf010 b on a.engno=b.engno order by a.engno" '依工程編號、商號找出保證金收的記錄
                mydatasetp = openmember("", "bailf020", sqlstr)
            Else
                sqlstr = "select a.*, b.UNIT AS unit from (SELECT bailf020.engno as engno,enf010.cop as cop,sum(amt) as amt  FROM bailf020 inner join enf010 on bailf020.engno=enf010.engno where rp='1'  and enf010.idno='" & txtInqCop.Text & "' and (balance is null or balance='') group by bailf020.engno,enf010.cop) a left outer join enf010 b on a.engno=b.engno order by a.engno" '依工程編號、商號找出保證金收的記錄
                mydataset = openmember("", "bailf020", sqlstr)
                sqlstr = "select a.*, b.UNIT AS unit from (SELECT bailf020.engno as engno,enf010.cop as cop,sum(amt) as amt  FROM bailf020 inner join enf010 on bailf020.engno=enf010.engno where rp='2'  and enf010.idno='" & txtInqCop.Text & "' and (balance is null or balance='') group by bailf020.engno,enf010.cop) a left outer join enf010 b on a.engno=b.engno order by a.engno" '依工程編號、商號找出保證金收的記錄

                mydatasetp = openmember("", "bailf020", sqlstr)
            End If

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

            Dim c2 As DataColumn = New DataColumn '原因 (未逾期)
            c2.DataType = System.Type.GetType("System.String")
            c2.ColumnName = "reason"
            c2.DefaultValue = ""
            mydataset.Tables("bailf020").Columns.Add(c2)


            For intI = 0 To (mydataset.Tables("bailf020").Rows.Count - 1)
                datarowp = mydatasetp.Tables("bailf020").Select("engno='" & mydataset.Tables("BAILF020").Rows(intI).Item("engno") & "' and cop='" & mydataset.Tables("BAILF020").Rows(intI).Item("cop") & "'")
                If datarowp.Length > 0 Then
                    mydataset.Tables("bailf020").Rows(intI).Item("amt") = mydataset.Tables("bailf020").Rows(intI).Item("amt") - datarowp(0).Item("amt")
                Else
                    mydataset.Tables("bailf020").Rows(intI).Item("amt") = mydataset.Tables("bailf020").Rows(intI).Item("amt")
                End If

                If mydataset.Tables("bailf020").Rows(intI).Item("amt") = 0 Then
                    mydataset.Tables("bailf020").Rows(intI).Delete()
                Else
                    sqlstr = "SELECT engname,cop FROM enf010 where engno='" & mydataset.Tables("BAILF020").Rows(intI).Item("engno") & "'"
                    mydatasetInq = openmember("", "enf010", sqlstr)
                    If (mydatasetInq.Tables("enf010").Rows.Count > 0) Then
                        mydataset.Tables("BAILF020").Rows(intI).Item("engname") = mydatasetInq.Tables("enf010").Rows(0).Item("engname")
                        mydataset.Tables("BAILF020").Rows(intI).Item("cop") = mydatasetInq.Tables("enf010").Rows(0).Item("cop")
                    End If
                    mydatasetInq.Clear()
                    '取得保固期限
                    sqlstr = "SELECT date_e,cop FROM bailf020 where engno='" & mydataset.Tables("BAILF020").Rows(intI).Item("engno") & "'  and rp='1' and date_e is not null and (balance is null or balance='') order by date_e desc"  'and kind='3' 96/4/11修改  因另外有租約期限
                    mydatasetInq = openmember("", "bailf020", sqlstr)
                    If (mydatasetInq.Tables("bailf020").Rows.Count > 0) Then
                        If IsDate(mydatasetInq.Tables("bailf020").Rows(0).Item("date_e")) Then
                            mydataset.Tables("BAILF020").Rows(intI).Item("date_e") = Format(mydatasetInq.Tables("bailf020").Rows(0).Item("date_e"), "yyy/MM/dd")
                            If mydatasetInq.Tables("bailf020").Rows(0).Item("date_e") > TransPara.TransP("UserDATE") Then
                                mydataset.Tables("BAILF020").Rows(intI).Item("reason") = "未逾期"
                            Else
                                mydataset.Tables("BAILF020").Rows(intI).Item("reason") = ""
                            End If

                        End If
                    End If
                    If (mydatasetInq.Tables("BAILF020").Rows.Count > 1) Then
                        strDate = ""
                        For k = 0 To mydatasetInq.Tables("BAILF020").Rows.Count - 1
                            strDate = strDate & " " & Format(mydatasetInq.Tables("bailf020").Rows(k).Item("date_e"), "yyy/MM/dd")
                        Next
                        If IsDate(mydatasetInq.Tables("bailf020").Rows(0).Item("date_e")) Then
                            mydataset.Tables("BAILF020").Rows(intI).Item("date_e") = strDate 'Format(mydatasetInq.Tables("bailf010").Rows(0).Item("date_e"), "yyy/MM/dd")
                            If mydatasetInq.Tables("bailf020").Rows(0).Item("date_e") < TransPara.TransP("UserDATE") Then
                                mydataset.Tables("BAILF020").Rows(intI).Item("reason") = ""
                            Else
                                mydataset.Tables("BAILF020").Rows(intI).Item("reason") = "?" '有二筆不同日期到期的保固
                            End If
                        End If
                    End If
                    '取得所屬單位
                    Dim j As Integer
                    For j = 0 To ds.Tables(0).Rows.Count - 1
                        If Not IsDBNull(mydataset.Tables("BAILF020").Rows(intI).Item("unit")) Then
                            If mydataset.Tables("BAILF020").Rows(intI).Item("unit") = ds.Tables(0).Rows(j).Item("unit_id") Then
                                mydataset.Tables("BAILF020").Rows(intI).Item("unit") = ds.Tables(0).Rows(j).Item("unit_name")
                            End If
                        End If
                    Next
                    '---------------------------------------------------------------------------                    mydatasetInq.Clear()
                End If
            Next
            mydatasetp = Nothing
            mydatasetInq = Nothing
            mydataset.Tables("bailf020").AcceptChanges()
            DataGridTableStyle1.MappingName = "bailf020"
            dtgBail.DataSource = mydataset
            dtgBail.DataMember = "bailf020"
            If mydataset.Tables("bailf020").Rows.Count > 0 Then
                btnPrt.Enabled = True
            End If

        End If
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged

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
                tt1 = "c:\App\bail\ReportData\bailf060sample.xls" 'mypath + "\bailf060sample.xls"
                tt2 = "c:\App\bail\Report\bailf060.xls"           'mypath + "\bailf060.xls"
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
            If rdbBail.Checked Then '查保證金
                xlRange = xlCells(1, 1)
                xlRange.Value = "查詢" & txtInqCop.Text & "未退之保證金"
                NAR(xlRange)

                For i = 0 To mydataset.Tables("bailf010").Rows.Count - 1
                    ' For j = 0 To 4
                    xlRange = xlCells(i + 3, 1)
                    xlRange.Value = i + 1
                    NAR(xlRange)
                    xlRange = xlCells(i + 3, 2)
                    xlRange.Value = nz(mydataset.Tables("bailf010").Rows(i).Item("engno"), "")
                    NAR(xlRange)
                    xlRange = xlCells(i + 3, 3)
                    xlRange.Value = nz(Trim(mydataset.Tables("bailf010").Rows(i).Item("engname")), "")
                    NAR(xlRange)
                    xlRange = xlCells(i + 3, 4)
                    xlRange.Value = nz(mydataset.Tables("bailf010").Rows(i).Item("date_e"), "")
                    NAR(xlRange)
                    xlRange = xlCells(i + 3, 5)
                    xlRange.Value = nz(mydataset.Tables("bailf010").Rows(i).Item("cop"), "")
                    NAR(xlRange)
                    xlRange = xlCells(i + 3, 6)
                    xlRange.Value = nz(mydataset.Tables("bailf010").Rows(i).Item("amt"), "")
                    NAR(xlRange)
                    xlRange = xlCells(i + 3, 7)
                    xlRange.Value = nz(mydataset.Tables("bailf010").Rows(i).Item("unit"), "")
                    NAR(xlRange)
                    xlRange = xlCells(i + 3, 8)
                    xlRange.Value = nz(mydataset.Tables("bailf010").Rows(i).Item("reason"), "")
                    NAR(xlRange)
                Next
            Else                   '查保管品
                xlRange = xlCells(1, 1)
                xlRange.Value = "查詢" & txtInqCop.Text & "未退之保管品"
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
                    xlRange.Value = nz(mydataset.Tables("bailf020").Rows(i).Item("date_e"), "")
                    NAR(xlRange)
                    xlRange = xlCells(i + 3, 5)
                    xlRange.Value = nz(mydataset.Tables("bailf020").Rows(i).Item("cop"), "")
                    NAR(xlRange)
                    xlRange = xlCells(i + 3, 6)
                    xlRange.Value = nz(mydataset.Tables("bailf020").Rows(i).Item("amt"), "")
                    NAR(xlRange)
                    xlRange = xlCells(i + 3, 7)
                    xlRange.Value = nz(mydataset.Tables("bailf020").Rows(i).Item("unit"), "")
                    NAR(xlRange)
                    xlRange = xlCells(i + 3, 8)
                    xlRange.Value = nz(mydataset.Tables("bailf020").Rows(i).Item("reason"), "")
                    NAR(xlRange)

                    'Next
                Next
            End If


            If NowPrinting = MsgBoxResult.Yes Then
                xlbook.PrintOut()
            Else
                MsgBox("檔案儲存路徑：" & "C:\App\bail\Report\bailf060.xls", , "保證金系統")
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
