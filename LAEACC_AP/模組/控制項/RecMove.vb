Public Class RecMove
    Dim bm As BindingManagerBase
    Dim LoadAfter As Boolean
    Dim UpdSqlValue As String, InsField, InsValue As String
    Public Event TransJob(ByVal JobName As String, ByVal JobPara As String)
    Public Property Setds() As BindingManagerBase
        Get

        End Get
        Set(ByVal Value As BindingManagerBase)
            If LoadAfter = True Then
                bm = Value
                bm.Position = 0 : Call IOPOS()
            End If
        End Set
    End Property
    Public Property Setpos() As Integer
        Get

        End Get
        Set(ByVal Value As Integer)
            If LoadAfter = True Then
                bm.Position = Value : Call IOPOS()
            End If
        End Set
    End Property
    Public Property FilesPara() As String
        Get

        End Get
        Set(ByVal Value As String)

        End Set
    End Property

    Private Sub FirstRec_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FirstRec.Click
        bm.Position = 0
        Call IOPOS()
    End Sub

    Private Sub Lastrec_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LastRec.Click
        bm.Position = bm.Count - 1
        Call IOPOS()
    End Sub

    Private Sub PreRec_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PreRec.Click
        bm.Position -= 1
        Call IOPOS()
    End Sub

    Private Sub NextRec_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NextRec.Click
        bm.Position += 1
        Call IOPOS()
    End Sub


    Sub IOPOS()
        FirstRec.Enabled = True
        LastRec.Enabled = True
        NextRec.Enabled = True
        PreRec.Enabled = True
        If bm.Position <= 0 Then
            bm.Position = 0
            FirstRec.Enabled = False
            PreRec.Enabled = False
        End If
        If bm.Position >= bm.Count - 1 Then
            bm.Position = bm.Count - 1
            LastRec.Enabled = False
            NextRec.Enabled = False
        End If

        Recpos.Text = bm.Position + 1 & "/" & bm.Count - 1
        GotoRec.Visible = False
        If bm.Position > 0 Then RaiseEvent TransJob("記錄移動", bm.Position.ToString)

    End Sub


    Private Sub GotoRec_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GotoRec.Click
        If IsNumeric(Recpos.Text) Then
            bm.Position = CInt(Recpos.Text) - 1
            Call IOPOS()
            Recpos.Focus()
        End If
        GotoRec.Visible = False

    End Sub

    Private Sub Recpos_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Recpos.TextChanged
        If LoadAfter = False Then Exit Sub
        GotoRec.Visible = True
    End Sub

    Private Sub RecMove_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadAfter = True
    End Sub

    Private Sub RecDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RecDel.Click
        If MsgBox("確定要刪除嗎?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1) = MsgBoxResult.Yes Then
            RaiseEvent TransJob("刪除記錄", bm.Position.ToString)
        End If
    End Sub

    Private Sub RecUpd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RecUpdCMD.Click
        If LoadAfter = False Then Exit Sub
        RaiseEvent TransJob("回寫記錄cmd", bm.Position.ToString)
    End Sub

    Private Sub UpdStr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If LoadAfter = False Then Exit Sub
        RaiseEvent TransJob("回寫記錄str", bm.Position.ToString)
    End Sub

    Private Sub RecIns_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RecIns.Click
        If LoadAfter = False Then Exit Sub
        RaiseEvent TransJob("新增記錄", bm.Position.ToString)
    End Sub
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
        Select Case FieldType
            Case Is = "T"
                If FieldValue <> "" Then
                    InsField &= FieldName & ","
                    InsValue &= "'" & FieldValue & "',"
                Else
                    InsField &= FieldName & ","
                    InsValue &= "'',"
                    'InsField &= FieldName & ","
                    'InsValue &= "' ',"   '不給null值,而給space(1)
                End If
            Case Is = "U"     'UNICODE 
                If FieldValue <> "" Then
                    InsField &= FieldName & ","
                    InsValue &= "N'" & FieldValue & "',"
                Else
                    InsField &= FieldName & ","
                    InsValue &= "'',"
                    'InsField &= FieldName & ","
                    'InsValue &= "' ',"   '不給null值,而給space(1)
                End If
            Case Is = "D"
                If FieldValue <> "" Then
                    Dim yy As Integer, mmdd As String
                    InsField &= FieldName & ","
                    yy = Mid(FieldValue, 1, InStr(FieldValue, "/") - 1)
                    If yy < 1000 Then yy = yy + 1911
                    mmdd = Mid(FieldValue, InStr(FieldValue, "/"))
                    InsValue &= "'" & yy & mmdd & "',"
                End If
            Case Is = "N", "R"
                If FieldValue <> "" Then
                    InsField &= FieldName & ","
                    InsValue &= Val(Replace(FieldValue, ",", "")) & ","
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
            If LoadAfter = True Then
                '   Call genupdsql(iofield, iovalue, datatype)
            End If
        End Set
    End Property

    Sub GenUpdsql(ByVal FieldName As String, ByVal FieldValue As String, ByVal FieldType As String)
        Dim yy, atp As Integer

        Select Case FieldType

            Case "T"
                If nz(FieldValue, "") = "" Then
                    'UpdSqlValue &= FieldName & "= NULL ,"  'put null to textfield
                    UpdSqlValue &= FieldName & "= ' ',"   'put space(1) to textfield
                Else
                    UpdSqlValue &= FieldName & "='" & Replace(FieldValue, "'", "''") & "', "
                End If
            Case "U"     'UniCode
                If nz(FieldValue, "") = "" Then
                    UpdSqlValue &= FieldName & "= ' ',"   'put space(1) to textfield
                Else
                    UpdSqlValue &= FieldName & "= N'" & Replace(FieldValue, "'", "''") & "', "
                End If
            Case "N", "R"
                UpdSqlValue &= FieldName & "=" & Val(Replace(FieldValue, ",", "")) & ", "
            Case "D"
                If Len(Trim(FieldValue)) = 0 Or Mid(FieldValue, 1, 4) = "0001" Then
                    UpdSqlValue &= FieldName & "= null, "
                Else
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

    Sub GenInsSql(ByRef FieldName As String, ByVal FieldValue As Object, ByVal FieldType As String)
        Select Case FieldType
            Case Is = "T"
                If nz(FieldValue, "") <> "" Then
                    InsField &= FieldName & ","
                    InsValue &= "'" & FieldValue & "',"
                Else
                    InsField &= FieldName & ","
                    InsValue &= "null,"
                    'InsField &= FieldName & ","
                    'InsValue &= "' ',"   '不給null值,而給space(1)
                End If
            Case Is = "TU" 'unicode
                If nz(FieldValue, "") <> "" Then
                    InsField &= FieldName & ","
                    InsValue &= "N'" & FieldValue & "',"
                Else
                    InsField &= FieldName & ","
                    InsValue &= "null,"
                    'InsField &= FieldName & ","
                    'InsValue &= "' ',"   '不給null值,而給space(1)
                End If
            Case Is = "D"
                If Not IsDBNull(FieldValue) Then
                    Dim yy As Integer, mmdd As String
                    InsField &= FieldName & ","
                    yy = Mid(FieldValue, 1, InStr(FieldValue, "/") - 1)
                    If yy < 1000 Then yy = yy + 1911
                    mmdd = Mid(FieldValue, InStr(FieldValue, "/"))
                    InsValue &= "'" & yy & mmdd & "',"
                Else
                    InsField &= FieldName & ","
                    InsValue &= "null,"
                End If
            Case Is = "N", "R"
                If CType(FieldValue, String) <> "" Then
                    InsField &= FieldName & ","
                    InsValue &= Replace(FieldValue, ",", "") & ","
                End If
        End Select
    End Sub
    Sub GenUpdsql(ByVal FieldName As String, ByVal FieldValue As Object, ByVal FieldType As String)
        Dim yy, atp As Integer
        Select Case FieldType

            Case "T"
                If nz(FieldValue, "") = "" Then
                    UpdSqlValue &= FieldName & "= NULL ,"
                Else
                    UpdSqlValue &= FieldName & "='" & Replace(FieldValue, "'", "''") & "', "
                End If
            Case "TU"
                If nz(FieldValue, "") = "" Then
                    UpdSqlValue &= FieldName & "= NULL ,"
                Else
                    UpdSqlValue &= FieldName & "=N'" & Replace(FieldValue, "'", "''") & "', "
                End If

            Case "N", "R"
                UpdSqlValue &= FieldName & "=" & FieldValue & ", "
            Case "D"
                If IsDBNull(FieldValue) Then
                    UpdSqlValue &= FieldName & "= NULL ,"
                Else
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
End Class
