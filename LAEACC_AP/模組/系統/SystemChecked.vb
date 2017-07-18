'::::::::::::::::
'::: 系統檢查 :::
'::::::::::::::::
Module SystemChecked
    Public Sub MySystemChecked()

    End Sub

#Region "系統檢查"
    ''' <summary>
    ''' 重要檔案是否存在
    ''' </summary>
    ''' <returns>系統訊息(Boolean)</returns>    
    Function FileIsExist() As Boolean
        Dim strFileINI() As String = {"BASIC.ini", "CONFIG.ini", "VERIFY.ini"} '[INI資料夾]主要檔案
        'Dim strFileSYS() As String = {"Upgrade.exe"} '[SYS資料夾]主要檔案


        '++ 開啟檢測 ++
        For I As Integer = 0 To strFileINI.Length - 1 Step 1
            If My.Computer.FileSystem.FileExists(Application.StartupPath & "\INI\" & strFileINI(I)) = False Then MsgBox("【" & strFileINI(I) & "】，檔案已遺失或損毀!!", MsgBoxStyle.Exclamation, "注意") : Return False : Exit Function
        Next
        'For I As Integer = 0 To strFileSYS.Length - 1 Step 1
        '    If My.Computer.FileSystem.FileExists(Application.StartupPath & "\SYS\" & strFileSYS(I)) = False Then MsgBox("【" & strFileSYS(I) & "】，檔案已遺失或損毀!!", MsgBoxStyle.Exclamation, "注意") : Return False : Exit Function
        'Next


        Return True
    End Function

    ''' <summary>
    ''' 伺服器連線是否正常開啟檢測
    ''' </summary>
    ''' <param name="strIP">檢測IP</param>
    ''' <returns>系統訊息(Boolean)</returns>    
    Function NetworkConnection(ByVal strIP As String) As Boolean
        Dim blnCheck As Boolean = True

        Try
            '檢測網路是否連線
            If My.Computer.Network.IsAvailable Then
                blnCheck = My.Computer.Network.Ping(strIP, 1000)
            Else
                blnCheck = My.Computer.Network.IsAvailable
            End If
        Catch ex As Exception

        End Try

        Return blnCheck
    End Function
#End Region
End Module