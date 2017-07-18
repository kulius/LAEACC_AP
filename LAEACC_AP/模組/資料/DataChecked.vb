'::::::::::::::::::
':::  資料驗證  :::
'::::::::::::::::::

Module DataChecked

    Sub MyDataChecked()

    End Sub

#Region "資料輸入"
    ''' <summary>
    ''' 必要欄位輸入防呆
    ''' </summary>
    ''' <param name="objArray">物件陣列(Object)</param>
    ''' <param name="strArray">說明文字陣列(String)</param>
    ''' <returns>是否驗證成功(Boolean)</returns>    
    Function blnInputCheck(ByVal objArray() As Object, ByVal strArray() As String) As Boolean
        Dim blnCheck As Boolean = True
        Dim strMsg As String = ""

        '判斷是否有資料
        For I As Integer = 0 To objArray.Length - 1 Step 1
            Select Case objArray(I).GetType.Name
                Case "TextBox"
                    Dim objTextBox As TextBox = objArray(I)

                    If objTextBox.Text = "" Then
                        strMsg = "※系統訊息：【" & strArray(I) & "】未填入！※"
                        objTextBox.Focus() : blnCheck = False

                    End If

                Case "ComboBox"
                    Dim objComboBox As ComboBox = objArray(I)

                    If objComboBox.SelectedValue = "~Z" Or objComboBox.SelectedValue = "" Then
                        strMsg = "※系統訊息：【" & strArray(I) & "】未選擇※！"
                        objComboBox.Focus() : blnCheck = False
                    End If
            End Select

            If blnCheck = False Then Exit For
        Next

        If blnCheck = False Then MsgBox(strMsg, MsgBoxStyle.Exclamation, "注意") '顯示錯誤訊息

        Return blnCheck
    End Function

    ''' <summary>
    ''' 輸入欄位數字防呆
    ''' </summary>
    ''' <param name="objArray">物件陣列(Object)</param>
    ''' <param name="strArray">說明文字陣列(String)</param>
    ''' <returns>是否驗證成功(Boolean)</returns>    
    Function blnInputNumeric(ByVal objArray() As Object, ByVal strArray() As String) As Boolean
        Dim blnCheck As Boolean = True
        Dim strMsg As String = ""

        '判斷是否有資料
        For I As Integer = 0 To objArray.Length - 1 Step 1
            Select Case objArray(I).GetType.Name
                Case "TextBox"
                    Dim objTextBox As TextBox = objArray(I)

                    If IsNumeric(objTextBox.Text) = False And objTextBox.Text <> "" Then
                        strMsg = "※系統訊息：【" & strArray(I) & "】必數字！※"
                        objTextBox.Focus() : blnCheck = False
                    End If
            End Select

            If blnCheck = False Then Exit For
        Next

        If blnCheck = False Then MsgBox(strMsg, MsgBoxStyle.Exclamation, "注意") '顯示錯誤訊息

        Return blnCheck
    End Function
#End Region
End Module
