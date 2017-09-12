
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO

Module vbdataio
    Dim sqlstr As String, mydataset As DataSet
    Dim oledbreader As OleDbDataReader, oledbconn As OleDbConnection, oledbcomm As OleDbCommand, oledbadapter As OleDbDataAdapter
    Dim sqlreader As SqlDataReader, sqlconn As SqlConnection, sqlcomm As SqlCommand, sqladapter As SqlDataAdapter
    Dim oledbupdcmd As OleDbCommand, sqlupdcmd As SqlCommand
    Dim ii As Integer, retstr As String
    Dim backstr As String
    Dim atp1, atp2 As Integer
    'Dim mastconn As String = INI_Read("CONFIG", "SET", "DNS_ACC")
    Dim UpdSqlValue As String, InsField, InsValue As String
    'Dim DNS_ACC As String = INI_Read("CONFIG", "SET", "DNS_ACC")



    Function openmember(ByVal connstr As String, ByVal membername As String, ByVal sqlstr As String)
        If connstr = "" Then
            connstr = fmMain.DNS_ACC
        End If
        If InStr(UCase(sqlstr), "FROM") = 0 Then
            sqlstr = "select * from " & sqlstr
        End If
        If InStr(LCase(connstr), "provider") > 0 Then 'oledb
            Try
                oledbconn = New OleDbConnection(connstr)
                oledbadapter = New OleDbDataAdapter(sqlstr, oledbconn)
                mydataset = New DataSet
                oledbadapter.Fill(mydataset, membername)
                oledbconn.Close()
            Catch
                oledbconn.Close()
            End Try
        Else
            Try
                sqlconn = New SqlConnection(connstr)
                sqladapter = New SqlDataAdapter(sqlstr, sqlconn)
                mydataset = New DataSet
                sqladapter.Fill(mydataset, membername)
                sqlconn.Close()
            Catch
                sqlconn.Close()
            End Try
        End If
        Return mydataset
    End Function

    Function openreader(ByVal connstr As String, ByVal sqlstr As String)
        If connstr = "" Then
            connstr = fmMain.DNS_ACC
        End If

        If InStr(LCase(connstr), "provider") > 0 Then  'oledb
            Try
                oledbconn = New OleDbConnection(connstr)
                oledbcomm = New OleDbCommand(sqlstr, oledbconn)
                oledbconn.Open()
                oledbreader = oledbcomm.ExecuteReader()
                Return oledbreader
            Catch
                oledbconn.Close()
                Throw
            End Try
        Else
            Try
                sqlconn = New SqlConnection(connstr)
                sqlcomm = New SqlCommand(sqlstr, sqlconn)
                sqlconn.Open()
                sqlreader = sqlcomm.ExecuteReader()
                Return sqlreader
            Catch
                sqlconn.Close()
                Throw
            End Try
        End If
    End Function

    Function updfunc(ByVal connstr As String, ByVal sqlstr As String, ByVal Parameters() As String)
        If connstr = "" Then
            connstr = fmMain.DNS_ACC
        End If

        If InStr(LCase(connstr), "provider") > 0 Then  'oledb
            oledbconn = New OleDbConnection(connstr)
            oledbconn.Open()
            oledbupdcmd = New OleDbCommand(sqlstr, oledbconn)

            For ii = 0 To Parameters.Length
                Dim subpara() As String
                If Parameters(ii) = "" Then Exit For
                subpara = Parameters(ii).Split(",")
                oledbupdcmd.Parameters.Add(New OleDbParameter(subpara(0), "oledbtype." & subpara(2)))
                oledbupdcmd.Parameters(subpara(0)).Value = subpara(1)
            Next
            oledbupdcmd.ExecuteNonQuery()
            oledbconn.Close()
        Else
            sqlconn = New SqlConnection(connstr)
            sqlcomm = New SqlCommand(sqlstr, sqlconn)
            sqlconn.Open()
            sqlreader = sqlcomm.ExecuteReader()
            Return sqlreader
        End If
    End Function
    Function setcheckbox(ByVal controlname, ByVal setdata)
        If Not IsDBNull(setdata) Then
            For ii = 0 To Len(setdata) - 1
                controlname.items(ii).selected = (Mid(setdata, ii + 1, 1) = "1")
            Next
        End If
    End Function

    Function getcheckbox(ByVal controlname)
        retstr = ""
        For ii = 0 To controlname.items.count - 1
            retstr &= IIf(controlname.items(ii).selected, "1", "0")
        Next
        Return retstr
    End Function

    Function runsql(ByVal connstr As String, ByVal sqlstr As String)
        If connstr = "" Then
            connstr = fmMain.DNS_ACC
        End If

        If InStr(LCase(connstr), "provider") > 0 Then  'oledb
            oledbconn = New OleDbConnection(connstr)
            oledbcomm = New OleDbCommand(sqlstr, oledbconn)
            oledbconn.Open()
            Try
                oledbcomm.ExecuteNonQuery()
                retstr = "sqlok"
            Catch
                retstr = "sqlerror"
            End Try
            oledbconn.Close()
            Return retstr
        Else
            sqlconn = New SqlConnection(connstr)
            sqlcomm = New SqlCommand(sqlstr, sqlconn)
            sqlconn.Open()
            Try
                sqlcomm.ExecuteNonQuery()
                retstr = "sqlok"
            Catch
                retstr = "sqlerror"
            End Try
            sqlconn.Close()
            Return retstr
        End If
    End Function


    Function backuprtn(ByVal oledbconn, ByVal filepath, ByVal sqlstr)
        If filepath = "" Then Exit Function
        On Error Resume Next
        MkDir(oledbconn & filepath)
        FileOpen(1, oledbconn & filepath & "\backdata.txt", OpenMode.Append)
        WriteLine(1, filepath, sqlstr, DateTime.Now)
        FileClose(1)
        On Error GoTo 0
    End Function

    Function setlist(ByVal controlname As Object, ByVal dataset As DataSet, ByVal membername As String, ByVal valuefield As String, ByVal textfield As String)
        controlname.datasource = dataset
        controlname.datamember = membername
        controlname.datavaluefield = valuefield
        controlname.datatextfield = textfield
        controlname.databind()
    End Function
    Function setlistview(ByVal controlname As Object, ByVal dataview As DataView, ByVal valuefield As String, ByVal textfield As String)
        controlname.datasource = dataview
        controlname.datavaluefield = valuefield
        controlname.datatextfield = textfield
        controlname.databind()
    End Function
    Function setgrid(ByVal controlname As Object, ByVal mydataset As DataSet, ByVal membername As String)
        controlname.datasource = mydataset
        controlname.datamember = membername
        controlname.databind()
        Return "ok"
    End Function

    Function setgridview(ByVal controlname As Object, ByVal mydataview As DataView)
        controlname.datasource = mydataview
        controlname.databind()
        Return "ok"
    End Function

    Sub findmultIndex(ByVal controlname, ByVal setvalue)
        For ii = 0 To controlname.items.count - 1
            controlname.items(ii).selected = IIf(Mid(setvalue, ii + 1, 1) = "1", True, False)
        Next
    End Sub

    Function FindIndexx(ByVal controlname, ByVal setvalue)
        Dim ControlsItems As Integer = controlname.items.count
        For ii = 0 To ControlsItems - 1
            If Trim(controlname.items(ii).value) = setvalue Then
                Exit For
            End If
        Next
        If ii = ControlsItems Then
            For ii = 0 To ControlsItems - 1
                If Trim(controlname.items(ii).text) = setvalue Then
                    Exit For
                End If
            Next
        End If
        If ii = ControlsItems Then
            ii = -1
        End If
        Return ii
    End Function

    Function nz(ByVal inputvalue, ByVal nullvalue)
        Return IIf(IsDBNull(inputvalue), nullvalue, inputvalue)
    End Function

    Function rate(ByVal chidvalue, ByVal monvalue, ByVal decvalue)
        If chidvalue = 0 Or monvalue = 0 Then Return 0
        Return Math.Round(chidvalue * 100 / monvalue, decvalue)
    End Function

    Function ValComa(ByVal lblNum As String)   '去除逗號
        Return Val(Replace(lblNum, ",", ""))
    End Function

    Function DlookUp(ByVal connstr As String, ByVal getfield As String, ByVal readtable As String, ByVal wherestr As String)
        If connstr = "" Then
            connstr = fmMain.DNS_ACC
        End If

        sqlstr = "select " & getfield & " from " & readtable & " where " & wherestr
        Dim existdata As Boolean
        If InStr(LCase(connstr), "provider") > 0 Then  'oledb
            Dim myreader As OleDbDataReader
            myreader = openreader(connstr, sqlstr)
            existdata = myreader.Read()
            If existdata Then
                Return nz(myreader.Item(getfield), "")
            Else
                Return ""
            End If
        Else
            Dim myreader As SqlDataReader
            myreader = openreader(connstr, sqlstr)
            existdata = myreader.Read()
            If existdata Then
                Return nz(myreader.Item(getfield), "")
            Else
                Return ""
            End If
        End If
    End Function

    Function Dmax(ByVal connstr As String, ByVal getfield As String, ByVal readtable As String, ByVal wherestr As String)
        If connstr = "" Then
            connstr = fmMain.DNS_ACC
        End If

        sqlstr = "select max(" & getfield & ") as maxvalue  from " & readtable & IIf(wherestr <> "", " where " & wherestr, "")
        Dim existdata As Boolean
        If InStr(LCase(connstr), "provider") > 0 Then  'oledb
            Dim myreader As OleDbDataReader
            myreader = openreader(connstr, sqlstr)
            existdata = myreader.Read()
            If existdata Then
                Return myreader.Item("maxvalue")
            Else
                Return ""
            End If
        Else
            Dim myreader As SqlDataReader
            myreader = openreader(connstr, sqlstr)
            existdata = myreader.Read()
            If existdata Then
                Return myreader.Item("maxvalue")
            Else
                Return ""
            End If
        End If
    End Function

    Function Dmin(ByVal connstr As String, ByVal getfield As String, ByVal readtable As String, ByVal wherestr As String)
        If connstr = "" Then
            connstr = fmMain.DNS_ACC
        End If

        sqlstr = "select min(" & getfield & ") as maxvalue  from " & readtable & IIf(wherestr <> "", " where " & wherestr, "")
        Dim existdata As Boolean
        If InStr(LCase(connstr), "provider") > 0 Then  'oledb
            Dim myreader As OleDbDataReader
            myreader = openreader(connstr, sqlstr)
            existdata = myreader.Read()
            If existdata Then
                Return myreader.Item("maxvalue")
            Else
                Return ""
            End If
        Else
            Dim myreader As SqlDataReader
            myreader = openreader(connstr, sqlstr)
            existdata = myreader.Read()
            If existdata Then
                Return myreader.Item("maxvalue")
            Else
                Return ""
            End If
        End If
    End Function

    Function Dcount(ByVal connstr As String, ByVal getfield As String, ByVal readtable As String, ByVal wherestr As String)
        If connstr = "" Then
            connstr = fmMain.DNS_ACC
        End If

        sqlstr = "select count(" & getfield & ") as maxvalue  from " & readtable & IIf(wherestr <> "", " where " & wherestr, "")
        Dim existdata As Boolean
        If InStr(LCase(connstr), "provider") > 0 Then  'oledb
            Dim myreader As OleDbDataReader
            myreader = openreader(connstr, sqlstr)
            existdata = myreader.Read()
            If existdata Then
                Return myreader.Item("maxvalue")
            Else
                Return ""
            End If
        Else
            Dim myreader As SqlDataReader
            myreader = openreader(connstr, sqlstr)
            existdata = myreader.Read()
            If existdata Then
                Return myreader.Item("maxvalue")
            Else
                Return ""
            End If
        End If
    End Function

    Sub checkboxclean(ByVal controlid)
        For ii = 0 To controlid.items.count - 1
            controlid.items(ii).selected = False
        Next
    End Sub

    Function cutright1(ByVal instr As String, ByVal compstr As String)
        instr = Trim(instr)
        If Len(instr) > 0 Then
            Return IIf(Right(instr, 1) = compstr, Left(instr, Len(instr) - 1), instr)
        Else
            Return instr
        End If
    End Function
    Function mstring(ByVal count, ByVal repstr)
        Dim retstr As String
        For ii = 1 To count
            retstr = retstr & repstr
        Next
        Return mstring
    End Function

    Function checkdatetime(ByVal indate, ByVal intime)
        Dim yy, mm, dd, atp As Integer, datestr, timestr As String
        indate = Replace(indate, ".", "/")

        If intime <> "" And Not IsDBNull(intime) Then
            If InStr(intime, ":") = 0 Then
                Select Case Len(intime)
                    Case 4
                        intime = Left(intime, 2) & ":" & Mid(intime, 3)
                    Case 2
                        intime = intime & ":00"
                End Select
            Else
                atp1 = InStr(intime, ":")
                intime = Val(Left(intime, atp1)) & ":" & Val(Mid(intime, atp1 + 1))
            End If
        End If

        datestr = indate
        If InStr(datestr, "/") > 0 Then
            atp = InStr(datestr, "/")
            yy = Left(datestr, atp - 1) : If yy < 1000 Then yy += 1911
            atp1 = InStr(atp + 1, datestr, "/")

            If atp1 > 0 Then
                mm = Mid(datestr, atp + 1, atp1 - atp - 1)
                If atp1 = Len(datestr) Then
                    dd = 1
                Else
                    dd = Mid(datestr, atp1 + 1, 2)
                End If
            ElseIf atp > 0 Then
                mm = Mid(datestr, atp + 1)
                dd = 1
            Else
                mm = 1
                dd = 1
            End If
        Else
            Select Case Len(datestr)
                Case 6
                    yy = Mid(datestr, 1, 2) + 1911 : mm = Mid(datestr, 3, 2) : dd = Mid(datestr, 5, 2)
                Case 7
                    yy = Mid(datestr, 1, 3) + 1911 : mm = Mid(datestr, 4, 2) : dd = Mid(datestr, 6, 2)
                Case 8
                    yy = Mid(datestr, 1, 4) : mm = Mid(datestr, 5, 2) : dd = Mid(datestr, 7, 2)
            End Select
        End If
        datestr = yy & "/" & mm & "/" & dd & " " & intime
        If Not IsDate(datestr) Then
            If Not IsDate(yy & "/" & mm & "/" & dd) Then
                MsgBox("日期不對")
                'datestr = "ERR 日期格式不對 2002/5/17 或 91/05/17 或 091/05/17 或 091/5/15 或 0910517 或 20020517 等 "
            Else
                MsgBox("時間格式不對 中間以:分開 或不在 24 小時之內")
                'datestr = "ERR 時間格式不對 中間以:分開 或不在 24 小時之內"
            End If
        End If
        Return datestr
    End Function

    Function checkdate(ByVal indate)
        Dim yy, mm, dd, atp As Integer, datestr, timestr As String
        indate = Replace(indate, ".", "/")
        datestr = indate
        If InStr(datestr, "/") > 0 Then
            atp = InStr(datestr, "/")
            yy = Left(datestr, atp - 1) : If yy < 1000 Then yy += 1911
            atp1 = InStr(atp + 1, datestr, "/")

            If atp1 > 0 Then
                mm = Mid(datestr, atp + 1, atp1 - atp - 1)
                If atp1 = Len(datestr) Then
                    dd = 1
                Else
                    dd = Mid(datestr, atp1 + 1, 2)
                End If
            ElseIf atp > 0 Then
                mm = Mid(datestr, atp + 1)
                dd = 1
            Else
                mm = 1
                dd = 1
            End If
        Else
            Select Case Len(datestr)
                Case 6
                    yy = Mid(datestr, 1, 2) + 1911 : mm = Mid(datestr, 3, 2) : dd = Mid(datestr, 5, 2)
                Case 7
                    yy = Mid(datestr, 1, 3) + 1911 : mm = Mid(datestr, 4, 2) : dd = Mid(datestr, 6, 2)
                Case 8
                    yy = Mid(datestr, 1, 4) : mm = Mid(datestr, 5, 2) : dd = Mid(datestr, 7, 2)
            End Select
        End If
        datestr = yy & "/" & mm & "/" & dd
        If Not IsDate(datestr) Then
            MsgBox("日期不對")
            'datestr = "ERR 日期格式不對 2002/5/17 或 91/05/17 或 091/05/17 或 091/5/15 或 0910517 或 20020517 等 "
        End If
        datestr = yy - 1911 & "/" & mm & "/" & dd   '以民國年顯示
        Return datestr
    End Function

    Function GenMsgBox(ByVal errmsg)
        retstr = "<script language=vbscript>" & vbCrLf & "sub window_onload() " & vbCrLf
        retstr &= "msgbox(" & Chr(34)
        retstr &= errmsg & Chr(34) & ")" & vbCrLf & "end sub" & vbCrLf
        retstr &= "</script>" & vbCrLf
        Return retstr
    End Function
    Function GenClientFunction(ByVal errmsg)
        retstr = "<script language=vbscript>" & vbCrLf & "sub window_onload() " & vbCrLf
        retstr &= errmsg
        retstr &= vbCrLf & "end sub" & vbCrLf
        retstr &= "</script>" & vbCrLf
        Return retstr
    End Function
    Function GenInputBox(ByVal titlemsg)
        retstr = "<script language=vbscript>" & vbCrLf & "inputbox(" & Chr(34)
        retstr &= titlemsg & Chr(34) & ")" & vbCrLf
        retstr &= "</script>" & vbCrLf
        Return retstr
    End Function

    Function findrecpos(ByVal connstr, ByVal sqlstr, ByVal getfield, ByVal compstr)
        If connstr = "" Then
            connstr = fmMain.DNS_ACC
        End If

        Dim existdata As Boolean, myreader
        If InStr(LCase(connstr), "provider") > 0 Then  'oledb
            myreader = openreader(connstr, sqlstr)
        Else
            myreader = openreader(connstr, sqlstr)
        End If
        ii = 1
        Do While myreader.read = True
            If myreader.item(getfield) = compstr Then
                Return ii
                Exit Do
            End If
            ii = ii + 1
        Loop
        Return 0
    End Function


    Function cutpara(ByVal para As String, ByVal paraname As String)

        atp1 = InStr(UCase(para), UCase(paraname))
        If atp1 = 0 Then
            Return GenMsgBox("ERR 參數 " & paraname & " 找不到")
            Exit Function
        End If
        atp1 = InStr(atp1, para, "=")
        atp2 = InStr(atp1, para, "|")


        Return Mid(para, atp1 + 1, atp2 - atp1 - 1)

    End Function
    Function replacepara(ByVal mastpara, ByVal cutpara, ByVal replpara)
        mastpara &= IIf(Right(mastpara, 1) <> "|", "|", "")
        mastpara = Replace(mastpara, "||", "|")
        atp1 = InStr(UCase(mastpara), UCase(cutpara))
        If atp1 > 0 Then
            atp2 = InStr(atp1, UCase(mastpara), "|")
            Return Left(mastpara, atp1 - 1) & replpara & Mid(mastpara, atp2)
        Else
            Return mastpara & replpara & IIf(Right(replpara, 1) <> "|", "|", "")
        End If
    End Function

    Function addfunc(ByVal a, ByVal b)
        Return a + b
    End Function
    Function dgvalue(ByVal indatagrid As DataGrid) As String
        Return indatagrid.Item(indatagrid.CurrentCell)
    End Function

    Function FindControl(ByVal ctlSource As Control, ByVal strName As String) As Control
        Dim ctlFind As Control, ctl As Control
        strName = strName.ToLower() '物件名稱不區分大小寫
        For Each ctl In ctlSource.Controls
            If ctl.Name.ToLower = strName Then
                Return ctl
            End If
            If ctl.Controls.Count > 0 Then '若還有下一層就遞回式地往下搜尋
                ctlFind = FindControl(ctl, strName)
                If Not ctlFind Is Nothing Then Return ctlFind
            End If
        Next
        'Return Nothing
        GenMsgBox(" 物件:" & strName & "  找不到")
    End Function

    Function FullDate(ByVal indate As String)
        Dim strOutDate As String
        strOutDate = Year(indate)
        If strOutDate <= 1000 Then strOutDate = strOutDate + 1911
        strOutDate &= "/" & Format(CDate(indate), "MM/dd")
        Return strOutDate
    End Function

    Function Grade(ByVal accno As String)
        Select Case Len(Trim(accno))
            Case Is = 1
                Return 1
            Case Is = 2
                Return 2
            Case Is = 3
                Return 3
            Case Is = 5
                Return 4
            Case Is <= 7
                Return 5
            Case Is <= 9
                Return 6
            Case Is <= 16
                Return 7
            Case Is = 17
                Return 8
        End Select
    End Function

    Function Season(ByVal date1 As Date)    '回復日期屬於那一季
        Dim i, ans As Integer
        i = Month(date1)
        Select Case i
            Case Is >= 10
                Return 4
            Case Is >= 7
                Return 3
            Case Is >= 4
                Return 2
            Case Is <= 3
                Return 1
        End Select
    End Function

    Function FormatAccno(ByVal accno As String)
        Dim i As Integer
        If Grade(accno) = 8 Then Return Mid(accno, 1, 1) & "-" & Mid(accno, 2, 4) & "-" & Mid(accno, 6, 2) & "-" & Mid(accno, 8, 2) & "-" & Mid(accno, 10, 7) & "-" & Mid(accno, 17, 1)
        If Grade(accno) = 7 Then Return Mid(accno, 1, 1) & "-" & Mid(accno, 2, 4) & "-" & Mid(accno, 6, 2) & "-" & Mid(accno, 8, 2) & "-" & Mid(accno, 10, 7)
        If Grade(accno) = 6 Then Return Mid(accno, 1, 1) & "-" & Mid(accno, 2, 4) & "-" & Mid(accno, 6, 2) & "-" & Mid(accno, 8, 2)
        If Grade(accno) = 5 Then Return Mid(accno, 1, 1) & "-" & Mid(accno, 2, 4) & "-" & Mid(accno, 6, 2)
        If Grade(accno) = 1 Then
            If Mid(accno, 1, 1) = "9" Then   '合計
                Return " "
            Else
                Return Mid(accno, 1, 1)
            End If
        Else
            Return Mid(accno, 1, 1) & "-" & Mid(accno, 2, 4)
        End If
    End Function

    Function RequireNO(ByVal connstr As String, ByVal noyear As Integer, ByVal kind As String)  '取用編號(取得號數控制檔的編號+1)
        If connstr = "" Then
            connstr = fmMain.DNS_ACC
        End If

        Dim tempdataset As DataSet
        Dim sqlstr, MaxNo As String
        Dim cnt As Integer
        sqlstr = "SELECT cont_no FROM acfno WHERE accyear=" & noyear & " and kind='" & kind & "'"
        'MaxNo = Dmax(connstr, "cont_no", "acfno", "accyear=" & noyear & " and kind='" & kind & "'") '無號碼時　回應””
        tempdataset = openmember(fmMain.DNS_ACC, "acfno", sqlstr)

        'If MaxNo = "" Then '空號
        If tempdataset.Tables("acfno").Rows.Count = 0 Then
            If kind = "3" Or kind = "6" Then   '轉帳傳票每年由2號起
                cnt = 2
            Else
                cnt = 1                      '收支傳票每年由1號起 
            End If
            sqlstr = "insert into acfno (accyear, kind, cont_no) values (" & noyear & ", '" & kind & "', " & cnt & ")"
        Else
            cnt = tempdataset.Tables("acfno").Rows(0).Item(0) + 1
            sqlstr = "update acfno set cont_no = cont_no+1 WHERE accyear=" & noyear & " and kind='" & kind & "'"
        End If
        retstr = runsql(connstr, sqlstr) '更新資料庫
        Return cnt
    End Function

    Function QueryNO(ByVal noyear As Integer, ByVal kind As String)  '查詢編號(取得號數控制檔的編號)
        Dim tempdataset As DataSet
        Dim sqlstr As String
        sqlstr = "SELECT cont_no FROM acfno WHERE accyear=" & noyear & " and kind='" & kind & "'"
        tempdataset = openmember(fmMain.DNS_ACC, "acfno", sqlstr)
        If tempdataset.Tables("acfno").Rows.Count = 0 Then
            If kind = "3" Or kind = "6" Then
                Return 1
            Else
                Return 0
            End If
        Else
            Return tempdataset.Tables("acfno").Rows(0).Item(0)
        End If
    End Function

    Function PrintSlip(ByVal noyear As Integer, ByVal kind As String, ByVal no1 As Integer)  '列印傳票
        Dim sqlstr As String
        sqlstr = "insert into 印表控制 (報表類型, 條件, 申請時間) values ('傳票', 'accyear=" & noyear & ";kind=" & kind & ";no_1_no=" & no1 & "', { fn NOW() })"
        retstr = runsql(fmMain.DNS_ACC, sqlstr) '寫入資料庫
        If retstr <> "sqlok" Then MsgBox("寫入印表控制資料庫錯誤" & sqlstr)
    End Function

    Public Property GenInsFunc()
        Get
            Dim InsField1, InsValue1 As String
            InsField1 = cutright1(InsField, ",")
            InsValue1 = cutright1(InsValue, ",")
            InsField = "" : InsValue = ""
            Return "(" & InsField1 & ") values (" & InsValue1 & ")"
        End Get
        Set(ByVal Value)

        End Set
    End Property

    Sub GenInsSql(ByRef FieldName As String, ByVal FieldValue As String, ByVal FieldType As String)
        If FieldType = "T" Or FieldType = "U" Then
            FieldValue = Replace(FieldValue, "'", "''")
            FieldValue = Replace(FieldValue, ",", "")
        End If

        Select Case FieldType
            Case Is = "T"
                If FieldValue <> "" Then
                    InsField &= FieldName & ","
                    InsValue &= "'" & FieldValue & "',"
                Else
                    InsField &= FieldName & ","
                    InsValue &= "'',"
                End If
            Case Is = "U"    'UniCode
                If FieldValue <> "" Then
                    InsField &= FieldName & ","
                    InsValue &= "N'" & FieldValue & "',"
                Else
                    InsField &= FieldName & ","
                    InsValue &= "'',"
                End If
            Case Is = "D"
                If FieldValue <> "" Then
                    FieldValue = Replace(FieldValue, ".", "/")
                    Dim yy As Integer, mmdd As String
                    InsField &= FieldName & ","
                    yy = Mid(FieldValue, 1, InStr(FieldValue, "/") - 1)
                    If yy < 1000 Then yy = yy + 1911
                    mmdd = Mid(FieldValue, InStr(FieldValue, "/"))
                    InsValue &= "'" & yy & mmdd & "',"
                    'Select Case Mid(FieldValue, 1, 1)
                    '    Case "1" '民國年
                    '        InsField &= FieldName & ","
                    '        InsValue &= "'" & strDateChinessToAD(strDateKindToDateKind(FieldValue, "/", "-")) & "',"
                    '    Case "2" '西元年
                    '        FieldValue = Replace(FieldValue, ".", "/")
                    '        Dim yy As Integer, mmdd As String
                    '        InsField &= FieldName & ","
                    '        yy = Mid(FieldValue, 1, InStr(FieldValue, "/") - 1)
                    '        If yy < 1000 Then yy = yy + 1911
                    '        mmdd = Mid(FieldValue, InStr(FieldValue, "/"))

                    '        InsValue &= "'" & yy & mmdd & "',"
                    'End Select
                End If
            Case Is = "N", "R"
                If FieldValue <> "" Then
                    InsField &= FieldName & ","
                    InsValue &= Replace(FieldValue, ",", "") & ","
                End If
        End Select
    End Sub

    Public Property genupdfunc()
        Get
            Dim UpdSqlValue1 As String
            UpdSqlValue1 = cutright1(UpdSqlValue, ",")
            UpdSqlValue = ""
            Return UpdSqlValue1
        End Get
        Set(ByVal Value)

        End Set
    End Property

    Sub GenUpdsql(ByVal FieldName As String, ByVal FieldValue As String, ByVal FieldType As String)
        Dim yy, atp As Integer
        If FieldType = "T" Or FieldType = "U" Then
            FieldValue = Replace(FieldValue, "'", "''")
            FieldValue = Replace(FieldValue, ",", "")
        End If

        Select Case FieldType

            Case "T"
                If nz(FieldValue, "") = "" Then
                    UpdSqlValue &= FieldName & "= ' ',"   'put space(1) to textfield
                Else
                    UpdSqlValue &= FieldName & "='" & FieldValue & "', "
                End If
            Case "U"
                If nz(FieldValue, "") = "" Then
                    UpdSqlValue &= FieldName & "= ' ',"   'put space(1) to textfield
                Else
                    UpdSqlValue &= FieldName & "=N'" & FieldValue & "', "
                End If
            Case "N", "R"
                UpdSqlValue &= FieldName & "=" & FieldValue & ", "
            Case "D"
                If Len(Trim(FieldValue)) = 0 Or Mid(FieldValue, 1, 4) = "0001" Then
                    UpdSqlValue &= FieldName & "= null, "
                Else
                    FieldValue = Replace(FieldValue, ".", "/")
                    atp = InStr(FieldValue, "/")
                    'yy = FieldValue.leftLeft(FieldValue, atp - 1)
                    yy = Mid(FieldValue, 1, atp - 1)

                    If yy < 1000 Then yy += 1911
                    Dim indate As Date
                    If IsDate(yy & Mid(FieldValue, atp)) Then
                        indate = CDate(yy & Mid(FieldValue, atp))
                        If Format(indate, "HH:mm") = "00:00" Then
                            UpdSqlValue &= FieldName & "='" & Format(indate, "yyyy/MM/dd") & "', "
                        Else
                            UpdSqlValue &= FieldName & "='" & Format(indate, "yyyy/MM/dd HH:mm") & "', "
                        End If
                    End If
                End If
        End Select
    End Sub

    Function ShortDate(ByVal indate As String)
        If indate = "" Then Return ""
        Dim strOutDate As String
        If Val(Mid(indate, 1, InStr(indate, "/") - 1)) < 1911 Then
            strOutDate = Format(Year(indate), 0) & "/" & Format(CDate(indate), "MM/dd")
        Else
            strOutDate = Format(Year(indate) - 1911, 0) & "/" & Format(CDate(indate), "MM/dd")
        End If
        Return strOutDate
    End Function

    Public Sub NAR(ByVal o As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(o)
        Catch
        Finally
            o = Nothing
        End Try
    End Sub
    Function GetAccno(ByVal Accno As String)
        Return Trim(Accno.Replace("-", "")).ToString
    End Function

    Function QueryBGAmt(ByVal noyear As Integer, ByVal accno As String)  '查詢預算餘額(取得全年預算扣除(已請購+已開支)的餘額)
        '業務單位查詢時,請購數並未update至totper,主計單位查詢時,請購數已由業務單位請購數update至totper  
        Dim tempdataset As DataSet
        Dim sqlstr As String
        sqlstr = "SELECT bg1+bg2+bg3+bg4+up1+up2+up3+up4-totper-totuse as balance,flow FROM bgf010 WHERE accyear=" & noyear & " and accno='" & accno & "'"
        tempdataset = openmember("", "temp", sqlstr)
        If tempdataset.Tables("temp").Rows.Count = 0 Then
            TransPara.TransP("flow") = "0"
            Return 0
        Else
            TransPara.TransP("flow") = tempdataset.Tables("temp").Rows(0).Item("flow")  '將可否溢支置全域變數
            Return tempdataset.Tables("temp").Rows(0).Item(0)
        End If
        tempdataset = Nothing
    End Function

    Function QueryUnUseAmt(ByVal noyear As Integer, ByVal accno As String, ByVal season As Integer)  '查詢開支餘額(取得當季可開支餘額(已分配數-已開支的餘額)
        '業務單位查詢時,開支數並未update至totuse,主計單位查詢時,開支數已由業務單位開支數update至totuse  
        Dim tempdataset As DataSet
        Dim sqlstr As String
        Dim i As Integer
        Dim ans As Decimal
        If noyear < GetYear(TransPara.TransP("UserDATE")) Then   'whh 95/5/31 modi 
            season = 4
        End If
        ans = 0
        sqlstr = "SELECT * FROM bgf010 WHERE accyear=" & noyear & " and accno='" & accno & "'"
        tempdataset = openmember("", "temp", sqlstr)
        If tempdataset.Tables("temp").Rows.Count = 0 Then
            TransPara.TransP("flow") = "0"
            Return 0
        Else
            For i = 1 To season
                ans = ans + tempdataset.Tables("temp").Rows(0).Item("bg" & LTrim(Str(i))) + tempdataset.Tables("temp").Rows(0).Item("up" & LTrim(Str(i)))
            Next
            TransPara.TransP("flow") = tempdataset.Tables("temp").Rows(0).Item("flow")  '將可否溢支置全域變數
            Return ans - tempdataset.Tables("temp").Rows(0).Item("totuse")
        End If
        '要加讀取2-1302收入總額,作為可開支數 must use 7grade accno
        tempdataset = Nothing
    End Function

    Function QueryUsedAmt(ByVal bgno As String)  '查詢該筆請購已開支總額
        Dim tempdataset As DataSet
        Dim sqlstr As String
        sqlstr = "SELECT sum(useamt) as useamt FROM bgf030 WHERE bgno='" & bgno & "'"
        tempdataset = openmember("", "temp", sqlstr)
        'If tempdataset.Tables("temp").Rows.Count = 0 Or tempdataset.Tables("temp").Rows(0).Item(0) Is DBNull Then
        If tempdataset.Tables("temp").Rows.Count = 0 Or IsDBNull(tempdataset.Tables("temp").Rows(0).Item(0)) Then
            Return 0
        Else
            Return tempdataset.Tables("temp").Rows(0).Item(0)
        End If
        tempdataset = Nothing
    End Function

    '支票號加1
    Public Function AddCheckNo(ByVal chkno As String)
        Dim intI As Integer
        Dim strI As String
        If Trim(chkno) = "" Then Return ""
        strI = Mid(chkno, 4, 7)    '前三碼預留給英文字
        intI = Len(strI)
        Return Mid(chkno, 1, 3) & Right(Format(Val(strI) + 1, "0000000"), intI)
    End Function

    Function genLongDate(ByVal Sdate As String)
        If Len(Trim(Sdate)) = 0 Or Mid(Sdate, 1, 4) = "0001" Then
            Return "null"
        Else
            Dim mmdd As String
            Dim yy As Integer
            yy = Mid(Sdate, 1, InStr(Sdate, "/") - 1)
            If yy < 1000 Then yy = yy + 1911
            mmdd = Mid(Sdate, InStr(Sdate, "/"))
            Return yy & mmdd
        End If
    End Function

    '將數字轉換成中文金額表示法
    Public Function GetChineseMoney(ByVal money As Long) As String
        Dim strEng As String = money.ToString()
        If strEng.Length > 16 Then Throw New Exception("數字超出範圍")

        Dim intLen As Integer, intCounter As Integer
        Dim strCh As String, strTempCh As String
        Dim strSeqCh1 As String, strSeqCh2 As String
        Dim strEng2Ch As String
        strEng2Ch = "零壹貳參肆伍陸柒捌玖"
        strSeqCh1 = " 拾佰仟 拾佰仟 拾佰仟 拾佰仟"
        strSeqCh2 = " 萬億兆"
        strEng = CStr(CDec(strEng))
        intLen = Len(strEng)
        For intCounter = 1 To intLen
            strTempCh = Mid(strEng2Ch, Val(Mid(strEng, intCounter, 1)) + 1, 1)
            If strTempCh = "零" And intLen <> 1 Then
                If Mid(strEng, intCounter + 1, 1) = "0" Or (intLen - intCounter + 1) Mod 4 = 1 Then
                    strTempCh = ""
                End If
            Else
                strTempCh = strTempCh & Trim(Mid(strSeqCh1, intLen - intCounter + 1, 1))
            End If
            If (intLen - intCounter + 1) Mod 4 = 1 Then
                strTempCh = strTempCh & Mid(strSeqCh2, (intLen - intCounter + 1) \ 4 + 1, 1)
                If intCounter > 3 Then
                    If Mid(strEng, intCounter - 3, 4) = "0000" Then strTempCh = _
                    Microsoft.VisualBasic.Left(strTempCh, Len(strTempCh) - 1)
                End If
            End If
            strCh = strCh & Trim(strTempCh)
        Next
        Return strCh
    End Function

    Sub ClearInsField_Insvalue()
        InsField = ""
        InsValue = ""
    End Sub

    Public Property genwherefunc()
        Get
            Dim UpdSqlValue1 As String
            UpdSqlValue1 = cutright3(UpdSqlValue, "and")
            UpdSqlValue = ""
            Return UpdSqlValue1
        End Get
        Set(ByVal Value)

        End Set
    End Property

    Function cutright3(ByVal instr As String, ByVal compstr As String)
        instr = Trim(instr)
        If Len(instr) > 0 Then
            Return IIf(Right(instr, 3) = compstr, Left(instr, Len(instr) - 3), instr)
        Else
            Return instr
        End If
    End Function

    Sub GenWheresql(ByVal FieldName As String, ByVal FieldValue As String, ByVal FieldType As String)
        Dim yy, atp As Integer

        Select Case FieldType

            Case "T"
                If nz(FieldValue, "") = "" Then
                    'UpdSqlValue &= FieldName & "= NULL ,"  'put null to textfield
                    UpdSqlValue &= FieldName & "= ' ' and "   'put space(1) to textfield
                Else
                    UpdSqlValue &= FieldName & "='" & Replace(FieldValue, "'", "''") & "' and "
                End If
            Case "N", "R"
                UpdSqlValue &= FieldName & "=" & Replace(FieldValue, ",", "") & "and "
            Case "D"
                If Len(Trim(FieldValue)) = 0 Or Mid(FieldValue, 1, 4) = "0001" Then
                    UpdSqlValue &= FieldName & "= null and "
                Else
                    atp = InStr(FieldValue, "/")
                    'yy = FieldValue.leftLeft(FieldValue, atp - 1)
                    yy = Mid(FieldValue, 1, atp - 1)

                    If yy < 1000 Then yy += 1911
                    Dim indate As Date
                    If IsDate(yy & Mid(FieldValue, atp)) Then
                        indate = CDate(yy & Mid(FieldValue, atp))
                        If Format(indate, "HH:mm") = "00:00" Then
                            UpdSqlValue &= FieldName & "='" & Format(indate, "yyyy/MM/dd") & "' and "
                        Else
                            UpdSqlValue &= FieldName & "='" & Format(indate, "yyyy/MM/dd HH:mm") & "' and "
                        End If
                    End If
                End If
        End Select
    End Sub

    Sub AppReport_Copy(ByVal strSysname As String, ByVal strReportName As String, ByVal strCopyPath As String)
        Dim AppReportPath As String = Application.StartupPath & "\APP\" & strSysname & "\報表樣本\" & strReportName '*.ini檔案路徑
        Dim Createpath As String = ""
        Dim Createpath1 As String = ""
        If strSysname = "bail" Then
            Createpath = "c:\APP\" & strSysname & "\ReportData\"
            Createpath1 = "c:\APP\" & strSysname & "\Report\"
        Else
            Createpath = "c:\APP\" & strSysname & "\報表樣本\"
            Createpath1 = "c:\APP\" & strSysname & "\報表\"
        End If
        

        If Not File.Exists(AppReportPath) Then
            MsgBox("找不到報表樣本，請洽資訊人員" & vbNewLine & AppReportPath)
            Exit Sub
        End If

        If Directory.Exists(Createpath) = False Then
            ' Create the directory.
            Directory.CreateDirectory(Createpath)
        End If

        If Directory.Exists(Createpath1) = False Then
            ' Create the directory.
            Directory.CreateDirectory(Createpath1)
        End If

        FileCopy(AppReportPath, strCopyPath)
    End Sub


End Module
